using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks
{
	// Token: 0x0200000C RID: 12
	[Cmdlet("Remove", "CompletedDarTask")]
	public sealed class RemoveCompletedDarTask : Task
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003260 File Offset: 0x00001460
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003268 File Offset: 0x00001468
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter TenantId { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003271 File Offset: 0x00001471
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003279 File Offset: 0x00001479
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public DateTime MaxCompletionTime { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003282 File Offset: 0x00001482
		// (set) Token: 0x06000071 RID: 113 RVA: 0x0000328A File Offset: 0x0000148A
		[Parameter(Mandatory = false)]
		public string TaskType { get; set; }

		// Token: 0x06000072 RID: 114 RVA: 0x00003294 File Offset: 0x00001494
		protected override void InternalProcessRecord()
		{
			if (this.TenantId != null)
			{
				base.CurrentOrganizationId = GetDarTask.ResolveOrganizationId(this.TenantId);
			}
			string fqdn = GetDarTask.ResolveServerId(base.CurrentOrganizationId).Fqdn;
			DarTaskParams darParams = new DarTaskParams
			{
				TenantId = base.CurrentOrganizationId.GetBytes(Encoding.UTF8),
				MaxCompletionTime = this.MaxCompletionTime,
				TaskType = this.TaskType
			};
			using (HostRpcClient hostRpcClient = new HostRpcClient(fqdn))
			{
				hostRpcClient.RemoveCompletedDarTasks(darParams);
			}
		}
	}
}
