using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000157 RID: 343
	public interface IMailboxQuarantineProvider
	{
		// Token: 0x06000D69 RID: 3433
		void PrequarantineMailbox(Guid databaseGuid, Guid mailboxGuid, string reason);

		// Token: 0x06000D6A RID: 3434
		void UnquarantineMailbox(Guid databaseGuid, Guid mailboxGuid);

		// Token: 0x06000D6B RID: 3435
		List<PrequarantinedMailbox> GetPreQuarantinedMailboxes(Guid databaseGuid);

		// Token: 0x06000D6C RID: 3436
		bool IsMigrationAccessAllowed(Guid databaseGuid, Guid mailboxGuid);
	}
}
