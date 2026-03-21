-- =============================================
-- Script: tenants_script0005.sql
-- Description: Create store_settings table
-- =============================================

CREATE TABLE IF NOT EXISTS tenants.store_settings (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id       UUID NOT NULL UNIQUE REFERENCES tenants.tenants(id),
    store_name      VARCHAR(255) NOT NULL,
    store_desc      TEXT,
    logo_url        VARCHAR(500),
    favicon_url     VARCHAR(500),
    banner_url      VARCHAR(500),
    primary_color   VARCHAR(7) NOT NULL DEFAULT '#3B82F6',
    contact_email   VARCHAR(255),
    contact_phone   VARCHAR(20),
    address         TEXT,
    social_links    JSONB NOT NULL DEFAULT '{}',
    seo_settings    JSONB NOT NULL DEFAULT '{}',
    business_hours  JSONB NOT NULL DEFAULT '{}',
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_by      UUID
);

CREATE INDEX IF NOT EXISTS idx_store_settings_tenant
    ON tenants.store_settings(tenant_id);
