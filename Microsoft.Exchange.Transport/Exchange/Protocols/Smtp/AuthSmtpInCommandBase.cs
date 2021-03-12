using System;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004DD RID: 1245
	internal abstract class AuthSmtpInCommandBase : SmtpInCommandBase
	{
		// Token: 0x0600396B RID: 14699 RVA: 0x000EB388 File Offset: 0x000E9588
		protected AuthSmtpInCommandBase(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000EB394 File Offset: 0x000E9594
		protected override ParseResult Parse(CommandContext commandContext, out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			ParseResult result = AuthSmtpCommandParser.ParseAuthMechanism(commandContext, this.sessionState, this.AuthCommandType, out this.authParseOutput);
			if (result.IsFailed)
			{
				agentEventTopic = null;
				agentEventArgs = null;
			}
			else
			{
				agentEventTopic = "OnAuthCommand";
				agentEventArgs = new AuthCommandEventArgs(this.sessionState, this.authParseOutput.AuthenticationMechanism.ToString());
			}
			return result;
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000EBBE0 File Offset: 0x000E9DE0
		protected override async Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsync(CancellationToken cancellationToken)
		{
			ParseAndProcessResult<SmtpInStateMachineEvents> result2;
			using (AuthenticationContext authenticationContext = this.CreateAuthenticationContext())
			{
				SecurityStatus securityStatus = this.InitializeAuthenticationContext(authenticationContext);
				if (SspiContext.IsSecurityStatusFailure(securityStatus))
				{
					this.LogInitializeAuthenticationContextFailure(securityStatus);
					result2 = AuthSmtpInCommandBase.AuthFailed;
				}
				else
				{
					CommandContext commandContext = this.authParseOutput.InitialBlob;
					if (this.authParseOutput.InitialBlob.Length == 0 && (this.IsNtlmAuthentication || this.IsGssapiAuthentication))
					{
						await base.WriteToClientAsync(this.NtlmOrGssapiSupportedResponse);
						base.OnAwaitCompleted(cancellationToken);
						NetworkConnection.LazyAsyncResultWithTimeout readResult = await Util.ReadLineAsync(this.sessionState);
						base.OnAwaitCompleted(cancellationToken);
						if (Util.IsReadFailure(readResult))
						{
							this.sessionState.HandleNetworkError(readResult.Result);
							return AuthSmtpInCommandBase.ClientDisconnected;
						}
						commandContext = CommandContext.FromAsyncResult(readResult);
						if (AuthCommandHelpers.IsCancelAuthBlob(commandContext))
						{
							return AuthSmtpInCommandBase.AuthCancelled;
						}
					}
					byte[] smtpResponseBlob;
					NetworkConnection.LazyAsyncResultWithTimeout readResult2;
					for (;;)
					{
						securityStatus = authenticationContext.NegotiateSecurityContext(commandContext.Command, commandContext.Offset, commandContext.Length, out smtpResponseBlob);
						if (SspiContext.IsSecurityStatusFailure(securityStatus))
						{
							break;
						}
						if (securityStatus == SecurityStatus.ContinueNeeded)
						{
							await base.WriteToClientAsync(SmtpResponse.AuthBlob(smtpResponseBlob));
							base.OnAwaitCompleted(cancellationToken);
							readResult2 = await Util.ReadLineAsync(this.sessionState);
							base.OnAwaitCompleted(cancellationToken);
							if (Util.IsReadFailure(readResult2))
							{
								goto Block_10;
							}
							commandContext = CommandContext.FromAsyncResult(readResult2);
							if (AuthCommandHelpers.IsCancelAuthBlob(commandContext))
							{
								goto Block_11;
							}
						}
						if (securityStatus != SecurityStatus.ContinueNeeded)
						{
							goto Block_12;
						}
					}
					this.LogNegotiateSecurityContextFailure(securityStatus);
					return AuthSmtpInCommandBase.AuthFailed;
					Block_10:
					this.sessionState.HandleNetworkError(readResult2.Result);
					return AuthSmtpInCommandBase.ClientDisconnected;
					Block_11:
					return AuthSmtpInCommandBase.AuthCancelled;
					Block_12:
					if (securityStatus == SecurityStatus.CompleteNeeded)
					{
						ProcessAuthenticationEventArgs eventArgs = new ProcessAuthenticationEventArgs(this.sessionState, this.loginUsername, this.loginPassword);
						ParseAndProcessResult<SmtpInStateMachineEvents> processAuthenticationResult = await base.RaiseAgentEventAsync("OnProcessAuthentication", eventArgs, commandContext, ParseResult.ParsingComplete, cancellationToken, null);
						base.OnAwaitCompleted(cancellationToken);
						if (!processAuthenticationResult.SmtpResponse.IsEmpty)
						{
							return processAuthenticationResult;
						}
						if (eventArgs.Identity == null)
						{
							this.LogProcessAuthenticationEventFailure(eventArgs.AuthResult, eventArgs.AuthErrorDetails);
							AuthCommandHelpers.OnAuthenticationFailure(authenticationContext, this.sessionState);
							return this.GetAuthenticationFailedResult(null, false);
						}
						authenticationContext.Identity = eventArgs.Identity;
					}
					ParseAndProcessResult<SmtpInStateMachineEvents> result = AuthCommandHelpers.OnAuthenticationComplete(this.authParseOutput, authenticationContext, this.sessionState, this.loginUsername, smtpResponseBlob, new AuthCommandHelpers.GetAuthenticationSuccessfulResult(this.GetAuthenticationSuccessfulResult), new AuthCommandHelpers.GetAuthenticationFailureResult(this.GetAuthenticationFailedResult));
					if (result.SmtpResponse.SmtpResponseType == SmtpResponseType.Success)
					{
						ParseAndProcessResult<SmtpInStateMachineEvents> endOfAuthenticationResult = await base.RaiseAgentEventAsync("OnEndOfAuthentication", new EndOfAuthenticationEventArgs(this.sessionState, this.authParseOutput.AuthenticationMechanism.ToString(), this.sessionState.RemoteIdentityName), commandContext, ParseResult.ParsingComplete, cancellationToken, EndOfAuthenticationEventSourceImpl.Create(this.sessionState));
						base.OnAwaitCompleted(cancellationToken);
						if (!endOfAuthenticationResult.SmtpResponse.IsEmpty)
						{
							return endOfAuthenticationResult;
						}
					}
					result2 = result;
				}
			}
			return result2;
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x0600396E RID: 14702
		protected abstract AuthCommand AuthCommandType { get; }

		// Token: 0x0600396F RID: 14703
		protected abstract AuthenticationContext CreateAuthenticationContext();

		// Token: 0x06003970 RID: 14704
		protected abstract SecurityStatus InitializeAuthenticationContext(AuthenticationContext authenticationContext);

		// Token: 0x06003971 RID: 14705
		protected abstract ParseAndProcessResult<SmtpInStateMachineEvents> GetAuthenticationSuccessfulResult(byte[] lastNegotiateSecurityContextResponse);

		// Token: 0x06003972 RID: 14706
		protected abstract ParseAndProcessResult<SmtpInStateMachineEvents> GetAuthenticationFailedResult(SmtpResponse? customSmtpResponse = null, bool disconnectClient = false);

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06003973 RID: 14707 RVA: 0x000EBC2E File Offset: 0x000E9E2E
		protected bool IsNtlmAuthentication
		{
			get
			{
				return this.authParseOutput.AuthenticationMechanism == SmtpAuthenticationMechanism.Ntlm;
			}
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06003974 RID: 14708 RVA: 0x000EBC3E File Offset: 0x000E9E3E
		protected bool IsGssapiAuthentication
		{
			get
			{
				return this.authParseOutput.AuthenticationMechanism == SmtpAuthenticationMechanism.Gssapi;
			}
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06003975 RID: 14709 RVA: 0x000EBC4E File Offset: 0x000E9E4E
		private SmtpResponse NtlmOrGssapiSupportedResponse
		{
			get
			{
				if (this.authParseOutput.AuthenticationMechanism != SmtpAuthenticationMechanism.Ntlm)
				{
					return SmtpResponse.GssapiSupported;
				}
				return SmtpResponse.NtlmSupported;
			}
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000EBC69 File Offset: 0x000E9E69
		protected void ExtractClearTextCredentialForLogin(byte[] domain, byte[] username, byte[] password)
		{
			this.loginUsername = AuthCommandHelpers.UsernameFromDomainAndUsername(domain, username);
			this.loginPassword = AuthCommandHelpers.SecureStringFromBytes(password);
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000EBC84 File Offset: 0x000E9E84
		protected SecurityStatus ExtractClearTextCredentialForLiveId(byte[] username, byte[] password, out WindowsIdentity windowsIdentity, out IAccountValidationContext accountValidationContext)
		{
			windowsIdentity = null;
			accountValidationContext = null;
			this.loginUsername = username;
			this.loginPassword = AuthCommandHelpers.SecureStringFromBytes(password);
			return SecurityStatus.CompleteNeeded;
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x000EBCA5 File Offset: 0x000E9EA5
		protected override void LogCommandReceived(CommandContext command)
		{
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000EBCA8 File Offset: 0x000E9EA8
		private void LogInitializeAuthenticationContextFailure(SecurityStatus status)
		{
			this.sessionState.Tracer.TraceError<SecurityStatus>((long)this.GetHashCode(), "InitializeForInboundNegotiate failed: {0}", status);
			this.sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Unable to initialize inbound AUTH because of " + status);
			this.sessionState.EventLog.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveAuthenticationInitializationFailed, this.networkConnection.RemoteEndPoint.Address.ToString(), new object[]
			{
				status,
				this.sessionState.ReceiveConnector.Name,
				this.authParseOutput.AuthenticationMechanism,
				this.networkConnection.RemoteEndPoint.Address
			});
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x000EBD6C File Offset: 0x000E9F6C
		private void LogNegotiateSecurityContextFailure(SecurityStatus status)
		{
			this.sessionState.Tracer.TraceDebug<SecurityStatus>((long)this.GetHashCode(), "NegotiateSecurityContext() failed: {0}", status);
			this.sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Inbound authentication failed because of " + status);
			this.sessionState.EventLog.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveAuthenticationFailed, this.networkConnection.RemoteEndPoint.Address.ToString(), new object[]
			{
				status,
				this.sessionState.ReceiveConnector.Name,
				this.authParseOutput.AuthenticationMechanism,
				this.networkConnection.RemoteEndPoint.Address
			});
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x000EBE30 File Offset: 0x000EA030
		private void LogProcessAuthenticationEventFailure(object authResult, string errorDetails)
		{
			if (authResult is LiveIdAuthResult)
			{
				LiveIdAuthResult liveIdAuthResult = (LiveIdAuthResult)authResult;
				if (liveIdAuthResult == LiveIdAuthResult.LiveServerUnreachable || liveIdAuthResult == LiveIdAuthResult.OperationTimedOut || liveIdAuthResult == LiveIdAuthResult.CommunicationFailure)
				{
					this.sessionState.UpdateAvailabilityPerfCounters(LegitimateSmtpAvailabilityCategory.RejectDueToWLIDDown);
				}
			}
			this.sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Custom (generally LiveID) Authentication failed with reason: " + authResult.ToString() + ", Details: " + AuthCommandHelpers.RedactUserNameInErrorDetails(errorDetails, Util.IsDataRedactionNecessary()));
		}

		// Token: 0x04001D31 RID: 7473
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> AuthFailed = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.AuthUnsuccessful, SmtpInStateMachineEvents.CommandFailed, false);

		// Token: 0x04001D32 RID: 7474
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> ClientDisconnected = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.Empty, SmtpInStateMachineEvents.NetworkError, false);

		// Token: 0x04001D33 RID: 7475
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> AuthFailedDisconnecting = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.AuthUnsuccessful, SmtpInStateMachineEvents.SendResponseAndDisconnectClient, false);

		// Token: 0x04001D34 RID: 7476
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> TooManyAuthenticationErrorsDisconnecting = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.TooManyAuthenticationErrors, SmtpInStateMachineEvents.SendResponseAndDisconnectClient, false);

		// Token: 0x04001D35 RID: 7477
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> AuthTempFailureDisconnecting = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.AuthTempFailure, SmtpInStateMachineEvents.SendResponseAndDisconnectClient, false);

		// Token: 0x04001D36 RID: 7478
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> AuthCancelled = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.AuthCancelled, SmtpInStateMachineEvents.CommandFailed, false);

		// Token: 0x04001D37 RID: 7479
		private byte[] loginUsername;

		// Token: 0x04001D38 RID: 7480
		private SecureString loginPassword;

		// Token: 0x04001D39 RID: 7481
		protected AuthParseOutput authParseOutput;
	}
}
