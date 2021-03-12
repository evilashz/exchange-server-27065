using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200051C RID: 1308
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class ManagementRoleAssignments : DataSourceService
	{
		// Token: 0x1700247E RID: 9342
		// (get) Token: 0x06003E9E RID: 16030 RVA: 0x000BD10A File Offset: 0x000BB30A
		public int NameMaxLength
		{
			get
			{
				return ManagementRoleAssignments.nameMaxLength;
			}
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x000BD111 File Offset: 0x000BB311
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-ManagementRoleAssignment@R:Organization")]
		public PowerShellResults<ManagementRoleAssignment> GetList(ManagementRoleAssignmentFilter filter, SortOptions sort)
		{
			return base.GetList<ManagementRoleAssignment, ManagementRoleAssignmentFilter>("Get-ManagementRoleAssignment", filter, sort);
		}

		// Token: 0x06003EA0 RID: 16032 RVA: 0x000BD120 File Offset: 0x000BB320
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-ManagementRoleAssignment?Identity@W:Organization")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.RemoveObjects("Remove-ManagementRoleAssignment", identities, parameters);
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x000BD12F File Offset: 0x000BB32F
		[PrincipalPermission(SecurityAction.Demand, Role = "New-ManagementRoleAssignment@W:Organization")]
		public PowerShellResults<ManagementRoleAssignment> NewObject(NewManagementRoleAssignment properties)
		{
			return base.NewObject<ManagementRoleAssignment, NewManagementRoleAssignment>("New-ManagementRoleAssignment", properties);
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x000BD13D File Offset: 0x000BB33D
		[PrincipalPermission(SecurityAction.Demand, Role = "Set-ManagementRoleAssignment@W:Organization")]
		public PowerShellResults<ManagementRoleAssignment> SetObject(Identity identity, SetManagementRoleAssignment properties)
		{
			return base.SetObject<ManagementRoleAssignment, SetManagementRoleAssignment, ManagementRoleAssignment>("Set-ManagementRoleAssignment", identity, properties);
		}

		// Token: 0x04002896 RID: 10390
		private const string Noun = "ManagementRoleAssignment";

		// Token: 0x04002897 RID: 10391
		internal const string NewCmdlet = "New-ManagementRoleAssignment";

		// Token: 0x04002898 RID: 10392
		internal const string GetCmdlet = "Get-ManagementRoleAssignment";

		// Token: 0x04002899 RID: 10393
		internal const string RemoveCmdlet = "Remove-ManagementRoleAssignment";

		// Token: 0x0400289A RID: 10394
		internal const string ReadScope = "@R:Organization";

		// Token: 0x0400289B RID: 10395
		internal const string SetCmdlet = "Set-ManagementRoleAssignment";

		// Token: 0x0400289C RID: 10396
		internal const string WriteScope = "@W:Organization";

		// Token: 0x0400289D RID: 10397
		internal const string GetListRole = "Get-ManagementRoleAssignment@R:Organization";

		// Token: 0x0400289E RID: 10398
		private const string RemoveObjectsRole = "Remove-ManagementRoleAssignment?Identity@W:Organization";

		// Token: 0x0400289F RID: 10399
		private const string NewObjectRole = "New-ManagementRoleAssignment@W:Organization";

		// Token: 0x040028A0 RID: 10400
		private const string SetObjectRole = "Set-ManagementRoleAssignment@W:Organization";

		// Token: 0x040028A1 RID: 10401
		internal const string ChangeRoleAssignmentRole = "Get-ManagementRole@R:Organization+New-ManagementRoleAssignment@W:Organization+Remove-ManagementRoleAssignment?Identity@W:Organization";

		// Token: 0x040028A2 RID: 10402
		private static int nameMaxLength = Util.GetMaxLengthFromDefinition(ADObjectSchema.Name);
	}
}
