using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200045C RID: 1116
	internal class HELOSmtpProxyCommand : SmtpCommand
	{
		// Token: 0x060033A4 RID: 13220 RVA: 0x000CF9CF File Offset: 0x000CDBCF
		public HELOSmtpProxyCommand(ISmtpSession session) : base(session, "HELO", "OnHeloCommand", LatencyComponent.None)
		{
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000CF9E3 File Offset: 0x000CDBE3
		internal override void InboundParseCommand()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x000CF9EA File Offset: 0x000CDBEA
		internal override void InboundProcessCommand()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x000CF9F4 File Offset: 0x000CDBF4
		internal override void OutboundCreateCommand()
		{
			SmtpOutProxySession smtpOutProxySession = (SmtpOutProxySession)base.SmtpSession;
			if (smtpOutProxySession.IsClientProxy)
			{
				throw new InvalidOperationException("Helo should not be sent if proxying a client session");
			}
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x000CFA20 File Offset: 0x000CDC20
		internal override void OutboundFormatCommand()
		{
			base.ProtocolCommandString = "HELO " + base.SmtpSession.HelloDomain;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000CFA40 File Offset: 0x000CDC40
		internal override void OutboundProcessResponse()
		{
			SmtpOutProxySession smtpOutProxySession = (SmtpOutProxySession)base.SmtpSession;
			if (base.SmtpResponse.SmtpResponseType == SmtpResponseType.TransientError)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "HELO failed with response {0}", base.SmtpResponse);
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "Initiating failover");
				smtpOutProxySession.FailoverConnection(base.SmtpResponse, SessionSetupFailureReason.ProtocolError);
				smtpOutProxySession.NextState = SmtpOutSession.SessionState.Quit;
				return;
			}
			if (base.SmtpResponse.SmtpResponseType != SmtpResponseType.Success)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "HELO command failed with response {0}", base.SmtpResponse);
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "The session will be terminated");
				smtpOutProxySession.AckConnection(AckStatus.Retry, base.SmtpResponse, SessionSetupFailureReason.ProtocolError);
				smtpOutProxySession.NextState = SmtpOutSession.SessionState.Quit;
				return;
			}
			if (!smtpOutProxySession.CheckRequireOorg())
			{
				return;
			}
			smtpOutProxySession.AdvertisedEhloOptions.ParseHeloResponse(base.SmtpResponse);
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "HELO command succeeded");
			smtpOutProxySession.IsProxying = true;
			if (!smtpOutProxySession.IsClientProxy)
			{
				SmtpResponse blindProxySuccessfulInboundResponse;
				if (XProxyToSmtpCommand.TryGetInboundXProxyToResponse(this.GetHashCode(), smtpOutProxySession, 2000, base.SmtpResponse, out blindProxySuccessfulInboundResponse))
				{
					smtpOutProxySession.BlindProxySuccessfulInboundResponse = blindProxySuccessfulInboundResponse;
					return;
				}
				smtpOutProxySession.BlindProxySuccessfulInboundResponse = base.SmtpResponse;
			}
		}
	}
}
