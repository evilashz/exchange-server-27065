using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200004A RID: 74
	public sealed class TextMessagingRoutingAgent : RoutingAgent
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x0000A31B File Offset: 0x0000851B
		internal TextMessagingRoutingAgent(MobileSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			this.session = session;
			base.OnSubmittedMessage += this.TextMessagingSubmittedMessageEventHandler;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A34A File Offset: 0x0000854A
		internal static void TextMessagingSubmittedMessageEventCompletionHandler(QueueDataAvailableEventSource<TextMessageDeliveryContext> src, QueueDataAvailableEventArgs<TextMessageDeliveryContext> e)
		{
			e.Item.AgentWrapper.LogTrackingForTransportMailItem();
			e.Item.AgentWrapper.SetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Originator", "Agent:TextMessagingExternalDelivery-803AF8EC-8F1B-42b3-854D-8CA8E8CB04FC");
			e.Item.AgentWrapper.CompleteAsyncEvent();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000A388 File Offset: 0x00008588
		public void TextMessagingSubmittedMessageEventHandler(SubmittedMessageEventSource source, QueuedMessageEventArgs e)
		{
			bool flag = false;
			TransportAgentWrapper transportAgentWrapper;
			try
			{
				transportAgentWrapper = new TransportAgentWrapper(null, source, e);
				string b = null;
				if (transportAgentWrapper.TryGetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Originator", out b) && (string.Equals("Agent:TextMessagingInternalDelivery-86DB88E6-E880-4564-B1EC-25C9797FEBBE", b) || string.Equals("Agent:TextMessagingExternalDelivery-803AF8EC-8F1B-42b3-854D-8CA8E8CB04FC", b)))
				{
					return;
				}
				List<EnvelopeRecipient> list = new List<EnvelopeRecipient>();
				foreach (EnvelopeRecipient envelopeRecipient in transportAgentWrapper.ReadOnlyRecipients)
				{
					if (null != EmailMessageHelper.GetMobileProxyAddressFromImceaAddress((string)envelopeRecipient.Address))
					{
						list.Add(envelopeRecipient);
					}
				}
				if (list.Count == 0)
				{
					return;
				}
				transportAgentWrapper.SetAgentAsyncContext(base.GetAgentAsyncContext());
				transportAgentWrapper.ForkSubmission(list);
				transportAgentWrapper.SetTextHeader("X-MS-Exchange-Organization-Text-Messaging-Originator", "Agent:TextMessagingInternalDelivery-86DB88E6-E880-4564-B1EC-25C9797FEBBE");
				transportAgentWrapper.SetOnceMapiMessageClassToMimeHeader();
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					ExSmsCounters.PendingDelivery.Decrement();
				}
			}
			this.session.Send(transportAgentWrapper, new QueueDataAvailableEventHandler<TextMessageDeliveryContext>(TextMessagingRoutingAgent.TextMessagingSubmittedMessageEventCompletionHandler));
		}

		// Token: 0x04000105 RID: 261
		private MobileSession session;
	}
}
