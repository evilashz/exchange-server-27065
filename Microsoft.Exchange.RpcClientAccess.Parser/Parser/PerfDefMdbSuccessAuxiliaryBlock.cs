using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000027 RID: 39
	internal sealed class PerfDefMdbSuccessAuxiliaryBlock : BasePerfDefMdbSuccessAuxiliaryBlock
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00003EFD File Offset: 0x000020FD
		public PerfDefMdbSuccessAuxiliaryBlock(uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, ushort blockRequestId) : base(AuxiliaryBlockTypes.PerfDefMdbSuccess, blockTimeSinceRequest, blockTimeToCompleteRequest, blockRequestId)
		{
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003F09 File Offset: 0x00002109
		internal PerfDefMdbSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
		}
	}
}
