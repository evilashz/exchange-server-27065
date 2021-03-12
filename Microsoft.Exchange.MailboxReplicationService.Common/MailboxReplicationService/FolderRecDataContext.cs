using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000F5 RID: 245
	internal class FolderRecDataContext : DataContext
	{
		// Token: 0x06000920 RID: 2336 RVA: 0x00012522 File Offset: 0x00010722
		public FolderRecDataContext(FolderRec folderRec)
		{
			this.folderRec = folderRec;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00012534 File Offset: 0x00010734
		public override string ToString()
		{
			switch (this.folderRec.FolderType)
			{
			case FolderType.Root:
				return string.Format("Root folder: entryId {0}", TraceUtils.DumpEntryId(this.folderRec.EntryId));
			case FolderType.Generic:
				return string.Format("Folder: '{0}', entryId {1}, parentId {2}", this.folderRec.FolderName, TraceUtils.DumpEntryId(this.folderRec.EntryId), TraceUtils.DumpEntryId(this.folderRec.ParentId));
			case FolderType.Search:
				return string.Format("Search folder: '{0}', entryId {1}, parentId {2}", this.folderRec.FolderName, TraceUtils.DumpEntryId(this.folderRec.EntryId), TraceUtils.DumpEntryId(this.folderRec.ParentId));
			default:
				return string.Format("Folder: entryId {0}", TraceUtils.DumpEntryId(this.folderRec.EntryId));
			}
		}

		// Token: 0x0400055A RID: 1370
		private FolderRec folderRec;
	}
}
