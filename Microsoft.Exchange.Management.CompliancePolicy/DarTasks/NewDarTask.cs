using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks
{
	// Token: 0x02000006 RID: 6
	[Cmdlet("New", "DarTask")]
	public sealed class NewDarTask : NewTaskBase<TaskStoreObject>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000276C File Offset: 0x0000096C
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002774 File Offset: 0x00000974
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter TenantId { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000277D File Offset: 0x0000097D
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002785 File Offset: 0x00000985
		[Parameter(Mandatory = true)]
		public string TaskType { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000278E File Offset: 0x0000098E
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002796 File Offset: 0x00000996
		[Parameter(Mandatory = false)]
		public int Priority { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000279F File Offset: 0x0000099F
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000027A7 File Offset: 0x000009A7
		[Parameter(Mandatory = false)]
		public string SerializedData { get; set; }

		// Token: 0x0600003B RID: 59 RVA: 0x000027B0 File Offset: 0x000009B0
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.TenantId != null)
			{
				base.CurrentOrganizationId = GetDarTask.ResolveOrganizationId(this.TenantId);
				this.server = GetDarTask.ResolveServerId(base.CurrentOrganizationId);
			}
			this.DataObject.Id = Guid.NewGuid().ToString();
			this.DataObject.TenantId = base.CurrentOrganizationId.GetBytes(Encoding.UTF8);
			this.DataObject.TaskType = this.TaskType;
			this.DataObject.Priority = this.Priority;
			this.DataObject.SerializedTaskData = this.SerializedData;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000285C File Offset: 0x00000A5C
		protected override IConfigDataProvider CreateSession()
		{
			DarTaskParams darParams = new DarTaskParams
			{
				TenantId = base.CurrentOrganizationId.GetBytes(Encoding.UTF8),
				TaskId = this.DataObject.Id
			};
			return new DarTaskDataProvider(darParams, this.server.Fqdn);
		}

		// Token: 0x04000014 RID: 20
		private ServerIdParameter server = new ServerIdParameter();
	}
}
