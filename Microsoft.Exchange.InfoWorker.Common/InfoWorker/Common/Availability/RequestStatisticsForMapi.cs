using System;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000C8 RID: 200
	internal sealed class RequestStatisticsForMapi
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x000165B0 File Offset: 0x000147B0
		public static RequestStatisticsForMapi Begin()
		{
			PerformanceContext performanceContext;
			NativeMethods.GetTLSPerformanceContext(out performanceContext);
			return new RequestStatisticsForMapi
			{
				beginRpcCount = performanceContext.rpcCount,
				beginRpcLatency = performanceContext.rpcLatency
			};
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000165E6 File Offset: 0x000147E6
		public RequestStatistics End(RequestStatisticsType tag)
		{
			return this.End(tag, null);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000165F0 File Offset: 0x000147F0
		public RequestStatistics End(RequestStatisticsType tag, string destination)
		{
			long timeTaken = 0L;
			int requestCount = 0;
			PerformanceContext performanceContext;
			if (NativeMethods.GetTLSPerformanceContext(out performanceContext))
			{
				timeTaken = (long)((performanceContext.rpcLatency - this.beginRpcLatency) / 100000UL);
				requestCount = (int)(performanceContext.rpcCount - this.beginRpcCount);
			}
			if (destination == null)
			{
				return RequestStatistics.Create(tag, timeTaken, requestCount);
			}
			return RequestStatistics.Create(tag, timeTaken, requestCount, destination);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00016645 File Offset: 0x00014845
		private RequestStatisticsForMapi()
		{
		}

		// Token: 0x040002F6 RID: 758
		private uint beginRpcCount;

		// Token: 0x040002F7 RID: 759
		private ulong beginRpcLatency;
	}
}
