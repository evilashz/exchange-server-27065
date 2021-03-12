using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x0200028F RID: 655
	public enum EventDescription
	{
		// Token: 0x04000C31 RID: 3121
		[EventDescriptionInformation(IsTerminal = false)]
		Submitted,
		// Token: 0x04000C32 RID: 3122
		[EventDescriptionInformation(IsTerminal = false)]
		SubmittedCrossSite,
		// Token: 0x04000C33 RID: 3123
		[EventDescriptionInformation(IsTerminal = false)]
		Resolved,
		// Token: 0x04000C34 RID: 3124
		[EventDescriptionInformation(IsTerminal = false)]
		Expanded,
		// Token: 0x04000C35 RID: 3125
		[EventDescriptionInformation(IsTerminal = true, EventPriority = 1)]
		Delivered,
		// Token: 0x04000C36 RID: 3126
		[EventDescriptionInformation(IsTerminal = true, EventPriority = 0)]
		MovedToFolderByInboxRule,
		// Token: 0x04000C37 RID: 3127
		[EventDescriptionInformation(IsTerminal = false)]
		RulesCc,
		// Token: 0x04000C38 RID: 3128
		[EventDescriptionInformation(IsTerminal = true, EventPriority = 4)]
		FailedGeneral,
		// Token: 0x04000C39 RID: 3129
		[EventDescriptionInformation(IsTerminal = true, EventPriority = 3)]
		FailedModeration,
		// Token: 0x04000C3A RID: 3130
		[EventDescriptionInformation(IsTerminal = true, EventPriority = 4)]
		FailedTransportRules,
		// Token: 0x04000C3B RID: 3131
		[EventDescriptionInformation(IsTerminal = false, EventPriority = 5)]
		SmtpSend,
		// Token: 0x04000C3C RID: 3132
		[EventDescriptionInformation(IsTerminal = false, EventPriority = 5)]
		SmtpSendCrossSite,
		// Token: 0x04000C3D RID: 3133
		[EventDescriptionInformation(IsTerminal = false, EventPriority = 5)]
		SmtpSendCrossForest,
		// Token: 0x04000C3E RID: 3134
		[EventDescriptionInformation(IsTerminal = false)]
		SmtpReceive,
		// Token: 0x04000C3F RID: 3135
		[EventDescriptionInformation(IsTerminal = false)]
		Forwarded,
		// Token: 0x04000C40 RID: 3136
		[EventDescriptionInformation(IsTerminal = false)]
		Pending,
		// Token: 0x04000C41 RID: 3137
		[EventDescriptionInformation(IsTerminal = false)]
		PendingModeration,
		// Token: 0x04000C42 RID: 3138
		[EventDescriptionInformation(IsTerminal = false, EventPriority = 2)]
		ApprovedModeration,
		// Token: 0x04000C43 RID: 3139
		[EventDescriptionInformation(IsTerminal = false, EventPriority = 4)]
		QueueRetry,
		// Token: 0x04000C44 RID: 3140
		[EventDescriptionInformation(IsTerminal = false, EventPriority = 4)]
		QueueRetryNoRetryTime,
		// Token: 0x04000C45 RID: 3141
		[EventDescriptionInformation(IsTerminal = false)]
		MessageDefer,
		// Token: 0x04000C46 RID: 3142
		[EventDescriptionInformation(IsTerminal = true, EventPriority = 5)]
		TransferredToForeignOrg,
		// Token: 0x04000C47 RID: 3143
		[EventDescriptionInformation(IsTerminal = true, EventPriority = 5)]
		TransferredToLegacyExchangeServer,
		// Token: 0x04000C48 RID: 3144
		[EventDescriptionInformation(IsTerminal = false, EventPriority = 5)]
		TransferredToPartnerOrg,
		// Token: 0x04000C49 RID: 3145
		[EventDescriptionInformation(IsTerminal = false, EventPriority = 5)]
		DelayedAfterTransferToPartnerOrg,
		// Token: 0x04000C4A RID: 3146
		[EventDescriptionInformation(IsTerminal = true, EventPriority = 0)]
		Read,
		// Token: 0x04000C4B RID: 3147
		[EventDescriptionInformation(IsTerminal = true, EventPriority = 0)]
		NotRead,
		// Token: 0x04000C4C RID: 3148
		[EventDescriptionInformation(IsTerminal = true, EventPriority = 0)]
		ForwardedToDelegateAndDeleted,
		// Token: 0x04000C4D RID: 3149
		[EventDescriptionInformation(IsTerminal = true, EventPriority = 4)]
		ExpiredWithNoModerationDecision
	}
}
