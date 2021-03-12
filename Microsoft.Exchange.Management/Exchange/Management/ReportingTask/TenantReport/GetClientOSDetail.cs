using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006CF RID: 1743
	[Cmdlet("Get", "O365ClientOSDetailReport")]
	[OutputType(new Type[]
	{
		typeof(ClientSoftwareOSDetailReport)
	})]
	public sealed class GetClientOSDetail : TenantReportBase<ClientSoftwareOSDetailReport>
	{
		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06003DDD RID: 15837 RVA: 0x00103929 File Offset: 0x00101B29
		// (set) Token: 0x06003DDE RID: 15838 RVA: 0x00103931 File Offset: 0x00101B31
		[Parameter(Mandatory = false)]
		public string OperatingSystem { get; set; }

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x0010393A File Offset: 0x00101B3A
		// (set) Token: 0x06003DE0 RID: 15840 RVA: 0x00103942 File Offset: 0x00101B42
		[Parameter(Mandatory = false)]
		public string OperatingSystemVersion { get; set; }

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06003DE1 RID: 15841 RVA: 0x0010394B File Offset: 0x00101B4B
		// (set) Token: 0x06003DE2 RID: 15842 RVA: 0x00103953 File Offset: 0x00101B53
		[Parameter(Mandatory = false)]
		public string WindowsLiveID { get; set; }

		// Token: 0x06003DE3 RID: 15843 RVA: 0x0010395C File Offset: 0x00101B5C
		protected override void ProcessNonPipelineParameter()
		{
			base.ProcessNonPipelineParameter();
			if (this.OperatingSystem != null || this.OperatingSystemVersion != null || this.WindowsLiveID != null)
			{
				base.AddQueryDecorator(new WhereDecorator<ClientSoftwareOSDetailReport>(base.TaskContext)
				{
					Predicate = ((ClientSoftwareOSDetailReport report) => ((this.OperatingSystem == null) ? true : this.OperatingSystem.Equals(report.Name)) && ((this.OperatingSystemVersion == null) ? true : this.OperatingSystemVersion.Equals(report.Version)) && ((this.WindowsLiveID == null) ? true : this.WindowsLiveID.Equals(report.UPN)))
				});
			}
		}
	}
}
