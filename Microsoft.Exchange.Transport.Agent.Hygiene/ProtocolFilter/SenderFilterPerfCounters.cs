using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.ProtocolFilter
{
	// Token: 0x0200002C RID: 44
	internal static class SenderFilterPerfCounters
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00008EB0 File Offset: 0x000070B0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SenderFilterPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in SenderFilterPerfCounters.AllCounters)
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

		// Token: 0x040000FD RID: 253
		public const string CategoryName = "MSExchange Sender Filter Agent";

		// Token: 0x040000FE RID: 254
		private static readonly ExPerformanceCounter MessagesEvaluatedBySenderFilterPerSecond = new ExPerformanceCounter("MSExchange Sender Filter Agent", "Messages Evaluated by Sender Filter/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000FF RID: 255
		public static readonly ExPerformanceCounter TotalMessagesEvaluatedBySenderFilter = new ExPerformanceCounter("MSExchange Sender Filter Agent", "Messages Evaluated by Sender Filter", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderFilterPerfCounters.MessagesEvaluatedBySenderFilterPerSecond
		});

		// Token: 0x04000100 RID: 256
		private static readonly ExPerformanceCounter MessagesFilteredBySenderFilterPerSecond = new ExPerformanceCounter("MSExchange Sender Filter Agent", "Messages Filtered by Sender Filter/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000101 RID: 257
		public static readonly ExPerformanceCounter TotalMessagesFilteredBySenderFilter = new ExPerformanceCounter("MSExchange Sender Filter Agent", "Messages Filtered by Sender Filter", string.Empty, null, new ExPerformanceCounter[]
		{
			SenderFilterPerfCounters.MessagesFilteredBySenderFilterPerSecond
		});

		// Token: 0x04000102 RID: 258
		public static readonly ExPerformanceCounter TotalPerRecipientSenderBlocks = new ExPerformanceCounter("MSExchange Sender Filter Agent", "Senders Blocked Due to Per-Recipient Blocked Senders", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000103 RID: 259
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			SenderFilterPerfCounters.TotalMessagesEvaluatedBySenderFilter,
			SenderFilterPerfCounters.TotalMessagesFilteredBySenderFilter,
			SenderFilterPerfCounters.TotalPerRecipientSenderBlocks
		};
	}
}
