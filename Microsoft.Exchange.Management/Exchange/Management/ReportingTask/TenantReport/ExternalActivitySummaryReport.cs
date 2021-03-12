using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006CB RID: 1739
	[DataServiceKey("Date")]
	[Table(Name = "dbo.ExternalDomainSummary")]
	[Serializable]
	public class ExternalActivitySummaryReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06003DC5 RID: 15813 RVA: 0x00103413 File Offset: 0x00101613
		// (set) Token: 0x06003DC6 RID: 15814 RVA: 0x0010341B File Offset: 0x0010161B
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06003DC7 RID: 15815 RVA: 0x00103424 File Offset: 0x00101624
		// (set) Token: 0x06003DC8 RID: 15816 RVA: 0x0010342C File Offset: 0x0010162C
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06003DC9 RID: 15817 RVA: 0x00103435 File Offset: 0x00101635
		// (set) Token: 0x06003DCA RID: 15818 RVA: 0x0010343D File Offset: 0x0010163D
		[Column(Name = "Count")]
		public long? Count { get; set; }
	}
}
