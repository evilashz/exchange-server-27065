using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200002B RID: 43
	internal sealed class PerfFgFailureAuxiliaryBlock : BasePerfFailureAuxiliaryBlock
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00003FAC File Offset: 0x000021AC
		public PerfFgFailureAuxiliaryBlock(ushort blockClientId, ushort blockServerId, ushort blockSessionId, ushort blockRequestId, uint blockTimeSinceRequest, uint blockTimeToFailRequest, uint blockResultCode, byte blockRequestOperation) : base(1, AuxiliaryBlockTypes.PerfFgFailure, 0, blockClientId, blockServerId, blockSessionId, blockRequestId, blockTimeSinceRequest, blockTimeToFailRequest, blockResultCode, blockRequestOperation)
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003FD0 File Offset: 0x000021D0
		public PerfFgFailureAuxiliaryBlock(ushort blockProcessId, ushort blockClientId, ushort blockServerId, ushort blockSessionId, ushort blockRequestId, uint blockTimeSinceRequest, uint blockTimeToFailRequest, uint blockResultCode, byte blockRequestOperation) : base(2, AuxiliaryBlockTypes.PerfFgFailure, blockProcessId, blockClientId, blockServerId, blockSessionId, blockRequestId, blockTimeSinceRequest, blockTimeToFailRequest, blockResultCode, blockRequestOperation)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003FF5 File Offset: 0x000021F5
		internal PerfFgFailureAuxiliaryBlock(Reader reader) : base(reader)
		{
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003FFE File Offset: 0x000021FE
		protected internal override void ReportClientPerformance(IClientPerformanceDataSink sink)
		{
			sink.ReportEvent(new ClientPerformanceEventArgs(ClientPerformanceEventType.ForegroundRpcFailed));
			base.ReportClientPerformance(sink);
		}
	}
}
