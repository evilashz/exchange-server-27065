using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingLookups
{
	// Token: 0x02000035 RID: 53
	internal class ArchiveSmtpSharedCacheLookup : AnchorMailboxSharedCacheLookup
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00003E97 File Offset: 0x00002097
		public ArchiveSmtpSharedCacheLookup(ISharedCache sharedCache) : base(sharedCache, RoutingItemType.ArchiveSmtp)
		{
		}
	}
}
