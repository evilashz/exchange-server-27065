using System;
using System.Net;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.OData;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000547 RID: 1351
	[XmlType(TypeName = "ResponseCodeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public enum ResponseCodeType
	{
		// Token: 0x040015F4 RID: 5620
		NoError,
		// Token: 0x040015F5 RID: 5621
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorAccessDenied,
		// Token: 0x040015F6 RID: 5622
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorAccessModeSpecified,
		// Token: 0x040015F7 RID: 5623
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorAccountDisabled,
		// Token: 0x040015F8 RID: 5624
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorAddDelegatesFailed,
		// Token: 0x040015F9 RID: 5625
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorADOperation,
		// Token: 0x040015FA RID: 5626
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorADSessionFilter,
		// Token: 0x040015FB RID: 5627
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorADUnavailable,
		// Token: 0x040015FC RID: 5628
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorArchiveFolderPathCreation,
		// Token: 0x040015FD RID: 5629
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorAffectedTaskOccurrencesRequired,
		// Token: 0x040015FE RID: 5630
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorAggregatedAccountLimitReached,
		// Token: 0x040015FF RID: 5631
		[ODataHttpStatusCode(HttpStatusCode.Conflict)]
		ErrorAggregatedAccountAlreadyExists,
		// Token: 0x04001600 RID: 5632
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorAccountNotSupportedForAggregation,
		// Token: 0x04001601 RID: 5633
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorServerTemporaryUnavailable,
		// Token: 0x04001602 RID: 5634
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorAttachmentNestLevelLimitExceeded,
		// Token: 0x04001603 RID: 5635
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorAttachmentSizeLimitExceeded,
		// Token: 0x04001604 RID: 5636
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorBatchProcessingStopped,
		// Token: 0x04001605 RID: 5637
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarCannotMoveOrCopyOccurrence,
		// Token: 0x04001606 RID: 5638
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarCannotUpdateDeletedItem,
		// Token: 0x04001607 RID: 5639
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarCannotUseIdForOccurrenceId,
		// Token: 0x04001608 RID: 5640
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarCannotUseIdForRecurringMasterId,
		// Token: 0x04001609 RID: 5641
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarDurationIsTooLong,
		// Token: 0x0400160A RID: 5642
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarEndDateIsEarlierThanStartDate,
		// Token: 0x0400160B RID: 5643
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarFolderIsInvalidForCalendarView,
		// Token: 0x0400160C RID: 5644
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarInvalidAttributeValue,
		// Token: 0x0400160D RID: 5645
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarInvalidDayForTimeChangePattern,
		// Token: 0x0400160E RID: 5646
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarInvalidDayForWeeklyRecurrence,
		// Token: 0x0400160F RID: 5647
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarInvalidPropertyState,
		// Token: 0x04001610 RID: 5648
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarInvalidPropertyValue,
		// Token: 0x04001611 RID: 5649
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarInvalidRecurrence,
		// Token: 0x04001612 RID: 5650
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarInvalidTimeZone,
		// Token: 0x04001613 RID: 5651
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsDelegatedForAccept,
		// Token: 0x04001614 RID: 5652
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsDelegatedForDecline,
		// Token: 0x04001615 RID: 5653
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsDelegatedForRemove,
		// Token: 0x04001616 RID: 5654
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsDelegatedForTentative,
		// Token: 0x04001617 RID: 5655
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsGroupMailboxForAccept,
		// Token: 0x04001618 RID: 5656
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsGroupMailboxForDecline,
		// Token: 0x04001619 RID: 5657
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsGroupMailboxForTentative,
		// Token: 0x0400161A RID: 5658
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsGroupMailboxForSuppressReadReceipt,
		// Token: 0x0400161B RID: 5659
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsNotOrganizer,
		// Token: 0x0400161C RID: 5660
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsOrganizerForAccept,
		// Token: 0x0400161D RID: 5661
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsOrganizerForDecline,
		// Token: 0x0400161E RID: 5662
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsOrganizerForRemove,
		// Token: 0x0400161F RID: 5663
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsOrganizerForTentative,
		// Token: 0x04001620 RID: 5664
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsCancelledForAccept,
		// Token: 0x04001621 RID: 5665
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsCancelledForDecline,
		// Token: 0x04001622 RID: 5666
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsCancelledForRemove,
		// Token: 0x04001623 RID: 5667
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsCancelledForTentative,
		// Token: 0x04001624 RID: 5668
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarIsCancelledMessageSent,
		// Token: 0x04001625 RID: 5669
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarOccurrenceIndexIsOutOfRecurrenceRange,
		// Token: 0x04001626 RID: 5670
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarOccurrenceIsDeletedFromRecurrence,
		// Token: 0x04001627 RID: 5671
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarOutOfRange,
		// Token: 0x04001628 RID: 5672
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarMeetingRequestIsOutOfDate,
		// Token: 0x04001629 RID: 5673
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarMeetingIsOutOfDateResponseNotProcessed,
		// Token: 0x0400162A RID: 5674
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarMeetingIsOutOfDateResponseNotProcessedMessageSent,
		// Token: 0x0400162B RID: 5675
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCalendarViewRangeTooBig,
		// Token: 0x0400162C RID: 5676
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCallerIsInvalidADAccount,
		// Token: 0x0400162D RID: 5677
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotAddAggregatedAccount,
		// Token: 0x0400162E RID: 5678
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotAddAggregatedAccountMailbox,
		// Token: 0x0400162F RID: 5679
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotAddAggregatedAccountToList,
		// Token: 0x04001630 RID: 5680
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotGetAggregatedAccount,
		// Token: 0x04001631 RID: 5681
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotRemoveAggregatedAccount,
		// Token: 0x04001632 RID: 5682
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotRemoveAggregatedAccountMailbox,
		// Token: 0x04001633 RID: 5683
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotSetAggregatedAccount,
		// Token: 0x04001634 RID: 5684
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotArchiveCalendarContactTaskFolderException,
		// Token: 0x04001635 RID: 5685
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotArchiveItemsInArchiveMailbox,
		// Token: 0x04001636 RID: 5686
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotArchiveItemsInPublicFolders,
		// Token: 0x04001637 RID: 5687
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotCreateCalendarItemInNonCalendarFolder,
		// Token: 0x04001638 RID: 5688
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotCreateContactInNonContactFolder,
		// Token: 0x04001639 RID: 5689
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotCreatePostItemInNonMailFolder,
		// Token: 0x0400163A RID: 5690
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotCreateTaskInNonTaskFolder,
		// Token: 0x0400163B RID: 5691
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotCreateUnifiedMailbox,
		// Token: 0x0400163C RID: 5692
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotFindUnifiedMailbox,
		// Token: 0x0400163D RID: 5693
		ErrorCannotFindUser,
		// Token: 0x0400163E RID: 5694
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotDeleteObject,
		// Token: 0x0400163F RID: 5695
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotDeleteTaskOccurrence,
		// Token: 0x04001640 RID: 5696
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotGetSourceFolderPath,
		// Token: 0x04001641 RID: 5697
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotLinkMoreThanOneO365AccountToAnMsa,
		// Token: 0x04001642 RID: 5698
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotOpenFileAttachment,
		// Token: 0x04001643 RID: 5699
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotResolveOrganizationName,
		// Token: 0x04001644 RID: 5700
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCannotSetCalendarPermissionOnNonCalendarFolder,
		// Token: 0x04001645 RID: 5701
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCannotSetNonCalendarPermissionOnCalendarFolder,
		// Token: 0x04001646 RID: 5702
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCannotSetPermissionUnknownEntries,
		// Token: 0x04001647 RID: 5703
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCannotSpecifySearchFolderAsSourceFolder,
		// Token: 0x04001648 RID: 5704
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCannotUseFolderIdForItemId,
		// Token: 0x04001649 RID: 5705
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCannotUseItemIdForFolderId,
		// Token: 0x0400164A RID: 5706
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorChangeKeyRequired,
		// Token: 0x0400164B RID: 5707
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorChangeKeyRequiredForWriteOperations,
		// Token: 0x0400164C RID: 5708
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorConnectionFailed,
		// Token: 0x0400164D RID: 5709
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorContainsFilterWrongType,
		// Token: 0x0400164E RID: 5710
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorContentConversionFailed,
		// Token: 0x0400164F RID: 5711
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorContentIndexingNotEnabled,
		// Token: 0x04001650 RID: 5712
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCorruptData,
		// Token: 0x04001651 RID: 5713
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorCreateItemAccessDenied,
		// Token: 0x04001652 RID: 5714
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorCreateSubfolderAccessDenied,
		// Token: 0x04001653 RID: 5715
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorCrossMailboxMoveCopy,
		// Token: 0x04001654 RID: 5716
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCrossSiteRequest,
		// Token: 0x04001655 RID: 5717
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorDataSizeLimitExceeded,
		// Token: 0x04001656 RID: 5718
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorDataSourceOperation,
		// Token: 0x04001657 RID: 5719
		[ODataHttpStatusCode(HttpStatusCode.Conflict)]
		ErrorDelegateAlreadyExists,
		// Token: 0x04001658 RID: 5720
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorDelegateCannotAddOwner,
		// Token: 0x04001659 RID: 5721
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorDelegateMissingConfiguration,
		// Token: 0x0400165A RID: 5722
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorDelegateNoUser,
		// Token: 0x0400165B RID: 5723
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorDelegateValidationFailed,
		// Token: 0x0400165C RID: 5724
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorDeleteDistinguishedFolder,
		// Token: 0x0400165D RID: 5725
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorDeleteItemsFailed,
		// Token: 0x0400165E RID: 5726
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorDeleteUnifiedMessagingPromptFailed,
		// Token: 0x0400165F RID: 5727
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorDistinguishedUserNotSupported,
		// Token: 0x04001660 RID: 5728
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorDistributionListMemberNotExist,
		// Token: 0x04001661 RID: 5729
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorDuplicateInputFolderNames,
		// Token: 0x04001662 RID: 5730
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorDuplicateUserIdsSpecified,
		// Token: 0x04001663 RID: 5731
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorExchangeApplicationNotEnabled,
		// Token: 0x04001664 RID: 5732
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorExceededConnectionCount,
		// Token: 0x04001665 RID: 5733
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorExceededFindCountLimit,
		// Token: 0x04001666 RID: 5734
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorExceededSubscriptionCount,
		// Token: 0x04001667 RID: 5735
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidContactEmailAddress,
		// Token: 0x04001668 RID: 5736
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidContactEmailIndex,
		// Token: 0x04001669 RID: 5737
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidManagedFolderProperty,
		// Token: 0x0400166A RID: 5738
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMissingManagedFolderId,
		// Token: 0x0400166B RID: 5739
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidArgument,
		// Token: 0x0400166C RID: 5740
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidAggregatedAccountCredentials,
		// Token: 0x0400166D RID: 5741
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidLogonType,
		// Token: 0x0400166E RID: 5742
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidMailbox,
		// Token: 0x0400166F RID: 5743
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidManagedFolderQuota,
		// Token: 0x04001670 RID: 5744
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidManagedFolderSize,
		// Token: 0x04001671 RID: 5745
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidPermissionSettings,
		// Token: 0x04001672 RID: 5746
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidProxySecurityContext,
		// Token: 0x04001673 RID: 5747
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidUnifiedViewParameter,
		// Token: 0x04001674 RID: 5748
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidUserInfo,
		// Token: 0x04001675 RID: 5749
		ErrorMailboxIsNotPartOfAggregatedMailboxes,
		// Token: 0x04001676 RID: 5750
		ErrorMailboxContainerGuidMismatch,
		// Token: 0x04001677 RID: 5751
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorManagedFolderNotFound,
		// Token: 0x04001678 RID: 5752
		[ODataHttpStatusCode(HttpStatusCode.Conflict)]
		ErrorManagedFolderAlreadyExists,
		// Token: 0x04001679 RID: 5753
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCreateManagedFolderPartialCompletion,
		// Token: 0x0400167A RID: 5754
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorManagedFoldersRootFailure,
		// Token: 0x0400167B RID: 5755
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorEmailAddressMismatch,
		// Token: 0x0400167C RID: 5756
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorEventNotFound,
		// Token: 0x0400167D RID: 5757
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorExpiredSubscription,
		// Token: 0x0400167E RID: 5758
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorFolderCorrupt,
		// Token: 0x0400167F RID: 5759
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorFolderNotFound,
		// Token: 0x04001680 RID: 5760
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorFolderPropertRequestFailed,
		// Token: 0x04001681 RID: 5761
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorFolderSave,
		// Token: 0x04001682 RID: 5762
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorFolderSaveFailed,
		// Token: 0x04001683 RID: 5763
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorFolderSavePropertyError,
		// Token: 0x04001684 RID: 5764
		[ODataHttpStatusCode(HttpStatusCode.Conflict)]
		ErrorFolderExists,
		// Token: 0x04001685 RID: 5765
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorFoldersMustBelongToSameMailbox,
		// Token: 0x04001686 RID: 5766
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorImContactLimitReached,
		// Token: 0x04001687 RID: 5767
		[ODataHttpStatusCode(HttpStatusCode.Conflict)]
		ErrorImGroupDisplayNameAlreadyExists,
		// Token: 0x04001688 RID: 5768
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorImGroupLimitReached,
		// Token: 0x04001689 RID: 5769
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorImpersonateUserDenied,
		// Token: 0x0400168A RID: 5770
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorImpersonationDenied,
		// Token: 0x0400168B RID: 5771
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorImpersonationFailed,
		// Token: 0x0400168C RID: 5772
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorIncorrectSchemaVersion,
		// Token: 0x0400168D RID: 5773
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorIncorrectUpdatePropertyCount,
		// Token: 0x0400168E RID: 5774
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInsufficientResources,
		// Token: 0x0400168F RID: 5775
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInternalServerError,
		// Token: 0x04001690 RID: 5776
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInternalServerTransientError,
		// Token: 0x04001691 RID: 5777
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidAttachmentId,
		// Token: 0x04001692 RID: 5778
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidAttachmentSubfilter,
		// Token: 0x04001693 RID: 5779
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidAttachmentSubfilterTextFilter,
		// Token: 0x04001694 RID: 5780
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidChangeKey,
		// Token: 0x04001695 RID: 5781
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidCompleteDate,
		// Token: 0x04001696 RID: 5782
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidDelegatePermission,
		// Token: 0x04001697 RID: 5783
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidDelegateUserId,
		// Token: 0x04001698 RID: 5784
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidExcludesRestriction,
		// Token: 0x04001699 RID: 5785
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidExpressionTypeForSubFilter,
		// Token: 0x0400169A RID: 5786
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidExtendedProperty,
		// Token: 0x0400169B RID: 5787
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidExtendedPropertyValue,
		// Token: 0x0400169C RID: 5788
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidFederatedOrganizationId,
		// Token: 0x0400169D RID: 5789
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidFolderId,
		// Token: 0x0400169E RID: 5790
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidFolderTypeForOperation,
		// Token: 0x0400169F RID: 5791
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidFractionalPagingParameters,
		// Token: 0x040016A0 RID: 5792
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidId,
		// Token: 0x040016A1 RID: 5793
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidIdEmpty,
		// Token: 0x040016A2 RID: 5794
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidIdMalformed,
		// Token: 0x040016A3 RID: 5795
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidIdMalformedEwsLegacyIdFormat,
		// Token: 0x040016A4 RID: 5796
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidIdMonikerTooLong,
		// Token: 0x040016A5 RID: 5797
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidIdNotAnItemAttachmentId,
		// Token: 0x040016A6 RID: 5798
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidIdStoreObjectIdTooLong,
		// Token: 0x040016A7 RID: 5799
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidIdReturnedByResolveNames,
		// Token: 0x040016A8 RID: 5800
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidIdTooManyAttachmentLevels,
		// Token: 0x040016A9 RID: 5801
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidIdXml,
		// Token: 0x040016AA RID: 5802
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidImContactId,
		// Token: 0x040016AB RID: 5803
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidImDistributionGroupSmtpAddress,
		// Token: 0x040016AC RID: 5804
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidImGroupId,
		// Token: 0x040016AD RID: 5805
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidIndexedPagingParameters,
		// Token: 0x040016AE RID: 5806
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidInternetHeaderChildNodes,
		// Token: 0x040016AF RID: 5807
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidItemForOperationCreateItemAttachment,
		// Token: 0x040016B0 RID: 5808
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidItemForOperationArchiveItem,
		// Token: 0x040016B1 RID: 5809
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidItemForOperationCreateItem,
		// Token: 0x040016B2 RID: 5810
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidItemForOperationAcceptItem,
		// Token: 0x040016B3 RID: 5811
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidItemForOperationDeclineItem,
		// Token: 0x040016B4 RID: 5812
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidItemForOperationCancelItem,
		// Token: 0x040016B5 RID: 5813
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidItemForOperationExpandDL,
		// Token: 0x040016B6 RID: 5814
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidItemForOperationRemoveItem,
		// Token: 0x040016B7 RID: 5815
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidItemForOperationSendItem,
		// Token: 0x040016B8 RID: 5816
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidItemForOperationTentative,
		// Token: 0x040016B9 RID: 5817
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidOofParameter,
		// Token: 0x040016BA RID: 5818
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidNameForNameResolution,
		// Token: 0x040016BB RID: 5819
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidOperation,
		// Token: 0x040016BC RID: 5820
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidPagingMaxRows,
		// Token: 0x040016BD RID: 5821
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidParentFolder,
		// Token: 0x040016BE RID: 5822
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidPercentCompleteValue,
		// Token: 0x040016BF RID: 5823
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidPhoneCallId,
		// Token: 0x040016C0 RID: 5824
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidPhoneNumber,
		// Token: 0x040016C1 RID: 5825
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidPropertyAppend,
		// Token: 0x040016C2 RID: 5826
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidPropertyDelete,
		// Token: 0x040016C3 RID: 5827
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidPropertyForExists,
		// Token: 0x040016C4 RID: 5828
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidPropertyRequest,
		// Token: 0x040016C5 RID: 5829
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidPropertySet,
		// Token: 0x040016C6 RID: 5830
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidPropertyForOperation,
		// Token: 0x040016C7 RID: 5831
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidPropertyUpdateSentMessage,
		// Token: 0x040016C8 RID: 5832
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidPullSubscriptionId,
		// Token: 0x040016C9 RID: 5833
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidPushSubscriptionUrl,
		// Token: 0x040016CA RID: 5834
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidRecipients,
		// Token: 0x040016CB RID: 5835
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidRecipientSubfilter,
		// Token: 0x040016CC RID: 5836
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidRecipientSubfilterComparison,
		// Token: 0x040016CD RID: 5837
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidRecipientSubfilterOrder,
		// Token: 0x040016CE RID: 5838
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidRecipientSubfilterTextFilter,
		// Token: 0x040016CF RID: 5839
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidReferenceItem,
		// Token: 0x040016D0 RID: 5840
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidRequest,
		// Token: 0x040016D1 RID: 5841
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidRestriction,
		// Token: 0x040016D2 RID: 5842
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidRetentionTagTypeMismatch,
		// Token: 0x040016D3 RID: 5843
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidRetentionTagInvisible,
		// Token: 0x040016D4 RID: 5844
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidRetentionTagInheritance,
		// Token: 0x040016D5 RID: 5845
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidRetentionTagIdGuid,
		// Token: 0x040016D6 RID: 5846
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidRoutingType,
		// Token: 0x040016D7 RID: 5847
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidScheduledOofDuration,
		// Token: 0x040016D8 RID: 5848
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidSchemaVersionForMailboxVersion,
		// Token: 0x040016D9 RID: 5849
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidSendItemSaveSettings,
		// Token: 0x040016DA RID: 5850
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidSerializedAccessToken,
		// Token: 0x040016DB RID: 5851
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidServerVersion,
		// Token: 0x040016DC RID: 5852
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidSid,
		// Token: 0x040016DD RID: 5853
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidSIPUri,
		// Token: 0x040016DE RID: 5854
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidSubfilterType,
		// Token: 0x040016DF RID: 5855
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidSubfilterTypeNotAttendeeType,
		// Token: 0x040016E0 RID: 5856
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidSubfilterTypeNotRecipientType,
		// Token: 0x040016E1 RID: 5857
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidSubscription,
		// Token: 0x040016E2 RID: 5858
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidSubscriptionRequest,
		// Token: 0x040016E3 RID: 5859
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidSyncStateData,
		// Token: 0x040016E4 RID: 5860
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidUserOofSettings,
		// Token: 0x040016E5 RID: 5861
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidUserPrincipalName,
		// Token: 0x040016E6 RID: 5862
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidUser,
		// Token: 0x040016E7 RID: 5863
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidParameter,
		// Token: 0x040016E8 RID: 5864
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidUserSid,
		// Token: 0x040016E9 RID: 5865
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidUserSidMissingUPN,
		// Token: 0x040016EA RID: 5866
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidValueForCountSystemQueryOption,
		// Token: 0x040016EB RID: 5867
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidValueForProperty,
		// Token: 0x040016EC RID: 5868
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidWatermark,
		// Token: 0x040016ED RID: 5869
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorIPGatewayNotFound,
		// Token: 0x040016EE RID: 5870
		[ODataHttpStatusCode(HttpStatusCode.Conflict)]
		ErrorIrresolvableConflict,
		// Token: 0x040016EF RID: 5871
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorItemCorrupt,
		// Token: 0x040016F0 RID: 5872
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorItemNotFound,
		// Token: 0x040016F1 RID: 5873
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorItemPropertyRequestFailed,
		// Token: 0x040016F2 RID: 5874
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorItemSave,
		// Token: 0x040016F3 RID: 5875
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorItemSavePropertyError,
		// Token: 0x040016F4 RID: 5876
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMailboxConfiguration,
		// Token: 0x040016F5 RID: 5877
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMailboxHoldNotFound,
		// Token: 0x040016F6 RID: 5878
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMailboxMoveInProgress,
		// Token: 0x040016F7 RID: 5879
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMailboxStoreUnavailable,
		// Token: 0x040016F8 RID: 5880
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMailTipsDisabled,
		// Token: 0x040016F9 RID: 5881
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMessageDispositionRequired,
		// Token: 0x040016FA RID: 5882
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMessageSizeExceeded,
		// Token: 0x040016FB RID: 5883
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMimeContentConversionFailed,
		// Token: 0x040016FC RID: 5884
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMimeContentInvalid,
		// Token: 0x040016FD RID: 5885
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMimeContentInvalidBase64String,
		// Token: 0x040016FE RID: 5886
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMissingEmailAddress,
		// Token: 0x040016FF RID: 5887
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMissingEmailAddressForManagedFolder,
		// Token: 0x04001700 RID: 5888
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMissingInformationEmailAddress,
		// Token: 0x04001701 RID: 5889
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMissingInformationReferenceItemId,
		// Token: 0x04001702 RID: 5890
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMissingItemForCreateItemAttachment,
		// Token: 0x04001703 RID: 5891
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMissingRecipients,
		// Token: 0x04001704 RID: 5892
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMissingUserIdInformation,
		// Token: 0x04001705 RID: 5893
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMoreThanOneAccessModeSpecified,
		// Token: 0x04001706 RID: 5894
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMoveCopyFailed,
		// Token: 0x04001707 RID: 5895
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMoveDistinguishedFolder,
		// Token: 0x04001708 RID: 5896
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMultiLegacyMailboxAccess,
		// Token: 0x04001709 RID: 5897
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNameResolutionMultipleResults,
		// Token: 0x0400170A RID: 5898
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNameResolutionNoMailbox,
		// Token: 0x0400170B RID: 5899
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNameResolutionNoResults,
		// Token: 0x0400170C RID: 5900
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNewEventStreamConnectionOpened,
		// Token: 0x0400170D RID: 5901
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNoApplicableProxyCASServersAvailable,
		// Token: 0x0400170E RID: 5902
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNoDestinationCASDueToKerberosRequirements,
		// Token: 0x0400170F RID: 5903
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNoDestinationCASDueToSSLRequirements,
		// Token: 0x04001710 RID: 5904
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNoDestinationCASDueToVersionMismatch,
		// Token: 0x04001711 RID: 5905
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNoFolderClassOverride,
		// Token: 0x04001712 RID: 5906
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorNonExistentMailbox,
		// Token: 0x04001713 RID: 5907
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNonPrimarySmtpAddress,
		// Token: 0x04001714 RID: 5908
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNoPropertyTagForCustomProperties,
		// Token: 0x04001715 RID: 5909
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNoPublicFolderReplicaAvailable,
		// Token: 0x04001716 RID: 5910
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNoRespondingCASInDestinationSite,
		// Token: 0x04001717 RID: 5911
		[ODataHttpStatusCode(HttpStatusCode.NotAcceptable)]
		ErrorNotAcceptable,
		// Token: 0x04001718 RID: 5912
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNotApplicableOutsideOfDatacenter,
		// Token: 0x04001719 RID: 5913
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNotEnoughMemory,
		// Token: 0x0400171A RID: 5914
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNotDelegate,
		// Token: 0x0400171B RID: 5915
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorObjectTypeChanged,
		// Token: 0x0400171C RID: 5916
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorOccurrenceCrossingBoundary,
		// Token: 0x0400171D RID: 5917
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorOccurrenceTimeSpanTooBig,
		// Token: 0x0400171E RID: 5918
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorOperationNotAllowedWithPublicFolderRoot,
		// Token: 0x0400171F RID: 5919
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorParentFolderIdRequired,
		// Token: 0x04001720 RID: 5920
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorParentFolderNotFound,
		// Token: 0x04001721 RID: 5921
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorPasswordChangeRequired,
		// Token: 0x04001722 RID: 5922
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorPasswordExpired,
		// Token: 0x04001723 RID: 5923
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorPeopleConnectFailedToReadApplicationConfiguration,
		// Token: 0x04001724 RID: 5924
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorPeopleConnectionNotFound,
		// Token: 0x04001725 RID: 5925
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorPeopleConnectionNoToken,
		// Token: 0x04001726 RID: 5926
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorPhoneNumberNotDialable,
		// Token: 0x04001727 RID: 5927
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorPropertyUpdate,
		// Token: 0x04001728 RID: 5928
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorPromptPublishingOperationFailed,
		// Token: 0x04001729 RID: 5929
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorPropertyValidationFailure,
		// Token: 0x0400172A RID: 5930
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorProxiedSubscriptionCallFailure,
		// Token: 0x0400172B RID: 5931
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorProxyGroupSidLimitExceeded,
		// Token: 0x0400172C RID: 5932
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorProxyCallFailed,
		// Token: 0x0400172D RID: 5933
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorProxyServiceDiscoveryFailed,
		// Token: 0x0400172E RID: 5934
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorProxyTokenExpired,
		// Token: 0x0400172F RID: 5935
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorPublicFolderMailboxDiscoveryFailed,
		// Token: 0x04001730 RID: 5936
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorPublicFolderOperationFailed,
		// Token: 0x04001731 RID: 5937
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorPublicFolderSyncException,
		// Token: 0x04001732 RID: 5938
		[ODataHttpStatusCode(HttpStatusCode.ServiceUnavailable)]
		ErrorMultifactorRegistrationUnavailable,
		// Token: 0x04001733 RID: 5939
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidProperty,
		// Token: 0x04001734 RID: 5940
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidUrlQuery,
		// Token: 0x04001735 RID: 5941
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidUrlQueryFilter,
		// Token: 0x04001736 RID: 5942
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorQueryFilterTooLong,
		// Token: 0x04001737 RID: 5943
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorQuotaExceeded,
		// Token: 0x04001738 RID: 5944
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorReadEventsFailed,
		// Token: 0x04001739 RID: 5945
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorReadReceiptNotPending,
		// Token: 0x0400173A RID: 5946
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorRecurrenceEndDateTooBig,
		// Token: 0x0400173B RID: 5947
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorRecurrenceHasNoOccurrence,
		// Token: 0x0400173C RID: 5948
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorRequiredPropertyMissing,
		// Token: 0x0400173D RID: 5949
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorResolveNamesInvalidFolderType,
		// Token: 0x0400173E RID: 5950
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorResolveNamesOnlyOneContactsFolderAllowed,
		// Token: 0x0400173F RID: 5951
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorRemoveDelegatesFailed,
		// Token: 0x04001740 RID: 5952
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorResponseSchemaValidation,
		// Token: 0x04001741 RID: 5953
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorRestrictionTooLong,
		// Token: 0x04001742 RID: 5954
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorRestrictionTooComplex,
		// Token: 0x04001743 RID: 5955
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidExchangeImpersonationHeaderData,
		// Token: 0x04001744 RID: 5956
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorSavedItemFolderNotFound,
		// Token: 0x04001745 RID: 5957
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorSchemaValidation,
		// Token: 0x04001746 RID: 5958
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorSearchFolderNotInitialized,
		// Token: 0x04001747 RID: 5959
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorSendAsDenied,
		// Token: 0x04001748 RID: 5960
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorSendMeetingCancellationsRequired,
		// Token: 0x04001749 RID: 5961
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorSendMeetingInvitationsOrCancellationsRequired,
		// Token: 0x0400174A RID: 5962
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorSendMeetingInvitationsRequired,
		// Token: 0x0400174B RID: 5963
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorSentMeetingRequestUpdate,
		// Token: 0x0400174C RID: 5964
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorSentTaskRequestUpdate,
		// Token: 0x0400174D RID: 5965
		[ODataHttpStatusCode(HttpStatusCode.ServiceUnavailable)]
		ErrorServerBusy,
		// Token: 0x0400174E RID: 5966
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorStaleObject,
		// Token: 0x0400174F RID: 5967
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorSubmissionQuotaExceeded,
		// Token: 0x04001750 RID: 5968
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorSubscriptionAccessDenied,
		// Token: 0x04001751 RID: 5969
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorSubscriptionDelegateAccessNotSupported,
		// Token: 0x04001752 RID: 5970
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorSubscriptionNotFound,
		// Token: 0x04001753 RID: 5971
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorSubscriptionUnsubscribed,
		// Token: 0x04001754 RID: 5972
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorSyncFolderNotFound,
		// Token: 0x04001755 RID: 5973
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorTeamMailboxNotFound,
		// Token: 0x04001756 RID: 5974
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorTeamMailboxNotLinkedToSharePoint,
		// Token: 0x04001757 RID: 5975
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorTeamMailboxUrlValidationFailed,
		// Token: 0x04001758 RID: 5976
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorTeamMailboxNotAuthorizedOwner,
		// Token: 0x04001759 RID: 5977
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorTeamMailboxActiveToPendingDelete,
		// Token: 0x0400175A RID: 5978
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorTeamMailboxFailedSendingNotifications,
		// Token: 0x0400175B RID: 5979
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorTeamMailboxErrorUnknown,
		// Token: 0x0400175C RID: 5980
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorTimeZone,
		// Token: 0x0400175D RID: 5981
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorToFolderNotFound,
		// Token: 0x0400175E RID: 5982
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorTokenSerializationDenied,
		// Token: 0x0400175F RID: 5983
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorTooManyObjectsOpened,
		// Token: 0x04001760 RID: 5984
		[ODataHttpStatusCode(HttpStatusCode.Conflict)]
		ErrorUnifiedMailboxAlreadyExists,
		// Token: 0x04001761 RID: 5985
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorUpdateDelegatesFailed,
		// Token: 0x04001762 RID: 5986
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorUpdatePropertyMismatch,
		// Token: 0x04001763 RID: 5987
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorUnableToGetUserOofSettings,
		// Token: 0x04001764 RID: 5988
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorUnableToRemoveImContactFromGroup,
		// Token: 0x04001765 RID: 5989
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorUnifiedMailboxSupportedOnlyWithMicrosoftAccount,
		// Token: 0x04001766 RID: 5990
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorUnifiedMessagingDialPlanNotFound,
		// Token: 0x04001767 RID: 5991
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorUnifiedMessagingReportDataNotFound,
		// Token: 0x04001768 RID: 5992
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorUnifiedMessagingPromptNotFound,
		// Token: 0x04001769 RID: 5993
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorUnifiedMessagingRequestFailed,
		// Token: 0x0400176A RID: 5994
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorUnifiedMessagingServerNotFound,
		// Token: 0x0400176B RID: 5995
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorUnsupportedSubFilter,
		// Token: 0x0400176C RID: 5996
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorUnsupportedCulture,
		// Token: 0x0400176D RID: 5997
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorUnsupportedMapiPropertyType,
		// Token: 0x0400176E RID: 5998
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorUnsupportedMimeConversion,
		// Token: 0x0400176F RID: 5999
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorUnsupportedPathForQuery,
		// Token: 0x04001770 RID: 6000
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorUnsupportedPathForSortGroup,
		// Token: 0x04001771 RID: 6001
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorUnsupportedPropertyDefinition,
		// Token: 0x04001772 RID: 6002
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorUnsupportedQueryFilter,
		// Token: 0x04001773 RID: 6003
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorUnsupportedRecurrence,
		// Token: 0x04001774 RID: 6004
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorUnsupportedTypeForConversion,
		// Token: 0x04001775 RID: 6005
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorUserNotUnifiedMessagingEnabled,
		// Token: 0x04001776 RID: 6006
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorValueOutOfRange,
		// Token: 0x04001777 RID: 6007
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorVirusDetected,
		// Token: 0x04001778 RID: 6008
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorVirusMessageDeleted,
		// Token: 0x04001779 RID: 6009
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorVoiceMailNotImplemented,
		// Token: 0x0400177A RID: 6010
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorWrongServerVersion,
		// Token: 0x0400177B RID: 6011
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorWrongServerVersionDelegate,
		// Token: 0x0400177C RID: 6012
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorSharingSynchronizationFailed,
		// Token: 0x0400177D RID: 6013
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorSharingNoExternalEwsAvailable,
		// Token: 0x0400177E RID: 6014
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidGetSharingFolderRequest,
		// Token: 0x0400177F RID: 6015
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorNotAllowedExternalSharingByPolicy,
		// Token: 0x04001780 RID: 6016
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorUserNotAllowedByPolicy,
		// Token: 0x04001781 RID: 6017
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorPermissionNotAllowedByPolicy,
		// Token: 0x04001782 RID: 6018
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorOrganizationNotFederated,
		// Token: 0x04001783 RID: 6019
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidExternalSharingInitiator,
		// Token: 0x04001784 RID: 6020
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorUserWithoutFederatedProxyAddress,
		// Token: 0x04001785 RID: 6021
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidExternalSharingSubscriber,
		// Token: 0x04001786 RID: 6022
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidSharingData,
		// Token: 0x04001787 RID: 6023
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidSharingMessage,
		// Token: 0x04001788 RID: 6024
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNotSupportedSharingMessage,
		// Token: 0x04001789 RID: 6025
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorTooManyContactsException,
		// Token: 0x0400178A RID: 6026
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorFullSyncRequiredException,
		// Token: 0x0400178B RID: 6027
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorClientIntentInvalidStateDefinition,
		// Token: 0x0400178C RID: 6028
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorClientIntentNotFound,
		// Token: 0x0400178D RID: 6029
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorRequestStreamTooBig = 5000,
		// Token: 0x0400178E RID: 6030
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMailboxDataArrayEmpty,
		// Token: 0x0400178F RID: 6031
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMailboxDataArrayTooBig,
		// Token: 0x04001790 RID: 6032
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorTimeIntervalTooBig,
		// Token: 0x04001791 RID: 6033
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidMergedFreeBusyInterval,
		// Token: 0x04001792 RID: 6034
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorResultSetTooBig = 5006,
		// Token: 0x04001793 RID: 6035
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidClientSecurityContext,
		// Token: 0x04001794 RID: 6036
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorMailboxLogonFailed,
		// Token: 0x04001795 RID: 6037
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorMailRecipientNotFound,
		// Token: 0x04001796 RID: 6038
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidTimeInterval,
		// Token: 0x04001797 RID: 6039
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorPublicFolderServerNotFound,
		// Token: 0x04001798 RID: 6040
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorInvalidAccessLevel,
		// Token: 0x04001799 RID: 6041
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidSecurityDescriptor,
		// Token: 0x0400179A RID: 6042
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorWin32InteropError,
		// Token: 0x0400179B RID: 6043
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorProxyRequestNotAllowed,
		// Token: 0x0400179C RID: 6044
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorProxyRequestProcessingFailed,
		// Token: 0x0400179D RID: 6045
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorPublicFolderRequestProcessingFailed,
		// Token: 0x0400179E RID: 6046
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorWorkingHoursXmlMalformed = 5019,
		// Token: 0x0400179F RID: 6047
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorServiceDiscoveryFailed = 5021,
		// Token: 0x040017A0 RID: 6048
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorAddressSpaceNotFound = 5023,
		// Token: 0x040017A1 RID: 6049
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorAvailabilityConfigNotFound,
		// Token: 0x040017A2 RID: 6050
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidCrossForestCredentials,
		// Token: 0x040017A3 RID: 6051
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidFreeBusyViewType,
		// Token: 0x040017A4 RID: 6052
		[ODataHttpStatusCode(HttpStatusCode.RequestTimeout)]
		ErrorTimeoutExpired,
		// Token: 0x040017A5 RID: 6053
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMissingArgument,
		// Token: 0x040017A6 RID: 6054
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorNoCalendar,
		// Token: 0x040017A7 RID: 6055
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNoConnectionSettingsAvailableForAggregatedAccount,
		// Token: 0x040017A8 RID: 6056
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidAuthorizationContext = 5032,
		// Token: 0x040017A9 RID: 6057
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorLogonAsNetworkServiceFailed,
		// Token: 0x040017AA RID: 6058
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidNetworkServiceContext,
		// Token: 0x040017AB RID: 6059
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidSmtpAddress,
		// Token: 0x040017AC RID: 6060
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorIndividualMailboxLimitReached,
		// Token: 0x040017AD RID: 6061
		[ODataHttpStatusCode(HttpStatusCode.Forbidden)]
		ErrorNoFreeBusyAccess,
		// Token: 0x040017AE RID: 6062
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorClientDisconnected = 5042,
		// Token: 0x040017AF RID: 6063
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorAutoDiscoverFailed = 5039,
		// Token: 0x040017B0 RID: 6064
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMeetingSuggestionGenerationFailed,
		// Token: 0x040017B1 RID: 6065
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorFreeBusyGenerationFailed,
		// Token: 0x040017B2 RID: 6066
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorFreeBusyDLLimitReached = 5043,
		// Token: 0x040017B3 RID: 6067
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMailboxFailover = 5046,
		// Token: 0x040017B4 RID: 6068
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidOrganizationRelationshipForFreeBusy,
		// Token: 0x040017B5 RID: 6069
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorRemoteUserMailboxMustSpecifyExplicitLocalMailbox,
		// Token: 0x040017B6 RID: 6070
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMissingInformationSharingFolderId,
		// Token: 0x040017B7 RID: 6071
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorDuplicateSOAPHeader,
		// Token: 0x040017B8 RID: 6072
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMessageTrackingPermanentError,
		// Token: 0x040017B9 RID: 6073
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMessageTrackingTransientError,
		// Token: 0x040017BA RID: 6074
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorMessageTrackingNoSuchDomain,
		// Token: 0x040017BB RID: 6075
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorApplyConversationActionFailed,
		// Token: 0x040017BC RID: 6076
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotEmptyFolder,
		// Token: 0x040017BD RID: 6077
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInboxRulesValidationError,
		// Token: 0x040017BE RID: 6078
		[ODataHttpStatusCode(HttpStatusCode.Conflict)]
		ErrorOutlookRuleBlobExists,
		// Token: 0x040017BF RID: 6079
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorRulesOverQuota,
		// Token: 0x040017C0 RID: 6080
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorDuplicateLegacyDistinguishedName,
		// Token: 0x040017C1 RID: 6081
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMissedNotificationEvents,
		// Token: 0x040017C2 RID: 6082
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorRightsManagementPermanentException,
		// Token: 0x040017C3 RID: 6083
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorRightsManagementTransientException,
		// Token: 0x040017C4 RID: 6084
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorExchangeConfigurationException,
		// Token: 0x040017C5 RID: 6085
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidClientAccessTokenRequest,
		// Token: 0x040017C6 RID: 6086
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorNoSpeechDetected,
		// Token: 0x040017C7 RID: 6087
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorUMServerUnavailable,
		// Token: 0x040017C8 RID: 6088
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorRecipientNotFound,
		// Token: 0x040017C9 RID: 6089
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorRecognizerNotInstalled,
		// Token: 0x040017CA RID: 6090
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorSpeechGrammarError,
		// Token: 0x040017CB RID: 6091
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidManagementRoleHeader,
		// Token: 0x040017CC RID: 6092
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorLocationServicesDisabled,
		// Token: 0x040017CD RID: 6093
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorLocationServicesRequestTimedOut,
		// Token: 0x040017CE RID: 6094
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorLocationServicesRequestFailed,
		// Token: 0x040017CF RID: 6095
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorLocationServicesInvalidRequest,
		// Token: 0x040017D0 RID: 6096
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorWeatherServiceDisabled,
		// Token: 0x040017D1 RID: 6097
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorMailboxScopeNotAllowedWithoutQueryString,
		// Token: 0x040017D2 RID: 6098
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorArchiveMailboxSearchFailed,
		// Token: 0x040017D3 RID: 6099
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorGetRemoteArchiveFolderFailed,
		// Token: 0x040017D4 RID: 6100
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorFindRemoteArchiveFolderFailed,
		// Token: 0x040017D5 RID: 6101
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorGetRemoteArchiveItemFailed,
		// Token: 0x040017D6 RID: 6102
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorExportRemoteArchiveItemsFailed,
		// Token: 0x040017D7 RID: 6103
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorArchiveMailboxServiceDiscoveryFailed,
		// Token: 0x040017D8 RID: 6104
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotAttachSelf,
		// Token: 0x040017D9 RID: 6105
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotDisableMandatoryExtension,
		// Token: 0x040017DA RID: 6106
		[ODataHttpStatusCode(HttpStatusCode.NotFound)]
		ErrorExtensionNotFound,
		// Token: 0x040017DB RID: 6107
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCannotGetExternalEcpUrl,
		// Token: 0x040017DC RID: 6108
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorSearchQueryHasTooManyKeywords,
		// Token: 0x040017DD RID: 6109
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorSearchTooManyMailboxes,
		// Token: 0x040017DE RID: 6110
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidRetentionTagNone,
		// Token: 0x040017DF RID: 6111
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorDiscoverySearchesDisabled,
		// Token: 0x040017E0 RID: 6112
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMaximumDevicesReached,
		// Token: 0x040017E1 RID: 6113
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorCalendarSeekToConditionNotSupported,
		// Token: 0x040017E2 RID: 6114
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidAppApiVersionSupported,
		// Token: 0x040017E3 RID: 6115
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorInvalidAppSchemaVersionSupported,
		// Token: 0x040017E4 RID: 6116
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorOrganizationAccessBlocked,
		// Token: 0x040017E5 RID: 6117
		[ODataHttpStatusCode(HttpStatusCode.BadRequest)]
		ErrorInvalidLicense,
		// Token: 0x040017E6 RID: 6118
		[ODataHttpStatusCode(HttpStatusCode.InternalServerError)]
		ErrorMessagePerFolderCountReceiveQuotaExceeded
	}
}
