using System;
using Microsoft.Exchange.Management.RbacTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000519 RID: 1305
	public class SetManagementRoleAssignment : SetObjectProperties
	{
		// Token: 0x1700246F RID: 9327
		// (get) Token: 0x06003E82 RID: 16002 RVA: 0x000BCF67 File Offset: 0x000BB167
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-ManagementRoleAssignment";
			}
		}

		// Token: 0x17002470 RID: 9328
		// (get) Token: 0x06003E83 RID: 16003 RVA: 0x000BCF6E File Offset: 0x000BB16E
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x17002471 RID: 9329
		// (get) Token: 0x06003E84 RID: 16004 RVA: 0x000BCF75 File Offset: 0x000BB175
		// (set) Token: 0x06003E85 RID: 16005 RVA: 0x000BCF87 File Offset: 0x000BB187
		internal Identity OrganizationalUnit
		{
			get
			{
				return (Identity)base[RbacCommonParameters.ParameterRecipientOrganizationalUnitScope];
			}
			set
			{
				base[RbacCommonParameters.ParameterRecipientOrganizationalUnitScope] = value.ToIdParameter();
			}
		}

		// Token: 0x17002472 RID: 9330
		// (get) Token: 0x06003E86 RID: 16006 RVA: 0x000BCF9A File Offset: 0x000BB19A
		// (set) Token: 0x06003E87 RID: 16007 RVA: 0x000BCFAC File Offset: 0x000BB1AC
		internal Identity RecipientWriteScope
		{
			get
			{
				return (Identity)base[RbacCommonParameters.ParameterCustomRecipientWriteScope];
			}
			set
			{
				base[RbacCommonParameters.ParameterCustomRecipientWriteScope] = value.ToIdParameter();
			}
		}

		// Token: 0x17002473 RID: 9331
		// (get) Token: 0x06003E88 RID: 16008 RVA: 0x000BCFBF File Offset: 0x000BB1BF
		// (set) Token: 0x06003E89 RID: 16009 RVA: 0x000BCFD1 File Offset: 0x000BB1D1
		internal Identity ConfigWriteScope
		{
			get
			{
				return (Identity)base[RbacCommonParameters.ParameterCustomConfigWriteScope];
			}
			set
			{
				base[RbacCommonParameters.ParameterCustomConfigWriteScope] = value.ToIdParameter();
			}
		}

		// Token: 0x17002474 RID: 9332
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x000BCFE4 File Offset: 0x000BB1E4
		// (set) Token: 0x06003E8B RID: 16011 RVA: 0x000BCFF6 File Offset: 0x000BB1F6
		internal string Name
		{
			get
			{
				return (string)base["Name"];
			}
			set
			{
				base["Name"] = value;
			}
		}
	}
}
