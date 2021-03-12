using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006D9 RID: 1753
	[Cmdlet("Get", "ExternalActivityByDomainReport")]
	[OutputType(new Type[]
	{
		typeof(ExternalActivityByDomainReport)
	})]
	public sealed class GetExternalActivityByDomainReport : TenantReportBase<ExternalActivityByDomainReport>
	{
		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06003E1C RID: 15900 RVA: 0x00104345 File Offset: 0x00102545
		protected override DataMartType DataMartType
		{
			get
			{
				return DataMartType.TenantSecurity;
			}
		}

		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06003E1D RID: 15901 RVA: 0x00104349 File Offset: 0x00102549
		// (set) Token: 0x06003E1E RID: 15902 RVA: 0x00104360 File Offset: 0x00102560
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

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06003E1F RID: 15903 RVA: 0x00104378 File Offset: 0x00102578
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x00104380 File Offset: 0x00102580
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x00104390 File Offset: 0x00102590
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetExternalActivityByDomainReport.ReportTypeMapping[key];
		}

		// Token: 0x040027E7 RID: 10215
		private const string ReportTypeKey = "ReportType";

		// Token: 0x040027E8 RID: 10216
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.ExternalActivityByDomainDaily"
			},
			{
				ReportType.Weekly,
				"dbo.ExternalActivityByDomainWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.ExternalActivityByDomainMonthly"
			}
		};

		// Token: 0x040027E9 RID: 10217
		private string viewName;
	}
}
