using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000285 RID: 645
	[ServiceContract(Namespace = "ECP", Name = "IEditListService")]
	public interface IEditListService<F, L, O, C, R> : IGetListService<F, L>, INewObjectService<L, C>, IRemoveObjectsService<R> where L : BaseRow where O : L
	{
	}
}
