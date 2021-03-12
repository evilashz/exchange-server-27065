using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200046F RID: 1135
	internal class XRsetProxyToSmtpCommand : SmtpCommand
	{
		// Token: 0x06003466 RID: 13414 RVA: 0x000D5856 File Offset: 0x000D3A56
		public XRsetProxyToSmtpCommand(ISmtpSession session) : base(session, "XRSETPROXYTO", null, LatencyComponent.None)
		{
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000D5866 File Offset: 0x000D3A66
		internal override void InboundParseCommand()
		{
			throw new InvalidOperationException("XRSETPROXYTO should not have been created");
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x000D5872 File Offset: 0x000D3A72
		internal override void InboundProcessCommand()
		{
			throw new InvalidOperationException("XRSETPROXYTO should not have been created");
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x000D587E File Offset: 0x000D3A7E
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x000D5880 File Offset: 0x000D3A80
		internal override void OutboundFormatCommand()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			int num = Math.Min(smtpOutSession.MessagesSentOverSession, 999);
			base.ProtocolCommandString = string.Format("XRSETPROXYTO {0} {1}XXX", XProxyToSmtpCommand.FormatSessionIdString(smtpOutSession.SessionId), num.ToString("000"));
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Formatted command: {0}", base.ProtocolCommandString);
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x000D58F0 File Offset: 0x000D3AF0
		internal override void OutboundProcessResponse()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			if (base.SmtpResponse.SmtpResponseType == SmtpResponseType.Success)
			{
				string[] statusText = base.SmtpResponse.StatusText;
				string value = XProxyToSmtpCommand.FormatSessionIdString(smtpOutSession.SessionId);
				if (statusText != null && statusText.Length > 0 && statusText[0].EndsWith(value, StringComparison.OrdinalIgnoreCase))
				{
					ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "XRSETPROXYTO accepted");
					smtpOutSession.XRsetProxyToAccepted = true;
				}
			}
			else
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<SmtpResponse>((long)this.GetHashCode(), "XRSETPROXYTO not accepted: {0}", base.SmtpResponse);
			}
			smtpOutSession.NextState = SmtpOutSession.SessionState.Quit;
		}
	}
}
