using System;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000117 RID: 279
	internal class FindDomainsResult
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x000358E6 File Offset: 0x00033AE6
		internal FindDomainsResult(FindDomainResult[] findDomainResults)
		{
			this.FindDomainResults = new ReadOnlyCollection<FindDomainResult>(findDomainResults);
		}

		// Token: 0x040005E2 RID: 1506
		internal readonly ReadOnlyCollection<FindDomainResult> FindDomainResults;
	}
}
