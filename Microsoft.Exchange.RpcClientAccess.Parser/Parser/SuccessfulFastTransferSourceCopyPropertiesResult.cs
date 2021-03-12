using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000243 RID: 579
	internal sealed class SuccessfulFastTransferSourceCopyPropertiesResult : RopResult
	{
		// Token: 0x06000C9B RID: 3227 RVA: 0x00027965 File Offset: 0x00025B65
		internal SuccessfulFastTransferSourceCopyPropertiesResult(IServerObject fastTransferDownloadObject) : base(RopId.FastTransferSourceCopyProperties, ErrorCode.None, fastTransferDownloadObject)
		{
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00027971 File Offset: 0x00025B71
		internal SuccessfulFastTransferSourceCopyPropertiesResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0002797A File Offset: 0x00025B7A
		internal static SuccessfulFastTransferSourceCopyPropertiesResult Parse(Reader reader)
		{
			return new SuccessfulFastTransferSourceCopyPropertiesResult(reader);
		}
	}
}
