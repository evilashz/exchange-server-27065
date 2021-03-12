using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004D9 RID: 1241
	internal class AnonymousTlsSmtpInCommand : TlsSmtpInCommandBase
	{
		// Token: 0x06003941 RID: 14657 RVA: 0x000EA39A File Offset: 0x000E859A
		public AnonymousTlsSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06003942 RID: 14658 RVA: 0x000EA3A4 File Offset: 0x000E85A4
		protected override ParseAndProcessResult<SmtpInStateMachineEvents> CommandCompletedResult
		{
			get
			{
				return AnonymousTlsSmtpInCommand.CommandComplete;
			}
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x000EA3AB File Offset: 0x000E85AB
		protected override void OnClientCertificateReceived(IX509Certificate2 remoteCertificate)
		{
			this.sessionState.UpdateIdentityBasedOnClientTlsCertificate(remoteCertificate);
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x000EA3B9 File Offset: 0x000E85B9
		protected override SecureState Command
		{
			get
			{
				return SecureState.AnonymousTls;
			}
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06003945 RID: 14661 RVA: 0x000EA3BC File Offset: 0x000E85BC
		protected override IX509Certificate2 LocalCertificate
		{
			get
			{
				return this.sessionState.InternalTransportCertificate;
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06003946 RID: 14662 RVA: 0x000EA3C9 File Offset: 0x000E85C9
		protected override bool ShouldRequestClientCertificate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001D2C RID: 7468
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> CommandComplete = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.Empty, SmtpInStateMachineEvents.XAnonymousTlsProcessed, false);
	}
}
