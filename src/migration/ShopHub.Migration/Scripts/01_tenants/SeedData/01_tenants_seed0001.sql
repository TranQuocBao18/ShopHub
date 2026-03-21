-- =============================================
-- Seed: tenants_seed0001.sql
-- Description: Seed default subscription plans
-- =============================================

INSERT INTO tenants.subscription_plans
    (id, name, price, billing_cycle, max_products, max_staff, max_storage_mb, features, is_active)
VALUES
    (
        gen_random_uuid(), 'Free', 0, 'monthly',
        10, 1, 100,
        '["basic_analytics","standard_support"]',
        TRUE
    ),
    (
        gen_random_uuid(), 'Pro', 299000, 'monthly',
        100, 5, 1024,
        '["advanced_analytics","priority_support","custom_domain","discount_codes"]',
        TRUE
    ),
    (
        gen_random_uuid(), 'Business', 799000, 'monthly',
        -1, -1, 10240,
        '["advanced_analytics","dedicated_support","custom_domain","discount_codes","api_access","multi_warehouse"]',
        TRUE
    )
ON CONFLICT DO NOTHING;
