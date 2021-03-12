using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000021 RID: 33
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICoreDatabaseManager
	{
		// Token: 0x060001FC RID: 508
		bool FindSystemMailboxGuid(string systemMailboxName, out Guid systemMailboxGuid);

		// Token: 0x060001FD RID: 509
		bool StartCacheManager(DatabaseManager databaseManager);

		// Token: 0x060001FE RID: 510
		void ShutdownCacheManager(DatabaseManager databaseManager);
	}
}
