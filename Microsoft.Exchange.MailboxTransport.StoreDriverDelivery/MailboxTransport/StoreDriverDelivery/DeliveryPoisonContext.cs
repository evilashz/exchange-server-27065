using System;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200001B RID: 27
	internal class DeliveryPoisonContext : MessageContext
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x000096FB File Offset: 0x000078FB
		public DeliveryPoisonContext(string internetMessageId) : base(0L, internetMessageId, MessageProcessingSource.StoreDriverLocalDelivery)
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00009707 File Offset: 0x00007907
		public override string ToString()
		{
			return base.InternetMessageId;
		}
	}
}
