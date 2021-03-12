using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001DA RID: 474
	internal class CompositeTenantRecipientSession : CompositeDirectorySession<ITenantRecipientSession>, ITenantRecipientSession, IRecipientSession, IDirectorySession, IConfigDataProvider, IAggregateSession
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x0005D8AC File Offset: 0x0005BAAC
		protected override string Implementor
		{
			get
			{
				return "CompositeTenantRecipientSession";
			}
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0005D8B3 File Offset: 0x0005BAB3
		internal CompositeTenantRecipientSession(ITenantRecipientSession cacheSession, ITenantRecipientSession directorySession, bool cacheSessionForDeletingOnly = false) : base(cacheSession, directorySession, cacheSessionForDeletingOnly)
		{
			this.compositeRecipientSession = new CompositeRecipientSession(cacheSession, directorySession, cacheSessionForDeletingOnly);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0005D8CC File Offset: 0x0005BACC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private IRecipientSession GetCompositeRecipientSession()
		{
			return this.compositeRecipientSession;
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x0005D8E1 File Offset: 0x0005BAE1
		ADObjectId IRecipientSession.SearchRoot
		{
			get
			{
				return this.GetCompositeRecipientSession().SearchRoot;
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0005D91C File Offset: 0x0005BB1C
		ITableView IRecipientSession.Browse(ADObjectId addressListId, int rowCountSuggestion, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<ITableView>(() => this.GetCompositeRecipientSession().Browse(addressListId, rowCountSuggestion, properties), "Browse");
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0005D984 File Offset: 0x0005BB84
		void IRecipientSession.Delete(ADRecipient instanceToDelete)
		{
			base.InvokeWithAPILogging<bool>(delegate
			{
				this.GetCompositeRecipientSession().Delete(instanceToDelete);
				return true;
			}, "Delete");
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0005D9F8 File Offset: 0x0005BBF8
		ADRecipient[] IRecipientSession.Find(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			return base.InvokeWithAPILogging<ADRecipient[]>(() => this.GetCompositeRecipientSession().Find(rootId, scope, filter, sortBy, maxResults), "Find");
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0005DA74 File Offset: 0x0005BC74
		ADRawEntry IRecipientSession.FindADRawEntryBySid(SecurityIdentifier sId, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<ADRawEntry>(() => this.GetCompositeRecipientSession().FindADRawEntryBySid(sId, properties), "FindADRawEntryBySid");
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0005DAF8 File Offset: 0x0005BCF8
		ADRawEntry[] IRecipientSession.FindADRawEntryByUsnRange(ADObjectId root, long startUsn, long endUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties, QueryScope scope, QueryFilter additionalFilter)
		{
			return base.InvokeWithAPILogging<ADRawEntry[]>(() => this.GetCompositeRecipientSession().FindADRawEntryByUsnRange(root, startUsn, endUsn, sizeLimit, properties, scope, additionalFilter), "FindADRawEntryByUsnRange");
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0005DB80 File Offset: 0x0005BD80
		Result<ADRecipient>[] IRecipientSession.FindADRecipientsByLegacyExchangeDNs(string[] legacyExchangeDNs)
		{
			return base.InvokeWithAPILogging<Result<ADRecipient>[]>(() => this.GetCompositeRecipientSession().FindADRecipientsByLegacyExchangeDNs(legacyExchangeDNs), "FindADRecipientsByLegacyExchangeDNs");
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0005DBF0 File Offset: 0x0005BDF0
		ADUser[] IRecipientSession.FindADUser(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			return base.InvokeWithAPILogging<ADUser[]>(() => this.GetCompositeRecipientSession().FindADUser(rootId, scope, filter, sortBy, maxResults), "FindADUser");
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0005DC68 File Offset: 0x0005BE68
		ADUser IRecipientSession.FindADUserByObjectId(ADObjectId adObjectId)
		{
			return base.InvokeGetObjectWithAPILogging<ADUser>(() => this.GetCompositeRecipientSession().FindADUserByObjectId(adObjectId), "FindADUserByObjectId");
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0005DCC0 File Offset: 0x0005BEC0
		ADUser IRecipientSession.FindADUserByExternalDirectoryObjectId(string externalDirectoryObjectId)
		{
			return base.InvokeGetObjectWithAPILogging<ADUser>(() => this.GetCompositeRecipientSession().FindADUserByExternalDirectoryObjectId(externalDirectoryObjectId), "FindADUserByExternalDirectoryObjectId");
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0005DD20 File Offset: 0x0005BF20
		ADObject IRecipientSession.FindByAccountName<T>(string domainName, string accountName)
		{
			return base.InvokeGetObjectWithAPILogging<ADObject>(() => this.GetCompositeRecipientSession().FindByAccountName<T>(domainName, accountName), "FindByAccountName");
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0005DD94 File Offset: 0x0005BF94
		IEnumerable<T> IRecipientSession.FindByAccountName<T>(string domain, string account, ADObjectId rootId, QueryFilter searchFilter)
		{
			return base.InvokeWithAPILogging<IEnumerable<T>>(() => this.GetCompositeRecipientSession().FindByAccountName<T>(domain, account, rootId, searchFilter), "FindByAccountName");
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0005DE10 File Offset: 0x0005C010
		ADRecipient[] IRecipientSession.FindByANR(string anrMatch, int maxResults, SortBy sortBy)
		{
			return base.InvokeWithAPILogging<ADRecipient[]>(() => this.GetCompositeRecipientSession().FindByANR(anrMatch, maxResults, sortBy), "FindByANR");
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0005DE88 File Offset: 0x0005C088
		ADRawEntry[] IRecipientSession.FindByANR(string anrMatch, int maxResults, SortBy sortBy, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeWithAPILogging<ADRawEntry[]>(() => this.GetCompositeRecipientSession().FindByANR(anrMatch, maxResults, sortBy, properties), "FindByANR");
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0005DEF8 File Offset: 0x0005C0F8
		ADRecipient IRecipientSession.FindByCertificate(X509Identifier identifier)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.GetCompositeRecipientSession().FindByCertificate(identifier), "FindByCertificate");
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0005DF58 File Offset: 0x0005C158
		ADRawEntry[] IRecipientSession.FindByCertificate(X509Identifier identifier, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<ADRawEntry[]>(() => this.GetCompositeRecipientSession().FindByCertificate(identifier, properties), "FindByCertificate");
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0005DFC0 File Offset: 0x0005C1C0
		ADRawEntry IRecipientSession.FindByExchangeGuid(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<ADRawEntry>(() => this.GetCompositeRecipientSession().FindByExchangeGuid(exchangeGuid, properties), "FindByExchangeGuid");
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0005E028 File Offset: 0x0005C228
		TEntry IRecipientSession.FindByExchangeGuid<TEntry>(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<TEntry>(() => this.GetCompositeRecipientSession().FindByExchangeGuid<TEntry>(exchangeGuid, properties), "FindByExchangeGuid");
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0005E088 File Offset: 0x0005C288
		ADRecipient IRecipientSession.FindByExchangeObjectId(Guid exchangeObjectId)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.GetCompositeRecipientSession().FindByExchangeObjectId(exchangeObjectId), "FindByExchangeObjectId");
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0005E0E0 File Offset: 0x0005C2E0
		ADRecipient IRecipientSession.FindByExchangeGuid(Guid exchangeGuid)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.GetCompositeRecipientSession().FindByExchangeGuid(exchangeGuid), "FindByExchangeGuid");
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0005E138 File Offset: 0x0005C338
		ADRecipient IRecipientSession.FindByExchangeGuidIncludingAlternate(Guid exchangeGuid)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.GetCompositeRecipientSession().FindByExchangeGuidIncludingAlternate(exchangeGuid), "FindByExchangeGuidIncludingAlternate");
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0005E198 File Offset: 0x0005C398
		ADRawEntry IRecipientSession.FindByExchangeGuidIncludingAlternate(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<ADRawEntry>(() => this.GetCompositeRecipientSession().FindByExchangeGuidIncludingAlternate(exchangeGuid, properties), "FindByExchangeGuidIncludingAlternate");
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0005E1F8 File Offset: 0x0005C3F8
		TObject IRecipientSession.FindByExchangeGuidIncludingAlternate<TObject>(Guid exchangeGuid)
		{
			return base.InvokeGetObjectWithAPILogging<TObject>(() => this.GetCompositeRecipientSession().FindByExchangeGuidIncludingAlternate<TObject>(exchangeGuid), "FindByExchangeGuidIncludingAlternate");
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0005E250 File Offset: 0x0005C450
		ADRecipient IRecipientSession.FindByExchangeGuidIncludingArchive(Guid exchangeGuid)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.GetCompositeRecipientSession().FindByExchangeGuidIncludingArchive(exchangeGuid), "FindByExchangeGuidIncludingArchive");
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0005E2A8 File Offset: 0x0005C4A8
		Result<ADRecipient>[] IRecipientSession.FindByExchangeGuidsIncludingArchive(Guid[] keys)
		{
			return base.InvokeWithAPILogging<Result<ADRecipient>[]>(() => this.GetCompositeRecipientSession().FindByExchangeGuidsIncludingArchive(keys), "FindByExchangeGuidsIncludingArchive");
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0005E300 File Offset: 0x0005C500
		ADRecipient IRecipientSession.FindByLegacyExchangeDN(string legacyExchangeDN)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.GetCompositeRecipientSession().FindByLegacyExchangeDN(legacyExchangeDN), "FindByLegacyExchangeDN");
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0005E360 File Offset: 0x0005C560
		Result<ADRawEntry>[] IRecipientSession.FindByLegacyExchangeDNs(string[] legacyExchangeDNs, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<Result<ADRawEntry>[]>(() => this.GetCompositeRecipientSession().FindByLegacyExchangeDNs(legacyExchangeDNs, properties), "FindByLegacyExchangeDNs");
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0005E3C0 File Offset: 0x0005C5C0
		ADRecipient IRecipientSession.FindByObjectGuid(Guid guid)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.GetCompositeRecipientSession().FindByObjectGuid(guid), "FindByObjectGuid");
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0005E418 File Offset: 0x0005C618
		ADRecipient IRecipientSession.FindByProxyAddress(ProxyAddress proxyAddress)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.GetCompositeRecipientSession().FindByProxyAddress(proxyAddress), "FindByProxyAddress");
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0005E478 File Offset: 0x0005C678
		ADRawEntry IRecipientSession.FindByProxyAddress(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<ADRawEntry>(() => this.GetCompositeRecipientSession().FindByProxyAddress(proxyAddress, properties), "FindByProxyAddress");
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0005E4D8 File Offset: 0x0005C6D8
		TEntry IRecipientSession.FindByProxyAddress<TEntry>(ProxyAddress proxyAddress)
		{
			return base.InvokeGetObjectWithAPILogging<TEntry>(() => this.GetCompositeRecipientSession().FindByProxyAddress<TEntry>(proxyAddress), "FindByProxyAddress");
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0005E538 File Offset: 0x0005C738
		Result<ADRawEntry>[] IRecipientSession.FindByProxyAddresses(ProxyAddress[] proxyAddresses, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<Result<ADRawEntry>[]>(() => this.GetCompositeRecipientSession().FindByProxyAddresses(proxyAddresses, properties), "FindByProxyAddresses");
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0005E598 File Offset: 0x0005C798
		Result<TEntry>[] IRecipientSession.FindByProxyAddresses<TEntry>(ProxyAddress[] proxyAddresses)
		{
			return base.InvokeWithAPILogging<Result<TEntry>[]>(() => this.GetCompositeRecipientSession().FindByProxyAddresses<TEntry>(proxyAddresses), "FindByProxyAddresses");
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0005E5F0 File Offset: 0x0005C7F0
		Result<ADRecipient>[] IRecipientSession.FindByProxyAddresses(ProxyAddress[] proxyAddresses)
		{
			return base.InvokeWithAPILogging<Result<ADRecipient>[]>(() => this.GetCompositeRecipientSession().FindByProxyAddresses(proxyAddresses), "FindByProxyAddresses");
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0005E648 File Offset: 0x0005C848
		ADRecipient IRecipientSession.FindBySid(SecurityIdentifier sId)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.GetCompositeRecipientSession().FindBySid(sId), "FindBySid");
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0005E6B8 File Offset: 0x0005C8B8
		ADRawEntry[] IRecipientSession.FindDeletedADRawEntryByUsnRange(ADObjectId lastKnownParentId, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties, QueryFilter additionalFilter)
		{
			return base.InvokeWithAPILogging<ADRawEntry[]>(() => this.GetCompositeRecipientSession().FindDeletedADRawEntryByUsnRange(lastKnownParentId, startUsn, sizeLimit, properties, additionalFilter), "FindDeletedADRawEntryByUsnRange");
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0005E74C File Offset: 0x0005C94C
		MiniRecipient[] IRecipientSession.FindMiniRecipient(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeWithAPILogging<MiniRecipient[]>(() => this.GetCompositeRecipientSession().FindMiniRecipient(rootId, scope, filter, sortBy, maxResults, properties), "FindMiniRecipient");
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0005E7DC File Offset: 0x0005C9DC
		MiniRecipient[] IRecipientSession.FindMiniRecipientByANR(string anrMatch, int maxResults, SortBy sortBy, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeWithAPILogging<MiniRecipient[]>(() => this.GetCompositeRecipientSession().FindMiniRecipientByANR(anrMatch, maxResults, sortBy, properties), "FindMiniRecipientByANR");
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0005E850 File Offset: 0x0005CA50
		TResult IRecipientSession.FindMiniRecipientByProxyAddress<TResult>(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<TResult>(() => this.GetCompositeRecipientSession().FindMiniRecipientByProxyAddress<TResult>(proxyAddress, properties), "FindMiniRecipientByProxyAddress");
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0005E8B8 File Offset: 0x0005CAB8
		TResult IRecipientSession.FindMiniRecipientBySid<TResult>(SecurityIdentifier sId, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<TResult>(() => this.GetCompositeRecipientSession().FindMiniRecipientBySid<TResult>(sId, properties), "FindMiniRecipientBySid");
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0005E920 File Offset: 0x0005CB20
		ADRecipient[] IRecipientSession.FindNames(IDictionary<PropertyDefinition, object> dictionary, int limit)
		{
			return base.InvokeWithAPILogging<ADRecipient[]>(() => this.GetCompositeRecipientSession().FindNames(dictionary, limit), "FindNames");
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0005E98C File Offset: 0x0005CB8C
		object[][] IRecipientSession.FindNamesView(IDictionary<PropertyDefinition, object> dictionary, int limit, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<object[][]>(() => this.GetCompositeRecipientSession().FindNamesView(dictionary, limit, properties), "FindNamesView");
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0005E9F4 File Offset: 0x0005CBF4
		Result<OWAMiniRecipient>[] IRecipientSession.FindOWAMiniRecipientByUserPrincipalName(string[] userPrincipalNames)
		{
			return base.InvokeWithAPILogging<Result<OWAMiniRecipient>[]>(() => this.GetCompositeRecipientSession().FindOWAMiniRecipientByUserPrincipalName(userPrincipalNames), "FindOWAMiniRecipientByUserPrincipalName");
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0005EA64 File Offset: 0x0005CC64
		ADPagedReader<ADRecipient> IRecipientSession.FindPaged(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			return base.InvokeWithAPILogging<ADPagedReader<ADRecipient>>(() => this.GetCompositeRecipientSession().FindPaged(rootId, scope, filter, sortBy, pageSize), "FindPaged");
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0005EAF8 File Offset: 0x0005CCF8
		ADPagedReader<TEntry> IRecipientSession.FindPagedMiniRecipient<TEntry>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeWithAPILogging<ADPagedReader<TEntry>>(() => this.GetCompositeRecipientSession().FindPagedMiniRecipient<TEntry>(rootId, scope, filter, sortBy, pageSize, properties), "FindPagedMiniRecipient");
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0005EB94 File Offset: 0x0005CD94
		ADRawEntry[] IRecipientSession.FindRecipient(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeWithAPILogging<ADRawEntry[]>(() => this.GetCompositeRecipientSession().FindRecipient(rootId, scope, filter, sortBy, maxResults, properties), "FindRecipient");
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0005EC18 File Offset: 0x0005CE18
		IEnumerable<ADGroup> IRecipientSession.FindRoleGroupsByForeignGroupSid(ADObjectId root, SecurityIdentifier sId)
		{
			return base.InvokeWithAPILogging<IEnumerable<ADGroup>>(() => this.GetCompositeRecipientSession().FindRoleGroupsByForeignGroupSid(root, sId), "FindRoleGroupsByForeignGroupSid");
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0005EC80 File Offset: 0x0005CE80
		List<string> IRecipientSession.GetTokenSids(ADRawEntry user, AssignmentMethod assignmentMethodFlags)
		{
			return base.InvokeWithAPILogging<List<string>>(() => this.GetCompositeRecipientSession().GetTokenSids(user, assignmentMethodFlags), "GetTokenSids");
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0005ECE8 File Offset: 0x0005CEE8
		List<string> IRecipientSession.GetTokenSids(ADObjectId userId, AssignmentMethod assignmentMethodFlags)
		{
			return base.InvokeWithAPILogging<List<string>>(() => this.GetCompositeRecipientSession().GetTokenSids(userId, assignmentMethodFlags), "GetTokenSids");
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0005ED48 File Offset: 0x0005CF48
		SecurityIdentifier IRecipientSession.GetWellKnownExchangeGroupSid(Guid wkguid)
		{
			return base.InvokeWithAPILogging<SecurityIdentifier>(() => this.GetCompositeRecipientSession().GetWellKnownExchangeGroupSid(wkguid), "GetWellKnownExchangeGroupSid");
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0005EDA0 File Offset: 0x0005CFA0
		bool IRecipientSession.IsLegacyDNInUse(string legacyDN)
		{
			return base.InvokeWithAPILogging<bool>(() => this.GetCompositeRecipientSession().IsLegacyDNInUse(legacyDN), "IsLegacyDNInUse");
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0005EE04 File Offset: 0x0005D004
		bool IRecipientSession.IsMemberOfGroupByWellKnownGuid(Guid wellKnownGuid, string containerDN, ADObjectId id)
		{
			return base.InvokeWithAPILogging<bool>(() => this.GetCompositeRecipientSession().IsMemberOfGroupByWellKnownGuid(wellKnownGuid, containerDN, id), "IsMemberOfGroupByWellKnownGuid");
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0005EE6C File Offset: 0x0005D06C
		bool IRecipientSession.IsRecipientInOrg(ProxyAddress proxyAddress)
		{
			return base.InvokeWithAPILogging<bool>(() => this.GetCompositeRecipientSession().IsRecipientInOrg(proxyAddress), "IsRecipientInOrg");
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0005EEB1 File Offset: 0x0005D0B1
		bool IRecipientSession.IsReducedRecipientSession()
		{
			return base.InvokeWithAPILogging<bool>(() => this.GetCompositeRecipientSession().IsReducedRecipientSession(), "IsReducedRecipientSession");
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0005EEEC File Offset: 0x0005D0EC
		bool IRecipientSession.IsThrottlingPolicyInUse(ADObjectId throttlingPolicyId)
		{
			return base.InvokeWithAPILogging<bool>(() => this.GetCompositeRecipientSession().IsThrottlingPolicyInUse(throttlingPolicyId), "IsThrottlingPolicyInUse");
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0005EF44 File Offset: 0x0005D144
		ADRecipient IRecipientSession.Read(ADObjectId entryId)
		{
			return base.InvokeGetObjectWithAPILogging<ADRecipient>(() => this.GetCompositeRecipientSession().Read(entryId), "Read");
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0005EFA4 File Offset: 0x0005D1A4
		MiniRecipient IRecipientSession.ReadMiniRecipient(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<MiniRecipient>(() => this.GetCompositeRecipientSession().ReadMiniRecipient(entryId, properties), "ReadMiniRecipient");
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0005F00C File Offset: 0x0005D20C
		TMiniRecipient IRecipientSession.ReadMiniRecipient<TMiniRecipient>(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<TMiniRecipient>(() => this.GetCompositeRecipientSession().ReadMiniRecipient<TMiniRecipient>(entryId, properties), "ReadMiniRecipient");
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0005F074 File Offset: 0x0005D274
		ADRawEntry IRecipientSession.FindUserBySid(SecurityIdentifier sId, IList<PropertyDefinition> properties)
		{
			return base.InvokeGetObjectWithAPILogging<ADRawEntry>(() => this.GetCompositeRecipientSession().FindUserBySid(sId, properties), "FindUserBySid");
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0005F0D4 File Offset: 0x0005D2D4
		Result<ADRecipient>[] IRecipientSession.ReadMultiple(ADObjectId[] entryIds)
		{
			return base.InvokeWithAPILogging<Result<ADRecipient>[]>(() => this.GetCompositeRecipientSession().ReadMultiple(entryIds), "ReadMultiple");
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0005F134 File Offset: 0x0005D334
		Result<ADRawEntry>[] IRecipientSession.ReadMultiple(ADObjectId[] entryIds, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<Result<ADRawEntry>[]>(() => this.GetCompositeRecipientSession().ReadMultiple(entryIds, properties), "ReadMultiple");
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0005F194 File Offset: 0x0005D394
		Result<ADGroup>[] IRecipientSession.ReadMultipleADGroups(ADObjectId[] entryIds)
		{
			return base.InvokeWithAPILogging<Result<ADGroup>[]>(() => this.GetCompositeRecipientSession().ReadMultipleADGroups(entryIds), "ReadMultipleADGroups");
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0005F1EC File Offset: 0x0005D3EC
		Result<ADUser>[] IRecipientSession.ReadMultipleADUsers(ADObjectId[] userIds)
		{
			return base.InvokeWithAPILogging<Result<ADUser>[]>(() => this.GetCompositeRecipientSession().ReadMultipleADUsers(userIds), "ReadMultipleADUsers");
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0005F24C File Offset: 0x0005D44C
		Result<ADRawEntry>[] IRecipientSession.ReadMultipleWithDeletedObjects(ADObjectId[] entryIds, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<Result<ADRawEntry>[]>(() => this.GetCompositeRecipientSession().ReadMultipleWithDeletedObjects(entryIds, properties), "ReadMultipleWithDeletedObjects");
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0005F28B File Offset: 0x0005D48B
		MiniRecipientWithTokenGroups IRecipientSession.ReadTokenGroupsGlobalAndUniversal(ADObjectId id)
		{
			return this.GetCompositeRecipientSession().ReadTokenGroupsGlobalAndUniversal(id);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0005F2BC File Offset: 0x0005D4BC
		ADObjectId[] IRecipientSession.ResolveSidsToADObjectIds(string[] sids)
		{
			return base.InvokeWithAPILogging<ADObjectId[]>(() => this.GetCompositeRecipientSession().ResolveSidsToADObjectIds(sids), "ResolveSidsToADObjectIds");
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0005F318 File Offset: 0x0005D518
		void IRecipientSession.Save(ADRecipient instanceToSave)
		{
			base.InvokeWithAPILogging<bool>(delegate
			{
				this.GetCompositeRecipientSession().Save(instanceToSave);
				return true;
			}, "Save");
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0005F378 File Offset: 0x0005D578
		void IRecipientSession.Save(ADRecipient instanceToSave, bool bypassValidation)
		{
			base.InvokeWithAPILogging<bool>(delegate
			{
				this.GetCompositeRecipientSession().Save(instanceToSave, bypassValidation);
				return true;
			}, "Save");
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0005F3E0 File Offset: 0x0005D5E0
		void IRecipientSession.SetPassword(ADObject obj, SecureString password)
		{
			base.InvokeWithAPILogging<bool>(delegate
			{
				this.GetCompositeRecipientSession().SetPassword(obj, password);
				return true;
			}, "SetPassword");
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0005F448 File Offset: 0x0005D648
		void IRecipientSession.SetPassword(ADObjectId id, SecureString password)
		{
			base.InvokeWithAPILogging<bool>(delegate
			{
				this.GetCompositeRecipientSession().SetPassword(id, password);
				return true;
			}, "SetPassword");
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0005F4A8 File Offset: 0x0005D6A8
		ADRawEntry ITenantRecipientSession.ChooseBetweenAmbiguousUsers(ADRawEntry[] entries)
		{
			return base.InvokeWithAPILogging<ADRawEntry>(() => this.GetSession().ChooseBetweenAmbiguousUsers(entries), "ChooseBetweenAmbiguousUsers");
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0005F508 File Offset: 0x0005D708
		ADObjectId ITenantRecipientSession.ChooseBetweenAmbiguousUsers(ADObjectId user1Id, ADObjectId user2Id)
		{
			return base.InvokeWithAPILogging<ADObjectId>(() => this.GetSession().ChooseBetweenAmbiguousUsers(user1Id, user2Id), "ChooseBetweenAmbiguousUsers");
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x0005F547 File Offset: 0x0005D747
		DirectoryBackendType ITenantRecipientSession.DirectoryBackendType
		{
			get
			{
				return base.GetSession().DirectoryBackendType;
			}
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0005F57C File Offset: 0x0005D77C
		Result<ADRawEntry>[] ITenantRecipientSession.FindByExternalDirectoryObjectIds(string[] externalDirectoryObjectIds, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<Result<ADRawEntry>[]>(() => this.GetSession().FindByExternalDirectoryObjectIds(externalDirectoryObjectIds, properties), "FindByExternalDirectoryObjectIds");
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0005F5E8 File Offset: 0x0005D7E8
		Result<ADRawEntry>[] ITenantRecipientSession.FindByExternalDirectoryObjectIds(string[] externalDirectoryObjectIds, bool includeDeletedObjects, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<Result<ADRawEntry>[]>(() => this.GetSession().FindByExternalDirectoryObjectIds(externalDirectoryObjectIds, includeDeletedObjects, properties), "FindByExternalDirectoryObjectIds");
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0005F65C File Offset: 0x0005D85C
		ADRawEntry[] ITenantRecipientSession.FindByNetID(string netID, string organizationContext, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<ADRawEntry[]>(() => this.GetSession().FindByNetID(netID, organizationContext, properties), "FindByNetID");
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0005F6C8 File Offset: 0x0005D8C8
		ADRawEntry[] ITenantRecipientSession.FindByNetID(string netID, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<ADRawEntry[]>(() => this.GetSession().FindByNetID(netID, properties), "FindByNetID");
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0005F738 File Offset: 0x0005D938
		MiniRecipient ITenantRecipientSession.FindRecipientByNetID(NetID netId)
		{
			return base.InvokeGetObjectWithAPILogging<MiniRecipient>(() => this.ExecuteSingleObjectQueryWithFallback<MiniRecipient>((ITenantRecipientSession session) => session.FindRecipientByNetID(netId), null, null), "FindRecipientByNetID");
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0005F80C File Offset: 0x0005DA0C
		ADRawEntry ITenantRecipientSession.FindUniqueEntryByNetID(string netID, string organizationContext, params PropertyDefinition[] properties)
		{
			return base.InvokeGetObjectWithAPILogging<ADRawEntry>(delegate
			{
				if (properties == null)
				{
					return null;
				}
				PropertyDefinition[] propertiesToRead = new List<PropertyDefinition>(properties)
				{
					ADUserSchema.NetID
				}.ToArray();
				return this.ExecuteSingleObjectQueryWithFallback<ADRawEntry>((ITenantRecipientSession session) => session.FindUniqueEntryByNetID(netID, organizationContext, propertiesToRead), null, propertiesToRead);
			}, "FindUniqueEntryByNetID");
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0005F8E4 File Offset: 0x0005DAE4
		ADRawEntry ITenantRecipientSession.FindUniqueEntryByNetID(string netID, params PropertyDefinition[] properties)
		{
			return base.InvokeGetObjectWithAPILogging<ADRawEntry>(delegate
			{
				if (properties == null)
				{
					return null;
				}
				PropertyDefinition[] propertiesToRead = new List<PropertyDefinition>(properties)
				{
					ADUserSchema.NetID
				}.ToArray();
				return this.ExecuteSingleObjectQueryWithFallback<ADRawEntry>((ITenantRecipientSession session) => session.FindUniqueEntryByNetID(netID, propertiesToRead), null, propertiesToRead);
			}, "FindUniqueEntryByNetID");
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0005F94C File Offset: 0x0005DB4C
		public ADRawEntry FindByLiveIdMemberName(string liveIdMemberName, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<ADRawEntry>(() => this.GetSession().FindByLiveIdMemberName(liveIdMemberName, properties), "FindByLiveIdMemberName");
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0005F9B4 File Offset: 0x0005DBB4
		Result<ADRawEntry>[] ITenantRecipientSession.ReadMultipleByLinkedPartnerId(LinkedPartnerGroupInformation[] entryIds, params PropertyDefinition[] properties)
		{
			return base.InvokeWithAPILogging<Result<ADRawEntry>[]>(() => this.GetSession().ReadMultipleByLinkedPartnerId(entryIds, properties), "ReadMultipleByLinkedPartnerId");
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x0005F9F4 File Offset: 0x0005DBF4
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x0005FA38 File Offset: 0x0005DC38
		MbxReadMode IAggregateSession.MbxReadMode
		{
			get
			{
				IAggregateSession aggregateSession = base.GetSession() as IAggregateSession;
				if (aggregateSession != null)
				{
					return aggregateSession.MbxReadMode;
				}
				LocalizedString message = DirectoryStrings.ApiNotSupportedInBusinessSessionError(base.GetSession().GetType().FullName, "MbxReadMode");
				throw new ApiNotSupportedException(message);
			}
			set
			{
				IAggregateSession aggregateSession = base.GetSession() as IAggregateSession;
				if (aggregateSession != null)
				{
					aggregateSession.MbxReadMode = value;
					return;
				}
				LocalizedString message = DirectoryStrings.ApiNotSupportedInBusinessSessionError(base.GetSession().GetType().FullName, "MbxReadMode");
				throw new ApiNotSupportedException(message);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x0005FA80 File Offset: 0x0005DC80
		// (set) Token: 0x060013B9 RID: 5049 RVA: 0x0005FAC4 File Offset: 0x0005DCC4
		BackendWriteMode IAggregateSession.BackendWriteMode
		{
			get
			{
				IAggregateSession aggregateSession = base.GetSession() as IAggregateSession;
				if (aggregateSession != null)
				{
					return aggregateSession.BackendWriteMode;
				}
				LocalizedString message = DirectoryStrings.ApiNotSupportedInBusinessSessionError(base.GetSession().GetType().FullName, "BackendWriteMode");
				throw new ApiNotSupportedException(message);
			}
			set
			{
				IAggregateSession aggregateSession = base.GetSession() as IAggregateSession;
				if (aggregateSession != null)
				{
					aggregateSession.BackendWriteMode = value;
					return;
				}
				LocalizedString message = DirectoryStrings.ApiNotSupportedInBusinessSessionError(base.GetSession().GetType().FullName, "BackendWriteMode");
				throw new ApiNotSupportedException(message);
			}
		}

		// Token: 0x04000AE6 RID: 2790
		private CompositeRecipientSession compositeRecipientSession;
	}
}
