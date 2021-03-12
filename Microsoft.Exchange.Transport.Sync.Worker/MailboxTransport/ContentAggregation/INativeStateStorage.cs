using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000203 RID: 515
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface INativeStateStorage : ISimpleStateStorage, IDisposeTrackable, IDisposable
	{
		// Token: 0x06001151 RID: 4433
		bool ContainsItem(StoreObjectId nativeId);

		// Token: 0x06001152 RID: 4434
		IEnumerator<StoreObjectId> GetNativeItemEnumerator();

		// Token: 0x06001153 RID: 4435
		bool TryAddItem(string cloudId, string cloudFolderId, StoreObjectId nativeId, byte[] changeKey, StoreObjectId nativeFolderId, string cloudVersion, Dictionary<string, string> itemProperties);

		// Token: 0x06001154 RID: 4436
		bool TryFindItem(string cloudId, out string cloudFolderId, out StoreObjectId nativeId, out byte[] changeKey, out StoreObjectId nativeFolderId, out string cloudVersion, out Dictionary<string, string> itemProperties);

		// Token: 0x06001155 RID: 4437
		bool TryFindItem(StoreObjectId nativeId, out string cloudId, out string cloudFolderId, out byte[] changeKey, out StoreObjectId nativeFolderId, out string cloudVersion, out Dictionary<string, string> itemProperties);

		// Token: 0x06001156 RID: 4438
		bool TryUpdateItem(StoreObjectId nativeId, byte[] changeKey, string cloudVersion, Dictionary<string, string> itemProperties);

		// Token: 0x06001157 RID: 4439
		bool TryRemoveItem(string cloudId);

		// Token: 0x06001158 RID: 4440
		bool TryRemoveItem(StoreObjectId nativeId);

		// Token: 0x06001159 RID: 4441
		bool ContainsFolder(StoreObjectId nativeId);

		// Token: 0x0600115A RID: 4442
		IEnumerator<StoreObjectId> GetNativeFolderEnumerator();

		// Token: 0x0600115B RID: 4443
		bool TryAddFolder(bool isInbox, string cloudId, string cloudFolderId, StoreObjectId nativeId, byte[] changeKey, StoreObjectId nativeFolderId, string cloudVersion, Dictionary<string, string> folderProperties);

		// Token: 0x0600115C RID: 4444
		bool TryFindFolder(StoreObjectId nativeId, out string cloudId, out string cloudFolderId, out byte[] changeKey, out StoreObjectId nativeFolderId, out string cloudVersion, out Dictionary<string, string> folderProperties);

		// Token: 0x0600115D RID: 4445
		bool TryFindFolder(string cloudId, out string cloudFolderId, out StoreObjectId nativeId, out byte[] changeKey, out StoreObjectId nativeFolderId, out string cloudVersion, out Dictionary<string, string> folderProperties);

		// Token: 0x0600115E RID: 4446
		bool TryUpdateFolder(bool isInbox, StoreObjectId nativeId, StoreObjectId newNativeId, string cloudId, string newCloudId, string newCloudFolderId, byte[] changeKey, StoreObjectId newNativeFolderId, string cloudVersion, Dictionary<string, string> folderProperties);

		// Token: 0x0600115F RID: 4447
		bool TryUpdateFolder(bool isInbox, StoreObjectId nativeId, StoreObjectId newNativeId);

		// Token: 0x06001160 RID: 4448
		bool TryRemoveFolder(string cloudId);

		// Token: 0x06001161 RID: 4449
		bool TryRemoveFolder(StoreObjectId nativeId);

		// Token: 0x06001162 RID: 4450
		IEnumerator<StoreObjectId> GetNativeItemFilteredByNativeFolderIdEnumerator(StoreObjectId nativeFolderId);

		// Token: 0x06001163 RID: 4451
		IEnumerator<StoreObjectId> GetNativeFolderFilteredByNativeFolderIdEnumerator(StoreObjectId nativeFolderId);
	}
}
