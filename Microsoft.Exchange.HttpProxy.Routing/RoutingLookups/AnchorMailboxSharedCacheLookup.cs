using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;
using Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations;
using Microsoft.Exchange.HttpProxy.Routing.RoutingEntries;
using Microsoft.Exchange.HttpProxy.Routing.Serialization;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingLookups
{
	// Token: 0x02000032 RID: 50
	internal abstract class AnchorMailboxSharedCacheLookup : IRoutingLookup
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00003B25 File Offset: 0x00001D25
		protected AnchorMailboxSharedCacheLookup(ISharedCache sharedCache, RoutingItemType validItemType)
		{
			if (sharedCache == null)
			{
				throw new ArgumentNullException("sharedCache");
			}
			this.sharedCache = sharedCache;
			this.validItemType = validItemType;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003B4C File Offset: 0x00001D4C
		IRoutingEntry IRoutingLookup.GetRoutingEntry(IRoutingKey routingKey, IRoutingDiagnostics diagnostics)
		{
			if (routingKey == null)
			{
				throw new ArgumentNullException("routingKey");
			}
			if (diagnostics == null)
			{
				throw new ArgumentNullException("diagnostics");
			}
			if (routingKey.RoutingItemType != this.validItemType)
			{
				string message = string.Format("Routing key type {0} is not supported in {1}", RoutingEntryHeaderSerializer.RoutingTypeToString(routingKey.RoutingItemType), base.GetType());
				throw new ArgumentException(message, "routingKey");
			}
			string sharedCacheKeyFromRoutingKey = this.sharedCache.GetSharedCacheKeyFromRoutingKey(routingKey);
			try
			{
				byte[] bytes;
				if (this.sharedCache.TryGet(sharedCacheKeyFromRoutingKey, out bytes, diagnostics))
				{
					AnchorMailboxCacheEntry anchorMailboxCacheEntry = new AnchorMailboxCacheEntry();
					anchorMailboxCacheEntry.FromByteArray(bytes);
					if (anchorMailboxCacheEntry.Database != null)
					{
						DatabaseGuidRoutingDestination destination = new DatabaseGuidRoutingDestination(anchorMailboxCacheEntry.Database.ObjectGuid, anchorMailboxCacheEntry.DomainName, anchorMailboxCacheEntry.Database.PartitionFQDN);
						return new SuccessfulMailboxRoutingEntry(routingKey, destination, DateTime.Now.ToFileTimeUtc());
					}
				}
			}
			catch (SharedCacheException ex)
			{
				ErrorRoutingDestination destination2 = new ErrorRoutingDestination(ex.Message);
				return new FailedMailboxRoutingEntry(routingKey, destination2, DateTime.UtcNow.ToFileTimeUtc());
			}
			return null;
		}

		// Token: 0x0400005C RID: 92
		private readonly ISharedCache sharedCache;

		// Token: 0x0400005D RID: 93
		private readonly RoutingItemType validItemType;
	}
}
