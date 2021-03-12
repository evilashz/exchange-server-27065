using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000325 RID: 805
	[ServiceContract(Namespace = "ECP", Name = "DeviceClassPicker")]
	public interface IDeviceClassPicker : IGetListService<DeviceClassPickerFilter, DeviceClassPickerObject>
	{
	}
}
