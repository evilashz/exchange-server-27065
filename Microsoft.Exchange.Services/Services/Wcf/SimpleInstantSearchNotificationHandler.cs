using System;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009D4 RID: 2516
	internal class SimpleInstantSearchNotificationHandler : IInstantSearchNotificationHandler
	{
		// Token: 0x06004736 RID: 18230 RVA: 0x000FEA5E File Offset: 0x000FCC5E
		public SimpleInstantSearchNotificationHandler(Action<InstantSearchPayloadType> searchPayloadCallback)
		{
			this.searchPayloadCallback = searchPayloadCallback;
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x000FEA6D File Offset: 0x000FCC6D
		public void DeliverInstantSearchPayload(InstantSearchPayloadType instantSearchPayload)
		{
			this.searchPayloadCallback(instantSearchPayload);
		}

		// Token: 0x040028E1 RID: 10465
		private Action<InstantSearchPayloadType> searchPayloadCallback;
	}
}
