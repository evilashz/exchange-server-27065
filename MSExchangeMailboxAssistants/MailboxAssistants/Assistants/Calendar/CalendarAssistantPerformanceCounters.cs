using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Calendar
{
	// Token: 0x020000BB RID: 187
	internal static class CalendarAssistantPerformanceCounters
	{
		// Token: 0x060007BF RID: 1983 RVA: 0x00038290 File Offset: 0x00036490
		public static void GetPerfCounterInfo(XElement element)
		{
			if (CalendarAssistantPerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in CalendarAssistantPerformanceCounters.AllCounters)
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

		// Token: 0x040005AE RID: 1454
		public const string CategoryName = "MSExchange Calendar Attendant";

		// Token: 0x040005AF RID: 1455
		public static readonly ExPerformanceCounter MeetingMessagesProcessed = new ExPerformanceCounter("MSExchange Calendar Attendant", "Meeting Messages Processed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040005B0 RID: 1456
		public static readonly ExPerformanceCounter MeetingMessagesDeleted = new ExPerformanceCounter("MSExchange Calendar Attendant", "Meeting Messages Deleted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040005B1 RID: 1457
		public static readonly ExPerformanceCounter RequestsFailed = new ExPerformanceCounter("MSExchange Calendar Attendant", "Requests Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040005B2 RID: 1458
		public static readonly ExPerformanceCounter MeetingRequests = new ExPerformanceCounter("MSExchange Calendar Attendant", "Invitations", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040005B3 RID: 1459
		public static readonly ExPerformanceCounter MeetingResponses = new ExPerformanceCounter("MSExchange Calendar Attendant", "Meeting Responses", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040005B4 RID: 1460
		public static readonly ExPerformanceCounter MeetingCancellations = new ExPerformanceCounter("MSExchange Calendar Attendant", "Meeting Cancellations", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040005B5 RID: 1461
		public static readonly ExPerformanceCounter RacesLost = new ExPerformanceCounter("MSExchange Calendar Attendant", "Lost Races", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040005B6 RID: 1462
		public static readonly ExPerformanceCounter LastCalAssistantProcessingTime = new ExPerformanceCounter("MSExchange Calendar Attendant", "Last Calendar Attendant Processing Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040005B7 RID: 1463
		public static readonly ExPerformanceCounter AverageCalAssistantProcessingTime = new ExPerformanceCounter("MSExchange Calendar Attendant", "Average Calendar Attendant Processing Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040005B8 RID: 1464
		public static readonly ExPerformanceCounter AverageCalAssistantProcessingTimeBase = new ExPerformanceCounter("MSExchange Calendar Attendant", "Average Calendar Attendant Processing Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040005B9 RID: 1465
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			CalendarAssistantPerformanceCounters.MeetingMessagesProcessed,
			CalendarAssistantPerformanceCounters.MeetingMessagesDeleted,
			CalendarAssistantPerformanceCounters.RequestsFailed,
			CalendarAssistantPerformanceCounters.MeetingRequests,
			CalendarAssistantPerformanceCounters.MeetingResponses,
			CalendarAssistantPerformanceCounters.MeetingCancellations,
			CalendarAssistantPerformanceCounters.RacesLost,
			CalendarAssistantPerformanceCounters.LastCalAssistantProcessingTime,
			CalendarAssistantPerformanceCounters.AverageCalAssistantProcessingTime,
			CalendarAssistantPerformanceCounters.AverageCalAssistantProcessingTimeBase
		};
	}
}
