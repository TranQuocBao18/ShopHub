-- =============================================
-- Script: orders_script0003.sql
-- Description: Create orders table
-- =============================================

CREATE TABLE IF NOT EXISTS orders.orders (
    id                  UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id           UUID NOT NULL REFERENCES tenants.tenants(id),
    user_id             UUID REFERENCES identity.users(id),
    order_code          VARCHAR(50) NOT NULL,
    status              orders.order_status NOT NULL DEFAULT 'pending',
    subtotal            DECIMAL(12,2) NOT NULL,
    discount_amount     DECIMAL(12,2) NOT NULL DEFAULT 0,
    shipping_fee        DECIMAL(12,2) NOT NULL DEFAULT 0,
    tax_amount          DECIMAL(12,2) NOT NULL DEFAULT 0,
    total_amount        DECIMAL(12,2) NOT NULL,
    discount_code_id    UUID,
    shipping_address    JSONB NOT NULL,
    shipping_method     VARCHAR(100),
    tracking_number     VARCHAR(255),
    estimated_delivery  DATE,
    customer_name       VARCHAR(255),
    customer_email      VARCHAR(255),
    customer_phone      VARCHAR(20),
    note                TEXT,
    cancelled_reason    TEXT,
    cancelled_at        TIMESTAMPTZ,
    created_at          TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at          TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT orders_code_tenant_unique UNIQUE (tenant_id, order_code)
);

CREATE INDEX IF NOT EXISTS idx_orders_tenant
    ON orders.orders(tenant_id, created_at DESC);

CREATE INDEX IF NOT EXISTS idx_orders_user
    ON orders.orders(user_id);

CREATE INDEX IF NOT EXISTS idx_orders_status
    ON orders.orders(tenant_id, status);

CREATE INDEX IF NOT EXISTS idx_orders_code
    ON orders.orders(order_code);
