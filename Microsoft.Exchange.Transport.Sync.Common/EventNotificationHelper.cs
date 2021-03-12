using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000068 RID: 104
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EventNotificationHelper
	{
		// Token: 0x06000287 RID: 647 RVA: 0x00007490 File Offset: 0x00005690
		public static void PublishTransportEventNotificationItem(string notificationEvent)
		{
			EventNotificationHelper.PublishEventNotificationItem(notificationEvent, ExchangeComponent.Transport.Name, null);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000074A3 File Offset: 0x000056A3
		public static void PublishTransportEventNotificationItem(string notificationEvent, Exception exception)
		{
			EventNotificationHelper.PublishEventNotificationItem(notificationEvent, ExchangeComponent.Transport.Name, exception);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000074B6 File Offset: 0x000056B6
		public static void PublishTransportSyncEventNotificationItem(string notificationEvent)
		{
			EventNotificationHelper.PublishEventNotificationItem(notificationEvent, ExchangeComponent.MailboxMigration.Name, null);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000074C9 File Offset: 0x000056C9
		public static void PublishTransportSyncEventNotificationItem(string notificationEvent, Exception exception)
		{
			EventNotificationHelper.PublishEventNotificationItem(notificationEvent, ExchangeComponent.MailboxMigration.Name, exception);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000074DC File Offset: 0x000056DC
		private static void PublishEventNotificationItem(string notificationEvent, string componentName, Exception exception)
		{
			EventNotificationItem eventNotificationItem = new EventNotificationItem(componentName, notificationEvent, string.Empty, ResultSeverityLevel.Error);
			if (exception != null)
			{
				eventNotificationItem.AddCustomProperty("Exception", exception.ToString());
			}
			eventNotificationItem.Publish(false);
		}
	}
}
