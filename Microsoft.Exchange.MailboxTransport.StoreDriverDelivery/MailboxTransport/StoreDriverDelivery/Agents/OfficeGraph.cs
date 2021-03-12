using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000A9 RID: 169
	internal static class OfficeGraph
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x0001E6B0 File Offset: 0x0001C8B0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (OfficeGraph.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in OfficeGraph.AllCounters)
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

		// Token: 0x04000323 RID: 803
		public const string CategoryName = "Office Graph Delivery Agent";

		// Token: 0x04000324 RID: 804
		public static readonly ExPerformanceCounter ItemsSeen = new ExPerformanceCounter("Office Graph Delivery Agent", "Items - Seen", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000325 RID: 805
		public static readonly ExPerformanceCounter ItemsFilteredTotal = new ExPerformanceCounter("Office Graph Delivery Agent", "Items - Filtered (Total)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000326 RID: 806
		public static readonly ExPerformanceCounter FilterCriteriaMessage = new ExPerformanceCounter("Office Graph Delivery Agent", "Filter Criteria - Message", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000327 RID: 807
		public static readonly ExPerformanceCounter FilterCriteriaHasAttachment = new ExPerformanceCounter("Office Graph Delivery Agent", "Filter Criteria - Has Attachment", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000328 RID: 808
		public static readonly ExPerformanceCounter FilterCriteriaInterestingAttachment = new ExPerformanceCounter("Office Graph Delivery Agent", "Filter Criteria - Interesting Attachment", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000329 RID: 809
		public static readonly ExPerformanceCounter FilterCriteriaHasSharePointUrl = new ExPerformanceCounter("Office Graph Delivery Agent", "Filter Criteria - Has SharePoint URL", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400032A RID: 810
		public static readonly ExPerformanceCounter FilterCriteriaIsFromFavoriteSender = new ExPerformanceCounter("Office Graph Delivery Agent", "Filter Criteria - Is From Favorite Sender", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400032B RID: 811
		public static readonly ExPerformanceCounter SignalPersisted = new ExPerformanceCounter("Office Graph Delivery Agent", "Persisted Signals - Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400032C RID: 812
		public static readonly ExPerformanceCounter TotalExceptions = new ExPerformanceCounter("Office Graph Delivery Agent", "Exceptions - Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400032D RID: 813
		public static readonly ExPerformanceCounter LastSharePointUrlRetrievalTime = new ExPerformanceCounter("Office Graph Delivery Agent", "Last SharePoint Url Retrieval time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400032E RID: 814
		public static readonly ExPerformanceCounter AverageSharePointUrlRetrievalTime = new ExPerformanceCounter("Office Graph Delivery Agent", "Average SharePoint Url Retrieval time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400032F RID: 815
		public static readonly ExPerformanceCounter AverageSharePointUrlRetrievalTimeBase = new ExPerformanceCounter("Office Graph Delivery Agent", "Average SharePoint Url Retrieval time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000330 RID: 816
		public static readonly ExPerformanceCounter LastSignalCreationTime = new ExPerformanceCounter("Office Graph Delivery Agent", "Last Signal Creation Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000331 RID: 817
		public static readonly ExPerformanceCounter AverageSignalCreationTime = new ExPerformanceCounter("Office Graph Delivery Agent", "Average Signal Creation Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000332 RID: 818
		public static readonly ExPerformanceCounter AverageSignalCreationTimeBase = new ExPerformanceCounter("Office Graph Delivery Agent", "Average Signal Creation Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000333 RID: 819
		public static readonly ExPerformanceCounter LastSignalPersistingTime = new ExPerformanceCounter("Office Graph Delivery Agent", "Last Signal Persisting Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000334 RID: 820
		public static readonly ExPerformanceCounter AverageSignalPersistingTime = new ExPerformanceCounter("Office Graph Delivery Agent", "Average Signal Persisting Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000335 RID: 821
		public static readonly ExPerformanceCounter AverageSignalPersistingTimeBase = new ExPerformanceCounter("Office Graph Delivery Agent", "Average Signal Persisting Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000336 RID: 822
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			OfficeGraph.ItemsSeen,
			OfficeGraph.ItemsFilteredTotal,
			OfficeGraph.FilterCriteriaMessage,
			OfficeGraph.FilterCriteriaHasAttachment,
			OfficeGraph.FilterCriteriaInterestingAttachment,
			OfficeGraph.FilterCriteriaHasSharePointUrl,
			OfficeGraph.FilterCriteriaIsFromFavoriteSender,
			OfficeGraph.SignalPersisted,
			OfficeGraph.TotalExceptions,
			OfficeGraph.LastSharePointUrlRetrievalTime,
			OfficeGraph.AverageSharePointUrlRetrievalTime,
			OfficeGraph.AverageSharePointUrlRetrievalTimeBase,
			OfficeGraph.LastSignalCreationTime,
			OfficeGraph.AverageSignalCreationTime,
			OfficeGraph.AverageSignalCreationTimeBase,
			OfficeGraph.LastSignalPersistingTime,
			OfficeGraph.AverageSignalPersistingTime,
			OfficeGraph.AverageSignalPersistingTimeBase
		};
	}
}
