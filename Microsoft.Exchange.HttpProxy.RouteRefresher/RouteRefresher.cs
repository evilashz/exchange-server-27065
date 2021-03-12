using System;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.HttpProxy.RouteSelector;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.HttpProxy.Routing.Serialization;
using Microsoft.Exchange.SharedCache.Client;

namespace Microsoft.Exchange.HttpProxy.RouteRefresher
{
	// Token: 0x02000004 RID: 4
	internal class RouteRefresher : IRouteRefresher
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020D0 File Offset: 0x000002D0
		public RouteRefresher(IRouteRefresherDiagnostics diagnostics)
		{
			RouteRefresher.InitializeStaticCacheClients();
			this.Initialize(diagnostics, RouteRefresher.anchorMailboxCacheClientInstance, RouteRefresher.mailboxServerCacheClientInstance);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020EE File Offset: 0x000002EE
		public RouteRefresher(ISharedCacheClient anchorMailboxCacheClient, ISharedCacheClient mailboxServerCacheClient, IRouteRefresherDiagnostics diagnostics)
		{
			this.Initialize(diagnostics, anchorMailboxCacheClient, mailboxServerCacheClient);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002100 File Offset: 0x00000300
		public void ProcessRoutingUpdates(string headerValue)
		{
			if (string.IsNullOrEmpty(headerValue))
			{
				throw new ArgumentNullException("headerValue");
			}
			string[] array = headerValue.Split(new char[]
			{
				','
			});
			foreach (string text in array)
			{
				try
				{
					IRoutingEntry routingEntry = RoutingEntryHeaderSerializer.Deserialize(text);
					if (ServerLocator.IsMailboxServerCacheKey(routingEntry.Key) && routingEntry.Destination.RoutingItemType == RoutingItemType.Server)
					{
						this.routeRefresherDiagnostics.IncrementTotalMailboxServerCacheUpdateAttempts();
						if (this.mailboxServerCacheClient.AddEntry(routingEntry))
						{
							string value = "MailboxServerCacheUpdate:" + text;
							this.routeRefresherDiagnostics.AddGenericInfo(value);
							this.routeRefresherDiagnostics.IncrementSuccessfulMailboxServerCacheUpdates();
						}
						else
						{
							string value2 = "MailboxServerCacheFailure:" + text;
							this.routeRefresherDiagnostics.AddErrorInfo(value2);
						}
					}
					else if (ServerLocator.IsAnchorMailboxCacheKey(routingEntry.Key) && routingEntry.Destination.RoutingItemType == RoutingItemType.DatabaseGuid)
					{
						this.routeRefresherDiagnostics.IncrementTotalAnchorMailboxCacheUpdateAttempts();
						if (this.anchorMailboxCacheClient.AddEntry(routingEntry))
						{
							string value3 = "AnchorMailboxCacheUpdate:" + text;
							this.routeRefresherDiagnostics.AddGenericInfo(value3);
							this.routeRefresherDiagnostics.IncrementSuccessfulAnchorMailboxCacheUpdates();
						}
						else
						{
							string value4 = "AnchorMailboxCacheFailure:" + text;
							this.routeRefresherDiagnostics.AddErrorInfo(value4);
						}
					}
					else
					{
						string value5 = "UnrecognizedRoutingEntry:" + text;
						this.routeRefresherDiagnostics.AddErrorInfo(value5);
					}
				}
				catch (ArgumentException)
				{
					string value6 = "DeserializationException:" + text;
					this.routeRefresherDiagnostics.AddErrorInfo(value6);
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022A8 File Offset: 0x000004A8
		public void Initialize(IRouteRefresherDiagnostics diagnostics, ISharedCacheClient anchorMailboxCacheClient, ISharedCacheClient mailboxServerCacheClient)
		{
			this.routeRefresherDiagnostics = diagnostics;
			this.anchorMailboxCacheClient = anchorMailboxCacheClient;
			this.mailboxServerCacheClient = mailboxServerCacheClient;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C0 File Offset: 0x000004C0
		private static void InitializeStaticCacheClients()
		{
			if (RouteRefresher.anchorMailboxCacheClientInstance == null)
			{
				lock (RouteRefresher.CacheClientLock)
				{
					if (RouteRefresher.anchorMailboxCacheClientInstance == null)
					{
						RouteRefresher.anchorMailboxCacheClientInstance = new SharedCacheClientWrapper(new SharedCacheClient(WellKnownSharedCache.AnchorMailboxCache, "RouteRefresher_AnchorMailboxCache_" + HttpProxyGlobals.ProtocolType, 1000, false, ConcurrencyGuards.SharedCache));
						RouteRefresher.mailboxServerCacheClientInstance = new SharedCacheClientWrapper(new SharedCacheClient(WellKnownSharedCache.MailboxServerLocator, "RouteRefresher_MailboxServerLocator_" + HttpProxyGlobals.ProtocolType, 1000, false, ConcurrencyGuards.SharedCache));
					}
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private const int CacheTimeoutMilliseconds = 1000;

		// Token: 0x04000002 RID: 2
		private static readonly object CacheClientLock = new object();

		// Token: 0x04000003 RID: 3
		private static ISharedCacheClient anchorMailboxCacheClientInstance;

		// Token: 0x04000004 RID: 4
		private static ISharedCacheClient mailboxServerCacheClientInstance;

		// Token: 0x04000005 RID: 5
		private ISharedCacheClient anchorMailboxCacheClient;

		// Token: 0x04000006 RID: 6
		private ISharedCacheClient mailboxServerCacheClient;

		// Token: 0x04000007 RID: 7
		private IRouteRefresherDiagnostics routeRefresherDiagnostics;
	}
}
