using System;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002B5 RID: 693
	public sealed class ColumnBehavior
	{
		// Token: 0x06001B23 RID: 6947 RVA: 0x0009B879 File Offset: 0x00099A79
		public ColumnBehavior(HorizontalAlign horizontalAlign, int width, bool isFixedWidth, SortOrder defaultSortOrder, GroupType groupType)
		{
			this.horizontalAlign = horizontalAlign;
			this.width = width;
			this.isFixedWidth = isFixedWidth;
			this.defaultSortOrder = defaultSortOrder;
			this.groupType = groupType;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0009B8AD File Offset: 0x00099AAD
		public ColumnBehavior(HorizontalAlign horizontalAlign, int width, bool isFixedWidth, SortOrder defaultSortOrder) : this(horizontalAlign, width, isFixedWidth, defaultSortOrder, GroupType.None)
		{
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0009B8BB File Offset: 0x00099ABB
		public ColumnBehavior(int width, bool isFixedWidth, SortOrder defaultSortOrder, GroupType groupType) : this(HorizontalAlign.NotSet, width, isFixedWidth, defaultSortOrder, groupType)
		{
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0009B8C9 File Offset: 0x00099AC9
		public ColumnBehavior(int width, bool isFixedWidth, SortOrder defaultSortOrder) : this(HorizontalAlign.NotSet, width, isFixedWidth, defaultSortOrder, GroupType.None)
		{
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0009B8D6 File Offset: 0x00099AD6
		public ColumnBehavior(HorizontalAlign horizontalAlign, int width, bool isFixedWidth)
		{
			this.horizontalAlign = horizontalAlign;
			this.width = width;
			this.isFixedWidth = isFixedWidth;
			this.isSortable = false;
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x0009B901 File Offset: 0x00099B01
		public SortOrder DefaultSortOrder
		{
			get
			{
				return this.defaultSortOrder;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0009B909 File Offset: 0x00099B09
		public GroupType GroupType
		{
			get
			{
				return this.groupType;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x0009B911 File Offset: 0x00099B11
		public HorizontalAlign HorizontalAlign
		{
			get
			{
				return this.horizontalAlign;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x0009B919 File Offset: 0x00099B19
		public int Width
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0009B921 File Offset: 0x00099B21
		public bool IsFixedWidth
		{
			get
			{
				return this.isFixedWidth;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x0009B929 File Offset: 0x00099B29
		public bool IsSortable
		{
			get
			{
				return this.isSortable;
			}
		}

		// Token: 0x0400132F RID: 4911
		private HorizontalAlign horizontalAlign;

		// Token: 0x04001330 RID: 4912
		private int width;

		// Token: 0x04001331 RID: 4913
		private bool isFixedWidth;

		// Token: 0x04001332 RID: 4914
		private GroupType groupType;

		// Token: 0x04001333 RID: 4915
		private SortOrder defaultSortOrder;

		// Token: 0x04001334 RID: 4916
		private bool isSortable = true;
	}
}
