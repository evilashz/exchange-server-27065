using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200004E RID: 78
	internal enum PostModernGroupItemMetadata
	{
		// Token: 0x04000484 RID: 1156
		[DisplayName("PMGI", "CT")]
		ConversationTopic,
		// Token: 0x04000485 RID: 1157
		[DisplayName("PMGI", "CI")]
		ConversationId,
		// Token: 0x04000486 RID: 1158
		[DisplayName("PMGI", "TRC")]
		ToRecipientCount,
		// Token: 0x04000487 RID: 1159
		[DisplayName("PMGI", "CRC")]
		CcRecipientCount,
		// Token: 0x04000488 RID: 1160
		[DisplayName("PMGI", "IR")]
		IsReplying,
		// Token: 0x04000489 RID: 1161
		[DisplayName("PMGI", "IRUD")]
		IsReplyingUsingDraft,
		// Token: 0x0400048A RID: 1162
		[DisplayName("PMGI", "PECL")]
		PreExecuteCommandLatency,
		// Token: 0x0400048B RID: 1163
		[DisplayName("PMGI", "MAC")]
		MissedAdCache
	}
}
