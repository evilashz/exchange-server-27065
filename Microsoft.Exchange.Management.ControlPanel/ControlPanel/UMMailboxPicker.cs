using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000369 RID: 873
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class UMMailboxPicker : DataSourceService, IUMMailboxPicker, IGetListService<UMMailboxPickerFilter, UMMailboxPickerObject>
	{
		// Token: 0x06002FF2 RID: 12274 RVA: 0x00091F6D File Offset: 0x0009016D
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties")]
		public PowerShellResults<UMMailboxPickerObject> GetList(UMMailboxPickerFilter filter, SortOptions sort)
		{
			return base.GetList<UMMailboxPickerObject, UMMailboxPickerFilter>("Get-Recipient", filter, sort, "DisplayName");
		}

		// Token: 0x0400232A RID: 9002
		private const string GetListRole = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties";
	}
}
