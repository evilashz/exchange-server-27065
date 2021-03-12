using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006D1 RID: 1745
	[Cmdlet("Get", "ConnectionByClientTypeDetailReport")]
	[OutputType(new Type[]
	{
		typeof(ConnectionByClientTypeDetailReport)
	})]
	public sealed class GetConnectionByClientTypeDetailReport : TenantReportBase<ConnectionByClientTypeDetailReport>
	{
		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06003DEA RID: 15850 RVA: 0x00103DAD File Offset: 0x00101FAD
		// (set) Token: 0x06003DEB RID: 15851 RVA: 0x00103DC4 File Offset: 0x00101FC4
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

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06003DEC RID: 15852 RVA: 0x00103DDC File Offset: 0x00101FDC
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x00103DE4 File Offset: 0x00101FE4
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x00103DF4 File Offset: 0x00101FF4
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetConnectionByClientTypeDetailReport.ReportTypeMapping[key];
		}

		// Token: 0x040027D2 RID: 10194
		private const string ReportTypeKey = "ReportType";

		// Token: 0x040027D3 RID: 10195
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.ConnectionByClientTypeDetailDaily"
			},
			{
				ReportType.Weekly,
				"dbo.ConnectionByClientTypeDetailWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.ConnectionByClientTypeDetailMonthly"
			},
			{
				ReportType.Yearly,
				"dbo.ConnectionByClientTypeDetailYearly"
			}
		};

		// Token: 0x040027D4 RID: 10196
		private string viewName;
	}
}
