using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x02000706 RID: 1798
	[DataServiceKey("Date")]
	[Table(Name = "dbo.StaleMailboxDetail")]
	[Serializable]
	public class StaleMailboxDetailReport : ScaledReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x00105A6A File Offset: 0x00103C6A
		// (set) Token: 0x06003FD9 RID: 16345 RVA: 0x00105A72 File Offset: 0x00103C72
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06003FDA RID: 16346 RVA: 0x00105A7B File Offset: 0x00103C7B
		// (set) Token: 0x06003FDB RID: 16347 RVA: 0x00105A83 File Offset: 0x00103C83
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06003FDC RID: 16348 RVA: 0x00105A8C File Offset: 0x00103C8C
		// (set) Token: 0x06003FDD RID: 16349 RVA: 0x00105A94 File Offset: 0x00103C94
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06003FDE RID: 16350 RVA: 0x00105A9D File Offset: 0x00103C9D
		// (set) Token: 0x06003FDF RID: 16351 RVA: 0x00105AA5 File Offset: 0x00103CA5
		[Column(Name = "WindowsLiveID")]
		[SuppressPii(PiiDataType = PiiDataType.Smtp)]
		public string WindowsLiveID { get; set; }

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x00105AAE File Offset: 0x00103CAE
		// (set) Token: 0x06003FE1 RID: 16353 RVA: 0x00105AB6 File Offset: 0x00103CB6
		[SuppressPii(PiiDataType = PiiDataType.String)]
		[Column(Name = "UserName")]
		public string UserName { get; set; }

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x00105ABF File Offset: 0x00103CBF
		// (set) Token: 0x06003FE3 RID: 16355 RVA: 0x00105AC7 File Offset: 0x00103CC7
		[Column(Name = "LastLogin")]
		public DateTime? LastLogin { get; set; }

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06003FE4 RID: 16356 RVA: 0x00105AD0 File Offset: 0x00103CD0
		// (set) Token: 0x06003FE5 RID: 16357 RVA: 0x00105AD8 File Offset: 0x00103CD8
		[Column(Name = "DaysInactive")]
		public int? DaysInactive { get; set; }
	}
}
