using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000461 RID: 1121
	internal class QuitSmtpCommand : SmtpCommand
	{
		// Token: 0x060033EE RID: 13294 RVA: 0x000D1763 File Offset: 0x000CF963
		public QuitSmtpCommand(ISmtpSession session) : base(session, "QUIT", null, LatencyComponent.None)
		{
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000D1774 File Offset: 0x000CF974
		internal override void InboundParseCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.QuitInboundParseCommand);
			ParseResult parseResult = QuitSmtpCommandParser.Parse(CommandContext.FromSmtpCommand(this), SmtpInSessionState.FromSmtpInSession(smtpInSession));
			base.SmtpResponse = parseResult.SmtpResponse;
			base.ParsingStatus = parseResult.ParsingStatus;
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000D17C4 File Offset: 0x000CF9C4
		internal override void InboundProcessCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.QuitInboundProcessCommand);
			base.SmtpResponse = SmtpResponse.Quit;
			smtpInSession.Disconnect(DisconnectReason.QuitVerb);
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000D17F7 File Offset: 0x000CF9F7
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000D17F9 File Offset: 0x000CF9F9
		internal override void OutboundFormatCommand()
		{
			base.ProtocolCommandString = "QUIT";
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Formatted command: {0}", base.ProtocolCommandString);
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000D1824 File Offset: 0x000CFA24
		internal override void OutboundProcessResponse()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Inititating Disconnect with remote host");
			smtpOutSession.Disconnect();
		}
	}
}
