using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x02000702 RID: 1794
	[DataServiceKey("Date")]
	[Table(Name = "dbo.SPOSkyDriveProStorageWeekly")]
	[Serializable]
	public class SPOSkyDriveProStorageReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06003FA2 RID: 16290 RVA: 0x001058A1 File Offset: 0x00103AA1
		// (set) Token: 0x06003FA3 RID: 16291 RVA: 0x001058A9 File Offset: 0x00103AA9
		[Column(Name = "ID")]
		public long ID { get; set; }

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06003FA4 RID: 16292 RVA: 0x001058B2 File Offset: 0x00103AB2
		// (set) Token: 0x06003FA5 RID: 16293 RVA: 0x001058BA File Offset: 0x00103ABA
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06003FA6 RID: 16294 RVA: 0x001058C3 File Offset: 0x00103AC3
		// (set) Token: 0x06003FA7 RID: 16295 RVA: 0x001058CB File Offset: 0x00103ACB
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06003FA8 RID: 16296 RVA: 0x001058D4 File Offset: 0x00103AD4
		// (set) Token: 0x06003FA9 RID: 16297 RVA: 0x001058DC File Offset: 0x00103ADC
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x06003FAA RID: 16298 RVA: 0x001058E5 File Offset: 0x00103AE5
		// (set) Token: 0x06003FAB RID: 16299 RVA: 0x001058ED File Offset: 0x00103AED
		[Column(Name = "Used")]
		public long? Used { get; set; }

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06003FAC RID: 16300 RVA: 0x001058F6 File Offset: 0x00103AF6
		// (set) Token: 0x06003FAD RID: 16301 RVA: 0x001058FE File Offset: 0x00103AFE
		[Column(Name = "Allocated")]
		public long? Allocated { get; set; }
	}
}
