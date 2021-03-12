using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000040 RID: 64
	internal class MessageRecComparer : IComparer<MessageRec>
	{
		// Token: 0x06000348 RID: 840 RVA: 0x00015338 File Offset: 0x00013538
		private MessageRecComparer(MessageRecSortBy sortBy)
		{
			this.sortBy = sortBy;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00015347 File Offset: 0x00013547
		public static IComparer<MessageRec> Comparer
		{
			get
			{
				return MessageRecComparer.normalInstance;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0001534E File Offset: 0x0001354E
		public static IComparer<MessageRec> DescendingComparer
		{
			get
			{
				return MessageRecComparer.descendingInstance;
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00015355 File Offset: 0x00013555
		public int Compare(MessageRec msg1, MessageRec msg2)
		{
			return msg1.CompareTo(this.sortBy, msg2.CreationTimestamp, msg2.FolderId, msg2.EntryId);
		}

		// Token: 0x0400014B RID: 331
		private static MessageRecComparer normalInstance = new MessageRecComparer(MessageRecSortBy.AscendingTimeStamp);

		// Token: 0x0400014C RID: 332
		private static MessageRecComparer descendingInstance = new MessageRecComparer(MessageRecSortBy.DescendingTimeStamp);

		// Token: 0x0400014D RID: 333
		private readonly MessageRecSortBy sortBy;
	}
}
