using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006F5 RID: 1781
	[Table(Name = "dbo.MailboxActivityDaily")]
	[DataServiceKey("Date")]
	[Serializable]
	public class MailboxActivityReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06003EA3 RID: 16035 RVA: 0x00105030 File Offset: 0x00103230
		// (set) Token: 0x06003EA4 RID: 16036 RVA: 0x00105038 File Offset: 0x00103238
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06003EA5 RID: 16037 RVA: 0x00105041 File Offset: 0x00103241
		// (set) Token: 0x06003EA6 RID: 16038 RVA: 0x00105049 File Offset: 0x00103249
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x00105052 File Offset: 0x00103252
		// (set) Token: 0x06003EA8 RID: 16040 RVA: 0x0010505A File Offset: 0x0010325A
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x00105063 File Offset: 0x00103263
		// (set) Token: 0x06003EAA RID: 16042 RVA: 0x0010506B File Offset: 0x0010326B
		[Column(Name = "TotalActiveCount")]
		public long? TotalNumberOfActiveMailboxes { get; set; }

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06003EAB RID: 16043 RVA: 0x00105074 File Offset: 0x00103274
		// (set) Token: 0x06003EAC RID: 16044 RVA: 0x0010507C File Offset: 0x0010327C
		[Column(Name = "CreatedCount")]
		public long? AccountCreated { get; set; }

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06003EAD RID: 16045 RVA: 0x00105085 File Offset: 0x00103285
		// (set) Token: 0x06003EAE RID: 16046 RVA: 0x0010508D File Offset: 0x0010328D
		[Column(Name = "DeletedCount")]
		public long? AccountDeleted { get; set; }
	}
}
