using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000886 RID: 2182
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversationAggregationLogger : IExtensibleLogger, IWorkloadLogger
	{
		// Token: 0x060051F6 RID: 20982
		void LogParentMessageData(IStorePropertyBag parentMessage);

		// Token: 0x060051F7 RID: 20983
		void LogDeliveredMessageData(ICorePropertyBag deliveredMessage);

		// Token: 0x060051F8 RID: 20984
		void LogMailboxOwnerData(IMailboxOwner mailboxOwner, bool shouldSearchForDuplicatedMessage);

		// Token: 0x060051F9 RID: 20985
		void LogAggregationResultData(ConversationAggregationResult aggregationResult);

		// Token: 0x060051FA RID: 20986
		void LogSideConversationProcessingData(HashSet<string> parentReplyAllParticipants, HashSet<string> deliveredReplyAllParticipants);

		// Token: 0x060051FB RID: 20987
		void LogSideConversationProcessingData(ParticipantSet parentReplyAllParticipants, ParticipantSet deliveredReplyAllParticipants);

		// Token: 0x060051FC RID: 20988
		void LogSideConversationProcessingData(string checkResult, bool requiredBindToParentMessage);
	}
}
