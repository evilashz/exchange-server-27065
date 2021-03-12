using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000205 RID: 517
	[Flags]
	internal enum AdjacencyOrConflictType
	{
		// Token: 0x04000ECA RID: 3786
		Precedes = 1,
		// Token: 0x04000ECB RID: 3787
		Follows = 2,
		// Token: 0x04000ECC RID: 3788
		Conflicts = 4
	}
}
