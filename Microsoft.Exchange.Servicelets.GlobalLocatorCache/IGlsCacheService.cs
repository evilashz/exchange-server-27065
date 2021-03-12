using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x0200000F RID: 15
	[ServiceContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService", Name = "LocatorService")]
	public interface IGlsCacheService
	{
		// Token: 0x06000044 RID: 68
		[FaultContract(typeof(LocatorServiceFault))]
		[OperationContract]
		FindTenantResponse FindTenant(RequestIdentity identity, FindTenantRequest findTenantRequest);

		// Token: 0x06000045 RID: 69
		[OperationContract]
		[FaultContract(typeof(LocatorServiceFault))]
		FindDomainResponse FindDomain(RequestIdentity identity, FindDomainRequest findDomainRequest);

		// Token: 0x06000046 RID: 70
		[OperationContract]
		[FaultContract(typeof(LocatorServiceFault))]
		FindDomainsResponse FindDomains(RequestIdentity identity, FindDomainsRequest findDomainsRequest);
	}
}
