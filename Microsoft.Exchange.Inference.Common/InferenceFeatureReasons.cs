using System;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000022 RID: 34
	internal enum InferenceFeatureReasons : ushort
	{
		// Token: 0x04000087 RID: 135
		None,
		// Token: 0x04000088 RID: 136
		ConversationStarterIsYou,
		// Token: 0x04000089 RID: 137
		OnlyRecipient,
		// Token: 0x0400008A RID: 138
		ConversationContributions,
		// Token: 0x0400008B RID: 139
		MarkedImportantBySender,
		// Token: 0x0400008C RID: 140
		SenderIsManager,
		// Token: 0x0400008D RID: 141
		SenderIsInManagementChain,
		// Token: 0x0400008E RID: 142
		SenderIsDirectReport,
		// Token: 0x0400008F RID: 143
		ActionBasedOnSender,
		// Token: 0x04000090 RID: 144
		NameOnToLine,
		// Token: 0x04000091 RID: 145
		NameOnCcLine,
		// Token: 0x04000092 RID: 146
		ManagerPosition,
		// Token: 0x04000093 RID: 147
		ReplyToAMessageFromMe,
		// Token: 0x04000094 RID: 148
		PreviouslyFlagged,
		// Token: 0x04000095 RID: 149
		ActionBasedOnRecipients,
		// Token: 0x04000096 RID: 150
		ActionBasedOnSubjectWords,
		// Token: 0x04000097 RID: 151
		ActionBasedOnBasedOnBodyWords
	}
}
