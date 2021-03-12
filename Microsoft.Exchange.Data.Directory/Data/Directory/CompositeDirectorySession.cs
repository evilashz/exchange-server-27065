using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Cache;
using Microsoft.Exchange.Data.Directory.Diagnostics;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.DirectoryCache;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000C5 RID: 197
	internal abstract class CompositeDirectorySession<TSession> : IDirectorySession, IConfigDataProvider where TSession : IDirectorySession, IConfigDataProvider
	{
		// Token: 0x06000A0E RID: 2574 RVA: 0x0002D2B9 File Offset: 0x0002B4B9
		protected CompositeDirectorySession(TSession cacheSession, TSession directorySession, bool cacheSessionForDeletingOnly = false)
		{
			ArgumentValidator.ThrowIfNull("cacheSession", cacheSession);
			ArgumentValidator.ThrowIfNull("directorySession", directorySession);
			this.cacheSession = cacheSession;
			this.directorySession = directorySession;
			this.CacheSessionForDeletingOnly = cacheSessionForDeletingOnly;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0002D2F6 File Offset: 0x0002B4F6
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x0002D2FE File Offset: 0x0002B4FE
		internal bool CacheSessionForDeletingOnly { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000A11 RID: 2577
		protected abstract string Implementor { get; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0002D308 File Offset: 0x0002B508
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x0002D32C File Offset: 0x0002B52C
		TimeSpan? IDirectorySession.ClientSideSearchTimeout
		{
			get
			{
				TSession session = this.GetSession();
				return session.ClientSideSearchTimeout;
			}
			set
			{
				TSession session = this.GetSession();
				session.ClientSideSearchTimeout = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0002D350 File Offset: 0x0002B550
		ConfigScopes IDirectorySession.ConfigScope
		{
			get
			{
				TSession session = this.GetSession();
				return session.ConfigScope;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0002D374 File Offset: 0x0002B574
		ConsistencyMode IDirectorySession.ConsistencyMode
		{
			get
			{
				TSession session = this.GetSession();
				return session.ConsistencyMode;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0002D398 File Offset: 0x0002B598
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0002D3BC File Offset: 0x0002B5BC
		string IDirectorySession.DomainController
		{
			get
			{
				TSession session = this.GetSession();
				return session.DomainController;
			}
			set
			{
				TSession session = this.GetSession();
				session.DomainController = value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0002D3E0 File Offset: 0x0002B5E0
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x0002D404 File Offset: 0x0002B604
		bool IDirectorySession.EnforceContainerizedScoping
		{
			get
			{
				TSession session = this.GetSession();
				return session.EnforceContainerizedScoping;
			}
			set
			{
				TSession session = this.GetSession();
				session.EnforceContainerizedScoping = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0002D428 File Offset: 0x0002B628
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0002D44C File Offset: 0x0002B64C
		bool IDirectorySession.EnforceDefaultScope
		{
			get
			{
				TSession session = this.GetSession();
				return session.EnforceDefaultScope;
			}
			set
			{
				TSession session = this.GetSession();
				session.EnforceDefaultScope = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0002D470 File Offset: 0x0002B670
		string IDirectorySession.LastUsedDc
		{
			get
			{
				TSession session = this.GetSession();
				return session.LastUsedDc;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0002D494 File Offset: 0x0002B694
		int IDirectorySession.Lcid
		{
			get
			{
				TSession session = this.GetSession();
				return session.Lcid;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0002D4B8 File Offset: 0x0002B6B8
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x0002D4DC File Offset: 0x0002B6DC
		string IDirectorySession.LinkResolutionServer
		{
			get
			{
				TSession session = this.GetSession();
				return session.LinkResolutionServer;
			}
			set
			{
				TSession session = this.GetSession();
				session.LinkResolutionServer = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0002D500 File Offset: 0x0002B700
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x0002D524 File Offset: 0x0002B724
		bool IDirectorySession.LogSizeLimitExceededEvent
		{
			get
			{
				TSession session = this.GetSession();
				return session.LogSizeLimitExceededEvent;
			}
			set
			{
				TSession session = this.GetSession();
				session.LogSizeLimitExceededEvent = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0002D548 File Offset: 0x0002B748
		NetworkCredential IDirectorySession.NetworkCredential
		{
			get
			{
				TSession session = this.GetSession();
				return session.NetworkCredential;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0002D56C File Offset: 0x0002B76C
		bool IDirectorySession.ReadOnly
		{
			get
			{
				TSession session = this.GetSession();
				return session.ReadOnly;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002D590 File Offset: 0x0002B790
		ADServerSettings IDirectorySession.ServerSettings
		{
			get
			{
				TSession session = this.GetSession();
				return session.ServerSettings;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0002D5B4 File Offset: 0x0002B7B4
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x0002D5D8 File Offset: 0x0002B7D8
		TimeSpan? IDirectorySession.ServerTimeout
		{
			get
			{
				TSession session = this.GetSession();
				return session.ServerTimeout;
			}
			set
			{
				TSession session = this.GetSession();
				session.ServerTimeout = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0002D5FC File Offset: 0x0002B7FC
		ADSessionSettings IDirectorySession.SessionSettings
		{
			get
			{
				TSession session = this.GetSession();
				return session.SessionSettings;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0002D620 File Offset: 0x0002B820
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x0002D644 File Offset: 0x0002B844
		bool IDirectorySession.SkipRangedAttributes
		{
			get
			{
				TSession session = this.GetSession();
				return session.SkipRangedAttributes;
			}
			set
			{
				TSession session = this.GetSession();
				session.SkipRangedAttributes = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0002D668 File Offset: 0x0002B868
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x0002D68C File Offset: 0x0002B88C
		public string[] ExclusiveLdapAttributes
		{
			get
			{
				TSession session = this.GetSession();
				return session.ExclusiveLdapAttributes;
			}
			set
			{
				TSession session = this.GetSession();
				session.ExclusiveLdapAttributes = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0002D6B0 File Offset: 0x0002B8B0
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x0002D6D4 File Offset: 0x0002B8D4
		bool IDirectorySession.UseConfigNC
		{
			get
			{
				TSession session = this.GetSession();
				return session.UseConfigNC;
			}
			set
			{
				TSession session = this.GetSession();
				session.UseConfigNC = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0002D6F8 File Offset: 0x0002B8F8
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x0002D71C File Offset: 0x0002B91C
		bool IDirectorySession.UseGlobalCatalog
		{
			get
			{
				TSession session = this.GetSession();
				return session.UseGlobalCatalog;
			}
			set
			{
				TSession session = this.GetSession();
				session.UseGlobalCatalog = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0002D740 File Offset: 0x0002B940
		// (set) Token: 0x06000A31 RID: 2609 RVA: 0x0002D764 File Offset: 0x0002B964
		IActivityScope IDirectorySession.ActivityScope
		{
			get
			{
				TSession session = this.GetSession();
				return session.ActivityScope;
			}
			set
			{
				TSession session = this.GetSession();
				session.ActivityScope = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0002D788 File Offset: 0x0002B988
		string IDirectorySession.CallerInfo
		{
			get
			{
				TSession session = this.GetSession();
				return session.CallerInfo;
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002D7AC File Offset: 0x0002B9AC
		void IDirectorySession.AnalyzeDirectoryError(PooledLdapConnection connection, DirectoryRequest request, DirectoryException de, int totalRetries, int retriesOnServer)
		{
			TSession session = this.GetSession();
			session.AnalyzeDirectoryError(connection, request, de, totalRetries, retriesOnServer);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002D7D4 File Offset: 0x0002B9D4
		QueryFilter IDirectorySession.ApplyDefaultFilters(QueryFilter filter, ADObjectId rootId, ADObject dummyObject, bool applyImplicitFilter)
		{
			TSession session = this.GetSession();
			return session.ApplyDefaultFilters(filter, rootId, dummyObject, applyImplicitFilter);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002D7FC File Offset: 0x0002B9FC
		QueryFilter IDirectorySession.ApplyDefaultFilters(QueryFilter filter, ADScope scope, ADObject dummyObject, bool applyImplicitFilter)
		{
			TSession session = this.GetSession();
			return session.ApplyDefaultFilters(filter, scope, dummyObject, applyImplicitFilter);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002D824 File Offset: 0x0002BA24
		void IDirectorySession.CheckFilterForUnsafeIdentity(QueryFilter filter)
		{
			TSession session = this.GetSession();
			session.CheckFilterForUnsafeIdentity(filter);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0002D848 File Offset: 0x0002BA48
		void IDirectorySession.UnsafeExecuteModificationRequest(DirectoryRequest request, ADObjectId rootId)
		{
			TSession session = this.GetSession();
			session.UnsafeExecuteModificationRequest(request, rootId);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002D86C File Offset: 0x0002BA6C
		ADRawEntry[] IDirectorySession.Find(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			TSession session = this.GetSession();
			return session.Find(rootId, scope, filter, sortBy, maxResults, properties);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002D898 File Offset: 0x0002BA98
		TResult[] IDirectorySession.Find<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			TSession session = this.GetSession();
			return session.Find<TResult>(rootId, scope, filter, sortBy, maxResults);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002D8C0 File Offset: 0x0002BAC0
		ADRawEntry[] IDirectorySession.FindAllADRawEntriesByUsnRange(ADObjectId root, long startUsn, long endUsn, int sizeLimit, bool useAtomicFilter, IEnumerable<PropertyDefinition> properties)
		{
			TSession session = this.GetSession();
			return session.FindAllADRawEntriesByUsnRange(root, startUsn, endUsn, sizeLimit, useAtomicFilter, properties);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002D8EC File Offset: 0x0002BAEC
		Result<ADRawEntry>[] IDirectorySession.FindByADObjectIds(ADObjectId[] ids, params PropertyDefinition[] properties)
		{
			TSession session = this.GetSession();
			return session.FindByADObjectIds(ids, properties);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002D910 File Offset: 0x0002BB10
		Result<TData>[] IDirectorySession.FindByADObjectIds<TData>(ADObjectId[] ids)
		{
			TSession session = this.GetSession();
			return session.FindByADObjectIds<TData>(ids);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002D934 File Offset: 0x0002BB34
		Result<ADRawEntry>[] IDirectorySession.FindByCorrelationIds(Guid[] correlationIds, ADObjectId configUnit, params PropertyDefinition[] properties)
		{
			TSession session = this.GetSession();
			return session.FindByCorrelationIds(correlationIds, configUnit, properties);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002D958 File Offset: 0x0002BB58
		Result<ADRawEntry>[] IDirectorySession.FindByExchangeLegacyDNs(string[] exchangeLegacyDNs, params PropertyDefinition[] properties)
		{
			TSession session = this.GetSession();
			return session.FindByExchangeLegacyDNs(exchangeLegacyDNs, properties);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002D97C File Offset: 0x0002BB7C
		Result<ADRawEntry>[] IDirectorySession.FindByObjectGuids(Guid[] objectGuids, params PropertyDefinition[] properties)
		{
			TSession session = this.GetSession();
			return session.FindByObjectGuids(objectGuids, properties);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002D9A0 File Offset: 0x0002BBA0
		ADRawEntry[] IDirectorySession.FindDeletedTenantSyncObjectByUsnRange(ADObjectId tenantOuRoot, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties)
		{
			TSession session = this.GetSession();
			return session.FindDeletedTenantSyncObjectByUsnRange(tenantOuRoot, startUsn, sizeLimit, properties);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002D9C8 File Offset: 0x0002BBC8
		ADPagedReader<TResult> IDirectorySession.FindPaged<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			TSession session = this.GetSession();
			return session.FindPaged<TResult>(rootId, scope, filter, sortBy, pageSize, properties);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002D9F4 File Offset: 0x0002BBF4
		ADPagedReader<ADRawEntry> IDirectorySession.FindPagedADRawEntry(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			TSession session = this.GetSession();
			return session.FindPagedADRawEntry(rootId, scope, filter, sortBy, pageSize, properties);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002DA20 File Offset: 0x0002BC20
		ADPagedReader<ADRawEntry> IDirectorySession.FindPagedADRawEntryWithDefaultFilters<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			TSession session = this.GetSession();
			return session.FindPagedADRawEntryWithDefaultFilters<TResult>(rootId, scope, filter, sortBy, pageSize, properties);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002DA4C File Offset: 0x0002BC4C
		ADPagedReader<TResult> IDirectorySession.FindPagedDeletedObject<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			TSession session = this.GetSession();
			return session.FindPagedDeletedObject<TResult>(rootId, scope, filter, sortBy, pageSize);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002DA74 File Offset: 0x0002BC74
		ADObjectId IDirectorySession.GetConfigurationNamingContext()
		{
			TSession session = this.GetSession();
			return session.GetConfigurationNamingContext();
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002DA98 File Offset: 0x0002BC98
		ADObjectId IDirectorySession.GetConfigurationUnitsRoot()
		{
			TSession session = this.GetSession();
			return session.GetConfigurationUnitsRoot();
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002DABC File Offset: 0x0002BCBC
		ADObjectId IDirectorySession.GetDomainNamingContext()
		{
			TSession session = this.GetSession();
			return session.GetDomainNamingContext();
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002DAE0 File Offset: 0x0002BCE0
		ADObjectId IDirectorySession.GetHostedOrganizationsRoot()
		{
			TSession session = this.GetSession();
			return session.GetHostedOrganizationsRoot();
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002DB04 File Offset: 0x0002BD04
		ADObjectId IDirectorySession.GetRootDomainNamingContext()
		{
			TSession session = this.GetSession();
			return session.GetRootDomainNamingContext();
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002DB28 File Offset: 0x0002BD28
		ADObjectId IDirectorySession.GetSchemaNamingContext()
		{
			TSession session = this.GetSession();
			return session.GetSchemaNamingContext();
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002DB4C File Offset: 0x0002BD4C
		PooledLdapConnection IDirectorySession.GetReadConnection(string preferredServer, ref ADObjectId rootId)
		{
			TSession session = this.GetSession();
			return session.GetReadConnection(preferredServer, ref rootId);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002DB70 File Offset: 0x0002BD70
		PooledLdapConnection IDirectorySession.GetReadConnection(string preferredServer, string optionalBaseDN, ref ADObjectId rootId, ADRawEntry scopeDeteriminingObject)
		{
			TSession session = this.GetSession();
			return session.GetReadConnection(preferredServer, optionalBaseDN, ref rootId, scopeDeteriminingObject);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002DB98 File Offset: 0x0002BD98
		ADScope IDirectorySession.GetReadScope(ADObjectId rootId, ADRawEntry scopeDeterminingObject)
		{
			TSession session = this.GetSession();
			return session.GetReadScope(rootId, scopeDeterminingObject);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002DBBC File Offset: 0x0002BDBC
		ADScope IDirectorySession.GetReadScope(ADObjectId rootId, ADRawEntry scopeDeterminingObject, bool isWellKnownGuidSearch, out ConfigScopes applicableScope)
		{
			TSession session = this.GetSession();
			return session.GetReadScope(rootId, scopeDeterminingObject, isWellKnownGuidSearch, out applicableScope);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002DBE4 File Offset: 0x0002BDE4
		bool IDirectorySession.GetSchemaAndApplyFilter(ADRawEntry adRawEntry, ADScope scope, out ADObject dummyObject, out string[] ldapAttributes, ref QueryFilter filter, ref IEnumerable<PropertyDefinition> properties)
		{
			TSession session = this.GetSession();
			return session.GetSchemaAndApplyFilter(adRawEntry, scope, out dummyObject, out ldapAttributes, ref filter, ref properties);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002DC10 File Offset: 0x0002BE10
		bool IDirectorySession.IsReadConnectionAvailable()
		{
			TSession session = this.GetSession();
			return session.IsReadConnectionAvailable();
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002DC34 File Offset: 0x0002BE34
		bool IDirectorySession.IsRootIdWithinScope<TObject>(ADObjectId rootId)
		{
			TSession session = this.GetSession();
			return session.IsRootIdWithinScope<TObject>(rootId);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002DC58 File Offset: 0x0002BE58
		bool IDirectorySession.IsTenantIdentity(ADObjectId id)
		{
			TSession session = this.GetSession();
			return session.IsTenantIdentity(id);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002DCA0 File Offset: 0x0002BEA0
		ADRawEntry IDirectorySession.ReadADRawEntry(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			return this.ExecuteSingleObjectQueryWithFallback<ADRawEntry>((TSession session) => session.ReadADRawEntry(entryId, properties), null, properties);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002DCDC File Offset: 0x0002BEDC
		RawSecurityDescriptor IDirectorySession.ReadSecurityDescriptor(ADObjectId id)
		{
			TSession session = this.GetSession();
			return session.ReadSecurityDescriptor(id);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002DD00 File Offset: 0x0002BF00
		SecurityDescriptor IDirectorySession.ReadSecurityDescriptorBlob(ADObjectId id)
		{
			TSession session = this.GetSession();
			return session.ReadSecurityDescriptorBlob(id);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002DD24 File Offset: 0x0002BF24
		string[] IDirectorySession.ReplicateSingleObject(ADObject instanceToReplicate, ADObjectId[] sites)
		{
			TSession session = this.GetSession();
			return session.ReplicateSingleObject(instanceToReplicate, sites);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002DD48 File Offset: 0x0002BF48
		bool IDirectorySession.ReplicateSingleObjectToTargetDC(ADObject instanceToReplicate, string targetServerName)
		{
			TSession session = this.GetSession();
			return session.ReplicateSingleObjectToTargetDC(instanceToReplicate, targetServerName);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002DD6C File Offset: 0x0002BF6C
		TResult IDirectorySession.ResolveWellKnownGuid<TResult>(Guid wellKnownGuid, ADObjectId containerId)
		{
			TSession session = this.GetSession();
			return session.ResolveWellKnownGuid<TResult>(wellKnownGuid, containerId);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002DD90 File Offset: 0x0002BF90
		TResult IDirectorySession.ResolveWellKnownGuid<TResult>(Guid wellKnownGuid, string containerDN)
		{
			TSession session = this.GetSession();
			return session.ResolveWellKnownGuid<TResult>(wellKnownGuid, containerDN);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002DDB4 File Offset: 0x0002BFB4
		TenantRelocationSyncObject IDirectorySession.RetrieveTenantRelocationSyncObject(ADObjectId entryId, IEnumerable<PropertyDefinition> properties)
		{
			TSession session = this.GetSession();
			return session.RetrieveTenantRelocationSyncObject(entryId, properties);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002DDD8 File Offset: 0x0002BFD8
		ADOperationResultWithData<TResult>[] IDirectorySession.RunAgainstAllDCsInSite<TResult>(ADObjectId siteId, Func<TResult> methodToCall)
		{
			TSession session = this.GetSession();
			return session.RunAgainstAllDCsInSite<TResult>(siteId, methodToCall);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002DDFC File Offset: 0x0002BFFC
		void IDirectorySession.SaveSecurityDescriptor(ADObjectId id, RawSecurityDescriptor sd)
		{
			TSession session = this.GetSession();
			session.SaveSecurityDescriptor(id, sd);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002DE20 File Offset: 0x0002C020
		void IDirectorySession.SaveSecurityDescriptor(ADObjectId id, RawSecurityDescriptor sd, bool modifyOwner)
		{
			TSession session = this.GetSession();
			session.SaveSecurityDescriptor(id, sd, modifyOwner);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002DE44 File Offset: 0x0002C044
		void IDirectorySession.SaveSecurityDescriptor(ADObject obj, RawSecurityDescriptor sd)
		{
			obj.m_Session = this.GetSession();
			TSession session = this.GetSession();
			session.SaveSecurityDescriptor(obj, sd);
			obj.m_Session = this;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002DE80 File Offset: 0x0002C080
		void IDirectorySession.SaveSecurityDescriptor(ADObject obj, RawSecurityDescriptor sd, bool modifyOwner)
		{
			obj.m_Session = this.GetSession();
			TSession session = this.GetSession();
			session.SaveSecurityDescriptor(obj, sd, modifyOwner);
			obj.m_Session = this;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002DEBC File Offset: 0x0002C0BC
		bool IDirectorySession.TryVerifyIsWithinScopes(ADObject entry, bool isModification, out ADScopeException exception)
		{
			TSession session = this.GetSession();
			return session.TryVerifyIsWithinScopes(entry, isModification, out exception);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002DEE0 File Offset: 0x0002C0E0
		void IDirectorySession.UpdateServerSettings(PooledLdapConnection connection)
		{
			TSession session = this.GetSession();
			session.UpdateServerSettings(connection);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002DF04 File Offset: 0x0002C104
		void IDirectorySession.VerifyIsWithinScopes(ADObject entry, bool isModification)
		{
			TSession session = this.GetSession();
			session.VerifyIsWithinScopes(entry, isModification);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002DF28 File Offset: 0x0002C128
		TResult[] IDirectorySession.ObjectsFromEntries<TResult>(SearchResultEntryCollection entries, string originatingServerName, IEnumerable<PropertyDefinition> properties, ADRawEntry dummyInstance)
		{
			TSession session = this.GetSession();
			return session.ObjectsFromEntries<TResult>(entries, originatingServerName, properties, dummyInstance);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002DF4E File Offset: 0x0002C14E
		void IConfigDataProvider.Delete(IConfigurable instance)
		{
			this.InternalDelete(instance);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002DF58 File Offset: 0x0002C158
		IConfigurable[] IConfigDataProvider.Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			TSession session = this.GetSession();
			return session.Find<T>(filter, rootId, deepSearch, sortBy);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002DF80 File Offset: 0x0002C180
		IEnumerable<T> IConfigDataProvider.FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			TSession session = this.GetSession();
			return session.FindPaged<T>(filter, rootId, deepSearch, sortBy, pageSize);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002DFA8 File Offset: 0x0002C1A8
		IConfigurable IConfigDataProvider.Read<T>(ObjectId identity)
		{
			TSession session = this.GetSession();
			return session.Read<T>(identity);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002DFCA File Offset: 0x0002C1CA
		void IConfigDataProvider.Save(IConfigurable instance)
		{
			this.InternalSave(instance);
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0002DFD4 File Offset: 0x0002C1D4
		string IConfigDataProvider.Source
		{
			get
			{
				TSession session = this.GetSession();
				return session.Source;
			}
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002DFF5 File Offset: 0x0002C1F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected TSession GetCacheSession()
		{
			return this.cacheSession;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002DFFD File Offset: 0x0002C1FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected TSession GetSession()
		{
			return this.directorySession;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002E008 File Offset: 0x0002C208
		protected TResult ExecuteSingleObjectQueryWithFallback<TResult>(Func<TSession, TResult> query, Func<TResult, List<Tuple<string, KeyType>>> getAdditionalKeys = null, IEnumerable<PropertyDefinition> properties = null) where TResult : ADRawEntry, new()
		{
			ArgumentValidator.ThrowIfNull("query", query);
			if (!Configuration.IsCacheEnabled(typeof(TResult)))
			{
				return query(this.GetSession());
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			CachePerformanceTracker.StartLogging();
			CompositeDirectorySession<TSession>.TraceState traceState = CompositeDirectorySession<TSession>.TraceState.None;
			CacheMode cacheModeForCurrentProcess = Configuration.GetCacheModeForCurrentProcess();
			double num = -1.0;
			double num2 = -1.0;
			double num3 = -1.0;
			double num4 = -1.0;
			TResult tresult = default(TResult);
			string text = string.Empty;
			Guid guid = Guid.Empty;
			ADCacheResultState adcacheResultState = ADCacheResultState.Succeed;
			bool flag = true;
			int num5 = 0;
			try
			{
				Stopwatch stopwatch2 = null;
				ADObject adobject = null;
				try
				{
					TSession session = this.GetSession();
					if (session.ActivityScope != null)
					{
						TSession session2 = this.GetSession();
						if (session2.ActivityScope.Status == ActivityContextStatus.ActivityStarted)
						{
							TSession session3 = this.GetSession();
							guid = session3.ActivityScope.ActivityId;
						}
					}
					if (!this.CacheSessionForDeletingOnly && (CacheMode.Read & cacheModeForCurrentProcess) != CacheMode.Disabled)
					{
						TSession session4 = this.GetSession();
						if (!session4.SessionSettings.IncludeSoftDeletedObjectLinks)
						{
							if (properties != null || !typeof(TResult).Equals(typeof(ADRawEntry)))
							{
								traceState |= CompositeDirectorySession<TSession>.TraceState.CacheRead;
								stopwatch2 = Stopwatch.StartNew();
								tresult = query(this.GetCacheSession());
								CacheDirectorySession cacheDirectorySession = this.GetCacheSession() as CacheDirectorySession;
								if (cacheDirectorySession != null)
								{
									adcacheResultState = cacheDirectorySession.ResultState;
									flag = cacheDirectorySession.IsNewProxyObject;
									num5 = cacheDirectorySession.RetryCount;
								}
								stopwatch2.Stop();
								num = (double)stopwatch2.ElapsedMilliseconds;
								goto IL_19A;
							}
							goto IL_19A;
						}
					}
					if ((CacheMode.Read & cacheModeForCurrentProcess) == CacheMode.Disabled || this.CacheSessionForDeletingOnly)
					{
						adcacheResultState = ADCacheResultState.CacheModeIsNotRead;
					}
					else
					{
						adcacheResultState = ADCacheResultState.SoftDeletedObject;
					}
					IL_19A:
					if (tresult != null && tresult.Id != null)
					{
						TSession session5 = this.GetSession();
						if (ADSession.ShouldFilterCNFObject(session5.SessionSettings, tresult.Id))
						{
							tresult = default(TResult);
							adcacheResultState = ADCacheResultState.CNFedObject;
						}
						else
						{
							TSession session6 = this.GetSession();
							if (ADSession.ShouldFilterSoftDeleteObject(session6.SessionSettings, tresult.Id))
							{
								tresult = default(TResult);
								adcacheResultState = ADCacheResultState.SoftDeletedObject;
							}
							else
							{
								if (tresult.Id != null && tresult.Id.DomainId != null)
								{
									TSession session7 = this.GetSession();
									if (!session7.SessionSettings.PartitionId.Equals(tresult.Id.GetPartitionId()))
									{
										if (ExEnvironment.IsTest)
										{
											TSession session8 = this.GetSession();
											if (session8.SessionSettings.PartitionId.ForestFQDN.EndsWith(tresult.Id.GetPartitionId().ForestFQDN, StringComparison.OrdinalIgnoreCase))
											{
												goto IL_341;
											}
										}
										ExEventLog.EventTuple tuple_WrongObjectReturned = DirectoryEventLogConstants.Tuple_WrongObjectReturned;
										string name = typeof(TResult).Name;
										object[] array = new object[2];
										object[] array2 = array;
										int num6 = 0;
										TSession session9 = this.GetSession();
										array2[num6] = session9.SessionSettings.PartitionId.ForestFQDN;
										array[1] = tresult.Id.GetPartitionId().ForestFQDN;
										Globals.LogEvent(tuple_WrongObjectReturned, name, array);
										tresult = default(TResult);
										adcacheResultState = ADCacheResultState.WrongForest;
										goto IL_3E1;
									}
								}
								IL_341:
								TSession session10 = this.GetSession();
								if (session10.SessionSettings.CurrentOrganizationId != OrganizationId.ForestWideOrgId)
								{
									TSession session11 = this.GetSession();
									OrganizationId currentOrganizationId = session11.SessionSettings.CurrentOrganizationId;
									if (!tresult.Id.IsDescendantOf(currentOrganizationId.OrganizationalUnit) && !tresult.Id.IsDescendantOf(currentOrganizationId.ConfigurationUnit.Parent))
									{
										tresult = default(TResult);
										adcacheResultState = ADCacheResultState.OranizationIdMismatch;
									}
								}
							}
						}
					}
					else if (tresult != null)
					{
						tresult = default(TResult);
					}
					else
					{
						adcacheResultState = ADCacheResultState.NotFound;
					}
					IL_3E1:
					adobject = (tresult as ADObject);
				}
				catch (Exception ex)
				{
					tresult = default(TResult);
					adcacheResultState = ADCacheResultState.ExceptionHappened;
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_CallADCacheServiceFailed, typeof(TResult).Name, new object[]
					{
						ex.ToString()
					});
					text = ex.ToString();
				}
				if (tresult == null)
				{
					traceState |= CompositeDirectorySession<TSession>.TraceState.ADRead;
					stopwatch2 = Stopwatch.StartNew();
					tresult = query(this.GetSession());
					adobject = (tresult as ADObject);
					stopwatch2.Stop();
					num4 = (double)stopwatch2.ElapsedMilliseconds;
					stopwatch2.Restart();
					bool flag2 = true;
					if (adobject != null)
					{
						TSession session12 = this.GetSession();
						if (!session12.ReadOnly && CompositeDirectorySession<TSession>.ExchangeConfigUnitCUType.Equals(adobject.GetType()) && OrganizationId.ForestWideOrgId.Equals(adobject.OrganizationId))
						{
							ExTraceGlobals.SessionTracer.TraceWarning<string>((long)this.GetHashCode(), "Newly created ExchangeCU with organizationId equals to RootOrgId, ignored till is fully populated. DN {0}", tresult.GetDistinguishedNameOrName());
							flag2 = false;
						}
					}
					if (tresult != null && CacheUtils.GetObjectTypeFor(tresult.GetType(), false) == ObjectType.Unknown)
					{
						flag2 = false;
					}
					if (tresult != null && ((CacheMode.SyncWrite | CacheMode.AsyncWrite) & cacheModeForCurrentProcess) != CacheMode.Disabled && tresult.Id != null && !tresult.Id.IsDeleted && flag2)
					{
						traceState |= CompositeDirectorySession<TSession>.TraceState.CacheInsert;
						if ((CacheMode.SyncWrite & cacheModeForCurrentProcess) != CacheMode.Disabled)
						{
							try
							{
								this.CacheInsert<TResult>(tresult, getAdditionalKeys, properties);
								goto IL_5C8;
							}
							catch (Exception ex2)
							{
								Globals.LogEvent(DirectoryEventLogConstants.Tuple_CallADCacheServiceFailed, "CacheInsert", new object[]
								{
									ex2.ToString()
								});
								text += ex2.Message;
								goto IL_5C8;
							}
						}
						TSession session13 = this.GetSession();
						if (session13.ReadOnly)
						{
							this.AsyncCacheInsert<TResult>(tresult, getAdditionalKeys, properties);
						}
						else
						{
							adcacheResultState |= ADCacheResultState.WritableSession;
						}
					}
					IL_5C8:
					stopwatch2.Stop();
					num3 = (double)stopwatch2.ElapsedMilliseconds;
				}
				if (adobject != null)
				{
					adobject.m_Session = this;
				}
			}
			finally
			{
				stopwatch.Stop();
				string text2 = CachePerformanceTracker.StopLogging();
				string operation = "Read";
				string dn = (tresult != null) ? tresult.GetDistinguishedNameOrName() : "<NULL>";
				DateTime whenReadUTC = (tresult != null) ? ((tresult.WhenReadUTC != null) ? tresult.WhenReadUTC.Value : DateTime.MinValue) : DateTime.MinValue;
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				long wcfGetProcessingTime = (long)num;
				long wcfRemoveProcessingTime = (long)num2;
				long wcfPutProcessingTime = (long)num3;
				long adProcessingTime = (long)num4;
				bool isNewProxyObject = flag;
				int retryCount = num5;
				string objectType = (tresult != null) ? tresult.GetType().Name : typeof(TResult).Name;
				string cachePerformanceTracker = text2;
				Guid activityId = guid;
				TSession session14 = this.GetSession();
				CacheProtocolLog.BeginAppend(operation, dn, whenReadUTC, elapsedMilliseconds, wcfGetProcessingTime, wcfRemoveProcessingTime, wcfPutProcessingTime, adProcessingTime, isNewProxyObject, retryCount, objectType, cachePerformanceTracker, activityId, session14.CallerInfo, string.Format("ResultState:{0};{1}", (int)adcacheResultState, text));
				ExTraceGlobals.SessionTracer.TraceDebug((long)this.GetHashCode(), "ExecuteSingleObjectQueryWithFallback. Cache Mode {0}. TraceState {1}. DN {2}. WhenRead {3}. IsCached {4}.", new object[]
				{
					cacheModeForCurrentProcess,
					traceState,
					(tresult != null) ? tresult.GetDistinguishedNameOrName() : "<NULL>",
					(tresult != null) ? ((tresult.WhenReadUTC != null) ? tresult.WhenReadUTC.Value.ToString() : "<NULL>") : "<NULL>",
					tresult != null && tresult.IsCached
				});
				ExTraceGlobals.SessionTracer.TracePerformance((long)this.GetHashCode(), "ExecuteSingleObjectQueryWithFallback.  Cache Mode {0}. TraceState {1}. DN {2}. TotalTime {3}. GetCacheTime {4}. RemoveCacheTime {5}. PutCacheTime {6}. ADTime {7}. WCFDetails [{8}]", new object[]
				{
					cacheModeForCurrentProcess,
					traceState,
					(tresult != null) ? tresult.GetDistinguishedNameOrName() : "<NULL>",
					stopwatch.ElapsedMilliseconds,
					num,
					num2,
					num3,
					num4,
					text2
				});
				if (tresult != null)
				{
					ADProviderPerf.UpdateADDriverCacheHitRate(tresult.IsCached);
				}
			}
			return tresult;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002E8B8 File Offset: 0x0002CAB8
		protected void InternalSave(IConfigurable instance)
		{
			ObjectState objectState = instance.ObjectState;
			TSession session = this.GetSession();
			session.Save(instance);
			this.CacheUpdateFromSavedObject(instance, objectState);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002E8EC File Offset: 0x0002CAEC
		protected void InternalDelete(IConfigurable instance)
		{
			TSession session = this.GetSession();
			session.Delete(instance);
			this.CacheDelete(instance);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002E918 File Offset: 0x0002CB18
		protected void CacheUpdateFromSavedObject(IConfigurable instance, ObjectState objectStateBeforeSave)
		{
			ADRawEntry adrawEntry = instance as ADRawEntry;
			bool flag = Configuration.IsCacheEnabledForInsertOnSave(adrawEntry);
			ExTraceGlobals.SessionTracer.TraceDebug((long)this.GetHashCode(), "UpdateOrRemoveFromCache. Identity={0}, ObjectStateBeforeSave={1}, DistinguishedName={2}, WhenCreatedUtc={3}, InsertInCache={4}", new object[]
			{
				instance.Identity,
				objectStateBeforeSave,
				(adrawEntry != null) ? adrawEntry.GetDistinguishedNameOrName() : "<NULL>",
				(adrawEntry != null) ? adrawEntry[ADObjectSchema.WhenCreatedUTC] : "<NULL>",
				flag
			});
			if (!flag)
			{
				if (objectStateBeforeSave != ObjectState.New)
				{
					this.CacheDelete(instance);
				}
				return;
			}
			IEnumerable<PropertyDefinition> objectProperties = null;
			if (!(adrawEntry is ADObject))
			{
				objectProperties = new List<PropertyDefinition>(0);
			}
			this.CacheInsert<ADRawEntry>(adrawEntry, null, objectProperties);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002E9D0 File Offset: 0x0002CBD0
		protected void CacheDelete(IConfigurable instance)
		{
			CacheMode cacheModeForCurrentProcess = Configuration.GetCacheModeForCurrentProcess();
			if (((CacheMode.SyncWrite | CacheMode.AsyncWrite) & cacheModeForCurrentProcess) != CacheMode.Disabled)
			{
				if ((CacheMode.SyncWrite & cacheModeForCurrentProcess) != CacheMode.Disabled)
				{
					this.CacheDeleteInternal(instance);
				}
				else
				{
					ThreadPool.QueueUserWorkItem(delegate(object x)
					{
						this.CacheDeleteInternal((IConfigurable)x);
					}, instance);
				}
			}
			ExTraceGlobals.SessionTracer.TraceDebug<CacheMode, string>((long)this.GetHashCode(), "CacheDelete. Cache Mode {0}. ID {1}", cacheModeForCurrentProcess, instance.Identity.ToString());
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002EA6C File Offset: 0x0002CC6C
		protected T InvokeWithAPILogging<T>(Func<T> action, [CallerMemberName] string memberName = null)
		{
			return ADScenarioLog.InvokeWithAPILog<T>(DateTime.UtcNow, memberName, default(Guid), this.Implementor, "", () => action(), delegate
			{
				TSession session = this.GetSession();
				return session.LastUsedDc;
			});
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002EB00 File Offset: 0x0002CD00
		protected T InvokeGetObjectWithAPILogging<T>(Func<T> action, [CallerMemberName] string memberName = null) where T : ADRawEntry
		{
			return ADScenarioLog.InvokeGetObjectAPIAndLog<T>(DateTime.UtcNow, memberName, default(Guid), this.Implementor, "", () => action(), delegate
			{
				TSession session = this.GetSession();
				return session.LastUsedDc;
			});
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002EB58 File Offset: 0x0002CD58
		private void CacheDeleteInternal(IConfigurable instance)
		{
			string error = null;
			Stopwatch stopwatch = Stopwatch.StartNew();
			CachePerformanceTracker.StartLogging();
			bool isNewProxyObject = false;
			int retryCount = 0;
			string callerInfo = null;
			Guid activityId = Guid.Empty;
			try
			{
				TSession tsession = this.GetCacheSession();
				tsession.Delete(instance);
				CacheDirectorySession cacheDirectorySession = this.GetCacheSession() as CacheDirectorySession;
				if (cacheDirectorySession != null)
				{
					isNewProxyObject = cacheDirectorySession.IsNewProxyObject;
					retryCount = cacheDirectorySession.RetryCount;
				}
				TSession session = this.GetSession();
				if (session.ActivityScope != null)
				{
					TSession session2 = this.GetSession();
					if (session2.ActivityScope.Status == ActivityContextStatus.ActivityStarted)
					{
						TSession session3 = this.GetSession();
						activityId = session3.ActivityScope.ActivityId;
					}
				}
				TSession session4 = this.GetSession();
				callerInfo = session4.CallerInfo;
			}
			catch (Exception ex)
			{
				error = ex.ToString();
				throw;
			}
			finally
			{
				stopwatch.Stop();
				string cachePerformanceTracker = CachePerformanceTracker.StopLogging();
				ADRawEntry adrawEntry = instance as ADRawEntry;
				CacheProtocolLog.BeginAppend("Remove", (adrawEntry != null) ? adrawEntry.GetDistinguishedNameOrName() : "<NULL>", DateTime.MinValue, stopwatch.ElapsedMilliseconds, -1L, stopwatch.ElapsedMilliseconds, -1L, -1L, isNewProxyObject, retryCount, instance.GetType().Name, cachePerformanceTracker, activityId, callerInfo, error);
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002ECF0 File Offset: 0x0002CEF0
		private void AsyncCacheInsert<TResult>(TResult instance, Func<TResult, List<Tuple<string, KeyType>>> getAdditionalKeys = null, IEnumerable<PropertyDefinition> objectProperties = null) where TResult : IConfigurable, new()
		{
			ThreadPool.QueueUserWorkItem(delegate(object x)
			{
				try
				{
					Tuple<TResult, Func<TResult, List<Tuple<string, KeyType>>>, IEnumerable<PropertyDefinition>> tuple = (Tuple<TResult, Func<TResult, List<Tuple<string, KeyType>>>, IEnumerable<PropertyDefinition>>)x;
					this.CacheInsert<TResult>(tuple.Item1, tuple.Item2, tuple.Item3);
				}
				catch
				{
				}
			}, new Tuple<TResult, Func<TResult, List<Tuple<string, KeyType>>>, IEnumerable<PropertyDefinition>>(instance, getAdditionalKeys, objectProperties));
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002ED0C File Offset: 0x0002CF0C
		private void CacheInsert<TResult>(TResult instance, Func<TResult, List<Tuple<string, KeyType>>> getAdditionalKeys = null, IEnumerable<PropertyDefinition> objectProperties = null) where TResult : IConfigurable, new()
		{
			ArgumentValidator.ThrowIfNull("query", instance);
			Stopwatch stopwatch = Stopwatch.StartNew();
			CachePerformanceTracker.StartLogging();
			bool isNewProxyObject = false;
			int retryCount = 0;
			Guid activityId = Guid.Empty;
			string callerInfo = null;
			string error = null;
			try
			{
				List<Tuple<string, KeyType>> keys = null;
				TSession session = this.GetSession();
				if (session.ActivityScope != null)
				{
					TSession session2 = this.GetSession();
					if (session2.ActivityScope.Status == ActivityContextStatus.ActivityStarted)
					{
						TSession session3 = this.GetSession();
						activityId = session3.ActivityScope.ActivityId;
					}
				}
				TSession session4 = this.GetSession();
				callerInfo = session4.CallerInfo;
				if (getAdditionalKeys != null)
				{
					keys = getAdditionalKeys(instance);
				}
				ICacheDirectorySession cacheDirectorySession = this.GetCacheSession() as ICacheDirectorySession;
				bool flag = false;
				if (((IDirectorySession)this).SessionSettings.TenantConsistencyMode == TenantConsistencyMode.IgnoreRetiredTenants && (TenantRelocationStateCache.IsTenantRetired((ADObjectId)instance.Identity) || TenantRelocationStateCache.IsTenantArriving((ADObjectId)instance.Identity)))
				{
					ExTraceGlobals.SessionTracer.TraceWarning<ObjectId>((long)this.GetHashCode(), "CacheInsert. DN {0}. Tenant Is Retired or Arriving, skipping.", instance.Identity);
				}
				else
				{
					if (!instance.GetType().Equals(CompositeDirectorySession<TSession>.ExchangeConfigUnitCUType) && (TenantRelocationStateCache.IsTenantLockedDown((ADObjectId)instance.Identity) || TenantRelocationStateCache.IsTenantRetired((ADObjectId)instance.Identity)))
					{
						flag = true;
					}
					int num = flag ? 30 : Configuration.GetCacheExpirationForObject(instance as ADRawEntry);
					CacheItemPriority cacheItemPriority = flag ? CacheItemPriority.Default : Configuration.GetCachePriorityForObject(instance as ADRawEntry);
					if (cacheDirectorySession != null)
					{
						cacheDirectorySession.Insert(instance, objectProperties, keys, num, cacheItemPriority);
					}
					else
					{
						TSession tsession = this.GetCacheSession();
						tsession.Save(instance);
					}
					CacheDirectorySession cacheDirectorySession2 = this.GetCacheSession() as CacheDirectorySession;
					if (cacheDirectorySession2 != null)
					{
						isNewProxyObject = cacheDirectorySession2.IsNewProxyObject;
						retryCount = cacheDirectorySession2.RetryCount;
					}
					ExTraceGlobals.SessionTracer.TraceDebug((long)this.GetHashCode(), "CacheInsert. DN={0}, IsTenantLockedDownOrRetired={1}, CacheExpiration={2}, Priority={3}", new object[]
					{
						instance.Identity,
						flag,
						num,
						cacheItemPriority
					});
				}
			}
			catch (TransientException ex)
			{
				ExTraceGlobals.SessionTracer.TraceError<ObjectId, TransientException>((long)this.GetHashCode(), "CacheInsert. DN {0}. Exception {1}", instance.Identity, ex);
				error = ex.ToString();
			}
			catch (Exception ex2)
			{
				error = ex2.ToString();
				throw;
			}
			finally
			{
				stopwatch.Stop();
				string cachePerformanceTracker = CachePerformanceTracker.StopLogging();
				ADRawEntry adrawEntry = instance as ADRawEntry;
				CacheProtocolLog.BeginAppend("Save", (adrawEntry != null) ? adrawEntry.GetDistinguishedNameOrName() : "<NULL>", DateTime.MinValue, stopwatch.ElapsedMilliseconds, -1L, -1L, stopwatch.ElapsedMilliseconds, -1L, isNewProxyObject, retryCount, instance.GetType().Name, cachePerformanceTracker, activityId, callerInfo, error);
			}
		}

		// Token: 0x04000405 RID: 1029
		private static readonly Type ExchangeConfigUnitCUType = typeof(ExchangeConfigurationUnit);

		// Token: 0x04000406 RID: 1030
		private TSession cacheSession;

		// Token: 0x04000407 RID: 1031
		private TSession directorySession;

		// Token: 0x020000C6 RID: 198
		[Flags]
		private enum TraceState : byte
		{
			// Token: 0x0400040A RID: 1034
			None = 0,
			// Token: 0x0400040B RID: 1035
			CacheRead = 1,
			// Token: 0x0400040C RID: 1036
			CacheRemoval = 2,
			// Token: 0x0400040D RID: 1037
			CacheInsert = 4,
			// Token: 0x0400040E RID: 1038
			ADRead = 8
		}
	}
}
