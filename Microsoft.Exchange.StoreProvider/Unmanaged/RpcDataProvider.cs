using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002AE RID: 686
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RpcDataProvider : PerformanceDataProvider
	{
		// Token: 0x06000C7E RID: 3198 RVA: 0x00033F19 File Offset: 0x00032119
		private RpcDataProvider() : base("Remote Procedure Calls", true)
		{
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00033F27 File Offset: 0x00032127
		public static RpcDataProvider Instance
		{
			get
			{
				return RpcDataProvider.singletonInstance;
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00033F30 File Offset: 0x00032130
		public override PerformanceData TakeSnapshot(bool begin)
		{
			PerformanceContext performanceContext;
			if (NativeMethods.GetTLSPerformanceContext(out performanceContext))
			{
				base.Latency = TimeSpan.FromMilliseconds((double)((int)(performanceContext.rpcLatency / 10000UL)));
				base.RequestCount = performanceContext.rpcCount;
			}
			else
			{
				base.Latency = TimeSpan.Zero;
				base.RequestCount = 0U;
			}
			base.TakeSnapshot(begin);
			return new PerformanceData(base.Latency, base.RequestCount, (int)performanceContext.currentActiveConnections, (int)performanceContext.currentConnectionPoolSize, (int)performanceContext.failedConnections);
		}

		// Token: 0x040011DC RID: 4572
		private const int MillisecondsFactor = 10000;

		// Token: 0x040011DD RID: 4573
		private static RpcDataProvider singletonInstance = new RpcDataProvider();
	}
}
