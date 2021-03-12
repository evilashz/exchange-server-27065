using System;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;
using Microsoft.Office.CompliancePolicy.Dar;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks
{
	// Token: 0x02000002 RID: 2
	[Cmdlet("Get", "DarTask")]
	public sealed class GetDarTask : GetTaskBase<TaskStoreObject>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public GetDarTask()
		{
			this.ExecutionUnit = new ServerIdParameter();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020E3 File Offset: 0x000002E3
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020EB File Offset: 0x000002EB
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public ServerIdParameter ExecutionUnit { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020F4 File Offset: 0x000002F4
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020FC File Offset: 0x000002FC
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter TenantId { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002105 File Offset: 0x00000305
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000210D File Offset: 0x0000030D
		[Parameter(Mandatory = false)]
		public string TaskId { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002116 File Offset: 0x00000316
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000211E File Offset: 0x0000031E
		[Parameter(Mandatory = false)]
		public DarTaskState TaskState { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002127 File Offset: 0x00000327
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000212F File Offset: 0x0000032F
		[Parameter(Mandatory = false)]
		public string TaskType { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002138 File Offset: 0x00000338
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002140 File Offset: 0x00000340
		[Parameter(Mandatory = false)]
		public DateTime MinQueuedTime { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002149 File Offset: 0x00000349
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002151 File Offset: 0x00000351
		[Parameter(Mandatory = false)]
		public DateTime MaxQueuedTime { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000215A File Offset: 0x0000035A
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002162 File Offset: 0x00000362
		[Parameter(Mandatory = false)]
		public DateTime MinCompletionTime { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000216B File Offset: 0x0000036B
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002173 File Offset: 0x00000373
		[Parameter(Mandatory = false)]
		public DateTime MaxCompletionTIme { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000217C File Offset: 0x0000037C
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002184 File Offset: 0x00000384
		[Parameter(Mandatory = false)]
		public SwitchParameter ActiveInRuntime { get; set; }

		// Token: 0x06000016 RID: 22 RVA: 0x00002190 File Offset: 0x00000390
		internal static OrganizationId ResolveOrganizationId(OrganizationIdParameter tenantId)
		{
			if (tenantId == null)
			{
				throw new ArgumentNullException("tenantId");
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 146, "ResolveOrganizationId", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\CompliancePolicy\\DarTasks\\GetDarTask.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			ADOrganizationalUnit adorganizationalUnit = tenantId.GetObjects<ADOrganizationalUnit>(null, tenantOrTopologyConfigurationSession).FirstOrDefault<ADOrganizationalUnit>();
			if (adorganizationalUnit == null)
			{
				throw new ArgumentException(Strings.ErrorOrganizationNotFound(tenantId.ToString()));
			}
			return adorganizationalUnit.OrganizationId;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002200 File Offset: 0x00000400
		internal static ServerIdParameter ResolveServerId(OrganizationId orgId)
		{
			ExchangePrincipal tenantMailbox = TenantStoreDataProvider.GetTenantMailbox(orgId);
			return new ServerIdParameter(new Fqdn(tenantMailbox.MailboxInfo.Location.ServerFqdn));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000222E File Offset: 0x0000042E
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.TenantId != null)
			{
				base.CurrentOrganizationId = GetDarTask.ResolveOrganizationId(this.TenantId);
				this.ExecutionUnit = GetDarTask.ResolveServerId(base.CurrentOrganizationId);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002260 File Offset: 0x00000460
		protected override IConfigDataProvider CreateSession()
		{
			DarTaskParams darParams = new DarTaskParams
			{
				TaskId = this.TaskId,
				TenantId = base.CurrentOrganizationId.GetBytes(Encoding.UTF8),
				TaskState = this.TaskState,
				TaskType = this.TaskType,
				MaxCompletionTime = this.MaxCompletionTIme,
				MaxQueuedTime = this.MaxQueuedTime,
				MinCompletionTime = this.MinCompletionTime,
				MinQueuedTime = this.MinQueuedTime,
				ActiveInRuntime = this.ActiveInRuntime.ToBool()
			};
			return new DarTaskDataProvider(darParams, this.ExecutionUnit.Fqdn);
		}
	}
}
