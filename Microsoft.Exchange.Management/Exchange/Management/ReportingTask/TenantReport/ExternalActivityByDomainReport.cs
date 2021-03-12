using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006C8 RID: 1736
	[DataServiceKey("Date")]
	[Table(Name = "dbo.ExternalActivityByDomain")]
	[Serializable]
	public class ExternalActivityByDomainReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06003DA4 RID: 15780 RVA: 0x001032FC File Offset: 0x001014FC
		// (set) Token: 0x06003DA5 RID: 15781 RVA: 0x00103304 File Offset: 0x00101504
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06003DA6 RID: 15782 RVA: 0x0010330D File Offset: 0x0010150D
		// (set) Token: 0x06003DA7 RID: 15783 RVA: 0x00103315 File Offset: 0x00101515
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06003DA8 RID: 15784 RVA: 0x0010331E File Offset: 0x0010151E
		// (set) Token: 0x06003DA9 RID: 15785 RVA: 0x00103326 File Offset: 0x00101526
		[Column(Name = "RecipientDomain")]
		public string RecipientDomain { get; set; }

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06003DAA RID: 15786 RVA: 0x0010332F File Offset: 0x0010152F
		// (set) Token: 0x06003DAB RID: 15787 RVA: 0x00103337 File Offset: 0x00101537
		[Column(Name = "Count")]
		public long? Count { get; set; }
	}
}
