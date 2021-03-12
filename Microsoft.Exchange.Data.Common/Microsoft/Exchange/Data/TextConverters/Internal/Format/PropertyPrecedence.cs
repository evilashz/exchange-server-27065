using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002B6 RID: 694
	internal enum PropertyPrecedence : byte
	{
		// Token: 0x04002114 RID: 8468
		InlineStyle,
		// Token: 0x04002115 RID: 8469
		StyleBase,
		// Token: 0x04002116 RID: 8470
		NonStyle = 9,
		// Token: 0x04002117 RID: 8471
		TagDefault,
		// Token: 0x04002118 RID: 8472
		Inherited
	}
}
