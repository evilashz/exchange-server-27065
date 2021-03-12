using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200018C RID: 396
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public enum ResponseCodeType
	{
		// Token: 0x040007C1 RID: 1985
		NoError,
		// Token: 0x040007C2 RID: 1986
		ErrorAccessDenied,
		// Token: 0x040007C3 RID: 1987
		ErrorAccessModeSpecified,
		// Token: 0x040007C4 RID: 1988
		ErrorAccountDisabled,
		// Token: 0x040007C5 RID: 1989
		ErrorAddDelegatesFailed,
		// Token: 0x040007C6 RID: 1990
		ErrorAddressSpaceNotFound,
		// Token: 0x040007C7 RID: 1991
		ErrorADOperation,
		// Token: 0x040007C8 RID: 1992
		ErrorADSessionFilter,
		// Token: 0x040007C9 RID: 1993
		ErrorADUnavailable,
		// Token: 0x040007CA RID: 1994
		ErrorAutoDiscoverFailed,
		// Token: 0x040007CB RID: 1995
		ErrorAffectedTaskOccurrencesRequired,
		// Token: 0x040007CC RID: 1996
		ErrorAttachmentNestLevelLimitExceeded,
		// Token: 0x040007CD RID: 1997
		ErrorAttachmentSizeLimitExceeded,
		// Token: 0x040007CE RID: 1998
		ErrorArchiveFolderPathCreation,
		// Token: 0x040007CF RID: 1999
		ErrorArchiveMailboxNotEnabled,
		// Token: 0x040007D0 RID: 2000
		ErrorArchiveMailboxServiceDiscoveryFailed,
		// Token: 0x040007D1 RID: 2001
		ErrorAvailabilityConfigNotFound,
		// Token: 0x040007D2 RID: 2002
		ErrorBatchProcessingStopped,
		// Token: 0x040007D3 RID: 2003
		ErrorCalendarCannotMoveOrCopyOccurrence,
		// Token: 0x040007D4 RID: 2004
		ErrorCalendarCannotUpdateDeletedItem,
		// Token: 0x040007D5 RID: 2005
		ErrorCalendarCannotUseIdForOccurrenceId,
		// Token: 0x040007D6 RID: 2006
		ErrorCalendarCannotUseIdForRecurringMasterId,
		// Token: 0x040007D7 RID: 2007
		ErrorCalendarDurationIsTooLong,
		// Token: 0x040007D8 RID: 2008
		ErrorCalendarEndDateIsEarlierThanStartDate,
		// Token: 0x040007D9 RID: 2009
		ErrorCalendarFolderIsInvalidForCalendarView,
		// Token: 0x040007DA RID: 2010
		ErrorCalendarInvalidAttributeValue,
		// Token: 0x040007DB RID: 2011
		ErrorCalendarInvalidDayForTimeChangePattern,
		// Token: 0x040007DC RID: 2012
		ErrorCalendarInvalidDayForWeeklyRecurrence,
		// Token: 0x040007DD RID: 2013
		ErrorCalendarInvalidPropertyState,
		// Token: 0x040007DE RID: 2014
		ErrorCalendarInvalidPropertyValue,
		// Token: 0x040007DF RID: 2015
		ErrorCalendarInvalidRecurrence,
		// Token: 0x040007E0 RID: 2016
		ErrorCalendarInvalidTimeZone,
		// Token: 0x040007E1 RID: 2017
		ErrorCalendarIsCancelledForAccept,
		// Token: 0x040007E2 RID: 2018
		ErrorCalendarIsCancelledForDecline,
		// Token: 0x040007E3 RID: 2019
		ErrorCalendarIsCancelledForRemove,
		// Token: 0x040007E4 RID: 2020
		ErrorCalendarIsCancelledForTentative,
		// Token: 0x040007E5 RID: 2021
		ErrorCalendarIsDelegatedForAccept,
		// Token: 0x040007E6 RID: 2022
		ErrorCalendarIsDelegatedForDecline,
		// Token: 0x040007E7 RID: 2023
		ErrorCalendarIsDelegatedForRemove,
		// Token: 0x040007E8 RID: 2024
		ErrorCalendarIsDelegatedForTentative,
		// Token: 0x040007E9 RID: 2025
		ErrorCalendarIsNotOrganizer,
		// Token: 0x040007EA RID: 2026
		ErrorCalendarIsOrganizerForAccept,
		// Token: 0x040007EB RID: 2027
		ErrorCalendarIsOrganizerForDecline,
		// Token: 0x040007EC RID: 2028
		ErrorCalendarIsOrganizerForRemove,
		// Token: 0x040007ED RID: 2029
		ErrorCalendarIsOrganizerForTentative,
		// Token: 0x040007EE RID: 2030
		ErrorCalendarOccurrenceIndexIsOutOfRecurrenceRange,
		// Token: 0x040007EF RID: 2031
		ErrorCalendarOccurrenceIsDeletedFromRecurrence,
		// Token: 0x040007F0 RID: 2032
		ErrorCalendarOutOfRange,
		// Token: 0x040007F1 RID: 2033
		ErrorCalendarMeetingRequestIsOutOfDate,
		// Token: 0x040007F2 RID: 2034
		ErrorCalendarViewRangeTooBig,
		// Token: 0x040007F3 RID: 2035
		ErrorCallerIsInvalidADAccount,
		// Token: 0x040007F4 RID: 2036
		ErrorCannotArchiveCalendarContactTaskFolderException,
		// Token: 0x040007F5 RID: 2037
		ErrorCannotArchiveItemsInPublicFolders,
		// Token: 0x040007F6 RID: 2038
		ErrorCannotArchiveItemsInArchiveMailbox,
		// Token: 0x040007F7 RID: 2039
		ErrorCannotCreateCalendarItemInNonCalendarFolder,
		// Token: 0x040007F8 RID: 2040
		ErrorCannotCreateContactInNonContactFolder,
		// Token: 0x040007F9 RID: 2041
		ErrorCannotCreatePostItemInNonMailFolder,
		// Token: 0x040007FA RID: 2042
		ErrorCannotCreateTaskInNonTaskFolder,
		// Token: 0x040007FB RID: 2043
		ErrorCannotDeleteObject,
		// Token: 0x040007FC RID: 2044
		ErrorCannotDisableMandatoryExtension,
		// Token: 0x040007FD RID: 2045
		ErrorCannotGetSourceFolderPath,
		// Token: 0x040007FE RID: 2046
		ErrorCannotGetExternalEcpUrl,
		// Token: 0x040007FF RID: 2047
		ErrorCannotOpenFileAttachment,
		// Token: 0x04000800 RID: 2048
		ErrorCannotDeleteTaskOccurrence,
		// Token: 0x04000801 RID: 2049
		ErrorCannotEmptyFolder,
		// Token: 0x04000802 RID: 2050
		ErrorCannotSetCalendarPermissionOnNonCalendarFolder,
		// Token: 0x04000803 RID: 2051
		ErrorCannotSetNonCalendarPermissionOnCalendarFolder,
		// Token: 0x04000804 RID: 2052
		ErrorCannotSetPermissionUnknownEntries,
		// Token: 0x04000805 RID: 2053
		ErrorCannotSpecifySearchFolderAsSourceFolder,
		// Token: 0x04000806 RID: 2054
		ErrorCannotUseFolderIdForItemId,
		// Token: 0x04000807 RID: 2055
		ErrorCannotUseItemIdForFolderId,
		// Token: 0x04000808 RID: 2056
		ErrorChangeKeyRequired,
		// Token: 0x04000809 RID: 2057
		ErrorChangeKeyRequiredForWriteOperations,
		// Token: 0x0400080A RID: 2058
		ErrorClientDisconnected,
		// Token: 0x0400080B RID: 2059
		ErrorClientIntentInvalidStateDefinition,
		// Token: 0x0400080C RID: 2060
		ErrorClientIntentNotFound,
		// Token: 0x0400080D RID: 2061
		ErrorConnectionFailed,
		// Token: 0x0400080E RID: 2062
		ErrorContainsFilterWrongType,
		// Token: 0x0400080F RID: 2063
		ErrorContentConversionFailed,
		// Token: 0x04000810 RID: 2064
		ErrorContentIndexingNotEnabled,
		// Token: 0x04000811 RID: 2065
		ErrorCorruptData,
		// Token: 0x04000812 RID: 2066
		ErrorCreateItemAccessDenied,
		// Token: 0x04000813 RID: 2067
		ErrorCreateManagedFolderPartialCompletion,
		// Token: 0x04000814 RID: 2068
		ErrorCreateSubfolderAccessDenied,
		// Token: 0x04000815 RID: 2069
		ErrorCrossMailboxMoveCopy,
		// Token: 0x04000816 RID: 2070
		ErrorCrossSiteRequest,
		// Token: 0x04000817 RID: 2071
		ErrorDataSizeLimitExceeded,
		// Token: 0x04000818 RID: 2072
		ErrorDataSourceOperation,
		// Token: 0x04000819 RID: 2073
		ErrorDelegateAlreadyExists,
		// Token: 0x0400081A RID: 2074
		ErrorDelegateCannotAddOwner,
		// Token: 0x0400081B RID: 2075
		ErrorDelegateMissingConfiguration,
		// Token: 0x0400081C RID: 2076
		ErrorDelegateNoUser,
		// Token: 0x0400081D RID: 2077
		ErrorDelegateValidationFailed,
		// Token: 0x0400081E RID: 2078
		ErrorDeleteDistinguishedFolder,
		// Token: 0x0400081F RID: 2079
		ErrorDeleteItemsFailed,
		// Token: 0x04000820 RID: 2080
		ErrorDeleteUnifiedMessagingPromptFailed,
		// Token: 0x04000821 RID: 2081
		ErrorDistinguishedUserNotSupported,
		// Token: 0x04000822 RID: 2082
		ErrorDistributionListMemberNotExist,
		// Token: 0x04000823 RID: 2083
		ErrorDuplicateInputFolderNames,
		// Token: 0x04000824 RID: 2084
		ErrorDuplicateUserIdsSpecified,
		// Token: 0x04000825 RID: 2085
		ErrorEmailAddressMismatch,
		// Token: 0x04000826 RID: 2086
		ErrorEventNotFound,
		// Token: 0x04000827 RID: 2087
		ErrorExceededConnectionCount,
		// Token: 0x04000828 RID: 2088
		ErrorExceededSubscriptionCount,
		// Token: 0x04000829 RID: 2089
		ErrorExceededFindCountLimit,
		// Token: 0x0400082A RID: 2090
		ErrorExpiredSubscription,
		// Token: 0x0400082B RID: 2091
		ErrorExtensionNotFound,
		// Token: 0x0400082C RID: 2092
		ErrorFolderCorrupt,
		// Token: 0x0400082D RID: 2093
		ErrorFolderNotFound,
		// Token: 0x0400082E RID: 2094
		ErrorFolderPropertRequestFailed,
		// Token: 0x0400082F RID: 2095
		ErrorFolderSave,
		// Token: 0x04000830 RID: 2096
		ErrorFolderSaveFailed,
		// Token: 0x04000831 RID: 2097
		ErrorFolderSavePropertyError,
		// Token: 0x04000832 RID: 2098
		ErrorFolderExists,
		// Token: 0x04000833 RID: 2099
		ErrorFreeBusyGenerationFailed,
		// Token: 0x04000834 RID: 2100
		ErrorGetServerSecurityDescriptorFailed,
		// Token: 0x04000835 RID: 2101
		ErrorImContactLimitReached,
		// Token: 0x04000836 RID: 2102
		ErrorImGroupDisplayNameAlreadyExists,
		// Token: 0x04000837 RID: 2103
		ErrorImGroupLimitReached,
		// Token: 0x04000838 RID: 2104
		ErrorImpersonateUserDenied,
		// Token: 0x04000839 RID: 2105
		ErrorImpersonationDenied,
		// Token: 0x0400083A RID: 2106
		ErrorImpersonationFailed,
		// Token: 0x0400083B RID: 2107
		ErrorIncorrectSchemaVersion,
		// Token: 0x0400083C RID: 2108
		ErrorIncorrectUpdatePropertyCount,
		// Token: 0x0400083D RID: 2109
		ErrorIndividualMailboxLimitReached,
		// Token: 0x0400083E RID: 2110
		ErrorInsufficientResources,
		// Token: 0x0400083F RID: 2111
		ErrorInternalServerError,
		// Token: 0x04000840 RID: 2112
		ErrorInternalServerTransientError,
		// Token: 0x04000841 RID: 2113
		ErrorInvalidAccessLevel,
		// Token: 0x04000842 RID: 2114
		ErrorInvalidArgument,
		// Token: 0x04000843 RID: 2115
		ErrorInvalidAttachmentId,
		// Token: 0x04000844 RID: 2116
		ErrorInvalidAttachmentSubfilter,
		// Token: 0x04000845 RID: 2117
		ErrorInvalidAttachmentSubfilterTextFilter,
		// Token: 0x04000846 RID: 2118
		ErrorInvalidAuthorizationContext,
		// Token: 0x04000847 RID: 2119
		ErrorInvalidChangeKey,
		// Token: 0x04000848 RID: 2120
		ErrorInvalidClientSecurityContext,
		// Token: 0x04000849 RID: 2121
		ErrorInvalidCompleteDate,
		// Token: 0x0400084A RID: 2122
		ErrorInvalidContactEmailAddress,
		// Token: 0x0400084B RID: 2123
		ErrorInvalidContactEmailIndex,
		// Token: 0x0400084C RID: 2124
		ErrorInvalidCrossForestCredentials,
		// Token: 0x0400084D RID: 2125
		ErrorInvalidDelegatePermission,
		// Token: 0x0400084E RID: 2126
		ErrorInvalidDelegateUserId,
		// Token: 0x0400084F RID: 2127
		ErrorInvalidExcludesRestriction,
		// Token: 0x04000850 RID: 2128
		ErrorInvalidExpressionTypeForSubFilter,
		// Token: 0x04000851 RID: 2129
		ErrorInvalidExtendedProperty,
		// Token: 0x04000852 RID: 2130
		ErrorInvalidExtendedPropertyValue,
		// Token: 0x04000853 RID: 2131
		ErrorInvalidFolderId,
		// Token: 0x04000854 RID: 2132
		ErrorInvalidFolderTypeForOperation,
		// Token: 0x04000855 RID: 2133
		ErrorInvalidFractionalPagingParameters,
		// Token: 0x04000856 RID: 2134
		ErrorInvalidFreeBusyViewType,
		// Token: 0x04000857 RID: 2135
		ErrorInvalidId,
		// Token: 0x04000858 RID: 2136
		ErrorInvalidIdEmpty,
		// Token: 0x04000859 RID: 2137
		ErrorInvalidIdMalformed,
		// Token: 0x0400085A RID: 2138
		ErrorInvalidIdMalformedEwsLegacyIdFormat,
		// Token: 0x0400085B RID: 2139
		ErrorInvalidIdMonikerTooLong,
		// Token: 0x0400085C RID: 2140
		ErrorInvalidIdNotAnItemAttachmentId,
		// Token: 0x0400085D RID: 2141
		ErrorInvalidIdReturnedByResolveNames,
		// Token: 0x0400085E RID: 2142
		ErrorInvalidIdStoreObjectIdTooLong,
		// Token: 0x0400085F RID: 2143
		ErrorInvalidIdTooManyAttachmentLevels,
		// Token: 0x04000860 RID: 2144
		ErrorInvalidIdXml,
		// Token: 0x04000861 RID: 2145
		ErrorInvalidImContactId,
		// Token: 0x04000862 RID: 2146
		ErrorInvalidImDistributionGroupSmtpAddress,
		// Token: 0x04000863 RID: 2147
		ErrorInvalidImGroupId,
		// Token: 0x04000864 RID: 2148
		ErrorInvalidIndexedPagingParameters,
		// Token: 0x04000865 RID: 2149
		ErrorInvalidInternetHeaderChildNodes,
		// Token: 0x04000866 RID: 2150
		ErrorInvalidItemForOperationArchiveItem,
		// Token: 0x04000867 RID: 2151
		ErrorInvalidItemForOperationCreateItemAttachment,
		// Token: 0x04000868 RID: 2152
		ErrorInvalidItemForOperationCreateItem,
		// Token: 0x04000869 RID: 2153
		ErrorInvalidItemForOperationAcceptItem,
		// Token: 0x0400086A RID: 2154
		ErrorInvalidItemForOperationDeclineItem,
		// Token: 0x0400086B RID: 2155
		ErrorInvalidItemForOperationCancelItem,
		// Token: 0x0400086C RID: 2156
		ErrorInvalidItemForOperationExpandDL,
		// Token: 0x0400086D RID: 2157
		ErrorInvalidItemForOperationRemoveItem,
		// Token: 0x0400086E RID: 2158
		ErrorInvalidItemForOperationSendItem,
		// Token: 0x0400086F RID: 2159
		ErrorInvalidItemForOperationTentative,
		// Token: 0x04000870 RID: 2160
		ErrorInvalidLogonType,
		// Token: 0x04000871 RID: 2161
		ErrorInvalidLikeRequest,
		// Token: 0x04000872 RID: 2162
		ErrorInvalidMailbox,
		// Token: 0x04000873 RID: 2163
		ErrorInvalidManagedFolderProperty,
		// Token: 0x04000874 RID: 2164
		ErrorInvalidManagedFolderQuota,
		// Token: 0x04000875 RID: 2165
		ErrorInvalidManagedFolderSize,
		// Token: 0x04000876 RID: 2166
		ErrorInvalidMergedFreeBusyInterval,
		// Token: 0x04000877 RID: 2167
		ErrorInvalidNameForNameResolution,
		// Token: 0x04000878 RID: 2168
		ErrorInvalidOperation,
		// Token: 0x04000879 RID: 2169
		ErrorInvalidNetworkServiceContext,
		// Token: 0x0400087A RID: 2170
		ErrorInvalidOofParameter,
		// Token: 0x0400087B RID: 2171
		ErrorInvalidPagingMaxRows,
		// Token: 0x0400087C RID: 2172
		ErrorInvalidParentFolder,
		// Token: 0x0400087D RID: 2173
		ErrorInvalidPercentCompleteValue,
		// Token: 0x0400087E RID: 2174
		ErrorInvalidPermissionSettings,
		// Token: 0x0400087F RID: 2175
		ErrorInvalidPhoneCallId,
		// Token: 0x04000880 RID: 2176
		ErrorInvalidPhoneNumber,
		// Token: 0x04000881 RID: 2177
		ErrorInvalidUserInfo,
		// Token: 0x04000882 RID: 2178
		ErrorInvalidPropertyAppend,
		// Token: 0x04000883 RID: 2179
		ErrorInvalidPropertyDelete,
		// Token: 0x04000884 RID: 2180
		ErrorInvalidPropertyForExists,
		// Token: 0x04000885 RID: 2181
		ErrorInvalidPropertyForOperation,
		// Token: 0x04000886 RID: 2182
		ErrorInvalidPropertyRequest,
		// Token: 0x04000887 RID: 2183
		ErrorInvalidPropertySet,
		// Token: 0x04000888 RID: 2184
		ErrorInvalidPropertyUpdateSentMessage,
		// Token: 0x04000889 RID: 2185
		ErrorInvalidProxySecurityContext,
		// Token: 0x0400088A RID: 2186
		ErrorInvalidPullSubscriptionId,
		// Token: 0x0400088B RID: 2187
		ErrorInvalidPushSubscriptionUrl,
		// Token: 0x0400088C RID: 2188
		ErrorInvalidRecipients,
		// Token: 0x0400088D RID: 2189
		ErrorInvalidRecipientSubfilter,
		// Token: 0x0400088E RID: 2190
		ErrorInvalidRecipientSubfilterComparison,
		// Token: 0x0400088F RID: 2191
		ErrorInvalidRecipientSubfilterOrder,
		// Token: 0x04000890 RID: 2192
		ErrorInvalidRecipientSubfilterTextFilter,
		// Token: 0x04000891 RID: 2193
		ErrorInvalidReferenceItem,
		// Token: 0x04000892 RID: 2194
		ErrorInvalidRequest,
		// Token: 0x04000893 RID: 2195
		ErrorInvalidRestriction,
		// Token: 0x04000894 RID: 2196
		ErrorInvalidRetentionTagTypeMismatch,
		// Token: 0x04000895 RID: 2197
		ErrorInvalidRetentionTagInvisible,
		// Token: 0x04000896 RID: 2198
		ErrorInvalidRetentionTagInheritance,
		// Token: 0x04000897 RID: 2199
		ErrorInvalidRetentionTagIdGuid,
		// Token: 0x04000898 RID: 2200
		ErrorInvalidRoutingType,
		// Token: 0x04000899 RID: 2201
		ErrorInvalidScheduledOofDuration,
		// Token: 0x0400089A RID: 2202
		ErrorInvalidSchemaVersionForMailboxVersion,
		// Token: 0x0400089B RID: 2203
		ErrorInvalidSecurityDescriptor,
		// Token: 0x0400089C RID: 2204
		ErrorInvalidSendItemSaveSettings,
		// Token: 0x0400089D RID: 2205
		ErrorInvalidSerializedAccessToken,
		// Token: 0x0400089E RID: 2206
		ErrorInvalidServerVersion,
		// Token: 0x0400089F RID: 2207
		ErrorInvalidSid,
		// Token: 0x040008A0 RID: 2208
		ErrorInvalidSIPUri,
		// Token: 0x040008A1 RID: 2209
		ErrorInvalidSmtpAddress,
		// Token: 0x040008A2 RID: 2210
		ErrorInvalidSubfilterType,
		// Token: 0x040008A3 RID: 2211
		ErrorInvalidSubfilterTypeNotAttendeeType,
		// Token: 0x040008A4 RID: 2212
		ErrorInvalidSubfilterTypeNotRecipientType,
		// Token: 0x040008A5 RID: 2213
		ErrorInvalidSubscription,
		// Token: 0x040008A6 RID: 2214
		ErrorInvalidSubscriptionRequest,
		// Token: 0x040008A7 RID: 2215
		ErrorInvalidSyncStateData,
		// Token: 0x040008A8 RID: 2216
		ErrorInvalidTimeInterval,
		// Token: 0x040008A9 RID: 2217
		ErrorInvalidUserOofSettings,
		// Token: 0x040008AA RID: 2218
		ErrorInvalidUserPrincipalName,
		// Token: 0x040008AB RID: 2219
		ErrorInvalidUserSid,
		// Token: 0x040008AC RID: 2220
		ErrorInvalidUserSidMissingUPN,
		// Token: 0x040008AD RID: 2221
		ErrorInvalidValueForProperty,
		// Token: 0x040008AE RID: 2222
		ErrorInvalidWatermark,
		// Token: 0x040008AF RID: 2223
		ErrorIPGatewayNotFound,
		// Token: 0x040008B0 RID: 2224
		ErrorIrresolvableConflict,
		// Token: 0x040008B1 RID: 2225
		ErrorItemCorrupt,
		// Token: 0x040008B2 RID: 2226
		ErrorItemNotFound,
		// Token: 0x040008B3 RID: 2227
		ErrorItemPropertyRequestFailed,
		// Token: 0x040008B4 RID: 2228
		ErrorItemSave,
		// Token: 0x040008B5 RID: 2229
		ErrorItemSavePropertyError,
		// Token: 0x040008B6 RID: 2230
		ErrorLegacyMailboxFreeBusyViewTypeNotMerged,
		// Token: 0x040008B7 RID: 2231
		ErrorLocalServerObjectNotFound,
		// Token: 0x040008B8 RID: 2232
		ErrorLogonAsNetworkServiceFailed,
		// Token: 0x040008B9 RID: 2233
		ErrorMailboxConfiguration,
		// Token: 0x040008BA RID: 2234
		ErrorMailboxDataArrayEmpty,
		// Token: 0x040008BB RID: 2235
		ErrorMailboxDataArrayTooBig,
		// Token: 0x040008BC RID: 2236
		ErrorMailboxHoldNotFound,
		// Token: 0x040008BD RID: 2237
		ErrorMailboxLogonFailed,
		// Token: 0x040008BE RID: 2238
		ErrorMailboxMoveInProgress,
		// Token: 0x040008BF RID: 2239
		ErrorMailboxStoreUnavailable,
		// Token: 0x040008C0 RID: 2240
		ErrorMailRecipientNotFound,
		// Token: 0x040008C1 RID: 2241
		ErrorMailTipsDisabled,
		// Token: 0x040008C2 RID: 2242
		ErrorManagedFolderAlreadyExists,
		// Token: 0x040008C3 RID: 2243
		ErrorManagedFolderNotFound,
		// Token: 0x040008C4 RID: 2244
		ErrorManagedFoldersRootFailure,
		// Token: 0x040008C5 RID: 2245
		ErrorMeetingSuggestionGenerationFailed,
		// Token: 0x040008C6 RID: 2246
		ErrorMessageDispositionRequired,
		// Token: 0x040008C7 RID: 2247
		ErrorMessageSizeExceeded,
		// Token: 0x040008C8 RID: 2248
		ErrorMimeContentConversionFailed,
		// Token: 0x040008C9 RID: 2249
		ErrorMimeContentInvalid,
		// Token: 0x040008CA RID: 2250
		ErrorMimeContentInvalidBase64String,
		// Token: 0x040008CB RID: 2251
		ErrorMissingArgument,
		// Token: 0x040008CC RID: 2252
		ErrorMissingEmailAddress,
		// Token: 0x040008CD RID: 2253
		ErrorMissingEmailAddressForManagedFolder,
		// Token: 0x040008CE RID: 2254
		ErrorMissingInformationEmailAddress,
		// Token: 0x040008CF RID: 2255
		ErrorMissingInformationReferenceItemId,
		// Token: 0x040008D0 RID: 2256
		ErrorMissingItemForCreateItemAttachment,
		// Token: 0x040008D1 RID: 2257
		ErrorMissingManagedFolderId,
		// Token: 0x040008D2 RID: 2258
		ErrorMissingRecipients,
		// Token: 0x040008D3 RID: 2259
		ErrorMissingUserIdInformation,
		// Token: 0x040008D4 RID: 2260
		ErrorMoreThanOneAccessModeSpecified,
		// Token: 0x040008D5 RID: 2261
		ErrorMoveCopyFailed,
		// Token: 0x040008D6 RID: 2262
		ErrorMoveDistinguishedFolder,
		// Token: 0x040008D7 RID: 2263
		ErrorMultiLegacyMailboxAccess,
		// Token: 0x040008D8 RID: 2264
		ErrorNameResolutionMultipleResults,
		// Token: 0x040008D9 RID: 2265
		ErrorNameResolutionNoMailbox,
		// Token: 0x040008DA RID: 2266
		ErrorNameResolutionNoResults,
		// Token: 0x040008DB RID: 2267
		ErrorNoApplicableProxyCASServersAvailable,
		// Token: 0x040008DC RID: 2268
		ErrorNoCalendar,
		// Token: 0x040008DD RID: 2269
		ErrorNoDestinationCASDueToKerberosRequirements,
		// Token: 0x040008DE RID: 2270
		ErrorNoDestinationCASDueToSSLRequirements,
		// Token: 0x040008DF RID: 2271
		ErrorNoDestinationCASDueToVersionMismatch,
		// Token: 0x040008E0 RID: 2272
		ErrorNoFolderClassOverride,
		// Token: 0x040008E1 RID: 2273
		ErrorNoFreeBusyAccess,
		// Token: 0x040008E2 RID: 2274
		ErrorNonExistentMailbox,
		// Token: 0x040008E3 RID: 2275
		ErrorNonPrimarySmtpAddress,
		// Token: 0x040008E4 RID: 2276
		ErrorNoPropertyTagForCustomProperties,
		// Token: 0x040008E5 RID: 2277
		ErrorNoPublicFolderReplicaAvailable,
		// Token: 0x040008E6 RID: 2278
		ErrorNoPublicFolderServerAvailable,
		// Token: 0x040008E7 RID: 2279
		ErrorNoRespondingCASInDestinationSite,
		// Token: 0x040008E8 RID: 2280
		ErrorNotDelegate,
		// Token: 0x040008E9 RID: 2281
		ErrorNotEnoughMemory,
		// Token: 0x040008EA RID: 2282
		ErrorObjectTypeChanged,
		// Token: 0x040008EB RID: 2283
		ErrorOccurrenceCrossingBoundary,
		// Token: 0x040008EC RID: 2284
		ErrorOccurrenceTimeSpanTooBig,
		// Token: 0x040008ED RID: 2285
		ErrorOperationNotAllowedWithPublicFolderRoot,
		// Token: 0x040008EE RID: 2286
		ErrorParentFolderIdRequired,
		// Token: 0x040008EF RID: 2287
		ErrorParentFolderNotFound,
		// Token: 0x040008F0 RID: 2288
		ErrorPasswordChangeRequired,
		// Token: 0x040008F1 RID: 2289
		ErrorPasswordExpired,
		// Token: 0x040008F2 RID: 2290
		ErrorPhoneNumberNotDialable,
		// Token: 0x040008F3 RID: 2291
		ErrorPropertyUpdate,
		// Token: 0x040008F4 RID: 2292
		ErrorPromptPublishingOperationFailed,
		// Token: 0x040008F5 RID: 2293
		ErrorPropertyValidationFailure,
		// Token: 0x040008F6 RID: 2294
		ErrorProxiedSubscriptionCallFailure,
		// Token: 0x040008F7 RID: 2295
		ErrorProxyCallFailed,
		// Token: 0x040008F8 RID: 2296
		ErrorProxyGroupSidLimitExceeded,
		// Token: 0x040008F9 RID: 2297
		ErrorProxyRequestNotAllowed,
		// Token: 0x040008FA RID: 2298
		ErrorProxyRequestProcessingFailed,
		// Token: 0x040008FB RID: 2299
		ErrorProxyServiceDiscoveryFailed,
		// Token: 0x040008FC RID: 2300
		ErrorProxyTokenExpired,
		// Token: 0x040008FD RID: 2301
		ErrorPublicFolderMailboxDiscoveryFailed,
		// Token: 0x040008FE RID: 2302
		ErrorPublicFolderOperationFailed,
		// Token: 0x040008FF RID: 2303
		ErrorPublicFolderRequestProcessingFailed,
		// Token: 0x04000900 RID: 2304
		ErrorPublicFolderServerNotFound,
		// Token: 0x04000901 RID: 2305
		ErrorPublicFolderSyncException,
		// Token: 0x04000902 RID: 2306
		ErrorQueryFilterTooLong,
		// Token: 0x04000903 RID: 2307
		ErrorQuotaExceeded,
		// Token: 0x04000904 RID: 2308
		ErrorReadEventsFailed,
		// Token: 0x04000905 RID: 2309
		ErrorReadReceiptNotPending,
		// Token: 0x04000906 RID: 2310
		ErrorRecurrenceEndDateTooBig,
		// Token: 0x04000907 RID: 2311
		ErrorRecurrenceHasNoOccurrence,
		// Token: 0x04000908 RID: 2312
		ErrorRemoveDelegatesFailed,
		// Token: 0x04000909 RID: 2313
		ErrorRequestAborted,
		// Token: 0x0400090A RID: 2314
		ErrorRequestStreamTooBig,
		// Token: 0x0400090B RID: 2315
		ErrorRequiredPropertyMissing,
		// Token: 0x0400090C RID: 2316
		ErrorResolveNamesInvalidFolderType,
		// Token: 0x0400090D RID: 2317
		ErrorResolveNamesOnlyOneContactsFolderAllowed,
		// Token: 0x0400090E RID: 2318
		ErrorResponseSchemaValidation,
		// Token: 0x0400090F RID: 2319
		ErrorRestrictionTooLong,
		// Token: 0x04000910 RID: 2320
		ErrorRestrictionTooComplex,
		// Token: 0x04000911 RID: 2321
		ErrorResultSetTooBig,
		// Token: 0x04000912 RID: 2322
		ErrorInvalidExchangeImpersonationHeaderData,
		// Token: 0x04000913 RID: 2323
		ErrorSavedItemFolderNotFound,
		// Token: 0x04000914 RID: 2324
		ErrorSchemaValidation,
		// Token: 0x04000915 RID: 2325
		ErrorSearchFolderNotInitialized,
		// Token: 0x04000916 RID: 2326
		ErrorSendAsDenied,
		// Token: 0x04000917 RID: 2327
		ErrorSendMeetingCancellationsRequired,
		// Token: 0x04000918 RID: 2328
		ErrorSendMeetingInvitationsOrCancellationsRequired,
		// Token: 0x04000919 RID: 2329
		ErrorSendMeetingInvitationsRequired,
		// Token: 0x0400091A RID: 2330
		ErrorSentMeetingRequestUpdate,
		// Token: 0x0400091B RID: 2331
		ErrorSentTaskRequestUpdate,
		// Token: 0x0400091C RID: 2332
		ErrorServerBusy,
		// Token: 0x0400091D RID: 2333
		ErrorServiceDiscoveryFailed,
		// Token: 0x0400091E RID: 2334
		ErrorStaleObject,
		// Token: 0x0400091F RID: 2335
		ErrorSubmissionQuotaExceeded,
		// Token: 0x04000920 RID: 2336
		ErrorSubscriptionAccessDenied,
		// Token: 0x04000921 RID: 2337
		ErrorSubscriptionDelegateAccessNotSupported,
		// Token: 0x04000922 RID: 2338
		ErrorSubscriptionNotFound,
		// Token: 0x04000923 RID: 2339
		ErrorSubscriptionUnsubscribed,
		// Token: 0x04000924 RID: 2340
		ErrorSyncFolderNotFound,
		// Token: 0x04000925 RID: 2341
		ErrorTeamMailboxNotFound,
		// Token: 0x04000926 RID: 2342
		ErrorTeamMailboxNotLinkedToSharePoint,
		// Token: 0x04000927 RID: 2343
		ErrorTeamMailboxUrlValidationFailed,
		// Token: 0x04000928 RID: 2344
		ErrorTeamMailboxNotAuthorizedOwner,
		// Token: 0x04000929 RID: 2345
		ErrorTeamMailboxActiveToPendingDelete,
		// Token: 0x0400092A RID: 2346
		ErrorTeamMailboxFailedSendingNotifications,
		// Token: 0x0400092B RID: 2347
		ErrorTeamMailboxErrorUnknown,
		// Token: 0x0400092C RID: 2348
		ErrorTimeIntervalTooBig,
		// Token: 0x0400092D RID: 2349
		ErrorTimeoutExpired,
		// Token: 0x0400092E RID: 2350
		ErrorTimeZone,
		// Token: 0x0400092F RID: 2351
		ErrorToFolderNotFound,
		// Token: 0x04000930 RID: 2352
		ErrorTokenSerializationDenied,
		// Token: 0x04000931 RID: 2353
		ErrorTooManyObjectsOpened,
		// Token: 0x04000932 RID: 2354
		ErrorUpdatePropertyMismatch,
		// Token: 0x04000933 RID: 2355
		ErrorUnifiedMessagingDialPlanNotFound,
		// Token: 0x04000934 RID: 2356
		ErrorUnifiedMessagingReportDataNotFound,
		// Token: 0x04000935 RID: 2357
		ErrorUnifiedMessagingPromptNotFound,
		// Token: 0x04000936 RID: 2358
		ErrorUnifiedMessagingRequestFailed,
		// Token: 0x04000937 RID: 2359
		ErrorUnifiedMessagingServerNotFound,
		// Token: 0x04000938 RID: 2360
		ErrorUnableToGetUserOofSettings,
		// Token: 0x04000939 RID: 2361
		ErrorUnableToRemoveImContactFromGroup,
		// Token: 0x0400093A RID: 2362
		ErrorUnsupportedSubFilter,
		// Token: 0x0400093B RID: 2363
		ErrorUnsupportedCulture,
		// Token: 0x0400093C RID: 2364
		ErrorUnsupportedMapiPropertyType,
		// Token: 0x0400093D RID: 2365
		ErrorUnsupportedMimeConversion,
		// Token: 0x0400093E RID: 2366
		ErrorUnsupportedPathForQuery,
		// Token: 0x0400093F RID: 2367
		ErrorUnsupportedPathForSortGroup,
		// Token: 0x04000940 RID: 2368
		ErrorUnsupportedPropertyDefinition,
		// Token: 0x04000941 RID: 2369
		ErrorUnsupportedQueryFilter,
		// Token: 0x04000942 RID: 2370
		ErrorUnsupportedRecurrence,
		// Token: 0x04000943 RID: 2371
		ErrorUnsupportedTypeForConversion,
		// Token: 0x04000944 RID: 2372
		ErrorUpdateDelegatesFailed,
		// Token: 0x04000945 RID: 2373
		ErrorUserNotUnifiedMessagingEnabled,
		// Token: 0x04000946 RID: 2374
		ErrorVoiceMailNotImplemented,
		// Token: 0x04000947 RID: 2375
		ErrorValueOutOfRange,
		// Token: 0x04000948 RID: 2376
		ErrorVirusDetected,
		// Token: 0x04000949 RID: 2377
		ErrorVirusMessageDeleted,
		// Token: 0x0400094A RID: 2378
		ErrorWebRequestInInvalidState,
		// Token: 0x0400094B RID: 2379
		ErrorWin32InteropError,
		// Token: 0x0400094C RID: 2380
		ErrorWorkingHoursSaveFailed,
		// Token: 0x0400094D RID: 2381
		ErrorWorkingHoursXmlMalformed,
		// Token: 0x0400094E RID: 2382
		ErrorWrongServerVersion,
		// Token: 0x0400094F RID: 2383
		ErrorWrongServerVersionDelegate,
		// Token: 0x04000950 RID: 2384
		ErrorMissingInformationSharingFolderId,
		// Token: 0x04000951 RID: 2385
		ErrorDuplicateSOAPHeader,
		// Token: 0x04000952 RID: 2386
		ErrorSharingSynchronizationFailed,
		// Token: 0x04000953 RID: 2387
		ErrorSharingNoExternalEwsAvailable,
		// Token: 0x04000954 RID: 2388
		ErrorFreeBusyDLLimitReached,
		// Token: 0x04000955 RID: 2389
		ErrorInvalidGetSharingFolderRequest,
		// Token: 0x04000956 RID: 2390
		ErrorNotAllowedExternalSharingByPolicy,
		// Token: 0x04000957 RID: 2391
		ErrorUserNotAllowedByPolicy,
		// Token: 0x04000958 RID: 2392
		ErrorPermissionNotAllowedByPolicy,
		// Token: 0x04000959 RID: 2393
		ErrorOrganizationNotFederated,
		// Token: 0x0400095A RID: 2394
		ErrorMailboxFailover,
		// Token: 0x0400095B RID: 2395
		ErrorInvalidExternalSharingInitiator,
		// Token: 0x0400095C RID: 2396
		ErrorMessageTrackingPermanentError,
		// Token: 0x0400095D RID: 2397
		ErrorMessageTrackingTransientError,
		// Token: 0x0400095E RID: 2398
		ErrorMessageTrackingNoSuchDomain,
		// Token: 0x0400095F RID: 2399
		ErrorUserWithoutFederatedProxyAddress,
		// Token: 0x04000960 RID: 2400
		ErrorInvalidOrganizationRelationshipForFreeBusy,
		// Token: 0x04000961 RID: 2401
		ErrorInvalidFederatedOrganizationId,
		// Token: 0x04000962 RID: 2402
		ErrorInvalidExternalSharingSubscriber,
		// Token: 0x04000963 RID: 2403
		ErrorInvalidSharingData,
		// Token: 0x04000964 RID: 2404
		ErrorInvalidSharingMessage,
		// Token: 0x04000965 RID: 2405
		ErrorNotSupportedSharingMessage,
		// Token: 0x04000966 RID: 2406
		ErrorApplyConversationActionFailed,
		// Token: 0x04000967 RID: 2407
		ErrorInboxRulesValidationError,
		// Token: 0x04000968 RID: 2408
		ErrorOutlookRuleBlobExists,
		// Token: 0x04000969 RID: 2409
		ErrorRulesOverQuota,
		// Token: 0x0400096A RID: 2410
		ErrorNewEventStreamConnectionOpened,
		// Token: 0x0400096B RID: 2411
		ErrorMissedNotificationEvents,
		// Token: 0x0400096C RID: 2412
		ErrorDuplicateLegacyDistinguishedName,
		// Token: 0x0400096D RID: 2413
		ErrorInvalidClientAccessTokenRequest,
		// Token: 0x0400096E RID: 2414
		ErrorNoSpeechDetected,
		// Token: 0x0400096F RID: 2415
		ErrorUMServerUnavailable,
		// Token: 0x04000970 RID: 2416
		ErrorRecipientNotFound,
		// Token: 0x04000971 RID: 2417
		ErrorRecognizerNotInstalled,
		// Token: 0x04000972 RID: 2418
		ErrorSpeechGrammarError,
		// Token: 0x04000973 RID: 2419
		ErrorInvalidManagementRoleHeader,
		// Token: 0x04000974 RID: 2420
		ErrorLocationServicesDisabled,
		// Token: 0x04000975 RID: 2421
		ErrorLocationServicesRequestTimedOut,
		// Token: 0x04000976 RID: 2422
		ErrorLocationServicesRequestFailed,
		// Token: 0x04000977 RID: 2423
		ErrorLocationServicesInvalidRequest,
		// Token: 0x04000978 RID: 2424
		ErrorWeatherServiceDisabled,
		// Token: 0x04000979 RID: 2425
		ErrorMailboxScopeNotAllowedWithoutQueryString,
		// Token: 0x0400097A RID: 2426
		ErrorArchiveMailboxSearchFailed,
		// Token: 0x0400097B RID: 2427
		ErrorGetRemoteArchiveFolderFailed,
		// Token: 0x0400097C RID: 2428
		ErrorFindRemoteArchiveFolderFailed,
		// Token: 0x0400097D RID: 2429
		ErrorGetRemoteArchiveItemFailed,
		// Token: 0x0400097E RID: 2430
		ErrorExportRemoteArchiveItemsFailed,
		// Token: 0x0400097F RID: 2431
		ErrorInvalidPhotoSize,
		// Token: 0x04000980 RID: 2432
		ErrorSearchQueryHasTooManyKeywords,
		// Token: 0x04000981 RID: 2433
		ErrorSearchTooManyMailboxes,
		// Token: 0x04000982 RID: 2434
		ErrorInvalidRetentionTagNone,
		// Token: 0x04000983 RID: 2435
		ErrorDiscoverySearchesDisabled,
		// Token: 0x04000984 RID: 2436
		ErrorCalendarSeekToConditionNotSupported,
		// Token: 0x04000985 RID: 2437
		ErrorCalendarIsGroupMailboxForAccept,
		// Token: 0x04000986 RID: 2438
		ErrorCalendarIsGroupMailboxForDecline,
		// Token: 0x04000987 RID: 2439
		ErrorCalendarIsGroupMailboxForTentative,
		// Token: 0x04000988 RID: 2440
		ErrorCalendarIsGroupMailboxForSuppressReadReceipt,
		// Token: 0x04000989 RID: 2441
		ErrorOrganizationAccessBlocked,
		// Token: 0x0400098A RID: 2442
		ErrorInvalidLicense,
		// Token: 0x0400098B RID: 2443
		ErrorMessagePerFolderCountReceiveQuotaExceeded
	}
}
