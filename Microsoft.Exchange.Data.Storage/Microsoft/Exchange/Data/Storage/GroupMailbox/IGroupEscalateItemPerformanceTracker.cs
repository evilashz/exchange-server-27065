using System;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.GroupMailbox
{
	// Token: 0x020007EA RID: 2026
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IGroupEscalateItemPerformanceTracker : IMailboxPerformanceTracker, IPerformanceTracker
	{
		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x06004BA9 RID: 19369
		// (set) Token: 0x06004BAA RID: 19370
		string OriginalMessageSender { get; set; }

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x06004BAB RID: 19371
		// (set) Token: 0x06004BAC RID: 19372
		string OriginalMessageSenderRecipientType { get; set; }

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x06004BAD RID: 19373
		// (set) Token: 0x06004BAE RID: 19374
		string OriginalMessageClass { get; set; }

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x06004BAF RID: 19375
		// (set) Token: 0x06004BB0 RID: 19376
		string OriginalMessageId { get; set; }

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x06004BB1 RID: 19377
		// (set) Token: 0x06004BB2 RID: 19378
		string OriginalInternetMessageId { get; set; }

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x06004BB3 RID: 19379
		// (set) Token: 0x06004BB4 RID: 19380
		int ParticipantsInOriginalMessage { get; set; }

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x06004BB5 RID: 19381
		// (set) Token: 0x06004BB6 RID: 19382
		bool IsGroupParticipantAddedToReplyTo { get; set; }

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x06004BB7 RID: 19383
		// (set) Token: 0x06004BB8 RID: 19384
		bool IsGroupParticipantAddedToParticipants { get; set; }

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x06004BB9 RID: 19385
		// (set) Token: 0x06004BBA RID: 19386
		bool IsGroupParticipantReplyToSkipped { get; set; }

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x06004BBB RID: 19387
		// (set) Token: 0x06004BBC RID: 19388
		long EnsureGroupParticipantAddedMilliseconds { get; set; }

		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x06004BBD RID: 19389
		// (set) Token: 0x06004BBE RID: 19390
		long DedupeParticipantsMilliseconds { get; set; }

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x06004BBF RID: 19391
		// (set) Token: 0x06004BC0 RID: 19392
		bool EscalateToYammer { get; set; }

		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x06004BC1 RID: 19393
		// (set) Token: 0x06004BC2 RID: 19394
		long SendToYammerMilliseconds { get; set; }

		// Token: 0x06004BC3 RID: 19395
		void IncrementParticipantsAddedToEscalatedMessage();

		// Token: 0x06004BC4 RID: 19396
		void IncrementParticipantsSkippedInEscalatedMessage();

		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x06004BC5 RID: 19397
		// (set) Token: 0x06004BC6 RID: 19398
		bool HasEscalatedUser { get; set; }

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x06004BC7 RID: 19399
		// (set) Token: 0x06004BC8 RID: 19400
		bool UnsubscribeUrlInserted { get; set; }

		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x06004BC9 RID: 19401
		// (set) Token: 0x06004BCA RID: 19402
		long BuildUnsubscribeUrlMilliseconds { get; set; }

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x06004BCB RID: 19403
		// (set) Token: 0x06004BCC RID: 19404
		long LinkBodySize { get; set; }

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x06004BCD RID: 19405
		// (set) Token: 0x06004BCE RID: 19406
		long LinkOnBodyDetectionMilliseconds { get; set; }

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x06004BCF RID: 19407
		// (set) Token: 0x06004BD0 RID: 19408
		long LinkInsertOnBodyMilliseconds { get; set; }
	}
}
