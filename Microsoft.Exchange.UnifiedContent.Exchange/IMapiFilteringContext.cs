using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.UnifiedContent.Exchange
{
	// Token: 0x02000009 RID: 9
	internal interface IMapiFilteringContext
	{
		// Token: 0x0600000C RID: 12
		bool NeedsClassificationScan(Attachment attachment);

		// Token: 0x0600000D RID: 13
		bool NeedsClassificationScan();
	}
}
