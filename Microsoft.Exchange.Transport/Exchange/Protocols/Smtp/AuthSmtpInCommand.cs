using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004DE RID: 1246
	internal class AuthSmtpInCommand : AuthSmtpInCommandBase
	{
		// Token: 0x0600397D RID: 14717 RVA: 0x000EBF11 File Offset: 0x000EA111
		public AuthSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x0600397E RID: 14718 RVA: 0x000EBF1B File Offset: 0x000EA11B
		protected override AuthCommand AuthCommandType
		{
			get
			{
				return AuthCommand.Auth;
			}
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x000EBF20 File Offset: 0x000EA120
		protected override AuthenticationContext CreateAuthenticationContext()
		{
			switch (this.authParseOutput.AuthenticationMechanism)
			{
			case SmtpAuthenticationMechanism.Login:
				if (!this.sessionState.ReceiveConnector.LiveCredentialEnabled)
				{
					return new AuthenticationContext(new ExternalLoginProcessing(base.ExtractClearTextCredentialForLogin));
				}
				return new AuthenticationContext(new ExternalLoginAuthentication(base.ExtractClearTextCredentialForLiveId));
			case SmtpAuthenticationMechanism.Gssapi:
			case SmtpAuthenticationMechanism.Ntlm:
				return new AuthenticationContext(this.sessionState.ExtendedProtectionConfig, this.sessionState.NetworkConnection.ChannelBindingToken);
			default:
				throw new ArgumentOutOfRangeException(string.Format("Unexpected authentication mechanism: {0}", this.authParseOutput.AuthenticationMechanism));
			}
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x000EBFC8 File Offset: 0x000EA1C8
		protected override SecurityStatus InitializeAuthenticationContext(AuthenticationContext authenticationContext)
		{
			switch (this.authParseOutput.AuthenticationMechanism)
			{
			case SmtpAuthenticationMechanism.Login:
				return authenticationContext.InitializeForInboundNegotiate(AuthenticationMechanism.Login);
			case SmtpAuthenticationMechanism.Gssapi:
				return authenticationContext.InitializeForInboundNegotiate(AuthenticationMechanism.Gssapi);
			case SmtpAuthenticationMechanism.Ntlm:
				return authenticationContext.InitializeForInboundNegotiate(AuthenticationMechanism.Ntlm);
			default:
				throw new ArgumentOutOfRangeException(string.Format("Unexpected authentication mechanism: {0}", this.authParseOutput.AuthenticationMechanism));
			}
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x000EC02E File Offset: 0x000EA22E
		protected override ParseAndProcessResult<SmtpInStateMachineEvents> GetAuthenticationSuccessfulResult(byte[] lastNegotiateSecurityContextResponse)
		{
			return AuthSmtpInCommand.CommandComplete;
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000EC038 File Offset: 0x000EA238
		protected override ParseAndProcessResult<SmtpInStateMachineEvents> GetAuthenticationFailedResult(SmtpResponse? customSmtpResponse = null, bool disconnectClient = false)
		{
			if (this.sessionState.IsMaxLogonFailuresExceeded)
			{
				this.sessionState.DisconnectReason = DisconnectReason.TooManyErrors;
				return AuthSmtpInCommandBase.TooManyAuthenticationErrorsDisconnecting;
			}
			if (disconnectClient)
			{
				this.sessionState.DisconnectReason = DisconnectReason.Local;
			}
			if (customSmtpResponse != null)
			{
				return new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, customSmtpResponse.Value, disconnectClient ? SmtpInStateMachineEvents.SendResponseAndDisconnectClient : SmtpInStateMachineEvents.CommandFailed, false);
			}
			if (!disconnectClient)
			{
				return AuthSmtpInCommandBase.AuthFailed;
			}
			return AuthSmtpInCommandBase.AuthFailedDisconnecting;
		}

		// Token: 0x04001D3A RID: 7482
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> CommandComplete = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.AuthSuccessful, SmtpInStateMachineEvents.AuthProcessed, false);
	}
}
