using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000CD RID: 205
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferDestinationPutBufferResultFactory : StandardResultFactory
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x0000EEEB File Offset: 0x0000D0EB
		internal FastTransferDestinationPutBufferResultFactory() : base(RopId.FastTransferDestinationPutBuffer)
		{
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000EEF5 File Offset: 0x0000D0F5
		public RopResult CreateSuccessfulResult(ushort progressCount, ushort totalStepCount, bool moveUserOperation, ushort usedBufferSize)
		{
			return new FastTransferDestinationPutBufferResult(ErrorCode.None, progressCount, totalStepCount, moveUserOperation, usedBufferSize);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000EF02 File Offset: 0x0000D102
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, 0, 0, false, 0);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000EF0F File Offset: 0x0000D10F
		public RopResult CreateFailedResult(ErrorCode errorCode, ushort progressCount, ushort totalStepCount, bool moveUserOperation, ushort usedBufferSize)
		{
			return new FastTransferDestinationPutBufferResult(errorCode, progressCount, totalStepCount, moveUserOperation, usedBufferSize);
		}
	}
}
