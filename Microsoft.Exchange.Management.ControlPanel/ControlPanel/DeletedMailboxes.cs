using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004E5 RID: 1253
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class DeletedMailboxes : DataSourceService, IDeletedMailboxes, IGetListService<DeletedMailboxFilter, DeletedMailboxRow>
	{
		// Token: 0x06003CFE RID: 15614 RVA: 0x000B716D File Offset: 0x000B536D
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-RemovedMailbox@R:Organization")]
		public PowerShellResults<DeletedMailboxRow> GetList(DeletedMailboxFilter filter, SortOptions sort)
		{
			return base.GetList<DeletedMailboxRow, DeletedMailboxFilter>("Get-RemovedMailbox", filter, sort, "DeletionDate");
		}

		// Token: 0x040027D8 RID: 10200
		private const string GetListRole = "Get-RemovedMailbox@R:Organization";
	}
}
