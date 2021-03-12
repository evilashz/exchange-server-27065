using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001DC RID: 476
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class NestedFanoutException : DiscoverySearchPermanentException
	{
		// Token: 0x06000C65 RID: 3173 RVA: 0x00035774 File Offset: 0x00033974
		public NestedFanoutException(string mailboxName) : base(Strings.NestedFanout(mailboxName))
		{
		}
	}
}
