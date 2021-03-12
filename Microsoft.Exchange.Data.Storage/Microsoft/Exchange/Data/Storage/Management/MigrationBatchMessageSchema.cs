using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A1D RID: 2589
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MigrationBatchMessageSchema
	{
		// Token: 0x040034FE RID: 13566
		public const DefaultFolderType WorkingFolder = DefaultFolderType.Inbox;

		// Token: 0x040034FF RID: 13567
		public const string InputCsvAttachmentName = "Request.csv";

		// Token: 0x04003500 RID: 13568
		public const string ErrorsCsvAttachmentName = "Errors.csv";

		// Token: 0x04003501 RID: 13569
		public static readonly char[] ListSeparator = new char[]
		{
			':'
		};

		// Token: 0x04003502 RID: 13570
		public static readonly string FolderSeparator = "\r\n";

		// Token: 0x04003503 RID: 13571
		public static readonly StorePropertyDefinition MigrationVersion = InternalSchema.MigrationVersion;

		// Token: 0x04003504 RID: 13572
		public static readonly StorePropertyDefinition MigrationJobId = InternalSchema.MigrationJobId;

		// Token: 0x04003505 RID: 13573
		public static readonly StorePropertyDefinition MigrationJobItemId = InternalSchema.MigrationJobItemId;

		// Token: 0x04003506 RID: 13574
		public static readonly StorePropertyDefinition MigrationJobName = InternalSchema.MigrationJobName;

		// Token: 0x04003507 RID: 13575
		public static readonly StorePropertyDefinition MigrationJobSubmittedBy = InternalSchema.MigrationJobSubmittedBy;

		// Token: 0x04003508 RID: 13576
		public static readonly StorePropertyDefinition MigrationJobTotalRowCount = InternalSchema.MigrationJobTotalRowCount;

		// Token: 0x04003509 RID: 13577
		public static readonly StorePropertyDefinition MigrationJobTotalItemCountLegacy = InternalSchema.MigrationJobTotalItemCountLegacy;

		// Token: 0x0400350A RID: 13578
		public static readonly StorePropertyDefinition MigrationJobCountCache = InternalSchema.MigrationJobCountCache;

		// Token: 0x0400350B RID: 13579
		public static readonly StorePropertyDefinition MigrationJobCountCacheFullScanTime = InternalSchema.MigrationJobCountCacheFullScanTime;

		// Token: 0x0400350C RID: 13580
		public static readonly StorePropertyDefinition MigrationJobExcludedFolders = InternalSchema.MigrationJobExcludedFolders;

		// Token: 0x0400350D RID: 13581
		public static readonly StorePropertyDefinition MigrationJobNotificationEmails = InternalSchema.MigrationJobNotificationEmails;

		// Token: 0x0400350E RID: 13582
		public static readonly StorePropertyDefinition MigrationJobOriginalCreationTime = InternalSchema.MigrationJobOriginalCreationTime;

		// Token: 0x0400350F RID: 13583
		public static readonly StorePropertyDefinition MigrationJobStartTime = InternalSchema.MigrationJobStartTime;

		// Token: 0x04003510 RID: 13584
		public static readonly StorePropertyDefinition MigrationJobLastRestartTime = InternalSchema.MigrationJobLastRestartTime;

		// Token: 0x04003511 RID: 13585
		public static readonly StorePropertyDefinition MigrationJobFinalizeTime = InternalSchema.MigrationJobFinalizeTime;

		// Token: 0x04003512 RID: 13586
		public static readonly StorePropertyDefinition MigrationJobLastFinalizationAttempt = InternalSchema.MigrationJobLastFinalizationAttempt;

		// Token: 0x04003513 RID: 13587
		public static readonly StorePropertyDefinition MigrationJobUserTimeZone = InternalSchema.MigrationJobUserTimeZone;

		// Token: 0x04003514 RID: 13588
		public static readonly StorePropertyDefinition MigrationJobCancelledFlag = InternalSchema.MigrationJobCancelledFlag;

		// Token: 0x04003515 RID: 13589
		public static readonly StorePropertyDefinition MigrationUserStatus = InternalSchema.MigrationJobItemStatus;

		// Token: 0x04003516 RID: 13590
		public static readonly StorePropertyDefinition MigrationJobItemRecipientType = InternalSchema.MigrationJobItemRecipientType;

		// Token: 0x04003517 RID: 13591
		public static readonly StorePropertyDefinition MigrationJobSuppressErrors = InternalSchema.MigrationJobSuppressErrors;

		// Token: 0x04003518 RID: 13592
		public static readonly StorePropertyDefinition MigrationJobItemIdentifier = InternalSchema.MigrationJobItemIdentifier;

		// Token: 0x04003519 RID: 13593
		public static readonly StorePropertyDefinition MigrationJobItemEncryptedIncomingPassword = InternalSchema.MigrationJobItemEncryptedIncomingPassword;

		// Token: 0x0400351A RID: 13594
		public static readonly StorePropertyDefinition MigrationJobItemIncomingUsername = InternalSchema.MigrationJobItemIncomingUsername;

		// Token: 0x0400351B RID: 13595
		public static readonly StorePropertyDefinition MigrationJobItemSubscriptionMessageId = InternalSchema.MigrationJobItemSubscriptionMessageId;

		// Token: 0x0400351C RID: 13596
		public static readonly StorePropertyDefinition MigrationJobItemMailboxServer = InternalSchema.MigrationJobItemMailboxServer;

		// Token: 0x0400351D RID: 13597
		public static readonly StorePropertyDefinition MigrationJobItemMailboxId = InternalSchema.MigrationJobItemMailboxId;

		// Token: 0x0400351E RID: 13598
		public static readonly StorePropertyDefinition MigrationJobItemMailboxDatabaseId = InternalSchema.MigrationJobItemMailboxDatabaseId;

		// Token: 0x0400351F RID: 13599
		public static readonly StorePropertyDefinition MigrationJobItemStateLastUpdated = InternalSchema.MigrationJobItemStateLastUpdated;

		// Token: 0x04003520 RID: 13600
		public static readonly StorePropertyDefinition MigrationJobItemSubscriptionCreated = InternalSchema.MigrationJobItemSubscriptionCreated;

		// Token: 0x04003521 RID: 13601
		public static readonly StorePropertyDefinition MigrationJobItemSubscriptionLastChecked = InternalSchema.MigrationJobItemSubscriptionLastChecked;

		// Token: 0x04003522 RID: 13602
		public static readonly StorePropertyDefinition MigrationJobItemMailboxLegacyDN = InternalSchema.MigrationJobItemMailboxLegacyDN;

		// Token: 0x04003523 RID: 13603
		public static readonly StorePropertyDefinition MigrationJobItemRowIndex = InternalSchema.MigrationJobItemRowIndex;

		// Token: 0x04003524 RID: 13604
		public static readonly StorePropertyDefinition MigrationJobItemLocalizedError = InternalSchema.MigrationJobItemLocalizedError;

		// Token: 0x04003525 RID: 13605
		public static readonly StorePropertyDefinition MigrationJobInternalError = InternalSchema.MigrationJobInternalError;

		// Token: 0x04003526 RID: 13606
		public static readonly StorePropertyDefinition MigrationJobInternalErrorTime = InternalSchema.MigrationJobInternalErrorTime;

		// Token: 0x04003527 RID: 13607
		public static readonly StorePropertyDefinition MigrationJobRemoteServerHostName = InternalSchema.MigrationJobRemoteServerHostName;

		// Token: 0x04003528 RID: 13608
		public static readonly StorePropertyDefinition MigrationJobRemoteServerPortNumber = InternalSchema.MigrationJobRemoteServerPortNumber;

		// Token: 0x04003529 RID: 13609
		public static readonly StorePropertyDefinition MigrationJobRemoteServerAuth = InternalSchema.MigrationJobRemoteServerAuth;

		// Token: 0x0400352A RID: 13610
		public static readonly StorePropertyDefinition MigrationJobRemoteServerSecurity = InternalSchema.MigrationJobRemoteServerSecurity;

		// Token: 0x0400352B RID: 13611
		public static readonly StorePropertyDefinition MigrationJobMaxConcurrentMigrations = InternalSchema.MigrationJobMaxConcurrentMigrations;

		// Token: 0x0400352C RID: 13612
		public static readonly StorePropertyDefinition MigrationJobAdminCulture = InternalSchema.MigrationJobAdminCulture;

		// Token: 0x0400352D RID: 13613
		public static readonly StorePropertyDefinition MigrationCacheEntryMailboxLegacyDN = InternalSchema.MigrationCacheEntryMailboxLegacyDN;

		// Token: 0x0400352E RID: 13614
		public static readonly StorePropertyDefinition MigrationCacheEntryTenantPartitionHint = InternalSchema.MigrationCacheEntryTenantPartitionHint;

		// Token: 0x0400352F RID: 13615
		public static readonly StorePropertyDefinition MigrationSubmittedByUserAdminType = InternalSchema.MigrationSubmittedByUserAdminType;

		// Token: 0x04003530 RID: 13616
		public static readonly StorePropertyDefinition MigrationCacheEntryLastUpdated = InternalSchema.MigrationCacheEntryLastUpdated;

		// Token: 0x04003531 RID: 13617
		public static readonly StorePropertyDefinition MigrationUserRootFolder = InternalSchema.MigrationUserRootFolder;

		// Token: 0x04003532 RID: 13618
		public static readonly StorePropertyDefinition MigrationType = InternalSchema.MigrationType;

		// Token: 0x04003533 RID: 13619
		public static readonly StorePropertyDefinition MigrationJobWindowsLiveNetId = InternalSchema.MigrationJobWindowsLiveNetId;

		// Token: 0x04003534 RID: 13620
		public static readonly StorePropertyDefinition MigrationJobCursorPosition = InternalSchema.MigrationJobCursorPosition;

		// Token: 0x04003535 RID: 13621
		public static readonly StorePropertyDefinition MigrationJobItemWLSASigned = InternalSchema.MigrationJobItemWLSASigned;

		// Token: 0x04003536 RID: 13622
		public static readonly StorePropertyDefinition MigrationJobOwnerId = InternalSchema.MigrationJobOwnerId;

		// Token: 0x04003537 RID: 13623
		public static readonly StorePropertyDefinition MigrationJobDelegatedAdminOwnerId = InternalSchema.MigrationJobDelegatedAdminOwnerId;

		// Token: 0x04003538 RID: 13624
		public static readonly StorePropertyDefinition MigrationJobExchangeHasAdminPrivilege = InternalSchema.MigrationJobHasAdminPrivilege;

		// Token: 0x04003539 RID: 13625
		public static readonly StorePropertyDefinition MigrationJobExchangeHasAutodiscovery = InternalSchema.MigrationJobHasAutodiscovery;

		// Token: 0x0400353A RID: 13626
		public static readonly StorePropertyDefinition MigrationJobExchangeEmailAddress = InternalSchema.MigrationJobEmailAddress;

		// Token: 0x0400353B RID: 13627
		public static readonly StorePropertyDefinition MigrationJobExchangeAutodiscoverUrl = InternalSchema.MigrationJobRemoteAutodiscoverUrl;

		// Token: 0x0400353C RID: 13628
		public static readonly StorePropertyDefinition MigrationJobExchangeRemoteServerHostName = InternalSchema.MigrationJobExchangeRemoteServerHostName;

		// Token: 0x0400353D RID: 13629
		public static readonly StorePropertyDefinition MigrationJobExchangeRPCProxyServerHostName = InternalSchema.MigrationJobProxyServerHostName;

		// Token: 0x0400353E RID: 13630
		public static readonly StorePropertyDefinition MigrationJobExchangeNSPIServerHostName = InternalSchema.MigrationJobRemoteNSPIServerHostName;

		// Token: 0x0400353F RID: 13631
		public static readonly StorePropertyDefinition MigrationJobExchangeDomain = InternalSchema.MigrationJobRemoteDomain;

		// Token: 0x04003540 RID: 13632
		public static readonly StorePropertyDefinition MigrationJobExchangeRemoteServerVersion = InternalSchema.MigrationJobRemoteServerVersion;

		// Token: 0x04003541 RID: 13633
		public static readonly StorePropertyDefinition MigrationJobStatisticsEnabled = InternalSchema.MigrationJobStatisticsEnabled;

		// Token: 0x04003542 RID: 13634
		public static readonly StorePropertyDefinition MigrationJobItemExchangeRemoteMailboxLegacyDN = InternalSchema.MigrationJobItemRemoteMailboxLegacyDN;

		// Token: 0x04003543 RID: 13635
		public static readonly StorePropertyDefinition MigrationJobItemExchangeRemoteServerLegacyDN = InternalSchema.MigrationJobItemRemoteServerLegacyDN;

		// Token: 0x04003544 RID: 13636
		public static readonly StorePropertyDefinition MigrationJobItemExchangeRemoteServerHostName = InternalSchema.MigrationJobItemExchangeRemoteServerHostName;

		// Token: 0x04003545 RID: 13637
		public static readonly StorePropertyDefinition MigrationJobItemExchangeRPCProxyServerHostName = InternalSchema.MigrationJobItemProxyServerHostName;

		// Token: 0x04003546 RID: 13638
		public static readonly StorePropertyDefinition MigrationJobItemExchangeAutodiscoverUrl = InternalSchema.MigrationJobItemRemoteAutodiscoverUrl;

		// Token: 0x04003547 RID: 13639
		public static readonly StorePropertyDefinition MigrationJobExchangeRemoteServerAuth = InternalSchema.MigrationJobExchangeRemoteServerAuth;

		// Token: 0x04003548 RID: 13640
		public static readonly StorePropertyDefinition MigrationJobItemProvisioningData = InternalSchema.MigrationJobItemProvisioningData;

		// Token: 0x04003549 RID: 13641
		public static readonly StorePropertyDefinition MigrationJobItemMRSId = InternalSchema.MigrationJobItemMRSId;

		// Token: 0x0400354A RID: 13642
		public static readonly StorePropertyDefinition MigrationJobItemExchangeRecipientIndex = InternalSchema.MigrationJobItemExchangeRecipientIndex;

		// Token: 0x0400354B RID: 13643
		public static readonly StorePropertyDefinition MigrationJobItemExchangeRecipientProperties = InternalSchema.MigrationJobItemExchangeRecipientProperties;

		// Token: 0x0400354C RID: 13644
		public static readonly StorePropertyDefinition MigrationJobItemExchangeMsExchHomeServerName = InternalSchema.MigrationJobItemExchangeMsExchHomeServerName;

		// Token: 0x0400354D RID: 13645
		public static readonly GuidIdPropertyDefinition MigrationJobItemItemsSynced = InternalSchema.MigrationJobItemItemsSynced;

		// Token: 0x0400354E RID: 13646
		public static readonly GuidIdPropertyDefinition MigrationJobItemItemsSkipped = InternalSchema.MigrationJobItemItemsSkipped;

		// Token: 0x0400354F RID: 13647
		public static readonly StorePropertyDefinition MigrationJobItemGroupMemberProvisioningState = InternalSchema.MigrationJobItemGroupMemberProvisioningState;

		// Token: 0x04003550 RID: 13648
		public static readonly StorePropertyDefinition MigrationJobItemGroupMemberProvisioned = InternalSchema.MigrationJobItemGroupMemberProvisioned;

		// Token: 0x04003551 RID: 13649
		public static readonly StorePropertyDefinition MigrationJobItemGroupMemberSkipped = InternalSchema.MigrationJobItemGroupMemberSkipped;

		// Token: 0x04003552 RID: 13650
		public static readonly StorePropertyDefinition MigrationJobItemLastProvisionedMemberIndex = InternalSchema.MigrationJobItemLastProvisionedMemberIndex;

		// Token: 0x04003553 RID: 13651
		public static readonly StorePropertyDefinition MigrationJobItemADObjectExists = InternalSchema.MigrationJobItemADObjectExists;

		// Token: 0x04003554 RID: 13652
		public static readonly StorePropertyDefinition MigrationReportName = InternalSchema.MigrationReportName;

		// Token: 0x04003555 RID: 13653
		public static readonly StorePropertyDefinition MigrationJobItemOwnerId = InternalSchema.MigrationJobItemOwnerId;

		// Token: 0x04003556 RID: 13654
		public static readonly StorePropertyDefinition MigrationJobCancellationReason = InternalSchema.MigrationJobCancellationReason;

		// Token: 0x04003557 RID: 13655
		public static readonly StorePropertyDefinition MigrationJobItemExchangeMbxEncryptedPassword = InternalSchema.MigrationJobItemExchangeMbxEncryptedPassword;

		// Token: 0x04003558 RID: 13656
		public static readonly StorePropertyDefinition MigrationJobIsStaged = InternalSchema.MigrationJobIsStaged;

		// Token: 0x04003559 RID: 13657
		public static readonly StorePropertyDefinition MigrationJobCheckWLSA = InternalSchema.MigrationJobCheckWLSA;

		// Token: 0x0400355A RID: 13658
		public static readonly StorePropertyDefinition MigrationJobItemTransientErrorCount = InternalSchema.MigrationJobItemTransientErrorCount;

		// Token: 0x0400355B RID: 13659
		public static readonly StorePropertyDefinition MigrationJobItemPreviousStatus = InternalSchema.MigrationJobItemPreviousStatus;

		// Token: 0x0400355C RID: 13660
		public static readonly StorePropertyDefinition MigrationJobItemSubscriptionId = InternalSchema.MigrationJobItemSubscriptionId;

		// Token: 0x0400355D RID: 13661
		public static readonly StorePropertyDefinition MigrationJobPoisonCount = InternalSchema.MigrationJobPoisonCount;

		// Token: 0x0400355E RID: 13662
		public static readonly StorePropertyDefinition MigrationReportType = InternalSchema.MigrationReportType;

		// Token: 0x0400355F RID: 13663
		public static readonly StorePropertyDefinition MigrationSuccessReportUrl = InternalSchema.MigrationSuccessReportUrl;

		// Token: 0x04003560 RID: 13664
		public static readonly StorePropertyDefinition MigrationErrorReportUrl = InternalSchema.MigrationErrorReportUrl;

		// Token: 0x04003561 RID: 13665
		public static readonly StorePropertyDefinition MigrationJobTargetDomainName = InternalSchema.MigrationJobTargetDomainName;

		// Token: 0x04003562 RID: 13666
		public static readonly StorePropertyDefinition MigrationJobItemForceChangePassword = InternalSchema.MigrationJobItemForceChangePassword;

		// Token: 0x04003563 RID: 13667
		public static readonly StorePropertyDefinition MigrationJobItemLocalizedErrorID = InternalSchema.MigrationJobItemLocalizedErrorID;

		// Token: 0x04003564 RID: 13668
		public static readonly StorePropertyDefinition MigrationJobItemStatusHistory = InternalSchema.MigrationJobItemStatusHistory;

		// Token: 0x04003565 RID: 13669
		public static readonly StorePropertyDefinition MigrationJobItemIDSIdentityFlags = InternalSchema.MigrationJobItemIDSIdentityFlags;

		// Token: 0x04003566 RID: 13670
		public static readonly StorePropertyDefinition MigrationJobItemLocalizedMessage = InternalSchema.MigrationJobItemLocalizedMessage;

		// Token: 0x04003567 RID: 13671
		public static readonly StorePropertyDefinition MigrationJobItemLocalizedMessageID = InternalSchema.MigrationJobItemLocalizedMessageID;

		// Token: 0x04003568 RID: 13672
		public static readonly StorePropertyDefinition MigrationSameStatusCount = InternalSchema.MigrationSameStatusCount;

		// Token: 0x04003569 RID: 13673
		public static readonly StorePropertyDefinition MigrationTransitionTime = InternalSchema.MigrationTransitionTime;

		// Token: 0x0400356A RID: 13674
		public static readonly StorePropertyDefinition MigrationDeltaSyncShouldSync = InternalSchema.MigrationDeltaSyncShouldSync;

		// Token: 0x0400356B RID: 13675
		public static readonly StorePropertyDefinition MigrationPersistableDictionary = InternalSchema.MigrationPersistableDictionary;

		// Token: 0x0400356C RID: 13676
		public static readonly StorePropertyDefinition MigrationRuntimeJobData = InternalSchema.MigrationRuntimeJobData;

		// Token: 0x0400356D RID: 13677
		public static readonly StorePropertyDefinition MigrationReportSets = InternalSchema.MigrationReportSets;

		// Token: 0x0400356E RID: 13678
		public static readonly StorePropertyDefinition MigrationDisableTime = InternalSchema.MigrationDisableTime;

		// Token: 0x0400356F RID: 13679
		public static readonly StorePropertyDefinition MigrationProvisionedTime = InternalSchema.MigrationProvisionedTime;

		// Token: 0x04003570 RID: 13680
		public static readonly StorePropertyDefinition MigrationLastSuccessfulSyncTime = InternalSchema.MigrationLastSuccessfulSyncTime;

		// Token: 0x04003571 RID: 13681
		public static readonly StorePropertyDefinition MigrationJobSourceEndpoint = InternalSchema.MigrationJobSourceEndpoint;

		// Token: 0x04003572 RID: 13682
		public static readonly StorePropertyDefinition MigrationJobTargetEndpoint = InternalSchema.MigrationJobTargetEndpoint;

		// Token: 0x04003573 RID: 13683
		public static readonly StorePropertyDefinition MigrationJobDirection = InternalSchema.MigrationJobDirection;

		// Token: 0x04003574 RID: 13684
		public static readonly PropertyDefinition MigrationJobSourcePublicFolderDatabase = InternalSchema.MigrationJobSourcePublicFolderDatabase;

		// Token: 0x04003575 RID: 13685
		public static readonly PropertyDefinition MigrationJobTargetDatabase = InternalSchema.MigrationJobTargetDatabase;

		// Token: 0x04003576 RID: 13686
		public static readonly PropertyDefinition MigrationJobTargetArchiveDatabase = InternalSchema.MigrationJobTargetArchiveDatabase;

		// Token: 0x04003577 RID: 13687
		public static readonly PropertyDefinition MigrationJobBadItemLimit = InternalSchema.MigrationJobBadItemLimit;

		// Token: 0x04003578 RID: 13688
		public static readonly PropertyDefinition MigrationJobLargeItemLimit = InternalSchema.MigrationJobLargeItemLimit;

		// Token: 0x04003579 RID: 13689
		public static readonly PropertyDefinition MigrationJobPrimaryOnly = InternalSchema.MigrationJobPrimaryOnly;

		// Token: 0x0400357A RID: 13690
		public static readonly PropertyDefinition MigrationJobArchiveOnly = InternalSchema.MigrationJobArchiveOnly;

		// Token: 0x0400357B RID: 13691
		public static readonly PropertyDefinition MigrationJobTargetDeliveryDomain = InternalSchema.MigrationJobTargetDeliveryDomain;

		// Token: 0x0400357C RID: 13692
		public static readonly PropertyDefinition MigrationSlotMaximumInitialSeedings = InternalSchema.MigrationSlotMaximumInitialSeedings;

		// Token: 0x0400357D RID: 13693
		public static readonly PropertyDefinition MigrationSlotMaximumIncrementalSeedings = InternalSchema.MigrationSlotMaximumIncrementalSeedings;

		// Token: 0x0400357E RID: 13694
		public static readonly PropertyDefinition MigrationJobSkipSteps = InternalSchema.MigrationJobSkipSteps;

		// Token: 0x0400357F RID: 13695
		public static readonly StorePropertyDefinition MigrationJobItemSlotType = InternalSchema.MigrationJobItemSlotType;

		// Token: 0x04003580 RID: 13696
		public static readonly StorePropertyDefinition MigrationJobItemSlotProviderId = InternalSchema.MigrationJobItemSlotProviderId;

		// Token: 0x04003581 RID: 13697
		public static readonly PropertyDefinition MigrationFailureRecord = InternalSchema.MigrationFailureRecord;

		// Token: 0x04003582 RID: 13698
		public static readonly PropertyDefinition MigrationJobIsRunning = InternalSchema.MigrationJobIsRunning;

		// Token: 0x04003583 RID: 13699
		public static readonly StorePropertyDefinition MigrationSubscriptionSettingsLastModifiedTime = InternalSchema.MigrationSubscriptionSettingsLastModifiedTime;

		// Token: 0x04003584 RID: 13700
		public static readonly StorePropertyDefinition MigrationJobItemSubscriptionSettingsLastUpdatedTime = InternalSchema.MigrationJobItemSubscriptionSettingsLastUpdatedTime;

		// Token: 0x04003585 RID: 13701
		public static readonly StorePropertyDefinition MigrationJobStartAfter = InternalSchema.MigrationJobStartAfter;

		// Token: 0x04003586 RID: 13702
		public static readonly StorePropertyDefinition MigrationJobCompleteAfter = InternalSchema.MigrationJobCompleteAfter;

		// Token: 0x04003587 RID: 13703
		public static readonly StorePropertyDefinition MigrationNextProcessTime = InternalSchema.MigrationNextProcessTime;

		// Token: 0x04003588 RID: 13704
		public static readonly StorePropertyDefinition MigrationStatusDataFailureWatsonHash = InternalSchema.MigrationStatusDataFailureWatsonHash;

		// Token: 0x04003589 RID: 13705
		public static readonly StorePropertyDefinition MigrationState = InternalSchema.MigrationState;

		// Token: 0x0400358A RID: 13706
		public static readonly StorePropertyDefinition MigrationFlags = InternalSchema.MigrationFlags;

		// Token: 0x0400358B RID: 13707
		public static readonly StorePropertyDefinition MigrationStage = InternalSchema.MigrationStage;

		// Token: 0x0400358C RID: 13708
		public static readonly StorePropertyDefinition MigrationStep = InternalSchema.MigrationStep;

		// Token: 0x0400358D RID: 13709
		public static readonly StorePropertyDefinition MigrationWorkflow = InternalSchema.MigrationWorkflow;

		// Token: 0x0400358E RID: 13710
		public static readonly StorePropertyDefinition MigrationPSTFilePath = InternalSchema.MigrationPSTFilePath;

		// Token: 0x0400358F RID: 13711
		public static readonly StorePropertyDefinition MigrationSourceRootFolder = InternalSchema.MigrationSourceRootFolder;

		// Token: 0x04003590 RID: 13712
		public static readonly StorePropertyDefinition MigrationTargetRootFolder = InternalSchema.MigrationTargetRootFolder;

		// Token: 0x04003591 RID: 13713
		public static readonly StorePropertyDefinition MigrationJobItemLocalMailboxIdentifier = InternalSchema.MigrationJobItemLocalMailboxIdentifier;

		// Token: 0x04003592 RID: 13714
		public static readonly StorePropertyDefinition MigrationExchangeObjectId = InternalSchema.MigrationExchangeObjectId;

		// Token: 0x04003593 RID: 13715
		public static readonly StorePropertyDefinition MigrationJobItemSubscriptionQueuedTime = InternalSchema.MigrationJobItemSubscriptionQueuedTime;

		// Token: 0x04003594 RID: 13716
		public static readonly StorePropertyDefinition MigrationJobItemPuid = InternalSchema.MigrationJobItemPuid;

		// Token: 0x04003595 RID: 13717
		public static readonly StorePropertyDefinition MigrationJobItemFirstName = InternalSchema.MigrationJobItemFirstName;

		// Token: 0x04003596 RID: 13718
		public static readonly StorePropertyDefinition MigrationJobItemLastName = InternalSchema.MigrationJobItemLastName;

		// Token: 0x04003597 RID: 13719
		public static readonly StorePropertyDefinition MigrationJobItemTimeZone = InternalSchema.MigrationJobItemTimeZone;

		// Token: 0x04003598 RID: 13720
		public static readonly StorePropertyDefinition MigrationJobItemLocaleId = InternalSchema.MigrationJobItemLocaleId;

		// Token: 0x04003599 RID: 13721
		public static readonly StorePropertyDefinition MigrationJobItemAliases = InternalSchema.MigrationJobItemAliases;

		// Token: 0x0400359A RID: 13722
		public static readonly StorePropertyDefinition MigrationJobItemAccountSize = InternalSchema.MigrationJobItemAccountSize;

		// Token: 0x0400359B RID: 13723
		public static readonly string MigrationJobClass = "IPM.MS-Exchange.MigrationJob";

		// Token: 0x0400359C RID: 13724
		public static readonly string MigrationJobItemClass = "IPM.MS-Exchange.MigrationJobItem";

		// Token: 0x0400359D RID: 13725
		public static readonly string MigrationCacheEntryClass = "IPM.MS-Exchange.MigrationCacheEntry";

		// Token: 0x0400359E RID: 13726
		public static readonly string MigrationReportItemClass = "IPM.MS-Exchange.MigrationReportItem";
	}
}
