-- =============================================
-- Script: 02_identity_script0009.sql
-- Description: Create role_claims table for ASP.NET Core Identity
-- =============================================

CREATE TABLE IF NOT EXISTS identity.role_claims (
    id          SERIAL PRIMARY KEY,
    role_id     UUID NOT NULL REFERENCES identity.roles(id) ON DELETE CASCADE,
    claim_type  TEXT,
    claim_value TEXT
);

CREATE INDEX IF NOT EXISTS idx_role_claims_role
    ON identity.role_claims(role_id);
