using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000E6 RID: 230
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum UnindexedFieldURIType
	{
		// Token: 0x04000631 RID: 1585
		[XmlEnum("folder:FolderId")]
		folderFolderId,
		// Token: 0x04000632 RID: 1586
		[XmlEnum("folder:ParentFolderId")]
		folderParentFolderId,
		// Token: 0x04000633 RID: 1587
		[XmlEnum("folder:DisplayName")]
		folderDisplayName,
		// Token: 0x04000634 RID: 1588
		[XmlEnum("folder:UnreadCount")]
		folderUnreadCount,
		// Token: 0x04000635 RID: 1589
		[XmlEnum("folder:TotalCount")]
		folderTotalCount,
		// Token: 0x04000636 RID: 1590
		[XmlEnum("folder:ChildFolderCount")]
		folderChildFolderCount,
		// Token: 0x04000637 RID: 1591
		[XmlEnum("folder:FolderClass")]
		folderFolderClass,
		// Token: 0x04000638 RID: 1592
		[XmlEnum("folder:SearchParameters")]
		folderSearchParameters,
		// Token: 0x04000639 RID: 1593
		[XmlEnum("folder:ManagedFolderInformation")]
		folderManagedFolderInformation,
		// Token: 0x0400063A RID: 1594
		[XmlEnum("folder:PermissionSet")]
		folderPermissionSet,
		// Token: 0x0400063B RID: 1595
		[XmlEnum("folder:EffectiveRights")]
		folderEffectiveRights,
		// Token: 0x0400063C RID: 1596
		[XmlEnum("folder:SharingEffectiveRights")]
		folderSharingEffectiveRights,
		// Token: 0x0400063D RID: 1597
		[XmlEnum("folder:DistinguishedFolderId")]
		folderDistinguishedFolderId,
		// Token: 0x0400063E RID: 1598
		[XmlEnum("folder:PolicyTag")]
		folderPolicyTag,
		// Token: 0x0400063F RID: 1599
		[XmlEnum("folder:ArchiveTag")]
		folderArchiveTag,
		// Token: 0x04000640 RID: 1600
		[XmlEnum("item:ItemId")]
		itemItemId,
		// Token: 0x04000641 RID: 1601
		[XmlEnum("item:ParentFolderId")]
		itemParentFolderId,
		// Token: 0x04000642 RID: 1602
		[XmlEnum("item:ItemClass")]
		itemItemClass,
		// Token: 0x04000643 RID: 1603
		[XmlEnum("item:MimeContent")]
		itemMimeContent,
		// Token: 0x04000644 RID: 1604
		[XmlEnum("item:Attachments")]
		itemAttachments,
		// Token: 0x04000645 RID: 1605
		[XmlEnum("item:Subject")]
		itemSubject,
		// Token: 0x04000646 RID: 1606
		[XmlEnum("item:DateTimeReceived")]
		itemDateTimeReceived,
		// Token: 0x04000647 RID: 1607
		[XmlEnum("item:Size")]
		itemSize,
		// Token: 0x04000648 RID: 1608
		[XmlEnum("item:Categories")]
		itemCategories,
		// Token: 0x04000649 RID: 1609
		[XmlEnum("item:HasAttachments")]
		itemHasAttachments,
		// Token: 0x0400064A RID: 1610
		[XmlEnum("item:Importance")]
		itemImportance,
		// Token: 0x0400064B RID: 1611
		[XmlEnum("item:InReplyTo")]
		itemInReplyTo,
		// Token: 0x0400064C RID: 1612
		[XmlEnum("item:InternetMessageHeaders")]
		itemInternetMessageHeaders,
		// Token: 0x0400064D RID: 1613
		[XmlEnum("item:IsAssociated")]
		itemIsAssociated,
		// Token: 0x0400064E RID: 1614
		[XmlEnum("item:IsDraft")]
		itemIsDraft,
		// Token: 0x0400064F RID: 1615
		[XmlEnum("item:IsFromMe")]
		itemIsFromMe,
		// Token: 0x04000650 RID: 1616
		[XmlEnum("item:IsResend")]
		itemIsResend,
		// Token: 0x04000651 RID: 1617
		[XmlEnum("item:IsSubmitted")]
		itemIsSubmitted,
		// Token: 0x04000652 RID: 1618
		[XmlEnum("item:IsUnmodified")]
		itemIsUnmodified,
		// Token: 0x04000653 RID: 1619
		[XmlEnum("item:DateTimeSent")]
		itemDateTimeSent,
		// Token: 0x04000654 RID: 1620
		[XmlEnum("item:DateTimeCreated")]
		itemDateTimeCreated,
		// Token: 0x04000655 RID: 1621
		[XmlEnum("item:Body")]
		itemBody,
		// Token: 0x04000656 RID: 1622
		[XmlEnum("item:ResponseObjects")]
		itemResponseObjects,
		// Token: 0x04000657 RID: 1623
		[XmlEnum("item:Sensitivity")]
		itemSensitivity,
		// Token: 0x04000658 RID: 1624
		[XmlEnum("item:ReminderDueBy")]
		itemReminderDueBy,
		// Token: 0x04000659 RID: 1625
		[XmlEnum("item:ReminderIsSet")]
		itemReminderIsSet,
		// Token: 0x0400065A RID: 1626
		[XmlEnum("item:ReminderNextTime")]
		itemReminderNextTime,
		// Token: 0x0400065B RID: 1627
		[XmlEnum("item:ReminderMinutesBeforeStart")]
		itemReminderMinutesBeforeStart,
		// Token: 0x0400065C RID: 1628
		[XmlEnum("item:DisplayTo")]
		itemDisplayTo,
		// Token: 0x0400065D RID: 1629
		[XmlEnum("item:DisplayCc")]
		itemDisplayCc,
		// Token: 0x0400065E RID: 1630
		[XmlEnum("item:Culture")]
		itemCulture,
		// Token: 0x0400065F RID: 1631
		[XmlEnum("item:EffectiveRights")]
		itemEffectiveRights,
		// Token: 0x04000660 RID: 1632
		[XmlEnum("item:LastModifiedName")]
		itemLastModifiedName,
		// Token: 0x04000661 RID: 1633
		[XmlEnum("item:LastModifiedTime")]
		itemLastModifiedTime,
		// Token: 0x04000662 RID: 1634
		[XmlEnum("item:ConversationId")]
		itemConversationId,
		// Token: 0x04000663 RID: 1635
		[XmlEnum("item:UniqueBody")]
		itemUniqueBody,
		// Token: 0x04000664 RID: 1636
		[XmlEnum("item:Flag")]
		itemFlag,
		// Token: 0x04000665 RID: 1637
		[XmlEnum("item:StoreEntryId")]
		itemStoreEntryId,
		// Token: 0x04000666 RID: 1638
		[XmlEnum("item:InstanceKey")]
		itemInstanceKey,
		// Token: 0x04000667 RID: 1639
		[XmlEnum("item:NormalizedBody")]
		itemNormalizedBody,
		// Token: 0x04000668 RID: 1640
		[XmlEnum("item:EntityExtractionResult")]
		itemEntityExtractionResult,
		// Token: 0x04000669 RID: 1641
		[XmlEnum("item:PolicyTag")]
		itemPolicyTag,
		// Token: 0x0400066A RID: 1642
		[XmlEnum("item:ArchiveTag")]
		itemArchiveTag,
		// Token: 0x0400066B RID: 1643
		[XmlEnum("item:RetentionDate")]
		itemRetentionDate,
		// Token: 0x0400066C RID: 1644
		[XmlEnum("item:Preview")]
		itemPreview,
		// Token: 0x0400066D RID: 1645
		[XmlEnum("item:PredictedActionReasons")]
		itemPredictedActionReasons,
		// Token: 0x0400066E RID: 1646
		[XmlEnum("item:IsClutter")]
		itemIsClutter,
		// Token: 0x0400066F RID: 1647
		[XmlEnum("item:RightsManagementLicenseData")]
		itemRightsManagementLicenseData,
		// Token: 0x04000670 RID: 1648
		[XmlEnum("item:BlockStatus")]
		itemBlockStatus,
		// Token: 0x04000671 RID: 1649
		[XmlEnum("item:HasBlockedImages")]
		itemHasBlockedImages,
		// Token: 0x04000672 RID: 1650
		[XmlEnum("item:WebClientReadFormQueryString")]
		itemWebClientReadFormQueryString,
		// Token: 0x04000673 RID: 1651
		[XmlEnum("item:WebClientEditFormQueryString")]
		itemWebClientEditFormQueryString,
		// Token: 0x04000674 RID: 1652
		[XmlEnum("item:TextBody")]
		itemTextBody,
		// Token: 0x04000675 RID: 1653
		[XmlEnum("item:IconIndex")]
		itemIconIndex,
		// Token: 0x04000676 RID: 1654
		[XmlEnum("item:MimeContentUTF8")]
		itemMimeContentUTF8,
		// Token: 0x04000677 RID: 1655
		[XmlEnum("message:ConversationIndex")]
		messageConversationIndex,
		// Token: 0x04000678 RID: 1656
		[XmlEnum("message:ConversationTopic")]
		messageConversationTopic,
		// Token: 0x04000679 RID: 1657
		[XmlEnum("message:InternetMessageId")]
		messageInternetMessageId,
		// Token: 0x0400067A RID: 1658
		[XmlEnum("message:IsRead")]
		messageIsRead,
		// Token: 0x0400067B RID: 1659
		[XmlEnum("message:IsResponseRequested")]
		messageIsResponseRequested,
		// Token: 0x0400067C RID: 1660
		[XmlEnum("message:IsReadReceiptRequested")]
		messageIsReadReceiptRequested,
		// Token: 0x0400067D RID: 1661
		[XmlEnum("message:IsDeliveryReceiptRequested")]
		messageIsDeliveryReceiptRequested,
		// Token: 0x0400067E RID: 1662
		[XmlEnum("message:ReceivedBy")]
		messageReceivedBy,
		// Token: 0x0400067F RID: 1663
		[XmlEnum("message:ReceivedRepresenting")]
		messageReceivedRepresenting,
		// Token: 0x04000680 RID: 1664
		[XmlEnum("message:References")]
		messageReferences,
		// Token: 0x04000681 RID: 1665
		[XmlEnum("message:ReplyTo")]
		messageReplyTo,
		// Token: 0x04000682 RID: 1666
		[XmlEnum("message:From")]
		messageFrom,
		// Token: 0x04000683 RID: 1667
		[XmlEnum("message:Sender")]
		messageSender,
		// Token: 0x04000684 RID: 1668
		[XmlEnum("message:ToRecipients")]
		messageToRecipients,
		// Token: 0x04000685 RID: 1669
		[XmlEnum("message:CcRecipients")]
		messageCcRecipients,
		// Token: 0x04000686 RID: 1670
		[XmlEnum("message:BccRecipients")]
		messageBccRecipients,
		// Token: 0x04000687 RID: 1671
		[XmlEnum("message:ApprovalRequestData")]
		messageApprovalRequestData,
		// Token: 0x04000688 RID: 1672
		[XmlEnum("message:VotingInformation")]
		messageVotingInformation,
		// Token: 0x04000689 RID: 1673
		[XmlEnum("message:ReminderMessageData")]
		messageReminderMessageData,
		// Token: 0x0400068A RID: 1674
		[XmlEnum("meeting:AssociatedCalendarItemId")]
		meetingAssociatedCalendarItemId,
		// Token: 0x0400068B RID: 1675
		[XmlEnum("meeting:IsDelegated")]
		meetingIsDelegated,
		// Token: 0x0400068C RID: 1676
		[XmlEnum("meeting:IsOutOfDate")]
		meetingIsOutOfDate,
		// Token: 0x0400068D RID: 1677
		[XmlEnum("meeting:HasBeenProcessed")]
		meetingHasBeenProcessed,
		// Token: 0x0400068E RID: 1678
		[XmlEnum("meeting:ResponseType")]
		meetingResponseType,
		// Token: 0x0400068F RID: 1679
		[XmlEnum("meeting:ProposedStart")]
		meetingProposedStart,
		// Token: 0x04000690 RID: 1680
		[XmlEnum("meeting:ProposedEnd")]
		meetingProposedEnd,
		// Token: 0x04000691 RID: 1681
		[XmlEnum("meetingRequest:MeetingRequestType")]
		meetingRequestMeetingRequestType,
		// Token: 0x04000692 RID: 1682
		[XmlEnum("meetingRequest:IntendedFreeBusyStatus")]
		meetingRequestIntendedFreeBusyStatus,
		// Token: 0x04000693 RID: 1683
		[XmlEnum("meetingRequest:ChangeHighlights")]
		meetingRequestChangeHighlights,
		// Token: 0x04000694 RID: 1684
		[XmlEnum("calendar:Start")]
		calendarStart,
		// Token: 0x04000695 RID: 1685
		[XmlEnum("calendar:End")]
		calendarEnd,
		// Token: 0x04000696 RID: 1686
		[XmlEnum("calendar:OriginalStart")]
		calendarOriginalStart,
		// Token: 0x04000697 RID: 1687
		[XmlEnum("calendar:StartWallClock")]
		calendarStartWallClock,
		// Token: 0x04000698 RID: 1688
		[XmlEnum("calendar:EndWallClock")]
		calendarEndWallClock,
		// Token: 0x04000699 RID: 1689
		[XmlEnum("calendar:StartTimeZoneId")]
		calendarStartTimeZoneId,
		// Token: 0x0400069A RID: 1690
		[XmlEnum("calendar:EndTimeZoneId")]
		calendarEndTimeZoneId,
		// Token: 0x0400069B RID: 1691
		[XmlEnum("calendar:IsAllDayEvent")]
		calendarIsAllDayEvent,
		// Token: 0x0400069C RID: 1692
		[XmlEnum("calendar:LegacyFreeBusyStatus")]
		calendarLegacyFreeBusyStatus,
		// Token: 0x0400069D RID: 1693
		[XmlEnum("calendar:Location")]
		calendarLocation,
		// Token: 0x0400069E RID: 1694
		[XmlEnum("calendar:EnhancedLocation")]
		calendarEnhancedLocation,
		// Token: 0x0400069F RID: 1695
		[XmlEnum("calendar:When")]
		calendarWhen,
		// Token: 0x040006A0 RID: 1696
		[XmlEnum("calendar:IsMeeting")]
		calendarIsMeeting,
		// Token: 0x040006A1 RID: 1697
		[XmlEnum("calendar:IsCancelled")]
		calendarIsCancelled,
		// Token: 0x040006A2 RID: 1698
		[XmlEnum("calendar:IsRecurring")]
		calendarIsRecurring,
		// Token: 0x040006A3 RID: 1699
		[XmlEnum("calendar:MeetingRequestWasSent")]
		calendarMeetingRequestWasSent,
		// Token: 0x040006A4 RID: 1700
		[XmlEnum("calendar:IsResponseRequested")]
		calendarIsResponseRequested,
		// Token: 0x040006A5 RID: 1701
		[XmlEnum("calendar:CalendarItemType")]
		calendarCalendarItemType,
		// Token: 0x040006A6 RID: 1702
		[XmlEnum("calendar:MyResponseType")]
		calendarMyResponseType,
		// Token: 0x040006A7 RID: 1703
		[XmlEnum("calendar:Organizer")]
		calendarOrganizer,
		// Token: 0x040006A8 RID: 1704
		[XmlEnum("calendar:RequiredAttendees")]
		calendarRequiredAttendees,
		// Token: 0x040006A9 RID: 1705
		[XmlEnum("calendar:OptionalAttendees")]
		calendarOptionalAttendees,
		// Token: 0x040006AA RID: 1706
		[XmlEnum("calendar:Resources")]
		calendarResources,
		// Token: 0x040006AB RID: 1707
		[XmlEnum("calendar:ConflictingMeetingCount")]
		calendarConflictingMeetingCount,
		// Token: 0x040006AC RID: 1708
		[XmlEnum("calendar:AdjacentMeetingCount")]
		calendarAdjacentMeetingCount,
		// Token: 0x040006AD RID: 1709
		[XmlEnum("calendar:ConflictingMeetings")]
		calendarConflictingMeetings,
		// Token: 0x040006AE RID: 1710
		[XmlEnum("calendar:AdjacentMeetings")]
		calendarAdjacentMeetings,
		// Token: 0x040006AF RID: 1711
		[XmlEnum("calendar:Duration")]
		calendarDuration,
		// Token: 0x040006B0 RID: 1712
		[XmlEnum("calendar:TimeZone")]
		calendarTimeZone,
		// Token: 0x040006B1 RID: 1713
		[XmlEnum("calendar:AppointmentReplyTime")]
		calendarAppointmentReplyTime,
		// Token: 0x040006B2 RID: 1714
		[XmlEnum("calendar:AppointmentSequenceNumber")]
		calendarAppointmentSequenceNumber,
		// Token: 0x040006B3 RID: 1715
		[XmlEnum("calendar:AppointmentState")]
		calendarAppointmentState,
		// Token: 0x040006B4 RID: 1716
		[XmlEnum("calendar:Recurrence")]
		calendarRecurrence,
		// Token: 0x040006B5 RID: 1717
		[XmlEnum("calendar:FirstOccurrence")]
		calendarFirstOccurrence,
		// Token: 0x040006B6 RID: 1718
		[XmlEnum("calendar:LastOccurrence")]
		calendarLastOccurrence,
		// Token: 0x040006B7 RID: 1719
		[XmlEnum("calendar:ModifiedOccurrences")]
		calendarModifiedOccurrences,
		// Token: 0x040006B8 RID: 1720
		[XmlEnum("calendar:DeletedOccurrences")]
		calendarDeletedOccurrences,
		// Token: 0x040006B9 RID: 1721
		[XmlEnum("calendar:MeetingTimeZone")]
		calendarMeetingTimeZone,
		// Token: 0x040006BA RID: 1722
		[XmlEnum("calendar:ConferenceType")]
		calendarConferenceType,
		// Token: 0x040006BB RID: 1723
		[XmlEnum("calendar:AllowNewTimeProposal")]
		calendarAllowNewTimeProposal,
		// Token: 0x040006BC RID: 1724
		[XmlEnum("calendar:IsOnlineMeeting")]
		calendarIsOnlineMeeting,
		// Token: 0x040006BD RID: 1725
		[XmlEnum("calendar:MeetingWorkspaceUrl")]
		calendarMeetingWorkspaceUrl,
		// Token: 0x040006BE RID: 1726
		[XmlEnum("calendar:NetShowUrl")]
		calendarNetShowUrl,
		// Token: 0x040006BF RID: 1727
		[XmlEnum("calendar:UID")]
		calendarUID,
		// Token: 0x040006C0 RID: 1728
		[XmlEnum("calendar:RecurrenceId")]
		calendarRecurrenceId,
		// Token: 0x040006C1 RID: 1729
		[XmlEnum("calendar:DateTimeStamp")]
		calendarDateTimeStamp,
		// Token: 0x040006C2 RID: 1730
		[XmlEnum("calendar:StartTimeZone")]
		calendarStartTimeZone,
		// Token: 0x040006C3 RID: 1731
		[XmlEnum("calendar:EndTimeZone")]
		calendarEndTimeZone,
		// Token: 0x040006C4 RID: 1732
		[XmlEnum("calendar:JoinOnlineMeetingUrl")]
		calendarJoinOnlineMeetingUrl,
		// Token: 0x040006C5 RID: 1733
		[XmlEnum("calendar:OnlineMeetingSettings")]
		calendarOnlineMeetingSettings,
		// Token: 0x040006C6 RID: 1734
		[XmlEnum("calendar:IsOrganizer")]
		calendarIsOrganizer,
		// Token: 0x040006C7 RID: 1735
		[XmlEnum("task:ActualWork")]
		taskActualWork,
		// Token: 0x040006C8 RID: 1736
		[XmlEnum("task:AssignedTime")]
		taskAssignedTime,
		// Token: 0x040006C9 RID: 1737
		[XmlEnum("task:BillingInformation")]
		taskBillingInformation,
		// Token: 0x040006CA RID: 1738
		[XmlEnum("task:ChangeCount")]
		taskChangeCount,
		// Token: 0x040006CB RID: 1739
		[XmlEnum("task:Companies")]
		taskCompanies,
		// Token: 0x040006CC RID: 1740
		[XmlEnum("task:CompleteDate")]
		taskCompleteDate,
		// Token: 0x040006CD RID: 1741
		[XmlEnum("task:Contacts")]
		taskContacts,
		// Token: 0x040006CE RID: 1742
		[XmlEnum("task:DelegationState")]
		taskDelegationState,
		// Token: 0x040006CF RID: 1743
		[XmlEnum("task:Delegator")]
		taskDelegator,
		// Token: 0x040006D0 RID: 1744
		[XmlEnum("task:DueDate")]
		taskDueDate,
		// Token: 0x040006D1 RID: 1745
		[XmlEnum("task:IsAssignmentEditable")]
		taskIsAssignmentEditable,
		// Token: 0x040006D2 RID: 1746
		[XmlEnum("task:IsComplete")]
		taskIsComplete,
		// Token: 0x040006D3 RID: 1747
		[XmlEnum("task:IsRecurring")]
		taskIsRecurring,
		// Token: 0x040006D4 RID: 1748
		[XmlEnum("task:IsTeamTask")]
		taskIsTeamTask,
		// Token: 0x040006D5 RID: 1749
		[XmlEnum("task:Mileage")]
		taskMileage,
		// Token: 0x040006D6 RID: 1750
		[XmlEnum("task:Owner")]
		taskOwner,
		// Token: 0x040006D7 RID: 1751
		[XmlEnum("task:PercentComplete")]
		taskPercentComplete,
		// Token: 0x040006D8 RID: 1752
		[XmlEnum("task:Recurrence")]
		taskRecurrence,
		// Token: 0x040006D9 RID: 1753
		[XmlEnum("task:StartDate")]
		taskStartDate,
		// Token: 0x040006DA RID: 1754
		[XmlEnum("task:Status")]
		taskStatus,
		// Token: 0x040006DB RID: 1755
		[XmlEnum("task:StatusDescription")]
		taskStatusDescription,
		// Token: 0x040006DC RID: 1756
		[XmlEnum("task:TotalWork")]
		taskTotalWork,
		// Token: 0x040006DD RID: 1757
		[XmlEnum("contacts:Alias")]
		contactsAlias,
		// Token: 0x040006DE RID: 1758
		[XmlEnum("contacts:AssistantName")]
		contactsAssistantName,
		// Token: 0x040006DF RID: 1759
		[XmlEnum("contacts:Birthday")]
		contactsBirthday,
		// Token: 0x040006E0 RID: 1760
		[XmlEnum("contacts:BusinessHomePage")]
		contactsBusinessHomePage,
		// Token: 0x040006E1 RID: 1761
		[XmlEnum("contacts:Children")]
		contactsChildren,
		// Token: 0x040006E2 RID: 1762
		[XmlEnum("contacts:Companies")]
		contactsCompanies,
		// Token: 0x040006E3 RID: 1763
		[XmlEnum("contacts:CompanyName")]
		contactsCompanyName,
		// Token: 0x040006E4 RID: 1764
		[XmlEnum("contacts:CompleteName")]
		contactsCompleteName,
		// Token: 0x040006E5 RID: 1765
		[XmlEnum("contacts:ContactSource")]
		contactsContactSource,
		// Token: 0x040006E6 RID: 1766
		[XmlEnum("contacts:Culture")]
		contactsCulture,
		// Token: 0x040006E7 RID: 1767
		[XmlEnum("contacts:Department")]
		contactsDepartment,
		// Token: 0x040006E8 RID: 1768
		[XmlEnum("contacts:DisplayName")]
		contactsDisplayName,
		// Token: 0x040006E9 RID: 1769
		[XmlEnum("contacts:DirectoryId")]
		contactsDirectoryId,
		// Token: 0x040006EA RID: 1770
		[XmlEnum("contacts:DirectReports")]
		contactsDirectReports,
		// Token: 0x040006EB RID: 1771
		[XmlEnum("contacts:EmailAddresses")]
		contactsEmailAddresses,
		// Token: 0x040006EC RID: 1772
		[XmlEnum("contacts:FileAs")]
		contactsFileAs,
		// Token: 0x040006ED RID: 1773
		[XmlEnum("contacts:FileAsMapping")]
		contactsFileAsMapping,
		// Token: 0x040006EE RID: 1774
		[XmlEnum("contacts:Generation")]
		contactsGeneration,
		// Token: 0x040006EF RID: 1775
		[XmlEnum("contacts:GivenName")]
		contactsGivenName,
		// Token: 0x040006F0 RID: 1776
		[XmlEnum("contacts:ImAddresses")]
		contactsImAddresses,
		// Token: 0x040006F1 RID: 1777
		[XmlEnum("contacts:Initials")]
		contactsInitials,
		// Token: 0x040006F2 RID: 1778
		[XmlEnum("contacts:JobTitle")]
		contactsJobTitle,
		// Token: 0x040006F3 RID: 1779
		[XmlEnum("contacts:Manager")]
		contactsManager,
		// Token: 0x040006F4 RID: 1780
		[XmlEnum("contacts:ManagerMailbox")]
		contactsManagerMailbox,
		// Token: 0x040006F5 RID: 1781
		[XmlEnum("contacts:MiddleName")]
		contactsMiddleName,
		// Token: 0x040006F6 RID: 1782
		[XmlEnum("contacts:Mileage")]
		contactsMileage,
		// Token: 0x040006F7 RID: 1783
		[XmlEnum("contacts:MSExchangeCertificate")]
		contactsMSExchangeCertificate,
		// Token: 0x040006F8 RID: 1784
		[XmlEnum("contacts:Nickname")]
		contactsNickname,
		// Token: 0x040006F9 RID: 1785
		[XmlEnum("contacts:Notes")]
		contactsNotes,
		// Token: 0x040006FA RID: 1786
		[XmlEnum("contacts:OfficeLocation")]
		contactsOfficeLocation,
		// Token: 0x040006FB RID: 1787
		[XmlEnum("contacts:PhoneNumbers")]
		contactsPhoneNumbers,
		// Token: 0x040006FC RID: 1788
		[XmlEnum("contacts:PhoneticFullName")]
		contactsPhoneticFullName,
		// Token: 0x040006FD RID: 1789
		[XmlEnum("contacts:PhoneticFirstName")]
		contactsPhoneticFirstName,
		// Token: 0x040006FE RID: 1790
		[XmlEnum("contacts:PhoneticLastName")]
		contactsPhoneticLastName,
		// Token: 0x040006FF RID: 1791
		[XmlEnum("contacts:Photo")]
		contactsPhoto,
		// Token: 0x04000700 RID: 1792
		[XmlEnum("contacts:PhysicalAddresses")]
		contactsPhysicalAddresses,
		// Token: 0x04000701 RID: 1793
		[XmlEnum("contacts:PostalAddressIndex")]
		contactsPostalAddressIndex,
		// Token: 0x04000702 RID: 1794
		[XmlEnum("contacts:Profession")]
		contactsProfession,
		// Token: 0x04000703 RID: 1795
		[XmlEnum("contacts:SpouseName")]
		contactsSpouseName,
		// Token: 0x04000704 RID: 1796
		[XmlEnum("contacts:Surname")]
		contactsSurname,
		// Token: 0x04000705 RID: 1797
		[XmlEnum("contacts:WeddingAnniversary")]
		contactsWeddingAnniversary,
		// Token: 0x04000706 RID: 1798
		[XmlEnum("contacts:UserSMIMECertificate")]
		contactsUserSMIMECertificate,
		// Token: 0x04000707 RID: 1799
		[XmlEnum("contacts:HasPicture")]
		contactsHasPicture,
		// Token: 0x04000708 RID: 1800
		[XmlEnum("distributionlist:Members")]
		distributionlistMembers,
		// Token: 0x04000709 RID: 1801
		[XmlEnum("postitem:PostedTime")]
		postitemPostedTime,
		// Token: 0x0400070A RID: 1802
		[XmlEnum("conversation:ConversationId")]
		conversationConversationId,
		// Token: 0x0400070B RID: 1803
		[XmlEnum("conversation:ConversationTopic")]
		conversationConversationTopic,
		// Token: 0x0400070C RID: 1804
		[XmlEnum("conversation:UniqueRecipients")]
		conversationUniqueRecipients,
		// Token: 0x0400070D RID: 1805
		[XmlEnum("conversation:GlobalUniqueRecipients")]
		conversationGlobalUniqueRecipients,
		// Token: 0x0400070E RID: 1806
		[XmlEnum("conversation:UniqueUnreadSenders")]
		conversationUniqueUnreadSenders,
		// Token: 0x0400070F RID: 1807
		[XmlEnum("conversation:GlobalUniqueUnreadSenders")]
		conversationGlobalUniqueUnreadSenders,
		// Token: 0x04000710 RID: 1808
		[XmlEnum("conversation:UniqueSenders")]
		conversationUniqueSenders,
		// Token: 0x04000711 RID: 1809
		[XmlEnum("conversation:GlobalUniqueSenders")]
		conversationGlobalUniqueSenders,
		// Token: 0x04000712 RID: 1810
		[XmlEnum("conversation:LastDeliveryTime")]
		conversationLastDeliveryTime,
		// Token: 0x04000713 RID: 1811
		[XmlEnum("conversation:GlobalLastDeliveryTime")]
		conversationGlobalLastDeliveryTime,
		// Token: 0x04000714 RID: 1812
		[XmlEnum("conversation:Categories")]
		conversationCategories,
		// Token: 0x04000715 RID: 1813
		[XmlEnum("conversation:GlobalCategories")]
		conversationGlobalCategories,
		// Token: 0x04000716 RID: 1814
		[XmlEnum("conversation:FlagStatus")]
		conversationFlagStatus,
		// Token: 0x04000717 RID: 1815
		[XmlEnum("conversation:GlobalFlagStatus")]
		conversationGlobalFlagStatus,
		// Token: 0x04000718 RID: 1816
		[XmlEnum("conversation:HasAttachments")]
		conversationHasAttachments,
		// Token: 0x04000719 RID: 1817
		[XmlEnum("conversation:GlobalHasAttachments")]
		conversationGlobalHasAttachments,
		// Token: 0x0400071A RID: 1818
		[XmlEnum("conversation:HasIrm")]
		conversationHasIrm,
		// Token: 0x0400071B RID: 1819
		[XmlEnum("conversation:GlobalHasIrm")]
		conversationGlobalHasIrm,
		// Token: 0x0400071C RID: 1820
		[XmlEnum("conversation:MessageCount")]
		conversationMessageCount,
		// Token: 0x0400071D RID: 1821
		[XmlEnum("conversation:GlobalMessageCount")]
		conversationGlobalMessageCount,
		// Token: 0x0400071E RID: 1822
		[XmlEnum("conversation:UnreadCount")]
		conversationUnreadCount,
		// Token: 0x0400071F RID: 1823
		[XmlEnum("conversation:GlobalUnreadCount")]
		conversationGlobalUnreadCount,
		// Token: 0x04000720 RID: 1824
		[XmlEnum("conversation:Size")]
		conversationSize,
		// Token: 0x04000721 RID: 1825
		[XmlEnum("conversation:GlobalSize")]
		conversationGlobalSize,
		// Token: 0x04000722 RID: 1826
		[XmlEnum("conversation:ItemClasses")]
		conversationItemClasses,
		// Token: 0x04000723 RID: 1827
		[XmlEnum("conversation:GlobalItemClasses")]
		conversationGlobalItemClasses,
		// Token: 0x04000724 RID: 1828
		[XmlEnum("conversation:Importance")]
		conversationImportance,
		// Token: 0x04000725 RID: 1829
		[XmlEnum("conversation:GlobalImportance")]
		conversationGlobalImportance,
		// Token: 0x04000726 RID: 1830
		[XmlEnum("conversation:ItemIds")]
		conversationItemIds,
		// Token: 0x04000727 RID: 1831
		[XmlEnum("conversation:GlobalItemIds")]
		conversationGlobalItemIds,
		// Token: 0x04000728 RID: 1832
		[XmlEnum("conversation:LastModifiedTime")]
		conversationLastModifiedTime,
		// Token: 0x04000729 RID: 1833
		[XmlEnum("conversation:InstanceKey")]
		conversationInstanceKey,
		// Token: 0x0400072A RID: 1834
		[XmlEnum("conversation:Preview")]
		conversationPreview,
		// Token: 0x0400072B RID: 1835
		[XmlEnum("conversation:IconIndex")]
		conversationIconIndex,
		// Token: 0x0400072C RID: 1836
		[XmlEnum("conversation:GlobalIconIndex")]
		conversationGlobalIconIndex,
		// Token: 0x0400072D RID: 1837
		[XmlEnum("conversation:DraftItemIds")]
		conversationDraftItemIds,
		// Token: 0x0400072E RID: 1838
		[XmlEnum("conversation:HasClutter")]
		conversationHasClutter,
		// Token: 0x0400072F RID: 1839
		[XmlEnum("persona:PersonaId")]
		personaPersonaId,
		// Token: 0x04000730 RID: 1840
		[XmlEnum("persona:PersonaType")]
		personaPersonaType,
		// Token: 0x04000731 RID: 1841
		[XmlEnum("persona:GivenName")]
		personaGivenName,
		// Token: 0x04000732 RID: 1842
		[XmlEnum("persona:CompanyName")]
		personaCompanyName,
		// Token: 0x04000733 RID: 1843
		[XmlEnum("persona:Surname")]
		personaSurname,
		// Token: 0x04000734 RID: 1844
		[XmlEnum("persona:DisplayName")]
		personaDisplayName,
		// Token: 0x04000735 RID: 1845
		[XmlEnum("persona:EmailAddress")]
		personaEmailAddress,
		// Token: 0x04000736 RID: 1846
		[XmlEnum("persona:FileAs")]
		personaFileAs,
		// Token: 0x04000737 RID: 1847
		[XmlEnum("persona:HomeCity")]
		personaHomeCity,
		// Token: 0x04000738 RID: 1848
		[XmlEnum("persona:CreationTime")]
		personaCreationTime,
		// Token: 0x04000739 RID: 1849
		[XmlEnum("persona:RelevanceScore")]
		personaRelevanceScore,
		// Token: 0x0400073A RID: 1850
		[XmlEnum("persona:WorkCity")]
		personaWorkCity,
		// Token: 0x0400073B RID: 1851
		[XmlEnum("persona:PersonaObjectStatus")]
		personaPersonaObjectStatus,
		// Token: 0x0400073C RID: 1852
		[XmlEnum("persona:FileAsId")]
		personaFileAsId,
		// Token: 0x0400073D RID: 1853
		[XmlEnum("persona:DisplayNamePrefix")]
		personaDisplayNamePrefix,
		// Token: 0x0400073E RID: 1854
		[XmlEnum("persona:YomiCompanyName")]
		personaYomiCompanyName,
		// Token: 0x0400073F RID: 1855
		[XmlEnum("persona:YomiFirstName")]
		personaYomiFirstName,
		// Token: 0x04000740 RID: 1856
		[XmlEnum("persona:YomiLastName")]
		personaYomiLastName,
		// Token: 0x04000741 RID: 1857
		[XmlEnum("persona:Title")]
		personaTitle,
		// Token: 0x04000742 RID: 1858
		[XmlEnum("persona:EmailAddresses")]
		personaEmailAddresses,
		// Token: 0x04000743 RID: 1859
		[XmlEnum("persona:PhoneNumber")]
		personaPhoneNumber,
		// Token: 0x04000744 RID: 1860
		[XmlEnum("persona:ImAddress")]
		personaImAddress,
		// Token: 0x04000745 RID: 1861
		[XmlEnum("persona:ImAddresses")]
		personaImAddresses,
		// Token: 0x04000746 RID: 1862
		[XmlEnum("persona:ImAddresses2")]
		personaImAddresses2,
		// Token: 0x04000747 RID: 1863
		[XmlEnum("persona:ImAddresses3")]
		personaImAddresses3,
		// Token: 0x04000748 RID: 1864
		[XmlEnum("persona:FolderIds")]
		personaFolderIds,
		// Token: 0x04000749 RID: 1865
		[XmlEnum("persona:Attributions")]
		personaAttributions,
		// Token: 0x0400074A RID: 1866
		[XmlEnum("persona:DisplayNames")]
		personaDisplayNames,
		// Token: 0x0400074B RID: 1867
		[XmlEnum("persona:Initials")]
		personaInitials,
		// Token: 0x0400074C RID: 1868
		[XmlEnum("persona:FileAses")]
		personaFileAses,
		// Token: 0x0400074D RID: 1869
		[XmlEnum("persona:FileAsIds")]
		personaFileAsIds,
		// Token: 0x0400074E RID: 1870
		[XmlEnum("persona:DisplayNamePrefixes")]
		personaDisplayNamePrefixes,
		// Token: 0x0400074F RID: 1871
		[XmlEnum("persona:GivenNames")]
		personaGivenNames,
		// Token: 0x04000750 RID: 1872
		[XmlEnum("persona:MiddleNames")]
		personaMiddleNames,
		// Token: 0x04000751 RID: 1873
		[XmlEnum("persona:Surnames")]
		personaSurnames,
		// Token: 0x04000752 RID: 1874
		[XmlEnum("persona:Generations")]
		personaGenerations,
		// Token: 0x04000753 RID: 1875
		[XmlEnum("persona:Nicknames")]
		personaNicknames,
		// Token: 0x04000754 RID: 1876
		[XmlEnum("persona:YomiCompanyNames")]
		personaYomiCompanyNames,
		// Token: 0x04000755 RID: 1877
		[XmlEnum("persona:YomiFirstNames")]
		personaYomiFirstNames,
		// Token: 0x04000756 RID: 1878
		[XmlEnum("persona:YomiLastNames")]
		personaYomiLastNames,
		// Token: 0x04000757 RID: 1879
		[XmlEnum("persona:BusinessPhoneNumbers")]
		personaBusinessPhoneNumbers,
		// Token: 0x04000758 RID: 1880
		[XmlEnum("persona:BusinessPhoneNumbers2")]
		personaBusinessPhoneNumbers2,
		// Token: 0x04000759 RID: 1881
		[XmlEnum("persona:HomePhones")]
		personaHomePhones,
		// Token: 0x0400075A RID: 1882
		[XmlEnum("persona:HomePhones2")]
		personaHomePhones2,
		// Token: 0x0400075B RID: 1883
		[XmlEnum("persona:MobilePhones")]
		personaMobilePhones,
		// Token: 0x0400075C RID: 1884
		[XmlEnum("persona:MobilePhones2")]
		personaMobilePhones2,
		// Token: 0x0400075D RID: 1885
		[XmlEnum("persona:AssistantPhoneNumbers")]
		personaAssistantPhoneNumbers,
		// Token: 0x0400075E RID: 1886
		[XmlEnum("persona:CallbackPhones")]
		personaCallbackPhones,
		// Token: 0x0400075F RID: 1887
		[XmlEnum("persona:CarPhones")]
		personaCarPhones,
		// Token: 0x04000760 RID: 1888
		[XmlEnum("persona:HomeFaxes")]
		personaHomeFaxes,
		// Token: 0x04000761 RID: 1889
		[XmlEnum("persona:OrganizationMainPhones")]
		personaOrganizationMainPhones,
		// Token: 0x04000762 RID: 1890
		[XmlEnum("persona:OtherFaxes")]
		personaOtherFaxes,
		// Token: 0x04000763 RID: 1891
		[XmlEnum("persona:OtherTelephones")]
		personaOtherTelephones,
		// Token: 0x04000764 RID: 1892
		[XmlEnum("persona:OtherPhones2")]
		personaOtherPhones2,
		// Token: 0x04000765 RID: 1893
		[XmlEnum("persona:Pagers")]
		personaPagers,
		// Token: 0x04000766 RID: 1894
		[XmlEnum("persona:RadioPhones")]
		personaRadioPhones,
		// Token: 0x04000767 RID: 1895
		[XmlEnum("persona:TelexNumbers")]
		personaTelexNumbers,
		// Token: 0x04000768 RID: 1896
		[XmlEnum("persona:WorkFaxes")]
		personaWorkFaxes,
		// Token: 0x04000769 RID: 1897
		[XmlEnum("persona:Emails1")]
		personaEmails1,
		// Token: 0x0400076A RID: 1898
		[XmlEnum("persona:Emails2")]
		personaEmails2,
		// Token: 0x0400076B RID: 1899
		[XmlEnum("persona:Emails3")]
		personaEmails3,
		// Token: 0x0400076C RID: 1900
		[XmlEnum("persona:BusinessHomePages")]
		personaBusinessHomePages,
		// Token: 0x0400076D RID: 1901
		[XmlEnum("persona:School")]
		personaSchool,
		// Token: 0x0400076E RID: 1902
		[XmlEnum("persona:PersonalHomePages")]
		personaPersonalHomePages,
		// Token: 0x0400076F RID: 1903
		[XmlEnum("persona:OfficeLocations")]
		personaOfficeLocations,
		// Token: 0x04000770 RID: 1904
		[XmlEnum("persona:BusinessAddresses")]
		personaBusinessAddresses,
		// Token: 0x04000771 RID: 1905
		[XmlEnum("persona:HomeAddresses")]
		personaHomeAddresses,
		// Token: 0x04000772 RID: 1906
		[XmlEnum("persona:OtherAddresses")]
		personaOtherAddresses,
		// Token: 0x04000773 RID: 1907
		[XmlEnum("persona:Titles")]
		personaTitles,
		// Token: 0x04000774 RID: 1908
		[XmlEnum("persona:Departments")]
		personaDepartments,
		// Token: 0x04000775 RID: 1909
		[XmlEnum("persona:CompanyNames")]
		personaCompanyNames,
		// Token: 0x04000776 RID: 1910
		[XmlEnum("persona:Managers")]
		personaManagers,
		// Token: 0x04000777 RID: 1911
		[XmlEnum("persona:AssistantNames")]
		personaAssistantNames,
		// Token: 0x04000778 RID: 1912
		[XmlEnum("persona:Professions")]
		personaProfessions,
		// Token: 0x04000779 RID: 1913
		[XmlEnum("persona:SpouseNames")]
		personaSpouseNames,
		// Token: 0x0400077A RID: 1914
		[XmlEnum("persona:Hobbies")]
		personaHobbies,
		// Token: 0x0400077B RID: 1915
		[XmlEnum("persona:WeddingAnniversaries")]
		personaWeddingAnniversaries,
		// Token: 0x0400077C RID: 1916
		[XmlEnum("persona:Birthdays")]
		personaBirthdays,
		// Token: 0x0400077D RID: 1917
		[XmlEnum("persona:Children")]
		personaChildren,
		// Token: 0x0400077E RID: 1918
		[XmlEnum("persona:Locations")]
		personaLocations,
		// Token: 0x0400077F RID: 1919
		[XmlEnum("persona:ExtendedProperties")]
		personaExtendedProperties,
		// Token: 0x04000780 RID: 1920
		[XmlEnum("persona:PostalAddress")]
		personaPostalAddress,
		// Token: 0x04000781 RID: 1921
		[XmlEnum("persona:Bodies")]
		personaBodies
	}
}
