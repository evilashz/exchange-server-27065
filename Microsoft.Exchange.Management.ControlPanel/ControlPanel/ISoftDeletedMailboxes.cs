using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004E8 RID: 1256
	[ServiceContract(Namespace = "ECP", Name = "SoftDeletedMailboxes")]
	public interface ISoftDeletedMailboxes : IGetListService<SoftDeletedMailboxFilter, SoftDeletedMailboxRow>
	{
	}
}
