using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000FF RID: 255
	internal static class CalNotifsCounters
	{
		// Token: 0x06000A8C RID: 2700 RVA: 0x0004528C File Offset: 0x0004348C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (CalNotifsCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in CalNotifsCounters.AllCounters)
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

		// Token: 0x040006BE RID: 1726
		public const string CategoryName = "Calendar Notifications Assistant";

		// Token: 0x040006BF RID: 1727
		private static readonly ExPerformanceCounter RateOfNotificationsSent = new ExPerformanceCounter("Calendar Notifications Assistant", "Notifications sent/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040006C0 RID: 1728
		public static readonly ExPerformanceCounter NumberOfNotificationsSent = new ExPerformanceCounter("Calendar Notifications Assistant", "Number of notifications sent", string.Empty, null, new ExPerformanceCounter[]
		{
			CalNotifsCounters.RateOfNotificationsSent
		});

		// Token: 0x040006C1 RID: 1729
		private static readonly ExPerformanceCounter RateOfAgendasSent = new ExPerformanceCounter("Calendar Notifications Assistant", "Agenda notifications sent/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040006C2 RID: 1730
		public static readonly ExPerformanceCounter NumberOfAgendasSent = new ExPerformanceCounter("Calendar Notifications Assistant", "Number of agenda notifications sent", string.Empty, null, new ExPerformanceCounter[]
		{
			CalNotifsCounters.RateOfAgendasSent
		});

		// Token: 0x040006C3 RID: 1731
		private static readonly ExPerformanceCounter RateOfTextRemindersSent = new ExPerformanceCounter("Calendar Notifications Assistant", "Text Reminder notifications sent/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040006C4 RID: 1732
		public static readonly ExPerformanceCounter NumberOfTextRemindersSent = new ExPerformanceCounter("Calendar Notifications Assistant", "Number of calendar text reminder notifications sent", string.Empty, null, new ExPerformanceCounter[]
		{
			CalNotifsCounters.RateOfTextRemindersSent
		});

		// Token: 0x040006C5 RID: 1733
		private static readonly ExPerformanceCounter RateOfVoiceRemindersSent = new ExPerformanceCounter("Calendar Notifications Assistant", "Voice Reminder notifications sent/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040006C6 RID: 1734
		public static readonly ExPerformanceCounter NumberOfVoiceRemindersSent = new ExPerformanceCounter("Calendar Notifications Assistant", "Number of calendar voice reminder notifications sent", string.Empty, null, new ExPerformanceCounter[]
		{
			CalNotifsCounters.RateOfVoiceRemindersSent
		});

		// Token: 0x040006C7 RID: 1735
		private static readonly ExPerformanceCounter RateOfUpdatesSent = new ExPerformanceCounter("Calendar Notifications Assistant", "Update notifications sent/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040006C8 RID: 1736
		public static readonly ExPerformanceCounter NumberOfInterestingMailboxEvents = new ExPerformanceCounter("Calendar Notifications Assistant", "Number of interesting mailbox events", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040006C9 RID: 1737
		private static readonly ExPerformanceCounter RateOfInterestingMailboxEvents = new ExPerformanceCounter("Calendar Notifications Assistant", "Interesting mailbox events/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040006CA RID: 1738
		public static readonly ExPerformanceCounter NumberOfUpdatesSent = new ExPerformanceCounter("Calendar Notifications Assistant", "Number of calendar update notifications sent", string.Empty, null, new ExPerformanceCounter[]
		{
			CalNotifsCounters.RateOfUpdatesSent,
			CalNotifsCounters.RateOfInterestingMailboxEvents
		});

		// Token: 0x040006CB RID: 1739
		public static readonly ExPerformanceCounter AverageUpdateLatency = new ExPerformanceCounter("Calendar Notifications Assistant", "Average update processing latency (milliseconds)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040006CC RID: 1740
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			CalNotifsCounters.NumberOfNotificationsSent,
			CalNotifsCounters.NumberOfAgendasSent,
			CalNotifsCounters.NumberOfTextRemindersSent,
			CalNotifsCounters.NumberOfVoiceRemindersSent,
			CalNotifsCounters.NumberOfUpdatesSent,
			CalNotifsCounters.NumberOfInterestingMailboxEvents,
			CalNotifsCounters.AverageUpdateLatency
		};
	}
}
