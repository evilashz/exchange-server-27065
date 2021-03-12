using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000071 RID: 113
	internal static class MSExchangeConversationsProcessing
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x00014734 File Offset: 0x00012934
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeConversationsProcessing.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in MSExchangeConversationsProcessing.AllCounters)
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

		// Token: 0x04000250 RID: 592
		public const string CategoryName = "MSExchange Conversations Transport Agent";

		// Token: 0x04000251 RID: 593
		public static readonly ExPerformanceCounter AverageMessageProcessingTime = new ExPerformanceCounter("MSExchange Conversations Transport Agent", "Average message processing time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000252 RID: 594
		public static readonly ExPerformanceCounter LastMessageProcessingTime = new ExPerformanceCounter("MSExchange Conversations Transport Agent", "Last message processing time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000253 RID: 595
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			MSExchangeConversationsProcessing.AverageMessageProcessingTime,
			MSExchangeConversationsProcessing.LastMessageProcessingTime
		};
	}
}
