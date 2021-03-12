using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000B9 RID: 185
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	internal sealed class DataColumnDefinitionAttribute : Attribute
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x000194B7 File Offset: 0x000176B7
		public DataColumnDefinitionAttribute(Type columnType, ColumnAccess columnAccess)
		{
			this.columnType = columnType;
			this.accessMode = columnAccess;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x000194CD File Offset: 0x000176CD
		public Type ColumnType
		{
			get
			{
				return this.columnType;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x000194D5 File Offset: 0x000176D5
		public ColumnAccess ColumnAccess
		{
			get
			{
				return this.accessMode;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x000194DD File Offset: 0x000176DD
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x000194E5 File Offset: 0x000176E5
		public bool PrimaryKey
		{
			get
			{
				return this.primaryKey;
			}
			set
			{
				this.primaryKey = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x000194EE File Offset: 0x000176EE
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x000194F6 File Offset: 0x000176F6
		public bool Required
		{
			get
			{
				return this.required;
			}
			set
			{
				this.required = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x000194FF File Offset: 0x000176FF
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x00019507 File Offset: 0x00017707
		public bool AutoIncrement
		{
			get
			{
				return this.autoIncrement;
			}
			set
			{
				this.autoIncrement = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x00019510 File Offset: 0x00017710
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x00019518 File Offset: 0x00017718
		public bool AutoVersioned
		{
			get
			{
				return this.autoVersioned;
			}
			set
			{
				this.autoVersioned = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00019521 File Offset: 0x00017721
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x00019529 File Offset: 0x00017729
		public bool IntrinsicLV
		{
			get
			{
				return this.intrinsicLV;
			}
			set
			{
				this.intrinsicLV = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00019532 File Offset: 0x00017732
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x0001953A File Offset: 0x0001773A
		public bool MultiValued
		{
			get
			{
				return this.multiValued;
			}
			set
			{
				this.multiValued = value;
			}
		}

		// Token: 0x04000300 RID: 768
		private readonly Type columnType;

		// Token: 0x04000301 RID: 769
		private readonly ColumnAccess accessMode;

		// Token: 0x04000302 RID: 770
		private bool primaryKey;

		// Token: 0x04000303 RID: 771
		private bool required;

		// Token: 0x04000304 RID: 772
		private bool autoIncrement;

		// Token: 0x04000305 RID: 773
		private bool autoVersioned;

		// Token: 0x04000306 RID: 774
		private bool intrinsicLV;

		// Token: 0x04000307 RID: 775
		private bool multiValued;
	}
}
