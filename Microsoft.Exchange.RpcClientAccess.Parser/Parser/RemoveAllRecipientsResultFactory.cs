using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000118 RID: 280
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RemoveAllRecipientsResultFactory : StandardResultFactory
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x000107C6 File Offset: 0x0000E9C6
		internal RemoveAllRecipientsResultFactory() : base(RopId.RemoveAllRecipients)
		{
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000107D0 File Offset: 0x0000E9D0
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.RemoveAllRecipients, ErrorCode.None);
		}
	}
}
