using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200003E RID: 62
	internal class SyncContext
	{
		// Token: 0x0600032C RID: 812 RVA: 0x00014F50 File Offset: 0x00013150
		public SyncContext(FolderMap sourceFolderMap, FolderMap targetFolderMap)
		{
			this.sourceFolderMap = sourceFolderMap;
			this.targetFolderMap = targetFolderMap;
			this.CopyMessagesCount = default(CopyMessagesCount);
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00014F80 File Offset: 0x00013180
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00014F88 File Offset: 0x00013188
		public int NumberOfHierarchyUpdates { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00014F91 File Offset: 0x00013191
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00014F99 File Offset: 0x00013199
		public CopyMessagesCount CopyMessagesCount { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00014FA2 File Offset: 0x000131A2
		public FolderMap SourceFolderMap
		{
			get
			{
				return this.sourceFolderMap;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00014FAA File Offset: 0x000131AA
		public FolderMap TargetFolderMap
		{
			get
			{
				return this.targetFolderMap;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00014FB2 File Offset: 0x000131B2
		public virtual byte[] GetSourceEntryIdFromTargetFolder(FolderRecWrapper targetFolder)
		{
			return targetFolder.EntryId;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00014FBA File Offset: 0x000131BA
		public virtual FolderRecWrapper GetTargetFolderBySourceId(byte[] sourceId)
		{
			return this.TargetFolderMap[sourceId];
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00014FC8 File Offset: 0x000131C8
		public virtual FolderRecWrapper GetTargetParentFolderBySourceParentId(byte[] sourceParentId)
		{
			return this.GetTargetFolderBySourceId(sourceParentId);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00014FD1 File Offset: 0x000131D1
		public virtual FolderRecWrapper CreateSourceFolderRec(FolderRec fRec)
		{
			return new FolderRecWrapper(fRec);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00014FD9 File Offset: 0x000131D9
		public virtual FolderRecWrapper CreateTargetFolderRec(FolderRecWrapper sourceFolderRec)
		{
			return new FolderRecWrapper(sourceFolderRec.FolderRec);
		}

		// Token: 0x04000142 RID: 322
		private readonly FolderMap sourceFolderMap;

		// Token: 0x04000143 RID: 323
		private readonly FolderMap targetFolderMap;
	}
}
