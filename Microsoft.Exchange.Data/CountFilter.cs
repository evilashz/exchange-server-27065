using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	internal class CountFilter : QueryFilter
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00008089 File Offset: 0x00006289
		public CountFilter(uint count, QueryFilter filter)
		{
			this.count = count;
			this.filter = filter;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000080A0 File Offset: 0x000062A0
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(");
			sb.Append(this.count);
			sb.Append(", (");
			if (this.filter != null)
			{
				this.filter.ToString(sb);
			}
			sb.Append("))");
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000080F2 File Offset: 0x000062F2
		public uint Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000080FA File Offset: 0x000062FA
		public QueryFilter Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x040000A2 RID: 162
		private readonly uint count;

		// Token: 0x040000A3 RID: 163
		private readonly QueryFilter filter;
	}
}
