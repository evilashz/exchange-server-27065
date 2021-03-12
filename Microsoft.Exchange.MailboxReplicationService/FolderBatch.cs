using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000004 RID: 4
	internal class FolderBatch
	{
		// Token: 0x06000012 RID: 18 RVA: 0x0000304E File Offset: 0x0000124E
		public FolderBatch(byte[] folderId)
		{
			this.folderId = folderId;
			this.batchByteSize = 0UL;
			this.batch = new List<MessageRec>(5);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00003071 File Offset: 0x00001271
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00003079 File Offset: 0x00001279
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00003082 File Offset: 0x00001282
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000308A File Offset: 0x0000128A
		public ulong BatchByteSize
		{
			get
			{
				return this.batchByteSize;
			}
			set
			{
				this.batchByteSize = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00003093 File Offset: 0x00001293
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000309B File Offset: 0x0000129B
		public List<MessageRec> Batch
		{
			get
			{
				return this.batch;
			}
			set
			{
				this.batch = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000030A4 File Offset: 0x000012A4
		public DateTime HeadTimestamp
		{
			get
			{
				return this.batch[0].CreationTimestamp;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000030B7 File Offset: 0x000012B7
		public void AddMsg(MessageRec msgRec)
		{
			this.batch.Add(msgRec);
			this.batchByteSize += (ulong)((long)msgRec.MessageSize);
		}

		// Token: 0x04000006 RID: 6
		private byte[] folderId;

		// Token: 0x04000007 RID: 7
		private ulong batchByteSize;

		// Token: 0x04000008 RID: 8
		private List<MessageRec> batch;
	}
}
