using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200004D RID: 77
	public enum WellKnownFolderType
	{
		// Token: 0x040002C1 RID: 705
		None,
		// Token: 0x040002C2 RID: 706
		Root,
		// Token: 0x040002C3 RID: 707
		NonIpmSubtree,
		// Token: 0x040002C4 RID: 708
		IpmSubtree,
		// Token: 0x040002C5 RID: 709
		EFormsRegistry,
		// Token: 0x040002C6 RID: 710
		FreeBusy,
		// Token: 0x040002C7 RID: 711
		OfflineAddressBook,
		// Token: 0x040002C8 RID: 712
		LocalSiteFreeBusy,
		// Token: 0x040002C9 RID: 713
		LocalSiteOfflineAddressBook,
		// Token: 0x040002CA RID: 714
		LocaleEFormsRegistry,
		// Token: 0x040002CB RID: 715
		Inbox,
		// Token: 0x040002CC RID: 716
		SentItems,
		// Token: 0x040002CD RID: 717
		Outbox,
		// Token: 0x040002CE RID: 718
		DeletedItems = 14,
		// Token: 0x040002CF RID: 719
		Calendar,
		// Token: 0x040002D0 RID: 720
		Contacts,
		// Token: 0x040002D1 RID: 721
		Drafts,
		// Token: 0x040002D2 RID: 722
		Journal,
		// Token: 0x040002D3 RID: 723
		Tasks,
		// Token: 0x040002D4 RID: 724
		Notes,
		// Token: 0x040002D5 RID: 725
		Dumpster,
		// Token: 0x040002D6 RID: 726
		DumpsterDeletions,
		// Token: 0x040002D7 RID: 727
		DumpsterVersions,
		// Token: 0x040002D8 RID: 728
		DumpsterPurges,
		// Token: 0x040002D9 RID: 729
		CommunicatorHistory,
		// Token: 0x040002DA RID: 730
		ELC,
		// Token: 0x040002DB RID: 731
		SyncRoot,
		// Token: 0x040002DC RID: 732
		UMVoicemail,
		// Token: 0x040002DD RID: 733
		UMFax,
		// Token: 0x040002DE RID: 734
		Reminders,
		// Token: 0x040002DF RID: 735
		AllItems,
		// Token: 0x040002E0 RID: 736
		CommonViews,
		// Token: 0x040002E1 RID: 737
		Finder,
		// Token: 0x040002E2 RID: 738
		DeferredActions,
		// Token: 0x040002E3 RID: 739
		Sharing,
		// Token: 0x040002E4 RID: 740
		System,
		// Token: 0x040002E5 RID: 741
		Conflicts,
		// Token: 0x040002E6 RID: 742
		SyncIssues,
		// Token: 0x040002E7 RID: 743
		LocalFailures,
		// Token: 0x040002E8 RID: 744
		ServerFailures,
		// Token: 0x040002E9 RID: 745
		JunkEmail,
		// Token: 0x040002EA RID: 746
		FreeBusyData,
		// Token: 0x040002EB RID: 747
		LegacySchedule,
		// Token: 0x040002EC RID: 748
		LegacyShortcuts,
		// Token: 0x040002ED RID: 749
		LegacyViews,
		// Token: 0x040002EE RID: 750
		DumpsterAdminAuditLogs,
		// Token: 0x040002EF RID: 751
		DumpsterAudits,
		// Token: 0x040002F0 RID: 752
		SpoolerQueue,
		// Token: 0x040002F1 RID: 753
		TransportQueue,
		// Token: 0x040002F2 RID: 754
		ConversationActionSettings,
		// Token: 0x040002F3 RID: 755
		MRSSyncStates,
		// Token: 0x040002F4 RID: 756
		MRSMoveHistory,
		// Token: 0x040002F5 RID: 757
		PublicFolderDumpsterRoot,
		// Token: 0x040002F6 RID: 758
		PublicFolderTombstonesRoot,
		// Token: 0x040002F7 RID: 759
		PublicFolderAsyncDeleteState,
		// Token: 0x040002F8 RID: 760
		PublicFolderInternalSubmission,
		// Token: 0x040002F9 RID: 761
		Location,
		// Token: 0x040002FA RID: 762
		PeopleConnect,
		// Token: 0x040002FB RID: 763
		CalendarLogging,
		// Token: 0x040002FC RID: 764
		SchemaRoot,
		// Token: 0x040002FD RID: 765
		EventsRoot,
		// Token: 0x040002FE RID: 766
		MailboxAssociations = 64,
		// Token: 0x040002FF RID: 767
		WorkingSet,
		// Token: 0x04000300 RID: 768
		ParkedMessages,
		// Token: 0x04000301 RID: 769
		TemporarySaves
	}
}
