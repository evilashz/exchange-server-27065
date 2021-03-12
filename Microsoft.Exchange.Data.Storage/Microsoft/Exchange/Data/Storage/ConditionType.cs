using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B85 RID: 2949
	internal enum ConditionType
	{
		// Token: 0x04003C6F RID: 15471
		FromRecipientsCondition = 1,
		// Token: 0x04003C70 RID: 15472
		ContainsSubjectStringCondition,
		// Token: 0x04003C71 RID: 15473
		SentOnlyToMeCondition = 4,
		// Token: 0x04003C72 RID: 15474
		SentToMeCondition,
		// Token: 0x04003C73 RID: 15475
		MarkedAsImportanceCondition,
		// Token: 0x04003C74 RID: 15476
		MarkedAsSensitivityCondition,
		// Token: 0x04003C75 RID: 15477
		SentCcMeCondition = 9,
		// Token: 0x04003C76 RID: 15478
		SentToOrCcMeCondition,
		// Token: 0x04003C77 RID: 15479
		NotSentToMeCondition,
		// Token: 0x04003C78 RID: 15480
		SentToRecipientsCondition,
		// Token: 0x04003C79 RID: 15481
		ContainsBodyStringCondition,
		// Token: 0x04003C7A RID: 15482
		ContainsSubjectOrBodyStringCondition,
		// Token: 0x04003C7B RID: 15483
		ContainsHeaderStringCondition,
		// Token: 0x04003C7C RID: 15484
		ContainsSenderStringCondition = 17,
		// Token: 0x04003C7D RID: 15485
		MarkedAsOofCondition = 19,
		// Token: 0x04003C7E RID: 15486
		HasAttachmentCondition,
		// Token: 0x04003C7F RID: 15487
		WithinSizeRangeCondition,
		// Token: 0x04003C80 RID: 15488
		WithinDateRangeCondition,
		// Token: 0x04003C81 RID: 15489
		MeetingMessageCondition = 26,
		// Token: 0x04003C82 RID: 15490
		ContainsRecipientStringCondition,
		// Token: 0x04003C83 RID: 15491
		AssignedCategoriesCondition,
		// Token: 0x04003C84 RID: 15492
		FormsCondition,
		// Token: 0x04003C85 RID: 15493
		MessageClassificationCondition,
		// Token: 0x04003C86 RID: 15494
		NdrCondition,
		// Token: 0x04003C87 RID: 15495
		AutomaticForwardCondition,
		// Token: 0x04003C88 RID: 15496
		EncryptedCondition,
		// Token: 0x04003C89 RID: 15497
		SignedCondition,
		// Token: 0x04003C8A RID: 15498
		ReadReceiptCondition,
		// Token: 0x04003C8B RID: 15499
		MeetingResponseCondition,
		// Token: 0x04003C8C RID: 15500
		PermissionControlledCondition,
		// Token: 0x04003C8D RID: 15501
		ApprovalRequestCondition,
		// Token: 0x04003C8E RID: 15502
		VoicemailCondition,
		// Token: 0x04003C8F RID: 15503
		FlaggedForActionCondition,
		// Token: 0x04003C90 RID: 15504
		FromSubscriptionCondition
	}
}
