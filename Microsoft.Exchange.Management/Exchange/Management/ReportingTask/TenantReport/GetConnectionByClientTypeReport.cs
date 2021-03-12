using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006D2 RID: 1746
	[OutputType(new Type[]
	{
		typeof(ConnectionByClientTypeReport)
	})]
	[Cmdlet("Get", "ConnectionByClientTypeReport")]
	public sealed class GetConnectionByClientTypeReport : TenantReportBase<ConnectionByClientTypeReport>
	{
		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06003DF1 RID: 15857 RVA: 0x00103E81 File Offset: 0x00102081
		// (set) Token: 0x06003DF2 RID: 15858 RVA: 0x00103E98 File Offset: 0x00102098
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

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x00103EB0 File Offset: 0x001020B0
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x00103EB8 File Offset: 0x001020B8
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x00103EC8 File Offset: 0x001020C8
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetConnectionByClientTypeReport.ReportTypeMapping[key];
		}

		// Token: 0x040027D5 RID: 10197
		private const string ReportTypeKey = "ReportType";

		// Token: 0x040027D6 RID: 10198
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.ConnectionByClientTypeDaily"
			},
			{
				ReportType.Weekly,
				"dbo.ConnectionByClientTypeWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.ConnectionByClientTypeMonthly"
			},
			{
				ReportType.Yearly,
				"dbo.ConnectionByClientTypeYearly"
			}
		};

		// Token: 0x040027D7 RID: 10199
		private string viewName;
	}
}
