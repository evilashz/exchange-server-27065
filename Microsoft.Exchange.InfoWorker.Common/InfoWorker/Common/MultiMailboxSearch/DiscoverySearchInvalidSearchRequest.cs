using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001DE RID: 478
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchInvalidSearchRequest : DiscoverySearchPermanentException
	{
		// Token: 0x06000C67 RID: 3175 RVA: 0x0003578F File Offset: 0x0003398F
		public DiscoverySearchInvalidSearchRequest(Guid mdbGuid, string server, Exception innerException) : base(Strings.InvalidSearchRequest(mdbGuid.ToString(), server), innerException)
		{
		}
	}
}
