using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000248 RID: 584
	internal sealed class SuccessfulFastTransferSourceGetBufferResult : FastTransferSourceGetBufferResult
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x00027DA2 File Offset: 0x00025FA2
		internal SuccessfulFastTransferSourceGetBufferResult(FastTransferState state, ushort progress, ushort steps, bool isMoveUser, ArraySegment<byte> data) : base(ErrorCode.None, new FastTransferSourceGetBufferData(state, (uint)progress, (uint)steps, isMoveUser, data, false))
		{
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00027DB8 File Offset: 0x00025FB8
		internal SuccessfulFastTransferSourceGetBufferResult(Reader reader) : base(reader, false)
		{
		}
	}
}
