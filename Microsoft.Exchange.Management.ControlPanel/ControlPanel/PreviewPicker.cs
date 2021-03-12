using System;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001D1 RID: 465
	public class PreviewPicker : RecipientPickerBase<PreviewPickerFilter, RecipientPickerObject>, IPreviewPicker, IGetListService<PreviewPickerFilter, RecipientPickerObject>
	{
		// Token: 0x06002551 RID: 9553 RVA: 0x0007248F File Offset: 0x0007068F
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties&RecipientPreviewFilter&OrganizationalUnit")]
		public new PowerShellResults<RecipientPickerObject> GetList(PreviewPickerFilter filter, SortOptions sort)
		{
			if (filter.HasCondition)
			{
				return base.GetList(filter, sort);
			}
			return new PowerShellResults<RecipientPickerObject>();
		}

		// Token: 0x04001ECA RID: 7882
		private const string GetListRole = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties&RecipientPreviewFilter&OrganizationalUnit";
	}
}
