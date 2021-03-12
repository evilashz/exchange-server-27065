using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000064 RID: 100
	[ServiceContract(Namespace = "ECP", Name = "IGetObjectService")]
	public interface IGetObjectService<O>
	{
		// Token: 0x06001A7E RID: 6782
		[OperationContract]
		PowerShellResults<O> GetObject(Identity identity);
	}
}
