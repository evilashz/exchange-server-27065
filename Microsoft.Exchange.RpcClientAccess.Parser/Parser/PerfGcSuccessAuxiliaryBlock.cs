using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200002E RID: 46
	internal sealed class PerfGcSuccessAuxiliaryBlock : BasePerfGcSuccessAuxiliaryBlock
	{
		// Token: 0x060000CE RID: 206 RVA: 0x000040C0 File Offset: 0x000022C0
		public PerfGcSuccessAuxiliaryBlock(ushort blockClientId, ushort blockServerId, ushort blockSessionId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, byte blockRequestOperation) : base(1, AuxiliaryBlockTypes.PerfGcSuccess, 0, blockClientId, blockServerId, blockSessionId, blockTimeSinceRequest, blockTimeToCompleteRequest, blockRequestOperation)
		{
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000040E0 File Offset: 0x000022E0
		public PerfGcSuccessAuxiliaryBlock(ushort blockProcessId, ushort blockClientId, ushort blockServerId, ushort blockSessionId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, byte blockRequestOperation) : base(2, AuxiliaryBlockTypes.PerfGcSuccess, blockProcessId, blockClientId, blockServerId, blockSessionId, blockTimeSinceRequest, blockTimeToCompleteRequest, blockRequestOperation)
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004100 File Offset: 0x00002300
		internal PerfGcSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
		}
	}
}
