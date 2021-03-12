using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000527 RID: 1319
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class ManagementScopes : DataSourceService, IManagementScopes, IGetListService<ManagementScopeFilter, ManagementScopeRow>
	{
		// Token: 0x06003EE4 RID: 16100 RVA: 0x000BD5A0 File Offset: 0x000BB7A0
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-ManagementScope?Exclusive@R:Organization")]
		public PowerShellResults<ManagementScopeRow> GetList(ManagementScopeFilter filter, SortOptions sort)
		{
			PowerShellResults<ManagementScopeRow> list = base.GetList<ManagementScopeRow, ManagementScopeFilter>("Get-ManagementScope", filter, null, "Name");
			if (list.Succeeded)
			{
				ManagementScopeRow[] output = list.Output;
				List<ManagementScopeRow> list2 = new List<ManagementScopeRow>(output.Length + 1);
				list2.Add(ManagementScopeRow.DefaultRow);
				list2.AddRange(output);
				list.Output = list2.ToArray();
			}
			return list;
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x000BD5F9 File Offset: 0x000BB7F9
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-ManagementScope?Identity@R:Organization")]
		public PowerShellResults<ManagementScopeRow> GetObject(Identity identity)
		{
			return base.GetObject<ManagementScopeRow>("Get-ManagementScope", identity);
		}

		// Token: 0x040028B1 RID: 10417
		internal const string NameProperty = "Name";

		// Token: 0x040028B2 RID: 10418
		internal const string ReadScope = "@R:Organization";

		// Token: 0x040028B3 RID: 10419
		internal const string RbacParameters = "?Exclusive";

		// Token: 0x040028B4 RID: 10420
		internal const string GetListScope = "Get-ManagementScope?Exclusive@R:Organization";

		// Token: 0x040028B5 RID: 10421
		private const string GetObjectRole = "Get-ManagementScope?Identity@R:Organization";
	}
}
