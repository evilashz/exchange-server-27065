using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000D9 RID: 217
	internal static class WorkingSet
	{
		// Token: 0x0600067B RID: 1659 RVA: 0x000245E0 File Offset: 0x000227E0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (WorkingSet.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in WorkingSet.AllCounters)
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

		// Token: 0x0400038C RID: 908
		public const string CategoryName = "Working Set Delivery Agent";

		// Token: 0x0400038D RID: 909
		public static readonly ExPerformanceCounter AverageStopWatchTime = new ExPerformanceCounter("Working Set Delivery Agent", "Average Time to Process Message", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400038E RID: 910
		public static readonly ExPerformanceCounter AverageStopWatchTimeBase = new ExPerformanceCounter("Working Set Delivery Agent", "Base for Average Time to Process Message", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400038F RID: 911
		public static readonly ExPerformanceCounter AverageCpuTime = new ExPerformanceCounter("Working Set Delivery Agent", "Average CPU Time to Process Message", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000390 RID: 912
		public static readonly ExPerformanceCounter AverageCpuTimeBase = new ExPerformanceCounter("Working Set Delivery Agent", "Base for Average CPU Time to Process Message", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000391 RID: 913
		public static readonly ExPerformanceCounter AverageStoreRPCs = new ExPerformanceCounter("Working Set Delivery Agent", "Average Store RPCs", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000392 RID: 914
		public static readonly ExPerformanceCounter AverageStoreRPCsBase = new ExPerformanceCounter("Working Set Delivery Agent", "Base for Average Store RPCs", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000393 RID: 915
		public static readonly ExPerformanceCounter ProcessingAccepted = new ExPerformanceCounter("Working Set Delivery Agent", "Signal Mails Received - Agent Enabled", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000394 RID: 916
		public static readonly ExPerformanceCounter ProcessingRejected = new ExPerformanceCounter("Working Set Delivery Agent", "Signal Mails Rejected - Agent Disabled", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000395 RID: 917
		public static readonly ExPerformanceCounter ProcessingSuccess = new ExPerformanceCounter("Working Set Delivery Agent", "Processing - Success", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000396 RID: 918
		public static readonly ExPerformanceCounter ProcessingFailed = new ExPerformanceCounter("Working Set Delivery Agent", "Processing - Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000397 RID: 919
		public static readonly ExPerformanceCounter AddItem = new ExPerformanceCounter("Working Set Delivery Agent", "Add Item - Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000398 RID: 920
		public static readonly ExPerformanceCounter AddExchangeItem = new ExPerformanceCounter("Working Set Delivery Agent", "Add Item - Exchange", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000399 RID: 921
		public static readonly ExPerformanceCounter ItemsNotSupported = new ExPerformanceCounter("Working Set Delivery Agent", "Add Item - Not Supported", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400039A RID: 922
		public static readonly ExPerformanceCounter PartitionsCreated = new ExPerformanceCounter("Working Set Delivery Agent", "Partitions - Created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400039B RID: 923
		public static readonly ExPerformanceCounter PartitionsDeleted = new ExPerformanceCounter("Working Set Delivery Agent", "Partitions - Deleted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400039C RID: 924
		public static readonly ExPerformanceCounter LastSignalProcessingTime = new ExPerformanceCounter("Working Set Delivery Agent", "Last Signal Processing Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400039D RID: 925
		public static readonly ExPerformanceCounter AverageSignalProcessingTime = new ExPerformanceCounter("Working Set Delivery Agent", "Average Signal Processing Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400039E RID: 926
		public static readonly ExPerformanceCounter AverageSignalProcessingTimeBase = new ExPerformanceCounter("Working Set Delivery Agent", "Average Signal Processing Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400039F RID: 927
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			WorkingSet.AverageStopWatchTime,
			WorkingSet.AverageStopWatchTimeBase,
			WorkingSet.AverageCpuTime,
			WorkingSet.AverageCpuTimeBase,
			WorkingSet.AverageStoreRPCs,
			WorkingSet.AverageStoreRPCsBase,
			WorkingSet.ProcessingAccepted,
			WorkingSet.ProcessingRejected,
			WorkingSet.ProcessingSuccess,
			WorkingSet.ProcessingFailed,
			WorkingSet.AddItem,
			WorkingSet.AddExchangeItem,
			WorkingSet.ItemsNotSupported,
			WorkingSet.PartitionsCreated,
			WorkingSet.PartitionsDeleted,
			WorkingSet.LastSignalProcessingTime,
			WorkingSet.AverageSignalProcessingTime,
			WorkingSet.AverageSignalProcessingTimeBase
		};
	}
}
