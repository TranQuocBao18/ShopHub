-- =============================================
-- Script: catalog_script0005.sql
-- Description: Create product_images table
-- =============================================

CREATE TABLE IF NOT EXISTS catalog.product_images (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id       UUID NOT NULL REFERENCES tenants.tenants(id),
    product_id      UUID NOT NULL REFERENCES catalog.products(id) ON DELETE CASCADE,
    variant_id      UUID REFERENCES catalog.product_variants(id) ON DELETE SET NULL,
    url             VARCHAR(500) NOT NULL,
    alt_text        VARCHAR(255),
    sort_order      INT NOT NULL DEFAULT 0,
    is_primary      BOOLEAN NOT NULL DEFAULT FALSE,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_images_product
    ON catalog.product_images(product_id);

CREATE INDEX IF NOT EXISTS idx_images_variant
    ON catalog.product_images(variant_id);
