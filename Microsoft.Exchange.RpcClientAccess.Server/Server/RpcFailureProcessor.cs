using System;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000035 RID: 53
	internal sealed class RpcFailureProcessor : IClientPerformanceDataSink
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00007E03 File Offset: 0x00006003
		private RpcFailureProcessor()
		{
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007E0C File Offset: 0x0000600C
		void IClientPerformanceDataSink.ReportEvent(ClientPerformanceEventArgs clientEvent)
		{
			if (clientEvent.EventType == ClientPerformanceEventType.RpcFailed && clientEvent is ClientFailureEventArgs)
			{
				ClientFailureEventArgs clientFailureEventArgs = (ClientFailureEventArgs)clientEvent;
				ProtocolLog.UpdateClientRpcFailureData(clientFailureEventArgs.TimeStamp, new FailureCounterData
				{
					FailureCode = clientFailureEventArgs.FailureCode
				});
				return;
			}
			if (clientEvent.EventType == ClientPerformanceEventType.RpcAttempted && clientEvent is ClientTimeStampedEventArgs)
			{
				ClientTimeStampedEventArgs clientTimeStampedEventArgs = (ClientTimeStampedEventArgs)clientEvent;
				ProtocolLog.UpdateClientRpcAttemptsData(clientTimeStampedEventArgs.TimeStamp, null);
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007E74 File Offset: 0x00006074
		void IClientPerformanceDataSink.ReportLatency(TimeSpan clientLatency)
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00007E76 File Offset: 0x00006076
		internal static RpcFailureProcessor Create()
		{
			return RpcFailureProcessor.instance;
		}

		// Token: 0x040000CD RID: 205
		private static readonly RpcFailureProcessor instance = new RpcFailureProcessor();
	}
}
