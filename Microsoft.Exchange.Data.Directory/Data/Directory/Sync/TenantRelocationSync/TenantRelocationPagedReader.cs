using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x0200080A RID: 2058
	internal class TenantRelocationPagedReader : ADPagedReader<TenantRelocationSyncObject>
	{
		// Token: 0x060065A4 RID: 26020 RVA: 0x0016450C File Offset: 0x0016270C
		internal TenantRelocationPagedReader(IDirectorySession session, ADObjectId OrganizationUnitRoot, int pageSize, IEnumerable<PropertyDefinition> properties, byte[] cookie) : base(session, session.GetDomainNamingContext(), QueryScope.SubTree, null, null, pageSize, properties, false)
		{
			base.LdapFilter = string.Format("(msexchouroot={0})", OrganizationUnitRoot.ToGuidOrDNString());
			base.Cookie = cookie;
			base.IncludeDeletedObjects = true;
			base.SearchAllNcs = true;
		}

		// Token: 0x060065A5 RID: 26021 RVA: 0x00164559 File Offset: 0x00162759
		internal TenantRelocationSyncObject[] GetNextResultPage()
		{
			return this.GetNextPage();
		}

		// Token: 0x04004360 RID: 17248
		private const string TenantObjectsLdapFilter = "(msexchouroot={0})";
	}
}
