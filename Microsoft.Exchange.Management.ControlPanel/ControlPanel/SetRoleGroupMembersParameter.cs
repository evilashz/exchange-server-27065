using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200053D RID: 1341
	public class SetRoleGroupMembersParameter : BaseRoleGroupParameters
	{
		// Token: 0x170024BC RID: 9404
		// (get) Token: 0x06003F5B RID: 16219 RVA: 0x000BEB6F File Offset: 0x000BCD6F
		// (set) Token: 0x06003F5C RID: 16220 RVA: 0x000BEB81 File Offset: 0x000BCD81
		public Identity[] Members
		{
			get
			{
				return (Identity[])base[ADGroupSchema.Members];
			}
			set
			{
				base[ADGroupSchema.Members] = value.ToIdParameters();
			}
		}

		// Token: 0x170024BD RID: 9405
		// (get) Token: 0x06003F5D RID: 16221 RVA: 0x000BEB94 File Offset: 0x000BCD94
		public override string AssociatedCmdlet
		{
			get
			{
				return "Update-RoleGroupMember";
			}
		}

		// Token: 0x170024BE RID: 9406
		// (get) Token: 0x06003F5E RID: 16222 RVA: 0x000BEB9B File Offset: 0x000BCD9B
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}
	}
}
