using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001C7 RID: 455
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum UnindexedFieldURIType
	{
		// Token: 0x04000A83 RID: 2691
		[XmlEnum("folder:FolderId")]
		folderFolderId,
		// Token: 0x04000A84 RID: 2692
		[XmlEnum("folder:ParentFolderId")]
		folderParentFolderId,
		// Token: 0x04000A85 RID: 2693
		[XmlEnum("folder:DisplayName")]
		folderDisplayName,
		// Token: 0x04000A86 RID: 2694
		[XmlEnum("folder:UnreadCount")]
		folderUnreadCount,
		// Token: 0x04000A87 RID: 2695
		[XmlEnum("folder:TotalCount")]
		folderTotalCount,
		// Token: 0x04000A88 RID: 2696
		[XmlEnum("folder:ChildFolderCount")]
		folderChildFolderCount,
		// Token: 0x04000A89 RID: 2697
		[XmlEnum("folder:FolderClass")]
		folderFolderClass,
		// Token: 0x04000A8A RID: 2698
		[XmlEnum("folder:SearchParameters")]
		folderSearchParameters,
		// Token: 0x04000A8B RID: 2699
		[XmlEnum("folder:ManagedFolderInformation")]
		folderManagedFolderInformation,
		// Token: 0x04000A8C RID: 2700
		[XmlEnum("folder:PermissionSet")]
		folderPermissionSet,
		// Token: 0x04000A8D RID: 2701
		[XmlEnum("folder:EffectiveRights")]
		folderEffectiveRights,
		// Token: 0x04000A8E RID: 2702
		[XmlEnum("folder:SharingEffectiveRights")]
		folderSharingEffectiveRights,
		// Token: 0x04000A8F RID: 2703
		[XmlEnum("folder:DistinguishedFolderId")]
		folderDistinguishedFolderId,
		// Token: 0x04000A90 RID: 2704
		[XmlEnum("folder:PolicyTag")]
		folderPolicyTag,
		// Token: 0x04000A91 RID: 2705
		[XmlEnum("folder:ArchiveTag")]
		folderArchiveTag,
		// Token: 0x04000A92 RID: 2706
		[XmlEnum("item:ItemId")]
		itemItemId,
		// Token: 0x04000A93 RID: 2707
		[XmlEnum("item:ParentFolderId")]
		itemParentFolderId,
		// Token: 0x04000A94 RID: 2708
		[XmlEnum("item:ItemClass")]
		itemItemClass,
		// Token: 0x04000A95 RID: 2709
		[XmlEnum("item:MimeContent")]
		itemMimeContent,
		// Token: 0x04000A96 RID: 2710
		[XmlEnum("item:Attachments")]
		itemAttachments,
		// Token: 0x04000A97 RID: 2711
		[XmlEnum("item:Subject")]
		itemSubject,
		// Token: 0x04000A98 RID: 2712
		[XmlEnum("item:DateTimeReceived")]
		itemDateTimeReceived,
		// Token: 0x04000A99 RID: 2713
		[XmlEnum("item:Size")]
		itemSize,
		// Token: 0x04000A9A RID: 2714
		[XmlEnum("item:Categories")]
		itemCategories,
		// Token: 0x04000A9B RID: 2715
		[XmlEnum("item:HasAttachments")]
		itemHasAttachments,
		// Token: 0x04000A9C RID: 2716
		[XmlEnum("item:Importance")]
		itemImportance,
		// Token: 0x04000A9D RID: 2717
		[XmlEnum("item:InReplyTo")]
		itemInReplyTo,
		// Token: 0x04000A9E RID: 2718
		[XmlEnum("item:InternetMessageHeaders")]
		itemInternetMessageHeaders,
		// Token: 0x04000A9F RID: 2719
		[XmlEnum("item:IsAssociated")]
		itemIsAssociated,
		// Token: 0x04000AA0 RID: 2720
		[XmlEnum("item:IsDraft")]
		itemIsDraft,
		// Token: 0x04000AA1 RID: 2721
		[XmlEnum("item:IsFromMe")]
		itemIsFromMe,
		// Token: 0x04000AA2 RID: 2722
		[XmlEnum("item:IsResend")]
		itemIsResend,
		// Token: 0x04000AA3 RID: 2723
		[XmlEnum("item:IsSubmitted")]
		itemIsSubmitted,
		// Token: 0x04000AA4 RID: 2724
		[XmlEnum("item:IsUnmodified")]
		itemIsUnmodified,
		// Token: 0x04000AA5 RID: 2725
		[XmlEnum("item:DateTimeSent")]
		itemDateTimeSent,
		// Token: 0x04000AA6 RID: 2726
		[XmlEnum("item:DateTimeCreated")]
		itemDateTimeCreated,
		// Token: 0x04000AA7 RID: 2727
		[XmlEnum("item:Body")]
		itemBody,
		// Token: 0x04000AA8 RID: 2728
		[XmlEnum("item:ResponseObjects")]
		itemResponseObjects,
		// Token: 0x04000AA9 RID: 2729
		[XmlEnum("item:Sensitivity")]
		itemSensitivity,
		// Token: 0x04000AAA RID: 2730
		[XmlEnum("item:ReminderDueBy")]
		itemReminderDueBy,
		// Token: 0x04000AAB RID: 2731
		[XmlEnum("item:ReminderIsSet")]
		itemReminderIsSet,
		// Token: 0x04000AAC RID: 2732
		[XmlEnum("item:ReminderNextTime")]
		itemReminderNextTime,
		// Token: 0x04000AAD RID: 2733
		[XmlEnum("item:ReminderMinutesBeforeStart")]
		itemReminderMinutesBeforeStart,
		// Token: 0x04000AAE RID: 2734
		[XmlEnum("item:DisplayTo")]
		itemDisplayTo,
		// Token: 0x04000AAF RID: 2735
		[XmlEnum("item:DisplayCc")]
		itemDisplayCc,
		// Token: 0x04000AB0 RID: 2736
		[XmlEnum("item:Culture")]
		itemCulture,
		// Token: 0x04000AB1 RID: 2737
		[XmlEnum("item:EffectiveRights")]
		itemEffectiveRights,
		// Token: 0x04000AB2 RID: 2738
		[XmlEnum("item:LastModifiedName")]
		itemLastModifiedName,
		// Token: 0x04000AB3 RID: 2739
		[XmlEnum("item:LastModifiedTime")]
		itemLastModifiedTime,
		// Token: 0x04000AB4 RID: 2740
		[XmlEnum("item:ConversationId")]
		itemConversationId,
		// Token: 0x04000AB5 RID: 2741
		[XmlEnum("item:UniqueBody")]
		itemUniqueBody,
		// Token: 0x04000AB6 RID: 2742
		[XmlEnum("item:Flag")]
		itemFlag,
		// Token: 0x04000AB7 RID: 2743
		[XmlEnum("item:StoreEntryId")]
		itemStoreEntryId,
		// Token: 0x04000AB8 RID: 2744
		[XmlEnum("item:InstanceKey")]
		itemInstanceKey,
		// Token: 0x04000AB9 RID: 2745
		[XmlEnum("item:NormalizedBody")]
		itemNormalizedBody,
		// Token: 0x04000ABA RID: 2746
		[XmlEnum("item:EntityExtractionResult")]
		itemEntityExtractionResult,
		// Token: 0x04000ABB RID: 2747
		[XmlEnum("item:PolicyTag")]
		itemPolicyTag,
		// Token: 0x04000ABC RID: 2748
		[XmlEnum("item:ArchiveTag")]
		itemArchiveTag,
		// Token: 0x04000ABD RID: 2749
		[XmlEnum("item:RetentionDate")]
		itemRetentionDate,
		// Token: 0x04000ABE RID: 2750
		[XmlEnum("item:Preview")]
		itemPreview,
		// Token: 0x04000ABF RID: 2751
		[XmlEnum("item:PredictedActionReasons")]
		itemPredictedActionReasons,
		// Token: 0x04000AC0 RID: 2752
		[XmlEnum("item:IsClutter")]
		itemIsClutter,
		// Token: 0x04000AC1 RID: 2753
		[XmlEnum("item:RightsManagementLicenseData")]
		itemRightsManagementLicenseData,
		// Token: 0x04000AC2 RID: 2754
		[XmlEnum("item:BlockStatus")]
		itemBlockStatus,
		// Token: 0x04000AC3 RID: 2755
		[XmlEnum("item:HasBlockedImages")]
		itemHasBlockedImages,
		// Token: 0x04000AC4 RID: 2756
		[XmlEnum("item:WebClientReadFormQueryString")]
		itemWebClientReadFormQueryString,
		// Token: 0x04000AC5 RID: 2757
		[XmlEnum("item:WebClientEditFormQueryString")]
		itemWebClientEditFormQueryString,
		// Token: 0x04000AC6 RID: 2758
		[XmlEnum("item:TextBody")]
		itemTextBody,
		// Token: 0x04000AC7 RID: 2759
		[XmlEnum("item:IconIndex")]
		itemIconIndex,
		// Token: 0x04000AC8 RID: 2760
		[XmlEnum("item:MimeContentUTF8")]
		itemMimeContentUTF8,
		// Token: 0x04000AC9 RID: 2761
		[XmlEnum("message:ConversationIndex")]
		messageConversationIndex,
		// Token: 0x04000ACA RID: 2762
		[XmlEnum("message:ConversationTopic")]
		messageConversationTopic,
		// Token: 0x04000ACB RID: 2763
		[XmlEnum("message:InternetMessageId")]
		messageInternetMessageId,
		// Token: 0x04000ACC RID: 2764
		[XmlEnum("message:IsRead")]
		messageIsRead,
		// Token: 0x04000ACD RID: 2765
		[XmlEnum("message:IsResponseRequested")]
		messageIsResponseRequested,
		// Token: 0x04000ACE RID: 2766
		[XmlEnum("message:IsReadReceiptRequested")]
		messageIsReadReceiptRequested,
		// Token: 0x04000ACF RID: 2767
		[XmlEnum("message:IsDeliveryReceiptRequested")]
		messageIsDeliveryReceiptRequested,
		// Token: 0x04000AD0 RID: 2768
		[XmlEnum("message:ReceivedBy")]
		messageReceivedBy,
		// Token: 0x04000AD1 RID: 2769
		[XmlEnum("message:ReceivedRepresenting")]
		messageReceivedRepresenting,
		// Token: 0x04000AD2 RID: 2770
		[XmlEnum("message:References")]
		messageReferences,
		// Token: 0x04000AD3 RID: 2771
		[XmlEnum("message:ReplyTo")]
		messageReplyTo,
		// Token: 0x04000AD4 RID: 2772
		[XmlEnum("message:From")]
		messageFrom,
		// Token: 0x04000AD5 RID: 2773
		[XmlEnum("message:Sender")]
		messageSender,
		// Token: 0x04000AD6 RID: 2774
		[XmlEnum("message:ToRecipients")]
		messageToRecipients,
		// Token: 0x04000AD7 RID: 2775
		[XmlEnum("message:CcRecipients")]
		messageCcRecipients,
		// Token: 0x04000AD8 RID: 2776
		[XmlEnum("message:BccRecipients")]
		messageBccRecipients,
		// Token: 0x04000AD9 RID: 2777
		[XmlEnum("message:ApprovalRequestData")]
		messageApprovalRequestData,
		// Token: 0x04000ADA RID: 2778
		[XmlEnum("message:VotingInformation")]
		messageVotingInformation,
		// Token: 0x04000ADB RID: 2779
		[XmlEnum("message:ReminderMessageData")]
		messageReminderMessageData,
		// Token: 0x04000ADC RID: 2780
		[XmlEnum("meeting:AssociatedCalendarItemId")]
		meetingAssociatedCalendarItemId,
		// Token: 0x04000ADD RID: 2781
		[XmlEnum("meeting:IsDelegated")]
		meetingIsDelegated,
		// Token: 0x04000ADE RID: 2782
		[XmlEnum("meeting:IsOutOfDate")]
		meetingIsOutOfDate,
		// Token: 0x04000ADF RID: 2783
		[XmlEnum("meeting:HasBeenProcessed")]
		meetingHasBeenProcessed,
		// Token: 0x04000AE0 RID: 2784
		[XmlEnum("meeting:ResponseType")]
		meetingResponseType,
		// Token: 0x04000AE1 RID: 2785
		[XmlEnum("meeting:ProposedStart")]
		meetingProposedStart,
		// Token: 0x04000AE2 RID: 2786
		[XmlEnum("meeting:ProposedEnd")]
		meetingProposedEnd,
		// Token: 0x04000AE3 RID: 2787
		[XmlEnum("meetingRequest:MeetingRequestType")]
		meetingRequestMeetingRequestType,
		// Token: 0x04000AE4 RID: 2788
		[XmlEnum("meetingRequest:IntendedFreeBusyStatus")]
		meetingRequestIntendedFreeBusyStatus,
		// Token: 0x04000AE5 RID: 2789
		[XmlEnum("meetingRequest:ChangeHighlights")]
		meetingRequestChangeHighlights,
		// Token: 0x04000AE6 RID: 2790
		[XmlEnum("calendar:Start")]
		calendarStart,
		// Token: 0x04000AE7 RID: 2791
		[XmlEnum("calendar:End")]
		calendarEnd,
		// Token: 0x04000AE8 RID: 2792
		[XmlEnum("calendar:OriginalStart")]
		calendarOriginalStart,
		// Token: 0x04000AE9 RID: 2793
		[XmlEnum("calendar:StartWallClock")]
		calendarStartWallClock,
		// Token: 0x04000AEA RID: 2794
		[XmlEnum("calendar:EndWallClock")]
		calendarEndWallClock,
		// Token: 0x04000AEB RID: 2795
		[XmlEnum("calendar:StartTimeZoneId")]
		calendarStartTimeZoneId,
		// Token: 0x04000AEC RID: 2796
		[XmlEnum("calendar:EndTimeZoneId")]
		calendarEndTimeZoneId,
		// Token: 0x04000AED RID: 2797
		[XmlEnum("calendar:IsAllDayEvent")]
		calendarIsAllDayEvent,
		// Token: 0x04000AEE RID: 2798
		[XmlEnum("calendar:LegacyFreeBusyStatus")]
		calendarLegacyFreeBusyStatus,
		// Token: 0x04000AEF RID: 2799
		[XmlEnum("calendar:Location")]
		calendarLocation,
		// Token: 0x04000AF0 RID: 2800
		[XmlEnum("calendar:EnhancedLocation")]
		calendarEnhancedLocation,
		// Token: 0x04000AF1 RID: 2801
		[XmlEnum("calendar:When")]
		calendarWhen,
		// Token: 0x04000AF2 RID: 2802
		[XmlEnum("calendar:IsMeeting")]
		calendarIsMeeting,
		// Token: 0x04000AF3 RID: 2803
		[XmlEnum("calendar:IsCancelled")]
		calendarIsCancelled,
		// Token: 0x04000AF4 RID: 2804
		[XmlEnum("calendar:IsRecurring")]
		calendarIsRecurring,
		// Token: 0x04000AF5 RID: 2805
		[XmlEnum("calendar:MeetingRequestWasSent")]
		calendarMeetingRequestWasSent,
		// Token: 0x04000AF6 RID: 2806
		[XmlEnum("calendar:IsResponseRequested")]
		calendarIsResponseRequested,
		// Token: 0x04000AF7 RID: 2807
		[XmlEnum("calendar:CalendarItemType")]
		calendarCalendarItemType,
		// Token: 0x04000AF8 RID: 2808
		[XmlEnum("calendar:MyResponseType")]
		calendarMyResponseType,
		// Token: 0x04000AF9 RID: 2809
		[XmlEnum("calendar:Organizer")]
		calendarOrganizer,
		// Token: 0x04000AFA RID: 2810
		[XmlEnum("calendar:RequiredAttendees")]
		calendarRequiredAttendees,
		// Token: 0x04000AFB RID: 2811
		[XmlEnum("calendar:OptionalAttendees")]
		calendarOptionalAttendees,
		// Token: 0x04000AFC RID: 2812
		[XmlEnum("calendar:Resources")]
		calendarResources,
		// Token: 0x04000AFD RID: 2813
		[XmlEnum("calendar:ConflictingMeetingCount")]
		calendarConflictingMeetingCount,
		// Token: 0x04000AFE RID: 2814
		[XmlEnum("calendar:AdjacentMeetingCount")]
		calendarAdjacentMeetingCount,
		// Token: 0x04000AFF RID: 2815
		[XmlEnum("calendar:ConflictingMeetings")]
		calendarConflictingMeetings,
		// Token: 0x04000B00 RID: 2816
		[XmlEnum("calendar:AdjacentMeetings")]
		calendarAdjacentMeetings,
		// Token: 0x04000B01 RID: 2817
		[XmlEnum("calendar:Duration")]
		calendarDuration,
		// Token: 0x04000B02 RID: 2818
		[XmlEnum("calendar:TimeZone")]
		calendarTimeZone,
		// Token: 0x04000B03 RID: 2819
		[XmlEnum("calendar:AppointmentReplyTime")]
		calendarAppointmentReplyTime,
		// Token: 0x04000B04 RID: 2820
		[XmlEnum("calendar:AppointmentSequenceNumber")]
		calendarAppointmentSequenceNumber,
		// Token: 0x04000B05 RID: 2821
		[XmlEnum("calendar:AppointmentState")]
		calendarAppointmentState,
		// Token: 0x04000B06 RID: 2822
		[XmlEnum("calendar:Recurrence")]
		calendarRecurrence,
		// Token: 0x04000B07 RID: 2823
		[XmlEnum("calendar:FirstOccurrence")]
		calendarFirstOccurrence,
		// Token: 0x04000B08 RID: 2824
		[XmlEnum("calendar:LastOccurrence")]
		calendarLastOccurrence,
		// Token: 0x04000B09 RID: 2825
		[XmlEnum("calendar:ModifiedOccurrences")]
		calendarModifiedOccurrences,
		// Token: 0x04000B0A RID: 2826
		[XmlEnum("calendar:DeletedOccurrences")]
		calendarDeletedOccurrences,
		// Token: 0x04000B0B RID: 2827
		[XmlEnum("calendar:MeetingTimeZone")]
		calendarMeetingTimeZone,
		// Token: 0x04000B0C RID: 2828
		[XmlEnum("calendar:ConferenceType")]
		calendarConferenceType,
		// Token: 0x04000B0D RID: 2829
		[XmlEnum("calendar:AllowNewTimeProposal")]
		calendarAllowNewTimeProposal,
		// Token: 0x04000B0E RID: 2830
		[XmlEnum("calendar:IsOnlineMeeting")]
		calendarIsOnlineMeeting,
		// Token: 0x04000B0F RID: 2831
		[XmlEnum("calendar:MeetingWorkspaceUrl")]
		calendarMeetingWorkspaceUrl,
		// Token: 0x04000B10 RID: 2832
		[XmlEnum("calendar:NetShowUrl")]
		calendarNetShowUrl,
		// Token: 0x04000B11 RID: 2833
		[XmlEnum("calendar:UID")]
		calendarUID,
		// Token: 0x04000B12 RID: 2834
		[XmlEnum("calendar:RecurrenceId")]
		calendarRecurrenceId,
		// Token: 0x04000B13 RID: 2835
		[XmlEnum("calendar:DateTimeStamp")]
		calendarDateTimeStamp,
		// Token: 0x04000B14 RID: 2836
		[XmlEnum("calendar:StartTimeZone")]
		calendarStartTimeZone,
		// Token: 0x04000B15 RID: 2837
		[XmlEnum("calendar:EndTimeZone")]
		calendarEndTimeZone,
		// Token: 0x04000B16 RID: 2838
		[XmlEnum("calendar:JoinOnlineMeetingUrl")]
		calendarJoinOnlineMeetingUrl,
		// Token: 0x04000B17 RID: 2839
		[XmlEnum("calendar:OnlineMeetingSettings")]
		calendarOnlineMeetingSettings,
		// Token: 0x04000B18 RID: 2840
		[XmlEnum("calendar:IsOrganizer")]
		calendarIsOrganizer,
		// Token: 0x04000B19 RID: 2841
		[XmlEnum("task:ActualWork")]
		taskActualWork,
		// Token: 0x04000B1A RID: 2842
		[XmlEnum("task:AssignedTime")]
		taskAssignedTime,
		// Token: 0x04000B1B RID: 2843
		[XmlEnum("task:BillingInformation")]
		taskBillingInformation,
		// Token: 0x04000B1C RID: 2844
		[XmlEnum("task:ChangeCount")]
		taskChangeCount,
		// Token: 0x04000B1D RID: 2845
		[XmlEnum("task:Companies")]
		taskCompanies,
		// Token: 0x04000B1E RID: 2846
		[XmlEnum("task:CompleteDate")]
		taskCompleteDate,
		// Token: 0x04000B1F RID: 2847
		[XmlEnum("task:Contacts")]
		taskContacts,
		// Token: 0x04000B20 RID: 2848
		[XmlEnum("task:DelegationState")]
		taskDelegationState,
		// Token: 0x04000B21 RID: 2849
		[XmlEnum("task:Delegator")]
		taskDelegator,
		// Token: 0x04000B22 RID: 2850
		[XmlEnum("task:DueDate")]
		taskDueDate,
		// Token: 0x04000B23 RID: 2851
		[XmlEnum("task:IsAssignmentEditable")]
		taskIsAssignmentEditable,
		// Token: 0x04000B24 RID: 2852
		[XmlEnum("task:IsComplete")]
		taskIsComplete,
		// Token: 0x04000B25 RID: 2853
		[XmlEnum("task:IsRecurring")]
		taskIsRecurring,
		// Token: 0x04000B26 RID: 2854
		[XmlEnum("task:IsTeamTask")]
		taskIsTeamTask,
		// Token: 0x04000B27 RID: 2855
		[XmlEnum("task:Mileage")]
		taskMileage,
		// Token: 0x04000B28 RID: 2856
		[XmlEnum("task:Owner")]
		taskOwner,
		// Token: 0x04000B29 RID: 2857
		[XmlEnum("task:PercentComplete")]
		taskPercentComplete,
		// Token: 0x04000B2A RID: 2858
		[XmlEnum("task:Recurrence")]
		taskRecurrence,
		// Token: 0x04000B2B RID: 2859
		[XmlEnum("task:StartDate")]
		taskStartDate,
		// Token: 0x04000B2C RID: 2860
		[XmlEnum("task:Status")]
		taskStatus,
		// Token: 0x04000B2D RID: 2861
		[XmlEnum("task:StatusDescription")]
		taskStatusDescription,
		// Token: 0x04000B2E RID: 2862
		[XmlEnum("task:TotalWork")]
		taskTotalWork,
		// Token: 0x04000B2F RID: 2863
		[XmlEnum("contacts:Alias")]
		contactsAlias,
		// Token: 0x04000B30 RID: 2864
		[XmlEnum("contacts:AssistantName")]
		contactsAssistantName,
		// Token: 0x04000B31 RID: 2865
		[XmlEnum("contacts:Birthday")]
		contactsBirthday,
		// Token: 0x04000B32 RID: 2866
		[XmlEnum("contacts:BusinessHomePage")]
		contactsBusinessHomePage,
		// Token: 0x04000B33 RID: 2867
		[XmlEnum("contacts:Children")]
		contactsChildren,
		// Token: 0x04000B34 RID: 2868
		[XmlEnum("contacts:Companies")]
		contactsCompanies,
		// Token: 0x04000B35 RID: 2869
		[XmlEnum("contacts:CompanyName")]
		contactsCompanyName,
		// Token: 0x04000B36 RID: 2870
		[XmlEnum("contacts:CompleteName")]
		contactsCompleteName,
		// Token: 0x04000B37 RID: 2871
		[XmlEnum("contacts:ContactSource")]
		contactsContactSource,
		// Token: 0x04000B38 RID: 2872
		[XmlEnum("contacts:Culture")]
		contactsCulture,
		// Token: 0x04000B39 RID: 2873
		[XmlEnum("contacts:Department")]
		contactsDepartment,
		// Token: 0x04000B3A RID: 2874
		[XmlEnum("contacts:DisplayName")]
		contactsDisplayName,
		// Token: 0x04000B3B RID: 2875
		[XmlEnum("contacts:DirectoryId")]
		contactsDirectoryId,
		// Token: 0x04000B3C RID: 2876
		[XmlEnum("contacts:DirectReports")]
		contactsDirectReports,
		// Token: 0x04000B3D RID: 2877
		[XmlEnum("contacts:EmailAddresses")]
		contactsEmailAddresses,
		// Token: 0x04000B3E RID: 2878
		[XmlEnum("contacts:FileAs")]
		contactsFileAs,
		// Token: 0x04000B3F RID: 2879
		[XmlEnum("contacts:FileAsMapping")]
		contactsFileAsMapping,
		// Token: 0x04000B40 RID: 2880
		[XmlEnum("contacts:Generation")]
		contactsGeneration,
		// Token: 0x04000B41 RID: 2881
		[XmlEnum("contacts:GivenName")]
		contactsGivenName,
		// Token: 0x04000B42 RID: 2882
		[XmlEnum("contacts:ImAddresses")]
		contactsImAddresses,
		// Token: 0x04000B43 RID: 2883
		[XmlEnum("contacts:Initials")]
		contactsInitials,
		// Token: 0x04000B44 RID: 2884
		[XmlEnum("contacts:JobTitle")]
		contactsJobTitle,
		// Token: 0x04000B45 RID: 2885
		[XmlEnum("contacts:Manager")]
		contactsManager,
		// Token: 0x04000B46 RID: 2886
		[XmlEnum("contacts:ManagerMailbox")]
		contactsManagerMailbox,
		// Token: 0x04000B47 RID: 2887
		[XmlEnum("contacts:MiddleName")]
		contactsMiddleName,
		// Token: 0x04000B48 RID: 2888
		[XmlEnum("contacts:Mileage")]
		contactsMileage,
		// Token: 0x04000B49 RID: 2889
		[XmlEnum("contacts:MSExchangeCertificate")]
		contactsMSExchangeCertificate,
		// Token: 0x04000B4A RID: 2890
		[XmlEnum("contacts:Nickname")]
		contactsNickname,
		// Token: 0x04000B4B RID: 2891
		[XmlEnum("contacts:Notes")]
		contactsNotes,
		// Token: 0x04000B4C RID: 2892
		[XmlEnum("contacts:OfficeLocation")]
		contactsOfficeLocation,
		// Token: 0x04000B4D RID: 2893
		[XmlEnum("contacts:PhoneNumbers")]
		contactsPhoneNumbers,
		// Token: 0x04000B4E RID: 2894
		[XmlEnum("contacts:PhoneticFullName")]
		contactsPhoneticFullName,
		// Token: 0x04000B4F RID: 2895
		[XmlEnum("contacts:PhoneticFirstName")]
		contactsPhoneticFirstName,
		// Token: 0x04000B50 RID: 2896
		[XmlEnum("contacts:PhoneticLastName")]
		contactsPhoneticLastName,
		// Token: 0x04000B51 RID: 2897
		[XmlEnum("contacts:Photo")]
		contactsPhoto,
		// Token: 0x04000B52 RID: 2898
		[XmlEnum("contacts:PhysicalAddresses")]
		contactsPhysicalAddresses,
		// Token: 0x04000B53 RID: 2899
		[XmlEnum("contacts:PostalAddressIndex")]
		contactsPostalAddressIndex,
		// Token: 0x04000B54 RID: 2900
		[XmlEnum("contacts:Profession")]
		contactsProfession,
		// Token: 0x04000B55 RID: 2901
		[XmlEnum("contacts:SpouseName")]
		contactsSpouseName,
		// Token: 0x04000B56 RID: 2902
		[XmlEnum("contacts:Surname")]
		contactsSurname,
		// Token: 0x04000B57 RID: 2903
		[XmlEnum("contacts:WeddingAnniversary")]
		contactsWeddingAnniversary,
		// Token: 0x04000B58 RID: 2904
		[XmlEnum("contacts:UserSMIMECertificate")]
		contactsUserSMIMECertificate,
		// Token: 0x04000B59 RID: 2905
		[XmlEnum("contacts:HasPicture")]
		contactsHasPicture,
		// Token: 0x04000B5A RID: 2906
		[XmlEnum("distributionlist:Members")]
		distributionlistMembers,
		// Token: 0x04000B5B RID: 2907
		[XmlEnum("postitem:PostedTime")]
		postitemPostedTime,
		// Token: 0x04000B5C RID: 2908
		[XmlEnum("conversation:ConversationId")]
		conversationConversationId,
		// Token: 0x04000B5D RID: 2909
		[XmlEnum("conversation:ConversationTopic")]
		conversationConversationTopic,
		// Token: 0x04000B5E RID: 2910
		[XmlEnum("conversation:UniqueRecipients")]
		conversationUniqueRecipients,
		// Token: 0x04000B5F RID: 2911
		[XmlEnum("conversation:GlobalUniqueRecipients")]
		conversationGlobalUniqueRecipients,
		// Token: 0x04000B60 RID: 2912
		[XmlEnum("conversation:UniqueUnreadSenders")]
		conversationUniqueUnreadSenders,
		// Token: 0x04000B61 RID: 2913
		[XmlEnum("conversation:GlobalUniqueUnreadSenders")]
		conversationGlobalUniqueUnreadSenders,
		// Token: 0x04000B62 RID: 2914
		[XmlEnum("conversation:UniqueSenders")]
		conversationUniqueSenders,
		// Token: 0x04000B63 RID: 2915
		[XmlEnum("conversation:GlobalUniqueSenders")]
		conversationGlobalUniqueSenders,
		// Token: 0x04000B64 RID: 2916
		[XmlEnum("conversation:LastDeliveryTime")]
		conversationLastDeliveryTime,
		// Token: 0x04000B65 RID: 2917
		[XmlEnum("conversation:GlobalLastDeliveryTime")]
		conversationGlobalLastDeliveryTime,
		// Token: 0x04000B66 RID: 2918
		[XmlEnum("conversation:Categories")]
		conversationCategories,
		// Token: 0x04000B67 RID: 2919
		[XmlEnum("conversation:GlobalCategories")]
		conversationGlobalCategories,
		// Token: 0x04000B68 RID: 2920
		[XmlEnum("conversation:FlagStatus")]
		conversationFlagStatus,
		// Token: 0x04000B69 RID: 2921
		[XmlEnum("conversation:GlobalFlagStatus")]
		conversationGlobalFlagStatus,
		// Token: 0x04000B6A RID: 2922
		[XmlEnum("conversation:HasAttachments")]
		conversationHasAttachments,
		// Token: 0x04000B6B RID: 2923
		[XmlEnum("conversation:GlobalHasAttachments")]
		conversationGlobalHasAttachments,
		// Token: 0x04000B6C RID: 2924
		[XmlEnum("conversation:HasIrm")]
		conversationHasIrm,
		// Token: 0x04000B6D RID: 2925
		[XmlEnum("conversation:GlobalHasIrm")]
		conversationGlobalHasIrm,
		// Token: 0x04000B6E RID: 2926
		[XmlEnum("conversation:MessageCount")]
		conversationMessageCount,
		// Token: 0x04000B6F RID: 2927
		[XmlEnum("conversation:GlobalMessageCount")]
		conversationGlobalMessageCount,
		// Token: 0x04000B70 RID: 2928
		[XmlEnum("conversation:UnreadCount")]
		conversationUnreadCount,
		// Token: 0x04000B71 RID: 2929
		[XmlEnum("conversation:GlobalUnreadCount")]
		conversationGlobalUnreadCount,
		// Token: 0x04000B72 RID: 2930
		[XmlEnum("conversation:Size")]
		conversationSize,
		// Token: 0x04000B73 RID: 2931
		[XmlEnum("conversation:GlobalSize")]
		conversationGlobalSize,
		// Token: 0x04000B74 RID: 2932
		[XmlEnum("conversation:ItemClasses")]
		conversationItemClasses,
		// Token: 0x04000B75 RID: 2933
		[XmlEnum("conversation:GlobalItemClasses")]
		conversationGlobalItemClasses,
		// Token: 0x04000B76 RID: 2934
		[XmlEnum("conversation:Importance")]
		conversationImportance,
		// Token: 0x04000B77 RID: 2935
		[XmlEnum("conversation:GlobalImportance")]
		conversationGlobalImportance,
		// Token: 0x04000B78 RID: 2936
		[XmlEnum("conversation:ItemIds")]
		conversationItemIds,
		// Token: 0x04000B79 RID: 2937
		[XmlEnum("conversation:GlobalItemIds")]
		conversationGlobalItemIds,
		// Token: 0x04000B7A RID: 2938
		[XmlEnum("conversation:LastModifiedTime")]
		conversationLastModifiedTime,
		// Token: 0x04000B7B RID: 2939
		[XmlEnum("conversation:InstanceKey")]
		conversationInstanceKey,
		// Token: 0x04000B7C RID: 2940
		[XmlEnum("conversation:Preview")]
		conversationPreview,
		// Token: 0x04000B7D RID: 2941
		[XmlEnum("conversation:IconIndex")]
		conversationIconIndex,
		// Token: 0x04000B7E RID: 2942
		[XmlEnum("conversation:GlobalIconIndex")]
		conversationGlobalIconIndex,
		// Token: 0x04000B7F RID: 2943
		[XmlEnum("conversation:DraftItemIds")]
		conversationDraftItemIds,
		// Token: 0x04000B80 RID: 2944
		[XmlEnum("conversation:HasClutter")]
		conversationHasClutter,
		// Token: 0x04000B81 RID: 2945
		[XmlEnum("persona:PersonaId")]
		personaPersonaId,
		// Token: 0x04000B82 RID: 2946
		[XmlEnum("persona:PersonaType")]
		personaPersonaType,
		// Token: 0x04000B83 RID: 2947
		[XmlEnum("persona:GivenName")]
		personaGivenName,
		// Token: 0x04000B84 RID: 2948
		[XmlEnum("persona:CompanyName")]
		personaCompanyName,
		// Token: 0x04000B85 RID: 2949
		[XmlEnum("persona:Surname")]
		personaSurname,
		// Token: 0x04000B86 RID: 2950
		[XmlEnum("persona:DisplayName")]
		personaDisplayName,
		// Token: 0x04000B87 RID: 2951
		[XmlEnum("persona:EmailAddress")]
		personaEmailAddress,
		// Token: 0x04000B88 RID: 2952
		[XmlEnum("persona:FileAs")]
		personaFileAs,
		// Token: 0x04000B89 RID: 2953
		[XmlEnum("persona:HomeCity")]
		personaHomeCity,
		// Token: 0x04000B8A RID: 2954
		[XmlEnum("persona:CreationTime")]
		personaCreationTime,
		// Token: 0x04000B8B RID: 2955
		[XmlEnum("persona:RelevanceScore")]
		personaRelevanceScore,
		// Token: 0x04000B8C RID: 2956
		[XmlEnum("persona:WorkCity")]
		personaWorkCity,
		// Token: 0x04000B8D RID: 2957
		[XmlEnum("persona:PersonaObjectStatus")]
		personaPersonaObjectStatus,
		// Token: 0x04000B8E RID: 2958
		[XmlEnum("persona:FileAsId")]
		personaFileAsId,
		// Token: 0x04000B8F RID: 2959
		[XmlEnum("persona:DisplayNamePrefix")]
		personaDisplayNamePrefix,
		// Token: 0x04000B90 RID: 2960
		[XmlEnum("persona:YomiCompanyName")]
		personaYomiCompanyName,
		// Token: 0x04000B91 RID: 2961
		[XmlEnum("persona:YomiFirstName")]
		personaYomiFirstName,
		// Token: 0x04000B92 RID: 2962
		[XmlEnum("persona:YomiLastName")]
		personaYomiLastName,
		// Token: 0x04000B93 RID: 2963
		[XmlEnum("persona:Title")]
		personaTitle,
		// Token: 0x04000B94 RID: 2964
		[XmlEnum("persona:EmailAddresses")]
		personaEmailAddresses,
		// Token: 0x04000B95 RID: 2965
		[XmlEnum("persona:PhoneNumber")]
		personaPhoneNumber,
		// Token: 0x04000B96 RID: 2966
		[XmlEnum("persona:ImAddress")]
		personaImAddress,
		// Token: 0x04000B97 RID: 2967
		[XmlEnum("persona:ImAddresses")]
		personaImAddresses,
		// Token: 0x04000B98 RID: 2968
		[XmlEnum("persona:ImAddresses2")]
		personaImAddresses2,
		// Token: 0x04000B99 RID: 2969
		[XmlEnum("persona:ImAddresses3")]
		personaImAddresses3,
		// Token: 0x04000B9A RID: 2970
		[XmlEnum("persona:FolderIds")]
		personaFolderIds,
		// Token: 0x04000B9B RID: 2971
		[XmlEnum("persona:Attributions")]
		personaAttributions,
		// Token: 0x04000B9C RID: 2972
		[XmlEnum("persona:DisplayNames")]
		personaDisplayNames,
		// Token: 0x04000B9D RID: 2973
		[XmlEnum("persona:Initials")]
		personaInitials,
		// Token: 0x04000B9E RID: 2974
		[XmlEnum("persona:FileAses")]
		personaFileAses,
		// Token: 0x04000B9F RID: 2975
		[XmlEnum("persona:FileAsIds")]
		personaFileAsIds,
		// Token: 0x04000BA0 RID: 2976
		[XmlEnum("persona:DisplayNamePrefixes")]
		personaDisplayNamePrefixes,
		// Token: 0x04000BA1 RID: 2977
		[XmlEnum("persona:GivenNames")]
		personaGivenNames,
		// Token: 0x04000BA2 RID: 2978
		[XmlEnum("persona:MiddleNames")]
		personaMiddleNames,
		// Token: 0x04000BA3 RID: 2979
		[XmlEnum("persona:Surnames")]
		personaSurnames,
		// Token: 0x04000BA4 RID: 2980
		[XmlEnum("persona:Generations")]
		personaGenerations,
		// Token: 0x04000BA5 RID: 2981
		[XmlEnum("persona:Nicknames")]
		personaNicknames,
		// Token: 0x04000BA6 RID: 2982
		[XmlEnum("persona:YomiCompanyNames")]
		personaYomiCompanyNames,
		// Token: 0x04000BA7 RID: 2983
		[XmlEnum("persona:YomiFirstNames")]
		personaYomiFirstNames,
		// Token: 0x04000BA8 RID: 2984
		[XmlEnum("persona:YomiLastNames")]
		personaYomiLastNames,
		// Token: 0x04000BA9 RID: 2985
		[XmlEnum("persona:BusinessPhoneNumbers")]
		personaBusinessPhoneNumbers,
		// Token: 0x04000BAA RID: 2986
		[XmlEnum("persona:BusinessPhoneNumbers2")]
		personaBusinessPhoneNumbers2,
		// Token: 0x04000BAB RID: 2987
		[XmlEnum("persona:HomePhones")]
		personaHomePhones,
		// Token: 0x04000BAC RID: 2988
		[XmlEnum("persona:HomePhones2")]
		personaHomePhones2,
		// Token: 0x04000BAD RID: 2989
		[XmlEnum("persona:MobilePhones")]
		personaMobilePhones,
		// Token: 0x04000BAE RID: 2990
		[XmlEnum("persona:MobilePhones2")]
		personaMobilePhones2,
		// Token: 0x04000BAF RID: 2991
		[XmlEnum("persona:AssistantPhoneNumbers")]
		personaAssistantPhoneNumbers,
		// Token: 0x04000BB0 RID: 2992
		[XmlEnum("persona:CallbackPhones")]
		personaCallbackPhones,
		// Token: 0x04000BB1 RID: 2993
		[XmlEnum("persona:CarPhones")]
		personaCarPhones,
		// Token: 0x04000BB2 RID: 2994
		[XmlEnum("persona:HomeFaxes")]
		personaHomeFaxes,
		// Token: 0x04000BB3 RID: 2995
		[XmlEnum("persona:OrganizationMainPhones")]
		personaOrganizationMainPhones,
		// Token: 0x04000BB4 RID: 2996
		[XmlEnum("persona:OtherFaxes")]
		personaOtherFaxes,
		// Token: 0x04000BB5 RID: 2997
		[XmlEnum("persona:OtherTelephones")]
		personaOtherTelephones,
		// Token: 0x04000BB6 RID: 2998
		[XmlEnum("persona:OtherPhones2")]
		personaOtherPhones2,
		// Token: 0x04000BB7 RID: 2999
		[XmlEnum("persona:Pagers")]
		personaPagers,
		// Token: 0x04000BB8 RID: 3000
		[XmlEnum("persona:RadioPhones")]
		personaRadioPhones,
		// Token: 0x04000BB9 RID: 3001
		[XmlEnum("persona:TelexNumbers")]
		personaTelexNumbers,
		// Token: 0x04000BBA RID: 3002
		[XmlEnum("persona:WorkFaxes")]
		personaWorkFaxes,
		// Token: 0x04000BBB RID: 3003
		[XmlEnum("persona:Emails1")]
		personaEmails1,
		// Token: 0x04000BBC RID: 3004
		[XmlEnum("persona:Emails2")]
		personaEmails2,
		// Token: 0x04000BBD RID: 3005
		[XmlEnum("persona:Emails3")]
		personaEmails3,
		// Token: 0x04000BBE RID: 3006
		[XmlEnum("persona:BusinessHomePages")]
		personaBusinessHomePages,
		// Token: 0x04000BBF RID: 3007
		[XmlEnum("persona:School")]
		personaSchool,
		// Token: 0x04000BC0 RID: 3008
		[XmlEnum("persona:PersonalHomePages")]
		personaPersonalHomePages,
		// Token: 0x04000BC1 RID: 3009
		[XmlEnum("persona:OfficeLocations")]
		personaOfficeLocations,
		// Token: 0x04000BC2 RID: 3010
		[XmlEnum("persona:BusinessAddresses")]
		personaBusinessAddresses,
		// Token: 0x04000BC3 RID: 3011
		[XmlEnum("persona:HomeAddresses")]
		personaHomeAddresses,
		// Token: 0x04000BC4 RID: 3012
		[XmlEnum("persona:OtherAddresses")]
		personaOtherAddresses,
		// Token: 0x04000BC5 RID: 3013
		[XmlEnum("persona:Titles")]
		personaTitles,
		// Token: 0x04000BC6 RID: 3014
		[XmlEnum("persona:Departments")]
		personaDepartments,
		// Token: 0x04000BC7 RID: 3015
		[XmlEnum("persona:CompanyNames")]
		personaCompanyNames,
		// Token: 0x04000BC8 RID: 3016
		[XmlEnum("persona:Managers")]
		personaManagers,
		// Token: 0x04000BC9 RID: 3017
		[XmlEnum("persona:AssistantNames")]
		personaAssistantNames,
		// Token: 0x04000BCA RID: 3018
		[XmlEnum("persona:Professions")]
		personaProfessions,
		// Token: 0x04000BCB RID: 3019
		[XmlEnum("persona:SpouseNames")]
		personaSpouseNames,
		// Token: 0x04000BCC RID: 3020
		[XmlEnum("persona:Hobbies")]
		personaHobbies,
		// Token: 0x04000BCD RID: 3021
		[XmlEnum("persona:WeddingAnniversaries")]
		personaWeddingAnniversaries,
		// Token: 0x04000BCE RID: 3022
		[XmlEnum("persona:Birthdays")]
		personaBirthdays,
		// Token: 0x04000BCF RID: 3023
		[XmlEnum("persona:Children")]
		personaChildren,
		// Token: 0x04000BD0 RID: 3024
		[XmlEnum("persona:Locations")]
		personaLocations,
		// Token: 0x04000BD1 RID: 3025
		[XmlEnum("persona:ExtendedProperties")]
		personaExtendedProperties,
		// Token: 0x04000BD2 RID: 3026
		[XmlEnum("persona:PostalAddress")]
		personaPostalAddress,
		// Token: 0x04000BD3 RID: 3027
		[XmlEnum("persona:Bodies")]
		personaBodies
	}
}
