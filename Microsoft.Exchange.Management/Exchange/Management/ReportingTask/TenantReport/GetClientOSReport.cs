using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006D0 RID: 1744
	[Cmdlet("Get", "O365ClientOSReport")]
	[OutputType(new Type[]
	{
		typeof(ClientSoftwareOSSummaryReport)
	})]
	public sealed class GetClientOSReport : TenantReportBase<ClientSoftwareOSSummaryReport>
	{
		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06003DE5 RID: 15845 RVA: 0x00103C0F File Offset: 0x00101E0F
		// (set) Token: 0x06003DE6 RID: 15846 RVA: 0x00103C17 File Offset: 0x00101E17
		[Parameter(Mandatory = false)]
		public string OS { get; set; }

		// Token: 0x06003DE7 RID: 15847 RVA: 0x00103C20 File Offset: 0x00101E20
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			WhereDecorator<ClientSoftwareOSSummaryReport> whereDecorator = new WhereDecorator<ClientSoftwareOSSummaryReport>(base.TaskContext);
			if (this.OS != null)
			{
				whereDecorator.Predicate = ((ClientSoftwareOSSummaryReport report) => this.OS.Equals(report.Category));
			}
			else
			{
				whereDecorator.Predicate = ((ClientSoftwareOSSummaryReport report) => "SUMMARY".Equals(report.Category));
			}
			base.AddQueryDecorator(whereDecorator);
		}
	}
}
