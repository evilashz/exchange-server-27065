using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200042D RID: 1069
	public sealed class ToolbarButtons
	{
		// Token: 0x0600260D RID: 9741 RVA: 0x000DC528 File Offset: 0x000DA728
		private ToolbarButtons()
		{
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x000DC530 File Offset: 0x000DA730
		public static ToolbarButton Actions
		{
			get
			{
				return ToolbarButtons.actions;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x000DC537 File Offset: 0x000DA737
		public static ToolbarButton AddressBook
		{
			get
			{
				return ToolbarButtons.addressBook;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x000DC53E File Offset: 0x000DA73E
		public static ToolbarButton AddToContacts
		{
			get
			{
				return ToolbarButtons.addToContacts;
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x000DC545 File Offset: 0x000DA745
		public static ToolbarButton ApprovalApprove
		{
			get
			{
				return ToolbarButtons.approvalApprove;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06002612 RID: 9746 RVA: 0x000DC54C File Offset: 0x000DA74C
		public static ToolbarButton ApprovalApproveMenu
		{
			get
			{
				return ToolbarButtons.approvalApproveMenu;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x000DC553 File Offset: 0x000DA753
		public static ToolbarButton ApprovalReject
		{
			get
			{
				return ToolbarButtons.approvalReject;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x000DC55A File Offset: 0x000DA75A
		public static ToolbarButton ApprovalRejectMenu
		{
			get
			{
				return ToolbarButtons.approvalRejectMenu;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x000DC561 File Offset: 0x000DA761
		public static ToolbarButton ApprovalEditResponse
		{
			get
			{
				return ToolbarButtons.approvalEditResponse;
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002616 RID: 9750 RVA: 0x000DC568 File Offset: 0x000DA768
		public static ToolbarButton ApprovalSendResponseNow
		{
			get
			{
				return ToolbarButtons.approvalSendResponseNow;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x000DC56F File Offset: 0x000DA76F
		public static ToolbarButton AttachFile
		{
			get
			{
				return ToolbarButtons.attachFile;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002618 RID: 9752 RVA: 0x000DC576 File Offset: 0x000DA776
		public static ToolbarButton InsertImage
		{
			get
			{
				return ToolbarButtons.insertImage;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x000DC57D File Offset: 0x000DA77D
		public static ToolbarButton CalendarTitle
		{
			get
			{
				return ToolbarButtons.calendarTitle;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x000DC584 File Offset: 0x000DA784
		public static ToolbarButton CheckMessages
		{
			get
			{
				return ToolbarButtons.checkMessages;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x000DC58B File Offset: 0x000DA78B
		public static ToolbarButton CheckNames
		{
			get
			{
				return ToolbarButtons.checkNames;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x000DC592 File Offset: 0x000DA792
		public static ToolbarButton Compliance
		{
			get
			{
				return ToolbarButtons.compliance;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x0600261D RID: 9757 RVA: 0x000DC599 File Offset: 0x000DA799
		public static ToolbarButton Edit
		{
			get
			{
				return ToolbarButtons.edit;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x000DC5A0 File Offset: 0x000DA7A0
		public static ToolbarButton DayView
		{
			get
			{
				return ToolbarButtons.dayView;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x000DC5A7 File Offset: 0x000DA7A7
		public static ToolbarButton Delete
		{
			get
			{
				return ToolbarButtons.delete;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002620 RID: 9760 RVA: 0x000DC5AE File Offset: 0x000DA7AE
		public static ToolbarButton DeleteTextOnly
		{
			get
			{
				return ToolbarButtons.deleteTextOnly;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x000DC5B5 File Offset: 0x000DA7B5
		public static ToolbarButton DeleteCombo
		{
			get
			{
				return ToolbarButtons.deleteCombo;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x000DC5BC File Offset: 0x000DA7BC
		public static ToolbarButton DeleteInDropDown
		{
			get
			{
				return ToolbarButtons.deleteInDropDown;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x000DC5C3 File Offset: 0x000DA7C3
		public static ToolbarButton DeleteWithText
		{
			get
			{
				return ToolbarButtons.deleteWithText;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x000DC5CA File Offset: 0x000DA7CA
		public static ToolbarButton IgnoreConversation
		{
			get
			{
				return ToolbarButtons.ignoreConversation;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06002625 RID: 9765 RVA: 0x000DC5D1 File Offset: 0x000DA7D1
		public static ToolbarButton CancelIgnoreConversationCombo
		{
			get
			{
				return ToolbarButtons.cancelIgnoreConversationCombo;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x000DC5D8 File Offset: 0x000DA7D8
		public static ToolbarButton CancelIgnoreConversationInDropDown
		{
			get
			{
				return ToolbarButtons.cancelIgnoreConversationInDropDown;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x000DC5DF File Offset: 0x000DA7DF
		public static ToolbarButton DeleteInCancelIgnoreConversationDropDown
		{
			get
			{
				return ToolbarButtons.deleteInCancelIgnoreConversationDropDown;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x000DC5E6 File Offset: 0x000DA7E6
		public static ToolbarButton Flag
		{
			get
			{
				return ToolbarButtons.flag;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x000DC5ED File Offset: 0x000DA7ED
		public static ToolbarButton Categories
		{
			get
			{
				return ToolbarButtons.categories;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x000DC5F4 File Offset: 0x000DA7F4
		public static ToolbarButton SearchInPublicFolder
		{
			get
			{
				return ToolbarButtons.searchInPublicFolder;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x000DC5FB File Offset: 0x000DA7FB
		public static ToolbarButton ChangeSharingPermissions
		{
			get
			{
				return ToolbarButtons.changeSharingPermissions;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x000DC602 File Offset: 0x000DA802
		public static ToolbarButton ShareCalendar
		{
			get
			{
				return ToolbarButtons.shareCalendar;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x000DC609 File Offset: 0x000DA809
		public static ToolbarButton OpenSharedCalendar
		{
			get
			{
				return ToolbarButtons.openSharedCalendar;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x000DC610 File Offset: 0x000DA810
		public static ToolbarButton ShareACalendar
		{
			get
			{
				return ToolbarButtons.shareACalendar;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x000DC617 File Offset: 0x000DA817
		public static ToolbarButton RequestSharedCalendar
		{
			get
			{
				return ToolbarButtons.requestSharedCalendar;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x000DC61E File Offset: 0x000DA81E
		public static ToolbarButton ShareContact
		{
			get
			{
				return ToolbarButtons.shareContact;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x000DC625 File Offset: 0x000DA825
		public static ToolbarButton OpenSharedContact
		{
			get
			{
				return ToolbarButtons.openSharedContact;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x000DC62C File Offset: 0x000DA82C
		public static ToolbarButton ShareThisContact
		{
			get
			{
				return ToolbarButtons.shareThisContact;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x000DC633 File Offset: 0x000DA833
		public static ToolbarButton ShareTask
		{
			get
			{
				return ToolbarButtons.shareTask;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x000DC63A File Offset: 0x000DA83A
		public static ToolbarButton OpenSharedTask
		{
			get
			{
				return ToolbarButtons.openSharedTask;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x000DC641 File Offset: 0x000DA841
		public static ToolbarButton ShareThisTask
		{
			get
			{
				return ToolbarButtons.shareThisTask;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x000DC648 File Offset: 0x000DA848
		public static ToolbarButton Forward
		{
			get
			{
				return ToolbarButtons.forward;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x000DC64F File Offset: 0x000DA84F
		public static ToolbarButton ForwardAsAttachment
		{
			get
			{
				return ToolbarButtons.forwardAsAttachment;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x000DC656 File Offset: 0x000DA856
		public static ToolbarButton ForwardImageOnly
		{
			get
			{
				return ToolbarButtons.forwardImageOnly;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x000DC65D File Offset: 0x000DA85D
		public static ToolbarButton ForwardTextOnly
		{
			get
			{
				return ToolbarButtons.forwardTextOnly;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x000DC664 File Offset: 0x000DA864
		public static ToolbarButton ForwardCombo
		{
			get
			{
				return ToolbarButtons.forwardCombo;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x000DC66B File Offset: 0x000DA86B
		public static ToolbarButton ForwardComboImageOnly
		{
			get
			{
				return ToolbarButtons.forwardComboImageOnly;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x000DC672 File Offset: 0x000DA872
		public static ToolbarButton ForwardInDropDown
		{
			get
			{
				return ToolbarButtons.forwardInDropDown;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x0600263D RID: 9789 RVA: 0x000DC679 File Offset: 0x000DA879
		public static ToolbarButton ForwardAsAttachmentInDropDown
		{
			get
			{
				return ToolbarButtons.forwardAsAttachmentInDropDown;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x000DC680 File Offset: 0x000DA880
		public static ToolbarButton ForwardSms
		{
			get
			{
				return ToolbarButtons.forwardSms;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x000DC687 File Offset: 0x000DA887
		public static ToolbarButton ImportContactList
		{
			get
			{
				return ToolbarButtons.importContactList;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x000DC68E File Offset: 0x000DA88E
		public static ToolbarButton ImportanceHigh
		{
			get
			{
				return ToolbarButtons.importanceHigh;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x000DC695 File Offset: 0x000DA895
		public static ToolbarButton ImportanceLow
		{
			get
			{
				return ToolbarButtons.importanceLow;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06002642 RID: 9794 RVA: 0x000DC69C File Offset: 0x000DA89C
		public static ToolbarButton InsertSignature
		{
			get
			{
				return ToolbarButtons.insertSignature;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x000DC6A3 File Offset: 0x000DA8A3
		public static ToolbarButton MarkComplete
		{
			get
			{
				return ToolbarButtons.markComplete;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x000DC6AA File Offset: 0x000DA8AA
		public static ToolbarButton MarkCompleteNoText
		{
			get
			{
				return ToolbarButtons.markCompleteNoText;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002645 RID: 9797 RVA: 0x000DC6B1 File Offset: 0x000DA8B1
		public static ToolbarButton MeetingAccept
		{
			get
			{
				return ToolbarButtons.meetingAccept;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06002646 RID: 9798 RVA: 0x000DC6B8 File Offset: 0x000DA8B8
		public static ToolbarButton MeetingAcceptMenu
		{
			get
			{
				return ToolbarButtons.meetingAcceptMenu;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x000DC6BF File Offset: 0x000DA8BF
		public static ToolbarButton MeetingDecline
		{
			get
			{
				return ToolbarButtons.meetingDecline;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06002648 RID: 9800 RVA: 0x000DC6C6 File Offset: 0x000DA8C6
		public static ToolbarButton MeetingDeclineMenu
		{
			get
			{
				return ToolbarButtons.meetingDeclineMenu;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x000DC6CD File Offset: 0x000DA8CD
		public static ToolbarButton MeetingTentative
		{
			get
			{
				return ToolbarButtons.meetingTentative;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x0600264A RID: 9802 RVA: 0x000DC6D4 File Offset: 0x000DA8D4
		public static ToolbarButton MeetingTentativeMenu
		{
			get
			{
				return ToolbarButtons.meetingTentativeMenu;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x000DC6DB File Offset: 0x000DA8DB
		public static ToolbarButton MeetingEditResponse
		{
			get
			{
				return ToolbarButtons.meetingEditResponse;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x0600264C RID: 9804 RVA: 0x000DC6E2 File Offset: 0x000DA8E2
		public static ToolbarButton MeetingSendResponseNow
		{
			get
			{
				return ToolbarButtons.meetingSendResponseNow;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x000DC6E9 File Offset: 0x000DA8E9
		public static ToolbarButton MeetingNoResponse
		{
			get
			{
				return ToolbarButtons.meetingNoResponse;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x000DC6F0 File Offset: 0x000DA8F0
		public static ToolbarButton MeetingNoResponseRequired
		{
			get
			{
				return ToolbarButtons.meetingNoResponseRequired;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x000DC6F7 File Offset: 0x000DA8F7
		public static ToolbarButton MeetingOutOfDate
		{
			get
			{
				return ToolbarButtons.meetingOutOfDate;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x000DC6FE File Offset: 0x000DA8FE
		public static ToolbarButton ParentFolder
		{
			get
			{
				return ToolbarButtons.parentFolder;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x000DC705 File Offset: 0x000DA905
		public static ToolbarButton AddToFavorites
		{
			get
			{
				return ToolbarButtons.addToFavorites;
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x000DC70C File Offset: 0x000DA90C
		public static ToolbarButton ShowCalendar
		{
			get
			{
				return ToolbarButtons.showCalendar;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x000DC713 File Offset: 0x000DA913
		public static ToolbarButton MeetingCancelled
		{
			get
			{
				return ToolbarButtons.meetingCancelled;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x000DC71A File Offset: 0x000DA91A
		public static ToolbarButton ResponseAccepted
		{
			get
			{
				return ToolbarButtons.responseAccepted;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x000DC721 File Offset: 0x000DA921
		public static ToolbarButton ResponseTentative
		{
			get
			{
				return ToolbarButtons.responseTentative;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x000DC728 File Offset: 0x000DA928
		public static ToolbarButton ResponseDeclined
		{
			get
			{
				return ToolbarButtons.responseDeclined;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002657 RID: 9815 RVA: 0x000DC72F File Offset: 0x000DA92F
		public static ToolbarButton MessageDetails
		{
			get
			{
				return ToolbarButtons.messageDetails;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x000DC736 File Offset: 0x000DA936
		public static ToolbarButton MessageOptions
		{
			get
			{
				return ToolbarButtons.messageOptions;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x000DC73D File Offset: 0x000DA93D
		public static ToolbarButton MailTips
		{
			get
			{
				return ToolbarButtons.mailTips;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x000DC744 File Offset: 0x000DA944
		public static ToolbarButton MonthView
		{
			get
			{
				return ToolbarButtons.monthView;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x000DC74B File Offset: 0x000DA94B
		public static ToolbarButton MultiLine
		{
			get
			{
				return ToolbarButtons.multiLine;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x000DC752 File Offset: 0x000DA952
		public static ToolbarButton SingleLine
		{
			get
			{
				return ToolbarButtons.singleLine;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x0600265D RID: 9821 RVA: 0x000DC759 File Offset: 0x000DA959
		public static ToolbarButton NewestOnTop
		{
			get
			{
				return ToolbarButtons.newestOnTop;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x000DC760 File Offset: 0x000DA960
		public static ToolbarButton OldestOnTop
		{
			get
			{
				return ToolbarButtons.oldestOnTop;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x000DC767 File Offset: 0x000DA967
		public static ToolbarButton ExpandAll
		{
			get
			{
				return ToolbarButtons.expandAll;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x000DC76E File Offset: 0x000DA96E
		public static ToolbarButton CollapseAll
		{
			get
			{
				return ToolbarButtons.collapseAll;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002661 RID: 9825 RVA: 0x000DC775 File Offset: 0x000DA975
		public static ToolbarButton ShowTree
		{
			get
			{
				return ToolbarButtons.showTree;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x000DC77C File Offset: 0x000DA97C
		public static ToolbarButton ShowFlatList
		{
			get
			{
				return ToolbarButtons.showFlatList;
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06002663 RID: 9827 RVA: 0x000DC783 File Offset: 0x000DA983
		public static ToolbarButton NewMessageCombo
		{
			get
			{
				return ToolbarButtons.newMessageCombo;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x000DC78A File Offset: 0x000DA98A
		public static ToolbarButton NewAppointmentCombo
		{
			get
			{
				return ToolbarButtons.newAppointmentCombo;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x000DC791 File Offset: 0x000DA991
		public static ToolbarButton NewContactCombo
		{
			get
			{
				return ToolbarButtons.newContactCombo;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x000DC798 File Offset: 0x000DA998
		public static ToolbarButton NewTaskCombo
		{
			get
			{
				return ToolbarButtons.newTaskCombo;
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x000DC79F File Offset: 0x000DA99F
		public static ToolbarButton NewAppointment
		{
			get
			{
				return ToolbarButtons.newAppointment;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x000DC7A6 File Offset: 0x000DA9A6
		public static ToolbarButton NewMeetingRequest
		{
			get
			{
				return ToolbarButtons.newMeetingRequest;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002669 RID: 9833 RVA: 0x000DC7AD File Offset: 0x000DA9AD
		public static ToolbarButton NewContactDistributionList
		{
			get
			{
				return ToolbarButtons.newContactDistributionList;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x000DC7B4 File Offset: 0x000DA9B4
		public static ToolbarButton NewContact
		{
			get
			{
				return ToolbarButtons.newContact;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x000DC7BB File Offset: 0x000DA9BB
		public static ToolbarButton NewFolder
		{
			get
			{
				return ToolbarButtons.newFolder;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x000DC7C2 File Offset: 0x000DA9C2
		public static ToolbarButton NewMessage
		{
			get
			{
				return ToolbarButtons.newMessage;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x0600266D RID: 9837 RVA: 0x000DC7C9 File Offset: 0x000DA9C9
		public static ToolbarButton NewMessageToContacts
		{
			get
			{
				return ToolbarButtons.newMessageToContacts;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x000DC7D0 File Offset: 0x000DA9D0
		public static ToolbarButton NewMeetingRequestToContacts
		{
			get
			{
				return ToolbarButtons.newMeetingRequestToContacts;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x000DC7D7 File Offset: 0x000DA9D7
		public static ToolbarButton NewMessageToContact
		{
			get
			{
				return ToolbarButtons.newMessageToContact;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x000DC7DE File Offset: 0x000DA9DE
		public static ToolbarButton NewMessageToDistributionList
		{
			get
			{
				return ToolbarButtons.newMessageToDistributionList;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x000DC7E5 File Offset: 0x000DA9E5
		public static ToolbarButton NewMeetingRequestToContact
		{
			get
			{
				return ToolbarButtons.newMeetingRequestToContact;
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x000DC7EC File Offset: 0x000DA9EC
		public static ToolbarButton NewTask
		{
			get
			{
				return ToolbarButtons.newTask;
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x000DC7F3 File Offset: 0x000DA9F3
		public static ToolbarButton NewWithTaskIcon
		{
			get
			{
				return ToolbarButtons.newWithTaskIcon;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x000DC7FA File Offset: 0x000DA9FA
		public static ToolbarButton NewPost
		{
			get
			{
				return ToolbarButtons.newPost;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x000DC801 File Offset: 0x000DAA01
		public static ToolbarButton NewWithAppointmentIcon
		{
			get
			{
				return ToolbarButtons.newWithAppointmentIcon;
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x000DC808 File Offset: 0x000DAA08
		public static ToolbarButton NewWithPostIcon
		{
			get
			{
				return ToolbarButtons.newWithPostIcon;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x000DC80F File Offset: 0x000DAA0F
		public static ToolbarButton Next
		{
			get
			{
				return ToolbarButtons.next;
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x000DC816 File Offset: 0x000DAA16
		public static ToolbarButton NotJunk
		{
			get
			{
				return ToolbarButtons.notJunk;
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x000DC81D File Offset: 0x000DAA1D
		public static ToolbarButton EmptyFolder
		{
			get
			{
				return ToolbarButtons.emptyFolder;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x000DC824 File Offset: 0x000DAA24
		public static ToolbarButton Post
		{
			get
			{
				return ToolbarButtons.post;
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x000DC82B File Offset: 0x000DAA2B
		public static ToolbarButton Previous
		{
			get
			{
				return ToolbarButtons.previous;
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x000DC832 File Offset: 0x000DAA32
		public static ToolbarButton Options
		{
			get
			{
				return ToolbarButtons.options;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x000DC839 File Offset: 0x000DAA39
		public static ToolbarButton Print
		{
			get
			{
				return ToolbarButtons.print;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x0600267E RID: 9854 RVA: 0x000DC840 File Offset: 0x000DAA40
		public static ToolbarButton PrintCalendar
		{
			get
			{
				return ToolbarButtons.printCalendar;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x000DC847 File Offset: 0x000DAA47
		public static ToolbarButton PrintCalendarLabel
		{
			get
			{
				return ToolbarButtons.printCalendarLabel;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x000DC84E File Offset: 0x000DAA4E
		public static ToolbarButton PrintDailyView
		{
			get
			{
				return ToolbarButtons.printDailyView;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x000DC855 File Offset: 0x000DAA55
		public static ToolbarButton PrintWeeklyView
		{
			get
			{
				return ToolbarButtons.printWeeklyView;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06002682 RID: 9858 RVA: 0x000DC85C File Offset: 0x000DAA5C
		public static ToolbarButton PrintMonthlyView
		{
			get
			{
				return ToolbarButtons.printMonthlyView;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06002683 RID: 9859 RVA: 0x000DC863 File Offset: 0x000DAA63
		public static ToolbarButton ChangeView
		{
			get
			{
				if (UserContextManager.GetUserContext().IsRtl)
				{
					return ToolbarButtons.changeViewRTL;
				}
				return ToolbarButtons.changeView;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06002684 RID: 9860 RVA: 0x000DC87C File Offset: 0x000DAA7C
		public static ToolbarButton UseConversations
		{
			get
			{
				return ToolbarButtons.useConversations;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x000DC883 File Offset: 0x000DAA83
		public static ToolbarButton ConversationOptions
		{
			get
			{
				return ToolbarButtons.conversationOptions;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x000DC88A File Offset: 0x000DAA8A
		public static ToolbarButton ReadingPaneBottom
		{
			get
			{
				return ToolbarButtons.readingPaneBottom;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x000DC891 File Offset: 0x000DAA91
		public static ToolbarButton ReadingPaneRight
		{
			get
			{
				if (UserContextManager.GetUserContext().IsRtl)
				{
					return ToolbarButtons.readingPaneRightRTL;
				}
				return ToolbarButtons.readingPaneRight;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x000DC8AA File Offset: 0x000DAAAA
		public static ToolbarButton ReadingPaneOff
		{
			get
			{
				return ToolbarButtons.readingPaneOff;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06002689 RID: 9865 RVA: 0x000DC8B1 File Offset: 0x000DAAB1
		public static ToolbarButton ReadingPaneOffSwap
		{
			get
			{
				if (UserContextManager.GetUserContext().IsRtl)
				{
					return ToolbarButtons.readingPaneOffSwapRTL;
				}
				return ToolbarButtons.readingPaneOffSwap;
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x000DC8CA File Offset: 0x000DAACA
		public static ToolbarButton ReadingPaneRightSwap
		{
			get
			{
				if (UserContextManager.GetUserContext().IsRtl)
				{
					return ToolbarButtons.readingPaneRightSwapRTL;
				}
				return ToolbarButtons.readingPaneRightSwap;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x000DC8E3 File Offset: 0x000DAAE3
		public static ToolbarButton Reply
		{
			get
			{
				return ToolbarButtons.reply;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x000DC8EA File Offset: 0x000DAAEA
		public static ToolbarButton ReplyImageOnly
		{
			get
			{
				return ToolbarButtons.replyImageOnly;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x000DC8F1 File Offset: 0x000DAAF1
		public static ToolbarButton ReplyTextOnly
		{
			get
			{
				return ToolbarButtons.replyTextOnly;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x000DC8F8 File Offset: 0x000DAAF8
		public static ToolbarButton ReplyCombo
		{
			get
			{
				return ToolbarButtons.replyCombo;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600268F RID: 9871 RVA: 0x000DC8FF File Offset: 0x000DAAFF
		public static ToolbarButton ReplyComboImageOnly
		{
			get
			{
				return ToolbarButtons.replyComboImageOnly;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x000DC906 File Offset: 0x000DAB06
		public static ToolbarButton ReplyInDropDown
		{
			get
			{
				return ToolbarButtons.replyInDropDown;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06002691 RID: 9873 RVA: 0x000DC90D File Offset: 0x000DAB0D
		public static ToolbarButton ReplyByChat
		{
			get
			{
				return ToolbarButtons.replyByChat;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x000DC914 File Offset: 0x000DAB14
		public static ToolbarButton ReplyByPhone
		{
			get
			{
				return ToolbarButtons.replyByPhone;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x000DC91B File Offset: 0x000DAB1B
		public static ToolbarButton ReplyBySms
		{
			get
			{
				return ToolbarButtons.replyBySms;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x000DC922 File Offset: 0x000DAB22
		public static ToolbarButton ReplyAll
		{
			get
			{
				return ToolbarButtons.replyAll;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x000DC929 File Offset: 0x000DAB29
		public static ToolbarButton ReplyAllImageOnly
		{
			get
			{
				return ToolbarButtons.replyAllImageOnly;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x000DC930 File Offset: 0x000DAB30
		public static ToolbarButton ReplyAllTextOnly
		{
			get
			{
				return ToolbarButtons.replyAllTextOnly;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x000DC937 File Offset: 0x000DAB37
		public static ToolbarButton ReplyAllSms
		{
			get
			{
				return ToolbarButtons.replyAllSms;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x000DC93E File Offset: 0x000DAB3E
		public static ToolbarButton ReplySms
		{
			get
			{
				return ToolbarButtons.replySms;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x000DC945 File Offset: 0x000DAB45
		public static ToolbarButton PostReply
		{
			get
			{
				return ToolbarButtons.postReply;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x000DC94C File Offset: 0x000DAB4C
		public static ToolbarButton Reminders
		{
			get
			{
				return ToolbarButtons.reminders;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x000DC953 File Offset: 0x000DAB53
		public static ToolbarButton Save
		{
			get
			{
				return ToolbarButtons.save;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x000DC95A File Offset: 0x000DAB5A
		public static ToolbarButton SaveAndClose
		{
			get
			{
				return ToolbarButtons.saveAndClose;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600269D RID: 9885 RVA: 0x000DC961 File Offset: 0x000DAB61
		public static ToolbarButton SaveAndCloseImageOnly
		{
			get
			{
				return ToolbarButtons.saveAndCloseImageOnly;
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x000DC968 File Offset: 0x000DAB68
		public static ToolbarButton SaveImageOnly
		{
			get
			{
				return ToolbarButtons.saveImageOnly;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600269F RID: 9887 RVA: 0x000DC96F File Offset: 0x000DAB6F
		public static ToolbarButton Send
		{
			get
			{
				return ToolbarButtons.send;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x000DC976 File Offset: 0x000DAB76
		public static ToolbarButton SendAgain
		{
			get
			{
				return ToolbarButtons.sendAgain;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060026A1 RID: 9889 RVA: 0x000DC97D File Offset: 0x000DAB7D
		public static ToolbarButton SendCancelation
		{
			get
			{
				return ToolbarButtons.sendCancelation;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x000DC984 File Offset: 0x000DAB84
		public static ToolbarButton CancelMeeting
		{
			get
			{
				return ToolbarButtons.cancelMeeting;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x000DC98B File Offset: 0x000DAB8B
		public static ToolbarButton SendUpdate
		{
			get
			{
				return ToolbarButtons.sendUpdate;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x000DC992 File Offset: 0x000DAB92
		public static ToolbarButton InviteAttendees
		{
			get
			{
				return ToolbarButtons.inviteAttendees;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060026A5 RID: 9893 RVA: 0x000DC999 File Offset: 0x000DAB99
		public static ToolbarButton CancelInvitation
		{
			get
			{
				return ToolbarButtons.cancelInvitation;
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x000DC9A0 File Offset: 0x000DABA0
		public static ToolbarButton SpellCheck
		{
			get
			{
				return ToolbarButtons.spellCheck;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060026A7 RID: 9895 RVA: 0x000DC9A7 File Offset: 0x000DABA7
		public static ToolbarButton Today
		{
			get
			{
				return ToolbarButtons.today;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060026A8 RID: 9896 RVA: 0x000DC9AE File Offset: 0x000DABAE
		public static ToolbarButton TimeZoneDropDown
		{
			get
			{
				return ToolbarButtons.timeZoneDropDown;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060026A9 RID: 9897 RVA: 0x000DC9B5 File Offset: 0x000DABB5
		public static ToolbarButton WeekView
		{
			get
			{
				return ToolbarButtons.weekView;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060026AA RID: 9898 RVA: 0x000DC9BC File Offset: 0x000DABBC
		public static ToolbarButton WorkWeekView
		{
			get
			{
				return ToolbarButtons.workWeekView;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060026AB RID: 9899 RVA: 0x000DC9C3 File Offset: 0x000DABC3
		public static ToolbarButton Recover
		{
			get
			{
				return ToolbarButtons.recover;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060026AC RID: 9900 RVA: 0x000DC9CA File Offset: 0x000DABCA
		public static ToolbarButton Purge
		{
			get
			{
				return ToolbarButtons.purge;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060026AD RID: 9901 RVA: 0x000DC9D1 File Offset: 0x000DABD1
		public static ToolbarButton Recurrence
		{
			get
			{
				return ToolbarButtons.recurrence;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060026AE RID: 9902 RVA: 0x000DC9D8 File Offset: 0x000DABD8
		public static ToolbarButton RecurrenceImageOnly
		{
			get
			{
				return ToolbarButtons.recurrenceImageOnly;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060026AF RID: 9903 RVA: 0x000DC9DF File Offset: 0x000DABDF
		public static ToolbarButton CreateRule
		{
			get
			{
				return ToolbarButtons.createRule;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060026B0 RID: 9904 RVA: 0x000DC9E6 File Offset: 0x000DABE6
		public static ToolbarButton MessageEncryptContents
		{
			get
			{
				return ToolbarButtons.messageEncryptContents;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060026B1 RID: 9905 RVA: 0x000DC9ED File Offset: 0x000DABED
		public static ToolbarButton MessageDigitalSignature
		{
			get
			{
				return ToolbarButtons.messageDigitalSignature;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x000DC9F4 File Offset: 0x000DABF4
		public static ToolbarButton Move
		{
			get
			{
				return ToolbarButtons.move;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x000DC9FB File Offset: 0x000DABFB
		public static ToolbarButton MoveWithText
		{
			get
			{
				return ToolbarButtons.moveWithText;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x000DCA02 File Offset: 0x000DAC02
		public static ToolbarButton MoveTextOnly
		{
			get
			{
				return ToolbarButtons.moveTextOnly;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060026B5 RID: 9909 RVA: 0x000DCA09 File Offset: 0x000DAC09
		public static ToolbarButton EditTextOnly
		{
			get
			{
				return ToolbarButtons.editTextOnly;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x000DCA10 File Offset: 0x000DAC10
		public static ToolbarButton NewPersonalAutoAttendant
		{
			get
			{
				return ToolbarButtons.newPersonalAutoAttendant;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060026B7 RID: 9911 RVA: 0x000DCA17 File Offset: 0x000DAC17
		public static ToolbarButton MoveUp
		{
			get
			{
				return ToolbarButtons.moveUp;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x000DCA1E File Offset: 0x000DAC1E
		public static ToolbarButton MoveDown
		{
			get
			{
				return ToolbarButtons.moveDown;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x000DCA25 File Offset: 0x000DAC25
		public static ToolbarButton Chat
		{
			get
			{
				return ToolbarButtons.chat;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x000DCA2C File Offset: 0x000DAC2C
		public static ToolbarButton AddToBuddyList
		{
			get
			{
				return ToolbarButtons.addToBuddyList;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x000DCA33 File Offset: 0x000DAC33
		public static ToolbarButton AddToBuddyListWithText
		{
			get
			{
				return ToolbarButtons.addToBuddyListWithText;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x000DCA3A File Offset: 0x000DAC3A
		public static ToolbarButton RemoveFromBuddyList
		{
			get
			{
				return ToolbarButtons.removeFromBuddyList;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x000DCA41 File Offset: 0x000DAC41
		public static ToolbarButton RemoveFromBuddyListWithText
		{
			get
			{
				return ToolbarButtons.removeFromBuddyListWithText;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x000DCA48 File Offset: 0x000DAC48
		public static ToolbarButton SendATextMessage
		{
			get
			{
				return ToolbarButtons.sendATextMessage;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x000DCA4F File Offset: 0x000DAC4F
		public static ToolbarButton NewSms
		{
			get
			{
				return ToolbarButtons.newSms;
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x000DCA56 File Offset: 0x000DAC56
		public static ToolbarButton SendSms
		{
			get
			{
				return ToolbarButtons.sendSms;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x000DCA5D File Offset: 0x000DAC5D
		public static ToolbarButton InviteContact
		{
			get
			{
				return ToolbarButtons.inviteContact;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x000DCA64 File Offset: 0x000DAC64
		public static ToolbarButton FilterCombo
		{
			get
			{
				return ToolbarButtons.filterCombo;
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x000DCA6B File Offset: 0x000DAC6B
		public static ToolbarButton AddThisCalendar
		{
			get
			{
				return ToolbarButtons.addThisCalendar;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x000DCA72 File Offset: 0x000DAC72
		public static ToolbarButton SharingMyCalendar
		{
			get
			{
				return ToolbarButtons.sharingMyCalendar;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x000DCA79 File Offset: 0x000DAC79
		public static ToolbarButton SharingDeclineMenu
		{
			get
			{
				return ToolbarButtons.sharingDeclineMenu;
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x000DCA80 File Offset: 0x000DAC80
		public static ToolbarButton Subscribe
		{
			get
			{
				return ToolbarButtons.subscribe;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060026C7 RID: 9927 RVA: 0x000DCA87 File Offset: 0x000DAC87
		public static ToolbarButton SubscribeToThisCalendar
		{
			get
			{
				return ToolbarButtons.subscribeToThisCalendar;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060026C8 RID: 9928 RVA: 0x000DCA8E File Offset: 0x000DAC8E
		public static ToolbarButton ViewThisCalendar
		{
			get
			{
				return ToolbarButtons.viewThisCalendar;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x000DCA95 File Offset: 0x000DAC95
		public static ToolbarButton MessageNoteInDropDown
		{
			get
			{
				return ToolbarButtons.messageNoteInDropDown;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x000DCA9C File Offset: 0x000DAC9C
		public static ToolbarButton MessageNoteInToolbar
		{
			get
			{
				return ToolbarButtons.messageNoteInToolbar;
			}
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x000DCAA4 File Offset: 0x000DACA4
		private static bool Initialize()
		{
			ToolbarButtons.importanceHigh.SetToggleButtons(new ToolbarButton[]
			{
				ToolbarButtons.importanceLow
			});
			ToolbarButtons.importanceLow.SetToggleButtons(new ToolbarButton[]
			{
				ToolbarButtons.importanceHigh
			});
			ToolbarButtons.multiLine.SetSwapButtons(new ToolbarButton[]
			{
				ToolbarButtons.singleLine
			});
			ToolbarButtons.singleLine.SetSwapButtons(new ToolbarButton[]
			{
				ToolbarButtons.multiLine
			});
			ToolbarButtons.readingPaneRightSwap.SetSwapButtons(new ToolbarButton[]
			{
				ToolbarButtons.readingPaneOffSwap
			});
			ToolbarButtons.readingPaneOffSwap.SetSwapButtons(new ToolbarButton[]
			{
				ToolbarButtons.readingPaneRightSwap
			});
			ToolbarButtons.readingPaneRightSwapRTL.SetSwapButtons(new ToolbarButton[]
			{
				ToolbarButtons.readingPaneOffSwapRTL
			});
			ToolbarButtons.readingPaneOffSwapRTL.SetSwapButtons(new ToolbarButton[]
			{
				ToolbarButtons.readingPaneRightSwapRTL
			});
			ToolbarButtons.cancelInvitation.SetSwapButtons(new ToolbarButton[]
			{
				ToolbarButtons.inviteAttendees,
				ToolbarButtons.save,
				ToolbarButtons.send,
				ToolbarButtons.saveAndClose,
				ToolbarButtons.checkNames
			});
			ToolbarButtons.inviteAttendees.SetSwapButtons(new ToolbarButton[]
			{
				ToolbarButtons.cancelInvitation,
				ToolbarButtons.save,
				ToolbarButtons.send,
				ToolbarButtons.saveAndClose,
				ToolbarButtons.checkNames
			});
			ToolbarButtons.newestOnTop.SetSwapButtons(new ToolbarButton[]
			{
				ToolbarButtons.oldestOnTop
			});
			ToolbarButtons.oldestOnTop.SetSwapButtons(new ToolbarButton[]
			{
				ToolbarButtons.newestOnTop
			});
			ToolbarButtons.dayView.SetToggleButtons(new ToolbarButton[]
			{
				ToolbarButtons.weekView,
				ToolbarButtons.workWeekView,
				ToolbarButtons.monthView
			});
			ToolbarButtons.weekView.SetToggleButtons(new ToolbarButton[]
			{
				ToolbarButtons.dayView,
				ToolbarButtons.workWeekView,
				ToolbarButtons.monthView
			});
			ToolbarButtons.workWeekView.SetToggleButtons(new ToolbarButton[]
			{
				ToolbarButtons.dayView,
				ToolbarButtons.weekView,
				ToolbarButtons.monthView
			});
			ToolbarButtons.monthView.SetToggleButtons(new ToolbarButton[]
			{
				ToolbarButtons.dayView,
				ToolbarButtons.weekView,
				ToolbarButtons.workWeekView
			});
			ToolbarButtons.printDailyView.SetToggleButtons(new ToolbarButton[]
			{
				ToolbarButtons.printWeeklyView,
				ToolbarButtons.printMonthlyView
			});
			ToolbarButtons.printWeeklyView.SetToggleButtons(new ToolbarButton[]
			{
				ToolbarButtons.printDailyView,
				ToolbarButtons.printMonthlyView
			});
			ToolbarButtons.printMonthlyView.SetToggleButtons(new ToolbarButton[]
			{
				ToolbarButtons.printDailyView,
				ToolbarButtons.printWeeklyView
			});
			return true;
		}

		// Token: 0x04001A61 RID: 6753
		private static readonly ToolbarButton actions = new ToolbarButton("actions", ToolbarButtonFlags.Text | ToolbarButtonFlags.CustomMenu, -859543544, ThemeFileId.None);

		// Token: 0x04001A62 RID: 6754
		private static readonly ToolbarButton addressBook = new ToolbarButton("addressbook", ToolbarButtonFlags.Image, 1139489555, ThemeFileId.AddressBook);

		// Token: 0x04001A63 RID: 6755
		private static readonly ToolbarButton addToContacts = new ToolbarButton("addct", ToolbarButtonFlags.Image, 1775424225, ThemeFileId.AddToContacts);

		// Token: 0x04001A64 RID: 6756
		private static readonly ToolbarButton approvalApprove = new ToolbarButton("approve", ToolbarButtonFlags.ImageAndText, -236685197, ThemeFileId.Approve);

		// Token: 0x04001A65 RID: 6757
		private static readonly ToolbarButton approvalApproveMenu = new ToolbarButton("approve", ToolbarButtonFlags.Text | ToolbarButtonFlags.Image | ToolbarButtonFlags.Menu, -236685197, ThemeFileId.Approve);

		// Token: 0x04001A66 RID: 6758
		private static readonly ToolbarButton approvalReject = new ToolbarButton("reject", ToolbarButtonFlags.ImageAndText, -2059328365, ThemeFileId.Reject);

		// Token: 0x04001A67 RID: 6759
		private static readonly ToolbarButton approvalRejectMenu = new ToolbarButton("reject", ToolbarButtonFlags.Text | ToolbarButtonFlags.Image | ToolbarButtonFlags.Menu, -2059328365, ThemeFileId.Reject);

		// Token: 0x04001A68 RID: 6760
		private static readonly ToolbarButton approvalEditResponse = new ToolbarButton("apvedrsp", 1050381195);

		// Token: 0x04001A69 RID: 6761
		private static readonly ToolbarButton approvalSendResponseNow = new ToolbarButton("apvsndrsp", -114654491);

		// Token: 0x04001A6A RID: 6762
		private static readonly ToolbarButton attachFile = new ToolbarButton("attachfile", ToolbarButtonFlags.Image, -1532412163, ThemeFileId.Attachment1);

		// Token: 0x04001A6B RID: 6763
		private static readonly ToolbarButton insertImage = new ToolbarButton("insertimage", ToolbarButtonFlags.Image, 7329360, ThemeFileId.InsertImage);

		// Token: 0x04001A6C RID: 6764
		private static readonly ToolbarButton calendarTitle = new ToolbarButton("noAction", ToolbarButtonFlags.Text | ToolbarButtonFlags.NoAction, -1018465893, ThemeFileId.None);

		// Token: 0x04001A6D RID: 6765
		private static readonly ToolbarButton checkMessages = new ToolbarButton("checkmessages", ToolbarButtonFlags.Image, 1476440846, ThemeFileId.CheckMessages);

		// Token: 0x04001A6E RID: 6766
		private static readonly ToolbarButton checkNames = new ToolbarButton("checknames", ToolbarButtonFlags.Image, -1374765726, ThemeFileId.CheckNames);

		// Token: 0x04001A6F RID: 6767
		private static readonly ToolbarButton compliance = new ToolbarButton("compliance", ToolbarButtonFlags.Image | ToolbarButtonFlags.CustomMenu, -1246480803, ThemeFileId.ComplianceDropDown);

		// Token: 0x04001A70 RID: 6768
		private static readonly ToolbarButton edit = new ToolbarButton("edt", ToolbarButtonFlags.Text, 2119799890, ThemeFileId.None);

		// Token: 0x04001A71 RID: 6769
		private static readonly ToolbarButton dayView = new ToolbarButton("day", ToolbarButtonFlags.Image | ToolbarButtonFlags.Sticky | ToolbarButtonFlags.Radio | ToolbarButtonFlags.Tooltip, -34880007, ThemeFileId.DayView, -509047980);

		// Token: 0x04001A72 RID: 6770
		private static readonly ToolbarButton delete = new ToolbarButton("delete", ToolbarButtonFlags.Image, 1381996313, ThemeFileId.Delete);

		// Token: 0x04001A73 RID: 6771
		private static readonly ToolbarButton deleteTextOnly = new ToolbarButton("delete", ToolbarButtonFlags.Text, 1381996313, ThemeFileId.None);

		// Token: 0x04001A74 RID: 6772
		private static readonly ToolbarButton deleteCombo = new ToolbarButton("delete", ToolbarButtonFlags.Text | ToolbarButtonFlags.ComboMenu, 1381996313, ThemeFileId.None);

		// Token: 0x04001A75 RID: 6773
		private static readonly ToolbarButton deleteInDropDown = new ToolbarButton("deletedrpdwn", ToolbarButtonFlags.ImageAndText, 1381996313, ThemeFileId.Delete);

		// Token: 0x04001A76 RID: 6774
		private static readonly ToolbarButton deleteWithText = new ToolbarButton("delete", ToolbarButtonFlags.Text, 1381996313, ThemeFileId.None);

		// Token: 0x04001A77 RID: 6775
		private static readonly ToolbarButton ignoreConversation = new ToolbarButton("ignoreconversation", ToolbarButtonFlags.ImageAndText, 1486263145, ThemeFileId.IgnoreConversation);

		// Token: 0x04001A78 RID: 6776
		private static readonly ToolbarButton cancelIgnoreConversationCombo = new ToolbarButton("cancelignoreconversation", ToolbarButtonFlags.Image | ToolbarButtonFlags.ComboMenu | ToolbarButtonFlags.AlwaysPressed, -476691185, ThemeFileId.IgnoreConversation);

		// Token: 0x04001A79 RID: 6777
		private static readonly ToolbarButton cancelIgnoreConversationInDropDown = new ToolbarButton("cancelignoreconversationdd", ToolbarButtonFlags.ImageAndText, -476691185, ThemeFileId.IgnoreConversation);

		// Token: 0x04001A7A RID: 6778
		private static readonly ToolbarButton deleteInCancelIgnoreConversationDropDown = new ToolbarButton("deleteigndrpdwn", ToolbarButtonFlags.ImageAndText, 1381996313, ThemeFileId.Delete);

		// Token: 0x04001A7B RID: 6779
		private static readonly ToolbarButton flag = new ToolbarButton("flag", ToolbarButtonFlags.Image | ToolbarButtonFlags.CustomMenu, -1950847676, ThemeFileId.Flag);

		// Token: 0x04001A7C RID: 6780
		private static readonly ToolbarButton categories = new ToolbarButton("cat", ToolbarButtonFlags.Image | ToolbarButtonFlags.CustomMenu, -1941714382, ThemeFileId.Categories);

		// Token: 0x04001A7D RID: 6781
		private static readonly ToolbarButton searchInPublicFolder = new ToolbarButton("searchpf", ToolbarButtonFlags.Text | ToolbarButtonFlags.Image | ToolbarButtonFlags.ImageAfterText, 656259478, ThemeFileId.Expand);

		// Token: 0x04001A7E RID: 6782
		private static readonly ToolbarButton changeSharingPermissions = new ToolbarButton("chgperm", ToolbarButtonFlags.Text | ToolbarButtonFlags.Menu, -82275026, ThemeFileId.None);

		// Token: 0x04001A7F RID: 6783
		private static readonly ToolbarButton shareCalendar = new ToolbarButton("sharecal", ToolbarButtonFlags.Text | ToolbarButtonFlags.CustomMenu, 869186573, ThemeFileId.None);

		// Token: 0x04001A80 RID: 6784
		private static readonly ToolbarButton openSharedCalendar = new ToolbarButton("opnshcal", ToolbarButtonFlags.ImageAndText, 1936872779, ThemeFileId.CalendarSharedTo);

		// Token: 0x04001A81 RID: 6785
		private static readonly ToolbarButton shareACalendar = new ToolbarButton("shcurcal", ToolbarButtonFlags.Text | ToolbarButtonFlags.Menu, 427125723, ThemeFileId.None);

		// Token: 0x04001A82 RID: 6786
		private static readonly ToolbarButton requestSharedCalendar = new ToolbarButton("requestcalendar", ToolbarButtonFlags.Text, -625603472, ThemeFileId.None);

		// Token: 0x04001A83 RID: 6787
		private static readonly ToolbarButton shareContact = new ToolbarButton("sharecnt", ToolbarButtonFlags.Image | ToolbarButtonFlags.Menu, 869186573, ThemeFileId.ContactSharedOut);

		// Token: 0x04001A84 RID: 6788
		private static readonly ToolbarButton openSharedContact = new ToolbarButton("opnshcnt", ToolbarButtonFlags.ImageAndText, 2042364774, ThemeFileId.ContactSharedTo);

		// Token: 0x04001A85 RID: 6789
		private static readonly ToolbarButton shareThisContact = new ToolbarButton("shcurcnt", ToolbarButtonFlags.ImageAndText, -1103633587, ThemeFileId.ContactSharedOut);

		// Token: 0x04001A86 RID: 6790
		private static readonly ToolbarButton shareTask = new ToolbarButton("sharetsk", ToolbarButtonFlags.Image | ToolbarButtonFlags.Menu, 869186573, ThemeFileId.TaskSharedOut);

		// Token: 0x04001A87 RID: 6791
		private static readonly ToolbarButton openSharedTask = new ToolbarButton("opnshtsk", ToolbarButtonFlags.ImageAndText, -1870011529, ThemeFileId.TaskSharedTo);

		// Token: 0x04001A88 RID: 6792
		private static readonly ToolbarButton shareThisTask = new ToolbarButton("shcurtsk", ToolbarButtonFlags.ImageAndText, 1583085584, ThemeFileId.TaskSharedOut);

		// Token: 0x04001A89 RID: 6793
		private static readonly ToolbarButton forward = new ToolbarButton("forward", ToolbarButtonFlags.Text, -1428116961, ThemeFileId.None);

		// Token: 0x04001A8A RID: 6794
		private static readonly ToolbarButton forwardAsAttachment = new ToolbarButton("fwia", ToolbarButtonFlags.Text, -1428116961, ThemeFileId.None);

		// Token: 0x04001A8B RID: 6795
		private static readonly ToolbarButton forwardImageOnly = new ToolbarButton("forward", ToolbarButtonFlags.Image, -1428116961, ThemeFileId.Forward);

		// Token: 0x04001A8C RID: 6796
		private static readonly ToolbarButton forwardTextOnly = new ToolbarButton("forward", ToolbarButtonFlags.Text, -1428116961, ThemeFileId.None);

		// Token: 0x04001A8D RID: 6797
		private static readonly ToolbarButton forwardCombo = new ToolbarButton("forwardcombo", ToolbarButtonFlags.Text | ToolbarButtonFlags.ComboMenu, -1428116961, ThemeFileId.None);

		// Token: 0x04001A8E RID: 6798
		private static readonly ToolbarButton forwardComboImageOnly = new ToolbarButton("forwardcombo", ToolbarButtonFlags.Image | ToolbarButtonFlags.ComboMenu, -1428116961, ThemeFileId.Forward);

		// Token: 0x04001A8F RID: 6799
		private static readonly ToolbarButton forwardInDropDown = new ToolbarButton("forwarddrpdwn", ToolbarButtonFlags.ImageAndText, -1428116961, ThemeFileId.Forward);

		// Token: 0x04001A90 RID: 6800
		private static readonly ToolbarButton forwardAsAttachmentInDropDown = new ToolbarButton("fwiadrpdwn", ToolbarButtonFlags.ImageAndText, 438661106, ThemeFileId.ForwardAsAttachment);

		// Token: 0x04001A91 RID: 6801
		private static readonly ToolbarButton forwardSms = new ToolbarButton("forward", ToolbarButtonFlags.Text, -1428116961, ThemeFileId.None);

		// Token: 0x04001A92 RID: 6802
		private static readonly ToolbarButton importContactList = new ToolbarButton("impcontactlist", ToolbarButtonFlags.Text, 1660557420, ThemeFileId.None);

		// Token: 0x04001A93 RID: 6803
		private static readonly ToolbarButton importanceHigh = new ToolbarButton("imphigh", ToolbarButtonFlags.Image | ToolbarButtonFlags.Sticky, 1535769152, ThemeFileId.ImportanceHigh2);

		// Token: 0x04001A94 RID: 6804
		private static readonly ToolbarButton importanceLow = new ToolbarButton("implow", ToolbarButtonFlags.Image | ToolbarButtonFlags.Sticky, -1341425078, ThemeFileId.ImportanceLow2);

		// Token: 0x04001A95 RID: 6805
		private static readonly ToolbarButton insertSignature = new ToolbarButton("insertsignature", ToolbarButtonFlags.Image, -1909117233, ThemeFileId.Signature);

		// Token: 0x04001A96 RID: 6806
		private static readonly ToolbarButton markComplete = new ToolbarButton("markcomplete", ToolbarButtonFlags.Text, -32068740, ThemeFileId.None);

		// Token: 0x04001A97 RID: 6807
		private static readonly ToolbarButton markCompleteNoText = new ToolbarButton("markcomplete", ToolbarButtonFlags.Image, -32068740, ThemeFileId.MarkComplete);

		// Token: 0x04001A98 RID: 6808
		private static readonly ToolbarButton meetingAccept = new ToolbarButton("accept", ToolbarButtonFlags.Image | ToolbarButtonFlags.BigSize, -475579318, ThemeFileId.MeetingAcceptBig);

		// Token: 0x04001A99 RID: 6809
		private static readonly ToolbarButton meetingAcceptMenu = new ToolbarButton("accept", ToolbarButtonFlags.Image | ToolbarButtonFlags.Menu | ToolbarButtonFlags.BigSize, -475579318, ThemeFileId.MeetingAcceptBig);

		// Token: 0x04001A9A RID: 6810
		private static readonly ToolbarButton meetingDecline = new ToolbarButton("decline", ToolbarButtonFlags.Image | ToolbarButtonFlags.BigSize, -2119870632, ThemeFileId.MeetingDeclineBig);

		// Token: 0x04001A9B RID: 6811
		private static readonly ToolbarButton meetingDeclineMenu = new ToolbarButton("decline", ToolbarButtonFlags.Image | ToolbarButtonFlags.Menu | ToolbarButtonFlags.BigSize, -2119870632, ThemeFileId.MeetingDeclineBig);

		// Token: 0x04001A9C RID: 6812
		private static readonly ToolbarButton meetingTentative = new ToolbarButton("tentative", ToolbarButtonFlags.Image | ToolbarButtonFlags.BigSize, 1797669216, ThemeFileId.MeetingTentativeBig);

		// Token: 0x04001A9D RID: 6813
		private static readonly ToolbarButton meetingTentativeMenu = new ToolbarButton("tentative", ToolbarButtonFlags.Image | ToolbarButtonFlags.Menu | ToolbarButtonFlags.BigSize, 1797669216, ThemeFileId.MeetingTentativeBig);

		// Token: 0x04001A9E RID: 6814
		private static readonly ToolbarButton meetingEditResponse = new ToolbarButton("edrsp", 1050381195);

		// Token: 0x04001A9F RID: 6815
		private static readonly ToolbarButton meetingSendResponseNow = new ToolbarButton("sndrsp", -114654491);

		// Token: 0x04001AA0 RID: 6816
		private static readonly ToolbarButton meetingNoResponse = new ToolbarButton("norsp", -990767046);

		// Token: 0x04001AA1 RID: 6817
		private static readonly ToolbarButton meetingNoResponseRequired = new ToolbarButton("noAction", ToolbarButtonFlags.Image | ToolbarButtonFlags.NoAction | ToolbarButtonFlags.BigSize, -398794157, ThemeFileId.MeetingInfoBig);

		// Token: 0x04001AA2 RID: 6818
		private static readonly ToolbarButton meetingOutOfDate = new ToolbarButton("noAction", ToolbarButtonFlags.Image | ToolbarButtonFlags.NoAction | ToolbarButtonFlags.BigSize, -1694210393, ThemeFileId.MeetingInfoBig);

		// Token: 0x04001AA3 RID: 6819
		private static readonly ToolbarButton parentFolder = new ToolbarButton("up", ToolbarButtonFlags.Text, 1543969273, ThemeFileId.None);

		// Token: 0x04001AA4 RID: 6820
		private static readonly ToolbarButton addToFavorites = new ToolbarButton("addFvTb", ToolbarButtonFlags.Text, -1028120515, ThemeFileId.None);

		// Token: 0x04001AA5 RID: 6821
		private static readonly ToolbarButton showCalendar = new ToolbarButton("showcalendar", ToolbarButtonFlags.Image | ToolbarButtonFlags.BigSize, -373408913, ThemeFileId.MeetingOpenBig);

		// Token: 0x04001AA6 RID: 6822
		private static readonly ToolbarButton meetingCancelled = new ToolbarButton("noAction", ToolbarButtonFlags.Image | ToolbarButtonFlags.NoAction, -1018465893, ThemeFileId.MeetingResponseDecline);

		// Token: 0x04001AA7 RID: 6823
		private static readonly ToolbarButton responseAccepted = new ToolbarButton("noAction", ToolbarButtonFlags.Image | ToolbarButtonFlags.NoAction, -1018465893, ThemeFileId.MeetingResponseAccept);

		// Token: 0x04001AA8 RID: 6824
		private static readonly ToolbarButton responseTentative = new ToolbarButton("noAction", ToolbarButtonFlags.Image | ToolbarButtonFlags.NoAction, -1018465893, ThemeFileId.MeetingResponseTentative);

		// Token: 0x04001AA9 RID: 6825
		private static readonly ToolbarButton responseDeclined = new ToolbarButton("noAction", ToolbarButtonFlags.Image | ToolbarButtonFlags.NoAction, -1018465893, ThemeFileId.MeetingResponseDecline);

		// Token: 0x04001AAA RID: 6826
		private static readonly ToolbarButton messageDetails = new ToolbarButton("messagedetails", ToolbarButtonFlags.Image, 1624231629, ThemeFileId.MessageDetails);

		// Token: 0x04001AAB RID: 6827
		private static readonly ToolbarButton messageOptions = new ToolbarButton("messageoptions", ToolbarButtonFlags.Text, -1086592719, ThemeFileId.None);

		// Token: 0x04001AAC RID: 6828
		private static readonly ToolbarButton mailTips = new ToolbarButton("mailTips", ToolbarButtonFlags.Image | ToolbarButtonFlags.Hidden | ToolbarButtonFlags.CustomMenu, ThemeFileId.Informational);

		// Token: 0x04001AAD RID: 6829
		private static readonly ToolbarButton monthView = new ToolbarButton("month", ToolbarButtonFlags.Image | ToolbarButtonFlags.Sticky | ToolbarButtonFlags.Radio | ToolbarButtonFlags.Tooltip, -1648015055, ThemeFileId.MonthView, 1011436404);

		// Token: 0x04001AAE RID: 6830
		private static readonly ToolbarButton multiLine = new ToolbarButton("ml", ToolbarButtonFlags.Text, 573748959, ThemeFileId.None);

		// Token: 0x04001AAF RID: 6831
		private static readonly ToolbarButton singleLine = new ToolbarButton("sl", ToolbarButtonFlags.Text, -2094330208, ThemeFileId.None);

		// Token: 0x04001AB0 RID: 6832
		private static readonly ToolbarButton newestOnTop = new ToolbarButton("newestOnTop", ToolbarButtonFlags.Text, 1746211700, ThemeFileId.None);

		// Token: 0x04001AB1 RID: 6833
		private static readonly ToolbarButton oldestOnTop = new ToolbarButton("oldestOnTop", ToolbarButtonFlags.Text, 2070168051, ThemeFileId.None);

		// Token: 0x04001AB2 RID: 6834
		private static readonly ToolbarButton expandAll = new ToolbarButton("expandAll", ToolbarButtonFlags.Text, 18372887, ThemeFileId.None);

		// Token: 0x04001AB3 RID: 6835
		private static readonly ToolbarButton collapseAll = new ToolbarButton("collapseAll", ToolbarButtonFlags.Text, -1678460464, ThemeFileId.None);

		// Token: 0x04001AB4 RID: 6836
		private static readonly ToolbarButton showTree = new ToolbarButton("showTree", ToolbarButtonFlags.Text, 1039762825, ThemeFileId.None);

		// Token: 0x04001AB5 RID: 6837
		private static readonly ToolbarButton showFlatList = new ToolbarButton("showFlatList", ToolbarButtonFlags.Text, 1376461660, ThemeFileId.None);

		// Token: 0x04001AB6 RID: 6838
		private static readonly ToolbarButton newMessageCombo = new ToolbarButton("newmsgc", ToolbarButtonFlags.Text | ToolbarButtonFlags.ComboMenu, -1273337860, ThemeFileId.None);

		// Token: 0x04001AB7 RID: 6839
		private static readonly ToolbarButton newAppointmentCombo = new ToolbarButton("newapptc", ToolbarButtonFlags.Text | ToolbarButtonFlags.ComboMenu, -1273337860, ThemeFileId.None);

		// Token: 0x04001AB8 RID: 6840
		private static readonly ToolbarButton newContactCombo = new ToolbarButton("newcc", ToolbarButtonFlags.Text | ToolbarButtonFlags.ComboMenu, -1273337860, ThemeFileId.None);

		// Token: 0x04001AB9 RID: 6841
		private static readonly ToolbarButton newTaskCombo = new ToolbarButton("newtc", ToolbarButtonFlags.Text | ToolbarButtonFlags.ComboMenu, -1273337860, ThemeFileId.None);

		// Token: 0x04001ABA RID: 6842
		private static readonly ToolbarButton newAppointment = new ToolbarButton("newappt", ToolbarButtonFlags.ImageAndText, -1797628885, ThemeFileId.Appointment);

		// Token: 0x04001ABB RID: 6843
		private static readonly ToolbarButton newMeetingRequest = new ToolbarButton("newmtng", ToolbarButtonFlags.ImageAndText, -1560657880, ThemeFileId.MeetingRequest);

		// Token: 0x04001ABC RID: 6844
		private static readonly ToolbarButton newContactDistributionList = new ToolbarButton("newcdl", ToolbarButtonFlags.ImageAndText, -1878983012, ThemeFileId.ContactDL);

		// Token: 0x04001ABD RID: 6845
		private static readonly ToolbarButton newContact = new ToolbarButton("newc", ToolbarButtonFlags.ImageAndText, 447307630, ThemeFileId.Contact);

		// Token: 0x04001ABE RID: 6846
		private static readonly ToolbarButton newFolder = new ToolbarButton("newfolder", ToolbarButtonFlags.ImageAndText, -1690271306, ThemeFileId.Folder2);

		// Token: 0x04001ABF RID: 6847
		private static readonly ToolbarButton newMessage = new ToolbarButton("newmsg", ToolbarButtonFlags.ImageAndText, 360502915, ThemeFileId.EMail3);

		// Token: 0x04001AC0 RID: 6848
		private static readonly ToolbarButton newMessageToContacts = new ToolbarButton("nmsgct", ToolbarButtonFlags.Image, -747517193, ThemeFileId.EMailContact);

		// Token: 0x04001AC1 RID: 6849
		private static readonly ToolbarButton newMeetingRequestToContacts = new ToolbarButton("nmrgct", ToolbarButtonFlags.Image, -1596894910, ThemeFileId.Appointment);

		// Token: 0x04001AC2 RID: 6850
		private static readonly ToolbarButton newMessageToContact = new ToolbarButton("nmsgct", ToolbarButtonFlags.Image, -811703550, ThemeFileId.EMailContact);

		// Token: 0x04001AC3 RID: 6851
		private static readonly ToolbarButton newMessageToDistributionList = new ToolbarButton("nmsgct", ToolbarButtonFlags.Image, -1496031392, ThemeFileId.EMailContact);

		// Token: 0x04001AC4 RID: 6852
		private static readonly ToolbarButton newMeetingRequestToContact = new ToolbarButton("nmrgct", ToolbarButtonFlags.Image, -1803617069, ThemeFileId.Appointment);

		// Token: 0x04001AC5 RID: 6853
		private static readonly ToolbarButton newTask = new ToolbarButton("newtsk", ToolbarButtonFlags.ImageAndText, -1516408339, ThemeFileId.Task);

		// Token: 0x04001AC6 RID: 6854
		private static readonly ToolbarButton newWithTaskIcon = new ToolbarButton("newtsk", ToolbarButtonFlags.Text, -1273337860, ThemeFileId.None);

		// Token: 0x04001AC7 RID: 6855
		private static readonly ToolbarButton newPost = new ToolbarButton("newpst", ToolbarButtonFlags.Text, -735376682, ThemeFileId.None);

		// Token: 0x04001AC8 RID: 6856
		private static readonly ToolbarButton newWithAppointmentIcon = new ToolbarButton("newappt", ToolbarButtonFlags.Text, -1273337860, ThemeFileId.None);

		// Token: 0x04001AC9 RID: 6857
		private static readonly ToolbarButton newWithPostIcon = new ToolbarButton("newpst", ToolbarButtonFlags.Text, -1273337860, ThemeFileId.None);

		// Token: 0x04001ACA RID: 6858
		private static readonly ToolbarButton next = new ToolbarButton("next", ToolbarButtonFlags.Image, -1846382016, ThemeFileId.Next);

		// Token: 0x04001ACB RID: 6859
		private static readonly ToolbarButton notJunk = new ToolbarButton("ntjnk", ToolbarButtonFlags.Text, 856598503, ThemeFileId.None);

		// Token: 0x04001ACC RID: 6860
		private static readonly ToolbarButton emptyFolder = new ToolbarButton("emptyfolder", ToolbarButtonFlags.Text, 445671445, ThemeFileId.None);

		// Token: 0x04001ACD RID: 6861
		private static readonly ToolbarButton post = new ToolbarButton("post", ToolbarButtonFlags.Text, -864298084, ThemeFileId.None);

		// Token: 0x04001ACE RID: 6862
		private static readonly ToolbarButton previous = new ToolbarButton("previous", ToolbarButtonFlags.Image, -577308044, ThemeFileId.Previous);

		// Token: 0x04001ACF RID: 6863
		private static readonly ToolbarButton options = new ToolbarButton("options", ToolbarButtonFlags.Text, -1086592719, ThemeFileId.None);

		// Token: 0x04001AD0 RID: 6864
		private static readonly ToolbarButton print = new ToolbarButton("print", ToolbarButtonFlags.Image, 1588554917, ThemeFileId.Print);

		// Token: 0x04001AD1 RID: 6865
		private static readonly ToolbarButton printCalendar = new ToolbarButton("printcalendarview", ToolbarButtonFlags.Image, 2066252695, ThemeFileId.Print);

		// Token: 0x04001AD2 RID: 6866
		private static readonly ToolbarButton printCalendarLabel = new ToolbarButton("noaction", ToolbarButtonFlags.Text | ToolbarButtonFlags.NoAction, -2048833216, ThemeFileId.None);

		// Token: 0x04001AD3 RID: 6867
		private static readonly ToolbarButton printDailyView = new ToolbarButton("day", ToolbarButtonFlags.Text | ToolbarButtonFlags.Sticky | ToolbarButtonFlags.Radio | ToolbarButtonFlags.Tooltip, -34880007, ThemeFileId.None, -509047980);

		// Token: 0x04001AD4 RID: 6868
		private static readonly ToolbarButton printWeeklyView = new ToolbarButton("week", ToolbarButtonFlags.Text | ToolbarButtonFlags.Sticky | ToolbarButtonFlags.Radio | ToolbarButtonFlags.Tooltip, -867675667, ThemeFileId.None, -382962026);

		// Token: 0x04001AD5 RID: 6869
		private static readonly ToolbarButton printMonthlyView = new ToolbarButton("month", ToolbarButtonFlags.Text | ToolbarButtonFlags.Sticky | ToolbarButtonFlags.Radio | ToolbarButtonFlags.Tooltip, -1648015055, ThemeFileId.None, 1011436404);

		// Token: 0x04001AD6 RID: 6870
		private static readonly ToolbarButton changeView = new ToolbarButton("rps", ToolbarButtonFlags.Text | ToolbarButtonFlags.Menu, 1582260093, ThemeFileId.None, 1898535179);

		// Token: 0x04001AD7 RID: 6871
		private static readonly ToolbarButton changeViewRTL = new ToolbarButton("rps", ToolbarButtonFlags.Text | ToolbarButtonFlags.Menu, 1582260093, ThemeFileId.None, 1898535179);

		// Token: 0x04001AD8 RID: 6872
		private static readonly ToolbarButton useConversations = new ToolbarButton("useConversations", ToolbarButtonFlags.Text, -1107404463, ThemeFileId.None);

		// Token: 0x04001AD9 RID: 6873
		private static readonly ToolbarButton conversationOptions = new ToolbarButton("conversationOptions", ToolbarButtonFlags.Text, 1931186673, ThemeFileId.None);

		// Token: 0x04001ADA RID: 6874
		private static readonly ToolbarButton readingPaneBottom = new ToolbarButton("rpb", ToolbarButtonFlags.Text, 165760971, ThemeFileId.None);

		// Token: 0x04001ADB RID: 6875
		private static readonly ToolbarButton readingPaneRight = new ToolbarButton("rpr", ToolbarButtonFlags.Text, 2064563686, ThemeFileId.None);

		// Token: 0x04001ADC RID: 6876
		private static readonly ToolbarButton readingPaneRightRTL = new ToolbarButton("rpr", ToolbarButtonFlags.Text, 771350883, ThemeFileId.None);

		// Token: 0x04001ADD RID: 6877
		private static readonly ToolbarButton readingPaneOff = new ToolbarButton("rpo", ToolbarButtonFlags.Text, 369254891, ThemeFileId.None);

		// Token: 0x04001ADE RID: 6878
		private static readonly ToolbarButton readingPaneOffSwap = new ToolbarButton("rpo", ToolbarButtonFlags.Image, -1439936760, ThemeFileId.ReadingPaneOff);

		// Token: 0x04001ADF RID: 6879
		private static readonly ToolbarButton readingPaneOffSwapRTL = new ToolbarButton("rpo", ToolbarButtonFlags.Image, -1439936760, ThemeFileId.ReadingPaneOffRTL);

		// Token: 0x04001AE0 RID: 6880
		private static readonly ToolbarButton readingPaneRightSwap = new ToolbarButton("rpr", ToolbarButtonFlags.Image, 576227563, ThemeFileId.ReadingPaneRight);

		// Token: 0x04001AE1 RID: 6881
		private static readonly ToolbarButton readingPaneRightSwapRTL = new ToolbarButton("rpr", ToolbarButtonFlags.Image, 576227563, ThemeFileId.ReadingPaneRightRTL);

		// Token: 0x04001AE2 RID: 6882
		private static readonly ToolbarButton reply = new ToolbarButton("reply", ToolbarButtonFlags.Text, -327372070, ThemeFileId.None);

		// Token: 0x04001AE3 RID: 6883
		private static readonly ToolbarButton replyImageOnly = new ToolbarButton("reply", ToolbarButtonFlags.Image, -327372070, ThemeFileId.Reply);

		// Token: 0x04001AE4 RID: 6884
		private static readonly ToolbarButton replyTextOnly = new ToolbarButton("reply", ToolbarButtonFlags.Text, -327372070, ThemeFileId.None);

		// Token: 0x04001AE5 RID: 6885
		private static readonly ToolbarButton replyCombo = new ToolbarButton("replycombo", ToolbarButtonFlags.Text | ToolbarButtonFlags.ComboMenu, -327372070, ThemeFileId.None);

		// Token: 0x04001AE6 RID: 6886
		private static readonly ToolbarButton replyComboImageOnly = new ToolbarButton("replycombo", ToolbarButtonFlags.Image | ToolbarButtonFlags.ComboMenu, -327372070, ThemeFileId.Reply);

		// Token: 0x04001AE7 RID: 6887
		private static readonly ToolbarButton replyInDropDown = new ToolbarButton("replydrpdwn", ToolbarButtonFlags.ImageAndText, -327372070, ThemeFileId.Reply);

		// Token: 0x04001AE8 RID: 6888
		private static readonly ToolbarButton replyByChat = new ToolbarButton("replybychat", ToolbarButtonFlags.Text, 412026487, ThemeFileId.None);

		// Token: 0x04001AE9 RID: 6889
		private static readonly ToolbarButton replyByPhone = new ToolbarButton("replybyphone", ToolbarButtonFlags.Text, -1314097243, ThemeFileId.None);

		// Token: 0x04001AEA RID: 6890
		private static readonly ToolbarButton replyBySms = new ToolbarButton("replybysms", ToolbarButtonFlags.Text, -838431440, ThemeFileId.None);

		// Token: 0x04001AEB RID: 6891
		private static readonly ToolbarButton replyAll = new ToolbarButton("replyall", ToolbarButtonFlags.Text, 826363927, ThemeFileId.None);

		// Token: 0x04001AEC RID: 6892
		private static readonly ToolbarButton replyAllImageOnly = new ToolbarButton("replyall", ToolbarButtonFlags.Image, 826363927, ThemeFileId.ReplyAll);

		// Token: 0x04001AED RID: 6893
		private static readonly ToolbarButton replyAllTextOnly = new ToolbarButton("replyall", ToolbarButtonFlags.Text, 826363927, ThemeFileId.None);

		// Token: 0x04001AEE RID: 6894
		private static readonly ToolbarButton replyAllSms = new ToolbarButton("replyall", ToolbarButtonFlags.Text, 826363927, ThemeFileId.None);

		// Token: 0x04001AEF RID: 6895
		private static readonly ToolbarButton replySms = new ToolbarButton("reply", ToolbarButtonFlags.Text, -327372070, ThemeFileId.None);

		// Token: 0x04001AF0 RID: 6896
		private static readonly ToolbarButton postReply = new ToolbarButton("postreply", ToolbarButtonFlags.Text, -1780771632, ThemeFileId.None);

		// Token: 0x04001AF1 RID: 6897
		private static readonly ToolbarButton reminders = new ToolbarButton("reminder", ToolbarButtonFlags.Image, -1223560515, ThemeFileId.ReminderSmall);

		// Token: 0x04001AF2 RID: 6898
		private static readonly ToolbarButton save = new ToolbarButton("save", ToolbarButtonFlags.Text, -1966746939, ThemeFileId.None);

		// Token: 0x04001AF3 RID: 6899
		private static readonly ToolbarButton saveAndClose = new ToolbarButton("saveclose", ToolbarButtonFlags.Text, -224317800, ThemeFileId.None);

		// Token: 0x04001AF4 RID: 6900
		private static readonly ToolbarButton saveAndCloseImageOnly = new ToolbarButton("saveclose", ToolbarButtonFlags.Image, -224317800, ThemeFileId.Save);

		// Token: 0x04001AF5 RID: 6901
		private static readonly ToolbarButton saveImageOnly = new ToolbarButton("save", ToolbarButtonFlags.Image, -1966746939, ThemeFileId.Save);

		// Token: 0x04001AF6 RID: 6902
		private static readonly ToolbarButton send = new ToolbarButton("send", ToolbarButtonFlags.Text, -158743924, ThemeFileId.None);

		// Token: 0x04001AF7 RID: 6903
		private static readonly ToolbarButton sendAgain = new ToolbarButton("sendAgain", ToolbarButtonFlags.Text, -1902695064, ThemeFileId.None);

		// Token: 0x04001AF8 RID: 6904
		private static readonly ToolbarButton sendCancelation = new ToolbarButton("sendC", ToolbarButtonFlags.Text, -158743924, ThemeFileId.None);

		// Token: 0x04001AF9 RID: 6905
		private static readonly ToolbarButton cancelMeeting = new ToolbarButton("delete", ToolbarButtonFlags.Image, -240976491, ThemeFileId.CancelInvitation);

		// Token: 0x04001AFA RID: 6906
		private static readonly ToolbarButton sendUpdate = new ToolbarButton("send", ToolbarButtonFlags.Text, 1302559757, ThemeFileId.None);

		// Token: 0x04001AFB RID: 6907
		private static readonly ToolbarButton inviteAttendees = new ToolbarButton("invatnd", ToolbarButtonFlags.Image, -775577546, ThemeFileId.MeetingRequest);

		// Token: 0x04001AFC RID: 6908
		private static readonly ToolbarButton cancelInvitation = new ToolbarButton("caninv", ToolbarButtonFlags.Image, -1442789841, ThemeFileId.CancelInvitation);

		// Token: 0x04001AFD RID: 6909
		private static readonly ToolbarButton spellCheck = new ToolbarButton("spellcheck", ToolbarButtonFlags.Image | ToolbarButtonFlags.ComboMenu, 1570327544, ThemeFileId.Spelling);

		// Token: 0x04001AFE RID: 6910
		private static readonly ToolbarButton today = new ToolbarButton("today", ToolbarButtonFlags.Text | ToolbarButtonFlags.Tooltip, -1483924202, ThemeFileId.None, -1062953366);

		// Token: 0x04001AFF RID: 6911
		private static readonly ToolbarButton timeZoneDropDown = new TimeZoneDropDownToolbarButton();

		// Token: 0x04001B00 RID: 6912
		private static readonly ToolbarButton weekView = new ToolbarButton("week", ToolbarButtonFlags.Image | ToolbarButtonFlags.Sticky | ToolbarButtonFlags.Radio | ToolbarButtonFlags.Tooltip, -867675667, ThemeFileId.WeekView, -382962026);

		// Token: 0x04001B01 RID: 6913
		private static readonly ToolbarButton workWeekView = new ToolbarButton("workweek", ToolbarButtonFlags.Image | ToolbarButtonFlags.Sticky | ToolbarButtonFlags.Radio | ToolbarButtonFlags.Tooltip, 483590582, ThemeFileId.WorkWeekView, 179982437);

		// Token: 0x04001B02 RID: 6914
		private static readonly ToolbarButton recover = new ToolbarButton("recover", ToolbarButtonFlags.Image, 1288772053, ThemeFileId.Recover);

		// Token: 0x04001B03 RID: 6915
		private static readonly ToolbarButton purge = new ToolbarButton("delete", ToolbarButtonFlags.Image, -870504544, ThemeFileId.Delete);

		// Token: 0x04001B04 RID: 6916
		private static readonly ToolbarButton recurrence = new ToolbarButton("rcr", ToolbarButtonFlags.Text, -1955658819, ThemeFileId.None);

		// Token: 0x04001B05 RID: 6917
		private static readonly ToolbarButton recurrenceImageOnly = new ToolbarButton("rcr", ToolbarButtonFlags.Image, -1955658819, ThemeFileId.Recurrence);

		// Token: 0x04001B06 RID: 6918
		private static readonly ToolbarButton createRule = new ToolbarButton("crrul", ToolbarButtonFlags.Image, 1219103799, ThemeFileId.RulesSmall);

		// Token: 0x04001B07 RID: 6919
		private static readonly ToolbarButton messageEncryptContents = new ToolbarButton("msgencypt", ToolbarButtonFlags.Image | ToolbarButtonFlags.Sticky, -155608005, ThemeFileId.MessageEncrypted);

		// Token: 0x04001B08 RID: 6920
		private static readonly ToolbarButton messageDigitalSignature = new ToolbarButton("msgsgnd", ToolbarButtonFlags.Image | ToolbarButtonFlags.Sticky, -1495007479, ThemeFileId.MessageSigned);

		// Token: 0x04001B09 RID: 6921
		private static readonly ToolbarButton move = new ToolbarButton("move", ToolbarButtonFlags.Image | ToolbarButtonFlags.CustomMenu, 1182470434, ThemeFileId.Move);

		// Token: 0x04001B0A RID: 6922
		private static readonly ToolbarButton moveWithText = new ToolbarButton("move", ToolbarButtonFlags.Text | ToolbarButtonFlags.Image | ToolbarButtonFlags.CustomMenu, 1414245993, ThemeFileId.Move);

		// Token: 0x04001B0B RID: 6923
		private static readonly ToolbarButton moveTextOnly = new ToolbarButton("move", ToolbarButtonFlags.Text | ToolbarButtonFlags.CustomMenu, 1414245993, ThemeFileId.None);

		// Token: 0x04001B0C RID: 6924
		private static readonly ToolbarButton editTextOnly = new ToolbarButton("edit", ToolbarButtonFlags.Text, -309370743, ThemeFileId.None);

		// Token: 0x04001B0D RID: 6925
		private static readonly ToolbarButton newPersonalAutoAttendant = new ToolbarButton("newpaa", ToolbarButtonFlags.Text, 699355213, ThemeFileId.None);

		// Token: 0x04001B0E RID: 6926
		private static readonly ToolbarButton moveUp = new ToolbarButton("up", ToolbarButtonFlags.Image, 137938150, ThemeFileId.Previous);

		// Token: 0x04001B0F RID: 6927
		private static readonly ToolbarButton moveDown = new ToolbarButton("dwn", ToolbarButtonFlags.Image, -798660877, ThemeFileId.Next);

		// Token: 0x04001B10 RID: 6928
		private static readonly ToolbarButton chat = new ToolbarButton("chat", ToolbarButtonFlags.Text, -124986716, ThemeFileId.None);

		// Token: 0x04001B11 RID: 6929
		private static readonly ToolbarButton addToBuddyList = new ToolbarButton("ablst", ToolbarButtonFlags.Image, 1457127060, ThemeFileId.AddBuddy);

		// Token: 0x04001B12 RID: 6930
		private static readonly ToolbarButton addToBuddyListWithText = new ToolbarButton("ablst", ToolbarButtonFlags.Text, 1457127060, ThemeFileId.None);

		// Token: 0x04001B13 RID: 6931
		private static readonly ToolbarButton removeFromBuddyList = new ToolbarButton("rmblst", ToolbarButtonFlags.Image, -205408082, ThemeFileId.RemoveBuddy);

		// Token: 0x04001B14 RID: 6932
		private static readonly ToolbarButton removeFromBuddyListWithText = new ToolbarButton("rmblst", ToolbarButtonFlags.Text, -205408082, ThemeFileId.None);

		// Token: 0x04001B15 RID: 6933
		private static readonly ToolbarButton sendATextMessage = new ToolbarButton("sendsms", ToolbarButtonFlags.Image, -843330244, ThemeFileId.Sms);

		// Token: 0x04001B16 RID: 6934
		private static readonly ToolbarButton newSms = new ToolbarButton("newsms", ToolbarButtonFlags.ImageAndText, 1509309420, ThemeFileId.Sms);

		// Token: 0x04001B17 RID: 6935
		private static readonly ToolbarButton sendSms = new ToolbarButton("send", ToolbarButtonFlags.Text, -158743924, ThemeFileId.None);

		// Token: 0x04001B18 RID: 6936
		private static readonly ToolbarButton inviteContact = new ToolbarButton("invt", ToolbarButtonFlags.Text, 923791697, ThemeFileId.None);

		// Token: 0x04001B19 RID: 6937
		private static readonly ToolbarButton filterCombo = new ToolbarButton("fltrc", ToolbarButtonFlags.Text | ToolbarButtonFlags.ComboMenu | ToolbarButtonFlags.CustomMenu, -1623789156, ThemeFileId.None, -645448913);

		// Token: 0x04001B1A RID: 6938
		private static readonly ToolbarButton addThisCalendar = new ToolbarButton("addThisCalendar", ToolbarButtonFlags.Text, 2009299861, ThemeFileId.None);

		// Token: 0x04001B1B RID: 6939
		private static readonly ToolbarButton sharingMyCalendar = new ToolbarButton("shareMyCalendar", ToolbarButtonFlags.Text, -1011044205, ThemeFileId.None);

		// Token: 0x04001B1C RID: 6940
		private static readonly ToolbarButton sharingDeclineMenu = new ToolbarButton("decline", ToolbarButtonFlags.Text | ToolbarButtonFlags.Menu, -788387211, ThemeFileId.None);

		// Token: 0x04001B1D RID: 6941
		private static readonly ToolbarButton subscribe = new ToolbarButton("subscribe", ToolbarButtonFlags.Text, -419851974, ThemeFileId.None);

		// Token: 0x04001B1E RID: 6942
		private static readonly ToolbarButton subscribeToThisCalendar = new ToolbarButton("addThisCalendar", ToolbarButtonFlags.Text, -1230529569, ThemeFileId.None);

		// Token: 0x04001B1F RID: 6943
		private static readonly ToolbarButton viewThisCalendar = new ToolbarButton("viewcal", ToolbarButtonFlags.Text, -142048603, ThemeFileId.None);

		// Token: 0x04001B20 RID: 6944
		private static readonly ToolbarButton messageNoteInDropDown = new ToolbarButton("messagenotedrpdwn", ToolbarButtonFlags.ImageAndText, 1146710980, ThemeFileId.MessageAnnotation);

		// Token: 0x04001B21 RID: 6945
		private static readonly ToolbarButton messageNoteInToolbar = new ToolbarButton("messagenotetoolbar", ToolbarButtonFlags.Image, 1146710980, ThemeFileId.MessageAnnotation);

		// Token: 0x04001B22 RID: 6946
		private static bool isInitialized = ToolbarButtons.Initialize();
	}
}
