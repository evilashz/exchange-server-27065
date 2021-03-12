using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200011F RID: 287
	internal abstract class SubWorkItemBase
	{
		// Token: 0x0600080D RID: 2061 RVA: 0x00017F88 File Offset: 0x00016188
		public SubWorkItemBase(SyncJob syncJob, SyncChangeInfo changeInfo = null)
		{
			ArgumentValidator.ThrowIfNull("syncJob", syncJob);
			this.SyncJob = syncJob;
			this.IsFullSync = this.SyncJob.CurrentWorkItem.FullSyncForTenant;
			this.ChangeInfo = changeInfo;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00017FBF File Offset: 0x000161BF
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x00017FC7 File Offset: 0x000161C7
		public SyncChangeInfo ChangeInfo { get; private set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00017FD0 File Offset: 0x000161D0
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x00017FD8 File Offset: 0x000161D8
		private protected bool IsFullSync { protected get; private set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00017FE1 File Offset: 0x000161E1
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x00017FE9 File Offset: 0x000161E9
		protected Dictionary<Guid, PolicyConfigBase> LocalObjectList { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00017FF2 File Offset: 0x000161F2
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x00017FFA File Offset: 0x000161FA
		private protected SyncJob SyncJob { protected get; private set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00018003 File Offset: 0x00016203
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x0001800B File Offset: 0x0001620B
		protected Exception LastError { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00018014 File Offset: 0x00016214
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x0001801C File Offset: 0x0001621C
		protected Action<SubWorkItemBase> ExternalCallback { get; set; }

		// Token: 0x0600081A RID: 2074
		public abstract void Execute();

		// Token: 0x0600081B RID: 2075
		public abstract void BeginExecute(Action<SubWorkItemBase> callback);

		// Token: 0x0600081C RID: 2076 RVA: 0x00018025 File Offset: 0x00016225
		public void EndExecute()
		{
			if (this.LastError != null)
			{
				throw this.LastError;
			}
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00018038 File Offset: 0x00016238
		internal static Guid? GetParentObjectId(PolicyConfigBase localObject)
		{
			if (localObject == null || localObject is PolicyAssociationConfig)
			{
				return null;
			}
			if (localObject is PolicyBindingSetConfig)
			{
				return new Guid?(((PolicyBindingSetConfig)localObject).PolicyDefinitionConfigId);
			}
			if (localObject is PolicyRuleConfig)
			{
				return new Guid?(((PolicyRuleConfig)localObject).PolicyDefinitionConfigId);
			}
			if (localObject is PolicyDefinitionConfig)
			{
				return new Guid?(localObject.Identity);
			}
			throw new NotSupportedException("object type " + localObject.GetType() + "is not supported");
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000180BC File Offset: 0x000162BC
		internal static Guid? GetParentObjectId(PolicyConfigurationBase deltaObject)
		{
			if (deltaObject == null || deltaObject is AssociationConfiguration)
			{
				return null;
			}
			if (deltaObject is BindingConfiguration)
			{
				return new Guid?(((BindingConfiguration)deltaObject).PolicyId);
			}
			if (deltaObject is RuleConfiguration)
			{
				return new Guid?(((RuleConfiguration)deltaObject).ParentPolicyId);
			}
			if (deltaObject is PolicyConfiguration)
			{
				return new Guid?(deltaObject.ObjectId);
			}
			throw new NotSupportedException("object type " + deltaObject.GetType() + "is not supported");
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001813E File Offset: 0x0001633E
		internal static Mode GetObjectMode(PolicyConfigBase localObject)
		{
			if (localObject is PolicyRuleConfig)
			{
				return ((PolicyRuleConfig)localObject).Mode;
			}
			if (localObject is PolicyDefinitionConfig)
			{
				return ((PolicyDefinitionConfig)localObject).Mode;
			}
			return Mode.Enforce;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001816C File Offset: 0x0001636C
		internal static Mode GetObjectMode(PolicyConfigurationBase deltaObject)
		{
			if (deltaObject is RuleConfiguration)
			{
				RuleConfiguration ruleConfiguration = (RuleConfiguration)deltaObject;
				if (ruleConfiguration != null && ruleConfiguration.Mode.Changed)
				{
					return ruleConfiguration.Mode.Value;
				}
			}
			else if (deltaObject is PolicyConfiguration)
			{
				PolicyConfiguration policyConfiguration = (PolicyConfiguration)deltaObject;
				if (policyConfiguration != null && policyConfiguration.Mode.Changed)
				{
					return policyConfiguration.Mode.Value;
				}
			}
			return Mode.Enforce;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000181D0 File Offset: 0x000163D0
		protected static UnifiedPolicyStatus CreateErrorStatus(ConfigurationObjectType objectType, Guid objectId, Guid? parentObjectId, PolicyVersion objectVersion, Mode mode)
		{
			return new UnifiedPolicyStatus
			{
				ObjectType = objectType,
				ObjectId = objectId,
				ParentObjectId = parentObjectId,
				Version = objectVersion,
				Mode = mode
			};
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00018208 File Offset: 0x00016408
		protected virtual void InternalCallback(IAsyncResult ar)
		{
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001820C File Offset: 0x0001640C
		protected void CommitObjectWrapper(Action commitObjectDelegate, UnifiedPolicyStatus errorStatusInfo)
		{
			try
			{
				commitObjectDelegate();
			}
			catch (SyncAgentTransientException ex)
			{
				if (!ex.IsPerObjectException || !this.SyncJob.IsLastTry)
				{
					throw;
				}
				UnifiedPolicyStatus unifiedPolicyStatus = this.FillInErrorStatus(errorStatusInfo, ex);
				this.SyncJob.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", this.SyncJob.CurrentWorkItem.TenantContext.TenantId.ToString(), this.SyncJob.CurrentWorkItem.ExternalIdentity, ExecutionLog.EventType.Information, "Unified Policy Sync Agent Publish Status", unifiedPolicyStatus.ToString(), null, new KeyValuePair<string, object>[0]);
				this.SyncJob.PolicyConfigProvider.PublishStatus(new UnifiedPolicyStatus[]
				{
					unifiedPolicyStatus
				});
				this.SyncJob.Errors.Add(ex);
				this.SyncJob.MonitorEventTracker.ReportObjectLevelFailure(ex, errorStatusInfo.ObjectType, errorStatusInfo.ParentObjectId);
			}
			catch (SyncAgentPermanentException ex2)
			{
				UnifiedPolicyStatus unifiedPolicyStatus2 = this.FillInErrorStatus(errorStatusInfo, ex2);
				this.SyncJob.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", this.SyncJob.CurrentWorkItem.TenantContext.TenantId.ToString(), this.SyncJob.CurrentWorkItem.ExternalIdentity, ExecutionLog.EventType.Information, "Unified Policy Sync Agent Publish Status", unifiedPolicyStatus2.ToString(), null, new KeyValuePair<string, object>[0]);
				this.SyncJob.PolicyConfigProvider.PublishStatus(new UnifiedPolicyStatus[]
				{
					unifiedPolicyStatus2
				});
				if (!ex2.IsPerObjectException)
				{
					throw;
				}
				this.SyncJob.Errors.Add(ex2);
				this.SyncJob.MonitorEventTracker.ReportObjectLevelFailure(ex2, errorStatusInfo.ObjectType, errorStatusInfo.ParentObjectId);
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000183D8 File Offset: 0x000165D8
		protected bool ShouldProcessDelta(PolicyConfigurationBase deltaObject)
		{
			if (deltaObject == null)
			{
				return true;
			}
			Workload workload = this.SyncJob.CurrentWorkItem.Workload;
			if ((deltaObject.Workload & workload) == workload)
			{
				return true;
			}
			switch (deltaObject.ObjectType)
			{
			case ConfigurationObjectType.Policy:
			case ConfigurationObjectType.Rule:
				return true;
			case ConfigurationObjectType.Association:
			case ConfigurationObjectType.Binding:
				this.SyncJob.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", this.SyncJob.CurrentWorkItem.TenantContext.TenantId.ToString(), this.SyncJob.CurrentWorkItem.ExternalIdentity, ExecutionLog.EventType.Information, "Unified Policy Sync Object Skipped", string.Format(CultureInfo.InvariantCulture, "Unified Policy Sync Object Skipped for type {0} object {1} because it doesn't apply to the current workload", new object[]
				{
					deltaObject.ObjectType,
					deltaObject.ObjectId
				}), null, new KeyValuePair<string, object>[0]);
				return false;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000184C0 File Offset: 0x000166C0
		protected void ProcessDelta(PolicyConfigurationBase deltaObject, Guid deltaObjectId, SyncChangeInfo changeInfo)
		{
			if (deltaObject == null)
			{
				if (ChangeType.Delete != changeInfo.ChangeType)
				{
					SyncAgentTransientException ex = new SyncAgentTransientException("Due to ffo DB replica delay, the latest change of Object " + this.ChangeInfo.ObjectId.Value + " can not be retrieved. To be retried in the next sync cycle", true, SyncAgentErrorCode.Generic);
					throw ex;
				}
				if (ConfigurationObjectType.Binding != this.ChangeInfo.ObjectType)
				{
					PolicyVersion policyVersion = this.SaveObject(deltaObject, this.ChangeInfo.ObjectType, deltaObjectId, true);
					return;
				}
				SyncAgentTransientException ex2 = new SyncAgentTransientException("Due to ffo DB replica delay, the latest change of Object " + this.ChangeInfo.ObjectId.Value + " can not be retrieved. To be retried in the next sync cycle", true, SyncAgentErrorCode.Generic);
				throw ex2;
			}
			else
			{
				bool flag = ChangeType.Delete == deltaObject.ChangeType;
				PolicyVersion policyVersion = this.SaveObject(deltaObject, this.ChangeInfo.ObjectType, deltaObjectId, flag);
				PolicyVersion version = this.ChangeInfo.Version;
				bool flag2 = this.ChangeInfo.ObjectId != null && !flag && policyVersion != null && version != null && policyVersion.CompareTo(version) < 0;
				if (flag2)
				{
					SyncAgentTransientException ex3 = new SyncAgentTransientException("Due to ffo DB replica delay, the latest change of Object " + this.ChangeInfo.ObjectId.Value + " can not be retrieved. To be retried in the next sync cycle", true, SyncAgentErrorCode.Generic);
					throw ex3;
				}
				return;
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00018608 File Offset: 0x00016808
		protected PolicyConfigBase GetWrapper(ConfigurationObjectType objectType, Guid objectSearchId)
		{
			switch (objectType)
			{
			case ConfigurationObjectType.Policy:
				return this.SyncJob.PolicyConfigProvider.GetWrapper(objectSearchId, this.SyncJob.MonitorEventTracker);
			case ConfigurationObjectType.Rule:
				return this.SyncJob.PolicyConfigProvider.GetWrapper(objectSearchId, this.SyncJob.MonitorEventTracker);
			case ConfigurationObjectType.Association:
				return this.SyncJob.PolicyConfigProvider.GetWrapper(objectSearchId, this.SyncJob.MonitorEventTracker);
			case ConfigurationObjectType.Binding:
				return this.SyncJob.PolicyConfigProvider.GetWrapper(objectSearchId, this.SyncJob.MonitorEventTracker);
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000186A8 File Offset: 0x000168A8
		protected IEnumerable<PolicyConfigBase> GetAllWrapper(ConfigurationObjectType objectType, IEnumerable<Guid> policyIds = null)
		{
			switch (objectType)
			{
			case ConfigurationObjectType.Policy:
				return this.SyncJob.PolicyConfigProvider.GetAllWrapper(this.SyncJob.MonitorEventTracker, policyIds);
			case ConfigurationObjectType.Rule:
				return this.SyncJob.PolicyConfigProvider.GetAllWrapper(this.SyncJob.MonitorEventTracker, policyIds);
			case ConfigurationObjectType.Association:
				return this.SyncJob.PolicyConfigProvider.GetAllWrapper(this.SyncJob.MonitorEventTracker, policyIds);
			case ConfigurationObjectType.Binding:
				return this.SyncJob.PolicyConfigProvider.GetAllWrapper(this.SyncJob.MonitorEventTracker, policyIds);
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00018748 File Offset: 0x00016948
		protected PolicyConfigBase NewBlankConfigInstanceWrapper(ConfigurationObjectType objectType, Guid objectId)
		{
			switch (objectType)
			{
			case ConfigurationObjectType.Policy:
				return this.SyncJob.PolicyConfigProvider.NewBlankConfigInstanceWrapper(objectId);
			case ConfigurationObjectType.Rule:
				return this.SyncJob.PolicyConfigProvider.NewBlankConfigInstanceWrapper(objectId);
			case ConfigurationObjectType.Association:
				return this.SyncJob.PolicyConfigProvider.NewBlankConfigInstanceWrapper(objectId);
			case ConfigurationObjectType.Binding:
				return this.SyncJob.PolicyConfigProvider.NewBlankConfigInstanceWrapper(objectId);
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000187BC File Offset: 0x000169BC
		protected void OnObjectDeleted(PolicyConfigBase obj)
		{
			this.OnObjectDeleted(obj.Identity);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000187CA File Offset: 0x000169CA
		protected void OnObjectAddedOrUpdated(PolicyConfigBase obj)
		{
			if (this.IsFullSync)
			{
				this.LocalObjectList.Remove(obj.Identity);
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000187E8 File Offset: 0x000169E8
		protected void LogDeltaObjectFromMasterStore(PolicyConfigurationBase deltaObject)
		{
			if (deltaObject != null)
			{
				BindingConfiguration bindingConfiguration = deltaObject as BindingConfiguration;
				if (bindingConfiguration != null && bindingConfiguration.AppliedScopes != null && bindingConfiguration.AppliedScopes.Changed)
				{
					IEnumerable<ScopeConfiguration>[] array = new IEnumerable<ScopeConfiguration>[]
					{
						bindingConfiguration.AppliedScopes.RemovedValues,
						bindingConfiguration.AppliedScopes.ChangedValues
					};
					foreach (IEnumerable<ScopeConfiguration> enumerable in array)
					{
						if (enumerable != null)
						{
							foreach (ScopeConfiguration deltaObject2 in enumerable)
							{
								this.LogDeltaObjectFromMasterStore(deltaObject2);
							}
						}
					}
				}
				object wholeProperty = Utility.GetWholeProperty(deltaObject, "PolicyScenario");
				string text = (wholeProperty != null) ? string.Format(CultureInfo.InvariantCulture, "; scenario: {0}.", new object[]
				{
					wholeProperty.ToString()
				}) : string.Empty;
				object obj;
				string text2 = Utility.GetIncrementalProperty(deltaObject, "Mode", out obj) ? string.Format(CultureInfo.InvariantCulture, "; mode: {0}.", new object[]
				{
					obj.ToString()
				}) : string.Empty;
				this.SyncJob.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", this.SyncJob.CurrentWorkItem.TenantContext.TenantId.ToString(), this.SyncJob.CurrentWorkItem.ExternalIdentity, ExecutionLog.EventType.Information, "Unified Policy Sync Get Object From EOP", string.Format(CultureInfo.InvariantCulture, "Unified Policy Sync Get Object From EOP: type: {0}; object id: {1}; object version: {2} {3} {4}", new object[]
				{
					deltaObject.ObjectType,
					deltaObject.ObjectId,
					deltaObject.Version,
					text,
					text2
				}), null, new KeyValuePair<string, object>[0]);
				return;
			}
			this.SyncJob.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", this.SyncJob.CurrentWorkItem.TenantContext.TenantId.ToString(), this.SyncJob.CurrentWorkItem.ExternalIdentity, ExecutionLog.EventType.Information, "Unified Policy Sync ObjectSync Get Nothing From EOP", string.Format(CultureInfo.InvariantCulture, "Unified Policy Sync ObjectSync Get Nothing From EOP for {0} Object {1} of Version {2} of ChangeType {3}", new object[]
			{
				this.ChangeInfo.ObjectType,
				(this.ChangeInfo.ObjectId != null) ? this.ChangeInfo.ObjectId.Value.ToString() : string.Empty,
				this.ChangeInfo.Version,
				this.ChangeInfo.ChangeType
			}), null, new KeyValuePair<string, object>[0]);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00018AC8 File Offset: 0x00016CC8
		protected void LogDeltaObjectCollectionFromMasterStore(PolicyChange policyChanges)
		{
			if (policyChanges == null || policyChanges.Changes == null || !policyChanges.Changes.Any<PolicyConfigurationBase>())
			{
				this.SyncJob.LogProvider.LogOneEntry("UnifiedPolicySyncAgent", this.SyncJob.CurrentWorkItem.TenantContext.TenantId.ToString(), this.SyncJob.CurrentWorkItem.ExternalIdentity, ExecutionLog.EventType.Information, "Unified Policy Sync TypeSync Get Nothing From EOP", string.Format(CultureInfo.InvariantCulture, "Unified Policy Sync TypeSync Get Nothing From EOP for Type {0}", new object[]
				{
					this.ChangeInfo.ObjectType
				}), null, new KeyValuePair<string, object>[0]);
				return;
			}
			foreach (PolicyConfigurationBase deltaObject in policyChanges.Changes)
			{
				this.LogDeltaObjectFromMasterStore(deltaObject);
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00018BB0 File Offset: 0x00016DB0
		private void OnObjectDeleted(Guid objId)
		{
			this.SyncJob.DeletedObjectList.Add(objId);
			if (this.IsFullSync)
			{
				this.LocalObjectList.Remove(objId);
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00018BDC File Offset: 0x00016DDC
		private PolicyVersion SaveObject(PolicyConfigurationBase deltaObject, ConfigurationObjectType objectType, Guid deltaObjectId, bool isDelete)
		{
			PolicyVersion result = null;
			PolicyConfigBase policyConfigBase = this.LoadObject(objectType, deltaObjectId, deltaObject);
			if (isDelete)
			{
				if (policyConfigBase != null)
				{
					this.SyncJob.PolicyConfigProvider.DeleteWrapper(policyConfigBase, new Action<PolicyConfigBase>(this.OnObjectDeleted), this.SyncJob.MonitorEventTracker);
				}
				else
				{
					this.OnObjectDeleted(deltaObjectId);
				}
			}
			else
			{
				if (policyConfigBase == null)
				{
					policyConfigBase = this.NewBlankConfigInstanceWrapper(objectType, deltaObjectId);
				}
				PolicyVersion version = policyConfigBase.Version;
				result = version;
				if (null == version || (null != version && version.CompareTo(deltaObject.Version) <= 0))
				{
					deltaObject.MergeInto(policyConfigBase, this.ChangeInfo.ObjectId != null, this.SyncJob.PolicyConfigProvider);
					this.SyncJob.PolicyConfigProvider.SaveWrapper(policyConfigBase, new Func<Guid, bool>(this.IsObjectAlreadyDeleted), new Action<PolicyConfigBase>(this.OnObjectAddedOrUpdated), this.SyncJob.MonitorEventTracker);
					result = deltaObject.Version;
				}
				else if (this.IsFullSync)
				{
					this.LocalObjectList.Remove(policyConfigBase.Identity);
				}
			}
			return result;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00018CE8 File Offset: 0x00016EE8
		private PolicyConfigBase LoadObject(ConfigurationObjectType objectType, Guid objectId, PolicyConfigurationBase deltaObject)
		{
			if (this.IsFullSync)
			{
				if (ConfigurationObjectType.Binding == objectType)
				{
					PolicyConfigBase policyConfigBase = null;
					foreach (PolicyConfigBase policyConfigBase2 in this.LocalObjectList.Values)
					{
						PolicyBindingSetConfig policyBindingSetConfig = (PolicyBindingSetConfig)policyConfigBase2;
						if (policyBindingSetConfig.PolicyDefinitionConfigId == ((BindingConfiguration)deltaObject).PolicyId)
						{
							policyConfigBase = policyBindingSetConfig;
							break;
						}
					}
					if (policyConfigBase != null)
					{
						this.LocalObjectList.Remove(policyConfigBase.Identity);
						policyConfigBase.Identity = deltaObject.ObjectId;
						this.LocalObjectList[policyConfigBase.Identity] = policyConfigBase;
					}
					return policyConfigBase;
				}
				if (!this.LocalObjectList.ContainsKey(objectId))
				{
					return null;
				}
				return this.LocalObjectList[objectId];
			}
			else
			{
				Guid objectSearchId = this.GetObjectSearchId(objectType, objectId, deltaObject);
				if (!(objectSearchId == Guid.Empty))
				{
					return this.GetWrapper(objectType, objectSearchId);
				}
				return null;
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00018DE4 File Offset: 0x00016FE4
		private Guid GetObjectSearchId(ConfigurationObjectType objectType, Guid objectId, PolicyConfigurationBase deltaObject)
		{
			if (ConfigurationObjectType.Binding != objectType)
			{
				return objectId;
			}
			if (deltaObject != null)
			{
				return ((BindingConfiguration)deltaObject).PolicyId;
			}
			return Guid.Empty;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00018E00 File Offset: 0x00017000
		private bool IsObjectAlreadyDeleted(Guid id)
		{
			return this.SyncJob.DeletedObjectList.Contains(id);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00018E14 File Offset: 0x00017014
		private UnifiedPolicyStatus FillInErrorStatus(UnifiedPolicyStatus errorStatusInfo, Exception ex)
		{
			errorStatusInfo.TenantId = this.SyncJob.CurrentWorkItem.TenantContext.TenantId;
			errorStatusInfo.Workload = this.SyncJob.CurrentWorkItem.Workload;
			errorStatusInfo.ErrorCode = UnifiedPolicyErrorCode.InternalError;
			AdditionalDiagnostics additionalDiagnostics = new AdditionalDiagnostics(Environment.MachineName, ex);
			errorStatusInfo.AdditionalDiagnostics = additionalDiagnostics.Serialize();
			errorStatusInfo.WhenProcessedUTC = DateTime.UtcNow;
			return errorStatusInfo;
		}
	}
}
