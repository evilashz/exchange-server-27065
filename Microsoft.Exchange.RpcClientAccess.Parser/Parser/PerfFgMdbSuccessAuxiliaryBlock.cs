using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200002D RID: 45
	internal sealed class PerfFgMdbSuccessAuxiliaryBlock : BasePerfMdbSuccessAuxiliaryBlock
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00004060 File Offset: 0x00002260
		public PerfFgMdbSuccessAuxiliaryBlock(ushort blockClientId, ushort blockServerId, ushort blockSessionId, ushort blockRequestId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest) : base(1, AuxiliaryBlockTypes.PerfFgMdbSuccess, 0, blockClientId, blockServerId, blockSessionId, blockRequestId, blockTimeSinceRequest, blockTimeToCompleteRequest)
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004080 File Offset: 0x00002280
		public PerfFgMdbSuccessAuxiliaryBlock(ushort blockProcessId, ushort blockClientId, ushort blockServerId, ushort blockSessionId, ushort blockRequestId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest) : base(2, AuxiliaryBlockTypes.PerfFgMdbSuccess, blockProcessId, blockClientId, blockServerId, blockSessionId, blockRequestId, blockTimeSinceRequest, blockTimeToCompleteRequest)
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000040A1 File Offset: 0x000022A1
		internal PerfFgMdbSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000040AA File Offset: 0x000022AA
		protected internal override void ReportClientPerformance(IClientPerformanceDataSink sink)
		{
			sink.ReportEvent(new ClientPerformanceEventArgs(ClientPerformanceEventType.ForegroundRpcSucceeded));
			base.ReportClientPerformance(sink);
		}
	}
}
