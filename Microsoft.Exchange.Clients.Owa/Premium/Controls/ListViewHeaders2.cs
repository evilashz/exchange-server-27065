using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000316 RID: 790
	internal abstract class ListViewHeaders2
	{
		// Token: 0x06001E05 RID: 7685 RVA: 0x000ADF80 File Offset: 0x000AC180
		protected ListViewHeaders2(ColumnId sortedColumn, SortOrder sortOrder, UserContext userContext)
		{
			this.sortedColumn = ListViewColumns.GetColumn(sortedColumn);
			this.sortOrder = sortOrder;
			this.userContext = userContext;
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x000ADFA2 File Offset: 0x000AC1A2
		protected Column SortedColumn
		{
			get
			{
				return this.sortedColumn;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x000ADFAA File Offset: 0x000AC1AA
		protected SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x000ADFB2 File Offset: 0x000AC1B2
		protected UserContext UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x000ADFBA File Offset: 0x000AC1BA
		internal void Render(TextWriter writer)
		{
			this.RenderHeaders(writer);
		}

		// Token: 0x06001E0A RID: 7690
		protected abstract void RenderHeaders(TextWriter writer);

		// Token: 0x06001E0B RID: 7691 RVA: 0x000ADFC4 File Offset: 0x000AC1C4
		protected void RenderSortIcon(TextWriter writer)
		{
			switch (this.sortOrder)
			{
			case SortOrder.Ascending:
				this.userContext.RenderThemeImage(writer, ThemeFileId.SortAscending, null, new object[0]);
				goto IL_49;
			}
			this.userContext.RenderThemeImage(writer, ThemeFileId.SortDescending, null, new object[0]);
			IL_49:
			writer.Write("&nbsp;");
		}

		// Token: 0x04001654 RID: 5716
		protected const string HeadersTableId = "tblH";

		// Token: 0x04001655 RID: 5717
		protected static readonly string SortAscending = 0.ToString(CultureInfo.InvariantCulture);

		// Token: 0x04001656 RID: 5718
		protected static readonly string SortDescending = 1.ToString(CultureInfo.InvariantCulture);

		// Token: 0x04001657 RID: 5719
		private Column sortedColumn;

		// Token: 0x04001658 RID: 5720
		private SortOrder sortOrder;

		// Token: 0x04001659 RID: 5721
		protected UserContext userContext;
	}
}
