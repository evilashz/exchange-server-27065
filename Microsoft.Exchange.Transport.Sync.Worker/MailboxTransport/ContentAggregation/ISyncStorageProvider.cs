using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000055 RID: 85
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISyncStorageProvider : ISyncStorageProviderItemRetriever
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003B2 RID: 946
		AggregationSubscriptionType SubscriptionType { get; }

		// Token: 0x060003B3 RID: 947
		SyncStorageProviderState Bind(ISyncWorkerData subscription, SyncLogSession syncLogSession, bool underRecovery);

		// Token: 0x060003B4 RID: 948
		void Unbind(SyncStorageProviderState state);

		// Token: 0x060003B5 RID: 949
		IAsyncResult BeginAuthenticate(SyncStorageProviderState state, AsyncCallback callback, object callbackState, object syncPoisonContext);

		// Token: 0x060003B6 RID: 950
		AsyncOperationResult<SyncProviderResultData> EndAuthenticate(IAsyncResult asyncResult);

		// Token: 0x060003B7 RID: 951
		IAsyncResult BeginCheckForChanges(SyncStorageProviderState state, AsyncCallback callback, object callbackState, object syncPoisonContext);

		// Token: 0x060003B8 RID: 952
		AsyncOperationResult<SyncProviderResultData> EndCheckForChanges(IAsyncResult asyncResult);

		// Token: 0x060003B9 RID: 953
		IAsyncResult BeginEnumerateChanges(SyncStorageProviderState state, AsyncCallback callback, object callbackState, object syncPoisonContext);

		// Token: 0x060003BA RID: 954
		AsyncOperationResult<SyncProviderResultData> EndEnumerateChanges(IAsyncResult asyncResult);

		// Token: 0x060003BB RID: 955
		IAsyncResult BeginAcknowledgeChanges(SyncStorageProviderState state, IList<SyncChangeEntry> changeList, bool hasPermanentSyncErrors, bool hasTransientSyncErrors, AsyncCallback callback, object callbackState, object syncPoisonContext);

		// Token: 0x060003BC RID: 956
		AsyncOperationResult<SyncProviderResultData> EndAcknowledgeChanges(IAsyncResult asyncResult);

		// Token: 0x060003BD RID: 957
		IAsyncResult BeginApplyChanges(SyncStorageProviderState state, IList<SyncChangeEntry> changeList, ISyncStorageProviderItemRetriever itemRetriever, object itemRetrieverState, AsyncCallback callback, object callbackState, object syncPoisonContext);

		// Token: 0x060003BE RID: 958
		AsyncOperationResult<SyncProviderResultData> EndApplyChanges(IAsyncResult asyncResult);

		// Token: 0x060003BF RID: 959
		void Cancel(IAsyncResult asyncResult);
	}
}
