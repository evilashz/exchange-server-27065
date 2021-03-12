using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200024F RID: 591
	[Flags]
	internal enum RouteComparison
	{
		// Token: 0x04000C3E RID: 3134
		None = 0,
		// Token: 0x04000C3F RID: 3135
		CompareNames = 1,
		// Token: 0x04000C40 RID: 3136
		CompareRestrictions = 2,
		// Token: 0x04000C41 RID: 3137
		IgnoreRGCosts = 4
	}
}
