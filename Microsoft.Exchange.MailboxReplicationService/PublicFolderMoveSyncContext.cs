using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000056 RID: 86
	internal class PublicFolderMoveSyncContext : SyncContext
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x0001A126 File Offset: 0x00018326
		public PublicFolderMoveSyncContext(ISourceMailbox sourceMailbox, FolderMap sourceFolderMap, IDestinationMailbox destinationMailbox, FolderMap targetFolderMap) : base(sourceFolderMap, targetFolderMap)
		{
			this.sourceMailbox = sourceMailbox;
			this.destinationMailbox = destinationMailbox;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001A13F File Offset: 0x0001833F
		public override byte[] GetSourceEntryIdFromTargetFolder(FolderRecWrapper targetFolder)
		{
			return this.sourceMailbox.GetSessionSpecificEntryId(targetFolder.EntryId);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001A152 File Offset: 0x00018352
		public override FolderRecWrapper GetTargetFolderBySourceId(byte[] sourceId)
		{
			return base.TargetFolderMap[this.destinationMailbox.GetSessionSpecificEntryId(sourceId)];
		}

		// Token: 0x040001E4 RID: 484
		private readonly ISourceMailbox sourceMailbox;

		// Token: 0x040001E5 RID: 485
		private readonly IDestinationMailbox destinationMailbox;
	}
}
