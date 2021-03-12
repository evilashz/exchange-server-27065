using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000225 RID: 549
	internal interface IPageInformation
	{
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001338 RID: 4920
		bool? MorePagesAvailable { get; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001339 RID: 4921
		int PageSize { get; }
	}
}
