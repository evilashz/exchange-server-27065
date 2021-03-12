using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000470 RID: 1136
	internal class XSessionParamsSmtpCommand : SmtpCommand
	{
		// Token: 0x0600346C RID: 13420 RVA: 0x000D598E File Offset: 0x000D3B8E
		public XSessionParamsSmtpCommand(ISmtpSession session) : base(session, "XSESSIONPARAMS", "OnXSessionParamsCommand", LatencyComponent.None)
		{
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x000D59A4 File Offset: 0x000D3BA4
		internal override void InboundParseCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.XSessionParamsInboundParseCommand);
			if (!base.VerifyEhloReceived() || !base.VerifyNoOngoingMailTransaction())
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.WrongSequence);
				return;
			}
			XSessionParams xsessionParams;
			ParseResult parseResult = XSessionParamsSmtpCommandParser.Parse(CommandContext.FromSmtpCommand(this), SmtpInSessionState.FromSmtpInSession(smtpInSession), out xsessionParams);
			if (parseResult.IsFailed)
			{
				if (parseResult.SmtpResponse == SmtpResponse.NotAuthorized)
				{
					smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.XSessionParamsNotAuthorized);
				}
				else if (parseResult.SmtpResponse == SmtpResponse.CommandNotImplemented)
				{
					smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.XSessionParamsNotEnabled);
				}
			}
			else
			{
				this.CommandEventArgs = new XSessionParamsCommandEventArgs(smtpInSession.SessionSource, xsessionParams.MdbGuid, xsessionParams.Type);
			}
			base.SmtpResponse = parseResult.SmtpResponse;
			base.ParsingStatus = parseResult.ParsingStatus;
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x000D5A78 File Offset: 0x000D3C78
		internal override void InboundProcessCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.XSessionParamsInboundProcessCommand);
			if (base.ParsingStatus == ParsingStatus.Complete)
			{
				base.SmtpResponse = SmtpResponse.XSessionParamsOk;
			}
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x000D5AB0 File Offset: 0x000D3CB0
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x000D5AB4 File Offset: 0x000D3CB4
		internal override void OutboundFormatCommand()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			StringBuilder stringBuilder = new StringBuilder("XSESSIONPARAMS");
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " {0}={1}", new object[]
			{
				"MDBGUID",
				smtpOutSession.NextHopConnection.Key.NextHopConnector.ToString("N")
			});
			base.ProtocolCommandString = stringBuilder.ToString();
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000D5B2C File Offset: 0x000D3D2C
		internal override void OutboundProcessResponse()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			if (base.SmtpResponse.SmtpResponseType == SmtpResponseType.Success)
			{
				smtpOutSession.PrepareNextStateForEstablishedSession();
				return;
			}
			smtpOutSession.FailoverConnection(base.SmtpResponse);
			smtpOutSession.NextState = SmtpOutSession.SessionState.Quit;
		}
	}
}
