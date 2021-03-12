using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security;
using System.Security.Principal;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000D8 RID: 216
	[Serializable]
	internal class FfoRecipientSession : FfoDirectorySession, IRecipientSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x060007B0 RID: 1968 RVA: 0x0001957A File Offset: 0x0001777A
		public FfoRecipientSession(bool useConfigNC, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings) : base(useConfigNC, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00019589 File Offset: 0x00017789
		protected FfoRecipientSession(ADObjectId tenantId) : base(tenantId)
		{
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00019594 File Offset: 0x00017794
		IConfigurable IConfigDataProvider.Read<T>(ObjectId identity)
		{
			IConfigurable configurable = this.ReadImpl<T>(identity);
			ConfigurableObject configurableObject = configurable as ConfigurableObject;
			if (configurableObject != null)
			{
				configurableObject.ResetChangeTracking();
			}
			return configurable;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000195BA File Offset: 0x000177BA
		IConfigurable[] IConfigDataProvider.Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			return (IConfigurable[])((IConfigDataProvider)this).FindPaged<T>(filter, rootId, deepSearch, sortBy, int.MaxValue).ToArray<T>();
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00019800 File Offset: 0x00017A00
		IEnumerable<T> IConfigDataProvider.FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			FfoRecipientSession.LogUnsupportedQueryFilter(typeof(T), filter);
			foreach (T t in this.FindImpl<T>(filter, rootId, deepSearch, sortBy, pageSize))
			{
				ConfigurableObject configurableObject = t as ConfigurableObject;
				if (configurableObject != null)
				{
					configurableObject.ResetChangeTracking();
				}
				yield return t;
			}
			yield break;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00019842 File Offset: 0x00017A42
		void IRecipientSession.Save(ADRecipient instanceToSave, bool bypassValidation)
		{
			((IConfigDataProvider)this).Save(instanceToSave);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001984B File Offset: 0x00017A4B
		void IConfigDataProvider.Save(IConfigurable configurable)
		{
			if (this.useGenericInitialization)
			{
				throw new NotSupportedException("The Reduced RecipientSession should never be used to save an object");
			}
			base.FixOrganizationalUnitRoot(configurable);
			base.GenerateIdForObject(configurable);
			base.ApplyAuditProperties(configurable);
			base.DataProvider.Save(configurable);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00019881 File Offset: 0x00017A81
		void IConfigDataProvider.Delete(IConfigurable configurable)
		{
			if (this.useGenericInitialization)
			{
				throw new NotSupportedException("The Reduced RecipientSession should never be used to delete an object");
			}
			base.FixOrganizationalUnitRoot(configurable);
			base.GenerateIdForObject(configurable);
			base.ApplyAuditProperties(configurable);
			base.DataProvider.Delete(configurable);
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000198B7 File Offset: 0x00017AB7
		string IConfigDataProvider.Source
		{
			get
			{
				return "FfoRecipientSession";
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x000198BE File Offset: 0x00017ABE
		ADObjectId IRecipientSession.SearchRoot
		{
			get
			{
				FfoDirectorySession.LogNotSupportedInFFO(null);
				return null;
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000198C7 File Offset: 0x00017AC7
		ITableView IRecipientSession.Browse(ADObjectId addressListId, int rowCountSuggestion, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x000198D0 File Offset: 0x00017AD0
		void IRecipientSession.Delete(ADRecipient instanceToDelete)
		{
			((IConfigDataProvider)this).Delete(instanceToDelete);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x000198D9 File Offset: 0x00017AD9
		ADRecipient[] IRecipientSession.Find(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			return (ADRecipient[])((IConfigDataProvider)this).Find<ADRecipient>(filter, rootId, false, sortBy);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000198EB File Offset: 0x00017AEB
		ADRawEntry IRecipientSession.FindADRawEntryBySid(SecurityIdentifier sId, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000198F4 File Offset: 0x00017AF4
		ADRawEntry[] IRecipientSession.FindADRawEntryByUsnRange(ADObjectId root, long startUsn, long endUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties, QueryScope scope, QueryFilter additionalFilter)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADRawEntry[0];
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00019902 File Offset: 0x00017B02
		Result<ADRecipient>[] IRecipientSession.FindADRecipientsByLegacyExchangeDNs(string[] legacyExchangeDNs)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ADRecipient>[0];
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00019910 File Offset: 0x00017B10
		ADUser[] IRecipientSession.FindADUser(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			return (ADUser[])((IConfigDataProvider)this).Find<ADUser>(filter, rootId, false, sortBy);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00019924 File Offset: 0x00017B24
		ADUser IRecipientSession.FindADUserByObjectId(ADObjectId adObjectId)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, adObjectId);
			return ((IConfigDataProvider)this).Find<ADUser>(filter, null, false, null).Cast<ADUser>().FirstOrDefault<ADUser>();
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00019952 File Offset: 0x00017B52
		ADUser IRecipientSession.FindADUserByExternalDirectoryObjectId(string externalDirectoryObjectId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001995B File Offset: 0x00017B5B
		ADObject IRecipientSession.FindByAccountName<T>(string domainName, string accountName)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00019964 File Offset: 0x00017B64
		IEnumerable<T> IRecipientSession.FindByAccountName<T>(string domain, string account, ADObjectId rootId, QueryFilter searchFilter)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new T[0];
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00019972 File Offset: 0x00017B72
		ADRecipient[] IRecipientSession.FindByANR(string anrMatch, int maxResults, SortBy sortBy)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADRecipient[0];
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00019980 File Offset: 0x00017B80
		ADRawEntry[] IRecipientSession.FindByANR(string anrMatch, int maxResults, SortBy sortBy, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADRawEntry[0];
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001998E File Offset: 0x00017B8E
		ADRecipient IRecipientSession.FindByCertificate(X509Identifier identifier)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00019997 File Offset: 0x00017B97
		ADRawEntry[] IRecipientSession.FindByCertificate(X509Identifier identifier, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADRawEntry[0];
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000199A5 File Offset: 0x00017BA5
		public ADRecipient FindByExchangeObjectId(Guid exchangeObjectId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000199AE File Offset: 0x00017BAE
		ADRawEntry IRecipientSession.FindByExchangeGuid(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000199B7 File Offset: 0x00017BB7
		ADRecipient IRecipientSession.FindByExchangeGuid(Guid exchangeGuid)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000199C0 File Offset: 0x00017BC0
		TEntry IRecipientSession.FindByExchangeGuid<TEntry>(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return default(TEntry);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000199DC File Offset: 0x00017BDC
		ADRecipient IRecipientSession.FindByExchangeGuidIncludingAlternate(Guid exchangeGuid)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000199E5 File Offset: 0x00017BE5
		ADRawEntry IRecipientSession.FindByExchangeGuidIncludingAlternate(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000199F0 File Offset: 0x00017BF0
		TEntry IRecipientSession.FindByExchangeGuidIncludingAlternate<TEntry>(Guid exchangeGuid)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return default(TEntry);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00019A0C File Offset: 0x00017C0C
		ADRecipient IRecipientSession.FindByExchangeGuidIncludingArchive(Guid exchangeGuid)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00019A15 File Offset: 0x00017C15
		Result<ADRecipient>[] IRecipientSession.FindByExchangeGuidsIncludingArchive(Guid[] keys)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ADRecipient>[0];
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00019A23 File Offset: 0x00017C23
		ADRecipient IRecipientSession.FindByLegacyExchangeDN(string legacyExchangeDN)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00019A2C File Offset: 0x00017C2C
		Result<ADRawEntry>[] IRecipientSession.FindByLegacyExchangeDNs(string[] legacyExchangeDNs, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ADRawEntry>[0];
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00019A3A File Offset: 0x00017C3A
		ADRecipient IRecipientSession.FindByObjectGuid(Guid guid)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00019A43 File Offset: 0x00017C43
		ADRecipient IRecipientSession.FindByProxyAddress(ProxyAddress proxyAddress)
		{
			return ((IRecipientSession)this).FindByProxyAddress<ADRecipient>(proxyAddress);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00019A4C File Offset: 0x00017C4C
		ADRawEntry IRecipientSession.FindByProxyAddress(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties)
		{
			return ((IRecipientSession)this).FindByProxyAddress(proxyAddress);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00019A64 File Offset: 0x00017C64
		TEntry IRecipientSession.FindByProxyAddress<TEntry>(ProxyAddress proxyAddress)
		{
			Func<IConfigurable, string> func = null;
			IConfigurable[] array = ((IConfigDataProvider)this).Find<TEntry>(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, proxyAddress.AddressString), null, true, null);
			if (array == null || array.Length == 0)
			{
				return default(TEntry);
			}
			if (array.Length > 1)
			{
				string addressString = proxyAddress.AddressString;
				string separator = ",";
				IEnumerable<IConfigurable> source = array;
				if (func == null)
				{
					func = ((IConfigurable rcpt) => rcpt.Identity.ToString());
				}
				throw new AmbiguousMatchException(HygieneDataStrings.ErrorMultipleMatchForUserProxy(addressString, string.Join(separator, source.Select(func).ToArray<string>())));
			}
			return (TEntry)((object)array[0]);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00019AF9 File Offset: 0x00017CF9
		Result<ADRawEntry>[] IRecipientSession.FindByProxyAddresses(ProxyAddress[] proxyAddresses, params PropertyDefinition[] properties)
		{
			if (proxyAddresses == null)
			{
				return base.GetDefaultArray<Result<ADRawEntry>>();
			}
			return (from proxyAddress in proxyAddresses
			select new Result<ADRawEntry>(((IRecipientSession)this).FindByProxyAddress(proxyAddress), null)).ToArray<Result<ADRawEntry>>();
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00019B2B File Offset: 0x00017D2B
		Result<TEntry>[] IRecipientSession.FindByProxyAddresses<TEntry>(ProxyAddress[] proxyAddresses)
		{
			if (proxyAddresses == null)
			{
				return base.GetDefaultArray<Result<TEntry>>();
			}
			return (from proxyAddress in proxyAddresses
			select new Result<TEntry>(((IRecipientSession)this).FindByProxyAddress<TEntry>(proxyAddress), null)).ToArray<Result<TEntry>>();
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00019B5D File Offset: 0x00017D5D
		Result<ADRecipient>[] IRecipientSession.FindByProxyAddresses(ProxyAddress[] proxyAddresses)
		{
			if (proxyAddresses == null)
			{
				return base.GetDefaultArray<Result<ADRecipient>>();
			}
			return (from proxyAddress in proxyAddresses
			select new Result<ADRecipient>(((IRecipientSession)this).FindByProxyAddress(proxyAddress), null)).ToArray<Result<ADRecipient>>();
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00019B80 File Offset: 0x00017D80
		ADRecipient IRecipientSession.FindBySid(SecurityIdentifier sId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return base.GetDefaultObject<ADRecipient>();
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00019B8E File Offset: 0x00017D8E
		ADRawEntry[] IRecipientSession.FindDeletedADRawEntryByUsnRange(ADObjectId lastKnownParentId, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties, QueryFilter additionalFilter)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADRawEntry[0];
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00019B9C File Offset: 0x00017D9C
		MiniRecipient[] IRecipientSession.FindMiniRecipient(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			return (MiniRecipient[])((IConfigDataProvider)this).Find<MiniRecipient>(filter, rootId, false, sortBy);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00019BAE File Offset: 0x00017DAE
		MiniRecipient[] IRecipientSession.FindMiniRecipientByANR(string anrMatch, int maxResults, SortBy sortBy, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new MiniRecipient[0];
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00019BBC File Offset: 0x00017DBC
		TResult IRecipientSession.FindMiniRecipientByProxyAddress<TResult>(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return default(TResult);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00019BD8 File Offset: 0x00017DD8
		TResult IRecipientSession.FindMiniRecipientBySid<TResult>(SecurityIdentifier sId, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return default(TResult);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00019BF4 File Offset: 0x00017DF4
		ADRawEntry IRecipientSession.FindUserBySid(SecurityIdentifier sId, IList<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00019BFD File Offset: 0x00017DFD
		ADRecipient[] IRecipientSession.FindNames(IDictionary<PropertyDefinition, object> dictionary, int limit)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADRecipient[0];
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00019C0B File Offset: 0x00017E0B
		object[][] IRecipientSession.FindNamesView(IDictionary<PropertyDefinition, object> dictionary, int limit, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new object[0][];
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00019C19 File Offset: 0x00017E19
		Result<OWAMiniRecipient>[] IRecipientSession.FindOWAMiniRecipientByUserPrincipalName(string[] userPrincipalNames)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<OWAMiniRecipient>[0];
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00019C27 File Offset: 0x00017E27
		ADPagedReader<ADRecipient> IRecipientSession.FindPaged(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADPagedReader<ADRecipient>();
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00019C34 File Offset: 0x00017E34
		ADPagedReader<TEntry> IRecipientSession.FindPagedMiniRecipient<TEntry>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00019C3D File Offset: 0x00017E3D
		ADRawEntry[] IRecipientSession.FindRecipient(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADRawEntry[0];
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00019C4B File Offset: 0x00017E4B
		IEnumerable<ADGroup> IRecipientSession.FindRoleGroupsByForeignGroupSid(ADObjectId root, SecurityIdentifier sId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADGroup[0];
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00019C59 File Offset: 0x00017E59
		List<string> IRecipientSession.GetTokenSids(ADRawEntry user, AssignmentMethod assignmentMethodFlags)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new List<string>();
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00019C66 File Offset: 0x00017E66
		List<string> IRecipientSession.GetTokenSids(ADObjectId userId, AssignmentMethod assignmentMethodFlags)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new List<string>();
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00019C73 File Offset: 0x00017E73
		SecurityIdentifier IRecipientSession.GetWellKnownExchangeGroupSid(Guid wkguid)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00019C7C File Offset: 0x00017E7C
		bool IRecipientSession.IsLegacyDNInUse(string legacyDN)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return false;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00019C85 File Offset: 0x00017E85
		bool IRecipientSession.IsMemberOfGroupByWellKnownGuid(Guid wellKnownGuid, string containerDN, ADObjectId id)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return false;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00019C8E File Offset: 0x00017E8E
		bool IRecipientSession.IsRecipientInOrg(ProxyAddress proxyAddress)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return false;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00019C97 File Offset: 0x00017E97
		public bool IsReducedRecipientSession()
		{
			return this.useGenericInitialization;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00019C9F File Offset: 0x00017E9F
		bool IRecipientSession.IsThrottlingPolicyInUse(ADObjectId throttlingPolicyId)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return false;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00019CA8 File Offset: 0x00017EA8
		ADRecipient IRecipientSession.Read(ADObjectId entryId)
		{
			ADRecipient adrecipient = ((IConfigDataProvider)this).Read<ADRecipient>(entryId) as ADRecipient;
			if (adrecipient != null)
			{
				return this.FixRecipientProperties(adrecipient) as ADRecipient;
			}
			return null;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00019CD3 File Offset: 0x00017ED3
		MiniRecipient IRecipientSession.ReadMiniRecipient(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			return (MiniRecipient)((IConfigDataProvider)this).Read<MiniRecipient>(entryId);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00019CE1 File Offset: 0x00017EE1
		TMiniRecipient IRecipientSession.ReadMiniRecipient<TMiniRecipient>(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			return (TMiniRecipient)((object)((IConfigDataProvider)this).Read<MiniRecipient>(entryId));
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00019D03 File Offset: 0x00017F03
		Result<ADRecipient>[] IRecipientSession.ReadMultiple(ADObjectId[] entryIds)
		{
			return (from entryId in entryIds
			select new Result<ADRecipient>((ADRecipient)((IConfigDataProvider)this).Read<ADRecipient>(entryId), null)).ToArray<Result<ADRecipient>>();
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00019D1C File Offset: 0x00017F1C
		Result<ADRawEntry>[] IRecipientSession.ReadMultiple(ADObjectId[] entryIds, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ADRawEntry>[0];
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00019D2A File Offset: 0x00017F2A
		Result<ADGroup>[] IRecipientSession.ReadMultipleADGroups(ADObjectId[] entryIds)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ADGroup>[0];
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00019D38 File Offset: 0x00017F38
		Result<ADUser>[] IRecipientSession.ReadMultipleADUsers(ADObjectId[] userIds)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ADUser>[0];
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00019D46 File Offset: 0x00017F46
		Result<ADRawEntry>[] IRecipientSession.ReadMultipleWithDeletedObjects(ADObjectId[] entryIds, params PropertyDefinition[] properties)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new Result<ADRawEntry>[0];
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00019D54 File Offset: 0x00017F54
		MiniRecipientWithTokenGroups IRecipientSession.ReadTokenGroupsGlobalAndUniversal(ADObjectId id)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return null;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00019D5D File Offset: 0x00017F5D
		ADObjectId[] IRecipientSession.ResolveSidsToADObjectIds(string[] sids)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
			return new ADObjectId[0];
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00019D6B File Offset: 0x00017F6B
		void IRecipientSession.Save(ADRecipient instanceToSave)
		{
			((IConfigDataProvider)this).Save(instanceToSave);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00019D74 File Offset: 0x00017F74
		void IRecipientSession.SetPassword(ADObject obj, SecureString password)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00019D7C File Offset: 0x00017F7C
		void IRecipientSession.SetPassword(ADObjectId id, SecureString password)
		{
			FfoDirectorySession.LogNotSupportedInFFO(null);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00019D84 File Offset: 0x00017F84
		internal void EnableReducedRecipientSession()
		{
			this.useGenericInitialization = true;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00019D90 File Offset: 0x00017F90
		private static void LogUnsupportedQueryFilter(Type dataType, QueryFilter filter)
		{
			if (dataType == typeof(ADGroup))
			{
				string text = filter.ToString();
				if (text.Contains("RecipientTypeDetailsValue Equal RoleGroup") && !FfoRecipientSession.getRoleGroupQueryFilterRegex.IsMatch(text) && !FfoRecipientSession.updateRoleGroupMemberFilterRegex.IsMatch(text))
				{
					EventLogger.Logger.LogEvent(FfoHygineDataProviderEventLogConstants.Tuple_UnsupportedQueryFilter, null, new object[]
					{
						text + " \n" + Environment.StackTrace
					});
				}
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00019E14 File Offset: 0x00018014
		private static QueryFilter ReduceSecurityPrincipalFilter(QueryFilter filter)
		{
			if (filter.ToString() == FfoRecipientSession.userOrRoleGroupForExtendedSecurityPrincipal)
			{
				return null;
			}
			CompositeFilter compositeFilter = filter as CompositeFilter;
			if (compositeFilter == null)
			{
				return filter;
			}
			QueryFilter[] array = (from childFilter in compositeFilter.Filters.Select(new Func<QueryFilter, QueryFilter>(FfoRecipientSession.ReduceSecurityPrincipalFilter))
			where childFilter != null
			select childFilter).ToArray<QueryFilter>();
			if (array.Length == 0)
			{
				return null;
			}
			if (array.Length == 1)
			{
				return array[0];
			}
			if (compositeFilter is AndFilter)
			{
				return QueryFilter.AndTogether(array);
			}
			if (compositeFilter is OrFilter)
			{
				return QueryFilter.OrTogether(array);
			}
			throw new NotSupportedException(HygieneDataStrings.ErrorQueryFilterType(filter.ToString()));
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00019EC8 File Offset: 0x000180C8
		private IConfigurable ReadImpl<T>(ObjectId identity) where T : IConfigurable, new()
		{
			QueryFilter filter = base.AddTenantIdFilter(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, identity));
			if (typeof(T) == typeof(ExtendedSecurityPrincipal))
			{
				ADUser user = base.ReadAndHandleException<ADUser>(filter);
				return (T)((object)this.GetExtendedSecurityPrincipal(user));
			}
			T t = base.ReadAndHandleException<T>(filter);
			ADObject adobject = t as ADObject;
			if (adobject != null)
			{
				FfoDirectorySession.FixDistinguishedName(adobject, base.TenantId.DistinguishedName, base.TenantId.ObjectGuid, ((ADObjectId)adobject.Identity).ObjectGuid, null);
				return (T)((object)this.FixRecipientProperties(adobject));
			}
			return t;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001A378 File Offset: 0x00018578
		private IEnumerable<T> FindImpl<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			if (base.TenantId == null)
			{
				FfoDirectorySession.LogNotSupportedInFFO(null);
			}
			else
			{
				filter = this.AddFilterOperator(filter);
				if (typeof(T) == typeof(ExtendedSecurityPrincipal))
				{
					filter = FfoRecipientSession.ReduceSecurityPrincipalFilter(filter);
					IEnumerable<ADUser> users = base.FindAndHandleException<ADUser>(filter, rootId, deepSearch, sortBy, pageSize);
					foreach (ADUser user in users)
					{
						yield return (T)((object)this.GetExtendedSecurityPrincipal(user));
					}
				}
				else
				{
					IEnumerable<T> configObjs = base.FindAndHandleException<T>(base.AddTenantIdFilter(filter), rootId, deepSearch, sortBy, pageSize);
					foreach (T configObj in configObjs)
					{
						ADObject adObject = configObj as ADObject;
						if (adObject != null)
						{
							FfoDirectorySession.FixDistinguishedName(adObject, base.TenantId.DistinguishedName, base.TenantId.ObjectGuid, ((ADObjectId)adObject.Identity).ObjectGuid, null);
							yield return (T)((object)this.FixRecipientProperties(adObject));
						}
						else
						{
							yield return configObj;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001A3BC File Offset: 0x000185BC
		private QueryFilter AddFilterOperator(QueryFilter filter)
		{
			if (filter == null)
			{
				return null;
			}
			string input = filter.ToString();
			Match match = FfoRecipientSession.getRecipientFilterRegex.Match(input);
			if (match.Success)
			{
				Group group = match.Groups[DalHelper.FilteringOperatorProp.Name];
				if (group != null && group.Value == "&")
				{
					filter = QueryFilter.AndTogether(new QueryFilter[]
					{
						filter,
						new ComparisonFilter(ComparisonOperator.Equal, DalHelper.FilteringOperatorProp, "and")
					});
				}
			}
			return filter;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001A43C File Offset: 0x0001863C
		private IConfigurable GetExtendedSecurityPrincipal(ADUser user)
		{
			if (user == null)
			{
				return null;
			}
			FfoDirectorySession.FixDistinguishedName(user, base.TenantId.DistinguishedName, base.TenantId.ObjectGuid, ((ADObjectId)user.Identity).ObjectGuid, null);
			ExtendedSecurityPrincipal extendedSecurityPrincipal = new ExtendedSecurityPrincipal();
			DalHelper.SetConfigurableObject(user.DisplayName, ExtendedSecurityPrincipalSchema.DisplayName, extendedSecurityPrincipal);
			DalHelper.SetConfigurableObject(user.DisplayName, ADObjectSchema.RawName, extendedSecurityPrincipal);
			DalHelper.SetConfigurableObject(new SecurityIdentifier(WellKnownSidType.NullSid, null), IADSecurityPrincipalSchema.Sid, extendedSecurityPrincipal);
			DalHelper.SetConfigurableObject(user.Id.GetChildId(ADUser.ObjectCategoryNameInternal), ADObjectSchema.ObjectCategory, extendedSecurityPrincipal);
			DalHelper.SetConfigurableObject(user.ObjectClass, ADObjectSchema.ObjectClass, extendedSecurityPrincipal);
			DalHelper.SetConfigurableObject(RecipientTypeDetails.MailUser, ExtendedSecurityPrincipalSchema.RecipientTypeDetails, extendedSecurityPrincipal);
			extendedSecurityPrincipal.SetId(user.Id);
			return extendedSecurityPrincipal;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001A518 File Offset: 0x00018718
		private ADObject FixRecipientProperties(ADObject adObject)
		{
			adObject = this.FixObjectType(adObject);
			adObject.SetIsReadOnly(false);
			adObject[IADMailStorageSchema.ProhibitSendQuota] = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
			this.FixGroupProperties(adObject as ADGroup);
			ADRecipient adrecipient = adObject as ADRecipient;
			if (adrecipient != null && (adrecipient is ADUser || adrecipient is ADContact))
			{
				if (!(adrecipient.ExternalEmailAddress == null))
				{
					if (adrecipient.EmailAddresses.Any((ProxyAddress proxy) => proxy.Prefix == ProxyAddressPrefix.Smtp))
					{
						goto IL_8A;
					}
				}
				adrecipient.Alias = null;
			}
			IL_8A:
			adObject.SetIsReadOnly(((IDirectorySession)this).ReadOnly);
			if (adObject is ADRecipient || adObject is MiniRecipient)
			{
				adObject.m_Session = (adObject.m_Session ?? this);
			}
			return adObject;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001A5E0 File Offset: 0x000187E0
		private ADObject FixObjectType(ADObject adObject)
		{
			if (adObject == null)
			{
				throw new ArgumentNullException("The recipient should not be post processed if null.", "recipient");
			}
			Type type = adObject.GetType();
			string key = adObject.ObjectClass.First<string>();
			Type type2;
			if (FfoRecipientSession.objectClassTypes.TryGetValue(key, out type2) && type.IsAssignableFrom(type2))
			{
				adObject = (ADObject)Activator.CreateInstance(type2, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[]
				{
					this,
					adObject.propertyBag
				}, null);
			}
			return adObject;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001A658 File Offset: 0x00018858
		private void FixGroupProperties(ADGroup group)
		{
			if (group == null)
			{
				return;
			}
			RoleGroup.RoleGroupTypeIds roleGroupTypeIds;
			if (group.RawCapabilities.Contains(Capability.Partner_Managed))
			{
				if (FfoRecipientSession.partnerManagedRoleGroups.TryGetValue(group.Name, out roleGroupTypeIds))
				{
					group[ADGroupSchema.RoleGroupTypeId] = (int)roleGroupTypeIds;
				}
			}
			else if (Enum.TryParse<RoleGroup.RoleGroupTypeIds>(group.Name, out roleGroupTypeIds))
			{
				group[ADGroupSchema.RoleGroupTypeId] = (int)roleGroupTypeIds;
			}
			ADObjectId organizationalUnitRoot = group.OrganizationalUnitRoot;
			MultiValuedProperty<ADObjectId> multiValuedProperty = group[ADGroupSchema.RoleAssignments] as MultiValuedProperty<ADObjectId>;
			if (multiValuedProperty != null && organizationalUnitRoot != null)
			{
				for (int i = 0; i < multiValuedProperty.Count; i++)
				{
					ADObjectId adobjectId = multiValuedProperty[i];
					adobjectId = new ADObjectId(organizationalUnitRoot.GetChildId(adobjectId.Name).DistinguishedName, adobjectId.ObjectGuid);
					multiValuedProperty[i] = adobjectId;
				}
			}
		}

		// Token: 0x04000466 RID: 1126
		private static readonly Dictionary<string, RoleGroup.RoleGroupTypeIds> partnerManagedRoleGroups = new Dictionary<string, RoleGroup.RoleGroupTypeIds>
		{
			{
				"HelpdeskAdmins",
				RoleGroup.RoleGroupTypeIds.MsoManagedTenantHelpdesk
			},
			{
				"TenantAdmins",
				RoleGroup.RoleGroupTypeIds.MsoManagedTenantAdmin
			}
		};

		// Token: 0x04000467 RID: 1127
		private static readonly Regex getRoleGroupQueryFilterRegex = new Regex("\\(\\&\\(\\(RoleGroupType NotEqual PartnerLinked\\)\\(\\&\\(\\(\\&\\(\\(RecipientTypeDetails Equal RoleGroup\\)\\(OrganizationalUnitRoot Equal .+\\)\\)\\)\\(\\&\\(\\(RecipientType Equal Group\\)\\(RecipientTypeDetailsValue Equal RoleGroup\\)\\(BitwiseAnd\\(GroupType,2147483656\\)\\)\\)\\)\\)\\)\\)\\)");

		// Token: 0x04000468 RID: 1128
		private static readonly Regex updateRoleGroupMemberFilterRegex = new Regex("\\(\\&\\(\\(\\&\\(\\(\\|\\(\\(ExternalDirectoryObjectId Equal .+\\)\\(UserPrincipalName Equal .+\\)\\(LegacyExchangeDN Equal .+\\)\\(EmailAddresses Equal .+\\)\\(Alias Equal .+\\)\\(DisplayName Equal .+\\)\\)\\)\\(RecipientType Equal Group\\)\\)\\)\\(\\&\\(\\(RecipientType Equal Group\\)\\(RecipientTypeDetailsValue Equal RoleGroup\\)\\(BitwiseAnd\\(GroupType,2147483656\\)\\)\\)\\)\\)\\)");

		// Token: 0x04000469 RID: 1129
		private static readonly Regex getRecipientFilterRegex = new Regex(string.Format("\\(\\&\\(\\(\\&\\(\\(\\|\\((\\(RecipientTypeDetails Equal [^\\)]+\\))+\\)\\)\\(OrganizationalUnitRoot Equal [^\\)]+\\)(\\((?<{0}>[^\\(]+)\\((\\([^\\)]+\\))+\\)\\))?\\)\\)\\(\\&\\(\\(pageCookie Equal \\)\\(storedProcOutputBag Equal System.Collections.Generic.Dictionary`2\\[Microsoft.Exchange.Data.PropertyDefinition\\,System.Object\\]\\)\\)\\)\\)\\)", DalHelper.FilteringOperatorProp.Name));

		// Token: 0x0400046A RID: 1130
		private static readonly string userOrRoleGroupForExtendedSecurityPrincipal = "(|((&((&((ObjectCategory Equal group)(BitwiseOr(GroupType,2147483648))(BitwiseAnd(GroupType,8))))(|((!((BitwiseAnd(GroupType,8))))(&((BitwiseAnd(GroupType,8))(|((Exists(Alias))(&((RecipientType Equal Group)(RecipientTypeDetailsValue Equal RoleGroup)(BitwiseAnd(GroupType,2147483656))))))))))))(&((ObjectCategory Equal person)(ObjectClass Equal user)))))";

		// Token: 0x0400046B RID: 1131
		private static readonly Dictionary<string, Type> objectClassTypes = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"group",
				typeof(ADGroup)
			},
			{
				"user",
				typeof(ADUser)
			},
			{
				"contact",
				typeof(ADContact)
			}
		};

		// Token: 0x0400046C RID: 1132
		private bool useGenericInitialization;
	}
}
