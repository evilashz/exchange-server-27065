using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000039 RID: 57
	public enum WellKnownException
	{
		// Token: 0x040001E2 RID: 482
		None,
		// Token: 0x040001E3 RID: 483
		Exchange,
		// Token: 0x040001E4 RID: 484
		Transient,
		// Token: 0x040001E5 RID: 485
		MRS = 10,
		// Token: 0x040001E6 RID: 486
		MRSTransient,
		// Token: 0x040001E7 RID: 487
		MRSPermanent,
		// Token: 0x040001E8 RID: 488
		MRSRemote = 20,
		// Token: 0x040001E9 RID: 489
		MRSProxyLimitReached,
		// Token: 0x040001EA RID: 490
		MRSUpdateMovedMailbox,
		// Token: 0x040001EB RID: 491
		MRSUnableToFetchEasMessage,
		// Token: 0x040001EC RID: 492
		Obsolete_MRSSourceConnectionFailure,
		// Token: 0x040001ED RID: 493
		Obsolete_MRSTargetConnectionFailure,
		// Token: 0x040001EE RID: 494
		MRSUnableToReadPSTMessage,
		// Token: 0x040001EF RID: 495
		MRSUnableToCreatePSTMessage,
		// Token: 0x040001F0 RID: 496
		MRSMailboxIsLocked,
		// Token: 0x040001F1 RID: 497
		MRSUnableToGetPSTProps,
		// Token: 0x040001F2 RID: 498
		Mapi,
		// Token: 0x040001F3 RID: 499
		MapiRetryable,
		// Token: 0x040001F4 RID: 500
		MapiPermanent,
		// Token: 0x040001F5 RID: 501
		MapiMdbOffline = 40,
		// Token: 0x040001F6 RID: 502
		MapiWrongServer,
		// Token: 0x040001F7 RID: 503
		MapiVersion,
		// Token: 0x040001F8 RID: 504
		MapiImportFailure,
		// Token: 0x040001F9 RID: 505
		MapiCorruptData,
		// Token: 0x040001FA RID: 506
		MapiExiting,
		// Token: 0x040001FB RID: 507
		MapiLogonFailed,
		// Token: 0x040001FC RID: 508
		MapiCannotRegisterMapping,
		// Token: 0x040001FD RID: 509
		MapiNetworkError,
		// Token: 0x040001FE RID: 510
		MapiSessionLimit,
		// Token: 0x040001FF RID: 511
		MapiMaxObjectsExceeded,
		// Token: 0x04000200 RID: 512
		MapiShutoffQuotaExceeded,
		// Token: 0x04000201 RID: 513
		MapiCannotPreserveSignature,
		// Token: 0x04000202 RID: 514
		MapiNonCanonicalACL,
		// Token: 0x04000203 RID: 515
		MapiPartialCompletion,
		// Token: 0x04000204 RID: 516
		MapiMaxSubmissionExceeded,
		// Token: 0x04000205 RID: 517
		MapiInvalidParameter,
		// Token: 0x04000206 RID: 518
		MapiNotFound,
		// Token: 0x04000207 RID: 519
		MapiRpcBufferTooSmall,
		// Token: 0x04000208 RID: 520
		MapiNotEnoughMemory,
		// Token: 0x04000209 RID: 521
		MapiObjectChanged,
		// Token: 0x0400020A RID: 522
		MapiNoAccess,
		// Token: 0x0400020B RID: 523
		MapiMailboxInTransit,
		// Token: 0x0400020C RID: 524
		MapiADUnavailable,
		// Token: 0x0400020D RID: 525
		MapiObjectDeleted,
		// Token: 0x0400020E RID: 526
		MapiCorruptMidsetDeleted,
		// Token: 0x0400020F RID: 527
		Storage = 70,
		// Token: 0x04000210 RID: 528
		ConnectionFailedTransient = 80,
		// Token: 0x04000211 RID: 529
		StorageCannotMoveDefaultFolder,
		// Token: 0x04000212 RID: 530
		UnhealthyResource = 100,
		// Token: 0x04000213 RID: 531
		DataProviderTransient,
		// Token: 0x04000214 RID: 532
		DataProviderPermanent,
		// Token: 0x04000215 RID: 533
		NonCanonicalACL,
		// Token: 0x04000216 RID: 534
		WrongServer,
		// Token: 0x04000217 RID: 535
		CorruptData,
		// Token: 0x04000218 RID: 536
		ObjectNotFound,
		// Token: 0x04000219 RID: 537
		GlobalCounterRangeExceeded,
		// Token: 0x0400021A RID: 538
		ExArgumentOutOfRange,
		// Token: 0x0400021B RID: 539
		ExArgument,
		// Token: 0x0400021C RID: 540
		FxParser = 200,
		// Token: 0x0400021D RID: 541
		BufferParse,
		// Token: 0x0400021E RID: 542
		BufferTooSmall,
		// Token: 0x0400021F RID: 543
		ClientBackoff,
		// Token: 0x04000220 RID: 544
		CorruptRecipient,
		// Token: 0x04000221 RID: 545
		ResourceReservation = 300,
		// Token: 0x04000222 RID: 546
		ExpiredReservation,
		// Token: 0x04000223 RID: 547
		StaticCapacityExceededReservation,
		// Token: 0x04000224 RID: 548
		WlmCapacityExceededReservation,
		// Token: 0x04000225 RID: 549
		WlmResourceUnhealthy,
		// Token: 0x04000226 RID: 550
		AD = 400,
		// Token: 0x04000227 RID: 551
		ADOperation,
		// Token: 0x04000228 RID: 552
		ADTransient,
		// Token: 0x04000229 RID: 553
		InvalidMultivalueElement = 500,
		// Token: 0x0400022A RID: 554
		ConflictEntryIdCorruption,
		// Token: 0x0400022B RID: 555
		MrsOlcSettingNotImplemented = 600,
		// Token: 0x0400022C RID: 556
		MrsPropertyMismatch = 605,
		// Token: 0x0400022D RID: 557
		CommandExecution = 700
	}
}
