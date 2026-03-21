-- =============================================
-- Script: payments_script0001.sql
-- Description: Create payments schema and ENUMs
-- =============================================

CREATE SCHEMA IF NOT EXISTS payments;

CREATE TYPE payments.payment_status AS ENUM (
    'pending',
    'processing',
    'completed',
    'failed',
    'refunded',
    'partially_refunded'
);

CREATE TYPE payments.payment_method AS ENUM (
    'cod',
    'vnpay',
    'stripe',
    'bank_transfer'
);
