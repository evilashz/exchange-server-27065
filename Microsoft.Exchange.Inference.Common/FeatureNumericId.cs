using System;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000021 RID: 33
	internal enum FeatureNumericId : short
	{
		// Token: 0x04000061 RID: 97
		UnAssigned,
		// Token: 0x04000062 RID: 98
		FeatureBias,
		// Token: 0x04000063 RID: 99
		IsFlaggedBySender,
		// Token: 0x04000064 RID: 100
		IsMarkedHighImportanceBySender,
		// Token: 0x04000065 RID: 101
		IsMarkedUnimportantBySender,
		// Token: 0x04000066 RID: 102
		IsResponseRequested,
		// Token: 0x04000067 RID: 103
		SenderIsAutomated,
		// Token: 0x04000068 RID: 104
		SenderIsYou,
		// Token: 0x04000069 RID: 105
		ConversationStarterIsYou,
		// Token: 0x0400006A RID: 106
		OnlyRecipient,
		// Token: 0x0400006B RID: 107
		ConversationContributions,
		// Token: 0x0400006C RID: 108
		SenderIsManager,
		// Token: 0x0400006D RID: 109
		SenderIsDirectReport,
		// Token: 0x0400006E RID: 110
		RecipientPosition,
		// Token: 0x0400006F RID: 111
		ManagerPosition,
		// Token: 0x04000070 RID: 112
		RecipientCountOnToLine,
		// Token: 0x04000071 RID: 113
		RecipientCountOnCcLine,
		// Token: 0x04000072 RID: 114
		ReplyToAMessageFromMe,
		// Token: 0x04000073 RID: 115
		PreviousUnread,
		// Token: 0x04000074 RID: 116
		PreviousFlagged,
		// Token: 0x04000075 RID: 117
		NumberOfFileAttachments,
		// Token: 0x04000076 RID: 118
		NumberOfMessageAttachments,
		// Token: 0x04000077 RID: 119
		NumberOfInlineAttachments,
		// Token: 0x04000078 RID: 120
		NewConversationStarter,
		// Token: 0x04000079 RID: 121
		NewSender,
		// Token: 0x0400007A RID: 122
		SwitchedToToLineFirstTime,
		// Token: 0x0400007B RID: 123
		SubjectContent,
		// Token: 0x0400007C RID: 124
		ItemClass,
		// Token: 0x0400007D RID: 125
		SubjectPrefix,
		// Token: 0x0400007E RID: 126
		Sender,
		// Token: 0x0400007F RID: 127
		SenderCc,
		// Token: 0x04000080 RID: 128
		SenderDList,
		// Token: 0x04000081 RID: 129
		ConversationStarter,
		// Token: 0x04000082 RID: 130
		ConversationStarterCc,
		// Token: 0x04000083 RID: 131
		ConversationStarterDList,
		// Token: 0x04000084 RID: 132
		RecipientOnToLine,
		// Token: 0x04000085 RID: 133
		RecipientOnCcLine
	}
}
