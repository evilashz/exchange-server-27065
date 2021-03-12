using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200001A RID: 26
	internal interface IDirectorySession
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000DF RID: 223
		// (set) Token: 0x060000E0 RID: 224
		TimeSpan? ClientSideSearchTimeout { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E1 RID: 225
		ConfigScopes ConfigScope { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E2 RID: 226
		ConsistencyMode ConsistencyMode { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000E3 RID: 227
		// (set) Token: 0x060000E4 RID: 228
		string DomainController { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000E5 RID: 229
		// (set) Token: 0x060000E6 RID: 230
		bool EnforceContainerizedScoping { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000E7 RID: 231
		// (set) Token: 0x060000E8 RID: 232
		bool EnforceDefaultScope { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E9 RID: 233
		string LastUsedDc { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000EA RID: 234
		int Lcid { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000EB RID: 235
		// (set) Token: 0x060000EC RID: 236
		string LinkResolutionServer { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000ED RID: 237
		// (set) Token: 0x060000EE RID: 238
		bool LogSizeLimitExceededEvent { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000EF RID: 239
		NetworkCredential NetworkCredential { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F0 RID: 240
		bool ReadOnly { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F1 RID: 241
		ADServerSettings ServerSettings { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F2 RID: 242
		// (set) Token: 0x060000F3 RID: 243
		TimeSpan? ServerTimeout { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F4 RID: 244
		ADSessionSettings SessionSettings { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F5 RID: 245
		// (set) Token: 0x060000F6 RID: 246
		bool SkipRangedAttributes { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000F7 RID: 247
		// (set) Token: 0x060000F8 RID: 248
		string[] ExclusiveLdapAttributes { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000F9 RID: 249
		// (set) Token: 0x060000FA RID: 250
		bool UseConfigNC { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000FB RID: 251
		// (set) Token: 0x060000FC RID: 252
		bool UseGlobalCatalog { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000FD RID: 253
		// (set) Token: 0x060000FE RID: 254
		IActivityScope ActivityScope { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000FF RID: 255
		string CallerInfo { get; }

		// Token: 0x06000100 RID: 256
		void AnalyzeDirectoryError(PooledLdapConnection connection, DirectoryRequest request, DirectoryException de, int totalRetries, int retriesOnServer);

		// Token: 0x06000101 RID: 257
		QueryFilter ApplyDefaultFilters(QueryFilter filter, ADObjectId rootId, ADObject dummyObject, bool applyImplicitFilter);

		// Token: 0x06000102 RID: 258
		QueryFilter ApplyDefaultFilters(QueryFilter filter, ADScope scope, ADObject dummyObject, bool applyImplicitFilter);

		// Token: 0x06000103 RID: 259
		void CheckFilterForUnsafeIdentity(QueryFilter filter);

		// Token: 0x06000104 RID: 260
		void UnsafeExecuteModificationRequest(DirectoryRequest request, ADObjectId rootId);

		// Token: 0x06000105 RID: 261
		ADRawEntry[] Find(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06000106 RID: 262
		TResult[] Find<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults) where TResult : ADObject, new();

		// Token: 0x06000107 RID: 263
		ADRawEntry[] FindAllADRawEntriesByUsnRange(ADObjectId root, long startUsn, long endUsn, int sizeLimit, bool useAtomicFilter, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06000108 RID: 264
		Result<ADRawEntry>[] FindByADObjectIds(ADObjectId[] ids, params PropertyDefinition[] properties);

		// Token: 0x06000109 RID: 265
		Result<TData>[] FindByADObjectIds<TData>(ADObjectId[] ids) where TData : ADObject, new();

		// Token: 0x0600010A RID: 266
		Result<ADRawEntry>[] FindByCorrelationIds(Guid[] correlationIds, ADObjectId configUnit, params PropertyDefinition[] properties);

		// Token: 0x0600010B RID: 267
		Result<ADRawEntry>[] FindByExchangeLegacyDNs(string[] exchangeLegacyDNs, params PropertyDefinition[] properties);

		// Token: 0x0600010C RID: 268
		Result<ADRawEntry>[] FindByObjectGuids(Guid[] objectGuids, params PropertyDefinition[] properties);

		// Token: 0x0600010D RID: 269
		ADRawEntry[] FindDeletedTenantSyncObjectByUsnRange(ADObjectId tenantOuRoot, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties);

		// Token: 0x0600010E RID: 270
		ADPagedReader<TResult> FindPaged<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties) where TResult : IConfigurable, new();

		// Token: 0x0600010F RID: 271
		ADPagedReader<ADRawEntry> FindPagedADRawEntry(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06000110 RID: 272
		ADPagedReader<ADRawEntry> FindPagedADRawEntryWithDefaultFilters<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties) where TResult : ADObject, new();

		// Token: 0x06000111 RID: 273
		ADPagedReader<TResult> FindPagedDeletedObject<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize) where TResult : DeletedObject, new();

		// Token: 0x06000112 RID: 274
		ADObjectId GetConfigurationNamingContext();

		// Token: 0x06000113 RID: 275
		ADObjectId GetConfigurationUnitsRoot();

		// Token: 0x06000114 RID: 276
		ADObjectId GetDomainNamingContext();

		// Token: 0x06000115 RID: 277
		ADObjectId GetHostedOrganizationsRoot();

		// Token: 0x06000116 RID: 278
		ADObjectId GetRootDomainNamingContext();

		// Token: 0x06000117 RID: 279
		ADObjectId GetSchemaNamingContext();

		// Token: 0x06000118 RID: 280
		PooledLdapConnection GetReadConnection(string preferredServer, ref ADObjectId rootId);

		// Token: 0x06000119 RID: 281
		PooledLdapConnection GetReadConnection(string preferredServer, string optionalBaseDN, ref ADObjectId rootId, ADRawEntry scopeDeteriminingObject);

		// Token: 0x0600011A RID: 282
		ADScope GetReadScope(ADObjectId rootId, ADRawEntry scopeDeterminingObject);

		// Token: 0x0600011B RID: 283
		ADScope GetReadScope(ADObjectId rootId, ADRawEntry scopeDeterminingObject, bool isWellKnownGuidSearch, out ConfigScopes applicableScope);

		// Token: 0x0600011C RID: 284
		bool GetSchemaAndApplyFilter(ADRawEntry adRawEntry, ADScope scope, out ADObject dummyObject, out string[] ldapAttributes, ref QueryFilter filter, ref IEnumerable<PropertyDefinition> properties);

		// Token: 0x0600011D RID: 285
		bool IsReadConnectionAvailable();

		// Token: 0x0600011E RID: 286
		bool IsRootIdWithinScope<TObject>(ADObjectId rootId) where TObject : IConfigurable, new();

		// Token: 0x0600011F RID: 287
		bool IsTenantIdentity(ADObjectId id);

		// Token: 0x06000120 RID: 288
		TResult[] ObjectsFromEntries<TResult>(SearchResultEntryCollection entries, string originatingServerName, IEnumerable<PropertyDefinition> properties, ADRawEntry dummyInstance) where TResult : IConfigurable, new();

		// Token: 0x06000121 RID: 289
		ADRawEntry ReadADRawEntry(ADObjectId entryId, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06000122 RID: 290
		RawSecurityDescriptor ReadSecurityDescriptor(ADObjectId id);

		// Token: 0x06000123 RID: 291
		SecurityDescriptor ReadSecurityDescriptorBlob(ADObjectId id);

		// Token: 0x06000124 RID: 292
		string[] ReplicateSingleObject(ADObject instanceToReplicate, ADObjectId[] sites);

		// Token: 0x06000125 RID: 293
		bool ReplicateSingleObjectToTargetDC(ADObject instanceToReplicate, string targetServerName);

		// Token: 0x06000126 RID: 294
		TResult ResolveWellKnownGuid<TResult>(Guid wellKnownGuid, ADObjectId containerId) where TResult : ADObject, new();

		// Token: 0x06000127 RID: 295
		TResult ResolveWellKnownGuid<TResult>(Guid wellKnownGuid, string containerDN) where TResult : ADObject, new();

		// Token: 0x06000128 RID: 296
		TenantRelocationSyncObject RetrieveTenantRelocationSyncObject(ADObjectId entryId, IEnumerable<PropertyDefinition> properties);

		// Token: 0x06000129 RID: 297
		ADOperationResultWithData<TResult>[] RunAgainstAllDCsInSite<TResult>(ADObjectId siteId, Func<TResult> methodToCall) where TResult : class;

		// Token: 0x0600012A RID: 298
		void SaveSecurityDescriptor(ADObjectId id, RawSecurityDescriptor sd);

		// Token: 0x0600012B RID: 299
		void SaveSecurityDescriptor(ADObjectId id, RawSecurityDescriptor sd, bool modifyOwner);

		// Token: 0x0600012C RID: 300
		void SaveSecurityDescriptor(ADObject obj, RawSecurityDescriptor sd);

		// Token: 0x0600012D RID: 301
		void SaveSecurityDescriptor(ADObject obj, RawSecurityDescriptor sd, bool modifyOwner);

		// Token: 0x0600012E RID: 302
		bool TryVerifyIsWithinScopes(ADObject entry, bool isModification, out ADScopeException exception);

		// Token: 0x0600012F RID: 303
		void UpdateServerSettings(PooledLdapConnection connection);

		// Token: 0x06000130 RID: 304
		void VerifyIsWithinScopes(ADObject entry, bool isModification);
	}
}
