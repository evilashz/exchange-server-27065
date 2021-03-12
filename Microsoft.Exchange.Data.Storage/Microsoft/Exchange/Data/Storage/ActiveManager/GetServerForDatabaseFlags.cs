using System;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x020002FC RID: 764
	[Flags]
	internal enum GetServerForDatabaseFlags
	{
		// Token: 0x04001419 RID: 5145
		None = 0,
		// Token: 0x0400141A RID: 5146
		ThrowServerForDatabaseNotFoundException = 1,
		// Token: 0x0400141B RID: 5147
		IgnoreAdSiteBoundary = 2,
		// Token: 0x0400141C RID: 5148
		ReadThrough = 4,
		// Token: 0x0400141D RID: 5149
		BasicQuery = 8
	}
}
