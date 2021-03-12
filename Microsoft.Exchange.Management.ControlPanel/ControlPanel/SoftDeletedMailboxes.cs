using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004E9 RID: 1257
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class SoftDeletedMailboxes : DataSourceService, ISoftDeletedMailboxes, IGetListService<SoftDeletedMailboxFilter, SoftDeletedMailboxRow>
	{
		// Token: 0x06003D0B RID: 15627 RVA: 0x000B7284 File Offset: 0x000B5484
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Mailbox@R:Organization")]
		public PowerShellResults<SoftDeletedMailboxRow> GetList(SoftDeletedMailboxFilter filter, SortOptions sort)
		{
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("Get-Mailbox");
			pscommand.AddParameter("SoftDeletedMailbox");
			return base.GetList<SoftDeletedMailboxRow, SoftDeletedMailboxFilter>(pscommand, filter, sort, "DeletionDate");
		}

		// Token: 0x040027DD RID: 10205
		private const string GetListRole = "Get-Mailbox@R:Organization";
	}
}
