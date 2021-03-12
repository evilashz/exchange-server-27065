using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000A4 RID: 164
	[Flags]
	internal enum DsnRequestedFlags
	{
		// Token: 0x04000260 RID: 608
		Default = 0,
		// Token: 0x04000261 RID: 609
		Success = 1,
		// Token: 0x04000262 RID: 610
		Failure = 2,
		// Token: 0x04000263 RID: 611
		Delay = 4,
		// Token: 0x04000264 RID: 612
		Never = 8,
		// Token: 0x04000265 RID: 613
		AllFlags = 15
	}
}
