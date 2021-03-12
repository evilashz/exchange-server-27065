using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E8A RID: 3722
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PolicyConfigConverter<TPolicyConfig, TPolicyStorage> : PolicyConfigConverterBase where TPolicyConfig : PolicyConfigBase where TPolicyStorage : UnifiedPolicyStorageBase, new()
	{
		// Token: 0x06008190 RID: 33168 RVA: 0x0023646C File Offset: 0x0023466C
		public PolicyConfigConverter(ADPropertyDefinition policyIdProperty, ConfigurationObjectType configurationObjectType, Action<TPolicyStorage, TPolicyConfig> copyPropertiesToPolicyConfigDelegate, Action<TPolicyConfig, TPolicyStorage> copyPropertiesToStorageDelegate) : base(typeof(TPolicyConfig), configurationObjectType, typeof(TPolicyStorage), policyIdProperty)
		{
			ArgumentValidator.ThrowIfNull("copyPropertiesToPolicyConfigDelegate", copyPropertiesToPolicyConfigDelegate);
			ArgumentValidator.ThrowIfNull("copyPropertiesToStorageDelegate", copyPropertiesToStorageDelegate);
			this.copyPropertiesToPolicyConfigDelegate = copyPropertiesToPolicyConfigDelegate;
			this.copyPropertiesToStorageDelegate = copyPropertiesToStorageDelegate;
		}

		// Token: 0x06008191 RID: 33169 RVA: 0x002364BB File Offset: 0x002346BB
		public override Func<QueryFilter, ObjectId, bool, SortBy, IConfigurable[]> GetFindStorageObjectsDelegate(ExPolicyConfigProvider provider)
		{
			return new Func<QueryFilter, ObjectId, bool, SortBy, IConfigurable[]>(provider.Find<TPolicyStorage>);
		}

		// Token: 0x06008192 RID: 33170 RVA: 0x002364CC File Offset: 0x002346CC
		public override PolicyConfigBase ConvertFromStorage(ExPolicyConfigProvider provider, UnifiedPolicyStorageBase storageObject)
		{
			ArgumentValidator.ThrowIfNull("provider", provider);
			ArgumentValidator.ThrowIfNull("storageObject", storageObject);
			PolicyConfigBase policyConfigBase = provider.NewBlankConfigInstance<TPolicyConfig>();
			if (!provider.ReadOnly)
			{
				policyConfigBase.RawObject = storageObject;
			}
			Guid identity = storageObject.Guid;
			if (!ExPolicyConfigProvider.IsFFOOnline)
			{
				identity = storageObject.MasterIdentity;
			}
			policyConfigBase.Identity = identity;
			policyConfigBase.Name = storageObject.Name;
			policyConfigBase.Version = PolicyVersion.Create(storageObject.PolicyVersion);
			policyConfigBase.Workload = storageObject.Workload;
			policyConfigBase.WhenChangedUTC = storageObject.WhenChangedUTC;
			policyConfigBase.WhenCreatedUTC = storageObject.WhenChangedUTC;
			this.copyPropertiesToPolicyConfigDelegate((TPolicyStorage)((object)storageObject), (TPolicyConfig)((object)policyConfigBase));
			policyConfigBase.ResetChangeTracking();
			return policyConfigBase;
		}

		// Token: 0x06008193 RID: 33171 RVA: 0x00236588 File Offset: 0x00234788
		public override UnifiedPolicyStorageBase ConvertToStorage(ExPolicyConfigProvider provider, PolicyConfigBase policyConfig)
		{
			ArgumentValidator.ThrowIfNull("provider", provider);
			ArgumentValidator.ThrowIfNull("storageObject", policyConfig);
			UnifiedPolicyStorageBase unifiedPolicyStorageBase = policyConfig.RawObject as TPolicyStorage;
			if (unifiedPolicyStorageBase == null)
			{
				unifiedPolicyStorageBase = Activator.CreateInstance<TPolicyStorage>();
				unifiedPolicyStorageBase.OrganizationId = provider.GetOrganizationId();
				if (ExPolicyConfigProvider.IsFFOOnline)
				{
					unifiedPolicyStorageBase.SetId(new ADObjectId(PolicyStorage.PoliciesContainer.GetChildId(policyConfig.Name).DistinguishedName, policyConfig.Identity));
				}
				else
				{
					PolicyRuleConfig policyRuleConfig = policyConfig as PolicyRuleConfig;
					ADObjectId policyConfigContainer = provider.GetPolicyConfigContainer((policyRuleConfig == null) ? null : new Guid?(policyRuleConfig.PolicyDefinitionConfigId));
					unifiedPolicyStorageBase.SetId(policyConfigContainer.GetChildId(policyConfig.Name));
					unifiedPolicyStorageBase.MasterIdentity = policyConfig.Identity;
				}
			}
			else if ((ExPolicyConfigProvider.IsFFOOnline && policyConfig.Identity != unifiedPolicyStorageBase.Guid) || (!ExPolicyConfigProvider.IsFFOOnline && policyConfig.Identity != unifiedPolicyStorageBase.MasterIdentity))
			{
				throw new PolicyConfigProviderPermanentException(ServerStrings.ErrorCouldNotUpdateMasterIdentityProperty(policyConfig.Name));
			}
			if (policyConfig.Version != null)
			{
				unifiedPolicyStorageBase.PolicyVersion = policyConfig.Version.InternalStorage;
			}
			unifiedPolicyStorageBase.Name = policyConfig.Name;
			if (unifiedPolicyStorageBase.Workload != policyConfig.Workload)
			{
				unifiedPolicyStorageBase.Workload = policyConfig.Workload;
			}
			this.copyPropertiesToStorageDelegate((TPolicyConfig)((object)policyConfig), (TPolicyStorage)((object)unifiedPolicyStorageBase));
			return unifiedPolicyStorageBase;
		}

		// Token: 0x040056FF RID: 22271
		private Action<TPolicyStorage, TPolicyConfig> copyPropertiesToPolicyConfigDelegate;

		// Token: 0x04005700 RID: 22272
		private Action<TPolicyConfig, TPolicyStorage> copyPropertiesToStorageDelegate;
	}
}
