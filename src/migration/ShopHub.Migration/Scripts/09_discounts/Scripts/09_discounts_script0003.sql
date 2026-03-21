-- =============================================
-- Script: discounts_script0003.sql
-- Description: Create discount_usages table
-- =============================================

CREATE TABLE IF NOT EXISTS discounts.discount_usages (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    discount_id     UUID NOT NULL REFERENCES discounts.discount_codes(id),
    order_id        UUID NOT NULL REFERENCES orders.orders(id),
    user_id         UUID REFERENCES identity.users(id),
    discount_amount DECIMAL(12,2) NOT NULL,
    used_at         TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_discount_usage_code
    ON discounts.discount_usages(discount_id);

CREATE INDEX IF NOT EXISTS idx_discount_usage_user
    ON discounts.discount_usages(user_id, discount_id);

CREATE INDEX IF NOT EXISTS idx_discount_usage_order
    ON discounts.discount_usages(order_id);
