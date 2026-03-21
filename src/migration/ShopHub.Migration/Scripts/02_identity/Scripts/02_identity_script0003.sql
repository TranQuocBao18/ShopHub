-- =============================================
-- Script: identity_script0003.sql
-- Description: Create roles table
-- =============================================

CREATE TABLE IF NOT EXISTS identity.roles (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name            VARCHAR(50) NOT NULL UNIQUE,
    description     VARCHAR(255),
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW()
);
