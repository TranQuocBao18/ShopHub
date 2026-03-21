-- =============================================
-- Script: inventory_script0003.sql
-- Description: Create inventory_logs table
-- =============================================

CREATE TABLE IF NOT EXISTS inventory.inventory_logs (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id       UUID NOT NULL REFERENCES tenants.tenants(id),
    inventory_id    UUID NOT NULL REFERENCES inventory.inventories(id),
    action          inventory.inventory_action NOT NULL,
    quantity_change INT NOT NULL,
    quantity_before INT NOT NULL,
    quantity_after  INT NOT NULL,
    reference_id    UUID,
    note            TEXT,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    created_by      UUID REFERENCES identity.users(id)
);

CREATE INDEX IF NOT EXISTS idx_inv_logs_inventory
    ON inventory.inventory_logs(inventory_id);

CREATE INDEX IF NOT EXISTS idx_inv_logs_tenant
    ON inventory.inventory_logs(tenant_id, created_at DESC);

CREATE INDEX IF NOT EXISTS idx_inv_logs_reference
    ON inventory.inventory_logs(reference_id);
