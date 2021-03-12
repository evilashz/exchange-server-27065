using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000026 RID: 38
	internal sealed class PerfDefGcSuccessAuxiliaryBlock : BasePerfDefGcSuccessAuxiliaryBlock
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00003EE4 File Offset: 0x000020E4
		public PerfDefGcSuccessAuxiliaryBlock(ushort blockServerId, ushort blockSessionId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, byte blockRequestOperation) : base(AuxiliaryBlockTypes.PerfDefGcSuccess, blockServerId, blockSessionId, blockTimeSinceRequest, blockTimeToCompleteRequest, blockRequestOperation)
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003EF4 File Offset: 0x000020F4
		internal PerfDefGcSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
		}
	}
}
