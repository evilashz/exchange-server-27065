using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	internal class NearFilter : QueryFilter
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00006F9B File Offset: 0x0000519B
		public NearFilter(uint distance, bool ordered, AndFilter filter)
		{
			this.distance = distance;
			this.ordered = ordered;
			this.filter = filter;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006FB8 File Offset: 0x000051B8
		public override bool Equals(object obj)
		{
			NearFilter nearFilter = obj as NearFilter;
			return nearFilter != null && nearFilter.GetType() == base.GetType() && nearFilter.Distance == this.Distance && nearFilter.Ordered == this.Ordered && nearFilter.Filter.Equals(this.Filter);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007014 File Offset: 0x00005214
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Distance.GetHashCode() ^ this.Ordered.GetHashCode() ^ this.filter.GetHashCode();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007056 File Offset: 0x00005256
		public override IEnumerable<string> Keywords()
		{
			return this.filter.Keywords();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007064 File Offset: 0x00005264
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(");
			sb.Append(this.distance);
			sb.Append(", ");
			sb.Append(this.ordered);
			sb.Append(", (");
			if (this.filter != null)
			{
				this.filter.ToString(sb);
			}
			sb.Append("))");
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000070D0 File Offset: 0x000052D0
		protected override int GetSize()
		{
			int size = base.GetSize();
			return size + this.filter.Size;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000070F3 File Offset: 0x000052F3
		public uint Distance
		{
			get
			{
				return this.distance;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000070FB File Offset: 0x000052FB
		public bool Ordered
		{
			get
			{
				return this.ordered;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007103 File Offset: 0x00005303
		public AndFilter Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x0400008D RID: 141
		private readonly uint distance;

		// Token: 0x0400008E RID: 142
		private readonly bool ordered;

		// Token: 0x0400008F RID: 143
		private readonly AndFilter filter;
	}
}
