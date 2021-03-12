using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200024B RID: 587
	internal abstract class FastTransferSourceGetBufferExtendedResult : FastTransferSourceGetBufferResultBase
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00027DFA File Offset: 0x00025FFA
		internal uint Progress
		{
			get
			{
				return this.ResultData.Progress;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00027E07 File Offset: 0x00026007
		internal uint Steps
		{
			get
			{
				return this.ResultData.Steps;
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00027E14 File Offset: 0x00026014
		internal FastTransferSourceGetBufferExtendedResult(ErrorCode errorCode, FastTransferSourceGetBufferData resultData) : base(RopId.FastTransferSourceGetBufferExtended, errorCode, resultData)
		{
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00027E23 File Offset: 0x00026023
		internal FastTransferSourceGetBufferExtendedResult(Reader reader, bool isServerBusy) : base(reader, isServerBusy, true)
		{
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00027E30 File Offset: 0x00026030
		internal static RopResult Parse(Reader reader)
		{
			ErrorCode errorCode = (ErrorCode)reader.PeekUInt32(2L);
			ErrorCode errorCode2 = errorCode;
			if (errorCode2 == ErrorCode.None)
			{
				return new SuccessfulFastTransferSourceGetBufferExtendedResult(reader);
			}
			if (errorCode2 == ErrorCode.ServerBusy)
			{
				return new BackOffFastTransferSourceGetBufferExtendedResult(reader);
			}
			return new FailedFastTransferSourceGetBufferExtendedResult(reader);
		}

		// Token: 0x040006EF RID: 1775
		internal const int SpecificRopHeaderSize = 13;

		// Token: 0x040006F0 RID: 1776
		internal static readonly int FullHeaderSize = Rop.ComputeResultHeaderSize(13);
	}
}
