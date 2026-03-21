-- =============================================
-- Script: orders_script0001.sql
-- Description: Create orders schema and ENUMs
-- =============================================

CREATE SCHEMA IF NOT EXISTS orders;

CREATE TYPE orders.order_status AS ENUM (
    'pending',
    'confirmed',
    'processing',
    'shipping',
    'delivered',
    'completed',
    'cancelled',
    'refunded'
);
