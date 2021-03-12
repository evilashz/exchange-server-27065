using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Exchange.Hygiene.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000113 RID: 275
	internal static class UnifiedPolicyStorageFactory
	{
		// Token: 0x06000A93 RID: 2707 RVA: 0x000202CC File Offset: 0x0001E4CC
		public static PolicyStorage ToPolicyStorage(PolicyConfiguration policy)
		{
			PolicyStorage policyStorage = new PolicyStorage();
			policyStorage[ADObjectSchema.OrganizationalUnitRoot] = new ADObjectId(policy.TenantId);
			policyStorage.Name = policy.Name;
			policyStorage.SetId((ADObjectId)DalHelper.ConvertFromStoreObject(policy.ObjectId, typeof(ADObjectId)));
			UnifiedPolicyStorageFactory.CopyPropertiesToStorage<PolicyConfiguration>(new TenantSettingFacade<PolicyStorage>(policyStorage), policy);
			return policyStorage;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00020334 File Offset: 0x0001E534
		public static RuleStorage ToRuleStorage(RuleConfiguration rule)
		{
			RuleStorage ruleStorage = new RuleStorage();
			ruleStorage[ADObjectSchema.OrganizationalUnitRoot] = new ADObjectId(rule.TenantId);
			ruleStorage.Name = rule.Name;
			ruleStorage.SetId((ADObjectId)DalHelper.ConvertFromStoreObject(rule.ObjectId, typeof(ADObjectId)));
			UnifiedPolicyStorageFactory.CopyPropertiesToStorage<RuleConfiguration>(new TenantSettingFacade<RuleStorage>(ruleStorage), rule);
			return ruleStorage;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000203A4 File Offset: 0x0001E5A4
		public static BindingStorage ToBindingStorage(BindingConfiguration binding)
		{
			BindingStorage bindingStorage = new BindingStorage();
			bindingStorage[ADObjectSchema.OrganizationalUnitRoot] = new ADObjectId(binding.TenantId);
			bindingStorage.Name = binding.Name;
			bindingStorage.SetId((ADObjectId)DalHelper.ConvertFromStoreObject(binding.ObjectId, typeof(ADObjectId)));
			UnifiedPolicyStorageFactory.CopyPropertiesToStorage<BindingConfiguration>(new TenantSettingFacade<BindingStorage>(bindingStorage), binding);
			if (binding.AppliedScopes != null && binding.AppliedScopes.Changed)
			{
				bindingStorage.AppliedScopes = new MultiValuedProperty<ScopeStorage>(from s in binding.AppliedScopes.ChangedValues
				select UnifiedPolicyStorageFactory.ToScopeStorage(s));
			}
			return bindingStorage;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00020458 File Offset: 0x0001E658
		public static AssociationStorage ToAssociationStorage(AssociationConfiguration association)
		{
			AssociationStorage associationStorage = new AssociationStorage();
			associationStorage[ADObjectSchema.OrganizationalUnitRoot] = new ADObjectId(association.TenantId);
			associationStorage.Name = association.Name;
			associationStorage.SetId((ADObjectId)DalHelper.ConvertFromStoreObject(association.ObjectId, typeof(ADObjectId)));
			UnifiedPolicyStorageFactory.CopyPropertiesToStorage<AssociationConfiguration>(new TenantSettingFacade<AssociationStorage>(associationStorage), association);
			return associationStorage;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000204C0 File Offset: 0x0001E6C0
		public static UnifiedPolicySettingStatus ToStatusStorage(UnifiedPolicyStatus status)
		{
			UnifiedPolicySettingStatus unifiedPolicySettingStatus = new UnifiedPolicySettingStatus();
			unifiedPolicySettingStatus[ADObjectSchema.OrganizationalUnitRoot] = new ADObjectId(status.TenantId);
			unifiedPolicySettingStatus.SetId((ADObjectId)DalHelper.ConvertFromStoreObject(status.ObjectId, typeof(ADObjectId)));
			unifiedPolicySettingStatus.SettingType = UnifiedPolicyStorageFactory.ConvertToSettingType(status.ObjectType);
			unifiedPolicySettingStatus.ParentObjectId = status.ParentObjectId;
			unifiedPolicySettingStatus.Container = status.Workload.ToString();
			unifiedPolicySettingStatus.ObjectVersion = status.Version.InternalStorage;
			unifiedPolicySettingStatus.ErrorCode = (int)status.ErrorCode;
			unifiedPolicySettingStatus.ErrorMessage = status.ErrorMessage;
			unifiedPolicySettingStatus.WhenProcessedUTC = status.WhenProcessedUTC;
			unifiedPolicySettingStatus.AdditionalDiagnostics = status.AdditionalDiagnostics;
			switch (status.Mode)
			{
			case Mode.PendingDeletion:
				unifiedPolicySettingStatus.ObjectStatus = StatusMode.PendingDeletion;
				break;
			case Mode.Deleted:
				unifiedPolicySettingStatus.ObjectStatus = StatusMode.Deleted;
				break;
			default:
				unifiedPolicySettingStatus.ObjectStatus = StatusMode.Active;
				break;
			}
			return unifiedPolicySettingStatus;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000205B8 File Offset: 0x0001E7B8
		public static ScopeStorage ToScopeStorage(ScopeConfiguration scope)
		{
			ScopeStorage scopeStorage = new ScopeStorage();
			scopeStorage[ADObjectSchema.OrganizationalUnitRoot] = new ADObjectId(scope.TenantId);
			scopeStorage.Name = scope.Name;
			scopeStorage.SetId((ADObjectId)DalHelper.ConvertFromStoreObject(scope.ObjectId, typeof(ADObjectId)));
			UnifiedPolicyStorageFactory.CopyPropertiesToStorage<ScopeConfiguration>(new TenantSettingFacade<ScopeStorage>(scopeStorage), scope);
			return scopeStorage;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00020620 File Offset: 0x0001E820
		public static PolicyConfiguration FromPolicyStorage(PolicyStorage policyStorage)
		{
			PolicyConfiguration policyConfiguration = new PolicyConfiguration(policyStorage.OrganizationalUnitRoot.ObjectGuid, policyStorage.Id.ObjectGuid);
			UnifiedPolicyStorageFactory.CopyPropertiesFromStorage<PolicyConfiguration>(policyConfiguration, new TenantSettingFacade<PolicyStorage>(policyStorage));
			return policyConfiguration;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00020658 File Offset: 0x0001E858
		public static RuleConfiguration FromRuleStorage(RuleStorage ruleStorage)
		{
			RuleConfiguration ruleConfiguration = new RuleConfiguration(ruleStorage.OrganizationalUnitRoot.ObjectGuid, ruleStorage.Id.ObjectGuid);
			UnifiedPolicyStorageFactory.CopyPropertiesFromStorage<RuleConfiguration>(ruleConfiguration, new TenantSettingFacade<RuleStorage>(ruleStorage));
			return ruleConfiguration;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00020690 File Offset: 0x0001E890
		public static AssociationConfiguration FromAssociationStorage(AssociationStorage associationStorage)
		{
			AssociationConfiguration associationConfiguration = new AssociationConfiguration(associationStorage.OrganizationalUnitRoot.ObjectGuid, associationStorage.Id.ObjectGuid);
			UnifiedPolicyStorageFactory.CopyPropertiesFromStorage<AssociationConfiguration>(associationConfiguration, new TenantSettingFacade<AssociationStorage>(associationStorage));
			return associationConfiguration;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000206D8 File Offset: 0x0001E8D8
		public static BindingConfiguration FromBindingStorage(BindingStorage bindingStorage)
		{
			BindingConfiguration bindingConfiguration = new BindingConfiguration(bindingStorage.OrganizationalUnitRoot.ObjectGuid, bindingStorage.Id.ObjectGuid);
			UnifiedPolicyStorageFactory.CopyPropertiesFromStorage<BindingConfiguration>(bindingConfiguration, new TenantSettingFacade<BindingStorage>(bindingStorage));
			if (bindingStorage.AppliedScopes.Any<ScopeStorage>() || bindingStorage.RemovedScopes.Any<ScopeStorage>())
			{
				bindingConfiguration.AppliedScopes = new IncrementalCollection<ScopeConfiguration>(from s in bindingStorage.AppliedScopes
				select UnifiedPolicyStorageFactory.FromScopeStorage(s), bindingStorage.RemovedScopes.Select((ScopeStorage s) => UnifiedPolicyStorageFactory.FromScopeStorage(s)));
			}
			else
			{
				bindingConfiguration.AppliedScopes = new IncrementalCollection<ScopeConfiguration>();
			}
			return bindingConfiguration;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00020790 File Offset: 0x0001E990
		public static ScopeConfiguration FromScopeStorage(ScopeStorage scopeStorage)
		{
			ScopeConfiguration scopeConfiguration = new ScopeConfiguration(scopeStorage.OrganizationalUnitRoot.ObjectGuid, scopeStorage.Id.ObjectGuid);
			UnifiedPolicyStorageFactory.CopyPropertiesFromStorage<ScopeConfiguration>(scopeConfiguration, new TenantSettingFacade<ScopeStorage>(scopeStorage));
			return scopeConfiguration;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000207F1 File Offset: 0x0001E9F1
		internal static IEnumerable<PropertyInfo> GetReflectedProperties<T>() where T : PolicyConfigurationBase
		{
			return from p in typeof(T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
			where p.GetCustomAttribute(typeof(DataMemberAttribute), false) != null && p.GetCustomAttribute(typeof(SkipReflectionMappingAttribute), false) == null
			select p;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00020815 File Offset: 0x0001EA15
		internal static bool PropertiesMatch(PropertyDefinition schemaProperty, PropertyInfo reflectedProperty)
		{
			return string.Equals(schemaProperty.Name, reflectedProperty.Name, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00020829 File Offset: 0x0001EA29
		internal static string GetRemovedCollectionPropertyName(PropertyDefinition schemaProperty)
		{
			return string.Format("_DELETED_{0}", schemaProperty.Name);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00020890 File Offset: 0x0001EA90
		private static void CopyPropertiesFromStorage<T>(T baseConfiguration, FacadeBase storage) where T : PolicyConfigurationBase
		{
			baseConfiguration.Name = (string)storage.InnerPropertyBag[ADObjectSchema.Name];
			baseConfiguration.Workload = (Workload)storage.InnerPropertyBag[UnifiedPolicyStorageBaseSchema.WorkloadProp];
			baseConfiguration.WhenCreatedUTC = (DateTime?)storage.InnerPropertyBag[ADObjectSchema.WhenCreatedUTC];
			baseConfiguration.WhenChangedUTC = (DateTime?)storage.InnerPropertyBag[ADObjectSchema.WhenChangedUTC];
			baseConfiguration.ChangeType = ((storage.InnerConfigurable.ObjectState == ObjectState.Deleted) ? ChangeType.Delete : ChangeType.Update);
			baseConfiguration.Version = PolicyVersion.Create((Guid)storage.InnerPropertyBag[UnifiedPolicyStorageBaseSchema.PolicyVersion]);
			IEnumerable<PropertyInfo> reflectedProperties = UnifiedPolicyStorageFactory.GetReflectedProperties<T>();
			UnifiedPolicyStorageFactory.InitializeIncrementalAttributes(baseConfiguration, reflectedProperties);
			IEnumerable<PropertyDefinition> propertyDefinitions = DalHelper.GetPropertyDefinitions(storage, false);
			using (IEnumerator<PropertyDefinition> enumerator = propertyDefinitions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PropertyDefinition property = enumerator.Current;
					object propertyValue;
					if (UnifiedPolicyStorageFactory.TryReadPropertyValue(storage, property, out propertyValue))
					{
						PropertyInfo propertyInfo = reflectedProperties.FirstOrDefault((PropertyInfo p) => UnifiedPolicyStorageFactory.PropertiesMatch(property, p));
						if (!(propertyInfo == null) && !UnifiedPolicyStorageFactory.IsIncrementalCollection(propertyInfo))
						{
							UnifiedPolicyStorageFactory.CopyPropertyFromStorage(propertyInfo, propertyValue, baseConfiguration);
						}
					}
				}
			}
			using (IEnumerator<PropertyInfo> enumerator2 = (from p in reflectedProperties
			where UnifiedPolicyStorageFactory.IsIncrementalCollection(p)
			select p).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					UnifiedPolicyStorageFactory.<>c__DisplayClassf<T> CS$<>8__locals2 = new UnifiedPolicyStorageFactory.<>c__DisplayClassf<T>();
					CS$<>8__locals2.incrementalCollectionProp = enumerator2.Current;
					PropertyDefinition addedProperty = propertyDefinitions.FirstOrDefault((PropertyDefinition p) => UnifiedPolicyStorageFactory.PropertiesMatch(p, CS$<>8__locals2.incrementalCollectionProp));
					PropertyDefinition propertyDefinition = propertyDefinitions.FirstOrDefault((PropertyDefinition p) => p.Name == UnifiedPolicyStorageFactory.GetRemovedCollectionPropertyName(addedProperty));
					if (addedProperty != null && propertyDefinition != null)
					{
						UnifiedPolicyStorageFactory.CopyIncrementalCollection(CS$<>8__locals2.incrementalCollectionProp, addedProperty, propertyDefinition, storage, baseConfiguration);
					}
				}
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00020AE8 File Offset: 0x0001ECE8
		private static void CopyPropertyFromStorage(PropertyInfo property, object propertyValue, PolicyConfigurationBase baseConfiguration)
		{
			if (UnifiedPolicyStorageFactory.IsIncrementalAttribute(property))
			{
				IncrementalAttributeBase incrementalAttribute = UnifiedPolicyStorageFactory.GetIncrementalAttribute(property, true, propertyValue);
				property.GetSetMethod().Invoke(baseConfiguration, new IncrementalAttributeBase[]
				{
					incrementalAttribute
				});
				return;
			}
			property.GetSetMethod().Invoke(baseConfiguration, new object[]
			{
				propertyValue
			});
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00020B3C File Offset: 0x0001ED3C
		private static void CopyIncrementalCollection(PropertyInfo incrementalProperty, PropertyDefinition addedProperty, PropertyDefinition removedProperty, FacadeBase storage, PolicyConfigurationBase baseConfiguration)
		{
			object obj = null;
			object obj2 = null;
			UnifiedPolicyStorageFactory.TryReadPropertyValue(storage, addedProperty, out obj);
			UnifiedPolicyStorageFactory.TryReadPropertyValue(storage, removedProperty, out obj2);
			if (obj != null || obj2 != null)
			{
				IncrementalAttributeBase incrementalCollection = UnifiedPolicyStorageFactory.GetIncrementalCollection(incrementalProperty, true, (MultiValuedPropertyBase)obj, (MultiValuedPropertyBase)obj2);
				incrementalProperty.GetSetMethod().Invoke(baseConfiguration, new IncrementalAttributeBase[]
				{
					incrementalCollection
				});
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00020BAC File Offset: 0x0001EDAC
		private static void CopyPropertiesToStorage<T>(FacadeBase storage, T baseConfiguration) where T : PolicyConfigurationBase
		{
			PropertyBag propertyBag = (storage.InnerPropertyBag as UnifiedPolicyStorageBase).propertyBag;
			propertyBag.SetField(UnifiedPolicyStorageBaseSchema.WorkloadProp, baseConfiguration.Workload);
			propertyBag.SetField(ADObjectSchema.WhenCreatedRaw, (baseConfiguration.WhenCreatedUTC != null) ? baseConfiguration.WhenCreatedUTC.Value.ToString("yyyyMMddHHmmss'.0Z'") : null);
			propertyBag.SetField(ADObjectSchema.WhenChangedRaw, (baseConfiguration.WhenChangedUTC != null) ? baseConfiguration.WhenChangedUTC.Value.ToString("yyyyMMddHHmmss'.0Z'") : null);
			propertyBag.SetField(UnifiedPolicyStorageBaseSchema.PolicyVersion, baseConfiguration.Version.InternalStorage);
			IEnumerable<PropertyDefinition> propertyDefinitions = DalHelper.GetPropertyDefinitions(storage, false);
			using (IEnumerator<PropertyInfo> enumerator = UnifiedPolicyStorageFactory.GetReflectedProperties<T>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PropertyInfo prop = enumerator.Current;
					PropertyDefinition propertyDefinition = propertyDefinitions.FirstOrDefault((PropertyDefinition p) => UnifiedPolicyStorageFactory.PropertiesMatch(p, prop));
					if (propertyDefinition != null)
					{
						UnifiedPolicyStorageFactory.CopyPropertyToStorage(propertyDefinition, prop, storage.InnerPropertyBag, baseConfiguration);
					}
				}
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00020D2C File Offset: 0x0001EF2C
		private static void CopyPropertyToStorage(PropertyDefinition schemaProperty, PropertyInfo property, IPropertyBag storage, PolicyConfigurationBase baseConfiguration)
		{
			object obj = property.GetGetMethod().Invoke(baseConfiguration, null);
			if (UnifiedPolicyStorageFactory.IsIncrementalAttribute(property) || UnifiedPolicyStorageFactory.IsIncrementalCollection(property))
			{
				IncrementalAttributeBase incrementalAttributeBase = (IncrementalAttributeBase)obj;
				if (incrementalAttributeBase != null && incrementalAttributeBase.Changed)
				{
					UnifiedPolicyStorageFactory.StoreValue(storage, schemaProperty, incrementalAttributeBase.GetObjectValue());
					return;
				}
			}
			else
			{
				UnifiedPolicyStorageFactory.StoreValue(storage, schemaProperty, obj);
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00020D80 File Offset: 0x0001EF80
		private static void StoreValue(IPropertyBag storage, PropertyDefinition property, object value)
		{
			if (property is ProviderPropertyDefinition && ((ProviderPropertyDefinition)property).IsMultivalued)
			{
				storage[property] = Activator.CreateInstance(typeof(MultiValuedProperty<>).MakeGenericType(new Type[]
				{
					property.Type
				}), new object[]
				{
					value
				});
				return;
			}
			storage[property] = value;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00020DF4 File Offset: 0x0001EFF4
		private static void InitializeIncrementalAttributes(PolicyConfigurationBase baseConfiguration, IEnumerable<PropertyInfo> allProperties)
		{
			foreach (PropertyInfo propertyInfo in from p in allProperties
			where UnifiedPolicyStorageFactory.IsIncrementalAttribute(p)
			select p)
			{
				IncrementalAttributeBase incrementalAttribute = UnifiedPolicyStorageFactory.GetIncrementalAttribute(propertyInfo, false, null);
				propertyInfo.GetSetMethod().Invoke(baseConfiguration, new IncrementalAttributeBase[]
				{
					incrementalAttribute
				});
			}
			foreach (PropertyInfo propertyInfo2 in from p in allProperties
			where UnifiedPolicyStorageFactory.IsIncrementalCollection(p)
			select p)
			{
				IncrementalAttributeBase incrementalCollection = UnifiedPolicyStorageFactory.GetIncrementalCollection(propertyInfo2, false, null, null);
				propertyInfo2.GetSetMethod().Invoke(baseConfiguration, new IncrementalAttributeBase[]
				{
					incrementalCollection
				});
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00020F00 File Offset: 0x0001F100
		private static bool IsIncrementalAttribute(PropertyInfo property)
		{
			return property.PropertyType.Name == typeof(IncrementalAttribute<>).Name;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00020F21 File Offset: 0x0001F121
		private static bool IsIncrementalCollection(PropertyInfo property)
		{
			return property.PropertyType.Name == typeof(IncrementalCollection<>).Name;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00020F42 File Offset: 0x0001F142
		private static bool TryReadPropertyValue(FacadeBase facade, PropertyDefinition property, out object propertyValue)
		{
			propertyValue = null;
			return (facade.InnerConfigurable as UnifiedPolicyStorageBase).propertyBag.TryGetField((ProviderPropertyDefinition)property, ref propertyValue);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00020F64 File Offset: 0x0001F164
		private static IncrementalAttributeBase GetIncrementalAttribute(PropertyInfo property, bool isChanged, object propertyValue)
		{
			if (isChanged)
			{
				return (IncrementalAttributeBase)Activator.CreateInstance(typeof(IncrementalAttribute<>).MakeGenericType(new Type[]
				{
					property.PropertyType.GenericTypeArguments[0]
				}), new object[]
				{
					propertyValue
				});
			}
			return (IncrementalAttributeBase)Activator.CreateInstance(typeof(IncrementalAttribute<>).MakeGenericType(new Type[]
			{
				property.PropertyType.GenericTypeArguments[0]
			}));
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00020FE4 File Offset: 0x0001F1E4
		private static IncrementalAttributeBase GetIncrementalCollection(PropertyInfo property, bool isChanged, MultiValuedPropertyBase addedValues, MultiValuedPropertyBase removedValues)
		{
			IncrementalAttributeBase incrementalAttributeBase = (IncrementalAttributeBase)Activator.CreateInstance(typeof(IncrementalCollection<>).MakeGenericType(new Type[]
			{
				property.PropertyType.GenericTypeArguments[0]
			}));
			if (isChanged)
			{
				IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[]
				{
					property.PropertyType.GenericTypeArguments[0]
				}));
				IList list2 = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[]
				{
					property.PropertyType.GenericTypeArguments[0]
				}));
				if (addedValues != null)
				{
					foreach (object value in ((IEnumerable)addedValues))
					{
						list.Add(value);
					}
				}
				if (removedValues != null)
				{
					foreach (object value2 in ((IEnumerable)removedValues))
					{
						list2.Add(value2);
					}
				}
				return (IncrementalAttributeBase)Activator.CreateInstance(typeof(IncrementalCollection<>).MakeGenericType(new Type[]
				{
					property.PropertyType.GenericTypeArguments[0]
				}), new IList[]
				{
					list,
					list2
				});
			}
			return (IncrementalAttributeBase)Activator.CreateInstance(typeof(IncrementalCollection<>).MakeGenericType(new Type[]
			{
				property.PropertyType.GenericTypeArguments[0]
			}));
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000211AC File Offset: 0x0001F3AC
		private static string ConvertToSettingType(ConfigurationObjectType objectType)
		{
			switch (objectType)
			{
			case ConfigurationObjectType.Policy:
				return typeof(PolicyStorage).Name;
			case ConfigurationObjectType.Rule:
				return typeof(RuleStorage).Name;
			case ConfigurationObjectType.Association:
				return typeof(AssociationStorage).Name;
			case ConfigurationObjectType.Binding:
				return typeof(BindingStorage).Name;
			case ConfigurationObjectType.Scope:
				return typeof(ScopeStorage).Name;
			default:
				throw new NotSupportedException(string.Format("Object type {0} not supported", objectType));
			}
		}
	}
}
