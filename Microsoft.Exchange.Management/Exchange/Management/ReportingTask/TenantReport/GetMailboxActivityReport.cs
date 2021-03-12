using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006DF RID: 1759
	[Cmdlet("Get", "MailboxActivityReport")]
	[OutputType(new Type[]
	{
		typeof(MailboxActivityReport)
	})]
	public sealed class GetMailboxActivityReport : TenantReportBase<MailboxActivityReport>
	{
		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06003E44 RID: 15940 RVA: 0x0010475D File Offset: 0x0010295D
		// (set) Token: 0x06003E45 RID: 15941 RVA: 0x00104774 File Offset: 0x00102974
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

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06003E46 RID: 15942 RVA: 0x0010478C File Offset: 0x0010298C
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x00104794 File Offset: 0x00102994
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x001047A4 File Offset: 0x001029A4
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetMailboxActivityReport.ReportTypeMapping[key];
		}

		// Token: 0x040027F6 RID: 10230
		private const string ReportTypeKey = "ReportType";

		// Token: 0x040027F7 RID: 10231
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.MailboxActivityDaily"
			},
			{
				ReportType.Weekly,
				"dbo.MailboxActivityWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.MailboxActivityMonthly"
			},
			{
				ReportType.Yearly,
				"dbo.MailboxActivityYearly"
			}
		};

		// Token: 0x040027F8 RID: 10232
		private string viewName;
	}
}
