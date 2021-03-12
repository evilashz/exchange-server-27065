using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B86 RID: 2950
	internal enum ConditionOrder
	{
		// Token: 0x04003C92 RID: 15506
		SentToMeCondition = 1,
		// Token: 0x04003C93 RID: 15507
		SentOnlyToMeCondition,
		// Token: 0x04003C94 RID: 15508
		SentCcMeCondition,
		// Token: 0x04003C95 RID: 15509
		SentToOrCcMeCondition,
		// Token: 0x04003C96 RID: 15510
		NotSentToMeCondition,
		// Token: 0x04003C97 RID: 15511
		FromRecipientsCondition,
		// Token: 0x04003C98 RID: 15512
		SentToRecipientsCondition,
		// Token: 0x04003C99 RID: 15513
		ContainsSubjectStringCondition,
		// Token: 0x04003C9A RID: 15514
		ContainsBodyStringCondition,
		// Token: 0x04003C9B RID: 15515
		ContainsSubjectOrBodyStringCondition,
		// Token: 0x04003C9C RID: 15516
		ContainsHeaderStringCondition,
		// Token: 0x04003C9D RID: 15517
		MarkedAsImportanceCondition,
		// Token: 0x04003C9E RID: 15518
		MarkedAsSensitivityCondition,
		// Token: 0x04003C9F RID: 15519
		MarkedAsOofCondition,
		// Token: 0x04003CA0 RID: 15520
		HasAttachmentCondition,
		// Token: 0x04003CA1 RID: 15521
		WithinSizeRangeCondition,
		// Token: 0x04003CA2 RID: 15522
		WithinDateRangeCondition,
		// Token: 0x04003CA3 RID: 15523
		ContainsSenderStringCondition,
		// Token: 0x04003CA4 RID: 15524
		MeetingMessageCondition,
		// Token: 0x04003CA5 RID: 15525
		MeetingResponseCondition,
		// Token: 0x04003CA6 RID: 15526
		ContainsRecipientStringCondition,
		// Token: 0x04003CA7 RID: 15527
		AssignedCategoriesCondition,
		// Token: 0x04003CA8 RID: 15528
		FormsCondition,
		// Token: 0x04003CA9 RID: 15529
		MessageClassificationCondition,
		// Token: 0x04003CAA RID: 15530
		NdrCondition,
		// Token: 0x04003CAB RID: 15531
		AutomaticForwardCondition,
		// Token: 0x04003CAC RID: 15532
		EncryptedCondition,
		// Token: 0x04003CAD RID: 15533
		SignedCondition,
		// Token: 0x04003CAE RID: 15534
		ReadReceiptCondition,
		// Token: 0x04003CAF RID: 15535
		PermissionControlledCondition,
		// Token: 0x04003CB0 RID: 15536
		ApprovalRequestCondition,
		// Token: 0x04003CB1 RID: 15537
		VoicemailCondition,
		// Token: 0x04003CB2 RID: 15538
		FlaggedForActionCondition,
		// Token: 0x04003CB3 RID: 15539
		FromSubscriptionCondition
	}
}
