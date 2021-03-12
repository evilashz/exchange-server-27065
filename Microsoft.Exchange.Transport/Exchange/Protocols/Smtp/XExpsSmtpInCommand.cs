using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004F9 RID: 1273
	internal class XExpsSmtpInCommand : AuthSmtpInCommandBase
	{
		// Token: 0x06003AA5 RID: 15013 RVA: 0x000F3A0D File Offset: 0x000F1C0D
		public XExpsSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x000F3A17 File Offset: 0x000F1C17
		public static ParseAndProcessResult<SmtpInStateMachineEvents> CreateExchangeAuthSuccessfulResult(byte[] lastNegotiateSecurityContextResponse)
		{
			return new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.ExchangeAuthSuccessful(lastNegotiateSecurityContextResponse), SmtpInStateMachineEvents.XExpsProcessed, false);
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000F3A28 File Offset: 0x000F1C28
		public override void LogSmtpResponse(SmtpResponse smtpResponse)
		{
			if (smtpResponse.IsEmpty)
			{
				return;
			}
			switch (this.authParseOutput.AuthenticationMechanism)
			{
			case SmtpAuthenticationMechanism.Gssapi:
			case SmtpAuthenticationMechanism.Ntlm:
				this.sessionState.ProtocolLogSession.LogSend(smtpResponse.ToByteArray());
				return;
			case SmtpAuthenticationMechanism.ExchangeAuth:
				this.sessionState.ProtocolLogSession.LogSend(XExpsSmtpInCommand.ExchangeAuthSuccessProtocolLogLine);
				return;
			default:
				throw new ArgumentOutOfRangeException(string.Format("Unexpected authentication mechanism: {0}", this.authParseOutput.AuthenticationMechanism));
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x000F3AAE File Offset: 0x000F1CAE
		protected override AuthCommand AuthCommandType
		{
			get
			{
				return AuthCommand.XExps;
			}
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000F3AB4 File Offset: 0x000F1CB4
		protected override AuthenticationContext CreateAuthenticationContext()
		{
			switch (this.authParseOutput.AuthenticationMechanism)
			{
			case SmtpAuthenticationMechanism.Gssapi:
			case SmtpAuthenticationMechanism.Ntlm:
				return new AuthenticationContext(this.sessionState.ExtendedProtectionConfig, this.sessionState.NetworkConnection.ChannelBindingToken);
			case SmtpAuthenticationMechanism.ExchangeAuth:
				return new AuthenticationContext();
			default:
				throw new ArgumentOutOfRangeException(string.Format("Unexpected authentication mechanism: {0}", this.authParseOutput.AuthenticationMechanism));
			}
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x000F3B2C File Offset: 0x000F1D2C
		protected override SecurityStatus InitializeAuthenticationContext(AuthenticationContext authenticationContext)
		{
			switch (this.authParseOutput.AuthenticationMechanism)
			{
			case SmtpAuthenticationMechanism.Gssapi:
				return authenticationContext.InitializeForInboundNegotiate(AuthenticationMechanism.Negotiate);
			case SmtpAuthenticationMechanism.Ntlm:
				return authenticationContext.InitializeForInboundNegotiate(AuthenticationMechanism.Ntlm);
			case SmtpAuthenticationMechanism.ExchangeAuth:
				return this.InitializeExchangeAuthentication(authenticationContext);
			default:
				throw new ArgumentOutOfRangeException(string.Format("Unexpected authentication mechanism: {0}", this.authParseOutput.AuthenticationMechanism));
			}
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x000F3B94 File Offset: 0x000F1D94
		protected override ParseAndProcessResult<SmtpInStateMachineEvents> GetAuthenticationSuccessfulResult(byte[] lastNegotiateSecurityContextResponse)
		{
			switch (this.authParseOutput.AuthenticationMechanism)
			{
			case SmtpAuthenticationMechanism.Gssapi:
			case SmtpAuthenticationMechanism.Ntlm:
				return XExpsSmtpInCommand.CommandComplete;
			case SmtpAuthenticationMechanism.ExchangeAuth:
				return XExpsSmtpInCommand.CreateExchangeAuthSuccessfulResult(lastNegotiateSecurityContextResponse);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x000F3BD5 File Offset: 0x000F1DD5
		protected override ParseAndProcessResult<SmtpInStateMachineEvents> GetAuthenticationFailedResult(SmtpResponse? customSmtpResponse = null, bool disconnectClient = false)
		{
			this.sessionState.DisconnectReason = DisconnectReason.DroppedSession;
			if (customSmtpResponse == null)
			{
				return AuthSmtpInCommandBase.AuthTempFailureDisconnecting;
			}
			return new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, customSmtpResponse.Value, SmtpInStateMachineEvents.SendResponseAndDisconnectClient, false);
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x000F3C04 File Offset: 0x000F1E04
		private SecurityStatus InitializeExchangeAuthentication(AuthenticationContext authenticationContext)
		{
			if (!this.sessionState.NetworkConnection.IsTls)
			{
				this.LogInitializeExchangeAuthenticationFailure(TransportEventLogConstants.Tuple_SmtpReceiveAuthenticationInitializationFailed, "Connection is not TLS", "InitializeExchangeAuthentication failed: Connection is not TLS", "Inbound ExchangeAuth negotiation failed because connection is not TLS");
				return SecurityStatus.InternalError;
			}
			byte[] publicKey;
			Exception ex;
			if (!AuthCommandHelpers.TryGetLocalCertificatePublicKey(this.sessionState.NetworkConnection, out publicKey, out ex))
			{
				this.LogInitializeExchangeAuthenticationFailure(TransportEventLogConstants.Tuple_SmtpReceiveAuthenticationInitializationFailed, ex.ToString(), "InitializeExchangeAuthentication failed: " + ex.Message, "Inbound ExchangeAuth negotiation failed because " + ex.Message);
				return SecurityStatus.CertUnknown;
			}
			SecurityStatus securityStatus = authenticationContext.InitializeForInboundExchangeAuth(this.authParseOutput.ExchangeAuthHashAlgorithm, "SMTPSVC/" + this.sessionState.HelloDomain, publicKey, this.sessionState.NetworkConnection.TlsEapKey);
			if (SspiContext.IsSecurityStatusFailure(securityStatus))
			{
				this.LogInitializeExchangeAuthenticationFailure(TransportEventLogConstants.Tuple_SmtpReceiveAuthenticationInitializationFailed, securityStatus.ToString(), "InitializeForInboundExchangeAuth failed: " + securityStatus, "Inbound ExchangeAuth negotiation failed because " + securityStatus);
				return securityStatus;
			}
			return securityStatus;
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000F3D0C File Offset: 0x000F1F0C
		private void LogInitializeExchangeAuthenticationFailure(ExEventLog.EventTuple eventTuple, string eventMsg, string traceMsg, string logMsg)
		{
			this.sessionState.Tracer.TraceError((long)this.GetHashCode(), traceMsg);
			this.sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, logMsg);
			this.sessionState.EventLog.LogEvent(eventTuple, this.sessionState.NetworkConnection.RemoteEndPoint.Address.ToString(), new object[]
			{
				eventMsg,
				this.sessionState.ReceiveConnector.Name,
				this.authParseOutput.AuthenticationMechanism,
				this.sessionState.NetworkConnection.RemoteEndPoint.Address
			});
		}

		// Token: 0x04001D82 RID: 7554
		protected const string SmtpSpnPrefix = "SMTPSVC/";

		// Token: 0x04001D83 RID: 7555
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> CommandComplete = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.AuthSuccessful, SmtpInStateMachineEvents.XExpsProcessed, false);

		// Token: 0x04001D84 RID: 7556
		private static readonly byte[] ExchangeAuthSuccessProtocolLogLine = Encoding.ASCII.GetBytes("235 <authentication response>");
	}
}
