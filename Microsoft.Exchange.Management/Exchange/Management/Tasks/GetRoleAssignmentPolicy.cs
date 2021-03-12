using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000451 RID: 1105
	[Cmdlet("Get", "RoleAssignmentPolicy")]
	public sealed class GetRoleAssignmentPolicy : GetMailboxPolicyBase<RoleAssignmentPolicy>
	{
		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x0009AEF9 File Offset: 0x000990F9
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Static;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06002723 RID: 10019 RVA: 0x0009AF0B File Offset: 0x0009910B
		// (set) Token: 0x06002724 RID: 10020 RVA: 0x0009AF13 File Offset: 0x00099113
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x06002725 RID: 10021 RVA: 0x0009AF1C File Offset: 0x0009911C
		protected override void WriteResult(IConfigurable dataObject)
		{
			RoleAssignmentPolicy roleAssignmentPolicy = (RoleAssignmentPolicy)dataObject;
			Result<ExchangeRoleAssignment>[] roleAssignmentResults;
			if (base.SharedConfiguration != null)
			{
				SharedConfiguration sharedConfiguration = base.SharedConfiguration;
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, sharedConfiguration.GetSharedConfigurationSessionSettings(), 553, "WriteResult", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxPolicies\\RoleAssignmentPolicyTasks.cs");
				roleAssignmentResults = tenantOrTopologyConfigurationSession.FindRoleAssignmentsByUserIds(new ADObjectId[]
				{
					sharedConfiguration.GetSharedRoleAssignmentPolicy()
				}, false);
				roleAssignmentPolicy.SharedConfiguration = sharedConfiguration.SharedConfigurationCU.Id;
			}
			else
			{
				roleAssignmentResults = this.ConfigurationSession.FindRoleAssignmentsByUserIds(new ADObjectId[]
				{
					roleAssignmentPolicy.Id
				}, false);
			}
			roleAssignmentPolicy.PopulateRoles(roleAssignmentResults);
			base.WriteResult(roleAssignmentPolicy);
		}
	}
}
