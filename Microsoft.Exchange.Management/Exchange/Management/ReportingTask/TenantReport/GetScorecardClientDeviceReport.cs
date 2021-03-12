using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006E5 RID: 1765
	[OutputType(new Type[]
	{
		typeof(ScorecardClientDeviceReport)
	})]
	[Cmdlet("Get", "ScorecardClientDeviceReport")]
	public sealed class GetScorecardClientDeviceReport : TenantReportBase<ScorecardClientDeviceReport>
	{
		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06003E50 RID: 15952 RVA: 0x00104810 File Offset: 0x00102A10
		// (set) Token: 0x06003E51 RID: 15953 RVA: 0x00104818 File Offset: 0x00102A18
		[Parameter(Mandatory = false)]
		public DataCategory Category { get; set; }

		// Token: 0x06003E52 RID: 15954 RVA: 0x00104824 File Offset: 0x00102A24
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			base.AddQueryDecorator(new WhereDecorator<ScorecardClientDeviceReport>(base.TaskContext)
			{
				Predicate = ((ScorecardClientDeviceReport report) => this.Category.ToString().Equals(report.Category))
			});
		}
	}
}
