using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200005D RID: 93
	internal enum UpdateAndPostModernGroupItemMetadata
	{
		// Token: 0x04000519 RID: 1305
		[DisplayName("UAPMGI", "CT")]
		ConversationTopic,
		// Token: 0x0400051A RID: 1306
		[DisplayName("UAPMGI", "CI")]
		ConversationId,
		// Token: 0x0400051B RID: 1307
		[DisplayName("UAPMGI", "TRC")]
		ToRecipientCount,
		// Token: 0x0400051C RID: 1308
		[DisplayName("UAPMGI", "CRC")]
		CcRecipientCount
	}
}
