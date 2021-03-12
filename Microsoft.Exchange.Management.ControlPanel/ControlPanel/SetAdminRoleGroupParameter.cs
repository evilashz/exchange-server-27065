using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004DD RID: 1245
	[DataContract]
	public class SetAdminRoleGroupParameter : BaseRoleGroupParameters
	{
		// Token: 0x170023FA RID: 9210
		// (get) Token: 0x06003CCF RID: 15567 RVA: 0x000B6594 File Offset: 0x000B4794
		// (set) Token: 0x06003CD0 RID: 15568 RVA: 0x000B659C File Offset: 0x000B479C
		[DataMember]
		public Identity[] Members { get; set; }

		// Token: 0x170023FB RID: 9211
		// (get) Token: 0x06003CD1 RID: 15569 RVA: 0x000B65A5 File Offset: 0x000B47A5
		// (set) Token: 0x06003CD2 RID: 15570 RVA: 0x000B65AD File Offset: 0x000B47AD
		[DataMember]
		public Identity[] Roles { get; set; }

		// Token: 0x170023FC RID: 9212
		// (get) Token: 0x06003CD3 RID: 15571 RVA: 0x000B65B6 File Offset: 0x000B47B6
		public bool IsRolesModified
		{
			get
			{
				return this.Roles != null;
			}
		}

		// Token: 0x170023FD RID: 9213
		// (get) Token: 0x06003CD4 RID: 15572 RVA: 0x000B65C4 File Offset: 0x000B47C4
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-RoleGroup";
			}
		}
	}
}
