using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000018 RID: 24
	internal static class EdgeSyncTopologyPerfCounters
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x000092C4 File Offset: 0x000074C4
		public static void GetPerfCounterInfo(XElement element)
		{
			if (EdgeSyncTopologyPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in EdgeSyncTopologyPerfCounters.AllCounters)
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

		// Token: 0x04000081 RID: 129
		public const string CategoryName = "MSExchangeEdgeSync Topology";

		// Token: 0x04000082 RID: 130
		public static readonly ExPerformanceCounter Updates = new ExPerformanceCounter("MSExchangeEdgeSync Topology", "Total topology updates", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000083 RID: 131
		public static readonly ExPerformanceCounter Servers = new ExPerformanceCounter("MSExchangeEdgeSync Topology", "Exchange Servers total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000084 RID: 132
		public static readonly ExPerformanceCounter Edges = new ExPerformanceCounter("MSExchangeEdgeSync Topology", "Edge Servers total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000085 RID: 133
		public static readonly ExPerformanceCounter Bridgheads = new ExPerformanceCounter("MSExchangeEdgeSync Topology", "Hub Transport Servers total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000086 RID: 134
		public static readonly ExPerformanceCounter EdgesLeased = new ExPerformanceCounter("MSExchangeEdgeSync Topology", "Edge Servers leased total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000087 RID: 135
		public static readonly ExPerformanceCounter SyncNowStarted = new ExPerformanceCounter("MSExchangeEdgeSync Topology", "SyncNow started total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000088 RID: 136
		public static readonly ExPerformanceCounter SyncNowPendingSyncs = new ExPerformanceCounter("MSExchangeEdgeSync Topology", "SyncNow Edges not completed total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000089 RID: 137
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			EdgeSyncTopologyPerfCounters.Updates,
			EdgeSyncTopologyPerfCounters.Servers,
			EdgeSyncTopologyPerfCounters.Edges,
			EdgeSyncTopologyPerfCounters.Bridgheads,
			EdgeSyncTopologyPerfCounters.EdgesLeased,
			EdgeSyncTopologyPerfCounters.SyncNowStarted,
			EdgeSyncTopologyPerfCounters.SyncNowPendingSyncs
		};
	}
}
