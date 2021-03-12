using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000131 RID: 305
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SpoolerLockMessageResultFactory : StandardResultFactory
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x00010EDB File Offset: 0x0000F0DB
		internal SpoolerLockMessageResultFactory() : base(RopId.SpoolerLockMessage)
		{
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00010EE5 File Offset: 0x0000F0E5
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.SpoolerLockMessage, ErrorCode.None);
		}
	}
}
