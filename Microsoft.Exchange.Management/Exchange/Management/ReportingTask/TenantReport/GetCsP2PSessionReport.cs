using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006D8 RID: 1752
	[Cmdlet("Get", "CsP2PSessionReport")]
	[OutputType(new Type[]
	{
		typeof(CsP2PSessionReport)
	})]
	public sealed class GetCsP2PSessionReport : TenantReportBase<CsP2PSessionReport>
	{
		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06003E15 RID: 15893 RVA: 0x0010427D File Offset: 0x0010247D
		// (set) Token: 0x06003E16 RID: 15894 RVA: 0x00104294 File Offset: 0x00102494
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

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06003E17 RID: 15895 RVA: 0x001042AC File Offset: 0x001024AC
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x001042B4 File Offset: 0x001024B4
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x001042C4 File Offset: 0x001024C4
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetCsP2PSessionReport.ReportTypeMapping[key];
		}

		// Token: 0x040027E4 RID: 10212
		private const string ReportTypeKey = "ReportType";

		// Token: 0x040027E5 RID: 10213
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.CsP2PSessionDaily"
			},
			{
				ReportType.Weekly,
				"dbo.CsP2PSessionWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.CsP2PSessionMonthly"
			}
		};

		// Token: 0x040027E6 RID: 10214
		private string viewName;
	}
}
