-- =============================================
-- Script: tenants_script0003.sql
-- Description: Create tenants table
-- =============================================

CREATE TABLE IF NOT EXISTS tenants.tenants (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name            VARCHAR(255) NOT NULL,
    slug            VARCHAR(100) NOT NULL UNIQUE,
    email           VARCHAR(255) NOT NULL UNIQUE,
    phone           VARCHAR(20),
    logo_url        VARCHAR(500),
    status          tenants.tenant_status NOT NULL DEFAULT 'pending',
    settings        JSONB NOT NULL DEFAULT '{}',
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT tenants_slug_format CHECK (slug ~ '^[a-z0-9-]+$')
);

CREATE INDEX IF NOT EXISTS idx_tenants_slug
    ON tenants.tenants(slug);

CREATE INDEX IF NOT EXISTS idx_tenants_status
    ON tenants.tenants(status);
