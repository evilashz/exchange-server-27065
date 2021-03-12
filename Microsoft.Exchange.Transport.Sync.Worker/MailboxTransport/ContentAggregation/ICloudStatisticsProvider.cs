using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000056 RID: 86
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ICloudStatisticsProvider
	{
		// Token: 0x060003C0 RID: 960
		IAsyncResult BeginGetStatistics(SyncStorageProviderState state, AsyncCallback callback, object callbackState, object syncPoisonContext);

		// Token: 0x060003C1 RID: 961
		AsyncOperationResult<CloudStatistics> EndGetStatistics(IAsyncResult asyncResult);
	}
}
