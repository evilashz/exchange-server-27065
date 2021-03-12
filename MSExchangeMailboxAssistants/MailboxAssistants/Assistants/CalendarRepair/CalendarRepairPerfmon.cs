using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x0200015B RID: 347
	internal static class CalendarRepairPerfmon
	{
		// Token: 0x06000E25 RID: 3621 RVA: 0x00055568 File Offset: 0x00053768
		public static void GetPerfCounterInfo(XElement element)
		{
			if (CalendarRepairPerfmon.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in CalendarRepairPerfmon.AllCounters)
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

		// Token: 0x0400091F RID: 2335
		public const string CategoryName = "MSExchange Calendar Repair Assistant";

		// Token: 0x04000920 RID: 2336
		public static readonly ExPerformanceCounter TotalItemsInspected = new ExPerformanceCounter("MSExchange Calendar Repair Assistant", "Items Inspected", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000921 RID: 2337
		public static readonly ExPerformanceCounter TotalItemsRepaired = new ExPerformanceCounter("MSExchange Calendar Repair Assistant", "Items Repaired", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000922 RID: 2338
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			CalendarRepairPerfmon.TotalItemsInspected,
			CalendarRepairPerfmon.TotalItemsRepaired
		};
	}
}
