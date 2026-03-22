-- =============================================
-- Script: 02_identity_script0008.sql
-- Description: Create user_claims table for ASP.NET Core Identity
-- =============================================

CREATE TABLE IF NOT EXISTS identity.user_claims (
    id          SERIAL PRIMARY KEY,
    user_id     UUID NOT NULL REFERENCES identity.users(id) ON DELETE CASCADE,
    claim_type  TEXT,
    claim_value TEXT
);

CREATE INDEX IF NOT EXISTS idx_user_claims_user
    ON identity.user_claims(user_id);
