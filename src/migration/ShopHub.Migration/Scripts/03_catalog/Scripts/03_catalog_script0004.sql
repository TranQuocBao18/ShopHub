-- =============================================
-- Script: catalog_script0004.sql
-- Description: Create product_variants table
-- =============================================

CREATE TABLE IF NOT EXISTS catalog.product_variants (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id       UUID NOT NULL REFERENCES tenants.tenants(id),
    product_id      UUID NOT NULL REFERENCES catalog.products(id) ON DELETE CASCADE,
    sku             VARCHAR(100) NOT NULL,
    name            VARCHAR(255) NOT NULL,
    options         JSONB NOT NULL DEFAULT '{}',
    price           DECIMAL(12,2) NOT NULL,
    compare_price   DECIMAL(12,2),
    weight          DECIMAL(8,2),
    is_active       BOOLEAN NOT NULL DEFAULT TRUE,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT variants_sku_tenant_unique UNIQUE (tenant_id, sku)
);

CREATE INDEX IF NOT EXISTS idx_variants_product
    ON catalog.product_variants(product_id);

CREATE INDEX IF NOT EXISTS idx_variants_tenant
    ON catalog.product_variants(tenant_id);
