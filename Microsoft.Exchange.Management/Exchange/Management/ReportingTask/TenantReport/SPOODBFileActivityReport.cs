using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006FF RID: 1791
	[Table(Name = "dbo.SPOODBFileActivity")]
	[DataServiceKey("Date")]
	[Serializable]
	public class SPOODBFileActivityReport : ScaledReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06003F57 RID: 16215 RVA: 0x00105625 File Offset: 0x00103825
		// (set) Token: 0x06003F58 RID: 16216 RVA: 0x0010562D File Offset: 0x0010382D
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06003F59 RID: 16217 RVA: 0x00105636 File Offset: 0x00103836
		// (set) Token: 0x06003F5A RID: 16218 RVA: 0x0010563E File Offset: 0x0010383E
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06003F5B RID: 16219 RVA: 0x00105647 File Offset: 0x00103847
		// (set) Token: 0x06003F5C RID: 16220 RVA: 0x0010564F File Offset: 0x0010384F
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06003F5D RID: 16221 RVA: 0x00105658 File Offset: 0x00103858
		// (set) Token: 0x06003F5E RID: 16222 RVA: 0x00105660 File Offset: 0x00103860
		[Column(Name = "UserPuid")]
		public string UserPuid { get; set; }

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06003F5F RID: 16223 RVA: 0x00105669 File Offset: 0x00103869
		// (set) Token: 0x06003F60 RID: 16224 RVA: 0x00105671 File Offset: 0x00103871
		[Column(Name = "DocumentId")]
		public Guid DocumentId { get; set; }

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x0010567A File Offset: 0x0010387A
		// (set) Token: 0x06003F62 RID: 16226 RVA: 0x00105682 File Offset: 0x00103882
		[Column(Name = "EventName")]
		public string EventName { get; set; }

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06003F63 RID: 16227 RVA: 0x0010568B File Offset: 0x0010388B
		// (set) Token: 0x06003F64 RID: 16228 RVA: 0x00105693 File Offset: 0x00103893
		[Column(Name = "UserDisplayName")]
		public string UserDisplayName { get; set; }

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06003F65 RID: 16229 RVA: 0x0010569C File Offset: 0x0010389C
		// (set) Token: 0x06003F66 RID: 16230 RVA: 0x001056A4 File Offset: 0x001038A4
		[Column(Name = "EmailAddress")]
		public string EmailAddress { get; set; }

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06003F67 RID: 16231 RVA: 0x001056AD File Offset: 0x001038AD
		// (set) Token: 0x06003F68 RID: 16232 RVA: 0x001056B5 File Offset: 0x001038B5
		[Column(Name = "IpAddress")]
		public string IpAddress { get; set; }

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06003F69 RID: 16233 RVA: 0x001056BE File Offset: 0x001038BE
		// (set) Token: 0x06003F6A RID: 16234 RVA: 0x001056C6 File Offset: 0x001038C6
		[Column(Name = "FileName")]
		public string DocumentName { get; set; }

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x06003F6B RID: 16235 RVA: 0x001056CF File Offset: 0x001038CF
		// (set) Token: 0x06003F6C RID: 16236 RVA: 0x001056D7 File Offset: 0x001038D7
		[Column(Name = "ParentFolderPath")]
		public string ParentFolderPath { get; set; }

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x06003F6D RID: 16237 RVA: 0x001056E0 File Offset: 0x001038E0
		// (set) Token: 0x06003F6E RID: 16238 RVA: 0x001056E8 File Offset: 0x001038E8
		[Column(Name = "ClientDevice")]
		public string ClientDevice { get; set; }

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x06003F6F RID: 16239 RVA: 0x001056F1 File Offset: 0x001038F1
		// (set) Token: 0x06003F70 RID: 16240 RVA: 0x001056F9 File Offset: 0x001038F9
		[Column(Name = "ClientOs")]
		public string ClientOs { get; set; }

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06003F71 RID: 16241 RVA: 0x00105702 File Offset: 0x00103902
		// (set) Token: 0x06003F72 RID: 16242 RVA: 0x0010570A File Offset: 0x0010390A
		[Column(Name = "ClientApplication")]
		public string ClientApplication { get; set; }

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x00105713 File Offset: 0x00103913
		// (set) Token: 0x06003F74 RID: 16244 RVA: 0x0010571B File Offset: 0x0010391B
		[Column(Name = "Details")]
		public string Details { get; set; }
	}
}
