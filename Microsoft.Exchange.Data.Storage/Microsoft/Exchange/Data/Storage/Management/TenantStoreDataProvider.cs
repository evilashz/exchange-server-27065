using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x0200045F RID: 1119
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TenantStoreDataProvider : EwsStoreDataProvider
	{
		// Token: 0x060031EF RID: 12783 RVA: 0x000CCC18 File Offset: 0x000CAE18
		protected TenantStoreDataProvider(OrganizationId organizationId) : base(new LazilyInitialized<IExchangePrincipal>(() => TenantStoreDataProvider.GetTenantMailbox(organizationId ?? OrganizationId.ForestWideOrgId)), SpecialLogonType.SystemService, OpenAsAdminOrSystemServiceBudgetTypeType.RunAsBackgroundLoad)
		{
			if (organizationId == null && Datacenter.IsMultiTenancyEnabled())
			{
				throw new ArgumentNullException("organizationId", "organizationId can't be null in hosting environment.");
			}
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000CCC78 File Offset: 0x000CAE78
		public static ExchangePrincipal GetTenantMailbox(OrganizationId organizationId)
		{
			string organizationKey = TenantStoreDataProvider.GetOrganizationKey(organizationId);
			ExchangePrincipal exchangePrincipal = null;
			if (!TenantStoreDataProvider.exchangePrincipalCache.TryGetValue(organizationKey, out exchangePrincipal))
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), organizationId, null, false);
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 67, "GetTenantMailbox", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Management\\EwsDriver\\TenantStoreDataProvider.cs");
				try
				{
					exchangePrincipal = ExchangePrincipal.FromADUser(MailboxDataProvider.GetDiscoveryMailbox(tenantOrRootOrgRecipientSession), null);
				}
				catch (ObjectNotFoundException ex)
				{
					throw new DataSourceOperationException(ex.LocalizedString, ex);
				}
				TenantStoreDataProvider.exchangePrincipalCache.Add(organizationKey, exchangePrincipal);
			}
			return exchangePrincipal;
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000CCD04 File Offset: 0x000CAF04
		protected static string GetOrganizationKey(OrganizationId organizationId)
		{
			if (!(organizationId == null) && !(organizationId == OrganizationId.ForestWideOrgId))
			{
				return organizationId.OrganizationalUnit.Name;
			}
			return "ForestWideOrg";
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000CCD2D File Offset: 0x000CAF2D
		protected override void ExpireCache()
		{
			base.ExpireCache();
			TenantStoreDataProvider.exchangePrincipalCache.Remove(TenantStoreDataProvider.GetOrganizationKey(base.Mailbox.MailboxInfo.OrganizationId));
		}

		// Token: 0x04001B01 RID: 6913
		private static readonly MruDictionaryCache<string, ExchangePrincipal> exchangePrincipalCache = new MruDictionaryCache<string, ExchangePrincipal>(10, 100, 60);
	}
}
