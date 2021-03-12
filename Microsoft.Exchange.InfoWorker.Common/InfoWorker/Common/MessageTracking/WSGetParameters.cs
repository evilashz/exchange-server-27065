using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002EF RID: 751
	internal class WSGetParameters
	{
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x00067353 File Offset: 0x00065553
		// (set) Token: 0x06001628 RID: 5672 RVA: 0x0006735B File Offset: 0x0006555B
		internal MessageTrackingReportId MessageTrackingReportId { get; private set; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x00067364 File Offset: 0x00065564
		// (set) Token: 0x0600162A RID: 5674 RVA: 0x0006736C File Offset: 0x0006556C
		internal WebServiceTrackingAuthority WSAuthority { get; private set; }

		// Token: 0x0600162B RID: 5675 RVA: 0x00067375 File Offset: 0x00065575
		internal WSGetParameters(MessageTrackingReportId reportId, WebServiceTrackingAuthority wsAuthority)
		{
			this.MessageTrackingReportId = reportId;
			this.WSAuthority = wsAuthority;
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0006738B File Offset: 0x0006558B
		public override string ToString()
		{
			return this.MessageTrackingReportId.ToString();
		}
	}
}
