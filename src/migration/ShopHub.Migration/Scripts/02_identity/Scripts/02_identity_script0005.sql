-- =============================================
-- Script: identity_script0005.sql
-- Description: Create refresh_tokens table
-- =============================================

CREATE TABLE IF NOT EXISTS identity.refresh_tokens (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id         UUID NOT NULL REFERENCES identity.users(id) ON DELETE CASCADE,
    token           VARCHAR(500) NOT NULL UNIQUE,
    expires_at      TIMESTAMPTZ NOT NULL,
    revoked_at      TIMESTAMPTZ,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    created_by_ip   VARCHAR(50),
    replaced_by     UUID REFERENCES identity.refresh_tokens(id)
);

CREATE INDEX IF NOT EXISTS idx_refresh_tokens_user
    ON identity.refresh_tokens(user_id);

CREATE INDEX IF NOT EXISTS idx_refresh_tokens_token
    ON identity.refresh_tokens(token);
