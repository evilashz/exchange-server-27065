using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006E9 RID: 1769
	[Cmdlet("Get", "SPOActiveUserReport")]
	[OutputType(new Type[]
	{
		typeof(SPOActiveUserReport)
	})]
	public sealed class GetSPOActiveUserReport : TenantReportBase<SPOActiveUserReport>
	{
		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06003E5E RID: 15966 RVA: 0x00104B05 File Offset: 0x00102D05
		// (set) Token: 0x06003E5F RID: 15967 RVA: 0x00104B1C File Offset: 0x00102D1C
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

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06003E60 RID: 15968 RVA: 0x00104B34 File Offset: 0x00102D34
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x00104B3C File Offset: 0x00102D3C
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x00104B4C File Offset: 0x00102D4C
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetSPOActiveUserReport.ReportTypeMapping[key];
		}

		// Token: 0x040027FC RID: 10236
		private const string ReportTypeKey = "ReportType";

		// Token: 0x040027FD RID: 10237
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.SPOActiveUserDaily"
			},
			{
				ReportType.Weekly,
				"dbo.SPOActiveUserWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.SPOActiveUserMonthly"
			}
		};

		// Token: 0x040027FE RID: 10238
		private string viewName;
	}
}
