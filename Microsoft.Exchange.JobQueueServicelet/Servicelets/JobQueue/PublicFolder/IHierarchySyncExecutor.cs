using System;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Servicelets.JobQueue.PublicFolder
{
	// Token: 0x02000004 RID: 4
	internal interface IHierarchySyncExecutor
	{
		// Token: 0x06000028 RID: 40
		void SyncSingleFolder(byte[] folderId);

		// Token: 0x06000029 RID: 41
		bool ProcessNextBatch();

		// Token: 0x0600002A RID: 42
		void EnsureDestinationFolderHasParentChain(FolderRec sourceFolderRec);
	}
}
