using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000197 RID: 407
	internal class RoleEntryComparer : IComparer<RoleEntry>
	{
		// Token: 0x06000D35 RID: 3381 RVA: 0x000291FC File Offset: 0x000273FC
		public int Compare(RoleEntry a, RoleEntry b)
		{
			return RoleEntry.CompareRoleEntriesByName(a, b);
		}

		// Token: 0x040007F3 RID: 2035
		public static readonly RoleEntryComparer Instance = new RoleEntryComparer();
	}
}
