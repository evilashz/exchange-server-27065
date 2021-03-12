using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004D7 RID: 1239
	internal abstract class SmtpInCommandBase : ISmtpInCommand<SmtpInStateMachineEvents>
	{
		// Token: 0x06003926 RID: 14630 RVA: 0x000E93CF File Offset: 0x000E75CF
		protected SmtpInCommandBase(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate)
		{
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			ArgumentValidator.ThrowIfNull("awaitCompletedDelegate", awaitCompletedDelegate);
			this.sessionState = sessionState;
			this.networkConnection = sessionState.NetworkConnection;
			this.awaitCompletedDelegate = awaitCompletedDelegate;
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x000E97A4 File Offset: 0x000E79A4
		public async Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ParseAndProcessAsync(CommandContext commandContext, CancellationToken cancellationToken)
		{
			string agentEventTopic;
			ReceiveCommandEventArgs agentEventArgs;
			ParseResult parseResult = this.Parse(commandContext, out agentEventTopic, out agentEventArgs);
			this.LogCommandReceived(commandContext);
			ParseAndProcessResult<SmtpInStateMachineEvents> result2;
			if (parseResult.IsFailed)
			{
				SmtpResponse smtpResponse = await this.RaiseRejectEventAsync(commandContext, parseResult.ParsingStatus, parseResult.SmtpResponse, null, cancellationToken);
				if (parseResult.DisconnectClient || this.sessionState.ShouldDisconnect)
				{
					if (this.sessionState.DisconnectReason == DisconnectReason.None)
					{
						this.sessionState.DisconnectReason = (this.sessionState.ShouldDisconnect ? DisconnectReason.DroppedSession : DisconnectReason.Local);
					}
					result2 = new ParseAndProcessResult<SmtpInStateMachineEvents>(parseResult.ParsingStatus, smtpResponse, SmtpInStateMachineEvents.SendResponseAndDisconnectClient, true);
				}
				else
				{
					result2 = new ParseAndProcessResult<SmtpInStateMachineEvents>(parseResult.ParsingStatus, smtpResponse, this.GetCommandFailureEvent(), false);
				}
			}
			else
			{
				using (new AutoLatencyTracker(this.sessionState.SmtpAgentSession.LatencyTracker, this.LatencyComponent, this.MailItemLatencyTracker))
				{
					ParseAndProcessResult<SmtpInStateMachineEvents> result = await this.RaiseAgentCommandEventAsync(agentEventTopic, agentEventArgs, commandContext, parseResult, cancellationToken);
					if (!result.SmtpResponse.IsEmpty)
					{
						return result;
					}
				}
				result2 = await this.ProcessAsync(cancellationToken);
			}
			return result2;
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x000E97FC File Offset: 0x000E79FC
		public virtual void LogSmtpResponse(SmtpResponse smtpResponse)
		{
			if (!smtpResponse.IsEmpty)
			{
				this.sessionState.ProtocolLogSession.LogSend(smtpResponse.ToByteArray());
				if (smtpResponse.SmtpResponseType == SmtpResponseType.TransientError || smtpResponse.SmtpResponseType == SmtpResponseType.PermanentError)
				{
					string text = null;
					if (this.sessionState.TransportMailItem != null)
					{
						text = this.sessionState.TransportMailItem.InternetMessageId;
					}
					if (!string.IsNullOrEmpty(text))
					{
						this.sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "InternetMessageId: {0}", new object[]
						{
							text
						});
					}
				}
			}
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x000E9888 File Offset: 0x000E7A88
		protected Task<object> WriteToClientAsync(SmtpResponse smtpResponse)
		{
			return Util.WriteToClientAsync(this.networkConnection, smtpResponse);
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000E9896 File Offset: 0x000E7A96
		protected void OnAwaitCompleted(CancellationToken cancellationToken)
		{
			this.awaitCompletedDelegate(cancellationToken);
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000E98A4 File Offset: 0x000E7AA4
		protected virtual void LogCommandReceived(CommandContext command)
		{
			command.LogReceivedCommand(this.sessionState.ProtocolLogSession);
		}

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x0600392C RID: 14636 RVA: 0x000E98B7 File Offset: 0x000E7AB7
		protected virtual LatencyComponent LatencyComponent
		{
			get
			{
				return LatencyComponent.None;
			}
		}

		// Token: 0x0600392D RID: 14637
		protected abstract ParseResult Parse(CommandContext commandContext, out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs);

		// Token: 0x0600392E RID: 14638
		protected abstract Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsync(CancellationToken cancellationToken);

		// Token: 0x0600392F RID: 14639 RVA: 0x000E9BB8 File Offset: 0x000E7DB8
		protected async Task<ParseAndProcessResult<SmtpInStateMachineEvents>> RaiseAgentEventAsync(string eventTopic, ReceiveEventArgs eventArgs, CommandContext commandContext, ParseResult parseResult, CancellationToken cancellationToken, ReceiveEventSource receiveEventSource = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("eventTopic", eventTopic);
			ArgumentValidator.ThrowIfNull("eventArgs", eventArgs);
			ArgumentValidator.ThrowIfNull("commandContext", commandContext);
			SmtpResponse smtpResponseFromMexRuntime = await this.sessionState.SmtpAgentSession.RaiseEventAsync(eventTopic, receiveEventSource ?? ReceiveCommandEventSourceImpl.Create(this.sessionState), eventArgs);
			this.awaitCompletedDelegate(cancellationToken);
			ParseAndProcessResult<SmtpInStateMachineEvents> result;
			if (!smtpResponseFromMexRuntime.IsEmpty)
			{
				result = this.CreateUnhandledAgentExceptionResult(parseResult.ParsingStatus, smtpResponseFromMexRuntime);
			}
			else if (this.sessionState.ShouldDisconnect)
			{
				SmtpResponse smtpResponse = this.sessionState.SmtpResponse.IsEmpty ? SmtpResponse.ConnectionDroppedByAgentError : this.sessionState.SmtpResponse;
				result = new ParseAndProcessResult<SmtpInStateMachineEvents>(parseResult.ParsingStatus, smtpResponse, SmtpInStateMachineEvents.SendResponseAndDisconnectClient, false);
			}
			else if (!this.sessionState.SmtpResponse.IsEmpty)
			{
				SmtpResponse finalSmtpResponse = await this.RaiseRejectEventAsync(commandContext, parseResult.ParsingStatus, this.sessionState.SmtpResponse, eventArgs, cancellationToken);
				this.sessionState.SmtpResponse = SmtpResponse.Empty;
				result = new ParseAndProcessResult<SmtpInStateMachineEvents>(parseResult.ParsingStatus, finalSmtpResponse, this.GetCommandFailureEvent(), false);
			}
			else
			{
				result = SmtpInCommandBase.SmtpResponseEmptyResult;
			}
			return result;
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000E9C31 File Offset: 0x000E7E31
		protected ParseAndProcessResult<SmtpInStateMachineEvents> CreateUnhandledAgentExceptionResult(ParsingStatus parsingStatus, SmtpResponse smtpResponse)
		{
			return new ParseAndProcessResult<SmtpInStateMachineEvents>(parsingStatus, smtpResponse, this.GetCommandFailureEvent(), false);
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000E9D70 File Offset: 0x000E7F70
		protected async Task<SmtpResponse> RaiseRejectEventAsync(CommandContext commandContext, ParsingStatus parsingStatus, SmtpResponse smtpResponse, ReceiveEventArgs originalEventArgs, CancellationToken cancellationToken)
		{
			SmtpResponse smtpResponseFromMexRuntime = await this.InvokeRaiseRejectEventAsync(parsingStatus, smtpResponse, commandContext, originalEventArgs);
			this.awaitCompletedDelegate(cancellationToken);
			SmtpResponse result;
			if (!smtpResponseFromMexRuntime.IsEmpty)
			{
				result = smtpResponseFromMexRuntime;
			}
			else
			{
				result = smtpResponse;
			}
			return result;
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000E9DE0 File Offset: 0x000E7FE0
		protected virtual SmtpInStateMachineEvents GetCommandFailureEvent()
		{
			return SmtpInStateMachineEvents.CommandFailed;
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x06003933 RID: 14643 RVA: 0x000E9DE3 File Offset: 0x000E7FE3
		private LatencyTracker MailItemLatencyTracker
		{
			get
			{
				if (this.sessionState.TransportMailItem != null)
				{
					return this.sessionState.TransportMailItem.LatencyTracker;
				}
				return null;
			}
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000E9E04 File Offset: 0x000E8004
		private Task<ParseAndProcessResult<SmtpInStateMachineEvents>> RaiseAgentCommandEventAsync(string agentEventTopic, ReceiveCommandEventArgs agentEventArgs, CommandContext commandContext, ParseResult parseResult, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(agentEventTopic) || agentEventArgs == null)
			{
				return SmtpInCommandBase.SmtpResponseEmptyResultTask;
			}
			return this.RaiseAgentEventAsync(agentEventTopic, agentEventArgs, commandContext, parseResult, cancellationToken, null);
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000E9E28 File Offset: 0x000E8028
		private Task<SmtpResponse> InvokeRaiseRejectEventAsync(ParsingStatus parsingStatus, SmtpResponse smtpResponse, CommandContext commandContext, EventArgs originalEventArgs)
		{
			return this.sessionState.SmtpAgentSession.RaiseEventAsync("OnReject", RejectEventSourceImpl.Create(this.sessionState), new RejectEventArgs(this.sessionState)
			{
				RawCommand = commandContext.Command,
				ParsingStatus = EnumConverter.InternalToPublic(parsingStatus),
				SmtpResponse = smtpResponse,
				OriginalArguments = originalEventArgs
			});
		}

		// Token: 0x04001D24 RID: 7460
		protected static readonly ParseAndProcessResult<SmtpInStateMachineEvents> SmtpResponseEmptyResult = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.Empty, SmtpInStateMachineEvents.CommandFailed, false);

		// Token: 0x04001D25 RID: 7461
		protected static readonly Task<ParseAndProcessResult<SmtpInStateMachineEvents>> SmtpResponseEmptyResultTask = Task.FromResult<ParseAndProcessResult<SmtpInStateMachineEvents>>(SmtpInCommandBase.SmtpResponseEmptyResult);

		// Token: 0x04001D26 RID: 7462
		protected readonly INetworkConnection networkConnection;

		// Token: 0x04001D27 RID: 7463
		protected readonly SmtpInSessionState sessionState;

		// Token: 0x04001D28 RID: 7464
		private readonly AwaitCompletedDelegate awaitCompletedDelegate;
	}
}
