using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020001A5 RID: 421
	internal interface IQueueItem
	{
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600120B RID: 4619
		// (set) Token: 0x0600120C RID: 4620
		DateTime DeferUntil { get; set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600120D RID: 4621
		DateTime Expiry { get; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600120E RID: 4622
		// (set) Token: 0x0600120F RID: 4623
		DeliveryPriority Priority { get; set; }

		// Token: 0x06001210 RID: 4624
		void Update();

		// Token: 0x06001211 RID: 4625
		MessageContext GetMessageContext(MessageProcessingSource source);
	}
}
