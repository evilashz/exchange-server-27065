using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200032A RID: 810
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class ADConfigurationSession : ADDataSession, IConfigurationSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x0600258E RID: 9614 RVA: 0x0009F1E0 File Offset: 0x0009D3E0
		public ADConfigurationSession(bool useConfigNC, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings) : base(useConfigNC, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x0009F1EF File Offset: 0x0009D3EF
		void IConfigDataProvider.Delete(IConfigurable instance)
		{
			this.Delete((ADConfigurationObject)instance);
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x0009F200 File Offset: 0x0009D400
		IConfigurable[] IConfigDataProvider.Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			if (!typeof(ADConfigurationObject).IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException(DirectoryStrings.ExceptionADConfigurationObjectRequired(typeof(T).Name, "IConfigDataProvider.Find<T>"), "T");
			}
			return (IConfigurable[])base.Find<T>((ADObjectId)rootId, deepSearch ? QueryScope.SubTree : QueryScope.OneLevel, filter, sortBy, 0, null, false);
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x0009F270 File Offset: 0x0009D470
		public AcceptedDomain[] FindAcceptedDomainsByFederatedOrgId(FederatedOrganizationId federatedOrganizationId)
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, AcceptedDomainSchema.FederatedOrganizationLink, federatedOrganizationId.Id)
			});
			return base.Find<AcceptedDomain>(federatedOrganizationId.ConfigurationUnit, QueryScope.SubTree, filter, null, 0);
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x0009F2B0 File Offset: 0x0009D4B0
		public OfflineAddressBook[] FindOABsForWebDistributionPoint(ADOabVirtualDirectory vDir)
		{
			if (vDir == null)
			{
				throw new ArgumentNullException("vDir");
			}
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, OfflineAddressBookSchema.VirtualDirectories, vDir.Id);
			ADPagedReader<OfflineAddressBook> adpagedReader = this.FindPaged<OfflineAddressBook>(null, QueryScope.SubTree, filter, null, 0);
			List<OfflineAddressBook> list = new List<OfflineAddressBook>();
			foreach (OfflineAddressBook item in adpagedReader)
			{
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x0009F338 File Offset: 0x0009D538
		IEnumerable<T> IConfigDataProvider.FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			return base.FindPaged<T>((ADObjectId)rootId, deepSearch ? QueryScope.SubTree : QueryScope.OneLevel, filter, sortBy, pageSize, null);
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x0009F353 File Offset: 0x0009D553
		IConfigurable IConfigDataProvider.Read<T>(ObjectId identity)
		{
			return base.InternalRead<T>((ADObjectId)identity, null);
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x0009F367 File Offset: 0x0009D567
		void IConfigDataProvider.Save(IConfigurable instance)
		{
			this.Save((ADConfigurationObject)instance);
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x0009F375 File Offset: 0x0009D575
		string IConfigDataProvider.Source
		{
			get
			{
				return base.LastUsedDc;
			}
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x0009F37D File Offset: 0x0009D57D
		public bool CheckForRetentionPolicyWithConflictingRetentionId(Guid retentionId, out string duplicateName)
		{
			return this.CheckForRetentionPolicyWithConflictingRetentionId(retentionId, null, out duplicateName);
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x0009F388 File Offset: 0x0009D588
		public bool CheckForRetentionPolicyWithConflictingRetentionId(Guid retentionId, string identity, out string duplicateName)
		{
			duplicateName = string.Empty;
			ADPagedReader<RetentionPolicy> adpagedReader = this.FindPaged<RetentionPolicy>(this.GetOrgContainerId(), QueryScope.SubTree, null, null, 0);
			foreach (RetentionPolicy retentionPolicy in adpagedReader)
			{
				if (retentionPolicy.RetentionId == retentionId)
				{
					if (!string.IsNullOrEmpty(identity) && retentionPolicy.Identity.Equals(identity))
					{
						return true;
					}
					duplicateName = retentionPolicy.Name;
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x0009F418 File Offset: 0x0009D618
		public bool CheckForRetentionTagWithConflictingRetentionId(Guid retentionId, out string duplicateName)
		{
			return this.CheckForRetentionTagWithConflictingRetentionId(retentionId, null, out duplicateName);
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x0009F424 File Offset: 0x0009D624
		public bool CheckForRetentionTagWithConflictingRetentionId(Guid retentionId, string identity, out string duplicateName)
		{
			duplicateName = string.Empty;
			ADPagedReader<RetentionPolicyTag> adpagedReader = this.FindPaged<RetentionPolicyTag>(this.GetOrgContainerId(), QueryScope.SubTree, null, null, 0);
			foreach (RetentionPolicyTag retentionPolicyTag in adpagedReader)
			{
				if (retentionPolicyTag.RetentionId == retentionId)
				{
					if (!string.IsNullOrEmpty(identity) && retentionPolicyTag.Identity.Equals(identity))
					{
						return true;
					}
					duplicateName = retentionPolicyTag.Name;
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x0009F4B4 File Offset: 0x0009D6B4
		protected override ADObject CreateAndInitializeObject<TResult>(ADPropertyBag propertyBag, ADRawEntry dummyObject)
		{
			return ADObjectFactory.CreateAndInitializeConfigObject<TResult>(propertyBag, dummyObject, this);
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x0009F4BE File Offset: 0x0009D6BE
		public ADPagedReader<TResult> FindAllPaged<TResult>() where TResult : ADConfigurationObject, new()
		{
			return this.FindPaged<TResult>(null, QueryScope.SubTree, null, null, 0);
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x0009F4CC File Offset: 0x0009D6CC
		public TResult FindByExchangeObjectId<TResult>(Guid exchangeObjectId) where TResult : ADConfigurationObject, new()
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ExchangeObjectId, exchangeObjectId);
			TResult[] array = base.Find<TResult>(null, QueryScope.SubTree, filter, null, 2);
			if (array == null || array.Length == 0)
			{
				return default(TResult);
			}
			return array[0];
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x0009F510 File Offset: 0x0009D710
		public TResult Read<TResult>(ADObjectId entryId) where TResult : ADConfigurationObject, new()
		{
			return base.InternalRead<TResult>(entryId, null);
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x0009F51A File Offset: 0x0009D71A
		public void Save(ADConfigurationObject instanceToSave)
		{
			if (instanceToSave == null)
			{
				throw new ArgumentNullException("instanceToSave");
			}
			base.Save(instanceToSave, instanceToSave.Schema.AllProperties);
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x0009F53C File Offset: 0x0009D73C
		public void Delete(ADConfigurationObject instanceToDelete)
		{
			base.Delete(instanceToDelete);
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x0009F548 File Offset: 0x0009D748
		public void DeleteTree(ADConfigurationObject instanceToDelete, TreeDeleteNotFinishedHandler handler)
		{
			if (instanceToDelete == null)
			{
				throw new ArgumentNullException("instanceToDelete");
			}
			ADConfigurationSession adconfigurationSession = this;
			IDirectorySession session = instanceToDelete.Session;
			try
			{
				for (;;)
				{
					try
					{
						adconfigurationSession.Delete(instanceToDelete, true);
					}
					catch (ADTreeDeleteNotFinishedException ex)
					{
						ExTraceGlobals.ADTopologyTracer.TraceWarning<string, string>((long)this.GetHashCode(), "ADTreeDeleteNotFinishedException is caught while deleting the whole tree '{0}': {1}", instanceToDelete.Identity.ToString(), ex.Message);
						if (handler != null)
						{
							handler(ex);
						}
						if (string.IsNullOrEmpty(adconfigurationSession.DomainController))
						{
							adconfigurationSession = (ADConfigurationSession)this.Clone();
							adconfigurationSession.DomainController = ex.Server;
							instanceToDelete.m_Session = null;
						}
						continue;
					}
					break;
				}
			}
			finally
			{
				instanceToDelete.m_Session = session;
			}
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x0009F600 File Offset: 0x0009D800
		private object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x0009F608 File Offset: 0x0009D808
		public T FindSingletonConfigurationObject<T>() where T : ADConfigurationObject, new()
		{
			T[] array = base.Find<T>(null, QueryScope.SubTree, null, null, 1);
			if (array.Length == 0)
			{
				return default(T);
			}
			return array[0];
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x0009F638 File Offset: 0x0009D838
		public AvailabilityAddressSpace GetAvailabilityAddressSpace(string domainName)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, AvailabilityAddressSpaceSchema.ForestName, domainName);
			AvailabilityAddressSpace[] array = base.Find<AvailabilityAddressSpace>(this.GetOrgContainerId(), QueryScope.SubTree, filter, null, 1);
			if (array.Length == 1)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x0009F670 File Offset: 0x0009D870
		public AvailabilityConfig GetAvailabilityConfig()
		{
			AvailabilityConfig[] array = base.Find<AvailabilityConfig>(this.GetOrgContainerId(), QueryScope.SubTree, null, null, 1);
			if (array.Length == 1)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x0009F699 File Offset: 0x0009D899
		public ExchangeConfigurationContainer GetExchangeConfigurationContainer()
		{
			return this.InternalGetExchangeConfigurationContainer<ExchangeConfigurationContainer>();
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x0009F6A1 File Offset: 0x0009D8A1
		public ExchangeConfigurationContainerWithAddressLists GetExchangeConfigurationContainerWithAddressLists()
		{
			return this.InternalGetExchangeConfigurationContainer<ExchangeConfigurationContainerWithAddressLists>();
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x0009F6A9 File Offset: 0x0009D8A9
		public ThrottlingPolicy GetOrganizationThrottlingPolicy(OrganizationId organizationId)
		{
			return this.GetOrganizationThrottlingPolicy(organizationId, true);
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x0009F6B4 File Offset: 0x0009D8B4
		public ThrottlingPolicy GetOrganizationThrottlingPolicy(OrganizationId organizationId, bool logFailedLookup)
		{
			string text = (OrganizationId.ForestWideOrgId.Equals(organizationId) || organizationId == null) ? this.GetOrgContainerId().DistinguishedName : organizationId.ConfigurationUnit.DistinguishedName;
			ThrottlingPolicy[] array = this.FindOrganizationThrottlingPolicies(organizationId);
			if (array == null || array.Length == 0)
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceDebug<string>((long)this.GetHashCode(), "[ADConfigurationSession::GetOrganizationThrottlingPolicy] No organization policy found in org '{0}'.", text);
				return null;
			}
			if (array.Length != 1)
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceError<string>((long)this.GetHashCode(), "[ADConfigurationSession::GetOrganizationThrottlingPolicy] Multiple organization policies found in org '{0}'.", text);
				if (logFailedLookup)
				{
					Globals.LogExchangeTopologyEvent(DirectoryEventLogConstants.Tuple_MoreThanOneOrganizationThrottlingPolicy, text, new object[]
					{
						text
					});
				}
				return null;
			}
			return array[0];
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x0009F758 File Offset: 0x0009D958
		public ADObjectId GetOrgContainerId()
		{
			if (this.orgContainerId != null)
			{
				return this.orgContainerId;
			}
			if (Globals.IsDatacenter && (base.SessionSettings.ConfigReadScope == null || base.SessionSettings.ConfigReadScope.Root == null))
			{
				this.orgContainerId = ADSystemConfigurationSession.GetRootOrgContainerId(base.SessionSettings.GetAccountOrResourceForestFqdn(), base.DomainController, base.NetworkCredential);
			}
			if (this.orgContainerId == null)
			{
				Organization orgContainer = this.GetOrgContainer();
				this.orgContainerId = orgContainer.Id;
			}
			return this.orgContainerId;
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x0009F7E0 File Offset: 0x0009D9E0
		public Organization GetOrgContainer()
		{
			ADObjectId adobjectId = null;
			if (base.SessionSettings.ConfigReadScope != null)
			{
				adobjectId = base.SessionSettings.ConfigReadScope.Root;
			}
			Organization[] array;
			if (adobjectId == null)
			{
				if (Globals.IsDatacenter)
				{
					Organization organization = (Organization)ADSystemConfigurationSession.GetRootOrgContainer(base.SessionSettings.GetAccountOrResourceForestFqdn(), base.DomainController, base.NetworkCredential).Clone();
					organization.SetIsReadOnly(false);
					organization.m_Session = this;
					return organization;
				}
				array = base.Find<Organization>(null, QueryScope.SubTree, null, null, 2);
			}
			else if (adobjectId.Parent.Parent.Equals(ADSession.GetConfigurationUnitsRoot(base.SessionSettings.GetAccountOrResourceForestFqdn())))
			{
				array = base.Find<ExchangeConfigurationUnit>(adobjectId, QueryScope.Base, null, null, 1);
			}
			else
			{
				array = base.Find<ExchangeConfigurationUnit>(adobjectId, QueryScope.SubTree, null, null, 2);
			}
			if (array == null || array.Length == 0)
			{
				if (adobjectId == null)
				{
					throw new OrgContainerNotFoundException();
				}
				throw new TenantOrgContainerNotFoundException(adobjectId.ToString());
			}
			else
			{
				if (array.Length > 1)
				{
					throw new OrgContainerAmbiguousException();
				}
				return array[0];
			}
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x0009F8C8 File Offset: 0x0009DAC8
		public RbacContainer GetRbacContainer()
		{
			ADObjectId adobjectId = this.GetOrgContainerId();
			return this.Read<RbacContainer>(adobjectId.GetDescendantId(new ADObjectId("CN=RBAC")));
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x0009F910 File Offset: 0x0009DB10
		public ManagementScope[] FindSimilarManagementScope(ManagementScope managementScope)
		{
			if (managementScope == null)
			{
				throw new ArgumentNullException("managementScope");
			}
			QueryFilter queryFilter;
			if (managementScope.RecipientRoot == null)
			{
				queryFilter = new NotFilter(new ExistsFilter(ManagementScopeSchema.RecipientRoot));
			}
			else
			{
				queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ManagementScopeSchema.RecipientRoot, managementScope.RecipientRoot);
			}
			QueryFilter queryFilter2;
			if (string.IsNullOrEmpty(managementScope.Filter))
			{
				queryFilter2 = new NotFilter(new ExistsFilter(ManagementScopeSchema.Filter));
			}
			else
			{
				queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ManagementScopeSchema.Filter, managementScope.Filter);
			}
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				queryFilter,
				queryFilter2,
				new ComparisonFilter(ComparisonOperator.Equal, ManagementScopeSchema.Exclusive, managementScope.Exclusive),
				new ComparisonFilter(ComparisonOperator.Equal, ManagementScopeSchema.ScopeRestrictionType, managementScope.ScopeRestrictionType),
				new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Id, managementScope.Id)
			});
			List<ManagementScope> source = new List<ManagementScope>(base.Find<ManagementScope>(managementScope.Id.Parent, QueryScope.OneLevel, filter, null, 0));
			return (from scope in source
			where scope.ScopeRestrictionType == managementScope.ScopeRestrictionType
			select scope).ToArray<ManagementScope>();
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x0009FA68 File Offset: 0x0009DC68
		public bool ManagementScopeIsInUse(ManagementScope managementScope)
		{
			ExchangeRoleAssignment[] array = this.FindAssignmentsForManagementScope(managementScope, false);
			return array.Length != 0;
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x0009FA88 File Offset: 0x0009DC88
		public ExchangeRoleAssignment[] FindAssignmentsForManagementScope(ManagementScope managementScope, bool returnAll)
		{
			if (managementScope == null)
			{
				throw new ArgumentNullException("managementScope");
			}
			QueryFilter filter = new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.CustomRecipientWriteScope, managementScope.Id),
				new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.CustomConfigWriteScope, managementScope.Id)
			});
			ADPagedReader<ExchangeRoleAssignment> adpagedReader = this.FindPaged<ExchangeRoleAssignment>(null, QueryScope.SubTree, filter, null, returnAll ? 0 : 1);
			List<ExchangeRoleAssignment> list = new List<ExchangeRoleAssignment>();
			foreach (ExchangeRoleAssignment item in adpagedReader)
			{
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x0009FB3C File Offset: 0x0009DD3C
		public NspiRpcClientConnection GetNspiRpcClientConnection()
		{
			string text = base.ServerSettings.PreferredGlobalCatalog(base.SessionSettings.GetAccountOrResourceForestFqdn());
			string domainController;
			if (!string.IsNullOrEmpty(text))
			{
				domainController = text;
			}
			else
			{
				PooledLdapConnection pooledLdapConnection = null;
				try
				{
					pooledLdapConnection = ConnectionPoolManager.GetConnection(ConnectionType.GlobalCatalog, base.SessionSettings.GetAccountOrResourceForestFqdn());
					domainController = pooledLdapConnection.ServerName;
				}
				finally
				{
					if (pooledLdapConnection != null)
					{
						pooledLdapConnection.ReturnToPool();
					}
				}
			}
			return NspiRpcClientConnection.GetNspiRpcClientConnection(domainController);
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x0009FBB0 File Offset: 0x0009DDB0
		public T FindMailboxPolicyByName<T>(string name) where T : MailboxPolicy, new()
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, name);
			T[] array = base.Find<T>(null, QueryScope.SubTree, filter, null, 2);
			if (array == null || array.Length <= 0)
			{
				return default(T);
			}
			return array[0];
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x0009FBF0 File Offset: 0x0009DDF0
		public MicrosoftExchangeRecipient FindMicrosoftExchangeRecipient()
		{
			return this.Read<MicrosoftExchangeRecipient>(ADMicrosoftExchangeRecipient.GetDefaultId(this));
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x0009FC00 File Offset: 0x0009DE00
		public ThrottlingPolicy[] FindOrganizationThrottlingPolicies(OrganizationId organizationId)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ThrottlingPolicySchema.ThrottlingPolicyScope, ThrottlingPolicyScopeType.Organization);
			if (organizationId == null && (base.ConfigScope == ConfigScopes.TenantSubTree || base.ConfigScope == ConfigScopes.None))
			{
				return null;
			}
			ADObjectId rootId = (OrganizationId.ForestWideOrgId.Equals(organizationId) || organizationId == null) ? this.GetOrgContainerId() : organizationId.ConfigurationUnit;
			return base.Find<ThrottlingPolicy>(rootId, QueryScope.SubTree, filter, null, 2);
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x0009FC6D File Offset: 0x0009DE6D
		public ADPagedReader<TResult> FindPaged<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize) where TResult : ADConfigurationObject, new()
		{
			return base.FindPaged<TResult>(rootId, scope, filter, sortBy, pageSize, null);
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x0009FC7D File Offset: 0x0009DE7D
		public FederatedOrganizationId GetFederatedOrganizationId(OrganizationId organizationId)
		{
			return this.GetFederatedOrganizationId(organizationId.ConfigurationUnit);
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x0009FC8B File Offset: 0x0009DE8B
		public FederatedOrganizationId GetFederatedOrganizationId()
		{
			return this.GetFederatedOrganizationId(this.GetOrgContainerId());
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x0009FC9C File Offset: 0x0009DE9C
		private FederatedOrganizationId GetFederatedOrganizationId(ADObjectId rootId)
		{
			FederatedOrganizationId[] array = base.Find<FederatedOrganizationId>(rootId, QueryScope.SubTree, null, null, 1);
			if (array.Length == 1)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x0009FCC0 File Offset: 0x0009DEC0
		public FederatedOrganizationId GetFederatedOrganizationIdByDomainName(string domainName)
		{
			AcceptedDomain acceptedDomainByDomainName = this.GetAcceptedDomainByDomainName(domainName);
			if (acceptedDomainByDomainName == null || acceptedDomainByDomainName.FederatedOrganizationLink == null)
			{
				return null;
			}
			return this.Read<FederatedOrganizationId>(acceptedDomainByDomainName.FederatedOrganizationLink);
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x0009FCF0 File Offset: 0x0009DEF0
		public AcceptedDomain GetDefaultAcceptedDomain()
		{
			BitMaskAndFilter filter = new BitMaskAndFilter(AcceptedDomainSchema.AcceptedDomainFlags, 4UL);
			AcceptedDomain[] array = base.Find<AcceptedDomain>(this.GetOrgContainerId(), QueryScope.SubTree, filter, null, 0);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			if (array.Length > 1)
			{
				Globals.LogExchangeTopologyEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_MULTIPLE_DEFAULT_ACCEPTED_DOMAIN, array[0].DistinguishedName, new object[]
				{
					array.Length.ToString()
				});
			}
			return array[0];
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x0009FD58 File Offset: 0x0009DF58
		public AcceptedDomain GetAcceptedDomainByDomainName(string domainName)
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectClassFilter("msExchAcceptedDomain"),
				new ComparisonFilter(ComparisonOperator.Equal, AcceptedDomainSchema.DomainName, domainName)
			});
			AcceptedDomain[] array = base.Find<AcceptedDomain>(null, QueryScope.SubTree, filter, null, 2);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			if (array.Length == 2)
			{
				throw new ADOperationException(DirectoryStrings.DuplicatedAcceptedDomain(domainName, array[0].Id.ToDNString(), array[1].Id.ToDNString()));
			}
			return array[0];
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x0009FDD4 File Offset: 0x0009DFD4
		public OrganizationRelationship GetOrganizationRelationship(string domainName)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, OrganizationRelationshipSchema.DomainNames, domainName);
			OrganizationRelationship[] array = base.Find<OrganizationRelationship>(this.GetOrgContainerId(), QueryScope.SubTree, filter, null, 1);
			if (array.Length == 1)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x0009FE0C File Offset: 0x0009E00C
		public Result<ExchangeRoleAssignment>[] FindRoleAssignmentsByUserIds(ADObjectId[] securityPrincipalIds, bool partnerMode)
		{
			QueryFilter partnerFilter = RoleAssignmentFlagsFormat.GetPartnerFilter(partnerMode);
			return this.FindRoleAssignmentsByUserIds(securityPrincipalIds, partnerFilter);
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x0009FE44 File Offset: 0x0009E044
		public Result<ExchangeRoleAssignment>[] FindRoleAssignmentsByUserIds(ADObjectId[] securityPrincipalIds, QueryFilter additionalFilter)
		{
			if (securityPrincipalIds == null)
			{
				throw new ArgumentNullException("securityPrincipalIds");
			}
			if (securityPrincipalIds.Length == 0)
			{
				throw new ArgumentException("securityPrincipalIds");
			}
			Converter<ADObjectId, QueryFilter> filterBuilder = delegate(ADObjectId id)
			{
				if (id == null)
				{
					throw new ArgumentNullException("id");
				}
				return new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.User, id);
			};
			return base.ReadMultiple<ADObjectId, ExchangeRoleAssignment>(securityPrincipalIds, null, filterBuilder, additionalFilter, null, null, null);
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0009FE9C File Offset: 0x0009E09C
		public Result<TResult>[] ReadMultiple<TResult>(ADObjectId[] identities) where TResult : ADConfigurationObject, new()
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			if (identities.Length == 0)
			{
				return new Result<TResult>[0];
			}
			return base.ReadMultiple<ADObjectId, TResult>(identities, new Converter<ADObjectId, QueryFilter>(ADRecipientObjectSession.ADObjectIdFilterBuilder), new ADDataSession.HashInserter<TResult>(ADRecipientObjectSession.ADObjectIdHashInserter<TResult>), new ADDataSession.HashLookup<ADObjectId, TResult>(ADRecipientObjectSession.ADObjectIdHashLookup<TResult>), null);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x0009FEEF File Offset: 0x0009E0EF
		public ADPagedReader<ManagementScope> GetAllExclusiveScopes()
		{
			return this.FindPaged<ManagementScope>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ManagementScopeSchema.Exclusive, true), null, 0);
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x0009FF0C File Offset: 0x0009E10C
		public ADPagedReader<ManagementScope> GetAllScopes(OrganizationId organizationId, ScopeRestrictionType restrictionType)
		{
			ADObjectId rootId = (OrganizationId.ForestWideOrgId.Equals(organizationId) || organizationId == null) ? this.GetOrgContainerId() : organizationId.ConfigurationUnit;
			return this.FindPaged<ManagementScope>(rootId, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ManagementScopeSchema.ScopeRestrictionType, restrictionType), null, 0);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x0009FF5C File Offset: 0x0009E15C
		public MultiValuedProperty<ReplicationCursor> ReadReplicationCursors(ADObjectId id)
		{
			if (string.IsNullOrEmpty(base.DomainController))
			{
				throw new NotSupportedException("The session has to be bound to a specific Domain Controller.");
			}
			ADRawEntry adrawEntry = base.InternalRead<ADRawEntry>(id, new ADPropertyDefinition[]
			{
				ADDomainSchema.ReplicationCursors
			});
			if (adrawEntry == null)
			{
				return new MultiValuedProperty<ReplicationCursor>();
			}
			return adrawEntry.propertyBag[ADDomainSchema.ReplicationCursors] as MultiValuedProperty<ReplicationCursor>;
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x0009FFB8 File Offset: 0x0009E1B8
		public void ReadReplicationData(ADObjectId id, out MultiValuedProperty<ReplicationCursor> replicationCursors, out MultiValuedProperty<ReplicationNeighbor> repsFrom)
		{
			if (string.IsNullOrEmpty(base.DomainController))
			{
				throw new NotSupportedException("The session has to be bound to a specific Domain Controller.");
			}
			ADRawEntry adrawEntry = base.InternalRead<ADRawEntry>(id, new ADPropertyDefinition[]
			{
				ADDomainSchema.ReplicationCursors,
				ADDomainSchema.RepsFrom
			});
			if (adrawEntry == null)
			{
				replicationCursors = new MultiValuedProperty<ReplicationCursor>();
				repsFrom = new MultiValuedProperty<ReplicationNeighbor>();
				return;
			}
			replicationCursors = (adrawEntry.propertyBag[ADDomainSchema.ReplicationCursors] as MultiValuedProperty<ReplicationCursor>);
			repsFrom = (adrawEntry.propertyBag[ADDomainSchema.RepsFrom] as MultiValuedProperty<ReplicationNeighbor>);
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x000A003D File Offset: 0x0009E23D
		public ADObjectId ConfigurationNamingContext
		{
			get
			{
				return base.GetConfigurationNamingContext();
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x000A0045 File Offset: 0x0009E245
		public virtual ADObjectId DeletedObjectsContainer
		{
			get
			{
				return this.ConfigurationNamingContext.GetChildId("Deleted Objects");
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x000A0057 File Offset: 0x0009E257
		public ADObjectId SchemaNamingContext
		{
			get
			{
				return base.GetSchemaNamingContext();
			}
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000A0060 File Offset: 0x0009E260
		private T InternalGetExchangeConfigurationContainer<T>() where T : ExchangeConfigurationContainer, new()
		{
			T[] array = base.Find<T>(null, QueryScope.SubTree, null, null, 1);
			if (array == null || array.Length == 0)
			{
				throw new ExchangeConfigurationContainerNotFoundException();
			}
			return array[0];
		}

		// Token: 0x04001703 RID: 5891
		[NonSerialized]
		private ADObjectId orgContainerId;
	}
}
