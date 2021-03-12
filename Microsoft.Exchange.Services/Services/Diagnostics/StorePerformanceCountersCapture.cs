using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Win32;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000056 RID: 86
	public class StorePerformanceCountersCapture
	{
		// Token: 0x06000218 RID: 536 RVA: 0x0000B774 File Offset: 0x00009974
		internal static StorePerformanceCountersCapture Start(StoreSession storeSession)
		{
			StorePerformanceCountersCapture storePerformanceCountersCapture = new StorePerformanceCountersCapture
			{
				storeSession = storeSession
			};
			storePerformanceCountersCapture.Restart();
			return storePerformanceCountersCapture;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000B797 File Offset: 0x00009997
		internal void Restart()
		{
			NativeMethods.GetTLSPerformanceContext(out this.beginPerformanceContext);
			this.beginCumulativeRPCPerformanceStatistics = this.storeSession.GetStoreCumulativeRPCStats();
			this.stopwatch = Stopwatch.StartNew();
			this.beginThreadTimes = ThreadTimes.GetFromCurrentThread();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000B7CC File Offset: 0x000099CC
		internal StorePerformanceCounters Stop()
		{
			ThreadTimes fromCurrentThread = ThreadTimes.GetFromCurrentThread();
			long elapsedMilliseconds = this.stopwatch.ElapsedMilliseconds;
			PerformanceContext performanceContext;
			NativeMethods.GetTLSPerformanceContext(out performanceContext);
			CumulativeRPCPerformanceStatistics storeCumulativeRPCStats = this.storeSession.GetStoreCumulativeRPCStats();
			return new StorePerformanceCounters
			{
				ElapsedMilliseconds = elapsedMilliseconds,
				Cpu = (fromCurrentThread.Kernel - this.beginThreadTimes.Kernel + (fromCurrentThread.User - this.beginThreadTimes.User)).TotalMilliseconds,
				RpcLatency = TimeSpan.FromTicks((long)(performanceContext.rpcLatency - this.beginPerformanceContext.rpcLatency)).TotalMilliseconds,
				RpcCount = (int)(performanceContext.rpcCount - this.beginPerformanceContext.rpcCount),
				RpcLatencyOnStore = (storeCumulativeRPCStats.timeInServer - this.beginCumulativeRPCPerformanceStatistics.timeInServer).TotalMilliseconds
			};
		}

		// Token: 0x040004CF RID: 1231
		private StoreSession storeSession;

		// Token: 0x040004D0 RID: 1232
		private Stopwatch stopwatch;

		// Token: 0x040004D1 RID: 1233
		private PerformanceContext beginPerformanceContext;

		// Token: 0x040004D2 RID: 1234
		private ThreadTimes beginThreadTimes;

		// Token: 0x040004D3 RID: 1235
		private CumulativeRPCPerformanceStatistics beginCumulativeRPCPerformanceStatistics;
	}
}
