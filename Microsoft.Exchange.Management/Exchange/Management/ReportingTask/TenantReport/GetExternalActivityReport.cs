using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006DB RID: 1755
	[OutputType(new Type[]
	{
		typeof(ExternalActivityReport)
	})]
	[Cmdlet("Get", "ExternalActivityReport")]
	public sealed class GetExternalActivityReport : TenantReportBase<ExternalActivityReport>
	{
		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06003E2C RID: 15916 RVA: 0x001044DD File Offset: 0x001026DD
		protected override DataMartType DataMartType
		{
			get
			{
				return DataMartType.TenantSecurity;
			}
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06003E2D RID: 15917 RVA: 0x001044E1 File Offset: 0x001026E1
		// (set) Token: 0x06003E2E RID: 15918 RVA: 0x001044F8 File Offset: 0x001026F8
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

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06003E2F RID: 15919 RVA: 0x00104510 File Offset: 0x00102710
		protected override string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x00104518 File Offset: 0x00102718
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			this.ValidateReportType();
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x00104528 File Offset: 0x00102728
		private void ValidateReportType()
		{
			ReportType key = ReportType.Daily;
			if (base.Fields.IsModified("ReportType"))
			{
				key = this.ReportType;
			}
			this.viewName = GetExternalActivityReport.ReportTypeMapping[key];
		}

		// Token: 0x040027ED RID: 10221
		private const string ReportTypeKey = "ReportType";

		// Token: 0x040027EE RID: 10222
		private static readonly Dictionary<ReportType, string> ReportTypeMapping = new Dictionary<ReportType, string>
		{
			{
				ReportType.Daily,
				"dbo.ExternalActivityDaily"
			},
			{
				ReportType.Weekly,
				"dbo.ExternalActivityWeekly"
			},
			{
				ReportType.Monthly,
				"dbo.ExternalActivityMonthly"
			}
		};

		// Token: 0x040027EF RID: 10223
		private string viewName;
	}
}
