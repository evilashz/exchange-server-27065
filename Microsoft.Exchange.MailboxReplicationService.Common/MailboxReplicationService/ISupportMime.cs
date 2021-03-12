using System;
using System.IO;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001A5 RID: 421
	internal interface ISupportMime
	{
		// Token: 0x06000FB5 RID: 4021
		Stream GetMimeStream(MessageRec message, out PropValueData[] extraPropValues);

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000FB6 RID: 4022
		ResourceHealthTracker RHTracker { get; }
	}
}
