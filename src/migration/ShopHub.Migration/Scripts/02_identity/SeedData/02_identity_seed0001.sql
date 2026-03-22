-- =============================================
-- Seed: 02_identity_seed0001.sql
-- Description: Seed default roles
-- =============================================

INSERT INTO identity.roles (id, name, normalized_name, description, created_at)
VALUES
    (gen_random_uuid(), 'SuperAdmin',  'SUPERADMIN',  'Platform owner - full system access', NOW()),
    (gen_random_uuid(), 'TenantAdmin', 'TENANTADMIN', 'Shop owner - full shop access',       NOW()),
    (gen_random_uuid(), 'Staff',       'STAFF',       'Shop staff - limited access',         NOW()),
    (gen_random_uuid(), 'Customer',    'CUSTOMER',    'Customer - shopping access only',      NOW())
ON CONFLICT DO NOTHING;