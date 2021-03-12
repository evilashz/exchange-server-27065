using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002AF RID: 687
	[ServiceContract(Namespace = "ECP", Name = "HotmailSubscriptions")]
	public interface IHotmailSubscriptions : IEditObjectService<HotmailSubscription, SetHotmailSubscription>, IGetObjectService<HotmailSubscription>
	{
	}
}
