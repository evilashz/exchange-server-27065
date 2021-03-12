using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200030E RID: 782
	[ServiceContract(Namespace = "ECP", Name = "QuarantinedDevices")]
	public interface IQuarantinedDevices : IGetListService<QuarantinedDeviceFilter, QuarantinedDevice>
	{
		// Token: 0x06002E6F RID: 11887
		[OperationContract]
		PowerShellResults AllowDevice(Identity[] identities, BaseWebServiceParameters parameters);

		// Token: 0x06002E70 RID: 11888
		[OperationContract]
		PowerShellResults BlockDevice(Identity[] identities, BaseWebServiceParameters parameters);
	}
}
