-- =============================================
-- Script: tenants_script0006.sql
-- Description: Create banners table
-- =============================================

CREATE TABLE IF NOT EXISTS tenants.banners (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id       UUID NOT NULL REFERENCES tenants.tenants(id),
    title           VARCHAR(255),
    image_url       VARCHAR(500) NOT NULL,
    link_url        VARCHAR(500),
    position        tenants.banner_position NOT NULL DEFAULT 'hero',
    sort_order      INT NOT NULL DEFAULT 0,
    is_active       BOOLEAN NOT NULL DEFAULT TRUE,
    starts_at       TIMESTAMPTZ,
    ends_at         TIMESTAMPTZ,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_banners_tenant
    ON tenants.banners(tenant_id, position)
    WHERE is_active = TRUE;
