using System;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x02000787 RID: 1927
	internal interface IProvisioningTemplate : IProvisioningRule
	{
		// Token: 0x0600605E RID: 24670
		void Provision(ADProvisioningPolicy templatePolicy, IConfigurable writablePresentationObject);
	}
}
