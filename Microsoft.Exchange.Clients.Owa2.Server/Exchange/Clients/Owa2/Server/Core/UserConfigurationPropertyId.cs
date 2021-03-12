using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000F8 RID: 248
	public enum UserConfigurationPropertyId
	{
		// Token: 0x0400056C RID: 1388
		TimeZone,
		// Token: 0x0400056D RID: 1389
		TimeFormat,
		// Token: 0x0400056E RID: 1390
		DateFormat,
		// Token: 0x0400056F RID: 1391
		WeekStartDay,
		// Token: 0x04000570 RID: 1392
		HourIncrement,
		// Token: 0x04000571 RID: 1393
		ShowWeekNumbers,
		// Token: 0x04000572 RID: 1394
		CheckNameInContactsFirst,
		// Token: 0x04000573 RID: 1395
		FirstWeekOfYear,
		// Token: 0x04000574 RID: 1396
		EnableReminders,
		// Token: 0x04000575 RID: 1397
		EnableReminderSound,
		// Token: 0x04000576 RID: 1398
		NewItemNotify,
		// Token: 0x04000577 RID: 1399
		ViewRowCount,
		// Token: 0x04000578 RID: 1400
		BasicViewRowCount,
		// Token: 0x04000579 RID: 1401
		SpellingDictionaryLanguage,
		// Token: 0x0400057A RID: 1402
		SpellingIgnoreUppercase,
		// Token: 0x0400057B RID: 1403
		SpellingIgnoreMixedDigits,
		// Token: 0x0400057C RID: 1404
		SpellingCheckBeforeSend,
		// Token: 0x0400057D RID: 1405
		SmimeEncrypt,
		// Token: 0x0400057E RID: 1406
		SmimeSign,
		// Token: 0x0400057F RID: 1407
		AlwaysShowBcc,
		// Token: 0x04000580 RID: 1408
		AlwaysShowFrom,
		// Token: 0x04000581 RID: 1409
		ComposeMarkup,
		// Token: 0x04000582 RID: 1410
		ComposeFontName,
		// Token: 0x04000583 RID: 1411
		ComposeFontSize,
		// Token: 0x04000584 RID: 1412
		ComposeFontColor,
		// Token: 0x04000585 RID: 1413
		ComposeFontFlags,
		// Token: 0x04000586 RID: 1414
		AutoAddSignature,
		// Token: 0x04000587 RID: 1415
		SignatureText,
		// Token: 0x04000588 RID: 1416
		SignatureHtml,
		// Token: 0x04000589 RID: 1417
		AutoAddSignatureOnMobile,
		// Token: 0x0400058A RID: 1418
		SignatureTextOnMobile,
		// Token: 0x0400058B RID: 1419
		UseDesktopSignature,
		// Token: 0x0400058C RID: 1420
		BlockExternalContent,
		// Token: 0x0400058D RID: 1421
		PreviewMarkAsRead,
		// Token: 0x0400058E RID: 1422
		MarkAsReadDelaytime,
		// Token: 0x0400058F RID: 1423
		NextSelection,
		// Token: 0x04000590 RID: 1424
		ReadReceipt,
		// Token: 0x04000591 RID: 1425
		EmptyDeletedItemsOnLogoff,
		// Token: 0x04000592 RID: 1426
		NavigationBarWidth,
		// Token: 0x04000593 RID: 1427
		NavigationBarWidthRatio,
		// Token: 0x04000594 RID: 1428
		MailFolderPaneExpanded,
		// Token: 0x04000595 RID: 1429
		IsFavoritesFolderTreeCollapsed,
		// Token: 0x04000596 RID: 1430
		IsMailRootFolderTreeCollapsed,
		// Token: 0x04000597 RID: 1431
		IsPeopleIKnowFolderTreeCollapsed,
		// Token: 0x04000598 RID: 1432
		ShowReadingPaneOnFirstLoad,
		// Token: 0x04000599 RID: 1433
		IsMiniBarVisible,
		// Token: 0x0400059A RID: 1434
		IsQuickLinksBarVisible,
		// Token: 0x0400059B RID: 1435
		IsTaskDetailsVisible,
		// Token: 0x0400059C RID: 1436
		IsDocumentFavoritesVisible,
		// Token: 0x0400059D RID: 1437
		IsOutlookSharedFoldersVisible,
		// Token: 0x0400059E RID: 1438
		FormatBarState,
		// Token: 0x0400059F RID: 1439
		MruFonts,
		// Token: 0x040005A0 RID: 1440
		PrimaryNavigationCollapsed,
		// Token: 0x040005A1 RID: 1441
		ThemeStorageId,
		// Token: 0x040005A2 RID: 1442
		MailFindBarOn,
		// Token: 0x040005A3 RID: 1443
		CalendarFindBarOn,
		// Token: 0x040005A4 RID: 1444
		ContactsFindBarOn,
		// Token: 0x040005A5 RID: 1445
		SearchScope,
		// Token: 0x040005A6 RID: 1446
		ContactsSearchScope,
		// Token: 0x040005A7 RID: 1447
		TasksSearchScope,
		// Token: 0x040005A8 RID: 1448
		IsOptimizedForAccessibility,
		// Token: 0x040005A9 RID: 1449
		NewEnabledPonts,
		// Token: 0x040005AA RID: 1450
		FlagAction,
		// Token: 0x040005AB RID: 1451
		AddRecipientsToAutoCompleteCache,
		// Token: 0x040005AC RID: 1452
		ManuallyPickCertificate,
		// Token: 0x040005AD RID: 1453
		SigningCertificateSubject,
		// Token: 0x040005AE RID: 1454
		SigningCertificateId,
		// Token: 0x040005AF RID: 1455
		UseDataCenterCustomTheme,
		// Token: 0x040005B0 RID: 1456
		ConversationSortOrder,
		// Token: 0x040005B1 RID: 1457
		ShowTreeInListView,
		// Token: 0x040005B2 RID: 1458
		HideDeletedItems,
		// Token: 0x040005B3 RID: 1459
		HideMailTipsByDefault,
		// Token: 0x040005B4 RID: 1460
		SendAddressDefault,
		// Token: 0x040005B5 RID: 1461
		EmailComposeMode,
		// Token: 0x040005B6 RID: 1462
		SendAsMruAddresses,
		// Token: 0x040005B7 RID: 1463
		CheckForForgottenAttachments,
		// Token: 0x040005B8 RID: 1464
		ShowInferenceUiElements,
		// Token: 0x040005B9 RID: 1465
		HasShownClutterBarIntroductionMouse,
		// Token: 0x040005BA RID: 1466
		HasShownClutterDeleteAllIntroductionMouse,
		// Token: 0x040005BB RID: 1467
		HasShownClutterBarIntroductionTNarrow,
		// Token: 0x040005BC RID: 1468
		HasShownClutterDeleteAllIntroductionTNarrow,
		// Token: 0x040005BD RID: 1469
		HasShownClutterBarIntroductionTWide,
		// Token: 0x040005BE RID: 1470
		HasShownClutterDeleteAllIntroductionTWide,
		// Token: 0x040005BF RID: 1471
		ShowSenderOnTopInListView,
		// Token: 0x040005C0 RID: 1472
		ShowPreviewTextInListView,
		// Token: 0x040005C1 RID: 1473
		GlobalReadingPanePosition,
		// Token: 0x040005C2 RID: 1474
		UserOptionsMigrationState,
		// Token: 0x040005C3 RID: 1475
		IsInferenceSurveyComplete,
		// Token: 0x040005C4 RID: 1476
		InferenceSurveyDate,
		// Token: 0x040005C5 RID: 1477
		PeopleIKnowFirstUseDate,
		// Token: 0x040005C6 RID: 1478
		PeopleIKnowLastUseDate,
		// Token: 0x040005C7 RID: 1479
		PeopleIKnowUse,
		// Token: 0x040005C8 RID: 1480
		ActiveSurvey,
		// Token: 0x040005C9 RID: 1481
		CompletedSurveys,
		// Token: 0x040005CA RID: 1482
		DismissedSurveys,
		// Token: 0x040005CB RID: 1483
		LastSurveyDate,
		// Token: 0x040005CC RID: 1484
		DontShowSurveys,
		// Token: 0x040005CD RID: 1485
		ReportJunkSelected,
		// Token: 0x040005CE RID: 1486
		CheckForReportJunkDialog,
		// Token: 0x040005CF RID: 1487
		HasShownIntroductionForPeopleCentricTriage,
		// Token: 0x040005D0 RID: 1488
		HasShownIntroductionForModernGroups,
		// Token: 0x040005D1 RID: 1489
		ModernGroupsFirstUseDate,
		// Token: 0x040005D2 RID: 1490
		ModernGroupsLastUseDate,
		// Token: 0x040005D3 RID: 1491
		ModernGroupsUseCount,
		// Token: 0x040005D4 RID: 1492
		BuildGreenLightSurveyLastShownDate,
		// Token: 0x040005D5 RID: 1493
		HasShownPeopleIKnow,
		// Token: 0x040005D6 RID: 1494
		LearnabilityTypesShown,
		// Token: 0x040005D7 RID: 1495
		NavigationPaneViewOption,
		// Token: 0x040005D8 RID: 1496
		CalendarSearchUseCount,
		// Token: 0x040005D9 RID: 1497
		FrequentlyUsedFolders,
		// Token: 0x040005DA RID: 1498
		ArchiveFolderId,
		// Token: 0x040005DB RID: 1499
		DefaultAttachmentsUploadFolderId,
		// Token: 0x040005DC RID: 1500
		CalendarViewTypeNarrow,
		// Token: 0x040005DD RID: 1501
		CalendarViewTypeWide,
		// Token: 0x040005DE RID: 1502
		CalendarViewTypeDesktop,
		// Token: 0x040005DF RID: 1503
		CalendarSidePanelIsExpanded,
		// Token: 0x040005E0 RID: 1504
		FolderViewState,
		// Token: 0x040005E1 RID: 1505
		SchedulingViewType,
		// Token: 0x040005E2 RID: 1506
		SchedulingLastUsedRoomListName,
		// Token: 0x040005E3 RID: 1507
		SchedulingLastUsedRoomListEmailAddress,
		// Token: 0x040005E4 RID: 1508
		SearchHistory,
		// Token: 0x040005E5 RID: 1509
		PeopleHubDisplayOption,
		// Token: 0x040005E6 RID: 1510
		PeopleHubSortOption,
		// Token: 0x040005E7 RID: 1511
		CalendarSidePanelMonthPickerCount,
		// Token: 0x040005E8 RID: 1512
		SelectedCalendarsDesktop,
		// Token: 0x040005E9 RID: 1513
		SelectedCalendarsTWide,
		// Token: 0x040005EA RID: 1514
		SelectedCalendarsTNarrow,
		// Token: 0x040005EB RID: 1515
		AttachmentsFilePickerViewTypeForMouse,
		// Token: 0x040005EC RID: 1516
		AttachmentsFilePickerViewTypeForTouch,
		// Token: 0x040005ED RID: 1517
		BookmarkedWeatherLocations,
		// Token: 0x040005EE RID: 1518
		CurrentWeatherLocationBookmarkIndex,
		// Token: 0x040005EF RID: 1519
		TemperatureUnit,
		// Token: 0x040005F0 RID: 1520
		GlobalFolderViewState,
		// Token: 0x040005F1 RID: 1521
		CalendarAgendaViewIsExpandedMouse,
		// Token: 0x040005F2 RID: 1522
		CalendarAgendaViewIsExpandedTWide,
		// Token: 0x040005F3 RID: 1523
		AttachmentsFilePickerHideBanner,
		// Token: 0x040005F4 RID: 1524
		IsClutterUIEnabled
	}
}
