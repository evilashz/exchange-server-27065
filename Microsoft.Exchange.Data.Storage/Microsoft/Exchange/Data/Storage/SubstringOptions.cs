using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002A9 RID: 681
	[Flags]
	internal enum SubstringOptions
	{
		// Token: 0x0400136B RID: 4971
		None = 0,
		// Token: 0x0400136C RID: 4972
		IgnoreMissingLeftDelimiter = 1,
		// Token: 0x0400136D RID: 4973
		IgnoreMissingRightDelimiter = 2,
		// Token: 0x0400136E RID: 4974
		Backward = 4
	}
}
