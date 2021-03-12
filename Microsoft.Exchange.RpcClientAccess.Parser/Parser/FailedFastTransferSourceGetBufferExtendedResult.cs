using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200024D RID: 589
	internal sealed class FailedFastTransferSourceGetBufferExtendedResult : FastTransferSourceGetBufferExtendedResult
	{
		// Token: 0x06000CC4 RID: 3268 RVA: 0x00027E96 File Offset: 0x00026096
		internal FailedFastTransferSourceGetBufferExtendedResult(ErrorCode errorCode) : base(errorCode, new FastTransferSourceGetBufferData(FastTransferState.Error, true))
		{
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00027EA6 File Offset: 0x000260A6
		internal FailedFastTransferSourceGetBufferExtendedResult(Reader reader) : base(reader, false)
		{
		}
	}
}
