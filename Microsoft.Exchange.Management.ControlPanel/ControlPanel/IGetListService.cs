using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000083 RID: 131
	[ServiceContract(Namespace = "ECP", Name = "IGetListService")]
	public interface IGetListService<F, L> where L : BaseRow
	{
		// Token: 0x06001B8C RID: 7052
		[OperationContract]
		PowerShellResults<L> GetList(F filter, SortOptions sort);
	}
}
