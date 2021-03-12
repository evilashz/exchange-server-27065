using System;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200002F RID: 47
	internal sealed class PerformanceCounterProcessor : IClientPerformanceDataSink
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00007475 File Offset: 0x00005675
		private PerformanceCounterProcessor()
		{
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007480 File Offset: 0x00005680
		void IClientPerformanceDataSink.ReportEvent(ClientPerformanceEventArgs clientEvent)
		{
			switch (clientEvent.EventType)
			{
			case ClientPerformanceEventType.BackgroundRpcFailed:
				RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.ClientBackgroundCallsFailed.Increment();
				return;
			case ClientPerformanceEventType.BackgroundRpcSucceeded:
				RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.ClientBackgroundCallsSucceeded.Increment();
				return;
			case ClientPerformanceEventType.ForegroundRpcFailed:
				RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.ClientForegroundCallsFailed.Increment();
				return;
			case ClientPerformanceEventType.ForegroundRpcSucceeded:
				RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.ClientForegroundCallsSucceeded.Increment();
				return;
			case ClientPerformanceEventType.RpcAttempted:
				RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.ClientCallsAttempted.Increment();
				return;
			case ClientPerformanceEventType.RpcFailed:
				RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.ClientCallsFailed.Increment();
				return;
			case ClientPerformanceEventType.RpcSucceeded:
				RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.ClientCallsSucceeded.Increment();
				return;
			default:
				throw new ArgumentException(string.Format("Counter {0} not implemented.", clientEvent.EventType), "counter");
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007550 File Offset: 0x00005750
		void IClientPerformanceDataSink.ReportLatency(TimeSpan latency)
		{
			if (latency > PerformanceCounterProcessor.slow1)
			{
				RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.ClientCallsSlow1.Increment();
			}
			if (latency > PerformanceCounterProcessor.slow2)
			{
				RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.ClientCallsSlow2.Increment();
			}
			if (latency > PerformanceCounterProcessor.slow3)
			{
				RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.ClientCallsSlow3.Increment();
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000075B4 File Offset: 0x000057B4
		internal static PerformanceCounterProcessor Create()
		{
			return PerformanceCounterProcessor.instance;
		}

		// Token: 0x040000A2 RID: 162
		private static readonly PerformanceCounterProcessor instance = new PerformanceCounterProcessor();

		// Token: 0x040000A3 RID: 163
		private static readonly TimeSpan slow1 = TimeSpan.FromSeconds(2.0);

		// Token: 0x040000A4 RID: 164
		private static readonly TimeSpan slow2 = TimeSpan.FromSeconds(5.0);

		// Token: 0x040000A5 RID: 165
		private static readonly TimeSpan slow3 = TimeSpan.FromSeconds(10.0);
	}
}
