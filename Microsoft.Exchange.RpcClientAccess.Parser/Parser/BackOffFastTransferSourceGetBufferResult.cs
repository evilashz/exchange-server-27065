using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200024A RID: 586
	internal sealed class BackOffFastTransferSourceGetBufferResult : FastTransferSourceGetBufferResult
	{
		// Token: 0x06000CBA RID: 3258 RVA: 0x00027DDC File Offset: 0x00025FDC
		internal BackOffFastTransferSourceGetBufferResult(uint backOffTime) : base(ErrorCode.ServerBusy, new FastTransferSourceGetBufferData(backOffTime, false))
		{
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00027DF0 File Offset: 0x00025FF0
		internal BackOffFastTransferSourceGetBufferResult(Reader reader) : base(reader, true)
		{
		}
	}
}
