using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.AttachFilter
{
	// Token: 0x02000026 RID: 38
	internal static class PerfCounters
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00005D9C File Offset: 0x00003F9C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in PerfCounters.AllCounters)
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

		// Token: 0x04000077 RID: 119
		public const string CategoryName = "MSExchange Attachment Filtering";

		// Token: 0x04000078 RID: 120
		private static readonly ExPerformanceCounter MsgsFilteredRate = new ExPerformanceCounter("MSExchange Attachment Filtering", "Messages Filtered/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000079 RID: 121
		public static readonly ExPerformanceCounter MsgsFiltered = new ExPerformanceCounter("MSExchange Attachment Filtering", "Messages Attachment Filtered", string.Empty, null, new ExPerformanceCounter[]
		{
			PerfCounters.MsgsFilteredRate
		});

		// Token: 0x0400007A RID: 122
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			PerfCounters.MsgsFiltered
		};
	}
}
