-- =============================================
-- Script: catalog_script0003.sql
-- Description: Create products table
-- =============================================

CREATE TABLE IF NOT EXISTS catalog.products (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id       UUID NOT NULL REFERENCES tenants.tenants(id),
    category_id     UUID REFERENCES catalog.categories(id),
    name            VARCHAR(500) NOT NULL,
    slug            VARCHAR(500) NOT NULL,
    description     TEXT,
    short_desc      VARCHAR(1000),
    base_price      DECIMAL(12,2) NOT NULL DEFAULT 0,
    compare_price   DECIMAL(12,2),
    sku             VARCHAR(100),
    status          catalog.product_status NOT NULL DEFAULT 'draft',
    is_featured     BOOLEAN NOT NULL DEFAULT FALSE,
    tags            TEXT[] DEFAULT '{}',
    meta_title      VARCHAR(255),
    meta_desc       VARCHAR(500),
    attributes      JSONB NOT NULL DEFAULT '{}',
    is_deleted      BOOLEAN NOT NULL DEFAULT FALSE,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    created_by      UUID REFERENCES identity.users(id),
    updated_by      UUID REFERENCES identity.users(id),
    CONSTRAINT products_slug_tenant_unique UNIQUE (tenant_id, slug)
);

CREATE INDEX IF NOT EXISTS idx_products_tenant
    ON catalog.products(tenant_id)
    WHERE is_deleted = FALSE;

CREATE INDEX IF NOT EXISTS idx_products_category
    ON catalog.products(category_id);

CREATE INDEX IF NOT EXISTS idx_products_status
    ON catalog.products(tenant_id, status)
    WHERE is_deleted = FALSE;

CREATE INDEX IF NOT EXISTS idx_products_featured
    ON catalog.products(tenant_id, is_featured)
    WHERE is_deleted = FALSE AND is_featured = TRUE;

CREATE INDEX IF NOT EXISTS idx_products_tags
    ON catalog.products USING GIN(tags);

CREATE INDEX IF NOT EXISTS idx_products_attributes
    ON catalog.products USING GIN(attributes);

CREATE INDEX IF NOT EXISTS idx_products_search
    ON catalog.products USING GIN(
        to_tsvector('simple', name || ' ' || COALESCE(short_desc, ''))
    );
