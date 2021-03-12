using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001CA RID: 458
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchAccessDenied : MultiMailboxSearchException
	{
		// Token: 0x06000C41 RID: 3137 RVA: 0x00035545 File Offset: 0x00033745
		public DiscoverySearchAccessDenied(string displayName, Guid databaseId) : base(Strings.SearchAdminRpcCallAccessDenied(displayName, databaseId.ToString()))
		{
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00035560 File Offset: 0x00033760
		protected DiscoverySearchAccessDenied(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
