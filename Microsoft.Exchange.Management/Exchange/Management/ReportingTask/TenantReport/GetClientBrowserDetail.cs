using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006CD RID: 1741
	[OutputType(new Type[]
	{
		typeof(ClientSoftwareBrowserDetailReport)
	})]
	[Cmdlet("Get", "O365ClientBrowserDetailReport")]
	public sealed class GetClientBrowserDetail : TenantReportBase<ClientSoftwareBrowserDetailReport>
	{
		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06003DD1 RID: 15825 RVA: 0x001034EF File Offset: 0x001016EF
		// (set) Token: 0x06003DD2 RID: 15826 RVA: 0x001034F7 File Offset: 0x001016F7
		[Parameter(Mandatory = false)]
		public string Browser { get; set; }

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06003DD3 RID: 15827 RVA: 0x00103500 File Offset: 0x00101700
		// (set) Token: 0x06003DD4 RID: 15828 RVA: 0x00103508 File Offset: 0x00101708
		[Parameter(Mandatory = false)]
		public string BrowserVersion { get; set; }

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06003DD5 RID: 15829 RVA: 0x00103511 File Offset: 0x00101711
		// (set) Token: 0x06003DD6 RID: 15830 RVA: 0x00103519 File Offset: 0x00101719
		[Parameter(Mandatory = false)]
		public string WindowsLiveID { get; set; }

		// Token: 0x06003DD7 RID: 15831 RVA: 0x00103524 File Offset: 0x00101724
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			if (this.Browser != null || this.BrowserVersion != null || this.WindowsLiveID != null)
			{
				base.AddQueryDecorator(new WhereDecorator<ClientSoftwareBrowserDetailReport>(base.TaskContext)
				{
					Predicate = ((ClientSoftwareBrowserDetailReport report) => ((this.Browser == null) ? true : this.Browser.Equals(report.Name)) && ((this.BrowserVersion == null) ? true : this.BrowserVersion.Equals(report.Version)) && ((this.WindowsLiveID == null) ? true : this.WindowsLiveID.Equals(report.UPN)))
				});
			}
		}
	}
}
