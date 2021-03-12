using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000037 RID: 55
	[Cmdlet("Get", "Constraint")]
	public class GetConstraint : SymphonyTaskBase
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0000D584 File Offset: 0x0000B784
		public GetConstraint()
		{
			this.Name = Array<string>.Empty;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000D597 File Offset: 0x0000B797
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000D59F File Offset: 0x0000B79F
		[Parameter(Mandatory = false)]
		public string[] Name { get; set; }

		// Token: 0x060002D7 RID: 727 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		protected override void InternalProcessRecord()
		{
			using (ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints> workloadClient = new ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints>(base.WorkloadUri, base.Certificate))
			{
				Constraint[] constraintResults = null;
				workloadClient.CallSymphony(delegate
				{
					constraintResults = workloadClient.Proxy.QueryConstraint(this.Name);
				}, base.WorkloadUri.ToString());
				base.WriteObject(constraintResults);
			}
		}
	}
}
