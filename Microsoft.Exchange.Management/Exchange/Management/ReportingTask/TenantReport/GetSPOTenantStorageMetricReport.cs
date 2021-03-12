using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006F0 RID: 1776
	[Cmdlet("Get", "SPOTenantStorageMetricReport")]
	[OutputType(new Type[]
	{
		typeof(SPOTenantStorageMetricReport)
	})]
	public sealed class GetSPOTenantStorageMetricReport : TenantReportBase<SPOTenantStorageMetricReport>
	{
		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06003E83 RID: 16003 RVA: 0x00104ECD File Offset: 0x001030CD
		// (set) Token: 0x06003E84 RID: 16004 RVA: 0x00104EE4 File Offset: 0x001030E4
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

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06003E85 RID: 16005 RVA: 0x00104EFC File Offset: 0x001030FC
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x00104F04 File Offset: 0x00103104
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x00104F14 File Offset: 0x00103114
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetSPOTenantStorageMetricReport.ReportTypeMapping[key];
		}

		// Token: 0x0400280B RID: 10251
		private const string ReportTypeKey = "ReportType";

		// Token: 0x0400280C RID: 10252
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.SPOTenantStorageMetricDaily"
			},
			{
				ReportType.Weekly,
				"dbo.SPOTenantStorageMetricWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.SPOTenantStorageMetricMonthly"
			}
		};

		// Token: 0x0400280D RID: 10253
		private string viewName;
	}
}
