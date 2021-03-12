using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000501 RID: 1281
	internal abstract class TransportSmtpInStateMachine<TState> : SmtpInStateMachine<TState, SmtpInStateMachineEvents> where TState : struct
	{
		// Token: 0x06003B2A RID: 15146 RVA: 0x000F77FD File Offset: 0x000F59FD
		protected TransportSmtpInStateMachine(SmtpInSessionState sessionState, TState startState, Dictionary<StateTransition<TState, SmtpInStateMachineEvents>, TState> stateTransitions) : base(sessionState, startState, stateTransitions)
		{
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06003B2B RID: 15147 RVA: 0x000F7808 File Offset: 0x000F5A08
		protected override SmtpInStateMachineEvents NetworkErrorEvent
		{
			get
			{
				return SmtpInStateMachineEvents.NetworkError;
			}
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06003B2C RID: 15148 RVA: 0x000F7822 File Offset: 0x000F5A22
		protected override SmtpResponse Banner
		{
			get
			{
				return Util.SmtpBanner(this.sessionState.ReceiveConnector, () => this.sessionState.Configuration.TransportConfiguration.PhysicalMachineName, this.sessionState.Configuration.TransportConfiguration.Version, this.sessionState.UtcNow, true);
			}
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06003B2D RID: 15149 RVA: 0x000F7861 File Offset: 0x000F5A61
		protected override int MaxCommandLength
		{
			get
			{
				return 32768;
			}
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x000F7AAC File Offset: 0x000F5CAC
		protected override async Task<SmtpResponse> OnNewConnectionAsync(IPEndPoint remoteEndPoint, CancellationToken cancellationToken)
		{
			this.IncrementConnectionPerformanceCounters();
			SmtpResponse result;
			if (this.sessionState.ServerState.RejectCommands)
			{
				this.sessionState.Tracer.TraceDebug<long>((long)this.GetHashCode(), "Session (id={0}) disconnected: RejectCommands==true", this.sessionState.NetworkConnection.ConnectionId);
				if (this.sessionState.ServerState.RejectionSmtpResponse.Equals(SmtpResponse.InsufficientResource))
				{
					this.sessionState.UpdateAvailabilityPerfCounters(LegitimateSmtpAvailabilityCategory.RejectDueToBackPressure);
				}
				result = this.sessionState.ServerState.RejectionSmtpResponse;
			}
			else
			{
				SmtpResponse smtpResponseFromMexRuntime = await this.sessionState.SmtpAgentSession.RaiseEventAsync("OnConnectEvent", ConnectEventSourceImpl.Create(this.sessionState), new ConnectEventArgs(this.sessionState));
				this.OnAwaitCompleted(cancellationToken);
				if (!smtpResponseFromMexRuntime.IsEmpty)
				{
					result = smtpResponseFromMexRuntime;
				}
				else if (this.sessionState.ShouldDisconnect)
				{
					result = (this.sessionState.SmtpResponse.IsEmpty ? SmtpResponse.ConnectionDroppedByAgentError : this.sessionState.SmtpResponse);
				}
				else
				{
					result = SmtpResponse.Empty;
				}
			}
			return result;
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000F7C28 File Offset: 0x000F5E28
		protected override async Task OnDisconnectingAsync(CancellationToken cancellationToken)
		{
			await this.sessionState.SmtpAgentSession.RaiseEventAsync("OnDisconnectEvent", DisconnectEventSourceImpl.Create(this.sessionState), new DisconnectEventArgs(this.sessionState));
			this.OnAwaitCompleted(cancellationToken);
			this.sessionState.OnDisconnect();
			this.DecrementConnectionPerformanceCounters();
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x000F7C76 File Offset: 0x000F5E76
		protected override Task<SmtpResponse> OnUnrecognizedCommandAsync(CommandContext commandContext)
		{
			return CompletedTasks.SmtpResponseUnrecognizedCommand;
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x000F7C7D File Offset: 0x000F5E7D
		protected override Task<SmtpResponse> OnBadCommandSequenceAsync(CommandContext commandContext)
		{
			return CompletedTasks.SmtpResponseBadCommandSequence;
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000F7C84 File Offset: 0x000F5E84
		protected override void OnCommandReceived(CommandContext commandContext)
		{
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000F7C86 File Offset: 0x000F5E86
		protected override void OnCommandCompleted(CommandContext commandContext, SmtpResponse smtpResponse)
		{
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000F7C88 File Offset: 0x000F5E88
		private void IncrementConnectionPerformanceCounters()
		{
			ISmtpReceivePerfCounters smtpReceivePerfCounterInstance = this.sessionState.ReceiveConnectorStub.SmtpReceivePerfCounterInstance;
			if (smtpReceivePerfCounterInstance != null)
			{
				smtpReceivePerfCounterInstance.ConnectionsCurrent.Increment();
				smtpReceivePerfCounterInstance.ConnectionsTotal.Increment();
			}
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x000F7CC4 File Offset: 0x000F5EC4
		private void DecrementConnectionPerformanceCounters()
		{
			ISmtpReceivePerfCounters smtpReceivePerfCounterInstance = this.sessionState.ReceiveConnectorStub.SmtpReceivePerfCounterInstance;
			if (smtpReceivePerfCounterInstance != null)
			{
				smtpReceivePerfCounterInstance.ConnectionsCurrent.Decrement();
				if (this.sessionState.NetworkConnection.IsTls)
				{
					smtpReceivePerfCounterInstance.TlsConnectionsCurrent.Decrement();
				}
			}
		}
	}
}
