using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000056 RID: 86
	internal class DeliveryProcessorFactory
	{
		// Token: 0x0600037C RID: 892 RVA: 0x0000F1C8 File Offset: 0x0000D3C8
		public static IDeliveryProcessor Create(MailItemDeliver mailItemDeliver)
		{
			if (DeliveryProcessorFactory.InstanceBuilder != null)
			{
				return DeliveryProcessorFactory.InstanceBuilder();
			}
			bool value = mailItemDeliver.Recipient.ExtendedProperties.GetValue<bool>("Microsoft.Exchange.Transport.MailboxTransport.RetryOnDuplicateDelivery ", false);
			if (value)
			{
				return new RetryAgentMessageSubmissionProcessor(mailItemDeliver);
			}
			return new DeliveryProcessorBase(mailItemDeliver);
		}

		// Token: 0x040001C4 RID: 452
		public static DeliveryProcessorFactory.DeliverProcessorBuilder InstanceBuilder;

		// Token: 0x02000057 RID: 87
		// (Invoke) Token: 0x0600037F RID: 895
		public delegate IDeliveryProcessor DeliverProcessorBuilder();
	}
}
