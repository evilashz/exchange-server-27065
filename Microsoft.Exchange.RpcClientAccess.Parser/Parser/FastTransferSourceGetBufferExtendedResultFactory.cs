using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000D4 RID: 212
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferSourceGetBufferExtendedResultFactory : ResultFactory
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x0000EF8C File Offset: 0x0000D18C
		internal FastTransferSourceGetBufferExtendedResultFactory(ArraySegment<byte> outputBuffer)
		{
			this.outputBuffer = outputBuffer;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000EF9B File Offset: 0x0000D19B
		public ArraySegment<byte> GetOutputBuffer()
		{
			return this.outputBuffer;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000EFA3 File Offset: 0x0000D1A3
		public RopResult CreateBackoffResult(uint backOffTime)
		{
			return new BackOffFastTransferSourceGetBufferExtendedResult(backOffTime);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000EFAB File Offset: 0x0000D1AB
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000EFB4 File Offset: 0x0000D1B4
		public RopResult CreateFailedResult(ErrorCode errorCode)
		{
			return new FailedFastTransferSourceGetBufferExtendedResult(errorCode);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000EFBC File Offset: 0x0000D1BC
		public RopResult CreateSuccessfulResult(FastTransferState state, uint progressCount, uint totalStepCount, bool moveUserOperation, int outputBufferSize)
		{
			return new SuccessfulFastTransferSourceGetBufferExtendedResult(state, progressCount, totalStepCount, moveUserOperation, this.outputBuffer.SubSegment(0, outputBufferSize));
		}

		// Token: 0x040002E1 RID: 737
		private readonly ArraySegment<byte> outputBuffer;

		// Token: 0x040002E2 RID: 738
		internal static readonly FastTransferSourceGetBufferExtendedResultFactory Empty = new FastTransferSourceGetBufferExtendedResultFactory(default(ArraySegment<byte>));
	}
}
