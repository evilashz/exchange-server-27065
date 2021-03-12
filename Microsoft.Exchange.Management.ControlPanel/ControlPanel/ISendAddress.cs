using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002E5 RID: 741
	[ServiceContract(Namespace = "ECP", Name = "SendAddress")]
	public interface ISendAddress : IGetListService<SendAddressFilter, SendAddressRow>
	{
	}
}
