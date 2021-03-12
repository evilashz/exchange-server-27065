using System;
using System.Threading;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009DC RID: 2524
	internal class ThrottlingPolicyCache
	{
		// Token: 0x060074D5 RID: 29909 RVA: 0x001812CC File Offset: 0x0017F4CC
		protected ThrottlingPolicyCache()
		{
			this.globalThrottlingPolicyCache = new ThrottlingPolicyCache.GlobalThrottlingPolicyCache(ThrottlingPolicyCache.cacheExpirationInterval);
			this.organizationThrottlingPolicies = new AutoRefreshCache<OrganizationId, CachableThrottlingPolicyItem, object>(10000L, ThrottlingPolicyCache.cacheExpirationInterval, ThrottlingPolicyCache.cacheCleanupInterval, ThrottlingPolicyCache.cachePurgeInterval, ThrottlingPolicyCache.cacheRefreshInterval, new DefaultCacheTracer<OrganizationId>(ThrottlingPolicyCache.Tracer, "OrganizationThrottlingPolicies"), ThrottlingPerfCounterWrapper.GetOrganizationThrottlingPolicyCacheCounters(10000L), new AutoRefreshCache<OrganizationId, CachableThrottlingPolicyItem, object>.CreateEntryDelegate(ThrottlingPolicyCache.ResolveOrganizationThrottlingPolicy));
			this.throttlingPolicies = new AutoRefreshCache<OrgAndObjectId, CachableThrottlingPolicyItem, object>(10000L, ThrottlingPolicyCache.cacheExpirationInterval, ThrottlingPolicyCache.cacheCleanupInterval, ThrottlingPolicyCache.cachePurgeInterval, ThrottlingPolicyCache.cacheRefreshInterval, new DefaultCacheTracer<OrgAndObjectId>(ThrottlingPolicyCache.Tracer, "ThrottlingPolicies"), ThrottlingPerfCounterWrapper.GetThrottlingPolicyCacheCounters(10000L), new AutoRefreshCache<OrgAndObjectId, CachableThrottlingPolicyItem, object>.CreateEntryDelegate(ThrottlingPolicyCache.ResolveThrottlingPolicy));
		}

		// Token: 0x060074D6 RID: 29910 RVA: 0x001813C0 File Offset: 0x0017F5C0
		internal static ThrottlingPolicy ReadThrottlingPolicyFromAD(IConfigurationSession session, object id, Func<IConfigurationSession, object, ThrottlingPolicy> getPolicy)
		{
			ThrottlingPolicy policy = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				session.SessionSettings.IsSharedConfigChecked = true;
				policy = getPolicy(session, id);
			});
			if (!adoperationResult.Succeeded && adoperationResult.Exception != null)
			{
				ThrottlingPolicyCache.Tracer.TraceError<string, string, object>(0L, "Encountered exception reading throttling policy.  Exception Class: {0}, Message: {1}, Key: {2}", adoperationResult.Exception.GetType().FullName, adoperationResult.Exception.Message, id);
			}
			if (policy != null)
			{
				ValidationError[] array = policy.Validate();
				if (array != null && array.Length > 0)
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_FailedToReadThrottlingPolicy, id.ToString(), new object[]
					{
						id,
						array[0].Description
					});
					policy = null;
				}
			}
			return policy;
		}

		// Token: 0x1700299A RID: 10650
		// (get) Token: 0x060074D7 RID: 29911 RVA: 0x001814A4 File Offset: 0x0017F6A4
		internal static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ClientThrottlingTracer;
			}
		}

		// Token: 0x1700299B RID: 10651
		// (get) Token: 0x060074D8 RID: 29912 RVA: 0x001814AB File Offset: 0x0017F6AB
		public int OrganizationThrottlingPolicyCount
		{
			get
			{
				return this.organizationThrottlingPolicies.Count;
			}
		}

		// Token: 0x1700299C RID: 10652
		// (get) Token: 0x060074D9 RID: 29913 RVA: 0x001814B8 File Offset: 0x0017F6B8
		public int ThrottlingPolicyCount
		{
			get
			{
				return this.throttlingPolicies.Count;
			}
		}

		// Token: 0x060074DA RID: 29914 RVA: 0x001814C5 File Offset: 0x0017F6C5
		public IThrottlingPolicy GetGlobalThrottlingPolicy()
		{
			this.BeforeGet();
			return this.globalThrottlingPolicyCache.Get();
		}

		// Token: 0x060074DB RID: 29915 RVA: 0x001814D8 File Offset: 0x0017F6D8
		public virtual IThrottlingPolicy Get(OrganizationId organizationId)
		{
			this.BeforeGet();
			return this.organizationThrottlingPolicies.GetValue(null, organizationId).ThrottlingPolicy;
		}

		// Token: 0x060074DC RID: 29916 RVA: 0x001814F2 File Offset: 0x0017F6F2
		public virtual IThrottlingPolicy Get(OrganizationId orgId, ADObjectId throttlingPolicyId)
		{
			if (throttlingPolicyId == null)
			{
				return this.Get(orgId);
			}
			return this.Get(new OrgAndObjectId(orgId, throttlingPolicyId));
		}

		// Token: 0x060074DD RID: 29917 RVA: 0x0018150C File Offset: 0x0017F70C
		public virtual IThrottlingPolicy Get(OrgAndObjectId orgAndObjectId)
		{
			this.BeforeGet();
			return this.throttlingPolicies.GetValue(null, orgAndObjectId).ThrottlingPolicy;
		}

		// Token: 0x060074DE RID: 29918 RVA: 0x00181526 File Offset: 0x0017F726
		public void Remove(OrganizationId organizationId)
		{
			this.organizationThrottlingPolicies.Remove(organizationId);
		}

		// Token: 0x060074DF RID: 29919 RVA: 0x00181534 File Offset: 0x0017F734
		public void Clear()
		{
			this.globalThrottlingPolicyCache.Clear();
			this.organizationThrottlingPolicies.Clear();
			this.throttlingPolicies.Clear();
		}

		// Token: 0x060074E0 RID: 29920 RVA: 0x00181558 File Offset: 0x0017F758
		private static IConfigurationSession GetSession(OrganizationId organizationId)
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(organizationId), 371, "GetSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\throttling\\throttlingpolicycache.cs");
		}

		// Token: 0x060074E1 RID: 29921 RVA: 0x00181598 File Offset: 0x0017F798
		private static CachableThrottlingPolicyItem ResolveOrganizationThrottlingPolicy(object obj, OrganizationId organizationId)
		{
			SharedConfiguration sharedConfiguration = SharedConfiguration.GetSharedConfiguration(organizationId);
			if (sharedConfiguration != null)
			{
				organizationId = sharedConfiguration.SharedConfigId;
			}
			ThrottlingPolicy throttlingPolicy = ThrottlingPolicyCache.ReadThrottlingPolicyFromAD(ThrottlingPolicyCache.GetSession(organizationId), organizationId, (IConfigurationSession session1, object id) => session1.GetOrganizationThrottlingPolicy((OrganizationId)id));
			return new CachableThrottlingPolicyItem((throttlingPolicy == null) ? ThrottlingPolicyCache.Singleton.GetGlobalThrottlingPolicy() : throttlingPolicy.GetEffectiveThrottlingPolicy(true));
		}

		// Token: 0x060074E2 RID: 29922 RVA: 0x0018160C File Offset: 0x0017F80C
		private static CachableThrottlingPolicyItem ResolveThrottlingPolicy(object obj, OrgAndObjectId orgAndObjectId)
		{
			ThrottlingPolicy throttlingPolicy = null;
			if (orgAndObjectId.Id.IsDeleted)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DeletedThrottlingPolicyReferenced, orgAndObjectId.ToString(), new object[]
				{
					orgAndObjectId
				});
			}
			else
			{
				throttlingPolicy = ThrottlingPolicyCache.ReadThrottlingPolicyFromAD(ThrottlingPolicyCache.GetSession(orgAndObjectId.OrganizationId), orgAndObjectId.Id, (IConfigurationSession session1, object id) => session1.Read<ThrottlingPolicy>((ADObjectId)id));
			}
			return new CachableThrottlingPolicyItem((throttlingPolicy == null) ? ThrottlingPolicyCache.Singleton.Get(orgAndObjectId.OrganizationId) : throttlingPolicy.GetEffectiveThrottlingPolicy(true));
		}

		// Token: 0x060074E3 RID: 29923 RVA: 0x0018169C File Offset: 0x0017F89C
		private void BeforeGet()
		{
			int num = 0;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(2663787837U, ref num);
			if (num != 0 && num != this.lastClearCacheStamp)
			{
				this.lastClearCacheStamp = num;
				this.Clear();
			}
		}

		// Token: 0x04004B3E RID: 19262
		private const int MaxCacheSize = 10000;

		// Token: 0x04004B3F RID: 19263
		private const uint LidClearThrottlingCaches = 2663787837U;

		// Token: 0x04004B40 RID: 19264
		private static readonly TimeSpan cacheExpirationInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04004B41 RID: 19265
		private static readonly TimeSpan cacheCleanupInterval = TimeSpan.FromMinutes(10.0);

		// Token: 0x04004B42 RID: 19266
		private static readonly TimeSpan cachePurgeInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04004B43 RID: 19267
		private static readonly TimeSpan cacheRefreshInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04004B44 RID: 19268
		public static readonly ThrottlingPolicyCache Singleton = new ThrottlingPolicyCache();

		// Token: 0x04004B45 RID: 19269
		private int lastClearCacheStamp;

		// Token: 0x04004B46 RID: 19270
		private ThrottlingPolicyCache.GlobalThrottlingPolicyCache globalThrottlingPolicyCache;

		// Token: 0x04004B47 RID: 19271
		private AutoRefreshCache<OrganizationId, CachableThrottlingPolicyItem, object> organizationThrottlingPolicies;

		// Token: 0x04004B48 RID: 19272
		private AutoRefreshCache<OrgAndObjectId, CachableThrottlingPolicyItem, object> throttlingPolicies;

		// Token: 0x020009DD RID: 2525
		private class GlobalThrottlingPolicyCache : IDisposable
		{
			// Token: 0x060074E7 RID: 29927 RVA: 0x0018173C File Offset: 0x0017F93C
			internal GlobalThrottlingPolicyCache(TimeSpan refreshInterval)
			{
				this.session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 481, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\throttling\\throttlingpolicycache.cs");
				this.refreshTimer = new GuardedTimer(new TimerCallback(this.Refresh), null, refreshInterval);
			}

			// Token: 0x060074E8 RID: 29928 RVA: 0x00181798 File Offset: 0x0017F998
			internal IThrottlingPolicy Get()
			{
				if (!this.disposed && this.policy == null)
				{
					this.Refresh(null);
				}
				return this.policy;
			}

			// Token: 0x060074E9 RID: 29929 RVA: 0x001817B8 File Offset: 0x0017F9B8
			internal void Clear()
			{
				lock (this.objLock)
				{
					this.policy = null;
				}
			}

			// Token: 0x060074EA RID: 29930 RVA: 0x001817FC File Offset: 0x0017F9FC
			public void Dispose()
			{
				this.refreshTimer.Dispose(true);
				this.disposed = true;
			}

			// Token: 0x060074EB RID: 29931 RVA: 0x00181820 File Offset: 0x0017FA20
			private IThrottlingPolicy ResolveGlobalThrottlingPolicy()
			{
				ThrottlingPolicy throttlingPolicy = null;
				if (!this.disposed)
				{
					throttlingPolicy = ThrottlingPolicyCache.ReadThrottlingPolicyFromAD(this.session, null, (IConfigurationSession session1, object id) => ((ITopologyConfigurationSession)session1).GetGlobalThrottlingPolicy());
				}
				if (throttlingPolicy != null)
				{
					return throttlingPolicy.GetEffectiveThrottlingPolicy(false);
				}
				return FallbackThrottlingPolicy.GetSingleton();
			}

			// Token: 0x060074EC RID: 29932 RVA: 0x00181874 File Offset: 0x0017FA74
			private void Refresh(object unused)
			{
				IThrottlingPolicy throttlingPolicy = this.ResolveGlobalThrottlingPolicy();
				lock (this.objLock)
				{
					this.policy = throttlingPolicy;
				}
			}

			// Token: 0x04004B4B RID: 19275
			private GuardedTimer refreshTimer;

			// Token: 0x04004B4C RID: 19276
			private IThrottlingPolicy policy;

			// Token: 0x04004B4D RID: 19277
			private object objLock = new object();

			// Token: 0x04004B4E RID: 19278
			private ITopologyConfigurationSession session;

			// Token: 0x04004B4F RID: 19279
			private bool disposed;
		}
	}
}
