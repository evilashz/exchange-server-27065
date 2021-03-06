using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.CompliancePolicy.Monitor;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000125 RID: 293
	internal static class PolicyConfigProviderHelper
	{
		// Token: 0x0600084A RID: 2122 RVA: 0x00019980 File Offset: 0x00017B80
		public static void SaveWrapper(this PolicyConfigProvider policyConfigProvider, PolicyConfigBase configObject, Func<Guid, bool> isObjectAlreadyDeleted, Action<PolicyConfigBase> onObjectSaved, SyncMonitorEventTracker monitorTracker)
		{
			Type type = PolicyConfigProviderHelper.TryGetWorkloadCommonType(configObject.GetType());
			if (type != null && PolicyConfigProviderHelper.RoutineTable.ContainsKey(type))
			{
				if (type == typeof(PolicyDefinitionConfig))
				{
					PolicyDefinitionConfig policyDefinitionConfig = (PolicyDefinitionConfig)configObject;
					Guid? defaultPolicyRuleConfigId = policyDefinitionConfig.DefaultPolicyRuleConfigId;
					if (policyDefinitionConfig.ObjectState == ChangeType.Add && defaultPolicyRuleConfigId != null)
					{
						policyDefinitionConfig.DefaultPolicyRuleConfigId = null;
						PolicyConfigProviderHelper.InternalWriteObjects(policyConfigProvider, new List<PolicyConfigBase>
						{
							policyDefinitionConfig
						}, onObjectSaved, null, monitorTracker);
						policyDefinitionConfig = policyConfigProvider.GetWrapper(policyDefinitionConfig.Identity, monitorTracker);
						policyDefinitionConfig.DefaultPolicyRuleConfigId = defaultPolicyRuleConfigId;
						configObject = policyDefinitionConfig;
					}
				}
				List<PolicyConfigBase> objectsToWrite = PolicyConfigProviderHelper.RoutineTable[type].Item1(policyConfigProvider, configObject, isObjectAlreadyDeleted, monitorTracker);
				PolicyConfigProviderHelper.InternalWriteObjects(policyConfigProvider, objectsToWrite, onObjectSaved, null, monitorTracker);
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00019A58 File Offset: 0x00017C58
		public static void DeleteWrapper(this PolicyConfigProvider policyConfigProvider, PolicyConfigBase configObject, Action<PolicyConfigBase> onObjectDeleted, SyncMonitorEventTracker monitorTracker)
		{
			Type type = PolicyConfigProviderHelper.TryGetWorkloadCommonType(configObject.GetType());
			if (type != null && PolicyConfigProviderHelper.RoutineTable.ContainsKey(type))
			{
				List<PolicyConfigBase> objectsToWrite = PolicyConfigProviderHelper.RoutineTable[type].Item2(policyConfigProvider, configObject, monitorTracker);
				PolicyConfigProviderHelper.InternalWriteObjects(policyConfigProvider, objectsToWrite, null, onObjectDeleted, monitorTracker);
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00019B10 File Offset: 0x00017D10
		public static T GetWrapper<T>(this PolicyConfigProvider policyConfigProvider, Guid objectId, SyncMonitorEventTracker monitorTracker) where T : PolicyConfigBase
		{
			Type objectType = PolicyConfigProviderHelper.TryGetWorkloadCommonType(typeof(T));
			if (objectType != null && PolicyConfigProviderHelper.RoutineTable.ContainsKey(objectType))
			{
				T result = default(T);
				monitorTracker.TrackLatencyWrapper(LatencyType.CrudMgr, PolicyConfigProviderHelper.GetObjectType(objectType), delegate()
				{
					result = ((PolicyConfigProviderHelper.GetDelegate<T>)PolicyConfigProviderHelper.RoutineTable[objectType].Item3)(policyConfigProvider, objectId);
				}, true);
				return result;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00019C0C File Offset: 0x00017E0C
		public static IEnumerable<PolicyConfigBase> GetAllWrapper<T>(this PolicyConfigProvider policyConfigProvider, SyncMonitorEventTracker monitorTracker, IEnumerable<Guid> policyIds = null) where T : PolicyConfigBase
		{
			Type objectType = PolicyConfigProviderHelper.TryGetWorkloadCommonType(typeof(T));
			if (objectType != null && PolicyConfigProviderHelper.RoutineTable.ContainsKey(objectType))
			{
				IEnumerable<PolicyConfigBase> result = null;
				monitorTracker.TrackLatencyWrapper(LatencyType.CrudMgr, PolicyConfigProviderHelper.GetObjectType(objectType), delegate()
				{
					result = ((PolicyConfigProviderHelper.GetAllDelegate<T>)PolicyConfigProviderHelper.RoutineTable[objectType].Item4)(policyConfigProvider, policyIds);
				}, true);
				return result;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00019CA4 File Offset: 0x00017EA4
		public static T NewBlankConfigInstanceWrapper<T>(this PolicyConfigProvider policyConfigProvider, Guid objectId) where T : PolicyConfigBase
		{
			T result = policyConfigProvider.NewBlankConfigInstance<T>();
			result.Identity = objectId;
			result.Name = result.Identity.ToString();
			result.Version = PolicyVersion.Empty;
			return result;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00019D01 File Offset: 0x00017F01
		private static T InternalGetObject<T>(PolicyConfigProvider policyConfigProvider, Guid objectId) where T : PolicyConfigBase
		{
			return policyConfigProvider.FindByIdentity<T>(objectId);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00019D0C File Offset: 0x00017F0C
		private static T InternalGetObjectByPolicyId<T>(PolicyConfigProvider policyConfigProvider, Guid policyId) where T : PolicyConfigBase
		{
			T result = default(T);
			IEnumerable<T> enumerable = policyConfigProvider.FindByPolicyDefinitionConfigId<T>(policyId);
			if (enumerable != null && enumerable.Any<T>())
			{
				result = enumerable.First<T>();
			}
			return result;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00019D3C File Offset: 0x00017F3C
		private static IEnumerable<PolicyConfigBase> InternalGetAllObjects<T>(PolicyConfigProvider policyConfigProvider, IEnumerable<Guid> policyIds) where T : PolicyConfigBase
		{
			return policyConfigProvider.FindByName<T>("*");
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00019D4C File Offset: 0x00017F4C
		private static IEnumerable<PolicyConfigBase> InternalGetAllObjectsByPolicyIds<T>(PolicyConfigProvider policyConfigProvider, IEnumerable<Guid> policyIds) where T : PolicyConfigBase
		{
			List<PolicyConfigBase> list = new List<PolicyConfigBase>();
			if (policyIds != null)
			{
				foreach (Guid policyDefinitionConfigId in policyIds)
				{
					IEnumerable<PolicyConfigBase> enumerable = policyConfigProvider.FindByPolicyDefinitionConfigId<T>(policyDefinitionConfigId);
					if (enumerable != null)
					{
						list.AddRange(enumerable);
					}
				}
			}
			return list;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00019E4C File Offset: 0x0001804C
		private static void InternalWriteObjects(PolicyConfigProvider policyConfigProvider, List<PolicyConfigBase> objectsToWrite, Action<PolicyConfigBase> onObjectSaved, Action<PolicyConfigBase> onObjectDeleted, SyncMonitorEventTracker monitorTracker)
		{
			if (objectsToWrite == null || !objectsToWrite.Any<PolicyConfigBase>())
			{
				return;
			}
			using (List<PolicyConfigBase>.Enumerator enumerator = objectsToWrite.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PolicyConfigBase objectToWrite = enumerator.Current;
					monitorTracker.TrackLatencyWrapper(LatencyType.CrudMgr, PolicyConfigProviderHelper.GetObjectType(PolicyConfigProviderHelper.TryGetWorkloadCommonType(objectToWrite.GetType())), delegate()
					{
						if (ChangeType.Delete == objectToWrite.ObjectState)
						{
							policyConfigProvider.Delete(objectToWrite);
							if (onObjectDeleted != null)
							{
								onObjectDeleted(objectToWrite);
								return;
							}
						}
						else
						{
							policyConfigProvider.Save(objectToWrite);
							if (onObjectSaved != null)
							{
								onObjectSaved(objectToWrite);
							}
						}
					}, true);
				}
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00019F00 File Offset: 0x00018100
		private static List<PolicyConfigBase> GetSavePolicyObjects(PolicyConfigProvider policyConfigProvider, PolicyConfigBase configObject, Func<Guid, bool> isObjectAlreadyDeleted, SyncMonitorEventTracker monitorTracker)
		{
			PolicyDefinitionConfig policyDefinitionConfig = (PolicyDefinitionConfig)configObject;
			Guid? defaultPolicyRuleConfigId = policyDefinitionConfig.DefaultPolicyRuleConfigId;
			List<PolicyConfigBase> list = new List<PolicyConfigBase>();
			if (!policyDefinitionConfig.IsModified(PolicyDefinitionConfigSchema.DefaultPolicyRuleConfigId) || defaultPolicyRuleConfigId == null)
			{
				list.Add(policyDefinitionConfig);
				return list;
			}
			if (policyConfigProvider.GetWrapper(defaultPolicyRuleConfigId.Value, monitorTracker) == null)
			{
				if (!isObjectAlreadyDeleted(defaultPolicyRuleConfigId.Value))
				{
					PolicyRuleConfig policyRuleConfig = policyConfigProvider.NewBlankConfigInstanceWrapper(defaultPolicyRuleConfigId.Value);
					policyRuleConfig.PolicyDefinitionConfigId = policyDefinitionConfig.Identity;
					list.Add(policyRuleConfig);
				}
				else
				{
					defaultPolicyRuleConfigId = null;
				}
			}
			policyDefinitionConfig.DefaultPolicyRuleConfigId = defaultPolicyRuleConfigId;
			list.Add(policyDefinitionConfig);
			return list;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00019F9C File Offset: 0x0001819C
		private static List<PolicyConfigBase> GetSaveRuleObjects(PolicyConfigProvider policyConfigProvider, PolicyConfigBase configObject, Func<Guid, bool> isObjectAlreadyDeleted, SyncMonitorEventTracker monitorTracker)
		{
			PolicyRuleConfig policyRuleConfig = (PolicyRuleConfig)configObject;
			Guid policyDefinitionConfigId = policyRuleConfig.PolicyDefinitionConfigId;
			if (isObjectAlreadyDeleted(policyDefinitionConfigId))
			{
				return null;
			}
			List<PolicyConfigBase> list = new List<PolicyConfigBase>();
			if (!policyRuleConfig.IsModified(PolicyRuleConfigSchema.PolicyDefinitionConfigId))
			{
				list.Add(policyRuleConfig);
				return list;
			}
			if (policyConfigProvider.GetWrapper(policyDefinitionConfigId, monitorTracker) == null)
			{
				PolicyDefinitionConfig item = policyConfigProvider.NewBlankConfigInstanceWrapper(policyRuleConfig.PolicyDefinitionConfigId);
				list.Add(item);
			}
			list.Add(policyRuleConfig);
			return list;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001A008 File Offset: 0x00018208
		private static List<PolicyConfigBase> GetSaveBindingSetObjects(PolicyConfigProvider policyConfigProvider, PolicyConfigBase configObject, Func<Guid, bool> isObjectAlreadyDeleted, SyncMonitorEventTracker monitorTracker)
		{
			PolicyBindingSetConfig policyBindingSetConfig = (PolicyBindingSetConfig)configObject;
			Guid policyDefinitionConfigId = policyBindingSetConfig.PolicyDefinitionConfigId;
			if (isObjectAlreadyDeleted(policyDefinitionConfigId))
			{
				return null;
			}
			List<PolicyConfigBase> list = new List<PolicyConfigBase>();
			if (!policyBindingSetConfig.IsModified(PolicyBindingSetConfigSchema.PolicyDefinitionConfigId))
			{
				list.Add(policyBindingSetConfig);
				return list;
			}
			if (policyConfigProvider.GetWrapper(policyBindingSetConfig.PolicyDefinitionConfigId, monitorTracker) == null)
			{
				PolicyDefinitionConfig item = policyConfigProvider.NewBlankConfigInstanceWrapper(policyBindingSetConfig.PolicyDefinitionConfigId);
				list.Add(item);
			}
			list.Add(policyBindingSetConfig);
			return list;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001A078 File Offset: 0x00018278
		private static List<PolicyConfigBase> GetSaveAssociationObjects(PolicyConfigProvider policyConfigProvider, PolicyConfigBase configObject, Func<Guid, bool> isObjectAlreadyDeleted, SyncMonitorEventTracker monitorTracker)
		{
			PolicyAssociationConfig policyAssociationConfig = (PolicyAssociationConfig)configObject;
			IEnumerable<Guid> policyDefinitionConfigIds = policyAssociationConfig.PolicyDefinitionConfigIds;
			if (policyDefinitionConfigIds == null || !policyDefinitionConfigIds.Any<Guid>())
			{
				return null;
			}
			List<PolicyConfigBase> list = new List<PolicyConfigBase>();
			List<Guid> list2 = new List<Guid>();
			foreach (Guid guid in policyDefinitionConfigIds)
			{
				if (isObjectAlreadyDeleted(guid))
				{
					list2.Add(guid);
				}
				else if (policyConfigProvider.GetWrapper(guid, monitorTracker) == null)
				{
					PolicyDefinitionConfig item = policyConfigProvider.NewBlankConfigInstanceWrapper(guid);
					list.Add(item);
				}
			}
			if (list2.Any<Guid>())
			{
				List<Guid> list3 = policyDefinitionConfigIds.ToList<Guid>();
				if (list2.Count == list3.Count)
				{
					return null;
				}
				foreach (Guid item2 in list2)
				{
					list3.Remove(item2);
				}
				policyAssociationConfig.PolicyDefinitionConfigIds = list3;
				if (policyAssociationConfig.DefaultPolicyDefinitionConfigId != null && !list3.Contains(policyAssociationConfig.DefaultPolicyDefinitionConfigId.Value))
				{
					policyAssociationConfig.DefaultPolicyDefinitionConfigId = null;
				}
			}
			list.Add(policyAssociationConfig);
			return list;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001A1CC File Offset: 0x000183CC
		private static List<PolicyConfigBase> GetDeletePolicyObjects(PolicyConfigProvider policyConfigProvider, PolicyConfigBase configObject, SyncMonitorEventTracker monitorTracker)
		{
			PolicyDefinitionConfig policyDefinitionConfig = (PolicyDefinitionConfig)configObject;
			List<PolicyConfigBase> list = new List<PolicyConfigBase>();
			if (policyDefinitionConfig.DefaultPolicyRuleConfigId != null)
			{
				policyDefinitionConfig.DefaultPolicyRuleConfigId = null;
				list.Add(policyDefinitionConfig.Clone());
			}
			Guid identity = policyDefinitionConfig.Identity;
			foreach (Type right in new Type[]
			{
				typeof(PolicyAssociationConfig),
				typeof(PolicyBindingSetConfig),
				typeof(PolicyRuleConfig)
			})
			{
				IEnumerable<PolicyConfigBase> enumerable = null;
				if (typeof(PolicyAssociationConfig) == right)
				{
					enumerable = policyConfigProvider.GetAllWrapper(monitorTracker, new List<Guid>
					{
						identity
					});
				}
				else if (typeof(PolicyBindingSetConfig) == right)
				{
					enumerable = policyConfigProvider.GetAllWrapper(monitorTracker, new List<Guid>
					{
						identity
					});
				}
				else if (typeof(PolicyRuleConfig) == right)
				{
					enumerable = policyConfigProvider.GetAllWrapper(monitorTracker, new List<Guid>
					{
						identity
					});
				}
				if (enumerable != null)
				{
					foreach (PolicyConfigBase policyConfigBase in enumerable)
					{
						if (typeof(PolicyAssociationConfig) == right)
						{
							PolicyAssociationConfig policyAssociationConfig = (PolicyAssociationConfig)policyConfigBase;
							List<Guid> list2 = policyAssociationConfig.PolicyDefinitionConfigIds.ToList<Guid>();
							list2.Remove(identity);
							policyAssociationConfig.PolicyDefinitionConfigIds = list2;
							if (policyAssociationConfig.DefaultPolicyDefinitionConfigId != null && policyAssociationConfig.DefaultPolicyDefinitionConfigId.Value == identity)
							{
								policyAssociationConfig.DefaultPolicyDefinitionConfigId = null;
							}
							list.Add(policyConfigBase);
						}
						else
						{
							policyConfigBase.MarkAsDeleted();
							list.Add(policyConfigBase);
						}
					}
				}
			}
			policyDefinitionConfig.MarkAsDeleted();
			list.Add(policyDefinitionConfig);
			return list;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001A3E4 File Offset: 0x000185E4
		private static List<PolicyConfigBase> GetDeleteRuleObjects(PolicyConfigProvider policyConfigProvider, PolicyConfigBase configObject, SyncMonitorEventTracker monitorTracker)
		{
			PolicyRuleConfig policyRuleConfig = (PolicyRuleConfig)configObject;
			List<PolicyConfigBase> list = new List<PolicyConfigBase>();
			PolicyDefinitionConfig wrapper = policyConfigProvider.GetWrapper(policyRuleConfig.PolicyDefinitionConfigId, monitorTracker);
			if (wrapper != null && wrapper.DefaultPolicyRuleConfigId != null && policyRuleConfig.Identity == wrapper.DefaultPolicyRuleConfigId.Value)
			{
				wrapper.DefaultPolicyRuleConfigId = null;
				list.Add(wrapper);
			}
			policyRuleConfig.MarkAsDeleted();
			list.Add(policyRuleConfig);
			return list;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001A460 File Offset: 0x00018660
		private static List<PolicyConfigBase> DefaultGetDeleteObjects(PolicyConfigProvider policyConfigProvider, PolicyConfigBase configObject, SyncMonitorEventTracker monitorTracker)
		{
			configObject.MarkAsDeleted();
			return new List<PolicyConfigBase>
			{
				configObject
			};
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001A484 File Offset: 0x00018684
		private static Type TryGetWorkloadCommonType(Type objectType)
		{
			Type typeFromHandle = typeof(PolicyConfigBase);
			if (objectType.BaseType == typeFromHandle)
			{
				return objectType;
			}
			if (objectType.IsSubclassOf(typeFromHandle))
			{
				do
				{
					objectType = objectType.BaseType;
				}
				while (objectType.BaseType != typeFromHandle);
				return objectType;
			}
			return null;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001A4D0 File Offset: 0x000186D0
		private static ConfigurationObjectType GetObjectType(Type workloadCommonType)
		{
			if (workloadCommonType == typeof(PolicyDefinitionConfig))
			{
				return ConfigurationObjectType.Policy;
			}
			if (workloadCommonType == typeof(PolicyRuleConfig))
			{
				return ConfigurationObjectType.Rule;
			}
			if (workloadCommonType == typeof(PolicyBindingSetConfig) || workloadCommonType == typeof(PolicyBindingConfig))
			{
				return ConfigurationObjectType.Binding;
			}
			if (workloadCommonType == typeof(PolicyAssociationConfig))
			{
				return ConfigurationObjectType.Association;
			}
			throw new NotSupportedException();
		}

		// Token: 0x04000474 RID: 1140
		private static readonly IDictionary<Type, Tuple<PolicyConfigProviderHelper.GetSaveObjectsDelegate, PolicyConfigProviderHelper.GetDeleteObjectsDelegate, object, object>> RoutineTable = new Dictionary<Type, Tuple<PolicyConfigProviderHelper.GetSaveObjectsDelegate, PolicyConfigProviderHelper.GetDeleteObjectsDelegate, object, object>>
		{
			{
				typeof(PolicyDefinitionConfig),
				new Tuple<PolicyConfigProviderHelper.GetSaveObjectsDelegate, PolicyConfigProviderHelper.GetDeleteObjectsDelegate, object, object>(new PolicyConfigProviderHelper.GetSaveObjectsDelegate(PolicyConfigProviderHelper.GetSavePolicyObjects), new PolicyConfigProviderHelper.GetDeleteObjectsDelegate(PolicyConfigProviderHelper.GetDeletePolicyObjects), new PolicyConfigProviderHelper.GetDelegate<PolicyDefinitionConfig>(PolicyConfigProviderHelper.InternalGetObject<PolicyDefinitionConfig>), new PolicyConfigProviderHelper.GetAllDelegate<PolicyDefinitionConfig>(PolicyConfigProviderHelper.InternalGetAllObjects<PolicyDefinitionConfig>))
			},
			{
				typeof(PolicyRuleConfig),
				new Tuple<PolicyConfigProviderHelper.GetSaveObjectsDelegate, PolicyConfigProviderHelper.GetDeleteObjectsDelegate, object, object>(new PolicyConfigProviderHelper.GetSaveObjectsDelegate(PolicyConfigProviderHelper.GetSaveRuleObjects), new PolicyConfigProviderHelper.GetDeleteObjectsDelegate(PolicyConfigProviderHelper.GetDeleteRuleObjects), new PolicyConfigProviderHelper.GetDelegate<PolicyRuleConfig>(PolicyConfigProviderHelper.InternalGetObject<PolicyRuleConfig>), new PolicyConfigProviderHelper.GetAllDelegate<PolicyRuleConfig>(PolicyConfigProviderHelper.InternalGetAllObjectsByPolicyIds<PolicyRuleConfig>))
			},
			{
				typeof(PolicyBindingSetConfig),
				new Tuple<PolicyConfigProviderHelper.GetSaveObjectsDelegate, PolicyConfigProviderHelper.GetDeleteObjectsDelegate, object, object>(new PolicyConfigProviderHelper.GetSaveObjectsDelegate(PolicyConfigProviderHelper.GetSaveBindingSetObjects), new PolicyConfigProviderHelper.GetDeleteObjectsDelegate(PolicyConfigProviderHelper.DefaultGetDeleteObjects), new PolicyConfigProviderHelper.GetDelegate<PolicyBindingSetConfig>(PolicyConfigProviderHelper.InternalGetObjectByPolicyId<PolicyBindingSetConfig>), new PolicyConfigProviderHelper.GetAllDelegate<PolicyBindingSetConfig>(PolicyConfigProviderHelper.InternalGetAllObjectsByPolicyIds<PolicyBindingSetConfig>))
			},
			{
				typeof(PolicyAssociationConfig),
				new Tuple<PolicyConfigProviderHelper.GetSaveObjectsDelegate, PolicyConfigProviderHelper.GetDeleteObjectsDelegate, object, object>(new PolicyConfigProviderHelper.GetSaveObjectsDelegate(PolicyConfigProviderHelper.GetSaveAssociationObjects), new PolicyConfigProviderHelper.GetDeleteObjectsDelegate(PolicyConfigProviderHelper.DefaultGetDeleteObjects), new PolicyConfigProviderHelper.GetDelegate<PolicyAssociationConfig>(PolicyConfigProviderHelper.InternalGetObject<PolicyAssociationConfig>), new PolicyConfigProviderHelper.GetAllDelegate<PolicyAssociationConfig>(PolicyConfigProviderHelper.InternalGetAllObjects<PolicyAssociationConfig>))
			}
		};

		// Token: 0x02000126 RID: 294
		// (Invoke) Token: 0x0600085F RID: 2143
		private delegate List<PolicyConfigBase> GetSaveObjectsDelegate(PolicyConfigProvider policyConfigProvider, PolicyConfigBase configObject, Func<Guid, bool> isObjectAlreadyDeleted, SyncMonitorEventTracker monitorTracker);

		// Token: 0x02000127 RID: 295
		// (Invoke) Token: 0x06000863 RID: 2147
		private delegate List<PolicyConfigBase> GetDeleteObjectsDelegate(PolicyConfigProvider policyConfigProvider, PolicyConfigBase configObject, SyncMonitorEventTracker monitorTracker);

		// Token: 0x02000128 RID: 296
		// (Invoke) Token: 0x06000867 RID: 2151
		private delegate T GetDelegate<T>(PolicyConfigProvider policyConfigProvider, Guid objectId);

		// Token: 0x02000129 RID: 297
		// (Invoke) Token: 0x0600086B RID: 2155
		private delegate IEnumerable<PolicyConfigBase> GetAllDelegate<T>(PolicyConfigProvider policyConfigProvider, IEnumerable<Guid> policyIds = null);
	}
}
