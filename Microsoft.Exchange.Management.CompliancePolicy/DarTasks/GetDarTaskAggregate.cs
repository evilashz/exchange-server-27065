using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks
{
	// Token: 0x02000007 RID: 7
	[Cmdlet("Get", "DarTaskAggregate")]
	public sealed class GetDarTaskAggregate : GetTaskBase<TaskAggregateStoreObject>
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000028A9 File Offset: 0x00000AA9
		public GetDarTaskAggregate()
		{
			this.ExecutionUnit = new ServerIdParameter();
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000028BC File Offset: 0x00000ABC
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000028C4 File Offset: 0x00000AC4
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public ServerIdParameter ExecutionUnit { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000028CD File Offset: 0x00000ACD
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000028D5 File Offset: 0x00000AD5
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter TenantId { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000028DE File Offset: 0x00000ADE
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000028E6 File Offset: 0x00000AE6
		[Parameter(Mandatory = false)]
		public string TaskType { get; set; }

		// Token: 0x06000044 RID: 68 RVA: 0x000028EF File Offset: 0x00000AEF
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.TenantId != null)
			{
				base.CurrentOrganizationId = GetDarTask.ResolveOrganizationId(this.TenantId);
				this.ExecutionUnit = GetDarTask.ResolveServerId(base.CurrentOrganizationId);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002924 File Offset: 0x00000B24
		protected override IConfigDataProvider CreateSession()
		{
			DarTaskAggregateParams darParams = new DarTaskAggregateParams
			{
				TenantId = base.CurrentOrganizationId.GetBytes(Encoding.UTF8),
				TaskType = this.TaskType
			};
			return new DarTaskAggregateDataProvider(darParams, this.ExecutionUnit.Fqdn);
		}
	}
}
