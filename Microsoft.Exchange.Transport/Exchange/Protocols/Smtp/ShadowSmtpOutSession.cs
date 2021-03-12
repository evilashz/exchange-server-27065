using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200039E RID: 926
	internal class ShadowSmtpOutSession : InboundProxySmtpOutSession
	{
		// Token: 0x06002966 RID: 10598 RVA: 0x000A3D08 File Offset: 0x000A1F08
		public ShadowSmtpOutSession(ulong sessionId, SmtpOutConnection smtpOutConnection, NextHopConnection nextHopConnection, IPEndPoint target, ProtocolLog protocolLog, ProtocolLoggingLevel loggingLevel, IMailRouter mailRouter, CertificateCache certificateCache, CertificateValidator certificateValidator, ShadowRedundancyManager shadowRedundancyManager, TransportAppConfig transportAppConfig, ITransportConfiguration transportConfiguration, IInboundProxyLayer proxyLayer) : base(sessionId, smtpOutConnection, nextHopConnection, target, protocolLog, loggingLevel, mailRouter, certificateCache, certificateValidator, shadowRedundancyManager, transportAppConfig, transportConfiguration, proxyLayer)
		{
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x000A3D32 File Offset: 0x000A1F32
		public override bool SendShadow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x000A3D35 File Offset: 0x000A1F35
		public override bool SendXShadowRequest
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x000A3D38 File Offset: 0x000A1F38
		public override bool SendXQDiscard
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x0600296A RID: 10602 RVA: 0x000A3D3B File Offset: 0x000A1F3B
		public override bool SupportExch50
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x0600296B RID: 10603 RVA: 0x000A3D3E File Offset: 0x000A1F3E
		public override bool ShadowCurrentMailItem
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x0600296C RID: 10604 RVA: 0x000A3D41 File Offset: 0x000A1F41
		protected override bool MessageContextBlobTransferSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000A3D44 File Offset: 0x000A1F44
		protected override SmtpCommand CreateSmtpCommand(string cmd)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "ShadowSmtpOutSession.CreateSmtpCommand: {0}", cmd);
			SmtpCommand smtpCommand = null;
			if (cmd != null)
			{
				if (<PrivateImplementationDetails>{35B61D4F-52EA-4BAD-B9BF-6C1C27B6D99A}.$$method0x60027f3-1 == null)
				{
					<PrivateImplementationDetails>{35B61D4F-52EA-4BAD-B9BF-6C1C27B6D99A}.$$method0x60027f3-1 = new Dictionary<string, int>(12)
					{
						{
							"ConnectResponse",
							0
						},
						{
							"EHLO",
							1
						},
						{
							"X-EXPS",
							2
						},
						{
							"STARTTLS",
							3
						},
						{
							"X-ANONYMOUSTLS",
							4
						},
						{
							"XSHADOWREQUEST",
							5
						},
						{
							"MAIL",
							6
						},
						{
							"RCPT",
							7
						},
						{
							"DATA",
							8
						},
						{
							"BDAT",
							9
						},
						{
							"RSET",
							10
						},
						{
							"QUIT",
							11
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{35B61D4F-52EA-4BAD-B9BF-6C1C27B6D99A}.$$method0x60027f3-1.TryGetValue(cmd, out num))
				{
					switch (num)
					{
					case 0:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.ShadowCreateCmdConnectResponse);
						break;
					case 1:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.ShadowCreateCmdEhlo);
						smtpCommand = new EHLOShadowSmtpCommand(this, this.transportConfiguration);
						break;
					case 2:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.ShadowCreateCmdAuth);
						smtpCommand = new AuthSmtpCommand(this, true, this.transportConfiguration);
						break;
					case 3:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.ShadowCreateCmdStarttls);
						smtpCommand = new StarttlsSmtpCommand(this, false);
						break;
					case 4:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.ShadowCreateCmdStarttls);
						smtpCommand = new StarttlsSmtpCommand(this, true);
						break;
					case 5:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.ShadowCreateCmdXShadowRequest);
						smtpCommand = new XShadowRequestSmtpCommand(this, this.shadowRedundancyManager);
						break;
					case 6:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.ShadowCreateCmdMail);
						smtpCommand = new MailSmtpCommand(this, this.transportAppConfig);
						break;
					case 7:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.ShadowCreateCmdRcpt);
						smtpCommand = new RcptInboundProxySmtpCommand(this, this.recipientCorrelator, this.transportAppConfig);
						break;
					case 8:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.ShadowCreateCmdData);
						smtpCommand = new DataInboundProxySmtpCommand(this, this.transportAppConfig);
						break;
					case 9:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.ShadowCreateCmdBdat);
						smtpCommand = new BdatInboundProxySmtpCommand(this, this.transportAppConfig);
						break;
					case 10:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.ShadowCreateCmdRset);
						smtpCommand = new RsetShadowSmtpCommand(this);
						break;
					case 11:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.ShadowCreateCmdQuit);
						smtpCommand = new QuitSmtpCommand(this);
						break;
					default:
						goto IL_22A;
					}
					if (smtpCommand != null)
					{
						smtpCommand.ParsingStatus = ParsingStatus.Complete;
						smtpCommand.OutboundCreateCommand();
					}
					return smtpCommand;
				}
			}
			IL_22A:
			throw new ArgumentException("Unknown command encountered in ShadowSmtpOut: " + cmd, "cmd");
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000A3FA2 File Offset: 0x000A21A2
		protected override IInboundProxyLayer GetProxyLayer(NextHopConnection newConnection)
		{
			if (!(newConnection is ShadowPeerNextHopConnection))
			{
				throw new InvalidOperationException("GetProxyLayer called with incorrect NextHopConnection type");
			}
			return ((ShadowPeerNextHopConnection)newConnection).ProxyLayer;
		}
	}
}
