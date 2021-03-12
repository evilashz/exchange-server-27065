using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004F0 RID: 1264
	internal class NoopSmtpInCommand : SmtpInCommandBase
	{
		// Token: 0x06003A6C RID: 14956 RVA: 0x000F2FC4 File Offset: 0x000F11C4
		public NoopSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x000F2FD0 File Offset: 0x000F11D0
		protected override ParseResult Parse(CommandContext commandContext, out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			ParseResult result = NoopSmtpCommandParser.Parse(commandContext, this.sessionState);
			if (result.IsFailed)
			{
				agentEventTopic = null;
				agentEventArgs = null;
			}
			else
			{
				agentEventTopic = "OnNoopCommand";
				agentEventArgs = new NoopCommandEventArgs(this.sessionState);
			}
			return result;
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x000F3114 File Offset: 0x000F1314
		protected override async Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsync(CancellationToken cancellationToken)
		{
			await this.TarpitAsync(this.sessionState.ReceiveConnector.TarpitInterval, cancellationToken);
			base.OnAwaitCompleted(cancellationToken);
			return NoopSmtpInCommand.ProcessCompletedTask;
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x000F3162 File Offset: 0x000F1362
		protected virtual Task TarpitAsync(EnhancedTimeSpan tarpitInterval, CancellationToken cancellationToken)
		{
			return Task.Delay(tarpitInterval, cancellationToken);
		}

		// Token: 0x04001D6E RID: 7534
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> ProcessCompletedTask = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.NoopOk, SmtpInStateMachineEvents.NoopProcessed, false);
	}
}
