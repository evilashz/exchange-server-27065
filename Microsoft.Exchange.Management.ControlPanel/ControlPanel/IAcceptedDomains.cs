using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000250 RID: 592
	[ServiceContract(Namespace = "ECP", Name = "AcceptedDomains")]
	public interface IAcceptedDomains : IGetListService<AcceptedDomainFilter, AcceptedDomain>
	{
	}
}
