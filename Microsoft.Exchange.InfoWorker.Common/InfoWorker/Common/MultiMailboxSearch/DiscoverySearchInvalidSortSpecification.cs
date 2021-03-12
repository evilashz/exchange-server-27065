using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001D7 RID: 471
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchInvalidSortSpecification : DiscoverySearchPermanentException
	{
		// Token: 0x06000C5D RID: 3165 RVA: 0x000356F8 File Offset: 0x000338F8
		public DiscoverySearchInvalidSortSpecification(string sortBy) : base(Strings.SearchInvalidSortSpecification(sortBy))
		{
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00035706 File Offset: 0x00033906
		protected DiscoverySearchInvalidSortSpecification(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
