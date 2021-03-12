using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200011A RID: 282
	public struct RopTraceKey
	{
		// Token: 0x06000AFC RID: 2812 RVA: 0x00038866 File Offset: 0x00036A66
		internal RopTraceKey(OperationType operationType, int mailboxNumber, ClientType clientType, uint activityId, byte operationId, uint detailId, bool sharedLock)
		{
			this.operationType = operationType;
			this.mailboxNumber = mailboxNumber;
			this.clientType = clientType;
			this.activityId = activityId;
			this.operationId = operationId;
			this.detailId = detailId;
			this.sharedLock = sharedLock;
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0003889D File Offset: 0x00036A9D
		internal OperationType OperationType
		{
			get
			{
				return this.operationType;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x000388A5 File Offset: 0x00036AA5
		internal int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x000388AD File Offset: 0x00036AAD
		internal ClientType ClientType
		{
			get
			{
				return this.clientType;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x000388B5 File Offset: 0x00036AB5
		internal uint ActivityId
		{
			get
			{
				return this.activityId;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x000388BD File Offset: 0x00036ABD
		internal byte OperationId
		{
			get
			{
				return this.operationId;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x000388C5 File Offset: 0x00036AC5
		internal uint DetailId
		{
			get
			{
				return this.detailId;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x000388CD File Offset: 0x00036ACD
		internal bool SharedLock
		{
			get
			{
				return this.sharedLock;
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000388D8 File Offset: 0x00036AD8
		public override int GetHashCode()
		{
			return this.operationType.GetHashCode() ^ this.mailboxNumber ^ this.clientType.GetHashCode() ^ (int)this.activityId ^ (int)this.operationId ^ (int)this.detailId ^ (this.sharedLock ? 1 : 0);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0003892F File Offset: 0x00036B2F
		public override bool Equals(object other)
		{
			return other is RopTraceKey && this.Equals((RopTraceKey)other);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00038948 File Offset: 0x00036B48
		public bool Equals(RopTraceKey other)
		{
			return this.operationType == other.operationType && this.mailboxNumber == other.mailboxNumber && this.clientType == other.clientType && this.activityId == other.activityId && this.operationId == other.operationId && this.detailId == other.detailId && this.sharedLock == other.sharedLock;
		}

		// Token: 0x04000602 RID: 1538
		private OperationType operationType;

		// Token: 0x04000603 RID: 1539
		private int mailboxNumber;

		// Token: 0x04000604 RID: 1540
		private ClientType clientType;

		// Token: 0x04000605 RID: 1541
		private uint activityId;

		// Token: 0x04000606 RID: 1542
		private byte operationId;

		// Token: 0x04000607 RID: 1543
		private uint detailId;

		// Token: 0x04000608 RID: 1544
		private bool sharedLock;
	}
}
