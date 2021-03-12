using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200044E RID: 1102
	[ServiceContract(Namespace = "ECP", Name = "SubscriptionItems")]
	public interface ISubscriptionItems : IGetListService<SubscriptionItemFilter, SubscriptionItem>
	{
	}
}
