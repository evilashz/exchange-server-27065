using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000A9 RID: 169
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IBestCopySelector
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060006E6 RID: 1766
		AmBcsType BestCopySelectionType { get; }

		// Token: 0x060006E7 RID: 1767
		AmServerName FindNextBestCopy();

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060006E8 RID: 1768
		IAmBcsErrorLogger ErrorLogger { get; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060006E9 RID: 1769
		Exception LastException { get; }
	}
}
