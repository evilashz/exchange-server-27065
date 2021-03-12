using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002D3 RID: 723
	[ServiceContract(Namespace = "ECP", Name = "PopSubscriptions")]
	public interface IPopSubscriptions : INewObjectService<PimSubscriptionRow, NewPopSubscription>, IEditObjectService<PopSubscription, SetPopSubscription>, IGetObjectService<PopSubscription>
	{
	}
}
