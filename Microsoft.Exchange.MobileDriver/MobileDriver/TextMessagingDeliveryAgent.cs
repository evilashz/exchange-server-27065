using System;
using Microsoft.Exchange.Data.Transport.Delivery;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000048 RID: 72
	public class TextMessagingDeliveryAgent : DeliveryAgent
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x0000A230 File Offset: 0x00008430
		internal TextMessagingDeliveryAgent(MobileSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			this.session = session;
			base.OnOpenConnection += this.TextMessagingOpenConnectionEventHandler;
			base.OnDeliverMailItem += this.TextMessagingDeliverMailItemEventHandler;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000A27C File Offset: 0x0000847C
		internal static void TextMessagingDeliverMailItemEventCompletionHandler(QueueDataAvailableEventSource<TextMessageDeliveryContext> src, QueueDataAvailableEventArgs<TextMessageDeliveryContext> e)
		{
			e.Item.AgentWrapper.CompleteAsyncEvent();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000A290 File Offset: 0x00008490
		public void TextMessagingOpenConnectionEventHandler(OpenConnectionEventSource source, OpenConnectionEventArgs e)
		{
			source.RegisterConnection("text.messaging.delivery.external", new SmtpResponse("250", string.Empty, new string[]
			{
				"OK"
			}));
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000A2C8 File Offset: 0x000084C8
		public void TextMessagingDeliverMailItemEventHandler(DeliverMailItemEventSource source, DeliverMailItemEventArgs e)
		{
			TransportAgentWrapper agentWrapper = new TransportAgentWrapper(base.GetAgentAsyncContext(), source, e);
			this.session.Send(agentWrapper, new QueueDataAvailableEventHandler<TextMessageDeliveryContext>(TextMessagingDeliveryAgent.TextMessagingDeliverMailItemEventCompletionHandler));
		}

		// Token: 0x04000103 RID: 259
		private MobileSession session;
	}
}
