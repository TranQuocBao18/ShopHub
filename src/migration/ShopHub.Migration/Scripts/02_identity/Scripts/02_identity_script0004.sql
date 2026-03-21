-- =============================================
-- Script: identity_script0004.sql
-- Description: Create user_roles table
-- =============================================

CREATE TABLE IF NOT EXISTS identity.user_roles (
    user_id         UUID NOT NULL REFERENCES identity.users(id) ON DELETE CASCADE,
    role_id         UUID NOT NULL REFERENCES identity.roles(id) ON DELETE CASCADE,
    assigned_at     TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    assigned_by     UUID REFERENCES identity.users(id),
    PRIMARY KEY (user_id, role_id)
);

CREATE INDEX IF NOT EXISTS idx_user_roles_user
    ON identity.user_roles(user_id);

CREATE INDEX IF NOT EXISTS idx_user_roles_role
    ON identity.user_roles(role_id);
