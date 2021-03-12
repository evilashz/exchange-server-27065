using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingLookups
{
	// Token: 0x02000039 RID: 57
	internal class ExternalDirectoryObjectIdSharedCacheLookup : AnchorMailboxSharedCacheLookup
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00004142 File Offset: 0x00002342
		public ExternalDirectoryObjectIdSharedCacheLookup(ISharedCache sharedCache) : base(sharedCache, RoutingItemType.ExternalDirectoryObjectId)
		{
		}
	}
}
