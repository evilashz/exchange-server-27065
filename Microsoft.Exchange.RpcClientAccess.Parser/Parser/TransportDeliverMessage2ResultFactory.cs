using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000140 RID: 320
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TransportDeliverMessage2ResultFactory : StandardResultFactory
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x00011007 File Offset: 0x0000F207
		internal TransportDeliverMessage2ResultFactory() : base(RopId.TransportDeliverMessage2)
		{
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00011014 File Offset: 0x0000F214
		public RopResult CreateSuccessfulResult(StoreId messageId)
		{
			return new SuccessfulTransportDeliverMessage2Result(messageId);
		}
	}
}
