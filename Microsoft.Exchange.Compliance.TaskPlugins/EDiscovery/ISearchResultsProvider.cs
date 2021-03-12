using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol.EDiscovery;

namespace Microsoft.Exchange.Compliance.TaskPlugins.EDiscovery
{
	// Token: 0x02000003 RID: 3
	internal interface ISearchResultsProvider
	{
		// Token: 0x06000001 RID: 1
		SearchResult PerformSearch(ComplianceMessage target, SearchWorkDefinition definition);

		// Token: 0x06000002 RID: 2
		SearchWorkDefinition ParseSearch(ComplianceMessage target, SearchWorkDefinition definition);
	}
}
