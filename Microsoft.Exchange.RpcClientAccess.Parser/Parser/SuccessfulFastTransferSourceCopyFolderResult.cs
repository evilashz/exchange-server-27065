using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000241 RID: 577
	internal sealed class SuccessfulFastTransferSourceCopyFolderResult : RopResult
	{
		// Token: 0x06000C95 RID: 3221 RVA: 0x0002792B File Offset: 0x00025B2B
		internal SuccessfulFastTransferSourceCopyFolderResult(IServerObject fastTransferDownloadObject) : base(RopId.FastTransferSourceCopyFolder, ErrorCode.None, fastTransferDownloadObject)
		{
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00027937 File Offset: 0x00025B37
		internal SuccessfulFastTransferSourceCopyFolderResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00027940 File Offset: 0x00025B40
		internal static SuccessfulFastTransferSourceCopyFolderResult Parse(Reader reader)
		{
			return new SuccessfulFastTransferSourceCopyFolderResult(reader);
		}
	}
}
