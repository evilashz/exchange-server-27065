using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200000B RID: 11
	internal class RoleToRAPMapping
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000353D File Offset: 0x0000173D
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00003545 File Offset: 0x00001745
		public RoleToRAPAssignmentDefinition[] Assignments { get; private set; }

		// Token: 0x06000047 RID: 71 RVA: 0x0000354E File Offset: 0x0000174E
		public RoleToRAPMapping(RoleToRAPAssignmentDefinition[] assignments)
		{
			this.Assignments = assignments;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003560 File Offset: 0x00001760
		public List<RoleToRAPAssignmentDefinition> GetRoleAssignments(List<string> enabledFeatures)
		{
			List<RoleToRAPAssignmentDefinition> list = new List<RoleToRAPAssignmentDefinition>();
			foreach (RoleToRAPAssignmentDefinition roleToRAPAssignmentDefinition in this.Assignments)
			{
				if (roleToRAPAssignmentDefinition.SatisfyCondition(enabledFeatures))
				{
					list.Add(roleToRAPAssignmentDefinition);
				}
			}
			return list;
		}
	}
}
