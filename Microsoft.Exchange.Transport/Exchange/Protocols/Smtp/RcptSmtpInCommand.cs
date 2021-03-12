using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004F2 RID: 1266
	internal class RcptSmtpInCommand : SmtpInCommandBase
	{
		// Token: 0x06003A75 RID: 14965 RVA: 0x000F31D0 File Offset: 0x000F13D0
		public RcptSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x000F31DA File Offset: 0x000F13DA
		protected override LatencyComponent LatencyComponent
		{
			get
			{
				return LatencyComponent.SmtpReceiveOnRcptCommand;
			}
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x000F31E0 File Offset: 0x000F13E0
		protected override ParseResult Parse(CommandContext commandContext, out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			ParseResult result = RcptSmtpCommandParser.Parse(commandContext, this.sessionState, this.sessionState.ServerState.IsDataRedactionNecessary, this.sessionState.Configuration, this.sessionState.Configuration.TransportConfiguration.SmtpAcceptAnyRecipient, out this.rcptParseOutput);
			if (result.IsFailed)
			{
				agentEventTopic = null;
				agentEventArgs = null;
			}
			else
			{
				agentEventTopic = "OnRcptCommand";
				agentEventArgs = new RcptCommandEventArgs(this.sessionState)
				{
					MailItem = this.sessionState.TransportMailItemWrapper,
					Notify = EnumConverter.InternalToPublic(this.rcptParseOutput.Notify),
					OriginalRecipient = this.rcptParseOutput.ORcpt,
					RecipientAddress = this.rcptParseOutput.RecipientAddress
				};
			}
			return result;
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x000F32A4 File Offset: 0x000F14A4
		protected override Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsync(CancellationToken cancellationToken)
		{
			if (!this.sessionState.RecipientCorrelator.Contains(this.rcptParseOutput.RecipientAddress.ToString()))
			{
				this.mailRecipient = this.sessionState.TransportMailItem.Recipients.Add(this.rcptParseOutput.RecipientAddress.ToString());
				this.mailRecipient.ORcpt = this.rcptParseOutput.ORcpt;
				this.mailRecipient.DsnRequested = this.rcptParseOutput.Notify;
				if (this.rcptParseOutput.Orar != RoutingAddress.Empty)
				{
					OrarGenerator.SetOrarAddress(this.mailRecipient, this.rcptParseOutput.Orar);
				}
				if (this.rcptParseOutput.RDst != null)
				{
					this.mailRecipient.ExtendedProperties.SetValue<string>("Microsoft.Exchange.Transport.RoutingOverride", this.rcptParseOutput.RDst);
				}
				if (SmtpInSessionUtils.IsPeerShadowSession(this.sessionState.PeerSessionPrimaryServer))
				{
					ShadowRedundancyManager.PrepareRecipientForShadowing(this.mailRecipient, this.sessionState.PeerSessionPrimaryServer);
				}
				this.sessionState.RecipientCorrelator.Add(this.mailRecipient);
			}
			else if (this.rcptParseOutput.Orar != RoutingAddress.Empty)
			{
				MailRecipient recipient = this.sessionState.RecipientCorrelator.Find(this.rcptParseOutput.RecipientAddress);
				if (!OrarGenerator.ContainsOrar(recipient))
				{
					OrarGenerator.SetOrarAddress(recipient, this.rcptParseOutput.Orar);
				}
			}
			return RcptSmtpInCommand.RcptCompleteTask;
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000F342A File Offset: 0x000F162A
		protected override void LogCommandReceived(CommandContext command)
		{
		}

		// Token: 0x04001D70 RID: 7536
		public static readonly Task<ParseAndProcessResult<SmtpInStateMachineEvents>> RcptCompleteTask = Task.FromResult<ParseAndProcessResult<SmtpInStateMachineEvents>>(new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.RcptToOk, SmtpInStateMachineEvents.RcptProcessed, false));

		// Token: 0x04001D71 RID: 7537
		private RcptParseOutput rcptParseOutput;

		// Token: 0x04001D72 RID: 7538
		protected MailRecipient mailRecipient;
	}
}
