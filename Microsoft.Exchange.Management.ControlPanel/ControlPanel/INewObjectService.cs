using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000272 RID: 626
	[ServiceContract(Namespace = "ECP", Name = "INewObjectService")]
	public interface INewObjectService<L, C> where L : BaseRow
	{
		// Token: 0x0600299B RID: 10651
		[OperationContract]
		PowerShellResults<L> NewObject(C properties);
	}
}
