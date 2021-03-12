using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000B7 RID: 183
	[Flags]
	internal enum UMDisplayAttributes
	{
		// Token: 0x040003AB RID: 939
		None = 0,
		// Token: 0x040003AC RID: 940
		ZeroTrailingSpaces = 2,
		// Token: 0x040003AD RID: 941
		OneTrailingSpace = 4,
		// Token: 0x040003AE RID: 942
		TwoTrailingSpaces = 8,
		// Token: 0x040003AF RID: 943
		ConsumeLeadingSpaces = 16
	}
}
