using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000249 RID: 585
	internal sealed class FailedFastTransferSourceGetBufferResult : FastTransferSourceGetBufferResult
	{
		// Token: 0x06000CB8 RID: 3256 RVA: 0x00027DC2 File Offset: 0x00025FC2
		internal FailedFastTransferSourceGetBufferResult(ErrorCode errorCode) : base(errorCode, new FastTransferSourceGetBufferData(FastTransferState.Error, false))
		{
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00027DD2 File Offset: 0x00025FD2
		internal FailedFastTransferSourceGetBufferResult(Reader reader) : base(reader, false)
		{
		}
	}
}
