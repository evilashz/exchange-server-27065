using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000408 RID: 1032
	[ServiceContract(Namespace = "ECP", Name = "IpSafeListings")]
	public interface IIpSafeListings : IEditObjectService<IpSafeListing, SetIpSafeListing>, IGetObjectService<IpSafeListing>
	{
	}
}
