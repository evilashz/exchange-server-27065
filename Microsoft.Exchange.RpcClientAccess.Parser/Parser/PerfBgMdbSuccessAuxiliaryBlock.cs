using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000023 RID: 35
	internal sealed class PerfBgMdbSuccessAuxiliaryBlock : BasePerfMdbSuccessAuxiliaryBlock
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00003C14 File Offset: 0x00001E14
		public PerfBgMdbSuccessAuxiliaryBlock(ushort blockClientId, ushort blockServerId, ushort blockSessionId, ushort blockRequestId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest) : base(1, AuxiliaryBlockTypes.PerfBgMdbSuccess, 0, blockClientId, blockServerId, blockSessionId, blockRequestId, blockTimeSinceRequest, blockTimeToCompleteRequest)
		{
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003C34 File Offset: 0x00001E34
		public PerfBgMdbSuccessAuxiliaryBlock(ushort blockProcessId, ushort blockClientId, ushort blockServerId, ushort blockSessionId, ushort blockRequestId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest) : base(2, AuxiliaryBlockTypes.PerfBgMdbSuccess, blockProcessId, blockClientId, blockServerId, blockSessionId, blockRequestId, blockTimeSinceRequest, blockTimeToCompleteRequest)
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003C55 File Offset: 0x00001E55
		internal PerfBgMdbSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003C5E File Offset: 0x00001E5E
		protected internal override void ReportClientPerformance(IClientPerformanceDataSink sink)
		{
			sink.ReportEvent(new ClientPerformanceEventArgs(ClientPerformanceEventType.BackgroundRpcSucceeded));
			base.ReportClientPerformance(sink);
		}
	}
}
