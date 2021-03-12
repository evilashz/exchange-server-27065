using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000103 RID: 259
	internal sealed class PolicySyncWebserviceClientFactory : IPolicySyncWebserviceClientFactory
	{
		// Token: 0x060006EC RID: 1772 RVA: 0x00014CE8 File Offset: 0x00012EE8
		public PolicySyncWebserviceClientFactory(ExecutionLog logProvider)
		{
			ArgumentValidator.ThrowIfNull("logProvider", logProvider);
			this.logProvider = logProvider;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00014D02 File Offset: 0x00012F02
		public IPolicySyncWebserviceClient CreatePolicySyncWebserviceClient(EndpointAddress endpoint, ICredentials credential, string partnerName)
		{
			ArgumentValidator.ThrowIfNull("endpoint", endpoint);
			ArgumentValidator.ThrowIfNull("credential", credential);
			ArgumentValidator.ThrowIfNullOrEmpty("partnerName", partnerName);
			return PolicySyncClientProxy.Create(endpoint, credential, partnerName, this.logProvider, true);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00014D34 File Offset: 0x00012F34
		public IPolicySyncWebserviceClient CreatePolicySyncWebserviceClient(EndpointAddress endpoint, X509Certificate2 credential, string partnerName)
		{
			ArgumentValidator.ThrowIfNull("endpoint", endpoint);
			ArgumentValidator.ThrowIfNull("credential", credential);
			ArgumentValidator.ThrowIfNullOrEmpty("partnerName", partnerName);
			return PolicySyncProxy.GetOrCreate(endpoint, credential, partnerName, this.logProvider);
		}

		// Token: 0x040003F3 RID: 1011
		private readonly ExecutionLog logProvider;
	}
}
