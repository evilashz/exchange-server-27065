using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006CE RID: 1742
	[OutputType(new Type[]
	{
		typeof(ClientSoftwareBrowserSummaryReport)
	})]
	[Cmdlet("Get", "O365ClientBrowserReport")]
	public sealed class GetClientBrowserReport : TenantReportBase<ClientSoftwareBrowserSummaryReport>
	{
		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06003DD9 RID: 15833 RVA: 0x001037D7 File Offset: 0x001019D7
		// (set) Token: 0x06003DDA RID: 15834 RVA: 0x001037DF File Offset: 0x001019DF
		[Parameter(Mandatory = false)]
		public string Browser { get; set; }

		// Token: 0x06003DDB RID: 15835 RVA: 0x001037E8 File Offset: 0x001019E8
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			WhereDecorator<ClientSoftwareBrowserSummaryReport> whereDecorator = new WhereDecorator<ClientSoftwareBrowserSummaryReport>(base.TaskContext);
			if (this.Browser != null)
			{
				whereDecorator.Predicate = ((ClientSoftwareBrowserSummaryReport report) => this.Browser.Equals(report.Category));
			}
			else
			{
				whereDecorator.Predicate = ((ClientSoftwareBrowserSummaryReport report) => "SUMMARY".Equals(report.Category));
			}
			base.AddQueryDecorator(whereDecorator);
		}
	}
}
