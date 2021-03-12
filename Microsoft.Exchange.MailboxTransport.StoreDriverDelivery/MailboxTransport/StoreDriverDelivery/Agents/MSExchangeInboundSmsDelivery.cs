using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000BF RID: 191
	internal static class MSExchangeInboundSmsDelivery
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x000204AC File Offset: 0x0001E6AC
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeInboundSmsDelivery.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in MSExchangeInboundSmsDelivery.AllCounters)
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

		// Token: 0x0400035E RID: 862
		public const string CategoryName = "MSExchange Inbound SMS Delivery Agent";

		// Token: 0x0400035F RID: 863
		public static readonly ExPerformanceCounter MessageReceived = new ExPerformanceCounter("MSExchange Inbound SMS Delivery Agent", "Number of SMS messages received", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000360 RID: 864
		public static readonly ExPerformanceCounter MaximumMessageProcessingTime = new ExPerformanceCounter("MSExchange Inbound SMS Delivery Agent", "Maximum message processing time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000361 RID: 865
		public static readonly ExPerformanceCounter AverageMessageProcessingTime = new ExPerformanceCounter("MSExchange Inbound SMS Delivery Agent", "Average message processing time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000362 RID: 866
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			MSExchangeInboundSmsDelivery.MessageReceived,
			MSExchangeInboundSmsDelivery.MaximumMessageProcessingTime,
			MSExchangeInboundSmsDelivery.AverageMessageProcessingTime
		};
	}
}
