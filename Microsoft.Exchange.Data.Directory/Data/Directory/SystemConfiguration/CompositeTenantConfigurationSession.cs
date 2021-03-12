using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Cache;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200062C RID: 1580
	internal class CompositeTenantConfigurationSession : CompositeDirectorySession<ITenantConfigurationSession>, ITenantConfigurationSession, IConfigurationSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x06004A9B RID: 19099 RVA: 0x001137E8 File Offset: 0x001119E8
		protected override string Implementor
		{
			get
			{
				return "CompositeTenantConfigurationSession";
			}
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x001137EF File Offset: 0x001119EF
		internal CompositeTenantConfigurationSession(ITenantConfigurationSession cacheSession, ITenantConfigurationSession directorySession, bool cacheSessionForDeletingOnly = false) : base(cacheSession, directorySession, cacheSessionForDeletingOnly)
		{
		}

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x06004A9D RID: 19101 RVA: 0x001137FA File Offset: 0x001119FA
		ADObjectId IConfigurationSession.ConfigurationNamingContext
		{
			get
			{
				return base.GetSession().ConfigurationNamingContext;
			}
		}

		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x06004A9E RID: 19102 RVA: 0x00113807 File Offset: 0x00111A07
		ADObjectId IConfigurationSession.DeletedObjectsContainer
		{
			get
			{
				return base.GetSession().DeletedObjectsContainer;
			}
		}

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x06004A9F RID: 19103 RVA: 0x00113814 File Offset: 0x00111A14
		ADObjectId IConfigurationSession.SchemaNamingContext
		{
			get
			{
				return base.GetSession().SchemaNamingContext;
			}
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x00113821 File Offset: 0x00111A21
		bool IConfigurationSession.CheckForRetentionPolicyWithConflictingRetentionId(Guid retentionId, out string duplicateName)
		{
			return base.GetSession().CheckForRetentionPolicyWithConflictingRetentionId(retentionId, out duplicateName);
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x00113830 File Offset: 0x00111A30
		bool IConfigurationSession.CheckForRetentionPolicyWithConflictingRetentionId(Guid retentionId, string identity, out string duplicateName)
		{
			return base.GetSession().CheckForRetentionPolicyWithConflictingRetentionId(retentionId, identity, out duplicateName);
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x00113840 File Offset: 0x00111A40
		bool IConfigurationSession.CheckForRetentionTagWithConflictingRetentionId(Guid retentionId, out string duplicateName)
		{
			return base.GetSession().CheckForRetentionPolicyWithConflictingRetentionId(retentionId, out duplicateName);
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x0011384F File Offset: 0x00111A4F
		bool IConfigurationSession.CheckForRetentionTagWithConflictingRetentionId(Guid retentionId, string identity, out string duplicateName)
		{
			return base.GetSession().CheckForRetentionPolicyWithConflictingRetentionId(retentionId, identity, out duplicateName);
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x00113888 File Offset: 0x00111A88
		void IConfigurationSession.DeleteTree(ADConfigurationObject instanceToDelete, TreeDeleteNotFinishedHandler handler)
		{
			base.InvokeWithAPILogging<bool>(delegate
			{
				this.GetSession().DeleteTree(instanceToDelete, handler);
				return true;
			}, "DeleteTree");
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x001138E8 File Offset: 0x00111AE8
		AcceptedDomain[] IConfigurationSession.FindAcceptedDomainsByFederatedOrgId(FederatedOrganizationId federatedOrganizationId)
		{
			return base.InvokeWithAPILogging<AcceptedDomain[]>(() => this.GetSession().FindAcceptedDomainsByFederatedOrgId(federatedOrganizationId), "FindAcceptedDomainsByFederatedOrgId");
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x0011392D File Offset: 0x00111B2D
		ADPagedReader<TResult> IConfigurationSession.FindAllPaged<TResult>()
		{
			return base.InvokeWithAPILogging<ADPagedReader<TResult>>(() => base.GetSession().FindAllPaged<TResult>(), "FindAllPaged");
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x0011396C File Offset: 0x00111B6C
		ExchangeRoleAssignment[] IConfigurationSession.FindAssignmentsForManagementScope(ManagementScope managementScope, bool returnAll)
		{
			return base.InvokeWithAPILogging<ExchangeRoleAssignment[]>(() => this.GetSession().FindAssignmentsForManagementScope(managementScope, returnAll), "FindAssignmentsForManagementScope");
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x001139CC File Offset: 0x00111BCC
		T IConfigurationSession.FindMailboxPolicyByName<T>(string name)
		{
			return base.InvokeGetObjectWithAPILogging<T>(() => this.GetSession().FindMailboxPolicyByName<T>(name), "FindMailboxPolicyByName");
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x00113A11 File Offset: 0x00111C11
		MicrosoftExchangeRecipient IConfigurationSession.FindMicrosoftExchangeRecipient()
		{
			return base.InvokeGetObjectWithAPILogging<MicrosoftExchangeRecipient>(() => base.GetSession().FindMicrosoftExchangeRecipient(), "FindMicrosoftExchangeRecipient");
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x00113A4C File Offset: 0x00111C4C
		OfflineAddressBook[] IConfigurationSession.FindOABsForWebDistributionPoint(ADOabVirtualDirectory vDir)
		{
			return base.InvokeWithAPILogging<OfflineAddressBook[]>(() => this.GetSession().FindOABsForWebDistributionPoint(vDir), "FindOABsForWebDistributionPoint");
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x00113AA4 File Offset: 0x00111CA4
		ThrottlingPolicy[] IConfigurationSession.FindOrganizationThrottlingPolicies(OrganizationId organizationId)
		{
			return base.InvokeWithAPILogging<ThrottlingPolicy[]>(() => this.GetSession().FindOrganizationThrottlingPolicies(organizationId), "FindOrganizationThrottlingPolicies");
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x00113B14 File Offset: 0x00111D14
		ADPagedReader<TResult> IConfigurationSession.FindPaged<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			return base.InvokeWithAPILogging<ADPagedReader<TResult>>(() => this.GetSession().FindPaged<TResult>(rootId, scope, filter, sortBy, pageSize), "FindPaged");
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x00113B90 File Offset: 0x00111D90
		Result<ExchangeRoleAssignment>[] IConfigurationSession.FindRoleAssignmentsByUserIds(ADObjectId[] securityPrincipalIds, bool partnerMode)
		{
			return base.InvokeWithAPILogging<Result<ExchangeRoleAssignment>[]>(() => this.GetSession().FindRoleAssignmentsByUserIds(securityPrincipalIds, partnerMode), "FindRoleAssignmentsByUserIds");
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x00113BF8 File Offset: 0x00111DF8
		Result<ExchangeRoleAssignment>[] IConfigurationSession.FindRoleAssignmentsByUserIds(ADObjectId[] securityPrincipalIds, QueryFilter additionalFilter)
		{
			return base.InvokeWithAPILogging<Result<ExchangeRoleAssignment>[]>(() => this.GetSession().FindRoleAssignmentsByUserIds(securityPrincipalIds, additionalFilter), "FindRoleAssignmentsByUserIds");
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x00113C58 File Offset: 0x00111E58
		ManagementScope[] IConfigurationSession.FindSimilarManagementScope(ManagementScope managementScope)
		{
			return base.InvokeWithAPILogging<ManagementScope[]>(() => this.GetSession().FindSimilarManagementScope(managementScope), "FindSimilarManagementScope");
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x00113C9D File Offset: 0x00111E9D
		T IConfigurationSession.FindSingletonConfigurationObject<T>()
		{
			return base.InvokeGetObjectWithAPILogging<T>(() => base.GetSession().FindSingletonConfigurationObject<T>(), "FindSingletonConfigurationObject");
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x00113CE8 File Offset: 0x00111EE8
		AcceptedDomain IConfigurationSession.GetAcceptedDomainByDomainName(string domainName)
		{
			return base.InvokeGetObjectWithAPILogging<AcceptedDomain>(() => this.ExecuteSingleObjectQueryWithFallback<AcceptedDomain>((ITenantConfigurationSession session) => session.GetAcceptedDomainByDomainName(domainName), null, null), "GetAcceptedDomainByDomainName");
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x00113D2D File Offset: 0x00111F2D
		ADPagedReader<ManagementScope> IConfigurationSession.GetAllExclusiveScopes()
		{
			return base.InvokeWithAPILogging<ADPagedReader<ManagementScope>>(() => base.GetSession().GetAllExclusiveScopes(), "GetAllExclusiveScopes");
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x00113D6C File Offset: 0x00111F6C
		ADPagedReader<ManagementScope> IConfigurationSession.GetAllScopes(OrganizationId organizationId, ScopeRestrictionType restrictionType)
		{
			return base.InvokeWithAPILogging<ADPagedReader<ManagementScope>>(() => this.GetSession().GetAllScopes(organizationId, restrictionType), "GetAllScopes");
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x00113DCC File Offset: 0x00111FCC
		AvailabilityAddressSpace IConfigurationSession.GetAvailabilityAddressSpace(string domainName)
		{
			return base.InvokeGetObjectWithAPILogging<AvailabilityAddressSpace>(() => this.GetSession().GetAvailabilityAddressSpace(domainName), "GetAvailabilityAddressSpace");
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x00113E11 File Offset: 0x00112011
		AvailabilityConfig IConfigurationSession.GetAvailabilityConfig()
		{
			return base.InvokeGetObjectWithAPILogging<AvailabilityConfig>(() => base.GetSession().GetAvailabilityConfig(), "GetAvailabilityConfig");
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x00113E37 File Offset: 0x00112037
		AcceptedDomain IConfigurationSession.GetDefaultAcceptedDomain()
		{
			return base.InvokeGetObjectWithAPILogging<AcceptedDomain>(() => base.GetSession().GetDefaultAcceptedDomain(), "GetDefaultAcceptedDomain");
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x00113E5D File Offset: 0x0011205D
		ExchangeConfigurationContainer IConfigurationSession.GetExchangeConfigurationContainer()
		{
			return base.InvokeGetObjectWithAPILogging<ExchangeConfigurationContainer>(() => base.GetSession().GetExchangeConfigurationContainer(), "GetExchangeConfigurationContainer");
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x00113E83 File Offset: 0x00112083
		ExchangeConfigurationContainerWithAddressLists IConfigurationSession.GetExchangeConfigurationContainerWithAddressLists()
		{
			return base.InvokeGetObjectWithAPILogging<ExchangeConfigurationContainerWithAddressLists>(() => base.GetSession().GetExchangeConfigurationContainerWithAddressLists(), "GetExchangeConfigurationContainerWithAddressLists");
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x00113EA9 File Offset: 0x001120A9
		FederatedOrganizationId IConfigurationSession.GetFederatedOrganizationId()
		{
			return base.InvokeGetObjectWithAPILogging<FederatedOrganizationId>(() => base.GetSession().GetFederatedOrganizationId(), "GetFederatedOrganizationId");
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x00113EF4 File Offset: 0x001120F4
		FederatedOrganizationId IConfigurationSession.GetFederatedOrganizationId(OrganizationId organizationId)
		{
			return base.InvokeGetObjectWithAPILogging<FederatedOrganizationId>(() => this.ExecuteSingleObjectQueryWithFallback<FederatedOrganizationId>((ITenantConfigurationSession session) => session.GetFederatedOrganizationId(organizationId), null, null), "GetFederatedOrganizationId");
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x00113F4C File Offset: 0x0011214C
		FederatedOrganizationId IConfigurationSession.GetFederatedOrganizationIdByDomainName(string domainName)
		{
			return base.InvokeGetObjectWithAPILogging<FederatedOrganizationId>(() => this.GetSession().GetFederatedOrganizationIdByDomainName(domainName), "GetFederatedOrganizationIdByDomainName");
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x00113F91 File Offset: 0x00112191
		NspiRpcClientConnection IConfigurationSession.GetNspiRpcClientConnection()
		{
			return base.InvokeWithAPILogging<NspiRpcClientConnection>(() => base.GetSession().GetNspiRpcClientConnection(), "GetNspiRpcClientConnection");
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x00113FCC File Offset: 0x001121CC
		ThrottlingPolicy IConfigurationSession.GetOrganizationThrottlingPolicy(OrganizationId organizationId)
		{
			return base.InvokeGetObjectWithAPILogging<ThrottlingPolicy>(() => this.GetSession().GetOrganizationThrottlingPolicy(organizationId), "GetOrganizationThrottlingPolicy");
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x0011402C File Offset: 0x0011222C
		ThrottlingPolicy IConfigurationSession.GetOrganizationThrottlingPolicy(OrganizationId organizationId, bool logFailedLookup)
		{
			return base.InvokeGetObjectWithAPILogging<ThrottlingPolicy>(() => this.GetSession().GetOrganizationThrottlingPolicy(organizationId, logFailedLookup), "GetOrganizationThrottlingPolicy");
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x0011409A File Offset: 0x0011229A
		Organization IConfigurationSession.GetOrgContainer()
		{
			return base.InvokeGetObjectWithAPILogging<Organization>(() => base.ExecuteSingleObjectQueryWithFallback<Organization>((ITenantConfigurationSession session) => session.GetOrgContainer(), null, null), "GetOrgContainer");
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x001140D4 File Offset: 0x001122D4
		OrganizationRelationship IConfigurationSession.GetOrganizationRelationship(string domainName)
		{
			return base.InvokeGetObjectWithAPILogging<OrganizationRelationship>(() => this.GetSession().GetOrganizationRelationship(domainName), "GetOrganizationRelationship");
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x00114152 File Offset: 0x00112352
		ADObjectId IConfigurationSession.GetOrgContainerId()
		{
			return base.InvokeWithAPILogging<ADObjectId>(delegate
			{
				Organization organization = base.ExecuteSingleObjectQueryWithFallback<Organization>((ITenantConfigurationSession session) => session.GetOrgContainer(), null, null);
				if (organization != null)
				{
					return organization.Id;
				}
				return null;
			}, "GetOrgContainerId");
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x00114178 File Offset: 0x00112378
		RbacContainer IConfigurationSession.GetRbacContainer()
		{
			return base.InvokeGetObjectWithAPILogging<RbacContainer>(() => base.GetSession().GetRbacContainer(), "GetRbacContainer");
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x001141B4 File Offset: 0x001123B4
		bool IConfigurationSession.ManagementScopeIsInUse(ManagementScope managementScope)
		{
			return base.InvokeWithAPILogging<bool>(() => this.GetSession().ManagementScopeIsInUse(managementScope), "ManagementScopeIsInUse");
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x0011420C File Offset: 0x0011240C
		public TResult FindByExchangeObjectId<TResult>(Guid exchangeObjectId) where TResult : ADConfigurationObject, new()
		{
			return base.InvokeGetObjectWithAPILogging<TResult>(() => this.GetSession().FindByExchangeObjectId<TResult>(exchangeObjectId), "FindByExchangeObjectId");
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x00114278 File Offset: 0x00112478
		TResult IConfigurationSession.Read<TResult>(ADObjectId entryId)
		{
			return base.InvokeGetObjectWithAPILogging<TResult>(() => this.ExecuteSingleObjectQueryWithFallback<TResult>((ITenantConfigurationSession session) => session.Read<TResult>(entryId), null, null), "Read");
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x001142D0 File Offset: 0x001124D0
		Result<TResult>[] IConfigurationSession.ReadMultiple<TResult>(ADObjectId[] identities)
		{
			return base.InvokeWithAPILogging<Result<TResult>[]>(() => this.GetSession().ReadMultiple<TResult>(identities), "ReadMultiple");
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x00114328 File Offset: 0x00112528
		MultiValuedProperty<ReplicationCursor> IConfigurationSession.ReadReplicationCursors(ADObjectId id)
		{
			return base.InvokeWithAPILogging<MultiValuedProperty<ReplicationCursor>>(() => this.GetSession().ReadReplicationCursors(id), "ReadReplicationCursors");
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x00114360 File Offset: 0x00112560
		void IConfigurationSession.ReadReplicationData(ADObjectId id, out MultiValuedProperty<ReplicationCursor> replicationCursors, out MultiValuedProperty<ReplicationNeighbor> repsFrom)
		{
			base.GetSession().ReadReplicationData(id, out replicationCursors, out repsFrom);
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x0011438C File Offset: 0x0011258C
		void IConfigurationSession.Save(ADConfigurationObject instanceToSave)
		{
			base.InvokeWithAPILogging<bool>(delegate
			{
				this.InternalSave(instanceToSave);
				return true;
			}, "Save");
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x001143E8 File Offset: 0x001125E8
		AcceptedDomain[] ITenantConfigurationSession.FindAllAcceptedDomainsInOrg(ADObjectId organizationCU)
		{
			return base.InvokeWithAPILogging<AcceptedDomain[]>(() => this.GetSession().FindAllAcceptedDomainsInOrg(organizationCU), "FindAllAcceptedDomainsInOrg");
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x00114440 File Offset: 0x00112640
		ExchangeConfigurationUnit[] ITenantConfigurationSession.FindAllOpenConfigurationUnits(bool excludeFull)
		{
			return base.InvokeWithAPILogging<ExchangeConfigurationUnit[]>(() => this.GetSession().FindAllOpenConfigurationUnits(excludeFull), "FindAllOpenConfigurationUnits");
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x001144A0 File Offset: 0x001126A0
		ExchangeConfigurationUnit[] ITenantConfigurationSession.FindSharedConfiguration(SharedConfigurationInfo sharedConfigInfo, bool enabledSharedOrgOnly)
		{
			return base.InvokeWithAPILogging<ExchangeConfigurationUnit[]>(() => this.GetSession().FindSharedConfiguration(sharedConfigInfo, enabledSharedOrgOnly), "FindSharedConfiguration");
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x00114500 File Offset: 0x00112700
		ExchangeConfigurationUnit[] ITenantConfigurationSession.FindSharedConfigurationByOrganizationId(OrganizationId tinyTenantId)
		{
			return base.InvokeWithAPILogging<ExchangeConfigurationUnit[]>(() => this.GetSession().FindSharedConfigurationByOrganizationId(tinyTenantId), "FindSharedConfigurationByOrganizationId");
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x0011456C File Offset: 0x0011276C
		ADRawEntry[] ITenantConfigurationSession.FindDeletedADRawEntryByUsnRange(ADObjectId lastKnownParentId, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeWithAPILogging<ADRawEntry[]>(() => this.GetSession().FindDeletedADRawEntryByUsnRange(lastKnownParentId, startUsn, sizeLimit, properties), "FindDeletedADRawEntryByUsnRange");
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x001145EC File Offset: 0x001127EC
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByExternalId(string externalId)
		{
			return base.InvokeGetObjectWithAPILogging<ExchangeConfigurationUnit>(() => this.ExecuteSingleObjectQueryWithFallback<ExchangeConfigurationUnit>((ITenantConfigurationSession session) => session.GetExchangeConfigurationUnitByExternalId(externalId), null, null), "GetExchangeConfigurationUnitByExternalId");
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x00114658 File Offset: 0x00112858
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByExternalId(Guid externalDirectoryOrganizationId)
		{
			return base.InvokeGetObjectWithAPILogging<ExchangeConfigurationUnit>(() => this.ExecuteSingleObjectQueryWithFallback<ExchangeConfigurationUnit>((ITenantConfigurationSession session) => session.GetExchangeConfigurationUnitByExternalId(externalDirectoryOrganizationId), null, null), "GetExchangeConfigurationUnitByExternalId");
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x001146C4 File Offset: 0x001128C4
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByName(string organizationName)
		{
			return base.InvokeGetObjectWithAPILogging<ExchangeConfigurationUnit>(() => this.ExecuteSingleObjectQueryWithFallback<ExchangeConfigurationUnit>((ITenantConfigurationSession session) => session.GetExchangeConfigurationUnitByName(organizationName), null, null), "GetExchangeConfigurationUnitByName");
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x0011471C File Offset: 0x0011291C
		ADObjectId ITenantConfigurationSession.GetExchangeConfigurationUnitIdByName(string organizationName)
		{
			return base.InvokeWithAPILogging<ADObjectId>(() => this.GetSession().GetExchangeConfigurationUnitIdByName(organizationName), "GetExchangeConfigurationUnitIdByName");
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x001147DC File Offset: 0x001129DC
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByNameOrAcceptedDomain(string organizationName)
		{
			return base.InvokeGetObjectWithAPILogging<ExchangeConfigurationUnit>(() => this.ExecuteSingleObjectQueryWithFallback<ExchangeConfigurationUnit>((ITenantConfigurationSession session) => session.GetExchangeConfigurationUnitByNameOrAcceptedDomain(organizationName), delegate(ExchangeConfigurationUnit exocu)
			{
				if (!exocu.Id.Parent.Name.Equals(organizationName, StringComparison.OrdinalIgnoreCase))
				{
					return new List<Tuple<string, KeyType>>
					{
						new Tuple<string, KeyType>(organizationName, KeyType.DomainName)
					};
				}
				return null;
			}, null), "GetExchangeConfigurationUnitByNameOrAcceptedDomain");
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x00114848 File Offset: 0x00112A48
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByUserNetID(string userNetID)
		{
			return base.InvokeGetObjectWithAPILogging<ExchangeConfigurationUnit>(() => this.ExecuteSingleObjectQueryWithFallback<ExchangeConfigurationUnit>((ITenantConfigurationSession session) => session.GetExchangeConfigurationUnitByUserNetID(userNetID), null, null), "GetExchangeConfigurationUnitByUserNetID");
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x001148A0 File Offset: 0x00112AA0
		OrganizationId ITenantConfigurationSession.GetOrganizationIdFromOrgNameOrAcceptedDomain(string domainName)
		{
			return base.InvokeWithAPILogging<OrganizationId>(() => this.GetSession().GetOrganizationIdFromOrgNameOrAcceptedDomain(domainName), "GetOrganizationIdFromOrgNameOrAcceptedDomain");
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x001148F8 File Offset: 0x00112AF8
		OrganizationId ITenantConfigurationSession.GetOrganizationIdFromExternalDirectoryOrgId(Guid externalDirectoryOrgId)
		{
			return base.InvokeWithAPILogging<OrganizationId>(() => this.GetSession().GetOrganizationIdFromExternalDirectoryOrgId(externalDirectoryOrgId), "GetOrganizationIdFromExternalDirectoryOrgId");
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x00114950 File Offset: 0x00112B50
		MsoTenantCookieContainer ITenantConfigurationSession.GetMsoTenantCookieContainer(Guid contextId)
		{
			return base.InvokeWithAPILogging<MsoTenantCookieContainer>(() => this.GetSession().GetMsoTenantCookieContainer(contextId), "GetMsoTenantCookieContainer");
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x001149B0 File Offset: 0x00112BB0
		Result<ADRawEntry>[] ITenantConfigurationSession.ReadMultipleOrganizationProperties(ADObjectId[] organizationOUIds, PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<Result<ADRawEntry>[]>(() => this.GetSession().ReadMultipleOrganizationProperties(organizationOUIds, properties), "ReadMultipleOrganizationProperties");
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x001149FC File Offset: 0x00112BFC
		T ITenantConfigurationSession.GetDefaultFilteringConfiguration<T>()
		{
			return base.InvokeGetObjectWithAPILogging<T>(() => base.GetSession().GetDefaultFilteringConfiguration<T>(), "GetDefaultFilteringConfiguration");
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x00114A22 File Offset: 0x00112C22
		public bool IsTenantLockedOut()
		{
			return base.InvokeWithAPILogging<bool>(() => base.GetSession().IsTenantLockedOut(), "IsTenantLockedOut");
		}
	}
}
