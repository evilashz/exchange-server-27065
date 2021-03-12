using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000459 RID: 1113
	internal class EHLOShadowSmtpCommand : EHLOSmtpCommand
	{
		// Token: 0x06003392 RID: 13202 RVA: 0x000CED96 File Offset: 0x000CCF96
		public EHLOShadowSmtpCommand(ISmtpSession session, ITransportConfiguration transportConfiguration) : base(session, transportConfiguration)
		{
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x000CEDA0 File Offset: 0x000CCFA0
		internal override void InboundParseCommand()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x000CEDA7 File Offset: 0x000CCFA7
		internal override void InboundProcessCommand()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x000CEDB0 File Offset: 0x000CCFB0
		internal override void OutboundProcessResponse()
		{
			ShadowSmtpOutSession shadowSmtpOutSession = (ShadowSmtpOutSession)base.SmtpSession;
			string statusCode = base.SmtpResponse.StatusCode;
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string, SmtpResponse>((long)this.GetHashCode(), "EHLOShadowSmtpCommand.OutboundProcessResponse. Status Code: {0} Response {1}", statusCode, base.SmtpResponse);
			if (base.SmtpResponse.SmtpResponseType != SmtpResponseType.Success)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "EHLO failed with response {0}", base.SmtpResponse);
				shadowSmtpOutSession.FailoverConnection(base.SmtpResponse);
				shadowSmtpOutSession.NextState = SmtpOutSession.SessionState.Quit;
				return;
			}
			shadowSmtpOutSession.AdvertisedEhloOptions.ParseResponse(base.SmtpResponse, shadowSmtpOutSession.RemoteEndPoint.Address);
			if (!shadowSmtpOutSession.AdvertisedEhloOptions.XShadowRequest)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "EHLO response did not advertise XSHADOWREQUEST, failing over");
				shadowSmtpOutSession.FailoverConnection(base.SmtpResponse);
				shadowSmtpOutSession.NextState = SmtpOutSession.SessionState.Quit;
				return;
			}
			base.OutboundProcessResponse();
		}
	}
}
