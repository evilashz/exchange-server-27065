using System;
using System.Net;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200002A RID: 42
	internal interface IMailboxDatabaseConnectionInfo
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000225 RID: 549
		Guid MailboxDatabaseGuid { get; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000226 RID: 550
		long SmtpSessionId { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000227 RID: 551
		IPAddress RemoteIPAddress { get; }
	}
}
