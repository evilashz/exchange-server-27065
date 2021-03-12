using System;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002B4 RID: 692
	public sealed class Column
	{
		// Token: 0x06001B12 RID: 6930 RVA: 0x0009B6C4 File Offset: 0x000998C4
		public Column(ColumnId id, ColumnBehavior behavior, bool groupable, ColumnHeader header, SortBoundaries sortBoundaries, params PropertyDefinition[] properties)
		{
			if (behavior == null)
			{
				behavior = Column.defaultBehavior;
			}
			if (groupable)
			{
				this.groupType = behavior.GroupType;
			}
			else
			{
				this.groupType = GroupType.None;
			}
			if (this.GroupType != GroupType.None && sortBoundaries == null)
			{
				throw new ArgumentException("sortBoundaries may not be null if groupType does not equal GroupType.None");
			}
			if (properties == null || properties.Length <= 0)
			{
				throw new ArgumentException("properties may not be null or an empty array");
			}
			this.id = id;
			this.behavior = behavior;
			this.properties = properties;
			this.header = header;
			this.sortBoundaries = sortBoundaries;
			if (1 <= properties.Length)
			{
				this.isTypeDownCapable = (properties[0].Type.Equals(typeof(string)) || properties[0].Type.Equals(typeof(string[])));
			}
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x0009B78E File Offset: 0x0009998E
		public Column(ColumnId id, ColumnBehavior behavior, bool groupable, ColumnHeader header, params PropertyDefinition[] properties) : this(id, behavior, groupable, header, null, properties)
		{
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0009B79E File Offset: 0x0009999E
		public Column(ColumnId id, params PropertyDefinition[] properties) : this(id, null, false, null, null, properties)
		{
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0009B7AC File Offset: 0x000999AC
		public Column(ColumnId id, ColumnBehavior behavior, bool groupable, ColumnHeader header, SortBoundaries sortBoundaries, bool isTypeDownCapable, params PropertyDefinition[] properties) : this(id, behavior, groupable, header, sortBoundaries, properties)
		{
			this.isTypeDownCapable = isTypeDownCapable;
		}

		// Token: 0x1700071E RID: 1822
		public PropertyDefinition this[int index]
		{
			get
			{
				return this.properties[index];
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x0009B7CF File Offset: 0x000999CF
		public ColumnId Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x0009B7D7 File Offset: 0x000999D7
		public int PropertyCount
		{
			get
			{
				return this.properties.Length;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x0009B7E1 File Offset: 0x000999E1
		public ColumnHeader Header
		{
			get
			{
				if (this.header == null)
				{
					throw new OwaInvalidOperationException("Header not available for this column.");
				}
				return this.header;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0009B7FC File Offset: 0x000999FC
		public SortBoundaries SortBoundaries
		{
			get
			{
				if (this.sortBoundaries == null)
				{
					throw new OwaInvalidOperationException("Sort boundaries not available for this column.");
				}
				return this.sortBoundaries;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x0009B817 File Offset: 0x00099A17
		public SortOrder DefaultSortOrder
		{
			get
			{
				return this.behavior.DefaultSortOrder;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x0009B824 File Offset: 0x00099A24
		public GroupType GroupType
		{
			get
			{
				return this.groupType;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0009B82C File Offset: 0x00099A2C
		public HorizontalAlign HorizontalAlign
		{
			get
			{
				return this.behavior.HorizontalAlign;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x0009B839 File Offset: 0x00099A39
		public int Width
		{
			get
			{
				return this.behavior.Width;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0009B846 File Offset: 0x00099A46
		public bool IsFixedWidth
		{
			get
			{
				return this.behavior.IsFixedWidth;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x0009B853 File Offset: 0x00099A53
		public bool IsTypeDownCapable
		{
			get
			{
				return this.isTypeDownCapable;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x0009B85B File Offset: 0x00099A5B
		public bool IsSortable
		{
			get
			{
				return this.behavior.IsSortable;
			}
		}

		// Token: 0x04001327 RID: 4903
		private static readonly ColumnBehavior defaultBehavior = new ColumnBehavior(10, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x04001328 RID: 4904
		private ColumnId id;

		// Token: 0x04001329 RID: 4905
		private ColumnBehavior behavior;

		// Token: 0x0400132A RID: 4906
		private ColumnHeader header;

		// Token: 0x0400132B RID: 4907
		private SortBoundaries sortBoundaries;

		// Token: 0x0400132C RID: 4908
		private PropertyDefinition[] properties;

		// Token: 0x0400132D RID: 4909
		private GroupType groupType;

		// Token: 0x0400132E RID: 4910
		private bool isTypeDownCapable;
	}
}
