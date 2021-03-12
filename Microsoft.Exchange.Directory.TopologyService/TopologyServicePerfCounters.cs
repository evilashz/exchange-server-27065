using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000034 RID: 52
	internal static class TopologyServicePerfCounters
	{
		// Token: 0x06000219 RID: 537 RVA: 0x0000E53C File Offset: 0x0000C73C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (TopologyServicePerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in TopologyServicePerfCounters.AllCounters)
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

		// Token: 0x04000161 RID: 353
		public const string CategoryName = "MSExchange ADAccess Topology Service";

		// Token: 0x04000162 RID: 354
		public static readonly ExPerformanceCounter PID = new ExPerformanceCounter("MSExchange ADAccess Topology Service", "PID", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000163 RID: 355
		public static readonly ExPerformanceCounter WorkItemCount = new ExPerformanceCounter("MSExchange ADAccess Topology Service", "Active Work Item Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000164 RID: 356
		public static readonly ExPerformanceCounter WorkItemsExecuted = new ExPerformanceCounter("MSExchange ADAccess Topology Service", "Work Items Executed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000165 RID: 357
		public static readonly ExPerformanceCounter WorkItemsCancelled = new ExPerformanceCounter("MSExchange ADAccess Topology Service", "Work Items Cancelled", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000166 RID: 358
		public static readonly ExPerformanceCounter WorkItemsFailures = new ExPerformanceCounter("MSExchange ADAccess Topology Service", "Work Items Failures", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000167 RID: 359
		public static readonly ExPerformanceCounter AverageWorkItemRunTime = new ExPerformanceCounter("MSExchange ADAccess Topology Service", "Work Items Average Run Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000168 RID: 360
		public static readonly ExPerformanceCounter AverageWorkItemRunTimeBase = new ExPerformanceCounter("MSExchange ADAccess Topology Service", "Work Items Average Run Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000169 RID: 361
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			TopologyServicePerfCounters.PID,
			TopologyServicePerfCounters.WorkItemCount,
			TopologyServicePerfCounters.WorkItemsExecuted,
			TopologyServicePerfCounters.WorkItemsCancelled,
			TopologyServicePerfCounters.WorkItemsFailures,
			TopologyServicePerfCounters.AverageWorkItemRunTime,
			TopologyServicePerfCounters.AverageWorkItemRunTimeBase
		};
	}
}
