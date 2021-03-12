using System;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000027 RID: 39
	internal enum ParseResult
	{
		// Token: 0x04000141 RID: 321
		notYetParsed,
		// Token: 0x04000142 RID: 322
		success,
		// Token: 0x04000143 RID: 323
		invalidArgument,
		// Token: 0x04000144 RID: 324
		invalidNumberOfArguments,
		// Token: 0x04000145 RID: 325
		invalidMessageSet,
		// Token: 0x04000146 RID: 326
		invalidCharset
	}
}
