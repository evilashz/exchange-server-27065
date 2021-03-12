using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x02000695 RID: 1685
	internal static class ADHelper
	{
		// Token: 0x06003BDC RID: 15324 RVA: 0x000FFEB4 File Offset: 0x000FE0B4
		internal static Guid? ResolveOrganizationGuid(OrganizationIdParameter organization, OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId, out Guid? tenantExternalDirectoryId)
		{
			tenantExternalDirectoryId = null;
			if (organization == null)
			{
				return null;
			}
			ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerIdForLocalForest, currentOrganizationId, executingUserOrganizationId, false);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 50, "ResolveOrganizationGuid", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ReportingTask\\Common\\ADHelper.cs");
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
			tenantExternalDirectoryId = new Guid?(Guid.Parse(adorganizationalUnit.OrganizationId.ToExternalDirectoryOrganizationId()));
			return new Guid?(adorganizationalUnit.OrganizationId.OrganizationalUnit.ObjectGuid);
		}
	}
}
