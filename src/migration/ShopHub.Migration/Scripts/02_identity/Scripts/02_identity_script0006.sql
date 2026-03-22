-- =============================================
-- Script: 02_identity_script0006.sql
-- Description: Add ASP.NET Core Identity columns to users table
-- =============================================

ALTER TABLE identity.users
    ADD COLUMN IF NOT EXISTS user_name              VARCHAR(255),
    ADD COLUMN IF NOT EXISTS normalized_user_name   VARCHAR(255),
    ADD COLUMN IF NOT EXISTS normalized_email       VARCHAR(255),
    ADD COLUMN IF NOT EXISTS security_stamp         TEXT,
    ADD COLUMN IF NOT EXISTS concurrency_stamp      TEXT,
    ADD COLUMN IF NOT EXISTS phone_number_confirmed BOOLEAN NOT NULL DEFAULT FALSE,
    ADD COLUMN IF NOT EXISTS two_factor_enabled     BOOLEAN NOT NULL DEFAULT FALSE,
    ADD COLUMN IF NOT EXISTS lockout_end            TIMESTAMPTZ,
    ADD COLUMN IF NOT EXISTS lockout_enabled        BOOLEAN NOT NULL DEFAULT TRUE,
    ADD COLUMN IF NOT EXISTS access_failed_count    INT NOT NULL DEFAULT 0;

-- Backfill required Identity columns for any existing rows
UPDATE identity.users
SET
    user_name            = email,
    normalized_user_name = UPPER(email),
    normalized_email     = UPPER(email),
    security_stamp       = gen_random_uuid()::TEXT,
    concurrency_stamp    = gen_random_uuid()::TEXT
WHERE normalized_email IS NULL;

CREATE UNIQUE INDEX IF NOT EXISTS idx_users_normalized_user_name
    ON identity.users(normalized_user_name)
    WHERE is_deleted = FALSE;

CREATE INDEX IF NOT EXISTS idx_users_normalized_email
    ON identity.users(normalized_email)
    WHERE is_deleted = FALSE;
