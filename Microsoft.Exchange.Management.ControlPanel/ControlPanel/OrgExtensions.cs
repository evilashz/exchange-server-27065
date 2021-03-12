using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001E5 RID: 485
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class OrgExtensions : DataSourceService, IOrgExtensions, IGetListService<ExtensionFilter, ExtensionRow>, IGetObjectService<ExtensionRow>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>
	{
		// Token: 0x060025C7 RID: 9671 RVA: 0x0007435C File Offset: 0x0007255C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-App@R:Organization")]
		public PowerShellResults<ExtensionRow> GetList(ExtensionFilter filter, SortOptions sort)
		{
			return base.GetList<ExtensionRow, ExtensionFilter>("Get-App", filter, sort);
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x0007436B File Offset: 0x0007256B
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-App?Identity@W:Organization")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.RemoveObjects("Remove-App", identities, parameters);
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x0007437A File Offset: 0x0007257A
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-App?Identity@R:Organization")]
		public PowerShellResults<ExtensionRow> GetObject(Identity identity)
		{
			return base.GetObject<ExtensionRow>("Get-App", identity);
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x00074388 File Offset: 0x00072588
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-App?Identity@R:Organization")]
		public PowerShellResults<ExtensionRow> GetObjectForList(Identity identity)
		{
			return base.GetObjectForList<ExtensionRow>("Get-App", identity);
		}

		// Token: 0x04001F1E RID: 7966
		internal const string ReadScope = "@R:Organization";

		// Token: 0x04001F1F RID: 7967
		internal const string WriteScope = "@W:Organization";

		// Token: 0x04001F20 RID: 7968
		internal const string GetExtension = "Get-App";

		// Token: 0x04001F21 RID: 7969
		internal const string RemoveExtension = "Remove-App";

		// Token: 0x04001F22 RID: 7970
		internal const string NewExtension = "New-App";

		// Token: 0x04001F23 RID: 7971
		internal const string GetListRole = "Get-App@R:Organization";

		// Token: 0x04001F24 RID: 7972
		internal const string RemoveObjectsRole = "Remove-App?Identity@W:Organization";

		// Token: 0x04001F25 RID: 7973
		internal const string GetObjectRole = "Get-App?Identity@R:Organization";
	}
}
