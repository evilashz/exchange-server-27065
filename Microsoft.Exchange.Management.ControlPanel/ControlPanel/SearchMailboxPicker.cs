using System;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000351 RID: 849
	public class SearchMailboxPicker : RecipientPickerBase<SearchMailboxPickerFilter, RecipientPickerObject>, ISearchMailboxPicker, IGetListService<SearchMailboxPickerFilter, RecipientPickerObject>
	{
		// Token: 0x06002F94 RID: 12180 RVA: 0x0009129F File Offset: 0x0008F49F
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties&RecipientPreviewFilter")]
		public new PowerShellResults<RecipientPickerObject> GetList(SearchMailboxPickerFilter filter, SortOptions sort)
		{
			return base.GetList(filter, sort);
		}

		// Token: 0x04002310 RID: 8976
		private const string GetListRole = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties&RecipientPreviewFilter";
	}
}
