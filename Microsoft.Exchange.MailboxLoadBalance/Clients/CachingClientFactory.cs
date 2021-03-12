using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.LoadBalance;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.Clients
{
	// Token: 0x02000031 RID: 49
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CachingClientFactory : IClientFactory
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00007044 File Offset: 0x00005244
		public CachingClientFactory(TimeSpan cacheDuration, IClientFactory clientFactory, ILogger logger)
		{
			this.physicalDatabaseCache = new ExactTimeoutCache<Guid, CachedPhysicalDatabase>(new RemoveItemDelegate<Guid, CachedPhysicalDatabase>(this.RemoveClient<Guid, CachedPhysicalDatabase>), new ShouldRemoveDelegate<Guid, CachedPhysicalDatabase>(this.ShouldRemoveEntry<Guid, CachedPhysicalDatabase>), null, 10000, true, CacheFullBehavior.ExpireExisting);
			this.loadBalanceClientCache = new ExactTimeoutCache<Guid, CachedLoadBalanceClient>(new RemoveItemDelegate<Guid, CachedLoadBalanceClient>(this.RemoveClient<Guid, CachedLoadBalanceClient>), new ShouldRemoveDelegate<Guid, CachedLoadBalanceClient>(this.ShouldRemoveEntry<Guid, CachedLoadBalanceClient>), null, 10000, true, CacheFullBehavior.ExpireExisting);
			this.injectorClientCache = new ExactTimeoutCache<Guid, CachedInjectorClient>(new RemoveItemDelegate<Guid, CachedInjectorClient>(this.RemoveClient<Guid, CachedInjectorClient>), new ShouldRemoveDelegate<Guid, CachedInjectorClient>(this.ShouldRemoveEntry<Guid, CachedInjectorClient>), null, 10000, true, CacheFullBehavior.ExpireExisting);
			this.cacheDuration = cacheDuration;
			this.clientFactory = clientFactory;
			this.logger = logger;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007118 File Offset: 0x00005318
		ILoadBalanceService IClientFactory.GetLoadBalanceClientForServer(DirectoryServer server, bool allowFallbackToLocal)
		{
			return this.GetClient<CachedLoadBalanceClient, DirectoryServer>(this.loadBalanceClientCache, server, () => new CachedLoadBalanceClient(this.clientFactory.GetLoadBalanceClientForServer(server, allowFallbackToLocal)));
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007184 File Offset: 0x00005384
		ILoadBalanceService IClientFactory.GetLoadBalanceClientForDatabase(DirectoryDatabase database)
		{
			return this.GetClient<CachedLoadBalanceClient, DirectoryDatabase>(this.loadBalanceClientCache, database, () => new CachedLoadBalanceClient(this.clientFactory.GetLoadBalanceClientForDatabase(database)));
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000071E8 File Offset: 0x000053E8
		IInjectorService IClientFactory.GetInjectorClientForDatabase(DirectoryDatabase database)
		{
			return this.GetClient<CachedInjectorClient, DirectoryDatabase>(this.injectorClientCache, database, () => new CachedInjectorClient(this.clientFactory.GetInjectorClientForDatabase(database)));
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000724C File Offset: 0x0000544C
		IPhysicalDatabase IClientFactory.GetPhysicalDatabaseConnection(DirectoryDatabase database)
		{
			return this.GetClient<CachedPhysicalDatabase, DirectoryDatabase>(this.physicalDatabaseCache, database, () => new CachedPhysicalDatabase(this.clientFactory.GetPhysicalDatabaseConnection(database)));
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000728B File Offset: 0x0000548B
		ILoadBalanceService IClientFactory.GetLoadBalanceClientForCentralServer()
		{
			return this.clientFactory.GetLoadBalanceClientForCentralServer();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007298 File Offset: 0x00005498
		private void RemoveClient<TKey, TCachedClient>(TKey key, TCachedClient value, RemoveReason reason) where TCachedClient : CachedClient
		{
			this.logger.Log(MigrationEventType.Instrumentation, "Removing {0} entry for {1}.", new object[]
			{
				value.GetType().Name,
				key
			});
			value.Cleanup();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000072EC File Offset: 0x000054EC
		private bool ShouldRemoveEntry<TKey, TCachedClient>(TKey key, TCachedClient value) where TCachedClient : CachedClient
		{
			bool canRemoveFromCache = value.CanRemoveFromCache;
			this.logger.Log(MigrationEventType.Instrumentation, "Can remove {0} entry for {1}? {2}.", new object[]
			{
				value.GetType().Name,
				key,
				canRemoveFromCache
			});
			return canRemoveFromCache;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007348 File Offset: 0x00005548
		private TCacheEntry GetClient<TCacheEntry, TKey>(ExactTimeoutCache<Guid, TCacheEntry> cache, TKey key, Func<TCacheEntry> createNewEntry) where TCacheEntry : CachedClient where TKey : DirectoryObject
		{
			TCacheEntry tcacheEntry;
			if (cache.TryGetValue(key.Guid, out tcacheEntry))
			{
				if (tcacheEntry.IsValid)
				{
					this.logger.Log(MigrationEventType.Instrumentation, "Returning cached {0} client for {1}", new object[]
					{
						typeof(TCacheEntry).Name,
						key.Name
					});
					tcacheEntry.IncrementReferenceCount();
					return tcacheEntry;
				}
				cache.Remove(key.Guid);
			}
			this.logger.Log(MigrationEventType.Instrumentation, "Creating new {0} client for {1}", new object[]
			{
				typeof(TCacheEntry).Name,
				key.Name
			});
			tcacheEntry = createNewEntry();
			cache.TryInsertSliding(key.Guid, tcacheEntry, this.cacheDuration);
			tcacheEntry.IncrementReferenceCount();
			return tcacheEntry;
		}

		// Token: 0x04000093 RID: 147
		private readonly TimeSpan cacheDuration;

		// Token: 0x04000094 RID: 148
		private readonly IClientFactory clientFactory;

		// Token: 0x04000095 RID: 149
		private readonly ILogger logger;

		// Token: 0x04000096 RID: 150
		private readonly ExactTimeoutCache<Guid, CachedPhysicalDatabase> physicalDatabaseCache;

		// Token: 0x04000097 RID: 151
		private readonly ExactTimeoutCache<Guid, CachedLoadBalanceClient> loadBalanceClientCache;

		// Token: 0x04000098 RID: 152
		private readonly ExactTimeoutCache<Guid, CachedInjectorClient> injectorClientCache;
	}
}
