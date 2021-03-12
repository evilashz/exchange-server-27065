using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001D4 RID: 468
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchNonFullTextQuery : DiscoverySearchPermanentException
	{
		// Token: 0x06000C57 RID: 3159 RVA: 0x000356A1 File Offset: 0x000338A1
		public DiscoverySearchNonFullTextQuery(SearchType searchType, string query) : base(Strings.SearchAdminRpcInvalidQuery((searchType == SearchType.Preview) ? "Preview" : "Statistics", query))
		{
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x000356BF File Offset: 0x000338BF
		protected DiscoverySearchNonFullTextQuery(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
