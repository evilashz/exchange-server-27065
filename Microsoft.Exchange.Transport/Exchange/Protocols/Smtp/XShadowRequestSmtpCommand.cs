using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000472 RID: 1138
	internal class XShadowRequestSmtpCommand : SmtpCommand
	{
		// Token: 0x0600347A RID: 13434 RVA: 0x000D5F1B File Offset: 0x000D411B
		public XShadowRequestSmtpCommand(ISmtpSession session, IShadowRedundancyManager shadowRedundancyManager) : base(session, "XSHADOWREQUEST", null, LatencyComponent.None)
		{
			this.shadowRedundancyManager = shadowRedundancyManager;
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000D5F34 File Offset: 0x000D4134
		internal override void InboundParseCommand()
		{
			this.ParseCommand();
			if (base.ParsingStatus != ParsingStatus.Complete)
			{
				ShadowRedundancyManager.ReceiveTracer.TraceError<string>((long)this.GetHashCode(), "XSHADOWREQUEST parsing failed; SMTP Response: {0}", base.SmtpResponse.ToString());
				return;
			}
			ShadowRedundancyManager.ReceiveTracer.TraceDebug((long)this.GetHashCode(), "XSHADOWREQUEST parsing completed");
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000D5F94 File Offset: 0x000D4194
		internal override void InboundProcessCommand()
		{
			if (base.ParsingStatus != ParsingStatus.Complete)
			{
				return;
			}
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.XShadowRequestInboundProcessCommand);
			this.recipientShadowContext = this.shadowRedundancyManager.GetShadowContextForInboundSession();
			base.SmtpResponse = new SmtpResponse("250", null, new string[]
			{
				this.recipientShadowContext
			});
			ShadowRedundancyManager.ReceiveTracer.TraceDebug<string>((long)this.GetHashCode(), "XSHADOWREQUEST accepted; returning shadow context {0}", this.recipientShadowContext);
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000D6011 File Offset: 0x000D4211
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x000D6014 File Offset: 0x000D4214
		internal override void OutboundFormatCommand()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			base.ProtocolCommandString = string.Join(" ", new string[]
			{
				"XSHADOWREQUEST",
				smtpOutSession.Connector.Fqdn.ToString(),
				this.shadowRedundancyManager.DatabaseIdForTransmit
			});
			ShadowRedundancyManager.SendTracer.TraceDebug<string>((long)this.GetHashCode(), "Formatted command : {0}", base.ProtocolCommandString);
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000D608C File Offset: 0x000D428C
		internal override void OutboundProcessResponse()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			if (base.SmtpResponse.SmtpResponseType != SmtpResponseType.Success)
			{
				ShadowRedundancyManager.SendTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "XSHADOWREQUEST rejected: {0}", base.SmtpResponse);
				smtpOutSession.FailoverConnection(base.SmtpResponse);
				smtpOutSession.NextState = SmtpOutSession.SessionState.Quit;
				return;
			}
			ShadowRedundancyManager.SendTracer.TraceDebug((long)this.GetHashCode(), "XSHADOWREQUEST accepted. Parsing the response.");
			if (!this.ParseResponse(base.SmtpResponse))
			{
				return;
			}
			ShadowRedundancyManager.SendTracer.TraceDebug((long)this.GetHashCode(), "Will issue MAIL FROM");
			smtpOutSession.PrepareForNextMessage(false);
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000D612C File Offset: 0x000D432C
		private void ParseCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.XShadowRequestInboundParseCommand);
			if (smtpInSession.IsPeerShadowSession || smtpInSession.IsShadowedBySender)
			{
				base.SmtpResponse = SmtpResponse.BadCommandSequence;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.WrongSequence);
				return;
			}
			if (!base.VerifyEhloReceived() || !base.VerifyNoOngoingBdat() || !base.VerifyNoOngoingMailTransaction())
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.WrongSequence);
				return;
			}
			if (!smtpInSession.AdvertisedEhloOptions.XShadowRequest)
			{
				base.SmtpResponse = SmtpResponse.CommandNotImplemented;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.XShadowRequestNotEnabled);
				return;
			}
			if (!SmtpInSessionUtils.HasSMTPAcceptXShadowPermission(smtpInSession.Permissions))
			{
				base.SmtpResponse = SmtpResponse.NotAuthorized;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.XShadowRequestNotAuthorized);
				return;
			}
			if (smtpInSession.SmtpInServer.RejectSubmits)
			{
				base.SmtpResponse = SmtpResponse.ServiceUnavailable;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				return;
			}
			string nextArg = base.GetNextArg();
			string nextArg2 = base.GetNextArg();
			if (string.IsNullOrEmpty(nextArg) || string.IsNullOrEmpty(nextArg2))
			{
				base.SmtpResponse = SmtpResponse.InvalidArguments;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				return;
			}
			if (nextArg2.Length > 255)
			{
				base.SmtpResponse = SmtpResponse.InvalidArguments;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				return;
			}
			this.shadowRedundancyManager.NotifyPrimaryServerState(nextArg, nextArg2, ShadowRedundancyCompatibilityVersion.E15);
			smtpInSession.PeerSessionPrimaryServer = nextArg;
			base.ParsingStatus = ParsingStatus.Complete;
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000D6280 File Offset: 0x000D4480
		private bool ParseResponse(SmtpResponse response)
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			string smtpHost = smtpOutSession.SmtpHost;
			if (response.StatusText == null || response.StatusText.Length < 1 || string.IsNullOrEmpty(response.StatusText[0]) || response.StatusText[0].Length > 255)
			{
				smtpOutSession.FailoverConnection(response);
				smtpOutSession.NextState = SmtpOutSession.SessionState.Quit;
				this.shadowRedundancyManager.NotifyServerViolatedSmtpContract(smtpHost);
				return false;
			}
			this.recipientShadowContext = response.StatusText[0];
			ShadowPeerNextHopConnection shadowPeerNextHopConnection = smtpOutSession.NextHopConnection as ShadowPeerNextHopConnection;
			if (shadowPeerNextHopConnection == null)
			{
				throw new InvalidOperationException("XShadowRequest command used from non ShadowPeerNextHopConnection");
			}
			shadowPeerNextHopConnection.ShadowSession.ShadowServerContext = this.recipientShadowContext;
			return true;
		}

		// Token: 0x04001AA9 RID: 6825
		private const string ArgumentDelimiter = " ";

		// Token: 0x04001AAA RID: 6826
		private const int PrimaryServerStateMaxLength = 255;

		// Token: 0x04001AAB RID: 6827
		private const int ShadowServerContextMaxLength = 255;

		// Token: 0x04001AAC RID: 6828
		private readonly IShadowRedundancyManager shadowRedundancyManager;

		// Token: 0x04001AAD RID: 6829
		private string recipientShadowContext;
	}
}
