using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.Hybrid;

namespace Microsoft.Exchange.Management.Deployment.HybridConfigurationDetection
{
	// Token: 0x02000031 RID: 49
	internal interface ITenantHybridDetectionCmdlet
	{
		// Token: 0x060000CB RID: 203
		void Connect(PSCredential psCredential, string targetServer, ILogger logger);

		// Token: 0x060000CC RID: 204
		IEnumerable<OrganizationConfig> GetOrganizationConfig();

		// Token: 0x060000CD RID: 205
		void Dispose();
	}
}
