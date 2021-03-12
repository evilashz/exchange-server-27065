using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000029 RID: 41
	internal sealed class PerfFgDefGcSuccessAuxiliaryBlock : BasePerfDefGcSuccessAuxiliaryBlock
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00003F66 File Offset: 0x00002166
		public PerfFgDefGcSuccessAuxiliaryBlock(ushort blockServerId, ushort blockSessionId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, byte blockRequestOperation) : base(AuxiliaryBlockTypes.PerfFgDefGcSuccess, blockServerId, blockSessionId, blockTimeSinceRequest, blockTimeToCompleteRequest, blockRequestOperation)
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003F77 File Offset: 0x00002177
		internal PerfFgDefGcSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
		}
	}
}
