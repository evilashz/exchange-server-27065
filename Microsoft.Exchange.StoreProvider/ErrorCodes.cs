using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ErrorCodes
	{
		// Token: 0x04000195 RID: 405
		internal const int None = 0;

		// Token: 0x04000196 RID: 406
		internal const int StoreTestFailure = 1000;

		// Token: 0x04000197 RID: 407
		internal const int UnknownUser = 1003;

		// Token: 0x04000198 RID: 408
		internal const int Exiting = 1005;

		// Token: 0x04000199 RID: 409
		internal const int LoginPerm = 1010;

		// Token: 0x0400019A RID: 410
		internal const int DatabaseError = 1108;

		// Token: 0x0400019B RID: 411
		internal const int UnsupportedProp = 1110;

		// Token: 0x0400019C RID: 412
		internal const int NoMessages = 2053;

		// Token: 0x0400019D RID: 413
		internal const int WarnCancelMessage = 263552;

		// Token: 0x0400019E RID: 414
		internal const int NoReplicaHere = 1128;

		// Token: 0x0400019F RID: 415
		internal const int NoReplicaAvailable = 1129;

		// Token: 0x040001A0 RID: 416
		internal const int NoRecordFound = 1132;

		// Token: 0x040001A1 RID: 417
		internal const int FormatError = 1261;

		// Token: 0x040001A2 RID: 418
		internal const int MdbNotInit = 1142;

		// Token: 0x040001A3 RID: 419
		internal const int RequiresRefResolve = 1150;

		// Token: 0x040001A4 RID: 420
		internal const int MailboxInTransit = 1292;

		// Token: 0x040001A5 RID: 421
		internal const int InvalidRecips = 1127;

		// Token: 0x040001A6 RID: 422
		internal const int NotPrivateMDB = 1163;

		// Token: 0x040001A7 RID: 423
		internal const int IsintegMDB = 1164;

		// Token: 0x040001A8 RID: 424
		internal const int RecoveryMDBMismatch = 1165;

		// Token: 0x040001A9 RID: 425
		internal const int SearchFolderNotEmpty = 1167;

		// Token: 0x040001AA RID: 426
		internal const int SearchFolderScopeViolation = 1168;

		// Token: 0x040001AB RID: 427
		internal const int CannotDeriveMsgViewFromBase = 1169;

		// Token: 0x040001AC RID: 428
		internal const int MsgHeaderIndexMismatch = 1170;

		// Token: 0x040001AD RID: 429
		internal const int MsgHeaderViewTableMismatch = 1171;

		// Token: 0x040001AE RID: 430
		internal const int CategViewTableMismatch = 1172;

		// Token: 0x040001AF RID: 431
		internal const int CorruptConversation = 1173;

		// Token: 0x040001B0 RID: 432
		internal const int ConversationNotFound = 1174;

		// Token: 0x040001B1 RID: 433
		internal const int ConversationMemberNotFound = 1175;

		// Token: 0x040001B2 RID: 434
		internal const int VersionStoreBusy = 1176;

		// Token: 0x040001B3 RID: 435
		internal const int SearchEvaluationInProgress = 1177;

		// Token: 0x040001B4 RID: 436
		internal const int RecursiveSearchChainTooDeep = 1181;

		// Token: 0x040001B5 RID: 437
		internal const int EmbeddedMessagePropertyCopyFailed = 1182;

		// Token: 0x040001B6 RID: 438
		internal const int GlobalCounterRangeExceeded = 1185;

		// Token: 0x040001B7 RID: 439
		internal const int CorruptMidsetDeleted = 1186;

		// Token: 0x040001B8 RID: 440
		internal const int AssertionFailedError = 1199;

		// Token: 0x040001B9 RID: 441
		internal const int NullObject = 1209;

		// Token: 0x040001BA RID: 442
		internal const int RpcAuthentication = 1212;

		// Token: 0x040001BB RID: 443
		internal const int TooManyRecips = 1285;

		// Token: 0x040001BC RID: 444
		internal const int TooManyProps = 1286;

		// Token: 0x040001BD RID: 445
		internal const int QuotaExceeded = 1241;

		// Token: 0x040001BE RID: 446
		internal const int MaxSubmissionExceeded = 1242;

		// Token: 0x040001BF RID: 447
		internal const int ShutoffQuotaExceeded = 1245;

		// Token: 0x040001C0 RID: 448
		internal const int MessageTooBig = 1236;

		// Token: 0x040001C1 RID: 449
		internal const int FormNotValid = 1237;

		// Token: 0x040001C2 RID: 450
		internal const int NotAuthorized = 1238;

		// Token: 0x040001C3 RID: 451
		internal const int FolderDisabled = 1275;

		// Token: 0x040001C4 RID: 452
		internal const int InvalidRTF = 1235;

		// Token: 0x040001C5 RID: 453
		internal const int SendAsDenied = 1244;

		// Token: 0x040001C6 RID: 454
		internal const int NoCreateRight = 1279;

		// Token: 0x040001C7 RID: 455
		internal const int DataLoss = 1157;

		// Token: 0x040001C8 RID: 456
		internal const int MaxTimeExpired = 1140;

		// Token: 0x040001C9 RID: 457
		internal const int NoCreateSubfolderRight = 1282;

		// Token: 0x040001CA RID: 458
		internal const int WrongMailbox = 1608;

		// Token: 0x040001CB RID: 459
		internal const int FolderNotCleanedUp = 1251;

		// Token: 0x040001CC RID: 460
		internal const int MessagePerFolderCountReceiveQuotaExceeded = 1252;

		// Token: 0x040001CD RID: 461
		internal const int FolderHierarchyChildrenCountReceiveQuotaExceeded = 1253;

		// Token: 0x040001CE RID: 462
		internal const int FolderHierarchyDepthReceiveQuotaExceeded = 1254;

		// Token: 0x040001CF RID: 463
		internal const int DynamicSearchFoldersPerScopeCountReceiveQuotaExceeded = 1255;

		// Token: 0x040001D0 RID: 464
		internal const int FolderHierarchySizeReceiveQuotaExceeded = 1256;

		// Token: 0x040001D1 RID: 465
		internal const int PublicRoot = 1280;

		// Token: 0x040001D2 RID: 466
		internal const int MsgCycle = 1284;

		// Token: 0x040001D3 RID: 467
		internal const int MaxAttachmentExceeded = 1243;

		// Token: 0x040001D4 RID: 468
		internal const int WrongServer = 1144;

		// Token: 0x040001D5 RID: 469
		internal const int VirusScanInProgress = 1290;

		// Token: 0x040001D6 RID: 470
		internal const int VirusDetected = 1291;

		// Token: 0x040001D7 RID: 471
		internal const int BackupInProgress = 1293;

		// Token: 0x040001D8 RID: 472
		internal const int VirusMessageDeleted = 1294;

		// Token: 0x040001D9 RID: 473
		internal const int PropsDontMatch = 1305;

		// Token: 0x040001DA RID: 474
		internal const int DuplicateObject = 1401;

		// Token: 0x040001DB RID: 475
		internal const int ChgPassword = 1612;

		// Token: 0x040001DC RID: 476
		internal const int PwdExpired = 1613;

		// Token: 0x040001DD RID: 477
		internal const int InvWkstn = 1614;

		// Token: 0x040001DE RID: 478
		internal const int InvLogonHrs = 1615;

		// Token: 0x040001DF RID: 479
		internal const int AcctDisabled = 1616;

		// Token: 0x040001E0 RID: 480
		internal const int RuleVersion = 1700;

		// Token: 0x040001E1 RID: 481
		internal const int RuleFormat = 1701;

		// Token: 0x040001E2 RID: 482
		internal const int RuleSendAsDenied = 1702;

		// Token: 0x040001E3 RID: 483
		internal const int CorruptStore = -2147219968;

		// Token: 0x040001E4 RID: 484
		internal const int CorruptData = -2147221221;

		// Token: 0x040001E5 RID: 485
		internal const int NotInQueue = -2147219967;

		// Token: 0x040001E6 RID: 486
		internal const int NotInitialized = -2147219963;

		// Token: 0x040001E7 RID: 487
		internal const int DuplicateName = -2147219964;

		// Token: 0x040001E8 RID: 488
		internal const int FolderHasContents = -2147219958;

		// Token: 0x040001E9 RID: 489
		internal const int FolderHasChildren = -2147219959;

		// Token: 0x040001EA RID: 490
		internal const int Incest = -2147219957;

		// Token: 0x040001EB RID: 491
		internal const int NotImplemented = -2147221246;

		// Token: 0x040001EC RID: 492
		internal const int PropBadValue = -2147220735;

		// Token: 0x040001ED RID: 493
		internal const int InvalidType = -2147220734;

		// Token: 0x040001EE RID: 494
		internal const int TypeNotSupported = -2147220733;

		// Token: 0x040001EF RID: 495
		internal const int NoFreeJses = 1100;

		// Token: 0x040001F0 RID: 496
		internal const int MaxObjsExceeded = 1246;

		// Token: 0x040001F1 RID: 497
		internal const int BufferTooSmall = 1149;

		// Token: 0x040001F2 RID: 498
		internal const int ProtocolDisabled = 2008;

		// Token: 0x040001F3 RID: 499
		internal const int CrossPostDenied = 2039;

		// Token: 0x040001F4 RID: 500
		internal const int NoRpcInterface = 2084;

		// Token: 0x040001F5 RID: 501
		internal const int AmbiguousAlias = 2202;

		// Token: 0x040001F6 RID: 502
		internal const int UnknownMailbox = 2203;

		// Token: 0x040001F7 RID: 503
		internal const int CorruptEvent = 2405;

		// Token: 0x040001F8 RID: 504
		internal const int CorruptWatermark = 2406;

		// Token: 0x040001F9 RID: 505
		internal const int EventError = 2407;

		// Token: 0x040001FA RID: 506
		internal const int WatermarkError = 2408;

		// Token: 0x040001FB RID: 507
		internal const int NonCanonicalACL = 2409;

		// Token: 0x040001FC RID: 508
		internal const int MailboxDisabled = 2412;

		// Token: 0x040001FD RID: 509
		internal const int ClientVerDisallowed = 1247;

		// Token: 0x040001FE RID: 510
		internal const int ServerPaused = 1151;

		// Token: 0x040001FF RID: 511
		internal const int ADUnavailable = 2414;

		// Token: 0x04000200 RID: 512
		internal const int ADError = 2415;

		// Token: 0x04000201 RID: 513
		internal const int ADNotFound = 2417;

		// Token: 0x04000202 RID: 514
		internal const int ADPropertyError = 2418;

		// Token: 0x04000203 RID: 515
		internal const int NotEncrypted = 2416;

		// Token: 0x04000204 RID: 516
		internal const int RpcServerTooBusy = 2419;

		// Token: 0x04000205 RID: 517
		internal const int RpcOutOfMemory = 2420;

		// Token: 0x04000206 RID: 518
		internal const int RpcServerOutOfMemory = 2421;

		// Token: 0x04000207 RID: 519
		internal const int RpcOutOfResources = 2422;

		// Token: 0x04000208 RID: 520
		internal const int RpcServerUnavailable = 2423;

		// Token: 0x04000209 RID: 521
		internal const int ADDuplicateEntry = 2424;

		// Token: 0x0400020A RID: 522
		internal const int ImailConversion = 2425;

		// Token: 0x0400020B RID: 523
		internal const int ImailConversionProhibited = 2427;

		// Token: 0x0400020C RID: 524
		internal const int EventsDeleted = 2428;

		// Token: 0x0400020D RID: 525
		internal const int SubsystemStopping = 2429;

		// Token: 0x0400020E RID: 526
		internal const int SAUnavailable = 2430;

		// Token: 0x0400020F RID: 527
		internal const int CIStopping = 2600;

		// Token: 0x04000210 RID: 528
		internal const int FxInvalidState = 2601;

		// Token: 0x04000211 RID: 529
		internal const int FxUnexpectedMarker = 2602;

		// Token: 0x04000212 RID: 530
		internal const int DuplicateDelivery = 2603;

		// Token: 0x04000213 RID: 531
		internal const int ConditionViolation = 2604;

		// Token: 0x04000214 RID: 532
		internal const int RpcInvalidHandle = 2606;

		// Token: 0x04000215 RID: 533
		internal const int EventNotFound = 2607;

		// Token: 0x04000216 RID: 534
		internal const int PropNotPromoted = 2608;

		// Token: 0x04000217 RID: 535
		internal const int LowDatabaseDiskSpace = 2609;

		// Token: 0x04000218 RID: 536
		internal const int LowDatabaseLogDiskSpace = 2610;

		// Token: 0x04000219 RID: 537
		internal const int MailboxQuarantined = 2611;

		// Token: 0x0400021A RID: 538
		internal const int MountInProgress = 2612;

		// Token: 0x0400021B RID: 539
		internal const int DismountInProgress = 2613;

		// Token: 0x0400021C RID: 540
		internal const int InvalidPool = 2617;

		// Token: 0x0400021D RID: 541
		internal const int VirusScannerError = 2618;

		// Token: 0x0400021E RID: 542
		internal const int GranularReplInitFailed = 2619;

		// Token: 0x0400021F RID: 543
		internal const int CannotRegisterNewReplidGuidMapping = 2620;

		// Token: 0x04000220 RID: 544
		internal const int CannotRegisterNewNamedPropertyMapping = 2621;

		// Token: 0x04000221 RID: 545
		internal const int GranularReplInvalidParameter = 2625;

		// Token: 0x04000222 RID: 546
		internal const int GranularReplStillInUse = 2626;

		// Token: 0x04000223 RID: 547
		internal const int GranularReplCommunicationFailed = 2628;

		// Token: 0x04000224 RID: 548
		internal const int CannotPreserveMailboxSignature = 2632;

		// Token: 0x04000225 RID: 549
		internal const int UnexpectedState = 2634;

		// Token: 0x04000226 RID: 550
		internal const int MailboxSoftDeleted = 2635;

		// Token: 0x04000227 RID: 551
		internal const int DatabaseStateConflict = 2636;

		// Token: 0x04000228 RID: 552
		internal const int RpcInvalidSession = 2637;

		// Token: 0x04000229 RID: 553
		internal const int MaxThreadsPerMdbExceeded = 2700;

		// Token: 0x0400022A RID: 554
		internal const int MaxThreadsPerSCTExceeded = 2701;

		// Token: 0x0400022B RID: 555
		internal const int WrongProvisionedFid = 2702;

		// Token: 0x0400022C RID: 556
		internal const int ISIntegMdbTaskExceeded = 2703;

		// Token: 0x0400022D RID: 557
		internal const int ISIntegQueueFull = 2704;

		// Token: 0x0400022E RID: 558
		internal const int InvalidMultiMailboxSearchRequest = 2800;

		// Token: 0x0400022F RID: 559
		internal const int InvalidMultiMailboxKeywordStatsRequest = 2801;

		// Token: 0x04000230 RID: 560
		internal const int MultiMailboxSearchFailed = 2802;

		// Token: 0x04000231 RID: 561
		internal const int MaxMultiMailboxSearchExceeded = 2803;

		// Token: 0x04000232 RID: 562
		internal const int MultiMailboxSearchOperationFailed = 2804;

		// Token: 0x04000233 RID: 563
		internal const int MultiMailboxSearchNonFullTextSearch = 2805;

		// Token: 0x04000234 RID: 564
		internal const int MultiMailboxSearchTimeOut = 2806;

		// Token: 0x04000235 RID: 565
		internal const int MultiMailboxKeywordStatsTimeOut = 2807;

		// Token: 0x04000236 RID: 566
		internal const int MultiMailboxSearchInvalidSortBy = 2808;

		// Token: 0x04000237 RID: 567
		internal const int MultiMailboxSearchNonFullTextSortBy = 2809;

		// Token: 0x04000238 RID: 568
		internal const int MultiMailboxSearchInvalidPagination = 2810;

		// Token: 0x04000239 RID: 569
		internal const int MultiMailboxSearchNonFullTextPropertyInPagination = 2811;

		// Token: 0x0400023A RID: 570
		internal const int MultiMailboxSearchMailboxNotFound = 2812;

		// Token: 0x0400023B RID: 571
		internal const int MultiMailboxSearchInvalidRestriction = 2813;

		// Token: 0x0400023C RID: 572
		internal const int UserInformationAlreadyExists = 2830;

		// Token: 0x0400023D RID: 573
		internal const int UserInformationLockTimeout = 2831;

		// Token: 0x0400023E RID: 574
		internal const int UserInformationNotFound = 2832;

		// Token: 0x0400023F RID: 575
		internal const int UserInformationNoAccess = 2833;

		// Token: 0x04000240 RID: 576
		internal const int JetWarningColumnMaxTruncated = 1512;

		// Token: 0x04000241 RID: 577
		internal const int JetErrorDatabaseBufferDependenciesCorrupted = -255;

		// Token: 0x04000242 RID: 578
		internal const int JetErrorLogWriteFail = -510;

		// Token: 0x04000243 RID: 579
		internal const int JetErrorBadParentPageLink = -338;

		// Token: 0x04000244 RID: 580
		internal const int JetErrorMissingLogFile = -528;

		// Token: 0x04000245 RID: 581
		internal const int JetErrorLogDiskFull = -529;

		// Token: 0x04000246 RID: 582
		internal const int JetErrorRequiredLogFilesMissing = -543;

		// Token: 0x04000247 RID: 583
		internal const int JetErrorConsistentTimeMismatch = -551;

		// Token: 0x04000248 RID: 584
		internal const int JetErrorCommittedLogFilesMissing = -582;

		// Token: 0x04000249 RID: 585
		internal const int JetErrorCommittedLogFilesCorrupt = -586;

		// Token: 0x0400024A RID: 586
		internal const int JetErrorUnicodeTranslationFail = -602;

		// Token: 0x0400024B RID: 587
		internal const int JetErrorCheckpointDepthTooDeep = -614;

		// Token: 0x0400024C RID: 588
		internal const int JetErrorOutOfMemory = -1011;

		// Token: 0x0400024D RID: 589
		internal const int JetErrorOutOfCursors = -1013;

		// Token: 0x0400024E RID: 590
		internal const int JetErrorOutOfBuffers = -1014;

		// Token: 0x0400024F RID: 591
		internal const int JetErrorRecordDeleted = -1017;

		// Token: 0x04000250 RID: 592
		internal const int JetErrorReadVerifyFailure = -1018;

		// Token: 0x04000251 RID: 593
		internal const int JetErrorPageNotInitialized = -1019;

		// Token: 0x04000252 RID: 594
		internal const int JetErrorDiskIO = -1022;

		// Token: 0x04000253 RID: 595
		internal const int JetErrorRecordTooBig = -1026;

		// Token: 0x04000254 RID: 596
		internal const int JetErrorInvalidBufferSize = -1047;

		// Token: 0x04000255 RID: 597
		internal const int JetErrorInvalidLanguageId = -1062;

		// Token: 0x04000256 RID: 598
		internal const int JetErrorVersionStoreOutOfMemoryAndCleanupTimedOut = -1066;

		// Token: 0x04000257 RID: 599
		internal const int JetErrorVersionStoreOutOfMemory = -1069;

		// Token: 0x04000258 RID: 600
		internal const int JetErrorInstanceNameInUse = -1086;

		// Token: 0x04000259 RID: 601
		internal const int JetErrorInstanceUnavailable = -1090;

		// Token: 0x0400025A RID: 602
		internal const int JetErrorInstanceUnavailableDueToFatalLogDiskFull = -1092;

		// Token: 0x0400025B RID: 603
		internal const int JetErrorOutOfSessions = -1101;

		// Token: 0x0400025C RID: 604
		internal const int JetErrorWriteConflict = -1102;

		// Token: 0x0400025D RID: 605
		internal const int JetErrorInvalidSesid = -1104;

		// Token: 0x0400025E RID: 606
		internal const int JetErrorDatabaseNotFound = -1203;

		// Token: 0x0400025F RID: 607
		internal const int JetErrorDatabaseCorrupted = -1206;

		// Token: 0x04000260 RID: 608
		internal const int JetErrorAttachedDatabaseMismatch = -1216;

		// Token: 0x04000261 RID: 609
		internal const int JetErrorDatabaseInvalidPath = -1217;

		// Token: 0x04000262 RID: 610
		internal const int JetErrorTableLocked = -1302;

		// Token: 0x04000263 RID: 611
		internal const int JetErrorTableDuplicate = -1303;

		// Token: 0x04000264 RID: 612
		internal const int JetErrorTableInUse = -1304;

		// Token: 0x04000265 RID: 613
		internal const int JetErrorObjectNotFound = -1305;

		// Token: 0x04000266 RID: 614
		internal const int JetErrorTooManyOpenTables = -1311;

		// Token: 0x04000267 RID: 615
		internal const int JetErrorTooManyOpenTablesAndCleanupTimedOut = -1313;

		// Token: 0x04000268 RID: 616
		internal const int JetErrorIndexNotFound = -1404;

		// Token: 0x04000269 RID: 617
		internal const int JetErrorColumnTooBig = -1506;

		// Token: 0x0400026A RID: 618
		internal const int JetErrorColumnNotFound = -1507;

		// Token: 0x0400026B RID: 619
		internal const int JetErrorBadColumnId = -1517;

		// Token: 0x0400026C RID: 620
		internal const int JetErrorDefaultValueTooBig = -1524;

		// Token: 0x0400026D RID: 621
		internal const int JetErrorLVCorrupted = -1526;

		// Token: 0x0400026E RID: 622
		internal const int JetErrorRecordNotFound = -1601;

		// Token: 0x0400026F RID: 623
		internal const int JetErrorNoCurrentRecord = -1603;

		// Token: 0x04000270 RID: 624
		internal const int JetErrorKeyDuplicate = -1605;

		// Token: 0x04000271 RID: 625
		internal const int JetErrorDiskFull = -1808;

		// Token: 0x04000272 RID: 626
		internal const int JetErrorFileNotFound = -1811;

		// Token: 0x04000273 RID: 627
		internal const int JetErrorFileIOBeyondEOF = -4001;
	}
}
