using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200010A RID: 266
	internal static class PublisherManagerCounters
	{
		// Token: 0x060008AA RID: 2218 RVA: 0x0001A518 File Offset: 0x00018718
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PublisherManagerCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in PublisherManagerCounters.AllCounters)
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

		// Token: 0x040004D2 RID: 1234
		public const string CategoryName = "MSExchange Push Notifications Publisher Manager";

		// Token: 0x040004D3 RID: 1235
		public static readonly ExPerformanceCounter TotalPushNotificationRequests = new ExPerformanceCounter("MSExchange Push Notifications Publisher Manager", "Total PushNotification Requests - Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004D4 RID: 1236
		public static readonly ExPerformanceCounter TotalInvalidPushNotifications = new ExPerformanceCounter("MSExchange Push Notifications Publisher Manager", "Total Invalid PushNotifications - Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004D5 RID: 1237
		public static readonly ExPerformanceCounter TotalDiscardedPushNotifications = new ExPerformanceCounter("MSExchange Push Notifications Publisher Manager", "Total Discarded PushNotifications - Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004D6 RID: 1238
		public static readonly ExPerformanceCounter TotalNotificationRequests = new ExPerformanceCounter("MSExchange Push Notifications Publisher Manager", "Total Notification Requests - Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004D7 RID: 1239
		public static readonly ExPerformanceCounter TotalInvalidNotifications = new ExPerformanceCounter("MSExchange Push Notifications Publisher Manager", "Total Invalid Notifications - Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004D8 RID: 1240
		public static readonly ExPerformanceCounter TotalDiscardedNotifications = new ExPerformanceCounter("MSExchange Push Notifications Publisher Manager", "Total Discarded Notifications - Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004D9 RID: 1241
		public static readonly ExPerformanceCounter TotalMulticastNotificationRequests = new ExPerformanceCounter("MSExchange Push Notifications Publisher Manager", "Total Multicast Notification Requests - Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004DA RID: 1242
		public static readonly ExPerformanceCounter TotalInvalidMulticastNotifications = new ExPerformanceCounter("MSExchange Push Notifications Publisher Manager", "Total Invalid Multicast Notifications - Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004DB RID: 1243
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			PublisherManagerCounters.TotalPushNotificationRequests,
			PublisherManagerCounters.TotalInvalidPushNotifications,
			PublisherManagerCounters.TotalDiscardedPushNotifications,
			PublisherManagerCounters.TotalNotificationRequests,
			PublisherManagerCounters.TotalInvalidNotifications,
			PublisherManagerCounters.TotalDiscardedNotifications,
			PublisherManagerCounters.TotalMulticastNotificationRequests,
			PublisherManagerCounters.TotalInvalidMulticastNotifications
		};
	}
}
