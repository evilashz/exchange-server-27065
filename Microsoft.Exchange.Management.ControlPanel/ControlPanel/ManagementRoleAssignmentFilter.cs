using System;
using Microsoft.Exchange.Management.RbacTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200051B RID: 1307
	public class ManagementRoleAssignmentFilter : WebServiceParameters
	{
		// Token: 0x17002479 RID: 9337
		// (get) Token: 0x06003E95 RID: 16021 RVA: 0x000BD085 File Offset: 0x000BB285
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-ManagementRoleAssignment";
			}
		}

		// Token: 0x1700247A RID: 9338
		// (get) Token: 0x06003E96 RID: 16022 RVA: 0x000BD08C File Offset: 0x000BB28C
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x1700247B RID: 9339
		// (get) Token: 0x06003E97 RID: 16023 RVA: 0x000BD093 File Offset: 0x000BB293
		// (set) Token: 0x06003E98 RID: 16024 RVA: 0x000BD0A5 File Offset: 0x000BB2A5
		public bool Delegating
		{
			get
			{
				return (bool)base[RbacCommonParameters.ParameterDelegating];
			}
			set
			{
				base[RbacCommonParameters.ParameterDelegating] = value;
			}
		}

		// Token: 0x1700247C RID: 9340
		// (get) Token: 0x06003E99 RID: 16025 RVA: 0x000BD0B8 File Offset: 0x000BB2B8
		// (set) Token: 0x06003E9A RID: 16026 RVA: 0x000BD0CA File Offset: 0x000BB2CA
		public Identity Role
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

		// Token: 0x1700247D RID: 9341
		// (get) Token: 0x06003E9B RID: 16027 RVA: 0x000BD0DD File Offset: 0x000BB2DD
		// (set) Token: 0x06003E9C RID: 16028 RVA: 0x000BD0EF File Offset: 0x000BB2EF
		public Identity RoleAssignee
		{
			get
			{
				return Identity.FromIdParameter(base[RbacCommonParameters.ParameterRoleAssignee]);
			}
			set
			{
				base[RbacCommonParameters.ParameterRoleAssignee] = value.ToIdParameter();
			}
		}
	}
}
