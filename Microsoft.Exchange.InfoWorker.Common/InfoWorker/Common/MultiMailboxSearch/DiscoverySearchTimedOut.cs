using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001D2 RID: 466
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchTimedOut : MultiMailboxSearchException
	{
		// Token: 0x06000C53 RID: 3155 RVA: 0x0003564B File Offset: 0x0003384B
		public DiscoverySearchTimedOut(int mailboxesCount, Guid databaseId, string aqs) : base(Strings.SearchAdminRpcSearchCallTimedout("preview", mailboxesCount, databaseId.ToString(), aqs))
		{
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0003566C File Offset: 0x0003386C
		protected DiscoverySearchTimedOut(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
