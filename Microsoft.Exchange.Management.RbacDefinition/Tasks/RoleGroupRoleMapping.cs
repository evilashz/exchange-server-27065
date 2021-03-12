using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200000C RID: 12
	internal class RoleGroupRoleMapping
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000359D File Offset: 0x0000179D
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000035A5 File Offset: 0x000017A5
		public string RoleGroup { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000035AE File Offset: 0x000017AE
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000035B6 File Offset: 0x000017B6
		public RoleAssignmentDefinition[] Assignments { get; private set; }

		// Token: 0x0600004D RID: 77 RVA: 0x000035BF File Offset: 0x000017BF
		public RoleGroupRoleMapping(string roleGroup, RoleAssignmentDefinition[] assignments)
		{
			this.RoleGroup = roleGroup;
			this.Assignments = assignments;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000035D8 File Offset: 0x000017D8
		public List<RoleAssignmentDefinition> GetRolesAssignments(List<string> enabledFeatures)
		{
			List<RoleAssignmentDefinition> list = new List<RoleAssignmentDefinition>();
			foreach (RoleAssignmentDefinition roleAssignmentDefinition in this.Assignments)
			{
				if (roleAssignmentDefinition.SatisfyCondition(enabledFeatures))
				{
					list.Add(roleAssignmentDefinition);
				}
			}
			return list;
		}
	}
}
