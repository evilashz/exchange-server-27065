using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000142 RID: 322
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TransportDoneWithMessageResultFactory : StandardResultFactory
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x00011036 File Offset: 0x0000F236
		internal TransportDoneWithMessageResultFactory() : base(RopId.TransportDoneWithMessage)
		{
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00011043 File Offset: 0x0000F243
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.TransportDoneWithMessage, ErrorCode.None);
		}
	}
}
