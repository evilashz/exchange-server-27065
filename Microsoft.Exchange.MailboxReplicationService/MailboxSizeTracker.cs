using System;
using System.Collections.Generic;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000038 RID: 56
	internal class MailboxSizeTracker
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x00013FFC File Offset: 0x000121FC
		public MailboxSizeTracker()
		{
			this.totals = new MailboxSizeTracker.FolderSizeRecord(null, null, 0, 0UL);
			this.folderData = new EntryIdMap<MailboxSizeTracker.FolderSizeRecord>();
			this.IsFinishedEstimating = false;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00014026 File Offset: 0x00012226
		public int MessageCount
		{
			get
			{
				return this.totals.MessageCount;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00014033 File Offset: 0x00012233
		public ulong TotalMessageSize
		{
			get
			{
				return this.totals.TotalMessageSize;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00014040 File Offset: 0x00012240
		public int DeletedMessageCount
		{
			get
			{
				return this.totals.DeletedMessageCount;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0001404D File Offset: 0x0001224D
		public ulong TotalDeletedMessageSize
		{
			get
			{
				return this.totals.TotalDeletedMessageSize;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0001405A File Offset: 0x0001225A
		public int AlreadyCopiedCount
		{
			get
			{
				return this.totals.AlreadyCopiedCount;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00014067 File Offset: 0x00012267
		public ulong AlreadyCopiedSize
		{
			get
			{
				return this.totals.AlreadyCopiedSize;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00014074 File Offset: 0x00012274
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0001407C File Offset: 0x0001227C
		public int TotalFolderCount { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00014085 File Offset: 0x00012285
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0001408D File Offset: 0x0001228D
		public int FoldersProcessed { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00014096 File Offset: 0x00012296
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0001409E File Offset: 0x0001229E
		public bool IsFinishedEstimating { get; set; }

		// Token: 0x060002F6 RID: 758 RVA: 0x000140A7 File Offset: 0x000122A7
		public void ResetFoldersProcessed(int totalFolders)
		{
			this.FoldersProcessed = 0;
			this.TotalFolderCount = totalFolders;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000140B7 File Offset: 0x000122B7
		public void IncrementFoldersProcessed()
		{
			this.FoldersProcessed++;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000140C7 File Offset: 0x000122C7
		public void TrackFolder(byte[] folderId, ICollection<MessageRec> folderMessages, int alreadyCopiedCount, ulong alreadyCopiedSize)
		{
			this.UpdateFolderData(new MailboxSizeTracker.FolderSizeRecord(folderId, folderMessages, alreadyCopiedCount, alreadyCopiedSize));
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000140D9 File Offset: 0x000122D9
		public void TrackFolder(byte[] folderId, int totalItemsCount, int alreadyCopiedCount, ulong alreadyCopiedSize)
		{
			this.UpdateFolderData(new MailboxSizeTracker.FolderSizeRecord(folderId, totalItemsCount, alreadyCopiedCount, alreadyCopiedSize));
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000140EB File Offset: 0x000122EB
		public void TrackFolder(FolderRec fRec)
		{
			this.UpdateFolderData(new MailboxSizeTracker.FolderSizeRecord(fRec));
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000140F9 File Offset: 0x000122F9
		public void TrackFolder(FolderStateSnapshot folderStateSnaphot)
		{
			this.UpdateFolderData(new MailboxSizeTracker.FolderSizeRecord(folderStateSnaphot.FolderId, folderStateSnaphot.TotalMessages, folderStateSnaphot.TotalMessageByteSize, folderStateSnaphot.MessagesWritten, folderStateSnaphot.MessageByteSizeWritten, folderStateSnaphot.SoftDeletedMessageCount, folderStateSnaphot.TotalSoftDeletedMessageSize));
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00014130 File Offset: 0x00012330
		public void GetFolderSize(byte[] folderId, out int itemCount, out ulong totalItemSize)
		{
			MailboxSizeTracker.FolderSizeRecord folderSizeRecord;
			if (this.folderData.TryGetValue(folderId, out folderSizeRecord))
			{
				itemCount = folderSizeRecord.MessageCount;
				totalItemSize = folderSizeRecord.TotalMessageSize;
				return;
			}
			itemCount = 0;
			totalItemSize = 0UL;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00014168 File Offset: 0x00012368
		private void UpdateFolderData(MailboxSizeTracker.FolderSizeRecord newRecord)
		{
			MailboxSizeTracker.FolderSizeRecord other;
			if (this.folderData.TryGetValue(newRecord.FolderId, out other))
			{
				this.totals.SubtractCounts(other);
			}
			this.totals.AddCounts(newRecord);
			this.folderData[newRecord.FolderId] = newRecord;
		}

		// Token: 0x0400012D RID: 301
		private MailboxSizeTracker.FolderSizeRecord totals;

		// Token: 0x0400012E RID: 302
		private EntryIdMap<MailboxSizeTracker.FolderSizeRecord> folderData;

		// Token: 0x02000039 RID: 57
		private class FolderSizeRecord
		{
			// Token: 0x060002FE RID: 766 RVA: 0x000141B4 File Offset: 0x000123B4
			public FolderSizeRecord(byte[] folderId, int messageCount, ulong totalMessageSize, int alreadyCopiedCount, ulong alreadyCopiedSize, int deletedMessageCount, ulong totalDeletedMessageSize)
			{
				this.FolderId = folderId;
				this.MessageCount = messageCount;
				this.TotalMessageSize = totalMessageSize;
				this.AlreadyCopiedCount = alreadyCopiedCount;
				this.AlreadyCopiedSize = alreadyCopiedSize;
				this.DeletedMessageCount = deletedMessageCount;
				this.TotalDeletedMessageSize = totalDeletedMessageSize;
			}

			// Token: 0x060002FF RID: 767 RVA: 0x000141F4 File Offset: 0x000123F4
			public FolderSizeRecord(byte[] folderId, ICollection<MessageRec> messages, int alreadyCopiedCount, ulong alreadyCopiedSize)
			{
				this.FolderId = folderId;
				this.MessageCount = alreadyCopiedCount;
				this.TotalMessageSize = alreadyCopiedSize;
				this.DeletedMessageCount = 0;
				this.TotalDeletedMessageSize = 0UL;
				this.AlreadyCopiedCount = alreadyCopiedCount;
				this.AlreadyCopiedSize = alreadyCopiedSize;
				if (messages != null)
				{
					this.TrackMessages(messages);
				}
			}

			// Token: 0x06000300 RID: 768 RVA: 0x00014248 File Offset: 0x00012448
			public FolderSizeRecord(byte[] folderId, int totalItemsCount, int alreadyCopiedCount, ulong alreadyCopiedSize)
			{
				this.FolderId = folderId;
				this.MessageCount = totalItemsCount;
				this.TotalMessageSize = (ulong)(totalItemsCount * 100 * 1024);
				this.DeletedMessageCount = 0;
				this.TotalDeletedMessageSize = 0UL;
				this.AlreadyCopiedCount = alreadyCopiedCount;
				this.AlreadyCopiedSize = alreadyCopiedSize;
			}

			// Token: 0x06000301 RID: 769 RVA: 0x00014298 File Offset: 0x00012498
			public FolderSizeRecord(FolderRec fRec)
			{
				this.FolderId = fRec.EntryId;
				this.MessageCount = 0;
				this.TotalMessageSize = 0UL;
				this.DeletedMessageCount = 0;
				this.TotalDeletedMessageSize = 0UL;
				object obj = fRec[PropTag.ContentCount];
				if (obj != null)
				{
					this.MessageCount += (int)obj;
				}
				object obj2 = fRec[PropTag.MessageSizeExtended];
				if (obj2 != null)
				{
					this.TotalMessageSize += (ulong)((long)obj2);
				}
				obj = fRec[PropTag.AssocContentCount];
				if (obj != null)
				{
					this.MessageCount += (int)obj;
				}
				obj2 = fRec[PropTag.AssocMessageSizeExtended];
				if (obj2 != null)
				{
					this.TotalMessageSize += (ulong)((long)obj2);
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000302 RID: 770 RVA: 0x0001435D File Offset: 0x0001255D
			// (set) Token: 0x06000303 RID: 771 RVA: 0x00014365 File Offset: 0x00012565
			public byte[] FolderId { get; private set; }

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000304 RID: 772 RVA: 0x0001436E File Offset: 0x0001256E
			// (set) Token: 0x06000305 RID: 773 RVA: 0x00014376 File Offset: 0x00012576
			public int MessageCount { get; private set; }

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000306 RID: 774 RVA: 0x0001437F File Offset: 0x0001257F
			// (set) Token: 0x06000307 RID: 775 RVA: 0x00014387 File Offset: 0x00012587
			public ulong TotalMessageSize { get; private set; }

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000308 RID: 776 RVA: 0x00014390 File Offset: 0x00012590
			// (set) Token: 0x06000309 RID: 777 RVA: 0x00014398 File Offset: 0x00012598
			public int DeletedMessageCount { get; private set; }

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x0600030A RID: 778 RVA: 0x000143A1 File Offset: 0x000125A1
			// (set) Token: 0x0600030B RID: 779 RVA: 0x000143A9 File Offset: 0x000125A9
			public ulong TotalDeletedMessageSize { get; private set; }

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x0600030C RID: 780 RVA: 0x000143B2 File Offset: 0x000125B2
			// (set) Token: 0x0600030D RID: 781 RVA: 0x000143BA File Offset: 0x000125BA
			public int AlreadyCopiedCount { get; private set; }

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x0600030E RID: 782 RVA: 0x000143C3 File Offset: 0x000125C3
			// (set) Token: 0x0600030F RID: 783 RVA: 0x000143CB File Offset: 0x000125CB
			public ulong AlreadyCopiedSize { get; private set; }

			// Token: 0x06000310 RID: 784 RVA: 0x000143D4 File Offset: 0x000125D4
			public void AddCounts(MailboxSizeTracker.FolderSizeRecord other)
			{
				this.MessageCount += other.MessageCount;
				this.TotalMessageSize += other.TotalMessageSize;
				this.DeletedMessageCount += other.DeletedMessageCount;
				this.TotalDeletedMessageSize += other.TotalDeletedMessageSize;
				this.AlreadyCopiedCount += other.AlreadyCopiedCount;
				this.AlreadyCopiedSize += other.AlreadyCopiedSize;
			}

			// Token: 0x06000311 RID: 785 RVA: 0x00014454 File Offset: 0x00012654
			public void SubtractCounts(MailboxSizeTracker.FolderSizeRecord other)
			{
				this.MessageCount -= other.MessageCount;
				this.TotalMessageSize -= other.TotalMessageSize;
				this.DeletedMessageCount -= other.DeletedMessageCount;
				this.TotalDeletedMessageSize -= other.TotalDeletedMessageSize;
				this.AlreadyCopiedCount -= other.AlreadyCopiedCount;
				this.AlreadyCopiedSize -= other.AlreadyCopiedSize;
			}

			// Token: 0x06000312 RID: 786 RVA: 0x000144D4 File Offset: 0x000126D4
			private void TrackMessage(MessageRec msgRec)
			{
				if (msgRec.IsDeleted)
				{
					this.DeletedMessageCount++;
					this.TotalDeletedMessageSize += (ulong)((long)msgRec.MessageSize);
					return;
				}
				this.MessageCount++;
				this.TotalMessageSize += (ulong)((long)msgRec.MessageSize);
			}

			// Token: 0x06000313 RID: 787 RVA: 0x00014530 File Offset: 0x00012730
			private void TrackMessages(ICollection<MessageRec> messages)
			{
				foreach (MessageRec msgRec in messages)
				{
					this.TrackMessage(msgRec);
				}
			}
		}
	}
}
