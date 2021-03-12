using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000242 RID: 578
	internal sealed class SuccessfulFastTransferSourceCopyMessagesResult : RopResult
	{
		// Token: 0x06000C98 RID: 3224 RVA: 0x00027948 File Offset: 0x00025B48
		internal SuccessfulFastTransferSourceCopyMessagesResult(IServerObject fastTransferDownloadObject) : base(RopId.FastTransferSourceCopyMessages, ErrorCode.None, fastTransferDownloadObject)
		{
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00027954 File Offset: 0x00025B54
		internal SuccessfulFastTransferSourceCopyMessagesResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0002795D File Offset: 0x00025B5D
		internal static SuccessfulFastTransferSourceCopyMessagesResult Parse(Reader reader)
		{
			return new SuccessfulFastTransferSourceCopyMessagesResult(reader);
		}
	}
}
