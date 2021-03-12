using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004F8 RID: 1272
	internal class VrfySmtpInCommand : SmtpInCommandBase
	{
		// Token: 0x06003AA1 RID: 15009 RVA: 0x000F39CE File Offset: 0x000F1BCE
		public VrfySmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000F39D8 File Offset: 0x000F1BD8
		protected override ParseResult Parse(CommandContext commandContext, out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			agentEventTopic = null;
			agentEventArgs = null;
			return VrfySmtpCommandParser.Parse(commandContext, this.sessionState);
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000F39EC File Offset: 0x000F1BEC
		protected override Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsync(CancellationToken cancellationToken)
		{
			return VrfySmtpInCommand.ProcessCompletedTask;
		}

		// Token: 0x04001D81 RID: 7553
		public static readonly Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessCompletedTask = Task.FromResult<ParseAndProcessResult<SmtpInStateMachineEvents>>(new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.UnableToVrfyUser, SmtpInStateMachineEvents.VrfyProcessed, false));
	}
}
