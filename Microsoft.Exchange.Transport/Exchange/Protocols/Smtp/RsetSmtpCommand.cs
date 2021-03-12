using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000465 RID: 1125
	internal class RsetSmtpCommand : SmtpCommand
	{
		// Token: 0x0600341C RID: 13340 RVA: 0x000D29B1 File Offset: 0x000D0BB1
		public RsetSmtpCommand(ISmtpSession session) : base(session, "RSET", "OnRsetCommand", LatencyComponent.None)
		{
			base.IsResponseBuffered = true;
			this.CommandEventArgs = new RsetCommandEventArgs();
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000D29D8 File Offset: 0x000D0BD8
		internal override void InboundParseCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RsetInboundParseCommand);
			ParseResult parseResult = RsetSmtpCommandParser.Parse(CommandContext.FromSmtpCommand(this), SmtpInSessionState.FromSmtpInSession(smtpInSession));
			base.SmtpResponse = parseResult.SmtpResponse;
			base.ParsingStatus = parseResult.ParsingStatus;
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000D2A28 File Offset: 0x000D0C28
		internal override void InboundProcessCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RsetInboundProcessCommand);
			if (smtpInSession.TarpitRset)
			{
				base.LowAuthenticationLevelTarpitOverride = TarpitAction.DoTarpit;
			}
			smtpInSession.TarpitRset = true;
			smtpInSession.AbortMailTransaction();
			base.SmtpResponse = SmtpResponse.Reset;
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000D2A70 File Offset: 0x000D0C70
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000D2A72 File Offset: 0x000D0C72
		internal override void OutboundFormatCommand()
		{
			base.ProtocolCommandString = "RSET";
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000D2A80 File Offset: 0x000D0C80
		internal override void OutboundProcessResponse()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			if (!smtpOutSession.BetweenMessagesRset)
			{
				throw new InvalidOperationException("Error, unexpected call to RSET");
			}
			smtpOutSession.BetweenMessagesRset = false;
			if (smtpOutSession.RoutedMailItem == null)
			{
				throw new InvalidOperationException("Must call PrepareForNextMessage when issuing RSET between messages");
			}
			smtpOutSession.NextState = SmtpOutSession.SessionState.MessageStart;
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Setting Next State: MessageStart");
		}
	}
}
