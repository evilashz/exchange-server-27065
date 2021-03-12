using System;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A6C RID: 2668
	internal class IPListQueryFilter : QueryFilter
	{
		// Token: 0x06005F40 RID: 24384 RVA: 0x0018F612 File Offset: 0x0018D812
		public IPListQueryFilter(IPAddress address)
		{
			this.address = ((address != null) ? new IPvxAddress(address) : IPvxAddress.None);
		}

		// Token: 0x06005F41 RID: 24385 RVA: 0x0018F630 File Offset: 0x0018D830
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(");
			sb.Append(this.address.ToString());
			sb.Append(")");
		}

		// Token: 0x17001CB3 RID: 7347
		// (get) Token: 0x06005F42 RID: 24386 RVA: 0x0018F662 File Offset: 0x0018D862
		public IPvxAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x0400351F RID: 13599
		private IPvxAddress address;
	}
}
