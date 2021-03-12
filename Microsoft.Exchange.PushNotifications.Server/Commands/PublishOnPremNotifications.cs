using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PushNotifications.Publishers;

namespace Microsoft.Exchange.PushNotifications.Server.Commands
{
	// Token: 0x02000013 RID: 19
	internal class PublishOnPremNotifications : PublishNotificationsBase<MailboxNotificationBatch>
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00002F90 File Offset: 0x00001190
		public PublishOnPremNotifications(MailboxNotificationBatch notifications, PushNotificationPublisherManager publisherManager, AsyncCallback asyncCallback, object asyncState) : base(notifications, publisherManager, asyncCallback, asyncState)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002F9D File Offset: 0x0000119D
		protected override void Publish()
		{
			base.PublisherManager.Publish(new ProxyNotification(PushNotificationCannedApp.OnPremProxy.Name, null, base.RequestInstance));
		}
	}
}
