-- =============================================
-- Script: 02_identity_script0010.sql
-- Description: Create user_logins table for ASP.NET Core Identity
-- =============================================

CREATE TABLE IF NOT EXISTS identity.user_logins (
    login_provider        VARCHAR(128) NOT NULL,
    provider_key          VARCHAR(128) NOT NULL,
    provider_display_name TEXT,
    user_id               UUID NOT NULL REFERENCES identity.users(id) ON DELETE CASCADE,
    PRIMARY KEY (login_provider, provider_key)
);

CREATE INDEX IF NOT EXISTS idx_user_logins_user
    ON identity.user_logins(user_id);
