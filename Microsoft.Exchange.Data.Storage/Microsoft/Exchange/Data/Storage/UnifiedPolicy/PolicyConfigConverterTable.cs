using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E8B RID: 3723
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class PolicyConfigConverterTable
	{
		// Token: 0x06008194 RID: 33172 RVA: 0x00236B68 File Offset: 0x00234D68
		static PolicyConfigConverterTable()
		{
			PolicyConfigConverterBase[] array = new PolicyConfigConverterBase[4];
			array[0] = new PolicyConfigConverter<PolicyDefinitionConfig, PolicyStorage>(ExPolicyConfigProvider.IsFFOOnline ? ADObjectSchema.Id : UnifiedPolicyStorageBaseSchema.MasterIdentity, ConfigurationObjectType.Policy, delegate(PolicyStorage storage, PolicyDefinitionConfig policyConfig)
			{
				policyConfig.Mode = storage.Mode;
				policyConfig.Scenario = storage.Scenario;
				policyConfig.DefaultPolicyRuleConfigId = storage.DefaultRuleId;
				policyConfig.Comment = storage.Comments;
				policyConfig.Description = storage.Description;
				policyConfig.Enabled = storage.IsEnabled;
				policyConfig.CreatedBy = storage.CreatedBy;
				policyConfig.LastModifiedBy = storage.LastModifiedBy;
			}, delegate(PolicyDefinitionConfig policyConfig, PolicyStorage storage)
			{
				if (policyConfig.Mode != storage.Mode)
				{
					storage.Mode = policyConfig.Mode;
				}
				storage.Scenario = policyConfig.Scenario;
				storage.DefaultRuleId = policyConfig.DefaultPolicyRuleConfigId;
				storage.Comments = policyConfig.Comment;
				storage.Description = policyConfig.Description;
				storage.IsEnabled = policyConfig.Enabled;
				storage.CreatedBy = policyConfig.CreatedBy;
				storage.LastModifiedBy = policyConfig.LastModifiedBy;
			});
			array[1] = new PolicyConfigConverter<PolicyRuleConfig, RuleStorage>(RuleStorageSchema.ParentPolicyId, ConfigurationObjectType.Rule, delegate(RuleStorage storage, PolicyRuleConfig policyConfig)
			{
				policyConfig.Mode = storage.Mode;
				policyConfig.PolicyDefinitionConfigId = storage.ParentPolicyId;
				policyConfig.Priority = storage.Priority;
				policyConfig.RuleBlob = storage.RuleBlob;
				policyConfig.Comment = storage.Comments;
				policyConfig.Description = storage.Description;
				policyConfig.Enabled = storage.IsEnabled;
				policyConfig.CreatedBy = storage.CreatedBy;
				policyConfig.LastModifiedBy = storage.LastModifiedBy;
				policyConfig.Scenario = storage.Scenario;
			}, delegate(PolicyRuleConfig policyConfig, RuleStorage storage)
			{
				if (policyConfig.Mode != storage.Mode)
				{
					storage.Mode = policyConfig.Mode;
				}
				storage.ParentPolicyId = policyConfig.PolicyDefinitionConfigId;
				storage.Priority = policyConfig.Priority;
				storage.RuleBlob = policyConfig.RuleBlob;
				storage.Comments = policyConfig.Comment;
				storage.Description = policyConfig.Description;
				storage.IsEnabled = policyConfig.Enabled;
				storage.CreatedBy = policyConfig.CreatedBy;
				storage.LastModifiedBy = policyConfig.LastModifiedBy;
				storage.Scenario = policyConfig.Scenario;
			});
			array[2] = new PolicyConfigConverter<PolicyBindingSetConfig, BindingStorage>(BindingStorageSchema.PolicyId, ConfigurationObjectType.Binding, delegate(BindingStorage storage, PolicyBindingSetConfig policyConfig)
			{
				policyConfig.PolicyDefinitionConfigId = storage.PolicyId;
				List<PolicyBindingConfig> list = new List<PolicyBindingConfig>(storage.AppliedScopes.Count);
				foreach (ScopeStorage storageScope in storage.AppliedScopes)
				{
					PolicyBindingConfig policyBindingConfig = PolicyConfigConverterTable.ToBindingScope(storageScope);
					policyBindingConfig.PolicyDefinitionConfigId = storage.PolicyId;
					list.Add(policyBindingConfig);
				}
				policyConfig.AppliedScopes = list;
			}, delegate(PolicyBindingSetConfig policyConfig, BindingStorage storage)
			{
				PolicyConfigConverterTable.<>c__DisplayClass12 CS$<>8__locals1 = new PolicyConfigConverterTable.<>c__DisplayClass12();
				CS$<>8__locals1.storage = storage;
				CS$<>8__locals1.storage.PolicyId = policyConfig.PolicyDefinitionConfigId;
				IEnumerable<PolicyBindingConfig> enumerable = policyConfig.AppliedScopes ?? ((IEnumerable<PolicyBindingConfig>)Array<PolicyBindingConfig>.Empty);
				int index;
				for (index = CS$<>8__locals1.storage.AppliedScopes.Count - 1; index >= 0; index--)
				{
					if (!enumerable.Any((PolicyBindingConfig bindingScope) => PolicyConfigConverterTable.IsSameScope(bindingScope, CS$<>8__locals1.storage.AppliedScopes[index])))
					{
						CS$<>8__locals1.storage.AppliedScopes.RemoveAt(index);
					}
				}
				using (IEnumerator<PolicyBindingConfig> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PolicyBindingConfig bindingScope = enumerator.Current;
						ScopeStorage scopeStorage = CS$<>8__locals1.storage.AppliedScopes.FirstOrDefault((ScopeStorage scope) => PolicyConfigConverterTable.IsSameScope(bindingScope, scope));
						if (scopeStorage == null)
						{
							CS$<>8__locals1.storage.AppliedScopes.Add(PolicyConfigConverterTable.ToStorageScope(bindingScope, CS$<>8__locals1.storage.OrganizationalUnitRoot));
						}
						else
						{
							PolicyConfigConverterTable.UpdateStorageScope(scopeStorage, bindingScope);
						}
					}
				}
			});
			array[3] = new PolicyConfigConverter<PolicyAssociationConfig, AssociationStorage>(AssociationStorageSchema.PolicyIds, ConfigurationObjectType.Association, delegate(AssociationStorage storage, PolicyAssociationConfig policyConfig)
			{
				throw new NotImplementedException("Not in v1 of unified policy.");
			}, delegate(PolicyAssociationConfig policyConfig, AssociationStorage storage)
			{
				throw new NotImplementedException("Not in v1 of unified policy.");
			});
			PolicyConfigConverterTable.policyConverters = array;
		}

		// Token: 0x06008195 RID: 33173 RVA: 0x00236D18 File Offset: 0x00234F18
		public static PolicyConfigConverterBase GetConverterByType(Type type, bool throwException = true)
		{
			PolicyConfigConverterBase policyConfigConverterBase = PolicyConfigConverterTable.policyConverters.FirstOrDefault(delegate(PolicyConfigConverterBase entry)
			{
				if (typeof(UnifiedPolicyStorageBase).IsAssignableFrom(type))
				{
					return type.Equals(entry.StorageType);
				}
				return typeof(PolicyConfigBase).IsAssignableFrom(type) && type.Equals(entry.PolicyConfigType);
			});
			if (policyConfigConverterBase == null && throwException)
			{
				throw new InvalidOperationException(string.Format("Type {0} has no converter.", type.FullName));
			}
			return policyConfigConverterBase;
		}

		// Token: 0x06008196 RID: 33174 RVA: 0x00236D8C File Offset: 0x00234F8C
		public static ConfigurationObjectType GetConfigurationObjectType(UnifiedPolicyStorageBase policyStorageObject)
		{
			ArgumentValidator.ThrowIfNull("policyStorageObject", policyStorageObject);
			PolicyConfigConverterBase policyConfigConverterBase = PolicyConfigConverterTable.policyConverters.FirstOrDefault((PolicyConfigConverterBase entry) => policyStorageObject.GetType().Equals(entry.StorageType));
			if (policyConfigConverterBase == null)
			{
				throw new InvalidOperationException(string.Format("Type {0} has no converter.", policyStorageObject.GetType()));
			}
			return policyConfigConverterBase.ConfigurationObjectType;
		}

		// Token: 0x06008197 RID: 33175 RVA: 0x00236DF1 File Offset: 0x00234FF1
		private static bool IsSameScope(PolicyBindingConfig bindingScope, ScopeStorage storageScope)
		{
			return bindingScope.Identity == (ExPolicyConfigProvider.IsFFOOnline ? storageScope.Guid : storageScope.MasterIdentity);
		}

		// Token: 0x06008198 RID: 33176 RVA: 0x00236E14 File Offset: 0x00235014
		private static void UpdateStorageScope(ScopeStorage storageScope, PolicyBindingConfig bindingScope)
		{
			storageScope.Name = bindingScope.Name;
			if (storageScope.Mode != bindingScope.Mode)
			{
				storageScope.Mode = bindingScope.Mode;
			}
			storageScope.Scope = BindingMetadata.ToStorage(bindingScope.Scope);
			storageScope.PolicyVersion = ((bindingScope.Version != null) ? bindingScope.Version.InternalStorage : Guid.Empty);
		}

		// Token: 0x06008199 RID: 33177 RVA: 0x00236E80 File Offset: 0x00235080
		private static ScopeStorage ToStorageScope(PolicyBindingConfig bindingScope, ADObjectId organizationalUnitRoot)
		{
			ScopeStorage scopeStorage = new ScopeStorage();
			scopeStorage[ADObjectSchema.OrganizationalUnitRoot] = organizationalUnitRoot;
			scopeStorage.SetId(new ADObjectId(PolicyStorage.PoliciesContainer.GetChildId(bindingScope.Identity.ToString()).DistinguishedName, bindingScope.Identity));
			scopeStorage.MasterIdentity = bindingScope.Identity;
			PolicyConfigConverterTable.UpdateStorageScope(scopeStorage, bindingScope);
			return scopeStorage;
		}

		// Token: 0x0600819A RID: 33178 RVA: 0x00236EE8 File Offset: 0x002350E8
		private static PolicyBindingConfig ToBindingScope(ScopeStorage storageScope)
		{
			PolicyBindingConfig policyBindingConfig = new PolicyBindingConfig();
			policyBindingConfig.Identity = (ExPolicyConfigProvider.IsFFOOnline ? storageScope.Guid : storageScope.MasterIdentity);
			policyBindingConfig.Name = storageScope.Name;
			policyBindingConfig.Mode = storageScope.Mode;
			policyBindingConfig.Scope = BindingMetadata.FromStorage(storageScope.Scope);
			policyBindingConfig.Version = PolicyVersion.Create(storageScope.PolicyVersion);
			policyBindingConfig.ResetChangeTracking();
			return policyBindingConfig;
		}

		// Token: 0x04005701 RID: 22273
		private static PolicyConfigConverterBase[] policyConverters;
	}
}
