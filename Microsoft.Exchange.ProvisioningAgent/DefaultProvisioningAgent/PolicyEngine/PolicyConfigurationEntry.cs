using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.DefaultProvisioningAgent.PolicyEngine
{
	// Token: 0x02000033 RID: 51
	internal class PolicyConfigurationEntry<TTemplateProvisioningPolicy, TEnforcementProvisioningPolicy> where TTemplateProvisioningPolicy : TemplateProvisioningPolicy, new() where TEnforcementProvisioningPolicy : EnforcementProvisioningPolicy, new()
	{
		// Token: 0x06000152 RID: 338 RVA: 0x000081BC File Offset: 0x000063BC
		public IEnumerable<TTemplateProvisioningPolicy> GetTemplateProvisioningPolicy(IConfigurationSession session, OrganizationId organizationId, Type poType, ProvisioningPolicyType policyType, int maxResults, ProvisioningCache provisioningCache)
		{
			return this.GetProvisioningPolicy<TTemplateProvisioningPolicy>(session, organizationId, poType, policyType, maxResults, provisioningCache);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000081CD File Offset: 0x000063CD
		public IEnumerable<TEnforcementProvisioningPolicy> GetEnforcementProvisioningPolicy(IConfigurationSession session, OrganizationId organizationId, Type poType, ProvisioningPolicyType policyType, int maxResults, ProvisioningCache provisioningCache)
		{
			return this.GetProvisioningPolicy<TEnforcementProvisioningPolicy>(session, organizationId, poType, policyType, maxResults, provisioningCache);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000081F4 File Offset: 0x000063F4
		private IEnumerable<TProvisioningPolicy> GetProvisioningPolicy<TProvisioningPolicy>(IConfigurationSession session, OrganizationId organizationId, Type poType, ProvisioningPolicyType policyType, int maxResults, ProvisioningCache provisioningCache) where TProvisioningPolicy : ADProvisioningPolicy, new()
		{
			ADObjectId adobjectId = organizationId.ConfigurationUnit;
			QueryFilter queryFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADProvisioningPolicySchema.PolicyType, policyType),
				new ComparisonFilter(ComparisonOperator.Equal, ADProvisioningPolicySchema.TargetObjects, ProvisioningHelper.GetProvisioningObjectTag(poType))
			});
			if (organizationId.ConfigurationUnit == null)
			{
				adobjectId = provisioningCache.TryAddAndGetGlobalData<ADObjectId>(CannedProvisioningCacheKeys.FirstOrgContainerId, () => session.GetOrgContainerId());
			}
			else
			{
				SharedConfiguration sharedConfiguration = SharedConfiguration.GetSharedConfiguration(organizationId);
				if (sharedConfiguration != null)
				{
					adobjectId = sharedConfiguration.SharedConfigurationCU.Id;
					session = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(session.ConsistencyMode, sharedConfiguration.GetSharedConfigurationSessionSettings(), 86, "GetProvisioningPolicy", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\PolicyEngine\\PolicyConfigurationEntry.cs");
				}
				else
				{
					session = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(session.ConsistencyMode, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 90, "GetProvisioningPolicy", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\PolicyEngine\\PolicyConfigurationEntry.cs");
					queryFilter = QueryFilter.AndTogether(new QueryFilter[]
					{
						queryFilter,
						new ComparisonFilter(ComparisonOperator.Equal, ADProvisioningPolicySchema.Scopes, adobjectId)
					});
				}
			}
			return session.FindPaged<TProvisioningPolicy>(adobjectId.GetDescendantId(ADProvisioningPolicy.RdnContainer), QueryScope.SubTree, queryFilter, null, maxResults).ReadAllPages();
		}
	}
}
