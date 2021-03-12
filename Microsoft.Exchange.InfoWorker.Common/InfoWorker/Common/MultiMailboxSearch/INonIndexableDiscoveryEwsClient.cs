using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001EB RID: 491
	internal interface INonIndexableDiscoveryEwsClient
	{
		// Token: 0x06000CDD RID: 3293
		IAsyncResult BeginGetNonIndexableItemStatistics(AsyncCallback callback, object state, GetNonIndexableItemStatisticsParameters parameters);

		// Token: 0x06000CDE RID: 3294
		GetNonIndexableItemStatisticsResponse EndGetNonIndexableItemStatistics(IAsyncResult result);

		// Token: 0x06000CDF RID: 3295
		IAsyncResult BeginGetNonIndexableItemDetails(AsyncCallback callback, object state, GetNonIndexableItemDetailsParameters parameters);

		// Token: 0x06000CE0 RID: 3296
		GetNonIndexableItemDetailsResponse EndGetNonIndexableItemDetails(IAsyncResult result);
	}
}
