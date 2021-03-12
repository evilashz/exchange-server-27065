using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks
{
	// Token: 0x0200000D RID: 13
	[Cmdlet("Remove", "DarTaskAggregate")]
	public sealed class RemoveDarTaskAggregate : Task
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003334 File Offset: 0x00001534
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000333C File Offset: 0x0000153C
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter TenantId { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003345 File Offset: 0x00001545
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000334D File Offset: 0x0000154D
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string TaskType { get; set; }

		// Token: 0x06000078 RID: 120 RVA: 0x00003358 File Offset: 0x00001558
		protected override void InternalProcessRecord()
		{
			if (this.TenantId != null)
			{
				base.CurrentOrganizationId = GetDarTask.ResolveOrganizationId(this.TenantId);
			}
			string fqdn = GetDarTask.ResolveServerId(base.CurrentOrganizationId).Fqdn;
			DarTaskAggregateParams darTaskAggregateParams = new DarTaskAggregateParams
			{
				TenantId = base.CurrentOrganizationId.GetBytes(Encoding.UTF8),
				TaskType = this.TaskType
			};
			using (HostRpcClient hostRpcClient = new HostRpcClient(fqdn))
			{
				hostRpcClient.RemoveDarTaskAggregate(darTaskAggregateParams);
			}
		}
	}
}
