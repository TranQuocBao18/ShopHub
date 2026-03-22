-- =============================================
-- Script: 02_identity_script0003.sql
-- Description: Create roles table (ASP.NET Core Identity)
-- =============================================

CREATE TABLE IF NOT EXISTS identity.roles (
    id                  UUID        NOT NULL PRIMARY KEY,
    name                VARCHAR(256),
    normalized_name     VARCHAR(256),
    concurrency_stamp   TEXT,
    -- Custom columns
    description         VARCHAR(500),
    created_at          TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE UNIQUE INDEX IF NOT EXISTS idx_roles_normalized_name
    ON identity.roles(normalized_name)
    WHERE normalized_name IS NOT NULL;