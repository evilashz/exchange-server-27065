using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Providers;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000EB RID: 235
	internal class AlternateMailboxQueryFilter : QueryFilter
	{
		// Token: 0x06000886 RID: 2182 RVA: 0x0001E85C File Offset: 0x0001CA5C
		public AlternateMailboxQueryFilter(AlternateMailboxObjectId amNames)
		{
			this.m_amNames = amNames;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0001E86B File Offset: 0x0001CA6B
		public AlternateMailboxObjectId NamesToMatch
		{
			get
			{
				return this.m_amNames;
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001E873 File Offset: 0x0001CA73
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(AlternateMailboxQueryFilter)");
		}

		// Token: 0x04000257 RID: 599
		private AlternateMailboxObjectId m_amNames;
	}
}
