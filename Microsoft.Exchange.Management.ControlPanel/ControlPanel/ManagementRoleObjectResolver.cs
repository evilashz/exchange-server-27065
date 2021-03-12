using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200051E RID: 1310
	public class ManagementRoleObjectResolver : AdObjectResolver
	{
		// Token: 0x06003EAB RID: 16043 RVA: 0x000BD20B File Offset: 0x000BB40B
		internal override IDirectorySession CreateAdSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, base.TenantSharedConfigurationSessionSetting ?? base.TenantSessionSetting, 97, "CreateAdSession", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\UsersGroups\\ManagementRoleResolver.cs");
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x000BD244 File Offset: 0x000BB444
		public IEnumerable<ManagementRoleResolveRow> ResolveObjects(IEnumerable<ADObjectId> identities)
		{
			return from row in base.ResolveObjects<ManagementRoleResolveRow>(identities, ManagementRoleResolveRow.Properties, (ADRawEntry e) => new ManagementRoleResolveRow(e))
			orderby row.DisplayName
			select row;
		}

		// Token: 0x040028A4 RID: 10404
		internal static readonly ManagementRoleObjectResolver Instance = new ManagementRoleObjectResolver();
	}
}
