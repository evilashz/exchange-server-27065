using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200000D RID: 13
	internal abstract class PushNotificationFactory<T> : IMonitoringMailboxNotificationRecipientFactory, IPushNotificationMapping<T> where T : PushNotification
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002B17 File Offset: 0x00000D17
		public PushNotificationFactory()
		{
			this.InputType = typeof(MailboxNotificationFragment);
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002B2F File Offset: 0x00000D2F
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002B37 File Offset: 0x00000D37
		public Type InputType { get; private set; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002B40 File Offset: 0x00000D40
		public bool TryMap(Notification notification, PushNotificationPublishingContext context, out T pushNotification)
		{
			MailboxNotificationFragment mailboxNotificationFragment = notification as MailboxNotificationFragment;
			if (mailboxNotificationFragment == null)
			{
				throw new InvalidOperationException("Notification type not supported: " + notification.ToFullString());
			}
			return this.TryCreate(mailboxNotificationFragment.Payload, mailboxNotificationFragment.Recipient, context, out pushNotification);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B81 File Offset: 0x00000D81
		public virtual bool TryCreate(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out T notification)
		{
			if (payload.IsMonitoring)
			{
				return this.TryCreateMonitoringNotification(payload, recipient, context, out notification);
			}
			return this.TryCreateUnseenEmailNotification(payload, recipient, context, out notification);
		}

		// Token: 0x06000055 RID: 85
		public abstract MailboxNotificationRecipient CreateMonitoringRecipient(string appId, int recipientId);

		// Token: 0x06000056 RID: 86
		public abstract MailboxNotificationRecipient CreateMonitoringRecipient(string appId, string recipientId);

		// Token: 0x06000057 RID: 87
		protected abstract bool TryCreateUnseenEmailNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out T notification);

		// Token: 0x06000058 RID: 88
		protected abstract bool TryCreateMonitoringNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out T notification);

		// Token: 0x06000059 RID: 89 RVA: 0x00002BA4 File Offset: 0x00000DA4
		protected string GetBackgroundSyncTypeString(BackgroundSyncType backgroundSyncType)
		{
			if (backgroundSyncType == BackgroundSyncType.Email)
			{
				return "e";
			}
			return null;
		}

		// Token: 0x0400001C RID: 28
		internal const string EmailBackgroundSyncType = "e";
	}
}
