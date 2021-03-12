using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000006 RID: 6
	internal class RoleNameMappingCollection : List<RoleNameMapping>
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000265C File Offset: 0x0000085C
		internal List<string> GetDeprecatedRoleNames()
		{
			return (from roleNameMapping in base.FindAll((RoleNameMapping x) => x.IsDeprecatedRole)
			select roleNameMapping.OldName).ToList<string>();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026E0 File Offset: 0x000008E0
		internal bool IsNewRoleFromOldRole(string roleName, out string oldRoleName)
		{
			List<string> list = (from roleNameMapping in base.FindAll((RoleNameMapping x) => x.NewNames != null && x.NewNames.Contains(roleName))
			select roleNameMapping.OldName).ToList<string>();
			oldRoleName = ((list.Count > 0) ? list[0] : string.Empty);
			return !string.IsNullOrEmpty(oldRoleName);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027B4 File Offset: 0x000009B4
		internal RoleNameMapping GetMapping(string newName)
		{
			List<RoleNameMapping> list = (from roleNameMapping in base.FindAll((RoleNameMapping x) => !x.IsSplitting && ((x.NewName != null && x.NewName.Equals(newName)) || (x.NewNames != null && x.NewNames.Contains(newName))))
			select roleNameMapping).ToList<RoleNameMapping>();
			if (list.Count <= 0)
			{
				return null;
			}
			return list[0];
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002838 File Offset: 0x00000A38
		internal RoleNameMapping GetMappingForSplittingRole(string splittingRole)
		{
			List<RoleNameMapping> list = (from roleNameMapping in base.FindAll((RoleNameMapping x) => x.OldName.Equals(splittingRole))
			select roleNameMapping).ToList<RoleNameMapping>();
			if (list.Count <= 0)
			{
				return null;
			}
			return list[0];
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002918 File Offset: 0x00000B18
		internal List<RoleNameMapping> GetNonRenamingMappings(string name)
		{
			List<RoleNameMapping> list = (from roleNameMapping in base.FindAll((RoleNameMapping x) => (x.IsSplitting || x.IsDeprecatedRole) && (x.OldName.Equals(name) || (x.NewName != null && x.NewName.Equals(name)) || (x.NewNames != null && x.NewNames.Contains(name))))
			select roleNameMapping).ToList<RoleNameMapping>();
			if (list.Count <= 0)
			{
				return null;
			}
			return list;
		}
	}
}
