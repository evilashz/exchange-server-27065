using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200023F RID: 575
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class MSExchangePop3Aggregation
	{
		// Token: 0x060014E9 RID: 5353 RVA: 0x0004BB88 File Offset: 0x00049D88
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangePop3Aggregation.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in MSExchangePop3Aggregation.AllCounters)
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

		// Token: 0x04000AFC RID: 2812
		public const string CategoryName = "MSExchange Transport Sync - Pop";

		// Token: 0x04000AFD RID: 2813
		private static readonly ExPerformanceCounter RateOfMessageBytesReceived = new ExPerformanceCounter("MSExchange Transport Sync - Pop", "Message Bytes Received/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000AFE RID: 2814
		public static readonly ExPerformanceCounter MessageBytesReceivedTotal = new ExPerformanceCounter("MSExchange Transport Sync - Pop", "Message Bytes Received Total", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangePop3Aggregation.RateOfMessageBytesReceived
		});

		// Token: 0x04000AFF RID: 2815
		private static readonly ExPerformanceCounter RateOfMessagesReceived = new ExPerformanceCounter("MSExchange Transport Sync - Pop", "Messages Received/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000B00 RID: 2816
		public static readonly ExPerformanceCounter MessagesReceivedTotal = new ExPerformanceCounter("MSExchange Transport Sync - Pop", "Messages Received Total", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangePop3Aggregation.RateOfMessagesReceived
		});

		// Token: 0x04000B01 RID: 2817
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			MSExchangePop3Aggregation.MessageBytesReceivedTotal,
			MSExchangePop3Aggregation.MessagesReceivedTotal
		};
	}
}
