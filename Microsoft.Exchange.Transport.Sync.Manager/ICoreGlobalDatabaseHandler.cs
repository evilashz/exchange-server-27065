using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICoreGlobalDatabaseHandler
	{
		// Token: 0x06000225 RID: 549
		SortedDictionary<Guid, bool> FindLocalDatabasesFromAD();

		// Token: 0x06000226 RID: 550
		SortedDictionary<Guid, bool> FindLocalDatabasesFromAdminRPC();

		// Token: 0x06000227 RID: 551
		void OnNewDatabaseManager(DatabaseManager databaseManager);
	}
}
