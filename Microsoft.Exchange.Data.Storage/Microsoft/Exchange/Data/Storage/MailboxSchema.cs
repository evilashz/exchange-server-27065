using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C81 RID: 3201
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxSchema : StoreObjectSchema
	{
		// Token: 0x17001E27 RID: 7719
		// (get) Token: 0x06007039 RID: 28729 RVA: 0x001F0E68 File Offset: 0x001EF068
		public new static MailboxSchema Instance
		{
			get
			{
				if (MailboxSchema.instance == null)
				{
					MailboxSchema.instance = new MailboxSchema();
				}
				return MailboxSchema.instance;
			}
		}

		// Token: 0x04004C96 RID: 19606
		[Autoload]
		internal static readonly StorePropertyDefinition Id = InternalSchema.MailboxId;

		// Token: 0x04004C97 RID: 19607
		public static readonly PropertyTagPropertyDefinition ImapSubscribeList = InternalSchema.ImapSubscribeList;

		// Token: 0x04004C98 RID: 19608
		public static readonly PropertyTagPropertyDefinition UserName = InternalSchema.UserName;

		// Token: 0x04004C99 RID: 19609
		public static readonly PropertyTagPropertyDefinition MailboxOofStateEx = InternalSchema.MailboxOofStateEx;

		// Token: 0x04004C9A RID: 19610
		public static readonly PropertyTagPropertyDefinition MailboxOofStateUserChangeTime = InternalSchema.MailboxOofStateUserChangeTime;

		// Token: 0x04004C9B RID: 19611
		[Autoload]
		public static readonly PropertyTagPropertyDefinition UserOofSettingsItemId = InternalSchema.UserOofSettingsItemId;

		// Token: 0x04004C9C RID: 19612
		[Autoload]
		internal static readonly PropertyTagPropertyDefinition AdditionalRenEntryIds = InternalSchema.AdditionalRenEntryIds;

		// Token: 0x04004C9D RID: 19613
		[Autoload]
		public static readonly PropertyTagPropertyDefinition QuotaStorageWarning = InternalSchema.QuotaStorageWarning;

		// Token: 0x04004C9E RID: 19614
		[Autoload]
		public static readonly PropertyTagPropertyDefinition StorageQuotaLimit = InternalSchema.StorageQuotaLimit;

		// Token: 0x04004C9F RID: 19615
		[Autoload]
		public static readonly PropertyTagPropertyDefinition PersistableTenantPartitionHint = InternalSchema.PersistableTenantPartitionHint;

		// Token: 0x04004CA0 RID: 19616
		public static readonly PropertyTagPropertyDefinition IsContentIndexingEnabled = InternalSchema.IsContentIndexingEnabled;

		// Token: 0x04004CA1 RID: 19617
		public static readonly StorePropertyDefinition UnifiedMessagingOptions = InternalSchema.UnifiedMessagingOptions;

		// Token: 0x04004CA2 RID: 19618
		public static readonly StorePropertyDefinition OfficeCommunicatorOptions = InternalSchema.OfficeCommunicatorOptions;

		// Token: 0x04004CA3 RID: 19619
		public static readonly PropertyTagPropertyDefinition InternetMdns = InternalSchema.InternetMdns;

		// Token: 0x04004CA4 RID: 19620
		[Autoload]
		public static readonly PropertyTagPropertyDefinition QuotaProhibitReceive = InternalSchema.ProhibitReceiveQuota;

		// Token: 0x04004CA5 RID: 19621
		[Autoload]
		public static readonly PropertyTagPropertyDefinition QuotaProhibitSend = InternalSchema.ProhibitSendQuota;

		// Token: 0x04004CA6 RID: 19622
		[Autoload]
		public static readonly PropertyTagPropertyDefinition QuotaUsed = InternalSchema.Size;

		// Token: 0x04004CA7 RID: 19623
		[Autoload]
		public static readonly PropertyTagPropertyDefinition QuotaUsedExtended = InternalSchema.ExtendedSize;

		// Token: 0x04004CA8 RID: 19624
		public static readonly PropertyTagPropertyDefinition DumpsterQuotaUsedExtended = InternalSchema.ExtendedDumpsterSize;

		// Token: 0x04004CA9 RID: 19625
		public static readonly PropertyTagPropertyDefinition MaxUserMessageSize = InternalSchema.MaxSubmitMessageSize;

		// Token: 0x04004CAA RID: 19626
		public static readonly StorePropertyDefinition MaxMessageSize = InternalSchema.MaxMessageSize;

		// Token: 0x04004CAB RID: 19627
		public static readonly PropertyTagPropertyDefinition SendReadNotifications = InternalSchema.SendReadNotifications;

		// Token: 0x04004CAC RID: 19628
		public static readonly PropertyTagPropertyDefinition MailboxMiscFlags = InternalSchema.MailboxMiscFlags;

		// Token: 0x04004CAD RID: 19629
		public static readonly PropertyTagPropertyDefinition MailboxGuid = InternalSchema.MailboxGuid;

		// Token: 0x04004CAE RID: 19630
		[Autoload]
		public static readonly PropertyTagPropertyDefinition MailboxNumber = InternalSchema.MailboxNumber;

		// Token: 0x04004CAF RID: 19631
		[Autoload]
		public static readonly PropertyTagPropertyDefinition LocaleId = InternalSchema.LocaleId;

		// Token: 0x04004CB0 RID: 19632
		[Autoload]
		public static readonly StorePropertyDefinition LastDelegatedAuditTime = InternalSchema.LastDelegatedAuditTime;

		// Token: 0x04004CB1 RID: 19633
		[Autoload]
		public static readonly StorePropertyDefinition LastExternalAuditTime = InternalSchema.LastExternalAuditTime;

		// Token: 0x04004CB2 RID: 19634
		[Autoload]
		public static readonly StorePropertyDefinition LastNonOwnerAuditTime = InternalSchema.LastNonOwnerAuditTime;

		// Token: 0x04004CB3 RID: 19635
		[Autoload]
		internal static readonly PropertyTagPropertyDefinition FinderEntryId = InternalSchema.FinderEntryId;

		// Token: 0x04004CB4 RID: 19636
		[Autoload]
		internal static readonly PropertyTagPropertyDefinition CommonViewsEntryId = InternalSchema.CommonViewsEntryId;

		// Token: 0x04004CB5 RID: 19637
		[Autoload]
		internal static readonly PropertyTagPropertyDefinition DeferredActionFolderEntryId = InternalSchema.DeferredActionFolderEntryId;

		// Token: 0x04004CB6 RID: 19638
		[Autoload]
		internal static readonly PropertyTagPropertyDefinition LegacyScheduleFolderEntryId = InternalSchema.LegacyScheduleFolderEntryId;

		// Token: 0x04004CB7 RID: 19639
		[Autoload]
		internal static readonly PropertyTagPropertyDefinition LegacyShortcutsFolderEntryId = InternalSchema.LegacyShortcutsFolderEntryId;

		// Token: 0x04004CB8 RID: 19640
		[Autoload]
		internal static readonly PropertyTagPropertyDefinition LegacyViewsFolderEntryId = InternalSchema.LegacyViewsFolderEntryId;

		// Token: 0x04004CB9 RID: 19641
		[Autoload]
		internal static readonly PropertyTagPropertyDefinition DeletedItemsEntryId = InternalSchema.DeletedItemsEntryId;

		// Token: 0x04004CBA RID: 19642
		[Autoload]
		internal static readonly PropertyTagPropertyDefinition SentItemsEntryId = InternalSchema.SentItemsEntryId;

		// Token: 0x04004CBB RID: 19643
		[Autoload]
		internal static readonly PropertyTagPropertyDefinition OutboxEntryId = InternalSchema.OutboxEntryId;

		// Token: 0x04004CBC RID: 19644
		[Autoload]
		internal static readonly PropertyTagPropertyDefinition LogonRightsOnMailbox = InternalSchema.LogonRightsOnMailbox;

		// Token: 0x04004CBD RID: 19645
		[Autoload]
		internal static readonly PropertyTagPropertyDefinition IsMailboxLocalized = InternalSchema.IsMailboxLocalized;

		// Token: 0x04004CBE RID: 19646
		[Autoload]
		public static readonly PropertyTagPropertyDefinition UserPhotoCacheId = InternalSchema.UserPhotoCacheId;

		// Token: 0x04004CBF RID: 19647
		[Autoload]
		public static readonly PropertyTagPropertyDefinition UserPhotoPreviewCacheId = InternalSchema.UserPhotoPreviewCacheId;

		// Token: 0x04004CC0 RID: 19648
		[Autoload]
		public static readonly PropertyTagPropertyDefinition InferenceClientActivityFlags = InternalSchema.InferenceClientActivityFlags;

		// Token: 0x04004CC1 RID: 19649
		[Autoload]
		public static readonly PropertyTagPropertyDefinition InferenceTrainedModelVersionBreadCrumb = InternalSchema.InferenceTrainedModelVersionBreadCrumb;

		// Token: 0x04004CC2 RID: 19650
		public static readonly PropertyTagPropertyDefinition ControlDataForCalendarRepairAssistant = InternalSchema.ControlDataForCalendarRepairAssistant;

		// Token: 0x04004CC3 RID: 19651
		public static readonly PropertyTagPropertyDefinition ControlDataForSharingPolicyAssistant = InternalSchema.ControlDataForSharingPolicyAssistant;

		// Token: 0x04004CC4 RID: 19652
		public static readonly PropertyTagPropertyDefinition ControlDataForElcAssistant = InternalSchema.ControlDataForElcAssistant;

		// Token: 0x04004CC5 RID: 19653
		public static readonly PropertyTagPropertyDefinition ElcLastRunTotalProcessingTime = InternalSchema.ElcLastRunTotalProcessingTime;

		// Token: 0x04004CC6 RID: 19654
		public static readonly PropertyTagPropertyDefinition ElcLastRunSubAssistantProcessingTime = InternalSchema.ElcLastRunSubAssistantProcessingTime;

		// Token: 0x04004CC7 RID: 19655
		public static readonly PropertyTagPropertyDefinition ElcLastRunUpdatedFolderCount = InternalSchema.ElcLastRunUpdatedFolderCount;

		// Token: 0x04004CC8 RID: 19656
		public static readonly PropertyTagPropertyDefinition ElcLastRunTaggedFolderCount = InternalSchema.ElcLastRunTaggedFolderCount;

		// Token: 0x04004CC9 RID: 19657
		public static readonly PropertyTagPropertyDefinition ElcLastRunUpdatedItemCount = InternalSchema.ElcLastRunUpdatedItemCount;

		// Token: 0x04004CCA RID: 19658
		public static readonly PropertyTagPropertyDefinition ElcLastRunTaggedWithArchiveItemCount = InternalSchema.ElcLastRunTaggedWithArchiveItemCount;

		// Token: 0x04004CCB RID: 19659
		public static readonly PropertyTagPropertyDefinition ElcLastRunTaggedWithExpiryItemCount = InternalSchema.ElcLastRunTaggedWithExpiryItemCount;

		// Token: 0x04004CCC RID: 19660
		public static readonly PropertyTagPropertyDefinition ElcLastRunDeletedFromRootItemCount = InternalSchema.ElcLastRunDeletedFromRootItemCount;

		// Token: 0x04004CCD RID: 19661
		public static readonly PropertyTagPropertyDefinition ElcLastRunDeletedFromDumpsterItemCount = InternalSchema.ElcLastRunDeletedFromDumpsterItemCount;

		// Token: 0x04004CCE RID: 19662
		public static readonly PropertyTagPropertyDefinition ElcLastRunArchivedFromRootItemCount = InternalSchema.ElcLastRunArchivedFromRootItemCount;

		// Token: 0x04004CCF RID: 19663
		public static readonly PropertyTagPropertyDefinition ElcLastRunArchivedFromDumpsterItemCount = InternalSchema.ElcLastRunArchivedFromDumpsterItemCount;

		// Token: 0x04004CD0 RID: 19664
		public static readonly PropertyTagPropertyDefinition ELCLastSuccessTimestamp = InternalSchema.ELCLastSuccessTimestamp;

		// Token: 0x04004CD1 RID: 19665
		public static readonly PropertyTagPropertyDefinition ControlDataForSearchIndexRepairAssistant = InternalSchema.ControlDataForSearchIndexRepairAssistant;

		// Token: 0x04004CD2 RID: 19666
		public static readonly PropertyTagPropertyDefinition ControlDataForTopNWordsAssistant = InternalSchema.ControlDataForTopNWordsAssistant;

		// Token: 0x04004CD3 RID: 19667
		public static readonly PropertyTagPropertyDefinition IsTopNEnabled = InternalSchema.IsTopNEnabled;

		// Token: 0x04004CD4 RID: 19668
		public static readonly PropertyTagPropertyDefinition ControlDataForJunkEmailAssistant = InternalSchema.ControlDataForJunkEmailAssistant;

		// Token: 0x04004CD5 RID: 19669
		public static readonly PropertyTagPropertyDefinition ControlDataForCalendarSyncAssistant = InternalSchema.ControlDataForCalendarSyncAssistant;

		// Token: 0x04004CD6 RID: 19670
		public static readonly PropertyTagPropertyDefinition ExternalSharingCalendarSubscriptionCount = InternalSchema.ExternalSharingCalendarSubscriptionCount;

		// Token: 0x04004CD7 RID: 19671
		public static readonly PropertyTagPropertyDefinition ConsumerSharingCalendarSubscriptionCount = InternalSchema.ConsumerSharingCalendarSubscriptionCount;

		// Token: 0x04004CD8 RID: 19672
		public static readonly PropertyTagPropertyDefinition ControlDataForUMReportingAssistant = InternalSchema.ControlDataForUMReportingAssistant;

		// Token: 0x04004CD9 RID: 19673
		public static readonly PropertyTagPropertyDefinition HasUMReportData = InternalSchema.HasUMReportData;

		// Token: 0x04004CDA RID: 19674
		public static readonly PropertyTagPropertyDefinition ControlDataForInferenceTrainingAssistant = InternalSchema.ControlDataForInferenceTrainingAssistant;

		// Token: 0x04004CDB RID: 19675
		public static readonly PropertyTagPropertyDefinition InferenceEnabled = InternalSchema.InferenceEnabled;

		// Token: 0x04004CDC RID: 19676
		public static readonly PropertyTagPropertyDefinition ControlDataForDirectoryProcessorAssistant = InternalSchema.ControlDataForDirectoryProcessorAssistant;

		// Token: 0x04004CDD RID: 19677
		public static readonly PropertyTagPropertyDefinition NeedsDirectoryProcessor = InternalSchema.NeedsDirectoryProcessor;

		// Token: 0x04004CDE RID: 19678
		public static readonly PropertyTagPropertyDefinition ControlDataForOABGeneratorAssistant = InternalSchema.ControlDataForOABGeneratorAssistant;

		// Token: 0x04004CDF RID: 19679
		public static readonly PropertyTagPropertyDefinition InternetCalendarSubscriptionCount = InternalSchema.InternetCalendarSubscriptionCount;

		// Token: 0x04004CE0 RID: 19680
		public static readonly PropertyTagPropertyDefinition ExternalSharingContactSubscriptionCount = InternalSchema.ExternalSharingContactSubscriptionCount;

		// Token: 0x04004CE1 RID: 19681
		public static readonly PropertyTagPropertyDefinition JunkEmailSafeListDirty = InternalSchema.JunkEmailSafeListDirty;

		// Token: 0x04004CE2 RID: 19682
		public static readonly PropertyTagPropertyDefinition LastSharingPolicyAppliedId = InternalSchema.LastSharingPolicyAppliedId;

		// Token: 0x04004CE3 RID: 19683
		public static readonly PropertyTagPropertyDefinition LastSharingPolicyAppliedHash = InternalSchema.LastSharingPolicyAppliedHash;

		// Token: 0x04004CE4 RID: 19684
		public static readonly PropertyTagPropertyDefinition LastSharingPolicyAppliedTime = InternalSchema.LastSharingPolicyAppliedTime;

		// Token: 0x04004CE5 RID: 19685
		[Autoload]
		public static readonly PropertyTagPropertyDefinition OofScheduleStart = InternalSchema.OofScheduleStart;

		// Token: 0x04004CE6 RID: 19686
		[Autoload]
		public static readonly PropertyTagPropertyDefinition OofScheduleEnd = InternalSchema.OofScheduleEnd;

		// Token: 0x04004CE7 RID: 19687
		public static readonly PropertyTagPropertyDefinition RetentionQueryInfo = InternalSchema.RetentionQueryInfo;

		// Token: 0x04004CE8 RID: 19688
		public static readonly PropertyTagPropertyDefinition ControlDataForPublicFolderAssistant = InternalSchema.ControlDataForPublicFolderAssistant;

		// Token: 0x04004CE9 RID: 19689
		public static readonly PropertyTagPropertyDefinition IsMarkedMailbox = InternalSchema.IsMarkedMailbox;

		// Token: 0x04004CEA RID: 19690
		public static readonly PropertyTagPropertyDefinition MailboxLastProcessedTimestamp = InternalSchema.MailboxLastProcessedTimestamp;

		// Token: 0x04004CEB RID: 19691
		[Autoload]
		public static readonly PropertyTagPropertyDefinition MailboxType = InternalSchema.MailboxType;

		// Token: 0x04004CEC RID: 19692
		[Autoload]
		public static readonly PropertyTagPropertyDefinition MailboxTypeDetail = InternalSchema.MailboxTypeDetail;

		// Token: 0x04004CED RID: 19693
		public static readonly PropertyTagPropertyDefinition ContactLinking = InternalSchema.ContactLinking;

		// Token: 0x04004CEE RID: 19694
		public static readonly PropertyTagPropertyDefinition ContactSaveVersion = InternalSchema.ContactSaveVersion;

		// Token: 0x04004CEF RID: 19695
		public static readonly PropertyTagPropertyDefinition PushNotificationSubscriptionType = InternalSchema.PushNotificationSubscriptionType;

		// Token: 0x04004CF0 RID: 19696
		public static readonly PropertyTagPropertyDefinition NotificationBrokerSubscriptions = InternalSchema.NotificationBrokerSubscriptions;

		// Token: 0x04004CF1 RID: 19697
		public static readonly PropertyTagPropertyDefinition ControlDataForInferenceDataCollectionAssistant = InternalSchema.ControlDataForInferenceDataCollectionAssistant;

		// Token: 0x04004CF2 RID: 19698
		public static readonly PropertyTagPropertyDefinition InferenceDataCollectionProcessingState = InternalSchema.InferenceDataCollectionProcessingState;

		// Token: 0x04004CF3 RID: 19699
		public static readonly PropertyTagPropertyDefinition SiteMailboxInternalState = InternalSchema.SiteMailboxInternalState;

		// Token: 0x04004CF4 RID: 19700
		[Autoload]
		public static readonly PropertyTagPropertyDefinition ExtendedRuleSizeLimit = InternalSchema.ExtendedRuleSizeLimit;

		// Token: 0x04004CF5 RID: 19701
		public static readonly PropertyTagPropertyDefinition ControlDataForSiteMailboxAssistant = InternalSchema.ControlDataForSiteMailboxAssistant;

		// Token: 0x04004CF6 RID: 19702
		public static readonly PropertyTagPropertyDefinition ControlDataForPeopleRelevanceAssistant = InternalSchema.ControlDataForPeopleRelevanceAssistant;

		// Token: 0x04004CF7 RID: 19703
		public static readonly PropertyTagPropertyDefinition ControlDataForSharePointSignalStoreAssistant = InternalSchema.ControlDataForSharePointSignalStoreAssistant;

		// Token: 0x04004CF8 RID: 19704
		public static readonly PropertyTagPropertyDefinition ControlDataForGroupMailboxAssistant = InternalSchema.ControlDataForGroupMailboxAssistant;

		// Token: 0x04004CF9 RID: 19705
		public static readonly PropertyTagPropertyDefinition ControlDataForMailboxAssociationReplicationAssistant = InternalSchema.ControlDataForMailboxAssociationReplicationAssistant;

		// Token: 0x04004CFA RID: 19706
		public static readonly PropertyTagPropertyDefinition ControlDataForPeopleCentricTriageAssistant = InternalSchema.ControlDataForPeopleCentricTriageAssistant;

		// Token: 0x04004CFB RID: 19707
		public static readonly PropertyTagPropertyDefinition MailboxAssociationNextReplicationTime = InternalSchema.MailboxAssociationNextReplicationTime;

		// Token: 0x04004CFC RID: 19708
		public static readonly PropertyTagPropertyDefinition MailboxAssociationProcessingFlags = InternalSchema.MailboxAssociationProcessingFlags;

		// Token: 0x04004CFD RID: 19709
		public static readonly PropertyTagPropertyDefinition GroupMailboxPermissionsVersion = InternalSchema.GroupMailboxPermissionsVersion;

		// Token: 0x04004CFE RID: 19710
		public static readonly PropertyTagPropertyDefinition GroupMailboxGeneratedPhotoSignature = InternalSchema.GroupMailboxGeneratedPhotoSignature;

		// Token: 0x04004CFF RID: 19711
		public static readonly PropertyTagPropertyDefinition GroupMailboxGeneratedPhotoVersion = InternalSchema.GroupMailboxGeneratedPhotoVersion;

		// Token: 0x04004D00 RID: 19712
		public static readonly PropertyTagPropertyDefinition GroupMailboxExchangeResourcesPublishedVersion = InternalSchema.GroupMailboxExchangeResourcesPublishedVersion;

		// Token: 0x04004D01 RID: 19713
		public static readonly PropertyTagPropertyDefinition ItemCount = InternalSchema.ItemCount;

		// Token: 0x04004D02 RID: 19714
		public static readonly PropertyTagPropertyDefinition InferenceTrainingLastContentCount = InternalSchema.InferenceTrainingLastContentCount;

		// Token: 0x04004D03 RID: 19715
		public static readonly PropertyTagPropertyDefinition InferenceTrainingLastAttemptTimestamp = InternalSchema.InferenceTrainingLastAttemptTimestamp;

		// Token: 0x04004D04 RID: 19716
		public static readonly PropertyTagPropertyDefinition InferenceTrainingLastSuccessTimestamp = InternalSchema.InferenceTrainingLastSuccessTimestamp;

		// Token: 0x04004D05 RID: 19717
		public static readonly PropertyTagPropertyDefinition InferenceTruthLoggingLastAttemptTimestamp = InternalSchema.InferenceTruthLoggingLastAttemptTimestamp;

		// Token: 0x04004D06 RID: 19718
		public static readonly PropertyTagPropertyDefinition InferenceTruthLoggingLastSuccessTimestamp = InternalSchema.InferenceTruthLoggingLastSuccessTimestamp;

		// Token: 0x04004D07 RID: 19719
		[Autoload]
		public static readonly PropertyTagPropertyDefinition InferenceUserCapabilityFlags = InternalSchema.InferenceUserCapabilityFlags;

		// Token: 0x04004D08 RID: 19720
		[Autoload]
		public static readonly StorePropertyDefinition InferenceUserClassificationReady = InternalSchema.InferenceUserClassificationReady;

		// Token: 0x04004D09 RID: 19721
		[Autoload]
		public static readonly StorePropertyDefinition InferenceUserUIReady = InternalSchema.InferenceUserUIReady;

		// Token: 0x04004D0A RID: 19722
		[Autoload]
		public static readonly StorePropertyDefinition InferenceClassificationEnabled = InternalSchema.InferenceClassificationEnabled;

		// Token: 0x04004D0B RID: 19723
		[Autoload]
		public static readonly StorePropertyDefinition InferenceClutterEnabled = InternalSchema.InferenceClutterEnabled;

		// Token: 0x04004D0C RID: 19724
		[Autoload]
		public static readonly StorePropertyDefinition InferenceHasBeenClutterInvited = InternalSchema.InferenceHasBeenClutterInvited;

		// Token: 0x04004D0D RID: 19725
		[Autoload]
		public static readonly StorePropertyDefinition MailboxOofState = new MailboxOofStateProperty();

		// Token: 0x04004D0E RID: 19726
		public static readonly PropertyTagPropertyDefinition InTransitStatus = InternalSchema.InTransitStatus;

		// Token: 0x04004D0F RID: 19727
		[Autoload]
		public static readonly PropertyTagPropertyDefinition ItemsPendingUpgrade = InternalSchema.ItemsPendingUpgrade;

		// Token: 0x04004D10 RID: 19728
		[Autoload]
		public static readonly PropertyTagPropertyDefinition LastLogonTime = InternalSchema.LastLogonTime;

		// Token: 0x04004D11 RID: 19729
		private static MailboxSchema instance = null;

		// Token: 0x04004D12 RID: 19730
		[Autoload]
		public static readonly StorePropertyDefinition InferenceOLKUserActivityLoggingEnabled = InternalSchema.InferenceOLKUserActivityLoggingEnabled;
	}
}
