using System;
using Microsoft.Exchange.Assistants;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000021 RID: 33
	internal interface IMailboxChangeHandler
	{
		// Token: 0x0600015F RID: 351
		void HandleDatabaseStart(DatabaseInfo databaseInfo);

		// Token: 0x06000160 RID: 352
		void HandleDatabaseShutdown(DatabaseInfo databaseInfo);

		// Token: 0x06000161 RID: 353
		void HandleMailboxCreatedOrConnected(DatabaseInfo databaseInfo, Guid mailboxGuid);

		// Token: 0x06000162 RID: 354
		void HandleMailboxMoveSucceeded(DatabaseInfo databaseInfo, Guid mailboxGuid);

		// Token: 0x06000163 RID: 355
		void HandleMailboxDeletedOrDisconnected(DatabaseInfo databaseInfo, Guid mailboxGuid);
	}
}
