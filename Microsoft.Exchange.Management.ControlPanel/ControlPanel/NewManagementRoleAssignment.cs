using System;
using Microsoft.Exchange.Management.RbacTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200051A RID: 1306
	public class NewManagementRoleAssignment : SetManagementRoleAssignment
	{
		// Token: 0x17002475 RID: 9333
		// (get) Token: 0x06003E8D RID: 16013 RVA: 0x000BD00C File Offset: 0x000BB20C
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "New-ManagementRoleAssignment";
			}
		}

		// Token: 0x17002476 RID: 9334
		// (get) Token: 0x06003E8E RID: 16014 RVA: 0x000BD013 File Offset: 0x000BB213
		// (set) Token: 0x06003E8F RID: 16015 RVA: 0x000BD025 File Offset: 0x000BB225
		internal Identity Policy
		{
			get
			{
				return Identity.FromIdParameter(base[RbacCommonParameters.ParameterPolicy]);
			}
			set
			{
				base[RbacCommonParameters.ParameterPolicy] = value.ToIdParameter();
			}
		}

		// Token: 0x17002477 RID: 9335
		// (get) Token: 0x06003E90 RID: 16016 RVA: 0x000BD038 File Offset: 0x000BB238
		// (set) Token: 0x06003E91 RID: 16017 RVA: 0x000BD04A File Offset: 0x000BB24A
		internal Identity Role
		{
			get
			{
				return Identity.FromIdParameter(base[RbacCommonParameters.ParameterRole]);
			}
			set
			{
				base[RbacCommonParameters.ParameterRole] = value.ToIdParameter();
			}
		}

		// Token: 0x17002478 RID: 9336
		// (get) Token: 0x06003E92 RID: 16018 RVA: 0x000BD05D File Offset: 0x000BB25D
		// (set) Token: 0x06003E93 RID: 16019 RVA: 0x000BD06F File Offset: 0x000BB26F
		internal string SecurityGroup
		{
			get
			{
				return (string)base[RbacCommonParameters.ParameterSecurityGroup];
			}
			set
			{
				base[RbacCommonParameters.ParameterSecurityGroup] = value;
			}
		}
	}
}
