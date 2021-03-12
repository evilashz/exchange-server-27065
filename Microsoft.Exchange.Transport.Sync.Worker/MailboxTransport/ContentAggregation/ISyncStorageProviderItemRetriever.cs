using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000054 RID: 84
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISyncStorageProviderItemRetriever
	{
		// Token: 0x060003AF RID: 943
		IAsyncResult BeginGetItem(object itemRetrieverState, SyncChangeEntry item, AsyncCallback callback, object callbackState, object syncPoisonContext);

		// Token: 0x060003B0 RID: 944
		AsyncOperationResult<SyncChangeEntry> EndGetItem(IAsyncResult asyncResult);

		// Token: 0x060003B1 RID: 945
		void CancelGetItem(IAsyncResult asyncResult);
	}
}
