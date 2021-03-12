using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ResourceBooking
{
	// Token: 0x0200012C RID: 300
	internal static class ResourceBookingPerfmon
	{
		// Token: 0x06000C0B RID: 3083 RVA: 0x0004E354 File Offset: 0x0004C554
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ResourceBookingPerfmon.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in ResourceBookingPerfmon.AllCounters)
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

		// Token: 0x0400076D RID: 1901
		public const string CategoryName = "MSExchange Resource Booking";

		// Token: 0x0400076E RID: 1902
		public static readonly ExPerformanceCounter TotalEvents = new ExPerformanceCounter("MSExchange Resource Booking", "Events", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400076F RID: 1903
		public static readonly ExPerformanceCounter RequestsSubmitted = new ExPerformanceCounter("MSExchange Resource Booking", "Requests Submitted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000770 RID: 1904
		public static readonly ExPerformanceCounter RequestsProcessed = new ExPerformanceCounter("MSExchange Resource Booking", "Requests Processed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000771 RID: 1905
		public static readonly ExPerformanceCounter RequestsFailed = new ExPerformanceCounter("MSExchange Resource Booking", "Requests Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000772 RID: 1906
		public static readonly ExPerformanceCounter Accepted = new ExPerformanceCounter("MSExchange Resource Booking", "Accepted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000773 RID: 1907
		public static readonly ExPerformanceCounter Declined = new ExPerformanceCounter("MSExchange Resource Booking", "Declined", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000774 RID: 1908
		public static readonly ExPerformanceCounter Cancelled = new ExPerformanceCounter("MSExchange Resource Booking", "Cancelled", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000775 RID: 1909
		public static readonly ExPerformanceCounter AverageResourceBookingProcessingTime = new ExPerformanceCounter("MSExchange Resource Booking", "Average Resource Booking Processing Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000776 RID: 1910
		public static readonly ExPerformanceCounter AverageResourceBookingProcessingTimeBase = new ExPerformanceCounter("MSExchange Resource Booking", "Average Processing Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000777 RID: 1911
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			ResourceBookingPerfmon.TotalEvents,
			ResourceBookingPerfmon.RequestsSubmitted,
			ResourceBookingPerfmon.RequestsProcessed,
			ResourceBookingPerfmon.RequestsFailed,
			ResourceBookingPerfmon.Accepted,
			ResourceBookingPerfmon.Declined,
			ResourceBookingPerfmon.Cancelled,
			ResourceBookingPerfmon.AverageResourceBookingProcessingTime,
			ResourceBookingPerfmon.AverageResourceBookingProcessingTimeBase
		};
	}
}
