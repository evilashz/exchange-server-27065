using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000202 RID: 514
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISimpleStateStorage : IDisposeTrackable, IDisposable
	{
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001139 RID: 4409
		SyncStateStorage SyncStateStorage { get; }

		// Token: 0x0600113A RID: 4410
		void SetForceRecoverySyncNext(bool forceRecoverySyncNext);

		// Token: 0x0600113B RID: 4411
		void AddProperty(string property, string value);

		// Token: 0x0600113C RID: 4412
		bool TryGetPropertyValue(string property, out string value);

		// Token: 0x0600113D RID: 4413
		bool TryRemoveProperty(string property);

		// Token: 0x0600113E RID: 4414
		void ChangePropertyValue(string property, string value);

		// Token: 0x0600113F RID: 4415
		bool ContainsProperty(string property);

		// Token: 0x06001140 RID: 4416
		bool TryFindItem(string cloudId, out string cloudFolderId, out string cloudVersion, out Dictionary<string, string> itemProperties);

		// Token: 0x06001141 RID: 4417
		bool TryFindItem(string cloudId, out string cloudFolderId, out string cloudVersion);

		// Token: 0x06001142 RID: 4418
		bool TryFindFolder(string cloudId, out string cloudFolderId, out string cloudVersion);

		// Token: 0x06001143 RID: 4419
		bool ContainsItem(string cloudId);

		// Token: 0x06001144 RID: 4420
		bool ContainsFailedItem(string cloudId);

		// Token: 0x06001145 RID: 4421
		bool ContainsFolder(string cloudId);

		// Token: 0x06001146 RID: 4422
		bool ContainsFailedFolder(string cloudId);

		// Token: 0x06001147 RID: 4423
		IEnumerator<string> GetCloudItemEnumerator();

		// Token: 0x06001148 RID: 4424
		IEnumerator<string> GetCloudItemFilteredByCloudFolderIdEnumerator(string cloudFolderId);

		// Token: 0x06001149 RID: 4425
		IEnumerator<string> GetFailedCloudItemEnumerator();

		// Token: 0x0600114A RID: 4426
		IEnumerator<string> GetFailedCloudItemFilteredByCloudFolderIdEnumerator(string cloudFolderId);

		// Token: 0x0600114B RID: 4427
		bool TryUpdateItemCloudVersion(string cloudId, string cloudVersion);

		// Token: 0x0600114C RID: 4428
		IEnumerator<string> GetCloudFolderEnumerator();

		// Token: 0x0600114D RID: 4429
		IEnumerator<string> GetCloudFolderFilteredByCloudFolderIdEnumerator(string cloudFolderId);

		// Token: 0x0600114E RID: 4430
		bool TryFindFolder(string cloudId, out string cloudFolderId, out string cloudVersion, out Dictionary<string, string> folderProperties);

		// Token: 0x0600114F RID: 4431
		bool TryUpdateFolder(ISyncWorkerData subscription, string cloudId, string newCloudId, string cloudVersion);

		// Token: 0x06001150 RID: 4432
		bool TryUpdateFolderCloudVersion(string cloudId, string cloudVersion);
	}
}
