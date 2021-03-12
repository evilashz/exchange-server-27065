using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	internal class SubFilter : QueryFilter
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00007E9F File Offset: 0x0000609F
		public SubFilter(SubFilterProperties subFilterProperty, QueryFilter filter)
		{
			this.subFilterProperty = subFilterProperty;
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			this.filter = filter;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00007EC3 File Offset: 0x000060C3
		public QueryFilter Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00007ECB File Offset: 0x000060CB
		public SubFilterProperties SubFilterProperty
		{
			get
			{
				return this.subFilterProperty;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007ED4 File Offset: 0x000060D4
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(");
			sb.Append(this.subFilterProperty.ToString());
			sb.Append("(");
			this.filter.ToString(sb);
			sb.Append("))");
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00007F28 File Offset: 0x00006128
		public override bool Equals(object obj)
		{
			SubFilter subFilter = obj as SubFilter;
			return subFilter != null && subFilter.GetType() == base.GetType() && this.subFilterProperty == subFilter.subFilterProperty && this.filter.Equals(subFilter.filter);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00007F73 File Offset: 0x00006173
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.filter.GetHashCode();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007F8C File Offset: 0x0000618C
		protected override int GetSize()
		{
			int size = base.GetSize();
			return size + this.filter.Size;
		}

		// Token: 0x0400009D RID: 157
		private readonly QueryFilter filter;

		// Token: 0x0400009E RID: 158
		private readonly SubFilterProperties subFilterProperty;
	}
}
