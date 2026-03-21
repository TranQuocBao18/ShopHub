-- =============================================
-- Script: catalog_script0002.sql
-- Description: Create categories table
-- =============================================

CREATE TABLE IF NOT EXISTS catalog.categories (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id       UUID NOT NULL REFERENCES tenants.tenants(id),
    parent_id       UUID REFERENCES catalog.categories(id),
    name            VARCHAR(255) NOT NULL,
    slug            VARCHAR(255) NOT NULL,
    description     TEXT,
    image_url       VARCHAR(500),
    sort_order      INT NOT NULL DEFAULT 0,
    is_active       BOOLEAN NOT NULL DEFAULT TRUE,
    is_deleted      BOOLEAN NOT NULL DEFAULT FALSE,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT categories_slug_tenant_unique UNIQUE (tenant_id, slug)
);

CREATE INDEX IF NOT EXISTS idx_categories_tenant
    ON catalog.categories(tenant_id)
    WHERE is_deleted = FALSE;

CREATE INDEX IF NOT EXISTS idx_categories_parent
    ON catalog.categories(parent_id);
