using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000021 RID: 33
	internal sealed class PerfBgFailureAuxiliaryBlock : BasePerfFailureAuxiliaryBlock
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00003B60 File Offset: 0x00001D60
		public PerfBgFailureAuxiliaryBlock(ushort blockClientId, ushort blockServerId, ushort blockSessionId, ushort blockRequestId, uint blockTimeSinceRequest, uint blockTimeToFailRequest, uint blockResultCode, byte blockRequestOperation) : base(1, AuxiliaryBlockTypes.PerfBgFailure, 0, blockClientId, blockServerId, blockSessionId, blockRequestId, blockTimeSinceRequest, blockTimeToFailRequest, blockResultCode, blockRequestOperation)
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003B84 File Offset: 0x00001D84
		public PerfBgFailureAuxiliaryBlock(ushort blockProcessId, ushort blockClientId, ushort blockServerId, ushort blockSessionId, ushort blockRequestId, uint blockTimeSinceRequest, uint blockTimeToFailRequest, uint blockResultCode, byte blockRequestOperation) : base(2, AuxiliaryBlockTypes.PerfBgFailure, blockProcessId, blockClientId, blockServerId, blockSessionId, blockRequestId, blockTimeSinceRequest, blockTimeToFailRequest, blockResultCode, blockRequestOperation)
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003BA9 File Offset: 0x00001DA9
		internal PerfBgFailureAuxiliaryBlock(Reader reader) : base(reader)
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003BB2 File Offset: 0x00001DB2
		protected internal override void ReportClientPerformance(IClientPerformanceDataSink sink)
		{
			sink.ReportEvent(new ClientPerformanceEventArgs(ClientPerformanceEventType.BackgroundRpcFailed));
			base.ReportClientPerformance(sink);
		}
	}
}
