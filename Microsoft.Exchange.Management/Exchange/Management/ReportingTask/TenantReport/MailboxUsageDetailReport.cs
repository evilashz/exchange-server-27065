using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006F6 RID: 1782
	[DataServiceKey("Date")]
	[Table(Name = "dbo.MailboxUsageDetail")]
	[Serializable]
	public class MailboxUsageDetailReport : ScaledReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06003EB0 RID: 16048 RVA: 0x0010509E File Offset: 0x0010329E
		// (set) Token: 0x06003EB1 RID: 16049 RVA: 0x001050A6 File Offset: 0x001032A6
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x001050AF File Offset: 0x001032AF
		// (set) Token: 0x06003EB3 RID: 16051 RVA: 0x001050B7 File Offset: 0x001032B7
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x001050C0 File Offset: 0x001032C0
		// (set) Token: 0x06003EB5 RID: 16053 RVA: 0x001050C8 File Offset: 0x001032C8
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06003EB6 RID: 16054 RVA: 0x001050D1 File Offset: 0x001032D1
		// (set) Token: 0x06003EB7 RID: 16055 RVA: 0x001050D9 File Offset: 0x001032D9
		[SuppressPii(PiiDataType = PiiDataType.Smtp)]
		[Column(Name = "WindowsLiveID")]
		public string WindowsLiveID { get; set; }

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x001050E2 File Offset: 0x001032E2
		// (set) Token: 0x06003EB9 RID: 16057 RVA: 0x001050EA File Offset: 0x001032EA
		[Column(Name = "UserName")]
		[SuppressPii(PiiDataType = PiiDataType.String)]
		public string UserName { get; set; }

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06003EBA RID: 16058 RVA: 0x001050F3 File Offset: 0x001032F3
		// (set) Token: 0x06003EBB RID: 16059 RVA: 0x001050FB File Offset: 0x001032FB
		[Column(Name = "MailboxSize")]
		public long? MailboxSize { get; set; }

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06003EBC RID: 16060 RVA: 0x00105104 File Offset: 0x00103304
		// (set) Token: 0x06003EBD RID: 16061 RVA: 0x0010510C File Offset: 0x0010330C
		[Column(Name = "CurrentMailboxSize")]
		public long? CurrentMailboxSize { get; set; }

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06003EBE RID: 16062 RVA: 0x00105115 File Offset: 0x00103315
		// (set) Token: 0x06003EBF RID: 16063 RVA: 0x0010511D File Offset: 0x0010331D
		[Column(Name = "PercentUsed")]
		public long? PercentUsed { get; set; }

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x00105126 File Offset: 0x00103326
		// (set) Token: 0x06003EC1 RID: 16065 RVA: 0x0010512E File Offset: 0x0010332E
		[Column(Name = "MailboxPlan")]
		public string MailboxPlan { get; set; }

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x00105137 File Offset: 0x00103337
		// (set) Token: 0x06003EC3 RID: 16067 RVA: 0x0010513F File Offset: 0x0010333F
		[Column(Name = "IsInactive")]
		public bool IsInactive { get; set; }

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x00105148 File Offset: 0x00103348
		// (set) Token: 0x06003EC5 RID: 16069 RVA: 0x00105150 File Offset: 0x00103350
		[Column(Name = "IssueWarningQuota")]
		public long? IssueWarningQuota { get; set; }

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x00105159 File Offset: 0x00103359
		// (set) Token: 0x06003EC7 RID: 16071 RVA: 0x00105161 File Offset: 0x00103361
		[Column(Name = "IsOverWarningQuota")]
		public bool IsOverWarningQuota { get; set; }
	}
}
