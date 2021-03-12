using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000500 RID: 1280
	internal abstract class SmtpInStateMachine<TState, TEvent> : FiniteStateMachine<TState, TEvent> where TState : struct where TEvent : struct
	{
		// Token: 0x06003B0E RID: 15118 RVA: 0x000F6390 File Offset: 0x000F4590
		protected SmtpInStateMachine(SmtpInSessionState sessionState, TState startState, Dictionary<StateTransition<TState, TEvent>, TState> stateTransitions) : base(startState, stateTransitions)
		{
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			ArgumentValidator.ThrowIfNull("stateTransitions", stateTransitions);
			this.sessionState = sessionState;
			this.commandFactory = new Lazy<ISmtpInCommandFactory<TEvent>>(new Func<ISmtpInCommandFactory<TEvent>>(this.CreateCommandFactory));
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x000F6B88 File Offset: 0x000F4D88
		public async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			try
			{
				bool continueExecution = await this.ProcessNewConnection(cancellationToken);
				this.OnAwaitCompleted(cancellationToken);
				if (!continueExecution)
				{
					this.sessionState.DisconnectReason = DisconnectReason.DroppedSession;
					await this.OnDisconnectingAsync(cancellationToken);
					this.OnAwaitCompleted(cancellationToken);
				}
				else
				{
					object writeResult = await this.WriteToClientAsync(this.Banner, true);
					this.OnAwaitCompleted(cancellationToken);
					if (writeResult != null)
					{
						this.sessionState.HandleNetworkError(writeResult);
						await this.OnDisconnectingAsync(cancellationToken);
						this.OnAwaitCompleted(cancellationToken);
					}
					else
					{
						while (!this.ReachedEndState)
						{
							NetworkConnection.LazyAsyncResultWithTimeout readResult = await Util.ReadLineAsync(this.sessionState);
							this.OnAwaitCompleted(cancellationToken);
							if (Util.IsReadFailure(readResult))
							{
								this.sessionState.HandleNetworkError(readResult.Result);
								this.MoveToNextStateAndLogOnFailure(this.NetworkErrorEvent);
								break;
							}
							CommandContext commandContext = CommandContext.FromAsyncResult(readResult);
							SmtpResponse smtpResponse = await this.ProcessCommandLineAsync(commandContext, cancellationToken);
							this.OnAwaitCompleted(cancellationToken);
							this.OnCommandCompleted(commandContext, smtpResponse);
							if (!smtpResponse.IsEmpty)
							{
								writeResult = await this.WriteToClientAsync(smtpResponse, false);
								this.OnAwaitCompleted(cancellationToken);
								if (this.IsWriteFailure(writeResult))
								{
									this.sessionState.HandleNetworkError(writeResult);
									this.MoveToNextStateAndLogOnFailure(this.NetworkErrorEvent);
									break;
								}
							}
						}
						if (this.sessionState.DisconnectReason == DisconnectReason.None)
						{
							this.sessionState.DisconnectReason = DisconnectReason.Local;
						}
						await this.OnDisconnectingAsync(cancellationToken);
						this.OnAwaitCompleted(cancellationToken);
					}
				}
			}
			catch (OperationCanceledException)
			{
				this.IsCancelled = true;
				this.sessionState.ServerState.Tracer.TraceDebug<long>(this.sessionState.SessionId, "Session ID {0} was cancelled", this.sessionState.SessionId);
			}
			catch (Exception ex)
			{
				this.SetupPoisonContext();
				this.sessionState.ServerState.EventLog.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveCatchAll, null, new object[]
				{
					this.sessionState.NetworkConnection.RemoteEndPoint.Address,
					ex
				});
				throw;
			}
			finally
			{
				this.sessionState.Dispose();
				this.sessionState = null;
			}
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06003B10 RID: 15120 RVA: 0x000F6BD6 File Offset: 0x000F4DD6
		// (set) Token: 0x06003B11 RID: 15121 RVA: 0x000F6BDE File Offset: 0x000F4DDE
		public bool IsCancelled { get; private set; }

		// Token: 0x06003B12 RID: 15122 RVA: 0x000F6C14 File Offset: 0x000F4E14
		public string StateTransitionHistoryToString()
		{
			return string.Join(Environment.NewLine, from transition in this.stateTransitionHistory.ToArray()
			select string.Format("{0} -> {1} ({2})", transition.Item1, transition.Item3, transition.Item2));
		}

		// Token: 0x06003B13 RID: 15123
		protected abstract ISmtpInCommandFactory<TEvent> CreateCommandFactory();

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06003B14 RID: 15124
		protected abstract bool ReachedEndState { get; }

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06003B15 RID: 15125
		protected abstract TEvent NetworkErrorEvent { get; }

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06003B16 RID: 15126
		protected abstract SmtpResponse Banner { get; }

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06003B17 RID: 15127
		protected abstract int MaxCommandLength { get; }

		// Token: 0x06003B18 RID: 15128
		protected abstract TEvent GetCompletedEventForCommand(SmtpInCommand commandType);

		// Token: 0x06003B19 RID: 15129
		protected abstract Task<SmtpResponse> OnNewConnectionAsync(IPEndPoint remoteEndPoint, CancellationToken cancellationToken);

		// Token: 0x06003B1A RID: 15130
		protected abstract Task<SmtpResponse> OnUnrecognizedCommandAsync(CommandContext commandContext);

		// Token: 0x06003B1B RID: 15131
		protected abstract Task<SmtpResponse> OnBadCommandSequenceAsync(CommandContext commandContext);

		// Token: 0x06003B1C RID: 15132
		protected abstract void OnCommandReceived(CommandContext commandContext);

		// Token: 0x06003B1D RID: 15133
		protected abstract void OnCommandCompleted(CommandContext commandContext, SmtpResponse smtpResponse);

		// Token: 0x06003B1E RID: 15134
		protected abstract Task OnDisconnectingAsync(CancellationToken cancellationToken);

		// Token: 0x06003B1F RID: 15135 RVA: 0x000F6C4D File Offset: 0x000F4E4D
		protected virtual void OnAwaitCompleted(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x000F6C56 File Offset: 0x000F4E56
		protected override void OnStateTransition(TState currentState, TEvent eventOccurred, TState nextState)
		{
			this.stateTransitionHistory.Enqueue(Tuple.Create<TState, TEvent, TState>(currentState, eventOccurred, nextState));
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x000F6C6C File Offset: 0x000F4E6C
		protected void SetupPoisonContext()
		{
			if (this.sessionState.TransportMailItem != null && !string.IsNullOrEmpty(this.sessionState.TransportMailItem.InternetMessageId))
			{
				PoisonMessage.Context = new MessageContext(this.sessionState.TransportMailItem.RecordId, this.sessionState.TransportMailItem.InternetMessageId, MessageProcessingSource.SmtpReceive);
				return;
			}
			PoisonMessage.Context = null;
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x000F6E8C File Offset: 0x000F508C
		private async Task<bool> ProcessNewConnection(CancellationToken cancellationToken)
		{
			SmtpResponse smtpResponse = await this.OnNewConnectionAsync(this.sessionState.NetworkConnection.RemoteEndPoint, cancellationToken);
			this.OnAwaitCompleted(cancellationToken);
			bool result;
			if (!smtpResponse.IsEmpty)
			{
				await this.WriteToClientAsync(smtpResponse, true);
				this.OnAwaitCompleted(cancellationToken);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x000F7208 File Offset: 0x000F5408
		private async Task<SmtpResponse> ProcessCommandLineAsync(CommandContext commandContext, CancellationToken cancellationToken)
		{
			this.OnCommandReceived(commandContext);
			SmtpResponse result;
			if (commandContext.Length > this.MaxCommandLength)
			{
				result = SmtpResponse.CommandTooLong;
			}
			else
			{
				SmtpInCommand commandType = commandContext.IdentifySmtpCommand();
				if (commandType == SmtpInCommand.UNKNOWN)
				{
					commandContext.LogReceivedCommand(this.sessionState.ProtocolLogSession);
					SmtpResponse smtpResponse = await this.OnUnrecognizedCommandAsync(commandContext);
					this.OnAwaitCompleted(cancellationToken);
					this.sessionState.ProtocolLogSession.LogSend(smtpResponse.ToByteArray());
					result = smtpResponse;
				}
				else
				{
					Tuple<ISmtpInCommand<TEvent>, SmtpResponse> commandOrSmtpResponse = await this.CreateCommandAsync(commandType, commandContext, cancellationToken);
					this.OnAwaitCompleted(cancellationToken);
					if (commandOrSmtpResponse.Item1 == null)
					{
						this.sessionState.ProtocolLogSession.LogSend(commandOrSmtpResponse.Item2.ToByteArray());
						result = commandOrSmtpResponse.Item2;
					}
					else
					{
						SmtpResponse response = await this.ParseAndProcessCommandAsync(commandOrSmtpResponse.Item1, commandContext, cancellationToken);
						this.OnAwaitCompleted(cancellationToken);
						result = response;
					}
				}
			}
			return result;
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x000F74B0 File Offset: 0x000F56B0
		private async Task<Tuple<ISmtpInCommand<TEvent>, SmtpResponse>> CreateCommandAsync(SmtpInCommand commandType, CommandContext commandContext, CancellationToken cancellationToken)
		{
			Tuple<ISmtpInCommand<TEvent>, SmtpResponse> result;
			if (!base.IsValidStateTransition(this.GetCompletedEventForCommand(commandType)))
			{
				commandContext.LogReceivedCommand(this.sessionState.ProtocolLogSession);
				SmtpResponse smtpResponse = await this.OnBadCommandSequenceAsync(commandContext);
				this.OnAwaitCompleted(cancellationToken);
				result = Tuple.Create<ISmtpInCommand<TEvent>, SmtpResponse>(null, smtpResponse);
			}
			else
			{
				ISmtpInCommand<TEvent> command = this.commandFactory.Value.CreateCommand(commandType);
				if (command == null)
				{
					commandContext.LogReceivedCommand(this.sessionState.ProtocolLogSession);
					SmtpResponse smtpResponse2 = await this.OnUnrecognizedCommandAsync(commandContext);
					this.OnAwaitCompleted(cancellationToken);
					result = Tuple.Create<ISmtpInCommand<TEvent>, SmtpResponse>(null, smtpResponse2);
				}
				else
				{
					result = Tuple.Create<ISmtpInCommand<TEvent>, SmtpResponse>(command, SmtpResponse.Empty);
				}
			}
			return result;
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x000F7644 File Offset: 0x000F5844
		private async Task<SmtpResponse> ParseAndProcessCommandAsync(ISmtpInCommand<TEvent> command, CommandContext commandContext, CancellationToken cancellationToken)
		{
			ParseAndProcessResult<TEvent> parseAndProcessResult = await command.ParseAndProcessAsync(commandContext, cancellationToken);
			this.OnAwaitCompleted(cancellationToken);
			command.LogSmtpResponse(parseAndProcessResult.SmtpResponse);
			this.MoveToNextStateAndLogOnFailure(parseAndProcessResult.SmtpEvent);
			return parseAndProcessResult.SmtpResponse;
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x000F76A4 File Offset: 0x000F58A4
		private void MoveToNextStateAndLogOnFailure(TEvent eventOccurred)
		{
			if (!base.TryMoveToNextState(eventOccurred))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(string.Format("Invalid state transition: CurrentState: {0}, eventOccurred: {1}", base.CurrentState, eventOccurred));
				stringBuilder.AppendLine("Recent state transition history for this session:");
				foreach (Tuple<TState, TEvent, TState> tuple in this.stateTransitionHistory.ToArray())
				{
					stringBuilder.AppendLine(string.Format("CurrentState: {0}, eventOccurred: {1}, nextState: {2}", tuple.Item1, tuple.Item2, tuple.Item3));
				}
				Exception ex = new InvalidOperationException(stringBuilder.ToString());
				bool flag;
				ExWatson.SendThrottledGenericWatsonReport("E12", ExWatson.ApplicationVersion.ToString(), ExWatson.AppName, "15.00.1497.010", Assembly.GetExecutingAssembly().GetName().Name, ex.GetType().Name, ex.StackTrace, ex.GetHashCode().ToString(CultureInfo.InvariantCulture), ex.TargetSite.Name, "details", TimeSpan.FromHours(1.0), out flag);
			}
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x000F77C7 File Offset: 0x000F59C7
		private bool IsWriteFailure(object writeResult)
		{
			return writeResult != null;
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x000F77D0 File Offset: 0x000F59D0
		private Task<object> WriteToClientAsync(SmtpResponse smtpResponse, bool logResponse)
		{
			if (logResponse)
			{
				this.sessionState.ProtocolLogSession.LogSend(smtpResponse.ToByteArray());
			}
			return Util.WriteToClientAsync(this.sessionState.NetworkConnection, smtpResponse);
		}

		// Token: 0x04001DDC RID: 7644
		private readonly DiagnosticsHistoryQueue<Tuple<TState, TEvent, TState>> stateTransitionHistory = new DiagnosticsHistoryQueue<Tuple<TState, TEvent, TState>>(100);

		// Token: 0x04001DDD RID: 7645
		private readonly Lazy<ISmtpInCommandFactory<TEvent>> commandFactory;

		// Token: 0x04001DDE RID: 7646
		protected SmtpInSessionState sessionState;
	}
}
