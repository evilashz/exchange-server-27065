using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.CompliancePolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E84 RID: 3716
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExPolicyConfigProvider : PolicyConfigProvider, IConfigurationSession, IDirectorySession, IConfigDataProvider, IDisposeTrackable, IDisposable
	{
		// Token: 0x060080BF RID: 32959 RVA: 0x002335D5 File Offset: 0x002317D5
		private ExPolicyConfigProvider(ExecutionLog logProvider) : base(logProvider)
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060080C0 RID: 32960 RVA: 0x002335EA File Offset: 0x002317EA
		public ExPolicyConfigProvider(IConfigurationSession configurationSession, ExecutionLog logProvider = null) : this(logProvider)
		{
			ArgumentValidator.ThrowIfNull("configurationSession", configurationSession);
			this.configurationSession = configurationSession;
			this.InitializeUnifiedPoliciesContainerIfNeeded();
		}

		// Token: 0x060080C1 RID: 32961 RVA: 0x0023360C File Offset: 0x0023180C
		public ExPolicyConfigProvider(Guid externalOrganizationId, bool readOnly = true, string domainController = "", ExecutionLog logProvider = null) : this(logProvider)
		{
			ADSessionSettings sessionSettings = VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled ? ADSessionSettings.FromExternalDirectoryOrganizationId(externalOrganizationId) : ADSessionSettings.FromRootOrgScopeSet();
			this.configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(domainController, readOnly, ConsistencyMode.PartiallyConsistent, sessionSettings, 116, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\UnifiedPolicy\\ExPolicyConfigProvider.cs");
			this.InitializeUnifiedPoliciesContainerIfNeeded();
		}

		// Token: 0x060080C2 RID: 32962 RVA: 0x00233670 File Offset: 0x00231870
		public ExPolicyConfigProvider(OrganizationId organizationId, bool readOnly = true, string domainController = "", ExecutionLog logProvider = null) : this(logProvider)
		{
			ArgumentValidator.ThrowIfNull("organizationId", organizationId);
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId);
			this.configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(domainController, readOnly, ConsistencyMode.IgnoreInvalid, sessionSettings, 141, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\UnifiedPolicy\\ExPolicyConfigProvider.cs");
			this.InitializeUnifiedPoliciesContainerIfNeeded();
		}

		// Token: 0x060080C3 RID: 32963 RVA: 0x002336C0 File Offset: 0x002318C0
		public ADObjectId GetPolicyConfigContainer(Guid? underPolicyId)
		{
			base.CheckDispose();
			if (ExPolicyConfigProvider.IsFFOOnline)
			{
				return null;
			}
			ADObjectId adobjectId = this.configurationSession.GetOrgContainerId().GetDescendantId(PolicyStorage.PoliciesContainer);
			if (underPolicyId != null)
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, UnifiedPolicyStorageBaseSchema.MasterIdentity, underPolicyId.Value.ToString());
				IConfigurable[] array = ((IConfigDataProvider)this).Find<PolicyStorage>(filter, adobjectId, false, null);
				if (array != null && array.Any<IConfigurable>())
				{
					adobjectId = (array[0].Identity as ADObjectId);
				}
			}
			return adobjectId;
		}

		// Token: 0x060080C4 RID: 32964 RVA: 0x00233741 File Offset: 0x00231941
		public OrganizationId GetOrganizationId()
		{
			base.CheckDispose();
			return this.configurationSession.GetOrgContainer().OrganizationId;
		}

		// Token: 0x17002245 RID: 8773
		// (get) Token: 0x060080C5 RID: 32965 RVA: 0x00233759 File Offset: 0x00231959
		public bool ReadOnly
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.ReadOnly;
			}
		}

		// Token: 0x17002246 RID: 8774
		// (get) Token: 0x060080C6 RID: 32966 RVA: 0x0023376C File Offset: 0x0023196C
		public string LastUsedDc
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.LastUsedDc;
			}
		}

		// Token: 0x060080C7 RID: 32967 RVA: 0x00233780 File Offset: 0x00231980
		private List<T> FindByQueryFilter<T>(QueryFilter queryFilter, Guid? underPolicyId) where T : PolicyConfigBase
		{
			PolicyConfigConverterBase converterByType = PolicyConfigConverterTable.GetConverterByType(typeof(T), true);
			List<T> list = new List<T>();
			bool arg = !ExPolicyConfigProvider.IsFFOOnline && underPolicyId == null && typeof(PolicyRuleConfig).Equals(typeof(T));
			IConfigurable[] array = converterByType.GetFindStorageObjectsDelegate(this)(queryFilter, this.GetPolicyConfigContainer(underPolicyId), arg, null);
			foreach (IConfigurable configurable in array)
			{
				list.Add(converterByType.ConvertFromStorage(this, configurable as UnifiedPolicyStorageBase) as T);
			}
			return list;
		}

		// Token: 0x060080C8 RID: 32968 RVA: 0x00233824 File Offset: 0x00231A24
		private void ManageStorageObjects(Type storageObjectType, Action fromMailboxDelegate, Action fromOtherStorageDelegate)
		{
			if (ExPolicyConfigProvider.IsFFOOnline || !(storageObjectType == typeof(BindingStorage)))
			{
				fromOtherStorageDelegate();
				return;
			}
			if (fromMailboxDelegate == null)
			{
				throw new InvalidOperationException("fromMailboxDelegate is null. BindingStorage in mailbox cannot be managed.");
			}
			fromMailboxDelegate();
		}

		// Token: 0x060080C9 RID: 32969 RVA: 0x0023385A File Offset: 0x00231A5A
		private ExBindingStoreObjectProvider GetExBindingStoreObjectProvider()
		{
			if (this.exBindingStoreProvider == null && !ExPolicyConfigProvider.IsFFOOnline)
			{
				this.exBindingStoreProvider = new ExBindingStoreObjectProvider(this);
			}
			return this.exBindingStoreProvider;
		}

		// Token: 0x060080CA RID: 32970 RVA: 0x00233880 File Offset: 0x00231A80
		private void InitializeUnifiedPoliciesContainerIfNeeded()
		{
			if (!this.ReadOnly)
			{
				ADObjectId policyConfigContainer = this.GetPolicyConfigContainer(null);
				if (policyConfigContainer == null)
				{
					return;
				}
				if (this.configurationSession.Read<Container>(policyConfigContainer) == null)
				{
					Container container = new Container();
					container.SetId(policyConfigContainer);
					container.OrganizationId = this.GetOrganizationId();
					this.configurationSession.Save(container);
					base.LogOneEntry(ExecutionLog.EventType.Information, string.Format("Container '{0}' is created for unified policy.", policyConfigContainer), null);
				}
			}
			this.configurationSession.DomainController = this.configurationSession.LastUsedDc;
			base.LogOneEntry(ExecutionLog.EventType.Information, string.Format("Use domain controller: '{0}'.", this.configurationSession.DomainController), null);
		}

		// Token: 0x060080CB RID: 32971 RVA: 0x00233924 File Offset: 0x00231B24
		private static QueryFilter CreateNameQueryFilter(ADPropertyDefinition nameProperty, string nameString)
		{
			nameString = (nameString ?? string.Empty);
			if (!nameString.StartsWith("*") && !nameString.EndsWith("*"))
			{
				return new ComparisonFilter(ComparisonOperator.Equal, nameProperty, nameString);
			}
			MatchOptions matchOptions = MatchOptions.FullString;
			if (nameString.StartsWith("*") && nameString.EndsWith("*"))
			{
				if (nameString.Length <= 2)
				{
					return null;
				}
				nameString = nameString.Substring(1, nameString.Length - 2);
				matchOptions = MatchOptions.SubString;
			}
			else if (nameString.EndsWith("*"))
			{
				nameString = nameString.Substring(0, nameString.Length - 1);
				matchOptions = MatchOptions.Prefix;
			}
			else if (nameString.StartsWith("*"))
			{
				nameString = nameString.Substring(1);
				matchOptions = MatchOptions.Suffix;
			}
			return new TextFilter(nameProperty, nameString, matchOptions, MatchFlags.IgnoreCase);
		}

		// Token: 0x060080CC RID: 32972 RVA: 0x002339E0 File Offset: 0x00231BE0
		private static void MarkAsUnchanged(IEnumerable<IConfigurable> instances)
		{
			if (ExPolicyConfigProvider.IsFFOOnline)
			{
				foreach (IConfigurable configurable in instances)
				{
					ConfigurableObject configurableObject = configurable as ConfigurableObject;
					if (configurableObject != null)
					{
						configurableObject.ResetChangeTracking(true);
					}
				}
			}
		}

		// Token: 0x060080CD RID: 32973 RVA: 0x00233A3C File Offset: 0x00231C3C
		protected override bool IsPermanentException(Exception exception)
		{
			return exception is DataSourceOperationException || exception is DataValidationException || exception is StoragePermanentException || base.IsPermanentException(exception);
		}

		// Token: 0x060080CE RID: 32974 RVA: 0x00233A5F File Offset: 0x00231C5F
		protected override bool IsTransientException(Exception exception)
		{
			return exception is DataSourceTransientException || exception is StorageTransientException || base.IsTransientException(exception);
		}

		// Token: 0x060080CF RID: 32975 RVA: 0x00233A7A File Offset: 0x00231C7A
		protected override bool IsPerObjectException(Exception exception)
		{
			return exception is DataValidationException || base.IsPerObjectException(exception);
		}

		// Token: 0x060080D0 RID: 32976 RVA: 0x00233A8D File Offset: 0x00231C8D
		protected override void Dispose(bool disposing)
		{
			this.configurationSession = null;
			this.exBindingStoreProvider = null;
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060080D1 RID: 32977 RVA: 0x00233AC1 File Offset: 0x00231CC1
		protected override string InternalGetLocalOrganizationId()
		{
			return Convert.ToBase64String(this.configurationSession.GetOrgContainer().OrganizationId.GetBytes(Encoding.UTF8));
		}

		// Token: 0x060080D2 RID: 32978 RVA: 0x00233AE4 File Offset: 0x00231CE4
		protected override T InternalFindByIdentity<T>(Guid identity)
		{
			QueryFilter queryFilter;
			if (ExPolicyConfigProvider.IsFFOOnline)
			{
				queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, identity);
			}
			else
			{
				queryFilter = new ComparisonFilter(ComparisonOperator.Equal, UnifiedPolicyStorageBaseSchema.MasterIdentity, identity.ToString());
			}
			List<T> list = this.FindByQueryFilter<T>(queryFilter, null);
			if (list.Count <= 0)
			{
				return default(T);
			}
			return list[0];
		}

		// Token: 0x060080D3 RID: 32979 RVA: 0x00233B54 File Offset: 0x00231D54
		protected override IEnumerable<T> InternalFindByName<T>(string name)
		{
			QueryFilter queryFilter = ExPolicyConfigProvider.CreateNameQueryFilter(ADObjectSchema.Name, name);
			return this.FindByQueryFilter<T>(queryFilter, null);
		}

		// Token: 0x060080D4 RID: 32980 RVA: 0x00233B8C File Offset: 0x00231D8C
		protected override IEnumerable<T> InternalFindByPolicyDefinitionConfigId<T>(Guid policyDefinitionConfigId)
		{
			ArgumentValidator.ThrowIfInvalidValue<Guid>("policyDefinitionConfigId", policyDefinitionConfigId, (Guid id) => Guid.Empty != id);
			if (!ExPolicyConfigProvider.IsFFOOnline && typeof(T).Equals(typeof(PolicyRuleConfig)))
			{
				return this.FindByQueryFilter<T>(null, new Guid?(policyDefinitionConfigId));
			}
			PolicyConfigConverterBase converterByType = PolicyConfigConverterTable.GetConverterByType(typeof(T), true);
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, converterByType.PolicyIdProperty, policyDefinitionConfigId);
			return this.FindByQueryFilter<T>(queryFilter, null);
		}

		// Token: 0x060080D5 RID: 32981 RVA: 0x00233C14 File Offset: 0x00231E14
		protected override IEnumerable<PolicyBindingConfig> InternalFindPolicyBindingConfigsByScopes(IEnumerable<string> scopes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060080D6 RID: 32982 RVA: 0x00233C1B File Offset: 0x00231E1B
		protected override PolicyAssociationConfig InternalFindPolicyAssociationConfigByScope(string scope)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060080D7 RID: 32983 RVA: 0x00233CB0 File Offset: 0x00231EB0
		protected override void InternalSave(PolicyConfigBase instance)
		{
			ArgumentValidator.ThrowIfNull("instance", instance);
			if (instance.ObjectState == ChangeType.Update || instance.ObjectState == ChangeType.Add)
			{
				PolicyConfigConverterBase converterByType = PolicyConfigConverterTable.GetConverterByType(instance.GetType(), true);
				IConfigurable storageObject = converterByType.ConvertToStorage(this, instance);
				this.ManageStorageObjects(storageObject.GetType(), delegate
				{
					ExBindingStoreObjectProvider exBindingStoreObjectProvider = this.GetExBindingStoreObjectProvider();
					if (exBindingStoreObjectProvider == null)
					{
						throw new InvalidOperationException("ExBindingStoreObjectProvider shouldn't be null.");
					}
					exBindingStoreObjectProvider.SaveBindingStorage(storageObject as BindingStorage);
				}, delegate
				{
					if (storageObject is ADConfigurationObject)
					{
						this.configurationSession.Save(storageObject as ADConfigurationObject);
						return;
					}
					this.configurationSession.Save(storageObject);
				});
			}
		}

		// Token: 0x060080D8 RID: 32984 RVA: 0x00233D68 File Offset: 0x00231F68
		protected override void InternalDelete(PolicyConfigBase instance)
		{
			ArgumentValidator.ThrowIfNull("instance", instance);
			PolicyConfigConverterBase converterByType = PolicyConfigConverterTable.GetConverterByType(instance.GetType(), true);
			IConfigurable storageObject = converterByType.ConvertToStorage(this, instance);
			this.ManageStorageObjects(storageObject.GetType(), delegate
			{
				this.GetExBindingStoreObjectProvider().DeleteBindingStorage(storageObject as BindingStorage);
			}, delegate
			{
				this.configurationSession.Delete(storageObject);
			});
		}

		// Token: 0x060080D9 RID: 32985 RVA: 0x00233DD4 File Offset: 0x00231FD4
		protected override void InternalPublishStatus(IEnumerable<UnifiedPolicyStatus> statusUpdateNotifications)
		{
			string text = this.GetOrganizationId().ToExternalDirectoryOrganizationId();
			Guid tenantId;
			if (!Guid.TryParse(text, out tenantId))
			{
				throw new PolicyConfigProviderPermanentException(string.Format("Cannot publish status because ExternalDirectoryOrganizationId is not valid guid: {0}, OrganizationId: {1}", text, this.GetOrganizationId().ToString()));
			}
			using (ITenantInfoProvider tenantInfoProvider = ExPolicyConfigProvider.tenantInfoProviderFactory.CreateTenantInfoProvider(new TenantContext(tenantId, null)))
			{
				TenantInfo tenantInfo = tenantInfoProvider.Load();
				if (tenantInfo == null || !(tenantInfo.TenantId != Guid.Empty) || string.IsNullOrEmpty(tenantInfo.SyncSvcUrl))
				{
					throw new PolicyConfigProviderPermanentException(string.Format("Cannot publish status because tenant info is wrong. TenantId: '{0}'; SyncSvcUrl: '{1}'.", (tenantInfo == null) ? string.Empty : tenantInfo.TenantId.ToString(), (tenantInfo == null) ? string.Empty : tenantInfo.SyncSvcUrl));
				}
				foreach (UnifiedPolicyStatus unifiedPolicyStatus in statusUpdateNotifications)
				{
					unifiedPolicyStatus.TenantId = tenantInfo.TenantId;
					unifiedPolicyStatus.Workload = Workload.Exchange;
				}
				string text2 = "PublishStatus_" + Guid.NewGuid().ToString();
				SyncNotificationResult syncNotificationResult = RpcClientWrapper.NotifyStatusChanges(text2, null, tenantInfo.TenantId, tenantInfo.SyncSvcUrl, false, statusUpdateNotifications.ToList<UnifiedPolicyStatus>());
				if (!syncNotificationResult.Success)
				{
					throw new PolicyConfigProviderTransientException(string.Format("Cannot publish status because of '{0}'.", syncNotificationResult.Error));
				}
				base.LogOneEntry(ExecutionLog.EventType.Information, string.Format("Status publish with notification '{0}' succeeded.", text2), null);
			}
		}

		// Token: 0x060080DA RID: 32986 RVA: 0x00233FB0 File Offset: 0x002321B0
		IConfigurable IConfigDataProvider.Read<T>(ObjectId identity)
		{
			base.CheckDispose();
			IConfigurable instance = null;
			this.ManageStorageObjects(typeof(T), null, delegate
			{
				instance = this.configurationSession.Read<T>(identity);
			});
			return instance;
		}

		// Token: 0x060080DB RID: 32987 RVA: 0x002340E4 File Offset: 0x002322E4
		IConfigurable[] IConfigDataProvider.Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			base.CheckDispose();
			IConfigurable[] instances = Array<IConfigurable>.Empty;
			this.ManageStorageObjects(typeof(T), delegate
			{
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				if (comparisonFilter == null || comparisonFilter.ComparisonOperator != ComparisonOperator.Equal || (comparisonFilter.Property != BindingStorageSchema.PolicyId && comparisonFilter.Property != UnifiedPolicyStorageBaseSchema.MasterIdentity))
				{
					throw new NotImplementedException("Exchange only supports query BindingStorage by policy definition id and MasterIdentity.");
				}
				BindingStorage bindingStorage;
				if (comparisonFilter.Property == BindingStorageSchema.PolicyId)
				{
					bindingStorage = this.GetExBindingStoreObjectProvider().FindBindingStorageByPolicyId((Guid)comparisonFilter.PropertyValue);
				}
				else
				{
					bindingStorage = this.GetExBindingStoreObjectProvider().FindBindingStorageById(comparisonFilter.PropertyValue.ToString());
				}
				if (bindingStorage != null)
				{
					instances = new IConfigurable[]
					{
						bindingStorage
					};
				}
			}, delegate
			{
				instances = this.configurationSession.Find<T>(filter, rootId, deepSearch, sortBy);
			});
			ExPolicyConfigProvider.MarkAsUnchanged(instances);
			return instances;
		}

		// Token: 0x060080DC RID: 32988 RVA: 0x002341A4 File Offset: 0x002323A4
		IEnumerable<T> IConfigDataProvider.FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			base.CheckDispose();
			IEnumerable<T> instances = null;
			this.ManageStorageObjects(typeof(T), null, delegate
			{
				instances = this.configurationSession.FindPaged<T>(filter, rootId, deepSearch, sortBy, pageSize);
			});
			return instances;
		}

		// Token: 0x060080DD RID: 32989 RVA: 0x00234284 File Offset: 0x00232484
		void IConfigDataProvider.Save(IConfigurable instance)
		{
			base.CheckDispose();
			if (instance.ObjectState != ObjectState.Unchanged)
			{
				UnifiedPolicyStorageBase unifiedPolicyStorageBase = instance as UnifiedPolicyStorageBase;
				if (unifiedPolicyStorageBase != null)
				{
					unifiedPolicyStorageBase.PolicyVersion = CombGuidGenerator.NewGuid();
					if (ExPolicyConfigProvider.IsFFOOnline && unifiedPolicyStorageBase.ObjectState == ObjectState.New && unifiedPolicyStorageBase.Guid == Guid.Empty && unifiedPolicyStorageBase.MasterIdentity != Guid.Empty)
					{
						unifiedPolicyStorageBase.SetId(new ADObjectId(unifiedPolicyStorageBase.Id.DistinguishedName, unifiedPolicyStorageBase.MasterIdentity));
					}
				}
				this.ManageStorageObjects(instance.GetType(), delegate
				{
					this.GetExBindingStoreObjectProvider().SaveBindingStorage(instance as BindingStorage);
				}, delegate
				{
					if (instance is ADConfigurationObject)
					{
						this.configurationSession.Save(instance as ADConfigurationObject);
						return;
					}
					this.configurationSession.Save(instance);
				});
				PolicyConfigConverterBase converterByType = PolicyConfigConverterTable.GetConverterByType(instance.GetType(), false);
				if (converterByType != null)
				{
					PolicyConfigBase policyConfig = converterByType.ConvertFromStorage(this, instance as UnifiedPolicyStorageBase);
					base.OnPolicyConfigChanged(new PolicyConfigChangeEventArgs(this, policyConfig, (instance.ObjectState == ObjectState.New) ? ChangeType.Add : ChangeType.Update));
				}
			}
		}

		// Token: 0x060080DE RID: 32990 RVA: 0x00234430 File Offset: 0x00232630
		void IConfigDataProvider.Delete(IConfigurable instance)
		{
			base.CheckDispose();
			UnifiedPolicyStorageBase policyStorage = instance as UnifiedPolicyStorageBase;
			this.ManageStorageObjects(instance.GetType(), delegate
			{
				this.GetExBindingStoreObjectProvider().DeleteBindingStorage(instance as BindingStorage);
			}, delegate
			{
				if (ExPolicyConfigProvider.IsFFOOnline && policyStorage != null)
				{
					policyStorage.PolicyVersion = CombGuidGenerator.NewGuid();
					this.configurationSession.Save(policyStorage);
				}
				this.configurationSession.Delete(instance);
			});
			PolicyConfigConverterBase converterByType = PolicyConfigConverterTable.GetConverterByType(instance.GetType(), false);
			if (converterByType != null)
			{
				if (policyStorage.ObjectState != ObjectState.Deleted)
				{
					policyStorage.MarkAsDeleted();
				}
				PolicyConfigBase policyConfig = converterByType.ConvertFromStorage(this, policyStorage);
				base.OnPolicyConfigChanged(new PolicyConfigChangeEventArgs(this, policyConfig, ChangeType.Delete));
			}
		}

		// Token: 0x17002247 RID: 8775
		// (get) Token: 0x060080DF RID: 32991 RVA: 0x002344DB File Offset: 0x002326DB
		string IConfigDataProvider.Source
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.Source;
			}
		}

		// Token: 0x060080E0 RID: 32992 RVA: 0x002344EE File Offset: 0x002326EE
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ExPolicyConfigProvider>(this);
		}

		// Token: 0x060080E1 RID: 32993 RVA: 0x002344F6 File Offset: 0x002326F6
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x17002248 RID: 8776
		// (get) Token: 0x060080E2 RID: 32994 RVA: 0x0023450B File Offset: 0x0023270B
		ADObjectId IConfigurationSession.ConfigurationNamingContext
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.ConfigurationNamingContext;
			}
		}

		// Token: 0x17002249 RID: 8777
		// (get) Token: 0x060080E3 RID: 32995 RVA: 0x0023451E File Offset: 0x0023271E
		ADObjectId IConfigurationSession.DeletedObjectsContainer
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.DeletedObjectsContainer;
			}
		}

		// Token: 0x1700224A RID: 8778
		// (get) Token: 0x060080E4 RID: 32996 RVA: 0x00234531 File Offset: 0x00232731
		ADObjectId IConfigurationSession.SchemaNamingContext
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.SchemaNamingContext;
			}
		}

		// Token: 0x060080E5 RID: 32997 RVA: 0x00234544 File Offset: 0x00232744
		bool IConfigurationSession.CheckForRetentionPolicyWithConflictingRetentionId(Guid retentionId, out string duplicateName)
		{
			base.CheckDispose();
			return this.configurationSession.CheckForRetentionPolicyWithConflictingRetentionId(retentionId, out duplicateName);
		}

		// Token: 0x060080E6 RID: 32998 RVA: 0x00234559 File Offset: 0x00232759
		bool IConfigurationSession.CheckForRetentionPolicyWithConflictingRetentionId(Guid retentionId, string identity, out string duplicateName)
		{
			base.CheckDispose();
			return this.configurationSession.CheckForRetentionPolicyWithConflictingRetentionId(retentionId, identity, out duplicateName);
		}

		// Token: 0x060080E7 RID: 32999 RVA: 0x0023456F File Offset: 0x0023276F
		bool IConfigurationSession.CheckForRetentionTagWithConflictingRetentionId(Guid retentionId, out string duplicateName)
		{
			base.CheckDispose();
			return this.configurationSession.CheckForRetentionTagWithConflictingRetentionId(retentionId, out duplicateName);
		}

		// Token: 0x060080E8 RID: 33000 RVA: 0x00234584 File Offset: 0x00232784
		bool IConfigurationSession.CheckForRetentionTagWithConflictingRetentionId(Guid retentionId, string identity, out string duplicateName)
		{
			base.CheckDispose();
			return this.configurationSession.CheckForRetentionTagWithConflictingRetentionId(retentionId, identity, out duplicateName);
		}

		// Token: 0x060080E9 RID: 33001 RVA: 0x0023459A File Offset: 0x0023279A
		void IConfigurationSession.DeleteTree(ADConfigurationObject instanceToDelete, TreeDeleteNotFinishedHandler handler)
		{
			base.CheckDispose();
			this.configurationSession.DeleteTree(instanceToDelete, handler);
		}

		// Token: 0x060080EA RID: 33002 RVA: 0x002345AF File Offset: 0x002327AF
		AcceptedDomain[] IConfigurationSession.FindAcceptedDomainsByFederatedOrgId(FederatedOrganizationId federatedOrganizationId)
		{
			base.CheckDispose();
			return this.configurationSession.FindAcceptedDomainsByFederatedOrgId(federatedOrganizationId);
		}

		// Token: 0x060080EB RID: 33003 RVA: 0x002345C3 File Offset: 0x002327C3
		ADPagedReader<TResult> IConfigurationSession.FindAllPaged<TResult>()
		{
			base.CheckDispose();
			return this.configurationSession.FindAllPaged<TResult>();
		}

		// Token: 0x060080EC RID: 33004 RVA: 0x002345D6 File Offset: 0x002327D6
		ExchangeRoleAssignment[] IConfigurationSession.FindAssignmentsForManagementScope(ManagementScope managementScope, bool returnAll)
		{
			base.CheckDispose();
			return this.configurationSession.FindAssignmentsForManagementScope(managementScope, returnAll);
		}

		// Token: 0x060080ED RID: 33005 RVA: 0x002345EB File Offset: 0x002327EB
		T IConfigurationSession.FindMailboxPolicyByName<T>(string name)
		{
			base.CheckDispose();
			return this.configurationSession.FindMailboxPolicyByName<T>(name);
		}

		// Token: 0x060080EE RID: 33006 RVA: 0x002345FF File Offset: 0x002327FF
		MicrosoftExchangeRecipient IConfigurationSession.FindMicrosoftExchangeRecipient()
		{
			base.CheckDispose();
			return this.configurationSession.FindMicrosoftExchangeRecipient();
		}

		// Token: 0x060080EF RID: 33007 RVA: 0x00234612 File Offset: 0x00232812
		OfflineAddressBook[] IConfigurationSession.FindOABsForWebDistributionPoint(ADOabVirtualDirectory vDir)
		{
			base.CheckDispose();
			return this.configurationSession.FindOABsForWebDistributionPoint(vDir);
		}

		// Token: 0x060080F0 RID: 33008 RVA: 0x00234626 File Offset: 0x00232826
		ThrottlingPolicy[] IConfigurationSession.FindOrganizationThrottlingPolicies(OrganizationId organizationId)
		{
			base.CheckDispose();
			return this.configurationSession.FindOrganizationThrottlingPolicies(organizationId);
		}

		// Token: 0x060080F1 RID: 33009 RVA: 0x0023463A File Offset: 0x0023283A
		ADPagedReader<TResult> IConfigurationSession.FindPaged<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			base.CheckDispose();
			return this.configurationSession.FindPaged<TResult>(rootId, scope, filter, sortBy, pageSize);
		}

		// Token: 0x060080F2 RID: 33010 RVA: 0x00234654 File Offset: 0x00232854
		Result<ExchangeRoleAssignment>[] IConfigurationSession.FindRoleAssignmentsByUserIds(ADObjectId[] securityPrincipalIds, bool partnerMode)
		{
			base.CheckDispose();
			return this.configurationSession.FindRoleAssignmentsByUserIds(securityPrincipalIds, partnerMode);
		}

		// Token: 0x060080F3 RID: 33011 RVA: 0x00234669 File Offset: 0x00232869
		Result<ExchangeRoleAssignment>[] IConfigurationSession.FindRoleAssignmentsByUserIds(ADObjectId[] securityPrincipalIds, QueryFilter additionalFilter)
		{
			base.CheckDispose();
			return this.configurationSession.FindRoleAssignmentsByUserIds(securityPrincipalIds, additionalFilter);
		}

		// Token: 0x060080F4 RID: 33012 RVA: 0x0023467E File Offset: 0x0023287E
		ManagementScope[] IConfigurationSession.FindSimilarManagementScope(ManagementScope managementScope)
		{
			base.CheckDispose();
			return this.configurationSession.FindSimilarManagementScope(managementScope);
		}

		// Token: 0x060080F5 RID: 33013 RVA: 0x00234692 File Offset: 0x00232892
		T IConfigurationSession.FindSingletonConfigurationObject<T>()
		{
			base.CheckDispose();
			return this.configurationSession.FindSingletonConfigurationObject<T>();
		}

		// Token: 0x060080F6 RID: 33014 RVA: 0x002346A5 File Offset: 0x002328A5
		AcceptedDomain IConfigurationSession.GetAcceptedDomainByDomainName(string domainName)
		{
			base.CheckDispose();
			return this.configurationSession.GetAcceptedDomainByDomainName(domainName);
		}

		// Token: 0x060080F7 RID: 33015 RVA: 0x002346B9 File Offset: 0x002328B9
		ADPagedReader<ManagementScope> IConfigurationSession.GetAllExclusiveScopes()
		{
			base.CheckDispose();
			return this.configurationSession.GetAllExclusiveScopes();
		}

		// Token: 0x060080F8 RID: 33016 RVA: 0x002346CC File Offset: 0x002328CC
		ADPagedReader<ManagementScope> IConfigurationSession.GetAllScopes(OrganizationId organizationId, ScopeRestrictionType restrictionType)
		{
			base.CheckDispose();
			return this.configurationSession.GetAllScopes(organizationId, restrictionType);
		}

		// Token: 0x060080F9 RID: 33017 RVA: 0x002346E1 File Offset: 0x002328E1
		AvailabilityAddressSpace IConfigurationSession.GetAvailabilityAddressSpace(string domainName)
		{
			base.CheckDispose();
			return this.configurationSession.GetAvailabilityAddressSpace(domainName);
		}

		// Token: 0x060080FA RID: 33018 RVA: 0x002346F5 File Offset: 0x002328F5
		AvailabilityConfig IConfigurationSession.GetAvailabilityConfig()
		{
			base.CheckDispose();
			return this.configurationSession.GetAvailabilityConfig();
		}

		// Token: 0x060080FB RID: 33019 RVA: 0x00234708 File Offset: 0x00232908
		AcceptedDomain IConfigurationSession.GetDefaultAcceptedDomain()
		{
			base.CheckDispose();
			return this.configurationSession.GetDefaultAcceptedDomain();
		}

		// Token: 0x060080FC RID: 33020 RVA: 0x0023471B File Offset: 0x0023291B
		ExchangeConfigurationContainer IConfigurationSession.GetExchangeConfigurationContainer()
		{
			base.CheckDispose();
			return this.configurationSession.GetExchangeConfigurationContainer();
		}

		// Token: 0x060080FD RID: 33021 RVA: 0x0023472E File Offset: 0x0023292E
		ExchangeConfigurationContainerWithAddressLists IConfigurationSession.GetExchangeConfigurationContainerWithAddressLists()
		{
			base.CheckDispose();
			return this.configurationSession.GetExchangeConfigurationContainerWithAddressLists();
		}

		// Token: 0x060080FE RID: 33022 RVA: 0x00234741 File Offset: 0x00232941
		FederatedOrganizationId IConfigurationSession.GetFederatedOrganizationId()
		{
			base.CheckDispose();
			return this.configurationSession.GetFederatedOrganizationId();
		}

		// Token: 0x060080FF RID: 33023 RVA: 0x00234754 File Offset: 0x00232954
		FederatedOrganizationId IConfigurationSession.GetFederatedOrganizationId(OrganizationId organizationId)
		{
			base.CheckDispose();
			return this.configurationSession.GetFederatedOrganizationId(organizationId);
		}

		// Token: 0x06008100 RID: 33024 RVA: 0x00234768 File Offset: 0x00232968
		FederatedOrganizationId IConfigurationSession.GetFederatedOrganizationIdByDomainName(string domainName)
		{
			base.CheckDispose();
			return this.configurationSession.GetFederatedOrganizationIdByDomainName(domainName);
		}

		// Token: 0x06008101 RID: 33025 RVA: 0x0023477C File Offset: 0x0023297C
		NspiRpcClientConnection IConfigurationSession.GetNspiRpcClientConnection()
		{
			base.CheckDispose();
			return this.configurationSession.GetNspiRpcClientConnection();
		}

		// Token: 0x06008102 RID: 33026 RVA: 0x0023478F File Offset: 0x0023298F
		ThrottlingPolicy IConfigurationSession.GetOrganizationThrottlingPolicy(OrganizationId organizationId)
		{
			base.CheckDispose();
			return this.configurationSession.GetOrganizationThrottlingPolicy(organizationId);
		}

		// Token: 0x06008103 RID: 33027 RVA: 0x002347A3 File Offset: 0x002329A3
		ThrottlingPolicy IConfigurationSession.GetOrganizationThrottlingPolicy(OrganizationId organizationId, bool logFailedLookup)
		{
			base.CheckDispose();
			return this.configurationSession.GetOrganizationThrottlingPolicy(organizationId, logFailedLookup);
		}

		// Token: 0x06008104 RID: 33028 RVA: 0x002347B8 File Offset: 0x002329B8
		Organization IConfigurationSession.GetOrgContainer()
		{
			base.CheckDispose();
			return this.configurationSession.GetOrgContainer();
		}

		// Token: 0x06008105 RID: 33029 RVA: 0x002347CB File Offset: 0x002329CB
		OrganizationRelationship IConfigurationSession.GetOrganizationRelationship(string domainName)
		{
			base.CheckDispose();
			return this.configurationSession.GetOrganizationRelationship(domainName);
		}

		// Token: 0x06008106 RID: 33030 RVA: 0x002347DF File Offset: 0x002329DF
		ADObjectId IConfigurationSession.GetOrgContainerId()
		{
			base.CheckDispose();
			return this.configurationSession.GetOrgContainerId();
		}

		// Token: 0x06008107 RID: 33031 RVA: 0x002347F2 File Offset: 0x002329F2
		RbacContainer IConfigurationSession.GetRbacContainer()
		{
			base.CheckDispose();
			return this.configurationSession.GetRbacContainer();
		}

		// Token: 0x06008108 RID: 33032 RVA: 0x00234805 File Offset: 0x00232A05
		bool IConfigurationSession.ManagementScopeIsInUse(ManagementScope managementScope)
		{
			base.CheckDispose();
			return this.configurationSession.ManagementScopeIsInUse(managementScope);
		}

		// Token: 0x06008109 RID: 33033 RVA: 0x00234819 File Offset: 0x00232A19
		TResult IConfigurationSession.FindByExchangeObjectId<TResult>(Guid exchangeObjectId)
		{
			base.CheckDispose();
			return this.configurationSession.FindByExchangeObjectId<TResult>(exchangeObjectId);
		}

		// Token: 0x0600810A RID: 33034 RVA: 0x0023482D File Offset: 0x00232A2D
		TResult IConfigurationSession.Read<TResult>(ADObjectId entryId)
		{
			base.CheckDispose();
			return this.configurationSession.Read<TResult>(entryId);
		}

		// Token: 0x0600810B RID: 33035 RVA: 0x00234841 File Offset: 0x00232A41
		Result<TResult>[] IConfigurationSession.ReadMultiple<TResult>(ADObjectId[] identities)
		{
			base.CheckDispose();
			return this.configurationSession.ReadMultiple<TResult>(identities);
		}

		// Token: 0x0600810C RID: 33036 RVA: 0x00234855 File Offset: 0x00232A55
		MultiValuedProperty<ReplicationCursor> IConfigurationSession.ReadReplicationCursors(ADObjectId id)
		{
			base.CheckDispose();
			return this.configurationSession.ReadReplicationCursors(id);
		}

		// Token: 0x0600810D RID: 33037 RVA: 0x00234869 File Offset: 0x00232A69
		void IConfigurationSession.ReadReplicationData(ADObjectId id, out MultiValuedProperty<ReplicationCursor> replicationCursors, out MultiValuedProperty<ReplicationNeighbor> repsFrom)
		{
			base.CheckDispose();
			this.configurationSession.ReadReplicationData(id, out replicationCursors, out repsFrom);
		}

		// Token: 0x0600810E RID: 33038 RVA: 0x0023487F File Offset: 0x00232A7F
		void IConfigurationSession.Save(ADConfigurationObject instanceToSave)
		{
			base.CheckDispose();
			this.configurationSession.Save(instanceToSave);
		}

		// Token: 0x1700224B RID: 8779
		// (get) Token: 0x0600810F RID: 33039 RVA: 0x00234893 File Offset: 0x00232A93
		// (set) Token: 0x06008110 RID: 33040 RVA: 0x002348A6 File Offset: 0x00232AA6
		TimeSpan? IDirectorySession.ClientSideSearchTimeout
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.ClientSideSearchTimeout;
			}
			set
			{
				base.CheckDispose();
				this.configurationSession.ClientSideSearchTimeout = value;
			}
		}

		// Token: 0x1700224C RID: 8780
		// (get) Token: 0x06008111 RID: 33041 RVA: 0x002348BA File Offset: 0x00232ABA
		ConfigScopes IDirectorySession.ConfigScope
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.ConfigScope;
			}
		}

		// Token: 0x1700224D RID: 8781
		// (get) Token: 0x06008112 RID: 33042 RVA: 0x002348CD File Offset: 0x00232ACD
		ConsistencyMode IDirectorySession.ConsistencyMode
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.ConsistencyMode;
			}
		}

		// Token: 0x1700224E RID: 8782
		// (get) Token: 0x06008113 RID: 33043 RVA: 0x002348E0 File Offset: 0x00232AE0
		// (set) Token: 0x06008114 RID: 33044 RVA: 0x002348F3 File Offset: 0x00232AF3
		string IDirectorySession.DomainController
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.DomainController;
			}
			set
			{
				base.CheckDispose();
				this.configurationSession.DomainController = value;
			}
		}

		// Token: 0x1700224F RID: 8783
		// (get) Token: 0x06008115 RID: 33045 RVA: 0x00234907 File Offset: 0x00232B07
		// (set) Token: 0x06008116 RID: 33046 RVA: 0x0023491A File Offset: 0x00232B1A
		bool IDirectorySession.EnforceContainerizedScoping
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.EnforceContainerizedScoping;
			}
			set
			{
				base.CheckDispose();
				this.configurationSession.EnforceContainerizedScoping = value;
			}
		}

		// Token: 0x17002250 RID: 8784
		// (get) Token: 0x06008117 RID: 33047 RVA: 0x0023492E File Offset: 0x00232B2E
		// (set) Token: 0x06008118 RID: 33048 RVA: 0x00234941 File Offset: 0x00232B41
		bool IDirectorySession.EnforceDefaultScope
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.EnforceDefaultScope;
			}
			set
			{
				base.CheckDispose();
				this.configurationSession.EnforceDefaultScope = value;
			}
		}

		// Token: 0x17002251 RID: 8785
		// (get) Token: 0x06008119 RID: 33049 RVA: 0x00234955 File Offset: 0x00232B55
		string IDirectorySession.LastUsedDc
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.LastUsedDc;
			}
		}

		// Token: 0x17002252 RID: 8786
		// (get) Token: 0x0600811A RID: 33050 RVA: 0x00234968 File Offset: 0x00232B68
		int IDirectorySession.Lcid
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.Lcid;
			}
		}

		// Token: 0x17002253 RID: 8787
		// (get) Token: 0x0600811B RID: 33051 RVA: 0x0023497B File Offset: 0x00232B7B
		// (set) Token: 0x0600811C RID: 33052 RVA: 0x0023498E File Offset: 0x00232B8E
		string IDirectorySession.LinkResolutionServer
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.LinkResolutionServer;
			}
			set
			{
				base.CheckDispose();
				this.configurationSession.LinkResolutionServer = value;
			}
		}

		// Token: 0x17002254 RID: 8788
		// (get) Token: 0x0600811D RID: 33053 RVA: 0x002349A2 File Offset: 0x00232BA2
		// (set) Token: 0x0600811E RID: 33054 RVA: 0x002349B5 File Offset: 0x00232BB5
		bool IDirectorySession.LogSizeLimitExceededEvent
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.LogSizeLimitExceededEvent;
			}
			set
			{
				base.CheckDispose();
				this.configurationSession.LogSizeLimitExceededEvent = value;
			}
		}

		// Token: 0x17002255 RID: 8789
		// (get) Token: 0x0600811F RID: 33055 RVA: 0x002349C9 File Offset: 0x00232BC9
		NetworkCredential IDirectorySession.NetworkCredential
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.NetworkCredential;
			}
		}

		// Token: 0x17002256 RID: 8790
		// (get) Token: 0x06008120 RID: 33056 RVA: 0x002349DC File Offset: 0x00232BDC
		bool IDirectorySession.ReadOnly
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.ReadOnly;
			}
		}

		// Token: 0x17002257 RID: 8791
		// (get) Token: 0x06008121 RID: 33057 RVA: 0x002349EF File Offset: 0x00232BEF
		ADServerSettings IDirectorySession.ServerSettings
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.ServerSettings;
			}
		}

		// Token: 0x17002258 RID: 8792
		// (get) Token: 0x06008122 RID: 33058 RVA: 0x00234A02 File Offset: 0x00232C02
		// (set) Token: 0x06008123 RID: 33059 RVA: 0x00234A15 File Offset: 0x00232C15
		TimeSpan? IDirectorySession.ServerTimeout
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.ServerTimeout;
			}
			set
			{
				base.CheckDispose();
				this.configurationSession.ServerTimeout = value;
			}
		}

		// Token: 0x17002259 RID: 8793
		// (get) Token: 0x06008124 RID: 33060 RVA: 0x00234A29 File Offset: 0x00232C29
		ADSessionSettings IDirectorySession.SessionSettings
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.SessionSettings;
			}
		}

		// Token: 0x1700225A RID: 8794
		// (get) Token: 0x06008125 RID: 33061 RVA: 0x00234A3C File Offset: 0x00232C3C
		// (set) Token: 0x06008126 RID: 33062 RVA: 0x00234A4F File Offset: 0x00232C4F
		bool IDirectorySession.SkipRangedAttributes
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.SkipRangedAttributes;
			}
			set
			{
				base.CheckDispose();
				this.configurationSession.SkipRangedAttributes = value;
			}
		}

		// Token: 0x1700225B RID: 8795
		// (get) Token: 0x06008127 RID: 33063 RVA: 0x00234A63 File Offset: 0x00232C63
		// (set) Token: 0x06008128 RID: 33064 RVA: 0x00234A76 File Offset: 0x00232C76
		public string[] ExclusiveLdapAttributes
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.ExclusiveLdapAttributes;
			}
			set
			{
				base.CheckDispose();
				this.configurationSession.ExclusiveLdapAttributes = value;
			}
		}

		// Token: 0x1700225C RID: 8796
		// (get) Token: 0x06008129 RID: 33065 RVA: 0x00234A8A File Offset: 0x00232C8A
		// (set) Token: 0x0600812A RID: 33066 RVA: 0x00234A9D File Offset: 0x00232C9D
		bool IDirectorySession.UseConfigNC
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.UseConfigNC;
			}
			set
			{
				base.CheckDispose();
				this.configurationSession.UseConfigNC = value;
			}
		}

		// Token: 0x1700225D RID: 8797
		// (get) Token: 0x0600812B RID: 33067 RVA: 0x00234AB1 File Offset: 0x00232CB1
		// (set) Token: 0x0600812C RID: 33068 RVA: 0x00234AC4 File Offset: 0x00232CC4
		bool IDirectorySession.UseGlobalCatalog
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.UseGlobalCatalog;
			}
			set
			{
				base.CheckDispose();
				this.configurationSession.UseGlobalCatalog = value;
			}
		}

		// Token: 0x1700225E RID: 8798
		// (get) Token: 0x0600812D RID: 33069 RVA: 0x00234AD8 File Offset: 0x00232CD8
		// (set) Token: 0x0600812E RID: 33070 RVA: 0x00234AEB File Offset: 0x00232CEB
		IActivityScope IDirectorySession.ActivityScope
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.ActivityScope;
			}
			set
			{
				base.CheckDispose();
				this.configurationSession.ActivityScope = value;
			}
		}

		// Token: 0x1700225F RID: 8799
		// (get) Token: 0x0600812F RID: 33071 RVA: 0x00234AFF File Offset: 0x00232CFF
		string IDirectorySession.CallerInfo
		{
			get
			{
				base.CheckDispose();
				return this.configurationSession.CallerInfo;
			}
		}

		// Token: 0x06008130 RID: 33072 RVA: 0x00234B12 File Offset: 0x00232D12
		void IDirectorySession.AnalyzeDirectoryError(PooledLdapConnection connection, DirectoryRequest request, DirectoryException de, int totalRetries, int retriesOnServer)
		{
			base.CheckDispose();
			this.configurationSession.AnalyzeDirectoryError(connection, request, de, totalRetries, retriesOnServer);
		}

		// Token: 0x06008131 RID: 33073 RVA: 0x00234B2C File Offset: 0x00232D2C
		QueryFilter IDirectorySession.ApplyDefaultFilters(QueryFilter filter, ADObjectId rootId, ADObject dummyObject, bool applyImplicitFilter)
		{
			base.CheckDispose();
			return this.configurationSession.ApplyDefaultFilters(filter, rootId, dummyObject, applyImplicitFilter);
		}

		// Token: 0x06008132 RID: 33074 RVA: 0x00234B44 File Offset: 0x00232D44
		QueryFilter IDirectorySession.ApplyDefaultFilters(QueryFilter filter, ADScope scope, ADObject dummyObject, bool applyImplicitFilter)
		{
			base.CheckDispose();
			return this.configurationSession.ApplyDefaultFilters(filter, scope, dummyObject, applyImplicitFilter);
		}

		// Token: 0x06008133 RID: 33075 RVA: 0x00234B5C File Offset: 0x00232D5C
		void IDirectorySession.CheckFilterForUnsafeIdentity(QueryFilter filter)
		{
			base.CheckDispose();
			this.configurationSession.CheckFilterForUnsafeIdentity(filter);
		}

		// Token: 0x06008134 RID: 33076 RVA: 0x00234B70 File Offset: 0x00232D70
		void IDirectorySession.UnsafeExecuteModificationRequest(DirectoryRequest request, ADObjectId rootId)
		{
			base.CheckDispose();
			this.configurationSession.UnsafeExecuteModificationRequest(request, rootId);
		}

		// Token: 0x06008135 RID: 33077 RVA: 0x00234B85 File Offset: 0x00232D85
		ADRawEntry[] IDirectorySession.Find(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			base.CheckDispose();
			return this.configurationSession.Find(rootId, scope, filter, sortBy, maxResults, properties);
		}

		// Token: 0x06008136 RID: 33078 RVA: 0x00234BA1 File Offset: 0x00232DA1
		TResult[] IDirectorySession.Find<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			base.CheckDispose();
			return this.configurationSession.Find<TResult>(rootId, scope, filter, sortBy, maxResults);
		}

		// Token: 0x06008137 RID: 33079 RVA: 0x00234BBB File Offset: 0x00232DBB
		ADRawEntry[] IDirectorySession.FindAllADRawEntriesByUsnRange(ADObjectId root, long startUsn, long endUsn, int sizeLimit, bool useAtomicFilter, IEnumerable<PropertyDefinition> properties)
		{
			base.CheckDispose();
			return this.configurationSession.FindAllADRawEntriesByUsnRange(root, startUsn, endUsn, sizeLimit, useAtomicFilter, properties);
		}

		// Token: 0x06008138 RID: 33080 RVA: 0x00234BD7 File Offset: 0x00232DD7
		Result<ADRawEntry>[] IDirectorySession.FindByADObjectIds(ADObjectId[] ids, params PropertyDefinition[] properties)
		{
			base.CheckDispose();
			return this.configurationSession.FindByADObjectIds(ids, properties);
		}

		// Token: 0x06008139 RID: 33081 RVA: 0x00234BEC File Offset: 0x00232DEC
		Result<TData>[] IDirectorySession.FindByADObjectIds<TData>(ADObjectId[] ids)
		{
			base.CheckDispose();
			return this.configurationSession.FindByADObjectIds<TData>(ids);
		}

		// Token: 0x0600813A RID: 33082 RVA: 0x00234C00 File Offset: 0x00232E00
		Result<ADRawEntry>[] IDirectorySession.FindByCorrelationIds(Guid[] correlationIds, ADObjectId configUnit, params PropertyDefinition[] properties)
		{
			base.CheckDispose();
			return this.configurationSession.FindByCorrelationIds(correlationIds, configUnit, properties);
		}

		// Token: 0x0600813B RID: 33083 RVA: 0x00234C16 File Offset: 0x00232E16
		Result<ADRawEntry>[] IDirectorySession.FindByExchangeLegacyDNs(string[] exchangeLegacyDNs, params PropertyDefinition[] properties)
		{
			base.CheckDispose();
			return this.configurationSession.FindByExchangeLegacyDNs(exchangeLegacyDNs, properties);
		}

		// Token: 0x0600813C RID: 33084 RVA: 0x00234C2B File Offset: 0x00232E2B
		Result<ADRawEntry>[] IDirectorySession.FindByObjectGuids(Guid[] objectGuids, params PropertyDefinition[] properties)
		{
			base.CheckDispose();
			return this.configurationSession.FindByObjectGuids(objectGuids, properties);
		}

		// Token: 0x0600813D RID: 33085 RVA: 0x00234C40 File Offset: 0x00232E40
		ADRawEntry[] IDirectorySession.FindDeletedTenantSyncObjectByUsnRange(ADObjectId tenantOuRoot, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties)
		{
			base.CheckDispose();
			return this.configurationSession.FindDeletedTenantSyncObjectByUsnRange(tenantOuRoot, startUsn, sizeLimit, properties);
		}

		// Token: 0x0600813E RID: 33086 RVA: 0x00234C58 File Offset: 0x00232E58
		ADPagedReader<TResult> IDirectorySession.FindPaged<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			base.CheckDispose();
			return this.configurationSession.FindPaged<TResult>(rootId, scope, filter, sortBy, pageSize, properties);
		}

		// Token: 0x0600813F RID: 33087 RVA: 0x00234C74 File Offset: 0x00232E74
		ADPagedReader<ADRawEntry> IDirectorySession.FindPagedADRawEntry(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			base.CheckDispose();
			return this.configurationSession.FindPagedADRawEntry(rootId, scope, filter, sortBy, pageSize, properties);
		}

		// Token: 0x06008140 RID: 33088 RVA: 0x00234C90 File Offset: 0x00232E90
		ADPagedReader<ADRawEntry> IDirectorySession.FindPagedADRawEntryWithDefaultFilters<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			base.CheckDispose();
			return this.configurationSession.FindPagedADRawEntryWithDefaultFilters<TResult>(rootId, scope, filter, sortBy, pageSize, properties);
		}

		// Token: 0x06008141 RID: 33089 RVA: 0x00234CAC File Offset: 0x00232EAC
		ADPagedReader<TResult> IDirectorySession.FindPagedDeletedObject<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			base.CheckDispose();
			return this.configurationSession.FindPagedDeletedObject<TResult>(rootId, scope, filter, sortBy, pageSize);
		}

		// Token: 0x06008142 RID: 33090 RVA: 0x00234CC6 File Offset: 0x00232EC6
		ADObjectId IDirectorySession.GetConfigurationNamingContext()
		{
			base.CheckDispose();
			return this.configurationSession.GetConfigurationNamingContext();
		}

		// Token: 0x06008143 RID: 33091 RVA: 0x00234CD9 File Offset: 0x00232ED9
		ADObjectId IDirectorySession.GetConfigurationUnitsRoot()
		{
			base.CheckDispose();
			return this.configurationSession.GetConfigurationUnitsRoot();
		}

		// Token: 0x06008144 RID: 33092 RVA: 0x00234CEC File Offset: 0x00232EEC
		ADObjectId IDirectorySession.GetDomainNamingContext()
		{
			base.CheckDispose();
			return this.configurationSession.GetDomainNamingContext();
		}

		// Token: 0x06008145 RID: 33093 RVA: 0x00234CFF File Offset: 0x00232EFF
		ADObjectId IDirectorySession.GetHostedOrganizationsRoot()
		{
			base.CheckDispose();
			return this.configurationSession.GetHostedOrganizationsRoot();
		}

		// Token: 0x06008146 RID: 33094 RVA: 0x00234D12 File Offset: 0x00232F12
		ADObjectId IDirectorySession.GetRootDomainNamingContext()
		{
			base.CheckDispose();
			return this.configurationSession.GetRootDomainNamingContext();
		}

		// Token: 0x06008147 RID: 33095 RVA: 0x00234D25 File Offset: 0x00232F25
		ADObjectId IDirectorySession.GetSchemaNamingContext()
		{
			base.CheckDispose();
			return this.configurationSession.GetSchemaNamingContext();
		}

		// Token: 0x06008148 RID: 33096 RVA: 0x00234D38 File Offset: 0x00232F38
		PooledLdapConnection IDirectorySession.GetReadConnection(string preferredServer, ref ADObjectId rootId)
		{
			base.CheckDispose();
			return this.configurationSession.GetReadConnection(preferredServer, ref rootId);
		}

		// Token: 0x06008149 RID: 33097 RVA: 0x00234D4D File Offset: 0x00232F4D
		PooledLdapConnection IDirectorySession.GetReadConnection(string preferredServer, string optionalBaseDN, ref ADObjectId rootId, ADRawEntry scopeDeteriminingObject)
		{
			base.CheckDispose();
			return this.configurationSession.GetReadConnection(preferredServer, optionalBaseDN, ref rootId, scopeDeteriminingObject);
		}

		// Token: 0x0600814A RID: 33098 RVA: 0x00234D65 File Offset: 0x00232F65
		ADScope IDirectorySession.GetReadScope(ADObjectId rootId, ADRawEntry scopeDeterminingObject)
		{
			base.CheckDispose();
			return this.configurationSession.GetReadScope(rootId, scopeDeterminingObject);
		}

		// Token: 0x0600814B RID: 33099 RVA: 0x00234D7A File Offset: 0x00232F7A
		ADScope IDirectorySession.GetReadScope(ADObjectId rootId, ADRawEntry scopeDeterminingObject, bool isWellKnownGuidSearch, out ConfigScopes applicableScope)
		{
			base.CheckDispose();
			return this.configurationSession.GetReadScope(rootId, scopeDeterminingObject, isWellKnownGuidSearch, out applicableScope);
		}

		// Token: 0x0600814C RID: 33100 RVA: 0x00234D92 File Offset: 0x00232F92
		bool IDirectorySession.GetSchemaAndApplyFilter(ADRawEntry adRawEntry, ADScope scope, out ADObject dummyObject, out string[] ldapAttributes, ref QueryFilter filter, ref IEnumerable<PropertyDefinition> properties)
		{
			base.CheckDispose();
			return this.configurationSession.GetSchemaAndApplyFilter(adRawEntry, scope, out dummyObject, out ldapAttributes, ref filter, ref properties);
		}

		// Token: 0x0600814D RID: 33101 RVA: 0x00234DAE File Offset: 0x00232FAE
		bool IDirectorySession.IsReadConnectionAvailable()
		{
			base.CheckDispose();
			return this.configurationSession.IsReadConnectionAvailable();
		}

		// Token: 0x0600814E RID: 33102 RVA: 0x00234DC1 File Offset: 0x00232FC1
		bool IDirectorySession.IsRootIdWithinScope<TObject>(ADObjectId rootId)
		{
			base.CheckDispose();
			return this.configurationSession.IsRootIdWithinScope<TObject>(rootId);
		}

		// Token: 0x0600814F RID: 33103 RVA: 0x00234DD5 File Offset: 0x00232FD5
		bool IDirectorySession.IsTenantIdentity(ADObjectId id)
		{
			base.CheckDispose();
			return this.configurationSession.IsTenantIdentity(id);
		}

		// Token: 0x06008150 RID: 33104 RVA: 0x00234DE9 File Offset: 0x00232FE9
		TResult[] IDirectorySession.ObjectsFromEntries<TResult>(SearchResultEntryCollection entries, string originatingServerName, IEnumerable<PropertyDefinition> properties, ADRawEntry dummyInstance)
		{
			base.CheckDispose();
			return this.configurationSession.ObjectsFromEntries<TResult>(entries, originatingServerName, properties, dummyInstance);
		}

		// Token: 0x06008151 RID: 33105 RVA: 0x00234E01 File Offset: 0x00233001
		ADRawEntry IDirectorySession.ReadADRawEntry(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			base.CheckDispose();
			return this.configurationSession.ReadADRawEntry(entryId, properties);
		}

		// Token: 0x06008152 RID: 33106 RVA: 0x00234E16 File Offset: 0x00233016
		RawSecurityDescriptor IDirectorySession.ReadSecurityDescriptor(ADObjectId id)
		{
			base.CheckDispose();
			return this.configurationSession.ReadSecurityDescriptor(id);
		}

		// Token: 0x06008153 RID: 33107 RVA: 0x00234E2A File Offset: 0x0023302A
		SecurityDescriptor IDirectorySession.ReadSecurityDescriptorBlob(ADObjectId id)
		{
			base.CheckDispose();
			return this.configurationSession.ReadSecurityDescriptorBlob(id);
		}

		// Token: 0x06008154 RID: 33108 RVA: 0x00234E3E File Offset: 0x0023303E
		string[] IDirectorySession.ReplicateSingleObject(ADObject instanceToReplicate, ADObjectId[] sites)
		{
			base.CheckDispose();
			return this.configurationSession.ReplicateSingleObject(instanceToReplicate, sites);
		}

		// Token: 0x06008155 RID: 33109 RVA: 0x00234E53 File Offset: 0x00233053
		bool IDirectorySession.ReplicateSingleObjectToTargetDC(ADObject instanceToReplicate, string targetServerName)
		{
			base.CheckDispose();
			return this.configurationSession.ReplicateSingleObjectToTargetDC(instanceToReplicate, targetServerName);
		}

		// Token: 0x06008156 RID: 33110 RVA: 0x00234E68 File Offset: 0x00233068
		TResult IDirectorySession.ResolveWellKnownGuid<TResult>(Guid wellKnownGuid, ADObjectId containerId)
		{
			base.CheckDispose();
			return this.configurationSession.ResolveWellKnownGuid<TResult>(wellKnownGuid, containerId);
		}

		// Token: 0x06008157 RID: 33111 RVA: 0x00234E7D File Offset: 0x0023307D
		TResult IDirectorySession.ResolveWellKnownGuid<TResult>(Guid wellKnownGuid, string containerDN)
		{
			base.CheckDispose();
			return this.configurationSession.ResolveWellKnownGuid<TResult>(wellKnownGuid, containerDN);
		}

		// Token: 0x06008158 RID: 33112 RVA: 0x00234E92 File Offset: 0x00233092
		TenantRelocationSyncObject IDirectorySession.RetrieveTenantRelocationSyncObject(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			base.CheckDispose();
			return this.configurationSession.RetrieveTenantRelocationSyncObject(entryId, properties);
		}

		// Token: 0x06008159 RID: 33113 RVA: 0x00234EA7 File Offset: 0x002330A7
		ADOperationResultWithData<TResult>[] IDirectorySession.RunAgainstAllDCsInSite<TResult>(ADObjectId siteId, Func<TResult> methodToCall)
		{
			base.CheckDispose();
			return this.configurationSession.RunAgainstAllDCsInSite<TResult>(siteId, methodToCall);
		}

		// Token: 0x0600815A RID: 33114 RVA: 0x00234EBC File Offset: 0x002330BC
		void IDirectorySession.SaveSecurityDescriptor(ADObjectId id, RawSecurityDescriptor sd)
		{
			base.CheckDispose();
			this.configurationSession.SaveSecurityDescriptor(id, sd);
		}

		// Token: 0x0600815B RID: 33115 RVA: 0x00234ED1 File Offset: 0x002330D1
		void IDirectorySession.SaveSecurityDescriptor(ADObjectId id, RawSecurityDescriptor sd, bool modifyOwner)
		{
			base.CheckDispose();
			this.configurationSession.SaveSecurityDescriptor(id, sd, modifyOwner);
		}

		// Token: 0x0600815C RID: 33116 RVA: 0x00234EE7 File Offset: 0x002330E7
		void IDirectorySession.SaveSecurityDescriptor(ADObject obj, RawSecurityDescriptor sd)
		{
			base.CheckDispose();
			this.configurationSession.SaveSecurityDescriptor(obj, sd);
		}

		// Token: 0x0600815D RID: 33117 RVA: 0x00234EFC File Offset: 0x002330FC
		void IDirectorySession.SaveSecurityDescriptor(ADObject obj, RawSecurityDescriptor sd, bool modifyOwner)
		{
			base.CheckDispose();
			this.configurationSession.SaveSecurityDescriptor(obj, sd, modifyOwner);
		}

		// Token: 0x0600815E RID: 33118 RVA: 0x00234F12 File Offset: 0x00233112
		bool IDirectorySession.TryVerifyIsWithinScopes(ADObject entry, bool isModification, out ADScopeException exception)
		{
			base.CheckDispose();
			return this.configurationSession.TryVerifyIsWithinScopes(entry, isModification, out exception);
		}

		// Token: 0x0600815F RID: 33119 RVA: 0x00234F28 File Offset: 0x00233128
		void IDirectorySession.UpdateServerSettings(PooledLdapConnection connection)
		{
			base.CheckDispose();
			this.configurationSession.UpdateServerSettings(connection);
		}

		// Token: 0x06008160 RID: 33120 RVA: 0x00234F3C File Offset: 0x0023313C
		void IDirectorySession.VerifyIsWithinScopes(ADObject entry, bool isModification)
		{
			base.CheckDispose();
			this.configurationSession.VerifyIsWithinScopes(entry, isModification);
		}

		// Token: 0x040056F0 RID: 22256
		private static TenantInfoProviderFactory tenantInfoProviderFactory = new TenantInfoProviderFactory(TimeSpan.FromHours(4.0), 10, 1000);

		// Token: 0x040056F1 RID: 22257
		internal static readonly bool IsFFOOnline = Datacenter.IsForefrontForOfficeDatacenter();

		// Token: 0x040056F2 RID: 22258
		private DisposeTracker disposeTracker;

		// Token: 0x040056F3 RID: 22259
		private IConfigurationSession configurationSession;

		// Token: 0x040056F4 RID: 22260
		private ExBindingStoreObjectProvider exBindingStoreProvider;
	}
}
