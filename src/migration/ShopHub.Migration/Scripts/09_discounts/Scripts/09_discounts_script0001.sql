-- =============================================
-- Script: discounts_script0001.sql
-- Description: Create discounts schema and ENUMs
-- =============================================

CREATE SCHEMA IF NOT EXISTS discounts;

CREATE TYPE discounts.discount_type AS ENUM (
    'percentage',
    'fixed_amount',
    'free_shipping'
);

CREATE TYPE discounts.discount_apply_to AS ENUM (
    'all',
    'specific_products',
    'specific_categories'
);
