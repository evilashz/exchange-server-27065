using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000495 RID: 1173
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class SupervisionListEntryService : DataSourceService, ISupervisionListEntryService, IGetListService<SupervisionListEntryFilter, SupervisionListEntryRow>
	{
		// Token: 0x06003A68 RID: 14952 RVA: 0x000B095B File Offset: 0x000AEB5B
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-SupervisionListEntry?Tag&Identity@R:Self")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-SupervisionListEntry?Tag&Identity@R:Organization")]
		public PowerShellResults<SupervisionListEntryRow> GetList(SupervisionListEntryFilter filter, SortOptions sort)
		{
			filter.Identity = Identity.FromExecutingUserId();
			return base.GetList<SupervisionListEntryRow, SupervisionListEntryFilter>("Get-SupervisionListEntry", filter, sort);
		}

		// Token: 0x040026F2 RID: 9970
		internal const string GetSupervisionListEntry = "Get-SupervisionListEntry";

		// Token: 0x040026F3 RID: 9971
		internal const string AddSupervisionListEntry = "Add-SupervisionListEntry";

		// Token: 0x040026F4 RID: 9972
		internal const string RemoveSupervisionListEntry = "Remove-SupervisionListEntry";

		// Token: 0x040026F5 RID: 9973
		internal const string ReadScopeOrg = "@R:Organization";

		// Token: 0x040026F6 RID: 9974
		internal const string ReadScopeSelf = "@R:Self";

		// Token: 0x040026F7 RID: 9975
		internal const string WriteScope = "@W:Organization";

		// Token: 0x040026F8 RID: 9976
		internal const string Allow = "allow";

		// Token: 0x040026F9 RID: 9977
		internal const string Reject = "reject";

		// Token: 0x040026FA RID: 9978
		private const string GetListRoleOrg = "Get-SupervisionListEntry?Tag&Identity@R:Organization";

		// Token: 0x040026FB RID: 9979
		private const string GetListRoleSelf = "Get-SupervisionListEntry?Tag&Identity@R:Self";
	}
}
