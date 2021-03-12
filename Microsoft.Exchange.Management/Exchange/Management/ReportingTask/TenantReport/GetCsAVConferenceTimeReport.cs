using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006D4 RID: 1748
	[Cmdlet("Get", "CsAVConferenceTimeReport")]
	[OutputType(new Type[]
	{
		typeof(CsAVConferenceTimeReport)
	})]
	public sealed class GetCsAVConferenceTimeReport : TenantReportBase<CsAVConferenceTimeReport>
	{
		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06003DFF RID: 15871 RVA: 0x0010401D File Offset: 0x0010221D
		// (set) Token: 0x06003E00 RID: 15872 RVA: 0x00104034 File Offset: 0x00102234
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

		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06003E01 RID: 15873 RVA: 0x0010404C File Offset: 0x0010224C
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x00104054 File Offset: 0x00102254
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x00104064 File Offset: 0x00102264
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetCsAVConferenceTimeReport.ReportTypeMapping[key];
		}

		// Token: 0x040027DB RID: 10203
		private const string ReportTypeKey = "ReportType";

		// Token: 0x040027DC RID: 10204
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.CsAVConferenceTimeDaily"
			},
			{
				ReportType.Weekly,
				"dbo.CsAVConferenceTimeWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.CsAVConferenceTimeMonthly"
			}
		};

		// Token: 0x040027DD RID: 10205
		private string viewName;
	}
}
