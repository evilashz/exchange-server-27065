using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006CA RID: 1738
	[DataServiceKey("Date")]
	[Table(Name = "dbo.ExternalActivity")]
	[Serializable]
	public class ExternalActivityReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06003DB8 RID: 15800 RVA: 0x001033A5 File Offset: 0x001015A5
		// (set) Token: 0x06003DB9 RID: 15801 RVA: 0x001033AD File Offset: 0x001015AD
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06003DBA RID: 15802 RVA: 0x001033B6 File Offset: 0x001015B6
		// (set) Token: 0x06003DBB RID: 15803 RVA: 0x001033BE File Offset: 0x001015BE
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06003DBC RID: 15804 RVA: 0x001033C7 File Offset: 0x001015C7
		// (set) Token: 0x06003DBD RID: 15805 RVA: 0x001033CF File Offset: 0x001015CF
		[Column(Name = "SenderAddress")]
		public string SenderAddress { get; set; }

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06003DBE RID: 15806 RVA: 0x001033D8 File Offset: 0x001015D8
		// (set) Token: 0x06003DBF RID: 15807 RVA: 0x001033E0 File Offset: 0x001015E0
		[Column(Name = "RecipientDomain")]
		public string RecipientDomain { get; set; }

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06003DC0 RID: 15808 RVA: 0x001033E9 File Offset: 0x001015E9
		// (set) Token: 0x06003DC1 RID: 15809 RVA: 0x001033F1 File Offset: 0x001015F1
		[Column(Name = "Count")]
		public long? Count { get; set; }

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06003DC2 RID: 15810 RVA: 0x001033FA File Offset: 0x001015FA
		// (set) Token: 0x06003DC3 RID: 15811 RVA: 0x00103402 File Offset: 0x00101602
		[Column(Name = "Size")]
		public long? Size { get; set; }
	}
}
