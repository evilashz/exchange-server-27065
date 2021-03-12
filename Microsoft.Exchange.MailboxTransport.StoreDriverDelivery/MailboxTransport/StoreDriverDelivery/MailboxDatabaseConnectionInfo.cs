using System;
using System.Net;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200002B RID: 43
	internal class MailboxDatabaseConnectionInfo : IMailboxDatabaseConnectionInfo
	{
		// Token: 0x06000228 RID: 552 RVA: 0x0000B564 File Offset: 0x00009764
		public MailboxDatabaseConnectionInfo(Guid mdbGuid, long smtpSessionId, IPAddress remoteIpAddress)
		{
			this.MailboxDatabaseGuid = mdbGuid;
			this.SmtpSessionId = smtpSessionId;
			this.RemoteIPAddress = remoteIpAddress;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000B581 File Offset: 0x00009781
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000B589 File Offset: 0x00009789
		public Guid MailboxDatabaseGuid { get; private set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000B592 File Offset: 0x00009792
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000B59A File Offset: 0x0000979A
		public IPAddress RemoteIPAddress { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000B5A3 File Offset: 0x000097A3
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000B5AB File Offset: 0x000097AB
		public long SmtpSessionId { get; private set; }
	}
}
