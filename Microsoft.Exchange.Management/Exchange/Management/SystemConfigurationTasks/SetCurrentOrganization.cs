using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AEC RID: 2796
	[Cmdlet("Set", "CurrentOrganization", SupportsShouldProcess = true)]
	public sealed class SetCurrentOrganization : SystemConfigurationObjectActionTask<OrganizationIdParameter, ADOrganizationalUnit>
	{
		// Token: 0x17001E1A RID: 7706
		// (get) Token: 0x0600633D RID: 25405 RVA: 0x0019EDD8 File Offset: 0x0019CFD8
		// (set) Token: 0x0600633E RID: 25406 RVA: 0x0019EDFE File Offset: 0x0019CFFE
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreSiteCheck
		{
			get
			{
				return (SwitchParameter)(base.Fields["IgnoreSiteCheck"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IgnoreSiteCheck"] = value;
			}
		}

		// Token: 0x17001E1B RID: 7707
		// (get) Token: 0x0600633F RID: 25407 RVA: 0x0019EE16 File Offset: 0x0019D016
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.SetCurrentOrganizationConfirmation(((ADObjectId)this.DataObject.Identity).Parent.Name);
			}
		}

		// Token: 0x06006340 RID: 25408 RVA: 0x0019EE38 File Offset: 0x0019D038
		internal override IConfigurationSession CreateConfigurationSession(ADSessionSettings sessionSettings)
		{
			IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 46, "CreateConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\organization\\SetCurrentOrganization.cs");
			configurationSession.UseConfigNC = false;
			return configurationSession;
		}

		// Token: 0x06006341 RID: 25409 RVA: 0x0019EE6A File Offset: 0x0019D06A
		protected override IConfigDataProvider CreateSession()
		{
			return this.CreateConfigurationSession(null);
		}

		// Token: 0x06006342 RID: 25410 RVA: 0x0019EE73 File Offset: 0x0019D073
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.IgnoreSiteCheck;
			base.ExchangeRunspaceConfig.SwitchCurrentPartnerOrganizationAndReloadRoleCmdletInfo(this.DataObject.Name);
			ExchangePropertyContainer.ResetPerOrganizationData(base.SessionState);
			TaskLogger.LogExit();
		}
	}
}
