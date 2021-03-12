using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200023D RID: 573
	internal sealed class SuccessfulFastTransferDestinationCopyOperationConfigureResult : RopResult
	{
		// Token: 0x06000C81 RID: 3201 RVA: 0x00027720 File Offset: 0x00025920
		internal SuccessfulFastTransferDestinationCopyOperationConfigureResult(IServerObject serverObject) : base(RopId.FastTransferDestinationCopyOperationConfigure, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002773A File Offset: 0x0002593A
		internal SuccessfulFastTransferDestinationCopyOperationConfigureResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00027743 File Offset: 0x00025943
		internal static SuccessfulFastTransferDestinationCopyOperationConfigureResult Parse(Reader reader)
		{
			return new SuccessfulFastTransferDestinationCopyOperationConfigureResult(reader);
		}
	}
}
