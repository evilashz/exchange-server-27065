using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001D8 RID: 472
	internal class CompositeRecipientSession : CompositeDirectorySession<IRecipientSession>, IRecipientSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0005B95E File Offset: 0x00059B5E
		protected override string Implementor
		{
			get
			{
				return "CompositeRecipientSession";
			}
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0005B965 File Offset: 0x00059B65
		internal CompositeRecipientSession(IRecipientSession cacheSession, IRecipientSession directorySession, bool cacheSessionForDeletingOnly = false) : base(cacheSession, directorySession, cacheSessionForDeletingOnly)
		{
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0005B970 File Offset: 0x00059B70
		ADObjectId IRecipientSession.SearchRoot
		{
			get
			{
				return base.GetSession().SearchRoot;
			}
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0005B9AC File Offset: 0x00059BAC
		ITableView IRecipientSession.Browse(ADObjectId addressListId, int rowCountSuggestion, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<ITableView>(() => this.GetSession().Browse(addressListId, rowCountSuggestion, properties), "Browse");
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0005BA10 File Offset: 0x00059C10
		void IRecipientSession.Delete(ADRecipient instanceToDelete)
		{
			base.InvokeWithAPILogging<bool>(delegate
			{
				this.InternalDelete(instanceToDelete);
				return true;
			}, "Delete");
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0005BA84 File Offset: 0x00059C84
		ADRecipient[] IRecipientSession.Find(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			return base.InvokeWithAPILogging<ADRecipient[]>(() => this.GetSession().Find(rootId, scope, filter, sortBy, maxResults), "Find");
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0005BB6C File Offset: 0x00059D6C
		ADRawEntry IRecipientSession.FindADRawEntryBySid(SecurityIdentifier sId, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<ADRawEntry>(delegate
			{
				if (properties == null)
				{
					return null;
				}
				PropertyDefinition[] second = new PropertyDefinition[]
				{
					IADSecurityPrincipalSchema.Sid
				};
				IEnumerable<PropertyDefinition> propertiesToRead = properties.Concat(second);
				return this.ExecuteSingleObjectQueryWithFallback<ADRawEntry>((IRecipientSession session) => session.FindADRawEntryBySid(sId, propertiesToRead), null, propertiesToRead);
			}, "FindADRawEntryBySid");
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0005BBF0 File Offset: 0x00059DF0
		ADRawEntry[] IRecipientSession.FindADRawEntryByUsnRange(ADObjectId root, long startUsn, long endUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties, QueryScope scope, QueryFilter additionalFilter)
		{
			return base.InvokeWithAPILogging<ADRawEntry[]>(() => this.GetSession().FindADRawEntryByUsnRange(root, startUsn, endUsn, sizeLimit, properties, scope, additionalFilter), "FindADRawEntryByUsnRange");
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0005BC78 File Offset: 0x00059E78
		Result<ADRecipient>[] IRecipientSession.FindADRecipientsByLegacyExchangeDNs(string[] legacyExchangeDNs)
		{
			return base.InvokeWithAPILogging<Result<ADRecipient>[]>(() => this.GetSession().FindADRecipientsByLegacyExchangeDNs(legacyExchangeDNs), "FindADRecipientsByLegacyExchangeDNs");
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0005BCE8 File Offset: 0x00059EE8
		ADUser[] IRecipientSession.FindADUser(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			return base.InvokeWithAPILogging<ADUser[]>(() => this.GetSession().FindADUser(rootId, scope, filter, sortBy, maxResults), "FindADUser");
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0005BD70 File Offset: 0x00059F70
		ADUser IRecipientSession.FindADUserByObjectId(ADObjectId adObjectId)
		{
			return base.InvokeGetObjectWithAPILogging<ADUser>(() => this.ExecuteSingleObjectQueryWithFallback<ADUser>((IRecipientSession session) => session.FindADUserByObjectId(adObjectId), null, null), "FindADUserByObjectId");
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0005BDC8 File Offset: 0x00059FC8
		ADUser IRecipientSession.FindADUserByExternalDirectoryObjectId(string externalDirectoryObjectId)
		{
			return base.InvokeGetObjectWithAPILogging<ADUser>(() => this.GetSession().FindADUserByExternalDirectoryObjectId(externalDirectoryObjectId), "FindADUserByExternalDirectoryObjectId");
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0005BE28 File Offset: 0x0005A028
		ADObject IRecipientSession.FindByAccountName<T>(string domainName, string accountName)
		{
			return base.InvokeGetObjectWithAPILogging<ADObject>(() => this.GetSession().FindByAccountName<T>(domainName, accountName), "FindByAccountName");
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0005BE9C File Offset: 0x0005A09C
		IEnumerable<T> IRecipientSession.FindByAccountName<T>(string domain, string account, ADObjectId rootId, QueryFilter searchFilter)
		{
			return base.InvokeWithAPILogging<IEnumerable<T>>(() => this.GetSession().FindByAccountName<T>(domain, account, rootId, searchFilter), "FindByAccountName");
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0005BF18 File Offset: 0x0005A118
		ADRecipient[] IRecipientSession.FindByANR(string anrMatch, int maxResults, SortBy sortBy)
		{
			return base.InvokeWithAPILogging<ADRecipient[]>(() => this.GetSession().FindByANR(anrMatch, maxResults, sortBy), "FindByANR");
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0005BF90 File Offset: 0x0005A190
		ADRawEntry[] IRecipientSession.FindByANR(string anrMatch, int maxResults, SortBy sortBy, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeWithAPILogging<ADRawEntry[]>(() => this.GetSession().FindByANR(anrMatch, maxResults, sortBy, properties), "FindByANR");
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0005C000 File Offset: 0x0005A200
		ADRecipient IRecipientSession.FindByCertificate(X509Identifier identifier)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.GetSession().FindByCertificate(identifier), "FindByCertificate");
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0005C060 File Offset: 0x0005A260
		ADRawEntry[] IRecipientSession.FindByCertificate(X509Identifier identifier, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<ADRawEntry[]>(() => this.GetSession().FindByCertificate(identifier, properties), "FindByCertificate");
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0005C130 File Offset: 0x0005A330
		ADRawEntry IRecipientSession.FindByExchangeGuid(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<ADRawEntry>(delegate
			{
				if (properties == null)
				{
					return null;
				}
				PropertyDefinition[] second = new PropertyDefinition[]
				{
					ADMailboxRecipientSchema.ExchangeGuid
				};
				IEnumerable<PropertyDefinition> propertiesToRead = properties.Concat(second);
				return this.ExecuteSingleObjectQueryWithFallback<ADRawEntry>((IRecipientSession session) => session.FindByExchangeGuid(exchangeGuid, propertiesToRead), null, propertiesToRead);
			}, "FindByExchangeGuid");
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0005C198 File Offset: 0x0005A398
		TEntry IRecipientSession.FindByExchangeGuid<TEntry>(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<TEntry>(() => this.GetSession().FindByExchangeGuid<TEntry>(exchangeGuid, properties), "FindByExchangeGuid");
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0005C1F8 File Offset: 0x0005A3F8
		ADRecipient IRecipientSession.FindByExchangeObjectId(Guid exchangeObjectId)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.GetSession().FindByExchangeObjectId(exchangeObjectId), "FindByExchangeObjectId");
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0005C264 File Offset: 0x0005A464
		ADRecipient IRecipientSession.FindByExchangeGuid(Guid exchangeGuid)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.ExecuteSingleObjectQueryWithFallback<ADRecipient>((IRecipientSession session) => session.FindByExchangeGuid(exchangeGuid), null, null), "FindByExchangeGuid");
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0005C2D0 File Offset: 0x0005A4D0
		ADRecipient IRecipientSession.FindByExchangeGuidIncludingAlternate(Guid exchangeGuid)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.ExecuteSingleObjectQueryWithFallback<ADRecipient>((IRecipientSession session) => session.FindByExchangeGuidIncludingAlternate(exchangeGuid), null, null), "FindByExchangeGuidIncludingAlternate");
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0005C39C File Offset: 0x0005A59C
		ADRawEntry IRecipientSession.FindByExchangeGuidIncludingAlternate(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<ADRawEntry>(delegate
			{
				if (properties == null)
				{
					return null;
				}
				PropertyDefinition[] second = new PropertyDefinition[]
				{
					ADMailboxRecipientSchema.ExchangeGuid
				};
				IEnumerable<PropertyDefinition> propertiesToRead = properties.Concat(second);
				return this.ExecuteSingleObjectQueryWithFallback<ADRawEntry>((IRecipientSession session) => session.FindByExchangeGuidIncludingAlternate(exchangeGuid, propertiesToRead), null, propertiesToRead);
			}, "FindByExchangeGuidIncludingAlternate");
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0005C40C File Offset: 0x0005A60C
		TObject IRecipientSession.FindByExchangeGuidIncludingAlternate<TObject>(Guid exchangeGuid)
		{
			return base.InvokeGetObjectWithAPILogging<TObject>(() => this.ExecuteSingleObjectQueryWithFallback<TObject>((IRecipientSession session) => session.FindByExchangeGuidIncludingAlternate<TObject>(exchangeGuid), null, null), "FindByExchangeGuidIncludingAlternate");
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0005C478 File Offset: 0x0005A678
		ADRecipient IRecipientSession.FindByExchangeGuidIncludingArchive(Guid exchangeGuid)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.ExecuteSingleObjectQueryWithFallback<ADRecipient>((IRecipientSession session) => session.FindByExchangeGuidIncludingArchive(exchangeGuid), null, null), "FindByExchangeGuidIncludingArchive");
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0005C4D0 File Offset: 0x0005A6D0
		Result<ADRecipient>[] IRecipientSession.FindByExchangeGuidsIncludingArchive(Guid[] keys)
		{
			return base.InvokeWithAPILogging<Result<ADRecipient>[]>(() => this.GetSession().FindByExchangeGuidsIncludingArchive(keys), "FindByExchangeGuidsIncludingArchive");
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0005C53C File Offset: 0x0005A73C
		ADRecipient IRecipientSession.FindByLegacyExchangeDN(string legacyExchangeDN)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.ExecuteSingleObjectQueryWithFallback<ADRecipient>((IRecipientSession session) => session.FindByLegacyExchangeDN(legacyExchangeDN), null, null), "FindByLegacyExchangeDN");
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0005C59C File Offset: 0x0005A79C
		Result<ADRawEntry>[] IRecipientSession.FindByLegacyExchangeDNs(string[] legacyExchangeDNs, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<Result<ADRawEntry>[]>(() => this.GetSession().FindByLegacyExchangeDNs(legacyExchangeDNs, properties), "FindByLegacyExchangeDNs");
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0005C60C File Offset: 0x0005A80C
		ADRecipient IRecipientSession.FindByObjectGuid(Guid guid)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.ExecuteSingleObjectQueryWithFallback<ADRecipient>((IRecipientSession session) => session.FindByObjectGuid(guid), null, null), "FindByObjectGuid");
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0005C678 File Offset: 0x0005A878
		ADRecipient IRecipientSession.FindByProxyAddress(ProxyAddress proxyAddress)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.ExecuteSingleObjectQueryWithFallback<ADRecipient>((IRecipientSession session) => session.FindByProxyAddress(proxyAddress), null, null), "FindByProxyAddress");
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0005C744 File Offset: 0x0005A944
		ADRawEntry IRecipientSession.FindByProxyAddress(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<ADRawEntry>(delegate
			{
				if (properties == null)
				{
					return null;
				}
				PropertyDefinition[] second = new PropertyDefinition[]
				{
					ADRecipientSchema.EmailAddresses
				};
				IEnumerable<PropertyDefinition> propertiesToRead = properties.Concat(second);
				return this.ExecuteSingleObjectQueryWithFallback<ADRawEntry>((IRecipientSession session) => session.FindByProxyAddress(proxyAddress, propertiesToRead), null, propertiesToRead);
			}, "FindByProxyAddress");
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0005C7A4 File Offset: 0x0005A9A4
		TEntry IRecipientSession.FindByProxyAddress<TEntry>(ProxyAddress proxyAddress)
		{
			return base.InvokeGetObjectWithAPILogging<TEntry>(() => this.GetSession().FindByProxyAddress<TEntry>(proxyAddress), "FindByProxyAddress");
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0005C804 File Offset: 0x0005AA04
		Result<ADRawEntry>[] IRecipientSession.FindByProxyAddresses(ProxyAddress[] proxyAddresses, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<Result<ADRawEntry>[]>(() => this.GetSession().FindByProxyAddresses(proxyAddresses, properties), "FindByProxyAddresses");
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0005C864 File Offset: 0x0005AA64
		Result<TEntry>[] IRecipientSession.FindByProxyAddresses<TEntry>(ProxyAddress[] proxyAddresses)
		{
			return base.InvokeWithAPILogging<Result<TEntry>[]>(() => this.GetSession().FindByProxyAddresses<TEntry>(proxyAddresses), "FindByProxyAddresses");
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0005C8BC File Offset: 0x0005AABC
		Result<ADRecipient>[] IRecipientSession.FindByProxyAddresses(ProxyAddress[] proxyAddresses)
		{
			return base.InvokeWithAPILogging<Result<ADRecipient>[]>(() => this.GetSession().FindByProxyAddresses(proxyAddresses), "FindByProxyAddresses");
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0005C928 File Offset: 0x0005AB28
		ADRecipient IRecipientSession.FindBySid(SecurityIdentifier sId)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.ExecuteSingleObjectQueryWithFallback<ADRecipient>((IRecipientSession session) => session.FindBySid(sId), null, null), "FindBySid");
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0005C998 File Offset: 0x0005AB98
		ADRawEntry[] IRecipientSession.FindDeletedADRawEntryByUsnRange(ADObjectId lastKnownParentId, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties, QueryFilter additionalFilter)
		{
			return base.InvokeWithAPILogging<ADRawEntry[]>(() => this.GetSession().FindDeletedADRawEntryByUsnRange(lastKnownParentId, startUsn, sizeLimit, properties, additionalFilter), "FindDeletedADRawEntryByUsnRange");
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0005CA2C File Offset: 0x0005AC2C
		MiniRecipient[] IRecipientSession.FindMiniRecipient(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeWithAPILogging<MiniRecipient[]>(() => this.GetSession().FindMiniRecipient(rootId, scope, filter, sortBy, maxResults, properties), "FindMiniRecipient");
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0005CABC File Offset: 0x0005ACBC
		MiniRecipient[] IRecipientSession.FindMiniRecipientByANR(string anrMatch, int maxResults, SortBy sortBy, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeWithAPILogging<MiniRecipient[]>(() => this.GetSession().FindMiniRecipientByANR(anrMatch, maxResults, sortBy, properties), "FindMiniRecipientByANR");
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0005CB48 File Offset: 0x0005AD48
		TResult IRecipientSession.FindMiniRecipientByProxyAddress<TResult>(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<TResult>(() => this.ExecuteSingleObjectQueryWithFallback<TResult>((IRecipientSession session) => session.FindMiniRecipientByProxyAddress<TResult>(proxyAddress, properties), null, properties), "FindMiniRecipientByProxyAddress");
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0005CBC4 File Offset: 0x0005ADC4
		TResult IRecipientSession.FindMiniRecipientBySid<TResult>(SecurityIdentifier sId, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<TResult>(() => this.ExecuteSingleObjectQueryWithFallback<TResult>((IRecipientSession session) => session.FindMiniRecipientBySid<TResult>(sId, properties), null, properties), "FindMiniRecipientBySid");
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0005CC2C File Offset: 0x0005AE2C
		ADRecipient[] IRecipientSession.FindNames(IDictionary<PropertyDefinition, object> dictionary, int limit)
		{
			return base.InvokeWithAPILogging<ADRecipient[]>(() => this.GetSession().FindNames(dictionary, limit), "FindNames");
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0005CC98 File Offset: 0x0005AE98
		object[][] IRecipientSession.FindNamesView(IDictionary<PropertyDefinition, object> dictionary, int limit, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<object[][]>(() => this.GetSession().FindNamesView(dictionary, limit, properties), "FindNamesView");
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0005CD00 File Offset: 0x0005AF00
		Result<OWAMiniRecipient>[] IRecipientSession.FindOWAMiniRecipientByUserPrincipalName(string[] userPrincipalNames)
		{
			return base.InvokeWithAPILogging<Result<OWAMiniRecipient>[]>(() => this.GetSession().FindOWAMiniRecipientByUserPrincipalName(userPrincipalNames), "FindOWAMiniRecipientByUserPrincipalName");
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0005CD70 File Offset: 0x0005AF70
		ADPagedReader<ADRecipient> IRecipientSession.FindPaged(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			return base.InvokeWithAPILogging<ADPagedReader<ADRecipient>>(() => this.GetSession().FindPaged(rootId, scope, filter, sortBy, pageSize), "FindPaged");
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0005CE04 File Offset: 0x0005B004
		ADPagedReader<TEntry> IRecipientSession.FindPagedMiniRecipient<TEntry>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeWithAPILogging<ADPagedReader<TEntry>>(() => this.GetSession().FindPagedMiniRecipient<TEntry>(rootId, scope, filter, sortBy, pageSize, properties), "FindPagedMiniRecipient");
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0005CEA0 File Offset: 0x0005B0A0
		ADRawEntry[] IRecipientSession.FindRecipient(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeWithAPILogging<ADRawEntry[]>(() => this.GetSession().FindRecipient(rootId, scope, filter, sortBy, maxResults, properties), "FindRecipient");
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0005CF24 File Offset: 0x0005B124
		IEnumerable<ADGroup> IRecipientSession.FindRoleGroupsByForeignGroupSid(ADObjectId root, SecurityIdentifier sId)
		{
			return base.InvokeWithAPILogging<IEnumerable<ADGroup>>(() => this.GetSession().FindRoleGroupsByForeignGroupSid(root, sId), "FindRoleGroupsByForeignGroupSid");
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0005CF8C File Offset: 0x0005B18C
		List<string> IRecipientSession.GetTokenSids(ADRawEntry user, AssignmentMethod assignmentMethodFlags)
		{
			return base.InvokeWithAPILogging<List<string>>(() => this.GetSession().GetTokenSids(user, assignmentMethodFlags), "GetTokenSids");
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0005CFF4 File Offset: 0x0005B1F4
		List<string> IRecipientSession.GetTokenSids(ADObjectId userId, AssignmentMethod assignmentMethodFlags)
		{
			return base.InvokeWithAPILogging<List<string>>(() => this.GetSession().GetTokenSids(userId, assignmentMethodFlags), "GetTokenSids");
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0005D054 File Offset: 0x0005B254
		SecurityIdentifier IRecipientSession.GetWellKnownExchangeGroupSid(Guid wkguid)
		{
			return base.InvokeWithAPILogging<SecurityIdentifier>(() => this.GetSession().GetWellKnownExchangeGroupSid(wkguid), "GetWellKnownExchangeGroupSid");
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0005D0AC File Offset: 0x0005B2AC
		bool IRecipientSession.IsLegacyDNInUse(string legacyDN)
		{
			return base.InvokeWithAPILogging<bool>(() => this.GetSession().IsLegacyDNInUse(legacyDN), "IsLegacyDNInUse");
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0005D110 File Offset: 0x0005B310
		bool IRecipientSession.IsMemberOfGroupByWellKnownGuid(Guid wellKnownGuid, string containerDN, ADObjectId id)
		{
			return base.InvokeWithAPILogging<bool>(() => this.GetSession().IsMemberOfGroupByWellKnownGuid(wellKnownGuid, containerDN, id), "IsMemberOfGroupByWellKnownGuid");
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0005D178 File Offset: 0x0005B378
		bool IRecipientSession.IsRecipientInOrg(ProxyAddress proxyAddress)
		{
			return base.InvokeWithAPILogging<bool>(() => this.GetSession().IsRecipientInOrg(proxyAddress), "IsRecipientInOrg");
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0005D1BD File Offset: 0x0005B3BD
		bool IRecipientSession.IsReducedRecipientSession()
		{
			return base.InvokeWithAPILogging<bool>(() => base.GetSession().IsReducedRecipientSession(), "IsReducedRecipientSession");
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0005D1F8 File Offset: 0x0005B3F8
		bool IRecipientSession.IsThrottlingPolicyInUse(ADObjectId throttlingPolicyId)
		{
			return base.InvokeWithAPILogging<bool>(() => this.GetSession().IsThrottlingPolicyInUse(throttlingPolicyId), "IsThrottlingPolicyInUse");
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0005D264 File Offset: 0x0005B464
		ADRecipient IRecipientSession.Read(ADObjectId entryId)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.ExecuteSingleObjectQueryWithFallback<ADRecipient>((IRecipientSession session) => session.Read(entryId), null, null), "Read");
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0005D2D8 File Offset: 0x0005B4D8
		MiniRecipient IRecipientSession.ReadMiniRecipient(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<MiniRecipient>(() => this.ExecuteSingleObjectQueryWithFallback<MiniRecipient>((IRecipientSession session) => session.ReadMiniRecipient(entryId, properties), null, properties), "ReadMiniRecipient");
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0005D354 File Offset: 0x0005B554
		TMiniRecipient IRecipientSession.ReadMiniRecipient<TMiniRecipient>(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<TMiniRecipient>(() => this.ExecuteSingleObjectQueryWithFallback<TMiniRecipient>((IRecipientSession session) => session.ReadMiniRecipient<TMiniRecipient>(entryId, properties), null, properties), "ReadMiniRecipient");
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0005D43C File Offset: 0x0005B63C
		ADRawEntry IRecipientSession.FindUserBySid(SecurityIdentifier sId, IList<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<ADRawEntry>(delegate
			{
				if (properties != null)
				{
					List<PropertyDefinition> propertiesToRead = new List<PropertyDefinition>();
					propertiesToRead.Add(IADSecurityPrincipalSchema.Sid);
					propertiesToRead.AddRange(properties);
					return this.ExecuteSingleObjectQueryWithFallback<ADRawEntry>((IRecipientSession session) => session.FindUserBySid(sId, propertiesToRead), null, propertiesToRead);
				}
				return ((IRecipientSession)this).FindBySid(sId);
			}, "FindUserBySid");
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0005D49C File Offset: 0x0005B69C
		Result<ADRecipient>[] IRecipientSession.ReadMultiple(ADObjectId[] entryIds)
		{
			return base.InvokeWithAPILogging<Result<ADRecipient>[]>(() => this.GetSession().ReadMultiple(entryIds), "ReadMultiple");
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0005D4FC File Offset: 0x0005B6FC
		Result<ADRawEntry>[] IRecipientSession.ReadMultiple(ADObjectId[] entryIds, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<Result<ADRawEntry>[]>(() => this.GetSession().ReadMultiple(entryIds, properties), "ReadMultiple");
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0005D55C File Offset: 0x0005B75C
		Result<ADGroup>[] IRecipientSession.ReadMultipleADGroups(ADObjectId[] entryIds)
		{
			return base.InvokeWithAPILogging<Result<ADGroup>[]>(() => this.GetSession().ReadMultipleADGroups(entryIds), "ReadMultipleADGroups");
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0005D5B4 File Offset: 0x0005B7B4
		Result<ADUser>[] IRecipientSession.ReadMultipleADUsers(ADObjectId[] userIds)
		{
			return base.InvokeWithAPILogging<Result<ADUser>[]>(() => this.GetSession().ReadMultipleADUsers(userIds), "ReadMultipleADUsers");
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0005D614 File Offset: 0x0005B814
		Result<ADRawEntry>[] IRecipientSession.ReadMultipleWithDeletedObjects(ADObjectId[] entryIds, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<Result<ADRawEntry>[]>(() => this.GetSession().ReadMultipleWithDeletedObjects(entryIds, properties), "ReadMultipleWithDeletedObjects");
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0005D66C File Offset: 0x0005B86C
		MiniRecipientWithTokenGroups IRecipientSession.ReadTokenGroupsGlobalAndUniversal(ADObjectId id)
		{
			return base.ExecuteSingleObjectQueryWithFallback<MiniRecipientWithTokenGroups>((IRecipientSession session) => session.ReadTokenGroupsGlobalAndUniversal(id), null, null);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0005D6BC File Offset: 0x0005B8BC
		ADObjectId[] IRecipientSession.ResolveSidsToADObjectIds(string[] sids)
		{
			return base.InvokeWithAPILogging<ADObjectId[]>(() => this.GetSession().ResolveSidsToADObjectIds(sids), "ResolveSidsToADObjectIds");
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0005D710 File Offset: 0x0005B910
		void IRecipientSession.Save(ADRecipient instanceToSave)
		{
			base.InvokeWithAPILogging<bool>(delegate
			{
				this.InternalSave(instanceToSave);
				return true;
			}, "Save");
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0005D79C File Offset: 0x0005B99C
		void IRecipientSession.Save(ADRecipient instanceToSave, bool bypassValidation)
		{
			base.InvokeWithAPILogging<bool>(delegate
			{
				ObjectState objectState = instanceToSave.ObjectState;
				this.GetSession().Save(instanceToSave, bypassValidation);
				this.CacheUpdateFromSavedObject(instanceToSave, objectState);
				return true;
			}, "Save");
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0005D804 File Offset: 0x0005BA04
		void IRecipientSession.SetPassword(ADObject obj, SecureString password)
		{
			base.InvokeWithAPILogging<bool>(delegate
			{
				this.GetSession().SetPassword(obj, password);
				return true;
			}, "SetPassword");
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0005D86C File Offset: 0x0005BA6C
		void IRecipientSession.SetPassword(ADObjectId id, SecureString password)
		{
			base.InvokeWithAPILogging<bool>(delegate
			{
				this.GetSession().SetPassword(id, password);
				return true;
			}, "SetPassword");
		}
	}
}
