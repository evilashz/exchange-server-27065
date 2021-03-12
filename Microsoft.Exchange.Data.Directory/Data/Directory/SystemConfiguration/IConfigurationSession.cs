using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200008E RID: 142
	internal interface IConfigurationSession : IDirectorySession, IConfigDataProvider
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000718 RID: 1816
		ADObjectId ConfigurationNamingContext { get; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000719 RID: 1817
		ADObjectId DeletedObjectsContainer { get; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600071A RID: 1818
		ADObjectId SchemaNamingContext { get; }

		// Token: 0x0600071B RID: 1819
		bool CheckForRetentionPolicyWithConflictingRetentionId(Guid retentionId, out string duplicateName);

		// Token: 0x0600071C RID: 1820
		bool CheckForRetentionPolicyWithConflictingRetentionId(Guid retentionId, string identity, out string duplicateName);

		// Token: 0x0600071D RID: 1821
		bool CheckForRetentionTagWithConflictingRetentionId(Guid retentionId, out string duplicateName);

		// Token: 0x0600071E RID: 1822
		bool CheckForRetentionTagWithConflictingRetentionId(Guid retentionId, string identity, out string duplicateName);

		// Token: 0x0600071F RID: 1823
		void DeleteTree(ADConfigurationObject instanceToDelete, TreeDeleteNotFinishedHandler handler);

		// Token: 0x06000720 RID: 1824
		AcceptedDomain[] FindAcceptedDomainsByFederatedOrgId(FederatedOrganizationId federatedOrganizationId);

		// Token: 0x06000721 RID: 1825
		ADPagedReader<TResult> FindAllPaged<TResult>() where TResult : ADConfigurationObject, new();

		// Token: 0x06000722 RID: 1826
		ExchangeRoleAssignment[] FindAssignmentsForManagementScope(ManagementScope managementScope, bool returnAll);

		// Token: 0x06000723 RID: 1827
		T FindMailboxPolicyByName<T>(string name) where T : MailboxPolicy, new();

		// Token: 0x06000724 RID: 1828
		MicrosoftExchangeRecipient FindMicrosoftExchangeRecipient();

		// Token: 0x06000725 RID: 1829
		OfflineAddressBook[] FindOABsForWebDistributionPoint(ADOabVirtualDirectory vDir);

		// Token: 0x06000726 RID: 1830
		ThrottlingPolicy[] FindOrganizationThrottlingPolicies(OrganizationId organizationId);

		// Token: 0x06000727 RID: 1831
		ADPagedReader<TResult> FindPaged<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize) where TResult : ADConfigurationObject, new();

		// Token: 0x06000728 RID: 1832
		Result<ExchangeRoleAssignment>[] FindRoleAssignmentsByUserIds(ADObjectId[] securityPrincipalIds, bool partnerMode);

		// Token: 0x06000729 RID: 1833
		Result<ExchangeRoleAssignment>[] FindRoleAssignmentsByUserIds(ADObjectId[] securityPrincipalIds, QueryFilter additionalFilter);

		// Token: 0x0600072A RID: 1834
		ManagementScope[] FindSimilarManagementScope(ManagementScope managementScope);

		// Token: 0x0600072B RID: 1835
		T FindSingletonConfigurationObject<T>() where T : ADConfigurationObject, new();

		// Token: 0x0600072C RID: 1836
		AcceptedDomain GetAcceptedDomainByDomainName(string domainName);

		// Token: 0x0600072D RID: 1837
		ADPagedReader<ManagementScope> GetAllExclusiveScopes();

		// Token: 0x0600072E RID: 1838
		ADPagedReader<ManagementScope> GetAllScopes(OrganizationId organizationId, ScopeRestrictionType restrictionType);

		// Token: 0x0600072F RID: 1839
		AvailabilityAddressSpace GetAvailabilityAddressSpace(string domainName);

		// Token: 0x06000730 RID: 1840
		AvailabilityConfig GetAvailabilityConfig();

		// Token: 0x06000731 RID: 1841
		AcceptedDomain GetDefaultAcceptedDomain();

		// Token: 0x06000732 RID: 1842
		ExchangeConfigurationContainer GetExchangeConfigurationContainer();

		// Token: 0x06000733 RID: 1843
		ExchangeConfigurationContainerWithAddressLists GetExchangeConfigurationContainerWithAddressLists();

		// Token: 0x06000734 RID: 1844
		FederatedOrganizationId GetFederatedOrganizationId();

		// Token: 0x06000735 RID: 1845
		FederatedOrganizationId GetFederatedOrganizationId(OrganizationId organizationId);

		// Token: 0x06000736 RID: 1846
		FederatedOrganizationId GetFederatedOrganizationIdByDomainName(string domainName);

		// Token: 0x06000737 RID: 1847
		NspiRpcClientConnection GetNspiRpcClientConnection();

		// Token: 0x06000738 RID: 1848
		ThrottlingPolicy GetOrganizationThrottlingPolicy(OrganizationId organizationId);

		// Token: 0x06000739 RID: 1849
		ThrottlingPolicy GetOrganizationThrottlingPolicy(OrganizationId organizationId, bool logFailedLookup);

		// Token: 0x0600073A RID: 1850
		Organization GetOrgContainer();

		// Token: 0x0600073B RID: 1851
		OrganizationRelationship GetOrganizationRelationship(string domainName);

		// Token: 0x0600073C RID: 1852
		ADObjectId GetOrgContainerId();

		// Token: 0x0600073D RID: 1853
		RbacContainer GetRbacContainer();

		// Token: 0x0600073E RID: 1854
		bool ManagementScopeIsInUse(ManagementScope managementScope);

		// Token: 0x0600073F RID: 1855
		TResult FindByExchangeObjectId<TResult>(Guid exchangeObjectId) where TResult : ADConfigurationObject, new();

		// Token: 0x06000740 RID: 1856
		TResult Read<TResult>(ADObjectId entryId) where TResult : ADConfigurationObject, new();

		// Token: 0x06000741 RID: 1857
		Result<TResult>[] ReadMultiple<TResult>(ADObjectId[] identities) where TResult : ADConfigurationObject, new();

		// Token: 0x06000742 RID: 1858
		MultiValuedProperty<ReplicationCursor> ReadReplicationCursors(ADObjectId id);

		// Token: 0x06000743 RID: 1859
		void ReadReplicationData(ADObjectId id, out MultiValuedProperty<ReplicationCursor> replicationCursors, out MultiValuedProperty<ReplicationNeighbor> repsFrom);

		// Token: 0x06000744 RID: 1860
		void Save(ADConfigurationObject instanceToSave);
	}
}
