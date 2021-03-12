using System;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000036 RID: 54
	internal sealed class RpcProcessingTimeProcessor : IClientPerformanceDataSink
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00007E89 File Offset: 0x00006089
		private RpcProcessingTimeProcessor()
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007E91 File Offset: 0x00006091
		void IClientPerformanceDataSink.ReportEvent(ClientPerformanceEventArgs clientEvent)
		{
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007E93 File Offset: 0x00006093
		void IClientPerformanceDataSink.ReportLatency(TimeSpan latency)
		{
			ProtocolLog.UpdateClientRpcLatency(latency);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007E9B File Offset: 0x0000609B
		internal static RpcProcessingTimeProcessor Create()
		{
			return RpcProcessingTimeProcessor.instance;
		}

		// Token: 0x040000CE RID: 206
		private static readonly RpcProcessingTimeProcessor instance = new RpcProcessingTimeProcessor();
	}
}
