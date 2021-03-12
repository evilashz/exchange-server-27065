using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000050 RID: 80
	public class SortBy
	{
		// Token: 0x06000248 RID: 584 RVA: 0x000089DC File Offset: 0x00006BDC
		public SortBy(PropertyDefinition columnDefinition, SortOrder sortOrder)
		{
			if (columnDefinition == null)
			{
				throw new ArgumentNullException("columnDefinition");
			}
			EnumValidator.ThrowIfInvalid<SortOrder>(sortOrder, "sortOrder");
			this.columnDefinition = columnDefinition;
			this.sortOrder = sortOrder;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00008A0B File Offset: 0x00006C0B
		internal PropertyDefinition ColumnDefinition
		{
			get
			{
				return this.columnDefinition;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00008A13 File Offset: 0x00006C13
		internal SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x040000E6 RID: 230
		private readonly PropertyDefinition columnDefinition;

		// Token: 0x040000E7 RID: 231
		private readonly SortOrder sortOrder;
	}
}
