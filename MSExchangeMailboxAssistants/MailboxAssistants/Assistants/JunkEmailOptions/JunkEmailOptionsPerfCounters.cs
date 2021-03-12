using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.JunkEmailOptions
{
	// Token: 0x0200011A RID: 282
	internal static class JunkEmailOptionsPerfCounters
	{
		// Token: 0x06000B6D RID: 2925 RVA: 0x00049864 File Offset: 0x00047A64
		public static void GetPerfCounterInfo(XElement element)
		{
			if (JunkEmailOptionsPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in JunkEmailOptionsPerfCounters.AllCounters)
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

		// Token: 0x04000719 RID: 1817
		public const string CategoryName = "MSExchange Junk E-mail Options Assistant";

		// Token: 0x0400071A RID: 1818
		private static readonly ExPerformanceCounter RecipientsUpdatedPerSecond = new ExPerformanceCounter("MSExchange Junk E-mail Options Assistant", "Recipients updated per second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400071B RID: 1819
		public static readonly ExPerformanceCounter TotalRecipientsUpdated = new ExPerformanceCounter("MSExchange Junk E-mail Options Assistant", "Recipients updated", string.Empty, null, new ExPerformanceCounter[]
		{
			JunkEmailOptionsPerfCounters.RecipientsUpdatedPerSecond
		});

		// Token: 0x0400071C RID: 1820
		public static readonly ExPerformanceCounter TotalPartialUpdates = new ExPerformanceCounter("MSExchange Junk E-mail Options Assistant", "Partial updates due to oversized safe or block lists", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400071D RID: 1821
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			JunkEmailOptionsPerfCounters.TotalRecipientsUpdated,
			JunkEmailOptionsPerfCounters.TotalPartialUpdates
		};
	}
}
