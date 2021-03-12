using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x02000272 RID: 626
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxLocation
	{
		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060019E7 RID: 6631
		string ServerFqdn { get; }

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060019E8 RID: 6632
		Guid ServerGuid { get; }

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060019E9 RID: 6633
		string ServerLegacyDn { get; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060019EA RID: 6634
		int ServerVersion { get; }

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060019EB RID: 6635
		ADObjectId ServerSite { get; }

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060019EC RID: 6636
		string DatabaseName { get; }

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060019ED RID: 6637
		string RpcClientAccessServerLegacyDn { get; }

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060019EE RID: 6638
		string DatabaseLegacyDn { get; }

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060019EF RID: 6639
		Guid HomePublicFolderDatabaseGuid { get; }
	}
}
