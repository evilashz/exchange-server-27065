using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001DD RID: 477
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchInvalidQuery : DiscoverySearchPermanentException
	{
		// Token: 0x06000C66 RID: 3174 RVA: 0x00035782 File Offset: 0x00033982
		public DiscoverySearchInvalidQuery() : base(Strings.InvalidSearchQuery)
		{
		}
	}
}
