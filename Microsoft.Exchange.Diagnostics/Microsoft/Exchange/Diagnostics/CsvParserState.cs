using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001AA RID: 426
	internal enum CsvParserState
	{
		// Token: 0x0400089E RID: 2206
		Whitespace,
		// Token: 0x0400089F RID: 2207
		LineEnd,
		// Token: 0x040008A0 RID: 2208
		Field,
		// Token: 0x040008A1 RID: 2209
		FieldCR,
		// Token: 0x040008A2 RID: 2210
		QuotedField,
		// Token: 0x040008A3 RID: 2211
		QuotedFieldCR,
		// Token: 0x040008A4 RID: 2212
		QuotedFieldQuote,
		// Token: 0x040008A5 RID: 2213
		EndQuote,
		// Token: 0x040008A6 RID: 2214
		EndQuoteIgnore
	}
}
