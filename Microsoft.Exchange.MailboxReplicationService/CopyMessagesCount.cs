using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000030 RID: 48
	internal struct CopyMessagesCount
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x0000A7D9 File Offset: 0x000089D9
		public CopyMessagesCount(int newMessages, int changed, int deleted, int read, int unread, int skipped)
		{
			this.NewMessages = newMessages;
			this.Changed = changed;
			this.Deleted = deleted;
			this.Read = read;
			this.Unread = unread;
			this.Skipped = skipped;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000A808 File Offset: 0x00008A08
		public int TotalContentCopied
		{
			get
			{
				return this.NewMessages + this.Changed + this.Deleted + this.Read + this.Unread;
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000A82C File Offset: 0x00008A2C
		public static CopyMessagesCount operator +(CopyMessagesCount left, CopyMessagesCount right)
		{
			return new CopyMessagesCount(left.NewMessages + right.NewMessages, left.Changed + right.Changed, left.Deleted + right.Deleted, left.Read + right.Read, left.Unread + right.Unread, left.Skipped + right.Skipped);
		}

		// Token: 0x040000EC RID: 236
		public int NewMessages;

		// Token: 0x040000ED RID: 237
		public int Changed;

		// Token: 0x040000EE RID: 238
		public int Deleted;

		// Token: 0x040000EF RID: 239
		public int Read;

		// Token: 0x040000F0 RID: 240
		public int Unread;

		// Token: 0x040000F1 RID: 241
		public int Skipped;
	}
}
