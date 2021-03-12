using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000287 RID: 647
	public enum JET_err
	{
		// Token: 0x0400053F RID: 1343
		Success,
		// Token: 0x04000540 RID: 1344
		RfsFailure = -100,
		// Token: 0x04000541 RID: 1345
		RfsNotArmed = -101,
		// Token: 0x04000542 RID: 1346
		FileClose = -102,
		// Token: 0x04000543 RID: 1347
		OutOfThreads = -103,
		// Token: 0x04000544 RID: 1348
		TooManyIO = -105,
		// Token: 0x04000545 RID: 1349
		TaskDropped = -106,
		// Token: 0x04000546 RID: 1350
		InternalError = -107,
		// Token: 0x04000547 RID: 1351
		DisabledFunctionality = -112,
		// Token: 0x04000548 RID: 1352
		UnloadableOSFunctionality = -113,
		// Token: 0x04000549 RID: 1353
		DatabaseBufferDependenciesCorrupted = -255,
		// Token: 0x0400054A RID: 1354
		PreviousVersion = -322,
		// Token: 0x0400054B RID: 1355
		PageBoundary = -323,
		// Token: 0x0400054C RID: 1356
		KeyBoundary = -324,
		// Token: 0x0400054D RID: 1357
		BadPageLink = -327,
		// Token: 0x0400054E RID: 1358
		BadBookmark = -328,
		// Token: 0x0400054F RID: 1359
		NTSystemCallFailed = -334,
		// Token: 0x04000550 RID: 1360
		BadParentPageLink = -338,
		// Token: 0x04000551 RID: 1361
		SPAvailExtCacheOutOfSync = -340,
		// Token: 0x04000552 RID: 1362
		SPAvailExtCorrupted = -341,
		// Token: 0x04000553 RID: 1363
		SPAvailExtCacheOutOfMemory = -342,
		// Token: 0x04000554 RID: 1364
		SPOwnExtCorrupted = -343,
		// Token: 0x04000555 RID: 1365
		DbTimeCorrupted = -344,
		// Token: 0x04000556 RID: 1366
		KeyTruncated = -346,
		// Token: 0x04000557 RID: 1367
		DatabaseLeakInSpace = -348,
		// Token: 0x04000558 RID: 1368
		BadEmptyPage = -351,
		// Token: 0x04000559 RID: 1369
		KeyTooBig = -408,
		// Token: 0x0400055A RID: 1370
		CannotSeparateIntrinsicLV = -416,
		// Token: 0x0400055B RID: 1371
		SeparatedLongValue = -421,
		// Token: 0x0400055C RID: 1372
		MustBeSeparateLongValue = -423,
		// Token: 0x0400055D RID: 1373
		InvalidPreread = -424,
		// Token: 0x0400055E RID: 1374
		InvalidLoggedOperation = -500,
		// Token: 0x0400055F RID: 1375
		LogFileCorrupt = -501,
		// Token: 0x04000560 RID: 1376
		NoBackupDirectory = -503,
		// Token: 0x04000561 RID: 1377
		BackupDirectoryNotEmpty = -504,
		// Token: 0x04000562 RID: 1378
		BackupInProgress = -505,
		// Token: 0x04000563 RID: 1379
		RestoreInProgress = -506,
		// Token: 0x04000564 RID: 1380
		MissingPreviousLogFile = -509,
		// Token: 0x04000565 RID: 1381
		LogWriteFail = -510,
		// Token: 0x04000566 RID: 1382
		LogDisabledDueToRecoveryFailure = -511,
		// Token: 0x04000567 RID: 1383
		CannotLogDuringRecoveryRedo = -512,
		// Token: 0x04000568 RID: 1384
		LogGenerationMismatch = -513,
		// Token: 0x04000569 RID: 1385
		BadLogVersion = -514,
		// Token: 0x0400056A RID: 1386
		InvalidLogSequence = -515,
		// Token: 0x0400056B RID: 1387
		LoggingDisabled = -516,
		// Token: 0x0400056C RID: 1388
		LogBufferTooSmall = -517,
		// Token: 0x0400056D RID: 1389
		LogSequenceEnd = -519,
		// Token: 0x0400056E RID: 1390
		NoBackup = -520,
		// Token: 0x0400056F RID: 1391
		InvalidBackupSequence = -521,
		// Token: 0x04000570 RID: 1392
		BackupNotAllowedYet = -523,
		// Token: 0x04000571 RID: 1393
		DeleteBackupFileFail = -524,
		// Token: 0x04000572 RID: 1394
		MakeBackupDirectoryFail = -525,
		// Token: 0x04000573 RID: 1395
		InvalidBackup = -526,
		// Token: 0x04000574 RID: 1396
		RecoveredWithErrors = -527,
		// Token: 0x04000575 RID: 1397
		MissingLogFile = -528,
		// Token: 0x04000576 RID: 1398
		LogDiskFull = -529,
		// Token: 0x04000577 RID: 1399
		BadLogSignature = -530,
		// Token: 0x04000578 RID: 1400
		BadDbSignature = -531,
		// Token: 0x04000579 RID: 1401
		BadCheckpointSignature = -532,
		// Token: 0x0400057A RID: 1402
		CheckpointCorrupt = -533,
		// Token: 0x0400057B RID: 1403
		MissingPatchPage = -534,
		// Token: 0x0400057C RID: 1404
		BadPatchPage = -535,
		// Token: 0x0400057D RID: 1405
		RedoAbruptEnded = -536,
		// Token: 0x0400057E RID: 1406
		PatchFileMissing = -538,
		// Token: 0x0400057F RID: 1407
		DatabaseLogSetMismatch = -539,
		// Token: 0x04000580 RID: 1408
		DatabaseStreamingFileMismatch = -540,
		// Token: 0x04000581 RID: 1409
		LogFileSizeMismatch = -541,
		// Token: 0x04000582 RID: 1410
		CheckpointFileNotFound = -542,
		// Token: 0x04000583 RID: 1411
		RequiredLogFilesMissing = -543,
		// Token: 0x04000584 RID: 1412
		SoftRecoveryOnBackupDatabase = -544,
		// Token: 0x04000585 RID: 1413
		LogFileSizeMismatchDatabasesConsistent = -545,
		// Token: 0x04000586 RID: 1414
		LogSectorSizeMismatch = -546,
		// Token: 0x04000587 RID: 1415
		LogSectorSizeMismatchDatabasesConsistent = -547,
		// Token: 0x04000588 RID: 1416
		LogSequenceEndDatabasesConsistent = -548,
		// Token: 0x04000589 RID: 1417
		StreamingDataNotLogged = -549,
		// Token: 0x0400058A RID: 1418
		DatabaseDirtyShutdown = -550,
		// Token: 0x0400058B RID: 1419
		ConsistentTimeMismatch = -551,
		// Token: 0x0400058C RID: 1420
		DatabasePatchFileMismatch = -552,
		// Token: 0x0400058D RID: 1421
		EndingRestoreLogTooLow = -553,
		// Token: 0x0400058E RID: 1422
		StartingRestoreLogTooHigh = -554,
		// Token: 0x0400058F RID: 1423
		GivenLogFileHasBadSignature = -555,
		// Token: 0x04000590 RID: 1424
		GivenLogFileIsNotContiguous = -556,
		// Token: 0x04000591 RID: 1425
		MissingRestoreLogFiles = -557,
		// Token: 0x04000592 RID: 1426
		MissingFullBackup = -560,
		// Token: 0x04000593 RID: 1427
		BadBackupDatabaseSize = -561,
		// Token: 0x04000594 RID: 1428
		DatabaseAlreadyUpgraded = -562,
		// Token: 0x04000595 RID: 1429
		DatabaseIncompleteUpgrade = -563,
		// Token: 0x04000596 RID: 1430
		MissingCurrentLogFiles = -565,
		// Token: 0x04000597 RID: 1431
		DbTimeTooOld = -566,
		// Token: 0x04000598 RID: 1432
		DbTimeTooNew = -567,
		// Token: 0x04000599 RID: 1433
		MissingFileToBackup = -569,
		// Token: 0x0400059A RID: 1434
		LogTornWriteDuringHardRestore = -570,
		// Token: 0x0400059B RID: 1435
		LogTornWriteDuringHardRecovery = -571,
		// Token: 0x0400059C RID: 1436
		LogCorruptDuringHardRestore = -573,
		// Token: 0x0400059D RID: 1437
		LogCorruptDuringHardRecovery = -574,
		// Token: 0x0400059E RID: 1438
		MustDisableLoggingForDbUpgrade = -575,
		// Token: 0x0400059F RID: 1439
		BadRestoreTargetInstance = -577,
		// Token: 0x040005A0 RID: 1440
		RecoveredWithoutUndo = -579,
		// Token: 0x040005A1 RID: 1441
		DatabasesNotFromSameSnapshot = -580,
		// Token: 0x040005A2 RID: 1442
		SoftRecoveryOnSnapshot = -581,
		// Token: 0x040005A3 RID: 1443
		CommittedLogFilesMissing = -582,
		// Token: 0x040005A4 RID: 1444
		SectorSizeNotSupported = -583,
		// Token: 0x040005A5 RID: 1445
		RecoveredWithoutUndoDatabasesConsistent = -584,
		// Token: 0x040005A6 RID: 1446
		CommittedLogFileCorrupt = -586,
		// Token: 0x040005A7 RID: 1447
		UnicodeTranslationBufferTooSmall = -601,
		// Token: 0x040005A8 RID: 1448
		UnicodeTranslationFail = -602,
		// Token: 0x040005A9 RID: 1449
		UnicodeNormalizationNotSupported = -603,
		// Token: 0x040005AA RID: 1450
		UnicodeLanguageValidationFailure = -604,
		// Token: 0x040005AB RID: 1451
		ExistingLogFileHasBadSignature = -610,
		// Token: 0x040005AC RID: 1452
		ExistingLogFileIsNotContiguous = -611,
		// Token: 0x040005AD RID: 1453
		LogReadVerifyFailure = -612,
		// Token: 0x040005AE RID: 1454
		CheckpointDepthTooDeep = -614,
		// Token: 0x040005AF RID: 1455
		RestoreOfNonBackupDatabase = -615,
		// Token: 0x040005B0 RID: 1456
		LogFileNotCopied = -616,
		// Token: 0x040005B1 RID: 1457
		SurrogateBackupInProgress = -617,
		// Token: 0x040005B2 RID: 1458
		TransactionTooLong = -618,
		// Token: 0x040005B3 RID: 1459
		BackupAbortByServer = -801,
		// Token: 0x040005B4 RID: 1460
		InvalidGrbit = -900,
		// Token: 0x040005B5 RID: 1461
		TermInProgress = -1000,
		// Token: 0x040005B6 RID: 1462
		FeatureNotAvailable = -1001,
		// Token: 0x040005B7 RID: 1463
		InvalidName = -1002,
		// Token: 0x040005B8 RID: 1464
		InvalidParameter = -1003,
		// Token: 0x040005B9 RID: 1465
		DatabaseFileReadOnly = -1008,
		// Token: 0x040005BA RID: 1466
		InvalidDatabaseId = -1010,
		// Token: 0x040005BB RID: 1467
		OutOfMemory = -1011,
		// Token: 0x040005BC RID: 1468
		OutOfDatabaseSpace = -1012,
		// Token: 0x040005BD RID: 1469
		OutOfCursors = -1013,
		// Token: 0x040005BE RID: 1470
		OutOfBuffers = -1014,
		// Token: 0x040005BF RID: 1471
		TooManyIndexes = -1015,
		// Token: 0x040005C0 RID: 1472
		TooManyKeys = -1016,
		// Token: 0x040005C1 RID: 1473
		RecordDeleted = -1017,
		// Token: 0x040005C2 RID: 1474
		ReadVerifyFailure = -1018,
		// Token: 0x040005C3 RID: 1475
		PageNotInitialized = -1019,
		// Token: 0x040005C4 RID: 1476
		OutOfFileHandles = -1020,
		// Token: 0x040005C5 RID: 1477
		DiskReadVerificationFailure = -1021,
		// Token: 0x040005C6 RID: 1478
		DiskIO = -1022,
		// Token: 0x040005C7 RID: 1479
		InvalidPath = -1023,
		// Token: 0x040005C8 RID: 1480
		InvalidSystemPath = -1024,
		// Token: 0x040005C9 RID: 1481
		InvalidLogDirectory = -1025,
		// Token: 0x040005CA RID: 1482
		RecordTooBig = -1026,
		// Token: 0x040005CB RID: 1483
		TooManyOpenDatabases = -1027,
		// Token: 0x040005CC RID: 1484
		InvalidDatabase = -1028,
		// Token: 0x040005CD RID: 1485
		NotInitialized = -1029,
		// Token: 0x040005CE RID: 1486
		AlreadyInitialized = -1030,
		// Token: 0x040005CF RID: 1487
		InitInProgress = -1031,
		// Token: 0x040005D0 RID: 1488
		FileAccessDenied = -1032,
		// Token: 0x040005D1 RID: 1489
		QueryNotSupported = -1034,
		// Token: 0x040005D2 RID: 1490
		SQLLinkNotSupported = -1035,
		// Token: 0x040005D3 RID: 1491
		BufferTooSmall = -1038,
		// Token: 0x040005D4 RID: 1492
		TooManyColumns = -1040,
		// Token: 0x040005D5 RID: 1493
		ContainerNotEmpty = -1043,
		// Token: 0x040005D6 RID: 1494
		InvalidFilename = -1044,
		// Token: 0x040005D7 RID: 1495
		InvalidBookmark = -1045,
		// Token: 0x040005D8 RID: 1496
		ColumnInUse = -1046,
		// Token: 0x040005D9 RID: 1497
		InvalidBufferSize = -1047,
		// Token: 0x040005DA RID: 1498
		ColumnNotUpdatable = -1048,
		// Token: 0x040005DB RID: 1499
		IndexInUse = -1051,
		// Token: 0x040005DC RID: 1500
		LinkNotSupported = -1052,
		// Token: 0x040005DD RID: 1501
		NullKeyDisallowed = -1053,
		// Token: 0x040005DE RID: 1502
		NotInTransaction = -1054,
		// Token: 0x040005DF RID: 1503
		MustRollback = -1057,
		// Token: 0x040005E0 RID: 1504
		TooManyActiveUsers = -1059,
		// Token: 0x040005E1 RID: 1505
		InvalidCountry = -1061,
		// Token: 0x040005E2 RID: 1506
		InvalidLanguageId = -1062,
		// Token: 0x040005E3 RID: 1507
		InvalidCodePage = -1063,
		// Token: 0x040005E4 RID: 1508
		InvalidLCMapStringFlags = -1064,
		// Token: 0x040005E5 RID: 1509
		VersionStoreEntryTooBig = -1065,
		// Token: 0x040005E6 RID: 1510
		VersionStoreOutOfMemoryAndCleanupTimedOut = -1066,
		// Token: 0x040005E7 RID: 1511
		VersionStoreOutOfMemory = -1069,
		// Token: 0x040005E8 RID: 1512
		CurrencyStackOutOfMemory = -1070,
		// Token: 0x040005E9 RID: 1513
		CannotIndex = -1071,
		// Token: 0x040005EA RID: 1514
		RecordNotDeleted = -1072,
		// Token: 0x040005EB RID: 1515
		TooManyMempoolEntries = -1073,
		// Token: 0x040005EC RID: 1516
		OutOfObjectIDs = -1074,
		// Token: 0x040005ED RID: 1517
		OutOfLongValueIDs = -1075,
		// Token: 0x040005EE RID: 1518
		OutOfAutoincrementValues = -1076,
		// Token: 0x040005EF RID: 1519
		OutOfDbtimeValues = -1077,
		// Token: 0x040005F0 RID: 1520
		OutOfSequentialIndexValues = -1078,
		// Token: 0x040005F1 RID: 1521
		RunningInOneInstanceMode = -1080,
		// Token: 0x040005F2 RID: 1522
		RunningInMultiInstanceMode = -1081,
		// Token: 0x040005F3 RID: 1523
		SystemParamsAlreadySet = -1082,
		// Token: 0x040005F4 RID: 1524
		SystemPathInUse = -1083,
		// Token: 0x040005F5 RID: 1525
		LogFilePathInUse = -1084,
		// Token: 0x040005F6 RID: 1526
		TempPathInUse = -1085,
		// Token: 0x040005F7 RID: 1527
		InstanceNameInUse = -1086,
		// Token: 0x040005F8 RID: 1528
		SystemParameterConflict = -1087,
		// Token: 0x040005F9 RID: 1529
		InstanceUnavailable = -1090,
		// Token: 0x040005FA RID: 1530
		DatabaseUnavailable = -1091,
		// Token: 0x040005FB RID: 1531
		InstanceUnavailableDueToFatalLogDiskFull = -1092,
		// Token: 0x040005FC RID: 1532
		OutOfSessions = -1101,
		// Token: 0x040005FD RID: 1533
		WriteConflict = -1102,
		// Token: 0x040005FE RID: 1534
		TransTooDeep = -1103,
		// Token: 0x040005FF RID: 1535
		InvalidSesid = -1104,
		// Token: 0x04000600 RID: 1536
		WriteConflictPrimaryIndex = -1105,
		// Token: 0x04000601 RID: 1537
		InTransaction = -1108,
		// Token: 0x04000602 RID: 1538
		RollbackRequired = -1109,
		// Token: 0x04000603 RID: 1539
		TransReadOnly = -1110,
		// Token: 0x04000604 RID: 1540
		SessionWriteConflict = -1111,
		// Token: 0x04000605 RID: 1541
		RecordTooBigForBackwardCompatibility = -1112,
		// Token: 0x04000606 RID: 1542
		CannotMaterializeForwardOnlySort = -1113,
		// Token: 0x04000607 RID: 1543
		SesidTableIdMismatch = -1114,
		// Token: 0x04000608 RID: 1544
		InvalidInstance = -1115,
		// Token: 0x04000609 RID: 1545
		DirtyShutdown = -1116,
		// Token: 0x0400060A RID: 1546
		ReadPgnoVerifyFailure = -1118,
		// Token: 0x0400060B RID: 1547
		ReadLostFlushVerifyFailure = -1119,
		// Token: 0x0400060C RID: 1548
		FileSystemCorruption = -1121,
		// Token: 0x0400060D RID: 1549
		RecoveryVerifyFailure = -1123,
		// Token: 0x0400060E RID: 1550
		FilteredMoveNotSupported = -1124,
		// Token: 0x0400060F RID: 1551
		MustCommitDistributedTransactionToLevel0 = -1150,
		// Token: 0x04000610 RID: 1552
		DistributedTransactionAlreadyPreparedToCommit = -1151,
		// Token: 0x04000611 RID: 1553
		NotInDistributedTransaction = -1152,
		// Token: 0x04000612 RID: 1554
		DistributedTransactionNotYetPreparedToCommit = -1153,
		// Token: 0x04000613 RID: 1555
		CannotNestDistributedTransactions = -1154,
		// Token: 0x04000614 RID: 1556
		DTCMissingCallback = -1160,
		// Token: 0x04000615 RID: 1557
		DTCMissingCallbackOnRecovery = -1161,
		// Token: 0x04000616 RID: 1558
		DTCCallbackUnexpectedError = -1162,
		// Token: 0x04000617 RID: 1559
		DatabaseDuplicate = -1201,
		// Token: 0x04000618 RID: 1560
		DatabaseInUse = -1202,
		// Token: 0x04000619 RID: 1561
		DatabaseNotFound = -1203,
		// Token: 0x0400061A RID: 1562
		DatabaseInvalidName = -1204,
		// Token: 0x0400061B RID: 1563
		DatabaseInvalidPages = -1205,
		// Token: 0x0400061C RID: 1564
		DatabaseCorrupted = -1206,
		// Token: 0x0400061D RID: 1565
		DatabaseLocked = -1207,
		// Token: 0x0400061E RID: 1566
		CannotDisableVersioning = -1208,
		// Token: 0x0400061F RID: 1567
		InvalidDatabaseVersion = -1209,
		// Token: 0x04000620 RID: 1568
		Database200Format = -1210,
		// Token: 0x04000621 RID: 1569
		Database400Format = -1211,
		// Token: 0x04000622 RID: 1570
		Database500Format = -1212,
		// Token: 0x04000623 RID: 1571
		PageSizeMismatch = -1213,
		// Token: 0x04000624 RID: 1572
		TooManyInstances = -1214,
		// Token: 0x04000625 RID: 1573
		DatabaseSharingViolation = -1215,
		// Token: 0x04000626 RID: 1574
		AttachedDatabaseMismatch = -1216,
		// Token: 0x04000627 RID: 1575
		DatabaseInvalidPath = -1217,
		// Token: 0x04000628 RID: 1576
		DatabaseIdInUse = -1218,
		// Token: 0x04000629 RID: 1577
		ForceDetachNotAllowed = -1219,
		// Token: 0x0400062A RID: 1578
		CatalogCorrupted = -1220,
		// Token: 0x0400062B RID: 1579
		PartiallyAttachedDB = -1221,
		// Token: 0x0400062C RID: 1580
		DatabaseSignInUse = -1222,
		// Token: 0x0400062D RID: 1581
		DatabaseCorruptedNoRepair = -1224,
		// Token: 0x0400062E RID: 1582
		InvalidCreateDbVersion = -1225,
		// Token: 0x0400062F RID: 1583
		DatabaseIncompleteIncrementalReseed = -1226,
		// Token: 0x04000630 RID: 1584
		DatabaseInvalidIncrementalReseed = -1227,
		// Token: 0x04000631 RID: 1585
		DatabaseFailedIncrementalReseed = -1228,
		// Token: 0x04000632 RID: 1586
		NoAttachmentsFailedIncrementalReseed = -1229,
		// Token: 0x04000633 RID: 1587
		TableLocked = -1302,
		// Token: 0x04000634 RID: 1588
		TableDuplicate = -1303,
		// Token: 0x04000635 RID: 1589
		TableInUse = -1304,
		// Token: 0x04000636 RID: 1590
		ObjectNotFound = -1305,
		// Token: 0x04000637 RID: 1591
		DensityInvalid = -1307,
		// Token: 0x04000638 RID: 1592
		TableNotEmpty = -1308,
		// Token: 0x04000639 RID: 1593
		InvalidTableId = -1310,
		// Token: 0x0400063A RID: 1594
		TooManyOpenTables = -1311,
		// Token: 0x0400063B RID: 1595
		IllegalOperation = -1312,
		// Token: 0x0400063C RID: 1596
		TooManyOpenTablesAndCleanupTimedOut = -1313,
		// Token: 0x0400063D RID: 1597
		ObjectDuplicate = -1314,
		// Token: 0x0400063E RID: 1598
		InvalidObject = -1316,
		// Token: 0x0400063F RID: 1599
		CannotDeleteTempTable = -1317,
		// Token: 0x04000640 RID: 1600
		CannotDeleteSystemTable = -1318,
		// Token: 0x04000641 RID: 1601
		CannotDeleteTemplateTable = -1319,
		// Token: 0x04000642 RID: 1602
		ExclusiveTableLockRequired = -1322,
		// Token: 0x04000643 RID: 1603
		FixedDDL = -1323,
		// Token: 0x04000644 RID: 1604
		FixedInheritedDDL = -1324,
		// Token: 0x04000645 RID: 1605
		CannotNestDDL = -1325,
		// Token: 0x04000646 RID: 1606
		DDLNotInheritable = -1326,
		// Token: 0x04000647 RID: 1607
		InvalidSettings = -1328,
		// Token: 0x04000648 RID: 1608
		ClientRequestToStopJetService = -1329,
		// Token: 0x04000649 RID: 1609
		CannotAddFixedVarColumnToDerivedTable = -1330,
		// Token: 0x0400064A RID: 1610
		IndexCantBuild = -1401,
		// Token: 0x0400064B RID: 1611
		IndexHasPrimary = -1402,
		// Token: 0x0400064C RID: 1612
		IndexDuplicate = -1403,
		// Token: 0x0400064D RID: 1613
		IndexNotFound = -1404,
		// Token: 0x0400064E RID: 1614
		IndexMustStay = -1405,
		// Token: 0x0400064F RID: 1615
		IndexInvalidDef = -1406,
		// Token: 0x04000650 RID: 1616
		InvalidCreateIndex = -1409,
		// Token: 0x04000651 RID: 1617
		TooManyOpenIndexes = -1410,
		// Token: 0x04000652 RID: 1618
		MultiValuedIndexViolation = -1411,
		// Token: 0x04000653 RID: 1619
		IndexBuildCorrupted = -1412,
		// Token: 0x04000654 RID: 1620
		PrimaryIndexCorrupted = -1413,
		// Token: 0x04000655 RID: 1621
		SecondaryIndexCorrupted = -1414,
		// Token: 0x04000656 RID: 1622
		InvalidIndexId = -1416,
		// Token: 0x04000657 RID: 1623
		IndexTuplesSecondaryIndexOnly = -1430,
		// Token: 0x04000658 RID: 1624
		IndexTuplesTooManyColumns = -1431,
		// Token: 0x04000659 RID: 1625
		IndexTuplesNonUniqueOnly = -1432,
		// Token: 0x0400065A RID: 1626
		IndexTuplesTextBinaryColumnsOnly = -1433,
		// Token: 0x0400065B RID: 1627
		IndexTuplesVarSegMacNotAllowed = -1434,
		// Token: 0x0400065C RID: 1628
		IndexTuplesInvalidLimits = -1435,
		// Token: 0x0400065D RID: 1629
		IndexTuplesCannotRetrieveFromIndex = -1436,
		// Token: 0x0400065E RID: 1630
		IndexTuplesKeyTooSmall = -1437,
		// Token: 0x0400065F RID: 1631
		ColumnLong = -1501,
		// Token: 0x04000660 RID: 1632
		ColumnNoChunk = -1502,
		// Token: 0x04000661 RID: 1633
		ColumnDoesNotFit = -1503,
		// Token: 0x04000662 RID: 1634
		NullInvalid = -1504,
		// Token: 0x04000663 RID: 1635
		ColumnIndexed = -1505,
		// Token: 0x04000664 RID: 1636
		ColumnTooBig = -1506,
		// Token: 0x04000665 RID: 1637
		ColumnNotFound = -1507,
		// Token: 0x04000666 RID: 1638
		ColumnDuplicate = -1508,
		// Token: 0x04000667 RID: 1639
		MultiValuedColumnMustBeTagged = -1509,
		// Token: 0x04000668 RID: 1640
		ColumnRedundant = -1510,
		// Token: 0x04000669 RID: 1641
		InvalidColumnType = -1511,
		// Token: 0x0400066A RID: 1642
		TaggedNotNULL = -1514,
		// Token: 0x0400066B RID: 1643
		NoCurrentIndex = -1515,
		// Token: 0x0400066C RID: 1644
		KeyIsMade = -1516,
		// Token: 0x0400066D RID: 1645
		BadColumnId = -1517,
		// Token: 0x0400066E RID: 1646
		BadItagSequence = -1518,
		// Token: 0x0400066F RID: 1647
		ColumnInRelationship = -1519,
		// Token: 0x04000670 RID: 1648
		CannotBeTagged = -1521,
		// Token: 0x04000671 RID: 1649
		DefaultValueTooBig = -1524,
		// Token: 0x04000672 RID: 1650
		MultiValuedDuplicate = -1525,
		// Token: 0x04000673 RID: 1651
		LVCorrupted = -1526,
		// Token: 0x04000674 RID: 1652
		MultiValuedDuplicateAfterTruncation = -1528,
		// Token: 0x04000675 RID: 1653
		DerivedColumnCorruption = -1529,
		// Token: 0x04000676 RID: 1654
		InvalidPlaceholderColumn = -1530,
		// Token: 0x04000677 RID: 1655
		ColumnCannotBeCompressed = -1538,
		// Token: 0x04000678 RID: 1656
		RecordNotFound = -1601,
		// Token: 0x04000679 RID: 1657
		RecordNoCopy = -1602,
		// Token: 0x0400067A RID: 1658
		NoCurrentRecord = -1603,
		// Token: 0x0400067B RID: 1659
		RecordPrimaryChanged = -1604,
		// Token: 0x0400067C RID: 1660
		KeyDuplicate = -1605,
		// Token: 0x0400067D RID: 1661
		AlreadyPrepared = -1607,
		// Token: 0x0400067E RID: 1662
		KeyNotMade = -1608,
		// Token: 0x0400067F RID: 1663
		UpdateNotPrepared = -1609,
		// Token: 0x04000680 RID: 1664
		DataHasChanged = -1611,
		// Token: 0x04000681 RID: 1665
		LanguageNotSupported = -1619,
		// Token: 0x04000682 RID: 1666
		DecompressionFailed = -1620,
		// Token: 0x04000683 RID: 1667
		UpdateMustVersion = -1621,
		// Token: 0x04000684 RID: 1668
		TooManySorts = -1701,
		// Token: 0x04000685 RID: 1669
		InvalidOnSort = -1702,
		// Token: 0x04000686 RID: 1670
		TempFileOpenError = -1803,
		// Token: 0x04000687 RID: 1671
		TooManyAttachedDatabases = -1805,
		// Token: 0x04000688 RID: 1672
		DiskFull = -1808,
		// Token: 0x04000689 RID: 1673
		PermissionDenied = -1809,
		// Token: 0x0400068A RID: 1674
		FileNotFound = -1811,
		// Token: 0x0400068B RID: 1675
		FileInvalidType = -1812,
		// Token: 0x0400068C RID: 1676
		AfterInitialization = -1850,
		// Token: 0x0400068D RID: 1677
		LogCorrupted = -1852,
		// Token: 0x0400068E RID: 1678
		InvalidOperation = -1906,
		// Token: 0x0400068F RID: 1679
		AccessDenied = -1907,
		// Token: 0x04000690 RID: 1680
		TooManySplits = -1909,
		// Token: 0x04000691 RID: 1681
		SessionSharingViolation = -1910,
		// Token: 0x04000692 RID: 1682
		EntryPointNotFound = -1911,
		// Token: 0x04000693 RID: 1683
		SessionContextAlreadySet = -1912,
		// Token: 0x04000694 RID: 1684
		SessionContextNotSetByThisThread = -1913,
		// Token: 0x04000695 RID: 1685
		SessionInUse = -1914,
		// Token: 0x04000696 RID: 1686
		RecordFormatConversionFailed = -1915,
		// Token: 0x04000697 RID: 1687
		OneDatabasePerSession = -1916,
		// Token: 0x04000698 RID: 1688
		RollbackError = -1917,
		// Token: 0x04000699 RID: 1689
		DatabaseAlreadyRunningMaintenance = -2004,
		// Token: 0x0400069A RID: 1690
		CallbackFailed = -2101,
		// Token: 0x0400069B RID: 1691
		CallbackNotResolved = -2102,
		// Token: 0x0400069C RID: 1692
		SpaceHintsInvalid = -2103,
		// Token: 0x0400069D RID: 1693
		OSSnapshotInvalidSequence = -2401,
		// Token: 0x0400069E RID: 1694
		OSSnapshotTimeOut = -2402,
		// Token: 0x0400069F RID: 1695
		OSSnapshotNotAllowed = -2403,
		// Token: 0x040006A0 RID: 1696
		OSSnapshotInvalidSnapId = -2404,
		// Token: 0x040006A1 RID: 1697
		TooManyTestInjections = -2501,
		// Token: 0x040006A2 RID: 1698
		TestInjectionNotSupported = -2502,
		// Token: 0x040006A3 RID: 1699
		InvalidLogDataSequence = -2601,
		// Token: 0x040006A4 RID: 1700
		LSCallbackNotSpecified = -3000,
		// Token: 0x040006A5 RID: 1701
		LSAlreadySet = -3001,
		// Token: 0x040006A6 RID: 1702
		LSNotSet = -3002,
		// Token: 0x040006A7 RID: 1703
		FileIOSparse = -4000,
		// Token: 0x040006A8 RID: 1704
		FileIOBeyondEOF = -4001,
		// Token: 0x040006A9 RID: 1705
		FileIOAbort = -4002,
		// Token: 0x040006AA RID: 1706
		FileIORetry = -4003,
		// Token: 0x040006AB RID: 1707
		FileIOFail = -4004,
		// Token: 0x040006AC RID: 1708
		FileCompressed = -4005
	}
}
