using System;
using System.Collections.Concurrent;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000016 RID: 22
	internal class TopologyCache : ConcurrentDictionary<string, TopologyDiscoveryInfo>
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00006402 File Offset: 0x00004602
		internal TopologyCache() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000640F File Offset: 0x0000460F
		public static TopologyCache Default
		{
			get
			{
				return TopologyCache.singleton;
			}
		}

		// Token: 0x0400004E RID: 78
		private static readonly TopologyCache singleton = new TopologyCache();
	}
}
