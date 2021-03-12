using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000398 RID: 920
	internal interface ITopologyConfigurationSession : IConfigurationSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x060029F8 RID: 10744
		ADCrossRef[] FindADCrossRefByDomainId(ADObjectId domainNc);

		// Token: 0x060029F9 RID: 10745
		ADCrossRef[] FindADCrossRefByNetBiosName(string domain);

		// Token: 0x060029FA RID: 10746
		AccountPartition[] FindAllAccountPartitions();

		// Token: 0x060029FB RID: 10747
		ADSite[] FindAllADSites();

		// Token: 0x060029FC RID: 10748
		IList<PublicFolderDatabase> FindAllPublicFolderDatabaseOfCurrentVersion();

		// Token: 0x060029FD RID: 10749
		ADPagedReader<Server> FindAllServersWithExactVersionNumber(int versionNumber);

		// Token: 0x060029FE RID: 10750
		ADPagedReader<Server> FindAllServersWithVersionNumber(int versionNumber);

		// Token: 0x060029FF RID: 10751
		ADPagedReader<MiniServer> FindAllServersWithExactVersionNumber(int versionNumber, QueryFilter additionalFilter, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06002A00 RID: 10752
		ADPagedReader<MiniServer> FindAllServersWithVersionNumber(int versionNumber, QueryFilter additionalFilter, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06002A01 RID: 10753
		CmdletExtensionAgent[] FindCmdletExtensionAgents(bool enabledOnly, bool sortByPriority);

		// Token: 0x06002A02 RID: 10754
		ADComputer FindComputerByHostName(string hostName);

		// Token: 0x06002A03 RID: 10755
		ADComputer FindComputerByHostName(ADObjectId domainId, string hostName);

		// Token: 0x06002A04 RID: 10756
		ADComputer FindComputerBySid(SecurityIdentifier sid);

		// Token: 0x06002A05 RID: 10757
		TDatabase FindDatabaseByGuid<TDatabase>(Guid dbGuid) where TDatabase : Database, new();

		// Token: 0x06002A06 RID: 10758
		ADServer FindDCByFqdn(string dnsHostName);

		// Token: 0x06002A07 RID: 10759
		ADServer FindDCByInvocationId(Guid invocationId);

		// Token: 0x06002A08 RID: 10760
		UMDialPlan[] FindDialPlansForServer(Server server);

		// Token: 0x06002A09 RID: 10761
		ELCFolder FindElcFolderByName(string name);

		// Token: 0x06002A0A RID: 10762
		ADComputer FindLocalComputer();

		// Token: 0x06002A0B RID: 10763
		Server FindLocalServer();

		// Token: 0x06002A0C RID: 10764
		MailboxDatabase FindMailboxDatabaseByNameAndServer(string databaseName, Server server);

		// Token: 0x06002A0D RID: 10765
		MesoContainer FindMesoContainer(ADDomain dom);

		// Token: 0x06002A0E RID: 10766
		MiniClientAccessServerOrArray[] FindMiniClientAccessServerOrArray(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06002A0F RID: 10767
		MiniClientAccessServerOrArray FindMiniClientAccessServerOrArrayByFqdn(string serverFqdn, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06002A10 RID: 10768
		MiniServer[] FindMiniServer(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06002A11 RID: 10769
		TResult[] Find<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties) where TResult : ADObject, new();

		// Token: 0x06002A12 RID: 10770
		MiniServer FindMiniServerByFqdn(string serverFqdn, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06002A13 RID: 10771
		MiniServer FindMiniServerByName(string serverName, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06002A14 RID: 10772
		ADOabVirtualDirectory[] FindOABVirtualDirectoriesForLocalServer();

		// Token: 0x06002A15 RID: 10773
		ADOwaVirtualDirectory[] FindOWAVirtualDirectoriesForLocalServer();

		// Token: 0x06002A16 RID: 10774
		ADO365SuiteServiceVirtualDirectory[] FindO365SuiteServiceVirtualDirectoriesForLocalServer();

		// Token: 0x06002A17 RID: 10775
		ADSnackyServiceVirtualDirectory[] FindSnackyServiceVirtualDirectoriesForLocalServer();

		// Token: 0x06002A18 RID: 10776
		MiniVirtualDirectory[] FindMiniVirtualDirectories(ADObjectId serverId);

		// Token: 0x06002A19 RID: 10777
		ADPagedReader<MiniServer> FindPagedMiniServer(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06002A1A RID: 10778
		MiniServer FindMiniServerByFqdn(string serverFqdn);

		// Token: 0x06002A1B RID: 10779
		Server FindServerByFqdn(string serverFqdn);

		// Token: 0x06002A1C RID: 10780
		Server FindServerByLegacyDN(string legacyExchangeDN);

		// Token: 0x06002A1D RID: 10781
		Server FindServerByName(string serverName);

		// Token: 0x06002A1E RID: 10782
		ReadOnlyCollection<ADServer> FindServerWithNtdsdsa(string domainDN, bool gcOnly, bool includingRodc);

		// Token: 0x06002A1F RID: 10783
		TResult FindUnique<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter) where TResult : ADConfigurationObject, new();

		// Token: 0x06002A20 RID: 10784
		TPolicy[] FindWorkloadManagementChildPolicies<TPolicy>(ADObjectId wlmPolicy) where TPolicy : ADConfigurationObject, new();

		// Token: 0x06002A21 RID: 10785
		AdministrativeGroup GetAdministrativeGroup();

		// Token: 0x06002A22 RID: 10786
		ADObjectId GetAdministrativeGroupId();

		// Token: 0x06002A23 RID: 10787
		ADPagedReader<ExtendedRight> GetAllExtendedRights();

		// Token: 0x06002A24 RID: 10788
		ADObjectId GetAutoDiscoverGlobalContainerId();

		// Token: 0x06002A25 RID: 10789
		string[] GetAutodiscoverTrustedHosters();

		// Token: 0x06002A26 RID: 10790
		ADObjectId GetClientAccessContainerId();

		// Token: 0x06002A27 RID: 10791
		DatabaseAvailabilityGroupContainer GetDatabaseAvailabilityGroupContainer();

		// Token: 0x06002A28 RID: 10792
		ADObjectId GetDatabaseAvailabilityGroupContainerId();

		// Token: 0x06002A29 RID: 10793
		DatabasesContainer GetDatabasesContainer();

		// Token: 0x06002A2A RID: 10794
		ADObjectId GetDatabasesContainerId();

		// Token: 0x06002A2B RID: 10795
		ServiceEndpointContainer GetEndpointContainer();

		// Token: 0x06002A2C RID: 10796
		ThrottlingPolicy GetGlobalThrottlingPolicy();

		// Token: 0x06002A2D RID: 10797
		ThrottlingPolicy GetGlobalThrottlingPolicy(bool throwError);

		// Token: 0x06002A2E RID: 10798
		Guid GetInvocationIdByDC(ADServer dc);

		// Token: 0x06002A2F RID: 10799
		Guid GetInvocationIdByFqdn(string serverFqdn);

		// Token: 0x06002A30 RID: 10800
		ADSite GetLocalSite();

		// Token: 0x06002A31 RID: 10801
		MsoMainStreamCookieContainer GetMsoMainStreamCookieContainer(string serviceInstanceName);

		// Token: 0x06002A32 RID: 10802
		Server GetParentServer(ADObjectId entryId, ADObjectId originalId);

		// Token: 0x06002A33 RID: 10803
		ProvisioningReconciliationConfig GetProvisioningReconciliationConfig();

		// Token: 0x06002A34 RID: 10804
		string GetRootDomainNamingContextFromCurrentReadConnection();

		// Token: 0x06002A35 RID: 10805
		RootDse GetRootDse();

		// Token: 0x06002A36 RID: 10806
		RoutingGroup GetRoutingGroup();

		// Token: 0x06002A37 RID: 10807
		ADObjectId GetRoutingGroupId();

		// Token: 0x06002A38 RID: 10808
		string GetSchemaMasterDC();

		// Token: 0x06002A39 RID: 10809
		ServicesContainer GetServicesContainer();

		// Token: 0x06002A3A RID: 10810
		SitesContainer GetSitesContainer();

		// Token: 0x06002A3B RID: 10811
		StampGroupContainer GetStampGroupContainer();

		// Token: 0x06002A3C RID: 10812
		ADObjectId GetStampGroupContainerId();

		// Token: 0x06002A3D RID: 10813
		bool HasAnyServer();

		// Token: 0x06002A3E RID: 10814
		bool IsInE12InteropMode();

		// Token: 0x06002A3F RID: 10815
		bool IsInPreE12InteropMode();

		// Token: 0x06002A40 RID: 10816
		bool IsInPreE14InteropMode();

		// Token: 0x06002A41 RID: 10817
		Server ReadLocalServer();

		// Token: 0x06002A42 RID: 10818
		MiniClientAccessServerOrArray ReadMiniClientAccessServerOrArray(ADObjectId entryId, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06002A43 RID: 10819
		MiniServer ReadMiniServer(ADObjectId entryId, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06002A44 RID: 10820
		Result<TResult>[] ReadMultipleLegacyObjects<TResult>(string[] objectNames) where TResult : ADLegacyVersionableObject, new();

		// Token: 0x06002A45 RID: 10821
		Result<Server>[] ReadMultipleServers(string[] serverNames);

		// Token: 0x06002A46 RID: 10822
		ManagementScope ReadRootOrgManagementScopeByName(string scopeName);

		// Token: 0x06002A47 RID: 10823
		bool TryFindByExchangeLegacyDN(string legacyExchangeDN, IEnumerable<PropertyDefinition> properties, out MiniServer miniServer);

		// Token: 0x06002A48 RID: 10824
		bool TryFindByExchangeLegacyDN(string legacyExchangeDN, IEnumerable<PropertyDefinition> properties, out MiniClientAccessServerOrArray miniClientAccessServerOrArray);

		// Token: 0x06002A49 RID: 10825
		void UpdateGwartLastModified();

		// Token: 0x06002A4A RID: 10826
		bool TryGetDefaultAdQueryPolicy(out ADQueryPolicy queryPolicy);
	}
}
