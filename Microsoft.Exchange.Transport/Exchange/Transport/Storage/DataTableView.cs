using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000CE RID: 206
	[Serializable]
	internal class DataTableView
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x0001D870 File Offset: 0x0001BA70
		public DataTableView(DataTable table)
		{
			this.table = table;
			this.Initialize();
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001D885 File Offset: 0x0001BA85
		public DataTableView(DataTable table, int[] viewColumns)
		{
			this.table = table;
			this.Initialize(viewColumns);
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001D89B File Offset: 0x0001BA9B
		public DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0001D8A3 File Offset: 0x0001BAA3
		public int ColumnCount
		{
			get
			{
				return this.viewToRowIndex.Length;
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001D8B0 File Offset: 0x0001BAB0
		public int GetColumnViewIndex(int rowIndex)
		{
			int num = this.rowToViewIndex[rowIndex];
			if (num == -1)
			{
				throw new ArgumentException(string.Format("Column {0} not in view", rowIndex), "rowIndex");
			}
			return num;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001D8E6 File Offset: 0x0001BAE6
		public int GetColumnRowIndex(int viewIndex)
		{
			return this.viewToRowIndex[viewIndex];
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001D8F0 File Offset: 0x0001BAF0
		private void Initialize()
		{
			int cacheCount = this.table.CacheCount;
			int[] array = new int[cacheCount];
			int num = 0;
			for (int i = 0; i < this.table.Schemas.Count; i++)
			{
				if (this.table.Schemas[i].Cached)
				{
					array[num] = i;
					num++;
				}
			}
			this.Initialize(array);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001D954 File Offset: 0x0001BB54
		private void Initialize(int[] viewColumns)
		{
			this.rowToViewIndex = new int[this.table.Schemas.Count];
			this.viewToRowIndex = new int[viewColumns.Length];
			int num = 0;
			for (int i = 0; i < this.table.Schemas.Count; i++)
			{
				this.rowToViewIndex[i] = -1;
				if (this.table.Schemas[i].Cached)
				{
					for (int j = 0; j < viewColumns.Length; j++)
					{
						if (viewColumns[j] == i)
						{
							this.rowToViewIndex[i] = num;
							this.viewToRowIndex[num] = i;
							num++;
							break;
						}
					}
				}
			}
			if (num != viewColumns.Length)
			{
				throw new IndexOutOfRangeException("The view columns array contained indexes that do not belong to any row column or duplicates");
			}
		}

		// Token: 0x04000376 RID: 886
		private DataTable table;

		// Token: 0x04000377 RID: 887
		private int[] rowToViewIndex;

		// Token: 0x04000378 RID: 888
		private int[] viewToRowIndex;
	}
}
