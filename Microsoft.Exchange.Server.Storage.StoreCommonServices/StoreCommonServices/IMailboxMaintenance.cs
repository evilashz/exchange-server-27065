using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000C9 RID: 201
	public interface IMailboxMaintenance
	{
		// Token: 0x0600083A RID: 2106
		bool MarkForMaintenance(Context context, MailboxState mailboxState);
	}
}
