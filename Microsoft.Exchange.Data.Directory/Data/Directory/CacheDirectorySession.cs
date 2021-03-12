using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Cache;
using Microsoft.Exchange.Data.Directory.Diagnostics;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.DirectoryCache;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000093 RID: 147
	internal class CacheDirectorySession : ITenantConfigurationSession, IConfigurationSession, ITenantRecipientSession, IRecipientSession, IDirectorySession, IConfigDataProvider, ICacheDirectorySession
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x00025DDC File Offset: 0x00023FDC
		public CacheDirectorySession(ADSessionSettings sessionSettings)
		{
			ArgumentValidator.ThrowIfNull("sessionSettings", sessionSettings);
			this.sessionSettings = sessionSettings;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00025E01 File Offset: 0x00024001
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x00025E09 File Offset: 0x00024009
		public ADCacheResultState ResultState { get; private set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00025E12 File Offset: 0x00024012
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x00025E1A File Offset: 0x0002401A
		public bool IsNewProxyObject { get; private set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00025E23 File Offset: 0x00024023
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00025E2B File Offset: 0x0002402B
		public int RetryCount { get; private set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00025E34 File Offset: 0x00024034
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x00025E3B File Offset: 0x0002403B
		TimeSpan? IDirectorySession.ClientSideSearchTimeout
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00025E42 File Offset: 0x00024042
		ConfigScopes IDirectorySession.ConfigScope
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00025E49 File Offset: 0x00024049
		ConsistencyMode IDirectorySession.ConsistencyMode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00025E50 File Offset: 0x00024050
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x00025E57 File Offset: 0x00024057
		string IDirectorySession.DomainController
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00025E5E File Offset: 0x0002405E
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x00025E65 File Offset: 0x00024065
		bool IDirectorySession.EnforceContainerizedScoping
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00025E6C File Offset: 0x0002406C
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00025E73 File Offset: 0x00024073
		bool IDirectorySession.EnforceDefaultScope
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00025E7A File Offset: 0x0002407A
		string IDirectorySession.LastUsedDc
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00025E81 File Offset: 0x00024081
		int IDirectorySession.Lcid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00025E88 File Offset: 0x00024088
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x00025E8F File Offset: 0x0002408F
		string IDirectorySession.LinkResolutionServer
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00025E96 File Offset: 0x00024096
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x00025E9D File Offset: 0x0002409D
		bool IDirectorySession.LogSizeLimitExceededEvent
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00025EA4 File Offset: 0x000240A4
		NetworkCredential IDirectorySession.NetworkCredential
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00025EAB File Offset: 0x000240AB
		bool IDirectorySession.ReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00025EB2 File Offset: 0x000240B2
		ADServerSettings IDirectorySession.ServerSettings
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00025EB9 File Offset: 0x000240B9
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x00025EC0 File Offset: 0x000240C0
		TimeSpan? IDirectorySession.ServerTimeout
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00025EC7 File Offset: 0x000240C7
		ADSessionSettings IDirectorySession.SessionSettings
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00025ECE File Offset: 0x000240CE
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x00025ED5 File Offset: 0x000240D5
		bool IDirectorySession.SkipRangedAttributes
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00025EDC File Offset: 0x000240DC
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x00025EE3 File Offset: 0x000240E3
		public string[] ExclusiveLdapAttributes
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00025EEA File Offset: 0x000240EA
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x00025EF1 File Offset: 0x000240F1
		bool IDirectorySession.UseConfigNC
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00025EF8 File Offset: 0x000240F8
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x00025EFF File Offset: 0x000240FF
		bool IDirectorySession.UseGlobalCatalog
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00025F06 File Offset: 0x00024106
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x00025F13 File Offset: 0x00024113
		public IActivityScope ActivityScope
		{
			get
			{
				return this.logContext.ActivityScope;
			}
			set
			{
				this.logContext.ActivityScope = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00025F21 File Offset: 0x00024121
		public string CallerInfo
		{
			get
			{
				return this.logContext.GetCallerInformation();
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00025F2E File Offset: 0x0002412E
		void IDirectorySession.AnalyzeDirectoryError(PooledLdapConnection connection, DirectoryRequest request, DirectoryException de, int totalRetries, int retriesOnServer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00025F35 File Offset: 0x00024135
		QueryFilter IDirectorySession.ApplyDefaultFilters(QueryFilter filter, ADObjectId rootId, ADObject dummyObject, bool applyImplicitFilter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00025F3C File Offset: 0x0002413C
		QueryFilter IDirectorySession.ApplyDefaultFilters(QueryFilter filter, ADScope scope, ADObject dummyObject, bool applyImplicitFilter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00025F43 File Offset: 0x00024143
		void IDirectorySession.CheckFilterForUnsafeIdentity(QueryFilter filter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00025F4A File Offset: 0x0002414A
		void IDirectorySession.UnsafeExecuteModificationRequest(DirectoryRequest request, ADObjectId rootId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00025F51 File Offset: 0x00024151
		ADRawEntry[] IDirectorySession.Find(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00025F58 File Offset: 0x00024158
		TResult[] IDirectorySession.Find<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00025F5F File Offset: 0x0002415F
		ADRawEntry[] IDirectorySession.FindAllADRawEntriesByUsnRange(ADObjectId root, long startUsn, long endUsn, int sizeLimit, bool useAtomicFilter, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00025F66 File Offset: 0x00024166
		Result<ADRawEntry>[] IDirectorySession.FindByADObjectIds(ADObjectId[] ids, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00025F6D File Offset: 0x0002416D
		Result<TData>[] IDirectorySession.FindByADObjectIds<TData>(ADObjectId[] ids)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00025F74 File Offset: 0x00024174
		Result<ADRawEntry>[] IDirectorySession.FindByCorrelationIds(Guid[] correlationIds, ADObjectId configUnit, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00025F7B File Offset: 0x0002417B
		Result<ADRawEntry>[] IDirectorySession.FindByExchangeLegacyDNs(string[] exchangeLegacyDNs, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00025F82 File Offset: 0x00024182
		Result<ADRawEntry>[] IDirectorySession.FindByObjectGuids(Guid[] objectGuids, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00025F89 File Offset: 0x00024189
		ADRawEntry[] IDirectorySession.FindDeletedTenantSyncObjectByUsnRange(ADObjectId tenantOuRoot, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00025F90 File Offset: 0x00024190
		ADPagedReader<TResult> IDirectorySession.FindPaged<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00025F97 File Offset: 0x00024197
		ADPagedReader<ADRawEntry> IDirectorySession.FindPagedADRawEntry(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00025F9E File Offset: 0x0002419E
		ADPagedReader<ADRawEntry> IDirectorySession.FindPagedADRawEntryWithDefaultFilters<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00025FA5 File Offset: 0x000241A5
		ADPagedReader<TResult> IDirectorySession.FindPagedDeletedObject<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00025FAC File Offset: 0x000241AC
		ADObjectId IDirectorySession.GetConfigurationNamingContext()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00025FB3 File Offset: 0x000241B3
		ADObjectId IDirectorySession.GetConfigurationUnitsRoot()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00025FBA File Offset: 0x000241BA
		ADObjectId IDirectorySession.GetDomainNamingContext()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00025FC1 File Offset: 0x000241C1
		ADObjectId IDirectorySession.GetHostedOrganizationsRoot()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00025FC8 File Offset: 0x000241C8
		ADObjectId IDirectorySession.GetRootDomainNamingContext()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00025FCF File Offset: 0x000241CF
		ADObjectId IDirectorySession.GetSchemaNamingContext()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00025FD6 File Offset: 0x000241D6
		PooledLdapConnection IDirectorySession.GetReadConnection(string preferredServer, ref ADObjectId rootId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00025FDD File Offset: 0x000241DD
		PooledLdapConnection IDirectorySession.GetReadConnection(string preferredServer, string optionalBaseDN, ref ADObjectId rootId, ADRawEntry scopeDeteriminingObject)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00025FE4 File Offset: 0x000241E4
		ADScope IDirectorySession.GetReadScope(ADObjectId rootId, ADRawEntry scopeDeterminingObject)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00025FEB File Offset: 0x000241EB
		ADScope IDirectorySession.GetReadScope(ADObjectId rootId, ADRawEntry scopeDeterminingObject, bool isWellKnownGuidSearch, out ConfigScopes applicableScope)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00025FF2 File Offset: 0x000241F2
		bool IDirectorySession.GetSchemaAndApplyFilter(ADRawEntry adRawEntry, ADScope scope, out ADObject dummyObject, out string[] ldapAttributes, ref QueryFilter filter, ref IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00025FF9 File Offset: 0x000241F9
		bool IDirectorySession.IsReadConnectionAvailable()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00026000 File Offset: 0x00024200
		bool IDirectorySession.IsRootIdWithinScope<TObject>(ADObjectId rootId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00026007 File Offset: 0x00024207
		bool IDirectorySession.IsTenantIdentity(ADObjectId id)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00026010 File Offset: 0x00024210
		ADRawEntry IDirectorySession.ReadADRawEntry(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			if (properties == null)
			{
				return null;
			}
			PropertyDefinition[] second = new PropertyDefinition[]
			{
				ADObjectSchema.Id
			};
			return this.InternalGet<ADRawEntry>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromADObjectId(entryId), ObjectType.ADRawEntry, properties.Concat(second)));
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00026060 File Offset: 0x00024260
		RawSecurityDescriptor IDirectorySession.ReadSecurityDescriptor(ADObjectId id)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00026067 File Offset: 0x00024267
		SecurityDescriptor IDirectorySession.ReadSecurityDescriptorBlob(ADObjectId id)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0002606E File Offset: 0x0002426E
		string[] IDirectorySession.ReplicateSingleObject(ADObject instanceToReplicate, ADObjectId[] sites)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00026075 File Offset: 0x00024275
		bool IDirectorySession.ReplicateSingleObjectToTargetDC(ADObject instanceToReplicate, string targetServerName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0002607C File Offset: 0x0002427C
		TResult IDirectorySession.ResolveWellKnownGuid<TResult>(Guid wellKnownGuid, ADObjectId containerId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00026083 File Offset: 0x00024283
		TResult IDirectorySession.ResolveWellKnownGuid<TResult>(Guid wellKnownGuid, string containerDN)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0002608A File Offset: 0x0002428A
		TenantRelocationSyncObject IDirectorySession.RetrieveTenantRelocationSyncObject(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00026091 File Offset: 0x00024291
		ADOperationResultWithData<TResult>[] IDirectorySession.RunAgainstAllDCsInSite<TResult>(ADObjectId siteId, Func<TResult> methodToCall)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00026098 File Offset: 0x00024298
		void IDirectorySession.SaveSecurityDescriptor(ADObjectId id, RawSecurityDescriptor sd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0002609F File Offset: 0x0002429F
		void IDirectorySession.SaveSecurityDescriptor(ADObjectId id, RawSecurityDescriptor sd, bool modifyOwner)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000260A6 File Offset: 0x000242A6
		void IDirectorySession.SaveSecurityDescriptor(ADObject obj, RawSecurityDescriptor sd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000260AD File Offset: 0x000242AD
		void IDirectorySession.SaveSecurityDescriptor(ADObject obj, RawSecurityDescriptor sd, bool modifyOwner)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x000260B4 File Offset: 0x000242B4
		bool IDirectorySession.TryVerifyIsWithinScopes(ADObject entry, bool isModification, out ADScopeException exception)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000260BB File Offset: 0x000242BB
		void IDirectorySession.UpdateServerSettings(PooledLdapConnection connection)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000260C2 File Offset: 0x000242C2
		void IDirectorySession.VerifyIsWithinScopes(ADObject entry, bool isModification)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000260C9 File Offset: 0x000242C9
		TResult[] IDirectorySession.ObjectsFromEntries<TResult>(SearchResultEntryCollection entries, string originatingServerName, IEnumerable<PropertyDefinition> properties, ADRawEntry dummyInstance)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x000260D0 File Offset: 0x000242D0
		ADObjectId IConfigurationSession.ConfigurationNamingContext
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x000260D7 File Offset: 0x000242D7
		ADObjectId IConfigurationSession.DeletedObjectsContainer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x000260DE File Offset: 0x000242DE
		ADObjectId IConfigurationSession.SchemaNamingContext
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x000260E5 File Offset: 0x000242E5
		bool IConfigurationSession.CheckForRetentionPolicyWithConflictingRetentionId(Guid retentionId, out string duplicateName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x000260EC File Offset: 0x000242EC
		bool IConfigurationSession.CheckForRetentionPolicyWithConflictingRetentionId(Guid retentionId, string identity, out string duplicateName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000260F3 File Offset: 0x000242F3
		bool IConfigurationSession.CheckForRetentionTagWithConflictingRetentionId(Guid retentionId, out string duplicateName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000260FA File Offset: 0x000242FA
		bool IConfigurationSession.CheckForRetentionTagWithConflictingRetentionId(Guid retentionId, string identity, out string duplicateName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00026101 File Offset: 0x00024301
		void IConfigurationSession.DeleteTree(ADConfigurationObject instanceToDelete, TreeDeleteNotFinishedHandler handler)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00026108 File Offset: 0x00024308
		AcceptedDomain[] IConfigurationSession.FindAcceptedDomainsByFederatedOrgId(FederatedOrganizationId federatedOrganizationId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0002610F File Offset: 0x0002430F
		ADPagedReader<TResult> IConfigurationSession.FindAllPaged<TResult>()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00026116 File Offset: 0x00024316
		ExchangeRoleAssignment[] IConfigurationSession.FindAssignmentsForManagementScope(ManagementScope managementScope, bool returnAll)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0002611D File Offset: 0x0002431D
		T IConfigurationSession.FindMailboxPolicyByName<T>(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00026124 File Offset: 0x00024324
		MicrosoftExchangeRecipient IConfigurationSession.FindMicrosoftExchangeRecipient()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0002612B File Offset: 0x0002432B
		OfflineAddressBook[] IConfigurationSession.FindOABsForWebDistributionPoint(ADOabVirtualDirectory vDir)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00026132 File Offset: 0x00024332
		ThrottlingPolicy[] IConfigurationSession.FindOrganizationThrottlingPolicies(OrganizationId organizationId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00026139 File Offset: 0x00024339
		ADPagedReader<TResult> IConfigurationSession.FindPaged<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00026140 File Offset: 0x00024340
		Result<ExchangeRoleAssignment>[] IConfigurationSession.FindRoleAssignmentsByUserIds(ADObjectId[] securityPrincipalIds, bool partnerMode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00026147 File Offset: 0x00024347
		Result<ExchangeRoleAssignment>[] IConfigurationSession.FindRoleAssignmentsByUserIds(ADObjectId[] securityPrincipalIds, QueryFilter additionalFilter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0002614E File Offset: 0x0002434E
		ManagementScope[] IConfigurationSession.FindSimilarManagementScope(ManagementScope managementScope)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00026155 File Offset: 0x00024355
		T IConfigurationSession.FindSingletonConfigurationObject<T>()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0002615C File Offset: 0x0002435C
		AcceptedDomain IConfigurationSession.GetAcceptedDomainByDomainName(string domainName)
		{
			return this.InternalGet<AcceptedDomain>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, domainName, KeyType.Name, ObjectType.AcceptedDomain, null));
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0002617D File Offset: 0x0002437D
		ADPagedReader<ManagementScope> IConfigurationSession.GetAllExclusiveScopes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00026184 File Offset: 0x00024384
		ADPagedReader<ManagementScope> IConfigurationSession.GetAllScopes(OrganizationId organizationId, ScopeRestrictionType restrictionType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0002618B File Offset: 0x0002438B
		AvailabilityAddressSpace IConfigurationSession.GetAvailabilityAddressSpace(string domainName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00026192 File Offset: 0x00024392
		AvailabilityConfig IConfigurationSession.GetAvailabilityConfig()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00026199 File Offset: 0x00024399
		AcceptedDomain IConfigurationSession.GetDefaultAcceptedDomain()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000261A0 File Offset: 0x000243A0
		ExchangeConfigurationContainer IConfigurationSession.GetExchangeConfigurationContainer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000261A7 File Offset: 0x000243A7
		ExchangeConfigurationContainerWithAddressLists IConfigurationSession.GetExchangeConfigurationContainerWithAddressLists()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000261AE File Offset: 0x000243AE
		FederatedOrganizationId IConfigurationSession.GetFederatedOrganizationId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000261B5 File Offset: 0x000243B5
		FederatedOrganizationId IConfigurationSession.GetFederatedOrganizationId(OrganizationId organizationId)
		{
			return this.InternalGet<FederatedOrganizationId>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, organizationId.ConfigurationUnit.DistinguishedName, KeyType.OrgCUDN, ObjectType.FederatedOrganizationId, null));
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000261E4 File Offset: 0x000243E4
		FederatedOrganizationId IConfigurationSession.GetFederatedOrganizationIdByDomainName(string domainName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000261EB File Offset: 0x000243EB
		NspiRpcClientConnection IConfigurationSession.GetNspiRpcClientConnection()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000261F2 File Offset: 0x000243F2
		ThrottlingPolicy IConfigurationSession.GetOrganizationThrottlingPolicy(OrganizationId organizationId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x000261F9 File Offset: 0x000243F9
		ThrottlingPolicy IConfigurationSession.GetOrganizationThrottlingPolicy(OrganizationId organizationId, bool logFailedLookup)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00026200 File Offset: 0x00024400
		Organization IConfigurationSession.GetOrgContainer()
		{
			if (null == this.sessionSettings.CurrentOrganizationId || OrganizationId.ForestWideOrgId.Equals(this.sessionSettings.CurrentOrganizationId))
			{
				return null;
			}
			return this.InternalGet<ExchangeConfigurationUnit>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromADObjectId(this.sessionSettings.CurrentOrganizationId.ConfigurationUnit), ObjectType.ExchangeConfigurationUnit, null));
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0002626B File Offset: 0x0002446B
		OrganizationRelationship IConfigurationSession.GetOrganizationRelationship(string domainName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00026272 File Offset: 0x00024472
		ADObjectId IConfigurationSession.GetOrgContainerId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00026279 File Offset: 0x00024479
		RbacContainer IConfigurationSession.GetRbacContainer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00026280 File Offset: 0x00024480
		bool IConfigurationSession.ManagementScopeIsInUse(ManagementScope managementScope)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00026287 File Offset: 0x00024487
		public TResult FindByExchangeObjectId<TResult>(Guid exchangeObjectId) where TResult : ADConfigurationObject, new()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00026290 File Offset: 0x00024490
		TResult IConfigurationSession.Read<TResult>(ADObjectId entryId)
		{
			ObjectType objectTypeFor = CacheUtils.GetObjectTypeFor(typeof(TResult), false);
			if (objectTypeFor == ObjectType.Unknown)
			{
				return default(TResult);
			}
			return this.InternalGet<TResult>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromADObjectId(entryId), objectTypeFor, null));
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000262DE File Offset: 0x000244DE
		Result<TResult>[] IConfigurationSession.ReadMultiple<TResult>(ADObjectId[] identities)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000262E5 File Offset: 0x000244E5
		MultiValuedProperty<ReplicationCursor> IConfigurationSession.ReadReplicationCursors(ADObjectId id)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000262EC File Offset: 0x000244EC
		void IConfigurationSession.ReadReplicationData(ADObjectId id, out MultiValuedProperty<ReplicationCursor> replicationCursors, out MultiValuedProperty<ReplicationNeighbor> repsFrom)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000262F3 File Offset: 0x000244F3
		void IConfigurationSession.Save(ADConfigurationObject instanceToSave)
		{
			this.InternalSave(instanceToSave, null, null, 2147483646, CacheItemPriority.Default);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00026304 File Offset: 0x00024504
		void IConfigDataProvider.Delete(IConfigurable instance)
		{
			this.InternalDelete((ADRawEntry)instance);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00026312 File Offset: 0x00024512
		IConfigurable[] IConfigDataProvider.Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00026319 File Offset: 0x00024519
		IEnumerable<T> IConfigDataProvider.FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00026320 File Offset: 0x00024520
		IConfigurable IConfigDataProvider.Read<T>(ObjectId identity)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00026327 File Offset: 0x00024527
		void IConfigDataProvider.Save(IConfigurable instance)
		{
			this.InternalSave((ADRawEntry)instance, null, null, 2147483646, CacheItemPriority.Default);
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x0002633D File Offset: 0x0002453D
		string IConfigDataProvider.Source
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00026344 File Offset: 0x00024544
		AcceptedDomain[] ITenantConfigurationSession.FindAllAcceptedDomainsInOrg(ADObjectId organizationCU)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0002634B File Offset: 0x0002454B
		ExchangeConfigurationUnit[] ITenantConfigurationSession.FindAllOpenConfigurationUnits(bool excludeFull)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00026352 File Offset: 0x00024552
		ExchangeConfigurationUnit[] ITenantConfigurationSession.FindSharedConfiguration(SharedConfigurationInfo sharedConfigInfo, bool enabledSharedOrgOnly)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00026359 File Offset: 0x00024559
		ExchangeConfigurationUnit[] ITenantConfigurationSession.FindSharedConfigurationByOrganizationId(OrganizationId tinyTenantId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00026360 File Offset: 0x00024560
		ADRawEntry[] ITenantConfigurationSession.FindDeletedADRawEntryByUsnRange(ADObjectId lastKnownParentId, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00026367 File Offset: 0x00024567
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByExternalId(string externalId)
		{
			return this.InternalGet<ExchangeConfigurationUnit>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, externalId, KeyType.ExternalDirectoryOrganizationId, ObjectType.ExchangeConfigurationUnit, null));
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00026388 File Offset: 0x00024588
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByExternalId(Guid externalDirectoryOrganizationId)
		{
			return this.InternalGet<ExchangeConfigurationUnit>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, externalDirectoryOrganizationId.ToString(), KeyType.ExternalDirectoryOrganizationId, ObjectType.ExchangeConfigurationUnit, null));
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000263B5 File Offset: 0x000245B5
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByName(string organizationName)
		{
			return this.InternalGet<ExchangeConfigurationUnit>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, organizationName.ToString(), KeyType.Name, ObjectType.ExchangeConfigurationUnit, null));
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000263DB File Offset: 0x000245DB
		ADObjectId ITenantConfigurationSession.GetExchangeConfigurationUnitIdByName(string organizationName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000263E2 File Offset: 0x000245E2
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByNameOrAcceptedDomain(string organizationName)
		{
			return this.InternalGet<ExchangeConfigurationUnit>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, organizationName.ToString(), KeyType.Name | KeyType.DomainName, ObjectType.ExchangeConfigurationUnit, null));
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00026409 File Offset: 0x00024609
		ExchangeConfigurationUnit ITenantConfigurationSession.GetExchangeConfigurationUnitByUserNetID(string userNetID)
		{
			return this.InternalGet<ExchangeConfigurationUnit>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, userNetID, KeyType.NetId, ObjectType.ExchangeConfigurationUnit, null));
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0002642E File Offset: 0x0002462E
		OrganizationId ITenantConfigurationSession.GetOrganizationIdFromOrgNameOrAcceptedDomain(string domainName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00026435 File Offset: 0x00024635
		OrganizationId ITenantConfigurationSession.GetOrganizationIdFromExternalDirectoryOrgId(Guid externalDirectoryOrgId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0002643C File Offset: 0x0002463C
		MsoTenantCookieContainer ITenantConfigurationSession.GetMsoTenantCookieContainer(Guid contextId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00026443 File Offset: 0x00024643
		Result<ADRawEntry>[] ITenantConfigurationSession.ReadMultipleOrganizationProperties(ADObjectId[] organizationOUIds, PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0002644A File Offset: 0x0002464A
		T ITenantConfigurationSession.GetDefaultFilteringConfiguration<T>()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00026451 File Offset: 0x00024651
		public bool IsTenantLockedOut()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00026458 File Offset: 0x00024658
		ADObjectId IRecipientSession.SearchRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0002645F File Offset: 0x0002465F
		ITableView IRecipientSession.Browse(ADObjectId addressListId, int rowCountSuggestion, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00026466 File Offset: 0x00024666
		void IRecipientSession.Delete(ADRecipient instanceToDelete)
		{
			this.InternalDelete(instanceToDelete);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0002646F File Offset: 0x0002466F
		ADRecipient[] IRecipientSession.Find(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00026476 File Offset: 0x00024676
		ADRawEntry IRecipientSession.FindADRawEntryBySid(SecurityIdentifier sId, IEnumerable<PropertyDefinition> properties)
		{
			return this.InternalGet<ADRawEntry>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromSid(sId), ObjectType.ADRawEntry, properties));
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0002649F File Offset: 0x0002469F
		ADRawEntry[] IRecipientSession.FindADRawEntryByUsnRange(ADObjectId root, long startUsn, long endUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties, QueryScope scope, QueryFilter additionalFilter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000264A6 File Offset: 0x000246A6
		Result<ADRecipient>[] IRecipientSession.FindADRecipientsByLegacyExchangeDNs(string[] legacyExchangeDNs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x000264AD File Offset: 0x000246AD
		ADUser[] IRecipientSession.FindADUser(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000264B4 File Offset: 0x000246B4
		ADUser IRecipientSession.FindADUserByObjectId(ADObjectId adObjectId)
		{
			return this.InternalGet<ADUser>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromADObjectId(adObjectId), ObjectType.Recipient, null));
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000264D9 File Offset: 0x000246D9
		ADUser IRecipientSession.FindADUserByExternalDirectoryObjectId(string externalDirectoryObjectId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000264E0 File Offset: 0x000246E0
		ADObject IRecipientSession.FindByAccountName<T>(string domainName, string accountName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000264E7 File Offset: 0x000246E7
		IEnumerable<T> IRecipientSession.FindByAccountName<T>(string domain, string account, ADObjectId rootId, QueryFilter searchFilter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000264EE File Offset: 0x000246EE
		ADRecipient[] IRecipientSession.FindByANR(string anrMatch, int maxResults, SortBy sortBy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000264F5 File Offset: 0x000246F5
		ADRawEntry[] IRecipientSession.FindByANR(string anrMatch, int maxResults, SortBy sortBy, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000264FC File Offset: 0x000246FC
		ADRecipient IRecipientSession.FindByCertificate(X509Identifier identifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00026503 File Offset: 0x00024703
		ADRawEntry[] IRecipientSession.FindByCertificate(X509Identifier identifier, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002650A File Offset: 0x0002470A
		ADRawEntry IRecipientSession.FindByExchangeGuid(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00026511 File Offset: 0x00024711
		TEntry IRecipientSession.FindByExchangeGuid<TEntry>(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00026518 File Offset: 0x00024718
		ADRecipient IRecipientSession.FindByExchangeObjectId(Guid exchangeObjectId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002651F File Offset: 0x0002471F
		ADRecipient IRecipientSession.FindByExchangeGuid(Guid exchangeGuid)
		{
			return this.InternalGet<ADRecipient>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromExchangeGuid(exchangeGuid, false, false), ObjectType.Recipient, null));
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00026546 File Offset: 0x00024746
		ADRecipient IRecipientSession.FindByExchangeGuidIncludingAlternate(Guid exchangeGuid)
		{
			return this.InternalGet<ADRecipient>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromExchangeGuid(exchangeGuid, true, true), ObjectType.Recipient, null));
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002656D File Offset: 0x0002476D
		ADRawEntry IRecipientSession.FindByExchangeGuidIncludingAlternate(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			return this.InternalGet<ADRawEntry>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromExchangeGuid(exchangeGuid, true, true), ObjectType.ADRawEntry, properties));
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00026598 File Offset: 0x00024798
		TObject IRecipientSession.FindByExchangeGuidIncludingAlternate<TObject>(Guid exchangeGuid)
		{
			ObjectType objectTypeFor = CacheUtils.GetObjectTypeFor(typeof(TObject), false);
			if (objectTypeFor == ObjectType.Unknown)
			{
				return default(TObject);
			}
			return this.InternalGet<TObject>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromExchangeGuid(exchangeGuid, true, true), objectTypeFor, null));
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000265E8 File Offset: 0x000247E8
		ADRecipient IRecipientSession.FindByExchangeGuidIncludingArchive(Guid exchangeGuid)
		{
			return this.InternalGet<ADRecipient>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromExchangeGuid(exchangeGuid, false, true), ObjectType.Recipient, null));
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0002660F File Offset: 0x0002480F
		Result<ADRecipient>[] IRecipientSession.FindByExchangeGuidsIncludingArchive(Guid[] keys)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00026616 File Offset: 0x00024816
		ADRecipient IRecipientSession.FindByLegacyExchangeDN(string legacyExchangeDN)
		{
			return this.InternalGet<ADRecipient>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromLegacyExchangeDNs(legacyExchangeDN), ObjectType.Recipient, null));
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0002663B File Offset: 0x0002483B
		Result<ADRawEntry>[] IRecipientSession.FindByLegacyExchangeDNs(string[] legacyExchangeDNs, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00026644 File Offset: 0x00024844
		ADRecipient IRecipientSession.FindByObjectGuid(Guid guid)
		{
			ADObjectId objectId = new ADObjectId(null, guid);
			return this.InternalGet<ADRecipient>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromADObjectId(objectId), ObjectType.Recipient, null));
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002667C File Offset: 0x0002487C
		ADRecipient IRecipientSession.FindByProxyAddress(ProxyAddress proxyAddress)
		{
			return this.InternalGet<ADRecipient>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromProxyAddress(proxyAddress), ObjectType.Recipient, null));
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000266A1 File Offset: 0x000248A1
		ADRawEntry IRecipientSession.FindByProxyAddress(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties)
		{
			return this.InternalGet<ADRawEntry>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromProxyAddress(proxyAddress), ObjectType.ADRawEntry, properties));
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000266CA File Offset: 0x000248CA
		TEntry IRecipientSession.FindByProxyAddress<TEntry>(ProxyAddress proxyAddress)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000266D1 File Offset: 0x000248D1
		Result<ADRawEntry>[] IRecipientSession.FindByProxyAddresses(ProxyAddress[] proxyAddresses, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000266D8 File Offset: 0x000248D8
		Result<TEntry>[] IRecipientSession.FindByProxyAddresses<TEntry>(ProxyAddress[] proxyAddresses)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000266DF File Offset: 0x000248DF
		Result<ADRecipient>[] IRecipientSession.FindByProxyAddresses(ProxyAddress[] proxyAddresses)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000266E6 File Offset: 0x000248E6
		ADRecipient IRecipientSession.FindBySid(SecurityIdentifier sId)
		{
			return this.InternalGet<ADRecipient>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromSid(sId), ObjectType.Recipient, null));
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0002670B File Offset: 0x0002490B
		ADRawEntry IRecipientSession.FindUserBySid(SecurityIdentifier sId, IList<PropertyDefinition> properties)
		{
			return this.InternalGet<ADRawEntry>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromSid(sId), ObjectType.ADRawEntry, properties));
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00026734 File Offset: 0x00024934
		ADRawEntry[] IRecipientSession.FindDeletedADRawEntryByUsnRange(ADObjectId lastKnownParentId, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties, QueryFilter additionalFilter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002673B File Offset: 0x0002493B
		MiniRecipient[] IRecipientSession.FindMiniRecipient(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00026742 File Offset: 0x00024942
		MiniRecipient[] IRecipientSession.FindMiniRecipientByANR(string anrMatch, int maxResults, SortBy sortBy, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0002674C File Offset: 0x0002494C
		TResult IRecipientSession.FindMiniRecipientByProxyAddress<TResult>(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties)
		{
			ObjectType objectTypeFor = CacheUtils.GetObjectTypeFor(typeof(TResult), false);
			if (objectTypeFor == ObjectType.Unknown)
			{
				return default(TResult);
			}
			return this.InternalGet<TResult>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromProxyAddress(proxyAddress), objectTypeFor, properties));
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0002679C File Offset: 0x0002499C
		TResult IRecipientSession.FindMiniRecipientBySid<TResult>(SecurityIdentifier sId, IEnumerable<PropertyDefinition> properties)
		{
			ObjectType objectTypeFor = CacheUtils.GetObjectTypeFor(typeof(TResult), false);
			if (objectTypeFor == ObjectType.Unknown)
			{
				return default(TResult);
			}
			return this.InternalGet<TResult>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromSid(sId), objectTypeFor, properties));
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000267EA File Offset: 0x000249EA
		ADRecipient[] IRecipientSession.FindNames(IDictionary<PropertyDefinition, object> dictionary, int limit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000267F1 File Offset: 0x000249F1
		object[][] IRecipientSession.FindNamesView(IDictionary<PropertyDefinition, object> dictionary, int limit, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000267F8 File Offset: 0x000249F8
		Result<OWAMiniRecipient>[] IRecipientSession.FindOWAMiniRecipientByUserPrincipalName(string[] userPrincipalNames)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000267FF File Offset: 0x000249FF
		ADPagedReader<ADRecipient> IRecipientSession.FindPaged(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00026806 File Offset: 0x00024A06
		ADPagedReader<TEntry> IRecipientSession.FindPagedMiniRecipient<TEntry>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0002680D File Offset: 0x00024A0D
		ADRawEntry[] IRecipientSession.FindRecipient(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00026814 File Offset: 0x00024A14
		IEnumerable<ADGroup> IRecipientSession.FindRoleGroupsByForeignGroupSid(ADObjectId root, SecurityIdentifier sId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0002681B File Offset: 0x00024A1B
		List<string> IRecipientSession.GetTokenSids(ADRawEntry user, AssignmentMethod assignmentMethodFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00026822 File Offset: 0x00024A22
		List<string> IRecipientSession.GetTokenSids(ADObjectId userId, AssignmentMethod assignmentMethodFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00026829 File Offset: 0x00024A29
		SecurityIdentifier IRecipientSession.GetWellKnownExchangeGroupSid(Guid wkguid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00026830 File Offset: 0x00024A30
		bool IRecipientSession.IsLegacyDNInUse(string legacyDN)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00026837 File Offset: 0x00024A37
		bool IRecipientSession.IsMemberOfGroupByWellKnownGuid(Guid wellKnownGuid, string containerDN, ADObjectId id)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0002683E File Offset: 0x00024A3E
		bool IRecipientSession.IsRecipientInOrg(ProxyAddress proxyAddress)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00026845 File Offset: 0x00024A45
		bool IRecipientSession.IsReducedRecipientSession()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0002684C File Offset: 0x00024A4C
		bool IRecipientSession.IsThrottlingPolicyInUse(ADObjectId throttlingPolicyId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00026853 File Offset: 0x00024A53
		ADRecipient IRecipientSession.Read(ADObjectId entryId)
		{
			return this.InternalGet<ADRecipient>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromADObjectId(entryId), ObjectType.Recipient, null));
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00026878 File Offset: 0x00024A78
		TMiniRecipient IRecipientSession.ReadMiniRecipient<TMiniRecipient>(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			ObjectType objectTypeFor = CacheUtils.GetObjectTypeFor(typeof(TMiniRecipient), false);
			if (objectTypeFor != ObjectType.MiniRecipient && objectTypeFor != ObjectType.TransportMiniRecipient && objectTypeFor != ObjectType.LoadBalancingMiniRecipient && objectTypeFor != ObjectType.OWAMiniRecipient && objectTypeFor != ObjectType.ActiveSyncMiniRecipient && objectTypeFor != ObjectType.StorageMiniRecipient)
			{
				return default(TMiniRecipient);
			}
			return this.InternalGet<TMiniRecipient>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromADObjectId(entryId), objectTypeFor, properties));
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000268EA File Offset: 0x00024AEA
		MiniRecipient IRecipientSession.ReadMiniRecipient(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			return this.InternalGet<MiniRecipient>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromADObjectId(entryId), ObjectType.MiniRecipient, properties));
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00026910 File Offset: 0x00024B10
		Result<ADRecipient>[] IRecipientSession.ReadMultiple(ADObjectId[] entryIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00026917 File Offset: 0x00024B17
		Result<ADRawEntry>[] IRecipientSession.ReadMultiple(ADObjectId[] entryIds, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0002691E File Offset: 0x00024B1E
		Result<ADGroup>[] IRecipientSession.ReadMultipleADGroups(ADObjectId[] entryIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00026925 File Offset: 0x00024B25
		Result<ADUser>[] IRecipientSession.ReadMultipleADUsers(ADObjectId[] userIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0002692C File Offset: 0x00024B2C
		Result<ADRawEntry>[] IRecipientSession.ReadMultipleWithDeletedObjects(ADObjectId[] entryIds, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00026933 File Offset: 0x00024B33
		ADObjectId[] IRecipientSession.ResolveSidsToADObjectIds(string[] sids)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0002693A File Offset: 0x00024B3A
		void IRecipientSession.Save(ADRecipient instanceToSave)
		{
			this.InternalSave(instanceToSave, null, null, 2147483646, CacheItemPriority.Default);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0002694B File Offset: 0x00024B4B
		void IRecipientSession.Save(ADRecipient instanceToSave, bool bypassValidation)
		{
			this.InternalSave(instanceToSave, null, null, 2147483646, CacheItemPriority.Default);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0002695C File Offset: 0x00024B5C
		void IRecipientSession.SetPassword(ADObject obj, SecureString password)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00026963 File Offset: 0x00024B63
		void IRecipientSession.SetPassword(ADObjectId id, SecureString password)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0002696A File Offset: 0x00024B6A
		ADRawEntry ITenantRecipientSession.ChooseBetweenAmbiguousUsers(ADRawEntry[] entries)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00026971 File Offset: 0x00024B71
		ADObjectId ITenantRecipientSession.ChooseBetweenAmbiguousUsers(ADObjectId user1Id, ADObjectId user2Id)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00026978 File Offset: 0x00024B78
		DirectoryBackendType ITenantRecipientSession.DirectoryBackendType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0002697F File Offset: 0x00024B7F
		Result<ADRawEntry>[] ITenantRecipientSession.FindByExternalDirectoryObjectIds(string[] externalDirectoryObjectIds, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00026986 File Offset: 0x00024B86
		Result<ADRawEntry>[] ITenantRecipientSession.FindByExternalDirectoryObjectIds(string[] externalDirectoryObjectIds, bool includeDeletedObjects, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0002698D File Offset: 0x00024B8D
		ADRawEntry[] ITenantRecipientSession.FindByNetID(string netID, string organizationContext, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00026994 File Offset: 0x00024B94
		ADRawEntry[] ITenantRecipientSession.FindByNetID(string netID, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0002699B File Offset: 0x00024B9B
		MiniRecipient ITenantRecipientSession.FindRecipientByNetID(NetID netId)
		{
			return this.InternalGet<MiniRecipient>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromNetId(netId.ToString()), ObjectType.MiniRecipient, null));
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x000269C6 File Offset: 0x00024BC6
		ADRawEntry ITenantRecipientSession.FindUniqueEntryByNetID(string netID, string organizationContext, params PropertyDefinition[] properties)
		{
			return this.InternalGet<ADRawEntry>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromNetId(netID), ObjectType.ADRawEntry, properties));
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000269EF File Offset: 0x00024BEF
		ADRawEntry ITenantRecipientSession.FindUniqueEntryByNetID(string netID, params PropertyDefinition[] properties)
		{
			return this.InternalGet<ADRawEntry>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromNetId(netID), ObjectType.ADRawEntry, properties));
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00026A18 File Offset: 0x00024C18
		ADRawEntry ITenantRecipientSession.FindByLiveIdMemberName(string liveIdMemberName, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00026A1F File Offset: 0x00024C1F
		Result<ADRawEntry>[] ITenantRecipientSession.ReadMultipleByLinkedPartnerId(LinkedPartnerGroupInformation[] entryIds, params PropertyDefinition[] properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00026A26 File Offset: 0x00024C26
		MiniRecipientWithTokenGroups IRecipientSession.ReadTokenGroupsGlobalAndUniversal(ADObjectId id)
		{
			return this.InternalGet<MiniRecipientWithTokenGroups>(new DirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, KeyBuilder.LookupKeysFromADObjectId(id), ObjectType.MiniRecipientWithTokenGroups, null));
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00026A4F File Offset: 0x00024C4F
		void ICacheDirectorySession.Insert(IConfigurable objectToSave, IEnumerable<PropertyDefinition> properties, List<Tuple<string, KeyType>> keys, int secondsTimeout, CacheItemPriority priority)
		{
			ArgumentValidator.ThrowIfNull("objectToSave", objectToSave);
			this.InternalSave((ADRawEntry)objectToSave, properties, keys, secondsTimeout, priority);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00026A6E File Offset: 0x00024C6E
		internal void SetCallerInfo(string callerFilePath, string memberName, int callerFileLine)
		{
			this.logContext.FilePath = callerFilePath;
			this.logContext.FileLine = callerFileLine;
			this.logContext.MemberName = memberName;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00026A94 File Offset: 0x00024C94
		private void InternalSave(ADRawEntry entryToSave, IEnumerable<PropertyDefinition> properties = null, List<Tuple<string, KeyType>> otherKeys = null, int secondsTimeout = 2147483646, CacheItemPriority priority = CacheItemPriority.Default)
		{
			ArgumentValidator.ThrowIfOutOfRange<int>("secondsTimeout", secondsTimeout, 1, 2147483646);
			if (CacheUtils.GetObjectTypeFor(entryToSave.GetType(), false) == ObjectType.Unknown)
			{
				return;
			}
			List<Tuple<string, KeyType>> list = null;
			if (entryToSave is ADObject)
			{
				list = KeyBuilder.GetAddKeysFromObject(entryToSave as ADObject);
			}
			else
			{
				list = KeyBuilder.GetAddKeysFromADRawEntry(entryToSave);
			}
			if (otherKeys != null)
			{
				list.AddRange(otherKeys);
			}
			if (ExTraceGlobals.CacheSessionTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.CacheSessionTracer.TraceDebug((long)this.GetHashCode(), "InternalSave. DN {0}. Timeout {1}. Priority={2}. Keys [{3}]", new object[]
				{
					entryToSave.GetDistinguishedNameOrName(),
					secondsTimeout,
					priority,
					string.Join<Tuple<string, KeyType>>("|", list)
				});
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				AddDirectoryCacheRequest cacheRequest;
				if (entryToSave is ADObject)
				{
					cacheRequest = new AddDirectoryCacheRequest(list, entryToSave, this.sessionSettings.PartitionId.ForestFQDN, this.sessionSettings.CurrentOrganizationId, properties, secondsTimeout, priority);
				}
				else
				{
					List<PropertyDefinition> list2 = new List<PropertyDefinition>(entryToSave.propertyBag.Keys.Count);
					foreach (object obj in entryToSave.propertyBag.Keys)
					{
						ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)obj;
						if (entryToSave.propertyBag[adpropertyDefinition] == null || (adpropertyDefinition.IsMultivalued && ((MultiValuedPropertyBase)entryToSave.propertyBag[adpropertyDefinition]).Count == 0))
						{
							using (IEnumerator<PropertyDefinition> enumerator2 = properties.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									PropertyDefinition propertyDefinition = enumerator2.Current;
									if (adpropertyDefinition.Name.Equals(propertyDefinition.Name))
									{
										list2.Add(adpropertyDefinition);
										break;
									}
								}
								continue;
							}
						}
						list2.Add(adpropertyDefinition);
					}
					cacheRequest = new AddDirectoryCacheRequest(list, entryToSave, this.sessionSettings.PartitionId.ForestFQDN, this.sessionSettings.CurrentOrganizationId, list2, secondsTimeout, priority);
				}
				this.GetCacheClient().Put(cacheRequest);
				this.IsNewProxyObject = this.GetCacheClient().IsNewProxyObject;
				this.RetryCount = this.GetCacheClient().RetryCount;
			}
			catch (ADTransientException ex)
			{
				ExTraceGlobals.CacheSessionTracer.TraceError<Exception>((long)this.GetHashCode(), "InternalGet. Exception {0}", ex);
				CachePerformanceTracker.AddException(Operation.PutOperation, ex);
			}
			finally
			{
				stopwatch.Stop();
				CachePerformanceTracker.AddPerfData(Operation.TotalWCFPutOperation, stopwatch.ElapsedMilliseconds);
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00026D5C File Offset: 0x00024F5C
		private void InternalDelete(ADRawEntry objectToDelete)
		{
			ArgumentValidator.ThrowIfNull("objectToDelete", objectToDelete);
			if (CacheUtils.GetObjectTypeFor(objectToDelete.GetType(), false) == ObjectType.Unknown)
			{
				return;
			}
			ExTraceGlobals.CacheSessionTracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "InternalDelete. Removing Object from cache {0}", objectToDelete.Id);
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				this.GetCacheClient().Remove(new RemoveDirectoryCacheRequest(this.sessionSettings.PartitionId.ForestFQDN, this.sessionSettings.CurrentOrganizationId, KeyBuilder.LookupKeysFromADObjectId(objectToDelete.Id), CacheUtils.GetObjectTypeFor(objectToDelete.GetType(), true)));
				this.IsNewProxyObject = this.GetCacheClient().IsNewProxyObject;
				this.RetryCount = this.GetCacheClient().RetryCount;
			}
			catch (ADTransientException ex)
			{
				ExTraceGlobals.CacheSessionTracer.TraceError<Exception>((long)this.GetHashCode(), "InternalDelete. Exception {0}", ex);
				CachePerformanceTracker.AddException(Operation.RemoveOperation, ex);
			}
			finally
			{
				stopwatch.Stop();
				CachePerformanceTracker.AddPerfData(Operation.TotalWCFRemoveOperation, stopwatch.ElapsedMilliseconds);
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00026E60 File Offset: 0x00025060
		private TObject InternalGet<TObject>(DirectoryCacheRequest request) where TObject : ADRawEntry, new()
		{
			TObject tobject = default(TObject);
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				request.SetOrganizationId(this.sessionSettings.CurrentOrganizationId);
				tobject = this.GetCacheClient().Get<TObject>(request);
				this.ResultState = this.GetCacheClient().ResultState;
				this.IsNewProxyObject = this.GetCacheClient().IsNewProxyObject;
				this.RetryCount = this.GetCacheClient().RetryCount;
			}
			catch (ADTransientException ex)
			{
				ExTraceGlobals.CacheSessionTracer.TraceError<DirectoryCacheRequest, ADTransientException>((long)this.GetHashCode(), "InternalGet. Request {0} . Exception {1}", request, ex);
				CachePerformanceTracker.AddException(Operation.GetOperation, ex);
			}
			finally
			{
				stopwatch.Stop();
				CachePerformanceTracker.AddPerfData(Operation.TotalWCFGetOperation, stopwatch.ElapsedMilliseconds);
			}
			ExTraceGlobals.CacheSessionTracer.TraceDebug<DirectoryCacheRequest, string>((long)this.GetHashCode(), "InternalGet. Request {0}. Cache {1}", request, (tobject != null) ? "HIT" : "MISS");
			return tobject;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00026F50 File Offset: 0x00025150
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private IDirectoryCacheProvider GetCacheClient()
		{
			if (this.cacheProvider == null)
			{
				this.cacheProvider = DirectoryCacheProviderFactory.Default.CreateNewDirectoryCacheProvider();
			}
			return this.cacheProvider;
		}

		// Token: 0x040002B0 RID: 688
		[NonSerialized]
		private IDirectoryCacheProvider cacheProvider;

		// Token: 0x040002B1 RID: 689
		private ADSessionSettings sessionSettings;

		// Token: 0x040002B2 RID: 690
		private ADLogContext logContext = new ADLogContext();
	}
}
