using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001D3 RID: 467
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoveryKeywordStatsSearchTimedOut : MultiMailboxSearchException
	{
		// Token: 0x06000C55 RID: 3157 RVA: 0x00035676 File Offset: 0x00033876
		public DiscoveryKeywordStatsSearchTimedOut(int mailboxesCount, Guid databaseId, string aqs) : base(Strings.SearchAdminRpcSearchCallTimedout("keyword stats", mailboxesCount, databaseId.ToString(), aqs))
		{
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00035697 File Offset: 0x00033897
		protected DiscoveryKeywordStatsSearchTimedOut(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
