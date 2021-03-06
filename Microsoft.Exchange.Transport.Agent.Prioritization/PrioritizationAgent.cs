using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Diagnostics.Components.StsUpdate;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Agent.Prioritization
{
	// Token: 0x02000002 RID: 2
	internal sealed class PrioritizationAgent : RoutingAgent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public PrioritizationAgent(MessagePrioritization prioritization)
		{
			if (prioritization != null)
			{
				this.prioritization = prioritization;
				base.OnResolvedMessage += this.OnResolvedMessageHandler;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F4 File Offset: 0x000002F4
		internal void OnResolvedMessageHandler(ResolvedMessageEventSource source, QueuedMessageEventArgs args)
		{
			MailItem mailItem = args.MailItem;
			RoutingAddress fromAddress = mailItem.FromAddress;
			if (!mailItem.FromAddress.IsValid)
			{
				return;
			}
			HeaderList headers = mailItem.MimeDocument.RootPart.Headers;
			Header header = headers.FindFirst("X-MS-Exchange-Organization-AuthAs");
			bool flag = header != null && header.Value == "Internal";
			if (flag && PrioritizationAgent.IsUmVoiceMessage(mailItem))
			{
				ExTraceGlobals.AgentTracer.TraceDebug<string>((long)this.GetHashCode(), "Skip prioritizing UM voice mail: {0}", mailItem.InternetMessageId ?? "NULL");
				return;
			}
			ITransportMailItemFacade transportMailItem = ((ITransportMailItemWrapperFacade)mailItem).TransportMailItem;
			if (transportMailItem.Priority != DeliveryPriority.Normal)
			{
				return;
			}
			string fromAddress2;
			if (mailItem.FromAddress == RoutingAddress.NullReversePath)
			{
				if (!string.IsNullOrEmpty(transportMailItem.PrioritizationReason))
				{
					return;
				}
				Header header2 = headers.FindFirst(HeaderId.From);
				if (header2 == null || string.IsNullOrEmpty(header2.Value))
				{
					header2 = headers.FindFirst(HeaderId.Sender);
				}
				if (header2 != null && !string.IsNullOrEmpty(header2.Value))
				{
					fromAddress2 = "P2@" + header2.Value;
				}
				else
				{
					fromAddress2 = RoutingAddress.NullReversePath.ToString();
				}
			}
			else
			{
				fromAddress2 = mailItem.FromAddress.ToString();
			}
			int count = mailItem.Recipients.Count;
			int num = mailItem.Message.To.Count + mailItem.Message.Cc.Count;
			DeliveryPriority priority;
			string prioritizationReason;
			this.prioritization.PrioritizeMessage(flag, fromAddress2, mailItem.CachedMimeStreamLength, count + num, mailItem, ExTraceGlobals.AgentTracer, out priority, out prioritizationReason);
			transportMailItem.PrioritizationReason = prioritizationReason;
			transportMailItem.Priority = priority;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000022A0 File Offset: 0x000004A0
		private static bool IsUmVoiceMessage(MailItem mailItem)
		{
			return TransportFacades.IsVoicemail(mailItem.Message) && (mailItem.FromAddress == GlobalConfigurationBase<MicrosoftExchangeRecipient, MicrosoftExchangeRecipientConfiguration>.Instance.Address || mailItem.MimeDocument.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-Mapi-Admin-Submission") != null);
		}

		// Token: 0x04000001 RID: 1
		private MessagePrioritization prioritization;
	}
}
