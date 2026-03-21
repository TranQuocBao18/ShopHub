-- =============================================
-- Script: cart_script0002.sql
-- Description: Create carts table
-- Note: Redis is primary storage, DB is backup/fallback
-- =============================================

CREATE TABLE IF NOT EXISTS cart.carts (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id       UUID NOT NULL REFERENCES tenants.tenants(id),
    user_id         UUID REFERENCES identity.users(id),
    session_id      VARCHAR(255),
    expires_at      TIMESTAMPTZ NOT NULL DEFAULT NOW() + INTERVAL '7 days',
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT carts_owner_check CHECK (
        user_id IS NOT NULL OR session_id IS NOT NULL
    )
);

CREATE INDEX IF NOT EXISTS idx_carts_user
    ON cart.carts(tenant_id, user_id);

CREATE INDEX IF NOT EXISTS idx_carts_session
    ON cart.carts(session_id);

CREATE INDEX IF NOT EXISTS idx_carts_expires
    ON cart.carts(expires_at);
