using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006D3 RID: 1747
	[OutputType(new Type[]
	{
		typeof(CsActiveUserReport)
	})]
	[Cmdlet("Get", "CsActiveUserReport")]
	public sealed class GetCsActiveUserReport : TenantReportBase<CsActiveUserReport>
	{
		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06003DF8 RID: 15864 RVA: 0x00103F55 File Offset: 0x00102155
		// (set) Token: 0x06003DF9 RID: 15865 RVA: 0x00103F6C File Offset: 0x0010216C
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

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06003DFA RID: 15866 RVA: 0x00103F84 File Offset: 0x00102184
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x00103F8C File Offset: 0x0010218C
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x00103F9C File Offset: 0x0010219C
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetCsActiveUserReport.ReportTypeMapping[key];
		}

		// Token: 0x040027D8 RID: 10200
		private const string ReportTypeKey = "ReportType";

		// Token: 0x040027D9 RID: 10201
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.CsActiveUserDaily"
			},
			{
				ReportType.Weekly,
				"dbo.CsActiveUserWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.CsActiveUserMonthly"
			},
			{
				ReportType.Yearly,
				"dbo.CsActiveUserYearly"
			}
		};

		// Token: 0x040027DA RID: 10202
		private string viewName;
	}
}
