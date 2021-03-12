using System;
using Microsoft.Exchange.Transport.Categorizer;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000054 RID: 84
	internal interface IDeliveryProcessor
	{
		// Token: 0x06000372 RID: 882
		void Initialize();

		// Token: 0x06000373 RID: 883
		DeliverableItem CreateSession();

		// Token: 0x06000374 RID: 884
		void CreateMessage(DeliverableItem item);

		// Token: 0x06000375 RID: 885
		void DeliverMessage();
	}
}
