using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000454 RID: 1108
	[ServiceContract(Namespace = "ECP", Name = "TransportConfigs")]
	public interface ITransportConfigs : IGetListService<TransportConfigFilter, SupervisionTag>
	{
	}
}
