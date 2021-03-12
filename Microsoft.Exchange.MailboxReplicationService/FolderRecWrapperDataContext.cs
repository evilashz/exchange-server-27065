using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200001A RID: 26
	internal class FolderRecWrapperDataContext : DataContext
	{
		// Token: 0x06000106 RID: 262 RVA: 0x00007E3D File Offset: 0x0000603D
		public FolderRecWrapperDataContext(FolderRecWrapper folderRecWrapper)
		{
			this.folderRecWrapper = folderRecWrapper;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00007E4C File Offset: 0x0000604C
		public override string ToString()
		{
			switch (this.folderRecWrapper.FolderType)
			{
			case FolderType.Root:
				return string.Format("Root folder: entryId {0}", TraceUtils.DumpEntryId(this.folderRecWrapper.EntryId));
			case FolderType.Generic:
				return string.Format("Folder: '{0}', entryId {1}, parentId {2}", this.folderRecWrapper.FullFolderName, TraceUtils.DumpEntryId(this.folderRecWrapper.EntryId), TraceUtils.DumpEntryId(this.folderRecWrapper.ParentId));
			case FolderType.Search:
				return string.Format("Search folder: '{0}', entryId {1}, parentId {2}", this.folderRecWrapper.FullFolderName, TraceUtils.DumpEntryId(this.folderRecWrapper.EntryId), TraceUtils.DumpEntryId(this.folderRecWrapper.ParentId));
			default:
				return string.Format("Folder: entryId {0}", TraceUtils.DumpEntryId(this.folderRecWrapper.EntryId));
			}
		}

		// Token: 0x04000066 RID: 102
		private FolderRecWrapper folderRecWrapper;
	}
}
