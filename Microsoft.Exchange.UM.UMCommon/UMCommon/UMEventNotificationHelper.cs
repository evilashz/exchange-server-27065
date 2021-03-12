using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200013E RID: 318
	internal class UMEventNotificationHelper
	{
		// Token: 0x06000A17 RID: 2583 RVA: 0x000267C8 File Offset: 0x000249C8
		public static void PublishUMSuccessEventNotificationItem(Component exchangeComponent, string notificationEvent)
		{
			EventNotificationItem eventNotificationItem = new EventNotificationItem(exchangeComponent.Name, notificationEvent, string.Empty, string.Empty, ResultSeverityLevel.Informational);
			eventNotificationItem.Publish(false);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000267F4 File Offset: 0x000249F4
		public static void PublishUMFailureEventNotificationItem(Component exchangeComponent, string notificationEvent)
		{
			EventNotificationItem eventNotificationItem = new EventNotificationItem(exchangeComponent.Name, notificationEvent, string.Empty, ResultSeverityLevel.Error);
			eventNotificationItem.Publish(false);
		}
	}
}
