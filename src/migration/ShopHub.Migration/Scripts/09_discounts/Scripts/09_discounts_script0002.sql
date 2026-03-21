-- =============================================
-- Script: discounts_script0002.sql
-- Description: Create discount_codes table
-- =============================================

CREATE TABLE IF NOT EXISTS discounts.discount_codes (
    id                  UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id           UUID NOT NULL REFERENCES tenants.tenants(id),
    code                VARCHAR(50) NOT NULL,
    type                discounts.discount_type NOT NULL,
    value               DECIMAL(12,2) NOT NULL,
    min_order_amount    DECIMAL(12,2),
    max_discount_amount DECIMAL(12,2),
    apply_to            discounts.discount_apply_to NOT NULL DEFAULT 'all',
    applicable_ids      UUID[] DEFAULT '{}',
    usage_limit         INT,
    usage_count         INT NOT NULL DEFAULT 0,
    per_user_limit      INT DEFAULT 1,
    is_active           BOOLEAN NOT NULL DEFAULT TRUE,
    starts_at           TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    ends_at             TIMESTAMPTZ,
    created_at          TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at          TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT discount_code_tenant_unique UNIQUE (tenant_id, code),
    CONSTRAINT discount_value_check CHECK (value > 0)
);

CREATE INDEX IF NOT EXISTS idx_discount_tenant
    ON discounts.discount_codes(tenant_id)
    WHERE is_active = TRUE;

CREATE INDEX IF NOT EXISTS idx_discount_code
    ON discounts.discount_codes(tenant_id, code);
