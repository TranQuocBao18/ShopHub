-- =============================================
-- Script: inventory_script0001.sql
-- Description: Create inventory schema and ENUMs
-- =============================================

CREATE SCHEMA IF NOT EXISTS inventory;

CREATE TYPE inventory.inventory_action AS ENUM (
    'restock',
    'sale',
    'return',
    'adjustment',
    'reserved',
    'released'
);
