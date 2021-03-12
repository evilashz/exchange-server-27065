using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000471 RID: 1137
	internal class XShadowSmtpCommand : SmtpCommand
	{
		// Token: 0x06003472 RID: 13426 RVA: 0x000D5B71 File Offset: 0x000D3D71
		public XShadowSmtpCommand(ISmtpSession session, IShadowRedundancyManager shadowRedundancyManager) : base(session, "XSHADOW", null, LatencyComponent.None)
		{
			this.shadowRedundancyManager = shadowRedundancyManager;
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000D5B88 File Offset: 0x000D3D88
		internal override void InboundParseCommand()
		{
			this.ParseCommand();
			if (base.ParsingStatus != ParsingStatus.Complete)
			{
				ShadowRedundancyManager.ReceiveTracer.TraceError<string>((long)this.GetHashCode(), "XSHADOW parsing failed; SMTP Response: {0}", base.SmtpResponse.ToString());
				return;
			}
			ShadowRedundancyManager.ReceiveTracer.TraceDebug((long)this.GetHashCode(), "XSHADOW parsing completed");
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000D5BE8 File Offset: 0x000D3DE8
		internal override void InboundProcessCommand()
		{
			if (base.ParsingStatus != ParsingStatus.Complete)
			{
				return;
			}
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.XShadowInboundProcessCommand);
			base.SmtpResponse = new SmtpResponse("250", null, new string[]
			{
				this.shadowRedundancyManager.DatabaseIdForTransmit
			});
			smtpInSession.SenderShadowContext = this.shadowServerContext;
			ShadowRedundancyManager.ReceiveTracer.TraceDebug<string>((long)this.GetHashCode(), "XSHADOW accepted; Shadow server context {0}", this.shadowServerContext);
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000D5C62 File Offset: 0x000D3E62
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000D5C64 File Offset: 0x000D3E64
		internal override void OutboundFormatCommand()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			ShadowRedundancyManager.GetShadowNextHopConnectorId(smtpOutSession.NextHopConnection.Key);
			base.ProtocolCommandString = "XSHADOW " + this.shadowRedundancyManager.GetShadowContext(smtpOutSession.AdvertisedEhloOptions, smtpOutSession.NextHopConnection.Key);
			ShadowRedundancyManager.SendTracer.TraceDebug<string>((long)this.GetHashCode(), "Formatted command : {0}", base.ProtocolCommandString);
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000D5CD8 File Offset: 0x000D3ED8
		internal override void OutboundProcessResponse()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			if (base.SmtpResponse.SmtpResponseType != SmtpResponseType.Success)
			{
				ShadowRedundancyManager.SendTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "XSHADOW rejected: {0}", base.SmtpResponse);
			}
			else
			{
				ShadowRedundancyManager.SendTracer.TraceDebug((long)this.GetHashCode(), "XSHADOW accepted. Parsing the response.");
				if (!this.ParseResponse(base.SmtpResponse))
				{
					return;
				}
			}
			ShadowRedundancyManager.SendTracer.TraceDebug((long)this.GetHashCode(), "Will issue MAIL FROM");
			smtpOutSession.PrepareForNextMessage(false);
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000D5D64 File Offset: 0x000D3F64
		private void ParseCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.XShadowInboundParseCommand);
			if (smtpInSession.IsShadowedBySender || smtpInSession.IsPeerShadowSession)
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
			if (!smtpInSession.AdvertisedEhloOptions.XShadow && !smtpInSession.AdvertisedEhloOptions.XShadowRequest)
			{
				base.SmtpResponse = SmtpResponse.CommandNotImplemented;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.XShadowNotEnabled);
				return;
			}
			if (!SmtpInSessionUtils.HasSMTPAcceptXShadowPermission(smtpInSession.Permissions))
			{
				base.SmtpResponse = SmtpResponse.NotAuthorized;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.XShadowNotAuthorized);
				return;
			}
			string nextArg = base.GetNextArg();
			if (string.IsNullOrEmpty(nextArg))
			{
				base.SmtpResponse = SmtpResponse.XShadowContextRequired;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				return;
			}
			if (nextArg.Length > 255)
			{
				base.SmtpResponse = SmtpResponse.InvalidArguments;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				return;
			}
			this.shadowServerContext = nextArg;
			base.ParsingStatus = ParsingStatus.Complete;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000D5E80 File Offset: 0x000D4080
		private bool ParseResponse(SmtpResponse xshadowResponse)
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			string smtpHost = smtpOutSession.SmtpHost;
			string text = string.Empty;
			if (xshadowResponse.StatusText != null)
			{
				text = xshadowResponse.StatusText[0];
				if (!string.IsNullOrEmpty(text))
				{
					int num = text.IndexOf(' ');
					if (num != -1)
					{
						text = text.Remove(num);
					}
					if (text.Length > 255)
					{
						smtpOutSession.FailoverConnection(base.SmtpResponse);
						smtpOutSession.NextState = SmtpOutSession.SessionState.Quit;
						this.shadowRedundancyManager.NotifyServerViolatedSmtpContract(smtpHost);
						return false;
					}
				}
			}
			this.shadowRedundancyManager.NotifyPrimaryServerState(smtpHost, text, ShadowRedundancyCompatibilityVersion.E14);
			smtpOutSession.Shadowed = true;
			return true;
		}

		// Token: 0x04001AA4 RID: 6820
		private const char WordDelimiter = ' ';

		// Token: 0x04001AA5 RID: 6821
		private const int PrimaryServerStateMaxLength = 255;

		// Token: 0x04001AA6 RID: 6822
		private const int ShadowServerContextMaxLength = 255;

		// Token: 0x04001AA7 RID: 6823
		private string shadowServerContext;

		// Token: 0x04001AA8 RID: 6824
		private readonly IShadowRedundancyManager shadowRedundancyManager;
	}
}
