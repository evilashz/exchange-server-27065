using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000474 RID: 1140
	[ServiceContract(Namespace = "ECP", Name = "JunkEmailConfigurations")]
	public interface IJunkEmailConfigurations : IEditObjectService<JunkEmailConfiguration, SetJunkEmailConfiguration>, IGetObjectService<JunkEmailConfiguration>
	{
	}
}
