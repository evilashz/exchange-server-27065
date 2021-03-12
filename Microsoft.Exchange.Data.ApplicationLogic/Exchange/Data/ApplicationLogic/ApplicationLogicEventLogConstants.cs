using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000076 RID: 118
	internal static class ApplicationLogicEventLogConstants
	{
		// Token: 0x0400019E RID: 414
		public const string EventSource = "MSExchangeApplicationLogic";

		// Token: 0x0400019F RID: 415
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MonitorHostingDataFileFailure = new ExEventLog.EventTuple(3221488617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001A0 RID: 416
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LoadHostingDataFileFailure = new ExEventLog.EventTuple(3221488618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001A1 RID: 417
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LoadHostingDataFilesSuccess = new ExEventLog.EventTuple(1074004971U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001A2 RID: 418
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LocalServerNotInSite = new ExEventLog.EventTuple(3221496716U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001A3 RID: 419
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LocalServerNotInSiteWarning = new ExEventLog.EventTuple(2147754893U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001A4 RID: 420
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CantGetLocalIP = new ExEventLog.EventTuple(3221496718U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001A5 RID: 421
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CantGetLocalIPWarning = new ExEventLog.EventTuple(2147754895U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001A6 RID: 422
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoLocalServer = new ExEventLog.EventTuple(3221496720U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001A7 RID: 423
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoLocalServerWarning = new ExEventLog.EventTuple(2147754897U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001A8 RID: 424
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TopologyException = new ExEventLog.EventTuple(3221496722U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001A9 RID: 425
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoServerInSite = new ExEventLog.EventTuple(3221496723U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001AA RID: 426
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MisconfiguredServer = new ExEventLog.EventTuple(3221496724U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001AB RID: 427
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidExtensionRemoved = new ExEventLog.EventTuple(3221490617U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001AC RID: 428
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ExtensionsCacheReachedMaxSize = new ExEventLog.EventTuple(1074006970U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001AD RID: 429
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ExtensionUpdateQueryMaxExceeded = new ExEventLog.EventTuple(3221490619U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001AE RID: 430
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MismatchedCacheMailboxExtensionId = new ExEventLog.EventTuple(3221490620U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001AF RID: 431
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ExtensionUpdateFailed = new ExEventLog.EventTuple(3221490621U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001B0 RID: 432
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CachedManifestParseFailed = new ExEventLog.EventTuple(3221490622U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001B1 RID: 433
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MismatchedExtensionIdUpdateFailed = new ExEventLog.EventTuple(3221490623U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001B2 RID: 434
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MoreCapabilitiesSkipUpdate = new ExEventLog.EventTuple(1074006976U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001B3 RID: 435
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidStateSkipUpdate = new ExEventLog.EventTuple(1074006977U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001B4 RID: 436
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidAssetIDReturnedInDownload = new ExEventLog.EventTuple(3221490626U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001B5 RID: 437
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MismatchedExtensionIDReturnedInDownload = new ExEventLog.EventTuple(3221490627U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001B6 RID: 438
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OldVersionReturnedInDownload = new ExEventLog.EventTuple(3221490628U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001B7 RID: 439
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MoreCapabilitiesReturnedInDownload = new ExEventLog.EventTuple(3221490629U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001B8 RID: 440
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidAssetIDReturnedByAppState = new ExEventLog.EventTuple(3221490630U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001B9 RID: 441
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MismatchedExtensionIDReturnedByAppState = new ExEventLog.EventTuple(3221490631U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001BA RID: 442
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TooManyExtensionsForAutomaticUpdate = new ExEventLog.EventTuple(3221490632U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001BB RID: 443
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ResponseExceedsBufferSize = new ExEventLog.EventTuple(3221490633U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001BC RID: 444
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RequestFailed = new ExEventLog.EventTuple(3221490634U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001BD RID: 445
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EmptyAppStateResponse = new ExEventLog.EventTuple(3221490635U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001BE RID: 446
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OmexWebServiceResponseParsed = new ExEventLog.EventTuple(1074006989U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001BF RID: 447
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidAssetSignatureReturnedInDownload = new ExEventLog.EventTuple(3221490638U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001C0 RID: 448
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ManifestExceedsAllowedSize = new ExEventLog.EventTuple(3221490639U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001C1 RID: 449
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidVersionSubmitUpdateQuery = new ExEventLog.EventTuple(3221490640U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001C2 RID: 450
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DownloadKillbitListFailed = new ExEventLog.EventTuple(3221490641U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001C3 RID: 451
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DownloadKillbitListSuccessed = new ExEventLog.EventTuple(1074006994U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001C4 RID: 452
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EntryAddedToKillbitList = new ExEventLog.EventTuple(1074006995U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001C5 RID: 453
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToReadKillbitList = new ExEventLog.EventTuple(2147748820U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001C6 RID: 454
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_KillbitAssetTagRefreshRateNotFound = new ExEventLog.EventTuple(3221490645U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001C7 RID: 455
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AppInKillbitListRemoved = new ExEventLog.EventTuple(1074006998U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001C8 RID: 456
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AssetIdMissingInKillbitEntry = new ExEventLog.EventTuple(3221490647U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001C9 RID: 457
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AppIdMissingInKillbitEntry = new ExEventLog.EventTuple(3221490648U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001CA RID: 458
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AppStateResponseInvalidVersion = new ExEventLog.EventTuple(3221490649U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001CB RID: 459
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AppStateResponseInvalidMarketplaceAssetID = new ExEventLog.EventTuple(3221490650U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001CC RID: 460
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AppStateResponseInvalidExtensionID = new ExEventLog.EventTuple(3221490651U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001CD RID: 461
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AppStateResponseInvalidState = new ExEventLog.EventTuple(3221490652U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001CE RID: 462
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigResponseTokenNamesMissing = new ExEventLog.EventTuple(3221490653U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001CF RID: 463
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigResponseServiceNameMissing = new ExEventLog.EventTuple(3221490654U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001D0 RID: 464
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigResponseUrlsMissing = new ExEventLog.EventTuple(3221490655U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001D1 RID: 465
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigResponseServiceNameParseFailed = new ExEventLog.EventTuple(3221490656U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001D2 RID: 466
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigResponseUrlParseFailed = new ExEventLog.EventTuple(3221490657U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001D3 RID: 467
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigResponseUrlNoTokens = new ExEventLog.EventTuple(2147748834U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001D4 RID: 468
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigResponseUrlTooManyTokens = new ExEventLog.EventTuple(3221490659U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001D5 RID: 469
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigResponseUrlTokenNotFound = new ExEventLog.EventTuple(3221490660U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001D6 RID: 470
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigResponseUrlNotWellFormed = new ExEventLog.EventTuple(3221490661U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001D7 RID: 471
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EmptyKillbitListLocalFile = new ExEventLog.EventTuple(3221490662U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001D8 RID: 472
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_KillbitFolderNotExist = new ExEventLog.EventTuple(2147748839U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001D9 RID: 473
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CanNotCreateKillbitFolder = new ExEventLog.EventTuple(3221490664U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001DA RID: 474
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_KillbitFileWatcherFailed = new ExEventLog.EventTuple(3221490665U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001DB RID: 475
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaVersionRetrievalFailed = new ExEventLog.EventTuple(3221490666U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001DC RID: 476
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DefaultExtensionPathNotExist = new ExEventLog.EventTuple(3221490667U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001DD RID: 477
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DefaultExtensionFolderAccessFailed = new ExEventLog.EventTuple(3221490668U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001DE RID: 478
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DefaultExtensionRetrievalFailed = new ExEventLog.EventTuple(3221490669U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001DF RID: 479
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrganizationMailboxRetrievalFailed = new ExEventLog.EventTuple(3221490670U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001E0 RID: 480
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrganizationMailboxWebServiceUrlRetrievalFailed = new ExEventLog.EventTuple(3221490671U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001E1 RID: 481
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MastertableSaveFailedSaveConflict = new ExEventLog.EventTuple(3221490672U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001E2 RID: 482
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetOrgExtensionsTimedOut = new ExEventLog.EventTuple(3221490673U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001E3 RID: 483
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GetContentDeliveryNetworkEndpointFailed = new ExEventLog.EventTuple(3221490674U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001E4 RID: 484
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EcpUriRetrievalFailed = new ExEventLog.EventTuple(3221490675U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001E5 RID: 485
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FindFronEndOwaServiceFailed = new ExEventLog.EventTuple(3221490676U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001E6 RID: 486
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OrgExtensionParsingFailed = new ExEventLog.EventTuple(1074007029U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001E7 RID: 487
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DownloadDataFromOfficeMarketPlaceSucceeded = new ExEventLog.EventTuple(1074007030U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001E8 RID: 488
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DownloadDataFromOfficeMarketPlaceFailed = new ExEventLog.EventTuple(3221490679U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001E9 RID: 489
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_StoredEtokenCorrupted = new ExEventLog.EventTuple(3221490680U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001EA RID: 490
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MissingNodeInEtoken = new ExEventLog.EventTuple(3221490681U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001EB RID: 491
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidDeploymentIdInEtoken = new ExEventLog.EventTuple(3221490682U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001EC RID: 492
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AssetIdNotMatchInEtoken = new ExEventLog.EventTuple(3221490683U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001ED RID: 493
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToGetDeploymentId = new ExEventLog.EventTuple(1074007036U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001EE RID: 494
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToConfigAppStatus = new ExEventLog.EventTuple(3221490685U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001EF RID: 495
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ParseEtokenSuccess = new ExEventLog.EventTuple(1074007038U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001F0 RID: 496
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetExtensionsSuccess = new ExEventLog.EventTuple(1074007039U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001F1 RID: 497
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExtensionUpdateSuccess = new ExEventLog.EventTuple(1074007040U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001F2 RID: 498
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetExtensionsFailed = new ExEventLog.EventTuple(3221490689U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001F3 RID: 499
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToWritebackRenewedTokens = new ExEventLog.EventTuple(3221490691U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001F4 RID: 500
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExtensionTokenQueryMaxExceeded = new ExEventLog.EventTuple(3221490692U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001F5 RID: 501
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MismatchedReturnedToken = new ExEventLog.EventTuple(3221490693U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001F6 RID: 502
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessTokenRenewCompleted = new ExEventLog.EventTuple(1074007046U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001F7 RID: 503
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OrgLevelEtokenMustBeSiteLicense = new ExEventLog.EventTuple(3221490695U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040001F8 RID: 504
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_E4EOrganizationMailboxWebServiceUrlRetrievalFailed = new ExEventLog.EventTuple(3221491617U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001F9 RID: 505
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_E4EOrganizationMailboxRetrievalFailed = new ExEventLog.EventTuple(3221491618U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040001FA RID: 506
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PersistentHandlerRegistrationFailed = new ExEventLog.EventTuple(3221492617U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000077 RID: 119
		private enum Category : short
		{
			// Token: 0x040001FC RID: 508
			TextMessaging = 1,
			// Token: 0x040001FD RID: 509
			ServerPicker,
			// Token: 0x040001FE RID: 510
			Extension,
			// Token: 0x040001FF RID: 511
			E4E,
			// Token: 0x04000200 RID: 512
			DiagnosticHandlers
		}

		// Token: 0x02000078 RID: 120
		internal enum Message : uint
		{
			// Token: 0x04000202 RID: 514
			MonitorHostingDataFileFailure = 3221488617U,
			// Token: 0x04000203 RID: 515
			LoadHostingDataFileFailure,
			// Token: 0x04000204 RID: 516
			LoadHostingDataFilesSuccess = 1074004971U,
			// Token: 0x04000205 RID: 517
			LocalServerNotInSite = 3221496716U,
			// Token: 0x04000206 RID: 518
			LocalServerNotInSiteWarning = 2147754893U,
			// Token: 0x04000207 RID: 519
			CantGetLocalIP = 3221496718U,
			// Token: 0x04000208 RID: 520
			CantGetLocalIPWarning = 2147754895U,
			// Token: 0x04000209 RID: 521
			NoLocalServer = 3221496720U,
			// Token: 0x0400020A RID: 522
			NoLocalServerWarning = 2147754897U,
			// Token: 0x0400020B RID: 523
			TopologyException = 3221496722U,
			// Token: 0x0400020C RID: 524
			NoServerInSite,
			// Token: 0x0400020D RID: 525
			MisconfiguredServer,
			// Token: 0x0400020E RID: 526
			InvalidExtensionRemoved = 3221490617U,
			// Token: 0x0400020F RID: 527
			ExtensionsCacheReachedMaxSize = 1074006970U,
			// Token: 0x04000210 RID: 528
			ExtensionUpdateQueryMaxExceeded = 3221490619U,
			// Token: 0x04000211 RID: 529
			MismatchedCacheMailboxExtensionId,
			// Token: 0x04000212 RID: 530
			ExtensionUpdateFailed,
			// Token: 0x04000213 RID: 531
			CachedManifestParseFailed,
			// Token: 0x04000214 RID: 532
			MismatchedExtensionIdUpdateFailed,
			// Token: 0x04000215 RID: 533
			MoreCapabilitiesSkipUpdate = 1074006976U,
			// Token: 0x04000216 RID: 534
			InvalidStateSkipUpdate,
			// Token: 0x04000217 RID: 535
			InvalidAssetIDReturnedInDownload = 3221490626U,
			// Token: 0x04000218 RID: 536
			MismatchedExtensionIDReturnedInDownload,
			// Token: 0x04000219 RID: 537
			OldVersionReturnedInDownload,
			// Token: 0x0400021A RID: 538
			MoreCapabilitiesReturnedInDownload,
			// Token: 0x0400021B RID: 539
			InvalidAssetIDReturnedByAppState,
			// Token: 0x0400021C RID: 540
			MismatchedExtensionIDReturnedByAppState,
			// Token: 0x0400021D RID: 541
			TooManyExtensionsForAutomaticUpdate,
			// Token: 0x0400021E RID: 542
			ResponseExceedsBufferSize,
			// Token: 0x0400021F RID: 543
			RequestFailed,
			// Token: 0x04000220 RID: 544
			EmptyAppStateResponse,
			// Token: 0x04000221 RID: 545
			OmexWebServiceResponseParsed = 1074006989U,
			// Token: 0x04000222 RID: 546
			InvalidAssetSignatureReturnedInDownload = 3221490638U,
			// Token: 0x04000223 RID: 547
			ManifestExceedsAllowedSize,
			// Token: 0x04000224 RID: 548
			InvalidVersionSubmitUpdateQuery,
			// Token: 0x04000225 RID: 549
			DownloadKillbitListFailed,
			// Token: 0x04000226 RID: 550
			DownloadKillbitListSuccessed = 1074006994U,
			// Token: 0x04000227 RID: 551
			EntryAddedToKillbitList,
			// Token: 0x04000228 RID: 552
			FailedToReadKillbitList = 2147748820U,
			// Token: 0x04000229 RID: 553
			KillbitAssetTagRefreshRateNotFound = 3221490645U,
			// Token: 0x0400022A RID: 554
			AppInKillbitListRemoved = 1074006998U,
			// Token: 0x0400022B RID: 555
			AssetIdMissingInKillbitEntry = 3221490647U,
			// Token: 0x0400022C RID: 556
			AppIdMissingInKillbitEntry,
			// Token: 0x0400022D RID: 557
			AppStateResponseInvalidVersion,
			// Token: 0x0400022E RID: 558
			AppStateResponseInvalidMarketplaceAssetID,
			// Token: 0x0400022F RID: 559
			AppStateResponseInvalidExtensionID,
			// Token: 0x04000230 RID: 560
			AppStateResponseInvalidState,
			// Token: 0x04000231 RID: 561
			ConfigResponseTokenNamesMissing,
			// Token: 0x04000232 RID: 562
			ConfigResponseServiceNameMissing,
			// Token: 0x04000233 RID: 563
			ConfigResponseUrlsMissing,
			// Token: 0x04000234 RID: 564
			ConfigResponseServiceNameParseFailed,
			// Token: 0x04000235 RID: 565
			ConfigResponseUrlParseFailed,
			// Token: 0x04000236 RID: 566
			ConfigResponseUrlNoTokens = 2147748834U,
			// Token: 0x04000237 RID: 567
			ConfigResponseUrlTooManyTokens = 3221490659U,
			// Token: 0x04000238 RID: 568
			ConfigResponseUrlTokenNotFound,
			// Token: 0x04000239 RID: 569
			ConfigResponseUrlNotWellFormed,
			// Token: 0x0400023A RID: 570
			EmptyKillbitListLocalFile,
			// Token: 0x0400023B RID: 571
			KillbitFolderNotExist = 2147748839U,
			// Token: 0x0400023C RID: 572
			CanNotCreateKillbitFolder = 3221490664U,
			// Token: 0x0400023D RID: 573
			KillbitFileWatcherFailed,
			// Token: 0x0400023E RID: 574
			OwaVersionRetrievalFailed,
			// Token: 0x0400023F RID: 575
			DefaultExtensionPathNotExist,
			// Token: 0x04000240 RID: 576
			DefaultExtensionFolderAccessFailed,
			// Token: 0x04000241 RID: 577
			DefaultExtensionRetrievalFailed,
			// Token: 0x04000242 RID: 578
			OrganizationMailboxRetrievalFailed,
			// Token: 0x04000243 RID: 579
			OrganizationMailboxWebServiceUrlRetrievalFailed,
			// Token: 0x04000244 RID: 580
			MastertableSaveFailedSaveConflict,
			// Token: 0x04000245 RID: 581
			GetOrgExtensionsTimedOut,
			// Token: 0x04000246 RID: 582
			GetContentDeliveryNetworkEndpointFailed,
			// Token: 0x04000247 RID: 583
			EcpUriRetrievalFailed,
			// Token: 0x04000248 RID: 584
			FindFronEndOwaServiceFailed,
			// Token: 0x04000249 RID: 585
			OrgExtensionParsingFailed = 1074007029U,
			// Token: 0x0400024A RID: 586
			DownloadDataFromOfficeMarketPlaceSucceeded,
			// Token: 0x0400024B RID: 587
			DownloadDataFromOfficeMarketPlaceFailed = 3221490679U,
			// Token: 0x0400024C RID: 588
			StoredEtokenCorrupted,
			// Token: 0x0400024D RID: 589
			MissingNodeInEtoken,
			// Token: 0x0400024E RID: 590
			InvalidDeploymentIdInEtoken,
			// Token: 0x0400024F RID: 591
			AssetIdNotMatchInEtoken,
			// Token: 0x04000250 RID: 592
			FailedToGetDeploymentId = 1074007036U,
			// Token: 0x04000251 RID: 593
			FailedToConfigAppStatus = 3221490685U,
			// Token: 0x04000252 RID: 594
			ParseEtokenSuccess = 1074007038U,
			// Token: 0x04000253 RID: 595
			GetExtensionsSuccess,
			// Token: 0x04000254 RID: 596
			ExtensionUpdateSuccess,
			// Token: 0x04000255 RID: 597
			GetExtensionsFailed = 3221490689U,
			// Token: 0x04000256 RID: 598
			FailedToWritebackRenewedTokens = 3221490691U,
			// Token: 0x04000257 RID: 599
			ExtensionTokenQueryMaxExceeded,
			// Token: 0x04000258 RID: 600
			MismatchedReturnedToken,
			// Token: 0x04000259 RID: 601
			ProcessTokenRenewCompleted = 1074007046U,
			// Token: 0x0400025A RID: 602
			OrgLevelEtokenMustBeSiteLicense = 3221490695U,
			// Token: 0x0400025B RID: 603
			E4EOrganizationMailboxWebServiceUrlRetrievalFailed = 3221491617U,
			// Token: 0x0400025C RID: 604
			E4EOrganizationMailboxRetrievalFailed,
			// Token: 0x0400025D RID: 605
			PersistentHandlerRegistrationFailed = 3221492617U
		}
	}
}
