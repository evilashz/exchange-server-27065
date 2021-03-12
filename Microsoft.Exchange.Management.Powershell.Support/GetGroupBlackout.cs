using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000038 RID: 56
	[Cmdlet("Get", "GroupBlackout")]
	public class GetGroupBlackout : SymphonyTaskBase
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x0000D67C File Offset: 0x0000B87C
		public GetGroupBlackout()
		{
			this.Group = Array<string>.Empty;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000D68F File Offset: 0x0000B88F
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000D697 File Offset: 0x0000B897
		[Parameter(Mandatory = false)]
		public string[] Group { get; set; }

		// Token: 0x060002DB RID: 731 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		protected override void InternalProcessRecord()
		{
			using (ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints> workloadClient = new ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints>(base.WorkloadUri, base.Certificate))
			{
				GroupBlackout[] groupBlackoutResults = null;
				workloadClient.CallSymphony(delegate
				{
					groupBlackoutResults = workloadClient.Proxy.QueryBlackout(this.Group);
				}, base.WorkloadUri.ToString());
				foreach (GroupBlackout groupBlackout in groupBlackoutResults)
				{
					foreach (BlackoutInterval blackout in groupBlackout.Intervals)
					{
						base.WriteObject(new GroupBlackoutDisplay(groupBlackout.GroupName, blackout));
					}
				}
			}
		}
	}
}
