using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000244 RID: 580
	internal sealed class SuccessfulFastTransferSourceCopyToResult : RopResult
	{
		// Token: 0x06000C9E RID: 3230 RVA: 0x00027982 File Offset: 0x00025B82
		internal SuccessfulFastTransferSourceCopyToResult(IServerObject fastTransferDownloadObject) : base(RopId.FastTransferSourceCopyTo, ErrorCode.None, fastTransferDownloadObject)
		{
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0002798E File Offset: 0x00025B8E
		internal SuccessfulFastTransferSourceCopyToResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00027997 File Offset: 0x00025B97
		internal static SuccessfulFastTransferSourceCopyToResult Parse(Reader reader)
		{
			return new SuccessfulFastTransferSourceCopyToResult(reader);
		}
	}
}
