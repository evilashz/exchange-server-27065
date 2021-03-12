using System;

namespace Microsoft.Exchange.Data.ContentTypes.Internal
{
	// Token: 0x020000CF RID: 207
	[Flags]
	internal enum ContentLineWriteState
	{
		// Token: 0x040006F8 RID: 1784
		Start = 1,
		// Token: 0x040006F9 RID: 1785
		Property = 2,
		// Token: 0x040006FA RID: 1786
		PropertyValue = 4,
		// Token: 0x040006FB RID: 1787
		PropertyEnd = 8,
		// Token: 0x040006FC RID: 1788
		Parameter = 16,
		// Token: 0x040006FD RID: 1789
		ParameterValue = 32,
		// Token: 0x040006FE RID: 1790
		ParameterEnd = 64,
		// Token: 0x040006FF RID: 1791
		Closed = 128
	}
}
