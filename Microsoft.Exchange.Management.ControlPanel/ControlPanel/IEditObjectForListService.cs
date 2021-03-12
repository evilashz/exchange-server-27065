using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000238 RID: 568
	[ServiceContract(Namespace = "ECP", Name = "IEditObjectService")]
	public interface IEditObjectForListService<O, U, L> : IGetObjectService<O>, IGetObjectForListService<L> where O : L where L : BaseRow
	{
		// Token: 0x060027D7 RID: 10199
		[OperationContract]
		PowerShellResults<L> SetObject(Identity identity, U properties);
	}
}
