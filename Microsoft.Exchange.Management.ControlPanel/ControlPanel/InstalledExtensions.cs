using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001E3 RID: 483
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class InstalledExtensions : DataSourceService, IInstalledExtensions, IGetListService<ExtensionFilter, ExtensionRow>, IGetObjectService<ExtensionRow>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>
	{
		// Token: 0x060025C0 RID: 9664 RVA: 0x000742E8 File Offset: 0x000724E8
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-App@R:Self")]
		public PowerShellResults<ExtensionRow> GetList(ExtensionFilter filter, SortOptions sort)
		{
			return base.GetList<ExtensionRow, ExtensionFilter>("Get-App", filter, sort);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x000742F7 File Offset: 0x000724F7
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-App?Identity@W:Self")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.RemoveObjects("Remove-App", identities, parameters);
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x00074306 File Offset: 0x00072506
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-App?Identity@R:Self")]
		public PowerShellResults<ExtensionRow> GetObject(Identity identity)
		{
			return base.GetObject<ExtensionRow>("Get-App", identity);
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x00074314 File Offset: 0x00072514
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-App?Identity@R:Self")]
		public PowerShellResults<ExtensionRow> GetObjectForList(Identity identity)
		{
			return base.GetObjectForList<ExtensionRow>("Get-App", identity);
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x00074322 File Offset: 0x00072522
		[PrincipalPermission(SecurityAction.Demand, Role = "Disable-App?Identity@W:Self")]
		public PowerShellResults<ExtensionRow> Disable(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.InvokeAndGetObject<ExtensionRow>(new PSCommand().AddCommand("Disable-App"), identities, parameters);
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x0007433B File Offset: 0x0007253B
		[PrincipalPermission(SecurityAction.Demand, Role = "Enable-App?Identity@W:Self")]
		public PowerShellResults<ExtensionRow> Enable(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.InvokeAndGetObject<ExtensionRow>(new PSCommand().AddCommand("Enable-App"), identities, parameters);
		}

		// Token: 0x04001F12 RID: 7954
		internal const string ReadScope = "@R:Self";

		// Token: 0x04001F13 RID: 7955
		internal const string WriteScope = "@W:Self";

		// Token: 0x04001F14 RID: 7956
		internal const string GetExtension = "Get-App";

		// Token: 0x04001F15 RID: 7957
		internal const string RemoveExtension = "Remove-App";

		// Token: 0x04001F16 RID: 7958
		internal const string DisableExtension = "Disable-App";

		// Token: 0x04001F17 RID: 7959
		internal const string EnableExtension = "Enable-App";

		// Token: 0x04001F18 RID: 7960
		internal const string NewExtension = "New-App";

		// Token: 0x04001F19 RID: 7961
		internal const string GetListRole = "Get-App@R:Self";

		// Token: 0x04001F1A RID: 7962
		internal const string RemoveObjectsRole = "Remove-App?Identity@W:Self";

		// Token: 0x04001F1B RID: 7963
		internal const string GetObjectRole = "Get-App?Identity@R:Self";

		// Token: 0x04001F1C RID: 7964
		internal const string DisableExtensionRole = "Disable-App?Identity@W:Self";

		// Token: 0x04001F1D RID: 7965
		internal const string EnableExtensionRole = "Enable-App?Identity@W:Self";
	}
}
