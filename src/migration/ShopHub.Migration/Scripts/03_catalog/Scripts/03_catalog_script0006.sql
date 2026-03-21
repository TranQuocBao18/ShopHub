-- =============================================
-- Script: catalog_script0006.sql
-- Description: Create reviews and review_images tables
-- =============================================

CREATE TABLE IF NOT EXISTS catalog.reviews (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id       UUID NOT NULL REFERENCES tenants.tenants(id),
    product_id      UUID NOT NULL REFERENCES catalog.products(id),
    order_item_id   UUID,
    user_id         UUID NOT NULL REFERENCES identity.users(id),
    rating          SMALLINT NOT NULL,
    title           VARCHAR(255),
    comment         TEXT,
    is_verified     BOOLEAN NOT NULL DEFAULT FALSE,
    is_approved     BOOLEAN NOT NULL DEFAULT FALSE,
    helpful_count   INT NOT NULL DEFAULT 0,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT reviews_rating_check CHECK (rating BETWEEN 1 AND 5),
    CONSTRAINT reviews_one_per_order_item UNIQUE (order_item_id)
);

CREATE INDEX IF NOT EXISTS idx_reviews_product
    ON catalog.reviews(product_id)
    WHERE is_approved = TRUE;

CREATE INDEX IF NOT EXISTS idx_reviews_user
    ON catalog.reviews(tenant_id, user_id);

CREATE TABLE IF NOT EXISTS catalog.review_images (
    id          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    review_id   UUID NOT NULL REFERENCES catalog.reviews(id) ON DELETE CASCADE,
    url         VARCHAR(500) NOT NULL,
    sort_order  INT NOT NULL DEFAULT 0,
    created_at  TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_review_images_review
    ON catalog.review_images(review_id);
