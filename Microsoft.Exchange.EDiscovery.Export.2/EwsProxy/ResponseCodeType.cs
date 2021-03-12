using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000AB RID: 171
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public enum ResponseCodeType
	{
		// Token: 0x0400036F RID: 879
		NoError,
		// Token: 0x04000370 RID: 880
		ErrorAccessDenied,
		// Token: 0x04000371 RID: 881
		ErrorAccessModeSpecified,
		// Token: 0x04000372 RID: 882
		ErrorAccountDisabled,
		// Token: 0x04000373 RID: 883
		ErrorAddDelegatesFailed,
		// Token: 0x04000374 RID: 884
		ErrorAddressSpaceNotFound,
		// Token: 0x04000375 RID: 885
		ErrorADOperation,
		// Token: 0x04000376 RID: 886
		ErrorADSessionFilter,
		// Token: 0x04000377 RID: 887
		ErrorADUnavailable,
		// Token: 0x04000378 RID: 888
		ErrorAutoDiscoverFailed,
		// Token: 0x04000379 RID: 889
		ErrorAffectedTaskOccurrencesRequired,
		// Token: 0x0400037A RID: 890
		ErrorAttachmentNestLevelLimitExceeded,
		// Token: 0x0400037B RID: 891
		ErrorAttachmentSizeLimitExceeded,
		// Token: 0x0400037C RID: 892
		ErrorArchiveFolderPathCreation,
		// Token: 0x0400037D RID: 893
		ErrorArchiveMailboxNotEnabled,
		// Token: 0x0400037E RID: 894
		ErrorArchiveMailboxServiceDiscoveryFailed,
		// Token: 0x0400037F RID: 895
		ErrorAvailabilityConfigNotFound,
		// Token: 0x04000380 RID: 896
		ErrorBatchProcessingStopped,
		// Token: 0x04000381 RID: 897
		ErrorCalendarCannotMoveOrCopyOccurrence,
		// Token: 0x04000382 RID: 898
		ErrorCalendarCannotUpdateDeletedItem,
		// Token: 0x04000383 RID: 899
		ErrorCalendarCannotUseIdForOccurrenceId,
		// Token: 0x04000384 RID: 900
		ErrorCalendarCannotUseIdForRecurringMasterId,
		// Token: 0x04000385 RID: 901
		ErrorCalendarDurationIsTooLong,
		// Token: 0x04000386 RID: 902
		ErrorCalendarEndDateIsEarlierThanStartDate,
		// Token: 0x04000387 RID: 903
		ErrorCalendarFolderIsInvalidForCalendarView,
		// Token: 0x04000388 RID: 904
		ErrorCalendarInvalidAttributeValue,
		// Token: 0x04000389 RID: 905
		ErrorCalendarInvalidDayForTimeChangePattern,
		// Token: 0x0400038A RID: 906
		ErrorCalendarInvalidDayForWeeklyRecurrence,
		// Token: 0x0400038B RID: 907
		ErrorCalendarInvalidPropertyState,
		// Token: 0x0400038C RID: 908
		ErrorCalendarInvalidPropertyValue,
		// Token: 0x0400038D RID: 909
		ErrorCalendarInvalidRecurrence,
		// Token: 0x0400038E RID: 910
		ErrorCalendarInvalidTimeZone,
		// Token: 0x0400038F RID: 911
		ErrorCalendarIsCancelledForAccept,
		// Token: 0x04000390 RID: 912
		ErrorCalendarIsCancelledForDecline,
		// Token: 0x04000391 RID: 913
		ErrorCalendarIsCancelledForRemove,
		// Token: 0x04000392 RID: 914
		ErrorCalendarIsCancelledForTentative,
		// Token: 0x04000393 RID: 915
		ErrorCalendarIsDelegatedForAccept,
		// Token: 0x04000394 RID: 916
		ErrorCalendarIsDelegatedForDecline,
		// Token: 0x04000395 RID: 917
		ErrorCalendarIsDelegatedForRemove,
		// Token: 0x04000396 RID: 918
		ErrorCalendarIsDelegatedForTentative,
		// Token: 0x04000397 RID: 919
		ErrorCalendarIsNotOrganizer,
		// Token: 0x04000398 RID: 920
		ErrorCalendarIsOrganizerForAccept,
		// Token: 0x04000399 RID: 921
		ErrorCalendarIsOrganizerForDecline,
		// Token: 0x0400039A RID: 922
		ErrorCalendarIsOrganizerForRemove,
		// Token: 0x0400039B RID: 923
		ErrorCalendarIsOrganizerForTentative,
		// Token: 0x0400039C RID: 924
		ErrorCalendarOccurrenceIndexIsOutOfRecurrenceRange,
		// Token: 0x0400039D RID: 925
		ErrorCalendarOccurrenceIsDeletedFromRecurrence,
		// Token: 0x0400039E RID: 926
		ErrorCalendarOutOfRange,
		// Token: 0x0400039F RID: 927
		ErrorCalendarMeetingRequestIsOutOfDate,
		// Token: 0x040003A0 RID: 928
		ErrorCalendarViewRangeTooBig,
		// Token: 0x040003A1 RID: 929
		ErrorCallerIsInvalidADAccount,
		// Token: 0x040003A2 RID: 930
		ErrorCannotArchiveCalendarContactTaskFolderException,
		// Token: 0x040003A3 RID: 931
		ErrorCannotArchiveItemsInPublicFolders,
		// Token: 0x040003A4 RID: 932
		ErrorCannotArchiveItemsInArchiveMailbox,
		// Token: 0x040003A5 RID: 933
		ErrorCannotCreateCalendarItemInNonCalendarFolder,
		// Token: 0x040003A6 RID: 934
		ErrorCannotCreateContactInNonContactFolder,
		// Token: 0x040003A7 RID: 935
		ErrorCannotCreatePostItemInNonMailFolder,
		// Token: 0x040003A8 RID: 936
		ErrorCannotCreateTaskInNonTaskFolder,
		// Token: 0x040003A9 RID: 937
		ErrorCannotDeleteObject,
		// Token: 0x040003AA RID: 938
		ErrorCannotDisableMandatoryExtension,
		// Token: 0x040003AB RID: 939
		ErrorCannotGetSourceFolderPath,
		// Token: 0x040003AC RID: 940
		ErrorCannotGetExternalEcpUrl,
		// Token: 0x040003AD RID: 941
		ErrorCannotOpenFileAttachment,
		// Token: 0x040003AE RID: 942
		ErrorCannotDeleteTaskOccurrence,
		// Token: 0x040003AF RID: 943
		ErrorCannotEmptyFolder,
		// Token: 0x040003B0 RID: 944
		ErrorCannotSetCalendarPermissionOnNonCalendarFolder,
		// Token: 0x040003B1 RID: 945
		ErrorCannotSetNonCalendarPermissionOnCalendarFolder,
		// Token: 0x040003B2 RID: 946
		ErrorCannotSetPermissionUnknownEntries,
		// Token: 0x040003B3 RID: 947
		ErrorCannotSpecifySearchFolderAsSourceFolder,
		// Token: 0x040003B4 RID: 948
		ErrorCannotUseFolderIdForItemId,
		// Token: 0x040003B5 RID: 949
		ErrorCannotUseItemIdForFolderId,
		// Token: 0x040003B6 RID: 950
		ErrorChangeKeyRequired,
		// Token: 0x040003B7 RID: 951
		ErrorChangeKeyRequiredForWriteOperations,
		// Token: 0x040003B8 RID: 952
		ErrorClientDisconnected,
		// Token: 0x040003B9 RID: 953
		ErrorClientIntentInvalidStateDefinition,
		// Token: 0x040003BA RID: 954
		ErrorClientIntentNotFound,
		// Token: 0x040003BB RID: 955
		ErrorConnectionFailed,
		// Token: 0x040003BC RID: 956
		ErrorContainsFilterWrongType,
		// Token: 0x040003BD RID: 957
		ErrorContentConversionFailed,
		// Token: 0x040003BE RID: 958
		ErrorContentIndexingNotEnabled,
		// Token: 0x040003BF RID: 959
		ErrorCorruptData,
		// Token: 0x040003C0 RID: 960
		ErrorCreateItemAccessDenied,
		// Token: 0x040003C1 RID: 961
		ErrorCreateManagedFolderPartialCompletion,
		// Token: 0x040003C2 RID: 962
		ErrorCreateSubfolderAccessDenied,
		// Token: 0x040003C3 RID: 963
		ErrorCrossMailboxMoveCopy,
		// Token: 0x040003C4 RID: 964
		ErrorCrossSiteRequest,
		// Token: 0x040003C5 RID: 965
		ErrorDataSizeLimitExceeded,
		// Token: 0x040003C6 RID: 966
		ErrorDataSourceOperation,
		// Token: 0x040003C7 RID: 967
		ErrorDelegateAlreadyExists,
		// Token: 0x040003C8 RID: 968
		ErrorDelegateCannotAddOwner,
		// Token: 0x040003C9 RID: 969
		ErrorDelegateMissingConfiguration,
		// Token: 0x040003CA RID: 970
		ErrorDelegateNoUser,
		// Token: 0x040003CB RID: 971
		ErrorDelegateValidationFailed,
		// Token: 0x040003CC RID: 972
		ErrorDeleteDistinguishedFolder,
		// Token: 0x040003CD RID: 973
		ErrorDeleteItemsFailed,
		// Token: 0x040003CE RID: 974
		ErrorDeleteUnifiedMessagingPromptFailed,
		// Token: 0x040003CF RID: 975
		ErrorDistinguishedUserNotSupported,
		// Token: 0x040003D0 RID: 976
		ErrorDistributionListMemberNotExist,
		// Token: 0x040003D1 RID: 977
		ErrorDuplicateInputFolderNames,
		// Token: 0x040003D2 RID: 978
		ErrorDuplicateUserIdsSpecified,
		// Token: 0x040003D3 RID: 979
		ErrorEmailAddressMismatch,
		// Token: 0x040003D4 RID: 980
		ErrorEventNotFound,
		// Token: 0x040003D5 RID: 981
		ErrorExceededConnectionCount,
		// Token: 0x040003D6 RID: 982
		ErrorExceededSubscriptionCount,
		// Token: 0x040003D7 RID: 983
		ErrorExceededFindCountLimit,
		// Token: 0x040003D8 RID: 984
		ErrorExpiredSubscription,
		// Token: 0x040003D9 RID: 985
		ErrorExtensionNotFound,
		// Token: 0x040003DA RID: 986
		ErrorFolderCorrupt,
		// Token: 0x040003DB RID: 987
		ErrorFolderNotFound,
		// Token: 0x040003DC RID: 988
		ErrorFolderPropertRequestFailed,
		// Token: 0x040003DD RID: 989
		ErrorFolderSave,
		// Token: 0x040003DE RID: 990
		ErrorFolderSaveFailed,
		// Token: 0x040003DF RID: 991
		ErrorFolderSavePropertyError,
		// Token: 0x040003E0 RID: 992
		ErrorFolderExists,
		// Token: 0x040003E1 RID: 993
		ErrorFreeBusyGenerationFailed,
		// Token: 0x040003E2 RID: 994
		ErrorGetServerSecurityDescriptorFailed,
		// Token: 0x040003E3 RID: 995
		ErrorImContactLimitReached,
		// Token: 0x040003E4 RID: 996
		ErrorImGroupDisplayNameAlreadyExists,
		// Token: 0x040003E5 RID: 997
		ErrorImGroupLimitReached,
		// Token: 0x040003E6 RID: 998
		ErrorImpersonateUserDenied,
		// Token: 0x040003E7 RID: 999
		ErrorImpersonationDenied,
		// Token: 0x040003E8 RID: 1000
		ErrorImpersonationFailed,
		// Token: 0x040003E9 RID: 1001
		ErrorIncorrectSchemaVersion,
		// Token: 0x040003EA RID: 1002
		ErrorIncorrectUpdatePropertyCount,
		// Token: 0x040003EB RID: 1003
		ErrorIndividualMailboxLimitReached,
		// Token: 0x040003EC RID: 1004
		ErrorInsufficientResources,
		// Token: 0x040003ED RID: 1005
		ErrorInternalServerError,
		// Token: 0x040003EE RID: 1006
		ErrorInternalServerTransientError,
		// Token: 0x040003EF RID: 1007
		ErrorInvalidAccessLevel,
		// Token: 0x040003F0 RID: 1008
		ErrorInvalidArgument,
		// Token: 0x040003F1 RID: 1009
		ErrorInvalidAttachmentId,
		// Token: 0x040003F2 RID: 1010
		ErrorInvalidAttachmentSubfilter,
		// Token: 0x040003F3 RID: 1011
		ErrorInvalidAttachmentSubfilterTextFilter,
		// Token: 0x040003F4 RID: 1012
		ErrorInvalidAuthorizationContext,
		// Token: 0x040003F5 RID: 1013
		ErrorInvalidChangeKey,
		// Token: 0x040003F6 RID: 1014
		ErrorInvalidClientSecurityContext,
		// Token: 0x040003F7 RID: 1015
		ErrorInvalidCompleteDate,
		// Token: 0x040003F8 RID: 1016
		ErrorInvalidContactEmailAddress,
		// Token: 0x040003F9 RID: 1017
		ErrorInvalidContactEmailIndex,
		// Token: 0x040003FA RID: 1018
		ErrorInvalidCrossForestCredentials,
		// Token: 0x040003FB RID: 1019
		ErrorInvalidDelegatePermission,
		// Token: 0x040003FC RID: 1020
		ErrorInvalidDelegateUserId,
		// Token: 0x040003FD RID: 1021
		ErrorInvalidExcludesRestriction,
		// Token: 0x040003FE RID: 1022
		ErrorInvalidExpressionTypeForSubFilter,
		// Token: 0x040003FF RID: 1023
		ErrorInvalidExtendedProperty,
		// Token: 0x04000400 RID: 1024
		ErrorInvalidExtendedPropertyValue,
		// Token: 0x04000401 RID: 1025
		ErrorInvalidFolderId,
		// Token: 0x04000402 RID: 1026
		ErrorInvalidFolderTypeForOperation,
		// Token: 0x04000403 RID: 1027
		ErrorInvalidFractionalPagingParameters,
		// Token: 0x04000404 RID: 1028
		ErrorInvalidFreeBusyViewType,
		// Token: 0x04000405 RID: 1029
		ErrorInvalidId,
		// Token: 0x04000406 RID: 1030
		ErrorInvalidIdEmpty,
		// Token: 0x04000407 RID: 1031
		ErrorInvalidIdMalformed,
		// Token: 0x04000408 RID: 1032
		ErrorInvalidIdMalformedEwsLegacyIdFormat,
		// Token: 0x04000409 RID: 1033
		ErrorInvalidIdMonikerTooLong,
		// Token: 0x0400040A RID: 1034
		ErrorInvalidIdNotAnItemAttachmentId,
		// Token: 0x0400040B RID: 1035
		ErrorInvalidIdReturnedByResolveNames,
		// Token: 0x0400040C RID: 1036
		ErrorInvalidIdStoreObjectIdTooLong,
		// Token: 0x0400040D RID: 1037
		ErrorInvalidIdTooManyAttachmentLevels,
		// Token: 0x0400040E RID: 1038
		ErrorInvalidIdXml,
		// Token: 0x0400040F RID: 1039
		ErrorInvalidImContactId,
		// Token: 0x04000410 RID: 1040
		ErrorInvalidImDistributionGroupSmtpAddress,
		// Token: 0x04000411 RID: 1041
		ErrorInvalidImGroupId,
		// Token: 0x04000412 RID: 1042
		ErrorInvalidIndexedPagingParameters,
		// Token: 0x04000413 RID: 1043
		ErrorInvalidInternetHeaderChildNodes,
		// Token: 0x04000414 RID: 1044
		ErrorInvalidItemForOperationArchiveItem,
		// Token: 0x04000415 RID: 1045
		ErrorInvalidItemForOperationCreateItemAttachment,
		// Token: 0x04000416 RID: 1046
		ErrorInvalidItemForOperationCreateItem,
		// Token: 0x04000417 RID: 1047
		ErrorInvalidItemForOperationAcceptItem,
		// Token: 0x04000418 RID: 1048
		ErrorInvalidItemForOperationDeclineItem,
		// Token: 0x04000419 RID: 1049
		ErrorInvalidItemForOperationCancelItem,
		// Token: 0x0400041A RID: 1050
		ErrorInvalidItemForOperationExpandDL,
		// Token: 0x0400041B RID: 1051
		ErrorInvalidItemForOperationRemoveItem,
		// Token: 0x0400041C RID: 1052
		ErrorInvalidItemForOperationSendItem,
		// Token: 0x0400041D RID: 1053
		ErrorInvalidItemForOperationTentative,
		// Token: 0x0400041E RID: 1054
		ErrorInvalidLogonType,
		// Token: 0x0400041F RID: 1055
		ErrorInvalidLikeRequest,
		// Token: 0x04000420 RID: 1056
		ErrorInvalidMailbox,
		// Token: 0x04000421 RID: 1057
		ErrorInvalidManagedFolderProperty,
		// Token: 0x04000422 RID: 1058
		ErrorInvalidManagedFolderQuota,
		// Token: 0x04000423 RID: 1059
		ErrorInvalidManagedFolderSize,
		// Token: 0x04000424 RID: 1060
		ErrorInvalidMergedFreeBusyInterval,
		// Token: 0x04000425 RID: 1061
		ErrorInvalidNameForNameResolution,
		// Token: 0x04000426 RID: 1062
		ErrorInvalidOperation,
		// Token: 0x04000427 RID: 1063
		ErrorInvalidNetworkServiceContext,
		// Token: 0x04000428 RID: 1064
		ErrorInvalidOofParameter,
		// Token: 0x04000429 RID: 1065
		ErrorInvalidPagingMaxRows,
		// Token: 0x0400042A RID: 1066
		ErrorInvalidParentFolder,
		// Token: 0x0400042B RID: 1067
		ErrorInvalidPercentCompleteValue,
		// Token: 0x0400042C RID: 1068
		ErrorInvalidPermissionSettings,
		// Token: 0x0400042D RID: 1069
		ErrorInvalidPhoneCallId,
		// Token: 0x0400042E RID: 1070
		ErrorInvalidPhoneNumber,
		// Token: 0x0400042F RID: 1071
		ErrorInvalidUserInfo,
		// Token: 0x04000430 RID: 1072
		ErrorInvalidPropertyAppend,
		// Token: 0x04000431 RID: 1073
		ErrorInvalidPropertyDelete,
		// Token: 0x04000432 RID: 1074
		ErrorInvalidPropertyForExists,
		// Token: 0x04000433 RID: 1075
		ErrorInvalidPropertyForOperation,
		// Token: 0x04000434 RID: 1076
		ErrorInvalidPropertyRequest,
		// Token: 0x04000435 RID: 1077
		ErrorInvalidPropertySet,
		// Token: 0x04000436 RID: 1078
		ErrorInvalidPropertyUpdateSentMessage,
		// Token: 0x04000437 RID: 1079
		ErrorInvalidProxySecurityContext,
		// Token: 0x04000438 RID: 1080
		ErrorInvalidPullSubscriptionId,
		// Token: 0x04000439 RID: 1081
		ErrorInvalidPushSubscriptionUrl,
		// Token: 0x0400043A RID: 1082
		ErrorInvalidRecipients,
		// Token: 0x0400043B RID: 1083
		ErrorInvalidRecipientSubfilter,
		// Token: 0x0400043C RID: 1084
		ErrorInvalidRecipientSubfilterComparison,
		// Token: 0x0400043D RID: 1085
		ErrorInvalidRecipientSubfilterOrder,
		// Token: 0x0400043E RID: 1086
		ErrorInvalidRecipientSubfilterTextFilter,
		// Token: 0x0400043F RID: 1087
		ErrorInvalidReferenceItem,
		// Token: 0x04000440 RID: 1088
		ErrorInvalidRequest,
		// Token: 0x04000441 RID: 1089
		ErrorInvalidRestriction,
		// Token: 0x04000442 RID: 1090
		ErrorInvalidRetentionTagTypeMismatch,
		// Token: 0x04000443 RID: 1091
		ErrorInvalidRetentionTagInvisible,
		// Token: 0x04000444 RID: 1092
		ErrorInvalidRetentionTagInheritance,
		// Token: 0x04000445 RID: 1093
		ErrorInvalidRetentionTagIdGuid,
		// Token: 0x04000446 RID: 1094
		ErrorInvalidRoutingType,
		// Token: 0x04000447 RID: 1095
		ErrorInvalidScheduledOofDuration,
		// Token: 0x04000448 RID: 1096
		ErrorInvalidSchemaVersionForMailboxVersion,
		// Token: 0x04000449 RID: 1097
		ErrorInvalidSecurityDescriptor,
		// Token: 0x0400044A RID: 1098
		ErrorInvalidSendItemSaveSettings,
		// Token: 0x0400044B RID: 1099
		ErrorInvalidSerializedAccessToken,
		// Token: 0x0400044C RID: 1100
		ErrorInvalidServerVersion,
		// Token: 0x0400044D RID: 1101
		ErrorInvalidSid,
		// Token: 0x0400044E RID: 1102
		ErrorInvalidSIPUri,
		// Token: 0x0400044F RID: 1103
		ErrorInvalidSmtpAddress,
		// Token: 0x04000450 RID: 1104
		ErrorInvalidSubfilterType,
		// Token: 0x04000451 RID: 1105
		ErrorInvalidSubfilterTypeNotAttendeeType,
		// Token: 0x04000452 RID: 1106
		ErrorInvalidSubfilterTypeNotRecipientType,
		// Token: 0x04000453 RID: 1107
		ErrorInvalidSubscription,
		// Token: 0x04000454 RID: 1108
		ErrorInvalidSubscriptionRequest,
		// Token: 0x04000455 RID: 1109
		ErrorInvalidSyncStateData,
		// Token: 0x04000456 RID: 1110
		ErrorInvalidTimeInterval,
		// Token: 0x04000457 RID: 1111
		ErrorInvalidUserOofSettings,
		// Token: 0x04000458 RID: 1112
		ErrorInvalidUserPrincipalName,
		// Token: 0x04000459 RID: 1113
		ErrorInvalidUserSid,
		// Token: 0x0400045A RID: 1114
		ErrorInvalidUserSidMissingUPN,
		// Token: 0x0400045B RID: 1115
		ErrorInvalidValueForProperty,
		// Token: 0x0400045C RID: 1116
		ErrorInvalidWatermark,
		// Token: 0x0400045D RID: 1117
		ErrorIPGatewayNotFound,
		// Token: 0x0400045E RID: 1118
		ErrorIrresolvableConflict,
		// Token: 0x0400045F RID: 1119
		ErrorItemCorrupt,
		// Token: 0x04000460 RID: 1120
		ErrorItemNotFound,
		// Token: 0x04000461 RID: 1121
		ErrorItemPropertyRequestFailed,
		// Token: 0x04000462 RID: 1122
		ErrorItemSave,
		// Token: 0x04000463 RID: 1123
		ErrorItemSavePropertyError,
		// Token: 0x04000464 RID: 1124
		ErrorLegacyMailboxFreeBusyViewTypeNotMerged,
		// Token: 0x04000465 RID: 1125
		ErrorLocalServerObjectNotFound,
		// Token: 0x04000466 RID: 1126
		ErrorLogonAsNetworkServiceFailed,
		// Token: 0x04000467 RID: 1127
		ErrorMailboxConfiguration,
		// Token: 0x04000468 RID: 1128
		ErrorMailboxDataArrayEmpty,
		// Token: 0x04000469 RID: 1129
		ErrorMailboxDataArrayTooBig,
		// Token: 0x0400046A RID: 1130
		ErrorMailboxHoldNotFound,
		// Token: 0x0400046B RID: 1131
		ErrorMailboxLogonFailed,
		// Token: 0x0400046C RID: 1132
		ErrorMailboxMoveInProgress,
		// Token: 0x0400046D RID: 1133
		ErrorMailboxStoreUnavailable,
		// Token: 0x0400046E RID: 1134
		ErrorMailRecipientNotFound,
		// Token: 0x0400046F RID: 1135
		ErrorMailTipsDisabled,
		// Token: 0x04000470 RID: 1136
		ErrorManagedFolderAlreadyExists,
		// Token: 0x04000471 RID: 1137
		ErrorManagedFolderNotFound,
		// Token: 0x04000472 RID: 1138
		ErrorManagedFoldersRootFailure,
		// Token: 0x04000473 RID: 1139
		ErrorMeetingSuggestionGenerationFailed,
		// Token: 0x04000474 RID: 1140
		ErrorMessageDispositionRequired,
		// Token: 0x04000475 RID: 1141
		ErrorMessageSizeExceeded,
		// Token: 0x04000476 RID: 1142
		ErrorMimeContentConversionFailed,
		// Token: 0x04000477 RID: 1143
		ErrorMimeContentInvalid,
		// Token: 0x04000478 RID: 1144
		ErrorMimeContentInvalidBase64String,
		// Token: 0x04000479 RID: 1145
		ErrorMissingArgument,
		// Token: 0x0400047A RID: 1146
		ErrorMissingEmailAddress,
		// Token: 0x0400047B RID: 1147
		ErrorMissingEmailAddressForManagedFolder,
		// Token: 0x0400047C RID: 1148
		ErrorMissingInformationEmailAddress,
		// Token: 0x0400047D RID: 1149
		ErrorMissingInformationReferenceItemId,
		// Token: 0x0400047E RID: 1150
		ErrorMissingItemForCreateItemAttachment,
		// Token: 0x0400047F RID: 1151
		ErrorMissingManagedFolderId,
		// Token: 0x04000480 RID: 1152
		ErrorMissingRecipients,
		// Token: 0x04000481 RID: 1153
		ErrorMissingUserIdInformation,
		// Token: 0x04000482 RID: 1154
		ErrorMoreThanOneAccessModeSpecified,
		// Token: 0x04000483 RID: 1155
		ErrorMoveCopyFailed,
		// Token: 0x04000484 RID: 1156
		ErrorMoveDistinguishedFolder,
		// Token: 0x04000485 RID: 1157
		ErrorMultiLegacyMailboxAccess,
		// Token: 0x04000486 RID: 1158
		ErrorNameResolutionMultipleResults,
		// Token: 0x04000487 RID: 1159
		ErrorNameResolutionNoMailbox,
		// Token: 0x04000488 RID: 1160
		ErrorNameResolutionNoResults,
		// Token: 0x04000489 RID: 1161
		ErrorNoApplicableProxyCASServersAvailable,
		// Token: 0x0400048A RID: 1162
		ErrorNoCalendar,
		// Token: 0x0400048B RID: 1163
		ErrorNoDestinationCASDueToKerberosRequirements,
		// Token: 0x0400048C RID: 1164
		ErrorNoDestinationCASDueToSSLRequirements,
		// Token: 0x0400048D RID: 1165
		ErrorNoDestinationCASDueToVersionMismatch,
		// Token: 0x0400048E RID: 1166
		ErrorNoFolderClassOverride,
		// Token: 0x0400048F RID: 1167
		ErrorNoFreeBusyAccess,
		// Token: 0x04000490 RID: 1168
		ErrorNonExistentMailbox,
		// Token: 0x04000491 RID: 1169
		ErrorNonPrimarySmtpAddress,
		// Token: 0x04000492 RID: 1170
		ErrorNoPropertyTagForCustomProperties,
		// Token: 0x04000493 RID: 1171
		ErrorNoPublicFolderReplicaAvailable,
		// Token: 0x04000494 RID: 1172
		ErrorNoPublicFolderServerAvailable,
		// Token: 0x04000495 RID: 1173
		ErrorNoRespondingCASInDestinationSite,
		// Token: 0x04000496 RID: 1174
		ErrorNotDelegate,
		// Token: 0x04000497 RID: 1175
		ErrorNotEnoughMemory,
		// Token: 0x04000498 RID: 1176
		ErrorObjectTypeChanged,
		// Token: 0x04000499 RID: 1177
		ErrorOccurrenceCrossingBoundary,
		// Token: 0x0400049A RID: 1178
		ErrorOccurrenceTimeSpanTooBig,
		// Token: 0x0400049B RID: 1179
		ErrorOperationNotAllowedWithPublicFolderRoot,
		// Token: 0x0400049C RID: 1180
		ErrorParentFolderIdRequired,
		// Token: 0x0400049D RID: 1181
		ErrorParentFolderNotFound,
		// Token: 0x0400049E RID: 1182
		ErrorPasswordChangeRequired,
		// Token: 0x0400049F RID: 1183
		ErrorPasswordExpired,
		// Token: 0x040004A0 RID: 1184
		ErrorPhoneNumberNotDialable,
		// Token: 0x040004A1 RID: 1185
		ErrorPropertyUpdate,
		// Token: 0x040004A2 RID: 1186
		ErrorPromptPublishingOperationFailed,
		// Token: 0x040004A3 RID: 1187
		ErrorPropertyValidationFailure,
		// Token: 0x040004A4 RID: 1188
		ErrorProxiedSubscriptionCallFailure,
		// Token: 0x040004A5 RID: 1189
		ErrorProxyCallFailed,
		// Token: 0x040004A6 RID: 1190
		ErrorProxyGroupSidLimitExceeded,
		// Token: 0x040004A7 RID: 1191
		ErrorProxyRequestNotAllowed,
		// Token: 0x040004A8 RID: 1192
		ErrorProxyRequestProcessingFailed,
		// Token: 0x040004A9 RID: 1193
		ErrorProxyServiceDiscoveryFailed,
		// Token: 0x040004AA RID: 1194
		ErrorProxyTokenExpired,
		// Token: 0x040004AB RID: 1195
		ErrorPublicFolderMailboxDiscoveryFailed,
		// Token: 0x040004AC RID: 1196
		ErrorPublicFolderOperationFailed,
		// Token: 0x040004AD RID: 1197
		ErrorPublicFolderRequestProcessingFailed,
		// Token: 0x040004AE RID: 1198
		ErrorPublicFolderServerNotFound,
		// Token: 0x040004AF RID: 1199
		ErrorPublicFolderSyncException,
		// Token: 0x040004B0 RID: 1200
		ErrorQueryFilterTooLong,
		// Token: 0x040004B1 RID: 1201
		ErrorQuotaExceeded,
		// Token: 0x040004B2 RID: 1202
		ErrorReadEventsFailed,
		// Token: 0x040004B3 RID: 1203
		ErrorReadReceiptNotPending,
		// Token: 0x040004B4 RID: 1204
		ErrorRecurrenceEndDateTooBig,
		// Token: 0x040004B5 RID: 1205
		ErrorRecurrenceHasNoOccurrence,
		// Token: 0x040004B6 RID: 1206
		ErrorRemoveDelegatesFailed,
		// Token: 0x040004B7 RID: 1207
		ErrorRequestAborted,
		// Token: 0x040004B8 RID: 1208
		ErrorRequestStreamTooBig,
		// Token: 0x040004B9 RID: 1209
		ErrorRequiredPropertyMissing,
		// Token: 0x040004BA RID: 1210
		ErrorResolveNamesInvalidFolderType,
		// Token: 0x040004BB RID: 1211
		ErrorResolveNamesOnlyOneContactsFolderAllowed,
		// Token: 0x040004BC RID: 1212
		ErrorResponseSchemaValidation,
		// Token: 0x040004BD RID: 1213
		ErrorRestrictionTooLong,
		// Token: 0x040004BE RID: 1214
		ErrorRestrictionTooComplex,
		// Token: 0x040004BF RID: 1215
		ErrorResultSetTooBig,
		// Token: 0x040004C0 RID: 1216
		ErrorInvalidExchangeImpersonationHeaderData,
		// Token: 0x040004C1 RID: 1217
		ErrorSavedItemFolderNotFound,
		// Token: 0x040004C2 RID: 1218
		ErrorSchemaValidation,
		// Token: 0x040004C3 RID: 1219
		ErrorSearchFolderNotInitialized,
		// Token: 0x040004C4 RID: 1220
		ErrorSendAsDenied,
		// Token: 0x040004C5 RID: 1221
		ErrorSendMeetingCancellationsRequired,
		// Token: 0x040004C6 RID: 1222
		ErrorSendMeetingInvitationsOrCancellationsRequired,
		// Token: 0x040004C7 RID: 1223
		ErrorSendMeetingInvitationsRequired,
		// Token: 0x040004C8 RID: 1224
		ErrorSentMeetingRequestUpdate,
		// Token: 0x040004C9 RID: 1225
		ErrorSentTaskRequestUpdate,
		// Token: 0x040004CA RID: 1226
		ErrorServerBusy,
		// Token: 0x040004CB RID: 1227
		ErrorServiceDiscoveryFailed,
		// Token: 0x040004CC RID: 1228
		ErrorStaleObject,
		// Token: 0x040004CD RID: 1229
		ErrorSubmissionQuotaExceeded,
		// Token: 0x040004CE RID: 1230
		ErrorSubscriptionAccessDenied,
		// Token: 0x040004CF RID: 1231
		ErrorSubscriptionDelegateAccessNotSupported,
		// Token: 0x040004D0 RID: 1232
		ErrorSubscriptionNotFound,
		// Token: 0x040004D1 RID: 1233
		ErrorSubscriptionUnsubscribed,
		// Token: 0x040004D2 RID: 1234
		ErrorSyncFolderNotFound,
		// Token: 0x040004D3 RID: 1235
		ErrorTeamMailboxNotFound,
		// Token: 0x040004D4 RID: 1236
		ErrorTeamMailboxNotLinkedToSharePoint,
		// Token: 0x040004D5 RID: 1237
		ErrorTeamMailboxUrlValidationFailed,
		// Token: 0x040004D6 RID: 1238
		ErrorTeamMailboxNotAuthorizedOwner,
		// Token: 0x040004D7 RID: 1239
		ErrorTeamMailboxActiveToPendingDelete,
		// Token: 0x040004D8 RID: 1240
		ErrorTeamMailboxFailedSendingNotifications,
		// Token: 0x040004D9 RID: 1241
		ErrorTeamMailboxErrorUnknown,
		// Token: 0x040004DA RID: 1242
		ErrorTimeIntervalTooBig,
		// Token: 0x040004DB RID: 1243
		ErrorTimeoutExpired,
		// Token: 0x040004DC RID: 1244
		ErrorTimeZone,
		// Token: 0x040004DD RID: 1245
		ErrorToFolderNotFound,
		// Token: 0x040004DE RID: 1246
		ErrorTokenSerializationDenied,
		// Token: 0x040004DF RID: 1247
		ErrorTooManyObjectsOpened,
		// Token: 0x040004E0 RID: 1248
		ErrorUpdatePropertyMismatch,
		// Token: 0x040004E1 RID: 1249
		ErrorUnifiedMessagingDialPlanNotFound,
		// Token: 0x040004E2 RID: 1250
		ErrorUnifiedMessagingReportDataNotFound,
		// Token: 0x040004E3 RID: 1251
		ErrorUnifiedMessagingPromptNotFound,
		// Token: 0x040004E4 RID: 1252
		ErrorUnifiedMessagingRequestFailed,
		// Token: 0x040004E5 RID: 1253
		ErrorUnifiedMessagingServerNotFound,
		// Token: 0x040004E6 RID: 1254
		ErrorUnableToGetUserOofSettings,
		// Token: 0x040004E7 RID: 1255
		ErrorUnableToRemoveImContactFromGroup,
		// Token: 0x040004E8 RID: 1256
		ErrorUnsupportedSubFilter,
		// Token: 0x040004E9 RID: 1257
		ErrorUnsupportedCulture,
		// Token: 0x040004EA RID: 1258
		ErrorUnsupportedMapiPropertyType,
		// Token: 0x040004EB RID: 1259
		ErrorUnsupportedMimeConversion,
		// Token: 0x040004EC RID: 1260
		ErrorUnsupportedPathForQuery,
		// Token: 0x040004ED RID: 1261
		ErrorUnsupportedPathForSortGroup,
		// Token: 0x040004EE RID: 1262
		ErrorUnsupportedPropertyDefinition,
		// Token: 0x040004EF RID: 1263
		ErrorUnsupportedQueryFilter,
		// Token: 0x040004F0 RID: 1264
		ErrorUnsupportedRecurrence,
		// Token: 0x040004F1 RID: 1265
		ErrorUnsupportedTypeForConversion,
		// Token: 0x040004F2 RID: 1266
		ErrorUpdateDelegatesFailed,
		// Token: 0x040004F3 RID: 1267
		ErrorUserNotUnifiedMessagingEnabled,
		// Token: 0x040004F4 RID: 1268
		ErrorVoiceMailNotImplemented,
		// Token: 0x040004F5 RID: 1269
		ErrorValueOutOfRange,
		// Token: 0x040004F6 RID: 1270
		ErrorVirusDetected,
		// Token: 0x040004F7 RID: 1271
		ErrorVirusMessageDeleted,
		// Token: 0x040004F8 RID: 1272
		ErrorWebRequestInInvalidState,
		// Token: 0x040004F9 RID: 1273
		ErrorWin32InteropError,
		// Token: 0x040004FA RID: 1274
		ErrorWorkingHoursSaveFailed,
		// Token: 0x040004FB RID: 1275
		ErrorWorkingHoursXmlMalformed,
		// Token: 0x040004FC RID: 1276
		ErrorWrongServerVersion,
		// Token: 0x040004FD RID: 1277
		ErrorWrongServerVersionDelegate,
		// Token: 0x040004FE RID: 1278
		ErrorMissingInformationSharingFolderId,
		// Token: 0x040004FF RID: 1279
		ErrorDuplicateSOAPHeader,
		// Token: 0x04000500 RID: 1280
		ErrorSharingSynchronizationFailed,
		// Token: 0x04000501 RID: 1281
		ErrorSharingNoExternalEwsAvailable,
		// Token: 0x04000502 RID: 1282
		ErrorFreeBusyDLLimitReached,
		// Token: 0x04000503 RID: 1283
		ErrorInvalidGetSharingFolderRequest,
		// Token: 0x04000504 RID: 1284
		ErrorNotAllowedExternalSharingByPolicy,
		// Token: 0x04000505 RID: 1285
		ErrorUserNotAllowedByPolicy,
		// Token: 0x04000506 RID: 1286
		ErrorPermissionNotAllowedByPolicy,
		// Token: 0x04000507 RID: 1287
		ErrorOrganizationNotFederated,
		// Token: 0x04000508 RID: 1288
		ErrorMailboxFailover,
		// Token: 0x04000509 RID: 1289
		ErrorInvalidExternalSharingInitiator,
		// Token: 0x0400050A RID: 1290
		ErrorMessageTrackingPermanentError,
		// Token: 0x0400050B RID: 1291
		ErrorMessageTrackingTransientError,
		// Token: 0x0400050C RID: 1292
		ErrorMessageTrackingNoSuchDomain,
		// Token: 0x0400050D RID: 1293
		ErrorUserWithoutFederatedProxyAddress,
		// Token: 0x0400050E RID: 1294
		ErrorInvalidOrganizationRelationshipForFreeBusy,
		// Token: 0x0400050F RID: 1295
		ErrorInvalidFederatedOrganizationId,
		// Token: 0x04000510 RID: 1296
		ErrorInvalidExternalSharingSubscriber,
		// Token: 0x04000511 RID: 1297
		ErrorInvalidSharingData,
		// Token: 0x04000512 RID: 1298
		ErrorInvalidSharingMessage,
		// Token: 0x04000513 RID: 1299
		ErrorNotSupportedSharingMessage,
		// Token: 0x04000514 RID: 1300
		ErrorApplyConversationActionFailed,
		// Token: 0x04000515 RID: 1301
		ErrorInboxRulesValidationError,
		// Token: 0x04000516 RID: 1302
		ErrorOutlookRuleBlobExists,
		// Token: 0x04000517 RID: 1303
		ErrorRulesOverQuota,
		// Token: 0x04000518 RID: 1304
		ErrorNewEventStreamConnectionOpened,
		// Token: 0x04000519 RID: 1305
		ErrorMissedNotificationEvents,
		// Token: 0x0400051A RID: 1306
		ErrorDuplicateLegacyDistinguishedName,
		// Token: 0x0400051B RID: 1307
		ErrorInvalidClientAccessTokenRequest,
		// Token: 0x0400051C RID: 1308
		ErrorNoSpeechDetected,
		// Token: 0x0400051D RID: 1309
		ErrorUMServerUnavailable,
		// Token: 0x0400051E RID: 1310
		ErrorRecipientNotFound,
		// Token: 0x0400051F RID: 1311
		ErrorRecognizerNotInstalled,
		// Token: 0x04000520 RID: 1312
		ErrorSpeechGrammarError,
		// Token: 0x04000521 RID: 1313
		ErrorInvalidManagementRoleHeader,
		// Token: 0x04000522 RID: 1314
		ErrorLocationServicesDisabled,
		// Token: 0x04000523 RID: 1315
		ErrorLocationServicesRequestTimedOut,
		// Token: 0x04000524 RID: 1316
		ErrorLocationServicesRequestFailed,
		// Token: 0x04000525 RID: 1317
		ErrorLocationServicesInvalidRequest,
		// Token: 0x04000526 RID: 1318
		ErrorWeatherServiceDisabled,
		// Token: 0x04000527 RID: 1319
		ErrorMailboxScopeNotAllowedWithoutQueryString,
		// Token: 0x04000528 RID: 1320
		ErrorArchiveMailboxSearchFailed,
		// Token: 0x04000529 RID: 1321
		ErrorGetRemoteArchiveFolderFailed,
		// Token: 0x0400052A RID: 1322
		ErrorFindRemoteArchiveFolderFailed,
		// Token: 0x0400052B RID: 1323
		ErrorGetRemoteArchiveItemFailed,
		// Token: 0x0400052C RID: 1324
		ErrorExportRemoteArchiveItemsFailed,
		// Token: 0x0400052D RID: 1325
		ErrorInvalidPhotoSize,
		// Token: 0x0400052E RID: 1326
		ErrorSearchQueryHasTooManyKeywords,
		// Token: 0x0400052F RID: 1327
		ErrorSearchTooManyMailboxes,
		// Token: 0x04000530 RID: 1328
		ErrorInvalidRetentionTagNone,
		// Token: 0x04000531 RID: 1329
		ErrorDiscoverySearchesDisabled,
		// Token: 0x04000532 RID: 1330
		ErrorCalendarSeekToConditionNotSupported,
		// Token: 0x04000533 RID: 1331
		ErrorCalendarIsGroupMailboxForAccept,
		// Token: 0x04000534 RID: 1332
		ErrorCalendarIsGroupMailboxForDecline,
		// Token: 0x04000535 RID: 1333
		ErrorCalendarIsGroupMailboxForTentative,
		// Token: 0x04000536 RID: 1334
		ErrorCalendarIsGroupMailboxForSuppressReadReceipt,
		// Token: 0x04000537 RID: 1335
		ErrorOrganizationAccessBlocked,
		// Token: 0x04000538 RID: 1336
		ErrorInvalidLicense,
		// Token: 0x04000539 RID: 1337
		ErrorMessagePerFolderCountReceiveQuotaExceeded
	}
}
