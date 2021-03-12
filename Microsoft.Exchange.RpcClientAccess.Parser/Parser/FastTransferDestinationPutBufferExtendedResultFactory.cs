using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000CC RID: 204
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferDestinationPutBufferExtendedResultFactory : StandardResultFactory
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x0000EEB6 File Offset: 0x0000D0B6
		internal FastTransferDestinationPutBufferExtendedResultFactory() : base(RopId.FastTransferDestinationPutBufferExtended)
		{
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000EEC3 File Offset: 0x0000D0C3
		public RopResult CreateSuccessfulResult(uint progressCount, uint totalStepCount, bool moveUserOperation, ushort usedBufferSize)
		{
			return new FastTransferDestinationPutBufferExtendedResult(ErrorCode.None, progressCount, totalStepCount, moveUserOperation, usedBufferSize);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode, 0U, 0U, false, 0);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000EEDD File Offset: 0x0000D0DD
		public RopResult CreateFailedResult(ErrorCode errorCode, uint progressCount, uint totalStepCount, bool moveUserOperation, ushort usedBufferSize)
		{
			return new FastTransferDestinationPutBufferExtendedResult(errorCode, progressCount, totalStepCount, moveUserOperation, usedBufferSize);
		}
	}
}
