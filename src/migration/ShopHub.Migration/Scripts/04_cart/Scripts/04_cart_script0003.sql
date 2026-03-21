-- =============================================
-- Script: cart_script0003.sql
-- Description: Create cart_items table
-- =============================================

CREATE TABLE IF NOT EXISTS cart.cart_items (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    cart_id         UUID NOT NULL REFERENCES cart.carts(id) ON DELETE CASCADE,
    product_id      UUID NOT NULL REFERENCES catalog.products(id),
    variant_id      UUID REFERENCES catalog.product_variants(id),
    quantity        INT NOT NULL DEFAULT 1,
    unit_price      DECIMAL(12,2) NOT NULL,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT cart_items_quantity_check CHECK (quantity > 0)
);

CREATE INDEX IF NOT EXISTS idx_cart_items_cart
    ON cart.cart_items(cart_id);

CREATE INDEX IF NOT EXISTS idx_cart_items_product
    ON cart.cart_items(product_id);
