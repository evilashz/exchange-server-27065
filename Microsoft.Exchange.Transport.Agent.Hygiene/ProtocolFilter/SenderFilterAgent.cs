using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.ProtocolFilter;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.Transport.Agent.ProtocolFilter
{
	// Token: 0x02000024 RID: 36
	internal sealed class SenderFilterAgent : SmtpReceiveAgent
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00007E24 File Offset: 0x00006024
		public SenderFilterAgent(SenderFilterConfig senderFilterConfig, BlockedSenders blockedSenders, AddressBook addressBook)
		{
			this.senderFilterConfig = senderFilterConfig;
			this.blockedSenders = blockedSenders;
			this.addressBook = addressBook;
			base.OnMailCommand += this.MailFromHandler;
			base.OnEndOfHeaders += this.EndOfHeadersHandler;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007E70 File Offset: 0x00006070
		private void MailFromHandler(ReceiveCommandEventSource source, MailCommandEventArgs args)
		{
			if (this.IsPolicyDisabled(args.SmtpSession))
			{
				return;
			}
			if (CommonUtils.HasAntispamBypassPermission(args.SmtpSession, ExTraceGlobals.SenderFilterAgentTracer, this))
			{
				return;
			}
			this.messageId = string.Empty;
			this.p1FromIsBlocked = false;
			this.mailFromAddress = args.FromAddress;
			this.messageFiltered = false;
			Util.PerformanceCounters.SenderFilter.MessageEvaluatedBySenderFilter();
			LogEntry logEntry = null;
			if (this.mailFromAddress.Length > 0 && this.blockedSenders.IsBlocked(this.mailFromAddress, out logEntry))
			{
				ExTraceGlobals.SenderFilterAgentTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Sender {0} matches a blocked sender on MAIL FROM", this.mailFromAddress);
				this.p1FromIsBlocked = this.BlockSenderOnMailFrom(source, args, logEntry);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00007F1C File Offset: 0x0000611C
		private void EndOfHeadersHandler(ReceiveMessageEventSource source, EndOfHeadersEventArgs args)
		{
			this.CacheMessageId(args.Headers);
			if (this.IsPolicyDisabled(args.SmtpSession))
			{
				return;
			}
			if (CommonUtils.HasAntispamBypassPermission(args.SmtpSession, ExTraceGlobals.SenderFilterAgentTracer, this))
			{
				return;
			}
			if (this.p1FromIsBlocked)
			{
				return;
			}
			IList<RoutingAddress> senderAddresses = SenderFilterAgent.GetSenderAddresses(args.Headers);
			bool flag;
			if (senderAddresses.Count > 0)
			{
				flag = this.EnforceOrganizationBlockedSenders(source, args, senderAddresses);
			}
			else
			{
				flag = this.EnforceBlankSenderBlocking(source, args);
			}
			if (!flag)
			{
				RoutingAddress sender = (senderAddresses.Count > 0) ? senderAddresses[0] : RoutingAddress.Empty;
				this.EnforcePerRecipientBlockedSenders(source, args, sender);
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007FB4 File Offset: 0x000061B4
		private bool EnforceOrganizationBlockedSenders(ReceiveMessageEventSource source, EndOfHeadersEventArgs e, IEnumerable<RoutingAddress> senders)
		{
			foreach (RoutingAddress routingAddress in senders)
			{
				LogEntry logEntry;
				if (!routingAddress.Equals(this.mailFromAddress) && this.blockedSenders.IsBlocked(routingAddress, out logEntry))
				{
					ExTraceGlobals.SenderFilterAgentTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Sender {0} matches a block sender at EOH", routingAddress);
					return this.BlockSenderOnEOH(source, e, logEntry);
				}
			}
			return false;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000803C File Offset: 0x0000623C
		private bool EnforceBlankSenderBlocking(ReceiveMessageEventSource source, EndOfHeadersEventArgs e)
		{
			if (this.senderFilterConfig.BlankSenderBlockingEnabled && this.mailFromAddress == RoutingAddress.NullReversePath)
			{
				ExTraceGlobals.SenderFilterAgentTracer.TraceDebug((long)this.GetHashCode(), "Sender is unknown");
				return this.BlockSenderOnEOH(source, e, SenderFilterAgent.RejectContext.SenderNotSpecified);
			}
			return false;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00008090 File Offset: 0x00006290
		private void EnforcePerRecipientBlockedSenders(ReceiveMessageEventSource source, EndOfHeadersEventArgs e, RoutingAddress sender)
		{
			if (e.MailItem == null || e.MailItem.Recipients == null)
			{
				ExTraceGlobals.SenderFilterAgentTracer.TraceError((long)this.GetHashCode(), "Either MailItem or MailItem.Recipients is null in EnforcePerRecipientBlockedSenders()");
				return;
			}
			if (this.addressBook == null)
			{
				ExTraceGlobals.SenderFilterAgentTracer.TraceError<string>((long)this.GetHashCode(), "Cannot enforce per-recipient blocked senders for message {0} because address book is empty", this.messageId);
				return;
			}
			if (RoutingAddress.Empty.Equals(sender))
			{
				ExTraceGlobals.SenderFilterAgentTracer.TraceError<string>((long)this.GetHashCode(), "Cannot determine the sender of message {0}. Per-recipient blocked senders cannot be enforced.", this.messageId);
				return;
			}
			int count = e.MailItem.Recipients.Count;
			List<EnvelopeRecipient> list = new List<EnvelopeRecipient>(count);
			ReadOnlyCollection<AddressBookEntry> readOnlyCollection;
			AddressBookFindStatus addressBookFindStatus;
			if (CommonUtils.TryAddressBookFind(this.addressBook, e.MailItem.Recipients, out readOnlyCollection, out addressBookFindStatus))
			{
				if (readOnlyCollection == null)
				{
					string formatString = "Cannot enforce per-recipient blocked senders for message {0} because address book items could not be found";
					ExTraceGlobals.SenderFilterAgentTracer.TraceError<string>((long)this.GetHashCode(), formatString, this.messageId);
					return;
				}
			}
			else if (addressBookFindStatus == AddressBookFindStatus.TransientFailure)
			{
				string formatString2 = "Cannot enforce per-recipient blocked senders for message {0} because address book items could not be found due to a transient error.";
				ExTraceGlobals.SenderFilterAgentTracer.TraceDebug<string>((long)this.GetHashCode(), formatString2, this.messageId);
				AgentLog.Instance.LogRejectMessage(base.Name, base.EventTopic, e, e.SmtpSession, e.MailItem, SmtpResponse.DataTransactionFailed, SenderFilterAgent.RejectContext.TransientFailure);
				source.RejectMessage(SmtpResponse.DataTransactionFailed);
				return;
			}
			ICollection<RoutingAddress> p2RecipientsForSafeRecipientsCheck = SenderFilterAgent.GetP2RecipientsForSafeRecipientsCheck(e.Headers);
			for (int i = 0; i < count; i++)
			{
				EnvelopeRecipient item = e.MailItem.Recipients[i];
				AddressBookEntry addressBookEntry = readOnlyCollection[i];
				if (addressBookEntry != null && addressBookEntry.IsBlockedSender(sender) && !addressBookEntry.IsSafeSender(sender) && !CommonUtils.IsSafeRecipient(addressBookEntry, p2RecipientsForSafeRecipientsCheck))
				{
					list.Add(item);
					Util.PerformanceCounters.SenderFilter.SenderBlockedDueToPerRecipientBlockedSender();
				}
			}
			if (list.Count > 0)
			{
				LogEntry logEntry = SenderFilterAgent.RejectContext.PerRecipientBlockedSender(sender.ToString());
				SmtpResponse smtpResponse = SenderFilterAgent.RejectSenderResponse;
				switch (this.senderFilterConfig.RecipientBlockedSenderAction)
				{
				case RecipientBlockedSenderAction.Reject:
					break;
				case RecipientBlockedSenderAction.Delete:
					if (list.Count == count)
					{
						this.MessageFilteredBySenderFilter();
						smtpResponse = this.GetDeleteResponse(e.Headers);
						ExTraceGlobals.SenderFilterAgentTracer.TraceDebug<string, SmtpResponse>((long)this.GetHashCode(), "Silently deleting entire message {0} with response '{1}'", this.messageId, smtpResponse);
						AgentLog.Instance.LogDeleteMessage(base.Name, base.EventTopic, e, e.SmtpSession, e.MailItem, logEntry);
						source.RejectMessage(smtpResponse);
						return;
					}
					ExTraceGlobals.SenderFilterAgentTracer.TraceDebug<int, string>((long)this.GetHashCode(), "Silently deleting {0} recipients from message {1}", list.Count, this.messageId);
					AgentLog.Instance.LogDeleteRecipients(base.Name, base.EventTopic, e, e.SmtpSession, e.MailItem, list, logEntry);
					using (List<EnvelopeRecipient>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							EnvelopeRecipient recipient = enumerator.Current;
							e.MailItem.Recipients.Remove(recipient);
						}
						return;
					}
					break;
				default:
					goto IL_3C3;
				}
				if (list.Count == count)
				{
					this.MessageFilteredBySenderFilter();
					ExTraceGlobals.SenderFilterAgentTracer.TraceDebug<string, SmtpResponse>((long)this.GetHashCode(), "Rejecting message {0} with response '{1}'", this.messageId, smtpResponse);
					AgentLog.Instance.LogRejectMessage(base.Name, base.EventTopic, e, e.SmtpSession, e.MailItem, smtpResponse, logEntry);
					source.RejectMessage(smtpResponse);
					return;
				}
				ExTraceGlobals.SenderFilterAgentTracer.TraceDebug<int, string>((long)this.GetHashCode(), "Removing and NDR'ing {0} recipients of message {1}", list.Count, this.messageId);
				AgentLog.Instance.LogRejectRecipients(base.Name, base.EventTopic, e, e.SmtpSession, e.MailItem, list, smtpResponse, logEntry);
				using (List<EnvelopeRecipient>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						EnvelopeRecipient recipient2 = enumerator2.Current;
						e.MailItem.Recipients.Remove(recipient2, DsnType.Failure, smtpResponse);
					}
					return;
				}
				IL_3C3:
				ExTraceGlobals.SenderFilterAgentTracer.TraceError((long)this.GetHashCode(), "unknown RecipientBlockedSenderAction.");
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00008494 File Offset: 0x00006694
		private bool IsPolicyDisabled(SmtpSession session)
		{
			if (!CommonUtils.IsEnabled(this.senderFilterConfig, session))
			{
				ExTraceGlobals.SenderFilterAgentTracer.TraceDebug((long)this.GetHashCode(), "Sender filter policy is disabled");
				return true;
			}
			return false;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000084C0 File Offset: 0x000066C0
		private static IList<RoutingAddress> GetSenderAddresses(HeaderList headers)
		{
			List<RoutingAddress> list = new List<RoutingAddress>();
			foreach (Header header in headers.FindAll(HeaderId.Sender))
			{
				list.AddRange(SenderFilterAgent.GetRoutingAddresses(header as AddressHeader));
			}
			foreach (Header header2 in headers.FindAll(HeaderId.From))
			{
				list.AddRange(SenderFilterAgent.GetRoutingAddresses(header2 as AddressHeader));
			}
			return list;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00008538 File Offset: 0x00006738
		private static ICollection<RoutingAddress> GetP2RecipientsForSafeRecipientsCheck(HeaderList headers)
		{
			List<RoutingAddress> list = new List<RoutingAddress>();
			foreach (Header header in headers.FindAll(HeaderId.To))
			{
				list.AddRange(SenderFilterAgent.GetRoutingAddresses(header as AddressHeader));
			}
			foreach (Header header2 in headers.FindAll(HeaderId.Cc))
			{
				list.AddRange(SenderFilterAgent.GetRoutingAddresses(header2 as AddressHeader));
			}
			return list;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008838 File Offset: 0x00006A38
		private static IEnumerable<RoutingAddress> GetRoutingAddresses(AddressHeader header)
		{
			if (header != null)
			{
				foreach (AddressItem item in header)
				{
					MimeRecipient recipient = item as MimeRecipient;
					if (recipient != null)
					{
						if (!string.IsNullOrEmpty(recipient.Email) && RoutingAddress.IsValidAddress(recipient.Email))
						{
							yield return new RoutingAddress(recipient.Email);
						}
					}
					else
					{
						MimeGroup group = item as MimeGroup;
						if (group != null)
						{
							recipient = (group.FirstChild as MimeRecipient);
							if (recipient != null && !string.IsNullOrEmpty(recipient.Email) && RoutingAddress.IsValidAddress(recipient.Email))
							{
								yield return new RoutingAddress(recipient.Email);
							}
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00008858 File Offset: 0x00006A58
		private bool BlockSenderOnMailFrom(ReceiveCommandEventSource source, MailCommandEventArgs args, LogEntry logEntry)
		{
			this.MessageFilteredBySenderFilter();
			SenderFilterAgent.StampSenderIsBlocked(args);
			switch (this.senderFilterConfig.Action)
			{
			case BlockedSenderAction.StampStatus:
				return false;
			case BlockedSenderAction.Reject:
				ExTraceGlobals.SenderFilterAgentTracer.TraceDebug((long)this.GetHashCode(), "MAIL FROM rejected");
				AgentLog.Instance.LogRejectCommand(base.Name, base.EventTopic, args, SenderFilterAgent.RejectSenderResponse, logEntry);
				source.RejectCommand(SenderFilterAgent.RejectSenderResponse);
				return true;
			default:
				ExTraceGlobals.SenderFilterAgentTracer.TraceError<BlockedSenderAction>((long)this.GetHashCode(), "Invalid Action: {0}", this.senderFilterConfig.Action);
				return false;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000088F4 File Offset: 0x00006AF4
		private bool BlockSenderOnEOH(ReceiveMessageEventSource source, EndOfHeadersEventArgs args, LogEntry logEntry)
		{
			this.MessageFilteredBySenderFilter();
			SenderFilterAgent.StampSenderIsBlocked(args);
			switch (this.senderFilterConfig.Action)
			{
			case BlockedSenderAction.StampStatus:
				return false;
			case BlockedSenderAction.Reject:
				ExTraceGlobals.SenderFilterAgentTracer.TraceDebug((long)this.GetHashCode(), "Message rejected");
				AgentLog.Instance.LogRejectMessage(base.Name, base.EventTopic, args, args.SmtpSession, args.MailItem, SenderFilterAgent.RejectSenderResponse, logEntry);
				source.RejectMessage(SenderFilterAgent.RejectSenderResponse);
				return true;
			default:
				ExTraceGlobals.SenderFilterAgentTracer.TraceError<BlockedSenderAction>((long)this.GetHashCode(), "Invalid Action: {0}", this.senderFilterConfig.Action);
				return false;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008999 File Offset: 0x00006B99
		private static void StampSenderIsBlocked(EndOfHeadersEventArgs e)
		{
			e.MailItem.Properties["Microsoft.Exchange.SenderBlocked"] = true;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000089B6 File Offset: 0x00006BB6
		private static void StampSenderIsBlocked(MailCommandEventArgs e)
		{
			e.MailItemProperties["Microsoft.Exchange.SenderBlocked"] = true;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000089CE File Offset: 0x00006BCE
		private void MessageFilteredBySenderFilter()
		{
			if (!this.messageFiltered)
			{
				Util.PerformanceCounters.SenderFilter.MessageFilteredBySenderFilter();
				this.messageFiltered = true;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000089E4 File Offset: 0x00006BE4
		private SmtpResponse GetDeleteResponse(HeaderList headers)
		{
			string value = this.messageId;
			if (string.IsNullOrEmpty(value))
			{
				ExTraceGlobals.SenderFilterAgentTracer.TraceError(0L, "Message-ID header is null/empty. Transport is supposed to normalize this header before End-of-Headers is raised.");
				value = "<" + Guid.NewGuid().ToString() + ">";
			}
			return SmtpResponse.QueuedMailForDelivery(value);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008A3C File Offset: 0x00006C3C
		private void CacheMessageId(HeaderList headers)
		{
			this.messageId = string.Empty;
			if (headers != null)
			{
				Header header = headers.FindFirst(HeaderId.MessageId);
				if (header != null)
				{
					this.messageId = (header.Value ?? string.Empty);
				}
			}
		}

		// Token: 0x040000EB RID: 235
		private static readonly SmtpResponse RejectSenderResponse = new SmtpResponse("554", "5.1.0", new string[]
		{
			"Sender denied"
		});

		// Token: 0x040000EC RID: 236
		private SenderFilterConfig senderFilterConfig;

		// Token: 0x040000ED RID: 237
		private BlockedSenders blockedSenders;

		// Token: 0x040000EE RID: 238
		private RoutingAddress mailFromAddress;

		// Token: 0x040000EF RID: 239
		private bool p1FromIsBlocked;

		// Token: 0x040000F0 RID: 240
		private AddressBook addressBook;

		// Token: 0x040000F1 RID: 241
		private string messageId;

		// Token: 0x040000F2 RID: 242
		private bool messageFiltered;

		// Token: 0x02000025 RID: 37
		internal static class RejectContext
		{
			// Token: 0x060000ED RID: 237 RVA: 0x00008AA9 File Offset: 0x00006CA9
			public static LogEntry ExactMatch(string blockingEntry)
			{
				return new LogEntry("ExactMatch", blockingEntry);
			}

			// Token: 0x060000EE RID: 238 RVA: 0x00008AB6 File Offset: 0x00006CB6
			public static LogEntry DomainMatch(string blockingEntry)
			{
				return new LogEntry("DomainMatch", blockingEntry);
			}

			// Token: 0x060000EF RID: 239 RVA: 0x00008AC3 File Offset: 0x00006CC3
			public static LogEntry SubdomainMatch(string blockingEntry)
			{
				return new LogEntry("SubdomainMatch", blockingEntry);
			}

			// Token: 0x060000F0 RID: 240 RVA: 0x00008AD0 File Offset: 0x00006CD0
			public static LogEntry PerRecipientBlockedSender(string blockingEntry)
			{
				return new LogEntry("PerRecipientBlockedSender", blockingEntry);
			}

			// Token: 0x040000F3 RID: 243
			public static readonly LogEntry SenderNotSpecified = new LogEntry("SenderNotSpecified", string.Empty);

			// Token: 0x040000F4 RID: 244
			public static readonly LogEntry TransientFailure = new LogEntry("TransientFailure", string.Empty);
		}
	}
}
