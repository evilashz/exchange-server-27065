using System;
using Microsoft.Exchange.Common.Cache;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200062D RID: 1581
	internal class TenantConfigurationCache<TSettings> : IDisposable where TSettings : TenantConfigurationCacheableItemBase, new()
	{
		// Token: 0x06004AEC RID: 19180 RVA: 0x00114A3B File Offset: 0x00112C3B
		public TenantConfigurationCache(long cacheSize, TimeSpan cacheExpirationInterval, TimeSpan cacheCleanupInterval, ICacheTracer<OrganizationId> tracer, ICachePerformanceCounters perfCounters) : this(cacheSize, cacheExpirationInterval, cacheCleanupInterval, Cache<OrganizationId, TSettings>.DefaultPurgeInterval, tracer, perfCounters)
		{
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x00114A4F File Offset: 0x00112C4F
		public TenantConfigurationCache(long cacheSize, TimeSpan cacheExpirationInterval, TimeSpan cacheCleanupInterval, TimeSpan cachePurgeInterval, ICacheTracer<OrganizationId> tracer, ICachePerformanceCounters perfCounters)
		{
			this.cache = new Cache<OrganizationId, TSettings>(cacheSize, cacheExpirationInterval, cacheCleanupInterval, cachePurgeInterval, tracer, perfCounters);
			this.cache.OnRemoved += this.HandleCacheItemRemoved;
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x00114A84 File Offset: 0x00112C84
		public bool TryGetValue(OrganizationId orgId, out TSettings perTenantSettings)
		{
			bool flag;
			return this.TryGetValue(orgId, out perTenantSettings, out flag);
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x00114A9C File Offset: 0x00112C9C
		public bool TryGetValue(OrganizationId orgId, out TSettings perTenantSettings, object state)
		{
			bool flag;
			return this.TryGetValue(orgId, false, out perTenantSettings, out flag, state);
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x00114AB5 File Offset: 0x00112CB5
		public bool TryGetValue(OrganizationId orgId, out TSettings perTenantSettings, out bool hasExpired)
		{
			return this.TryGetValue(orgId, false, out perTenantSettings, out hasExpired, null);
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x00114AC2 File Offset: 0x00112CC2
		public bool TryGetValue(IConfigurationSession adSession, out TSettings perTenantSettings)
		{
			return this.TryGetPerTenantSettingsWithoutRegistrationAndCaching(adSession, false, out perTenantSettings);
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x00114AD0 File Offset: 0x00112CD0
		public virtual TSettings GetValue(OrganizationId orgId)
		{
			TSettings result;
			bool flag;
			this.TryGetValue(orgId, true, out result, out flag, null);
			return result;
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x00114AEC File Offset: 0x00112CEC
		public TSettings GetValue(IConfigurationSession adSession)
		{
			TSettings result;
			this.TryGetPerTenantSettingsWithoutRegistrationAndCaching(adSession, true, out result);
			return result;
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x00114B08 File Offset: 0x00112D08
		public TSettings GetValue(OrganizationId orgId, object state)
		{
			TSettings result;
			bool flag;
			this.TryGetValue(orgId, true, out result, out flag, state);
			return result;
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x00114B24 File Offset: 0x00112D24
		public bool ContainsInCache(OrganizationId organizationId)
		{
			TSettings tsettings;
			bool flag2;
			bool flag = this.cache.TryGetValue(organizationId, out tsettings, out flag2);
			return flag && !flag2;
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x00114B4B File Offset: 0x00112D4B
		public void Clear()
		{
			this.cache.Clear();
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x00114B58 File Offset: 0x00112D58
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x00114B67 File Offset: 0x00112D67
		internal void RemoveValue(OrganizationId orgId)
		{
			this.cache.Remove(orgId);
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x00114B75 File Offset: 0x00112D75
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.cache.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x00114B94 File Offset: 0x00112D94
		private bool TryGetValue(OrganizationId orgId, bool allowExceptions, out TSettings perTenantSettings, out bool hasExpired, object state)
		{
			perTenantSettings = default(TSettings);
			if (this.cache.TryGetValue(orgId, out perTenantSettings, out hasExpired))
			{
				TSettings tsettings;
				if (hasExpired && this.InitializeAndAddPerTenantSettings(orgId, allowExceptions, out tsettings, state))
				{
					perTenantSettings = tsettings;
				}
			}
			else if (!this.InitializeAndAddPerTenantSettings(orgId, allowExceptions, out perTenantSettings, state))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x00114BE8 File Offset: 0x00112DE8
		private bool InitializeAndAddPerTenantSettings(OrganizationId orgId, bool allowExceptions, out TSettings perTenantSettings, object state)
		{
			perTenantSettings = Activator.CreateInstance<TSettings>();
			bool flag = allowExceptions ? perTenantSettings.Initialize(orgId, new CacheNotificationHandler(this.RemoveValue), state) : perTenantSettings.TryInitialize(orgId, new CacheNotificationHandler(this.RemoveValue), state);
			if (flag)
			{
				if (!this.cache.TryAdd(orgId, perTenantSettings))
				{
					perTenantSettings.UnregisterChangeNotification();
				}
				return true;
			}
			return false;
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x00114C62 File Offset: 0x00112E62
		private bool TryGetPerTenantSettingsWithoutRegistrationAndCaching(IConfigurationSession adSession, bool allowExceptions, out TSettings perTenantSettings)
		{
			perTenantSettings = Activator.CreateInstance<TSettings>();
			return perTenantSettings.InitializeWithoutRegistration(adSession, allowExceptions);
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x00114C80 File Offset: 0x00112E80
		private void HandleCacheItemRemoved(object sender, OnRemovedEventArgs<OrganizationId, TSettings> e)
		{
			TSettings value = e.Value;
			value.UnregisterChangeNotification();
		}

		// Token: 0x04003399 RID: 13209
		private Cache<OrganizationId, TSettings> cache;

		// Token: 0x0400339A RID: 13210
		private bool disposed;
	}
}
