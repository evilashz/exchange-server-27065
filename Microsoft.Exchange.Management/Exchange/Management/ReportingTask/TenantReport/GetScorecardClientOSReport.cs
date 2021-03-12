using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006E6 RID: 1766
	[OutputType(new Type[]
	{
		typeof(ScorecardClientOSReport)
	})]
	[Cmdlet("Get", "ScorecardClientOSReport")]
	public sealed class GetScorecardClientOSReport : TenantReportBase<ScorecardClientOSReport>
	{
		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06003E54 RID: 15956 RVA: 0x001048F5 File Offset: 0x00102AF5
		// (set) Token: 0x06003E55 RID: 15957 RVA: 0x001048FD File Offset: 0x00102AFD
		[Parameter(Mandatory = false)]
		public DataCategory Category { get; set; }

		// Token: 0x06003E56 RID: 15958 RVA: 0x00104908 File Offset: 0x00102B08
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			base.AddQueryDecorator(new WhereDecorator<ScorecardClientOSReport>(base.TaskContext)
			{
				Predicate = ((ScorecardClientOSReport report) => this.Category.ToString().Equals(report.Category))
			});
		}
	}
}
