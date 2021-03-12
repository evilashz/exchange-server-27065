using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000468 RID: 1128
	internal class StarttlsSmtpProxyCommand : StarttlsSmtpCommand
	{
		// Token: 0x0600342D RID: 13357 RVA: 0x000D2E33 File Offset: 0x000D1033
		public StarttlsSmtpProxyCommand(ISmtpSession session, ITransportConfiguration transportConfiguration, bool anonymous) : base(session, anonymous)
		{
			if (transportConfiguration == null)
			{
				throw new ArgumentNullException("transportConfiguration");
			}
			this.transportConfiguration = transportConfiguration;
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000D2E52 File Offset: 0x000D1052
		internal override void InboundParseCommand()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000D2E59 File Offset: 0x000D1059
		internal override void InboundProcessCommand()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000D2E60 File Offset: 0x000D1060
		internal override void OutboundFormatCommand()
		{
			base.ProtocolCommandString = base.ProtocolCommandKeyword;
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Issued {0} command", base.ProtocolCommandKeyword);
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000D2E8C File Offset: 0x000D108C
		internal override void OutboundProcessResponse()
		{
			SmtpOutProxySession smtpOutProxySession = (SmtpOutProxySession)base.SmtpSession;
			string statusCode = base.SmtpResponse.StatusCode;
			if (string.Equals(statusCode, "220", StringComparison.Ordinal))
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} command succeeded, will start TLS negotiation", base.ProtocolCommandKeyword);
				smtpOutProxySession.StartTls(this.anonymous ? SecureState.AnonymousTls : SecureState.StartTls);
				return;
			}
			this.HandleStartTlsErrorsProxy();
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000D2EF8 File Offset: 0x000D10F8
		private void HandleStartTlsErrorsProxy()
		{
			SmtpOutProxySession smtpOutProxySession = (SmtpOutProxySession)base.SmtpSession;
			if (smtpOutProxySession.TlsConfiguration.RequireTls || this.anonymous)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string, SmtpResponse>((long)this.GetHashCode(), "{0} command failed with response {1}", base.ProtocolCommandKeyword, base.SmtpResponse);
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "The session will be terminated");
				smtpOutProxySession.AckConnection(AckStatus.Retry, base.SmtpResponse, SessionSetupFailureReason.ProtocolError);
				smtpOutProxySession.NextState = SmtpOutSession.SessionState.Quit;
				return;
			}
			if (smtpOutProxySession.CheckRequireOorg())
			{
				if (smtpOutProxySession.IsClientProxy)
				{
					EHLOSmtpProxyCommand.DetermineNextStateForClientProxySession(smtpOutProxySession, this.transportConfiguration, this.GetHashCode());
					return;
				}
				smtpOutProxySession.IsProxying = true;
			}
		}

		// Token: 0x04001A83 RID: 6787
		private ITransportConfiguration transportConfiguration;
	}
}
