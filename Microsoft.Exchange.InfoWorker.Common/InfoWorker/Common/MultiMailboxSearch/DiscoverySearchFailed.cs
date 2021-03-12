using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001C8 RID: 456
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchFailed : MultiMailboxSearchException
	{
		// Token: 0x06000C3C RID: 3132 RVA: 0x000354E0 File Offset: 0x000336E0
		public DiscoverySearchFailed(Guid databaseId, int errorCode) : base(Strings.SearchAdminRpcCallFailed(databaseId.ToString(), errorCode))
		{
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x000354FB File Offset: 0x000336FB
		public DiscoverySearchFailed(Guid databaseId, int errorCode, Exception innerException) : base(Strings.SearchAdminRpcCallFailed(databaseId.ToString(), errorCode), innerException)
		{
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00035517 File Offset: 0x00033717
		protected DiscoverySearchFailed(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
