using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001C9 RID: 457
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchMaxSearchesExceeded : MultiMailboxSearchException
	{
		// Token: 0x06000C3F RID: 3135 RVA: 0x00035521 File Offset: 0x00033721
		public DiscoverySearchMaxSearchesExceeded(Guid databaseId) : base(Strings.SearchAdminRpcCallMaxSearches(databaseId.ToString()))
		{
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0003553B File Offset: 0x0003373B
		protected DiscoverySearchMaxSearchesExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
