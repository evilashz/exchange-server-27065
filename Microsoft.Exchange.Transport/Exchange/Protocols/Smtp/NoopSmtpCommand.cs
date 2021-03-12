using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200045F RID: 1119
	internal class NoopSmtpCommand : SmtpCommand
	{
		// Token: 0x060033D8 RID: 13272 RVA: 0x000D117A File Offset: 0x000CF37A
		public NoopSmtpCommand(ISmtpSession session) : base(session, "NOOP", "OnNoopCommand", LatencyComponent.None)
		{
			this.CommandEventArgs = new NoopCommandEventArgs();
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000D119C File Offset: 0x000CF39C
		internal override void InboundParseCommand()
		{
			ISmtpInSession session = (ISmtpInSession)base.SmtpSession;
			ParseResult parseResult = NoopSmtpCommandParser.Parse(CommandContext.FromSmtpCommand(this), SmtpInSessionState.FromSmtpInSession(session));
			base.ParsingStatus = parseResult.ParsingStatus;
			base.SmtpResponse = parseResult.SmtpResponse;
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000D11E1 File Offset: 0x000CF3E1
		internal override void InboundProcessCommand()
		{
			base.LowAuthenticationLevelTarpitOverride = TarpitAction.DoTarpit;
			base.SmtpResponse = SmtpResponse.NoopOk;
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000D11F5 File Offset: 0x000CF3F5
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x000D11F7 File Offset: 0x000CF3F7
		internal override void OutboundFormatCommand()
		{
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x000D11F9 File Offset: 0x000CF3F9
		internal override void OutboundProcessResponse()
		{
		}
	}
}
