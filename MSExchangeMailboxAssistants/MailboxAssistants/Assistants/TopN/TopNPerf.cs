using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.TopN
{
	// Token: 0x0200018C RID: 396
	internal static class TopNPerf
	{
		// Token: 0x06000FAF RID: 4015 RVA: 0x0005CDEC File Offset: 0x0005AFEC
		public static void GetPerfCounterInfo(XElement element)
		{
			if (TopNPerf.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in TopNPerf.AllCounters)
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

		// Token: 0x040009FC RID: 2556
		public const string CategoryName = "MSExchange TopN Words Assistant";

		// Token: 0x040009FD RID: 2557
		public static readonly ExPerformanceCounter TimeToProcessLastMailbox = new ExPerformanceCounter("MSExchange TopN Words Assistant", "Time to Process Last Mailbox in Milliseconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040009FE RID: 2558
		public static readonly ExPerformanceCounter NumberOfMailboxesProcessed = new ExPerformanceCounter("MSExchange TopN Words Assistant", "Number of Mailboxes Processed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040009FF RID: 2559
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			TopNPerf.TimeToProcessLastMailbox,
			TopNPerf.NumberOfMailboxesProcessed
		};
	}
}
