using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000055 RID: 85
	internal class PublicFolderMigrationSyncContext : SyncContext
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x0001A052 File Offset: 0x00018252
		public PublicFolderMigrationSyncContext(ISourceMailbox sourceDatabase, FolderMap sourceFolderMap, IDestinationMailbox destinationMailbox, FolderMap targetFolderMap, bool isTargetPrimaryHierarchyMailbox) : base(sourceFolderMap, targetFolderMap)
		{
			this.sourceDatabase = sourceDatabase;
			this.destinationMailbox = destinationMailbox;
			this.isTargetPrimaryHierarchyMailbox = isTargetPrimaryHierarchyMailbox;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001A073 File Offset: 0x00018273
		public override byte[] GetSourceEntryIdFromTargetFolder(FolderRecWrapper targetFolder)
		{
			return this.sourceDatabase.GetSessionSpecificEntryId(targetFolder.EntryId);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001A088 File Offset: 0x00018288
		public override FolderRecWrapper GetTargetFolderBySourceId(byte[] sourceId)
		{
			FolderMap sourceFolderMap = base.SourceFolderMap;
			FolderMapping folderMapping = base.SourceFolderMap[sourceId] as FolderMapping;
			if (folderMapping != null && folderMapping.IsSystemPublicFolder)
			{
				FolderHierarchy folderHierarchy = base.TargetFolderMap as FolderHierarchy;
				return folderHierarchy.GetWellKnownFolder(folderMapping.WKFType);
			}
			return base.TargetFolderMap[this.destinationMailbox.GetSessionSpecificEntryId(sourceId)];
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001A0E9 File Offset: 0x000182E9
		public override FolderRecWrapper GetTargetParentFolderBySourceParentId(byte[] sourceParentId)
		{
			if (!this.isTargetPrimaryHierarchyMailbox)
			{
				return ((FolderHierarchy)base.TargetFolderMap).GetWellKnownFolder(WellKnownFolderType.IpmSubtree);
			}
			return this.GetTargetFolderBySourceId(sourceParentId);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001A10C File Offset: 0x0001830C
		public override FolderRecWrapper CreateSourceFolderRec(FolderRec fRec)
		{
			return new FolderMapping(fRec);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001A114 File Offset: 0x00018314
		public override FolderRecWrapper CreateTargetFolderRec(FolderRecWrapper sourceFolderRec)
		{
			return new FolderMapping(((FolderMapping)sourceFolderRec).FolderRec);
		}

		// Token: 0x040001E1 RID: 481
		private readonly ISourceMailbox sourceDatabase;

		// Token: 0x040001E2 RID: 482
		private readonly IDestinationMailbox destinationMailbox;

		// Token: 0x040001E3 RID: 483
		private readonly bool isTargetPrimaryHierarchyMailbox;
	}
}
