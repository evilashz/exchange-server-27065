using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000023 RID: 35
	internal interface ISummarizable
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600018F RID: 399
		string SummaryName { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000190 RID: 400
		IEnumerable<string> SummaryTokens { get; }
	}
}
