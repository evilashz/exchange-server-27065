using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200001E RID: 30
	internal class FolderUpdates
	{
		// Token: 0x0600012C RID: 300 RVA: 0x000081CF File Offset: 0x000063CF
		public FolderUpdates()
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000081D7 File Offset: 0x000063D7
		internal FolderUpdates(byte[] folderId)
		{
			this.folderId = folderId;
			this.deletedMessages = null;
			this.readMessages = null;
			this.unreadMessages = null;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000081FB File Offset: 0x000063FB
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00008203 File Offset: 0x00006403
		public byte[] FolderId
		{
			get
			{
				return this.folderId;
			}
			set
			{
				this.folderId = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000820C File Offset: 0x0000640C
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00008214 File Offset: 0x00006414
		public List<byte[]> DeletedMessages
		{
			get
			{
				return this.deletedMessages;
			}
			set
			{
				this.deletedMessages = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000821D File Offset: 0x0000641D
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00008225 File Offset: 0x00006425
		public List<byte[]> ReadMessages
		{
			get
			{
				return this.readMessages;
			}
			set
			{
				this.readMessages = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000822E File Offset: 0x0000642E
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00008236 File Offset: 0x00006436
		public List<byte[]> UnreadMessages
		{
			get
			{
				return this.unreadMessages;
			}
			set
			{
				this.unreadMessages = value;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00008240 File Offset: 0x00006440
		public bool IsEmpty()
		{
			return (this.deletedMessages == null || this.deletedMessages.Count == 0) && (this.readMessages == null || this.readMessages.Count == 0) && (this.unreadMessages == null || this.unreadMessages.Count == 0);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00008294 File Offset: 0x00006494
		public List<byte[]> GetListForUpdateType(MessageUpdateType updateType, bool createIfNeeded)
		{
			List<byte[]> result = null;
			switch (updateType)
			{
			case MessageUpdateType.Delete:
				result = this.GetOrCreateList(ref this.deletedMessages, createIfNeeded);
				break;
			case MessageUpdateType.SetRead:
				result = this.GetOrCreateList(ref this.readMessages, createIfNeeded);
				break;
			case MessageUpdateType.SetUnread:
				result = this.GetOrCreateList(ref this.unreadMessages, createIfNeeded);
				break;
			}
			return result;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000082EC File Offset: 0x000064EC
		internal int GetUpdateCount(MessageUpdateType updateType)
		{
			List<byte[]> listForUpdateType = this.GetListForUpdateType(updateType, false);
			if (listForUpdateType == null)
			{
				return 0;
			}
			return listForUpdateType.Count;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000830D File Offset: 0x0000650D
		private List<byte[]> GetOrCreateList(ref List<byte[]> list, bool createIfNeeded)
		{
			if (list == null && createIfNeeded)
			{
				list = new List<byte[]>(1);
			}
			return list;
		}

		// Token: 0x04000080 RID: 128
		private byte[] folderId;

		// Token: 0x04000081 RID: 129
		private List<byte[]> deletedMessages;

		// Token: 0x04000082 RID: 130
		private List<byte[]> readMessages;

		// Token: 0x04000083 RID: 131
		private List<byte[]> unreadMessages;
	}
}
