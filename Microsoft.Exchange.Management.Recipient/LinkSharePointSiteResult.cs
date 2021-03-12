using System;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000E6 RID: 230
	internal enum LinkSharePointSiteResult
	{
		// Token: 0x04000366 RID: 870
		Success,
		// Token: 0x04000367 RID: 871
		NotSiteOwner,
		// Token: 0x04000368 RID: 872
		SPServerVersionNotCompatible,
		// Token: 0x04000369 RID: 873
		NotTeamMailboxOwner,
		// Token: 0x0400036A RID: 874
		AlreadyLinkedBySelf,
		// Token: 0x0400036B RID: 875
		CurrentlyNotLinked,
		// Token: 0x0400036C RID: 876
		LinkedByOthers,
		// Token: 0x0400036D RID: 877
		ResultNotSet
	}
}
