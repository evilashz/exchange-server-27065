using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000D5 RID: 213
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferSourceGetBufferResultFactory : ResultFactory
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
		internal FastTransferSourceGetBufferResultFactory(ArraySegment<byte> outputBuffer)
		{
			this.outputBuffer = outputBuffer;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000F007 File Offset: 0x0000D207
		public ArraySegment<byte> GetOutputBuffer()
		{
			return this.outputBuffer;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000F00F File Offset: 0x0000D20F
		public RopResult CreateBackoffResult(uint backOffTime)
		{
			return new BackOffFastTransferSourceGetBufferResult(backOffTime);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000F017 File Offset: 0x0000D217
		public override RopResult CreateStandardFailedResult(ErrorCode errorCode)
		{
			return this.CreateFailedResult(errorCode);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000F020 File Offset: 0x0000D220
		public RopResult CreateFailedResult(ErrorCode errorCode)
		{
			return new FailedFastTransferSourceGetBufferResult(errorCode);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000F028 File Offset: 0x0000D228
		public RopResult CreateSuccessfulResult(FastTransferState state, ushort progressCount, ushort totalStepCount, bool moveUserOperation, int outputBufferSize)
		{
			return new SuccessfulFastTransferSourceGetBufferResult(state, progressCount, totalStepCount, moveUserOperation, this.outputBuffer.SubSegment(0, outputBufferSize));
		}

		// Token: 0x040002E3 RID: 739
		private readonly ArraySegment<byte> outputBuffer;

		// Token: 0x040002E4 RID: 740
		internal static readonly FastTransferSourceGetBufferResultFactory Empty = new FastTransferSourceGetBufferResultFactory(default(ArraySegment<byte>));
	}
}
