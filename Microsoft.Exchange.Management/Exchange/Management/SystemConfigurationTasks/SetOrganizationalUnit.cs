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
	// Token: 0x02000C5B RID: 3163
	[Cmdlet("Set", "OrganizationalUnit", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetOrganizationalUnit : SetADTaskBase<ExtendedOrganizationalUnitIdParameter, ExtendedOrganizationalUnit, ExtendedOrganizationalUnit>
	{
		// Token: 0x060077F8 RID: 30712 RVA: 0x001E8E7C File Offset: 0x001E707C
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, ConfigScopes.TenantSubTree, 42, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\SetOrganizationalUnit.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			tenantOrTopologyConfigurationSession.UseGlobalCatalog = true;
			tenantOrTopologyConfigurationSession.EnforceDefaultScope = false;
			return tenantOrTopologyConfigurationSession;
		}

		// Token: 0x17002513 RID: 9491
		// (get) Token: 0x060077F9 RID: 30713 RVA: 0x001E8ECB File Offset: 0x001E70CB
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetOrganization(this.Identity.ToString());
			}
		}

		// Token: 0x060077FA RID: 30714 RVA: 0x001E8EDD File Offset: 0x001E70DD
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return true;
		}
	}
}
