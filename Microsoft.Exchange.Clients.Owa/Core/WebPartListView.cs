using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020002D9 RID: 729
	public class WebPartListView
	{
		// Token: 0x06001C1B RID: 7195 RVA: 0x000A1D5C File Offset: 0x0009FF5C
		public WebPartListView()
		{
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000A1D64 File Offset: 0x0009FF64
		public WebPartListView(int? columnId, int? sortOrder, bool? isMultiline, bool isPremiumOnly)
		{
			this.columnId = columnId;
			this.sortOrder = sortOrder;
			this.isMultiLine = isMultiline;
			this.isPremiumOnly = isPremiumOnly;
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x000A1D89 File Offset: 0x0009FF89
		// (set) Token: 0x06001C1E RID: 7198 RVA: 0x000A1D91 File Offset: 0x0009FF91
		public int? ColumnId
		{
			get
			{
				return this.columnId;
			}
			set
			{
				this.columnId = value;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x000A1D9A File Offset: 0x0009FF9A
		// (set) Token: 0x06001C20 RID: 7200 RVA: 0x000A1DA2 File Offset: 0x0009FFA2
		public int? SortOrder
		{
			get
			{
				return this.sortOrder;
			}
			set
			{
				this.sortOrder = value;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x000A1DAB File Offset: 0x0009FFAB
		// (set) Token: 0x06001C22 RID: 7202 RVA: 0x000A1DB3 File Offset: 0x0009FFB3
		public bool? IsMultiLine
		{
			get
			{
				return this.isMultiLine;
			}
			set
			{
				this.isMultiLine = value;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x000A1DBC File Offset: 0x0009FFBC
		// (set) Token: 0x06001C24 RID: 7204 RVA: 0x000A1DC4 File Offset: 0x0009FFC4
		public bool IsPremiumOnly
		{
			get
			{
				return this.isPremiumOnly;
			}
			set
			{
				this.isPremiumOnly = value;
			}
		}

		// Token: 0x040014DB RID: 5339
		private int? columnId;

		// Token: 0x040014DC RID: 5340
		private int? sortOrder;

		// Token: 0x040014DD RID: 5341
		private bool? isMultiLine;

		// Token: 0x040014DE RID: 5342
		private bool isPremiumOnly;
	}
}
