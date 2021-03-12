using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000266 RID: 614
	public enum ThemeFileId
	{
		// Token: 0x04000E27 RID: 3623
		[ThemeFileInfo]
		None,
		// Token: 0x04000E28 RID: 3624
		[ThemeFileInfo("premium.css")]
		PremiumCss,
		// Token: 0x04000E29 RID: 3625
		[ThemeFileInfo("owafont.css", ThemeFileInfoFlags.Resource)]
		OwaFontCss,
		// Token: 0x04000E2A RID: 3626
		[ThemeFileInfo("csssprites.css")]
		CssSpritesCss,
		// Token: 0x04000E2B RID: 3627
		[ThemeFileInfo("csssprites2.css")]
		CssSpritesCss2,
		// Token: 0x04000E2C RID: 3628
		[ThemeFileInfo("basic.css")]
		BasicCss,
		// Token: 0x04000E2D RID: 3629
		[ThemeFileInfo("options.css", ThemeFileInfoFlags.Resource)]
		OptionsCss,
		// Token: 0x04000E2E RID: 3630
		[ThemeFileInfo("logon.css", ThemeFileInfoFlags.Resource)]
		LogonCss,
		// Token: 0x04000E2F RID: 3631
		[ThemeFileInfo("error2.css", ThemeFileInfoFlags.Resource)]
		Error2Css,
		// Token: 0x04000E30 RID: 3632
		[ThemeFileInfo("webready.css", ThemeFileInfoFlags.Resource)]
		WebReadyCss,
		// Token: 0x04000E31 RID: 3633
		[ThemeFileInfo("editorstyles.css", ThemeFileInfoFlags.Resource)]
		EditorCss,
		// Token: 0x04000E32 RID: 3634
		[ThemeFileInfo("printcalendar.css", ThemeFileInfoFlags.Resource)]
		PrintCalendarCss,
		// Token: 0x04000E33 RID: 3635
		[ThemeFileInfo("csssprites.png", ThemeFileInfoFlags.LooseImage)]
		CssSpritesPng,
		// Token: 0x04000E34 RID: 3636
		[ThemeFileInfo("gradientv.png", ThemeFileInfoFlags.LooseImage)]
		GradientVerticalPng,
		// Token: 0x04000E35 RID: 3637
		[ThemeFileInfo("gradienth.png", ThemeFileInfoFlags.LooseImage)]
		GradientHorizentalPng,
		// Token: 0x04000E36 RID: 3638
		[ThemeFileInfo("hmask.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		HorizontalGradientMask,
		// Token: 0x04000E37 RID: 3639
		[ThemeFileInfo("hmaskrtl.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		HorizontalGradientMaskRtl,
		// Token: 0x04000E38 RID: 3640
		[ThemeFileInfo("vmask.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		VerticalGradientMask,
		// Token: 0x04000E39 RID: 3641
		[ThemeFileInfo("cobrandgradient.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		CobrandGradientMask,
		// Token: 0x04000E3A RID: 3642
		[ThemeFileInfo("cobrandgradient-rtl.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		CobrandGradientMaskRtl,
		// Token: 0x04000E3B RID: 3643
		[ThemeFileInfo("logoowa.png")]
		PremiumLogoOwa,
		// Token: 0x04000E3C RID: 3644
		[ThemeFileInfo("logob.gif", ThemeFileInfoFlags.LooseImage)]
		BasicLogo,
		// Token: 0x04000E3D RID: 3645
		[ThemeFileInfo("nothemepreview.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		NoThemePreview,
		// Token: 0x04000E3E RID: 3646
		[ThemeFileInfo("themepreview.png", ThemeFileInfoFlags.LooseImage)]
		ThemePreview,
		// Token: 0x04000E3F RID: 3647
		[ThemeFileInfo("paaheader.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		PAAHeader,
		// Token: 0x04000E40 RID: 3648
		[ThemeFileInfo("about.gif", ThemeFileInfoFlags.PhaseII)]
		AboutOwa,
		// Token: 0x04000E41 RID: 3649
		[ThemeFileInfo("accsblty.gif", ThemeFileInfoFlags.PhaseII)]
		Accessibility,
		// Token: 0x04000E42 RID: 3650
		[ThemeFileInfo("addct.gif")]
		AddToContacts,
		// Token: 0x04000E43 RID: 3651
		[ThemeFileInfo("addrbook.png")]
		AddressBook,
		// Token: 0x04000E44 RID: 3652
		[ThemeFileInfo("addrbk2.png")]
		AddressBook2,
		// Token: 0x04000E45 RID: 3653
		[ThemeFileInfo("addricn.gif", ThemeFileInfoFlags.PhaseII)]
		AddressBookIcon,
		// Token: 0x04000E46 RID: 3654
		[ThemeFileInfo("anropts.gif", ThemeFileInfoFlags.LooseImage)]
		AnrOptions,
		// Token: 0x04000E47 RID: 3655
		[ThemeFileInfo("appt.gif")]
		Appointment,
		// Token: 0x04000E48 RID: 3656
		[ThemeFileInfo("attach.png")]
		Attachment1,
		// Token: 0x04000E49 RID: 3657
		[ThemeFileInfo("attch.png")]
		Attachment2,
		// Token: 0x04000E4A RID: 3658
		[ThemeFileInfo("bcnclsrch.gif", ThemeFileInfoFlags.LooseImage)]
		BasicCancelSearch,
		// Token: 0x04000E4B RID: 3659
		[ThemeFileInfo("bsearch.gif", ThemeFileInfoFlags.LooseImage)]
		BasicSearch,
		// Token: 0x04000E4C RID: 3660
		[ThemeFileInfo("bsa.gif", ThemeFileInfoFlags.LooseImage)]
		BasicSortAscending,
		// Token: 0x04000E4D RID: 3661
		[ThemeFileInfo("bsd.gif", ThemeFileInfoFlags.LooseImage)]
		BasicSortDescending,
		// Token: 0x04000E4E RID: 3662
		[ThemeFileInfo("btbattach.png", ThemeFileInfoFlags.LooseImage)]
		BasicToolbarAttach,
		// Token: 0x04000E4F RID: 3663
		[ThemeFileInfo("btbcheckname.gif", ThemeFileInfoFlags.LooseImage)]
		BasicToolbarCheckNames,
		// Token: 0x04000E50 RID: 3664
		[ThemeFileInfo("bdi_inf.gif")]
		ButtonDialogInfo,
		// Token: 0x04000E51 RID: 3665
		[ThemeFileInfo("bdi_qst.gif")]
		ButtonDialogQuestion,
		// Token: 0x04000E52 RID: 3666
		[ThemeFileInfo("bdi_wrn.gif")]
		Warning,
		// Token: 0x04000E53 RID: 3667
		[ThemeFileInfo("closedlg.gif")]
		CloseDialog,
		// Token: 0x04000E54 RID: 3668
		[ThemeFileInfo("cal-attachment.gif")]
		Attachment3,
		// Token: 0x04000E55 RID: 3669
		[ThemeFileInfo("print-cal-attach.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		PrintAttachment3,
		// Token: 0x04000E56 RID: 3670
		[ThemeFileInfo("cal-attachment-w.gif")]
		Attachment3White,
		// Token: 0x04000E57 RID: 3671
		[ThemeFileInfo("print-cal-attach-w.gif", ThemeFileInfoFlags.Resource)]
		PrintAttachment3White,
		// Token: 0x04000E58 RID: 3672
		[ThemeFileInfo("DayView16.png", ThemeFileInfoFlags.PhaseII)]
		DayView,
		// Token: 0x04000E59 RID: 3673
		[ThemeFileInfo("cal-down.gif")]
		CalendarDown,
		// Token: 0x04000E5A RID: 3674
		[ThemeFileInfo("cal-excep.gif")]
		Exception,
		// Token: 0x04000E5B RID: 3675
		[ThemeFileInfo("print-cal-excep.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		PrintException,
		// Token: 0x04000E5C RID: 3676
		[ThemeFileInfo("cal-excep-w.gif")]
		ExceptionWhite,
		// Token: 0x04000E5D RID: 3677
		[ThemeFileInfo("print-cal-excep-w.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		PrintExceptionWhite,
		// Token: 0x04000E5E RID: 3678
		[ThemeFileInfo("MonthView16.png", ThemeFileInfoFlags.PhaseII)]
		MonthView,
		// Token: 0x04000E5F RID: 3679
		[ThemeFileInfo("cal-mnupriv.gif", ThemeFileInfoFlags.PhaseII)]
		MenuPrivate,
		// Token: 0x04000E60 RID: 3680
		[ThemeFileInfo("cal-next.png", ThemeFileInfoFlags.PhaseII)]
		CalendarNext,
		// Token: 0x04000E61 RID: 3681
		[ThemeFileInfo("cal-prev.png", ThemeFileInfoFlags.PhaseII)]
		CalendarPrevious,
		// Token: 0x04000E62 RID: 3682
		[ThemeFileInfo("cal-priv.gif")]
		Private,
		// Token: 0x04000E63 RID: 3683
		[ThemeFileInfo("print-cal-priv.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		PrintPrivate,
		// Token: 0x04000E64 RID: 3684
		[ThemeFileInfo("cal-priv-w.gif")]
		PrivateWhite,
		// Token: 0x04000E65 RID: 3685
		[ThemeFileInfo("print-cal-priv-w.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		PrintPrivateWhite,
		// Token: 0x04000E66 RID: 3686
		[ThemeFileInfo("cal-recur.gif", ThemeFileInfoFlags.PhaseII)]
		RecurringAppointment,
		// Token: 0x04000E67 RID: 3687
		[ThemeFileInfo("print-cal-recur.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		PrintRecurringAppointment,
		// Token: 0x04000E68 RID: 3688
		[ThemeFileInfo("cal-recur-w.gif")]
		RecurringAppointmentWhite,
		// Token: 0x04000E69 RID: 3689
		[ThemeFileInfo("print-cal-recur-w.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		PrintRecurringAppointmentWhite,
		// Token: 0x04000E6A RID: 3690
		[ThemeFileInfo("cal-sel.gif")]
		SelectedCalendar,
		// Token: 0x04000E6B RID: 3691
		[ThemeFileInfo("cal-up.gif")]
		CalendarUp,
		// Token: 0x04000E6C RID: 3692
		[ThemeFileInfo("WeekView16.png", ThemeFileInfoFlags.PhaseII)]
		WeekView,
		// Token: 0x04000E6D RID: 3693
		[ThemeFileInfo("WorkWeekView16.png", ThemeFileInfoFlags.PhaseII)]
		WorkWeekView,
		// Token: 0x04000E6E RID: 3694
		[ThemeFileInfo("calendar.gif", ThemeFileInfoFlags.LooseImage)]
		Calendar,
		// Token: 0x04000E6F RID: 3695
		[ThemeFileInfo("calwrkwk.gif", ThemeFileInfoFlags.PhaseII)]
		CalendarWorkWeek,
		// Token: 0x04000E70 RID: 3696
		[ThemeFileInfo("canmtg.gif", ThemeFileInfoFlags.PhaseII)]
		CancelInvitation,
		// Token: 0x04000E71 RID: 3697
		[ThemeFileInfo("changepass.gif", ThemeFileInfoFlags.PhaseII)]
		ChangePassword,
		// Token: 0x04000E72 RID: 3698
		[ThemeFileInfo("checkname.gif")]
		CheckNames,
		// Token: 0x04000E73 RID: 3699
		[ThemeFileInfo("clear.gif")]
		Clear,
		// Token: 0x04000E74 RID: 3700
		[ThemeFileInfo("clear1x1.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		Clear1x1,
		// Token: 0x04000E75 RID: 3701
		[ThemeFileInfo("cllps.gif")]
		Collapse,
		// Token: 0x04000E76 RID: 3702
		[ThemeFileInfo("clndr.png")]
		Calendar2,
		// Token: 0x04000E77 RID: 3703
		[ThemeFileInfo("clndrsmll.gif")]
		Calendar2Small,
		// Token: 0x04000E78 RID: 3704
		[ThemeFileInfo("close.gif", ThemeFileInfoFlags.LooseImage)]
		Close,
		// Token: 0x04000E79 RID: 3705
		[ThemeFileInfo("conflict.gif")]
		Conflict,
		// Token: 0x04000E7A RID: 3706
		[ThemeFileInfo("cntct.png")]
		Contact2,
		// Token: 0x04000E7B RID: 3707
		[ThemeFileInfo("cntctsmll.gif")]
		Contact2Small,
		// Token: 0x04000E7C RID: 3708
		[ThemeFileInfo("contact.gif")]
		Contact,
		// Token: 0x04000E7D RID: 3709
		[ThemeFileInfo("contactdl.gif")]
		ContactDL,
		// Token: 0x04000E7E RID: 3710
		[ThemeFileInfo("crvbtmlt.gif")]
		CornerBottomLeft,
		// Token: 0x04000E7F RID: 3711
		[ThemeFileInfo("crvbtmrt.gif")]
		CornerBottomRight,
		// Token: 0x04000E80 RID: 3712
		[ThemeFileInfo("crvtplt.gif")]
		CornerTopLeft,
		// Token: 0x04000E81 RID: 3713
		[ThemeFileInfo("crvtprt.gif")]
		CornerTopRight,
		// Token: 0x04000E82 RID: 3714
		[ThemeFileInfo("datebook.gif", ThemeFileInfoFlags.PhaseII)]
		DateBook,
		// Token: 0x04000E83 RID: 3715
		[ThemeFileInfo("delayed_dlvr.gif")]
		DelayedDelivery,
		// Token: 0x04000E84 RID: 3716
		[ThemeFileInfo("delete.png")]
		Delete,
		// Token: 0x04000E85 RID: 3717
		[ThemeFileInfo("deleted.gif")]
		Deleted,
		// Token: 0x04000E86 RID: 3718
		[ThemeFileInfo("bdeleted.gif", ThemeFileInfoFlags.LooseImage)]
		BasicDeleted,
		// Token: 0x04000E87 RID: 3719
		[ThemeFileInfo("bddeleted.gif", ThemeFileInfoFlags.LooseImage)]
		BasicDarkDeleted,
		// Token: 0x04000E88 RID: 3720
		[ThemeFileInfo("down-b.gif", ThemeFileInfoFlags.PhaseII)]
		DownButton,
		// Token: 0x04000E89 RID: 3721
		[ThemeFileInfo("drafts.png")]
		Drafts,
		// Token: 0x04000E8A RID: 3722
		[ThemeFileInfo("drparw.gif")]
		DownButton2,
		// Token: 0x04000E8B RID: 3723
		[ThemeFileInfo("dwn.png")]
		DownButton3,
		// Token: 0x04000E8C RID: 3724
		[ThemeFileInfo("email-lg.gif", ThemeFileInfoFlags.PhaseII)]
		EMailLarge,
		// Token: 0x04000E8D RID: 3725
		[ThemeFileInfo("email-xlg.gif", ThemeFileInfoFlags.PhaseII)]
		EMailExtraLarge,
		// Token: 0x04000E8E RID: 3726
		[ThemeFileInfo("email.png")]
		EMail,
		// Token: 0x04000E8F RID: 3727
		[ThemeFileInfo("emailcontact.gif")]
		EMailContact,
		// Token: 0x04000E90 RID: 3728
		[ThemeFileInfo("eml.png")]
		EMail2,
		// Token: 0x04000E91 RID: 3729
		[ThemeFileInfo("emlsmll.png")]
		EMail2Small,
		// Token: 0x04000E92 RID: 3730
		[ThemeFileInfo("emptydel.gif", ThemeFileInfoFlags.PhaseII)]
		EmptyDeletedItems,
		// Token: 0x04000E93 RID: 3731
		[ThemeFileInfo("warn.png")]
		Error,
		// Token: 0x04000E94 RID: 3732
		[ThemeFileInfo("errorBG.gif", ThemeFileInfoFlags.LooseImage)]
		ErrorBackground,
		// Token: 0x04000E95 RID: 3733
		[ThemeFileInfo("evt-break.gif", ThemeFileInfoFlags.PhaseII)]
		EventBreak,
		// Token: 0x04000E96 RID: 3734
		[ThemeFileInfo("exclaim.gif")]
		Exclaim,
		// Token: 0x04000E97 RID: 3735
		[ThemeFileInfo("expnd.gif")]
		Expand,
		// Token: 0x04000E98 RID: 3736
		[ThemeFileInfo("fb-tnt.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		Tentative2,
		// Token: 0x04000E99 RID: 3737
		[ThemeFileInfo("tntgtr.gif", ThemeFileInfoFlags.LooseImage)]
		TentativeBasicGutter,
		// Token: 0x04000E9A RID: 3738
		[ThemeFileInfo("tntevnt.gif", ThemeFileInfoFlags.LooseImage)]
		TentativeBasicEvent,
		// Token: 0x04000E9B RID: 3739
		[ThemeFileInfo("tntnnwrk.gif", ThemeFileInfoFlags.LooseImage)]
		TentativeBasicNonWorkingHours,
		// Token: 0x04000E9C RID: 3740
		[ThemeFileInfo("tntwrk.gif", ThemeFileInfoFlags.LooseImage)]
		TentativeBasicWorkingHours,
		// Token: 0x04000E9D RID: 3741
		[ThemeFileInfo("fldr.png")]
		Folder,
		// Token: 0x04000E9E RID: 3742
		[ThemeFileInfo("fldr-opn.gif")]
		FolderOpen,
		// Token: 0x04000E9F RID: 3743
		[ThemeFileInfo("forecolor.png", ThemeFileInfoFlags.PhaseII)]
		ForeColor,
		// Token: 0x04000EA0 RID: 3744
		[ThemeFileInfo("forward.gif")]
		Forward,
		// Token: 0x04000EA1 RID: 3745
		[ThemeFileInfo("forwardsms.gif")]
		ForwardSms,
		// Token: 0x04000EA2 RID: 3746
		[ThemeFileInfo("fp.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		FirstPage,
		// Token: 0x04000EA3 RID: 3747
		[ThemeFileInfo("fpg.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		FirstPageGray,
		// Token: 0x04000EA4 RID: 3748
		[ThemeFileInfo("globe.gif", ThemeFileInfoFlags.PhaseII)]
		Globe,
		// Token: 0x04000EA5 RID: 3749
		[ThemeFileInfo("go2.gif", ThemeFileInfoFlags.LooseImage)]
		Go2,
		// Token: 0x04000EA6 RID: 3750
		[ThemeFileInfo("help.png")]
		Help,
		// Token: 0x04000EA7 RID: 3751
		[ThemeFileInfo("home.gif")]
		HomePhone,
		// Token: 0x04000EA8 RID: 3752
		[ThemeFileInfo("icon-doc.gif")]
		Document,
		// Token: 0x04000EA9 RID: 3753
		[ThemeFileInfo("doc.gif")]
		FlatDocument,
		// Token: 0x04000EAA RID: 3754
		[ThemeFileInfo("icon-flag.gif")]
		Flag,
		// Token: 0x04000EAB RID: 3755
		[ThemeFileInfo("msg-rd.png")]
		MessageRead,
		// Token: 0x04000EAC RID: 3756
		[ThemeFileInfo("msg-unrd.png")]
		MessageUnread,
		// Token: 0x04000EAD RID: 3757
		[ThemeFileInfo("icon-wrn.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		WarningIcon,
		// Token: 0x04000EAE RID: 3758
		[ThemeFileInfo("ih.gif")]
		ImportanceHigh,
		// Token: 0x04000EAF RID: 3759
		[ThemeFileInfo("il.gif")]
		ImportanceLow,
		// Token: 0x04000EB0 RID: 3760
		[ThemeFileInfo("imphigh.gif")]
		ImportanceHigh2,
		// Token: 0x04000EB1 RID: 3761
		[ThemeFileInfo("implow.gif")]
		ImportanceLow2,
		// Token: 0x04000EB2 RID: 3762
		[ThemeFileInfo("impnorm.gif")]
		ImportanceNormal,
		// Token: 0x04000EB3 RID: 3763
		[ThemeFileInfo("inbox.png")]
		Inbox,
		// Token: 0x04000EB4 RID: 3764
		[ThemeFileInfo("info.gif")]
		Informational,
		// Token: 0x04000EB5 RID: 3765
		[ThemeFileInfo("ignorecnv.png")]
		IgnoreConversation,
		// Token: 0x04000EB6 RID: 3766
		[ThemeFileInfo("journal.gif")]
		Journal,
		// Token: 0x04000EB7 RID: 3767
		[ThemeFileInfo("junkemail.gif")]
		JunkEMail,
		// Token: 0x04000EB8 RID: 3768
		[ThemeFileInfo("junkemailbig.gif", ThemeFileInfoFlags.PhaseII)]
		JunkEmailBig,
		// Token: 0x04000EB9 RID: 3769
		[ThemeFileInfo("lgntopl.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonTopLeft,
		// Token: 0x04000EBA RID: 3770
		[ThemeFileInfo("lgntopm.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonTopMiddle,
		// Token: 0x04000EBB RID: 3771
		[ThemeFileInfo("lgntopr.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonTopRight,
		// Token: 0x04000EBC RID: 3772
		[ThemeFileInfo("lgnbotl.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonBottomLeft,
		// Token: 0x04000EBD RID: 3773
		[ThemeFileInfo("lgnbotm.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonBottomMiddle,
		// Token: 0x04000EBE RID: 3774
		[ThemeFileInfo("lgnbotr.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonBottomRight,
		// Token: 0x04000EBF RID: 3775
		[ThemeFileInfo("lgnexlogo.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonExchangeLogo,
		// Token: 0x04000EC0 RID: 3776
		[ThemeFileInfo("lp.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LastPage,
		// Token: 0x04000EC1 RID: 3777
		[ThemeFileInfo("lpg.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LastPageGray,
		// Token: 0x04000EC2 RID: 3778
		[ThemeFileInfo("lvdivide.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		ListViewDivider,
		// Token: 0x04000EC3 RID: 3779
		[ThemeFileInfo("mrkcmplt.gif")]
		MarkComplete,
		// Token: 0x04000EC4 RID: 3780
		[ThemeFileInfo("minus.png")]
		Minus,
		// Token: 0x04000EC5 RID: 3781
		[ThemeFileInfo("ml.gif")]
		MultiLine,
		// Token: 0x04000EC6 RID: 3782
		[ThemeFileInfo("mobile.gif")]
		MobilePhone,
		// Token: 0x04000EC7 RID: 3783
		[ThemeFileInfo("move.gif")]
		Move,
		// Token: 0x04000EC8 RID: 3784
		[ThemeFileInfo("move-folder.gif")]
		MoveFolder,
		// Token: 0x04000EC9 RID: 3785
		[ThemeFileInfo("msgdtls.gif")]
		MessageDetails,
		// Token: 0x04000ECA RID: 3786
		[ThemeFileInfo("mtg-accept.png")]
		MeetingAccept,
		// Token: 0x04000ECB RID: 3787
		[ThemeFileInfo("mtg-decline.png")]
		MeetingDecline,
		// Token: 0x04000ECC RID: 3788
		[ThemeFileInfo("mtg-tent.png")]
		MeetingTentative,
		// Token: 0x04000ECD RID: 3789
		[ThemeFileInfo("mtg-accept-big.png")]
		MeetingAcceptBig,
		// Token: 0x04000ECE RID: 3790
		[ThemeFileInfo("mtg-decline-big.png")]
		MeetingDeclineBig,
		// Token: 0x04000ECF RID: 3791
		[ThemeFileInfo("mtg-tentative-big.png")]
		MeetingTentativeBig,
		// Token: 0x04000ED0 RID: 3792
		[ThemeFileInfo("mtg-info-big.png")]
		MeetingInfoBig,
		// Token: 0x04000ED1 RID: 3793
		[ThemeFileInfo("mtg-open-big.png")]
		MeetingOpenBig,
		// Token: 0x04000ED2 RID: 3794
		[ThemeFileInfo("mtg-delete-big.png")]
		MeetingDeleteBig,
		// Token: 0x04000ED3 RID: 3795
		[ThemeFileInfo("mtgreq-cancel.gif")]
		MeetingRequestCancel,
		// Token: 0x04000ED4 RID: 3796
		[ThemeFileInfo("mtgreq.gif")]
		MeetingRequest,
		// Token: 0x04000ED5 RID: 3797
		[ThemeFileInfo("mtgrsp-accept.gif")]
		MeetingResponseAccept,
		// Token: 0x04000ED6 RID: 3798
		[ThemeFileInfo("mtgrsp-decline.gif")]
		MeetingResponseDecline,
		// Token: 0x04000ED7 RID: 3799
		[ThemeFileInfo("mtgrsp-tent.gif")]
		MeetingResponseTentative,
		// Token: 0x04000ED8 RID: 3800
		[ThemeFileInfo("noterr.gif")]
		NotificationError,
		// Token: 0x04000ED9 RID: 3801
		[ThemeFileInfo("newemail.png")]
		EMail3,
		// Token: 0x04000EDA RID: 3802
		[ThemeFileInfo("fax-unrd.gif")]
		FaxMessage,
		// Token: 0x04000EDB RID: 3803
		[ThemeFileInfo("newfldr.gif")]
		Folder2,
		// Token: 0x04000EDC RID: 3804
		[ThemeFileInfo("next.gif")]
		Next,
		// Token: 0x04000EDD RID: 3805
		[ThemeFileInfo("nm.gif")]
		NextArrow,
		// Token: 0x04000EDE RID: 3806
		[ThemeFileInfo("nodata.gif")]
		NoData,
		// Token: 0x04000EDF RID: 3807
		[ThemeFileInfo("notes.png")]
		Notes,
		// Token: 0x04000EE0 RID: 3808
		[ThemeFileInfo("np.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		NextPage,
		// Token: 0x04000EE1 RID: 3809
		[ThemeFileInfo("npg.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		NextPageGray,
		// Token: 0x04000EE2 RID: 3810
		[ThemeFileInfo("obdv.gif", ThemeFileInfoFlags.LooseImage)]
		OptionsBarDivider,
		// Token: 0x04000EE3 RID: 3811
		[ThemeFileInfo("oof.gif", ThemeFileInfoFlags.PhaseII)]
		Oof,
		// Token: 0x04000EE4 RID: 3812
		[ThemeFileInfo("opt.gif", ThemeFileInfoFlags.PhaseII)]
		OptionalAttendee,
		// Token: 0x04000EE5 RID: 3813
		[ThemeFileInfo("options.gif")]
		Options,
		// Token: 0x04000EE6 RID: 3814
		[ThemeFileInfo("org-up-arrow.gif")]
		OrganizationUpArrow,
		// Token: 0x04000EE7 RID: 3815
		[ThemeFileInfo("org.gif", ThemeFileInfoFlags.PhaseII)]
		Organizer,
		// Token: 0x04000EE8 RID: 3816
		[ThemeFileInfo("outbox.gif")]
		Outbox,
		// Token: 0x04000EE9 RID: 3817
		[ThemeFileInfo("pda.gif", ThemeFileInfoFlags.PhaseII)]
		Pda,
		// Token: 0x04000EEA RID: 3818
		[ThemeFileInfo("perfmon.gif", ThemeFileInfoFlags.PhaseII)]
		PerformanceMonitor,
		// Token: 0x04000EEB RID: 3819
		[ThemeFileInfo("progress.gif", ThemeFileInfoFlags.LooseImage)]
		Progress,
		// Token: 0x04000EEC RID: 3820
		[ThemeFileInfo("pgrs-sm.gif", ThemeFileInfoFlags.LooseImage)]
		ProgressSmall,
		// Token: 0x04000EED RID: 3821
		[ThemeFileInfo("pl_ont.gif")]
		PlayOnTelephone,
		// Token: 0x04000EEE RID: 3822
		[ThemeFileInfo("plus.png")]
		Plus,
		// Token: 0x04000EEF RID: 3823
		[ThemeFileInfo("pm.gif")]
		PreviousArrow,
		// Token: 0x04000EF0 RID: 3824
		[ThemeFileInfo("pnrsz.gif", ThemeFileInfoFlags.LooseImage)]
		NavigationResize,
		// Token: 0x04000EF1 RID: 3825
		[ThemeFileInfo("post.gif")]
		Post,
		// Token: 0x04000EF2 RID: 3826
		[ThemeFileInfo("postRpActive.png")]
		PostReplyActive,
		// Token: 0x04000EF3 RID: 3827
		[ThemeFileInfo("postRpGhost.png")]
		PostReplyGhost,
		// Token: 0x04000EF4 RID: 3828
		[ThemeFileInfo("postRpDisabled.png")]
		PostReplyDisabled,
		// Token: 0x04000EF5 RID: 3829
		[ThemeFileInfo("postRpGhostDisabled.png")]
		PostReplyGhostDisabled,
		// Token: 0x04000EF6 RID: 3830
		[ThemeFileInfo("pp.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		PreviousPage,
		// Token: 0x04000EF7 RID: 3831
		[ThemeFileInfo("ppg.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		PreviousPageGray,
		// Token: 0x04000EF8 RID: 3832
		[ThemeFileInfo("prev.gif")]
		Previous,
		// Token: 0x04000EF9 RID: 3833
		[ThemeFileInfo("priph.gif", ThemeFileInfoFlags.PhaseII)]
		PrimaryPhone,
		// Token: 0x04000EFA RID: 3834
		[ThemeFileInfo("print.png")]
		Print,
		// Token: 0x04000EFB RID: 3835
		[ThemeFileInfo("red-ul.gif", ThemeFileInfoFlags.Resource)]
		RedUnderline,
		// Token: 0x04000EFC RID: 3836
		[ThemeFileInfo("rem-sm.png")]
		ReminderSmall,
		// Token: 0x04000EFD RID: 3837
		[ThemeFileInfo("reply.gif")]
		Reply,
		// Token: 0x04000EFE RID: 3838
		[ThemeFileInfo("replyActive.png")]
		ReplyActiveIcon,
		// Token: 0x04000EFF RID: 3839
		[ThemeFileInfo("replyGhost.png", ThemeFileInfoFlags.PhaseII)]
		ReplyGhostIcon,
		// Token: 0x04000F00 RID: 3840
		[ThemeFileInfo("replyDisabled.png")]
		ReplyDisabledIcon,
		// Token: 0x04000F01 RID: 3841
		[ThemeFileInfo("replyGhostDisabled.png", ThemeFileInfoFlags.PhaseII)]
		ReplyGhostDisabledIcon,
		// Token: 0x04000F02 RID: 3842
		[ThemeFileInfo("replyall.gif")]
		ReplyAll,
		// Token: 0x04000F03 RID: 3843
		[ThemeFileInfo("replyAllActive.png")]
		ReplyAllActiveIcon,
		// Token: 0x04000F04 RID: 3844
		[ThemeFileInfo("replyAllDisabled.png")]
		ReplyAllDisabledIcon,
		// Token: 0x04000F05 RID: 3845
		[ThemeFileInfo("replyAllGhost.png", ThemeFileInfoFlags.PhaseII)]
		ReplyAllGhostIcon,
		// Token: 0x04000F06 RID: 3846
		[ThemeFileInfo("replyAllGhostDisabled.png", ThemeFileInfoFlags.PhaseII)]
		ReplyAllGhostDisabledIcon,
		// Token: 0x04000F07 RID: 3847
		[ThemeFileInfo("forwardActive.png")]
		ForwardActiveIcon,
		// Token: 0x04000F08 RID: 3848
		[ThemeFileInfo("forwardGhost.png", ThemeFileInfoFlags.PhaseII)]
		ForwardGhostIcon,
		// Token: 0x04000F09 RID: 3849
		[ThemeFileInfo("forwardDisabled.png")]
		ForwardDisabledIcon,
		// Token: 0x04000F0A RID: 3850
		[ThemeFileInfo("forwardGhostDisabled.png", ThemeFileInfoFlags.PhaseII)]
		ForwardGhostDisabledIcon,
		// Token: 0x04000F0B RID: 3851
		[ThemeFileInfo("replyallsms.gif")]
		ReplyAllSms,
		// Token: 0x04000F0C RID: 3852
		[ThemeFileInfo("replyphone.gif")]
		ReplyByPhone,
		// Token: 0x04000F0D RID: 3853
		[ThemeFileInfo("replysms.gif")]
		ReplyBySms,
		// Token: 0x04000F0E RID: 3854
		[ThemeFileInfo("reqd.gif", ThemeFileInfoFlags.PhaseII)]
		RequiredAttendee,
		// Token: 0x04000F0F RID: 3855
		[ThemeFileInfo("res.gif", ThemeFileInfoFlags.PhaseII)]
		ResourceAttendee,
		// Token: 0x04000F10 RID: 3856
		[ThemeFileInfo("root.png")]
		Root,
		// Token: 0x04000F11 RID: 3857
		[ThemeFileInfo("rpb.gif")]
		ReadingPaneBottom,
		// Token: 0x04000F12 RID: 3858
		[ThemeFileInfo("rpb_rtl.gif")]
		ReadingPaneBottomRTL,
		// Token: 0x04000F13 RID: 3859
		[ThemeFileInfo("rpo.gif")]
		ReadingPaneOff,
		// Token: 0x04000F14 RID: 3860
		[ThemeFileInfo("rpo_rtl.gif")]
		ReadingPaneOffRTL,
		// Token: 0x04000F15 RID: 3861
		[ThemeFileInfo("rpr.gif")]
		ReadingPaneRight,
		// Token: 0x04000F16 RID: 3862
		[ThemeFileInfo("rpr_rtl.gif")]
		ReadingPaneRightRTL,
		// Token: 0x04000F17 RID: 3863
		[ThemeFileInfo("hdiv-l.png")]
		HorizontalDividerImageLeft,
		// Token: 0x04000F18 RID: 3864
		[ThemeFileInfo("hdiv-r.png")]
		HorizontalDividerImageRight,
		// Token: 0x04000F19 RID: 3865
		[ThemeFileInfo("hdiv-t.png", ThemeFileInfoFlags.LooseImage)]
		HorizontalDividerImageTile,
		// Token: 0x04000F1A RID: 3866
		[ThemeFileInfo("sa.gif")]
		SortAscending,
		// Token: 0x04000F1B RID: 3867
		[ThemeFileInfo("save.png")]
		Save,
		// Token: 0x04000F1C RID: 3868
		[ThemeFileInfo("buttonspanelsave.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		OptionsSave,
		// Token: 0x04000F1D RID: 3869
		[ThemeFileInfo("sd.gif")]
		SortDescending,
		// Token: 0x04000F1E RID: 3870
		[ThemeFileInfo("send.gif")]
		Send,
		// Token: 0x04000F1F RID: 3871
		[ThemeFileInfo("sentitems.png")]
		SentItems,
		// Token: 0x04000F20 RID: 3872
		[ThemeFileInfo("showcalendar.gif")]
		ShowCalendar,
		// Token: 0x04000F21 RID: 3873
		[ThemeFileInfo("addcalendar.png")]
		AddCalendar,
		// Token: 0x04000F22 RID: 3874
		[ThemeFileInfo("sig-lg.gif", ThemeFileInfoFlags.PhaseII)]
		SignatureLarge,
		// Token: 0x04000F23 RID: 3875
		[ThemeFileInfo("sig.gif")]
		Signature,
		// Token: 0x04000F24 RID: 3876
		[ThemeFileInfo("sl.gif", ThemeFileInfoFlags.PhaseII)]
		SingleLine,
		// Token: 0x04000F25 RID: 3877
		[ThemeFileInfo("spelling.gif")]
		Spelling,
		// Token: 0x04000F26 RID: 3878
		[ThemeFileInfo("sp-dict.gif", ThemeFileInfoFlags.PhaseII | ThemeFileInfoFlags.Resource)]
		SpellingDictionary,
		// Token: 0x04000F27 RID: 3879
		[ThemeFileInfo("sr.png")]
		CheckMessages,
		// Token: 0x04000F28 RID: 3880
		[ThemeFileInfo("tsk.gif")]
		Task,
		// Token: 0x04000F29 RID: 3881
		[ThemeFileInfo("tbdv.gif")]
		ToolbarDivider,
		// Token: 0x04000F2A RID: 3882
		[ThemeFileInfo("divider.gif", ThemeFileInfoFlags.LooseImage)]
		FolderStatusBarDividerPremium,
		// Token: 0x04000F2B RID: 3883
		[ThemeFileInfo("themes.gif", ThemeFileInfoFlags.PhaseII)]
		Themes,
		// Token: 0x04000F2C RID: 3884
		[ThemeFileInfo("tntv.gif", ThemeFileInfoFlags.PhaseII)]
		Tentative,
		// Token: 0x04000F2D RID: 3885
		[ThemeFileInfo("free.png", ThemeFileInfoFlags.PhaseII)]
		Available,
		// Token: 0x04000F2E RID: 3886
		[ThemeFileInfo("outofoffice.png", ThemeFileInfoFlags.PhaseII)]
		OutOfOffice,
		// Token: 0x04000F2F RID: 3887
		[ThemeFileInfo("cal-busy.png", ThemeFileInfoFlags.PhaseII)]
		Busy,
		// Token: 0x04000F30 RID: 3888
		[ThemeFileInfo("nodata70.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		Nodata70,
		// Token: 0x04000F31 RID: 3889
		[ThemeFileInfo("fb-tnt70.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		Tentative70,
		// Token: 0x04000F32 RID: 3890
		[ThemeFileInfo("work.gif", ThemeFileInfoFlags.PhaseII)]
		WorkPhone,
		// Token: 0x04000F33 RID: 3891
		[ThemeFileInfo("wunderbar_resize.png")]
		PrimaryNavigationResize,
		// Token: 0x04000F34 RID: 3892
		[ThemeFileInfo("wunderbar_top.gif")]
		PrimaryNavigationTop,
		// Token: 0x04000F35 RID: 3893
		[ThemeFileInfo("vm-unrd.gif")]
		VoiceMessage,
		// Token: 0x04000F36 RID: 3894
		[ThemeFileInfo("play.gif")]
		VoiceMessageAttachmentPlay,
		// Token: 0x04000F37 RID: 3895
		[ThemeFileInfo("stop.gif")]
		VoiceMessageAttachmentStop,
		// Token: 0x04000F38 RID: 3896
		[ThemeFileInfo("search.png")]
		Search,
		// Token: 0x04000F39 RID: 3897
		[ThemeFileInfo("copy.gif")]
		Copy,
		// Token: 0x04000F3A RID: 3898
		[ThemeFileInfo("copy-folder.gif")]
		CopyFolder,
		// Token: 0x04000F3B RID: 3899
		[ThemeFileInfo("copy-to-folder.gif")]
		CopyToFolder,
		// Token: 0x04000F3C RID: 3900
		[ThemeFileInfo("opdlvrp.gif")]
		OpenDeliveryReport,
		// Token: 0x04000F3D RID: 3901
		[ThemeFileInfo("notify.wav")]
		NotificationSound,
		// Token: 0x04000F3E RID: 3902
		[ThemeFileInfo("evtfrom.gif")]
		EventFrom,
		// Token: 0x04000F3F RID: 3903
		[ThemeFileInfo("evtfrom-w.gif")]
		EventFromWhite,
		// Token: 0x04000F40 RID: 3904
		[ThemeFileInfo("evtto.gif")]
		EventTo,
		// Token: 0x04000F41 RID: 3905
		[ThemeFileInfo("evtto-w.gif")]
		EventToWhite,
		// Token: 0x04000F42 RID: 3906
		[ThemeFileInfo("cal-up-hl.gif")]
		CalendarUpHighlighted,
		// Token: 0x04000F43 RID: 3907
		[ThemeFileInfo("cal-down-hl.gif")]
		CalendarDownHighlighted,
		// Token: 0x04000F44 RID: 3908
		[ThemeFileInfo("addrbook-disabled.png")]
		AddressBookDisabled,
		// Token: 0x04000F45 RID: 3909
		[ThemeFileInfo("mtgrcpnt.gif", ThemeFileInfoFlags.LooseImage)]
		MeetingRecipients,
		// Token: 0x04000F46 RID: 3910
		[ThemeFileInfo("recur.gif")]
		Recurrence,
		// Token: 0x04000F47 RID: 3911
		[ThemeFileInfo("up.gif")]
		Up,
		// Token: 0x04000F48 RID: 3912
		[ThemeFileInfo("fldr-web.gif", ThemeFileInfoFlags.PhaseII)]
		WebFolder,
		// Token: 0x04000F49 RID: 3913
		[ThemeFileInfo("mnu-r.gif")]
		FlyoutMenuRight,
		// Token: 0x04000F4A RID: 3914
		[ThemeFileInfo("mnu-l.gif")]
		FlyoutMenuLeft,
		// Token: 0x04000F4B RID: 3915
		[ThemeFileInfo("dl-user.gif")]
		DistributionListUser,
		// Token: 0x04000F4C RID: 3916
		[ThemeFileInfo("dl-other.gif")]
		DistributionListOther,
		// Token: 0x04000F4D RID: 3917
		[ThemeFileInfo("pkrarwl.gif")]
		PickerArrowLtr,
		// Token: 0x04000F4E RID: 3918
		[ThemeFileInfo("pkrarwr.gif")]
		PickerArrowRtl,
		// Token: 0x04000F4F RID: 3919
		[ThemeFileInfo("dl.gif", ThemeFileInfoFlags.PhaseII)]
		AddressBookDL,
		// Token: 0x04000F50 RID: 3920
		[ThemeFileInfo("fax.gif", ThemeFileInfoFlags.PhaseII)]
		Fax,
		// Token: 0x04000F51 RID: 3921
		[ThemeFileInfo("up-arrw.gif")]
		UpArrow,
		// Token: 0x04000F52 RID: 3922
		[ThemeFileInfo("msgopts.gif")]
		MessageOptions,
		// Token: 0x04000F53 RID: 3923
		[ThemeFileInfo("rt.gif")]
		RightArrow,
		// Token: 0x04000F54 RID: 3924
		[ThemeFileInfo("lt.gif")]
		LeftArrow,
		// Token: 0x04000F55 RID: 3925
		[ThemeFileInfo("dc-html.gif")]
		HtmlDocument,
		// Token: 0x04000F56 RID: 3926
		[ThemeFileInfo("cal-rsc-perm.gif", ThemeFileInfoFlags.PhaseII)]
		CalendarResourcePermissions,
		// Token: 0x04000F57 RID: 3927
		[ThemeFileInfo("srchfdr.png")]
		SearchFolderIcon,
		// Token: 0x04000F58 RID: 3928
		[ThemeFileInfo("cal-proc.gif", ThemeFileInfoFlags.PhaseII)]
		AutoCalProcessing,
		// Token: 0x04000F59 RID: 3929
		[ThemeFileInfo("flg-empty.gif")]
		FlagEmpty,
		// Token: 0x04000F5A RID: 3930
		[ThemeFileInfo("flg-compl.gif")]
		FlagComplete,
		// Token: 0x04000F5B RID: 3931
		[ThemeFileInfo("flg-sender.gif")]
		FlagSender,
		// Token: 0x04000F5C RID: 3932
		[ThemeFileInfo("flg-dsbl.gif")]
		FlagDisabled,
		// Token: 0x04000F5D RID: 3933
		[ThemeFileInfo("flg-compl-dsbl.gif")]
		FlagCompleteDisabled,
		// Token: 0x04000F5E RID: 3934
		[ThemeFileInfo("cut.gif")]
		EditorCut,
		// Token: 0x04000F5F RID: 3935
		[ThemeFileInfo("paste.gif")]
		EditorPaste,
		// Token: 0x04000F60 RID: 3936
		[ThemeFileInfo("undo.gif")]
		EditorUndo,
		// Token: 0x04000F61 RID: 3937
		[ThemeFileInfo("dn_mblgn.gif")]
		ExplicitLogonDownArrow,
		// Token: 0x04000F62 RID: 3938
		[ThemeFileInfo("yellowshield.gif")]
		YellowShield,
		// Token: 0x04000F63 RID: 3939
		[ThemeFileInfo("cmpldd.gif")]
		ComplianceDropDown,
		// Token: 0x04000F64 RID: 3940
		[ThemeFileInfo("cmpl_chk.gif", ThemeFileInfoFlags.PhaseII)]
		ComplianceCheck,
		// Token: 0x04000F65 RID: 3941
		[ThemeFileInfo("addtsk.gif")]
		AddTask,
		// Token: 0x04000F66 RID: 3942
		[ThemeFileInfo("dwn-gry.gif", ThemeFileInfoFlags.PhaseII)]
		DownArrowGrey,
		// Token: 0x04000F67 RID: 3943
		[ThemeFileInfo("chkbxhdr.gif")]
		CheckboxHeader,
		// Token: 0x04000F68 RID: 3944
		[ThemeFileInfo("elcfldr.gif")]
		ELCFolderIcon,
		// Token: 0x04000F69 RID: 3945
		[ThemeFileInfo("ctgrs.gif")]
		Categories,
		// Token: 0x04000F6A RID: 3946
		[ThemeFileInfo("ctgrs-hdr.gif")]
		CategoriesHeader,
		// Token: 0x04000F6B RID: 3947
		[ThemeFileInfo("chk-unchk.gif")]
		CheckUnchecked,
		// Token: 0x04000F6C RID: 3948
		[ThemeFileInfo("chk-prtl.gif")]
		CheckPartial,
		// Token: 0x04000F6D RID: 3949
		[ThemeFileInfo("chk-chkd.gif")]
		CheckChecked,
		// Token: 0x04000F6E RID: 3950
		[ThemeFileInfo("chkmrk.png")]
		Checkmark,
		// Token: 0x04000F6F RID: 3951
		[ThemeFileInfo("lv-chk.png")]
		ListViewCheckboxChecked,
		// Token: 0x04000F70 RID: 3952
		[ThemeFileInfo("lv-unchk.png")]
		ListViewCheckboxUnchecked,
		// Token: 0x04000F71 RID: 3953
		[ThemeFileInfo("rd-sel.gif")]
		RadioSelected,
		// Token: 0x04000F72 RID: 3954
		[ThemeFileInfo("rd-unsel.gif")]
		RadioUnselected,
		// Token: 0x04000F73 RID: 3955
		[ThemeFileInfo("cnclsrch.gif")]
		CancelSearch,
		// Token: 0x04000F74 RID: 3956
		[ThemeFileInfo("fvdoc.gif")]
		AddToFavorites,
		// Token: 0x04000F75 RID: 3957
		[ThemeFileInfo("dot.gif")]
		Dot,
		// Token: 0x04000F76 RID: 3958
		[ThemeFileInfo("squiggly.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		SpellCheckUnderline,
		// Token: 0x04000F77 RID: 3959
		[ThemeFileInfo("rul-sml.gif")]
		RulesSmall,
		// Token: 0x04000F78 RID: 3960
		[ThemeFileInfo("msg-encyptd.gif")]
		MessageEncrypted,
		// Token: 0x04000F79 RID: 3961
		[ThemeFileInfo("msg-sgnd.gif")]
		MessageSigned,
		// Token: 0x04000F7A RID: 3962
		[ThemeFileInfo("msg-irm.gif")]
		MessageIrm,
		// Token: 0x04000F7B RID: 3963
		[ThemeFileInfo("vm-irm.gif")]
		VoicemailIrm,
		// Token: 0x04000F7C RID: 3964
		[ThemeFileInfo("encrypted.gif")]
		Encrypted,
		// Token: 0x04000F7D RID: 3965
		[ThemeFileInfo("sigvalid.gif")]
		ValidSignature,
		// Token: 0x04000F7E RID: 3966
		[ThemeFileInfo("sigwarning.gif", ThemeFileInfoFlags.PhaseII)]
		WarningSignature,
		// Token: 0x04000F7F RID: 3967
		[ThemeFileInfo("siginvalid.gif")]
		InvalidSignature,
		// Token: 0x04000F80 RID: 3968
		[ThemeFileInfo("ovf-exp.gif")]
		MonthlyViewExpandOverflow,
		// Token: 0x04000F81 RID: 3969
		[ThemeFileInfo("ovf-col.gif")]
		MonthlyViewCollapseOverflow,
		// Token: 0x04000F82 RID: 3970
		[ThemeFileInfo("ovf-exp-rtl.gif")]
		MonthlyViewExpandOverflowRtl,
		// Token: 0x04000F83 RID: 3971
		[ThemeFileInfo("ovf-col-rtl.gif")]
		MonthlyViewCollapseOverflowRtl,
		// Token: 0x04000F84 RID: 3972
		[ThemeFileInfo("dc-msg.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		EmbeddedMessage,
		// Token: 0x04000F85 RID: 3973
		[ThemeFileInfo("recover.png", ThemeFileInfoFlags.PhaseII)]
		Recover,
		// Token: 0x04000F86 RID: 3974
		[ThemeFileInfo("rcvdelitms.gif", ThemeFileInfoFlags.PhaseII)]
		RecoverDeletedItems,
		// Token: 0x04000F87 RID: 3975
		[ThemeFileInfo("rcvdelitms_small.png")]
		RecoverDeletedItemsSmall,
		// Token: 0x04000F88 RID: 3976
		[ThemeFileInfo("fwdattach.gif")]
		ForwardAsAttachment,
		// Token: 0x04000F89 RID: 3977
		[ThemeFileInfo("attachitem.png", ThemeFileInfoFlags.PhaseII)]
		AttachItem,
		// Token: 0x04000F8A RID: 3978
		[ThemeFileInfo("fldr-shared-to.gif")]
		FolderSharedTo,
		// Token: 0x04000F8B RID: 3979
		[ThemeFileInfo("cal-shared-to.gif", ThemeFileInfoFlags.PhaseII)]
		CalendarSharedTo,
		// Token: 0x04000F8C RID: 3980
		[ThemeFileInfo("cnt-shared-to.gif", ThemeFileInfoFlags.PhaseII)]
		ContactSharedTo,
		// Token: 0x04000F8D RID: 3981
		[ThemeFileInfo("tsk-shared-to.gif", ThemeFileInfoFlags.PhaseII)]
		TaskSharedTo,
		// Token: 0x04000F8E RID: 3982
		[ThemeFileInfo("fldr-shared-out.gif")]
		FolderSharedOut,
		// Token: 0x04000F8F RID: 3983
		[ThemeFileInfo("cal-shared-out.gif", ThemeFileInfoFlags.PhaseII)]
		CalendarSharedOut,
		// Token: 0x04000F90 RID: 3984
		[ThemeFileInfo("cnt-shared-out.gif", ThemeFileInfoFlags.PhaseII)]
		ContactSharedOut,
		// Token: 0x04000F91 RID: 3985
		[ThemeFileInfo("tsk-shared-out.gif", ThemeFileInfoFlags.PhaseII)]
		TaskSharedOut,
		// Token: 0x04000F92 RID: 3986
		[ThemeFileInfo("approve.gif")]
		Approve,
		// Token: 0x04000F93 RID: 3987
		[ThemeFileInfo("reject.gif")]
		Reject,
		// Token: 0x04000F94 RID: 3988
		[ThemeFileInfo("error2.gif")]
		Error2,
		// Token: 0x04000F95 RID: 3989
		[ThemeFileInfo("delete_small.gif")]
		DeleteSmall,
		// Token: 0x04000F96 RID: 3990
		[ThemeFileInfo("phone.gif")]
		Phone,
		// Token: 0x04000F97 RID: 3991
		[ThemeFileInfo("chat.gif")]
		Chat,
		// Token: 0x04000F98 RID: 3992
		[ThemeFileInfo("newchat.gif")]
		NewChat,
		// Token: 0x04000F99 RID: 3993
		[ThemeFileInfo("filter.gif")]
		RecipientFilter,
		// Token: 0x04000F9A RID: 3994
		[ThemeFileInfo("doughboy.png")]
		DoughboyPerson,
		// Token: 0x04000F9B RID: 3995
		[ThemeFileInfo("doughboySm.png")]
		DoughboyPersonSmall,
		// Token: 0x04000F9C RID: 3996
		[ThemeFileInfo("doughboyDL.png")]
		DoughboyDL,
		// Token: 0x04000F9D RID: 3997
		[ThemeFileInfo("away.png")]
		PresenceAway,
		// Token: 0x04000F9E RID: 3998
		[ThemeFileInfo("busy.png")]
		PresenceBusy,
		// Token: 0x04000F9F RID: 3999
		[ThemeFileInfo("dnd.png")]
		PresenceDoNotDisturb,
		// Token: 0x04000FA0 RID: 4000
		[ThemeFileInfo("offline.png")]
		PresenceOffline,
		// Token: 0x04000FA1 RID: 4001
		[ThemeFileInfo("avlbl.png")]
		PresenceAvailable,
		// Token: 0x04000FA2 RID: 4002
		[ThemeFileInfo("blocked.png")]
		PresenceBlocked,
		// Token: 0x04000FA3 RID: 4003
		[ThemeFileInfo("unkwn.png")]
		PresenceUnknown,
		// Token: 0x04000FA4 RID: 4004
		[ThemeFileInfo("awayVbar.png")]
		PresenceAwayVbar,
		// Token: 0x04000FA5 RID: 4005
		[ThemeFileInfo("busyVbar.png")]
		PresenceBusyVbar,
		// Token: 0x04000FA6 RID: 4006
		[ThemeFileInfo("dndVbar.png")]
		PresenceDoNotDisturbVbar,
		// Token: 0x04000FA7 RID: 4007
		[ThemeFileInfo("offlineVbar.png")]
		PresenceOfflineVbar,
		// Token: 0x04000FA8 RID: 4008
		[ThemeFileInfo("avlblVbar.png")]
		PresenceAvailableVbar,
		// Token: 0x04000FA9 RID: 4009
		[ThemeFileInfo("blockedVbar.png")]
		PresenceBlockedVbar,
		// Token: 0x04000FAA RID: 4010
		[ThemeFileInfo("unkwnVbar.png")]
		PresenceUnknownVbar,
		// Token: 0x04000FAB RID: 4011
		[ThemeFileInfo("awayVbarSm.png")]
		PresenceAwayVbarSmall,
		// Token: 0x04000FAC RID: 4012
		[ThemeFileInfo("busyVbarSm.png")]
		PresenceBusyVbarSmall,
		// Token: 0x04000FAD RID: 4013
		[ThemeFileInfo("dndVbarSm.png")]
		PresenceDoNotDisturbVbarSmall,
		// Token: 0x04000FAE RID: 4014
		[ThemeFileInfo("offlineVbarSm.png")]
		PresenceOfflineVbarSmall,
		// Token: 0x04000FAF RID: 4015
		[ThemeFileInfo("avlblVbarSm.png")]
		PresenceAvailableVbarSmall,
		// Token: 0x04000FB0 RID: 4016
		[ThemeFileInfo("blockedVbarSm.png")]
		PresenceBlockedVbarSmall,
		// Token: 0x04000FB1 RID: 4017
		[ThemeFileInfo("unkwnVbarSm.png")]
		PresenceUnknownVbarSmall,
		// Token: 0x04000FB2 RID: 4018
		[ThemeFileInfo("big-blocked.png")]
		BigPresenceBlocked,
		// Token: 0x04000FB3 RID: 4019
		[ThemeFileInfo("adbdy.gif")]
		AddBuddy,
		// Token: 0x04000FB4 RID: 4020
		[ThemeFileInfo("rmbdy.gif")]
		RemoveBuddy,
		// Token: 0x04000FB5 RID: 4021
		[ThemeFileInfo("chat-arw.gif")]
		ChatArrow,
		// Token: 0x04000FB6 RID: 4022
		[ThemeFileInfo("failinv.gif", ThemeFileInfoFlags.PhaseII)]
		FailedToInvite,
		// Token: 0x04000FB7 RID: 4023
		[ThemeFileInfo("typng.gif", ThemeFileInfoFlags.PhaseII)]
		Typing,
		// Token: 0x04000FB8 RID: 4024
		[ThemeFileInfo("joinchat.gif")]
		JoinedChat,
		// Token: 0x04000FB9 RID: 4025
		[ThemeFileInfo("leftchat.gif")]
		LeftChat,
		// Token: 0x04000FBA RID: 4026
		[ThemeFileInfo("sendsms.gif")]
		SendSms,
		// Token: 0x04000FBB RID: 4027
		[ThemeFileInfo("sms.gif")]
		Sms,
		// Token: 0x04000FBC RID: 4028
		[ThemeFileInfo("new.png")]
		New,
		// Token: 0x04000FBD RID: 4029
		[ThemeFileInfo("lvlogo.png")]
		LiveLogo,
		// Token: 0x04000FBE RID: 4030
		[ThemeFileInfo("yahoo.png")]
		YahooLogo,
		// Token: 0x04000FBF RID: 4031
		[ThemeFileInfo("oc.png")]
		OfficeCommunicatorLogo,
		// Token: 0x04000FC0 RID: 4032
		[ThemeFileInfo("rss.gif")]
		RssSubscription,
		// Token: 0x04000FC1 RID: 4033
		[ThemeFileInfo("fvrfltr.gif")]
		FavoritesFilter,
		// Token: 0x04000FC2 RID: 4034
		[ThemeFileInfo("minusrtl.png")]
		MinusRTL,
		// Token: 0x04000FC3 RID: 4035
		[ThemeFileInfo("plusrtl.png")]
		PlusRTL,
		// Token: 0x04000FC4 RID: 4036
		[ThemeFileInfo("chgperm.gif")]
		ChangePermission,
		// Token: 0x04000FC5 RID: 4037
		[ThemeFileInfo("fltr-addfav.png")]
		FilterAddToFav,
		// Token: 0x04000FC6 RID: 4038
		[ThemeFileInfo("fltr-clr.gif")]
		FilterClear,
		// Token: 0x04000FC7 RID: 4039
		[ThemeFileInfo("tsklg.png")]
		Task2,
		// Token: 0x04000FC8 RID: 4040
		[ThemeFileInfo("dcmnt.gif", ThemeFileInfoFlags.PhaseII)]
		Documents,
		// Token: 0x04000FC9 RID: 4041
		[ThemeFileInfo("dcmntsmll.gif", ThemeFileInfoFlags.PhaseII)]
		DocumentsSmall,
		// Token: 0x04000FCA RID: 4042
		[ThemeFileInfo("pf.gif")]
		PublicFolder,
		// Token: 0x04000FCB RID: 4043
		[ThemeFileInfo("pfsmll.gif")]
		PublicFolderSmall,
		// Token: 0x04000FCC RID: 4044
		[ThemeFileInfo("lgnleft.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonLeft,
		// Token: 0x04000FCD RID: 4045
		[ThemeFileInfo("lgnright.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonRight,
		// Token: 0x04000FCE RID: 4046
		[ThemeFileInfo("cnv-his.gif")]
		ConversationHistory,
		// Token: 0x04000FCF RID: 4047
		[ThemeFileInfo("shw-b.png", ThemeFileInfoFlags.LooseImage)]
		ShadowBottom,
		// Token: 0x04000FD0 RID: 4048
		[ThemeFileInfo("shw-bl.png")]
		ShadowBottomLeft,
		// Token: 0x04000FD1 RID: 4049
		[ThemeFileInfo("shw-br.png")]
		ShadowBottomRight,
		// Token: 0x04000FD2 RID: 4050
		[ThemeFileInfo("shw-r.png", ThemeFileInfoFlags.LooseImage)]
		ShadowRight,
		// Token: 0x04000FD3 RID: 4051
		[ThemeFileInfo("shw-tr.png")]
		ShadowTopRight,
		// Token: 0x04000FD4 RID: 4052
		[ThemeFileInfo("cnv-draft.png", ThemeFileInfoFlags.LooseImage)]
		ConversationsDraftPattern,
		// Token: 0x04000FD5 RID: 4053
		[ThemeFileInfo("headerbgmain.png", ThemeFileInfoFlags.LooseImage)]
		OwaPremiumBackgroundImageMain,
		// Token: 0x04000FD6 RID: 4054
		[ThemeFileInfo("headerbgmainrtl.png", ThemeFileInfoFlags.LooseImage)]
		OwaPremiumBackgroundImageMainRtl,
		// Token: 0x04000FD7 RID: 4055
		[ThemeFileInfo("headerbgright.png", ThemeFileInfoFlags.LooseImage)]
		OwaPremiumBackgroundImageRight,
		// Token: 0x04000FD8 RID: 4056
		[ThemeFileInfo("hdrdiv-l.png")]
		HeaderDividerImageLeft,
		// Token: 0x04000FD9 RID: 4057
		[ThemeFileInfo("hdrdiv-r.png")]
		HeaderDividerImageRight,
		// Token: 0x04000FDA RID: 4058
		[ThemeFileInfo("hdrdiv-t.png", ThemeFileInfoFlags.LooseImage)]
		HeaderDividerImageTile,
		// Token: 0x04000FDB RID: 4059
		[ThemeFileInfo("userdropdown.png")]
		UserTileDropDownArrow,
		// Token: 0x04000FDC RID: 4060
		[ThemeFileInfo("alertdropdown.gif")]
		AlertBarDropDownArrow,
		// Token: 0x04000FDD RID: 4061
		[ThemeFileInfo("cnv-msg-rd.gif")]
		ConversationIconRead,
		// Token: 0x04000FDE RID: 4062
		[ThemeFileInfo("cnv-msg-unrd.gif")]
		ConversationIconUnread,
		// Token: 0x04000FDF RID: 4063
		[ThemeFileInfo("cnv-msg-rpl.gif")]
		ConversationIconReply,
		// Token: 0x04000FE0 RID: 4064
		[ThemeFileInfo("cnv-msg-fwd.gif")]
		ConversationIconForward,
		// Token: 0x04000FE1 RID: 4065
		[ThemeFileInfo("cnv-msg-irm-rd.gif")]
		IrmConversationIconRead,
		// Token: 0x04000FE2 RID: 4066
		[ThemeFileInfo("cnv-msg-irm-unrd.gif")]
		IrmConversationIconUnread,
		// Token: 0x04000FE3 RID: 4067
		[ThemeFileInfo("cnv-msg-irm-rpl.gif")]
		IrmConversationIconReply,
		// Token: 0x04000FE4 RID: 4068
		[ThemeFileInfo("cnv-msg-irm-fwd.gif")]
		IrmConversationIconForward,
		// Token: 0x04000FE5 RID: 4069
		[ThemeFileInfo("sms-conv.gif")]
		ConversationIconSmsReadAndUnread,
		// Token: 0x04000FE6 RID: 4070
		[ThemeFileInfo("sms-conv-reply.gif")]
		ConversationIconSmsReply,
		// Token: 0x04000FE7 RID: 4071
		[ThemeFileInfo("sms-conv-forward.gif")]
		ConversationIconSmsForward,
		// Token: 0x04000FE8 RID: 4072
		[ThemeFileInfo("cnv-mtg.gif")]
		ConversationIconMeeting,
		// Token: 0x04000FE9 RID: 4073
		[ThemeFileInfo("pipe-stop-l.png")]
		PipeStopLarge,
		// Token: 0x04000FEA RID: 4074
		[ThemeFileInfo("pipe-stop-s.png")]
		PipeStopSmall,
		// Token: 0x04000FEB RID: 4075
		[ThemeFileInfo("pipe-end.png")]
		PipeEnd,
		// Token: 0x04000FEC RID: 4076
		[ThemeFileInfo("favicon.ico", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		FavoriteIcon,
		// Token: 0x04000FED RID: 4077
		[ThemeFileInfo("excal.png")]
		ExchangeCalendar,
		// Token: 0x04000FEE RID: 4078
		[ThemeFileInfo("dash.gif")]
		Dash,
		// Token: 0x04000FEF RID: 4079
		[ThemeFileInfo("warning.png")]
		WarningSmall,
		// Token: 0x04000FF0 RID: 4080
		[ThemeFileInfo("insertimage.png")]
		InsertImage,
		// Token: 0x04000FF1 RID: 4081
		[ThemeFileInfo("cal-web.png", ThemeFileInfoFlags.PhaseII)]
		WebCalendar,
		// Token: 0x04000FF2 RID: 4082
		[ThemeFileInfo("cal-web-big.png", ThemeFileInfoFlags.PhaseII)]
		WebCalendarBig,
		// Token: 0x04000FF3 RID: 4083
		[ThemeFileInfo("print-tentative.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		PrintTentative,
		// Token: 0x04000FF4 RID: 4084
		[ThemeFileInfo("print-tentative-agenda.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		PrintTentativeForAgenda,
		// Token: 0x04000FF5 RID: 4085
		[ThemeFileInfo("dc-accdb.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeAccdb,
		// Token: 0x04000FF6 RID: 4086
		[ThemeFileInfo("dc-accde.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeAccde,
		// Token: 0x04000FF7 RID: 4087
		[ThemeFileInfo("dc-acs.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeAcs,
		// Token: 0x04000FF8 RID: 4088
		[ThemeFileInfo("dc-ascx.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeAscx,
		// Token: 0x04000FF9 RID: 4089
		[ThemeFileInfo("dc-asf.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeAsf,
		// Token: 0x04000FFA RID: 4090
		[ThemeFileInfo("dc-asp.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeAsp,
		// Token: 0x04000FFB RID: 4091
		[ThemeFileInfo("dc-aspc.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeAspc,
		// Token: 0x04000FFC RID: 4092
		[ThemeFileInfo("dc-aspx.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeAspx,
		// Token: 0x04000FFD RID: 4093
		[ThemeFileInfo("dc-bmp.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeBmp,
		// Token: 0x04000FFE RID: 4094
		[ThemeFileInfo("dc-cab.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeCab,
		// Token: 0x04000FFF RID: 4095
		[ThemeFileInfo("dc-chm.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeChm,
		// Token: 0x04001000 RID: 4096
		[ThemeFileInfo("dc-cpp.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeCpp,
		// Token: 0x04001001 RID: 4097
		[ThemeFileInfo("dc-cs.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeCs,
		// Token: 0x04001002 RID: 4098
		[ThemeFileInfo("dc-css.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeCss,
		// Token: 0x04001003 RID: 4099
		[ThemeFileInfo("dc-dll.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeDll,
		// Token: 0x04001004 RID: 4100
		[ThemeFileInfo("dc-docx.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeDocx,
		// Token: 0x04001005 RID: 4101
		[ThemeFileInfo("dc-dot.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeDot,
		// Token: 0x04001006 RID: 4102
		[ThemeFileInfo("dc-dotx.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeDotx,
		// Token: 0x04001007 RID: 4103
		[ThemeFileInfo("dc-dwt.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeDwt,
		// Token: 0x04001008 RID: 4104
		[ThemeFileInfo("dc-exe.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeExe,
		// Token: 0x04001009 RID: 4105
		[ThemeFileInfo("icon-doc.gif.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeGif,
		// Token: 0x0400100A RID: 4106
		[ThemeFileInfo("dc-htc.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeHtc,
		// Token: 0x0400100B RID: 4107
		[ThemeFileInfo("dc-htt.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeHtt,
		// Token: 0x0400100C RID: 4108
		[ThemeFileInfo("dc-hxx.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeHxx,
		// Token: 0x0400100D RID: 4109
		[ThemeFileInfo("dc-ini.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeIni,
		// Token: 0x0400100E RID: 4110
		[ThemeFileInfo("dc-jpg.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeJpg,
		// Token: 0x0400100F RID: 4111
		[ThemeFileInfo("dc-js.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeJs,
		// Token: 0x04001010 RID: 4112
		[ThemeFileInfo("dc-master.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeMaster,
		// Token: 0x04001011 RID: 4113
		[ThemeFileInfo("dc-mht.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeMht,
		// Token: 0x04001012 RID: 4114
		[ThemeFileInfo("dc-mpg.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeMpg,
		// Token: 0x04001013 RID: 4115
		[ThemeFileInfo("dc-mpnt.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeMpnt,
		// Token: 0x04001014 RID: 4116
		[ThemeFileInfo("dc-mpt.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeMpt,
		// Token: 0x04001015 RID: 4117
		[ThemeFileInfo("dc-mpw.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeMpw,
		// Token: 0x04001016 RID: 4118
		[ThemeFileInfo("dc-mpx.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeMpx,
		// Token: 0x04001017 RID: 4119
		[ThemeFileInfo("dc-oicon-doc.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeOdc,
		// Token: 0x04001018 RID: 4120
		[ThemeFileInfo("dc-one.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeOne,
		// Token: 0x04001019 RID: 4121
		[ThemeFileInfo("dc-onp.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeOnp,
		// Token: 0x0400101A RID: 4122
		[ThemeFileInfo("dc-pblsh.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypePblsh,
		// Token: 0x0400101B RID: 4123
		[ThemeFileInfo("dc-png.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypePng,
		// Token: 0x0400101C RID: 4124
		[ThemeFileInfo("dc-pot.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypePot,
		// Token: 0x0400101D RID: 4125
		[ThemeFileInfo("dc-potx.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypePotx,
		// Token: 0x0400101E RID: 4126
		[ThemeFileInfo("dc-pps.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypePps,
		// Token: 0x0400101F RID: 4127
		[ThemeFileInfo("dc-ppt.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypePpt,
		// Token: 0x04001020 RID: 4128
		[ThemeFileInfo("dc-pptx.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypePptx,
		// Token: 0x04001021 RID: 4129
		[ThemeFileInfo("dc-prj.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypePrj,
		// Token: 0x04001022 RID: 4130
		[ThemeFileInfo("dc-ptm.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypePtm,
		// Token: 0x04001023 RID: 4131
		[ThemeFileInfo("dc-rpmsg.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeRpmsg,
		// Token: 0x04001024 RID: 4132
		[ThemeFileInfo("dc-rtf.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeRtf,
		// Token: 0x04001025 RID: 4133
		[ThemeFileInfo("dc-tif.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeTif,
		// Token: 0x04001026 RID: 4134
		[ThemeFileInfo("dc-txt.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeTxt,
		// Token: 0x04001027 RID: 4135
		[ThemeFileInfo("dc-url.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeUrl,
		// Token: 0x04001028 RID: 4136
		[ThemeFileInfo("dc-vbs.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeVbs,
		// Token: 0x04001029 RID: 4137
		[ThemeFileInfo("dc-vdx.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeVdx,
		// Token: 0x0400102A RID: 4138
		[ThemeFileInfo("dc-vsd.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeVsd,
		// Token: 0x0400102B RID: 4139
		[ThemeFileInfo("dc-vsl.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeVsl,
		// Token: 0x0400102C RID: 4140
		[ThemeFileInfo("dc-vss.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeVss,
		// Token: 0x0400102D RID: 4141
		[ThemeFileInfo("dc-vst.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeVst,
		// Token: 0x0400102E RID: 4142
		[ThemeFileInfo("dc-vsu.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeVsu,
		// Token: 0x0400102F RID: 4143
		[ThemeFileInfo("dc-vsw.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeVsw,
		// Token: 0x04001030 RID: 4144
		[ThemeFileInfo("dc-vsx.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeVsx,
		// Token: 0x04001031 RID: 4145
		[ThemeFileInfo("dc-vtx.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeVtx,
		// Token: 0x04001032 RID: 4146
		[ThemeFileInfo("dc-wrd.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeWrd,
		// Token: 0x04001033 RID: 4147
		[ThemeFileInfo("dc-wv.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeWv,
		// Token: 0x04001034 RID: 4148
		[ThemeFileInfo("dc-xcl.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeXcl,
		// Token: 0x04001035 RID: 4149
		[ThemeFileInfo("dc-xlsx.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeXlsx,
		// Token: 0x04001036 RID: 4150
		[ThemeFileInfo("dc-xlt.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeXlt,
		// Token: 0x04001037 RID: 4151
		[ThemeFileInfo("dc-xltx.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeXltx,
		// Token: 0x04001038 RID: 4152
		[ThemeFileInfo("dc-xml.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeXml,
		// Token: 0x04001039 RID: 4153
		[ThemeFileInfo("dc-xsd.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeXsd,
		// Token: 0x0400103A RID: 4154
		[ThemeFileInfo("dc-xsl.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeXsl,
		// Token: 0x0400103B RID: 4155
		[ThemeFileInfo("dc-xslt.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeXslt,
		// Token: 0x0400103C RID: 4156
		[ThemeFileInfo("dc-xsn.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeXsn,
		// Token: 0x0400103D RID: 4157
		[ThemeFileInfo("dc-zip.gif", ThemeFileInfoFlags.PhaseII, "icon-doc.gif")]
		FileTypeZip,
		// Token: 0x0400103E RID: 4158
		[ThemeFileInfo("recall.gif", ThemeFileInfoFlags.PhaseII)]
		MessageRecall,
		// Token: 0x0400103F RID: 4159
		[ThemeFileInfo("recallflr.gif", ThemeFileInfoFlags.PhaseII)]
		RecallReportFailure,
		// Token: 0x04001040 RID: 4160
		[ThemeFileInfo("recallsuc.gif", ThemeFileInfoFlags.PhaseII)]
		RecallReportSuccess,
		// Token: 0x04001041 RID: 4161
		[ThemeFileInfo("tsk-rcvd.gif", ThemeFileInfoFlags.PhaseII)]
		TaskRequestUpdate,
		// Token: 0x04001042 RID: 4162
		[ThemeFileInfo("tsk-dlg.gif", ThemeFileInfoFlags.PhaseII)]
		TaskDelecated,
		// Token: 0x04001043 RID: 4163
		[ThemeFileInfo("tsk-rcr.gif", ThemeFileInfoFlags.PhaseII)]
		TaskRecur,
		// Token: 0x04001044 RID: 4164
		[ThemeFileInfo("tskreq.gif", ThemeFileInfoFlags.PhaseII)]
		TaskRequest,
		// Token: 0x04001045 RID: 4165
		[ThemeFileInfo("tskreq-acc.gif", ThemeFileInfoFlags.PhaseII)]
		TaskAcceptance,
		// Token: 0x04001046 RID: 4166
		[ThemeFileInfo("tskreq-dec.gif", ThemeFileInfoFlags.PhaseII)]
		TaskDecline,
		// Token: 0x04001047 RID: 4167
		[ThemeFileInfo("apv_rsp_cl_app.gif", ThemeFileInfoFlags.PhaseII)]
		UnreadApprovedResponse,
		// Token: 0x04001048 RID: 4168
		[ThemeFileInfo("apv_rsp_cl_rej.gif", ThemeFileInfoFlags.PhaseII)]
		UnreadRegectedResponse,
		// Token: 0x04001049 RID: 4169
		[ThemeFileInfo("apv_rsp_op_app.gif", ThemeFileInfoFlags.PhaseII)]
		ReadApprovedResponse,
		// Token: 0x0400104A RID: 4170
		[ThemeFileInfo("apv_rsp_op_rej.gif", ThemeFileInfoFlags.PhaseII)]
		ReadRejectedResponse,
		// Token: 0x0400104B RID: 4171
		[ThemeFileInfo("apv_rsp_rep_app.gif", ThemeFileInfoFlags.PhaseII)]
		RepliedToApprovedResponse,
		// Token: 0x0400104C RID: 4172
		[ThemeFileInfo("apv_rsp_rep_rej.gif", ThemeFileInfoFlags.PhaseII)]
		RepliedToRejectedResponse,
		// Token: 0x0400104D RID: 4173
		[ThemeFileInfo("apv_rsp_fwd_app.gif", ThemeFileInfoFlags.PhaseII)]
		ForwardedApprovedResponse,
		// Token: 0x0400104E RID: 4174
		[ThemeFileInfo("apv_rsp_fwd_rej.gif", ThemeFileInfoFlags.PhaseII)]
		ForwardedRejectedResponse,
		// Token: 0x0400104F RID: 4175
		[ThemeFileInfo("backcolor.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarBackColor,
		// Token: 0x04001050 RID: 4176
		[ThemeFileInfo("blockdirltr.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarDirLTR,
		// Token: 0x04001051 RID: 4177
		[ThemeFileInfo("blockdirrtl.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarDirRTL,
		// Token: 0x04001052 RID: 4178
		[ThemeFileInfo("createlink.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarCreateLink,
		// Token: 0x04001053 RID: 4179
		[ThemeFileInfo("customize.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarCustomize,
		// Token: 0x04001054 RID: 4180
		[ThemeFileInfo("indent.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarIndent,
		// Token: 0x04001055 RID: 4181
		[ThemeFileInfo("indentrtl.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarIndentRTL,
		// Token: 0x04001056 RID: 4182
		[ThemeFileInfo("inserthorizontalrule.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarInsertHorizontalRule,
		// Token: 0x04001057 RID: 4183
		[ThemeFileInfo("insertorderedlist.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarInsertOrderList,
		// Token: 0x04001058 RID: 4184
		[ThemeFileInfo("insertorderedlistrtl.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarInsertOrderedListRTL,
		// Token: 0x04001059 RID: 4185
		[ThemeFileInfo("insertunorderedlist.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarInsertUnorderedList,
		// Token: 0x0400105A RID: 4186
		[ThemeFileInfo("insertunorderedlistrtl.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarInsertUnorderedListRTL,
		// Token: 0x0400105B RID: 4187
		[ThemeFileInfo("justifycenter.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarJustifyCenter,
		// Token: 0x0400105C RID: 4188
		[ThemeFileInfo("justifyleft.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarJustfyLeft,
		// Token: 0x0400105D RID: 4189
		[ThemeFileInfo("justifyright.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarJustifyRight,
		// Token: 0x0400105E RID: 4190
		[ThemeFileInfo("outdent.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarOutdent,
		// Token: 0x0400105F RID: 4191
		[ThemeFileInfo("outdentrtl.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarOutdentRTL,
		// Token: 0x04001060 RID: 4192
		[ThemeFileInfo("redo.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarRedo,
		// Token: 0x04001061 RID: 4193
		[ThemeFileInfo("removeformat.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarRemoveFormat,
		// Token: 0x04001062 RID: 4194
		[ThemeFileInfo("strikethrough.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarStrikeThrough,
		// Token: 0x04001063 RID: 4195
		[ThemeFileInfo("subscript.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarSubscript,
		// Token: 0x04001064 RID: 4196
		[ThemeFileInfo("superscript.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarSuperscript,
		// Token: 0x04001065 RID: 4197
		[ThemeFileInfo("undo.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarUndo,
		// Token: 0x04001066 RID: 4198
		[ThemeFileInfo("unlink.png", ThemeFileInfoFlags.PhaseII)]
		FormatbarUnlink,
		// Token: 0x04001067 RID: 4199
		[ThemeFileInfo("placeholderimage.png", ThemeFileInfoFlags.LooseImage)]
		PlaceholderImage,
		// Token: 0x04001068 RID: 4200
		[ThemeFileInfo("VLVShadowTop.png")]
		VLVShadowTop,
		// Token: 0x04001069 RID: 4201
		[ThemeFileInfo("RTL-VLVShadowTop.png")]
		VLVShadowTopRTL,
		// Token: 0x0400106A RID: 4202
		[ThemeFileInfo("VLVShadowTile.png", ThemeFileInfoFlags.LooseImage)]
		VLVShadowTile,
		// Token: 0x0400106B RID: 4203
		[ThemeFileInfo("RTL-VLVShadowTile.png", ThemeFileInfoFlags.LooseImage)]
		VLVShadowTileRTL,
		// Token: 0x0400106C RID: 4204
		[ThemeFileInfo("NavDivideTop.png")]
		NavDivideTop,
		// Token: 0x0400106D RID: 4205
		[ThemeFileInfo("NavDivideTile.png", ThemeFileInfoFlags.LooseImage)]
		NavDivideTile,
		// Token: 0x0400106E RID: 4206
		[ThemeFileInfo("NavDivideBottom.png")]
		NavDivideBottom,
		// Token: 0x0400106F RID: 4207
		[ThemeFileInfo("RTL-NavDivideTop.png")]
		NavDivideTopRTL,
		// Token: 0x04001070 RID: 4208
		[ThemeFileInfo("RTL-NavDivideTile.png", ThemeFileInfoFlags.LooseImage)]
		NavDivideTileRTL,
		// Token: 0x04001071 RID: 4209
		[ThemeFileInfo("RTL-NavDivideBottom.png")]
		NavDivideBottomRTL,
		// Token: 0x04001072 RID: 4210
		[ThemeFileInfo("brdcrmb.png")]
		BreadcrumbsArrow,
		// Token: 0x04001073 RID: 4211
		[ThemeFileInfo("brdcrmbrtl.png")]
		BreadcrumbsArrowRtl,
		// Token: 0x04001074 RID: 4212
		[ThemeFileInfo("rpbottom-t.png", ThemeFileInfoFlags.LooseImage)]
		ReadingPaneBottomDividerTile,
		// Token: 0x04001075 RID: 4213
		[ThemeFileInfo("dropshadow-bottom-left.png")]
		DropShadowBottomLeft,
		// Token: 0x04001076 RID: 4214
		[ThemeFileInfo("dropshadow-bottom-right.png")]
		DropShadowBottomRight,
		// Token: 0x04001077 RID: 4215
		[ThemeFileInfo("dropshadow-corner-bottom-left.png")]
		DropShadowCornerBottomLeft,
		// Token: 0x04001078 RID: 4216
		[ThemeFileInfo("dropshadow-corner-bottom-right.png")]
		DropShadowCornerBottomRight,
		// Token: 0x04001079 RID: 4217
		[ThemeFileInfo("dropshadow-top-left.png")]
		DropShadowTopLeft,
		// Token: 0x0400107A RID: 4218
		[ThemeFileInfo("dropshadow-top-right.png")]
		DropShadowTopRight,
		// Token: 0x0400107B RID: 4219
		[ThemeFileInfo("shadow-div-left.png")]
		ShadowDivLeft,
		// Token: 0x0400107C RID: 4220
		[ThemeFileInfo("shadow-div-right.png")]
		ShadowDivRight,
		// Token: 0x0400107D RID: 4221
		[ThemeFileInfo("shadow-div-tile.png", ThemeFileInfoFlags.LooseImage)]
		ShadowDivTile,
		// Token: 0x0400107E RID: 4222
		[ThemeFileInfo("Error-Icon.png")]
		ErrorIcon,
		// Token: 0x0400107F RID: 4223
		[ThemeFileInfo("msgannotation.png")]
		MessageAnnotation,
		// Token: 0x04001080 RID: 4224
		[ThemeFileInfo]
		Count
	}
}
