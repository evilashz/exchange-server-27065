using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.SharingPolicyAssistant
{
	// Token: 0x0200015C RID: 348
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FolderData : IDisposable
	{
		// Token: 0x06000E27 RID: 3623 RVA: 0x0005565E File Offset: 0x0005385E
		internal FolderData(MailboxSession mailboxSession, StoreId folderId)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			this.mailboxSession = mailboxSession;
			this.folderId = folderId;
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00055690 File Offset: 0x00053890
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x00055698 File Offset: 0x00053898
		internal StoreId Id
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x000556A0 File Offset: 0x000538A0
		internal Folder Folder
		{
			get
			{
				if (this.folder == null)
				{
					this.folder = Folder.Bind(this.mailboxSession, this.folderId);
				}
				return this.folder;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x000556C7 File Offset: 0x000538C7
		// (set) Token: 0x06000E2C RID: 3628 RVA: 0x000556CF File Offset: 0x000538CF
		internal bool IsChanged { get; set; }

		// Token: 0x06000E2D RID: 3629 RVA: 0x000556D8 File Offset: 0x000538D8
		public void Dispose()
		{
			if (this.folder != null)
			{
				this.folder.Dispose();
				this.folder = null;
			}
		}

		// Token: 0x04000923 RID: 2339
		private readonly MailboxSession mailboxSession;

		// Token: 0x04000924 RID: 2340
		private readonly StoreId folderId;

		// Token: 0x04000925 RID: 2341
		private Folder folder;
	}
}
