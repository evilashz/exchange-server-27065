using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport.Agent.SharedMailboxSentItemsRoutingAgent
{
	// Token: 0x0200000A RID: 10
	internal sealed class SharedMailboxSentItemsRoutingAgent : RoutingAgent
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002484 File Offset: 0x00000684
		internal SharedMailboxSentItemsRoutingAgent(ISharedMailboxConfigurationFactory configurationFactory, ISentItemWrapperCreator wrapperCreator, ITracer tracer)
		{
			if (configurationFactory == null)
			{
				throw new ArgumentNullException("configurationFactory");
			}
			if (tracer == null)
			{
				throw new ArgumentNullException("tracer");
			}
			if (wrapperCreator == null)
			{
				throw new ArgumentNullException("wrapperCreator");
			}
			this.tracer = tracer;
			this.wrapperCreator = wrapperCreator;
			this.traceId = this.GetHashCode();
			this.configurationFactory = configurationFactory;
			base.OnSubmittedMessage += this.OnSubmittedMessageHandler;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024F4 File Offset: 0x000006F4
		internal void OnSubmittedMessageHandler(SubmittedMessageEventSource source, QueuedMessageEventArgs args)
		{
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).SharedMailbox.SharedMailboxSentItemsRoutingAgent.Enabled)
			{
				this.tracer.TraceDebug((long)this.traceId, "SharedMailboxSentItemsRoutingAgent flight is not enabled. Exiting.");
				return;
			}
			this.HandleSubmittedMessage(new EventSource(source), args.MailItem);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000254C File Offset: 0x0000074C
		internal void HandleSubmittedMessage(IEventSource source, MailItem mailItem)
		{
			if (mailItem == null || mailItem.Message == null || mailItem.Message.MapiMessageClass == null)
			{
				this.tracer.TraceDebug((long)this.traceId, "Cannot find the message class for the current message. Exiting.");
				return;
			}
			if (!ObjectClass.IsOfClass(mailItem.Message.MapiMessageClass, "IPM.Note"))
			{
				this.tracer.TraceDebug((long)this.traceId, "Current message is not Ipmnote. Exiting.");
				return;
			}
			if (mailItem.Message != null && mailItem.Message.RootPart != null && mailItem.Message.RootPart.Headers != null)
			{
				Header header = mailItem.Message.RootPart.Headers.FindFirst("X-MS-Exchange-SharedMailbox-RoutingAgent-Processed");
				if (header != null && string.Equals("True", header.Value, StringComparison.OrdinalIgnoreCase))
				{
					this.tracer.TraceDebug((long)this.traceId, "Message has been already been processed by the agent. Exiting.");
					return;
				}
			}
			int messageRetryCount = SharedMailboxSentItemsRoutingAgent.GetMessageRetryCount(mailItem.Properties);
			try
			{
				if (!this.ShouldCopyMessageToSentItemsOfSharedMailbox(mailItem))
				{
					this.tracer.TraceDebug((long)this.traceId, "Configuration indicates that the message need not be copied to the shared mailbox. Exiting.");
				}
				else
				{
					Exception ex = this.wrapperCreator.CreateAndSubmit(mailItem, this.traceId);
					if (ex != null)
					{
						this.tracer.TraceError((long)this.traceId, "error occured in sending the wrapper message:" + ex);
						SharedMailboxSentItemsRoutingAgent.DeferMessageForRetry(source, mailItem, messageRetryCount, SharedMailboxSentItemsRoutingAgent.FailedSendMessage);
					}
					else
					{
						mailItem.Message.RootPart.Headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-SharedMailbox-RoutingAgent-Processed", "True"));
					}
				}
			}
			catch (Exception ex2)
			{
				if (ex2 is OutOfMemoryException || ex2 is StackOverflowException || ex2 is ThreadAbortException)
				{
					throw;
				}
				this.tracer.TraceError((long)this.traceId, "Unhandled exception occured :" + ex2);
				SharedMailboxSentItemsRoutingAgent.DeferMessageForRetry(source, mailItem, messageRetryCount, SharedMailboxSentItemsRoutingAgent.ErrorOccuredInProcessing);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000271C File Offset: 0x0000091C
		private static int GetMessageRetryCount(IDictionary<string, object> properties)
		{
			object obj;
			if (properties.TryGetValue("SharedMailboxSentItemsRoutingAgent.RetryCount", out obj) && obj is int)
			{
				return (int)obj;
			}
			return 0;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002748 File Offset: 0x00000948
		private static void DeferMessageForRetry(IEventSource source, MailItem mailItem, int currentMessageRetryCount, SmtpResponse reason)
		{
			if (currentMessageRetryCount < 12)
			{
				mailItem.Properties["SharedMailboxSentItemsRoutingAgent.RetryCount"] = currentMessageRetryCount + 1;
				source.Defer(SharedMailboxSentItemsRoutingAgent.DeferTimeout, reason);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002774 File Offset: 0x00000974
		private MessageSentRepresentingFlags GetMessageSentRepresentingType(EmailMessage emailMessage)
		{
			if (emailMessage == null || emailMessage.RootPart == null || emailMessage.RootPart.Headers == null)
			{
				return MessageSentRepresentingFlags.None;
			}
			Header header = emailMessage.RootPart.Headers.FindFirst("X-MS-Exchange-MessageSentRepresentingType");
			if (header == null || string.IsNullOrEmpty(header.Value))
			{
				return MessageSentRepresentingFlags.None;
			}
			int num;
			if (!int.TryParse(header.Value, out num))
			{
				return MessageSentRepresentingFlags.None;
			}
			MessageSentRepresentingFlags messageSentRepresentingFlags = (MessageSentRepresentingFlags)num;
			if (!Enum.IsDefined(typeof(MessageSentRepresentingFlags), messageSentRepresentingFlags))
			{
				return MessageSentRepresentingFlags.None;
			}
			return messageSentRepresentingFlags;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000027F0 File Offset: 0x000009F0
		private bool ShouldCopyMessageToSentItemsOfSharedMailbox(MailItem transportMailItem)
		{
			MessageSentRepresentingFlags messageSentRepresentingType = this.GetMessageSentRepresentingType(transportMailItem.Message);
			if (messageSentRepresentingType == MessageSentRepresentingFlags.None)
			{
				this.tracer.TraceDebug((long)this.traceId, "Message is not sent as or on-behalf of another user.");
				return false;
			}
			this.tracer.TraceDebug((long)this.traceId, "Message is sent as another user. SentRepresentingFlags value : " + messageSentRepresentingType.ToString());
			SharedMailboxConfiguration sharedMailboxConfiguration = this.configurationFactory.GetSharedMailboxConfiguration(transportMailItem, transportMailItem.FromAddress.ToString());
			if (!sharedMailboxConfiguration.IsSharedMailbox)
			{
				this.tracer.TraceDebug((long)this.traceId, "Message sender is not a shared mailbox.");
				return false;
			}
			this.tracer.TraceDebug((long)this.traceId, string.Concat(new object[]
			{
				"Sharedmailbox sent item configuration. SentAsBehavior:",
				sharedMailboxConfiguration.SentAsBehavior,
				" SentOnBehalfOfBehavior:",
				sharedMailboxConfiguration.SentOnBehalfOfBehavior
			}));
			return (messageSentRepresentingType != MessageSentRepresentingFlags.SendAs || sharedMailboxConfiguration.SentAsBehavior == SharedMailboxSentItemBehavior.CopyToSharedMailbox) && (messageSentRepresentingType != MessageSentRepresentingFlags.SendOnBehalfOf || sharedMailboxConfiguration.SentOnBehalfOfBehavior == SharedMailboxSentItemBehavior.CopyToSharedMailbox);
		}

		// Token: 0x04000009 RID: 9
		internal const string AgentName = "SharedMailboxSentItemsRoutingAgent";

		// Token: 0x0400000A RID: 10
		internal const string RetryCountKey = "SharedMailboxSentItemsRoutingAgent.RetryCount";

		// Token: 0x0400000B RID: 11
		internal const string AgentProcessedHeaderValue = "True";

		// Token: 0x0400000C RID: 12
		private const int MaxRetryCount = 12;

		// Token: 0x0400000D RID: 13
		private static readonly TimeSpan DeferTimeout = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400000E RID: 14
		private static readonly SmtpResponse FailedSendMessage = new SmtpResponse("452", "4.3.2", new string[]
		{
			"Failed to send the message"
		});

		// Token: 0x0400000F RID: 15
		private static readonly SmtpResponse ErrorOccuredInProcessing = new SmtpResponse("452", "4.3.2", new string[]
		{
			"Failed to process the message. Should retry."
		});

		// Token: 0x04000010 RID: 16
		private readonly ITracer tracer;

		// Token: 0x04000011 RID: 17
		private readonly ISharedMailboxConfigurationFactory configurationFactory;

		// Token: 0x04000012 RID: 18
		private readonly ISentItemWrapperCreator wrapperCreator;

		// Token: 0x04000013 RID: 19
		private readonly int traceId;
	}
}
