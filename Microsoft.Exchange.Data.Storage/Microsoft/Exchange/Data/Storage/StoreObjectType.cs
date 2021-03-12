using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001F7 RID: 503
	internal enum StoreObjectType
	{
		// Token: 0x04000E02 RID: 3586
		Unknown,
		// Token: 0x04000E03 RID: 3587
		Folder,
		// Token: 0x04000E04 RID: 3588
		CalendarFolder,
		// Token: 0x04000E05 RID: 3589
		ContactsFolder,
		// Token: 0x04000E06 RID: 3590
		TasksFolder,
		// Token: 0x04000E07 RID: 3591
		NotesFolder,
		// Token: 0x04000E08 RID: 3592
		JournalFolder,
		// Token: 0x04000E09 RID: 3593
		SearchFolder,
		// Token: 0x04000E0A RID: 3594
		OutlookSearchFolder,
		// Token: 0x04000E0B RID: 3595
		Message,
		// Token: 0x04000E0C RID: 3596
		MeetingMessage,
		// Token: 0x04000E0D RID: 3597
		MeetingRequest,
		// Token: 0x04000E0E RID: 3598
		MeetingResponse,
		// Token: 0x04000E0F RID: 3599
		MeetingCancellation,
		// Token: 0x04000E10 RID: 3600
		ConflictMessage,
		// Token: 0x04000E11 RID: 3601
		CalendarItem,
		// Token: 0x04000E12 RID: 3602
		CalendarItemOccurrence,
		// Token: 0x04000E13 RID: 3603
		Contact,
		// Token: 0x04000E14 RID: 3604
		DistributionList,
		// Token: 0x04000E15 RID: 3605
		Task,
		// Token: 0x04000E16 RID: 3606
		TaskRequest,
		// Token: 0x04000E17 RID: 3607
		Note,
		// Token: 0x04000E18 RID: 3608
		Post,
		// Token: 0x04000E19 RID: 3609
		Report,
		// Token: 0x04000E1A RID: 3610
		MeetingForwardNotification,
		// Token: 0x04000E1B RID: 3611
		ConversationActionItem,
		// Token: 0x04000E1C RID: 3612
		ApprovalRequest,
		// Token: 0x04000E1D RID: 3613
		ApprovalReply,
		// Token: 0x04000E1E RID: 3614
		SharingMessage,
		// Token: 0x04000E1F RID: 3615
		RightsManagedMessage,
		// Token: 0x04000E20 RID: 3616
		MeetingInquiryMessage,
		// Token: 0x04000E21 RID: 3617
		OofMessage,
		// Token: 0x04000E22 RID: 3618
		ExternalOofMessage,
		// Token: 0x04000E23 RID: 3619
		Place,
		// Token: 0x04000E24 RID: 3620
		CalendarGroup,
		// Token: 0x04000E25 RID: 3621
		CalendarGroupEntry,
		// Token: 0x04000E26 RID: 3622
		UserPhoto,
		// Token: 0x04000E27 RID: 3623
		UserPhotoPreview,
		// Token: 0x04000E28 RID: 3624
		ShortcutFolder,
		// Token: 0x04000E29 RID: 3625
		Person,
		// Token: 0x04000E2A RID: 3626
		FavoriteFolderEntry,
		// Token: 0x04000E2B RID: 3627
		ShortcutMessage,
		// Token: 0x04000E2C RID: 3628
		TaskGroup,
		// Token: 0x04000E2D RID: 3629
		TaskGroupEntry,
		// Token: 0x04000E2E RID: 3630
		PushNotificationSubscription,
		// Token: 0x04000E2F RID: 3631
		MailboxAssociationGroup,
		// Token: 0x04000E30 RID: 3632
		MailboxAssociationUser,
		// Token: 0x04000E31 RID: 3633
		CalendarItemSeries,
		// Token: 0x04000E32 RID: 3634
		GroupMailboxRequestMessage,
		// Token: 0x04000E33 RID: 3635
		OutlookServiceSubscription,
		// Token: 0x04000E34 RID: 3636
		EventTimeBasedInboxReminder,
		// Token: 0x04000E35 RID: 3637
		QuickCaptureReminder,
		// Token: 0x04000E36 RID: 3638
		ReminderMessage,
		// Token: 0x04000E37 RID: 3639
		Configuration,
		// Token: 0x04000E38 RID: 3640
		MeetingRequestSeries,
		// Token: 0x04000E39 RID: 3641
		MeetingResponseSeries,
		// Token: 0x04000E3A RID: 3642
		MeetingCancellationSeries,
		// Token: 0x04000E3B RID: 3643
		MeetingForwardNotificationSeries,
		// Token: 0x04000E3C RID: 3644
		ParkedMeetingMessage,
		// Token: 0x04000E3D RID: 3645
		HierarchySyncMetadata,
		// Token: 0x04000E3E RID: 3646
		Mailbox = 255
	}
}
