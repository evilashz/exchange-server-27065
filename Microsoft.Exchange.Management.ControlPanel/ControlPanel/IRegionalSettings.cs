using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000AA RID: 170
	[ServiceContract(Namespace = "ECP", Name = "RegionalSettings")]
	public interface IRegionalSettings : IEditObjectService<RegionalSettingsConfiguration, SetRegionalSettingsConfiguration>, IGetObjectService<RegionalSettingsConfiguration>
	{
	}
}
