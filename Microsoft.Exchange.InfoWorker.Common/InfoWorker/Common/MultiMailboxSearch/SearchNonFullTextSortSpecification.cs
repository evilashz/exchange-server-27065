using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001D8 RID: 472
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchNonFullTextSortSpecification : DiscoverySearchPermanentException
	{
		// Token: 0x06000C5F RID: 3167 RVA: 0x00035710 File Offset: 0x00033910
		public SearchNonFullTextSortSpecification(string sortBy) : base(Strings.SearchNonFullTextSortByProperty(sortBy))
		{
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0003571E File Offset: 0x0003391E
		protected SearchNonFullTextSortSpecification(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
