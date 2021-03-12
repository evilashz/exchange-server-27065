using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006EC RID: 1772
	[Cmdlet("Get", "SPOSkyDriveProDeployedReport")]
	[OutputType(new Type[]
	{
		typeof(SPOSkyDriveProDeployedReport)
	})]
	public sealed class GetSPOSkyDriveProDeployedReport : TenantReportBase<SPOSkyDriveProDeployedReport>
	{
		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06003E67 RID: 15975 RVA: 0x00104BD1 File Offset: 0x00102DD1
		// (set) Token: 0x06003E68 RID: 15976 RVA: 0x00104BE8 File Offset: 0x00102DE8
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

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06003E69 RID: 15977 RVA: 0x00104C00 File Offset: 0x00102E00
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x00104C08 File Offset: 0x00102E08
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x00104C18 File Offset: 0x00102E18
		private void ValidateReportType()
		{
			ReportType key = ReportType.Weekly;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetSPOSkyDriveProDeployedReport.ReportTypeMapping[key];
		}

		// Token: 0x040027FF RID: 10239
		private const string ReportTypeKey = "ReportType";

		// Token: 0x04002800 RID: 10240
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Weekly,
				"dbo.SPOSkyDriveProDeployedWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.SPOSkyDriveProDeployedMonthly"
			}
		};

		// Token: 0x04002801 RID: 10241
		private string viewName;
	}
}
