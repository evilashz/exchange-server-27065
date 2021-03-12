using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002EB RID: 747
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NoncachingReplayAdObjectLookupPartiallyConsistent : NoncachingReplayAdObjectLookup
	{
		// Token: 0x06001E13 RID: 7699 RVA: 0x00089A40 File Offset: 0x00087C40
		public NoncachingReplayAdObjectLookupPartiallyConsistent()
		{
			base.Initialize(ConsistencyMode.PartiallyConsistent);
		}
	}
}
