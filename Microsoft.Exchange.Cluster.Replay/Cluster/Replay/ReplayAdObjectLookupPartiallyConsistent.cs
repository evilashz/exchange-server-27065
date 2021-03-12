using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002F2 RID: 754
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplayAdObjectLookupPartiallyConsistent : ReplayAdObjectLookup
	{
		// Token: 0x06001E74 RID: 7796 RVA: 0x0008A7C4 File Offset: 0x000889C4
		public ReplayAdObjectLookupPartiallyConsistent()
		{
			base.Initialize(ConsistencyMode.PartiallyConsistent);
		}
	}
}
