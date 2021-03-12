using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Deployment.HybridConfigurationDetection
{
	// Token: 0x02000030 RID: 48
	internal interface IOnPremisesHybridDetectionCmdlets
	{
		// Token: 0x060000C9 RID: 201
		IEnumerable<AcceptedDomain> GetAcceptedDomain();

		// Token: 0x060000CA RID: 202
		IEnumerable<OrganizationRelationship> GetOrganizationRelationship();
	}
}
