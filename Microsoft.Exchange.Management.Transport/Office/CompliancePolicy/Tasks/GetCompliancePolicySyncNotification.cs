using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200011C RID: 284
	[Cmdlet("Get", "CompliancePolicySyncNotification")]
	public sealed class GetCompliancePolicySyncNotification : GetTenantADObjectWithIdentityTaskBase<UnifiedPolicySyncNotificationIdParameter, UnifiedPolicySyncNotification>
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0002E8F0 File Offset: 0x0002CAF0
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x0002E907 File Offset: 0x0002CB07
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002E91C File Offset: 0x0002CB1C
		protected override IConfigDataProvider CreateSession()
		{
			ADUser discoveryMailbox = MailboxDataProvider.GetDiscoveryMailbox(base.TenantGlobalCatalogSession);
			return new UnifiedPolicySyncNotificationDataProvider(base.SessionSettings, discoveryMailbox, "Get-CompliancePolicySyncNotification");
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002E946 File Offset: 0x0002CB46
		protected override void InternalStateReset()
		{
			this.DisposeDataSession();
			base.InternalStateReset();
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002E954 File Offset: 0x0002CB54
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.DisposeDataSession();
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002E964 File Offset: 0x0002CB64
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 88, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\UnifiedPolicy\\Tasks\\GetCompliancePolicySyncNotification.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())), ExchangeErrorCategory.Client);
				return adorganizationalUnit.OrganizationId;
			}
			OrganizationId organizationId = base.ResolveCurrentOrganization();
			Utils.ValidateNotForestWideOrganization(organizationId);
			return organizationId;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002EA1C File Offset: 0x0002CC1C
		private void DisposeDataSession()
		{
			UnifiedPolicySyncNotificationDataProvider unifiedPolicySyncNotificationDataProvider = (UnifiedPolicySyncNotificationDataProvider)base.DataSession;
			if (unifiedPolicySyncNotificationDataProvider != null)
			{
				unifiedPolicySyncNotificationDataProvider.Dispose();
			}
		}
	}
}
