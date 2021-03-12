using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000225 RID: 549
	[Flags]
	internal enum StateStorageFeatures
	{
		// Token: 0x0400101B RID: 4123
		ContentState = 1,
		// Token: 0x0400101C RID: 4124
		IdMap = 2,
		// Token: 0x0400101D RID: 4125
		HierarchyState = 4
	}
}
