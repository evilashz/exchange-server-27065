using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000085 RID: 133
	[ServiceContract(Namespace = "ECP", Name = "IRemoveObjectsService")]
	public interface IRemoveObjectsService : IRemoveObjectsService<BaseWebServiceParameters>
	{
	}
}
