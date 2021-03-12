using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200024B RID: 587
	internal class RouteCalculatorContext
	{
		// Token: 0x0600199A RID: 6554 RVA: 0x00067EF5 File Offset: 0x000660F5
		public RouteCalculatorContext(RoutingContextCore contextCore, RoutingTopologyBase topologyConfig, RoutingServerInfoMap serverMap)
		{
			this.Core = contextCore;
			this.TopologyConfig = topologyConfig;
			this.ServerMap = serverMap;
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x00067F12 File Offset: 0x00066112
		public DateTime Timestamp
		{
			get
			{
				return this.TopologyConfig.WhenCreated;
			}
		}

		// Token: 0x04000C2D RID: 3117
		public readonly RoutingContextCore Core;

		// Token: 0x04000C2E RID: 3118
		public readonly RoutingTopologyBase TopologyConfig;

		// Token: 0x04000C2F RID: 3119
		public readonly RoutingServerInfoMap ServerMap;
	}
}
