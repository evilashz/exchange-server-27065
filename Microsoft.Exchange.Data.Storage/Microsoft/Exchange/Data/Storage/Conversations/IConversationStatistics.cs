using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000896 RID: 2198
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversationStatistics
	{
		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x0600521F RID: 21023
		ConversationId ConversationId { get; }

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x06005220 RID: 21024
		int TotalNodeCount { get; }

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x06005221 RID: 21025
		int LeafNodeCount { get; }

		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x06005222 RID: 21026
		int ItemsExtracted { get; }

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x06005223 RID: 21027
		int ItemsOpened { get; }

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x06005224 RID: 21028
		int SummariesConstructed { get; }

		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x06005225 RID: 21029
		int BodyTagMatchingAttemptsCount { get; }

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x06005226 RID: 21030
		int BodyTagMatchingIssuesCount { get; }

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x06005227 RID: 21031
		int BodyTagNotPresentCount { get; }

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x06005228 RID: 21032
		int BodyTagMismatchedCount { get; }

		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x06005229 RID: 21033
		int BodyFormatMismatchedCount { get; }

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x0600522A RID: 21034
		int NonMSHeaderCount { get; }

		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x0600522B RID: 21035
		int ExtraPropertiesNeededCount { get; }

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x0600522C RID: 21036
		int ParticipantNotFoundCount { get; }

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x0600522D RID: 21037
		int AttachmentPresentCount { get; }

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x0600522E RID: 21038
		int MapiAttachmentPresentCount { get; }

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x0600522F RID: 21039
		int PossibleInlinesCount { get; }

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x06005230 RID: 21040
		int IrmProtectedCount { get; }
	}
}
