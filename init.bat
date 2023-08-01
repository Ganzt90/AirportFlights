@echo off
DBNAME="demo.sql"
:MENU_START
@echo off
cls
set INPUT=false
set "MENU_OPTION="
set "DBSIZE="
set "OPTION2_INPUT="
echo +===============================================+
echo . SELECT DEMO DB SIZE                           .
echo +===============================================+
echo .                                               .
echo .  1) SMALL                                     .
echo .  2) MEDIUM                                    .
echo .  3) BIG                                       .
echo .  4) EXIT                                      .
echo .                                               .
echo +===============================================+
set /p MENU_OPTION="OPTION: "

IF %MENU_OPTION%==1 GOTO OPTION1
IF %MENU_OPTION%==2 GOTO OPTION2
IF %MENU_OPTION%==3 GOTO OPTION3
IF %MENU_OPTION%==4 GOTO OPTION4
IF %INPUT%==false GOTO DEFAULT

:OPTION1
set DBSIZE=small
echo "Selected %DBSIZE%"
timeout 2 > NUL
GOTO INITAPP

:OPTION2
set DBSIZE=medium
echo %DBSIZE%
timeout 2 > NUL
GOTO INITAPP

:OPTION3
set INPUT=true
set DBSIZE=big
echo %DBSIZE%
timeout 2 > NUL
GOTO INITAPP

:OPTION4
set INPUT=true
echo Bye
timeout 2 > NUL
exit /b

:DEFAULT
echo Option not available
timeout 2 > NUL
GOTO MENU_START

:INITAPP
IF EXIST ./db/demo.sql (
    echo "Backup Exists"
    docker compose up -d
) ELSE (
    echo "Backup No Exists"
    curl -o ./db/demo.zip https://edu.postgrespro.com/demo-%DBSIZE%-en.zip 
    powershell -command "Expand-Archive -Force ./db/demo.zip ./db/"

    cd ./db
    for %%f in (*.sql) do (
        if "%%~xf"==".sql" echo %%f
        powershell -command "Rename-Item -Path "%%f" -NewName "demo.sql""
    )
    IF EXIST demo.zip (
    del demo.zip
    )
    docker compose up -d
)

