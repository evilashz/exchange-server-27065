using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004FA RID: 1274
	internal class XSessionParamsSmtpInCommand : SmtpInCommandBase
	{
		// Token: 0x06003AB0 RID: 15024 RVA: 0x000F3DE4 File Offset: 0x000F1FE4
		public XSessionParamsSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x000F3DF0 File Offset: 0x000F1FF0
		protected override ParseResult Parse(CommandContext commandContext, out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			XSessionParams xsessionParams;
			ParseResult result = XSessionParamsSmtpCommandParser.Parse(commandContext, this.sessionState, out xsessionParams);
			if (result.IsFailed)
			{
				agentEventTopic = null;
				agentEventArgs = null;
			}
			else
			{
				agentEventTopic = "OnXSessionParamsCommand";
				agentEventArgs = new XSessionParamsCommandEventArgs(this.sessionState, xsessionParams.MdbGuid, xsessionParams.Type);
			}
			return result;
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x000F3E3E File Offset: 0x000F203E
		protected override Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsync(CancellationToken cancellationToken)
		{
			return XSessionParamsSmtpInCommand.XSessionParamsCompleteTask;
		}

		// Token: 0x04001D85 RID: 7557
		public static readonly Task<ParseAndProcessResult<SmtpInStateMachineEvents>> XSessionParamsCompleteTask = Task.FromResult<ParseAndProcessResult<SmtpInStateMachineEvents>>(new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.XSessionParamsOk, SmtpInStateMachineEvents.XSessionParamsProcessed, false));
	}
}
