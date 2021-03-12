using System;
using System.Collections.Generic;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000040 RID: 64
	internal interface IEwsClient : ISourceDataProvider
	{
		// Token: 0x06000283 RID: 643
		void GetSearchResultEstimation(string mailboxEmailAddress, string query, string language, IEnumerable<string> mailboxIds, out int mailboxesSearchedCount, out long itemCount, out long totalSize, out List<ErrorRecord> failedMailboxes, string searchConfiguration = null);

		// Token: 0x06000284 RID: 644
		void GetSearchResultEstimation(string mailboxEmailAddress, string query, string language, IEnumerable<string> mailboxIds, out int mailboxesSearchedCount, bool isUnsearchable, out long itemCount, out long totalSize, out List<ErrorRecord> failedMailboxes, out bool newSchemaSearchSucceeded, string searchConfiguration = null);

		// Token: 0x06000285 RID: 645
		List<KeywordStatisticsSearchResultType> GetKeywordStatistics(string mailboxEmailAddress, string query, string language, IEnumerable<string> mailboxIds, out List<ErrorRecord> failedMailboxes, string searchConfiguration = null);

		// Token: 0x06000286 RID: 646
		BaseFolderType GetFolderById(string mailboxEmailAddress, BaseFolderIdType folderId);

		// Token: 0x06000287 RID: 647
		BaseFolderType GetFolderByName(string mailboxEmailAddress, BaseFolderIdType parentFolderId, string folderDisplayName);

		// Token: 0x06000288 RID: 648
		List<BaseFolderType> CreateFolder(string mailboxEmailAddress, BaseFolderIdType parentFolderId, BaseFolderType[] folders);

		// Token: 0x06000289 RID: 649
		void DeleteFolder(string mailboxEmailAddress, BaseFolderIdType[] folderIds);

		// Token: 0x0600028A RID: 650
		void MoveFolder(string mailboxEmailAddress, BaseFolderIdType targetFolderId, BaseFolderIdType[] folderIds);

		// Token: 0x0600028B RID: 651
		List<ItemType> RetrieveItems(string mailboxEmailAddress, BaseFolderIdType parentFolderId, BasePathToElementType[] additionalProperties, RestrictionType restriction, bool isAssociated, int? pageItemCount, int offset);

		// Token: 0x0600028C RID: 652
		List<ItemType> CreateItems(string mailboxEmailAddress, BaseFolderIdType parentFolderId, ItemType[] items);

		// Token: 0x0600028D RID: 653
		List<ItemType> UpdateItems(string mailboxEmailAddress, BaseFolderIdType parentFolderId, ItemChangeType[] itemChanges);

		// Token: 0x0600028E RID: 654
		void DeleteItems(string mailboxEmailAddress, BaseItemIdType[] itemIds);

		// Token: 0x0600028F RID: 655
		List<ItemInformation> UploadItems(string mailboxEmailAddress, FolderIdType parentFolderId, IList<ItemInformation> items, bool alwaysCreateNew);

		// Token: 0x06000290 RID: 656
		long GetUnsearchableItemStatistics(string mailboxEmailAddress, string mailboxId);

		// Token: 0x06000291 RID: 657
		ItemType GetItem(string mailboxEmailAddress, string itemId);

		// Token: 0x06000292 RID: 658
		void SendEmails(string mailboxEmailAddress, MessageType[] emails);

		// Token: 0x06000293 RID: 659
		AttachmentType GetAttachment(string mailboxEmailAddress, string attachmentId);

		// Token: 0x06000294 RID: 660
		List<AttachmentType> CreateAttachments(string mailboxEmailAddress, string itemId, IList<AttachmentType> attachments);

		// Token: 0x06000295 RID: 661
		void DeleteAttachments(string mailboxEmailAddress, IList<string> attachmentIds);
	}
}
