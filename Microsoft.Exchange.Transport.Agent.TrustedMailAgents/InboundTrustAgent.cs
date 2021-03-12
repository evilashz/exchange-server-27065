using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.Transport.Agent.TrustedMail
{
	// Token: 0x02000005 RID: 5
	internal class InboundTrustAgent : SmtpReceiveAgent
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002720 File Offset: 0x00000920
		public InboundTrustAgent(SmtpServer server, InboundTrustAgentFactory parent, bool enabled)
		{
			if (!enabled)
			{
				return;
			}
			this.smtpServer = server;
			this.parent = parent;
			base.OnMailCommand += this.MailCommandHandler;
			base.OnEndOfHeaders += this.EndOfHeadersHandler;
			base.OnEndOfData += this.EndOfDataHandler;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000277B File Offset: 0x0000097B
		public InboundTrustAgent()
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002783 File Offset: 0x00000983
		protected virtual bool IsFrontEndTransport
		{
			get
			{
				return this.parent.IsFrontEndTransport;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002790 File Offset: 0x00000990
		protected virtual string ComputerName
		{
			get
			{
				return this.parent.ComputerName;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000279D File Offset: 0x0000099D
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.InboundTrustAgentTracer;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000027A4 File Offset: 0x000009A4
		public void MailCommandHandler(ReceiveCommandEventSource eventSource, MailCommandEventArgs eventArgs)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			if (eventArgs == null)
			{
				throw new ArgumentNullException("eventArgs");
			}
			if (TrustedMailUtils.IsMultiTenancyEnabled)
			{
				return;
			}
			SmtpDomain originatingDomain = this.GetOriginatingDomain(eventArgs);
			bool flag = false;
			if (originatingDomain == null)
			{
				InboundTrustAgent.Tracer.TraceDebug((long)this.GetHashCode(), "No originating domain present on mail");
				return;
			}
			if (TrustedMailUtils.IsOriginatingOrgDomainInboundTrustEnabled(this.GetHashCode(), InboundTrustAgent.Tracer, new TrustedMailUtils.GetRemoteDomainEntryDelegate(this.GetRemoteDomainEntry), new TrustedMailUtils.GetAcceptedDomainEntryDelegate(this.GetAcceptedDomainEntry), originatingDomain, null, out flag))
			{
				InboundTrustAgent.Tracer.TraceDebug((long)this.GetHashCode(), "Originating domain is trusted, so setting anti-spam and accept any recipient permissions");
				this.StampTrustedMessagePermissions(eventArgs);
				eventArgs.MailItemProperties["Microsoft.Exchange.Transport.InboundTrustEnabled"] = true;
				return;
			}
			eventArgs.MailItemProperties["Microsoft.Exchange.Transport.InboundTrustEnabled"] = false;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002874 File Offset: 0x00000A74
		public void EndOfHeadersHandler(ReceiveMessageEventSource eventSource, EndOfHeadersEventArgs eventArgs)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			if (eventArgs == null)
			{
				throw new ArgumentNullException("eventArgs");
			}
			if (InboundTrustAgent.AlreadyProcessedMessage(eventArgs.Headers))
			{
				InboundTrustAgent.Tracer.TraceDebug((long)this.GetHashCode(), "Message has already been processed for cross premises headers");
				return;
			}
			if (TrustedMailUtils.HeadersPreservedOutbound(eventArgs.Headers))
			{
				InboundTrustAgent.Tracer.TraceDebug((long)this.GetHashCode(), "Cross premises headers have been preserved for outbound; ignoring the message");
				return;
			}
			SmtpDomain originatingDomain = this.GetOriginatingDomain(eventArgs);
			bool flag = false;
			if (originatingDomain == null)
			{
				InboundTrustAgent.Tracer.TraceDebug((long)this.GetHashCode(), "No originating domain present on mail");
			}
			else if (TrustedMailUtils.InboundTrustEnabledOnMail(this.GetHashCode(), InboundTrustAgent.Tracer, eventArgs.MailItem.Properties, new TrustedMailUtils.GetRemoteDomainEntryDelegate(this.GetRemoteDomainEntry), new TrustedMailUtils.GetAcceptedDomainEntryDelegate(this.GetAcceptedDomainEntry), originatingDomain, eventArgs.MailItem, out flag))
			{
				bool flag2 = TrustedMailUtils.CrossPremisesHeadersPreserved;
				object obj;
				if (eventArgs.MailItem.Properties.TryGetValue("PreserveCrossPremisesHeaders", out obj))
				{
					flag2 = (bool)obj;
				}
				if ((flag2 || flag) && HeaderFirewall.PromoteIncomingCrossPremisesHeaders(InboundTrustAgent.Tracer, eventArgs.Headers))
				{
					InboundTrustAgent.Tracer.TraceDebug((long)this.GetHashCode(), "Cross premises headers found and promoted to organization headers on message");
					TrustedMailUtils.StampHeader(eventArgs.Headers, "X-CrossPremisesHeadersPromoted", this.ComputerName);
					TrustedMailUtils.StampHeader(eventArgs.Headers, "X-MS-Exchange-Organization-Cross-Premises-Headers-Promoted", this.ComputerName);
					this.LogModifyHeaders(eventArgs, InboundTrustAgent.headersPromotedLogEntry);
					if (MultilevelAuth.IsAuthenticated(eventArgs.Headers))
					{
						if (!string.IsNullOrEmpty(eventArgs.MailItem.OriginalAuthenticator) && !string.Equals(eventArgs.MailItem.OriginalAuthenticator, (string)RoutingAddress.NullReversePath, StringComparison.OrdinalIgnoreCase))
						{
							throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unexpected original authenticator value: {0}", new object[]
							{
								eventArgs.MailItem.OriginalAuthenticator
							}));
						}
						eventArgs.MailItem.OriginalAuthenticator = string.Empty;
					}
				}
			}
			if (HeaderFirewall.FilterCrossPremisesHeaders(eventArgs.Headers))
			{
				InboundTrustAgent.Tracer.TraceDebug((long)this.GetHashCode(), "Cross premises headers found and filtered from message");
				TrustedMailUtils.StampHeader(eventArgs.Headers, "X-CrossPremisesHeadersFiltered", this.ComputerName);
				this.LogModifyHeaders(eventArgs, InboundTrustAgent.headersFilteredLogEntry);
			}
			TrustedMailUtils.StampHeader(eventArgs.Headers, "X-MS-Exchange-Organization-Cross-Premises-Headers-Processed", this.ComputerName);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public void EndOfDataHandler(ReceiveMessageEventSource eventSource, EndOfDataEventArgs eventArgs)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			if (eventArgs == null)
			{
				throw new ArgumentNullException("eventArgs");
			}
			if (!this.IsFrontEndTransport && TrustedMailUtils.HandleCrossPremisesProbeEnabled && CrossPremisesMonitoringHelper.TryHandleCrossPremisesProbe(eventArgs.MailItem, this.smtpServer))
			{
				InboundTrustAgent.Tracer.TraceDebug((long)this.GetHashCode(), "Cross premises monitoring probe found and handled.");
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002B19 File Offset: 0x00000D19
		protected virtual void LogModifyHeaders(EndOfHeadersEventArgs eventArgs, LogEntry logEntry)
		{
			AgentLog.Instance.LogModifyHeaders(base.Name, base.EventTopic, eventArgs, eventArgs.SmtpSession, eventArgs.MailItem, logEntry);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B3F File Offset: 0x00000D3F
		protected virtual SmtpDomain GetOriginatingDomain(MailCommandEventArgs mailCommandEventArgs)
		{
			if (!string.IsNullOrEmpty(mailCommandEventArgs.Oorg))
			{
				return new SmtpDomain(mailCommandEventArgs.Oorg);
			}
			return null;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B5C File Offset: 0x00000D5C
		protected virtual SmtpDomain GetOriginatingDomain(EndOfHeadersEventArgs endOfHeadersEventArgs)
		{
			string originatorOrganization = endOfHeadersEventArgs.MailItem.OriginatorOrganization;
			SmtpDomain result = null;
			if (!string.IsNullOrEmpty(originatorOrganization))
			{
				result = new SmtpDomain(originatorOrganization);
			}
			else if (TrustedMailUtils.StampOriginatorOrgForMsitConnector && string.Equals("From_MSITCorp", endOfHeadersEventArgs.SmtpSession.ReceiveConnectorName, StringComparison.OrdinalIgnoreCase))
			{
				result = InboundTrustAgent.MsitOriginatorOrganization;
				endOfHeadersEventArgs.MailItem.OriginatorOrganization = InboundTrustAgent.MsitOriginatorOrganization.Domain;
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002BC4 File Offset: 0x00000DC4
		protected virtual void StampTrustedMessagePermissions(ReceiveEventArgs eventArgs)
		{
			Permission permission = Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit;
			if (TrustedMailUtils.AcceptAnyRecipientOnPremises)
			{
				permission |= Permission.SMTPAcceptAnyRecipient;
			}
			eventArgs.SmtpSession.GrantMailItemPermissions(permission);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002BEE File Offset: 0x00000DEE
		protected virtual RemoteDomainEntry GetRemoteDomainEntry(SmtpDomain domain, MailItem mailItem)
		{
			return TrustedMailUtils.GetRemoteDomainEntry(domain, mailItem);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002BF7 File Offset: 0x00000DF7
		protected virtual AcceptedDomainEntry GetAcceptedDomainEntry(SmtpDomain domain, MailItem mailItem)
		{
			return TrustedMailUtils.GetAcceptedDomainEntry(domain, mailItem);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002C00 File Offset: 0x00000E00
		private static bool AlreadyProcessedMessage(HeaderList headers)
		{
			Header header = headers.FindFirst("X-MS-Exchange-Organization-Cross-Premises-Headers-Processed");
			return null != header;
		}

		// Token: 0x04000009 RID: 9
		private const string MsitConnectorName = "From_MSITCorp";

		// Token: 0x0400000A RID: 10
		private const string PromotedDiagnosticHeader = "X-CrossPremisesHeadersPromoted";

		// Token: 0x0400000B RID: 11
		private const string FilteredDiagnosticHeader = "X-CrossPremisesHeadersFiltered";

		// Token: 0x0400000C RID: 12
		private const string CrossPremisesHeadersProcessedHeader = "X-MS-Exchange-Organization-Cross-Premises-Headers-Processed";

		// Token: 0x0400000D RID: 13
		private static readonly SmtpDomain MsitOriginatorOrganization = SmtpDomain.Parse("microsoft.com");

		// Token: 0x0400000E RID: 14
		private static LogEntry headersPromotedLogEntry = new LogEntry(string.Empty, string.Empty, "Cross premises headers promoted");

		// Token: 0x0400000F RID: 15
		private static LogEntry headersFilteredLogEntry = new LogEntry(string.Empty, string.Empty, "Cross premises headers filtered");

		// Token: 0x04000010 RID: 16
		private readonly InboundTrustAgentFactory parent;

		// Token: 0x04000011 RID: 17
		private readonly SmtpServer smtpServer;
	}
}
