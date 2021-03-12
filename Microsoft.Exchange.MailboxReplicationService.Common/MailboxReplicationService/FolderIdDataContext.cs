using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000F6 RID: 246
	internal class FolderIdDataContext : DataContext
	{
		// Token: 0x06000922 RID: 2338 RVA: 0x00012605 File Offset: 0x00010805
		public FolderIdDataContext(byte[] folderId)
		{
			this.folderId = folderId;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00012614 File Offset: 0x00010814
		public override string ToString()
		{
			return string.Format("Folder: entryId {0}", TraceUtils.DumpEntryId(this.folderId));
		}

		// Token: 0x0400055B RID: 1371
		private byte[] folderId;
	}
}
