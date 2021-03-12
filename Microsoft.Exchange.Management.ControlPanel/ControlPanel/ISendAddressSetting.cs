using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002EA RID: 746
	[ServiceContract(Namespace = "ECP", Name = "SendAddressSetting")]
	public interface ISendAddressSetting : IEditObjectService<SendAddressConfiguration, SetSendAddressDefaultConfiguration>, IGetObjectService<SendAddressConfiguration>
	{
	}
}
