using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020000AA RID: 170
	internal enum ErrorCode : uint
	{
		// Token: 0x04000275 RID: 629
		None,
		// Token: 0x04000276 RID: 630
		UnknownUser = 1003U,
		// Token: 0x04000277 RID: 631
		UnknownCodepage = 1007U,
		// Token: 0x04000278 RID: 632
		Memory,
		// Token: 0x04000279 RID: 633
		LoginPerm = 1010U,
		// Token: 0x0400027A RID: 634
		UnsupportedProperty = 1110U,
		// Token: 0x0400027B RID: 635
		BadFolderName = 1116U,
		// Token: 0x0400027C RID: 636
		NotSearchFolder = 1121U,
		// Token: 0x0400027D RID: 637
		NoDeleteSubmitMessage = 1125U,
		// Token: 0x0400027E RID: 638
		InvalidRecipients = 1127U,
		// Token: 0x0400027F RID: 639
		NoReplicaHere,
		// Token: 0x04000280 RID: 640
		NoReplicaAvailable,
		// Token: 0x04000281 RID: 641
		MdbOffline = 1142U,
		// Token: 0x04000282 RID: 642
		WrongServer = 1144U,
		// Token: 0x04000283 RID: 643
		BufferTooSmall = 1149U,
		// Token: 0x04000284 RID: 644
		ServerBusy = 1152U,
		// Token: 0x04000285 RID: 645
		NoSuchLogon,
		// Token: 0x04000286 RID: 646
		RecoveryMdbMismatch = 1165U,
		// Token: 0x04000287 RID: 647
		SearchEvaluationInProgress = 1177U,
		// Token: 0x04000288 RID: 648
		RpcFormat = 1206U,
		// Token: 0x04000289 RID: 649
		NullObject = 1209U,
		// Token: 0x0400028A RID: 650
		MaxSubmissionExceeded = 1242U,
		// Token: 0x0400028B RID: 651
		ShutoffQuotaExceeded = 1245U,
		// Token: 0x0400028C RID: 652
		ClientVerDisallowed = 1247U,
		// Token: 0x0400028D RID: 653
		IdSetFormatError = 1261U,
		// Token: 0x0400028E RID: 654
		NotExpanded = 1271U,
		// Token: 0x0400028F RID: 655
		NotCollapsed,
		// Token: 0x04000290 RID: 656
		DestinationNullObject = 1283U,
		// Token: 0x04000291 RID: 657
		MessageCycle,
		// Token: 0x04000292 RID: 658
		NoServerSupport = 1721U,
		// Token: 0x04000293 RID: 659
		AmbiguousAlias = 2202U,
		// Token: 0x04000294 RID: 660
		NonCanonicalACL = 2409U,
		// Token: 0x04000295 RID: 661
		ADUnavailable = 2414U,
		// Token: 0x04000296 RID: 662
		ADError,
		// Token: 0x04000297 RID: 663
		ADNotFound = 2417U,
		// Token: 0x04000298 RID: 664
		ADPropertyError,
		// Token: 0x04000299 RID: 665
		RpcServerTooBusy,
		// Token: 0x0400029A RID: 666
		FxUnexpectedMarker = 2602U,
		// Token: 0x0400029B RID: 667
		PropertyNotPromoted = 2608U,
		// Token: 0x0400029C RID: 668
		CannotRegisterNewReplidGuidMapping = 2620U,
		// Token: 0x0400029D RID: 669
		InvalidOperation = 2631U,
		// Token: 0x0400029E RID: 670
		AvailabilityServiceError = 2633U,
		// Token: 0x0400029F RID: 671
		UnexpectedMailboxState,
		// Token: 0x040002A0 RID: 672
		MaxThreadsPerMdbExceeded = 2700U,
		// Token: 0x040002A1 RID: 673
		MaxThreadsPerSCTExceeded,
		// Token: 0x040002A2 RID: 674
		WrongProvisionedFid,
		// Token: 0x040002A3 RID: 675
		ISIntegMdbTaskExceeded,
		// Token: 0x040002A4 RID: 676
		ISIntegQueueFull,
		// Token: 0x040002A5 RID: 677
		SharePointWebDavFailure = 5000U,
		// Token: 0x040002A6 RID: 678
		SharePointWebDavFileAlreadyExists,
		// Token: 0x040002A7 RID: 679
		WrongSessionToOpenLinkedFolder,
		// Token: 0x040002A8 RID: 680
		SyncClientChangeNewer = 264225U,
		// Token: 0x040002A9 RID: 681
		PartialCompletion = 263808U,
		// Token: 0x040002AA RID: 682
		GenericError = 2147500037U,
		// Token: 0x040002AB RID: 683
		StreamAccessDenied = 2147680261U,
		// Token: 0x040002AC RID: 684
		StreamSeekError = 2147680281U,
		// Token: 0x040002AD RID: 685
		StreamInvalidParam = 2147680343U,
		// Token: 0x040002AE RID: 686
		NotSupported = 2147746050U,
		// Token: 0x040002AF RID: 687
		InvalidEntryId = 2147746055U,
		// Token: 0x040002B0 RID: 688
		InvalidObject,
		// Token: 0x040002B1 RID: 689
		ObjectChanged,
		// Token: 0x040002B2 RID: 690
		ObjectDeleted,
		// Token: 0x040002B3 RID: 691
		NotFound = 2147746063U,
		// Token: 0x040002B4 RID: 692
		Version,
		// Token: 0x040002B5 RID: 693
		LogonFailed,
		// Token: 0x040002B6 RID: 694
		UserAbort = 2147746067U,
		// Token: 0x040002B7 RID: 695
		NetworkError = 2147746069U,
		// Token: 0x040002B8 RID: 696
		TooComplex = 2147746071U,
		// Token: 0x040002B9 RID: 697
		PropertyComputed = 2147746074U,
		// Token: 0x040002BA RID: 698
		CorruptData,
		// Token: 0x040002BB RID: 699
		PropertyBadValue = 2147746561U,
		// Token: 0x040002BC RID: 700
		PropertyInvalidType,
		// Token: 0x040002BD RID: 701
		PropertyUnexpectedType = 2147746564U,
		// Token: 0x040002BE RID: 702
		PropertyTooBig,
		// Token: 0x040002BF RID: 703
		NotInQueue = 2147747329U,
		// Token: 0x040002C0 RID: 704
		Collision = 2147747332U,
		// Token: 0x040002C1 RID: 705
		NotInitialized,
		// Token: 0x040002C2 RID: 706
		SyncObjectDeleted = 2147747840U,
		// Token: 0x040002C3 RID: 707
		SyncIgnore,
		// Token: 0x040002C4 RID: 708
		SyncConflict,
		// Token: 0x040002C5 RID: 709
		SyncNoParent,
		// Token: 0x040002C6 RID: 710
		NotImplemented = 2147749887U,
		// Token: 0x040002C7 RID: 711
		AccessDenied = 2147942405U,
		// Token: 0x040002C8 RID: 712
		NotEnoughMemory = 2147942414U,
		// Token: 0x040002C9 RID: 713
		InvalidParam = 2147942487U,
		// Token: 0x040002CA RID: 714
		InvalidBookmark = 2147746821U,
		// Token: 0x040002CB RID: 715
		NotConfigured = 2147746076U,
		// Token: 0x040002CC RID: 716
		FolderCycle = 2147747339U
	}
}
