using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.MailboxTransport.StoreDriverDelivery;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;

namespace Microsoft.Exchange.MailboxTransport.Delivery
{
	// Token: 0x02000005 RID: 5
	internal class DeliveryListener : IMbxDeliveryListener, ICategorizer
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000028A7 File Offset: 0x00000AA7
		internal DeliveryListener()
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000028AF File Offset: 0x00000AAF
		public SmtpResponse EnqueueSubmittedMessage(TransportMailItem mailItem)
		{
			return this.HandleAcceptedMessage(mailItem);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000028B8 File Offset: 0x00000AB8
		public void SetLoadTimeDependencies(IProcessingQuotaComponent processingQuotaComponent, IMessageDepotComponent messageDepotComponent)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000028BA File Offset: 0x00000ABA
		private SmtpResponse HandleAcceptedMessage(TransportMailItem mailItem)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem", "TransportMailItem is null");
			}
			return Components.StoreDriverDelivery.DoLocalDelivery(mailItem);
		}
	}
}
