using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200002A RID: 42
	internal sealed class PerfFgDefMdbSuccessAuxiliaryBlock : BasePerfDefMdbSuccessAuxiliaryBlock
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00003F80 File Offset: 0x00002180
		public PerfFgDefMdbSuccessAuxiliaryBlock(uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, ushort blockRequestId) : base(AuxiliaryBlockTypes.PerfFgDefMdbSuccess, blockTimeSinceRequest, blockTimeToCompleteRequest, blockRequestId)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003F8D File Offset: 0x0000218D
		internal PerfFgDefMdbSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003F96 File Offset: 0x00002196
		protected internal override void ReportClientPerformance(IClientPerformanceDataSink sink)
		{
			sink.ReportEvent(new ClientPerformanceEventArgs(ClientPerformanceEventType.ForegroundRpcSucceeded));
			base.ReportClientPerformance(sink);
		}
	}
}
