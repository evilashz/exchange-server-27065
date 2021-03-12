using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000027 RID: 39
	internal enum CatalogState
	{
		// Token: 0x04000027 RID: 39
		Unknown,
		// Token: 0x04000028 RID: 40
		Healthy,
		// Token: 0x04000029 RID: 41
		Seeding,
		// Token: 0x0400002A RID: 42
		Suspended,
		// Token: 0x0400002B RID: 43
		Failed
	}
}
