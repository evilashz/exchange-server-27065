using System;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.HttpProxy.Routing.Providers;
using Microsoft.Exchange.SharedCache.Client;

namespace Microsoft.Exchange.HttpProxy.RouteSelector
{
	// Token: 0x0200000B RID: 11
	public class RouteSelector : IServerLocatorFactory
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002EAE File Offset: 0x000010AE
		public RouteSelector()
		{
			this.InitializeStaticSharedCacheClients();
			this.Initialize(RouteSelector.anchorMailboxSharedCacheClientInstance, RouteSelector.mailboxServerSharedCacheClientInstance, RouteSelector.locatorServiceLookupFactoryInstance);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002ED1 File Offset: 0x000010D1
		public RouteSelector(ISharedCacheClient anchorMailboxSharedCacheClient, ISharedCacheClient mailboxServerSharedCacheClient, IRoutingLookupFactory locatorServiceLookupFactory)
		{
			if (anchorMailboxSharedCacheClient == null)
			{
				throw new ArgumentNullException("anchorMailboxCacheClient");
			}
			if (mailboxServerSharedCacheClient == null)
			{
				throw new ArgumentNullException("mailboxServerCacheClient");
			}
			if (locatorServiceLookupFactory == null)
			{
				throw new ArgumentNullException("locatorServiceLookupFactory");
			}
			this.Initialize(anchorMailboxSharedCacheClient, mailboxServerSharedCacheClient, locatorServiceLookupFactory);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002F0C File Offset: 0x0000110C
		IServerLocator IServerLocatorFactory.GetServerLocator(ProtocolType protocolType)
		{
			if (this.locatorInstance == null)
			{
				this.locatorInstance = new ServerLocator(this.anchorMailboxSharedCacheClient, this.mailboxServerSharedCacheClient, this.locatorServiceLookupFactory);
			}
			return this.locatorInstance;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002F39 File Offset: 0x00001139
		private void Initialize(ISharedCacheClient anchorMailboxSharedCacheClient, ISharedCacheClient mailboxServerSharedCacheClient, IRoutingLookupFactory locatorServiceLookupFactory)
		{
			this.anchorMailboxSharedCacheClient = anchorMailboxSharedCacheClient;
			this.mailboxServerSharedCacheClient = mailboxServerSharedCacheClient;
			this.locatorServiceLookupFactory = locatorServiceLookupFactory;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002F50 File Offset: 0x00001150
		private void InitializeStaticSharedCacheClients()
		{
			if (RouteSelector.anchorMailboxSharedCacheClientInstance == null)
			{
				lock (RouteSelector.CacheClientLock)
				{
					if (RouteSelector.anchorMailboxSharedCacheClientInstance == null)
					{
						RouteSelector.anchorMailboxSharedCacheClientInstance = new SharedCacheClientWrapper(new SharedCacheClient(WellKnownSharedCache.AnchorMailboxCache, "RouteSelector_AnchorMailboxCache_" + HttpProxyGlobals.ProtocolType, 1000, false, ConcurrencyGuards.SharedCache));
						RouteSelector.mailboxServerSharedCacheClientInstance = new SharedCacheClientWrapper(new SharedCacheClient(WellKnownSharedCache.MailboxServerLocator, "RouteSelector_MailboxServerLocator_" + HttpProxyGlobals.ProtocolType, 1000, false, ConcurrencyGuards.SharedCache));
						RouteSelector.locatorServiceLookupFactoryInstance = new RoutingEntryLookupFactory(new MailboxServerLocatorServiceProvider(), new ActiveDirectoryUserProvider(false));
					}
				}
			}
		}

		// Token: 0x0400000F RID: 15
		private const int CacheTimeoutMilliseconds = 1000;

		// Token: 0x04000010 RID: 16
		private static readonly object CacheClientLock = new object();

		// Token: 0x04000011 RID: 17
		private static ISharedCacheClient anchorMailboxSharedCacheClientInstance;

		// Token: 0x04000012 RID: 18
		private static ISharedCacheClient mailboxServerSharedCacheClientInstance;

		// Token: 0x04000013 RID: 19
		private static IRoutingLookupFactory locatorServiceLookupFactoryInstance;

		// Token: 0x04000014 RID: 20
		private ServerLocator locatorInstance;

		// Token: 0x04000015 RID: 21
		private ISharedCacheClient anchorMailboxSharedCacheClient;

		// Token: 0x04000016 RID: 22
		private ISharedCacheClient mailboxServerSharedCacheClient;

		// Token: 0x04000017 RID: 23
		private IRoutingLookupFactory locatorServiceLookupFactory;
	}
}
