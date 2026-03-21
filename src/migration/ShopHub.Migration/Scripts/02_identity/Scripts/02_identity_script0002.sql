-- =============================================
-- Script: identity_script0002.sql
-- Description: Create users table
-- =============================================

CREATE TABLE IF NOT EXISTS identity.users (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id       UUID REFERENCES tenants.tenants(id),
    email           VARCHAR(255) NOT NULL,
    password_hash   VARCHAR(500) NOT NULL,
    full_name       VARCHAR(255) NOT NULL,
    phone           VARCHAR(20),
    avatar_url      VARCHAR(500),
    is_active       BOOLEAN NOT NULL DEFAULT TRUE,
    is_deleted      BOOLEAN NOT NULL DEFAULT FALSE,
    email_verified  BOOLEAN NOT NULL DEFAULT FALSE,
    last_login_at   TIMESTAMPTZ,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    created_by      UUID,
    CONSTRAINT users_email_tenant_unique UNIQUE (tenant_id, email)
);

CREATE INDEX IF NOT EXISTS idx_users_tenant
    ON identity.users(tenant_id)
    WHERE is_deleted = FALSE;

CREATE INDEX IF NOT EXISTS idx_users_email
    ON identity.users(email)
    WHERE is_deleted = FALSE;
