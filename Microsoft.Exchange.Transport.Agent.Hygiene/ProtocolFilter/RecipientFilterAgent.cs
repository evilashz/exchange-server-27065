using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.ProtocolFilter;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.Transport.Agent.ProtocolFilter
{
	// Token: 0x02000021 RID: 33
	internal sealed class RecipientFilterAgent : SmtpReceiveAgent
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x000078DB File Offset: 0x00005ADB
		public RecipientFilterAgent(RecipientFilterConfig recipientFilterConfig, Dictionary<RoutingAddress, bool> blockedRecipients, AddressBook addressBook, AcceptedDomainCollection acceptedDomains)
		{
			this.recipientFilterConfig = recipientFilterConfig;
			this.blockedRecipients = blockedRecipients;
			this.addressBook = addressBook;
			this.acceptedDomains = acceptedDomains;
			base.OnRcptCommand += this.RcptToHandler;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007914 File Offset: 0x00005B14
		private static bool IsAuthenticated(RcptCommandEventArgs rcptEventArgs)
		{
			object obj;
			return rcptEventArgs.SmtpSession.Properties.TryGetValue("Microsoft.Exchange.IsAuthenticated", out obj) && obj is bool && (bool)obj;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000794C File Offset: 0x00005B4C
		private void RcptToHandler(ReceiveCommandEventSource receiveMessageEventSource, RcptCommandEventArgs rcptEventArgs)
		{
			if (this.IsPolicyDisabled(rcptEventArgs))
			{
				return;
			}
			if (CommonUtils.HasAntispamBypassPermission(rcptEventArgs.SmtpSession, ExTraceGlobals.RecipientFilterAgentTracer, this))
			{
				return;
			}
			LogEntry logEntry = null;
			SmtpResponse rcptNotFound = SmtpResponse.RcptNotFound;
			if ((this.IsRecipientLookupEnabled() && this.RecipientIsBlockedByRecipientLookup(rcptEventArgs, ref logEntry, ref rcptNotFound)) || (this.IsBlockListEnabled() && this.RecipientIsBlockedByBlockList(rcptEventArgs, ref logEntry)))
			{
				AgentLog.Instance.LogRejectCommand(base.Name, base.EventTopic, rcptEventArgs, rcptNotFound, logEntry);
				receiveMessageEventSource.RejectCommand(rcptNotFound);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000079C7 File Offset: 0x00005BC7
		private bool IsPolicyDisabled(RcptCommandEventArgs rcptEventArgs)
		{
			if (!CommonUtils.IsEnabled(this.recipientFilterConfig, rcptEventArgs.SmtpSession))
			{
				ExTraceGlobals.RecipientFilterAgentTracer.TraceDebug((long)this.GetHashCode(), "Recipient filter policy is disabled.");
				return true;
			}
			return false;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000079F5 File Offset: 0x00005BF5
		private bool IsRecipientLookupEnabled()
		{
			return this.addressBook != null && this.recipientFilterConfig.RecipientValidationEnabled;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007A0C File Offset: 0x00005C0C
		private bool RecipientIsBlockedByRecipientLookup(RcptCommandEventArgs rcptEventArgs, ref LogEntry logEntry, ref SmtpResponse response)
		{
			bool flag = false;
			bool flag2 = false;
			Microsoft.Exchange.Data.Transport.AcceptedDomain acceptedDomain = this.acceptedDomains.Find(rcptEventArgs.RecipientAddress.DomainPart);
			if (acceptedDomain != null && acceptedDomain.UseAddressBook)
			{
				AddressBookEntry addressBookEntry;
				AddressBookFindStatus arg;
				if (CommonUtils.TryAddressBookFind(this.addressBook, rcptEventArgs.RecipientAddress, out addressBookEntry, out arg))
				{
					if (addressBookEntry == null)
					{
						ExTraceGlobals.RecipientFilterAgentTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Recipient {0} does not exist", rcptEventArgs.RecipientAddress);
						flag = true;
						logEntry = RecipientFilterAgent.RejectContext.RecipientDoesNotExist;
						response = SmtpResponse.RcptNotFound;
					}
					else if (addressBookEntry.RequiresAuthentication && !RecipientFilterAgent.IsAuthenticated(rcptEventArgs))
					{
						ExTraceGlobals.RecipientFilterAgentTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Recipient {0} requires authentication and session is not authenticated", rcptEventArgs.RecipientAddress);
						flag = true;
						logEntry = RecipientFilterAgent.RejectContext.RecipientIsRestricted;
						response = SmtpResponse.RcptNotFound;
					}
				}
				else
				{
					flag2 = true;
					switch (arg)
					{
					case AddressBookFindStatus.TransientFailure:
						ExTraceGlobals.RecipientFilterAgentTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Could not look up recipient {0}: transient failure.", rcptEventArgs.RecipientAddress);
						logEntry = RecipientFilterAgent.RejectContext.TransientFailure;
						response = SmtpResponse.DataTransactionFailed;
						break;
					case AddressBookFindStatus.PermanentFailure:
						ExTraceGlobals.RecipientFilterAgentTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Could not look up recipient {0}: permanent failure.", rcptEventArgs.RecipientAddress);
						logEntry = RecipientFilterAgent.RejectContext.PermanentFailure;
						response = SmtpResponse.RcptNotFound;
						break;
					default:
						ExTraceGlobals.RecipientFilterAgentTracer.TraceError<RoutingAddress, AddressBookFindStatus>((long)this.GetHashCode(), "Could not look up recipient {0}: unexpected status {1}. Returning temporary error response.", rcptEventArgs.RecipientAddress, arg);
						logEntry = RecipientFilterAgent.RejectContext.UnexpectedFailure;
						response = SmtpResponse.DataTransactionFailed;
						break;
					}
				}
			}
			if (flag)
			{
				Util.PerformanceCounters.RecipientFilter.RecipientRejectedByRecipientValidation();
			}
			return flag || flag2;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007B9D File Offset: 0x00005D9D
		private bool IsBlockListEnabled()
		{
			return this.recipientFilterConfig.BlockListEnabled;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00007BAA File Offset: 0x00005DAA
		private bool RecipientIsBlockedByBlockList(RcptCommandEventArgs rcptEventArgs, ref LogEntry logEntry)
		{
			if (this.blockedRecipients.ContainsKey(rcptEventArgs.RecipientAddress))
			{
				ExTraceGlobals.RecipientFilterAgentTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Recipient {0} is on the block list", rcptEventArgs.RecipientAddress);
				Util.PerformanceCounters.RecipientFilter.RecipientRejectedByBlockList();
				logEntry = RecipientFilterAgent.RejectContext.RecipientOnBlockList;
				return true;
			}
			return false;
		}

		// Token: 0x040000DC RID: 220
		private RecipientFilterConfig recipientFilterConfig;

		// Token: 0x040000DD RID: 221
		private Dictionary<RoutingAddress, bool> blockedRecipients;

		// Token: 0x040000DE RID: 222
		private AddressBook addressBook;

		// Token: 0x040000DF RID: 223
		private AcceptedDomainCollection acceptedDomains;

		// Token: 0x02000022 RID: 34
		private static class RejectContext
		{
			// Token: 0x040000E0 RID: 224
			public static readonly LogEntry RecipientDoesNotExist = new LogEntry("RecipientDoesNotExist", string.Empty);

			// Token: 0x040000E1 RID: 225
			public static readonly LogEntry RecipientIsRestricted = new LogEntry("RecipientIsRestricted", string.Empty);

			// Token: 0x040000E2 RID: 226
			public static readonly LogEntry RecipientOnBlockList = new LogEntry("RecipientOnBlockList", string.Empty);

			// Token: 0x040000E3 RID: 227
			public static readonly LogEntry PermanentFailure = new LogEntry("PermanentFailure", string.Empty);

			// Token: 0x040000E4 RID: 228
			public static readonly LogEntry TransientFailure = new LogEntry("TransientFailure", string.Empty);

			// Token: 0x040000E5 RID: 229
			public static readonly LogEntry UnexpectedFailure = new LogEntry("UnexpectedFailure", string.Empty);
		}
	}
}
