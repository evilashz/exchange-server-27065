using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200012B RID: 299
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public enum ResponseCodeType
	{
		// Token: 0x040004F3 RID: 1267
		NoError,
		// Token: 0x040004F4 RID: 1268
		ErrorAccessDenied,
		// Token: 0x040004F5 RID: 1269
		ErrorAccountDisabled,
		// Token: 0x040004F6 RID: 1270
		ErrorAddDelegatesFailed,
		// Token: 0x040004F7 RID: 1271
		ErrorAddressSpaceNotFound,
		// Token: 0x040004F8 RID: 1272
		ErrorADOperation,
		// Token: 0x040004F9 RID: 1273
		ErrorADSessionFilter,
		// Token: 0x040004FA RID: 1274
		ErrorADUnavailable,
		// Token: 0x040004FB RID: 1275
		ErrorAutoDiscoverFailed,
		// Token: 0x040004FC RID: 1276
		ErrorAffectedTaskOccurrencesRequired,
		// Token: 0x040004FD RID: 1277
		ErrorAttachmentSizeLimitExceeded,
		// Token: 0x040004FE RID: 1278
		ErrorAvailabilityConfigNotFound,
		// Token: 0x040004FF RID: 1279
		ErrorBatchProcessingStopped,
		// Token: 0x04000500 RID: 1280
		ErrorCalendarCannotMoveOrCopyOccurrence,
		// Token: 0x04000501 RID: 1281
		ErrorCalendarCannotUpdateDeletedItem,
		// Token: 0x04000502 RID: 1282
		ErrorCalendarCannotUseIdForOccurrenceId,
		// Token: 0x04000503 RID: 1283
		ErrorCalendarCannotUseIdForRecurringMasterId,
		// Token: 0x04000504 RID: 1284
		ErrorCalendarDurationIsTooLong,
		// Token: 0x04000505 RID: 1285
		ErrorCalendarEndDateIsEarlierThanStartDate,
		// Token: 0x04000506 RID: 1286
		ErrorCalendarFolderIsInvalidForCalendarView,
		// Token: 0x04000507 RID: 1287
		ErrorCalendarInvalidAttributeValue,
		// Token: 0x04000508 RID: 1288
		ErrorCalendarInvalidDayForTimeChangePattern,
		// Token: 0x04000509 RID: 1289
		ErrorCalendarInvalidDayForWeeklyRecurrence,
		// Token: 0x0400050A RID: 1290
		ErrorCalendarInvalidPropertyState,
		// Token: 0x0400050B RID: 1291
		ErrorCalendarInvalidPropertyValue,
		// Token: 0x0400050C RID: 1292
		ErrorCalendarInvalidRecurrence,
		// Token: 0x0400050D RID: 1293
		ErrorCalendarInvalidTimeZone,
		// Token: 0x0400050E RID: 1294
		ErrorCalendarIsCancelledForAccept,
		// Token: 0x0400050F RID: 1295
		ErrorCalendarIsCancelledForDecline,
		// Token: 0x04000510 RID: 1296
		ErrorCalendarIsCancelledForRemove,
		// Token: 0x04000511 RID: 1297
		ErrorCalendarIsCancelledForTentative,
		// Token: 0x04000512 RID: 1298
		ErrorCalendarIsDelegatedForAccept,
		// Token: 0x04000513 RID: 1299
		ErrorCalendarIsDelegatedForDecline,
		// Token: 0x04000514 RID: 1300
		ErrorCalendarIsDelegatedForRemove,
		// Token: 0x04000515 RID: 1301
		ErrorCalendarIsDelegatedForTentative,
		// Token: 0x04000516 RID: 1302
		ErrorCalendarIsNotOrganizer,
		// Token: 0x04000517 RID: 1303
		ErrorCalendarIsOrganizerForAccept,
		// Token: 0x04000518 RID: 1304
		ErrorCalendarIsOrganizerForDecline,
		// Token: 0x04000519 RID: 1305
		ErrorCalendarIsOrganizerForRemove,
		// Token: 0x0400051A RID: 1306
		ErrorCalendarIsOrganizerForTentative,
		// Token: 0x0400051B RID: 1307
		ErrorCalendarOccurrenceIndexIsOutOfRecurrenceRange,
		// Token: 0x0400051C RID: 1308
		ErrorCalendarOccurrenceIsDeletedFromRecurrence,
		// Token: 0x0400051D RID: 1309
		ErrorCalendarOutOfRange,
		// Token: 0x0400051E RID: 1310
		ErrorCalendarMeetingRequestIsOutOfDate,
		// Token: 0x0400051F RID: 1311
		ErrorCalendarViewRangeTooBig,
		// Token: 0x04000520 RID: 1312
		ErrorCannotCreateCalendarItemInNonCalendarFolder,
		// Token: 0x04000521 RID: 1313
		ErrorCannotCreateContactInNonContactFolder,
		// Token: 0x04000522 RID: 1314
		ErrorCannotCreatePostItemInNonMailFolder,
		// Token: 0x04000523 RID: 1315
		ErrorCannotCreateTaskInNonTaskFolder,
		// Token: 0x04000524 RID: 1316
		ErrorCannotDeleteObject,
		// Token: 0x04000525 RID: 1317
		ErrorCannotOpenFileAttachment,
		// Token: 0x04000526 RID: 1318
		ErrorCannotDeleteTaskOccurrence,
		// Token: 0x04000527 RID: 1319
		ErrorCannotSetCalendarPermissionOnNonCalendarFolder,
		// Token: 0x04000528 RID: 1320
		ErrorCannotSetNonCalendarPermissionOnCalendarFolder,
		// Token: 0x04000529 RID: 1321
		ErrorCannotSetPermissionUnknownEntries,
		// Token: 0x0400052A RID: 1322
		ErrorCannotUseFolderIdForItemId,
		// Token: 0x0400052B RID: 1323
		ErrorCannotUseItemIdForFolderId,
		// Token: 0x0400052C RID: 1324
		ErrorChangeKeyRequired,
		// Token: 0x0400052D RID: 1325
		ErrorChangeKeyRequiredForWriteOperations,
		// Token: 0x0400052E RID: 1326
		ErrorConnectionFailed,
		// Token: 0x0400052F RID: 1327
		ErrorContainsFilterWrongType,
		// Token: 0x04000530 RID: 1328
		ErrorContentConversionFailed,
		// Token: 0x04000531 RID: 1329
		ErrorCorruptData,
		// Token: 0x04000532 RID: 1330
		ErrorCreateItemAccessDenied,
		// Token: 0x04000533 RID: 1331
		ErrorCreateManagedFolderPartialCompletion,
		// Token: 0x04000534 RID: 1332
		ErrorCreateSubfolderAccessDenied,
		// Token: 0x04000535 RID: 1333
		ErrorCrossMailboxMoveCopy,
		// Token: 0x04000536 RID: 1334
		ErrorDataSizeLimitExceeded,
		// Token: 0x04000537 RID: 1335
		ErrorDataSourceOperation,
		// Token: 0x04000538 RID: 1336
		ErrorDelegateAlreadyExists,
		// Token: 0x04000539 RID: 1337
		ErrorDelegateCannotAddOwner,
		// Token: 0x0400053A RID: 1338
		ErrorDelegateMissingConfiguration,
		// Token: 0x0400053B RID: 1339
		ErrorDelegateNoUser,
		// Token: 0x0400053C RID: 1340
		ErrorDelegateValidationFailed,
		// Token: 0x0400053D RID: 1341
		ErrorDeleteDistinguishedFolder,
		// Token: 0x0400053E RID: 1342
		ErrorDeleteItemsFailed,
		// Token: 0x0400053F RID: 1343
		ErrorDistinguishedUserNotSupported,
		// Token: 0x04000540 RID: 1344
		ErrorDistributionListMemberNotExist,
		// Token: 0x04000541 RID: 1345
		ErrorDuplicateInputFolderNames,
		// Token: 0x04000542 RID: 1346
		ErrorDuplicateUserIdsSpecified,
		// Token: 0x04000543 RID: 1347
		ErrorEmailAddressMismatch,
		// Token: 0x04000544 RID: 1348
		ErrorEventNotFound,
		// Token: 0x04000545 RID: 1349
		ErrorExpiredSubscription,
		// Token: 0x04000546 RID: 1350
		ErrorExternalFacingCAS,
		// Token: 0x04000547 RID: 1351
		ErrorFolderCorrupt,
		// Token: 0x04000548 RID: 1352
		ErrorFolderNotFound,
		// Token: 0x04000549 RID: 1353
		ErrorFolderPropertRequestFailed,
		// Token: 0x0400054A RID: 1354
		ErrorFolderSave,
		// Token: 0x0400054B RID: 1355
		ErrorFolderSaveFailed,
		// Token: 0x0400054C RID: 1356
		ErrorFolderSavePropertyError,
		// Token: 0x0400054D RID: 1357
		ErrorFolderExists,
		// Token: 0x0400054E RID: 1358
		ErrorFreeBusyGenerationFailed,
		// Token: 0x0400054F RID: 1359
		ErrorGetServerSecurityDescriptorFailed,
		// Token: 0x04000550 RID: 1360
		ErrorImpersonateUserDenied,
		// Token: 0x04000551 RID: 1361
		ErrorImpersonationDenied,
		// Token: 0x04000552 RID: 1362
		ErrorImpersonationFailed,
		// Token: 0x04000553 RID: 1363
		ErrorIncorrectSchemaVersion,
		// Token: 0x04000554 RID: 1364
		ErrorIncorrectUpdatePropertyCount,
		// Token: 0x04000555 RID: 1365
		ErrorIndividualMailboxLimitReached,
		// Token: 0x04000556 RID: 1366
		ErrorInsufficientResources,
		// Token: 0x04000557 RID: 1367
		ErrorInternalServerError,
		// Token: 0x04000558 RID: 1368
		ErrorInternalServerTransientError,
		// Token: 0x04000559 RID: 1369
		ErrorInvalidAccessLevel,
		// Token: 0x0400055A RID: 1370
		ErrorInvalidArgument,
		// Token: 0x0400055B RID: 1371
		ErrorInvalidAttachmentId,
		// Token: 0x0400055C RID: 1372
		ErrorInvalidAttachmentSubfilter,
		// Token: 0x0400055D RID: 1373
		ErrorInvalidAttachmentSubfilterTextFilter,
		// Token: 0x0400055E RID: 1374
		ErrorInvalidAuthorizationContext,
		// Token: 0x0400055F RID: 1375
		ErrorInvalidChangeKey,
		// Token: 0x04000560 RID: 1376
		ErrorInvalidClientSecurityContext,
		// Token: 0x04000561 RID: 1377
		ErrorInvalidCompleteDate,
		// Token: 0x04000562 RID: 1378
		ErrorInvalidCrossForestCredentials,
		// Token: 0x04000563 RID: 1379
		ErrorInvalidDelegatePermission,
		// Token: 0x04000564 RID: 1380
		ErrorInvalidDelegateUserId,
		// Token: 0x04000565 RID: 1381
		ErrorInvalidExcludesRestriction,
		// Token: 0x04000566 RID: 1382
		ErrorInvalidExpressionTypeForSubFilter,
		// Token: 0x04000567 RID: 1383
		ErrorInvalidExtendedProperty,
		// Token: 0x04000568 RID: 1384
		ErrorInvalidExtendedPropertyValue,
		// Token: 0x04000569 RID: 1385
		ErrorInvalidFolderId,
		// Token: 0x0400056A RID: 1386
		ErrorInvalidFolderTypeForOperation,
		// Token: 0x0400056B RID: 1387
		ErrorInvalidFractionalPagingParameters,
		// Token: 0x0400056C RID: 1388
		ErrorInvalidFreeBusyViewType,
		// Token: 0x0400056D RID: 1389
		ErrorInvalidId,
		// Token: 0x0400056E RID: 1390
		ErrorInvalidIdEmpty,
		// Token: 0x0400056F RID: 1391
		ErrorInvalidIdMalformed,
		// Token: 0x04000570 RID: 1392
		ErrorInvalidIdMalformedEwsLegacyIdFormat,
		// Token: 0x04000571 RID: 1393
		ErrorInvalidIdMonikerTooLong,
		// Token: 0x04000572 RID: 1394
		ErrorInvalidIdNotAnItemAttachmentId,
		// Token: 0x04000573 RID: 1395
		ErrorInvalidIdReturnedByResolveNames,
		// Token: 0x04000574 RID: 1396
		ErrorInvalidIdStoreObjectIdTooLong,
		// Token: 0x04000575 RID: 1397
		ErrorInvalidIdTooManyAttachmentLevels,
		// Token: 0x04000576 RID: 1398
		ErrorInvalidIdXml,
		// Token: 0x04000577 RID: 1399
		ErrorInvalidIndexedPagingParameters,
		// Token: 0x04000578 RID: 1400
		ErrorInvalidInternetHeaderChildNodes,
		// Token: 0x04000579 RID: 1401
		ErrorInvalidItemForOperationCreateItemAttachment,
		// Token: 0x0400057A RID: 1402
		ErrorInvalidItemForOperationCreateItem,
		// Token: 0x0400057B RID: 1403
		ErrorInvalidItemForOperationAcceptItem,
		// Token: 0x0400057C RID: 1404
		ErrorInvalidItemForOperationDeclineItem,
		// Token: 0x0400057D RID: 1405
		ErrorInvalidItemForOperationCancelItem,
		// Token: 0x0400057E RID: 1406
		ErrorInvalidItemForOperationExpandDL,
		// Token: 0x0400057F RID: 1407
		ErrorInvalidItemForOperationRemoveItem,
		// Token: 0x04000580 RID: 1408
		ErrorInvalidItemForOperationSendItem,
		// Token: 0x04000581 RID: 1409
		ErrorInvalidItemForOperationTentative,
		// Token: 0x04000582 RID: 1410
		ErrorInvalidMailbox,
		// Token: 0x04000583 RID: 1411
		ErrorInvalidManagedFolderProperty,
		// Token: 0x04000584 RID: 1412
		ErrorInvalidManagedFolderQuota,
		// Token: 0x04000585 RID: 1413
		ErrorInvalidManagedFolderSize,
		// Token: 0x04000586 RID: 1414
		ErrorInvalidMergedFreeBusyInterval,
		// Token: 0x04000587 RID: 1415
		ErrorInvalidNameForNameResolution,
		// Token: 0x04000588 RID: 1416
		ErrorInvalidOperation,
		// Token: 0x04000589 RID: 1417
		ErrorInvalidNetworkServiceContext,
		// Token: 0x0400058A RID: 1418
		ErrorInvalidOofParameter,
		// Token: 0x0400058B RID: 1419
		ErrorInvalidPagingMaxRows,
		// Token: 0x0400058C RID: 1420
		ErrorInvalidParentFolder,
		// Token: 0x0400058D RID: 1421
		ErrorInvalidPercentCompleteValue,
		// Token: 0x0400058E RID: 1422
		ErrorInvalidPermissionSettings,
		// Token: 0x0400058F RID: 1423
		ErrorInvalidPhoneCallId,
		// Token: 0x04000590 RID: 1424
		ErrorInvalidPhoneNumber,
		// Token: 0x04000591 RID: 1425
		ErrorInvalidUserInfo,
		// Token: 0x04000592 RID: 1426
		ErrorInvalidPropertyAppend,
		// Token: 0x04000593 RID: 1427
		ErrorInvalidPropertyDelete,
		// Token: 0x04000594 RID: 1428
		ErrorInvalidPropertyForExists,
		// Token: 0x04000595 RID: 1429
		ErrorInvalidPropertyForOperation,
		// Token: 0x04000596 RID: 1430
		ErrorInvalidPropertyRequest,
		// Token: 0x04000597 RID: 1431
		ErrorInvalidPropertySet,
		// Token: 0x04000598 RID: 1432
		ErrorInvalidPropertyUpdateSentMessage,
		// Token: 0x04000599 RID: 1433
		ErrorInvalidProxySecurityContext,
		// Token: 0x0400059A RID: 1434
		ErrorInvalidPullSubscriptionId,
		// Token: 0x0400059B RID: 1435
		ErrorInvalidPushSubscriptionUrl,
		// Token: 0x0400059C RID: 1436
		ErrorInvalidRecipients,
		// Token: 0x0400059D RID: 1437
		ErrorInvalidRecipientSubfilter,
		// Token: 0x0400059E RID: 1438
		ErrorInvalidRecipientSubfilterComparison,
		// Token: 0x0400059F RID: 1439
		ErrorInvalidRecipientSubfilterOrder,
		// Token: 0x040005A0 RID: 1440
		ErrorInvalidRecipientSubfilterTextFilter,
		// Token: 0x040005A1 RID: 1441
		ErrorInvalidReferenceItem,
		// Token: 0x040005A2 RID: 1442
		ErrorInvalidRequest,
		// Token: 0x040005A3 RID: 1443
		ErrorInvalidRestriction,
		// Token: 0x040005A4 RID: 1444
		ErrorInvalidRoutingType,
		// Token: 0x040005A5 RID: 1445
		ErrorInvalidScheduledOofDuration,
		// Token: 0x040005A6 RID: 1446
		ErrorInvalidSchemaVersionForMailboxVersion,
		// Token: 0x040005A7 RID: 1447
		ErrorInvalidSecurityDescriptor,
		// Token: 0x040005A8 RID: 1448
		ErrorInvalidSendItemSaveSettings,
		// Token: 0x040005A9 RID: 1449
		ErrorInvalidSerializedAccessToken,
		// Token: 0x040005AA RID: 1450
		ErrorInvalidServerVersion,
		// Token: 0x040005AB RID: 1451
		ErrorInvalidSid,
		// Token: 0x040005AC RID: 1452
		ErrorInvalidSIPUri,
		// Token: 0x040005AD RID: 1453
		ErrorInvalidSmtpAddress,
		// Token: 0x040005AE RID: 1454
		ErrorInvalidSubfilterType,
		// Token: 0x040005AF RID: 1455
		ErrorInvalidSubfilterTypeNotAttendeeType,
		// Token: 0x040005B0 RID: 1456
		ErrorInvalidSubfilterTypeNotRecipientType,
		// Token: 0x040005B1 RID: 1457
		ErrorInvalidSubscription,
		// Token: 0x040005B2 RID: 1458
		ErrorInvalidSubscriptionRequest,
		// Token: 0x040005B3 RID: 1459
		ErrorInvalidSyncStateData,
		// Token: 0x040005B4 RID: 1460
		ErrorInvalidTimeInterval,
		// Token: 0x040005B5 RID: 1461
		ErrorInvalidUserOofSettings,
		// Token: 0x040005B6 RID: 1462
		ErrorInvalidUserPrincipalName,
		// Token: 0x040005B7 RID: 1463
		ErrorInvalidUserSid,
		// Token: 0x040005B8 RID: 1464
		ErrorInvalidUserSidMissingUPN,
		// Token: 0x040005B9 RID: 1465
		ErrorInvalidValueForProperty,
		// Token: 0x040005BA RID: 1466
		ErrorInvalidWatermark,
		// Token: 0x040005BB RID: 1467
		ErrorIPGatewayNotFound,
		// Token: 0x040005BC RID: 1468
		ErrorIrresolvableConflict,
		// Token: 0x040005BD RID: 1469
		ErrorItemCorrupt,
		// Token: 0x040005BE RID: 1470
		ErrorItemNotFound,
		// Token: 0x040005BF RID: 1471
		ErrorItemPropertyRequestFailed,
		// Token: 0x040005C0 RID: 1472
		ErrorItemSave,
		// Token: 0x040005C1 RID: 1473
		ErrorItemSavePropertyError,
		// Token: 0x040005C2 RID: 1474
		ErrorLegacyMailboxFreeBusyViewTypeNotMerged,
		// Token: 0x040005C3 RID: 1475
		ErrorLocalServerObjectNotFound,
		// Token: 0x040005C4 RID: 1476
		ErrorLogonAsNetworkServiceFailed,
		// Token: 0x040005C5 RID: 1477
		ErrorMailboxConfiguration,
		// Token: 0x040005C6 RID: 1478
		ErrorMailboxDataArrayEmpty,
		// Token: 0x040005C7 RID: 1479
		ErrorMailboxDataArrayTooBig,
		// Token: 0x040005C8 RID: 1480
		ErrorMailboxLogonFailed,
		// Token: 0x040005C9 RID: 1481
		ErrorMailboxMoveInProgress,
		// Token: 0x040005CA RID: 1482
		ErrorMailboxStoreUnavailable,
		// Token: 0x040005CB RID: 1483
		ErrorMailRecipientNotFound,
		// Token: 0x040005CC RID: 1484
		ErrorMailTipsDisabled,
		// Token: 0x040005CD RID: 1485
		ErrorManagedFolderAlreadyExists,
		// Token: 0x040005CE RID: 1486
		ErrorManagedFolderNotFound,
		// Token: 0x040005CF RID: 1487
		ErrorManagedFoldersRootFailure,
		// Token: 0x040005D0 RID: 1488
		ErrorMeetingSuggestionGenerationFailed,
		// Token: 0x040005D1 RID: 1489
		ErrorMessageDispositionRequired,
		// Token: 0x040005D2 RID: 1490
		ErrorMessageSizeExceeded,
		// Token: 0x040005D3 RID: 1491
		ErrorMimeContentConversionFailed,
		// Token: 0x040005D4 RID: 1492
		ErrorMimeContentInvalid,
		// Token: 0x040005D5 RID: 1493
		ErrorMimeContentInvalidBase64String,
		// Token: 0x040005D6 RID: 1494
		ErrorMissingArgument,
		// Token: 0x040005D7 RID: 1495
		ErrorMissingEmailAddress,
		// Token: 0x040005D8 RID: 1496
		ErrorMissingEmailAddressForManagedFolder,
		// Token: 0x040005D9 RID: 1497
		ErrorMissingInformationEmailAddress,
		// Token: 0x040005DA RID: 1498
		ErrorMissingInformationReferenceItemId,
		// Token: 0x040005DB RID: 1499
		ErrorMissingItemForCreateItemAttachment,
		// Token: 0x040005DC RID: 1500
		ErrorMissingManagedFolderId,
		// Token: 0x040005DD RID: 1501
		ErrorMissingRecipients,
		// Token: 0x040005DE RID: 1502
		ErrorMissingUserIdInformation,
		// Token: 0x040005DF RID: 1503
		ErrorMoreThanOneAccessModeSpecified,
		// Token: 0x040005E0 RID: 1504
		ErrorMoveCopyFailed,
		// Token: 0x040005E1 RID: 1505
		ErrorMoveDistinguishedFolder,
		// Token: 0x040005E2 RID: 1506
		ErrorNameResolutionMultipleResults,
		// Token: 0x040005E3 RID: 1507
		ErrorNameResolutionNoMailbox,
		// Token: 0x040005E4 RID: 1508
		ErrorNameResolutionNoResults,
		// Token: 0x040005E5 RID: 1509
		ErrorNoCalendar,
		// Token: 0x040005E6 RID: 1510
		ErrorNoDestinationCASDueToKerberosRequirements,
		// Token: 0x040005E7 RID: 1511
		ErrorNoDestinationCASDueToSSLRequirements,
		// Token: 0x040005E8 RID: 1512
		ErrorNoDestinationCASDueToVersionMismatch,
		// Token: 0x040005E9 RID: 1513
		ErrorNoFolderClassOverride,
		// Token: 0x040005EA RID: 1514
		ErrorNoFreeBusyAccess,
		// Token: 0x040005EB RID: 1515
		ErrorNonExistentMailbox,
		// Token: 0x040005EC RID: 1516
		ErrorNonPrimarySmtpAddress,
		// Token: 0x040005ED RID: 1517
		ErrorNoPropertyTagForCustomProperties,
		// Token: 0x040005EE RID: 1518
		ErrorNoPublicFolderReplicaAvailable,
		// Token: 0x040005EF RID: 1519
		ErrorNoRespondingCASInDestinationSite,
		// Token: 0x040005F0 RID: 1520
		ErrorNotDelegate,
		// Token: 0x040005F1 RID: 1521
		ErrorNotEnoughMemory,
		// Token: 0x040005F2 RID: 1522
		ErrorObjectTypeChanged,
		// Token: 0x040005F3 RID: 1523
		ErrorOccurrenceCrossingBoundary,
		// Token: 0x040005F4 RID: 1524
		ErrorOccurrenceTimeSpanTooBig,
		// Token: 0x040005F5 RID: 1525
		ErrorOperationNotAllowedWithPublicFolderRoot,
		// Token: 0x040005F6 RID: 1526
		ErrorParentFolderIdRequired,
		// Token: 0x040005F7 RID: 1527
		ErrorParentFolderNotFound,
		// Token: 0x040005F8 RID: 1528
		ErrorPasswordChangeRequired,
		// Token: 0x040005F9 RID: 1529
		ErrorPasswordExpired,
		// Token: 0x040005FA RID: 1530
		ErrorPhoneNumberNotDialable,
		// Token: 0x040005FB RID: 1531
		ErrorPropertyUpdate,
		// Token: 0x040005FC RID: 1532
		ErrorPropertyValidationFailure,
		// Token: 0x040005FD RID: 1533
		ErrorProxiedSubscriptionCallFailure,
		// Token: 0x040005FE RID: 1534
		ErrorProxyCallFailed,
		// Token: 0x040005FF RID: 1535
		ErrorProxyGroupSidLimitExceeded,
		// Token: 0x04000600 RID: 1536
		ErrorProxyRequestNotAllowed,
		// Token: 0x04000601 RID: 1537
		ErrorProxyRequestProcessingFailed,
		// Token: 0x04000602 RID: 1538
		ErrorProxyTokenExpired,
		// Token: 0x04000603 RID: 1539
		ErrorPublicFolderRequestProcessingFailed,
		// Token: 0x04000604 RID: 1540
		ErrorPublicFolderServerNotFound,
		// Token: 0x04000605 RID: 1541
		ErrorQueryFilterTooLong,
		// Token: 0x04000606 RID: 1542
		ErrorQuotaExceeded,
		// Token: 0x04000607 RID: 1543
		ErrorReadEventsFailed,
		// Token: 0x04000608 RID: 1544
		ErrorReadReceiptNotPending,
		// Token: 0x04000609 RID: 1545
		ErrorRecurrenceEndDateTooBig,
		// Token: 0x0400060A RID: 1546
		ErrorRecurrenceHasNoOccurrence,
		// Token: 0x0400060B RID: 1547
		ErrorRemoveDelegatesFailed,
		// Token: 0x0400060C RID: 1548
		ErrorRequestAborted,
		// Token: 0x0400060D RID: 1549
		ErrorRequestStreamTooBig,
		// Token: 0x0400060E RID: 1550
		ErrorRequiredPropertyMissing,
		// Token: 0x0400060F RID: 1551
		ErrorResolveNamesInvalidFolderType,
		// Token: 0x04000610 RID: 1552
		ErrorResolveNamesOnlyOneContactsFolderAllowed,
		// Token: 0x04000611 RID: 1553
		ErrorResponseSchemaValidation,
		// Token: 0x04000612 RID: 1554
		ErrorRestrictionTooLong,
		// Token: 0x04000613 RID: 1555
		ErrorRestrictionTooComplex,
		// Token: 0x04000614 RID: 1556
		ErrorResultSetTooBig,
		// Token: 0x04000615 RID: 1557
		ErrorInvalidExchangeImpersonationHeaderData,
		// Token: 0x04000616 RID: 1558
		ErrorSavedItemFolderNotFound,
		// Token: 0x04000617 RID: 1559
		ErrorSchemaValidation,
		// Token: 0x04000618 RID: 1560
		ErrorSearchFolderNotInitialized,
		// Token: 0x04000619 RID: 1561
		ErrorSendAsDenied,
		// Token: 0x0400061A RID: 1562
		ErrorSendMeetingCancellationsRequired,
		// Token: 0x0400061B RID: 1563
		ErrorSendMeetingInvitationsOrCancellationsRequired,
		// Token: 0x0400061C RID: 1564
		ErrorSendMeetingInvitationsRequired,
		// Token: 0x0400061D RID: 1565
		ErrorSentMeetingRequestUpdate,
		// Token: 0x0400061E RID: 1566
		ErrorSentTaskRequestUpdate,
		// Token: 0x0400061F RID: 1567
		ErrorServerBusy,
		// Token: 0x04000620 RID: 1568
		ErrorServiceDiscoveryFailed,
		// Token: 0x04000621 RID: 1569
		ErrorStaleObject,
		// Token: 0x04000622 RID: 1570
		ErrorSubmissionQuotaExceeded,
		// Token: 0x04000623 RID: 1571
		ErrorSubscriptionAccessDenied,
		// Token: 0x04000624 RID: 1572
		ErrorSubscriptionDelegateAccessNotSupported,
		// Token: 0x04000625 RID: 1573
		ErrorSubscriptionNotFound,
		// Token: 0x04000626 RID: 1574
		ErrorSyncFolderNotFound,
		// Token: 0x04000627 RID: 1575
		ErrorTimeIntervalTooBig,
		// Token: 0x04000628 RID: 1576
		ErrorTimeoutExpired,
		// Token: 0x04000629 RID: 1577
		ErrorTimeZone,
		// Token: 0x0400062A RID: 1578
		ErrorToFolderNotFound,
		// Token: 0x0400062B RID: 1579
		ErrorTokenSerializationDenied,
		// Token: 0x0400062C RID: 1580
		ErrorUpdatePropertyMismatch,
		// Token: 0x0400062D RID: 1581
		ErrorUnifiedMessagingDialPlanNotFound,
		// Token: 0x0400062E RID: 1582
		ErrorUnifiedMessagingRequestFailed,
		// Token: 0x0400062F RID: 1583
		ErrorUnifiedMessagingServerNotFound,
		// Token: 0x04000630 RID: 1584
		ErrorUnableToGetUserOofSettings,
		// Token: 0x04000631 RID: 1585
		ErrorUnsupportedSubFilter,
		// Token: 0x04000632 RID: 1586
		ErrorUnsupportedCulture,
		// Token: 0x04000633 RID: 1587
		ErrorUnsupportedMapiPropertyType,
		// Token: 0x04000634 RID: 1588
		ErrorUnsupportedMimeConversion,
		// Token: 0x04000635 RID: 1589
		ErrorUnsupportedPathForQuery,
		// Token: 0x04000636 RID: 1590
		ErrorUnsupportedPathForSortGroup,
		// Token: 0x04000637 RID: 1591
		ErrorUnsupportedPropertyDefinition,
		// Token: 0x04000638 RID: 1592
		ErrorUnsupportedQueryFilter,
		// Token: 0x04000639 RID: 1593
		ErrorUnsupportedRecurrence,
		// Token: 0x0400063A RID: 1594
		ErrorUnsupportedTypeForConversion,
		// Token: 0x0400063B RID: 1595
		ErrorUpdateDelegatesFailed,
		// Token: 0x0400063C RID: 1596
		ErrorUserNotUnifiedMessagingEnabled,
		// Token: 0x0400063D RID: 1597
		ErrorVoiceMailNotImplemented,
		// Token: 0x0400063E RID: 1598
		ErrorVirusDetected,
		// Token: 0x0400063F RID: 1599
		ErrorVirusMessageDeleted,
		// Token: 0x04000640 RID: 1600
		ErrorWebRequestInInvalidState,
		// Token: 0x04000641 RID: 1601
		ErrorWin32InteropError,
		// Token: 0x04000642 RID: 1602
		ErrorWorkingHoursSaveFailed,
		// Token: 0x04000643 RID: 1603
		ErrorWorkingHoursXmlMalformed,
		// Token: 0x04000644 RID: 1604
		ErrorWrongServerVersion,
		// Token: 0x04000645 RID: 1605
		ErrorWrongServerVersionDelegate,
		// Token: 0x04000646 RID: 1606
		ErrorMissingInformationSharingFolderId,
		// Token: 0x04000647 RID: 1607
		ErrorDuplicateSOAPHeader,
		// Token: 0x04000648 RID: 1608
		ErrorSharingSynchronizationFailed,
		// Token: 0x04000649 RID: 1609
		ErrorSharingNoExternalEwsAvailable,
		// Token: 0x0400064A RID: 1610
		ErrorFreeBusyDLLimitReached,
		// Token: 0x0400064B RID: 1611
		ErrorInvalidGetSharingFolderRequest,
		// Token: 0x0400064C RID: 1612
		ErrorNotAllowedExternalSharingByPolicy,
		// Token: 0x0400064D RID: 1613
		ErrorUserNotAllowedByPolicy,
		// Token: 0x0400064E RID: 1614
		ErrorPermissionNotAllowedByPolicy,
		// Token: 0x0400064F RID: 1615
		ErrorOrganizationNotFederated,
		// Token: 0x04000650 RID: 1616
		ErrorMailboxFailover,
		// Token: 0x04000651 RID: 1617
		ErrorInvalidExternalSharingInitiator,
		// Token: 0x04000652 RID: 1618
		ErrorMessageTrackingPermanentError,
		// Token: 0x04000653 RID: 1619
		ErrorMessageTrackingTransientError,
		// Token: 0x04000654 RID: 1620
		ErrorMessageTrackingNoSuchDomain
	}
}
