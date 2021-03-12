using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000504 RID: 1284
	internal sealed class MailboxTransportSmtpInCommandFactory : ISmtpInCommandFactory<SmtpInStateMachineEvents>
	{
		// Token: 0x06003B3F RID: 15167 RVA: 0x000F7FCC File Offset: 0x000F61CC
		public MailboxTransportSmtpInCommandFactory(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate)
		{
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			ArgumentValidator.ThrowIfNull("awaitCompletedDelegate", awaitCompletedDelegate);
			this.sessionState = sessionState;
			this.awaitCompletedDelegate = awaitCompletedDelegate;
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x000F7FF8 File Offset: 0x000F61F8
		public ISmtpInCommand<SmtpInStateMachineEvents> CreateCommand(SmtpInCommand commandType)
		{
			switch (commandType)
			{
			case SmtpInCommand.AUTH:
				return new AuthSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.BDAT:
				return new BdatSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.DATA:
				return new DataSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.EHLO:
				return new EhloSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.EXPN:
				return new ExpnSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.HELO:
				return new HeloSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.HELP:
				return new HelpSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.MAIL:
				return new MailSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.NOOP:
				return new NoopSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.QUIT:
				return new QuitSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.RCPT:
				return new RcptSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.RSET:
				return new RsetSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.STARTTLS:
				return new StartTlsSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.VRFY:
				return new VrfySmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.XANONYMOUSTLS:
				return new AnonymousTlsSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.XEXPS:
				return new XExpsSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			case SmtpInCommand.XSESSIONPARAMS:
				return new XSessionParamsSmtpInCommand(this.sessionState, this.awaitCompletedDelegate);
			}
			return null;
		}

		// Token: 0x04001DE2 RID: 7650
		private readonly SmtpInSessionState sessionState;

		// Token: 0x04001DE3 RID: 7651
		private readonly AwaitCompletedDelegate awaitCompletedDelegate;
	}
}
