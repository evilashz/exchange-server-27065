using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000357 RID: 855
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class SecurityPrincipalPicker : DataSourceService, ISecurityPrincipalPicker, IGetListService<SecurityPrincipalPickerFilter, SecurityPrincipalPickerObject>
	{
		// Token: 0x06002FB0 RID: 12208 RVA: 0x0009154A File Offset: 0x0008F74A
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-SecurityPrincipal?ResultSize&Filter&Types&RoleGroupAssignable")]
		public PowerShellResults<SecurityPrincipalPickerObject> GetList(SecurityPrincipalPickerFilter filter, SortOptions sort)
		{
			return base.GetList<SecurityPrincipalPickerObject, SecurityPrincipalPickerFilter>("Get-SecurityPrincipal", filter, sort, "Name");
		}

		// Token: 0x04002316 RID: 8982
		private const string GetListRole = "Get-SecurityPrincipal?ResultSize&Filter&Types&RoleGroupAssignable";
	}
}
