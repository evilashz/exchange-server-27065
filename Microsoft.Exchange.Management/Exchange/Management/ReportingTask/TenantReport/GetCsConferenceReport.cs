using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006D6 RID: 1750
	[OutputType(new Type[]
	{
		typeof(CsConferenceReport)
	})]
	[Cmdlet("Get", "CsConferenceReport")]
	public sealed class GetCsConferenceReport : TenantReportBase<CsConferenceReport>
	{
		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06003E07 RID: 15879 RVA: 0x001040ED File Offset: 0x001022ED
		// (set) Token: 0x06003E08 RID: 15880 RVA: 0x00104104 File Offset: 0x00102304
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

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06003E09 RID: 15881 RVA: 0x0010411C File Offset: 0x0010231C
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x00104124 File Offset: 0x00102324
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x00104134 File Offset: 0x00102334
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetCsConferenceReport.ReportTypeMapping[key];
		}

		// Token: 0x040027DE RID: 10206
		private const string ReportTypeKey = "ReportType";

		// Token: 0x040027DF RID: 10207
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.CsConferenceDaily"
			},
			{
				ReportType.Weekly,
				"dbo.CsConferenceWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.CsConferenceMonthly"
			}
		};

		// Token: 0x040027E0 RID: 10208
		private string viewName;
	}
}
