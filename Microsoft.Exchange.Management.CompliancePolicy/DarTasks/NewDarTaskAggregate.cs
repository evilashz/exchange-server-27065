using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks
{
	// Token: 0x02000009 RID: 9
	[Cmdlet("New", "DarTaskAggregate")]
	public sealed class NewDarTaskAggregate : NewTaskBase<TaskAggregateStoreObject>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002ABA File Offset: 0x00000CBA
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002AC2 File Offset: 0x00000CC2
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter TenantId { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002ACB File Offset: 0x00000CCB
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002AD3 File Offset: 0x00000CD3
		[Parameter(Mandatory = true)]
		public string TaskType { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002ADC File Offset: 0x00000CDC
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002AE4 File Offset: 0x00000CE4
		[Parameter(Mandatory = false)]
		public bool IsEnabled { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002AED File Offset: 0x00000CED
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002AF5 File Offset: 0x00000CF5
		[Parameter(Mandatory = false)]
		public int MaxRunningTasks { get; set; }

		// Token: 0x0600005B RID: 91 RVA: 0x00002B00 File Offset: 0x00000D00
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.TenantId != null)
			{
				base.CurrentOrganizationId = GetDarTask.ResolveOrganizationId(this.TenantId);
				this.server = GetDarTask.ResolveServerId(base.CurrentOrganizationId);
			}
			this.DataObject.ScopeId = base.CurrentOrganizationId.GetBytes(Encoding.UTF8);
			this.DataObject.TaskType = this.TaskType;
			this.DataObject.MaxRunningTasks = this.MaxRunningTasks;
			this.DataObject.Enabled = this.IsEnabled;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002B8C File Offset: 0x00000D8C
		protected override IConfigDataProvider CreateSession()
		{
			DarTaskAggregateParams darParams = new DarTaskAggregateParams
			{
				TenantId = base.CurrentOrganizationId.GetBytes(Encoding.UTF8),
				TaskId = this.DataObject.Id
			};
			return new DarTaskAggregateDataProvider(darParams, this.server.Fqdn);
		}

		// Token: 0x04000021 RID: 33
		private ServerIdParameter server = new ServerIdParameter();
	}
}
