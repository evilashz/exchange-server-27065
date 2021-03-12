using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000022 RID: 34
	internal sealed class PerfBgGcSuccessAuxiliaryBlock : BasePerfGcSuccessAuxiliaryBlock
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public PerfBgGcSuccessAuxiliaryBlock(ushort blockClientId, ushort blockServerId, ushort blockSessionId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, byte blockRequestOperation) : base(1, AuxiliaryBlockTypes.PerfBgGcSuccess, 0, blockClientId, blockServerId, blockSessionId, blockTimeSinceRequest, blockTimeToCompleteRequest, blockRequestOperation)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public PerfBgGcSuccessAuxiliaryBlock(ushort blockProcessId, ushort blockClientId, ushort blockServerId, ushort blockSessionId, uint blockTimeSinceRequest, uint blockTimeToCompleteRequest, byte blockRequestOperation) : base(2, AuxiliaryBlockTypes.PerfBgGcSuccess, blockProcessId, blockClientId, blockServerId, blockSessionId, blockTimeSinceRequest, blockTimeToCompleteRequest, blockRequestOperation)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003C09 File Offset: 0x00001E09
		internal PerfBgGcSuccessAuxiliaryBlock(Reader reader) : base(reader)
		{
		}
	}
}
