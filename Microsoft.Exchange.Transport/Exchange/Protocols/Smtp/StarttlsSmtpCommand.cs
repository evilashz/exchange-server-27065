using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000467 RID: 1127
	internal class StarttlsSmtpCommand : SmtpCommand
	{
		// Token: 0x06003426 RID: 13350 RVA: 0x000D2B6B File Offset: 0x000D0D6B
		public StarttlsSmtpCommand(ISmtpSession session, bool anonymous) : base(session, anonymous ? "X-ANONYMOUSTLS" : "STARTTLS", "OnStartTlsCommand", LatencyComponent.None)
		{
			this.anonymous = anonymous;
			this.CommandEventArgs = new StartTlsCommandEventArgs();
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000D2B9C File Offset: 0x000D0D9C
		internal override void InboundParseCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.StarttlsInboundParseCommand);
			if (!base.VerifyHelloReceived() || !base.VerifyNoOngoingBdat())
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.WrongSequence);
				return;
			}
			ParseResult parseResult = StartTlsSmtpCommandParser.Parse(CommandContext.FromSmtpCommand(this), SmtpInSessionState.FromSmtpInSession(smtpInSession), this.anonymous ? SecureState.AnonymousTls : SecureState.StartTls);
			base.CurrentOffset = base.ProtocolCommandLength;
			if (parseResult.ParsingStatus != ParsingStatus.Complete)
			{
				base.SmtpResponse = parseResult.SmtpResponse;
				base.ParsingStatus = parseResult.ParsingStatus;
				return;
			}
			if (smtpInSession.SecureState != SecureState.None)
			{
				base.SmtpResponse = SmtpResponse.StartTlsAlreadyNegotiated;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				return;
			}
			base.ParsingStatus = ParsingStatus.Complete;
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000D2C48 File Offset: 0x000D0E48
		internal override void InboundProcessCommand()
		{
			if (base.ParsingStatus != ParsingStatus.Complete)
			{
				return;
			}
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.StarttlsInboundProcessCommand);
			if (SmtpInSessionUtils.ShouldThrottleIncomingTLSConnections(smtpInSession.SmtpInServer.InboundTlsIPConnectionTable, smtpInSession.SmtpInServer.ReceiveTlsThrottlingEnabled))
			{
				base.SmtpResponse = SmtpResponse.StartTlsTempReject;
				smtpInSession.SmtpReceivePerformanceCounters.TlsConnectionsRejectedDueToRateExceeded.Increment();
				return;
			}
			smtpInSession.SeenHelo = false;
			smtpInSession.SeenEhlo = false;
			smtpInSession.AbortMailTransaction();
			base.SmtpResponse = SmtpResponse.StartTlsReadyToNegotiate;
			smtpInSession.StartTls(this.anonymous ? SecureState.AnonymousTls : SecureState.StartTls);
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x000D2CDE File Offset: 0x000D0EDE
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x000D2CE0 File Offset: 0x000D0EE0
		internal override void OutboundFormatCommand()
		{
			base.ProtocolCommandString = base.ProtocolCommandKeyword;
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Issued {0} command", base.ProtocolCommandKeyword);
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000D2D0C File Offset: 0x000D0F0C
		internal override void OutboundProcessResponse()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			string statusCode = base.SmtpResponse.StatusCode;
			if (string.Equals(statusCode, "220", StringComparison.Ordinal))
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} command succeeded, will start TLS negotiation", base.ProtocolCommandKeyword);
				smtpOutSession.StartTls(this.anonymous ? SecureState.AnonymousTls : SecureState.StartTls);
				return;
			}
			this.HandleStartTlsErrors(smtpOutSession, base.SmtpResponse);
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x000D2D80 File Offset: 0x000D0F80
		private void HandleStartTlsErrors(SmtpOutSession session, SmtpResponse response)
		{
			if (session.TlsConfiguration.RequireTls || session.AuthMechanism == SmtpSendConnectorConfig.AuthMechanisms.BasicAuthRequireTLS || this.anonymous)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string, SmtpResponse>((long)this.GetHashCode(), "{0} command failed with response {1}", base.ProtocolCommandKeyword, base.SmtpResponse);
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "The session will be terminated");
				session.AckConnection(AckStatus.Retry, response);
				session.NextState = SmtpOutSession.SessionState.Quit;
				return;
			}
			if (!session.CheckDomainSecure<SmtpResponse>(false, "TLS negotiation failed with response: ", TransportEventLogConstants.Tuple_MessageToSecureDomainFailedBecauseTlsNegotiationFailed, base.SmtpResponse))
			{
				return;
			}
			if (!session.CheckRequireOorg())
			{
				return;
			}
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Remote doesn't support TLS, since we don't require it on outbound, continue the session");
			session.PrepareNextStateForEstablishedSession();
		}

		// Token: 0x04001A82 RID: 6786
		protected bool anonymous;
	}
}
