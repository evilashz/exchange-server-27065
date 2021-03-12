using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000339 RID: 825
	internal abstract class LegacyListViewHeaders
	{
		// Token: 0x06001F3A RID: 7994 RVA: 0x000B36CA File Offset: 0x000B18CA
		protected LegacyListViewHeaders(ColumnId sortedColumn, SortOrder sortOrder, UserContext userContext)
		{
			this.sortedColumn = ListViewColumns.GetColumn(sortedColumn);
			this.sortOrder = sortOrder;
			this.userContext = userContext;
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x000B36EC File Offset: 0x000B18EC
		protected Column SortedColumn
		{
			get
			{
				return this.sortedColumn;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x000B36F4 File Offset: 0x000B18F4
		protected SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x000B36FC File Offset: 0x000B18FC
		protected UserContext UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x000B3704 File Offset: 0x000B1904
		internal void Render(TextWriter writer)
		{
			this.RenderHeaders(writer);
		}

		// Token: 0x06001F3F RID: 7999
		protected abstract void RenderHeaders(TextWriter writer);

		// Token: 0x06001F40 RID: 8000 RVA: 0x000B3710 File Offset: 0x000B1910
		protected void RenderSortIcon(TextWriter writer)
		{
			switch (this.sortOrder)
			{
			case SortOrder.Ascending:
				this.userContext.RenderThemeImage(writer, ThemeFileId.SortAscending, null, new object[]
				{
					"style=\"vertical-align:middle;\""
				});
				goto IL_5D;
			}
			this.userContext.RenderThemeImage(writer, ThemeFileId.SortDescending, null, new object[]
			{
				"style=\"vertical-align:middle;\""
			});
			IL_5D:
			writer.Write("&nbsp;");
		}

		// Token: 0x040016BD RID: 5821
		protected const string HeadersTableId = "tblH";

		// Token: 0x040016BE RID: 5822
		protected static readonly string SortAscending = 0.ToString(CultureInfo.InvariantCulture);

		// Token: 0x040016BF RID: 5823
		protected static readonly string SortDescending = 1.ToString(CultureInfo.InvariantCulture);

		// Token: 0x040016C0 RID: 5824
		private Column sortedColumn;

		// Token: 0x040016C1 RID: 5825
		private SortOrder sortOrder;

		// Token: 0x040016C2 RID: 5826
		protected UserContext userContext;
	}
}
