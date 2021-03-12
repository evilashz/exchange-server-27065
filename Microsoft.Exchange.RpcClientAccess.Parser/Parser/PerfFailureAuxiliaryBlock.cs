using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000028 RID: 40
	internal sealed class PerfFailureAuxiliaryBlock : BasePerfFailureAuxiliaryBlock
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00003F14 File Offset: 0x00002114
		public PerfFailureAuxiliaryBlock(ushort blockClientId, ushort blockServerId, ushort blockSessionId, ushort blockRequestId, uint blockTimeSinceRequest, uint blockTimeToFailRequest, uint blockResultCode, byte blockRequestOperation) : base(1, AuxiliaryBlockTypes.PerfFailure, 0, blockClientId, blockServerId, blockSessionId, blockRequestId, blockTimeSinceRequest, blockTimeToFailRequest, blockResultCode, blockRequestOperation)
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003F38 File Offset: 0x00002138
		public PerfFailureAuxiliaryBlock(ushort blockProcessId, ushort blockClientId, ushort blockServerId, ushort blockSessionId, ushort blockRequestId, uint blockTimeSinceRequest, uint blockTimeToFailRequest, uint blockResultCode, byte blockRequestOperation) : base(2, AuxiliaryBlockTypes.PerfFailure, blockProcessId, blockClientId, blockServerId, blockSessionId, blockRequestId, blockTimeSinceRequest, blockTimeToFailRequest, blockResultCode, blockRequestOperation)
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003F5D File Offset: 0x0000215D
		internal PerfFailureAuxiliaryBlock(Reader reader) : base(reader)
		{
		}
	}
}
