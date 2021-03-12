using System;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;
using Microsoft.Exchange.Management.CompliancePolicy.LocStrings;
using Microsoft.Office.CompliancePolicy.Dar;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks
{
	// Token: 0x02000005 RID: 5
	[Cmdlet("Set", "DarTask")]
	public sealed class SetDarTask : SetTaskBase<TaskStoreObject>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002627 File Offset: 0x00000827
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000262F File Offset: 0x0000082F
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter TenantId { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002638 File Offset: 0x00000838
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002640 File Offset: 0x00000840
		[Parameter(Mandatory = true)]
		public string TaskId { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002649 File Offset: 0x00000849
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002651 File Offset: 0x00000851
		[Parameter(Mandatory = false)]
		public DarTaskState TaskState { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000265A File Offset: 0x0000085A
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002662 File Offset: 0x00000862
		[Parameter(Mandatory = false)]
		public int Priority { get; set; }

		// Token: 0x0600002F RID: 47 RVA: 0x0000266B File Offset: 0x0000086B
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.TenantId != null)
			{
				base.CurrentOrganizationId = GetDarTask.ResolveOrganizationId(this.TenantId);
				this.server = GetDarTask.ResolveServerId(base.CurrentOrganizationId);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026A0 File Offset: 0x000008A0
		protected override IConfigDataProvider CreateSession()
		{
			DarTaskParams darParams = new DarTaskParams
			{
				TenantId = base.CurrentOrganizationId.GetBytes(Encoding.UTF8),
				TaskId = this.TaskId
			};
			return new DarTaskDataProvider(darParams, this.server.Fqdn);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026E8 File Offset: 0x000008E8
		protected override IConfigurable PrepareDataObject()
		{
			TaskStoreObject taskStoreObject = base.DataSession.FindPaged<TaskStoreObject>(null, null, false, null, 0).FirstOrDefault<TaskStoreObject>();
			if (taskStoreObject == null)
			{
				throw new DataSourceOperationException(new LocalizedString(Strings.TaskNotFound));
			}
			taskStoreObject.Id = this.TaskId;
			taskStoreObject.TenantId = base.CurrentOrganizationId.GetBytes(Encoding.UTF8);
			taskStoreObject.TaskState = this.TaskState;
			taskStoreObject.Priority = this.Priority;
			return taskStoreObject;
		}

		// Token: 0x0400000F RID: 15
		private ServerIdParameter server = new ServerIdParameter();
	}
}
