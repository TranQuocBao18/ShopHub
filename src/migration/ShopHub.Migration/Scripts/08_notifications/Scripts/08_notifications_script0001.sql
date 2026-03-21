-- =============================================
-- Script: notifications_script0001.sql
-- Description: Create notifications schema and ENUMs
-- =============================================

CREATE SCHEMA IF NOT EXISTS notifications;

CREATE TYPE notifications.notification_type AS ENUM (
    'order_placed',
    'order_confirmed',
    'order_shipping',
    'order_delivered',
    'order_cancelled',
    'low_stock',
    'new_review',
    'payment_success',
    'payment_failed',
    'subscription_expiring',
    'system'
);
