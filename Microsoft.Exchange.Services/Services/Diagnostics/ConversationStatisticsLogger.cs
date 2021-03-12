using System;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200002C RID: 44
	internal sealed class ConversationStatisticsLogger
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x0000A0CE File Offset: 0x000082CE
		public ConversationStatisticsLogger(RequestDetailsLogger requestDetailsLogger)
		{
			this.requestDetailsLogger = requestDetailsLogger;
			OwsLogRegistry.Register(ConversationStatisticsLogger.GetConversationItemsActionName, ConversationStatisticsLogger.GetConversationItemsMetadataType, new Type[0]);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000A0F4 File Offset: 0x000082F4
		public void Log(IConversationStatistics conversationStatistics)
		{
			if (this.requestDetailsLogger != null && conversationStatistics != null && conversationStatistics.ConversationId != null)
			{
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.ConversationId, conversationStatistics.ConversationId.ToBase64String());
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.TotalNodeCount, conversationStatistics.TotalNodeCount);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.AttachmentPresentCount, conversationStatistics.AttachmentPresentCount);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.BodyTagMatchingAttemptsCount, conversationStatistics.BodyTagMatchingAttemptsCount);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.BodyTagMatchingIssuesCount, conversationStatistics.BodyTagMatchingIssuesCount);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.BodyFormatMismatchedCount, conversationStatistics.BodyFormatMismatchedCount);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.BodyTagMismatchedCount, conversationStatistics.BodyTagMismatchedCount);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.BodyTagNotPresentCount, conversationStatistics.BodyTagNotPresentCount);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.ExtraPropertiesNeededCount, conversationStatistics.ExtraPropertiesNeededCount);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.IrmProtectedCount, conversationStatistics.IrmProtectedCount);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.ItemsExtracted, conversationStatistics.ItemsExtracted);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.ItemsOpened, conversationStatistics.ItemsOpened);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.LeafNodeCount, conversationStatistics.LeafNodeCount);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.MapiAttachmentPresentCount, conversationStatistics.MapiAttachmentPresentCount);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.NonMSHeaderCount, conversationStatistics.NonMSHeaderCount);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.ParticipantNotFoundCount, conversationStatistics.ParticipantNotFoundCount);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.PossibleInlinesCount, conversationStatistics.PossibleInlinesCount);
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.SummariesConstructed, conversationStatistics.SummariesConstructed);
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000A330 File Offset: 0x00008530
		public void Log(ConversationRequestType conversationRequest)
		{
			if (this.requestDetailsLogger != null)
			{
				this.requestDetailsLogger.Set(GetConversationItemsMetadata.SyncState, conversationRequest.SyncStateString ?? string.Empty);
			}
		}

		// Token: 0x040001DD RID: 477
		private static readonly string GetConversationItemsActionName = typeof(GetConversationItems).Name;

		// Token: 0x040001DE RID: 478
		private static readonly Type GetConversationItemsMetadataType = typeof(GetConversationItemsMetadata);

		// Token: 0x040001DF RID: 479
		private readonly RequestDetailsLogger requestDetailsLogger;
	}
}
