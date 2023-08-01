#!/bin/bash
set -e

# Function to restore the database
restore_database() {
    echo "Restoring database..."
    psql -f /tmp/dump/demo.sql -U "$POSTGRES_USER"
    echo "Database restored successfully!"
}

# Check if the backup file exists and restore if needed
if [ -f /tmp/dump/demo.sql ]; then
    restore_database
fi
