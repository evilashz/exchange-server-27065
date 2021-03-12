using System;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200035E RID: 862
	public class SourceMailboxPicker : RecipientPickerBase<SourceMailboxPickerFilter, RecipientPickerObject>, ISourceMailboxPicker, IGetListService<SourceMailboxPickerFilter, RecipientPickerObject>
	{
		// Token: 0x06002FCA RID: 12234 RVA: 0x000919DC File Offset: 0x0008FBDC
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties&RecipientPreviewFilter")]
		public new PowerShellResults<RecipientPickerObject> GetList(SourceMailboxPickerFilter filter, SortOptions sort)
		{
			return base.GetList(filter, sort);
		}

		// Token: 0x0400231C RID: 8988
		private const string GetListRole = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties&RecipientPreviewFilter";
	}
}
