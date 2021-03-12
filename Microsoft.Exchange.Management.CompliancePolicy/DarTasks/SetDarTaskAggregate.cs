using System;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks
{
	// Token: 0x02000008 RID: 8
	[Cmdlet("Set", "DarTaskAggregate")]
	public sealed class SetDarTaskAggregate : SetTaskBase<TaskAggregateStoreObject>
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000297F File Offset: 0x00000B7F
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002987 File Offset: 0x00000B87
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter TenantId { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002990 File Offset: 0x00000B90
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002998 File Offset: 0x00000B98
		[Parameter(Mandatory = true)]
		public string TaskType { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000029A1 File Offset: 0x00000BA1
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000029A9 File Offset: 0x00000BA9
		[Parameter(Mandatory = false)]
		public bool IsEnabled { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000029B2 File Offset: 0x00000BB2
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000029BA File Offset: 0x00000BBA
		[Parameter(Mandatory = false)]
		public int MaxRunningTasks { get; set; }

		// Token: 0x0600004F RID: 79 RVA: 0x000029C4 File Offset: 0x00000BC4
		protected override IConfigDataProvider CreateSession()
		{
			DarTaskAggregateParams darParams = new DarTaskAggregateParams
			{
				TenantId = base.CurrentOrganizationId.GetBytes(Encoding.UTF8),
				TaskType = this.TaskType
			};
			return new DarTaskAggregateDataProvider(darParams, this.server.Fqdn);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A0C File Offset: 0x00000C0C
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.TenantId != null)
			{
				base.CurrentOrganizationId = GetDarTask.ResolveOrganizationId(this.TenantId);
				this.server = GetDarTask.ResolveServerId(base.CurrentOrganizationId);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A40 File Offset: 0x00000C40
		protected override IConfigurable PrepareDataObject()
		{
			TaskAggregateStoreObject taskAggregateStoreObject = base.DataSession.FindPaged<TaskAggregateStoreObject>(null, null, false, null, 0).FirstOrDefault<TaskAggregateStoreObject>();
			if (taskAggregateStoreObject == null)
			{
				taskAggregateStoreObject = new TaskAggregateStoreObject();
			}
			taskAggregateStoreObject.ScopeId = base.CurrentOrganizationId.GetBytes(Encoding.UTF8);
			taskAggregateStoreObject.TaskType = this.TaskType;
			taskAggregateStoreObject.MaxRunningTasks = this.MaxRunningTasks;
			taskAggregateStoreObject.Enabled = this.IsEnabled;
			return taskAggregateStoreObject;
		}

		// Token: 0x0400001C RID: 28
		private ServerIdParameter server = new ServerIdParameter();
	}
}
