using System;

namespace Microsoft.Exchange.Data.Search.AqsParser
{
	// Token: 0x02000CF7 RID: 3319
	internal enum PropertyKeyword
	{
		// Token: 0x04004FBA RID: 20410
		[CIKeyword]
		Subject,
		// Token: 0x04004FBB RID: 20411
		[CIKeyword]
		Body,
		// Token: 0x04004FBC RID: 20412
		[CIKeyword]
		Attachment,
		// Token: 0x04004FBD RID: 20413
		[CIKeyword]
		Sent,
		// Token: 0x04004FBE RID: 20414
		[CIKeyword]
		Received,
		// Token: 0x04004FBF RID: 20415
		[CIKeyword]
		To,
		// Token: 0x04004FC0 RID: 20416
		[CIKeyword]
		From,
		// Token: 0x04004FC1 RID: 20417
		[CIKeyword]
		Cc,
		// Token: 0x04004FC2 RID: 20418
		[CIKeyword]
		Bcc,
		// Token: 0x04004FC3 RID: 20419
		[CIKeyword]
		Participants,
		// Token: 0x04004FC4 RID: 20420
		[CIKeyword]
		Recipients,
		// Token: 0x04004FC5 RID: 20421
		[BasicKeyword]
		[CIKeyword]
		Kind,
		// Token: 0x04004FC6 RID: 20422
		PolicyTag,
		// Token: 0x04004FC7 RID: 20423
		Expires,
		// Token: 0x04004FC8 RID: 20424
		HasAttachment,
		// Token: 0x04004FC9 RID: 20425
		[CIKeyword]
		[BasicKeyword]
		Category,
		// Token: 0x04004FCA RID: 20426
		IsFlagged,
		// Token: 0x04004FCB RID: 20427
		IsRead,
		// Token: 0x04004FCC RID: 20428
		[CIKeyword]
		Importance,
		// Token: 0x04004FCD RID: 20429
		[BasicKeyword]
		[CIKeyword]
		Size,
		// Token: 0x04004FCE RID: 20430
		[BasicKeyword]
		[CIKeyword]
		All,
		// Token: 0x04004FCF RID: 20431
		[CIKeyword]
		AttachmentNames
	}
}
