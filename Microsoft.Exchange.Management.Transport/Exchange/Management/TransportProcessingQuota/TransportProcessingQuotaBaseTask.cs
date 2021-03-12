using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Reporting;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.TransportProcessingQuota
{
	// Token: 0x020000AD RID: 173
	public class TransportProcessingQuotaBaseTask : Task
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0001A3F9 File Offset: 0x000185F9
		internal ITenantThrottlingSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001A401 File Offset: 0x00018601
		protected override void InternalValidate()
		{
			base.InternalValidate();
			this.session = HygieneUtils.InstantiateType<ITenantThrottlingSession>("Microsoft.Exchange.Hygiene.Data.Reporting.ReportingSession");
		}

		// Token: 0x04000242 RID: 578
		private ITenantThrottlingSession session;
	}
}
