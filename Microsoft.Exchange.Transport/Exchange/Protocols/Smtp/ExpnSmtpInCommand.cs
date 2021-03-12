using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004E7 RID: 1255
	internal class ExpnSmtpInCommand : SmtpInCommandBase
	{
		// Token: 0x06003A39 RID: 14905 RVA: 0x000F1261 File Offset: 0x000EF461
		public ExpnSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x000F126B File Offset: 0x000EF46B
		protected override ParseResult Parse(CommandContext commandContext, out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			agentEventTopic = null;
			agentEventArgs = null;
			return ExpnSmtpCommandParser.Parse(commandContext, this.sessionState);
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000F127F File Offset: 0x000EF47F
		protected override Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsync(CancellationToken cancellationToken)
		{
			return ExpnSmtpInCommand.ProcessCompletedTask;
		}

		// Token: 0x04001D5A RID: 7514
		public static readonly Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessCompletedTask = Task.FromResult<ParseAndProcessResult<SmtpInStateMachineEvents>>(new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.CommandNotImplemented, SmtpInStateMachineEvents.ExpnProcessed, false));
	}
}
