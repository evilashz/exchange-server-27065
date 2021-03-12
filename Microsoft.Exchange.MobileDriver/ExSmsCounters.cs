using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.TextMessaging
{
	// Token: 0x02000002 RID: 2
	internal static class ExSmsCounters
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ExSmsCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in ExSmsCounters.AllCounters)
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

		// Token: 0x04000001 RID: 1
		public const string CategoryName = "MSExchange Text Messaging";

		// Token: 0x04000002 RID: 2
		private static readonly ExPerformanceCounter RateOfTextMessagesSent = new ExPerformanceCounter("MSExchange Text Messaging", "Text Messages Sent/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000003 RID: 3
		public static readonly ExPerformanceCounter NumberOfTextMessagesSent = new ExPerformanceCounter("MSExchange Text Messaging", "Total Text Messages Sent", string.Empty, null, new ExPerformanceCounter[]
		{
			ExSmsCounters.RateOfTextMessagesSent
		});

		// Token: 0x04000004 RID: 4
		private static readonly ExPerformanceCounter RateOfTextMessagesSentViaEas = new ExPerformanceCounter("MSExchange Text Messaging", "Text Messages Sent via Exchange ActiveSync/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000005 RID: 5
		public static readonly ExPerformanceCounter NumberOfTextMessagesSentViaEas = new ExPerformanceCounter("MSExchange Text Messaging", "Total Text Messages Sent via Exchange ActiveSync", string.Empty, null, new ExPerformanceCounter[]
		{
			ExSmsCounters.RateOfTextMessagesSentViaEas
		});

		// Token: 0x04000006 RID: 6
		private static readonly ExPerformanceCounter RateOfTextMessagesSentViaSmtp = new ExPerformanceCounter("MSExchange Text Messaging", "Text messages Sent via SMTP/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000007 RID: 7
		public static readonly ExPerformanceCounter NumberOfTextMessagesSentViaSmtp = new ExPerformanceCounter("MSExchange Text Messaging", "Total Messages Sent via SMTP", string.Empty, null, new ExPerformanceCounter[]
		{
			ExSmsCounters.RateOfTextMessagesSentViaSmtp
		});

		// Token: 0x04000008 RID: 8
		public static readonly ExPerformanceCounter AverageDeliveryLatency = new ExPerformanceCounter("MSExchange Text Messaging", "Average Text Message Delivery Latency (milliseconds)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000009 RID: 9
		public static readonly ExPerformanceCounter PendingDelivery = new ExPerformanceCounter("MSExchange Text Messaging", "Text Messages Pending Delivery", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400000A RID: 10
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			ExSmsCounters.NumberOfTextMessagesSent,
			ExSmsCounters.NumberOfTextMessagesSentViaEas,
			ExSmsCounters.NumberOfTextMessagesSentViaSmtp,
			ExSmsCounters.AverageDeliveryLatency,
			ExSmsCounters.PendingDelivery
		};
	}
}
