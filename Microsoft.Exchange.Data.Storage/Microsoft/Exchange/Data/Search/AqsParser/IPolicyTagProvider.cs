using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search.AqsParser
{
	// Token: 0x02000D00 RID: 3328
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPolicyTagProvider
	{
		// Token: 0x17001E8E RID: 7822
		// (get) Token: 0x06007292 RID: 29330
		PolicyTag[] PolicyTags { get; }
	}
}
