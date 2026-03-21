-- =============================================
-- Script: notifications_script0002.sql
-- Description: Create notifications table
-- =============================================

CREATE TABLE IF NOT EXISTS notifications.notifications (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    tenant_id       UUID REFERENCES tenants.tenants(id),
    user_id         UUID NOT NULL REFERENCES identity.users(id) ON DELETE CASCADE,
    type            notifications.notification_type NOT NULL,
    title           VARCHAR(255) NOT NULL,
    message         TEXT NOT NULL,
    data            JSONB NOT NULL DEFAULT '{}',
    is_read         BOOLEAN NOT NULL DEFAULT FALSE,
    read_at         TIMESTAMPTZ,
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_notif_user
    ON notifications.notifications(user_id, is_read, created_at DESC);

CREATE INDEX IF NOT EXISTS idx_notif_tenant
    ON notifications.notifications(tenant_id, created_at DESC);

CREATE INDEX IF NOT EXISTS idx_notif_unread
    ON notifications.notifications(user_id)
    WHERE is_read = FALSE;
