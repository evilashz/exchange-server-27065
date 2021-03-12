using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002B1 RID: 689
	[ServiceContract(Namespace = "ECP", Name = "ImapSubscriptions")]
	public interface IImapSubscriptions : INewObjectService<PimSubscriptionRow, NewImapSubscription>, IEditObjectService<ImapSubscription, SetImapSubscription>, IGetObjectService<ImapSubscription>
	{
	}
}
