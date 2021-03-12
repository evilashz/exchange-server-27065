using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006ED RID: 1773
	[OutputType(new Type[]
	{
		typeof(SPOSkyDriveProStorageReport)
	})]
	[Cmdlet("Get", "SPOSkyDriveProStorageReport")]
	public sealed class GetSPOSkyDriveProStorageReport : TenantReportBase<SPOSkyDriveProStorageReport>
	{
		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06003E6E RID: 15982 RVA: 0x00104C8D File Offset: 0x00102E8D
		// (set) Token: 0x06003E6F RID: 15983 RVA: 0x00104CA4 File Offset: 0x00102EA4
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

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06003E70 RID: 15984 RVA: 0x00104CBC File Offset: 0x00102EBC
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x00104CC4 File Offset: 0x00102EC4
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x00104CD4 File Offset: 0x00102ED4
		private void ValidateReportType()
		{
			ReportType key = ReportType.Weekly;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetSPOSkyDriveProStorageReport.ReportTypeMapping[key];
		}

		// Token: 0x04002802 RID: 10242
		private const string ReportTypeKey = "ReportType";

		// Token: 0x04002803 RID: 10243
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Weekly,
				"dbo.SPOSkyDriveProStorageWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.SPOSkyDriveProStorageMonthly"
			}
		};

		// Token: 0x04002804 RID: 10244
		private string viewName;
	}
}
