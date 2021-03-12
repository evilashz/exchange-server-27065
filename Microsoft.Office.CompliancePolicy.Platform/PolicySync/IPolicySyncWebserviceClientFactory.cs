using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000102 RID: 258
	internal interface IPolicySyncWebserviceClientFactory
	{
		// Token: 0x060006EA RID: 1770
		IPolicySyncWebserviceClient CreatePolicySyncWebserviceClient(EndpointAddress endpoint, ICredentials credential, string partnerName);

		// Token: 0x060006EB RID: 1771
		IPolicySyncWebserviceClient CreatePolicySyncWebserviceClient(EndpointAddress endpoint, X509Certificate2 credential, string partnerName);
	}
}
