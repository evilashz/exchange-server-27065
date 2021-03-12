using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x02000221 RID: 545
	internal enum HtmlLexicalUnit : uint
	{
		// Token: 0x04001918 RID: 6424
		Invalid,
		// Token: 0x04001919 RID: 6425
		Text = 67108864U,
		// Token: 0x0400191A RID: 6426
		TagPrefix = 134217728U,
		// Token: 0x0400191B RID: 6427
		TagSuffix = 201326592U,
		// Token: 0x0400191C RID: 6428
		Name = 268435456U,
		// Token: 0x0400191D RID: 6429
		TagWhitespace = 335544320U,
		// Token: 0x0400191E RID: 6430
		AttrEqual = 402653184U,
		// Token: 0x0400191F RID: 6431
		AttrQuote = 469762048U,
		// Token: 0x04001920 RID: 6432
		AttrValue = 536870912U,
		// Token: 0x04001921 RID: 6433
		TagText = 603979776U
	}
}
