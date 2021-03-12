using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004ED RID: 1261
	internal class MailSmtpInCommand : SmtpInCommandBase
	{
		// Token: 0x06003A57 RID: 14935 RVA: 0x000F220A File Offset: 0x000F040A
		public MailSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
			this.agentEventArgs = new MailCommandEventArgs(this.sessionState);
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x000F2228 File Offset: 0x000F0428
		protected override ParseResult Parse(CommandContext commandContext, out string eventTopic, out ReceiveCommandEventArgs eventArgs)
		{
			eventTopic = null;
			eventArgs = null;
			this.sessionState.ResetMailItemPermissions();
			this.sessionState.ResetExpectedBlobs();
			ParseResult result = MailSmtpCommandParser.Parse(commandContext, this.sessionState, out this.parseOutput);
			if (result.IsFailed)
			{
				return result;
			}
			if (this.sessionState.ServerState.ServiceState == ServiceState.Inactive && this.parseOutput.SystemProbeId == Guid.Empty)
			{
				this.sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Rejecting the non-probe message and disconnecting as transport service is Inactive.");
				this.sessionState.DisconnectReason = DisconnectReason.Local;
				return ParseResult.ServiceInactiveDisconnect;
			}
			if (!MailCommandHelpers.ValidateSecureState(this.sessionState))
			{
				return ParseResult.RequireTlsToSendMail;
			}
			if (MailCommandHelpers.HasMessageRateLimitExceeded(this.sessionState))
			{
				return ParseResult.MessageRateLimitExceeded;
			}
			SmtpResponse smtpResponse;
			if (CommandParsingHelper.ShouldRejectMailItem(this.parseOutput.FromAddress, this.sessionState, false, out smtpResponse))
			{
				return new ParseResult(ParsingStatus.Error, smtpResponse, false);
			}
			if (!MailCommandHelpers.CanSubmitMessage(this.parseOutput.FromAddress, this.sessionState, out smtpResponse))
			{
				if (smtpResponse.Equals(SmtpResponse.UnableToAcceptAnonymousSession, SmtpResponseCompareOptions.IncludeTextComponent) || smtpResponse.Equals(SmtpResponse.SubmitDenied, SmtpResponseCompareOptions.IncludeTextComponent))
				{
					return new ParseResult(ParsingStatus.ProtocolError, smtpResponse, false);
				}
				return new ParseResult(ParsingStatus.ProtocolError, smtpResponse, false);
			}
			else
			{
				if (!MailCommandHelpers.IsValidXShadow(this.parseOutput.XShadow, this.sessionState))
				{
					return ParseResult.InvalidArguments;
				}
				result = MailCommandHelpers.CheckArgumentPermissions(this.sessionState, this.parseOutput);
				if (result.IsFailed)
				{
					return result;
				}
				result = MailCommandHelpers.ValidateMessageId(this.sessionState, this.parseOutput.InternetMessageId);
				if (result.IsFailed)
				{
					return result;
				}
				if (!MailCommandHelpers.AreAllMandatoryBlobsPresent(this.parseOutput.MessageContextParameters, this.sessionState))
				{
					return ParseResult.XMessageEPropNotFoundInMailCommand;
				}
				if (!MailCommandHelpers.AreSpecifiedBlobsAdvertised(this.parseOutput.MessageContextParameters, this.sessionState))
				{
					return ParseResult.InvalidArguments;
				}
				if (this.parseOutput.XAttrOrgId != null && this.parseOutput.XAttrOrgId.InternalOrgId != null && (this.sessionState.CombinedPermissions & Permission.AcceptForestHeaders) == Permission.None)
				{
					this.parseOutput.XAttrOrgId.InternalOrgId = null;
					this.sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Ignored provided internal organization id.");
				}
				this.parseOutput.ShadowMessageId = MailCommandHelpers.ExtractShadowMessageId(this.sessionState, this.parseOutput);
				if (!MailCommandHelpers.ValidateSizeRestrictions(this.sessionState, this.parseOutput.Size))
				{
					return ParseResult.MessageTooLarge;
				}
				result = this.ValidateQueueQuota();
				if (result.IsFailed)
				{
					return result;
				}
				this.agentEventArgs.Auth = this.parseOutput.Auth;
				this.agentEventArgs.BodyType = EnumConverter.InternalToPublic(this.parseOutput.MailBodyType);
				this.agentEventArgs.EnvelopeId = this.parseOutput.EnvelopeId;
				this.agentEventArgs.FromAddress = this.parseOutput.FromAddress;
				this.agentEventArgs.DsnFormatRequested = EnumConverter.InternalToPublic(this.parseOutput.DsnFormat);
				this.agentEventArgs.Size = this.parseOutput.Size;
				this.agentEventArgs.SmtpUtf8 = this.parseOutput.SmtpUtf8;
				this.agentEventArgs.Oorg = this.parseOutput.Oorg;
				this.agentEventArgs.SystemProbeId = this.parseOutput.SystemProbeId;
				this.agentEventArgs.OriginalFromAddress = this.parseOutput.OriginalFromAddress;
				eventTopic = "OnMailCommand";
				eventArgs = this.agentEventArgs;
				return ParseResult.ParsingComplete;
			}
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x000F2754 File Offset: 0x000F0954
		protected override async Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsync(CancellationToken cancellationToken)
		{
			SmtpResponse response = this.sessionState.CreateTransportMailItem(this.parseOutput, this.agentEventArgs);
			this.sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, Encoding.ASCII.GetBytes(this.sessionState.CurrentMessageTemporaryId), "receiving message");
			if (this.sessionState.ServerState.ThrottleDelay > TimeSpan.Zero)
			{
				await this.ThrottleAsync(this.sessionState.ServerState.ThrottleDelay, cancellationToken);
				base.OnAwaitCompleted(cancellationToken);
			}
			ParseAndProcessResult<SmtpInStateMachineEvents> result;
			if (!response.IsEmpty)
			{
				result = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, response, SmtpInStateMachineEvents.CommandFailed, false);
			}
			else
			{
				result = MailSmtpInCommand.ProcessCompleted;
			}
			return result;
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x000F27A2 File Offset: 0x000F09A2
		protected override void LogCommandReceived(CommandContext command)
		{
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000F27A4 File Offset: 0x000F09A4
		protected virtual Task ThrottleAsync(EnhancedTimeSpan delay, CancellationToken cancellationToken)
		{
			return Task.Delay(delay, cancellationToken);
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x000F27B4 File Offset: 0x000F09B4
		private ParseResult ValidateQueueQuota()
		{
			string arg;
			if (this.parseOutput.XAttrOrgId != null && this.parseOutput.XAttrOrgId.ExternalOrgId != Guid.Empty && this.sessionState.ServerState.QueueQuotaComponent != null && !SmtpInSessionUtils.IsPeerShadowSession(this.sessionState.PeerSessionPrimaryServer) && this.sessionState.ServerState.QueueQuotaComponent.IsOrganizationOverQuota(this.parseOutput.XAttrOrgId.ExoAccountForest, this.parseOutput.XAttrOrgId.ExternalOrgId, this.parseOutput.FromAddress.ToString(), out arg))
			{
				this.sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, string.Format("Rejecting the message QQ limit was exceeded. ({0})", arg));
				return ParseResult.OrgQueueQuotaExceeded;
			}
			return ParseResult.ParsingComplete;
		}

		// Token: 0x04001D60 RID: 7520
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> ProcessCompleted = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.MailFromOk, SmtpInStateMachineEvents.MailProcessed, false);

		// Token: 0x04001D61 RID: 7521
		private static readonly TimeSpan BadCertificateTarpitInterval = TimeSpan.FromSeconds(30.0);

		// Token: 0x04001D62 RID: 7522
		private MailParseOutput parseOutput;

		// Token: 0x04001D63 RID: 7523
		private readonly MailCommandEventArgs agentEventArgs;
	}
}
