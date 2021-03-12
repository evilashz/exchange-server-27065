using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200000E RID: 14
	internal interface IPushNotificationOptics
	{
		// Token: 0x0600005A RID: 90
		void ReportReceived(MulticastNotification notification, PushNotificationPublishingContext context);

		// Token: 0x0600005B RID: 91
		void ReportDiscardedByValidation(MulticastNotification notification);

		// Token: 0x0600005C RID: 92
		void ReportReceived(Notification notification, PushNotificationPublishingContext context);

		// Token: 0x0600005D RID: 93
		void ReportDiscardedByValidation(Notification notification);

		// Token: 0x0600005E RID: 94
		void ReportDiscardedByUnsuitablePublisher(Notification notification);

		// Token: 0x0600005F RID: 95
		void ReportDiscardedByUnknownPublisher(Notification notification);

		// Token: 0x06000060 RID: 96
		void ReportDiscardedByUnknownMapping(Notification notification);

		// Token: 0x06000061 RID: 97
		void ReportDiscardedByFailedMapping(Notification notification);

		// Token: 0x06000062 RID: 98
		void ReportDiscardedByDisposedPublisher(Notification notification);

		// Token: 0x06000063 RID: 99
		void ReportProcessed(Notification notification, PushNotification pushNotification, PushNotificationPublishingContext context);

		// Token: 0x06000064 RID: 100
		void ReportDiscardedByMissmatchingType(PushNotification notification);

		// Token: 0x06000065 RID: 101
		void ReportDiscardedByValidation(PushNotification notification, Exception ex);

		// Token: 0x06000066 RID: 102
		void ReportDiscardedByUnsuitablePublisher(PushNotification notification);

		// Token: 0x06000067 RID: 103
		void ReportDiscardedByUnknownPublisher(PushNotification notification);

		// Token: 0x06000068 RID: 104
		void ReportDiscardedByThrottling(PushNotification notification, OverBudgetException obe);

		// Token: 0x06000069 RID: 105
		void ReportDiscardedByDisposedPublisher(PushNotification notification);
	}
}
