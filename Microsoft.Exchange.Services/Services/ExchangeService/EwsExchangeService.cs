using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DE4 RID: 3556
	internal class EwsExchangeService : ExchangeServiceBase
	{
		// Token: 0x06005C08 RID: 23560 RVA: 0x0011DDD3 File Offset: 0x0011BFD3
		public EwsExchangeService(CallContext callContext)
		{
			ArgumentValidator.ThrowIfNull("callContext", callContext);
			base.CallContext = callContext;
		}

		// Token: 0x06005C09 RID: 23561 RVA: 0x0011DDED File Offset: 0x0011BFED
		protected override void InternalDispose(bool disposing)
		{
		}
	}
}
