﻿using System;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.SendAs
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SendAsRoutingAgent : RoutingAgent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public SendAsRoutingAgent(SendAsManager sendAsManager)
		{
			this.sendAsManager = sendAsManager;
			base.OnCategorizedMessage += this.OnCategorizedMessageHandler;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020F1 File Offset: 0x000002F1
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.SendAsTracer;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F8 File Offset: 0x000002F8
		private void OnCategorizedMessageHandler(CategorizedMessageEventSource source, QueuedMessageEventArgs e)
		{
			SendAsManager.SendAsProperties sendAsProperties;
			if (this.sendAsManager.TryLoadSubscriptionProperties(e.MailItem.Properties, out sendAsProperties))
			{
				if (!sendAsProperties.IsValid)
				{
					SendAsRoutingAgent.Tracer.TraceError<string>((long)this.GetHashCode(), "Rejecting message with invalid send as properties. Message id: {0}", e.MailItem.Message.MessageId);
					this.RejectMessage(e.MailItem, SendAsRoutingAgent.SendAsPropertiesModified);
					return;
				}
				SendAsRoutingAgent.Tracer.TraceDebug<AggregationSubscriptionType, string, Guid>((long)this.GetHashCode(), "Modifying headers for {0} send as message. Message id: {1}, subscription id: {2}", sendAsProperties.SubscriptionType, e.MailItem.Message.MessageId, sendAsProperties.SubscriptionGuid);
				HeaderList headers = e.MailItem.Message.RootPart.Headers;
				AddressHeader addressHeader = headers.FindFirst(HeaderId.Sender) as AddressHeader;
				AddressHeader addressHeader2 = headers.FindFirst(HeaderId.From) as AddressHeader;
				AddressHeader addressHeader3 = addressHeader ?? addressHeader2;
				MimeRecipient mimeRecipient = (addressHeader3 != null) ? (addressHeader3.FirstChild as MimeRecipient) : null;
				if (mimeRecipient != null && !string.IsNullOrEmpty(mimeRecipient.Email) && sendAsProperties.UserEmailAddress.IsValidAddress)
				{
					this.sendAsManager.SetSendAsHeaders(headers, mimeRecipient.Email, mimeRecipient.DisplayName, sendAsProperties.UserEmailAddress.ToString(), sendAsProperties.UserDisplayName);
					return;
				}
				SendAsRoutingAgent.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Rejecting message with no From or Sender header. Message id: {0}", e.MailItem.Message.MessageId);
				this.RejectMessage(e.MailItem, SendAsRoutingAgent.InvalidSubscription);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002278 File Offset: 0x00000478
		private void RejectMessage(MailItem mailItem, SmtpResponse smtpResponse)
		{
			foreach (EnvelopeRecipient recipient in mailItem.Recipients)
			{
				mailItem.Recipients.Remove(recipient, DsnType.Failure, smtpResponse);
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly SmtpResponse InvalidSubscription = new SmtpResponse("550", "5.6.0", new string[]
		{
			"Invalid subscription."
		});

		// Token: 0x04000002 RID: 2
		private static readonly SmtpResponse SendAsPropertiesModified = new SmtpResponse("550", "5.6.0", new string[]
		{
			"Subscription properties on message were modified. Unable to deliver."
		});

		// Token: 0x04000003 RID: 3
		private SendAsManager sendAsManager;
	}
}
