using System;
using System.Data.Linq.Mapping;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000029 RID: 41
	[Table]
	public class WorkDefinitionOverride
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000AAE7 File Offset: 0x00008CE7
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000AAEF File Offset: 0x00008CEF
		[Column(IsPrimaryKey = true)]
		public string WorkDefinitionName { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000AB00 File Offset: 0x00008D00
		[Column]
		public int AggregationLevel { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000AB09 File Offset: 0x00008D09
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0000AB11 File Offset: 0x00008D11
		[Column(IsPrimaryKey = true)]
		public string Scope { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000AB1A File Offset: 0x00008D1A
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0000AB22 File Offset: 0x00008D22
		[Column]
		public DateTime CreatedTime { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000AB2B File Offset: 0x00008D2B
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000AB33 File Offset: 0x00008D33
		[Column]
		public DateTime UpdatedTime { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000AB3C File Offset: 0x00008D3C
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000AB44 File Offset: 0x00008D44
		[Column]
		public DateTime ExpirationDate { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000AB4D File Offset: 0x00008D4D
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000AB55 File Offset: 0x00008D55
		[Column]
		public string ServiceName { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000AB5E File Offset: 0x00008D5E
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000AB66 File Offset: 0x00008D66
		[Column(IsPrimaryKey = true)]
		public string PropertyName { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000AB6F File Offset: 0x00008D6F
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000AB77 File Offset: 0x00008D77
		[Column]
		public string NewPropertyValue { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000AB80 File Offset: 0x00008D80
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000AB88 File Offset: 0x00008D88
		[Column]
		public string CreatedBy { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000AB91 File Offset: 0x00008D91
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000AB99 File Offset: 0x00008D99
		[Column]
		public string UpdatedBy { get; set; }

		// Token: 0x060002F0 RID: 752 RVA: 0x0000ABA4 File Offset: 0x00008DA4
		public string GetIdentityString()
		{
			if (string.IsNullOrWhiteSpace(this.Scope))
			{
				return string.Format("{0}~{1}~{2}", this.ServiceName, this.WorkDefinitionName, this.PropertyName);
			}
			return string.Format("{0}~{1}~{2}~{3}", new object[]
			{
				this.ServiceName,
				this.WorkDefinitionName,
				this.PropertyName,
				this.Scope
			});
		}
	}
}
