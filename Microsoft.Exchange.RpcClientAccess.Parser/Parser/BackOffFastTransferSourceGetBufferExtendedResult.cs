using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200024E RID: 590
	internal sealed class BackOffFastTransferSourceGetBufferExtendedResult : FastTransferSourceGetBufferExtendedResult
	{
		// Token: 0x06000CC6 RID: 3270 RVA: 0x00027EB0 File Offset: 0x000260B0
		internal BackOffFastTransferSourceGetBufferExtendedResult(uint backOffTime) : base(ErrorCode.ServerBusy, new FastTransferSourceGetBufferData(backOffTime, true))
		{
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00027EC4 File Offset: 0x000260C4
		internal BackOffFastTransferSourceGetBufferExtendedResult(Reader reader) : base(reader, true)
		{
		}
	}
}
