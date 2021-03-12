using System;
using System.Linq;
using System.Security;
using System.Text;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000075 RID: 117
	public class AirSyncInMemoryTraceHandler : ExchangeDiagnosableWrapper<AirSyncTraces>
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00025368 File Offset: 0x00023568
		protected override string UsageText
		{
			get
			{
				return "The In-Memory tracing handler is a diagnostics handler that returns the currently collected in-memory traces. Below are examples for using this diagnostics handler: ";
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0002536F File Offset: 0x0002356F
		protected override string UsageSample
		{
			get
			{
				return " Example 1: Returns health for all devices in cache\r\n                            Get-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component AirSyncInMemoryTrace";
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00025378 File Offset: 0x00023578
		public static AirSyncInMemoryTraceHandler GetInstance()
		{
			if (AirSyncInMemoryTraceHandler.instance == null)
			{
				lock (AirSyncInMemoryTraceHandler.lockObject)
				{
					if (AirSyncInMemoryTraceHandler.instance == null)
					{
						AirSyncInMemoryTraceHandler.instance = new AirSyncInMemoryTraceHandler();
					}
				}
			}
			return AirSyncInMemoryTraceHandler.instance;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000253D0 File Offset: 0x000235D0
		private AirSyncInMemoryTraceHandler()
		{
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x000253D8 File Offset: 0x000235D8
		protected override string ComponentName
		{
			get
			{
				return "AirSyncInMemoryTrace";
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00025488 File Offset: 0x00023688
		internal override AirSyncTraces GetExchangeDiagnosticsInfoData(DiagnosableParameters arguments)
		{
			AirSyncTraces airSyncTraces = new AirSyncTraces();
			StringBuilder tracesBuilder = new StringBuilder("TimeStamp,TraceTag,FormatString,NativeThreadId,ComponentGuid,TraceTag,StartIndex,Id\r\n");
			if (AirSyncDiagnostics.IsInMemoryTracingEnabled() && AirSyncDiagnostics.TroubleshootingContext.MemoryTraceBuilder != null)
			{
				AirSyncDiagnostics.TroubleshootingContext.MemoryTraceBuilder.GetTraces().ToList<TraceEntry>().ForEach(delegate(TraceEntry traceLine)
				{
					tracesBuilder.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", new object[]
					{
						traceLine.Timestamp,
						traceLine.TraceType,
						traceLine.FormatString,
						traceLine.NativeThreadId,
						traceLine.ComponentGuid,
						traceLine.TraceTag,
						traceLine.StartIndex,
						traceLine.Id
					}));
				});
				airSyncTraces.TraceData = SecurityElement.Escape(tracesBuilder.ToString());
				airSyncTraces.TracingEnabled = new bool?(true);
			}
			else
			{
				airSyncTraces.TracingEnabled = new bool?(false);
			}
			return airSyncTraces;
		}

		// Token: 0x0400047F RID: 1151
		private const string TraceHeader = "TimeStamp,TraceTag,FormatString,NativeThreadId,ComponentGuid,TraceTag,StartIndex,Id\r\n";

		// Token: 0x04000480 RID: 1152
		private const string TraceFormatString = "{0},{1},{2},{3},{4},{5},{6},{7}";

		// Token: 0x04000481 RID: 1153
		private static AirSyncInMemoryTraceHandler instance;

		// Token: 0x04000482 RID: 1154
		private static object lockObject = new object();
	}
}
