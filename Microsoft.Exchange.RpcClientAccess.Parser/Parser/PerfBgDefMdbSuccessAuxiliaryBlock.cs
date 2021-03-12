using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000020 RID: 32
	internal sealed class PerfBgDefMdbSuccessAuxiliaryBlock : BasePerfDefMdbSuccessAuxiliaryBlock
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00003B34 File Offset: 0x00001D34
		public PerfBgDefMdbSuccessAuxiliaryBlock(uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, ushort blockRequestId) : base(AuxiliaryBlockTypes.PerfBgDefMdbSuccess, blockTimeSinceRequest, blockTimeToCompleteRequest, blockRequestId)
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003B41 File Offset: 0x00001D41
		internal PerfBgDefMdbSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003B4A File Offset: 0x00001D4A
		protected internal override void ReportClientPerformance(IClientPerformanceDataSink sink)
		{
			sink.ReportEvent(new ClientPerformanceEventArgs(ClientPerformanceEventType.BackgroundRpcSucceeded));
			base.ReportClientPerformance(sink);
		}
	}
}
