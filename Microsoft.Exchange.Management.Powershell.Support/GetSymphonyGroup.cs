using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200003A RID: 58
	[Cmdlet("Get", "SymphonyGroup")]
	public class GetSymphonyGroup : SymphonyTaskBase
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000D904 File Offset: 0x0000BB04
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000D90C File Offset: 0x0000BB0C
		[Parameter(Mandatory = true)]
		public string[] Name { get; set; }

		// Token: 0x060002E3 RID: 739 RVA: 0x0000D950 File Offset: 0x0000BB50
		protected override void InternalProcessRecord()
		{
			using (ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints> workloadClient = new ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints>(base.WorkloadUri, base.Certificate))
			{
				Group[] groupResults = null;
				workloadClient.CallSymphony(delegate
				{
					groupResults = workloadClient.Proxy.QueryGroup(this.Name);
				}, base.WorkloadUri.ToString());
				base.WriteObject(groupResults);
			}
		}
	}
}
