using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x02000700 RID: 1792
	[Table(Name = "dbo.SPOODBUserStatistics")]
	[DataServiceKey("Date")]
	[Serializable]
	public class SPOODBUserStatisticsReport : ScaledReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x06003F76 RID: 16246 RVA: 0x0010572C File Offset: 0x0010392C
		// (set) Token: 0x06003F77 RID: 16247 RVA: 0x00105734 File Offset: 0x00103934
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06003F78 RID: 16248 RVA: 0x0010573D File Offset: 0x0010393D
		// (set) Token: 0x06003F79 RID: 16249 RVA: 0x00105745 File Offset: 0x00103945
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06003F7A RID: 16250 RVA: 0x0010574E File Offset: 0x0010394E
		// (set) Token: 0x06003F7B RID: 16251 RVA: 0x00105756 File Offset: 0x00103956
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x0010575F File Offset: 0x0010395F
		// (set) Token: 0x06003F7D RID: 16253 RVA: 0x00105767 File Offset: 0x00103967
		[Column(Name = "UserPuid")]
		public string UserPuid { get; set; }

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06003F7E RID: 16254 RVA: 0x00105770 File Offset: 0x00103970
		// (set) Token: 0x06003F7F RID: 16255 RVA: 0x00105778 File Offset: 0x00103978
		[Column(Name = "UserDisplayName")]
		public string UserDisplayName { get; set; }

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06003F80 RID: 16256 RVA: 0x00105781 File Offset: 0x00103981
		// (set) Token: 0x06003F81 RID: 16257 RVA: 0x00105789 File Offset: 0x00103989
		[Column(Name = "EmailAddress")]
		public string EmailAddress { get; set; }

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x06003F82 RID: 16258 RVA: 0x00105792 File Offset: 0x00103992
		// (set) Token: 0x06003F83 RID: 16259 RVA: 0x0010579A File Offset: 0x0010399A
		[Column(Name = "FileDownloaded")]
		public int FileDownloaded { get; set; }

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x001057A3 File Offset: 0x001039A3
		// (set) Token: 0x06003F85 RID: 16261 RVA: 0x001057AB File Offset: 0x001039AB
		[Column(Name = "FileViewed")]
		public int FileViewed { get; set; }

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x001057B4 File Offset: 0x001039B4
		// (set) Token: 0x06003F87 RID: 16263 RVA: 0x001057BC File Offset: 0x001039BC
		[Column(Name = "FileModified")]
		public int FileModified { get; set; }

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x001057C5 File Offset: 0x001039C5
		// (set) Token: 0x06003F89 RID: 16265 RVA: 0x001057CD File Offset: 0x001039CD
		[Column(Name = "FileUploaded")]
		public int FileUploaded { get; set; }

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x001057D6 File Offset: 0x001039D6
		// (set) Token: 0x06003F8B RID: 16267 RVA: 0x001057DE File Offset: 0x001039DE
		[Column(Name = "FileCheckedOut")]
		public int FileCheckedOut { get; set; }

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x06003F8C RID: 16268 RVA: 0x001057E7 File Offset: 0x001039E7
		// (set) Token: 0x06003F8D RID: 16269 RVA: 0x001057EF File Offset: 0x001039EF
		[Column(Name = "FileCheckedIn")]
		public int FileCheckedIn { get; set; }

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x06003F8E RID: 16270 RVA: 0x001057F8 File Offset: 0x001039F8
		// (set) Token: 0x06003F8F RID: 16271 RVA: 0x00105800 File Offset: 0x00103A00
		[Column(Name = "FileCheckOutDiscarded")]
		public int FileCheckOutDiscarded { get; set; }

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06003F90 RID: 16272 RVA: 0x00105809 File Offset: 0x00103A09
		// (set) Token: 0x06003F91 RID: 16273 RVA: 0x00105811 File Offset: 0x00103A11
		[Column(Name = "FileMoved")]
		public int FileMoved { get; set; }

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06003F92 RID: 16274 RVA: 0x0010581A File Offset: 0x00103A1A
		// (set) Token: 0x06003F93 RID: 16275 RVA: 0x00105822 File Offset: 0x00103A22
		[Column(Name = "FileCopied")]
		public int FileCopied { get; set; }
	}
}
