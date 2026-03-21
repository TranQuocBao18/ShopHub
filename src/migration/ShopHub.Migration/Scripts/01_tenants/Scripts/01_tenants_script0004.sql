-- =============================================
-- Script: tenants_script0004.sql
-- Description: Create tenant_subscriptions table
-- =============================================

CREATE TABLE IF NOT EXISTS tenants.tenant_subscriptions (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id       UUID NOT NULL REFERENCES tenants.tenants(id),
    plan_id         UUID NOT NULL REFERENCES tenants.subscription_plans(id),
    status          tenants.subscription_status NOT NULL DEFAULT 'trial',
    started_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    expires_at      TIMESTAMPTZ NOT NULL,
    cancelled_at    TIMESTAMPTZ,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_tenant_subs_tenant
    ON tenants.tenant_subscriptions(tenant_id);

CREATE INDEX IF NOT EXISTS idx_tenant_subs_expires
    ON tenants.tenant_subscriptions(expires_at);
