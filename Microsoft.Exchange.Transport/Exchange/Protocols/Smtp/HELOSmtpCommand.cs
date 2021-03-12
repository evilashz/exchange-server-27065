using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200045B RID: 1115
	internal class HELOSmtpCommand : SmtpCommand
	{
		// Token: 0x0600339C RID: 13212 RVA: 0x000CF59D File Offset: 0x000CD79D
		public HELOSmtpCommand(ISmtpSession session) : base(session, "HELO", "OnHeloCommand", LatencyComponent.None)
		{
			this.heloCommandEventArgs = new HeloCommandEventArgs();
			this.CommandEventArgs = this.heloCommandEventArgs;
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x000CF5C8 File Offset: 0x000CD7C8
		// (set) Token: 0x0600339E RID: 13214 RVA: 0x000CF5D5 File Offset: 0x000CD7D5
		internal string HelloDomain
		{
			get
			{
				return this.heloCommandEventArgs.Domain;
			}
			set
			{
				this.heloCommandEventArgs.Domain = value;
			}
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000CF5E4 File Offset: 0x000CD7E4
		internal override void InboundParseCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.HELOInboundParseCommand);
			if (!base.VerifyNoOngoingBdat())
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.WrongSequence);
				return;
			}
			HeloEhloParseOutput heloEhloParseOutput;
			ParseResult parseResult = HeloSmtpCommandParser.Parse(CommandContext.FromSmtpCommand(this), SmtpInSessionState.FromSmtpInSession(smtpInSession), HeloOrEhlo.Helo, out heloEhloParseOutput);
			if (!parseResult.IsFailed && !string.IsNullOrEmpty(heloEhloParseOutput.HeloDomain))
			{
				this.HelloDomain = heloEhloParseOutput.HeloDomain;
				base.CurrentOffset = base.ProtocolCommandLength;
			}
			base.SmtpResponse = parseResult.SmtpResponse;
			base.ParsingStatus = parseResult.ParsingStatus;
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x000CF674 File Offset: 0x000CD874
		internal override void InboundProcessCommand()
		{
			if (base.ParsingStatus != ParsingStatus.Complete)
			{
				return;
			}
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.HELOInboundProcessCommand);
			if ((smtpInSession.SecureState == SecureState.AnonymousTls || smtpInSession.SecureState == SecureState.StartTls) && smtpInSession.TlsCipherKeySize < 128)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceError<int>((long)this.GetHashCode(), "Negotiated TLS cipher strength is too weak at {0} bit.", smtpInSession.TlsCipherKeySize);
				base.SmtpResponse = SmtpResponse.AuthTempFailureTLSCipherTooWeak;
				smtpInSession.Disconnect(DisconnectReason.DroppedSession);
				return;
			}
			if (smtpInSession.SeenHelo || smtpInSession.SeenEhlo)
			{
				base.LowAuthenticationLevelTarpitOverride = TarpitAction.DoTarpit;
			}
			smtpInSession.HelloSmtpDomain = this.HelloDomain;
			smtpInSession.SeenHelo = true;
			smtpInSession.AbortMailTransaction();
			if (smtpInSession.SecureState == SecureState.StartTls && !smtpInSession.DetermineTlsDomainCapabilities())
			{
				base.SmtpResponse = SmtpResponse.CertificateValidationFailure;
				smtpInSession.Disconnect(DisconnectReason.DroppedSession);
				return;
			}
			smtpInSession.AddSessionPermissions(smtpInSession.Capabilities);
			base.SmtpResponse = SmtpResponse.Helo(smtpInSession.AdvertisedEhloOptions.AdvertisedFQDN, smtpInSession.RemoteEndPoint.Address);
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000CF76E File Offset: 0x000CD96E
		internal override void OutboundCreateCommand()
		{
			this.HelloDomain = base.SmtpSession.HelloDomain;
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000CF784 File Offset: 0x000CD984
		internal override void OutboundFormatCommand()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			smtpOutSession.UsingHELO = true;
			base.ProtocolCommandString = "HELO " + this.HelloDomain;
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000CF7BC File Offset: 0x000CD9BC
		internal override void OutboundProcessResponse()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			string statusCode = base.SmtpResponse.StatusCode;
			if (statusCode[0] == '4')
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "HELO failed with response {0}", base.SmtpResponse);
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "Initiating failover");
				smtpOutSession.FailoverConnection(base.SmtpResponse);
				smtpOutSession.SetNextStateToQuit();
				return;
			}
			if (statusCode[0] != '2')
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "HELO command failed with response {0}", base.SmtpResponse);
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "The session will be terminated");
				smtpOutSession.AckConnection(AckStatus.Retry, base.SmtpResponse);
				smtpOutSession.SetNextStateToQuit();
				return;
			}
			string trace = smtpOutSession.Connector.ForceHELO ? "ForceHELO is set on the send connector and hence STARTTLS can't be issued" : "TLS was not offered on HELO";
			if (!smtpOutSession.CheckDomainSecure<string>(false, trace, TransportEventLogConstants.Tuple_MessageToSecureDomainFailedBecauseTlsNotOffered, string.Empty))
			{
				EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "MessageToSecureDomainFailedBecauseTlsNotOffered", null, "A message from a domain-secured domain failed to authenticate because the TLS certificate does not contain the domain name.", ResultSeverityLevel.Error, false);
				return;
			}
			if (!smtpOutSession.Connector.DNSRoutingEnabled || string.Equals(smtpOutSession.Connector.Name, Strings.IntraorgSendConnectorName))
			{
				if (smtpOutSession.AuthMechanism == SmtpSendConnectorConfig.AuthMechanisms.BasicAuthRequireTLS)
				{
					string message = smtpOutSession.Connector.ForceHELO ? "We require AUTH after TLS, but our send connector has ForceHELO set to true and hence can't establish STARTTLS." : "We require AUTH after TLS, but remote doesn't advertise STARTTLS, will drop session";
					ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), message);
					smtpOutSession.FailoverConnection(SmtpResponse.RequireSTARTTLSToBasicAuth);
					smtpOutSession.SetNextStateToQuit();
					return;
				}
				if (smtpOutSession.AuthMechanism == SmtpSendConnectorConfig.AuthMechanisms.ExchangeServer)
				{
					string message2 = smtpOutSession.Connector.ForceHELO ? "Connector requires ExchangeServer authentication, but out send connector has ForceHELO set to true." : "Connector requires ExchangeServer authentication, but we can't achieve it because remote server doesn't support EHLO.";
					ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), message2);
					smtpOutSession.FailoverConnection(SmtpResponse.CannotExchangeAuthenticate);
					smtpOutSession.SetNextStateToQuit();
					return;
				}
			}
			if (!smtpOutSession.CheckRequireOorg())
			{
				return;
			}
			smtpOutSession.AdvertisedEhloOptions.ParseHeloResponse(base.SmtpResponse);
			smtpOutSession.NextHopConnection.GenerateSuccessDSNs = DsnFlags.Relay;
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "HELO command succeeded");
			smtpOutSession.PrepareNextStateForEstablishedSession();
		}

		// Token: 0x04001A29 RID: 6697
		private readonly HeloCommandEventArgs heloCommandEventArgs;
	}
}
