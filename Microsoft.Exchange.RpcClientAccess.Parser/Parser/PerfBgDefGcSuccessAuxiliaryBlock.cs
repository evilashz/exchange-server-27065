using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200001F RID: 31
	internal sealed class PerfBgDefGcSuccessAuxiliaryBlock : BasePerfDefGcSuccessAuxiliaryBlock
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00003B1A File Offset: 0x00001D1A
		public PerfBgDefGcSuccessAuxiliaryBlock(ushort blockServerId, ushort blockSessionId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, byte blockRequestOperation) : base(AuxiliaryBlockTypes.PerfBgDefGcSuccess, blockServerId, blockSessionId, blockTimeSinceRequest, blockTimeToCompleteRequest, blockRequestOperation)
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003B2B File Offset: 0x00001D2B
		internal PerfBgDefGcSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
		}
	}
}
