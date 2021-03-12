using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200007F RID: 127
	public class SortOrderBuilder : IEnumerable<SortColumn>, IEnumerable
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x00019E88 File Offset: 0x00018088
		public SortOrderBuilder() : this(6)
		{
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00019E91 File Offset: 0x00018091
		public SortOrderBuilder(int space)
		{
			this.columns = new List<Column>(space);
			this.ascending = new List<bool>(space);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00019EB4 File Offset: 0x000180B4
		public SortOrderBuilder(SortOrder sortOrder)
		{
			if (sortOrder.Count == 0)
			{
				this.columns = new List<Column>(6);
				this.ascending = new List<bool>(6);
				return;
			}
			this.columns = sortOrder.Columns;
			this.ascending = sortOrder.Ascending;
			this.listsAreReadOnly = true;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00019F0A File Offset: 0x0001810A
		public int Count
		{
			get
			{
				return this.columns.Count;
			}
		}

		// Token: 0x17000189 RID: 393
		public SortColumn this[int index]
		{
			get
			{
				return new SortColumn(this.columns[index], this.ascending[index]);
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00019F36 File Offset: 0x00018136
		public static explicit operator SortOrder(SortOrderBuilder builder)
		{
			return builder.ToSortOrder();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00019F3E File Offset: 0x0001813E
		public SortOrder ToSortOrder()
		{
			this.listsAreReadOnly = true;
			return this.PrivateToSortOrder();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00019F4D File Offset: 0x0001814D
		private SortOrder PrivateToSortOrder()
		{
			return new SortOrder(this.columns, this.ascending);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00019F60 File Offset: 0x00018160
		public void CopyFrom(SortOrder sortOrder)
		{
			if (sortOrder.Count == 0)
			{
				this.Clear();
				return;
			}
			this.columns = sortOrder.Columns;
			this.ascending = sortOrder.Ascending;
			this.listsAreReadOnly = true;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00019F94 File Offset: 0x00018194
		public void Add(Column column, bool ascending)
		{
			if (this.listsAreReadOnly)
			{
				this.columns = new List<Column>(this.columns);
				this.ascending = new List<bool>(this.ascending);
				this.listsAreReadOnly = false;
			}
			this.columns.Add(column);
			this.ascending.Add(ascending);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00019FEA File Offset: 0x000181EA
		public void Add(Column column)
		{
			this.Add(column, true);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00019FF4 File Offset: 0x000181F4
		public void Clear()
		{
			if (this.listsAreReadOnly)
			{
				this.columns = new List<Column>(6);
				this.ascending = new List<bool>(6);
				this.listsAreReadOnly = false;
				return;
			}
			this.columns.Clear();
			this.ascending.Clear();
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001A034 File Offset: 0x00018234
		public bool Contains(Column column)
		{
			return this.IndexOf(column) != -1;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001A044 File Offset: 0x00018244
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

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001A07C File Offset: 0x0001827C
		public void Reverse()
		{
			if (this.Count != 0)
			{
				if (this.listsAreReadOnly)
				{
					this.columns = new List<Column>(this.columns);
					this.ascending = new List<bool>(this.ascending);
					this.listsAreReadOnly = false;
				}
				for (int i = 0; i < this.Count; i++)
				{
					this.ascending[i] = !this.ascending[i];
				}
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001A0F0 File Offset: 0x000182F0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(50 * this.columns.Count);
			this.PrivateToSortOrder().AppendToStringBuilder(stringBuilder, StringFormatOptions.IncludeDetails);
			return stringBuilder.ToString();
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001A128 File Offset: 0x00018328
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.PrivateToSortOrder().GetEnumerator();
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001A148 File Offset: 0x00018348
		IEnumerator<SortColumn> IEnumerable<SortColumn>.GetEnumerator()
		{
			return this.PrivateToSortOrder().GetEnumerator();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001A168 File Offset: 0x00018368
		private SortOrder.Enumerator GetEnumerator()
		{
			return this.PrivateToSortOrder().GetEnumerator();
		}

		// Token: 0x040001AA RID: 426
		private const int AverageSortOrderSize = 6;

		// Token: 0x040001AB RID: 427
		private IList<Column> columns;

		// Token: 0x040001AC RID: 428
		private IList<bool> ascending;

		// Token: 0x040001AD RID: 429
		private bool listsAreReadOnly;
	}
}
