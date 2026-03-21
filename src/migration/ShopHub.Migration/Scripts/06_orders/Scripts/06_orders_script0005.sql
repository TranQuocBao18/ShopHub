-- =============================================
-- Script: orders_script0005.sql
-- Description: Create order_status_histories table
-- =============================================

CREATE TABLE IF NOT EXISTS orders.order_status_histories (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    order_id        UUID NOT NULL REFERENCES orders.orders(id) ON DELETE CASCADE,
    old_status      orders.order_status,
    new_status      orders.order_status NOT NULL,
    note            TEXT,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    created_by      UUID REFERENCES identity.users(id)
);

CREATE INDEX IF NOT EXISTS idx_order_history_order
    ON orders.order_status_histories(order_id);
