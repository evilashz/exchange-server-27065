using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001E1 RID: 481
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchCIFailure : MultiMailboxSearchException
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x000357EF File Offset: 0x000339EF
		public DiscoverySearchCIFailure(Guid databaseId, string server, int errorCode, Exception innerException) : base(Strings.DiscoverySearchCIFailure(databaseId.ToString(), server, errorCode), innerException)
		{
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0003580D File Offset: 0x00033A0D
		protected DiscoverySearchCIFailure(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
