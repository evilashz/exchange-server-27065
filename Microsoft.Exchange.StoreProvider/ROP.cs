using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200002C RID: 44
	internal enum ROP : uint
	{
		// Token: 0x04000275 RID: 629
		ropNone,
		// Token: 0x04000276 RID: 630
		ropRelease,
		// Token: 0x04000277 RID: 631
		ropOpenFolder,
		// Token: 0x04000278 RID: 632
		ropOpenMessage,
		// Token: 0x04000279 RID: 633
		ropGetHierarchyTable,
		// Token: 0x0400027A RID: 634
		ropGetContentsTable,
		// Token: 0x0400027B RID: 635
		ropCreateMessage,
		// Token: 0x0400027C RID: 636
		ropGetPropsSpecific,
		// Token: 0x0400027D RID: 637
		ropGetPropsAll,
		// Token: 0x0400027E RID: 638
		ropGetPropList,
		// Token: 0x0400027F RID: 639
		ropSetProps,
		// Token: 0x04000280 RID: 640
		ropDeleteProps,
		// Token: 0x04000281 RID: 641
		ropSaveChangesMessage,
		// Token: 0x04000282 RID: 642
		ropNukeRecipients,
		// Token: 0x04000283 RID: 643
		ropFlushRecipients,
		// Token: 0x04000284 RID: 644
		ropReadRecipients,
		// Token: 0x04000285 RID: 645
		ropReloadCachedInfo,
		// Token: 0x04000286 RID: 646
		ropSetReadFlag,
		// Token: 0x04000287 RID: 647
		ropSetColumns,
		// Token: 0x04000288 RID: 648
		ropSortTable,
		// Token: 0x04000289 RID: 649
		ropRestrict,
		// Token: 0x0400028A RID: 650
		ropQueryRows,
		// Token: 0x0400028B RID: 651
		ropGetStatus,
		// Token: 0x0400028C RID: 652
		ropQueryPosition,
		// Token: 0x0400028D RID: 653
		ropSeekRow,
		// Token: 0x0400028E RID: 654
		ropSeekRowBookmark,
		// Token: 0x0400028F RID: 655
		ropSeekRowApprox,
		// Token: 0x04000290 RID: 656
		ropCreateBookmark,
		// Token: 0x04000291 RID: 657
		ropCreateFolder,
		// Token: 0x04000292 RID: 658
		ropDeleteFolder,
		// Token: 0x04000293 RID: 659
		ropDeleteMessages,
		// Token: 0x04000294 RID: 660
		ropGetMessageStatus,
		// Token: 0x04000295 RID: 661
		ropSetMessageStatus,
		// Token: 0x04000296 RID: 662
		ropGetAttachmentTable,
		// Token: 0x04000297 RID: 663
		ropOpenAttach,
		// Token: 0x04000298 RID: 664
		ropCreateAttach,
		// Token: 0x04000299 RID: 665
		ropDeleteAttach,
		// Token: 0x0400029A RID: 666
		ropSaveChangesAttach,
		// Token: 0x0400029B RID: 667
		ropSetReceiveFolder,
		// Token: 0x0400029C RID: 668
		ropGetReceiveFolder,
		// Token: 0x0400029D RID: 669
		ropSpoolerRules,
		// Token: 0x0400029E RID: 670
		ropRegisterNotification,
		// Token: 0x0400029F RID: 671
		ropNotify,
		// Token: 0x040002A0 RID: 672
		ropOpenStream,
		// Token: 0x040002A1 RID: 673
		ropReadStream,
		// Token: 0x040002A2 RID: 674
		ropWriteStream,
		// Token: 0x040002A3 RID: 675
		ropSeekStream,
		// Token: 0x040002A4 RID: 676
		ropSetSizeStream,
		// Token: 0x040002A5 RID: 677
		ropSetSearchCriteria,
		// Token: 0x040002A6 RID: 678
		ropGetSearchCriteria,
		// Token: 0x040002A7 RID: 679
		ropSubmitMessage,
		// Token: 0x040002A8 RID: 680
		ropMoveCopyMessages,
		// Token: 0x040002A9 RID: 681
		ropAbortSubmit,
		// Token: 0x040002AA RID: 682
		ropMoveFolder,
		// Token: 0x040002AB RID: 683
		ropCopyFolder,
		// Token: 0x040002AC RID: 684
		ropQueryColumnsAll,
		// Token: 0x040002AD RID: 685
		ropAbort,
		// Token: 0x040002AE RID: 686
		ropCopyTo,
		// Token: 0x040002AF RID: 687
		ropCopyToStream,
		// Token: 0x040002B0 RID: 688
		ropCloneStream,
		// Token: 0x040002B1 RID: 689
		ropRegisterTableNotification,
		// Token: 0x040002B2 RID: 690
		ropDeregisterTableNotification,
		// Token: 0x040002B3 RID: 691
		ropGetACLTable,
		// Token: 0x040002B4 RID: 692
		ropGetRulesTable,
		// Token: 0x040002B5 RID: 693
		ropModifyACL,
		// Token: 0x040002B6 RID: 694
		ropModifyRules,
		// Token: 0x040002B7 RID: 695
		ropGetOwningMDBs,
		// Token: 0x040002B8 RID: 696
		ropLtidFromId,
		// Token: 0x040002B9 RID: 697
		ropIdFromLtid,
		// Token: 0x040002BA RID: 698
		ropFGhosted,
		// Token: 0x040002BB RID: 699
		ropOpenMessageProp,
		// Token: 0x040002BC RID: 700
		ropSetSpooler,
		// Token: 0x040002BD RID: 701
		ropSpoolerLockMsg,
		// Token: 0x040002BE RID: 702
		ropAddressTypes,
		// Token: 0x040002BF RID: 703
		ropTransportSend,
		// Token: 0x040002C0 RID: 704
		ropFXSrcCopyMessages,
		// Token: 0x040002C1 RID: 705
		ropFXSrcCopyFolder,
		// Token: 0x040002C2 RID: 706
		ropFXSrcCopyTo,
		// Token: 0x040002C3 RID: 707
		ropFXSrcGetBuffer,
		// Token: 0x040002C4 RID: 708
		ropFindRow,
		// Token: 0x040002C5 RID: 709
		ropProgress,
		// Token: 0x040002C6 RID: 710
		ropXportNewMail,
		// Token: 0x040002C7 RID: 711
		ropValidAttachs,
		// Token: 0x040002C8 RID: 712
		ropFXDstCopyConfig,
		// Token: 0x040002C9 RID: 713
		ropFXDstPutBuffer,
		// Token: 0x040002CA RID: 714
		ropGetNamesFromIDs,
		// Token: 0x040002CB RID: 715
		ropGetIDsFromNames,
		// Token: 0x040002CC RID: 716
		ropUpdateDAMs,
		// Token: 0x040002CD RID: 717
		ropEmptyFolder,
		// Token: 0x040002CE RID: 718
		ropExpandRow,
		// Token: 0x040002CF RID: 719
		ropCollapseRow,
		// Token: 0x040002D0 RID: 720
		ropLockRegionStream,
		// Token: 0x040002D1 RID: 721
		ropUnlockRegionStream,
		// Token: 0x040002D2 RID: 722
		ropCommitStream,
		// Token: 0x040002D3 RID: 723
		ropGetStreamSize,
		// Token: 0x040002D4 RID: 724
		ropQryNamedProps,
		// Token: 0x040002D5 RID: 725
		ropGetPerUserLtids,
		// Token: 0x040002D6 RID: 726
		ropGetPerUserGuid,
		// Token: 0x040002D7 RID: 727
		ropFlushPerUser,
		// Token: 0x040002D8 RID: 728
		ropGetPerUser,
		// Token: 0x040002D9 RID: 729
		ropSetPerUser,
		// Token: 0x040002DA RID: 730
		ropCacheCcnRead,
		// Token: 0x040002DB RID: 731
		ropSetReadFlags,
		// Token: 0x040002DC RID: 732
		ropCopyProps,
		// Token: 0x040002DD RID: 733
		ropGetReceiveFolderTable,
		// Token: 0x040002DE RID: 734
		ropFXSrcCopyProps,
		// Token: 0x040002DF RID: 735
		ropFXDstCopyProps,
		// Token: 0x040002E0 RID: 736
		ropGetCollapseState,
		// Token: 0x040002E1 RID: 737
		ropSetCollapseState,
		// Token: 0x040002E2 RID: 738
		ropSetXport,
		// Token: 0x040002E3 RID: 739
		ropPending,
		// Token: 0x040002E4 RID: 740
		ropOptionsData,
		// Token: 0x040002E5 RID: 741
		ropIncrCfg,
		// Token: 0x040002E6 RID: 742
		ropIncrState,
		// Token: 0x040002E7 RID: 743
		ropImportMsgChange,
		// Token: 0x040002E8 RID: 744
		ropImportHierChange,
		// Token: 0x040002E9 RID: 745
		ropImportDelete,
		// Token: 0x040002EA RID: 746
		ropUpldStStrmBegin,
		// Token: 0x040002EB RID: 747
		ropUpldStStrmContinue,
		// Token: 0x040002EC RID: 748
		ropUpldStStrmEnd,
		// Token: 0x040002ED RID: 749
		ropImportMsgMove,
		// Token: 0x040002EE RID: 750
		ropSetPropsNoReplicate,
		// Token: 0x040002EF RID: 751
		ropDeletePropsNoReplicate,
		// Token: 0x040002F0 RID: 752
		ropGetStoreState,
		// Token: 0x040002F1 RID: 753
		ropGetRights,
		// Token: 0x040002F2 RID: 754
		ropGetAllPerUserLtids,
		// Token: 0x040002F3 RID: 755
		ropOpenCollect,
		// Token: 0x040002F4 RID: 756
		ropGetLrepIds,
		// Token: 0x040002F5 RID: 757
		ropImportReads,
		// Token: 0x040002F6 RID: 758
		ropResetTable,
		// Token: 0x040002F7 RID: 759
		ropFXGetIncrState,
		// Token: 0x040002F8 RID: 760
		ropOpenAdvisor,
		// Token: 0x040002F9 RID: 761
		ropRegICSNotifs,
		// Token: 0x040002FA RID: 762
		ropOpenCStream,
		// Token: 0x040002FB RID: 763
		ropTellVersion,
		// Token: 0x040002FC RID: 764
		ropOpenFolderByName,
		// Token: 0x040002FD RID: 765
		ropSetICSNotifGUID,
		// Token: 0x040002FE RID: 766
		ropFreeBookmark,
		// Token: 0x040002FF RID: 767
		ropDeleteFolderByName,
		// Token: 0x04000300 RID: 768
		ropConfigNntpNewsfeed,
		// Token: 0x04000301 RID: 769
		ropCheckMsgIds,
		// Token: 0x04000302 RID: 770
		ropBeginNntpArticle,
		// Token: 0x04000303 RID: 771
		ropWriteNntpArticle,
		// Token: 0x04000304 RID: 772
		ropSaveNntpArticle,
		// Token: 0x04000305 RID: 773
		ropWriteCommitStream,
		// Token: 0x04000306 RID: 774
		ropHardDeleteMessages,
		// Token: 0x04000307 RID: 775
		ropHardEmptyFolder,
		// Token: 0x04000308 RID: 776
		ropSetLocalRepMidsetDeleted,
		// Token: 0x04000309 RID: 777
		ropTransportDeliverMessage,
		// Token: 0x0400030A RID: 778
		ropTransportDoneWithMessage,
		// Token: 0x0400030B RID: 779
		ropIdFromLegacyDN,
		// Token: 0x0400030C RID: 780
		ropSetAuthenticatedContext,
		// Token: 0x0400030D RID: 781
		ropCopyToEx,
		// Token: 0x0400030E RID: 782
		ropImportMsgChangePartial,
		// Token: 0x0400030F RID: 783
		ropSetMessageFlags,
		// Token: 0x04000310 RID: 784
		ropMoveCopyMessagesEx,
		// Token: 0x04000311 RID: 785
		ropFXSrcGetBufferEx,
		// Token: 0x04000312 RID: 786
		ropFXDstPutBufferEx,
		// Token: 0x04000313 RID: 787
		ropTransportDeliverMessage2,
		// Token: 0x04000314 RID: 788
		ropCreateMessageEx,
		// Token: 0x04000315 RID: 789
		ropMoveCopyMessagesEID,
		// Token: 0x04000316 RID: 790
		ropTransportDupDlvCheck,
		// Token: 0x04000317 RID: 791
		ropPrereadMessages,
		// Token: 0x04000318 RID: 792
		ropGetContentsTableExtended = 164U,
		// Token: 0x04000319 RID: 793
		ropBackoff = 249U,
		// Token: 0x0400031A RID: 794
		ropExtendedError,
		// Token: 0x0400031B RID: 795
		ropBookmarkReturned,
		// Token: 0x0400031C RID: 796
		ropFidReturned,
		// Token: 0x0400031D RID: 797
		ropHsotReturned,
		// Token: 0x0400031E RID: 798
		ropLogon,
		// Token: 0x0400031F RID: 799
		ropBufferTooSmall
	}
}
