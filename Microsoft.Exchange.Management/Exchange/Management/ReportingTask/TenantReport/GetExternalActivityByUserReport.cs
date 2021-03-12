using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006DA RID: 1754
	[OutputType(new Type[]
	{
		typeof(ExternalActivityByUserReport)
	})]
	[Cmdlet("Get", "ExternalActivityByUserReport")]
	public sealed class GetExternalActivityByUserReport : TenantReportBase<ExternalActivityByUserReport>
	{
		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06003E24 RID: 15908 RVA: 0x00104411 File Offset: 0x00102611
		protected override DataMartType DataMartType
		{
			get
			{
				return DataMartType.TenantSecurity;
			}
		}

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x06003E25 RID: 15909 RVA: 0x00104415 File Offset: 0x00102615
		// (set) Token: 0x06003E26 RID: 15910 RVA: 0x0010442C File Offset: 0x0010262C
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

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x06003E27 RID: 15911 RVA: 0x00104444 File Offset: 0x00102644
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x0010444C File Offset: 0x0010264C
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x0010445C File Offset: 0x0010265C
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetExternalActivityByUserReport.ReportTypeMapping[key];
		}

		// Token: 0x040027EA RID: 10218
		private const string ReportTypeKey = "ReportType";

		// Token: 0x040027EB RID: 10219
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.ExternalActivityByUserDaily"
			},
			{
				ReportType.Weekly,
				"dbo.ExternalActivityByUserWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.ExternalActivityByUserMonthly"
			}
		};

		// Token: 0x040027EC RID: 10220
		private string viewName;
	}
}
