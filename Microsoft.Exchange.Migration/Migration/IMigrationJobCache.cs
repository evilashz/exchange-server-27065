using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200000B RID: 11
	internal interface IMigrationJobCache
	{
		// Token: 0x06000020 RID: 32
		bool Add(string mailboxLegacyDn, Guid mdbGuid, OrganizationId organizationId, bool refresh);

		// Token: 0x06000021 RID: 33
		void Remove(MigrationCacheEntry cacheEntry);

		// Token: 0x06000022 RID: 34
		bool SyncWithStore();

		// Token: 0x06000023 RID: 35
		List<MigrationCacheEntry> Get();
	}
}
