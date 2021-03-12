using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004F7 RID: 1271
	internal class StartTlsSmtpInCommand : TlsSmtpInCommandBase
	{
		// Token: 0x06003A9B RID: 15003 RVA: 0x000F392B File Offset: 0x000F1B2B
		public StartTlsSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
			this.tlsReceiveDomainSecureList = this.sessionState.Configuration.TransportConfiguration.TlsReceiveDomainSecureList;
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06003A9C RID: 15004 RVA: 0x000F3950 File Offset: 0x000F1B50
		protected override SecureState Command
		{
			get
			{
				return SecureState.StartTls;
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06003A9D RID: 15005 RVA: 0x000F3953 File Offset: 0x000F1B53
		protected override IX509Certificate2 LocalCertificate
		{
			get
			{
				return this.sessionState.AdvertisedTlsCertificate;
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06003A9E RID: 15006 RVA: 0x000F3960 File Offset: 0x000F1B60
		protected override bool ShouldRequestClientCertificate
		{
			get
			{
				return this.sessionState.RequestClientTlsCertificate || (this.sessionState.ReceiveConnector.DomainSecureEnabled && this.tlsReceiveDomainSecureList.Any<SmtpDomain>()) || this.sessionState.ReceiveConnectorStub.ContainsTlsDomainCapabilities;
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06003A9F RID: 15007 RVA: 0x000F39B2 File Offset: 0x000F1BB2
		protected override ParseAndProcessResult<SmtpInStateMachineEvents> CommandCompletedResult
		{
			get
			{
				return StartTlsSmtpInCommand.CommandComplete;
			}
		}

		// Token: 0x04001D7F RID: 7551
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> CommandComplete = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.Empty, SmtpInStateMachineEvents.StartTlsProcessed, false);

		// Token: 0x04001D80 RID: 7552
		private readonly IEnumerable<SmtpDomain> tlsReceiveDomainSecureList;
	}
}
