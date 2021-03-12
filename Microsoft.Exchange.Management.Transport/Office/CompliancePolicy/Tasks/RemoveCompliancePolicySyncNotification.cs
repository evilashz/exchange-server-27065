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
	// Token: 0x0200011E RID: 286
	[Cmdlet("Remove", "CompliancePolicySyncNotification")]
	public sealed class RemoveCompliancePolicySyncNotification : RemoveTenantADTaskBase<UnifiedPolicySyncNotificationIdParameter, UnifiedPolicySyncNotification>
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0002EE79 File Offset: 0x0002D079
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x0002EE90 File Offset: 0x0002D090
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

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002EEA4 File Offset: 0x0002D0A4
		protected override IConfigDataProvider CreateSession()
		{
			this.ResolveCurrentOrganization();
			ADUser discoveryMailbox = MailboxDataProvider.GetDiscoveryMailbox(base.TenantGlobalCatalogSession);
			return new UnifiedPolicySyncNotificationDataProvider(base.SessionSettings, discoveryMailbox, "Remove-CompliancePolicySyncNotification");
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0002EED4 File Offset: 0x0002D0D4
		protected override void InternalStateReset()
		{
			this.DisposeDataSession();
			base.InternalStateReset();
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0002EEE2 File Offset: 0x0002D0E2
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.DisposeDataSession();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0002EEF8 File Offset: 0x0002D0F8
		private void ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 90, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\UnifiedPolicy\\Tasks\\RemoveCompliancePolicySyncNotification.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())), ExchangeErrorCategory.Client);
				base.CurrentOrganizationId = adorganizationalUnit.OrganizationId;
			}
			Utils.ValidateNotForestWideOrganization(base.CurrentOrganizationId);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0002EFB4 File Offset: 0x0002D1B4
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
