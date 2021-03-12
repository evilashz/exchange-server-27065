using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000FC RID: 252
	internal class TenantLevelPartnerApplicationCache : LazyLookupTimeoutCache<TenantLevelPartnerApplicationCache.CacheKey, PartnerApplication>
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x00037B98 File Offset: 0x00035D98
		private TenantLevelPartnerApplicationCache() : base(2, TenantLevelPartnerApplicationCache.cacheSize.Value, false, TenantLevelPartnerApplicationCache.cacheTimeToLive.Value)
		{
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00037BB8 File Offset: 0x00035DB8
		public static TenantLevelPartnerApplicationCache Singleton
		{
			get
			{
				if (TenantLevelPartnerApplicationCache.singleton == null)
				{
					lock (TenantLevelPartnerApplicationCache.lockObj)
					{
						if (TenantLevelPartnerApplicationCache.singleton == null)
						{
							TenantLevelPartnerApplicationCache.singleton = new TenantLevelPartnerApplicationCache();
						}
					}
				}
				return TenantLevelPartnerApplicationCache.singleton;
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00037C10 File Offset: 0x00035E10
		public PartnerApplication Get(OrganizationId organizationId, string applicationId)
		{
			return base.Get(new TenantLevelPartnerApplicationCache.CacheKey(organizationId, applicationId));
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00037C64 File Offset: 0x00035E64
		protected override PartnerApplication CreateOnCacheMiss(TenantLevelPartnerApplicationCache.CacheKey key, ref bool shouldAdd)
		{
			ExTraceGlobals.OAuthTracer.TraceFunction(0L, "[TenantLevelPartnerApplicationCache::CreateOnCacheMiss] Entering");
			shouldAdd = true;
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(key.OrganizationId);
			IConfigurationSession configSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 103, "CreateOnCacheMiss", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\OAuth\\TenantLevelPartnerApplicationCache.cs");
			PartnerApplication[] results = null;
			ADNotificationAdapter.RunADOperation(delegate()
			{
				results = configSession.Find<PartnerApplication>(PartnerApplication.GetContainerId(configSession), QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, PartnerApplicationSchema.ApplicationIdentifier, key.ApplicationId), null, ADGenericPagedReader<PartnerApplication>.DefaultPageSize);
			});
			switch (results.Length)
			{
			case 0:
				return null;
			case 1:
				return results[0];
			default:
				ExTraceGlobals.OAuthTracer.TraceError<int, string, OrganizationId>(0L, "[TenantLevelPartnerApplicationCache::CreateOnCacheMiss] found {0} partner application for given pid {1} in org {2}", results.Length, key.ApplicationId, key.OrganizationId);
				return null;
			}
		}

		// Token: 0x040007C6 RID: 1990
		private static TimeSpanAppSettingsEntry cacheTimeToLive = new TimeSpanAppSettingsEntry("TenantLevelPartnerApplicationCacheTimeToLive", TimeSpanUnit.Minutes, TimeSpan.FromHours(30.0), ExTraceGlobals.OAuthTracer);

		// Token: 0x040007C7 RID: 1991
		private static IntAppSettingsEntry cacheSize = new IntAppSettingsEntry("TenantLevelPartnerApplicationCacheMaxItems", 500, ExTraceGlobals.OAuthTracer);

		// Token: 0x040007C8 RID: 1992
		private static readonly object lockObj = new object();

		// Token: 0x040007C9 RID: 1993
		private static TenantLevelPartnerApplicationCache singleton = null;

		// Token: 0x020000FD RID: 253
		internal sealed class CacheKey
		{
			// Token: 0x06000865 RID: 2149 RVA: 0x00037D89 File Offset: 0x00035F89
			public CacheKey(OrganizationId organizationId, string applicationId)
			{
				if (organizationId == OrganizationId.ForestWideOrgId)
				{
					throw new ArgumentException("organizationId must not come from first org");
				}
				this.organizationId = organizationId;
				this.applicationId = applicationId;
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06000866 RID: 2150 RVA: 0x00037DB7 File Offset: 0x00035FB7
			public OrganizationId OrganizationId
			{
				get
				{
					return this.organizationId;
				}
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x06000867 RID: 2151 RVA: 0x00037DBF File Offset: 0x00035FBF
			public string ApplicationId
			{
				get
				{
					return this.applicationId;
				}
			}

			// Token: 0x06000868 RID: 2152 RVA: 0x00037DC8 File Offset: 0x00035FC8
			public override bool Equals(object obj)
			{
				TenantLevelPartnerApplicationCache.CacheKey cacheKey = obj as TenantLevelPartnerApplicationCache.CacheKey;
				return cacheKey != null && this.organizationId.Equals(cacheKey.organizationId) && this.applicationId == cacheKey.applicationId;
			}

			// Token: 0x06000869 RID: 2153 RVA: 0x00037E07 File Offset: 0x00036007
			public override int GetHashCode()
			{
				return this.organizationId.GetHashCode() ^ this.applicationId.GetHashCode();
			}

			// Token: 0x040007CA RID: 1994
			private readonly OrganizationId organizationId;

			// Token: 0x040007CB RID: 1995
			private readonly string applicationId;
		}
	}
}
