using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000136 RID: 310
	public class SimplePseudoIndex : IPseudoIndex, IIndex
	{
		// Token: 0x06000BCE RID: 3022 RVA: 0x0003C3E4 File Offset: 0x0003A5E4
		public SimplePseudoIndex(Table table, Table indexTable, object[] tableFunctionParameters, SortOrder logicalSortOrder, IReadOnlyDictionary<Column, Column> renameDictionary, IList<object> indexKeyPrefix, bool shouldBeCurrent)
		{
			this.table = table;
			this.indexTable = indexTable;
			this.tableFunctionParameters = tableFunctionParameters;
			this.logicalSortOrder = logicalSortOrder;
			this.renameDictionary = renameDictionary;
			this.indexKeyPrefix = indexKeyPrefix;
			this.shouldBeCurrent = shouldBeCurrent;
			this.columns = new List<Column>(this.renameDictionary.Keys);
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0003C442 File Offset: 0x0003A642
		public bool ShouldBeCurrent
		{
			get
			{
				return this.shouldBeCurrent;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0003C44A File Offset: 0x0003A64A
		public IReadOnlyDictionary<Column, Column> RenameDictionary
		{
			get
			{
				return this.renameDictionary;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0003C452 File Offset: 0x0003A652
		public IList<Column> Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0003C45A File Offset: 0x0003A65A
		public ISet<Column> ConstantColumns
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0003C45D File Offset: 0x0003A65D
		public SortOrder LogicalSortOrder
		{
			get
			{
				return this.logicalSortOrder;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0003C465 File Offset: 0x0003A665
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0003C46D File Offset: 0x0003A66D
		public Table IndexTable
		{
			get
			{
				return this.indexTable;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0003C475 File Offset: 0x0003A675
		public SortOrder SortOrder
		{
			get
			{
				return this.indexTable.PrimaryKeyIndex.SortOrder;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0003C487 File Offset: 0x0003A687
		public object[] IndexTableFunctionParameters
		{
			get
			{
				return this.tableFunctionParameters;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0003C48F File Offset: 0x0003A68F
		public int RedundantKeyColumnsCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0003C492 File Offset: 0x0003A692
		public CategorizedTableParams GetCategorizedTableParams(Context context)
		{
			return null;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0003C495 File Offset: 0x0003A695
		public IList<object> IndexKeyPrefix
		{
			get
			{
				return this.indexKeyPrefix;
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0003C49D File Offset: 0x0003A69D
		public bool GetIndexColumn(Column column, bool acceptTruncated, out Column indexColumn)
		{
			indexColumn = column;
			if ((column.Table == this.indexTable || this.RenameDictionary.TryGetValue(column, out indexColumn)) && (acceptTruncated || column.MaxLength <= indexColumn.MaxLength))
			{
				return true;
			}
			indexColumn = null;
			return false;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0003C4DC File Offset: 0x0003A6DC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("SimplePseudoIndex:[Table:[");
			stringBuilder.Append(this.table.Name);
			stringBuilder.Append("] IndexTable:[");
			stringBuilder.Append(this.indexTable);
			stringBuilder.Append("] tableFunctionParameters:[");
			stringBuilder.Append(this.tableFunctionParameters);
			stringBuilder.Append("] LogicalSortOrder:[");
			stringBuilder.Append(this.logicalSortOrder);
			stringBuilder.Append("] RenameDictionary:[");
			stringBuilder.AppendAsString(this.renameDictionary);
			stringBuilder.Append("] IndexKeyPrefix:[");
			stringBuilder.AppendAsString(this.indexKeyPrefix);
			stringBuilder.Append("] ShouldBeCurrent:[");
			stringBuilder.Append(this.shouldBeCurrent);
			stringBuilder.Append("] Columns:[");
			stringBuilder.AppendAsString(this.columns);
			stringBuilder.Append("]]");
			return stringBuilder.ToString();
		}

		// Token: 0x040006AC RID: 1708
		private Table table;

		// Token: 0x040006AD RID: 1709
		private Table indexTable;

		// Token: 0x040006AE RID: 1710
		private object[] tableFunctionParameters;

		// Token: 0x040006AF RID: 1711
		private SortOrder logicalSortOrder;

		// Token: 0x040006B0 RID: 1712
		private IReadOnlyDictionary<Column, Column> renameDictionary;

		// Token: 0x040006B1 RID: 1713
		private IList<object> indexKeyPrefix;

		// Token: 0x040006B2 RID: 1714
		private bool shouldBeCurrent;

		// Token: 0x040006B3 RID: 1715
		private IList<Column> columns;
	}
}
