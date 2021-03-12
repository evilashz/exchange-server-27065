using System;
using System.Linq;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200002D RID: 45
	internal class MailboxChanges
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x0000A07B File Offset: 0x0000827B
		public MailboxChanges(MailboxChangesManifest hierarchyChanges)
		{
			this.hierarchyChanges = hierarchyChanges;
			this.folderChanges = new EntryIdMap<FolderChangesManifest>();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000A095 File Offset: 0x00008295
		public MailboxChanges(EntryIdMap<FolderChangesManifest> folderChanges)
		{
			this.hierarchyChanges = new MailboxChangesManifest();
			this.folderChanges = folderChanges;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000A0AF File Offset: 0x000082AF
		public MailboxChangesManifest HierarchyChanges
		{
			get
			{
				return this.hierarchyChanges;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000A0B7 File Offset: 0x000082B7
		public EntryIdMap<FolderChangesManifest> FolderChanges
		{
			get
			{
				return this.folderChanges;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000A0C7 File Offset: 0x000082C7
		public bool HasFolderRecoverySync
		{
			get
			{
				return this.FolderChanges.Values.Any((FolderChangesManifest fc) => fc.FolderRecoverySync);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A0F8 File Offset: 0x000082F8
		public void GetMessageCounts(out int newMessages, out int updated, out int deleted, out int read, out int unread)
		{
			newMessages = 0;
			updated = 0;
			deleted = 0;
			read = 0;
			unread = 0;
			foreach (FolderChangesManifest folderChangesManifest in this.FolderChanges.Values)
			{
				if (folderChangesManifest.ReadMessages != null)
				{
					read += folderChangesManifest.ReadMessages.Count;
				}
				if (folderChangesManifest.UnreadMessages != null)
				{
					unread += folderChangesManifest.UnreadMessages.Count;
				}
				int num;
				int num2;
				int num3;
				folderChangesManifest.GetMessageCounts(out num, out num2, out num3);
				newMessages += num;
				updated += num2;
				deleted += num3;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000A1AC File Offset: 0x000083AC
		public int MessageChanges
		{
			get
			{
				int num = 0;
				foreach (FolderChangesManifest folderChangesManifest in this.FolderChanges.Values)
				{
					if (folderChangesManifest.ChangedMessages != null)
					{
						num += folderChangesManifest.ChangedMessages.Count;
					}
					if (folderChangesManifest.ReadMessages != null)
					{
						num += folderChangesManifest.ReadMessages.Count;
					}
					if (folderChangesManifest.UnreadMessages != null)
					{
						num += folderChangesManifest.UnreadMessages.Count;
					}
				}
				return num;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000A244 File Offset: 0x00008444
		public int EntryCount
		{
			get
			{
				int num = this.MessageChanges;
				if (this.hierarchyChanges.ChangedFolders != null)
				{
					num += this.hierarchyChanges.ChangedFolders.Count;
				}
				if (this.hierarchyChanges.DeletedFolders != null)
				{
					num += this.hierarchyChanges.DeletedFolders.Count;
				}
				return num;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000A29C File Offset: 0x0000849C
		public bool HasChanges
		{
			get
			{
				foreach (FolderChangesManifest folderChangesManifest in this.folderChanges.Values)
				{
					if (folderChangesManifest.HasChanges)
					{
						return true;
					}
				}
				return (this.hierarchyChanges.ChangedFolders != null && this.hierarchyChanges.ChangedFolders.Count > 0) || (this.hierarchyChanges.DeletedFolders != null && this.hierarchyChanges.DeletedFolders.Count > 0);
			}
		}

		// Token: 0x17000083 RID: 131
		public FolderChangesManifest this[byte[] folderId]
		{
			get
			{
				FolderChangesManifest folderChangesManifest;
				if (!this.folderChanges.TryGetValue(folderId, out folderChangesManifest))
				{
					folderChangesManifest = new FolderChangesManifest(folderId);
					this.folderChanges.Add(folderId, folderChangesManifest);
				}
				return folderChangesManifest;
			}
			set
			{
				this.folderChanges[folderId] = value;
			}
		}

		// Token: 0x040000D0 RID: 208
		private MailboxChangesManifest hierarchyChanges;

		// Token: 0x040000D1 RID: 209
		private EntryIdMap<FolderChangesManifest> folderChanges;
	}
}
