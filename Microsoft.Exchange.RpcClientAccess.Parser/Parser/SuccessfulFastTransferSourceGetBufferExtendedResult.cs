using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200024C RID: 588
	internal sealed class SuccessfulFastTransferSourceGetBufferExtendedResult : FastTransferSourceGetBufferExtendedResult
	{
		// Token: 0x06000CC2 RID: 3266 RVA: 0x00027E76 File Offset: 0x00026076
		internal SuccessfulFastTransferSourceGetBufferExtendedResult(FastTransferState state, uint progress, uint steps, bool isMoveUser, ArraySegment<byte> data) : base(ErrorCode.None, new FastTransferSourceGetBufferData(state, progress, steps, isMoveUser, data, true))
		{
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00027E8C File Offset: 0x0002608C
		internal SuccessfulFastTransferSourceGetBufferExtendedResult(Reader reader) : base(reader, false)
		{
		}
	}
}
