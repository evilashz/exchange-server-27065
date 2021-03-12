using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001DF RID: 479
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchInvalidKeywordStatsRequest : DiscoverySearchPermanentException
	{
		// Token: 0x06000C68 RID: 3176 RVA: 0x000357AB File Offset: 0x000339AB
		public DiscoverySearchInvalidKeywordStatsRequest(Guid mdbGuid, string server, Exception innerException) : base(Strings.InvalidKeywordStatsRequest(mdbGuid.ToString(), server), innerException)
		{
		}
	}
}
