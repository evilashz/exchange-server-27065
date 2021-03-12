using System;
using System.Management.Automation;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;
using Microsoft.Office.CompliancePolicy.Dar;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks
{
	// Token: 0x02000004 RID: 4
	[Cmdlet("Test", "DarTask")]
	public sealed class TestDarTask : Task
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000245C File Offset: 0x0000065C
		public TestDarTask()
		{
			this.Retries = 10;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000246C File Offset: 0x0000066C
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002474 File Offset: 0x00000674
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter TenantId { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000247D File Offset: 0x0000067D
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002485 File Offset: 0x00000685
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public int Retries { get; set; }

		// Token: 0x06000025 RID: 37 RVA: 0x00002490 File Offset: 0x00000690
		protected override void InternalProcessRecord()
		{
			base.WriteObject("Sending no op task");
			OrganizationId organizationId = (this.TenantId != null) ? GetDarTask.ResolveOrganizationId(this.TenantId) : OrganizationId.ForestWideOrgId;
			string fqdn = GetDarTask.ResolveServerId(base.CurrentOrganizationId).Fqdn;
			using (HostRpcClient hostRpcClient = new HostRpcClient(fqdn))
			{
				string text = Guid.NewGuid().ToString();
				TaskStoreObject darTask = new TaskStoreObject
				{
					Id = text,
					TaskType = "Common.NoOp",
					TenantId = organizationId.GetBytes(Encoding.UTF8)
				};
				hostRpcClient.SetDarTask(darTask);
				base.WriteObject("Task enqueued, waiting for completed status. ID:" + text);
				for (int i = 0; i < this.Retries; i++)
				{
					TaskStoreObject[] darTask2 = hostRpcClient.GetDarTask(new DarTaskParams
					{
						TaskId = text
					});
					if (darTask2.Length > 1)
					{
						base.WriteError(new Exception("Unexected number of tasks returned by GetDarTask"), ErrorCategory.InvalidResult, darTask2.Length);
						return;
					}
					if (darTask2.Length == 1)
					{
						base.WriteObject("Task state: " + darTask2[0].TaskState);
						if (darTask2[0].TaskState == DarTaskState.Completed)
						{
							return;
						}
					}
					else
					{
						base.WriteObject("No tasks found");
					}
					Thread.Sleep(1000);
				}
			}
			base.WriteError(new Exception("Operation timeout"), ErrorCategory.OperationTimeout, null);
		}
	}
}
