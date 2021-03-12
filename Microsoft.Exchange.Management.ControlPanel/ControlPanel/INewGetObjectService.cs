using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000309 RID: 777
	[ServiceContract(Namespace = "ECP", Name = "INewGetObjectService")]
	public interface INewGetObjectService<L, C, W> : INewObjectService<L, C> where L : BaseRow
	{
		// Token: 0x06002E59 RID: 11865
		[OperationContract]
		PowerShellResults<W> GetObjectForNew(Identity identity);
	}
}
