using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200010C RID: 268
	internal static class ProxyCounters
	{
		// Token: 0x060008AE RID: 2222 RVA: 0x0001A910 File Offset: 0x00018B10
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ProxyCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in ProxyCounters.AllCounters)
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

		// Token: 0x040004E7 RID: 1255
		public const string CategoryName = "MSExchange Push Notifications Proxy";

		// Token: 0x040004E8 RID: 1256
		public static readonly ExPerformanceCounter AveragePublishingTime = new ExPerformanceCounter("MSExchange Push Notifications Proxy", "Publishing - Average Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004E9 RID: 1257
		public static readonly ExPerformanceCounter AveragePublishingTimeBase = new ExPerformanceCounter("MSExchange Push Notifications Proxy", "Publishing - Average Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004EA RID: 1258
		public static readonly ExPerformanceCounter AverageNotificationBatchSize = new ExPerformanceCounter("MSExchange Push Notifications Proxy", "Average Notification Batch Size - Average Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004EB RID: 1259
		public static readonly ExPerformanceCounter AverageNotificationBatchSizeBase = new ExPerformanceCounter("MSExchange Push Notifications Proxy", "Average Notification Batch Size - Count Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004EC RID: 1260
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			ProxyCounters.AveragePublishingTime,
			ProxyCounters.AveragePublishingTimeBase,
			ProxyCounters.AverageNotificationBatchSize,
			ProxyCounters.AverageNotificationBatchSizeBase
		};
	}
}
