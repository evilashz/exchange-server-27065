using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000240 RID: 576
	internal sealed class SuccessfulFastTransferGetIncrementalStateResult : RopResult
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x0002790B File Offset: 0x00025B0B
		internal SuccessfulFastTransferGetIncrementalStateResult(IServerObject fastTransferDownloadObject) : base(RopId.FastTransferGetIncrementalState, ErrorCode.None, fastTransferDownloadObject)
		{
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0002791A File Offset: 0x00025B1A
		internal SuccessfulFastTransferGetIncrementalStateResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00027923 File Offset: 0x00025B23
		internal static SuccessfulFastTransferGetIncrementalStateResult Parse(Reader reader)
		{
			return new SuccessfulFastTransferGetIncrementalStateResult(reader);
		}
	}
}
