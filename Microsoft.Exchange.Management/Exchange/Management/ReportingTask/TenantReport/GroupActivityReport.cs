using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006F3 RID: 1779
	[DataServiceKey("Date")]
	[Table(Name = "dbo.GroupActivityDaily")]
	[Serializable]
	public class GroupActivityReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06003E8B RID: 16011 RVA: 0x00104F65 File Offset: 0x00103165
		// (set) Token: 0x06003E8C RID: 16012 RVA: 0x00104F6D File Offset: 0x0010316D
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06003E8D RID: 16013 RVA: 0x00104F76 File Offset: 0x00103176
		// (set) Token: 0x06003E8E RID: 16014 RVA: 0x00104F7E File Offset: 0x0010317E
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06003E8F RID: 16015 RVA: 0x00104F87 File Offset: 0x00103187
		// (set) Token: 0x06003E90 RID: 16016 RVA: 0x00104F8F File Offset: 0x0010318F
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06003E91 RID: 16017 RVA: 0x00104F98 File Offset: 0x00103198
		// (set) Token: 0x06003E92 RID: 16018 RVA: 0x00104FA0 File Offset: 0x001031A0
		[Column(Name = "CreatedCount")]
		public long? GroupCreated { get; set; }

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06003E93 RID: 16019 RVA: 0x00104FA9 File Offset: 0x001031A9
		// (set) Token: 0x06003E94 RID: 16020 RVA: 0x00104FB1 File Offset: 0x001031B1
		[Column(Name = "DeletedCount")]
		public long? GroupDeleted { get; set; }
	}
}
