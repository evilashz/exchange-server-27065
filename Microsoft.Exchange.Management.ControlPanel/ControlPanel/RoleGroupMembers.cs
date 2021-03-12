using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200053E RID: 1342
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class RoleGroupMembers : DataSourceService
	{
		// Token: 0x06003F60 RID: 16224 RVA: 0x000BEBAA File Offset: 0x000BCDAA
		[PrincipalPermission(SecurityAction.Demand, Role = "Update-RoleGroupMember?Identity")]
		public PowerShellResults<RoleGroupMembersRow> SetObject(Identity identity, SetRoleGroupMembersParameter properties)
		{
			return base.SetObject<RoleGroupMembersRow, SetRoleGroupMembersParameter, RoleGroupMembersRow>("Update-RoleGroupMember", identity, properties);
		}

		// Token: 0x040028F4 RID: 10484
		internal const string UpdateMembersCmdlet = "Update-RoleGroupMember";

		// Token: 0x040028F5 RID: 10485
		internal const string ReadScope = "@R:Organization";

		// Token: 0x040028F6 RID: 10486
		private const string SetObjectRole = "Update-RoleGroupMember?Identity";
	}
}
