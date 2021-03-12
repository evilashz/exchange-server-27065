using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000A1 RID: 161
	internal static class MeetingSeriesMessageOrdering
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x0001D67C File Offset: 0x0001B87C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MeetingSeriesMessageOrdering.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in MeetingSeriesMessageOrdering.AllCounters)
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

		// Token: 0x0400030A RID: 778
		public const string CategoryName = "MSExchange Meeting Series Message Ordering";

		// Token: 0x0400030B RID: 779
		public static readonly ExPerformanceCounter MeetingMessages = new ExPerformanceCounter("MSExchange Meeting Series Message Ordering", "Number of Meeting Messages", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400030C RID: 780
		public static readonly ExPerformanceCounter SeriesMeetingInstanceMessages = new ExPerformanceCounter("MSExchange Meeting Series Message Ordering", "Number of Series Meeting Instance Messages", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400030D RID: 781
		public static readonly ExPerformanceCounter ParkedSeriesMeetingInstanceMessages = new ExPerformanceCounter("MSExchange Meeting Series Message Ordering", "Number of Parked Series Meeting Instance Messages", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400030E RID: 782
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			MeetingSeriesMessageOrdering.MeetingMessages,
			MeetingSeriesMessageOrdering.SeriesMeetingInstanceMessages,
			MeetingSeriesMessageOrdering.ParkedSeriesMeetingInstanceMessages
		};
	}
}
