using System;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x0200033B RID: 827
	internal enum ActivityId
	{
		// Token: 0x040015F0 RID: 5616
		Min,
		// Token: 0x040015F1 RID: 5617
		ReadingPaneDisplayStart,
		// Token: 0x040015F2 RID: 5618
		ReadingPaneDisplayEnd,
		// Token: 0x040015F3 RID: 5619
		ListViewDisplayStart,
		// Token: 0x040015F4 RID: 5620
		ListViewDisplayEnd,
		// Token: 0x040015F5 RID: 5621
		LinkClicked,
		// Token: 0x040015F6 RID: 5622
		Delete,
		// Token: 0x040015F7 RID: 5623
		Move,
		// Token: 0x040015F8 RID: 5624
		Flag,
		// Token: 0x040015F9 RID: 5625
		FlagComplete,
		// Token: 0x040015FA RID: 5626
		FlagCleared,
		// Token: 0x040015FB RID: 5627
		Categorize,
		// Token: 0x040015FC RID: 5628
		InspectorDisplayStart,
		// Token: 0x040015FD RID: 5629
		InspectorDisplayEnd,
		// Token: 0x040015FE RID: 5630
		MarkAsRead,
		// Token: 0x040015FF RID: 5631
		MarkAsUnread,
		// Token: 0x04001600 RID: 5632
		MarkAsJunk,
		// Token: 0x04001601 RID: 5633
		Print,
		// Token: 0x04001602 RID: 5634
		Logon,
		// Token: 0x04001603 RID: 5635
		Reply,
		// Token: 0x04001604 RID: 5636
		ReplyAll,
		// Token: 0x04001605 RID: 5637
		Forward,
		// Token: 0x04001606 RID: 5638
		ItemSelectStart,
		// Token: 0x04001607 RID: 5639
		ItemSelectEnd,
		// Token: 0x04001608 RID: 5640
		ItemVisibleInListViewStart,
		// Token: 0x04001609 RID: 5641
		ItemVisibleInListViewEnd,
		// Token: 0x0400160A RID: 5642
		WindowActiveStart,
		// Token: 0x0400160B RID: 5643
		WindowActiveEnd,
		// Token: 0x0400160C RID: 5644
		OpenedAnAttachment,
		// Token: 0x0400160D RID: 5645
		PreviewOfAttachmentStarted,
		// Token: 0x0400160E RID: 5646
		PreviewOfAttachmentEnded,
		// Token: 0x0400160F RID: 5647
		AcceptedMeetingRequest,
		// Token: 0x04001610 RID: 5648
		DeclinedMeetingRequest,
		// Token: 0x04001611 RID: 5649
		TentativeMeetingRequest,
		// Token: 0x04001612 RID: 5650
		ProposeNewTime,
		// Token: 0x04001613 RID: 5651
		ModuleChange,
		// Token: 0x04001614 RID: 5652
		NewMessage,
		// Token: 0x04001615 RID: 5653
		MarkMessageAsImportant,
		// Token: 0x04001616 RID: 5654
		MarkMessageAsUnimportant,
		// Token: 0x04001617 RID: 5655
		MarkMessageAsNormal,
		// Token: 0x04001618 RID: 5656
		MessageSelected,
		// Token: 0x04001619 RID: 5657
		MessageSent,
		// Token: 0x0400161A RID: 5658
		DraftCreated,
		// Token: 0x0400161B RID: 5659
		MarkMessageAsClutter,
		// Token: 0x0400161C RID: 5660
		MarkMessageAsNotClutter,
		// Token: 0x0400161D RID: 5661
		IgnoreConversation,
		// Token: 0x0400161E RID: 5662
		PivotChange,
		// Token: 0x0400161F RID: 5663
		ClutterGroupOpened,
		// Token: 0x04001620 RID: 5664
		ClutterGroupClosed,
		// Token: 0x04001621 RID: 5665
		ClutterInfobarExpanded,
		// Token: 0x04001622 RID: 5666
		TurnInferenceOff,
		// Token: 0x04001623 RID: 5667
		TurnInferenceOn,
		// Token: 0x04001624 RID: 5668
		FeatureValueResponse,
		// Token: 0x04001625 RID: 5669
		MessageDelivered,
		// Token: 0x04001626 RID: 5670
		Like,
		// Token: 0x04001627 RID: 5671
		Error,
		// Token: 0x04001628 RID: 5672
		ClutterBarIntroductionAcked,
		// Token: 0x04001629 RID: 5673
		DeleteAllIntroductionAcked,
		// Token: 0x0400162A RID: 5674
		ClutterDeleteAllClicked,
		// Token: 0x0400162B RID: 5675
		ClutterBarIntroductionLearnMoreClicked,
		// Token: 0x0400162C RID: 5676
		DeleteAllIntroductionLearnMoreClicked,
		// Token: 0x0400162D RID: 5677
		ClutterBarIntroductionDisplayed,
		// Token: 0x0400162E RID: 5678
		DeleteAllIntroductionDisplayed,
		// Token: 0x0400162F RID: 5679
		IntroductionPeekControllerCreated,
		// Token: 0x04001630 RID: 5680
		IntroductionPeekShown,
		// Token: 0x04001631 RID: 5681
		IntroductionPeekDismissed,
		// Token: 0x04001632 RID: 5682
		IntroductionLearnMoreClicked,
		// Token: 0x04001633 RID: 5683
		IntroductionTryFeatureClicked,
		// Token: 0x04001634 RID: 5684
		ModernGroupsQuickCompose,
		// Token: 0x04001635 RID: 5685
		ModernGroupsQuickReply,
		// Token: 0x04001636 RID: 5686
		ModernGroupsConversationSelected,
		// Token: 0x04001637 RID: 5687
		SearchSessionStart,
		// Token: 0x04001638 RID: 5688
		SearchSessionEnd,
		// Token: 0x04001639 RID: 5689
		SearchRequestStart,
		// Token: 0x0400163A RID: 5690
		SearchResultsReceived,
		// Token: 0x0400163B RID: 5691
		SearchRefinersReceived,
		// Token: 0x0400163C RID: 5692
		SearchRequestEnd,
		// Token: 0x0400163D RID: 5693
		SurveyResponse,
		// Token: 0x0400163E RID: 5694
		MarkAllItemsAsRead,
		// Token: 0x0400163F RID: 5695
		MoveFromInbox,
		// Token: 0x04001640 RID: 5696
		MoveToInbox,
		// Token: 0x04001641 RID: 5697
		MoveFromClutter,
		// Token: 0x04001642 RID: 5698
		MoveToClutter,
		// Token: 0x04001643 RID: 5699
		DeleteFromInbox,
		// Token: 0x04001644 RID: 5700
		DeleteFromClutter,
		// Token: 0x04001645 RID: 5701
		RemoteSend,
		// Token: 0x04001646 RID: 5702
		CreateCalendarEvent,
		// Token: 0x04001647 RID: 5703
		UpdateCalendarEvent,
		// Token: 0x04001648 RID: 5704
		HelpCenterShown,
		// Token: 0x04001649 RID: 5705
		HelpPanelCreated,
		// Token: 0x0400164A RID: 5706
		HelpPanelShown,
		// Token: 0x0400164B RID: 5707
		HelpPanelClosed,
		// Token: 0x0400164C RID: 5708
		HelpArticleShown,
		// Token: 0x0400164D RID: 5709
		HelpArticleLinkClicked,
		// Token: 0x0400164E RID: 5710
		ClutterNotificationSent,
		// Token: 0x0400164F RID: 5711
		Unlike,
		// Token: 0x04001650 RID: 5712
		ServerLogon,
		// Token: 0x04001651 RID: 5713
		UserPhotoUploaded,
		// Token: 0x04001652 RID: 5714
		CreateAppointment,
		// Token: 0x04001653 RID: 5715
		SendMeetingRequest,
		// Token: 0x04001654 RID: 5716
		CancelMeeting,
		// Token: 0x04001655 RID: 5717
		CreateTask
	}
}
