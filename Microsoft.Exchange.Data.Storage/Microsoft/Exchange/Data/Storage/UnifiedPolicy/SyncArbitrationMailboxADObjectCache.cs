using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E90 RID: 3728
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SyncArbitrationMailboxADObjectCache : LazyLookupTimeoutCache<Guid, ADUser>
	{
		// Token: 0x060081BB RID: 33211 RVA: 0x002374B4 File Offset: 0x002356B4
		public SyncArbitrationMailboxADObjectCache() : base(10, 1000, false, TimeSpan.FromHours(24.0))
		{
		}

		// Token: 0x060081BC RID: 33212 RVA: 0x002374D4 File Offset: 0x002356D4
		protected override ADUser CreateOnCacheMiss(Guid key, ref bool shouldAdd)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromExternalDirectoryOrganizationId(key), 42, "CreateOnCacheMiss", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\UnifiedPolicy\\SyncArbitrationMailboxADObjectCache.cs");
			return MailboxDataProvider.GetDiscoveryMailbox(tenantOrRootOrgRecipientSession);
		}
	}
}
