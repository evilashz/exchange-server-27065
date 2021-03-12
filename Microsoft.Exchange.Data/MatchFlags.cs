using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000049 RID: 73
	[Flags]
	internal enum MatchFlags
	{
		// Token: 0x040000C0 RID: 192
		Default = 0,
		// Token: 0x040000C1 RID: 193
		IgnoreCase = 1,
		// Token: 0x040000C2 RID: 194
		IgnoreNonSpace = 2,
		// Token: 0x040000C3 RID: 195
		Loose = 4
	}
}
