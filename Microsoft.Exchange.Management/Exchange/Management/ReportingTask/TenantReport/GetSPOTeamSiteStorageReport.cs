using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006EF RID: 1775
	[OutputType(new Type[]
	{
		typeof(SPOTeamSiteStorageReport)
	})]
	[Cmdlet("Get", "SPOTeamSiteStorageReport")]
	public sealed class GetSPOTeamSiteStorageReport : TenantReportBase<SPOTeamSiteStorageReport>
	{
		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06003E7C RID: 15996 RVA: 0x00104E05 File Offset: 0x00103005
		// (set) Token: 0x06003E7D RID: 15997 RVA: 0x00104E1C File Offset: 0x0010301C
		[Parameter(Mandatory = false)]
		public ReportType ReportType
		{
			get
			{
				return (ReportType)base.Fields["ReportType"];
			}
			set
			{
				base.Fields["ReportType"] = value;
			}
		}

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06003E7E RID: 15998 RVA: 0x00104E34 File Offset: 0x00103034
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x00104E3C File Offset: 0x0010303C
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x00104E4C File Offset: 0x0010304C
		private void ValidateReportType()
		{
			ReportType key = ReportType.Weekly;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetSPOTeamSiteStorageReport.ReportTypeMapping[key];
		}

		// Token: 0x04002808 RID: 10248
		private const string ReportTypeKey = "ReportType";

		// Token: 0x04002809 RID: 10249
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Weekly,
				"dbo.SPOTeamSiteStorageWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.SPOTeamSiteStorageMonthly"
			}
		};

		// Token: 0x0400280A RID: 10250
		private string viewName;
	}
}
