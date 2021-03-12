using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002A1 RID: 673
	[ServiceContract(Namespace = "ECP", Name = "EmailSubscriptions")]
	public interface IEmailSubscriptions : INewObjectService<PimSubscription, NewSubscription>, IGetListService<EmailSubscriptionFilter, PimSubscriptionRow>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>
	{
		// Token: 0x06002B7F RID: 11135
		[OperationContract]
		PowerShellResults<PimSubscriptionRow> ResendPopVerificationEmail(Identity[] identities, BaseWebServiceParameters parameters);

		// Token: 0x06002B80 RID: 11136
		[OperationContract]
		PowerShellResults<PimSubscriptionRow> ResendImapVerificationEmail(Identity[] identities, BaseWebServiceParameters parameters);
	}
}
