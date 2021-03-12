using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000049 RID: 73
	internal enum RpcErrorCode
	{
		// Token: 0x040001F6 RID: 502
		None,
		// Token: 0x040001F7 RID: 503
		UnknownUser = 1003,
		// Token: 0x040001F8 RID: 504
		Exiting = 1005,
		// Token: 0x040001F9 RID: 505
		ServerOOM = 1008,
		// Token: 0x040001FA RID: 506
		LoginPerm = 1010,
		// Token: 0x040001FB RID: 507
		NoFreeJses = 1100,
		// Token: 0x040001FC RID: 508
		UnsupportedProp = 1110,
		// Token: 0x040001FD RID: 509
		NoReplicaHere = 1128,
		// Token: 0x040001FE RID: 510
		NoReplicaAvailable,
		// Token: 0x040001FF RID: 511
		InvalidRecips = 1127,
		// Token: 0x04000200 RID: 512
		NoRecordFound = 1132,
		// Token: 0x04000201 RID: 513
		MaxTimeExpired = 1140,
		// Token: 0x04000202 RID: 514
		MdbNotInit = 1142,
		// Token: 0x04000203 RID: 515
		WrongServer = 1144,
		// Token: 0x04000204 RID: 516
		BufferTooSmall = 1149,
		// Token: 0x04000205 RID: 517
		ServerPaused = 1151,
		// Token: 0x04000206 RID: 518
		DataLoss = 1157,
		// Token: 0x04000207 RID: 519
		NotPrivateMDB = 1163,
		// Token: 0x04000208 RID: 520
		IsintegMDB,
		// Token: 0x04000209 RID: 521
		RecoveryMDBMismatch,
		// Token: 0x0400020A RID: 522
		RpcFormat = 1206,
		// Token: 0x0400020B RID: 523
		NullObject = 1209,
		// Token: 0x0400020C RID: 524
		RpcAuthentication = 1212,
		// Token: 0x0400020D RID: 525
		InvalidRTF = 1235,
		// Token: 0x0400020E RID: 526
		MessageTooBig,
		// Token: 0x0400020F RID: 527
		FormNotValid,
		// Token: 0x04000210 RID: 528
		NotAuthorized,
		// Token: 0x04000211 RID: 529
		QuotaExceeded = 1241,
		// Token: 0x04000212 RID: 530
		MaxSubmissionExceeded,
		// Token: 0x04000213 RID: 531
		MaxAttachmentExceeded,
		// Token: 0x04000214 RID: 532
		SendAsDenied,
		// Token: 0x04000215 RID: 533
		ShutoffQuotaExceeded,
		// Token: 0x04000216 RID: 534
		MaxObjsExceeded,
		// Token: 0x04000217 RID: 535
		ClientVerDisallowed,
		// Token: 0x04000218 RID: 536
		RpcHttpDisallowed,
		// Token: 0x04000219 RID: 537
		CachedModeRequired,
		// Token: 0x0400021A RID: 538
		FolderNotCleanedUp = 1251,
		// Token: 0x0400021B RID: 539
		FormatError = 1261,
		// Token: 0x0400021C RID: 540
		FolderDisabled = 1275,
		// Token: 0x0400021D RID: 541
		NoCreateRight = 1279,
		// Token: 0x0400021E RID: 542
		PublicRoot,
		// Token: 0x0400021F RID: 543
		NoCreateSubfolderRight = 1282,
		// Token: 0x04000220 RID: 544
		MsgCycle = 1284,
		// Token: 0x04000221 RID: 545
		TooManyRecips,
		// Token: 0x04000222 RID: 546
		VirusScanInProgress = 1290,
		// Token: 0x04000223 RID: 547
		VirusDetected,
		// Token: 0x04000224 RID: 548
		MailboxInTransit,
		// Token: 0x04000225 RID: 549
		BackupInProgress,
		// Token: 0x04000226 RID: 550
		VirusMessageDeleted,
		// Token: 0x04000227 RID: 551
		PropsDontMatch = 1305,
		// Token: 0x04000228 RID: 552
		DuplicateObject = 1401,
		// Token: 0x04000229 RID: 553
		WrongMailbox = 1608,
		// Token: 0x0400022A RID: 554
		ChgPassword = 1612,
		// Token: 0x0400022B RID: 555
		PwdExpired,
		// Token: 0x0400022C RID: 556
		InvWkstn,
		// Token: 0x0400022D RID: 557
		InvLogonHrs,
		// Token: 0x0400022E RID: 558
		AcctDisabled,
		// Token: 0x0400022F RID: 559
		RuleVersion = 1700,
		// Token: 0x04000230 RID: 560
		RuleFormat,
		// Token: 0x04000231 RID: 561
		RuleSendAsDenied,
		// Token: 0x04000232 RID: 562
		ProtocolDisabled = 2008,
		// Token: 0x04000233 RID: 563
		Rejected = 2030,
		// Token: 0x04000234 RID: 564
		CrossPostDenied = 2039,
		// Token: 0x04000235 RID: 565
		NoMessages = 2053,
		// Token: 0x04000236 RID: 566
		NoRpcInterface = 2084,
		// Token: 0x04000237 RID: 567
		AmbiguousAlias = 2202,
		// Token: 0x04000238 RID: 568
		UnknownMailbox,
		// Token: 0x04000239 RID: 569
		CorruptEvent = 2405,
		// Token: 0x0400023A RID: 570
		CorruptWatermark,
		// Token: 0x0400023B RID: 571
		EventError,
		// Token: 0x0400023C RID: 572
		WatermarkError,
		// Token: 0x0400023D RID: 573
		MailboxDisabled = 2412,
		// Token: 0x0400023E RID: 574
		ADUnavailable = 2414,
		// Token: 0x0400023F RID: 575
		ADError,
		// Token: 0x04000240 RID: 576
		NotEncrypted,
		// Token: 0x04000241 RID: 577
		ADNotFound,
		// Token: 0x04000242 RID: 578
		ADPropertyError,
		// Token: 0x04000243 RID: 579
		RpcServerTooBusy,
		// Token: 0x04000244 RID: 580
		RpcOutOfMemory,
		// Token: 0x04000245 RID: 581
		RpcServerOutOfMemory,
		// Token: 0x04000246 RID: 582
		RpcOutOfResources,
		// Token: 0x04000247 RID: 583
		RpcServerUnavailable,
		// Token: 0x04000248 RID: 584
		ImailConversion = 2425,
		// Token: 0x04000249 RID: 585
		ImailConversionProhibited = 2427,
		// Token: 0x0400024A RID: 586
		EventsDeleted,
		// Token: 0x0400024B RID: 587
		SubsystemStopping,
		// Token: 0x0400024C RID: 588
		SAUnavailable,
		// Token: 0x0400024D RID: 589
		CIStopping = 2600,
		// Token: 0x0400024E RID: 590
		FxInvalidState,
		// Token: 0x0400024F RID: 591
		FxUnexpectedMarker,
		// Token: 0x04000250 RID: 592
		DuplicateDelivery,
		// Token: 0x04000251 RID: 593
		MaxConnectionsExceeded = 2614,
		// Token: 0x04000252 RID: 594
		WarnCancelMessage = 263552,
		// Token: 0x04000253 RID: 595
		NotSupported = -2147221246,
		// Token: 0x04000254 RID: 596
		CorruptStore = -2147219968,
		// Token: 0x04000255 RID: 597
		CorruptData = -2147221221,
		// Token: 0x04000256 RID: 598
		NotInQueue = -2147219967,
		// Token: 0x04000257 RID: 599
		NotInitialized = -2147219963,
		// Token: 0x04000258 RID: 600
		DuplicateName = -2147219964,
		// Token: 0x04000259 RID: 601
		FolderHasContents = -2147219958,
		// Token: 0x0400025A RID: 602
		FolderHasChildren = -2147219959,
		// Token: 0x0400025B RID: 603
		Incest = -2147219957,
		// Token: 0x0400025C RID: 604
		NotImplemented = -2147221246,
		// Token: 0x0400025D RID: 605
		PropBadValue = -2147220735,
		// Token: 0x0400025E RID: 606
		InvalidType,
		// Token: 0x0400025F RID: 607
		TypeNotSupported,
		// Token: 0x04000260 RID: 608
		LoginFailure = -2147221231,
		// Token: 0x04000261 RID: 609
		AccessDenied = -2147024891,
		// Token: 0x04000262 RID: 610
		Error = -2147467259,
		// Token: 0x04000263 RID: 611
		InvalidParam = -2147024809,
		// Token: 0x04000264 RID: 612
		NotFound = -2147221233,
		// Token: 0x04000265 RID: 613
		RpcFailed = -2147221227,
		// Token: 0x04000266 RID: 614
		SyncClientChangeNewer = 264225,
		// Token: 0x04000267 RID: 615
		PartialCompletion = 263808
	}
}
