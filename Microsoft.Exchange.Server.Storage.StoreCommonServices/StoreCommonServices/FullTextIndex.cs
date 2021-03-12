using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000155 RID: 341
	public class FullTextIndex : IIndex
	{
		// Token: 0x06000D55 RID: 3413 RVA: 0x00042F6C File Offset: 0x0004116C
		public FullTextIndex(string name, Column[] columns)
		{
			this.name = name;
			this.columns = new List<Column>(columns);
			this.indexTableFunction = new FullTextIndexTableFunctionTableFunction().TableFunction;
			this.renameDictionary = new Dictionary<Column, Column>(this.columns.Count);
			foreach (Column column in this.columns)
			{
				bool flag = false;
				for (int i = 0; i < this.indexTableFunction.Columns.Count; i++)
				{
					if (this.indexTableFunction.Columns[i].Name == column.Name)
					{
						this.renameDictionary.Add(column, this.indexTableFunction.Columns[i]);
						flag = true;
					}
				}
				if (!flag)
				{
					this.renameDictionary.Add(column, column);
				}
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00043060 File Offset: 0x00041260
		public bool ShouldBeCurrent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00043063 File Offset: 0x00041263
		public IReadOnlyDictionary<Column, Column> RenameDictionary
		{
			get
			{
				return this.renameDictionary;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0004306B File Offset: 0x0004126B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x00043073 File Offset: 0x00041273
		public IList<Column> Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0004307B File Offset: 0x0004127B
		public ISet<Column> ConstantColumns
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0004307E File Offset: 0x0004127E
		public SortOrder LogicalSortOrder
		{
			get
			{
				return SortOrder.Empty;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00043085 File Offset: 0x00041285
		public Table Table
		{
			get
			{
				return this.Columns[0].Table;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00043098 File Offset: 0x00041298
		public Table IndexTable
		{
			get
			{
				return this.indexTableFunction;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x000430A0 File Offset: 0x000412A0
		public IList<object> IndexKeyPrefix
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x000430A3 File Offset: 0x000412A3
		public SortOrder SortOrder
		{
			get
			{
				return SortOrder.Empty;
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x000430AA File Offset: 0x000412AA
		public bool GetIndexColumn(Column column, bool acceptTruncated, out Column indexColumn)
		{
			indexColumn = column;
			if ((column.Table == this.indexTableFunction || this.RenameDictionary.TryGetValue(column, out indexColumn)) && (acceptTruncated || column.MaxLength <= indexColumn.MaxLength))
			{
				return true;
			}
			indexColumn = null;
			return false;
		}

		// Token: 0x04000765 RID: 1893
		private readonly string name;

		// Token: 0x04000766 RID: 1894
		private TableFunction indexTableFunction;

		// Token: 0x04000767 RID: 1895
		private Dictionary<Column, Column> renameDictionary;

		// Token: 0x04000768 RID: 1896
		private IList<Column> columns;
	}
}
