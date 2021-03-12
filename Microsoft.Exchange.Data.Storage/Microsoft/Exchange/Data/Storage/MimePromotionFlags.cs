using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005E1 RID: 1505
	[Flags]
	internal enum MimePromotionFlags
	{
		// Token: 0x04002133 RID: 8499
		Default = 0,
		// Token: 0x04002134 RID: 8500
		SkipMessageHeaders = 1,
		// Token: 0x04002135 RID: 8501
		SkipMessageBody = 2,
		// Token: 0x04002136 RID: 8502
		SkipRegularAttachments = 4,
		// Token: 0x04002137 RID: 8503
		SkipInlineAttachments = 8,
		// Token: 0x04002138 RID: 8504
		SkipAllAttachments = 12,
		// Token: 0x04002139 RID: 8505
		AllFlags = 15,
		// Token: 0x0400213A RID: 8506
		PromoteHeadersOnly = 14,
		// Token: 0x0400213B RID: 8507
		PromoteBodyOnly = 13,
		// Token: 0x0400213C RID: 8508
		PromoteAttachmentsOnly = 3,
		// Token: 0x0400213D RID: 8509
		PromoteInlineAttachmentsOnly = 7
	}
}
