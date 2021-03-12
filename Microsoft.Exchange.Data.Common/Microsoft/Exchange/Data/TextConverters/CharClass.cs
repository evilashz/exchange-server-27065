using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200017F RID: 383
	[Flags]
	internal enum CharClass : uint
	{
		// Token: 0x04001114 RID: 4372
		Invalid = 0U,
		// Token: 0x04001115 RID: 4373
		NotInterestingText = 1U,
		// Token: 0x04001116 RID: 4374
		Control = 2U,
		// Token: 0x04001117 RID: 4375
		Whitespace = 4U,
		// Token: 0x04001118 RID: 4376
		Alpha = 8U,
		// Token: 0x04001119 RID: 4377
		Numeric = 16U,
		// Token: 0x0400111A RID: 4378
		Backslash = 32U,
		// Token: 0x0400111B RID: 4379
		LessThan = 64U,
		// Token: 0x0400111C RID: 4380
		Equals = 128U,
		// Token: 0x0400111D RID: 4381
		GreaterThan = 256U,
		// Token: 0x0400111E RID: 4382
		Solidus = 512U,
		// Token: 0x0400111F RID: 4383
		Ampersand = 1024U,
		// Token: 0x04001120 RID: 4384
		Nbsp = 2048U,
		// Token: 0x04001121 RID: 4385
		Comma = 4096U,
		// Token: 0x04001122 RID: 4386
		SingleQuote = 8192U,
		// Token: 0x04001123 RID: 4387
		DoubleQuote = 16384U,
		// Token: 0x04001124 RID: 4388
		GraveAccent = 32768U,
		// Token: 0x04001125 RID: 4389
		Circumflex = 65536U,
		// Token: 0x04001126 RID: 4390
		VerticalLine = 131072U,
		// Token: 0x04001127 RID: 4391
		Parentheses = 262144U,
		// Token: 0x04001128 RID: 4392
		CurlyBrackets = 524288U,
		// Token: 0x04001129 RID: 4393
		SquareBrackets = 1048576U,
		// Token: 0x0400112A RID: 4394
		Tilde = 2097152U,
		// Token: 0x0400112B RID: 4395
		Colon = 4194304U,
		// Token: 0x0400112C RID: 4396
		UniqueMask = 16777215U,
		// Token: 0x0400112D RID: 4397
		AlphaHex = 2147483648U,
		// Token: 0x0400112E RID: 4398
		HtmlSuffix = 1073741824U,
		// Token: 0x0400112F RID: 4399
		RtfInteresting = 536870912U,
		// Token: 0x04001130 RID: 4400
		OverlappedMask = 4278190080U,
		// Token: 0x04001131 RID: 4401
		Quote = 57344U,
		// Token: 0x04001132 RID: 4402
		Brackets = 1572864U,
		// Token: 0x04001133 RID: 4403
		NonWhitespaceText = 16775163U,
		// Token: 0x04001134 RID: 4404
		NonWhitespaceNonControlText = 16775161U,
		// Token: 0x04001135 RID: 4405
		HtmlNonWhitespaceText = 16774075U,
		// Token: 0x04001136 RID: 4406
		NonWhitespaceNonUri = 3917120U,
		// Token: 0x04001137 RID: 4407
		NonWhitespaceUri = 12858043U,
		// Token: 0x04001138 RID: 4408
		HtmlTagName = 16776443U,
		// Token: 0x04001139 RID: 4409
		HtmlTagNamePrefix = 12582139U,
		// Token: 0x0400113A RID: 4410
		HtmlAttrName = 16776315U,
		// Token: 0x0400113B RID: 4411
		HtmlAttrNamePrefix = 12582011U,
		// Token: 0x0400113C RID: 4412
		HtmlAttrValue = 16718587U,
		// Token: 0x0400113D RID: 4413
		HtmlScanQuoteSensitive = 132U,
		// Token: 0x0400113E RID: 4414
		HtmlEntity = 24U,
		// Token: 0x0400113F RID: 4415
		HtmlSimpleTagName = 12524731U,
		// Token: 0x04001140 RID: 4416
		HtmlEndTagName = 772U,
		// Token: 0x04001141 RID: 4417
		HtmlSimpleAttrName = 12524667U,
		// Token: 0x04001142 RID: 4418
		HtmlEndAttrName = 900U,
		// Token: 0x04001143 RID: 4419
		HtmlSimpleAttrQuotedValue = 16718847U,
		// Token: 0x04001144 RID: 4420
		HtmlSimpleAttrUnquotedValue = 16718587U,
		// Token: 0x04001145 RID: 4421
		HtmlEndAttrUnquotedValue = 260U,
		// Token: 0x04001146 RID: 4422
		Hex = 2147483664U
	}
}
