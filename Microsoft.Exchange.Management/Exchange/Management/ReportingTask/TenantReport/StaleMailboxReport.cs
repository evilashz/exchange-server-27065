using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x02000707 RID: 1799
	[Table(Name = "dbo.StaleMailbox")]
	[DataServiceKey("Date")]
	[Serializable]
	public class StaleMailboxReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06003FE7 RID: 16359 RVA: 0x00105AE9 File Offset: 0x00103CE9
		// (set) Token: 0x06003FE8 RID: 16360 RVA: 0x00105AF1 File Offset: 0x00103CF1
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x06003FE9 RID: 16361 RVA: 0x00105AFA File Offset: 0x00103CFA
		// (set) Token: 0x06003FEA RID: 16362 RVA: 0x00105B02 File Offset: 0x00103D02
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x06003FEB RID: 16363 RVA: 0x00105B0B File Offset: 0x00103D0B
		// (set) Token: 0x06003FEC RID: 16364 RVA: 0x00105B13 File Offset: 0x00103D13
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x06003FED RID: 16365 RVA: 0x00105B1C File Offset: 0x00103D1C
		// (set) Token: 0x06003FEE RID: 16366 RVA: 0x00105B24 File Offset: 0x00103D24
		[Column(Name = "ActiveCount")]
		public long? ActiveMailboxes { get; set; }

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06003FEF RID: 16367 RVA: 0x00105B2D File Offset: 0x00103D2D
		// (set) Token: 0x06003FF0 RID: 16368 RVA: 0x00105B35 File Offset: 0x00103D35
		[Column(Name = "StaleTwoMonthCount")]
		public long? InactiveMailboxes31To60Days { get; set; }

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06003FF1 RID: 16369 RVA: 0x00105B3E File Offset: 0x00103D3E
		// (set) Token: 0x06003FF2 RID: 16370 RVA: 0x00105B46 File Offset: 0x00103D46
		[Column(Name = "StaleThreeMonthCount")]
		public long? InactiveMailboxes61To90Days { get; set; }

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06003FF3 RID: 16371 RVA: 0x00105B4F File Offset: 0x00103D4F
		// (set) Token: 0x06003FF4 RID: 16372 RVA: 0x00105B57 File Offset: 0x00103D57
		[Column(Name = "StaleOthersCount")]
		public long? InactiveMailboxes91To1460Days { get; set; }
	}
}
