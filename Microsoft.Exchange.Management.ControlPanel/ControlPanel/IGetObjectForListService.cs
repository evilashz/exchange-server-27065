using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000237 RID: 567
	[ServiceContract(Namespace = "ECP", Name = "IGetObjectForListService")]
	public interface IGetObjectForListService<L>
	{
		// Token: 0x060027D6 RID: 10198
		[OperationContract]
		PowerShellResults<L> GetObjectForList(Identity identity);
	}
}
