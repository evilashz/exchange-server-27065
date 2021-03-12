using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x020003C3 RID: 963
	internal static class ADHelper
	{
		// Token: 0x060022AE RID: 8878 RVA: 0x0008CFFC File Offset: 0x0008B1FC
		internal static OrganizationId ResolveOrganization(OrganizationIdParameter organization, OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId)
		{
			if (organization == null)
			{
				return null;
			}
			ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerIdForLocalForest, currentOrganizationId, executingUserOrganizationId, false);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 48, "ResolveOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\FfoReporting\\Common\\ADHelper.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			ADOrganizationalUnit adorganizationalUnit = null;
			LocalizedString? localizedString = null;
			IEnumerable<ADOrganizationalUnit> objects = organization.GetObjects<ADOrganizationalUnit>(null, tenantOrTopologyConfigurationSession, null, out localizedString);
			using (IEnumerator<ADOrganizationalUnit> enumerator = objects.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new ManagementObjectNotFoundException(Strings.ErrorOrganizationNotFound(organization.ToString()));
				}
				adorganizationalUnit = enumerator.Current;
				if (enumerator.MoveNext())
				{
					throw new ManagementObjectAmbiguousException(Strings.ErrorOrganizationNotUnique(organization.ToString()));
				}
			}
			return adorganizationalUnit.OrganizationId;
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0008D0C4 File Offset: 0x0008B2C4
		internal static IConfigDataProvider CreateConfigSession(OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromCustomScopeSet(ScopeSet.GetOrgWideDefaultScopeSet(currentOrganizationId), ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), currentOrganizationId, executingUserOrganizationId, true);
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 102, "CreateConfigSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\FfoReporting\\Common\\ADHelper.cs");
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0008D104 File Offset: 0x0008B304
		internal static Guid GetExternalDirectoryOrganizationId(OrganizationIdParameter organization, OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId)
		{
			OrganizationId organizationId = ADHelper.ResolveOrganization(organization, currentOrganizationId, executingUserOrganizationId);
			return ADHelper.GetExternalDirectoryOrganizationId(organizationId);
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0008D120 File Offset: 0x0008B320
		internal static Guid GetExternalDirectoryOrganizationId(OrganizationId organizationId)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 139, "GetExternalDirectoryOrganizationId", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\FfoReporting\\Common\\ADHelper.cs");
			ExchangeConfigurationUnit exchangeConfigurationUnit = tenantOrTopologyConfigurationSession.Read<ExchangeConfigurationUnit>(organizationId.ConfigurationUnit);
			if (exchangeConfigurationUnit == null)
			{
				throw new ArgumentException("External configuration unit is null.");
			}
			if (string.IsNullOrEmpty(exchangeConfigurationUnit.ExternalDirectoryOrganizationId))
			{
				throw new ArgumentException("External directory organization is either null or empty.");
			}
			return Guid.Parse(exchangeConfigurationUnit.ExternalDirectoryOrganizationId);
		}
	}
}
