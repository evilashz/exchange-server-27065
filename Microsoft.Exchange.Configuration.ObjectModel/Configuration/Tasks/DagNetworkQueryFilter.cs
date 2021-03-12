using System;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000100 RID: 256
	internal class DagNetworkQueryFilter : QueryFilter
	{
		// Token: 0x0600093E RID: 2366 RVA: 0x0001FE60 File Offset: 0x0001E060
		public DagNetworkQueryFilter(DagNetworkObjectId networkNames)
		{
			this.m_networkNames = networkNames;
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001FE6F File Offset: 0x0001E06F
		public DagNetworkObjectId NamesToMatch
		{
			get
			{
				return this.m_networkNames;
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001FE77 File Offset: 0x0001E077
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(DagNetworkQueryFilter)");
		}

		// Token: 0x04000268 RID: 616
		private DagNetworkObjectId m_networkNames;
	}
}
