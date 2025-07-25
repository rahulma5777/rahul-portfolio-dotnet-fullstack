-- Run this script to initialise the database: schema, seed data and stored procedures

-- Note: This script uses SQLCMD :r directive to include files.  If
-- executing in SSMS, open each file manually or adjust accordingly.

:r schema.sql
:r seed.sql
:r stored_procedures.sql