using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000141 RID: 321
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TransportDeliverMessageResultFactory : StandardResultFactory
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x0001101C File Offset: 0x0000F21C
		internal TransportDeliverMessageResultFactory() : base(RopId.TransportDeliverMessage)
		{
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00011029 File Offset: 0x0000F229
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.TransportDeliverMessage, ErrorCode.None);
		}
	}
}
