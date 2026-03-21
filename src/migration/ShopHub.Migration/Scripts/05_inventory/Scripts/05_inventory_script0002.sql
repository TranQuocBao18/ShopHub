-- =============================================
-- Script: inventory_script0002.sql
-- Description: Create inventories table
-- =============================================

CREATE TABLE IF NOT EXISTS inventory.inventories (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id       UUID NOT NULL REFERENCES tenants.tenants(id),
    product_id      UUID NOT NULL REFERENCES catalog.products(id),
    variant_id      UUID REFERENCES catalog.product_variants(id),
    quantity        INT NOT NULL DEFAULT 0,
    reserved        INT NOT NULL DEFAULT 0,
    low_stock_alert INT NOT NULL DEFAULT 5,
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT inventories_unique UNIQUE (tenant_id, product_id, variant_id),
    CONSTRAINT inventories_quantity_check CHECK (quantity >= 0),
    CONSTRAINT inventories_reserved_check CHECK (reserved >= 0)
);

CREATE INDEX IF NOT EXISTS idx_inventory_tenant
    ON inventory.inventories(tenant_id);

CREATE INDEX IF NOT EXISTS idx_inventory_product
    ON inventory.inventories(product_id);

CREATE INDEX IF NOT EXISTS idx_inventory_low_stock
    ON inventory.inventories(tenant_id, quantity)
    WHERE quantity <= low_stock_alert;
