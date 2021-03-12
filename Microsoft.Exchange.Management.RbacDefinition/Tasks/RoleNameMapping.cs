using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000005 RID: 5
	internal class RoleNameMapping
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000025CB File Offset: 0x000007CB
		public bool IsDeprecatedRole
		{
			get
			{
				return this.NewNames != null && !this.IsSplitting;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000025E0 File Offset: 0x000007E0
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000025E8 File Offset: 0x000007E8
		public bool IsSplitting { get; private set; }

		// Token: 0x06000019 RID: 25 RVA: 0x000025F1 File Offset: 0x000007F1
		public RoleNameMapping(string nameOld, string nameNew)
		{
			this.OldName = nameOld;
			this.NewName = nameNew;
			this.NewNames = null;
			this.IsSplitting = false;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002615 File Offset: 0x00000815
		public RoleNameMapping(string nameOld, params string[] newNames) : this(nameOld, false, newNames)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002620 File Offset: 0x00000820
		public RoleNameMapping(string nameOld, bool splitting, params string[] newNames)
		{
			this.OldName = nameOld;
			this.NewName = null;
			this.IsSplitting = splitting;
			this.NewNames = newNames.ToList<string>();
		}

		// Token: 0x0400000C RID: 12
		public string OldName;

		// Token: 0x0400000D RID: 13
		public string NewName;

		// Token: 0x0400000E RID: 14
		public List<string> NewNames;
	}
}
