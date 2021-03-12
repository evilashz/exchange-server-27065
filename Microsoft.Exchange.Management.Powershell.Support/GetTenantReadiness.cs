using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200003B RID: 59
	[Cmdlet("Get", "TenantReadiness")]
	public class GetTenantReadiness : SymphonyTaskBase
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		[Parameter(Mandatory = true)]
		public Guid[] TenantID { get; set; }

		// Token: 0x060002E7 RID: 743 RVA: 0x0000DA40 File Offset: 0x0000BC40
		protected override void InternalProcessRecord()
		{
			using (ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints> workloadClient = new ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints>(base.WorkloadUri, base.Certificate))
			{
				TenantReadiness[] readiness = null;
				workloadClient.CallSymphony(delegate
				{
					readiness = workloadClient.Proxy.QueryTenantReadiness(this.TenantID);
				}, base.WorkloadUri.ToString());
				base.WriteObject(readiness);
			}
		}
	}
}
