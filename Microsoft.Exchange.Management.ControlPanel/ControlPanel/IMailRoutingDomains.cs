using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200041A RID: 1050
	[ServiceContract(Namespace = "ECP", Name = "MailRoutingDomains")]
	public interface IMailRoutingDomains : IGetListService<MailRoutingDomainFilter, MailRoutingDomain>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectService<MailRoutingDomain, SetMailRoutingDomain>, IGetObjectService<MailRoutingDomain>
	{
	}
}
