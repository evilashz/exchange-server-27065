using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000008 RID: 8
	internal class RoleGroupCollection : List<RoleGroupDefinition>
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002B9C File Offset: 0x00000D9C
		internal ADGroup GetADGroupByGuid(Guid wkGuid)
		{
			RoleGroupDefinition roleGroupDefinition = this.FirstOrDefault((RoleGroupDefinition x) => x.RoleGroupGuid.Equals(wkGuid));
			if (roleGroupDefinition == null)
			{
				return null;
			}
			return roleGroupDefinition.ADGroup;
		}
	}
}
