using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000091 RID: 145
	public class TableLevelIOStats
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x0001C789 File Offset: 0x0001A989
		public TableLevelIOStats(string tableName)
		{
			this.Reset();
			this.tableName = tableName;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001C79E File Offset: 0x0001A99E
		public string TableName
		{
			get
			{
				return this.tableName;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001C7A6 File Offset: 0x0001A9A6
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x0001C7AE File Offset: 0x0001A9AE
		public int LogicalReads
		{
			get
			{
				return this.logicalReads;
			}
			set
			{
				this.logicalReads = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001C7B7 File Offset: 0x0001A9B7
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x0001C7BF File Offset: 0x0001A9BF
		public int PhysicalReads
		{
			get
			{
				return this.physicalReads;
			}
			set
			{
				this.physicalReads = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001C7C8 File Offset: 0x0001A9C8
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x0001C7D0 File Offset: 0x0001A9D0
		public int ReadAheads
		{
			get
			{
				return this.readAheads;
			}
			set
			{
				this.readAheads = value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0001C7D9 File Offset: 0x0001A9D9
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x0001C7E1 File Offset: 0x0001A9E1
		public int LobLogicalReads
		{
			get
			{
				return this.lobLogicalReads;
			}
			set
			{
				this.lobLogicalReads = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001C7EA File Offset: 0x0001A9EA
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x0001C7F2 File Offset: 0x0001A9F2
		public int LobPhysicalReads
		{
			get
			{
				return this.lobPhysicalReads;
			}
			set
			{
				this.lobPhysicalReads = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0001C7FB File Offset: 0x0001A9FB
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0001C803 File Offset: 0x0001AA03
		public int LobReadAheads
		{
			get
			{
				return this.lobReadAheads;
			}
			set
			{
				this.lobReadAheads = value;
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001C80C File Offset: 0x0001AA0C
		public void Reset()
		{
			this.logicalReads = 0;
			this.physicalReads = 0;
			this.readAheads = 0;
			this.lobLogicalReads = 0;
			this.lobPhysicalReads = 0;
			this.lobReadAheads = 0;
		}

		// Token: 0x0400024B RID: 587
		private readonly string tableName;

		// Token: 0x0400024C RID: 588
		private int logicalReads;

		// Token: 0x0400024D RID: 589
		private int physicalReads;

		// Token: 0x0400024E RID: 590
		private int readAheads;

		// Token: 0x0400024F RID: 591
		private int lobLogicalReads;

		// Token: 0x04000250 RID: 592
		private int lobPhysicalReads;

		// Token: 0x04000251 RID: 593
		private int lobReadAheads;
	}
}
