using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000EB RID: 235
	internal class WnsNotificationFactory : PushNotificationFactory<WnsNotification>
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x000178FD File Offset: 0x00015AFD
		public override MailboxNotificationRecipient CreateMonitoringRecipient(string appId, int recipientId)
		{
			return new MailboxNotificationRecipient(appId, string.Format("http://127.0.0.1:0/send?id={0}", recipientId), DateTime.UtcNow, null);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001791B File Offset: 0x00015B1B
		public override MailboxNotificationRecipient CreateMonitoringRecipient(string appId, string recipientId)
		{
			throw new NotImplementedException("RecipientId is required as int for creating WNS MonitoringRecipient");
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00017928 File Offset: 0x00015B28
		protected override bool TryCreateUnseenEmailNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out WnsNotification notification)
		{
			notification = new WnsBadgeNotification(recipient.AppId, context.OrgId, recipient.DeviceId, payload.UnseenEmailCount.Value, null, new WnsCachePolicy?(WnsCachePolicy.Cache));
			return true;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001796D File Offset: 0x00015B6D
		protected override bool TryCreateMonitoringNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, PushNotificationPublishingContext context, out WnsNotification notification)
		{
			notification = new WnsMonitoringNotification(recipient.AppId, recipient.DeviceId);
			return true;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00017984 File Offset: 0x00015B84
		private bool TryCreateWnsTileNotification(MailboxNotificationPayload payload, MailboxNotificationRecipient recipient, OrganizationId tenantId, string text, out WnsNotification notification)
		{
			WnsText[] texts = new WnsText[]
			{
				new WnsText(text, null)
			};
			WnsTileBinding binding = new WnsTileBinding(WnsTile.SquareText04, null, null, null, null, null, texts, null);
			WnsTileBinding[] extraBindings = new WnsTileBinding[]
			{
				new WnsTileBinding(WnsTile.WideText04, null, null, null, null, null, texts, null)
			};
			notification = new WnsTileNotification(recipient.AppId, tenantId, recipient.DeviceId, new WnsTileVisual(binding, extraBindings, null, null, null, null), null, null, null);
			return true;
		}

		// Token: 0x04000426 RID: 1062
		public static readonly WnsNotificationFactory Default = new WnsNotificationFactory();
	}
}
