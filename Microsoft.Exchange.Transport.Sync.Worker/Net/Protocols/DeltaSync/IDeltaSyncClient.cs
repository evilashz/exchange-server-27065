using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x0200005D RID: 93
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IDeltaSyncClient : IDisposable
	{
		// Token: 0x06000471 RID: 1137
		IAsyncResult BeginGetChanges(int windowSize, AsyncCallback callback, object asyncState, object syncPoisonContext);

		// Token: 0x06000472 RID: 1138
		IAsyncResult BeginApplyChanges(List<DeltaSyncOperation> deltaSyncOperations, ConflictResolution conflictResolution, AsyncCallback callback, object asyncState, object syncPoisonContext);

		// Token: 0x06000473 RID: 1139
		IAsyncResult BeginSendMessage(DeltaSyncMail deltaSyncEmail, bool saveInSentItems, DeltaSyncRecipients deltaSyncRecipients, AsyncCallback callback, object asyncState, object syncPoisonContext);

		// Token: 0x06000474 RID: 1140
		IAsyncResult BeginFetchMessage(Guid serverId, AsyncCallback callback, object asyncState, object syncPoisonContext);

		// Token: 0x06000475 RID: 1141
		IAsyncResult BeginVerifyAccount(AsyncCallback callback, object asyncState, object syncPoisonContext);

		// Token: 0x06000476 RID: 1142
		IAsyncResult BeginGetSettings(AsyncCallback callback, object asyncState, object syncPoisonContext);

		// Token: 0x06000477 RID: 1143
		IAsyncResult BeginGetStatistics(AsyncCallback callback, object asyncState, object syncPoisonContext);

		// Token: 0x06000478 RID: 1144
		AsyncOperationResult<DeltaSyncResultData> EndVerifyAccount(IAsyncResult asyncResult);

		// Token: 0x06000479 RID: 1145
		AsyncOperationResult<DeltaSyncResultData> EndGetChanges(IAsyncResult asyncResult);

		// Token: 0x0600047A RID: 1146
		AsyncOperationResult<DeltaSyncResultData> EndApplyChanges(IAsyncResult asyncResult);

		// Token: 0x0600047B RID: 1147
		AsyncOperationResult<DeltaSyncResultData> EndSendMessage(IAsyncResult asyncResult);

		// Token: 0x0600047C RID: 1148
		AsyncOperationResult<DeltaSyncResultData> EndFetchMessage(IAsyncResult asyncResult);

		// Token: 0x0600047D RID: 1149
		AsyncOperationResult<DeltaSyncResultData> EndGetSettings(IAsyncResult asyncResult);

		// Token: 0x0600047E RID: 1150
		AsyncOperationResult<DeltaSyncResultData> EndGetStatistics(IAsyncResult asyncResult);

		// Token: 0x0600047F RID: 1151
		void SubscribeDownloadCompletedEvent(EventHandler<DownloadCompleteEventArgs> eventHandler);

		// Token: 0x06000480 RID: 1152
		void NotifyRoundtripComplete(object sender, RoundtripCompleteEventArgs roundtripCompleteEventArgs);
	}
}
