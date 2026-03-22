-- =============================================
-- Script: 02_identity_script0002.sql
-- Description: Create users table (ASP.NET Core Identity)
-- =============================================

CREATE TABLE IF NOT EXISTS identity.users (
    id                      UUID        NOT NULL PRIMARY KEY,
    user_name               VARCHAR(256),
    normalized_user_name    VARCHAR(256),
    email                   VARCHAR(256),
    normalized_email        VARCHAR(256),
    email_confirmed         BOOLEAN     NOT NULL DEFAULT FALSE,
    password_hash           TEXT,
    security_stamp          TEXT,
    concurrency_stamp       TEXT,
    phone_number            VARCHAR(20),
    phone_number_confirmed  BOOLEAN     NOT NULL DEFAULT FALSE,
    two_factor_enabled      BOOLEAN     NOT NULL DEFAULT FALSE,
    lockout_end             TIMESTAMPTZ,
    lockout_enabled         BOOLEAN     NOT NULL DEFAULT FALSE,
    access_failed_count     INTEGER     NOT NULL DEFAULT 0,
    -- Custom columns
    full_name               VARCHAR(255) NOT NULL DEFAULT '',
    status                  SMALLINT     NOT NULL DEFAULT 1,
    last_login_at           TIMESTAMPTZ,
    created_at              TIMESTAMPTZ  NOT NULL DEFAULT NOW()
);

CREATE UNIQUE INDEX IF NOT EXISTS idx_users_normalized_user_name
    ON identity.users(normalized_user_name)
    WHERE normalized_user_name IS NOT NULL;

CREATE UNIQUE INDEX IF NOT EXISTS idx_users_normalized_email
    ON identity.users(normalized_email)
    WHERE normalized_email IS NOT NULL;