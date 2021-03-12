using System;

namespace Microsoft.Exchange.Servicelets.JobQueue.PublicFolder
{
	// Token: 0x02000018 RID: 24
	internal enum SyncActivity
	{
		// Token: 0x04000090 RID: 144
		ClearFolderProperties,
		// Token: 0x04000091 RID: 145
		CommitBatch,
		// Token: 0x04000092 RID: 146
		CreateFolder,
		// Token: 0x04000093 RID: 147
		DeleteFolder,
		// Token: 0x04000094 RID: 148
		EnumerateHierarchyChanges,
		// Token: 0x04000095 RID: 149
		FixOrphanFolders,
		// Token: 0x04000096 RID: 150
		FxCopyProperties,
		// Token: 0x04000097 RID: 151
		GetChangeManifestInitializeSyncContext,
		// Token: 0x04000098 RID: 152
		GetChangeManifestPersistSyncContext,
		// Token: 0x04000099 RID: 153
		GetDestinationFolderIdSet,
		// Token: 0x0400009A RID: 154
		GetDestinationMailboxFolder,
		// Token: 0x0400009B RID: 155
		GetDestinationSessionSpecificEntryId,
		// Token: 0x0400009C RID: 156
		GetFolderRec,
		// Token: 0x0400009D RID: 157
		GetSourceFolderIdSet,
		// Token: 0x0400009E RID: 158
		GetSourceMailboxFolder,
		// Token: 0x0400009F RID: 159
		GetSourceSessionSpecificEntryId,
		// Token: 0x040000A0 RID: 160
		MapSourceToDestinationFolderId,
		// Token: 0x040000A1 RID: 161
		MoveFolder,
		// Token: 0x040000A2 RID: 162
		ProcessNextBatch,
		// Token: 0x040000A3 RID: 163
		SetIcsState,
		// Token: 0x040000A4 RID: 164
		SetSecurityDescriptor,
		// Token: 0x040000A5 RID: 165
		UpdateDumpsterId,
		// Token: 0x040000A6 RID: 166
		UpdateFoldersInBatch
	}
}
