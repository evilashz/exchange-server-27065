using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.Deployment.HybridConfigurationDetection
{
	// Token: 0x0200002D RID: 45
	internal interface IHybridConfigurationDetection
	{
		// Token: 0x060000AE RID: 174
		bool RunTenantHybridTest(PSCredential psCredential, string organizationConfigHash);

		// Token: 0x060000AF RID: 175
		bool RunTenantHybridTest(string pathToConfigFile);

		// Token: 0x060000B0 RID: 176
		bool RunOnPremisesHybridTest();
	}
}
