using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001DA RID: 474
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class FailedToFetchPreviewItems : MultiMailboxSearchException
	{
		// Token: 0x06000C63 RID: 3171 RVA: 0x00035756 File Offset: 0x00033956
		public FailedToFetchPreviewItems(string mailboxName) : base(Strings.FailedToFetchPreviewItems(mailboxName))
		{
		}
	}
}
