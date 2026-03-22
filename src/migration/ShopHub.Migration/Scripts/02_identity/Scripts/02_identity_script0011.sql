-- =============================================
-- Script: 02_identity_script0011.sql
-- Description: Create user_tokens table for ASP.NET Core Identity
-- =============================================

CREATE TABLE IF NOT EXISTS identity.user_tokens (
    user_id        UUID         NOT NULL REFERENCES identity.users(id) ON DELETE CASCADE,
    login_provider VARCHAR(128) NOT NULL,
    name           VARCHAR(128) NOT NULL,
    value          TEXT,
    PRIMARY KEY (user_id, login_provider, name)
);
