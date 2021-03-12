using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000E0 RID: 224
	[Serializable]
	internal class FfoTopologyConfigurationSession : FfoConfigurationSession, ITopologyConfigurationSession, IConfigurationSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x0001B2C6 File Offset: 0x000194C6
		public FfoTopologyConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings) : base(true, true, consistencyMode, null, sessionSettings)
		{
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001B2D3 File Offset: 0x000194D3
		public FfoTopologyConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings) : base(true, readOnly, consistencyMode, null, sessionSettings)
		{
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001B2E0 File Offset: 0x000194E0
		public FfoTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings) : base(true, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
			base.DomainController = domainController;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001B2F6 File Offset: 0x000194F6
		public FfoTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope) : this(domainController, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
			base.ConfigScope = configScope;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001B30D File Offset: 0x0001950D
		bool ITopologyConfigurationSession.TryGetDefaultAdQueryPolicy(out ADQueryPolicy queryPolicy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001B314 File Offset: 0x00019514
		ADCrossRef[] ITopologyConfigurationSession.FindADCrossRefByDomainId(ADObjectId domainNc)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001B31B File Offset: 0x0001951B
		ADCrossRef[] ITopologyConfigurationSession.FindADCrossRefByNetBiosName(string domain)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001B322 File Offset: 0x00019522
		AccountPartition[] ITopologyConfigurationSession.FindAllAccountPartitions()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001B329 File Offset: 0x00019529
		ADSite[] ITopologyConfigurationSession.FindAllADSites()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001B330 File Offset: 0x00019530
		IList<PublicFolderDatabase> ITopologyConfigurationSession.FindAllPublicFolderDatabaseOfCurrentVersion()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001B337 File Offset: 0x00019537
		ADPagedReader<Server> ITopologyConfigurationSession.FindAllServersWithExactVersionNumber(int versionNumber)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001B33E File Offset: 0x0001953E
		ADPagedReader<Server> ITopologyConfigurationSession.FindAllServersWithVersionNumber(int versionNumber)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001B345 File Offset: 0x00019545
		ADPagedReader<MiniServer> ITopologyConfigurationSession.FindAllServersWithExactVersionNumber(int versionNumber, QueryFilter additionalFilter, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001B34C File Offset: 0x0001954C
		ADPagedReader<MiniServer> ITopologyConfigurationSession.FindAllServersWithVersionNumber(int versionNumber, QueryFilter additionalFilter, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001B353 File Offset: 0x00019553
		CmdletExtensionAgent[] ITopologyConfigurationSession.FindCmdletExtensionAgents(bool enabledOnly, bool sortByPriority)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001B35A File Offset: 0x0001955A
		ADComputer ITopologyConfigurationSession.FindComputerByHostName(string hostName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001B361 File Offset: 0x00019561
		ADComputer ITopologyConfigurationSession.FindComputerByHostName(ADObjectId domainId, string hostName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001B368 File Offset: 0x00019568
		ADComputer ITopologyConfigurationSession.FindComputerBySid(SecurityIdentifier sid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001B36F File Offset: 0x0001956F
		TDatabase ITopologyConfigurationSession.FindDatabaseByGuid<TDatabase>(Guid dbGuid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001B376 File Offset: 0x00019576
		ADServer ITopologyConfigurationSession.FindDCByFqdn(string dnsHostName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001B37D File Offset: 0x0001957D
		ADServer ITopologyConfigurationSession.FindDCByInvocationId(Guid invocationId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001B384 File Offset: 0x00019584
		UMDialPlan[] ITopologyConfigurationSession.FindDialPlansForServer(Server server)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001B38B File Offset: 0x0001958B
		ELCFolder ITopologyConfigurationSession.FindElcFolderByName(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001B392 File Offset: 0x00019592
		ADComputer ITopologyConfigurationSession.FindLocalComputer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001B399 File Offset: 0x00019599
		Server ITopologyConfigurationSession.FindLocalServer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001B3A0 File Offset: 0x000195A0
		MailboxDatabase ITopologyConfigurationSession.FindMailboxDatabaseByNameAndServer(string databaseName, Server server)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001B3A7 File Offset: 0x000195A7
		MesoContainer ITopologyConfigurationSession.FindMesoContainer(ADDomain dom)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001B3AE File Offset: 0x000195AE
		MiniClientAccessServerOrArray[] ITopologyConfigurationSession.FindMiniClientAccessServerOrArray(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001B3B5 File Offset: 0x000195B5
		MiniClientAccessServerOrArray ITopologyConfigurationSession.FindMiniClientAccessServerOrArrayByFqdn(string serverFqdn, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001B3BC File Offset: 0x000195BC
		MiniServer[] ITopologyConfigurationSession.FindMiniServer(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001B3C3 File Offset: 0x000195C3
		MiniServer ITopologyConfigurationSession.FindMiniServerByFqdn(string serverFqdn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001B3CA File Offset: 0x000195CA
		MiniServer ITopologyConfigurationSession.FindMiniServerByFqdn(string serverFqdn, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001B3D1 File Offset: 0x000195D1
		MiniServer ITopologyConfigurationSession.FindMiniServerByName(string serverName, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001B3D8 File Offset: 0x000195D8
		ADOabVirtualDirectory[] ITopologyConfigurationSession.FindOABVirtualDirectoriesForLocalServer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001B3DF File Offset: 0x000195DF
		ADOwaVirtualDirectory[] ITopologyConfigurationSession.FindOWAVirtualDirectoriesForLocalServer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001B3E6 File Offset: 0x000195E6
		ADSnackyServiceVirtualDirectory[] ITopologyConfigurationSession.FindSnackyServiceVirtualDirectoriesForLocalServer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001B3ED File Offset: 0x000195ED
		ADO365SuiteServiceVirtualDirectory[] ITopologyConfigurationSession.FindO365SuiteServiceVirtualDirectoriesForLocalServer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001B3F4 File Offset: 0x000195F4
		public MiniVirtualDirectory[] FindMiniVirtualDirectories(ADObjectId serverId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001B3FB File Offset: 0x000195FB
		ADPagedReader<MiniServer> ITopologyConfigurationSession.FindPagedMiniServer(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001B402 File Offset: 0x00019602
		public TResult[] Find<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties) where TResult : ADObject, new()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001B409 File Offset: 0x00019609
		Server ITopologyConfigurationSession.FindServerByFqdn(string serverFqdn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001B410 File Offset: 0x00019610
		Server ITopologyConfigurationSession.FindServerByLegacyDN(string legacyExchangeDN)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001B417 File Offset: 0x00019617
		Server ITopologyConfigurationSession.FindServerByName(string serverName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001B41E File Offset: 0x0001961E
		ReadOnlyCollection<ADServer> ITopologyConfigurationSession.FindServerWithNtdsdsa(string domainDN, bool gcOnly, bool includingRodc)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001B425 File Offset: 0x00019625
		TResult ITopologyConfigurationSession.FindUnique<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001B42C File Offset: 0x0001962C
		TPolicy[] ITopologyConfigurationSession.FindWorkloadManagementChildPolicies<TPolicy>(ADObjectId wlmPolicy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001B433 File Offset: 0x00019633
		AdministrativeGroup ITopologyConfigurationSession.GetAdministrativeGroup()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001B43A File Offset: 0x0001963A
		ADObjectId ITopologyConfigurationSession.GetAdministrativeGroupId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001B441 File Offset: 0x00019641
		ADPagedReader<ExtendedRight> ITopologyConfigurationSession.GetAllExtendedRights()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001B448 File Offset: 0x00019648
		ADObjectId ITopologyConfigurationSession.GetAutoDiscoverGlobalContainerId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001B44F File Offset: 0x0001964F
		string[] ITopologyConfigurationSession.GetAutodiscoverTrustedHosters()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001B456 File Offset: 0x00019656
		ADObjectId ITopologyConfigurationSession.GetClientAccessContainerId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001B45D File Offset: 0x0001965D
		DatabaseAvailabilityGroupContainer ITopologyConfigurationSession.GetDatabaseAvailabilityGroupContainer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001B464 File Offset: 0x00019664
		ADObjectId ITopologyConfigurationSession.GetDatabaseAvailabilityGroupContainerId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001B46B File Offset: 0x0001966B
		DatabasesContainer ITopologyConfigurationSession.GetDatabasesContainer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001B472 File Offset: 0x00019672
		ADObjectId ITopologyConfigurationSession.GetDatabasesContainerId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001B479 File Offset: 0x00019679
		ServiceEndpointContainer ITopologyConfigurationSession.GetEndpointContainer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001B480 File Offset: 0x00019680
		ThrottlingPolicy ITopologyConfigurationSession.GetGlobalThrottlingPolicy()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001B487 File Offset: 0x00019687
		ThrottlingPolicy ITopologyConfigurationSession.GetGlobalThrottlingPolicy(bool throwError)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001B48E File Offset: 0x0001968E
		Guid ITopologyConfigurationSession.GetInvocationIdByDC(ADServer dc)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001B495 File Offset: 0x00019695
		Guid ITopologyConfigurationSession.GetInvocationIdByFqdn(string serverFqdn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001B49C File Offset: 0x0001969C
		ADSite ITopologyConfigurationSession.GetLocalSite()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001B4A3 File Offset: 0x000196A3
		MsoMainStreamCookieContainer ITopologyConfigurationSession.GetMsoMainStreamCookieContainer(string serviceInstanceName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0001B4AA File Offset: 0x000196AA
		Server ITopologyConfigurationSession.GetParentServer(ADObjectId entryId, ADObjectId originalId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001B4B1 File Offset: 0x000196B1
		ProvisioningReconciliationConfig ITopologyConfigurationSession.GetProvisioningReconciliationConfig()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001B4B8 File Offset: 0x000196B8
		string ITopologyConfigurationSession.GetRootDomainNamingContextFromCurrentReadConnection()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001B4BF File Offset: 0x000196BF
		RootDse ITopologyConfigurationSession.GetRootDse()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001B4C6 File Offset: 0x000196C6
		RoutingGroup ITopologyConfigurationSession.GetRoutingGroup()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001B4CD File Offset: 0x000196CD
		ADObjectId ITopologyConfigurationSession.GetRoutingGroupId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001B4D4 File Offset: 0x000196D4
		string ITopologyConfigurationSession.GetSchemaMasterDC()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001B4DB File Offset: 0x000196DB
		ServicesContainer ITopologyConfigurationSession.GetServicesContainer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001B4E2 File Offset: 0x000196E2
		SitesContainer ITopologyConfigurationSession.GetSitesContainer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001B4E9 File Offset: 0x000196E9
		StampGroupContainer ITopologyConfigurationSession.GetStampGroupContainer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001B4F0 File Offset: 0x000196F0
		ADObjectId ITopologyConfigurationSession.GetStampGroupContainerId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001B4F7 File Offset: 0x000196F7
		bool ITopologyConfigurationSession.HasAnyServer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001B4FE File Offset: 0x000196FE
		bool ITopologyConfigurationSession.IsInE12InteropMode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001B505 File Offset: 0x00019705
		bool ITopologyConfigurationSession.IsInPreE12InteropMode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001B50C File Offset: 0x0001970C
		bool ITopologyConfigurationSession.IsInPreE14InteropMode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001B513 File Offset: 0x00019713
		Server ITopologyConfigurationSession.ReadLocalServer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001B51A File Offset: 0x0001971A
		MiniClientAccessServerOrArray ITopologyConfigurationSession.ReadMiniClientAccessServerOrArray(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001B521 File Offset: 0x00019721
		MiniServer ITopologyConfigurationSession.ReadMiniServer(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001B528 File Offset: 0x00019728
		Result<TResult>[] ITopologyConfigurationSession.ReadMultipleLegacyObjects<TResult>(string[] objectNames)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001B52F File Offset: 0x0001972F
		Result<Server>[] ITopologyConfigurationSession.ReadMultipleServers(string[] serverNames)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001B536 File Offset: 0x00019736
		ManagementScope ITopologyConfigurationSession.ReadRootOrgManagementScopeByName(string scopeName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001B53D File Offset: 0x0001973D
		bool ITopologyConfigurationSession.TryFindByExchangeLegacyDN(string legacyExchangeDN, IEnumerable<PropertyDefinition> properties, out MiniServer miniServer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001B544 File Offset: 0x00019744
		bool ITopologyConfigurationSession.TryFindByExchangeLegacyDN(string legacyExchangeDN, IEnumerable<PropertyDefinition> properties, out MiniClientAccessServerOrArray miniClientAccessServerOrArray)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001B54B File Offset: 0x0001974B
		void ITopologyConfigurationSession.UpdateGwartLastModified()
		{
			throw new NotImplementedException();
		}
	}
}
