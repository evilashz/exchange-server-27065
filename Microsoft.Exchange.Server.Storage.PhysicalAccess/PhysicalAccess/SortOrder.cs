using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200007D RID: 125
	public struct SortOrder : IEnumerable<SortColumn>, IEnumerable, IEquatable<SortOrder>
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x00019A0B File Offset: 0x00017C0B
		public SortOrder(IList<Column> columns, IList<bool> ascending)
		{
			this.columns = columns;
			this.ascending = ascending;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00019A1B File Offset: 0x00017C1B
		public static SortOrder Empty
		{
			get
			{
				return SortOrder.empty;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00019A22 File Offset: 0x00017C22
		public IList<Column> Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00019A2A File Offset: 0x00017C2A
		public IList<bool> Ascending
		{
			get
			{
				return this.ascending;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00019A32 File Offset: 0x00017C32
		public int Count
		{
			get
			{
				if (this.columns != null)
				{
					return this.columns.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00019A49 File Offset: 0x00017C49
		public bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		// Token: 0x17000185 RID: 389
		public SortColumn this[int index]
		{
			get
			{
				return new SortColumn(this.columns[index], this.ascending[index]);
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00019A74 File Offset: 0x00017C74
		public static bool IsMatch(SortOrder desiredSortOrder, SortOrder actualSortOrder, ISet<Column> ignoreConstantColumns, out bool reverseSortOrder)
		{
			reverseSortOrder = false;
			int count = desiredSortOrder.Count;
			if (count > 0)
			{
				int count2 = actualSortOrder.Count;
				if (count2 == 0)
				{
					return false;
				}
				bool flag = true;
				int i = 0;
				int j = 0;
				while (j < count)
				{
					if (i < count2 && !(desiredSortOrder.Columns[j] != actualSortOrder.Columns[i]))
					{
						goto IL_BB;
					}
					if (ignoreConstantColumns == null || !ignoreConstantColumns.Contains(desiredSortOrder.Columns[j]))
					{
						while (i < count2)
						{
							if (ignoreConstantColumns != null && ignoreConstantColumns.Contains(actualSortOrder.Columns[i]))
							{
								i++;
							}
							else
							{
								if (desiredSortOrder.Columns[j] != actualSortOrder.Columns[i])
								{
									reverseSortOrder = false;
									return false;
								}
								goto IL_BB;
							}
						}
						reverseSortOrder = false;
						return false;
					}
					IL_12F:
					j++;
					continue;
					IL_BB:
					if (flag)
					{
						reverseSortOrder = (desiredSortOrder.Ascending[j] != actualSortOrder.Ascending[i]);
						flag = false;
					}
					else if ((!reverseSortOrder && desiredSortOrder.Ascending[j] != actualSortOrder.Ascending[i]) || (reverseSortOrder && desiredSortOrder.Ascending[j] == actualSortOrder.Ascending[i]))
					{
						reverseSortOrder = false;
						return false;
					}
					i++;
					goto IL_12F;
				}
			}
			return true;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00019BC0 File Offset: 0x00017DC0
		public static bool IsMatchOrReverse(SortOrder desiredSortOrder, SortOrder actualSortOrder)
		{
			bool flag;
			return SortOrder.IsMatch(desiredSortOrder, actualSortOrder, null, out flag);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00019BD7 File Offset: 0x00017DD7
		public bool Contains(Column column)
		{
			return this.IndexOf(column) != -1;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00019BE8 File Offset: 0x00017DE8
		public int IndexOf(Column column)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (column == this.columns[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00019C20 File Offset: 0x00017E20
		public SortOrder Reverse()
		{
			if (this.Count == 0)
			{
				return this;
			}
			SortOrderBuilder sortOrderBuilder = new SortOrderBuilder();
			for (int i = 0; i < this.Count; i++)
			{
				sortOrderBuilder.Add(this.columns[i], !this.ascending[i]);
			}
			return sortOrderBuilder.ToSortOrder();
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00019C7C File Offset: 0x00017E7C
		internal void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (i != 0)
				{
					sb.Append(", ");
				}
				this.columns[i].AppendToString(sb, formatOptions);
				if (!this.ascending[i])
				{
					sb.Append(" desc");
				}
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00019CD8 File Offset: 0x00017ED8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(50 * this.Count);
			this.AppendToStringBuilder(stringBuilder, StringFormatOptions.IncludeDetails);
			return stringBuilder.ToString();
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00019D04 File Offset: 0x00017F04
		public bool Equals(SortOrder other)
		{
			if (this.Count != other.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (this.columns[i] != other.Columns[i] || this.ascending[i] != other.Ascending[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00019D71 File Offset: 0x00017F71
		public override bool Equals(object other)
		{
			return other is SortOrder && this.Equals((SortOrder)other);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00019D8C File Offset: 0x00017F8C
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.Count; i++)
			{
				num += this.columns[i].GetHashCode() + (this.ascending[i] ? 0 : 1);
			}
			return num;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00019DD4 File Offset: 0x00017FD4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00019DE1 File Offset: 0x00017FE1
		IEnumerator<SortColumn> IEnumerable<SortColumn>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00019DEE File Offset: 0x00017FEE
		public SortOrder.Enumerator GetEnumerator()
		{
			return new SortOrder.Enumerator(this);
		}

		// Token: 0x040001A5 RID: 421
		private static readonly SortOrder empty = default(SortOrder);

		// Token: 0x040001A6 RID: 422
		private IList<Column> columns;

		// Token: 0x040001A7 RID: 423
		private IList<bool> ascending;

		// Token: 0x0200007E RID: 126
		public struct Enumerator : IEnumerator<SortColumn>, IDisposable, IEnumerator
		{
			// Token: 0x06000594 RID: 1428 RVA: 0x00019E08 File Offset: 0x00018008
			internal Enumerator(SortOrder sortOrder)
			{
				this.sortOrder = sortOrder;
				this.index = -1;
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x06000595 RID: 1429 RVA: 0x00019E18 File Offset: 0x00018018
			public SortColumn Current
			{
				get
				{
					return this.sortOrder[this.index];
				}
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x06000596 RID: 1430 RVA: 0x00019E2B File Offset: 0x0001802B
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x00019E38 File Offset: 0x00018038
			public bool MoveNext()
			{
				if (this.index < this.sortOrder.Count)
				{
					this.index++;
					if (this.index < this.sortOrder.Count)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000598 RID: 1432 RVA: 0x00019E71 File Offset: 0x00018071
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x06000599 RID: 1433 RVA: 0x00019E7A File Offset: 0x0001807A
			public void Dispose()
			{
				this.sortOrder = default(SortOrder);
			}

			// Token: 0x040001A8 RID: 424
			private SortOrder sortOrder;

			// Token: 0x040001A9 RID: 425
			private int index;
		}
	}
}
