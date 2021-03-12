using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006F7 RID: 1783
	[Table(Name = "dbo.MailboxUsage")]
	[DataServiceKey("Date")]
	[Serializable]
	public class MailboxUsageReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x00105172 File Offset: 0x00103372
		// (set) Token: 0x06003ECA RID: 16074 RVA: 0x0010517A File Offset: 0x0010337A
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x00105183 File Offset: 0x00103383
		// (set) Token: 0x06003ECC RID: 16076 RVA: 0x0010518B File Offset: 0x0010338B
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06003ECD RID: 16077 RVA: 0x00105194 File Offset: 0x00103394
		// (set) Token: 0x06003ECE RID: 16078 RVA: 0x0010519C File Offset: 0x0010339C
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06003ECF RID: 16079 RVA: 0x001051A5 File Offset: 0x001033A5
		// (set) Token: 0x06003ED0 RID: 16080 RVA: 0x001051AD File Offset: 0x001033AD
		[Column(Name = "TotalMailboxCount")]
		public long? TotalMailboxCount { get; set; }

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06003ED1 RID: 16081 RVA: 0x001051B6 File Offset: 0x001033B6
		// (set) Token: 0x06003ED2 RID: 16082 RVA: 0x001051BE File Offset: 0x001033BE
		[Column(Name = "TotalInactiveMailboxCount")]
		public long? TotalInactiveMailboxCount { get; set; }

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06003ED3 RID: 16083 RVA: 0x001051C7 File Offset: 0x001033C7
		// (set) Token: 0x06003ED4 RID: 16084 RVA: 0x001051CF File Offset: 0x001033CF
		[Column(Name = "MailboxesOverWarningSize")]
		public long? MailboxesOverWarningSize { get; set; }

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06003ED5 RID: 16085 RVA: 0x001051D8 File Offset: 0x001033D8
		// (set) Token: 0x06003ED6 RID: 16086 RVA: 0x001051E0 File Offset: 0x001033E0
		[Column(Name = "MailboxesUsedLessthan25Percent")]
		public long? MailboxesUsedLessthan25Percent { get; set; }
	}
}
