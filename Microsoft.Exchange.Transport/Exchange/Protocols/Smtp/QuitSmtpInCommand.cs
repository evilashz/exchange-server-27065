using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004F1 RID: 1265
	internal class QuitSmtpInCommand : SmtpInCommandBase
	{
		// Token: 0x06003A71 RID: 14961 RVA: 0x000F3185 File Offset: 0x000F1385
		public QuitSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000F318F File Offset: 0x000F138F
		protected override ParseResult Parse(CommandContext commandContext, out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			agentEventTopic = null;
			agentEventArgs = null;
			return QuitSmtpCommandParser.Parse(commandContext, this.sessionState);
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x000F31A3 File Offset: 0x000F13A3
		protected override Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsync(CancellationToken cancellationToken)
		{
			this.sessionState.DisconnectReason = DisconnectReason.QuitVerb;
			return QuitSmtpInCommand.ProcessCompletedTask;
		}

		// Token: 0x04001D6F RID: 7535
		public static readonly Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessCompletedTask = Task.FromResult<ParseAndProcessResult<SmtpInStateMachineEvents>>(new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.Quit, SmtpInStateMachineEvents.QuitProcessed, false));
	}
}
