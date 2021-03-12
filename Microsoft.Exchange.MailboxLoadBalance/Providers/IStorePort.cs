using System;
using System.Collections.Generic;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxLoadBalance.Providers
{
	// Token: 0x020000C4 RID: 196
	internal interface IStorePort
	{
		// Token: 0x0600065C RID: 1628
		IEnumerable<MailboxTableEntry> GetMailboxTable(DirectoryDatabase database, Guid mailboxGuid, PropTag[] propertiesToLoad);

		// Token: 0x0600065D RID: 1629
		DatabaseSizeInfo GetDatabaseSize(DirectoryDatabase database);
	}
}
