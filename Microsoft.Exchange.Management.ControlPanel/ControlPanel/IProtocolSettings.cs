using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002BF RID: 703
	[ServiceContract(Namespace = "ECP", Name = "ProtocolSettings")]
	public interface IProtocolSettings : IGetObjectService<ProtocolSettingsData>
	{
	}
}
