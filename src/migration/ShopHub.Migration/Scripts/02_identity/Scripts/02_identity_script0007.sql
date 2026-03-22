-- =============================================
-- Script: 02_identity_script0007.sql
-- Description: Add ASP.NET Core Identity columns to roles table
-- =============================================

ALTER TABLE identity.roles
    ADD COLUMN IF NOT EXISTS normalized_name  VARCHAR(50),
    ADD COLUMN IF NOT EXISTS concurrency_stamp TEXT;

-- Backfill required Identity columns for any existing rows
UPDATE identity.roles
SET
    normalized_name   = UPPER(name),
    concurrency_stamp = gen_random_uuid()::TEXT
WHERE normalized_name IS NULL;

CREATE UNIQUE INDEX IF NOT EXISTS idx_roles_normalized_name
    ON identity.roles(normalized_name);
