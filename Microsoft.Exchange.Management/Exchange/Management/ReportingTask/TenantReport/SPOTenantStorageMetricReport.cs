using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x02000705 RID: 1797
	[DataServiceKey("Date")]
	[Table(Name = "dbo.SPOTenantStorageMetricDaily")]
	[Serializable]
	public class SPOTenantStorageMetricReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06003FC9 RID: 16329 RVA: 0x001059EB File Offset: 0x00103BEB
		// (set) Token: 0x06003FCA RID: 16330 RVA: 0x001059F3 File Offset: 0x00103BF3
		[Column(Name = "ID")]
		public long ID { get; set; }

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06003FCB RID: 16331 RVA: 0x001059FC File Offset: 0x00103BFC
		// (set) Token: 0x06003FCC RID: 16332 RVA: 0x00105A04 File Offset: 0x00103C04
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06003FCD RID: 16333 RVA: 0x00105A0D File Offset: 0x00103C0D
		// (set) Token: 0x06003FCE RID: 16334 RVA: 0x00105A15 File Offset: 0x00103C15
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06003FCF RID: 16335 RVA: 0x00105A1E File Offset: 0x00103C1E
		// (set) Token: 0x06003FD0 RID: 16336 RVA: 0x00105A26 File Offset: 0x00103C26
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06003FD1 RID: 16337 RVA: 0x00105A2F File Offset: 0x00103C2F
		// (set) Token: 0x06003FD2 RID: 16338 RVA: 0x00105A37 File Offset: 0x00103C37
		[Column(Name = "Used")]
		public long? Used { get; set; }

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x00105A40 File Offset: 0x00103C40
		// (set) Token: 0x06003FD4 RID: 16340 RVA: 0x00105A48 File Offset: 0x00103C48
		[Column(Name = "Allocated")]
		public long? Allocated { get; set; }

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06003FD5 RID: 16341 RVA: 0x00105A51 File Offset: 0x00103C51
		// (set) Token: 0x06003FD6 RID: 16342 RVA: 0x00105A59 File Offset: 0x00103C59
		[Column(Name = "Total")]
		public long? Total { get; set; }
	}
}
