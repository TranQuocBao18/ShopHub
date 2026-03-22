-- =============================================
-- Script: 02_identity_script0004.sql
-- Description: Create ASP.NET Core Identity junction tables
-- =============================================

-- user_roles
CREATE TABLE IF NOT EXISTS identity.user_roles (
    user_id UUID NOT NULL REFERENCES identity.users(id) ON DELETE CASCADE,
    role_id UUID NOT NULL REFERENCES identity.roles(id) ON DELETE CASCADE,
    PRIMARY KEY (user_id, role_id)
);

-- user_claims
CREATE TABLE IF NOT EXISTS identity.user_claims (
    id          SERIAL  PRIMARY KEY,
    user_id     UUID    NOT NULL REFERENCES identity.users(id) ON DELETE CASCADE,
    claim_type  TEXT,
    claim_value TEXT
);

-- user_logins
CREATE TABLE IF NOT EXISTS identity.user_logins (
    login_provider          VARCHAR(128) NOT NULL,
    provider_key            VARCHAR(128) NOT NULL,
    provider_display_name   TEXT,
    user_id                 UUID NOT NULL REFERENCES identity.users(id) ON DELETE CASCADE,
    PRIMARY KEY (login_provider, provider_key)
);

-- user_tokens
CREATE TABLE IF NOT EXISTS identity.user_tokens (
    user_id         UUID         NOT NULL REFERENCES identity.users(id) ON DELETE CASCADE,
    login_provider  VARCHAR(128) NOT NULL,
    name            VARCHAR(128) NOT NULL,
    value           TEXT,
    PRIMARY KEY (user_id, login_provider, name)
);

-- role_claims
CREATE TABLE IF NOT EXISTS identity.role_claims (
    id          SERIAL PRIMARY KEY,
    role_id     UUID   NOT NULL REFERENCES identity.roles(id) ON DELETE CASCADE,
    claim_type  TEXT,
    claim_value TEXT
);