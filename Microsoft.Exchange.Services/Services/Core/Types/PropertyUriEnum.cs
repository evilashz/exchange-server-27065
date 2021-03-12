using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000851 RID: 2129
	[XmlType(TypeName = "UnindexedFieldURIType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum PropertyUriEnum
	{
		// Token: 0x040021CF RID: 8655
		[XmlEnum("folder:FolderId")]
		FolderId,
		// Token: 0x040021D0 RID: 8656
		[XmlEnum("folder:ParentFolderId")]
		ParentFolderId,
		// Token: 0x040021D1 RID: 8657
		[XmlEnum("folder:DisplayName")]
		FolderDisplayName,
		// Token: 0x040021D2 RID: 8658
		[XmlEnum("folder:UnreadCount")]
		UnreadCount,
		// Token: 0x040021D3 RID: 8659
		[XmlEnum("folder:TotalCount")]
		TotalCount,
		// Token: 0x040021D4 RID: 8660
		[XmlEnum("folder:ChildFolderCount")]
		ChildFolderCount,
		// Token: 0x040021D5 RID: 8661
		[XmlEnum("folder:FolderClass")]
		FolderClass,
		// Token: 0x040021D6 RID: 8662
		[XmlEnum("folder:SearchParameters")]
		SearchParameters,
		// Token: 0x040021D7 RID: 8663
		[XmlEnum("folder:ManagedFolderInformation")]
		ManagedFolderInformation,
		// Token: 0x040021D8 RID: 8664
		[XmlEnum("folder:PermissionSet")]
		PermissionSet,
		// Token: 0x040021D9 RID: 8665
		[XmlEnum("folder:EffectiveRights")]
		FolderEffectiveRights,
		// Token: 0x040021DA RID: 8666
		[XmlEnum("folder:SharingEffectiveRights")]
		FolderSharingEffectiveRights,
		// Token: 0x040021DB RID: 8667
		[XmlEnum("folder:DistinguishedFolderId")]
		DistinguishedFolderId,
		// Token: 0x040021DC RID: 8668
		[XmlEnum("folder:PolicyTag")]
		FolderPolicyTag,
		// Token: 0x040021DD RID: 8669
		[XmlEnum("folder:ArchiveTag")]
		FolderArchiveTag,
		// Token: 0x040021DE RID: 8670
		[XmlEnum("folder:UnClutteredViewFolderEntryId")]
		UnClutteredViewFolderEntryId,
		// Token: 0x040021DF RID: 8671
		[XmlEnum("folder:ClutteredViewFolderEntryId")]
		ClutteredViewFolderEntryId,
		// Token: 0x040021E0 RID: 8672
		[XmlEnum("folder:ClutterCount")]
		ClutterCount,
		// Token: 0x040021E1 RID: 8673
		[XmlEnum("folder:UnreadClutterCount")]
		UnreadClutterCount,
		// Token: 0x040021E2 RID: 8674
		[XmlEnum("folder:ReplicaList")]
		ReplicaList,
		// Token: 0x040021E3 RID: 8675
		[XmlEnum("item:ItemId")]
		ItemId,
		// Token: 0x040021E4 RID: 8676
		[XmlEnum("item:ParentFolderId")]
		ItemParentId,
		// Token: 0x040021E5 RID: 8677
		[XmlEnum("item:ItemClass")]
		ItemClass,
		// Token: 0x040021E6 RID: 8678
		[XmlEnum("item:MimeContent")]
		MimeContent,
		// Token: 0x040021E7 RID: 8679
		[XmlEnum("item:Attachments")]
		Attachments,
		// Token: 0x040021E8 RID: 8680
		[XmlEnum("item:Subject")]
		Subject,
		// Token: 0x040021E9 RID: 8681
		[XmlEnum("item:DateTimeReceived")]
		DateTimeReceived,
		// Token: 0x040021EA RID: 8682
		[XmlEnum("item:Size")]
		Size,
		// Token: 0x040021EB RID: 8683
		[XmlEnum("item:Categories")]
		Categories,
		// Token: 0x040021EC RID: 8684
		[XmlEnum("item:HasAttachments")]
		HasAttachments,
		// Token: 0x040021ED RID: 8685
		[XmlEnum("item:Importance")]
		Importance,
		// Token: 0x040021EE RID: 8686
		[XmlEnum("item:InReplyTo")]
		InReplyTo,
		// Token: 0x040021EF RID: 8687
		[XmlEnum("item:InternetMessageHeaders")]
		InternetMessageHeaders,
		// Token: 0x040021F0 RID: 8688
		[XmlEnum("item:IsAssociated")]
		IsAssociated,
		// Token: 0x040021F1 RID: 8689
		[XmlEnum("item:IsDraft")]
		IsDraft,
		// Token: 0x040021F2 RID: 8690
		[XmlEnum("item:IsFromMe")]
		IsFromMe,
		// Token: 0x040021F3 RID: 8691
		[XmlEnum("item:IsResend")]
		IsResend,
		// Token: 0x040021F4 RID: 8692
		[XmlEnum("item:IsSubmitted")]
		IsSubmitted,
		// Token: 0x040021F5 RID: 8693
		[XmlEnum("item:IsUnmodified")]
		IsUnmodified,
		// Token: 0x040021F6 RID: 8694
		[XmlEnum("item:DateTimeSent")]
		DateTimeSent,
		// Token: 0x040021F7 RID: 8695
		[XmlEnum("item:DateTimeCreated")]
		DateTimeCreated,
		// Token: 0x040021F8 RID: 8696
		[XmlEnum("item:Body")]
		Body,
		// Token: 0x040021F9 RID: 8697
		[XmlEnum("item:ResponseObjects")]
		ResponseObjects,
		// Token: 0x040021FA RID: 8698
		[XmlEnum("item:Sensitivity")]
		Sensitivity,
		// Token: 0x040021FB RID: 8699
		[XmlEnum("item:ReminderDueBy")]
		ReminderDueBy,
		// Token: 0x040021FC RID: 8700
		[XmlEnum("item:ReminderIsSet")]
		ReminderIsSet,
		// Token: 0x040021FD RID: 8701
		[XmlEnum("item:ReminderNextTime")]
		ReminderNextTime,
		// Token: 0x040021FE RID: 8702
		[XmlEnum("item:ReminderMinutesBeforeStart")]
		ReminderMinutesBeforeStart,
		// Token: 0x040021FF RID: 8703
		[XmlEnum("item:DisplayTo")]
		DisplayTo,
		// Token: 0x04002200 RID: 8704
		[XmlEnum("item:DisplayCc")]
		DisplayCc,
		// Token: 0x04002201 RID: 8705
		[XmlEnum("item:Culture")]
		Culture,
		// Token: 0x04002202 RID: 8706
		[XmlEnum("item:EffectiveRights")]
		ItemEffectiveRights,
		// Token: 0x04002203 RID: 8707
		[XmlEnum("item:LastModifiedName")]
		LastModifiedName,
		// Token: 0x04002204 RID: 8708
		[XmlEnum("item:LastModifiedTime")]
		ItemLastModifiedTime,
		// Token: 0x04002205 RID: 8709
		[XmlEnum("item:WebClientReadFormQueryString")]
		WebClientReadFormQueryString,
		// Token: 0x04002206 RID: 8710
		[XmlEnum("item:WebClientEditFormQueryString")]
		WebClientEditFormQueryString,
		// Token: 0x04002207 RID: 8711
		[XmlEnum("item:ConversationId")]
		ConversationId,
		// Token: 0x04002208 RID: 8712
		[XmlEnum("item:UniqueBody")]
		UniqueBody,
		// Token: 0x04002209 RID: 8713
		[XmlEnum("item:PredictedActionReasons")]
		PredictedActionReasons,
		// Token: 0x0400220A RID: 8714
		[XmlEnum("item:IsClutter")]
		IsClutter,
		// Token: 0x0400220B RID: 8715
		[XmlEnum("item:Flag")]
		Flag,
		// Token: 0x0400220C RID: 8716
		[XmlEnum("item:StoreEntryId")]
		StoreEntryId,
		// Token: 0x0400220D RID: 8717
		[XmlEnum("item:InstanceKey")]
		InstanceKey,
		// Token: 0x0400220E RID: 8718
		[XmlEnum("item:NormalizedBody")]
		NormalizedBody,
		// Token: 0x0400220F RID: 8719
		[XmlEnum("item:EntityExtractionResult")]
		EntityExtractionResult,
		// Token: 0x04002210 RID: 8720
		[XmlEnum("item:PolicyTag")]
		ItemPolicyTag,
		// Token: 0x04002211 RID: 8721
		[XmlEnum("item:ArchiveTag")]
		ItemArchiveTag,
		// Token: 0x04002212 RID: 8722
		[XmlEnum("item:RetentionDate")]
		ItemRetentionDate,
		// Token: 0x04002213 RID: 8723
		[XmlEnum("item:Preview")]
		Preview,
		// Token: 0x04002214 RID: 8724
		[XmlEnum("item:RightsManagementLicenseData")]
		RightsManagementLicenseData,
		// Token: 0x04002215 RID: 8725
		[XmlEnum("item:BlockStatus")]
		BlockStatus,
		// Token: 0x04002216 RID: 8726
		[XmlEnum("item:HasBlockedImages")]
		HasBlockedImages,
		// Token: 0x04002217 RID: 8727
		[XmlEnum("item:TextBody")]
		TextBody,
		// Token: 0x04002218 RID: 8728
		[XmlEnum("item:IconIndex")]
		IconIndex,
		// Token: 0x04002219 RID: 8729
		[XmlEnum("item:ModernReminders")]
		ModernReminders,
		// Token: 0x0400221A RID: 8730
		[XmlEnum("item:WorkingSetSourcePartition")]
		WorkingSetSourcePartition,
		// Token: 0x0400221B RID: 8731
		[XmlEnum("item:ReceivedOrRenewTime")]
		ReceivedOrRenewTime,
		// Token: 0x0400221C RID: 8732
		[XmlEnum("item:RenewTime")]
		RenewTime,
		// Token: 0x0400221D RID: 8733
		[XmlEnum("item:MimeContentUTF8")]
		MimeContentUTF8,
		// Token: 0x0400221E RID: 8734
		[XmlEnum("message:ConversationIndex")]
		ConversationIndex,
		// Token: 0x0400221F RID: 8735
		[XmlEnum("message:ConversationTopic")]
		ConversationTopic,
		// Token: 0x04002220 RID: 8736
		[XmlEnum("message:InternetMessageId")]
		InternetMessageId,
		// Token: 0x04002221 RID: 8737
		[XmlEnum("message:IsRead")]
		IsRead,
		// Token: 0x04002222 RID: 8738
		[XmlEnum("message:IsResponseRequested")]
		IsResponseRequested,
		// Token: 0x04002223 RID: 8739
		[XmlEnum("message:IsReadReceiptRequested")]
		IsReadReceiptRequested,
		// Token: 0x04002224 RID: 8740
		[XmlEnum("message:IsDeliveryReceiptRequested")]
		IsDeliveryReceiptRequested,
		// Token: 0x04002225 RID: 8741
		[XmlEnum("message:ReceivedBy")]
		ReceivedBy,
		// Token: 0x04002226 RID: 8742
		[XmlEnum("message:ReceivedRepresenting")]
		ReceivedRepresenting,
		// Token: 0x04002227 RID: 8743
		[XmlEnum("message:References")]
		References,
		// Token: 0x04002228 RID: 8744
		[XmlEnum("message:ReplyTo")]
		ReplyTo,
		// Token: 0x04002229 RID: 8745
		[XmlEnum("message:From")]
		From,
		// Token: 0x0400222A RID: 8746
		[XmlEnum("message:Sender")]
		Sender,
		// Token: 0x0400222B RID: 8747
		[XmlEnum("message:ToRecipients")]
		ToRecipients,
		// Token: 0x0400222C RID: 8748
		[XmlEnum("message:CcRecipients")]
		CcRecipients,
		// Token: 0x0400222D RID: 8749
		[XmlEnum("message:BccRecipients")]
		BccRecipients,
		// Token: 0x0400222E RID: 8750
		[XmlEnum("message:ApprovalRequestData")]
		ApprovalRequestData,
		// Token: 0x0400222F RID: 8751
		[XmlEnum("message:VotingInformation")]
		VotingInformation,
		// Token: 0x04002230 RID: 8752
		[XmlEnum("message:ReminderMessageData")]
		ReminderMessageData,
		// Token: 0x04002231 RID: 8753
		[XmlEnum("message:MailboxGuid")]
		MailboxGuid,
		// Token: 0x04002232 RID: 8754
		[XmlEnum("message:RecipientCounts")]
		RecipientCounts,
		// Token: 0x04002233 RID: 8755
		[XmlEnum("message:IsGroupEscalationMessage")]
		IsGroupEscalationMessage,
		// Token: 0x04002234 RID: 8756
		[XmlEnum("meeting:AssociatedCalendarItemId")]
		AssociatedCalendarItemId,
		// Token: 0x04002235 RID: 8757
		[XmlEnum("meeting:IsDelegated")]
		IsDelegated,
		// Token: 0x04002236 RID: 8758
		[XmlEnum("meeting:IsOutOfDate")]
		IsOutOfDate,
		// Token: 0x04002237 RID: 8759
		[XmlEnum("meeting:HasBeenProcessed")]
		HasBeenProcessed,
		// Token: 0x04002238 RID: 8760
		[XmlEnum("meeting:ResponseType")]
		ResponseType,
		// Token: 0x04002239 RID: 8761
		[XmlEnum("meeting:ProposedStart")]
		ProposedStart,
		// Token: 0x0400223A RID: 8762
		[XmlEnum("meeting:ProposedEnd")]
		ProposedEnd,
		// Token: 0x0400223B RID: 8763
		[XmlEnum("meetingRequest:MeetingRequestType")]
		MeetingRequestType,
		// Token: 0x0400223C RID: 8764
		[XmlEnum("meetingRequest:IntendedFreeBusyStatus")]
		IntendedFreeBusyStatus,
		// Token: 0x0400223D RID: 8765
		[XmlEnum("meetingRequest:ChangeHighlights")]
		ChangeHighlights,
		// Token: 0x0400223E RID: 8766
		[XmlEnum("calendar:Start")]
		Start,
		// Token: 0x0400223F RID: 8767
		[XmlEnum("calendar:End")]
		End,
		// Token: 0x04002240 RID: 8768
		[XmlEnum("calendar:StartWallClock")]
		StartWallClock,
		// Token: 0x04002241 RID: 8769
		[XmlEnum("calendar:EndWallClock")]
		EndWallClock,
		// Token: 0x04002242 RID: 8770
		[XmlEnum("calendar:EnhancedLocation")]
		EnhancedLocation,
		// Token: 0x04002243 RID: 8771
		[XmlEnum("calendar:OriginalStart")]
		OriginalStart,
		// Token: 0x04002244 RID: 8772
		[XmlEnum("calendar:IsAllDayEvent")]
		IsAllDayEvent,
		// Token: 0x04002245 RID: 8773
		[XmlEnum("calendar:LegacyFreeBusyStatus")]
		LegacyFreeBusyStatus,
		// Token: 0x04002246 RID: 8774
		[XmlEnum("calendar:Location")]
		Location,
		// Token: 0x04002247 RID: 8775
		[XmlEnum("calendar:When")]
		When,
		// Token: 0x04002248 RID: 8776
		[XmlEnum("calendar:IsOrganizer")]
		IsOrganizer,
		// Token: 0x04002249 RID: 8777
		[XmlEnum("calendar:IsMeeting")]
		IsMeeting,
		// Token: 0x0400224A RID: 8778
		[XmlEnum("calendar:IsCancelled")]
		IsCancelled,
		// Token: 0x0400224B RID: 8779
		[XmlEnum("calendar:IsSeriesCancelled")]
		IsSeriesCancelled,
		// Token: 0x0400224C RID: 8780
		[XmlEnum("calendar:IsRecurring")]
		IsRecurring,
		// Token: 0x0400224D RID: 8781
		[XmlEnum("calendar:MeetingRequestWasSent")]
		MeetingRequestWasSent,
		// Token: 0x0400224E RID: 8782
		[XmlEnum("calendar:IsResponseRequested")]
		CalendarIsResponseRequested,
		// Token: 0x0400224F RID: 8783
		[XmlEnum("calendar:CalendarItemType")]
		CalendarItemType,
		// Token: 0x04002250 RID: 8784
		[XmlEnum("calendar:MyResponseType")]
		MyResponseType,
		// Token: 0x04002251 RID: 8785
		[XmlEnum("calendar:Organizer")]
		Organizer,
		// Token: 0x04002252 RID: 8786
		[XmlEnum("calendar:RequiredAttendees")]
		RequiredAttendees,
		// Token: 0x04002253 RID: 8787
		[XmlEnum("calendar:OptionalAttendees")]
		OptionalAttendees,
		// Token: 0x04002254 RID: 8788
		[XmlEnum("calendar:Resources")]
		Resources,
		// Token: 0x04002255 RID: 8789
		[XmlEnum("calendar:AttendeeCounts")]
		AttendeeCounts,
		// Token: 0x04002256 RID: 8790
		[XmlEnum("calendar:ConflictingMeetingCount")]
		ConflictingMeetingCount,
		// Token: 0x04002257 RID: 8791
		[XmlEnum("calendar:AdjacentMeetingCount")]
		AdjacentMeetingCount,
		// Token: 0x04002258 RID: 8792
		[XmlEnum("calendar:ConflictingMeetings")]
		ConflictingMeetings,
		// Token: 0x04002259 RID: 8793
		[XmlEnum("calendar:AdjacentMeetings")]
		AdjacentMeetings,
		// Token: 0x0400225A RID: 8794
		[XmlEnum("calendar:Duration")]
		Duration,
		// Token: 0x0400225B RID: 8795
		[XmlEnum("calendar:TimeZone")]
		TimeZone,
		// Token: 0x0400225C RID: 8796
		[XmlEnum("calendar:AppointmentReplyTime")]
		AppointmentReplyTime,
		// Token: 0x0400225D RID: 8797
		[XmlEnum("calendar:AppointmentReplyName")]
		AppointmentReplyName,
		// Token: 0x0400225E RID: 8798
		[XmlEnum("calendar:AppointmentSequenceNumber")]
		AppointmentSequenceNumber,
		// Token: 0x0400225F RID: 8799
		[XmlEnum("calendar:AppointmentState")]
		AppointmentState,
		// Token: 0x04002260 RID: 8800
		[XmlEnum("calendar:Recurrence")]
		Recurrence,
		// Token: 0x04002261 RID: 8801
		[XmlEnum("calendar:FirstOccurrence")]
		FirstOccurrence,
		// Token: 0x04002262 RID: 8802
		[XmlEnum("calendar:LastOccurrence")]
		LastOccurrence,
		// Token: 0x04002263 RID: 8803
		[XmlEnum("calendar:ModifiedOccurrences")]
		ModifiedOccurrences,
		// Token: 0x04002264 RID: 8804
		[XmlEnum("calendar:DeletedOccurrences")]
		DeletedOccurrences,
		// Token: 0x04002265 RID: 8805
		[XmlEnum("calendar:MeetingTimeZone")]
		MeetingTimeZone,
		// Token: 0x04002266 RID: 8806
		[XmlEnum("calendar:StartTimeZone")]
		StartTimeZone,
		// Token: 0x04002267 RID: 8807
		[XmlEnum("calendar:EndTimeZone")]
		EndTimeZone,
		// Token: 0x04002268 RID: 8808
		[XmlEnum("calendar:StartTimeZoneId")]
		StartTimeZoneId,
		// Token: 0x04002269 RID: 8809
		[XmlEnum("calendar:EndTimeZoneId")]
		EndTimeZoneId,
		// Token: 0x0400226A RID: 8810
		[XmlEnum("calendar:ConferenceType")]
		ConferenceType,
		// Token: 0x0400226B RID: 8811
		[XmlEnum("calendar:AllowNewTimeProposal")]
		AllowNewTimeProposal,
		// Token: 0x0400226C RID: 8812
		[XmlEnum("calendar:IsOnlineMeeting")]
		IsOnlineMeeting,
		// Token: 0x0400226D RID: 8813
		[XmlEnum("calendar:MeetingWorkspaceUrl")]
		MeetingWorkspaceUrl,
		// Token: 0x0400226E RID: 8814
		[XmlEnum("calendar:NetShowUrl")]
		NetShowUrl,
		// Token: 0x0400226F RID: 8815
		[XmlEnum("calendar:UID")]
		UID,
		// Token: 0x04002270 RID: 8816
		[XmlEnum("calendar:RecurrenceId")]
		RecurrenceId,
		// Token: 0x04002271 RID: 8817
		[XmlEnum("calendar:DateTimeStamp")]
		DateTimeStamp,
		// Token: 0x04002272 RID: 8818
		[XmlEnum("calendar:JoinOnlineMeetingUrl")]
		JoinOnlineMeetingUrl,
		// Token: 0x04002273 RID: 8819
		[XmlEnum("calendar:OnlineMeetingSettings")]
		OnlineMeetingSettings,
		// Token: 0x04002274 RID: 8820
		[XmlEnum("calendar:InboxReminders")]
		InboxReminders,
		// Token: 0x04002275 RID: 8821
		[XmlEnum("task:ActualWork")]
		TaskActualWork,
		// Token: 0x04002276 RID: 8822
		[XmlEnum("task:AssignedTime")]
		TaskAssignedTime,
		// Token: 0x04002277 RID: 8823
		[XmlEnum("task:BillingInformation")]
		TaskBillingInformation,
		// Token: 0x04002278 RID: 8824
		[XmlEnum("task:ChangeCount")]
		TaskChangeCount,
		// Token: 0x04002279 RID: 8825
		[XmlEnum("task:Companies")]
		TaskCompanies,
		// Token: 0x0400227A RID: 8826
		[XmlEnum("task:CompleteDate")]
		TaskCompleteDate,
		// Token: 0x0400227B RID: 8827
		[XmlEnum("task:Contacts")]
		TaskContacts,
		// Token: 0x0400227C RID: 8828
		[XmlEnum("task:DelegationState")]
		TaskDelegationState,
		// Token: 0x0400227D RID: 8829
		[XmlEnum("task:Delegator")]
		TaskDelegator,
		// Token: 0x0400227E RID: 8830
		[XmlEnum("task:DueDate")]
		TaskDueDate,
		// Token: 0x0400227F RID: 8831
		[XmlEnum("task:IsAssignmentEditable")]
		TaskIsAssignmentEditable,
		// Token: 0x04002280 RID: 8832
		[XmlEnum("task:IsComplete")]
		TaskIsComplete,
		// Token: 0x04002281 RID: 8833
		[XmlEnum("task:IsRecurring")]
		TaskIsTaskRecurring,
		// Token: 0x04002282 RID: 8834
		[XmlEnum("task:IsTeamTask")]
		TaskIsTeamTask,
		// Token: 0x04002283 RID: 8835
		[XmlEnum("task:Mileage")]
		TaskMileage,
		// Token: 0x04002284 RID: 8836
		[XmlEnum("task:Owner")]
		TaskOwner,
		// Token: 0x04002285 RID: 8837
		[XmlEnum("task:PercentComplete")]
		TaskPercentComplete,
		// Token: 0x04002286 RID: 8838
		[XmlEnum("task:Recurrence")]
		TaskRecurrence,
		// Token: 0x04002287 RID: 8839
		[XmlEnum("task:StartDate")]
		TaskStartDate,
		// Token: 0x04002288 RID: 8840
		[XmlEnum("task:Status")]
		TaskStatus,
		// Token: 0x04002289 RID: 8841
		[XmlEnum("task:StatusDescription")]
		TaskStatusDescription,
		// Token: 0x0400228A RID: 8842
		[XmlEnum("task:TotalWork")]
		TaskTotalWork,
		// Token: 0x0400228B RID: 8843
		[XmlEnum("task:DoItTime")]
		TaskDoItTime,
		// Token: 0x0400228C RID: 8844
		[XmlEnum("contacts:Alias")]
		Alias,
		// Token: 0x0400228D RID: 8845
		[XmlEnum("contacts:AssistantName")]
		AssistantName,
		// Token: 0x0400228E RID: 8846
		[XmlEnum("contacts:Birthday")]
		Birthday,
		// Token: 0x0400228F RID: 8847
		[XmlEnum("contacts:BirthdayLocal")]
		BirthdayLocal,
		// Token: 0x04002290 RID: 8848
		[XmlEnum("contacts:BusinessHomePage")]
		BusinessHomePage,
		// Token: 0x04002291 RID: 8849
		[XmlEnum("contacts:Children")]
		Children,
		// Token: 0x04002292 RID: 8850
		[XmlEnum("contacts:Companies")]
		Companies,
		// Token: 0x04002293 RID: 8851
		[XmlEnum("contacts:CompanyName")]
		CompanyName,
		// Token: 0x04002294 RID: 8852
		[XmlEnum("contacts:CompleteName")]
		CompleteName,
		// Token: 0x04002295 RID: 8853
		[XmlEnum("contacts:ContactSource")]
		ContactSource,
		// Token: 0x04002296 RID: 8854
		[XmlEnum("contacts:Culture")]
		ContactCulture,
		// Token: 0x04002297 RID: 8855
		[XmlEnum("contacts:Department")]
		Department,
		// Token: 0x04002298 RID: 8856
		[XmlEnum("contacts:DirectoryId")]
		DirectoryId,
		// Token: 0x04002299 RID: 8857
		[XmlEnum("contacts:DirectReports")]
		DirectReports,
		// Token: 0x0400229A RID: 8858
		[XmlEnum("contacts:DisplayName")]
		DisplayName,
		// Token: 0x0400229B RID: 8859
		[XmlEnum("contacts:EmailAddresses")]
		EmailAddresses,
		// Token: 0x0400229C RID: 8860
		[XmlEnum("contacts:FileAs")]
		FileAs,
		// Token: 0x0400229D RID: 8861
		[XmlEnum("contacts:FileAsMapping")]
		FileAsMapping,
		// Token: 0x0400229E RID: 8862
		[XmlEnum("contacts:Generation")]
		Generation,
		// Token: 0x0400229F RID: 8863
		[XmlEnum("contacts:GivenName")]
		GivenName,
		// Token: 0x040022A0 RID: 8864
		[XmlEnum("contacts:ImAddresses")]
		ImAddresses,
		// Token: 0x040022A1 RID: 8865
		[XmlEnum("contacts:Initials")]
		Initials,
		// Token: 0x040022A2 RID: 8866
		[XmlEnum("contacts:JobTitle")]
		JobTitle,
		// Token: 0x040022A3 RID: 8867
		[XmlEnum("contacts:Manager")]
		Manager,
		// Token: 0x040022A4 RID: 8868
		[XmlEnum("contacts:ManagerMailbox")]
		ManagerMailbox,
		// Token: 0x040022A5 RID: 8869
		[XmlEnum("contacts:MiddleName")]
		MiddleName,
		// Token: 0x040022A6 RID: 8870
		[XmlEnum("contacts:Mileage")]
		Mileage,
		// Token: 0x040022A7 RID: 8871
		[XmlEnum("contacts:MSExchangeCertificate")]
		MSExchangeCertificate,
		// Token: 0x040022A8 RID: 8872
		[XmlEnum("contacts:Nickname")]
		Nickname,
		// Token: 0x040022A9 RID: 8873
		[XmlEnum("contacts:Notes")]
		Notes,
		// Token: 0x040022AA RID: 8874
		[XmlEnum("contacts:OfficeLocation")]
		OfficeLocation,
		// Token: 0x040022AB RID: 8875
		[XmlEnum("contacts:PhoneNumbers")]
		PhoneNumbers,
		// Token: 0x040022AC RID: 8876
		[XmlEnum("contacts:PhoneticFullName")]
		PhoneticFullName,
		// Token: 0x040022AD RID: 8877
		[XmlEnum("contacts:PhoneticFirstName")]
		PhoneticFirstName,
		// Token: 0x040022AE RID: 8878
		[XmlEnum("contacts:PhoneticLastName")]
		PhoneticLastName,
		// Token: 0x040022AF RID: 8879
		[XmlEnum("contacts:Photo")]
		Photo,
		// Token: 0x040022B0 RID: 8880
		[XmlEnum("contacts:PhysicalAddresses")]
		PhysicalAddresses,
		// Token: 0x040022B1 RID: 8881
		[XmlEnum("contacts:PostalAddressIndex")]
		PostalAddressIndex,
		// Token: 0x040022B2 RID: 8882
		[XmlEnum("contacts:Profession")]
		Profession,
		// Token: 0x040022B3 RID: 8883
		[XmlEnum("contacts:SpouseName")]
		SpouseName,
		// Token: 0x040022B4 RID: 8884
		[XmlEnum("contacts:Surname")]
		Surname,
		// Token: 0x040022B5 RID: 8885
		[XmlEnum("contacts:WeddingAnniversary")]
		WeddingAnniversary,
		// Token: 0x040022B6 RID: 8886
		[XmlEnum("contacts:WeddingAnniversaryLocal")]
		WeddingAnniversaryLocal,
		// Token: 0x040022B7 RID: 8887
		[XmlEnum("contacts:UserSMIMECertificate")]
		UserSMIMECertificate,
		// Token: 0x040022B8 RID: 8888
		[XmlEnum("contacts:HasPicture")]
		HasPicture,
		// Token: 0x040022B9 RID: 8889
		[XmlEnum("conversation:ConversationId")]
		ConversationGuidId,
		// Token: 0x040022BA RID: 8890
		[XmlEnum("conversation:ConversationTopic")]
		Topic,
		// Token: 0x040022BB RID: 8891
		[XmlEnum("conversation:UniqueRecipients")]
		ConversationUniqueRecipients,
		// Token: 0x040022BC RID: 8892
		[XmlEnum("conversation:GlobalUniqueRecipients")]
		ConversationGlobalUniqueRecipients,
		// Token: 0x040022BD RID: 8893
		[XmlEnum("conversation:UniqueUnreadSenders")]
		ConversationUniqueUnreadSenders,
		// Token: 0x040022BE RID: 8894
		[XmlEnum("conversation:GlobalUniqueUnreadSenders")]
		ConversationGlobalUniqueUnreadSenders,
		// Token: 0x040022BF RID: 8895
		[XmlEnum("conversation:UniqueSenders")]
		ConversationUniqueSenders,
		// Token: 0x040022C0 RID: 8896
		[XmlEnum("conversation:GlobalUniqueSenders")]
		ConversationGlobalUniqueSenders,
		// Token: 0x040022C1 RID: 8897
		[XmlEnum("conversation:LastDeliveryTime")]
		ConversationLastDeliveryTime,
		// Token: 0x040022C2 RID: 8898
		[XmlEnum("conversation:GlobalLastDeliveryTime")]
		ConversationGlobalLastDeliveryTime,
		// Token: 0x040022C3 RID: 8899
		[XmlEnum("conversation:Categories")]
		ConversationCategories,
		// Token: 0x040022C4 RID: 8900
		[XmlEnum("conversation:GlobalCategories")]
		ConversationGlobalCategories,
		// Token: 0x040022C5 RID: 8901
		[XmlEnum("conversation:FlagStatus")]
		ConversationFlagStatus,
		// Token: 0x040022C6 RID: 8902
		[XmlEnum("conversation:GlobalFlagStatus")]
		ConversationGlobalFlagStatus,
		// Token: 0x040022C7 RID: 8903
		[XmlEnum("conversation:HasAttachments")]
		ConversationHasAttachments,
		// Token: 0x040022C8 RID: 8904
		[XmlEnum("conversation:GlobalHasAttachments")]
		ConversationGlobalHasAttachments,
		// Token: 0x040022C9 RID: 8905
		[XmlEnum("conversation:HasIrm")]
		ConversationHasIrm,
		// Token: 0x040022CA RID: 8906
		[XmlEnum("conversation:GlobalHasIrm")]
		ConversationGlobalHasIrm,
		// Token: 0x040022CB RID: 8907
		[XmlEnum("conversation:MessageCount")]
		ConversationMessageCount,
		// Token: 0x040022CC RID: 8908
		[XmlEnum("conversation:GlobalMessageCount")]
		ConversationGlobalMessageCount,
		// Token: 0x040022CD RID: 8909
		[XmlEnum("conversation:UnreadCount")]
		ConversationUnreadCount,
		// Token: 0x040022CE RID: 8910
		[XmlEnum("conversation:GlobalUnreadCount")]
		ConversationGlobalUnreadCount,
		// Token: 0x040022CF RID: 8911
		[XmlEnum("conversation:Size")]
		ConversationSize,
		// Token: 0x040022D0 RID: 8912
		[XmlEnum("conversation:GlobalSize")]
		ConversationGlobalSize,
		// Token: 0x040022D1 RID: 8913
		[XmlEnum("conversation:ItemClasses")]
		ConversationItemClasses,
		// Token: 0x040022D2 RID: 8914
		[XmlEnum("conversation:GlobalItemClasses")]
		ConversationGlobalItemClasses,
		// Token: 0x040022D3 RID: 8915
		[XmlEnum("conversation:Importance")]
		ConversationImportance,
		// Token: 0x040022D4 RID: 8916
		[XmlEnum("conversation:GlobalImportance")]
		ConversationGlobalImportance,
		// Token: 0x040022D5 RID: 8917
		[XmlEnum("conversation:ItemIds")]
		ConversationItemIds,
		// Token: 0x040022D6 RID: 8918
		[XmlEnum("conversation:GlobalItemIds")]
		ConversationGlobalItemIds,
		// Token: 0x040022D7 RID: 8919
		[XmlEnum("conversation:LastModifiedTime")]
		ConversationLastModifiedTime,
		// Token: 0x040022D8 RID: 8920
		[XmlEnum("conversation:InstanceKey")]
		ConversationInstanceKey,
		// Token: 0x040022D9 RID: 8921
		[XmlEnum("conversation:Preview")]
		ConversationPreview,
		// Token: 0x040022DA RID: 8922
		[XmlEnum("conversation:IconIndex")]
		ConversationIconIndex,
		// Token: 0x040022DB RID: 8923
		[XmlEnum("conversation:GlobalIconIndex")]
		ConversationGlobalIconIndex,
		// Token: 0x040022DC RID: 8924
		[XmlEnum("conversation:DraftItemIds")]
		ConversationDraftItemIds,
		// Token: 0x040022DD RID: 8925
		[XmlEnum("conversation:HasClutter")]
		ConversationHasClutter,
		// Token: 0x040022DE RID: 8926
		[XmlEnum("conversation:InitialPost")]
		ConversationInitialPost,
		// Token: 0x040022DF RID: 8927
		[XmlEnum("conversation:RecentReplys")]
		ConversationRecentReplys,
		// Token: 0x040022E0 RID: 8928
		[XmlEnum("conversation:WorkingSetSourcePartition")]
		ConversationWorkingSetSourcePartition,
		// Token: 0x040022E1 RID: 8929
		[XmlEnum("conversation:FamilyId")]
		FamilyId,
		// Token: 0x040022E2 RID: 8930
		[XmlEnum("distributionlist:Members")]
		Members,
		// Token: 0x040022E3 RID: 8931
		[XmlEnum("postitem:PostedTime")]
		PostedTime,
		// Token: 0x040022E4 RID: 8932
		[XmlEnum("persona:GivenName")]
		PersonaGivenName,
		// Token: 0x040022E5 RID: 8933
		[XmlEnum("persona:DisplayName")]
		PersonaDisplayName,
		// Token: 0x040022E6 RID: 8934
		[XmlEnum("persona:Surname")]
		PersonaSurname,
		// Token: 0x040022E7 RID: 8935
		[XmlEnum("persona:CompanyName")]
		PersonaCompanyName,
		// Token: 0x040022E8 RID: 8936
		[XmlEnum("persona:CompanyNameSortKey")]
		PersonaCompanyNameSortKey,
		// Token: 0x040022E9 RID: 8937
		[XmlEnum("persona:FileAs")]
		PersonaFileAs,
		// Token: 0x040022EA RID: 8938
		[XmlEnum("persona:CreationTime")]
		PersonaCreationTime,
		// Token: 0x040022EB RID: 8939
		[XmlEnum("persona:IsFavorite")]
		PersonaIsFavorite,
		// Token: 0x040022EC RID: 8940
		[XmlEnum("persona:WorkCity")]
		PersonaWorkCity,
		// Token: 0x040022ED RID: 8941
		[XmlEnum("persona:WorkCitySortKey")]
		PersonaWorkCitySortKey,
		// Token: 0x040022EE RID: 8942
		[XmlEnum("persona:DisplayNameFirstLast")]
		PersonaDisplayNameFirstLast,
		// Token: 0x040022EF RID: 8943
		[XmlEnum("persona:DisplayNameFirstLastSortKey")]
		PersonaDisplayNameFirstLastSortKey,
		// Token: 0x040022F0 RID: 8944
		[XmlEnum("persona:DisplayNameFirstLastHeader")]
		PersonaDisplayNameFirstLastHeader,
		// Token: 0x040022F1 RID: 8945
		[XmlEnum("persona:DisplayNameLastFirst")]
		PersonaDisplayNameLastFirst,
		// Token: 0x040022F2 RID: 8946
		[XmlEnum("persona:DisplayNameLastFirstHeader")]
		PersonaDisplayNameLastFirstHeader,
		// Token: 0x040022F3 RID: 8947
		[XmlEnum("persona:DisplayNameLastFirstSortKey")]
		PersonaDisplayNameLastFirstSortKey,
		// Token: 0x040022F4 RID: 8948
		[XmlEnum("persona:PersonaId")]
		PersonaId,
		// Token: 0x040022F5 RID: 8949
		[XmlEnum("persona:PersonaType")]
		PersonaType,
		// Token: 0x040022F6 RID: 8950
		[XmlEnum("persona:PersonaObjectStatus")]
		PersonaObjectStatus,
		// Token: 0x040022F7 RID: 8951
		[XmlEnum("persona:FileAsId")]
		PersonaFileAsId,
		// Token: 0x040022F8 RID: 8952
		[XmlEnum("persona:DisplayNamePrefix")]
		PersonaDisplayNamePrefix,
		// Token: 0x040022F9 RID: 8953
		[XmlEnum("persona:YomiCompanyName")]
		PersonaYomiCompanyName,
		// Token: 0x040022FA RID: 8954
		[XmlEnum("persona:YomiFirstName")]
		PersonaYomiFirstName,
		// Token: 0x040022FB RID: 8955
		[XmlEnum("persona:YomiLastName")]
		PersonaYomiLastName,
		// Token: 0x040022FC RID: 8956
		[XmlEnum("persona:Title")]
		PersonaTitle,
		// Token: 0x040022FD RID: 8957
		[XmlEnum("persona:EmailAddress")]
		PersonaEmailAddress,
		// Token: 0x040022FE RID: 8958
		[XmlEnum("persona:EmailAddresses")]
		PersonaEmailAddresses,
		// Token: 0x040022FF RID: 8959
		[XmlEnum("persona:PhoneNumber")]
		PersonaPhoneNumber,
		// Token: 0x04002300 RID: 8960
		[XmlEnum("persona:ImAddress")]
		PersonaImAddress,
		// Token: 0x04002301 RID: 8961
		[XmlEnum("persona:ImAddresses")]
		PersonaImAddresses,
		// Token: 0x04002302 RID: 8962
		[XmlEnum("persona:ImAddresses2")]
		PersonaImAddresses2,
		// Token: 0x04002303 RID: 8963
		[XmlEnum("persona:ImAddresses3")]
		PersonaImAddresses3,
		// Token: 0x04002304 RID: 8964
		[XmlEnum("persona:HomeCity")]
		PersonaHomeCity,
		// Token: 0x04002305 RID: 8965
		[XmlEnum("persona:HomeCitySortKey")]
		PersonaHomeCitySortKey,
		// Token: 0x04002306 RID: 8966
		[XmlEnum("persona:FolderIds")]
		PersonaFolderIds,
		// Token: 0x04002307 RID: 8967
		[XmlEnum("persona:Attributions")]
		PersonaAttributions,
		// Token: 0x04002308 RID: 8968
		[XmlEnum("persona:DisplayNames")]
		PersonaDisplayNames,
		// Token: 0x04002309 RID: 8969
		[XmlEnum("persona:Initials")]
		PersonaInitials,
		// Token: 0x0400230A RID: 8970
		[XmlEnum("persona:FileAses")]
		PersonaFileAses,
		// Token: 0x0400230B RID: 8971
		[XmlEnum("persona:FileAsIds")]
		PersonaFileAsIds,
		// Token: 0x0400230C RID: 8972
		[XmlEnum("persona:DisplayNamePrefixes")]
		PersonaDisplayNamePrefixes,
		// Token: 0x0400230D RID: 8973
		[XmlEnum("persona:GivenNames")]
		PersonaGivenNames,
		// Token: 0x0400230E RID: 8974
		[XmlEnum("persona:MiddleName")]
		PersonaMiddleName,
		// Token: 0x0400230F RID: 8975
		[XmlEnum("persona:MiddleNames")]
		PersonaMiddleNames,
		// Token: 0x04002310 RID: 8976
		[XmlEnum("persona:Surnames")]
		PersonaSurnames,
		// Token: 0x04002311 RID: 8977
		[XmlEnum("persona:Generation")]
		PersonaGeneration,
		// Token: 0x04002312 RID: 8978
		[XmlEnum("persona:Generations")]
		PersonaGenerations,
		// Token: 0x04002313 RID: 8979
		[XmlEnum("persona:Nickname")]
		PersonaNickname,
		// Token: 0x04002314 RID: 8980
		[XmlEnum("persona:Nicknames")]
		PersonaNicknames,
		// Token: 0x04002315 RID: 8981
		[XmlEnum("persona:Alias")]
		PersonaAlias,
		// Token: 0x04002316 RID: 8982
		[XmlEnum("persona:UnreadCount")]
		PersonaUnreadCount,
		// Token: 0x04002317 RID: 8983
		[XmlEnum("persona:YomiCompanyNames")]
		PersonaYomiCompanyNames,
		// Token: 0x04002318 RID: 8984
		[XmlEnum("persona:YomiFirstNames")]
		PersonaYomiFirstNames,
		// Token: 0x04002319 RID: 8985
		[XmlEnum("persona:YomiLastNames")]
		PersonaYomiLastNames,
		// Token: 0x0400231A RID: 8986
		[XmlEnum("persona:BusinessPhoneNumbers")]
		PersonaBusinessPhoneNumbers,
		// Token: 0x0400231B RID: 8987
		[XmlEnum("persona:BusinessPhoneNumbers2")]
		PersonaBusinessPhoneNumbers2,
		// Token: 0x0400231C RID: 8988
		[XmlEnum("persona:HomePhones")]
		PersonaHomePhones,
		// Token: 0x0400231D RID: 8989
		[XmlEnum("persona:HomePhones2")]
		PersonaHomePhones2,
		// Token: 0x0400231E RID: 8990
		[XmlEnum("persona:MobilePhones")]
		PersonaMobilePhones,
		// Token: 0x0400231F RID: 8991
		[XmlEnum("persona:MobilePhones2")]
		PersonaMobilePhones2,
		// Token: 0x04002320 RID: 8992
		[XmlEnum("persona:AssistantPhoneNumbers")]
		PersonaAssistantPhoneNumbers,
		// Token: 0x04002321 RID: 8993
		[XmlEnum("persona:CallbackPhones")]
		PersonaCallbackPhones,
		// Token: 0x04002322 RID: 8994
		[XmlEnum("persona:CarPhones")]
		PersonaCarPhones,
		// Token: 0x04002323 RID: 8995
		[XmlEnum("persona:HomeFaxes")]
		PersonaHomeFaxes,
		// Token: 0x04002324 RID: 8996
		[XmlEnum("persona:OrganizationMainPhones")]
		PersonaOrganizationMainPhones,
		// Token: 0x04002325 RID: 8997
		[XmlEnum("persona:OtherFaxes")]
		PersonaOtherFaxes,
		// Token: 0x04002326 RID: 8998
		[XmlEnum("persona:OtherTelephones")]
		PersonaOtherTelephones,
		// Token: 0x04002327 RID: 8999
		[XmlEnum("persona:OtherPhones2")]
		PersonaOtherPhones2,
		// Token: 0x04002328 RID: 9000
		[XmlEnum("persona:Pagers")]
		PersonaPagers,
		// Token: 0x04002329 RID: 9001
		[XmlEnum("persona:RadioPhones")]
		PersonaRadioPhones,
		// Token: 0x0400232A RID: 9002
		[XmlEnum("persona:TelexNumbers")]
		PersonaTelexNumbers,
		// Token: 0x0400232B RID: 9003
		[XmlEnum("persona:TTYTDDPhoneNumbers")]
		PersonaTTYTDDPhoneNumbers,
		// Token: 0x0400232C RID: 9004
		[XmlEnum("persona:WorkFaxes")]
		PersonaWorkFaxes,
		// Token: 0x0400232D RID: 9005
		[XmlEnum("persona:Emails1")]
		PersonaEmails1,
		// Token: 0x0400232E RID: 9006
		[XmlEnum("persona:Emails1DisplayNames")]
		PersonaEmails1DisplayNames,
		// Token: 0x0400232F RID: 9007
		[XmlEnum("persona:Emails1OriginalDisplayNames")]
		PersonaEmails1OriginalDisplayNames,
		// Token: 0x04002330 RID: 9008
		[XmlEnum("persona:Emails2")]
		PersonaEmails2,
		// Token: 0x04002331 RID: 9009
		[XmlEnum("persona:Emails2DisplayNames")]
		PersonaEmails2DisplayNames,
		// Token: 0x04002332 RID: 9010
		[XmlEnum("persona:Emails2OriginalDisplayNames")]
		PersonaEmails2OriginalDisplayNames,
		// Token: 0x04002333 RID: 9011
		[XmlEnum("persona:Emails3")]
		PersonaEmails3,
		// Token: 0x04002334 RID: 9012
		[XmlEnum("persona:Emails3DisplayNames")]
		PersonaEmails3DisplayNames,
		// Token: 0x04002335 RID: 9013
		[XmlEnum("persona:Emails3OriginalDisplayNames")]
		PersonaEmails3OriginalDisplayNames,
		// Token: 0x04002336 RID: 9014
		[XmlEnum("persona:BusinessHomePages")]
		PersonaBusinessHomePages,
		// Token: 0x04002337 RID: 9015
		[XmlEnum("persona:School")]
		PersonaSchools,
		// Token: 0x04002338 RID: 9016
		[XmlEnum("persona:PersonalHomePages")]
		PersonaPersonalHomePages,
		// Token: 0x04002339 RID: 9017
		[XmlEnum("persona:OfficeLocations")]
		PersonaOfficeLocations,
		// Token: 0x0400233A RID: 9018
		[XmlEnum("persona:BusinessAddresses")]
		PersonaBusinessAddresses,
		// Token: 0x0400233B RID: 9019
		[XmlEnum("persona:HomeAddresses")]
		PersonaHomeAddresses,
		// Token: 0x0400233C RID: 9020
		[XmlEnum("persona:OtherAddresses")]
		PersonaOtherAddresses,
		// Token: 0x0400233D RID: 9021
		[XmlEnum("persona:Titles")]
		PersonaTitles,
		// Token: 0x0400233E RID: 9022
		[XmlEnum("persona:Department")]
		PersonaDepartment,
		// Token: 0x0400233F RID: 9023
		[XmlEnum("persona:Departments")]
		PersonaDepartments,
		// Token: 0x04002340 RID: 9024
		[XmlEnum("persona:CompanyNames")]
		PersonaCompanyNames,
		// Token: 0x04002341 RID: 9025
		[XmlEnum("persona:Managers")]
		PersonaManagers,
		// Token: 0x04002342 RID: 9026
		[XmlEnum("persona:AssistantNames")]
		PersonaAssistantNames,
		// Token: 0x04002343 RID: 9027
		[XmlEnum("persona:Professions")]
		PersonaProfessions,
		// Token: 0x04002344 RID: 9028
		[XmlEnum("persona:SpouseNames")]
		PersonaSpouseNames,
		// Token: 0x04002345 RID: 9029
		[XmlEnum("persona:Hobbies")]
		PersonaHobbies,
		// Token: 0x04002346 RID: 9030
		[XmlEnum("persona:WeddingAnniversaries")]
		PersonaWeddingAnniversaries,
		// Token: 0x04002347 RID: 9031
		[XmlEnum("persona:WeddingAnniversariesLocal")]
		PersonaWeddingAnniversariesLocal,
		// Token: 0x04002348 RID: 9032
		[XmlEnum("persona:Birthdays")]
		PersonaBirthdays,
		// Token: 0x04002349 RID: 9033
		[XmlEnum("persona:BirthdaysLocal")]
		PersonaBirthdaysLocal,
		// Token: 0x0400234A RID: 9034
		[XmlEnum("persona:Children")]
		PersonaChildren,
		// Token: 0x0400234B RID: 9035
		[XmlEnum("persona:Location")]
		PersonaLocation,
		// Token: 0x0400234C RID: 9036
		[XmlEnum("persona:Locations")]
		PersonaLocations,
		// Token: 0x0400234D RID: 9037
		[XmlEnum("persona:ExtendedProperties")]
		PersonaExtendedProperties,
		// Token: 0x0400234E RID: 9038
		[XmlEnum("persona:ADObjectId")]
		PersonaADObjectId,
		// Token: 0x0400234F RID: 9039
		[XmlEnum("persona:PostalAddress")]
		PersonaPostalAddress,
		// Token: 0x04002350 RID: 9040
		[XmlEnum("persona:RelevanceScore")]
		PersonaRelevanceScore,
		// Token: 0x04002351 RID: 9041
		[XmlEnum("persona:Bodies")]
		PersonaBodies,
		// Token: 0x04002352 RID: 9042
		[XmlEnum("persona:Members")]
		PersonaMembers,
		// Token: 0x04002353 RID: 9043
		[XmlEnum("persona:ThirdPartyPhotoUrls")]
		PersonaThirdPartyPhotoUrls,
		// Token: 0x04002354 RID: 9044
		[XmlEnum("message:RelyOnConversationIndex")]
		RelyOnConversationIndex,
		// Token: 0x04002355 RID: 9045
		[XmlEnum("message:IsSpecificMessageReplyStamped")]
		IsSpecificMessageReplyStamped,
		// Token: 0x04002356 RID: 9046
		[XmlEnum("message:IsSpecificMessageReply")]
		IsSpecificMessageReply,
		// Token: 0x04002357 RID: 9047
		[XmlEnum("item:SupportsSideConversation")]
		SupportsSideConversation,
		// Token: 0x04002358 RID: 9048
		[XmlEnum("conversation:GlobalLastDeliveryOrRenewTime")]
		ConversationGlobalLastDeliveryOrRenewTime,
		// Token: 0x04002359 RID: 9049
		[XmlEnum("message:LikeCount")]
		LikeCount,
		// Token: 0x0400235A RID: 9050
		[XmlEnum("item:RichContent")]
		RichContent,
		// Token: 0x0400235B RID: 9051
		[XmlEnum("conversation:GlobalRichContent")]
		ConversationGlobalRichContent,
		// Token: 0x0400235C RID: 9052
		[XmlEnum("conversation:ConversationMailboxGuid")]
		ConversationMailboxGuid,
		// Token: 0x0400235D RID: 9053
		[XmlEnum("conversation:ConversationGlobalPreview")]
		ConversationGlobalPreview,
		// Token: 0x0400235E RID: 9054
		[XmlEnum("conversation:LastDeliveryOrRenewTime")]
		ConversationLastDeliveryOrRenewTime,
		// Token: 0x0400235F RID: 9055
		[XmlEnum("message:Likers")]
		Likers
	}
}
