using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000055 RID: 85
	public enum ExportErrorType
	{
		// Token: 0x04000212 RID: 530
		Unauthorized,
		// Token: 0x04000213 RID: 531
		NotFound,
		// Token: 0x04000214 RID: 532
		Unavailable,
		// Token: 0x04000215 RID: 533
		Unknown,
		// Token: 0x04000216 RID: 534
		UnsupportedType,
		// Token: 0x04000217 RID: 535
		MessageDataCorrupted,
		// Token: 0x04000218 RID: 536
		ParentFolderNotFound,
		// Token: 0x04000219 RID: 537
		ExchangeWebServiceCallFailed,
		// Token: 0x0400021A RID: 538
		FailedToSearchMailboxes,
		// Token: 0x0400021B RID: 539
		FailedToExportItem,
		// Token: 0x0400021C RID: 540
		FailedToGetFolderHierarchy,
		// Token: 0x0400021D RID: 541
		FailedToGetRootFolders,
		// Token: 0x0400021E RID: 542
		FailedToWriteItemIdList,
		// Token: 0x0400021F RID: 543
		FailedToRemoveItemIdList,
		// Token: 0x04000220 RID: 544
		FailedToReadItemIdList,
		// Token: 0x04000221 RID: 545
		ItemIdListCorrupted,
		// Token: 0x04000222 RID: 546
		AsynchronousTaskTimeout,
		// Token: 0x04000223 RID: 547
		OperationNotSupportedWithCurrentStatus,
		// Token: 0x04000224 RID: 548
		FailedToUpdateStatus,
		// Token: 0x04000225 RID: 549
		FailedToCleanupCorruptedStatusLog,
		// Token: 0x04000226 RID: 550
		FailedToUpdateSourceStatus,
		// Token: 0x04000227 RID: 551
		FailedToDeleteStatusFile,
		// Token: 0x04000228 RID: 552
		CorruptedStatus,
		// Token: 0x04000229 RID: 553
		FailedToOpenStatusFile,
		// Token: 0x0400022A RID: 554
		FailedToLoadStatus,
		// Token: 0x0400022B RID: 555
		ExistingStatusMustBeAResumeOperation,
		// Token: 0x0400022C RID: 556
		FailedToOpenPstFile,
		// Token: 0x0400022D RID: 557
		StatusNotFoundToResume,
		// Token: 0x0400022E RID: 558
		FailedToOpenExistingPstFile,
		// Token: 0x0400022F RID: 559
		FailedToRecreateUniqueItemHashMap,
		// Token: 0x04000230 RID: 560
		CannotResumeWithConfigurationChange,
		// Token: 0x04000231 RID: 561
		FailedToRollbackResultsInTargetLocation,
		// Token: 0x04000232 RID: 562
		FailedToCreateExportLocation,
		// Token: 0x04000233 RID: 563
		FailedToGetFolderById,
		// Token: 0x04000234 RID: 564
		FailedToGetFolderByName,
		// Token: 0x04000235 RID: 565
		FailedToCreateFolder,
		// Token: 0x04000236 RID: 566
		FailedToDeleteFolder,
		// Token: 0x04000237 RID: 567
		FailedToMoveFolder,
		// Token: 0x04000238 RID: 568
		FailedToMoveItem,
		// Token: 0x04000239 RID: 569
		FailedToRetrieveItems,
		// Token: 0x0400023A RID: 570
		FailedToCreateItems,
		// Token: 0x0400023B RID: 571
		FailedToUpdateItems,
		// Token: 0x0400023C RID: 572
		FailedToDeleteItems,
		// Token: 0x0400023D RID: 573
		FailedToUploadItems,
		// Token: 0x0400023E RID: 574
		NumberOfItemsMismatched,
		// Token: 0x0400023F RID: 575
		FailedToGetUnsearchableItemStatistics,
		// Token: 0x04000240 RID: 576
		FailedToGetUnsearchableItems,
		// Token: 0x04000241 RID: 577
		FailedToGetItemForUnsearchableItem,
		// Token: 0x04000242 RID: 578
		FailedToGetItem,
		// Token: 0x04000243 RID: 579
		FailedToGetAttachment,
		// Token: 0x04000244 RID: 580
		FailedToCreateAttachments,
		// Token: 0x04000245 RID: 581
		FailedToDeleteAttachments,
		// Token: 0x04000246 RID: 582
		UnexpectedAutoDiscoverServiceUrl,
		// Token: 0x04000247 RID: 583
		UnexpectedWebServiceUrlRedirection,
		// Token: 0x04000248 RID: 584
		FailedToAutoDiscoverExchangeWebServiceUrl,
		// Token: 0x04000249 RID: 585
		FailedToRetrieveSearchConfiguration,
		// Token: 0x0400024A RID: 586
		FailedToRetrieveSearchConfigurationNoSearchableMailboxesFound,
		// Token: 0x0400024B RID: 587
		TargetOutOfSpace,
		// Token: 0x0400024C RID: 588
		StopRequested
	}
}
