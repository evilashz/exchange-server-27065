using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006C5 RID: 1733
	[DataServiceKey("Date")]
	[Table(Name = "dbo.CsConferenceDaily")]
	[Serializable]
	public class CsConferenceReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06003D6D RID: 15725 RVA: 0x0010312A File Offset: 0x0010132A
		// (set) Token: 0x06003D6E RID: 15726 RVA: 0x00103132 File Offset: 0x00101332
		[Column(Name = "ID")]
		public long ID { get; set; }

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06003D6F RID: 15727 RVA: 0x0010313B File Offset: 0x0010133B
		// (set) Token: 0x06003D70 RID: 15728 RVA: 0x00103143 File Offset: 0x00101343
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06003D71 RID: 15729 RVA: 0x0010314C File Offset: 0x0010134C
		// (set) Token: 0x06003D72 RID: 15730 RVA: 0x00103154 File Offset: 0x00101354
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06003D73 RID: 15731 RVA: 0x0010315D File Offset: 0x0010135D
		// (set) Token: 0x06003D74 RID: 15732 RVA: 0x00103165 File Offset: 0x00101365
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06003D75 RID: 15733 RVA: 0x0010316E File Offset: 0x0010136E
		// (set) Token: 0x06003D76 RID: 15734 RVA: 0x00103176 File Offset: 0x00101376
		[Column(Name = "TotalConferences")]
		public long? TotalConferences { get; set; }

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06003D77 RID: 15735 RVA: 0x0010317F File Offset: 0x0010137F
		// (set) Token: 0x06003D78 RID: 15736 RVA: 0x00103187 File Offset: 0x00101387
		[Column(Name = "AVConferences")]
		public long? AVConferences { get; set; }

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06003D79 RID: 15737 RVA: 0x00103190 File Offset: 0x00101390
		// (set) Token: 0x06003D7A RID: 15738 RVA: 0x00103198 File Offset: 0x00101398
		[Column(Name = "IMConferences")]
		public long? IMConferences { get; set; }

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06003D7B RID: 15739 RVA: 0x001031A1 File Offset: 0x001013A1
		// (set) Token: 0x06003D7C RID: 15740 RVA: 0x001031A9 File Offset: 0x001013A9
		[Column(Name = "ApplicationSharingConferences")]
		public long? ApplicationSharingConferences { get; set; }

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06003D7D RID: 15741 RVA: 0x001031B2 File Offset: 0x001013B2
		// (set) Token: 0x06003D7E RID: 15742 RVA: 0x001031BA File Offset: 0x001013BA
		[Column(Name = "WebConferences")]
		public long? WebConferences { get; set; }

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06003D7F RID: 15743 RVA: 0x001031C3 File Offset: 0x001013C3
		// (set) Token: 0x06003D80 RID: 15744 RVA: 0x001031CB File Offset: 0x001013CB
		[Column(Name = "TelephonyConferences")]
		public long? TelephonyConferences { get; set; }
	}
}
