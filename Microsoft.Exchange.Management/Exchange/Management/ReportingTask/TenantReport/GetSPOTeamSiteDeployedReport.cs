using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006EE RID: 1774
	[OutputType(new Type[]
	{
		typeof(SPOTeamSiteDeployedReport)
	})]
	[Cmdlet("Get", "SPOTeamSiteDeployedReport")]
	public sealed class GetSPOTeamSiteDeployedReport : TenantReportBase<SPOTeamSiteDeployedReport>
	{
		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06003E75 RID: 15989 RVA: 0x00104D49 File Offset: 0x00102F49
		// (set) Token: 0x06003E76 RID: 15990 RVA: 0x00104D60 File Offset: 0x00102F60
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

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06003E77 RID: 15991 RVA: 0x00104D78 File Offset: 0x00102F78
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x00104D80 File Offset: 0x00102F80
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x00104D90 File Offset: 0x00102F90
		private void ValidateReportType()
		{
			ReportType key = ReportType.Weekly;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetSPOTeamSiteDeployedReport.ReportTypeMapping[key];
		}

		// Token: 0x04002805 RID: 10245
		private const string ReportTypeKey = "ReportType";

		// Token: 0x04002806 RID: 10246
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Weekly,
				"dbo.SPOTeamSiteDeployedWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.SPOTeamSiteDeployedMonthly"
			}
		};

		// Token: 0x04002807 RID: 10247
		private string viewName;
	}
}
