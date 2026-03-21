-- =============================================
-- Script: payments_script0002.sql
-- Description: Create payments table
-- =============================================

CREATE TABLE IF NOT EXISTS payments.payments (
    id                  UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id           UUID NOT NULL REFERENCES tenants.tenants(id),
    order_id            UUID NOT NULL REFERENCES orders.orders(id),
    method              payments.payment_method NOT NULL,
    status              payments.payment_status NOT NULL DEFAULT 'pending',
    amount              DECIMAL(12,2) NOT NULL,
    currency            VARCHAR(10) NOT NULL DEFAULT 'VND',
    refunded_amount     DECIMAL(12,2) NOT NULL DEFAULT 0,
    gateway_txn_id      VARCHAR(255),
    gateway_response    JSONB,
    paid_at             TIMESTAMPTZ,
    created_at          TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at          TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_payments_order
    ON payments.payments(order_id);

CREATE INDEX IF NOT EXISTS idx_payments_tenant
    ON payments.payments(tenant_id, created_at DESC);

CREATE INDEX IF NOT EXISTS idx_payments_gateway
    ON payments.payments(gateway_txn_id);

CREATE INDEX IF NOT EXISTS idx_payments_status
    ON payments.payments(tenant_id, status);
