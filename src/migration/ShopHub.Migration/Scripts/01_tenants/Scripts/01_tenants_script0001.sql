-- =============================================
-- Script: tenants_script0001.sql
-- Description: Create tenants schema and ENUMs
-- =============================================

CREATE SCHEMA IF NOT EXISTS tenants;

CREATE TYPE tenants.tenant_status AS ENUM (
    'active',
    'inactive',
    'suspended',
    'pending'
);

CREATE TYPE tenants.subscription_status AS ENUM (
    'active',
    'expired',
    'cancelled',
    'trial'
);

CREATE TYPE tenants.banner_position AS ENUM (
    'hero',
    'sidebar',
    'popup',
    'category'
);
