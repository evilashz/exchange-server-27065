using System;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000466 RID: 1126
	internal class RsetShadowSmtpCommand : RsetSmtpCommand
	{
		// Token: 0x06003422 RID: 13346 RVA: 0x000D2AE3 File Offset: 0x000D0CE3
		public RsetShadowSmtpCommand(ISmtpSession session) : base(session)
		{
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000D2AEC File Offset: 0x000D0CEC
		internal override void InboundParseCommand()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x000D2AF3 File Offset: 0x000D0CF3
		internal override void InboundProcessCommand()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x000D2AFC File Offset: 0x000D0CFC
		internal override void OutboundProcessResponse()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			if (!smtpOutSession.AdvertisedEhloOptions.XShadowRequest)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "EHLO response did not advertise XSHADOWREQUEST, failing over");
				smtpOutSession.FailoverConnection(base.SmtpResponse);
				smtpOutSession.NextState = SmtpOutSession.SessionState.Quit;
				return;
			}
			smtpOutSession.NextState = SmtpOutSession.SessionState.XShadowRequest;
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Setting Next State: XShadowRequest");
		}
	}
}
