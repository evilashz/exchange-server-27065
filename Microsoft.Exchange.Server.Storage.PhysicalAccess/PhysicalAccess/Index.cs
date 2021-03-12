using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200002C RID: 44
	public sealed class Index : IIndex
	{
		// Token: 0x0600024C RID: 588 RVA: 0x0000EBC4 File Offset: 0x0000CDC4
		public Index(string name, bool primaryKey, bool unique, bool schemaExtension, bool[] conditional, bool[] ascending, params PhysicalColumn[] columns)
		{
			this.name = name;
			this.primaryKey = primaryKey;
			this.unique = unique;
			this.columns = new List<Column>(columns);
			this.conditional = new List<bool>(conditional);
			this.ascending = new List<bool>(ascending);
			if (schemaExtension)
			{
				this.minVersion = int.MaxValue;
			}
			else
			{
				this.minVersion = 0;
			}
			this.maxVersion = int.MaxValue;
			this.renameDictionary = new Dictionary<Column, Column>(columns.Length);
			foreach (PhysicalColumn column in columns)
			{
				this.renameDictionary.Add(column, column);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000EC67 File Offset: 0x0000CE67
		public IReadOnlyDictionary<Column, Column> RenameDictionary
		{
			get
			{
				return this.renameDictionary;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000EC6F File Offset: 0x0000CE6F
		public Table Table
		{
			get
			{
				return this.columns[0].Table;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000EC82 File Offset: 0x0000CE82
		public Table IndexTable
		{
			get
			{
				return this.Table;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000EC8A File Offset: 0x0000CE8A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000EC92 File Offset: 0x0000CE92
		public IList<Column> Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000EC9A File Offset: 0x0000CE9A
		public ISet<Column> ConstantColumns
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000EC9D File Offset: 0x0000CE9D
		public int ColumnCount
		{
			get
			{
				return this.columns.Count;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000ECAA File Offset: 0x0000CEAA
		public IList<object> IndexKeyPrefix
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000ECAD File Offset: 0x0000CEAD
		public bool PrimaryKey
		{
			get
			{
				return this.primaryKey;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000ECB5 File Offset: 0x0000CEB5
		public bool Unique
		{
			get
			{
				return this.unique;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000ECBD File Offset: 0x0000CEBD
		public IList<bool> Conditional
		{
			get
			{
				return this.conditional;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000ECC5 File Offset: 0x0000CEC5
		public IList<bool> Ascending
		{
			get
			{
				return this.ascending;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000ECCD File Offset: 0x0000CECD
		public SortOrder SortOrder
		{
			get
			{
				return new SortOrder(this.columns, this.ascending);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000ECE0 File Offset: 0x0000CEE0
		public SortOrder LogicalSortOrder
		{
			get
			{
				return this.SortOrder;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		public bool SchemaExtension
		{
			get
			{
				return this.minVersion != 0;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000ECF6 File Offset: 0x0000CEF6
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000ECFE File Offset: 0x0000CEFE
		public int MinVersion
		{
			get
			{
				return this.minVersion;
			}
			set
			{
				this.minVersion = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000ED07 File Offset: 0x0000CF07
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000ED0F File Offset: 0x0000CF0F
		public int MaxVersion
		{
			get
			{
				return this.maxVersion;
			}
			set
			{
				this.maxVersion = value;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000ED18 File Offset: 0x0000CF18
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(20);
			if (this.PrimaryKey)
			{
				stringBuilder.Append("PrimaryKey ");
			}
			if (this.Unique)
			{
				stringBuilder.Append("Unique ");
			}
			stringBuilder.Append("Name:[");
			stringBuilder.Append(this.Name);
			stringBuilder.Append("] Columns:[");
			for (int i = 0; i < this.ColumnCount; i++)
			{
				stringBuilder.Append("[");
				stringBuilder.Append(this.Columns[i].Name);
				if (this.Conditional[i])
				{
					stringBuilder.Append(", Conditional");
				}
				if (!this.Ascending[i])
				{
					stringBuilder.Append(", Descending");
				}
				stringBuilder.Append("]");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000EE04 File Offset: 0x0000D004
		public int PositionInIndex(Column col)
		{
			for (int i = 0; i < this.columns.Count; i++)
			{
				if (this.columns[i] == col)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000EE3E File Offset: 0x0000D03E
		public bool GetIndexColumn(Column column, bool acceptTruncated, out Column indexColumn)
		{
			if (this.PositionInIndex(column) != -1 && (acceptTruncated || column.Size > 0 || column.MaxLength <= 255))
			{
				indexColumn = column;
				return true;
			}
			indexColumn = null;
			return false;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000EE6C File Offset: 0x0000D06C
		internal void SetIsPrimaryKeyForUpgraders(bool primaryKey)
		{
			this.primaryKey = primaryKey;
		}

		// Token: 0x040000BA RID: 186
		public const CompareOptions IndexCompareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x040000BB RID: 187
		private readonly string name;

		// Token: 0x040000BC RID: 188
		private readonly List<Column> columns;

		// Token: 0x040000BD RID: 189
		private readonly List<bool> conditional;

		// Token: 0x040000BE RID: 190
		private readonly List<bool> ascending;

		// Token: 0x040000BF RID: 191
		private readonly bool unique;

		// Token: 0x040000C0 RID: 192
		private bool primaryKey;

		// Token: 0x040000C1 RID: 193
		private int minVersion;

		// Token: 0x040000C2 RID: 194
		private int maxVersion;

		// Token: 0x040000C3 RID: 195
		private Dictionary<Column, Column> renameDictionary;
	}
}
