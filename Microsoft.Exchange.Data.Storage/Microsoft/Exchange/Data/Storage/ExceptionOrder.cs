using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B87 RID: 2951
	internal enum ExceptionOrder
	{
		// Token: 0x04003CB5 RID: 15541
		FromRecipientsCondition = 1,
		// Token: 0x04003CB6 RID: 15542
		MarkedAsImportanceCondition,
		// Token: 0x04003CB7 RID: 15543
		MarkedAsSensitivityCondition,
		// Token: 0x04003CB8 RID: 15544
		MarkedAsOofCondition,
		// Token: 0x04003CB9 RID: 15545
		HasAttachmentCondition,
		// Token: 0x04003CBA RID: 15546
		SentToMeCondition,
		// Token: 0x04003CBB RID: 15547
		SentOnlyToMeCondition,
		// Token: 0x04003CBC RID: 15548
		SentCcMeCondition,
		// Token: 0x04003CBD RID: 15549
		SentToOrCcMeCondition,
		// Token: 0x04003CBE RID: 15550
		NotSentToMeCondition,
		// Token: 0x04003CBF RID: 15551
		SentToRecipientsCondition,
		// Token: 0x04003CC0 RID: 15552
		ContainsSubjectStringCondition,
		// Token: 0x04003CC1 RID: 15553
		ContainsBodyStringCondition,
		// Token: 0x04003CC2 RID: 15554
		ContainsSubjectOrBodyStringCondition,
		// Token: 0x04003CC3 RID: 15555
		WithinSizeRangeCondition,
		// Token: 0x04003CC4 RID: 15556
		WithinDateRangeCondition,
		// Token: 0x04003CC5 RID: 15557
		ContainsSenderStringCondition,
		// Token: 0x04003CC6 RID: 15558
		ContainsHeaderStringCondition,
		// Token: 0x04003CC7 RID: 15559
		MeetingMessageCondition,
		// Token: 0x04003CC8 RID: 15560
		MeetingResponseCondition,
		// Token: 0x04003CC9 RID: 15561
		ContainsRecipientStringCondition,
		// Token: 0x04003CCA RID: 15562
		AssignedCategoriesCondition,
		// Token: 0x04003CCB RID: 15563
		FormsCondition,
		// Token: 0x04003CCC RID: 15564
		MessageClassificationCondition,
		// Token: 0x04003CCD RID: 15565
		NdrCondition,
		// Token: 0x04003CCE RID: 15566
		AutomaticForwardCondition,
		// Token: 0x04003CCF RID: 15567
		EncryptedCondition,
		// Token: 0x04003CD0 RID: 15568
		SignedCondition,
		// Token: 0x04003CD1 RID: 15569
		ReadReceiptCondition,
		// Token: 0x04003CD2 RID: 15570
		PermissionControlledCondition,
		// Token: 0x04003CD3 RID: 15571
		ApprovalRequestCondition,
		// Token: 0x04003CD4 RID: 15572
		VoicemailCondition,
		// Token: 0x04003CD5 RID: 15573
		FlaggedForActionCondition,
		// Token: 0x04003CD6 RID: 15574
		FromSubscriptionCondition
	}
}
