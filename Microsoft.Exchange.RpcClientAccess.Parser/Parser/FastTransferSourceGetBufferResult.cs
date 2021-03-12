using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000247 RID: 583
	internal abstract class FastTransferSourceGetBufferResult : FastTransferSourceGetBufferResultBase
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00027D27 File Offset: 0x00025F27
		internal ushort Progress
		{
			get
			{
				return (ushort)this.ResultData.Progress;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00027D35 File Offset: 0x00025F35
		internal ushort Steps
		{
			get
			{
				return (ushort)this.ResultData.Steps;
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00027D43 File Offset: 0x00025F43
		internal FastTransferSourceGetBufferResult(ErrorCode errorCode, FastTransferSourceGetBufferData resultData) : base(RopId.FastTransferSourceGetBuffer, errorCode, resultData)
		{
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00027D4F File Offset: 0x00025F4F
		internal FastTransferSourceGetBufferResult(Reader reader, bool isServerBusy) : base(reader, isServerBusy, false)
		{
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00027D5C File Offset: 0x00025F5C
		internal static RopResult Parse(Reader reader)
		{
			ErrorCode errorCode = (ErrorCode)reader.PeekUInt32(2L);
			ErrorCode errorCode2 = errorCode;
			if (errorCode2 == ErrorCode.None)
			{
				return new SuccessfulFastTransferSourceGetBufferResult(reader);
			}
			if (errorCode2 == ErrorCode.ServerBusy)
			{
				return new BackOffFastTransferSourceGetBufferResult(reader);
			}
			return new FailedFastTransferSourceGetBufferResult(reader);
		}

		// Token: 0x040006ED RID: 1773
		internal const int SpecificRopHeaderSize = 9;

		// Token: 0x040006EE RID: 1774
		internal static readonly int FullHeaderSize = Rop.ComputeResultHeaderSize(9);
	}
}
