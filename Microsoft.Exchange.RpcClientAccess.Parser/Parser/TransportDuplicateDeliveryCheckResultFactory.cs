using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000143 RID: 323
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TransportDuplicateDeliveryCheckResultFactory : StandardResultFactory
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x00011050 File Offset: 0x0000F250
		internal TransportDuplicateDeliveryCheckResultFactory() : base(RopId.TransportDuplicateDeliveryCheck)
		{
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001105D File Offset: 0x0000F25D
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.TransportDuplicateDeliveryCheck, ErrorCode.None);
		}
	}
}
