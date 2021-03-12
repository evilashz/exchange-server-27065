using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006C9 RID: 1737
	[DataServiceKey("Date")]
	[Table(Name = "dbo.ExternalActivityByUser")]
	[Serializable]
	public class ExternalActivityByUserReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06003DAD RID: 15789 RVA: 0x00103348 File Offset: 0x00101548
		// (set) Token: 0x06003DAE RID: 15790 RVA: 0x00103350 File Offset: 0x00101550
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06003DAF RID: 15791 RVA: 0x00103359 File Offset: 0x00101559
		// (set) Token: 0x06003DB0 RID: 15792 RVA: 0x00103361 File Offset: 0x00101561
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06003DB1 RID: 15793 RVA: 0x0010336A File Offset: 0x0010156A
		// (set) Token: 0x06003DB2 RID: 15794 RVA: 0x00103372 File Offset: 0x00101572
		[Column(Name = "SenderAddress")]
		public string SenderAddress { get; set; }

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06003DB3 RID: 15795 RVA: 0x0010337B File Offset: 0x0010157B
		// (set) Token: 0x06003DB4 RID: 15796 RVA: 0x00103383 File Offset: 0x00101583
		[Column(Name = "Count")]
		public long? Count { get; set; }

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06003DB5 RID: 15797 RVA: 0x0010338C File Offset: 0x0010158C
		// (set) Token: 0x06003DB6 RID: 15798 RVA: 0x00103394 File Offset: 0x00101594
		[Column(Name = "Size")]
		public long? Size { get; set; }
	}
}
