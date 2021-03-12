using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000007 RID: 7
	internal class RoleGroupDefinition
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002980 File Offset: 0x00000B80
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002988 File Offset: 0x00000B88
		public Guid RoleGroupGuid { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002991 File Offset: 0x00000B91
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002999 File Offset: 0x00000B99
		public string Name { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000029A2 File Offset: 0x00000BA2
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000029AA File Offset: 0x00000BAA
		public int Id { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000029B3 File Offset: 0x00000BB3
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000029BB File Offset: 0x00000BBB
		public string Description { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000029C4 File Offset: 0x00000BC4
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000029CC File Offset: 0x00000BCC
		public List<Guid> E12USG { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000029D5 File Offset: 0x00000BD5
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000029DD File Offset: 0x00000BDD
		public List<Datacenter.ExchangeSku> AlwaysCreateOnSku { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000029E6 File Offset: 0x00000BE6
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000029EE File Offset: 0x00000BEE
		internal ADGroup ADGroup { get; set; }

		// Token: 0x06000036 RID: 54 RVA: 0x000029F7 File Offset: 0x00000BF7
		public RoleGroupDefinition(RoleGroupDefinition roleGroup) : this(roleGroup.Name, roleGroup.Id, roleGroup.RoleGroupGuid, roleGroup.Description, roleGroup.AlwaysCreateOnSku, roleGroup.E12USG.ToArray())
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A28 File Offset: 0x00000C28
		public RoleGroupDefinition(RoleGroupInitInfo roleGroupInitInfo, string description, params Guid[] e12USG) : this(roleGroupInitInfo.Name, roleGroupInitInfo.Id, roleGroupInitInfo.WellKnownGuid, description, null, e12USG)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A48 File Offset: 0x00000C48
		public RoleGroupDefinition(string name, int id, Guid wellKnownGuid, string description, params Guid[] e12USG) : this(name, id, wellKnownGuid, description, null, e12USG)
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A58 File Offset: 0x00000C58
		public RoleGroupDefinition(RoleGroupInitInfo roleGroupInitInfo, string description, List<Datacenter.ExchangeSku> alwaysCreateOn, params Guid[] e12USG) : this(roleGroupInitInfo.Name, roleGroupInitInfo.Id, roleGroupInitInfo.WellKnownGuid, description, alwaysCreateOn, e12USG)
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A7C File Offset: 0x00000C7C
		public RoleGroupDefinition(string name, int id, Guid wellKnownGuid, string description, List<Datacenter.ExchangeSku> alwaysCreateOn, params Guid[] e12USG)
		{
			this.Name = name;
			this.RoleGroupGuid = wellKnownGuid;
			this.Description = description;
			this.Id = id;
			this.E12USG = ((e12USG == null) ? new List<Guid>(0) : e12USG.ToList<Guid>());
			this.ADGroup = null;
			this.AlwaysCreateOnSku = alwaysCreateOn;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public LocalizedException GuidNotFoundException
		{
			get
			{
				string name;
				if ((name = this.Name) != null)
				{
					if (name == "Organization Management")
					{
						return new ExOrgAdminSGroupNotFoundException(this.RoleGroupGuid);
					}
					if (name == "Public Folder Management")
					{
						return new ExPublicFolderAdminSGroupNotFoundException(this.RoleGroupGuid);
					}
					if (name == "Recipient Management")
					{
						return new ExMailboxAdminSGroupNotFoundException(this.RoleGroupGuid);
					}
					if (name == "View-Only Organization Management")
					{
						return new ExOrgReadAdminSGroupNotFoundException(this.RoleGroupGuid);
					}
				}
				return new ExRbacRoleGroupNotFoundException(this.RoleGroupGuid, this.Name);
			}
		}
	}
}
