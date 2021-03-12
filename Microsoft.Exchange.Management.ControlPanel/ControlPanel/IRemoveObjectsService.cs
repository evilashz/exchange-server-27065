using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000084 RID: 132
	[ServiceContract(Namespace = "ECP", Name = "IRemoveObjectsService")]
	public interface IRemoveObjectsService<R>
	{
		// Token: 0x06001B8D RID: 7053
		[OperationContract]
		PowerShellResults RemoveObjects(Identity[] identities, R parameters);
	}
}
