using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.DefaultProvisioningAgent.PolicyEngine
{
	// Token: 0x02000036 RID: 54
	[ImmutableObject(true)]
	internal class DefaultPolicyDataProvider : IPolicyDataProvider
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00008682 File Offset: 0x00006882
		public DefaultPolicyDataProvider(IConfigurationSession scSession)
		{
			if (scSession == null)
			{
				throw new ArgumentNullException("scSession");
			}
			this.scSession = scSession;
			this.scSession.SessionSettings.IsSharedConfigChecked = true;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008720 File Offset: 0x00006920
		public IEnumerable<ADProvisioningPolicy> GetEffectiveProvisioningPolicy(OrganizationId organizationId, Type poType, ProvisioningPolicyType policyType, int maxResults, ProvisioningCache provisioningCache)
		{
			if (null == poType)
			{
				throw new ArgumentNullException("poType");
			}
			if ((policyType & ~(ProvisioningPolicyType.Template | ProvisioningPolicyType.Enforcement)) != (ProvisioningPolicyType)0)
			{
				throw new ArgumentOutOfRangeException("policyType");
			}
			if (!PolicyConfiguration.ObjectType2PolicyEntryDictionary.ContainsKey(poType))
			{
				return null;
			}
			if (policyType == ProvisioningPolicyType.Template)
			{
				PolicyConfigurationEntry<RecipientTemplateProvisioningPolicy, RecipientEnforcementProvisioningPolicy> policyConfigurationEntry = PolicyConfiguration.ObjectType2PolicyEntryDictionary[poType];
				if (policyConfigurationEntry == null)
				{
					return null;
				}
				return policyConfigurationEntry.GetTemplateProvisioningPolicy(this.ConfigurationSession, organizationId, poType, policyType, maxResults, provisioningCache);
			}
			else
			{
				if (policyType != ProvisioningPolicyType.Enforcement)
				{
					throw new ArgumentOutOfRangeException("policyType");
				}
				PolicyConfigurationEntry<RecipientTemplateProvisioningPolicy, RecipientEnforcementProvisioningPolicy> policyConfigEntry = PolicyConfiguration.ObjectType2PolicyEntryDictionary[poType];
				if (policyConfigEntry == null)
				{
					return null;
				}
				Guid enforcementProvisioningPolicies = CannedProvisioningCacheKeys.EnforcementProvisioningPolicies;
				return provisioningCache.TryAddAndGetOrganizationDictionaryValue<IEnumerable<ADProvisioningPolicy>, string>(enforcementProvisioningPolicies, organizationId, poType.Name, () => policyConfigEntry.GetEnforcementProvisioningPolicy(this.ConfigurationSession, organizationId, poType, policyType, maxResults, provisioningCache));
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00008875 File Offset: 0x00006A75
		public IConfigurationSession ConfigurationSession
		{
			get
			{
				return this.scSession;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000887D File Offset: 0x00006A7D
		public string Source
		{
			get
			{
				return this.scSession.DomainController;
			}
		}

		// Token: 0x040000AA RID: 170
		private readonly IConfigurationSession scSession;
	}
}
