-- =============================================
-- Script: orders_script0004.sql
-- Description: Create order_items table
-- =============================================

CREATE TABLE IF NOT EXISTS orders.order_items (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    order_id        UUID NOT NULL REFERENCES orders.orders(id) ON DELETE CASCADE,
    product_id      UUID NOT NULL REFERENCES catalog.products(id),
    variant_id      UUID REFERENCES catalog.product_variants(id),
    product_name    VARCHAR(500) NOT NULL,
    variant_name    VARCHAR(255),
    sku             VARCHAR(100),
    unit_price      DECIMAL(12,2) NOT NULL,
    quantity        INT NOT NULL,
    total_price     DECIMAL(12,2) NOT NULL,
    image_url       VARCHAR(500),
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT order_items_quantity_check CHECK (quantity > 0)
);

CREATE INDEX IF NOT EXISTS idx_order_items_order
    ON orders.order_items(order_id);

CREATE INDEX IF NOT EXISTS idx_order_items_product
    ON orders.order_items(product_id);
