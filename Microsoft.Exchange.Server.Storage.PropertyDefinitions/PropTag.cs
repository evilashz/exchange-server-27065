using System;

namespace Microsoft.Exchange.Server.Storage.PropTags
{
	// Token: 0x02000005 RID: 5
	public static class PropTag
	{
		// Token: 0x02000006 RID: 6
		public static class Mailbox
		{
			// Token: 0x04000DE2 RID: 3554
			public static readonly StorePropTag DeleteAfterSubmit = new StorePropTag(3585, PropertyType.Boolean, new StorePropInfo("DeleteAfterSubmit", 3585, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000DE3 RID: 3555
			public static readonly StorePropTag MessageSize = new StorePropTag(3592, PropertyType.Int64, new StorePropInfo("MessageSize", 3592, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000DE4 RID: 3556
			public static readonly StorePropTag MessageSize32 = new StorePropTag(3592, PropertyType.Int32, new StorePropInfo("MessageSize32", 3592, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000DE5 RID: 3557
			public static readonly StorePropTag SentMailEntryId = new StorePropTag(3594, PropertyType.Binary, new StorePropInfo("SentMailEntryId", 3594, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000DE6 RID: 3558
			public static readonly StorePropTag HighestFolderInternetId = new StorePropTag(3619, PropertyType.Int32, new StorePropInfo("HighestFolderInternetId", 3619, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000DE7 RID: 3559
			public static readonly StorePropTag NTSecurityDescriptor = new StorePropTag(3623, PropertyType.Binary, new StorePropInfo("NTSecurityDescriptor", 3623, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000DE8 RID: 3560
			public static readonly StorePropTag CISearchEnabled = new StorePropTag(3676, PropertyType.Boolean, new StorePropInfo("CISearchEnabled", 3676, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000DE9 RID: 3561
			public static readonly StorePropTag ExtendedRuleSizeLimit = new StorePropTag(3739, PropertyType.Int32, new StorePropInfo("ExtendedRuleSizeLimit", 3739, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000DEA RID: 3562
			public static readonly StorePropTag Access = new StorePropTag(4084, PropertyType.Int32, new StorePropInfo("Access", 4084, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000DEB RID: 3563
			public static readonly StorePropTag MappingSignature = new StorePropTag(4088, PropertyType.Binary, new StorePropInfo("MappingSignature", 4088, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000DEC RID: 3564
			public static readonly StorePropTag StoreRecordKey = new StorePropTag(4090, PropertyType.Binary, new StorePropInfo("StoreRecordKey", 4090, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000DED RID: 3565
			public static readonly StorePropTag StoreEntryId = new StorePropTag(4091, PropertyType.Binary, new StorePropInfo("StoreEntryId", 4091, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000DEE RID: 3566
			public static readonly StorePropTag DisplayName = new StorePropTag(12289, PropertyType.Unicode, new StorePropInfo("DisplayName", 12289, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000DEF RID: 3567
			public static readonly StorePropTag EmailAddress = new StorePropTag(12291, PropertyType.Unicode, new StorePropInfo("EmailAddress", 12291, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000DF0 RID: 3568
			public static readonly StorePropTag Comment = new StorePropTag(12292, PropertyType.Unicode, new StorePropInfo("Comment", 12292, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000DF1 RID: 3569
			public static readonly StorePropTag CreationTime = new StorePropTag(12295, PropertyType.SysTime, new StorePropInfo("CreationTime", 12295, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000DF2 RID: 3570
			public static readonly StorePropTag LastModificationTime = new StorePropTag(12296, PropertyType.SysTime, new StorePropInfo("LastModificationTime", 12296, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000DF3 RID: 3571
			public static readonly StorePropTag ResourceFlags = new StorePropTag(12297, PropertyType.Int32, new StorePropInfo("ResourceFlags", 12297, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000DF4 RID: 3572
			public static readonly StorePropTag MessageTableTotalPages = new StorePropTag(13313, PropertyType.Int32, new StorePropInfo("MessageTableTotalPages", 13313, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000DF5 RID: 3573
			public static readonly StorePropTag MessageTableAvailablePages = new StorePropTag(13314, PropertyType.Int32, new StorePropInfo("MessageTableAvailablePages", 13314, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000DF6 RID: 3574
			public static readonly StorePropTag OtherTablesTotalPages = new StorePropTag(13315, PropertyType.Int32, new StorePropInfo("OtherTablesTotalPages", 13315, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000DF7 RID: 3575
			public static readonly StorePropTag OtherTablesAvailablePages = new StorePropTag(13316, PropertyType.Int32, new StorePropInfo("OtherTablesAvailablePages", 13316, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000DF8 RID: 3576
			public static readonly StorePropTag AttachmentTableTotalPages = new StorePropTag(13317, PropertyType.Int32, new StorePropInfo("AttachmentTableTotalPages", 13317, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000DF9 RID: 3577
			public static readonly StorePropTag AttachmentTableAvailablePages = new StorePropTag(13318, PropertyType.Int32, new StorePropInfo("AttachmentTableAvailablePages", 13318, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000DFA RID: 3578
			public static readonly StorePropTag MailboxTypeVersion = new StorePropTag(13319, PropertyType.Int32, new StorePropInfo("MailboxTypeVersion", 13319, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000DFB RID: 3579
			public static readonly StorePropTag MailboxPartitionMailboxGuids = new StorePropTag(13320, PropertyType.MVGuid, new StorePropInfo("MailboxPartitionMailboxGuids", 13320, PropertyType.MVGuid, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000DFC RID: 3580
			public static readonly StorePropTag StoreSupportMask = new StorePropTag(13325, PropertyType.Int32, new StorePropInfo("StoreSupportMask", 13325, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000DFD RID: 3581
			public static readonly StorePropTag StoreState = new StorePropTag(13326, PropertyType.Int32, new StorePropInfo("StoreState", 13326, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000DFE RID: 3582
			public static readonly StorePropTag IPMSubtreeSearchKey = new StorePropTag(13328, PropertyType.Binary, new StorePropInfo("IPMSubtreeSearchKey", 13328, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000DFF RID: 3583
			public static readonly StorePropTag IPMOutboxSearchKey = new StorePropTag(13329, PropertyType.Binary, new StorePropInfo("IPMOutboxSearchKey", 13329, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E00 RID: 3584
			public static readonly StorePropTag IPMWastebasketSearchKey = new StorePropTag(13330, PropertyType.Binary, new StorePropInfo("IPMWastebasketSearchKey", 13330, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E01 RID: 3585
			public static readonly StorePropTag IPMSentmailSearchKey = new StorePropTag(13331, PropertyType.Binary, new StorePropInfo("IPMSentmailSearchKey", 13331, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E02 RID: 3586
			public static readonly StorePropTag MdbProvider = new StorePropTag(13332, PropertyType.Binary, new StorePropInfo("MdbProvider", 13332, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E03 RID: 3587
			public static readonly StorePropTag ReceiveFolderSettings = new StorePropTag(13333, PropertyType.Object, new StorePropInfo("ReceiveFolderSettings", 13333, PropertyType.Object, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E04 RID: 3588
			public static readonly StorePropTag LocalDirectoryEntryID = new StorePropTag(13334, PropertyType.Binary, new StorePropInfo("LocalDirectoryEntryID", 13334, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5)), ObjectType.Mailbox);

			// Token: 0x04000E05 RID: 3589
			public static readonly StorePropTag ControlDataForCalendarRepairAssistant = new StorePropTag(13344, PropertyType.Binary, new StorePropInfo("ControlDataForCalendarRepairAssistant", 13344, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E06 RID: 3590
			public static readonly StorePropTag ControlDataForSharingPolicyAssistant = new StorePropTag(13345, PropertyType.Binary, new StorePropInfo("ControlDataForSharingPolicyAssistant", 13345, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E07 RID: 3591
			public static readonly StorePropTag ControlDataForElcAssistant = new StorePropTag(13346, PropertyType.Binary, new StorePropInfo("ControlDataForElcAssistant", 13346, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E08 RID: 3592
			public static readonly StorePropTag ControlDataForTopNWordsAssistant = new StorePropTag(13347, PropertyType.Binary, new StorePropInfo("ControlDataForTopNWordsAssistant", 13347, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E09 RID: 3593
			public static readonly StorePropTag ControlDataForJunkEmailAssistant = new StorePropTag(13348, PropertyType.Binary, new StorePropInfo("ControlDataForJunkEmailAssistant", 13348, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E0A RID: 3594
			public static readonly StorePropTag ControlDataForCalendarSyncAssistant = new StorePropTag(13349, PropertyType.Binary, new StorePropInfo("ControlDataForCalendarSyncAssistant", 13349, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E0B RID: 3595
			public static readonly StorePropTag ExternalSharingCalendarSubscriptionCount = new StorePropTag(13350, PropertyType.Int32, new StorePropInfo("ExternalSharingCalendarSubscriptionCount", 13350, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E0C RID: 3596
			public static readonly StorePropTag ControlDataForUMReportingAssistant = new StorePropTag(13351, PropertyType.Binary, new StorePropInfo("ControlDataForUMReportingAssistant", 13351, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E0D RID: 3597
			public static readonly StorePropTag HasUMReportData = new StorePropTag(13352, PropertyType.Boolean, new StorePropInfo("HasUMReportData", 13352, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E0E RID: 3598
			public static readonly StorePropTag InternetCalendarSubscriptionCount = new StorePropTag(13353, PropertyType.Int32, new StorePropInfo("InternetCalendarSubscriptionCount", 13353, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E0F RID: 3599
			public static readonly StorePropTag ExternalSharingContactSubscriptionCount = new StorePropTag(13354, PropertyType.Int32, new StorePropInfo("ExternalSharingContactSubscriptionCount", 13354, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E10 RID: 3600
			public static readonly StorePropTag JunkEmailSafeListDirty = new StorePropTag(13355, PropertyType.Int32, new StorePropInfo("JunkEmailSafeListDirty", 13355, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E11 RID: 3601
			public static readonly StorePropTag IsTopNEnabled = new StorePropTag(13356, PropertyType.Boolean, new StorePropInfo("IsTopNEnabled", 13356, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E12 RID: 3602
			public static readonly StorePropTag LastSharingPolicyAppliedId = new StorePropTag(13357, PropertyType.Binary, new StorePropInfo("LastSharingPolicyAppliedId", 13357, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E13 RID: 3603
			public static readonly StorePropTag LastSharingPolicyAppliedHash = new StorePropTag(13358, PropertyType.Binary, new StorePropInfo("LastSharingPolicyAppliedHash", 13358, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E14 RID: 3604
			public static readonly StorePropTag LastSharingPolicyAppliedTime = new StorePropTag(13359, PropertyType.SysTime, new StorePropInfo("LastSharingPolicyAppliedTime", 13359, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E15 RID: 3605
			public static readonly StorePropTag OofScheduleStart = new StorePropTag(13360, PropertyType.SysTime, new StorePropInfo("OofScheduleStart", 13360, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E16 RID: 3606
			public static readonly StorePropTag OofScheduleEnd = new StorePropTag(13361, PropertyType.SysTime, new StorePropInfo("OofScheduleEnd", 13361, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E17 RID: 3607
			public static readonly StorePropTag ControlDataForDirectoryProcessorAssistant = new StorePropTag(13362, PropertyType.Binary, new StorePropInfo("ControlDataForDirectoryProcessorAssistant", 13362, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E18 RID: 3608
			public static readonly StorePropTag NeedsDirectoryProcessor = new StorePropTag(13363, PropertyType.Boolean, new StorePropInfo("NeedsDirectoryProcessor", 13363, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E19 RID: 3609
			public static readonly StorePropTag RetentionQueryIds = new StorePropTag(13364, PropertyType.MVUnicode, new StorePropInfo("RetentionQueryIds", 13364, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E1A RID: 3610
			public static readonly StorePropTag RetentionQueryInfo = new StorePropTag(13365, PropertyType.Int64, new StorePropInfo("RetentionQueryInfo", 13365, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E1B RID: 3611
			public static readonly StorePropTag ControlDataForPublicFolderAssistant = new StorePropTag(13367, PropertyType.Binary, new StorePropInfo("ControlDataForPublicFolderAssistant", 13367, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E1C RID: 3612
			public static readonly StorePropTag ControlDataForInferenceTrainingAssistant = new StorePropTag(13368, PropertyType.Binary, new StorePropInfo("ControlDataForInferenceTrainingAssistant", 13368, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E1D RID: 3613
			public static readonly StorePropTag InferenceEnabled = new StorePropTag(13369, PropertyType.Boolean, new StorePropInfo("InferenceEnabled", 13369, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E1E RID: 3614
			public static readonly StorePropTag ControlDataForContactLinkingAssistant = new StorePropTag(13370, PropertyType.Binary, new StorePropInfo("ControlDataForContactLinkingAssistant", 13370, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E1F RID: 3615
			public static readonly StorePropTag ContactLinking = new StorePropTag(13371, PropertyType.Int32, new StorePropInfo("ContactLinking", 13371, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E20 RID: 3616
			public static readonly StorePropTag ControlDataForOABGeneratorAssistant = new StorePropTag(13372, PropertyType.Binary, new StorePropInfo("ControlDataForOABGeneratorAssistant", 13372, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E21 RID: 3617
			public static readonly StorePropTag ContactSaveVersion = new StorePropTag(13373, PropertyType.Int32, new StorePropInfo("ContactSaveVersion", 13373, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E22 RID: 3618
			public static readonly StorePropTag ControlDataForOrgContactsSyncAssistant = new StorePropTag(13374, PropertyType.Binary, new StorePropInfo("ControlDataForOrgContactsSyncAssistant", 13374, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E23 RID: 3619
			public static readonly StorePropTag OrgContactsSyncTimestamp = new StorePropTag(13375, PropertyType.SysTime, new StorePropInfo("OrgContactsSyncTimestamp", 13375, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E24 RID: 3620
			public static readonly StorePropTag PushNotificationSubscriptionType = new StorePropTag(13376, PropertyType.Binary, new StorePropInfo("PushNotificationSubscriptionType", 13376, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E25 RID: 3621
			public static readonly StorePropTag OrgContactsSyncADWatermark = new StorePropTag(13377, PropertyType.SysTime, new StorePropInfo("OrgContactsSyncADWatermark", 13377, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E26 RID: 3622
			public static readonly StorePropTag ControlDataForInferenceDataCollectionAssistant = new StorePropTag(13378, PropertyType.Binary, new StorePropInfo("ControlDataForInferenceDataCollectionAssistant", 13378, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E27 RID: 3623
			public static readonly StorePropTag InferenceDataCollectionProcessingState = new StorePropTag(13379, PropertyType.Binary, new StorePropInfo("InferenceDataCollectionProcessingState", 13379, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E28 RID: 3624
			public static readonly StorePropTag ControlDataForPeopleRelevanceAssistant = new StorePropTag(13380, PropertyType.Binary, new StorePropInfo("ControlDataForPeopleRelevanceAssistant", 13380, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E29 RID: 3625
			public static readonly StorePropTag SiteMailboxInternalState = new StorePropTag(13381, PropertyType.Int32, new StorePropInfo("SiteMailboxInternalState", 13381, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E2A RID: 3626
			public static readonly StorePropTag ControlDataForSiteMailboxAssistant = new StorePropTag(13382, PropertyType.Binary, new StorePropInfo("ControlDataForSiteMailboxAssistant", 13382, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E2B RID: 3627
			public static readonly StorePropTag InferenceTrainingLastContentCount = new StorePropTag(13383, PropertyType.Int32, new StorePropInfo("InferenceTrainingLastContentCount", 13383, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E2C RID: 3628
			public static readonly StorePropTag InferenceTrainingLastAttemptTimestamp = new StorePropTag(13384, PropertyType.SysTime, new StorePropInfo("InferenceTrainingLastAttemptTimestamp", 13384, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E2D RID: 3629
			public static readonly StorePropTag InferenceTrainingLastSuccessTimestamp = new StorePropTag(13385, PropertyType.SysTime, new StorePropInfo("InferenceTrainingLastSuccessTimestamp", 13385, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E2E RID: 3630
			public static readonly StorePropTag InferenceUserCapabilityFlags = new StorePropTag(13386, PropertyType.Int32, new StorePropInfo("InferenceUserCapabilityFlags", 13386, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E2F RID: 3631
			public static readonly StorePropTag ControlDataForMailboxAssociationReplicationAssistant = new StorePropTag(13387, PropertyType.Binary, new StorePropInfo("ControlDataForMailboxAssociationReplicationAssistant", 13387, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E30 RID: 3632
			public static readonly StorePropTag MailboxAssociationNextReplicationTime = new StorePropTag(13388, PropertyType.SysTime, new StorePropInfo("MailboxAssociationNextReplicationTime", 13388, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E31 RID: 3633
			public static readonly StorePropTag MailboxAssociationProcessingFlags = new StorePropTag(13389, PropertyType.Int32, new StorePropInfo("MailboxAssociationProcessingFlags", 13389, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E32 RID: 3634
			public static readonly StorePropTag ControlDataForSharePointSignalStoreAssistant = new StorePropTag(13390, PropertyType.Binary, new StorePropInfo("ControlDataForSharePointSignalStoreAssistant", 13390, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E33 RID: 3635
			public static readonly StorePropTag ControlDataForPeopleCentricTriageAssistant = new StorePropTag(13391, PropertyType.Binary, new StorePropInfo("ControlDataForPeopleCentricTriageAssistant", 13391, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E34 RID: 3636
			public static readonly StorePropTag NotificationBrokerSubscriptions = new StorePropTag(13392, PropertyType.Int32, new StorePropInfo("NotificationBrokerSubscriptions", 13392, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E35 RID: 3637
			public static readonly StorePropTag GroupMailboxPermissionsVersion = new StorePropTag(13393, PropertyType.Int32, new StorePropInfo("GroupMailboxPermissionsVersion", 13393, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E36 RID: 3638
			public static readonly StorePropTag ElcLastRunTotalProcessingTime = new StorePropTag(13394, PropertyType.Int64, new StorePropInfo("ElcLastRunTotalProcessingTime", 13394, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E37 RID: 3639
			public static readonly StorePropTag ElcLastRunSubAssistantProcessingTime = new StorePropTag(13395, PropertyType.Int64, new StorePropInfo("ElcLastRunSubAssistantProcessingTime", 13395, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E38 RID: 3640
			public static readonly StorePropTag ElcLastRunUpdatedFolderCount = new StorePropTag(13396, PropertyType.Int64, new StorePropInfo("ElcLastRunUpdatedFolderCount", 13396, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E39 RID: 3641
			public static readonly StorePropTag ElcLastRunTaggedFolderCount = new StorePropTag(13397, PropertyType.Int64, new StorePropInfo("ElcLastRunTaggedFolderCount", 13397, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E3A RID: 3642
			public static readonly StorePropTag ElcLastRunUpdatedItemCount = new StorePropTag(13398, PropertyType.Int64, new StorePropInfo("ElcLastRunUpdatedItemCount", 13398, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E3B RID: 3643
			public static readonly StorePropTag ElcLastRunTaggedWithArchiveItemCount = new StorePropTag(13399, PropertyType.Int64, new StorePropInfo("ElcLastRunTaggedWithArchiveItemCount", 13399, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E3C RID: 3644
			public static readonly StorePropTag ElcLastRunTaggedWithExpiryItemCount = new StorePropTag(13400, PropertyType.Int64, new StorePropInfo("ElcLastRunTaggedWithExpiryItemCount", 13400, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E3D RID: 3645
			public static readonly StorePropTag ElcLastRunDeletedFromRootItemCount = new StorePropTag(13401, PropertyType.Int64, new StorePropInfo("ElcLastRunDeletedFromRootItemCount", 13401, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E3E RID: 3646
			public static readonly StorePropTag ElcLastRunDeletedFromDumpsterItemCount = new StorePropTag(13402, PropertyType.Int64, new StorePropInfo("ElcLastRunDeletedFromDumpsterItemCount", 13402, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E3F RID: 3647
			public static readonly StorePropTag ElcLastRunArchivedFromRootItemCount = new StorePropTag(13403, PropertyType.Int64, new StorePropInfo("ElcLastRunArchivedFromRootItemCount", 13403, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E40 RID: 3648
			public static readonly StorePropTag ElcLastRunArchivedFromDumpsterItemCount = new StorePropTag(13404, PropertyType.Int64, new StorePropInfo("ElcLastRunArchivedFromDumpsterItemCount", 13404, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E41 RID: 3649
			public static readonly StorePropTag ScheduledISIntegLastFinished = new StorePropTag(13405, PropertyType.SysTime, new StorePropInfo("ScheduledISIntegLastFinished", 13405, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E42 RID: 3650
			public static readonly StorePropTag ControlDataForSearchIndexRepairAssistant = new StorePropTag(13406, PropertyType.Binary, new StorePropInfo("ControlDataForSearchIndexRepairAssistant", 13406, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E43 RID: 3651
			public static readonly StorePropTag ELCLastSuccessTimestamp = new StorePropTag(13407, PropertyType.SysTime, new StorePropInfo("ELCLastSuccessTimestamp", 13407, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E44 RID: 3652
			public static readonly StorePropTag InferenceTruthLoggingLastAttemptTimestamp = new StorePropTag(13409, PropertyType.SysTime, new StorePropInfo("InferenceTruthLoggingLastAttemptTimestamp", 13409, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E45 RID: 3653
			public static readonly StorePropTag InferenceTruthLoggingLastSuccessTimestamp = new StorePropTag(13410, PropertyType.SysTime, new StorePropInfo("InferenceTruthLoggingLastSuccessTimestamp", 13410, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E46 RID: 3654
			public static readonly StorePropTag ControlDataForGroupMailboxAssistant = new StorePropTag(13411, PropertyType.Binary, new StorePropInfo("ControlDataForGroupMailboxAssistant", 13411, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E47 RID: 3655
			public static readonly StorePropTag ItemsPendingUpgrade = new StorePropTag(13412, PropertyType.Int32, new StorePropInfo("ItemsPendingUpgrade", 13412, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E48 RID: 3656
			public static readonly StorePropTag ConsumerSharingCalendarSubscriptionCount = new StorePropTag(13413, PropertyType.Int32, new StorePropInfo("ConsumerSharingCalendarSubscriptionCount", 13413, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E49 RID: 3657
			public static readonly StorePropTag GroupMailboxGeneratedPhotoVersion = new StorePropTag(13414, PropertyType.Int32, new StorePropInfo("GroupMailboxGeneratedPhotoVersion", 13414, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E4A RID: 3658
			public static readonly StorePropTag GroupMailboxGeneratedPhotoSignature = new StorePropTag(13415, PropertyType.Binary, new StorePropInfo("GroupMailboxGeneratedPhotoSignature", 13415, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E4B RID: 3659
			public static readonly StorePropTag GroupMailboxExchangeResourcesPublishedVersion = new StorePropTag(13416, PropertyType.Int32, new StorePropInfo("GroupMailboxExchangeResourcesPublishedVersion", 13416, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E4C RID: 3660
			public static readonly StorePropTag ValidFolderMask = new StorePropTag(13791, PropertyType.Int32, new StorePropInfo("ValidFolderMask", 13791, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E4D RID: 3661
			public static readonly StorePropTag IPMSubtreeEntryId = new StorePropTag(13792, PropertyType.Binary, new StorePropInfo("IPMSubtreeEntryId", 13792, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E4E RID: 3662
			public static readonly StorePropTag IPMOutboxEntryId = new StorePropTag(13794, PropertyType.Binary, new StorePropInfo("IPMOutboxEntryId", 13794, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E4F RID: 3663
			public static readonly StorePropTag IPMWastebasketEntryId = new StorePropTag(13795, PropertyType.Binary, new StorePropInfo("IPMWastebasketEntryId", 13795, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E50 RID: 3664
			public static readonly StorePropTag IPMSentmailEntryId = new StorePropTag(13796, PropertyType.Binary, new StorePropInfo("IPMSentmailEntryId", 13796, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E51 RID: 3665
			public static readonly StorePropTag IPMViewsEntryId = new StorePropTag(13797, PropertyType.Binary, new StorePropInfo("IPMViewsEntryId", 13797, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E52 RID: 3666
			public static readonly StorePropTag UnsearchableItems = new StorePropTag(13822, PropertyType.Binary, new StorePropInfo("UnsearchableItems", 13822, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E53 RID: 3667
			public static readonly StorePropTag IPMFinderEntryId = new StorePropTag(13824, PropertyType.Binary, new StorePropInfo("IPMFinderEntryId", 13824, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E54 RID: 3668
			public static readonly StorePropTag ContentCount = new StorePropTag(13826, PropertyType.Int32, new StorePropInfo("ContentCount", 13826, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E55 RID: 3669
			public static readonly StorePropTag ContentCountInt64 = new StorePropTag(13826, PropertyType.Int64, new StorePropInfo("ContentCountInt64", 13826, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E56 RID: 3670
			public static readonly StorePropTag Search = new StorePropTag(13831, PropertyType.Object, new StorePropInfo("Search", 13831, PropertyType.Object, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E57 RID: 3671
			public static readonly StorePropTag AssociatedContentCount = new StorePropTag(13847, PropertyType.Int32, new StorePropInfo("AssociatedContentCount", 13847, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E58 RID: 3672
			public static readonly StorePropTag AssociatedContentCountInt64 = new StorePropTag(13847, PropertyType.Int64, new StorePropInfo("AssociatedContentCountInt64", 13847, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E59 RID: 3673
			public static readonly StorePropTag AdditionalRENEntryIds = new StorePropTag(14040, PropertyType.MVBinary, new StorePropInfo("AdditionalRENEntryIds", 14040, PropertyType.MVBinary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E5A RID: 3674
			public static readonly StorePropTag SimpleDisplayName = new StorePropTag(14847, PropertyType.Unicode, new StorePropInfo("SimpleDisplayName", 14847, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E5B RID: 3675
			public static readonly StorePropTag TestBlobProperty = new StorePropTag(15616, PropertyType.Int64, new StorePropInfo("TestBlobProperty", 15616, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E5C RID: 3676
			public static readonly StorePropTag ScheduledISIntegCorruptionCount = new StorePropTag(15773, PropertyType.Int32, new StorePropInfo("ScheduledISIntegCorruptionCount", 15773, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(0, 1, 3)), ObjectType.Mailbox);

			// Token: 0x04000E5D RID: 3677
			public static readonly StorePropTag ScheduledISIntegExecutionTime = new StorePropTag(15774, PropertyType.Int32, new StorePropInfo("ScheduledISIntegExecutionTime", 15774, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(0, 1, 3)), ObjectType.Mailbox);

			// Token: 0x04000E5E RID: 3678
			public static readonly StorePropTag MailboxPartitionNumber = new StorePropTag(15775, PropertyType.Int32, new StorePropInfo("MailboxPartitionNumber", 15775, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(0, 1, 3)), ObjectType.Mailbox);

			// Token: 0x04000E5F RID: 3679
			public static readonly StorePropTag MailboxTypeDetail = new StorePropTag(15782, PropertyType.Int32, new StorePropInfo("MailboxTypeDetail", 15782, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E60 RID: 3680
			public static readonly StorePropTag InternalTenantHint = new StorePropTag(15783, PropertyType.Binary, new StorePropInfo("InternalTenantHint", 15783, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(0, 1, 3)), ObjectType.Mailbox);

			// Token: 0x04000E61 RID: 3681
			public static readonly StorePropTag TenantHint = new StorePropTag(15790, PropertyType.Binary, new StorePropInfo("TenantHint", 15790, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E62 RID: 3682
			public static readonly StorePropTag MaintenanceId = new StorePropTag(15803, PropertyType.Guid, new StorePropInfo("MaintenanceId", 15803, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, new PropertyCategories(0, 1, 3)), ObjectType.Mailbox);

			// Token: 0x04000E63 RID: 3683
			public static readonly StorePropTag MailboxType = new StorePropTag(15804, PropertyType.Int32, new StorePropInfo("MailboxType", 15804, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E64 RID: 3684
			public static readonly StorePropTag ACLData = new StorePropTag(16352, PropertyType.Binary, new StorePropInfo("ACLData", 16352, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E65 RID: 3685
			public static readonly StorePropTag DesignInProgress = new StorePropTag(16356, PropertyType.Boolean, new StorePropInfo("DesignInProgress", 16356, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E66 RID: 3686
			public static readonly StorePropTag StorageQuotaLimit = new StorePropTag(16373, PropertyType.Int32, new StorePropInfo("StorageQuotaLimit", 16373, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E67 RID: 3687
			public static readonly StorePropTag RulesSize = new StorePropTag(16383, PropertyType.Int32, new StorePropInfo("RulesSize", 16383, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 3)), ObjectType.Mailbox);

			// Token: 0x04000E68 RID: 3688
			public static readonly StorePropTag IMAPSubscribeList = new StorePropTag(26102, PropertyType.MVUnicode, new StorePropInfo("IMAPSubscribeList", 26102, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E69 RID: 3689
			public static readonly StorePropTag InTransitState = new StorePropTag(26136, PropertyType.Boolean, new StorePropInfo("InTransitState", 26136, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E6A RID: 3690
			public static readonly StorePropTag InTransitStatus = new StorePropTag(26136, PropertyType.Int32, new StorePropInfo("InTransitStatus", 26136, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5)), ObjectType.Mailbox);

			// Token: 0x04000E6B RID: 3691
			public static readonly StorePropTag UserEntryId = new StorePropTag(26137, PropertyType.Binary, new StorePropInfo("UserEntryId", 26137, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E6C RID: 3692
			public static readonly StorePropTag UserName = new StorePropTag(26138, PropertyType.Unicode, new StorePropInfo("UserName", 26138, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E6D RID: 3693
			public static readonly StorePropTag MailboxOwnerEntryId = new StorePropTag(26139, PropertyType.Binary, new StorePropInfo("MailboxOwnerEntryId", 26139, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E6E RID: 3694
			public static readonly StorePropTag MailboxOwnerName = new StorePropTag(26140, PropertyType.Unicode, new StorePropInfo("MailboxOwnerName", 26140, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E6F RID: 3695
			public static readonly StorePropTag OofState = new StorePropTag(26141, PropertyType.Boolean, new StorePropInfo("OofState", 26141, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E70 RID: 3696
			public static readonly StorePropTag TestLineSpeed = new StorePropTag(26155, PropertyType.Binary, new StorePropInfo("TestLineSpeed", 26155, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E71 RID: 3697
			public static readonly StorePropTag SerializedReplidGuidMap = new StorePropTag(26168, PropertyType.Binary, new StorePropInfo("SerializedReplidGuidMap", 26168, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E72 RID: 3698
			public static readonly StorePropTag DeletedMsgCount = new StorePropTag(26176, PropertyType.Int32, new StorePropInfo("DeletedMsgCount", 26176, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E73 RID: 3699
			public static readonly StorePropTag DeletedMsgCountInt64 = new StorePropTag(26176, PropertyType.Int64, new StorePropInfo("DeletedMsgCountInt64", 26176, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E74 RID: 3700
			public static readonly StorePropTag DeletedAssocMsgCount = new StorePropTag(26179, PropertyType.Int32, new StorePropInfo("DeletedAssocMsgCount", 26179, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E75 RID: 3701
			public static readonly StorePropTag DeletedAssocMsgCountInt64 = new StorePropTag(26179, PropertyType.Int64, new StorePropInfo("DeletedAssocMsgCountInt64", 26179, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E76 RID: 3702
			public static readonly StorePropTag HasNamedProperties = new StorePropTag(26186, PropertyType.Boolean, new StorePropInfo("HasNamedProperties", 26186, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E77 RID: 3703
			public static readonly StorePropTag ActiveUserEntryId = new StorePropTag(26194, PropertyType.Binary, new StorePropInfo("ActiveUserEntryId", 26194, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E78 RID: 3704
			public static readonly StorePropTag ProhibitReceiveQuota = new StorePropTag(26218, PropertyType.Int32, new StorePropInfo("ProhibitReceiveQuota", 26218, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E79 RID: 3705
			public static readonly StorePropTag MaxSubmitMessageSize = new StorePropTag(26221, PropertyType.Int32, new StorePropInfo("MaxSubmitMessageSize", 26221, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E7A RID: 3706
			public static readonly StorePropTag ProhibitSendQuota = new StorePropTag(26222, PropertyType.Int32, new StorePropInfo("ProhibitSendQuota", 26222, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E7B RID: 3707
			public static readonly StorePropTag DeletedOn = new StorePropTag(26255, PropertyType.SysTime, new StorePropInfo("DeletedOn", 26255, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E7C RID: 3708
			public static readonly StorePropTag MailboxDatabaseVersion = new StorePropTag(26266, PropertyType.Int32, new StorePropInfo("MailboxDatabaseVersion", 26266, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E7D RID: 3709
			public static readonly StorePropTag DeletedMessageSize = new StorePropTag(26267, PropertyType.Int64, new StorePropInfo("DeletedMessageSize", 26267, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E7E RID: 3710
			public static readonly StorePropTag DeletedMessageSize32 = new StorePropTag(26267, PropertyType.Int32, new StorePropInfo("DeletedMessageSize32", 26267, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E7F RID: 3711
			public static readonly StorePropTag DeletedNormalMessageSize = new StorePropTag(26268, PropertyType.Int64, new StorePropInfo("DeletedNormalMessageSize", 26268, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E80 RID: 3712
			public static readonly StorePropTag DeletedNormalMessageSize32 = new StorePropTag(26268, PropertyType.Int32, new StorePropInfo("DeletedNormalMessageSize32", 26268, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E81 RID: 3713
			public static readonly StorePropTag DeletedAssociatedMessageSize = new StorePropTag(26269, PropertyType.Int64, new StorePropInfo("DeletedAssociatedMessageSize", 26269, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E82 RID: 3714
			public static readonly StorePropTag DeletedAssociatedMessageSize32 = new StorePropTag(26269, PropertyType.Int32, new StorePropInfo("DeletedAssociatedMessageSize32", 26269, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E83 RID: 3715
			public static readonly StorePropTag NTUsername = new StorePropTag(26272, PropertyType.Unicode, new StorePropInfo("NTUsername", 26272, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E84 RID: 3716
			public static readonly StorePropTag NTUserSid = new StorePropTag(26272, PropertyType.Binary, new StorePropInfo("NTUserSid", 26272, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E85 RID: 3717
			public static readonly StorePropTag LocaleId = new StorePropTag(26273, PropertyType.Int32, new StorePropInfo("LocaleId", 26273, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E86 RID: 3718
			public static readonly StorePropTag LastLogonTime = new StorePropTag(26274, PropertyType.SysTime, new StorePropInfo("LastLogonTime", 26274, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4)), ObjectType.Mailbox);

			// Token: 0x04000E87 RID: 3719
			public static readonly StorePropTag LastLogoffTime = new StorePropTag(26275, PropertyType.SysTime, new StorePropInfo("LastLogoffTime", 26275, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4)), ObjectType.Mailbox);

			// Token: 0x04000E88 RID: 3720
			public static readonly StorePropTag StorageLimitInformation = new StorePropTag(26276, PropertyType.Int32, new StorePropInfo("StorageLimitInformation", 26276, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E89 RID: 3721
			public static readonly StorePropTag InternetMdns = new StorePropTag(26277, PropertyType.Boolean, new StorePropInfo("InternetMdns", 26277, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E8A RID: 3722
			public static readonly StorePropTag MailboxStatus = new StorePropTag(26277, PropertyType.Int16, new StorePropInfo("MailboxStatus", 26277, PropertyType.Int16, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E8B RID: 3723
			public static readonly StorePropTag MailboxFlags = new StorePropTag(26279, PropertyType.Int32, new StorePropInfo("MailboxFlags", 26279, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E8C RID: 3724
			public static readonly StorePropTag PreservingMailboxSignature = new StorePropTag(26280, PropertyType.Boolean, new StorePropInfo("PreservingMailboxSignature", 26280, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E8D RID: 3725
			public static readonly StorePropTag MRSPreservingMailboxSignature = new StorePropTag(26281, PropertyType.Boolean, new StorePropInfo("MRSPreservingMailboxSignature", 26281, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E8E RID: 3726
			public static readonly StorePropTag MailboxMessagesPerFolderCountWarningQuota = new StorePropTag(26283, PropertyType.Int32, new StorePropInfo("MailboxMessagesPerFolderCountWarningQuota", 26283, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5)), ObjectType.Mailbox);

			// Token: 0x04000E8F RID: 3727
			public static readonly StorePropTag MailboxMessagesPerFolderCountReceiveQuota = new StorePropTag(26284, PropertyType.Int32, new StorePropInfo("MailboxMessagesPerFolderCountReceiveQuota", 26284, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5)), ObjectType.Mailbox);

			// Token: 0x04000E90 RID: 3728
			public static readonly StorePropTag DumpsterMessagesPerFolderCountWarningQuota = new StorePropTag(26285, PropertyType.Int32, new StorePropInfo("DumpsterMessagesPerFolderCountWarningQuota", 26285, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5)), ObjectType.Mailbox);

			// Token: 0x04000E91 RID: 3729
			public static readonly StorePropTag DumpsterMessagesPerFolderCountReceiveQuota = new StorePropTag(26286, PropertyType.Int32, new StorePropInfo("DumpsterMessagesPerFolderCountReceiveQuota", 26286, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5)), ObjectType.Mailbox);

			// Token: 0x04000E92 RID: 3730
			public static readonly StorePropTag FolderHierarchyChildrenCountWarningQuota = new StorePropTag(26287, PropertyType.Int32, new StorePropInfo("FolderHierarchyChildrenCountWarningQuota", 26287, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5)), ObjectType.Mailbox);

			// Token: 0x04000E93 RID: 3731
			public static readonly StorePropTag FolderHierarchyChildrenCountReceiveQuota = new StorePropTag(26288, PropertyType.Int32, new StorePropInfo("FolderHierarchyChildrenCountReceiveQuota", 26288, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5)), ObjectType.Mailbox);

			// Token: 0x04000E94 RID: 3732
			public static readonly StorePropTag FolderHierarchyDepthWarningQuota = new StorePropTag(26289, PropertyType.Int32, new StorePropInfo("FolderHierarchyDepthWarningQuota", 26289, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5)), ObjectType.Mailbox);

			// Token: 0x04000E95 RID: 3733
			public static readonly StorePropTag FolderHierarchyDepthReceiveQuota = new StorePropTag(26290, PropertyType.Int32, new StorePropInfo("FolderHierarchyDepthReceiveQuota", 26290, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5)), ObjectType.Mailbox);

			// Token: 0x04000E96 RID: 3734
			public static readonly StorePropTag NormalMessageSize = new StorePropTag(26291, PropertyType.Int64, new StorePropInfo("NormalMessageSize", 26291, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E97 RID: 3735
			public static readonly StorePropTag NormalMessageSize32 = new StorePropTag(26291, PropertyType.Int32, new StorePropInfo("NormalMessageSize32", 26291, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E98 RID: 3736
			public static readonly StorePropTag AssociatedMessageSize = new StorePropTag(26292, PropertyType.Int64, new StorePropInfo("AssociatedMessageSize", 26292, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E99 RID: 3737
			public static readonly StorePropTag AssociatedMessageSize32 = new StorePropTag(26292, PropertyType.Int32, new StorePropInfo("AssociatedMessageSize32", 26292, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000E9A RID: 3738
			public static readonly StorePropTag FoldersCountWarningQuota = new StorePropTag(26293, PropertyType.Int32, new StorePropInfo("FoldersCountWarningQuota", 26293, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5)), ObjectType.Mailbox);

			// Token: 0x04000E9B RID: 3739
			public static readonly StorePropTag FoldersCountReceiveQuota = new StorePropTag(26294, PropertyType.Int32, new StorePropInfo("FoldersCountReceiveQuota", 26294, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5)), ObjectType.Mailbox);

			// Token: 0x04000E9C RID: 3740
			public static readonly StorePropTag NamedPropertiesCountQuota = new StorePropTag(26295, PropertyType.Int32, new StorePropInfo("NamedPropertiesCountQuota", 26295, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5)), ObjectType.Mailbox);

			// Token: 0x04000E9D RID: 3741
			public static readonly StorePropTag CodePageId = new StorePropTag(26307, PropertyType.Int32, new StorePropInfo("CodePageId", 26307, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000E9E RID: 3742
			public static readonly StorePropTag RetentionAgeLimit = new StorePropTag(26308, PropertyType.Int32, new StorePropInfo("RetentionAgeLimit", 26308, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000E9F RID: 3743
			public static readonly StorePropTag UserDisplayName = new StorePropTag(26315, PropertyType.Unicode, new StorePropInfo("UserDisplayName", 26315, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EA0 RID: 3744
			public static readonly StorePropTag SortLocaleId = new StorePropTag(26373, PropertyType.Int32, new StorePropInfo("SortLocaleId", 26373, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EA1 RID: 3745
			public static readonly StorePropTag MailboxDSGuid = new StorePropTag(26375, PropertyType.Binary, new StorePropInfo("MailboxDSGuid", 26375, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000EA2 RID: 3746
			public static readonly StorePropTag MailboxDSGuidGuid = new StorePropTag(26375, PropertyType.Guid, new StorePropInfo("MailboxDSGuidGuid", 26375, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000EA3 RID: 3747
			public static readonly StorePropTag DateDiscoveredAbsentInDS = new StorePropTag(26376, PropertyType.SysTime, new StorePropInfo("DateDiscoveredAbsentInDS", 26376, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000EA4 RID: 3748
			public static readonly StorePropTag UnifiedMailboxGuidGuid = new StorePropTag(26376, PropertyType.Guid, new StorePropInfo("UnifiedMailboxGuidGuid", 26376, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000EA5 RID: 3749
			public static readonly StorePropTag QuotaWarningThreshold = new StorePropTag(26401, PropertyType.Int32, new StorePropInfo("QuotaWarningThreshold", 26401, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EA6 RID: 3750
			public static readonly StorePropTag QuotaSendThreshold = new StorePropTag(26402, PropertyType.Int32, new StorePropInfo("QuotaSendThreshold", 26402, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EA7 RID: 3751
			public static readonly StorePropTag QuotaReceiveThreshold = new StorePropTag(26403, PropertyType.Int32, new StorePropInfo("QuotaReceiveThreshold", 26403, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EA8 RID: 3752
			public static readonly StorePropTag PropertyGroupMappingId = new StorePropTag(26420, PropertyType.Int32, new StorePropInfo("PropertyGroupMappingId", 26420, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EA9 RID: 3753
			public static readonly StorePropTag SentMailSvrEID = new StorePropTag(26432, PropertyType.SvrEid, new StorePropInfo("SentMailSvrEID", 26432, PropertyType.SvrEid, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EAA RID: 3754
			public static readonly StorePropTag SentMailSvrEIDBin = new StorePropTag(26432, PropertyType.Binary, new StorePropInfo("SentMailSvrEIDBin", 26432, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EAB RID: 3755
			public static readonly StorePropTag LocalIdNext = new StorePropTag(26465, PropertyType.Binary, new StorePropInfo("LocalIdNext", 26465, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EAC RID: 3756
			public static readonly StorePropTag RootFid = new StorePropTag(26468, PropertyType.Int64, new StorePropInfo("RootFid", 26468, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000EAD RID: 3757
			public static readonly StorePropTag FIDC = new StorePropTag(26470, PropertyType.Binary, new StorePropInfo("FIDC", 26470, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EAE RID: 3758
			public static readonly StorePropTag MdbDSGuid = new StorePropTag(26474, PropertyType.Binary, new StorePropInfo("MdbDSGuid", 26474, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000EAF RID: 3759
			public static readonly StorePropTag MailboxOwnerDN = new StorePropTag(26475, PropertyType.Unicode, new StorePropInfo("MailboxOwnerDN", 26475, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000EB0 RID: 3760
			public static readonly StorePropTag MapiEntryIdGuid = new StorePropTag(26476, PropertyType.Binary, new StorePropInfo("MapiEntryIdGuid", 26476, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EB1 RID: 3761
			public static readonly StorePropTag Localized = new StorePropTag(26477, PropertyType.Boolean, new StorePropInfo("Localized", 26477, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EB2 RID: 3762
			public static readonly StorePropTag LCID = new StorePropTag(26478, PropertyType.Int32, new StorePropInfo("LCID", 26478, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EB3 RID: 3763
			public static readonly StorePropTag AltRecipientDN = new StorePropTag(26479, PropertyType.Unicode, new StorePropInfo("AltRecipientDN", 26479, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EB4 RID: 3764
			public static readonly StorePropTag NoLocalDelivery = new StorePropTag(26480, PropertyType.Boolean, new StorePropInfo("NoLocalDelivery", 26480, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EB5 RID: 3765
			public static readonly StorePropTag DeliveryContentLength = new StorePropTag(26481, PropertyType.Int32, new StorePropInfo("DeliveryContentLength", 26481, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EB6 RID: 3766
			public static readonly StorePropTag AutoReply = new StorePropTag(26482, PropertyType.Boolean, new StorePropInfo("AutoReply", 26482, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EB7 RID: 3767
			public static readonly StorePropTag MailboxOwnerDisplayName = new StorePropTag(26483, PropertyType.Unicode, new StorePropInfo("MailboxOwnerDisplayName", 26483, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000EB8 RID: 3768
			public static readonly StorePropTag MailboxLastUpdated = new StorePropTag(26484, PropertyType.SysTime, new StorePropInfo("MailboxLastUpdated", 26484, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EB9 RID: 3769
			public static readonly StorePropTag AdminSurName = new StorePropTag(26485, PropertyType.Unicode, new StorePropInfo("AdminSurName", 26485, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EBA RID: 3770
			public static readonly StorePropTag AdminGivenName = new StorePropTag(26486, PropertyType.Unicode, new StorePropInfo("AdminGivenName", 26486, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EBB RID: 3771
			public static readonly StorePropTag ActiveSearchCount = new StorePropTag(26487, PropertyType.Int32, new StorePropInfo("ActiveSearchCount", 26487, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EBC RID: 3772
			public static readonly StorePropTag AdminNickname = new StorePropTag(26488, PropertyType.Unicode, new StorePropInfo("AdminNickname", 26488, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EBD RID: 3773
			public static readonly StorePropTag QuotaStyle = new StorePropTag(26489, PropertyType.Int32, new StorePropInfo("QuotaStyle", 26489, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EBE RID: 3774
			public static readonly StorePropTag OverQuotaLimit = new StorePropTag(26490, PropertyType.Int32, new StorePropInfo("OverQuotaLimit", 26490, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EBF RID: 3775
			public static readonly StorePropTag StorageQuota = new StorePropTag(26491, PropertyType.Int32, new StorePropInfo("StorageQuota", 26491, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EC0 RID: 3776
			public static readonly StorePropTag SubmitContentLength = new StorePropTag(26492, PropertyType.Int32, new StorePropInfo("SubmitContentLength", 26492, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EC1 RID: 3777
			public static readonly StorePropTag ReservedIdCounterRangeUpperLimit = new StorePropTag(26494, PropertyType.Int64, new StorePropInfo("ReservedIdCounterRangeUpperLimit", 26494, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EC2 RID: 3778
			public static readonly StorePropTag ReservedCnCounterRangeUpperLimit = new StorePropTag(26495, PropertyType.Int64, new StorePropInfo("ReservedCnCounterRangeUpperLimit", 26495, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EC3 RID: 3779
			public static readonly StorePropTag FolderIdsetIn = new StorePropTag(26514, PropertyType.Binary, new StorePropInfo("FolderIdsetIn", 26514, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000EC4 RID: 3780
			public static readonly StorePropTag CnsetIn = new StorePropTag(26516, PropertyType.Binary, new StorePropInfo("CnsetIn", 26516, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000EC5 RID: 3781
			public static readonly StorePropTag ShutoffQuota = new StorePropTag(26628, PropertyType.Int32, new StorePropInfo("ShutoffQuota", 26628, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EC6 RID: 3782
			public static readonly StorePropTag MailboxMiscFlags = new StorePropTag(26630, PropertyType.Int32, new StorePropInfo("MailboxMiscFlags", 26630, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 10)), ObjectType.Mailbox);

			// Token: 0x04000EC7 RID: 3783
			public static readonly StorePropTag MailboxInCreation = new StorePropTag(26635, PropertyType.Boolean, new StorePropInfo("MailboxInCreation", 26635, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EC8 RID: 3784
			public static readonly StorePropTag ObjectClassFlags = new StorePropTag(26637, PropertyType.Int32, new StorePropInfo("ObjectClassFlags", 26637, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000EC9 RID: 3785
			public static readonly StorePropTag OOFStateEx = new StorePropTag(26640, PropertyType.Int32, new StorePropInfo("OOFStateEx", 26640, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000ECA RID: 3786
			public static readonly StorePropTag OofStateUserChangeTime = new StorePropTag(26643, PropertyType.SysTime, new StorePropInfo("OofStateUserChangeTime", 26643, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000ECB RID: 3787
			public static readonly StorePropTag UserOofSettingsItemId = new StorePropTag(26644, PropertyType.Binary, new StorePropInfo("UserOofSettingsItemId", 26644, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000ECC RID: 3788
			public static readonly StorePropTag MailboxQuarantined = new StorePropTag(26650, PropertyType.Boolean, new StorePropInfo("MailboxQuarantined", 26650, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000ECD RID: 3789
			public static readonly StorePropTag MailboxQuarantineDescription = new StorePropTag(26651, PropertyType.Unicode, new StorePropInfo("MailboxQuarantineDescription", 26651, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000ECE RID: 3790
			public static readonly StorePropTag MailboxQuarantineLastCrash = new StorePropTag(26652, PropertyType.SysTime, new StorePropInfo("MailboxQuarantineLastCrash", 26652, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000ECF RID: 3791
			public static readonly StorePropTag MailboxQuarantineEnd = new StorePropTag(26653, PropertyType.SysTime, new StorePropInfo("MailboxQuarantineEnd", 26653, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000ED0 RID: 3792
			public static readonly StorePropTag MailboxNum = new StorePropTag(26655, PropertyType.Int32, new StorePropInfo("MailboxNum", 26655, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.Mailbox);

			// Token: 0x04000ED1 RID: 3793
			public static readonly StorePropTag MaxMessageSize = new StorePropTag(26669, PropertyType.Int32, new StorePropInfo("MaxMessageSize", 26669, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000ED2 RID: 3794
			public static readonly StorePropTag InferenceClientActivityFlags = new StorePropTag(26676, PropertyType.Int32, new StorePropInfo("InferenceClientActivityFlags", 26676, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000ED3 RID: 3795
			public static readonly StorePropTag InferenceOWAUserActivityLoggingEnabledDeprecated = new StorePropTag(26677, PropertyType.Boolean, new StorePropInfo("InferenceOWAUserActivityLoggingEnabledDeprecated", 26677, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000ED4 RID: 3796
			public static readonly StorePropTag InferenceOLKUserActivityLoggingEnabled = new StorePropTag(26678, PropertyType.Boolean, new StorePropInfo("InferenceOLKUserActivityLoggingEnabled", 26678, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000ED5 RID: 3797
			public static readonly StorePropTag InferenceTrainedModelVersionBreadCrumb = new StorePropTag(26739, PropertyType.Binary, new StorePropInfo("InferenceTrainedModelVersionBreadCrumb", 26739, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000ED6 RID: 3798
			public static readonly StorePropTag UserPhotoCacheId = new StorePropTag(31770, PropertyType.Int32, new StorePropInfo("UserPhotoCacheId", 31770, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000ED7 RID: 3799
			public static readonly StorePropTag UserPhotoPreviewCacheId = new StorePropTag(31771, PropertyType.Int32, new StorePropInfo("UserPhotoPreviewCacheId", 31771, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Mailbox);

			// Token: 0x04000ED8 RID: 3800
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[]
			{
				PropTag.Mailbox.ScheduledISIntegCorruptionCount,
				PropTag.Mailbox.ScheduledISIntegExecutionTime,
				PropTag.Mailbox.MailboxPartitionNumber,
				PropTag.Mailbox.InternalTenantHint,
				PropTag.Mailbox.MaintenanceId
			};

			// Token: 0x04000ED9 RID: 3801
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[]
			{
				PropTag.Mailbox.ScheduledISIntegCorruptionCount,
				PropTag.Mailbox.ScheduledISIntegExecutionTime,
				PropTag.Mailbox.MailboxPartitionNumber,
				PropTag.Mailbox.InternalTenantHint,
				PropTag.Mailbox.MaintenanceId,
				PropTag.Mailbox.RulesSize
			};

			// Token: 0x04000EDA RID: 3802
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[0];

			// Token: 0x04000EDB RID: 3803
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[]
			{
				PropTag.Mailbox.MessageSize,
				PropTag.Mailbox.MessageSize32,
				PropTag.Mailbox.HighestFolderInternetId,
				PropTag.Mailbox.CISearchEnabled,
				PropTag.Mailbox.Access,
				PropTag.Mailbox.DisplayName,
				PropTag.Mailbox.EmailAddress,
				PropTag.Mailbox.Comment,
				PropTag.Mailbox.CreationTime,
				PropTag.Mailbox.MessageTableTotalPages,
				PropTag.Mailbox.MessageTableAvailablePages,
				PropTag.Mailbox.OtherTablesTotalPages,
				PropTag.Mailbox.OtherTablesAvailablePages,
				PropTag.Mailbox.AttachmentTableTotalPages,
				PropTag.Mailbox.AttachmentTableAvailablePages,
				PropTag.Mailbox.MailboxTypeVersion,
				PropTag.Mailbox.MailboxPartitionMailboxGuids,
				PropTag.Mailbox.StoreState,
				PropTag.Mailbox.LocalDirectoryEntryID,
				PropTag.Mailbox.ContentCount,
				PropTag.Mailbox.ContentCountInt64,
				PropTag.Mailbox.AssociatedContentCount,
				PropTag.Mailbox.AssociatedContentCountInt64,
				PropTag.Mailbox.SimpleDisplayName,
				PropTag.Mailbox.ScheduledISIntegCorruptionCount,
				PropTag.Mailbox.ScheduledISIntegExecutionTime,
				PropTag.Mailbox.MailboxPartitionNumber,
				PropTag.Mailbox.MailboxTypeDetail,
				PropTag.Mailbox.InternalTenantHint,
				PropTag.Mailbox.TenantHint,
				PropTag.Mailbox.MaintenanceId,
				PropTag.Mailbox.MailboxType,
				PropTag.Mailbox.StorageQuotaLimit,
				PropTag.Mailbox.RulesSize,
				PropTag.Mailbox.InTransitState,
				PropTag.Mailbox.InTransitStatus,
				PropTag.Mailbox.UserEntryId,
				PropTag.Mailbox.UserName,
				PropTag.Mailbox.MailboxOwnerEntryId,
				PropTag.Mailbox.MailboxOwnerName,
				PropTag.Mailbox.SerializedReplidGuidMap,
				PropTag.Mailbox.DeletedMsgCount,
				PropTag.Mailbox.DeletedMsgCountInt64,
				PropTag.Mailbox.DeletedAssocMsgCount,
				PropTag.Mailbox.DeletedAssocMsgCountInt64,
				PropTag.Mailbox.HasNamedProperties,
				PropTag.Mailbox.ProhibitReceiveQuota,
				PropTag.Mailbox.MaxSubmitMessageSize,
				PropTag.Mailbox.ProhibitSendQuota,
				PropTag.Mailbox.DeletedOn,
				PropTag.Mailbox.DeletedMessageSize,
				PropTag.Mailbox.DeletedMessageSize32,
				PropTag.Mailbox.DeletedNormalMessageSize,
				PropTag.Mailbox.DeletedNormalMessageSize32,
				PropTag.Mailbox.DeletedAssociatedMessageSize,
				PropTag.Mailbox.DeletedAssociatedMessageSize32,
				PropTag.Mailbox.LastLogonTime,
				PropTag.Mailbox.LastLogoffTime,
				PropTag.Mailbox.InternetMdns,
				PropTag.Mailbox.MailboxStatus,
				PropTag.Mailbox.PreservingMailboxSignature,
				PropTag.Mailbox.MRSPreservingMailboxSignature,
				PropTag.Mailbox.MailboxMessagesPerFolderCountWarningQuota,
				PropTag.Mailbox.MailboxMessagesPerFolderCountReceiveQuota,
				PropTag.Mailbox.DumpsterMessagesPerFolderCountWarningQuota,
				PropTag.Mailbox.DumpsterMessagesPerFolderCountReceiveQuota,
				PropTag.Mailbox.FolderHierarchyChildrenCountWarningQuota,
				PropTag.Mailbox.FolderHierarchyChildrenCountReceiveQuota,
				PropTag.Mailbox.FolderHierarchyDepthWarningQuota,
				PropTag.Mailbox.FolderHierarchyDepthReceiveQuota,
				PropTag.Mailbox.NormalMessageSize,
				PropTag.Mailbox.NormalMessageSize32,
				PropTag.Mailbox.AssociatedMessageSize,
				PropTag.Mailbox.AssociatedMessageSize32,
				PropTag.Mailbox.FoldersCountWarningQuota,
				PropTag.Mailbox.FoldersCountReceiveQuota,
				PropTag.Mailbox.NamedPropertiesCountQuota,
				PropTag.Mailbox.CodePageId,
				PropTag.Mailbox.MailboxDSGuid,
				PropTag.Mailbox.MailboxDSGuidGuid,
				PropTag.Mailbox.DateDiscoveredAbsentInDS,
				PropTag.Mailbox.UnifiedMailboxGuidGuid,
				PropTag.Mailbox.RootFid,
				PropTag.Mailbox.MdbDSGuid,
				PropTag.Mailbox.MailboxOwnerDN,
				PropTag.Mailbox.MailboxOwnerDisplayName,
				PropTag.Mailbox.FolderIdsetIn,
				PropTag.Mailbox.CnsetIn,
				PropTag.Mailbox.MailboxMiscFlags,
				PropTag.Mailbox.MailboxNum
			};

			// Token: 0x04000EDC RID: 3804
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[]
			{
				PropTag.Mailbox.LastLogonTime,
				PropTag.Mailbox.LastLogoffTime
			};

			// Token: 0x04000EDD RID: 3805
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[]
			{
				PropTag.Mailbox.LocalDirectoryEntryID,
				PropTag.Mailbox.InTransitStatus,
				PropTag.Mailbox.MailboxMessagesPerFolderCountWarningQuota,
				PropTag.Mailbox.MailboxMessagesPerFolderCountReceiveQuota,
				PropTag.Mailbox.DumpsterMessagesPerFolderCountWarningQuota,
				PropTag.Mailbox.DumpsterMessagesPerFolderCountReceiveQuota,
				PropTag.Mailbox.FolderHierarchyChildrenCountWarningQuota,
				PropTag.Mailbox.FolderHierarchyChildrenCountReceiveQuota,
				PropTag.Mailbox.FolderHierarchyDepthWarningQuota,
				PropTag.Mailbox.FolderHierarchyDepthReceiveQuota,
				PropTag.Mailbox.FoldersCountWarningQuota,
				PropTag.Mailbox.FoldersCountReceiveQuota,
				PropTag.Mailbox.NamedPropertiesCountQuota
			};

			// Token: 0x04000EDE RID: 3806
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[0];

			// Token: 0x04000EDF RID: 3807
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x04000EE0 RID: 3808
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x04000EE1 RID: 3809
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[0];

			// Token: 0x04000EE2 RID: 3810
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[]
			{
				PropTag.Mailbox.MessageSize,
				PropTag.Mailbox.MessageSize32,
				PropTag.Mailbox.Access,
				PropTag.Mailbox.MailboxPartitionMailboxGuids,
				PropTag.Mailbox.StoreState,
				PropTag.Mailbox.ContentCount,
				PropTag.Mailbox.ContentCountInt64,
				PropTag.Mailbox.AssociatedContentCount,
				PropTag.Mailbox.AssociatedContentCountInt64,
				PropTag.Mailbox.SerializedReplidGuidMap,
				PropTag.Mailbox.DeletedMsgCount,
				PropTag.Mailbox.DeletedMsgCountInt64,
				PropTag.Mailbox.DeletedAssocMsgCount,
				PropTag.Mailbox.DeletedAssocMsgCountInt64,
				PropTag.Mailbox.HasNamedProperties,
				PropTag.Mailbox.DeletedOn,
				PropTag.Mailbox.DeletedMessageSize,
				PropTag.Mailbox.DeletedMessageSize32,
				PropTag.Mailbox.DeletedNormalMessageSize,
				PropTag.Mailbox.DeletedNormalMessageSize32,
				PropTag.Mailbox.DeletedAssociatedMessageSize,
				PropTag.Mailbox.DeletedAssociatedMessageSize32,
				PropTag.Mailbox.NormalMessageSize,
				PropTag.Mailbox.NormalMessageSize32,
				PropTag.Mailbox.AssociatedMessageSize,
				PropTag.Mailbox.AssociatedMessageSize32,
				PropTag.Mailbox.MailboxMiscFlags
			};

			// Token: 0x04000EE3 RID: 3811
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x04000EE4 RID: 3812
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x04000EE5 RID: 3813
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x04000EE6 RID: 3814
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x04000EE7 RID: 3815
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x04000EE8 RID: 3816
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x04000EE9 RID: 3817
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x04000EEA RID: 3818
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x04000EEB RID: 3819
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.Mailbox.DeleteAfterSubmit,
				PropTag.Mailbox.MessageSize,
				PropTag.Mailbox.MessageSize32,
				PropTag.Mailbox.SentMailEntryId,
				PropTag.Mailbox.HighestFolderInternetId,
				PropTag.Mailbox.NTSecurityDescriptor,
				PropTag.Mailbox.CISearchEnabled,
				PropTag.Mailbox.ExtendedRuleSizeLimit,
				PropTag.Mailbox.Access,
				PropTag.Mailbox.MappingSignature,
				PropTag.Mailbox.StoreRecordKey,
				PropTag.Mailbox.StoreEntryId,
				PropTag.Mailbox.DisplayName,
				PropTag.Mailbox.EmailAddress,
				PropTag.Mailbox.Comment,
				PropTag.Mailbox.CreationTime,
				PropTag.Mailbox.LastModificationTime,
				PropTag.Mailbox.ResourceFlags,
				PropTag.Mailbox.MessageTableTotalPages,
				PropTag.Mailbox.MessageTableAvailablePages,
				PropTag.Mailbox.OtherTablesTotalPages,
				PropTag.Mailbox.OtherTablesAvailablePages,
				PropTag.Mailbox.AttachmentTableTotalPages,
				PropTag.Mailbox.AttachmentTableAvailablePages,
				PropTag.Mailbox.MailboxTypeVersion,
				PropTag.Mailbox.MailboxPartitionMailboxGuids,
				PropTag.Mailbox.StoreSupportMask,
				PropTag.Mailbox.StoreState,
				PropTag.Mailbox.IPMSubtreeSearchKey,
				PropTag.Mailbox.IPMOutboxSearchKey,
				PropTag.Mailbox.IPMWastebasketSearchKey,
				PropTag.Mailbox.IPMSentmailSearchKey,
				PropTag.Mailbox.MdbProvider,
				PropTag.Mailbox.ReceiveFolderSettings,
				PropTag.Mailbox.LocalDirectoryEntryID,
				PropTag.Mailbox.ControlDataForCalendarRepairAssistant,
				PropTag.Mailbox.ControlDataForSharingPolicyAssistant,
				PropTag.Mailbox.ControlDataForElcAssistant,
				PropTag.Mailbox.ControlDataForTopNWordsAssistant,
				PropTag.Mailbox.ControlDataForJunkEmailAssistant,
				PropTag.Mailbox.ControlDataForCalendarSyncAssistant,
				PropTag.Mailbox.ExternalSharingCalendarSubscriptionCount,
				PropTag.Mailbox.ControlDataForUMReportingAssistant,
				PropTag.Mailbox.HasUMReportData,
				PropTag.Mailbox.InternetCalendarSubscriptionCount,
				PropTag.Mailbox.ExternalSharingContactSubscriptionCount,
				PropTag.Mailbox.JunkEmailSafeListDirty,
				PropTag.Mailbox.IsTopNEnabled,
				PropTag.Mailbox.LastSharingPolicyAppliedId,
				PropTag.Mailbox.LastSharingPolicyAppliedHash,
				PropTag.Mailbox.LastSharingPolicyAppliedTime,
				PropTag.Mailbox.OofScheduleStart,
				PropTag.Mailbox.OofScheduleEnd,
				PropTag.Mailbox.ControlDataForDirectoryProcessorAssistant,
				PropTag.Mailbox.NeedsDirectoryProcessor,
				PropTag.Mailbox.RetentionQueryIds,
				PropTag.Mailbox.RetentionQueryInfo,
				PropTag.Mailbox.ControlDataForPublicFolderAssistant,
				PropTag.Mailbox.ControlDataForInferenceTrainingAssistant,
				PropTag.Mailbox.InferenceEnabled,
				PropTag.Mailbox.ControlDataForContactLinkingAssistant,
				PropTag.Mailbox.ContactLinking,
				PropTag.Mailbox.ControlDataForOABGeneratorAssistant,
				PropTag.Mailbox.ContactSaveVersion,
				PropTag.Mailbox.ControlDataForOrgContactsSyncAssistant,
				PropTag.Mailbox.OrgContactsSyncTimestamp,
				PropTag.Mailbox.PushNotificationSubscriptionType,
				PropTag.Mailbox.OrgContactsSyncADWatermark,
				PropTag.Mailbox.ControlDataForInferenceDataCollectionAssistant,
				PropTag.Mailbox.InferenceDataCollectionProcessingState,
				PropTag.Mailbox.ControlDataForPeopleRelevanceAssistant,
				PropTag.Mailbox.SiteMailboxInternalState,
				PropTag.Mailbox.ControlDataForSiteMailboxAssistant,
				PropTag.Mailbox.InferenceTrainingLastContentCount,
				PropTag.Mailbox.InferenceTrainingLastAttemptTimestamp,
				PropTag.Mailbox.InferenceTrainingLastSuccessTimestamp,
				PropTag.Mailbox.InferenceUserCapabilityFlags,
				PropTag.Mailbox.ControlDataForMailboxAssociationReplicationAssistant,
				PropTag.Mailbox.MailboxAssociationNextReplicationTime,
				PropTag.Mailbox.MailboxAssociationProcessingFlags,
				PropTag.Mailbox.ControlDataForSharePointSignalStoreAssistant,
				PropTag.Mailbox.ControlDataForPeopleCentricTriageAssistant,
				PropTag.Mailbox.NotificationBrokerSubscriptions,
				PropTag.Mailbox.GroupMailboxPermissionsVersion,
				PropTag.Mailbox.ElcLastRunTotalProcessingTime,
				PropTag.Mailbox.ElcLastRunSubAssistantProcessingTime,
				PropTag.Mailbox.ElcLastRunUpdatedFolderCount,
				PropTag.Mailbox.ElcLastRunTaggedFolderCount,
				PropTag.Mailbox.ElcLastRunUpdatedItemCount,
				PropTag.Mailbox.ElcLastRunTaggedWithArchiveItemCount,
				PropTag.Mailbox.ElcLastRunTaggedWithExpiryItemCount,
				PropTag.Mailbox.ElcLastRunDeletedFromRootItemCount,
				PropTag.Mailbox.ElcLastRunDeletedFromDumpsterItemCount,
				PropTag.Mailbox.ElcLastRunArchivedFromRootItemCount,
				PropTag.Mailbox.ElcLastRunArchivedFromDumpsterItemCount,
				PropTag.Mailbox.ScheduledISIntegLastFinished,
				PropTag.Mailbox.ControlDataForSearchIndexRepairAssistant,
				PropTag.Mailbox.ELCLastSuccessTimestamp,
				PropTag.Mailbox.InferenceTruthLoggingLastAttemptTimestamp,
				PropTag.Mailbox.InferenceTruthLoggingLastSuccessTimestamp,
				PropTag.Mailbox.ControlDataForGroupMailboxAssistant,
				PropTag.Mailbox.ItemsPendingUpgrade,
				PropTag.Mailbox.ConsumerSharingCalendarSubscriptionCount,
				PropTag.Mailbox.GroupMailboxGeneratedPhotoVersion,
				PropTag.Mailbox.GroupMailboxGeneratedPhotoSignature,
				PropTag.Mailbox.GroupMailboxExchangeResourcesPublishedVersion,
				PropTag.Mailbox.ValidFolderMask,
				PropTag.Mailbox.IPMSubtreeEntryId,
				PropTag.Mailbox.IPMOutboxEntryId,
				PropTag.Mailbox.IPMWastebasketEntryId,
				PropTag.Mailbox.IPMSentmailEntryId,
				PropTag.Mailbox.IPMViewsEntryId,
				PropTag.Mailbox.UnsearchableItems,
				PropTag.Mailbox.IPMFinderEntryId,
				PropTag.Mailbox.ContentCount,
				PropTag.Mailbox.ContentCountInt64,
				PropTag.Mailbox.Search,
				PropTag.Mailbox.AssociatedContentCount,
				PropTag.Mailbox.AssociatedContentCountInt64,
				PropTag.Mailbox.AdditionalRENEntryIds,
				PropTag.Mailbox.SimpleDisplayName,
				PropTag.Mailbox.TestBlobProperty,
				PropTag.Mailbox.ScheduledISIntegCorruptionCount,
				PropTag.Mailbox.ScheduledISIntegExecutionTime,
				PropTag.Mailbox.MailboxPartitionNumber,
				PropTag.Mailbox.MailboxTypeDetail,
				PropTag.Mailbox.InternalTenantHint,
				PropTag.Mailbox.TenantHint,
				PropTag.Mailbox.MaintenanceId,
				PropTag.Mailbox.MailboxType,
				PropTag.Mailbox.ACLData,
				PropTag.Mailbox.DesignInProgress,
				PropTag.Mailbox.StorageQuotaLimit,
				PropTag.Mailbox.RulesSize,
				PropTag.Mailbox.IMAPSubscribeList,
				PropTag.Mailbox.InTransitState,
				PropTag.Mailbox.InTransitStatus,
				PropTag.Mailbox.UserEntryId,
				PropTag.Mailbox.UserName,
				PropTag.Mailbox.MailboxOwnerEntryId,
				PropTag.Mailbox.MailboxOwnerName,
				PropTag.Mailbox.OofState,
				PropTag.Mailbox.TestLineSpeed,
				PropTag.Mailbox.SerializedReplidGuidMap,
				PropTag.Mailbox.DeletedMsgCount,
				PropTag.Mailbox.DeletedMsgCountInt64,
				PropTag.Mailbox.DeletedAssocMsgCount,
				PropTag.Mailbox.DeletedAssocMsgCountInt64,
				PropTag.Mailbox.HasNamedProperties,
				PropTag.Mailbox.ActiveUserEntryId,
				PropTag.Mailbox.ProhibitReceiveQuota,
				PropTag.Mailbox.MaxSubmitMessageSize,
				PropTag.Mailbox.ProhibitSendQuota,
				PropTag.Mailbox.DeletedOn,
				PropTag.Mailbox.MailboxDatabaseVersion,
				PropTag.Mailbox.DeletedMessageSize,
				PropTag.Mailbox.DeletedMessageSize32,
				PropTag.Mailbox.DeletedNormalMessageSize,
				PropTag.Mailbox.DeletedNormalMessageSize32,
				PropTag.Mailbox.DeletedAssociatedMessageSize,
				PropTag.Mailbox.DeletedAssociatedMessageSize32,
				PropTag.Mailbox.NTUsername,
				PropTag.Mailbox.NTUserSid,
				PropTag.Mailbox.LocaleId,
				PropTag.Mailbox.LastLogonTime,
				PropTag.Mailbox.LastLogoffTime,
				PropTag.Mailbox.StorageLimitInformation,
				PropTag.Mailbox.InternetMdns,
				PropTag.Mailbox.MailboxStatus,
				PropTag.Mailbox.MailboxFlags,
				PropTag.Mailbox.PreservingMailboxSignature,
				PropTag.Mailbox.MRSPreservingMailboxSignature,
				PropTag.Mailbox.MailboxMessagesPerFolderCountWarningQuota,
				PropTag.Mailbox.MailboxMessagesPerFolderCountReceiveQuota,
				PropTag.Mailbox.DumpsterMessagesPerFolderCountWarningQuota,
				PropTag.Mailbox.DumpsterMessagesPerFolderCountReceiveQuota,
				PropTag.Mailbox.FolderHierarchyChildrenCountWarningQuota,
				PropTag.Mailbox.FolderHierarchyChildrenCountReceiveQuota,
				PropTag.Mailbox.FolderHierarchyDepthWarningQuota,
				PropTag.Mailbox.FolderHierarchyDepthReceiveQuota,
				PropTag.Mailbox.NormalMessageSize,
				PropTag.Mailbox.NormalMessageSize32,
				PropTag.Mailbox.AssociatedMessageSize,
				PropTag.Mailbox.AssociatedMessageSize32,
				PropTag.Mailbox.FoldersCountWarningQuota,
				PropTag.Mailbox.FoldersCountReceiveQuota,
				PropTag.Mailbox.NamedPropertiesCountQuota,
				PropTag.Mailbox.CodePageId,
				PropTag.Mailbox.RetentionAgeLimit,
				PropTag.Mailbox.UserDisplayName,
				PropTag.Mailbox.SortLocaleId,
				PropTag.Mailbox.MailboxDSGuid,
				PropTag.Mailbox.MailboxDSGuidGuid,
				PropTag.Mailbox.DateDiscoveredAbsentInDS,
				PropTag.Mailbox.UnifiedMailboxGuidGuid,
				PropTag.Mailbox.QuotaWarningThreshold,
				PropTag.Mailbox.QuotaSendThreshold,
				PropTag.Mailbox.QuotaReceiveThreshold,
				PropTag.Mailbox.PropertyGroupMappingId,
				PropTag.Mailbox.SentMailSvrEID,
				PropTag.Mailbox.SentMailSvrEIDBin,
				PropTag.Mailbox.LocalIdNext,
				PropTag.Mailbox.RootFid,
				PropTag.Mailbox.FIDC,
				PropTag.Mailbox.MdbDSGuid,
				PropTag.Mailbox.MailboxOwnerDN,
				PropTag.Mailbox.MapiEntryIdGuid,
				PropTag.Mailbox.Localized,
				PropTag.Mailbox.LCID,
				PropTag.Mailbox.AltRecipientDN,
				PropTag.Mailbox.NoLocalDelivery,
				PropTag.Mailbox.DeliveryContentLength,
				PropTag.Mailbox.AutoReply,
				PropTag.Mailbox.MailboxOwnerDisplayName,
				PropTag.Mailbox.MailboxLastUpdated,
				PropTag.Mailbox.AdminSurName,
				PropTag.Mailbox.AdminGivenName,
				PropTag.Mailbox.ActiveSearchCount,
				PropTag.Mailbox.AdminNickname,
				PropTag.Mailbox.QuotaStyle,
				PropTag.Mailbox.OverQuotaLimit,
				PropTag.Mailbox.StorageQuota,
				PropTag.Mailbox.SubmitContentLength,
				PropTag.Mailbox.ReservedIdCounterRangeUpperLimit,
				PropTag.Mailbox.ReservedCnCounterRangeUpperLimit,
				PropTag.Mailbox.FolderIdsetIn,
				PropTag.Mailbox.CnsetIn,
				PropTag.Mailbox.ShutoffQuota,
				PropTag.Mailbox.MailboxMiscFlags,
				PropTag.Mailbox.MailboxInCreation,
				PropTag.Mailbox.ObjectClassFlags,
				PropTag.Mailbox.OOFStateEx,
				PropTag.Mailbox.OofStateUserChangeTime,
				PropTag.Mailbox.UserOofSettingsItemId,
				PropTag.Mailbox.MailboxQuarantined,
				PropTag.Mailbox.MailboxQuarantineDescription,
				PropTag.Mailbox.MailboxQuarantineLastCrash,
				PropTag.Mailbox.MailboxQuarantineEnd,
				PropTag.Mailbox.MailboxNum,
				PropTag.Mailbox.MaxMessageSize,
				PropTag.Mailbox.InferenceClientActivityFlags,
				PropTag.Mailbox.InferenceOWAUserActivityLoggingEnabledDeprecated,
				PropTag.Mailbox.InferenceOLKUserActivityLoggingEnabled,
				PropTag.Mailbox.InferenceTrainedModelVersionBreadCrumb,
				PropTag.Mailbox.UserPhotoCacheId,
				PropTag.Mailbox.UserPhotoPreviewCacheId
			};
		}

		// Token: 0x02000007 RID: 7
		public static class Folder
		{
			// Token: 0x04000EEC RID: 3820
			public static readonly StorePropTag MessageClass = new StorePropTag(26, PropertyType.Unicode, new StorePropInfo("MessageClass", 26, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EED RID: 3821
			public static readonly StorePropTag MessageSize = new StorePropTag(3592, PropertyType.Int64, new StorePropInfo("MessageSize", 3592, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EEE RID: 3822
			public static readonly StorePropTag MessageSize32 = new StorePropTag(3592, PropertyType.Int32, new StorePropInfo("MessageSize32", 3592, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EEF RID: 3823
			public static readonly StorePropTag ParentEntryId = new StorePropTag(3593, PropertyType.Binary, new StorePropInfo("ParentEntryId", 3593, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EF0 RID: 3824
			public static readonly StorePropTag ParentEntryIdSvrEid = new StorePropTag(3593, PropertyType.SvrEid, new StorePropInfo("ParentEntryIdSvrEid", 3593, PropertyType.SvrEid, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EF1 RID: 3825
			public static readonly StorePropTag SentMailEntryId = new StorePropTag(3594, PropertyType.Binary, new StorePropInfo("SentMailEntryId", 3594, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EF2 RID: 3826
			public static readonly StorePropTag MessageDownloadTime = new StorePropTag(3608, PropertyType.Int32, new StorePropInfo("MessageDownloadTime", 3608, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EF3 RID: 3827
			public static readonly StorePropTag FolderInternetId = new StorePropTag(3619, PropertyType.Int32, new StorePropInfo("FolderInternetId", 3619, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EF4 RID: 3828
			public static readonly StorePropTag NTSecurityDescriptor = new StorePropTag(3623, PropertyType.Binary, new StorePropInfo("NTSecurityDescriptor", 3623, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EF5 RID: 3829
			public static readonly StorePropTag AclTableAndSecurityDescriptor = new StorePropTag(3647, PropertyType.Binary, new StorePropInfo("AclTableAndSecurityDescriptor", 3647, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(9, 17)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EF6 RID: 3830
			public static readonly StorePropTag CreatorSID = new StorePropTag(3672, PropertyType.Binary, new StorePropInfo("CreatorSID", 3672, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9, 11)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EF7 RID: 3831
			public static readonly StorePropTag LastModifierSid = new StorePropTag(3673, PropertyType.Binary, new StorePropInfo("LastModifierSid", 3673, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EF8 RID: 3832
			public static readonly StorePropTag Catalog = new StorePropTag(3675, PropertyType.Binary, new StorePropInfo("Catalog", 3675, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EF9 RID: 3833
			public static readonly StorePropTag CISearchEnabled = new StorePropTag(3676, PropertyType.Boolean, new StorePropInfo("CISearchEnabled", 3676, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EFA RID: 3834
			public static readonly StorePropTag CINotificationEnabled = new StorePropTag(3677, PropertyType.Boolean, new StorePropInfo("CINotificationEnabled", 3677, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EFB RID: 3835
			public static readonly StorePropTag MaxIndices = new StorePropTag(3678, PropertyType.Int32, new StorePropInfo("MaxIndices", 3678, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EFC RID: 3836
			public static readonly StorePropTag SourceFid = new StorePropTag(3679, PropertyType.Int64, new StorePropInfo("SourceFid", 3679, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EFD RID: 3837
			public static readonly StorePropTag PFContactsGuid = new StorePropTag(3680, PropertyType.Binary, new StorePropInfo("PFContactsGuid", 3680, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EFE RID: 3838
			public static readonly StorePropTag SubfolderCount = new StorePropTag(3683, PropertyType.Int32, new StorePropInfo("SubfolderCount", 3683, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000EFF RID: 3839
			public static readonly StorePropTag DeletedSubfolderCt = new StorePropTag(3684, PropertyType.Int32, new StorePropInfo("DeletedSubfolderCt", 3684, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F00 RID: 3840
			public static readonly StorePropTag MaxCachedViews = new StorePropTag(3688, PropertyType.Int32, new StorePropInfo("MaxCachedViews", 3688, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F01 RID: 3841
			public static readonly StorePropTag NTSecurityDescriptorAsXML = new StorePropTag(3690, PropertyType.Unicode, new StorePropInfo("NTSecurityDescriptorAsXML", 3690, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F02 RID: 3842
			public static readonly StorePropTag AdminNTSecurityDescriptorAsXML = new StorePropTag(3691, PropertyType.Unicode, new StorePropInfo("AdminNTSecurityDescriptorAsXML", 3691, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F03 RID: 3843
			public static readonly StorePropTag CreatorSidAsXML = new StorePropTag(3692, PropertyType.Unicode, new StorePropInfo("CreatorSidAsXML", 3692, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F04 RID: 3844
			public static readonly StorePropTag LastModifierSidAsXML = new StorePropTag(3693, PropertyType.Unicode, new StorePropInfo("LastModifierSidAsXML", 3693, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F05 RID: 3845
			public static readonly StorePropTag MergeMidsetDeleted = new StorePropTag(3706, PropertyType.Binary, new StorePropInfo("MergeMidsetDeleted", 3706, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F06 RID: 3846
			public static readonly StorePropTag ReserveRangeOfIDs = new StorePropTag(3707, PropertyType.Binary, new StorePropInfo("ReserveRangeOfIDs", 3707, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F07 RID: 3847
			public static readonly StorePropTag FreeBusyNTSD = new StorePropTag(3840, PropertyType.Binary, new StorePropInfo("FreeBusyNTSD", 3840, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F08 RID: 3848
			public static readonly StorePropTag Access = new StorePropTag(4084, PropertyType.Int32, new StorePropInfo("Access", 4084, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F09 RID: 3849
			public static readonly StorePropTag InstanceKey = new StorePropTag(4086, PropertyType.Binary, new StorePropInfo("InstanceKey", 4086, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F0A RID: 3850
			public static readonly StorePropTag InstanceKeySvrEid = new StorePropTag(4086, PropertyType.SvrEid, new StorePropInfo("InstanceKeySvrEid", 4086, PropertyType.SvrEid, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F0B RID: 3851
			public static readonly StorePropTag AccessLevel = new StorePropTag(4087, PropertyType.Int32, new StorePropInfo("AccessLevel", 4087, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F0C RID: 3852
			public static readonly StorePropTag MappingSignature = new StorePropTag(4088, PropertyType.Binary, new StorePropInfo("MappingSignature", 4088, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F0D RID: 3853
			public static readonly StorePropTag RecordKey = new StorePropTag(4089, PropertyType.Binary, new StorePropInfo("RecordKey", 4089, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F0E RID: 3854
			public static readonly StorePropTag RecordKeySvrEid = new StorePropTag(4089, PropertyType.SvrEid, new StorePropInfo("RecordKeySvrEid", 4089, PropertyType.SvrEid, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F0F RID: 3855
			public static readonly StorePropTag StoreRecordKey = new StorePropTag(4090, PropertyType.Binary, new StorePropInfo("StoreRecordKey", 4090, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F10 RID: 3856
			public static readonly StorePropTag StoreEntryId = new StorePropTag(4091, PropertyType.Binary, new StorePropInfo("StoreEntryId", 4091, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F11 RID: 3857
			public static readonly StorePropTag ObjectType = new StorePropTag(4094, PropertyType.Int32, new StorePropInfo("ObjectType", 4094, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F12 RID: 3858
			public static readonly StorePropTag EntryId = new StorePropTag(4095, PropertyType.Binary, new StorePropInfo("EntryId", 4095, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F13 RID: 3859
			public static readonly StorePropTag EntryIdSvrEid = new StorePropTag(4095, PropertyType.SvrEid, new StorePropInfo("EntryIdSvrEid", 4095, PropertyType.SvrEid, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F14 RID: 3860
			public static readonly StorePropTag URLCompName = new StorePropTag(4339, PropertyType.Unicode, new StorePropInfo("URLCompName", 4339, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F15 RID: 3861
			public static readonly StorePropTag AttrHidden = new StorePropTag(4340, PropertyType.Boolean, new StorePropInfo("AttrHidden", 4340, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F16 RID: 3862
			public static readonly StorePropTag AttrSystem = new StorePropTag(4341, PropertyType.Boolean, new StorePropInfo("AttrSystem", 4341, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F17 RID: 3863
			public static readonly StorePropTag AttrReadOnly = new StorePropTag(4342, PropertyType.Boolean, new StorePropInfo("AttrReadOnly", 4342, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F18 RID: 3864
			public static readonly StorePropTag DisplayName = new StorePropTag(12289, PropertyType.Unicode, new StorePropInfo("DisplayName", 12289, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F19 RID: 3865
			public static readonly StorePropTag EmailAddress = new StorePropTag(12291, PropertyType.Unicode, new StorePropInfo("EmailAddress", 12291, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F1A RID: 3866
			public static readonly StorePropTag Comment = new StorePropTag(12292, PropertyType.Unicode, new StorePropInfo("Comment", 12292, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F1B RID: 3867
			public static readonly StorePropTag Depth = new StorePropTag(12293, PropertyType.Int32, new StorePropInfo("Depth", 12293, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F1C RID: 3868
			public static readonly StorePropTag CreationTime = new StorePropTag(12295, PropertyType.SysTime, new StorePropInfo("CreationTime", 12295, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9, 11)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F1D RID: 3869
			public static readonly StorePropTag LastModificationTime = new StorePropTag(12296, PropertyType.SysTime, new StorePropInfo("LastModificationTime", 12296, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9, 11)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F1E RID: 3870
			public static readonly StorePropTag StoreSupportMask = new StorePropTag(13325, PropertyType.Int32, new StorePropInfo("StoreSupportMask", 13325, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F1F RID: 3871
			public static readonly StorePropTag IPMWastebasketEntryId = new StorePropTag(13795, PropertyType.Binary, new StorePropInfo("IPMWastebasketEntryId", 13795, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F20 RID: 3872
			public static readonly StorePropTag IPMCommonViewsEntryId = new StorePropTag(13798, PropertyType.Binary, new StorePropInfo("IPMCommonViewsEntryId", 13798, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F21 RID: 3873
			public static readonly StorePropTag IPMConversationsEntryId = new StorePropTag(13804, PropertyType.Binary, new StorePropInfo("IPMConversationsEntryId", 13804, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F22 RID: 3874
			public static readonly StorePropTag IPMAllItemsEntryId = new StorePropTag(13806, PropertyType.Binary, new StorePropInfo("IPMAllItemsEntryId", 13806, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F23 RID: 3875
			public static readonly StorePropTag IPMSharingEntryId = new StorePropTag(13807, PropertyType.Binary, new StorePropInfo("IPMSharingEntryId", 13807, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F24 RID: 3876
			public static readonly StorePropTag AdminDataEntryId = new StorePropTag(13821, PropertyType.Binary, new StorePropInfo("AdminDataEntryId", 13821, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F25 RID: 3877
			public static readonly StorePropTag FolderType = new StorePropTag(13825, PropertyType.Int32, new StorePropInfo("FolderType", 13825, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F26 RID: 3878
			public static readonly StorePropTag ContentCount = new StorePropTag(13826, PropertyType.Int32, new StorePropInfo("ContentCount", 13826, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F27 RID: 3879
			public static readonly StorePropTag ContentCountInt64 = new StorePropTag(13826, PropertyType.Int64, new StorePropInfo("ContentCountInt64", 13826, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F28 RID: 3880
			public static readonly StorePropTag UnreadCount = new StorePropTag(13827, PropertyType.Int32, new StorePropInfo("UnreadCount", 13827, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F29 RID: 3881
			public static readonly StorePropTag UnreadCountInt64 = new StorePropTag(13827, PropertyType.Int64, new StorePropInfo("UnreadCountInt64", 13827, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F2A RID: 3882
			public static readonly StorePropTag Subfolders = new StorePropTag(13834, PropertyType.Boolean, new StorePropInfo("Subfolders", 13834, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F2B RID: 3883
			public static readonly StorePropTag FolderStatus = new StorePropTag(13835, PropertyType.Int32, new StorePropInfo("FolderStatus", 13835, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F2C RID: 3884
			public static readonly StorePropTag ContentsSortOrder = new StorePropTag(13837, PropertyType.MVInt32, new StorePropInfo("ContentsSortOrder", 13837, PropertyType.MVInt32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F2D RID: 3885
			public static readonly StorePropTag ContainerHierarchy = new StorePropTag(13838, PropertyType.Object, new StorePropInfo("ContainerHierarchy", 13838, PropertyType.Object, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F2E RID: 3886
			public static readonly StorePropTag ContainerContents = new StorePropTag(13839, PropertyType.Object, new StorePropInfo("ContainerContents", 13839, PropertyType.Object, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F2F RID: 3887
			public static readonly StorePropTag FolderAssociatedContents = new StorePropTag(13840, PropertyType.Object, new StorePropInfo("FolderAssociatedContents", 13840, PropertyType.Object, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F30 RID: 3888
			public static readonly StorePropTag ContainerClass = new StorePropTag(13843, PropertyType.Unicode, new StorePropInfo("ContainerClass", 13843, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F31 RID: 3889
			public static readonly StorePropTag ContainerModifyVersion = new StorePropTag(13844, PropertyType.Int64, new StorePropInfo("ContainerModifyVersion", 13844, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F32 RID: 3890
			public static readonly StorePropTag DefaultViewEntryId = new StorePropTag(13846, PropertyType.Binary, new StorePropInfo("DefaultViewEntryId", 13846, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F33 RID: 3891
			public static readonly StorePropTag AssociatedContentCount = new StorePropTag(13847, PropertyType.Int32, new StorePropInfo("AssociatedContentCount", 13847, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F34 RID: 3892
			public static readonly StorePropTag AssociatedContentCountInt64 = new StorePropTag(13847, PropertyType.Int64, new StorePropInfo("AssociatedContentCountInt64", 13847, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F35 RID: 3893
			public static readonly StorePropTag PackedNamedProps = new StorePropTag(13852, PropertyType.Binary, new StorePropInfo("PackedNamedProps", 13852, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F36 RID: 3894
			public static readonly StorePropTag AllowAgeOut = new StorePropTag(13855, PropertyType.Boolean, new StorePropInfo("AllowAgeOut", 13855, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F37 RID: 3895
			public static readonly StorePropTag SearchFolderMsgCount = new StorePropTag(13892, PropertyType.Int32, new StorePropInfo("SearchFolderMsgCount", 13892, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F38 RID: 3896
			public static readonly StorePropTag PartOfContentIndexing = new StorePropTag(13893, PropertyType.Boolean, new StorePropInfo("PartOfContentIndexing", 13893, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F39 RID: 3897
			public static readonly StorePropTag OwnerLogonUserConfigurationCache = new StorePropTag(13894, PropertyType.Binary, new StorePropInfo("OwnerLogonUserConfigurationCache", 13894, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F3A RID: 3898
			public static readonly StorePropTag SearchFolderAgeOutTimeout = new StorePropTag(13895, PropertyType.Int32, new StorePropInfo("SearchFolderAgeOutTimeout", 13895, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F3B RID: 3899
			public static readonly StorePropTag SearchFolderPopulationResult = new StorePropTag(13896, PropertyType.Int32, new StorePropInfo("SearchFolderPopulationResult", 13896, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F3C RID: 3900
			public static readonly StorePropTag SearchFolderPopulationDiagnostics = new StorePropTag(13897, PropertyType.Binary, new StorePropInfo("SearchFolderPopulationDiagnostics", 13897, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F3D RID: 3901
			public static readonly StorePropTag ConversationTopicHashEntries = new StorePropTag(13920, PropertyType.Binary, new StorePropInfo("ConversationTopicHashEntries", 13920, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F3E RID: 3902
			public static readonly StorePropTag ContentAggregationFlags = new StorePropTag(13967, PropertyType.Int32, new StorePropInfo("ContentAggregationFlags", 13967, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F3F RID: 3903
			public static readonly StorePropTag TransportRulesSnapshot = new StorePropTag(13968, PropertyType.Binary, new StorePropInfo("TransportRulesSnapshot", 13968, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 6, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F40 RID: 3904
			public static readonly StorePropTag TransportRulesSnapshotId = new StorePropTag(13969, PropertyType.Guid, new StorePropInfo("TransportRulesSnapshotId", 13969, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 6, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F41 RID: 3905
			public static readonly StorePropTag CurrentIPMWasteBasketContainerEntryId = new StorePropTag(14031, PropertyType.Binary, new StorePropInfo("CurrentIPMWasteBasketContainerEntryId", 14031, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F42 RID: 3906
			public static readonly StorePropTag IPMAppointmentEntryId = new StorePropTag(14032, PropertyType.Binary, new StorePropInfo("IPMAppointmentEntryId", 14032, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F43 RID: 3907
			public static readonly StorePropTag IPMContactEntryId = new StorePropTag(14033, PropertyType.Binary, new StorePropInfo("IPMContactEntryId", 14033, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F44 RID: 3908
			public static readonly StorePropTag IPMJournalEntryId = new StorePropTag(14034, PropertyType.Binary, new StorePropInfo("IPMJournalEntryId", 14034, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F45 RID: 3909
			public static readonly StorePropTag IPMNoteEntryId = new StorePropTag(14035, PropertyType.Binary, new StorePropInfo("IPMNoteEntryId", 14035, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F46 RID: 3910
			public static readonly StorePropTag IPMTaskEntryId = new StorePropTag(14036, PropertyType.Binary, new StorePropInfo("IPMTaskEntryId", 14036, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F47 RID: 3911
			public static readonly StorePropTag REMOnlineEntryId = new StorePropTag(14037, PropertyType.Binary, new StorePropInfo("REMOnlineEntryId", 14037, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F48 RID: 3912
			public static readonly StorePropTag IPMOfflineEntryId = new StorePropTag(14038, PropertyType.Binary, new StorePropInfo("IPMOfflineEntryId", 14038, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F49 RID: 3913
			public static readonly StorePropTag IPMDraftsEntryId = new StorePropTag(14039, PropertyType.Binary, new StorePropInfo("IPMDraftsEntryId", 14039, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F4A RID: 3914
			public static readonly StorePropTag AdditionalRENEntryIds = new StorePropTag(14040, PropertyType.MVBinary, new StorePropInfo("AdditionalRENEntryIds", 14040, PropertyType.MVBinary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F4B RID: 3915
			public static readonly StorePropTag AdditionalRENEntryIdsExtended = new StorePropTag(14041, PropertyType.Binary, new StorePropInfo("AdditionalRENEntryIdsExtended", 14041, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F4C RID: 3916
			public static readonly StorePropTag AdditionalRENEntryIdsExtendedMV = new StorePropTag(14041, PropertyType.MVBinary, new StorePropInfo("AdditionalRENEntryIdsExtendedMV", 14041, PropertyType.MVBinary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F4D RID: 3917
			public static readonly StorePropTag ExtendedFolderFlags = new StorePropTag(14042, PropertyType.Binary, new StorePropInfo("ExtendedFolderFlags", 14042, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F4E RID: 3918
			public static readonly StorePropTag ContainerTimestamp = new StorePropTag(14043, PropertyType.SysTime, new StorePropInfo("ContainerTimestamp", 14043, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F4F RID: 3919
			public static readonly StorePropTag INetUnread = new StorePropTag(14045, PropertyType.Int32, new StorePropInfo("INetUnread", 14045, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F50 RID: 3920
			public static readonly StorePropTag NetFolderFlags = new StorePropTag(14046, PropertyType.Int32, new StorePropInfo("NetFolderFlags", 14046, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F51 RID: 3921
			public static readonly StorePropTag FolderWebViewInfo = new StorePropTag(14047, PropertyType.Binary, new StorePropInfo("FolderWebViewInfo", 14047, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F52 RID: 3922
			public static readonly StorePropTag FolderWebViewInfoExtended = new StorePropTag(14048, PropertyType.Binary, new StorePropInfo("FolderWebViewInfoExtended", 14048, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F53 RID: 3923
			public static readonly StorePropTag FolderViewFlags = new StorePropTag(14049, PropertyType.Int32, new StorePropInfo("FolderViewFlags", 14049, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F54 RID: 3924
			public static readonly StorePropTag FreeBusyEntryIds = new StorePropTag(14052, PropertyType.MVBinary, new StorePropInfo("FreeBusyEntryIds", 14052, PropertyType.MVBinary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F55 RID: 3925
			public static readonly StorePropTag DefaultPostMsgClass = new StorePropTag(14053, PropertyType.Unicode, new StorePropInfo("DefaultPostMsgClass", 14053, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F56 RID: 3926
			public static readonly StorePropTag DefaultPostDisplayName = new StorePropTag(14054, PropertyType.Unicode, new StorePropInfo("DefaultPostDisplayName", 14054, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F57 RID: 3927
			public static readonly StorePropTag FolderViewList = new StorePropTag(14059, PropertyType.Binary, new StorePropInfo("FolderViewList", 14059, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F58 RID: 3928
			public static readonly StorePropTag AgingPeriod = new StorePropTag(14060, PropertyType.Int32, new StorePropInfo("AgingPeriod", 14060, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F59 RID: 3929
			public static readonly StorePropTag AgingGranularity = new StorePropTag(14062, PropertyType.Int32, new StorePropInfo("AgingGranularity", 14062, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F5A RID: 3930
			public static readonly StorePropTag DefaultFoldersLocaleId = new StorePropTag(14064, PropertyType.Int32, new StorePropInfo("DefaultFoldersLocaleId", 14064, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F5B RID: 3931
			public static readonly StorePropTag InternalAccess = new StorePropTag(14065, PropertyType.Boolean, new StorePropInfo("InternalAccess", 14065, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F5C RID: 3932
			public static readonly StorePropTag SyncEventSuppressGuid = new StorePropTag(14464, PropertyType.Binary, new StorePropInfo("SyncEventSuppressGuid", 14464, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F5D RID: 3933
			public static readonly StorePropTag DisplayType = new StorePropTag(14592, PropertyType.Int32, new StorePropInfo("DisplayType", 14592, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F5E RID: 3934
			public static readonly StorePropTag TestBlobProperty = new StorePropTag(15616, PropertyType.Int64, new StorePropInfo("TestBlobProperty", 15616, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F5F RID: 3935
			public static readonly StorePropTag AdminSecurityDescriptor = new StorePropTag(15649, PropertyType.Binary, new StorePropInfo("AdminSecurityDescriptor", 15649, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F60 RID: 3936
			public static readonly StorePropTag Win32NTSecurityDescriptor = new StorePropTag(15650, PropertyType.Binary, new StorePropInfo("Win32NTSecurityDescriptor", 15650, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F61 RID: 3937
			public static readonly StorePropTag NonWin32ACL = new StorePropTag(15651, PropertyType.Boolean, new StorePropInfo("NonWin32ACL", 15651, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F62 RID: 3938
			public static readonly StorePropTag ItemLevelACL = new StorePropTag(15652, PropertyType.Boolean, new StorePropInfo("ItemLevelACL", 15652, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F63 RID: 3939
			public static readonly StorePropTag ICSGid = new StorePropTag(15662, PropertyType.Binary, new StorePropInfo("ICSGid", 15662, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F64 RID: 3940
			public static readonly StorePropTag SystemFolderFlags = new StorePropTag(15663, PropertyType.Int32, new StorePropInfo("SystemFolderFlags", 15663, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F65 RID: 3941
			public static readonly StorePropTag MaterializedRestrictionSearchRoot = new StorePropTag(15772, PropertyType.Binary, new StorePropInfo("MaterializedRestrictionSearchRoot", 15772, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				16
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F66 RID: 3942
			public static readonly StorePropTag MailboxPartitionNumber = new StorePropTag(15775, PropertyType.Int32, new StorePropInfo("MailboxPartitionNumber", 15775, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				16
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F67 RID: 3943
			public static readonly StorePropTag MailboxNumberInternal = new StorePropTag(15776, PropertyType.Int32, new StorePropInfo("MailboxNumberInternal", 15776, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				16
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F68 RID: 3944
			public static readonly StorePropTag QueryCriteriaInternal = new StorePropTag(15777, PropertyType.Binary, new StorePropInfo("QueryCriteriaInternal", 15777, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				16
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F69 RID: 3945
			public static readonly StorePropTag LastQuotaNotificationTime = new StorePropTag(15778, PropertyType.SysTime, new StorePropInfo("LastQuotaNotificationTime", 15778, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				16
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F6A RID: 3946
			public static readonly StorePropTag PropertyPromotionInProgressHiddenItems = new StorePropTag(15779, PropertyType.Boolean, new StorePropInfo("PropertyPromotionInProgressHiddenItems", 15779, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				16
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F6B RID: 3947
			public static readonly StorePropTag PropertyPromotionInProgressNormalItems = new StorePropTag(15780, PropertyType.Boolean, new StorePropInfo("PropertyPromotionInProgressNormalItems", 15780, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				16
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F6C RID: 3948
			public static readonly StorePropTag VirtualUnreadMessageCount = new StorePropTag(15787, PropertyType.Int64, new StorePropInfo("VirtualUnreadMessageCount", 15787, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				16
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F6D RID: 3949
			public static readonly StorePropTag InternalChangeKey = new StorePropTag(15806, PropertyType.Binary, new StorePropInfo("InternalChangeKey", 15806, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				16
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F6E RID: 3950
			public static readonly StorePropTag InternalSourceKey = new StorePropTag(15807, PropertyType.Binary, new StorePropInfo("InternalSourceKey", 15807, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				16
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F6F RID: 3951
			public static readonly StorePropTag CorrelationId = new StorePropTag(15825, PropertyType.Guid, new StorePropInfo("CorrelationId", 15825, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F70 RID: 3952
			public static readonly StorePropTag LastConflict = new StorePropTag(16329, PropertyType.Binary, new StorePropInfo("LastConflict", 16329, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F71 RID: 3953
			public static readonly StorePropTag NTSDModificationTime = new StorePropTag(16342, PropertyType.SysTime, new StorePropInfo("NTSDModificationTime", 16342, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F72 RID: 3954
			public static readonly StorePropTag ACLDataChecksum = new StorePropTag(16343, PropertyType.Int32, new StorePropInfo("ACLDataChecksum", 16343, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(0, 1, 2, 3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F73 RID: 3955
			public static readonly StorePropTag ACLData = new StorePropTag(16352, PropertyType.Binary, new StorePropInfo("ACLData", 16352, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(0, 1, 2, 3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F74 RID: 3956
			public static readonly StorePropTag ACLTable = new StorePropTag(16352, PropertyType.Object, new StorePropInfo("ACLTable", 16352, PropertyType.Object, StorePropInfo.Flags.None, 0UL, new PropertyCategories(0, 1, 2, 3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F75 RID: 3957
			public static readonly StorePropTag RulesData = new StorePropTag(16353, PropertyType.Binary, new StorePropInfo("RulesData", 16353, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F76 RID: 3958
			public static readonly StorePropTag RulesTable = new StorePropTag(16353, PropertyType.Object, new StorePropInfo("RulesTable", 16353, PropertyType.Object, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F77 RID: 3959
			public static readonly StorePropTag OofHistory = new StorePropTag(16355, PropertyType.Binary, new StorePropInfo("OofHistory", 16355, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(17)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F78 RID: 3960
			public static readonly StorePropTag DesignInProgress = new StorePropTag(16356, PropertyType.Boolean, new StorePropInfo("DesignInProgress", 16356, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F79 RID: 3961
			public static readonly StorePropTag SecureOrigination = new StorePropTag(16357, PropertyType.Boolean, new StorePropInfo("SecureOrigination", 16357, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F7A RID: 3962
			public static readonly StorePropTag PublishInAddressBook = new StorePropTag(16358, PropertyType.Boolean, new StorePropInfo("PublishInAddressBook", 16358, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F7B RID: 3963
			public static readonly StorePropTag ResolveMethod = new StorePropTag(16359, PropertyType.Int32, new StorePropInfo("ResolveMethod", 16359, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F7C RID: 3964
			public static readonly StorePropTag AddressBookDisplayName = new StorePropTag(16360, PropertyType.Unicode, new StorePropInfo("AddressBookDisplayName", 16360, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F7D RID: 3965
			public static readonly StorePropTag EFormsLocaleId = new StorePropTag(16361, PropertyType.Int32, new StorePropInfo("EFormsLocaleId", 16361, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F7E RID: 3966
			public static readonly StorePropTag ExtendedACLData = new StorePropTag(16382, PropertyType.Binary, new StorePropInfo("ExtendedACLData", 16382, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(0, 1, 2, 3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F7F RID: 3967
			public static readonly StorePropTag RulesSize = new StorePropTag(16383, PropertyType.Int32, new StorePropInfo("RulesSize", 16383, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F80 RID: 3968
			public static readonly StorePropTag NewAttach = new StorePropTag(16384, PropertyType.Int32, new StorePropInfo("NewAttach", 16384, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F81 RID: 3969
			public static readonly StorePropTag StartEmbed = new StorePropTag(16385, PropertyType.Int32, new StorePropInfo("StartEmbed", 16385, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F82 RID: 3970
			public static readonly StorePropTag EndEmbed = new StorePropTag(16386, PropertyType.Int32, new StorePropInfo("EndEmbed", 16386, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F83 RID: 3971
			public static readonly StorePropTag StartRecip = new StorePropTag(16387, PropertyType.Int32, new StorePropInfo("StartRecip", 16387, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F84 RID: 3972
			public static readonly StorePropTag EndRecip = new StorePropTag(16388, PropertyType.Int32, new StorePropInfo("EndRecip", 16388, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F85 RID: 3973
			public static readonly StorePropTag EndCcRecip = new StorePropTag(16389, PropertyType.Int32, new StorePropInfo("EndCcRecip", 16389, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F86 RID: 3974
			public static readonly StorePropTag EndBccRecip = new StorePropTag(16390, PropertyType.Int32, new StorePropInfo("EndBccRecip", 16390, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F87 RID: 3975
			public static readonly StorePropTag EndP1Recip = new StorePropTag(16391, PropertyType.Int32, new StorePropInfo("EndP1Recip", 16391, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F88 RID: 3976
			public static readonly StorePropTag DNPrefix = new StorePropTag(16392, PropertyType.Unicode, new StorePropInfo("DNPrefix", 16392, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F89 RID: 3977
			public static readonly StorePropTag StartTopFolder = new StorePropTag(16393, PropertyType.Int32, new StorePropInfo("StartTopFolder", 16393, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F8A RID: 3978
			public static readonly StorePropTag StartSubFolder = new StorePropTag(16394, PropertyType.Int32, new StorePropInfo("StartSubFolder", 16394, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F8B RID: 3979
			public static readonly StorePropTag EndFolder = new StorePropTag(16395, PropertyType.Int32, new StorePropInfo("EndFolder", 16395, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F8C RID: 3980
			public static readonly StorePropTag StartMessage = new StorePropTag(16396, PropertyType.Int32, new StorePropInfo("StartMessage", 16396, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F8D RID: 3981
			public static readonly StorePropTag EndMessage = new StorePropTag(16397, PropertyType.Int32, new StorePropInfo("EndMessage", 16397, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F8E RID: 3982
			public static readonly StorePropTag EndAttach = new StorePropTag(16398, PropertyType.Int32, new StorePropInfo("EndAttach", 16398, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F8F RID: 3983
			public static readonly StorePropTag EcWarning = new StorePropTag(16399, PropertyType.Int32, new StorePropInfo("EcWarning", 16399, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F90 RID: 3984
			public static readonly StorePropTag StartFAIMessage = new StorePropTag(16400, PropertyType.Int32, new StorePropInfo("StartFAIMessage", 16400, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F91 RID: 3985
			public static readonly StorePropTag NewFXFolder = new StorePropTag(16401, PropertyType.Binary, new StorePropInfo("NewFXFolder", 16401, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F92 RID: 3986
			public static readonly StorePropTag IncrSyncChange = new StorePropTag(16402, PropertyType.Int32, new StorePropInfo("IncrSyncChange", 16402, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F93 RID: 3987
			public static readonly StorePropTag IncrSyncDelete = new StorePropTag(16403, PropertyType.Int32, new StorePropInfo("IncrSyncDelete", 16403, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F94 RID: 3988
			public static readonly StorePropTag IncrSyncEnd = new StorePropTag(16404, PropertyType.Int32, new StorePropInfo("IncrSyncEnd", 16404, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F95 RID: 3989
			public static readonly StorePropTag IncrSyncMessage = new StorePropTag(16405, PropertyType.Int32, new StorePropInfo("IncrSyncMessage", 16405, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F96 RID: 3990
			public static readonly StorePropTag FastTransferDelProp = new StorePropTag(16406, PropertyType.Int32, new StorePropInfo("FastTransferDelProp", 16406, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F97 RID: 3991
			public static readonly StorePropTag IdsetGiven = new StorePropTag(16407, PropertyType.Binary, new StorePropInfo("IdsetGiven", 16407, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F98 RID: 3992
			public static readonly StorePropTag IdsetGivenInt32 = new StorePropTag(16407, PropertyType.Int32, new StorePropInfo("IdsetGivenInt32", 16407, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F99 RID: 3993
			public static readonly StorePropTag FastTransferErrorInfo = new StorePropTag(16408, PropertyType.Int32, new StorePropInfo("FastTransferErrorInfo", 16408, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F9A RID: 3994
			public static readonly StorePropTag SoftDeletes = new StorePropTag(16417, PropertyType.Binary, new StorePropInfo("SoftDeletes", 16417, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F9B RID: 3995
			public static readonly StorePropTag IdsetRead = new StorePropTag(16429, PropertyType.Binary, new StorePropInfo("IdsetRead", 16429, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F9C RID: 3996
			public static readonly StorePropTag IdsetUnread = new StorePropTag(16430, PropertyType.Binary, new StorePropInfo("IdsetUnread", 16430, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F9D RID: 3997
			public static readonly StorePropTag IncrSyncRead = new StorePropTag(16431, PropertyType.Int32, new StorePropInfo("IncrSyncRead", 16431, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F9E RID: 3998
			public static readonly StorePropTag IncrSyncStateBegin = new StorePropTag(16442, PropertyType.Int32, new StorePropInfo("IncrSyncStateBegin", 16442, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000F9F RID: 3999
			public static readonly StorePropTag IncrSyncStateEnd = new StorePropTag(16443, PropertyType.Int32, new StorePropInfo("IncrSyncStateEnd", 16443, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FA0 RID: 4000
			public static readonly StorePropTag IncrSyncImailStream = new StorePropTag(16444, PropertyType.Int32, new StorePropInfo("IncrSyncImailStream", 16444, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FA1 RID: 4001
			public static readonly StorePropTag IncrSyncImailStreamContinue = new StorePropTag(16486, PropertyType.Int32, new StorePropInfo("IncrSyncImailStreamContinue", 16486, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FA2 RID: 4002
			public static readonly StorePropTag IncrSyncImailStreamCancel = new StorePropTag(16487, PropertyType.Int32, new StorePropInfo("IncrSyncImailStreamCancel", 16487, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FA3 RID: 4003
			public static readonly StorePropTag IncrSyncImailStream2Continue = new StorePropTag(16497, PropertyType.Int32, new StorePropInfo("IncrSyncImailStream2Continue", 16497, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FA4 RID: 4004
			public static readonly StorePropTag IncrSyncProgressMode = new StorePropTag(16500, PropertyType.Boolean, new StorePropInfo("IncrSyncProgressMode", 16500, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FA5 RID: 4005
			public static readonly StorePropTag SyncProgressPerMsg = new StorePropTag(16501, PropertyType.Boolean, new StorePropInfo("SyncProgressPerMsg", 16501, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FA6 RID: 4006
			public static readonly StorePropTag IncrSyncMsgPartial = new StorePropTag(16506, PropertyType.Int32, new StorePropInfo("IncrSyncMsgPartial", 16506, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FA7 RID: 4007
			public static readonly StorePropTag IncrSyncGroupInfo = new StorePropTag(16507, PropertyType.Int32, new StorePropInfo("IncrSyncGroupInfo", 16507, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FA8 RID: 4008
			public static readonly StorePropTag IncrSyncGroupId = new StorePropTag(16508, PropertyType.Int32, new StorePropInfo("IncrSyncGroupId", 16508, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FA9 RID: 4009
			public static readonly StorePropTag IncrSyncChangePartial = new StorePropTag(16509, PropertyType.Int32, new StorePropInfo("IncrSyncChangePartial", 16509, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FAA RID: 4010
			public static readonly StorePropTag HierRev = new StorePropTag(16514, PropertyType.SysTime, new StorePropInfo("HierRev", 16514, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 5, 9, 11)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FAB RID: 4011
			public static readonly StorePropTag SourceKey = new StorePropTag(26080, PropertyType.Binary, new StorePropInfo("SourceKey", 26080, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FAC RID: 4012
			public static readonly StorePropTag ParentSourceKey = new StorePropTag(26081, PropertyType.Binary, new StorePropInfo("ParentSourceKey", 26081, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FAD RID: 4013
			public static readonly StorePropTag ChangeKey = new StorePropTag(26082, PropertyType.Binary, new StorePropInfo("ChangeKey", 26082, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FAE RID: 4014
			public static readonly StorePropTag PredecessorChangeList = new StorePropTag(26083, PropertyType.Binary, new StorePropInfo("PredecessorChangeList", 26083, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FAF RID: 4015
			public static readonly StorePropTag PreventMsgCreate = new StorePropTag(26100, PropertyType.Boolean, new StorePropInfo("PreventMsgCreate", 26100, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FB0 RID: 4016
			public static readonly StorePropTag LISSD = new StorePropTag(26105, PropertyType.Binary, new StorePropInfo("LISSD", 26105, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FB1 RID: 4017
			public static readonly StorePropTag FavoritesDefaultName = new StorePropTag(26165, PropertyType.Unicode, new StorePropInfo("FavoritesDefaultName", 26165, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FB2 RID: 4018
			public static readonly StorePropTag FolderChildCount = new StorePropTag(26168, PropertyType.Int32, new StorePropInfo("FolderChildCount", 26168, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FB3 RID: 4019
			public static readonly StorePropTag FolderChildCountInt64 = new StorePropTag(26168, PropertyType.Int64, new StorePropInfo("FolderChildCountInt64", 26168, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FB4 RID: 4020
			public static readonly StorePropTag Rights = new StorePropTag(26169, PropertyType.Int32, new StorePropInfo("Rights", 26169, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FB5 RID: 4021
			public static readonly StorePropTag HasRules = new StorePropTag(26170, PropertyType.Boolean, new StorePropInfo("HasRules", 26170, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FB6 RID: 4022
			public static readonly StorePropTag AddressBookEntryId = new StorePropTag(26171, PropertyType.Binary, new StorePropInfo("AddressBookEntryId", 26171, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FB7 RID: 4023
			public static readonly StorePropTag HierarchyChangeNumber = new StorePropTag(26174, PropertyType.Int32, new StorePropInfo("HierarchyChangeNumber", 26174, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9, 16)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FB8 RID: 4024
			public static readonly StorePropTag HasModeratorRules = new StorePropTag(26175, PropertyType.Boolean, new StorePropInfo("HasModeratorRules", 26175, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FB9 RID: 4025
			public static readonly StorePropTag ModeratorRuleCount = new StorePropTag(26175, PropertyType.Int32, new StorePropInfo("ModeratorRuleCount", 26175, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FBA RID: 4026
			public static readonly StorePropTag DeletedMsgCount = new StorePropTag(26176, PropertyType.Int32, new StorePropInfo("DeletedMsgCount", 26176, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FBB RID: 4027
			public static readonly StorePropTag DeletedMsgCountInt64 = new StorePropTag(26176, PropertyType.Int64, new StorePropInfo("DeletedMsgCountInt64", 26176, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FBC RID: 4028
			public static readonly StorePropTag DeletedFolderCount = new StorePropTag(26177, PropertyType.Int32, new StorePropInfo("DeletedFolderCount", 26177, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FBD RID: 4029
			public static readonly StorePropTag DeletedAssocMsgCount = new StorePropTag(26179, PropertyType.Int32, new StorePropInfo("DeletedAssocMsgCount", 26179, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FBE RID: 4030
			public static readonly StorePropTag DeletedAssocMsgCountInt64 = new StorePropTag(26179, PropertyType.Int64, new StorePropInfo("DeletedAssocMsgCountInt64", 26179, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FBF RID: 4031
			public static readonly StorePropTag PromotedProperties = new StorePropTag(26181, PropertyType.Binary, new StorePropInfo("PromotedProperties", 26181, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FC0 RID: 4032
			public static readonly StorePropTag HiddenPromotedProperties = new StorePropTag(26182, PropertyType.Binary, new StorePropInfo("HiddenPromotedProperties", 26182, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FC1 RID: 4033
			public static readonly StorePropTag LinkedSiteAuthorityUrl = new StorePropTag(26183, PropertyType.Unicode, new StorePropInfo("LinkedSiteAuthorityUrl", 26183, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FC2 RID: 4034
			public static readonly StorePropTag HasNamedProperties = new StorePropTag(26186, PropertyType.Boolean, new StorePropInfo("HasNamedProperties", 26186, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FC3 RID: 4035
			public static readonly StorePropTag FidMid = new StorePropTag(26188, PropertyType.Binary, new StorePropInfo("FidMid", 26188, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FC4 RID: 4036
			public static readonly StorePropTag ICSChangeKey = new StorePropTag(26197, PropertyType.Binary, new StorePropInfo("ICSChangeKey", 26197, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FC5 RID: 4037
			public static readonly StorePropTag SetPropsCondition = new StorePropTag(26199, PropertyType.Binary, new StorePropInfo("SetPropsCondition", 26199, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FC6 RID: 4038
			public static readonly StorePropTag DeletedOn = new StorePropTag(26255, PropertyType.SysTime, new StorePropInfo("DeletedOn", 26255, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FC7 RID: 4039
			public static readonly StorePropTag ReplicationStyle = new StorePropTag(26256, PropertyType.Int32, new StorePropInfo("ReplicationStyle", 26256, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FC8 RID: 4040
			public static readonly StorePropTag ReplicationTIB = new StorePropTag(26257, PropertyType.Binary, new StorePropInfo("ReplicationTIB", 26257, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FC9 RID: 4041
			public static readonly StorePropTag ReplicationMsgPriority = new StorePropTag(26258, PropertyType.Int32, new StorePropInfo("ReplicationMsgPriority", 26258, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FCA RID: 4042
			public static readonly StorePropTag ReplicaList = new StorePropTag(26264, PropertyType.Binary, new StorePropInfo("ReplicaList", 26264, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(17)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FCB RID: 4043
			public static readonly StorePropTag OverallAgeLimit = new StorePropTag(26265, PropertyType.Int32, new StorePropInfo("OverallAgeLimit", 26265, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FCC RID: 4044
			public static readonly StorePropTag DeletedMessageSize = new StorePropTag(26267, PropertyType.Int64, new StorePropInfo("DeletedMessageSize", 26267, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FCD RID: 4045
			public static readonly StorePropTag DeletedMessageSize32 = new StorePropTag(26267, PropertyType.Int32, new StorePropInfo("DeletedMessageSize32", 26267, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FCE RID: 4046
			public static readonly StorePropTag DeletedNormalMessageSize = new StorePropTag(26268, PropertyType.Int64, new StorePropInfo("DeletedNormalMessageSize", 26268, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FCF RID: 4047
			public static readonly StorePropTag DeletedNormalMessageSize32 = new StorePropTag(26268, PropertyType.Int32, new StorePropInfo("DeletedNormalMessageSize32", 26268, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FD0 RID: 4048
			public static readonly StorePropTag DeletedAssociatedMessageSize = new StorePropTag(26269, PropertyType.Int64, new StorePropInfo("DeletedAssociatedMessageSize", 26269, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FD1 RID: 4049
			public static readonly StorePropTag DeletedAssociatedMessageSize32 = new StorePropTag(26269, PropertyType.Int32, new StorePropInfo("DeletedAssociatedMessageSize32", 26269, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FD2 RID: 4050
			public static readonly StorePropTag SecureInSite = new StorePropTag(26270, PropertyType.Boolean, new StorePropInfo("SecureInSite", 26270, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FD3 RID: 4051
			public static readonly StorePropTag FolderFlags = new StorePropTag(26280, PropertyType.Int32, new StorePropInfo("FolderFlags", 26280, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FD4 RID: 4052
			public static readonly StorePropTag LastAccessTime = new StorePropTag(26281, PropertyType.SysTime, new StorePropInfo("LastAccessTime", 26281, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FD5 RID: 4053
			public static readonly StorePropTag NormalMsgWithAttachCount = new StorePropTag(26285, PropertyType.Int32, new StorePropInfo("NormalMsgWithAttachCount", 26285, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FD6 RID: 4054
			public static readonly StorePropTag NormalMsgWithAttachCountInt64 = new StorePropTag(26285, PropertyType.Int64, new StorePropInfo("NormalMsgWithAttachCountInt64", 26285, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FD7 RID: 4055
			public static readonly StorePropTag AssocMsgWithAttachCount = new StorePropTag(26286, PropertyType.Int32, new StorePropInfo("AssocMsgWithAttachCount", 26286, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FD8 RID: 4056
			public static readonly StorePropTag AssocMsgWithAttachCountInt64 = new StorePropTag(26286, PropertyType.Int64, new StorePropInfo("AssocMsgWithAttachCountInt64", 26286, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FD9 RID: 4057
			public static readonly StorePropTag RecipientOnNormalMsgCount = new StorePropTag(26287, PropertyType.Int32, new StorePropInfo("RecipientOnNormalMsgCount", 26287, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FDA RID: 4058
			public static readonly StorePropTag RecipientOnNormalMsgCountInt64 = new StorePropTag(26287, PropertyType.Int64, new StorePropInfo("RecipientOnNormalMsgCountInt64", 26287, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FDB RID: 4059
			public static readonly StorePropTag RecipientOnAssocMsgCount = new StorePropTag(26288, PropertyType.Int32, new StorePropInfo("RecipientOnAssocMsgCount", 26288, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FDC RID: 4060
			public static readonly StorePropTag RecipientOnAssocMsgCountInt64 = new StorePropTag(26288, PropertyType.Int64, new StorePropInfo("RecipientOnAssocMsgCountInt64", 26288, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FDD RID: 4061
			public static readonly StorePropTag AttachOnNormalMsgCt = new StorePropTag(26289, PropertyType.Int32, new StorePropInfo("AttachOnNormalMsgCt", 26289, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FDE RID: 4062
			public static readonly StorePropTag AttachOnNormalMsgCtInt64 = new StorePropTag(26289, PropertyType.Int64, new StorePropInfo("AttachOnNormalMsgCtInt64", 26289, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FDF RID: 4063
			public static readonly StorePropTag AttachOnAssocMsgCt = new StorePropTag(26290, PropertyType.Int32, new StorePropInfo("AttachOnAssocMsgCt", 26290, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FE0 RID: 4064
			public static readonly StorePropTag AttachOnAssocMsgCtInt64 = new StorePropTag(26290, PropertyType.Int64, new StorePropInfo("AttachOnAssocMsgCtInt64", 26290, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FE1 RID: 4065
			public static readonly StorePropTag NormalMessageSize = new StorePropTag(26291, PropertyType.Int64, new StorePropInfo("NormalMessageSize", 26291, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FE2 RID: 4066
			public static readonly StorePropTag NormalMessageSize32 = new StorePropTag(26291, PropertyType.Int32, new StorePropInfo("NormalMessageSize32", 26291, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FE3 RID: 4067
			public static readonly StorePropTag AssociatedMessageSize = new StorePropTag(26292, PropertyType.Int64, new StorePropInfo("AssociatedMessageSize", 26292, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FE4 RID: 4068
			public static readonly StorePropTag AssociatedMessageSize32 = new StorePropTag(26292, PropertyType.Int32, new StorePropInfo("AssociatedMessageSize32", 26292, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FE5 RID: 4069
			public static readonly StorePropTag FolderPathName = new StorePropTag(26293, PropertyType.Unicode, new StorePropInfo("FolderPathName", 26293, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FE6 RID: 4070
			public static readonly StorePropTag OwnerCount = new StorePropTag(26294, PropertyType.Int32, new StorePropInfo("OwnerCount", 26294, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FE7 RID: 4071
			public static readonly StorePropTag ContactCount = new StorePropTag(26295, PropertyType.Int32, new StorePropInfo("ContactCount", 26295, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FE8 RID: 4072
			public static readonly StorePropTag RetentionAgeLimit = new StorePropTag(26308, PropertyType.Int32, new StorePropInfo("RetentionAgeLimit", 26308, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FE9 RID: 4073
			public static readonly StorePropTag DisablePerUserRead = new StorePropTag(26309, PropertyType.Boolean, new StorePropInfo("DisablePerUserRead", 26309, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FEA RID: 4074
			public static readonly StorePropTag ServerDN = new StorePropTag(26336, PropertyType.Unicode, new StorePropInfo("ServerDN", 26336, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FEB RID: 4075
			public static readonly StorePropTag BackfillRanking = new StorePropTag(26337, PropertyType.Int32, new StorePropInfo("BackfillRanking", 26337, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FEC RID: 4076
			public static readonly StorePropTag LastTransmissionTime = new StorePropTag(26338, PropertyType.Int32, new StorePropInfo("LastTransmissionTime", 26338, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FED RID: 4077
			public static readonly StorePropTag StatusSendTime = new StorePropTag(26339, PropertyType.SysTime, new StorePropInfo("StatusSendTime", 26339, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FEE RID: 4078
			public static readonly StorePropTag BackfillEntryCount = new StorePropTag(26340, PropertyType.Int32, new StorePropInfo("BackfillEntryCount", 26340, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FEF RID: 4079
			public static readonly StorePropTag NextBroadcastTime = new StorePropTag(26341, PropertyType.SysTime, new StorePropInfo("NextBroadcastTime", 26341, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FF0 RID: 4080
			public static readonly StorePropTag NextBackfillTime = new StorePropTag(26342, PropertyType.SysTime, new StorePropInfo("NextBackfillTime", 26342, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FF1 RID: 4081
			public static readonly StorePropTag LastCNBroadcast = new StorePropTag(26343, PropertyType.Binary, new StorePropInfo("LastCNBroadcast", 26343, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FF2 RID: 4082
			public static readonly StorePropTag LastShortCNBroadcast = new StorePropTag(26356, PropertyType.Binary, new StorePropInfo("LastShortCNBroadcast", 26356, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FF3 RID: 4083
			public static readonly StorePropTag AverageTransmissionTime = new StorePropTag(26363, PropertyType.SysTime, new StorePropInfo("AverageTransmissionTime", 26363, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FF4 RID: 4084
			public static readonly StorePropTag ReplicationStatus = new StorePropTag(26364, PropertyType.Int64, new StorePropInfo("ReplicationStatus", 26364, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FF5 RID: 4085
			public static readonly StorePropTag LastDataReceivalTime = new StorePropTag(26365, PropertyType.SysTime, new StorePropInfo("LastDataReceivalTime", 26365, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FF6 RID: 4086
			public static readonly StorePropTag AdminDisplayName = new StorePropTag(26366, PropertyType.Unicode, new StorePropInfo("AdminDisplayName", 26366, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FF7 RID: 4087
			public static readonly StorePropTag URLName = new StorePropTag(26375, PropertyType.Unicode, new StorePropInfo("URLName", 26375, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FF8 RID: 4088
			public static readonly StorePropTag LocalCommitTime = new StorePropTag(26377, PropertyType.SysTime, new StorePropInfo("LocalCommitTime", 26377, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FF9 RID: 4089
			public static readonly StorePropTag LocalCommitTimeMax = new StorePropTag(26378, PropertyType.SysTime, new StorePropInfo("LocalCommitTimeMax", 26378, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FFA RID: 4090
			public static readonly StorePropTag DeletedCountTotal = new StorePropTag(26379, PropertyType.Int32, new StorePropInfo("DeletedCountTotal", 26379, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FFB RID: 4091
			public static readonly StorePropTag DeletedCountTotalInt64 = new StorePropTag(26379, PropertyType.Int64, new StorePropInfo("DeletedCountTotalInt64", 26379, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FFC RID: 4092
			public static readonly StorePropTag ScopeFIDs = new StorePropTag(26386, PropertyType.Binary, new StorePropInfo("ScopeFIDs", 26386, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FFD RID: 4093
			public static readonly StorePropTag PFAdminDescription = new StorePropTag(26391, PropertyType.Unicode, new StorePropInfo("PFAdminDescription", 26391, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FFE RID: 4094
			public static readonly StorePropTag PFProxy = new StorePropTag(26397, PropertyType.Binary, new StorePropInfo("PFProxy", 26397, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04000FFF RID: 4095
			public static readonly StorePropTag PFPlatinumHomeMdb = new StorePropTag(26398, PropertyType.Boolean, new StorePropInfo("PFPlatinumHomeMdb", 26398, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001000 RID: 4096
			public static readonly StorePropTag PFProxyRequired = new StorePropTag(26399, PropertyType.Boolean, new StorePropInfo("PFProxyRequired", 26399, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001001 RID: 4097
			public static readonly StorePropTag PFOverHardQuotaLimit = new StorePropTag(26401, PropertyType.Int32, new StorePropInfo("PFOverHardQuotaLimit", 26401, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001002 RID: 4098
			public static readonly StorePropTag PFMsgSizeLimit = new StorePropTag(26402, PropertyType.Int32, new StorePropInfo("PFMsgSizeLimit", 26402, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001003 RID: 4099
			public static readonly StorePropTag PFDisallowMdbWideExpiry = new StorePropTag(26403, PropertyType.Boolean, new StorePropInfo("PFDisallowMdbWideExpiry", 26403, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001004 RID: 4100
			public static readonly StorePropTag FolderAdminFlags = new StorePropTag(26413, PropertyType.Int32, new StorePropInfo("FolderAdminFlags", 26413, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001005 RID: 4101
			public static readonly StorePropTag ProvisionedFID = new StorePropTag(26415, PropertyType.Int64, new StorePropInfo("ProvisionedFID", 26415, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001006 RID: 4102
			public static readonly StorePropTag ELCFolderSize = new StorePropTag(26416, PropertyType.Int64, new StorePropInfo("ELCFolderSize", 26416, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001007 RID: 4103
			public static readonly StorePropTag ELCFolderQuota = new StorePropTag(26417, PropertyType.Int32, new StorePropInfo("ELCFolderQuota", 26417, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001008 RID: 4104
			public static readonly StorePropTag ELCPolicyId = new StorePropTag(26418, PropertyType.Unicode, new StorePropInfo("ELCPolicyId", 26418, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001009 RID: 4105
			public static readonly StorePropTag ELCPolicyComment = new StorePropTag(26419, PropertyType.Unicode, new StorePropInfo("ELCPolicyComment", 26419, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400100A RID: 4106
			public static readonly StorePropTag PropertyGroupMappingId = new StorePropTag(26420, PropertyType.Int32, new StorePropInfo("PropertyGroupMappingId", 26420, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400100B RID: 4107
			public static readonly StorePropTag Fid = new StorePropTag(26440, PropertyType.Int64, new StorePropInfo("Fid", 26440, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400100C RID: 4108
			public static readonly StorePropTag FidBin = new StorePropTag(26440, PropertyType.Binary, new StorePropInfo("FidBin", 26440, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400100D RID: 4109
			public static readonly StorePropTag ParentFid = new StorePropTag(26441, PropertyType.Int64, new StorePropInfo("ParentFid", 26441, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400100E RID: 4110
			public static readonly StorePropTag ParentFidBin = new StorePropTag(26441, PropertyType.Binary, new StorePropInfo("ParentFidBin", 26441, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400100F RID: 4111
			public static readonly StorePropTag ArticleNumNext = new StorePropTag(26449, PropertyType.Int32, new StorePropInfo("ArticleNumNext", 26449, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001010 RID: 4112
			public static readonly StorePropTag ImapLastArticleId = new StorePropTag(26450, PropertyType.Int32, new StorePropInfo("ImapLastArticleId", 26450, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001011 RID: 4113
			public static readonly StorePropTag CnExport = new StorePropTag(26457, PropertyType.Binary, new StorePropInfo("CnExport", 26457, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001012 RID: 4114
			public static readonly StorePropTag PclExport = new StorePropTag(26458, PropertyType.Binary, new StorePropInfo("PclExport", 26458, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001013 RID: 4115
			public static readonly StorePropTag CnMvExport = new StorePropTag(26459, PropertyType.Binary, new StorePropInfo("CnMvExport", 26459, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001014 RID: 4116
			public static readonly StorePropTag MidsetDeletedExport = new StorePropTag(26460, PropertyType.Binary, new StorePropInfo("MidsetDeletedExport", 26460, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001015 RID: 4117
			public static readonly StorePropTag ArticleNumMic = new StorePropTag(26461, PropertyType.Int32, new StorePropInfo("ArticleNumMic", 26461, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001016 RID: 4118
			public static readonly StorePropTag ArticleNumMost = new StorePropTag(26462, PropertyType.Int32, new StorePropInfo("ArticleNumMost", 26462, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001017 RID: 4119
			public static readonly StorePropTag RulesSync = new StorePropTag(26464, PropertyType.Int32, new StorePropInfo("RulesSync", 26464, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001018 RID: 4120
			public static readonly StorePropTag ReplicaListR = new StorePropTag(26465, PropertyType.Binary, new StorePropInfo("ReplicaListR", 26465, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001019 RID: 4121
			public static readonly StorePropTag ReplicaListRC = new StorePropTag(26466, PropertyType.Binary, new StorePropInfo("ReplicaListRC", 26466, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400101A RID: 4122
			public static readonly StorePropTag ReplicaListRBUG = new StorePropTag(26467, PropertyType.Binary, new StorePropInfo("ReplicaListRBUG", 26467, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400101B RID: 4123
			public static readonly StorePropTag RootFid = new StorePropTag(26468, PropertyType.Int64, new StorePropInfo("RootFid", 26468, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400101C RID: 4124
			public static readonly StorePropTag SoftDeleted = new StorePropTag(26480, PropertyType.Boolean, new StorePropInfo("SoftDeleted", 26480, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400101D RID: 4125
			public static readonly StorePropTag QuotaStyle = new StorePropTag(26489, PropertyType.Int32, new StorePropInfo("QuotaStyle", 26489, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400101E RID: 4126
			public static readonly StorePropTag StorageQuota = new StorePropTag(26491, PropertyType.Int32, new StorePropInfo("StorageQuota", 26491, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 5, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400101F RID: 4127
			public static readonly StorePropTag FolderPropTagArray = new StorePropTag(26494, PropertyType.Binary, new StorePropInfo("FolderPropTagArray", 26494, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001020 RID: 4128
			public static readonly StorePropTag MsgFolderPropTagArray = new StorePropTag(26495, PropertyType.Binary, new StorePropInfo("MsgFolderPropTagArray", 26495, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001021 RID: 4129
			public static readonly StorePropTag SetReceiveCount = new StorePropTag(26496, PropertyType.Int32, new StorePropInfo("SetReceiveCount", 26496, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001022 RID: 4130
			public static readonly StorePropTag SubmittedCount = new StorePropTag(26498, PropertyType.Int32, new StorePropInfo("SubmittedCount", 26498, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001023 RID: 4131
			public static readonly StorePropTag CreatorToken = new StorePropTag(26499, PropertyType.Binary, new StorePropInfo("CreatorToken", 26499, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001024 RID: 4132
			public static readonly StorePropTag SearchState = new StorePropTag(26499, PropertyType.Int32, new StorePropInfo("SearchState", 26499, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001025 RID: 4133
			public static readonly StorePropTag SearchRestriction = new StorePropTag(26500, PropertyType.Binary, new StorePropInfo("SearchRestriction", 26500, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001026 RID: 4134
			public static readonly StorePropTag SearchFIDs = new StorePropTag(26501, PropertyType.Binary, new StorePropInfo("SearchFIDs", 26501, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001027 RID: 4135
			public static readonly StorePropTag RecursiveSearchFIDs = new StorePropTag(26502, PropertyType.Binary, new StorePropInfo("RecursiveSearchFIDs", 26502, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001028 RID: 4136
			public static readonly StorePropTag SearchBacklinks = new StorePropTag(26503, PropertyType.Binary, new StorePropInfo("SearchBacklinks", 26503, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001029 RID: 4137
			public static readonly StorePropTag CategFIDs = new StorePropTag(26506, PropertyType.Binary, new StorePropInfo("CategFIDs", 26506, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400102A RID: 4138
			public static readonly StorePropTag FolderCDN = new StorePropTag(26509, PropertyType.Binary, new StorePropInfo("FolderCDN", 26509, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400102B RID: 4139
			public static readonly StorePropTag MidSegmentStart = new StorePropTag(26513, PropertyType.Int64, new StorePropInfo("MidSegmentStart", 26513, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400102C RID: 4140
			public static readonly StorePropTag MidsetDeleted = new StorePropTag(26514, PropertyType.Binary, new StorePropInfo("MidsetDeleted", 26514, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400102D RID: 4141
			public static readonly StorePropTag MidsetExpired = new StorePropTag(26515, PropertyType.Binary, new StorePropInfo("MidsetExpired", 26515, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400102E RID: 4142
			public static readonly StorePropTag CnsetIn = new StorePropTag(26516, PropertyType.Binary, new StorePropInfo("CnsetIn", 26516, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 16)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400102F RID: 4143
			public static readonly StorePropTag CnsetSeen = new StorePropTag(26518, PropertyType.Binary, new StorePropInfo("CnsetSeen", 26518, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 16)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001030 RID: 4144
			public static readonly StorePropTag MidsetTombstones = new StorePropTag(26520, PropertyType.Binary, new StorePropInfo("MidsetTombstones", 26520, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001031 RID: 4145
			public static readonly StorePropTag GWFolder = new StorePropTag(26522, PropertyType.Boolean, new StorePropInfo("GWFolder", 26522, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001032 RID: 4146
			public static readonly StorePropTag IPMFolder = new StorePropTag(26523, PropertyType.Boolean, new StorePropInfo("IPMFolder", 26523, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001033 RID: 4147
			public static readonly StorePropTag PublicFolderPath = new StorePropTag(26524, PropertyType.Unicode, new StorePropInfo("PublicFolderPath", 26524, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001034 RID: 4148
			public static readonly StorePropTag MidSegmentIndex = new StorePropTag(26527, PropertyType.Int16, new StorePropInfo("MidSegmentIndex", 26527, PropertyType.Int16, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001035 RID: 4149
			public static readonly StorePropTag MidSegmentSize = new StorePropTag(26528, PropertyType.Int16, new StorePropInfo("MidSegmentSize", 26528, PropertyType.Int16, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001036 RID: 4150
			public static readonly StorePropTag CnSegmentStart = new StorePropTag(26529, PropertyType.Int16, new StorePropInfo("CnSegmentStart", 26529, PropertyType.Int16, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001037 RID: 4151
			public static readonly StorePropTag CnSegmentIndex = new StorePropTag(26530, PropertyType.Int16, new StorePropInfo("CnSegmentIndex", 26530, PropertyType.Int16, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001038 RID: 4152
			public static readonly StorePropTag CnSegmentSize = new StorePropTag(26531, PropertyType.Int16, new StorePropInfo("CnSegmentSize", 26531, PropertyType.Int16, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001039 RID: 4153
			public static readonly StorePropTag ChangeNumber = new StorePropTag(26532, PropertyType.Int64, new StorePropInfo("ChangeNumber", 26532, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400103A RID: 4154
			public static readonly StorePropTag ChangeNumberBin = new StorePropTag(26532, PropertyType.Binary, new StorePropInfo("ChangeNumberBin", 26532, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400103B RID: 4155
			public static readonly StorePropTag PCL = new StorePropTag(26533, PropertyType.Binary, new StorePropInfo("PCL", 26533, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400103C RID: 4156
			public static readonly StorePropTag CnMv = new StorePropTag(26534, PropertyType.MVInt64, new StorePropInfo("CnMv", 26534, PropertyType.MVInt64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400103D RID: 4157
			public static readonly StorePropTag FolderTreeRootFID = new StorePropTag(26535, PropertyType.Int64, new StorePropInfo("FolderTreeRootFID", 26535, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400103E RID: 4158
			public static readonly StorePropTag SourceEntryId = new StorePropTag(26536, PropertyType.Binary, new StorePropInfo("SourceEntryId", 26536, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400103F RID: 4159
			public static readonly StorePropTag AnonymousRights = new StorePropTag(26564, PropertyType.Int16, new StorePropInfo("AnonymousRights", 26564, PropertyType.Int16, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001040 RID: 4160
			public static readonly StorePropTag SearchGUID = new StorePropTag(26574, PropertyType.Binary, new StorePropInfo("SearchGUID", 26574, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001041 RID: 4161
			public static readonly StorePropTag CnsetRead = new StorePropTag(26578, PropertyType.Binary, new StorePropInfo("CnsetRead", 26578, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001042 RID: 4162
			public static readonly StorePropTag CnsetSeenFAI = new StorePropTag(26586, PropertyType.Binary, new StorePropInfo("CnsetSeenFAI", 26586, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 16)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001043 RID: 4163
			public static readonly StorePropTag IdSetDeleted = new StorePropTag(26597, PropertyType.Binary, new StorePropInfo("IdSetDeleted", 26597, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001044 RID: 4164
			public static readonly StorePropTag ModifiedCount = new StorePropTag(26614, PropertyType.Int32, new StorePropInfo("ModifiedCount", 26614, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001045 RID: 4165
			public static readonly StorePropTag DeletedState = new StorePropTag(26615, PropertyType.Int32, new StorePropInfo("DeletedState", 26615, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001046 RID: 4166
			public static readonly StorePropTag ptagMsgHeaderTableFID = new StorePropTag(26638, PropertyType.Int64, new StorePropInfo("ptagMsgHeaderTableFID", 26638, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001047 RID: 4167
			public static readonly StorePropTag MailboxNum = new StorePropTag(26655, PropertyType.Int32, new StorePropInfo("MailboxNum", 26655, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001048 RID: 4168
			public static readonly StorePropTag LastUserAccessTime = new StorePropTag(26672, PropertyType.SysTime, new StorePropInfo("LastUserAccessTime", 26672, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x04001049 RID: 4169
			public static readonly StorePropTag LastUserModificationTime = new StorePropTag(26673, PropertyType.SysTime, new StorePropInfo("LastUserModificationTime", 26673, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400104A RID: 4170
			public static readonly StorePropTag SyncCustomState = new StorePropTag(31746, PropertyType.Binary, new StorePropInfo("SyncCustomState", 31746, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400104B RID: 4171
			public static readonly StorePropTag SyncFolderChangeKey = new StorePropTag(31748, PropertyType.Binary, new StorePropInfo("SyncFolderChangeKey", 31748, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400104C RID: 4172
			public static readonly StorePropTag SyncFolderLastModificationTime = new StorePropTag(31749, PropertyType.SysTime, new StorePropInfo("SyncFolderLastModificationTime", 31749, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400104D RID: 4173
			public static readonly StorePropTag ptagSyncState = new StorePropTag(31754, PropertyType.Binary, new StorePropInfo("ptagSyncState", 31754, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Folder);

			// Token: 0x0400104E RID: 4174
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[]
			{
				PropTag.Folder.MaterializedRestrictionSearchRoot,
				PropTag.Folder.MailboxPartitionNumber,
				PropTag.Folder.MailboxNumberInternal,
				PropTag.Folder.QueryCriteriaInternal,
				PropTag.Folder.LastQuotaNotificationTime,
				PropTag.Folder.PropertyPromotionInProgressHiddenItems,
				PropTag.Folder.PropertyPromotionInProgressNormalItems,
				PropTag.Folder.VirtualUnreadMessageCount,
				PropTag.Folder.InternalChangeKey,
				PropTag.Folder.InternalSourceKey,
				PropTag.Folder.ACLDataChecksum,
				PropTag.Folder.ACLData,
				PropTag.Folder.ACLTable,
				PropTag.Folder.ExtendedACLData
			};

			// Token: 0x0400104F RID: 4175
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[]
			{
				PropTag.Folder.ParentEntryId,
				PropTag.Folder.ParentEntryIdSvrEid,
				PropTag.Folder.InstanceKey,
				PropTag.Folder.InstanceKeySvrEid,
				PropTag.Folder.MappingSignature,
				PropTag.Folder.RecordKey,
				PropTag.Folder.RecordKeySvrEid,
				PropTag.Folder.StoreRecordKey,
				PropTag.Folder.StoreEntryId,
				PropTag.Folder.ObjectType,
				PropTag.Folder.EntryId,
				PropTag.Folder.EntryIdSvrEid,
				PropTag.Folder.StoreSupportMask,
				PropTag.Folder.DisplayType,
				PropTag.Folder.MaterializedRestrictionSearchRoot,
				PropTag.Folder.MailboxPartitionNumber,
				PropTag.Folder.MailboxNumberInternal,
				PropTag.Folder.QueryCriteriaInternal,
				PropTag.Folder.LastQuotaNotificationTime,
				PropTag.Folder.PropertyPromotionInProgressHiddenItems,
				PropTag.Folder.PropertyPromotionInProgressNormalItems,
				PropTag.Folder.VirtualUnreadMessageCount,
				PropTag.Folder.InternalChangeKey,
				PropTag.Folder.InternalSourceKey,
				PropTag.Folder.ACLDataChecksum,
				PropTag.Folder.ACLData,
				PropTag.Folder.ACLTable,
				PropTag.Folder.ExtendedACLData,
				PropTag.Folder.MidsetDeleted
			};

			// Token: 0x04001050 RID: 4176
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[]
			{
				PropTag.Folder.ParentEntryId,
				PropTag.Folder.ParentEntryIdSvrEid,
				PropTag.Folder.InstanceKey,
				PropTag.Folder.InstanceKeySvrEid,
				PropTag.Folder.MappingSignature,
				PropTag.Folder.RecordKey,
				PropTag.Folder.RecordKeySvrEid,
				PropTag.Folder.StoreRecordKey,
				PropTag.Folder.StoreEntryId,
				PropTag.Folder.ObjectType,
				PropTag.Folder.EntryId,
				PropTag.Folder.EntryIdSvrEid,
				PropTag.Folder.StoreSupportMask,
				PropTag.Folder.DisplayType,
				PropTag.Folder.MaterializedRestrictionSearchRoot,
				PropTag.Folder.MailboxPartitionNumber,
				PropTag.Folder.MailboxNumberInternal,
				PropTag.Folder.QueryCriteriaInternal,
				PropTag.Folder.LastQuotaNotificationTime,
				PropTag.Folder.PropertyPromotionInProgressHiddenItems,
				PropTag.Folder.PropertyPromotionInProgressNormalItems,
				PropTag.Folder.VirtualUnreadMessageCount,
				PropTag.Folder.InternalChangeKey,
				PropTag.Folder.InternalSourceKey,
				PropTag.Folder.ACLDataChecksum,
				PropTag.Folder.ACLData,
				PropTag.Folder.ACLTable,
				PropTag.Folder.ExtendedACLData,
				PropTag.Folder.MidsetDeleted
			};

			// Token: 0x04001051 RID: 4177
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[]
			{
				PropTag.Folder.MessageSize,
				PropTag.Folder.MessageSize32,
				PropTag.Folder.ParentEntryId,
				PropTag.Folder.ParentEntryIdSvrEid,
				PropTag.Folder.FolderInternetId,
				PropTag.Folder.CreatorSID,
				PropTag.Folder.LastModifierSid,
				PropTag.Folder.SourceFid,
				PropTag.Folder.SubfolderCount,
				PropTag.Folder.DeletedSubfolderCt,
				PropTag.Folder.ReserveRangeOfIDs,
				PropTag.Folder.Access,
				PropTag.Folder.InstanceKey,
				PropTag.Folder.InstanceKeySvrEid,
				PropTag.Folder.AccessLevel,
				PropTag.Folder.RecordKey,
				PropTag.Folder.RecordKeySvrEid,
				PropTag.Folder.EntryId,
				PropTag.Folder.EntryIdSvrEid,
				PropTag.Folder.Depth,
				PropTag.Folder.CreationTime,
				PropTag.Folder.LastModificationTime,
				PropTag.Folder.IPMWastebasketEntryId,
				PropTag.Folder.FolderType,
				PropTag.Folder.ContentCount,
				PropTag.Folder.ContentCountInt64,
				PropTag.Folder.UnreadCount,
				PropTag.Folder.UnreadCountInt64,
				PropTag.Folder.Subfolders,
				PropTag.Folder.ContainerHierarchy,
				PropTag.Folder.ContainerContents,
				PropTag.Folder.FolderAssociatedContents,
				PropTag.Folder.AssociatedContentCount,
				PropTag.Folder.AssociatedContentCountInt64,
				PropTag.Folder.TransportRulesSnapshot,
				PropTag.Folder.TransportRulesSnapshotId,
				PropTag.Folder.CurrentIPMWasteBasketContainerEntryId,
				PropTag.Folder.InternalAccess,
				PropTag.Folder.MaterializedRestrictionSearchRoot,
				PropTag.Folder.MailboxPartitionNumber,
				PropTag.Folder.MailboxNumberInternal,
				PropTag.Folder.QueryCriteriaInternal,
				PropTag.Folder.LastQuotaNotificationTime,
				PropTag.Folder.PropertyPromotionInProgressHiddenItems,
				PropTag.Folder.PropertyPromotionInProgressNormalItems,
				PropTag.Folder.VirtualUnreadMessageCount,
				PropTag.Folder.InternalChangeKey,
				PropTag.Folder.InternalSourceKey,
				PropTag.Folder.CorrelationId,
				PropTag.Folder.LastConflict,
				PropTag.Folder.ACLDataChecksum,
				PropTag.Folder.ACLData,
				PropTag.Folder.ACLTable,
				PropTag.Folder.RulesData,
				PropTag.Folder.RulesTable,
				PropTag.Folder.ExtendedACLData,
				PropTag.Folder.NewAttach,
				PropTag.Folder.StartEmbed,
				PropTag.Folder.EndEmbed,
				PropTag.Folder.StartRecip,
				PropTag.Folder.EndRecip,
				PropTag.Folder.EndCcRecip,
				PropTag.Folder.EndBccRecip,
				PropTag.Folder.EndP1Recip,
				PropTag.Folder.DNPrefix,
				PropTag.Folder.StartTopFolder,
				PropTag.Folder.StartSubFolder,
				PropTag.Folder.EndFolder,
				PropTag.Folder.StartMessage,
				PropTag.Folder.EndMessage,
				PropTag.Folder.EndAttach,
				PropTag.Folder.EcWarning,
				PropTag.Folder.StartFAIMessage,
				PropTag.Folder.NewFXFolder,
				PropTag.Folder.IncrSyncChange,
				PropTag.Folder.IncrSyncDelete,
				PropTag.Folder.IncrSyncEnd,
				PropTag.Folder.IncrSyncMessage,
				PropTag.Folder.FastTransferDelProp,
				PropTag.Folder.IdsetGiven,
				PropTag.Folder.IdsetGivenInt32,
				PropTag.Folder.FastTransferErrorInfo,
				PropTag.Folder.SoftDeletes,
				PropTag.Folder.IdsetRead,
				PropTag.Folder.IdsetUnread,
				PropTag.Folder.IncrSyncRead,
				PropTag.Folder.IncrSyncStateBegin,
				PropTag.Folder.IncrSyncStateEnd,
				PropTag.Folder.IncrSyncImailStream,
				PropTag.Folder.IncrSyncImailStreamContinue,
				PropTag.Folder.IncrSyncImailStreamCancel,
				PropTag.Folder.IncrSyncImailStream2Continue,
				PropTag.Folder.IncrSyncProgressMode,
				PropTag.Folder.SyncProgressPerMsg,
				PropTag.Folder.IncrSyncMsgPartial,
				PropTag.Folder.IncrSyncGroupInfo,
				PropTag.Folder.IncrSyncGroupId,
				PropTag.Folder.IncrSyncChangePartial,
				PropTag.Folder.HierRev,
				PropTag.Folder.SourceKey,
				PropTag.Folder.ParentSourceKey,
				PropTag.Folder.ChangeKey,
				PropTag.Folder.PredecessorChangeList,
				PropTag.Folder.FolderChildCount,
				PropTag.Folder.FolderChildCountInt64,
				PropTag.Folder.Rights,
				PropTag.Folder.HierarchyChangeNumber,
				PropTag.Folder.HasModeratorRules,
				PropTag.Folder.ModeratorRuleCount,
				PropTag.Folder.DeletedMsgCount,
				PropTag.Folder.DeletedMsgCountInt64,
				PropTag.Folder.DeletedFolderCount,
				PropTag.Folder.DeletedAssocMsgCount,
				PropTag.Folder.DeletedAssocMsgCountInt64,
				PropTag.Folder.PromotedProperties,
				PropTag.Folder.HiddenPromotedProperties,
				PropTag.Folder.HasNamedProperties,
				PropTag.Folder.ICSChangeKey,
				PropTag.Folder.DeletedOn,
				PropTag.Folder.DeletedMessageSize,
				PropTag.Folder.DeletedMessageSize32,
				PropTag.Folder.DeletedNormalMessageSize,
				PropTag.Folder.DeletedNormalMessageSize32,
				PropTag.Folder.DeletedAssociatedMessageSize,
				PropTag.Folder.DeletedAssociatedMessageSize32,
				PropTag.Folder.FolderFlags,
				PropTag.Folder.NormalMsgWithAttachCount,
				PropTag.Folder.NormalMsgWithAttachCountInt64,
				PropTag.Folder.AssocMsgWithAttachCount,
				PropTag.Folder.AssocMsgWithAttachCountInt64,
				PropTag.Folder.RecipientOnNormalMsgCount,
				PropTag.Folder.RecipientOnNormalMsgCountInt64,
				PropTag.Folder.RecipientOnAssocMsgCount,
				PropTag.Folder.RecipientOnAssocMsgCountInt64,
				PropTag.Folder.AttachOnNormalMsgCt,
				PropTag.Folder.AttachOnNormalMsgCtInt64,
				PropTag.Folder.AttachOnAssocMsgCt,
				PropTag.Folder.AttachOnAssocMsgCtInt64,
				PropTag.Folder.NormalMessageSize,
				PropTag.Folder.NormalMessageSize32,
				PropTag.Folder.AssociatedMessageSize,
				PropTag.Folder.AssociatedMessageSize32,
				PropTag.Folder.FolderPathName,
				PropTag.Folder.LocalCommitTime,
				PropTag.Folder.LocalCommitTimeMax,
				PropTag.Folder.DeletedCountTotal,
				PropTag.Folder.DeletedCountTotalInt64,
				PropTag.Folder.ScopeFIDs,
				PropTag.Folder.PFOverHardQuotaLimit,
				PropTag.Folder.PFMsgSizeLimit,
				PropTag.Folder.FolderAdminFlags,
				PropTag.Folder.ELCFolderQuota,
				PropTag.Folder.ELCPolicyId,
				PropTag.Folder.ELCPolicyComment,
				PropTag.Folder.PropertyGroupMappingId,
				PropTag.Folder.Fid,
				PropTag.Folder.FidBin,
				PropTag.Folder.ParentFid,
				PropTag.Folder.ParentFidBin,
				PropTag.Folder.ArticleNumNext,
				PropTag.Folder.CnExport,
				PropTag.Folder.PclExport,
				PropTag.Folder.CnMvExport,
				PropTag.Folder.MidsetDeletedExport,
				PropTag.Folder.ArticleNumMic,
				PropTag.Folder.ArticleNumMost,
				PropTag.Folder.RulesSync,
				PropTag.Folder.ReplicaListR,
				PropTag.Folder.ReplicaListRC,
				PropTag.Folder.ReplicaListRBUG,
				PropTag.Folder.RootFid,
				PropTag.Folder.SoftDeleted,
				PropTag.Folder.QuotaStyle,
				PropTag.Folder.StorageQuota,
				PropTag.Folder.FolderPropTagArray,
				PropTag.Folder.MsgFolderPropTagArray,
				PropTag.Folder.SetReceiveCount,
				PropTag.Folder.SubmittedCount,
				PropTag.Folder.CreatorToken,
				PropTag.Folder.SearchState,
				PropTag.Folder.SearchRestriction,
				PropTag.Folder.SearchFIDs,
				PropTag.Folder.RecursiveSearchFIDs,
				PropTag.Folder.SearchBacklinks,
				PropTag.Folder.CategFIDs,
				PropTag.Folder.FolderCDN,
				PropTag.Folder.MidSegmentStart,
				PropTag.Folder.MidsetDeleted,
				PropTag.Folder.MidsetExpired,
				PropTag.Folder.CnsetIn,
				PropTag.Folder.CnsetSeen,
				PropTag.Folder.MidsetTombstones,
				PropTag.Folder.GWFolder,
				PropTag.Folder.IPMFolder,
				PropTag.Folder.PublicFolderPath,
				PropTag.Folder.MidSegmentIndex,
				PropTag.Folder.MidSegmentSize,
				PropTag.Folder.CnSegmentStart,
				PropTag.Folder.CnSegmentIndex,
				PropTag.Folder.CnSegmentSize,
				PropTag.Folder.ChangeNumber,
				PropTag.Folder.ChangeNumberBin,
				PropTag.Folder.PCL,
				PropTag.Folder.CnMv,
				PropTag.Folder.FolderTreeRootFID,
				PropTag.Folder.SourceEntryId,
				PropTag.Folder.AnonymousRights,
				PropTag.Folder.SearchGUID,
				PropTag.Folder.CnsetRead,
				PropTag.Folder.CnsetSeenFAI,
				PropTag.Folder.IdSetDeleted,
				PropTag.Folder.ModifiedCount,
				PropTag.Folder.DeletedState,
				PropTag.Folder.MailboxNum,
				PropTag.Folder.LastUserAccessTime,
				PropTag.Folder.LastUserModificationTime
			};

			// Token: 0x04001052 RID: 4178
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[]
			{
				PropTag.Folder.CreatorSID,
				PropTag.Folder.LastModifierSid,
				PropTag.Folder.CreationTime,
				PropTag.Folder.LastModificationTime,
				PropTag.Folder.IPMWastebasketEntryId,
				PropTag.Folder.CurrentIPMWasteBasketContainerEntryId,
				PropTag.Folder.HierRev,
				PropTag.Folder.PredecessorChangeList,
				PropTag.Folder.HierarchyChangeNumber,
				PropTag.Folder.LocalCommitTime,
				PropTag.Folder.LocalCommitTimeMax,
				PropTag.Folder.ArticleNumNext,
				PropTag.Folder.CnExport,
				PropTag.Folder.PclExport,
				PropTag.Folder.CnMvExport,
				PropTag.Folder.MidsetDeletedExport,
				PropTag.Folder.MidsetDeleted,
				PropTag.Folder.ChangeNumber,
				PropTag.Folder.PCL,
				PropTag.Folder.CnMv,
				PropTag.Folder.SearchGUID,
				PropTag.Folder.LastUserAccessTime,
				PropTag.Folder.LastUserModificationTime
			};

			// Token: 0x04001053 RID: 4179
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[]
			{
				PropTag.Folder.HierRev,
				PropTag.Folder.PFOverHardQuotaLimit,
				PropTag.Folder.PFMsgSizeLimit,
				PropTag.Folder.FolderAdminFlags,
				PropTag.Folder.ELCFolderQuota,
				PropTag.Folder.ELCPolicyId,
				PropTag.Folder.ELCPolicyComment,
				PropTag.Folder.QuotaStyle,
				PropTag.Folder.StorageQuota
			};

			// Token: 0x04001054 RID: 4180
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[]
			{
				PropTag.Folder.TransportRulesSnapshot,
				PropTag.Folder.TransportRulesSnapshotId
			};

			// Token: 0x04001055 RID: 4181
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x04001056 RID: 4182
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x04001057 RID: 4183
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[]
			{
				PropTag.Folder.MessageSize,
				PropTag.Folder.MessageSize32,
				PropTag.Folder.ParentEntryId,
				PropTag.Folder.ParentEntryIdSvrEid,
				PropTag.Folder.FolderInternetId,
				PropTag.Folder.NTSecurityDescriptor,
				PropTag.Folder.AclTableAndSecurityDescriptor,
				PropTag.Folder.CreatorSID,
				PropTag.Folder.LastModifierSid,
				PropTag.Folder.SourceFid,
				PropTag.Folder.SubfolderCount,
				PropTag.Folder.DeletedSubfolderCt,
				PropTag.Folder.ReserveRangeOfIDs,
				PropTag.Folder.FreeBusyNTSD,
				PropTag.Folder.Access,
				PropTag.Folder.InstanceKey,
				PropTag.Folder.InstanceKeySvrEid,
				PropTag.Folder.AccessLevel,
				PropTag.Folder.MappingSignature,
				PropTag.Folder.RecordKey,
				PropTag.Folder.RecordKeySvrEid,
				PropTag.Folder.StoreRecordKey,
				PropTag.Folder.StoreEntryId,
				PropTag.Folder.ObjectType,
				PropTag.Folder.EntryId,
				PropTag.Folder.EntryIdSvrEid,
				PropTag.Folder.Depth,
				PropTag.Folder.CreationTime,
				PropTag.Folder.LastModificationTime,
				PropTag.Folder.StoreSupportMask,
				PropTag.Folder.IPMWastebasketEntryId,
				PropTag.Folder.FolderType,
				PropTag.Folder.ContentCount,
				PropTag.Folder.ContentCountInt64,
				PropTag.Folder.UnreadCount,
				PropTag.Folder.UnreadCountInt64,
				PropTag.Folder.Subfolders,
				PropTag.Folder.ContainerHierarchy,
				PropTag.Folder.ContainerContents,
				PropTag.Folder.FolderAssociatedContents,
				PropTag.Folder.AssociatedContentCount,
				PropTag.Folder.AssociatedContentCountInt64,
				PropTag.Folder.TransportRulesSnapshot,
				PropTag.Folder.TransportRulesSnapshotId,
				PropTag.Folder.CurrentIPMWasteBasketContainerEntryId,
				PropTag.Folder.InternalAccess,
				PropTag.Folder.DisplayType,
				PropTag.Folder.MaterializedRestrictionSearchRoot,
				PropTag.Folder.MailboxPartitionNumber,
				PropTag.Folder.MailboxNumberInternal,
				PropTag.Folder.QueryCriteriaInternal,
				PropTag.Folder.LastQuotaNotificationTime,
				PropTag.Folder.PropertyPromotionInProgressHiddenItems,
				PropTag.Folder.PropertyPromotionInProgressNormalItems,
				PropTag.Folder.VirtualUnreadMessageCount,
				PropTag.Folder.InternalChangeKey,
				PropTag.Folder.InternalSourceKey,
				PropTag.Folder.CorrelationId,
				PropTag.Folder.LastConflict,
				PropTag.Folder.NTSDModificationTime,
				PropTag.Folder.ACLDataChecksum,
				PropTag.Folder.ACLData,
				PropTag.Folder.ACLTable,
				PropTag.Folder.RulesData,
				PropTag.Folder.RulesTable,
				PropTag.Folder.ExtendedACLData,
				PropTag.Folder.NewAttach,
				PropTag.Folder.StartEmbed,
				PropTag.Folder.EndEmbed,
				PropTag.Folder.StartRecip,
				PropTag.Folder.EndRecip,
				PropTag.Folder.EndCcRecip,
				PropTag.Folder.EndBccRecip,
				PropTag.Folder.EndP1Recip,
				PropTag.Folder.DNPrefix,
				PropTag.Folder.StartTopFolder,
				PropTag.Folder.StartSubFolder,
				PropTag.Folder.EndFolder,
				PropTag.Folder.StartMessage,
				PropTag.Folder.EndMessage,
				PropTag.Folder.EndAttach,
				PropTag.Folder.EcWarning,
				PropTag.Folder.StartFAIMessage,
				PropTag.Folder.NewFXFolder,
				PropTag.Folder.IncrSyncChange,
				PropTag.Folder.IncrSyncDelete,
				PropTag.Folder.IncrSyncEnd,
				PropTag.Folder.IncrSyncMessage,
				PropTag.Folder.FastTransferDelProp,
				PropTag.Folder.IdsetGiven,
				PropTag.Folder.IdsetGivenInt32,
				PropTag.Folder.FastTransferErrorInfo,
				PropTag.Folder.SoftDeletes,
				PropTag.Folder.IdsetRead,
				PropTag.Folder.IdsetUnread,
				PropTag.Folder.IncrSyncRead,
				PropTag.Folder.IncrSyncStateBegin,
				PropTag.Folder.IncrSyncStateEnd,
				PropTag.Folder.IncrSyncImailStream,
				PropTag.Folder.IncrSyncImailStreamContinue,
				PropTag.Folder.IncrSyncImailStreamCancel,
				PropTag.Folder.IncrSyncImailStream2Continue,
				PropTag.Folder.IncrSyncProgressMode,
				PropTag.Folder.SyncProgressPerMsg,
				PropTag.Folder.IncrSyncMsgPartial,
				PropTag.Folder.IncrSyncGroupInfo,
				PropTag.Folder.IncrSyncGroupId,
				PropTag.Folder.IncrSyncChangePartial,
				PropTag.Folder.HierRev,
				PropTag.Folder.SourceKey,
				PropTag.Folder.ParentSourceKey,
				PropTag.Folder.ChangeKey,
				PropTag.Folder.PredecessorChangeList,
				PropTag.Folder.FolderChildCount,
				PropTag.Folder.FolderChildCountInt64,
				PropTag.Folder.Rights,
				PropTag.Folder.HierarchyChangeNumber,
				PropTag.Folder.HasModeratorRules,
				PropTag.Folder.ModeratorRuleCount,
				PropTag.Folder.DeletedMsgCount,
				PropTag.Folder.DeletedMsgCountInt64,
				PropTag.Folder.DeletedFolderCount,
				PropTag.Folder.DeletedAssocMsgCount,
				PropTag.Folder.DeletedAssocMsgCountInt64,
				PropTag.Folder.PromotedProperties,
				PropTag.Folder.HiddenPromotedProperties,
				PropTag.Folder.HasNamedProperties,
				PropTag.Folder.ICSChangeKey,
				PropTag.Folder.DeletedOn,
				PropTag.Folder.DeletedMessageSize,
				PropTag.Folder.DeletedMessageSize32,
				PropTag.Folder.DeletedNormalMessageSize,
				PropTag.Folder.DeletedNormalMessageSize32,
				PropTag.Folder.DeletedAssociatedMessageSize,
				PropTag.Folder.DeletedAssociatedMessageSize32,
				PropTag.Folder.FolderFlags,
				PropTag.Folder.NormalMsgWithAttachCount,
				PropTag.Folder.NormalMsgWithAttachCountInt64,
				PropTag.Folder.AssocMsgWithAttachCount,
				PropTag.Folder.AssocMsgWithAttachCountInt64,
				PropTag.Folder.RecipientOnNormalMsgCount,
				PropTag.Folder.RecipientOnNormalMsgCountInt64,
				PropTag.Folder.RecipientOnAssocMsgCount,
				PropTag.Folder.RecipientOnAssocMsgCountInt64,
				PropTag.Folder.AttachOnNormalMsgCt,
				PropTag.Folder.AttachOnNormalMsgCtInt64,
				PropTag.Folder.AttachOnAssocMsgCt,
				PropTag.Folder.AttachOnAssocMsgCtInt64,
				PropTag.Folder.NormalMessageSize,
				PropTag.Folder.NormalMessageSize32,
				PropTag.Folder.AssociatedMessageSize,
				PropTag.Folder.AssociatedMessageSize32,
				PropTag.Folder.FolderPathName,
				PropTag.Folder.LocalCommitTime,
				PropTag.Folder.LocalCommitTimeMax,
				PropTag.Folder.DeletedCountTotal,
				PropTag.Folder.DeletedCountTotalInt64,
				PropTag.Folder.ScopeFIDs,
				PropTag.Folder.PFOverHardQuotaLimit,
				PropTag.Folder.PFMsgSizeLimit,
				PropTag.Folder.FolderAdminFlags,
				PropTag.Folder.ELCFolderQuota,
				PropTag.Folder.ELCPolicyId,
				PropTag.Folder.ELCPolicyComment,
				PropTag.Folder.Fid,
				PropTag.Folder.FidBin,
				PropTag.Folder.ParentFid,
				PropTag.Folder.ParentFidBin,
				PropTag.Folder.ArticleNumNext,
				PropTag.Folder.CnExport,
				PropTag.Folder.PclExport,
				PropTag.Folder.CnMvExport,
				PropTag.Folder.MidsetDeletedExport,
				PropTag.Folder.RulesSync,
				PropTag.Folder.RootFid,
				PropTag.Folder.QuotaStyle,
				PropTag.Folder.StorageQuota,
				PropTag.Folder.FolderPropTagArray,
				PropTag.Folder.MsgFolderPropTagArray,
				PropTag.Folder.SubmittedCount,
				PropTag.Folder.CreatorToken,
				PropTag.Folder.SearchState,
				PropTag.Folder.MidsetDeleted,
				PropTag.Folder.CnsetIn,
				PropTag.Folder.CnsetSeen,
				PropTag.Folder.IPMFolder,
				PropTag.Folder.ChangeNumber,
				PropTag.Folder.ChangeNumberBin,
				PropTag.Folder.PCL,
				PropTag.Folder.CnMv,
				PropTag.Folder.SearchGUID,
				PropTag.Folder.CnsetRead,
				PropTag.Folder.CnsetSeenFAI,
				PropTag.Folder.IdSetDeleted,
				PropTag.Folder.MailboxNum,
				PropTag.Folder.LastUserAccessTime,
				PropTag.Folder.LastUserModificationTime
			};

			// Token: 0x04001058 RID: 4184
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[]
			{
				PropTag.Folder.MessageSize,
				PropTag.Folder.MessageSize32,
				PropTag.Folder.ParentEntryId,
				PropTag.Folder.ParentEntryIdSvrEid,
				PropTag.Folder.FolderInternetId,
				PropTag.Folder.SourceFid,
				PropTag.Folder.SubfolderCount,
				PropTag.Folder.DeletedSubfolderCt,
				PropTag.Folder.Access,
				PropTag.Folder.InstanceKey,
				PropTag.Folder.InstanceKeySvrEid,
				PropTag.Folder.AccessLevel,
				PropTag.Folder.RecordKey,
				PropTag.Folder.RecordKeySvrEid,
				PropTag.Folder.Depth,
				PropTag.Folder.FolderType,
				PropTag.Folder.ContentCount,
				PropTag.Folder.ContentCountInt64,
				PropTag.Folder.UnreadCount,
				PropTag.Folder.UnreadCountInt64,
				PropTag.Folder.Subfolders,
				PropTag.Folder.AssociatedContentCount,
				PropTag.Folder.AssociatedContentCountInt64,
				PropTag.Folder.CorrelationId,
				PropTag.Folder.PublishInAddressBook,
				PropTag.Folder.ResolveMethod,
				PropTag.Folder.AddressBookDisplayName,
				PropTag.Folder.SourceKey,
				PropTag.Folder.ParentSourceKey,
				PropTag.Folder.ChangeKey,
				PropTag.Folder.PredecessorChangeList,
				PropTag.Folder.LISSD,
				PropTag.Folder.FolderChildCount,
				PropTag.Folder.FolderChildCountInt64,
				PropTag.Folder.Rights,
				PropTag.Folder.HasModeratorRules,
				PropTag.Folder.DeletedMsgCount,
				PropTag.Folder.DeletedMsgCountInt64,
				PropTag.Folder.DeletedFolderCount,
				PropTag.Folder.DeletedAssocMsgCount,
				PropTag.Folder.DeletedAssocMsgCountInt64,
				PropTag.Folder.HasNamedProperties,
				PropTag.Folder.DeletedOn,
				PropTag.Folder.DeletedMessageSize,
				PropTag.Folder.DeletedMessageSize32,
				PropTag.Folder.DeletedNormalMessageSize,
				PropTag.Folder.DeletedNormalMessageSize32,
				PropTag.Folder.DeletedAssociatedMessageSize,
				PropTag.Folder.DeletedAssociatedMessageSize32,
				PropTag.Folder.FolderFlags,
				PropTag.Folder.NormalMsgWithAttachCount,
				PropTag.Folder.NormalMsgWithAttachCountInt64,
				PropTag.Folder.AssocMsgWithAttachCount,
				PropTag.Folder.AssocMsgWithAttachCountInt64,
				PropTag.Folder.RecipientOnNormalMsgCount,
				PropTag.Folder.RecipientOnNormalMsgCountInt64,
				PropTag.Folder.RecipientOnAssocMsgCount,
				PropTag.Folder.RecipientOnAssocMsgCountInt64,
				PropTag.Folder.AttachOnNormalMsgCt,
				PropTag.Folder.AttachOnNormalMsgCtInt64,
				PropTag.Folder.AttachOnAssocMsgCt,
				PropTag.Folder.AttachOnAssocMsgCtInt64,
				PropTag.Folder.NormalMessageSize,
				PropTag.Folder.NormalMessageSize32,
				PropTag.Folder.AssociatedMessageSize,
				PropTag.Folder.AssociatedMessageSize32,
				PropTag.Folder.FolderPathName,
				PropTag.Folder.LocalCommitTime,
				PropTag.Folder.LocalCommitTimeMax,
				PropTag.Folder.DeletedCountTotal,
				PropTag.Folder.DeletedCountTotalInt64,
				PropTag.Folder.RootFid,
				PropTag.Folder.SubmittedCount,
				PropTag.Folder.SearchState
			};

			// Token: 0x04001059 RID: 4185
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[]
			{
				PropTag.Folder.CreatorSID,
				PropTag.Folder.CreationTime,
				PropTag.Folder.LastModificationTime,
				PropTag.Folder.HierRev
			};

			// Token: 0x0400105A RID: 4186
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x0400105B RID: 4187
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x0400105C RID: 4188
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x0400105D RID: 4189
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x0400105E RID: 4190
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[]
			{
				PropTag.Folder.MaterializedRestrictionSearchRoot,
				PropTag.Folder.MailboxPartitionNumber,
				PropTag.Folder.MailboxNumberInternal,
				PropTag.Folder.QueryCriteriaInternal,
				PropTag.Folder.LastQuotaNotificationTime,
				PropTag.Folder.PropertyPromotionInProgressHiddenItems,
				PropTag.Folder.PropertyPromotionInProgressNormalItems,
				PropTag.Folder.VirtualUnreadMessageCount,
				PropTag.Folder.InternalChangeKey,
				PropTag.Folder.InternalSourceKey,
				PropTag.Folder.HierarchyChangeNumber,
				PropTag.Folder.CnsetIn,
				PropTag.Folder.CnsetSeen,
				PropTag.Folder.CnsetSeenFAI
			};

			// Token: 0x0400105F RID: 4191
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[]
			{
				PropTag.Folder.AclTableAndSecurityDescriptor,
				PropTag.Folder.OofHistory,
				PropTag.Folder.ReplicaList
			};

			// Token: 0x04001060 RID: 4192
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x04001061 RID: 4193
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.Folder.MessageClass,
				PropTag.Folder.MessageSize,
				PropTag.Folder.MessageSize32,
				PropTag.Folder.ParentEntryId,
				PropTag.Folder.ParentEntryIdSvrEid,
				PropTag.Folder.SentMailEntryId,
				PropTag.Folder.MessageDownloadTime,
				PropTag.Folder.FolderInternetId,
				PropTag.Folder.NTSecurityDescriptor,
				PropTag.Folder.AclTableAndSecurityDescriptor,
				PropTag.Folder.CreatorSID,
				PropTag.Folder.LastModifierSid,
				PropTag.Folder.Catalog,
				PropTag.Folder.CISearchEnabled,
				PropTag.Folder.CINotificationEnabled,
				PropTag.Folder.MaxIndices,
				PropTag.Folder.SourceFid,
				PropTag.Folder.PFContactsGuid,
				PropTag.Folder.SubfolderCount,
				PropTag.Folder.DeletedSubfolderCt,
				PropTag.Folder.MaxCachedViews,
				PropTag.Folder.NTSecurityDescriptorAsXML,
				PropTag.Folder.AdminNTSecurityDescriptorAsXML,
				PropTag.Folder.CreatorSidAsXML,
				PropTag.Folder.LastModifierSidAsXML,
				PropTag.Folder.MergeMidsetDeleted,
				PropTag.Folder.ReserveRangeOfIDs,
				PropTag.Folder.FreeBusyNTSD,
				PropTag.Folder.Access,
				PropTag.Folder.InstanceKey,
				PropTag.Folder.InstanceKeySvrEid,
				PropTag.Folder.AccessLevel,
				PropTag.Folder.MappingSignature,
				PropTag.Folder.RecordKey,
				PropTag.Folder.RecordKeySvrEid,
				PropTag.Folder.StoreRecordKey,
				PropTag.Folder.StoreEntryId,
				PropTag.Folder.ObjectType,
				PropTag.Folder.EntryId,
				PropTag.Folder.EntryIdSvrEid,
				PropTag.Folder.URLCompName,
				PropTag.Folder.AttrHidden,
				PropTag.Folder.AttrSystem,
				PropTag.Folder.AttrReadOnly,
				PropTag.Folder.DisplayName,
				PropTag.Folder.EmailAddress,
				PropTag.Folder.Comment,
				PropTag.Folder.Depth,
				PropTag.Folder.CreationTime,
				PropTag.Folder.LastModificationTime,
				PropTag.Folder.StoreSupportMask,
				PropTag.Folder.IPMWastebasketEntryId,
				PropTag.Folder.IPMCommonViewsEntryId,
				PropTag.Folder.IPMConversationsEntryId,
				PropTag.Folder.IPMAllItemsEntryId,
				PropTag.Folder.IPMSharingEntryId,
				PropTag.Folder.AdminDataEntryId,
				PropTag.Folder.FolderType,
				PropTag.Folder.ContentCount,
				PropTag.Folder.ContentCountInt64,
				PropTag.Folder.UnreadCount,
				PropTag.Folder.UnreadCountInt64,
				PropTag.Folder.Subfolders,
				PropTag.Folder.FolderStatus,
				PropTag.Folder.ContentsSortOrder,
				PropTag.Folder.ContainerHierarchy,
				PropTag.Folder.ContainerContents,
				PropTag.Folder.FolderAssociatedContents,
				PropTag.Folder.ContainerClass,
				PropTag.Folder.ContainerModifyVersion,
				PropTag.Folder.DefaultViewEntryId,
				PropTag.Folder.AssociatedContentCount,
				PropTag.Folder.AssociatedContentCountInt64,
				PropTag.Folder.PackedNamedProps,
				PropTag.Folder.AllowAgeOut,
				PropTag.Folder.SearchFolderMsgCount,
				PropTag.Folder.PartOfContentIndexing,
				PropTag.Folder.OwnerLogonUserConfigurationCache,
				PropTag.Folder.SearchFolderAgeOutTimeout,
				PropTag.Folder.SearchFolderPopulationResult,
				PropTag.Folder.SearchFolderPopulationDiagnostics,
				PropTag.Folder.ConversationTopicHashEntries,
				PropTag.Folder.ContentAggregationFlags,
				PropTag.Folder.TransportRulesSnapshot,
				PropTag.Folder.TransportRulesSnapshotId,
				PropTag.Folder.CurrentIPMWasteBasketContainerEntryId,
				PropTag.Folder.IPMAppointmentEntryId,
				PropTag.Folder.IPMContactEntryId,
				PropTag.Folder.IPMJournalEntryId,
				PropTag.Folder.IPMNoteEntryId,
				PropTag.Folder.IPMTaskEntryId,
				PropTag.Folder.REMOnlineEntryId,
				PropTag.Folder.IPMOfflineEntryId,
				PropTag.Folder.IPMDraftsEntryId,
				PropTag.Folder.AdditionalRENEntryIds,
				PropTag.Folder.AdditionalRENEntryIdsExtended,
				PropTag.Folder.AdditionalRENEntryIdsExtendedMV,
				PropTag.Folder.ExtendedFolderFlags,
				PropTag.Folder.ContainerTimestamp,
				PropTag.Folder.INetUnread,
				PropTag.Folder.NetFolderFlags,
				PropTag.Folder.FolderWebViewInfo,
				PropTag.Folder.FolderWebViewInfoExtended,
				PropTag.Folder.FolderViewFlags,
				PropTag.Folder.FreeBusyEntryIds,
				PropTag.Folder.DefaultPostMsgClass,
				PropTag.Folder.DefaultPostDisplayName,
				PropTag.Folder.FolderViewList,
				PropTag.Folder.AgingPeriod,
				PropTag.Folder.AgingGranularity,
				PropTag.Folder.DefaultFoldersLocaleId,
				PropTag.Folder.InternalAccess,
				PropTag.Folder.SyncEventSuppressGuid,
				PropTag.Folder.DisplayType,
				PropTag.Folder.TestBlobProperty,
				PropTag.Folder.AdminSecurityDescriptor,
				PropTag.Folder.Win32NTSecurityDescriptor,
				PropTag.Folder.NonWin32ACL,
				PropTag.Folder.ItemLevelACL,
				PropTag.Folder.ICSGid,
				PropTag.Folder.SystemFolderFlags,
				PropTag.Folder.MaterializedRestrictionSearchRoot,
				PropTag.Folder.MailboxPartitionNumber,
				PropTag.Folder.MailboxNumberInternal,
				PropTag.Folder.QueryCriteriaInternal,
				PropTag.Folder.LastQuotaNotificationTime,
				PropTag.Folder.PropertyPromotionInProgressHiddenItems,
				PropTag.Folder.PropertyPromotionInProgressNormalItems,
				PropTag.Folder.VirtualUnreadMessageCount,
				PropTag.Folder.InternalChangeKey,
				PropTag.Folder.InternalSourceKey,
				PropTag.Folder.CorrelationId,
				PropTag.Folder.LastConflict,
				PropTag.Folder.NTSDModificationTime,
				PropTag.Folder.ACLDataChecksum,
				PropTag.Folder.ACLData,
				PropTag.Folder.ACLTable,
				PropTag.Folder.RulesData,
				PropTag.Folder.RulesTable,
				PropTag.Folder.OofHistory,
				PropTag.Folder.DesignInProgress,
				PropTag.Folder.SecureOrigination,
				PropTag.Folder.PublishInAddressBook,
				PropTag.Folder.ResolveMethod,
				PropTag.Folder.AddressBookDisplayName,
				PropTag.Folder.EFormsLocaleId,
				PropTag.Folder.ExtendedACLData,
				PropTag.Folder.RulesSize,
				PropTag.Folder.NewAttach,
				PropTag.Folder.StartEmbed,
				PropTag.Folder.EndEmbed,
				PropTag.Folder.StartRecip,
				PropTag.Folder.EndRecip,
				PropTag.Folder.EndCcRecip,
				PropTag.Folder.EndBccRecip,
				PropTag.Folder.EndP1Recip,
				PropTag.Folder.DNPrefix,
				PropTag.Folder.StartTopFolder,
				PropTag.Folder.StartSubFolder,
				PropTag.Folder.EndFolder,
				PropTag.Folder.StartMessage,
				PropTag.Folder.EndMessage,
				PropTag.Folder.EndAttach,
				PropTag.Folder.EcWarning,
				PropTag.Folder.StartFAIMessage,
				PropTag.Folder.NewFXFolder,
				PropTag.Folder.IncrSyncChange,
				PropTag.Folder.IncrSyncDelete,
				PropTag.Folder.IncrSyncEnd,
				PropTag.Folder.IncrSyncMessage,
				PropTag.Folder.FastTransferDelProp,
				PropTag.Folder.IdsetGiven,
				PropTag.Folder.IdsetGivenInt32,
				PropTag.Folder.FastTransferErrorInfo,
				PropTag.Folder.SoftDeletes,
				PropTag.Folder.IdsetRead,
				PropTag.Folder.IdsetUnread,
				PropTag.Folder.IncrSyncRead,
				PropTag.Folder.IncrSyncStateBegin,
				PropTag.Folder.IncrSyncStateEnd,
				PropTag.Folder.IncrSyncImailStream,
				PropTag.Folder.IncrSyncImailStreamContinue,
				PropTag.Folder.IncrSyncImailStreamCancel,
				PropTag.Folder.IncrSyncImailStream2Continue,
				PropTag.Folder.IncrSyncProgressMode,
				PropTag.Folder.SyncProgressPerMsg,
				PropTag.Folder.IncrSyncMsgPartial,
				PropTag.Folder.IncrSyncGroupInfo,
				PropTag.Folder.IncrSyncGroupId,
				PropTag.Folder.IncrSyncChangePartial,
				PropTag.Folder.HierRev,
				PropTag.Folder.SourceKey,
				PropTag.Folder.ParentSourceKey,
				PropTag.Folder.ChangeKey,
				PropTag.Folder.PredecessorChangeList,
				PropTag.Folder.PreventMsgCreate,
				PropTag.Folder.LISSD,
				PropTag.Folder.FavoritesDefaultName,
				PropTag.Folder.FolderChildCount,
				PropTag.Folder.FolderChildCountInt64,
				PropTag.Folder.Rights,
				PropTag.Folder.HasRules,
				PropTag.Folder.AddressBookEntryId,
				PropTag.Folder.HierarchyChangeNumber,
				PropTag.Folder.HasModeratorRules,
				PropTag.Folder.ModeratorRuleCount,
				PropTag.Folder.DeletedMsgCount,
				PropTag.Folder.DeletedMsgCountInt64,
				PropTag.Folder.DeletedFolderCount,
				PropTag.Folder.DeletedAssocMsgCount,
				PropTag.Folder.DeletedAssocMsgCountInt64,
				PropTag.Folder.PromotedProperties,
				PropTag.Folder.HiddenPromotedProperties,
				PropTag.Folder.LinkedSiteAuthorityUrl,
				PropTag.Folder.HasNamedProperties,
				PropTag.Folder.FidMid,
				PropTag.Folder.ICSChangeKey,
				PropTag.Folder.SetPropsCondition,
				PropTag.Folder.DeletedOn,
				PropTag.Folder.ReplicationStyle,
				PropTag.Folder.ReplicationTIB,
				PropTag.Folder.ReplicationMsgPriority,
				PropTag.Folder.ReplicaList,
				PropTag.Folder.OverallAgeLimit,
				PropTag.Folder.DeletedMessageSize,
				PropTag.Folder.DeletedMessageSize32,
				PropTag.Folder.DeletedNormalMessageSize,
				PropTag.Folder.DeletedNormalMessageSize32,
				PropTag.Folder.DeletedAssociatedMessageSize,
				PropTag.Folder.DeletedAssociatedMessageSize32,
				PropTag.Folder.SecureInSite,
				PropTag.Folder.FolderFlags,
				PropTag.Folder.LastAccessTime,
				PropTag.Folder.NormalMsgWithAttachCount,
				PropTag.Folder.NormalMsgWithAttachCountInt64,
				PropTag.Folder.AssocMsgWithAttachCount,
				PropTag.Folder.AssocMsgWithAttachCountInt64,
				PropTag.Folder.RecipientOnNormalMsgCount,
				PropTag.Folder.RecipientOnNormalMsgCountInt64,
				PropTag.Folder.RecipientOnAssocMsgCount,
				PropTag.Folder.RecipientOnAssocMsgCountInt64,
				PropTag.Folder.AttachOnNormalMsgCt,
				PropTag.Folder.AttachOnNormalMsgCtInt64,
				PropTag.Folder.AttachOnAssocMsgCt,
				PropTag.Folder.AttachOnAssocMsgCtInt64,
				PropTag.Folder.NormalMessageSize,
				PropTag.Folder.NormalMessageSize32,
				PropTag.Folder.AssociatedMessageSize,
				PropTag.Folder.AssociatedMessageSize32,
				PropTag.Folder.FolderPathName,
				PropTag.Folder.OwnerCount,
				PropTag.Folder.ContactCount,
				PropTag.Folder.RetentionAgeLimit,
				PropTag.Folder.DisablePerUserRead,
				PropTag.Folder.ServerDN,
				PropTag.Folder.BackfillRanking,
				PropTag.Folder.LastTransmissionTime,
				PropTag.Folder.StatusSendTime,
				PropTag.Folder.BackfillEntryCount,
				PropTag.Folder.NextBroadcastTime,
				PropTag.Folder.NextBackfillTime,
				PropTag.Folder.LastCNBroadcast,
				PropTag.Folder.LastShortCNBroadcast,
				PropTag.Folder.AverageTransmissionTime,
				PropTag.Folder.ReplicationStatus,
				PropTag.Folder.LastDataReceivalTime,
				PropTag.Folder.AdminDisplayName,
				PropTag.Folder.URLName,
				PropTag.Folder.LocalCommitTime,
				PropTag.Folder.LocalCommitTimeMax,
				PropTag.Folder.DeletedCountTotal,
				PropTag.Folder.DeletedCountTotalInt64,
				PropTag.Folder.ScopeFIDs,
				PropTag.Folder.PFAdminDescription,
				PropTag.Folder.PFProxy,
				PropTag.Folder.PFPlatinumHomeMdb,
				PropTag.Folder.PFProxyRequired,
				PropTag.Folder.PFOverHardQuotaLimit,
				PropTag.Folder.PFMsgSizeLimit,
				PropTag.Folder.PFDisallowMdbWideExpiry,
				PropTag.Folder.FolderAdminFlags,
				PropTag.Folder.ProvisionedFID,
				PropTag.Folder.ELCFolderSize,
				PropTag.Folder.ELCFolderQuota,
				PropTag.Folder.ELCPolicyId,
				PropTag.Folder.ELCPolicyComment,
				PropTag.Folder.PropertyGroupMappingId,
				PropTag.Folder.Fid,
				PropTag.Folder.FidBin,
				PropTag.Folder.ParentFid,
				PropTag.Folder.ParentFidBin,
				PropTag.Folder.ArticleNumNext,
				PropTag.Folder.ImapLastArticleId,
				PropTag.Folder.CnExport,
				PropTag.Folder.PclExport,
				PropTag.Folder.CnMvExport,
				PropTag.Folder.MidsetDeletedExport,
				PropTag.Folder.ArticleNumMic,
				PropTag.Folder.ArticleNumMost,
				PropTag.Folder.RulesSync,
				PropTag.Folder.ReplicaListR,
				PropTag.Folder.ReplicaListRC,
				PropTag.Folder.ReplicaListRBUG,
				PropTag.Folder.RootFid,
				PropTag.Folder.SoftDeleted,
				PropTag.Folder.QuotaStyle,
				PropTag.Folder.StorageQuota,
				PropTag.Folder.FolderPropTagArray,
				PropTag.Folder.MsgFolderPropTagArray,
				PropTag.Folder.SetReceiveCount,
				PropTag.Folder.SubmittedCount,
				PropTag.Folder.CreatorToken,
				PropTag.Folder.SearchState,
				PropTag.Folder.SearchRestriction,
				PropTag.Folder.SearchFIDs,
				PropTag.Folder.RecursiveSearchFIDs,
				PropTag.Folder.SearchBacklinks,
				PropTag.Folder.CategFIDs,
				PropTag.Folder.FolderCDN,
				PropTag.Folder.MidSegmentStart,
				PropTag.Folder.MidsetDeleted,
				PropTag.Folder.MidsetExpired,
				PropTag.Folder.CnsetIn,
				PropTag.Folder.CnsetSeen,
				PropTag.Folder.MidsetTombstones,
				PropTag.Folder.GWFolder,
				PropTag.Folder.IPMFolder,
				PropTag.Folder.PublicFolderPath,
				PropTag.Folder.MidSegmentIndex,
				PropTag.Folder.MidSegmentSize,
				PropTag.Folder.CnSegmentStart,
				PropTag.Folder.CnSegmentIndex,
				PropTag.Folder.CnSegmentSize,
				PropTag.Folder.ChangeNumber,
				PropTag.Folder.ChangeNumberBin,
				PropTag.Folder.PCL,
				PropTag.Folder.CnMv,
				PropTag.Folder.FolderTreeRootFID,
				PropTag.Folder.SourceEntryId,
				PropTag.Folder.AnonymousRights,
				PropTag.Folder.SearchGUID,
				PropTag.Folder.CnsetRead,
				PropTag.Folder.CnsetSeenFAI,
				PropTag.Folder.IdSetDeleted,
				PropTag.Folder.ModifiedCount,
				PropTag.Folder.DeletedState,
				PropTag.Folder.ptagMsgHeaderTableFID,
				PropTag.Folder.MailboxNum,
				PropTag.Folder.LastUserAccessTime,
				PropTag.Folder.LastUserModificationTime,
				PropTag.Folder.SyncCustomState,
				PropTag.Folder.SyncFolderChangeKey,
				PropTag.Folder.SyncFolderLastModificationTime,
				PropTag.Folder.ptagSyncState
			};
		}

		// Token: 0x02000008 RID: 8
		public static class Message
		{
			// Token: 0x04001062 RID: 4194
			public static readonly StorePropTag AcknowledgementMode = new StorePropTag(1, PropertyType.Int32, new StorePropInfo("AcknowledgementMode", 1, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001063 RID: 4195
			public static readonly StorePropTag TestTest = new StorePropTag(1, PropertyType.Binary, new StorePropInfo("TestTest", 1, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001064 RID: 4196
			public static readonly StorePropTag AlternateRecipientAllowed = new StorePropTag(2, PropertyType.Boolean, new StorePropInfo("AlternateRecipientAllowed", 2, PropertyType.Boolean, StorePropInfo.Flags.None, 1UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001065 RID: 4197
			public static readonly StorePropTag AuthorizingUsers = new StorePropTag(3, PropertyType.Binary, new StorePropInfo("AuthorizingUsers", 3, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001066 RID: 4198
			public static readonly StorePropTag AutoForwardComment = new StorePropTag(4, PropertyType.Unicode, new StorePropInfo("AutoForwardComment", 4, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001067 RID: 4199
			public static readonly StorePropTag AutoForwarded = new StorePropTag(5, PropertyType.Boolean, new StorePropInfo("AutoForwarded", 5, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001068 RID: 4200
			public static readonly StorePropTag ContentConfidentialityAlgorithmId = new StorePropTag(6, PropertyType.Binary, new StorePropInfo("ContentConfidentialityAlgorithmId", 6, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001069 RID: 4201
			public static readonly StorePropTag ContentCorrelator = new StorePropTag(7, PropertyType.Binary, new StorePropInfo("ContentCorrelator", 7, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400106A RID: 4202
			public static readonly StorePropTag ContentIdentifier = new StorePropTag(8, PropertyType.Unicode, new StorePropInfo("ContentIdentifier", 8, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400106B RID: 4203
			public static readonly StorePropTag ContentLength = new StorePropTag(9, PropertyType.Int32, new StorePropInfo("ContentLength", 9, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400106C RID: 4204
			public static readonly StorePropTag ContentReturnRequested = new StorePropTag(10, PropertyType.Boolean, new StorePropInfo("ContentReturnRequested", 10, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400106D RID: 4205
			public static readonly StorePropTag ConversationKey = new StorePropTag(11, PropertyType.Binary, new StorePropInfo("ConversationKey", 11, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400106E RID: 4206
			public static readonly StorePropTag ConversionEits = new StorePropTag(12, PropertyType.Binary, new StorePropInfo("ConversionEits", 12, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400106F RID: 4207
			public static readonly StorePropTag ConversionWithLossProhibited = new StorePropTag(13, PropertyType.Boolean, new StorePropInfo("ConversionWithLossProhibited", 13, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001070 RID: 4208
			public static readonly StorePropTag ConvertedEits = new StorePropTag(14, PropertyType.Binary, new StorePropInfo("ConvertedEits", 14, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001071 RID: 4209
			public static readonly StorePropTag DeferredDeliveryTime = new StorePropTag(15, PropertyType.SysTime, new StorePropInfo("DeferredDeliveryTime", 15, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001072 RID: 4210
			public static readonly StorePropTag DeliverTime = new StorePropTag(16, PropertyType.SysTime, new StorePropInfo("DeliverTime", 16, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001073 RID: 4211
			public static readonly StorePropTag DiscardReason = new StorePropTag(17, PropertyType.Int32, new StorePropInfo("DiscardReason", 17, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001074 RID: 4212
			public static readonly StorePropTag DisclosureOfRecipients = new StorePropTag(18, PropertyType.Boolean, new StorePropInfo("DisclosureOfRecipients", 18, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001075 RID: 4213
			public static readonly StorePropTag DLExpansionHistory = new StorePropTag(19, PropertyType.Binary, new StorePropInfo("DLExpansionHistory", 19, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001076 RID: 4214
			public static readonly StorePropTag DLExpansionProhibited = new StorePropTag(20, PropertyType.Boolean, new StorePropInfo("DLExpansionProhibited", 20, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001077 RID: 4215
			public static readonly StorePropTag ExpiryTime = new StorePropTag(21, PropertyType.SysTime, new StorePropInfo("ExpiryTime", 21, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001078 RID: 4216
			public static readonly StorePropTag ImplicitConversionProhibited = new StorePropTag(22, PropertyType.Boolean, new StorePropInfo("ImplicitConversionProhibited", 22, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001079 RID: 4217
			public static readonly StorePropTag Importance = new StorePropTag(23, PropertyType.Int32, new StorePropInfo("Importance", 23, PropertyType.Int32, StorePropInfo.Flags.None, 2341871806232657924UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400107A RID: 4218
			public static readonly StorePropTag IPMID = new StorePropTag(24, PropertyType.Binary, new StorePropInfo("IPMID", 24, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400107B RID: 4219
			public static readonly StorePropTag LatestDeliveryTime = new StorePropTag(25, PropertyType.SysTime, new StorePropInfo("LatestDeliveryTime", 25, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400107C RID: 4220
			public static readonly StorePropTag MessageClass = new StorePropTag(26, PropertyType.Unicode, new StorePropInfo("MessageClass", 26, PropertyType.Unicode, StorePropInfo.Flags.None, 2341871806232658048UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400107D RID: 4221
			public static readonly StorePropTag MessageDeliveryId = new StorePropTag(27, PropertyType.Binary, new StorePropInfo("MessageDeliveryId", 27, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400107E RID: 4222
			public static readonly StorePropTag MessageSecurityLabel = new StorePropTag(30, PropertyType.Binary, new StorePropInfo("MessageSecurityLabel", 30, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400107F RID: 4223
			public static readonly StorePropTag ObsoletedIPMS = new StorePropTag(31, PropertyType.Binary, new StorePropInfo("ObsoletedIPMS", 31, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001080 RID: 4224
			public static readonly StorePropTag OriginallyIntendedRecipientName = new StorePropTag(32, PropertyType.Binary, new StorePropInfo("OriginallyIntendedRecipientName", 32, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001081 RID: 4225
			public static readonly StorePropTag OriginalEITS = new StorePropTag(33, PropertyType.Binary, new StorePropInfo("OriginalEITS", 33, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001082 RID: 4226
			public static readonly StorePropTag OriginatorCertificate = new StorePropTag(34, PropertyType.Binary, new StorePropInfo("OriginatorCertificate", 34, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001083 RID: 4227
			public static readonly StorePropTag DeliveryReportRequested = new StorePropTag(35, PropertyType.Boolean, new StorePropInfo("DeliveryReportRequested", 35, PropertyType.Boolean, StorePropInfo.Flags.None, 4UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001084 RID: 4228
			public static readonly StorePropTag OriginatorReturnAddress = new StorePropTag(36, PropertyType.Binary, new StorePropInfo("OriginatorReturnAddress", 36, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(18)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001085 RID: 4229
			public static readonly StorePropTag ParentKey = new StorePropTag(37, PropertyType.Binary, new StorePropInfo("ParentKey", 37, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001086 RID: 4230
			public static readonly StorePropTag Priority = new StorePropTag(38, PropertyType.Int32, new StorePropInfo("Priority", 38, PropertyType.Int32, StorePropInfo.Flags.None, 4UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001087 RID: 4231
			public static readonly StorePropTag OriginCheck = new StorePropTag(39, PropertyType.Binary, new StorePropInfo("OriginCheck", 39, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001088 RID: 4232
			public static readonly StorePropTag ProofOfSubmissionRequested = new StorePropTag(40, PropertyType.Boolean, new StorePropInfo("ProofOfSubmissionRequested", 40, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001089 RID: 4233
			public static readonly StorePropTag ReadReceiptRequested = new StorePropTag(41, PropertyType.Boolean, new StorePropInfo("ReadReceiptRequested", 41, PropertyType.Boolean, StorePropInfo.Flags.None, 2UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400108A RID: 4234
			public static readonly StorePropTag ReceiptTime = new StorePropTag(42, PropertyType.SysTime, new StorePropInfo("ReceiptTime", 42, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400108B RID: 4235
			public static readonly StorePropTag RecipientReassignmentProhibited = new StorePropTag(43, PropertyType.Boolean, new StorePropInfo("RecipientReassignmentProhibited", 43, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400108C RID: 4236
			public static readonly StorePropTag RedirectionHistory = new StorePropTag(44, PropertyType.Binary, new StorePropInfo("RedirectionHistory", 44, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400108D RID: 4237
			public static readonly StorePropTag RelatedIPMS = new StorePropTag(45, PropertyType.Binary, new StorePropInfo("RelatedIPMS", 45, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400108E RID: 4238
			public static readonly StorePropTag OriginalSensitivity = new StorePropTag(46, PropertyType.Int32, new StorePropInfo("OriginalSensitivity", 46, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400108F RID: 4239
			public static readonly StorePropTag Languages = new StorePropTag(47, PropertyType.Unicode, new StorePropInfo("Languages", 47, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001090 RID: 4240
			public static readonly StorePropTag ReplyTime = new StorePropTag(48, PropertyType.SysTime, new StorePropInfo("ReplyTime", 48, PropertyType.SysTime, StorePropInfo.Flags.None, 2UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001091 RID: 4241
			public static readonly StorePropTag ReportTag = new StorePropTag(49, PropertyType.Binary, new StorePropInfo("ReportTag", 49, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001092 RID: 4242
			public static readonly StorePropTag ReportTime = new StorePropTag(50, PropertyType.SysTime, new StorePropInfo("ReportTime", 50, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001093 RID: 4243
			public static readonly StorePropTag ReturnedIPM = new StorePropTag(51, PropertyType.Boolean, new StorePropInfo("ReturnedIPM", 51, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001094 RID: 4244
			public static readonly StorePropTag Security = new StorePropTag(52, PropertyType.Int32, new StorePropInfo("Security", 52, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001095 RID: 4245
			public static readonly StorePropTag IncompleteCopy = new StorePropTag(53, PropertyType.Boolean, new StorePropInfo("IncompleteCopy", 53, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001096 RID: 4246
			public static readonly StorePropTag Sensitivity = new StorePropTag(54, PropertyType.Int32, new StorePropInfo("Sensitivity", 54, PropertyType.Int32, StorePropInfo.Flags.None, 4UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001097 RID: 4247
			public static readonly StorePropTag Subject = new StorePropTag(55, PropertyType.Unicode, new StorePropInfo("Subject", 55, PropertyType.Unicode, StorePropInfo.Flags.None, 2341871806232658048UL, new PropertyCategories(1, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001098 RID: 4248
			public static readonly StorePropTag SubjectIPM = new StorePropTag(56, PropertyType.Binary, new StorePropInfo("SubjectIPM", 56, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001099 RID: 4249
			public static readonly StorePropTag ClientSubmitTime = new StorePropTag(57, PropertyType.SysTime, new StorePropInfo("ClientSubmitTime", 57, PropertyType.SysTime, StorePropInfo.Flags.None, 2305843009213693953UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400109A RID: 4250
			public static readonly StorePropTag ReportName = new StorePropTag(58, PropertyType.Unicode, new StorePropInfo("ReportName", 58, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400109B RID: 4251
			public static readonly StorePropTag SentRepresentingSearchKey = new StorePropTag(59, PropertyType.Binary, new StorePropInfo("SentRepresentingSearchKey", 59, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400109C RID: 4252
			public static readonly StorePropTag X400ContentType = new StorePropTag(60, PropertyType.Binary, new StorePropInfo("X400ContentType", 60, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400109D RID: 4253
			public static readonly StorePropTag SubjectPrefix = new StorePropTag(61, PropertyType.Unicode, new StorePropInfo("SubjectPrefix", 61, PropertyType.Unicode, StorePropInfo.Flags.None, 2341871806232658048UL, new PropertyCategories(1)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400109E RID: 4254
			public static readonly StorePropTag NonReceiptReason = new StorePropTag(62, PropertyType.Int32, new StorePropInfo("NonReceiptReason", 62, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400109F RID: 4255
			public static readonly StorePropTag ReceivedByEntryId = new StorePropTag(63, PropertyType.Binary, new StorePropInfo("ReceivedByEntryId", 63, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010A0 RID: 4256
			public static readonly StorePropTag ReceivedByName = new StorePropTag(64, PropertyType.Unicode, new StorePropInfo("ReceivedByName", 64, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010A1 RID: 4257
			public static readonly StorePropTag SentRepresentingEntryId = new StorePropTag(65, PropertyType.Binary, new StorePropInfo("SentRepresentingEntryId", 65, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010A2 RID: 4258
			public static readonly StorePropTag SentRepresentingName = new StorePropTag(66, PropertyType.Unicode, new StorePropInfo("SentRepresentingName", 66, PropertyType.Unicode, StorePropInfo.Flags.None, 2341871806232658048UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010A3 RID: 4259
			public static readonly StorePropTag ReceivedRepresentingEntryId = new StorePropTag(67, PropertyType.Binary, new StorePropInfo("ReceivedRepresentingEntryId", 67, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010A4 RID: 4260
			public static readonly StorePropTag ReceivedRepresentingName = new StorePropTag(68, PropertyType.Unicode, new StorePropInfo("ReceivedRepresentingName", 68, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010A5 RID: 4261
			public static readonly StorePropTag ReportEntryId = new StorePropTag(69, PropertyType.Binary, new StorePropInfo("ReportEntryId", 69, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010A6 RID: 4262
			public static readonly StorePropTag ReadReceiptEntryId = new StorePropTag(70, PropertyType.Binary, new StorePropInfo("ReadReceiptEntryId", 70, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010A7 RID: 4263
			public static readonly StorePropTag MessageSubmissionId = new StorePropTag(71, PropertyType.Binary, new StorePropInfo("MessageSubmissionId", 71, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010A8 RID: 4264
			public static readonly StorePropTag ProviderSubmitTime = new StorePropTag(72, PropertyType.SysTime, new StorePropInfo("ProviderSubmitTime", 72, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010A9 RID: 4265
			public static readonly StorePropTag OriginalSubject = new StorePropTag(73, PropertyType.Unicode, new StorePropInfo("OriginalSubject", 73, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010AA RID: 4266
			public static readonly StorePropTag DiscVal = new StorePropTag(74, PropertyType.Boolean, new StorePropInfo("DiscVal", 74, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010AB RID: 4267
			public static readonly StorePropTag OriginalMessageClass = new StorePropTag(75, PropertyType.Unicode, new StorePropInfo("OriginalMessageClass", 75, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010AC RID: 4268
			public static readonly StorePropTag OriginalAuthorEntryId = new StorePropTag(76, PropertyType.Binary, new StorePropInfo("OriginalAuthorEntryId", 76, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010AD RID: 4269
			public static readonly StorePropTag OriginalAuthorName = new StorePropTag(77, PropertyType.Unicode, new StorePropInfo("OriginalAuthorName", 77, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010AE RID: 4270
			public static readonly StorePropTag OriginalSubmitTime = new StorePropTag(78, PropertyType.SysTime, new StorePropInfo("OriginalSubmitTime", 78, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010AF RID: 4271
			public static readonly StorePropTag ReplyRecipientEntries = new StorePropTag(79, PropertyType.Binary, new StorePropInfo("ReplyRecipientEntries", 79, PropertyType.Binary, StorePropInfo.Flags.None, 16UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010B0 RID: 4272
			public static readonly StorePropTag ReplyRecipientNames = new StorePropTag(80, PropertyType.Unicode, new StorePropInfo("ReplyRecipientNames", 80, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010B1 RID: 4273
			public static readonly StorePropTag ReceivedBySearchKey = new StorePropTag(81, PropertyType.Binary, new StorePropInfo("ReceivedBySearchKey", 81, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010B2 RID: 4274
			public static readonly StorePropTag ReceivedRepresentingSearchKey = new StorePropTag(82, PropertyType.Binary, new StorePropInfo("ReceivedRepresentingSearchKey", 82, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010B3 RID: 4275
			public static readonly StorePropTag ReadReceiptSearchKey = new StorePropTag(83, PropertyType.Binary, new StorePropInfo("ReadReceiptSearchKey", 83, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010B4 RID: 4276
			public static readonly StorePropTag ReportSearchKey = new StorePropTag(84, PropertyType.Binary, new StorePropInfo("ReportSearchKey", 84, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010B5 RID: 4277
			public static readonly StorePropTag OriginalDeliveryTime = new StorePropTag(85, PropertyType.SysTime, new StorePropInfo("OriginalDeliveryTime", 85, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010B6 RID: 4278
			public static readonly StorePropTag OriginalAuthorSearchKey = new StorePropTag(86, PropertyType.Binary, new StorePropInfo("OriginalAuthorSearchKey", 86, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010B7 RID: 4279
			public static readonly StorePropTag MessageToMe = new StorePropTag(87, PropertyType.Boolean, new StorePropInfo("MessageToMe", 87, PropertyType.Boolean, StorePropInfo.Flags.None, 1UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010B8 RID: 4280
			public static readonly StorePropTag MessageCCMe = new StorePropTag(88, PropertyType.Boolean, new StorePropInfo("MessageCCMe", 88, PropertyType.Boolean, StorePropInfo.Flags.None, 1UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010B9 RID: 4281
			public static readonly StorePropTag MessageRecipMe = new StorePropTag(89, PropertyType.Boolean, new StorePropInfo("MessageRecipMe", 89, PropertyType.Boolean, StorePropInfo.Flags.None, 1UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010BA RID: 4282
			public static readonly StorePropTag OriginalSenderName = new StorePropTag(90, PropertyType.Unicode, new StorePropInfo("OriginalSenderName", 90, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010BB RID: 4283
			public static readonly StorePropTag OriginalSenderEntryId = new StorePropTag(91, PropertyType.Binary, new StorePropInfo("OriginalSenderEntryId", 91, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010BC RID: 4284
			public static readonly StorePropTag OriginalSenderSearchKey = new StorePropTag(92, PropertyType.Binary, new StorePropInfo("OriginalSenderSearchKey", 92, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010BD RID: 4285
			public static readonly StorePropTag OriginalSentRepresentingName = new StorePropTag(93, PropertyType.Unicode, new StorePropInfo("OriginalSentRepresentingName", 93, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010BE RID: 4286
			public static readonly StorePropTag OriginalSentRepresentingEntryId = new StorePropTag(94, PropertyType.Binary, new StorePropInfo("OriginalSentRepresentingEntryId", 94, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010BF RID: 4287
			public static readonly StorePropTag OriginalSentRepresentingSearchKey = new StorePropTag(95, PropertyType.Binary, new StorePropInfo("OriginalSentRepresentingSearchKey", 95, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010C0 RID: 4288
			public static readonly StorePropTag StartDate = new StorePropTag(96, PropertyType.SysTime, new StorePropInfo("StartDate", 96, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010C1 RID: 4289
			public static readonly StorePropTag EndDate = new StorePropTag(97, PropertyType.SysTime, new StorePropInfo("EndDate", 97, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010C2 RID: 4290
			public static readonly StorePropTag OwnerApptId = new StorePropTag(98, PropertyType.Int32, new StorePropInfo("OwnerApptId", 98, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010C3 RID: 4291
			public static readonly StorePropTag ResponseRequested = new StorePropTag(99, PropertyType.Boolean, new StorePropInfo("ResponseRequested", 99, PropertyType.Boolean, StorePropInfo.Flags.None, 4096UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010C4 RID: 4292
			public static readonly StorePropTag SentRepresentingAddressType = new StorePropTag(100, PropertyType.Unicode, new StorePropInfo("SentRepresentingAddressType", 100, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010C5 RID: 4293
			public static readonly StorePropTag SentRepresentingEmailAddress = new StorePropTag(101, PropertyType.Unicode, new StorePropInfo("SentRepresentingEmailAddress", 101, PropertyType.Unicode, StorePropInfo.Flags.None, 128UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010C6 RID: 4294
			public static readonly StorePropTag OriginalSenderAddressType = new StorePropTag(102, PropertyType.Unicode, new StorePropInfo("OriginalSenderAddressType", 102, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010C7 RID: 4295
			public static readonly StorePropTag OriginalSenderEmailAddress = new StorePropTag(103, PropertyType.Unicode, new StorePropInfo("OriginalSenderEmailAddress", 103, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010C8 RID: 4296
			public static readonly StorePropTag OriginalSentRepresentingAddressType = new StorePropTag(104, PropertyType.Unicode, new StorePropInfo("OriginalSentRepresentingAddressType", 104, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010C9 RID: 4297
			public static readonly StorePropTag OriginalSentRepresentingEmailAddress = new StorePropTag(105, PropertyType.Unicode, new StorePropInfo("OriginalSentRepresentingEmailAddress", 105, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010CA RID: 4298
			public static readonly StorePropTag ConversationTopic = new StorePropTag(112, PropertyType.Unicode, new StorePropInfo("ConversationTopic", 112, PropertyType.Unicode, StorePropInfo.Flags.None, 2341871806232658048UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010CB RID: 4299
			public static readonly StorePropTag ConversationIndex = new StorePropTag(113, PropertyType.Binary, new StorePropInfo("ConversationIndex", 113, PropertyType.Binary, StorePropInfo.Flags.None, 2341871806232658048UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010CC RID: 4300
			public static readonly StorePropTag OriginalDisplayBcc = new StorePropTag(114, PropertyType.Unicode, new StorePropInfo("OriginalDisplayBcc", 114, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010CD RID: 4301
			public static readonly StorePropTag OriginalDisplayCc = new StorePropTag(115, PropertyType.Unicode, new StorePropInfo("OriginalDisplayCc", 115, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010CE RID: 4302
			public static readonly StorePropTag OriginalDisplayTo = new StorePropTag(116, PropertyType.Unicode, new StorePropInfo("OriginalDisplayTo", 116, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010CF RID: 4303
			public static readonly StorePropTag ReceivedByAddressType = new StorePropTag(117, PropertyType.Unicode, new StorePropInfo("ReceivedByAddressType", 117, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010D0 RID: 4304
			public static readonly StorePropTag ReceivedByEmailAddress = new StorePropTag(118, PropertyType.Unicode, new StorePropInfo("ReceivedByEmailAddress", 118, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010D1 RID: 4305
			public static readonly StorePropTag ReceivedRepresentingAddressType = new StorePropTag(119, PropertyType.Unicode, new StorePropInfo("ReceivedRepresentingAddressType", 119, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010D2 RID: 4306
			public static readonly StorePropTag ReceivedRepresentingEmailAddress = new StorePropTag(120, PropertyType.Unicode, new StorePropInfo("ReceivedRepresentingEmailAddress", 120, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010D3 RID: 4307
			public static readonly StorePropTag OriginalAuthorAddressType = new StorePropTag(121, PropertyType.Unicode, new StorePropInfo("OriginalAuthorAddressType", 121, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010D4 RID: 4308
			public static readonly StorePropTag OriginalAuthorEmailAddress = new StorePropTag(122, PropertyType.Unicode, new StorePropInfo("OriginalAuthorEmailAddress", 122, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010D5 RID: 4309
			public static readonly StorePropTag OriginallyIntendedRecipientAddressType = new StorePropTag(124, PropertyType.Unicode, new StorePropInfo("OriginallyIntendedRecipientAddressType", 124, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010D6 RID: 4310
			public static readonly StorePropTag TransportMessageHeaders = new StorePropTag(125, PropertyType.Unicode, new StorePropInfo("TransportMessageHeaders", 125, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010D7 RID: 4311
			public static readonly StorePropTag Delegation = new StorePropTag(126, PropertyType.Binary, new StorePropInfo("Delegation", 126, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010D8 RID: 4312
			public static readonly StorePropTag ReportDisposition = new StorePropTag(128, PropertyType.Unicode, new StorePropInfo("ReportDisposition", 128, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010D9 RID: 4313
			public static readonly StorePropTag ReportDispositionMode = new StorePropTag(129, PropertyType.Unicode, new StorePropInfo("ReportDispositionMode", 129, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010DA RID: 4314
			public static readonly StorePropTag ReportOriginalSender = new StorePropTag(130, PropertyType.Unicode, new StorePropInfo("ReportOriginalSender", 130, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010DB RID: 4315
			public static readonly StorePropTag ReportDispositionToNames = new StorePropTag(131, PropertyType.Unicode, new StorePropInfo("ReportDispositionToNames", 131, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010DC RID: 4316
			public static readonly StorePropTag ReportDispositionToEmailAddress = new StorePropTag(132, PropertyType.Unicode, new StorePropInfo("ReportDispositionToEmailAddress", 132, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010DD RID: 4317
			public static readonly StorePropTag ReportDispositionOptions = new StorePropTag(133, PropertyType.Unicode, new StorePropInfo("ReportDispositionOptions", 133, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010DE RID: 4318
			public static readonly StorePropTag RichContent = new StorePropTag(134, PropertyType.Int16, new StorePropInfo("RichContent", 134, PropertyType.Int16, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010DF RID: 4319
			public static readonly StorePropTag AdministratorEMail = new StorePropTag(256, PropertyType.MVUnicode, new StorePropInfo("AdministratorEMail", 256, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010E0 RID: 4320
			public static readonly StorePropTag ContentIntegrityCheck = new StorePropTag(3072, PropertyType.Binary, new StorePropInfo("ContentIntegrityCheck", 3072, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010E1 RID: 4321
			public static readonly StorePropTag ExplicitConversion = new StorePropTag(3073, PropertyType.Int32, new StorePropInfo("ExplicitConversion", 3073, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010E2 RID: 4322
			public static readonly StorePropTag ReturnRequested = new StorePropTag(3074, PropertyType.Boolean, new StorePropInfo("ReturnRequested", 3074, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010E3 RID: 4323
			public static readonly StorePropTag MessageToken = new StorePropTag(3075, PropertyType.Binary, new StorePropInfo("MessageToken", 3075, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010E4 RID: 4324
			public static readonly StorePropTag NDRReasonCode = new StorePropTag(3076, PropertyType.Int32, new StorePropInfo("NDRReasonCode", 3076, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010E5 RID: 4325
			public static readonly StorePropTag NDRDiagCode = new StorePropTag(3077, PropertyType.Int32, new StorePropInfo("NDRDiagCode", 3077, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010E6 RID: 4326
			public static readonly StorePropTag NonReceiptNotificationRequested = new StorePropTag(3078, PropertyType.Boolean, new StorePropInfo("NonReceiptNotificationRequested", 3078, PropertyType.Boolean, StorePropInfo.Flags.None, 2UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010E7 RID: 4327
			public static readonly StorePropTag DeliveryPoint = new StorePropTag(3079, PropertyType.Int32, new StorePropInfo("DeliveryPoint", 3079, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010E8 RID: 4328
			public static readonly StorePropTag NonDeliveryReportRequested = new StorePropTag(3080, PropertyType.Boolean, new StorePropInfo("NonDeliveryReportRequested", 3080, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010E9 RID: 4329
			public static readonly StorePropTag OriginatorRequestedAlterateRecipient = new StorePropTag(3081, PropertyType.Binary, new StorePropInfo("OriginatorRequestedAlterateRecipient", 3081, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010EA RID: 4330
			public static readonly StorePropTag PhysicalDeliveryBureauFaxDelivery = new StorePropTag(3082, PropertyType.Boolean, new StorePropInfo("PhysicalDeliveryBureauFaxDelivery", 3082, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010EB RID: 4331
			public static readonly StorePropTag PhysicalDeliveryMode = new StorePropTag(3083, PropertyType.Int32, new StorePropInfo("PhysicalDeliveryMode", 3083, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010EC RID: 4332
			public static readonly StorePropTag PhysicalDeliveryReportRequest = new StorePropTag(3084, PropertyType.Int32, new StorePropInfo("PhysicalDeliveryReportRequest", 3084, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010ED RID: 4333
			public static readonly StorePropTag PhysicalForwardingAddress = new StorePropTag(3085, PropertyType.Binary, new StorePropInfo("PhysicalForwardingAddress", 3085, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010EE RID: 4334
			public static readonly StorePropTag PhysicalForwardingAddressRequested = new StorePropTag(3086, PropertyType.Boolean, new StorePropInfo("PhysicalForwardingAddressRequested", 3086, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010EF RID: 4335
			public static readonly StorePropTag PhysicalForwardingProhibited = new StorePropTag(3087, PropertyType.Boolean, new StorePropInfo("PhysicalForwardingProhibited", 3087, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010F0 RID: 4336
			public static readonly StorePropTag ProofOfDelivery = new StorePropTag(3089, PropertyType.Binary, new StorePropInfo("ProofOfDelivery", 3089, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010F1 RID: 4337
			public static readonly StorePropTag ProofOfDeliveryRequested = new StorePropTag(3090, PropertyType.Boolean, new StorePropInfo("ProofOfDeliveryRequested", 3090, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010F2 RID: 4338
			public static readonly StorePropTag RecipientCertificate = new StorePropTag(3091, PropertyType.Binary, new StorePropInfo("RecipientCertificate", 3091, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010F3 RID: 4339
			public static readonly StorePropTag RecipientNumberForAdvice = new StorePropTag(3092, PropertyType.Unicode, new StorePropInfo("RecipientNumberForAdvice", 3092, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010F4 RID: 4340
			public static readonly StorePropTag RecipientType = new StorePropTag(3093, PropertyType.Int32, new StorePropInfo("RecipientType", 3093, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010F5 RID: 4341
			public static readonly StorePropTag RegisteredMailType = new StorePropTag(3094, PropertyType.Int32, new StorePropInfo("RegisteredMailType", 3094, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010F6 RID: 4342
			public static readonly StorePropTag ReplyRequested = new StorePropTag(3095, PropertyType.Boolean, new StorePropInfo("ReplyRequested", 3095, PropertyType.Boolean, StorePropInfo.Flags.None, 4096UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010F7 RID: 4343
			public static readonly StorePropTag RequestedDeliveryMethod = new StorePropTag(3096, PropertyType.Int32, new StorePropInfo("RequestedDeliveryMethod", 3096, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010F8 RID: 4344
			public static readonly StorePropTag SenderEntryId = new StorePropTag(3097, PropertyType.Binary, new StorePropInfo("SenderEntryId", 3097, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010F9 RID: 4345
			public static readonly StorePropTag SenderName = new StorePropTag(3098, PropertyType.Unicode, new StorePropInfo("SenderName", 3098, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694080UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010FA RID: 4346
			public static readonly StorePropTag SupplementaryInfo = new StorePropTag(3099, PropertyType.Unicode, new StorePropInfo("SupplementaryInfo", 3099, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010FB RID: 4347
			public static readonly StorePropTag TypeOfMTSUser = new StorePropTag(3100, PropertyType.Int32, new StorePropInfo("TypeOfMTSUser", 3100, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010FC RID: 4348
			public static readonly StorePropTag SenderSearchKey = new StorePropTag(3101, PropertyType.Binary, new StorePropInfo("SenderSearchKey", 3101, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010FD RID: 4349
			public static readonly StorePropTag SenderAddressType = new StorePropTag(3102, PropertyType.Unicode, new StorePropInfo("SenderAddressType", 3102, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010FE RID: 4350
			public static readonly StorePropTag SenderEmailAddress = new StorePropTag(3103, PropertyType.Unicode, new StorePropInfo("SenderEmailAddress", 3103, PropertyType.Unicode, StorePropInfo.Flags.None, 128UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040010FF RID: 4351
			public static readonly StorePropTag ParticipantSID = new StorePropTag(3108, PropertyType.Binary, new StorePropInfo("ParticipantSID", 3108, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001100 RID: 4352
			public static readonly StorePropTag ParticipantGuid = new StorePropTag(3109, PropertyType.Binary, new StorePropInfo("ParticipantGuid", 3109, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001101 RID: 4353
			public static readonly StorePropTag ToGroupExpansionRecipients = new StorePropTag(3110, PropertyType.Unicode, new StorePropInfo("ToGroupExpansionRecipients", 3110, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001102 RID: 4354
			public static readonly StorePropTag CcGroupExpansionRecipients = new StorePropTag(3111, PropertyType.Unicode, new StorePropInfo("CcGroupExpansionRecipients", 3111, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001103 RID: 4355
			public static readonly StorePropTag BccGroupExpansionRecipients = new StorePropTag(3112, PropertyType.Unicode, new StorePropInfo("BccGroupExpansionRecipients", 3112, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001104 RID: 4356
			public static readonly StorePropTag CurrentVersion = new StorePropTag(3584, PropertyType.Int64, new StorePropInfo("CurrentVersion", 3584, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001105 RID: 4357
			public static readonly StorePropTag DeleteAfterSubmit = new StorePropTag(3585, PropertyType.Boolean, new StorePropInfo("DeleteAfterSubmit", 3585, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001106 RID: 4358
			public static readonly StorePropTag DisplayBcc = new StorePropTag(3586, PropertyType.Unicode, new StorePropInfo("DisplayBcc", 3586, PropertyType.Unicode, StorePropInfo.Flags.None, 16UL, new PropertyCategories(3, 9, 10, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001107 RID: 4359
			public static readonly StorePropTag DisplayCc = new StorePropTag(3587, PropertyType.Unicode, new StorePropInfo("DisplayCc", 3587, PropertyType.Unicode, StorePropInfo.Flags.None, 16UL, new PropertyCategories(3, 9, 10, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001108 RID: 4360
			public static readonly StorePropTag DisplayTo = new StorePropTag(3588, PropertyType.Unicode, new StorePropInfo("DisplayTo", 3588, PropertyType.Unicode, StorePropInfo.Flags.None, 36028797018963984UL, new PropertyCategories(3, 9, 10, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001109 RID: 4361
			public static readonly StorePropTag ParentDisplay = new StorePropTag(3589, PropertyType.Unicode, new StorePropInfo("ParentDisplay", 3589, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400110A RID: 4362
			public static readonly StorePropTag MessageDeliveryTime = new StorePropTag(3590, PropertyType.SysTime, new StorePropInfo("MessageDeliveryTime", 3590, PropertyType.SysTime, StorePropInfo.Flags.None, 2341871806232657921UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400110B RID: 4363
			public static readonly StorePropTag MessageFlags = new StorePropTag(3591, PropertyType.Int32, new StorePropInfo("MessageFlags", 3591, PropertyType.Int32, StorePropInfo.Flags.None, 2UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400110C RID: 4364
			public static readonly StorePropTag MessageSize = new StorePropTag(3592, PropertyType.Int64, new StorePropInfo("MessageSize", 3592, PropertyType.Int64, StorePropInfo.Flags.None, 1UL, new PropertyCategories(3, 9, 10, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400110D RID: 4365
			public static readonly StorePropTag MessageSize32 = new StorePropTag(3592, PropertyType.Int32, new StorePropInfo("MessageSize32", 3592, PropertyType.Int32, StorePropInfo.Flags.None, 1UL, new PropertyCategories(3, 9, 10, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400110E RID: 4366
			public static readonly StorePropTag ParentEntryId = new StorePropTag(3593, PropertyType.Binary, new StorePropInfo("ParentEntryId", 3593, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				10,
				14,
				18
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400110F RID: 4367
			public static readonly StorePropTag ParentEntryIdSvrEid = new StorePropTag(3593, PropertyType.SvrEid, new StorePropInfo("ParentEntryIdSvrEid", 3593, PropertyType.SvrEid, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				10,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001110 RID: 4368
			public static readonly StorePropTag SentMailEntryId = new StorePropTag(3594, PropertyType.Binary, new StorePropInfo("SentMailEntryId", 3594, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001111 RID: 4369
			public static readonly StorePropTag Correlate = new StorePropTag(3596, PropertyType.Boolean, new StorePropInfo("Correlate", 3596, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001112 RID: 4370
			public static readonly StorePropTag CorrelateMTSID = new StorePropTag(3597, PropertyType.Binary, new StorePropInfo("CorrelateMTSID", 3597, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001113 RID: 4371
			public static readonly StorePropTag DiscreteValues = new StorePropTag(3598, PropertyType.Boolean, new StorePropInfo("DiscreteValues", 3598, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001114 RID: 4372
			public static readonly StorePropTag Responsibility = new StorePropTag(3599, PropertyType.Boolean, new StorePropInfo("Responsibility", 3599, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001115 RID: 4373
			public static readonly StorePropTag SpoolerStatus = new StorePropTag(3600, PropertyType.Int32, new StorePropInfo("SpoolerStatus", 3600, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001116 RID: 4374
			public static readonly StorePropTag TransportStatus = new StorePropTag(3601, PropertyType.Int32, new StorePropInfo("TransportStatus", 3601, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001117 RID: 4375
			public static readonly StorePropTag MessageRecipients = new StorePropTag(3602, PropertyType.Object, new StorePropInfo("MessageRecipients", 3602, PropertyType.Object, StorePropInfo.Flags.None, 2305843009213693968UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001118 RID: 4376
			public static readonly StorePropTag MessageRecipientsMVBin = new StorePropTag(3602, PropertyType.MVBinary, new StorePropInfo("MessageRecipientsMVBin", 3602, PropertyType.MVBinary, StorePropInfo.Flags.None, 2305843009213693968UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001119 RID: 4377
			public static readonly StorePropTag MessageAttachments = new StorePropTag(3603, PropertyType.Object, new StorePropInfo("MessageAttachments", 3603, PropertyType.Object, StorePropInfo.Flags.None, 3458764513820540960UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400111A RID: 4378
			public static readonly StorePropTag ItemSubobjectsBin = new StorePropTag(3603, PropertyType.Binary, new StorePropInfo("ItemSubobjectsBin", 3603, PropertyType.Binary, StorePropInfo.Flags.None, 3458764513820540960UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400111B RID: 4379
			public static readonly StorePropTag SubmitFlags = new StorePropTag(3604, PropertyType.Int32, new StorePropInfo("SubmitFlags", 3604, PropertyType.Int32, StorePropInfo.Flags.None, 2UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400111C RID: 4380
			public static readonly StorePropTag RecipientStatus = new StorePropTag(3605, PropertyType.Int32, new StorePropInfo("RecipientStatus", 3605, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400111D RID: 4381
			public static readonly StorePropTag TransportKey = new StorePropTag(3606, PropertyType.Int32, new StorePropInfo("TransportKey", 3606, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400111E RID: 4382
			public static readonly StorePropTag MsgStatus = new StorePropTag(3607, PropertyType.Int32, new StorePropInfo("MsgStatus", 3607, PropertyType.Int32, StorePropInfo.Flags.None, 1UL, new PropertyCategories(1, 2, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400111F RID: 4383
			public static readonly StorePropTag CreationVersion = new StorePropTag(3609, PropertyType.Int64, new StorePropInfo("CreationVersion", 3609, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001120 RID: 4384
			public static readonly StorePropTag ModifyVersion = new StorePropTag(3610, PropertyType.Int64, new StorePropInfo("ModifyVersion", 3610, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001121 RID: 4385
			public static readonly StorePropTag HasAttach = new StorePropTag(3611, PropertyType.Boolean, new StorePropInfo("HasAttach", 3611, PropertyType.Boolean, StorePropInfo.Flags.None, 2UL, new PropertyCategories(3, 9, 10, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001122 RID: 4386
			public static readonly StorePropTag BodyCRC = new StorePropTag(3612, PropertyType.Int32, new StorePropInfo("BodyCRC", 3612, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001123 RID: 4387
			public static readonly StorePropTag NormalizedSubject = new StorePropTag(3613, PropertyType.Unicode, new StorePropInfo("NormalizedSubject", 3613, PropertyType.Unicode, StorePropInfo.Flags.None, 2341871806232658048UL, new PropertyCategories(1)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001124 RID: 4388
			public static readonly StorePropTag RTFInSync = new StorePropTag(3615, PropertyType.Boolean, new StorePropInfo("RTFInSync", 3615, PropertyType.Boolean, StorePropInfo.Flags.None, 3458764513820540936UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001125 RID: 4389
			public static readonly StorePropTag Preprocess = new StorePropTag(3618, PropertyType.Boolean, new StorePropInfo("Preprocess", 3618, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001126 RID: 4390
			public static readonly StorePropTag InternetArticleNumber = new StorePropTag(3619, PropertyType.Int32, new StorePropInfo("InternetArticleNumber", 3619, PropertyType.Int32, StorePropInfo.Flags.None, 1UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001127 RID: 4391
			public static readonly StorePropTag OriginatingMTACertificate = new StorePropTag(3621, PropertyType.Binary, new StorePropInfo("OriginatingMTACertificate", 3621, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001128 RID: 4392
			public static readonly StorePropTag ProofOfSubmission = new StorePropTag(3622, PropertyType.Binary, new StorePropInfo("ProofOfSubmission", 3622, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001129 RID: 4393
			public static readonly StorePropTag NTSecurityDescriptor = new StorePropTag(3623, PropertyType.Binary, new StorePropInfo("NTSecurityDescriptor", 3623, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400112A RID: 4394
			public static readonly StorePropTag PrimarySendAccount = new StorePropTag(3624, PropertyType.Unicode, new StorePropInfo("PrimarySendAccount", 3624, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400112B RID: 4395
			public static readonly StorePropTag NextSendAccount = new StorePropTag(3625, PropertyType.Unicode, new StorePropInfo("NextSendAccount", 3625, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400112C RID: 4396
			public static readonly StorePropTag TodoItemFlags = new StorePropTag(3627, PropertyType.Int32, new StorePropInfo("TodoItemFlags", 3627, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400112D RID: 4397
			public static readonly StorePropTag SwappedTODOStore = new StorePropTag(3628, PropertyType.Binary, new StorePropInfo("SwappedTODOStore", 3628, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400112E RID: 4398
			public static readonly StorePropTag SwappedTODOData = new StorePropTag(3629, PropertyType.Binary, new StorePropInfo("SwappedTODOData", 3629, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400112F RID: 4399
			public static readonly StorePropTag IMAPId = new StorePropTag(3631, PropertyType.Int32, new StorePropInfo("IMAPId", 3631, PropertyType.Int32, StorePropInfo.Flags.None, 1UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001130 RID: 4400
			public static readonly StorePropTag OriginalSourceServerVersion = new StorePropTag(3633, PropertyType.Int16, new StorePropInfo("OriginalSourceServerVersion", 3633, PropertyType.Int16, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001131 RID: 4401
			public static readonly StorePropTag ReplFlags = new StorePropTag(3640, PropertyType.Int32, new StorePropInfo("ReplFlags", 3640, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001132 RID: 4402
			public static readonly StorePropTag MessageDeepAttachments = new StorePropTag(3642, PropertyType.Object, new StorePropInfo("MessageDeepAttachments", 3642, PropertyType.Object, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001133 RID: 4403
			public static readonly StorePropTag SenderGuid = new StorePropTag(3648, PropertyType.Binary, new StorePropInfo("SenderGuid", 3648, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001134 RID: 4404
			public static readonly StorePropTag SentRepresentingGuid = new StorePropTag(3649, PropertyType.Binary, new StorePropInfo("SentRepresentingGuid", 3649, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001135 RID: 4405
			public static readonly StorePropTag OriginalSenderGuid = new StorePropTag(3650, PropertyType.Binary, new StorePropInfo("OriginalSenderGuid", 3650, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001136 RID: 4406
			public static readonly StorePropTag OriginalSentRepresentingGuid = new StorePropTag(3651, PropertyType.Binary, new StorePropInfo("OriginalSentRepresentingGuid", 3651, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001137 RID: 4407
			public static readonly StorePropTag ReadReceiptGuid = new StorePropTag(3652, PropertyType.Binary, new StorePropInfo("ReadReceiptGuid", 3652, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001138 RID: 4408
			public static readonly StorePropTag ReportGuid = new StorePropTag(3653, PropertyType.Binary, new StorePropInfo("ReportGuid", 3653, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001139 RID: 4409
			public static readonly StorePropTag OriginatorGuid = new StorePropTag(3654, PropertyType.Binary, new StorePropInfo("OriginatorGuid", 3654, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400113A RID: 4410
			public static readonly StorePropTag ReportDestinationGuid = new StorePropTag(3655, PropertyType.Binary, new StorePropInfo("ReportDestinationGuid", 3655, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400113B RID: 4411
			public static readonly StorePropTag OriginalAuthorGuid = new StorePropTag(3656, PropertyType.Binary, new StorePropInfo("OriginalAuthorGuid", 3656, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400113C RID: 4412
			public static readonly StorePropTag ReceivedByGuid = new StorePropTag(3657, PropertyType.Binary, new StorePropInfo("ReceivedByGuid", 3657, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400113D RID: 4413
			public static readonly StorePropTag ReceivedRepresentingGuid = new StorePropTag(3658, PropertyType.Binary, new StorePropInfo("ReceivedRepresentingGuid", 3658, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400113E RID: 4414
			public static readonly StorePropTag CreatorGuid = new StorePropTag(3659, PropertyType.Binary, new StorePropInfo("CreatorGuid", 3659, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400113F RID: 4415
			public static readonly StorePropTag LastModifierGuid = new StorePropTag(3660, PropertyType.Binary, new StorePropInfo("LastModifierGuid", 3660, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 7, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001140 RID: 4416
			public static readonly StorePropTag SenderSID = new StorePropTag(3661, PropertyType.Binary, new StorePropInfo("SenderSID", 3661, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001141 RID: 4417
			public static readonly StorePropTag SentRepresentingSID = new StorePropTag(3662, PropertyType.Binary, new StorePropInfo("SentRepresentingSID", 3662, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001142 RID: 4418
			public static readonly StorePropTag OriginalSenderSid = new StorePropTag(3663, PropertyType.Binary, new StorePropInfo("OriginalSenderSid", 3663, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001143 RID: 4419
			public static readonly StorePropTag OriginalSentRepresentingSid = new StorePropTag(3664, PropertyType.Binary, new StorePropInfo("OriginalSentRepresentingSid", 3664, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001144 RID: 4420
			public static readonly StorePropTag ReadReceiptSid = new StorePropTag(3665, PropertyType.Binary, new StorePropInfo("ReadReceiptSid", 3665, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001145 RID: 4421
			public static readonly StorePropTag ReportSid = new StorePropTag(3666, PropertyType.Binary, new StorePropInfo("ReportSid", 3666, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001146 RID: 4422
			public static readonly StorePropTag OriginatorSid = new StorePropTag(3667, PropertyType.Binary, new StorePropInfo("OriginatorSid", 3667, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001147 RID: 4423
			public static readonly StorePropTag ReportDestinationSid = new StorePropTag(3668, PropertyType.Binary, new StorePropInfo("ReportDestinationSid", 3668, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001148 RID: 4424
			public static readonly StorePropTag OriginalAuthorSid = new StorePropTag(3669, PropertyType.Binary, new StorePropInfo("OriginalAuthorSid", 3669, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001149 RID: 4425
			public static readonly StorePropTag RcvdBySid = new StorePropTag(3670, PropertyType.Binary, new StorePropInfo("RcvdBySid", 3670, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400114A RID: 4426
			public static readonly StorePropTag RcvdRepresentingSid = new StorePropTag(3671, PropertyType.Binary, new StorePropInfo("RcvdRepresentingSid", 3671, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400114B RID: 4427
			public static readonly StorePropTag CreatorSID = new StorePropTag(3672, PropertyType.Binary, new StorePropInfo("CreatorSID", 3672, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400114C RID: 4428
			public static readonly StorePropTag LastModifierSid = new StorePropTag(3673, PropertyType.Binary, new StorePropInfo("LastModifierSid", 3673, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 7, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400114D RID: 4429
			public static readonly StorePropTag RecipientCAI = new StorePropTag(3674, PropertyType.Binary, new StorePropInfo("RecipientCAI", 3674, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400114E RID: 4430
			public static readonly StorePropTag ConversationCreatorSID = new StorePropTag(3675, PropertyType.Binary, new StorePropInfo("ConversationCreatorSID", 3675, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400114F RID: 4431
			public static readonly StorePropTag URLCompNamePostfix = new StorePropTag(3681, PropertyType.Int32, new StorePropInfo("URLCompNamePostfix", 3681, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001150 RID: 4432
			public static readonly StorePropTag URLCompNameSet = new StorePropTag(3682, PropertyType.Boolean, new StorePropInfo("URLCompNameSet", 3682, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001151 RID: 4433
			public static readonly StorePropTag Read = new StorePropTag(3689, PropertyType.Boolean, new StorePropInfo("Read", 3689, PropertyType.Boolean, StorePropInfo.Flags.None, 2UL, new PropertyCategories(1, 2)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001152 RID: 4434
			public static readonly StorePropTag CreatorSidAsXML = new StorePropTag(3692, PropertyType.Unicode, new StorePropInfo("CreatorSidAsXML", 3692, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001153 RID: 4435
			public static readonly StorePropTag LastModifierSidAsXML = new StorePropTag(3693, PropertyType.Unicode, new StorePropInfo("LastModifierSidAsXML", 3693, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001154 RID: 4436
			public static readonly StorePropTag SenderSIDAsXML = new StorePropTag(3694, PropertyType.Unicode, new StorePropInfo("SenderSIDAsXML", 3694, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001155 RID: 4437
			public static readonly StorePropTag SentRepresentingSidAsXML = new StorePropTag(3695, PropertyType.Unicode, new StorePropInfo("SentRepresentingSidAsXML", 3695, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001156 RID: 4438
			public static readonly StorePropTag OriginalSenderSIDAsXML = new StorePropTag(3696, PropertyType.Unicode, new StorePropInfo("OriginalSenderSIDAsXML", 3696, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001157 RID: 4439
			public static readonly StorePropTag OriginalSentRepresentingSIDAsXML = new StorePropTag(3697, PropertyType.Unicode, new StorePropInfo("OriginalSentRepresentingSIDAsXML", 3697, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001158 RID: 4440
			public static readonly StorePropTag ReadReceiptSIDAsXML = new StorePropTag(3698, PropertyType.Unicode, new StorePropInfo("ReadReceiptSIDAsXML", 3698, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001159 RID: 4441
			public static readonly StorePropTag ReportSIDAsXML = new StorePropTag(3699, PropertyType.Unicode, new StorePropInfo("ReportSIDAsXML", 3699, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400115A RID: 4442
			public static readonly StorePropTag OriginatorSidAsXML = new StorePropTag(3700, PropertyType.Unicode, new StorePropInfo("OriginatorSidAsXML", 3700, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400115B RID: 4443
			public static readonly StorePropTag ReportDestinationSIDAsXML = new StorePropTag(3701, PropertyType.Unicode, new StorePropInfo("ReportDestinationSIDAsXML", 3701, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400115C RID: 4444
			public static readonly StorePropTag OriginalAuthorSIDAsXML = new StorePropTag(3702, PropertyType.Unicode, new StorePropInfo("OriginalAuthorSIDAsXML", 3702, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400115D RID: 4445
			public static readonly StorePropTag ReceivedBySIDAsXML = new StorePropTag(3703, PropertyType.Unicode, new StorePropInfo("ReceivedBySIDAsXML", 3703, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400115E RID: 4446
			public static readonly StorePropTag ReceivedRepersentingSIDAsXML = new StorePropTag(3704, PropertyType.Unicode, new StorePropInfo("ReceivedRepersentingSIDAsXML", 3704, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400115F RID: 4447
			public static readonly StorePropTag TrustSender = new StorePropTag(3705, PropertyType.Int32, new StorePropInfo("TrustSender", 3705, PropertyType.Int32, StorePropInfo.Flags.None, 1UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001160 RID: 4448
			public static readonly StorePropTag SenderSMTPAddress = new StorePropTag(3721, PropertyType.Unicode, new StorePropInfo("SenderSMTPAddress", 3721, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001161 RID: 4449
			public static readonly StorePropTag SentRepresentingSMTPAddress = new StorePropTag(3722, PropertyType.Unicode, new StorePropInfo("SentRepresentingSMTPAddress", 3722, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001162 RID: 4450
			public static readonly StorePropTag OriginalSenderSMTPAddress = new StorePropTag(3723, PropertyType.Unicode, new StorePropInfo("OriginalSenderSMTPAddress", 3723, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001163 RID: 4451
			public static readonly StorePropTag OriginalSentRepresentingSMTPAddress = new StorePropTag(3724, PropertyType.Unicode, new StorePropInfo("OriginalSentRepresentingSMTPAddress", 3724, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001164 RID: 4452
			public static readonly StorePropTag ReadReceiptSMTPAddress = new StorePropTag(3725, PropertyType.Unicode, new StorePropInfo("ReadReceiptSMTPAddress", 3725, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001165 RID: 4453
			public static readonly StorePropTag ReportSMTPAddress = new StorePropTag(3726, PropertyType.Unicode, new StorePropInfo("ReportSMTPAddress", 3726, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001166 RID: 4454
			public static readonly StorePropTag OriginatorSMTPAddress = new StorePropTag(3727, PropertyType.Unicode, new StorePropInfo("OriginatorSMTPAddress", 3727, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001167 RID: 4455
			public static readonly StorePropTag ReportDestinationSMTPAddress = new StorePropTag(3728, PropertyType.Unicode, new StorePropInfo("ReportDestinationSMTPAddress", 3728, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001168 RID: 4456
			public static readonly StorePropTag OriginalAuthorSMTPAddress = new StorePropTag(3729, PropertyType.Unicode, new StorePropInfo("OriginalAuthorSMTPAddress", 3729, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001169 RID: 4457
			public static readonly StorePropTag ReceivedBySMTPAddress = new StorePropTag(3730, PropertyType.Unicode, new StorePropInfo("ReceivedBySMTPAddress", 3730, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400116A RID: 4458
			public static readonly StorePropTag ReceivedRepresentingSMTPAddress = new StorePropTag(3731, PropertyType.Unicode, new StorePropInfo("ReceivedRepresentingSMTPAddress", 3731, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400116B RID: 4459
			public static readonly StorePropTag CreatorSMTPAddress = new StorePropTag(3732, PropertyType.Unicode, new StorePropInfo("CreatorSMTPAddress", 3732, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400116C RID: 4460
			public static readonly StorePropTag LastModifierSMTPAddress = new StorePropTag(3733, PropertyType.Unicode, new StorePropInfo("LastModifierSMTPAddress", 3733, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400116D RID: 4461
			public static readonly StorePropTag VirusScannerStamp = new StorePropTag(3734, PropertyType.Binary, new StorePropInfo("VirusScannerStamp", 3734, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, new PropertyCategories(3, 6, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400116E RID: 4462
			public static readonly StorePropTag VirusTransportStamp = new StorePropTag(3734, PropertyType.MVUnicode, new StorePropInfo("VirusTransportStamp", 3734, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400116F RID: 4463
			public static readonly StorePropTag AddrTo = new StorePropTag(3735, PropertyType.Unicode, new StorePropInfo("AddrTo", 3735, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001170 RID: 4464
			public static readonly StorePropTag AddrCc = new StorePropTag(3736, PropertyType.Unicode, new StorePropInfo("AddrCc", 3736, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001171 RID: 4465
			public static readonly StorePropTag ExtendedRuleActions = new StorePropTag(3737, PropertyType.Binary, new StorePropInfo("ExtendedRuleActions", 3737, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001172 RID: 4466
			public static readonly StorePropTag ExtendedRuleCondition = new StorePropTag(3738, PropertyType.Binary, new StorePropInfo("ExtendedRuleCondition", 3738, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001173 RID: 4467
			public static readonly StorePropTag EntourageSentHistory = new StorePropTag(3743, PropertyType.MVUnicode, new StorePropInfo("EntourageSentHistory", 3743, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001174 RID: 4468
			public static readonly StorePropTag ProofInProgress = new StorePropTag(3746, PropertyType.Int32, new StorePropInfo("ProofInProgress", 3746, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001175 RID: 4469
			public static readonly StorePropTag SearchAttachmentsOLK = new StorePropTag(3749, PropertyType.Unicode, new StorePropInfo("SearchAttachmentsOLK", 3749, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001176 RID: 4470
			public static readonly StorePropTag SearchRecipEmailTo = new StorePropTag(3750, PropertyType.Unicode, new StorePropInfo("SearchRecipEmailTo", 3750, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001177 RID: 4471
			public static readonly StorePropTag SearchRecipEmailCc = new StorePropTag(3751, PropertyType.Unicode, new StorePropInfo("SearchRecipEmailCc", 3751, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001178 RID: 4472
			public static readonly StorePropTag SearchRecipEmailBcc = new StorePropTag(3752, PropertyType.Unicode, new StorePropInfo("SearchRecipEmailBcc", 3752, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001179 RID: 4473
			public static readonly StorePropTag SFGAOFlags = new StorePropTag(3754, PropertyType.Int32, new StorePropInfo("SFGAOFlags", 3754, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400117A RID: 4474
			public static readonly StorePropTag SearchFullTextSubject = new StorePropTag(3756, PropertyType.Unicode, new StorePropInfo("SearchFullTextSubject", 3756, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400117B RID: 4475
			public static readonly StorePropTag SearchFullTextBody = new StorePropTag(3757, PropertyType.Unicode, new StorePropInfo("SearchFullTextBody", 3757, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400117C RID: 4476
			public static readonly StorePropTag FullTextConversationIndex = new StorePropTag(3758, PropertyType.Unicode, new StorePropInfo("FullTextConversationIndex", 3758, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400117D RID: 4477
			public static readonly StorePropTag SearchAllIndexedProps = new StorePropTag(3759, PropertyType.Unicode, new StorePropInfo("SearchAllIndexedProps", 3759, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400117E RID: 4478
			public static readonly StorePropTag SearchRecipients = new StorePropTag(3761, PropertyType.Unicode, new StorePropInfo("SearchRecipients", 3761, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400117F RID: 4479
			public static readonly StorePropTag SearchRecipientsTo = new StorePropTag(3762, PropertyType.Unicode, new StorePropInfo("SearchRecipientsTo", 3762, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001180 RID: 4480
			public static readonly StorePropTag SearchRecipientsCc = new StorePropTag(3763, PropertyType.Unicode, new StorePropInfo("SearchRecipientsCc", 3763, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001181 RID: 4481
			public static readonly StorePropTag SearchRecipientsBcc = new StorePropTag(3764, PropertyType.Unicode, new StorePropInfo("SearchRecipientsBcc", 3764, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001182 RID: 4482
			public static readonly StorePropTag SearchAccountTo = new StorePropTag(3765, PropertyType.Unicode, new StorePropInfo("SearchAccountTo", 3765, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001183 RID: 4483
			public static readonly StorePropTag SearchAccountCc = new StorePropTag(3766, PropertyType.Unicode, new StorePropInfo("SearchAccountCc", 3766, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001184 RID: 4484
			public static readonly StorePropTag SearchAccountBcc = new StorePropTag(3767, PropertyType.Unicode, new StorePropInfo("SearchAccountBcc", 3767, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001185 RID: 4485
			public static readonly StorePropTag SearchEmailAddressTo = new StorePropTag(3768, PropertyType.Unicode, new StorePropInfo("SearchEmailAddressTo", 3768, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001186 RID: 4486
			public static readonly StorePropTag SearchEmailAddressCc = new StorePropTag(3769, PropertyType.Unicode, new StorePropInfo("SearchEmailAddressCc", 3769, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001187 RID: 4487
			public static readonly StorePropTag SearchEmailAddressBcc = new StorePropTag(3770, PropertyType.Unicode, new StorePropInfo("SearchEmailAddressBcc", 3770, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001188 RID: 4488
			public static readonly StorePropTag SearchSmtpAddressTo = new StorePropTag(3771, PropertyType.Unicode, new StorePropInfo("SearchSmtpAddressTo", 3771, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001189 RID: 4489
			public static readonly StorePropTag SearchSmtpAddressCc = new StorePropTag(3772, PropertyType.Unicode, new StorePropInfo("SearchSmtpAddressCc", 3772, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400118A RID: 4490
			public static readonly StorePropTag SearchSmtpAddressBcc = new StorePropTag(3773, PropertyType.Unicode, new StorePropInfo("SearchSmtpAddressBcc", 3773, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400118B RID: 4491
			public static readonly StorePropTag SearchSender = new StorePropTag(3774, PropertyType.Unicode, new StorePropInfo("SearchSender", 3774, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400118C RID: 4492
			public static readonly StorePropTag IsIRMMessage = new StorePropTag(3789, PropertyType.Boolean, new StorePropInfo("IsIRMMessage", 3789, PropertyType.Boolean, StorePropInfo.Flags.None, 2UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400118D RID: 4493
			public static readonly StorePropTag SearchIsPartiallyIndexed = new StorePropTag(3790, PropertyType.Boolean, new StorePropInfo("SearchIsPartiallyIndexed", 3790, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400118E RID: 4494
			public static readonly StorePropTag RenewTime = new StorePropTag(3841, PropertyType.SysTime, new StorePropInfo("RenewTime", 3841, PropertyType.SysTime, StorePropInfo.Flags.None, 2341871806232657921UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400118F RID: 4495
			public static readonly StorePropTag DeliveryOrRenewTime = new StorePropTag(3842, PropertyType.SysTime, new StorePropInfo("DeliveryOrRenewTime", 3842, PropertyType.SysTime, StorePropInfo.Flags.None, 2341871806232657921UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001190 RID: 4496
			public static readonly StorePropTag ConversationFamilyId = new StorePropTag(3843, PropertyType.Binary, new StorePropInfo("ConversationFamilyId", 3843, PropertyType.Binary, StorePropInfo.Flags.None, 2341871806232658048UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001191 RID: 4497
			public static readonly StorePropTag LikeCount = new StorePropTag(3844, PropertyType.Int32, new StorePropInfo("LikeCount", 3844, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001192 RID: 4498
			public static readonly StorePropTag RichContentDeprecated = new StorePropTag(3845, PropertyType.Int16, new StorePropInfo("RichContentDeprecated", 3845, PropertyType.Int16, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001193 RID: 4499
			public static readonly StorePropTag PeopleCentricConversationId = new StorePropTag(3846, PropertyType.Int32, new StorePropInfo("PeopleCentricConversationId", 3846, PropertyType.Int32, StorePropInfo.Flags.None, 9259400833873739776UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001194 RID: 4500
			public static readonly StorePropTag DiscoveryAnnotation = new StorePropTag(3854, PropertyType.Unicode, new StorePropInfo("DiscoveryAnnotation", 3854, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001195 RID: 4501
			public static readonly StorePropTag Access = new StorePropTag(4084, PropertyType.Int32, new StorePropInfo("Access", 4084, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 10, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001196 RID: 4502
			public static readonly StorePropTag RowType = new StorePropTag(4085, PropertyType.Int32, new StorePropInfo("RowType", 4085, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001197 RID: 4503
			public static readonly StorePropTag InstanceKey = new StorePropTag(4086, PropertyType.Binary, new StorePropInfo("InstanceKey", 4086, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001198 RID: 4504
			public static readonly StorePropTag InstanceKeySvrEid = new StorePropTag(4086, PropertyType.SvrEid, new StorePropInfo("InstanceKeySvrEid", 4086, PropertyType.SvrEid, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001199 RID: 4505
			public static readonly StorePropTag AccessLevel = new StorePropTag(4087, PropertyType.Int32, new StorePropInfo("AccessLevel", 4087, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400119A RID: 4506
			public static readonly StorePropTag MappingSignature = new StorePropTag(4088, PropertyType.Binary, new StorePropInfo("MappingSignature", 4088, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400119B RID: 4507
			public static readonly StorePropTag RecordKey = new StorePropTag(4089, PropertyType.Binary, new StorePropInfo("RecordKey", 4089, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				10,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400119C RID: 4508
			public static readonly StorePropTag RecordKeySvrEid = new StorePropTag(4089, PropertyType.SvrEid, new StorePropInfo("RecordKeySvrEid", 4089, PropertyType.SvrEid, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				10,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400119D RID: 4509
			public static readonly StorePropTag StoreRecordKey = new StorePropTag(4090, PropertyType.Binary, new StorePropInfo("StoreRecordKey", 4090, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400119E RID: 4510
			public static readonly StorePropTag StoreEntryId = new StorePropTag(4091, PropertyType.Binary, new StorePropInfo("StoreEntryId", 4091, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400119F RID: 4511
			public static readonly StorePropTag MiniIcon = new StorePropTag(4092, PropertyType.Binary, new StorePropInfo("MiniIcon", 4092, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011A0 RID: 4512
			public static readonly StorePropTag Icon = new StorePropTag(4093, PropertyType.Binary, new StorePropInfo("Icon", 4093, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011A1 RID: 4513
			public static readonly StorePropTag ObjectType = new StorePropTag(4094, PropertyType.Int32, new StorePropInfo("ObjectType", 4094, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011A2 RID: 4514
			public static readonly StorePropTag EntryId = new StorePropTag(4095, PropertyType.Binary, new StorePropInfo("EntryId", 4095, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				10,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011A3 RID: 4515
			public static readonly StorePropTag EntryIdSvrEid = new StorePropTag(4095, PropertyType.SvrEid, new StorePropInfo("EntryIdSvrEid", 4095, PropertyType.SvrEid, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				10,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011A4 RID: 4516
			public static readonly StorePropTag BodyUnicode = new StorePropTag(4096, PropertyType.Unicode, new StorePropInfo("BodyUnicode", 4096, PropertyType.Unicode, StorePropInfo.Flags.Private, 3458764513820540936UL, new PropertyCategories(12, 15)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011A5 RID: 4517
			public static readonly StorePropTag ReportText = new StorePropTag(4097, PropertyType.Unicode, new StorePropInfo("ReportText", 4097, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011A6 RID: 4518
			public static readonly StorePropTag OriginatorAndDLExpansionHistory = new StorePropTag(4098, PropertyType.Binary, new StorePropInfo("OriginatorAndDLExpansionHistory", 4098, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011A7 RID: 4519
			public static readonly StorePropTag ReportingDLName = new StorePropTag(4099, PropertyType.Binary, new StorePropInfo("ReportingDLName", 4099, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011A8 RID: 4520
			public static readonly StorePropTag ReportingMTACertificate = new StorePropTag(4100, PropertyType.Binary, new StorePropInfo("ReportingMTACertificate", 4100, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011A9 RID: 4521
			public static readonly StorePropTag RtfSyncBodyCrc = new StorePropTag(4102, PropertyType.Int32, new StorePropInfo("RtfSyncBodyCrc", 4102, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011AA RID: 4522
			public static readonly StorePropTag RtfSyncBodyCount = new StorePropTag(4103, PropertyType.Int32, new StorePropInfo("RtfSyncBodyCount", 4103, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011AB RID: 4523
			public static readonly StorePropTag RtfSyncBodyTag = new StorePropTag(4104, PropertyType.Unicode, new StorePropInfo("RtfSyncBodyTag", 4104, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011AC RID: 4524
			public static readonly StorePropTag RtfCompressed = new StorePropTag(4105, PropertyType.Binary, new StorePropInfo("RtfCompressed", 4105, PropertyType.Binary, StorePropInfo.Flags.Private, 3458764513820540936UL, new PropertyCategories(12, 15)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011AD RID: 4525
			public static readonly StorePropTag AlternateBestBody = new StorePropTag(4106, PropertyType.Binary, new StorePropInfo("AlternateBestBody", 4106, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011AE RID: 4526
			public static readonly StorePropTag RtfSyncPrefixCount = new StorePropTag(4112, PropertyType.Int32, new StorePropInfo("RtfSyncPrefixCount", 4112, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011AF RID: 4527
			public static readonly StorePropTag RtfSyncTrailingCount = new StorePropTag(4113, PropertyType.Int32, new StorePropInfo("RtfSyncTrailingCount", 4113, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011B0 RID: 4528
			public static readonly StorePropTag OriginallyIntendedRecipientEntryId = new StorePropTag(4114, PropertyType.Binary, new StorePropInfo("OriginallyIntendedRecipientEntryId", 4114, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011B1 RID: 4529
			public static readonly StorePropTag BodyHtml = new StorePropTag(4115, PropertyType.Binary, new StorePropInfo("BodyHtml", 4115, PropertyType.Binary, StorePropInfo.Flags.Private, 3458764513820540936UL, new PropertyCategories(12, 15)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011B2 RID: 4530
			public static readonly StorePropTag BodyHtmlUnicode = new StorePropTag(4115, PropertyType.Unicode, new StorePropInfo("BodyHtmlUnicode", 4115, PropertyType.Unicode, StorePropInfo.Flags.Private, 3458764513820540936UL, new PropertyCategories(12, 15)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011B3 RID: 4531
			public static readonly StorePropTag BodyContentLocation = new StorePropTag(4116, PropertyType.Unicode, new StorePropInfo("BodyContentLocation", 4116, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011B4 RID: 4532
			public static readonly StorePropTag BodyContentId = new StorePropTag(4117, PropertyType.Unicode, new StorePropInfo("BodyContentId", 4117, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011B5 RID: 4533
			public static readonly StorePropTag NativeBodyInfo = new StorePropTag(4118, PropertyType.Int32, new StorePropInfo("NativeBodyInfo", 4118, PropertyType.Int32, StorePropInfo.Flags.None, 3458764513820540936UL, new PropertyCategories(3, 9, 10, 15)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011B6 RID: 4534
			public static readonly StorePropTag NativeBodyType = new StorePropTag(4118, PropertyType.Int16, new StorePropInfo("NativeBodyType", 4118, PropertyType.Int16, StorePropInfo.Flags.None, 3458764513820540936UL, new PropertyCategories(3, 9, 15)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011B7 RID: 4535
			public static readonly StorePropTag NativeBody = new StorePropTag(4118, PropertyType.Binary, new StorePropInfo("NativeBody", 4118, PropertyType.Binary, StorePropInfo.Flags.Private, 3458764513820540936UL, new PropertyCategories(3, 9, 15)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011B8 RID: 4536
			public static readonly StorePropTag AnnotationToken = new StorePropTag(4119, PropertyType.Binary, new StorePropInfo("AnnotationToken", 4119, PropertyType.Binary, StorePropInfo.Flags.Private, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011B9 RID: 4537
			public static readonly StorePropTag InternetApproved = new StorePropTag(4144, PropertyType.Unicode, new StorePropInfo("InternetApproved", 4144, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011BA RID: 4538
			public static readonly StorePropTag InternetFollowupTo = new StorePropTag(4147, PropertyType.Unicode, new StorePropInfo("InternetFollowupTo", 4147, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011BB RID: 4539
			public static readonly StorePropTag InternetMessageId = new StorePropTag(4149, PropertyType.Unicode, new StorePropInfo("InternetMessageId", 4149, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011BC RID: 4540
			public static readonly StorePropTag InetNewsgroups = new StorePropTag(4150, PropertyType.Unicode, new StorePropInfo("InetNewsgroups", 4150, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011BD RID: 4541
			public static readonly StorePropTag InternetReferences = new StorePropTag(4153, PropertyType.Unicode, new StorePropInfo("InternetReferences", 4153, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011BE RID: 4542
			public static readonly StorePropTag PostReplyFolderEntries = new StorePropTag(4157, PropertyType.Binary, new StorePropInfo("PostReplyFolderEntries", 4157, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011BF RID: 4543
			public static readonly StorePropTag NNTPXRef = new StorePropTag(4160, PropertyType.Unicode, new StorePropInfo("NNTPXRef", 4160, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011C0 RID: 4544
			public static readonly StorePropTag InReplyToId = new StorePropTag(4162, PropertyType.Unicode, new StorePropInfo("InReplyToId", 4162, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011C1 RID: 4545
			public static readonly StorePropTag OriginalInternetMessageId = new StorePropTag(4166, PropertyType.Unicode, new StorePropInfo("OriginalInternetMessageId", 4166, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011C2 RID: 4546
			public static readonly StorePropTag IconIndex = new StorePropTag(4224, PropertyType.Int32, new StorePropInfo("IconIndex", 4224, PropertyType.Int32, StorePropInfo.Flags.None, 2UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011C3 RID: 4547
			public static readonly StorePropTag LastVerbExecuted = new StorePropTag(4225, PropertyType.Int32, new StorePropInfo("LastVerbExecuted", 4225, PropertyType.Int32, StorePropInfo.Flags.None, 2UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011C4 RID: 4548
			public static readonly StorePropTag LastVerbExecutionTime = new StorePropTag(4226, PropertyType.SysTime, new StorePropInfo("LastVerbExecutionTime", 4226, PropertyType.SysTime, StorePropInfo.Flags.None, 2UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011C5 RID: 4549
			public static readonly StorePropTag Relevance = new StorePropTag(4228, PropertyType.Int32, new StorePropInfo("Relevance", 4228, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011C6 RID: 4550
			public static readonly StorePropTag FlagStatus = new StorePropTag(4240, PropertyType.Int32, new StorePropInfo("FlagStatus", 4240, PropertyType.Int32, StorePropInfo.Flags.None, 2305843009213693954UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011C7 RID: 4551
			public static readonly StorePropTag FlagCompleteTime = new StorePropTag(4241, PropertyType.SysTime, new StorePropInfo("FlagCompleteTime", 4241, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011C8 RID: 4552
			public static readonly StorePropTag FormatPT = new StorePropTag(4242, PropertyType.Int32, new StorePropInfo("FormatPT", 4242, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011C9 RID: 4553
			public static readonly StorePropTag FollowupIcon = new StorePropTag(4245, PropertyType.Int32, new StorePropInfo("FollowupIcon", 4245, PropertyType.Int32, StorePropInfo.Flags.None, 2UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011CA RID: 4554
			public static readonly StorePropTag BlockStatus = new StorePropTag(4246, PropertyType.Int32, new StorePropInfo("BlockStatus", 4246, PropertyType.Int32, StorePropInfo.Flags.None, 2UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011CB RID: 4555
			public static readonly StorePropTag ItemTempFlags = new StorePropTag(4247, PropertyType.Int32, new StorePropInfo("ItemTempFlags", 4247, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011CC RID: 4556
			public static readonly StorePropTag SMTPTempTblData = new StorePropTag(4288, PropertyType.Binary, new StorePropInfo("SMTPTempTblData", 4288, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011CD RID: 4557
			public static readonly StorePropTag SMTPTempTblData2 = new StorePropTag(4289, PropertyType.Int32, new StorePropInfo("SMTPTempTblData2", 4289, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011CE RID: 4558
			public static readonly StorePropTag SMTPTempTblData3 = new StorePropTag(4290, PropertyType.Binary, new StorePropInfo("SMTPTempTblData3", 4290, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011CF RID: 4559
			public static readonly StorePropTag DAVSubmitData = new StorePropTag(4294, PropertyType.Unicode, new StorePropInfo("DAVSubmitData", 4294, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011D0 RID: 4560
			public static readonly StorePropTag ImapCachedMsgSize = new StorePropTag(4336, PropertyType.Binary, new StorePropInfo("ImapCachedMsgSize", 4336, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, new PropertyCategories(1)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011D1 RID: 4561
			public static readonly StorePropTag DisableFullFidelity = new StorePropTag(4338, PropertyType.Boolean, new StorePropInfo("DisableFullFidelity", 4338, PropertyType.Boolean, StorePropInfo.Flags.None, 1UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011D2 RID: 4562
			public static readonly StorePropTag URLCompName = new StorePropTag(4339, PropertyType.Unicode, new StorePropInfo("URLCompName", 4339, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011D3 RID: 4563
			public static readonly StorePropTag AttrHidden = new StorePropTag(4340, PropertyType.Boolean, new StorePropInfo("AttrHidden", 4340, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011D4 RID: 4564
			public static readonly StorePropTag AttrSystem = new StorePropTag(4341, PropertyType.Boolean, new StorePropInfo("AttrSystem", 4341, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011D5 RID: 4565
			public static readonly StorePropTag AttrReadOnly = new StorePropTag(4342, PropertyType.Boolean, new StorePropInfo("AttrReadOnly", 4342, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011D6 RID: 4566
			public static readonly StorePropTag PredictedActions = new StorePropTag(4612, PropertyType.MVInt16, new StorePropInfo("PredictedActions", 4612, PropertyType.MVInt16, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011D7 RID: 4567
			public static readonly StorePropTag GroupingActions = new StorePropTag(4613, PropertyType.MVInt16, new StorePropInfo("GroupingActions", 4613, PropertyType.MVInt16, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011D8 RID: 4568
			public static readonly StorePropTag PredictedActionsSummary = new StorePropTag(4614, PropertyType.Int32, new StorePropInfo("PredictedActionsSummary", 4614, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011D9 RID: 4569
			public static readonly StorePropTag PredictedActionsThresholds = new StorePropTag(4615, PropertyType.Binary, new StorePropInfo("PredictedActionsThresholds", 4615, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011DA RID: 4570
			public static readonly StorePropTag IsClutter = new StorePropTag(4615, PropertyType.Boolean, new StorePropInfo("IsClutter", 4615, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011DB RID: 4571
			public static readonly StorePropTag InferencePredictedReplyForwardReasons = new StorePropTag(4616, PropertyType.Binary, new StorePropInfo("InferencePredictedReplyForwardReasons", 4616, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011DC RID: 4572
			public static readonly StorePropTag InferencePredictedDeleteReasons = new StorePropTag(4617, PropertyType.Binary, new StorePropInfo("InferencePredictedDeleteReasons", 4617, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011DD RID: 4573
			public static readonly StorePropTag InferencePredictedIgnoreReasons = new StorePropTag(4618, PropertyType.Binary, new StorePropInfo("InferencePredictedIgnoreReasons", 4618, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011DE RID: 4574
			public static readonly StorePropTag OriginalDeliveryFolderInfo = new StorePropTag(4619, PropertyType.Binary, new StorePropInfo("OriginalDeliveryFolderInfo", 4619, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011DF RID: 4575
			public static readonly StorePropTag RowId = new StorePropTag(12288, PropertyType.Int32, new StorePropInfo("RowId", 12288, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011E0 RID: 4576
			public static readonly StorePropTag DisplayName = new StorePropTag(12289, PropertyType.Unicode, new StorePropInfo("DisplayName", 12289, PropertyType.Unicode, StorePropInfo.Flags.None, 128UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011E1 RID: 4577
			public static readonly StorePropTag AddressType = new StorePropTag(12290, PropertyType.Unicode, new StorePropInfo("AddressType", 12290, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011E2 RID: 4578
			public static readonly StorePropTag EmailAddress = new StorePropTag(12291, PropertyType.Unicode, new StorePropInfo("EmailAddress", 12291, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011E3 RID: 4579
			public static readonly StorePropTag Comment = new StorePropTag(12292, PropertyType.Unicode, new StorePropInfo("Comment", 12292, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011E4 RID: 4580
			public static readonly StorePropTag Depth = new StorePropTag(12293, PropertyType.Int32, new StorePropInfo("Depth", 12293, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011E5 RID: 4581
			public static readonly StorePropTag CreationTime = new StorePropTag(12295, PropertyType.SysTime, new StorePropInfo("CreationTime", 12295, PropertyType.SysTime, StorePropInfo.Flags.None, 1UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011E6 RID: 4582
			public static readonly StorePropTag LastModificationTime = new StorePropTag(12296, PropertyType.SysTime, new StorePropInfo("LastModificationTime", 12296, PropertyType.SysTime, StorePropInfo.Flags.None, 1UL, new PropertyCategories(3, 4, 7, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011E7 RID: 4583
			public static readonly StorePropTag SearchKey = new StorePropTag(12299, PropertyType.Binary, new StorePropInfo("SearchKey", 12299, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011E8 RID: 4584
			public static readonly StorePropTag SearchKeySvrEid = new StorePropTag(12299, PropertyType.SvrEid, new StorePropInfo("SearchKeySvrEid", 12299, PropertyType.SvrEid, StorePropInfo.Flags.None, 1UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011E9 RID: 4585
			public static readonly StorePropTag TargetEntryId = new StorePropTag(12304, PropertyType.Binary, new StorePropInfo("TargetEntryId", 12304, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011EA RID: 4586
			public static readonly StorePropTag ConversationId = new StorePropTag(12307, PropertyType.Binary, new StorePropInfo("ConversationId", 12307, PropertyType.Binary, StorePropInfo.Flags.None, 2341871806232658048UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011EB RID: 4587
			public static readonly StorePropTag BodyTag = new StorePropTag(12308, PropertyType.Binary, new StorePropInfo("BodyTag", 12308, PropertyType.Binary, StorePropInfo.Flags.None, 4194304UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011EC RID: 4588
			public static readonly StorePropTag ConversationIndexTrackingObsolete = new StorePropTag(12309, PropertyType.Int64, new StorePropInfo("ConversationIndexTrackingObsolete", 12309, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011ED RID: 4589
			public static readonly StorePropTag ConversationIndexTracking = new StorePropTag(12310, PropertyType.Boolean, new StorePropInfo("ConversationIndexTracking", 12310, PropertyType.Boolean, StorePropInfo.Flags.None, 2341871806232658048UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011EE RID: 4590
			public static readonly StorePropTag ArchiveTag = new StorePropTag(12312, PropertyType.Binary, new StorePropInfo("ArchiveTag", 12312, PropertyType.Binary, StorePropInfo.Flags.None, 576460752304472064UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011EF RID: 4591
			public static readonly StorePropTag PolicyTag = new StorePropTag(12313, PropertyType.Binary, new StorePropInfo("PolicyTag", 12313, PropertyType.Binary, StorePropInfo.Flags.None, 2882303761518166016UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011F0 RID: 4592
			public static readonly StorePropTag RetentionPeriod = new StorePropTag(12314, PropertyType.Int32, new StorePropInfo("RetentionPeriod", 12314, PropertyType.Int32, StorePropInfo.Flags.None, 288230376153808896UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011F1 RID: 4593
			public static readonly StorePropTag StartDateEtc = new StorePropTag(12315, PropertyType.Binary, new StorePropInfo("StartDateEtc", 12315, PropertyType.Binary, StorePropInfo.Flags.None, 288230376153808896UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011F2 RID: 4594
			public static readonly StorePropTag RetentionDate = new StorePropTag(12316, PropertyType.SysTime, new StorePropInfo("RetentionDate", 12316, PropertyType.SysTime, StorePropInfo.Flags.None, 288230376153808896UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011F3 RID: 4595
			public static readonly StorePropTag RetentionFlags = new StorePropTag(12317, PropertyType.Int32, new StorePropInfo("RetentionFlags", 12317, PropertyType.Int32, StorePropInfo.Flags.None, 288230376153808896UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011F4 RID: 4596
			public static readonly StorePropTag ArchivePeriod = new StorePropTag(12318, PropertyType.Int32, new StorePropInfo("ArchivePeriod", 12318, PropertyType.Int32, StorePropInfo.Flags.None, 288230376153808896UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011F5 RID: 4597
			public static readonly StorePropTag ArchiveDate = new StorePropTag(12319, PropertyType.SysTime, new StorePropInfo("ArchiveDate", 12319, PropertyType.SysTime, StorePropInfo.Flags.None, 288230376153808896UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011F6 RID: 4598
			public static readonly StorePropTag FormVersion = new StorePropTag(13057, PropertyType.Unicode, new StorePropInfo("FormVersion", 13057, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011F7 RID: 4599
			public static readonly StorePropTag FormCLSID = new StorePropTag(13058, PropertyType.Guid, new StorePropInfo("FormCLSID", 13058, PropertyType.Guid, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011F8 RID: 4600
			public static readonly StorePropTag FormContactName = new StorePropTag(13059, PropertyType.Unicode, new StorePropInfo("FormContactName", 13059, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011F9 RID: 4601
			public static readonly StorePropTag FormCategory = new StorePropTag(13060, PropertyType.Unicode, new StorePropInfo("FormCategory", 13060, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011FA RID: 4602
			public static readonly StorePropTag FormCategorySub = new StorePropTag(13061, PropertyType.Unicode, new StorePropInfo("FormCategorySub", 13061, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011FB RID: 4603
			public static readonly StorePropTag FormHidden = new StorePropTag(13063, PropertyType.Boolean, new StorePropInfo("FormHidden", 13063, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011FC RID: 4604
			public static readonly StorePropTag FormDesignerName = new StorePropTag(13064, PropertyType.Unicode, new StorePropInfo("FormDesignerName", 13064, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011FD RID: 4605
			public static readonly StorePropTag FormDesignerGuid = new StorePropTag(13065, PropertyType.Guid, new StorePropInfo("FormDesignerGuid", 13065, PropertyType.Guid, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011FE RID: 4606
			public static readonly StorePropTag FormMessageBehavior = new StorePropTag(13066, PropertyType.Int32, new StorePropInfo("FormMessageBehavior", 13066, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040011FF RID: 4607
			public static readonly StorePropTag StoreSupportMask = new StorePropTag(13325, PropertyType.Int32, new StorePropInfo("StoreSupportMask", 13325, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001200 RID: 4608
			public static readonly StorePropTag MdbProvider = new StorePropTag(13332, PropertyType.Binary, new StorePropInfo("MdbProvider", 13332, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001201 RID: 4609
			public static readonly StorePropTag EventEmailReminderTimer = new StorePropTag(13408, PropertyType.SysTime, new StorePropInfo("EventEmailReminderTimer", 13408, PropertyType.SysTime, StorePropInfo.Flags.None, 9232379236109516800UL, new PropertyCategories(9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001202 RID: 4610
			public static readonly StorePropTag ContentCount = new StorePropTag(13826, PropertyType.Int32, new StorePropInfo("ContentCount", 13826, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001203 RID: 4611
			public static readonly StorePropTag UnreadCount = new StorePropTag(13827, PropertyType.Int32, new StorePropInfo("UnreadCount", 13827, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001204 RID: 4612
			public static readonly StorePropTag UnreadCountInt64 = new StorePropTag(13827, PropertyType.Int64, new StorePropInfo("UnreadCountInt64", 13827, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001205 RID: 4613
			public static readonly StorePropTag DetailsTable = new StorePropTag(13829, PropertyType.Object, new StorePropInfo("DetailsTable", 13829, PropertyType.Object, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001206 RID: 4614
			public static readonly StorePropTag AppointmentColorName = new StorePropTag(14044, PropertyType.Binary, new StorePropInfo("AppointmentColorName", 14044, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001207 RID: 4615
			public static readonly StorePropTag ContentId = new StorePropTag(14083, PropertyType.Unicode, new StorePropInfo("ContentId", 14083, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001208 RID: 4616
			public static readonly StorePropTag MimeUrl = new StorePropTag(14087, PropertyType.Unicode, new StorePropInfo("MimeUrl", 14087, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001209 RID: 4617
			public static readonly StorePropTag DisplayType = new StorePropTag(14592, PropertyType.Int32, new StorePropInfo("DisplayType", 14592, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400120A RID: 4618
			public static readonly StorePropTag SmtpAddress = new StorePropTag(14846, PropertyType.Unicode, new StorePropInfo("SmtpAddress", 14846, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400120B RID: 4619
			public static readonly StorePropTag SimpleDisplayName = new StorePropTag(14847, PropertyType.Unicode, new StorePropInfo("SimpleDisplayName", 14847, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400120C RID: 4620
			public static readonly StorePropTag Account = new StorePropTag(14848, PropertyType.Unicode, new StorePropInfo("Account", 14848, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400120D RID: 4621
			public static readonly StorePropTag AlternateRecipient = new StorePropTag(14849, PropertyType.Binary, new StorePropInfo("AlternateRecipient", 14849, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400120E RID: 4622
			public static readonly StorePropTag CallbackTelephoneNumber = new StorePropTag(14850, PropertyType.Unicode, new StorePropInfo("CallbackTelephoneNumber", 14850, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400120F RID: 4623
			public static readonly StorePropTag ConversionProhibited = new StorePropTag(14851, PropertyType.Boolean, new StorePropInfo("ConversionProhibited", 14851, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001210 RID: 4624
			public static readonly StorePropTag Generation = new StorePropTag(14853, PropertyType.Unicode, new StorePropInfo("Generation", 14853, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001211 RID: 4625
			public static readonly StorePropTag GivenName = new StorePropTag(14854, PropertyType.Unicode, new StorePropInfo("GivenName", 14854, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001212 RID: 4626
			public static readonly StorePropTag GovernmentIDNumber = new StorePropTag(14855, PropertyType.Unicode, new StorePropInfo("GovernmentIDNumber", 14855, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001213 RID: 4627
			public static readonly StorePropTag BusinessTelephoneNumber = new StorePropTag(14856, PropertyType.Unicode, new StorePropInfo("BusinessTelephoneNumber", 14856, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001214 RID: 4628
			public static readonly StorePropTag HomeTelephoneNumber = new StorePropTag(14857, PropertyType.Unicode, new StorePropInfo("HomeTelephoneNumber", 14857, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001215 RID: 4629
			public static readonly StorePropTag Initials = new StorePropTag(14858, PropertyType.Unicode, new StorePropInfo("Initials", 14858, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001216 RID: 4630
			public static readonly StorePropTag Keyword = new StorePropTag(14859, PropertyType.Unicode, new StorePropInfo("Keyword", 14859, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001217 RID: 4631
			public static readonly StorePropTag Language = new StorePropTag(14860, PropertyType.Unicode, new StorePropInfo("Language", 14860, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001218 RID: 4632
			public static readonly StorePropTag Location = new StorePropTag(14861, PropertyType.Unicode, new StorePropInfo("Location", 14861, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001219 RID: 4633
			public static readonly StorePropTag MailPermission = new StorePropTag(14862, PropertyType.Boolean, new StorePropInfo("MailPermission", 14862, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400121A RID: 4634
			public static readonly StorePropTag MHSCommonName = new StorePropTag(14863, PropertyType.Unicode, new StorePropInfo("MHSCommonName", 14863, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400121B RID: 4635
			public static readonly StorePropTag OrganizationalIDNumber = new StorePropTag(14864, PropertyType.Unicode, new StorePropInfo("OrganizationalIDNumber", 14864, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400121C RID: 4636
			public static readonly StorePropTag SurName = new StorePropTag(14865, PropertyType.Unicode, new StorePropInfo("SurName", 14865, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400121D RID: 4637
			public static readonly StorePropTag OriginalEntryId = new StorePropTag(14866, PropertyType.Binary, new StorePropInfo("OriginalEntryId", 14866, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400121E RID: 4638
			public static readonly StorePropTag OriginalDisplayName = new StorePropTag(14867, PropertyType.Unicode, new StorePropInfo("OriginalDisplayName", 14867, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400121F RID: 4639
			public static readonly StorePropTag OriginalSearchKey = new StorePropTag(14868, PropertyType.Binary, new StorePropInfo("OriginalSearchKey", 14868, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001220 RID: 4640
			public static readonly StorePropTag PostalAddress = new StorePropTag(14869, PropertyType.Unicode, new StorePropInfo("PostalAddress", 14869, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001221 RID: 4641
			public static readonly StorePropTag CompanyName = new StorePropTag(14870, PropertyType.Unicode, new StorePropInfo("CompanyName", 14870, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001222 RID: 4642
			public static readonly StorePropTag Title = new StorePropTag(14871, PropertyType.Unicode, new StorePropInfo("Title", 14871, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001223 RID: 4643
			public static readonly StorePropTag DepartmentName = new StorePropTag(14872, PropertyType.Unicode, new StorePropInfo("DepartmentName", 14872, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001224 RID: 4644
			public static readonly StorePropTag OfficeLocation = new StorePropTag(14873, PropertyType.Unicode, new StorePropInfo("OfficeLocation", 14873, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001225 RID: 4645
			public static readonly StorePropTag PrimaryTelephoneNumber = new StorePropTag(14874, PropertyType.Unicode, new StorePropInfo("PrimaryTelephoneNumber", 14874, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001226 RID: 4646
			public static readonly StorePropTag Business2TelephoneNumber = new StorePropTag(14875, PropertyType.Unicode, new StorePropInfo("Business2TelephoneNumber", 14875, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001227 RID: 4647
			public static readonly StorePropTag Business2TelephoneNumberMv = new StorePropTag(14875, PropertyType.MVUnicode, new StorePropInfo("Business2TelephoneNumberMv", 14875, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001228 RID: 4648
			public static readonly StorePropTag MobileTelephoneNumber = new StorePropTag(14876, PropertyType.Unicode, new StorePropInfo("MobileTelephoneNumber", 14876, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001229 RID: 4649
			public static readonly StorePropTag RadioTelephoneNumber = new StorePropTag(14877, PropertyType.Unicode, new StorePropInfo("RadioTelephoneNumber", 14877, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400122A RID: 4650
			public static readonly StorePropTag CarTelephoneNumber = new StorePropTag(14878, PropertyType.Unicode, new StorePropInfo("CarTelephoneNumber", 14878, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400122B RID: 4651
			public static readonly StorePropTag OtherTelephoneNumber = new StorePropTag(14879, PropertyType.Unicode, new StorePropInfo("OtherTelephoneNumber", 14879, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400122C RID: 4652
			public static readonly StorePropTag TransmitableDisplayName = new StorePropTag(14880, PropertyType.Unicode, new StorePropInfo("TransmitableDisplayName", 14880, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400122D RID: 4653
			public static readonly StorePropTag PagerTelephoneNumber = new StorePropTag(14881, PropertyType.Unicode, new StorePropInfo("PagerTelephoneNumber", 14881, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400122E RID: 4654
			public static readonly StorePropTag UserCertificate = new StorePropTag(14882, PropertyType.Binary, new StorePropInfo("UserCertificate", 14882, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400122F RID: 4655
			public static readonly StorePropTag PrimaryFaxNumber = new StorePropTag(14883, PropertyType.Unicode, new StorePropInfo("PrimaryFaxNumber", 14883, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001230 RID: 4656
			public static readonly StorePropTag BusinessFaxNumber = new StorePropTag(14884, PropertyType.Unicode, new StorePropInfo("BusinessFaxNumber", 14884, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001231 RID: 4657
			public static readonly StorePropTag HomeFaxNumber = new StorePropTag(14885, PropertyType.Unicode, new StorePropInfo("HomeFaxNumber", 14885, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001232 RID: 4658
			public static readonly StorePropTag Country = new StorePropTag(14886, PropertyType.Unicode, new StorePropInfo("Country", 14886, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001233 RID: 4659
			public static readonly StorePropTag Locality = new StorePropTag(14887, PropertyType.Unicode, new StorePropInfo("Locality", 14887, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001234 RID: 4660
			public static readonly StorePropTag StateOrProvince = new StorePropTag(14888, PropertyType.Unicode, new StorePropInfo("StateOrProvince", 14888, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001235 RID: 4661
			public static readonly StorePropTag StreetAddress = new StorePropTag(14889, PropertyType.Unicode, new StorePropInfo("StreetAddress", 14889, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001236 RID: 4662
			public static readonly StorePropTag PostalCode = new StorePropTag(14890, PropertyType.Unicode, new StorePropInfo("PostalCode", 14890, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001237 RID: 4663
			public static readonly StorePropTag PostOfficeBox = new StorePropTag(14891, PropertyType.Unicode, new StorePropInfo("PostOfficeBox", 14891, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001238 RID: 4664
			public static readonly StorePropTag TelexNumber = new StorePropTag(14892, PropertyType.Unicode, new StorePropInfo("TelexNumber", 14892, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001239 RID: 4665
			public static readonly StorePropTag ISDNNumber = new StorePropTag(14893, PropertyType.Unicode, new StorePropInfo("ISDNNumber", 14893, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400123A RID: 4666
			public static readonly StorePropTag AssistantTelephoneNumber = new StorePropTag(14894, PropertyType.Unicode, new StorePropInfo("AssistantTelephoneNumber", 14894, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400123B RID: 4667
			public static readonly StorePropTag Home2TelephoneNumber = new StorePropTag(14895, PropertyType.Unicode, new StorePropInfo("Home2TelephoneNumber", 14895, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400123C RID: 4668
			public static readonly StorePropTag Home2TelephoneNumberMv = new StorePropTag(14895, PropertyType.MVUnicode, new StorePropInfo("Home2TelephoneNumberMv", 14895, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400123D RID: 4669
			public static readonly StorePropTag Assistant = new StorePropTag(14896, PropertyType.Unicode, new StorePropInfo("Assistant", 14896, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400123E RID: 4670
			public static readonly StorePropTag SendRichInfo = new StorePropTag(14912, PropertyType.Boolean, new StorePropInfo("SendRichInfo", 14912, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400123F RID: 4671
			public static readonly StorePropTag WeddingAnniversary = new StorePropTag(14913, PropertyType.SysTime, new StorePropInfo("WeddingAnniversary", 14913, PropertyType.SysTime, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001240 RID: 4672
			public static readonly StorePropTag Birthday = new StorePropTag(14914, PropertyType.SysTime, new StorePropInfo("Birthday", 14914, PropertyType.SysTime, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001241 RID: 4673
			public static readonly StorePropTag Hobbies = new StorePropTag(14915, PropertyType.Unicode, new StorePropInfo("Hobbies", 14915, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001242 RID: 4674
			public static readonly StorePropTag MiddleName = new StorePropTag(14916, PropertyType.Unicode, new StorePropInfo("MiddleName", 14916, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001243 RID: 4675
			public static readonly StorePropTag DisplayNamePrefix = new StorePropTag(14917, PropertyType.Unicode, new StorePropInfo("DisplayNamePrefix", 14917, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001244 RID: 4676
			public static readonly StorePropTag Profession = new StorePropTag(14918, PropertyType.Unicode, new StorePropInfo("Profession", 14918, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001245 RID: 4677
			public static readonly StorePropTag ReferredByName = new StorePropTag(14919, PropertyType.Unicode, new StorePropInfo("ReferredByName", 14919, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001246 RID: 4678
			public static readonly StorePropTag SpouseName = new StorePropTag(14920, PropertyType.Unicode, new StorePropInfo("SpouseName", 14920, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001247 RID: 4679
			public static readonly StorePropTag ComputerNetworkName = new StorePropTag(14921, PropertyType.Unicode, new StorePropInfo("ComputerNetworkName", 14921, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001248 RID: 4680
			public static readonly StorePropTag CustomerId = new StorePropTag(14922, PropertyType.Unicode, new StorePropInfo("CustomerId", 14922, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001249 RID: 4681
			public static readonly StorePropTag TTYTDDPhoneNumber = new StorePropTag(14923, PropertyType.Unicode, new StorePropInfo("TTYTDDPhoneNumber", 14923, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400124A RID: 4682
			public static readonly StorePropTag FTPSite = new StorePropTag(14924, PropertyType.Unicode, new StorePropInfo("FTPSite", 14924, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400124B RID: 4683
			public static readonly StorePropTag Gender = new StorePropTag(14925, PropertyType.Int16, new StorePropInfo("Gender", 14925, PropertyType.Int16, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400124C RID: 4684
			public static readonly StorePropTag ManagerName = new StorePropTag(14926, PropertyType.Unicode, new StorePropInfo("ManagerName", 14926, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400124D RID: 4685
			public static readonly StorePropTag NickName = new StorePropTag(14927, PropertyType.Unicode, new StorePropInfo("NickName", 14927, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400124E RID: 4686
			public static readonly StorePropTag PersonalHomePage = new StorePropTag(14928, PropertyType.Unicode, new StorePropInfo("PersonalHomePage", 14928, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400124F RID: 4687
			public static readonly StorePropTag BusinessHomePage = new StorePropTag(14929, PropertyType.Unicode, new StorePropInfo("BusinessHomePage", 14929, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001250 RID: 4688
			public static readonly StorePropTag ContactVersion = new StorePropTag(14930, PropertyType.Guid, new StorePropInfo("ContactVersion", 14930, PropertyType.Guid, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001251 RID: 4689
			public static readonly StorePropTag ContactEntryIds = new StorePropTag(14931, PropertyType.MVBinary, new StorePropInfo("ContactEntryIds", 14931, PropertyType.MVBinary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001252 RID: 4690
			public static readonly StorePropTag ContactAddressTypes = new StorePropTag(14932, PropertyType.MVUnicode, new StorePropInfo("ContactAddressTypes", 14932, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001253 RID: 4691
			public static readonly StorePropTag ContactDefaultAddressIndex = new StorePropTag(14933, PropertyType.Int32, new StorePropInfo("ContactDefaultAddressIndex", 14933, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001254 RID: 4692
			public static readonly StorePropTag ContactEmailAddress = new StorePropTag(14934, PropertyType.MVUnicode, new StorePropInfo("ContactEmailAddress", 14934, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001255 RID: 4693
			public static readonly StorePropTag CompanyMainPhoneNumber = new StorePropTag(14935, PropertyType.Unicode, new StorePropInfo("CompanyMainPhoneNumber", 14935, PropertyType.Unicode, StorePropInfo.Flags.None, 2305843009213694208UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001256 RID: 4694
			public static readonly StorePropTag ChildrensNames = new StorePropTag(14936, PropertyType.MVUnicode, new StorePropInfo("ChildrensNames", 14936, PropertyType.MVUnicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001257 RID: 4695
			public static readonly StorePropTag HomeAddressCity = new StorePropTag(14937, PropertyType.Unicode, new StorePropInfo("HomeAddressCity", 14937, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001258 RID: 4696
			public static readonly StorePropTag HomeAddressCountry = new StorePropTag(14938, PropertyType.Unicode, new StorePropInfo("HomeAddressCountry", 14938, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001259 RID: 4697
			public static readonly StorePropTag HomeAddressPostalCode = new StorePropTag(14939, PropertyType.Unicode, new StorePropInfo("HomeAddressPostalCode", 14939, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400125A RID: 4698
			public static readonly StorePropTag HomeAddressStateOrProvince = new StorePropTag(14940, PropertyType.Unicode, new StorePropInfo("HomeAddressStateOrProvince", 14940, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400125B RID: 4699
			public static readonly StorePropTag HomeAddressStreet = new StorePropTag(14941, PropertyType.Unicode, new StorePropInfo("HomeAddressStreet", 14941, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400125C RID: 4700
			public static readonly StorePropTag HomeAddressPostOfficeBox = new StorePropTag(14942, PropertyType.Unicode, new StorePropInfo("HomeAddressPostOfficeBox", 14942, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400125D RID: 4701
			public static readonly StorePropTag OtherAddressCity = new StorePropTag(14943, PropertyType.Unicode, new StorePropInfo("OtherAddressCity", 14943, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400125E RID: 4702
			public static readonly StorePropTag OtherAddressCountry = new StorePropTag(14944, PropertyType.Unicode, new StorePropInfo("OtherAddressCountry", 14944, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400125F RID: 4703
			public static readonly StorePropTag OtherAddressPostalCode = new StorePropTag(14945, PropertyType.Unicode, new StorePropInfo("OtherAddressPostalCode", 14945, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001260 RID: 4704
			public static readonly StorePropTag OtherAddressStateOrProvince = new StorePropTag(14946, PropertyType.Unicode, new StorePropInfo("OtherAddressStateOrProvince", 14946, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001261 RID: 4705
			public static readonly StorePropTag OtherAddressStreet = new StorePropTag(14947, PropertyType.Unicode, new StorePropInfo("OtherAddressStreet", 14947, PropertyType.Unicode, StorePropInfo.Flags.None, 256UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001262 RID: 4706
			public static readonly StorePropTag OtherAddressPostOfficeBox = new StorePropTag(14948, PropertyType.Unicode, new StorePropInfo("OtherAddressPostOfficeBox", 14948, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001263 RID: 4707
			public static readonly StorePropTag UserX509CertificateABSearchPath = new StorePropTag(14960, PropertyType.MVBinary, new StorePropInfo("UserX509CertificateABSearchPath", 14960, PropertyType.MVBinary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001264 RID: 4708
			public static readonly StorePropTag SendInternetEncoding = new StorePropTag(14961, PropertyType.Int32, new StorePropInfo("SendInternetEncoding", 14961, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001265 RID: 4709
			public static readonly StorePropTag PartnerNetworkId = new StorePropTag(14966, PropertyType.Unicode, new StorePropInfo("PartnerNetworkId", 14966, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001266 RID: 4710
			public static readonly StorePropTag PartnerNetworkUserId = new StorePropTag(14967, PropertyType.Unicode, new StorePropInfo("PartnerNetworkUserId", 14967, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001267 RID: 4711
			public static readonly StorePropTag PartnerNetworkThumbnailPhotoUrl = new StorePropTag(14968, PropertyType.Unicode, new StorePropInfo("PartnerNetworkThumbnailPhotoUrl", 14968, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001268 RID: 4712
			public static readonly StorePropTag PartnerNetworkProfilePhotoUrl = new StorePropTag(14969, PropertyType.Unicode, new StorePropInfo("PartnerNetworkProfilePhotoUrl", 14969, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001269 RID: 4713
			public static readonly StorePropTag PartnerNetworkContactType = new StorePropTag(14970, PropertyType.Unicode, new StorePropInfo("PartnerNetworkContactType", 14970, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400126A RID: 4714
			public static readonly StorePropTag RelevanceScore = new StorePropTag(14971, PropertyType.Int32, new StorePropInfo("RelevanceScore", 14971, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400126B RID: 4715
			public static readonly StorePropTag IsDistributionListContact = new StorePropTag(14972, PropertyType.Boolean, new StorePropInfo("IsDistributionListContact", 14972, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400126C RID: 4716
			public static readonly StorePropTag IsPromotedContact = new StorePropTag(14973, PropertyType.Boolean, new StorePropInfo("IsPromotedContact", 14973, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400126D RID: 4717
			public static readonly StorePropTag OrgUnitName = new StorePropTag(15358, PropertyType.Unicode, new StorePropInfo("OrgUnitName", 15358, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400126E RID: 4718
			public static readonly StorePropTag OrganizationName = new StorePropTag(15359, PropertyType.Unicode, new StorePropInfo("OrganizationName", 15359, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400126F RID: 4719
			public static readonly StorePropTag TestBlobProperty = new StorePropTag(15616, PropertyType.Int64, new StorePropInfo("TestBlobProperty", 15616, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001270 RID: 4720
			public static readonly StorePropTag FilteringHooks = new StorePropTag(15624, PropertyType.Binary, new StorePropInfo("FilteringHooks", 15624, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001271 RID: 4721
			public static readonly StorePropTag MailboxPartitionNumber = new StorePropTag(15775, PropertyType.Int32, new StorePropInfo("MailboxPartitionNumber", 15775, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001272 RID: 4722
			public static readonly StorePropTag MailboxNumberInternal = new StorePropTag(15776, PropertyType.Int32, new StorePropInfo("MailboxNumberInternal", 15776, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001273 RID: 4723
			public static readonly StorePropTag VirtualParentDisplay = new StorePropTag(15781, PropertyType.Unicode, new StorePropInfo("VirtualParentDisplay", 15781, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001274 RID: 4724
			public static readonly StorePropTag InternalConversationIndexTracking = new StorePropTag(15784, PropertyType.Boolean, new StorePropInfo("InternalConversationIndexTracking", 15784, PropertyType.Boolean, StorePropInfo.Flags.None, 2341871806232658048UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001275 RID: 4725
			public static readonly StorePropTag InternalConversationIndex = new StorePropTag(15785, PropertyType.Binary, new StorePropInfo("InternalConversationIndex", 15785, PropertyType.Binary, StorePropInfo.Flags.None, 2341871806232658048UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001276 RID: 4726
			public static readonly StorePropTag ConversationItemConversationId = new StorePropTag(15786, PropertyType.Binary, new StorePropInfo("ConversationItemConversationId", 15786, PropertyType.Binary, StorePropInfo.Flags.None, 2341871806232658048UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001277 RID: 4727
			public static readonly StorePropTag VirtualUnreadMessageCount = new StorePropTag(15787, PropertyType.Int64, new StorePropInfo("VirtualUnreadMessageCount", 15787, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001278 RID: 4728
			public static readonly StorePropTag VirtualIsRead = new StorePropTag(15788, PropertyType.Boolean, new StorePropInfo("VirtualIsRead", 15788, PropertyType.Boolean, StorePropInfo.Flags.None, 2UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001279 RID: 4729
			public static readonly StorePropTag IsReadColumn = new StorePropTag(15789, PropertyType.Boolean, new StorePropInfo("IsReadColumn", 15789, PropertyType.Boolean, StorePropInfo.Flags.None, 2UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400127A RID: 4730
			public static readonly StorePropTag Internal9ByteChangeNumber = new StorePropTag(15791, PropertyType.Binary, new StorePropInfo("Internal9ByteChangeNumber", 15791, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400127B RID: 4731
			public static readonly StorePropTag Internal9ByteReadCnNew = new StorePropTag(15792, PropertyType.Binary, new StorePropInfo("Internal9ByteReadCnNew", 15792, PropertyType.Binary, StorePropInfo.Flags.None, 2UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400127C RID: 4732
			public static readonly StorePropTag CategoryHeaderLevelStub1 = new StorePropTag(15793, PropertyType.Boolean, new StorePropInfo("CategoryHeaderLevelStub1", 15793, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400127D RID: 4733
			public static readonly StorePropTag CategoryHeaderLevelStub2 = new StorePropTag(15794, PropertyType.Boolean, new StorePropInfo("CategoryHeaderLevelStub2", 15794, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400127E RID: 4734
			public static readonly StorePropTag CategoryHeaderLevelStub3 = new StorePropTag(15795, PropertyType.Boolean, new StorePropInfo("CategoryHeaderLevelStub3", 15795, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400127F RID: 4735
			public static readonly StorePropTag CategoryHeaderAggregateProp0 = new StorePropTag(15796, PropertyType.Binary, new StorePropInfo("CategoryHeaderAggregateProp0", 15796, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001280 RID: 4736
			public static readonly StorePropTag CategoryHeaderAggregateProp1 = new StorePropTag(15797, PropertyType.Binary, new StorePropInfo("CategoryHeaderAggregateProp1", 15797, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001281 RID: 4737
			public static readonly StorePropTag CategoryHeaderAggregateProp2 = new StorePropTag(15798, PropertyType.Binary, new StorePropInfo("CategoryHeaderAggregateProp2", 15798, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001282 RID: 4738
			public static readonly StorePropTag CategoryHeaderAggregateProp3 = new StorePropTag(15799, PropertyType.Binary, new StorePropInfo("CategoryHeaderAggregateProp3", 15799, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001283 RID: 4739
			public static readonly StorePropTag MessageFlagsActual = new StorePropTag(15805, PropertyType.Int32, new StorePropInfo("MessageFlagsActual", 15805, PropertyType.Int32, StorePropInfo.Flags.None, 2UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001284 RID: 4740
			public static readonly StorePropTag InternalChangeKey = new StorePropTag(15806, PropertyType.Binary, new StorePropInfo("InternalChangeKey", 15806, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001285 RID: 4741
			public static readonly StorePropTag InternalSourceKey = new StorePropTag(15807, PropertyType.Binary, new StorePropInfo("InternalSourceKey", 15807, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				0,
				1,
				2,
				3,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001286 RID: 4742
			public static readonly StorePropTag HeaderFolderEntryId = new StorePropTag(15882, PropertyType.Binary, new StorePropInfo("HeaderFolderEntryId", 15882, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001287 RID: 4743
			public static readonly StorePropTag RemoteProgress = new StorePropTag(15883, PropertyType.Int32, new StorePropInfo("RemoteProgress", 15883, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001288 RID: 4744
			public static readonly StorePropTag RemoteProgressText = new StorePropTag(15884, PropertyType.Unicode, new StorePropInfo("RemoteProgressText", 15884, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001289 RID: 4745
			public static readonly StorePropTag VID = new StorePropTag(16264, PropertyType.Int64, new StorePropInfo("VID", 16264, PropertyType.Int64, StorePropInfo.Flags.None, 1UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400128A RID: 4746
			public static readonly StorePropTag GVid = new StorePropTag(16265, PropertyType.Binary, new StorePropInfo("GVid", 16265, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400128B RID: 4747
			public static readonly StorePropTag GDID = new StorePropTag(16266, PropertyType.Binary, new StorePropInfo("GDID", 16266, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400128C RID: 4748
			public static readonly StorePropTag XVid = new StorePropTag(16277, PropertyType.Binary, new StorePropInfo("XVid", 16277, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400128D RID: 4749
			public static readonly StorePropTag GDefVid = new StorePropTag(16278, PropertyType.Binary, new StorePropInfo("GDefVid", 16278, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400128E RID: 4750
			public static readonly StorePropTag PrimaryMailboxOverQuota = new StorePropTag(16322, PropertyType.Boolean, new StorePropInfo("PrimaryMailboxOverQuota", 16322, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400128F RID: 4751
			public static readonly StorePropTag InternalPostReply = new StorePropTag(16341, PropertyType.Binary, new StorePropInfo("InternalPostReply", 16341, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001290 RID: 4752
			public static readonly StorePropTag PreviewUnread = new StorePropTag(16344, PropertyType.Unicode, new StorePropInfo("PreviewUnread", 16344, PropertyType.Unicode, StorePropInfo.Flags.None, 2UL, new PropertyCategories(3, 9, 10, 15)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001291 RID: 4753
			public static readonly StorePropTag Preview = new StorePropTag(16345, PropertyType.Unicode, new StorePropInfo("Preview", 16345, PropertyType.Unicode, StorePropInfo.Flags.Private, 2UL, new PropertyCategories(15)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001292 RID: 4754
			public static readonly StorePropTag InternetCPID = new StorePropTag(16350, PropertyType.Int32, new StorePropInfo("InternetCPID", 16350, PropertyType.Int32, StorePropInfo.Flags.None, 1UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001293 RID: 4755
			public static readonly StorePropTag AutoResponseSuppress = new StorePropTag(16351, PropertyType.Int32, new StorePropInfo("AutoResponseSuppress", 16351, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001294 RID: 4756
			public static readonly StorePropTag HasDAMs = new StorePropTag(16362, PropertyType.Boolean, new StorePropInfo("HasDAMs", 16362, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001295 RID: 4757
			public static readonly StorePropTag DeferredSendNumber = new StorePropTag(16363, PropertyType.Int32, new StorePropInfo("DeferredSendNumber", 16363, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001296 RID: 4758
			public static readonly StorePropTag DeferredSendUnits = new StorePropTag(16364, PropertyType.Int32, new StorePropInfo("DeferredSendUnits", 16364, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001297 RID: 4759
			public static readonly StorePropTag ExpiryNumber = new StorePropTag(16365, PropertyType.Int32, new StorePropInfo("ExpiryNumber", 16365, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001298 RID: 4760
			public static readonly StorePropTag ExpiryUnits = new StorePropTag(16366, PropertyType.Int32, new StorePropInfo("ExpiryUnits", 16366, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001299 RID: 4761
			public static readonly StorePropTag DeferredSendTime = new StorePropTag(16367, PropertyType.SysTime, new StorePropInfo("DeferredSendTime", 16367, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400129A RID: 4762
			public static readonly StorePropTag MessageLocaleId = new StorePropTag(16369, PropertyType.Int32, new StorePropInfo("MessageLocaleId", 16369, PropertyType.Int32, StorePropInfo.Flags.None, 1UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400129B RID: 4763
			public static readonly StorePropTag RuleTriggerHistory = new StorePropTag(16370, PropertyType.Binary, new StorePropInfo("RuleTriggerHistory", 16370, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400129C RID: 4764
			public static readonly StorePropTag MoveToStoreEid = new StorePropTag(16371, PropertyType.Binary, new StorePropInfo("MoveToStoreEid", 16371, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400129D RID: 4765
			public static readonly StorePropTag MoveToFolderEid = new StorePropTag(16372, PropertyType.Binary, new StorePropInfo("MoveToFolderEid", 16372, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400129E RID: 4766
			public static readonly StorePropTag StorageQuotaLimit = new StorePropTag(16373, PropertyType.Int32, new StorePropInfo("StorageQuotaLimit", 16373, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400129F RID: 4767
			public static readonly StorePropTag ExcessStorageUsed = new StorePropTag(16374, PropertyType.Int32, new StorePropInfo("ExcessStorageUsed", 16374, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012A0 RID: 4768
			public static readonly StorePropTag ServerGeneratingQuotaMsg = new StorePropTag(16375, PropertyType.Unicode, new StorePropInfo("ServerGeneratingQuotaMsg", 16375, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012A1 RID: 4769
			public static readonly StorePropTag CreatorName = new StorePropTag(16376, PropertyType.Unicode, new StorePropInfo("CreatorName", 16376, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012A2 RID: 4770
			public static readonly StorePropTag CreatorEntryId = new StorePropTag(16377, PropertyType.Binary, new StorePropInfo("CreatorEntryId", 16377, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012A3 RID: 4771
			public static readonly StorePropTag LastModifierName = new StorePropTag(16378, PropertyType.Unicode, new StorePropInfo("LastModifierName", 16378, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 5, 7, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012A4 RID: 4772
			public static readonly StorePropTag LastModifierEntryId = new StorePropTag(16379, PropertyType.Binary, new StorePropInfo("LastModifierEntryId", 16379, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 7, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012A5 RID: 4773
			public static readonly StorePropTag MessageCodePage = new StorePropTag(16381, PropertyType.Int32, new StorePropInfo("MessageCodePage", 16381, PropertyType.Int32, StorePropInfo.Flags.None, 1UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012A6 RID: 4774
			public static readonly StorePropTag QuotaType = new StorePropTag(16382, PropertyType.Int32, new StorePropInfo("QuotaType", 16382, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012A7 RID: 4775
			public static readonly StorePropTag IsPublicFolderQuotaMessage = new StorePropTag(16383, PropertyType.Boolean, new StorePropInfo("IsPublicFolderQuotaMessage", 16383, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012A8 RID: 4776
			public static readonly StorePropTag NewAttach = new StorePropTag(16384, PropertyType.Int32, new StorePropInfo("NewAttach", 16384, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012A9 RID: 4777
			public static readonly StorePropTag StartEmbed = new StorePropTag(16385, PropertyType.Int32, new StorePropInfo("StartEmbed", 16385, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012AA RID: 4778
			public static readonly StorePropTag EndEmbed = new StorePropTag(16386, PropertyType.Int32, new StorePropInfo("EndEmbed", 16386, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012AB RID: 4779
			public static readonly StorePropTag StartRecip = new StorePropTag(16387, PropertyType.Int32, new StorePropInfo("StartRecip", 16387, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012AC RID: 4780
			public static readonly StorePropTag EndRecip = new StorePropTag(16388, PropertyType.Int32, new StorePropInfo("EndRecip", 16388, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012AD RID: 4781
			public static readonly StorePropTag EndCcRecip = new StorePropTag(16389, PropertyType.Int32, new StorePropInfo("EndCcRecip", 16389, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012AE RID: 4782
			public static readonly StorePropTag EndBccRecip = new StorePropTag(16390, PropertyType.Int32, new StorePropInfo("EndBccRecip", 16390, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012AF RID: 4783
			public static readonly StorePropTag EndP1Recip = new StorePropTag(16391, PropertyType.Int32, new StorePropInfo("EndP1Recip", 16391, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012B0 RID: 4784
			public static readonly StorePropTag DNPrefix = new StorePropTag(16392, PropertyType.Unicode, new StorePropInfo("DNPrefix", 16392, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012B1 RID: 4785
			public static readonly StorePropTag StartTopFolder = new StorePropTag(16393, PropertyType.Int32, new StorePropInfo("StartTopFolder", 16393, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012B2 RID: 4786
			public static readonly StorePropTag StartSubFolder = new StorePropTag(16394, PropertyType.Int32, new StorePropInfo("StartSubFolder", 16394, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012B3 RID: 4787
			public static readonly StorePropTag EndFolder = new StorePropTag(16395, PropertyType.Int32, new StorePropInfo("EndFolder", 16395, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012B4 RID: 4788
			public static readonly StorePropTag StartMessage = new StorePropTag(16396, PropertyType.Int32, new StorePropInfo("StartMessage", 16396, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012B5 RID: 4789
			public static readonly StorePropTag EndMessage = new StorePropTag(16397, PropertyType.Int32, new StorePropInfo("EndMessage", 16397, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012B6 RID: 4790
			public static readonly StorePropTag EndAttach = new StorePropTag(16398, PropertyType.Int32, new StorePropInfo("EndAttach", 16398, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012B7 RID: 4791
			public static readonly StorePropTag EcWarning = new StorePropTag(16399, PropertyType.Int32, new StorePropInfo("EcWarning", 16399, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012B8 RID: 4792
			public static readonly StorePropTag StartFAIMessage = new StorePropTag(16400, PropertyType.Int32, new StorePropInfo("StartFAIMessage", 16400, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012B9 RID: 4793
			public static readonly StorePropTag NewFXFolder = new StorePropTag(16401, PropertyType.Binary, new StorePropInfo("NewFXFolder", 16401, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012BA RID: 4794
			public static readonly StorePropTag IncrSyncChange = new StorePropTag(16402, PropertyType.Int32, new StorePropInfo("IncrSyncChange", 16402, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012BB RID: 4795
			public static readonly StorePropTag IncrSyncDelete = new StorePropTag(16403, PropertyType.Int32, new StorePropInfo("IncrSyncDelete", 16403, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012BC RID: 4796
			public static readonly StorePropTag IncrSyncEnd = new StorePropTag(16404, PropertyType.Int32, new StorePropInfo("IncrSyncEnd", 16404, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012BD RID: 4797
			public static readonly StorePropTag IncrSyncMessage = new StorePropTag(16405, PropertyType.Int32, new StorePropInfo("IncrSyncMessage", 16405, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012BE RID: 4798
			public static readonly StorePropTag FastTransferDelProp = new StorePropTag(16406, PropertyType.Int32, new StorePropInfo("FastTransferDelProp", 16406, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012BF RID: 4799
			public static readonly StorePropTag IdsetGiven = new StorePropTag(16407, PropertyType.Binary, new StorePropInfo("IdsetGiven", 16407, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012C0 RID: 4800
			public static readonly StorePropTag IdsetGivenInt32 = new StorePropTag(16407, PropertyType.Int32, new StorePropInfo("IdsetGivenInt32", 16407, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012C1 RID: 4801
			public static readonly StorePropTag FastTransferErrorInfo = new StorePropTag(16408, PropertyType.Int32, new StorePropInfo("FastTransferErrorInfo", 16408, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012C2 RID: 4802
			public static readonly StorePropTag SenderFlags = new StorePropTag(16409, PropertyType.Int32, new StorePropInfo("SenderFlags", 16409, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012C3 RID: 4803
			public static readonly StorePropTag SentRepresentingFlags = new StorePropTag(16410, PropertyType.Int32, new StorePropInfo("SentRepresentingFlags", 16410, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012C4 RID: 4804
			public static readonly StorePropTag RcvdByFlags = new StorePropTag(16411, PropertyType.Int32, new StorePropInfo("RcvdByFlags", 16411, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012C5 RID: 4805
			public static readonly StorePropTag RcvdRepresentingFlags = new StorePropTag(16412, PropertyType.Int32, new StorePropInfo("RcvdRepresentingFlags", 16412, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012C6 RID: 4806
			public static readonly StorePropTag OriginalSenderFlags = new StorePropTag(16413, PropertyType.Int32, new StorePropInfo("OriginalSenderFlags", 16413, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012C7 RID: 4807
			public static readonly StorePropTag OriginalSentRepresentingFlags = new StorePropTag(16414, PropertyType.Int32, new StorePropInfo("OriginalSentRepresentingFlags", 16414, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012C8 RID: 4808
			public static readonly StorePropTag ReportFlags = new StorePropTag(16415, PropertyType.Int32, new StorePropInfo("ReportFlags", 16415, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012C9 RID: 4809
			public static readonly StorePropTag ReadReceiptFlags = new StorePropTag(16416, PropertyType.Int32, new StorePropInfo("ReadReceiptFlags", 16416, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012CA RID: 4810
			public static readonly StorePropTag SoftDeletes = new StorePropTag(16417, PropertyType.Binary, new StorePropInfo("SoftDeletes", 16417, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012CB RID: 4811
			public static readonly StorePropTag CreatorAddressType = new StorePropTag(16418, PropertyType.Unicode, new StorePropInfo("CreatorAddressType", 16418, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012CC RID: 4812
			public static readonly StorePropTag CreatorEmailAddr = new StorePropTag(16419, PropertyType.Unicode, new StorePropInfo("CreatorEmailAddr", 16419, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012CD RID: 4813
			public static readonly StorePropTag LastModifierAddressType = new StorePropTag(16420, PropertyType.Unicode, new StorePropInfo("LastModifierAddressType", 16420, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 7, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012CE RID: 4814
			public static readonly StorePropTag LastModifierEmailAddr = new StorePropTag(16421, PropertyType.Unicode, new StorePropInfo("LastModifierEmailAddr", 16421, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 7, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012CF RID: 4815
			public static readonly StorePropTag ReportAddressType = new StorePropTag(16422, PropertyType.Unicode, new StorePropInfo("ReportAddressType", 16422, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012D0 RID: 4816
			public static readonly StorePropTag ReportEmailAddress = new StorePropTag(16423, PropertyType.Unicode, new StorePropInfo("ReportEmailAddress", 16423, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012D1 RID: 4817
			public static readonly StorePropTag ReportDisplayName = new StorePropTag(16424, PropertyType.Unicode, new StorePropInfo("ReportDisplayName", 16424, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012D2 RID: 4818
			public static readonly StorePropTag ReadReceiptAddressType = new StorePropTag(16425, PropertyType.Unicode, new StorePropInfo("ReadReceiptAddressType", 16425, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012D3 RID: 4819
			public static readonly StorePropTag ReadReceiptEmailAddress = new StorePropTag(16426, PropertyType.Unicode, new StorePropInfo("ReadReceiptEmailAddress", 16426, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012D4 RID: 4820
			public static readonly StorePropTag ReadReceiptDisplayName = new StorePropTag(16427, PropertyType.Unicode, new StorePropInfo("ReadReceiptDisplayName", 16427, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012D5 RID: 4821
			public static readonly StorePropTag IdsetRead = new StorePropTag(16429, PropertyType.Binary, new StorePropInfo("IdsetRead", 16429, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012D6 RID: 4822
			public static readonly StorePropTag IdsetUnread = new StorePropTag(16430, PropertyType.Binary, new StorePropInfo("IdsetUnread", 16430, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012D7 RID: 4823
			public static readonly StorePropTag IncrSyncRead = new StorePropTag(16431, PropertyType.Int32, new StorePropInfo("IncrSyncRead", 16431, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012D8 RID: 4824
			public static readonly StorePropTag SenderSimpleDisplayName = new StorePropTag(16432, PropertyType.Unicode, new StorePropInfo("SenderSimpleDisplayName", 16432, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012D9 RID: 4825
			public static readonly StorePropTag SentRepresentingSimpleDisplayName = new StorePropTag(16433, PropertyType.Unicode, new StorePropInfo("SentRepresentingSimpleDisplayName", 16433, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012DA RID: 4826
			public static readonly StorePropTag OriginalSenderSimpleDisplayName = new StorePropTag(16434, PropertyType.Unicode, new StorePropInfo("OriginalSenderSimpleDisplayName", 16434, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012DB RID: 4827
			public static readonly StorePropTag OriginalSentRepresentingSimpleDisplayName = new StorePropTag(16435, PropertyType.Unicode, new StorePropInfo("OriginalSentRepresentingSimpleDisplayName", 16435, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012DC RID: 4828
			public static readonly StorePropTag ReceivedBySimpleDisplayName = new StorePropTag(16436, PropertyType.Unicode, new StorePropInfo("ReceivedBySimpleDisplayName", 16436, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012DD RID: 4829
			public static readonly StorePropTag ReceivedRepresentingSimpleDisplayName = new StorePropTag(16437, PropertyType.Unicode, new StorePropInfo("ReceivedRepresentingSimpleDisplayName", 16437, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012DE RID: 4830
			public static readonly StorePropTag ReadReceiptSimpleDisplayName = new StorePropTag(16438, PropertyType.Unicode, new StorePropInfo("ReadReceiptSimpleDisplayName", 16438, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012DF RID: 4831
			public static readonly StorePropTag ReportSimpleDisplayName = new StorePropTag(16439, PropertyType.Unicode, new StorePropInfo("ReportSimpleDisplayName", 16439, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012E0 RID: 4832
			public static readonly StorePropTag CreatorSimpleDisplayName = new StorePropTag(16440, PropertyType.Unicode, new StorePropInfo("CreatorSimpleDisplayName", 16440, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012E1 RID: 4833
			public static readonly StorePropTag LastModifierSimpleDisplayName = new StorePropTag(16441, PropertyType.Unicode, new StorePropInfo("LastModifierSimpleDisplayName", 16441, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 7, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012E2 RID: 4834
			public static readonly StorePropTag IncrSyncStateBegin = new StorePropTag(16442, PropertyType.Int32, new StorePropInfo("IncrSyncStateBegin", 16442, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012E3 RID: 4835
			public static readonly StorePropTag IncrSyncStateEnd = new StorePropTag(16443, PropertyType.Int32, new StorePropInfo("IncrSyncStateEnd", 16443, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012E4 RID: 4836
			public static readonly StorePropTag IncrSyncImailStream = new StorePropTag(16444, PropertyType.Int32, new StorePropInfo("IncrSyncImailStream", 16444, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012E5 RID: 4837
			public static readonly StorePropTag SenderOrgAddressType = new StorePropTag(16447, PropertyType.Unicode, new StorePropInfo("SenderOrgAddressType", 16447, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012E6 RID: 4838
			public static readonly StorePropTag SenderOrgEmailAddr = new StorePropTag(16448, PropertyType.Unicode, new StorePropInfo("SenderOrgEmailAddr", 16448, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012E7 RID: 4839
			public static readonly StorePropTag SentRepresentingOrgAddressType = new StorePropTag(16449, PropertyType.Unicode, new StorePropInfo("SentRepresentingOrgAddressType", 16449, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012E8 RID: 4840
			public static readonly StorePropTag SentRepresentingOrgEmailAddr = new StorePropTag(16450, PropertyType.Unicode, new StorePropInfo("SentRepresentingOrgEmailAddr", 16450, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012E9 RID: 4841
			public static readonly StorePropTag OriginalSenderOrgAddressType = new StorePropTag(16451, PropertyType.Unicode, new StorePropInfo("OriginalSenderOrgAddressType", 16451, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012EA RID: 4842
			public static readonly StorePropTag OriginalSenderOrgEmailAddr = new StorePropTag(16452, PropertyType.Unicode, new StorePropInfo("OriginalSenderOrgEmailAddr", 16452, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012EB RID: 4843
			public static readonly StorePropTag OriginalSentRepresentingOrgAddressType = new StorePropTag(16453, PropertyType.Unicode, new StorePropInfo("OriginalSentRepresentingOrgAddressType", 16453, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012EC RID: 4844
			public static readonly StorePropTag OriginalSentRepresentingOrgEmailAddr = new StorePropTag(16454, PropertyType.Unicode, new StorePropInfo("OriginalSentRepresentingOrgEmailAddr", 16454, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012ED RID: 4845
			public static readonly StorePropTag RcvdByOrgAddressType = new StorePropTag(16455, PropertyType.Unicode, new StorePropInfo("RcvdByOrgAddressType", 16455, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012EE RID: 4846
			public static readonly StorePropTag RcvdByOrgEmailAddr = new StorePropTag(16456, PropertyType.Unicode, new StorePropInfo("RcvdByOrgEmailAddr", 16456, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012EF RID: 4847
			public static readonly StorePropTag RcvdRepresentingOrgAddressType = new StorePropTag(16457, PropertyType.Unicode, new StorePropInfo("RcvdRepresentingOrgAddressType", 16457, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012F0 RID: 4848
			public static readonly StorePropTag RcvdRepresentingOrgEmailAddr = new StorePropTag(16458, PropertyType.Unicode, new StorePropInfo("RcvdRepresentingOrgEmailAddr", 16458, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012F1 RID: 4849
			public static readonly StorePropTag ReadReceiptOrgAddressType = new StorePropTag(16459, PropertyType.Unicode, new StorePropInfo("ReadReceiptOrgAddressType", 16459, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012F2 RID: 4850
			public static readonly StorePropTag ReadReceiptOrgEmailAddr = new StorePropTag(16460, PropertyType.Unicode, new StorePropInfo("ReadReceiptOrgEmailAddr", 16460, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012F3 RID: 4851
			public static readonly StorePropTag ReportOrgAddressType = new StorePropTag(16461, PropertyType.Unicode, new StorePropInfo("ReportOrgAddressType", 16461, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012F4 RID: 4852
			public static readonly StorePropTag ReportOrgEmailAddr = new StorePropTag(16462, PropertyType.Unicode, new StorePropInfo("ReportOrgEmailAddr", 16462, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012F5 RID: 4853
			public static readonly StorePropTag CreatorOrgAddressType = new StorePropTag(16463, PropertyType.Unicode, new StorePropInfo("CreatorOrgAddressType", 16463, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012F6 RID: 4854
			public static readonly StorePropTag CreatorOrgEmailAddr = new StorePropTag(16464, PropertyType.Unicode, new StorePropInfo("CreatorOrgEmailAddr", 16464, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012F7 RID: 4855
			public static readonly StorePropTag LastModifierOrgAddressType = new StorePropTag(16465, PropertyType.Unicode, new StorePropInfo("LastModifierOrgAddressType", 16465, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 7, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012F8 RID: 4856
			public static readonly StorePropTag LastModifierOrgEmailAddr = new StorePropTag(16466, PropertyType.Unicode, new StorePropInfo("LastModifierOrgEmailAddr", 16466, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 7, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012F9 RID: 4857
			public static readonly StorePropTag OriginatorOrgAddressType = new StorePropTag(16467, PropertyType.Unicode, new StorePropInfo("OriginatorOrgAddressType", 16467, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012FA RID: 4858
			public static readonly StorePropTag OriginatorOrgEmailAddr = new StorePropTag(16468, PropertyType.Unicode, new StorePropInfo("OriginatorOrgEmailAddr", 16468, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012FB RID: 4859
			public static readonly StorePropTag ReportDestinationOrgEmailType = new StorePropTag(16469, PropertyType.Unicode, new StorePropInfo("ReportDestinationOrgEmailType", 16469, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012FC RID: 4860
			public static readonly StorePropTag ReportDestinationOrgEmailAddr = new StorePropTag(16470, PropertyType.Unicode, new StorePropInfo("ReportDestinationOrgEmailAddr", 16470, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012FD RID: 4861
			public static readonly StorePropTag OriginalAuthorOrgAddressType = new StorePropTag(16471, PropertyType.Unicode, new StorePropInfo("OriginalAuthorOrgAddressType", 16471, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012FE RID: 4862
			public static readonly StorePropTag OriginalAuthorOrgEmailAddr = new StorePropTag(16472, PropertyType.Unicode, new StorePropInfo("OriginalAuthorOrgEmailAddr", 16472, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040012FF RID: 4863
			public static readonly StorePropTag CreatorFlags = new StorePropTag(16473, PropertyType.Int32, new StorePropInfo("CreatorFlags", 16473, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001300 RID: 4864
			public static readonly StorePropTag LastModifierFlags = new StorePropTag(16474, PropertyType.Int32, new StorePropInfo("LastModifierFlags", 16474, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 7, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001301 RID: 4865
			public static readonly StorePropTag OriginatorFlags = new StorePropTag(16475, PropertyType.Int32, new StorePropInfo("OriginatorFlags", 16475, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001302 RID: 4866
			public static readonly StorePropTag ReportDestinationFlags = new StorePropTag(16476, PropertyType.Int32, new StorePropInfo("ReportDestinationFlags", 16476, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001303 RID: 4867
			public static readonly StorePropTag OriginalAuthorFlags = new StorePropTag(16477, PropertyType.Int32, new StorePropInfo("OriginalAuthorFlags", 16477, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001304 RID: 4868
			public static readonly StorePropTag OriginatorSimpleDisplayName = new StorePropTag(16478, PropertyType.Unicode, new StorePropInfo("OriginatorSimpleDisplayName", 16478, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001305 RID: 4869
			public static readonly StorePropTag ReportDestinationSimpleDisplayName = new StorePropTag(16479, PropertyType.Unicode, new StorePropInfo("ReportDestinationSimpleDisplayName", 16479, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001306 RID: 4870
			public static readonly StorePropTag OriginalAuthorSimpleDispName = new StorePropTag(16480, PropertyType.Unicode, new StorePropInfo("OriginalAuthorSimpleDispName", 16480, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001307 RID: 4871
			public static readonly StorePropTag OriginatorSearchKey = new StorePropTag(16481, PropertyType.Binary, new StorePropInfo("OriginatorSearchKey", 16481, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001308 RID: 4872
			public static readonly StorePropTag ReportDestinationAddressType = new StorePropTag(16482, PropertyType.Unicode, new StorePropInfo("ReportDestinationAddressType", 16482, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001309 RID: 4873
			public static readonly StorePropTag ReportDestinationEmailAddress = new StorePropTag(16483, PropertyType.Unicode, new StorePropInfo("ReportDestinationEmailAddress", 16483, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400130A RID: 4874
			public static readonly StorePropTag ReportDestinationSearchKey = new StorePropTag(16484, PropertyType.Binary, new StorePropInfo("ReportDestinationSearchKey", 16484, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400130B RID: 4875
			public static readonly StorePropTag IncrSyncImailStreamContinue = new StorePropTag(16486, PropertyType.Int32, new StorePropInfo("IncrSyncImailStreamContinue", 16486, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400130C RID: 4876
			public static readonly StorePropTag IncrSyncImailStreamCancel = new StorePropTag(16487, PropertyType.Int32, new StorePropInfo("IncrSyncImailStreamCancel", 16487, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400130D RID: 4877
			public static readonly StorePropTag IncrSyncImailStream2Continue = new StorePropTag(16497, PropertyType.Int32, new StorePropInfo("IncrSyncImailStream2Continue", 16497, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400130E RID: 4878
			public static readonly StorePropTag IncrSyncProgressMode = new StorePropTag(16500, PropertyType.Boolean, new StorePropInfo("IncrSyncProgressMode", 16500, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400130F RID: 4879
			public static readonly StorePropTag SyncProgressPerMsg = new StorePropTag(16501, PropertyType.Boolean, new StorePropInfo("SyncProgressPerMsg", 16501, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001310 RID: 4880
			public static readonly StorePropTag ContentFilterSCL = new StorePropTag(16502, PropertyType.Int32, new StorePropInfo("ContentFilterSCL", 16502, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001311 RID: 4881
			public static readonly StorePropTag IncrSyncMsgPartial = new StorePropTag(16506, PropertyType.Int32, new StorePropInfo("IncrSyncMsgPartial", 16506, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001312 RID: 4882
			public static readonly StorePropTag IncrSyncGroupInfo = new StorePropTag(16507, PropertyType.Int32, new StorePropInfo("IncrSyncGroupInfo", 16507, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001313 RID: 4883
			public static readonly StorePropTag IncrSyncGroupId = new StorePropTag(16508, PropertyType.Int32, new StorePropInfo("IncrSyncGroupId", 16508, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001314 RID: 4884
			public static readonly StorePropTag IncrSyncChangePartial = new StorePropTag(16509, PropertyType.Int32, new StorePropInfo("IncrSyncChangePartial", 16509, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001315 RID: 4885
			public static readonly StorePropTag ContentFilterPCL = new StorePropTag(16516, PropertyType.Int32, new StorePropInfo("ContentFilterPCL", 16516, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001316 RID: 4886
			public static readonly StorePropTag DeliverAsRead = new StorePropTag(22534, PropertyType.Boolean, new StorePropInfo("DeliverAsRead", 22534, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001317 RID: 4887
			public static readonly StorePropTag InetMailOverrideFormat = new StorePropTag(22786, PropertyType.Int32, new StorePropInfo("InetMailOverrideFormat", 22786, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001318 RID: 4888
			public static readonly StorePropTag MessageEditorFormat = new StorePropTag(22793, PropertyType.Int32, new StorePropInfo("MessageEditorFormat", 22793, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001319 RID: 4889
			public static readonly StorePropTag SenderSMTPAddressXSO = new StorePropTag(23809, PropertyType.Unicode, new StorePropInfo("SenderSMTPAddressXSO", 23809, PropertyType.Unicode, StorePropInfo.Flags.None, 11565243843087433728UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400131A RID: 4890
			public static readonly StorePropTag SentRepresentingSMTPAddressXSO = new StorePropTag(23810, PropertyType.Unicode, new StorePropInfo("SentRepresentingSMTPAddressXSO", 23810, PropertyType.Unicode, StorePropInfo.Flags.None, 11529215046068469760UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400131B RID: 4891
			public static readonly StorePropTag OriginalSenderSMTPAddressXSO = new StorePropTag(23811, PropertyType.Unicode, new StorePropInfo("OriginalSenderSMTPAddressXSO", 23811, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400131C RID: 4892
			public static readonly StorePropTag OriginalSentRepresentingSMTPAddressXSO = new StorePropTag(23812, PropertyType.Unicode, new StorePropInfo("OriginalSentRepresentingSMTPAddressXSO", 23812, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400131D RID: 4893
			public static readonly StorePropTag ReadReceiptSMTPAddressXSO = new StorePropTag(23813, PropertyType.Unicode, new StorePropInfo("ReadReceiptSMTPAddressXSO", 23813, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400131E RID: 4894
			public static readonly StorePropTag OriginalAuthorSMTPAddressXSO = new StorePropTag(23814, PropertyType.Unicode, new StorePropInfo("OriginalAuthorSMTPAddressXSO", 23814, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400131F RID: 4895
			public static readonly StorePropTag ReceivedBySMTPAddressXSO = new StorePropTag(23815, PropertyType.Unicode, new StorePropInfo("ReceivedBySMTPAddressXSO", 23815, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001320 RID: 4896
			public static readonly StorePropTag ReceivedRepresentingSMTPAddressXSO = new StorePropTag(23816, PropertyType.Unicode, new StorePropInfo("ReceivedRepresentingSMTPAddressXSO", 23816, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001321 RID: 4897
			public static readonly StorePropTag RecipientOrder = new StorePropTag(24543, PropertyType.Int32, new StorePropInfo("RecipientOrder", 24543, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001322 RID: 4898
			public static readonly StorePropTag RecipientSipUri = new StorePropTag(24549, PropertyType.Unicode, new StorePropInfo("RecipientSipUri", 24549, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001323 RID: 4899
			public static readonly StorePropTag RecipientDisplayName = new StorePropTag(24566, PropertyType.Unicode, new StorePropInfo("RecipientDisplayName", 24566, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001324 RID: 4900
			public static readonly StorePropTag RecipientEntryId = new StorePropTag(24567, PropertyType.Binary, new StorePropInfo("RecipientEntryId", 24567, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001325 RID: 4901
			public static readonly StorePropTag RecipientFlags = new StorePropTag(24573, PropertyType.Int32, new StorePropInfo("RecipientFlags", 24573, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001326 RID: 4902
			public static readonly StorePropTag RecipientTrackStatus = new StorePropTag(24575, PropertyType.Int32, new StorePropInfo("RecipientTrackStatus", 24575, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001327 RID: 4903
			public static readonly StorePropTag DotStuffState = new StorePropTag(24577, PropertyType.Unicode, new StorePropInfo("DotStuffState", 24577, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001328 RID: 4904
			public static readonly StorePropTag InternetMessageIdHash = new StorePropTag(25088, PropertyType.Int32, new StorePropInfo("InternetMessageIdHash", 25088, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001329 RID: 4905
			public static readonly StorePropTag ConversationTopicHash = new StorePropTag(25089, PropertyType.Int32, new StorePropInfo("ConversationTopicHash", 25089, PropertyType.Int32, StorePropInfo.Flags.None, 2341871806232658048UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400132A RID: 4906
			public static readonly StorePropTag MimeSkeleton = new StorePropTag(25840, PropertyType.Binary, new StorePropInfo("MimeSkeleton", 25840, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400132B RID: 4907
			public static readonly StorePropTag ReplyTemplateId = new StorePropTag(26050, PropertyType.Binary, new StorePropInfo("ReplyTemplateId", 26050, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400132C RID: 4908
			public static readonly StorePropTag SecureSubmitFlags = new StorePropTag(26054, PropertyType.Int32, new StorePropInfo("SecureSubmitFlags", 26054, PropertyType.Int32, StorePropInfo.Flags.None, 1UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400132D RID: 4909
			public static readonly StorePropTag SourceKey = new StorePropTag(26080, PropertyType.Binary, new StorePropInfo("SourceKey", 26080, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(2, 3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400132E RID: 4910
			public static readonly StorePropTag ParentSourceKey = new StorePropTag(26081, PropertyType.Binary, new StorePropInfo("ParentSourceKey", 26081, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400132F RID: 4911
			public static readonly StorePropTag ChangeKey = new StorePropTag(26082, PropertyType.Binary, new StorePropInfo("ChangeKey", 26082, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, new PropertyCategories(2, 3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001330 RID: 4912
			public static readonly StorePropTag PredecessorChangeList = new StorePropTag(26083, PropertyType.Binary, new StorePropInfo("PredecessorChangeList", 26083, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, new PropertyCategories(2, 3, 4, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001331 RID: 4913
			public static readonly StorePropTag RuleMsgState = new StorePropTag(26089, PropertyType.Int32, new StorePropInfo("RuleMsgState", 26089, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001332 RID: 4914
			public static readonly StorePropTag RuleMsgUserFlags = new StorePropTag(26090, PropertyType.Int32, new StorePropInfo("RuleMsgUserFlags", 26090, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001333 RID: 4915
			public static readonly StorePropTag RuleMsgProvider = new StorePropTag(26091, PropertyType.Unicode, new StorePropInfo("RuleMsgProvider", 26091, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001334 RID: 4916
			public static readonly StorePropTag RuleMsgName = new StorePropTag(26092, PropertyType.Unicode, new StorePropInfo("RuleMsgName", 26092, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001335 RID: 4917
			public static readonly StorePropTag RuleMsgLevel = new StorePropTag(26093, PropertyType.Int32, new StorePropInfo("RuleMsgLevel", 26093, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001336 RID: 4918
			public static readonly StorePropTag RuleMsgProviderData = new StorePropTag(26094, PropertyType.Binary, new StorePropInfo("RuleMsgProviderData", 26094, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001337 RID: 4919
			public static readonly StorePropTag RuleMsgActions = new StorePropTag(26095, PropertyType.Binary, new StorePropInfo("RuleMsgActions", 26095, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001338 RID: 4920
			public static readonly StorePropTag RuleMsgCondition = new StorePropTag(26096, PropertyType.Binary, new StorePropInfo("RuleMsgCondition", 26096, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001339 RID: 4921
			public static readonly StorePropTag RuleMsgConditionLCID = new StorePropTag(26097, PropertyType.Int32, new StorePropInfo("RuleMsgConditionLCID", 26097, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400133A RID: 4922
			public static readonly StorePropTag RuleMsgVersion = new StorePropTag(26098, PropertyType.Int16, new StorePropInfo("RuleMsgVersion", 26098, PropertyType.Int16, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400133B RID: 4923
			public static readonly StorePropTag RuleMsgSequence = new StorePropTag(26099, PropertyType.Int32, new StorePropInfo("RuleMsgSequence", 26099, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400133C RID: 4924
			public static readonly StorePropTag LISSD = new StorePropTag(26105, PropertyType.Binary, new StorePropInfo("LISSD", 26105, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400133D RID: 4925
			public static readonly StorePropTag ReplicaServer = new StorePropTag(26180, PropertyType.Unicode, new StorePropInfo("ReplicaServer", 26180, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400133E RID: 4926
			public static readonly StorePropTag DAMOriginalEntryId = new StorePropTag(26182, PropertyType.Binary, new StorePropInfo("DAMOriginalEntryId", 26182, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400133F RID: 4927
			public static readonly StorePropTag HasNamedProperties = new StorePropTag(26186, PropertyType.Boolean, new StorePropInfo("HasNamedProperties", 26186, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 10, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001340 RID: 4928
			public static readonly StorePropTag FidMid = new StorePropTag(26188, PropertyType.Binary, new StorePropInfo("FidMid", 26188, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001341 RID: 4929
			public static readonly StorePropTag InternetContent = new StorePropTag(26201, PropertyType.Binary, new StorePropInfo("InternetContent", 26201, PropertyType.Binary, StorePropInfo.Flags.None, 1152921504606848000UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001342 RID: 4930
			public static readonly StorePropTag OriginatorName = new StorePropTag(26203, PropertyType.Unicode, new StorePropInfo("OriginatorName", 26203, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001343 RID: 4931
			public static readonly StorePropTag OriginatorEmailAddress = new StorePropTag(26204, PropertyType.Unicode, new StorePropInfo("OriginatorEmailAddress", 26204, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001344 RID: 4932
			public static readonly StorePropTag OriginatorAddressType = new StorePropTag(26205, PropertyType.Unicode, new StorePropInfo("OriginatorAddressType", 26205, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001345 RID: 4933
			public static readonly StorePropTag OriginatorEntryId = new StorePropTag(26206, PropertyType.Binary, new StorePropInfo("OriginatorEntryId", 26206, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001346 RID: 4934
			public static readonly StorePropTag RecipientNumber = new StorePropTag(26210, PropertyType.Int32, new StorePropInfo("RecipientNumber", 26210, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001347 RID: 4935
			public static readonly StorePropTag ReportDestinationName = new StorePropTag(26212, PropertyType.Unicode, new StorePropInfo("ReportDestinationName", 26212, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001348 RID: 4936
			public static readonly StorePropTag ReportDestinationEntryId = new StorePropTag(26213, PropertyType.Binary, new StorePropInfo("ReportDestinationEntryId", 26213, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001349 RID: 4937
			public static readonly StorePropTag ProhibitReceiveQuota = new StorePropTag(26218, PropertyType.Int32, new StorePropInfo("ProhibitReceiveQuota", 26218, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400134A RID: 4938
			public static readonly StorePropTag SearchAttachments = new StorePropTag(26221, PropertyType.Unicode, new StorePropInfo("SearchAttachments", 26221, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400134B RID: 4939
			public static readonly StorePropTag ProhibitSendQuota = new StorePropTag(26222, PropertyType.Int32, new StorePropInfo("ProhibitSendQuota", 26222, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400134C RID: 4940
			public static readonly StorePropTag SubmittedByAdmin = new StorePropTag(26223, PropertyType.Boolean, new StorePropInfo("SubmittedByAdmin", 26223, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 4, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400134D RID: 4941
			public static readonly StorePropTag LongTermEntryIdFromTable = new StorePropTag(26224, PropertyType.Binary, new StorePropInfo("LongTermEntryIdFromTable", 26224, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400134E RID: 4942
			public static readonly StorePropTag RuleIds = new StorePropTag(26229, PropertyType.Binary, new StorePropInfo("RuleIds", 26229, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400134F RID: 4943
			public static readonly StorePropTag RuleMsgConditionOld = new StorePropTag(26233, PropertyType.Binary, new StorePropInfo("RuleMsgConditionOld", 26233, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001350 RID: 4944
			public static readonly StorePropTag RuleMsgActionsOld = new StorePropTag(26240, PropertyType.Binary, new StorePropInfo("RuleMsgActionsOld", 26240, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001351 RID: 4945
			public static readonly StorePropTag DeletedOn = new StorePropTag(26255, PropertyType.SysTime, new StorePropInfo("DeletedOn", 26255, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				10,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001352 RID: 4946
			public static readonly StorePropTag CodePageId = new StorePropTag(26307, PropertyType.Int32, new StorePropInfo("CodePageId", 26307, PropertyType.Int32, StorePropInfo.Flags.None, 1UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001353 RID: 4947
			public static readonly StorePropTag UserDN = new StorePropTag(26314, PropertyType.Unicode, new StorePropInfo("UserDN", 26314, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001354 RID: 4948
			public static readonly StorePropTag MailboxDSGuidGuid = new StorePropTag(26375, PropertyType.Guid, new StorePropInfo("MailboxDSGuidGuid", 26375, PropertyType.Guid, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001355 RID: 4949
			public static readonly StorePropTag URLName = new StorePropTag(26375, PropertyType.Unicode, new StorePropInfo("URLName", 26375, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001356 RID: 4950
			public static readonly StorePropTag LocalCommitTime = new StorePropTag(26377, PropertyType.SysTime, new StorePropInfo("LocalCommitTime", 26377, PropertyType.SysTime, StorePropInfo.Flags.None, 1UL, new PropertyCategories(3, 9, 10, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001357 RID: 4951
			public static readonly StorePropTag AutoReset = new StorePropTag(26380, PropertyType.MVGuid, new StorePropInfo("AutoReset", 26380, PropertyType.MVGuid, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001358 RID: 4952
			public static readonly StorePropTag ELCAutoCopyTag = new StorePropTag(26390, PropertyType.Binary, new StorePropInfo("ELCAutoCopyTag", 26390, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001359 RID: 4953
			public static readonly StorePropTag ELCMoveDate = new StorePropTag(26391, PropertyType.Binary, new StorePropInfo("ELCMoveDate", 26391, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400135A RID: 4954
			public static readonly StorePropTag PropGroupInfo = new StorePropTag(26430, PropertyType.Binary, new StorePropInfo("PropGroupInfo", 26430, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400135B RID: 4955
			public static readonly StorePropTag PropertyGroupChangeMask = new StorePropTag(26430, PropertyType.Int32, new StorePropInfo("PropertyGroupChangeMask", 26430, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400135C RID: 4956
			public static readonly StorePropTag ReadCnNewExport = new StorePropTag(26431, PropertyType.Binary, new StorePropInfo("ReadCnNewExport", 26431, PropertyType.Binary, StorePropInfo.Flags.None, 2UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				4,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400135D RID: 4957
			public static readonly StorePropTag SentMailSvrEID = new StorePropTag(26432, PropertyType.SvrEid, new StorePropInfo("SentMailSvrEID", 26432, PropertyType.SvrEid, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400135E RID: 4958
			public static readonly StorePropTag SentMailSvrEIDBin = new StorePropTag(26432, PropertyType.Binary, new StorePropInfo("SentMailSvrEIDBin", 26432, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400135F RID: 4959
			public static readonly StorePropTag LocallyDelivered = new StorePropTag(26437, PropertyType.Binary, new StorePropInfo("LocallyDelivered", 26437, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001360 RID: 4960
			public static readonly StorePropTag MimeSize = new StorePropTag(26438, PropertyType.Int64, new StorePropInfo("MimeSize", 26438, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001361 RID: 4961
			public static readonly StorePropTag MimeSize32 = new StorePropTag(26438, PropertyType.Int32, new StorePropInfo("MimeSize32", 26438, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001362 RID: 4962
			public static readonly StorePropTag FileSize = new StorePropTag(26439, PropertyType.Int64, new StorePropInfo("FileSize", 26439, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001363 RID: 4963
			public static readonly StorePropTag FileSize32 = new StorePropTag(26439, PropertyType.Int32, new StorePropInfo("FileSize32", 26439, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001364 RID: 4964
			public static readonly StorePropTag Fid = new StorePropTag(26440, PropertyType.Int64, new StorePropInfo("Fid", 26440, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001365 RID: 4965
			public static readonly StorePropTag FidBin = new StorePropTag(26440, PropertyType.Binary, new StorePropInfo("FidBin", 26440, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001366 RID: 4966
			public static readonly StorePropTag ParentFid = new StorePropTag(26441, PropertyType.Int64, new StorePropInfo("ParentFid", 26441, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001367 RID: 4967
			public static readonly StorePropTag Mid = new StorePropTag(26442, PropertyType.Int64, new StorePropInfo("Mid", 26442, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001368 RID: 4968
			public static readonly StorePropTag MidBin = new StorePropTag(26442, PropertyType.Binary, new StorePropInfo("MidBin", 26442, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001369 RID: 4969
			public static readonly StorePropTag CategID = new StorePropTag(26443, PropertyType.Int64, new StorePropInfo("CategID", 26443, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 10, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400136A RID: 4970
			public static readonly StorePropTag ParentCategID = new StorePropTag(26444, PropertyType.Int64, new StorePropInfo("ParentCategID", 26444, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 10, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400136B RID: 4971
			public static readonly StorePropTag InstanceId = new StorePropTag(26445, PropertyType.Int64, new StorePropInfo("InstanceId", 26445, PropertyType.Int64, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				10,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400136C RID: 4972
			public static readonly StorePropTag InstanceNum = new StorePropTag(26446, PropertyType.Int32, new StorePropInfo("InstanceNum", 26446, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				10,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400136D RID: 4973
			public static readonly StorePropTag ChangeType = new StorePropTag(26448, PropertyType.Int16, new StorePropInfo("ChangeType", 26448, PropertyType.Int16, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 10, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400136E RID: 4974
			public static readonly StorePropTag RequiresRefResolve = new StorePropTag(26449, PropertyType.Boolean, new StorePropInfo("RequiresRefResolve", 26449, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400136F RID: 4975
			public static readonly StorePropTag LTID = new StorePropTag(26456, PropertyType.Binary, new StorePropInfo("LTID", 26456, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9, 10, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001370 RID: 4976
			public static readonly StorePropTag CnExport = new StorePropTag(26457, PropertyType.Binary, new StorePropInfo("CnExport", 26457, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				4,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001371 RID: 4977
			public static readonly StorePropTag PclExport = new StorePropTag(26458, PropertyType.Binary, new StorePropInfo("PclExport", 26458, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				4,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001372 RID: 4978
			public static readonly StorePropTag CnMvExport = new StorePropTag(26459, PropertyType.Binary, new StorePropInfo("CnMvExport", 26459, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, new PropertyCategories(1, 3, 4, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001373 RID: 4979
			public static readonly StorePropTag MailboxGuid = new StorePropTag(26476, PropertyType.Binary, new StorePropInfo("MailboxGuid", 26476, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001374 RID: 4980
			public static readonly StorePropTag MapiEntryIdGuidGuid = new StorePropTag(26476, PropertyType.Guid, new StorePropInfo("MapiEntryIdGuidGuid", 26476, PropertyType.Guid, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001375 RID: 4981
			public static readonly StorePropTag ImapCachedBodystructure = new StorePropTag(26477, PropertyType.Binary, new StorePropInfo("ImapCachedBodystructure", 26477, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001376 RID: 4982
			public static readonly StorePropTag StorageQuota = new StorePropTag(26491, PropertyType.Int32, new StorePropInfo("StorageQuota", 26491, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001377 RID: 4983
			public static readonly StorePropTag CnsetIn = new StorePropTag(26516, PropertyType.Binary, new StorePropInfo("CnsetIn", 26516, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				10,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001378 RID: 4984
			public static readonly StorePropTag CnsetSeen = new StorePropTag(26518, PropertyType.Binary, new StorePropInfo("CnsetSeen", 26518, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001379 RID: 4985
			public static readonly StorePropTag ChangeNumber = new StorePropTag(26532, PropertyType.Int64, new StorePropInfo("ChangeNumber", 26532, PropertyType.Int64, StorePropInfo.Flags.None, 1UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				4,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400137A RID: 4986
			public static readonly StorePropTag ChangeNumberBin = new StorePropTag(26532, PropertyType.Binary, new StorePropInfo("ChangeNumberBin", 26532, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400137B RID: 4987
			public static readonly StorePropTag PCL = new StorePropTag(26533, PropertyType.Binary, new StorePropInfo("PCL", 26533, PropertyType.Binary, StorePropInfo.Flags.None, 1UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				4,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400137C RID: 4988
			public static readonly StorePropTag CnMv = new StorePropTag(26534, PropertyType.MVInt64, new StorePropInfo("CnMv", 26534, PropertyType.MVInt64, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				4,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400137D RID: 4989
			public static readonly StorePropTag SourceEntryId = new StorePropTag(26536, PropertyType.Binary, new StorePropInfo("SourceEntryId", 26536, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400137E RID: 4990
			public static readonly StorePropTag MailFlags = new StorePropTag(26537, PropertyType.Int16, new StorePropInfo("MailFlags", 26537, PropertyType.Int16, StorePropInfo.Flags.None, 2UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				4,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400137F RID: 4991
			public static readonly StorePropTag Associated = new StorePropTag(26538, PropertyType.Boolean, new StorePropInfo("Associated", 26538, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001380 RID: 4992
			public static readonly StorePropTag SubmitResponsibility = new StorePropTag(26539, PropertyType.Int32, new StorePropInfo("SubmitResponsibility", 26539, PropertyType.Int32, StorePropInfo.Flags.None, 2UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001381 RID: 4993
			public static readonly StorePropTag SharedReceiptHandling = new StorePropTag(26541, PropertyType.Boolean, new StorePropInfo("SharedReceiptHandling", 26541, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001382 RID: 4994
			public static readonly StorePropTag Inid = new StorePropTag(26544, PropertyType.Binary, new StorePropInfo("Inid", 26544, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001383 RID: 4995
			public static readonly StorePropTag MessageAttachList = new StorePropTag(26547, PropertyType.Binary, new StorePropInfo("MessageAttachList", 26547, PropertyType.Binary, StorePropInfo.Flags.None, 32UL, new PropertyCategories(1, 2, 3, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001384 RID: 4996
			public static readonly StorePropTag SenderCAI = new StorePropTag(26549, PropertyType.Binary, new StorePropInfo("SenderCAI", 26549, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				13,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001385 RID: 4997
			public static readonly StorePropTag SentRepresentingCAI = new StorePropTag(26550, PropertyType.Binary, new StorePropInfo("SentRepresentingCAI", 26550, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				13,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001386 RID: 4998
			public static readonly StorePropTag OriginalSenderCAI = new StorePropTag(26551, PropertyType.Binary, new StorePropInfo("OriginalSenderCAI", 26551, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				13,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001387 RID: 4999
			public static readonly StorePropTag OriginalSentRepresentingCAI = new StorePropTag(26552, PropertyType.Binary, new StorePropInfo("OriginalSentRepresentingCAI", 26552, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				13,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001388 RID: 5000
			public static readonly StorePropTag ReceivedByCAI = new StorePropTag(26553, PropertyType.Binary, new StorePropInfo("ReceivedByCAI", 26553, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				13,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001389 RID: 5001
			public static readonly StorePropTag ReceivedRepresentingCAI = new StorePropTag(26554, PropertyType.Binary, new StorePropInfo("ReceivedRepresentingCAI", 26554, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				13,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400138A RID: 5002
			public static readonly StorePropTag ReadReceiptCAI = new StorePropTag(26555, PropertyType.Binary, new StorePropInfo("ReadReceiptCAI", 26555, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				13,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400138B RID: 5003
			public static readonly StorePropTag ReportCAI = new StorePropTag(26556, PropertyType.Binary, new StorePropInfo("ReportCAI", 26556, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				13,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400138C RID: 5004
			public static readonly StorePropTag CreatorCAI = new StorePropTag(26557, PropertyType.Binary, new StorePropInfo("CreatorCAI", 26557, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				13,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400138D RID: 5005
			public static readonly StorePropTag LastModifierCAI = new StorePropTag(26558, PropertyType.Binary, new StorePropInfo("LastModifierCAI", 26558, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				13,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400138E RID: 5006
			public static readonly StorePropTag CnsetRead = new StorePropTag(26578, PropertyType.Binary, new StorePropInfo("CnsetRead", 26578, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400138F RID: 5007
			public static readonly StorePropTag CnsetSeenFAI = new StorePropTag(26586, PropertyType.Binary, new StorePropInfo("CnsetSeenFAI", 26586, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001390 RID: 5008
			public static readonly StorePropTag IdSetDeleted = new StorePropTag(26597, PropertyType.Binary, new StorePropInfo("IdSetDeleted", 26597, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001391 RID: 5009
			public static readonly StorePropTag OriginatorCAI = new StorePropTag(26616, PropertyType.Binary, new StorePropInfo("OriginatorCAI", 26616, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				13,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001392 RID: 5010
			public static readonly StorePropTag ReportDestinationCAI = new StorePropTag(26617, PropertyType.Binary, new StorePropInfo("ReportDestinationCAI", 26617, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				13,
				14,
				18
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001393 RID: 5011
			public static readonly StorePropTag OriginalAuthorCAI = new StorePropTag(26618, PropertyType.Binary, new StorePropInfo("OriginalAuthorCAI", 26618, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				9,
				13,
				14,
				18
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001394 RID: 5012
			public static readonly StorePropTag ReadCnNew = new StorePropTag(26622, PropertyType.Int64, new StorePropInfo("ReadCnNew", 26622, PropertyType.Int64, StorePropInfo.Flags.None, 2UL, new PropertyCategories(new int[]
			{
				1,
				2,
				3,
				4,
				9,
				14
			})), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001395 RID: 5013
			public static readonly StorePropTag ReadCnNewBin = new StorePropTag(26622, PropertyType.Binary, new StorePropInfo("ReadCnNewBin", 26622, PropertyType.Binary, StorePropInfo.Flags.None, 2UL, new PropertyCategories(1, 2, 3, 9, 14)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001396 RID: 5014
			public static readonly StorePropTag SenderTelephoneNumber = new StorePropTag(26626, PropertyType.Unicode, new StorePropInfo("SenderTelephoneNumber", 26626, PropertyType.Unicode, StorePropInfo.Flags.None, 128UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001397 RID: 5015
			public static readonly StorePropTag VoiceMessageAttachmentOrder = new StorePropTag(26629, PropertyType.Unicode, new StorePropInfo("VoiceMessageAttachmentOrder", 26629, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001398 RID: 5016
			public static readonly StorePropTag DocumentId = new StorePropTag(26645, PropertyType.Int32, new StorePropInfo("DocumentId", 26645, PropertyType.Int32, StorePropInfo.Flags.None, 2341871806232658048UL, new PropertyCategories(1, 2, 3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001399 RID: 5017
			public static readonly StorePropTag MailboxNum = new StorePropTag(26655, PropertyType.Int32, new StorePropInfo("MailboxNum", 26655, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400139A RID: 5018
			public static readonly StorePropTag ConversationIdHash = new StorePropTag(26663, PropertyType.Int32, new StorePropInfo("ConversationIdHash", 26663, PropertyType.Int32, StorePropInfo.Flags.None, 2341871806232658048UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400139B RID: 5019
			public static readonly StorePropTag ConversationDocumentId = new StorePropTag(26662, PropertyType.Int32, new StorePropInfo("ConversationDocumentId", 26662, PropertyType.Int32, StorePropInfo.Flags.None, 1UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400139C RID: 5020
			public static readonly StorePropTag LocalDirectoryBlob = new StorePropTag(26664, PropertyType.Binary, new StorePropInfo("LocalDirectoryBlob", 26664, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400139D RID: 5021
			public static readonly StorePropTag ViewStyle = new StorePropTag(26676, PropertyType.Int32, new StorePropInfo("ViewStyle", 26676, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400139E RID: 5022
			public static readonly StorePropTag FreebusyEMA = new StorePropTag(26697, PropertyType.Unicode, new StorePropInfo("FreebusyEMA", 26697, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x0400139F RID: 5023
			public static readonly StorePropTag WunderbarLinkEntryID = new StorePropTag(26700, PropertyType.Binary, new StorePropInfo("WunderbarLinkEntryID", 26700, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013A0 RID: 5024
			public static readonly StorePropTag WunderbarLinkStoreEntryId = new StorePropTag(26702, PropertyType.Binary, new StorePropInfo("WunderbarLinkStoreEntryId", 26702, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013A1 RID: 5025
			public static readonly StorePropTag SchdInfoFreebusyMerged = new StorePropTag(26704, PropertyType.MVBinary, new StorePropInfo("SchdInfoFreebusyMerged", 26704, PropertyType.MVBinary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013A2 RID: 5026
			public static readonly StorePropTag WunderbarLinkGroupClsId = new StorePropTag(26704, PropertyType.Binary, new StorePropInfo("WunderbarLinkGroupClsId", 26704, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013A3 RID: 5027
			public static readonly StorePropTag WunderbarLinkGroupName = new StorePropTag(26705, PropertyType.Unicode, new StorePropInfo("WunderbarLinkGroupName", 26705, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013A4 RID: 5028
			public static readonly StorePropTag WunderbarLinkSection = new StorePropTag(26706, PropertyType.Int32, new StorePropInfo("WunderbarLinkSection", 26706, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013A5 RID: 5029
			public static readonly StorePropTag NavigationNodeCalendarColor = new StorePropTag(26707, PropertyType.Int32, new StorePropInfo("NavigationNodeCalendarColor", 26707, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013A6 RID: 5030
			public static readonly StorePropTag NavigationNodeAddressbookEntryId = new StorePropTag(26708, PropertyType.Binary, new StorePropInfo("NavigationNodeAddressbookEntryId", 26708, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013A7 RID: 5031
			public static readonly StorePropTag AgingDeleteItems = new StorePropTag(26709, PropertyType.Int32, new StorePropInfo("AgingDeleteItems", 26709, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013A8 RID: 5032
			public static readonly StorePropTag AgingFileName9AndPrev = new StorePropTag(26710, PropertyType.Unicode, new StorePropInfo("AgingFileName9AndPrev", 26710, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013A9 RID: 5033
			public static readonly StorePropTag AgingAgeFolder = new StorePropTag(26711, PropertyType.Boolean, new StorePropInfo("AgingAgeFolder", 26711, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013AA RID: 5034
			public static readonly StorePropTag AgingDontAgeMe = new StorePropTag(26712, PropertyType.Boolean, new StorePropInfo("AgingDontAgeMe", 26712, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013AB RID: 5035
			public static readonly StorePropTag AgingFileNameAfter9 = new StorePropTag(26713, PropertyType.Unicode, new StorePropInfo("AgingFileNameAfter9", 26713, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013AC RID: 5036
			public static readonly StorePropTag AgingWhenDeletedOnServer = new StorePropTag(26715, PropertyType.Boolean, new StorePropInfo("AgingWhenDeletedOnServer", 26715, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013AD RID: 5037
			public static readonly StorePropTag AgingWaitUntilExpired = new StorePropTag(26716, PropertyType.Boolean, new StorePropInfo("AgingWaitUntilExpired", 26716, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013AE RID: 5038
			public static readonly StorePropTag ConversationMvFrom = new StorePropTag(26752, PropertyType.MVUnicode, new StorePropInfo("ConversationMvFrom", 26752, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013AF RID: 5039
			public static readonly StorePropTag ConversationMvFromMailboxWide = new StorePropTag(26753, PropertyType.MVUnicode, new StorePropInfo("ConversationMvFromMailboxWide", 26753, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013B0 RID: 5040
			public static readonly StorePropTag ConversationMvTo = new StorePropTag(26754, PropertyType.MVUnicode, new StorePropInfo("ConversationMvTo", 26754, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013B1 RID: 5041
			public static readonly StorePropTag ConversationMvToMailboxWide = new StorePropTag(26755, PropertyType.MVUnicode, new StorePropInfo("ConversationMvToMailboxWide", 26755, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013B2 RID: 5042
			public static readonly StorePropTag ConversationMessageDeliveryTime = new StorePropTag(26756, PropertyType.SysTime, new StorePropInfo("ConversationMessageDeliveryTime", 26756, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013B3 RID: 5043
			public static readonly StorePropTag ConversationMessageDeliveryTimeMailboxWide = new StorePropTag(26757, PropertyType.SysTime, new StorePropInfo("ConversationMessageDeliveryTimeMailboxWide", 26757, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013B4 RID: 5044
			public static readonly StorePropTag ConversationCategories = new StorePropTag(26758, PropertyType.MVUnicode, new StorePropInfo("ConversationCategories", 26758, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013B5 RID: 5045
			public static readonly StorePropTag ConversationCategoriesMailboxWide = new StorePropTag(26759, PropertyType.MVUnicode, new StorePropInfo("ConversationCategoriesMailboxWide", 26759, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013B6 RID: 5046
			public static readonly StorePropTag ConversationFlagStatus = new StorePropTag(26760, PropertyType.Int32, new StorePropInfo("ConversationFlagStatus", 26760, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013B7 RID: 5047
			public static readonly StorePropTag ConversationFlagStatusMailboxWide = new StorePropTag(26761, PropertyType.Int32, new StorePropInfo("ConversationFlagStatusMailboxWide", 26761, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013B8 RID: 5048
			public static readonly StorePropTag ConversationFlagCompleteTime = new StorePropTag(26762, PropertyType.SysTime, new StorePropInfo("ConversationFlagCompleteTime", 26762, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013B9 RID: 5049
			public static readonly StorePropTag ConversationFlagCompleteTimeMailboxWide = new StorePropTag(26763, PropertyType.SysTime, new StorePropInfo("ConversationFlagCompleteTimeMailboxWide", 26763, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013BA RID: 5050
			public static readonly StorePropTag ConversationHasAttach = new StorePropTag(26764, PropertyType.Boolean, new StorePropInfo("ConversationHasAttach", 26764, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013BB RID: 5051
			public static readonly StorePropTag ConversationHasAttachMailboxWide = new StorePropTag(26765, PropertyType.Boolean, new StorePropInfo("ConversationHasAttachMailboxWide", 26765, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013BC RID: 5052
			public static readonly StorePropTag ConversationContentCount = new StorePropTag(26766, PropertyType.Int32, new StorePropInfo("ConversationContentCount", 26766, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013BD RID: 5053
			public static readonly StorePropTag ConversationContentCountMailboxWide = new StorePropTag(26767, PropertyType.Int32, new StorePropInfo("ConversationContentCountMailboxWide", 26767, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013BE RID: 5054
			public static readonly StorePropTag ConversationContentUnread = new StorePropTag(26768, PropertyType.Int32, new StorePropInfo("ConversationContentUnread", 26768, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013BF RID: 5055
			public static readonly StorePropTag ConversationContentUnreadMailboxWide = new StorePropTag(26769, PropertyType.Int32, new StorePropInfo("ConversationContentUnreadMailboxWide", 26769, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013C0 RID: 5056
			public static readonly StorePropTag ConversationMessageSize = new StorePropTag(26770, PropertyType.Int32, new StorePropInfo("ConversationMessageSize", 26770, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013C1 RID: 5057
			public static readonly StorePropTag ConversationMessageSizeMailboxWide = new StorePropTag(26771, PropertyType.Int32, new StorePropInfo("ConversationMessageSizeMailboxWide", 26771, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013C2 RID: 5058
			public static readonly StorePropTag ConversationMessageClasses = new StorePropTag(26772, PropertyType.MVUnicode, new StorePropInfo("ConversationMessageClasses", 26772, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013C3 RID: 5059
			public static readonly StorePropTag ConversationMessageClassesMailboxWide = new StorePropTag(26773, PropertyType.MVUnicode, new StorePropInfo("ConversationMessageClassesMailboxWide", 26773, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013C4 RID: 5060
			public static readonly StorePropTag ConversationReplyForwardState = new StorePropTag(26774, PropertyType.Int32, new StorePropInfo("ConversationReplyForwardState", 26774, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013C5 RID: 5061
			public static readonly StorePropTag ConversationReplyForwardStateMailboxWide = new StorePropTag(26775, PropertyType.Int32, new StorePropInfo("ConversationReplyForwardStateMailboxWide", 26775, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013C6 RID: 5062
			public static readonly StorePropTag ConversationImportance = new StorePropTag(26776, PropertyType.Int32, new StorePropInfo("ConversationImportance", 26776, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013C7 RID: 5063
			public static readonly StorePropTag ConversationImportanceMailboxWide = new StorePropTag(26777, PropertyType.Int32, new StorePropInfo("ConversationImportanceMailboxWide", 26777, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013C8 RID: 5064
			public static readonly StorePropTag ConversationMvFromUnread = new StorePropTag(26778, PropertyType.MVUnicode, new StorePropInfo("ConversationMvFromUnread", 26778, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013C9 RID: 5065
			public static readonly StorePropTag ConversationMvFromUnreadMailboxWide = new StorePropTag(26779, PropertyType.MVUnicode, new StorePropInfo("ConversationMvFromUnreadMailboxWide", 26779, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013CA RID: 5066
			public static readonly StorePropTag ConversationMvItemIds = new StorePropTag(26784, PropertyType.MVBinary, new StorePropInfo("ConversationMvItemIds", 26784, PropertyType.MVBinary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013CB RID: 5067
			public static readonly StorePropTag ConversationMvItemIdsMailboxWide = new StorePropTag(26785, PropertyType.MVBinary, new StorePropInfo("ConversationMvItemIdsMailboxWide", 26785, PropertyType.MVBinary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013CC RID: 5068
			public static readonly StorePropTag ConversationHasIrm = new StorePropTag(26786, PropertyType.Boolean, new StorePropInfo("ConversationHasIrm", 26786, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013CD RID: 5069
			public static readonly StorePropTag ConversationHasIrmMailboxWide = new StorePropTag(26787, PropertyType.Boolean, new StorePropInfo("ConversationHasIrmMailboxWide", 26787, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013CE RID: 5070
			public static readonly StorePropTag PersonCompanyNameMailboxWide = new StorePropTag(26788, PropertyType.Unicode, new StorePropInfo("PersonCompanyNameMailboxWide", 26788, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013CF RID: 5071
			public static readonly StorePropTag PersonDisplayNameMailboxWide = new StorePropTag(26789, PropertyType.Unicode, new StorePropInfo("PersonDisplayNameMailboxWide", 26789, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013D0 RID: 5072
			public static readonly StorePropTag PersonGivenNameMailboxWide = new StorePropTag(26790, PropertyType.Unicode, new StorePropInfo("PersonGivenNameMailboxWide", 26790, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013D1 RID: 5073
			public static readonly StorePropTag PersonSurnameMailboxWide = new StorePropTag(26791, PropertyType.Unicode, new StorePropInfo("PersonSurnameMailboxWide", 26791, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013D2 RID: 5074
			public static readonly StorePropTag PersonPhotoContactEntryIdMailboxWide = new StorePropTag(26792, PropertyType.Binary, new StorePropInfo("PersonPhotoContactEntryIdMailboxWide", 26792, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013D3 RID: 5075
			public static readonly StorePropTag ConversationInferredImportanceInternal = new StorePropTag(26794, PropertyType.Real64, new StorePropInfo("ConversationInferredImportanceInternal", 26794, PropertyType.Real64, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013D4 RID: 5076
			public static readonly StorePropTag ConversationInferredImportanceOverride = new StorePropTag(26795, PropertyType.Int32, new StorePropInfo("ConversationInferredImportanceOverride", 26795, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013D5 RID: 5077
			public static readonly StorePropTag ConversationInferredUnimportanceInternal = new StorePropTag(26796, PropertyType.Real64, new StorePropInfo("ConversationInferredUnimportanceInternal", 26796, PropertyType.Real64, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013D6 RID: 5078
			public static readonly StorePropTag ConversationInferredImportanceInternalMailboxWide = new StorePropTag(26797, PropertyType.Real64, new StorePropInfo("ConversationInferredImportanceInternalMailboxWide", 26797, PropertyType.Real64, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013D7 RID: 5079
			public static readonly StorePropTag ConversationInferredImportanceOverrideMailboxWide = new StorePropTag(26798, PropertyType.Int32, new StorePropInfo("ConversationInferredImportanceOverrideMailboxWide", 26798, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013D8 RID: 5080
			public static readonly StorePropTag ConversationInferredUnimportanceInternalMailboxWide = new StorePropTag(26799, PropertyType.Real64, new StorePropInfo("ConversationInferredUnimportanceInternalMailboxWide", 26799, PropertyType.Real64, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013D9 RID: 5081
			public static readonly StorePropTag PersonFileAsMailboxWide = new StorePropTag(26800, PropertyType.Unicode, new StorePropInfo("PersonFileAsMailboxWide", 26800, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013DA RID: 5082
			public static readonly StorePropTag PersonRelevanceScoreMailboxWide = new StorePropTag(26801, PropertyType.Int32, new StorePropInfo("PersonRelevanceScoreMailboxWide", 26801, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013DB RID: 5083
			public static readonly StorePropTag PersonIsDistributionListMailboxWide = new StorePropTag(26802, PropertyType.Boolean, new StorePropInfo("PersonIsDistributionListMailboxWide", 26802, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013DC RID: 5084
			public static readonly StorePropTag PersonHomeCityMailboxWide = new StorePropTag(26803, PropertyType.Unicode, new StorePropInfo("PersonHomeCityMailboxWide", 26803, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013DD RID: 5085
			public static readonly StorePropTag PersonCreationTimeMailboxWide = new StorePropTag(26804, PropertyType.SysTime, new StorePropInfo("PersonCreationTimeMailboxWide", 26804, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013DE RID: 5086
			public static readonly StorePropTag PersonGALLinkIDMailboxWide = new StorePropTag(26807, PropertyType.Guid, new StorePropInfo("PersonGALLinkIDMailboxWide", 26807, PropertyType.Guid, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013DF RID: 5087
			public static readonly StorePropTag PersonMvEmailAddressMailboxWide = new StorePropTag(26810, PropertyType.MVUnicode, new StorePropInfo("PersonMvEmailAddressMailboxWide", 26810, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013E0 RID: 5088
			public static readonly StorePropTag PersonMvEmailDisplayNameMailboxWide = new StorePropTag(26811, PropertyType.MVUnicode, new StorePropInfo("PersonMvEmailDisplayNameMailboxWide", 26811, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013E1 RID: 5089
			public static readonly StorePropTag PersonMvEmailRoutingTypeMailboxWide = new StorePropTag(26812, PropertyType.MVUnicode, new StorePropInfo("PersonMvEmailRoutingTypeMailboxWide", 26812, PropertyType.MVUnicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013E2 RID: 5090
			public static readonly StorePropTag PersonImAddressMailboxWide = new StorePropTag(26813, PropertyType.Unicode, new StorePropInfo("PersonImAddressMailboxWide", 26813, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013E3 RID: 5091
			public static readonly StorePropTag PersonWorkCityMailboxWide = new StorePropTag(26814, PropertyType.Unicode, new StorePropInfo("PersonWorkCityMailboxWide", 26814, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013E4 RID: 5092
			public static readonly StorePropTag PersonDisplayNameFirstLastMailboxWide = new StorePropTag(26815, PropertyType.Unicode, new StorePropInfo("PersonDisplayNameFirstLastMailboxWide", 26815, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013E5 RID: 5093
			public static readonly StorePropTag PersonDisplayNameLastFirstMailboxWide = new StorePropTag(26816, PropertyType.Unicode, new StorePropInfo("PersonDisplayNameLastFirstMailboxWide", 26816, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013E6 RID: 5094
			public static readonly StorePropTag ConversationGroupingActions = new StorePropTag(26814, PropertyType.MVInt16, new StorePropInfo("ConversationGroupingActions", 26814, PropertyType.MVInt16, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013E7 RID: 5095
			public static readonly StorePropTag ConversationGroupingActionsMailboxWide = new StorePropTag(26815, PropertyType.MVInt16, new StorePropInfo("ConversationGroupingActionsMailboxWide", 26815, PropertyType.MVInt16, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013E8 RID: 5096
			public static readonly StorePropTag ConversationPredictedActionsSummary = new StorePropTag(26816, PropertyType.Int32, new StorePropInfo("ConversationPredictedActionsSummary", 26816, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013E9 RID: 5097
			public static readonly StorePropTag ConversationPredictedActionsSummaryMailboxWide = new StorePropTag(26817, PropertyType.Int32, new StorePropInfo("ConversationPredictedActionsSummaryMailboxWide", 26817, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013EA RID: 5098
			public static readonly StorePropTag ConversationHasClutter = new StorePropTag(26818, PropertyType.Boolean, new StorePropInfo("ConversationHasClutter", 26818, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013EB RID: 5099
			public static readonly StorePropTag ConversationHasClutterMailboxWide = new StorePropTag(26819, PropertyType.Boolean, new StorePropInfo("ConversationHasClutterMailboxWide", 26819, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013EC RID: 5100
			public static readonly StorePropTag ConversationLastMemberDocumentId = new StorePropTag(26880, PropertyType.Int32, new StorePropInfo("ConversationLastMemberDocumentId", 26880, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013ED RID: 5101
			public static readonly StorePropTag ConversationPreview = new StorePropTag(26881, PropertyType.Unicode, new StorePropInfo("ConversationPreview", 26881, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013EE RID: 5102
			public static readonly StorePropTag ConversationLastMemberDocumentIdMailboxWide = new StorePropTag(26882, PropertyType.Int32, new StorePropInfo("ConversationLastMemberDocumentIdMailboxWide", 26882, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013EF RID: 5103
			public static readonly StorePropTag ConversationInitialMemberDocumentId = new StorePropTag(26883, PropertyType.Int32, new StorePropInfo("ConversationInitialMemberDocumentId", 26883, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013F0 RID: 5104
			public static readonly StorePropTag ConversationMemberDocumentIds = new StorePropTag(26884, PropertyType.MVInt32, new StorePropInfo("ConversationMemberDocumentIds", 26884, PropertyType.MVInt32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013F1 RID: 5105
			public static readonly StorePropTag ConversationMessageDeliveryOrRenewTimeMailboxWide = new StorePropTag(26885, PropertyType.SysTime, new StorePropInfo("ConversationMessageDeliveryOrRenewTimeMailboxWide", 26885, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013F2 RID: 5106
			public static readonly StorePropTag FamilyId = new StorePropTag(26886, PropertyType.Binary, new StorePropInfo("FamilyId", 26886, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013F3 RID: 5107
			public static readonly StorePropTag ConversationMessageRichContentMailboxWide = new StorePropTag(26887, PropertyType.MVInt16, new StorePropInfo("ConversationMessageRichContentMailboxWide", 26887, PropertyType.MVInt16, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013F4 RID: 5108
			public static readonly StorePropTag ConversationPreviewMailboxWide = new StorePropTag(26888, PropertyType.Unicode, new StorePropInfo("ConversationPreviewMailboxWide", 26888, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013F5 RID: 5109
			public static readonly StorePropTag ConversationMessageDeliveryOrRenewTime = new StorePropTag(26889, PropertyType.SysTime, new StorePropInfo("ConversationMessageDeliveryOrRenewTime", 26889, PropertyType.SysTime, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013F6 RID: 5110
			public static readonly StorePropTag ConversationWorkingSetSourcePartition = new StorePropTag(26890, PropertyType.Unicode, new StorePropInfo("ConversationWorkingSetSourcePartition", 26890, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013F7 RID: 5111
			public static readonly StorePropTag NDRFromName = new StorePropTag(26885, PropertyType.Unicode, new StorePropInfo("NDRFromName", 26885, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013F8 RID: 5112
			public static readonly StorePropTag SecurityFlags = new StorePropTag(28161, PropertyType.Int32, new StorePropInfo("SecurityFlags", 28161, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013F9 RID: 5113
			public static readonly StorePropTag SecurityReceiptRequestProcessed = new StorePropTag(28164, PropertyType.Boolean, new StorePropInfo("SecurityReceiptRequestProcessed", 28164, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013FA RID: 5114
			public static readonly StorePropTag FavoriteDisplayName = new StorePropTag(31744, PropertyType.Unicode, new StorePropInfo("FavoriteDisplayName", 31744, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013FB RID: 5115
			public static readonly StorePropTag FavoriteDisplayAlias = new StorePropTag(31745, PropertyType.Unicode, new StorePropInfo("FavoriteDisplayAlias", 31745, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013FC RID: 5116
			public static readonly StorePropTag FavPublicSourceKey = new StorePropTag(31746, PropertyType.Binary, new StorePropInfo("FavPublicSourceKey", 31746, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013FD RID: 5117
			public static readonly StorePropTag SyncFolderSourceKey = new StorePropTag(31747, PropertyType.Binary, new StorePropInfo("SyncFolderSourceKey", 31747, PropertyType.Binary, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013FE RID: 5118
			public static readonly StorePropTag UserConfigurationDataType = new StorePropTag(31750, PropertyType.Int32, new StorePropInfo("UserConfigurationDataType", 31750, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x040013FF RID: 5119
			public static readonly StorePropTag UserConfigurationXmlStream = new StorePropTag(31752, PropertyType.Binary, new StorePropInfo("UserConfigurationXmlStream", 31752, PropertyType.Binary, StorePropInfo.Flags.Private, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001400 RID: 5120
			public static readonly StorePropTag UserConfigurationStream = new StorePropTag(31753, PropertyType.Binary, new StorePropInfo("UserConfigurationStream", 31753, PropertyType.Binary, StorePropInfo.Flags.Private, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001401 RID: 5121
			public static readonly StorePropTag ReplyFwdStatus = new StorePropTag(31755, PropertyType.Unicode, new StorePropInfo("ReplyFwdStatus", 31755, PropertyType.Unicode, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001402 RID: 5122
			public static readonly StorePropTag OscSyncEnabledOnServer = new StorePropTag(31780, PropertyType.Boolean, new StorePropInfo("OscSyncEnabledOnServer", 31780, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001403 RID: 5123
			public static readonly StorePropTag Processed = new StorePropTag(32001, PropertyType.Boolean, new StorePropInfo("Processed", 32001, PropertyType.Boolean, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001404 RID: 5124
			public static readonly StorePropTag FavLevelMask = new StorePropTag(32003, PropertyType.Int32, new StorePropInfo("FavLevelMask", 32003, PropertyType.Int32, StorePropInfo.Flags.None, 9223372036854775808UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Message);

			// Token: 0x04001405 RID: 5125
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[]
			{
				PropTag.Message.MailboxPartitionNumber,
				PropTag.Message.MailboxNumberInternal,
				PropTag.Message.VirtualParentDisplay,
				PropTag.Message.InternalConversationIndexTracking,
				PropTag.Message.InternalConversationIndex,
				PropTag.Message.ConversationItemConversationId,
				PropTag.Message.VirtualUnreadMessageCount,
				PropTag.Message.VirtualIsRead,
				PropTag.Message.IsReadColumn,
				PropTag.Message.Internal9ByteChangeNumber,
				PropTag.Message.Internal9ByteReadCnNew,
				PropTag.Message.CategoryHeaderLevelStub1,
				PropTag.Message.CategoryHeaderLevelStub2,
				PropTag.Message.CategoryHeaderLevelStub3,
				PropTag.Message.CategoryHeaderAggregateProp0,
				PropTag.Message.CategoryHeaderAggregateProp1,
				PropTag.Message.CategoryHeaderAggregateProp2,
				PropTag.Message.CategoryHeaderAggregateProp3,
				PropTag.Message.MessageFlagsActual,
				PropTag.Message.InternalChangeKey,
				PropTag.Message.InternalSourceKey
			};

			// Token: 0x04001406 RID: 5126
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[]
			{
				PropTag.Message.Subject,
				PropTag.Message.SubjectPrefix,
				PropTag.Message.ParentEntryId,
				PropTag.Message.ParentEntryIdSvrEid,
				PropTag.Message.SentMailEntryId,
				PropTag.Message.MsgStatus,
				PropTag.Message.NormalizedSubject,
				PropTag.Message.Read,
				PropTag.Message.InstanceKey,
				PropTag.Message.InstanceKeySvrEid,
				PropTag.Message.MappingSignature,
				PropTag.Message.RecordKey,
				PropTag.Message.RecordKeySvrEid,
				PropTag.Message.StoreRecordKey,
				PropTag.Message.StoreEntryId,
				PropTag.Message.ObjectType,
				PropTag.Message.EntryId,
				PropTag.Message.EntryIdSvrEid,
				PropTag.Message.ImapCachedMsgSize,
				PropTag.Message.StoreSupportMask,
				PropTag.Message.MdbProvider,
				PropTag.Message.MailboxPartitionNumber,
				PropTag.Message.MailboxNumberInternal,
				PropTag.Message.VirtualParentDisplay,
				PropTag.Message.InternalConversationIndexTracking,
				PropTag.Message.InternalConversationIndex,
				PropTag.Message.ConversationItemConversationId,
				PropTag.Message.VirtualUnreadMessageCount,
				PropTag.Message.VirtualIsRead,
				PropTag.Message.IsReadColumn,
				PropTag.Message.Internal9ByteChangeNumber,
				PropTag.Message.Internal9ByteReadCnNew,
				PropTag.Message.CategoryHeaderLevelStub1,
				PropTag.Message.CategoryHeaderLevelStub2,
				PropTag.Message.CategoryHeaderLevelStub3,
				PropTag.Message.CategoryHeaderAggregateProp0,
				PropTag.Message.CategoryHeaderAggregateProp1,
				PropTag.Message.CategoryHeaderAggregateProp2,
				PropTag.Message.CategoryHeaderAggregateProp3,
				PropTag.Message.MessageFlagsActual,
				PropTag.Message.InternalChangeKey,
				PropTag.Message.InternalSourceKey,
				PropTag.Message.InternalPostReply,
				PropTag.Message.ReplicaServer,
				PropTag.Message.LongTermEntryIdFromTable,
				PropTag.Message.DeletedOn,
				PropTag.Message.PropGroupInfo,
				PropTag.Message.PropertyGroupChangeMask,
				PropTag.Message.ReadCnNewExport,
				PropTag.Message.LocallyDelivered,
				PropTag.Message.Fid,
				PropTag.Message.FidBin,
				PropTag.Message.Mid,
				PropTag.Message.MidBin,
				PropTag.Message.InstanceId,
				PropTag.Message.InstanceNum,
				PropTag.Message.RequiresRefResolve,
				PropTag.Message.CnExport,
				PropTag.Message.PclExport,
				PropTag.Message.CnMvExport,
				PropTag.Message.MailboxGuid,
				PropTag.Message.MapiEntryIdGuidGuid,
				PropTag.Message.ImapCachedBodystructure,
				PropTag.Message.StorageQuota,
				PropTag.Message.CnsetIn,
				PropTag.Message.CnsetSeen,
				PropTag.Message.ChangeNumber,
				PropTag.Message.ChangeNumberBin,
				PropTag.Message.PCL,
				PropTag.Message.CnMv,
				PropTag.Message.SourceEntryId,
				PropTag.Message.MailFlags,
				PropTag.Message.Associated,
				PropTag.Message.SubmitResponsibility,
				PropTag.Message.SharedReceiptHandling,
				PropTag.Message.Inid,
				PropTag.Message.MessageAttachList,
				PropTag.Message.SenderCAI,
				PropTag.Message.SentRepresentingCAI,
				PropTag.Message.OriginalSenderCAI,
				PropTag.Message.OriginalSentRepresentingCAI,
				PropTag.Message.ReceivedByCAI,
				PropTag.Message.ReceivedRepresentingCAI,
				PropTag.Message.ReadReceiptCAI,
				PropTag.Message.ReportCAI,
				PropTag.Message.CreatorCAI,
				PropTag.Message.LastModifierCAI,
				PropTag.Message.CnsetRead,
				PropTag.Message.CnsetSeenFAI,
				PropTag.Message.IdSetDeleted,
				PropTag.Message.OriginatorCAI,
				PropTag.Message.ReportDestinationCAI,
				PropTag.Message.OriginalAuthorCAI,
				PropTag.Message.ReadCnNew,
				PropTag.Message.ReadCnNewBin,
				PropTag.Message.DocumentId
			};

			// Token: 0x04001407 RID: 5127
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[]
			{
				PropTag.Message.ParentEntryId,
				PropTag.Message.ParentEntryIdSvrEid,
				PropTag.Message.MsgStatus,
				PropTag.Message.Read,
				PropTag.Message.InstanceKey,
				PropTag.Message.InstanceKeySvrEid,
				PropTag.Message.MappingSignature,
				PropTag.Message.RecordKey,
				PropTag.Message.RecordKeySvrEid,
				PropTag.Message.StoreRecordKey,
				PropTag.Message.StoreEntryId,
				PropTag.Message.ObjectType,
				PropTag.Message.EntryId,
				PropTag.Message.EntryIdSvrEid,
				PropTag.Message.StoreSupportMask,
				PropTag.Message.MdbProvider,
				PropTag.Message.MailboxPartitionNumber,
				PropTag.Message.MailboxNumberInternal,
				PropTag.Message.VirtualParentDisplay,
				PropTag.Message.InternalConversationIndexTracking,
				PropTag.Message.InternalConversationIndex,
				PropTag.Message.ConversationItemConversationId,
				PropTag.Message.VirtualUnreadMessageCount,
				PropTag.Message.VirtualIsRead,
				PropTag.Message.IsReadColumn,
				PropTag.Message.Internal9ByteChangeNumber,
				PropTag.Message.Internal9ByteReadCnNew,
				PropTag.Message.CategoryHeaderLevelStub1,
				PropTag.Message.CategoryHeaderLevelStub2,
				PropTag.Message.CategoryHeaderLevelStub3,
				PropTag.Message.CategoryHeaderAggregateProp0,
				PropTag.Message.CategoryHeaderAggregateProp1,
				PropTag.Message.CategoryHeaderAggregateProp2,
				PropTag.Message.CategoryHeaderAggregateProp3,
				PropTag.Message.MessageFlagsActual,
				PropTag.Message.InternalChangeKey,
				PropTag.Message.InternalSourceKey,
				PropTag.Message.InternalPostReply,
				PropTag.Message.SourceKey,
				PropTag.Message.ChangeKey,
				PropTag.Message.PredecessorChangeList,
				PropTag.Message.ReplicaServer,
				PropTag.Message.LongTermEntryIdFromTable,
				PropTag.Message.DeletedOn,
				PropTag.Message.PropGroupInfo,
				PropTag.Message.PropertyGroupChangeMask,
				PropTag.Message.ReadCnNewExport,
				PropTag.Message.Fid,
				PropTag.Message.FidBin,
				PropTag.Message.Mid,
				PropTag.Message.MidBin,
				PropTag.Message.InstanceId,
				PropTag.Message.InstanceNum,
				PropTag.Message.CnExport,
				PropTag.Message.PclExport,
				PropTag.Message.MailboxGuid,
				PropTag.Message.MapiEntryIdGuidGuid,
				PropTag.Message.ImapCachedBodystructure,
				PropTag.Message.StorageQuota,
				PropTag.Message.CnsetIn,
				PropTag.Message.CnsetSeen,
				PropTag.Message.ChangeNumber,
				PropTag.Message.ChangeNumberBin,
				PropTag.Message.PCL,
				PropTag.Message.CnMv,
				PropTag.Message.SourceEntryId,
				PropTag.Message.MailFlags,
				PropTag.Message.Associated,
				PropTag.Message.SubmitResponsibility,
				PropTag.Message.SharedReceiptHandling,
				PropTag.Message.Inid,
				PropTag.Message.MessageAttachList,
				PropTag.Message.SenderCAI,
				PropTag.Message.SentRepresentingCAI,
				PropTag.Message.OriginalSenderCAI,
				PropTag.Message.OriginalSentRepresentingCAI,
				PropTag.Message.ReceivedByCAI,
				PropTag.Message.ReceivedRepresentingCAI,
				PropTag.Message.ReadReceiptCAI,
				PropTag.Message.ReportCAI,
				PropTag.Message.CreatorCAI,
				PropTag.Message.LastModifierCAI,
				PropTag.Message.CnsetRead,
				PropTag.Message.CnsetSeenFAI,
				PropTag.Message.IdSetDeleted,
				PropTag.Message.OriginatorCAI,
				PropTag.Message.ReportDestinationCAI,
				PropTag.Message.OriginalAuthorCAI,
				PropTag.Message.ReadCnNew,
				PropTag.Message.ReadCnNewBin,
				PropTag.Message.DocumentId
			};

			// Token: 0x04001408 RID: 5128
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[]
			{
				PropTag.Message.DisplayBcc,
				PropTag.Message.DisplayCc,
				PropTag.Message.DisplayTo,
				PropTag.Message.ParentDisplay,
				PropTag.Message.MessageSize,
				PropTag.Message.MessageSize32,
				PropTag.Message.ParentEntryId,
				PropTag.Message.ParentEntryIdSvrEid,
				PropTag.Message.MessageRecipients,
				PropTag.Message.MessageRecipientsMVBin,
				PropTag.Message.MessageAttachments,
				PropTag.Message.ItemSubobjectsBin,
				PropTag.Message.SubmitFlags,
				PropTag.Message.HasAttach,
				PropTag.Message.InternetArticleNumber,
				PropTag.Message.IMAPId,
				PropTag.Message.MessageDeepAttachments,
				PropTag.Message.CreatorGuid,
				PropTag.Message.LastModifierGuid,
				PropTag.Message.CreatorSID,
				PropTag.Message.LastModifierSid,
				PropTag.Message.VirusScannerStamp,
				PropTag.Message.VirusTransportStamp,
				PropTag.Message.SearchAttachmentsOLK,
				PropTag.Message.SearchRecipEmailTo,
				PropTag.Message.SearchRecipEmailCc,
				PropTag.Message.SearchRecipEmailBcc,
				PropTag.Message.SearchFullTextSubject,
				PropTag.Message.SearchFullTextBody,
				PropTag.Message.SearchAllIndexedProps,
				PropTag.Message.SearchRecipients,
				PropTag.Message.SearchRecipientsTo,
				PropTag.Message.SearchRecipientsCc,
				PropTag.Message.SearchRecipientsBcc,
				PropTag.Message.SearchAccountTo,
				PropTag.Message.SearchAccountCc,
				PropTag.Message.SearchAccountBcc,
				PropTag.Message.SearchEmailAddressTo,
				PropTag.Message.SearchEmailAddressCc,
				PropTag.Message.SearchEmailAddressBcc,
				PropTag.Message.SearchSmtpAddressTo,
				PropTag.Message.SearchSmtpAddressCc,
				PropTag.Message.SearchSmtpAddressBcc,
				PropTag.Message.SearchSender,
				PropTag.Message.SearchIsPartiallyIndexed,
				PropTag.Message.Access,
				PropTag.Message.RowType,
				PropTag.Message.InstanceKey,
				PropTag.Message.InstanceKeySvrEid,
				PropTag.Message.AccessLevel,
				PropTag.Message.RecordKey,
				PropTag.Message.RecordKeySvrEid,
				PropTag.Message.EntryId,
				PropTag.Message.EntryIdSvrEid,
				PropTag.Message.NativeBodyInfo,
				PropTag.Message.NativeBodyType,
				PropTag.Message.NativeBody,
				PropTag.Message.Depth,
				PropTag.Message.CreationTime,
				PropTag.Message.LastModificationTime,
				PropTag.Message.ConversationId,
				PropTag.Message.ContentCount,
				PropTag.Message.UnreadCount,
				PropTag.Message.UnreadCountInt64,
				PropTag.Message.MailboxPartitionNumber,
				PropTag.Message.MailboxNumberInternal,
				PropTag.Message.VirtualParentDisplay,
				PropTag.Message.InternalConversationIndexTracking,
				PropTag.Message.InternalConversationIndex,
				PropTag.Message.ConversationItemConversationId,
				PropTag.Message.VirtualUnreadMessageCount,
				PropTag.Message.VirtualIsRead,
				PropTag.Message.IsReadColumn,
				PropTag.Message.Internal9ByteChangeNumber,
				PropTag.Message.Internal9ByteReadCnNew,
				PropTag.Message.CategoryHeaderLevelStub1,
				PropTag.Message.CategoryHeaderLevelStub2,
				PropTag.Message.CategoryHeaderLevelStub3,
				PropTag.Message.CategoryHeaderAggregateProp0,
				PropTag.Message.CategoryHeaderAggregateProp1,
				PropTag.Message.CategoryHeaderAggregateProp2,
				PropTag.Message.CategoryHeaderAggregateProp3,
				PropTag.Message.MessageFlagsActual,
				PropTag.Message.InternalChangeKey,
				PropTag.Message.InternalSourceKey,
				PropTag.Message.PreviewUnread,
				PropTag.Message.CreatorName,
				PropTag.Message.CreatorEntryId,
				PropTag.Message.LastModifierName,
				PropTag.Message.LastModifierEntryId,
				PropTag.Message.NewAttach,
				PropTag.Message.StartEmbed,
				PropTag.Message.EndEmbed,
				PropTag.Message.StartRecip,
				PropTag.Message.EndRecip,
				PropTag.Message.EndCcRecip,
				PropTag.Message.EndBccRecip,
				PropTag.Message.EndP1Recip,
				PropTag.Message.DNPrefix,
				PropTag.Message.StartTopFolder,
				PropTag.Message.StartSubFolder,
				PropTag.Message.EndFolder,
				PropTag.Message.StartMessage,
				PropTag.Message.EndMessage,
				PropTag.Message.EndAttach,
				PropTag.Message.EcWarning,
				PropTag.Message.StartFAIMessage,
				PropTag.Message.NewFXFolder,
				PropTag.Message.IncrSyncChange,
				PropTag.Message.IncrSyncDelete,
				PropTag.Message.IncrSyncEnd,
				PropTag.Message.IncrSyncMessage,
				PropTag.Message.FastTransferDelProp,
				PropTag.Message.IdsetGiven,
				PropTag.Message.IdsetGivenInt32,
				PropTag.Message.FastTransferErrorInfo,
				PropTag.Message.SoftDeletes,
				PropTag.Message.CreatorAddressType,
				PropTag.Message.CreatorEmailAddr,
				PropTag.Message.LastModifierAddressType,
				PropTag.Message.LastModifierEmailAddr,
				PropTag.Message.IdsetRead,
				PropTag.Message.IdsetUnread,
				PropTag.Message.IncrSyncRead,
				PropTag.Message.CreatorSimpleDisplayName,
				PropTag.Message.LastModifierSimpleDisplayName,
				PropTag.Message.IncrSyncStateBegin,
				PropTag.Message.IncrSyncStateEnd,
				PropTag.Message.IncrSyncImailStream,
				PropTag.Message.CreatorOrgAddressType,
				PropTag.Message.CreatorOrgEmailAddr,
				PropTag.Message.LastModifierOrgAddressType,
				PropTag.Message.LastModifierOrgEmailAddr,
				PropTag.Message.CreatorFlags,
				PropTag.Message.LastModifierFlags,
				PropTag.Message.IncrSyncImailStreamContinue,
				PropTag.Message.IncrSyncImailStreamCancel,
				PropTag.Message.IncrSyncImailStream2Continue,
				PropTag.Message.IncrSyncProgressMode,
				PropTag.Message.SyncProgressPerMsg,
				PropTag.Message.IncrSyncMsgPartial,
				PropTag.Message.IncrSyncGroupInfo,
				PropTag.Message.IncrSyncGroupId,
				PropTag.Message.IncrSyncChangePartial,
				PropTag.Message.InternetMessageIdHash,
				PropTag.Message.ConversationTopicHash,
				PropTag.Message.SourceKey,
				PropTag.Message.ParentSourceKey,
				PropTag.Message.ChangeKey,
				PropTag.Message.PredecessorChangeList,
				PropTag.Message.HasNamedProperties,
				PropTag.Message.SearchAttachments,
				PropTag.Message.SubmittedByAdmin,
				PropTag.Message.DeletedOn,
				PropTag.Message.MailboxDSGuidGuid,
				PropTag.Message.URLName,
				PropTag.Message.LocalCommitTime,
				PropTag.Message.PropGroupInfo,
				PropTag.Message.PropertyGroupChangeMask,
				PropTag.Message.ReadCnNewExport,
				PropTag.Message.Fid,
				PropTag.Message.FidBin,
				PropTag.Message.ParentFid,
				PropTag.Message.Mid,
				PropTag.Message.MidBin,
				PropTag.Message.CategID,
				PropTag.Message.ParentCategID,
				PropTag.Message.InstanceId,
				PropTag.Message.InstanceNum,
				PropTag.Message.ChangeType,
				PropTag.Message.LTID,
				PropTag.Message.CnExport,
				PropTag.Message.PclExport,
				PropTag.Message.CnMvExport,
				PropTag.Message.MailboxGuid,
				PropTag.Message.MapiEntryIdGuidGuid,
				PropTag.Message.ImapCachedBodystructure,
				PropTag.Message.StorageQuota,
				PropTag.Message.CnsetIn,
				PropTag.Message.CnsetSeen,
				PropTag.Message.ChangeNumber,
				PropTag.Message.ChangeNumberBin,
				PropTag.Message.PCL,
				PropTag.Message.CnMv,
				PropTag.Message.SourceEntryId,
				PropTag.Message.MailFlags,
				PropTag.Message.Associated,
				PropTag.Message.SubmitResponsibility,
				PropTag.Message.SharedReceiptHandling,
				PropTag.Message.Inid,
				PropTag.Message.MessageAttachList,
				PropTag.Message.SenderCAI,
				PropTag.Message.SentRepresentingCAI,
				PropTag.Message.OriginalSenderCAI,
				PropTag.Message.OriginalSentRepresentingCAI,
				PropTag.Message.ReceivedByCAI,
				PropTag.Message.ReceivedRepresentingCAI,
				PropTag.Message.ReadReceiptCAI,
				PropTag.Message.ReportCAI,
				PropTag.Message.CreatorCAI,
				PropTag.Message.LastModifierCAI,
				PropTag.Message.CnsetRead,
				PropTag.Message.CnsetSeenFAI,
				PropTag.Message.IdSetDeleted,
				PropTag.Message.OriginatorCAI,
				PropTag.Message.ReportDestinationCAI,
				PropTag.Message.OriginalAuthorCAI,
				PropTag.Message.ReadCnNew,
				PropTag.Message.ReadCnNewBin,
				PropTag.Message.DocumentId,
				PropTag.Message.MailboxNum,
				PropTag.Message.ConversationIdHash,
				PropTag.Message.ConversationDocumentId
			};

			// Token: 0x04001409 RID: 5129
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[]
			{
				PropTag.Message.InternetArticleNumber,
				PropTag.Message.IMAPId,
				PropTag.Message.CreatorGuid,
				PropTag.Message.LastModifierGuid,
				PropTag.Message.CreatorSID,
				PropTag.Message.LastModifierSid,
				PropTag.Message.CreationTime,
				PropTag.Message.LastModificationTime,
				PropTag.Message.CreatorName,
				PropTag.Message.CreatorEntryId,
				PropTag.Message.LastModifierName,
				PropTag.Message.LastModifierEntryId,
				PropTag.Message.CreatorAddressType,
				PropTag.Message.CreatorEmailAddr,
				PropTag.Message.LastModifierAddressType,
				PropTag.Message.LastModifierEmailAddr,
				PropTag.Message.CreatorSimpleDisplayName,
				PropTag.Message.LastModifierSimpleDisplayName,
				PropTag.Message.CreatorOrgAddressType,
				PropTag.Message.CreatorOrgEmailAddr,
				PropTag.Message.LastModifierOrgAddressType,
				PropTag.Message.LastModifierOrgEmailAddr,
				PropTag.Message.CreatorFlags,
				PropTag.Message.LastModifierFlags,
				PropTag.Message.PredecessorChangeList,
				PropTag.Message.SubmittedByAdmin,
				PropTag.Message.ReadCnNewExport,
				PropTag.Message.CnExport,
				PropTag.Message.PclExport,
				PropTag.Message.CnMvExport,
				PropTag.Message.ChangeNumber,
				PropTag.Message.PCL,
				PropTag.Message.CnMv,
				PropTag.Message.MailFlags,
				PropTag.Message.ReadCnNew
			};

			// Token: 0x0400140A RID: 5130
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[]
			{
				PropTag.Message.LastModifierName
			};

			// Token: 0x0400140B RID: 5131
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[]
			{
				PropTag.Message.VirusScannerStamp
			};

			// Token: 0x0400140C RID: 5132
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[]
			{
				PropTag.Message.LastModifierGuid,
				PropTag.Message.LastModifierSid,
				PropTag.Message.LastModificationTime,
				PropTag.Message.LastModifierName,
				PropTag.Message.LastModifierEntryId,
				PropTag.Message.LastModifierAddressType,
				PropTag.Message.LastModifierEmailAddr,
				PropTag.Message.LastModifierSimpleDisplayName,
				PropTag.Message.LastModifierOrgAddressType,
				PropTag.Message.LastModifierOrgEmailAddr,
				PropTag.Message.LastModifierFlags
			};

			// Token: 0x0400140D RID: 5133
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x0400140E RID: 5134
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[]
			{
				PropTag.Message.Subject,
				PropTag.Message.DisplayBcc,
				PropTag.Message.DisplayCc,
				PropTag.Message.DisplayTo,
				PropTag.Message.ParentDisplay,
				PropTag.Message.MessageSize,
				PropTag.Message.MessageSize32,
				PropTag.Message.ParentEntryId,
				PropTag.Message.ParentEntryIdSvrEid,
				PropTag.Message.MessageRecipients,
				PropTag.Message.MessageRecipientsMVBin,
				PropTag.Message.MessageAttachments,
				PropTag.Message.ItemSubobjectsBin,
				PropTag.Message.SubmitFlags,
				PropTag.Message.MsgStatus,
				PropTag.Message.HasAttach,
				PropTag.Message.InternetArticleNumber,
				PropTag.Message.IMAPId,
				PropTag.Message.MessageDeepAttachments,
				PropTag.Message.CreatorGuid,
				PropTag.Message.LastModifierGuid,
				PropTag.Message.CreatorSID,
				PropTag.Message.LastModifierSid,
				PropTag.Message.VirusScannerStamp,
				PropTag.Message.VirusTransportStamp,
				PropTag.Message.SearchAttachmentsOLK,
				PropTag.Message.SearchRecipEmailTo,
				PropTag.Message.SearchRecipEmailCc,
				PropTag.Message.SearchRecipEmailBcc,
				PropTag.Message.SearchFullTextSubject,
				PropTag.Message.SearchFullTextBody,
				PropTag.Message.SearchAllIndexedProps,
				PropTag.Message.SearchRecipients,
				PropTag.Message.SearchRecipientsTo,
				PropTag.Message.SearchRecipientsCc,
				PropTag.Message.SearchRecipientsBcc,
				PropTag.Message.SearchAccountTo,
				PropTag.Message.SearchAccountCc,
				PropTag.Message.SearchAccountBcc,
				PropTag.Message.SearchEmailAddressTo,
				PropTag.Message.SearchEmailAddressCc,
				PropTag.Message.SearchEmailAddressBcc,
				PropTag.Message.SearchSmtpAddressTo,
				PropTag.Message.SearchSmtpAddressCc,
				PropTag.Message.SearchSmtpAddressBcc,
				PropTag.Message.SearchSender,
				PropTag.Message.SearchIsPartiallyIndexed,
				PropTag.Message.Access,
				PropTag.Message.RowType,
				PropTag.Message.InstanceKey,
				PropTag.Message.InstanceKeySvrEid,
				PropTag.Message.AccessLevel,
				PropTag.Message.MappingSignature,
				PropTag.Message.RecordKey,
				PropTag.Message.RecordKeySvrEid,
				PropTag.Message.StoreRecordKey,
				PropTag.Message.StoreEntryId,
				PropTag.Message.ObjectType,
				PropTag.Message.EntryId,
				PropTag.Message.EntryIdSvrEid,
				PropTag.Message.NativeBodyInfo,
				PropTag.Message.NativeBodyType,
				PropTag.Message.NativeBody,
				PropTag.Message.Depth,
				PropTag.Message.CreationTime,
				PropTag.Message.LastModificationTime,
				PropTag.Message.ConversationId,
				PropTag.Message.StoreSupportMask,
				PropTag.Message.MdbProvider,
				PropTag.Message.EventEmailReminderTimer,
				PropTag.Message.ContentCount,
				PropTag.Message.UnreadCount,
				PropTag.Message.UnreadCountInt64,
				PropTag.Message.MailboxPartitionNumber,
				PropTag.Message.MailboxNumberInternal,
				PropTag.Message.VirtualParentDisplay,
				PropTag.Message.InternalConversationIndexTracking,
				PropTag.Message.InternalConversationIndex,
				PropTag.Message.ConversationItemConversationId,
				PropTag.Message.VirtualUnreadMessageCount,
				PropTag.Message.VirtualIsRead,
				PropTag.Message.IsReadColumn,
				PropTag.Message.Internal9ByteChangeNumber,
				PropTag.Message.Internal9ByteReadCnNew,
				PropTag.Message.CategoryHeaderLevelStub1,
				PropTag.Message.CategoryHeaderLevelStub2,
				PropTag.Message.CategoryHeaderLevelStub3,
				PropTag.Message.CategoryHeaderAggregateProp0,
				PropTag.Message.CategoryHeaderAggregateProp1,
				PropTag.Message.CategoryHeaderAggregateProp2,
				PropTag.Message.CategoryHeaderAggregateProp3,
				PropTag.Message.MessageFlagsActual,
				PropTag.Message.InternalChangeKey,
				PropTag.Message.InternalSourceKey,
				PropTag.Message.PreviewUnread,
				PropTag.Message.CreatorName,
				PropTag.Message.CreatorEntryId,
				PropTag.Message.LastModifierName,
				PropTag.Message.LastModifierEntryId,
				PropTag.Message.NewAttach,
				PropTag.Message.StartEmbed,
				PropTag.Message.EndEmbed,
				PropTag.Message.StartRecip,
				PropTag.Message.EndRecip,
				PropTag.Message.EndCcRecip,
				PropTag.Message.EndBccRecip,
				PropTag.Message.EndP1Recip,
				PropTag.Message.DNPrefix,
				PropTag.Message.StartTopFolder,
				PropTag.Message.StartSubFolder,
				PropTag.Message.EndFolder,
				PropTag.Message.StartMessage,
				PropTag.Message.EndMessage,
				PropTag.Message.EndAttach,
				PropTag.Message.EcWarning,
				PropTag.Message.StartFAIMessage,
				PropTag.Message.NewFXFolder,
				PropTag.Message.IncrSyncChange,
				PropTag.Message.IncrSyncDelete,
				PropTag.Message.IncrSyncEnd,
				PropTag.Message.IncrSyncMessage,
				PropTag.Message.FastTransferDelProp,
				PropTag.Message.IdsetGiven,
				PropTag.Message.IdsetGivenInt32,
				PropTag.Message.FastTransferErrorInfo,
				PropTag.Message.SoftDeletes,
				PropTag.Message.CreatorAddressType,
				PropTag.Message.CreatorEmailAddr,
				PropTag.Message.LastModifierAddressType,
				PropTag.Message.LastModifierEmailAddr,
				PropTag.Message.IdsetRead,
				PropTag.Message.IdsetUnread,
				PropTag.Message.IncrSyncRead,
				PropTag.Message.CreatorSimpleDisplayName,
				PropTag.Message.LastModifierSimpleDisplayName,
				PropTag.Message.IncrSyncStateBegin,
				PropTag.Message.IncrSyncStateEnd,
				PropTag.Message.IncrSyncImailStream,
				PropTag.Message.CreatorOrgAddressType,
				PropTag.Message.CreatorOrgEmailAddr,
				PropTag.Message.LastModifierOrgAddressType,
				PropTag.Message.LastModifierOrgEmailAddr,
				PropTag.Message.CreatorFlags,
				PropTag.Message.LastModifierFlags,
				PropTag.Message.IncrSyncImailStreamContinue,
				PropTag.Message.IncrSyncImailStreamCancel,
				PropTag.Message.IncrSyncImailStream2Continue,
				PropTag.Message.IncrSyncProgressMode,
				PropTag.Message.SyncProgressPerMsg,
				PropTag.Message.IncrSyncMsgPartial,
				PropTag.Message.IncrSyncGroupInfo,
				PropTag.Message.IncrSyncGroupId,
				PropTag.Message.IncrSyncChangePartial,
				PropTag.Message.InternetMessageIdHash,
				PropTag.Message.ConversationTopicHash,
				PropTag.Message.SourceKey,
				PropTag.Message.ParentSourceKey,
				PropTag.Message.ChangeKey,
				PropTag.Message.PredecessorChangeList,
				PropTag.Message.ReplicaServer,
				PropTag.Message.HasNamedProperties,
				PropTag.Message.SearchAttachments,
				PropTag.Message.SubmittedByAdmin,
				PropTag.Message.LongTermEntryIdFromTable,
				PropTag.Message.DeletedOn,
				PropTag.Message.MailboxDSGuidGuid,
				PropTag.Message.URLName,
				PropTag.Message.LocalCommitTime,
				PropTag.Message.PropGroupInfo,
				PropTag.Message.PropertyGroupChangeMask,
				PropTag.Message.ReadCnNewExport,
				PropTag.Message.Fid,
				PropTag.Message.FidBin,
				PropTag.Message.Mid,
				PropTag.Message.MidBin,
				PropTag.Message.CategID,
				PropTag.Message.ParentCategID,
				PropTag.Message.InstanceId,
				PropTag.Message.InstanceNum,
				PropTag.Message.ChangeType,
				PropTag.Message.LTID,
				PropTag.Message.CnExport,
				PropTag.Message.PclExport,
				PropTag.Message.CnMvExport,
				PropTag.Message.MailboxGuid,
				PropTag.Message.MapiEntryIdGuidGuid,
				PropTag.Message.CnsetIn,
				PropTag.Message.CnsetSeen,
				PropTag.Message.ChangeNumber,
				PropTag.Message.ChangeNumberBin,
				PropTag.Message.PCL,
				PropTag.Message.CnMv,
				PropTag.Message.MailFlags,
				PropTag.Message.Associated,
				PropTag.Message.SubmitResponsibility,
				PropTag.Message.SenderCAI,
				PropTag.Message.SentRepresentingCAI,
				PropTag.Message.OriginalSenderCAI,
				PropTag.Message.OriginalSentRepresentingCAI,
				PropTag.Message.ReceivedByCAI,
				PropTag.Message.ReceivedRepresentingCAI,
				PropTag.Message.ReadReceiptCAI,
				PropTag.Message.ReportCAI,
				PropTag.Message.CreatorCAI,
				PropTag.Message.LastModifierCAI,
				PropTag.Message.CnsetRead,
				PropTag.Message.CnsetSeenFAI,
				PropTag.Message.IdSetDeleted,
				PropTag.Message.OriginatorCAI,
				PropTag.Message.ReportDestinationCAI,
				PropTag.Message.OriginalAuthorCAI,
				PropTag.Message.ReadCnNew,
				PropTag.Message.ReadCnNewBin,
				PropTag.Message.DocumentId,
				PropTag.Message.MailboxNum,
				PropTag.Message.ConversationIdHash,
				PropTag.Message.ConversationDocumentId
			};

			// Token: 0x0400140F RID: 5135
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[]
			{
				PropTag.Message.MessageToMe,
				PropTag.Message.MessageCCMe,
				PropTag.Message.MessageRecipMe,
				PropTag.Message.DisplayBcc,
				PropTag.Message.DisplayCc,
				PropTag.Message.DisplayTo,
				PropTag.Message.ParentDisplay,
				PropTag.Message.MessageFlags,
				PropTag.Message.MessageSize,
				PropTag.Message.MessageSize32,
				PropTag.Message.ParentEntryId,
				PropTag.Message.ParentEntryIdSvrEid,
				PropTag.Message.SubmitFlags,
				PropTag.Message.HasAttach,
				PropTag.Message.FullTextConversationIndex,
				PropTag.Message.Access,
				PropTag.Message.RowType,
				PropTag.Message.InstanceKey,
				PropTag.Message.InstanceKeySvrEid,
				PropTag.Message.AccessLevel,
				PropTag.Message.RecordKey,
				PropTag.Message.RecordKeySvrEid,
				PropTag.Message.EntryId,
				PropTag.Message.EntryIdSvrEid,
				PropTag.Message.NativeBodyInfo,
				PropTag.Message.NNTPXRef,
				PropTag.Message.Depth,
				PropTag.Message.ConversationId,
				PropTag.Message.ContentCount,
				PropTag.Message.UnreadCount,
				PropTag.Message.GVid,
				PropTag.Message.GDID,
				PropTag.Message.XVid,
				PropTag.Message.GDefVid,
				PropTag.Message.PreviewUnread,
				PropTag.Message.HasDAMs,
				PropTag.Message.DeferredSendNumber,
				PropTag.Message.InternetMessageIdHash,
				PropTag.Message.ConversationTopicHash,
				PropTag.Message.SourceKey,
				PropTag.Message.ParentSourceKey,
				PropTag.Message.ChangeKey,
				PropTag.Message.PredecessorChangeList,
				PropTag.Message.LISSD,
				PropTag.Message.HasNamedProperties,
				PropTag.Message.DeletedOn,
				PropTag.Message.LocalCommitTime,
				PropTag.Message.CategID,
				PropTag.Message.ParentCategID,
				PropTag.Message.InstanceId,
				PropTag.Message.InstanceNum,
				PropTag.Message.ChangeType,
				PropTag.Message.LTID,
				PropTag.Message.CnsetIn,
				PropTag.Message.ConversationIdHash
			};

			// Token: 0x04001410 RID: 5136
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x04001411 RID: 5137
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[]
			{
				PropTag.Message.BodyUnicode,
				PropTag.Message.RtfCompressed,
				PropTag.Message.BodyHtml,
				PropTag.Message.BodyHtmlUnicode
			};

			// Token: 0x04001412 RID: 5138
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[]
			{
				PropTag.Message.SenderCAI,
				PropTag.Message.SentRepresentingCAI,
				PropTag.Message.OriginalSenderCAI,
				PropTag.Message.OriginalSentRepresentingCAI,
				PropTag.Message.ReceivedByCAI,
				PropTag.Message.ReceivedRepresentingCAI,
				PropTag.Message.ReadReceiptCAI,
				PropTag.Message.ReportCAI,
				PropTag.Message.CreatorCAI,
				PropTag.Message.LastModifierCAI,
				PropTag.Message.OriginatorCAI,
				PropTag.Message.ReportDestinationCAI,
				PropTag.Message.OriginalAuthorCAI
			};

			// Token: 0x04001413 RID: 5139
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[]
			{
				PropTag.Message.DisplayBcc,
				PropTag.Message.DisplayCc,
				PropTag.Message.DisplayTo,
				PropTag.Message.MessageSize,
				PropTag.Message.MessageSize32,
				PropTag.Message.ParentEntryId,
				PropTag.Message.ParentEntryIdSvrEid,
				PropTag.Message.HasAttach,
				PropTag.Message.Access,
				PropTag.Message.RecordKey,
				PropTag.Message.RecordKeySvrEid,
				PropTag.Message.StoreRecordKey,
				PropTag.Message.StoreEntryId,
				PropTag.Message.ObjectType,
				PropTag.Message.EntryId,
				PropTag.Message.EntryIdSvrEid,
				PropTag.Message.InetNewsgroups,
				PropTag.Message.SMTPTempTblData,
				PropTag.Message.SMTPTempTblData2,
				PropTag.Message.SMTPTempTblData3,
				PropTag.Message.MailboxPartitionNumber,
				PropTag.Message.MailboxNumberInternal,
				PropTag.Message.VirtualParentDisplay,
				PropTag.Message.InternalConversationIndexTracking,
				PropTag.Message.InternalConversationIndex,
				PropTag.Message.ConversationItemConversationId,
				PropTag.Message.VirtualUnreadMessageCount,
				PropTag.Message.VirtualIsRead,
				PropTag.Message.IsReadColumn,
				PropTag.Message.Internal9ByteChangeNumber,
				PropTag.Message.Internal9ByteReadCnNew,
				PropTag.Message.CategoryHeaderLevelStub1,
				PropTag.Message.CategoryHeaderLevelStub2,
				PropTag.Message.CategoryHeaderLevelStub3,
				PropTag.Message.CategoryHeaderAggregateProp0,
				PropTag.Message.CategoryHeaderAggregateProp1,
				PropTag.Message.CategoryHeaderAggregateProp2,
				PropTag.Message.CategoryHeaderAggregateProp3,
				PropTag.Message.MessageFlagsActual,
				PropTag.Message.InternalChangeKey,
				PropTag.Message.InternalSourceKey,
				PropTag.Message.MimeSkeleton,
				PropTag.Message.ReplicaServer,
				PropTag.Message.DAMOriginalEntryId,
				PropTag.Message.HasNamedProperties,
				PropTag.Message.FidMid,
				PropTag.Message.InternetContent,
				PropTag.Message.OriginatorName,
				PropTag.Message.OriginatorEmailAddress,
				PropTag.Message.OriginatorAddressType,
				PropTag.Message.OriginatorEntryId,
				PropTag.Message.RecipientNumber,
				PropTag.Message.ReportDestinationName,
				PropTag.Message.ReportDestinationEntryId,
				PropTag.Message.ProhibitReceiveQuota,
				PropTag.Message.SearchAttachments,
				PropTag.Message.ProhibitSendQuota,
				PropTag.Message.SubmittedByAdmin,
				PropTag.Message.LongTermEntryIdFromTable,
				PropTag.Message.RuleIds,
				PropTag.Message.RuleMsgConditionOld,
				PropTag.Message.RuleMsgActionsOld,
				PropTag.Message.DeletedOn,
				PropTag.Message.CodePageId,
				PropTag.Message.UserDN,
				PropTag.Message.MailboxDSGuidGuid,
				PropTag.Message.URLName,
				PropTag.Message.LocalCommitTime,
				PropTag.Message.AutoReset,
				PropTag.Message.ELCAutoCopyTag,
				PropTag.Message.ELCMoveDate,
				PropTag.Message.PropGroupInfo,
				PropTag.Message.PropertyGroupChangeMask,
				PropTag.Message.ReadCnNewExport,
				PropTag.Message.SentMailSvrEID,
				PropTag.Message.SentMailSvrEIDBin,
				PropTag.Message.LocallyDelivered,
				PropTag.Message.MimeSize,
				PropTag.Message.MimeSize32,
				PropTag.Message.FileSize,
				PropTag.Message.FileSize32,
				PropTag.Message.Fid,
				PropTag.Message.FidBin,
				PropTag.Message.ParentFid,
				PropTag.Message.Mid,
				PropTag.Message.MidBin,
				PropTag.Message.CategID,
				PropTag.Message.ParentCategID,
				PropTag.Message.InstanceId,
				PropTag.Message.InstanceNum,
				PropTag.Message.ChangeType,
				PropTag.Message.RequiresRefResolve,
				PropTag.Message.LTID,
				PropTag.Message.CnExport,
				PropTag.Message.PclExport,
				PropTag.Message.CnMvExport,
				PropTag.Message.MailboxGuid,
				PropTag.Message.MapiEntryIdGuidGuid,
				PropTag.Message.ImapCachedBodystructure,
				PropTag.Message.StorageQuota,
				PropTag.Message.CnsetIn,
				PropTag.Message.CnsetSeen,
				PropTag.Message.ChangeNumber,
				PropTag.Message.ChangeNumberBin,
				PropTag.Message.PCL,
				PropTag.Message.CnMv,
				PropTag.Message.SourceEntryId,
				PropTag.Message.MailFlags,
				PropTag.Message.Associated,
				PropTag.Message.SubmitResponsibility,
				PropTag.Message.SharedReceiptHandling,
				PropTag.Message.Inid,
				PropTag.Message.MessageAttachList,
				PropTag.Message.SenderCAI,
				PropTag.Message.SentRepresentingCAI,
				PropTag.Message.OriginalSenderCAI,
				PropTag.Message.OriginalSentRepresentingCAI,
				PropTag.Message.ReceivedByCAI,
				PropTag.Message.ReceivedRepresentingCAI,
				PropTag.Message.ReadReceiptCAI,
				PropTag.Message.ReportCAI,
				PropTag.Message.CreatorCAI,
				PropTag.Message.LastModifierCAI,
				PropTag.Message.CnsetRead,
				PropTag.Message.CnsetSeenFAI,
				PropTag.Message.IdSetDeleted,
				PropTag.Message.OriginatorCAI,
				PropTag.Message.ReportDestinationCAI,
				PropTag.Message.OriginalAuthorCAI,
				PropTag.Message.ReadCnNew,
				PropTag.Message.ReadCnNewBin
			};

			// Token: 0x04001414 RID: 5140
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[]
			{
				PropTag.Message.BodyUnicode,
				PropTag.Message.RtfCompressed,
				PropTag.Message.BodyHtml,
				PropTag.Message.BodyHtmlUnicode,
				PropTag.Message.NativeBodyInfo,
				PropTag.Message.NativeBodyType,
				PropTag.Message.NativeBody,
				PropTag.Message.PreviewUnread,
				PropTag.Message.Preview
			};

			// Token: 0x04001415 RID: 5141
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x04001416 RID: 5142
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x04001417 RID: 5143
			public static readonly StorePropTag[] TestProperties = new StorePropTag[]
			{
				PropTag.Message.AcknowledgementMode,
				PropTag.Message.TestTest,
				PropTag.Message.AuthorizingUsers,
				PropTag.Message.AutoForwarded,
				PropTag.Message.ContentConfidentialityAlgorithmId,
				PropTag.Message.ContentCorrelator,
				PropTag.Message.ContentIdentifier,
				PropTag.Message.ContentLength,
				PropTag.Message.ContentReturnRequested,
				PropTag.Message.ConversationKey,
				PropTag.Message.ConversionEits,
				PropTag.Message.ConversionWithLossProhibited,
				PropTag.Message.ConvertedEits,
				PropTag.Message.DeferredDeliveryTime,
				PropTag.Message.DeliverTime,
				PropTag.Message.DiscardReason,
				PropTag.Message.DisclosureOfRecipients,
				PropTag.Message.DLExpansionHistory,
				PropTag.Message.DLExpansionProhibited,
				PropTag.Message.ExpiryTime,
				PropTag.Message.ImplicitConversionProhibited,
				PropTag.Message.Importance,
				PropTag.Message.ObsoletedIPMS,
				PropTag.Message.OriginallyIntendedRecipientName,
				PropTag.Message.OriginalEITS,
				PropTag.Message.OriginatorReturnAddress,
				PropTag.Message.ParentEntryId,
				PropTag.Message.ReportDestinationCAI,
				PropTag.Message.OriginalAuthorCAI
			};

			// Token: 0x04001418 RID: 5144
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.Message.AcknowledgementMode,
				PropTag.Message.TestTest,
				PropTag.Message.AlternateRecipientAllowed,
				PropTag.Message.AuthorizingUsers,
				PropTag.Message.AutoForwardComment,
				PropTag.Message.AutoForwarded,
				PropTag.Message.ContentConfidentialityAlgorithmId,
				PropTag.Message.ContentCorrelator,
				PropTag.Message.ContentIdentifier,
				PropTag.Message.ContentLength,
				PropTag.Message.ContentReturnRequested,
				PropTag.Message.ConversationKey,
				PropTag.Message.ConversionEits,
				PropTag.Message.ConversionWithLossProhibited,
				PropTag.Message.ConvertedEits,
				PropTag.Message.DeferredDeliveryTime,
				PropTag.Message.DeliverTime,
				PropTag.Message.DiscardReason,
				PropTag.Message.DisclosureOfRecipients,
				PropTag.Message.DLExpansionHistory,
				PropTag.Message.DLExpansionProhibited,
				PropTag.Message.ExpiryTime,
				PropTag.Message.ImplicitConversionProhibited,
				PropTag.Message.Importance,
				PropTag.Message.IPMID,
				PropTag.Message.LatestDeliveryTime,
				PropTag.Message.MessageClass,
				PropTag.Message.MessageDeliveryId,
				PropTag.Message.MessageSecurityLabel,
				PropTag.Message.ObsoletedIPMS,
				PropTag.Message.OriginallyIntendedRecipientName,
				PropTag.Message.OriginalEITS,
				PropTag.Message.OriginatorCertificate,
				PropTag.Message.DeliveryReportRequested,
				PropTag.Message.OriginatorReturnAddress,
				PropTag.Message.ParentKey,
				PropTag.Message.Priority,
				PropTag.Message.OriginCheck,
				PropTag.Message.ProofOfSubmissionRequested,
				PropTag.Message.ReadReceiptRequested,
				PropTag.Message.ReceiptTime,
				PropTag.Message.RecipientReassignmentProhibited,
				PropTag.Message.RedirectionHistory,
				PropTag.Message.RelatedIPMS,
				PropTag.Message.OriginalSensitivity,
				PropTag.Message.Languages,
				PropTag.Message.ReplyTime,
				PropTag.Message.ReportTag,
				PropTag.Message.ReportTime,
				PropTag.Message.ReturnedIPM,
				PropTag.Message.Security,
				PropTag.Message.IncompleteCopy,
				PropTag.Message.Sensitivity,
				PropTag.Message.Subject,
				PropTag.Message.SubjectIPM,
				PropTag.Message.ClientSubmitTime,
				PropTag.Message.ReportName,
				PropTag.Message.SentRepresentingSearchKey,
				PropTag.Message.X400ContentType,
				PropTag.Message.SubjectPrefix,
				PropTag.Message.NonReceiptReason,
				PropTag.Message.ReceivedByEntryId,
				PropTag.Message.ReceivedByName,
				PropTag.Message.SentRepresentingEntryId,
				PropTag.Message.SentRepresentingName,
				PropTag.Message.ReceivedRepresentingEntryId,
				PropTag.Message.ReceivedRepresentingName,
				PropTag.Message.ReportEntryId,
				PropTag.Message.ReadReceiptEntryId,
				PropTag.Message.MessageSubmissionId,
				PropTag.Message.ProviderSubmitTime,
				PropTag.Message.OriginalSubject,
				PropTag.Message.DiscVal,
				PropTag.Message.OriginalMessageClass,
				PropTag.Message.OriginalAuthorEntryId,
				PropTag.Message.OriginalAuthorName,
				PropTag.Message.OriginalSubmitTime,
				PropTag.Message.ReplyRecipientEntries,
				PropTag.Message.ReplyRecipientNames,
				PropTag.Message.ReceivedBySearchKey,
				PropTag.Message.ReceivedRepresentingSearchKey,
				PropTag.Message.ReadReceiptSearchKey,
				PropTag.Message.ReportSearchKey,
				PropTag.Message.OriginalDeliveryTime,
				PropTag.Message.OriginalAuthorSearchKey,
				PropTag.Message.MessageToMe,
				PropTag.Message.MessageCCMe,
				PropTag.Message.MessageRecipMe,
				PropTag.Message.OriginalSenderName,
				PropTag.Message.OriginalSenderEntryId,
				PropTag.Message.OriginalSenderSearchKey,
				PropTag.Message.OriginalSentRepresentingName,
				PropTag.Message.OriginalSentRepresentingEntryId,
				PropTag.Message.OriginalSentRepresentingSearchKey,
				PropTag.Message.StartDate,
				PropTag.Message.EndDate,
				PropTag.Message.OwnerApptId,
				PropTag.Message.ResponseRequested,
				PropTag.Message.SentRepresentingAddressType,
				PropTag.Message.SentRepresentingEmailAddress,
				PropTag.Message.OriginalSenderAddressType,
				PropTag.Message.OriginalSenderEmailAddress,
				PropTag.Message.OriginalSentRepresentingAddressType,
				PropTag.Message.OriginalSentRepresentingEmailAddress,
				PropTag.Message.ConversationTopic,
				PropTag.Message.ConversationIndex,
				PropTag.Message.OriginalDisplayBcc,
				PropTag.Message.OriginalDisplayCc,
				PropTag.Message.OriginalDisplayTo,
				PropTag.Message.ReceivedByAddressType,
				PropTag.Message.ReceivedByEmailAddress,
				PropTag.Message.ReceivedRepresentingAddressType,
				PropTag.Message.ReceivedRepresentingEmailAddress,
				PropTag.Message.OriginalAuthorAddressType,
				PropTag.Message.OriginalAuthorEmailAddress,
				PropTag.Message.OriginallyIntendedRecipientAddressType,
				PropTag.Message.TransportMessageHeaders,
				PropTag.Message.Delegation,
				PropTag.Message.ReportDisposition,
				PropTag.Message.ReportDispositionMode,
				PropTag.Message.ReportOriginalSender,
				PropTag.Message.ReportDispositionToNames,
				PropTag.Message.ReportDispositionToEmailAddress,
				PropTag.Message.ReportDispositionOptions,
				PropTag.Message.RichContent,
				PropTag.Message.AdministratorEMail,
				PropTag.Message.ContentIntegrityCheck,
				PropTag.Message.ExplicitConversion,
				PropTag.Message.ReturnRequested,
				PropTag.Message.MessageToken,
				PropTag.Message.NDRReasonCode,
				PropTag.Message.NDRDiagCode,
				PropTag.Message.NonReceiptNotificationRequested,
				PropTag.Message.DeliveryPoint,
				PropTag.Message.NonDeliveryReportRequested,
				PropTag.Message.OriginatorRequestedAlterateRecipient,
				PropTag.Message.PhysicalDeliveryBureauFaxDelivery,
				PropTag.Message.PhysicalDeliveryMode,
				PropTag.Message.PhysicalDeliveryReportRequest,
				PropTag.Message.PhysicalForwardingAddress,
				PropTag.Message.PhysicalForwardingAddressRequested,
				PropTag.Message.PhysicalForwardingProhibited,
				PropTag.Message.ProofOfDelivery,
				PropTag.Message.ProofOfDeliveryRequested,
				PropTag.Message.RecipientCertificate,
				PropTag.Message.RecipientNumberForAdvice,
				PropTag.Message.RecipientType,
				PropTag.Message.RegisteredMailType,
				PropTag.Message.ReplyRequested,
				PropTag.Message.RequestedDeliveryMethod,
				PropTag.Message.SenderEntryId,
				PropTag.Message.SenderName,
				PropTag.Message.SupplementaryInfo,
				PropTag.Message.TypeOfMTSUser,
				PropTag.Message.SenderSearchKey,
				PropTag.Message.SenderAddressType,
				PropTag.Message.SenderEmailAddress,
				PropTag.Message.ParticipantSID,
				PropTag.Message.ParticipantGuid,
				PropTag.Message.ToGroupExpansionRecipients,
				PropTag.Message.CcGroupExpansionRecipients,
				PropTag.Message.BccGroupExpansionRecipients,
				PropTag.Message.CurrentVersion,
				PropTag.Message.DeleteAfterSubmit,
				PropTag.Message.DisplayBcc,
				PropTag.Message.DisplayCc,
				PropTag.Message.DisplayTo,
				PropTag.Message.ParentDisplay,
				PropTag.Message.MessageDeliveryTime,
				PropTag.Message.MessageFlags,
				PropTag.Message.MessageSize,
				PropTag.Message.MessageSize32,
				PropTag.Message.ParentEntryId,
				PropTag.Message.ParentEntryIdSvrEid,
				PropTag.Message.SentMailEntryId,
				PropTag.Message.Correlate,
				PropTag.Message.CorrelateMTSID,
				PropTag.Message.DiscreteValues,
				PropTag.Message.Responsibility,
				PropTag.Message.SpoolerStatus,
				PropTag.Message.TransportStatus,
				PropTag.Message.MessageRecipients,
				PropTag.Message.MessageRecipientsMVBin,
				PropTag.Message.MessageAttachments,
				PropTag.Message.ItemSubobjectsBin,
				PropTag.Message.SubmitFlags,
				PropTag.Message.RecipientStatus,
				PropTag.Message.TransportKey,
				PropTag.Message.MsgStatus,
				PropTag.Message.CreationVersion,
				PropTag.Message.ModifyVersion,
				PropTag.Message.HasAttach,
				PropTag.Message.BodyCRC,
				PropTag.Message.NormalizedSubject,
				PropTag.Message.RTFInSync,
				PropTag.Message.Preprocess,
				PropTag.Message.InternetArticleNumber,
				PropTag.Message.OriginatingMTACertificate,
				PropTag.Message.ProofOfSubmission,
				PropTag.Message.NTSecurityDescriptor,
				PropTag.Message.PrimarySendAccount,
				PropTag.Message.NextSendAccount,
				PropTag.Message.TodoItemFlags,
				PropTag.Message.SwappedTODOStore,
				PropTag.Message.SwappedTODOData,
				PropTag.Message.IMAPId,
				PropTag.Message.OriginalSourceServerVersion,
				PropTag.Message.ReplFlags,
				PropTag.Message.MessageDeepAttachments,
				PropTag.Message.SenderGuid,
				PropTag.Message.SentRepresentingGuid,
				PropTag.Message.OriginalSenderGuid,
				PropTag.Message.OriginalSentRepresentingGuid,
				PropTag.Message.ReadReceiptGuid,
				PropTag.Message.ReportGuid,
				PropTag.Message.OriginatorGuid,
				PropTag.Message.ReportDestinationGuid,
				PropTag.Message.OriginalAuthorGuid,
				PropTag.Message.ReceivedByGuid,
				PropTag.Message.ReceivedRepresentingGuid,
				PropTag.Message.CreatorGuid,
				PropTag.Message.LastModifierGuid,
				PropTag.Message.SenderSID,
				PropTag.Message.SentRepresentingSID,
				PropTag.Message.OriginalSenderSid,
				PropTag.Message.OriginalSentRepresentingSid,
				PropTag.Message.ReadReceiptSid,
				PropTag.Message.ReportSid,
				PropTag.Message.OriginatorSid,
				PropTag.Message.ReportDestinationSid,
				PropTag.Message.OriginalAuthorSid,
				PropTag.Message.RcvdBySid,
				PropTag.Message.RcvdRepresentingSid,
				PropTag.Message.CreatorSID,
				PropTag.Message.LastModifierSid,
				PropTag.Message.RecipientCAI,
				PropTag.Message.ConversationCreatorSID,
				PropTag.Message.URLCompNamePostfix,
				PropTag.Message.URLCompNameSet,
				PropTag.Message.Read,
				PropTag.Message.CreatorSidAsXML,
				PropTag.Message.LastModifierSidAsXML,
				PropTag.Message.SenderSIDAsXML,
				PropTag.Message.SentRepresentingSidAsXML,
				PropTag.Message.OriginalSenderSIDAsXML,
				PropTag.Message.OriginalSentRepresentingSIDAsXML,
				PropTag.Message.ReadReceiptSIDAsXML,
				PropTag.Message.ReportSIDAsXML,
				PropTag.Message.OriginatorSidAsXML,
				PropTag.Message.ReportDestinationSIDAsXML,
				PropTag.Message.OriginalAuthorSIDAsXML,
				PropTag.Message.ReceivedBySIDAsXML,
				PropTag.Message.ReceivedRepersentingSIDAsXML,
				PropTag.Message.TrustSender,
				PropTag.Message.SenderSMTPAddress,
				PropTag.Message.SentRepresentingSMTPAddress,
				PropTag.Message.OriginalSenderSMTPAddress,
				PropTag.Message.OriginalSentRepresentingSMTPAddress,
				PropTag.Message.ReadReceiptSMTPAddress,
				PropTag.Message.ReportSMTPAddress,
				PropTag.Message.OriginatorSMTPAddress,
				PropTag.Message.ReportDestinationSMTPAddress,
				PropTag.Message.OriginalAuthorSMTPAddress,
				PropTag.Message.ReceivedBySMTPAddress,
				PropTag.Message.ReceivedRepresentingSMTPAddress,
				PropTag.Message.CreatorSMTPAddress,
				PropTag.Message.LastModifierSMTPAddress,
				PropTag.Message.VirusScannerStamp,
				PropTag.Message.VirusTransportStamp,
				PropTag.Message.AddrTo,
				PropTag.Message.AddrCc,
				PropTag.Message.ExtendedRuleActions,
				PropTag.Message.ExtendedRuleCondition,
				PropTag.Message.EntourageSentHistory,
				PropTag.Message.ProofInProgress,
				PropTag.Message.SearchAttachmentsOLK,
				PropTag.Message.SearchRecipEmailTo,
				PropTag.Message.SearchRecipEmailCc,
				PropTag.Message.SearchRecipEmailBcc,
				PropTag.Message.SFGAOFlags,
				PropTag.Message.SearchFullTextSubject,
				PropTag.Message.SearchFullTextBody,
				PropTag.Message.FullTextConversationIndex,
				PropTag.Message.SearchAllIndexedProps,
				PropTag.Message.SearchRecipients,
				PropTag.Message.SearchRecipientsTo,
				PropTag.Message.SearchRecipientsCc,
				PropTag.Message.SearchRecipientsBcc,
				PropTag.Message.SearchAccountTo,
				PropTag.Message.SearchAccountCc,
				PropTag.Message.SearchAccountBcc,
				PropTag.Message.SearchEmailAddressTo,
				PropTag.Message.SearchEmailAddressCc,
				PropTag.Message.SearchEmailAddressBcc,
				PropTag.Message.SearchSmtpAddressTo,
				PropTag.Message.SearchSmtpAddressCc,
				PropTag.Message.SearchSmtpAddressBcc,
				PropTag.Message.SearchSender,
				PropTag.Message.IsIRMMessage,
				PropTag.Message.SearchIsPartiallyIndexed,
				PropTag.Message.RenewTime,
				PropTag.Message.DeliveryOrRenewTime,
				PropTag.Message.ConversationFamilyId,
				PropTag.Message.LikeCount,
				PropTag.Message.RichContentDeprecated,
				PropTag.Message.PeopleCentricConversationId,
				PropTag.Message.DiscoveryAnnotation,
				PropTag.Message.Access,
				PropTag.Message.RowType,
				PropTag.Message.InstanceKey,
				PropTag.Message.InstanceKeySvrEid,
				PropTag.Message.AccessLevel,
				PropTag.Message.MappingSignature,
				PropTag.Message.RecordKey,
				PropTag.Message.RecordKeySvrEid,
				PropTag.Message.StoreRecordKey,
				PropTag.Message.StoreEntryId,
				PropTag.Message.MiniIcon,
				PropTag.Message.Icon,
				PropTag.Message.ObjectType,
				PropTag.Message.EntryId,
				PropTag.Message.EntryIdSvrEid,
				PropTag.Message.BodyUnicode,
				PropTag.Message.ReportText,
				PropTag.Message.OriginatorAndDLExpansionHistory,
				PropTag.Message.ReportingDLName,
				PropTag.Message.ReportingMTACertificate,
				PropTag.Message.RtfSyncBodyCrc,
				PropTag.Message.RtfSyncBodyCount,
				PropTag.Message.RtfSyncBodyTag,
				PropTag.Message.RtfCompressed,
				PropTag.Message.AlternateBestBody,
				PropTag.Message.RtfSyncPrefixCount,
				PropTag.Message.RtfSyncTrailingCount,
				PropTag.Message.OriginallyIntendedRecipientEntryId,
				PropTag.Message.BodyHtml,
				PropTag.Message.BodyHtmlUnicode,
				PropTag.Message.BodyContentLocation,
				PropTag.Message.BodyContentId,
				PropTag.Message.NativeBodyInfo,
				PropTag.Message.NativeBodyType,
				PropTag.Message.NativeBody,
				PropTag.Message.AnnotationToken,
				PropTag.Message.InternetApproved,
				PropTag.Message.InternetFollowupTo,
				PropTag.Message.InternetMessageId,
				PropTag.Message.InetNewsgroups,
				PropTag.Message.InternetReferences,
				PropTag.Message.PostReplyFolderEntries,
				PropTag.Message.NNTPXRef,
				PropTag.Message.InReplyToId,
				PropTag.Message.OriginalInternetMessageId,
				PropTag.Message.IconIndex,
				PropTag.Message.LastVerbExecuted,
				PropTag.Message.LastVerbExecutionTime,
				PropTag.Message.Relevance,
				PropTag.Message.FlagStatus,
				PropTag.Message.FlagCompleteTime,
				PropTag.Message.FormatPT,
				PropTag.Message.FollowupIcon,
				PropTag.Message.BlockStatus,
				PropTag.Message.ItemTempFlags,
				PropTag.Message.SMTPTempTblData,
				PropTag.Message.SMTPTempTblData2,
				PropTag.Message.SMTPTempTblData3,
				PropTag.Message.DAVSubmitData,
				PropTag.Message.ImapCachedMsgSize,
				PropTag.Message.DisableFullFidelity,
				PropTag.Message.URLCompName,
				PropTag.Message.AttrHidden,
				PropTag.Message.AttrSystem,
				PropTag.Message.AttrReadOnly,
				PropTag.Message.PredictedActions,
				PropTag.Message.GroupingActions,
				PropTag.Message.PredictedActionsSummary,
				PropTag.Message.PredictedActionsThresholds,
				PropTag.Message.IsClutter,
				PropTag.Message.InferencePredictedReplyForwardReasons,
				PropTag.Message.InferencePredictedDeleteReasons,
				PropTag.Message.InferencePredictedIgnoreReasons,
				PropTag.Message.OriginalDeliveryFolderInfo,
				PropTag.Message.RowId,
				PropTag.Message.DisplayName,
				PropTag.Message.AddressType,
				PropTag.Message.EmailAddress,
				PropTag.Message.Comment,
				PropTag.Message.Depth,
				PropTag.Message.CreationTime,
				PropTag.Message.LastModificationTime,
				PropTag.Message.SearchKey,
				PropTag.Message.SearchKeySvrEid,
				PropTag.Message.TargetEntryId,
				PropTag.Message.ConversationId,
				PropTag.Message.BodyTag,
				PropTag.Message.ConversationIndexTrackingObsolete,
				PropTag.Message.ConversationIndexTracking,
				PropTag.Message.ArchiveTag,
				PropTag.Message.PolicyTag,
				PropTag.Message.RetentionPeriod,
				PropTag.Message.StartDateEtc,
				PropTag.Message.RetentionDate,
				PropTag.Message.RetentionFlags,
				PropTag.Message.ArchivePeriod,
				PropTag.Message.ArchiveDate,
				PropTag.Message.FormVersion,
				PropTag.Message.FormCLSID,
				PropTag.Message.FormContactName,
				PropTag.Message.FormCategory,
				PropTag.Message.FormCategorySub,
				PropTag.Message.FormHidden,
				PropTag.Message.FormDesignerName,
				PropTag.Message.FormDesignerGuid,
				PropTag.Message.FormMessageBehavior,
				PropTag.Message.StoreSupportMask,
				PropTag.Message.MdbProvider,
				PropTag.Message.EventEmailReminderTimer,
				PropTag.Message.ContentCount,
				PropTag.Message.UnreadCount,
				PropTag.Message.UnreadCountInt64,
				PropTag.Message.DetailsTable,
				PropTag.Message.AppointmentColorName,
				PropTag.Message.ContentId,
				PropTag.Message.MimeUrl,
				PropTag.Message.DisplayType,
				PropTag.Message.SmtpAddress,
				PropTag.Message.SimpleDisplayName,
				PropTag.Message.Account,
				PropTag.Message.AlternateRecipient,
				PropTag.Message.CallbackTelephoneNumber,
				PropTag.Message.ConversionProhibited,
				PropTag.Message.Generation,
				PropTag.Message.GivenName,
				PropTag.Message.GovernmentIDNumber,
				PropTag.Message.BusinessTelephoneNumber,
				PropTag.Message.HomeTelephoneNumber,
				PropTag.Message.Initials,
				PropTag.Message.Keyword,
				PropTag.Message.Language,
				PropTag.Message.Location,
				PropTag.Message.MailPermission,
				PropTag.Message.MHSCommonName,
				PropTag.Message.OrganizationalIDNumber,
				PropTag.Message.SurName,
				PropTag.Message.OriginalEntryId,
				PropTag.Message.OriginalDisplayName,
				PropTag.Message.OriginalSearchKey,
				PropTag.Message.PostalAddress,
				PropTag.Message.CompanyName,
				PropTag.Message.Title,
				PropTag.Message.DepartmentName,
				PropTag.Message.OfficeLocation,
				PropTag.Message.PrimaryTelephoneNumber,
				PropTag.Message.Business2TelephoneNumber,
				PropTag.Message.Business2TelephoneNumberMv,
				PropTag.Message.MobileTelephoneNumber,
				PropTag.Message.RadioTelephoneNumber,
				PropTag.Message.CarTelephoneNumber,
				PropTag.Message.OtherTelephoneNumber,
				PropTag.Message.TransmitableDisplayName,
				PropTag.Message.PagerTelephoneNumber,
				PropTag.Message.UserCertificate,
				PropTag.Message.PrimaryFaxNumber,
				PropTag.Message.BusinessFaxNumber,
				PropTag.Message.HomeFaxNumber,
				PropTag.Message.Country,
				PropTag.Message.Locality,
				PropTag.Message.StateOrProvince,
				PropTag.Message.StreetAddress,
				PropTag.Message.PostalCode,
				PropTag.Message.PostOfficeBox,
				PropTag.Message.TelexNumber,
				PropTag.Message.ISDNNumber,
				PropTag.Message.AssistantTelephoneNumber,
				PropTag.Message.Home2TelephoneNumber,
				PropTag.Message.Home2TelephoneNumberMv,
				PropTag.Message.Assistant,
				PropTag.Message.SendRichInfo,
				PropTag.Message.WeddingAnniversary,
				PropTag.Message.Birthday,
				PropTag.Message.Hobbies,
				PropTag.Message.MiddleName,
				PropTag.Message.DisplayNamePrefix,
				PropTag.Message.Profession,
				PropTag.Message.ReferredByName,
				PropTag.Message.SpouseName,
				PropTag.Message.ComputerNetworkName,
				PropTag.Message.CustomerId,
				PropTag.Message.TTYTDDPhoneNumber,
				PropTag.Message.FTPSite,
				PropTag.Message.Gender,
				PropTag.Message.ManagerName,
				PropTag.Message.NickName,
				PropTag.Message.PersonalHomePage,
				PropTag.Message.BusinessHomePage,
				PropTag.Message.ContactVersion,
				PropTag.Message.ContactEntryIds,
				PropTag.Message.ContactAddressTypes,
				PropTag.Message.ContactDefaultAddressIndex,
				PropTag.Message.ContactEmailAddress,
				PropTag.Message.CompanyMainPhoneNumber,
				PropTag.Message.ChildrensNames,
				PropTag.Message.HomeAddressCity,
				PropTag.Message.HomeAddressCountry,
				PropTag.Message.HomeAddressPostalCode,
				PropTag.Message.HomeAddressStateOrProvince,
				PropTag.Message.HomeAddressStreet,
				PropTag.Message.HomeAddressPostOfficeBox,
				PropTag.Message.OtherAddressCity,
				PropTag.Message.OtherAddressCountry,
				PropTag.Message.OtherAddressPostalCode,
				PropTag.Message.OtherAddressStateOrProvince,
				PropTag.Message.OtherAddressStreet,
				PropTag.Message.OtherAddressPostOfficeBox,
				PropTag.Message.UserX509CertificateABSearchPath,
				PropTag.Message.SendInternetEncoding,
				PropTag.Message.PartnerNetworkId,
				PropTag.Message.PartnerNetworkUserId,
				PropTag.Message.PartnerNetworkThumbnailPhotoUrl,
				PropTag.Message.PartnerNetworkProfilePhotoUrl,
				PropTag.Message.PartnerNetworkContactType,
				PropTag.Message.RelevanceScore,
				PropTag.Message.IsDistributionListContact,
				PropTag.Message.IsPromotedContact,
				PropTag.Message.OrgUnitName,
				PropTag.Message.OrganizationName,
				PropTag.Message.TestBlobProperty,
				PropTag.Message.FilteringHooks,
				PropTag.Message.MailboxPartitionNumber,
				PropTag.Message.MailboxNumberInternal,
				PropTag.Message.VirtualParentDisplay,
				PropTag.Message.InternalConversationIndexTracking,
				PropTag.Message.InternalConversationIndex,
				PropTag.Message.ConversationItemConversationId,
				PropTag.Message.VirtualUnreadMessageCount,
				PropTag.Message.VirtualIsRead,
				PropTag.Message.IsReadColumn,
				PropTag.Message.Internal9ByteChangeNumber,
				PropTag.Message.Internal9ByteReadCnNew,
				PropTag.Message.CategoryHeaderLevelStub1,
				PropTag.Message.CategoryHeaderLevelStub2,
				PropTag.Message.CategoryHeaderLevelStub3,
				PropTag.Message.CategoryHeaderAggregateProp0,
				PropTag.Message.CategoryHeaderAggregateProp1,
				PropTag.Message.CategoryHeaderAggregateProp2,
				PropTag.Message.CategoryHeaderAggregateProp3,
				PropTag.Message.MessageFlagsActual,
				PropTag.Message.InternalChangeKey,
				PropTag.Message.InternalSourceKey,
				PropTag.Message.HeaderFolderEntryId,
				PropTag.Message.RemoteProgress,
				PropTag.Message.RemoteProgressText,
				PropTag.Message.VID,
				PropTag.Message.GVid,
				PropTag.Message.GDID,
				PropTag.Message.XVid,
				PropTag.Message.GDefVid,
				PropTag.Message.PrimaryMailboxOverQuota,
				PropTag.Message.InternalPostReply,
				PropTag.Message.PreviewUnread,
				PropTag.Message.Preview,
				PropTag.Message.InternetCPID,
				PropTag.Message.AutoResponseSuppress,
				PropTag.Message.HasDAMs,
				PropTag.Message.DeferredSendNumber,
				PropTag.Message.DeferredSendUnits,
				PropTag.Message.ExpiryNumber,
				PropTag.Message.ExpiryUnits,
				PropTag.Message.DeferredSendTime,
				PropTag.Message.MessageLocaleId,
				PropTag.Message.RuleTriggerHistory,
				PropTag.Message.MoveToStoreEid,
				PropTag.Message.MoveToFolderEid,
				PropTag.Message.StorageQuotaLimit,
				PropTag.Message.ExcessStorageUsed,
				PropTag.Message.ServerGeneratingQuotaMsg,
				PropTag.Message.CreatorName,
				PropTag.Message.CreatorEntryId,
				PropTag.Message.LastModifierName,
				PropTag.Message.LastModifierEntryId,
				PropTag.Message.MessageCodePage,
				PropTag.Message.QuotaType,
				PropTag.Message.IsPublicFolderQuotaMessage,
				PropTag.Message.NewAttach,
				PropTag.Message.StartEmbed,
				PropTag.Message.EndEmbed,
				PropTag.Message.StartRecip,
				PropTag.Message.EndRecip,
				PropTag.Message.EndCcRecip,
				PropTag.Message.EndBccRecip,
				PropTag.Message.EndP1Recip,
				PropTag.Message.DNPrefix,
				PropTag.Message.StartTopFolder,
				PropTag.Message.StartSubFolder,
				PropTag.Message.EndFolder,
				PropTag.Message.StartMessage,
				PropTag.Message.EndMessage,
				PropTag.Message.EndAttach,
				PropTag.Message.EcWarning,
				PropTag.Message.StartFAIMessage,
				PropTag.Message.NewFXFolder,
				PropTag.Message.IncrSyncChange,
				PropTag.Message.IncrSyncDelete,
				PropTag.Message.IncrSyncEnd,
				PropTag.Message.IncrSyncMessage,
				PropTag.Message.FastTransferDelProp,
				PropTag.Message.IdsetGiven,
				PropTag.Message.IdsetGivenInt32,
				PropTag.Message.FastTransferErrorInfo,
				PropTag.Message.SenderFlags,
				PropTag.Message.SentRepresentingFlags,
				PropTag.Message.RcvdByFlags,
				PropTag.Message.RcvdRepresentingFlags,
				PropTag.Message.OriginalSenderFlags,
				PropTag.Message.OriginalSentRepresentingFlags,
				PropTag.Message.ReportFlags,
				PropTag.Message.ReadReceiptFlags,
				PropTag.Message.SoftDeletes,
				PropTag.Message.CreatorAddressType,
				PropTag.Message.CreatorEmailAddr,
				PropTag.Message.LastModifierAddressType,
				PropTag.Message.LastModifierEmailAddr,
				PropTag.Message.ReportAddressType,
				PropTag.Message.ReportEmailAddress,
				PropTag.Message.ReportDisplayName,
				PropTag.Message.ReadReceiptAddressType,
				PropTag.Message.ReadReceiptEmailAddress,
				PropTag.Message.ReadReceiptDisplayName,
				PropTag.Message.IdsetRead,
				PropTag.Message.IdsetUnread,
				PropTag.Message.IncrSyncRead,
				PropTag.Message.SenderSimpleDisplayName,
				PropTag.Message.SentRepresentingSimpleDisplayName,
				PropTag.Message.OriginalSenderSimpleDisplayName,
				PropTag.Message.OriginalSentRepresentingSimpleDisplayName,
				PropTag.Message.ReceivedBySimpleDisplayName,
				PropTag.Message.ReceivedRepresentingSimpleDisplayName,
				PropTag.Message.ReadReceiptSimpleDisplayName,
				PropTag.Message.ReportSimpleDisplayName,
				PropTag.Message.CreatorSimpleDisplayName,
				PropTag.Message.LastModifierSimpleDisplayName,
				PropTag.Message.IncrSyncStateBegin,
				PropTag.Message.IncrSyncStateEnd,
				PropTag.Message.IncrSyncImailStream,
				PropTag.Message.SenderOrgAddressType,
				PropTag.Message.SenderOrgEmailAddr,
				PropTag.Message.SentRepresentingOrgAddressType,
				PropTag.Message.SentRepresentingOrgEmailAddr,
				PropTag.Message.OriginalSenderOrgAddressType,
				PropTag.Message.OriginalSenderOrgEmailAddr,
				PropTag.Message.OriginalSentRepresentingOrgAddressType,
				PropTag.Message.OriginalSentRepresentingOrgEmailAddr,
				PropTag.Message.RcvdByOrgAddressType,
				PropTag.Message.RcvdByOrgEmailAddr,
				PropTag.Message.RcvdRepresentingOrgAddressType,
				PropTag.Message.RcvdRepresentingOrgEmailAddr,
				PropTag.Message.ReadReceiptOrgAddressType,
				PropTag.Message.ReadReceiptOrgEmailAddr,
				PropTag.Message.ReportOrgAddressType,
				PropTag.Message.ReportOrgEmailAddr,
				PropTag.Message.CreatorOrgAddressType,
				PropTag.Message.CreatorOrgEmailAddr,
				PropTag.Message.LastModifierOrgAddressType,
				PropTag.Message.LastModifierOrgEmailAddr,
				PropTag.Message.OriginatorOrgAddressType,
				PropTag.Message.OriginatorOrgEmailAddr,
				PropTag.Message.ReportDestinationOrgEmailType,
				PropTag.Message.ReportDestinationOrgEmailAddr,
				PropTag.Message.OriginalAuthorOrgAddressType,
				PropTag.Message.OriginalAuthorOrgEmailAddr,
				PropTag.Message.CreatorFlags,
				PropTag.Message.LastModifierFlags,
				PropTag.Message.OriginatorFlags,
				PropTag.Message.ReportDestinationFlags,
				PropTag.Message.OriginalAuthorFlags,
				PropTag.Message.OriginatorSimpleDisplayName,
				PropTag.Message.ReportDestinationSimpleDisplayName,
				PropTag.Message.OriginalAuthorSimpleDispName,
				PropTag.Message.OriginatorSearchKey,
				PropTag.Message.ReportDestinationAddressType,
				PropTag.Message.ReportDestinationEmailAddress,
				PropTag.Message.ReportDestinationSearchKey,
				PropTag.Message.IncrSyncImailStreamContinue,
				PropTag.Message.IncrSyncImailStreamCancel,
				PropTag.Message.IncrSyncImailStream2Continue,
				PropTag.Message.IncrSyncProgressMode,
				PropTag.Message.SyncProgressPerMsg,
				PropTag.Message.ContentFilterSCL,
				PropTag.Message.IncrSyncMsgPartial,
				PropTag.Message.IncrSyncGroupInfo,
				PropTag.Message.IncrSyncGroupId,
				PropTag.Message.IncrSyncChangePartial,
				PropTag.Message.ContentFilterPCL,
				PropTag.Message.DeliverAsRead,
				PropTag.Message.InetMailOverrideFormat,
				PropTag.Message.MessageEditorFormat,
				PropTag.Message.SenderSMTPAddressXSO,
				PropTag.Message.SentRepresentingSMTPAddressXSO,
				PropTag.Message.OriginalSenderSMTPAddressXSO,
				PropTag.Message.OriginalSentRepresentingSMTPAddressXSO,
				PropTag.Message.ReadReceiptSMTPAddressXSO,
				PropTag.Message.OriginalAuthorSMTPAddressXSO,
				PropTag.Message.ReceivedBySMTPAddressXSO,
				PropTag.Message.ReceivedRepresentingSMTPAddressXSO,
				PropTag.Message.RecipientOrder,
				PropTag.Message.RecipientSipUri,
				PropTag.Message.RecipientDisplayName,
				PropTag.Message.RecipientEntryId,
				PropTag.Message.RecipientFlags,
				PropTag.Message.RecipientTrackStatus,
				PropTag.Message.DotStuffState,
				PropTag.Message.InternetMessageIdHash,
				PropTag.Message.ConversationTopicHash,
				PropTag.Message.MimeSkeleton,
				PropTag.Message.ReplyTemplateId,
				PropTag.Message.SecureSubmitFlags,
				PropTag.Message.SourceKey,
				PropTag.Message.ParentSourceKey,
				PropTag.Message.ChangeKey,
				PropTag.Message.PredecessorChangeList,
				PropTag.Message.RuleMsgState,
				PropTag.Message.RuleMsgUserFlags,
				PropTag.Message.RuleMsgProvider,
				PropTag.Message.RuleMsgName,
				PropTag.Message.RuleMsgLevel,
				PropTag.Message.RuleMsgProviderData,
				PropTag.Message.RuleMsgActions,
				PropTag.Message.RuleMsgCondition,
				PropTag.Message.RuleMsgConditionLCID,
				PropTag.Message.RuleMsgVersion,
				PropTag.Message.RuleMsgSequence,
				PropTag.Message.LISSD,
				PropTag.Message.ReplicaServer,
				PropTag.Message.DAMOriginalEntryId,
				PropTag.Message.HasNamedProperties,
				PropTag.Message.FidMid,
				PropTag.Message.InternetContent,
				PropTag.Message.OriginatorName,
				PropTag.Message.OriginatorEmailAddress,
				PropTag.Message.OriginatorAddressType,
				PropTag.Message.OriginatorEntryId,
				PropTag.Message.RecipientNumber,
				PropTag.Message.ReportDestinationName,
				PropTag.Message.ReportDestinationEntryId,
				PropTag.Message.ProhibitReceiveQuota,
				PropTag.Message.SearchAttachments,
				PropTag.Message.ProhibitSendQuota,
				PropTag.Message.SubmittedByAdmin,
				PropTag.Message.LongTermEntryIdFromTable,
				PropTag.Message.RuleIds,
				PropTag.Message.RuleMsgConditionOld,
				PropTag.Message.RuleMsgActionsOld,
				PropTag.Message.DeletedOn,
				PropTag.Message.CodePageId,
				PropTag.Message.UserDN,
				PropTag.Message.MailboxDSGuidGuid,
				PropTag.Message.URLName,
				PropTag.Message.LocalCommitTime,
				PropTag.Message.AutoReset,
				PropTag.Message.ELCAutoCopyTag,
				PropTag.Message.ELCMoveDate,
				PropTag.Message.PropGroupInfo,
				PropTag.Message.PropertyGroupChangeMask,
				PropTag.Message.ReadCnNewExport,
				PropTag.Message.SentMailSvrEID,
				PropTag.Message.SentMailSvrEIDBin,
				PropTag.Message.LocallyDelivered,
				PropTag.Message.MimeSize,
				PropTag.Message.MimeSize32,
				PropTag.Message.FileSize,
				PropTag.Message.FileSize32,
				PropTag.Message.Fid,
				PropTag.Message.FidBin,
				PropTag.Message.ParentFid,
				PropTag.Message.Mid,
				PropTag.Message.MidBin,
				PropTag.Message.CategID,
				PropTag.Message.ParentCategID,
				PropTag.Message.InstanceId,
				PropTag.Message.InstanceNum,
				PropTag.Message.ChangeType,
				PropTag.Message.RequiresRefResolve,
				PropTag.Message.LTID,
				PropTag.Message.CnExport,
				PropTag.Message.PclExport,
				PropTag.Message.CnMvExport,
				PropTag.Message.MailboxGuid,
				PropTag.Message.MapiEntryIdGuidGuid,
				PropTag.Message.ImapCachedBodystructure,
				PropTag.Message.StorageQuota,
				PropTag.Message.CnsetIn,
				PropTag.Message.CnsetSeen,
				PropTag.Message.ChangeNumber,
				PropTag.Message.ChangeNumberBin,
				PropTag.Message.PCL,
				PropTag.Message.CnMv,
				PropTag.Message.SourceEntryId,
				PropTag.Message.MailFlags,
				PropTag.Message.Associated,
				PropTag.Message.SubmitResponsibility,
				PropTag.Message.SharedReceiptHandling,
				PropTag.Message.Inid,
				PropTag.Message.MessageAttachList,
				PropTag.Message.SenderCAI,
				PropTag.Message.SentRepresentingCAI,
				PropTag.Message.OriginalSenderCAI,
				PropTag.Message.OriginalSentRepresentingCAI,
				PropTag.Message.ReceivedByCAI,
				PropTag.Message.ReceivedRepresentingCAI,
				PropTag.Message.ReadReceiptCAI,
				PropTag.Message.ReportCAI,
				PropTag.Message.CreatorCAI,
				PropTag.Message.LastModifierCAI,
				PropTag.Message.CnsetRead,
				PropTag.Message.CnsetSeenFAI,
				PropTag.Message.IdSetDeleted,
				PropTag.Message.OriginatorCAI,
				PropTag.Message.ReportDestinationCAI,
				PropTag.Message.OriginalAuthorCAI,
				PropTag.Message.ReadCnNew,
				PropTag.Message.ReadCnNewBin,
				PropTag.Message.SenderTelephoneNumber,
				PropTag.Message.VoiceMessageAttachmentOrder,
				PropTag.Message.DocumentId,
				PropTag.Message.MailboxNum,
				PropTag.Message.ConversationIdHash,
				PropTag.Message.ConversationDocumentId,
				PropTag.Message.LocalDirectoryBlob,
				PropTag.Message.ViewStyle,
				PropTag.Message.FreebusyEMA,
				PropTag.Message.WunderbarLinkEntryID,
				PropTag.Message.WunderbarLinkStoreEntryId,
				PropTag.Message.SchdInfoFreebusyMerged,
				PropTag.Message.WunderbarLinkGroupClsId,
				PropTag.Message.WunderbarLinkGroupName,
				PropTag.Message.WunderbarLinkSection,
				PropTag.Message.NavigationNodeCalendarColor,
				PropTag.Message.NavigationNodeAddressbookEntryId,
				PropTag.Message.AgingDeleteItems,
				PropTag.Message.AgingFileName9AndPrev,
				PropTag.Message.AgingAgeFolder,
				PropTag.Message.AgingDontAgeMe,
				PropTag.Message.AgingFileNameAfter9,
				PropTag.Message.AgingWhenDeletedOnServer,
				PropTag.Message.AgingWaitUntilExpired,
				PropTag.Message.ConversationMvFrom,
				PropTag.Message.ConversationMvFromMailboxWide,
				PropTag.Message.ConversationMvTo,
				PropTag.Message.ConversationMvToMailboxWide,
				PropTag.Message.ConversationMessageDeliveryTime,
				PropTag.Message.ConversationMessageDeliveryTimeMailboxWide,
				PropTag.Message.ConversationCategories,
				PropTag.Message.ConversationCategoriesMailboxWide,
				PropTag.Message.ConversationFlagStatus,
				PropTag.Message.ConversationFlagStatusMailboxWide,
				PropTag.Message.ConversationFlagCompleteTime,
				PropTag.Message.ConversationFlagCompleteTimeMailboxWide,
				PropTag.Message.ConversationHasAttach,
				PropTag.Message.ConversationHasAttachMailboxWide,
				PropTag.Message.ConversationContentCount,
				PropTag.Message.ConversationContentCountMailboxWide,
				PropTag.Message.ConversationContentUnread,
				PropTag.Message.ConversationContentUnreadMailboxWide,
				PropTag.Message.ConversationMessageSize,
				PropTag.Message.ConversationMessageSizeMailboxWide,
				PropTag.Message.ConversationMessageClasses,
				PropTag.Message.ConversationMessageClassesMailboxWide,
				PropTag.Message.ConversationReplyForwardState,
				PropTag.Message.ConversationReplyForwardStateMailboxWide,
				PropTag.Message.ConversationImportance,
				PropTag.Message.ConversationImportanceMailboxWide,
				PropTag.Message.ConversationMvFromUnread,
				PropTag.Message.ConversationMvFromUnreadMailboxWide,
				PropTag.Message.ConversationMvItemIds,
				PropTag.Message.ConversationMvItemIdsMailboxWide,
				PropTag.Message.ConversationHasIrm,
				PropTag.Message.ConversationHasIrmMailboxWide,
				PropTag.Message.PersonCompanyNameMailboxWide,
				PropTag.Message.PersonDisplayNameMailboxWide,
				PropTag.Message.PersonGivenNameMailboxWide,
				PropTag.Message.PersonSurnameMailboxWide,
				PropTag.Message.PersonPhotoContactEntryIdMailboxWide,
				PropTag.Message.ConversationInferredImportanceInternal,
				PropTag.Message.ConversationInferredImportanceOverride,
				PropTag.Message.ConversationInferredUnimportanceInternal,
				PropTag.Message.ConversationInferredImportanceInternalMailboxWide,
				PropTag.Message.ConversationInferredImportanceOverrideMailboxWide,
				PropTag.Message.ConversationInferredUnimportanceInternalMailboxWide,
				PropTag.Message.PersonFileAsMailboxWide,
				PropTag.Message.PersonRelevanceScoreMailboxWide,
				PropTag.Message.PersonIsDistributionListMailboxWide,
				PropTag.Message.PersonHomeCityMailboxWide,
				PropTag.Message.PersonCreationTimeMailboxWide,
				PropTag.Message.PersonGALLinkIDMailboxWide,
				PropTag.Message.PersonMvEmailAddressMailboxWide,
				PropTag.Message.PersonMvEmailDisplayNameMailboxWide,
				PropTag.Message.PersonMvEmailRoutingTypeMailboxWide,
				PropTag.Message.PersonImAddressMailboxWide,
				PropTag.Message.PersonWorkCityMailboxWide,
				PropTag.Message.PersonDisplayNameFirstLastMailboxWide,
				PropTag.Message.PersonDisplayNameLastFirstMailboxWide,
				PropTag.Message.ConversationGroupingActions,
				PropTag.Message.ConversationGroupingActionsMailboxWide,
				PropTag.Message.ConversationPredictedActionsSummary,
				PropTag.Message.ConversationPredictedActionsSummaryMailboxWide,
				PropTag.Message.ConversationHasClutter,
				PropTag.Message.ConversationHasClutterMailboxWide,
				PropTag.Message.ConversationLastMemberDocumentId,
				PropTag.Message.ConversationPreview,
				PropTag.Message.ConversationLastMemberDocumentIdMailboxWide,
				PropTag.Message.ConversationInitialMemberDocumentId,
				PropTag.Message.ConversationMemberDocumentIds,
				PropTag.Message.ConversationMessageDeliveryOrRenewTimeMailboxWide,
				PropTag.Message.FamilyId,
				PropTag.Message.ConversationMessageRichContentMailboxWide,
				PropTag.Message.ConversationPreviewMailboxWide,
				PropTag.Message.ConversationMessageDeliveryOrRenewTime,
				PropTag.Message.ConversationWorkingSetSourcePartition,
				PropTag.Message.NDRFromName,
				PropTag.Message.SecurityFlags,
				PropTag.Message.SecurityReceiptRequestProcessed,
				PropTag.Message.FavoriteDisplayName,
				PropTag.Message.FavoriteDisplayAlias,
				PropTag.Message.FavPublicSourceKey,
				PropTag.Message.SyncFolderSourceKey,
				PropTag.Message.UserConfigurationDataType,
				PropTag.Message.UserConfigurationXmlStream,
				PropTag.Message.UserConfigurationStream,
				PropTag.Message.ReplyFwdStatus,
				PropTag.Message.OscSyncEnabledOnServer,
				PropTag.Message.Processed,
				PropTag.Message.FavLevelMask
			};
		}

		// Token: 0x02000009 RID: 9
		public static class Attachment
		{
			// Token: 0x04001419 RID: 5145
			public static readonly StorePropTag TNEFCorrelationKey = new StorePropTag(127, PropertyType.Binary, new StorePropInfo("TNEFCorrelationKey", 127, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400141A RID: 5146
			public static readonly StorePropTag PhysicalRenditionAttributes = new StorePropTag(3088, PropertyType.Binary, new StorePropInfo("PhysicalRenditionAttributes", 3088, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400141B RID: 5147
			public static readonly StorePropTag ItemSubobjectsBin = new StorePropTag(3603, PropertyType.Binary, new StorePropInfo("ItemSubobjectsBin", 3603, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400141C RID: 5148
			public static readonly StorePropTag AttachSize = new StorePropTag(3616, PropertyType.Int32, new StorePropInfo("AttachSize", 3616, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400141D RID: 5149
			public static readonly StorePropTag AttachSizeInt64 = new StorePropTag(3616, PropertyType.Int64, new StorePropInfo("AttachSizeInt64", 3616, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400141E RID: 5150
			public static readonly StorePropTag AttachNum = new StorePropTag(3617, PropertyType.Int32, new StorePropInfo("AttachNum", 3617, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400141F RID: 5151
			public static readonly StorePropTag CreatorSID = new StorePropTag(3672, PropertyType.Binary, new StorePropInfo("CreatorSID", 3672, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001420 RID: 5152
			public static readonly StorePropTag LastModifierSid = new StorePropTag(3673, PropertyType.Binary, new StorePropInfo("LastModifierSid", 3673, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001421 RID: 5153
			public static readonly StorePropTag VirusScannerStamp = new StorePropTag(3734, PropertyType.Binary, new StorePropInfo("VirusScannerStamp", 3734, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 6, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001422 RID: 5154
			public static readonly StorePropTag VirusTransportStamp = new StorePropTag(3734, PropertyType.MVUnicode, new StorePropInfo("VirusTransportStamp", 3734, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001423 RID: 5155
			public static readonly StorePropTag AccessLevel = new StorePropTag(4087, PropertyType.Int32, new StorePropInfo("AccessLevel", 4087, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001424 RID: 5156
			public static readonly StorePropTag MappingSignature = new StorePropTag(4088, PropertyType.Binary, new StorePropInfo("MappingSignature", 4088, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001425 RID: 5157
			public static readonly StorePropTag RecordKey = new StorePropTag(4089, PropertyType.Binary, new StorePropInfo("RecordKey", 4089, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001426 RID: 5158
			public static readonly StorePropTag ObjectType = new StorePropTag(4094, PropertyType.Int32, new StorePropInfo("ObjectType", 4094, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001427 RID: 5159
			public static readonly StorePropTag DisplayName = new StorePropTag(12289, PropertyType.Unicode, new StorePropInfo("DisplayName", 12289, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001428 RID: 5160
			public static readonly StorePropTag Comment = new StorePropTag(12292, PropertyType.Unicode, new StorePropInfo("Comment", 12292, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001429 RID: 5161
			public static readonly StorePropTag CreationTime = new StorePropTag(12295, PropertyType.SysTime, new StorePropInfo("CreationTime", 12295, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400142A RID: 5162
			public static readonly StorePropTag LastModificationTime = new StorePropTag(12296, PropertyType.SysTime, new StorePropInfo("LastModificationTime", 12296, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400142B RID: 5163
			public static readonly StorePropTag AttachmentX400Parameters = new StorePropTag(14080, PropertyType.Binary, new StorePropInfo("AttachmentX400Parameters", 14080, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400142C RID: 5164
			public static readonly StorePropTag Content = new StorePropTag(14081, PropertyType.Binary, new StorePropInfo("Content", 14081, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(15)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400142D RID: 5165
			public static readonly StorePropTag ContentObj = new StorePropTag(14081, PropertyType.Object, new StorePropInfo("ContentObj", 14081, PropertyType.Object, StorePropInfo.Flags.None, 0UL, new PropertyCategories(15)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400142E RID: 5166
			public static readonly StorePropTag AttachmentEncoding = new StorePropTag(14082, PropertyType.Binary, new StorePropInfo("AttachmentEncoding", 14082, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400142F RID: 5167
			public static readonly StorePropTag ContentId = new StorePropTag(14083, PropertyType.Unicode, new StorePropInfo("ContentId", 14083, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001430 RID: 5168
			public static readonly StorePropTag ContentType = new StorePropTag(14084, PropertyType.Unicode, new StorePropInfo("ContentType", 14084, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001431 RID: 5169
			public static readonly StorePropTag AttachMethod = new StorePropTag(14085, PropertyType.Int32, new StorePropInfo("AttachMethod", 14085, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001432 RID: 5170
			public static readonly StorePropTag MimeUrl = new StorePropTag(14087, PropertyType.Unicode, new StorePropInfo("MimeUrl", 14087, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001433 RID: 5171
			public static readonly StorePropTag AttachmentPathName = new StorePropTag(14088, PropertyType.Unicode, new StorePropInfo("AttachmentPathName", 14088, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001434 RID: 5172
			public static readonly StorePropTag AttachRendering = new StorePropTag(14089, PropertyType.Binary, new StorePropInfo("AttachRendering", 14089, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001435 RID: 5173
			public static readonly StorePropTag AttachTag = new StorePropTag(14090, PropertyType.Binary, new StorePropInfo("AttachTag", 14090, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001436 RID: 5174
			public static readonly StorePropTag RenderingPosition = new StorePropTag(14091, PropertyType.Int32, new StorePropInfo("RenderingPosition", 14091, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001437 RID: 5175
			public static readonly StorePropTag AttachTransportName = new StorePropTag(14092, PropertyType.Unicode, new StorePropInfo("AttachTransportName", 14092, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001438 RID: 5176
			public static readonly StorePropTag AttachmentLongPathName = new StorePropTag(14093, PropertyType.Unicode, new StorePropInfo("AttachmentLongPathName", 14093, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001439 RID: 5177
			public static readonly StorePropTag AttachmentMimeTag = new StorePropTag(14094, PropertyType.Unicode, new StorePropInfo("AttachmentMimeTag", 14094, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400143A RID: 5178
			public static readonly StorePropTag AttachAdditionalInfo = new StorePropTag(14095, PropertyType.Binary, new StorePropInfo("AttachAdditionalInfo", 14095, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400143B RID: 5179
			public static readonly StorePropTag AttachmentMimeSequence = new StorePropTag(14096, PropertyType.Int32, new StorePropInfo("AttachmentMimeSequence", 14096, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400143C RID: 5180
			public static readonly StorePropTag AttachContentBase = new StorePropTag(14097, PropertyType.Unicode, new StorePropInfo("AttachContentBase", 14097, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400143D RID: 5181
			public static readonly StorePropTag AttachContentId = new StorePropTag(14098, PropertyType.Unicode, new StorePropInfo("AttachContentId", 14098, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400143E RID: 5182
			public static readonly StorePropTag AttachContentLocation = new StorePropTag(14099, PropertyType.Unicode, new StorePropInfo("AttachContentLocation", 14099, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400143F RID: 5183
			public static readonly StorePropTag AttachmentFlags = new StorePropTag(14100, PropertyType.Int32, new StorePropInfo("AttachmentFlags", 14100, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001440 RID: 5184
			public static readonly StorePropTag AttachDisposition = new StorePropTag(14102, PropertyType.Unicode, new StorePropInfo("AttachDisposition", 14102, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001441 RID: 5185
			public static readonly StorePropTag AttachPayloadProviderGuidString = new StorePropTag(14105, PropertyType.Unicode, new StorePropInfo("AttachPayloadProviderGuidString", 14105, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001442 RID: 5186
			public static readonly StorePropTag AttachPayloadClass = new StorePropTag(14106, PropertyType.Unicode, new StorePropInfo("AttachPayloadClass", 14106, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001443 RID: 5187
			public static readonly StorePropTag TextAttachmentCharset = new StorePropTag(14107, PropertyType.Unicode, new StorePropInfo("TextAttachmentCharset", 14107, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001444 RID: 5188
			public static readonly StorePropTag Language = new StorePropTag(14860, PropertyType.Unicode, new StorePropInfo("Language", 14860, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001445 RID: 5189
			public static readonly StorePropTag TestBlobProperty = new StorePropTag(15616, PropertyType.Int64, new StorePropInfo("TestBlobProperty", 15616, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001446 RID: 5190
			public static readonly StorePropTag MailboxPartitionNumber = new StorePropTag(15775, PropertyType.Int32, new StorePropInfo("MailboxPartitionNumber", 15775, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(0, 1, 2, 3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001447 RID: 5191
			public static readonly StorePropTag MailboxNumberInternal = new StorePropTag(15776, PropertyType.Int32, new StorePropInfo("MailboxNumberInternal", 15776, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(0, 1, 2, 3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001448 RID: 5192
			public static readonly StorePropTag AttachmentId = new StorePropTag(16264, PropertyType.Int64, new StorePropInfo("AttachmentId", 16264, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001449 RID: 5193
			public static readonly StorePropTag AttachmentIdBin = new StorePropTag(16264, PropertyType.Binary, new StorePropInfo("AttachmentIdBin", 16264, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400144A RID: 5194
			public static readonly StorePropTag ReplicaChangeNumber = new StorePropTag(16328, PropertyType.Binary, new StorePropInfo("ReplicaChangeNumber", 16328, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400144B RID: 5195
			public static readonly StorePropTag NewAttach = new StorePropTag(16384, PropertyType.Int32, new StorePropInfo("NewAttach", 16384, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400144C RID: 5196
			public static readonly StorePropTag StartEmbed = new StorePropTag(16385, PropertyType.Int32, new StorePropInfo("StartEmbed", 16385, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400144D RID: 5197
			public static readonly StorePropTag EndEmbed = new StorePropTag(16386, PropertyType.Int32, new StorePropInfo("EndEmbed", 16386, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400144E RID: 5198
			public static readonly StorePropTag StartRecip = new StorePropTag(16387, PropertyType.Int32, new StorePropInfo("StartRecip", 16387, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400144F RID: 5199
			public static readonly StorePropTag EndRecip = new StorePropTag(16388, PropertyType.Int32, new StorePropInfo("EndRecip", 16388, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001450 RID: 5200
			public static readonly StorePropTag EndCcRecip = new StorePropTag(16389, PropertyType.Int32, new StorePropInfo("EndCcRecip", 16389, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001451 RID: 5201
			public static readonly StorePropTag EndBccRecip = new StorePropTag(16390, PropertyType.Int32, new StorePropInfo("EndBccRecip", 16390, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001452 RID: 5202
			public static readonly StorePropTag EndP1Recip = new StorePropTag(16391, PropertyType.Int32, new StorePropInfo("EndP1Recip", 16391, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001453 RID: 5203
			public static readonly StorePropTag DNPrefix = new StorePropTag(16392, PropertyType.Unicode, new StorePropInfo("DNPrefix", 16392, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001454 RID: 5204
			public static readonly StorePropTag StartTopFolder = new StorePropTag(16393, PropertyType.Int32, new StorePropInfo("StartTopFolder", 16393, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001455 RID: 5205
			public static readonly StorePropTag StartSubFolder = new StorePropTag(16394, PropertyType.Int32, new StorePropInfo("StartSubFolder", 16394, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001456 RID: 5206
			public static readonly StorePropTag EndFolder = new StorePropTag(16395, PropertyType.Int32, new StorePropInfo("EndFolder", 16395, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001457 RID: 5207
			public static readonly StorePropTag StartMessage = new StorePropTag(16396, PropertyType.Int32, new StorePropInfo("StartMessage", 16396, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001458 RID: 5208
			public static readonly StorePropTag EndMessage = new StorePropTag(16397, PropertyType.Int32, new StorePropInfo("EndMessage", 16397, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001459 RID: 5209
			public static readonly StorePropTag EndAttach = new StorePropTag(16398, PropertyType.Int32, new StorePropInfo("EndAttach", 16398, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400145A RID: 5210
			public static readonly StorePropTag EcWarning = new StorePropTag(16399, PropertyType.Int32, new StorePropInfo("EcWarning", 16399, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400145B RID: 5211
			public static readonly StorePropTag StartFAIMessage = new StorePropTag(16400, PropertyType.Int32, new StorePropInfo("StartFAIMessage", 16400, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400145C RID: 5212
			public static readonly StorePropTag NewFXFolder = new StorePropTag(16401, PropertyType.Binary, new StorePropInfo("NewFXFolder", 16401, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400145D RID: 5213
			public static readonly StorePropTag IncrSyncChange = new StorePropTag(16402, PropertyType.Int32, new StorePropInfo("IncrSyncChange", 16402, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400145E RID: 5214
			public static readonly StorePropTag IncrSyncDelete = new StorePropTag(16403, PropertyType.Int32, new StorePropInfo("IncrSyncDelete", 16403, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400145F RID: 5215
			public static readonly StorePropTag IncrSyncEnd = new StorePropTag(16404, PropertyType.Int32, new StorePropInfo("IncrSyncEnd", 16404, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001460 RID: 5216
			public static readonly StorePropTag IncrSyncMessage = new StorePropTag(16405, PropertyType.Int32, new StorePropInfo("IncrSyncMessage", 16405, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001461 RID: 5217
			public static readonly StorePropTag FastTransferDelProp = new StorePropTag(16406, PropertyType.Int32, new StorePropInfo("FastTransferDelProp", 16406, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001462 RID: 5218
			public static readonly StorePropTag IdsetGiven = new StorePropTag(16407, PropertyType.Binary, new StorePropInfo("IdsetGiven", 16407, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001463 RID: 5219
			public static readonly StorePropTag IdsetGivenInt32 = new StorePropTag(16407, PropertyType.Int32, new StorePropInfo("IdsetGivenInt32", 16407, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001464 RID: 5220
			public static readonly StorePropTag FastTransferErrorInfo = new StorePropTag(16408, PropertyType.Int32, new StorePropInfo("FastTransferErrorInfo", 16408, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001465 RID: 5221
			public static readonly StorePropTag SoftDeletes = new StorePropTag(16417, PropertyType.Binary, new StorePropInfo("SoftDeletes", 16417, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001466 RID: 5222
			public static readonly StorePropTag IdsetRead = new StorePropTag(16429, PropertyType.Binary, new StorePropInfo("IdsetRead", 16429, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001467 RID: 5223
			public static readonly StorePropTag IdsetUnread = new StorePropTag(16430, PropertyType.Binary, new StorePropInfo("IdsetUnread", 16430, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001468 RID: 5224
			public static readonly StorePropTag IncrSyncRead = new StorePropTag(16431, PropertyType.Int32, new StorePropInfo("IncrSyncRead", 16431, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001469 RID: 5225
			public static readonly StorePropTag IncrSyncStateBegin = new StorePropTag(16442, PropertyType.Int32, new StorePropInfo("IncrSyncStateBegin", 16442, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400146A RID: 5226
			public static readonly StorePropTag IncrSyncStateEnd = new StorePropTag(16443, PropertyType.Int32, new StorePropInfo("IncrSyncStateEnd", 16443, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400146B RID: 5227
			public static readonly StorePropTag IncrSyncImailStream = new StorePropTag(16444, PropertyType.Int32, new StorePropInfo("IncrSyncImailStream", 16444, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400146C RID: 5228
			public static readonly StorePropTag IncrSyncImailStreamContinue = new StorePropTag(16486, PropertyType.Int32, new StorePropInfo("IncrSyncImailStreamContinue", 16486, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400146D RID: 5229
			public static readonly StorePropTag IncrSyncImailStreamCancel = new StorePropTag(16487, PropertyType.Int32, new StorePropInfo("IncrSyncImailStreamCancel", 16487, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400146E RID: 5230
			public static readonly StorePropTag IncrSyncImailStream2Continue = new StorePropTag(16497, PropertyType.Int32, new StorePropInfo("IncrSyncImailStream2Continue", 16497, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400146F RID: 5231
			public static readonly StorePropTag IncrSyncProgressMode = new StorePropTag(16500, PropertyType.Boolean, new StorePropInfo("IncrSyncProgressMode", 16500, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001470 RID: 5232
			public static readonly StorePropTag SyncProgressPerMsg = new StorePropTag(16501, PropertyType.Boolean, new StorePropInfo("SyncProgressPerMsg", 16501, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001471 RID: 5233
			public static readonly StorePropTag IncrSyncMsgPartial = new StorePropTag(16506, PropertyType.Int32, new StorePropInfo("IncrSyncMsgPartial", 16506, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001472 RID: 5234
			public static readonly StorePropTag IncrSyncGroupInfo = new StorePropTag(16507, PropertyType.Int32, new StorePropInfo("IncrSyncGroupInfo", 16507, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001473 RID: 5235
			public static readonly StorePropTag IncrSyncGroupId = new StorePropTag(16508, PropertyType.Int32, new StorePropInfo("IncrSyncGroupId", 16508, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001474 RID: 5236
			public static readonly StorePropTag IncrSyncChangePartial = new StorePropTag(16509, PropertyType.Int32, new StorePropInfo("IncrSyncChangePartial", 16509, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001475 RID: 5237
			public static readonly StorePropTag HasNamedProperties = new StorePropTag(26186, PropertyType.Boolean, new StorePropInfo("HasNamedProperties", 26186, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001476 RID: 5238
			public static readonly StorePropTag CodePageId = new StorePropTag(26307, PropertyType.Int32, new StorePropInfo("CodePageId", 26307, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001477 RID: 5239
			public static readonly StorePropTag URLName = new StorePropTag(26375, PropertyType.Unicode, new StorePropInfo("URLName", 26375, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001478 RID: 5240
			public static readonly StorePropTag MimeSize = new StorePropTag(26438, PropertyType.Int64, new StorePropInfo("MimeSize", 26438, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001479 RID: 5241
			public static readonly StorePropTag MimeSize32 = new StorePropTag(26438, PropertyType.Int32, new StorePropInfo("MimeSize32", 26438, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400147A RID: 5242
			public static readonly StorePropTag FileSize = new StorePropTag(26439, PropertyType.Int64, new StorePropInfo("FileSize", 26439, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400147B RID: 5243
			public static readonly StorePropTag FileSize32 = new StorePropTag(26439, PropertyType.Int32, new StorePropInfo("FileSize32", 26439, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400147C RID: 5244
			public static readonly StorePropTag Mid = new StorePropTag(26442, PropertyType.Int64, new StorePropInfo("Mid", 26442, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400147D RID: 5245
			public static readonly StorePropTag MidBin = new StorePropTag(26442, PropertyType.Binary, new StorePropInfo("MidBin", 26442, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400147E RID: 5246
			public static readonly StorePropTag LTID = new StorePropTag(26456, PropertyType.Binary, new StorePropInfo("LTID", 26456, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9, 10)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400147F RID: 5247
			public static readonly StorePropTag CnsetSeen = new StorePropTag(26518, PropertyType.Binary, new StorePropInfo("CnsetSeen", 26518, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001480 RID: 5248
			public static readonly StorePropTag Inid = new StorePropTag(26544, PropertyType.Binary, new StorePropInfo("Inid", 26544, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 3)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001481 RID: 5249
			public static readonly StorePropTag CnsetRead = new StorePropTag(26578, PropertyType.Binary, new StorePropInfo("CnsetRead", 26578, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001482 RID: 5250
			public static readonly StorePropTag CnsetSeenFAI = new StorePropTag(26586, PropertyType.Binary, new StorePropInfo("CnsetSeenFAI", 26586, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001483 RID: 5251
			public static readonly StorePropTag IdSetDeleted = new StorePropTag(26597, PropertyType.Binary, new StorePropInfo("IdSetDeleted", 26597, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(1, 2, 3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001484 RID: 5252
			public static readonly StorePropTag MailboxNum = new StorePropTag(26655, PropertyType.Int32, new StorePropInfo("MailboxNum", 26655, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001485 RID: 5253
			public static readonly StorePropTag AttachEXCLIVersion = new StorePropTag(26889, PropertyType.Int32, new StorePropInfo("AttachEXCLIVersion", 26889, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001486 RID: 5254
			public static readonly StorePropTag HasDlpDetectedAttachmentClassifications = new StorePropTag(32760, PropertyType.Unicode, new StorePropInfo("HasDlpDetectedAttachmentClassifications", 32760, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001487 RID: 5255
			public static readonly StorePropTag SExceptionReplaceTime = new StorePropTag(32761, PropertyType.SysTime, new StorePropInfo("SExceptionReplaceTime", 32761, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001488 RID: 5256
			public static readonly StorePropTag AttachmentLinkId = new StorePropTag(32762, PropertyType.Int32, new StorePropInfo("AttachmentLinkId", 32762, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x04001489 RID: 5257
			public static readonly StorePropTag ExceptionStartTime = new StorePropTag(32763, PropertyType.AppTime, new StorePropInfo("ExceptionStartTime", 32763, PropertyType.AppTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400148A RID: 5258
			public static readonly StorePropTag ExceptionEndTime = new StorePropTag(32764, PropertyType.AppTime, new StorePropInfo("ExceptionEndTime", 32764, PropertyType.AppTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400148B RID: 5259
			public static readonly StorePropTag AttachmentFlags2 = new StorePropTag(32765, PropertyType.Int32, new StorePropInfo("AttachmentFlags2", 32765, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400148C RID: 5260
			public static readonly StorePropTag AttachmentHidden = new StorePropTag(32766, PropertyType.Boolean, new StorePropInfo("AttachmentHidden", 32766, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400148D RID: 5261
			public static readonly StorePropTag AttachmentContactPhoto = new StorePropTag(32767, PropertyType.Boolean, new StorePropInfo("AttachmentContactPhoto", 32767, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Attachment);

			// Token: 0x0400148E RID: 5262
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[]
			{
				PropTag.Attachment.MailboxPartitionNumber,
				PropTag.Attachment.MailboxNumberInternal
			};

			// Token: 0x0400148F RID: 5263
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[]
			{
				PropTag.Attachment.MappingSignature,
				PropTag.Attachment.ObjectType,
				PropTag.Attachment.MailboxPartitionNumber,
				PropTag.Attachment.MailboxNumberInternal,
				PropTag.Attachment.CnsetSeen,
				PropTag.Attachment.Inid,
				PropTag.Attachment.CnsetRead,
				PropTag.Attachment.CnsetSeenFAI,
				PropTag.Attachment.IdSetDeleted
			};

			// Token: 0x04001490 RID: 5264
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[]
			{
				PropTag.Attachment.MappingSignature,
				PropTag.Attachment.ObjectType,
				PropTag.Attachment.MailboxPartitionNumber,
				PropTag.Attachment.MailboxNumberInternal,
				PropTag.Attachment.CnsetSeen,
				PropTag.Attachment.Inid,
				PropTag.Attachment.CnsetRead,
				PropTag.Attachment.CnsetSeenFAI,
				PropTag.Attachment.IdSetDeleted
			};

			// Token: 0x04001491 RID: 5265
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[]
			{
				PropTag.Attachment.ItemSubobjectsBin,
				PropTag.Attachment.AttachSize,
				PropTag.Attachment.AttachSizeInt64,
				PropTag.Attachment.AttachNum,
				PropTag.Attachment.VirusScannerStamp,
				PropTag.Attachment.VirusTransportStamp,
				PropTag.Attachment.AccessLevel,
				PropTag.Attachment.RecordKey,
				PropTag.Attachment.MailboxPartitionNumber,
				PropTag.Attachment.MailboxNumberInternal,
				PropTag.Attachment.AttachmentId,
				PropTag.Attachment.AttachmentIdBin,
				PropTag.Attachment.ReplicaChangeNumber,
				PropTag.Attachment.NewAttach,
				PropTag.Attachment.StartEmbed,
				PropTag.Attachment.EndEmbed,
				PropTag.Attachment.StartRecip,
				PropTag.Attachment.EndRecip,
				PropTag.Attachment.EndCcRecip,
				PropTag.Attachment.EndBccRecip,
				PropTag.Attachment.EndP1Recip,
				PropTag.Attachment.DNPrefix,
				PropTag.Attachment.StartTopFolder,
				PropTag.Attachment.StartSubFolder,
				PropTag.Attachment.EndFolder,
				PropTag.Attachment.StartMessage,
				PropTag.Attachment.EndMessage,
				PropTag.Attachment.EndAttach,
				PropTag.Attachment.EcWarning,
				PropTag.Attachment.StartFAIMessage,
				PropTag.Attachment.NewFXFolder,
				PropTag.Attachment.IncrSyncChange,
				PropTag.Attachment.IncrSyncDelete,
				PropTag.Attachment.IncrSyncEnd,
				PropTag.Attachment.IncrSyncMessage,
				PropTag.Attachment.FastTransferDelProp,
				PropTag.Attachment.IdsetGiven,
				PropTag.Attachment.IdsetGivenInt32,
				PropTag.Attachment.FastTransferErrorInfo,
				PropTag.Attachment.SoftDeletes,
				PropTag.Attachment.IdsetRead,
				PropTag.Attachment.IdsetUnread,
				PropTag.Attachment.IncrSyncRead,
				PropTag.Attachment.IncrSyncStateBegin,
				PropTag.Attachment.IncrSyncStateEnd,
				PropTag.Attachment.IncrSyncImailStream,
				PropTag.Attachment.IncrSyncImailStreamContinue,
				PropTag.Attachment.IncrSyncImailStreamCancel,
				PropTag.Attachment.IncrSyncImailStream2Continue,
				PropTag.Attachment.IncrSyncProgressMode,
				PropTag.Attachment.SyncProgressPerMsg,
				PropTag.Attachment.IncrSyncMsgPartial,
				PropTag.Attachment.IncrSyncGroupInfo,
				PropTag.Attachment.IncrSyncGroupId,
				PropTag.Attachment.IncrSyncChangePartial,
				PropTag.Attachment.HasNamedProperties,
				PropTag.Attachment.FileSize,
				PropTag.Attachment.FileSize32,
				PropTag.Attachment.Mid,
				PropTag.Attachment.MidBin,
				PropTag.Attachment.LTID,
				PropTag.Attachment.CnsetSeen,
				PropTag.Attachment.Inid,
				PropTag.Attachment.CnsetRead,
				PropTag.Attachment.CnsetSeenFAI,
				PropTag.Attachment.IdSetDeleted,
				PropTag.Attachment.MailboxNum
			};

			// Token: 0x04001492 RID: 5266
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[0];

			// Token: 0x04001493 RID: 5267
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[0];

			// Token: 0x04001494 RID: 5268
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[]
			{
				PropTag.Attachment.VirusScannerStamp
			};

			// Token: 0x04001495 RID: 5269
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x04001496 RID: 5270
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x04001497 RID: 5271
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[]
			{
				PropTag.Attachment.ItemSubobjectsBin,
				PropTag.Attachment.AttachSize,
				PropTag.Attachment.AttachSizeInt64,
				PropTag.Attachment.AttachNum,
				PropTag.Attachment.VirusScannerStamp,
				PropTag.Attachment.VirusTransportStamp,
				PropTag.Attachment.AccessLevel,
				PropTag.Attachment.MappingSignature,
				PropTag.Attachment.RecordKey,
				PropTag.Attachment.ObjectType,
				PropTag.Attachment.MailboxPartitionNumber,
				PropTag.Attachment.MailboxNumberInternal,
				PropTag.Attachment.AttachmentId,
				PropTag.Attachment.AttachmentIdBin,
				PropTag.Attachment.ReplicaChangeNumber,
				PropTag.Attachment.NewAttach,
				PropTag.Attachment.StartEmbed,
				PropTag.Attachment.EndEmbed,
				PropTag.Attachment.StartRecip,
				PropTag.Attachment.EndRecip,
				PropTag.Attachment.EndCcRecip,
				PropTag.Attachment.EndBccRecip,
				PropTag.Attachment.EndP1Recip,
				PropTag.Attachment.DNPrefix,
				PropTag.Attachment.StartTopFolder,
				PropTag.Attachment.StartSubFolder,
				PropTag.Attachment.EndFolder,
				PropTag.Attachment.StartMessage,
				PropTag.Attachment.EndMessage,
				PropTag.Attachment.EndAttach,
				PropTag.Attachment.EcWarning,
				PropTag.Attachment.StartFAIMessage,
				PropTag.Attachment.NewFXFolder,
				PropTag.Attachment.IncrSyncChange,
				PropTag.Attachment.IncrSyncDelete,
				PropTag.Attachment.IncrSyncEnd,
				PropTag.Attachment.IncrSyncMessage,
				PropTag.Attachment.FastTransferDelProp,
				PropTag.Attachment.IdsetGiven,
				PropTag.Attachment.IdsetGivenInt32,
				PropTag.Attachment.FastTransferErrorInfo,
				PropTag.Attachment.SoftDeletes,
				PropTag.Attachment.IdsetRead,
				PropTag.Attachment.IdsetUnread,
				PropTag.Attachment.IncrSyncRead,
				PropTag.Attachment.IncrSyncStateBegin,
				PropTag.Attachment.IncrSyncStateEnd,
				PropTag.Attachment.IncrSyncImailStream,
				PropTag.Attachment.IncrSyncImailStreamContinue,
				PropTag.Attachment.IncrSyncImailStreamCancel,
				PropTag.Attachment.IncrSyncImailStream2Continue,
				PropTag.Attachment.IncrSyncProgressMode,
				PropTag.Attachment.SyncProgressPerMsg,
				PropTag.Attachment.IncrSyncMsgPartial,
				PropTag.Attachment.IncrSyncGroupInfo,
				PropTag.Attachment.IncrSyncGroupId,
				PropTag.Attachment.IncrSyncChangePartial,
				PropTag.Attachment.HasNamedProperties,
				PropTag.Attachment.FileSize,
				PropTag.Attachment.FileSize32,
				PropTag.Attachment.Mid,
				PropTag.Attachment.MidBin,
				PropTag.Attachment.LTID,
				PropTag.Attachment.CnsetSeen,
				PropTag.Attachment.CnsetRead,
				PropTag.Attachment.CnsetSeenFAI,
				PropTag.Attachment.IdSetDeleted,
				PropTag.Attachment.MailboxNum
			};

			// Token: 0x04001498 RID: 5272
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[]
			{
				PropTag.Attachment.AttachSize,
				PropTag.Attachment.AttachSizeInt64,
				PropTag.Attachment.AttachNum,
				PropTag.Attachment.AccessLevel,
				PropTag.Attachment.HasNamedProperties,
				PropTag.Attachment.FileSize,
				PropTag.Attachment.FileSize32,
				PropTag.Attachment.LTID
			};

			// Token: 0x04001499 RID: 5273
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x0400149A RID: 5274
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x0400149B RID: 5275
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x0400149C RID: 5276
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x0400149D RID: 5277
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[]
			{
				PropTag.Attachment.Content,
				PropTag.Attachment.ContentObj
			};

			// Token: 0x0400149E RID: 5278
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x0400149F RID: 5279
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x040014A0 RID: 5280
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x040014A1 RID: 5281
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.Attachment.TNEFCorrelationKey,
				PropTag.Attachment.PhysicalRenditionAttributes,
				PropTag.Attachment.ItemSubobjectsBin,
				PropTag.Attachment.AttachSize,
				PropTag.Attachment.AttachSizeInt64,
				PropTag.Attachment.AttachNum,
				PropTag.Attachment.CreatorSID,
				PropTag.Attachment.LastModifierSid,
				PropTag.Attachment.VirusScannerStamp,
				PropTag.Attachment.VirusTransportStamp,
				PropTag.Attachment.AccessLevel,
				PropTag.Attachment.MappingSignature,
				PropTag.Attachment.RecordKey,
				PropTag.Attachment.ObjectType,
				PropTag.Attachment.DisplayName,
				PropTag.Attachment.Comment,
				PropTag.Attachment.CreationTime,
				PropTag.Attachment.LastModificationTime,
				PropTag.Attachment.AttachmentX400Parameters,
				PropTag.Attachment.Content,
				PropTag.Attachment.ContentObj,
				PropTag.Attachment.AttachmentEncoding,
				PropTag.Attachment.ContentId,
				PropTag.Attachment.ContentType,
				PropTag.Attachment.AttachMethod,
				PropTag.Attachment.MimeUrl,
				PropTag.Attachment.AttachmentPathName,
				PropTag.Attachment.AttachRendering,
				PropTag.Attachment.AttachTag,
				PropTag.Attachment.RenderingPosition,
				PropTag.Attachment.AttachTransportName,
				PropTag.Attachment.AttachmentLongPathName,
				PropTag.Attachment.AttachmentMimeTag,
				PropTag.Attachment.AttachAdditionalInfo,
				PropTag.Attachment.AttachmentMimeSequence,
				PropTag.Attachment.AttachContentBase,
				PropTag.Attachment.AttachContentId,
				PropTag.Attachment.AttachContentLocation,
				PropTag.Attachment.AttachmentFlags,
				PropTag.Attachment.AttachDisposition,
				PropTag.Attachment.AttachPayloadProviderGuidString,
				PropTag.Attachment.AttachPayloadClass,
				PropTag.Attachment.TextAttachmentCharset,
				PropTag.Attachment.Language,
				PropTag.Attachment.TestBlobProperty,
				PropTag.Attachment.MailboxPartitionNumber,
				PropTag.Attachment.MailboxNumberInternal,
				PropTag.Attachment.AttachmentId,
				PropTag.Attachment.AttachmentIdBin,
				PropTag.Attachment.ReplicaChangeNumber,
				PropTag.Attachment.NewAttach,
				PropTag.Attachment.StartEmbed,
				PropTag.Attachment.EndEmbed,
				PropTag.Attachment.StartRecip,
				PropTag.Attachment.EndRecip,
				PropTag.Attachment.EndCcRecip,
				PropTag.Attachment.EndBccRecip,
				PropTag.Attachment.EndP1Recip,
				PropTag.Attachment.DNPrefix,
				PropTag.Attachment.StartTopFolder,
				PropTag.Attachment.StartSubFolder,
				PropTag.Attachment.EndFolder,
				PropTag.Attachment.StartMessage,
				PropTag.Attachment.EndMessage,
				PropTag.Attachment.EndAttach,
				PropTag.Attachment.EcWarning,
				PropTag.Attachment.StartFAIMessage,
				PropTag.Attachment.NewFXFolder,
				PropTag.Attachment.IncrSyncChange,
				PropTag.Attachment.IncrSyncDelete,
				PropTag.Attachment.IncrSyncEnd,
				PropTag.Attachment.IncrSyncMessage,
				PropTag.Attachment.FastTransferDelProp,
				PropTag.Attachment.IdsetGiven,
				PropTag.Attachment.IdsetGivenInt32,
				PropTag.Attachment.FastTransferErrorInfo,
				PropTag.Attachment.SoftDeletes,
				PropTag.Attachment.IdsetRead,
				PropTag.Attachment.IdsetUnread,
				PropTag.Attachment.IncrSyncRead,
				PropTag.Attachment.IncrSyncStateBegin,
				PropTag.Attachment.IncrSyncStateEnd,
				PropTag.Attachment.IncrSyncImailStream,
				PropTag.Attachment.IncrSyncImailStreamContinue,
				PropTag.Attachment.IncrSyncImailStreamCancel,
				PropTag.Attachment.IncrSyncImailStream2Continue,
				PropTag.Attachment.IncrSyncProgressMode,
				PropTag.Attachment.SyncProgressPerMsg,
				PropTag.Attachment.IncrSyncMsgPartial,
				PropTag.Attachment.IncrSyncGroupInfo,
				PropTag.Attachment.IncrSyncGroupId,
				PropTag.Attachment.IncrSyncChangePartial,
				PropTag.Attachment.HasNamedProperties,
				PropTag.Attachment.CodePageId,
				PropTag.Attachment.URLName,
				PropTag.Attachment.MimeSize,
				PropTag.Attachment.MimeSize32,
				PropTag.Attachment.FileSize,
				PropTag.Attachment.FileSize32,
				PropTag.Attachment.Mid,
				PropTag.Attachment.MidBin,
				PropTag.Attachment.LTID,
				PropTag.Attachment.CnsetSeen,
				PropTag.Attachment.Inid,
				PropTag.Attachment.CnsetRead,
				PropTag.Attachment.CnsetSeenFAI,
				PropTag.Attachment.IdSetDeleted,
				PropTag.Attachment.MailboxNum,
				PropTag.Attachment.AttachEXCLIVersion,
				PropTag.Attachment.HasDlpDetectedAttachmentClassifications,
				PropTag.Attachment.SExceptionReplaceTime,
				PropTag.Attachment.AttachmentLinkId,
				PropTag.Attachment.ExceptionStartTime,
				PropTag.Attachment.ExceptionEndTime,
				PropTag.Attachment.AttachmentFlags2,
				PropTag.Attachment.AttachmentHidden,
				PropTag.Attachment.AttachmentContactPhoto
			};
		}

		// Token: 0x0200000A RID: 10
		public static class Recipient
		{
			// Token: 0x040014A2 RID: 5282
			public static readonly StorePropTag DeliveryReportRequested = new StorePropTag(35, PropertyType.Boolean, new StorePropInfo("DeliveryReportRequested", 35, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014A3 RID: 5283
			public static readonly StorePropTag ReadReceiptRequested = new StorePropTag(41, PropertyType.Boolean, new StorePropInfo("ReadReceiptRequested", 41, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014A4 RID: 5284
			public static readonly StorePropTag ReportTime = new StorePropTag(50, PropertyType.SysTime, new StorePropInfo("ReportTime", 50, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014A5 RID: 5285
			public static readonly StorePropTag DiscVal = new StorePropTag(74, PropertyType.Boolean, new StorePropInfo("DiscVal", 74, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014A6 RID: 5286
			public static readonly StorePropTag ExplicitConversion = new StorePropTag(3073, PropertyType.Int32, new StorePropInfo("ExplicitConversion", 3073, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014A7 RID: 5287
			public static readonly StorePropTag NDRReasonCode = new StorePropTag(3076, PropertyType.Int32, new StorePropInfo("NDRReasonCode", 3076, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014A8 RID: 5288
			public static readonly StorePropTag NDRDiagCode = new StorePropTag(3077, PropertyType.Int32, new StorePropInfo("NDRDiagCode", 3077, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014A9 RID: 5289
			public static readonly StorePropTag NonReceiptNotificationRequested = new StorePropTag(3078, PropertyType.Boolean, new StorePropInfo("NonReceiptNotificationRequested", 3078, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014AA RID: 5290
			public static readonly StorePropTag NonDeliveryReportRequested = new StorePropTag(3080, PropertyType.Boolean, new StorePropInfo("NonDeliveryReportRequested", 3080, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014AB RID: 5291
			public static readonly StorePropTag OriginatorRequestedAlterateRecipient = new StorePropTag(3081, PropertyType.Binary, new StorePropInfo("OriginatorRequestedAlterateRecipient", 3081, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014AC RID: 5292
			public static readonly StorePropTag PhysicalDeliveryMode = new StorePropTag(3083, PropertyType.Int32, new StorePropInfo("PhysicalDeliveryMode", 3083, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014AD RID: 5293
			public static readonly StorePropTag PhysicalDeliveryReportRequest = new StorePropTag(3084, PropertyType.Int32, new StorePropInfo("PhysicalDeliveryReportRequest", 3084, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014AE RID: 5294
			public static readonly StorePropTag PhysicalForwardingAddress = new StorePropTag(3085, PropertyType.Binary, new StorePropInfo("PhysicalForwardingAddress", 3085, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014AF RID: 5295
			public static readonly StorePropTag PhysicalForwardingAddressRequested = new StorePropTag(3086, PropertyType.Boolean, new StorePropInfo("PhysicalForwardingAddressRequested", 3086, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014B0 RID: 5296
			public static readonly StorePropTag PhysicalForwardingProhibited = new StorePropTag(3087, PropertyType.Boolean, new StorePropInfo("PhysicalForwardingProhibited", 3087, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014B1 RID: 5297
			public static readonly StorePropTag ProofOfDelivery = new StorePropTag(3089, PropertyType.Binary, new StorePropInfo("ProofOfDelivery", 3089, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014B2 RID: 5298
			public static readonly StorePropTag ProofOfDeliveryRequested = new StorePropTag(3090, PropertyType.Boolean, new StorePropInfo("ProofOfDeliveryRequested", 3090, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014B3 RID: 5299
			public static readonly StorePropTag RecipientCertificate = new StorePropTag(3091, PropertyType.Binary, new StorePropInfo("RecipientCertificate", 3091, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014B4 RID: 5300
			public static readonly StorePropTag RecipientNumberForAdvice = new StorePropTag(3092, PropertyType.Unicode, new StorePropInfo("RecipientNumberForAdvice", 3092, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014B5 RID: 5301
			public static readonly StorePropTag RecipientType = new StorePropTag(3093, PropertyType.Int32, new StorePropInfo("RecipientType", 3093, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014B6 RID: 5302
			public static readonly StorePropTag TypeOfMTSUser = new StorePropTag(3100, PropertyType.Int32, new StorePropInfo("TypeOfMTSUser", 3100, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014B7 RID: 5303
			public static readonly StorePropTag DiscreteValues = new StorePropTag(3598, PropertyType.Boolean, new StorePropInfo("DiscreteValues", 3598, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014B8 RID: 5304
			public static readonly StorePropTag Responsibility = new StorePropTag(3599, PropertyType.Boolean, new StorePropInfo("Responsibility", 3599, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014B9 RID: 5305
			public static readonly StorePropTag RecipientStatus = new StorePropTag(3605, PropertyType.Int32, new StorePropInfo("RecipientStatus", 3605, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014BA RID: 5306
			public static readonly StorePropTag InstanceKey = new StorePropTag(4086, PropertyType.Binary, new StorePropInfo("InstanceKey", 4086, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014BB RID: 5307
			public static readonly StorePropTag AccessLevel = new StorePropTag(4087, PropertyType.Int32, new StorePropInfo("AccessLevel", 4087, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014BC RID: 5308
			public static readonly StorePropTag RecordKey = new StorePropTag(4089, PropertyType.Binary, new StorePropInfo("RecordKey", 4089, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014BD RID: 5309
			public static readonly StorePropTag RecordKeySvrEid = new StorePropTag(4089, PropertyType.SvrEid, new StorePropInfo("RecordKeySvrEid", 4089, PropertyType.SvrEid, StorePropInfo.Flags.None, 0UL, new PropertyCategories(9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014BE RID: 5310
			public static readonly StorePropTag ObjectType = new StorePropTag(4094, PropertyType.Int32, new StorePropInfo("ObjectType", 4094, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014BF RID: 5311
			public static readonly StorePropTag EntryId = new StorePropTag(4095, PropertyType.Binary, new StorePropInfo("EntryId", 4095, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014C0 RID: 5312
			public static readonly StorePropTag EntryIdSvrEid = new StorePropTag(4095, PropertyType.SvrEid, new StorePropInfo("EntryIdSvrEid", 4095, PropertyType.SvrEid, StorePropInfo.Flags.None, 0UL, new PropertyCategories(9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014C1 RID: 5313
			public static readonly StorePropTag RowId = new StorePropTag(12288, PropertyType.Int32, new StorePropInfo("RowId", 12288, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014C2 RID: 5314
			public static readonly StorePropTag DisplayName = new StorePropTag(12289, PropertyType.Unicode, new StorePropInfo("DisplayName", 12289, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014C3 RID: 5315
			public static readonly StorePropTag AddressType = new StorePropTag(12290, PropertyType.Unicode, new StorePropInfo("AddressType", 12290, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014C4 RID: 5316
			public static readonly StorePropTag EmailAddress = new StorePropTag(12291, PropertyType.Unicode, new StorePropInfo("EmailAddress", 12291, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014C5 RID: 5317
			public static readonly StorePropTag Comment = new StorePropTag(12292, PropertyType.Unicode, new StorePropInfo("Comment", 12292, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014C6 RID: 5318
			public static readonly StorePropTag LastModificationTime = new StorePropTag(12296, PropertyType.SysTime, new StorePropInfo("LastModificationTime", 12296, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014C7 RID: 5319
			public static readonly StorePropTag SearchKey = new StorePropTag(12299, PropertyType.Binary, new StorePropInfo("SearchKey", 12299, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014C8 RID: 5320
			public static readonly StorePropTag SearchKeySvrEid = new StorePropTag(12299, PropertyType.SvrEid, new StorePropInfo("SearchKeySvrEid", 12299, PropertyType.SvrEid, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014C9 RID: 5321
			public static readonly StorePropTag DetailsTable = new StorePropTag(13829, PropertyType.Object, new StorePropInfo("DetailsTable", 13829, PropertyType.Object, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014CA RID: 5322
			public static readonly StorePropTag DisplayType = new StorePropTag(14592, PropertyType.Int32, new StorePropInfo("DisplayType", 14592, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014CB RID: 5323
			public static readonly StorePropTag SmtpAddress = new StorePropTag(14846, PropertyType.Unicode, new StorePropInfo("SmtpAddress", 14846, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014CC RID: 5324
			public static readonly StorePropTag SimpleDisplayName = new StorePropTag(14847, PropertyType.Unicode, new StorePropInfo("SimpleDisplayName", 14847, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014CD RID: 5325
			public static readonly StorePropTag Account = new StorePropTag(14848, PropertyType.Unicode, new StorePropInfo("Account", 14848, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014CE RID: 5326
			public static readonly StorePropTag AlternateRecipient = new StorePropTag(14849, PropertyType.Binary, new StorePropInfo("AlternateRecipient", 14849, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014CF RID: 5327
			public static readonly StorePropTag CallbackTelephoneNumber = new StorePropTag(14850, PropertyType.Unicode, new StorePropInfo("CallbackTelephoneNumber", 14850, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014D0 RID: 5328
			public static readonly StorePropTag Generation = new StorePropTag(14853, PropertyType.Unicode, new StorePropInfo("Generation", 14853, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014D1 RID: 5329
			public static readonly StorePropTag GivenName = new StorePropTag(14854, PropertyType.Unicode, new StorePropInfo("GivenName", 14854, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014D2 RID: 5330
			public static readonly StorePropTag GovernmentIDNumber = new StorePropTag(14855, PropertyType.Unicode, new StorePropInfo("GovernmentIDNumber", 14855, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014D3 RID: 5331
			public static readonly StorePropTag BusinessTelephoneNumber = new StorePropTag(14856, PropertyType.Unicode, new StorePropInfo("BusinessTelephoneNumber", 14856, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014D4 RID: 5332
			public static readonly StorePropTag HomeTelephoneNumber = new StorePropTag(14857, PropertyType.Unicode, new StorePropInfo("HomeTelephoneNumber", 14857, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014D5 RID: 5333
			public static readonly StorePropTag Initials = new StorePropTag(14858, PropertyType.Unicode, new StorePropInfo("Initials", 14858, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014D6 RID: 5334
			public static readonly StorePropTag Keyword = new StorePropTag(14859, PropertyType.Unicode, new StorePropInfo("Keyword", 14859, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014D7 RID: 5335
			public static readonly StorePropTag Language = new StorePropTag(14860, PropertyType.Unicode, new StorePropInfo("Language", 14860, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014D8 RID: 5336
			public static readonly StorePropTag Location = new StorePropTag(14861, PropertyType.Unicode, new StorePropInfo("Location", 14861, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014D9 RID: 5337
			public static readonly StorePropTag MailPermission = new StorePropTag(14862, PropertyType.Boolean, new StorePropInfo("MailPermission", 14862, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014DA RID: 5338
			public static readonly StorePropTag OrganizationalIDNumber = new StorePropTag(14864, PropertyType.Unicode, new StorePropInfo("OrganizationalIDNumber", 14864, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014DB RID: 5339
			public static readonly StorePropTag SurName = new StorePropTag(14865, PropertyType.Unicode, new StorePropInfo("SurName", 14865, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014DC RID: 5340
			public static readonly StorePropTag OriginalEntryId = new StorePropTag(14866, PropertyType.Binary, new StorePropInfo("OriginalEntryId", 14866, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014DD RID: 5341
			public static readonly StorePropTag OriginalDisplayName = new StorePropTag(14867, PropertyType.Unicode, new StorePropInfo("OriginalDisplayName", 14867, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014DE RID: 5342
			public static readonly StorePropTag OriginalSearchKey = new StorePropTag(14868, PropertyType.Binary, new StorePropInfo("OriginalSearchKey", 14868, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014DF RID: 5343
			public static readonly StorePropTag PostalAddress = new StorePropTag(14869, PropertyType.Unicode, new StorePropInfo("PostalAddress", 14869, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014E0 RID: 5344
			public static readonly StorePropTag CompanyName = new StorePropTag(14870, PropertyType.Unicode, new StorePropInfo("CompanyName", 14870, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014E1 RID: 5345
			public static readonly StorePropTag Title = new StorePropTag(14871, PropertyType.Unicode, new StorePropInfo("Title", 14871, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014E2 RID: 5346
			public static readonly StorePropTag DepartmentName = new StorePropTag(14872, PropertyType.Unicode, new StorePropInfo("DepartmentName", 14872, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014E3 RID: 5347
			public static readonly StorePropTag OfficeLocation = new StorePropTag(14873, PropertyType.Unicode, new StorePropInfo("OfficeLocation", 14873, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014E4 RID: 5348
			public static readonly StorePropTag PrimaryTelephoneNumber = new StorePropTag(14874, PropertyType.Unicode, new StorePropInfo("PrimaryTelephoneNumber", 14874, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014E5 RID: 5349
			public static readonly StorePropTag Business2TelephoneNumber = new StorePropTag(14875, PropertyType.Unicode, new StorePropInfo("Business2TelephoneNumber", 14875, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014E6 RID: 5350
			public static readonly StorePropTag Business2TelephoneNumberMv = new StorePropTag(14875, PropertyType.MVUnicode, new StorePropInfo("Business2TelephoneNumberMv", 14875, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014E7 RID: 5351
			public static readonly StorePropTag MobileTelephoneNumber = new StorePropTag(14876, PropertyType.Unicode, new StorePropInfo("MobileTelephoneNumber", 14876, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014E8 RID: 5352
			public static readonly StorePropTag RadioTelephoneNumber = new StorePropTag(14877, PropertyType.Unicode, new StorePropInfo("RadioTelephoneNumber", 14877, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014E9 RID: 5353
			public static readonly StorePropTag CarTelephoneNumber = new StorePropTag(14878, PropertyType.Unicode, new StorePropInfo("CarTelephoneNumber", 14878, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014EA RID: 5354
			public static readonly StorePropTag OtherTelephoneNumber = new StorePropTag(14879, PropertyType.Unicode, new StorePropInfo("OtherTelephoneNumber", 14879, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014EB RID: 5355
			public static readonly StorePropTag TransmitableDisplayName = new StorePropTag(14880, PropertyType.Unicode, new StorePropInfo("TransmitableDisplayName", 14880, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014EC RID: 5356
			public static readonly StorePropTag PagerTelephoneNumber = new StorePropTag(14881, PropertyType.Unicode, new StorePropInfo("PagerTelephoneNumber", 14881, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014ED RID: 5357
			public static readonly StorePropTag UserCertificate = new StorePropTag(14882, PropertyType.Binary, new StorePropInfo("UserCertificate", 14882, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014EE RID: 5358
			public static readonly StorePropTag PrimaryFaxNumber = new StorePropTag(14883, PropertyType.Unicode, new StorePropInfo("PrimaryFaxNumber", 14883, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014EF RID: 5359
			public static readonly StorePropTag BusinessFaxNumber = new StorePropTag(14884, PropertyType.Unicode, new StorePropInfo("BusinessFaxNumber", 14884, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014F0 RID: 5360
			public static readonly StorePropTag HomeFaxNumber = new StorePropTag(14885, PropertyType.Unicode, new StorePropInfo("HomeFaxNumber", 14885, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014F1 RID: 5361
			public static readonly StorePropTag Country = new StorePropTag(14886, PropertyType.Unicode, new StorePropInfo("Country", 14886, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014F2 RID: 5362
			public static readonly StorePropTag Locality = new StorePropTag(14887, PropertyType.Unicode, new StorePropInfo("Locality", 14887, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014F3 RID: 5363
			public static readonly StorePropTag StateOrProvince = new StorePropTag(14888, PropertyType.Unicode, new StorePropInfo("StateOrProvince", 14888, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014F4 RID: 5364
			public static readonly StorePropTag StreetAddress = new StorePropTag(14889, PropertyType.Unicode, new StorePropInfo("StreetAddress", 14889, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014F5 RID: 5365
			public static readonly StorePropTag PostalCode = new StorePropTag(14890, PropertyType.Unicode, new StorePropInfo("PostalCode", 14890, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014F6 RID: 5366
			public static readonly StorePropTag PostOfficeBox = new StorePropTag(14891, PropertyType.Unicode, new StorePropInfo("PostOfficeBox", 14891, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014F7 RID: 5367
			public static readonly StorePropTag TelexNumber = new StorePropTag(14892, PropertyType.Unicode, new StorePropInfo("TelexNumber", 14892, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014F8 RID: 5368
			public static readonly StorePropTag ISDNNumber = new StorePropTag(14893, PropertyType.Unicode, new StorePropInfo("ISDNNumber", 14893, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014F9 RID: 5369
			public static readonly StorePropTag AssistantTelephoneNumber = new StorePropTag(14894, PropertyType.Unicode, new StorePropInfo("AssistantTelephoneNumber", 14894, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014FA RID: 5370
			public static readonly StorePropTag Home2TelephoneNumber = new StorePropTag(14895, PropertyType.Unicode, new StorePropInfo("Home2TelephoneNumber", 14895, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014FB RID: 5371
			public static readonly StorePropTag Home2TelephoneNumberMv = new StorePropTag(14895, PropertyType.MVUnicode, new StorePropInfo("Home2TelephoneNumberMv", 14895, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014FC RID: 5372
			public static readonly StorePropTag Assistant = new StorePropTag(14896, PropertyType.Unicode, new StorePropInfo("Assistant", 14896, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014FD RID: 5373
			public static readonly StorePropTag SendRichInfo = new StorePropTag(14912, PropertyType.Boolean, new StorePropInfo("SendRichInfo", 14912, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014FE RID: 5374
			public static readonly StorePropTag WeddingAnniversary = new StorePropTag(14913, PropertyType.SysTime, new StorePropInfo("WeddingAnniversary", 14913, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x040014FF RID: 5375
			public static readonly StorePropTag Birthday = new StorePropTag(14914, PropertyType.SysTime, new StorePropInfo("Birthday", 14914, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001500 RID: 5376
			public static readonly StorePropTag Hobbies = new StorePropTag(14915, PropertyType.Unicode, new StorePropInfo("Hobbies", 14915, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001501 RID: 5377
			public static readonly StorePropTag MiddleName = new StorePropTag(14916, PropertyType.Unicode, new StorePropInfo("MiddleName", 14916, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001502 RID: 5378
			public static readonly StorePropTag DisplayNamePrefix = new StorePropTag(14917, PropertyType.Unicode, new StorePropInfo("DisplayNamePrefix", 14917, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001503 RID: 5379
			public static readonly StorePropTag Profession = new StorePropTag(14918, PropertyType.Unicode, new StorePropInfo("Profession", 14918, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001504 RID: 5380
			public static readonly StorePropTag ReferredByName = new StorePropTag(14919, PropertyType.Unicode, new StorePropInfo("ReferredByName", 14919, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001505 RID: 5381
			public static readonly StorePropTag SpouseName = new StorePropTag(14920, PropertyType.Unicode, new StorePropInfo("SpouseName", 14920, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001506 RID: 5382
			public static readonly StorePropTag ComputerNetworkName = new StorePropTag(14921, PropertyType.Unicode, new StorePropInfo("ComputerNetworkName", 14921, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001507 RID: 5383
			public static readonly StorePropTag CustomerId = new StorePropTag(14922, PropertyType.Unicode, new StorePropInfo("CustomerId", 14922, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001508 RID: 5384
			public static readonly StorePropTag TTYTDDPhoneNumber = new StorePropTag(14923, PropertyType.Unicode, new StorePropInfo("TTYTDDPhoneNumber", 14923, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001509 RID: 5385
			public static readonly StorePropTag FTPSite = new StorePropTag(14924, PropertyType.Unicode, new StorePropInfo("FTPSite", 14924, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400150A RID: 5386
			public static readonly StorePropTag Gender = new StorePropTag(14925, PropertyType.Int16, new StorePropInfo("Gender", 14925, PropertyType.Int16, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400150B RID: 5387
			public static readonly StorePropTag ManagerName = new StorePropTag(14926, PropertyType.Unicode, new StorePropInfo("ManagerName", 14926, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400150C RID: 5388
			public static readonly StorePropTag NickName = new StorePropTag(14927, PropertyType.Unicode, new StorePropInfo("NickName", 14927, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400150D RID: 5389
			public static readonly StorePropTag PersonalHomePage = new StorePropTag(14928, PropertyType.Unicode, new StorePropInfo("PersonalHomePage", 14928, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400150E RID: 5390
			public static readonly StorePropTag BusinessHomePage = new StorePropTag(14929, PropertyType.Unicode, new StorePropInfo("BusinessHomePage", 14929, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400150F RID: 5391
			public static readonly StorePropTag ContactVersion = new StorePropTag(14930, PropertyType.Guid, new StorePropInfo("ContactVersion", 14930, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001510 RID: 5392
			public static readonly StorePropTag ContactEntryIds = new StorePropTag(14931, PropertyType.MVBinary, new StorePropInfo("ContactEntryIds", 14931, PropertyType.MVBinary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001511 RID: 5393
			public static readonly StorePropTag ContactAddressTypes = new StorePropTag(14932, PropertyType.MVUnicode, new StorePropInfo("ContactAddressTypes", 14932, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001512 RID: 5394
			public static readonly StorePropTag ContactDefaultAddressIndex = new StorePropTag(14933, PropertyType.Int32, new StorePropInfo("ContactDefaultAddressIndex", 14933, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001513 RID: 5395
			public static readonly StorePropTag ContactEmailAddress = new StorePropTag(14934, PropertyType.MVUnicode, new StorePropInfo("ContactEmailAddress", 14934, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001514 RID: 5396
			public static readonly StorePropTag CompanyMainPhoneNumber = new StorePropTag(14935, PropertyType.Unicode, new StorePropInfo("CompanyMainPhoneNumber", 14935, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001515 RID: 5397
			public static readonly StorePropTag ChildrensNames = new StorePropTag(14936, PropertyType.MVUnicode, new StorePropInfo("ChildrensNames", 14936, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001516 RID: 5398
			public static readonly StorePropTag HomeAddressCity = new StorePropTag(14937, PropertyType.Unicode, new StorePropInfo("HomeAddressCity", 14937, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001517 RID: 5399
			public static readonly StorePropTag HomeAddressCountry = new StorePropTag(14938, PropertyType.Unicode, new StorePropInfo("HomeAddressCountry", 14938, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001518 RID: 5400
			public static readonly StorePropTag HomeAddressPostalCode = new StorePropTag(14939, PropertyType.Unicode, new StorePropInfo("HomeAddressPostalCode", 14939, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001519 RID: 5401
			public static readonly StorePropTag HomeAddressStateOrProvince = new StorePropTag(14940, PropertyType.Unicode, new StorePropInfo("HomeAddressStateOrProvince", 14940, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400151A RID: 5402
			public static readonly StorePropTag HomeAddressStreet = new StorePropTag(14941, PropertyType.Unicode, new StorePropInfo("HomeAddressStreet", 14941, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400151B RID: 5403
			public static readonly StorePropTag HomeAddressPostOfficeBox = new StorePropTag(14942, PropertyType.Unicode, new StorePropInfo("HomeAddressPostOfficeBox", 14942, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400151C RID: 5404
			public static readonly StorePropTag OtherAddressCity = new StorePropTag(14943, PropertyType.Unicode, new StorePropInfo("OtherAddressCity", 14943, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400151D RID: 5405
			public static readonly StorePropTag OtherAddressCountry = new StorePropTag(14944, PropertyType.Unicode, new StorePropInfo("OtherAddressCountry", 14944, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400151E RID: 5406
			public static readonly StorePropTag OtherAddressPostalCode = new StorePropTag(14945, PropertyType.Unicode, new StorePropInfo("OtherAddressPostalCode", 14945, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400151F RID: 5407
			public static readonly StorePropTag OtherAddressStateOrProvince = new StorePropTag(14946, PropertyType.Unicode, new StorePropInfo("OtherAddressStateOrProvince", 14946, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001520 RID: 5408
			public static readonly StorePropTag OtherAddressStreet = new StorePropTag(14947, PropertyType.Unicode, new StorePropInfo("OtherAddressStreet", 14947, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001521 RID: 5409
			public static readonly StorePropTag OtherAddressPostOfficeBox = new StorePropTag(14948, PropertyType.Unicode, new StorePropInfo("OtherAddressPostOfficeBox", 14948, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001522 RID: 5410
			public static readonly StorePropTag UserX509CertificateABSearchPath = new StorePropTag(14960, PropertyType.MVBinary, new StorePropInfo("UserX509CertificateABSearchPath", 14960, PropertyType.MVBinary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001523 RID: 5411
			public static readonly StorePropTag SendInternetEncoding = new StorePropTag(14961, PropertyType.Int32, new StorePropInfo("SendInternetEncoding", 14961, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001524 RID: 5412
			public static readonly StorePropTag PartnerNetworkId = new StorePropTag(14966, PropertyType.Unicode, new StorePropInfo("PartnerNetworkId", 14966, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001525 RID: 5413
			public static readonly StorePropTag PartnerNetworkUserId = new StorePropTag(14967, PropertyType.Unicode, new StorePropInfo("PartnerNetworkUserId", 14967, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001526 RID: 5414
			public static readonly StorePropTag PartnerNetworkThumbnailPhotoUrl = new StorePropTag(14968, PropertyType.Unicode, new StorePropInfo("PartnerNetworkThumbnailPhotoUrl", 14968, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001527 RID: 5415
			public static readonly StorePropTag PartnerNetworkProfilePhotoUrl = new StorePropTag(14969, PropertyType.Unicode, new StorePropInfo("PartnerNetworkProfilePhotoUrl", 14969, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001528 RID: 5416
			public static readonly StorePropTag PartnerNetworkContactType = new StorePropTag(14970, PropertyType.Unicode, new StorePropInfo("PartnerNetworkContactType", 14970, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001529 RID: 5417
			public static readonly StorePropTag RelevanceScore = new StorePropTag(14971, PropertyType.Int32, new StorePropInfo("RelevanceScore", 14971, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400152A RID: 5418
			public static readonly StorePropTag IsDistributionListContact = new StorePropTag(14972, PropertyType.Boolean, new StorePropInfo("IsDistributionListContact", 14972, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400152B RID: 5419
			public static readonly StorePropTag IsPromotedContact = new StorePropTag(14973, PropertyType.Boolean, new StorePropInfo("IsPromotedContact", 14973, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400152C RID: 5420
			public static readonly StorePropTag OrgUnitName = new StorePropTag(15358, PropertyType.Unicode, new StorePropInfo("OrgUnitName", 15358, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400152D RID: 5421
			public static readonly StorePropTag OrganizationName = new StorePropTag(15359, PropertyType.Unicode, new StorePropInfo("OrganizationName", 15359, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400152E RID: 5422
			public static readonly StorePropTag TestBlobProperty = new StorePropTag(15616, PropertyType.Int64, new StorePropInfo("TestBlobProperty", 15616, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400152F RID: 5423
			public static readonly StorePropTag NewAttach = new StorePropTag(16384, PropertyType.Int32, new StorePropInfo("NewAttach", 16384, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001530 RID: 5424
			public static readonly StorePropTag StartEmbed = new StorePropTag(16385, PropertyType.Int32, new StorePropInfo("StartEmbed", 16385, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001531 RID: 5425
			public static readonly StorePropTag EndEmbed = new StorePropTag(16386, PropertyType.Int32, new StorePropInfo("EndEmbed", 16386, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001532 RID: 5426
			public static readonly StorePropTag StartRecip = new StorePropTag(16387, PropertyType.Int32, new StorePropInfo("StartRecip", 16387, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001533 RID: 5427
			public static readonly StorePropTag EndRecip = new StorePropTag(16388, PropertyType.Int32, new StorePropInfo("EndRecip", 16388, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001534 RID: 5428
			public static readonly StorePropTag EndCcRecip = new StorePropTag(16389, PropertyType.Int32, new StorePropInfo("EndCcRecip", 16389, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001535 RID: 5429
			public static readonly StorePropTag EndBccRecip = new StorePropTag(16390, PropertyType.Int32, new StorePropInfo("EndBccRecip", 16390, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001536 RID: 5430
			public static readonly StorePropTag EndP1Recip = new StorePropTag(16391, PropertyType.Int32, new StorePropInfo("EndP1Recip", 16391, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001537 RID: 5431
			public static readonly StorePropTag DNPrefix = new StorePropTag(16392, PropertyType.Unicode, new StorePropInfo("DNPrefix", 16392, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001538 RID: 5432
			public static readonly StorePropTag StartTopFolder = new StorePropTag(16393, PropertyType.Int32, new StorePropInfo("StartTopFolder", 16393, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001539 RID: 5433
			public static readonly StorePropTag StartSubFolder = new StorePropTag(16394, PropertyType.Int32, new StorePropInfo("StartSubFolder", 16394, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400153A RID: 5434
			public static readonly StorePropTag EndFolder = new StorePropTag(16395, PropertyType.Int32, new StorePropInfo("EndFolder", 16395, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400153B RID: 5435
			public static readonly StorePropTag StartMessage = new StorePropTag(16396, PropertyType.Int32, new StorePropInfo("StartMessage", 16396, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400153C RID: 5436
			public static readonly StorePropTag EndMessage = new StorePropTag(16397, PropertyType.Int32, new StorePropInfo("EndMessage", 16397, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400153D RID: 5437
			public static readonly StorePropTag EndAttach = new StorePropTag(16398, PropertyType.Int32, new StorePropInfo("EndAttach", 16398, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400153E RID: 5438
			public static readonly StorePropTag EcWarning = new StorePropTag(16399, PropertyType.Int32, new StorePropInfo("EcWarning", 16399, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400153F RID: 5439
			public static readonly StorePropTag StartFAIMessage = new StorePropTag(16400, PropertyType.Int32, new StorePropInfo("StartFAIMessage", 16400, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001540 RID: 5440
			public static readonly StorePropTag NewFXFolder = new StorePropTag(16401, PropertyType.Binary, new StorePropInfo("NewFXFolder", 16401, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001541 RID: 5441
			public static readonly StorePropTag IncrSyncChange = new StorePropTag(16402, PropertyType.Int32, new StorePropInfo("IncrSyncChange", 16402, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001542 RID: 5442
			public static readonly StorePropTag IncrSyncDelete = new StorePropTag(16403, PropertyType.Int32, new StorePropInfo("IncrSyncDelete", 16403, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001543 RID: 5443
			public static readonly StorePropTag IncrSyncEnd = new StorePropTag(16404, PropertyType.Int32, new StorePropInfo("IncrSyncEnd", 16404, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001544 RID: 5444
			public static readonly StorePropTag IncrSyncMessage = new StorePropTag(16405, PropertyType.Int32, new StorePropInfo("IncrSyncMessage", 16405, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001545 RID: 5445
			public static readonly StorePropTag FastTransferDelProp = new StorePropTag(16406, PropertyType.Int32, new StorePropInfo("FastTransferDelProp", 16406, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001546 RID: 5446
			public static readonly StorePropTag IdsetGiven = new StorePropTag(16407, PropertyType.Binary, new StorePropInfo("IdsetGiven", 16407, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001547 RID: 5447
			public static readonly StorePropTag IdsetGivenInt32 = new StorePropTag(16407, PropertyType.Int32, new StorePropInfo("IdsetGivenInt32", 16407, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001548 RID: 5448
			public static readonly StorePropTag FastTransferErrorInfo = new StorePropTag(16408, PropertyType.Int32, new StorePropInfo("FastTransferErrorInfo", 16408, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001549 RID: 5449
			public static readonly StorePropTag SoftDeletes = new StorePropTag(16417, PropertyType.Binary, new StorePropInfo("SoftDeletes", 16417, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400154A RID: 5450
			public static readonly StorePropTag IdsetRead = new StorePropTag(16429, PropertyType.Binary, new StorePropInfo("IdsetRead", 16429, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400154B RID: 5451
			public static readonly StorePropTag IdsetUnread = new StorePropTag(16430, PropertyType.Binary, new StorePropInfo("IdsetUnread", 16430, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400154C RID: 5452
			public static readonly StorePropTag IncrSyncRead = new StorePropTag(16431, PropertyType.Int32, new StorePropInfo("IncrSyncRead", 16431, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400154D RID: 5453
			public static readonly StorePropTag IncrSyncStateBegin = new StorePropTag(16442, PropertyType.Int32, new StorePropInfo("IncrSyncStateBegin", 16442, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400154E RID: 5454
			public static readonly StorePropTag IncrSyncStateEnd = new StorePropTag(16443, PropertyType.Int32, new StorePropInfo("IncrSyncStateEnd", 16443, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400154F RID: 5455
			public static readonly StorePropTag IncrSyncImailStream = new StorePropTag(16444, PropertyType.Int32, new StorePropInfo("IncrSyncImailStream", 16444, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001550 RID: 5456
			public static readonly StorePropTag IncrSyncImailStreamContinue = new StorePropTag(16486, PropertyType.Int32, new StorePropInfo("IncrSyncImailStreamContinue", 16486, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001551 RID: 5457
			public static readonly StorePropTag IncrSyncImailStreamCancel = new StorePropTag(16487, PropertyType.Int32, new StorePropInfo("IncrSyncImailStreamCancel", 16487, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001552 RID: 5458
			public static readonly StorePropTag IncrSyncImailStream2Continue = new StorePropTag(16497, PropertyType.Int32, new StorePropInfo("IncrSyncImailStream2Continue", 16497, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001553 RID: 5459
			public static readonly StorePropTag IncrSyncProgressMode = new StorePropTag(16500, PropertyType.Boolean, new StorePropInfo("IncrSyncProgressMode", 16500, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001554 RID: 5460
			public static readonly StorePropTag SyncProgressPerMsg = new StorePropTag(16501, PropertyType.Boolean, new StorePropInfo("SyncProgressPerMsg", 16501, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001555 RID: 5461
			public static readonly StorePropTag IncrSyncMsgPartial = new StorePropTag(16506, PropertyType.Int32, new StorePropInfo("IncrSyncMsgPartial", 16506, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001556 RID: 5462
			public static readonly StorePropTag IncrSyncGroupInfo = new StorePropTag(16507, PropertyType.Int32, new StorePropInfo("IncrSyncGroupInfo", 16507, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001557 RID: 5463
			public static readonly StorePropTag IncrSyncGroupId = new StorePropTag(16508, PropertyType.Int32, new StorePropInfo("IncrSyncGroupId", 16508, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001558 RID: 5464
			public static readonly StorePropTag IncrSyncChangePartial = new StorePropTag(16509, PropertyType.Int32, new StorePropInfo("IncrSyncChangePartial", 16509, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001559 RID: 5465
			public static readonly StorePropTag RecipientOrder = new StorePropTag(24543, PropertyType.Int32, new StorePropInfo("RecipientOrder", 24543, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400155A RID: 5466
			public static readonly StorePropTag RecipientSipUri = new StorePropTag(24549, PropertyType.Unicode, new StorePropInfo("RecipientSipUri", 24549, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400155B RID: 5467
			public static readonly StorePropTag RecipientDisplayName = new StorePropTag(24566, PropertyType.Unicode, new StorePropInfo("RecipientDisplayName", 24566, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400155C RID: 5468
			public static readonly StorePropTag RecipientEntryId = new StorePropTag(24567, PropertyType.Binary, new StorePropInfo("RecipientEntryId", 24567, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400155D RID: 5469
			public static readonly StorePropTag RecipientFlags = new StorePropTag(24573, PropertyType.Int32, new StorePropInfo("RecipientFlags", 24573, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400155E RID: 5470
			public static readonly StorePropTag RecipientTrackStatus = new StorePropTag(24575, PropertyType.Int32, new StorePropInfo("RecipientTrackStatus", 24575, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x0400155F RID: 5471
			public static readonly StorePropTag DotStuffState = new StorePropTag(24577, PropertyType.Unicode, new StorePropInfo("DotStuffState", 24577, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001560 RID: 5472
			public static readonly StorePropTag RecipientNumber = new StorePropTag(26210, PropertyType.Int32, new StorePropInfo("RecipientNumber", 26210, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001561 RID: 5473
			public static readonly StorePropTag UserDN = new StorePropTag(26314, PropertyType.Unicode, new StorePropInfo("UserDN", 26314, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001562 RID: 5474
			public static readonly StorePropTag CnsetSeen = new StorePropTag(26518, PropertyType.Binary, new StorePropInfo("CnsetSeen", 26518, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001563 RID: 5475
			public static readonly StorePropTag SourceEntryId = new StorePropTag(26536, PropertyType.Binary, new StorePropInfo("SourceEntryId", 26536, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001564 RID: 5476
			public static readonly StorePropTag CnsetRead = new StorePropTag(26578, PropertyType.Binary, new StorePropInfo("CnsetRead", 26578, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001565 RID: 5477
			public static readonly StorePropTag CnsetSeenFAI = new StorePropTag(26586, PropertyType.Binary, new StorePropInfo("CnsetSeenFAI", 26586, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001566 RID: 5478
			public static readonly StorePropTag IdSetDeleted = new StorePropTag(26597, PropertyType.Binary, new StorePropInfo("IdSetDeleted", 26597, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 9)), Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Recipient);

			// Token: 0x04001567 RID: 5479
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[0];

			// Token: 0x04001568 RID: 5480
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[0];

			// Token: 0x04001569 RID: 5481
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[0];

			// Token: 0x0400156A RID: 5482
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[]
			{
				PropTag.Recipient.NewAttach,
				PropTag.Recipient.StartEmbed,
				PropTag.Recipient.EndEmbed,
				PropTag.Recipient.StartRecip,
				PropTag.Recipient.EndRecip,
				PropTag.Recipient.EndCcRecip,
				PropTag.Recipient.EndBccRecip,
				PropTag.Recipient.EndP1Recip,
				PropTag.Recipient.DNPrefix,
				PropTag.Recipient.StartTopFolder,
				PropTag.Recipient.StartSubFolder,
				PropTag.Recipient.EndFolder,
				PropTag.Recipient.StartMessage,
				PropTag.Recipient.EndMessage,
				PropTag.Recipient.EndAttach,
				PropTag.Recipient.EcWarning,
				PropTag.Recipient.StartFAIMessage,
				PropTag.Recipient.NewFXFolder,
				PropTag.Recipient.IncrSyncChange,
				PropTag.Recipient.IncrSyncDelete,
				PropTag.Recipient.IncrSyncEnd,
				PropTag.Recipient.IncrSyncMessage,
				PropTag.Recipient.FastTransferDelProp,
				PropTag.Recipient.IdsetGiven,
				PropTag.Recipient.IdsetGivenInt32,
				PropTag.Recipient.FastTransferErrorInfo,
				PropTag.Recipient.SoftDeletes,
				PropTag.Recipient.IdsetRead,
				PropTag.Recipient.IdsetUnread,
				PropTag.Recipient.IncrSyncRead,
				PropTag.Recipient.IncrSyncStateBegin,
				PropTag.Recipient.IncrSyncStateEnd,
				PropTag.Recipient.IncrSyncImailStream,
				PropTag.Recipient.IncrSyncImailStreamContinue,
				PropTag.Recipient.IncrSyncImailStreamCancel,
				PropTag.Recipient.IncrSyncImailStream2Continue,
				PropTag.Recipient.IncrSyncProgressMode,
				PropTag.Recipient.SyncProgressPerMsg,
				PropTag.Recipient.IncrSyncMsgPartial,
				PropTag.Recipient.IncrSyncGroupInfo,
				PropTag.Recipient.IncrSyncGroupId,
				PropTag.Recipient.IncrSyncChangePartial,
				PropTag.Recipient.CnsetSeen,
				PropTag.Recipient.CnsetRead,
				PropTag.Recipient.CnsetSeenFAI,
				PropTag.Recipient.IdSetDeleted
			};

			// Token: 0x0400156B RID: 5483
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[0];

			// Token: 0x0400156C RID: 5484
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[0];

			// Token: 0x0400156D RID: 5485
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[0];

			// Token: 0x0400156E RID: 5486
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x0400156F RID: 5487
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x04001570 RID: 5488
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[]
			{
				PropTag.Recipient.RecordKey,
				PropTag.Recipient.RecordKeySvrEid,
				PropTag.Recipient.ObjectType,
				PropTag.Recipient.EntryId,
				PropTag.Recipient.EntryIdSvrEid,
				PropTag.Recipient.RowId,
				PropTag.Recipient.NewAttach,
				PropTag.Recipient.StartEmbed,
				PropTag.Recipient.EndEmbed,
				PropTag.Recipient.StartRecip,
				PropTag.Recipient.EndRecip,
				PropTag.Recipient.EndCcRecip,
				PropTag.Recipient.EndBccRecip,
				PropTag.Recipient.EndP1Recip,
				PropTag.Recipient.DNPrefix,
				PropTag.Recipient.StartTopFolder,
				PropTag.Recipient.StartSubFolder,
				PropTag.Recipient.EndFolder,
				PropTag.Recipient.StartMessage,
				PropTag.Recipient.EndMessage,
				PropTag.Recipient.EndAttach,
				PropTag.Recipient.EcWarning,
				PropTag.Recipient.StartFAIMessage,
				PropTag.Recipient.NewFXFolder,
				PropTag.Recipient.IncrSyncChange,
				PropTag.Recipient.IncrSyncDelete,
				PropTag.Recipient.IncrSyncEnd,
				PropTag.Recipient.IncrSyncMessage,
				PropTag.Recipient.FastTransferDelProp,
				PropTag.Recipient.IdsetGiven,
				PropTag.Recipient.IdsetGivenInt32,
				PropTag.Recipient.FastTransferErrorInfo,
				PropTag.Recipient.SoftDeletes,
				PropTag.Recipient.IdsetRead,
				PropTag.Recipient.IdsetUnread,
				PropTag.Recipient.IncrSyncRead,
				PropTag.Recipient.IncrSyncStateBegin,
				PropTag.Recipient.IncrSyncStateEnd,
				PropTag.Recipient.IncrSyncImailStream,
				PropTag.Recipient.IncrSyncImailStreamContinue,
				PropTag.Recipient.IncrSyncImailStreamCancel,
				PropTag.Recipient.IncrSyncImailStream2Continue,
				PropTag.Recipient.IncrSyncProgressMode,
				PropTag.Recipient.SyncProgressPerMsg,
				PropTag.Recipient.IncrSyncMsgPartial,
				PropTag.Recipient.IncrSyncGroupInfo,
				PropTag.Recipient.IncrSyncGroupId,
				PropTag.Recipient.IncrSyncChangePartial,
				PropTag.Recipient.CnsetSeen,
				PropTag.Recipient.CnsetRead,
				PropTag.Recipient.CnsetSeenFAI,
				PropTag.Recipient.IdSetDeleted
			};

			// Token: 0x04001571 RID: 5489
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[0];

			// Token: 0x04001572 RID: 5490
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x04001573 RID: 5491
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x04001574 RID: 5492
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x04001575 RID: 5493
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x04001576 RID: 5494
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x04001577 RID: 5495
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x04001578 RID: 5496
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x04001579 RID: 5497
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x0400157A RID: 5498
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.Recipient.DeliveryReportRequested,
				PropTag.Recipient.ReadReceiptRequested,
				PropTag.Recipient.ReportTime,
				PropTag.Recipient.DiscVal,
				PropTag.Recipient.ExplicitConversion,
				PropTag.Recipient.NDRReasonCode,
				PropTag.Recipient.NDRDiagCode,
				PropTag.Recipient.NonReceiptNotificationRequested,
				PropTag.Recipient.NonDeliveryReportRequested,
				PropTag.Recipient.OriginatorRequestedAlterateRecipient,
				PropTag.Recipient.PhysicalDeliveryMode,
				PropTag.Recipient.PhysicalDeliveryReportRequest,
				PropTag.Recipient.PhysicalForwardingAddress,
				PropTag.Recipient.PhysicalForwardingAddressRequested,
				PropTag.Recipient.PhysicalForwardingProhibited,
				PropTag.Recipient.ProofOfDelivery,
				PropTag.Recipient.ProofOfDeliveryRequested,
				PropTag.Recipient.RecipientCertificate,
				PropTag.Recipient.RecipientNumberForAdvice,
				PropTag.Recipient.RecipientType,
				PropTag.Recipient.TypeOfMTSUser,
				PropTag.Recipient.DiscreteValues,
				PropTag.Recipient.Responsibility,
				PropTag.Recipient.RecipientStatus,
				PropTag.Recipient.InstanceKey,
				PropTag.Recipient.AccessLevel,
				PropTag.Recipient.RecordKey,
				PropTag.Recipient.RecordKeySvrEid,
				PropTag.Recipient.ObjectType,
				PropTag.Recipient.EntryId,
				PropTag.Recipient.EntryIdSvrEid,
				PropTag.Recipient.RowId,
				PropTag.Recipient.DisplayName,
				PropTag.Recipient.AddressType,
				PropTag.Recipient.EmailAddress,
				PropTag.Recipient.Comment,
				PropTag.Recipient.LastModificationTime,
				PropTag.Recipient.SearchKey,
				PropTag.Recipient.SearchKeySvrEid,
				PropTag.Recipient.DetailsTable,
				PropTag.Recipient.DisplayType,
				PropTag.Recipient.SmtpAddress,
				PropTag.Recipient.SimpleDisplayName,
				PropTag.Recipient.Account,
				PropTag.Recipient.AlternateRecipient,
				PropTag.Recipient.CallbackTelephoneNumber,
				PropTag.Recipient.Generation,
				PropTag.Recipient.GivenName,
				PropTag.Recipient.GovernmentIDNumber,
				PropTag.Recipient.BusinessTelephoneNumber,
				PropTag.Recipient.HomeTelephoneNumber,
				PropTag.Recipient.Initials,
				PropTag.Recipient.Keyword,
				PropTag.Recipient.Language,
				PropTag.Recipient.Location,
				PropTag.Recipient.MailPermission,
				PropTag.Recipient.OrganizationalIDNumber,
				PropTag.Recipient.SurName,
				PropTag.Recipient.OriginalEntryId,
				PropTag.Recipient.OriginalDisplayName,
				PropTag.Recipient.OriginalSearchKey,
				PropTag.Recipient.PostalAddress,
				PropTag.Recipient.CompanyName,
				PropTag.Recipient.Title,
				PropTag.Recipient.DepartmentName,
				PropTag.Recipient.OfficeLocation,
				PropTag.Recipient.PrimaryTelephoneNumber,
				PropTag.Recipient.Business2TelephoneNumber,
				PropTag.Recipient.Business2TelephoneNumberMv,
				PropTag.Recipient.MobileTelephoneNumber,
				PropTag.Recipient.RadioTelephoneNumber,
				PropTag.Recipient.CarTelephoneNumber,
				PropTag.Recipient.OtherTelephoneNumber,
				PropTag.Recipient.TransmitableDisplayName,
				PropTag.Recipient.PagerTelephoneNumber,
				PropTag.Recipient.UserCertificate,
				PropTag.Recipient.PrimaryFaxNumber,
				PropTag.Recipient.BusinessFaxNumber,
				PropTag.Recipient.HomeFaxNumber,
				PropTag.Recipient.Country,
				PropTag.Recipient.Locality,
				PropTag.Recipient.StateOrProvince,
				PropTag.Recipient.StreetAddress,
				PropTag.Recipient.PostalCode,
				PropTag.Recipient.PostOfficeBox,
				PropTag.Recipient.TelexNumber,
				PropTag.Recipient.ISDNNumber,
				PropTag.Recipient.AssistantTelephoneNumber,
				PropTag.Recipient.Home2TelephoneNumber,
				PropTag.Recipient.Home2TelephoneNumberMv,
				PropTag.Recipient.Assistant,
				PropTag.Recipient.SendRichInfo,
				PropTag.Recipient.WeddingAnniversary,
				PropTag.Recipient.Birthday,
				PropTag.Recipient.Hobbies,
				PropTag.Recipient.MiddleName,
				PropTag.Recipient.DisplayNamePrefix,
				PropTag.Recipient.Profession,
				PropTag.Recipient.ReferredByName,
				PropTag.Recipient.SpouseName,
				PropTag.Recipient.ComputerNetworkName,
				PropTag.Recipient.CustomerId,
				PropTag.Recipient.TTYTDDPhoneNumber,
				PropTag.Recipient.FTPSite,
				PropTag.Recipient.Gender,
				PropTag.Recipient.ManagerName,
				PropTag.Recipient.NickName,
				PropTag.Recipient.PersonalHomePage,
				PropTag.Recipient.BusinessHomePage,
				PropTag.Recipient.ContactVersion,
				PropTag.Recipient.ContactEntryIds,
				PropTag.Recipient.ContactAddressTypes,
				PropTag.Recipient.ContactDefaultAddressIndex,
				PropTag.Recipient.ContactEmailAddress,
				PropTag.Recipient.CompanyMainPhoneNumber,
				PropTag.Recipient.ChildrensNames,
				PropTag.Recipient.HomeAddressCity,
				PropTag.Recipient.HomeAddressCountry,
				PropTag.Recipient.HomeAddressPostalCode,
				PropTag.Recipient.HomeAddressStateOrProvince,
				PropTag.Recipient.HomeAddressStreet,
				PropTag.Recipient.HomeAddressPostOfficeBox,
				PropTag.Recipient.OtherAddressCity,
				PropTag.Recipient.OtherAddressCountry,
				PropTag.Recipient.OtherAddressPostalCode,
				PropTag.Recipient.OtherAddressStateOrProvince,
				PropTag.Recipient.OtherAddressStreet,
				PropTag.Recipient.OtherAddressPostOfficeBox,
				PropTag.Recipient.UserX509CertificateABSearchPath,
				PropTag.Recipient.SendInternetEncoding,
				PropTag.Recipient.PartnerNetworkId,
				PropTag.Recipient.PartnerNetworkUserId,
				PropTag.Recipient.PartnerNetworkThumbnailPhotoUrl,
				PropTag.Recipient.PartnerNetworkProfilePhotoUrl,
				PropTag.Recipient.PartnerNetworkContactType,
				PropTag.Recipient.RelevanceScore,
				PropTag.Recipient.IsDistributionListContact,
				PropTag.Recipient.IsPromotedContact,
				PropTag.Recipient.OrgUnitName,
				PropTag.Recipient.OrganizationName,
				PropTag.Recipient.TestBlobProperty,
				PropTag.Recipient.NewAttach,
				PropTag.Recipient.StartEmbed,
				PropTag.Recipient.EndEmbed,
				PropTag.Recipient.StartRecip,
				PropTag.Recipient.EndRecip,
				PropTag.Recipient.EndCcRecip,
				PropTag.Recipient.EndBccRecip,
				PropTag.Recipient.EndP1Recip,
				PropTag.Recipient.DNPrefix,
				PropTag.Recipient.StartTopFolder,
				PropTag.Recipient.StartSubFolder,
				PropTag.Recipient.EndFolder,
				PropTag.Recipient.StartMessage,
				PropTag.Recipient.EndMessage,
				PropTag.Recipient.EndAttach,
				PropTag.Recipient.EcWarning,
				PropTag.Recipient.StartFAIMessage,
				PropTag.Recipient.NewFXFolder,
				PropTag.Recipient.IncrSyncChange,
				PropTag.Recipient.IncrSyncDelete,
				PropTag.Recipient.IncrSyncEnd,
				PropTag.Recipient.IncrSyncMessage,
				PropTag.Recipient.FastTransferDelProp,
				PropTag.Recipient.IdsetGiven,
				PropTag.Recipient.IdsetGivenInt32,
				PropTag.Recipient.FastTransferErrorInfo,
				PropTag.Recipient.SoftDeletes,
				PropTag.Recipient.IdsetRead,
				PropTag.Recipient.IdsetUnread,
				PropTag.Recipient.IncrSyncRead,
				PropTag.Recipient.IncrSyncStateBegin,
				PropTag.Recipient.IncrSyncStateEnd,
				PropTag.Recipient.IncrSyncImailStream,
				PropTag.Recipient.IncrSyncImailStreamContinue,
				PropTag.Recipient.IncrSyncImailStreamCancel,
				PropTag.Recipient.IncrSyncImailStream2Continue,
				PropTag.Recipient.IncrSyncProgressMode,
				PropTag.Recipient.SyncProgressPerMsg,
				PropTag.Recipient.IncrSyncMsgPartial,
				PropTag.Recipient.IncrSyncGroupInfo,
				PropTag.Recipient.IncrSyncGroupId,
				PropTag.Recipient.IncrSyncChangePartial,
				PropTag.Recipient.RecipientOrder,
				PropTag.Recipient.RecipientSipUri,
				PropTag.Recipient.RecipientDisplayName,
				PropTag.Recipient.RecipientEntryId,
				PropTag.Recipient.RecipientFlags,
				PropTag.Recipient.RecipientTrackStatus,
				PropTag.Recipient.DotStuffState,
				PropTag.Recipient.RecipientNumber,
				PropTag.Recipient.UserDN,
				PropTag.Recipient.CnsetSeen,
				PropTag.Recipient.SourceEntryId,
				PropTag.Recipient.CnsetRead,
				PropTag.Recipient.CnsetSeenFAI,
				PropTag.Recipient.IdSetDeleted
			};
		}

		// Token: 0x0200000B RID: 11
		public static class Event
		{
			// Token: 0x0400157B RID: 5499
			public static readonly StorePropTag EventMailboxGuid = new StorePropTag(26474, PropertyType.Binary, new StorePropInfo("EventMailboxGuid", 26474, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x0400157C RID: 5500
			public static readonly StorePropTag EventCounter = new StorePropTag(26631, PropertyType.Int64, new StorePropInfo("EventCounter", 26631, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x0400157D RID: 5501
			public static readonly StorePropTag EventMask = new StorePropTag(26632, PropertyType.Int32, new StorePropInfo("EventMask", 26632, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x0400157E RID: 5502
			public static readonly StorePropTag EventFid = new StorePropTag(26633, PropertyType.Binary, new StorePropInfo("EventFid", 26633, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x0400157F RID: 5503
			public static readonly StorePropTag EventMid = new StorePropTag(26634, PropertyType.Binary, new StorePropInfo("EventMid", 26634, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x04001580 RID: 5504
			public static readonly StorePropTag EventFidParent = new StorePropTag(26635, PropertyType.Binary, new StorePropInfo("EventFidParent", 26635, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x04001581 RID: 5505
			public static readonly StorePropTag EventFidOld = new StorePropTag(26636, PropertyType.Binary, new StorePropInfo("EventFidOld", 26636, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x04001582 RID: 5506
			public static readonly StorePropTag EventMidOld = new StorePropTag(26637, PropertyType.Binary, new StorePropInfo("EventMidOld", 26637, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x04001583 RID: 5507
			public static readonly StorePropTag EventFidOldParent = new StorePropTag(26638, PropertyType.Binary, new StorePropInfo("EventFidOldParent", 26638, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x04001584 RID: 5508
			public static readonly StorePropTag EventCreatedTime = new StorePropTag(26639, PropertyType.SysTime, new StorePropInfo("EventCreatedTime", 26639, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x04001585 RID: 5509
			public static readonly StorePropTag EventMessageClass = new StorePropTag(26640, PropertyType.Unicode, new StorePropInfo("EventMessageClass", 26640, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x04001586 RID: 5510
			public static readonly StorePropTag EventItemCount = new StorePropTag(26641, PropertyType.Int32, new StorePropInfo("EventItemCount", 26641, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x04001587 RID: 5511
			public static readonly StorePropTag EventFidRoot = new StorePropTag(26642, PropertyType.Binary, new StorePropInfo("EventFidRoot", 26642, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x04001588 RID: 5512
			public static readonly StorePropTag EventUnreadCount = new StorePropTag(26643, PropertyType.Int32, new StorePropInfo("EventUnreadCount", 26643, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x04001589 RID: 5513
			public static readonly StorePropTag EventTransacId = new StorePropTag(26644, PropertyType.Int32, new StorePropInfo("EventTransacId", 26644, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x0400158A RID: 5514
			public static readonly StorePropTag EventFlags = new StorePropTag(26645, PropertyType.Int32, new StorePropInfo("EventFlags", 26645, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x0400158B RID: 5515
			public static readonly StorePropTag EventExtendedFlags = new StorePropTag(26648, PropertyType.Int64, new StorePropInfo("EventExtendedFlags", 26648, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x0400158C RID: 5516
			public static readonly StorePropTag EventClientType = new StorePropTag(26649, PropertyType.Int32, new StorePropInfo("EventClientType", 26649, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x0400158D RID: 5517
			public static readonly StorePropTag EventSid = new StorePropTag(26650, PropertyType.Binary, new StorePropInfo("EventSid", 26650, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x0400158E RID: 5518
			public static readonly StorePropTag EventDocId = new StorePropTag(26651, PropertyType.Int32, new StorePropInfo("EventDocId", 26651, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x0400158F RID: 5519
			public static readonly StorePropTag MailboxNum = new StorePropTag(26655, PropertyType.Int32, new StorePropInfo("MailboxNum", 26655, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.Event);

			// Token: 0x04001590 RID: 5520
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[0];

			// Token: 0x04001591 RID: 5521
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[0];

			// Token: 0x04001592 RID: 5522
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[0];

			// Token: 0x04001593 RID: 5523
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[0];

			// Token: 0x04001594 RID: 5524
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[0];

			// Token: 0x04001595 RID: 5525
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[0];

			// Token: 0x04001596 RID: 5526
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[0];

			// Token: 0x04001597 RID: 5527
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x04001598 RID: 5528
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x04001599 RID: 5529
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[0];

			// Token: 0x0400159A RID: 5530
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[0];

			// Token: 0x0400159B RID: 5531
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x0400159C RID: 5532
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x0400159D RID: 5533
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x0400159E RID: 5534
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x0400159F RID: 5535
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x040015A0 RID: 5536
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x040015A1 RID: 5537
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x040015A2 RID: 5538
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x040015A3 RID: 5539
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.Event.EventMailboxGuid,
				PropTag.Event.EventCounter,
				PropTag.Event.EventMask,
				PropTag.Event.EventFid,
				PropTag.Event.EventMid,
				PropTag.Event.EventFidParent,
				PropTag.Event.EventFidOld,
				PropTag.Event.EventMidOld,
				PropTag.Event.EventFidOldParent,
				PropTag.Event.EventCreatedTime,
				PropTag.Event.EventMessageClass,
				PropTag.Event.EventItemCount,
				PropTag.Event.EventFidRoot,
				PropTag.Event.EventUnreadCount,
				PropTag.Event.EventTransacId,
				PropTag.Event.EventFlags,
				PropTag.Event.EventExtendedFlags,
				PropTag.Event.EventClientType,
				PropTag.Event.EventSid,
				PropTag.Event.EventDocId,
				PropTag.Event.MailboxNum
			};
		}

		// Token: 0x0200000C RID: 12
		public static class PermissionView
		{
			// Token: 0x040015A4 RID: 5540
			public static readonly StorePropTag EntryId = new StorePropTag(4095, PropertyType.Binary, new StorePropInfo("EntryId", 4095, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.PermissionView);

			// Token: 0x040015A5 RID: 5541
			public static readonly StorePropTag MemberId = new StorePropTag(26225, PropertyType.Int64, new StorePropInfo("MemberId", 26225, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.PermissionView);

			// Token: 0x040015A6 RID: 5542
			public static readonly StorePropTag MemberName = new StorePropTag(26226, PropertyType.Unicode, new StorePropInfo("MemberName", 26226, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.PermissionView);

			// Token: 0x040015A7 RID: 5543
			public static readonly StorePropTag MemberRights = new StorePropTag(26227, PropertyType.Int32, new StorePropInfo("MemberRights", 26227, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.PermissionView);

			// Token: 0x040015A8 RID: 5544
			public static readonly StorePropTag MemberSecurityIdentifier = new StorePropTag(26228, PropertyType.Binary, new StorePropInfo("MemberSecurityIdentifier", 26228, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.PermissionView);

			// Token: 0x040015A9 RID: 5545
			public static readonly StorePropTag MemberIsGroup = new StorePropTag(26229, PropertyType.Boolean, new StorePropInfo("MemberIsGroup", 26229, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.PermissionView);

			// Token: 0x040015AA RID: 5546
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[0];

			// Token: 0x040015AB RID: 5547
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[0];

			// Token: 0x040015AC RID: 5548
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[0];

			// Token: 0x040015AD RID: 5549
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[0];

			// Token: 0x040015AE RID: 5550
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[0];

			// Token: 0x040015AF RID: 5551
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[0];

			// Token: 0x040015B0 RID: 5552
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[0];

			// Token: 0x040015B1 RID: 5553
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x040015B2 RID: 5554
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x040015B3 RID: 5555
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[0];

			// Token: 0x040015B4 RID: 5556
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[0];

			// Token: 0x040015B5 RID: 5557
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x040015B6 RID: 5558
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x040015B7 RID: 5559
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x040015B8 RID: 5560
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x040015B9 RID: 5561
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x040015BA RID: 5562
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x040015BB RID: 5563
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x040015BC RID: 5564
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x040015BD RID: 5565
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.PermissionView.EntryId,
				PropTag.PermissionView.MemberId,
				PropTag.PermissionView.MemberName,
				PropTag.PermissionView.MemberRights,
				PropTag.PermissionView.MemberSecurityIdentifier,
				PropTag.PermissionView.MemberIsGroup
			};
		}

		// Token: 0x0200000D RID: 13
		public static class ViewDefinition
		{
			// Token: 0x040015BE RID: 5566
			public static readonly StorePropTag SortOrder = new StorePropTag(26465, PropertyType.Binary, new StorePropInfo("SortOrder", 26465, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ViewDefinition);

			// Token: 0x040015BF RID: 5567
			public static readonly StorePropTag LCID = new StorePropTag(26478, PropertyType.Int32, new StorePropInfo("LCID", 26478, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ViewDefinition);

			// Token: 0x040015C0 RID: 5568
			public static readonly StorePropTag ViewCoveringPropertyTags = new StorePropTag(26610, PropertyType.MVInt32, new StorePropInfo("ViewCoveringPropertyTags", 26610, PropertyType.MVInt32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ViewDefinition);

			// Token: 0x040015C1 RID: 5569
			public static readonly StorePropTag ViewAccessTime = new StorePropTag(26611, PropertyType.SysTime, new StorePropInfo("ViewAccessTime", 26611, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ViewDefinition);

			// Token: 0x040015C2 RID: 5570
			public static readonly StorePropTag ICSViewFilter = new StorePropTag(26612, PropertyType.Boolean, new StorePropInfo("ICSViewFilter", 26612, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ViewDefinition);

			// Token: 0x040015C3 RID: 5571
			public static readonly StorePropTag SoftDeletedFilter = new StorePropTag(26649, PropertyType.Boolean, new StorePropInfo("SoftDeletedFilter", 26649, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ViewDefinition);

			// Token: 0x040015C4 RID: 5572
			public static readonly StorePropTag AssociatedFilter = new StorePropTag(26650, PropertyType.Boolean, new StorePropInfo("AssociatedFilter", 26650, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ViewDefinition);

			// Token: 0x040015C5 RID: 5573
			public static readonly StorePropTag ConversationsFilter = new StorePropTag(26651, PropertyType.Boolean, new StorePropInfo("ConversationsFilter", 26651, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ViewDefinition);

			// Token: 0x040015C6 RID: 5574
			public static readonly StorePropTag CategCount = new StorePropTag(26782, PropertyType.Int32, new StorePropInfo("CategCount", 26782, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ViewDefinition);

			// Token: 0x040015C7 RID: 5575
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[0];

			// Token: 0x040015C8 RID: 5576
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[0];

			// Token: 0x040015C9 RID: 5577
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[0];

			// Token: 0x040015CA RID: 5578
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[0];

			// Token: 0x040015CB RID: 5579
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[0];

			// Token: 0x040015CC RID: 5580
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[0];

			// Token: 0x040015CD RID: 5581
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[0];

			// Token: 0x040015CE RID: 5582
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x040015CF RID: 5583
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x040015D0 RID: 5584
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[0];

			// Token: 0x040015D1 RID: 5585
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[0];

			// Token: 0x040015D2 RID: 5586
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x040015D3 RID: 5587
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x040015D4 RID: 5588
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x040015D5 RID: 5589
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x040015D6 RID: 5590
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x040015D7 RID: 5591
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x040015D8 RID: 5592
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x040015D9 RID: 5593
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x040015DA RID: 5594
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.ViewDefinition.SortOrder,
				PropTag.ViewDefinition.LCID,
				PropTag.ViewDefinition.ViewCoveringPropertyTags,
				PropTag.ViewDefinition.ViewAccessTime,
				PropTag.ViewDefinition.ICSViewFilter,
				PropTag.ViewDefinition.SoftDeletedFilter,
				PropTag.ViewDefinition.AssociatedFilter,
				PropTag.ViewDefinition.ConversationsFilter,
				PropTag.ViewDefinition.CategCount
			};
		}

		// Token: 0x0200000E RID: 14
		public static class RestrictionView
		{
			// Token: 0x040015DB RID: 5595
			public static readonly StorePropTag EntryId = new StorePropTag(4095, PropertyType.Binary, new StorePropInfo("EntryId", 4095, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.RestrictionView);

			// Token: 0x040015DC RID: 5596
			public static readonly StorePropTag DisplayName = new StorePropTag(12289, PropertyType.Unicode, new StorePropInfo("DisplayName", 12289, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.RestrictionView);

			// Token: 0x040015DD RID: 5597
			public static readonly StorePropTag ContentCount = new StorePropTag(13826, PropertyType.Int32, new StorePropInfo("ContentCount", 13826, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.RestrictionView);

			// Token: 0x040015DE RID: 5598
			public static readonly StorePropTag UnreadCount = new StorePropTag(13827, PropertyType.Int32, new StorePropInfo("UnreadCount", 13827, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.RestrictionView);

			// Token: 0x040015DF RID: 5599
			public static readonly StorePropTag LCIDRestriction = new StorePropTag(26504, PropertyType.Int32, new StorePropInfo("LCIDRestriction", 26504, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.RestrictionView);

			// Token: 0x040015E0 RID: 5600
			public static readonly StorePropTag ViewRestriction = new StorePropTag(26544, PropertyType.SRestriction, new StorePropInfo("ViewRestriction", 26544, PropertyType.SRestriction, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.RestrictionView);

			// Token: 0x040015E1 RID: 5601
			public static readonly StorePropTag ViewAccessTime = new StorePropTag(26611, PropertyType.SysTime, new StorePropInfo("ViewAccessTime", 26611, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.RestrictionView);

			// Token: 0x040015E2 RID: 5602
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[0];

			// Token: 0x040015E3 RID: 5603
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[0];

			// Token: 0x040015E4 RID: 5604
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[0];

			// Token: 0x040015E5 RID: 5605
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[0];

			// Token: 0x040015E6 RID: 5606
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[0];

			// Token: 0x040015E7 RID: 5607
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[0];

			// Token: 0x040015E8 RID: 5608
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[0];

			// Token: 0x040015E9 RID: 5609
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x040015EA RID: 5610
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x040015EB RID: 5611
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[0];

			// Token: 0x040015EC RID: 5612
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[0];

			// Token: 0x040015ED RID: 5613
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x040015EE RID: 5614
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x040015EF RID: 5615
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x040015F0 RID: 5616
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x040015F1 RID: 5617
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x040015F2 RID: 5618
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x040015F3 RID: 5619
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x040015F4 RID: 5620
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x040015F5 RID: 5621
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.RestrictionView.EntryId,
				PropTag.RestrictionView.DisplayName,
				PropTag.RestrictionView.ContentCount,
				PropTag.RestrictionView.UnreadCount,
				PropTag.RestrictionView.LCIDRestriction,
				PropTag.RestrictionView.ViewRestriction,
				PropTag.RestrictionView.ViewAccessTime
			};
		}

		// Token: 0x0200000F RID: 15
		public static class LocalDirectory
		{
			// Token: 0x040015F6 RID: 5622
			public static readonly StorePropTag LocalDirectoryEntryID = new StorePropTag(13334, PropertyType.Binary, new StorePropInfo("LocalDirectoryEntryID", 13334, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.LocalDirectory);

			// Token: 0x040015F7 RID: 5623
			public static readonly StorePropTag MemberName = new StorePropTag(26226, PropertyType.Unicode, new StorePropInfo("MemberName", 26226, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.LocalDirectory);

			// Token: 0x040015F8 RID: 5624
			public static readonly StorePropTag MemberEmail = new StorePropTag(26665, PropertyType.Unicode, new StorePropInfo("MemberEmail", 26665, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.LocalDirectory);

			// Token: 0x040015F9 RID: 5625
			public static readonly StorePropTag MemberExternalId = new StorePropTag(26666, PropertyType.Unicode, new StorePropInfo("MemberExternalId", 26666, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.LocalDirectory);

			// Token: 0x040015FA RID: 5626
			public static readonly StorePropTag MemberSID = new StorePropTag(26667, PropertyType.Binary, new StorePropInfo("MemberSID", 26667, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.LocalDirectory);

			// Token: 0x040015FB RID: 5627
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[0];

			// Token: 0x040015FC RID: 5628
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[0];

			// Token: 0x040015FD RID: 5629
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[0];

			// Token: 0x040015FE RID: 5630
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[0];

			// Token: 0x040015FF RID: 5631
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[0];

			// Token: 0x04001600 RID: 5632
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[0];

			// Token: 0x04001601 RID: 5633
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[0];

			// Token: 0x04001602 RID: 5634
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x04001603 RID: 5635
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x04001604 RID: 5636
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[0];

			// Token: 0x04001605 RID: 5637
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[0];

			// Token: 0x04001606 RID: 5638
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x04001607 RID: 5639
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x04001608 RID: 5640
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x04001609 RID: 5641
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x0400160A RID: 5642
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x0400160B RID: 5643
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x0400160C RID: 5644
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x0400160D RID: 5645
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x0400160E RID: 5646
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.LocalDirectory.LocalDirectoryEntryID,
				PropTag.LocalDirectory.MemberName,
				PropTag.LocalDirectory.MemberEmail,
				PropTag.LocalDirectory.MemberExternalId,
				PropTag.LocalDirectory.MemberSID
			};
		}

		// Token: 0x02000010 RID: 16
		public static class ResourceDigest
		{
			// Token: 0x0400160F RID: 5647
			public static readonly StorePropTag DisplayName = new StorePropTag(12289, PropertyType.Unicode, new StorePropInfo("DisplayName", 12289, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x04001610 RID: 5648
			public static readonly StorePropTag MailboxDSGuid = new StorePropTag(26375, PropertyType.Binary, new StorePropInfo("MailboxDSGuid", 26375, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x04001611 RID: 5649
			public static readonly StorePropTag TimeInServer = new StorePropTag(26413, PropertyType.Int32, new StorePropInfo("TimeInServer", 26413, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x04001612 RID: 5650
			public static readonly StorePropTag TimeInCpu = new StorePropTag(26414, PropertyType.Int32, new StorePropInfo("TimeInCpu", 26414, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x04001613 RID: 5651
			public static readonly StorePropTag RopCount = new StorePropTag(26415, PropertyType.Int32, new StorePropInfo("RopCount", 26415, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x04001614 RID: 5652
			public static readonly StorePropTag PageRead = new StorePropTag(26416, PropertyType.Int32, new StorePropInfo("PageRead", 26416, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x04001615 RID: 5653
			public static readonly StorePropTag PagePreread = new StorePropTag(26417, PropertyType.Int32, new StorePropInfo("PagePreread", 26417, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x04001616 RID: 5654
			public static readonly StorePropTag LogRecordCount = new StorePropTag(26418, PropertyType.Int32, new StorePropInfo("LogRecordCount", 26418, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x04001617 RID: 5655
			public static readonly StorePropTag LogRecordBytes = new StorePropTag(26419, PropertyType.Int32, new StorePropInfo("LogRecordBytes", 26419, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x04001618 RID: 5656
			public static readonly StorePropTag LdapReads = new StorePropTag(26420, PropertyType.Int32, new StorePropInfo("LdapReads", 26420, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x04001619 RID: 5657
			public static readonly StorePropTag LdapSearches = new StorePropTag(26421, PropertyType.Int32, new StorePropInfo("LdapSearches", 26421, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x0400161A RID: 5658
			public static readonly StorePropTag DigestCategory = new StorePropTag(26422, PropertyType.Unicode, new StorePropInfo("DigestCategory", 26422, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x0400161B RID: 5659
			public static readonly StorePropTag SampleId = new StorePropTag(26423, PropertyType.Int32, new StorePropInfo("SampleId", 26423, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x0400161C RID: 5660
			public static readonly StorePropTag SampleTime = new StorePropTag(26424, PropertyType.SysTime, new StorePropInfo("SampleTime", 26424, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x0400161D RID: 5661
			public static readonly StorePropTag MailboxQuarantined = new StorePropTag(26650, PropertyType.Boolean, new StorePropInfo("MailboxQuarantined", 26650, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ResourceDigest);

			// Token: 0x0400161E RID: 5662
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[0];

			// Token: 0x0400161F RID: 5663
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[0];

			// Token: 0x04001620 RID: 5664
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[0];

			// Token: 0x04001621 RID: 5665
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[0];

			// Token: 0x04001622 RID: 5666
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[0];

			// Token: 0x04001623 RID: 5667
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[0];

			// Token: 0x04001624 RID: 5668
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[0];

			// Token: 0x04001625 RID: 5669
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x04001626 RID: 5670
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x04001627 RID: 5671
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[0];

			// Token: 0x04001628 RID: 5672
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[0];

			// Token: 0x04001629 RID: 5673
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x0400162A RID: 5674
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x0400162B RID: 5675
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x0400162C RID: 5676
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x0400162D RID: 5677
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x0400162E RID: 5678
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x0400162F RID: 5679
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x04001630 RID: 5680
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x04001631 RID: 5681
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.ResourceDigest.DisplayName,
				PropTag.ResourceDigest.MailboxDSGuid,
				PropTag.ResourceDigest.TimeInServer,
				PropTag.ResourceDigest.TimeInCpu,
				PropTag.ResourceDigest.RopCount,
				PropTag.ResourceDigest.PageRead,
				PropTag.ResourceDigest.PagePreread,
				PropTag.ResourceDigest.LogRecordCount,
				PropTag.ResourceDigest.LogRecordBytes,
				PropTag.ResourceDigest.LdapReads,
				PropTag.ResourceDigest.LdapSearches,
				PropTag.ResourceDigest.DigestCategory,
				PropTag.ResourceDigest.SampleId,
				PropTag.ResourceDigest.SampleTime,
				PropTag.ResourceDigest.MailboxQuarantined
			};
		}

		// Token: 0x02000011 RID: 17
		public static class IcsState
		{
			// Token: 0x04001632 RID: 5682
			public static readonly StorePropTag IdsetGiven = new StorePropTag(16407, PropertyType.Binary, new StorePropInfo("IdsetGiven", 16407, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IcsState);

			// Token: 0x04001633 RID: 5683
			public static readonly StorePropTag IdsetGivenInt32 = new StorePropTag(16407, PropertyType.Int32, new StorePropInfo("IdsetGivenInt32", 16407, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IcsState);

			// Token: 0x04001634 RID: 5684
			public static readonly StorePropTag CnsetSeen = new StorePropTag(26518, PropertyType.Binary, new StorePropInfo("CnsetSeen", 26518, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IcsState);

			// Token: 0x04001635 RID: 5685
			public static readonly StorePropTag CnsetRead = new StorePropTag(26578, PropertyType.Binary, new StorePropInfo("CnsetRead", 26578, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IcsState);

			// Token: 0x04001636 RID: 5686
			public static readonly StorePropTag CnsetSeenFAI = new StorePropTag(26586, PropertyType.Binary, new StorePropInfo("CnsetSeenFAI", 26586, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IcsState);

			// Token: 0x04001637 RID: 5687
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[0];

			// Token: 0x04001638 RID: 5688
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[0];

			// Token: 0x04001639 RID: 5689
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[0];

			// Token: 0x0400163A RID: 5690
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[0];

			// Token: 0x0400163B RID: 5691
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[0];

			// Token: 0x0400163C RID: 5692
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[0];

			// Token: 0x0400163D RID: 5693
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[0];

			// Token: 0x0400163E RID: 5694
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x0400163F RID: 5695
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x04001640 RID: 5696
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[0];

			// Token: 0x04001641 RID: 5697
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[0];

			// Token: 0x04001642 RID: 5698
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x04001643 RID: 5699
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x04001644 RID: 5700
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x04001645 RID: 5701
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x04001646 RID: 5702
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x04001647 RID: 5703
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x04001648 RID: 5704
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x04001649 RID: 5705
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x0400164A RID: 5706
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.IcsState.IdsetGiven,
				PropTag.IcsState.IdsetGivenInt32,
				PropTag.IcsState.CnsetSeen,
				PropTag.IcsState.CnsetRead,
				PropTag.IcsState.CnsetSeenFAI
			};
		}

		// Token: 0x02000012 RID: 18
		public static class InferenceLog
		{
			// Token: 0x0400164B RID: 5707
			public static readonly StorePropTag RowId = new StorePropTag(12288, PropertyType.Int32, new StorePropInfo("RowId", 12288, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x0400164C RID: 5708
			public static readonly StorePropTag MailboxPartitionNumber = new StorePropTag(15775, PropertyType.Int32, new StorePropInfo("MailboxPartitionNumber", 15775, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x0400164D RID: 5709
			public static readonly StorePropTag MailboxNumberInternal = new StorePropTag(15776, PropertyType.Int32, new StorePropInfo("MailboxNumberInternal", 15776, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x0400164E RID: 5710
			public static readonly StorePropTag Mid = new StorePropTag(26442, PropertyType.Int64, new StorePropInfo("Mid", 26442, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x0400164F RID: 5711
			public static readonly StorePropTag MailboxNum = new StorePropTag(26655, PropertyType.Int32, new StorePropInfo("MailboxNum", 26655, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001650 RID: 5712
			public static readonly StorePropTag InferenceActivityId = new StorePropTag(26656, PropertyType.Int32, new StorePropInfo("InferenceActivityId", 26656, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001651 RID: 5713
			public static readonly StorePropTag InferenceClientId = new StorePropTag(26657, PropertyType.Int32, new StorePropInfo("InferenceClientId", 26657, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001652 RID: 5714
			public static readonly StorePropTag InferenceItemId = new StorePropTag(26658, PropertyType.Binary, new StorePropInfo("InferenceItemId", 26658, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001653 RID: 5715
			public static readonly StorePropTag InferenceTimeStamp = new StorePropTag(26659, PropertyType.SysTime, new StorePropInfo("InferenceTimeStamp", 26659, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001654 RID: 5716
			public static readonly StorePropTag InferenceWindowId = new StorePropTag(26660, PropertyType.Guid, new StorePropInfo("InferenceWindowId", 26660, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001655 RID: 5717
			public static readonly StorePropTag InferenceSessionId = new StorePropTag(26661, PropertyType.Guid, new StorePropInfo("InferenceSessionId", 26661, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001656 RID: 5718
			public static readonly StorePropTag InferenceFolderId = new StorePropTag(26662, PropertyType.Binary, new StorePropInfo("InferenceFolderId", 26662, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001657 RID: 5719
			public static readonly StorePropTag InferenceOofEnabled = new StorePropTag(26663, PropertyType.Boolean, new StorePropInfo("InferenceOofEnabled", 26663, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001658 RID: 5720
			public static readonly StorePropTag InferenceDeleteType = new StorePropTag(26664, PropertyType.Int32, new StorePropInfo("InferenceDeleteType", 26664, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001659 RID: 5721
			public static readonly StorePropTag InferenceBrowser = new StorePropTag(26665, PropertyType.Unicode, new StorePropInfo("InferenceBrowser", 26665, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x0400165A RID: 5722
			public static readonly StorePropTag InferenceLocaleId = new StorePropTag(26666, PropertyType.Int32, new StorePropInfo("InferenceLocaleId", 26666, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x0400165B RID: 5723
			public static readonly StorePropTag InferenceLocation = new StorePropTag(26667, PropertyType.Unicode, new StorePropInfo("InferenceLocation", 26667, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x0400165C RID: 5724
			public static readonly StorePropTag InferenceConversationId = new StorePropTag(26668, PropertyType.Binary, new StorePropInfo("InferenceConversationId", 26668, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x0400165D RID: 5725
			public static readonly StorePropTag InferenceIpAddress = new StorePropTag(26669, PropertyType.Unicode, new StorePropInfo("InferenceIpAddress", 26669, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x0400165E RID: 5726
			public static readonly StorePropTag InferenceTimeZone = new StorePropTag(26670, PropertyType.Unicode, new StorePropInfo("InferenceTimeZone", 26670, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x0400165F RID: 5727
			public static readonly StorePropTag InferenceCategory = new StorePropTag(26671, PropertyType.Unicode, new StorePropInfo("InferenceCategory", 26671, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001660 RID: 5728
			public static readonly StorePropTag InferenceAttachmentId = new StorePropTag(26672, PropertyType.Binary, new StorePropInfo("InferenceAttachmentId", 26672, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001661 RID: 5729
			public static readonly StorePropTag InferenceGlobalObjectId = new StorePropTag(26673, PropertyType.Binary, new StorePropInfo("InferenceGlobalObjectId", 26673, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001662 RID: 5730
			public static readonly StorePropTag InferenceModuleSelected = new StorePropTag(26674, PropertyType.Int32, new StorePropInfo("InferenceModuleSelected", 26674, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001663 RID: 5731
			public static readonly StorePropTag InferenceLayoutType = new StorePropTag(26675, PropertyType.Unicode, new StorePropInfo("InferenceLayoutType", 26675, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.InferenceLog);

			// Token: 0x04001664 RID: 5732
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[0];

			// Token: 0x04001665 RID: 5733
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[0];

			// Token: 0x04001666 RID: 5734
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[0];

			// Token: 0x04001667 RID: 5735
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[0];

			// Token: 0x04001668 RID: 5736
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[0];

			// Token: 0x04001669 RID: 5737
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[0];

			// Token: 0x0400166A RID: 5738
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[0];

			// Token: 0x0400166B RID: 5739
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x0400166C RID: 5740
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x0400166D RID: 5741
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[0];

			// Token: 0x0400166E RID: 5742
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[0];

			// Token: 0x0400166F RID: 5743
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x04001670 RID: 5744
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x04001671 RID: 5745
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x04001672 RID: 5746
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x04001673 RID: 5747
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x04001674 RID: 5748
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x04001675 RID: 5749
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x04001676 RID: 5750
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x04001677 RID: 5751
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.InferenceLog.RowId,
				PropTag.InferenceLog.MailboxPartitionNumber,
				PropTag.InferenceLog.MailboxNumberInternal,
				PropTag.InferenceLog.Mid,
				PropTag.InferenceLog.MailboxNum,
				PropTag.InferenceLog.InferenceActivityId,
				PropTag.InferenceLog.InferenceClientId,
				PropTag.InferenceLog.InferenceItemId,
				PropTag.InferenceLog.InferenceTimeStamp,
				PropTag.InferenceLog.InferenceWindowId,
				PropTag.InferenceLog.InferenceSessionId,
				PropTag.InferenceLog.InferenceFolderId,
				PropTag.InferenceLog.InferenceOofEnabled,
				PropTag.InferenceLog.InferenceDeleteType,
				PropTag.InferenceLog.InferenceBrowser,
				PropTag.InferenceLog.InferenceLocaleId,
				PropTag.InferenceLog.InferenceLocation,
				PropTag.InferenceLog.InferenceConversationId,
				PropTag.InferenceLog.InferenceIpAddress,
				PropTag.InferenceLog.InferenceTimeZone,
				PropTag.InferenceLog.InferenceCategory,
				PropTag.InferenceLog.InferenceAttachmentId,
				PropTag.InferenceLog.InferenceGlobalObjectId,
				PropTag.InferenceLog.InferenceModuleSelected,
				PropTag.InferenceLog.InferenceLayoutType
			};
		}

		// Token: 0x02000013 RID: 19
		public static class ProcessInfo
		{
			// Token: 0x04001678 RID: 5752
			public static readonly StorePropTag WorkerProcessId = new StorePropTag(26263, PropertyType.Int32, new StorePropInfo("WorkerProcessId", 26263, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ProcessInfo);

			// Token: 0x04001679 RID: 5753
			public static readonly StorePropTag MinimumDatabaseSchemaVersion = new StorePropTag(26264, PropertyType.Int32, new StorePropInfo("MinimumDatabaseSchemaVersion", 26264, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ProcessInfo);

			// Token: 0x0400167A RID: 5754
			public static readonly StorePropTag MaximumDatabaseSchemaVersion = new StorePropTag(26265, PropertyType.Int32, new StorePropInfo("MaximumDatabaseSchemaVersion", 26265, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ProcessInfo);

			// Token: 0x0400167B RID: 5755
			public static readonly StorePropTag CurrentDatabaseSchemaVersion = new StorePropTag(26266, PropertyType.Int32, new StorePropInfo("CurrentDatabaseSchemaVersion", 26266, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ProcessInfo);

			// Token: 0x0400167C RID: 5756
			public static readonly StorePropTag RequestedDatabaseSchemaVersion = new StorePropTag(26267, PropertyType.Int32, new StorePropInfo("RequestedDatabaseSchemaVersion", 26267, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.ProcessInfo);

			// Token: 0x0400167D RID: 5757
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[0];

			// Token: 0x0400167E RID: 5758
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[0];

			// Token: 0x0400167F RID: 5759
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[0];

			// Token: 0x04001680 RID: 5760
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[0];

			// Token: 0x04001681 RID: 5761
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[0];

			// Token: 0x04001682 RID: 5762
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[0];

			// Token: 0x04001683 RID: 5763
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[0];

			// Token: 0x04001684 RID: 5764
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x04001685 RID: 5765
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x04001686 RID: 5766
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[0];

			// Token: 0x04001687 RID: 5767
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[0];

			// Token: 0x04001688 RID: 5768
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x04001689 RID: 5769
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x0400168A RID: 5770
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x0400168B RID: 5771
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x0400168C RID: 5772
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x0400168D RID: 5773
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x0400168E RID: 5774
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x0400168F RID: 5775
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x04001690 RID: 5776
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.ProcessInfo.WorkerProcessId,
				PropTag.ProcessInfo.MinimumDatabaseSchemaVersion,
				PropTag.ProcessInfo.MaximumDatabaseSchemaVersion,
				PropTag.ProcessInfo.CurrentDatabaseSchemaVersion,
				PropTag.ProcessInfo.RequestedDatabaseSchemaVersion
			};
		}

		// Token: 0x02000014 RID: 20
		public static class FastTransferStream
		{
			// Token: 0x04001691 RID: 5777
			public static readonly StorePropTag InstanceGuid = new StorePropTag(26653, PropertyType.Guid, new StorePropInfo("InstanceGuid", 26653, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.FastTransferStream);

			// Token: 0x04001692 RID: 5778
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[0];

			// Token: 0x04001693 RID: 5779
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[0];

			// Token: 0x04001694 RID: 5780
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[0];

			// Token: 0x04001695 RID: 5781
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[0];

			// Token: 0x04001696 RID: 5782
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[0];

			// Token: 0x04001697 RID: 5783
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[0];

			// Token: 0x04001698 RID: 5784
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[0];

			// Token: 0x04001699 RID: 5785
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x0400169A RID: 5786
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x0400169B RID: 5787
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[0];

			// Token: 0x0400169C RID: 5788
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[0];

			// Token: 0x0400169D RID: 5789
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x0400169E RID: 5790
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x0400169F RID: 5791
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x040016A0 RID: 5792
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x040016A1 RID: 5793
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x040016A2 RID: 5794
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x040016A3 RID: 5795
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x040016A4 RID: 5796
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x040016A5 RID: 5797
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.FastTransferStream.InstanceGuid
			};
		}

		// Token: 0x02000015 RID: 21
		public static class IsIntegJob
		{
			// Token: 0x040016A6 RID: 5798
			public static readonly StorePropTag IsIntegJobMailboxGuid = new StorePropTag(4096, PropertyType.Guid, new StorePropInfo("IsIntegJobMailboxGuid", 4096, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016A7 RID: 5799
			public static readonly StorePropTag IsIntegJobGuid = new StorePropTag(4097, PropertyType.Guid, new StorePropInfo("IsIntegJobGuid", 4097, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016A8 RID: 5800
			public static readonly StorePropTag IsIntegJobFlags = new StorePropTag(4098, PropertyType.Int32, new StorePropInfo("IsIntegJobFlags", 4098, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016A9 RID: 5801
			public static readonly StorePropTag IsIntegJobTask = new StorePropTag(4099, PropertyType.Int32, new StorePropInfo("IsIntegJobTask", 4099, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016AA RID: 5802
			public static readonly StorePropTag IsIntegJobState = new StorePropTag(4100, PropertyType.Int16, new StorePropInfo("IsIntegJobState", 4100, PropertyType.Int16, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016AB RID: 5803
			public static readonly StorePropTag IsIntegJobCreationTime = new StorePropTag(4101, PropertyType.SysTime, new StorePropInfo("IsIntegJobCreationTime", 4101, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016AC RID: 5804
			public static readonly StorePropTag IsIntegJobCompletedTime = new StorePropTag(4102, PropertyType.SysTime, new StorePropInfo("IsIntegJobCompletedTime", 4102, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016AD RID: 5805
			public static readonly StorePropTag IsIntegJobLastExecutionTime = new StorePropTag(4103, PropertyType.SysTime, new StorePropInfo("IsIntegJobLastExecutionTime", 4103, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016AE RID: 5806
			public static readonly StorePropTag IsIntegJobCorruptionsDetected = new StorePropTag(4104, PropertyType.Int32, new StorePropInfo("IsIntegJobCorruptionsDetected", 4104, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016AF RID: 5807
			public static readonly StorePropTag IsIntegJobCorruptionsFixed = new StorePropTag(4105, PropertyType.Int32, new StorePropInfo("IsIntegJobCorruptionsFixed", 4105, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016B0 RID: 5808
			public static readonly StorePropTag IsIntegJobRequestGuid = new StorePropTag(4106, PropertyType.Guid, new StorePropInfo("IsIntegJobRequestGuid", 4106, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016B1 RID: 5809
			public static readonly StorePropTag IsIntegJobProgress = new StorePropTag(4107, PropertyType.Int16, new StorePropInfo("IsIntegJobProgress", 4107, PropertyType.Int16, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016B2 RID: 5810
			public static readonly StorePropTag IsIntegJobCorruptions = new StorePropTag(4108, PropertyType.Binary, new StorePropInfo("IsIntegJobCorruptions", 4108, PropertyType.Binary, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016B3 RID: 5811
			public static readonly StorePropTag IsIntegJobSource = new StorePropTag(4109, PropertyType.Int16, new StorePropInfo("IsIntegJobSource", 4109, PropertyType.Int16, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016B4 RID: 5812
			public static readonly StorePropTag IsIntegJobPriority = new StorePropTag(4110, PropertyType.Int16, new StorePropInfo("IsIntegJobPriority", 4110, PropertyType.Int16, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016B5 RID: 5813
			public static readonly StorePropTag IsIntegJobTimeInServer = new StorePropTag(4111, PropertyType.Real64, new StorePropInfo("IsIntegJobTimeInServer", 4111, PropertyType.Real64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016B6 RID: 5814
			public static readonly StorePropTag IsIntegJobMailboxNumber = new StorePropTag(4112, PropertyType.Int32, new StorePropInfo("IsIntegJobMailboxNumber", 4112, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016B7 RID: 5815
			public static readonly StorePropTag IsIntegJobError = new StorePropTag(4113, PropertyType.Int32, new StorePropInfo("IsIntegJobError", 4113, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.IsIntegJob);

			// Token: 0x040016B8 RID: 5816
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[0];

			// Token: 0x040016B9 RID: 5817
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[0];

			// Token: 0x040016BA RID: 5818
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[0];

			// Token: 0x040016BB RID: 5819
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[0];

			// Token: 0x040016BC RID: 5820
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[0];

			// Token: 0x040016BD RID: 5821
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[0];

			// Token: 0x040016BE RID: 5822
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[0];

			// Token: 0x040016BF RID: 5823
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x040016C0 RID: 5824
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x040016C1 RID: 5825
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[0];

			// Token: 0x040016C2 RID: 5826
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[0];

			// Token: 0x040016C3 RID: 5827
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x040016C4 RID: 5828
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x040016C5 RID: 5829
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x040016C6 RID: 5830
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x040016C7 RID: 5831
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x040016C8 RID: 5832
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x040016C9 RID: 5833
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x040016CA RID: 5834
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x040016CB RID: 5835
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.IsIntegJob.IsIntegJobMailboxGuid,
				PropTag.IsIntegJob.IsIntegJobGuid,
				PropTag.IsIntegJob.IsIntegJobFlags,
				PropTag.IsIntegJob.IsIntegJobTask,
				PropTag.IsIntegJob.IsIntegJobState,
				PropTag.IsIntegJob.IsIntegJobCreationTime,
				PropTag.IsIntegJob.IsIntegJobCompletedTime,
				PropTag.IsIntegJob.IsIntegJobLastExecutionTime,
				PropTag.IsIntegJob.IsIntegJobCorruptionsDetected,
				PropTag.IsIntegJob.IsIntegJobCorruptionsFixed,
				PropTag.IsIntegJob.IsIntegJobRequestGuid,
				PropTag.IsIntegJob.IsIntegJobProgress,
				PropTag.IsIntegJob.IsIntegJobCorruptions,
				PropTag.IsIntegJob.IsIntegJobSource,
				PropTag.IsIntegJob.IsIntegJobPriority,
				PropTag.IsIntegJob.IsIntegJobTimeInServer,
				PropTag.IsIntegJob.IsIntegJobMailboxNumber,
				PropTag.IsIntegJob.IsIntegJobError
			};
		}

		// Token: 0x02000016 RID: 22
		public static class UserInfo
		{
			// Token: 0x040016CC RID: 5836
			public static readonly StorePropTag UserInformationGuid = new StorePropTag(12288, PropertyType.Guid, new StorePropInfo("UserInformationGuid", 12288, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3)), ObjectType.UserInfo);

			// Token: 0x040016CD RID: 5837
			public static readonly StorePropTag UserInformationDisplayName = new StorePropTag(12289, PropertyType.Unicode, new StorePropInfo("UserInformationDisplayName", 12289, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016CE RID: 5838
			public static readonly StorePropTag UserInformationCreationTime = new StorePropTag(12290, PropertyType.SysTime, new StorePropInfo("UserInformationCreationTime", 12290, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4)), ObjectType.UserInfo);

			// Token: 0x040016CF RID: 5839
			public static readonly StorePropTag UserInformationLastModificationTime = new StorePropTag(12291, PropertyType.SysTime, new StorePropInfo("UserInformationLastModificationTime", 12291, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4)), ObjectType.UserInfo);

			// Token: 0x040016D0 RID: 5840
			public static readonly StorePropTag UserInformationChangeNumber = new StorePropTag(12292, PropertyType.Int64, new StorePropInfo("UserInformationChangeNumber", 12292, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, new PropertyCategories(3, 4)), ObjectType.UserInfo);

			// Token: 0x040016D1 RID: 5841
			public static readonly StorePropTag UserInformationLastInteractiveLogonTime = new StorePropTag(12293, PropertyType.SysTime, new StorePropInfo("UserInformationLastInteractiveLogonTime", 12293, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016D2 RID: 5842
			public static readonly StorePropTag UserInformationActiveSyncAllowedDeviceIDs = new StorePropTag(12294, PropertyType.MVUnicode, new StorePropInfo("UserInformationActiveSyncAllowedDeviceIDs", 12294, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016D3 RID: 5843
			public static readonly StorePropTag UserInformationActiveSyncBlockedDeviceIDs = new StorePropTag(12295, PropertyType.MVUnicode, new StorePropInfo("UserInformationActiveSyncBlockedDeviceIDs", 12295, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016D4 RID: 5844
			public static readonly StorePropTag UserInformationActiveSyncDebugLogging = new StorePropTag(12296, PropertyType.Int32, new StorePropInfo("UserInformationActiveSyncDebugLogging", 12296, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016D5 RID: 5845
			public static readonly StorePropTag UserInformationActiveSyncEnabled = new StorePropTag(12297, PropertyType.Boolean, new StorePropInfo("UserInformationActiveSyncEnabled", 12297, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016D6 RID: 5846
			public static readonly StorePropTag UserInformationAdminDisplayName = new StorePropTag(12298, PropertyType.Unicode, new StorePropInfo("UserInformationAdminDisplayName", 12298, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016D7 RID: 5847
			public static readonly StorePropTag UserInformationAggregationSubscriptionCredential = new StorePropTag(12299, PropertyType.MVUnicode, new StorePropInfo("UserInformationAggregationSubscriptionCredential", 12299, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016D8 RID: 5848
			public static readonly StorePropTag UserInformationAllowArchiveAddressSync = new StorePropTag(12300, PropertyType.Boolean, new StorePropInfo("UserInformationAllowArchiveAddressSync", 12300, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016D9 RID: 5849
			public static readonly StorePropTag UserInformationAltitude = new StorePropTag(12301, PropertyType.Int32, new StorePropInfo("UserInformationAltitude", 12301, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016DA RID: 5850
			public static readonly StorePropTag UserInformationAntispamBypassEnabled = new StorePropTag(12302, PropertyType.Boolean, new StorePropInfo("UserInformationAntispamBypassEnabled", 12302, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016DB RID: 5851
			public static readonly StorePropTag UserInformationArchiveDomain = new StorePropTag(12303, PropertyType.Unicode, new StorePropInfo("UserInformationArchiveDomain", 12303, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016DC RID: 5852
			public static readonly StorePropTag UserInformationArchiveGuid = new StorePropTag(12304, PropertyType.Guid, new StorePropInfo("UserInformationArchiveGuid", 12304, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016DD RID: 5853
			public static readonly StorePropTag UserInformationArchiveName = new StorePropTag(12305, PropertyType.MVUnicode, new StorePropInfo("UserInformationArchiveName", 12305, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016DE RID: 5854
			public static readonly StorePropTag UserInformationArchiveQuota = new StorePropTag(12306, PropertyType.Unicode, new StorePropInfo("UserInformationArchiveQuota", 12306, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016DF RID: 5855
			public static readonly StorePropTag UserInformationArchiveRelease = new StorePropTag(12307, PropertyType.Unicode, new StorePropInfo("UserInformationArchiveRelease", 12307, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016E0 RID: 5856
			public static readonly StorePropTag UserInformationArchiveStatus = new StorePropTag(12308, PropertyType.Int32, new StorePropInfo("UserInformationArchiveStatus", 12308, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016E1 RID: 5857
			public static readonly StorePropTag UserInformationArchiveWarningQuota = new StorePropTag(12309, PropertyType.Unicode, new StorePropInfo("UserInformationArchiveWarningQuota", 12309, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016E2 RID: 5858
			public static readonly StorePropTag UserInformationAssistantName = new StorePropTag(12310, PropertyType.Unicode, new StorePropInfo("UserInformationAssistantName", 12310, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016E3 RID: 5859
			public static readonly StorePropTag UserInformationBirthdate = new StorePropTag(12311, PropertyType.SysTime, new StorePropInfo("UserInformationBirthdate", 12311, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016E4 RID: 5860
			public static readonly StorePropTag UserInformationBypassNestedModerationEnabled = new StorePropTag(12312, PropertyType.Boolean, new StorePropInfo("UserInformationBypassNestedModerationEnabled", 12312, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016E5 RID: 5861
			public static readonly StorePropTag UserInformationC = new StorePropTag(12313, PropertyType.Unicode, new StorePropInfo("UserInformationC", 12313, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016E6 RID: 5862
			public static readonly StorePropTag UserInformationCalendarLoggingQuota = new StorePropTag(12314, PropertyType.Unicode, new StorePropInfo("UserInformationCalendarLoggingQuota", 12314, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016E7 RID: 5863
			public static readonly StorePropTag UserInformationCalendarRepairDisabled = new StorePropTag(12315, PropertyType.Boolean, new StorePropInfo("UserInformationCalendarRepairDisabled", 12315, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016E8 RID: 5864
			public static readonly StorePropTag UserInformationCalendarVersionStoreDisabled = new StorePropTag(12316, PropertyType.Boolean, new StorePropInfo("UserInformationCalendarVersionStoreDisabled", 12316, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016E9 RID: 5865
			public static readonly StorePropTag UserInformationCity = new StorePropTag(12317, PropertyType.Unicode, new StorePropInfo("UserInformationCity", 12317, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016EA RID: 5866
			public static readonly StorePropTag UserInformationCountry = new StorePropTag(12318, PropertyType.Unicode, new StorePropInfo("UserInformationCountry", 12318, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016EB RID: 5867
			public static readonly StorePropTag UserInformationCountryCode = new StorePropTag(12319, PropertyType.Int32, new StorePropInfo("UserInformationCountryCode", 12319, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016EC RID: 5868
			public static readonly StorePropTag UserInformationCountryOrRegion = new StorePropTag(12320, PropertyType.Unicode, new StorePropInfo("UserInformationCountryOrRegion", 12320, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016ED RID: 5869
			public static readonly StorePropTag UserInformationDefaultMailTip = new StorePropTag(12321, PropertyType.Unicode, new StorePropInfo("UserInformationDefaultMailTip", 12321, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016EE RID: 5870
			public static readonly StorePropTag UserInformationDeliverToMailboxAndForward = new StorePropTag(12322, PropertyType.Boolean, new StorePropInfo("UserInformationDeliverToMailboxAndForward", 12322, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016EF RID: 5871
			public static readonly StorePropTag UserInformationDescription = new StorePropTag(12323, PropertyType.MVUnicode, new StorePropInfo("UserInformationDescription", 12323, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016F0 RID: 5872
			public static readonly StorePropTag UserInformationDisabledArchiveGuid = new StorePropTag(12324, PropertyType.Guid, new StorePropInfo("UserInformationDisabledArchiveGuid", 12324, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016F1 RID: 5873
			public static readonly StorePropTag UserInformationDowngradeHighPriorityMessagesEnabled = new StorePropTag(12325, PropertyType.Boolean, new StorePropInfo("UserInformationDowngradeHighPriorityMessagesEnabled", 12325, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016F2 RID: 5874
			public static readonly StorePropTag UserInformationECPEnabled = new StorePropTag(12326, PropertyType.Boolean, new StorePropInfo("UserInformationECPEnabled", 12326, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016F3 RID: 5875
			public static readonly StorePropTag UserInformationEmailAddressPolicyEnabled = new StorePropTag(12327, PropertyType.Boolean, new StorePropInfo("UserInformationEmailAddressPolicyEnabled", 12327, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016F4 RID: 5876
			public static readonly StorePropTag UserInformationEwsAllowEntourage = new StorePropTag(12328, PropertyType.Boolean, new StorePropInfo("UserInformationEwsAllowEntourage", 12328, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016F5 RID: 5877
			public static readonly StorePropTag UserInformationEwsAllowMacOutlook = new StorePropTag(12329, PropertyType.Boolean, new StorePropInfo("UserInformationEwsAllowMacOutlook", 12329, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016F6 RID: 5878
			public static readonly StorePropTag UserInformationEwsAllowOutlook = new StorePropTag(12330, PropertyType.Boolean, new StorePropInfo("UserInformationEwsAllowOutlook", 12330, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016F7 RID: 5879
			public static readonly StorePropTag UserInformationEwsApplicationAccessPolicy = new StorePropTag(12331, PropertyType.Int32, new StorePropInfo("UserInformationEwsApplicationAccessPolicy", 12331, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016F8 RID: 5880
			public static readonly StorePropTag UserInformationEwsEnabled = new StorePropTag(12332, PropertyType.Int32, new StorePropInfo("UserInformationEwsEnabled", 12332, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016F9 RID: 5881
			public static readonly StorePropTag UserInformationEwsExceptions = new StorePropTag(12333, PropertyType.MVUnicode, new StorePropInfo("UserInformationEwsExceptions", 12333, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016FA RID: 5882
			public static readonly StorePropTag UserInformationEwsWellKnownApplicationAccessPolicies = new StorePropTag(12334, PropertyType.MVUnicode, new StorePropInfo("UserInformationEwsWellKnownApplicationAccessPolicies", 12334, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016FB RID: 5883
			public static readonly StorePropTag UserInformationExchangeGuid = new StorePropTag(12335, PropertyType.Guid, new StorePropInfo("UserInformationExchangeGuid", 12335, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016FC RID: 5884
			public static readonly StorePropTag UserInformationExternalOofOptions = new StorePropTag(12336, PropertyType.Int32, new StorePropInfo("UserInformationExternalOofOptions", 12336, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016FD RID: 5885
			public static readonly StorePropTag UserInformationFirstName = new StorePropTag(12337, PropertyType.Unicode, new StorePropInfo("UserInformationFirstName", 12337, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016FE RID: 5886
			public static readonly StorePropTag UserInformationForwardingSmtpAddress = new StorePropTag(12338, PropertyType.Unicode, new StorePropInfo("UserInformationForwardingSmtpAddress", 12338, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x040016FF RID: 5887
			public static readonly StorePropTag UserInformationGender = new StorePropTag(12339, PropertyType.Unicode, new StorePropInfo("UserInformationGender", 12339, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001700 RID: 5888
			public static readonly StorePropTag UserInformationGenericForwardingAddress = new StorePropTag(12340, PropertyType.Unicode, new StorePropInfo("UserInformationGenericForwardingAddress", 12340, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001701 RID: 5889
			public static readonly StorePropTag UserInformationGeoCoordinates = new StorePropTag(12341, PropertyType.Unicode, new StorePropInfo("UserInformationGeoCoordinates", 12341, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001702 RID: 5890
			public static readonly StorePropTag UserInformationHABSeniorityIndex = new StorePropTag(12342, PropertyType.Int32, new StorePropInfo("UserInformationHABSeniorityIndex", 12342, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001703 RID: 5891
			public static readonly StorePropTag UserInformationHasActiveSyncDevicePartnership = new StorePropTag(12343, PropertyType.Boolean, new StorePropInfo("UserInformationHasActiveSyncDevicePartnership", 12343, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001704 RID: 5892
			public static readonly StorePropTag UserInformationHiddenFromAddressListsEnabled = new StorePropTag(12344, PropertyType.Boolean, new StorePropInfo("UserInformationHiddenFromAddressListsEnabled", 12344, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001705 RID: 5893
			public static readonly StorePropTag UserInformationHiddenFromAddressListsValue = new StorePropTag(12345, PropertyType.Boolean, new StorePropInfo("UserInformationHiddenFromAddressListsValue", 12345, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001706 RID: 5894
			public static readonly StorePropTag UserInformationHomePhone = new StorePropTag(12346, PropertyType.Unicode, new StorePropInfo("UserInformationHomePhone", 12346, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001707 RID: 5895
			public static readonly StorePropTag UserInformationImapEnabled = new StorePropTag(12347, PropertyType.Boolean, new StorePropInfo("UserInformationImapEnabled", 12347, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001708 RID: 5896
			public static readonly StorePropTag UserInformationImapEnableExactRFC822Size = new StorePropTag(12348, PropertyType.Boolean, new StorePropInfo("UserInformationImapEnableExactRFC822Size", 12348, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001709 RID: 5897
			public static readonly StorePropTag UserInformationImapForceICalForCalendarRetrievalOption = new StorePropTag(12349, PropertyType.Boolean, new StorePropInfo("UserInformationImapForceICalForCalendarRetrievalOption", 12349, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400170A RID: 5898
			public static readonly StorePropTag UserInformationImapMessagesRetrievalMimeFormat = new StorePropTag(12350, PropertyType.Int32, new StorePropInfo("UserInformationImapMessagesRetrievalMimeFormat", 12350, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400170B RID: 5899
			public static readonly StorePropTag UserInformationImapProtocolLoggingEnabled = new StorePropTag(12351, PropertyType.Int32, new StorePropInfo("UserInformationImapProtocolLoggingEnabled", 12351, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400170C RID: 5900
			public static readonly StorePropTag UserInformationImapSuppressReadReceipt = new StorePropTag(12352, PropertyType.Boolean, new StorePropInfo("UserInformationImapSuppressReadReceipt", 12352, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400170D RID: 5901
			public static readonly StorePropTag UserInformationImapUseProtocolDefaults = new StorePropTag(12353, PropertyType.Boolean, new StorePropInfo("UserInformationImapUseProtocolDefaults", 12353, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400170E RID: 5902
			public static readonly StorePropTag UserInformationIncludeInGarbageCollection = new StorePropTag(12354, PropertyType.Boolean, new StorePropInfo("UserInformationIncludeInGarbageCollection", 12354, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400170F RID: 5903
			public static readonly StorePropTag UserInformationInitials = new StorePropTag(12355, PropertyType.Unicode, new StorePropInfo("UserInformationInitials", 12355, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001710 RID: 5904
			public static readonly StorePropTag UserInformationInPlaceHolds = new StorePropTag(12356, PropertyType.MVUnicode, new StorePropInfo("UserInformationInPlaceHolds", 12356, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001711 RID: 5905
			public static readonly StorePropTag UserInformationInternalOnly = new StorePropTag(12357, PropertyType.Boolean, new StorePropInfo("UserInformationInternalOnly", 12357, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001712 RID: 5906
			public static readonly StorePropTag UserInformationInternalUsageLocation = new StorePropTag(12358, PropertyType.Unicode, new StorePropInfo("UserInformationInternalUsageLocation", 12358, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001713 RID: 5907
			public static readonly StorePropTag UserInformationInternetEncoding = new StorePropTag(12359, PropertyType.Int32, new StorePropInfo("UserInformationInternetEncoding", 12359, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001714 RID: 5908
			public static readonly StorePropTag UserInformationIsCalculatedTargetAddress = new StorePropTag(12360, PropertyType.Boolean, new StorePropInfo("UserInformationIsCalculatedTargetAddress", 12360, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001715 RID: 5909
			public static readonly StorePropTag UserInformationIsExcludedFromServingHierarchy = new StorePropTag(12361, PropertyType.Boolean, new StorePropInfo("UserInformationIsExcludedFromServingHierarchy", 12361, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001716 RID: 5910
			public static readonly StorePropTag UserInformationIsHierarchyReady = new StorePropTag(12362, PropertyType.Boolean, new StorePropInfo("UserInformationIsHierarchyReady", 12362, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001717 RID: 5911
			public static readonly StorePropTag UserInformationIsInactiveMailbox = new StorePropTag(12363, PropertyType.Boolean, new StorePropInfo("UserInformationIsInactiveMailbox", 12363, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001718 RID: 5912
			public static readonly StorePropTag UserInformationIsSoftDeletedByDisable = new StorePropTag(12364, PropertyType.Boolean, new StorePropInfo("UserInformationIsSoftDeletedByDisable", 12364, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001719 RID: 5913
			public static readonly StorePropTag UserInformationIsSoftDeletedByRemove = new StorePropTag(12365, PropertyType.Boolean, new StorePropInfo("UserInformationIsSoftDeletedByRemove", 12365, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400171A RID: 5914
			public static readonly StorePropTag UserInformationIssueWarningQuota = new StorePropTag(12366, PropertyType.Unicode, new StorePropInfo("UserInformationIssueWarningQuota", 12366, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400171B RID: 5915
			public static readonly StorePropTag UserInformationJournalArchiveAddress = new StorePropTag(12367, PropertyType.Unicode, new StorePropInfo("UserInformationJournalArchiveAddress", 12367, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400171C RID: 5916
			public static readonly StorePropTag UserInformationLanguages = new StorePropTag(12368, PropertyType.MVUnicode, new StorePropInfo("UserInformationLanguages", 12368, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400171D RID: 5917
			public static readonly StorePropTag UserInformationLastExchangeChangedTime = new StorePropTag(12369, PropertyType.SysTime, new StorePropInfo("UserInformationLastExchangeChangedTime", 12369, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400171E RID: 5918
			public static readonly StorePropTag UserInformationLastName = new StorePropTag(12370, PropertyType.Unicode, new StorePropInfo("UserInformationLastName", 12370, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400171F RID: 5919
			public static readonly StorePropTag UserInformationLatitude = new StorePropTag(12371, PropertyType.Int32, new StorePropInfo("UserInformationLatitude", 12371, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001720 RID: 5920
			public static readonly StorePropTag UserInformationLEOEnabled = new StorePropTag(12372, PropertyType.Boolean, new StorePropInfo("UserInformationLEOEnabled", 12372, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001721 RID: 5921
			public static readonly StorePropTag UserInformationLocaleID = new StorePropTag(12373, PropertyType.MVInt32, new StorePropInfo("UserInformationLocaleID", 12373, PropertyType.MVInt32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001722 RID: 5922
			public static readonly StorePropTag UserInformationLongitude = new StorePropTag(12374, PropertyType.Int32, new StorePropInfo("UserInformationLongitude", 12374, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001723 RID: 5923
			public static readonly StorePropTag UserInformationMacAttachmentFormat = new StorePropTag(12375, PropertyType.Int32, new StorePropInfo("UserInformationMacAttachmentFormat", 12375, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001724 RID: 5924
			public static readonly StorePropTag UserInformationMailboxContainerGuid = new StorePropTag(12376, PropertyType.Guid, new StorePropInfo("UserInformationMailboxContainerGuid", 12376, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001725 RID: 5925
			public static readonly StorePropTag UserInformationMailboxMoveBatchName = new StorePropTag(12377, PropertyType.Unicode, new StorePropInfo("UserInformationMailboxMoveBatchName", 12377, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001726 RID: 5926
			public static readonly StorePropTag UserInformationMailboxMoveRemoteHostName = new StorePropTag(12378, PropertyType.Unicode, new StorePropInfo("UserInformationMailboxMoveRemoteHostName", 12378, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001727 RID: 5927
			public static readonly StorePropTag UserInformationMailboxMoveStatus = new StorePropTag(12379, PropertyType.Int32, new StorePropInfo("UserInformationMailboxMoveStatus", 12379, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001728 RID: 5928
			public static readonly StorePropTag UserInformationMailboxRelease = new StorePropTag(12380, PropertyType.Unicode, new StorePropInfo("UserInformationMailboxRelease", 12380, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001729 RID: 5929
			public static readonly StorePropTag UserInformationMailTipTranslations = new StorePropTag(12381, PropertyType.MVUnicode, new StorePropInfo("UserInformationMailTipTranslations", 12381, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400172A RID: 5930
			public static readonly StorePropTag UserInformationMAPIBlockOutlookNonCachedMode = new StorePropTag(12382, PropertyType.Boolean, new StorePropInfo("UserInformationMAPIBlockOutlookNonCachedMode", 12382, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400172B RID: 5931
			public static readonly StorePropTag UserInformationMAPIBlockOutlookRpcHttp = new StorePropTag(12383, PropertyType.Boolean, new StorePropInfo("UserInformationMAPIBlockOutlookRpcHttp", 12383, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400172C RID: 5932
			public static readonly StorePropTag UserInformationMAPIBlockOutlookVersions = new StorePropTag(12384, PropertyType.Unicode, new StorePropInfo("UserInformationMAPIBlockOutlookVersions", 12384, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400172D RID: 5933
			public static readonly StorePropTag UserInformationMAPIEnabled = new StorePropTag(12385, PropertyType.Boolean, new StorePropInfo("UserInformationMAPIEnabled", 12385, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400172E RID: 5934
			public static readonly StorePropTag UserInformationMapiRecipient = new StorePropTag(12386, PropertyType.Boolean, new StorePropInfo("UserInformationMapiRecipient", 12386, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400172F RID: 5935
			public static readonly StorePropTag UserInformationMaxBlockedSenders = new StorePropTag(12387, PropertyType.Int32, new StorePropInfo("UserInformationMaxBlockedSenders", 12387, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001730 RID: 5936
			public static readonly StorePropTag UserInformationMaxReceiveSize = new StorePropTag(12388, PropertyType.Unicode, new StorePropInfo("UserInformationMaxReceiveSize", 12388, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001731 RID: 5937
			public static readonly StorePropTag UserInformationMaxSafeSenders = new StorePropTag(12389, PropertyType.Int32, new StorePropInfo("UserInformationMaxSafeSenders", 12389, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001732 RID: 5938
			public static readonly StorePropTag UserInformationMaxSendSize = new StorePropTag(12390, PropertyType.Unicode, new StorePropInfo("UserInformationMaxSendSize", 12390, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001733 RID: 5939
			public static readonly StorePropTag UserInformationMemberName = new StorePropTag(12391, PropertyType.Unicode, new StorePropInfo("UserInformationMemberName", 12391, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001734 RID: 5940
			public static readonly StorePropTag UserInformationMessageBodyFormat = new StorePropTag(12392, PropertyType.Int32, new StorePropInfo("UserInformationMessageBodyFormat", 12392, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001735 RID: 5941
			public static readonly StorePropTag UserInformationMessageFormat = new StorePropTag(12393, PropertyType.Int32, new StorePropInfo("UserInformationMessageFormat", 12393, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001736 RID: 5942
			public static readonly StorePropTag UserInformationMessageTrackingReadStatusDisabled = new StorePropTag(12394, PropertyType.Boolean, new StorePropInfo("UserInformationMessageTrackingReadStatusDisabled", 12394, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001737 RID: 5943
			public static readonly StorePropTag UserInformationMobileFeaturesEnabled = new StorePropTag(12395, PropertyType.Int32, new StorePropInfo("UserInformationMobileFeaturesEnabled", 12395, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001738 RID: 5944
			public static readonly StorePropTag UserInformationMobilePhone = new StorePropTag(12396, PropertyType.Unicode, new StorePropInfo("UserInformationMobilePhone", 12396, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001739 RID: 5945
			public static readonly StorePropTag UserInformationModerationFlags = new StorePropTag(12397, PropertyType.Int32, new StorePropInfo("UserInformationModerationFlags", 12397, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400173A RID: 5946
			public static readonly StorePropTag UserInformationNotes = new StorePropTag(12398, PropertyType.Unicode, new StorePropInfo("UserInformationNotes", 12398, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400173B RID: 5947
			public static readonly StorePropTag UserInformationOccupation = new StorePropTag(12399, PropertyType.Unicode, new StorePropInfo("UserInformationOccupation", 12399, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400173C RID: 5948
			public static readonly StorePropTag UserInformationOpenDomainRoutingDisabled = new StorePropTag(12400, PropertyType.Boolean, new StorePropInfo("UserInformationOpenDomainRoutingDisabled", 12400, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400173D RID: 5949
			public static readonly StorePropTag UserInformationOtherHomePhone = new StorePropTag(12401, PropertyType.MVUnicode, new StorePropInfo("UserInformationOtherHomePhone", 12401, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400173E RID: 5950
			public static readonly StorePropTag UserInformationOtherMobile = new StorePropTag(12402, PropertyType.MVUnicode, new StorePropInfo("UserInformationOtherMobile", 12402, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400173F RID: 5951
			public static readonly StorePropTag UserInformationOtherTelephone = new StorePropTag(12403, PropertyType.MVUnicode, new StorePropInfo("UserInformationOtherTelephone", 12403, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001740 RID: 5952
			public static readonly StorePropTag UserInformationOWAEnabled = new StorePropTag(12404, PropertyType.Boolean, new StorePropInfo("UserInformationOWAEnabled", 12404, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001741 RID: 5953
			public static readonly StorePropTag UserInformationOWAforDevicesEnabled = new StorePropTag(12405, PropertyType.Boolean, new StorePropInfo("UserInformationOWAforDevicesEnabled", 12405, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001742 RID: 5954
			public static readonly StorePropTag UserInformationPager = new StorePropTag(12406, PropertyType.Unicode, new StorePropInfo("UserInformationPager", 12406, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001743 RID: 5955
			public static readonly StorePropTag UserInformationPersistedCapabilities = new StorePropTag(12407, PropertyType.MVInt32, new StorePropInfo("UserInformationPersistedCapabilities", 12407, PropertyType.MVInt32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001744 RID: 5956
			public static readonly StorePropTag UserInformationPhone = new StorePropTag(12408, PropertyType.Unicode, new StorePropInfo("UserInformationPhone", 12408, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001745 RID: 5957
			public static readonly StorePropTag UserInformationPhoneProviderId = new StorePropTag(12409, PropertyType.Unicode, new StorePropInfo("UserInformationPhoneProviderId", 12409, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001746 RID: 5958
			public static readonly StorePropTag UserInformationPopEnabled = new StorePropTag(12410, PropertyType.Boolean, new StorePropInfo("UserInformationPopEnabled", 12410, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001747 RID: 5959
			public static readonly StorePropTag UserInformationPopEnableExactRFC822Size = new StorePropTag(12411, PropertyType.Boolean, new StorePropInfo("UserInformationPopEnableExactRFC822Size", 12411, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001748 RID: 5960
			public static readonly StorePropTag UserInformationPopForceICalForCalendarRetrievalOption = new StorePropTag(12412, PropertyType.Boolean, new StorePropInfo("UserInformationPopForceICalForCalendarRetrievalOption", 12412, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001749 RID: 5961
			public static readonly StorePropTag UserInformationPopMessagesRetrievalMimeFormat = new StorePropTag(12413, PropertyType.Int32, new StorePropInfo("UserInformationPopMessagesRetrievalMimeFormat", 12413, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400174A RID: 5962
			public static readonly StorePropTag UserInformationPopProtocolLoggingEnabled = new StorePropTag(12414, PropertyType.Int32, new StorePropInfo("UserInformationPopProtocolLoggingEnabled", 12414, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400174B RID: 5963
			public static readonly StorePropTag UserInformationPopSuppressReadReceipt = new StorePropTag(12415, PropertyType.Boolean, new StorePropInfo("UserInformationPopSuppressReadReceipt", 12415, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400174C RID: 5964
			public static readonly StorePropTag UserInformationPopUseProtocolDefaults = new StorePropTag(12416, PropertyType.Boolean, new StorePropInfo("UserInformationPopUseProtocolDefaults", 12416, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400174D RID: 5965
			public static readonly StorePropTag UserInformationPostalCode = new StorePropTag(12417, PropertyType.Unicode, new StorePropInfo("UserInformationPostalCode", 12417, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400174E RID: 5966
			public static readonly StorePropTag UserInformationPostOfficeBox = new StorePropTag(12418, PropertyType.MVUnicode, new StorePropInfo("UserInformationPostOfficeBox", 12418, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400174F RID: 5967
			public static readonly StorePropTag UserInformationPreviousExchangeGuid = new StorePropTag(12419, PropertyType.Guid, new StorePropInfo("UserInformationPreviousExchangeGuid", 12419, PropertyType.Guid, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001750 RID: 5968
			public static readonly StorePropTag UserInformationPreviousRecipientTypeDetails = new StorePropTag(12420, PropertyType.Int32, new StorePropInfo("UserInformationPreviousRecipientTypeDetails", 12420, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001751 RID: 5969
			public static readonly StorePropTag UserInformationProhibitSendQuota = new StorePropTag(12421, PropertyType.Unicode, new StorePropInfo("UserInformationProhibitSendQuota", 12421, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001752 RID: 5970
			public static readonly StorePropTag UserInformationProhibitSendReceiveQuota = new StorePropTag(12422, PropertyType.Unicode, new StorePropInfo("UserInformationProhibitSendReceiveQuota", 12422, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001753 RID: 5971
			public static readonly StorePropTag UserInformationQueryBaseDNRestrictionEnabled = new StorePropTag(12423, PropertyType.Boolean, new StorePropInfo("UserInformationQueryBaseDNRestrictionEnabled", 12423, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001754 RID: 5972
			public static readonly StorePropTag UserInformationRecipientDisplayType = new StorePropTag(12424, PropertyType.Int32, new StorePropInfo("UserInformationRecipientDisplayType", 12424, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001755 RID: 5973
			public static readonly StorePropTag UserInformationRecipientLimits = new StorePropTag(12425, PropertyType.Unicode, new StorePropInfo("UserInformationRecipientLimits", 12425, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001756 RID: 5974
			public static readonly StorePropTag UserInformationRecipientSoftDeletedStatus = new StorePropTag(12426, PropertyType.Int32, new StorePropInfo("UserInformationRecipientSoftDeletedStatus", 12426, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001757 RID: 5975
			public static readonly StorePropTag UserInformationRecoverableItemsQuota = new StorePropTag(12427, PropertyType.Unicode, new StorePropInfo("UserInformationRecoverableItemsQuota", 12427, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001758 RID: 5976
			public static readonly StorePropTag UserInformationRecoverableItemsWarningQuota = new StorePropTag(12428, PropertyType.Unicode, new StorePropInfo("UserInformationRecoverableItemsWarningQuota", 12428, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001759 RID: 5977
			public static readonly StorePropTag UserInformationRegion = new StorePropTag(12429, PropertyType.Unicode, new StorePropInfo("UserInformationRegion", 12429, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400175A RID: 5978
			public static readonly StorePropTag UserInformationRemotePowerShellEnabled = new StorePropTag(12430, PropertyType.Boolean, new StorePropInfo("UserInformationRemotePowerShellEnabled", 12430, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400175B RID: 5979
			public static readonly StorePropTag UserInformationRemoteRecipientType = new StorePropTag(12431, PropertyType.Int32, new StorePropInfo("UserInformationRemoteRecipientType", 12431, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400175C RID: 5980
			public static readonly StorePropTag UserInformationRequireAllSendersAreAuthenticated = new StorePropTag(12432, PropertyType.Boolean, new StorePropInfo("UserInformationRequireAllSendersAreAuthenticated", 12432, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400175D RID: 5981
			public static readonly StorePropTag UserInformationResetPasswordOnNextLogon = new StorePropTag(12433, PropertyType.Boolean, new StorePropInfo("UserInformationResetPasswordOnNextLogon", 12433, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400175E RID: 5982
			public static readonly StorePropTag UserInformationRetainDeletedItemsFor = new StorePropTag(12434, PropertyType.Int64, new StorePropInfo("UserInformationRetainDeletedItemsFor", 12434, PropertyType.Int64, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400175F RID: 5983
			public static readonly StorePropTag UserInformationRetainDeletedItemsUntilBackup = new StorePropTag(12435, PropertyType.Boolean, new StorePropInfo("UserInformationRetainDeletedItemsUntilBackup", 12435, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001760 RID: 5984
			public static readonly StorePropTag UserInformationRulesQuota = new StorePropTag(12436, PropertyType.Unicode, new StorePropInfo("UserInformationRulesQuota", 12436, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001761 RID: 5985
			public static readonly StorePropTag UserInformationShouldUseDefaultRetentionPolicy = new StorePropTag(12437, PropertyType.Boolean, new StorePropInfo("UserInformationShouldUseDefaultRetentionPolicy", 12437, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001762 RID: 5986
			public static readonly StorePropTag UserInformationSimpleDisplayName = new StorePropTag(12438, PropertyType.Unicode, new StorePropInfo("UserInformationSimpleDisplayName", 12438, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001763 RID: 5987
			public static readonly StorePropTag UserInformationSingleItemRecoveryEnabled = new StorePropTag(12439, PropertyType.Boolean, new StorePropInfo("UserInformationSingleItemRecoveryEnabled", 12439, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001764 RID: 5988
			public static readonly StorePropTag UserInformationStateOrProvince = new StorePropTag(12440, PropertyType.Unicode, new StorePropInfo("UserInformationStateOrProvince", 12440, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001765 RID: 5989
			public static readonly StorePropTag UserInformationStreetAddress = new StorePropTag(12441, PropertyType.Unicode, new StorePropInfo("UserInformationStreetAddress", 12441, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001766 RID: 5990
			public static readonly StorePropTag UserInformationSubscriberAccessEnabled = new StorePropTag(12442, PropertyType.Boolean, new StorePropInfo("UserInformationSubscriberAccessEnabled", 12442, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001767 RID: 5991
			public static readonly StorePropTag UserInformationTextEncodedORAddress = new StorePropTag(12443, PropertyType.Unicode, new StorePropInfo("UserInformationTextEncodedORAddress", 12443, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001768 RID: 5992
			public static readonly StorePropTag UserInformationTextMessagingState = new StorePropTag(12444, PropertyType.MVUnicode, new StorePropInfo("UserInformationTextMessagingState", 12444, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001769 RID: 5993
			public static readonly StorePropTag UserInformationTimezone = new StorePropTag(12445, PropertyType.Unicode, new StorePropInfo("UserInformationTimezone", 12445, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400176A RID: 5994
			public static readonly StorePropTag UserInformationUCSImListMigrationCompleted = new StorePropTag(12446, PropertyType.Boolean, new StorePropInfo("UserInformationUCSImListMigrationCompleted", 12446, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400176B RID: 5995
			public static readonly StorePropTag UserInformationUpgradeDetails = new StorePropTag(12447, PropertyType.Unicode, new StorePropInfo("UserInformationUpgradeDetails", 12447, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400176C RID: 5996
			public static readonly StorePropTag UserInformationUpgradeMessage = new StorePropTag(12448, PropertyType.Unicode, new StorePropInfo("UserInformationUpgradeMessage", 12448, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400176D RID: 5997
			public static readonly StorePropTag UserInformationUpgradeRequest = new StorePropTag(12449, PropertyType.Int32, new StorePropInfo("UserInformationUpgradeRequest", 12449, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400176E RID: 5998
			public static readonly StorePropTag UserInformationUpgradeStage = new StorePropTag(12450, PropertyType.Int32, new StorePropInfo("UserInformationUpgradeStage", 12450, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400176F RID: 5999
			public static readonly StorePropTag UserInformationUpgradeStageTimeStamp = new StorePropTag(12451, PropertyType.SysTime, new StorePropInfo("UserInformationUpgradeStageTimeStamp", 12451, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001770 RID: 6000
			public static readonly StorePropTag UserInformationUpgradeStatus = new StorePropTag(12452, PropertyType.Int32, new StorePropInfo("UserInformationUpgradeStatus", 12452, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001771 RID: 6001
			public static readonly StorePropTag UserInformationUsageLocation = new StorePropTag(12453, PropertyType.Unicode, new StorePropInfo("UserInformationUsageLocation", 12453, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001772 RID: 6002
			public static readonly StorePropTag UserInformationUseMapiRichTextFormat = new StorePropTag(12454, PropertyType.Int32, new StorePropInfo("UserInformationUseMapiRichTextFormat", 12454, PropertyType.Int32, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001773 RID: 6003
			public static readonly StorePropTag UserInformationUsePreferMessageFormat = new StorePropTag(12455, PropertyType.Boolean, new StorePropInfo("UserInformationUsePreferMessageFormat", 12455, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001774 RID: 6004
			public static readonly StorePropTag UserInformationUseUCCAuditConfig = new StorePropTag(12456, PropertyType.Boolean, new StorePropInfo("UserInformationUseUCCAuditConfig", 12456, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001775 RID: 6005
			public static readonly StorePropTag UserInformationWebPage = new StorePropTag(12457, PropertyType.Unicode, new StorePropInfo("UserInformationWebPage", 12457, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001776 RID: 6006
			public static readonly StorePropTag UserInformationWhenMailboxCreated = new StorePropTag(12458, PropertyType.SysTime, new StorePropInfo("UserInformationWhenMailboxCreated", 12458, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001777 RID: 6007
			public static readonly StorePropTag UserInformationWhenSoftDeleted = new StorePropTag(12459, PropertyType.SysTime, new StorePropInfo("UserInformationWhenSoftDeleted", 12459, PropertyType.SysTime, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001778 RID: 6008
			public static readonly StorePropTag UserInformationBirthdayPrecision = new StorePropTag(12460, PropertyType.Unicode, new StorePropInfo("UserInformationBirthdayPrecision", 12460, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001779 RID: 6009
			public static readonly StorePropTag UserInformationNameVersion = new StorePropTag(12461, PropertyType.Unicode, new StorePropInfo("UserInformationNameVersion", 12461, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400177A RID: 6010
			public static readonly StorePropTag UserInformationOptInUser = new StorePropTag(12462, PropertyType.Boolean, new StorePropInfo("UserInformationOptInUser", 12462, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400177B RID: 6011
			public static readonly StorePropTag UserInformationIsMigratedConsumerMailbox = new StorePropTag(12463, PropertyType.Boolean, new StorePropInfo("UserInformationIsMigratedConsumerMailbox", 12463, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400177C RID: 6012
			public static readonly StorePropTag UserInformationMigrationDryRun = new StorePropTag(12464, PropertyType.Boolean, new StorePropInfo("UserInformationMigrationDryRun", 12464, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400177D RID: 6013
			public static readonly StorePropTag UserInformationIsPremiumConsumerMailbox = new StorePropTag(12465, PropertyType.Boolean, new StorePropInfo("UserInformationIsPremiumConsumerMailbox", 12465, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400177E RID: 6014
			public static readonly StorePropTag UserInformationAlternateSupportEmailAddresses = new StorePropTag(12466, PropertyType.Unicode, new StorePropInfo("UserInformationAlternateSupportEmailAddresses", 12466, PropertyType.Unicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x0400177F RID: 6015
			public static readonly StorePropTag UserInformationEmailAddresses = new StorePropTag(12467, PropertyType.MVUnicode, new StorePropInfo("UserInformationEmailAddresses", 12467, PropertyType.MVUnicode, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001780 RID: 6016
			public static readonly StorePropTag UserInformationMapiHttpEnabled = new StorePropTag(12502, PropertyType.Boolean, new StorePropInfo("UserInformationMapiHttpEnabled", 12502, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001781 RID: 6017
			public static readonly StorePropTag UserInformationMAPIBlockOutlookExternalConnectivity = new StorePropTag(12503, PropertyType.Boolean, new StorePropInfo("UserInformationMAPIBlockOutlookExternalConnectivity", 12503, PropertyType.Boolean, StorePropInfo.Flags.None, 0UL, default(PropertyCategories)), ObjectType.UserInfo);

			// Token: 0x04001782 RID: 6018
			public static readonly StorePropTag[] NoGetPropProperties = new StorePropTag[0];

			// Token: 0x04001783 RID: 6019
			public static readonly StorePropTag[] NoGetPropListProperties = new StorePropTag[0];

			// Token: 0x04001784 RID: 6020
			public static readonly StorePropTag[] NoGetPropListForFastTransferProperties = new StorePropTag[0];

			// Token: 0x04001785 RID: 6021
			public static readonly StorePropTag[] SetPropRestrictedProperties = new StorePropTag[]
			{
				PropTag.UserInfo.UserInformationGuid,
				PropTag.UserInfo.UserInformationCreationTime,
				PropTag.UserInfo.UserInformationLastModificationTime,
				PropTag.UserInfo.UserInformationChangeNumber
			};

			// Token: 0x04001786 RID: 6022
			public static readonly StorePropTag[] SetPropAllowedForMailboxMoveProperties = new StorePropTag[]
			{
				PropTag.UserInfo.UserInformationCreationTime,
				PropTag.UserInfo.UserInformationLastModificationTime,
				PropTag.UserInfo.UserInformationChangeNumber
			};

			// Token: 0x04001787 RID: 6023
			public static readonly StorePropTag[] SetPropAllowedForAdminProperties = new StorePropTag[0];

			// Token: 0x04001788 RID: 6024
			public static readonly StorePropTag[] SetPropAllowedForTransportProperties = new StorePropTag[0];

			// Token: 0x04001789 RID: 6025
			public static readonly StorePropTag[] SetPropAllowedOnEmbeddedMessageProperties = new StorePropTag[0];

			// Token: 0x0400178A RID: 6026
			public static readonly StorePropTag[] FacebookProtectedPropertiesProperties = new StorePropTag[0];

			// Token: 0x0400178B RID: 6027
			public static readonly StorePropTag[] NoCopyProperties = new StorePropTag[0];

			// Token: 0x0400178C RID: 6028
			public static readonly StorePropTag[] ComputedProperties = new StorePropTag[0];

			// Token: 0x0400178D RID: 6029
			public static readonly StorePropTag[] IgnoreSetErrorProperties = new StorePropTag[0];

			// Token: 0x0400178E RID: 6030
			public static readonly StorePropTag[] MessageBodyProperties = new StorePropTag[0];

			// Token: 0x0400178F RID: 6031
			public static readonly StorePropTag[] CAIProperties = new StorePropTag[0];

			// Token: 0x04001790 RID: 6032
			public static readonly StorePropTag[] ServerOnlySyncGroupPropertyProperties = new StorePropTag[0];

			// Token: 0x04001791 RID: 6033
			public static readonly StorePropTag[] SensitiveProperties = new StorePropTag[0];

			// Token: 0x04001792 RID: 6034
			public static readonly StorePropTag[] DoNotBumpChangeNumberProperties = new StorePropTag[0];

			// Token: 0x04001793 RID: 6035
			public static readonly StorePropTag[] DoNotDeleteAtFXCopyToDestinationProperties = new StorePropTag[0];

			// Token: 0x04001794 RID: 6036
			public static readonly StorePropTag[] TestProperties = new StorePropTag[0];

			// Token: 0x04001795 RID: 6037
			public static readonly StorePropTag[] AllProperties = new StorePropTag[]
			{
				PropTag.UserInfo.UserInformationGuid,
				PropTag.UserInfo.UserInformationDisplayName,
				PropTag.UserInfo.UserInformationCreationTime,
				PropTag.UserInfo.UserInformationLastModificationTime,
				PropTag.UserInfo.UserInformationChangeNumber,
				PropTag.UserInfo.UserInformationLastInteractiveLogonTime,
				PropTag.UserInfo.UserInformationActiveSyncAllowedDeviceIDs,
				PropTag.UserInfo.UserInformationActiveSyncBlockedDeviceIDs,
				PropTag.UserInfo.UserInformationActiveSyncDebugLogging,
				PropTag.UserInfo.UserInformationActiveSyncEnabled,
				PropTag.UserInfo.UserInformationAdminDisplayName,
				PropTag.UserInfo.UserInformationAggregationSubscriptionCredential,
				PropTag.UserInfo.UserInformationAllowArchiveAddressSync,
				PropTag.UserInfo.UserInformationAltitude,
				PropTag.UserInfo.UserInformationAntispamBypassEnabled,
				PropTag.UserInfo.UserInformationArchiveDomain,
				PropTag.UserInfo.UserInformationArchiveGuid,
				PropTag.UserInfo.UserInformationArchiveName,
				PropTag.UserInfo.UserInformationArchiveQuota,
				PropTag.UserInfo.UserInformationArchiveRelease,
				PropTag.UserInfo.UserInformationArchiveStatus,
				PropTag.UserInfo.UserInformationArchiveWarningQuota,
				PropTag.UserInfo.UserInformationAssistantName,
				PropTag.UserInfo.UserInformationBirthdate,
				PropTag.UserInfo.UserInformationBypassNestedModerationEnabled,
				PropTag.UserInfo.UserInformationC,
				PropTag.UserInfo.UserInformationCalendarLoggingQuota,
				PropTag.UserInfo.UserInformationCalendarRepairDisabled,
				PropTag.UserInfo.UserInformationCalendarVersionStoreDisabled,
				PropTag.UserInfo.UserInformationCity,
				PropTag.UserInfo.UserInformationCountry,
				PropTag.UserInfo.UserInformationCountryCode,
				PropTag.UserInfo.UserInformationCountryOrRegion,
				PropTag.UserInfo.UserInformationDefaultMailTip,
				PropTag.UserInfo.UserInformationDeliverToMailboxAndForward,
				PropTag.UserInfo.UserInformationDescription,
				PropTag.UserInfo.UserInformationDisabledArchiveGuid,
				PropTag.UserInfo.UserInformationDowngradeHighPriorityMessagesEnabled,
				PropTag.UserInfo.UserInformationECPEnabled,
				PropTag.UserInfo.UserInformationEmailAddressPolicyEnabled,
				PropTag.UserInfo.UserInformationEwsAllowEntourage,
				PropTag.UserInfo.UserInformationEwsAllowMacOutlook,
				PropTag.UserInfo.UserInformationEwsAllowOutlook,
				PropTag.UserInfo.UserInformationEwsApplicationAccessPolicy,
				PropTag.UserInfo.UserInformationEwsEnabled,
				PropTag.UserInfo.UserInformationEwsExceptions,
				PropTag.UserInfo.UserInformationEwsWellKnownApplicationAccessPolicies,
				PropTag.UserInfo.UserInformationExchangeGuid,
				PropTag.UserInfo.UserInformationExternalOofOptions,
				PropTag.UserInfo.UserInformationFirstName,
				PropTag.UserInfo.UserInformationForwardingSmtpAddress,
				PropTag.UserInfo.UserInformationGender,
				PropTag.UserInfo.UserInformationGenericForwardingAddress,
				PropTag.UserInfo.UserInformationGeoCoordinates,
				PropTag.UserInfo.UserInformationHABSeniorityIndex,
				PropTag.UserInfo.UserInformationHasActiveSyncDevicePartnership,
				PropTag.UserInfo.UserInformationHiddenFromAddressListsEnabled,
				PropTag.UserInfo.UserInformationHiddenFromAddressListsValue,
				PropTag.UserInfo.UserInformationHomePhone,
				PropTag.UserInfo.UserInformationImapEnabled,
				PropTag.UserInfo.UserInformationImapEnableExactRFC822Size,
				PropTag.UserInfo.UserInformationImapForceICalForCalendarRetrievalOption,
				PropTag.UserInfo.UserInformationImapMessagesRetrievalMimeFormat,
				PropTag.UserInfo.UserInformationImapProtocolLoggingEnabled,
				PropTag.UserInfo.UserInformationImapSuppressReadReceipt,
				PropTag.UserInfo.UserInformationImapUseProtocolDefaults,
				PropTag.UserInfo.UserInformationIncludeInGarbageCollection,
				PropTag.UserInfo.UserInformationInitials,
				PropTag.UserInfo.UserInformationInPlaceHolds,
				PropTag.UserInfo.UserInformationInternalOnly,
				PropTag.UserInfo.UserInformationInternalUsageLocation,
				PropTag.UserInfo.UserInformationInternetEncoding,
				PropTag.UserInfo.UserInformationIsCalculatedTargetAddress,
				PropTag.UserInfo.UserInformationIsExcludedFromServingHierarchy,
				PropTag.UserInfo.UserInformationIsHierarchyReady,
				PropTag.UserInfo.UserInformationIsInactiveMailbox,
				PropTag.UserInfo.UserInformationIsSoftDeletedByDisable,
				PropTag.UserInfo.UserInformationIsSoftDeletedByRemove,
				PropTag.UserInfo.UserInformationIssueWarningQuota,
				PropTag.UserInfo.UserInformationJournalArchiveAddress,
				PropTag.UserInfo.UserInformationLanguages,
				PropTag.UserInfo.UserInformationLastExchangeChangedTime,
				PropTag.UserInfo.UserInformationLastName,
				PropTag.UserInfo.UserInformationLatitude,
				PropTag.UserInfo.UserInformationLEOEnabled,
				PropTag.UserInfo.UserInformationLocaleID,
				PropTag.UserInfo.UserInformationLongitude,
				PropTag.UserInfo.UserInformationMacAttachmentFormat,
				PropTag.UserInfo.UserInformationMailboxContainerGuid,
				PropTag.UserInfo.UserInformationMailboxMoveBatchName,
				PropTag.UserInfo.UserInformationMailboxMoveRemoteHostName,
				PropTag.UserInfo.UserInformationMailboxMoveStatus,
				PropTag.UserInfo.UserInformationMailboxRelease,
				PropTag.UserInfo.UserInformationMailTipTranslations,
				PropTag.UserInfo.UserInformationMAPIBlockOutlookNonCachedMode,
				PropTag.UserInfo.UserInformationMAPIBlockOutlookRpcHttp,
				PropTag.UserInfo.UserInformationMAPIBlockOutlookVersions,
				PropTag.UserInfo.UserInformationMAPIEnabled,
				PropTag.UserInfo.UserInformationMapiRecipient,
				PropTag.UserInfo.UserInformationMaxBlockedSenders,
				PropTag.UserInfo.UserInformationMaxReceiveSize,
				PropTag.UserInfo.UserInformationMaxSafeSenders,
				PropTag.UserInfo.UserInformationMaxSendSize,
				PropTag.UserInfo.UserInformationMemberName,
				PropTag.UserInfo.UserInformationMessageBodyFormat,
				PropTag.UserInfo.UserInformationMessageFormat,
				PropTag.UserInfo.UserInformationMessageTrackingReadStatusDisabled,
				PropTag.UserInfo.UserInformationMobileFeaturesEnabled,
				PropTag.UserInfo.UserInformationMobilePhone,
				PropTag.UserInfo.UserInformationModerationFlags,
				PropTag.UserInfo.UserInformationNotes,
				PropTag.UserInfo.UserInformationOccupation,
				PropTag.UserInfo.UserInformationOpenDomainRoutingDisabled,
				PropTag.UserInfo.UserInformationOtherHomePhone,
				PropTag.UserInfo.UserInformationOtherMobile,
				PropTag.UserInfo.UserInformationOtherTelephone,
				PropTag.UserInfo.UserInformationOWAEnabled,
				PropTag.UserInfo.UserInformationOWAforDevicesEnabled,
				PropTag.UserInfo.UserInformationPager,
				PropTag.UserInfo.UserInformationPersistedCapabilities,
				PropTag.UserInfo.UserInformationPhone,
				PropTag.UserInfo.UserInformationPhoneProviderId,
				PropTag.UserInfo.UserInformationPopEnabled,
				PropTag.UserInfo.UserInformationPopEnableExactRFC822Size,
				PropTag.UserInfo.UserInformationPopForceICalForCalendarRetrievalOption,
				PropTag.UserInfo.UserInformationPopMessagesRetrievalMimeFormat,
				PropTag.UserInfo.UserInformationPopProtocolLoggingEnabled,
				PropTag.UserInfo.UserInformationPopSuppressReadReceipt,
				PropTag.UserInfo.UserInformationPopUseProtocolDefaults,
				PropTag.UserInfo.UserInformationPostalCode,
				PropTag.UserInfo.UserInformationPostOfficeBox,
				PropTag.UserInfo.UserInformationPreviousExchangeGuid,
				PropTag.UserInfo.UserInformationPreviousRecipientTypeDetails,
				PropTag.UserInfo.UserInformationProhibitSendQuota,
				PropTag.UserInfo.UserInformationProhibitSendReceiveQuota,
				PropTag.UserInfo.UserInformationQueryBaseDNRestrictionEnabled,
				PropTag.UserInfo.UserInformationRecipientDisplayType,
				PropTag.UserInfo.UserInformationRecipientLimits,
				PropTag.UserInfo.UserInformationRecipientSoftDeletedStatus,
				PropTag.UserInfo.UserInformationRecoverableItemsQuota,
				PropTag.UserInfo.UserInformationRecoverableItemsWarningQuota,
				PropTag.UserInfo.UserInformationRegion,
				PropTag.UserInfo.UserInformationRemotePowerShellEnabled,
				PropTag.UserInfo.UserInformationRemoteRecipientType,
				PropTag.UserInfo.UserInformationRequireAllSendersAreAuthenticated,
				PropTag.UserInfo.UserInformationResetPasswordOnNextLogon,
				PropTag.UserInfo.UserInformationRetainDeletedItemsFor,
				PropTag.UserInfo.UserInformationRetainDeletedItemsUntilBackup,
				PropTag.UserInfo.UserInformationRulesQuota,
				PropTag.UserInfo.UserInformationShouldUseDefaultRetentionPolicy,
				PropTag.UserInfo.UserInformationSimpleDisplayName,
				PropTag.UserInfo.UserInformationSingleItemRecoveryEnabled,
				PropTag.UserInfo.UserInformationStateOrProvince,
				PropTag.UserInfo.UserInformationStreetAddress,
				PropTag.UserInfo.UserInformationSubscriberAccessEnabled,
				PropTag.UserInfo.UserInformationTextEncodedORAddress,
				PropTag.UserInfo.UserInformationTextMessagingState,
				PropTag.UserInfo.UserInformationTimezone,
				PropTag.UserInfo.UserInformationUCSImListMigrationCompleted,
				PropTag.UserInfo.UserInformationUpgradeDetails,
				PropTag.UserInfo.UserInformationUpgradeMessage,
				PropTag.UserInfo.UserInformationUpgradeRequest,
				PropTag.UserInfo.UserInformationUpgradeStage,
				PropTag.UserInfo.UserInformationUpgradeStageTimeStamp,
				PropTag.UserInfo.UserInformationUpgradeStatus,
				PropTag.UserInfo.UserInformationUsageLocation,
				PropTag.UserInfo.UserInformationUseMapiRichTextFormat,
				PropTag.UserInfo.UserInformationUsePreferMessageFormat,
				PropTag.UserInfo.UserInformationUseUCCAuditConfig,
				PropTag.UserInfo.UserInformationWebPage,
				PropTag.UserInfo.UserInformationWhenMailboxCreated,
				PropTag.UserInfo.UserInformationWhenSoftDeleted,
				PropTag.UserInfo.UserInformationBirthdayPrecision,
				PropTag.UserInfo.UserInformationNameVersion,
				PropTag.UserInfo.UserInformationOptInUser,
				PropTag.UserInfo.UserInformationIsMigratedConsumerMailbox,
				PropTag.UserInfo.UserInformationMigrationDryRun,
				PropTag.UserInfo.UserInformationIsPremiumConsumerMailbox,
				PropTag.UserInfo.UserInformationAlternateSupportEmailAddresses,
				PropTag.UserInfo.UserInformationEmailAddresses,
				PropTag.UserInfo.UserInformationMapiHttpEnabled,
				PropTag.UserInfo.UserInformationMAPIBlockOutlookExternalConnectivity
			};
		}
	}
}
