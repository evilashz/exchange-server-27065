using System;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000317 RID: 791
	public class AdminPicker : RecipientPickerBase<AdminPickerFilter, RecipientPickerObject>, IAdminPicker, IGetListService<AdminPickerFilter, RecipientPickerObject>
	{
		// Token: 0x06002EA4 RID: 11940 RVA: 0x0008E95B File Offset: 0x0008CB5B
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties")]
		public new PowerShellResults<RecipientPickerObject> GetList(AdminPickerFilter filter, SortOptions sort)
		{
			return base.GetList(filter, sort);
		}

		// Token: 0x040022CE RID: 8910
		private const string GetListRole = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties";
	}
}
