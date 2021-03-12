using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006DD RID: 1757
	[Cmdlet("Get", "GroupActivityReport")]
	[OutputType(new Type[]
	{
		typeof(GroupActivityReport)
	})]
	public sealed class GetGroupActivityReport : TenantReportBase<GroupActivityReport>
	{
		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x06003E3C RID: 15932 RVA: 0x00104681 File Offset: 0x00102881
		// (set) Token: 0x06003E3D RID: 15933 RVA: 0x00104698 File Offset: 0x00102898
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

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06003E3E RID: 15934 RVA: 0x001046B0 File Offset: 0x001028B0
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x001046B8 File Offset: 0x001028B8
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x001046C8 File Offset: 0x001028C8
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetGroupActivityReport.ReportTypeMapping[key];
		}

		// Token: 0x040027F3 RID: 10227
		private const string ReportTypeKey = "ReportType";

		// Token: 0x040027F4 RID: 10228
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.GroupActivityDaily"
			},
			{
				ReportType.Weekly,
				"dbo.GroupActivityWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.GroupActivityMonthly"
			},
			{
				ReportType.Yearly,
				"dbo.GroupActivityYearly"
			}
		};

		// Token: 0x040027F5 RID: 10229
		private string viewName;
	}
}
