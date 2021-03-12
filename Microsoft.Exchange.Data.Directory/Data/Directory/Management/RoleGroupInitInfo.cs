using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200074F RID: 1871
	public struct RoleGroupInitInfo
	{
		// Token: 0x17001F83 RID: 8067
		// (get) Token: 0x06005AE2 RID: 23266 RVA: 0x0013E4B7 File Offset: 0x0013C6B7
		public string Name
		{
			get
			{
				return this.roleGroupName;
			}
		}

		// Token: 0x17001F84 RID: 8068
		// (get) Token: 0x06005AE3 RID: 23267 RVA: 0x0013E4BF File Offset: 0x0013C6BF
		public int Id
		{
			get
			{
				return this.roleGroupId;
			}
		}

		// Token: 0x17001F85 RID: 8069
		// (get) Token: 0x06005AE4 RID: 23268 RVA: 0x0013E4C7 File Offset: 0x0013C6C7
		public Guid WellKnownGuid
		{
			get
			{
				return this.wellKnownGuid;
			}
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x0013E4CF File Offset: 0x0013C6CF
		public RoleGroupInitInfo(string name, int id, Guid wkGuid)
		{
			this.roleGroupName = name;
			this.roleGroupId = id;
			this.wellKnownGuid = wkGuid;
		}

		// Token: 0x04003D33 RID: 15667
		private string roleGroupName;

		// Token: 0x04003D34 RID: 15668
		private int roleGroupId;

		// Token: 0x04003D35 RID: 15669
		private Guid wellKnownGuid;
	}
}
