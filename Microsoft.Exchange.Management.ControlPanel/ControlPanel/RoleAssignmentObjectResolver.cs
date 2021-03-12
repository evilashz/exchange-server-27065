using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200053B RID: 1339
	public class RoleAssignmentObjectResolver : AdObjectResolver
	{
		// Token: 0x06003F53 RID: 16211 RVA: 0x000BEAE4 File Offset: 0x000BCCE4
		public IEnumerable<RoleAssignmentObjectResolverRow> ResolveObjects(IEnumerable<ADObjectId> identities)
		{
			return base.ResolveObjects<RoleAssignmentObjectResolverRow>(identities, RoleAssignmentObjectResolverRow.Properties, (ADRawEntry e) => new RoleAssignmentObjectResolverRow(e));
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x000BEB0F File Offset: 0x000BCD0F
		internal override IDirectorySession CreateAdSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, base.TenantSharedConfigurationSessionSetting ?? base.TenantSessionSetting, 332, "CreateAdSession", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\UsersGroups\\RoleAssignmentResolver.cs");
		}

		// Token: 0x040028F1 RID: 10481
		internal static readonly RoleAssignmentObjectResolver Instance = new RoleAssignmentObjectResolver();
	}
}
