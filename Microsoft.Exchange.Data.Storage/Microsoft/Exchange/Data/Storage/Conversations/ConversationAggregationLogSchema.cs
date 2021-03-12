using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x0200088C RID: 2188
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ConversationAggregationLogSchema
	{
		// Token: 0x0200088D RID: 2189
		internal enum OperationStart
		{
			// Token: 0x04002C9F RID: 11423
			OperationName
		}

		// Token: 0x0200088E RID: 2190
		internal enum Error
		{
			// Token: 0x04002CA1 RID: 11425
			Exception,
			// Token: 0x04002CA2 RID: 11426
			Context
		}

		// Token: 0x0200088F RID: 2191
		internal enum MailboxOwnerData
		{
			// Token: 0x04002CA4 RID: 11428
			SideConversationProcessingEnabled,
			// Token: 0x04002CA5 RID: 11429
			SearchDuplicatedMessages,
			// Token: 0x04002CA6 RID: 11430
			IsGroupMailbox
		}

		// Token: 0x02000890 RID: 2192
		internal enum ParentMessageData
		{
			// Token: 0x04002CA8 RID: 11432
			ConversationFamilyId,
			// Token: 0x04002CA9 RID: 11433
			ConversationId,
			// Token: 0x04002CAA RID: 11434
			InternetMessageId,
			// Token: 0x04002CAB RID: 11435
			ItemClass,
			// Token: 0x04002CAC RID: 11436
			SupportsSideConversation
		}

		// Token: 0x02000891 RID: 2193
		internal enum DeliveredMessageData
		{
			// Token: 0x04002CAE RID: 11438
			InternetMessageId,
			// Token: 0x04002CAF RID: 11439
			ItemClass
		}

		// Token: 0x02000892 RID: 2194
		internal enum AggregationResult
		{
			// Token: 0x04002CB1 RID: 11441
			ConversationFamilyId,
			// Token: 0x04002CB2 RID: 11442
			ConversationId,
			// Token: 0x04002CB3 RID: 11443
			IsOutOfOrderDelivery,
			// Token: 0x04002CB4 RID: 11444
			NewConversationCreated,
			// Token: 0x04002CB5 RID: 11445
			SupportsSideConversation,
			// Token: 0x04002CB6 RID: 11446
			FixupStage
		}

		// Token: 0x02000893 RID: 2195
		internal enum SideConversationProcessingData
		{
			// Token: 0x04002CB8 RID: 11448
			ParentMessageReplyAllParticipantsCount,
			// Token: 0x04002CB9 RID: 11449
			ParentMessageReplyAllDisplayNames,
			// Token: 0x04002CBA RID: 11450
			DeliveredMessageReplyAllParticipantsCount,
			// Token: 0x04002CBB RID: 11451
			DeliveredMessageReplyAllDisplayNames,
			// Token: 0x04002CBC RID: 11452
			RequiredBindToParentMessage,
			// Token: 0x04002CBD RID: 11453
			DisplayNameCheckResult
		}

		// Token: 0x02000894 RID: 2196
		internal enum OperationEnd
		{
			// Token: 0x04002CBF RID: 11455
			OperationName,
			// Token: 0x04002CC0 RID: 11456
			Elapsed,
			// Token: 0x04002CC1 RID: 11457
			CPU,
			// Token: 0x04002CC2 RID: 11458
			RPCCount,
			// Token: 0x04002CC3 RID: 11459
			RPCLatency,
			// Token: 0x04002CC4 RID: 11460
			DirectoryCount,
			// Token: 0x04002CC5 RID: 11461
			DirectoryLatency,
			// Token: 0x04002CC6 RID: 11462
			StoreTimeInServer,
			// Token: 0x04002CC7 RID: 11463
			StoreTimeInCPU,
			// Token: 0x04002CC8 RID: 11464
			StorePagesRead,
			// Token: 0x04002CC9 RID: 11465
			StorePagesPreRead,
			// Token: 0x04002CCA RID: 11466
			StoreLogRecords,
			// Token: 0x04002CCB RID: 11467
			StoreLogBytes
		}
	}
}
