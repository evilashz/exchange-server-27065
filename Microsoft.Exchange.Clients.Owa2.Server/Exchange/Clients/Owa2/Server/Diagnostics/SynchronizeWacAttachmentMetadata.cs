using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000464 RID: 1124
	internal enum SynchronizeWacAttachmentMetadata
	{
		// Token: 0x040015EC RID: 5612
		[DisplayName("SWA", "R")]
		Result,
		// Token: 0x040015ED RID: 5613
		[DisplayName("SWA", "C")]
		Count,
		// Token: 0x040015EE RID: 5614
		[DisplayName("SWA", "ESID")]
		SessionId
	}
}
