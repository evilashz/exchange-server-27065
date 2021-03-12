using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006D7 RID: 1751
	[OutputType(new Type[]
	{
		typeof(CsP2PAVTimeReport)
	})]
	[Cmdlet("Get", "CsP2PAVTimeReport")]
	public sealed class GetCsP2PAVTimeReport : TenantReportBase<CsP2PAVTimeReport>
	{
		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06003E0E RID: 15886 RVA: 0x001041B5 File Offset: 0x001023B5
		// (set) Token: 0x06003E0F RID: 15887 RVA: 0x001041CC File Offset: 0x001023CC
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

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06003E10 RID: 15888 RVA: 0x001041E4 File Offset: 0x001023E4
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x001041EC File Offset: 0x001023EC
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x001041FC File Offset: 0x001023FC
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetCsP2PAVTimeReport.ReportTypeMapping[key];
		}

		// Token: 0x040027E1 RID: 10209
		private const string ReportTypeKey = "ReportType";

		// Token: 0x040027E2 RID: 10210
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.CsP2PAVTimeDaily"
			},
			{
				ReportType.Weekly,
				"dbo.CsP2PAVTimeWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.CsP2PAVTimeMonthly"
			}
		};

		// Token: 0x040027E3 RID: 10211
		private string viewName;
	}
}
