using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x02000213 RID: 531
	internal static class PushNotificationsAssistantPerfCounters
	{
		// Token: 0x0600144B RID: 5195 RVA: 0x00075078 File Offset: 0x00073278
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PushNotificationsAssistantPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in PushNotificationsAssistantPerfCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x04000C32 RID: 3122
		public const string CategoryName = "MSExchange Push Notifications Assistant";

		// Token: 0x04000C33 RID: 3123
		public static readonly ExPerformanceCounter TotalSubscriptionsExpiredCleanup = new ExPerformanceCounter("MSExchange Push Notifications Assistant", "Subscription - Expired cleanup Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C34 RID: 3124
		private static readonly ExPerformanceCounter TotalNewSubscriptionsCreatedPerSecond = new ExPerformanceCounter("MSExchange Push Notifications Assistant", "Subscription - Created Count/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C35 RID: 3125
		public static readonly ExPerformanceCounter TotalNewSubscriptionsCreated = new ExPerformanceCounter("MSExchange Push Notifications Assistant", "Subscription - Created Count", string.Empty, null, new ExPerformanceCounter[]
		{
			PushNotificationsAssistantPerfCounters.TotalNewSubscriptionsCreatedPerSecond
		});

		// Token: 0x04000C36 RID: 3126
		private static readonly ExPerformanceCounter TotalSubscriptionsUpdatedPerSecond = new ExPerformanceCounter("MSExchange Push Notifications Assistant", "Subscription - Updated Count/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C37 RID: 3127
		public static readonly ExPerformanceCounter TotalSubscriptionsUpdated = new ExPerformanceCounter("MSExchange Push Notifications Assistant", "Subscription - Updated Count", string.Empty, null, new ExPerformanceCounter[]
		{
			PushNotificationsAssistantPerfCounters.TotalSubscriptionsUpdatedPerSecond
		});

		// Token: 0x04000C38 RID: 3128
		public static readonly ExPerformanceCounter CurrentActiveUserSubscriptions = new ExPerformanceCounter("MSExchange Push Notifications Assistant", "Current Number of Active User Subscriptions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C39 RID: 3129
		public static readonly ExPerformanceCounter AveragePublishingRequestProcessing = new ExPerformanceCounter("MSExchange Push Notifications Assistant", "Publishing Request - Average processing Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C3A RID: 3130
		public static readonly ExPerformanceCounter AveragePublishingRequestProcessingBase = new ExPerformanceCounter("MSExchange Push Notifications Assistant", "Publishing Request - Average processing Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C3B RID: 3131
		private static readonly ExPerformanceCounter PublishingRequestErrorsPerSecond = new ExPerformanceCounter("MSExchange Push Notifications Assistant", "Publishing Request - Error Count/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C3C RID: 3132
		public static readonly ExPerformanceCounter PublishingRequestErrors = new ExPerformanceCounter("MSExchange Push Notifications Assistant", "Publishing Request - Error Count", string.Empty, null, new ExPerformanceCounter[]
		{
			PushNotificationsAssistantPerfCounters.PublishingRequestErrorsPerSecond
		});

		// Token: 0x04000C3D RID: 3133
		public static readonly ExPerformanceCounter NotificationsPerBatch = new ExPerformanceCounter("MSExchange Push Notifications Assistant", "Notifications Per Batch - Average Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C3E RID: 3134
		public static readonly ExPerformanceCounter NotificationsPerBatchBase = new ExPerformanceCounter("MSExchange Push Notifications Assistant", "Notifications Per Batch - Count Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C3F RID: 3135
		public static readonly ExPerformanceCounter DiscardedNotificationsPerBatch = new ExPerformanceCounter("MSExchange Push Notifications Assistant", "Discarded Notifications Per Batch - Average Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C40 RID: 3136
		public static readonly ExPerformanceCounter DiscardedNotificationsPerBatchBase = new ExPerformanceCounter("MSExchange Push Notifications Assistant", "Discarded Notifications Per Batch - Count Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000C41 RID: 3137
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			PushNotificationsAssistantPerfCounters.TotalSubscriptionsExpiredCleanup,
			PushNotificationsAssistantPerfCounters.TotalNewSubscriptionsCreated,
			PushNotificationsAssistantPerfCounters.TotalSubscriptionsUpdated,
			PushNotificationsAssistantPerfCounters.CurrentActiveUserSubscriptions,
			PushNotificationsAssistantPerfCounters.AveragePublishingRequestProcessing,
			PushNotificationsAssistantPerfCounters.AveragePublishingRequestProcessingBase,
			PushNotificationsAssistantPerfCounters.PublishingRequestErrors,
			PushNotificationsAssistantPerfCounters.NotificationsPerBatch,
			PushNotificationsAssistantPerfCounters.NotificationsPerBatchBase,
			PushNotificationsAssistantPerfCounters.DiscardedNotificationsPerBatch,
			PushNotificationsAssistantPerfCounters.DiscardedNotificationsPerBatchBase
		};
	}
}
