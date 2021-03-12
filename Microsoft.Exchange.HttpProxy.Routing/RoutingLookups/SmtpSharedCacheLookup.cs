using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingLookups
{
	// Token: 0x02000040 RID: 64
	internal class SmtpSharedCacheLookup : AnchorMailboxSharedCacheLookup
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00004353 File Offset: 0x00002553
		public SmtpSharedCacheLookup(ISharedCache sharedCache) : base(sharedCache, RoutingItemType.Smtp)
		{
		}
	}
}
