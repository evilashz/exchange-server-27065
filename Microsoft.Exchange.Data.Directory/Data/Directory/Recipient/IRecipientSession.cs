using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000090 RID: 144
	internal interface IRecipientSession : IDirectorySession, IConfigDataProvider
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000756 RID: 1878
		ADObjectId SearchRoot { get; }

		// Token: 0x06000757 RID: 1879
		ITableView Browse(ADObjectId addressListId, int rowCountSuggestion, params PropertyDefinition[] properties);

		// Token: 0x06000758 RID: 1880
		void Delete(ADRecipient instanceToDelete);

		// Token: 0x06000759 RID: 1881
		ADRecipient[] Find(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults);

		// Token: 0x0600075A RID: 1882
		ADRawEntry FindADRawEntryBySid(SecurityIdentifier sId, IEnumerable<PropertyDefinition> properties);

		// Token: 0x0600075B RID: 1883
		ADRawEntry[] FindADRawEntryByUsnRange(ADObjectId root, long startUsn, long endUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties, QueryScope scope, QueryFilter additionalFilter);

		// Token: 0x0600075C RID: 1884
		Result<ADRecipient>[] FindADRecipientsByLegacyExchangeDNs(string[] legacyExchangeDNs);

		// Token: 0x0600075D RID: 1885
		ADUser[] FindADUser(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults);

		// Token: 0x0600075E RID: 1886
		ADUser FindADUserByObjectId(ADObjectId adObjectId);

		// Token: 0x0600075F RID: 1887
		ADUser FindADUserByExternalDirectoryObjectId(string externalDirectoryObjectId);

		// Token: 0x06000760 RID: 1888
		ADObject FindByAccountName<T>(string domainName, string accountName) where T : IConfigurable, new();

		// Token: 0x06000761 RID: 1889
		IEnumerable<T> FindByAccountName<T>(string domain, string account, ADObjectId rootId, QueryFilter searchFilter) where T : IConfigurable, new();

		// Token: 0x06000762 RID: 1890
		ADRecipient[] FindByANR(string anrMatch, int maxResults, SortBy sortBy);

		// Token: 0x06000763 RID: 1891
		ADRawEntry[] FindByANR(string anrMatch, int maxResults, SortBy sortBy, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06000764 RID: 1892
		ADRecipient FindByCertificate(X509Identifier identifier);

		// Token: 0x06000765 RID: 1893
		ADRawEntry[] FindByCertificate(X509Identifier identifier, params PropertyDefinition[] properties);

		// Token: 0x06000766 RID: 1894
		ADRawEntry FindByExchangeGuid(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06000767 RID: 1895
		TEntry FindByExchangeGuid<TEntry>(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties) where TEntry : MiniRecipient, new();

		// Token: 0x06000768 RID: 1896
		ADRecipient FindByExchangeObjectId(Guid exchangeObjectId);

		// Token: 0x06000769 RID: 1897
		ADRecipient FindByExchangeGuid(Guid exchangeGuid);

		// Token: 0x0600076A RID: 1898
		ADRecipient FindByExchangeGuidIncludingAlternate(Guid exchangeGuid);

		// Token: 0x0600076B RID: 1899
		ADRawEntry FindByExchangeGuidIncludingAlternate(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties);

		// Token: 0x0600076C RID: 1900
		TObject FindByExchangeGuidIncludingAlternate<TObject>(Guid exchangeGuid) where TObject : ADObject, new();

		// Token: 0x0600076D RID: 1901
		ADRecipient FindByExchangeGuidIncludingArchive(Guid exchangeGuid);

		// Token: 0x0600076E RID: 1902
		Result<ADRecipient>[] FindByExchangeGuidsIncludingArchive(Guid[] keys);

		// Token: 0x0600076F RID: 1903
		ADRecipient FindByLegacyExchangeDN(string legacyExchangeDN);

		// Token: 0x06000770 RID: 1904
		Result<ADRawEntry>[] FindByLegacyExchangeDNs(string[] legacyExchangeDNs, params PropertyDefinition[] properties);

		// Token: 0x06000771 RID: 1905
		ADRecipient FindByObjectGuid(Guid guid);

		// Token: 0x06000772 RID: 1906
		ADRecipient FindByProxyAddress(ProxyAddress proxyAddress);

		// Token: 0x06000773 RID: 1907
		ADRawEntry FindByProxyAddress(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06000774 RID: 1908
		TEntry FindByProxyAddress<TEntry>(ProxyAddress proxyAddress) where TEntry : ADObject, new();

		// Token: 0x06000775 RID: 1909
		Result<ADRawEntry>[] FindByProxyAddresses(ProxyAddress[] proxyAddresses, params PropertyDefinition[] properties);

		// Token: 0x06000776 RID: 1910
		Result<TEntry>[] FindByProxyAddresses<TEntry>(ProxyAddress[] proxyAddresses) where TEntry : ADObject, new();

		// Token: 0x06000777 RID: 1911
		Result<ADRecipient>[] FindByProxyAddresses(ProxyAddress[] proxyAddresses);

		// Token: 0x06000778 RID: 1912
		ADRecipient FindBySid(SecurityIdentifier sId);

		// Token: 0x06000779 RID: 1913
		ADRawEntry[] FindDeletedADRawEntryByUsnRange(ADObjectId lastKnownParentId, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties, QueryFilter additionalFilter);

		// Token: 0x0600077A RID: 1914
		MiniRecipient[] FindMiniRecipient(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties);

		// Token: 0x0600077B RID: 1915
		MiniRecipient[] FindMiniRecipientByANR(string anrMatch, int maxResults, SortBy sortBy, IEnumerable<PropertyDefinition> properties);

		// Token: 0x0600077C RID: 1916
		TResult FindMiniRecipientByProxyAddress<TResult>(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties) where TResult : MiniRecipient, new();

		// Token: 0x0600077D RID: 1917
		TResult FindMiniRecipientBySid<TResult>(SecurityIdentifier sId, IEnumerable<PropertyDefinition> properties) where TResult : MiniRecipient, new();

		// Token: 0x0600077E RID: 1918
		ADRecipient[] FindNames(IDictionary<PropertyDefinition, object> dictionary, int limit);

		// Token: 0x0600077F RID: 1919
		object[][] FindNamesView(IDictionary<PropertyDefinition, object> dictionary, int limit, params PropertyDefinition[] properties);

		// Token: 0x06000780 RID: 1920
		Result<OWAMiniRecipient>[] FindOWAMiniRecipientByUserPrincipalName(string[] userPrincipalNames);

		// Token: 0x06000781 RID: 1921
		ADPagedReader<ADRecipient> FindPaged(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize);

		// Token: 0x06000782 RID: 1922
		ADPagedReader<TEntry> FindPagedMiniRecipient<TEntry>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties) where TEntry : MiniRecipient, new();

		// Token: 0x06000783 RID: 1923
		ADRawEntry[] FindRecipient(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06000784 RID: 1924
		IEnumerable<ADGroup> FindRoleGroupsByForeignGroupSid(ADObjectId root, SecurityIdentifier sId);

		// Token: 0x06000785 RID: 1925
		List<string> GetTokenSids(ADRawEntry user, AssignmentMethod assignmentMethodFlags);

		// Token: 0x06000786 RID: 1926
		List<string> GetTokenSids(ADObjectId userId, AssignmentMethod assignmentMethodFlags);

		// Token: 0x06000787 RID: 1927
		SecurityIdentifier GetWellKnownExchangeGroupSid(Guid wkguid);

		// Token: 0x06000788 RID: 1928
		bool IsLegacyDNInUse(string legacyDN);

		// Token: 0x06000789 RID: 1929
		bool IsMemberOfGroupByWellKnownGuid(Guid wellKnownGuid, string containerDN, ADObjectId id);

		// Token: 0x0600078A RID: 1930
		bool IsRecipientInOrg(ProxyAddress proxyAddress);

		// Token: 0x0600078B RID: 1931
		bool IsReducedRecipientSession();

		// Token: 0x0600078C RID: 1932
		bool IsThrottlingPolicyInUse(ADObjectId throttlingPolicyId);

		// Token: 0x0600078D RID: 1933
		ADRecipient Read(ADObjectId entryId);

		// Token: 0x0600078E RID: 1934
		MiniRecipient ReadMiniRecipient(ADObjectId entryId, IEnumerable<PropertyDefinition> properties);

		// Token: 0x0600078F RID: 1935
		TMiniRecipient ReadMiniRecipient<TMiniRecipient>(ADObjectId entryId, IEnumerable<PropertyDefinition> properties) where TMiniRecipient : ADObject, new();

		// Token: 0x06000790 RID: 1936
		ADRawEntry FindUserBySid(SecurityIdentifier sId, IList<PropertyDefinition> properties);

		// Token: 0x06000791 RID: 1937
		Result<ADRecipient>[] ReadMultiple(ADObjectId[] entryIds);

		// Token: 0x06000792 RID: 1938
		Result<ADRawEntry>[] ReadMultiple(ADObjectId[] entryIds, params PropertyDefinition[] properties);

		// Token: 0x06000793 RID: 1939
		Result<ADGroup>[] ReadMultipleADGroups(ADObjectId[] entryIds);

		// Token: 0x06000794 RID: 1940
		Result<ADUser>[] ReadMultipleADUsers(ADObjectId[] userIds);

		// Token: 0x06000795 RID: 1941
		Result<ADRawEntry>[] ReadMultipleWithDeletedObjects(ADObjectId[] entryIds, params PropertyDefinition[] properties);

		// Token: 0x06000796 RID: 1942
		MiniRecipientWithTokenGroups ReadTokenGroupsGlobalAndUniversal(ADObjectId id);

		// Token: 0x06000797 RID: 1943
		ADObjectId[] ResolveSidsToADObjectIds(string[] sids);

		// Token: 0x06000798 RID: 1944
		void Save(ADRecipient instanceToSave);

		// Token: 0x06000799 RID: 1945
		void Save(ADRecipient instanceToSave, bool bypassValidation);

		// Token: 0x0600079A RID: 1946
		void SetPassword(ADObject obj, SecureString password);

		// Token: 0x0600079B RID: 1947
		void SetPassword(ADObjectId id, SecureString password);
	}
}
