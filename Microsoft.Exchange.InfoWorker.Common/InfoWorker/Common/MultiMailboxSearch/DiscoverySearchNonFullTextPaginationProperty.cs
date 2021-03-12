using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001D6 RID: 470
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchNonFullTextPaginationProperty : DiscoverySearchPermanentException
	{
		// Token: 0x06000C5B RID: 3163 RVA: 0x000356E0 File Offset: 0x000338E0
		public DiscoverySearchNonFullTextPaginationProperty(string paginationClause) : base(Strings.SearchNonFullTextPaginationProperty(paginationClause))
		{
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x000356EE File Offset: 0x000338EE
		protected DiscoverySearchNonFullTextPaginationProperty(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
