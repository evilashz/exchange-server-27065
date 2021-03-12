using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004F3 RID: 1267
	internal class RsetSmtpInCommand : SmtpInCommandBase
	{
		// Token: 0x06003A7B RID: 14971 RVA: 0x000F3446 File Offset: 0x000F1646
		public RsetSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x000F3450 File Offset: 0x000F1650
		protected override ParseResult Parse(CommandContext commandContext, out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			ParseResult result = RsetSmtpCommandParser.Parse(commandContext, this.sessionState);
			if (result.IsFailed)
			{
				agentEventTopic = null;
				agentEventArgs = null;
			}
			else
			{
				agentEventTopic = "OnRsetCommand";
				agentEventArgs = new RsetCommandEventArgs(this.sessionState);
			}
			return result;
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x000F3490 File Offset: 0x000F1690
		protected override Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsync(CancellationToken cancellationToken)
		{
			this.sessionState.AbortMailTransaction();
			return RsetSmtpInCommand.ProcessCompletedTask;
		}

		// Token: 0x04001D73 RID: 7539
		public static readonly Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessCompletedTask = Task.FromResult<ParseAndProcessResult<SmtpInStateMachineEvents>>(new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.Reset, SmtpInStateMachineEvents.RsetProcessed, false));
	}
}
