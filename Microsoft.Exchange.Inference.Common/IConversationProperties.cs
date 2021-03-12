using System;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x0200002B RID: 43
	internal interface IConversationProperties
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000A8 RID: 168
		IMessageRecipient ConversationStarter { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000A9 RID: 169
		int MailboxOwnerContributions { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000AA RID: 170
		int ContributionsByOthers { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000AB RID: 171
		bool ConversationHasMeeting { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000AC RID: 172
		bool ConversationStartedByForward { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000AD RID: 173
		int NumberOfPreviousMessages { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000AE RID: 174
		int NumberOfPreviousUnread { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000AF RID: 175
		int NumberOfPreviousFlaggedByOwner { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000B0 RID: 176
		ReplyToAMessageFromMeEnum IsReplyToAMessageFromMe { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000B1 RID: 177
		bool IsSwitchedToToLineFirstTime { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000B2 RID: 178
		bool IsResponsePropagated { get; }
	}
}
