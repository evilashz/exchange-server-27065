using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000003 RID: 3
	internal interface IADDataProvider
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2
		// (set) Token: 0x06000003 RID: 3
		int SizeLimit { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4
		// (set) Token: 0x06000005 RID: 5
		int PageSize { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6
		TimeSpan ServerTimeLimit { get; }

		// Token: 0x06000007 RID: 7
		SearchResultCollection Run(bool useGC, string directoryEntry, string[] listOfPropertiesToCollect, string filter, SearchScope searchScope);

		// Token: 0x06000008 RID: 8
		List<string> Run(bool useGC, string directoryEntry);
	}
}
