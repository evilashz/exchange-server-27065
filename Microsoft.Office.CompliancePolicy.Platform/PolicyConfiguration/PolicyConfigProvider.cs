using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x020000A0 RID: 160
	public abstract class PolicyConfigProvider : IDisposable
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x0000C7F0 File Offset: 0x0000A9F0
		public PolicyConfigProvider() : this(null)
		{
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000C7F9 File Offset: 0x0000A9F9
		public PolicyConfigProvider(ExecutionLog logProvider)
		{
			this.logProvider = logProvider;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000405 RID: 1029 RVA: 0x0000C808 File Offset: 0x0000AA08
		// (remove) Token: 0x06000406 RID: 1030 RVA: 0x0000C840 File Offset: 0x0000AA40
		public event PolicyConfigChangeEventHandler PolicyConfigChanged;

		// Token: 0x06000407 RID: 1031 RVA: 0x0000C878 File Offset: 0x0000AA78
		public void LogOneEntry(string tenantId, ExecutionLog.EventType eventType, string objectType, string contextData, Exception exception)
		{
			if (this.logProvider != null)
			{
				this.logProvider.LogOneEntry("PolicyConfigProvider", tenantId, this.GetHashCode().ToString(), eventType, objectType, contextData, exception, new KeyValuePair<string, object>[0]);
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		public void LogOneEntry(ExecutionLog.EventType eventType, string contextData, Exception exception)
		{
			this.LogOneEntry(string.Empty, eventType, string.Empty, contextData, exception);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		public string GetLocalOrganizationId()
		{
			this.CheckDispose();
			string organizationId = null;
			this.WrapKnownException(delegate
			{
				organizationId = this.InternalGetLocalOrganizationId();
			});
			return organizationId;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000C968 File Offset: 0x0000AB68
		public T FindByIdentity<T>(Guid identity) where T : PolicyConfigBase
		{
			this.CheckDispose();
			T config = default(T);
			this.WrapKnownException(delegate
			{
				config = this.InternalFindByIdentity<T>(identity);
				if (config != null)
				{
					config.ResetChangeTracking();
				}
			});
			return config;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		public IEnumerable<T> FindByName<T>(string name) where T : PolicyConfigBase
		{
			this.CheckDispose();
			IEnumerable<T> configs = null;
			this.WrapKnownException(delegate
			{
				configs = this.InternalFindByName<T>(name);
				this.ResetChangeTrackingForCollection<T>(configs);
			});
			return configs;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000CA60 File Offset: 0x0000AC60
		public IEnumerable<T> FindByPolicyDefinitionConfigId<T>(Guid policyDefinitionConfigId) where T : PolicyConfigBase
		{
			this.CheckDispose();
			IEnumerable<T> configs = null;
			this.WrapKnownException(delegate
			{
				configs = this.InternalFindByPolicyDefinitionConfigId<T>(policyDefinitionConfigId);
				this.ResetChangeTrackingForCollection<T>(configs);
			});
			return configs;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		public IEnumerable<PolicyBindingConfig> FindPolicyBindingConfigsByScopes(IEnumerable<string> scopes)
		{
			this.CheckDispose();
			IEnumerable<PolicyBindingConfig> bindingConfigs = null;
			this.WrapKnownException(delegate
			{
				bindingConfigs = this.InternalFindPolicyBindingConfigsByScopes(scopes);
				this.ResetChangeTrackingForCollection<PolicyBindingConfig>(bindingConfigs);
			});
			return bindingConfigs;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000CB54 File Offset: 0x0000AD54
		public PolicyAssociationConfig FindPolicyAssociationConfigByScope(string scope)
		{
			this.CheckDispose();
			PolicyAssociationConfig associationConfig = null;
			this.WrapKnownException(delegate
			{
				associationConfig = this.InternalFindPolicyAssociationConfigByScope(scope);
				if (associationConfig != null)
				{
					associationConfig.ResetChangeTracking();
				}
			});
			return associationConfig;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000CD08 File Offset: 0x0000AF08
		public void Save(PolicyConfigBase instance)
		{
			this.CheckDispose();
			ArgumentValidator.ThrowIfNull("instance", instance);
			this.WrapKnownException(delegate
			{
				string name = instance.GetType().Name;
				object wholeProperty = Utility.GetWholeProperty(instance, "Scenario");
				string text = (wholeProperty != null) ? string.Format(CultureInfo.InvariantCulture, "Scenario: {0}.", new object[]
				{
					wholeProperty.ToString()
				}) : string.Empty;
				this.LogOneEntry(string.Empty, ExecutionLog.EventType.Information, name, string.Format(CultureInfo.InvariantCulture, "Begin Save: {0}. {1}", new object[]
				{
					instance.Identity,
					text
				}), null);
				this.InternalSave(instance);
				this.LogOneEntry(string.Empty, ExecutionLog.EventType.Information, name, string.Format(CultureInfo.InvariantCulture, "Begin Save Change Event: {0}. {1}", new object[]
				{
					instance.Identity,
					text
				}), null);
				this.OnPolicyConfigChanged(new PolicyConfigChangeEventArgs(this, instance, ChangeType.Update));
				instance.ResetChangeTracking();
				this.LogOneEntry(string.Empty, ExecutionLog.EventType.Information, name, string.Format(CultureInfo.InvariantCulture, "End Save: {0}. {1}", new object[]
				{
					instance.Identity,
					text
				}), null);
			});
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000CE5C File Offset: 0x0000B05C
		public void Delete(PolicyConfigBase instance)
		{
			this.CheckDispose();
			ArgumentValidator.ThrowIfNull("instance", instance);
			this.WrapKnownException(delegate
			{
				string name = instance.GetType().Name;
				object wholeProperty = Utility.GetWholeProperty(instance, "Scenario");
				string text = (wholeProperty != null) ? string.Format(CultureInfo.InvariantCulture, "Scenario: {0}.", new object[]
				{
					wholeProperty.ToString()
				}) : string.Empty;
				this.LogOneEntry(string.Empty, ExecutionLog.EventType.Information, name, string.Format(CultureInfo.InvariantCulture, "Begin Delete: {0}. {1}", new object[]
				{
					instance.Identity,
					text
				}), null);
				this.InternalDelete(instance);
				instance.MarkAsDeleted();
				this.LogOneEntry(string.Empty, ExecutionLog.EventType.Information, name, string.Format(CultureInfo.InvariantCulture, "End Delete: {0}. {1}", new object[]
				{
					instance.Identity,
					text
				}), null);
			});
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000CEC0 File Offset: 0x0000B0C0
		public void PublishStatus(IEnumerable<UnifiedPolicyStatus> statusUpdateNotifications)
		{
			ArgumentValidator.ThrowIfCollectionNullOrEmpty<UnifiedPolicyStatus>("statusUpdateNotifications", statusUpdateNotifications);
			this.CheckDispose();
			this.WrapKnownException(delegate
			{
				this.InternalPublishStatus(statusUpdateNotifications);
			});
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000CF24 File Offset: 0x0000B124
		public T NewBlankConfigInstance<T>() where T : PolicyConfigBase
		{
			this.CheckDispose();
			T config = default(T);
			this.WrapKnownException(delegate
			{
				config = this.InternalNewBlankConfigInstance<T>();
			});
			return config;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000CF68 File Offset: 0x0000B168
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000CF77 File Offset: 0x0000B177
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed && disposing)
			{
				this.isDisposed = true;
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000CF8B File Offset: 0x0000B18B
		protected void CheckDispose()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
		}

		// Token: 0x06000416 RID: 1046
		protected abstract string InternalGetLocalOrganizationId();

		// Token: 0x06000417 RID: 1047
		protected abstract T InternalFindByIdentity<T>(Guid identity) where T : PolicyConfigBase;

		// Token: 0x06000418 RID: 1048
		protected abstract IEnumerable<T> InternalFindByName<T>(string name) where T : PolicyConfigBase;

		// Token: 0x06000419 RID: 1049 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		protected virtual IEnumerable<T> InternalFindByPolicyDefinitionConfigId<T>(Guid policyDefinitionConfigId) where T : PolicyConfigBase
		{
			if (!typeof(PolicyBindingSetConfig).IsAssignableFrom(typeof(T)))
			{
				throw new InvalidOperationException();
			}
			IEnumerable<PolicyBindingConfig> enumerable = this.FindByPolicyDefinitionConfigId<PolicyBindingConfig>(policyDefinitionConfigId);
			if (enumerable == null || !enumerable.Any<PolicyBindingConfig>())
			{
				return null;
			}
			PolicyBindingSetConfig policyBindingSetConfig = this.NewBlankConfigInstance<PolicyBindingSetConfig>();
			policyBindingSetConfig.Identity = Guid.NewGuid();
			policyBindingSetConfig.PolicyDefinitionConfigId = policyDefinitionConfigId;
			policyBindingSetConfig.AppliedScopes = enumerable;
			policyBindingSetConfig.Version = PolicyVersion.Empty;
			policyBindingSetConfig.ResetChangeTracking();
			return new T[]
			{
				policyBindingSetConfig as T
			};
		}

		// Token: 0x0600041A RID: 1050
		protected abstract IEnumerable<PolicyBindingConfig> InternalFindPolicyBindingConfigsByScopes(IEnumerable<string> scopes);

		// Token: 0x0600041B RID: 1051
		protected abstract PolicyAssociationConfig InternalFindPolicyAssociationConfigByScope(string scope);

		// Token: 0x0600041C RID: 1052 RVA: 0x0000D034 File Offset: 0x0000B234
		protected virtual void InternalSave(PolicyConfigBase instance)
		{
			if (instance is PolicyBindingSetConfig)
			{
				PolicyBindingSetConfig policyBindingSetConfig = instance as PolicyBindingSetConfig;
				if (policyBindingSetConfig.AppliedScopes != null)
				{
					using (IEnumerator<PolicyBindingConfig> enumerator = policyBindingSetConfig.AppliedScopes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							PolicyBindingConfig policyBindingConfig = enumerator.Current;
							if (policyBindingConfig.ObjectState == ChangeType.Delete)
							{
								this.InternalDelete(policyBindingConfig);
							}
							else if (policyBindingConfig.ObjectState != ChangeType.None)
							{
								this.InternalSave(policyBindingConfig);
							}
						}
						return;
					}
					goto IL_61;
				}
				return;
			}
			IL_61:
			throw new InvalidOperationException();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		protected virtual void InternalDelete(PolicyConfigBase instance)
		{
			if (instance is PolicyBindingSetConfig)
			{
				PolicyBindingSetConfig policyBindingSetConfig = instance as PolicyBindingSetConfig;
				if (policyBindingSetConfig.AppliedScopes != null)
				{
					using (IEnumerator<PolicyBindingConfig> enumerator = policyBindingSetConfig.AppliedScopes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							PolicyBindingConfig instance2 = enumerator.Current;
							this.InternalDelete(instance2);
						}
						return;
					}
					goto IL_47;
				}
				return;
			}
			IL_47:
			throw new InvalidOperationException();
		}

		// Token: 0x0600041E RID: 1054
		protected abstract void InternalPublishStatus(IEnumerable<UnifiedPolicyStatus> statusUpdateNotifications);

		// Token: 0x0600041F RID: 1055 RVA: 0x0000D124 File Offset: 0x0000B324
		protected virtual T InternalNewBlankConfigInstance<T>() where T : PolicyConfigBase
		{
			if (typeof(T).Equals(typeof(PolicyDefinitionConfig)))
			{
				return new PolicyDefinitionConfig() as T;
			}
			if (typeof(T).Equals(typeof(PolicyRuleConfig)))
			{
				return new PolicyRuleConfig() as T;
			}
			if (typeof(T).Equals(typeof(PolicyBindingConfig)))
			{
				return new PolicyBindingConfig() as T;
			}
			if (typeof(T).Equals(typeof(PolicyBindingSetConfig)))
			{
				return new PolicyBindingSetConfig() as T;
			}
			if (typeof(T).Equals(typeof(PolicyAssociationConfig)))
			{
				return new PolicyAssociationConfig() as T;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000D210 File Offset: 0x0000B410
		protected void WrapKnownException(Action dowork)
		{
			try
			{
				dowork();
			}
			catch (Exception ex)
			{
				if (this.IsTransientException(ex))
				{
					bool flag = this.IsPerObjectException(ex);
					this.LogOneEntry(ExecutionLog.EventType.Warning, string.Format("Known transient exception, per object:{0}.", flag), ex);
					throw new PolicyConfigProviderTransientException(ex.Message, ex, flag, SyncAgentErrorCode.Generic);
				}
				if (this.IsPermanentException(ex))
				{
					bool flag2 = this.IsPerObjectException(ex);
					this.LogOneEntry(ExecutionLog.EventType.Warning, string.Format("Known permanent exception, per object:{0}.", flag2), ex);
					throw new PolicyConfigProviderPermanentException(ex.Message, ex, flag2, SyncAgentErrorCode.Generic);
				}
				this.LogOneEntry(ExecutionLog.EventType.Error, "Unhandled Exception", ex);
				throw;
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000D2B8 File Offset: 0x0000B4B8
		protected virtual bool IsPerObjectException(Exception exception)
		{
			return false;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000D2BB File Offset: 0x0000B4BB
		protected virtual bool IsTransientException(Exception exception)
		{
			return false;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000D2BE File Offset: 0x0000B4BE
		protected virtual bool IsPermanentException(Exception exception)
		{
			return false;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000D2C1 File Offset: 0x0000B4C1
		protected void OnPolicyConfigChanged(PolicyConfigChangeEventArgs e)
		{
			if (this.PolicyConfigChanged != null)
			{
				this.PolicyConfigChanged(this, e);
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		private void ResetChangeTrackingForCollection<T>(IEnumerable<T> objects) where T : PolicyConfigBase
		{
			if (objects != null)
			{
				foreach (T t in objects)
				{
					t.ResetChangeTracking();
				}
			}
		}

		// Token: 0x040002A6 RID: 678
		private readonly ExecutionLog logProvider;

		// Token: 0x040002A7 RID: 679
		private bool isDisposed;
	}
}
