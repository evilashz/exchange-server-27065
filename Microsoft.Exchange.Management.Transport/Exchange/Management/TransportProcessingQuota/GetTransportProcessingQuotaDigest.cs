using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data.Reporting;

namespace Microsoft.Exchange.Management.TransportProcessingQuota
{
	// Token: 0x020000AF RID: 175
	[Cmdlet("Get", "TransportProcessingQuotaDigest")]
	public sealed class GetTransportProcessingQuotaDigest : TransportProcessingQuotaBaseTask
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0001A442 File Offset: 0x00018642
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x0001A44A File Offset: 0x0001864A
		[Parameter(Mandatory = false)]
		public int? ResultSize { get; set; }

		// Token: 0x06000648 RID: 1608 RVA: 0x0001A45C File Offset: 0x0001865C
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			foreach (TenantThrottleInfo tenantThrottleInfo in from i in base.Session.GetTenantThrottlingDigest(0, null, false, this.ResultSize ?? 100, false)
			orderby i.AverageMessageCostMs descending
			select i)
			{
				base.WriteObject(TransportProcessingQuotaDigest.Create(tenantThrottleInfo));
			}
		}

		// Token: 0x04000243 RID: 579
		private const int DefaultResultSize = 100;
	}
}
