using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006DC RID: 1756
	[OutputType(new Type[]
	{
		typeof(ExternalActivitySummaryReport)
	})]
	[Cmdlet("Get", "ExternalActivitySummaryReport")]
	public sealed class GetExternalActivitySummaryReport : TenantReportBase<ExternalActivitySummaryReport>
	{
		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06003E34 RID: 15924 RVA: 0x001045A9 File Offset: 0x001027A9
		protected override DataMartType DataMartType
		{
			get
			{
				return DataMartType.TenantSecurity;
			}
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06003E35 RID: 15925 RVA: 0x001045AD File Offset: 0x001027AD
		// (set) Token: 0x06003E36 RID: 15926 RVA: 0x001045C4 File Offset: 0x001027C4
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

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x06003E37 RID: 15927 RVA: 0x001045DC File Offset: 0x001027DC
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x001045E4 File Offset: 0x001027E4
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x001045F4 File Offset: 0x001027F4
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetExternalActivitySummaryReport.ReportTypeMapping[key];
		}

		// Token: 0x040027F0 RID: 10224
		private const string ReportTypeKey = "ReportType";

		// Token: 0x040027F1 RID: 10225
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.ExternalDomainSummaryDaily"
			},
			{
				ReportType.Weekly,
				"dbo.ExternalDomainSummaryWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.ExternalDomainSummaryMonthly"
			}
		};

		// Token: 0x040027F2 RID: 10226
		private string viewName;
	}
}
