-- =============================================
-- Script: orders_script0002.sql
-- Description: Create shipping_addresses table
-- =============================================

CREATE TABLE IF NOT EXISTS orders.shipping_addresses (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id       UUID NOT NULL REFERENCES tenants.tenants(id),
    user_id         UUID NOT NULL REFERENCES identity.users(id) ON DELETE CASCADE,
    full_name       VARCHAR(255) NOT NULL,
    phone           VARCHAR(20) NOT NULL,
    address_line1   VARCHAR(500) NOT NULL,
    address_line2   VARCHAR(500),
    ward            VARCHAR(100),
    district        VARCHAR(100) NOT NULL,
    province        VARCHAR(100) NOT NULL,
    is_default      BOOLEAN NOT NULL DEFAULT FALSE,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_shipping_addr_user
    ON orders.shipping_addresses(tenant_id, user_id);

CREATE INDEX IF NOT EXISTS idx_shipping_addr_default
    ON orders.shipping_addresses(user_id, is_default)
    WHERE is_default = TRUE;
