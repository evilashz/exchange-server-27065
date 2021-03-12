using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006C3 RID: 1731
	[Table(Name = "dbo.CsAVConferenceTimeDaily")]
	[DataServiceKey("Date")]
	[Serializable]
	public class CsAVConferenceTimeReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06003D4F RID: 15695 RVA: 0x0010302C File Offset: 0x0010122C
		// (set) Token: 0x06003D50 RID: 15696 RVA: 0x00103034 File Offset: 0x00101234
		[Column(Name = "ID")]
		public long ID { get; set; }

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06003D51 RID: 15697 RVA: 0x0010303D File Offset: 0x0010123D
		// (set) Token: 0x06003D52 RID: 15698 RVA: 0x00103045 File Offset: 0x00101245
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06003D53 RID: 15699 RVA: 0x0010304E File Offset: 0x0010124E
		// (set) Token: 0x06003D54 RID: 15700 RVA: 0x00103056 File Offset: 0x00101256
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06003D55 RID: 15701 RVA: 0x0010305F File Offset: 0x0010125F
		// (set) Token: 0x06003D56 RID: 15702 RVA: 0x00103067 File Offset: 0x00101267
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06003D57 RID: 15703 RVA: 0x00103070 File Offset: 0x00101270
		// (set) Token: 0x06003D58 RID: 15704 RVA: 0x00103078 File Offset: 0x00101278
		[Column(Name = "AVConferenceMinutes")]
		public long? AVConferenceMinutes { get; set; }
	}
}
