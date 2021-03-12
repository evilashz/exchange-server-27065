using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000087 RID: 135
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IArchiveProcessor
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004FB RID: 1275
		int MaxMessageSizeInArchive { get; }

		// Token: 0x060004FC RID: 1276
		bool SaveConfigItemInArchive(byte[] xmlData);

		// Token: 0x060004FD RID: 1277
		void DeleteConfigItemInArchive();

		// Token: 0x060004FE RID: 1278
		void MoveToArchive(TagExpirationExecutor.ItemSet itemSet, ElcSubAssistant assistant, FolderArchiver folderArchiver, int totalFailuresSoFar, ref List<Exception> allExceptionsSoFar, out List<string> foldersWithErrors, out int newMoveErrorsTotal);

		// Token: 0x060004FF RID: 1279
		void MoveToArchiveDumpster(DefaultFolderType folderType, List<ItemData> itemsToMove, ElcSubAssistant assistant, FolderArchiver folderArchiver, int totalFailuresSoFar, ref List<Exception> allExceptionsSoFar, out List<string> foldersWithErrors, out int newMoveErrorsTotal);

		// Token: 0x06000500 RID: 1280
		Dictionary<StoreObjectId, FolderTuple> GetFolderHierarchyInArchive();

		// Token: 0x06000501 RID: 1281
		void UpdatePropertiesOnFolderInArchive(FolderTuple source, FolderTuple target);

		// Token: 0x06000502 RID: 1282
		FolderTuple CreateAndUpdateFolderInArchive(FolderTuple parent, FolderTuple sourceInPrimary);
	}
}
