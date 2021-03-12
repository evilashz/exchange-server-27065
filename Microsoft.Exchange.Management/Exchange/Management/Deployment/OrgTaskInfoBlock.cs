using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200018A RID: 394
	[ClassAccessLevel(AccessLevel.Consumer)]
	public class OrgTaskInfoBlock : TaskInfoBlock
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00041C06 File Offset: 0x0003FE06
		// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x00041C21 File Offset: 0x0003FE21
		public OrgTaskInfoEntry Global
		{
			get
			{
				if (this.global == null)
				{
					this.global = new OrgTaskInfoEntry();
				}
				return this.global;
			}
			set
			{
				this.global = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00041C2A File Offset: 0x0003FE2A
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x00041C45 File Offset: 0x0003FE45
		public OrgTaskInfoEntry Tenant
		{
			get
			{
				if (this.tenant == null)
				{
					this.tenant = new OrgTaskInfoEntry();
				}
				return this.tenant;
			}
			set
			{
				this.tenant = value;
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00041C50 File Offset: 0x0003FE50
		internal override string GetTask(InstallationCircumstances circumstance)
		{
			switch (circumstance)
			{
			case InstallationCircumstances.Standalone:
				return this.Global.Task;
			case InstallationCircumstances.TenantOrganization:
				return this.Tenant.Task;
			default:
				return string.Empty;
			}
		}

		// Token: 0x040006D3 RID: 1747
		private OrgTaskInfoEntry global;

		// Token: 0x040006D4 RID: 1748
		private OrgTaskInfoEntry tenant;
	}
}
