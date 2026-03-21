-- =============================================
-- Script: catalog_script0001.sql
-- Description: Create catalog schema and ENUMs
-- =============================================

CREATE SCHEMA IF NOT EXISTS catalog;

CREATE TYPE catalog.product_status AS ENUM (
    'draft',
    'active',
    'inactive',
    'out_of_stock'
);
