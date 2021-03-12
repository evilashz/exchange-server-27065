using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001D5 RID: 469
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchInvalidPagination : DiscoverySearchPermanentException
	{
		// Token: 0x06000C59 RID: 3161 RVA: 0x000356C9 File Offset: 0x000338C9
		public DiscoverySearchInvalidPagination() : base(Strings.SearchInvalidPagination)
		{
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x000356D6 File Offset: 0x000338D6
		protected DiscoverySearchInvalidPagination(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
