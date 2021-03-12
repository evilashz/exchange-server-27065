using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006E7 RID: 1767
	[OutputType(new Type[]
	{
		typeof(ScorecardClientOutlookReport)
	})]
	[Cmdlet("Get", "ScorecardClientOutlookReport")]
	public sealed class GetScorecardClientOutlookReport : TenantReportBase<ScorecardClientOutlookReport>
	{
		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06003E58 RID: 15960 RVA: 0x001049D9 File Offset: 0x00102BD9
		// (set) Token: 0x06003E59 RID: 15961 RVA: 0x001049E1 File Offset: 0x00102BE1
		[Parameter(Mandatory = false)]
		public DataCategory Category { get; set; }

		// Token: 0x06003E5A RID: 15962 RVA: 0x001049EC File Offset: 0x00102BEC
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			base.AddQueryDecorator(new WhereDecorator<ScorecardClientOutlookReport>(base.TaskContext)
			{
				Predicate = ((ScorecardClientOutlookReport report) => this.Category.ToString().Equals(report.Category))
			});
		}
	}
}
