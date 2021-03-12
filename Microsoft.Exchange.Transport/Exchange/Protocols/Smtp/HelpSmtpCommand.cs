using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200045D RID: 1117
	internal class HelpSmtpCommand : SmtpCommand
	{
		// Token: 0x060033AA RID: 13226 RVA: 0x000CFB7A File Offset: 0x000CDD7A
		public HelpSmtpCommand(ISmtpSession session) : base(session, "HELP", "OnHelpCommand", LatencyComponent.None)
		{
			this.helpCommandEventArgs = new HelpCommandEventArgs();
			this.CommandEventArgs = this.helpCommandEventArgs;
		}

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x060033AB RID: 13227 RVA: 0x000CFBA5 File Offset: 0x000CDDA5
		// (set) Token: 0x060033AC RID: 13228 RVA: 0x000CFBB2 File Offset: 0x000CDDB2
		internal string HelpArg
		{
			get
			{
				return this.helpCommandEventArgs.HelpArgument;
			}
			set
			{
				this.helpCommandEventArgs.HelpArgument = value;
			}
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x000CFBC0 File Offset: 0x000CDDC0
		internal override void InboundParseCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (!base.VerifyNoOngoingBdat())
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.WrongSequence);
				return;
			}
			string helpArg;
			ParseResult parseResult = HelpSmtpCommandParser.Parse(CommandContext.FromSmtpCommand(this), SmtpInSessionState.FromSmtpInSession(smtpInSession), out helpArg);
			if (!parseResult.IsFailed)
			{
				this.HelpArg = helpArg;
			}
			base.SmtpResponse = parseResult.SmtpResponse;
			base.ParsingStatus = parseResult.ParsingStatus;
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x000CFC28 File Offset: 0x000CDE28
		internal override void InboundProcessCommand()
		{
			base.LowAuthenticationLevelTarpitOverride = TarpitAction.DoTarpit;
			base.SmtpResponse = SmtpResponse.Help;
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000CFC3C File Offset: 0x000CDE3C
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000CFC3E File Offset: 0x000CDE3E
		internal override void OutboundFormatCommand()
		{
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000CFC40 File Offset: 0x000CDE40
		internal override void OutboundProcessResponse()
		{
		}

		// Token: 0x04001A2A RID: 6698
		private readonly HelpCommandEventArgs helpCommandEventArgs;
	}
}
