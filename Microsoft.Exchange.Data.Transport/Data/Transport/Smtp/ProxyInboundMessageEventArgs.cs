using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000046 RID: 70
	internal class ProxyInboundMessageEventArgs : ReceiveCommandEventArgs
	{
		// Token: 0x0600019F RID: 415 RVA: 0x000062FC File Offset: 0x000044FC
		public ProxyInboundMessageEventArgs(SmtpSession smtpSession, MailItem mailItem, bool clientIsPreE15InternalServer, bool localFrontendIsColocatedWithHub, string localServerFqdn) : base(smtpSession)
		{
			this.MailItem = mailItem;
			this.ClientIsPreE15InternalServer = clientIsPreE15InternalServer;
			this.LocalFrontendIsColocatedWithHub = localFrontendIsColocatedWithHub;
			this.LocalServerFqdn = localServerFqdn;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006323 File Offset: 0x00004523
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x0000632B File Offset: 0x0000452B
		public MailItem MailItem { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006334 File Offset: 0x00004534
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000633C File Offset: 0x0000453C
		public bool ClientIsPreE15InternalServer { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006345 File Offset: 0x00004545
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000634D File Offset: 0x0000454D
		public bool LocalFrontendIsColocatedWithHub { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00006356 File Offset: 0x00004556
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000635E File Offset: 0x0000455E
		public string LocalServerFqdn { get; private set; }
	}
}
