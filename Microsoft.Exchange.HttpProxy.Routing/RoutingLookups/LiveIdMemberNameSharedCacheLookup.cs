using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingLookups
{
	// Token: 0x0200003B RID: 59
	internal class LiveIdMemberNameSharedCacheLookup : AnchorMailboxSharedCacheLookup
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00004177 File Offset: 0x00002377
		public LiveIdMemberNameSharedCacheLookup(ISharedCache sharedCache) : base(sharedCache, RoutingItemType.LiveIdMemberName)
		{
		}
	}
}
