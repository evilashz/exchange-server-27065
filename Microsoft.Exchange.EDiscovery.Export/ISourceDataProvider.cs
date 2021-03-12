using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000010 RID: 16
	internal interface ISourceDataProvider
	{
		// Token: 0x06000058 RID: 88
		string GetRootFolder(string mailboxEmailAddress, bool isArchive);

		// Token: 0x06000059 RID: 89
		List<ItemId> SearchMailboxes(string mailboxEmailAddress, string query, string language, IEnumerable<string> mailboxIds, ref string pageItemReference, out List<ErrorRecord> failedMailboxes, bool isArchive, string searchName = null);

		// Token: 0x0600005A RID: 90
		List<ItemId> SearchMailboxes(string mailboxEmailAddress, string query, string language, IEnumerable<string> mailboxIds, bool isUnsearchable, ref string pageItemReference, out List<ErrorRecord> failedMailboxes, out bool newSchemaSearchSucceeded, bool isArchive, string searchConfiguration = null);

		// Token: 0x0600005B RID: 91
		List<ItemInformation> ExportItems(string mailboxEmailAddress, IList<ItemId> messageIds, bool isDocIdHintFlighted = false);

		// Token: 0x0600005C RID: 92
		void GetAllFolders(string mailboxEmailAddress, string parentFolderId, bool isDeepTraversal, bool isArchive, Dictionary<string, string> resultFolderInformations);

		// Token: 0x0600005D RID: 93
		List<UnsearchableItemId> GetUnsearchableItems(string mailboxEmailAddress, string mailboxId, ref string pageItemReference);

		// Token: 0x0600005E RID: 94
		void FillInUnsearchableItemIds(string mailboxEmailAddress, IList<UnsearchableItemId> itemIds);

		// Token: 0x0600005F RID: 95
		List<SourceInformation.SourceConfiguration> RetrieveSearchConfiguration(string searchName, out string language, string mailboxEmailAddress = null);

		// Token: 0x06000060 RID: 96
		void UpdateDGExpansion(string mailboxEmailAddress, IList<ItemId> itemIds);

		// Token: 0x06000061 RID: 97
		string GetPhysicalPartitionIdentifier(ItemId itemId);

		// Token: 0x06000062 RID: 98
		object GetItemHashObject(string itemHash);
	}
}
