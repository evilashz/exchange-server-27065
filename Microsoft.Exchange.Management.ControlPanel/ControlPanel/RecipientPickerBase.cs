using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001CF RID: 463
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public abstract class RecipientPickerBase<F, L> : DataSourceService where F : RecipientPickerFilterBase, new() where L : RecipientPickerObject
	{
		// Token: 0x0600254F RID: 9551 RVA: 0x00072473 File Offset: 0x00070673
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties")]
		public PowerShellResults<L> GetList(F filter, SortOptions sort)
		{
			return base.GetList<L, F>("Get-Recipient", filter, sort, "DisplayName");
		}

		// Token: 0x04001EC9 RID: 7881
		private const string GetListRole = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties";
	}
}
