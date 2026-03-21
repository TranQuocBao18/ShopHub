-- =============================================
-- Script: tenants_script0002.sql
-- Description: Create subscription_plans table
-- =============================================

CREATE TABLE IF NOT EXISTS tenants.subscription_plans (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name            VARCHAR(50) NOT NULL,
    price           DECIMAL(12,2) NOT NULL DEFAULT 0,
    billing_cycle   VARCHAR(20) NOT NULL,
    max_products    INT NOT NULL DEFAULT 10,
    max_staff       INT NOT NULL DEFAULT 1,
    max_storage_mb  INT NOT NULL DEFAULT 100,
    features        JSONB NOT NULL DEFAULT '[]',
    is_active       BOOLEAN NOT NULL DEFAULT TRUE,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW()
);
