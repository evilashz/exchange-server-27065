using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000088 RID: 136
	internal enum InferredImportanceReason : uint
	{
		// Token: 0x0400019C RID: 412
		[Description("FeatureBias")]
		FeatureBias,
		// Token: 0x0400019D RID: 413
		[Description("UserOverride")]
		UserOverride,
		// Token: 0x0400019E RID: 414
		[Description("IsMarkedHighImportanceBySender")]
		IsMarkedHighImportanceBySender,
		// Token: 0x0400019F RID: 415
		[Description("IsMarkedLowImportanceBySender")]
		IsMarkedLowImportanceBySender,
		// Token: 0x040001A0 RID: 416
		[Description("SenderIsYou")]
		SenderIsYou,
		// Token: 0x040001A1 RID: 417
		[Description("ConversationStarterIsYou")]
		ConversationStarterIsYou,
		// Token: 0x040001A2 RID: 418
		[Description("OnlyRecipient")]
		OnlyRecipient,
		// Token: 0x040001A3 RID: 419
		[Description("IsFlagged")]
		IsFlagged,
		// Token: 0x040001A4 RID: 420
		[Description("HasAttachment")]
		HasAttachment,
		// Token: 0x040001A5 RID: 421
		[Description("SenderIsManager")]
		SenderIsManager,
		// Token: 0x040001A6 RID: 422
		[Description("SenderIsDirectReport")]
		SenderIsDirectReport,
		// Token: 0x040001A7 RID: 423
		[Description("SenderIsAutomated")]
		SenderIsAutomated,
		// Token: 0x040001A8 RID: 424
		[Description("HasMultipleRecipients")]
		HasMultipleRecipients,
		// Token: 0x040001A9 RID: 425
		[Description("SubjectPrefix")]
		SubjectPrefix,
		// Token: 0x040001AA RID: 426
		[Description("SubjectContent")]
		SubjectContent,
		// Token: 0x040001AB RID: 427
		[Description("RecipientPosition")]
		RecipientPosition,
		// Token: 0x040001AC RID: 428
		[Description("ConversationContributions")]
		ConversationContributions,
		// Token: 0x040001AD RID: 429
		[Description("ManagerPosition")]
		ManagerPosition,
		// Token: 0x040001AE RID: 430
		[Description("RecipientCountOnToLine")]
		RecipientCountOnToLine,
		// Token: 0x040001AF RID: 431
		[Description("RecipientCountOnCcLine")]
		RecipientCountOnCcLine,
		// Token: 0x040001B0 RID: 432
		[Description("ReplyToAMessageFromMe")]
		ReplyToAMessageFromMe,
		// Token: 0x040001B1 RID: 433
		[Description("PreviousUnread")]
		PreviousUnread,
		// Token: 0x040001B2 RID: 434
		[Description("PreviousFlagged")]
		PreviousFlagged,
		// Token: 0x040001B3 RID: 435
		[Description("SwitchedToToLineFirstTime")]
		SwitchedToToLineFirstTime,
		// Token: 0x040001B4 RID: 436
		[Description("ItemClass")]
		ItemClass,
		// Token: 0x040001B5 RID: 437
		[Description("IsResponseRequested")]
		IsResponseRequested,
		// Token: 0x040001B6 RID: 438
		[Description("NumberOfFileAttachments")]
		NumberOfFileAttachments,
		// Token: 0x040001B7 RID: 439
		[Description("NumberOfMessageAttachments")]
		NumberOfMessageAttachments,
		// Token: 0x040001B8 RID: 440
		[Description("NumberOfInlineAttachments")]
		NumberOfInlineAttachments,
		// Token: 0x040001B9 RID: 441
		[Description("NewSender")]
		NewSender,
		// Token: 0x040001BA RID: 442
		[Description("NewConversationStarter")]
		NewConversationStarter,
		// Token: 0x040001BB RID: 443
		[Description("TimeSinceLastSentToSender")]
		TimeSinceLastSentToSender,
		// Token: 0x040001BC RID: 444
		[Description("IsTaskPresent")]
		IsTaskPresent,
		// Token: 0x040001BD RID: 445
		[Description("IsMeetingPresent")]
		IsMeetingPresent,
		// Token: 0x040001BE RID: 446
		[Description("IsQuestionPresent")]
		IsQuestionPresent,
		// Token: 0x040001BF RID: 447
		[Description("QuestionWithRecipientOntheToOrCcLine")]
		QuestionWithRecipientOntheToOrCcLine,
		// Token: 0x040001C0 RID: 448
		[Description("SubjectLength")]
		SubjectLength,
		// Token: 0x040001C1 RID: 449
		[Description("UniqueBodyLength")]
		UniqueBodyLength,
		// Token: 0x040001C2 RID: 450
		[Description("WordsInUniqueBody")]
		WordsInUniqueBody,
		// Token: 0x040001C3 RID: 451
		[Description("BigramsInUniqueBody")]
		BigramsInUniqueBody,
		// Token: 0x040001C4 RID: 452
		[Description("IsEndOfConversation")]
		IsEndOfConversation,
		// Token: 0x040001C5 RID: 453
		[Description("Sender")]
		Sender = 2147483648U,
		// Token: 0x040001C6 RID: 454
		[Description("SenderCc")]
		SenderCc,
		// Token: 0x040001C7 RID: 455
		[Description("ConversationStarter")]
		ConversationStarter = 2147483664U,
		// Token: 0x040001C8 RID: 456
		[Description("ConversationStarterCc")]
		ConversationStarterCc,
		// Token: 0x040001C9 RID: 457
		[Description("Keywords")]
		Keywords = 2147483904U,
		// Token: 0x040001CA RID: 458
		[Description("SenderDList")]
		SenderDList,
		// Token: 0x040001CB RID: 459
		[Description("ConversationStarterDList")]
		ConversationStarterDList = 2147483920U,
		// Token: 0x040001CC RID: 460
		[Description("RecipientOnToLine")]
		RecipientOnToLine,
		// Token: 0x040001CD RID: 461
		[Description("RecipientOnCcLine")]
		RecipientOnCcLine = 2147487744U
	}
}
