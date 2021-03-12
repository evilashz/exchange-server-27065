using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services
{
	// Token: 0x02000F75 RID: 3957
	internal static class CoreResources
	{
		// Token: 0x06006435 RID: 25653 RVA: 0x0013A38C File Offset: 0x0013858C
		static CoreResources()
		{
			CoreResources.stringIDs.Add(580413482U, "ErrorCannotSaveSentItemInArchiveFolder");
			CoreResources.stringIDs.Add(844193848U, "ErrorMissingUserIdInformation");
			CoreResources.stringIDs.Add(2524429953U, "ErrorSearchConfigurationNotFound");
			CoreResources.stringIDs.Add(1087525243U, "ErrorCannotCreateContactInNonContactFolder");
			CoreResources.stringIDs.Add(1049269714U, "IrmFeatureDisabled");
			CoreResources.stringIDs.Add(1795180790U, "EwsProxyResponseTooBig");
			CoreResources.stringIDs.Add(3858003337U, "UpdateFavoritesUnableToDeleteFavoriteEntry");
			CoreResources.stringIDs.Add(754853510U, "ErrorUpdateDelegatesFailed");
			CoreResources.stringIDs.Add(1991349599U, "ErrorNoMailboxSpecifiedForSearchOperation");
			CoreResources.stringIDs.Add(1455468349U, "ErrorCannotApplyHoldOperationOnDG");
			CoreResources.stringIDs.Add(1944820597U, "ErrorInvalidExchangeImpersonationHeaderData");
			CoreResources.stringIDs.Add(1504384645U, "ExOrganizerCannotCallUpdateCalendarItem");
			CoreResources.stringIDs.Add(2498511721U, "IrmViewRightNotGranted");
			CoreResources.stringIDs.Add(93006148U, "UpdateNonDraftItemInDumpsterNotAllowed");
			CoreResources.stringIDs.Add(2252936850U, "ErrorIPGatewayNotFound");
			CoreResources.stringIDs.Add(2517173182U, "ErrorInvalidPropertyForOperation");
			CoreResources.stringIDs.Add(574561672U, "ErrorNameResolutionNoResults");
			CoreResources.stringIDs.Add(4225005690U, "ErrorInvalidItemForOperationCreateItemAttachment");
			CoreResources.stringIDs.Add(3599592070U, "Loading");
			CoreResources.stringIDs.Add(2339310738U, "ErrorItemSave");
			CoreResources.stringIDs.Add(3413891549U, "SubjectColon");
			CoreResources.stringIDs.Add(2181052460U, "ErrorInvalidItemForOperationExpandDL");
			CoreResources.stringIDs.Add(1978429817U, "MessageApplicationHasNoUserApplicationRoleAssigned");
			CoreResources.stringIDs.Add(3167358706U, "ErrorCalendarIsCancelledMessageSent");
			CoreResources.stringIDs.Add(1633083780U, "ErrorInvalidUserInfo");
			CoreResources.stringIDs.Add(2945703152U, "ErrorCalendarViewRangeTooBig");
			CoreResources.stringIDs.Add(495132450U, "ErrorCalendarIsOrganizerForRemove");
			CoreResources.stringIDs.Add(244533303U, "ErrorInvalidRecipientSubfilterComparison");
			CoreResources.stringIDs.Add(2476021338U, "ErrorPassingActingAsForUMConfig");
			CoreResources.stringIDs.Add(3060608191U, "ErrorUserWithoutFederatedProxyAddress");
			CoreResources.stringIDs.Add(3825363766U, "ErrorInvalidSendItemSaveSettings");
			CoreResources.stringIDs.Add(3533302998U, "ErrorWrongServerVersion");
			CoreResources.stringIDs.Add(3735354645U, "ErrorAssociatedTraversalDisallowedWithViewFilter");
			CoreResources.stringIDs.Add(4045771774U, "ErrorMailboxHoldIsNotPermitted");
			CoreResources.stringIDs.Add(4197444273U, "ErrorDuplicateSOAPHeader");
			CoreResources.stringIDs.Add(2744667914U, "ErrorInvalidValueForPropertyUserConfigurationName");
			CoreResources.stringIDs.Add(3510999536U, "ErrorIncorrectSchemaVersion");
			CoreResources.stringIDs.Add(143544280U, "ErrorImpersonationRequiredForPush");
			CoreResources.stringIDs.Add(3135900505U, "ErrorUnifiedMessagingPromptNotFound");
			CoreResources.stringIDs.Add(3227656327U, "ErrorCalendarMeetingRequestIsOutOfDate");
			CoreResources.stringIDs.Add(1234709444U, "MessageExtensionNotAllowedToCreateFAI");
			CoreResources.stringIDs.Add(2966054199U, "ErrorFolderCorrupt");
			CoreResources.stringIDs.Add(2306155022U, "ErrorManagedFolderNotFound");
			CoreResources.stringIDs.Add(1701713067U, "MessageManagementRoleHeaderCannotUseWithOtherHeaders");
			CoreResources.stringIDs.Add(2285125742U, "ErrorQueryFilterTooLong");
			CoreResources.stringIDs.Add(630435929U, "MessageApplicationUnableActAsUser");
			CoreResources.stringIDs.Add(2886480659U, "ErrorInvalidContactEmailIndex");
			CoreResources.stringIDs.Add(1248433804U, "MessageMalformedSoapHeader");
			CoreResources.stringIDs.Add(3629808665U, "ConversationItemQueryFailed");
			CoreResources.stringIDs.Add(4038759526U, "ErrorADOperation");
			CoreResources.stringIDs.Add(2633097826U, "ErrorCalendarIsOrganizerForAccept");
			CoreResources.stringIDs.Add(3049158008U, "ErrorCannotDeleteTaskOccurrence");
			CoreResources.stringIDs.Add(2291046867U, "ErrorTooManyContactsException");
			CoreResources.stringIDs.Add(3577190220U, "ErrorReadEventsFailed");
			CoreResources.stringIDs.Add(1264061593U, "descInvalidEIParameter");
			CoreResources.stringIDs.Add(3584287689U, "ErrorDuplicateLegacyDistinguishedName");
			CoreResources.stringIDs.Add(2886782397U, "MessageActingAsIsNotAValidEmailAddress");
			CoreResources.stringIDs.Add(1703911099U, "MessageInvalidServerVersionForJsonRequest");
			CoreResources.stringIDs.Add(706889665U, "ErrorCalendarCannotMoveOrCopyOccurrence");
			CoreResources.stringIDs.Add(1747094812U, "ErrorPeopleConnectionNotFound");
			CoreResources.stringIDs.Add(3407017993U, "ErrorCalendarMeetingIsOutOfDateResponseNotProcessedMessageSent");
			CoreResources.stringIDs.Add(2122949970U, "ErrorInvalidExcludesRestriction");
			CoreResources.stringIDs.Add(789479259U, "ErrorMoreThanOneAccessModeSpecified");
			CoreResources.stringIDs.Add(4062262029U, "ErrorCreateSubfolderAccessDenied");
			CoreResources.stringIDs.Add(1244610207U, "ErrorInvalidMailboxIdFormat");
			CoreResources.stringIDs.Add(275979752U, "ErrorCalendarIsCancelledForAccept");
			CoreResources.stringIDs.Add(1596102169U, "MessageApplicationRoleShouldPresentWhenUserRolePresent");
			CoreResources.stringIDs.Add(3078968203U, "ErrorInvalidUMSubscriberDataTimeoutValue");
			CoreResources.stringIDs.Add(602009568U, "ErrorSearchTimeoutExpired");
			CoreResources.stringIDs.Add(3471859246U, "descLocalServerConfigurationRetrievalFailed");
			CoreResources.stringIDs.Add(3156759755U, "ErrorInvalidContactEmailAddress");
			CoreResources.stringIDs.Add(1000223261U, "ErrorInvalidValueForPropertyStringArrayDictionaryKey");
			CoreResources.stringIDs.Add(3941855338U, "ErrorChangeKeyRequiredForWriteOperations");
			CoreResources.stringIDs.Add(4767764U, "ErrorMissingEmailAddress");
			CoreResources.stringIDs.Add(932372376U, "ErrorFullSyncRequiredException");
			CoreResources.stringIDs.Add(2058899143U, "ErrorADSessionFilter");
			CoreResources.stringIDs.Add(4170132598U, "ErrorDistinguishedUserNotSupported");
			CoreResources.stringIDs.Add(3681363043U, "ErrorCrossForestCallerNeedsADObject");
			CoreResources.stringIDs.Add(3422864683U, "ErrorSendMeetingInvitationsOrCancellationsRequired");
			CoreResources.stringIDs.Add(349902350U, "RuleErrorDuplicatedOperationOnTheSameRule");
			CoreResources.stringIDs.Add(2151362503U, "ErrorDeletePersonaOnInvalidFolder");
			CoreResources.stringIDs.Add(1407341086U, "ErrorCannotAddAggregatedAccountMailbox");
			CoreResources.stringIDs.Add(1108442436U, "ErrorExceededConnectionCount");
			CoreResources.stringIDs.Add(1153968262U, "ErrorFolderSavePropertyError");
			CoreResources.stringIDs.Add(590346139U, "ErrorCannotUsePersonalContactsAsRecipientsOrAttendees");
			CoreResources.stringIDs.Add(4004906780U, "ErrorInvalidItemForForward");
			CoreResources.stringIDs.Add(2538925974U, "ErrorChangeKeyRequired");
			CoreResources.stringIDs.Add(147036963U, "ErrorNotAcceptable");
			CoreResources.stringIDs.Add(2055932554U, "ErrorMessageTrackingNoSuchDomain");
			CoreResources.stringIDs.Add(1412463668U, "ErrorTraversalNotAllowedWithoutQueryString");
			CoreResources.stringIDs.Add(3276585407U, "ErrorOrganizationAccessBlocked");
			CoreResources.stringIDs.Add(3169826345U, "ErrorInvalidNumberOfMailboxSearch");
			CoreResources.stringIDs.Add(727968988U, "ErrorCreateManagedFolderPartialCompletion");
			CoreResources.stringIDs.Add(1486053208U, "UpdateFavoritesUnableToRenameFavorite");
			CoreResources.stringIDs.Add(170634829U, "ErrorActiveDirectoryTransientError");
			CoreResources.stringIDs.Add(2956059769U, "ErrorInvalidSubscriptionRequestAllFoldersWithFolderIds");
			CoreResources.stringIDs.Add(2990730164U, "ErrorInvalidOperationSendMeetingInvitationCancellationForPublicFolderItem");
			CoreResources.stringIDs.Add(593146268U, "ErrorIrresolvableConflict");
			CoreResources.stringIDs.Add(479697568U, "ErrorInvalidItemForReplyAll");
			CoreResources.stringIDs.Add(4266358168U, "ErrorPhoneNumberNotDialable");
			CoreResources.stringIDs.Add(4187771604U, "ErrorInvalidInternetHeaderChildNodes");
			CoreResources.stringIDs.Add(3459701324U, "ErrorInvalidExpressionTypeForSubFilter");
			CoreResources.stringIDs.Add(3931903304U, "MessageResolveNamesNotSufficientPermissionsToPrivateDLMember");
			CoreResources.stringIDs.Add(924348366U, "ErrorCannotSetNonCalendarPermissionOnCalendarFolder");
			CoreResources.stringIDs.Add(2126704764U, "ErrorParentFolderIdRequired");
			CoreResources.stringIDs.Add(1854021767U, "ErrorEventNotFound");
			CoreResources.stringIDs.Add(3731723330U, "ErrorVoiceMailNotImplemented");
			CoreResources.stringIDs.Add(3448951775U, "ErrorDeleteDistinguishedFolder");
			CoreResources.stringIDs.Add(2354781453U, "ErrorNoPermissionToSearchOrHoldMailbox");
			CoreResources.stringIDs.Add(3213161861U, "ErrorExchangeApplicationNotEnabled");
			CoreResources.stringIDs.Add(1634698783U, "ErrorResolveNamesInvalidFolderType");
			CoreResources.stringIDs.Add(2226715912U, "ErrorExceededFindCountLimit");
			CoreResources.stringIDs.Add(2771555298U, "MessageExtensionAccessActAsMailboxOnly");
			CoreResources.stringIDs.Add(3093510304U, "ErrorPasswordChangeRequired");
			CoreResources.stringIDs.Add(254805997U, "ErrorInvalidManagedFolderProperty");
			CoreResources.stringIDs.Add(1565810069U, "ErrorInvalidIdMalformedEwsLegacyIdFormat");
			CoreResources.stringIDs.Add(1538229710U, "ErrorSchemaViolation");
			CoreResources.stringIDs.Add(478602263U, "MessageInvalidMailboxContactAddressNotFound");
			CoreResources.stringIDs.Add(1293185920U, "ErrorInvalidIndexedPagingParameters");
			CoreResources.stringIDs.Add(361161677U, "ErrorUnsupportedPathForQuery");
			CoreResources.stringIDs.Add(3721795127U, "ErrorInvalidOperationDelegationAssociatedItem");
			CoreResources.stringIDs.Add(4256465912U, "ErrorRemoteUserMailboxMustSpecifyExplicitLocalMailbox");
			CoreResources.stringIDs.Add(1473115829U, "ErrorNoDestinationCASDueToVersionMismatch");
			CoreResources.stringIDs.Add(1805735881U, "ErrorInvalidValueForPropertyBinaryData");
			CoreResources.stringIDs.Add(2410622290U, "ErrorNotDelegate");
			CoreResources.stringIDs.Add(1829860367U, "ErrorCalendarInvalidDayForTimeChangePattern");
			CoreResources.stringIDs.Add(1988987848U, "ErrorInvalidPullSubscriptionId");
			CoreResources.stringIDs.Add(2571138389U, "ErrorCannotCopyPublicFolderRoot");
			CoreResources.stringIDs.Add(1967767132U, "MessageOperationRequiresUserContext");
			CoreResources.stringIDs.Add(2217412679U, "ErrorPromptPublishingOperationFailed");
			CoreResources.stringIDs.Add(2620420056U, "ErrorInvalidFractionalPagingParameters");
			CoreResources.stringIDs.Add(4236561690U, "ErrorPublicFolderMailboxDiscoveryFailed");
			CoreResources.stringIDs.Add(3162641137U, "ErrorUnableToRemoveImContactFromGroup");
			CoreResources.stringIDs.Add(1549704648U, "ErrorSendMeetingCancellationsRequired");
			CoreResources.stringIDs.Add(2011475698U, "MessageRecipientsArrayMustNotBeEmpty");
			CoreResources.stringIDs.Add(757111886U, "ErrorInvalidItemForOperationTentative");
			CoreResources.stringIDs.Add(2519519915U, "ErrorInvalidReferenceItem");
			CoreResources.stringIDs.Add(1314141112U, "IrmReachNotConfigured");
			CoreResources.stringIDs.Add(3907819958U, "ErrorMimeContentInvalidBase64String");
			CoreResources.stringIDs.Add(364289873U, "ErrorSentTaskRequestUpdate");
			CoreResources.stringIDs.Add(1527356366U, "ErrorFoundSyncRequestForNonAggregatedAccount");
			CoreResources.stringIDs.Add(3384523424U, "MessagePropertyIsDeprecatedForThisVersion");
			CoreResources.stringIDs.Add(3954262679U, "ErrorInvalidOperationContactsViewAssociatedItem");
			CoreResources.stringIDs.Add(3655513582U, "ErrorServerBusy");
			CoreResources.stringIDs.Add(3967405104U, "ConversationActionNeedRetentionPolicyTypeForSetRetentionPolicy");
			CoreResources.stringIDs.Add(2671356913U, "ErrorCannotDeletePublicFolderRoot");
			CoreResources.stringIDs.Add(3809605342U, "ErrorImGroupDisplayNameAlreadyExists");
			CoreResources.stringIDs.Add(384737734U, "NoServer");
			CoreResources.stringIDs.Add(948947750U, "ErrorInvalidImDistributionGroupSmtpAddress");
			CoreResources.stringIDs.Add(3640136739U, "ErrorSubscriptionDelegateAccessNotSupported");
			CoreResources.stringIDs.Add(273046712U, "RuleErrorItemIsNotTemplate");
			CoreResources.stringIDs.Add(2549623104U, "ErrorCannotSetPermissionUnknownEntries");
			CoreResources.stringIDs.Add(357277919U, "MessageIdOrTokenTypeNotFound");
			CoreResources.stringIDs.Add(2451443999U, "ErrorLocationServicesDisabled");
			CoreResources.stringIDs.Add(3773912990U, "MessageNotSupportedApplicationRole");
			CoreResources.stringIDs.Add(2636256287U, "ErrorPublicFolderSyncException");
			CoreResources.stringIDs.Add(1448063240U, "ErrorCalendarIsDelegatedForDecline");
			CoreResources.stringIDs.Add(435342351U, "ErrorUnsupportedODataRequest");
			CoreResources.stringIDs.Add(4039615479U, "ErrorDeepTraversalsNotAllowedOnPublicFolders");
			CoreResources.stringIDs.Add(2521448946U, "MessageCouldNotFindWeatherLocationsForSearchString");
			CoreResources.stringIDs.Add(2566235088U, "ErrorInvalidPropertyForSortBy");
			CoreResources.stringIDs.Add(3823874672U, "MessageCalendarUnableToGetAssociatedCalendarItem");
			CoreResources.stringIDs.Add(2841035169U, "ErrorSortByPropertyIsNotFoundOrNotSupported");
			CoreResources.stringIDs.Add(3991730990U, "ErrorNotSupportedSharingMessage");
			CoreResources.stringIDs.Add(1492851991U, "ErrorMissingInformationReferenceItemId");
			CoreResources.stringIDs.Add(1825729465U, "ErrorInvalidSIPUri");
			CoreResources.stringIDs.Add(3371984686U, "ErrorInvalidCompleteDateOutOfRange");
			CoreResources.stringIDs.Add(226219872U, "ErrorUnifiedMessagingDialPlanNotFound");
			CoreResources.stringIDs.Add(2688988465U, "MessageRecipientMustHaveRoutingType");
			CoreResources.stringIDs.Add(3793759002U, "MessageResolveNamesNotSufficientPermissionsToPrivateDL");
			CoreResources.stringIDs.Add(1964352390U, "MessageMissingUserRolesForOrganizationConfigurationRoleTypeApp");
			CoreResources.stringIDs.Add(368663972U, "ErrorInvalidUserSid");
			CoreResources.stringIDs.Add(1954916878U, "ErrorInvalidRecipientSubfilter");
			CoreResources.stringIDs.Add(248293106U, "ErrorSuffixSearchNotAllowed");
			CoreResources.stringIDs.Add(283029019U, "ErrorUnifiedMessagingReportDataNotFound");
			CoreResources.stringIDs.Add(1502984804U, "UpdateFavoritesFolderAlreadyInFavorites");
			CoreResources.stringIDs.Add(2329012714U, "MessageManagementRoleHeaderNotSupportedForOfficeExtension");
			CoreResources.stringIDs.Add(2614511650U, "OneDriveProAttachmentDataProviderName");
			CoreResources.stringIDs.Add(2961161516U, "ErrorCalendarInvalidAttributeValue");
			CoreResources.stringIDs.Add(3854873845U, "MessageInvalidRecurrenceFormat");
			CoreResources.stringIDs.Add(2449079760U, "ErrorInvalidAppApiVersionSupported");
			CoreResources.stringIDs.Add(4227165423U, "ErrorInvalidManagedFolderSize");
			CoreResources.stringIDs.Add(3279473776U, "ErrorTokenSerializationDenied");
			CoreResources.stringIDs.Add(3784063568U, "ErrorInvalidRequest");
			CoreResources.stringIDs.Add(2041209694U, "ErrorSubscriptionUnsubscribed");
			CoreResources.stringIDs.Add(1426183245U, "ErrorInvalidItemForOperationCancelItem");
			CoreResources.stringIDs.Add(684230472U, "IrmCorruptProtectedMessage");
			CoreResources.stringIDs.Add(920557414U, "ErrorCalendarIsGroupMailboxForAccept");
			CoreResources.stringIDs.Add(2933656041U, "ErrorMailboxSearchFailed");
			CoreResources.stringIDs.Add(1188755898U, "ErrorMailboxConfiguration");
			CoreResources.stringIDs.Add(382988367U, "RuleErrorNotSettable");
			CoreResources.stringIDs.Add(4177991609U, "ErrorCopyPublicFolderNotSupported");
			CoreResources.stringIDs.Add(3312780993U, "ErrorInvalidWatermark");
			CoreResources.stringIDs.Add(310545492U, "ErrorActingAsUserNotFound");
			CoreResources.stringIDs.Add(3438146603U, "ErrorDelegateMissingConfiguration");
			CoreResources.stringIDs.Add(1562596869U, "MessageCalendarUnableToUpdateAssociatedCalendarItem");
			CoreResources.stringIDs.Add(2555117076U, "MessageMissingMailboxOwnerEmailAddress");
			CoreResources.stringIDs.Add(3080514177U, "ErrorSentMeetingRequestUpdate");
			CoreResources.stringIDs.Add(2141183275U, "descInvalidTimeZone");
			CoreResources.stringIDs.Add(1733132070U, "ErrorInvalidOperationDisposalTypeAssociatedItem");
			CoreResources.stringIDs.Add(59180037U, "UpdateFavoritesMoveTypeMustBeSet");
			CoreResources.stringIDs.Add(436889836U, "ConversationActionNeedDeleteTypeForSetDeleteAction");
			CoreResources.stringIDs.Add(3616451054U, "ErrorInvalidProxySecurityContext");
			CoreResources.stringIDs.Add(570782166U, "ErrorInvalidValueForProperty");
			CoreResources.stringIDs.Add(1904020973U, "ErrorInvalidRestriction");
			CoreResources.stringIDs.Add(2909492621U, "RuleErrorInvalidAddress");
			CoreResources.stringIDs.Add(599310039U, "RuleErrorSizeLessThanZero");
			CoreResources.stringIDs.Add(3664749912U, "Orange");
			CoreResources.stringIDs.Add(3611326890U, "ErrorRecipientTypeNotSupported");
			CoreResources.stringIDs.Add(3632066599U, "ErrorInvalidIdTooManyAttachmentLevels");
			CoreResources.stringIDs.Add(523664899U, "ErrorExportRemoteArchiveItemsFailed");
			CoreResources.stringIDs.Add(1303377787U, "ErrorCannotSendMessageFromPublicFolder");
			CoreResources.stringIDs.Add(3694049238U, "MessageInsufficientPermissions");
			CoreResources.stringIDs.Add(2611688746U, "MessageCorrelationFailed");
			CoreResources.stringIDs.Add(3819492078U, "ErrorNoMailboxSpecifiedForHoldOperation");
			CoreResources.stringIDs.Add(610144303U, "ErrorTimeZone");
			CoreResources.stringIDs.Add(4260694481U, "ErrorSendAsDenied");
			CoreResources.stringIDs.Add(4292861306U, "MessageSingleOrRecurringCalendarItemExpected");
			CoreResources.stringIDs.Add(2226875331U, "ErrorSearchQueryCannotBeEmpty");
			CoreResources.stringIDs.Add(4136809189U, "ErrorMultipleMailboxesCurrentlyNotSupported");
			CoreResources.stringIDs.Add(4217637937U, "ErrorParentFolderNotFound");
			CoreResources.stringIDs.Add(143488278U, "ErrorDelegateCannotAddOwner");
			CoreResources.stringIDs.Add(3869946114U, "MessageCalendarInsufficientPermissionsToMoveMeetingCancellation");
			CoreResources.stringIDs.Add(73255155U, "ErrorImpersonateUserDenied");
			CoreResources.stringIDs.Add(2875907804U, "ErrorReadReceiptNotPending");
			CoreResources.stringIDs.Add(1676008137U, "ErrorInvalidRetentionTagIdGuid");
			CoreResources.stringIDs.Add(379663703U, "ErrorCannotCreateTaskInNonTaskFolder");
			CoreResources.stringIDs.Add(4074099229U, "MessageNonExistentMailboxNoSmtpAddress");
			CoreResources.stringIDs.Add(2523006528U, "ErrorSchemaValidation");
			CoreResources.stringIDs.Add(3264410200U, "MessageManagementRoleHeaderValueNotApplicable");
			CoreResources.stringIDs.Add(2540872182U, "MessageInvalidRuleVersion");
			CoreResources.stringIDs.Add(1174046717U, "ErrorUnsupportedMimeConversion");
			CoreResources.stringIDs.Add(463452338U, "ErrorCannotMovePublicFolderItemOnDelete");
			CoreResources.stringIDs.Add(966537145U, "ErrorInvalidItemForOperationArchiveItem");
			CoreResources.stringIDs.Add(3021008902U, "ErrorInvalidSearchQuerySyntax");
			CoreResources.stringIDs.Add(4179066588U, "ErrorInvalidValueForCountSystemQueryOption");
			CoreResources.stringIDs.Add(1067402124U, "ErrorFolderSaveFailed");
			CoreResources.stringIDs.Add(2435663882U, "MessageTargetMailboxNotInRoleScope");
			CoreResources.stringIDs.Add(2179607746U, "ErrorInvalidSearchId");
			CoreResources.stringIDs.Add(2674546476U, "ErrorInvalidOperationSyncFolderHierarchyForPublicFolder");
			CoreResources.stringIDs.Add(2624402344U, "ErrorItemCorrupt");
			CoreResources.stringIDs.Add(3120707856U, "ErrorServerTemporaryUnavailable");
			CoreResources.stringIDs.Add(2786380669U, "ErrorCannotArchiveCalendarContactTaskFolderException");
			CoreResources.stringIDs.Add(4123291671U, "ErrorInvalidItemForOperationSendItem");
			CoreResources.stringIDs.Add(68528320U, "ErrorAggregatedAccountAlreadyExists");
			CoreResources.stringIDs.Add(109614196U, "ErrorInvalidServerVersion");
			CoreResources.stringIDs.Add(1487884331U, "ErrorGroupingNonNullWithSuggestionsViewFilter");
			CoreResources.stringIDs.Add(1958477060U, "MessageInvalidMailboxNotPrivateDL");
			CoreResources.stringIDs.Add(1272021886U, "ErrorItemPropertyRequestFailed");
			CoreResources.stringIDs.Add(1706062739U, "ConversationActionNeedDestinationFolderForCopyAction");
			CoreResources.stringIDs.Add(2653243941U, "ErrorLocationServicesRequestFailed");
			CoreResources.stringIDs.Add(220777420U, "UnrecognizedDistinguishedFolderName");
			CoreResources.stringIDs.Add(559784827U, "ErrorInvalidSubfilterTypeNotRecipientType");
			CoreResources.stringIDs.Add(1701761470U, "ErrorInvalidPropertySet");
			CoreResources.stringIDs.Add(3625531057U, "UpdateFavoritesFolderCannotBeNull");
			CoreResources.stringIDs.Add(1326676491U, "ErrorCannotRemoveAggregatedAccountFromList");
			CoreResources.stringIDs.Add(3699987394U, "ErrorProxyTokenExpired");
			CoreResources.stringIDs.Add(3564002022U, "ErrorCannotCreateCalendarItemInNonCalendarFolder");
			CoreResources.stringIDs.Add(3732945645U, "ErrorInvalidOperationGroupByAssociatedItem");
			CoreResources.stringIDs.Add(2890836210U, "MessageCalendarUnableToCreateAssociatedCalendarItem");
			CoreResources.stringIDs.Add(896367800U, "ErrorMultiLegacyMailboxAccess");
			CoreResources.stringIDs.Add(3392207806U, "ErrorUnifiedMailboxAlreadyExists");
			CoreResources.stringIDs.Add(3619206730U, "ErrorInvalidPropertyAppend");
			CoreResources.stringIDs.Add(4261845811U, "ErrorObjectTypeChanged");
			CoreResources.stringIDs.Add(4252616528U, "ErrorSearchableObjectNotFound");
			CoreResources.stringIDs.Add(2498507918U, "ErrorEndTimeMustBeGreaterThanStartTime");
			CoreResources.stringIDs.Add(765833303U, "ErrorInvalidFederatedOrganizationId");
			CoreResources.stringIDs.Add(1583798271U, "MessageExtensionNotAllowedToUpdateFAI");
			CoreResources.stringIDs.Add(1335290147U, "ErrorValueOutOfRange");
			CoreResources.stringIDs.Add(3719196410U, "ErrorNotEnoughMemory");
			CoreResources.stringIDs.Add(3635256568U, "ErrorInvalidExtendedPropertyValue");
			CoreResources.stringIDs.Add(2524108663U, "ErrorMoveCopyFailed");
			CoreResources.stringIDs.Add(1985973150U, "GetClientExtensionTokenFailed");
			CoreResources.stringIDs.Add(3705244005U, "ErrorVirusDetected");
			CoreResources.stringIDs.Add(671866695U, "ErrorInvalidVotingResponse");
			CoreResources.stringIDs.Add(2296308088U, "RuleErrorInboxRulesValidationError");
			CoreResources.stringIDs.Add(1897020671U, "ErrorInvalidIdMonikerTooLong");
			CoreResources.stringIDs.Add(111518940U, "ErrorMultipleSearchRootsDisallowedWithSearchContext");
			CoreResources.stringIDs.Add(4142344047U, "ErrorUserNotUnifiedMessagingEnabled");
			CoreResources.stringIDs.Add(3206878473U, "ErrorCannotMovePublicFolderToPrivateMailbox");
			CoreResources.stringIDs.Add(751424501U, "ConversationActionAlwaysMoveNoPublicFolder");
			CoreResources.stringIDs.Add(1834319386U, "ErrorCallerIsInvalidADAccount");
			CoreResources.stringIDs.Add(3319799507U, "ErrorNoDestinationCASDueToSSLRequirements");
			CoreResources.stringIDs.Add(3995283118U, "ErrorInternalServerTransientError");
			CoreResources.stringIDs.Add(3659985571U, "ErrorInvalidParentFolder");
			CoreResources.stringIDs.Add(2565659540U, "ErrorArchiveFolderPathCreation");
			CoreResources.stringIDs.Add(4066246803U, "MessageCalendarInsufficientPermissionsToMoveItem");
			CoreResources.stringIDs.Add(2791864679U, "ErrorMessagePerFolderCountReceiveQuotaExceeded");
			CoreResources.stringIDs.Add(2643283981U, "ErrorDateTimeNotInUTC");
			CoreResources.stringIDs.Add(2798800298U, "ErrorInvalidAttachmentSubfilter");
			CoreResources.stringIDs.Add(2214456911U, "ErrorUserConfigurationDictionaryNotExist");
			CoreResources.stringIDs.Add(2918743951U, "FromColon");
			CoreResources.stringIDs.Add(2362895530U, "ErrorInvalidSubscriptionRequestNoFolderIds");
			CoreResources.stringIDs.Add(1115854773U, "ErrorCallerIsComputerAccount");
			CoreResources.stringIDs.Add(69571280U, "ErrorDeleteItemsFailed");
			CoreResources.stringIDs.Add(2014273875U, "ErrorNotApplicableOutsideOfDatacenter");
			CoreResources.stringIDs.Add(526527128U, "RuleErrorOutlookRuleBlobExists");
			CoreResources.stringIDs.Add(2843974690U, "descInvalidOofRequestPublicFolder");
			CoreResources.stringIDs.Add(21808504U, "ErrorMailboxIsNotPartOfAggregatedMailboxes");
			CoreResources.stringIDs.Add(2043815785U, "ErrorInvalidRetentionTagNone");
			CoreResources.stringIDs.Add(2448725207U, "MessageInvalidRoleTypeString");
			CoreResources.stringIDs.Add(2343198056U, "MessageInvalidMailboxRecipientNotFoundInActiveDirectory");
			CoreResources.stringIDs.Add(402485116U, "ErrorNoSyncRequestsMatchedSpecifiedEmailAddress");
			CoreResources.stringIDs.Add(2999374145U, "ErrorInvalidDestinationFolderForPostItem");
			CoreResources.stringIDs.Add(377617137U, "ErrorGetRemoteArchiveFolderFailed");
			CoreResources.stringIDs.Add(3410698111U, "RightsManagementMailboxOnlySupport");
			CoreResources.stringIDs.Add(122085112U, "ErrorMissingItemForCreateItemAttachment");
			CoreResources.stringIDs.Add(4160418372U, "ErrorFindRemoteArchiveFolderFailed");
			CoreResources.stringIDs.Add(2989650895U, "ErrorCalendarFolderIsInvalidForCalendarView");
			CoreResources.stringIDs.Add(3359997542U, "ErrorFindConversationNotSupportedForPublicFolders");
			CoreResources.stringIDs.Add(969219158U, "ErrorUserConfigurationBinaryDataNotExist");
			CoreResources.stringIDs.Add(1063299331U, "DefaultHtmlAttachmentHrefText");
			CoreResources.stringIDs.Add(3510846499U, "Green");
			CoreResources.stringIDs.Add(4005418156U, "ErrorItemNotFound");
			CoreResources.stringIDs.Add(2838198776U, "ErrorCannotEmptyFolder");
			CoreResources.stringIDs.Add(777220966U, "Yellow");
			CoreResources.stringIDs.Add(1967895810U, "ErrorInvalidSubscription");
			CoreResources.stringIDs.Add(4281412187U, "ErrorSchemaValidationColon");
			CoreResources.stringIDs.Add(707372475U, "ErrorDelegateNoUser");
			CoreResources.stringIDs.Add(107796140U, "RuleErrorMissingRangeValue");
			CoreResources.stringIDs.Add(2554577046U, "MessageWebMethodUnavailable");
			CoreResources.stringIDs.Add(395078619U, "ErrorUnsupportedQueryFilter");
			CoreResources.stringIDs.Add(968334519U, "ErrorCannotMovePublicFolderOnDelete");
			CoreResources.stringIDs.Add(3314483401U, "ErrorAccessModeSpecified");
			CoreResources.stringIDs.Add(654139516U, "ErrorInvalidPhotoSize");
			CoreResources.stringIDs.Add(2656700117U, "ErrorMultipleMailboxSearchNotSupported");
			CoreResources.stringIDs.Add(728324719U, "MessageManagementRoleHeaderNotSupportedForPartnerIdentity");
			CoreResources.stringIDs.Add(1457631314U, "ConversationActionInvalidFolderType");
			CoreResources.stringIDs.Add(259054457U, "ErrorUnsupportedSubFilter");
			CoreResources.stringIDs.Add(1408093181U, "ErrorInvalidComplianceId");
			CoreResources.stringIDs.Add(3843271914U, "ErrorCalendarCannotUpdateDeletedItem");
			CoreResources.stringIDs.Add(3782996725U, "ErrorInvalidOperationDistinguishedGroupByAssociatedItem");
			CoreResources.stringIDs.Add(3537364541U, "ErrorInvalidDelegatePermission");
			CoreResources.stringIDs.Add(594155080U, "ErrorInternalServerError");
			CoreResources.stringIDs.Add(2356362688U, "ErrorNoPublicFolderServerAvailable");
			CoreResources.stringIDs.Add(3978299680U, "ErrorInvalidPhoneCallId");
			CoreResources.stringIDs.Add(1793222072U, "ErrorInvalidGetSharingFolderRequest");
			CoreResources.stringIDs.Add(2927988853U, "ErrorCannotResolveOrganizationName");
			CoreResources.stringIDs.Add(809187661U, "ErrorUnsupportedCulture");
			CoreResources.stringIDs.Add(865206910U, "ErrorInvalidChangeKey");
			CoreResources.stringIDs.Add(3846347532U, "ErrorMimeContentConversionFailed");
			CoreResources.stringIDs.Add(2683464521U, "ErrorResolveNamesOnlyOneContactsFolderAllowed");
			CoreResources.stringIDs.Add(901489999U, "ErrorInvalidSchemaVersionForMailboxVersion");
			CoreResources.stringIDs.Add(2012012473U, "ErrorInvalidRequestQuotaExceeded");
			CoreResources.stringIDs.Add(768426321U, "MessageTokenRequestUnauthorized");
			CoreResources.stringIDs.Add(3910111167U, "MessageUserRoleNotApplicableForAppOnlyToken");
			CoreResources.stringIDs.Add(828992378U, "ErrorInvalidValueForPropertyKeyValueConversion");
			CoreResources.stringIDs.Add(3769371271U, "ErrorInvalidRetentionTagInheritance");
			CoreResources.stringIDs.Add(710925581U, "Conversation");
			CoreResources.stringIDs.Add(1338511205U, "ErrorCannotCreateUnifiedMailbox");
			CoreResources.stringIDs.Add(1897429247U, "ErrorMailTipsDisabled");
			CoreResources.stringIDs.Add(4222588379U, "ErrorMissingItemIdForCreateItemAttachment");
			CoreResources.stringIDs.Add(1762041369U, "ErrorInvalidMailbox");
			CoreResources.stringIDs.Add(4097108255U, "ErrorDelegateValidationFailed");
			CoreResources.stringIDs.Add(2715027708U, "ErrorUserPromptNeeded");
			CoreResources.stringIDs.Add(1898482716U, "RuleErrorMissingAction");
			CoreResources.stringIDs.Add(706596508U, "ErrorApplyConversationActionFailed");
			CoreResources.stringIDs.Add(60783832U, "ErrorInsufficientResources");
			CoreResources.stringIDs.Add(905739673U, "ErrorActingAsRequired");
			CoreResources.stringIDs.Add(2681298929U, "ErrorCalendarInvalidDayForWeeklyRecurrence");
			CoreResources.stringIDs.Add(549150802U, "ErrorMissingInformationEmailAddress");
			CoreResources.stringIDs.Add(4019544117U, "UpdateFavoritesFavoriteNotFound");
			CoreResources.stringIDs.Add(2484699530U, "ErrorCalendarDurationIsTooLong");
			CoreResources.stringIDs.Add(4252309617U, "ErrorNoRespondingCASInDestinationSite");
			CoreResources.stringIDs.Add(388443881U, "ErrorInvalidRecipients");
			CoreResources.stringIDs.Add(269577600U, "ErrorAppendBodyTypeMismatch");
			CoreResources.stringIDs.Add(514021796U, "ErrorDistributionListMemberNotExist");
			CoreResources.stringIDs.Add(3285224352U, "ErrorRequestTimeout");
			CoreResources.stringIDs.Add(3607262778U, "MessageApplicationHasNoRoleAssginedWhichUserHas");
			CoreResources.stringIDs.Add(3668888236U, "ErrorArchiveMailboxGetConversationFailed");
			CoreResources.stringIDs.Add(2851949310U, "ErrorClientIntentNotFound");
			CoreResources.stringIDs.Add(2489326695U, "ErrorNonExistentMailbox");
			CoreResources.stringIDs.Add(1164605313U, "ErrorVirusMessageDeleted");
			CoreResources.stringIDs.Add(175403818U, "ErrorCannotFindUnifiedMailbox");
			CoreResources.stringIDs.Add(1505256501U, "ErrorUnifiedMailboxSupportedOnlyWithMicrosoftAccount");
			CoreResources.stringIDs.Add(2833024077U, "GroupMailboxCreationFailed");
			CoreResources.stringIDs.Add(1233823477U, "ErrorInvalidSearchQueryLength");
			CoreResources.stringIDs.Add(391940363U, "ErrorCalendarInvalidPropertyState");
			CoreResources.stringIDs.Add(1850561764U, "ErrorAddDelegatesFailed");
			CoreResources.stringIDs.Add(3496891301U, "CcColon");
			CoreResources.stringIDs.Add(1062691260U, "ErrorCrossSiteRequest");
			CoreResources.stringIDs.Add(565625999U, "ErrorPublicFolderUserMustHaveMailbox");
			CoreResources.stringIDs.Add(3399410586U, "ErrorMessageTrackingTransientError");
			CoreResources.stringIDs.Add(1027490726U, "ErrorToFolderNotFound");
			CoreResources.stringIDs.Add(1637412134U, "ErrorDeleteUnifiedMessagingPromptFailed");
			CoreResources.stringIDs.Add(3914315493U, "UpdateFavoritesUnableToMoveFavorite");
			CoreResources.stringIDs.Add(3141449171U, "ErrorPeopleConnectionNoToken");
			CoreResources.stringIDs.Add(3848937923U, "ErrorCannotSpecifySearchFolderAsSourceFolder");
			CoreResources.stringIDs.Add(933080956U, "ErrorEmailAddressMismatch");
			CoreResources.stringIDs.Add(2419720676U, "ErrorUserConfigurationXmlDataNotExist");
			CoreResources.stringIDs.Add(2346704662U, "ErrorUnifiedMessagingRequestFailed");
			CoreResources.stringIDs.Add(1075303082U, "ErrorCreateItemAccessDenied");
			CoreResources.stringIDs.Add(2887245343U, "RuleErrorFolderDoesNotExist");
			CoreResources.stringIDs.Add(3485828594U, "ErrorInvalidImContactId");
			CoreResources.stringIDs.Add(3969305989U, "ErrorNoPropertyTagForCustomProperties");
			CoreResources.stringIDs.Add(2677919833U, "SentTime");
			CoreResources.stringIDs.Add(3279543955U, "MessageNonExistentMailboxGuid");
			CoreResources.stringIDs.Add(216781884U, "ErrorMaxRequestedUnifiedGroupsSetsExceeded");
			CoreResources.stringIDs.Add(3555230765U, "ErrorInvalidAppSchemaVersionSupported");
			CoreResources.stringIDs.Add(3522975510U, "ErrorInvalidLogonType");
			CoreResources.stringIDs.Add(131857255U, "MessageActAsUserRequiredForSuchApplicationRole");
			CoreResources.stringIDs.Add(3773356320U, "ErrorCalendarOutOfRange");
			CoreResources.stringIDs.Add(3975089319U, "ErrorContentIndexingNotEnabled");
			CoreResources.stringIDs.Add(4227151856U, "ErrorContentConversionFailed");
			CoreResources.stringIDs.Add(3426540703U, "ConversationIdNotSupported");
			CoreResources.stringIDs.Add(730941518U, "ConversationSupportedOnlyForMailboxSession");
			CoreResources.stringIDs.Add(3771523283U, "ErrorMoveDistinguishedFolder");
			CoreResources.stringIDs.Add(2092164778U, "ErrorMailboxCannotBeSpecifiedForPublicFolderRoot");
			CoreResources.stringIDs.Add(2805212767U, "IrmPreLicensingFailure");
			CoreResources.stringIDs.Add(734136355U, "MessageMissingUserRolesForLegalHoldRoleTypeApp");
			CoreResources.stringIDs.Add(430009573U, "ErrorMailboxVersionNotSupported");
			CoreResources.stringIDs.Add(560492804U, "ErrorRestrictionTooComplex");
			CoreResources.stringIDs.Add(3546363172U, "RuleErrorRecipientDoesNotExist");
			CoreResources.stringIDs.Add(3667869681U, "ErrorInvalidAggregatedAccountCredentials");
			CoreResources.stringIDs.Add(2653688977U, "descInvalidSecurityContext");
			CoreResources.stringIDs.Add(213621866U, "MessagePublicFoldersNotSupportedForNonIndexable");
			CoreResources.stringIDs.Add(3943930965U, "ErrorInvalidFilterNode");
			CoreResources.stringIDs.Add(1508237301U, "ErrorIrmUserRightNotGranted");
			CoreResources.stringIDs.Add(3956968185U, "descInvalidRequestType");
			CoreResources.stringIDs.Add(184315686U, "DowaNotProvisioned");
			CoreResources.stringIDs.Add(2652436543U, "ErrorRecurrenceEndDateTooBig");
			CoreResources.stringIDs.Add(629782913U, "ErrorInvalidItemForReply");
			CoreResources.stringIDs.Add(1342320011U, "UpdateFavoritesInvalidUpdateFavoriteOperationType");
			CoreResources.stringIDs.Add(2674011741U, "ErrorInvalidManagementRoleHeader");
			CoreResources.stringIDs.Add(200462199U, "ErrorCannotGetExternalEcpUrl");
			CoreResources.stringIDs.Add(1645715101U, "ErrorCannotCreateSearchFolderInPublicFolder");
			CoreResources.stringIDs.Add(3288028209U, "RuleErrorUnsupportedRule");
			CoreResources.stringIDs.Add(2518142400U, "ErrorMissingManagedFolderId");
			CoreResources.stringIDs.Add(1990145025U, "MessageInsufficientPermissionsToSend");
			CoreResources.stringIDs.Add(3098927940U, "ErrorInvalidCompleteDate");
			CoreResources.stringIDs.Add(2447591155U, "ErrorSearchFolderTimeout");
			CoreResources.stringIDs.Add(4089853131U, "ErrorCannotSetAggregatedAccount");
			CoreResources.stringIDs.Add(1962425675U, "ErrorInvalidPushSubscriptionUrl");
			CoreResources.stringIDs.Add(1669051638U, "ErrorCannotAddAggregatedAccount");
			CoreResources.stringIDs.Add(1718538996U, "ErrorCalendarIsGroupMailboxForDecline");
			CoreResources.stringIDs.Add(761574210U, "ErrorNameResolutionNoMailbox");
			CoreResources.stringIDs.Add(2225772284U, "ErrorCannotArchiveItemsInArchiveMailbox");
			CoreResources.stringIDs.Add(556297389U, "MowaNotProvisioned");
			CoreResources.stringIDs.Add(529689091U, "ErrorInvalidOperationSendAndSaveCopyToPublicFolder");
			CoreResources.stringIDs.Add(1998574567U, "ConversationActionNeedDestinationFolderForMoveAction");
			CoreResources.stringIDs.Add(718120058U, "ErrorViewFilterRequiresSearchContext");
			CoreResources.stringIDs.Add(1363870753U, "ErrorDelegateAlreadyExists");
			CoreResources.stringIDs.Add(2479091638U, "ErrorSubmitQueryBasedHoldTaskFailed");
			CoreResources.stringIDs.Add(2869245557U, "ErrorPeopleConnectFailedToReadApplicationConfiguration");
			CoreResources.stringIDs.Add(937093447U, "ErrorUnsupportedMapiPropertyType");
			CoreResources.stringIDs.Add(1829541172U, "ErrorApprovalRequestAlreadyDecided");
			CoreResources.stringIDs.Add(472949644U, "MessageCouldNotFindWeatherLocations");
			CoreResources.stringIDs.Add(3770755973U, "WhenColon");
			CoreResources.stringIDs.Add(70508874U, "ErrorNoGroupingForQueryString");
			CoreResources.stringIDs.Add(2651121857U, "ErrorInvalidIdStoreObjectIdTooLong");
			CoreResources.stringIDs.Add(3654265673U, "ErrorQuotaExceeded");
			CoreResources.stringIDs.Add(3601113588U, "ConversationActionNeedReadStateForSetReadStateAction");
			CoreResources.stringIDs.Add(4226485813U, "ErrorLocationServicesRequestTimedOut");
			CoreResources.stringIDs.Add(3349192959U, "ErrorCalendarInvalidPropertyValue");
			CoreResources.stringIDs.Add(978558141U, "ErrorManagedFolderAlreadyExists");
			CoreResources.stringIDs.Add(1008089967U, "ErrorLocationServicesInvalidSource");
			CoreResources.stringIDs.Add(2560374358U, "OnPremiseSynchorizedDiscoverySearch");
			CoreResources.stringIDs.Add(3859804741U, "ErrorInvalidOperationForAssociatedItems");
			CoreResources.stringIDs.Add(953197733U, "ErrorCorruptData");
			CoreResources.stringIDs.Add(39525862U, "ErrorCalendarInvalidTimeZone");
			CoreResources.stringIDs.Add(3281131813U, "ErrorInvalidOperationMessageDispositionAssociatedItem");
			CoreResources.stringIDs.Add(2662672540U, "ErrorSubscriptionAccessDenied");
			CoreResources.stringIDs.Add(2608213760U, "ErrorCannotReadRequestBody");
			CoreResources.stringIDs.Add(2070630207U, "ErrorNameResolutionMultipleResults");
			CoreResources.stringIDs.Add(866480793U, "ErrorInvalidExtendedProperty");
			CoreResources.stringIDs.Add(3760366944U, "EwsProxyCannotGetCredentials");
			CoreResources.stringIDs.Add(2761096871U, "UpdateFavoritesInvalidMoveFavoriteRequest");
			CoreResources.stringIDs.Add(1359116179U, "ErrorInvalidPermissionSettings");
			CoreResources.stringIDs.Add(1645280882U, "ErrorProxyServiceDiscoveryFailed");
			CoreResources.stringIDs.Add(598450895U, "ErrorInvalidItemForOperationAcceptItem");
			CoreResources.stringIDs.Add(2578390262U, "ErrorInvalidValueForPropertyDuplicateDictionaryKey");
			CoreResources.stringIDs.Add(516980747U, "ErrorExceededSubscriptionCount");
			CoreResources.stringIDs.Add(739553585U, "ErrorPermissionNotAllowedByPolicy");
			CoreResources.stringIDs.Add(48346381U, "MessageInsufficientPermissionsToSubscribe");
			CoreResources.stringIDs.Add(1803820018U, "ErrorInvalidValueForPropertyDate");
			CoreResources.stringIDs.Add(3322365201U, "ErrorUnsupportedRecurrence");
			CoreResources.stringIDs.Add(837503410U, "ErrorUserADObjectNotFound");
			CoreResources.stringIDs.Add(2020376324U, "ErrorCannotAttachSelf");
			CoreResources.stringIDs.Add(2938284467U, "ErrorMissingInformationSharingFolderId");
			CoreResources.stringIDs.Add(1762596806U, "ErrorCannotSetFromOnMeetingResponse");
			CoreResources.stringIDs.Add(3795663900U, "MessageInvalidOperationForPublicFolderItemsAddParticipantByItemId");
			CoreResources.stringIDs.Add(1439158331U, "ErrorInvalidItemForOperationCreateItem");
			CoreResources.stringIDs.Add(3788524313U, "ErrorInvalidPropertyForExists");
			CoreResources.stringIDs.Add(792522617U, "ErrorCannotSaveSentItemInPublicFolder");
			CoreResources.stringIDs.Add(3143473274U, "ErrorRestrictionTooLong");
			CoreResources.stringIDs.Add(789094727U, "ErrorUnsupportedPropertyDefinition");
			CoreResources.stringIDs.Add(3311760175U, "SharePointCreationFailed");
			CoreResources.stringIDs.Add(2935460503U, "ErrorDataSizeLimitExceeded");
			CoreResources.stringIDs.Add(628559436U, "ErrorFolderExists");
			CoreResources.stringIDs.Add(2930851601U, "ErrorUnifiedGroupAlreadyExists");
			CoreResources.stringIDs.Add(1580647852U, "MessageApplicationTokenOnly");
			CoreResources.stringIDs.Add(4047718788U, "ErrorSharingNoExternalEwsAvailable");
			CoreResources.stringIDs.Add(1661960732U, "RuleErrorEmptyValueFound");
			CoreResources.stringIDs.Add(852852329U, "ErrorOccurrenceCrossingBoundary");
			CoreResources.stringIDs.Add(3156121664U, "ErrorArchiveMailboxServiceDiscoveryFailed");
			CoreResources.stringIDs.Add(1064353045U, "ErrorInvalidAttachmentSubfilterTextFilter");
			CoreResources.stringIDs.Add(1966896516U, "ErrorGetSharingMetadataNotSupported");
			CoreResources.stringIDs.Add(1507828071U, "MessageRecipientMustHaveEmailAddress");
			CoreResources.stringIDs.Add(4094604515U, "ErrorInvalidRecipientSubfilterTextFilter");
			CoreResources.stringIDs.Add(3673396595U, "ErrorInvalidPropertyRequest");
			CoreResources.stringIDs.Add(1582215140U, "ErrorCalendarIsNotOrganizer");
			CoreResources.stringIDs.Add(3374101509U, "ErrorInvalidProvisionDeviceID");
			CoreResources.stringIDs.Add(4117821571U, "MessageCouldNotGetWeatherDataForLocation");
			CoreResources.stringIDs.Add(2326085984U, "ErrorTimeProposalMissingStartOrEndTimeError");
			CoreResources.stringIDs.Add(1946206036U, "ErrorInvalidSubfilterTypeNotAttendeeType");
			CoreResources.stringIDs.Add(3890629732U, "PropertyCommandNotSupportSet");
			CoreResources.stringIDs.Add(347738787U, "ErrorImpersonationFailed");
			CoreResources.stringIDs.Add(2884324330U, "ErrorSubscriptionNotFound");
			CoreResources.stringIDs.Add(2225154662U, "MessageCalendarInsufficientPermissionsToMoveMeetingRequest");
			CoreResources.stringIDs.Add(3107705007U, "ErrorInvalidIdMalformed");
			CoreResources.stringIDs.Add(1035957819U, "ErrorCalendarIsGroupMailboxForSuppressReadReceipt");
			CoreResources.stringIDs.Add(2940401781U, "ErrorCannotGetSourceFolderPath");
			CoreResources.stringIDs.Add(4083587704U, "ErrorWildcardAndGroupExpansionNotAllowed");
			CoreResources.stringIDs.Add(4077357270U, "UnsupportedInlineAttachmentContentType");
			CoreResources.stringIDs.Add(1170272727U, "RuleErrorUnexpectedError");
			CoreResources.stringIDs.Add(1601473907U, "MessageCalendarInsufficientPermissionsToDraftsFolder");
			CoreResources.stringIDs.Add(634294555U, "ErrorADUnavailable");
			CoreResources.stringIDs.Add(3260461220U, "ErrorInvalidPhoneNumber");
			CoreResources.stringIDs.Add(547900838U, "ErrorSoftDeletedTraversalsNotAllowedOnPublicFolders");
			CoreResources.stringIDs.Add(2687301200U, "ErrorCalendarIsDelegatedForTentative");
			CoreResources.stringIDs.Add(2952942328U, "ErrorFoldersMustBelongToSameMailbox");
			CoreResources.stringIDs.Add(2697731302U, "ErrorDataSourceOperation");
			CoreResources.stringIDs.Add(3573754788U, "ErrorCalendarMeetingIsOutOfDateResponseNotProcessed");
			CoreResources.stringIDs.Add(1475709851U, "MessageInvalidIdMalformedEwsIdFormat");
			CoreResources.stringIDs.Add(3699315399U, "ErrorPreviousPageNavigationCurrentlyNotSupported");
			CoreResources.stringIDs.Add(2058507107U, "ErrorCannotEmptyPublicFolderToDeletedItems");
			CoreResources.stringIDs.Add(1014449457U, "ErrorInvalidSharingData");
			CoreResources.stringIDs.Add(1746698887U, "MessageCalendarInsufficientPermissionsToMeetingMessageFolder");
			CoreResources.stringIDs.Add(2503843052U, "ErrorInvalidOperationCannotSpecifyItemId");
			CoreResources.stringIDs.Add(3187786876U, "ErrorCalendarIsGroupMailboxForTentative");
			CoreResources.stringIDs.Add(2913173341U, "ErrorMessageSizeExceeded");
			CoreResources.stringIDs.Add(3468080577U, "InvalidDateTimePrecisionValue");
			CoreResources.stringIDs.Add(3943872330U, "ErrorStaleObject");
			CoreResources.stringIDs.Add(3119664543U, "UpdateFavoritesUnableToAddFolderToFavorites");
			CoreResources.stringIDs.Add(1282299710U, "ErrorPasswordExpired");
			CoreResources.stringIDs.Add(3142918589U, "ErrorInvalidOperationCannotPerformOperationOnADRecipients");
			CoreResources.stringIDs.Add(157861094U, "ErrorTooManyObjectsOpened");
			CoreResources.stringIDs.Add(256399585U, "MessageInvalidMailboxInvalidReferencedItem");
			CoreResources.stringIDs.Add(3901728717U, "MessageApplicationHasNoGivenRoleAssigned");
			CoreResources.stringIDs.Add(3113724054U, "MessageRecipientsArrayTooLong");
			CoreResources.stringIDs.Add(3852956793U, "ErrorInvalidIdXml");
			CoreResources.stringIDs.Add(2271901695U, "ErrorCallerWithoutMailboxCannotUseSendOnly");
			CoreResources.stringIDs.Add(2535285679U, "ErrorArchiveMailboxSearchFailed");
			CoreResources.stringIDs.Add(2543409328U, "PostedOn");
			CoreResources.stringIDs.Add(4028591235U, "ErrorInvalidExternalSharingInitiator");
			CoreResources.stringIDs.Add(1627983613U, "ErrorMailboxStoreUnavailable");
			CoreResources.stringIDs.Add(2358398289U, "ErrorInvalidCalendarViewRestrictionOrSort");
			CoreResources.stringIDs.Add(3610830273U, "ErrorSavedItemFolderNotFound");
			CoreResources.stringIDs.Add(3335161738U, "ErrorCalendarOccurrenceIsDeletedFromRecurrence");
			CoreResources.stringIDs.Add(2985674644U, "ErrorMissingRecipients");
			CoreResources.stringIDs.Add(3997746891U, "ErrorTimeProposalInvalidInCreateItemRequest");
			CoreResources.stringIDs.Add(2990436390U, "ErrorCalendarIsDelegatedForRemove");
			CoreResources.stringIDs.Add(4151155219U, "ErrorInvalidLikeRequest");
			CoreResources.stringIDs.Add(271991716U, "MessageRecurrenceStartDateTooSmall");
			CoreResources.stringIDs.Add(4066404319U, "ErrorUnknownTimeZone");
			CoreResources.stringIDs.Add(1656583547U, "ErrorProxyGroupSidLimitExceeded");
			CoreResources.stringIDs.Add(2834376775U, "ErrorCannotRemoveAggregatedAccount");
			CoreResources.stringIDs.Add(1816334244U, "ErrorInvalidShape");
			CoreResources.stringIDs.Add(1812149170U, "ErrorInvalidLicense");
			CoreResources.stringIDs.Add(531497785U, "ErrorAccountDisabled");
			CoreResources.stringIDs.Add(1949840710U, "ErrorHoldIsNotFound");
			CoreResources.stringIDs.Add(1830098328U, "MessageMessageIsNotDraft");
			CoreResources.stringIDs.Add(3778961523U, "ErrorWrongServerVersionDelegate");
			CoreResources.stringIDs.Add(2868846894U, "OnBehalfOf");
			CoreResources.stringIDs.Add(1902653190U, "ErrorInvalidOperationForPublicFolderItems");
			CoreResources.stringIDs.Add(1069471396U, "ErrorCalendarCannotUseIdForRecurringMasterId");
			CoreResources.stringIDs.Add(3647226175U, "ErrorInvalidSubscriptionRequest");
			CoreResources.stringIDs.Add(4226852029U, "ErrorInvalidIdEmpty");
			CoreResources.stringIDs.Add(922305341U, "ErrorInvalidAttachmentId");
			CoreResources.stringIDs.Add(2329210449U, "ErrorBothQueryStringAndRestrictionNonNull");
			CoreResources.stringIDs.Add(1725658743U, "RuleErrorRuleNotFound");
			CoreResources.stringIDs.Add(1277300954U, "ErrorDiscoverySearchesDisabled");
			CoreResources.stringIDs.Add(1147653914U, "ErrorCalendarIsCancelledForTentative");
			CoreResources.stringIDs.Add(1564162812U, "ErrorRecurrenceHasNoOccurrence");
			CoreResources.stringIDs.Add(103255531U, "MessageNonExistentMailboxLegacyDN");
			CoreResources.stringIDs.Add(3137087456U, "ErrorNoDestinationCASDueToKerberosRequirements");
			CoreResources.stringIDs.Add(3395659933U, "ErrorFolderNotFound");
			CoreResources.stringIDs.Add(2923349632U, "ErrorCannotPinGroupIfNotAMember");
			CoreResources.stringIDs.Add(2118011096U, "MessageInsufficientPermissionsToSync");
			CoreResources.stringIDs.Add(2483737250U, "ErrorCalendarIsDelegatedForAccept");
			CoreResources.stringIDs.Add(2958727324U, "ErrorInvalidClientAccessTokenRequest");
			CoreResources.stringIDs.Add(2006869741U, "ErrorCalendarOccurrenceIndexIsOutOfRecurrenceRange");
			CoreResources.stringIDs.Add(124559532U, "MessageMissingUpdateDelegateRequestInformation");
			CoreResources.stringIDs.Add(492857424U, "ErrorCannotOpenFileAttachment");
			CoreResources.stringIDs.Add(234107130U, "ErrorInvalidFolderId");
			CoreResources.stringIDs.Add(2141227684U, "ErrorInvalidPropertyUpdateSentMessage");
			CoreResources.stringIDs.Add(1686445652U, "MessageCalendarInsufficientPermissionsToDefaultCalendarFolder");
			CoreResources.stringIDs.Add(784482022U, "IrmServerMisConfigured");
			CoreResources.stringIDs.Add(357046427U, "RuleErrorRulesOverQuota");
			CoreResources.stringIDs.Add(2890296403U, "ErrorNotAllowedExternalSharingByPolicy");
			CoreResources.stringIDs.Add(3792171687U, "ErrorCannotCreatePostItemInNonMailFolder");
			CoreResources.stringIDs.Add(3080652515U, "ErrorCannotEmptyCalendarOrSearchFolder");
			CoreResources.stringIDs.Add(3504612180U, "ErrorEmptyAggregatedAccountMailboxGuidStoredInSyncRequest");
			CoreResources.stringIDs.Add(3329761676U, "ErrorExpiredSubscription");
			CoreResources.stringIDs.Add(3795993851U, "ErrorODataAccessDisabled");
			CoreResources.stringIDs.Add(3558192788U, "ErrorCannotArchiveItemsInPublicFolders");
			CoreResources.stringIDs.Add(908888675U, "ErrorAssociatedTraversalDisallowedWithQueryString");
			CoreResources.stringIDs.Add(2980490932U, "ErrorCalendarIsOrganizerForDecline");
			CoreResources.stringIDs.Add(1228157268U, "ErrorMissingEmailAddressForManagedFolder");
			CoreResources.stringIDs.Add(1105778474U, "ErrorGetSharingMetadataOnlyForMailbox");
			CoreResources.stringIDs.Add(2292082652U, "MessageActingAsMustHaveRoutingType");
			CoreResources.stringIDs.Add(473053729U, "ErrorInvalidOperationAddItemToMyCalendar");
			CoreResources.stringIDs.Add(1581442160U, "ErrorSyncFolderNotFound");
			CoreResources.stringIDs.Add(471235856U, "ErrorInvalidSharingMessage");
			CoreResources.stringIDs.Add(1735870649U, "descInvalidRequest");
			CoreResources.stringIDs.Add(3640136612U, "ErrorUnsupportedServiceConfigurationType");
			CoreResources.stringIDs.Add(918293667U, "RuleErrorCreateWithRuleId");
			CoreResources.stringIDs.Add(3053756532U, "LoadExtensionCustomPropertiesFailed");
			CoreResources.stringIDs.Add(311942179U, "ErrorUserNotAllowedByPolicy");
			CoreResources.stringIDs.Add(2933471333U, "MessageCouldNotGetWeatherData");
			CoreResources.stringIDs.Add(2335200077U, "MessageMultipleApplicationRolesNotSupported");
			CoreResources.stringIDs.Add(3967923828U, "ErrorPropertyValidationFailure");
			CoreResources.stringIDs.Add(3789879302U, "ErrorInvalidOperationCalendarViewAssociatedItem");
			CoreResources.stringIDs.Add(266941361U, "ErrorInvalidUserPrincipalName");
			CoreResources.stringIDs.Add(124305755U, "ErrorMissedNotificationEvents");
			CoreResources.stringIDs.Add(3635708019U, "ErrorCannotRemoveAggregatedAccountMailbox");
			CoreResources.stringIDs.Add(2102429258U, "MessageCalendarUnableToUpdateMeetingRequest");
			CoreResources.stringIDs.Add(3014743008U, "ErrorInvalidValueForPropertyUserConfigurationPublicFolder");
			CoreResources.stringIDs.Add(3867216855U, "ErrorFolderSave");
			CoreResources.stringIDs.Add(2034497546U, "MessageResolveNamesNotSufficientPermissionsToContactsFolder");
			CoreResources.stringIDs.Add(1790760926U, "descMissingForestConfiguration");
			CoreResources.stringIDs.Add(1103930166U, "ErrorUnsupportedPathForSortGroup");
			CoreResources.stringIDs.Add(3836413508U, "ErrorContainsFilterWrongType");
			CoreResources.stringIDs.Add(1270269734U, "ErrorMailboxScopeNotAllowedWithoutQueryString");
			CoreResources.stringIDs.Add(2084976894U, "ErrorMessageTrackingPermanentError");
			CoreResources.stringIDs.Add(3912965805U, "ErrorCannotDeleteObject");
			CoreResources.stringIDs.Add(1670145257U, "MessageCallerHasNoAdminRoleGranted");
			CoreResources.stringIDs.Add(2692292357U, "ErrorIrmNotSupported");
			CoreResources.stringIDs.Add(707914022U, "ReferenceLinkSharedFrom");
			CoreResources.stringIDs.Add(295620541U, "SentColon");
			CoreResources.stringIDs.Add(1479620947U, "ErrorActingAsUserNotUnique");
			CoreResources.stringIDs.Add(3032287327U, "ErrorSearchQueryHasTooManyKeywords");
			CoreResources.stringIDs.Add(2370747299U, "ErrorFolderPropertyRequestFailed");
			CoreResources.stringIDs.Add(1795652632U, "ErrorMimeContentInvalid");
			CoreResources.stringIDs.Add(3469371317U, "ErrorSharingSynchronizationFailed");
			CoreResources.stringIDs.Add(2109751382U, "ErrorPublicFolderSearchNotSupportedOnMultipleFolders");
			CoreResources.stringIDs.Add(3753602229U, "ErrorNoFolderClassOverride");
			CoreResources.stringIDs.Add(836749070U, "ErrorUnsupportedTypeForConversion");
			CoreResources.stringIDs.Add(1535520491U, "ErrorInvalidItemForOperationDeclineItem");
			CoreResources.stringIDs.Add(3563948173U, "MessageCalendarInsufficientPermissionsToSaveCalendarItem");
			CoreResources.stringIDs.Add(237462827U, "ErrorRightsManagementException");
			CoreResources.stringIDs.Add(2440725179U, "ErrorOperationNotAllowedWithPublicFolderRoot");
			CoreResources.stringIDs.Add(189525348U, "ErrorInvalidIdReturnedByResolveNames");
			CoreResources.stringIDs.Add(2194994953U, "descNoRequestType");
			CoreResources.stringIDs.Add(3371251772U, "ErrorCalendarIsOrganizerForTentative");
			CoreResources.stringIDs.Add(2253496121U, "ErrorInvalidVotingRequest");
			CoreResources.stringIDs.Add(832351692U, "ErrorInvalidProvisionDeviceType");
			CoreResources.stringIDs.Add(2176189925U, "RuleErrorUnsupportedAddress");
			CoreResources.stringIDs.Add(1093593955U, "ErrorInvalidCallStatus");
			CoreResources.stringIDs.Add(2718542415U, "ErrorInvalidSid");
			CoreResources.stringIDs.Add(2580909644U, "ErrorManagedFoldersRootFailure");
			CoreResources.stringIDs.Add(1569114342U, "ErrorProxiedSubscriptionCallFailure");
			CoreResources.stringIDs.Add(1214042036U, "ErrorOccurrenceTimeSpanTooBig");
			CoreResources.stringIDs.Add(1110621977U, "MessageCalendarInsufficientPermissionsToMoveCalendarItem");
			CoreResources.stringIDs.Add(2943900075U, "ErrorNewEventStreamConnectionOpened");
			CoreResources.stringIDs.Add(2054881972U, "ErrorArchiveMailboxNotEnabled");
			CoreResources.stringIDs.Add(4180336284U, "ErrorCalendarCannotUseIdForOccurrenceId");
			CoreResources.stringIDs.Add(3579904699U, "ErrorAccessDenied");
			CoreResources.stringIDs.Add(1721078306U, "ErrorAttachmentSizeLimitExceeded");
			CoreResources.stringIDs.Add(1912743644U, "ErrorPropertyUpdate");
			CoreResources.stringIDs.Add(842243550U, "RuleErrorInvalidValue");
			CoreResources.stringIDs.Add(2756368512U, "ErrorInvalidManagedFolderQuota");
			CoreResources.stringIDs.Add(304669716U, "ErrorCreateDistinguishedFolder");
			CoreResources.stringIDs.Add(3684919469U, "ShowDetails");
			CoreResources.stringIDs.Add(3465339554U, "ToColon");
			CoreResources.stringIDs.Add(2832845860U, "ErrorCrossMailboxMoveCopy");
			CoreResources.stringIDs.Add(4273162695U, "FlagForFollowUp");
			CoreResources.stringIDs.Add(4264440001U, "ErrorGetStreamingEventsProxy");
			CoreResources.stringIDs.Add(2183377470U, "ErrorCannotSetCalendarPermissionOnNonCalendarFolder");
			CoreResources.stringIDs.Add(1686056205U, "SaveExtensionCustomPropertiesFailed");
			CoreResources.stringIDs.Add(3500594897U, "ErrorConnectionFailed");
			CoreResources.stringIDs.Add(2976928908U, "ErrorCannotUseLocalAccount");
			CoreResources.stringIDs.Add(810356415U, "descInvalidOofParameter");
			CoreResources.stringIDs.Add(2538042329U, "ErrorTimeRangeIsTooLarge");
			CoreResources.stringIDs.Add(2684918840U, "ErrorAffectedTaskOccurrencesRequired");
			CoreResources.stringIDs.Add(513058151U, "ErrorCannotGetAggregatedAccount");
			CoreResources.stringIDs.Add(917420962U, "AADIdentityCreationFailed");
			CoreResources.stringIDs.Add(217482359U, "ErrorDuplicateInputFolderNames");
			CoreResources.stringIDs.Add(4088802584U, "MessageNonExistentMailboxSmtpAddress");
			CoreResources.stringIDs.Add(1439726170U, "ErrorIncorrectUpdatePropertyCount");
			CoreResources.stringIDs.Add(2485795088U, "ErrorInvalidSerializedAccessToken");
			CoreResources.stringIDs.Add(4103342537U, "ErrorInvalidRoutingType");
			CoreResources.stringIDs.Add(3383701276U, "ErrorSendMeetingInvitationsRequired");
			CoreResources.stringIDs.Add(1500443603U, "ErrorInvalidIdNotAnItemAttachmentId");
			CoreResources.stringIDs.Add(1702622873U, "RightsManagementInternalLicensingDisabled");
			CoreResources.stringIDs.Add(247950989U, "MessageCannotUseItemAsRecipient");
			CoreResources.stringIDs.Add(815859081U, "ErrorItemSaveUserConfigurationExists");
			CoreResources.stringIDs.Add(687843280U, "MessageInvalidMailboxMailboxType");
			CoreResources.stringIDs.Add(2997278338U, "ErrorCalendarIsCancelledForDecline");
			CoreResources.stringIDs.Add(3510335548U, "ErrorClientIntentInvalidStateDefinition");
			CoreResources.stringIDs.Add(4105318492U, "ErrorInvalidRetentionTagInvisible");
			CoreResources.stringIDs.Add(1583125739U, "ErrorItemSavePropertyError");
			CoreResources.stringIDs.Add(3308334241U, "GetScopedTokenFailedWithInvalidScope");
			CoreResources.stringIDs.Add(1518836305U, "ErrorInvalidItemForOperationRemoveItem");
			CoreResources.stringIDs.Add(1858753018U, "RuleErrorMessageClassificationNotFound");
			CoreResources.stringIDs.Add(30276810U, "MessageUnableToLoadRBACSettings");
			CoreResources.stringIDs.Add(707594371U, "ErrorQueryLanguageNotValid");
			CoreResources.stringIDs.Add(4158023012U, "Purple");
			CoreResources.stringIDs.Add(133083912U, "InvalidMaxItemsToReturn");
			CoreResources.stringIDs.Add(4109493280U, "PostedTo");
			CoreResources.stringIDs.Add(607223707U, "ExchangeServiceResponseErrorNoResponse");
			CoreResources.stringIDs.Add(1569475421U, "ErrorPublicFolderOperationFailed");
			CoreResources.stringIDs.Add(444799972U, "ErrorBatchProcessingStopped");
			CoreResources.stringIDs.Add(1722439826U, "ErrorUnifiedMessagingServerNotFound");
			CoreResources.stringIDs.Add(4077186341U, "InstantSearchNullFolderId");
			CoreResources.stringIDs.Add(4210036349U, "ErrorWeatherServiceDisabled");
			CoreResources.stringIDs.Add(2710201884U, "descNotEnoughPrivileges");
			CoreResources.stringIDs.Add(3284680126U, "CalendarInvalidFirstDayOfWeek");
			CoreResources.stringIDs.Add(3021629811U, "Red");
			CoreResources.stringIDs.Add(3133201118U, "ErrorInvalidExternalSharingSubscriber");
			CoreResources.stringIDs.Add(2770848984U, "ErrorCannotUseFolderIdForItemId");
			CoreResources.stringIDs.Add(3336001063U, "ErrorExchange14Required");
			CoreResources.stringIDs.Add(3032417457U, "ErrorProxyCallFailed");
			CoreResources.stringIDs.Add(1788731128U, "ErrorOrganizationNotFederated");
			CoreResources.stringIDs.Add(2395476974U, "Blue");
			CoreResources.stringIDs.Add(1411102909U, "ErrorCannotDeleteSubfoldersOfMsgRootFolder");
			CoreResources.stringIDs.Add(1737721488U, "ErrorUpdatePropertyMismatch");
			CoreResources.stringIDs.Add(1160056179U, "ErrorIllegalCrossServerConnection");
			CoreResources.stringIDs.Add(822125946U, "ErrorImListMigration");
			CoreResources.stringIDs.Add(610335429U, "ErrorResponseSchemaValidation");
			CoreResources.stringIDs.Add(3829975538U, "ServerNotInSite");
			CoreResources.stringIDs.Add(3220333293U, "ErrorCannotAddAggregatedAccountToList");
			CoreResources.stringIDs.Add(1666265192U, "WhereColon");
			CoreResources.stringIDs.Add(708456719U, "ErrorInvalidApprovalRequest");
			CoreResources.stringIDs.Add(3010537222U, "ErrorIncorrectEncodedIdType");
			CoreResources.stringIDs.Add(4106572054U, "ErrorGetRemoteArchiveItemFailed");
			CoreResources.stringIDs.Add(827411151U, "ErrorInvalidImGroupId");
			CoreResources.stringIDs.Add(1835609958U, "ErrorInvalidRequestUnknownMethodDebug");
			CoreResources.stringIDs.Add(2675632227U, "ErrorBothViewFilterAndRestrictionNonNull");
			CoreResources.stringIDs.Add(2423603834U, "ErrorCannotUseItemIdForFolderId");
			CoreResources.stringIDs.Add(1262244671U, "ErrorCannotDisableMandatoryExtension");
			CoreResources.stringIDs.Add(1655535493U, "ErrorInvalidSyncStateData");
			CoreResources.stringIDs.Add(178029729U, "ErrorSubmissionQuotaExceeded");
			CoreResources.stringIDs.Add(2130715693U, "ErrorMessageDispositionRequired");
			CoreResources.stringIDs.Add(1395824819U, "ErrorSearchScopeCannotHavePublicFolders");
			CoreResources.stringIDs.Add(3763931121U, "ErrorRemoveDelegatesFailed");
			CoreResources.stringIDs.Add(2467205866U, "ErrorInvalidPagingMaxRows");
			CoreResources.stringIDs.Add(3016713339U, "RuleErrorMissingParameter");
			CoreResources.stringIDs.Add(1085788054U, "ErrorLocationServicesInvalidQuery");
			CoreResources.stringIDs.Add(1319006043U, "MessageOccurrenceNotFound");
			CoreResources.stringIDs.Add(1658627017U, "ErrorSearchFolderNotInitialized");
			CoreResources.stringIDs.Add(1064030279U, "FolderScopeNotSpecified");
			CoreResources.stringIDs.Add(3880436217U, "ErrorInvalidSubfilterType");
			CoreResources.stringIDs.Add(4289255106U, "ErrorDuplicateUserIdsSpecified");
			CoreResources.stringIDs.Add(1422139444U, "ErrorDelegateMustBeCalendarEditorToGetMeetingMessages");
			CoreResources.stringIDs.Add(3041888687U, "ErrorMismatchFolderId");
			CoreResources.stringIDs.Add(444235555U, "ErrorInvalidPropertyDelete");
			CoreResources.stringIDs.Add(3538999938U, "MessageActingAsMustHaveEmailAddress");
			CoreResources.stringIDs.Add(4064247940U, "ErrorCalendarIsCancelledForRemove");
			CoreResources.stringIDs.Add(884514945U, "ErrorCannotResolveODataUrl");
			CoreResources.stringIDs.Add(4006585486U, "ErrorCalendarEndDateIsEarlierThanStartDate");
			CoreResources.stringIDs.Add(3035123300U, "ErrorInvalidPercentCompleteValue");
			CoreResources.stringIDs.Add(4164112684U, "ErrorNoApplicableProxyCASServersAvailable");
			CoreResources.stringIDs.Add(106943791U, "IrmProtectedVoicemailFeatureDisabled");
			CoreResources.stringIDs.Add(1397740097U, "IrmExternalLicensingDisabled");
			CoreResources.stringIDs.Add(2132997082U, "ErrorExchangeConfigurationException");
			CoreResources.stringIDs.Add(2069536979U, "ErrorMailboxMoveInProgress");
			CoreResources.stringIDs.Add(2643780243U, "ErrorInvalidValueForPropertyXmlData");
			CoreResources.stringIDs.Add(668221183U, "RuleErrorDuplicatedPriority");
			CoreResources.stringIDs.Add(2622305962U, "ItemNotExistInPurgesFolder");
			CoreResources.stringIDs.Add(3313362701U, "MessageMissingUserRolesForMailboxSearchRoleTypeApp");
			CoreResources.stringIDs.Add(4279571010U, "ErrorInvalidNameForNameResolution");
			CoreResources.stringIDs.Add(3776856310U, "ErrorInvalidRecipientSubfilterOrder");
			CoreResources.stringIDs.Add(3146925354U, "ErrorMailboxContainerGuidMismatch");
			CoreResources.stringIDs.Add(1935400134U, "ErrorInvalidId");
			CoreResources.stringIDs.Add(195228379U, "ErrorNonPrimarySmtpAddress");
			CoreResources.stringIDs.Add(2280668014U, "ErrorSharedFolderSearchNotSupportedOnMultipleFolders");
			CoreResources.stringIDs.Add(1532804559U, "ErrorCalendarInvalidRecurrence");
			CoreResources.stringIDs.Add(4104292452U, "ErrorInvalidOperationSaveReplyForwardToPublicFolder");
			CoreResources.stringIDs.Add(4072847078U, "ErrorInvalidOrderbyThenby");
			CoreResources.stringIDs.Add(531605055U, "ErrorInvalidRetentionTagTypeMismatch");
			CoreResources.stringIDs.Add(608291240U, "ErrorRequiredPropertyMissing");
			CoreResources.stringIDs.Add(54420019U, "ErrorActiveDirectoryPermanentError");
			CoreResources.stringIDs.Add(360598592U, "IrmRmsError");
			CoreResources.stringIDs.Add(3654096821U, "ErrorNoPropertyUpdatesOrAttachmentsSpecified");
			CoreResources.stringIDs.Add(2137439660U, "ConversationActionNeedFlagForFlagAction");
			CoreResources.stringIDs.Add(178421269U, "ErrorAttachmentNestLevelLimitExceeded");
			CoreResources.stringIDs.Add(938097637U, "ErrorInvalidSmtpAddress");
		}

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x06006436 RID: 25654 RVA: 0x0013E093 File Offset: 0x0013C293
		public static LocalizedString ErrorCannotSaveSentItemInArchiveFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotSaveSentItemInArchiveFolder", "ExE485E1", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x06006437 RID: 25655 RVA: 0x0013E0B1 File Offset: 0x0013C2B1
		public static LocalizedString ErrorMissingUserIdInformation
		{
			get
			{
				return new LocalizedString("ErrorMissingUserIdInformation", "ExF85CAB", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x06006438 RID: 25656 RVA: 0x0013E0CF File Offset: 0x0013C2CF
		public static LocalizedString ErrorSearchConfigurationNotFound
		{
			get
			{
				return new LocalizedString("ErrorSearchConfigurationNotFound", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x06006439 RID: 25657 RVA: 0x0013E0ED File Offset: 0x0013C2ED
		public static LocalizedString ErrorCannotCreateContactInNonContactFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotCreateContactInNonContactFolder", "Ex8AFB0E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x0600643A RID: 25658 RVA: 0x0013E10B File Offset: 0x0013C30B
		public static LocalizedString IrmFeatureDisabled
		{
			get
			{
				return new LocalizedString("IrmFeatureDisabled", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x0600643B RID: 25659 RVA: 0x0013E129 File Offset: 0x0013C329
		public static LocalizedString EwsProxyResponseTooBig
		{
			get
			{
				return new LocalizedString("EwsProxyResponseTooBig", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x0600643C RID: 25660 RVA: 0x0013E147 File Offset: 0x0013C347
		public static LocalizedString UpdateFavoritesUnableToDeleteFavoriteEntry
		{
			get
			{
				return new LocalizedString("UpdateFavoritesUnableToDeleteFavoriteEntry", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x0600643D RID: 25661 RVA: 0x0013E165 File Offset: 0x0013C365
		public static LocalizedString ErrorUpdateDelegatesFailed
		{
			get
			{
				return new LocalizedString("ErrorUpdateDelegatesFailed", "Ex76491C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x0600643E RID: 25662 RVA: 0x0013E183 File Offset: 0x0013C383
		public static LocalizedString ErrorNoMailboxSpecifiedForSearchOperation
		{
			get
			{
				return new LocalizedString("ErrorNoMailboxSpecifiedForSearchOperation", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x0600643F RID: 25663 RVA: 0x0013E1A1 File Offset: 0x0013C3A1
		public static LocalizedString ErrorCannotApplyHoldOperationOnDG
		{
			get
			{
				return new LocalizedString("ErrorCannotApplyHoldOperationOnDG", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x06006440 RID: 25664 RVA: 0x0013E1BF File Offset: 0x0013C3BF
		public static LocalizedString ErrorInvalidExchangeImpersonationHeaderData
		{
			get
			{
				return new LocalizedString("ErrorInvalidExchangeImpersonationHeaderData", "ExDEBB9C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x06006441 RID: 25665 RVA: 0x0013E1DD File Offset: 0x0013C3DD
		public static LocalizedString ExOrganizerCannotCallUpdateCalendarItem
		{
			get
			{
				return new LocalizedString("ExOrganizerCannotCallUpdateCalendarItem", "ExE5CA94", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x06006442 RID: 25666 RVA: 0x0013E1FB File Offset: 0x0013C3FB
		public static LocalizedString IrmViewRightNotGranted
		{
			get
			{
				return new LocalizedString("IrmViewRightNotGranted", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x06006443 RID: 25667 RVA: 0x0013E219 File Offset: 0x0013C419
		public static LocalizedString UpdateNonDraftItemInDumpsterNotAllowed
		{
			get
			{
				return new LocalizedString("UpdateNonDraftItemInDumpsterNotAllowed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06006444 RID: 25668 RVA: 0x0013E237 File Offset: 0x0013C437
		public static LocalizedString ErrorIPGatewayNotFound
		{
			get
			{
				return new LocalizedString("ErrorIPGatewayNotFound", "Ex829B0D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06006445 RID: 25669 RVA: 0x0013E255 File Offset: 0x0013C455
		public static LocalizedString ErrorInvalidPropertyForOperation
		{
			get
			{
				return new LocalizedString("ErrorInvalidPropertyForOperation", "Ex2C4929", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06006446 RID: 25670 RVA: 0x0013E273 File Offset: 0x0013C473
		public static LocalizedString ErrorNameResolutionNoResults
		{
			get
			{
				return new LocalizedString("ErrorNameResolutionNoResults", "Ex79F5CE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006447 RID: 25671 RVA: 0x0013E294 File Offset: 0x0013C494
		public static LocalizedString ErrorNoConnectionSettingsAvailableForAggregatedAccount(string email)
		{
			return new LocalizedString("ErrorNoConnectionSettingsAvailableForAggregatedAccount", "", false, false, CoreResources.ResourceManager, new object[]
			{
				email
			});
		}

		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x06006448 RID: 25672 RVA: 0x0013E2C3 File Offset: 0x0013C4C3
		public static LocalizedString ErrorInvalidItemForOperationCreateItemAttachment
		{
			get
			{
				return new LocalizedString("ErrorInvalidItemForOperationCreateItemAttachment", "ExDB14A3", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x06006449 RID: 25673 RVA: 0x0013E2E1 File Offset: 0x0013C4E1
		public static LocalizedString Loading
		{
			get
			{
				return new LocalizedString("Loading", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x0600644A RID: 25674 RVA: 0x0013E2FF File Offset: 0x0013C4FF
		public static LocalizedString ErrorItemSave
		{
			get
			{
				return new LocalizedString("ErrorItemSave", "ExDDC5BD", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x0600644B RID: 25675 RVA: 0x0013E31D File Offset: 0x0013C51D
		public static LocalizedString SubjectColon
		{
			get
			{
				return new LocalizedString("SubjectColon", "Ex506FE5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x0600644C RID: 25676 RVA: 0x0013E33B File Offset: 0x0013C53B
		public static LocalizedString ErrorInvalidItemForOperationExpandDL
		{
			get
			{
				return new LocalizedString("ErrorInvalidItemForOperationExpandDL", "Ex0DD286", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x0600644D RID: 25677 RVA: 0x0013E359 File Offset: 0x0013C559
		public static LocalizedString MessageApplicationHasNoUserApplicationRoleAssigned
		{
			get
			{
				return new LocalizedString("MessageApplicationHasNoUserApplicationRoleAssigned", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x0600644E RID: 25678 RVA: 0x0013E377 File Offset: 0x0013C577
		public static LocalizedString ErrorCalendarIsCancelledMessageSent
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsCancelledMessageSent", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x0600644F RID: 25679 RVA: 0x0013E395 File Offset: 0x0013C595
		public static LocalizedString ErrorInvalidUserInfo
		{
			get
			{
				return new LocalizedString("ErrorInvalidUserInfo", "Ex5020C4", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x06006450 RID: 25680 RVA: 0x0013E3B3 File Offset: 0x0013C5B3
		public static LocalizedString ErrorCalendarViewRangeTooBig
		{
			get
			{
				return new LocalizedString("ErrorCalendarViewRangeTooBig", "Ex9AD1A2", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x06006451 RID: 25681 RVA: 0x0013E3D1 File Offset: 0x0013C5D1
		public static LocalizedString ErrorCalendarIsOrganizerForRemove
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsOrganizerForRemove", "ExAC0DE1", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x06006452 RID: 25682 RVA: 0x0013E3EF File Offset: 0x0013C5EF
		public static LocalizedString ErrorInvalidRecipientSubfilterComparison
		{
			get
			{
				return new LocalizedString("ErrorInvalidRecipientSubfilterComparison", "ExF399B3", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x06006453 RID: 25683 RVA: 0x0013E40D File Offset: 0x0013C60D
		public static LocalizedString ErrorPassingActingAsForUMConfig
		{
			get
			{
				return new LocalizedString("ErrorPassingActingAsForUMConfig", "Ex8BE355", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x06006454 RID: 25684 RVA: 0x0013E42B File Offset: 0x0013C62B
		public static LocalizedString ErrorUserWithoutFederatedProxyAddress
		{
			get
			{
				return new LocalizedString("ErrorUserWithoutFederatedProxyAddress", "ExC6130A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x06006455 RID: 25685 RVA: 0x0013E449 File Offset: 0x0013C649
		public static LocalizedString ErrorInvalidSendItemSaveSettings
		{
			get
			{
				return new LocalizedString("ErrorInvalidSendItemSaveSettings", "Ex196391", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x06006456 RID: 25686 RVA: 0x0013E467 File Offset: 0x0013C667
		public static LocalizedString ErrorWrongServerVersion
		{
			get
			{
				return new LocalizedString("ErrorWrongServerVersion", "ExF8DB35", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x06006457 RID: 25687 RVA: 0x0013E485 File Offset: 0x0013C685
		public static LocalizedString ErrorAssociatedTraversalDisallowedWithViewFilter
		{
			get
			{
				return new LocalizedString("ErrorAssociatedTraversalDisallowedWithViewFilter", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x06006458 RID: 25688 RVA: 0x0013E4A3 File Offset: 0x0013C6A3
		public static LocalizedString ErrorMailboxHoldIsNotPermitted
		{
			get
			{
				return new LocalizedString("ErrorMailboxHoldIsNotPermitted", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x06006459 RID: 25689 RVA: 0x0013E4C1 File Offset: 0x0013C6C1
		public static LocalizedString ErrorDuplicateSOAPHeader
		{
			get
			{
				return new LocalizedString("ErrorDuplicateSOAPHeader", "Ex69584C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x0600645A RID: 25690 RVA: 0x0013E4DF File Offset: 0x0013C6DF
		public static LocalizedString ErrorInvalidValueForPropertyUserConfigurationName
		{
			get
			{
				return new LocalizedString("ErrorInvalidValueForPropertyUserConfigurationName", "ExD121B8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x0600645B RID: 25691 RVA: 0x0013E4FD File Offset: 0x0013C6FD
		public static LocalizedString ErrorIncorrectSchemaVersion
		{
			get
			{
				return new LocalizedString("ErrorIncorrectSchemaVersion", "Ex3734AB", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x0600645C RID: 25692 RVA: 0x0013E51B File Offset: 0x0013C71B
		public static LocalizedString ErrorImpersonationRequiredForPush
		{
			get
			{
				return new LocalizedString("ErrorImpersonationRequiredForPush", "Ex2184D5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x0600645D RID: 25693 RVA: 0x0013E539 File Offset: 0x0013C739
		public static LocalizedString ErrorUnifiedMessagingPromptNotFound
		{
			get
			{
				return new LocalizedString("ErrorUnifiedMessagingPromptNotFound", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x0600645E RID: 25694 RVA: 0x0013E557 File Offset: 0x0013C757
		public static LocalizedString ErrorCalendarMeetingRequestIsOutOfDate
		{
			get
			{
				return new LocalizedString("ErrorCalendarMeetingRequestIsOutOfDate", "Ex602910", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x0600645F RID: 25695 RVA: 0x0013E575 File Offset: 0x0013C775
		public static LocalizedString MessageExtensionNotAllowedToCreateFAI
		{
			get
			{
				return new LocalizedString("MessageExtensionNotAllowedToCreateFAI", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x06006460 RID: 25696 RVA: 0x0013E593 File Offset: 0x0013C793
		public static LocalizedString ErrorFolderCorrupt
		{
			get
			{
				return new LocalizedString("ErrorFolderCorrupt", "ExBB20FE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x06006461 RID: 25697 RVA: 0x0013E5B1 File Offset: 0x0013C7B1
		public static LocalizedString ErrorManagedFolderNotFound
		{
			get
			{
				return new LocalizedString("ErrorManagedFolderNotFound", "Ex481826", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x06006462 RID: 25698 RVA: 0x0013E5CF File Offset: 0x0013C7CF
		public static LocalizedString MessageManagementRoleHeaderCannotUseWithOtherHeaders
		{
			get
			{
				return new LocalizedString("MessageManagementRoleHeaderCannotUseWithOtherHeaders", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x06006463 RID: 25699 RVA: 0x0013E5ED File Offset: 0x0013C7ED
		public static LocalizedString ErrorQueryFilterTooLong
		{
			get
			{
				return new LocalizedString("ErrorQueryFilterTooLong", "Ex9140DE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006464 RID: 25700 RVA: 0x0013E60C File Offset: 0x0013C80C
		public static LocalizedString ErrorTimeProposal(string innerError)
		{
			return new LocalizedString("ErrorTimeProposal", "", false, false, CoreResources.ResourceManager, new object[]
			{
				innerError
			});
		}

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x06006465 RID: 25701 RVA: 0x0013E63B File Offset: 0x0013C83B
		public static LocalizedString MessageApplicationUnableActAsUser
		{
			get
			{
				return new LocalizedString("MessageApplicationUnableActAsUser", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x06006466 RID: 25702 RVA: 0x0013E659 File Offset: 0x0013C859
		public static LocalizedString ErrorInvalidContactEmailIndex
		{
			get
			{
				return new LocalizedString("ErrorInvalidContactEmailIndex", "ExCBD1E3", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x06006467 RID: 25703 RVA: 0x0013E677 File Offset: 0x0013C877
		public static LocalizedString MessageMalformedSoapHeader
		{
			get
			{
				return new LocalizedString("MessageMalformedSoapHeader", "Ex68BB33", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x06006468 RID: 25704 RVA: 0x0013E695 File Offset: 0x0013C895
		public static LocalizedString ConversationItemQueryFailed
		{
			get
			{
				return new LocalizedString("ConversationItemQueryFailed", "Ex1B61EC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x06006469 RID: 25705 RVA: 0x0013E6B3 File Offset: 0x0013C8B3
		public static LocalizedString ErrorADOperation
		{
			get
			{
				return new LocalizedString("ErrorADOperation", "ExBF6F3C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x0600646A RID: 25706 RVA: 0x0013E6D1 File Offset: 0x0013C8D1
		public static LocalizedString ErrorCalendarIsOrganizerForAccept
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsOrganizerForAccept", "Ex4F8BBD", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600646B RID: 25707 RVA: 0x0013E6F0 File Offset: 0x0013C8F0
		public static LocalizedString IrmRmsErrorLocation(string uri)
		{
			return new LocalizedString("IrmRmsErrorLocation", "", false, false, CoreResources.ResourceManager, new object[]
			{
				uri
			});
		}

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x0600646C RID: 25708 RVA: 0x0013E71F File Offset: 0x0013C91F
		public static LocalizedString ErrorCannotDeleteTaskOccurrence
		{
			get
			{
				return new LocalizedString("ErrorCannotDeleteTaskOccurrence", "Ex02031F", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x0600646D RID: 25709 RVA: 0x0013E73D File Offset: 0x0013C93D
		public static LocalizedString ErrorTooManyContactsException
		{
			get
			{
				return new LocalizedString("ErrorTooManyContactsException", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x0600646E RID: 25710 RVA: 0x0013E75B File Offset: 0x0013C95B
		public static LocalizedString ErrorReadEventsFailed
		{
			get
			{
				return new LocalizedString("ErrorReadEventsFailed", "ExD3DF4E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x0600646F RID: 25711 RVA: 0x0013E779 File Offset: 0x0013C979
		public static LocalizedString descInvalidEIParameter
		{
			get
			{
				return new LocalizedString("descInvalidEIParameter", "Ex3FE4B5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x06006470 RID: 25712 RVA: 0x0013E797 File Offset: 0x0013C997
		public static LocalizedString ErrorDuplicateLegacyDistinguishedName
		{
			get
			{
				return new LocalizedString("ErrorDuplicateLegacyDistinguishedName", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x06006471 RID: 25713 RVA: 0x0013E7B5 File Offset: 0x0013C9B5
		public static LocalizedString MessageActingAsIsNotAValidEmailAddress
		{
			get
			{
				return new LocalizedString("MessageActingAsIsNotAValidEmailAddress", "Ex31868D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x06006472 RID: 25714 RVA: 0x0013E7D3 File Offset: 0x0013C9D3
		public static LocalizedString MessageInvalidServerVersionForJsonRequest
		{
			get
			{
				return new LocalizedString("MessageInvalidServerVersionForJsonRequest", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x06006473 RID: 25715 RVA: 0x0013E7F1 File Offset: 0x0013C9F1
		public static LocalizedString ErrorCalendarCannotMoveOrCopyOccurrence
		{
			get
			{
				return new LocalizedString("ErrorCalendarCannotMoveOrCopyOccurrence", "ExECABF5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x06006474 RID: 25716 RVA: 0x0013E80F File Offset: 0x0013CA0F
		public static LocalizedString ErrorPeopleConnectionNotFound
		{
			get
			{
				return new LocalizedString("ErrorPeopleConnectionNotFound", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x06006475 RID: 25717 RVA: 0x0013E82D File Offset: 0x0013CA2D
		public static LocalizedString ErrorCalendarMeetingIsOutOfDateResponseNotProcessedMessageSent
		{
			get
			{
				return new LocalizedString("ErrorCalendarMeetingIsOutOfDateResponseNotProcessedMessageSent", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x06006476 RID: 25718 RVA: 0x0013E84B File Offset: 0x0013CA4B
		public static LocalizedString ErrorInvalidExcludesRestriction
		{
			get
			{
				return new LocalizedString("ErrorInvalidExcludesRestriction", "ExFBE947", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x06006477 RID: 25719 RVA: 0x0013E869 File Offset: 0x0013CA69
		public static LocalizedString ErrorMoreThanOneAccessModeSpecified
		{
			get
			{
				return new LocalizedString("ErrorMoreThanOneAccessModeSpecified", "Ex5B156F", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x06006478 RID: 25720 RVA: 0x0013E887 File Offset: 0x0013CA87
		public static LocalizedString ErrorCreateSubfolderAccessDenied
		{
			get
			{
				return new LocalizedString("ErrorCreateSubfolderAccessDenied", "Ex26CEA3", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x06006479 RID: 25721 RVA: 0x0013E8A5 File Offset: 0x0013CAA5
		public static LocalizedString ErrorInvalidMailboxIdFormat
		{
			get
			{
				return new LocalizedString("ErrorInvalidMailboxIdFormat", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x0600647A RID: 25722 RVA: 0x0013E8C3 File Offset: 0x0013CAC3
		public static LocalizedString ErrorCalendarIsCancelledForAccept
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsCancelledForAccept", "ExE945D4", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x0600647B RID: 25723 RVA: 0x0013E8E1 File Offset: 0x0013CAE1
		public static LocalizedString MessageApplicationRoleShouldPresentWhenUserRolePresent
		{
			get
			{
				return new LocalizedString("MessageApplicationRoleShouldPresentWhenUserRolePresent", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x0600647C RID: 25724 RVA: 0x0013E8FF File Offset: 0x0013CAFF
		public static LocalizedString ErrorInvalidUMSubscriberDataTimeoutValue
		{
			get
			{
				return new LocalizedString("ErrorInvalidUMSubscriberDataTimeoutValue", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x0600647D RID: 25725 RVA: 0x0013E91D File Offset: 0x0013CB1D
		public static LocalizedString ErrorSearchTimeoutExpired
		{
			get
			{
				return new LocalizedString("ErrorSearchTimeoutExpired", "ExEEADCE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600647E RID: 25726 RVA: 0x0013E93C File Offset: 0x0013CB3C
		public static LocalizedString RuleErrorInvalidSizeRange(int minimumSize, int maximumSize)
		{
			return new LocalizedString("RuleErrorInvalidSizeRange", "Ex4FF3F2", false, true, CoreResources.ResourceManager, new object[]
			{
				minimumSize,
				maximumSize
			});
		}

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x0600647F RID: 25727 RVA: 0x0013E979 File Offset: 0x0013CB79
		public static LocalizedString descLocalServerConfigurationRetrievalFailed
		{
			get
			{
				return new LocalizedString("descLocalServerConfigurationRetrievalFailed", "Ex2BE2B5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06006480 RID: 25728 RVA: 0x0013E997 File Offset: 0x0013CB97
		public static LocalizedString ErrorInvalidContactEmailAddress
		{
			get
			{
				return new LocalizedString("ErrorInvalidContactEmailAddress", "Ex72374D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x06006481 RID: 25729 RVA: 0x0013E9B5 File Offset: 0x0013CBB5
		public static LocalizedString ErrorInvalidValueForPropertyStringArrayDictionaryKey
		{
			get
			{
				return new LocalizedString("ErrorInvalidValueForPropertyStringArrayDictionaryKey", "Ex2F0895", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x06006482 RID: 25730 RVA: 0x0013E9D3 File Offset: 0x0013CBD3
		public static LocalizedString ErrorChangeKeyRequiredForWriteOperations
		{
			get
			{
				return new LocalizedString("ErrorChangeKeyRequiredForWriteOperations", "ExD38F2F", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x06006483 RID: 25731 RVA: 0x0013E9F1 File Offset: 0x0013CBF1
		public static LocalizedString ErrorMissingEmailAddress
		{
			get
			{
				return new LocalizedString("ErrorMissingEmailAddress", "Ex0AACA0", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06006484 RID: 25732 RVA: 0x0013EA0F File Offset: 0x0013CC0F
		public static LocalizedString ErrorFullSyncRequiredException
		{
			get
			{
				return new LocalizedString("ErrorFullSyncRequiredException", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06006485 RID: 25733 RVA: 0x0013EA2D File Offset: 0x0013CC2D
		public static LocalizedString ErrorADSessionFilter
		{
			get
			{
				return new LocalizedString("ErrorADSessionFilter", "Ex82EE97", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06006486 RID: 25734 RVA: 0x0013EA4B File Offset: 0x0013CC4B
		public static LocalizedString ErrorDistinguishedUserNotSupported
		{
			get
			{
				return new LocalizedString("ErrorDistinguishedUserNotSupported", "Ex964408", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x06006487 RID: 25735 RVA: 0x0013EA69 File Offset: 0x0013CC69
		public static LocalizedString ErrorCrossForestCallerNeedsADObject
		{
			get
			{
				return new LocalizedString("ErrorCrossForestCallerNeedsADObject", "Ex126808", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06006488 RID: 25736 RVA: 0x0013EA87 File Offset: 0x0013CC87
		public static LocalizedString ErrorSendMeetingInvitationsOrCancellationsRequired
		{
			get
			{
				return new LocalizedString("ErrorSendMeetingInvitationsOrCancellationsRequired", "ExFDD460", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x06006489 RID: 25737 RVA: 0x0013EAA5 File Offset: 0x0013CCA5
		public static LocalizedString RuleErrorDuplicatedOperationOnTheSameRule
		{
			get
			{
				return new LocalizedString("RuleErrorDuplicatedOperationOnTheSameRule", "Ex6F0889", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x0600648A RID: 25738 RVA: 0x0013EAC3 File Offset: 0x0013CCC3
		public static LocalizedString ErrorDeletePersonaOnInvalidFolder
		{
			get
			{
				return new LocalizedString("ErrorDeletePersonaOnInvalidFolder", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x0600648B RID: 25739 RVA: 0x0013EAE1 File Offset: 0x0013CCE1
		public static LocalizedString ErrorCannotAddAggregatedAccountMailbox
		{
			get
			{
				return new LocalizedString("ErrorCannotAddAggregatedAccountMailbox", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x0600648C RID: 25740 RVA: 0x0013EAFF File Offset: 0x0013CCFF
		public static LocalizedString ErrorExceededConnectionCount
		{
			get
			{
				return new LocalizedString("ErrorExceededConnectionCount", "Ex8D752F", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x0600648D RID: 25741 RVA: 0x0013EB1D File Offset: 0x0013CD1D
		public static LocalizedString ErrorFolderSavePropertyError
		{
			get
			{
				return new LocalizedString("ErrorFolderSavePropertyError", "Ex5E13B1", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x0600648E RID: 25742 RVA: 0x0013EB3B File Offset: 0x0013CD3B
		public static LocalizedString ErrorCannotUsePersonalContactsAsRecipientsOrAttendees
		{
			get
			{
				return new LocalizedString("ErrorCannotUsePersonalContactsAsRecipientsOrAttendees", "Ex136B8C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x0600648F RID: 25743 RVA: 0x0013EB59 File Offset: 0x0013CD59
		public static LocalizedString ErrorInvalidItemForForward
		{
			get
			{
				return new LocalizedString("ErrorInvalidItemForForward", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x06006490 RID: 25744 RVA: 0x0013EB77 File Offset: 0x0013CD77
		public static LocalizedString ErrorChangeKeyRequired
		{
			get
			{
				return new LocalizedString("ErrorChangeKeyRequired", "Ex5996C7", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x06006491 RID: 25745 RVA: 0x0013EB95 File Offset: 0x0013CD95
		public static LocalizedString ErrorNotAcceptable
		{
			get
			{
				return new LocalizedString("ErrorNotAcceptable", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x06006492 RID: 25746 RVA: 0x0013EBB3 File Offset: 0x0013CDB3
		public static LocalizedString ErrorMessageTrackingNoSuchDomain
		{
			get
			{
				return new LocalizedString("ErrorMessageTrackingNoSuchDomain", "Ex8E0EFF", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06006493 RID: 25747 RVA: 0x0013EBD1 File Offset: 0x0013CDD1
		public static LocalizedString ErrorTraversalNotAllowedWithoutQueryString
		{
			get
			{
				return new LocalizedString("ErrorTraversalNotAllowedWithoutQueryString", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06006494 RID: 25748 RVA: 0x0013EBEF File Offset: 0x0013CDEF
		public static LocalizedString ErrorOrganizationAccessBlocked
		{
			get
			{
				return new LocalizedString("ErrorOrganizationAccessBlocked", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06006495 RID: 25749 RVA: 0x0013EC0D File Offset: 0x0013CE0D
		public static LocalizedString ErrorInvalidNumberOfMailboxSearch
		{
			get
			{
				return new LocalizedString("ErrorInvalidNumberOfMailboxSearch", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x06006496 RID: 25750 RVA: 0x0013EC2B File Offset: 0x0013CE2B
		public static LocalizedString ErrorCreateManagedFolderPartialCompletion
		{
			get
			{
				return new LocalizedString("ErrorCreateManagedFolderPartialCompletion", "ExBAD6DC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x06006497 RID: 25751 RVA: 0x0013EC49 File Offset: 0x0013CE49
		public static LocalizedString UpdateFavoritesUnableToRenameFavorite
		{
			get
			{
				return new LocalizedString("UpdateFavoritesUnableToRenameFavorite", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06006498 RID: 25752 RVA: 0x0013EC67 File Offset: 0x0013CE67
		public static LocalizedString ErrorActiveDirectoryTransientError
		{
			get
			{
				return new LocalizedString("ErrorActiveDirectoryTransientError", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x06006499 RID: 25753 RVA: 0x0013EC85 File Offset: 0x0013CE85
		public static LocalizedString ErrorInvalidSubscriptionRequestAllFoldersWithFolderIds
		{
			get
			{
				return new LocalizedString("ErrorInvalidSubscriptionRequestAllFoldersWithFolderIds", "Ex96F104", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x0600649A RID: 25754 RVA: 0x0013ECA3 File Offset: 0x0013CEA3
		public static LocalizedString ErrorInvalidOperationSendMeetingInvitationCancellationForPublicFolderItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationSendMeetingInvitationCancellationForPublicFolderItem", "Ex93893D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x0600649B RID: 25755 RVA: 0x0013ECC1 File Offset: 0x0013CEC1
		public static LocalizedString ErrorIrresolvableConflict
		{
			get
			{
				return new LocalizedString("ErrorIrresolvableConflict", "Ex926A33", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x0600649C RID: 25756 RVA: 0x0013ECDF File Offset: 0x0013CEDF
		public static LocalizedString ErrorInvalidItemForReplyAll
		{
			get
			{
				return new LocalizedString("ErrorInvalidItemForReplyAll", "ExD5C17A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x0600649D RID: 25757 RVA: 0x0013ECFD File Offset: 0x0013CEFD
		public static LocalizedString ErrorPhoneNumberNotDialable
		{
			get
			{
				return new LocalizedString("ErrorPhoneNumberNotDialable", "ExFCBEF5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x0600649E RID: 25758 RVA: 0x0013ED1B File Offset: 0x0013CF1B
		public static LocalizedString ErrorInvalidInternetHeaderChildNodes
		{
			get
			{
				return new LocalizedString("ErrorInvalidInternetHeaderChildNodes", "Ex032D34", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x0600649F RID: 25759 RVA: 0x0013ED39 File Offset: 0x0013CF39
		public static LocalizedString ErrorInvalidExpressionTypeForSubFilter
		{
			get
			{
				return new LocalizedString("ErrorInvalidExpressionTypeForSubFilter", "Ex259E33", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x060064A0 RID: 25760 RVA: 0x0013ED57 File Offset: 0x0013CF57
		public static LocalizedString MessageResolveNamesNotSufficientPermissionsToPrivateDLMember
		{
			get
			{
				return new LocalizedString("MessageResolveNamesNotSufficientPermissionsToPrivateDLMember", "ExFBCE9A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x060064A1 RID: 25761 RVA: 0x0013ED75 File Offset: 0x0013CF75
		public static LocalizedString ErrorCannotSetNonCalendarPermissionOnCalendarFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotSetNonCalendarPermissionOnCalendarFolder", "Ex54F76F", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x060064A2 RID: 25762 RVA: 0x0013ED93 File Offset: 0x0013CF93
		public static LocalizedString ErrorParentFolderIdRequired
		{
			get
			{
				return new LocalizedString("ErrorParentFolderIdRequired", "ExA7F8D6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x060064A3 RID: 25763 RVA: 0x0013EDB1 File Offset: 0x0013CFB1
		public static LocalizedString ErrorEventNotFound
		{
			get
			{
				return new LocalizedString("ErrorEventNotFound", "Ex901B01", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x060064A4 RID: 25764 RVA: 0x0013EDCF File Offset: 0x0013CFCF
		public static LocalizedString ErrorVoiceMailNotImplemented
		{
			get
			{
				return new LocalizedString("ErrorVoiceMailNotImplemented", "Ex3EB0D9", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060064A5 RID: 25765 RVA: 0x0013EDF0 File Offset: 0x0013CFF0
		public static LocalizedString ExchangeServiceResponseErrorNoResponseForType(string responseType)
		{
			return new LocalizedString("ExchangeServiceResponseErrorNoResponseForType", "", false, false, CoreResources.ResourceManager, new object[]
			{
				responseType
			});
		}

		// Token: 0x060064A6 RID: 25766 RVA: 0x0013EE20 File Offset: 0x0013D020
		public static LocalizedString ErrorAccountNotSupportedForAggregation(string email)
		{
			return new LocalizedString("ErrorAccountNotSupportedForAggregation", "", false, false, CoreResources.ResourceManager, new object[]
			{
				email
			});
		}

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x060064A7 RID: 25767 RVA: 0x0013EE4F File Offset: 0x0013D04F
		public static LocalizedString ErrorDeleteDistinguishedFolder
		{
			get
			{
				return new LocalizedString("ErrorDeleteDistinguishedFolder", "ExCA65B2", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x060064A8 RID: 25768 RVA: 0x0013EE6D File Offset: 0x0013D06D
		public static LocalizedString ErrorNoPermissionToSearchOrHoldMailbox
		{
			get
			{
				return new LocalizedString("ErrorNoPermissionToSearchOrHoldMailbox", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x060064A9 RID: 25769 RVA: 0x0013EE8B File Offset: 0x0013D08B
		public static LocalizedString ErrorExchangeApplicationNotEnabled
		{
			get
			{
				return new LocalizedString("ErrorExchangeApplicationNotEnabled", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x060064AA RID: 25770 RVA: 0x0013EEA9 File Offset: 0x0013D0A9
		public static LocalizedString ErrorResolveNamesInvalidFolderType
		{
			get
			{
				return new LocalizedString("ErrorResolveNamesInvalidFolderType", "ExC8376A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x060064AB RID: 25771 RVA: 0x0013EEC7 File Offset: 0x0013D0C7
		public static LocalizedString ErrorExceededFindCountLimit
		{
			get
			{
				return new LocalizedString("ErrorExceededFindCountLimit", "ExC12DC8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x060064AC RID: 25772 RVA: 0x0013EEE5 File Offset: 0x0013D0E5
		public static LocalizedString MessageExtensionAccessActAsMailboxOnly
		{
			get
			{
				return new LocalizedString("MessageExtensionAccessActAsMailboxOnly", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x060064AD RID: 25773 RVA: 0x0013EF03 File Offset: 0x0013D103
		public static LocalizedString ErrorPasswordChangeRequired
		{
			get
			{
				return new LocalizedString("ErrorPasswordChangeRequired", "ExDD78FA", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x060064AE RID: 25774 RVA: 0x0013EF21 File Offset: 0x0013D121
		public static LocalizedString ErrorInvalidManagedFolderProperty
		{
			get
			{
				return new LocalizedString("ErrorInvalidManagedFolderProperty", "ExA0963B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x060064AF RID: 25775 RVA: 0x0013EF3F File Offset: 0x0013D13F
		public static LocalizedString ErrorInvalidIdMalformedEwsLegacyIdFormat
		{
			get
			{
				return new LocalizedString("ErrorInvalidIdMalformedEwsLegacyIdFormat", "ExC7A6F0", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x060064B0 RID: 25776 RVA: 0x0013EF5D File Offset: 0x0013D15D
		public static LocalizedString ErrorSchemaViolation
		{
			get
			{
				return new LocalizedString("ErrorSchemaViolation", "ExA59402", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060064B1 RID: 25777 RVA: 0x0013EF7C File Offset: 0x0013D17C
		public static LocalizedString NewGroupMailboxFailed(string name, string error)
		{
			return new LocalizedString("NewGroupMailboxFailed", "", false, false, CoreResources.ResourceManager, new object[]
			{
				name,
				error
			});
		}

		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x060064B2 RID: 25778 RVA: 0x0013EFAF File Offset: 0x0013D1AF
		public static LocalizedString MessageInvalidMailboxContactAddressNotFound
		{
			get
			{
				return new LocalizedString("MessageInvalidMailboxContactAddressNotFound", "Ex8C5ACD", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x060064B3 RID: 25779 RVA: 0x0013EFCD File Offset: 0x0013D1CD
		public static LocalizedString ErrorInvalidIndexedPagingParameters
		{
			get
			{
				return new LocalizedString("ErrorInvalidIndexedPagingParameters", "ExF15AA5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x060064B4 RID: 25780 RVA: 0x0013EFEB File Offset: 0x0013D1EB
		public static LocalizedString ErrorUnsupportedPathForQuery
		{
			get
			{
				return new LocalizedString("ErrorUnsupportedPathForQuery", "Ex3680D2", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x060064B5 RID: 25781 RVA: 0x0013F009 File Offset: 0x0013D209
		public static LocalizedString ErrorInvalidOperationDelegationAssociatedItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationDelegationAssociatedItem", "Ex2D7706", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x060064B6 RID: 25782 RVA: 0x0013F027 File Offset: 0x0013D227
		public static LocalizedString ErrorRemoteUserMailboxMustSpecifyExplicitLocalMailbox
		{
			get
			{
				return new LocalizedString("ErrorRemoteUserMailboxMustSpecifyExplicitLocalMailbox", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x060064B7 RID: 25783 RVA: 0x0013F045 File Offset: 0x0013D245
		public static LocalizedString ErrorNoDestinationCASDueToVersionMismatch
		{
			get
			{
				return new LocalizedString("ErrorNoDestinationCASDueToVersionMismatch", "ExAFEEE0", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x060064B8 RID: 25784 RVA: 0x0013F063 File Offset: 0x0013D263
		public static LocalizedString ErrorInvalidValueForPropertyBinaryData
		{
			get
			{
				return new LocalizedString("ErrorInvalidValueForPropertyBinaryData", "Ex2E726C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x060064B9 RID: 25785 RVA: 0x0013F081 File Offset: 0x0013D281
		public static LocalizedString ErrorNotDelegate
		{
			get
			{
				return new LocalizedString("ErrorNotDelegate", "Ex850DEA", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x060064BA RID: 25786 RVA: 0x0013F09F File Offset: 0x0013D29F
		public static LocalizedString ErrorCalendarInvalidDayForTimeChangePattern
		{
			get
			{
				return new LocalizedString("ErrorCalendarInvalidDayForTimeChangePattern", "ExB1A176", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060064BB RID: 25787 RVA: 0x0013F0C0 File Offset: 0x0013D2C0
		public static LocalizedString ErrorInvalidProperty(string property)
		{
			return new LocalizedString("ErrorInvalidProperty", "", false, false, CoreResources.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x060064BC RID: 25788 RVA: 0x0013F0EF File Offset: 0x0013D2EF
		public static LocalizedString ErrorInvalidPullSubscriptionId
		{
			get
			{
				return new LocalizedString("ErrorInvalidPullSubscriptionId", "ExCBB4CC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x060064BD RID: 25789 RVA: 0x0013F10D File Offset: 0x0013D30D
		public static LocalizedString ErrorCannotCopyPublicFolderRoot
		{
			get
			{
				return new LocalizedString("ErrorCannotCopyPublicFolderRoot", "Ex8C2BFD", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x060064BE RID: 25790 RVA: 0x0013F12B File Offset: 0x0013D32B
		public static LocalizedString MessageOperationRequiresUserContext
		{
			get
			{
				return new LocalizedString("MessageOperationRequiresUserContext", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x060064BF RID: 25791 RVA: 0x0013F149 File Offset: 0x0013D349
		public static LocalizedString ErrorPromptPublishingOperationFailed
		{
			get
			{
				return new LocalizedString("ErrorPromptPublishingOperationFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x060064C0 RID: 25792 RVA: 0x0013F167 File Offset: 0x0013D367
		public static LocalizedString ErrorInvalidFractionalPagingParameters
		{
			get
			{
				return new LocalizedString("ErrorInvalidFractionalPagingParameters", "Ex1CE32C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x060064C1 RID: 25793 RVA: 0x0013F185 File Offset: 0x0013D385
		public static LocalizedString ErrorPublicFolderMailboxDiscoveryFailed
		{
			get
			{
				return new LocalizedString("ErrorPublicFolderMailboxDiscoveryFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x060064C2 RID: 25794 RVA: 0x0013F1A3 File Offset: 0x0013D3A3
		public static LocalizedString ErrorUnableToRemoveImContactFromGroup
		{
			get
			{
				return new LocalizedString("ErrorUnableToRemoveImContactFromGroup", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x060064C3 RID: 25795 RVA: 0x0013F1C1 File Offset: 0x0013D3C1
		public static LocalizedString ErrorSendMeetingCancellationsRequired
		{
			get
			{
				return new LocalizedString("ErrorSendMeetingCancellationsRequired", "ExF3C1DA", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x060064C4 RID: 25796 RVA: 0x0013F1DF File Offset: 0x0013D3DF
		public static LocalizedString MessageRecipientsArrayMustNotBeEmpty
		{
			get
			{
				return new LocalizedString("MessageRecipientsArrayMustNotBeEmpty", "Ex79FA63", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x060064C5 RID: 25797 RVA: 0x0013F1FD File Offset: 0x0013D3FD
		public static LocalizedString ErrorInvalidItemForOperationTentative
		{
			get
			{
				return new LocalizedString("ErrorInvalidItemForOperationTentative", "ExC6A2C3", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x060064C6 RID: 25798 RVA: 0x0013F21B File Offset: 0x0013D41B
		public static LocalizedString ErrorInvalidReferenceItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidReferenceItem", "ExC62CDC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x060064C7 RID: 25799 RVA: 0x0013F239 File Offset: 0x0013D439
		public static LocalizedString IrmReachNotConfigured
		{
			get
			{
				return new LocalizedString("IrmReachNotConfigured", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x060064C8 RID: 25800 RVA: 0x0013F257 File Offset: 0x0013D457
		public static LocalizedString ErrorMimeContentInvalidBase64String
		{
			get
			{
				return new LocalizedString("ErrorMimeContentInvalidBase64String", "Ex822BE8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x060064C9 RID: 25801 RVA: 0x0013F275 File Offset: 0x0013D475
		public static LocalizedString ErrorSentTaskRequestUpdate
		{
			get
			{
				return new LocalizedString("ErrorSentTaskRequestUpdate", "ExEBF2A6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x060064CA RID: 25802 RVA: 0x0013F293 File Offset: 0x0013D493
		public static LocalizedString ErrorFoundSyncRequestForNonAggregatedAccount
		{
			get
			{
				return new LocalizedString("ErrorFoundSyncRequestForNonAggregatedAccount", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x060064CB RID: 25803 RVA: 0x0013F2B1 File Offset: 0x0013D4B1
		public static LocalizedString MessagePropertyIsDeprecatedForThisVersion
		{
			get
			{
				return new LocalizedString("MessagePropertyIsDeprecatedForThisVersion", "Ex2AFD57", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060064CC RID: 25804 RVA: 0x0013F2D0 File Offset: 0x0013D4D0
		public static LocalizedString ErrorInvalidUnifiedViewParameter(string parameterName)
		{
			return new LocalizedString("ErrorInvalidUnifiedViewParameter", "", false, false, CoreResources.ResourceManager, new object[]
			{
				parameterName
			});
		}

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x060064CD RID: 25805 RVA: 0x0013F2FF File Offset: 0x0013D4FF
		public static LocalizedString ErrorInvalidOperationContactsViewAssociatedItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationContactsViewAssociatedItem", "Ex3A3CE8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x060064CE RID: 25806 RVA: 0x0013F31D File Offset: 0x0013D51D
		public static LocalizedString ErrorServerBusy
		{
			get
			{
				return new LocalizedString("ErrorServerBusy", "Ex631C4E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x060064CF RID: 25807 RVA: 0x0013F33B File Offset: 0x0013D53B
		public static LocalizedString ConversationActionNeedRetentionPolicyTypeForSetRetentionPolicy
		{
			get
			{
				return new LocalizedString("ConversationActionNeedRetentionPolicyTypeForSetRetentionPolicy", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x060064D0 RID: 25808 RVA: 0x0013F359 File Offset: 0x0013D559
		public static LocalizedString ErrorCannotDeletePublicFolderRoot
		{
			get
			{
				return new LocalizedString("ErrorCannotDeletePublicFolderRoot", "Ex1E59DE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x060064D1 RID: 25809 RVA: 0x0013F377 File Offset: 0x0013D577
		public static LocalizedString ErrorImGroupDisplayNameAlreadyExists
		{
			get
			{
				return new LocalizedString("ErrorImGroupDisplayNameAlreadyExists", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x060064D2 RID: 25810 RVA: 0x0013F395 File Offset: 0x0013D595
		public static LocalizedString NoServer
		{
			get
			{
				return new LocalizedString("NoServer", "ExE10847", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x060064D3 RID: 25811 RVA: 0x0013F3B3 File Offset: 0x0013D5B3
		public static LocalizedString ErrorInvalidImDistributionGroupSmtpAddress
		{
			get
			{
				return new LocalizedString("ErrorInvalidImDistributionGroupSmtpAddress", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x060064D4 RID: 25812 RVA: 0x0013F3D1 File Offset: 0x0013D5D1
		public static LocalizedString ErrorSubscriptionDelegateAccessNotSupported
		{
			get
			{
				return new LocalizedString("ErrorSubscriptionDelegateAccessNotSupported", "ExE9CBB8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x060064D5 RID: 25813 RVA: 0x0013F3EF File Offset: 0x0013D5EF
		public static LocalizedString RuleErrorItemIsNotTemplate
		{
			get
			{
				return new LocalizedString("RuleErrorItemIsNotTemplate", "ExA942E0", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x060064D6 RID: 25814 RVA: 0x0013F40D File Offset: 0x0013D60D
		public static LocalizedString ErrorCannotSetPermissionUnknownEntries
		{
			get
			{
				return new LocalizedString("ErrorCannotSetPermissionUnknownEntries", "Ex0F0A47", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x060064D7 RID: 25815 RVA: 0x0013F42B File Offset: 0x0013D62B
		public static LocalizedString MessageIdOrTokenTypeNotFound
		{
			get
			{
				return new LocalizedString("MessageIdOrTokenTypeNotFound", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060064D8 RID: 25816 RVA: 0x0013F44C File Offset: 0x0013D64C
		public static LocalizedString ErrorRightsManagementTemplateNotFound(string id)
		{
			return new LocalizedString("ErrorRightsManagementTemplateNotFound", "", false, false, CoreResources.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x060064D9 RID: 25817 RVA: 0x0013F47B File Offset: 0x0013D67B
		public static LocalizedString ErrorLocationServicesDisabled
		{
			get
			{
				return new LocalizedString("ErrorLocationServicesDisabled", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x060064DA RID: 25818 RVA: 0x0013F499 File Offset: 0x0013D699
		public static LocalizedString MessageNotSupportedApplicationRole
		{
			get
			{
				return new LocalizedString("MessageNotSupportedApplicationRole", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x060064DB RID: 25819 RVA: 0x0013F4B7 File Offset: 0x0013D6B7
		public static LocalizedString ErrorPublicFolderSyncException
		{
			get
			{
				return new LocalizedString("ErrorPublicFolderSyncException", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x060064DC RID: 25820 RVA: 0x0013F4D5 File Offset: 0x0013D6D5
		public static LocalizedString ErrorCalendarIsDelegatedForDecline
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsDelegatedForDecline", "Ex51730C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x060064DD RID: 25821 RVA: 0x0013F4F3 File Offset: 0x0013D6F3
		public static LocalizedString ErrorUnsupportedODataRequest
		{
			get
			{
				return new LocalizedString("ErrorUnsupportedODataRequest", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x060064DE RID: 25822 RVA: 0x0013F511 File Offset: 0x0013D711
		public static LocalizedString ErrorDeepTraversalsNotAllowedOnPublicFolders
		{
			get
			{
				return new LocalizedString("ErrorDeepTraversalsNotAllowedOnPublicFolders", "ExF3A6EB", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x060064DF RID: 25823 RVA: 0x0013F52F File Offset: 0x0013D72F
		public static LocalizedString MessageCouldNotFindWeatherLocationsForSearchString
		{
			get
			{
				return new LocalizedString("MessageCouldNotFindWeatherLocationsForSearchString", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x060064E0 RID: 25824 RVA: 0x0013F54D File Offset: 0x0013D74D
		public static LocalizedString ErrorInvalidPropertyForSortBy
		{
			get
			{
				return new LocalizedString("ErrorInvalidPropertyForSortBy", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x060064E1 RID: 25825 RVA: 0x0013F56B File Offset: 0x0013D76B
		public static LocalizedString MessageCalendarUnableToGetAssociatedCalendarItem
		{
			get
			{
				return new LocalizedString("MessageCalendarUnableToGetAssociatedCalendarItem", "ExD82E7F", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x060064E2 RID: 25826 RVA: 0x0013F589 File Offset: 0x0013D789
		public static LocalizedString ErrorSortByPropertyIsNotFoundOrNotSupported
		{
			get
			{
				return new LocalizedString("ErrorSortByPropertyIsNotFoundOrNotSupported", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001757 RID: 5975
		// (get) Token: 0x060064E3 RID: 25827 RVA: 0x0013F5A7 File Offset: 0x0013D7A7
		public static LocalizedString ErrorNotSupportedSharingMessage
		{
			get
			{
				return new LocalizedString("ErrorNotSupportedSharingMessage", "ExB5C5A5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x060064E4 RID: 25828 RVA: 0x0013F5C5 File Offset: 0x0013D7C5
		public static LocalizedString ErrorMissingInformationReferenceItemId
		{
			get
			{
				return new LocalizedString("ErrorMissingInformationReferenceItemId", "Ex927959", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x060064E5 RID: 25829 RVA: 0x0013F5E3 File Offset: 0x0013D7E3
		public static LocalizedString ErrorInvalidSIPUri
		{
			get
			{
				return new LocalizedString("ErrorInvalidSIPUri", "Ex325805", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x060064E6 RID: 25830 RVA: 0x0013F601 File Offset: 0x0013D801
		public static LocalizedString ErrorInvalidCompleteDateOutOfRange
		{
			get
			{
				return new LocalizedString("ErrorInvalidCompleteDateOutOfRange", "Ex48AF3C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x060064E7 RID: 25831 RVA: 0x0013F61F File Offset: 0x0013D81F
		public static LocalizedString ErrorUnifiedMessagingDialPlanNotFound
		{
			get
			{
				return new LocalizedString("ErrorUnifiedMessagingDialPlanNotFound", "ExCE21DC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x060064E8 RID: 25832 RVA: 0x0013F63D File Offset: 0x0013D83D
		public static LocalizedString MessageRecipientMustHaveRoutingType
		{
			get
			{
				return new LocalizedString("MessageRecipientMustHaveRoutingType", "Ex3E2F67", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x060064E9 RID: 25833 RVA: 0x0013F65B File Offset: 0x0013D85B
		public static LocalizedString MessageResolveNamesNotSufficientPermissionsToPrivateDL
		{
			get
			{
				return new LocalizedString("MessageResolveNamesNotSufficientPermissionsToPrivateDL", "Ex30B3A4", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060064EA RID: 25834 RVA: 0x0013F67C File Offset: 0x0013D87C
		public static LocalizedString TooManyMailboxQueryObjects(int entryCount, int maxAllowedCount)
		{
			return new LocalizedString("TooManyMailboxQueryObjects", "", false, false, CoreResources.ResourceManager, new object[]
			{
				entryCount,
				maxAllowedCount
			});
		}

		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x060064EB RID: 25835 RVA: 0x0013F6B9 File Offset: 0x0013D8B9
		public static LocalizedString MessageMissingUserRolesForOrganizationConfigurationRoleTypeApp
		{
			get
			{
				return new LocalizedString("MessageMissingUserRolesForOrganizationConfigurationRoleTypeApp", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x060064EC RID: 25836 RVA: 0x0013F6D7 File Offset: 0x0013D8D7
		public static LocalizedString ErrorInvalidUserSid
		{
			get
			{
				return new LocalizedString("ErrorInvalidUserSid", "ExFA6FD9", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x060064ED RID: 25837 RVA: 0x0013F6F5 File Offset: 0x0013D8F5
		public static LocalizedString ErrorInvalidRecipientSubfilter
		{
			get
			{
				return new LocalizedString("ErrorInvalidRecipientSubfilter", "Ex6BCAB8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001761 RID: 5985
		// (get) Token: 0x060064EE RID: 25838 RVA: 0x0013F713 File Offset: 0x0013D913
		public static LocalizedString ErrorSuffixSearchNotAllowed
		{
			get
			{
				return new LocalizedString("ErrorSuffixSearchNotAllowed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x060064EF RID: 25839 RVA: 0x0013F731 File Offset: 0x0013D931
		public static LocalizedString ErrorUnifiedMessagingReportDataNotFound
		{
			get
			{
				return new LocalizedString("ErrorUnifiedMessagingReportDataNotFound", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x060064F0 RID: 25840 RVA: 0x0013F74F File Offset: 0x0013D94F
		public static LocalizedString UpdateFavoritesFolderAlreadyInFavorites
		{
			get
			{
				return new LocalizedString("UpdateFavoritesFolderAlreadyInFavorites", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x060064F1 RID: 25841 RVA: 0x0013F76D File Offset: 0x0013D96D
		public static LocalizedString MessageManagementRoleHeaderNotSupportedForOfficeExtension
		{
			get
			{
				return new LocalizedString("MessageManagementRoleHeaderNotSupportedForOfficeExtension", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x060064F2 RID: 25842 RVA: 0x0013F78B File Offset: 0x0013D98B
		public static LocalizedString OneDriveProAttachmentDataProviderName
		{
			get
			{
				return new LocalizedString("OneDriveProAttachmentDataProviderName", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x060064F3 RID: 25843 RVA: 0x0013F7A9 File Offset: 0x0013D9A9
		public static LocalizedString ErrorCalendarInvalidAttributeValue
		{
			get
			{
				return new LocalizedString("ErrorCalendarInvalidAttributeValue", "Ex347803", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x060064F4 RID: 25844 RVA: 0x0013F7C7 File Offset: 0x0013D9C7
		public static LocalizedString MessageInvalidRecurrenceFormat
		{
			get
			{
				return new LocalizedString("MessageInvalidRecurrenceFormat", "Ex67F0C8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x060064F5 RID: 25845 RVA: 0x0013F7E5 File Offset: 0x0013D9E5
		public static LocalizedString ErrorInvalidAppApiVersionSupported
		{
			get
			{
				return new LocalizedString("ErrorInvalidAppApiVersionSupported", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x060064F6 RID: 25846 RVA: 0x0013F803 File Offset: 0x0013DA03
		public static LocalizedString ErrorInvalidManagedFolderSize
		{
			get
			{
				return new LocalizedString("ErrorInvalidManagedFolderSize", "Ex430991", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700176A RID: 5994
		// (get) Token: 0x060064F7 RID: 25847 RVA: 0x0013F821 File Offset: 0x0013DA21
		public static LocalizedString ErrorTokenSerializationDenied
		{
			get
			{
				return new LocalizedString("ErrorTokenSerializationDenied", "ExEEBC40", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x060064F8 RID: 25848 RVA: 0x0013F83F File Offset: 0x0013DA3F
		public static LocalizedString ErrorInvalidRequest
		{
			get
			{
				return new LocalizedString("ErrorInvalidRequest", "ExC846B3", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x060064F9 RID: 25849 RVA: 0x0013F85D File Offset: 0x0013DA5D
		public static LocalizedString ErrorSubscriptionUnsubscribed
		{
			get
			{
				return new LocalizedString("ErrorSubscriptionUnsubscribed", "Ex58FF9D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x060064FA RID: 25850 RVA: 0x0013F87B File Offset: 0x0013DA7B
		public static LocalizedString ErrorInvalidItemForOperationCancelItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidItemForOperationCancelItem", "ExE44FBE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x060064FB RID: 25851 RVA: 0x0013F899 File Offset: 0x0013DA99
		public static LocalizedString IrmCorruptProtectedMessage
		{
			get
			{
				return new LocalizedString("IrmCorruptProtectedMessage", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x060064FC RID: 25852 RVA: 0x0013F8B7 File Offset: 0x0013DAB7
		public static LocalizedString ErrorCalendarIsGroupMailboxForAccept
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsGroupMailboxForAccept", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060064FD RID: 25853 RVA: 0x0013F8D8 File Offset: 0x0013DAD8
		public static LocalizedString ErrorPropertyNotSupportCreate(string property)
		{
			return new LocalizedString("ErrorPropertyNotSupportCreate", "", false, false, CoreResources.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x060064FE RID: 25854 RVA: 0x0013F907 File Offset: 0x0013DB07
		public static LocalizedString ErrorMailboxSearchFailed
		{
			get
			{
				return new LocalizedString("ErrorMailboxSearchFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x060064FF RID: 25855 RVA: 0x0013F925 File Offset: 0x0013DB25
		public static LocalizedString ErrorMailboxConfiguration
		{
			get
			{
				return new LocalizedString("ErrorMailboxConfiguration", "ExAA65D5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x06006500 RID: 25856 RVA: 0x0013F943 File Offset: 0x0013DB43
		public static LocalizedString RuleErrorNotSettable
		{
			get
			{
				return new LocalizedString("RuleErrorNotSettable", "Ex977AE9", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x06006501 RID: 25857 RVA: 0x0013F961 File Offset: 0x0013DB61
		public static LocalizedString ErrorCopyPublicFolderNotSupported
		{
			get
			{
				return new LocalizedString("ErrorCopyPublicFolderNotSupported", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x06006502 RID: 25858 RVA: 0x0013F97F File Offset: 0x0013DB7F
		public static LocalizedString ErrorInvalidWatermark
		{
			get
			{
				return new LocalizedString("ErrorInvalidWatermark", "Ex4803E3", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x06006503 RID: 25859 RVA: 0x0013F99D File Offset: 0x0013DB9D
		public static LocalizedString ErrorActingAsUserNotFound
		{
			get
			{
				return new LocalizedString("ErrorActingAsUserNotFound", "ExC5B2FD", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x06006504 RID: 25860 RVA: 0x0013F9BB File Offset: 0x0013DBBB
		public static LocalizedString ErrorDelegateMissingConfiguration
		{
			get
			{
				return new LocalizedString("ErrorDelegateMissingConfiguration", "Ex065950", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x06006505 RID: 25861 RVA: 0x0013F9D9 File Offset: 0x0013DBD9
		public static LocalizedString MessageCalendarUnableToUpdateAssociatedCalendarItem
		{
			get
			{
				return new LocalizedString("MessageCalendarUnableToUpdateAssociatedCalendarItem", "Ex675015", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x06006506 RID: 25862 RVA: 0x0013F9F7 File Offset: 0x0013DBF7
		public static LocalizedString MessageMissingMailboxOwnerEmailAddress
		{
			get
			{
				return new LocalizedString("MessageMissingMailboxOwnerEmailAddress", "Ex9E01C1", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x06006507 RID: 25863 RVA: 0x0013FA15 File Offset: 0x0013DC15
		public static LocalizedString ErrorSentMeetingRequestUpdate
		{
			get
			{
				return new LocalizedString("ErrorSentMeetingRequestUpdate", "Ex71A9BA", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x06006508 RID: 25864 RVA: 0x0013FA33 File Offset: 0x0013DC33
		public static LocalizedString descInvalidTimeZone
		{
			get
			{
				return new LocalizedString("descInvalidTimeZone", "ExBADDEF", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x06006509 RID: 25865 RVA: 0x0013FA51 File Offset: 0x0013DC51
		public static LocalizedString ErrorInvalidOperationDisposalTypeAssociatedItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationDisposalTypeAssociatedItem", "Ex3BE185", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x0600650A RID: 25866 RVA: 0x0013FA6F File Offset: 0x0013DC6F
		public static LocalizedString UpdateFavoritesMoveTypeMustBeSet
		{
			get
			{
				return new LocalizedString("UpdateFavoritesMoveTypeMustBeSet", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x0600650B RID: 25867 RVA: 0x0013FA8D File Offset: 0x0013DC8D
		public static LocalizedString ConversationActionNeedDeleteTypeForSetDeleteAction
		{
			get
			{
				return new LocalizedString("ConversationActionNeedDeleteTypeForSetDeleteAction", "Ex4822A0", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x0600650C RID: 25868 RVA: 0x0013FAAB File Offset: 0x0013DCAB
		public static LocalizedString ErrorInvalidProxySecurityContext
		{
			get
			{
				return new LocalizedString("ErrorInvalidProxySecurityContext", "Ex6DFD0A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x0600650D RID: 25869 RVA: 0x0013FAC9 File Offset: 0x0013DCC9
		public static LocalizedString ErrorInvalidValueForProperty
		{
			get
			{
				return new LocalizedString("ErrorInvalidValueForProperty", "Ex0E5A87", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x0600650E RID: 25870 RVA: 0x0013FAE7 File Offset: 0x0013DCE7
		public static LocalizedString ErrorInvalidRestriction
		{
			get
			{
				return new LocalizedString("ErrorInvalidRestriction", "Ex377BAE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x0600650F RID: 25871 RVA: 0x0013FB05 File Offset: 0x0013DD05
		public static LocalizedString RuleErrorInvalidAddress
		{
			get
			{
				return new LocalizedString("RuleErrorInvalidAddress", "ExF432C6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x06006510 RID: 25872 RVA: 0x0013FB23 File Offset: 0x0013DD23
		public static LocalizedString RuleErrorSizeLessThanZero
		{
			get
			{
				return new LocalizedString("RuleErrorSizeLessThanZero", "ExB93225", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x06006511 RID: 25873 RVA: 0x0013FB41 File Offset: 0x0013DD41
		public static LocalizedString Orange
		{
			get
			{
				return new LocalizedString("Orange", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x06006512 RID: 25874 RVA: 0x0013FB5F File Offset: 0x0013DD5F
		public static LocalizedString ErrorRecipientTypeNotSupported
		{
			get
			{
				return new LocalizedString("ErrorRecipientTypeNotSupported", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x06006513 RID: 25875 RVA: 0x0013FB7D File Offset: 0x0013DD7D
		public static LocalizedString ErrorInvalidIdTooManyAttachmentLevels
		{
			get
			{
				return new LocalizedString("ErrorInvalidIdTooManyAttachmentLevels", "Ex9944CB", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x06006514 RID: 25876 RVA: 0x0013FB9B File Offset: 0x0013DD9B
		public static LocalizedString ErrorExportRemoteArchiveItemsFailed
		{
			get
			{
				return new LocalizedString("ErrorExportRemoteArchiveItemsFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x06006515 RID: 25877 RVA: 0x0013FBB9 File Offset: 0x0013DDB9
		public static LocalizedString ErrorCannotSendMessageFromPublicFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotSendMessageFromPublicFolder", "Ex4E013A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x06006516 RID: 25878 RVA: 0x0013FBD7 File Offset: 0x0013DDD7
		public static LocalizedString MessageInsufficientPermissions
		{
			get
			{
				return new LocalizedString("MessageInsufficientPermissions", "Ex54669E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x06006517 RID: 25879 RVA: 0x0013FBF5 File Offset: 0x0013DDF5
		public static LocalizedString MessageCorrelationFailed
		{
			get
			{
				return new LocalizedString("MessageCorrelationFailed", "ExED4639", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x06006518 RID: 25880 RVA: 0x0013FC13 File Offset: 0x0013DE13
		public static LocalizedString ErrorNoMailboxSpecifiedForHoldOperation
		{
			get
			{
				return new LocalizedString("ErrorNoMailboxSpecifiedForHoldOperation", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x06006519 RID: 25881 RVA: 0x0013FC31 File Offset: 0x0013DE31
		public static LocalizedString ErrorTimeZone
		{
			get
			{
				return new LocalizedString("ErrorTimeZone", "Ex0F53B2", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x0600651A RID: 25882 RVA: 0x0013FC4F File Offset: 0x0013DE4F
		public static LocalizedString ErrorSendAsDenied
		{
			get
			{
				return new LocalizedString("ErrorSendAsDenied", "ExC8411C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600651B RID: 25883 RVA: 0x0013FC70 File Offset: 0x0013DE70
		public static LocalizedString MultifactorRegistrationUnavailable(string appId)
		{
			return new LocalizedString("MultifactorRegistrationUnavailable", "", false, false, CoreResources.ResourceManager, new object[]
			{
				appId
			});
		}

		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x0600651C RID: 25884 RVA: 0x0013FC9F File Offset: 0x0013DE9F
		public static LocalizedString MessageSingleOrRecurringCalendarItemExpected
		{
			get
			{
				return new LocalizedString("MessageSingleOrRecurringCalendarItemExpected", "Ex95B08F", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x0600651D RID: 25885 RVA: 0x0013FCBD File Offset: 0x0013DEBD
		public static LocalizedString ErrorSearchQueryCannotBeEmpty
		{
			get
			{
				return new LocalizedString("ErrorSearchQueryCannotBeEmpty", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700178F RID: 6031
		// (get) Token: 0x0600651E RID: 25886 RVA: 0x0013FCDB File Offset: 0x0013DEDB
		public static LocalizedString ErrorMultipleMailboxesCurrentlyNotSupported
		{
			get
			{
				return new LocalizedString("ErrorMultipleMailboxesCurrentlyNotSupported", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x0600651F RID: 25887 RVA: 0x0013FCF9 File Offset: 0x0013DEF9
		public static LocalizedString ErrorParentFolderNotFound
		{
			get
			{
				return new LocalizedString("ErrorParentFolderNotFound", "ExD04F86", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x06006520 RID: 25888 RVA: 0x0013FD17 File Offset: 0x0013DF17
		public static LocalizedString ErrorDelegateCannotAddOwner
		{
			get
			{
				return new LocalizedString("ErrorDelegateCannotAddOwner", "ExB2DEF5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x06006521 RID: 25889 RVA: 0x0013FD35 File Offset: 0x0013DF35
		public static LocalizedString MessageCalendarInsufficientPermissionsToMoveMeetingCancellation
		{
			get
			{
				return new LocalizedString("MessageCalendarInsufficientPermissionsToMoveMeetingCancellation", "ExC83B53", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x06006522 RID: 25890 RVA: 0x0013FD53 File Offset: 0x0013DF53
		public static LocalizedString ErrorImpersonateUserDenied
		{
			get
			{
				return new LocalizedString("ErrorImpersonateUserDenied", "ExD323C0", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x06006523 RID: 25891 RVA: 0x0013FD71 File Offset: 0x0013DF71
		public static LocalizedString ErrorReadReceiptNotPending
		{
			get
			{
				return new LocalizedString("ErrorReadReceiptNotPending", "Ex2490C5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x06006524 RID: 25892 RVA: 0x0013FD8F File Offset: 0x0013DF8F
		public static LocalizedString ErrorInvalidRetentionTagIdGuid
		{
			get
			{
				return new LocalizedString("ErrorInvalidRetentionTagIdGuid", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x06006525 RID: 25893 RVA: 0x0013FDAD File Offset: 0x0013DFAD
		public static LocalizedString ErrorCannotCreateTaskInNonTaskFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotCreateTaskInNonTaskFolder", "ExB741C4", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x06006526 RID: 25894 RVA: 0x0013FDCB File Offset: 0x0013DFCB
		public static LocalizedString MessageNonExistentMailboxNoSmtpAddress
		{
			get
			{
				return new LocalizedString("MessageNonExistentMailboxNoSmtpAddress", "ExDE06C3", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x06006527 RID: 25895 RVA: 0x0013FDE9 File Offset: 0x0013DFE9
		public static LocalizedString ErrorSchemaValidation
		{
			get
			{
				return new LocalizedString("ErrorSchemaValidation", "Ex98A4F8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x06006528 RID: 25896 RVA: 0x0013FE07 File Offset: 0x0013E007
		public static LocalizedString MessageManagementRoleHeaderValueNotApplicable
		{
			get
			{
				return new LocalizedString("MessageManagementRoleHeaderValueNotApplicable", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x06006529 RID: 25897 RVA: 0x0013FE25 File Offset: 0x0013E025
		public static LocalizedString MessageInvalidRuleVersion
		{
			get
			{
				return new LocalizedString("MessageInvalidRuleVersion", "ExCE2C4A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x0600652A RID: 25898 RVA: 0x0013FE43 File Offset: 0x0013E043
		public static LocalizedString ErrorUnsupportedMimeConversion
		{
			get
			{
				return new LocalizedString("ErrorUnsupportedMimeConversion", "Ex4A3BBA", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x0600652B RID: 25899 RVA: 0x0013FE61 File Offset: 0x0013E061
		public static LocalizedString ErrorCannotMovePublicFolderItemOnDelete
		{
			get
			{
				return new LocalizedString("ErrorCannotMovePublicFolderItemOnDelete", "Ex2C5752", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x0600652C RID: 25900 RVA: 0x0013FE7F File Offset: 0x0013E07F
		public static LocalizedString ErrorInvalidItemForOperationArchiveItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidItemForOperationArchiveItem", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x0600652D RID: 25901 RVA: 0x0013FE9D File Offset: 0x0013E09D
		public static LocalizedString ErrorInvalidSearchQuerySyntax
		{
			get
			{
				return new LocalizedString("ErrorInvalidSearchQuerySyntax", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x0600652E RID: 25902 RVA: 0x0013FEBB File Offset: 0x0013E0BB
		public static LocalizedString ErrorInvalidValueForCountSystemQueryOption
		{
			get
			{
				return new LocalizedString("ErrorInvalidValueForCountSystemQueryOption", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x0600652F RID: 25903 RVA: 0x0013FED9 File Offset: 0x0013E0D9
		public static LocalizedString ErrorFolderSaveFailed
		{
			get
			{
				return new LocalizedString("ErrorFolderSaveFailed", "Ex7238DA", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006530 RID: 25904 RVA: 0x0013FEF8 File Offset: 0x0013E0F8
		public static LocalizedString RuleErrorStringValueTooBig(int limit)
		{
			return new LocalizedString("RuleErrorStringValueTooBig", "ExA45ABF", false, true, CoreResources.ResourceManager, new object[]
			{
				limit
			});
		}

		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x06006531 RID: 25905 RVA: 0x0013FF2C File Offset: 0x0013E12C
		public static LocalizedString MessageTargetMailboxNotInRoleScope
		{
			get
			{
				return new LocalizedString("MessageTargetMailboxNotInRoleScope", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x06006532 RID: 25906 RVA: 0x0013FF4A File Offset: 0x0013E14A
		public static LocalizedString ErrorInvalidSearchId
		{
			get
			{
				return new LocalizedString("ErrorInvalidSearchId", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x06006533 RID: 25907 RVA: 0x0013FF68 File Offset: 0x0013E168
		public static LocalizedString ErrorInvalidOperationSyncFolderHierarchyForPublicFolder
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationSyncFolderHierarchyForPublicFolder", "ExA79391", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006534 RID: 25908 RVA: 0x0013FF88 File Offset: 0x0013E188
		public static LocalizedString ErrorInternalServerErrorFaultInjection(string errorcode, string soapaction)
		{
			return new LocalizedString("ErrorInternalServerErrorFaultInjection", "", false, false, CoreResources.ResourceManager, new object[]
			{
				errorcode,
				soapaction
			});
		}

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x06006535 RID: 25909 RVA: 0x0013FFBB File Offset: 0x0013E1BB
		public static LocalizedString ErrorItemCorrupt
		{
			get
			{
				return new LocalizedString("ErrorItemCorrupt", "Ex6F566D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x06006536 RID: 25910 RVA: 0x0013FFD9 File Offset: 0x0013E1D9
		public static LocalizedString ErrorServerTemporaryUnavailable
		{
			get
			{
				return new LocalizedString("ErrorServerTemporaryUnavailable", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x06006537 RID: 25911 RVA: 0x0013FFF7 File Offset: 0x0013E1F7
		public static LocalizedString ErrorCannotArchiveCalendarContactTaskFolderException
		{
			get
			{
				return new LocalizedString("ErrorCannotArchiveCalendarContactTaskFolderException", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x00140018 File Offset: 0x0013E218
		public static LocalizedString ErrorReturnTooManyMailboxesFromDG(string dgName, int maxMailboxesToReturn)
		{
			return new LocalizedString("ErrorReturnTooManyMailboxesFromDG", "", false, false, CoreResources.ResourceManager, new object[]
			{
				dgName,
				maxMailboxesToReturn
			});
		}

		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x06006539 RID: 25913 RVA: 0x00140050 File Offset: 0x0013E250
		public static LocalizedString ErrorInvalidItemForOperationSendItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidItemForOperationSendItem", "ExCD2AED", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x0600653A RID: 25914 RVA: 0x0014006E File Offset: 0x0013E26E
		public static LocalizedString ErrorAggregatedAccountAlreadyExists
		{
			get
			{
				return new LocalizedString("ErrorAggregatedAccountAlreadyExists", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x0600653B RID: 25915 RVA: 0x0014008C File Offset: 0x0013E28C
		public static LocalizedString ErrorInvalidServerVersion
		{
			get
			{
				return new LocalizedString("ErrorInvalidServerVersion", "ExFF89AD", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x0600653C RID: 25916 RVA: 0x001400AA File Offset: 0x0013E2AA
		public static LocalizedString ErrorGroupingNonNullWithSuggestionsViewFilter
		{
			get
			{
				return new LocalizedString("ErrorGroupingNonNullWithSuggestionsViewFilter", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x0600653D RID: 25917 RVA: 0x001400C8 File Offset: 0x0013E2C8
		public static LocalizedString MessageInvalidMailboxNotPrivateDL
		{
			get
			{
				return new LocalizedString("MessageInvalidMailboxNotPrivateDL", "Ex56C8F2", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x0600653E RID: 25918 RVA: 0x001400E6 File Offset: 0x0013E2E6
		public static LocalizedString ErrorItemPropertyRequestFailed
		{
			get
			{
				return new LocalizedString("ErrorItemPropertyRequestFailed", "Ex205ED8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x0600653F RID: 25919 RVA: 0x00140104 File Offset: 0x0013E304
		public static LocalizedString ConversationActionNeedDestinationFolderForCopyAction
		{
			get
			{
				return new LocalizedString("ConversationActionNeedDestinationFolderForCopyAction", "Ex4E6A21", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x06006540 RID: 25920 RVA: 0x00140122 File Offset: 0x0013E322
		public static LocalizedString ErrorLocationServicesRequestFailed
		{
			get
			{
				return new LocalizedString("ErrorLocationServicesRequestFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x06006541 RID: 25921 RVA: 0x00140140 File Offset: 0x0013E340
		public static LocalizedString UnrecognizedDistinguishedFolderName
		{
			get
			{
				return new LocalizedString("UnrecognizedDistinguishedFolderName", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x06006542 RID: 25922 RVA: 0x0014015E File Offset: 0x0013E35E
		public static LocalizedString ErrorInvalidSubfilterTypeNotRecipientType
		{
			get
			{
				return new LocalizedString("ErrorInvalidSubfilterTypeNotRecipientType", "ExD18E5C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x06006543 RID: 25923 RVA: 0x0014017C File Offset: 0x0013E37C
		public static LocalizedString ErrorInvalidPropertySet
		{
			get
			{
				return new LocalizedString("ErrorInvalidPropertySet", "ExE5C0CC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x06006544 RID: 25924 RVA: 0x0014019A File Offset: 0x0013E39A
		public static LocalizedString UpdateFavoritesFolderCannotBeNull
		{
			get
			{
				return new LocalizedString("UpdateFavoritesFolderCannotBeNull", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x06006545 RID: 25925 RVA: 0x001401B8 File Offset: 0x0013E3B8
		public static LocalizedString ErrorCannotRemoveAggregatedAccountFromList
		{
			get
			{
				return new LocalizedString("ErrorCannotRemoveAggregatedAccountFromList", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x06006546 RID: 25926 RVA: 0x001401D6 File Offset: 0x0013E3D6
		public static LocalizedString ErrorProxyTokenExpired
		{
			get
			{
				return new LocalizedString("ErrorProxyTokenExpired", "Ex252BFB", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017B5 RID: 6069
		// (get) Token: 0x06006547 RID: 25927 RVA: 0x001401F4 File Offset: 0x0013E3F4
		public static LocalizedString ErrorCannotCreateCalendarItemInNonCalendarFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotCreateCalendarItemInNonCalendarFolder", "Ex7797D0", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017B6 RID: 6070
		// (get) Token: 0x06006548 RID: 25928 RVA: 0x00140212 File Offset: 0x0013E412
		public static LocalizedString ErrorInvalidOperationGroupByAssociatedItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationGroupByAssociatedItem", "ExDB608C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x06006549 RID: 25929 RVA: 0x00140230 File Offset: 0x0013E430
		public static LocalizedString MessageCalendarUnableToCreateAssociatedCalendarItem
		{
			get
			{
				return new LocalizedString("MessageCalendarUnableToCreateAssociatedCalendarItem", "Ex6A674D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x0600654A RID: 25930 RVA: 0x0014024E File Offset: 0x0013E44E
		public static LocalizedString ErrorMultiLegacyMailboxAccess
		{
			get
			{
				return new LocalizedString("ErrorMultiLegacyMailboxAccess", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x0600654B RID: 25931 RVA: 0x0014026C File Offset: 0x0013E46C
		public static LocalizedString ErrorUnifiedMailboxAlreadyExists
		{
			get
			{
				return new LocalizedString("ErrorUnifiedMailboxAlreadyExists", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x0600654C RID: 25932 RVA: 0x0014028A File Offset: 0x0013E48A
		public static LocalizedString ErrorInvalidPropertyAppend
		{
			get
			{
				return new LocalizedString("ErrorInvalidPropertyAppend", "Ex0102FB", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x0600654D RID: 25933 RVA: 0x001402A8 File Offset: 0x0013E4A8
		public static LocalizedString ErrorObjectTypeChanged
		{
			get
			{
				return new LocalizedString("ErrorObjectTypeChanged", "ExBF8119", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x0600654E RID: 25934 RVA: 0x001402C6 File Offset: 0x0013E4C6
		public static LocalizedString ErrorSearchableObjectNotFound
		{
			get
			{
				return new LocalizedString("ErrorSearchableObjectNotFound", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x0600654F RID: 25935 RVA: 0x001402E4 File Offset: 0x0013E4E4
		public static LocalizedString ErrorEndTimeMustBeGreaterThanStartTime
		{
			get
			{
				return new LocalizedString("ErrorEndTimeMustBeGreaterThanStartTime", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x06006550 RID: 25936 RVA: 0x00140302 File Offset: 0x0013E502
		public static LocalizedString ErrorInvalidFederatedOrganizationId
		{
			get
			{
				return new LocalizedString("ErrorInvalidFederatedOrganizationId", "Ex25CFAE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x06006551 RID: 25937 RVA: 0x00140320 File Offset: 0x0013E520
		public static LocalizedString MessageExtensionNotAllowedToUpdateFAI
		{
			get
			{
				return new LocalizedString("MessageExtensionNotAllowedToUpdateFAI", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x06006552 RID: 25938 RVA: 0x0014033E File Offset: 0x0013E53E
		public static LocalizedString ErrorValueOutOfRange
		{
			get
			{
				return new LocalizedString("ErrorValueOutOfRange", "ExBC5EF4", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x06006553 RID: 25939 RVA: 0x0014035C File Offset: 0x0013E55C
		public static LocalizedString ErrorNotEnoughMemory
		{
			get
			{
				return new LocalizedString("ErrorNotEnoughMemory", "Ex0BDAD7", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x06006554 RID: 25940 RVA: 0x0014037A File Offset: 0x0013E57A
		public static LocalizedString ErrorInvalidExtendedPropertyValue
		{
			get
			{
				return new LocalizedString("ErrorInvalidExtendedPropertyValue", "ExB0CA9C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x06006555 RID: 25941 RVA: 0x00140398 File Offset: 0x0013E598
		public static LocalizedString ErrorMoveCopyFailed
		{
			get
			{
				return new LocalizedString("ErrorMoveCopyFailed", "Ex76C12C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x06006556 RID: 25942 RVA: 0x001403B6 File Offset: 0x0013E5B6
		public static LocalizedString GetClientExtensionTokenFailed
		{
			get
			{
				return new LocalizedString("GetClientExtensionTokenFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x06006557 RID: 25943 RVA: 0x001403D4 File Offset: 0x0013E5D4
		public static LocalizedString ErrorVirusDetected
		{
			get
			{
				return new LocalizedString("ErrorVirusDetected", "ExB375DC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x06006558 RID: 25944 RVA: 0x001403F2 File Offset: 0x0013E5F2
		public static LocalizedString ErrorInvalidVotingResponse
		{
			get
			{
				return new LocalizedString("ErrorInvalidVotingResponse", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x06006559 RID: 25945 RVA: 0x00140410 File Offset: 0x0013E610
		public static LocalizedString RuleErrorInboxRulesValidationError
		{
			get
			{
				return new LocalizedString("RuleErrorInboxRulesValidationError", "ExFE2837", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x0600655A RID: 25946 RVA: 0x0014042E File Offset: 0x0013E62E
		public static LocalizedString ErrorInvalidIdMonikerTooLong
		{
			get
			{
				return new LocalizedString("ErrorInvalidIdMonikerTooLong", "Ex5391B9", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600655B RID: 25947 RVA: 0x0014044C File Offset: 0x0013E64C
		public static LocalizedString ActionQueueDeserializationError(string key, string value, string typeName, string message)
		{
			return new LocalizedString("ActionQueueDeserializationError", "", false, false, CoreResources.ResourceManager, new object[]
			{
				key,
				value,
				typeName,
				message
			});
		}

		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x0600655C RID: 25948 RVA: 0x00140487 File Offset: 0x0013E687
		public static LocalizedString ErrorMultipleSearchRootsDisallowedWithSearchContext
		{
			get
			{
				return new LocalizedString("ErrorMultipleSearchRootsDisallowedWithSearchContext", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x0600655D RID: 25949 RVA: 0x001404A5 File Offset: 0x0013E6A5
		public static LocalizedString ErrorUserNotUnifiedMessagingEnabled
		{
			get
			{
				return new LocalizedString("ErrorUserNotUnifiedMessagingEnabled", "Ex9ADB0A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x0600655E RID: 25950 RVA: 0x001404C3 File Offset: 0x0013E6C3
		public static LocalizedString ErrorCannotMovePublicFolderToPrivateMailbox
		{
			get
			{
				return new LocalizedString("ErrorCannotMovePublicFolderToPrivateMailbox", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x0600655F RID: 25951 RVA: 0x001404E1 File Offset: 0x0013E6E1
		public static LocalizedString ConversationActionAlwaysMoveNoPublicFolder
		{
			get
			{
				return new LocalizedString("ConversationActionAlwaysMoveNoPublicFolder", "ExFD860E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x06006560 RID: 25952 RVA: 0x001404FF File Offset: 0x0013E6FF
		public static LocalizedString ErrorCallerIsInvalidADAccount
		{
			get
			{
				return new LocalizedString("ErrorCallerIsInvalidADAccount", "ExCB60F2", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x06006561 RID: 25953 RVA: 0x0014051D File Offset: 0x0013E71D
		public static LocalizedString ErrorNoDestinationCASDueToSSLRequirements
		{
			get
			{
				return new LocalizedString("ErrorNoDestinationCASDueToSSLRequirements", "Ex73A10A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x06006562 RID: 25954 RVA: 0x0014053B File Offset: 0x0013E73B
		public static LocalizedString ErrorInternalServerTransientError
		{
			get
			{
				return new LocalizedString("ErrorInternalServerTransientError", "Ex92A2FF", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x06006563 RID: 25955 RVA: 0x00140559 File Offset: 0x0013E759
		public static LocalizedString ErrorInvalidParentFolder
		{
			get
			{
				return new LocalizedString("ErrorInvalidParentFolder", "Ex329760", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x06006564 RID: 25956 RVA: 0x00140577 File Offset: 0x0013E777
		public static LocalizedString ErrorArchiveFolderPathCreation
		{
			get
			{
				return new LocalizedString("ErrorArchiveFolderPathCreation", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017D2 RID: 6098
		// (get) Token: 0x06006565 RID: 25957 RVA: 0x00140595 File Offset: 0x0013E795
		public static LocalizedString MessageCalendarInsufficientPermissionsToMoveItem
		{
			get
			{
				return new LocalizedString("MessageCalendarInsufficientPermissionsToMoveItem", "Ex8FB7D7", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x06006566 RID: 25958 RVA: 0x001405B3 File Offset: 0x0013E7B3
		public static LocalizedString ErrorMessagePerFolderCountReceiveQuotaExceeded
		{
			get
			{
				return new LocalizedString("ErrorMessagePerFolderCountReceiveQuotaExceeded", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017D4 RID: 6100
		// (get) Token: 0x06006567 RID: 25959 RVA: 0x001405D1 File Offset: 0x0013E7D1
		public static LocalizedString ErrorDateTimeNotInUTC
		{
			get
			{
				return new LocalizedString("ErrorDateTimeNotInUTC", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017D5 RID: 6101
		// (get) Token: 0x06006568 RID: 25960 RVA: 0x001405EF File Offset: 0x0013E7EF
		public static LocalizedString ErrorInvalidAttachmentSubfilter
		{
			get
			{
				return new LocalizedString("ErrorInvalidAttachmentSubfilter", "Ex41988F", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006569 RID: 25961 RVA: 0x00140610 File Offset: 0x0013E810
		public static LocalizedString ErrorMandatoryPropertyMissing(string property)
		{
			return new LocalizedString("ErrorMandatoryPropertyMissing", "", false, false, CoreResources.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x0600656A RID: 25962 RVA: 0x00140640 File Offset: 0x0013E840
		public static LocalizedString GetTenantFailed(string name, string error)
		{
			return new LocalizedString("GetTenantFailed", "", false, false, CoreResources.ResourceManager, new object[]
			{
				name,
				error
			});
		}

		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x0600656B RID: 25963 RVA: 0x00140673 File Offset: 0x0013E873
		public static LocalizedString ErrorUserConfigurationDictionaryNotExist
		{
			get
			{
				return new LocalizedString("ErrorUserConfigurationDictionaryNotExist", "Ex48A876", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x0600656C RID: 25964 RVA: 0x00140691 File Offset: 0x0013E891
		public static LocalizedString FromColon
		{
			get
			{
				return new LocalizedString("FromColon", "Ex67804C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x0600656D RID: 25965 RVA: 0x001406AF File Offset: 0x0013E8AF
		public static LocalizedString ErrorInvalidSubscriptionRequestNoFolderIds
		{
			get
			{
				return new LocalizedString("ErrorInvalidSubscriptionRequestNoFolderIds", "ExE4CE3B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x0600656E RID: 25966 RVA: 0x001406CD File Offset: 0x0013E8CD
		public static LocalizedString ErrorCallerIsComputerAccount
		{
			get
			{
				return new LocalizedString("ErrorCallerIsComputerAccount", "Ex3123DF", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600656F RID: 25967 RVA: 0x001406EC File Offset: 0x0013E8EC
		public static LocalizedString GetGroupMailboxFailed(string name, string error)
		{
			return new LocalizedString("GetGroupMailboxFailed", "", false, false, CoreResources.ResourceManager, new object[]
			{
				name,
				error
			});
		}

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x06006570 RID: 25968 RVA: 0x0014071F File Offset: 0x0013E91F
		public static LocalizedString ErrorDeleteItemsFailed
		{
			get
			{
				return new LocalizedString("ErrorDeleteItemsFailed", "Ex22718B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x06006571 RID: 25969 RVA: 0x0014073D File Offset: 0x0013E93D
		public static LocalizedString ErrorNotApplicableOutsideOfDatacenter
		{
			get
			{
				return new LocalizedString("ErrorNotApplicableOutsideOfDatacenter", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x06006572 RID: 25970 RVA: 0x0014075B File Offset: 0x0013E95B
		public static LocalizedString RuleErrorOutlookRuleBlobExists
		{
			get
			{
				return new LocalizedString("RuleErrorOutlookRuleBlobExists", "ExECF55C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x06006573 RID: 25971 RVA: 0x00140779 File Offset: 0x0013E979
		public static LocalizedString descInvalidOofRequestPublicFolder
		{
			get
			{
				return new LocalizedString("descInvalidOofRequestPublicFolder", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006574 RID: 25972 RVA: 0x00140798 File Offset: 0x0013E998
		public static LocalizedString ErrorInvalidUrlQuery(string value)
		{
			return new LocalizedString("ErrorInvalidUrlQuery", "", false, false, CoreResources.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x06006575 RID: 25973 RVA: 0x001407C7 File Offset: 0x0013E9C7
		public static LocalizedString ErrorMailboxIsNotPartOfAggregatedMailboxes
		{
			get
			{
				return new LocalizedString("ErrorMailboxIsNotPartOfAggregatedMailboxes", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x06006576 RID: 25974 RVA: 0x001407E5 File Offset: 0x0013E9E5
		public static LocalizedString ErrorInvalidRetentionTagNone
		{
			get
			{
				return new LocalizedString("ErrorInvalidRetentionTagNone", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x06006577 RID: 25975 RVA: 0x00140803 File Offset: 0x0013EA03
		public static LocalizedString MessageInvalidRoleTypeString
		{
			get
			{
				return new LocalizedString("MessageInvalidRoleTypeString", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x06006578 RID: 25976 RVA: 0x00140821 File Offset: 0x0013EA21
		public static LocalizedString MessageInvalidMailboxRecipientNotFoundInActiveDirectory
		{
			get
			{
				return new LocalizedString("MessageInvalidMailboxRecipientNotFoundInActiveDirectory", "Ex38F07E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x06006579 RID: 25977 RVA: 0x0014083F File Offset: 0x0013EA3F
		public static LocalizedString ErrorNoSyncRequestsMatchedSpecifiedEmailAddress
		{
			get
			{
				return new LocalizedString("ErrorNoSyncRequestsMatchedSpecifiedEmailAddress", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x0600657A RID: 25978 RVA: 0x0014085D File Offset: 0x0013EA5D
		public static LocalizedString ErrorInvalidDestinationFolderForPostItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidDestinationFolderForPostItem", "Ex3ABA0D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x0600657B RID: 25979 RVA: 0x0014087B File Offset: 0x0013EA7B
		public static LocalizedString ErrorGetRemoteArchiveFolderFailed
		{
			get
			{
				return new LocalizedString("ErrorGetRemoteArchiveFolderFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x0600657C RID: 25980 RVA: 0x00140899 File Offset: 0x0013EA99
		public static LocalizedString RightsManagementMailboxOnlySupport
		{
			get
			{
				return new LocalizedString("RightsManagementMailboxOnlySupport", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x0600657D RID: 25981 RVA: 0x001408B7 File Offset: 0x0013EAB7
		public static LocalizedString ErrorMissingItemForCreateItemAttachment
		{
			get
			{
				return new LocalizedString("ErrorMissingItemForCreateItemAttachment", "Ex271EC4", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x0600657E RID: 25982 RVA: 0x001408D5 File Offset: 0x0013EAD5
		public static LocalizedString ErrorFindRemoteArchiveFolderFailed
		{
			get
			{
				return new LocalizedString("ErrorFindRemoteArchiveFolderFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x0600657F RID: 25983 RVA: 0x001408F3 File Offset: 0x0013EAF3
		public static LocalizedString ErrorCalendarFolderIsInvalidForCalendarView
		{
			get
			{
				return new LocalizedString("ErrorCalendarFolderIsInvalidForCalendarView", "Ex90EC10", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x06006580 RID: 25984 RVA: 0x00140911 File Offset: 0x0013EB11
		public static LocalizedString ErrorFindConversationNotSupportedForPublicFolders
		{
			get
			{
				return new LocalizedString("ErrorFindConversationNotSupportedForPublicFolders", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x06006581 RID: 25985 RVA: 0x0014092F File Offset: 0x0013EB2F
		public static LocalizedString ErrorUserConfigurationBinaryDataNotExist
		{
			get
			{
				return new LocalizedString("ErrorUserConfigurationBinaryDataNotExist", "ExDC22B0", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x06006582 RID: 25986 RVA: 0x0014094D File Offset: 0x0013EB4D
		public static LocalizedString DefaultHtmlAttachmentHrefText
		{
			get
			{
				return new LocalizedString("DefaultHtmlAttachmentHrefText", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x06006583 RID: 25987 RVA: 0x0014096B File Offset: 0x0013EB6B
		public static LocalizedString Green
		{
			get
			{
				return new LocalizedString("Green", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x06006584 RID: 25988 RVA: 0x00140989 File Offset: 0x0013EB89
		public static LocalizedString ErrorItemNotFound
		{
			get
			{
				return new LocalizedString("ErrorItemNotFound", "ExB31B07", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006585 RID: 25989 RVA: 0x001409A8 File Offset: 0x0013EBA8
		public static LocalizedString ErrorCannotLinkMoreThanOneO365AccountToAnMsa(string existingLinkedAccount)
		{
			return new LocalizedString("ErrorCannotLinkMoreThanOneO365AccountToAnMsa", "", false, false, CoreResources.ResourceManager, new object[]
			{
				existingLinkedAccount
			});
		}

		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x06006586 RID: 25990 RVA: 0x001409D7 File Offset: 0x0013EBD7
		public static LocalizedString ErrorCannotEmptyFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotEmptyFolder", "Ex3AAFA2", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x06006587 RID: 25991 RVA: 0x001409F5 File Offset: 0x0013EBF5
		public static LocalizedString Yellow
		{
			get
			{
				return new LocalizedString("Yellow", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x06006588 RID: 25992 RVA: 0x00140A13 File Offset: 0x0013EC13
		public static LocalizedString ErrorInvalidSubscription
		{
			get
			{
				return new LocalizedString("ErrorInvalidSubscription", "ExF6C668", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x06006589 RID: 25993 RVA: 0x00140A31 File Offset: 0x0013EC31
		public static LocalizedString ErrorSchemaValidationColon
		{
			get
			{
				return new LocalizedString("ErrorSchemaValidationColon", "Ex736F42", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x0600658A RID: 25994 RVA: 0x00140A4F File Offset: 0x0013EC4F
		public static LocalizedString ErrorDelegateNoUser
		{
			get
			{
				return new LocalizedString("ErrorDelegateNoUser", "Ex1430F8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x0600658B RID: 25995 RVA: 0x00140A6D File Offset: 0x0013EC6D
		public static LocalizedString RuleErrorMissingRangeValue
		{
			get
			{
				return new LocalizedString("RuleErrorMissingRangeValue", "Ex84FF39", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x0600658C RID: 25996 RVA: 0x00140A8B File Offset: 0x0013EC8B
		public static LocalizedString MessageWebMethodUnavailable
		{
			get
			{
				return new LocalizedString("MessageWebMethodUnavailable", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x0600658D RID: 25997 RVA: 0x00140AA9 File Offset: 0x0013ECA9
		public static LocalizedString ErrorUnsupportedQueryFilter
		{
			get
			{
				return new LocalizedString("ErrorUnsupportedQueryFilter", "Ex3C1A1A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600658E RID: 25998 RVA: 0x00140AC8 File Offset: 0x0013ECC8
		public static LocalizedString ErrorDuplicateLegacyDistinguishedNameFound(string legDN)
		{
			return new LocalizedString("ErrorDuplicateLegacyDistinguishedNameFound", "", false, false, CoreResources.ResourceManager, new object[]
			{
				legDN
			});
		}

		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x0600658F RID: 25999 RVA: 0x00140AF7 File Offset: 0x0013ECF7
		public static LocalizedString ErrorCannotMovePublicFolderOnDelete
		{
			get
			{
				return new LocalizedString("ErrorCannotMovePublicFolderOnDelete", "Ex60D2B0", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x06006590 RID: 26000 RVA: 0x00140B15 File Offset: 0x0013ED15
		public static LocalizedString ErrorAccessModeSpecified
		{
			get
			{
				return new LocalizedString("ErrorAccessModeSpecified", "ExD27C78", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x06006591 RID: 26001 RVA: 0x00140B33 File Offset: 0x0013ED33
		public static LocalizedString ErrorInvalidPhotoSize
		{
			get
			{
				return new LocalizedString("ErrorInvalidPhotoSize", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x06006592 RID: 26002 RVA: 0x00140B51 File Offset: 0x0013ED51
		public static LocalizedString ErrorMultipleMailboxSearchNotSupported
		{
			get
			{
				return new LocalizedString("ErrorMultipleMailboxSearchNotSupported", "ExA7218B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x06006593 RID: 26003 RVA: 0x00140B6F File Offset: 0x0013ED6F
		public static LocalizedString MessageManagementRoleHeaderNotSupportedForPartnerIdentity
		{
			get
			{
				return new LocalizedString("MessageManagementRoleHeaderNotSupportedForPartnerIdentity", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x06006594 RID: 26004 RVA: 0x00140B8D File Offset: 0x0013ED8D
		public static LocalizedString ConversationActionInvalidFolderType
		{
			get
			{
				return new LocalizedString("ConversationActionInvalidFolderType", "ExF5AE11", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x06006595 RID: 26005 RVA: 0x00140BAB File Offset: 0x0013EDAB
		public static LocalizedString ErrorUnsupportedSubFilter
		{
			get
			{
				return new LocalizedString("ErrorUnsupportedSubFilter", "Ex1EF04B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x06006596 RID: 26006 RVA: 0x00140BC9 File Offset: 0x0013EDC9
		public static LocalizedString ErrorInvalidComplianceId
		{
			get
			{
				return new LocalizedString("ErrorInvalidComplianceId", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x06006597 RID: 26007 RVA: 0x00140BE7 File Offset: 0x0013EDE7
		public static LocalizedString ErrorCalendarCannotUpdateDeletedItem
		{
			get
			{
				return new LocalizedString("ErrorCalendarCannotUpdateDeletedItem", "ExE942E8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x06006598 RID: 26008 RVA: 0x00140C05 File Offset: 0x0013EE05
		public static LocalizedString ErrorInvalidOperationDistinguishedGroupByAssociatedItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationDistinguishedGroupByAssociatedItem", "Ex543CAE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x06006599 RID: 26009 RVA: 0x00140C23 File Offset: 0x0013EE23
		public static LocalizedString ErrorInvalidDelegatePermission
		{
			get
			{
				return new LocalizedString("ErrorInvalidDelegatePermission", "ExE9381E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x0600659A RID: 26010 RVA: 0x00140C41 File Offset: 0x0013EE41
		public static LocalizedString ErrorInternalServerError
		{
			get
			{
				return new LocalizedString("ErrorInternalServerError", "ExE0EC81", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x0600659B RID: 26011 RVA: 0x00140C5F File Offset: 0x0013EE5F
		public static LocalizedString ErrorNoPublicFolderServerAvailable
		{
			get
			{
				return new LocalizedString("ErrorNoPublicFolderServerAvailable", "Ex22D031", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x0600659C RID: 26012 RVA: 0x00140C7D File Offset: 0x0013EE7D
		public static LocalizedString ErrorInvalidPhoneCallId
		{
			get
			{
				return new LocalizedString("ErrorInvalidPhoneCallId", "Ex92D768", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x0600659D RID: 26013 RVA: 0x00140C9B File Offset: 0x0013EE9B
		public static LocalizedString ErrorInvalidGetSharingFolderRequest
		{
			get
			{
				return new LocalizedString("ErrorInvalidGetSharingFolderRequest", "ExB1BEBA", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x0600659E RID: 26014 RVA: 0x00140CB9 File Offset: 0x0013EEB9
		public static LocalizedString ErrorCannotResolveOrganizationName
		{
			get
			{
				return new LocalizedString("ErrorCannotResolveOrganizationName", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x0600659F RID: 26015 RVA: 0x00140CD7 File Offset: 0x0013EED7
		public static LocalizedString ErrorUnsupportedCulture
		{
			get
			{
				return new LocalizedString("ErrorUnsupportedCulture", "ExF0FD16", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x060065A0 RID: 26016 RVA: 0x00140CF5 File Offset: 0x0013EEF5
		public static LocalizedString ErrorInvalidChangeKey
		{
			get
			{
				return new LocalizedString("ErrorInvalidChangeKey", "Ex590F94", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x060065A1 RID: 26017 RVA: 0x00140D13 File Offset: 0x0013EF13
		public static LocalizedString ErrorMimeContentConversionFailed
		{
			get
			{
				return new LocalizedString("ErrorMimeContentConversionFailed", "Ex06F3D8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x060065A2 RID: 26018 RVA: 0x00140D31 File Offset: 0x0013EF31
		public static LocalizedString ErrorResolveNamesOnlyOneContactsFolderAllowed
		{
			get
			{
				return new LocalizedString("ErrorResolveNamesOnlyOneContactsFolderAllowed", "Ex89E325", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x060065A3 RID: 26019 RVA: 0x00140D4F File Offset: 0x0013EF4F
		public static LocalizedString ErrorInvalidSchemaVersionForMailboxVersion
		{
			get
			{
				return new LocalizedString("ErrorInvalidSchemaVersionForMailboxVersion", "ExB35CA2", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x060065A4 RID: 26020 RVA: 0x00140D6D File Offset: 0x0013EF6D
		public static LocalizedString ErrorInvalidRequestQuotaExceeded
		{
			get
			{
				return new LocalizedString("ErrorInvalidRequestQuotaExceeded", "ExC14A12", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x060065A5 RID: 26021 RVA: 0x00140D8B File Offset: 0x0013EF8B
		public static LocalizedString MessageTokenRequestUnauthorized
		{
			get
			{
				return new LocalizedString("MessageTokenRequestUnauthorized", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x060065A6 RID: 26022 RVA: 0x00140DA9 File Offset: 0x0013EFA9
		public static LocalizedString MessageUserRoleNotApplicableForAppOnlyToken
		{
			get
			{
				return new LocalizedString("MessageUserRoleNotApplicableForAppOnlyToken", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x060065A7 RID: 26023 RVA: 0x00140DC7 File Offset: 0x0013EFC7
		public static LocalizedString ErrorInvalidValueForPropertyKeyValueConversion
		{
			get
			{
				return new LocalizedString("ErrorInvalidValueForPropertyKeyValueConversion", "Ex707887", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x060065A8 RID: 26024 RVA: 0x00140DE5 File Offset: 0x0013EFE5
		public static LocalizedString ErrorInvalidRetentionTagInheritance
		{
			get
			{
				return new LocalizedString("ErrorInvalidRetentionTagInheritance", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x060065A9 RID: 26025 RVA: 0x00140E03 File Offset: 0x0013F003
		public static LocalizedString Conversation
		{
			get
			{
				return new LocalizedString("Conversation", "ExC6ED2D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x060065AA RID: 26026 RVA: 0x00140E21 File Offset: 0x0013F021
		public static LocalizedString ErrorCannotCreateUnifiedMailbox
		{
			get
			{
				return new LocalizedString("ErrorCannotCreateUnifiedMailbox", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x060065AB RID: 26027 RVA: 0x00140E3F File Offset: 0x0013F03F
		public static LocalizedString ErrorMailTipsDisabled
		{
			get
			{
				return new LocalizedString("ErrorMailTipsDisabled", "ExDFD164", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x060065AC RID: 26028 RVA: 0x00140E5D File Offset: 0x0013F05D
		public static LocalizedString ErrorMissingItemIdForCreateItemAttachment
		{
			get
			{
				return new LocalizedString("ErrorMissingItemIdForCreateItemAttachment", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x060065AD RID: 26029 RVA: 0x00140E7B File Offset: 0x0013F07B
		public static LocalizedString ErrorInvalidMailbox
		{
			get
			{
				return new LocalizedString("ErrorInvalidMailbox", "ExDBBA0C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x060065AE RID: 26030 RVA: 0x00140E99 File Offset: 0x0013F099
		public static LocalizedString ErrorDelegateValidationFailed
		{
			get
			{
				return new LocalizedString("ErrorDelegateValidationFailed", "ExBB2997", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x060065AF RID: 26031 RVA: 0x00140EB7 File Offset: 0x0013F0B7
		public static LocalizedString ErrorUserPromptNeeded
		{
			get
			{
				return new LocalizedString("ErrorUserPromptNeeded", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x060065B0 RID: 26032 RVA: 0x00140ED5 File Offset: 0x0013F0D5
		public static LocalizedString RuleErrorMissingAction
		{
			get
			{
				return new LocalizedString("RuleErrorMissingAction", "ExC05CF5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x060065B1 RID: 26033 RVA: 0x00140EF3 File Offset: 0x0013F0F3
		public static LocalizedString ErrorApplyConversationActionFailed
		{
			get
			{
				return new LocalizedString("ErrorApplyConversationActionFailed", "ExEE0DC7", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x060065B2 RID: 26034 RVA: 0x00140F11 File Offset: 0x0013F111
		public static LocalizedString ErrorInsufficientResources
		{
			get
			{
				return new LocalizedString("ErrorInsufficientResources", "ExFACD4B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x060065B3 RID: 26035 RVA: 0x00140F2F File Offset: 0x0013F12F
		public static LocalizedString ErrorActingAsRequired
		{
			get
			{
				return new LocalizedString("ErrorActingAsRequired", "ExC275EF", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x060065B4 RID: 26036 RVA: 0x00140F4D File Offset: 0x0013F14D
		public static LocalizedString ErrorCalendarInvalidDayForWeeklyRecurrence
		{
			get
			{
				return new LocalizedString("ErrorCalendarInvalidDayForWeeklyRecurrence", "Ex8952F9", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x060065B5 RID: 26037 RVA: 0x00140F6B File Offset: 0x0013F16B
		public static LocalizedString ErrorMissingInformationEmailAddress
		{
			get
			{
				return new LocalizedString("ErrorMissingInformationEmailAddress", "Ex606D76", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x060065B6 RID: 26038 RVA: 0x00140F89 File Offset: 0x0013F189
		public static LocalizedString UpdateFavoritesFavoriteNotFound
		{
			get
			{
				return new LocalizedString("UpdateFavoritesFavoriteNotFound", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x060065B7 RID: 26039 RVA: 0x00140FA7 File Offset: 0x0013F1A7
		public static LocalizedString ErrorCalendarDurationIsTooLong
		{
			get
			{
				return new LocalizedString("ErrorCalendarDurationIsTooLong", "Ex7FDD46", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x060065B8 RID: 26040 RVA: 0x00140FC5 File Offset: 0x0013F1C5
		public static LocalizedString ErrorNoRespondingCASInDestinationSite
		{
			get
			{
				return new LocalizedString("ErrorNoRespondingCASInDestinationSite", "Ex1634C2", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x060065B9 RID: 26041 RVA: 0x00140FE3 File Offset: 0x0013F1E3
		public static LocalizedString ErrorInvalidRecipients
		{
			get
			{
				return new LocalizedString("ErrorInvalidRecipients", "ExA4DFCE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x060065BA RID: 26042 RVA: 0x00141001 File Offset: 0x0013F201
		public static LocalizedString ErrorAppendBodyTypeMismatch
		{
			get
			{
				return new LocalizedString("ErrorAppendBodyTypeMismatch", "Ex64354B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x060065BB RID: 26043 RVA: 0x0014101F File Offset: 0x0013F21F
		public static LocalizedString ErrorDistributionListMemberNotExist
		{
			get
			{
				return new LocalizedString("ErrorDistributionListMemberNotExist", "ExF51352", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x060065BC RID: 26044 RVA: 0x0014103D File Offset: 0x0013F23D
		public static LocalizedString ErrorRequestTimeout
		{
			get
			{
				return new LocalizedString("ErrorRequestTimeout", "Ex5C1EFA", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x060065BD RID: 26045 RVA: 0x0014105B File Offset: 0x0013F25B
		public static LocalizedString MessageApplicationHasNoRoleAssginedWhichUserHas
		{
			get
			{
				return new LocalizedString("MessageApplicationHasNoRoleAssginedWhichUserHas", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x060065BE RID: 26046 RVA: 0x00141079 File Offset: 0x0013F279
		public static LocalizedString ErrorArchiveMailboxGetConversationFailed
		{
			get
			{
				return new LocalizedString("ErrorArchiveMailboxGetConversationFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x060065BF RID: 26047 RVA: 0x00141097 File Offset: 0x0013F297
		public static LocalizedString ErrorClientIntentNotFound
		{
			get
			{
				return new LocalizedString("ErrorClientIntentNotFound", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x060065C0 RID: 26048 RVA: 0x001410B5 File Offset: 0x0013F2B5
		public static LocalizedString ErrorNonExistentMailbox
		{
			get
			{
				return new LocalizedString("ErrorNonExistentMailbox", "ExD4BB19", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x060065C1 RID: 26049 RVA: 0x001410D3 File Offset: 0x0013F2D3
		public static LocalizedString ErrorVirusMessageDeleted
		{
			get
			{
				return new LocalizedString("ErrorVirusMessageDeleted", "ExE1B402", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x060065C2 RID: 26050 RVA: 0x001410F1 File Offset: 0x0013F2F1
		public static LocalizedString ErrorCannotFindUnifiedMailbox
		{
			get
			{
				return new LocalizedString("ErrorCannotFindUnifiedMailbox", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x060065C3 RID: 26051 RVA: 0x0014110F File Offset: 0x0013F30F
		public static LocalizedString ErrorUnifiedMailboxSupportedOnlyWithMicrosoftAccount
		{
			get
			{
				return new LocalizedString("ErrorUnifiedMailboxSupportedOnlyWithMicrosoftAccount", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x060065C4 RID: 26052 RVA: 0x0014112D File Offset: 0x0013F32D
		public static LocalizedString GroupMailboxCreationFailed
		{
			get
			{
				return new LocalizedString("GroupMailboxCreationFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x060065C5 RID: 26053 RVA: 0x0014114B File Offset: 0x0013F34B
		public static LocalizedString ErrorInvalidSearchQueryLength
		{
			get
			{
				return new LocalizedString("ErrorInvalidSearchQueryLength", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x060065C6 RID: 26054 RVA: 0x00141169 File Offset: 0x0013F369
		public static LocalizedString ErrorCalendarInvalidPropertyState
		{
			get
			{
				return new LocalizedString("ErrorCalendarInvalidPropertyState", "ExA8B660", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x060065C7 RID: 26055 RVA: 0x00141187 File Offset: 0x0013F387
		public static LocalizedString ErrorAddDelegatesFailed
		{
			get
			{
				return new LocalizedString("ErrorAddDelegatesFailed", "ExFDCC08", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x060065C8 RID: 26056 RVA: 0x001411A5 File Offset: 0x0013F3A5
		public static LocalizedString CcColon
		{
			get
			{
				return new LocalizedString("CcColon", "Ex449B53", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x060065C9 RID: 26057 RVA: 0x001411C3 File Offset: 0x0013F3C3
		public static LocalizedString ErrorCrossSiteRequest
		{
			get
			{
				return new LocalizedString("ErrorCrossSiteRequest", "Ex3F6822", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x060065CA RID: 26058 RVA: 0x001411E1 File Offset: 0x0013F3E1
		public static LocalizedString ErrorPublicFolderUserMustHaveMailbox
		{
			get
			{
				return new LocalizedString("ErrorPublicFolderUserMustHaveMailbox", "ExCAB9A6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x060065CB RID: 26059 RVA: 0x001411FF File Offset: 0x0013F3FF
		public static LocalizedString ErrorMessageTrackingTransientError
		{
			get
			{
				return new LocalizedString("ErrorMessageTrackingTransientError", "ExA14CF6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x060065CC RID: 26060 RVA: 0x0014121D File Offset: 0x0013F41D
		public static LocalizedString ErrorToFolderNotFound
		{
			get
			{
				return new LocalizedString("ErrorToFolderNotFound", "Ex97069E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x060065CD RID: 26061 RVA: 0x0014123B File Offset: 0x0013F43B
		public static LocalizedString ErrorDeleteUnifiedMessagingPromptFailed
		{
			get
			{
				return new LocalizedString("ErrorDeleteUnifiedMessagingPromptFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x060065CE RID: 26062 RVA: 0x00141259 File Offset: 0x0013F459
		public static LocalizedString UpdateFavoritesUnableToMoveFavorite
		{
			get
			{
				return new LocalizedString("UpdateFavoritesUnableToMoveFavorite", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x060065CF RID: 26063 RVA: 0x00141277 File Offset: 0x0013F477
		public static LocalizedString ErrorPeopleConnectionNoToken
		{
			get
			{
				return new LocalizedString("ErrorPeopleConnectionNoToken", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x060065D0 RID: 26064 RVA: 0x00141295 File Offset: 0x0013F495
		public static LocalizedString ErrorCannotSpecifySearchFolderAsSourceFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotSpecifySearchFolderAsSourceFolder", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x060065D1 RID: 26065 RVA: 0x001412B3 File Offset: 0x0013F4B3
		public static LocalizedString ErrorEmailAddressMismatch
		{
			get
			{
				return new LocalizedString("ErrorEmailAddressMismatch", "Ex0EE79E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060065D2 RID: 26066 RVA: 0x001412D4 File Offset: 0x0013F4D4
		public static LocalizedString ErrorAggregatedAccountLimitReached(int limit)
		{
			return new LocalizedString("ErrorAggregatedAccountLimitReached", "", false, false, CoreResources.ResourceManager, new object[]
			{
				limit
			});
		}

		// Token: 0x17001839 RID: 6201
		// (get) Token: 0x060065D3 RID: 26067 RVA: 0x00141308 File Offset: 0x0013F508
		public static LocalizedString ErrorUserConfigurationXmlDataNotExist
		{
			get
			{
				return new LocalizedString("ErrorUserConfigurationXmlDataNotExist", "Ex886EE9", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x060065D4 RID: 26068 RVA: 0x00141326 File Offset: 0x0013F526
		public static LocalizedString ErrorUnifiedMessagingRequestFailed
		{
			get
			{
				return new LocalizedString("ErrorUnifiedMessagingRequestFailed", "Ex5F4492", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x060065D5 RID: 26069 RVA: 0x00141344 File Offset: 0x0013F544
		public static LocalizedString ErrorCreateItemAccessDenied
		{
			get
			{
				return new LocalizedString("ErrorCreateItemAccessDenied", "Ex01C220", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x060065D6 RID: 26070 RVA: 0x00141362 File Offset: 0x0013F562
		public static LocalizedString RuleErrorFolderDoesNotExist
		{
			get
			{
				return new LocalizedString("RuleErrorFolderDoesNotExist", "Ex4685E4", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x060065D7 RID: 26071 RVA: 0x00141380 File Offset: 0x0013F580
		public static LocalizedString ErrorInvalidImContactId
		{
			get
			{
				return new LocalizedString("ErrorInvalidImContactId", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x060065D8 RID: 26072 RVA: 0x0014139E File Offset: 0x0013F59E
		public static LocalizedString ErrorNoPropertyTagForCustomProperties
		{
			get
			{
				return new LocalizedString("ErrorNoPropertyTagForCustomProperties", "ExD53820", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060065D9 RID: 26073 RVA: 0x001413BC File Offset: 0x0013F5BC
		public static LocalizedString ErrorRightsManagementDuplicateTemplateId(string id)
		{
			return new LocalizedString("ErrorRightsManagementDuplicateTemplateId", "", false, false, CoreResources.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x060065DA RID: 26074 RVA: 0x001413EB File Offset: 0x0013F5EB
		public static LocalizedString SentTime
		{
			get
			{
				return new LocalizedString("SentTime", "Ex3CA30C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x060065DB RID: 26075 RVA: 0x00141409 File Offset: 0x0013F609
		public static LocalizedString MessageNonExistentMailboxGuid
		{
			get
			{
				return new LocalizedString("MessageNonExistentMailboxGuid", "ExF743A6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x060065DC RID: 26076 RVA: 0x00141427 File Offset: 0x0013F627
		public static LocalizedString ErrorMaxRequestedUnifiedGroupsSetsExceeded
		{
			get
			{
				return new LocalizedString("ErrorMaxRequestedUnifiedGroupsSetsExceeded", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x060065DD RID: 26077 RVA: 0x00141445 File Offset: 0x0013F645
		public static LocalizedString ErrorInvalidAppSchemaVersionSupported
		{
			get
			{
				return new LocalizedString("ErrorInvalidAppSchemaVersionSupported", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x060065DE RID: 26078 RVA: 0x00141463 File Offset: 0x0013F663
		public static LocalizedString ErrorInvalidLogonType
		{
			get
			{
				return new LocalizedString("ErrorInvalidLogonType", "Ex5C410E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x060065DF RID: 26079 RVA: 0x00141481 File Offset: 0x0013F681
		public static LocalizedString MessageActAsUserRequiredForSuchApplicationRole
		{
			get
			{
				return new LocalizedString("MessageActAsUserRequiredForSuchApplicationRole", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x060065E0 RID: 26080 RVA: 0x0014149F File Offset: 0x0013F69F
		public static LocalizedString ErrorCalendarOutOfRange
		{
			get
			{
				return new LocalizedString("ErrorCalendarOutOfRange", "Ex09A8D3", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x060065E1 RID: 26081 RVA: 0x001414BD File Offset: 0x0013F6BD
		public static LocalizedString ErrorContentIndexingNotEnabled
		{
			get
			{
				return new LocalizedString("ErrorContentIndexingNotEnabled", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x060065E2 RID: 26082 RVA: 0x001414DB File Offset: 0x0013F6DB
		public static LocalizedString ErrorContentConversionFailed
		{
			get
			{
				return new LocalizedString("ErrorContentConversionFailed", "Ex0EA831", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x060065E3 RID: 26083 RVA: 0x001414F9 File Offset: 0x0013F6F9
		public static LocalizedString ConversationIdNotSupported
		{
			get
			{
				return new LocalizedString("ConversationIdNotSupported", "Ex5D7451", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x060065E4 RID: 26084 RVA: 0x00141517 File Offset: 0x0013F717
		public static LocalizedString ConversationSupportedOnlyForMailboxSession
		{
			get
			{
				return new LocalizedString("ConversationSupportedOnlyForMailboxSession", "ExFF98D8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x060065E5 RID: 26085 RVA: 0x00141535 File Offset: 0x0013F735
		public static LocalizedString ErrorMoveDistinguishedFolder
		{
			get
			{
				return new LocalizedString("ErrorMoveDistinguishedFolder", "Ex1871EC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x060065E6 RID: 26086 RVA: 0x00141553 File Offset: 0x0013F753
		public static LocalizedString ErrorMailboxCannotBeSpecifiedForPublicFolderRoot
		{
			get
			{
				return new LocalizedString("ErrorMailboxCannotBeSpecifiedForPublicFolderRoot", "Ex04F82B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x060065E7 RID: 26087 RVA: 0x00141571 File Offset: 0x0013F771
		public static LocalizedString IrmPreLicensingFailure
		{
			get
			{
				return new LocalizedString("IrmPreLicensingFailure", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x060065E8 RID: 26088 RVA: 0x0014158F File Offset: 0x0013F78F
		public static LocalizedString MessageMissingUserRolesForLegalHoldRoleTypeApp
		{
			get
			{
				return new LocalizedString("MessageMissingUserRolesForLegalHoldRoleTypeApp", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700184E RID: 6222
		// (get) Token: 0x060065E9 RID: 26089 RVA: 0x001415AD File Offset: 0x0013F7AD
		public static LocalizedString ErrorMailboxVersionNotSupported
		{
			get
			{
				return new LocalizedString("ErrorMailboxVersionNotSupported", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700184F RID: 6223
		// (get) Token: 0x060065EA RID: 26090 RVA: 0x001415CB File Offset: 0x0013F7CB
		public static LocalizedString ErrorRestrictionTooComplex
		{
			get
			{
				return new LocalizedString("ErrorRestrictionTooComplex", "Ex73969B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x060065EB RID: 26091 RVA: 0x001415E9 File Offset: 0x0013F7E9
		public static LocalizedString RuleErrorRecipientDoesNotExist
		{
			get
			{
				return new LocalizedString("RuleErrorRecipientDoesNotExist", "Ex7BFD5A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x060065EC RID: 26092 RVA: 0x00141607 File Offset: 0x0013F807
		public static LocalizedString ErrorInvalidAggregatedAccountCredentials
		{
			get
			{
				return new LocalizedString("ErrorInvalidAggregatedAccountCredentials", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x060065ED RID: 26093 RVA: 0x00141625 File Offset: 0x0013F825
		public static LocalizedString descInvalidSecurityContext
		{
			get
			{
				return new LocalizedString("descInvalidSecurityContext", "ExA3F1D9", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x060065EE RID: 26094 RVA: 0x00141643 File Offset: 0x0013F843
		public static LocalizedString MessagePublicFoldersNotSupportedForNonIndexable
		{
			get
			{
				return new LocalizedString("MessagePublicFoldersNotSupportedForNonIndexable", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x060065EF RID: 26095 RVA: 0x00141661 File Offset: 0x0013F861
		public static LocalizedString ErrorInvalidFilterNode
		{
			get
			{
				return new LocalizedString("ErrorInvalidFilterNode", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001855 RID: 6229
		// (get) Token: 0x060065F0 RID: 26096 RVA: 0x0014167F File Offset: 0x0013F87F
		public static LocalizedString ErrorIrmUserRightNotGranted
		{
			get
			{
				return new LocalizedString("ErrorIrmUserRightNotGranted", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001856 RID: 6230
		// (get) Token: 0x060065F1 RID: 26097 RVA: 0x0014169D File Offset: 0x0013F89D
		public static LocalizedString descInvalidRequestType
		{
			get
			{
				return new LocalizedString("descInvalidRequestType", "ExF721AD", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x060065F2 RID: 26098 RVA: 0x001416BB File Offset: 0x0013F8BB
		public static LocalizedString DowaNotProvisioned
		{
			get
			{
				return new LocalizedString("DowaNotProvisioned", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x060065F3 RID: 26099 RVA: 0x001416D9 File Offset: 0x0013F8D9
		public static LocalizedString ErrorRecurrenceEndDateTooBig
		{
			get
			{
				return new LocalizedString("ErrorRecurrenceEndDateTooBig", "Ex3B4015", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x060065F4 RID: 26100 RVA: 0x001416F7 File Offset: 0x0013F8F7
		public static LocalizedString ErrorInvalidItemForReply
		{
			get
			{
				return new LocalizedString("ErrorInvalidItemForReply", "ExC9FD00", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x060065F5 RID: 26101 RVA: 0x00141715 File Offset: 0x0013F915
		public static LocalizedString UpdateFavoritesInvalidUpdateFavoriteOperationType
		{
			get
			{
				return new LocalizedString("UpdateFavoritesInvalidUpdateFavoriteOperationType", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x060065F6 RID: 26102 RVA: 0x00141733 File Offset: 0x0013F933
		public static LocalizedString ErrorInvalidManagementRoleHeader
		{
			get
			{
				return new LocalizedString("ErrorInvalidManagementRoleHeader", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x060065F7 RID: 26103 RVA: 0x00141751 File Offset: 0x0013F951
		public static LocalizedString ErrorCannotGetExternalEcpUrl
		{
			get
			{
				return new LocalizedString("ErrorCannotGetExternalEcpUrl", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060065F8 RID: 26104 RVA: 0x00141770 File Offset: 0x0013F970
		public static LocalizedString ErrorSearchTooManyMailboxes(string errorHint, int mailboxCount, int maxAllowedMailboxes)
		{
			return new LocalizedString("ErrorSearchTooManyMailboxes", "", false, false, CoreResources.ResourceManager, new object[]
			{
				errorHint,
				mailboxCount,
				maxAllowedMailboxes
			});
		}

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x060065F9 RID: 26105 RVA: 0x001417B1 File Offset: 0x0013F9B1
		public static LocalizedString ErrorCannotCreateSearchFolderInPublicFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotCreateSearchFolderInPublicFolder", "Ex76F926", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x060065FA RID: 26106 RVA: 0x001417CF File Offset: 0x0013F9CF
		public static LocalizedString RuleErrorUnsupportedRule
		{
			get
			{
				return new LocalizedString("RuleErrorUnsupportedRule", "ExD129BD", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x060065FB RID: 26107 RVA: 0x001417ED File Offset: 0x0013F9ED
		public static LocalizedString ErrorMissingManagedFolderId
		{
			get
			{
				return new LocalizedString("ErrorMissingManagedFolderId", "Ex341D82", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x060065FC RID: 26108 RVA: 0x0014180B File Offset: 0x0013FA0B
		public static LocalizedString MessageInsufficientPermissionsToSend
		{
			get
			{
				return new LocalizedString("MessageInsufficientPermissionsToSend", "Ex3C9088", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x060065FD RID: 26109 RVA: 0x00141829 File Offset: 0x0013FA29
		public static LocalizedString ErrorInvalidCompleteDate
		{
			get
			{
				return new LocalizedString("ErrorInvalidCompleteDate", "ExE257EC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x060065FE RID: 26110 RVA: 0x00141847 File Offset: 0x0013FA47
		public static LocalizedString ErrorSearchFolderTimeout
		{
			get
			{
				return new LocalizedString("ErrorSearchFolderTimeout", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x060065FF RID: 26111 RVA: 0x00141865 File Offset: 0x0013FA65
		public static LocalizedString ErrorCannotSetAggregatedAccount
		{
			get
			{
				return new LocalizedString("ErrorCannotSetAggregatedAccount", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001864 RID: 6244
		// (get) Token: 0x06006600 RID: 26112 RVA: 0x00141883 File Offset: 0x0013FA83
		public static LocalizedString ErrorInvalidPushSubscriptionUrl
		{
			get
			{
				return new LocalizedString("ErrorInvalidPushSubscriptionUrl", "Ex53B39C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001865 RID: 6245
		// (get) Token: 0x06006601 RID: 26113 RVA: 0x001418A1 File Offset: 0x0013FAA1
		public static LocalizedString ErrorCannotAddAggregatedAccount
		{
			get
			{
				return new LocalizedString("ErrorCannotAddAggregatedAccount", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001866 RID: 6246
		// (get) Token: 0x06006602 RID: 26114 RVA: 0x001418BF File Offset: 0x0013FABF
		public static LocalizedString ErrorCalendarIsGroupMailboxForDecline
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsGroupMailboxForDecline", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x06006603 RID: 26115 RVA: 0x001418DD File Offset: 0x0013FADD
		public static LocalizedString ErrorNameResolutionNoMailbox
		{
			get
			{
				return new LocalizedString("ErrorNameResolutionNoMailbox", "Ex5D7FF7", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001868 RID: 6248
		// (get) Token: 0x06006604 RID: 26116 RVA: 0x001418FB File Offset: 0x0013FAFB
		public static LocalizedString ErrorCannotArchiveItemsInArchiveMailbox
		{
			get
			{
				return new LocalizedString("ErrorCannotArchiveItemsInArchiveMailbox", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006605 RID: 26117 RVA: 0x0014191C File Offset: 0x0013FB1C
		public static LocalizedString IrmRmsErrorCode(string code)
		{
			return new LocalizedString("IrmRmsErrorCode", "", false, false, CoreResources.ResourceManager, new object[]
			{
				code
			});
		}

		// Token: 0x17001869 RID: 6249
		// (get) Token: 0x06006606 RID: 26118 RVA: 0x0014194B File Offset: 0x0013FB4B
		public static LocalizedString MowaNotProvisioned
		{
			get
			{
				return new LocalizedString("MowaNotProvisioned", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700186A RID: 6250
		// (get) Token: 0x06006607 RID: 26119 RVA: 0x00141969 File Offset: 0x0013FB69
		public static LocalizedString ErrorInvalidOperationSendAndSaveCopyToPublicFolder
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationSendAndSaveCopyToPublicFolder", "Ex4ADBB0", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700186B RID: 6251
		// (get) Token: 0x06006608 RID: 26120 RVA: 0x00141987 File Offset: 0x0013FB87
		public static LocalizedString ConversationActionNeedDestinationFolderForMoveAction
		{
			get
			{
				return new LocalizedString("ConversationActionNeedDestinationFolderForMoveAction", "ExCEB5BD", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006609 RID: 26121 RVA: 0x001419A8 File Offset: 0x0013FBA8
		public static LocalizedString GetFederatedDirectoryGroupFailed(string name, string error)
		{
			return new LocalizedString("GetFederatedDirectoryGroupFailed", "", false, false, CoreResources.ResourceManager, new object[]
			{
				name,
				error
			});
		}

		// Token: 0x1700186C RID: 6252
		// (get) Token: 0x0600660A RID: 26122 RVA: 0x001419DB File Offset: 0x0013FBDB
		public static LocalizedString ErrorViewFilterRequiresSearchContext
		{
			get
			{
				return new LocalizedString("ErrorViewFilterRequiresSearchContext", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x0600660B RID: 26123 RVA: 0x001419F9 File Offset: 0x0013FBF9
		public static LocalizedString ErrorDelegateAlreadyExists
		{
			get
			{
				return new LocalizedString("ErrorDelegateAlreadyExists", "ExB85A77", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700186E RID: 6254
		// (get) Token: 0x0600660C RID: 26124 RVA: 0x00141A17 File Offset: 0x0013FC17
		public static LocalizedString ErrorSubmitQueryBasedHoldTaskFailed
		{
			get
			{
				return new LocalizedString("ErrorSubmitQueryBasedHoldTaskFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700186F RID: 6255
		// (get) Token: 0x0600660D RID: 26125 RVA: 0x00141A35 File Offset: 0x0013FC35
		public static LocalizedString ErrorPeopleConnectFailedToReadApplicationConfiguration
		{
			get
			{
				return new LocalizedString("ErrorPeopleConnectFailedToReadApplicationConfiguration", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001870 RID: 6256
		// (get) Token: 0x0600660E RID: 26126 RVA: 0x00141A53 File Offset: 0x0013FC53
		public static LocalizedString ErrorUnsupportedMapiPropertyType
		{
			get
			{
				return new LocalizedString("ErrorUnsupportedMapiPropertyType", "ExB8162D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001871 RID: 6257
		// (get) Token: 0x0600660F RID: 26127 RVA: 0x00141A71 File Offset: 0x0013FC71
		public static LocalizedString ErrorApprovalRequestAlreadyDecided
		{
			get
			{
				return new LocalizedString("ErrorApprovalRequestAlreadyDecided", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001872 RID: 6258
		// (get) Token: 0x06006610 RID: 26128 RVA: 0x00141A8F File Offset: 0x0013FC8F
		public static LocalizedString MessageCouldNotFindWeatherLocations
		{
			get
			{
				return new LocalizedString("MessageCouldNotFindWeatherLocations", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001873 RID: 6259
		// (get) Token: 0x06006611 RID: 26129 RVA: 0x00141AAD File Offset: 0x0013FCAD
		public static LocalizedString WhenColon
		{
			get
			{
				return new LocalizedString("WhenColon", "Ex592F5E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001874 RID: 6260
		// (get) Token: 0x06006612 RID: 26130 RVA: 0x00141ACB File Offset: 0x0013FCCB
		public static LocalizedString ErrorNoGroupingForQueryString
		{
			get
			{
				return new LocalizedString("ErrorNoGroupingForQueryString", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001875 RID: 6261
		// (get) Token: 0x06006613 RID: 26131 RVA: 0x00141AE9 File Offset: 0x0013FCE9
		public static LocalizedString ErrorInvalidIdStoreObjectIdTooLong
		{
			get
			{
				return new LocalizedString("ErrorInvalidIdStoreObjectIdTooLong", "ExC5B225", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001876 RID: 6262
		// (get) Token: 0x06006614 RID: 26132 RVA: 0x00141B07 File Offset: 0x0013FD07
		public static LocalizedString ErrorQuotaExceeded
		{
			get
			{
				return new LocalizedString("ErrorQuotaExceeded", "Ex276CAA", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001877 RID: 6263
		// (get) Token: 0x06006615 RID: 26133 RVA: 0x00141B25 File Offset: 0x0013FD25
		public static LocalizedString ConversationActionNeedReadStateForSetReadStateAction
		{
			get
			{
				return new LocalizedString("ConversationActionNeedReadStateForSetReadStateAction", "Ex632E59", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001878 RID: 6264
		// (get) Token: 0x06006616 RID: 26134 RVA: 0x00141B43 File Offset: 0x0013FD43
		public static LocalizedString ErrorLocationServicesRequestTimedOut
		{
			get
			{
				return new LocalizedString("ErrorLocationServicesRequestTimedOut", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001879 RID: 6265
		// (get) Token: 0x06006617 RID: 26135 RVA: 0x00141B61 File Offset: 0x0013FD61
		public static LocalizedString ErrorCalendarInvalidPropertyValue
		{
			get
			{
				return new LocalizedString("ErrorCalendarInvalidPropertyValue", "Ex8AD342", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700187A RID: 6266
		// (get) Token: 0x06006618 RID: 26136 RVA: 0x00141B7F File Offset: 0x0013FD7F
		public static LocalizedString ErrorManagedFolderAlreadyExists
		{
			get
			{
				return new LocalizedString("ErrorManagedFolderAlreadyExists", "ExEC0F10", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700187B RID: 6267
		// (get) Token: 0x06006619 RID: 26137 RVA: 0x00141B9D File Offset: 0x0013FD9D
		public static LocalizedString ErrorLocationServicesInvalidSource
		{
			get
			{
				return new LocalizedString("ErrorLocationServicesInvalidSource", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700187C RID: 6268
		// (get) Token: 0x0600661A RID: 26138 RVA: 0x00141BBB File Offset: 0x0013FDBB
		public static LocalizedString OnPremiseSynchorizedDiscoverySearch
		{
			get
			{
				return new LocalizedString("OnPremiseSynchorizedDiscoverySearch", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700187D RID: 6269
		// (get) Token: 0x0600661B RID: 26139 RVA: 0x00141BD9 File Offset: 0x0013FDD9
		public static LocalizedString ErrorInvalidOperationForAssociatedItems
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationForAssociatedItems", "Ex6863C6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700187E RID: 6270
		// (get) Token: 0x0600661C RID: 26140 RVA: 0x00141BF7 File Offset: 0x0013FDF7
		public static LocalizedString ErrorCorruptData
		{
			get
			{
				return new LocalizedString("ErrorCorruptData", "Ex6C01DC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600661D RID: 26141 RVA: 0x00141C18 File Offset: 0x0013FE18
		public static LocalizedString RuleErrorInvalidDateRange(string startDateTime, string endDateTime)
		{
			return new LocalizedString("RuleErrorInvalidDateRange", "Ex805828", false, true, CoreResources.ResourceManager, new object[]
			{
				startDateTime,
				endDateTime
			});
		}

		// Token: 0x1700187F RID: 6271
		// (get) Token: 0x0600661E RID: 26142 RVA: 0x00141C4B File Offset: 0x0013FE4B
		public static LocalizedString ErrorCalendarInvalidTimeZone
		{
			get
			{
				return new LocalizedString("ErrorCalendarInvalidTimeZone", "ExB0CF21", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001880 RID: 6272
		// (get) Token: 0x0600661F RID: 26143 RVA: 0x00141C69 File Offset: 0x0013FE69
		public static LocalizedString ErrorInvalidOperationMessageDispositionAssociatedItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationMessageDispositionAssociatedItem", "Ex33C0D8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001881 RID: 6273
		// (get) Token: 0x06006620 RID: 26144 RVA: 0x00141C87 File Offset: 0x0013FE87
		public static LocalizedString ErrorSubscriptionAccessDenied
		{
			get
			{
				return new LocalizedString("ErrorSubscriptionAccessDenied", "ExD4869E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001882 RID: 6274
		// (get) Token: 0x06006621 RID: 26145 RVA: 0x00141CA5 File Offset: 0x0013FEA5
		public static LocalizedString ErrorCannotReadRequestBody
		{
			get
			{
				return new LocalizedString("ErrorCannotReadRequestBody", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001883 RID: 6275
		// (get) Token: 0x06006622 RID: 26146 RVA: 0x00141CC3 File Offset: 0x0013FEC3
		public static LocalizedString ErrorNameResolutionMultipleResults
		{
			get
			{
				return new LocalizedString("ErrorNameResolutionMultipleResults", "ExD4D5C6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x06006623 RID: 26147 RVA: 0x00141CE1 File Offset: 0x0013FEE1
		public static LocalizedString ErrorInvalidExtendedProperty
		{
			get
			{
				return new LocalizedString("ErrorInvalidExtendedProperty", "ExF51F0A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x06006624 RID: 26148 RVA: 0x00141CFF File Offset: 0x0013FEFF
		public static LocalizedString EwsProxyCannotGetCredentials
		{
			get
			{
				return new LocalizedString("EwsProxyCannotGetCredentials", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x06006625 RID: 26149 RVA: 0x00141D1D File Offset: 0x0013FF1D
		public static LocalizedString UpdateFavoritesInvalidMoveFavoriteRequest
		{
			get
			{
				return new LocalizedString("UpdateFavoritesInvalidMoveFavoriteRequest", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001887 RID: 6279
		// (get) Token: 0x06006626 RID: 26150 RVA: 0x00141D3B File Offset: 0x0013FF3B
		public static LocalizedString ErrorInvalidPermissionSettings
		{
			get
			{
				return new LocalizedString("ErrorInvalidPermissionSettings", "Ex523233", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x06006627 RID: 26151 RVA: 0x00141D59 File Offset: 0x0013FF59
		public static LocalizedString ErrorProxyServiceDiscoveryFailed
		{
			get
			{
				return new LocalizedString("ErrorProxyServiceDiscoveryFailed", "Ex84DFBC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x06006628 RID: 26152 RVA: 0x00141D77 File Offset: 0x0013FF77
		public static LocalizedString ErrorInvalidItemForOperationAcceptItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidItemForOperationAcceptItem", "Ex5E6DCB", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x06006629 RID: 26153 RVA: 0x00141D95 File Offset: 0x0013FF95
		public static LocalizedString ErrorInvalidValueForPropertyDuplicateDictionaryKey
		{
			get
			{
				return new LocalizedString("ErrorInvalidValueForPropertyDuplicateDictionaryKey", "ExE0001C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x0600662A RID: 26154 RVA: 0x00141DB3 File Offset: 0x0013FFB3
		public static LocalizedString ErrorExceededSubscriptionCount
		{
			get
			{
				return new LocalizedString("ErrorExceededSubscriptionCount", "Ex61D62C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x0600662B RID: 26155 RVA: 0x00141DD1 File Offset: 0x0013FFD1
		public static LocalizedString ErrorPermissionNotAllowedByPolicy
		{
			get
			{
				return new LocalizedString("ErrorPermissionNotAllowedByPolicy", "Ex150362", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x0600662C RID: 26156 RVA: 0x00141DEF File Offset: 0x0013FFEF
		public static LocalizedString MessageInsufficientPermissionsToSubscribe
		{
			get
			{
				return new LocalizedString("MessageInsufficientPermissionsToSubscribe", "Ex69B609", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x0600662D RID: 26157 RVA: 0x00141E0D File Offset: 0x0014000D
		public static LocalizedString ErrorInvalidValueForPropertyDate
		{
			get
			{
				return new LocalizedString("ErrorInvalidValueForPropertyDate", "Ex130502", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x0600662E RID: 26158 RVA: 0x00141E2B File Offset: 0x0014002B
		public static LocalizedString ErrorUnsupportedRecurrence
		{
			get
			{
				return new LocalizedString("ErrorUnsupportedRecurrence", "Ex065C5D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x0600662F RID: 26159 RVA: 0x00141E49 File Offset: 0x00140049
		public static LocalizedString ErrorUserADObjectNotFound
		{
			get
			{
				return new LocalizedString("ErrorUserADObjectNotFound", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x06006630 RID: 26160 RVA: 0x00141E67 File Offset: 0x00140067
		public static LocalizedString ErrorCannotAttachSelf
		{
			get
			{
				return new LocalizedString("ErrorCannotAttachSelf", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x06006631 RID: 26161 RVA: 0x00141E85 File Offset: 0x00140085
		public static LocalizedString ErrorMissingInformationSharingFolderId
		{
			get
			{
				return new LocalizedString("ErrorMissingInformationSharingFolderId", "ExEBBBDE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x06006632 RID: 26162 RVA: 0x00141EA3 File Offset: 0x001400A3
		public static LocalizedString ErrorCannotSetFromOnMeetingResponse
		{
			get
			{
				return new LocalizedString("ErrorCannotSetFromOnMeetingResponse", "Ex0E0324", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x06006633 RID: 26163 RVA: 0x00141EC1 File Offset: 0x001400C1
		public static LocalizedString MessageInvalidOperationForPublicFolderItemsAddParticipantByItemId
		{
			get
			{
				return new LocalizedString("MessageInvalidOperationForPublicFolderItemsAddParticipantByItemId", "Ex20810A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x06006634 RID: 26164 RVA: 0x00141EDF File Offset: 0x001400DF
		public static LocalizedString ErrorInvalidItemForOperationCreateItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidItemForOperationCreateItem", "Ex9BB6C4", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x06006635 RID: 26165 RVA: 0x00141EFD File Offset: 0x001400FD
		public static LocalizedString ErrorInvalidPropertyForExists
		{
			get
			{
				return new LocalizedString("ErrorInvalidPropertyForExists", "Ex0E0BE8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x06006636 RID: 26166 RVA: 0x00141F1B File Offset: 0x0014011B
		public static LocalizedString ErrorCannotSaveSentItemInPublicFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotSaveSentItemInPublicFolder", "Ex0B0492", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006637 RID: 26167 RVA: 0x00141F3C File Offset: 0x0014013C
		public static LocalizedString ErrorExtensionNotFound(string extensionID)
		{
			return new LocalizedString("ErrorExtensionNotFound", "", false, false, CoreResources.ResourceManager, new object[]
			{
				extensionID
			});
		}

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x06006638 RID: 26168 RVA: 0x00141F6B File Offset: 0x0014016B
		public static LocalizedString ErrorRestrictionTooLong
		{
			get
			{
				return new LocalizedString("ErrorRestrictionTooLong", "Ex321685", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x06006639 RID: 26169 RVA: 0x00141F89 File Offset: 0x00140189
		public static LocalizedString ErrorUnsupportedPropertyDefinition
		{
			get
			{
				return new LocalizedString("ErrorUnsupportedPropertyDefinition", "ExE641E6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600663A RID: 26170 RVA: 0x00141FA8 File Offset: 0x001401A8
		public static LocalizedString ErrorImContactLimitReached(int limit)
		{
			return new LocalizedString("ErrorImContactLimitReached", "", false, false, CoreResources.ResourceManager, new object[]
			{
				limit
			});
		}

		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x0600663B RID: 26171 RVA: 0x00141FDC File Offset: 0x001401DC
		public static LocalizedString SharePointCreationFailed
		{
			get
			{
				return new LocalizedString("SharePointCreationFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x0600663C RID: 26172 RVA: 0x00141FFA File Offset: 0x001401FA
		public static LocalizedString ErrorDataSizeLimitExceeded
		{
			get
			{
				return new LocalizedString("ErrorDataSizeLimitExceeded", "Ex515477", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x0600663D RID: 26173 RVA: 0x00142018 File Offset: 0x00140218
		public static LocalizedString ErrorFolderExists
		{
			get
			{
				return new LocalizedString("ErrorFolderExists", "ExF56EB5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600663E RID: 26174 RVA: 0x00142038 File Offset: 0x00140238
		public static LocalizedString ErrorPropertyNotSupportUpdate(string property)
		{
			return new LocalizedString("ErrorPropertyNotSupportUpdate", "", false, false, CoreResources.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x0600663F RID: 26175 RVA: 0x00142067 File Offset: 0x00140267
		public static LocalizedString ErrorUnifiedGroupAlreadyExists
		{
			get
			{
				return new LocalizedString("ErrorUnifiedGroupAlreadyExists", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x06006640 RID: 26176 RVA: 0x00142085 File Offset: 0x00140285
		public static LocalizedString MessageApplicationTokenOnly
		{
			get
			{
				return new LocalizedString("MessageApplicationTokenOnly", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x06006641 RID: 26177 RVA: 0x001420A3 File Offset: 0x001402A3
		public static LocalizedString ErrorSharingNoExternalEwsAvailable
		{
			get
			{
				return new LocalizedString("ErrorSharingNoExternalEwsAvailable", "Ex3D67D4", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006642 RID: 26178 RVA: 0x001420C4 File Offset: 0x001402C4
		public static LocalizedString ErrorPropertyNotSupportFilter(string property)
		{
			return new LocalizedString("ErrorPropertyNotSupportFilter", "", false, false, CoreResources.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x06006643 RID: 26179 RVA: 0x001420F3 File Offset: 0x001402F3
		public static LocalizedString RuleErrorEmptyValueFound
		{
			get
			{
				return new LocalizedString("RuleErrorEmptyValueFound", "Ex14D20A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x06006644 RID: 26180 RVA: 0x00142111 File Offset: 0x00140311
		public static LocalizedString ErrorOccurrenceCrossingBoundary
		{
			get
			{
				return new LocalizedString("ErrorOccurrenceCrossingBoundary", "ExEE33A0", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x06006645 RID: 26181 RVA: 0x0014212F File Offset: 0x0014032F
		public static LocalizedString ErrorArchiveMailboxServiceDiscoveryFailed
		{
			get
			{
				return new LocalizedString("ErrorArchiveMailboxServiceDiscoveryFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x06006646 RID: 26182 RVA: 0x0014214D File Offset: 0x0014034D
		public static LocalizedString ErrorInvalidAttachmentSubfilterTextFilter
		{
			get
			{
				return new LocalizedString("ErrorInvalidAttachmentSubfilterTextFilter", "Ex1FDE39", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x06006647 RID: 26183 RVA: 0x0014216B File Offset: 0x0014036B
		public static LocalizedString ErrorGetSharingMetadataNotSupported
		{
			get
			{
				return new LocalizedString("ErrorGetSharingMetadataNotSupported", "Ex836DF7", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x06006648 RID: 26184 RVA: 0x00142189 File Offset: 0x00140389
		public static LocalizedString MessageRecipientMustHaveEmailAddress
		{
			get
			{
				return new LocalizedString("MessageRecipientMustHaveEmailAddress", "ExAC2952", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x06006649 RID: 26185 RVA: 0x001421A7 File Offset: 0x001403A7
		public static LocalizedString ErrorInvalidRecipientSubfilterTextFilter
		{
			get
			{
				return new LocalizedString("ErrorInvalidRecipientSubfilterTextFilter", "ExC1900B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x0600664A RID: 26186 RVA: 0x001421C5 File Offset: 0x001403C5
		public static LocalizedString ErrorInvalidPropertyRequest
		{
			get
			{
				return new LocalizedString("ErrorInvalidPropertyRequest", "Ex84179E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x0600664B RID: 26187 RVA: 0x001421E3 File Offset: 0x001403E3
		public static LocalizedString ErrorCalendarIsNotOrganizer
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsNotOrganizer", "ExA9638F", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x0600664C RID: 26188 RVA: 0x00142201 File Offset: 0x00140401
		public static LocalizedString ErrorInvalidProvisionDeviceID
		{
			get
			{
				return new LocalizedString("ErrorInvalidProvisionDeviceID", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x0600664D RID: 26189 RVA: 0x0014221F File Offset: 0x0014041F
		public static LocalizedString MessageCouldNotGetWeatherDataForLocation
		{
			get
			{
				return new LocalizedString("MessageCouldNotGetWeatherDataForLocation", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x0600664E RID: 26190 RVA: 0x0014223D File Offset: 0x0014043D
		public static LocalizedString ErrorTimeProposalMissingStartOrEndTimeError
		{
			get
			{
				return new LocalizedString("ErrorTimeProposalMissingStartOrEndTimeError", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x0600664F RID: 26191 RVA: 0x0014225B File Offset: 0x0014045B
		public static LocalizedString ErrorInvalidSubfilterTypeNotAttendeeType
		{
			get
			{
				return new LocalizedString("ErrorInvalidSubfilterTypeNotAttendeeType", "ExC66AB4", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x06006650 RID: 26192 RVA: 0x00142279 File Offset: 0x00140479
		public static LocalizedString PropertyCommandNotSupportSet
		{
			get
			{
				return new LocalizedString("PropertyCommandNotSupportSet", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x06006651 RID: 26193 RVA: 0x00142297 File Offset: 0x00140497
		public static LocalizedString ErrorImpersonationFailed
		{
			get
			{
				return new LocalizedString("ErrorImpersonationFailed", "Ex4CB570", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x06006652 RID: 26194 RVA: 0x001422B5 File Offset: 0x001404B5
		public static LocalizedString ErrorSubscriptionNotFound
		{
			get
			{
				return new LocalizedString("ErrorSubscriptionNotFound", "Ex308BB5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x06006653 RID: 26195 RVA: 0x001422D3 File Offset: 0x001404D3
		public static LocalizedString MessageCalendarInsufficientPermissionsToMoveMeetingRequest
		{
			get
			{
				return new LocalizedString("MessageCalendarInsufficientPermissionsToMoveMeetingRequest", "Ex281B8F", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x06006654 RID: 26196 RVA: 0x001422F1 File Offset: 0x001404F1
		public static LocalizedString ErrorInvalidIdMalformed
		{
			get
			{
				return new LocalizedString("ErrorInvalidIdMalformed", "Ex9C1B66", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x06006655 RID: 26197 RVA: 0x0014230F File Offset: 0x0014050F
		public static LocalizedString ErrorCalendarIsGroupMailboxForSuppressReadReceipt
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsGroupMailboxForSuppressReadReceipt", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x06006656 RID: 26198 RVA: 0x0014232D File Offset: 0x0014052D
		public static LocalizedString ErrorCannotGetSourceFolderPath
		{
			get
			{
				return new LocalizedString("ErrorCannotGetSourceFolderPath", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x06006657 RID: 26199 RVA: 0x0014234B File Offset: 0x0014054B
		public static LocalizedString ErrorWildcardAndGroupExpansionNotAllowed
		{
			get
			{
				return new LocalizedString("ErrorWildcardAndGroupExpansionNotAllowed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x06006658 RID: 26200 RVA: 0x00142369 File Offset: 0x00140569
		public static LocalizedString UnsupportedInlineAttachmentContentType
		{
			get
			{
				return new LocalizedString("UnsupportedInlineAttachmentContentType", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018B6 RID: 6326
		// (get) Token: 0x06006659 RID: 26201 RVA: 0x00142387 File Offset: 0x00140587
		public static LocalizedString RuleErrorUnexpectedError
		{
			get
			{
				return new LocalizedString("RuleErrorUnexpectedError", "ExF7776A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018B7 RID: 6327
		// (get) Token: 0x0600665A RID: 26202 RVA: 0x001423A5 File Offset: 0x001405A5
		public static LocalizedString MessageCalendarInsufficientPermissionsToDraftsFolder
		{
			get
			{
				return new LocalizedString("MessageCalendarInsufficientPermissionsToDraftsFolder", "Ex9573E3", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x0600665B RID: 26203 RVA: 0x001423C3 File Offset: 0x001405C3
		public static LocalizedString ErrorADUnavailable
		{
			get
			{
				return new LocalizedString("ErrorADUnavailable", "Ex182232", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018B9 RID: 6329
		// (get) Token: 0x0600665C RID: 26204 RVA: 0x001423E1 File Offset: 0x001405E1
		public static LocalizedString ErrorInvalidPhoneNumber
		{
			get
			{
				return new LocalizedString("ErrorInvalidPhoneNumber", "Ex712A46", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018BA RID: 6330
		// (get) Token: 0x0600665D RID: 26205 RVA: 0x001423FF File Offset: 0x001405FF
		public static LocalizedString ErrorSoftDeletedTraversalsNotAllowedOnPublicFolders
		{
			get
			{
				return new LocalizedString("ErrorSoftDeletedTraversalsNotAllowedOnPublicFolders", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018BB RID: 6331
		// (get) Token: 0x0600665E RID: 26206 RVA: 0x0014241D File Offset: 0x0014061D
		public static LocalizedString ErrorCalendarIsDelegatedForTentative
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsDelegatedForTentative", "Ex695CDA", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018BC RID: 6332
		// (get) Token: 0x0600665F RID: 26207 RVA: 0x0014243B File Offset: 0x0014063B
		public static LocalizedString ErrorFoldersMustBelongToSameMailbox
		{
			get
			{
				return new LocalizedString("ErrorFoldersMustBelongToSameMailbox", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x06006660 RID: 26208 RVA: 0x00142459 File Offset: 0x00140659
		public static LocalizedString ErrorDataSourceOperation
		{
			get
			{
				return new LocalizedString("ErrorDataSourceOperation", "ExDA7D8E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x06006661 RID: 26209 RVA: 0x00142477 File Offset: 0x00140677
		public static LocalizedString ErrorCalendarMeetingIsOutOfDateResponseNotProcessed
		{
			get
			{
				return new LocalizedString("ErrorCalendarMeetingIsOutOfDateResponseNotProcessed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x06006662 RID: 26210 RVA: 0x00142495 File Offset: 0x00140695
		public static LocalizedString MessageInvalidIdMalformedEwsIdFormat
		{
			get
			{
				return new LocalizedString("MessageInvalidIdMalformedEwsIdFormat", "ExAB29BC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x06006663 RID: 26211 RVA: 0x001424B3 File Offset: 0x001406B3
		public static LocalizedString ErrorPreviousPageNavigationCurrentlyNotSupported
		{
			get
			{
				return new LocalizedString("ErrorPreviousPageNavigationCurrentlyNotSupported", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x06006664 RID: 26212 RVA: 0x001424D1 File Offset: 0x001406D1
		public static LocalizedString ErrorCannotEmptyPublicFolderToDeletedItems
		{
			get
			{
				return new LocalizedString("ErrorCannotEmptyPublicFolderToDeletedItems", "Ex37E536", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x06006665 RID: 26213 RVA: 0x001424EF File Offset: 0x001406EF
		public static LocalizedString ErrorInvalidSharingData
		{
			get
			{
				return new LocalizedString("ErrorInvalidSharingData", "Ex2BBC6A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x06006666 RID: 26214 RVA: 0x0014250D File Offset: 0x0014070D
		public static LocalizedString MessageCalendarInsufficientPermissionsToMeetingMessageFolder
		{
			get
			{
				return new LocalizedString("MessageCalendarInsufficientPermissionsToMeetingMessageFolder", "ExB60528", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x06006667 RID: 26215 RVA: 0x0014252B File Offset: 0x0014072B
		public static LocalizedString ErrorInvalidOperationCannotSpecifyItemId
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationCannotSpecifyItemId", "Ex38B50D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x06006668 RID: 26216 RVA: 0x00142549 File Offset: 0x00140749
		public static LocalizedString ErrorCalendarIsGroupMailboxForTentative
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsGroupMailboxForTentative", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x06006669 RID: 26217 RVA: 0x00142567 File Offset: 0x00140767
		public static LocalizedString ErrorMessageSizeExceeded
		{
			get
			{
				return new LocalizedString("ErrorMessageSizeExceeded", "ExA25687", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x0600666A RID: 26218 RVA: 0x00142585 File Offset: 0x00140785
		public static LocalizedString InvalidDateTimePrecisionValue
		{
			get
			{
				return new LocalizedString("InvalidDateTimePrecisionValue", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x0600666B RID: 26219 RVA: 0x001425A3 File Offset: 0x001407A3
		public static LocalizedString ErrorStaleObject
		{
			get
			{
				return new LocalizedString("ErrorStaleObject", "ExBF6C57", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600666C RID: 26220 RVA: 0x001425C4 File Offset: 0x001407C4
		public static LocalizedString GetFederatedDirectoryUserFailed(string name, string error)
		{
			return new LocalizedString("GetFederatedDirectoryUserFailed", "", false, false, CoreResources.ResourceManager, new object[]
			{
				name,
				error
			});
		}

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x0600666D RID: 26221 RVA: 0x001425F7 File Offset: 0x001407F7
		public static LocalizedString UpdateFavoritesUnableToAddFolderToFavorites
		{
			get
			{
				return new LocalizedString("UpdateFavoritesUnableToAddFolderToFavorites", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x0600666E RID: 26222 RVA: 0x00142615 File Offset: 0x00140815
		public static LocalizedString ErrorPasswordExpired
		{
			get
			{
				return new LocalizedString("ErrorPasswordExpired", "Ex84A415", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x0600666F RID: 26223 RVA: 0x00142633 File Offset: 0x00140833
		public static LocalizedString ErrorInvalidOperationCannotPerformOperationOnADRecipients
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationCannotPerformOperationOnADRecipients", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x06006670 RID: 26224 RVA: 0x00142651 File Offset: 0x00140851
		public static LocalizedString ErrorTooManyObjectsOpened
		{
			get
			{
				return new LocalizedString("ErrorTooManyObjectsOpened", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x06006671 RID: 26225 RVA: 0x0014266F File Offset: 0x0014086F
		public static LocalizedString MessageInvalidMailboxInvalidReferencedItem
		{
			get
			{
				return new LocalizedString("MessageInvalidMailboxInvalidReferencedItem", "Ex8886C6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x06006672 RID: 26226 RVA: 0x0014268D File Offset: 0x0014088D
		public static LocalizedString MessageApplicationHasNoGivenRoleAssigned
		{
			get
			{
				return new LocalizedString("MessageApplicationHasNoGivenRoleAssigned", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x06006673 RID: 26227 RVA: 0x001426AB File Offset: 0x001408AB
		public static LocalizedString MessageRecipientsArrayTooLong
		{
			get
			{
				return new LocalizedString("MessageRecipientsArrayTooLong", "Ex173DA5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x06006674 RID: 26228 RVA: 0x001426C9 File Offset: 0x001408C9
		public static LocalizedString ErrorInvalidIdXml
		{
			get
			{
				return new LocalizedString("ErrorInvalidIdXml", "Ex2F9DBD", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x06006675 RID: 26229 RVA: 0x001426E7 File Offset: 0x001408E7
		public static LocalizedString ErrorCallerWithoutMailboxCannotUseSendOnly
		{
			get
			{
				return new LocalizedString("ErrorCallerWithoutMailboxCannotUseSendOnly", "Ex760520", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x06006676 RID: 26230 RVA: 0x00142705 File Offset: 0x00140905
		public static LocalizedString ErrorArchiveMailboxSearchFailed
		{
			get
			{
				return new LocalizedString("ErrorArchiveMailboxSearchFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018D3 RID: 6355
		// (get) Token: 0x06006677 RID: 26231 RVA: 0x00142723 File Offset: 0x00140923
		public static LocalizedString PostedOn
		{
			get
			{
				return new LocalizedString("PostedOn", "Ex4A0682", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x06006678 RID: 26232 RVA: 0x00142741 File Offset: 0x00140941
		public static LocalizedString ErrorInvalidExternalSharingInitiator
		{
			get
			{
				return new LocalizedString("ErrorInvalidExternalSharingInitiator", "Ex425BF6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x06006679 RID: 26233 RVA: 0x0014275F File Offset: 0x0014095F
		public static LocalizedString ErrorMailboxStoreUnavailable
		{
			get
			{
				return new LocalizedString("ErrorMailboxStoreUnavailable", "ExAD00DA", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x0600667A RID: 26234 RVA: 0x0014277D File Offset: 0x0014097D
		public static LocalizedString ErrorInvalidCalendarViewRestrictionOrSort
		{
			get
			{
				return new LocalizedString("ErrorInvalidCalendarViewRestrictionOrSort", "Ex500516", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x0600667B RID: 26235 RVA: 0x0014279B File Offset: 0x0014099B
		public static LocalizedString ErrorSavedItemFolderNotFound
		{
			get
			{
				return new LocalizedString("ErrorSavedItemFolderNotFound", "Ex65F59A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x0600667C RID: 26236 RVA: 0x001427B9 File Offset: 0x001409B9
		public static LocalizedString ErrorCalendarOccurrenceIsDeletedFromRecurrence
		{
			get
			{
				return new LocalizedString("ErrorCalendarOccurrenceIsDeletedFromRecurrence", "ExA3AC90", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x0600667D RID: 26237 RVA: 0x001427D7 File Offset: 0x001409D7
		public static LocalizedString ErrorMissingRecipients
		{
			get
			{
				return new LocalizedString("ErrorMissingRecipients", "Ex631099", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018DA RID: 6362
		// (get) Token: 0x0600667E RID: 26238 RVA: 0x001427F5 File Offset: 0x001409F5
		public static LocalizedString ErrorTimeProposalInvalidInCreateItemRequest
		{
			get
			{
				return new LocalizedString("ErrorTimeProposalInvalidInCreateItemRequest", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018DB RID: 6363
		// (get) Token: 0x0600667F RID: 26239 RVA: 0x00142813 File Offset: 0x00140A13
		public static LocalizedString ErrorCalendarIsDelegatedForRemove
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsDelegatedForRemove", "Ex5432D1", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018DC RID: 6364
		// (get) Token: 0x06006680 RID: 26240 RVA: 0x00142831 File Offset: 0x00140A31
		public static LocalizedString ErrorInvalidLikeRequest
		{
			get
			{
				return new LocalizedString("ErrorInvalidLikeRequest", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x06006681 RID: 26241 RVA: 0x0014284F File Offset: 0x00140A4F
		public static LocalizedString MessageRecurrenceStartDateTooSmall
		{
			get
			{
				return new LocalizedString("MessageRecurrenceStartDateTooSmall", "Ex30666F", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x06006682 RID: 26242 RVA: 0x0014286D File Offset: 0x00140A6D
		public static LocalizedString ErrorUnknownTimeZone
		{
			get
			{
				return new LocalizedString("ErrorUnknownTimeZone", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006683 RID: 26243 RVA: 0x0014288C File Offset: 0x00140A8C
		public static LocalizedString ExchangeServiceResponseErrorWithCode(string errorCode, string errorMessage)
		{
			return new LocalizedString("ExchangeServiceResponseErrorWithCode", "", false, false, CoreResources.ResourceManager, new object[]
			{
				errorCode,
				errorMessage
			});
		}

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x06006684 RID: 26244 RVA: 0x001428BF File Offset: 0x00140ABF
		public static LocalizedString ErrorProxyGroupSidLimitExceeded
		{
			get
			{
				return new LocalizedString("ErrorProxyGroupSidLimitExceeded", "Ex3A4700", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x06006685 RID: 26245 RVA: 0x001428DD File Offset: 0x00140ADD
		public static LocalizedString ErrorCannotRemoveAggregatedAccount
		{
			get
			{
				return new LocalizedString("ErrorCannotRemoveAggregatedAccount", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x06006686 RID: 26246 RVA: 0x001428FB File Offset: 0x00140AFB
		public static LocalizedString ErrorInvalidShape
		{
			get
			{
				return new LocalizedString("ErrorInvalidShape", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x06006687 RID: 26247 RVA: 0x00142919 File Offset: 0x00140B19
		public static LocalizedString ErrorInvalidLicense
		{
			get
			{
				return new LocalizedString("ErrorInvalidLicense", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x06006688 RID: 26248 RVA: 0x00142937 File Offset: 0x00140B37
		public static LocalizedString ErrorAccountDisabled
		{
			get
			{
				return new LocalizedString("ErrorAccountDisabled", "Ex9B575D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x06006689 RID: 26249 RVA: 0x00142955 File Offset: 0x00140B55
		public static LocalizedString ErrorHoldIsNotFound
		{
			get
			{
				return new LocalizedString("ErrorHoldIsNotFound", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x0600668A RID: 26250 RVA: 0x00142973 File Offset: 0x00140B73
		public static LocalizedString MessageMessageIsNotDraft
		{
			get
			{
				return new LocalizedString("MessageMessageIsNotDraft", "Ex4EC065", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x0600668B RID: 26251 RVA: 0x00142991 File Offset: 0x00140B91
		public static LocalizedString ErrorWrongServerVersionDelegate
		{
			get
			{
				return new LocalizedString("ErrorWrongServerVersionDelegate", "Ex1FAF18", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x0600668C RID: 26252 RVA: 0x001429AF File Offset: 0x00140BAF
		public static LocalizedString OnBehalfOf
		{
			get
			{
				return new LocalizedString("OnBehalfOf", "ExA4C7CC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x0600668D RID: 26253 RVA: 0x001429CD File Offset: 0x00140BCD
		public static LocalizedString ErrorInvalidOperationForPublicFolderItems
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationForPublicFolderItems", "ExE4739A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600668E RID: 26254 RVA: 0x001429EC File Offset: 0x00140BEC
		public static LocalizedString ErrorInvalidDelegateUserId(string s)
		{
			return new LocalizedString("ErrorInvalidDelegateUserId", "ExF63986", false, true, CoreResources.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x0600668F RID: 26255 RVA: 0x00142A1B File Offset: 0x00140C1B
		public static LocalizedString ErrorCalendarCannotUseIdForRecurringMasterId
		{
			get
			{
				return new LocalizedString("ErrorCalendarCannotUseIdForRecurringMasterId", "Ex39E629", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x06006690 RID: 26256 RVA: 0x00142A39 File Offset: 0x00140C39
		public static LocalizedString ErrorInvalidSubscriptionRequest
		{
			get
			{
				return new LocalizedString("ErrorInvalidSubscriptionRequest", "Ex781E33", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x06006691 RID: 26257 RVA: 0x00142A57 File Offset: 0x00140C57
		public static LocalizedString ErrorInvalidIdEmpty
		{
			get
			{
				return new LocalizedString("ErrorInvalidIdEmpty", "ExF5E1DE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018EC RID: 6380
		// (get) Token: 0x06006692 RID: 26258 RVA: 0x00142A75 File Offset: 0x00140C75
		public static LocalizedString ErrorInvalidAttachmentId
		{
			get
			{
				return new LocalizedString("ErrorInvalidAttachmentId", "Ex2CB985", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x06006693 RID: 26259 RVA: 0x00142A93 File Offset: 0x00140C93
		public static LocalizedString ErrorBothQueryStringAndRestrictionNonNull
		{
			get
			{
				return new LocalizedString("ErrorBothQueryStringAndRestrictionNonNull", "Ex1CF719", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x06006694 RID: 26260 RVA: 0x00142AB1 File Offset: 0x00140CB1
		public static LocalizedString RuleErrorRuleNotFound
		{
			get
			{
				return new LocalizedString("RuleErrorRuleNotFound", "ExA39D9D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x06006695 RID: 26261 RVA: 0x00142ACF File Offset: 0x00140CCF
		public static LocalizedString ErrorDiscoverySearchesDisabled
		{
			get
			{
				return new LocalizedString("ErrorDiscoverySearchesDisabled", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x06006696 RID: 26262 RVA: 0x00142AED File Offset: 0x00140CED
		public static LocalizedString ErrorCalendarIsCancelledForTentative
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsCancelledForTentative", "Ex062096", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018F1 RID: 6385
		// (get) Token: 0x06006697 RID: 26263 RVA: 0x00142B0B File Offset: 0x00140D0B
		public static LocalizedString ErrorRecurrenceHasNoOccurrence
		{
			get
			{
				return new LocalizedString("ErrorRecurrenceHasNoOccurrence", "Ex8B963C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018F2 RID: 6386
		// (get) Token: 0x06006698 RID: 26264 RVA: 0x00142B29 File Offset: 0x00140D29
		public static LocalizedString MessageNonExistentMailboxLegacyDN
		{
			get
			{
				return new LocalizedString("MessageNonExistentMailboxLegacyDN", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018F3 RID: 6387
		// (get) Token: 0x06006699 RID: 26265 RVA: 0x00142B47 File Offset: 0x00140D47
		public static LocalizedString ErrorNoDestinationCASDueToKerberosRequirements
		{
			get
			{
				return new LocalizedString("ErrorNoDestinationCASDueToKerberosRequirements", "Ex862091", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018F4 RID: 6388
		// (get) Token: 0x0600669A RID: 26266 RVA: 0x00142B65 File Offset: 0x00140D65
		public static LocalizedString ErrorFolderNotFound
		{
			get
			{
				return new LocalizedString("ErrorFolderNotFound", "ExB41165", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600669B RID: 26267 RVA: 0x00142B84 File Offset: 0x00140D84
		public static LocalizedString ErrorInvalidPropertyVersionRequest(string prop, string version)
		{
			return new LocalizedString("ErrorInvalidPropertyVersionRequest", "", false, false, CoreResources.ResourceManager, new object[]
			{
				prop,
				version
			});
		}

		// Token: 0x170018F5 RID: 6389
		// (get) Token: 0x0600669C RID: 26268 RVA: 0x00142BB7 File Offset: 0x00140DB7
		public static LocalizedString ErrorCannotPinGroupIfNotAMember
		{
			get
			{
				return new LocalizedString("ErrorCannotPinGroupIfNotAMember", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x0600669D RID: 26269 RVA: 0x00142BD5 File Offset: 0x00140DD5
		public static LocalizedString MessageInsufficientPermissionsToSync
		{
			get
			{
				return new LocalizedString("MessageInsufficientPermissionsToSync", "ExB32262", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x0600669E RID: 26270 RVA: 0x00142BF3 File Offset: 0x00140DF3
		public static LocalizedString ErrorCalendarIsDelegatedForAccept
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsDelegatedForAccept", "Ex516DAB", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018F8 RID: 6392
		// (get) Token: 0x0600669F RID: 26271 RVA: 0x00142C11 File Offset: 0x00140E11
		public static LocalizedString ErrorInvalidClientAccessTokenRequest
		{
			get
			{
				return new LocalizedString("ErrorInvalidClientAccessTokenRequest", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x060066A0 RID: 26272 RVA: 0x00142C2F File Offset: 0x00140E2F
		public static LocalizedString ErrorCalendarOccurrenceIndexIsOutOfRecurrenceRange
		{
			get
			{
				return new LocalizedString("ErrorCalendarOccurrenceIndexIsOutOfRecurrenceRange", "ExF31924", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x060066A1 RID: 26273 RVA: 0x00142C4D File Offset: 0x00140E4D
		public static LocalizedString MessageMissingUpdateDelegateRequestInformation
		{
			get
			{
				return new LocalizedString("MessageMissingUpdateDelegateRequestInformation", "ExEB2E7B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x060066A2 RID: 26274 RVA: 0x00142C6B File Offset: 0x00140E6B
		public static LocalizedString ErrorCannotOpenFileAttachment
		{
			get
			{
				return new LocalizedString("ErrorCannotOpenFileAttachment", "Ex51CFA6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x060066A3 RID: 26275 RVA: 0x00142C89 File Offset: 0x00140E89
		public static LocalizedString ErrorInvalidFolderId
		{
			get
			{
				return new LocalizedString("ErrorInvalidFolderId", "ExE497FB", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x060066A4 RID: 26276 RVA: 0x00142CA7 File Offset: 0x00140EA7
		public static LocalizedString ErrorInvalidPropertyUpdateSentMessage
		{
			get
			{
				return new LocalizedString("ErrorInvalidPropertyUpdateSentMessage", "Ex2684A9", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x060066A5 RID: 26277 RVA: 0x00142CC5 File Offset: 0x00140EC5
		public static LocalizedString MessageCalendarInsufficientPermissionsToDefaultCalendarFolder
		{
			get
			{
				return new LocalizedString("MessageCalendarInsufficientPermissionsToDefaultCalendarFolder", "Ex4652AD", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x060066A6 RID: 26278 RVA: 0x00142CE3 File Offset: 0x00140EE3
		public static LocalizedString IrmServerMisConfigured
		{
			get
			{
				return new LocalizedString("IrmServerMisConfigured", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x060066A7 RID: 26279 RVA: 0x00142D01 File Offset: 0x00140F01
		public static LocalizedString RuleErrorRulesOverQuota
		{
			get
			{
				return new LocalizedString("RuleErrorRulesOverQuota", "Ex1804B4", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x060066A8 RID: 26280 RVA: 0x00142D1F File Offset: 0x00140F1F
		public static LocalizedString ErrorNotAllowedExternalSharingByPolicy
		{
			get
			{
				return new LocalizedString("ErrorNotAllowedExternalSharingByPolicy", "ExA27DCB", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x060066A9 RID: 26281 RVA: 0x00142D3D File Offset: 0x00140F3D
		public static LocalizedString ErrorCannotCreatePostItemInNonMailFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotCreatePostItemInNonMailFolder", "Ex98D95C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x060066AA RID: 26282 RVA: 0x00142D5B File Offset: 0x00140F5B
		public static LocalizedString ErrorCannotEmptyCalendarOrSearchFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotEmptyCalendarOrSearchFolder", "Ex3E1847", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060066AB RID: 26283 RVA: 0x00142D7C File Offset: 0x00140F7C
		public static LocalizedString ErrorInvalidParameter(string parameter)
		{
			return new LocalizedString("ErrorInvalidParameter", "", false, false, CoreResources.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x060066AC RID: 26284 RVA: 0x00142DAB File Offset: 0x00140FAB
		public static LocalizedString ErrorEmptyAggregatedAccountMailboxGuidStoredInSyncRequest
		{
			get
			{
				return new LocalizedString("ErrorEmptyAggregatedAccountMailboxGuidStoredInSyncRequest", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x060066AD RID: 26285 RVA: 0x00142DC9 File Offset: 0x00140FC9
		public static LocalizedString ErrorExpiredSubscription
		{
			get
			{
				return new LocalizedString("ErrorExpiredSubscription", "Ex7DB92C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x060066AE RID: 26286 RVA: 0x00142DE7 File Offset: 0x00140FE7
		public static LocalizedString ErrorODataAccessDisabled
		{
			get
			{
				return new LocalizedString("ErrorODataAccessDisabled", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x060066AF RID: 26287 RVA: 0x00142E05 File Offset: 0x00141005
		public static LocalizedString ErrorCannotArchiveItemsInPublicFolders
		{
			get
			{
				return new LocalizedString("ErrorCannotArchiveItemsInPublicFolders", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001908 RID: 6408
		// (get) Token: 0x060066B0 RID: 26288 RVA: 0x00142E23 File Offset: 0x00141023
		public static LocalizedString ErrorAssociatedTraversalDisallowedWithQueryString
		{
			get
			{
				return new LocalizedString("ErrorAssociatedTraversalDisallowedWithQueryString", "Ex4414E5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001909 RID: 6409
		// (get) Token: 0x060066B1 RID: 26289 RVA: 0x00142E41 File Offset: 0x00141041
		public static LocalizedString ErrorCalendarIsOrganizerForDecline
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsOrganizerForDecline", "ExC4D4B5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700190A RID: 6410
		// (get) Token: 0x060066B2 RID: 26290 RVA: 0x00142E5F File Offset: 0x0014105F
		public static LocalizedString ErrorMissingEmailAddressForManagedFolder
		{
			get
			{
				return new LocalizedString("ErrorMissingEmailAddressForManagedFolder", "Ex9BC5A7", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700190B RID: 6411
		// (get) Token: 0x060066B3 RID: 26291 RVA: 0x00142E7D File Offset: 0x0014107D
		public static LocalizedString ErrorGetSharingMetadataOnlyForMailbox
		{
			get
			{
				return new LocalizedString("ErrorGetSharingMetadataOnlyForMailbox", "Ex02B446", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700190C RID: 6412
		// (get) Token: 0x060066B4 RID: 26292 RVA: 0x00142E9B File Offset: 0x0014109B
		public static LocalizedString MessageActingAsMustHaveRoutingType
		{
			get
			{
				return new LocalizedString("MessageActingAsMustHaveRoutingType", "Ex560094", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700190D RID: 6413
		// (get) Token: 0x060066B5 RID: 26293 RVA: 0x00142EB9 File Offset: 0x001410B9
		public static LocalizedString ErrorInvalidOperationAddItemToMyCalendar
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationAddItemToMyCalendar", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700190E RID: 6414
		// (get) Token: 0x060066B6 RID: 26294 RVA: 0x00142ED7 File Offset: 0x001410D7
		public static LocalizedString ErrorSyncFolderNotFound
		{
			get
			{
				return new LocalizedString("ErrorSyncFolderNotFound", "Ex84FE57", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700190F RID: 6415
		// (get) Token: 0x060066B7 RID: 26295 RVA: 0x00142EF5 File Offset: 0x001410F5
		public static LocalizedString ErrorInvalidSharingMessage
		{
			get
			{
				return new LocalizedString("ErrorInvalidSharingMessage", "Ex147FF8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001910 RID: 6416
		// (get) Token: 0x060066B8 RID: 26296 RVA: 0x00142F13 File Offset: 0x00141113
		public static LocalizedString descInvalidRequest
		{
			get
			{
				return new LocalizedString("descInvalidRequest", "ExF11EFC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001911 RID: 6417
		// (get) Token: 0x060066B9 RID: 26297 RVA: 0x00142F31 File Offset: 0x00141131
		public static LocalizedString ErrorUnsupportedServiceConfigurationType
		{
			get
			{
				return new LocalizedString("ErrorUnsupportedServiceConfigurationType", "Ex4E8D4A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001912 RID: 6418
		// (get) Token: 0x060066BA RID: 26298 RVA: 0x00142F4F File Offset: 0x0014114F
		public static LocalizedString RuleErrorCreateWithRuleId
		{
			get
			{
				return new LocalizedString("RuleErrorCreateWithRuleId", "Ex39EAA8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001913 RID: 6419
		// (get) Token: 0x060066BB RID: 26299 RVA: 0x00142F6D File Offset: 0x0014116D
		public static LocalizedString LoadExtensionCustomPropertiesFailed
		{
			get
			{
				return new LocalizedString("LoadExtensionCustomPropertiesFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001914 RID: 6420
		// (get) Token: 0x060066BC RID: 26300 RVA: 0x00142F8B File Offset: 0x0014118B
		public static LocalizedString ErrorUserNotAllowedByPolicy
		{
			get
			{
				return new LocalizedString("ErrorUserNotAllowedByPolicy", "Ex7CA58E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060066BD RID: 26301 RVA: 0x00142FAC File Offset: 0x001411AC
		public static LocalizedString ErrorImGroupLimitReached(int limit)
		{
			return new LocalizedString("ErrorImGroupLimitReached", "", false, false, CoreResources.ResourceManager, new object[]
			{
				limit
			});
		}

		// Token: 0x17001915 RID: 6421
		// (get) Token: 0x060066BE RID: 26302 RVA: 0x00142FE0 File Offset: 0x001411E0
		public static LocalizedString MessageCouldNotGetWeatherData
		{
			get
			{
				return new LocalizedString("MessageCouldNotGetWeatherData", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001916 RID: 6422
		// (get) Token: 0x060066BF RID: 26303 RVA: 0x00142FFE File Offset: 0x001411FE
		public static LocalizedString MessageMultipleApplicationRolesNotSupported
		{
			get
			{
				return new LocalizedString("MessageMultipleApplicationRolesNotSupported", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001917 RID: 6423
		// (get) Token: 0x060066C0 RID: 26304 RVA: 0x0014301C File Offset: 0x0014121C
		public static LocalizedString ErrorPropertyValidationFailure
		{
			get
			{
				return new LocalizedString("ErrorPropertyValidationFailure", "Ex75C34C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001918 RID: 6424
		// (get) Token: 0x060066C1 RID: 26305 RVA: 0x0014303A File Offset: 0x0014123A
		public static LocalizedString ErrorInvalidOperationCalendarViewAssociatedItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationCalendarViewAssociatedItem", "Ex5014BE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001919 RID: 6425
		// (get) Token: 0x060066C2 RID: 26306 RVA: 0x00143058 File Offset: 0x00141258
		public static LocalizedString ErrorInvalidUserPrincipalName
		{
			get
			{
				return new LocalizedString("ErrorInvalidUserPrincipalName", "ExCB3AC6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700191A RID: 6426
		// (get) Token: 0x060066C3 RID: 26307 RVA: 0x00143076 File Offset: 0x00141276
		public static LocalizedString ErrorMissedNotificationEvents
		{
			get
			{
				return new LocalizedString("ErrorMissedNotificationEvents", "Ex1A0D20", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700191B RID: 6427
		// (get) Token: 0x060066C4 RID: 26308 RVA: 0x00143094 File Offset: 0x00141294
		public static LocalizedString ErrorCannotRemoveAggregatedAccountMailbox
		{
			get
			{
				return new LocalizedString("ErrorCannotRemoveAggregatedAccountMailbox", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700191C RID: 6428
		// (get) Token: 0x060066C5 RID: 26309 RVA: 0x001430B2 File Offset: 0x001412B2
		public static LocalizedString MessageCalendarUnableToUpdateMeetingRequest
		{
			get
			{
				return new LocalizedString("MessageCalendarUnableToUpdateMeetingRequest", "Ex6979E3", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060066C6 RID: 26310 RVA: 0x001430D0 File Offset: 0x001412D0
		public static LocalizedString ExecutingUserNotFound(string legacyDn)
		{
			return new LocalizedString("ExecutingUserNotFound", "", false, false, CoreResources.ResourceManager, new object[]
			{
				legacyDn
			});
		}

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x060066C7 RID: 26311 RVA: 0x001430FF File Offset: 0x001412FF
		public static LocalizedString ErrorInvalidValueForPropertyUserConfigurationPublicFolder
		{
			get
			{
				return new LocalizedString("ErrorInvalidValueForPropertyUserConfigurationPublicFolder", "Ex9996D2", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700191E RID: 6430
		// (get) Token: 0x060066C8 RID: 26312 RVA: 0x0014311D File Offset: 0x0014131D
		public static LocalizedString ErrorFolderSave
		{
			get
			{
				return new LocalizedString("ErrorFolderSave", "Ex9A2CCC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700191F RID: 6431
		// (get) Token: 0x060066C9 RID: 26313 RVA: 0x0014313B File Offset: 0x0014133B
		public static LocalizedString MessageResolveNamesNotSufficientPermissionsToContactsFolder
		{
			get
			{
				return new LocalizedString("MessageResolveNamesNotSufficientPermissionsToContactsFolder", "Ex352183", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x060066CA RID: 26314 RVA: 0x00143159 File Offset: 0x00141359
		public static LocalizedString descMissingForestConfiguration
		{
			get
			{
				return new LocalizedString("descMissingForestConfiguration", "ExCF96F0", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001921 RID: 6433
		// (get) Token: 0x060066CB RID: 26315 RVA: 0x00143177 File Offset: 0x00141377
		public static LocalizedString ErrorUnsupportedPathForSortGroup
		{
			get
			{
				return new LocalizedString("ErrorUnsupportedPathForSortGroup", "Ex137B0E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001922 RID: 6434
		// (get) Token: 0x060066CC RID: 26316 RVA: 0x00143195 File Offset: 0x00141395
		public static LocalizedString ErrorContainsFilterWrongType
		{
			get
			{
				return new LocalizedString("ErrorContainsFilterWrongType", "ExFD021E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x060066CD RID: 26317 RVA: 0x001431B3 File Offset: 0x001413B3
		public static LocalizedString ErrorMailboxScopeNotAllowedWithoutQueryString
		{
			get
			{
				return new LocalizedString("ErrorMailboxScopeNotAllowedWithoutQueryString", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x060066CE RID: 26318 RVA: 0x001431D1 File Offset: 0x001413D1
		public static LocalizedString ErrorMessageTrackingPermanentError
		{
			get
			{
				return new LocalizedString("ErrorMessageTrackingPermanentError", "ExB40310", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001925 RID: 6437
		// (get) Token: 0x060066CF RID: 26319 RVA: 0x001431EF File Offset: 0x001413EF
		public static LocalizedString ErrorCannotDeleteObject
		{
			get
			{
				return new LocalizedString("ErrorCannotDeleteObject", "Ex621F13", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001926 RID: 6438
		// (get) Token: 0x060066D0 RID: 26320 RVA: 0x0014320D File Offset: 0x0014140D
		public static LocalizedString MessageCallerHasNoAdminRoleGranted
		{
			get
			{
				return new LocalizedString("MessageCallerHasNoAdminRoleGranted", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001927 RID: 6439
		// (get) Token: 0x060066D1 RID: 26321 RVA: 0x0014322B File Offset: 0x0014142B
		public static LocalizedString ErrorIrmNotSupported
		{
			get
			{
				return new LocalizedString("ErrorIrmNotSupported", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001928 RID: 6440
		// (get) Token: 0x060066D2 RID: 26322 RVA: 0x00143249 File Offset: 0x00141449
		public static LocalizedString ReferenceLinkSharedFrom
		{
			get
			{
				return new LocalizedString("ReferenceLinkSharedFrom", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x060066D3 RID: 26323 RVA: 0x00143267 File Offset: 0x00141467
		public static LocalizedString SentColon
		{
			get
			{
				return new LocalizedString("SentColon", "ExD86B1F", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700192A RID: 6442
		// (get) Token: 0x060066D4 RID: 26324 RVA: 0x00143285 File Offset: 0x00141485
		public static LocalizedString ErrorActingAsUserNotUnique
		{
			get
			{
				return new LocalizedString("ErrorActingAsUserNotUnique", "ExBD39AF", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700192B RID: 6443
		// (get) Token: 0x060066D5 RID: 26325 RVA: 0x001432A3 File Offset: 0x001414A3
		public static LocalizedString ErrorSearchQueryHasTooManyKeywords
		{
			get
			{
				return new LocalizedString("ErrorSearchQueryHasTooManyKeywords", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700192C RID: 6444
		// (get) Token: 0x060066D6 RID: 26326 RVA: 0x001432C1 File Offset: 0x001414C1
		public static LocalizedString ErrorFolderPropertyRequestFailed
		{
			get
			{
				return new LocalizedString("ErrorFolderPropertyRequestFailed", "ExF06095", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700192D RID: 6445
		// (get) Token: 0x060066D7 RID: 26327 RVA: 0x001432DF File Offset: 0x001414DF
		public static LocalizedString ErrorMimeContentInvalid
		{
			get
			{
				return new LocalizedString("ErrorMimeContentInvalid", "ExA584F2", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700192E RID: 6446
		// (get) Token: 0x060066D8 RID: 26328 RVA: 0x001432FD File Offset: 0x001414FD
		public static LocalizedString ErrorSharingSynchronizationFailed
		{
			get
			{
				return new LocalizedString("ErrorSharingSynchronizationFailed", "Ex88386E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700192F RID: 6447
		// (get) Token: 0x060066D9 RID: 26329 RVA: 0x0014331B File Offset: 0x0014151B
		public static LocalizedString ErrorPublicFolderSearchNotSupportedOnMultipleFolders
		{
			get
			{
				return new LocalizedString("ErrorPublicFolderSearchNotSupportedOnMultipleFolders", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001930 RID: 6448
		// (get) Token: 0x060066DA RID: 26330 RVA: 0x00143339 File Offset: 0x00141539
		public static LocalizedString ErrorNoFolderClassOverride
		{
			get
			{
				return new LocalizedString("ErrorNoFolderClassOverride", "Ex46F206", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001931 RID: 6449
		// (get) Token: 0x060066DB RID: 26331 RVA: 0x00143357 File Offset: 0x00141557
		public static LocalizedString ErrorUnsupportedTypeForConversion
		{
			get
			{
				return new LocalizedString("ErrorUnsupportedTypeForConversion", "Ex083F2A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001932 RID: 6450
		// (get) Token: 0x060066DC RID: 26332 RVA: 0x00143375 File Offset: 0x00141575
		public static LocalizedString ErrorInvalidItemForOperationDeclineItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidItemForOperationDeclineItem", "Ex8827A8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001933 RID: 6451
		// (get) Token: 0x060066DD RID: 26333 RVA: 0x00143393 File Offset: 0x00141593
		public static LocalizedString MessageCalendarInsufficientPermissionsToSaveCalendarItem
		{
			get
			{
				return new LocalizedString("MessageCalendarInsufficientPermissionsToSaveCalendarItem", "Ex5A0214", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001934 RID: 6452
		// (get) Token: 0x060066DE RID: 26334 RVA: 0x001433B1 File Offset: 0x001415B1
		public static LocalizedString ErrorRightsManagementException
		{
			get
			{
				return new LocalizedString("ErrorRightsManagementException", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001935 RID: 6453
		// (get) Token: 0x060066DF RID: 26335 RVA: 0x001433CF File Offset: 0x001415CF
		public static LocalizedString ErrorOperationNotAllowedWithPublicFolderRoot
		{
			get
			{
				return new LocalizedString("ErrorOperationNotAllowedWithPublicFolderRoot", "ExFE1183", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001936 RID: 6454
		// (get) Token: 0x060066E0 RID: 26336 RVA: 0x001433ED File Offset: 0x001415ED
		public static LocalizedString ErrorInvalidIdReturnedByResolveNames
		{
			get
			{
				return new LocalizedString("ErrorInvalidIdReturnedByResolveNames", "Ex37182D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001937 RID: 6455
		// (get) Token: 0x060066E1 RID: 26337 RVA: 0x0014340B File Offset: 0x0014160B
		public static LocalizedString descNoRequestType
		{
			get
			{
				return new LocalizedString("descNoRequestType", "ExCC601F", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001938 RID: 6456
		// (get) Token: 0x060066E2 RID: 26338 RVA: 0x00143429 File Offset: 0x00141629
		public static LocalizedString ErrorCalendarIsOrganizerForTentative
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsOrganizerForTentative", "Ex9E7C5B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001939 RID: 6457
		// (get) Token: 0x060066E3 RID: 26339 RVA: 0x00143447 File Offset: 0x00141647
		public static LocalizedString ErrorInvalidVotingRequest
		{
			get
			{
				return new LocalizedString("ErrorInvalidVotingRequest", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700193A RID: 6458
		// (get) Token: 0x060066E4 RID: 26340 RVA: 0x00143465 File Offset: 0x00141665
		public static LocalizedString ErrorInvalidProvisionDeviceType
		{
			get
			{
				return new LocalizedString("ErrorInvalidProvisionDeviceType", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700193B RID: 6459
		// (get) Token: 0x060066E5 RID: 26341 RVA: 0x00143483 File Offset: 0x00141683
		public static LocalizedString RuleErrorUnsupportedAddress
		{
			get
			{
				return new LocalizedString("RuleErrorUnsupportedAddress", "Ex269A81", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700193C RID: 6460
		// (get) Token: 0x060066E6 RID: 26342 RVA: 0x001434A1 File Offset: 0x001416A1
		public static LocalizedString ErrorInvalidCallStatus
		{
			get
			{
				return new LocalizedString("ErrorInvalidCallStatus", "Ex477F32", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700193D RID: 6461
		// (get) Token: 0x060066E7 RID: 26343 RVA: 0x001434BF File Offset: 0x001416BF
		public static LocalizedString ErrorInvalidSid
		{
			get
			{
				return new LocalizedString("ErrorInvalidSid", "Ex24BA8D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700193E RID: 6462
		// (get) Token: 0x060066E8 RID: 26344 RVA: 0x001434DD File Offset: 0x001416DD
		public static LocalizedString ErrorManagedFoldersRootFailure
		{
			get
			{
				return new LocalizedString("ErrorManagedFoldersRootFailure", "Ex191FE3", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700193F RID: 6463
		// (get) Token: 0x060066E9 RID: 26345 RVA: 0x001434FB File Offset: 0x001416FB
		public static LocalizedString ErrorProxiedSubscriptionCallFailure
		{
			get
			{
				return new LocalizedString("ErrorProxiedSubscriptionCallFailure", "Ex4151CF", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001940 RID: 6464
		// (get) Token: 0x060066EA RID: 26346 RVA: 0x00143519 File Offset: 0x00141719
		public static LocalizedString ErrorOccurrenceTimeSpanTooBig
		{
			get
			{
				return new LocalizedString("ErrorOccurrenceTimeSpanTooBig", "Ex444958", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001941 RID: 6465
		// (get) Token: 0x060066EB RID: 26347 RVA: 0x00143537 File Offset: 0x00141737
		public static LocalizedString MessageCalendarInsufficientPermissionsToMoveCalendarItem
		{
			get
			{
				return new LocalizedString("MessageCalendarInsufficientPermissionsToMoveCalendarItem", "ExF09A79", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001942 RID: 6466
		// (get) Token: 0x060066EC RID: 26348 RVA: 0x00143555 File Offset: 0x00141755
		public static LocalizedString ErrorNewEventStreamConnectionOpened
		{
			get
			{
				return new LocalizedString("ErrorNewEventStreamConnectionOpened", "Ex8EDE18", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001943 RID: 6467
		// (get) Token: 0x060066ED RID: 26349 RVA: 0x00143573 File Offset: 0x00141773
		public static LocalizedString ErrorArchiveMailboxNotEnabled
		{
			get
			{
				return new LocalizedString("ErrorArchiveMailboxNotEnabled", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060066EE RID: 26350 RVA: 0x00143594 File Offset: 0x00141794
		public static LocalizedString SetGroupMailboxFailed(string name, string error)
		{
			return new LocalizedString("SetGroupMailboxFailed", "", false, false, CoreResources.ResourceManager, new object[]
			{
				name,
				error
			});
		}

		// Token: 0x17001944 RID: 6468
		// (get) Token: 0x060066EF RID: 26351 RVA: 0x001435C7 File Offset: 0x001417C7
		public static LocalizedString ErrorCalendarCannotUseIdForOccurrenceId
		{
			get
			{
				return new LocalizedString("ErrorCalendarCannotUseIdForOccurrenceId", "ExA9D0B6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001945 RID: 6469
		// (get) Token: 0x060066F0 RID: 26352 RVA: 0x001435E5 File Offset: 0x001417E5
		public static LocalizedString ErrorAccessDenied
		{
			get
			{
				return new LocalizedString("ErrorAccessDenied", "Ex83A27D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001946 RID: 6470
		// (get) Token: 0x060066F1 RID: 26353 RVA: 0x00143603 File Offset: 0x00141803
		public static LocalizedString ErrorAttachmentSizeLimitExceeded
		{
			get
			{
				return new LocalizedString("ErrorAttachmentSizeLimitExceeded", "ExF8960C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001947 RID: 6471
		// (get) Token: 0x060066F2 RID: 26354 RVA: 0x00143621 File Offset: 0x00141821
		public static LocalizedString ErrorPropertyUpdate
		{
			get
			{
				return new LocalizedString("ErrorPropertyUpdate", "Ex0FE6C0", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001948 RID: 6472
		// (get) Token: 0x060066F3 RID: 26355 RVA: 0x0014363F File Offset: 0x0014183F
		public static LocalizedString RuleErrorInvalidValue
		{
			get
			{
				return new LocalizedString("RuleErrorInvalidValue", "Ex08048C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001949 RID: 6473
		// (get) Token: 0x060066F4 RID: 26356 RVA: 0x0014365D File Offset: 0x0014185D
		public static LocalizedString ErrorInvalidManagedFolderQuota
		{
			get
			{
				return new LocalizedString("ErrorInvalidManagedFolderQuota", "Ex37A451", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060066F5 RID: 26357 RVA: 0x0014367C File Offset: 0x0014187C
		public static LocalizedString IrmRmsErrorMessage(string message)
		{
			return new LocalizedString("IrmRmsErrorMessage", "", false, false, CoreResources.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x1700194A RID: 6474
		// (get) Token: 0x060066F6 RID: 26358 RVA: 0x001436AB File Offset: 0x001418AB
		public static LocalizedString ErrorCreateDistinguishedFolder
		{
			get
			{
				return new LocalizedString("ErrorCreateDistinguishedFolder", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700194B RID: 6475
		// (get) Token: 0x060066F7 RID: 26359 RVA: 0x001436C9 File Offset: 0x001418C9
		public static LocalizedString ShowDetails
		{
			get
			{
				return new LocalizedString("ShowDetails", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700194C RID: 6476
		// (get) Token: 0x060066F8 RID: 26360 RVA: 0x001436E7 File Offset: 0x001418E7
		public static LocalizedString ToColon
		{
			get
			{
				return new LocalizedString("ToColon", "ExC32F38", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700194D RID: 6477
		// (get) Token: 0x060066F9 RID: 26361 RVA: 0x00143705 File Offset: 0x00141905
		public static LocalizedString ErrorCrossMailboxMoveCopy
		{
			get
			{
				return new LocalizedString("ErrorCrossMailboxMoveCopy", "Ex3023B2", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700194E RID: 6478
		// (get) Token: 0x060066FA RID: 26362 RVA: 0x00143723 File Offset: 0x00141923
		public static LocalizedString FlagForFollowUp
		{
			get
			{
				return new LocalizedString("FlagForFollowUp", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700194F RID: 6479
		// (get) Token: 0x060066FB RID: 26363 RVA: 0x00143741 File Offset: 0x00141941
		public static LocalizedString ErrorGetStreamingEventsProxy
		{
			get
			{
				return new LocalizedString("ErrorGetStreamingEventsProxy", "Ex05EBB5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001950 RID: 6480
		// (get) Token: 0x060066FC RID: 26364 RVA: 0x0014375F File Offset: 0x0014195F
		public static LocalizedString ErrorCannotSetCalendarPermissionOnNonCalendarFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotSetCalendarPermissionOnNonCalendarFolder", "Ex0996B8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001951 RID: 6481
		// (get) Token: 0x060066FD RID: 26365 RVA: 0x0014377D File Offset: 0x0014197D
		public static LocalizedString SaveExtensionCustomPropertiesFailed
		{
			get
			{
				return new LocalizedString("SaveExtensionCustomPropertiesFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001952 RID: 6482
		// (get) Token: 0x060066FE RID: 26366 RVA: 0x0014379B File Offset: 0x0014199B
		public static LocalizedString ErrorConnectionFailed
		{
			get
			{
				return new LocalizedString("ErrorConnectionFailed", "ExB728B7", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001953 RID: 6483
		// (get) Token: 0x060066FF RID: 26367 RVA: 0x001437B9 File Offset: 0x001419B9
		public static LocalizedString ErrorCannotUseLocalAccount
		{
			get
			{
				return new LocalizedString("ErrorCannotUseLocalAccount", "ExFA6E4F", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001954 RID: 6484
		// (get) Token: 0x06006700 RID: 26368 RVA: 0x001437D7 File Offset: 0x001419D7
		public static LocalizedString descInvalidOofParameter
		{
			get
			{
				return new LocalizedString("descInvalidOofParameter", "Ex0D2EB7", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006701 RID: 26369 RVA: 0x001437F8 File Offset: 0x001419F8
		public static LocalizedString ErrorCalendarSeekToConditionNotSupported(string s)
		{
			return new LocalizedString("ErrorCalendarSeekToConditionNotSupported", "", false, false, CoreResources.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x17001955 RID: 6485
		// (get) Token: 0x06006702 RID: 26370 RVA: 0x00143827 File Offset: 0x00141A27
		public static LocalizedString ErrorTimeRangeIsTooLarge
		{
			get
			{
				return new LocalizedString("ErrorTimeRangeIsTooLarge", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001956 RID: 6486
		// (get) Token: 0x06006703 RID: 26371 RVA: 0x00143845 File Offset: 0x00141A45
		public static LocalizedString ErrorAffectedTaskOccurrencesRequired
		{
			get
			{
				return new LocalizedString("ErrorAffectedTaskOccurrencesRequired", "Ex837516", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001957 RID: 6487
		// (get) Token: 0x06006704 RID: 26372 RVA: 0x00143863 File Offset: 0x00141A63
		public static LocalizedString ErrorCannotGetAggregatedAccount
		{
			get
			{
				return new LocalizedString("ErrorCannotGetAggregatedAccount", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001958 RID: 6488
		// (get) Token: 0x06006705 RID: 26373 RVA: 0x00143881 File Offset: 0x00141A81
		public static LocalizedString AADIdentityCreationFailed
		{
			get
			{
				return new LocalizedString("AADIdentityCreationFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001959 RID: 6489
		// (get) Token: 0x06006706 RID: 26374 RVA: 0x0014389F File Offset: 0x00141A9F
		public static LocalizedString ErrorDuplicateInputFolderNames
		{
			get
			{
				return new LocalizedString("ErrorDuplicateInputFolderNames", "ExDBA238", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700195A RID: 6490
		// (get) Token: 0x06006707 RID: 26375 RVA: 0x001438BD File Offset: 0x00141ABD
		public static LocalizedString MessageNonExistentMailboxSmtpAddress
		{
			get
			{
				return new LocalizedString("MessageNonExistentMailboxSmtpAddress", "Ex1798DB", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006708 RID: 26376 RVA: 0x001438DC File Offset: 0x00141ADC
		public static LocalizedString RemoveGroupMailboxFailed(string name, string error)
		{
			return new LocalizedString("RemoveGroupMailboxFailed", "", false, false, CoreResources.ResourceManager, new object[]
			{
				name,
				error
			});
		}

		// Token: 0x1700195B RID: 6491
		// (get) Token: 0x06006709 RID: 26377 RVA: 0x0014390F File Offset: 0x00141B0F
		public static LocalizedString ErrorIncorrectUpdatePropertyCount
		{
			get
			{
				return new LocalizedString("ErrorIncorrectUpdatePropertyCount", "ExA6A8AC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700195C RID: 6492
		// (get) Token: 0x0600670A RID: 26378 RVA: 0x0014392D File Offset: 0x00141B2D
		public static LocalizedString ErrorInvalidSerializedAccessToken
		{
			get
			{
				return new LocalizedString("ErrorInvalidSerializedAccessToken", "ExE9C1D8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700195D RID: 6493
		// (get) Token: 0x0600670B RID: 26379 RVA: 0x0014394B File Offset: 0x00141B4B
		public static LocalizedString ErrorInvalidRoutingType
		{
			get
			{
				return new LocalizedString("ErrorInvalidRoutingType", "Ex79E48E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700195E RID: 6494
		// (get) Token: 0x0600670C RID: 26380 RVA: 0x00143969 File Offset: 0x00141B69
		public static LocalizedString ErrorSendMeetingInvitationsRequired
		{
			get
			{
				return new LocalizedString("ErrorSendMeetingInvitationsRequired", "Ex4BC018", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700195F RID: 6495
		// (get) Token: 0x0600670D RID: 26381 RVA: 0x00143987 File Offset: 0x00141B87
		public static LocalizedString ErrorInvalidIdNotAnItemAttachmentId
		{
			get
			{
				return new LocalizedString("ErrorInvalidIdNotAnItemAttachmentId", "ExAE23E4", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001960 RID: 6496
		// (get) Token: 0x0600670E RID: 26382 RVA: 0x001439A5 File Offset: 0x00141BA5
		public static LocalizedString RightsManagementInternalLicensingDisabled
		{
			get
			{
				return new LocalizedString("RightsManagementInternalLicensingDisabled", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001961 RID: 6497
		// (get) Token: 0x0600670F RID: 26383 RVA: 0x001439C3 File Offset: 0x00141BC3
		public static LocalizedString MessageCannotUseItemAsRecipient
		{
			get
			{
				return new LocalizedString("MessageCannotUseItemAsRecipient", "Ex043558", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001962 RID: 6498
		// (get) Token: 0x06006710 RID: 26384 RVA: 0x001439E1 File Offset: 0x00141BE1
		public static LocalizedString ErrorItemSaveUserConfigurationExists
		{
			get
			{
				return new LocalizedString("ErrorItemSaveUserConfigurationExists", "Ex3673F5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001963 RID: 6499
		// (get) Token: 0x06006711 RID: 26385 RVA: 0x001439FF File Offset: 0x00141BFF
		public static LocalizedString MessageInvalidMailboxMailboxType
		{
			get
			{
				return new LocalizedString("MessageInvalidMailboxMailboxType", "Ex2F3F2E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001964 RID: 6500
		// (get) Token: 0x06006712 RID: 26386 RVA: 0x00143A1D File Offset: 0x00141C1D
		public static LocalizedString ErrorCalendarIsCancelledForDecline
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsCancelledForDecline", "ExCEB40C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001965 RID: 6501
		// (get) Token: 0x06006713 RID: 26387 RVA: 0x00143A3B File Offset: 0x00141C3B
		public static LocalizedString ErrorClientIntentInvalidStateDefinition
		{
			get
			{
				return new LocalizedString("ErrorClientIntentInvalidStateDefinition", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001966 RID: 6502
		// (get) Token: 0x06006714 RID: 26388 RVA: 0x00143A59 File Offset: 0x00141C59
		public static LocalizedString ErrorInvalidRetentionTagInvisible
		{
			get
			{
				return new LocalizedString("ErrorInvalidRetentionTagInvisible", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001967 RID: 6503
		// (get) Token: 0x06006715 RID: 26389 RVA: 0x00143A77 File Offset: 0x00141C77
		public static LocalizedString ErrorItemSavePropertyError
		{
			get
			{
				return new LocalizedString("ErrorItemSavePropertyError", "Ex1E7CFA", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001968 RID: 6504
		// (get) Token: 0x06006716 RID: 26390 RVA: 0x00143A95 File Offset: 0x00141C95
		public static LocalizedString GetScopedTokenFailedWithInvalidScope
		{
			get
			{
				return new LocalizedString("GetScopedTokenFailedWithInvalidScope", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001969 RID: 6505
		// (get) Token: 0x06006717 RID: 26391 RVA: 0x00143AB3 File Offset: 0x00141CB3
		public static LocalizedString ErrorInvalidItemForOperationRemoveItem
		{
			get
			{
				return new LocalizedString("ErrorInvalidItemForOperationRemoveItem", "Ex38D554", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700196A RID: 6506
		// (get) Token: 0x06006718 RID: 26392 RVA: 0x00143AD1 File Offset: 0x00141CD1
		public static LocalizedString RuleErrorMessageClassificationNotFound
		{
			get
			{
				return new LocalizedString("RuleErrorMessageClassificationNotFound", "Ex66EBA5", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700196B RID: 6507
		// (get) Token: 0x06006719 RID: 26393 RVA: 0x00143AEF File Offset: 0x00141CEF
		public static LocalizedString MessageUnableToLoadRBACSettings
		{
			get
			{
				return new LocalizedString("MessageUnableToLoadRBACSettings", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700196C RID: 6508
		// (get) Token: 0x0600671A RID: 26394 RVA: 0x00143B0D File Offset: 0x00141D0D
		public static LocalizedString ErrorQueryLanguageNotValid
		{
			get
			{
				return new LocalizedString("ErrorQueryLanguageNotValid", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700196D RID: 6509
		// (get) Token: 0x0600671B RID: 26395 RVA: 0x00143B2B File Offset: 0x00141D2B
		public static LocalizedString Purple
		{
			get
			{
				return new LocalizedString("Purple", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700196E RID: 6510
		// (get) Token: 0x0600671C RID: 26396 RVA: 0x00143B49 File Offset: 0x00141D49
		public static LocalizedString InvalidMaxItemsToReturn
		{
			get
			{
				return new LocalizedString("InvalidMaxItemsToReturn", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700196F RID: 6511
		// (get) Token: 0x0600671D RID: 26397 RVA: 0x00143B67 File Offset: 0x00141D67
		public static LocalizedString PostedTo
		{
			get
			{
				return new LocalizedString("PostedTo", "Ex346A8E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001970 RID: 6512
		// (get) Token: 0x0600671E RID: 26398 RVA: 0x00143B85 File Offset: 0x00141D85
		public static LocalizedString ExchangeServiceResponseErrorNoResponse
		{
			get
			{
				return new LocalizedString("ExchangeServiceResponseErrorNoResponse", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001971 RID: 6513
		// (get) Token: 0x0600671F RID: 26399 RVA: 0x00143BA3 File Offset: 0x00141DA3
		public static LocalizedString ErrorPublicFolderOperationFailed
		{
			get
			{
				return new LocalizedString("ErrorPublicFolderOperationFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001972 RID: 6514
		// (get) Token: 0x06006720 RID: 26400 RVA: 0x00143BC1 File Offset: 0x00141DC1
		public static LocalizedString ErrorBatchProcessingStopped
		{
			get
			{
				return new LocalizedString("ErrorBatchProcessingStopped", "ExECB924", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001973 RID: 6515
		// (get) Token: 0x06006721 RID: 26401 RVA: 0x00143BDF File Offset: 0x00141DDF
		public static LocalizedString ErrorUnifiedMessagingServerNotFound
		{
			get
			{
				return new LocalizedString("ErrorUnifiedMessagingServerNotFound", "ExA051A1", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001974 RID: 6516
		// (get) Token: 0x06006722 RID: 26402 RVA: 0x00143BFD File Offset: 0x00141DFD
		public static LocalizedString InstantSearchNullFolderId
		{
			get
			{
				return new LocalizedString("InstantSearchNullFolderId", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001975 RID: 6517
		// (get) Token: 0x06006723 RID: 26403 RVA: 0x00143C1B File Offset: 0x00141E1B
		public static LocalizedString ErrorWeatherServiceDisabled
		{
			get
			{
				return new LocalizedString("ErrorWeatherServiceDisabled", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001976 RID: 6518
		// (get) Token: 0x06006724 RID: 26404 RVA: 0x00143C39 File Offset: 0x00141E39
		public static LocalizedString descNotEnoughPrivileges
		{
			get
			{
				return new LocalizedString("descNotEnoughPrivileges", "Ex44F71C", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001977 RID: 6519
		// (get) Token: 0x06006725 RID: 26405 RVA: 0x00143C57 File Offset: 0x00141E57
		public static LocalizedString CalendarInvalidFirstDayOfWeek
		{
			get
			{
				return new LocalizedString("CalendarInvalidFirstDayOfWeek", "Ex36C37A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001978 RID: 6520
		// (get) Token: 0x06006726 RID: 26406 RVA: 0x00143C75 File Offset: 0x00141E75
		public static LocalizedString Red
		{
			get
			{
				return new LocalizedString("Red", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001979 RID: 6521
		// (get) Token: 0x06006727 RID: 26407 RVA: 0x00143C93 File Offset: 0x00141E93
		public static LocalizedString ErrorInvalidExternalSharingSubscriber
		{
			get
			{
				return new LocalizedString("ErrorInvalidExternalSharingSubscriber", "Ex6048B7", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700197A RID: 6522
		// (get) Token: 0x06006728 RID: 26408 RVA: 0x00143CB1 File Offset: 0x00141EB1
		public static LocalizedString ErrorCannotUseFolderIdForItemId
		{
			get
			{
				return new LocalizedString("ErrorCannotUseFolderIdForItemId", "Ex52BA80", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700197B RID: 6523
		// (get) Token: 0x06006729 RID: 26409 RVA: 0x00143CCF File Offset: 0x00141ECF
		public static LocalizedString ErrorExchange14Required
		{
			get
			{
				return new LocalizedString("ErrorExchange14Required", "Ex0A199B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700197C RID: 6524
		// (get) Token: 0x0600672A RID: 26410 RVA: 0x00143CED File Offset: 0x00141EED
		public static LocalizedString ErrorProxyCallFailed
		{
			get
			{
				return new LocalizedString("ErrorProxyCallFailed", "Ex4796FE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700197D RID: 6525
		// (get) Token: 0x0600672B RID: 26411 RVA: 0x00143D0B File Offset: 0x00141F0B
		public static LocalizedString ErrorOrganizationNotFederated
		{
			get
			{
				return new LocalizedString("ErrorOrganizationNotFederated", "Ex8BFBA4", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700197E RID: 6526
		// (get) Token: 0x0600672C RID: 26412 RVA: 0x00143D29 File Offset: 0x00141F29
		public static LocalizedString Blue
		{
			get
			{
				return new LocalizedString("Blue", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700197F RID: 6527
		// (get) Token: 0x0600672D RID: 26413 RVA: 0x00143D47 File Offset: 0x00141F47
		public static LocalizedString ErrorCannotDeleteSubfoldersOfMsgRootFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotDeleteSubfoldersOfMsgRootFolder", "Ex630B38", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001980 RID: 6528
		// (get) Token: 0x0600672E RID: 26414 RVA: 0x00143D65 File Offset: 0x00141F65
		public static LocalizedString ErrorUpdatePropertyMismatch
		{
			get
			{
				return new LocalizedString("ErrorUpdatePropertyMismatch", "Ex203920", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001981 RID: 6529
		// (get) Token: 0x0600672F RID: 26415 RVA: 0x00143D83 File Offset: 0x00141F83
		public static LocalizedString ErrorIllegalCrossServerConnection
		{
			get
			{
				return new LocalizedString("ErrorIllegalCrossServerConnection", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001982 RID: 6530
		// (get) Token: 0x06006730 RID: 26416 RVA: 0x00143DA1 File Offset: 0x00141FA1
		public static LocalizedString ErrorImListMigration
		{
			get
			{
				return new LocalizedString("ErrorImListMigration", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001983 RID: 6531
		// (get) Token: 0x06006731 RID: 26417 RVA: 0x00143DBF File Offset: 0x00141FBF
		public static LocalizedString ErrorResponseSchemaValidation
		{
			get
			{
				return new LocalizedString("ErrorResponseSchemaValidation", "Ex2527B1", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001984 RID: 6532
		// (get) Token: 0x06006732 RID: 26418 RVA: 0x00143DDD File Offset: 0x00141FDD
		public static LocalizedString ServerNotInSite
		{
			get
			{
				return new LocalizedString("ServerNotInSite", "ExA23656", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001985 RID: 6533
		// (get) Token: 0x06006733 RID: 26419 RVA: 0x00143DFB File Offset: 0x00141FFB
		public static LocalizedString ErrorCannotAddAggregatedAccountToList
		{
			get
			{
				return new LocalizedString("ErrorCannotAddAggregatedAccountToList", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001986 RID: 6534
		// (get) Token: 0x06006734 RID: 26420 RVA: 0x00143E19 File Offset: 0x00142019
		public static LocalizedString WhereColon
		{
			get
			{
				return new LocalizedString("WhereColon", "Ex306FE6", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001987 RID: 6535
		// (get) Token: 0x06006735 RID: 26421 RVA: 0x00143E37 File Offset: 0x00142037
		public static LocalizedString ErrorInvalidApprovalRequest
		{
			get
			{
				return new LocalizedString("ErrorInvalidApprovalRequest", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001988 RID: 6536
		// (get) Token: 0x06006736 RID: 26422 RVA: 0x00143E55 File Offset: 0x00142055
		public static LocalizedString ErrorIncorrectEncodedIdType
		{
			get
			{
				return new LocalizedString("ErrorIncorrectEncodedIdType", "ExD8826E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001989 RID: 6537
		// (get) Token: 0x06006737 RID: 26423 RVA: 0x00143E73 File Offset: 0x00142073
		public static LocalizedString ErrorGetRemoteArchiveItemFailed
		{
			get
			{
				return new LocalizedString("ErrorGetRemoteArchiveItemFailed", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700198A RID: 6538
		// (get) Token: 0x06006738 RID: 26424 RVA: 0x00143E91 File Offset: 0x00142091
		public static LocalizedString ErrorInvalidImGroupId
		{
			get
			{
				return new LocalizedString("ErrorInvalidImGroupId", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700198B RID: 6539
		// (get) Token: 0x06006739 RID: 26425 RVA: 0x00143EAF File Offset: 0x001420AF
		public static LocalizedString ErrorInvalidRequestUnknownMethodDebug
		{
			get
			{
				return new LocalizedString("ErrorInvalidRequestUnknownMethodDebug", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700198C RID: 6540
		// (get) Token: 0x0600673A RID: 26426 RVA: 0x00143ECD File Offset: 0x001420CD
		public static LocalizedString ErrorBothViewFilterAndRestrictionNonNull
		{
			get
			{
				return new LocalizedString("ErrorBothViewFilterAndRestrictionNonNull", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700198D RID: 6541
		// (get) Token: 0x0600673B RID: 26427 RVA: 0x00143EEB File Offset: 0x001420EB
		public static LocalizedString ErrorCannotUseItemIdForFolderId
		{
			get
			{
				return new LocalizedString("ErrorCannotUseItemIdForFolderId", "Ex74D79D", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600673C RID: 26428 RVA: 0x00143F0C File Offset: 0x0014210C
		public static LocalizedString ErrorInvalidRequestedUser(string user)
		{
			return new LocalizedString("ErrorInvalidRequestedUser", "", false, false, CoreResources.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x1700198E RID: 6542
		// (get) Token: 0x0600673D RID: 26429 RVA: 0x00143F3B File Offset: 0x0014213B
		public static LocalizedString ErrorCannotDisableMandatoryExtension
		{
			get
			{
				return new LocalizedString("ErrorCannotDisableMandatoryExtension", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700198F RID: 6543
		// (get) Token: 0x0600673E RID: 26430 RVA: 0x00143F59 File Offset: 0x00142159
		public static LocalizedString ErrorInvalidSyncStateData
		{
			get
			{
				return new LocalizedString("ErrorInvalidSyncStateData", "ExB49446", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001990 RID: 6544
		// (get) Token: 0x0600673F RID: 26431 RVA: 0x00143F77 File Offset: 0x00142177
		public static LocalizedString ErrorSubmissionQuotaExceeded
		{
			get
			{
				return new LocalizedString("ErrorSubmissionQuotaExceeded", "Ex65324B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001991 RID: 6545
		// (get) Token: 0x06006740 RID: 26432 RVA: 0x00143F95 File Offset: 0x00142195
		public static LocalizedString ErrorMessageDispositionRequired
		{
			get
			{
				return new LocalizedString("ErrorMessageDispositionRequired", "ExEC5273", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001992 RID: 6546
		// (get) Token: 0x06006741 RID: 26433 RVA: 0x00143FB3 File Offset: 0x001421B3
		public static LocalizedString ErrorSearchScopeCannotHavePublicFolders
		{
			get
			{
				return new LocalizedString("ErrorSearchScopeCannotHavePublicFolders", "Ex1C5029", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001993 RID: 6547
		// (get) Token: 0x06006742 RID: 26434 RVA: 0x00143FD1 File Offset: 0x001421D1
		public static LocalizedString ErrorRemoveDelegatesFailed
		{
			get
			{
				return new LocalizedString("ErrorRemoveDelegatesFailed", "ExB2F113", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001994 RID: 6548
		// (get) Token: 0x06006743 RID: 26435 RVA: 0x00143FEF File Offset: 0x001421EF
		public static LocalizedString ErrorInvalidPagingMaxRows
		{
			get
			{
				return new LocalizedString("ErrorInvalidPagingMaxRows", "ExC20F4A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001995 RID: 6549
		// (get) Token: 0x06006744 RID: 26436 RVA: 0x0014400D File Offset: 0x0014220D
		public static LocalizedString RuleErrorMissingParameter
		{
			get
			{
				return new LocalizedString("RuleErrorMissingParameter", "Ex28AE72", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001996 RID: 6550
		// (get) Token: 0x06006745 RID: 26437 RVA: 0x0014402B File Offset: 0x0014222B
		public static LocalizedString ErrorLocationServicesInvalidQuery
		{
			get
			{
				return new LocalizedString("ErrorLocationServicesInvalidQuery", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001997 RID: 6551
		// (get) Token: 0x06006746 RID: 26438 RVA: 0x00144049 File Offset: 0x00142249
		public static LocalizedString MessageOccurrenceNotFound
		{
			get
			{
				return new LocalizedString("MessageOccurrenceNotFound", "ExBD686A", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001998 RID: 6552
		// (get) Token: 0x06006747 RID: 26439 RVA: 0x00144067 File Offset: 0x00142267
		public static LocalizedString ErrorSearchFolderNotInitialized
		{
			get
			{
				return new LocalizedString("ErrorSearchFolderNotInitialized", "Ex5E01EE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001999 RID: 6553
		// (get) Token: 0x06006748 RID: 26440 RVA: 0x00144085 File Offset: 0x00142285
		public static LocalizedString FolderScopeNotSpecified
		{
			get
			{
				return new LocalizedString("FolderScopeNotSpecified", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700199A RID: 6554
		// (get) Token: 0x06006749 RID: 26441 RVA: 0x001440A3 File Offset: 0x001422A3
		public static LocalizedString ErrorInvalidSubfilterType
		{
			get
			{
				return new LocalizedString("ErrorInvalidSubfilterType", "Ex7AD9DA", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700199B RID: 6555
		// (get) Token: 0x0600674A RID: 26442 RVA: 0x001440C1 File Offset: 0x001422C1
		public static LocalizedString ErrorDuplicateUserIdsSpecified
		{
			get
			{
				return new LocalizedString("ErrorDuplicateUserIdsSpecified", "Ex5DC80E", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700199C RID: 6556
		// (get) Token: 0x0600674B RID: 26443 RVA: 0x001440DF File Offset: 0x001422DF
		public static LocalizedString ErrorDelegateMustBeCalendarEditorToGetMeetingMessages
		{
			get
			{
				return new LocalizedString("ErrorDelegateMustBeCalendarEditorToGetMeetingMessages", "Ex91BB7B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700199D RID: 6557
		// (get) Token: 0x0600674C RID: 26444 RVA: 0x001440FD File Offset: 0x001422FD
		public static LocalizedString ErrorMismatchFolderId
		{
			get
			{
				return new LocalizedString("ErrorMismatchFolderId", "Ex7E49A8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700199E RID: 6558
		// (get) Token: 0x0600674D RID: 26445 RVA: 0x0014411B File Offset: 0x0014231B
		public static LocalizedString ErrorInvalidPropertyDelete
		{
			get
			{
				return new LocalizedString("ErrorInvalidPropertyDelete", "Ex345314", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700199F RID: 6559
		// (get) Token: 0x0600674E RID: 26446 RVA: 0x00144139 File Offset: 0x00142339
		public static LocalizedString MessageActingAsMustHaveEmailAddress
		{
			get
			{
				return new LocalizedString("MessageActingAsMustHaveEmailAddress", "ExF281BC", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019A0 RID: 6560
		// (get) Token: 0x0600674F RID: 26447 RVA: 0x00144157 File Offset: 0x00142357
		public static LocalizedString ErrorCalendarIsCancelledForRemove
		{
			get
			{
				return new LocalizedString("ErrorCalendarIsCancelledForRemove", "Ex31BC28", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019A1 RID: 6561
		// (get) Token: 0x06006750 RID: 26448 RVA: 0x00144175 File Offset: 0x00142375
		public static LocalizedString ErrorCannotResolveODataUrl
		{
			get
			{
				return new LocalizedString("ErrorCannotResolveODataUrl", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019A2 RID: 6562
		// (get) Token: 0x06006751 RID: 26449 RVA: 0x00144193 File Offset: 0x00142393
		public static LocalizedString ErrorCalendarEndDateIsEarlierThanStartDate
		{
			get
			{
				return new LocalizedString("ErrorCalendarEndDateIsEarlierThanStartDate", "ExBDF8D7", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019A3 RID: 6563
		// (get) Token: 0x06006752 RID: 26450 RVA: 0x001441B1 File Offset: 0x001423B1
		public static LocalizedString ErrorInvalidPercentCompleteValue
		{
			get
			{
				return new LocalizedString("ErrorInvalidPercentCompleteValue", "Ex04687B", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019A4 RID: 6564
		// (get) Token: 0x06006753 RID: 26451 RVA: 0x001441CF File Offset: 0x001423CF
		public static LocalizedString ErrorNoApplicableProxyCASServersAvailable
		{
			get
			{
				return new LocalizedString("ErrorNoApplicableProxyCASServersAvailable", "Ex5046B0", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019A5 RID: 6565
		// (get) Token: 0x06006754 RID: 26452 RVA: 0x001441ED File Offset: 0x001423ED
		public static LocalizedString IrmProtectedVoicemailFeatureDisabled
		{
			get
			{
				return new LocalizedString("IrmProtectedVoicemailFeatureDisabled", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019A6 RID: 6566
		// (get) Token: 0x06006755 RID: 26453 RVA: 0x0014420B File Offset: 0x0014240B
		public static LocalizedString IrmExternalLicensingDisabled
		{
			get
			{
				return new LocalizedString("IrmExternalLicensingDisabled", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019A7 RID: 6567
		// (get) Token: 0x06006756 RID: 26454 RVA: 0x00144229 File Offset: 0x00142429
		public static LocalizedString ErrorExchangeConfigurationException
		{
			get
			{
				return new LocalizedString("ErrorExchangeConfigurationException", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019A8 RID: 6568
		// (get) Token: 0x06006757 RID: 26455 RVA: 0x00144247 File Offset: 0x00142447
		public static LocalizedString ErrorMailboxMoveInProgress
		{
			get
			{
				return new LocalizedString("ErrorMailboxMoveInProgress", "ExC760FF", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019A9 RID: 6569
		// (get) Token: 0x06006758 RID: 26456 RVA: 0x00144265 File Offset: 0x00142465
		public static LocalizedString ErrorInvalidValueForPropertyXmlData
		{
			get
			{
				return new LocalizedString("ErrorInvalidValueForPropertyXmlData", "Ex68ECD8", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019AA RID: 6570
		// (get) Token: 0x06006759 RID: 26457 RVA: 0x00144283 File Offset: 0x00142483
		public static LocalizedString RuleErrorDuplicatedPriority
		{
			get
			{
				return new LocalizedString("RuleErrorDuplicatedPriority", "ExCAE182", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019AB RID: 6571
		// (get) Token: 0x0600675A RID: 26458 RVA: 0x001442A1 File Offset: 0x001424A1
		public static LocalizedString ItemNotExistInPurgesFolder
		{
			get
			{
				return new LocalizedString("ItemNotExistInPurgesFolder", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019AC RID: 6572
		// (get) Token: 0x0600675B RID: 26459 RVA: 0x001442BF File Offset: 0x001424BF
		public static LocalizedString MessageMissingUserRolesForMailboxSearchRoleTypeApp
		{
			get
			{
				return new LocalizedString("MessageMissingUserRolesForMailboxSearchRoleTypeApp", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019AD RID: 6573
		// (get) Token: 0x0600675C RID: 26460 RVA: 0x001442DD File Offset: 0x001424DD
		public static LocalizedString ErrorInvalidNameForNameResolution
		{
			get
			{
				return new LocalizedString("ErrorInvalidNameForNameResolution", "Ex152E92", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019AE RID: 6574
		// (get) Token: 0x0600675D RID: 26461 RVA: 0x001442FB File Offset: 0x001424FB
		public static LocalizedString ErrorInvalidRecipientSubfilterOrder
		{
			get
			{
				return new LocalizedString("ErrorInvalidRecipientSubfilterOrder", "ExE7B6F1", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019AF RID: 6575
		// (get) Token: 0x0600675E RID: 26462 RVA: 0x00144319 File Offset: 0x00142519
		public static LocalizedString ErrorMailboxContainerGuidMismatch
		{
			get
			{
				return new LocalizedString("ErrorMailboxContainerGuidMismatch", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019B0 RID: 6576
		// (get) Token: 0x0600675F RID: 26463 RVA: 0x00144337 File Offset: 0x00142537
		public static LocalizedString ErrorInvalidId
		{
			get
			{
				return new LocalizedString("ErrorInvalidId", "Ex3371DA", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019B1 RID: 6577
		// (get) Token: 0x06006760 RID: 26464 RVA: 0x00144355 File Offset: 0x00142555
		public static LocalizedString ErrorNonPrimarySmtpAddress
		{
			get
			{
				return new LocalizedString("ErrorNonPrimarySmtpAddress", "Ex8143F7", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019B2 RID: 6578
		// (get) Token: 0x06006761 RID: 26465 RVA: 0x00144373 File Offset: 0x00142573
		public static LocalizedString ErrorSharedFolderSearchNotSupportedOnMultipleFolders
		{
			get
			{
				return new LocalizedString("ErrorSharedFolderSearchNotSupportedOnMultipleFolders", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019B3 RID: 6579
		// (get) Token: 0x06006762 RID: 26466 RVA: 0x00144391 File Offset: 0x00142591
		public static LocalizedString ErrorCalendarInvalidRecurrence
		{
			get
			{
				return new LocalizedString("ErrorCalendarInvalidRecurrence", "ExF37915", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019B4 RID: 6580
		// (get) Token: 0x06006763 RID: 26467 RVA: 0x001443AF File Offset: 0x001425AF
		public static LocalizedString ErrorInvalidOperationSaveReplyForwardToPublicFolder
		{
			get
			{
				return new LocalizedString("ErrorInvalidOperationSaveReplyForwardToPublicFolder", "ExBAF833", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019B5 RID: 6581
		// (get) Token: 0x06006764 RID: 26468 RVA: 0x001443CD File Offset: 0x001425CD
		public static LocalizedString ErrorInvalidOrderbyThenby
		{
			get
			{
				return new LocalizedString("ErrorInvalidOrderbyThenby", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019B6 RID: 6582
		// (get) Token: 0x06006765 RID: 26469 RVA: 0x001443EB File Offset: 0x001425EB
		public static LocalizedString ErrorInvalidRetentionTagTypeMismatch
		{
			get
			{
				return new LocalizedString("ErrorInvalidRetentionTagTypeMismatch", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019B7 RID: 6583
		// (get) Token: 0x06006766 RID: 26470 RVA: 0x00144409 File Offset: 0x00142609
		public static LocalizedString ErrorRequiredPropertyMissing
		{
			get
			{
				return new LocalizedString("ErrorRequiredPropertyMissing", "ExCBB0E4", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06006767 RID: 26471 RVA: 0x00144428 File Offset: 0x00142628
		public static LocalizedString ErrorParameterValueEmpty(string parameter)
		{
			return new LocalizedString("ErrorParameterValueEmpty", "", false, false, CoreResources.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x170019B8 RID: 6584
		// (get) Token: 0x06006768 RID: 26472 RVA: 0x00144457 File Offset: 0x00142657
		public static LocalizedString ErrorActiveDirectoryPermanentError
		{
			get
			{
				return new LocalizedString("ErrorActiveDirectoryPermanentError", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019B9 RID: 6585
		// (get) Token: 0x06006769 RID: 26473 RVA: 0x00144475 File Offset: 0x00142675
		public static LocalizedString IrmRmsError
		{
			get
			{
				return new LocalizedString("IrmRmsError", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019BA RID: 6586
		// (get) Token: 0x0600676A RID: 26474 RVA: 0x00144493 File Offset: 0x00142693
		public static LocalizedString ErrorNoPropertyUpdatesOrAttachmentsSpecified
		{
			get
			{
				return new LocalizedString("ErrorNoPropertyUpdatesOrAttachmentsSpecified", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019BB RID: 6587
		// (get) Token: 0x0600676B RID: 26475 RVA: 0x001444B1 File Offset: 0x001426B1
		public static LocalizedString ConversationActionNeedFlagForFlagAction
		{
			get
			{
				return new LocalizedString("ConversationActionNeedFlagForFlagAction", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019BC RID: 6588
		// (get) Token: 0x0600676C RID: 26476 RVA: 0x001444CF File Offset: 0x001426CF
		public static LocalizedString ErrorAttachmentNestLevelLimitExceeded
		{
			get
			{
				return new LocalizedString("ErrorAttachmentNestLevelLimitExceeded", "", false, false, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170019BD RID: 6589
		// (get) Token: 0x0600676D RID: 26477 RVA: 0x001444ED File Offset: 0x001426ED
		public static LocalizedString ErrorInvalidSmtpAddress
		{
			get
			{
				return new LocalizedString("ErrorInvalidSmtpAddress", "ExE9AFDE", false, true, CoreResources.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600676E RID: 26478 RVA: 0x0014450B File Offset: 0x0014270B
		public static LocalizedString GetLocalizedString(CoreResources.IDs key)
		{
			return new LocalizedString(CoreResources.stringIDs[(uint)key], CoreResources.ResourceManager, new object[0]);
		}

		// Token: 0x04003548 RID: 13640
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(778);

		// Token: 0x04003549 RID: 13641
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Services.CoreResources", typeof(CoreResources).GetTypeInfo().Assembly);

		// Token: 0x02000F76 RID: 3958
		public enum IDs : uint
		{
			// Token: 0x0400354B RID: 13643
			ErrorCannotSaveSentItemInArchiveFolder = 580413482U,
			// Token: 0x0400354C RID: 13644
			ErrorMissingUserIdInformation = 844193848U,
			// Token: 0x0400354D RID: 13645
			ErrorSearchConfigurationNotFound = 2524429953U,
			// Token: 0x0400354E RID: 13646
			ErrorCannotCreateContactInNonContactFolder = 1087525243U,
			// Token: 0x0400354F RID: 13647
			IrmFeatureDisabled = 1049269714U,
			// Token: 0x04003550 RID: 13648
			EwsProxyResponseTooBig = 1795180790U,
			// Token: 0x04003551 RID: 13649
			UpdateFavoritesUnableToDeleteFavoriteEntry = 3858003337U,
			// Token: 0x04003552 RID: 13650
			ErrorUpdateDelegatesFailed = 754853510U,
			// Token: 0x04003553 RID: 13651
			ErrorNoMailboxSpecifiedForSearchOperation = 1991349599U,
			// Token: 0x04003554 RID: 13652
			ErrorCannotApplyHoldOperationOnDG = 1455468349U,
			// Token: 0x04003555 RID: 13653
			ErrorInvalidExchangeImpersonationHeaderData = 1944820597U,
			// Token: 0x04003556 RID: 13654
			ExOrganizerCannotCallUpdateCalendarItem = 1504384645U,
			// Token: 0x04003557 RID: 13655
			IrmViewRightNotGranted = 2498511721U,
			// Token: 0x04003558 RID: 13656
			UpdateNonDraftItemInDumpsterNotAllowed = 93006148U,
			// Token: 0x04003559 RID: 13657
			ErrorIPGatewayNotFound = 2252936850U,
			// Token: 0x0400355A RID: 13658
			ErrorInvalidPropertyForOperation = 2517173182U,
			// Token: 0x0400355B RID: 13659
			ErrorNameResolutionNoResults = 574561672U,
			// Token: 0x0400355C RID: 13660
			ErrorInvalidItemForOperationCreateItemAttachment = 4225005690U,
			// Token: 0x0400355D RID: 13661
			Loading = 3599592070U,
			// Token: 0x0400355E RID: 13662
			ErrorItemSave = 2339310738U,
			// Token: 0x0400355F RID: 13663
			SubjectColon = 3413891549U,
			// Token: 0x04003560 RID: 13664
			ErrorInvalidItemForOperationExpandDL = 2181052460U,
			// Token: 0x04003561 RID: 13665
			MessageApplicationHasNoUserApplicationRoleAssigned = 1978429817U,
			// Token: 0x04003562 RID: 13666
			ErrorCalendarIsCancelledMessageSent = 3167358706U,
			// Token: 0x04003563 RID: 13667
			ErrorInvalidUserInfo = 1633083780U,
			// Token: 0x04003564 RID: 13668
			ErrorCalendarViewRangeTooBig = 2945703152U,
			// Token: 0x04003565 RID: 13669
			ErrorCalendarIsOrganizerForRemove = 495132450U,
			// Token: 0x04003566 RID: 13670
			ErrorInvalidRecipientSubfilterComparison = 244533303U,
			// Token: 0x04003567 RID: 13671
			ErrorPassingActingAsForUMConfig = 2476021338U,
			// Token: 0x04003568 RID: 13672
			ErrorUserWithoutFederatedProxyAddress = 3060608191U,
			// Token: 0x04003569 RID: 13673
			ErrorInvalidSendItemSaveSettings = 3825363766U,
			// Token: 0x0400356A RID: 13674
			ErrorWrongServerVersion = 3533302998U,
			// Token: 0x0400356B RID: 13675
			ErrorAssociatedTraversalDisallowedWithViewFilter = 3735354645U,
			// Token: 0x0400356C RID: 13676
			ErrorMailboxHoldIsNotPermitted = 4045771774U,
			// Token: 0x0400356D RID: 13677
			ErrorDuplicateSOAPHeader = 4197444273U,
			// Token: 0x0400356E RID: 13678
			ErrorInvalidValueForPropertyUserConfigurationName = 2744667914U,
			// Token: 0x0400356F RID: 13679
			ErrorIncorrectSchemaVersion = 3510999536U,
			// Token: 0x04003570 RID: 13680
			ErrorImpersonationRequiredForPush = 143544280U,
			// Token: 0x04003571 RID: 13681
			ErrorUnifiedMessagingPromptNotFound = 3135900505U,
			// Token: 0x04003572 RID: 13682
			ErrorCalendarMeetingRequestIsOutOfDate = 3227656327U,
			// Token: 0x04003573 RID: 13683
			MessageExtensionNotAllowedToCreateFAI = 1234709444U,
			// Token: 0x04003574 RID: 13684
			ErrorFolderCorrupt = 2966054199U,
			// Token: 0x04003575 RID: 13685
			ErrorManagedFolderNotFound = 2306155022U,
			// Token: 0x04003576 RID: 13686
			MessageManagementRoleHeaderCannotUseWithOtherHeaders = 1701713067U,
			// Token: 0x04003577 RID: 13687
			ErrorQueryFilterTooLong = 2285125742U,
			// Token: 0x04003578 RID: 13688
			MessageApplicationUnableActAsUser = 630435929U,
			// Token: 0x04003579 RID: 13689
			ErrorInvalidContactEmailIndex = 2886480659U,
			// Token: 0x0400357A RID: 13690
			MessageMalformedSoapHeader = 1248433804U,
			// Token: 0x0400357B RID: 13691
			ConversationItemQueryFailed = 3629808665U,
			// Token: 0x0400357C RID: 13692
			ErrorADOperation = 4038759526U,
			// Token: 0x0400357D RID: 13693
			ErrorCalendarIsOrganizerForAccept = 2633097826U,
			// Token: 0x0400357E RID: 13694
			ErrorCannotDeleteTaskOccurrence = 3049158008U,
			// Token: 0x0400357F RID: 13695
			ErrorTooManyContactsException = 2291046867U,
			// Token: 0x04003580 RID: 13696
			ErrorReadEventsFailed = 3577190220U,
			// Token: 0x04003581 RID: 13697
			descInvalidEIParameter = 1264061593U,
			// Token: 0x04003582 RID: 13698
			ErrorDuplicateLegacyDistinguishedName = 3584287689U,
			// Token: 0x04003583 RID: 13699
			MessageActingAsIsNotAValidEmailAddress = 2886782397U,
			// Token: 0x04003584 RID: 13700
			MessageInvalidServerVersionForJsonRequest = 1703911099U,
			// Token: 0x04003585 RID: 13701
			ErrorCalendarCannotMoveOrCopyOccurrence = 706889665U,
			// Token: 0x04003586 RID: 13702
			ErrorPeopleConnectionNotFound = 1747094812U,
			// Token: 0x04003587 RID: 13703
			ErrorCalendarMeetingIsOutOfDateResponseNotProcessedMessageSent = 3407017993U,
			// Token: 0x04003588 RID: 13704
			ErrorInvalidExcludesRestriction = 2122949970U,
			// Token: 0x04003589 RID: 13705
			ErrorMoreThanOneAccessModeSpecified = 789479259U,
			// Token: 0x0400358A RID: 13706
			ErrorCreateSubfolderAccessDenied = 4062262029U,
			// Token: 0x0400358B RID: 13707
			ErrorInvalidMailboxIdFormat = 1244610207U,
			// Token: 0x0400358C RID: 13708
			ErrorCalendarIsCancelledForAccept = 275979752U,
			// Token: 0x0400358D RID: 13709
			MessageApplicationRoleShouldPresentWhenUserRolePresent = 1596102169U,
			// Token: 0x0400358E RID: 13710
			ErrorInvalidUMSubscriberDataTimeoutValue = 3078968203U,
			// Token: 0x0400358F RID: 13711
			ErrorSearchTimeoutExpired = 602009568U,
			// Token: 0x04003590 RID: 13712
			descLocalServerConfigurationRetrievalFailed = 3471859246U,
			// Token: 0x04003591 RID: 13713
			ErrorInvalidContactEmailAddress = 3156759755U,
			// Token: 0x04003592 RID: 13714
			ErrorInvalidValueForPropertyStringArrayDictionaryKey = 1000223261U,
			// Token: 0x04003593 RID: 13715
			ErrorChangeKeyRequiredForWriteOperations = 3941855338U,
			// Token: 0x04003594 RID: 13716
			ErrorMissingEmailAddress = 4767764U,
			// Token: 0x04003595 RID: 13717
			ErrorFullSyncRequiredException = 932372376U,
			// Token: 0x04003596 RID: 13718
			ErrorADSessionFilter = 2058899143U,
			// Token: 0x04003597 RID: 13719
			ErrorDistinguishedUserNotSupported = 4170132598U,
			// Token: 0x04003598 RID: 13720
			ErrorCrossForestCallerNeedsADObject = 3681363043U,
			// Token: 0x04003599 RID: 13721
			ErrorSendMeetingInvitationsOrCancellationsRequired = 3422864683U,
			// Token: 0x0400359A RID: 13722
			RuleErrorDuplicatedOperationOnTheSameRule = 349902350U,
			// Token: 0x0400359B RID: 13723
			ErrorDeletePersonaOnInvalidFolder = 2151362503U,
			// Token: 0x0400359C RID: 13724
			ErrorCannotAddAggregatedAccountMailbox = 1407341086U,
			// Token: 0x0400359D RID: 13725
			ErrorExceededConnectionCount = 1108442436U,
			// Token: 0x0400359E RID: 13726
			ErrorFolderSavePropertyError = 1153968262U,
			// Token: 0x0400359F RID: 13727
			ErrorCannotUsePersonalContactsAsRecipientsOrAttendees = 590346139U,
			// Token: 0x040035A0 RID: 13728
			ErrorInvalidItemForForward = 4004906780U,
			// Token: 0x040035A1 RID: 13729
			ErrorChangeKeyRequired = 2538925974U,
			// Token: 0x040035A2 RID: 13730
			ErrorNotAcceptable = 147036963U,
			// Token: 0x040035A3 RID: 13731
			ErrorMessageTrackingNoSuchDomain = 2055932554U,
			// Token: 0x040035A4 RID: 13732
			ErrorTraversalNotAllowedWithoutQueryString = 1412463668U,
			// Token: 0x040035A5 RID: 13733
			ErrorOrganizationAccessBlocked = 3276585407U,
			// Token: 0x040035A6 RID: 13734
			ErrorInvalidNumberOfMailboxSearch = 3169826345U,
			// Token: 0x040035A7 RID: 13735
			ErrorCreateManagedFolderPartialCompletion = 727968988U,
			// Token: 0x040035A8 RID: 13736
			UpdateFavoritesUnableToRenameFavorite = 1486053208U,
			// Token: 0x040035A9 RID: 13737
			ErrorActiveDirectoryTransientError = 170634829U,
			// Token: 0x040035AA RID: 13738
			ErrorInvalidSubscriptionRequestAllFoldersWithFolderIds = 2956059769U,
			// Token: 0x040035AB RID: 13739
			ErrorInvalidOperationSendMeetingInvitationCancellationForPublicFolderItem = 2990730164U,
			// Token: 0x040035AC RID: 13740
			ErrorIrresolvableConflict = 593146268U,
			// Token: 0x040035AD RID: 13741
			ErrorInvalidItemForReplyAll = 479697568U,
			// Token: 0x040035AE RID: 13742
			ErrorPhoneNumberNotDialable = 4266358168U,
			// Token: 0x040035AF RID: 13743
			ErrorInvalidInternetHeaderChildNodes = 4187771604U,
			// Token: 0x040035B0 RID: 13744
			ErrorInvalidExpressionTypeForSubFilter = 3459701324U,
			// Token: 0x040035B1 RID: 13745
			MessageResolveNamesNotSufficientPermissionsToPrivateDLMember = 3931903304U,
			// Token: 0x040035B2 RID: 13746
			ErrorCannotSetNonCalendarPermissionOnCalendarFolder = 924348366U,
			// Token: 0x040035B3 RID: 13747
			ErrorParentFolderIdRequired = 2126704764U,
			// Token: 0x040035B4 RID: 13748
			ErrorEventNotFound = 1854021767U,
			// Token: 0x040035B5 RID: 13749
			ErrorVoiceMailNotImplemented = 3731723330U,
			// Token: 0x040035B6 RID: 13750
			ErrorDeleteDistinguishedFolder = 3448951775U,
			// Token: 0x040035B7 RID: 13751
			ErrorNoPermissionToSearchOrHoldMailbox = 2354781453U,
			// Token: 0x040035B8 RID: 13752
			ErrorExchangeApplicationNotEnabled = 3213161861U,
			// Token: 0x040035B9 RID: 13753
			ErrorResolveNamesInvalidFolderType = 1634698783U,
			// Token: 0x040035BA RID: 13754
			ErrorExceededFindCountLimit = 2226715912U,
			// Token: 0x040035BB RID: 13755
			MessageExtensionAccessActAsMailboxOnly = 2771555298U,
			// Token: 0x040035BC RID: 13756
			ErrorPasswordChangeRequired = 3093510304U,
			// Token: 0x040035BD RID: 13757
			ErrorInvalidManagedFolderProperty = 254805997U,
			// Token: 0x040035BE RID: 13758
			ErrorInvalidIdMalformedEwsLegacyIdFormat = 1565810069U,
			// Token: 0x040035BF RID: 13759
			ErrorSchemaViolation = 1538229710U,
			// Token: 0x040035C0 RID: 13760
			MessageInvalidMailboxContactAddressNotFound = 478602263U,
			// Token: 0x040035C1 RID: 13761
			ErrorInvalidIndexedPagingParameters = 1293185920U,
			// Token: 0x040035C2 RID: 13762
			ErrorUnsupportedPathForQuery = 361161677U,
			// Token: 0x040035C3 RID: 13763
			ErrorInvalidOperationDelegationAssociatedItem = 3721795127U,
			// Token: 0x040035C4 RID: 13764
			ErrorRemoteUserMailboxMustSpecifyExplicitLocalMailbox = 4256465912U,
			// Token: 0x040035C5 RID: 13765
			ErrorNoDestinationCASDueToVersionMismatch = 1473115829U,
			// Token: 0x040035C6 RID: 13766
			ErrorInvalidValueForPropertyBinaryData = 1805735881U,
			// Token: 0x040035C7 RID: 13767
			ErrorNotDelegate = 2410622290U,
			// Token: 0x040035C8 RID: 13768
			ErrorCalendarInvalidDayForTimeChangePattern = 1829860367U,
			// Token: 0x040035C9 RID: 13769
			ErrorInvalidPullSubscriptionId = 1988987848U,
			// Token: 0x040035CA RID: 13770
			ErrorCannotCopyPublicFolderRoot = 2571138389U,
			// Token: 0x040035CB RID: 13771
			MessageOperationRequiresUserContext = 1967767132U,
			// Token: 0x040035CC RID: 13772
			ErrorPromptPublishingOperationFailed = 2217412679U,
			// Token: 0x040035CD RID: 13773
			ErrorInvalidFractionalPagingParameters = 2620420056U,
			// Token: 0x040035CE RID: 13774
			ErrorPublicFolderMailboxDiscoveryFailed = 4236561690U,
			// Token: 0x040035CF RID: 13775
			ErrorUnableToRemoveImContactFromGroup = 3162641137U,
			// Token: 0x040035D0 RID: 13776
			ErrorSendMeetingCancellationsRequired = 1549704648U,
			// Token: 0x040035D1 RID: 13777
			MessageRecipientsArrayMustNotBeEmpty = 2011475698U,
			// Token: 0x040035D2 RID: 13778
			ErrorInvalidItemForOperationTentative = 757111886U,
			// Token: 0x040035D3 RID: 13779
			ErrorInvalidReferenceItem = 2519519915U,
			// Token: 0x040035D4 RID: 13780
			IrmReachNotConfigured = 1314141112U,
			// Token: 0x040035D5 RID: 13781
			ErrorMimeContentInvalidBase64String = 3907819958U,
			// Token: 0x040035D6 RID: 13782
			ErrorSentTaskRequestUpdate = 364289873U,
			// Token: 0x040035D7 RID: 13783
			ErrorFoundSyncRequestForNonAggregatedAccount = 1527356366U,
			// Token: 0x040035D8 RID: 13784
			MessagePropertyIsDeprecatedForThisVersion = 3384523424U,
			// Token: 0x040035D9 RID: 13785
			ErrorInvalidOperationContactsViewAssociatedItem = 3954262679U,
			// Token: 0x040035DA RID: 13786
			ErrorServerBusy = 3655513582U,
			// Token: 0x040035DB RID: 13787
			ConversationActionNeedRetentionPolicyTypeForSetRetentionPolicy = 3967405104U,
			// Token: 0x040035DC RID: 13788
			ErrorCannotDeletePublicFolderRoot = 2671356913U,
			// Token: 0x040035DD RID: 13789
			ErrorImGroupDisplayNameAlreadyExists = 3809605342U,
			// Token: 0x040035DE RID: 13790
			NoServer = 384737734U,
			// Token: 0x040035DF RID: 13791
			ErrorInvalidImDistributionGroupSmtpAddress = 948947750U,
			// Token: 0x040035E0 RID: 13792
			ErrorSubscriptionDelegateAccessNotSupported = 3640136739U,
			// Token: 0x040035E1 RID: 13793
			RuleErrorItemIsNotTemplate = 273046712U,
			// Token: 0x040035E2 RID: 13794
			ErrorCannotSetPermissionUnknownEntries = 2549623104U,
			// Token: 0x040035E3 RID: 13795
			MessageIdOrTokenTypeNotFound = 357277919U,
			// Token: 0x040035E4 RID: 13796
			ErrorLocationServicesDisabled = 2451443999U,
			// Token: 0x040035E5 RID: 13797
			MessageNotSupportedApplicationRole = 3773912990U,
			// Token: 0x040035E6 RID: 13798
			ErrorPublicFolderSyncException = 2636256287U,
			// Token: 0x040035E7 RID: 13799
			ErrorCalendarIsDelegatedForDecline = 1448063240U,
			// Token: 0x040035E8 RID: 13800
			ErrorUnsupportedODataRequest = 435342351U,
			// Token: 0x040035E9 RID: 13801
			ErrorDeepTraversalsNotAllowedOnPublicFolders = 4039615479U,
			// Token: 0x040035EA RID: 13802
			MessageCouldNotFindWeatherLocationsForSearchString = 2521448946U,
			// Token: 0x040035EB RID: 13803
			ErrorInvalidPropertyForSortBy = 2566235088U,
			// Token: 0x040035EC RID: 13804
			MessageCalendarUnableToGetAssociatedCalendarItem = 3823874672U,
			// Token: 0x040035ED RID: 13805
			ErrorSortByPropertyIsNotFoundOrNotSupported = 2841035169U,
			// Token: 0x040035EE RID: 13806
			ErrorNotSupportedSharingMessage = 3991730990U,
			// Token: 0x040035EF RID: 13807
			ErrorMissingInformationReferenceItemId = 1492851991U,
			// Token: 0x040035F0 RID: 13808
			ErrorInvalidSIPUri = 1825729465U,
			// Token: 0x040035F1 RID: 13809
			ErrorInvalidCompleteDateOutOfRange = 3371984686U,
			// Token: 0x040035F2 RID: 13810
			ErrorUnifiedMessagingDialPlanNotFound = 226219872U,
			// Token: 0x040035F3 RID: 13811
			MessageRecipientMustHaveRoutingType = 2688988465U,
			// Token: 0x040035F4 RID: 13812
			MessageResolveNamesNotSufficientPermissionsToPrivateDL = 3793759002U,
			// Token: 0x040035F5 RID: 13813
			MessageMissingUserRolesForOrganizationConfigurationRoleTypeApp = 1964352390U,
			// Token: 0x040035F6 RID: 13814
			ErrorInvalidUserSid = 368663972U,
			// Token: 0x040035F7 RID: 13815
			ErrorInvalidRecipientSubfilter = 1954916878U,
			// Token: 0x040035F8 RID: 13816
			ErrorSuffixSearchNotAllowed = 248293106U,
			// Token: 0x040035F9 RID: 13817
			ErrorUnifiedMessagingReportDataNotFound = 283029019U,
			// Token: 0x040035FA RID: 13818
			UpdateFavoritesFolderAlreadyInFavorites = 1502984804U,
			// Token: 0x040035FB RID: 13819
			MessageManagementRoleHeaderNotSupportedForOfficeExtension = 2329012714U,
			// Token: 0x040035FC RID: 13820
			OneDriveProAttachmentDataProviderName = 2614511650U,
			// Token: 0x040035FD RID: 13821
			ErrorCalendarInvalidAttributeValue = 2961161516U,
			// Token: 0x040035FE RID: 13822
			MessageInvalidRecurrenceFormat = 3854873845U,
			// Token: 0x040035FF RID: 13823
			ErrorInvalidAppApiVersionSupported = 2449079760U,
			// Token: 0x04003600 RID: 13824
			ErrorInvalidManagedFolderSize = 4227165423U,
			// Token: 0x04003601 RID: 13825
			ErrorTokenSerializationDenied = 3279473776U,
			// Token: 0x04003602 RID: 13826
			ErrorInvalidRequest = 3784063568U,
			// Token: 0x04003603 RID: 13827
			ErrorSubscriptionUnsubscribed = 2041209694U,
			// Token: 0x04003604 RID: 13828
			ErrorInvalidItemForOperationCancelItem = 1426183245U,
			// Token: 0x04003605 RID: 13829
			IrmCorruptProtectedMessage = 684230472U,
			// Token: 0x04003606 RID: 13830
			ErrorCalendarIsGroupMailboxForAccept = 920557414U,
			// Token: 0x04003607 RID: 13831
			ErrorMailboxSearchFailed = 2933656041U,
			// Token: 0x04003608 RID: 13832
			ErrorMailboxConfiguration = 1188755898U,
			// Token: 0x04003609 RID: 13833
			RuleErrorNotSettable = 382988367U,
			// Token: 0x0400360A RID: 13834
			ErrorCopyPublicFolderNotSupported = 4177991609U,
			// Token: 0x0400360B RID: 13835
			ErrorInvalidWatermark = 3312780993U,
			// Token: 0x0400360C RID: 13836
			ErrorActingAsUserNotFound = 310545492U,
			// Token: 0x0400360D RID: 13837
			ErrorDelegateMissingConfiguration = 3438146603U,
			// Token: 0x0400360E RID: 13838
			MessageCalendarUnableToUpdateAssociatedCalendarItem = 1562596869U,
			// Token: 0x0400360F RID: 13839
			MessageMissingMailboxOwnerEmailAddress = 2555117076U,
			// Token: 0x04003610 RID: 13840
			ErrorSentMeetingRequestUpdate = 3080514177U,
			// Token: 0x04003611 RID: 13841
			descInvalidTimeZone = 2141183275U,
			// Token: 0x04003612 RID: 13842
			ErrorInvalidOperationDisposalTypeAssociatedItem = 1733132070U,
			// Token: 0x04003613 RID: 13843
			UpdateFavoritesMoveTypeMustBeSet = 59180037U,
			// Token: 0x04003614 RID: 13844
			ConversationActionNeedDeleteTypeForSetDeleteAction = 436889836U,
			// Token: 0x04003615 RID: 13845
			ErrorInvalidProxySecurityContext = 3616451054U,
			// Token: 0x04003616 RID: 13846
			ErrorInvalidValueForProperty = 570782166U,
			// Token: 0x04003617 RID: 13847
			ErrorInvalidRestriction = 1904020973U,
			// Token: 0x04003618 RID: 13848
			RuleErrorInvalidAddress = 2909492621U,
			// Token: 0x04003619 RID: 13849
			RuleErrorSizeLessThanZero = 599310039U,
			// Token: 0x0400361A RID: 13850
			Orange = 3664749912U,
			// Token: 0x0400361B RID: 13851
			ErrorRecipientTypeNotSupported = 3611326890U,
			// Token: 0x0400361C RID: 13852
			ErrorInvalidIdTooManyAttachmentLevels = 3632066599U,
			// Token: 0x0400361D RID: 13853
			ErrorExportRemoteArchiveItemsFailed = 523664899U,
			// Token: 0x0400361E RID: 13854
			ErrorCannotSendMessageFromPublicFolder = 1303377787U,
			// Token: 0x0400361F RID: 13855
			MessageInsufficientPermissions = 3694049238U,
			// Token: 0x04003620 RID: 13856
			MessageCorrelationFailed = 2611688746U,
			// Token: 0x04003621 RID: 13857
			ErrorNoMailboxSpecifiedForHoldOperation = 3819492078U,
			// Token: 0x04003622 RID: 13858
			ErrorTimeZone = 610144303U,
			// Token: 0x04003623 RID: 13859
			ErrorSendAsDenied = 4260694481U,
			// Token: 0x04003624 RID: 13860
			MessageSingleOrRecurringCalendarItemExpected = 4292861306U,
			// Token: 0x04003625 RID: 13861
			ErrorSearchQueryCannotBeEmpty = 2226875331U,
			// Token: 0x04003626 RID: 13862
			ErrorMultipleMailboxesCurrentlyNotSupported = 4136809189U,
			// Token: 0x04003627 RID: 13863
			ErrorParentFolderNotFound = 4217637937U,
			// Token: 0x04003628 RID: 13864
			ErrorDelegateCannotAddOwner = 143488278U,
			// Token: 0x04003629 RID: 13865
			MessageCalendarInsufficientPermissionsToMoveMeetingCancellation = 3869946114U,
			// Token: 0x0400362A RID: 13866
			ErrorImpersonateUserDenied = 73255155U,
			// Token: 0x0400362B RID: 13867
			ErrorReadReceiptNotPending = 2875907804U,
			// Token: 0x0400362C RID: 13868
			ErrorInvalidRetentionTagIdGuid = 1676008137U,
			// Token: 0x0400362D RID: 13869
			ErrorCannotCreateTaskInNonTaskFolder = 379663703U,
			// Token: 0x0400362E RID: 13870
			MessageNonExistentMailboxNoSmtpAddress = 4074099229U,
			// Token: 0x0400362F RID: 13871
			ErrorSchemaValidation = 2523006528U,
			// Token: 0x04003630 RID: 13872
			MessageManagementRoleHeaderValueNotApplicable = 3264410200U,
			// Token: 0x04003631 RID: 13873
			MessageInvalidRuleVersion = 2540872182U,
			// Token: 0x04003632 RID: 13874
			ErrorUnsupportedMimeConversion = 1174046717U,
			// Token: 0x04003633 RID: 13875
			ErrorCannotMovePublicFolderItemOnDelete = 463452338U,
			// Token: 0x04003634 RID: 13876
			ErrorInvalidItemForOperationArchiveItem = 966537145U,
			// Token: 0x04003635 RID: 13877
			ErrorInvalidSearchQuerySyntax = 3021008902U,
			// Token: 0x04003636 RID: 13878
			ErrorInvalidValueForCountSystemQueryOption = 4179066588U,
			// Token: 0x04003637 RID: 13879
			ErrorFolderSaveFailed = 1067402124U,
			// Token: 0x04003638 RID: 13880
			MessageTargetMailboxNotInRoleScope = 2435663882U,
			// Token: 0x04003639 RID: 13881
			ErrorInvalidSearchId = 2179607746U,
			// Token: 0x0400363A RID: 13882
			ErrorInvalidOperationSyncFolderHierarchyForPublicFolder = 2674546476U,
			// Token: 0x0400363B RID: 13883
			ErrorItemCorrupt = 2624402344U,
			// Token: 0x0400363C RID: 13884
			ErrorServerTemporaryUnavailable = 3120707856U,
			// Token: 0x0400363D RID: 13885
			ErrorCannotArchiveCalendarContactTaskFolderException = 2786380669U,
			// Token: 0x0400363E RID: 13886
			ErrorInvalidItemForOperationSendItem = 4123291671U,
			// Token: 0x0400363F RID: 13887
			ErrorAggregatedAccountAlreadyExists = 68528320U,
			// Token: 0x04003640 RID: 13888
			ErrorInvalidServerVersion = 109614196U,
			// Token: 0x04003641 RID: 13889
			ErrorGroupingNonNullWithSuggestionsViewFilter = 1487884331U,
			// Token: 0x04003642 RID: 13890
			MessageInvalidMailboxNotPrivateDL = 1958477060U,
			// Token: 0x04003643 RID: 13891
			ErrorItemPropertyRequestFailed = 1272021886U,
			// Token: 0x04003644 RID: 13892
			ConversationActionNeedDestinationFolderForCopyAction = 1706062739U,
			// Token: 0x04003645 RID: 13893
			ErrorLocationServicesRequestFailed = 2653243941U,
			// Token: 0x04003646 RID: 13894
			UnrecognizedDistinguishedFolderName = 220777420U,
			// Token: 0x04003647 RID: 13895
			ErrorInvalidSubfilterTypeNotRecipientType = 559784827U,
			// Token: 0x04003648 RID: 13896
			ErrorInvalidPropertySet = 1701761470U,
			// Token: 0x04003649 RID: 13897
			UpdateFavoritesFolderCannotBeNull = 3625531057U,
			// Token: 0x0400364A RID: 13898
			ErrorCannotRemoveAggregatedAccountFromList = 1326676491U,
			// Token: 0x0400364B RID: 13899
			ErrorProxyTokenExpired = 3699987394U,
			// Token: 0x0400364C RID: 13900
			ErrorCannotCreateCalendarItemInNonCalendarFolder = 3564002022U,
			// Token: 0x0400364D RID: 13901
			ErrorInvalidOperationGroupByAssociatedItem = 3732945645U,
			// Token: 0x0400364E RID: 13902
			MessageCalendarUnableToCreateAssociatedCalendarItem = 2890836210U,
			// Token: 0x0400364F RID: 13903
			ErrorMultiLegacyMailboxAccess = 896367800U,
			// Token: 0x04003650 RID: 13904
			ErrorUnifiedMailboxAlreadyExists = 3392207806U,
			// Token: 0x04003651 RID: 13905
			ErrorInvalidPropertyAppend = 3619206730U,
			// Token: 0x04003652 RID: 13906
			ErrorObjectTypeChanged = 4261845811U,
			// Token: 0x04003653 RID: 13907
			ErrorSearchableObjectNotFound = 4252616528U,
			// Token: 0x04003654 RID: 13908
			ErrorEndTimeMustBeGreaterThanStartTime = 2498507918U,
			// Token: 0x04003655 RID: 13909
			ErrorInvalidFederatedOrganizationId = 765833303U,
			// Token: 0x04003656 RID: 13910
			MessageExtensionNotAllowedToUpdateFAI = 1583798271U,
			// Token: 0x04003657 RID: 13911
			ErrorValueOutOfRange = 1335290147U,
			// Token: 0x04003658 RID: 13912
			ErrorNotEnoughMemory = 3719196410U,
			// Token: 0x04003659 RID: 13913
			ErrorInvalidExtendedPropertyValue = 3635256568U,
			// Token: 0x0400365A RID: 13914
			ErrorMoveCopyFailed = 2524108663U,
			// Token: 0x0400365B RID: 13915
			GetClientExtensionTokenFailed = 1985973150U,
			// Token: 0x0400365C RID: 13916
			ErrorVirusDetected = 3705244005U,
			// Token: 0x0400365D RID: 13917
			ErrorInvalidVotingResponse = 671866695U,
			// Token: 0x0400365E RID: 13918
			RuleErrorInboxRulesValidationError = 2296308088U,
			// Token: 0x0400365F RID: 13919
			ErrorInvalidIdMonikerTooLong = 1897020671U,
			// Token: 0x04003660 RID: 13920
			ErrorMultipleSearchRootsDisallowedWithSearchContext = 111518940U,
			// Token: 0x04003661 RID: 13921
			ErrorUserNotUnifiedMessagingEnabled = 4142344047U,
			// Token: 0x04003662 RID: 13922
			ErrorCannotMovePublicFolderToPrivateMailbox = 3206878473U,
			// Token: 0x04003663 RID: 13923
			ConversationActionAlwaysMoveNoPublicFolder = 751424501U,
			// Token: 0x04003664 RID: 13924
			ErrorCallerIsInvalidADAccount = 1834319386U,
			// Token: 0x04003665 RID: 13925
			ErrorNoDestinationCASDueToSSLRequirements = 3319799507U,
			// Token: 0x04003666 RID: 13926
			ErrorInternalServerTransientError = 3995283118U,
			// Token: 0x04003667 RID: 13927
			ErrorInvalidParentFolder = 3659985571U,
			// Token: 0x04003668 RID: 13928
			ErrorArchiveFolderPathCreation = 2565659540U,
			// Token: 0x04003669 RID: 13929
			MessageCalendarInsufficientPermissionsToMoveItem = 4066246803U,
			// Token: 0x0400366A RID: 13930
			ErrorMessagePerFolderCountReceiveQuotaExceeded = 2791864679U,
			// Token: 0x0400366B RID: 13931
			ErrorDateTimeNotInUTC = 2643283981U,
			// Token: 0x0400366C RID: 13932
			ErrorInvalidAttachmentSubfilter = 2798800298U,
			// Token: 0x0400366D RID: 13933
			ErrorUserConfigurationDictionaryNotExist = 2214456911U,
			// Token: 0x0400366E RID: 13934
			FromColon = 2918743951U,
			// Token: 0x0400366F RID: 13935
			ErrorInvalidSubscriptionRequestNoFolderIds = 2362895530U,
			// Token: 0x04003670 RID: 13936
			ErrorCallerIsComputerAccount = 1115854773U,
			// Token: 0x04003671 RID: 13937
			ErrorDeleteItemsFailed = 69571280U,
			// Token: 0x04003672 RID: 13938
			ErrorNotApplicableOutsideOfDatacenter = 2014273875U,
			// Token: 0x04003673 RID: 13939
			RuleErrorOutlookRuleBlobExists = 526527128U,
			// Token: 0x04003674 RID: 13940
			descInvalidOofRequestPublicFolder = 2843974690U,
			// Token: 0x04003675 RID: 13941
			ErrorMailboxIsNotPartOfAggregatedMailboxes = 21808504U,
			// Token: 0x04003676 RID: 13942
			ErrorInvalidRetentionTagNone = 2043815785U,
			// Token: 0x04003677 RID: 13943
			MessageInvalidRoleTypeString = 2448725207U,
			// Token: 0x04003678 RID: 13944
			MessageInvalidMailboxRecipientNotFoundInActiveDirectory = 2343198056U,
			// Token: 0x04003679 RID: 13945
			ErrorNoSyncRequestsMatchedSpecifiedEmailAddress = 402485116U,
			// Token: 0x0400367A RID: 13946
			ErrorInvalidDestinationFolderForPostItem = 2999374145U,
			// Token: 0x0400367B RID: 13947
			ErrorGetRemoteArchiveFolderFailed = 377617137U,
			// Token: 0x0400367C RID: 13948
			RightsManagementMailboxOnlySupport = 3410698111U,
			// Token: 0x0400367D RID: 13949
			ErrorMissingItemForCreateItemAttachment = 122085112U,
			// Token: 0x0400367E RID: 13950
			ErrorFindRemoteArchiveFolderFailed = 4160418372U,
			// Token: 0x0400367F RID: 13951
			ErrorCalendarFolderIsInvalidForCalendarView = 2989650895U,
			// Token: 0x04003680 RID: 13952
			ErrorFindConversationNotSupportedForPublicFolders = 3359997542U,
			// Token: 0x04003681 RID: 13953
			ErrorUserConfigurationBinaryDataNotExist = 969219158U,
			// Token: 0x04003682 RID: 13954
			DefaultHtmlAttachmentHrefText = 1063299331U,
			// Token: 0x04003683 RID: 13955
			Green = 3510846499U,
			// Token: 0x04003684 RID: 13956
			ErrorItemNotFound = 4005418156U,
			// Token: 0x04003685 RID: 13957
			ErrorCannotEmptyFolder = 2838198776U,
			// Token: 0x04003686 RID: 13958
			Yellow = 777220966U,
			// Token: 0x04003687 RID: 13959
			ErrorInvalidSubscription = 1967895810U,
			// Token: 0x04003688 RID: 13960
			ErrorSchemaValidationColon = 4281412187U,
			// Token: 0x04003689 RID: 13961
			ErrorDelegateNoUser = 707372475U,
			// Token: 0x0400368A RID: 13962
			RuleErrorMissingRangeValue = 107796140U,
			// Token: 0x0400368B RID: 13963
			MessageWebMethodUnavailable = 2554577046U,
			// Token: 0x0400368C RID: 13964
			ErrorUnsupportedQueryFilter = 395078619U,
			// Token: 0x0400368D RID: 13965
			ErrorCannotMovePublicFolderOnDelete = 968334519U,
			// Token: 0x0400368E RID: 13966
			ErrorAccessModeSpecified = 3314483401U,
			// Token: 0x0400368F RID: 13967
			ErrorInvalidPhotoSize = 654139516U,
			// Token: 0x04003690 RID: 13968
			ErrorMultipleMailboxSearchNotSupported = 2656700117U,
			// Token: 0x04003691 RID: 13969
			MessageManagementRoleHeaderNotSupportedForPartnerIdentity = 728324719U,
			// Token: 0x04003692 RID: 13970
			ConversationActionInvalidFolderType = 1457631314U,
			// Token: 0x04003693 RID: 13971
			ErrorUnsupportedSubFilter = 259054457U,
			// Token: 0x04003694 RID: 13972
			ErrorInvalidComplianceId = 1408093181U,
			// Token: 0x04003695 RID: 13973
			ErrorCalendarCannotUpdateDeletedItem = 3843271914U,
			// Token: 0x04003696 RID: 13974
			ErrorInvalidOperationDistinguishedGroupByAssociatedItem = 3782996725U,
			// Token: 0x04003697 RID: 13975
			ErrorInvalidDelegatePermission = 3537364541U,
			// Token: 0x04003698 RID: 13976
			ErrorInternalServerError = 594155080U,
			// Token: 0x04003699 RID: 13977
			ErrorNoPublicFolderServerAvailable = 2356362688U,
			// Token: 0x0400369A RID: 13978
			ErrorInvalidPhoneCallId = 3978299680U,
			// Token: 0x0400369B RID: 13979
			ErrorInvalidGetSharingFolderRequest = 1793222072U,
			// Token: 0x0400369C RID: 13980
			ErrorCannotResolveOrganizationName = 2927988853U,
			// Token: 0x0400369D RID: 13981
			ErrorUnsupportedCulture = 809187661U,
			// Token: 0x0400369E RID: 13982
			ErrorInvalidChangeKey = 865206910U,
			// Token: 0x0400369F RID: 13983
			ErrorMimeContentConversionFailed = 3846347532U,
			// Token: 0x040036A0 RID: 13984
			ErrorResolveNamesOnlyOneContactsFolderAllowed = 2683464521U,
			// Token: 0x040036A1 RID: 13985
			ErrorInvalidSchemaVersionForMailboxVersion = 901489999U,
			// Token: 0x040036A2 RID: 13986
			ErrorInvalidRequestQuotaExceeded = 2012012473U,
			// Token: 0x040036A3 RID: 13987
			MessageTokenRequestUnauthorized = 768426321U,
			// Token: 0x040036A4 RID: 13988
			MessageUserRoleNotApplicableForAppOnlyToken = 3910111167U,
			// Token: 0x040036A5 RID: 13989
			ErrorInvalidValueForPropertyKeyValueConversion = 828992378U,
			// Token: 0x040036A6 RID: 13990
			ErrorInvalidRetentionTagInheritance = 3769371271U,
			// Token: 0x040036A7 RID: 13991
			Conversation = 710925581U,
			// Token: 0x040036A8 RID: 13992
			ErrorCannotCreateUnifiedMailbox = 1338511205U,
			// Token: 0x040036A9 RID: 13993
			ErrorMailTipsDisabled = 1897429247U,
			// Token: 0x040036AA RID: 13994
			ErrorMissingItemIdForCreateItemAttachment = 4222588379U,
			// Token: 0x040036AB RID: 13995
			ErrorInvalidMailbox = 1762041369U,
			// Token: 0x040036AC RID: 13996
			ErrorDelegateValidationFailed = 4097108255U,
			// Token: 0x040036AD RID: 13997
			ErrorUserPromptNeeded = 2715027708U,
			// Token: 0x040036AE RID: 13998
			RuleErrorMissingAction = 1898482716U,
			// Token: 0x040036AF RID: 13999
			ErrorApplyConversationActionFailed = 706596508U,
			// Token: 0x040036B0 RID: 14000
			ErrorInsufficientResources = 60783832U,
			// Token: 0x040036B1 RID: 14001
			ErrorActingAsRequired = 905739673U,
			// Token: 0x040036B2 RID: 14002
			ErrorCalendarInvalidDayForWeeklyRecurrence = 2681298929U,
			// Token: 0x040036B3 RID: 14003
			ErrorMissingInformationEmailAddress = 549150802U,
			// Token: 0x040036B4 RID: 14004
			UpdateFavoritesFavoriteNotFound = 4019544117U,
			// Token: 0x040036B5 RID: 14005
			ErrorCalendarDurationIsTooLong = 2484699530U,
			// Token: 0x040036B6 RID: 14006
			ErrorNoRespondingCASInDestinationSite = 4252309617U,
			// Token: 0x040036B7 RID: 14007
			ErrorInvalidRecipients = 388443881U,
			// Token: 0x040036B8 RID: 14008
			ErrorAppendBodyTypeMismatch = 269577600U,
			// Token: 0x040036B9 RID: 14009
			ErrorDistributionListMemberNotExist = 514021796U,
			// Token: 0x040036BA RID: 14010
			ErrorRequestTimeout = 3285224352U,
			// Token: 0x040036BB RID: 14011
			MessageApplicationHasNoRoleAssginedWhichUserHas = 3607262778U,
			// Token: 0x040036BC RID: 14012
			ErrorArchiveMailboxGetConversationFailed = 3668888236U,
			// Token: 0x040036BD RID: 14013
			ErrorClientIntentNotFound = 2851949310U,
			// Token: 0x040036BE RID: 14014
			ErrorNonExistentMailbox = 2489326695U,
			// Token: 0x040036BF RID: 14015
			ErrorVirusMessageDeleted = 1164605313U,
			// Token: 0x040036C0 RID: 14016
			ErrorCannotFindUnifiedMailbox = 175403818U,
			// Token: 0x040036C1 RID: 14017
			ErrorUnifiedMailboxSupportedOnlyWithMicrosoftAccount = 1505256501U,
			// Token: 0x040036C2 RID: 14018
			GroupMailboxCreationFailed = 2833024077U,
			// Token: 0x040036C3 RID: 14019
			ErrorInvalidSearchQueryLength = 1233823477U,
			// Token: 0x040036C4 RID: 14020
			ErrorCalendarInvalidPropertyState = 391940363U,
			// Token: 0x040036C5 RID: 14021
			ErrorAddDelegatesFailed = 1850561764U,
			// Token: 0x040036C6 RID: 14022
			CcColon = 3496891301U,
			// Token: 0x040036C7 RID: 14023
			ErrorCrossSiteRequest = 1062691260U,
			// Token: 0x040036C8 RID: 14024
			ErrorPublicFolderUserMustHaveMailbox = 565625999U,
			// Token: 0x040036C9 RID: 14025
			ErrorMessageTrackingTransientError = 3399410586U,
			// Token: 0x040036CA RID: 14026
			ErrorToFolderNotFound = 1027490726U,
			// Token: 0x040036CB RID: 14027
			ErrorDeleteUnifiedMessagingPromptFailed = 1637412134U,
			// Token: 0x040036CC RID: 14028
			UpdateFavoritesUnableToMoveFavorite = 3914315493U,
			// Token: 0x040036CD RID: 14029
			ErrorPeopleConnectionNoToken = 3141449171U,
			// Token: 0x040036CE RID: 14030
			ErrorCannotSpecifySearchFolderAsSourceFolder = 3848937923U,
			// Token: 0x040036CF RID: 14031
			ErrorEmailAddressMismatch = 933080956U,
			// Token: 0x040036D0 RID: 14032
			ErrorUserConfigurationXmlDataNotExist = 2419720676U,
			// Token: 0x040036D1 RID: 14033
			ErrorUnifiedMessagingRequestFailed = 2346704662U,
			// Token: 0x040036D2 RID: 14034
			ErrorCreateItemAccessDenied = 1075303082U,
			// Token: 0x040036D3 RID: 14035
			RuleErrorFolderDoesNotExist = 2887245343U,
			// Token: 0x040036D4 RID: 14036
			ErrorInvalidImContactId = 3485828594U,
			// Token: 0x040036D5 RID: 14037
			ErrorNoPropertyTagForCustomProperties = 3969305989U,
			// Token: 0x040036D6 RID: 14038
			SentTime = 2677919833U,
			// Token: 0x040036D7 RID: 14039
			MessageNonExistentMailboxGuid = 3279543955U,
			// Token: 0x040036D8 RID: 14040
			ErrorMaxRequestedUnifiedGroupsSetsExceeded = 216781884U,
			// Token: 0x040036D9 RID: 14041
			ErrorInvalidAppSchemaVersionSupported = 3555230765U,
			// Token: 0x040036DA RID: 14042
			ErrorInvalidLogonType = 3522975510U,
			// Token: 0x040036DB RID: 14043
			MessageActAsUserRequiredForSuchApplicationRole = 131857255U,
			// Token: 0x040036DC RID: 14044
			ErrorCalendarOutOfRange = 3773356320U,
			// Token: 0x040036DD RID: 14045
			ErrorContentIndexingNotEnabled = 3975089319U,
			// Token: 0x040036DE RID: 14046
			ErrorContentConversionFailed = 4227151856U,
			// Token: 0x040036DF RID: 14047
			ConversationIdNotSupported = 3426540703U,
			// Token: 0x040036E0 RID: 14048
			ConversationSupportedOnlyForMailboxSession = 730941518U,
			// Token: 0x040036E1 RID: 14049
			ErrorMoveDistinguishedFolder = 3771523283U,
			// Token: 0x040036E2 RID: 14050
			ErrorMailboxCannotBeSpecifiedForPublicFolderRoot = 2092164778U,
			// Token: 0x040036E3 RID: 14051
			IrmPreLicensingFailure = 2805212767U,
			// Token: 0x040036E4 RID: 14052
			MessageMissingUserRolesForLegalHoldRoleTypeApp = 734136355U,
			// Token: 0x040036E5 RID: 14053
			ErrorMailboxVersionNotSupported = 430009573U,
			// Token: 0x040036E6 RID: 14054
			ErrorRestrictionTooComplex = 560492804U,
			// Token: 0x040036E7 RID: 14055
			RuleErrorRecipientDoesNotExist = 3546363172U,
			// Token: 0x040036E8 RID: 14056
			ErrorInvalidAggregatedAccountCredentials = 3667869681U,
			// Token: 0x040036E9 RID: 14057
			descInvalidSecurityContext = 2653688977U,
			// Token: 0x040036EA RID: 14058
			MessagePublicFoldersNotSupportedForNonIndexable = 213621866U,
			// Token: 0x040036EB RID: 14059
			ErrorInvalidFilterNode = 3943930965U,
			// Token: 0x040036EC RID: 14060
			ErrorIrmUserRightNotGranted = 1508237301U,
			// Token: 0x040036ED RID: 14061
			descInvalidRequestType = 3956968185U,
			// Token: 0x040036EE RID: 14062
			DowaNotProvisioned = 184315686U,
			// Token: 0x040036EF RID: 14063
			ErrorRecurrenceEndDateTooBig = 2652436543U,
			// Token: 0x040036F0 RID: 14064
			ErrorInvalidItemForReply = 629782913U,
			// Token: 0x040036F1 RID: 14065
			UpdateFavoritesInvalidUpdateFavoriteOperationType = 1342320011U,
			// Token: 0x040036F2 RID: 14066
			ErrorInvalidManagementRoleHeader = 2674011741U,
			// Token: 0x040036F3 RID: 14067
			ErrorCannotGetExternalEcpUrl = 200462199U,
			// Token: 0x040036F4 RID: 14068
			ErrorCannotCreateSearchFolderInPublicFolder = 1645715101U,
			// Token: 0x040036F5 RID: 14069
			RuleErrorUnsupportedRule = 3288028209U,
			// Token: 0x040036F6 RID: 14070
			ErrorMissingManagedFolderId = 2518142400U,
			// Token: 0x040036F7 RID: 14071
			MessageInsufficientPermissionsToSend = 1990145025U,
			// Token: 0x040036F8 RID: 14072
			ErrorInvalidCompleteDate = 3098927940U,
			// Token: 0x040036F9 RID: 14073
			ErrorSearchFolderTimeout = 2447591155U,
			// Token: 0x040036FA RID: 14074
			ErrorCannotSetAggregatedAccount = 4089853131U,
			// Token: 0x040036FB RID: 14075
			ErrorInvalidPushSubscriptionUrl = 1962425675U,
			// Token: 0x040036FC RID: 14076
			ErrorCannotAddAggregatedAccount = 1669051638U,
			// Token: 0x040036FD RID: 14077
			ErrorCalendarIsGroupMailboxForDecline = 1718538996U,
			// Token: 0x040036FE RID: 14078
			ErrorNameResolutionNoMailbox = 761574210U,
			// Token: 0x040036FF RID: 14079
			ErrorCannotArchiveItemsInArchiveMailbox = 2225772284U,
			// Token: 0x04003700 RID: 14080
			MowaNotProvisioned = 556297389U,
			// Token: 0x04003701 RID: 14081
			ErrorInvalidOperationSendAndSaveCopyToPublicFolder = 529689091U,
			// Token: 0x04003702 RID: 14082
			ConversationActionNeedDestinationFolderForMoveAction = 1998574567U,
			// Token: 0x04003703 RID: 14083
			ErrorViewFilterRequiresSearchContext = 718120058U,
			// Token: 0x04003704 RID: 14084
			ErrorDelegateAlreadyExists = 1363870753U,
			// Token: 0x04003705 RID: 14085
			ErrorSubmitQueryBasedHoldTaskFailed = 2479091638U,
			// Token: 0x04003706 RID: 14086
			ErrorPeopleConnectFailedToReadApplicationConfiguration = 2869245557U,
			// Token: 0x04003707 RID: 14087
			ErrorUnsupportedMapiPropertyType = 937093447U,
			// Token: 0x04003708 RID: 14088
			ErrorApprovalRequestAlreadyDecided = 1829541172U,
			// Token: 0x04003709 RID: 14089
			MessageCouldNotFindWeatherLocations = 472949644U,
			// Token: 0x0400370A RID: 14090
			WhenColon = 3770755973U,
			// Token: 0x0400370B RID: 14091
			ErrorNoGroupingForQueryString = 70508874U,
			// Token: 0x0400370C RID: 14092
			ErrorInvalidIdStoreObjectIdTooLong = 2651121857U,
			// Token: 0x0400370D RID: 14093
			ErrorQuotaExceeded = 3654265673U,
			// Token: 0x0400370E RID: 14094
			ConversationActionNeedReadStateForSetReadStateAction = 3601113588U,
			// Token: 0x0400370F RID: 14095
			ErrorLocationServicesRequestTimedOut = 4226485813U,
			// Token: 0x04003710 RID: 14096
			ErrorCalendarInvalidPropertyValue = 3349192959U,
			// Token: 0x04003711 RID: 14097
			ErrorManagedFolderAlreadyExists = 978558141U,
			// Token: 0x04003712 RID: 14098
			ErrorLocationServicesInvalidSource = 1008089967U,
			// Token: 0x04003713 RID: 14099
			OnPremiseSynchorizedDiscoverySearch = 2560374358U,
			// Token: 0x04003714 RID: 14100
			ErrorInvalidOperationForAssociatedItems = 3859804741U,
			// Token: 0x04003715 RID: 14101
			ErrorCorruptData = 953197733U,
			// Token: 0x04003716 RID: 14102
			ErrorCalendarInvalidTimeZone = 39525862U,
			// Token: 0x04003717 RID: 14103
			ErrorInvalidOperationMessageDispositionAssociatedItem = 3281131813U,
			// Token: 0x04003718 RID: 14104
			ErrorSubscriptionAccessDenied = 2662672540U,
			// Token: 0x04003719 RID: 14105
			ErrorCannotReadRequestBody = 2608213760U,
			// Token: 0x0400371A RID: 14106
			ErrorNameResolutionMultipleResults = 2070630207U,
			// Token: 0x0400371B RID: 14107
			ErrorInvalidExtendedProperty = 866480793U,
			// Token: 0x0400371C RID: 14108
			EwsProxyCannotGetCredentials = 3760366944U,
			// Token: 0x0400371D RID: 14109
			UpdateFavoritesInvalidMoveFavoriteRequest = 2761096871U,
			// Token: 0x0400371E RID: 14110
			ErrorInvalidPermissionSettings = 1359116179U,
			// Token: 0x0400371F RID: 14111
			ErrorProxyServiceDiscoveryFailed = 1645280882U,
			// Token: 0x04003720 RID: 14112
			ErrorInvalidItemForOperationAcceptItem = 598450895U,
			// Token: 0x04003721 RID: 14113
			ErrorInvalidValueForPropertyDuplicateDictionaryKey = 2578390262U,
			// Token: 0x04003722 RID: 14114
			ErrorExceededSubscriptionCount = 516980747U,
			// Token: 0x04003723 RID: 14115
			ErrorPermissionNotAllowedByPolicy = 739553585U,
			// Token: 0x04003724 RID: 14116
			MessageInsufficientPermissionsToSubscribe = 48346381U,
			// Token: 0x04003725 RID: 14117
			ErrorInvalidValueForPropertyDate = 1803820018U,
			// Token: 0x04003726 RID: 14118
			ErrorUnsupportedRecurrence = 3322365201U,
			// Token: 0x04003727 RID: 14119
			ErrorUserADObjectNotFound = 837503410U,
			// Token: 0x04003728 RID: 14120
			ErrorCannotAttachSelf = 2020376324U,
			// Token: 0x04003729 RID: 14121
			ErrorMissingInformationSharingFolderId = 2938284467U,
			// Token: 0x0400372A RID: 14122
			ErrorCannotSetFromOnMeetingResponse = 1762596806U,
			// Token: 0x0400372B RID: 14123
			MessageInvalidOperationForPublicFolderItemsAddParticipantByItemId = 3795663900U,
			// Token: 0x0400372C RID: 14124
			ErrorInvalidItemForOperationCreateItem = 1439158331U,
			// Token: 0x0400372D RID: 14125
			ErrorInvalidPropertyForExists = 3788524313U,
			// Token: 0x0400372E RID: 14126
			ErrorCannotSaveSentItemInPublicFolder = 792522617U,
			// Token: 0x0400372F RID: 14127
			ErrorRestrictionTooLong = 3143473274U,
			// Token: 0x04003730 RID: 14128
			ErrorUnsupportedPropertyDefinition = 789094727U,
			// Token: 0x04003731 RID: 14129
			SharePointCreationFailed = 3311760175U,
			// Token: 0x04003732 RID: 14130
			ErrorDataSizeLimitExceeded = 2935460503U,
			// Token: 0x04003733 RID: 14131
			ErrorFolderExists = 628559436U,
			// Token: 0x04003734 RID: 14132
			ErrorUnifiedGroupAlreadyExists = 2930851601U,
			// Token: 0x04003735 RID: 14133
			MessageApplicationTokenOnly = 1580647852U,
			// Token: 0x04003736 RID: 14134
			ErrorSharingNoExternalEwsAvailable = 4047718788U,
			// Token: 0x04003737 RID: 14135
			RuleErrorEmptyValueFound = 1661960732U,
			// Token: 0x04003738 RID: 14136
			ErrorOccurrenceCrossingBoundary = 852852329U,
			// Token: 0x04003739 RID: 14137
			ErrorArchiveMailboxServiceDiscoveryFailed = 3156121664U,
			// Token: 0x0400373A RID: 14138
			ErrorInvalidAttachmentSubfilterTextFilter = 1064353045U,
			// Token: 0x0400373B RID: 14139
			ErrorGetSharingMetadataNotSupported = 1966896516U,
			// Token: 0x0400373C RID: 14140
			MessageRecipientMustHaveEmailAddress = 1507828071U,
			// Token: 0x0400373D RID: 14141
			ErrorInvalidRecipientSubfilterTextFilter = 4094604515U,
			// Token: 0x0400373E RID: 14142
			ErrorInvalidPropertyRequest = 3673396595U,
			// Token: 0x0400373F RID: 14143
			ErrorCalendarIsNotOrganizer = 1582215140U,
			// Token: 0x04003740 RID: 14144
			ErrorInvalidProvisionDeviceID = 3374101509U,
			// Token: 0x04003741 RID: 14145
			MessageCouldNotGetWeatherDataForLocation = 4117821571U,
			// Token: 0x04003742 RID: 14146
			ErrorTimeProposalMissingStartOrEndTimeError = 2326085984U,
			// Token: 0x04003743 RID: 14147
			ErrorInvalidSubfilterTypeNotAttendeeType = 1946206036U,
			// Token: 0x04003744 RID: 14148
			PropertyCommandNotSupportSet = 3890629732U,
			// Token: 0x04003745 RID: 14149
			ErrorImpersonationFailed = 347738787U,
			// Token: 0x04003746 RID: 14150
			ErrorSubscriptionNotFound = 2884324330U,
			// Token: 0x04003747 RID: 14151
			MessageCalendarInsufficientPermissionsToMoveMeetingRequest = 2225154662U,
			// Token: 0x04003748 RID: 14152
			ErrorInvalidIdMalformed = 3107705007U,
			// Token: 0x04003749 RID: 14153
			ErrorCalendarIsGroupMailboxForSuppressReadReceipt = 1035957819U,
			// Token: 0x0400374A RID: 14154
			ErrorCannotGetSourceFolderPath = 2940401781U,
			// Token: 0x0400374B RID: 14155
			ErrorWildcardAndGroupExpansionNotAllowed = 4083587704U,
			// Token: 0x0400374C RID: 14156
			UnsupportedInlineAttachmentContentType = 4077357270U,
			// Token: 0x0400374D RID: 14157
			RuleErrorUnexpectedError = 1170272727U,
			// Token: 0x0400374E RID: 14158
			MessageCalendarInsufficientPermissionsToDraftsFolder = 1601473907U,
			// Token: 0x0400374F RID: 14159
			ErrorADUnavailable = 634294555U,
			// Token: 0x04003750 RID: 14160
			ErrorInvalidPhoneNumber = 3260461220U,
			// Token: 0x04003751 RID: 14161
			ErrorSoftDeletedTraversalsNotAllowedOnPublicFolders = 547900838U,
			// Token: 0x04003752 RID: 14162
			ErrorCalendarIsDelegatedForTentative = 2687301200U,
			// Token: 0x04003753 RID: 14163
			ErrorFoldersMustBelongToSameMailbox = 2952942328U,
			// Token: 0x04003754 RID: 14164
			ErrorDataSourceOperation = 2697731302U,
			// Token: 0x04003755 RID: 14165
			ErrorCalendarMeetingIsOutOfDateResponseNotProcessed = 3573754788U,
			// Token: 0x04003756 RID: 14166
			MessageInvalidIdMalformedEwsIdFormat = 1475709851U,
			// Token: 0x04003757 RID: 14167
			ErrorPreviousPageNavigationCurrentlyNotSupported = 3699315399U,
			// Token: 0x04003758 RID: 14168
			ErrorCannotEmptyPublicFolderToDeletedItems = 2058507107U,
			// Token: 0x04003759 RID: 14169
			ErrorInvalidSharingData = 1014449457U,
			// Token: 0x0400375A RID: 14170
			MessageCalendarInsufficientPermissionsToMeetingMessageFolder = 1746698887U,
			// Token: 0x0400375B RID: 14171
			ErrorInvalidOperationCannotSpecifyItemId = 2503843052U,
			// Token: 0x0400375C RID: 14172
			ErrorCalendarIsGroupMailboxForTentative = 3187786876U,
			// Token: 0x0400375D RID: 14173
			ErrorMessageSizeExceeded = 2913173341U,
			// Token: 0x0400375E RID: 14174
			InvalidDateTimePrecisionValue = 3468080577U,
			// Token: 0x0400375F RID: 14175
			ErrorStaleObject = 3943872330U,
			// Token: 0x04003760 RID: 14176
			UpdateFavoritesUnableToAddFolderToFavorites = 3119664543U,
			// Token: 0x04003761 RID: 14177
			ErrorPasswordExpired = 1282299710U,
			// Token: 0x04003762 RID: 14178
			ErrorInvalidOperationCannotPerformOperationOnADRecipients = 3142918589U,
			// Token: 0x04003763 RID: 14179
			ErrorTooManyObjectsOpened = 157861094U,
			// Token: 0x04003764 RID: 14180
			MessageInvalidMailboxInvalidReferencedItem = 256399585U,
			// Token: 0x04003765 RID: 14181
			MessageApplicationHasNoGivenRoleAssigned = 3901728717U,
			// Token: 0x04003766 RID: 14182
			MessageRecipientsArrayTooLong = 3113724054U,
			// Token: 0x04003767 RID: 14183
			ErrorInvalidIdXml = 3852956793U,
			// Token: 0x04003768 RID: 14184
			ErrorCallerWithoutMailboxCannotUseSendOnly = 2271901695U,
			// Token: 0x04003769 RID: 14185
			ErrorArchiveMailboxSearchFailed = 2535285679U,
			// Token: 0x0400376A RID: 14186
			PostedOn = 2543409328U,
			// Token: 0x0400376B RID: 14187
			ErrorInvalidExternalSharingInitiator = 4028591235U,
			// Token: 0x0400376C RID: 14188
			ErrorMailboxStoreUnavailable = 1627983613U,
			// Token: 0x0400376D RID: 14189
			ErrorInvalidCalendarViewRestrictionOrSort = 2358398289U,
			// Token: 0x0400376E RID: 14190
			ErrorSavedItemFolderNotFound = 3610830273U,
			// Token: 0x0400376F RID: 14191
			ErrorCalendarOccurrenceIsDeletedFromRecurrence = 3335161738U,
			// Token: 0x04003770 RID: 14192
			ErrorMissingRecipients = 2985674644U,
			// Token: 0x04003771 RID: 14193
			ErrorTimeProposalInvalidInCreateItemRequest = 3997746891U,
			// Token: 0x04003772 RID: 14194
			ErrorCalendarIsDelegatedForRemove = 2990436390U,
			// Token: 0x04003773 RID: 14195
			ErrorInvalidLikeRequest = 4151155219U,
			// Token: 0x04003774 RID: 14196
			MessageRecurrenceStartDateTooSmall = 271991716U,
			// Token: 0x04003775 RID: 14197
			ErrorUnknownTimeZone = 4066404319U,
			// Token: 0x04003776 RID: 14198
			ErrorProxyGroupSidLimitExceeded = 1656583547U,
			// Token: 0x04003777 RID: 14199
			ErrorCannotRemoveAggregatedAccount = 2834376775U,
			// Token: 0x04003778 RID: 14200
			ErrorInvalidShape = 1816334244U,
			// Token: 0x04003779 RID: 14201
			ErrorInvalidLicense = 1812149170U,
			// Token: 0x0400377A RID: 14202
			ErrorAccountDisabled = 531497785U,
			// Token: 0x0400377B RID: 14203
			ErrorHoldIsNotFound = 1949840710U,
			// Token: 0x0400377C RID: 14204
			MessageMessageIsNotDraft = 1830098328U,
			// Token: 0x0400377D RID: 14205
			ErrorWrongServerVersionDelegate = 3778961523U,
			// Token: 0x0400377E RID: 14206
			OnBehalfOf = 2868846894U,
			// Token: 0x0400377F RID: 14207
			ErrorInvalidOperationForPublicFolderItems = 1902653190U,
			// Token: 0x04003780 RID: 14208
			ErrorCalendarCannotUseIdForRecurringMasterId = 1069471396U,
			// Token: 0x04003781 RID: 14209
			ErrorInvalidSubscriptionRequest = 3647226175U,
			// Token: 0x04003782 RID: 14210
			ErrorInvalidIdEmpty = 4226852029U,
			// Token: 0x04003783 RID: 14211
			ErrorInvalidAttachmentId = 922305341U,
			// Token: 0x04003784 RID: 14212
			ErrorBothQueryStringAndRestrictionNonNull = 2329210449U,
			// Token: 0x04003785 RID: 14213
			RuleErrorRuleNotFound = 1725658743U,
			// Token: 0x04003786 RID: 14214
			ErrorDiscoverySearchesDisabled = 1277300954U,
			// Token: 0x04003787 RID: 14215
			ErrorCalendarIsCancelledForTentative = 1147653914U,
			// Token: 0x04003788 RID: 14216
			ErrorRecurrenceHasNoOccurrence = 1564162812U,
			// Token: 0x04003789 RID: 14217
			MessageNonExistentMailboxLegacyDN = 103255531U,
			// Token: 0x0400378A RID: 14218
			ErrorNoDestinationCASDueToKerberosRequirements = 3137087456U,
			// Token: 0x0400378B RID: 14219
			ErrorFolderNotFound = 3395659933U,
			// Token: 0x0400378C RID: 14220
			ErrorCannotPinGroupIfNotAMember = 2923349632U,
			// Token: 0x0400378D RID: 14221
			MessageInsufficientPermissionsToSync = 2118011096U,
			// Token: 0x0400378E RID: 14222
			ErrorCalendarIsDelegatedForAccept = 2483737250U,
			// Token: 0x0400378F RID: 14223
			ErrorInvalidClientAccessTokenRequest = 2958727324U,
			// Token: 0x04003790 RID: 14224
			ErrorCalendarOccurrenceIndexIsOutOfRecurrenceRange = 2006869741U,
			// Token: 0x04003791 RID: 14225
			MessageMissingUpdateDelegateRequestInformation = 124559532U,
			// Token: 0x04003792 RID: 14226
			ErrorCannotOpenFileAttachment = 492857424U,
			// Token: 0x04003793 RID: 14227
			ErrorInvalidFolderId = 234107130U,
			// Token: 0x04003794 RID: 14228
			ErrorInvalidPropertyUpdateSentMessage = 2141227684U,
			// Token: 0x04003795 RID: 14229
			MessageCalendarInsufficientPermissionsToDefaultCalendarFolder = 1686445652U,
			// Token: 0x04003796 RID: 14230
			IrmServerMisConfigured = 784482022U,
			// Token: 0x04003797 RID: 14231
			RuleErrorRulesOverQuota = 357046427U,
			// Token: 0x04003798 RID: 14232
			ErrorNotAllowedExternalSharingByPolicy = 2890296403U,
			// Token: 0x04003799 RID: 14233
			ErrorCannotCreatePostItemInNonMailFolder = 3792171687U,
			// Token: 0x0400379A RID: 14234
			ErrorCannotEmptyCalendarOrSearchFolder = 3080652515U,
			// Token: 0x0400379B RID: 14235
			ErrorEmptyAggregatedAccountMailboxGuidStoredInSyncRequest = 3504612180U,
			// Token: 0x0400379C RID: 14236
			ErrorExpiredSubscription = 3329761676U,
			// Token: 0x0400379D RID: 14237
			ErrorODataAccessDisabled = 3795993851U,
			// Token: 0x0400379E RID: 14238
			ErrorCannotArchiveItemsInPublicFolders = 3558192788U,
			// Token: 0x0400379F RID: 14239
			ErrorAssociatedTraversalDisallowedWithQueryString = 908888675U,
			// Token: 0x040037A0 RID: 14240
			ErrorCalendarIsOrganizerForDecline = 2980490932U,
			// Token: 0x040037A1 RID: 14241
			ErrorMissingEmailAddressForManagedFolder = 1228157268U,
			// Token: 0x040037A2 RID: 14242
			ErrorGetSharingMetadataOnlyForMailbox = 1105778474U,
			// Token: 0x040037A3 RID: 14243
			MessageActingAsMustHaveRoutingType = 2292082652U,
			// Token: 0x040037A4 RID: 14244
			ErrorInvalidOperationAddItemToMyCalendar = 473053729U,
			// Token: 0x040037A5 RID: 14245
			ErrorSyncFolderNotFound = 1581442160U,
			// Token: 0x040037A6 RID: 14246
			ErrorInvalidSharingMessage = 471235856U,
			// Token: 0x040037A7 RID: 14247
			descInvalidRequest = 1735870649U,
			// Token: 0x040037A8 RID: 14248
			ErrorUnsupportedServiceConfigurationType = 3640136612U,
			// Token: 0x040037A9 RID: 14249
			RuleErrorCreateWithRuleId = 918293667U,
			// Token: 0x040037AA RID: 14250
			LoadExtensionCustomPropertiesFailed = 3053756532U,
			// Token: 0x040037AB RID: 14251
			ErrorUserNotAllowedByPolicy = 311942179U,
			// Token: 0x040037AC RID: 14252
			MessageCouldNotGetWeatherData = 2933471333U,
			// Token: 0x040037AD RID: 14253
			MessageMultipleApplicationRolesNotSupported = 2335200077U,
			// Token: 0x040037AE RID: 14254
			ErrorPropertyValidationFailure = 3967923828U,
			// Token: 0x040037AF RID: 14255
			ErrorInvalidOperationCalendarViewAssociatedItem = 3789879302U,
			// Token: 0x040037B0 RID: 14256
			ErrorInvalidUserPrincipalName = 266941361U,
			// Token: 0x040037B1 RID: 14257
			ErrorMissedNotificationEvents = 124305755U,
			// Token: 0x040037B2 RID: 14258
			ErrorCannotRemoveAggregatedAccountMailbox = 3635708019U,
			// Token: 0x040037B3 RID: 14259
			MessageCalendarUnableToUpdateMeetingRequest = 2102429258U,
			// Token: 0x040037B4 RID: 14260
			ErrorInvalidValueForPropertyUserConfigurationPublicFolder = 3014743008U,
			// Token: 0x040037B5 RID: 14261
			ErrorFolderSave = 3867216855U,
			// Token: 0x040037B6 RID: 14262
			MessageResolveNamesNotSufficientPermissionsToContactsFolder = 2034497546U,
			// Token: 0x040037B7 RID: 14263
			descMissingForestConfiguration = 1790760926U,
			// Token: 0x040037B8 RID: 14264
			ErrorUnsupportedPathForSortGroup = 1103930166U,
			// Token: 0x040037B9 RID: 14265
			ErrorContainsFilterWrongType = 3836413508U,
			// Token: 0x040037BA RID: 14266
			ErrorMailboxScopeNotAllowedWithoutQueryString = 1270269734U,
			// Token: 0x040037BB RID: 14267
			ErrorMessageTrackingPermanentError = 2084976894U,
			// Token: 0x040037BC RID: 14268
			ErrorCannotDeleteObject = 3912965805U,
			// Token: 0x040037BD RID: 14269
			MessageCallerHasNoAdminRoleGranted = 1670145257U,
			// Token: 0x040037BE RID: 14270
			ErrorIrmNotSupported = 2692292357U,
			// Token: 0x040037BF RID: 14271
			ReferenceLinkSharedFrom = 707914022U,
			// Token: 0x040037C0 RID: 14272
			SentColon = 295620541U,
			// Token: 0x040037C1 RID: 14273
			ErrorActingAsUserNotUnique = 1479620947U,
			// Token: 0x040037C2 RID: 14274
			ErrorSearchQueryHasTooManyKeywords = 3032287327U,
			// Token: 0x040037C3 RID: 14275
			ErrorFolderPropertyRequestFailed = 2370747299U,
			// Token: 0x040037C4 RID: 14276
			ErrorMimeContentInvalid = 1795652632U,
			// Token: 0x040037C5 RID: 14277
			ErrorSharingSynchronizationFailed = 3469371317U,
			// Token: 0x040037C6 RID: 14278
			ErrorPublicFolderSearchNotSupportedOnMultipleFolders = 2109751382U,
			// Token: 0x040037C7 RID: 14279
			ErrorNoFolderClassOverride = 3753602229U,
			// Token: 0x040037C8 RID: 14280
			ErrorUnsupportedTypeForConversion = 836749070U,
			// Token: 0x040037C9 RID: 14281
			ErrorInvalidItemForOperationDeclineItem = 1535520491U,
			// Token: 0x040037CA RID: 14282
			MessageCalendarInsufficientPermissionsToSaveCalendarItem = 3563948173U,
			// Token: 0x040037CB RID: 14283
			ErrorRightsManagementException = 237462827U,
			// Token: 0x040037CC RID: 14284
			ErrorOperationNotAllowedWithPublicFolderRoot = 2440725179U,
			// Token: 0x040037CD RID: 14285
			ErrorInvalidIdReturnedByResolveNames = 189525348U,
			// Token: 0x040037CE RID: 14286
			descNoRequestType = 2194994953U,
			// Token: 0x040037CF RID: 14287
			ErrorCalendarIsOrganizerForTentative = 3371251772U,
			// Token: 0x040037D0 RID: 14288
			ErrorInvalidVotingRequest = 2253496121U,
			// Token: 0x040037D1 RID: 14289
			ErrorInvalidProvisionDeviceType = 832351692U,
			// Token: 0x040037D2 RID: 14290
			RuleErrorUnsupportedAddress = 2176189925U,
			// Token: 0x040037D3 RID: 14291
			ErrorInvalidCallStatus = 1093593955U,
			// Token: 0x040037D4 RID: 14292
			ErrorInvalidSid = 2718542415U,
			// Token: 0x040037D5 RID: 14293
			ErrorManagedFoldersRootFailure = 2580909644U,
			// Token: 0x040037D6 RID: 14294
			ErrorProxiedSubscriptionCallFailure = 1569114342U,
			// Token: 0x040037D7 RID: 14295
			ErrorOccurrenceTimeSpanTooBig = 1214042036U,
			// Token: 0x040037D8 RID: 14296
			MessageCalendarInsufficientPermissionsToMoveCalendarItem = 1110621977U,
			// Token: 0x040037D9 RID: 14297
			ErrorNewEventStreamConnectionOpened = 2943900075U,
			// Token: 0x040037DA RID: 14298
			ErrorArchiveMailboxNotEnabled = 2054881972U,
			// Token: 0x040037DB RID: 14299
			ErrorCalendarCannotUseIdForOccurrenceId = 4180336284U,
			// Token: 0x040037DC RID: 14300
			ErrorAccessDenied = 3579904699U,
			// Token: 0x040037DD RID: 14301
			ErrorAttachmentSizeLimitExceeded = 1721078306U,
			// Token: 0x040037DE RID: 14302
			ErrorPropertyUpdate = 1912743644U,
			// Token: 0x040037DF RID: 14303
			RuleErrorInvalidValue = 842243550U,
			// Token: 0x040037E0 RID: 14304
			ErrorInvalidManagedFolderQuota = 2756368512U,
			// Token: 0x040037E1 RID: 14305
			ErrorCreateDistinguishedFolder = 304669716U,
			// Token: 0x040037E2 RID: 14306
			ShowDetails = 3684919469U,
			// Token: 0x040037E3 RID: 14307
			ToColon = 3465339554U,
			// Token: 0x040037E4 RID: 14308
			ErrorCrossMailboxMoveCopy = 2832845860U,
			// Token: 0x040037E5 RID: 14309
			FlagForFollowUp = 4273162695U,
			// Token: 0x040037E6 RID: 14310
			ErrorGetStreamingEventsProxy = 4264440001U,
			// Token: 0x040037E7 RID: 14311
			ErrorCannotSetCalendarPermissionOnNonCalendarFolder = 2183377470U,
			// Token: 0x040037E8 RID: 14312
			SaveExtensionCustomPropertiesFailed = 1686056205U,
			// Token: 0x040037E9 RID: 14313
			ErrorConnectionFailed = 3500594897U,
			// Token: 0x040037EA RID: 14314
			ErrorCannotUseLocalAccount = 2976928908U,
			// Token: 0x040037EB RID: 14315
			descInvalidOofParameter = 810356415U,
			// Token: 0x040037EC RID: 14316
			ErrorTimeRangeIsTooLarge = 2538042329U,
			// Token: 0x040037ED RID: 14317
			ErrorAffectedTaskOccurrencesRequired = 2684918840U,
			// Token: 0x040037EE RID: 14318
			ErrorCannotGetAggregatedAccount = 513058151U,
			// Token: 0x040037EF RID: 14319
			AADIdentityCreationFailed = 917420962U,
			// Token: 0x040037F0 RID: 14320
			ErrorDuplicateInputFolderNames = 217482359U,
			// Token: 0x040037F1 RID: 14321
			MessageNonExistentMailboxSmtpAddress = 4088802584U,
			// Token: 0x040037F2 RID: 14322
			ErrorIncorrectUpdatePropertyCount = 1439726170U,
			// Token: 0x040037F3 RID: 14323
			ErrorInvalidSerializedAccessToken = 2485795088U,
			// Token: 0x040037F4 RID: 14324
			ErrorInvalidRoutingType = 4103342537U,
			// Token: 0x040037F5 RID: 14325
			ErrorSendMeetingInvitationsRequired = 3383701276U,
			// Token: 0x040037F6 RID: 14326
			ErrorInvalidIdNotAnItemAttachmentId = 1500443603U,
			// Token: 0x040037F7 RID: 14327
			RightsManagementInternalLicensingDisabled = 1702622873U,
			// Token: 0x040037F8 RID: 14328
			MessageCannotUseItemAsRecipient = 247950989U,
			// Token: 0x040037F9 RID: 14329
			ErrorItemSaveUserConfigurationExists = 815859081U,
			// Token: 0x040037FA RID: 14330
			MessageInvalidMailboxMailboxType = 687843280U,
			// Token: 0x040037FB RID: 14331
			ErrorCalendarIsCancelledForDecline = 2997278338U,
			// Token: 0x040037FC RID: 14332
			ErrorClientIntentInvalidStateDefinition = 3510335548U,
			// Token: 0x040037FD RID: 14333
			ErrorInvalidRetentionTagInvisible = 4105318492U,
			// Token: 0x040037FE RID: 14334
			ErrorItemSavePropertyError = 1583125739U,
			// Token: 0x040037FF RID: 14335
			GetScopedTokenFailedWithInvalidScope = 3308334241U,
			// Token: 0x04003800 RID: 14336
			ErrorInvalidItemForOperationRemoveItem = 1518836305U,
			// Token: 0x04003801 RID: 14337
			RuleErrorMessageClassificationNotFound = 1858753018U,
			// Token: 0x04003802 RID: 14338
			MessageUnableToLoadRBACSettings = 30276810U,
			// Token: 0x04003803 RID: 14339
			ErrorQueryLanguageNotValid = 707594371U,
			// Token: 0x04003804 RID: 14340
			Purple = 4158023012U,
			// Token: 0x04003805 RID: 14341
			InvalidMaxItemsToReturn = 133083912U,
			// Token: 0x04003806 RID: 14342
			PostedTo = 4109493280U,
			// Token: 0x04003807 RID: 14343
			ExchangeServiceResponseErrorNoResponse = 607223707U,
			// Token: 0x04003808 RID: 14344
			ErrorPublicFolderOperationFailed = 1569475421U,
			// Token: 0x04003809 RID: 14345
			ErrorBatchProcessingStopped = 444799972U,
			// Token: 0x0400380A RID: 14346
			ErrorUnifiedMessagingServerNotFound = 1722439826U,
			// Token: 0x0400380B RID: 14347
			InstantSearchNullFolderId = 4077186341U,
			// Token: 0x0400380C RID: 14348
			ErrorWeatherServiceDisabled = 4210036349U,
			// Token: 0x0400380D RID: 14349
			descNotEnoughPrivileges = 2710201884U,
			// Token: 0x0400380E RID: 14350
			CalendarInvalidFirstDayOfWeek = 3284680126U,
			// Token: 0x0400380F RID: 14351
			Red = 3021629811U,
			// Token: 0x04003810 RID: 14352
			ErrorInvalidExternalSharingSubscriber = 3133201118U,
			// Token: 0x04003811 RID: 14353
			ErrorCannotUseFolderIdForItemId = 2770848984U,
			// Token: 0x04003812 RID: 14354
			ErrorExchange14Required = 3336001063U,
			// Token: 0x04003813 RID: 14355
			ErrorProxyCallFailed = 3032417457U,
			// Token: 0x04003814 RID: 14356
			ErrorOrganizationNotFederated = 1788731128U,
			// Token: 0x04003815 RID: 14357
			Blue = 2395476974U,
			// Token: 0x04003816 RID: 14358
			ErrorCannotDeleteSubfoldersOfMsgRootFolder = 1411102909U,
			// Token: 0x04003817 RID: 14359
			ErrorUpdatePropertyMismatch = 1737721488U,
			// Token: 0x04003818 RID: 14360
			ErrorIllegalCrossServerConnection = 1160056179U,
			// Token: 0x04003819 RID: 14361
			ErrorImListMigration = 822125946U,
			// Token: 0x0400381A RID: 14362
			ErrorResponseSchemaValidation = 610335429U,
			// Token: 0x0400381B RID: 14363
			ServerNotInSite = 3829975538U,
			// Token: 0x0400381C RID: 14364
			ErrorCannotAddAggregatedAccountToList = 3220333293U,
			// Token: 0x0400381D RID: 14365
			WhereColon = 1666265192U,
			// Token: 0x0400381E RID: 14366
			ErrorInvalidApprovalRequest = 708456719U,
			// Token: 0x0400381F RID: 14367
			ErrorIncorrectEncodedIdType = 3010537222U,
			// Token: 0x04003820 RID: 14368
			ErrorGetRemoteArchiveItemFailed = 4106572054U,
			// Token: 0x04003821 RID: 14369
			ErrorInvalidImGroupId = 827411151U,
			// Token: 0x04003822 RID: 14370
			ErrorInvalidRequestUnknownMethodDebug = 1835609958U,
			// Token: 0x04003823 RID: 14371
			ErrorBothViewFilterAndRestrictionNonNull = 2675632227U,
			// Token: 0x04003824 RID: 14372
			ErrorCannotUseItemIdForFolderId = 2423603834U,
			// Token: 0x04003825 RID: 14373
			ErrorCannotDisableMandatoryExtension = 1262244671U,
			// Token: 0x04003826 RID: 14374
			ErrorInvalidSyncStateData = 1655535493U,
			// Token: 0x04003827 RID: 14375
			ErrorSubmissionQuotaExceeded = 178029729U,
			// Token: 0x04003828 RID: 14376
			ErrorMessageDispositionRequired = 2130715693U,
			// Token: 0x04003829 RID: 14377
			ErrorSearchScopeCannotHavePublicFolders = 1395824819U,
			// Token: 0x0400382A RID: 14378
			ErrorRemoveDelegatesFailed = 3763931121U,
			// Token: 0x0400382B RID: 14379
			ErrorInvalidPagingMaxRows = 2467205866U,
			// Token: 0x0400382C RID: 14380
			RuleErrorMissingParameter = 3016713339U,
			// Token: 0x0400382D RID: 14381
			ErrorLocationServicesInvalidQuery = 1085788054U,
			// Token: 0x0400382E RID: 14382
			MessageOccurrenceNotFound = 1319006043U,
			// Token: 0x0400382F RID: 14383
			ErrorSearchFolderNotInitialized = 1658627017U,
			// Token: 0x04003830 RID: 14384
			FolderScopeNotSpecified = 1064030279U,
			// Token: 0x04003831 RID: 14385
			ErrorInvalidSubfilterType = 3880436217U,
			// Token: 0x04003832 RID: 14386
			ErrorDuplicateUserIdsSpecified = 4289255106U,
			// Token: 0x04003833 RID: 14387
			ErrorDelegateMustBeCalendarEditorToGetMeetingMessages = 1422139444U,
			// Token: 0x04003834 RID: 14388
			ErrorMismatchFolderId = 3041888687U,
			// Token: 0x04003835 RID: 14389
			ErrorInvalidPropertyDelete = 444235555U,
			// Token: 0x04003836 RID: 14390
			MessageActingAsMustHaveEmailAddress = 3538999938U,
			// Token: 0x04003837 RID: 14391
			ErrorCalendarIsCancelledForRemove = 4064247940U,
			// Token: 0x04003838 RID: 14392
			ErrorCannotResolveODataUrl = 884514945U,
			// Token: 0x04003839 RID: 14393
			ErrorCalendarEndDateIsEarlierThanStartDate = 4006585486U,
			// Token: 0x0400383A RID: 14394
			ErrorInvalidPercentCompleteValue = 3035123300U,
			// Token: 0x0400383B RID: 14395
			ErrorNoApplicableProxyCASServersAvailable = 4164112684U,
			// Token: 0x0400383C RID: 14396
			IrmProtectedVoicemailFeatureDisabled = 106943791U,
			// Token: 0x0400383D RID: 14397
			IrmExternalLicensingDisabled = 1397740097U,
			// Token: 0x0400383E RID: 14398
			ErrorExchangeConfigurationException = 2132997082U,
			// Token: 0x0400383F RID: 14399
			ErrorMailboxMoveInProgress = 2069536979U,
			// Token: 0x04003840 RID: 14400
			ErrorInvalidValueForPropertyXmlData = 2643780243U,
			// Token: 0x04003841 RID: 14401
			RuleErrorDuplicatedPriority = 668221183U,
			// Token: 0x04003842 RID: 14402
			ItemNotExistInPurgesFolder = 2622305962U,
			// Token: 0x04003843 RID: 14403
			MessageMissingUserRolesForMailboxSearchRoleTypeApp = 3313362701U,
			// Token: 0x04003844 RID: 14404
			ErrorInvalidNameForNameResolution = 4279571010U,
			// Token: 0x04003845 RID: 14405
			ErrorInvalidRecipientSubfilterOrder = 3776856310U,
			// Token: 0x04003846 RID: 14406
			ErrorMailboxContainerGuidMismatch = 3146925354U,
			// Token: 0x04003847 RID: 14407
			ErrorInvalidId = 1935400134U,
			// Token: 0x04003848 RID: 14408
			ErrorNonPrimarySmtpAddress = 195228379U,
			// Token: 0x04003849 RID: 14409
			ErrorSharedFolderSearchNotSupportedOnMultipleFolders = 2280668014U,
			// Token: 0x0400384A RID: 14410
			ErrorCalendarInvalidRecurrence = 1532804559U,
			// Token: 0x0400384B RID: 14411
			ErrorInvalidOperationSaveReplyForwardToPublicFolder = 4104292452U,
			// Token: 0x0400384C RID: 14412
			ErrorInvalidOrderbyThenby = 4072847078U,
			// Token: 0x0400384D RID: 14413
			ErrorInvalidRetentionTagTypeMismatch = 531605055U,
			// Token: 0x0400384E RID: 14414
			ErrorRequiredPropertyMissing = 608291240U,
			// Token: 0x0400384F RID: 14415
			ErrorActiveDirectoryPermanentError = 54420019U,
			// Token: 0x04003850 RID: 14416
			IrmRmsError = 360598592U,
			// Token: 0x04003851 RID: 14417
			ErrorNoPropertyUpdatesOrAttachmentsSpecified = 3654096821U,
			// Token: 0x04003852 RID: 14418
			ConversationActionNeedFlagForFlagAction = 2137439660U,
			// Token: 0x04003853 RID: 14419
			ErrorAttachmentNestLevelLimitExceeded = 178421269U,
			// Token: 0x04003854 RID: 14420
			ErrorInvalidSmtpAddress = 938097637U
		}

		// Token: 0x02000F77 RID: 3959
		private enum ParamIDs
		{
			// Token: 0x04003856 RID: 14422
			ErrorNoConnectionSettingsAvailableForAggregatedAccount,
			// Token: 0x04003857 RID: 14423
			ErrorTimeProposal,
			// Token: 0x04003858 RID: 14424
			IrmRmsErrorLocation,
			// Token: 0x04003859 RID: 14425
			RuleErrorInvalidSizeRange,
			// Token: 0x0400385A RID: 14426
			ExchangeServiceResponseErrorNoResponseForType,
			// Token: 0x0400385B RID: 14427
			ErrorAccountNotSupportedForAggregation,
			// Token: 0x0400385C RID: 14428
			NewGroupMailboxFailed,
			// Token: 0x0400385D RID: 14429
			ErrorInvalidProperty,
			// Token: 0x0400385E RID: 14430
			ErrorInvalidUnifiedViewParameter,
			// Token: 0x0400385F RID: 14431
			ErrorRightsManagementTemplateNotFound,
			// Token: 0x04003860 RID: 14432
			TooManyMailboxQueryObjects,
			// Token: 0x04003861 RID: 14433
			ErrorPropertyNotSupportCreate,
			// Token: 0x04003862 RID: 14434
			MultifactorRegistrationUnavailable,
			// Token: 0x04003863 RID: 14435
			RuleErrorStringValueTooBig,
			// Token: 0x04003864 RID: 14436
			ErrorInternalServerErrorFaultInjection,
			// Token: 0x04003865 RID: 14437
			ErrorReturnTooManyMailboxesFromDG,
			// Token: 0x04003866 RID: 14438
			ActionQueueDeserializationError,
			// Token: 0x04003867 RID: 14439
			ErrorMandatoryPropertyMissing,
			// Token: 0x04003868 RID: 14440
			GetTenantFailed,
			// Token: 0x04003869 RID: 14441
			GetGroupMailboxFailed,
			// Token: 0x0400386A RID: 14442
			ErrorInvalidUrlQuery,
			// Token: 0x0400386B RID: 14443
			ErrorCannotLinkMoreThanOneO365AccountToAnMsa,
			// Token: 0x0400386C RID: 14444
			ErrorDuplicateLegacyDistinguishedNameFound,
			// Token: 0x0400386D RID: 14445
			ErrorAggregatedAccountLimitReached,
			// Token: 0x0400386E RID: 14446
			ErrorRightsManagementDuplicateTemplateId,
			// Token: 0x0400386F RID: 14447
			ErrorSearchTooManyMailboxes,
			// Token: 0x04003870 RID: 14448
			IrmRmsErrorCode,
			// Token: 0x04003871 RID: 14449
			GetFederatedDirectoryGroupFailed,
			// Token: 0x04003872 RID: 14450
			RuleErrorInvalidDateRange,
			// Token: 0x04003873 RID: 14451
			ErrorExtensionNotFound,
			// Token: 0x04003874 RID: 14452
			ErrorImContactLimitReached,
			// Token: 0x04003875 RID: 14453
			ErrorPropertyNotSupportUpdate,
			// Token: 0x04003876 RID: 14454
			ErrorPropertyNotSupportFilter,
			// Token: 0x04003877 RID: 14455
			GetFederatedDirectoryUserFailed,
			// Token: 0x04003878 RID: 14456
			ExchangeServiceResponseErrorWithCode,
			// Token: 0x04003879 RID: 14457
			ErrorInvalidDelegateUserId,
			// Token: 0x0400387A RID: 14458
			ErrorInvalidPropertyVersionRequest,
			// Token: 0x0400387B RID: 14459
			ErrorInvalidParameter,
			// Token: 0x0400387C RID: 14460
			ErrorImGroupLimitReached,
			// Token: 0x0400387D RID: 14461
			ExecutingUserNotFound,
			// Token: 0x0400387E RID: 14462
			SetGroupMailboxFailed,
			// Token: 0x0400387F RID: 14463
			IrmRmsErrorMessage,
			// Token: 0x04003880 RID: 14464
			ErrorCalendarSeekToConditionNotSupported,
			// Token: 0x04003881 RID: 14465
			RemoveGroupMailboxFailed,
			// Token: 0x04003882 RID: 14466
			ErrorInvalidRequestedUser,
			// Token: 0x04003883 RID: 14467
			ErrorParameterValueEmpty
		}
	}
}
