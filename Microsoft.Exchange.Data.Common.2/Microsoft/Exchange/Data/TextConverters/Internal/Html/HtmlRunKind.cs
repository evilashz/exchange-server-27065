using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x02000222 RID: 546
	internal enum HtmlRunKind : uint
	{
		// Token: 0x04001923 RID: 6435
		Invalid,
		// Token: 0x04001924 RID: 6436
		Text = 67108864U,
		// Token: 0x04001925 RID: 6437
		TagPrefix = 134217728U,
		// Token: 0x04001926 RID: 6438
		TagSuffix = 201326592U,
		// Token: 0x04001927 RID: 6439
		Name = 268435456U,
		// Token: 0x04001928 RID: 6440
		NamePrefixDelimiter = 285212672U,
		// Token: 0x04001929 RID: 6441
		TagWhitespace = 335544320U,
		// Token: 0x0400192A RID: 6442
		AttrEqual = 402653184U,
		// Token: 0x0400192B RID: 6443
		AttrQuote = 469762048U,
		// Token: 0x0400192C RID: 6444
		AttrValue = 536870912U,
		// Token: 0x0400192D RID: 6445
		TagText = 603979776U
	}
}
