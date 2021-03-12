using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200002F RID: 47
	internal sealed class PerfMdbSuccessAuxiliaryBlock : BasePerfMdbSuccessAuxiliaryBlock
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x0000410C File Offset: 0x0000230C
		public PerfMdbSuccessAuxiliaryBlock(ushort blockClientId, ushort blockServerId, ushort blockSessionId, ushort blockRequestId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest) : base(1, AuxiliaryBlockTypes.PerfMdbSuccess, 0, blockClientId, blockServerId, blockSessionId, blockRequestId, blockTimeSinceRequest, blockTimeToCompleteRequest)
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000412C File Offset: 0x0000232C
		public PerfMdbSuccessAuxiliaryBlock(ushort blockProcessId, ushort blockClientId, ushort blockServerId, ushort blockSessionId, ushort blockRequestId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest) : base(2, AuxiliaryBlockTypes.PerfMdbSuccess, blockProcessId, blockClientId, blockServerId, blockSessionId, blockRequestId, blockTimeSinceRequest, blockTimeToCompleteRequest)
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000414C File Offset: 0x0000234C
		internal PerfMdbSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
		}
	}
}
