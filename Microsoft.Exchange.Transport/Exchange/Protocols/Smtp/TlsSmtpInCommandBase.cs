using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004D8 RID: 1240
	internal abstract class TlsSmtpInCommandBase : SmtpInCommandBase
	{
		// Token: 0x06003937 RID: 14647 RVA: 0x000E9EAC File Offset: 0x000E80AC
		protected TlsSmtpInCommandBase(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000E9EB8 File Offset: 0x000E80B8
		protected override ParseResult Parse(CommandContext commandContext, out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			ParseResult result = StartTlsSmtpCommandParser.Parse(commandContext, this.sessionState, this.Command);
			if (result.IsFailed)
			{
				agentEventTopic = null;
				agentEventArgs = null;
			}
			else
			{
				agentEventTopic = "OnStartTlsCommand";
				agentEventArgs = new StartTlsCommandEventArgs(this.sessionState);
			}
			return result;
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000EA2E8 File Offset: 0x000E84E8
		protected override async Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsync(CancellationToken cancellationToken)
		{
			ParseAndProcessResult<SmtpInStateMachineEvents> result;
			if (this.ShouldThrottleConnection)
			{
				this.sessionState.ReceivePerfCounters.TlsConnectionsRejectedDueToRateExceeded.Increment();
				result = TlsSmtpInCommandBase.TlsConnectionThrottledResult;
			}
			else
			{
				this.sessionState.AbortMailTransaction();
				object error = await base.WriteToClientAsync(SmtpResponse.StartTlsReadyToNegotiate);
				base.OnAwaitCompleted(cancellationToken);
				if (error != null)
				{
					this.sessionState.HandleNetworkError(error);
					result = TlsSmtpInCommandBase.TlsNegotiationFailureResult;
				}
				else
				{
					this.sessionState.SecureState = this.Command;
					this.sessionState.ProtocolLogSession.LogCertificate("Sending certificate", this.LocalCertificate);
					error = await this.networkConnection.NegotiateTlsAsServerAsync(this.LocalCertificate, this.ShouldRequestClientCertificate);
					base.OnAwaitCompleted(cancellationToken);
					if (error != null)
					{
						this.sessionState.ReceivePerfCounters.TlsNegotiationsFailed.Increment();
						this.sessionState.HandleNetworkError(error);
						this.sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "TLS negotiation failed with error " + error);
						result = TlsSmtpInCommandBase.TlsNegotiationFailureResult;
					}
					else
					{
						ConnectionInfo tlsConnectionInfo = this.networkConnection.TlsConnectionInfo;
						Util.LogTlsSuccessResult(this.sessionState.ProtocolLogSession, tlsConnectionInfo, this.sessionState.NetworkConnection.RemoteCertificate);
						if (this.networkConnection.RemoteCertificate != null)
						{
							this.sessionState.TlsRemoteCertificateInternal = this.networkConnection.RemoteCertificate;
							this.sessionState.ProtocolLogSession.LogCertificateThumbprint("Received certificate", this.networkConnection.RemoteCertificate);
							this.OnClientCertificateReceived(this.networkConnection.RemoteCertificate);
						}
						if (this.networkConnection.IsTls)
						{
							this.sessionState.ReceivePerfCounters.TlsConnectionsCurrent.Increment();
						}
						this.sessionState.AdvertisedEhloOptions.StartTLS = false;
						this.sessionState.AdvertisedEhloOptions.AnonymousTLS = false;
						result = this.CommandCompletedResult;
					}
				}
			}
			return result;
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x0600393A RID: 14650
		protected abstract SecureState Command { get; }

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x0600393B RID: 14651
		protected abstract IX509Certificate2 LocalCertificate { get; }

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x0600393C RID: 14652
		protected abstract bool ShouldRequestClientCertificate { get; }

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x0600393D RID: 14653
		protected abstract ParseAndProcessResult<SmtpInStateMachineEvents> CommandCompletedResult { get; }

		// Token: 0x0600393E RID: 14654 RVA: 0x000EA336 File Offset: 0x000E8536
		protected virtual void OnClientCertificateReceived(IX509Certificate2 remoteCertificate)
		{
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x0600393F RID: 14655 RVA: 0x000EA338 File Offset: 0x000E8538
		protected virtual bool ShouldThrottleConnection
		{
			get
			{
				return SmtpInSessionUtils.ShouldThrottleIncomingTLSConnections(this.sessionState.ServerState.InboundTlsIPConnectionTable, this.sessionState.Configuration.TransportConfiguration.IsReceiveTlsThrottlingEnabled);
			}
		}

		// Token: 0x04001D29 RID: 7465
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> TlsDisabledResult = new ParseAndProcessResult<SmtpInStateMachineEvents>(StartTlsSmtpCommandParser.UnrecognizedCommand, SmtpInStateMachineEvents.CommandFailed);

		// Token: 0x04001D2A RID: 7466
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> TlsConnectionThrottledResult = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.StartTlsTempReject, SmtpInStateMachineEvents.CommandFailed, false);

		// Token: 0x04001D2B RID: 7467
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> TlsNegotiationFailureResult = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.Empty, SmtpInStateMachineEvents.SendResponseAndDisconnectClient, false);
	}
}
