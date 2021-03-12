using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001E0 RID: 480
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchAbortedException : DiscoverySearchPermanentException
	{
		// Token: 0x06000C69 RID: 3177 RVA: 0x000357C7 File Offset: 0x000339C7
		public DiscoverySearchAbortedException(Guid queryCorrelationId, Guid mdbGuid, string server) : base(Strings.DiscoverySearchAborted(queryCorrelationId.ToString(), mdbGuid.ToString(), server))
		{
		}
	}
}
