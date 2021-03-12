using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000005 RID: 5
	public enum ErrorCodeValue
	{
		// Token: 0x04000114 RID: 276
		NoError,
		// Token: 0x04000115 RID: 277
		StoreTestFailure = 1000,
		// Token: 0x04000116 RID: 278
		UnknownUser = 1003,
		// Token: 0x04000117 RID: 279
		DatabaseRolledBack = 1011,
		// Token: 0x04000118 RID: 280
		DatabaseBadVersion = 1105,
		// Token: 0x04000119 RID: 281
		DatabaseError = 1108,
		// Token: 0x0400011A RID: 282
		InvalidCollapseState = 1118,
		// Token: 0x0400011B RID: 283
		NoDeleteSubmitMessage = 1125,
		// Token: 0x0400011C RID: 284
		RecoveryMDBMismatch = 1165,
		// Token: 0x0400011D RID: 285
		SearchFolderScopeViolation = 1168,
		// Token: 0x0400011E RID: 286
		SearchEvaluationInProgress = 1177,
		// Token: 0x0400011F RID: 287
		NestedSearchChainTooDeep = 1181,
		// Token: 0x04000120 RID: 288
		CorruptSearchScope = 1183,
		// Token: 0x04000121 RID: 289
		CorruptSearchBacklink,
		// Token: 0x04000122 RID: 290
		GlobalCounterRangeExceeded,
		// Token: 0x04000123 RID: 291
		CorruptMidsetDeleted,
		// Token: 0x04000124 RID: 292
		RpcFormat = 1206,
		// Token: 0x04000125 RID: 293
		QuotaExceeded = 1241,
		// Token: 0x04000126 RID: 294
		MaxSubmissionExceeded,
		// Token: 0x04000127 RID: 295
		MaxAttachmentExceeded,
		// Token: 0x04000128 RID: 296
		ShutoffQuotaExceeded = 1245,
		// Token: 0x04000129 RID: 297
		MaxObjectsExceeded,
		// Token: 0x0400012A RID: 298
		MessagePerFolderCountReceiveQuotaExceeded = 1252,
		// Token: 0x0400012B RID: 299
		FolderHierarchyChildrenCountReceiveQuotaExceeded,
		// Token: 0x0400012C RID: 300
		FolderHierarchyDepthReceiveQuotaExceeded,
		// Token: 0x0400012D RID: 301
		DynamicSearchFoldersPerScopeCountReceiveQuotaExceeded,
		// Token: 0x0400012E RID: 302
		FolderHierarchySizeReceiveQuotaExceeded,
		// Token: 0x0400012F RID: 303
		NotVisible = 1270,
		// Token: 0x04000130 RID: 304
		NotExpanded,
		// Token: 0x04000131 RID: 305
		NotCollapsed,
		// Token: 0x04000132 RID: 306
		Leaf,
		// Token: 0x04000133 RID: 307
		MessageCycle = 1284,
		// Token: 0x04000134 RID: 308
		Rejected = 2030,
		// Token: 0x04000135 RID: 309
		UnknownMailbox = 2203,
		// Token: 0x04000136 RID: 310
		DisabledMailbox = 2412,
		// Token: 0x04000137 RID: 311
		AdUnavailable = 2414,
		// Token: 0x04000138 RID: 312
		ADPropertyError = 2418,
		// Token: 0x04000139 RID: 313
		RpcServerTooBusy,
		// Token: 0x0400013A RID: 314
		RpcServerUnavailable = 2423,
		// Token: 0x0400013B RID: 315
		EventsDeleted = 2428,
		// Token: 0x0400013C RID: 316
		MaxPoolExceeded = 2605,
		// Token: 0x0400013D RID: 317
		EventNotFound = 2607,
		// Token: 0x0400013E RID: 318
		InvalidPool = 2617,
		// Token: 0x0400013F RID: 319
		BlockModeInitFailed = 2619,
		// Token: 0x04000140 RID: 320
		UnexpectedMailboxState = 2634,
		// Token: 0x04000141 RID: 321
		SoftDeletedMailbox,
		// Token: 0x04000142 RID: 322
		DatabaseStateConflict,
		// Token: 0x04000143 RID: 323
		RpcInvalidSession,
		// Token: 0x04000144 RID: 324
		PublicFolderDumpstersLimitExceeded,
		// Token: 0x04000145 RID: 325
		InvalidMultiMailboxSearchRequest = 2800,
		// Token: 0x04000146 RID: 326
		InvalidMultiMailboxKeywordStatsRequest,
		// Token: 0x04000147 RID: 327
		MultiMailboxSearchFailed,
		// Token: 0x04000148 RID: 328
		MaxMultiMailboxSearchExceeded,
		// Token: 0x04000149 RID: 329
		MultiMailboxSearchOperationFailed,
		// Token: 0x0400014A RID: 330
		MultiMailboxSearchNonFullTextSearch,
		// Token: 0x0400014B RID: 331
		MultiMailboxSearchTimeOut,
		// Token: 0x0400014C RID: 332
		MultiMailboxKeywordStatsTimeOut,
		// Token: 0x0400014D RID: 333
		MultiMailboxSearchInvalidSortBy,
		// Token: 0x0400014E RID: 334
		MultiMailboxSearchNonFullTextSortBy,
		// Token: 0x0400014F RID: 335
		MultiMailboxSearchInvalidPagination,
		// Token: 0x04000150 RID: 336
		MultiMailboxSearchNonFullTextPropertyInPagination,
		// Token: 0x04000151 RID: 337
		MultiMailboxSearchMailboxNotFound,
		// Token: 0x04000152 RID: 338
		MultiMailboxSearchInvalidRestriction,
		// Token: 0x04000153 RID: 339
		FullTextIndexCallFailed = 2820,
		// Token: 0x04000154 RID: 340
		UserInformationAlreadyExists = 2830,
		// Token: 0x04000155 RID: 341
		UserInformationLockTimeout,
		// Token: 0x04000156 RID: 342
		UserInformationNotFound,
		// Token: 0x04000157 RID: 343
		UserInformationNoAccess,
		// Token: 0x04000158 RID: 344
		UserInformationPropertyError,
		// Token: 0x04000159 RID: 345
		UserInformationSoftDeleted,
		// Token: 0x0400015A RID: 346
		InterfaceNotSupported = -2147467262,
		// Token: 0x0400015B RID: 347
		CallFailed = -2147467259,
		// Token: 0x0400015C RID: 348
		StreamAccessDenied = -2147287035,
		// Token: 0x0400015D RID: 349
		StgInsufficientMemory = -2147287032,
		// Token: 0x0400015E RID: 350
		StreamSeekError = -2147287015,
		// Token: 0x0400015F RID: 351
		LockViolation = -2147287007,
		// Token: 0x04000160 RID: 352
		StreamInvalidParam = -2147286953,
		// Token: 0x04000161 RID: 353
		NotSupported = -2147221246,
		// Token: 0x04000162 RID: 354
		BadCharWidth,
		// Token: 0x04000163 RID: 355
		StringTooLong = -2147221243,
		// Token: 0x04000164 RID: 356
		UnknownFlags,
		// Token: 0x04000165 RID: 357
		InvalidEntryId,
		// Token: 0x04000166 RID: 358
		InvalidObject,
		// Token: 0x04000167 RID: 359
		ObjectChanged,
		// Token: 0x04000168 RID: 360
		ObjectDeleted,
		// Token: 0x04000169 RID: 361
		Busy,
		// Token: 0x0400016A RID: 362
		NotEnoughDisk = -2147221235,
		// Token: 0x0400016B RID: 363
		NotEnoughResources,
		// Token: 0x0400016C RID: 364
		NotFound,
		// Token: 0x0400016D RID: 365
		VersionMismatch,
		// Token: 0x0400016E RID: 366
		LogonFailed,
		// Token: 0x0400016F RID: 367
		SessionLimit,
		// Token: 0x04000170 RID: 368
		UserCancel,
		// Token: 0x04000171 RID: 369
		UnableToAbort,
		// Token: 0x04000172 RID: 370
		NetworkError,
		// Token: 0x04000173 RID: 371
		DiskError,
		// Token: 0x04000174 RID: 372
		TooComplex,
		// Token: 0x04000175 RID: 373
		ConditionViolation = 2604,
		// Token: 0x04000176 RID: 374
		BadColumn = -2147221224,
		// Token: 0x04000177 RID: 375
		ExtendedError,
		// Token: 0x04000178 RID: 376
		Computed,
		// Token: 0x04000179 RID: 377
		CorruptData,
		// Token: 0x0400017A RID: 378
		Unconfigured,
		// Token: 0x0400017B RID: 379
		FailOneProvider,
		// Token: 0x0400017C RID: 380
		UnknownCPID,
		// Token: 0x0400017D RID: 381
		UnknownLCID,
		// Token: 0x0400017E RID: 382
		PasswordChangeRequired,
		// Token: 0x0400017F RID: 383
		PasswordExpired,
		// Token: 0x04000180 RID: 384
		InvalidWorkstationAccount,
		// Token: 0x04000181 RID: 385
		InvalidAccessTime,
		// Token: 0x04000182 RID: 386
		AccountDisabled,
		// Token: 0x04000183 RID: 387
		EndOfSession = -2147220992,
		// Token: 0x04000184 RID: 388
		UnknownEntryId,
		// Token: 0x04000185 RID: 389
		MissingRequiredColumn,
		// Token: 0x04000186 RID: 390
		FailCallback = -2147220967,
		// Token: 0x04000187 RID: 391
		BadValue = -2147220735,
		// Token: 0x04000188 RID: 392
		InvalidType,
		// Token: 0x04000189 RID: 393
		TypeNoSupport,
		// Token: 0x0400018A RID: 394
		UnexpectedType,
		// Token: 0x0400018B RID: 395
		TooBig,
		// Token: 0x0400018C RID: 396
		DeclineCopy,
		// Token: 0x0400018D RID: 397
		UnexpectedId,
		// Token: 0x0400018E RID: 398
		UnableToComplete = -2147220480,
		// Token: 0x0400018F RID: 399
		Timeout,
		// Token: 0x04000190 RID: 400
		TableEmpty,
		// Token: 0x04000191 RID: 401
		TableTooBig,
		// Token: 0x04000192 RID: 402
		InvalidBookmark = -2147220475,
		// Token: 0x04000193 RID: 403
		DataLoss = -2147220347,
		// Token: 0x04000194 RID: 404
		Wait = -2147220224,
		// Token: 0x04000195 RID: 405
		Cancel,
		// Token: 0x04000196 RID: 406
		NotMe,
		// Token: 0x04000197 RID: 407
		CorruptStore = -2147219968,
		// Token: 0x04000198 RID: 408
		NotInQueue,
		// Token: 0x04000199 RID: 409
		NoSuppress,
		// Token: 0x0400019A RID: 410
		Collision = -2147219964,
		// Token: 0x0400019B RID: 411
		NotInitialized,
		// Token: 0x0400019C RID: 412
		NonStandard,
		// Token: 0x0400019D RID: 413
		NoRecipients,
		// Token: 0x0400019E RID: 414
		Submitted,
		// Token: 0x0400019F RID: 415
		HasFolders,
		// Token: 0x040001A0 RID: 416
		HasMessages,
		// Token: 0x040001A1 RID: 417
		FolderCycle,
		// Token: 0x040001A2 RID: 418
		RootFolder = -2147219957,
		// Token: 0x040001A3 RID: 419
		RecursionLimit,
		// Token: 0x040001A4 RID: 420
		LockIdLimit,
		// Token: 0x040001A5 RID: 421
		TooManyMountedDatabases,
		// Token: 0x040001A6 RID: 422
		PartialItem = -2147219834,
		// Token: 0x040001A7 RID: 423
		AmbiguousRecip = -2147219712,
		// Token: 0x040001A8 RID: 424
		SyncObjectDeleted = -2147219456,
		// Token: 0x040001A9 RID: 425
		SyncIgnore,
		// Token: 0x040001AA RID: 426
		SyncConflict,
		// Token: 0x040001AB RID: 427
		SyncNoParent,
		// Token: 0x040001AC RID: 428
		SyncIncest,
		// Token: 0x040001AD RID: 429
		ErrorPathNotFound = -2147024893,
		// Token: 0x040001AE RID: 430
		NoAccess = -2147024891,
		// Token: 0x040001AF RID: 431
		NotEnoughMemory = -2147024882,
		// Token: 0x040001B0 RID: 432
		InvalidParameter = -2147024809,
		// Token: 0x040001B1 RID: 433
		ErrorCanNotComplete = -2147023893,
		// Token: 0x040001B2 RID: 434
		NamedPropQuotaExceeded = -2147219200,
		// Token: 0x040001B3 RID: 435
		TooManyRecips = 1285,
		// Token: 0x040001B4 RID: 436
		TooManyProps,
		// Token: 0x040001B5 RID: 437
		ParameterOverflow = 1104,
		// Token: 0x040001B6 RID: 438
		BadFolderName = 1116,
		// Token: 0x040001B7 RID: 439
		SearchFolder = 1120,
		// Token: 0x040001B8 RID: 440
		NotSearchFolder,
		// Token: 0x040001B9 RID: 441
		FolderSetReceive,
		// Token: 0x040001BA RID: 442
		NoReceiveFolder,
		// Token: 0x040001BB RID: 443
		InvalidRecipients = 1127,
		// Token: 0x040001BC RID: 444
		BufferTooSmall = 1149,
		// Token: 0x040001BD RID: 445
		RequiresRefResolve,
		// Token: 0x040001BE RID: 446
		NullObject = 1209,
		// Token: 0x040001BF RID: 447
		SendAsDenied = 1244,
		// Token: 0x040001C0 RID: 448
		DestinationNullObject = 1283,
		// Token: 0x040001C1 RID: 449
		NoService = 262659,
		// Token: 0x040001C2 RID: 450
		ErrorsReturned = 263040,
		// Token: 0x040001C3 RID: 451
		PositionChanged = 263297,
		// Token: 0x040001C4 RID: 452
		ApproxCount,
		// Token: 0x040001C5 RID: 453
		CancelMessage = 263552,
		// Token: 0x040001C6 RID: 454
		PartialCompletion = 263808,
		// Token: 0x040001C7 RID: 455
		SecurityRequiredLow,
		// Token: 0x040001C8 RID: 456
		SecuirtyRequiredMedium,
		// Token: 0x040001C9 RID: 457
		PartialItems = 263815,
		// Token: 0x040001CA RID: 458
		SyncProgress = 264224,
		// Token: 0x040001CB RID: 459
		SyncClientChangeNewer,
		// Token: 0x040001CC RID: 460
		Exiting = 1005,
		// Token: 0x040001CD RID: 461
		MdbNotInitialized = 1142,
		// Token: 0x040001CE RID: 462
		ServerOutOfMemory = 1008,
		// Token: 0x040001CF RID: 463
		MailboxInTransit = 1292,
		// Token: 0x040001D0 RID: 464
		BackupInProgress,
		// Token: 0x040001D1 RID: 465
		InvalidBackupSequence = 1295,
		// Token: 0x040001D2 RID: 466
		WrongServer = 1144,
		// Token: 0x040001D3 RID: 467
		MailboxQuarantined = 2611,
		// Token: 0x040001D4 RID: 468
		MountInProgress,
		// Token: 0x040001D5 RID: 469
		DismountInProgress,
		// Token: 0x040001D6 RID: 470
		CannotRegisterNewReplidGuidMapping = 2620,
		// Token: 0x040001D7 RID: 471
		CannotRegisterNewNamedPropertyMapping,
		// Token: 0x040001D8 RID: 472
		CannotPreserveMailboxSignature = 2632,
		// Token: 0x040001D9 RID: 473
		ExceptionThrown = 5000,
		// Token: 0x040001DA RID: 474
		SessionLocked,
		// Token: 0x040001DB RID: 475
		DuplicateObject = 1401,
		// Token: 0x040001DC RID: 476
		DuplicateDelivery = 2603,
		// Token: 0x040001DD RID: 477
		UnregisteredNamedProp = 1274,
		// Token: 0x040001DE RID: 478
		TaskRequestFailed = 5002,
		// Token: 0x040001DF RID: 479
		NoReplicaHere = 1128,
		// Token: 0x040001E0 RID: 480
		NoReplicaAvailable
	}
}
