using System;
using System.Linq;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000541 RID: 1345
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class ViewAdminRoleGroups : DataSourceService, IViewAdminRoleGroups, IGetObjectService<AdminRoleGroupObject>
	{
		// Token: 0x06003F67 RID: 16231 RVA: 0x000BF0D4 File Offset: 0x000BD2D4
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-RoleGroup?Identity@R:Organization")]
		public PowerShellResults<AdminRoleGroupObject> GetObject(Identity identity)
		{
			PowerShellResults<AdminRoleGroupObject> @object = base.GetObject<AdminRoleGroupObject>(new PSCommand().AddCommand("Get-RoleGroup").AddParameter("ReadFromDomainController"), identity);
			if (@object.SucceededWithoutWarnings && @object.Value.IsMultipleScopesScenario)
			{
				@object.Warnings = @object.Warnings.Concat(new string[]
				{
					Strings.CannotCopyWarning
				}).ToArray<string>();
			}
			return @object;
		}

		// Token: 0x040028FF RID: 10495
		internal const string GetCmdlet = "Get-RoleGroup";

		// Token: 0x04002900 RID: 10496
		internal const string ReadScope = "@R:Organization";

		// Token: 0x04002901 RID: 10497
		private const string GetObjectRole = "Get-RoleGroup?Identity@R:Organization";
	}
}
