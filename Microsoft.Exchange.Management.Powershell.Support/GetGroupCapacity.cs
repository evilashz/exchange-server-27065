using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000039 RID: 57
	[Cmdlet("Get", "GroupCapacity")]
	public class GetGroupCapacity : SymphonyTaskBase
	{
		// Token: 0x060002DC RID: 732 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		public GetGroupCapacity()
		{
			this.Group = Array<string>.Empty;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000D7CF File Offset: 0x0000B9CF
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000D7D7 File Offset: 0x0000B9D7
		[Parameter(Mandatory = false)]
		public string[] Group { get; set; }

		// Token: 0x060002DF RID: 735 RVA: 0x0000D818 File Offset: 0x0000BA18
		protected override void InternalProcessRecord()
		{
			using (ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints> workloadClient = new ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints>(base.WorkloadUri, base.Certificate))
			{
				GroupCapacity[] groupCapacityResults = null;
				workloadClient.CallSymphony(delegate
				{
					groupCapacityResults = workloadClient.Proxy.QueryCapacity(this.Group);
				}, base.WorkloadUri.ToString());
				foreach (GroupCapacity groupCapacity in groupCapacityResults)
				{
					foreach (CapacityBlock capacity in groupCapacity.Capacities)
					{
						base.WriteObject(new GroupCapacityDisplay(groupCapacity.GroupName, capacity));
					}
				}
			}
		}
	}
}
