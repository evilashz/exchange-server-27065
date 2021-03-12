using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200003D RID: 61
	internal class MailboxUpdates
	{
		// Token: 0x06000324 RID: 804 RVA: 0x00014D8F File Offset: 0x00012F8F
		public MailboxUpdates()
		{
			this.folderData = new EntryIdMap<FolderUpdates>();
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00014DA2 File Offset: 0x00012FA2
		// (set) Token: 0x06000326 RID: 806 RVA: 0x00014DAA File Offset: 0x00012FAA
		public EntryIdMap<FolderUpdates> FolderData
		{
			get
			{
				return this.folderData;
			}
			set
			{
				this.folderData = value;
			}
		}

		// Token: 0x170000CC RID: 204
		public FolderUpdates this[byte[] folderId]
		{
			get
			{
				FolderUpdates folderUpdates;
				if (!this.FolderData.TryGetValue(folderId, out folderUpdates))
				{
					folderUpdates = new FolderUpdates(folderId);
					this.folderData.Add(folderId, folderUpdates);
				}
				return folderUpdates;
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00014DE8 File Offset: 0x00012FE8
		public void AddMessage(byte[] folderId, byte[] messageId, MessageUpdateType updateType)
		{
			FolderUpdates folderUpdates = this[folderId];
			folderUpdates.GetListForUpdateType(updateType, true).Add(messageId);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00014E0C File Offset: 0x0001300C
		public void AddReadUnread(byte[] folderId, List<byte[]> readMessages, List<byte[]> unreadMessages)
		{
			FolderUpdates folderUpdates = this[folderId];
			folderUpdates.ReadMessages = readMessages;
			folderUpdates.UnreadMessages = unreadMessages;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00014E30 File Offset: 0x00013030
		public int GetUpdateCount(MessageUpdateType updateType)
		{
			int num = 0;
			foreach (FolderUpdates folderUpdates in this.folderData.Values)
			{
				num += folderUpdates.GetUpdateCount(updateType);
			}
			return num;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00014E90 File Offset: 0x00013090
		public bool IsEmpty()
		{
			List<byte[]> list = new List<byte[]>();
			foreach (FolderUpdates folderUpdates in this.folderData.Values)
			{
				if (folderUpdates.IsEmpty())
				{
					list.Add(folderUpdates.FolderId);
				}
			}
			foreach (byte[] key in list)
			{
				this.folderData.Remove(key);
			}
			return this.folderData.Count == 0;
		}

		// Token: 0x04000141 RID: 321
		private EntryIdMap<FolderUpdates> folderData;
	}
}
