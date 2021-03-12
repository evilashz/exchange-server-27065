using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000FD RID: 253
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FlushRecipientsResultFactory : StandardResultFactory
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x0000F73A File Offset: 0x0000D93A
		internal FlushRecipientsResultFactory() : base(RopId.FlushRecipients)
		{
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000F744 File Offset: 0x0000D944
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.FlushRecipients, ErrorCode.None);
		}
	}
}
