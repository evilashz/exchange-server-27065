using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004E9 RID: 1257
	internal class HelpSmtpInCommand : SmtpInCommandBase
	{
		// Token: 0x06003A41 RID: 14913 RVA: 0x000F1340 File Offset: 0x000EF540
		public HelpSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x000F134C File Offset: 0x000EF54C
		protected override ParseResult Parse(CommandContext commandContext, out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			string helpArg;
			ParseResult result = HelpSmtpCommandParser.Parse(commandContext, this.sessionState, out helpArg);
			if (result.IsFailed)
			{
				agentEventTopic = null;
				agentEventArgs = null;
			}
			else
			{
				agentEventTopic = "OnHelpCommand";
				agentEventArgs = new HelpCommandEventArgs(this.sessionState, helpArg);
			}
			return result;
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x000F1494 File Offset: 0x000EF694
		protected override async Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsync(CancellationToken cancellationToken)
		{
			await this.TarpitAsync(this.sessionState.ReceiveConnector.TarpitInterval, cancellationToken);
			base.OnAwaitCompleted(cancellationToken);
			return HelpSmtpInCommand.HelpCompleteTask;
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x000F14E2 File Offset: 0x000EF6E2
		protected virtual Task TarpitAsync(EnhancedTimeSpan tarpitInterval, CancellationToken cancellationToken)
		{
			return Task.Delay(tarpitInterval, cancellationToken);
		}

		// Token: 0x04001D5B RID: 7515
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> HelpCompleteTask = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.Help, SmtpInStateMachineEvents.HelpProcessed, false);
	}
}
