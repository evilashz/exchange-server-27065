using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa
{
	// Token: 0x020007D9 RID: 2009
	internal class OwaLoginParameters
	{
		// Token: 0x060029A4 RID: 10660 RVA: 0x00059916 File Offset: 0x00057B16
		public OwaLoginParameters()
		{
			this.ShouldDownloadStaticFile = false;
			this.ShouldDownloadStaticFileOnLogonPage = false;
			this.ShouldMeasureClientLatency = true;
			this.CafeOutboundRequestTimeout = TimeSpan.FromSeconds(100.0);
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x00059947 File Offset: 0x00057B47
		// (set) Token: 0x060029A6 RID: 10662 RVA: 0x0005994F File Offset: 0x00057B4F
		public bool ShouldDownloadStaticFile { get; set; }

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060029A7 RID: 10663 RVA: 0x00059958 File Offset: 0x00057B58
		// (set) Token: 0x060029A8 RID: 10664 RVA: 0x00059960 File Offset: 0x00057B60
		public bool ShouldDownloadStaticFileOnLogonPage { get; set; }

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060029A9 RID: 10665 RVA: 0x00059969 File Offset: 0x00057B69
		// (set) Token: 0x060029AA RID: 10666 RVA: 0x00059971 File Offset: 0x00057B71
		public bool ShouldMeasureClientLatency { get; set; }

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060029AB RID: 10667 RVA: 0x0005997A File Offset: 0x00057B7A
		// (set) Token: 0x060029AC RID: 10668 RVA: 0x00059982 File Offset: 0x00057B82
		public TimeSpan CafeOutboundRequestTimeout { get; set; }
	}
}
