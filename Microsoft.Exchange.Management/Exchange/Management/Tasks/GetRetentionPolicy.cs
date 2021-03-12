using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200044C RID: 1100
	[Cmdlet("get", "RetentionPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetRetentionPolicy : GetMailboxPolicyBase<RetentionPolicy>
	{
		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x00099FA3 File Offset: 0x000981A3
		// (set) Token: 0x060026E6 RID: 9958 RVA: 0x00099FAB File Offset: 0x000981AB
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x00099FB4 File Offset: 0x000981B4
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Dehydrateable;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x00099FC8 File Offset: 0x000981C8
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession configurationSession;
			if (!this.IgnoreDehydratedFlag && SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
			{
				configurationSession = SharedConfiguration.CreateScopedToSharedConfigADSession(base.CurrentOrganizationId);
				return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, configurationSession.SessionSettings, 527, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxPolicies\\RetentionPolicyTasks.cs");
			}
			if (!MobileDeviceTaskHelper.IsRunningUnderMyOptionsRole(this, base.TenantGlobalCatalogSession, base.SessionSettings))
			{
				configurationSession = (IConfigurationSession)base.CreateSession();
			}
			else
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 546, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxPolicies\\RetentionPolicyTasks.cs");
			}
			return configurationSession;
		}
	}
}
