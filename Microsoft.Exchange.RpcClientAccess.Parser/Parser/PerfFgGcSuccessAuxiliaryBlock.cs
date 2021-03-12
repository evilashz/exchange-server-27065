using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200002C RID: 44
	internal sealed class PerfFgGcSuccessAuxiliaryBlock : BasePerfGcSuccessAuxiliaryBlock
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00004014 File Offset: 0x00002214
		public PerfFgGcSuccessAuxiliaryBlock(ushort blockClientId, ushort blockServerId, ushort blockSessionId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, byte blockRequestOperation) : base(1, AuxiliaryBlockTypes.PerfFgGcSuccess, 0, blockClientId, blockServerId, blockSessionId, blockTimeSinceRequest, blockTimeToCompleteRequest, blockRequestOperation)
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004034 File Offset: 0x00002234
		public PerfFgGcSuccessAuxiliaryBlock(ushort blockProcessId, ushort blockClientId, ushort blockServerId, ushort blockSessionId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, byte blockRequestOperation) : base(2, AuxiliaryBlockTypes.PerfFgGcSuccess, blockProcessId, blockClientId, blockServerId, blockSessionId, blockTimeSinceRequest, blockTimeToCompleteRequest, blockRequestOperation)
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004055 File Offset: 0x00002255
		internal PerfFgGcSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
		}
	}
}
