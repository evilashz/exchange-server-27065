using System;

namespace System.Globalization
{
	// Token: 0x02000381 RID: 897
	[Flags]
	internal enum DateTimeFormatFlags
	{
		// Token: 0x040012B6 RID: 4790
		None = 0,
		// Token: 0x040012B7 RID: 4791
		UseGenitiveMonth = 1,
		// Token: 0x040012B8 RID: 4792
		UseLeapYearMonth = 2,
		// Token: 0x040012B9 RID: 4793
		UseSpacesInMonthNames = 4,
		// Token: 0x040012BA RID: 4794
		UseHebrewRule = 8,
		// Token: 0x040012BB RID: 4795
		UseSpacesInDayNames = 16,
		// Token: 0x040012BC RID: 4796
		UseDigitPrefixInTokens = 32,
		// Token: 0x040012BD RID: 4797
		NotInitialized = -1
	}
}
