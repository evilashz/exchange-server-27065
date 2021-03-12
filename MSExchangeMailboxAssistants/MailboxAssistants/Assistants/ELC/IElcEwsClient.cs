using System;
using System.Collections.Generic;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200008F RID: 143
	internal interface IElcEwsClient
	{
		// Token: 0x06000569 RID: 1385
		byte[] GetMrmConfiguration(BaseFolderIdType folderId);

		// Token: 0x0600056A RID: 1386
		void CreateMrmConfiguration(BaseFolderIdType folderId, byte[] userConfiguration);

		// Token: 0x0600056B RID: 1387
		void UpdateMrmConfiguration(BaseFolderIdType folderId, byte[] userConfiguration);

		// Token: 0x0600056C RID: 1388
		void DeleteMrmConfiguration(BaseFolderIdType folderId);

		// Token: 0x0600056D RID: 1389
		BaseFolderType GetFolderById(BaseFolderIdType folderId, BasePathToElementType[] additionalProperties);

		// Token: 0x0600056E RID: 1390
		IEnumerable<BaseFolderType> GetFolderHierarchy(BaseFolderIdType parentFolderId, bool isDeepTraversal, BasePathToElementType[] additionalProperties);

		// Token: 0x0600056F RID: 1391
		BaseFolderType CreateFolder(BaseFolderIdType parentFolderId, BaseFolderType newFolder);

		// Token: 0x06000570 RID: 1392
		BaseFolderType GetFolderByName(BaseFolderIdType parentFolderId, string folderDisplayName, BasePathToElementType[] additionalProperties);

		// Token: 0x06000571 RID: 1393
		BaseFolderType UpdateFolder(FolderChangeType folderChange);

		// Token: 0x06000572 RID: 1394
		List<ElcEwsItem> ExportItems(IList<ElcEwsItem> items);

		// Token: 0x06000573 RID: 1395
		List<ElcEwsItem> UploadItems(FolderIdType parentFolderId, IList<ElcEwsItem> items, bool alwaysCreateNew);

		// Token: 0x06000574 RID: 1396
		List<ElcEwsItem> GetItems(IList<ElcEwsItem> items);
	}
}
