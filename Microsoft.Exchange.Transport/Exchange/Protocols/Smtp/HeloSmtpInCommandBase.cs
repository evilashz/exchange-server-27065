using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004E5 RID: 1253
	internal abstract class HeloSmtpInCommandBase : SmtpInCommandBase
	{
		// Token: 0x06003A1B RID: 14875 RVA: 0x000F0C29 File Offset: 0x000EEE29
		protected HeloSmtpInCommandBase(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000F0C34 File Offset: 0x000EEE34
		protected override ParseResult Parse(CommandContext commandContext, out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			ParseResult result = HeloSmtpCommandParser.Parse(commandContext, this.sessionState, this.Command, out this.parseOutput);
			if (result.IsFailed)
			{
				agentEventTopic = null;
				agentEventArgs = null;
			}
			else
			{
				this.OnParseComplete(out agentEventTopic, out agentEventArgs);
			}
			return result;
		}

		// Token: 0x06003A1D RID: 14877
		protected abstract void OnParseComplete(out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs);

		// Token: 0x06003A1E RID: 14878 RVA: 0x000F0C74 File Offset: 0x000EEE74
		protected override Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsync(CancellationToken cancellationToken)
		{
			if (this.sessionState.SecureState == SecureState.StartTls)
			{
				this.sessionState.TlsDomainCapabilities = new SmtpReceiveCapabilities?(this.parseOutput.TlsDomainCapabilities);
				this.sessionState.AddSessionPermissions(this.sessionState.Capabilities);
			}
			this.sessionState.AbortMailTransaction();
			return this.ProcessAsyncInternal(cancellationToken);
		}

		// Token: 0x06003A1F RID: 14879
		protected abstract Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsyncInternal(CancellationToken cancellationToken);

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06003A20 RID: 14880
		protected abstract HeloOrEhlo Command { get; }

		// Token: 0x04001D58 RID: 7512
		protected HeloEhloParseOutput parseOutput;
	}
}
