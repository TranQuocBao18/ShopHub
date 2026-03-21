-- =============================================
-- Seed: identity_seed0001.sql
-- Description: Seed default roles
-- =============================================

INSERT INTO identity.roles (id, name, description, created_at)
VALUES
    (gen_random_uuid(), 'SuperAdmin',  'Platform owner - full system access',  NOW()),
    (gen_random_uuid(), 'TenantAdmin', 'Shop owner - full shop access',        NOW()),
    (gen_random_uuid(), 'Staff',       'Shop staff - limited access',          NOW()),
    (gen_random_uuid(), 'Customer',    'Customer - shopping access only',      NOW())
ON CONFLICT (name) DO NOTHING;
