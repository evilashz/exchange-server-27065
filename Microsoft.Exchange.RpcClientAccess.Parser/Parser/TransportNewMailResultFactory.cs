using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000144 RID: 324
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TransportNewMailResultFactory : StandardResultFactory
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x0001106A File Offset: 0x0000F26A
		internal TransportNewMailResultFactory() : base(RopId.TransportNewMail)
		{
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00011074 File Offset: 0x0000F274
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.TransportNewMail, ErrorCode.None);
		}
	}
}
