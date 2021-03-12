using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200049D RID: 1181
	[ServiceContract(Namespace = "ECP", Name = "MobileDevices")]
	public interface IMobileDevices : IGetListService<MobileDeviceFilter, MobileDeviceRow>, IGetObjectService<MobileDevice>, IGetObjectForListService<MobileDeviceRow>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>
	{
		// Token: 0x06003ACF RID: 15055
		[OperationContract]
		PowerShellResults<MobileDeviceRow> BlockOrWipeDevice(Identity[] identities, BaseWebServiceParameters parameters);

		// Token: 0x06003AD0 RID: 15056
		[OperationContract]
		PowerShellResults<MobileDeviceRow> UnBlockOrCancelWipeDevice(Identity[] identities, BaseWebServiceParameters parameters);

		// Token: 0x06003AD1 RID: 15057
		[OperationContract]
		PowerShellResults StartLogging(Identity[] identities, BaseWebServiceParameters parameters);

		// Token: 0x06003AD2 RID: 15058
		[OperationContract]
		PowerShellResults StopAndRetrieveLog(Identity[] identities, BaseWebServiceParameters parameters);
	}
}
