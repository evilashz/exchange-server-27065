using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingLookups
{
	// Token: 0x0200003D RID: 61
	internal class MailboxGuidSharedCacheLookup : AnchorMailboxSharedCacheLookup
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x0000420D File Offset: 0x0000240D
		public MailboxGuidSharedCacheLookup(ISharedCache sharedCache) : base(sharedCache, RoutingItemType.MailboxGuid)
		{
		}
	}
}
