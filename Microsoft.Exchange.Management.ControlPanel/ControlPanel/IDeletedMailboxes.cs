using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004E4 RID: 1252
	[ServiceContract(Namespace = "ECP", Name = "DeletedMailboxes")]
	public interface IDeletedMailboxes : IGetListService<DeletedMailboxFilter, DeletedMailboxRow>
	{
	}
}
