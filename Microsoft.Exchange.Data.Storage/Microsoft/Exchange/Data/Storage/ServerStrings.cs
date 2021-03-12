using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Security.RightsManagement.SOAP.Server;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000B5 RID: 181
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ServerStrings
	{
		// Token: 0x06000C3B RID: 3131 RVA: 0x00054B74 File Offset: 0x00052D74
		static ServerStrings()
		{
			ServerStrings.stringIDs.Add(2721128427U, "MissingRightsManagementLicense");
			ServerStrings.stringIDs.Add(195662054U, "ServerLocatorServiceTransientFault");
			ServerStrings.stringIDs.Add(824541142U, "EmailAddressMissing");
			ServerStrings.stringIDs.Add(2906390296U, "CannotShareSearchFolder");
			ServerStrings.stringIDs.Add(1390813357U, "EstimateStateStopping");
			ServerStrings.stringIDs.Add(1028519129U, "SpellCheckerDutch");
			ServerStrings.stringIDs.Add(2064592175U, "SpellCheckerNorwegianNynorsk");
			ServerStrings.stringIDs.Add(307085623U, "MigrationFlagsStart");
			ServerStrings.stringIDs.Add(4243709143U, "TeamMailboxMessageSiteAndSiteMailboxDetails");
			ServerStrings.stringIDs.Add(3398846764U, "CannotGetSupportedRoutingTypes");
			ServerStrings.stringIDs.Add(4287963345U, "ClientCulture_0x816");
			ServerStrings.stringIDs.Add(1588035907U, "AsyncOperationTypeMailboxRestore");
			ServerStrings.stringIDs.Add(1805838968U, "MatchContainerClassValidationFailed");
			ServerStrings.stringIDs.Add(3004251640U, "ExCannotCreateSubfolderUnderSearchFolder");
			ServerStrings.stringIDs.Add(161866662U, "InboxRuleImportanceHigh");
			ServerStrings.stringIDs.Add(1003186114U, "MapiCopyFailedProperties");
			ServerStrings.stringIDs.Add(1489962323U, "ClientCulture_0x3C0A");
			ServerStrings.stringIDs.Add(2738718017U, "ErrorTeamMailboxGetUsersNullResponse");
			ServerStrings.stringIDs.Add(2367323544U, "MigrationBatchSupportedActionNone");
			ServerStrings.stringIDs.Add(2198921793U, "ExAuditsUpdateDenied");
			ServerStrings.stringIDs.Add(2134621305U, "ExBadMessageEntryIdSize");
			ServerStrings.stringIDs.Add(2993889924U, "ExAdminAuditLogsUpdateDenied");
			ServerStrings.stringIDs.Add(949829336U, "ExInvalidNumberOfOccurrences");
			ServerStrings.stringIDs.Add(1342791165U, "OleConversionFailed");
			ServerStrings.stringIDs.Add(1551862906U, "ClientCulture_0x3401");
			ServerStrings.stringIDs.Add(346824164U, "ExCannotReadFolderData");
			ServerStrings.stringIDs.Add(1533116792U, "InboxRuleSensitivityNormal");
			ServerStrings.stringIDs.Add(2788778759U, "SpellCheckerCatalan");
			ServerStrings.stringIDs.Add(894291518U, "TeamMailboxMessageHowToGetStarted");
			ServerStrings.stringIDs.Add(2688169016U, "ExInvalidMasterValueAndColumnLength");
			ServerStrings.stringIDs.Add(1002199416U, "MigrationBatchStatusStopping");
			ServerStrings.stringIDs.Add(2264323463U, "ClientCulture_0x440A");
			ServerStrings.stringIDs.Add(3064105318U, "ExFolderAlreadyExistsInClientState");
			ServerStrings.stringIDs.Add(3192255565U, "InvalidPermissionsEntry");
			ServerStrings.stringIDs.Add(753703675U, "ConversionInternalFailure");
			ServerStrings.stringIDs.Add(150715741U, "MigrationTypeExchangeOutlookAnywhere");
			ServerStrings.stringIDs.Add(630982379U, "ClientCulture_0x4809");
			ServerStrings.stringIDs.Add(2605826718U, "MigrationAutodiscoverConfigurationFailure");
			ServerStrings.stringIDs.Add(273958411U, "ExDefaultContactFilename");
			ServerStrings.stringIDs.Add(687935806U, "TeamMailboxMessageReopenClosedSiteMailbox");
			ServerStrings.stringIDs.Add(3547809333U, "MigrationObjectsCountStringPFs");
			ServerStrings.stringIDs.Add(1305341899U, "ExCannotCreateRecurringMeetingWithoutTimeZone");
			ServerStrings.stringIDs.Add(1790240390U, "ExInvalidSaveOnCorrelatedItem");
			ServerStrings.stringIDs.Add(2801555237U, "ErrorTeamMailboxGetListItemChangesNullResponse");
			ServerStrings.stringIDs.Add(3339597578U, "ExCorruptedTimeZone");
			ServerStrings.stringIDs.Add(1540857282U, "MigrationUserStatusSummaryStopped");
			ServerStrings.stringIDs.Add(113768464U, "InboxRuleSensitivityCompanyConfidential");
			ServerStrings.stringIDs.Add(3597498295U, "FailedToAddAttachments");
			ServerStrings.stringIDs.Add(379773582U, "MapiCannotDeliverItem");
			ServerStrings.stringIDs.Add(2576522508U, "MapiCannotGetLocalRepIds");
			ServerStrings.stringIDs.Add(3647547459U, "ClientCulture_0x3C01");
			ServerStrings.stringIDs.Add(209873198U, "FirstDay");
			ServerStrings.stringIDs.Add(4287963599U, "ClientCulture_0x41D");
			ServerStrings.stringIDs.Add(3944203608U, "PersonIsAlreadyLinkedWithGALContact");
			ServerStrings.stringIDs.Add(303788285U, "InboxRuleMessageTypeCalendaring");
			ServerStrings.stringIDs.Add(3653626825U, "Editor");
			ServerStrings.stringIDs.Add(4226852782U, "CannotShareOtherPersonalFolder");
			ServerStrings.stringIDs.Add(540789371U, "TeamMailboxMessageClosedBodyIntroText");
			ServerStrings.stringIDs.Add(2800053981U, "InboxRuleMessageTypeSigned");
			ServerStrings.stringIDs.Add(1894870732U, "MigrationTypePSTImport");
			ServerStrings.stringIDs.Add(179758620U, "DateRangeOneYear");
			ServerStrings.stringIDs.Add(1682339598U, "ExAuditsFolderAccessDenied");
			ServerStrings.stringIDs.Add(1908093332U, "ClientCulture_0x1409");
			ServerStrings.stringIDs.Add(3485169071U, "NoExternalOwaAvailableException");
			ServerStrings.stringIDs.Add(60274512U, "DelegateNotSupportedFolder");
			ServerStrings.stringIDs.Add(3906173474U, "MigrationBatchDirectionLocal");
			ServerStrings.stringIDs.Add(3130219463U, "ErrorFolderDeleted");
			ServerStrings.stringIDs.Add(3911406152U, "BadDateTimeFormatInChangeDate");
			ServerStrings.stringIDs.Add(1489962389U, "ClientCulture_0x1C0A");
			ServerStrings.stringIDs.Add(2011085458U, "TeamMailboxMessageNoActionText");
			ServerStrings.stringIDs.Add(4201517212U, "InvalidMigrationBatchId");
			ServerStrings.stringIDs.Add(3924992590U, "FolderRuleStageOnPromotedMessage");
			ServerStrings.stringIDs.Add(1870702413U, "idUnableToCreateDefaultTaskGroupException");
			ServerStrings.stringIDs.Add(194841891U, "PublicFolderStartSyncFolderHierarchyRpcFailed");
			ServerStrings.stringIDs.Add(149719578U, "SearchStateFailed");
			ServerStrings.stringIDs.Add(1551862809U, "ClientCulture_0x2401");
			ServerStrings.stringIDs.Add(3503392803U, "MapiCannotWritePerUserInformation");
			ServerStrings.stringIDs.Add(2750670341U, "SearchNameCharacterConstraint");
			ServerStrings.stringIDs.Add(4022186804U, "InboxRuleMessageTypeCalendaringResponse");
			ServerStrings.stringIDs.Add(2114183459U, "ErrorTimeProposalInvalidWhenNotAllowedByOrganizer");
			ServerStrings.stringIDs.Add(981561289U, "RequestStateCertExpired");
			ServerStrings.stringIDs.Add(1760294240U, "Thursday");
			ServerStrings.stringIDs.Add(1553527118U, "MapiCannotDeleteAttachment");
			ServerStrings.stringIDs.Add(1512287666U, "ExEventsNotSupportedForDelegateUser");
			ServerStrings.stringIDs.Add(4181674605U, "AsyncOperationTypeImportPST");
			ServerStrings.stringIDs.Add(4056598008U, "MigrationStepDataMigration");
			ServerStrings.stringIDs.Add(2239334052U, "JunkEmailBlockedListXsoTooManyException");
			ServerStrings.stringIDs.Add(2721879535U, "ClientCulture_0x409");
			ServerStrings.stringIDs.Add(3828388569U, "ErrorFolderCreationIsBlocked");
			ServerStrings.stringIDs.Add(3332812272U, "ExInvalidParticipantEntryId");
			ServerStrings.stringIDs.Add(3512980427U, "ExInvalidSpecifierValueError");
			ServerStrings.stringIDs.Add(4064061502U, "TeamMailboxSyncStatusDocumentSyncFailureOnly");
			ServerStrings.stringIDs.Add(2549870927U, "SearchStateInProgress");
			ServerStrings.stringIDs.Add(2171204805U, "JunkEmailInvalidOperationException");
			ServerStrings.stringIDs.Add(1029618557U, "TeamMailboxMessageWhatIsSiteMailbox");
			ServerStrings.stringIDs.Add(2603495179U, "SpellCheckerFrench");
			ServerStrings.stringIDs.Add(192606181U, "ExUnknownRecurrenceBlobRange");
			ServerStrings.stringIDs.Add(1753710770U, "ExAttachmentCannotOpenDueToUnSave");
			ServerStrings.stringIDs.Add(3125164062U, "ClientCulture_0x439");
			ServerStrings.stringIDs.Add(3837865737U, "SpellCheckerEnglishCanada");
			ServerStrings.stringIDs.Add(3771526820U, "MapiCannotUpdateDeferredActionMessages");
			ServerStrings.stringIDs.Add(2014714852U, "MigrationStatisticsCompleteStatus");
			ServerStrings.stringIDs.Add(737023398U, "OleConversionInvalidResultType");
			ServerStrings.stringIDs.Add(3684140834U, "UnableToMakeAutoDiscoveryRequest");
			ServerStrings.stringIDs.Add(4266601947U, "NotificationEmailSubjectImportPst");
			ServerStrings.stringIDs.Add(1338408148U, "SharingMessageAttachmentNotFoundException");
			ServerStrings.stringIDs.Add(2642389795U, "MigrationBatchStatusIncrementalSyncing");
			ServerStrings.stringIDs.Add(4084900412U, "SearchStateStopped");
			ServerStrings.stringIDs.Add(2354834546U, "PublicFolderMailboxNotFound");
			ServerStrings.stringIDs.Add(3021899907U, "MapiCannotGetReceiveFolder");
			ServerStrings.stringIDs.Add(445051701U, "MigrationUserStatusStarting");
			ServerStrings.stringIDs.Add(2210183777U, "AmDbMountNotAllowedDueToLossException");
			ServerStrings.stringIDs.Add(770684443U, "SpellCheckerGermanPreReform");
			ServerStrings.stringIDs.Add(2412724648U, "InvalidKindFormat");
			ServerStrings.stringIDs.Add(905358579U, "OleUnableToReadAttachment");
			ServerStrings.stringIDs.Add(2666546556U, "InvalidModifier");
			ServerStrings.stringIDs.Add(3840447572U, "MigrationUserStatusProvisionUpdating");
			ServerStrings.stringIDs.Add(594875070U, "MigrationMailboxPermissionFullAccess");
			ServerStrings.stringIDs.Add(3928045320U, "WeatherUnitCelsius");
			ServerStrings.stringIDs.Add(2953752128U, "NullDateInChangeDate");
			ServerStrings.stringIDs.Add(4003777722U, "ClientCulture_0x2C09");
			ServerStrings.stringIDs.Add(2084642916U, "MigrationBatchDirectionOffboarding");
			ServerStrings.stringIDs.Add(194112229U, "CalNotifTypeUninteresting");
			ServerStrings.stringIDs.Add(2088104068U, "AmDatabaseNeverMountedException");
			ServerStrings.stringIDs.Add(870651383U, "MapiCannotSetReceiveFolder");
			ServerStrings.stringIDs.Add(3000352360U, "RpcClientWrapperFailedToLoadTopology");
			ServerStrings.stringIDs.Add(3609586398U, "ExCorruptConversationActionItem");
			ServerStrings.stringIDs.Add(2820941203U, "Tuesday");
			ServerStrings.stringIDs.Add(3647326574U, "MapiCannotCreateRestriction");
			ServerStrings.stringIDs.Add(2969695521U, "CorruptJunkMoveStamp");
			ServerStrings.stringIDs.Add(599296811U, "InvalidAttachmentNumber");
			ServerStrings.stringIDs.Add(3125164046U, "ClientCulture_0x83E");
			ServerStrings.stringIDs.Add(4094875965U, "Friday");
			ServerStrings.stringIDs.Add(1586627622U, "NoServerValueAvailable");
			ServerStrings.stringIDs.Add(1690131397U, "DelegateInvalidPermission");
			ServerStrings.stringIDs.Add(1769372998U, "OperationAborted");
			ServerStrings.stringIDs.Add(380587417U, "DiscoveryMailboxNotFound");
			ServerStrings.stringIDs.Add(1559080126U, "ClientCulture_0x422");
			ServerStrings.stringIDs.Add(434577051U, "MigrationUserStatusSummarySynced");
			ServerStrings.stringIDs.Add(656727185U, "FourHours");
			ServerStrings.stringIDs.Add(3033708196U, "MigrationUserStatusCompleted");
			ServerStrings.stringIDs.Add(3797097900U, "ExVotingBlobCorrupt");
			ServerStrings.stringIDs.Add(912807925U, "HexadecimalHtmlColorPatternDescription");
			ServerStrings.stringIDs.Add(3420516204U, "MigrationStepInitialization");
			ServerStrings.stringIDs.Add(3118133575U, "TeamMailboxSyncStatusMembershipSyncFailureOnly");
			ServerStrings.stringIDs.Add(788681379U, "InvalidEncryptedSharedFolderDataException");
			ServerStrings.stringIDs.Add(3198793348U, "ExCorruptRestrictionFilter");
			ServerStrings.stringIDs.Add(3806649575U, "ErrorNotificationAlreadyExists");
			ServerStrings.stringIDs.Add(1743342709U, "ExItemIsOpenedInReadOnlyMode");
			ServerStrings.stringIDs.Add(2479239329U, "UnbalancedParenthesis");
			ServerStrings.stringIDs.Add(3259603877U, "InvalidRpMsgFormat");
			ServerStrings.stringIDs.Add(586169060U, "UserPhotoNotAnImage");
			ServerStrings.stringIDs.Add(1079495974U, "MapiCannotCreateEntryIdFromShortTermId");
			ServerStrings.stringIDs.Add(2721879405U, "ClientCulture_0x807");
			ServerStrings.stringIDs.Add(3407343648U, "MapiCannotCreateBookmark");
			ServerStrings.stringIDs.Add(630534408U, "InvalidateDateRange");
			ServerStrings.stringIDs.Add(1290997994U, "MigrationUserAdminTypeUnknown");
			ServerStrings.stringIDs.Add(2519000101U, "CannotMoveOrCopyBetweenPrivateAndPublicMailbox");
			ServerStrings.stringIDs.Add(2234262993U, "SpellCheckerNorwegianBokmal");
			ServerStrings.stringIDs.Add(3279312390U, "ClientCulture_0x1007");
			ServerStrings.stringIDs.Add(3025564610U, "MigrationBatchSupportedActionStop");
			ServerStrings.stringIDs.Add(1295345518U, "CrossForestNotSupported");
			ServerStrings.stringIDs.Add(2475132438U, "ExCannotAccessSystemFolderId");
			ServerStrings.stringIDs.Add(4287963483U, "ClientCulture_0x410");
			ServerStrings.stringIDs.Add(153308512U, "ExStatefulFilterMustBeSetWhenSetSyncFiltersIsInvokedWithNullFilter");
			ServerStrings.stringIDs.Add(2948566116U, "MapiCannotReadPermissions");
			ServerStrings.stringIDs.Add(3983802523U, "FolderRuleStageOnPublicFolderBefore");
			ServerStrings.stringIDs.Add(3551282863U, "UnbalancedQuote");
			ServerStrings.stringIDs.Add(2092669490U, "FailedToWriteActivityLog");
			ServerStrings.stringIDs.Add(3948653022U, "NoFreeBusyFolder");
			ServerStrings.stringIDs.Add(2721879534U, "ClientCulture_0x408");
			ServerStrings.stringIDs.Add(4050068885U, "SharingConflictException");
			ServerStrings.stringIDs.Add(329227683U, "MapiCannotQueryColumns");
			ServerStrings.stringIDs.Add(914093861U, "ExCaughtMapiExceptionWhileReadingEvents");
			ServerStrings.stringIDs.Add(4287963459U, "ClientCulture_0x81D");
			ServerStrings.stringIDs.Add(1324407795U, "ConversionInvalidSmimeContent");
			ServerStrings.stringIDs.Add(1786112539U, "ExCannotRevertSentMeetingToAppointment");
			ServerStrings.stringIDs.Add(1566523742U, "ExMustSaveFolderToMakeVisibleToOutlook");
			ServerStrings.stringIDs.Add(309976908U, "UnsupportedContentRestriction");
			ServerStrings.stringIDs.Add(1559080244U, "ClientCulture_0x42D");
			ServerStrings.stringIDs.Add(443718431U, "NotificationEmailBodyImportPSTCreated");
			ServerStrings.stringIDs.Add(59886577U, "SearchTargetInSource");
			ServerStrings.stringIDs.Add(809558091U, "ExAddItemAttachmentFailed");
			ServerStrings.stringIDs.Add(2721879541U, "ClientCulture_0x403");
			ServerStrings.stringIDs.Add(1083109339U, "ExCannotMoveOrDeleteDefaultFolders");
			ServerStrings.stringIDs.Add(3923324564U, "ExCannotSeekRow");
			ServerStrings.stringIDs.Add(3620611820U, "MigrationReportBatch");
			ServerStrings.stringIDs.Add(2187234323U, "ExErrorInDetectE15Store");
			ServerStrings.stringIDs.Add(2391045796U, "idDefaultFoldersNotLocalizedException");
			ServerStrings.stringIDs.Add(2969986172U, "MigrationStateCompleted");
			ServerStrings.stringIDs.Add(297411134U, "ErrorMissingMailboxOrPermission");
			ServerStrings.stringIDs.Add(1101524278U, "ClientCulture_0x140C");
			ServerStrings.stringIDs.Add(2192010005U, "MigrationTypeIMAP");
			ServerStrings.stringIDs.Add(630982445U, "ClientCulture_0x2809");
			ServerStrings.stringIDs.Add(1955147499U, "ClientCulture_0x1404");
			ServerStrings.stringIDs.Add(984546473U, "ExCannotRejectDeletes");
			ServerStrings.stringIDs.Add(415920568U, "TeamMailboxSyncStatusMaintenanceSyncFailureOnly");
			ServerStrings.stringIDs.Add(564935456U, "MigrationStateStopped");
			ServerStrings.stringIDs.Add(3735242122U, "MigrationStageDiscovery");
			ServerStrings.stringIDs.Add(2721879655U, "ClientCulture_0x40A");
			ServerStrings.stringIDs.Add(3791574374U, "NotificationEmailBodyCertExpired");
			ServerStrings.stringIDs.Add(1657454002U, "ExCannotRejectSameOperationTwice");
			ServerStrings.stringIDs.Add(3371920351U, "ExCannotGetSearchCriteria");
			ServerStrings.stringIDs.Add(3800790638U, "ExInvalidMaxQueueSize");
			ServerStrings.stringIDs.Add(1437123480U, "ADException");
			ServerStrings.stringIDs.Add(653269905U, "ExNoMailboxOwner");
			ServerStrings.stringIDs.Add(4002989775U, "ExNotConnected");
			ServerStrings.stringIDs.Add(478700673U, "SearchStateStopping");
			ServerStrings.stringIDs.Add(1750841907U, "SpellCheckerKorean");
			ServerStrings.stringIDs.Add(1435478051U, "MigrationTypeExchangeLocalMove");
			ServerStrings.stringIDs.Add(2120544431U, "MapiCannotSubmitMessage");
			ServerStrings.stringIDs.Add(4003777885U, "ClientCulture_0x1C09");
			ServerStrings.stringIDs.Add(1458958954U, "ExInvalidOrder");
			ServerStrings.stringIDs.Add(725138746U, "NoProviderSupportShareFolder");
			ServerStrings.stringIDs.Add(3332361397U, "ExConnectionCacheSizeNotSet");
			ServerStrings.stringIDs.Add(4100720225U, "MigrationFlagsRemove");
			ServerStrings.stringIDs.Add(2084653907U, "ExInvalidRecipient");
			ServerStrings.stringIDs.Add(1532066664U, "ExFoundInvalidRowType");
			ServerStrings.stringIDs.Add(3156797033U, "ExInvalidOffset");
			ServerStrings.stringIDs.Add(2795849522U, "NotEnoughPermissionsToPerformOperation");
			ServerStrings.stringIDs.Add(3321416304U, "MigrationStatisticsPartiallyCompleteStatus");
			ServerStrings.stringIDs.Add(1147224948U, "TeamMailboxSyncStatusNotAvailable");
			ServerStrings.stringIDs.Add(3791300057U, "InvalidOperator");
			ServerStrings.stringIDs.Add(1063299331U, "DefaultHtmlAttachmentHrefText");
			ServerStrings.stringIDs.Add(2883857825U, "ConversionBodyCorrupt");
			ServerStrings.stringIDs.Add(2358432026U, "ClientCulture_0x1407");
			ServerStrings.stringIDs.Add(2091534526U, "MapiCannotSaveChanges");
			ServerStrings.stringIDs.Add(1712595778U, "RejectedSuggestionPersonIdSameAsPersonId");
			ServerStrings.stringIDs.Add(2860711473U, "ErrorInvalidPhoneNumberFormat");
			ServerStrings.stringIDs.Add(1551718751U, "MigrationStateCorrupted");
			ServerStrings.stringIDs.Add(3286105202U, "ProvisioningRequestCsvContainsNeitherPasswordNorFederatedIdentity");
			ServerStrings.stringIDs.Add(3657645519U, "SecurityPrincipalAlreadyDefined");
			ServerStrings.stringIDs.Add(968748330U, "KqlParseException");
			ServerStrings.stringIDs.Add(2414329026U, "ExEventNotFound");
			ServerStrings.stringIDs.Add(1227907325U, "ThreeDays");
			ServerStrings.stringIDs.Add(278097122U, "ExInvalidSortLength");
			ServerStrings.stringIDs.Add(3752161350U, "MapiCannotGetPerUserLongTermIds");
			ServerStrings.stringIDs.Add(2656172439U, "ExFolderWithoutMapiProp");
			ServerStrings.stringIDs.Add(3755862658U, "ExChangeKeyTooLong");
			ServerStrings.stringIDs.Add(2902058685U, "ExUnknownRestrictionType");
			ServerStrings.stringIDs.Add(91197877U, "ExInvalidRowCount");
			ServerStrings.stringIDs.Add(2720991162U, "UnsupportedExistRestriction");
			ServerStrings.stringIDs.Add(4121599705U, "AvailabilityOnly");
			ServerStrings.stringIDs.Add(1399515408U, "MapiCannotExecuteWithInternalAccess");
			ServerStrings.stringIDs.Add(2879753762U, "ExItemNoParentId");
			ServerStrings.stringIDs.Add(1107465087U, "MigrationTypePublicFolder");
			ServerStrings.stringIDs.Add(130796183U, "MapiCannotGetPerUserGuid");
			ServerStrings.stringIDs.Add(2521346493U, "FederationNotEnabled");
			ServerStrings.stringIDs.Add(241258410U, "RequestStateWaitingForFinalization");
			ServerStrings.stringIDs.Add(1176987915U, "RequestStateCompleted");
			ServerStrings.stringIDs.Add(493948276U, "TooManyCultures");
			ServerStrings.stringIDs.Add(3358444168U, "MapiCannotSetCollapseState");
			ServerStrings.stringIDs.Add(1834533699U, "IncompleteUserInformationToAccessGroupMailbox");
			ServerStrings.stringIDs.Add(2116512813U, "ClientCulture_0x2001");
			ServerStrings.stringIDs.Add(1016962785U, "CannotImportMessageChange");
			ServerStrings.stringIDs.Add(3299931409U, "InvalidTimesInTimeSlot");
			ServerStrings.stringIDs.Add(3086195034U, "MigrationReportFinalizationFailure");
			ServerStrings.stringIDs.Add(3050947916U, "StructuredQueryException");
			ServerStrings.stringIDs.Add(628804796U, "ExUnknownResponseType");
			ServerStrings.stringIDs.Add(2214930724U, "RequestStateCreated");
			ServerStrings.stringIDs.Add(2474224463U, "ExInvalidComparisonOperatorInComparisonFilter");
			ServerStrings.stringIDs.Add(3920537899U, "MigrationFolderSettings");
			ServerStrings.stringIDs.Add(2721879419U, "ClientCulture_0x809");
			ServerStrings.stringIDs.Add(3653170115U, "ExUnsupportedSeekReference");
			ServerStrings.stringIDs.Add(3031733457U, "MigrationBatchStatusCompleted");
			ServerStrings.stringIDs.Add(713262539U, "MigrationTestMSAWarning");
			ServerStrings.stringIDs.Add(2595969499U, "InvalidDateTimeRange");
			ServerStrings.stringIDs.Add(2125622397U, "MapiCannotGetMapiTable");
			ServerStrings.stringIDs.Add(1130047631U, "MapiCannotCheckForNotifications");
			ServerStrings.stringIDs.Add(977128087U, "CannotStamplocalFreeBusyId");
			ServerStrings.stringIDs.Add(2828973696U, "ClientCulture_0x100A");
			ServerStrings.stringIDs.Add(1893783426U, "MigrationBatchStatusSynced");
			ServerStrings.stringIDs.Add(2428434059U, "ExchangePrincipalFromMailboxDataError");
			ServerStrings.stringIDs.Add(3615217052U, "MigrationUserStatusIncrementalFailed");
			ServerStrings.stringIDs.Add(3321965778U, "InvalidXml");
			ServerStrings.stringIDs.Add(223559137U, "ExEntryIdToLong");
			ServerStrings.stringIDs.Add(1559080128U, "ClientCulture_0x420");
			ServerStrings.stringIDs.Add(697160562U, "PrincipalFromDifferentSite");
			ServerStrings.stringIDs.Add(3415665237U, "ErrorSavingRules");
			ServerStrings.stringIDs.Add(1802620674U, "PublishedFolderAccessDeniedException");
			ServerStrings.stringIDs.Add(305670746U, "PublicFoldersNotEnabledForEnterprise");
			ServerStrings.stringIDs.Add(3382673089U, "InboxRuleMessageTypeApprovalRequest");
			ServerStrings.stringIDs.Add(4042709143U, "NonUniqueRecipientError");
			ServerStrings.stringIDs.Add(3548482161U, "ExSystemFolderAccessDenied");
			ServerStrings.stringIDs.Add(3379789351U, "MapiCannotRemoveNotification");
			ServerStrings.stringIDs.Add(1699673688U, "ClientCulture_0x180A");
			ServerStrings.stringIDs.Add(3770139034U, "ExCommentFilterPropertiesNotSupported");
			ServerStrings.stringIDs.Add(2450087029U, "ExDictionaryDataCorruptedNullKey");
			ServerStrings.stringIDs.Add(3165677092U, "MigrationBatchStatusStarting");
			ServerStrings.stringIDs.Add(2828973630U, "ClientCulture_0x300A");
			ServerStrings.stringIDs.Add(1987097643U, "ExBadValueForTypeCode0");
			ServerStrings.stringIDs.Add(510893832U, "ErrorTimeProposalInvalidOnRecurringMaster");
			ServerStrings.stringIDs.Add(2114464927U, "SearchStateDeletionInProgress");
			ServerStrings.stringIDs.Add(1600317547U, "ExRuleIdInvalid");
			ServerStrings.stringIDs.Add(3959385997U, "MapiCannotCollapseRow");
			ServerStrings.stringIDs.Add(3606405084U, "SharingUnableToGenerateEncryptedSharedFolderData");
			ServerStrings.stringIDs.Add(3825572190U, "ExConnectionNotCached");
			ServerStrings.stringIDs.Add(931793818U, "CVSPopulationTimedout");
			ServerStrings.stringIDs.Add(300160841U, "BadDateFormatInChangeDate");
			ServerStrings.stringIDs.Add(391772390U, "MigrationBatchStatusCompletedWithErrors");
			ServerStrings.stringIDs.Add(765916263U, "NotReadSubjectPrefix");
			ServerStrings.stringIDs.Add(1428661747U, "MapiCannotFinishSubmit");
			ServerStrings.stringIDs.Add(2721875976U, "ClientCulture_0xC01");
			ServerStrings.stringIDs.Add(2811676186U, "ExItemNotFoundInClientManifest");
			ServerStrings.stringIDs.Add(2612888606U, "ErrorNoStoreObjectId");
			ServerStrings.stringIDs.Add(2194255608U, "CalendarItemCorrelationFailed");
			ServerStrings.stringIDs.Add(260083086U, "ExInvalidOccurrenceId");
			ServerStrings.stringIDs.Add(2814533567U, "DateRangeOneWeek");
			ServerStrings.stringIDs.Add(2512850293U, "EnforceRulesQuota");
			ServerStrings.stringIDs.Add(1837976028U, "ExInvalidMonth");
			ServerStrings.stringIDs.Add(3370782263U, "MigrationUserStatusCompletionSynced");
			ServerStrings.stringIDs.Add(1880367925U, "FirstFullWeek");
			ServerStrings.stringIDs.Add(3254542318U, "MigrationFeatureEndpoints");
			ServerStrings.stringIDs.Add(1291493387U, "ExNoSearchHasBeenInitiated");
			ServerStrings.stringIDs.Add(1916012218U, "MigrationUserStatusIncrementalSyncing");
			ServerStrings.stringIDs.Add(2113494780U, "PublicFolderMailboxesCannotBeCreatedDuringMigration");
			ServerStrings.stringIDs.Add(2841260818U, "MapiCannotCreateFilter");
			ServerStrings.stringIDs.Add(932673369U, "MapiCannotNotifyMessageNewMail");
			ServerStrings.stringIDs.Add(1644908980U, "MigrationUserStatusSyncing");
			ServerStrings.stringIDs.Add(2220521549U, "MigrationBatchFlagForceNewMigration");
			ServerStrings.stringIDs.Add(2553874506U, "CannotGetFinalStateSynchronizerProviderBase");
			ServerStrings.stringIDs.Add(2057374892U, "ServerLocatorClientWCFCallCommunicationError");
			ServerStrings.stringIDs.Add(553140815U, "ExValueCannotBeNull");
			ServerStrings.stringIDs.Add(2472743270U, "ClientCulture_0x3009");
			ServerStrings.stringIDs.Add(2764365677U, "MigrationTypeBulkProvisioning");
			ServerStrings.stringIDs.Add(156083260U, "ErrorFolderIsMailEnabled");
			ServerStrings.stringIDs.Add(2030923649U, "ExCantAccessOccurrenceFromNewItem");
			ServerStrings.stringIDs.Add(1309170924U, "ConversionCorruptContent");
			ServerStrings.stringIDs.Add(2021640990U, "AutoDFailedToGetToken");
			ServerStrings.stringIDs.Add(4205544601U, "ExCorruptPropertyTag");
			ServerStrings.stringIDs.Add(77158404U, "InvalidTimeSlot");
			ServerStrings.stringIDs.Add(3242781799U, "ExCannotOpenMultipleCorrelatedItems");
			ServerStrings.stringIDs.Add(902677431U, "ErrorLanguageIsNull");
			ServerStrings.stringIDs.Add(372387097U, "ExInvalidAcrBaseProfiles");
			ServerStrings.stringIDs.Add(2802946662U, "ExMustSaveFolderToApplySearch");
			ServerStrings.stringIDs.Add(2753071453U, "ExReadTopologyTimeout");
			ServerStrings.stringIDs.Add(3333234210U, "ExUnknownRecurrenceBlobType");
			ServerStrings.stringIDs.Add(4287963476U, "ClientCulture_0x419");
			ServerStrings.stringIDs.Add(113369212U, "SpellCheckerHebrew");
			ServerStrings.stringIDs.Add(2116512976U, "ClientCulture_0x1001");
			ServerStrings.stringIDs.Add(1820555509U, "InvalidAttachmentId");
			ServerStrings.stringIDs.Add(3125164183U, "ClientCulture_0x43F");
			ServerStrings.stringIDs.Add(899245499U, "ExInvalidFolderId");
			ServerStrings.stringIDs.Add(1012814431U, "AmDbMountNotAllowedDueToRegistryConfigurationException");
			ServerStrings.stringIDs.Add(911781703U, "CannotSaveReadOnlyAttachment");
			ServerStrings.stringIDs.Add(3787888884U, "InvalidTnef");
			ServerStrings.stringIDs.Add(392648307U, "MigrationUserStatusIncrementalSynced");
			ServerStrings.stringIDs.Add(2071885718U, "ExAdminAuditLogsDeleteDenied");
			ServerStrings.stringIDs.Add(2114545814U, "ConversionInvalidMessageCodepageCharset");
			ServerStrings.stringIDs.Add(2721879653U, "ClientCulture_0x40C");
			ServerStrings.stringIDs.Add(2379279263U, "DumpsterStatusShutdownException");
			ServerStrings.stringIDs.Add(1345910406U, "CannotDeleteRootFolder");
			ServerStrings.stringIDs.Add(1856386122U, "MapiCannotGetEffectiveRights");
			ServerStrings.stringIDs.Add(3136620084U, "InvalidMechanismToAccessGroupMailbox");
			ServerStrings.stringIDs.Add(3536723681U, "MapiCannotSavePermissions");
			ServerStrings.stringIDs.Add(2876027863U, "ClientCulture_0x1004");
			ServerStrings.stringIDs.Add(3588257392U, "MigrationBatchStatusCreated");
			ServerStrings.stringIDs.Add(2776843979U, "NotAllowedExternalSharingByPolicy");
			ServerStrings.stringIDs.Add(1311410701U, "InboxRuleMessageTypeReadReceipt");
			ServerStrings.stringIDs.Add(1490728717U, "StoreOperationFailed");
			ServerStrings.stringIDs.Add(33074401U, "ErrorExTimeZoneValueNoGmtMatch");
			ServerStrings.stringIDs.Add(3552080970U, "ExStoreObjectValidationError");
			ServerStrings.stringIDs.Add(1010688174U, "MigrationBatchFlagNone");
			ServerStrings.stringIDs.Add(2472743107U, "ClientCulture_0x4009");
			ServerStrings.stringIDs.Add(3188685413U, "TooManyAttachmentsOnProtectedMessage");
			ServerStrings.stringIDs.Add(3397519574U, "PublicFolderOpenFailedOnExistingFolder");
			ServerStrings.stringIDs.Add(1522765994U, "ExInvalidSortOrder");
			ServerStrings.stringIDs.Add(3678175251U, "ReplyRuleNotSupportedOnNonMailPublicFolder");
			ServerStrings.stringIDs.Add(2626172332U, "ExGetPropsFailed");
			ServerStrings.stringIDs.Add(2344409522U, "EstimateStateSucceeded");
			ServerStrings.stringIDs.Add(2621679806U, "MigrationBatchSupportedActionRemove");
			ServerStrings.stringIDs.Add(2545687272U, "MapiCannotSaveMessageStream");
			ServerStrings.stringIDs.Add(3515331893U, "MapiInvalidId");
			ServerStrings.stringIDs.Add(2975245987U, "ContactLinkingMaximumNumberOfContactsPerPersonError");
			ServerStrings.stringIDs.Add(2827742042U, "ConversionUnsupportedContent");
			ServerStrings.stringIDs.Add(4287122418U, "MigrationUserStatusIncrementalStopped");
			ServerStrings.stringIDs.Add(955524647U, "MapiCannotCreateMessage");
			ServerStrings.stringIDs.Add(968419245U, "InvalidSendAddressIdentity");
			ServerStrings.stringIDs.Add(1559080133U, "ClientCulture_0x425");
			ServerStrings.stringIDs.Add(2559314081U, "DisposeOOFHistoryFolder");
			ServerStrings.stringIDs.Add(1115353353U, "ExCantDeleteLastOccurrence");
			ServerStrings.stringIDs.Add(3283674777U, "MigrationReportBatchSuccess");
			ServerStrings.stringIDs.Add(4284023892U, "ErrorAccessingLargeProperty");
			ServerStrings.stringIDs.Add(208132458U, "OperationNotSupportedOnPublicFolderMailbox");
			ServerStrings.stringIDs.Add(872597966U, "ExCannotCreateMeetingCancellation");
			ServerStrings.stringIDs.Add(3533048780U, "MigrationFeaturePAW");
			ServerStrings.stringIDs.Add(2850981798U, "InboxRuleFlagStatusFlagged");
			ServerStrings.stringIDs.Add(135679156U, "JunkEmailBlockedListXsoNullException");
			ServerStrings.stringIDs.Add(3125164186U, "ClientCulture_0x43E");
			ServerStrings.stringIDs.Add(1695521020U, "TeamMailboxMessageGoToYourGroupSite");
			ServerStrings.stringIDs.Add(4287963464U, "ClientCulture_0x81A");
			ServerStrings.stringIDs.Add(2076121062U, "CannotImportMessageMove");
			ServerStrings.stringIDs.Add(1860805300U, "FifteenMinutes");
			ServerStrings.stringIDs.Add(823631819U, "OneDays");
			ServerStrings.stringIDs.Add(1680647297U, "CorruptNaturalLanguageProperty");
			ServerStrings.stringIDs.Add(2312979504U, "DumpsterStatusAlreadyStartedException");
			ServerStrings.stringIDs.Add(4217834195U, "ExCannotSetSearchCriteria");
			ServerStrings.stringIDs.Add(192747935U, "ExBadObjectType");
			ServerStrings.stringIDs.Add(298653586U, "SpellCheckerFinnish");
			ServerStrings.stringIDs.Add(3098387483U, "MigrationBatchStatusWaiting");
			ServerStrings.stringIDs.Add(4038623627U, "UnsupportedKindKeywords");
			ServerStrings.stringIDs.Add(2721879545U, "ClientCulture_0x407");
			ServerStrings.stringIDs.Add(2516633257U, "PropertyChangeMetadataParseError");
			ServerStrings.stringIDs.Add(2642058832U, "SyncFailedToCreateNewItemOrBindToExistingOne");
			ServerStrings.stringIDs.Add(3516269798U, "ConversionFailedInvalidMacBin");
			ServerStrings.stringIDs.Add(435552220U, "SpellCheckerEnglishUnitedStates");
			ServerStrings.stringIDs.Add(1551474375U, "ExContactHasNoId");
			ServerStrings.stringIDs.Add(2860195473U, "ErrorExTimeZoneValueTimeZoneNotFound");
			ServerStrings.stringIDs.Add(1337636428U, "SpellCheckerGermanPostReform");
			ServerStrings.stringIDs.Add(851950942U, "InboxRuleMessageTypePermissionControlled");
			ServerStrings.stringIDs.Add(2721879656U, "ClientCulture_0x40F");
			ServerStrings.stringIDs.Add(1310001827U, "PropertyDefinitionsValuesNotMatch");
			ServerStrings.stringIDs.Add(4287959997U, "ClientCulture_0xC1A");
			ServerStrings.stringIDs.Add(2706950694U, "DateRangeThreeMonths");
			ServerStrings.stringIDs.Add(3038510311U, "ExConnectionAlternate");
			ServerStrings.stringIDs.Add(2795037022U, "MigrationBatchSupportedActionStart");
			ServerStrings.stringIDs.Add(2721879540U, "ClientCulture_0x402");
			ServerStrings.stringIDs.Add(3256433766U, "ExCannotAccessAdminAuditLogsFolderId");
			ServerStrings.stringIDs.Add(1559080132U, "ClientCulture_0x424");
			ServerStrings.stringIDs.Add(132681062U, "MigrationStateWaiting");
			ServerStrings.stringIDs.Add(2224931539U, "MigrationStageProcessing");
			ServerStrings.stringIDs.Add(662607817U, "Database");
			ServerStrings.stringIDs.Add(1899965635U, "MapiCannotGetTransportQueueFolderId");
			ServerStrings.stringIDs.Add(252713015U, "UnsupportedAction");
			ServerStrings.stringIDs.Add(305850519U, "FolderRuleErrorInvalidRecipientEntryId");
			ServerStrings.stringIDs.Add(2536777203U, "TeamMailboxMessageGoToTheSite");
			ServerStrings.stringIDs.Add(834480874U, "TwelveHours");
			ServerStrings.stringIDs.Add(1710892423U, "MigrationStageInjection");
			ServerStrings.stringIDs.Add(3729217742U, "MapiCannotGetContentsTable");
			ServerStrings.stringIDs.Add(3285920218U, "EstimateStateStopped");
			ServerStrings.stringIDs.Add(1888800485U, "NullWorkHours");
			ServerStrings.stringIDs.Add(3088889153U, "MigrationUserStatusCompleting");
			ServerStrings.stringIDs.Add(3726325313U, "FiveMinutes");
			ServerStrings.stringIDs.Add(3449171400U, "InboxRuleMessageTypeVoicemail");
			ServerStrings.stringIDs.Add(3412096701U, "SpellCheckerPortugueseBrasil");
			ServerStrings.stringIDs.Add(561027979U, "GenericFailureRMDecryption");
			ServerStrings.stringIDs.Add(312177309U, "SpellCheckerEnglishAustralia");
			ServerStrings.stringIDs.Add(3160415695U, "NoDeferredActions");
			ServerStrings.stringIDs.Add(3022558012U, "ErrorSetDateTimeFormatWithoutLanguage");
			ServerStrings.stringIDs.Add(987212968U, "ClientCulture_0x1801");
			ServerStrings.stringIDs.Add(2113411584U, "MapiErrorParsingId");
			ServerStrings.stringIDs.Add(563787386U, "MigrationUserAdminTypePartnerTenant");
			ServerStrings.stringIDs.Add(3598152156U, "MigrationUserStatusStopped");
			ServerStrings.stringIDs.Add(909292782U, "MigrationReportBatchFailure");
			ServerStrings.stringIDs.Add(490979673U, "MapiCannotCreateAttachment");
			ServerStrings.stringIDs.Add(1946505183U, "NotificationEmailBodyCertExpiring");
			ServerStrings.stringIDs.Add(2470109498U, "MapiCannotReadPerUserInformation");
			ServerStrings.stringIDs.Add(2189690271U, "ExInvalidSubFilterProperty");
			ServerStrings.stringIDs.Add(1931248018U, "StockReplyTemplate");
			ServerStrings.stringIDs.Add(3362131860U, "CalNotifTypeSummary");
			ServerStrings.stringIDs.Add(633339293U, "JunkEmailInvalidConstructionException");
			ServerStrings.stringIDs.Add(3309334123U, "MapiCannotCreateAssociatedMessage");
			ServerStrings.stringIDs.Add(4287963482U, "ClientCulture_0x413");
			ServerStrings.stringIDs.Add(1706900424U, "MapiCannotSortTable");
			ServerStrings.stringIDs.Add(2822102721U, "MigrationUserStatusStopping");
			ServerStrings.stringIDs.Add(1778962839U, "MapiCannotGetRecipientTable");
			ServerStrings.stringIDs.Add(2932685544U, "ExInvalidCallToTryUpdateCalendarItem");
			ServerStrings.stringIDs.Add(2721879544U, "ClientCulture_0x406");
			ServerStrings.stringIDs.Add(102022121U, "ExCannotAccessAuditsFolderId");
			ServerStrings.stringIDs.Add(18047843U, "ExReadEventsFailed");
			ServerStrings.stringIDs.Add(727433418U, "ExCannotQueryAssociatedTable");
			ServerStrings.stringIDs.Add(1559080121U, "ClientCulture_0x429");
			ServerStrings.stringIDs.Add(2303146172U, "MigrationStepProvisioningUpdate");
			ServerStrings.stringIDs.Add(1311803567U, "TwoWeeks");
			ServerStrings.stringIDs.Add(4021122445U, "MigrationFeatureUpgradeBlock");
			ServerStrings.stringIDs.Add(1530529173U, "ExInvalidServiceType");
			ServerStrings.stringIDs.Add(3873491039U, "NullTimeInChangeDate");
			ServerStrings.stringIDs.Add(1192664468U, "ConversionInvalidSmimeClearSignedContent");
			ServerStrings.stringIDs.Add(2464640173U, "RequestStateSuspended");
			ServerStrings.stringIDs.Add(2393270488U, "MapiIsFromPublicStoreCheckFailed");
			ServerStrings.stringIDs.Add(4172275237U, "ExCannotSendMeetingMessages");
			ServerStrings.stringIDs.Add(2982637405U, "ExAuditsDeleteDenied");
			ServerStrings.stringIDs.Add(4287963485U, "ClientCulture_0x416");
			ServerStrings.stringIDs.Add(4113710984U, "MissingPropertyValue");
			ServerStrings.stringIDs.Add(3202920824U, "FolderNotPublishedException");
			ServerStrings.stringIDs.Add(1807895935U, "ServerLocatorClientEndpointNotFoundException");
			ServerStrings.stringIDs.Add(988424741U, "ExTooComplexGroupSortParameter");
			ServerStrings.stringIDs.Add(220684915U, "MapiCannotLookupEntryId");
			ServerStrings.stringIDs.Add(543772960U, "NotificationEmailBodyExportPSTCreated");
			ServerStrings.stringIDs.Add(1291339327U, "TeamMailboxMessageReactivatedBodyIntroText");
			ServerStrings.stringIDs.Add(1027067688U, "NotSupportedWithMailboxVersionException");
			ServerStrings.stringIDs.Add(1908093266U, "ClientCulture_0x3409");
			ServerStrings.stringIDs.Add(4287963593U, "ClientCulture_0x41B");
			ServerStrings.stringIDs.Add(1892124050U, "CannotAddAttachmentToReadOnlyCollection");
			ServerStrings.stringIDs.Add(1940443510U, "UserPhotoPreviewNotFound");
			ServerStrings.stringIDs.Add(3503580213U, "PublicFolderMailboxesCannotBeMovedDuringMigration");
			ServerStrings.stringIDs.Add(3739954134U, "MigrationBatchDirectionOnboarding");
			ServerStrings.stringIDs.Add(1702371863U, "AsyncOperationTypeMigration");
			ServerStrings.stringIDs.Add(2721879659U, "ClientCulture_0x40E");
			ServerStrings.stringIDs.Add(1959086372U, "OriginatingServer");
			ServerStrings.stringIDs.Add(3553550262U, "EstimateStatePartiallySucceeded");
			ServerStrings.stringIDs.Add(1063322282U, "CannotImportDeletion");
			ServerStrings.stringIDs.Add(668312535U, "MigrationUserStatusSynced");
			ServerStrings.stringIDs.Add(1644403520U, "CannotImportFolderChange");
			ServerStrings.stringIDs.Add(2717353738U, "MigrationUserStatusValidating");
			ServerStrings.stringIDs.Add(2473956989U, "ExConstraintNotSupportedForThisPropertyType");
			ServerStrings.stringIDs.Add(614444523U, "NotificationEmailSubjectCertExpiring");
			ServerStrings.stringIDs.Add(1918350106U, "MigrationUserStatusSummaryCompleted");
			ServerStrings.stringIDs.Add(116529921U, "SpellCheckerArabic");
			ServerStrings.stringIDs.Add(2162216975U, "InternalLicensingDisabledForEnterprise");
			ServerStrings.stringIDs.Add(2264323529U, "ClientCulture_0x240A");
			ServerStrings.stringIDs.Add(1621771988U, "RPCOperationAbortedBecauseOfAnotherRPCThread");
			ServerStrings.stringIDs.Add(2356708790U, "ExInvalidMdbGuid");
			ServerStrings.stringIDs.Add(3718014775U, "SpellCheckerEnglishUnitedKingdom");
			ServerStrings.stringIDs.Add(1886702248U, "ExFilterHierarchyIsTooDeep");
			ServerStrings.stringIDs.Add(176188277U, "MapiCannotSetMessageLockState");
			ServerStrings.stringIDs.Add(2635947676U, "CannotProtectMessageForNonSmtpSender");
			ServerStrings.stringIDs.Add(3669082289U, "ExSearchFolderIsAlreadyVisibleToOutlook");
			ServerStrings.stringIDs.Add(1534555903U, "ExEntryIdFirst4Bytes");
			ServerStrings.stringIDs.Add(4224870879U, "CustomMessageLengthExceeded");
			ServerStrings.stringIDs.Add(469526714U, "ExWrappedStreamFailure");
			ServerStrings.stringIDs.Add(3215248187U, "ErrorExTimeZoneValueWrongGmtFormat");
			ServerStrings.stringIDs.Add(2069696862U, "InternalParserError");
			ServerStrings.stringIDs.Add(2542755219U, "ExInvalidCount");
			ServerStrings.stringIDs.Add(3554710343U, "ADUserNotFound");
			ServerStrings.stringIDs.Add(2441819035U, "InboxRuleFlagStatusNotFlagged");
			ServerStrings.stringIDs.Add(1728333927U, "ConversionMustLoadAllPropeties");
			ServerStrings.stringIDs.Add(4222627801U, "ThreeHours");
			ServerStrings.stringIDs.Add(1816423621U, "MapiCannotGetIDFromNames");
			ServerStrings.stringIDs.Add(3922439094U, "ErrorSigntureTooLarge");
			ServerStrings.stringIDs.Add(2247108930U, "MigrationBatchFlagReportInitial");
			ServerStrings.stringIDs.Add(3391255931U, "ErrorTimeProposalEndTimeBeforeStartTime");
			ServerStrings.stringIDs.Add(3041727278U, "CannotSetMessageFlagStatus");
			ServerStrings.stringIDs.Add(2084296733U, "MigrationFlagsReport");
			ServerStrings.stringIDs.Add(2188504663U, "MigrationStepProvisioning");
			ServerStrings.stringIDs.Add(3392951782U, "FirstFourDayWeek");
			ServerStrings.stringIDs.Add(313625050U, "MapiCannotModifyRecipients");
			ServerStrings.stringIDs.Add(3755684524U, "ConversionCorruptSummaryTnef");
			ServerStrings.stringIDs.Add(1908093169U, "ClientCulture_0x2409");
			ServerStrings.stringIDs.Add(2511211668U, "ExAlreadyConnected");
			ServerStrings.stringIDs.Add(2066272930U, "ExReportMessageCorruptedDueToWrongItemAttachmentType");
			ServerStrings.stringIDs.Add(2828973564U, "ClientCulture_0x500A");
			ServerStrings.stringIDs.Add(4033769924U, "MigrationTypeNone");
			ServerStrings.stringIDs.Add(1112725991U, "CannotImportReadStateChange");
			ServerStrings.stringIDs.Add(2653492529U, "MapiCannotGetAttachmentTable");
			ServerStrings.stringIDs.Add(4054158457U, "MapiCannotOpenAttachment");
			ServerStrings.stringIDs.Add(4130050910U, "ExSuffixTextFilterNotSupported");
			ServerStrings.stringIDs.Add(2641324698U, "ExSeparatorNotFoundOnCompoundValue");
			ServerStrings.stringIDs.Add(4232824632U, "MigrationBatchStatusCorrupted");
			ServerStrings.stringIDs.Add(3135377227U, "MigrationBatchStatusSyncing");
			ServerStrings.stringIDs.Add(4287963488U, "ClientCulture_0x415");
			ServerStrings.stringIDs.Add(3647547362U, "ClientCulture_0x2C01");
			ServerStrings.stringIDs.Add(984518113U, "CannotAccessRemoteMailbox");
			ServerStrings.stringIDs.Add(456720827U, "MapiCannotFindRow");
			ServerStrings.stringIDs.Add(101242371U, "ThirtyMinutes");
			ServerStrings.stringIDs.Add(1022111068U, "MapiCannotSeekRow");
			ServerStrings.stringIDs.Add(2976127978U, "MigrationUserStatusFailed");
			ServerStrings.stringIDs.Add(3086681225U, "ExceptionObjectHasBeenDeleted");
			ServerStrings.stringIDs.Add(2706959952U, "MigrationBatchFlagDisallowExistingUsers");
			ServerStrings.stringIDs.Add(3884678960U, "ClientCulture_0x464");
			ServerStrings.stringIDs.Add(2868399142U, "UnsupportedPropertyRestriction");
			ServerStrings.stringIDs.Add(265623663U, "ServerLocatorClientWCFCallTimeout");
			ServerStrings.stringIDs.Add(3055339528U, "InvalidServiceLocationResponse");
			ServerStrings.stringIDs.Add(722987850U, "MapiCannotDeleteProperties");
			ServerStrings.stringIDs.Add(4146176105U, "NeedFolderIdForPublicFolder");
			ServerStrings.stringIDs.Add(1666174282U, "ClientCulture_0x100C");
			ServerStrings.stringIDs.Add(813145114U, "ManagedByRemoteExchangeOrganization");
			ServerStrings.stringIDs.Add(1511731857U, "AutoDRequestFailed");
			ServerStrings.stringIDs.Add(3680872945U, "DumpsterFolderNotFound");
			ServerStrings.stringIDs.Add(847028569U, "ExFolderNotFoundInClientState");
			ServerStrings.stringIDs.Add(2399298613U, "ImportResultContainedFailure");
			ServerStrings.stringIDs.Add(4287963350U, "ClientCulture_0x813");
			ServerStrings.stringIDs.Add(1419302978U, "ExCannotCreateMeetingResponse");
			ServerStrings.stringIDs.Add(2061760288U, "EightHours");
			ServerStrings.stringIDs.Add(277994955U, "OperationResultFailed");
			ServerStrings.stringIDs.Add(860625858U, "ErrorWorkingHoursEndTimeSmaller");
			ServerStrings.stringIDs.Add(1977152861U, "RoutingTypeRequired");
			ServerStrings.stringIDs.Add(244097521U, "FolderRuleCannotSaveItem");
			ServerStrings.stringIDs.Add(3695851705U, "RequestStateRemoving");
			ServerStrings.stringIDs.Add(1058417226U, "MigrationStateFailed");
			ServerStrings.stringIDs.Add(2568510612U, "MigrationUserStatusCompletionFailed");
			ServerStrings.stringIDs.Add(1709649557U, "MailboxSearchEwsEmptyResponse");
			ServerStrings.stringIDs.Add(2264323692U, "ClientCulture_0x140A");
			ServerStrings.stringIDs.Add(3414312136U, "MigrationUserStatusRemoving");
			ServerStrings.stringIDs.Add(1240718058U, "MigrationFolderCorruptedItems");
			ServerStrings.stringIDs.Add(4287963475U, "ClientCulture_0x418");
			ServerStrings.stringIDs.Add(1953718728U, "SpellCheckerPortuguesePortugal");
			ServerStrings.stringIDs.Add(3480868172U, "TeamMailboxMessageReactivatingText");
			ServerStrings.stringIDs.Add(1241888079U, "SearchLogFileCreateException");
			ServerStrings.stringIDs.Add(2010446754U, "ExCannotGetDeletedItem");
			ServerStrings.stringIDs.Add(496581163U, "MailboxSearchNameTooLong");
			ServerStrings.stringIDs.Add(4287963597U, "ClientCulture_0x41F");
			ServerStrings.stringIDs.Add(1908093103U, "ClientCulture_0x4409");
			ServerStrings.stringIDs.Add(3877064532U, "ExSubmissionQuotaExceeded");
			ServerStrings.stringIDs.Add(1682139678U, "ExCorruptMessageCorrelationBlob");
			ServerStrings.stringIDs.Add(3199235754U, "MigrationFolderDrumTesting");
			ServerStrings.stringIDs.Add(4044636857U, "ExCorruptFolderWebViewInfo");
			ServerStrings.stringIDs.Add(3987708908U, "MigrationBatchFlagDisableOnCopy");
			ServerStrings.stringIDs.Add(1372163092U, "ICSSynchronizationFailed");
			ServerStrings.stringIDs.Add(4036040747U, "OneHours");
			ServerStrings.stringIDs.Add(1163271202U, "InvalidBodyFormat");
			ServerStrings.stringIDs.Add(3938391037U, "PeopleQuickContactsAttributionDisplayName");
			ServerStrings.stringIDs.Add(1437739311U, "TwoHours");
			ServerStrings.stringIDs.Add(1085572452U, "ExPropertyDefinitionInMoreThanOnePropertyProfile");
			ServerStrings.stringIDs.Add(2260241133U, "TeamMailboxSyncStatusMembershipAndMaintenanceSyncFailure");
			ServerStrings.stringIDs.Add(2721876058U, "ClientCulture_0xC0C");
			ServerStrings.stringIDs.Add(3360384478U, "ExUnableToCopyAttachments");
			ServerStrings.stringIDs.Add(4014822501U, "ExCannotUpdateResponses");
			ServerStrings.stringIDs.Add(3909125539U, "ConversationItemHasNoBody");
			ServerStrings.stringIDs.Add(390622925U, "DelegateCollectionInvalidAfterSave");
			ServerStrings.stringIDs.Add(2946586312U, "TeamMailboxSyncStatusDocumentAndMaintenanceSyncFailure");
			ServerStrings.stringIDs.Add(3712347292U, "InvalidAttachmentType");
			ServerStrings.stringIDs.Add(3049568671U, "ExCannotMarkTaskCompletedWhenSuppressCreateOneOff");
			ServerStrings.stringIDs.Add(748440690U, "CannotGetPropertyList");
			ServerStrings.stringIDs.Add(1090294952U, "ErrorInvalidConfigurationXml");
			ServerStrings.stringIDs.Add(3064401295U, "InvalidBase64String");
			ServerStrings.stringIDs.Add(1573110229U, "RequestStateFailed");
			ServerStrings.stringIDs.Add(403751771U, "InboxRuleImportanceNormal");
			ServerStrings.stringIDs.Add(2718297568U, "MigrationLocalhostNotFound");
			ServerStrings.stringIDs.Add(1551862972U, "ClientCulture_0x1401");
			ServerStrings.stringIDs.Add(2594591409U, "TeamMailboxSyncStatusSucceeded");
			ServerStrings.stringIDs.Add(3678601929U, "MigrationErrorAttachmentCorrupted");
			ServerStrings.stringIDs.Add(974121200U, "OleConversionResultFailed");
			ServerStrings.stringIDs.Add(559182252U, "MigrationUserStatusSummaryFailed");
			ServerStrings.stringIDs.Add(2151518657U, "RequestStateCanceled");
			ServerStrings.stringIDs.Add(1326926126U, "ModifyRuleInStore");
			ServerStrings.stringIDs.Add(1178929403U, "ExItemDeletedInRace");
			ServerStrings.stringIDs.Add(2264323626U, "ClientCulture_0x340A");
			ServerStrings.stringIDs.Add(2482422690U, "WeatherUnitFahrenheit");
			ServerStrings.stringIDs.Add(4014230567U, "MessageNotRightsProtected");
			ServerStrings.stringIDs.Add(704719423U, "ConversionMaliciousContent");
			ServerStrings.stringIDs.Add(3550236610U, "NoTemplateMessage");
			ServerStrings.stringIDs.Add(3838981202U, "FolderRuleStageLoading");
			ServerStrings.stringIDs.Add(2310868878U, "LimitedDetails");
			ServerStrings.stringIDs.Add(3851145766U, "AppendOOFHistoryEntry");
			ServerStrings.stringIDs.Add(1180958385U, "ExStoreSessionDisconnected");
			ServerStrings.stringIDs.Add(1719015848U, "NotificationEmailSubjectCertExpired");
			ServerStrings.stringIDs.Add(1951112992U, "MigrationUserAdminTypeDCAdmin");
			ServerStrings.stringIDs.Add(1511957081U, "SpellCheckerSpanish");
			ServerStrings.stringIDs.Add(693971404U, "UserPhotoNotFound");
			ServerStrings.stringIDs.Add(3647547525U, "ClientCulture_0x1C01");
			ServerStrings.stringIDs.Add(959876171U, "MigrationBatchFlagAutoStop");
			ServerStrings.stringIDs.Add(1561549651U, "RemoteArchiveOffline");
			ServerStrings.stringIDs.Add(89761473U, "CannotOpenLocalFreeBusy");
			ServerStrings.stringIDs.Add(3320490165U, "CannotFindExchangePrincipal");
			ServerStrings.stringIDs.Add(2938839179U, "MigrationStageValidation");
			ServerStrings.stringIDs.Add(1865294994U, "CalNotifTypeReminder");
			ServerStrings.stringIDs.Add(4057666506U, "ExFailedToGetUnsearchableItems");
			ServerStrings.stringIDs.Add(3364213626U, "Monday");
			ServerStrings.stringIDs.Add(135933047U, "AsyncOperationTypeUnknown");
			ServerStrings.stringIDs.Add(3737835697U, "MigrationFolderSyncMigration");
			ServerStrings.stringIDs.Add(950284413U, "InboxRuleFlagStatusComplete");
			ServerStrings.stringIDs.Add(838610512U, "NotificationEmailSubjectMoveMailbox");
			ServerStrings.stringIDs.Add(3803104512U, "MessageRpmsgAttachmentIncorrectType");
			ServerStrings.stringIDs.Add(2897062825U, "FullDetails");
			ServerStrings.stringIDs.Add(4157021563U, "JunkEmailObjectDisposedException");
			ServerStrings.stringIDs.Add(4199032562U, "FailedToReadLocalServer");
			ServerStrings.stringIDs.Add(1207296209U, "MapiCannotGetCurrentRow");
			ServerStrings.stringIDs.Add(1074414016U, "MigrationInvalidPassword");
			ServerStrings.stringIDs.Add(2253780921U, "ExCannotDeletePropertiesOnOccurrences");
			ServerStrings.stringIDs.Add(2322466952U, "EstimateStateFailed");
			ServerStrings.stringIDs.Add(1348198184U, "RequestStateCompleting");
			ServerStrings.stringIDs.Add(4287963600U, "ClientCulture_0x41E");
			ServerStrings.stringIDs.Add(3913958124U, "InvalidSharingRecipientsException");
			ServerStrings.stringIDs.Add(3340405076U, "MapiCannotOpenFolder");
			ServerStrings.stringIDs.Add(536874274U, "ClientCulture_0x180C");
			ServerStrings.stringIDs.Add(1558200374U, "ADOperationAbortedBecauseOfAnotherADThread");
			ServerStrings.stringIDs.Add(1606180860U, "UpdateOOFHistoryOperation");
			ServerStrings.stringIDs.Add(3643161838U, "ExAttachmentAlreadyOpen");
			ServerStrings.stringIDs.Add(1606937438U, "NotificationEmailBodyImportPSTCompleted");
			ServerStrings.stringIDs.Add(2205094141U, "ExInvalidAggregate");
			ServerStrings.stringIDs.Add(2046213432U, "NotificationEmailBodyImportPSTFailed");
			ServerStrings.stringIDs.Add(3350951418U, "ExCantAccessOccurrenceFromSingle");
			ServerStrings.stringIDs.Add(3182347319U, "NotificationEmailBodyExportPSTCompleted");
			ServerStrings.stringIDs.Add(2721879411U, "ClientCulture_0x801");
			ServerStrings.stringIDs.Add(2721879543U, "ClientCulture_0x401");
			ServerStrings.stringIDs.Add(1685125049U, "CalNotifTypeNewUpdate");
			ServerStrings.stringIDs.Add(148503943U, "NoExternalEwsAvailableException");
			ServerStrings.stringIDs.Add(3737728227U, "CannotSharePublicFolder");
			ServerStrings.stringIDs.Add(705409536U, "MigrationTypeExchangeRemoteMove");
			ServerStrings.stringIDs.Add(3160872492U, "MigrationUserStatusCompletedWithWarning");
			ServerStrings.stringIDs.Add(898461441U, "FailedToParseUseLicense");
			ServerStrings.stringIDs.Add(2930877107U, "MigrationStateDisabled");
			ServerStrings.stringIDs.Add(3957837829U, "MigrationBatchSupportedActionComplete");
			ServerStrings.stringIDs.Add(3969194465U, "MapiCannotOpenEmbeddedMessage");
			ServerStrings.stringIDs.Add(2205239196U, "InboxRuleImportanceLow");
			ServerStrings.stringIDs.Add(792524531U, "NoMapiPDLs");
			ServerStrings.stringIDs.Add(1929574072U, "RmExceptionGenericMessage");
			ServerStrings.stringIDs.Add(1065277019U, "NotRead");
			ServerStrings.stringIDs.Add(2721879521U, "ClientCulture_0x80C");
			ServerStrings.stringIDs.Add(913845744U, "idUnableToAddDefaultCalendarToDefaultCalendarGroup");
			ServerStrings.stringIDs.Add(987212902U, "ClientCulture_0x3801");
			ServerStrings.stringIDs.Add(1599639819U, "MapiCannotGetAllPerUserLongTermIds");
			ServerStrings.stringIDs.Add(101151487U, "TeamMailboxMessageSiteMailboxEmailAddress");
			ServerStrings.stringIDs.Add(2267978872U, "CalNotifTypeDeletedUpdate");
			ServerStrings.stringIDs.Add(676596483U, "CannotCreateSearchFoldersInPublicStore");
			ServerStrings.stringIDs.Add(3860059626U, "ExDictionaryDataCorruptedNoField");
			ServerStrings.stringIDs.Add(3414452687U, "ExceptionFolderIsRootFolder");
			ServerStrings.stringIDs.Add(2303439835U, "DateRangeOneDay");
			ServerStrings.stringIDs.Add(4287963481U, "ClientCulture_0x412");
			ServerStrings.stringIDs.Add(2937224961U, "AppointmentTombstoneCorrupt");
			ServerStrings.stringIDs.Add(627389694U, "MigrationBatchAutoComplete");
			ServerStrings.stringIDs.Add(3368210174U, "MigrationObjectsCountStringNone");
			ServerStrings.stringIDs.Add(4287963351U, "ClientCulture_0x810");
			ServerStrings.stringIDs.Add(1064525218U, "MapiCannotCopyItem");
			ServerStrings.stringIDs.Add(1405495556U, "ErrorNoStoreObjectIdAndFolderPath");
			ServerStrings.stringIDs.Add(805861954U, "MigrationFolderSyncMigrationReports");
			ServerStrings.stringIDs.Add(2576334771U, "UnsupportedFormsCondition");
			ServerStrings.stringIDs.Add(3719801171U, "ExStartTimeNotSet");
			ServerStrings.stringIDs.Add(2721879406U, "ClientCulture_0x804");
			ServerStrings.stringIDs.Add(718334902U, "ExConversationActionItemNotFound");
			ServerStrings.stringIDs.Add(3108568302U, "MigrationUserStatusProvisioning");
			ServerStrings.stringIDs.Add(951826856U, "InboxRuleMessageTypeNonDeliveryReport");
			ServerStrings.stringIDs.Add(1983194762U, "ExFailedToUnregisterExchangeTopologyNotification");
			ServerStrings.stringIDs.Add(1453422183U, "TeamMailboxMessageLearnMore");
			ServerStrings.stringIDs.Add(1701953962U, "DateRangeThreeDays");
			ServerStrings.stringIDs.Add(2423165282U, "MapiCannotTransportSendMessage");
			ServerStrings.stringIDs.Add(1627212429U, "ExSortNotSupportedInDeepTraversalQuery");
			ServerStrings.stringIDs.Add(1452910403U, "JunkEmailAmbiguousUsernameException");
			ServerStrings.stringIDs.Add(3249771727U, "MigrationBatchStatusSyncedWithErrors");
			ServerStrings.stringIDs.Add(1714411372U, "FailedToFindAvailableHubs");
			ServerStrings.stringIDs.Add(3876212982U, "InternetCalendarName");
			ServerStrings.stringIDs.Add(11761379U, "ExItemNotFound");
			ServerStrings.stringIDs.Add(1042040859U, "ExDelegateNotSupportedRespondToMeetingRequest");
			ServerStrings.stringIDs.Add(4247577850U, "DisposeNonIPMFolder");
			ServerStrings.stringIDs.Add(2585840702U, "InboxRuleSensitivityPrivate");
			ServerStrings.stringIDs.Add(1728869634U, "MigrationFeatureNone");
			ServerStrings.stringIDs.Add(2721879547U, "ClientCulture_0x405");
			ServerStrings.stringIDs.Add(46060694U, "ExStringContainsSurroundingWhiteSpace");
			ServerStrings.stringIDs.Add(1489962160U, "ClientCulture_0x4C0A");
			ServerStrings.stringIDs.Add(630982608U, "ClientCulture_0x1809");
			ServerStrings.stringIDs.Add(2138889051U, "FailedToResealKey");
			ServerStrings.stringIDs.Add(2342985388U, "InvalidParticipantForRules");
			ServerStrings.stringIDs.Add(3831935814U, "SpellCheckerDanish");
			ServerStrings.stringIDs.Add(1280140891U, "MapiCannotGetProperties");
			ServerStrings.stringIDs.Add(1835617035U, "MapiCopyMessagesFailed");
			ServerStrings.stringIDs.Add(2765862542U, "FailedToParseValue");
			ServerStrings.stringIDs.Add(1208307535U, "RuleWriterObjectNotFound");
			ServerStrings.stringIDs.Add(975909957U, "ExInvalidWatermarkString");
			ServerStrings.stringIDs.Add(3000070570U, "ProvisioningRequestCsvContainsBothPasswordAndFederatedIdentity");
			ServerStrings.stringIDs.Add(3348992335U, "OperationResultSucceeded");
			ServerStrings.stringIDs.Add(141464842U, "ServerLocatorServicePermanentFault");
			ServerStrings.stringIDs.Add(1039613051U, "NotMailboxSession");
			ServerStrings.stringIDs.Add(1391852443U, "MigrationStateActive");
			ServerStrings.stringIDs.Add(1743625299U, "Null");
			ServerStrings.stringIDs.Add(1066460788U, "FolderRuleStageOnCreatedMessage");
			ServerStrings.stringIDs.Add(3238359831U, "CannotSetMessageFlags");
			ServerStrings.stringIDs.Add(1511089307U, "ExInvalidAsyncResult");
			ServerStrings.stringIDs.Add(987212805U, "ClientCulture_0x2801");
			ServerStrings.stringIDs.Add(3815733289U, "ExFolderPropertyBagCannotSaveChanges");
			ServerStrings.stringIDs.Add(1665468465U, "MigrationBatchStatusStopped");
			ServerStrings.stringIDs.Add(1852466064U, "KqlParserTimeout");
			ServerStrings.stringIDs.Add(3623312858U, "TenMinutes");
			ServerStrings.stringIDs.Add(2730199010U, "ExMustSetSearchCriteriaToMakeVisibleToOutlook");
			ServerStrings.stringIDs.Add(3452652986U, "Wednesday");
			ServerStrings.stringIDs.Add(2721875974U, "ClientCulture_0xC07");
			ServerStrings.stringIDs.Add(3155538965U, "LegacyMailboxSearchDescription");
			ServerStrings.stringIDs.Add(1494015926U, "MapiCannotFreeBookmark");
			ServerStrings.stringIDs.Add(4041018166U, "CannotChangePermissionsOnFolder");
			ServerStrings.stringIDs.Add(14329882U, "MapiCannotSetProps");
			ServerStrings.stringIDs.Add(1082563208U, "SearchStatePartiallySucceeded");
			ServerStrings.stringIDs.Add(1720321054U, "InboxRuleMessageTypeAutomaticReply");
			ServerStrings.stringIDs.Add(270030022U, "RuleHistoryError");
			ServerStrings.stringIDs.Add(1699673525U, "ClientCulture_0x280A");
			ServerStrings.stringIDs.Add(2116512910U, "ClientCulture_0x3001");
			ServerStrings.stringIDs.Add(422603853U, "SharePoint");
			ServerStrings.stringIDs.Add(3562004828U, "NoDelegateAction");
			ServerStrings.stringIDs.Add(3008535783U, "MigrationFlagsStop");
			ServerStrings.stringIDs.Add(2280589817U, "ExNoOptimizedCodePath");
			ServerStrings.stringIDs.Add(2285862258U, "MigrationBatchFlagUseAdvancedValidation");
			ServerStrings.stringIDs.Add(721699019U, "MapiCannotGetParentEntryId");
			ServerStrings.stringIDs.Add(3915122871U, "ExOnlyMessagesHaveParent");
			ServerStrings.stringIDs.Add(4287963484U, "ClientCulture_0x411");
			ServerStrings.stringIDs.Add(1405345367U, "ExFolderDoesNotMatchFolderId");
			ServerStrings.stringIDs.Add(2992671490U, "MigrationTypeXO1");
			ServerStrings.stringIDs.Add(273956641U, "NotOperator");
			ServerStrings.stringIDs.Add(1699673459U, "ClientCulture_0x480A");
			ServerStrings.stringIDs.Add(3478111469U, "Saturday");
			ServerStrings.stringIDs.Add(4173454943U, "ExFailedToRegisterExchangeTopologyNotification");
			ServerStrings.stringIDs.Add(1732871098U, "MigrationBatchSupportedActionSet");
			ServerStrings.stringIDs.Add(552541799U, "ConversionCannotOpenJournalMessage");
			ServerStrings.stringIDs.Add(2519002207U, "JunkEmailTrustedListXsoEmptyException");
			ServerStrings.stringIDs.Add(2181826069U, "AmFailedToFindSuitableServer");
			ServerStrings.stringIDs.Add(1299125360U, "OperationalError");
			ServerStrings.stringIDs.Add(2689882193U, "ExTooManySortColumns");
			ServerStrings.stringIDs.Add(3810093794U, "LoadRulesFromStore");
			ServerStrings.stringIDs.Add(2721879658U, "ClientCulture_0x40D");
			ServerStrings.stringIDs.Add(1282190256U, "ExCantCopyBadAlienDLMember");
			ServerStrings.stringIDs.Add(3434190160U, "TeamMailboxMessageSendMailToTheSiteMailbox");
			ServerStrings.stringIDs.Add(3610984697U, "InboxRuleSensitivityPersonal");
			ServerStrings.stringIDs.Add(2078392877U, "ExInvalidItemCountAdvisorCondition");
			ServerStrings.stringIDs.Add(3955579167U, "ErrorLoadingRules");
			ServerStrings.stringIDs.Add(4287963487U, "ClientCulture_0x414");
			ServerStrings.stringIDs.Add(1246168046U, "ExEndTimeNotSet");
			ServerStrings.stringIDs.Add(1055197681U, "InboxRuleMessageTypeAutomaticForward");
			ServerStrings.stringIDs.Add(392605258U, "MapiCannotCopyMapiProps");
			ServerStrings.stringIDs.Add(2708396435U, "OneWeeks");
			ServerStrings.stringIDs.Add(494393471U, "TeamMailboxMessageWhatYouCanDoNext");
			ServerStrings.stringIDs.Add(1985781485U, "MapiCannotGetReceiveFolderInfo");
			ServerStrings.stringIDs.Add(2870966119U, "ExInvalidStoreObjectId");
			ServerStrings.stringIDs.Add(1332658223U, "RequestStateQueued");
			ServerStrings.stringIDs.Add(1762000491U, "RecurrenceBlobCorrupted");
			ServerStrings.stringIDs.Add(3526072757U, "CannotFindAttachment");
			ServerStrings.stringIDs.Add(2356618506U, "ExInvalidRecipientBlob");
			ServerStrings.stringIDs.Add(1272751292U, "ExIncompleteBlob");
			ServerStrings.stringIDs.Add(379946208U, "ExPatternNotSet");
			ServerStrings.stringIDs.Add(1944490681U, "ExInvalidDayOfMonth");
			ServerStrings.stringIDs.Add(3835493555U, "ExInvalidGlobalObjectId");
			ServerStrings.stringIDs.Add(2185974115U, "MapiCannotGetHierarchyTable");
			ServerStrings.stringIDs.Add(2721876056U, "ClientCulture_0xC0A");
			ServerStrings.stringIDs.Add(2799724840U, "ExInvalidFullyQualifiedServerName");
			ServerStrings.stringIDs.Add(751268339U, "EstimateStateInProgress");
			ServerStrings.stringIDs.Add(919965275U, "MapiCannotSetReadFlags");
			ServerStrings.stringIDs.Add(2710414285U, "PublicFolderQueryStatusSyncFolderHierarchyRpcFailed");
			ServerStrings.stringIDs.Add(3367423950U, "ExInvalidSearchFolderScope");
			ServerStrings.stringIDs.Add(298783488U, "ActivitySessionIsNull");
			ServerStrings.stringIDs.Add(3325987673U, "SpellCheckerItalian");
			ServerStrings.stringIDs.Add(3248071036U, "FolderRuleStageOnPublicFolderAfter");
			ServerStrings.stringIDs.Add(1964131369U, "NotAllowedAnonymousSharingByPolicy");
			ServerStrings.stringIDs.Add(1214453788U, "MapiCannotGetCollapseState");
			ServerStrings.stringIDs.Add(3651846103U, "ZeroMinutes");
			ServerStrings.stringIDs.Add(3749282747U, "RecipientNotSupportedByAnyProviderException");
			ServerStrings.stringIDs.Add(1970390563U, "MigrationReportFinalizationSuccess");
			ServerStrings.stringIDs.Add(3878608135U, "CalNotifTypeChangedUpdate");
			ServerStrings.stringIDs.Add(3697440372U, "ExSizeFilterPropertyNotSupported");
			ServerStrings.stringIDs.Add(3733109861U, "ExConversationActionInvalidFolderType");
			ServerStrings.stringIDs.Add(2472743173U, "ClientCulture_0x2009");
			ServerStrings.stringIDs.Add(2230033988U, "UserDiscoveryMailboxNotFound");
			ServerStrings.stringIDs.Add(314838903U, "ExCorruptedRecurringCalItem");
			ServerStrings.stringIDs.Add(780978771U, "ExStartDateLaterThanEndDate");
			ServerStrings.stringIDs.Add(2635186097U, "ExInvalidOrganizer");
			ServerStrings.stringIDs.Add(3162466950U, "MaxExclusionReached");
			ServerStrings.stringIDs.Add(291587104U, "JunkEmailBlockedListXsoEmptyException");
			ServerStrings.stringIDs.Add(4224027968U, "SearchStateSucceeded");
			ServerStrings.stringIDs.Add(4078034164U, "ExInvalidJournalReportFormat");
			ServerStrings.stringIDs.Add(1049215562U, "RequestStateCertExpiring");
			ServerStrings.stringIDs.Add(3206936715U, "ConversionBodyConversionFailed");
			ServerStrings.stringIDs.Add(167159909U, "MigrationUserAdminTypeTenantAdmin");
			ServerStrings.stringIDs.Add(2721879652U, "ClientCulture_0x40B");
			ServerStrings.stringIDs.Add(871369479U, "ConversionEmptyAddress");
			ServerStrings.stringIDs.Add(1699673622U, "ClientCulture_0x380A");
			ServerStrings.stringIDs.Add(2430821738U, "PublicFoldersCannotBeAccessedDuringCompletion");
			ServerStrings.stringIDs.Add(845797650U, "MigrationReportUnknown");
			ServerStrings.stringIDs.Add(2220118463U, "FolderRuleResolvingAddressBookEntryId");
			ServerStrings.stringIDs.Add(2430200359U, "SharePointLifecyclePolicy");
			ServerStrings.stringIDs.Add(1296109892U, "MapiCannotAddNotification");
			ServerStrings.stringIDs.Add(2828973533U, "ClientCulture_0x200A");
			ServerStrings.stringIDs.Add(1595290732U, "ExFailedToCreateEventManager");
			ServerStrings.stringIDs.Add(2693957296U, "MaixmumNumberOfMailboxAssociationsForUserReached");
			ServerStrings.stringIDs.Add(4067432976U, "ExInvalidEIT");
			ServerStrings.stringIDs.Add(3176034413U, "ExFilterAndSortNotSupportedInSimpleVirtualPropertyDefinition");
			ServerStrings.stringIDs.Add(2864562615U, "DateRangeOneMonth");
			ServerStrings.stringIDs.Add(1299122915U, "MapiInvalidParam");
			ServerStrings.stringIDs.Add(94201836U, "MapiCannotDeleteUserPhoto");
			ServerStrings.stringIDs.Add(3755817478U, "MigrationUserAdminTypePartner");
			ServerStrings.stringIDs.Add(1504384645U, "ExOrganizerCannotCallUpdateCalendarItem");
			ServerStrings.stringIDs.Add(1680240084U, "DuplicateCondition");
			ServerStrings.stringIDs.Add(1817477041U, "JunkEmailTrustedListXsoTooManyException");
			ServerStrings.stringIDs.Add(1076453993U, "VersionNotInteger");
			ServerStrings.stringIDs.Add(4287963596U, "ClientCulture_0x41A");
			ServerStrings.stringIDs.Add(3423699388U, "MapiCannotGetNamedProperties");
			ServerStrings.stringIDs.Add(543137909U, "ExFailedToDeleteDefaultFolder");
			ServerStrings.stringIDs.Add(1775042376U, "ExDefaultFoldersNotInitialized");
			ServerStrings.stringIDs.Add(2045069482U, "AsyncOperationTypeCertExpiry");
			ServerStrings.stringIDs.Add(3561051015U, "ClosingTagExpectedNoneFound");
			ServerStrings.stringIDs.Add(4098965384U, "FolderRuleStageEvaluation");
			ServerStrings.stringIDs.Add(4287963347U, "ClientCulture_0x814");
			ServerStrings.stringIDs.Add(4024171282U, "TeamMailboxMessageMemberInvitationBodyIntroText");
			ServerStrings.stringIDs.Add(2721875968U, "ClientCulture_0xC09");
			ServerStrings.stringIDs.Add(3683655246U, "UnexpectedToken");
			ServerStrings.stringIDs.Add(3146200960U, "MapiCannotSeekRowBookmark");
			ServerStrings.stringIDs.Add(2294313398U, "PublicFolderSyncFolderRpcFailed");
			ServerStrings.stringIDs.Add(1106682409U, "MigrationFlagsNone");
			ServerStrings.stringIDs.Add(3045858265U, "JunkEmailTrustedListXsoNullException");
			ServerStrings.stringIDs.Add(2855609935U, "MapiCannotQueryRows");
			ServerStrings.stringIDs.Add(147262116U, "MigrationTestMSASuccess");
			ServerStrings.stringIDs.Add(732586480U, "MigrationMailboxPermissionAdmin");
			ServerStrings.stringIDs.Add(1719335392U, "ExInvalidItemId");
			ServerStrings.stringIDs.Add(3009511836U, "MapiCannotSetSpooler");
			ServerStrings.stringIDs.Add(1932453382U, "InvalidSharingTargetRecipientException");
			ServerStrings.stringIDs.Add(1816014188U, "RequestStateInProgress");
			ServerStrings.stringIDs.Add(4127188442U, "UnlockOOFHistory");
			ServerStrings.stringIDs.Add(559598517U, "ExMclCannotBeResolved");
			ServerStrings.stringIDs.Add(3224364440U, "FailedToReadConfiguration");
			ServerStrings.stringIDs.Add(749132645U, "ADUserNoMailbox");
			ServerStrings.stringIDs.Add(1136597198U, "ExReadExchangeTopologyFailed");
			ServerStrings.stringIDs.Add(152248145U, "MigrationUserStatusCorrupted");
			ServerStrings.stringIDs.Add(4055176570U, "ExMatchShouldHaveBeenCalled");
			ServerStrings.stringIDs.Add(3282126921U, "ExCannotModifyRemovedRecipient");
			ServerStrings.stringIDs.Add(3306331751U, "InvalidDateTimeFormat");
			ServerStrings.stringIDs.Add(3270231634U, "OleConversionPrepareFailed");
			ServerStrings.stringIDs.Add(2098990361U, "NotificationEmailBodyExportPSTFailed");
			ServerStrings.stringIDs.Add(1342934277U, "ExRangeNotSet");
			ServerStrings.stringIDs.Add(2096844093U, "OrganizationNotFederatedException");
			ServerStrings.stringIDs.Add(1559080129U, "ClientCulture_0x421");
			ServerStrings.stringIDs.Add(1453305798U, "ACLTooBig");
			ServerStrings.stringIDs.Add(1949750262U, "TeamMailboxMessageNotConnectedToSite");
			ServerStrings.stringIDs.Add(77059561U, "TeamMailboxSyncStatusFailed");
			ServerStrings.stringIDs.Add(2721879523U, "ClientCulture_0x80A");
			ServerStrings.stringIDs.Add(713774522U, "FailedToBindToUseLicense");
			ServerStrings.stringIDs.Add(3349952875U, "MapiCannotGetParentId");
			ServerStrings.stringIDs.Add(3964486357U, "ExEndDateEarlierThanStartDate");
			ServerStrings.stringIDs.Add(2183577153U, "ExInvalidCustomSerializationData");
			ServerStrings.stringIDs.Add(1559063653U, "ConversionLimitsExceeded");
			ServerStrings.stringIDs.Add(1403049999U, "LargeMultivaluedPropertiesNotSupportedInTNEF");
			ServerStrings.stringIDs.Add(3892201190U, "MigrationSkippableStepSettingTargetAddress");
			ServerStrings.stringIDs.Add(3041066937U, "InvalidPropertyKey");
			ServerStrings.stringIDs.Add(1512671255U, "TwoDays");
			ServerStrings.stringIDs.Add(1027306046U, "ExOutlookSearchFolderDoesNotHaveMailboxSession");
			ServerStrings.stringIDs.Add(1559080130U, "ClientCulture_0x426");
			ServerStrings.stringIDs.Add(1572145744U, "MalformedAdrEntry");
			ServerStrings.stringIDs.Add(1033019572U, "FailedToFindIssuanceLicenseAndURI");
			ServerStrings.stringIDs.Add(4010739301U, "InboxRuleMessageTypeEncrypted");
			ServerStrings.stringIDs.Add(927529551U, "SuffixMatchNotSupported");
			ServerStrings.stringIDs.Add(1799732388U, "idClientSessionInfoParseException");
			ServerStrings.stringIDs.Add(2472743336U, "ClientCulture_0x1009");
			ServerStrings.stringIDs.Add(2107406877U, "OperationResultPartiallySucceeded");
			ServerStrings.stringIDs.Add(628765410U, "UceContentFilterLoadFailure");
			ServerStrings.stringIDs.Add(1228234464U, "MapiRulesError");
			ServerStrings.stringIDs.Add(2423017895U, "MigrationBatchStatusRemoving");
			ServerStrings.stringIDs.Add(2503716610U, "ExBadFolderEntryIdSize");
			ServerStrings.stringIDs.Add(3097513127U, "NotSupportedSharingMessageException");
			ServerStrings.stringIDs.Add(885639843U, "IncompleteExchangePrincipal");
			ServerStrings.stringIDs.Add(2444446273U, "MigrationFeatureMultiBatch");
			ServerStrings.stringIDs.Add(2889367749U, "MapiCannotSetTableColumns");
			ServerStrings.stringIDs.Add(3398202471U, "FailedToReadActivityLog");
			ServerStrings.stringIDs.Add(2045304318U, "SearchOperationFailed");
			ServerStrings.stringIDs.Add(3997956246U, "ExDefaultJournalFilename");
			ServerStrings.stringIDs.Add(1407904664U, "MapiCannotExpandRow");
			ServerStrings.stringIDs.Add(3375709410U, "ExCannotStartDeadSessionChecking");
			ServerStrings.stringIDs.Add(2953075549U, "SearchStateNotStarted");
			ServerStrings.stringIDs.Add(2989293604U, "SpellCheckerSwedish");
			ServerStrings.stringIDs.Add(3084838222U, "NotificationEmailSubjectExportPst");
			ServerStrings.stringIDs.Add(2174906965U, "ContentRestrictionOnSearchKey");
			ServerStrings.stringIDs.Add(671447089U, "ExUnknownFilterType");
			ServerStrings.stringIDs.Add(2828973467U, "ClientCulture_0x400A");
			ServerStrings.stringIDs.Add(3647529948U, "UnableToLoadDrmMessage");
			ServerStrings.stringIDs.Add(4048687496U, "ExCannotParseValue");
			ServerStrings.stringIDs.Add(2116512747U, "ClientCulture_0x4001");
			ServerStrings.stringIDs.Add(1918332060U, "TeamMailboxMessageToLearnMore");
			ServerStrings.stringIDs.Add(1272465216U, "MigrationBatchStatusCompleting");
			ServerStrings.stringIDs.Add(2453169079U, "MapiCannotGetRowCount");
			ServerStrings.stringIDs.Add(3849632232U, "FolderRuleStageOnDeliveredMessage");
			ServerStrings.stringIDs.Add(2614560510U, "MigrationBatchSupportedActionAppend");
			ServerStrings.stringIDs.Add(1858795727U, "ExCannotOpenRejectedItem");
			ServerStrings.stringIDs.Add(82143339U, "MigrationBatchStatusFailed");
			ServerStrings.stringIDs.Add(597150166U, "VotingDataCorrupt");
			ServerStrings.stringIDs.Add(3941673604U, "MigrationTestMSAFailed");
			ServerStrings.stringIDs.Add(656484203U, "TeamMailboxSyncStatusDocumentAndMembershipSyncFailure");
			ServerStrings.stringIDs.Add(460612780U, "PublicFoldersCannotBeMovedDuringMigration");
			ServerStrings.stringIDs.Add(480886895U, "ConversionNoReplayContent");
			ServerStrings.stringIDs.Add(2721879546U, "ClientCulture_0x404");
			ServerStrings.stringIDs.Add(495633109U, "ExCantSubmitWithoutRecipients");
			ServerStrings.stringIDs.Add(1614900577U, "LastErrorMessage");
			ServerStrings.stringIDs.Add(2849660304U, "JunkEmailFolderNotFoundException");
			ServerStrings.stringIDs.Add(1559080241U, "ClientCulture_0x42A");
			ServerStrings.stringIDs.Add(3133024367U, "AmMoveNotApplicableForDbException");
			ServerStrings.stringIDs.Add(3710501592U, "ExCantUndeleteOccurrence");
			ServerStrings.stringIDs.Add(2418303566U, "MigrationUserStatusQueued");
			ServerStrings.stringIDs.Add(4210093826U, "DateRangeSixMonths");
			ServerStrings.stringIDs.Add(1365634215U, "MigrationSkippableStepNone");
			ServerStrings.stringIDs.Add(3746574982U, "MalformedCommentRestriction");
			ServerStrings.stringIDs.Add(1559080131U, "ClientCulture_0x427");
			ServerStrings.stringIDs.Add(2702427225U, "MigrationUserStatusSummaryActive");
			ServerStrings.stringIDs.Add(2343856628U, "ErrorEmptyFolderNotSupported");
			ServerStrings.stringIDs.Add(2264323560U, "ClientCulture_0x540A");
			ServerStrings.stringIDs.Add(1489962226U, "ClientCulture_0x2C0A");
			ServerStrings.stringIDs.Add(3302221775U, "MissingOperand");
			ServerStrings.stringIDs.Add(1949879021U, "DuplicateAction");
			ServerStrings.stringIDs.Add(2882103486U, "SearchStateQueued");
			ServerStrings.stringIDs.Add(3607950093U, "ExQueryPropertyBagRowNotSet");
			ServerStrings.stringIDs.Add(1073167130U, "Sunday");
			ServerStrings.stringIDs.Add(3059751105U, "WeatherUnitDefault");
			ServerStrings.stringIDs.Add(3120595407U, "RequestSecurityTokenException");
			ServerStrings.stringIDs.Add(1768187831U, "ExNoOccurrencesInRecurrence");
			ServerStrings.stringIDs.Add(276036243U, "ExInvalidMclXml");
			ServerStrings.stringIDs.Add(4168806856U, "ExInvalidIdFormat");
			ServerStrings.stringIDs.Add(2161223297U, "ExAdminAuditLogsFolderAccessDenied");
			ServerStrings.stringIDs.Add(2721875973U, "ClientCulture_0xC04");
			ServerStrings.stringIDs.Add(4170247214U, "ExInvalidComparisionOperatorInPropertyComparisionFilter");
			ServerStrings.stringIDs.Add(1937417240U, "AsyncOperationTypeExportPST");
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00059790 File Offset: 0x00057990
		public static LocalizedString InvalidReceiveMeetingMessageCopiesException(string delegateUser)
		{
			return new LocalizedString("InvalidReceiveMeetingMessageCopiesException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				delegateUser
			});
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x000597BF File Offset: 0x000579BF
		public static LocalizedString MissingRightsManagementLicense
		{
			get
			{
				return new LocalizedString("MissingRightsManagementLicense", "ExD831F7", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000597DD File Offset: 0x000579DD
		public static LocalizedString ServerLocatorServiceTransientFault
		{
			get
			{
				return new LocalizedString("ServerLocatorServiceTransientFault", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x000597FC File Offset: 0x000579FC
		public static LocalizedString AmDatabaseADException(string dbName, string error)
		{
			return new LocalizedString("AmDatabaseADException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				dbName,
				error
			});
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0005982F File Offset: 0x00057A2F
		public static LocalizedString EmailAddressMissing
		{
			get
			{
				return new LocalizedString("EmailAddressMissing", "ExE5B1C8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0005984D File Offset: 0x00057A4D
		public static LocalizedString CannotShareSearchFolder
		{
			get
			{
				return new LocalizedString("CannotShareSearchFolder", "Ex5B8087", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0005986B File Offset: 0x00057A6B
		public static LocalizedString EstimateStateStopping
		{
			get
			{
				return new LocalizedString("EstimateStateStopping", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00059889 File Offset: 0x00057A89
		public static LocalizedString SpellCheckerDutch
		{
			get
			{
				return new LocalizedString("SpellCheckerDutch", "Ex75A03E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x000598A7 File Offset: 0x00057AA7
		public static LocalizedString SpellCheckerNorwegianNynorsk
		{
			get
			{
				return new LocalizedString("SpellCheckerNorwegianNynorsk", "ExF0B996", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000598C8 File Offset: 0x00057AC8
		public static LocalizedString AddressAndOriginMismatch(object origin)
		{
			return new LocalizedString("AddressAndOriginMismatch", "ExA7983D", false, true, ServerStrings.ResourceManager, new object[]
			{
				origin
			});
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000598F8 File Offset: 0x00057AF8
		public static LocalizedString RepairingIsNotApplicableForCurrentMonitorState(string monitorName, string targetResource, string alertState)
		{
			return new LocalizedString("RepairingIsNotApplicableForCurrentMonitorState", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				monitorName,
				targetResource,
				alertState
			});
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00059930 File Offset: 0x00057B30
		public static LocalizedString MigrationInvalidTargetAddress(string email)
		{
			return new LocalizedString("MigrationInvalidTargetAddress", "ExE23BF5", false, true, ServerStrings.ResourceManager, new object[]
			{
				email
			});
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0005995F File Offset: 0x00057B5F
		public static LocalizedString MigrationFlagsStart
		{
			get
			{
				return new LocalizedString("MigrationFlagsStart", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00059980 File Offset: 0x00057B80
		public static LocalizedString JunkEmailTrustedListXsoFormatException(string value)
		{
			return new LocalizedString("JunkEmailTrustedListXsoFormatException", "Ex1B39F7", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x000599AF File Offset: 0x00057BAF
		public static LocalizedString TeamMailboxMessageSiteAndSiteMailboxDetails
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageSiteAndSiteMailboxDetails", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x000599CD File Offset: 0x00057BCD
		public static LocalizedString CannotGetSupportedRoutingTypes
		{
			get
			{
				return new LocalizedString("CannotGetSupportedRoutingTypes", "Ex7EB29B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x000599EC File Offset: 0x00057BEC
		public static LocalizedString ExSyncStateAlreadyExists(string syncStateName)
		{
			return new LocalizedString("ExSyncStateAlreadyExists", "Ex835B84", false, true, ServerStrings.ResourceManager, new object[]
			{
				syncStateName
			});
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00059A1C File Offset: 0x00057C1C
		public static LocalizedString FailedToAcquireFederationRac(string tenantId, Uri uri)
		{
			return new LocalizedString("FailedToAcquireFederationRac", "Ex8251B9", false, true, ServerStrings.ResourceManager, new object[]
			{
				tenantId,
				uri
			});
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00059A50 File Offset: 0x00057C50
		public static LocalizedString SaveFailureAfterPromotion(string uid)
		{
			return new LocalizedString("SaveFailureAfterPromotion", "Ex9EBF5C", false, true, ServerStrings.ResourceManager, new object[]
			{
				uid
			});
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00059A7F File Offset: 0x00057C7F
		public static LocalizedString ClientCulture_0x816
		{
			get
			{
				return new LocalizedString("ClientCulture_0x816", "ExDC0F9D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00059AA0 File Offset: 0x00057CA0
		public static LocalizedString ErrorCannotSyncHoldObjectManagedByOtherOrg(string name, string currentOrg, string objOrg)
		{
			return new LocalizedString("ErrorCannotSyncHoldObjectManagedByOtherOrg", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				name,
				currentOrg,
				objOrg
			});
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00059AD8 File Offset: 0x00057CD8
		public static LocalizedString TaskOperationFailedWithEcException(int ec)
		{
			return new LocalizedString("TaskOperationFailedWithEcException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				ec
			});
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00059B0C File Offset: 0x00057D0C
		public static LocalizedString AsyncOperationTypeMailboxRestore
		{
			get
			{
				return new LocalizedString("AsyncOperationTypeMailboxRestore", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00059B2A File Offset: 0x00057D2A
		public static LocalizedString MatchContainerClassValidationFailed
		{
			get
			{
				return new LocalizedString("MatchContainerClassValidationFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00059B48 File Offset: 0x00057D48
		public static LocalizedString ExCannotCreateSubfolderUnderSearchFolder
		{
			get
			{
				return new LocalizedString("ExCannotCreateSubfolderUnderSearchFolder", "Ex27B20C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00059B66 File Offset: 0x00057D66
		public static LocalizedString InboxRuleImportanceHigh
		{
			get
			{
				return new LocalizedString("InboxRuleImportanceHigh", "Ex2DEA34", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00059B84 File Offset: 0x00057D84
		public static LocalizedString MapiCopyFailedProperties
		{
			get
			{
				return new LocalizedString("MapiCopyFailedProperties", "ExC43ADE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x00059BA2 File Offset: 0x00057DA2
		public static LocalizedString ClientCulture_0x3C0A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x3C0A", "Ex81C073", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00059BC0 File Offset: 0x00057DC0
		public static LocalizedString ErrorTeamMailboxGetUsersNullResponse
		{
			get
			{
				return new LocalizedString("ErrorTeamMailboxGetUsersNullResponse", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x00059BDE File Offset: 0x00057DDE
		public static LocalizedString MigrationBatchSupportedActionNone
		{
			get
			{
				return new LocalizedString("MigrationBatchSupportedActionNone", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00059BFC File Offset: 0x00057DFC
		public static LocalizedString UserPhotoFileTooLarge(int limit)
		{
			return new LocalizedString("UserPhotoFileTooLarge", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				limit
			});
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00059C30 File Offset: 0x00057E30
		public static LocalizedString ExternalIdentityInconsistentSid(string mailbox, string entryId, string currentSid, string stringNewSid)
		{
			return new LocalizedString("ExternalIdentityInconsistentSid", "Ex1995BD", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailbox,
				entryId,
				currentSid,
				stringNewSid
			});
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00059C6B File Offset: 0x00057E6B
		public static LocalizedString ExAuditsUpdateDenied
		{
			get
			{
				return new LocalizedString("ExAuditsUpdateDenied", "ExC1463B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00059C8C File Offset: 0x00057E8C
		public static LocalizedString DataMoveReplicationConstraintFlushNotSatisfied(DataMoveReplicationConstraintParameter constraint, Guid databaseGuid, object utcCommitTime, object replicationTime)
		{
			return new LocalizedString("DataMoveReplicationConstraintFlushNotSatisfied", "Ex09A030", false, true, ServerStrings.ResourceManager, new object[]
			{
				constraint,
				databaseGuid,
				utcCommitTime,
				replicationTime
			});
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00059CD1 File Offset: 0x00057ED1
		public static LocalizedString ExBadMessageEntryIdSize
		{
			get
			{
				return new LocalizedString("ExBadMessageEntryIdSize", "Ex54225F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00059CF0 File Offset: 0x00057EF0
		public static LocalizedString PublicFoldersNotEnabledForTenant(string org)
		{
			return new LocalizedString("PublicFoldersNotEnabledForTenant", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				org
			});
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00059D1F File Offset: 0x00057F1F
		public static LocalizedString ExAdminAuditLogsUpdateDenied
		{
			get
			{
				return new LocalizedString("ExAdminAuditLogsUpdateDenied", "Ex7B50FB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00059D3D File Offset: 0x00057F3D
		public static LocalizedString ExInvalidNumberOfOccurrences
		{
			get
			{
				return new LocalizedString("ExInvalidNumberOfOccurrences", "Ex99D03F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00059D5C File Offset: 0x00057F5C
		public static LocalizedString MigrationJobConnectionSettingsIncomplete(string fieldName)
		{
			return new LocalizedString("MigrationJobConnectionSettingsIncomplete", "Ex4889F7", false, true, ServerStrings.ResourceManager, new object[]
			{
				fieldName
			});
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00059D8C File Offset: 0x00057F8C
		public static LocalizedString JunkEmailBlockedListOwnersEmailAddressException(string value)
		{
			return new LocalizedString("JunkEmailBlockedListOwnersEmailAddressException", "Ex93027C", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00059DBC File Offset: 0x00057FBC
		public static LocalizedString ExServerNotInSite(string serverName)
		{
			return new LocalizedString("ExServerNotInSite", "ExC143F9", false, true, ServerStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x00059DEB File Offset: 0x00057FEB
		public static LocalizedString OleConversionFailed
		{
			get
			{
				return new LocalizedString("OleConversionFailed", "Ex438E33", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00059E09 File Offset: 0x00058009
		public static LocalizedString ClientCulture_0x3401
		{
			get
			{
				return new LocalizedString("ClientCulture_0x3401", "Ex547263", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x00059E27 File Offset: 0x00058027
		public static LocalizedString ExCannotReadFolderData
		{
			get
			{
				return new LocalizedString("ExCannotReadFolderData", "ExB2C80E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00059E45 File Offset: 0x00058045
		public static LocalizedString InboxRuleSensitivityNormal
		{
			get
			{
				return new LocalizedString("InboxRuleSensitivityNormal", "ExC3B29E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00059E63 File Offset: 0x00058063
		public static LocalizedString SpellCheckerCatalan
		{
			get
			{
				return new LocalizedString("SpellCheckerCatalan", "ExC9BC78", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x00059E81 File Offset: 0x00058081
		public static LocalizedString TeamMailboxMessageHowToGetStarted
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageHowToGetStarted", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00059E9F File Offset: 0x0005809F
		public static LocalizedString ExInvalidMasterValueAndColumnLength
		{
			get
			{
				return new LocalizedString("ExInvalidMasterValueAndColumnLength", "Ex0EA1D6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00059EC0 File Offset: 0x000580C0
		public static LocalizedString InvalidSharingMessageException(string property)
		{
			return new LocalizedString("InvalidSharingMessageException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00059EEF File Offset: 0x000580EF
		public static LocalizedString MigrationBatchStatusStopping
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusStopping", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00059F0D File Offset: 0x0005810D
		public static LocalizedString ClientCulture_0x440A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x440A", "Ex9C0E65", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00059F2B File Offset: 0x0005812B
		public static LocalizedString ExFolderAlreadyExistsInClientState
		{
			get
			{
				return new LocalizedString("ExFolderAlreadyExistsInClientState", "Ex2386C6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00059F49 File Offset: 0x00058149
		public static LocalizedString InvalidPermissionsEntry
		{
			get
			{
				return new LocalizedString("InvalidPermissionsEntry", "ExF7C3B8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00059F67 File Offset: 0x00058167
		public static LocalizedString ConversionInternalFailure
		{
			get
			{
				return new LocalizedString("ConversionInternalFailure", "ExD5AF06", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00059F88 File Offset: 0x00058188
		public static LocalizedString CannotObtainSynchronizationUploadState(Type mapiCollectorType)
		{
			return new LocalizedString("CannotObtainSynchronizationUploadState", "ExD6FAB7", false, true, ServerStrings.ResourceManager, new object[]
			{
				mapiCollectorType
			});
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00059FB8 File Offset: 0x000581B8
		public static LocalizedString ExFolderDeletePropsFailed(string exceptionMessage)
		{
			return new LocalizedString("ExFolderDeletePropsFailed", "Ex7AB4F1", false, true, ServerStrings.ResourceManager, new object[]
			{
				exceptionMessage
			});
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x00059FE7 File Offset: 0x000581E7
		public static LocalizedString MigrationTypeExchangeOutlookAnywhere
		{
			get
			{
				return new LocalizedString("MigrationTypeExchangeOutlookAnywhere", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0005A008 File Offset: 0x00058208
		public static LocalizedString ExConfigTypeNotMatched(string definedType, string usedType)
		{
			return new LocalizedString("ExConfigTypeNotMatched", "Ex7FF538", false, true, ServerStrings.ResourceManager, new object[]
			{
				definedType,
				usedType
			});
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0005A03C File Offset: 0x0005823C
		public static LocalizedString UserCannotBeFoundFromContext(int errorCode)
		{
			return new LocalizedString("UserCannotBeFoundFromContext", "Ex485AC1", false, true, ServerStrings.ResourceManager, new object[]
			{
				errorCode
			});
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0005A070 File Offset: 0x00058270
		public static LocalizedString ClientCulture_0x4809
		{
			get
			{
				return new LocalizedString("ClientCulture_0x4809", "Ex7E27E4", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0005A08E File Offset: 0x0005828E
		public static LocalizedString MigrationAutodiscoverConfigurationFailure
		{
			get
			{
				return new LocalizedString("MigrationAutodiscoverConfigurationFailure", "ExA6C5F6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0005A0AC File Offset: 0x000582AC
		public static LocalizedString ExDefaultContactFilename
		{
			get
			{
				return new LocalizedString("ExDefaultContactFilename", "Ex6280F9", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0005A0CC File Offset: 0x000582CC
		public static LocalizedString JunkEmailBlockedListXsoGenericException(string value)
		{
			return new LocalizedString("JunkEmailBlockedListXsoGenericException", "Ex56BA32", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0005A0FB File Offset: 0x000582FB
		public static LocalizedString TeamMailboxMessageReopenClosedSiteMailbox
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageReopenClosedSiteMailbox", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0005A11C File Offset: 0x0005831C
		public static LocalizedString MapiCannotCreateFolder(string name)
		{
			return new LocalizedString("MapiCannotCreateFolder", "ExD0BCE9", false, true, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0005A14B File Offset: 0x0005834B
		public static LocalizedString MigrationObjectsCountStringPFs
		{
			get
			{
				return new LocalizedString("MigrationObjectsCountStringPFs", "Ex190242", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0005A169 File Offset: 0x00058369
		public static LocalizedString ExCannotCreateRecurringMeetingWithoutTimeZone
		{
			get
			{
				return new LocalizedString("ExCannotCreateRecurringMeetingWithoutTimeZone", "Ex890DB1", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0005A187 File Offset: 0x00058387
		public static LocalizedString ExInvalidSaveOnCorrelatedItem
		{
			get
			{
				return new LocalizedString("ExInvalidSaveOnCorrelatedItem", "Ex0B95C5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0005A1A8 File Offset: 0x000583A8
		public static LocalizedString ExCantDeleteOpenedOccurrence(object occId)
		{
			return new LocalizedString("ExCantDeleteOpenedOccurrence", "ExFB5989", false, true, ServerStrings.ResourceManager, new object[]
			{
				occId
			});
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0005A1D7 File Offset: 0x000583D7
		public static LocalizedString ErrorTeamMailboxGetListItemChangesNullResponse
		{
			get
			{
				return new LocalizedString("ErrorTeamMailboxGetListItemChangesNullResponse", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0005A1F5 File Offset: 0x000583F5
		public static LocalizedString ExCorruptedTimeZone
		{
			get
			{
				return new LocalizedString("ExCorruptedTimeZone", "ExB67D00", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0005A214 File Offset: 0x00058414
		public static LocalizedString BindToWrongObjectType(string objectClass, string intendedType)
		{
			return new LocalizedString("BindToWrongObjectType", "Ex5CD494", false, true, ServerStrings.ResourceManager, new object[]
			{
				objectClass,
				intendedType
			});
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0005A247 File Offset: 0x00058447
		public static LocalizedString MigrationUserStatusSummaryStopped
		{
			get
			{
				return new LocalizedString("MigrationUserStatusSummaryStopped", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0005A268 File Offset: 0x00058468
		public static LocalizedString idMailboxInfoStaleException(string mailboxId)
		{
			return new LocalizedString("idMailboxInfoStaleException", "Ex6160E3", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailboxId
			});
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0005A298 File Offset: 0x00058498
		public static LocalizedString ExCannotSaveInvalidObject(object firstError)
		{
			return new LocalizedString("ExCannotSaveInvalidObject", "Ex31080A", false, true, ServerStrings.ResourceManager, new object[]
			{
				firstError
			});
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0005A2C7 File Offset: 0x000584C7
		public static LocalizedString InboxRuleSensitivityCompanyConfidential
		{
			get
			{
				return new LocalizedString("InboxRuleSensitivityCompanyConfidential", "Ex24A804", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0005A2E8 File Offset: 0x000584E8
		public static LocalizedString AmServerTransientException(string errorMessage)
		{
			return new LocalizedString("AmServerTransientException", "Ex0BB11D", false, true, ServerStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0005A317 File Offset: 0x00058517
		public static LocalizedString FailedToAddAttachments
		{
			get
			{
				return new LocalizedString("FailedToAddAttachments", "Ex709496", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0005A335 File Offset: 0x00058535
		public static LocalizedString MapiCannotDeliverItem
		{
			get
			{
				return new LocalizedString("MapiCannotDeliverItem", "ExEE6234", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0005A354 File Offset: 0x00058554
		public static LocalizedString ExCalendarTypeNotSupported(object calendarType)
		{
			return new LocalizedString("ExCalendarTypeNotSupported", "ExB93B8B", false, true, ServerStrings.ResourceManager, new object[]
			{
				calendarType
			});
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0005A383 File Offset: 0x00058583
		public static LocalizedString MapiCannotGetLocalRepIds
		{
			get
			{
				return new LocalizedString("MapiCannotGetLocalRepIds", "Ex623AC5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0005A3A1 File Offset: 0x000585A1
		public static LocalizedString ClientCulture_0x3C01
		{
			get
			{
				return new LocalizedString("ClientCulture_0x3C01", "ExA55642", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0005A3C0 File Offset: 0x000585C0
		public static LocalizedString FailedToAcquireUseLicense(Uri url)
		{
			return new LocalizedString("FailedToAcquireUseLicense", "Ex2DF607", false, true, ServerStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0005A3EF File Offset: 0x000585EF
		public static LocalizedString FirstDay
		{
			get
			{
				return new LocalizedString("FirstDay", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0005A40D File Offset: 0x0005860D
		public static LocalizedString ClientCulture_0x41D
		{
			get
			{
				return new LocalizedString("ClientCulture_0x41D", "Ex3E7039", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0005A42B File Offset: 0x0005862B
		public static LocalizedString PersonIsAlreadyLinkedWithGALContact
		{
			get
			{
				return new LocalizedString("PersonIsAlreadyLinkedWithGALContact", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0005A44C File Offset: 0x0005864C
		public static LocalizedString AmReplayServiceDownException(string serverName, string rpcErrorMessage)
		{
			return new LocalizedString("AmReplayServiceDownException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				serverName,
				rpcErrorMessage
			});
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0005A47F File Offset: 0x0005867F
		public static LocalizedString InboxRuleMessageTypeCalendaring
		{
			get
			{
				return new LocalizedString("InboxRuleMessageTypeCalendaring", "Ex2E56BF", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0005A49D File Offset: 0x0005869D
		public static LocalizedString Editor
		{
			get
			{
				return new LocalizedString("Editor", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0005A4BB File Offset: 0x000586BB
		public static LocalizedString CannotShareOtherPersonalFolder
		{
			get
			{
				return new LocalizedString("CannotShareOtherPersonalFolder", "ExB54D40", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0005A4D9 File Offset: 0x000586D9
		public static LocalizedString TeamMailboxMessageClosedBodyIntroText
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageClosedBodyIntroText", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0005A4F7 File Offset: 0x000586F7
		public static LocalizedString InboxRuleMessageTypeSigned
		{
			get
			{
				return new LocalizedString("InboxRuleMessageTypeSigned", "ExDFBD7D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0005A518 File Offset: 0x00058718
		public static LocalizedString ExDefaultFolderNotFound(object folder)
		{
			return new LocalizedString("ExDefaultFolderNotFound", "Ex0E920E", false, true, ServerStrings.ResourceManager, new object[]
			{
				folder
			});
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0005A547 File Offset: 0x00058747
		public static LocalizedString MigrationTypePSTImport
		{
			get
			{
				return new LocalizedString("MigrationTypePSTImport", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0005A565 File Offset: 0x00058765
		public static LocalizedString DateRangeOneYear
		{
			get
			{
				return new LocalizedString("DateRangeOneYear", "ExE6B1F6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0005A583 File Offset: 0x00058783
		public static LocalizedString ExAuditsFolderAccessDenied
		{
			get
			{
				return new LocalizedString("ExAuditsFolderAccessDenied", "Ex36C1CC", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0005A5A1 File Offset: 0x000587A1
		public static LocalizedString ClientCulture_0x1409
		{
			get
			{
				return new LocalizedString("ClientCulture_0x1409", "ExB851E5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0005A5BF File Offset: 0x000587BF
		public static LocalizedString NoExternalOwaAvailableException
		{
			get
			{
				return new LocalizedString("NoExternalOwaAvailableException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0005A5DD File Offset: 0x000587DD
		public static LocalizedString DelegateNotSupportedFolder
		{
			get
			{
				return new LocalizedString("DelegateNotSupportedFolder", "Ex9EEEAD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0005A5FC File Offset: 0x000587FC
		public static LocalizedString JunkEmailBlockedListInternalToOrganizationException(string value)
		{
			return new LocalizedString("JunkEmailBlockedListInternalToOrganizationException", "ExBB3B55", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0005A62C File Offset: 0x0005882C
		public static LocalizedString FailedToAcquireUseLicenses(string orgId)
		{
			return new LocalizedString("FailedToAcquireUseLicenses", "ExB83176", false, true, ServerStrings.ResourceManager, new object[]
			{
				orgId
			});
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0005A65C File Offset: 0x0005885C
		public static LocalizedString AmDbNotMountedNoViableServersException(string dbName)
		{
			return new LocalizedString("AmDbNotMountedNoViableServersException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0005A68C File Offset: 0x0005888C
		public static LocalizedString ExNumberOfRowsToFetchInvalid(string numRows)
		{
			return new LocalizedString("ExNumberOfRowsToFetchInvalid", "Ex92A046", false, true, ServerStrings.ResourceManager, new object[]
			{
				numRows
			});
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0005A6BC File Offset: 0x000588BC
		public static LocalizedString PropertyIsReadOnly(string property)
		{
			return new LocalizedString("PropertyIsReadOnly", "Ex776C3D", false, true, ServerStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0005A6EB File Offset: 0x000588EB
		public static LocalizedString MigrationBatchDirectionLocal
		{
			get
			{
				return new LocalizedString("MigrationBatchDirectionLocal", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0005A709 File Offset: 0x00058909
		public static LocalizedString ErrorFolderDeleted
		{
			get
			{
				return new LocalizedString("ErrorFolderDeleted", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0005A728 File Offset: 0x00058928
		public static LocalizedString ActiveMonitoringServerException(string errorMessage)
		{
			return new LocalizedString("ActiveMonitoringServerException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0005A758 File Offset: 0x00058958
		public static LocalizedString AmServerDagNotFound(string serverName)
		{
			return new LocalizedString("AmServerDagNotFound", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0005A788 File Offset: 0x00058988
		public static LocalizedString MigrationObjectsCountStringMailboxes(string mailboxes)
		{
			return new LocalizedString("MigrationObjectsCountStringMailboxes", "Ex6AF053", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailboxes
			});
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0005A7B8 File Offset: 0x000589B8
		public static LocalizedString AddressAndRoutingTypeMismatch(string routingType)
		{
			return new LocalizedString("AddressAndRoutingTypeMismatch", "Ex6E092B", false, true, ServerStrings.ResourceManager, new object[]
			{
				routingType
			});
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0005A7E7 File Offset: 0x000589E7
		public static LocalizedString BadDateTimeFormatInChangeDate
		{
			get
			{
				return new LocalizedString("BadDateTimeFormatInChangeDate", "ExB87CF2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0005A808 File Offset: 0x00058A08
		public static LocalizedString RecipientAddressInvalidForExternalLicensing(string address, Uri uri, string tenantId)
		{
			return new LocalizedString("RecipientAddressInvalidForExternalLicensing", "Ex951F0D", false, true, ServerStrings.ResourceManager, new object[]
			{
				address,
				uri,
				tenantId
			});
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0005A83F File Offset: 0x00058A3F
		public static LocalizedString ClientCulture_0x1C0A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x1C0A", "Ex02ED11", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0005A85D File Offset: 0x00058A5D
		public static LocalizedString TeamMailboxMessageNoActionText
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageNoActionText", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0005A87B File Offset: 0x00058A7B
		public static LocalizedString InvalidMigrationBatchId
		{
			get
			{
				return new LocalizedString("InvalidMigrationBatchId", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0005A89C File Offset: 0x00058A9C
		public static LocalizedString MigrationBatchNotFoundError(string batchName)
		{
			return new LocalizedString("MigrationBatchNotFoundError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				batchName
			});
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0005A8CB File Offset: 0x00058ACB
		public static LocalizedString FolderRuleStageOnPromotedMessage
		{
			get
			{
				return new LocalizedString("FolderRuleStageOnPromotedMessage", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0005A8E9 File Offset: 0x00058AE9
		public static LocalizedString idUnableToCreateDefaultTaskGroupException
		{
			get
			{
				return new LocalizedString("idUnableToCreateDefaultTaskGroupException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0005A907 File Offset: 0x00058B07
		public static LocalizedString PublicFolderStartSyncFolderHierarchyRpcFailed
		{
			get
			{
				return new LocalizedString("PublicFolderStartSyncFolderHierarchyRpcFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0005A925 File Offset: 0x00058B25
		public static LocalizedString SearchStateFailed
		{
			get
			{
				return new LocalizedString("SearchStateFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0005A943 File Offset: 0x00058B43
		public static LocalizedString ClientCulture_0x2401
		{
			get
			{
				return new LocalizedString("ClientCulture_0x2401", "ExC9AB56", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0005A964 File Offset: 0x00058B64
		public static LocalizedString ExInvalidMonthNthDayMask(object dayMask)
		{
			return new LocalizedString("ExInvalidMonthNthDayMask", "ExB66492", false, true, ServerStrings.ResourceManager, new object[]
			{
				dayMask
			});
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0005A994 File Offset: 0x00058B94
		public static LocalizedString PublicFolderPerServerThreadLimitExceeded(string server, int limit, int totalActiveServers)
		{
			return new LocalizedString("PublicFolderPerServerThreadLimitExceeded", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				server,
				limit,
				totalActiveServers
			});
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0005A9D5 File Offset: 0x00058BD5
		public static LocalizedString MapiCannotWritePerUserInformation
		{
			get
			{
				return new LocalizedString("MapiCannotWritePerUserInformation", "Ex374E09", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0005A9F3 File Offset: 0x00058BF3
		public static LocalizedString SearchNameCharacterConstraint
		{
			get
			{
				return new LocalizedString("SearchNameCharacterConstraint", "Ex23EF52", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0005AA11 File Offset: 0x00058C11
		public static LocalizedString InboxRuleMessageTypeCalendaringResponse
		{
			get
			{
				return new LocalizedString("InboxRuleMessageTypeCalendaringResponse", "Ex1AC335", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0005AA30 File Offset: 0x00058C30
		public static LocalizedString DataMoveReplicationConstraintNotSatisfiedForNonReplicatedDatabase(DataMoveReplicationConstraintParameter constraint, Guid databaseGuid)
		{
			return new LocalizedString("DataMoveReplicationConstraintNotSatisfiedForNonReplicatedDatabase", "Ex6588C5", false, true, ServerStrings.ResourceManager, new object[]
			{
				constraint,
				databaseGuid
			});
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0005AA6D File Offset: 0x00058C6D
		public static LocalizedString ErrorTimeProposalInvalidWhenNotAllowedByOrganizer
		{
			get
			{
				return new LocalizedString("ErrorTimeProposalInvalidWhenNotAllowedByOrganizer", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0005AA8B File Offset: 0x00058C8B
		public static LocalizedString RequestStateCertExpired
		{
			get
			{
				return new LocalizedString("RequestStateCertExpired", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0005AAAC File Offset: 0x00058CAC
		public static LocalizedString FailedToCreateLicenseGenerator(string orgId, string type)
		{
			return new LocalizedString("FailedToCreateLicenseGenerator", "Ex105715", false, true, ServerStrings.ResourceManager, new object[]
			{
				orgId,
				type
			});
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0005AAE0 File Offset: 0x00058CE0
		public static LocalizedString OscFolderForProviderNotFound(string provider)
		{
			return new LocalizedString("OscFolderForProviderNotFound", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				provider
			});
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0005AB0F File Offset: 0x00058D0F
		public static LocalizedString Thursday
		{
			get
			{
				return new LocalizedString("Thursday", "Ex245CEF", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0005AB2D File Offset: 0x00058D2D
		public static LocalizedString MapiCannotDeleteAttachment
		{
			get
			{
				return new LocalizedString("MapiCannotDeleteAttachment", "ExD20458", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0005AB4B File Offset: 0x00058D4B
		public static LocalizedString ExEventsNotSupportedForDelegateUser
		{
			get
			{
				return new LocalizedString("ExEventsNotSupportedForDelegateUser", "Ex03D0FB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0005AB69 File Offset: 0x00058D69
		public static LocalizedString AsyncOperationTypeImportPST
		{
			get
			{
				return new LocalizedString("AsyncOperationTypeImportPST", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0005AB88 File Offset: 0x00058D88
		public static LocalizedString ImportItemThrewGrayException(string exception)
		{
			return new LocalizedString("ImportItemThrewGrayException", "ExCE7517", false, true, ServerStrings.ResourceManager, new object[]
			{
				exception
			});
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0005ABB7 File Offset: 0x00058DB7
		public static LocalizedString MigrationStepDataMigration
		{
			get
			{
				return new LocalizedString("MigrationStepDataMigration", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0005ABD5 File Offset: 0x00058DD5
		public static LocalizedString JunkEmailBlockedListXsoTooManyException
		{
			get
			{
				return new LocalizedString("JunkEmailBlockedListXsoTooManyException", "Ex99A43E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0005ABF3 File Offset: 0x00058DF3
		public static LocalizedString ClientCulture_0x409
		{
			get
			{
				return new LocalizedString("ClientCulture_0x409", "ExA7BCC8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0005AC14 File Offset: 0x00058E14
		public static LocalizedString NonCanonicalACL(string errorInformation)
		{
			return new LocalizedString("NonCanonicalACL", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				errorInformation
			});
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0005AC43 File Offset: 0x00058E43
		public static LocalizedString ErrorFolderCreationIsBlocked
		{
			get
			{
				return new LocalizedString("ErrorFolderCreationIsBlocked", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0005AC61 File Offset: 0x00058E61
		public static LocalizedString ExInvalidParticipantEntryId
		{
			get
			{
				return new LocalizedString("ExInvalidParticipantEntryId", "Ex9B1835", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0005AC7F File Offset: 0x00058E7F
		public static LocalizedString ExInvalidSpecifierValueError
		{
			get
			{
				return new LocalizedString("ExInvalidSpecifierValueError", "ExF5B629", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0005AC9D File Offset: 0x00058E9D
		public static LocalizedString TeamMailboxSyncStatusDocumentSyncFailureOnly
		{
			get
			{
				return new LocalizedString("TeamMailboxSyncStatusDocumentSyncFailureOnly", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0005ACBB File Offset: 0x00058EBB
		public static LocalizedString SearchStateInProgress
		{
			get
			{
				return new LocalizedString("SearchStateInProgress", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0005ACD9 File Offset: 0x00058ED9
		public static LocalizedString JunkEmailInvalidOperationException
		{
			get
			{
				return new LocalizedString("JunkEmailInvalidOperationException", "ExB16774", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0005ACF8 File Offset: 0x00058EF8
		public static LocalizedString UserPhotoFileTooSmall(int min)
		{
			return new LocalizedString("UserPhotoFileTooSmall", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				min
			});
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0005AD2C File Offset: 0x00058F2C
		public static LocalizedString InvalidWorkingPeriod(string parameter, int rangeStart, int rangeEnd)
		{
			return new LocalizedString("InvalidWorkingPeriod", "Ex593C43", false, true, ServerStrings.ResourceManager, new object[]
			{
				parameter,
				rangeStart,
				rangeEnd
			});
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0005AD6D File Offset: 0x00058F6D
		public static LocalizedString TeamMailboxMessageWhatIsSiteMailbox
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageWhatIsSiteMailbox", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0005AD8B File Offset: 0x00058F8B
		public static LocalizedString SpellCheckerFrench
		{
			get
			{
				return new LocalizedString("SpellCheckerFrench", "Ex4AEFA2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0005ADA9 File Offset: 0x00058FA9
		public static LocalizedString ExUnknownRecurrenceBlobRange
		{
			get
			{
				return new LocalizedString("ExUnknownRecurrenceBlobRange", "Ex9988EF", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0005ADC8 File Offset: 0x00058FC8
		public static LocalizedString AmFailedToDeterminePAM(string dagName)
		{
			return new LocalizedString("AmFailedToDeterminePAM", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				dagName
			});
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0005ADF8 File Offset: 0x00058FF8
		public static LocalizedString ExInvalidMAPIType(uint type)
		{
			return new LocalizedString("ExInvalidMAPIType", "Ex8A2E1E", false, true, ServerStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0005AE2C File Offset: 0x0005902C
		public static LocalizedString MapiCannotOpenAttachmentId(object id)
		{
			return new LocalizedString("MapiCannotOpenAttachmentId", "ExF29E6F", false, true, ServerStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0005AE5C File Offset: 0x0005905C
		public static LocalizedString ExUnableToOpenOrCreateDefaultFolder(string defaultFolderName)
		{
			return new LocalizedString("ExUnableToOpenOrCreateDefaultFolder", "ExAD93AB", false, true, ServerStrings.ResourceManager, new object[]
			{
				defaultFolderName
			});
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0005AE8C File Offset: 0x0005908C
		public static LocalizedString TeamMailboxMessageWelcomeSubject(string tmName)
		{
			return new LocalizedString("TeamMailboxMessageWelcomeSubject", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				tmName
			});
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0005AEBB File Offset: 0x000590BB
		public static LocalizedString ExAttachmentCannotOpenDueToUnSave
		{
			get
			{
				return new LocalizedString("ExAttachmentCannotOpenDueToUnSave", "ExBD810D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0005AED9 File Offset: 0x000590D9
		public static LocalizedString ClientCulture_0x439
		{
			get
			{
				return new LocalizedString("ClientCulture_0x439", "Ex77B11F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0005AEF8 File Offset: 0x000590F8
		public static LocalizedString ConversionCorruptTnef(int complianceStatus)
		{
			return new LocalizedString("ConversionCorruptTnef", "Ex1A97B8", false, true, ServerStrings.ResourceManager, new object[]
			{
				complianceStatus
			});
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0005AF2C File Offset: 0x0005912C
		public static LocalizedString SpellCheckerEnglishCanada
		{
			get
			{
				return new LocalizedString("SpellCheckerEnglishCanada", "ExC86394", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0005AF4A File Offset: 0x0005914A
		public static LocalizedString MapiCannotUpdateDeferredActionMessages
		{
			get
			{
				return new LocalizedString("MapiCannotUpdateDeferredActionMessages", "Ex4B27D9", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0005AF68 File Offset: 0x00059168
		public static LocalizedString MigrationStatisticsCompleteStatus
		{
			get
			{
				return new LocalizedString("MigrationStatisticsCompleteStatus", "Ex35A837", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0005AF86 File Offset: 0x00059186
		public static LocalizedString OleConversionInvalidResultType
		{
			get
			{
				return new LocalizedString("OleConversionInvalidResultType", "ExEC0E54", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0005AFA4 File Offset: 0x000591A4
		public static LocalizedString UnableToMakeAutoDiscoveryRequest
		{
			get
			{
				return new LocalizedString("UnableToMakeAutoDiscoveryRequest", "ExF7FF53", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0005AFC2 File Offset: 0x000591C2
		public static LocalizedString NotificationEmailSubjectImportPst
		{
			get
			{
				return new LocalizedString("NotificationEmailSubjectImportPst", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0005AFE0 File Offset: 0x000591E0
		public static LocalizedString SharingMessageAttachmentNotFoundException
		{
			get
			{
				return new LocalizedString("SharingMessageAttachmentNotFoundException", "Ex2B46C8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0005B000 File Offset: 0x00059200
		public static LocalizedString ExNullSortOrderParameter(int index)
		{
			return new LocalizedString("ExNullSortOrderParameter", "Ex2308AA", false, true, ServerStrings.ResourceManager, new object[]
			{
				index
			});
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0005B034 File Offset: 0x00059234
		public static LocalizedString MigrationBatchStatusIncrementalSyncing
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusIncrementalSyncing", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0005B052 File Offset: 0x00059252
		public static LocalizedString SearchStateStopped
		{
			get
			{
				return new LocalizedString("SearchStateStopped", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0005B070 File Offset: 0x00059270
		public static LocalizedString PublicFolderMailboxNotFound
		{
			get
			{
				return new LocalizedString("PublicFolderMailboxNotFound", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0005B08E File Offset: 0x0005928E
		public static LocalizedString MapiCannotGetReceiveFolder
		{
			get
			{
				return new LocalizedString("MapiCannotGetReceiveFolder", "ExA9569C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0005B0AC File Offset: 0x000592AC
		public static LocalizedString MigrationUserStatusStarting
		{
			get
			{
				return new LocalizedString("MigrationUserStatusStarting", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0005B0CC File Offset: 0x000592CC
		public static LocalizedString UserSidNotFound(string userLegDn)
		{
			return new LocalizedString("UserSidNotFound", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				userLegDn
			});
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0005B0FC File Offset: 0x000592FC
		public static LocalizedString PublicFolderSyncFolderHierarchyFailed(string innerMessage)
		{
			return new LocalizedString("PublicFolderSyncFolderHierarchyFailed", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				innerMessage
			});
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0005B12B File Offset: 0x0005932B
		public static LocalizedString AmDbMountNotAllowedDueToLossException
		{
			get
			{
				return new LocalizedString("AmDbMountNotAllowedDueToLossException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x0005B149 File Offset: 0x00059349
		public static LocalizedString SpellCheckerGermanPreReform
		{
			get
			{
				return new LocalizedString("SpellCheckerGermanPreReform", "ExF478AF", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0005B167 File Offset: 0x00059367
		public static LocalizedString InvalidKindFormat
		{
			get
			{
				return new LocalizedString("InvalidKindFormat", "Ex93B836", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0005B185 File Offset: 0x00059385
		public static LocalizedString OleUnableToReadAttachment
		{
			get
			{
				return new LocalizedString("OleUnableToReadAttachment", "Ex60F765", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0005B1A3 File Offset: 0x000593A3
		public static LocalizedString InvalidModifier
		{
			get
			{
				return new LocalizedString("InvalidModifier", "Ex61E271", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0005B1C1 File Offset: 0x000593C1
		public static LocalizedString MigrationUserStatusProvisionUpdating
		{
			get
			{
				return new LocalizedString("MigrationUserStatusProvisionUpdating", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0005B1DF File Offset: 0x000593DF
		public static LocalizedString MigrationMailboxPermissionFullAccess
		{
			get
			{
				return new LocalizedString("MigrationMailboxPermissionFullAccess", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0005B200 File Offset: 0x00059400
		public static LocalizedString MailboxNotFoundByAdObjectId(string adObjectId)
		{
			return new LocalizedString("MailboxNotFoundByAdObjectId", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				adObjectId
			});
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0005B230 File Offset: 0x00059430
		public static LocalizedString idResourceQuarantinedDueToBlackHole(Guid resourceIdentity)
		{
			return new LocalizedString("idResourceQuarantinedDueToBlackHole", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				resourceIdentity
			});
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x0005B264 File Offset: 0x00059464
		public static LocalizedString WeatherUnitCelsius
		{
			get
			{
				return new LocalizedString("WeatherUnitCelsius", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0005B282 File Offset: 0x00059482
		public static LocalizedString NullDateInChangeDate
		{
			get
			{
				return new LocalizedString("NullDateInChangeDate", "ExF7EDF1", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0005B2A0 File Offset: 0x000594A0
		public static LocalizedString ClientCulture_0x2C09
		{
			get
			{
				return new LocalizedString("ClientCulture_0x2C09", "ExE1BF2F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0005B2BE File Offset: 0x000594BE
		public static LocalizedString MigrationBatchDirectionOffboarding
		{
			get
			{
				return new LocalizedString("MigrationBatchDirectionOffboarding", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0005B2DC File Offset: 0x000594DC
		public static LocalizedString ExInvalidParameter(string argumentName, int argumentNumber)
		{
			return new LocalizedString("ExInvalidParameter", "Ex234101", false, true, ServerStrings.ResourceManager, new object[]
			{
				argumentName,
				argumentNumber
			});
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0005B314 File Offset: 0x00059514
		public static LocalizedString CalNotifTypeUninteresting
		{
			get
			{
				return new LocalizedString("CalNotifTypeUninteresting", "Ex8096C6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0005B332 File Offset: 0x00059532
		public static LocalizedString AmDatabaseNeverMountedException
		{
			get
			{
				return new LocalizedString("AmDatabaseNeverMountedException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0005B350 File Offset: 0x00059550
		public static LocalizedString MapiCannotSetReceiveFolder
		{
			get
			{
				return new LocalizedString("MapiCannotSetReceiveFolder", "Ex199343", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x0005B36E File Offset: 0x0005956E
		public static LocalizedString RpcClientWrapperFailedToLoadTopology
		{
			get
			{
				return new LocalizedString("RpcClientWrapperFailedToLoadTopology", "ExF24F3F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0005B38C File Offset: 0x0005958C
		public static LocalizedString ExCorruptConversationActionItem
		{
			get
			{
				return new LocalizedString("ExCorruptConversationActionItem", "Ex663F93", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0005B3AC File Offset: 0x000595AC
		public static LocalizedString FailedToUnprotectAttachment(string filename)
		{
			return new LocalizedString("FailedToUnprotectAttachment", "Ex59A6ED", false, true, ServerStrings.ResourceManager, new object[]
			{
				filename
			});
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0005B3DC File Offset: 0x000595DC
		public static LocalizedString SearchMandatoryParameter(string p1)
		{
			return new LocalizedString("SearchMandatoryParameter", "Ex1C995B", false, true, ServerStrings.ResourceManager, new object[]
			{
				p1
			});
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0005B40C File Offset: 0x0005960C
		public static LocalizedString InvalidDuration(int duration)
		{
			return new LocalizedString("InvalidDuration", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				duration
			});
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0005B440 File Offset: 0x00059640
		public static LocalizedString Tuesday
		{
			get
			{
				return new LocalizedString("Tuesday", "Ex180ABD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0005B45E File Offset: 0x0005965E
		public static LocalizedString MapiCannotCreateRestriction
		{
			get
			{
				return new LocalizedString("MapiCannotCreateRestriction", "Ex12B44B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0005B47C File Offset: 0x0005967C
		public static LocalizedString CorruptJunkMoveStamp
		{
			get
			{
				return new LocalizedString("CorruptJunkMoveStamp", "Ex2FEEB0", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0005B49A File Offset: 0x0005969A
		public static LocalizedString InvalidAttachmentNumber
		{
			get
			{
				return new LocalizedString("InvalidAttachmentNumber", "Ex2371CF", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0005B4B8 File Offset: 0x000596B8
		public static LocalizedString ClientCulture_0x83E
		{
			get
			{
				return new LocalizedString("ClientCulture_0x83E", "ExD610E5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0005B4D8 File Offset: 0x000596D8
		public static LocalizedString NotificationEmailSubjectFailed(string notificationType)
		{
			return new LocalizedString("NotificationEmailSubjectFailed", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				notificationType
			});
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0005B507 File Offset: 0x00059707
		public static LocalizedString Friday
		{
			get
			{
				return new LocalizedString("Friday", "Ex5D0CA6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0005B525 File Offset: 0x00059725
		public static LocalizedString NoServerValueAvailable
		{
			get
			{
				return new LocalizedString("NoServerValueAvailable", "ExA26E67", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0005B543 File Offset: 0x00059743
		public static LocalizedString DelegateInvalidPermission
		{
			get
			{
				return new LocalizedString("DelegateInvalidPermission", "ExFC228F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0005B561 File Offset: 0x00059761
		public static LocalizedString OperationAborted
		{
			get
			{
				return new LocalizedString("OperationAborted", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0005B57F File Offset: 0x0005977F
		public static LocalizedString DiscoveryMailboxNotFound
		{
			get
			{
				return new LocalizedString("DiscoveryMailboxNotFound", "ExAB49AD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0005B59D File Offset: 0x0005979D
		public static LocalizedString ClientCulture_0x422
		{
			get
			{
				return new LocalizedString("ClientCulture_0x422", "Ex496440", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x0005B5BB File Offset: 0x000597BB
		public static LocalizedString MigrationUserStatusSummarySynced
		{
			get
			{
				return new LocalizedString("MigrationUserStatusSummarySynced", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0005B5D9 File Offset: 0x000597D9
		public static LocalizedString FourHours
		{
			get
			{
				return new LocalizedString("FourHours", "Ex21670B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0005B5F7 File Offset: 0x000597F7
		public static LocalizedString MigrationUserStatusCompleted
		{
			get
			{
				return new LocalizedString("MigrationUserStatusCompleted", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0005B615 File Offset: 0x00059815
		public static LocalizedString ExVotingBlobCorrupt
		{
			get
			{
				return new LocalizedString("ExVotingBlobCorrupt", "ExBEF25D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0005B634 File Offset: 0x00059834
		public static LocalizedString InternalLicensingDisabledForTenant(OrganizationId orgId)
		{
			return new LocalizedString("InternalLicensingDisabledForTenant", "ExA60B3F", false, true, ServerStrings.ResourceManager, new object[]
			{
				orgId
			});
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0005B663 File Offset: 0x00059863
		public static LocalizedString HexadecimalHtmlColorPatternDescription
		{
			get
			{
				return new LocalizedString("HexadecimalHtmlColorPatternDescription", "Ex7F3109", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0005B684 File Offset: 0x00059884
		public static LocalizedString ExBatchBuilderADLookupFailed(ProxyAddress proxyAddress, object error)
		{
			return new LocalizedString("ExBatchBuilderADLookupFailed", "Ex65A385", false, true, ServerStrings.ResourceManager, new object[]
			{
				proxyAddress,
				error
			});
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0005B6B7 File Offset: 0x000598B7
		public static LocalizedString MigrationStepInitialization
		{
			get
			{
				return new LocalizedString("MigrationStepInitialization", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0005B6D5 File Offset: 0x000598D5
		public static LocalizedString TeamMailboxSyncStatusMembershipSyncFailureOnly
		{
			get
			{
				return new LocalizedString("TeamMailboxSyncStatusMembershipSyncFailureOnly", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x0005B6F3 File Offset: 0x000598F3
		public static LocalizedString InvalidEncryptedSharedFolderDataException
		{
			get
			{
				return new LocalizedString("InvalidEncryptedSharedFolderDataException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0005B714 File Offset: 0x00059914
		public static LocalizedString ActiveMonitoringServiceDown(string serverName, string rpcErrorMessage)
		{
			return new LocalizedString("ActiveMonitoringServiceDown", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				serverName,
				rpcErrorMessage
			});
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0005B747 File Offset: 0x00059947
		public static LocalizedString ExCorruptRestrictionFilter
		{
			get
			{
				return new LocalizedString("ExCorruptRestrictionFilter", "ExA40FB8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0005B768 File Offset: 0x00059968
		public static LocalizedString ErrorStatisticsStartIndexIsOutOfBound(int startIndex, int totalKeywords)
		{
			return new LocalizedString("ErrorStatisticsStartIndexIsOutOfBound", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				startIndex,
				totalKeywords
			});
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0005B7A8 File Offset: 0x000599A8
		public static LocalizedString ExServerNotFound(string serverName)
		{
			return new LocalizedString("ExServerNotFound", "Ex2775B7", false, true, ServerStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0005B7D7 File Offset: 0x000599D7
		public static LocalizedString ErrorNotificationAlreadyExists
		{
			get
			{
				return new LocalizedString("ErrorNotificationAlreadyExists", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0005B7F8 File Offset: 0x000599F8
		public static LocalizedString CanUseApiOnlyWhenTimeZoneIsNull(string api)
		{
			return new LocalizedString("CanUseApiOnlyWhenTimeZoneIsNull", "ExC62EB7", false, true, ServerStrings.ResourceManager, new object[]
			{
				api
			});
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0005B827 File Offset: 0x00059A27
		public static LocalizedString ExItemIsOpenedInReadOnlyMode
		{
			get
			{
				return new LocalizedString("ExItemIsOpenedInReadOnlyMode", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0005B845 File Offset: 0x00059A45
		public static LocalizedString UnbalancedParenthesis
		{
			get
			{
				return new LocalizedString("UnbalancedParenthesis", "Ex4F2B12", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0005B864 File Offset: 0x00059A64
		public static LocalizedString ExUnsupportedMapiType(Type type)
		{
			return new LocalizedString("ExUnsupportedMapiType", "ExD122BE", false, true, ServerStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0005B894 File Offset: 0x00059A94
		public static LocalizedString DataMoveReplicationConstraintNotSatisfiedNoHealthyCopies(DataMoveReplicationConstraintParameter constraint, Guid databaseGuid)
		{
			return new LocalizedString("DataMoveReplicationConstraintNotSatisfiedNoHealthyCopies", "Ex998A23", false, true, ServerStrings.ResourceManager, new object[]
			{
				constraint,
				databaseGuid
			});
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0005B8D1 File Offset: 0x00059AD1
		public static LocalizedString InvalidRpMsgFormat
		{
			get
			{
				return new LocalizedString("InvalidRpMsgFormat", "ExB640A2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0005B8F0 File Offset: 0x00059AF0
		public static LocalizedString DagNetworkManagementError(string err)
		{
			return new LocalizedString("DagNetworkManagementError", "ExE6256F", false, true, ServerStrings.ResourceManager, new object[]
			{
				err
			});
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0005B91F File Offset: 0x00059B1F
		public static LocalizedString UserPhotoNotAnImage
		{
			get
			{
				return new LocalizedString("UserPhotoNotAnImage", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0005B93D File Offset: 0x00059B3D
		public static LocalizedString MapiCannotCreateEntryIdFromShortTermId
		{
			get
			{
				return new LocalizedString("MapiCannotCreateEntryIdFromShortTermId", "Ex1B40B7", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0005B95B File Offset: 0x00059B5B
		public static LocalizedString ClientCulture_0x807
		{
			get
			{
				return new LocalizedString("ClientCulture_0x807", "ExFB27B1", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0005B97C File Offset: 0x00059B7C
		public static LocalizedString ActiveMonitoringServerTransientException(string errorMessage)
		{
			return new LocalizedString("ActiveMonitoringServerTransientException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0005B9AB File Offset: 0x00059BAB
		public static LocalizedString MapiCannotCreateBookmark
		{
			get
			{
				return new LocalizedString("MapiCannotCreateBookmark", "ExFF98D4", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0005B9C9 File Offset: 0x00059BC9
		public static LocalizedString InvalidateDateRange
		{
			get
			{
				return new LocalizedString("InvalidateDateRange", "Ex0F12D9", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x0005B9E7 File Offset: 0x00059BE7
		public static LocalizedString MigrationUserAdminTypeUnknown
		{
			get
			{
				return new LocalizedString("MigrationUserAdminTypeUnknown", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0005BA05 File Offset: 0x00059C05
		public static LocalizedString CannotMoveOrCopyBetweenPrivateAndPublicMailbox
		{
			get
			{
				return new LocalizedString("CannotMoveOrCopyBetweenPrivateAndPublicMailbox", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0005BA24 File Offset: 0x00059C24
		public static LocalizedString TeamMailboxMessageClosedSubject(string tmName)
		{
			return new LocalizedString("TeamMailboxMessageClosedSubject", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				tmName
			});
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0005BA54 File Offset: 0x00059C54
		public static LocalizedString ErrorSavingSearchObject(string id)
		{
			return new LocalizedString("ErrorSavingSearchObject", "ExCBA4AC", false, true, ServerStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0005BA83 File Offset: 0x00059C83
		public static LocalizedString SpellCheckerNorwegianBokmal
		{
			get
			{
				return new LocalizedString("SpellCheckerNorwegianBokmal", "ExF3957A", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0005BAA4 File Offset: 0x00059CA4
		public static LocalizedString FolderSaveOperationResult(LocalizedString operationResult, int errorCount, string propertyErrors)
		{
			return new LocalizedString("FolderSaveOperationResult", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				operationResult,
				errorCount,
				propertyErrors
			});
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0005BAE5 File Offset: 0x00059CE5
		public static LocalizedString ClientCulture_0x1007
		{
			get
			{
				return new LocalizedString("ClientCulture_0x1007", "Ex562DC5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0005BB03 File Offset: 0x00059D03
		public static LocalizedString MigrationBatchSupportedActionStop
		{
			get
			{
				return new LocalizedString("MigrationBatchSupportedActionStop", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0005BB21 File Offset: 0x00059D21
		public static LocalizedString CrossForestNotSupported
		{
			get
			{
				return new LocalizedString("CrossForestNotSupported", "ExCA5E73", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0005BB3F File Offset: 0x00059D3F
		public static LocalizedString ExCannotAccessSystemFolderId
		{
			get
			{
				return new LocalizedString("ExCannotAccessSystemFolderId", "Ex29F424", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0005BB5D File Offset: 0x00059D5D
		public static LocalizedString ClientCulture_0x410
		{
			get
			{
				return new LocalizedString("ClientCulture_0x410", "Ex0E6E86", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0005BB7C File Offset: 0x00059D7C
		public static LocalizedString FailedToLocateTPDConfig(string orgId)
		{
			return new LocalizedString("FailedToLocateTPDConfig", "Ex054C4E", false, true, ServerStrings.ResourceManager, new object[]
			{
				orgId
			});
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0005BBAB File Offset: 0x00059DAB
		public static LocalizedString ExStatefulFilterMustBeSetWhenSetSyncFiltersIsInvokedWithNullFilter
		{
			get
			{
				return new LocalizedString("ExStatefulFilterMustBeSetWhenSetSyncFiltersIsInvokedWithNullFilter", "Ex678A08", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0005BBC9 File Offset: 0x00059DC9
		public static LocalizedString MapiCannotReadPermissions
		{
			get
			{
				return new LocalizedString("MapiCannotReadPermissions", "Ex043A0E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0005BBE7 File Offset: 0x00059DE7
		public static LocalizedString FolderRuleStageOnPublicFolderBefore
		{
			get
			{
				return new LocalizedString("FolderRuleStageOnPublicFolderBefore", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0005BC05 File Offset: 0x00059E05
		public static LocalizedString UnbalancedQuote
		{
			get
			{
				return new LocalizedString("UnbalancedQuote", "Ex107021", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0005BC23 File Offset: 0x00059E23
		public static LocalizedString FailedToWriteActivityLog
		{
			get
			{
				return new LocalizedString("FailedToWriteActivityLog", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0005BC41 File Offset: 0x00059E41
		public static LocalizedString NoFreeBusyFolder
		{
			get
			{
				return new LocalizedString("NoFreeBusyFolder", "Ex44578F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0005BC5F File Offset: 0x00059E5F
		public static LocalizedString ClientCulture_0x408
		{
			get
			{
				return new LocalizedString("ClientCulture_0x408", "Ex6C6C30", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0005BC80 File Offset: 0x00059E80
		public static LocalizedString ExInvalidExceptionListWithDoubleEntry(object date)
		{
			return new LocalizedString("ExInvalidExceptionListWithDoubleEntry", "ExC81AA6", false, true, ServerStrings.ResourceManager, new object[]
			{
				date
			});
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x0005BCAF File Offset: 0x00059EAF
		public static LocalizedString SharingConflictException
		{
			get
			{
				return new LocalizedString("SharingConflictException", "ExF63A28", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x0005BCCD File Offset: 0x00059ECD
		public static LocalizedString MapiCannotQueryColumns
		{
			get
			{
				return new LocalizedString("MapiCannotQueryColumns", "ExFBC1B3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x0005BCEB File Offset: 0x00059EEB
		public static LocalizedString ExCaughtMapiExceptionWhileReadingEvents
		{
			get
			{
				return new LocalizedString("ExCaughtMapiExceptionWhileReadingEvents", "Ex2F746B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0005BD09 File Offset: 0x00059F09
		public static LocalizedString ClientCulture_0x81D
		{
			get
			{
				return new LocalizedString("ClientCulture_0x81D", "ExB8F2B4", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0005BD28 File Offset: 0x00059F28
		public static LocalizedString ExCannotCreateFolder(object saveResult)
		{
			return new LocalizedString("ExCannotCreateFolder", "Ex13C206", false, true, ServerStrings.ResourceManager, new object[]
			{
				saveResult
			});
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0005BD57 File Offset: 0x00059F57
		public static LocalizedString ConversionInvalidSmimeContent
		{
			get
			{
				return new LocalizedString("ConversionInvalidSmimeContent", "Ex4BD2C4", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0005BD75 File Offset: 0x00059F75
		public static LocalizedString ExCannotRevertSentMeetingToAppointment
		{
			get
			{
				return new LocalizedString("ExCannotRevertSentMeetingToAppointment", "Ex5B5AC4", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0005BD94 File Offset: 0x00059F94
		public static LocalizedString ExInvalidDateTimeRange(object startTime, object endTime)
		{
			return new LocalizedString("ExInvalidDateTimeRange", "ExFD3ECD", false, true, ServerStrings.ResourceManager, new object[]
			{
				startTime,
				endTime
			});
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0005BDC8 File Offset: 0x00059FC8
		public static LocalizedString AmServerNotFoundException(string server)
		{
			return new LocalizedString("AmServerNotFoundException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0005BDF8 File Offset: 0x00059FF8
		public static LocalizedString UserHasNoEventPermisson(string folderId)
		{
			return new LocalizedString("UserHasNoEventPermisson", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				folderId
			});
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0005BE28 File Offset: 0x0005A028
		public static LocalizedString RpcServerRequestAlreadyPending(string mailboxGuid, string pendingClient, string pendingSince)
		{
			return new LocalizedString("RpcServerRequestAlreadyPending", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				mailboxGuid,
				pendingClient,
				pendingSince
			});
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0005BE60 File Offset: 0x0005A060
		public static LocalizedString ExUnsupportedABProvider(string provider, string version)
		{
			return new LocalizedString("ExUnsupportedABProvider", "ExC220F9", false, true, ServerStrings.ResourceManager, new object[]
			{
				provider,
				version
			});
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0005BE93 File Offset: 0x0005A093
		public static LocalizedString ExMustSaveFolderToMakeVisibleToOutlook
		{
			get
			{
				return new LocalizedString("ExMustSaveFolderToMakeVisibleToOutlook", "ExB803F3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0005BEB1 File Offset: 0x0005A0B1
		public static LocalizedString UnsupportedContentRestriction
		{
			get
			{
				return new LocalizedString("UnsupportedContentRestriction", "ExAC8684", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0005BED0 File Offset: 0x0005A0D0
		public static LocalizedString MigrationItemStatisticsString(string emailAddress, long itemsSynced, long itemsSkipped, string startTime)
		{
			return new LocalizedString("MigrationItemStatisticsString", "Ex5D8F20", false, true, ServerStrings.ResourceManager, new object[]
			{
				emailAddress,
				itemsSynced,
				itemsSkipped,
				startTime
			});
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x0005BF15 File Offset: 0x0005A115
		public static LocalizedString ClientCulture_0x42D
		{
			get
			{
				return new LocalizedString("ClientCulture_0x42D", "Ex5BB852", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0005BF34 File Offset: 0x0005A134
		public static LocalizedString ValidationFailureAfterPromotion(string uid)
		{
			return new LocalizedString("ValidationFailureAfterPromotion", "Ex2465C2", false, true, ServerStrings.ResourceManager, new object[]
			{
				uid
			});
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0005BF64 File Offset: 0x0005A164
		public static LocalizedString ExCurrentServerNotInSite(string serverName)
		{
			return new LocalizedString("ExCurrentServerNotInSite", "Ex6F891D", false, true, ServerStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0005BF93 File Offset: 0x0005A193
		public static LocalizedString NotificationEmailBodyImportPSTCreated
		{
			get
			{
				return new LocalizedString("NotificationEmailBodyImportPSTCreated", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0005BFB1 File Offset: 0x0005A1B1
		public static LocalizedString SearchTargetInSource
		{
			get
			{
				return new LocalizedString("SearchTargetInSource", "Ex615213", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0005BFCF File Offset: 0x0005A1CF
		public static LocalizedString ExAddItemAttachmentFailed
		{
			get
			{
				return new LocalizedString("ExAddItemAttachmentFailed", "ExE2BCCE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0005BFF0 File Offset: 0x0005A1F0
		public static LocalizedString CannotResolvePropertyException(string propertySchema)
		{
			return new LocalizedString("CannotResolvePropertyException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				propertySchema
			});
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0005C020 File Offset: 0x0005A220
		public static LocalizedString FailedToVerifyDRMPropsSignatureADError(string sid)
		{
			return new LocalizedString("FailedToVerifyDRMPropsSignatureADError", "ExEF0CC5", false, true, ServerStrings.ResourceManager, new object[]
			{
				sid
			});
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0005C050 File Offset: 0x0005A250
		public static LocalizedString RpcServerIgnorePendingDeleteTeamMailbox(string mailboxGuid)
		{
			return new LocalizedString("RpcServerIgnorePendingDeleteTeamMailbox", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				mailboxGuid
			});
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0005C07F File Offset: 0x0005A27F
		public static LocalizedString ClientCulture_0x403
		{
			get
			{
				return new LocalizedString("ClientCulture_0x403", "ExF12F46", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0005C09D File Offset: 0x0005A29D
		public static LocalizedString ExCannotMoveOrDeleteDefaultFolders
		{
			get
			{
				return new LocalizedString("ExCannotMoveOrDeleteDefaultFolders", "Ex5E676E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0005C0BC File Offset: 0x0005A2BC
		public static LocalizedString MigrationObjectsCountStringGroups(string groups)
		{
			return new LocalizedString("MigrationObjectsCountStringGroups", "ExCE86CB", false, true, ServerStrings.ResourceManager, new object[]
			{
				groups
			});
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0005C0EB File Offset: 0x0005A2EB
		public static LocalizedString ExCannotSeekRow
		{
			get
			{
				return new LocalizedString("ExCannotSeekRow", "Ex143A07", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0005C10C File Offset: 0x0005A30C
		public static LocalizedString ExInvalidMonthlyDayOfMonth(int dayOfMonth)
		{
			return new LocalizedString("ExInvalidMonthlyDayOfMonth", "ExA6D522", false, true, ServerStrings.ResourceManager, new object[]
			{
				dayOfMonth
			});
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0005C140 File Offset: 0x0005A340
		public static LocalizedString ErrorCalendarReminderNotMinutes(string value)
		{
			return new LocalizedString("ErrorCalendarReminderNotMinutes", "Ex6D6701", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0005C16F File Offset: 0x0005A36F
		public static LocalizedString MigrationReportBatch
		{
			get
			{
				return new LocalizedString("MigrationReportBatch", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0005C18D File Offset: 0x0005A38D
		public static LocalizedString ExErrorInDetectE15Store
		{
			get
			{
				return new LocalizedString("ExErrorInDetectE15Store", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0005C1AB File Offset: 0x0005A3AB
		public static LocalizedString idDefaultFoldersNotLocalizedException
		{
			get
			{
				return new LocalizedString("idDefaultFoldersNotLocalizedException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0005C1C9 File Offset: 0x0005A3C9
		public static LocalizedString MigrationStateCompleted
		{
			get
			{
				return new LocalizedString("MigrationStateCompleted", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0005C1E8 File Offset: 0x0005A3E8
		public static LocalizedString ExCorrelationFailedForOccurrence(string subject)
		{
			return new LocalizedString("ExCorrelationFailedForOccurrence", "Ex36138C", false, true, ServerStrings.ResourceManager, new object[]
			{
				subject
			});
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0005C217 File Offset: 0x0005A417
		public static LocalizedString ErrorMissingMailboxOrPermission
		{
			get
			{
				return new LocalizedString("ErrorMissingMailboxOrPermission", "ExBAA10A", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0005C238 File Offset: 0x0005A438
		public static LocalizedString ErrorTimeProposalInvalidDuration(int days)
		{
			return new LocalizedString("ErrorTimeProposalInvalidDuration", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				days
			});
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0005C26C File Offset: 0x0005A46C
		public static LocalizedString MigrationFolderNotFound(string mailboxName)
		{
			return new LocalizedString("MigrationFolderNotFound", "Ex9B7920", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailboxName
			});
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0005C29B File Offset: 0x0005A49B
		public static LocalizedString ClientCulture_0x140C
		{
			get
			{
				return new LocalizedString("ClientCulture_0x140C", "ExA20802", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x0005C2B9 File Offset: 0x0005A4B9
		public static LocalizedString MigrationTypeIMAP
		{
			get
			{
				return new LocalizedString("MigrationTypeIMAP", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0005C2D7 File Offset: 0x0005A4D7
		public static LocalizedString ClientCulture_0x2809
		{
			get
			{
				return new LocalizedString("ClientCulture_0x2809", "ExF70A3B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0005C2F5 File Offset: 0x0005A4F5
		public static LocalizedString ClientCulture_0x1404
		{
			get
			{
				return new LocalizedString("ClientCulture_0x1404", "ExF8CE5F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0005C313 File Offset: 0x0005A513
		public static LocalizedString ExCannotRejectDeletes
		{
			get
			{
				return new LocalizedString("ExCannotRejectDeletes", "ExB96859", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x0005C331 File Offset: 0x0005A531
		public static LocalizedString TeamMailboxSyncStatusMaintenanceSyncFailureOnly
		{
			get
			{
				return new LocalizedString("TeamMailboxSyncStatusMaintenanceSyncFailureOnly", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0005C34F File Offset: 0x0005A54F
		public static LocalizedString MigrationStateStopped
		{
			get
			{
				return new LocalizedString("MigrationStateStopped", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0005C36D File Offset: 0x0005A56D
		public static LocalizedString MigrationStageDiscovery
		{
			get
			{
				return new LocalizedString("MigrationStageDiscovery", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0005C38C File Offset: 0x0005A58C
		public static LocalizedString MigrationMailboxDatabaseInfoNotAvailable(string mbxid)
		{
			return new LocalizedString("MigrationMailboxDatabaseInfoNotAvailable", "Ex7F3CA2", false, true, ServerStrings.ResourceManager, new object[]
			{
				mbxid
			});
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0005C3BB File Offset: 0x0005A5BB
		public static LocalizedString ClientCulture_0x40A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x40A", "Ex901F0E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x0005C3D9 File Offset: 0x0005A5D9
		public static LocalizedString NotificationEmailBodyCertExpired
		{
			get
			{
				return new LocalizedString("NotificationEmailBodyCertExpired", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0005C3F8 File Offset: 0x0005A5F8
		public static LocalizedString MultipleSystemMigrationMailboxes(string mailboxName)
		{
			return new LocalizedString("MultipleSystemMigrationMailboxes", "ExFD4BE3", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailboxName
			});
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0005C427 File Offset: 0x0005A627
		public static LocalizedString ExCannotRejectSameOperationTwice
		{
			get
			{
				return new LocalizedString("ExCannotRejectSameOperationTwice", "Ex272E05", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0005C445 File Offset: 0x0005A645
		public static LocalizedString ExCannotGetSearchCriteria
		{
			get
			{
				return new LocalizedString("ExCannotGetSearchCriteria", "Ex7842CB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0005C463 File Offset: 0x0005A663
		public static LocalizedString ExInvalidMaxQueueSize
		{
			get
			{
				return new LocalizedString("ExInvalidMaxQueueSize", "ExDC03B5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0005C481 File Offset: 0x0005A681
		public static LocalizedString ADException
		{
			get
			{
				return new LocalizedString("ADException", "Ex4589A3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0005C49F File Offset: 0x0005A69F
		public static LocalizedString ExNoMailboxOwner
		{
			get
			{
				return new LocalizedString("ExNoMailboxOwner", "Ex007029", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0005C4BD File Offset: 0x0005A6BD
		public static LocalizedString ExNotConnected
		{
			get
			{
				return new LocalizedString("ExNotConnected", "ExC4FA49", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0005C4DB File Offset: 0x0005A6DB
		public static LocalizedString SearchStateStopping
		{
			get
			{
				return new LocalizedString("SearchStateStopping", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0005C4F9 File Offset: 0x0005A6F9
		public static LocalizedString SpellCheckerKorean
		{
			get
			{
				return new LocalizedString("SpellCheckerKorean", "ExF7EA11", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0005C518 File Offset: 0x0005A718
		public static LocalizedString ExInvalidNamedProperty(string property)
		{
			return new LocalizedString("ExInvalidNamedProperty", "Ex48473F", false, true, ServerStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0005C548 File Offset: 0x0005A748
		public static LocalizedString DagNetworkRenameDupName(string oldName, string newName)
		{
			return new LocalizedString("DagNetworkRenameDupName", "ExCE24CA", false, true, ServerStrings.ResourceManager, new object[]
			{
				oldName,
				newName
			});
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0005C57C File Offset: 0x0005A77C
		public static LocalizedString ErrorInvalidQueryLanguage(string name, string language)
		{
			return new LocalizedString("ErrorInvalidQueryLanguage", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				name,
				language
			});
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0005C5B0 File Offset: 0x0005A7B0
		public static LocalizedString ExValueOfWrongType(object value, Type type)
		{
			return new LocalizedString("ExValueOfWrongType", "Ex5CBBF4", false, true, ServerStrings.ResourceManager, new object[]
			{
				value,
				type
			});
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0005C5E4 File Offset: 0x0005A7E4
		public static LocalizedString InvalidReminderTime(string reminderTime, string referenceTime)
		{
			return new LocalizedString("InvalidReminderTime", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				reminderTime,
				referenceTime
			});
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x0005C617 File Offset: 0x0005A817
		public static LocalizedString MigrationTypeExchangeLocalMove
		{
			get
			{
				return new LocalizedString("MigrationTypeExchangeLocalMove", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0005C638 File Offset: 0x0005A838
		public static LocalizedString InvalidExternalDirectoryObjectIdError(string externalDirectoryObjectId)
		{
			return new LocalizedString("InvalidExternalDirectoryObjectIdError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				externalDirectoryObjectId
			});
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x0005C667 File Offset: 0x0005A867
		public static LocalizedString MapiCannotSubmitMessage
		{
			get
			{
				return new LocalizedString("MapiCannotSubmitMessage", "ExD4E03D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0005C685 File Offset: 0x0005A885
		public static LocalizedString ClientCulture_0x1C09
		{
			get
			{
				return new LocalizedString("ClientCulture_0x1C09", "Ex398E95", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0005C6A4 File Offset: 0x0005A8A4
		public static LocalizedString idUnableToCreateDefaultCalendarGroupException(string calendarType)
		{
			return new LocalizedString("idUnableToCreateDefaultCalendarGroupException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				calendarType
			});
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0005C6D4 File Offset: 0x0005A8D4
		public static LocalizedString ExInvalidWeeklyDayMask(object dayMask)
		{
			return new LocalizedString("ExInvalidWeeklyDayMask", "Ex26FC14", false, true, ServerStrings.ResourceManager, new object[]
			{
				dayMask
			});
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x0005C703 File Offset: 0x0005A903
		public static LocalizedString ExInvalidOrder
		{
			get
			{
				return new LocalizedString("ExInvalidOrder", "Ex8F7E7A", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0005C724 File Offset: 0x0005A924
		public static LocalizedString IncorrectEntriesInServiceLocationResponse(int numResponses, int expected)
		{
			return new LocalizedString("IncorrectEntriesInServiceLocationResponse", "ExFCEB21", false, true, ServerStrings.ResourceManager, new object[]
			{
				numResponses,
				expected
			});
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0005C764 File Offset: 0x0005A964
		public static LocalizedString InconsistentCalendarMethod(string method, string id)
		{
			return new LocalizedString("InconsistentCalendarMethod", "Ex408F1F", false, true, ServerStrings.ResourceManager, new object[]
			{
				method,
				id
			});
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0005C797 File Offset: 0x0005A997
		public static LocalizedString NoProviderSupportShareFolder
		{
			get
			{
				return new LocalizedString("NoProviderSupportShareFolder", "Ex0497E6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0005C7B5 File Offset: 0x0005A9B5
		public static LocalizedString ExConnectionCacheSizeNotSet
		{
			get
			{
				return new LocalizedString("ExConnectionCacheSizeNotSet", "Ex554DB9", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0005C7D3 File Offset: 0x0005A9D3
		public static LocalizedString MigrationFlagsRemove
		{
			get
			{
				return new LocalizedString("MigrationFlagsRemove", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0005C7F4 File Offset: 0x0005A9F4
		public static LocalizedString CannotCreateSynchronizerEx(Type xsoManifestType)
		{
			return new LocalizedString("CannotCreateSynchronizerEx", "ExE5BAE6", false, true, ServerStrings.ResourceManager, new object[]
			{
				xsoManifestType
			});
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0005C823 File Offset: 0x0005AA23
		public static LocalizedString ExInvalidRecipient
		{
			get
			{
				return new LocalizedString("ExInvalidRecipient", "ExE8E5B3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0005C841 File Offset: 0x0005AA41
		public static LocalizedString ExFoundInvalidRowType
		{
			get
			{
				return new LocalizedString("ExFoundInvalidRowType", "Ex55BFE3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0005C85F File Offset: 0x0005AA5F
		public static LocalizedString ExInvalidOffset
		{
			get
			{
				return new LocalizedString("ExInvalidOffset", "ExB3B3DA", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x0005C87D File Offset: 0x0005AA7D
		public static LocalizedString NotEnoughPermissionsToPerformOperation
		{
			get
			{
				return new LocalizedString("NotEnoughPermissionsToPerformOperation", "Ex1B6032", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0005C89C File Offset: 0x0005AA9C
		public static LocalizedString ExInvalidExceptionCount(int exceptionListCount, int exceptionInfoCount)
		{
			return new LocalizedString("ExInvalidExceptionCount", "Ex5994AD", false, true, ServerStrings.ResourceManager, new object[]
			{
				exceptionListCount,
				exceptionInfoCount
			});
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0005C8DC File Offset: 0x0005AADC
		public static LocalizedString RemoteAccountPolicyNotFound(string policy)
		{
			return new LocalizedString("RemoteAccountPolicyNotFound", "Ex226A6A", false, true, ServerStrings.ResourceManager, new object[]
			{
				policy
			});
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0005C90B File Offset: 0x0005AB0B
		public static LocalizedString MigrationStatisticsPartiallyCompleteStatus
		{
			get
			{
				return new LocalizedString("MigrationStatisticsPartiallyCompleteStatus", "Ex7F1C00", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0005C929 File Offset: 0x0005AB29
		public static LocalizedString TeamMailboxSyncStatusNotAvailable
		{
			get
			{
				return new LocalizedString("TeamMailboxSyncStatusNotAvailable", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0005C947 File Offset: 0x0005AB47
		public static LocalizedString InvalidOperator
		{
			get
			{
				return new LocalizedString("InvalidOperator", "Ex138281", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x0005C965 File Offset: 0x0005AB65
		public static LocalizedString DefaultHtmlAttachmentHrefText
		{
			get
			{
				return new LocalizedString("DefaultHtmlAttachmentHrefText", "Ex2D9CFF", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0005C984 File Offset: 0x0005AB84
		public static LocalizedString IncorrectServerError(SmtpAddress targetMailboxSmtpAddress, string expectedServerFqdn)
		{
			return new LocalizedString("IncorrectServerError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				targetMailboxSmtpAddress,
				expectedServerFqdn
			});
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0005C9BC File Offset: 0x0005ABBC
		public static LocalizedString ExInvalidYearlyRecurrencePeriod(int period)
		{
			return new LocalizedString("ExInvalidYearlyRecurrencePeriod", "Ex45CE8D", false, true, ServerStrings.ResourceManager, new object[]
			{
				period
			});
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0005C9F0 File Offset: 0x0005ABF0
		public static LocalizedString ConversionBodyCorrupt
		{
			get
			{
				return new LocalizedString("ConversionBodyCorrupt", "ExE250BB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0005CA10 File Offset: 0x0005AC10
		public static LocalizedString RpcServerDirectoryError(string mailboxGuid, string error)
		{
			return new LocalizedString("RpcServerDirectoryError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				mailboxGuid,
				error
			});
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0005CA43 File Offset: 0x0005AC43
		public static LocalizedString ClientCulture_0x1407
		{
			get
			{
				return new LocalizedString("ClientCulture_0x1407", "ExE4EC57", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0005CA64 File Offset: 0x0005AC64
		public static LocalizedString FailedToReadUserConfig(string user)
		{
			return new LocalizedString("FailedToReadUserConfig", "ExBB9B9B", false, true, ServerStrings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0005CA94 File Offset: 0x0005AC94
		public static LocalizedString InvalidDueDate1(string dueDate, string referenceTime)
		{
			return new LocalizedString("InvalidDueDate1", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				dueDate,
				referenceTime
			});
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0005CAC8 File Offset: 0x0005ACC8
		public static LocalizedString ExUnsupportedCodepage(int codepage)
		{
			return new LocalizedString("ExUnsupportedCodepage", "Ex3D3B19", false, true, ServerStrings.ResourceManager, new object[]
			{
				codepage
			});
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0005CAFC File Offset: 0x0005ACFC
		public static LocalizedString MapiCannotSaveChanges
		{
			get
			{
				return new LocalizedString("MapiCannotSaveChanges", "ExF1AEC3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0005CB1A File Offset: 0x0005AD1A
		public static LocalizedString RejectedSuggestionPersonIdSameAsPersonId
		{
			get
			{
				return new LocalizedString("RejectedSuggestionPersonIdSameAsPersonId", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0005CB38 File Offset: 0x0005AD38
		public static LocalizedString ErrorInvalidPhoneNumberFormat
		{
			get
			{
				return new LocalizedString("ErrorInvalidPhoneNumberFormat", "Ex4ACC2A", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0005CB56 File Offset: 0x0005AD56
		public static LocalizedString MigrationStateCorrupted
		{
			get
			{
				return new LocalizedString("MigrationStateCorrupted", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0005CB74 File Offset: 0x0005AD74
		public static LocalizedString ProvisioningRequestCsvContainsNeitherPasswordNorFederatedIdentity
		{
			get
			{
				return new LocalizedString("ProvisioningRequestCsvContainsNeitherPasswordNorFederatedIdentity", "Ex2732BB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0005CB92 File Offset: 0x0005AD92
		public static LocalizedString SecurityPrincipalAlreadyDefined
		{
			get
			{
				return new LocalizedString("SecurityPrincipalAlreadyDefined", "Ex94DA23", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0005CBB0 File Offset: 0x0005ADB0
		public static LocalizedString EmailFormatNotSupported(object addressType, string routingType)
		{
			return new LocalizedString("EmailFormatNotSupported", "Ex04CF93", false, true, ServerStrings.ResourceManager, new object[]
			{
				addressType,
				routingType
			});
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0005CBE4 File Offset: 0x0005ADE4
		public static LocalizedString ExInvalidTypeGroupCombination(object type, object group)
		{
			return new LocalizedString("ExInvalidTypeGroupCombination", "ExFDC860", false, true, ServerStrings.ResourceManager, new object[]
			{
				type,
				group
			});
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0005CC17 File Offset: 0x0005AE17
		public static LocalizedString KqlParseException
		{
			get
			{
				return new LocalizedString("KqlParseException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0005CC38 File Offset: 0x0005AE38
		public static LocalizedString MigrationBatchErrorString(int rowIndex, string emailAddress, string localizedMessage)
		{
			return new LocalizedString("MigrationBatchErrorString", "Ex817ADC", false, true, ServerStrings.ResourceManager, new object[]
			{
				rowIndex,
				emailAddress,
				localizedMessage
			});
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0005CC74 File Offset: 0x0005AE74
		public static LocalizedString ExEventNotFound
		{
			get
			{
				return new LocalizedString("ExEventNotFound", "ExED913E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0005CC92 File Offset: 0x0005AE92
		public static LocalizedString ThreeDays
		{
			get
			{
				return new LocalizedString("ThreeDays", "Ex4D4EB8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0005CCB0 File Offset: 0x0005AEB0
		public static LocalizedString ClosingTagExpected(string found)
		{
			return new LocalizedString("ClosingTagExpected", "Ex494BA1", false, true, ServerStrings.ResourceManager, new object[]
			{
				found
			});
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0005CCE0 File Offset: 0x0005AEE0
		public static LocalizedString AmServiceDownException(string serverName)
		{
			return new LocalizedString("AmServiceDownException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0005CD0F File Offset: 0x0005AF0F
		public static LocalizedString ExInvalidSortLength
		{
			get
			{
				return new LocalizedString("ExInvalidSortLength", "Ex0FA70B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0005CD30 File Offset: 0x0005AF30
		public static LocalizedString ExInvalidAttendeeType(object attendeeType)
		{
			return new LocalizedString("ExInvalidAttendeeType", "ExF16A21", false, true, ServerStrings.ResourceManager, new object[]
			{
				attendeeType
			});
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0005CD60 File Offset: 0x0005AF60
		public static LocalizedString AmDatabaseMasterIsInvalid(string dbName)
		{
			return new LocalizedString("AmDatabaseMasterIsInvalid", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0005CD8F File Offset: 0x0005AF8F
		public static LocalizedString MapiCannotGetPerUserLongTermIds
		{
			get
			{
				return new LocalizedString("MapiCannotGetPerUserLongTermIds", "ExBBF4BC", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0005CDAD File Offset: 0x0005AFAD
		public static LocalizedString ExFolderWithoutMapiProp
		{
			get
			{
				return new LocalizedString("ExFolderWithoutMapiProp", "Ex618E8F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0005CDCB File Offset: 0x0005AFCB
		public static LocalizedString ExChangeKeyTooLong
		{
			get
			{
				return new LocalizedString("ExChangeKeyTooLong", "Ex0E36B5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0005CDEC File Offset: 0x0005AFEC
		public static LocalizedString MigrationGroupMembersAttachmentCorrupted(string groupSmtpAddress)
		{
			return new LocalizedString("MigrationGroupMembersAttachmentCorrupted", "Ex9A0446", false, true, ServerStrings.ResourceManager, new object[]
			{
				groupSmtpAddress
			});
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0005CE1B File Offset: 0x0005B01B
		public static LocalizedString ExUnknownRestrictionType
		{
			get
			{
				return new LocalizedString("ExUnknownRestrictionType", "Ex024C18", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0005CE39 File Offset: 0x0005B039
		public static LocalizedString ExInvalidRowCount
		{
			get
			{
				return new LocalizedString("ExInvalidRowCount", "ExD3C2FA", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x0005CE57 File Offset: 0x0005B057
		public static LocalizedString UnsupportedExistRestriction
		{
			get
			{
				return new LocalizedString("UnsupportedExistRestriction", "Ex5AAE45", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0005CE78 File Offset: 0x0005B078
		public static LocalizedString UseMethodInstead(string method)
		{
			return new LocalizedString("UseMethodInstead", "Ex6527F6", false, true, ServerStrings.ResourceManager, new object[]
			{
				method
			});
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0005CEA7 File Offset: 0x0005B0A7
		public static LocalizedString AvailabilityOnly
		{
			get
			{
				return new LocalizedString("AvailabilityOnly", "Ex6FD924", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0005CEC5 File Offset: 0x0005B0C5
		public static LocalizedString MapiCannotExecuteWithInternalAccess
		{
			get
			{
				return new LocalizedString("MapiCannotExecuteWithInternalAccess", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0005CEE4 File Offset: 0x0005B0E4
		public static LocalizedString InvalidReminderOffset(int offset, int min, int max)
		{
			return new LocalizedString("InvalidReminderOffset", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				offset,
				min,
				max
			});
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0005CF2C File Offset: 0x0005B12C
		public static LocalizedString ExSearchFolderNoAssociatedItem(object Guid)
		{
			return new LocalizedString("ExSearchFolderNoAssociatedItem", "ExA72C7E", false, true, ServerStrings.ResourceManager, new object[]
			{
				Guid
			});
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0005CF5B File Offset: 0x0005B15B
		public static LocalizedString ExItemNoParentId
		{
			get
			{
				return new LocalizedString("ExItemNoParentId", "Ex6217D1", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0005CF79 File Offset: 0x0005B179
		public static LocalizedString MigrationTypePublicFolder
		{
			get
			{
				return new LocalizedString("MigrationTypePublicFolder", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0005CF97 File Offset: 0x0005B197
		public static LocalizedString MapiCannotGetPerUserGuid
		{
			get
			{
				return new LocalizedString("MapiCannotGetPerUserGuid", "Ex4E15E8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0005CFB8 File Offset: 0x0005B1B8
		public static LocalizedString DatabaseLocationNotAvailable(Guid databaseGuid)
		{
			return new LocalizedString("DatabaseLocationNotAvailable", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				databaseGuid
			});
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0005CFEC File Offset: 0x0005B1EC
		public static LocalizedString FederationNotEnabled
		{
			get
			{
				return new LocalizedString("FederationNotEnabled", "Ex8154EB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x0005D00A File Offset: 0x0005B20A
		public static LocalizedString RequestStateWaitingForFinalization
		{
			get
			{
				return new LocalizedString("RequestStateWaitingForFinalization", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0005D028 File Offset: 0x0005B228
		public static LocalizedString MigrationJobConnectionSettingsInvalid(string fieldName, string value)
		{
			return new LocalizedString("MigrationJobConnectionSettingsInvalid", "ExA76022", false, true, ServerStrings.ResourceManager, new object[]
			{
				fieldName,
				value
			});
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x0005D05B File Offset: 0x0005B25B
		public static LocalizedString RequestStateCompleted
		{
			get
			{
				return new LocalizedString("RequestStateCompleted", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0005D07C File Offset: 0x0005B27C
		public static LocalizedString MailboxSearchObjectNotExist(string id)
		{
			return new LocalizedString("MailboxSearchObjectNotExist", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0005D0AB File Offset: 0x0005B2AB
		public static LocalizedString TooManyCultures
		{
			get
			{
				return new LocalizedString("TooManyCultures", "Ex874281", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0005D0C9 File Offset: 0x0005B2C9
		public static LocalizedString MapiCannotSetCollapseState
		{
			get
			{
				return new LocalizedString("MapiCannotSetCollapseState", "Ex3FD53C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0005D0E7 File Offset: 0x0005B2E7
		public static LocalizedString IncompleteUserInformationToAccessGroupMailbox
		{
			get
			{
				return new LocalizedString("IncompleteUserInformationToAccessGroupMailbox", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x0005D105 File Offset: 0x0005B305
		public static LocalizedString ClientCulture_0x2001
		{
			get
			{
				return new LocalizedString("ClientCulture_0x2001", "ExDA8A26", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0005D124 File Offset: 0x0005B324
		public static LocalizedString ExInvalidValueTypeForCalculatedProperty(object value, Type property)
		{
			return new LocalizedString("ExInvalidValueTypeForCalculatedProperty", "Ex77AB27", false, true, ServerStrings.ResourceManager, new object[]
			{
				value,
				property
			});
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x0005D157 File Offset: 0x0005B357
		public static LocalizedString CannotImportMessageChange
		{
			get
			{
				return new LocalizedString("CannotImportMessageChange", "Ex5634B0", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0005D175 File Offset: 0x0005B375
		public static LocalizedString InvalidTimesInTimeSlot
		{
			get
			{
				return new LocalizedString("InvalidTimesInTimeSlot", "Ex7EB919", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0005D193 File Offset: 0x0005B393
		public static LocalizedString MigrationReportFinalizationFailure
		{
			get
			{
				return new LocalizedString("MigrationReportFinalizationFailure", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0005D1B1 File Offset: 0x0005B3B1
		public static LocalizedString StructuredQueryException
		{
			get
			{
				return new LocalizedString("StructuredQueryException", "Ex7CCE6A", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0005D1D0 File Offset: 0x0005B3D0
		public static LocalizedString PrincipalNotAllowedByPolicy(string principal)
		{
			return new LocalizedString("PrincipalNotAllowedByPolicy", "Ex52DBF9", false, true, ServerStrings.ResourceManager, new object[]
			{
				principal
			});
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0005D1FF File Offset: 0x0005B3FF
		public static LocalizedString ExUnknownResponseType
		{
			get
			{
				return new LocalizedString("ExUnknownResponseType", "ExE9BF33", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0005D21D File Offset: 0x0005B41D
		public static LocalizedString RequestStateCreated
		{
			get
			{
				return new LocalizedString("RequestStateCreated", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0005D23C File Offset: 0x0005B43C
		public static LocalizedString ExCorruptDataRecoverError(string folder)
		{
			return new LocalizedString("ExCorruptDataRecoverError", "ExEA04E5", false, true, ServerStrings.ResourceManager, new object[]
			{
				folder
			});
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x0005D26B File Offset: 0x0005B46B
		public static LocalizedString ExInvalidComparisonOperatorInComparisonFilter
		{
			get
			{
				return new LocalizedString("ExInvalidComparisonOperatorInComparisonFilter", "ExCDE221", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0005D289 File Offset: 0x0005B489
		public static LocalizedString MigrationFolderSettings
		{
			get
			{
				return new LocalizedString("MigrationFolderSettings", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x0005D2A7 File Offset: 0x0005B4A7
		public static LocalizedString ClientCulture_0x809
		{
			get
			{
				return new LocalizedString("ClientCulture_0x809", "Ex36E366", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0005D2C8 File Offset: 0x0005B4C8
		public static LocalizedString ActiveMonitoringUnknownGenericRpcCommand(int requestedServerVersion, int replyingServerVersion, int commandId)
		{
			return new LocalizedString("ActiveMonitoringUnknownGenericRpcCommand", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				requestedServerVersion,
				replyingServerVersion,
				commandId
			});
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0005D30E File Offset: 0x0005B50E
		public static LocalizedString ExUnsupportedSeekReference
		{
			get
			{
				return new LocalizedString("ExUnsupportedSeekReference", "Ex68EB89", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0005D32C File Offset: 0x0005B52C
		public static LocalizedString MigrationBatchStatusCompleted
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusCompleted", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0005D34C File Offset: 0x0005B54C
		public static LocalizedString ExTooManySubscriptionsOnPublicStore(string serverFqdn)
		{
			return new LocalizedString("ExTooManySubscriptionsOnPublicStore", "ExEF6549", false, true, ServerStrings.ResourceManager, new object[]
			{
				serverFqdn
			});
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0005D37B File Offset: 0x0005B57B
		public static LocalizedString MigrationTestMSAWarning
		{
			get
			{
				return new LocalizedString("MigrationTestMSAWarning", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0005D399 File Offset: 0x0005B599
		public static LocalizedString InvalidDateTimeRange
		{
			get
			{
				return new LocalizedString("InvalidDateTimeRange", "Ex2E5F2C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0005D3B8 File Offset: 0x0005B5B8
		public static LocalizedString ExCalculatedPropertyStreamAccessNotSupported(string property)
		{
			return new LocalizedString("ExCalculatedPropertyStreamAccessNotSupported", "Ex3A51CF", false, true, ServerStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0005D3E8 File Offset: 0x0005B5E8
		public static LocalizedString InvalidRmsUrl(ServiceType type, string response)
		{
			return new LocalizedString("InvalidRmsUrl", "Ex298347", false, true, ServerStrings.ResourceManager, new object[]
			{
				type,
				response
			});
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0005D420 File Offset: 0x0005B620
		public static LocalizedString AmRpcOperationNotImplemented(string operationHint, string serverName)
		{
			return new LocalizedString("AmRpcOperationNotImplemented", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				operationHint,
				serverName
			});
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0005D454 File Offset: 0x0005B654
		public static LocalizedString SharingFolderName(string folderName, string sharer)
		{
			return new LocalizedString("SharingFolderName", "Ex46F8A3", false, true, ServerStrings.ResourceManager, new object[]
			{
				folderName,
				sharer
			});
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0005D487 File Offset: 0x0005B687
		public static LocalizedString MapiCannotGetMapiTable
		{
			get
			{
				return new LocalizedString("MapiCannotGetMapiTable", "ExF264D9", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0005D4A5 File Offset: 0x0005B6A5
		public static LocalizedString MapiCannotCheckForNotifications
		{
			get
			{
				return new LocalizedString("MapiCannotCheckForNotifications", "ExD6EAF7", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0005D4C4 File Offset: 0x0005B6C4
		public static LocalizedString OperationNotAllowed(string operation, string type)
		{
			return new LocalizedString("OperationNotAllowed", "Ex314361", false, true, ServerStrings.ResourceManager, new object[]
			{
				operation,
				type
			});
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0005D4F7 File Offset: 0x0005B6F7
		public static LocalizedString CannotStamplocalFreeBusyId
		{
			get
			{
				return new LocalizedString("CannotStamplocalFreeBusyId", "Ex72C521", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0005D518 File Offset: 0x0005B718
		public static LocalizedString SharingPolicyNotFound(string policy)
		{
			return new LocalizedString("SharingPolicyNotFound", "Ex32A5DA", false, true, ServerStrings.ResourceManager, new object[]
			{
				policy
			});
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0005D547 File Offset: 0x0005B747
		public static LocalizedString ClientCulture_0x100A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x100A", "ExCD7310", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0005D565 File Offset: 0x0005B765
		public static LocalizedString MigrationBatchStatusSynced
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusSynced", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0005D583 File Offset: 0x0005B783
		public static LocalizedString ExchangePrincipalFromMailboxDataError
		{
			get
			{
				return new LocalizedString("ExchangePrincipalFromMailboxDataError", "ExEF79A4", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0005D5A1 File Offset: 0x0005B7A1
		public static LocalizedString MigrationUserStatusIncrementalFailed
		{
			get
			{
				return new LocalizedString("MigrationUserStatusIncrementalFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0005D5BF File Offset: 0x0005B7BF
		public static LocalizedString InvalidXml
		{
			get
			{
				return new LocalizedString("InvalidXml", "Ex6F8448", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0005D5E0 File Offset: 0x0005B7E0
		public static LocalizedString InvalidExternalSharingInitiatorException(string initiator, string sender)
		{
			return new LocalizedString("InvalidExternalSharingInitiatorException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				initiator,
				sender
			});
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x0005D613 File Offset: 0x0005B813
		public static LocalizedString ExEntryIdToLong
		{
			get
			{
				return new LocalizedString("ExEntryIdToLong", "Ex2718EE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0005D634 File Offset: 0x0005B834
		public static LocalizedString ExSortGroupNotSupportedForProperty(string propertyName)
		{
			return new LocalizedString("ExSortGroupNotSupportedForProperty", "ExEC90F1", false, true, ServerStrings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0005D663 File Offset: 0x0005B863
		public static LocalizedString ClientCulture_0x420
		{
			get
			{
				return new LocalizedString("ClientCulture_0x420", "ExCD0590", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0005D684 File Offset: 0x0005B884
		public static LocalizedString ConversationContainsDuplicateMids(string legacyDN, long mid)
		{
			return new LocalizedString("ConversationContainsDuplicateMids", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				legacyDN,
				mid
			});
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0005D6BC File Offset: 0x0005B8BC
		public static LocalizedString PrincipalFromDifferentSite
		{
			get
			{
				return new LocalizedString("PrincipalFromDifferentSite", "Ex669A85", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0005D6DA File Offset: 0x0005B8DA
		public static LocalizedString ErrorSavingRules
		{
			get
			{
				return new LocalizedString("ErrorSavingRules", "Ex2BFE42", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0005D6F8 File Offset: 0x0005B8F8
		public static LocalizedString PublishedFolderAccessDeniedException
		{
			get
			{
				return new LocalizedString("PublishedFolderAccessDeniedException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0005D716 File Offset: 0x0005B916
		public static LocalizedString PublicFoldersNotEnabledForEnterprise
		{
			get
			{
				return new LocalizedString("PublicFoldersNotEnabledForEnterprise", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0005D734 File Offset: 0x0005B934
		public static LocalizedString InboxRuleMessageTypeApprovalRequest
		{
			get
			{
				return new LocalizedString("InboxRuleMessageTypeApprovalRequest", "ExB4DDBD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0005D752 File Offset: 0x0005B952
		public static LocalizedString NonUniqueRecipientError
		{
			get
			{
				return new LocalizedString("NonUniqueRecipientError", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0005D770 File Offset: 0x0005B970
		public static LocalizedString ParticipantPropertyTooBig(string property)
		{
			return new LocalizedString("ParticipantPropertyTooBig", "Ex7AD3E4", false, true, ServerStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0005D7A0 File Offset: 0x0005B9A0
		public static LocalizedString CopyToDumpsterFailure(string error)
		{
			return new LocalizedString("CopyToDumpsterFailure", "Ex0AAB56", false, true, ServerStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0005D7CF File Offset: 0x0005B9CF
		public static LocalizedString ExSystemFolderAccessDenied
		{
			get
			{
				return new LocalizedString("ExSystemFolderAccessDenied", "ExB7E350", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0005D7F0 File Offset: 0x0005B9F0
		public static LocalizedString ExNotSupportedConfigurationSearchFlags(string flags)
		{
			return new LocalizedString("ExNotSupportedConfigurationSearchFlags", "Ex5EBE6D", false, true, ServerStrings.ResourceManager, new object[]
			{
				flags
			});
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0005D81F File Offset: 0x0005BA1F
		public static LocalizedString MapiCannotRemoveNotification
		{
			get
			{
				return new LocalizedString("MapiCannotRemoveNotification", "Ex8848E7", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0005D840 File Offset: 0x0005BA40
		public static LocalizedString InvalidParticipant(string explanation, object status)
		{
			return new LocalizedString("InvalidParticipant", "ExA8F2DB", false, true, ServerStrings.ResourceManager, new object[]
			{
				explanation,
				status
			});
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0005D873 File Offset: 0x0005BA73
		public static LocalizedString ClientCulture_0x180A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x180A", "ExB8A2FF", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0005D894 File Offset: 0x0005BA94
		public static LocalizedString ExInvalidEventWatermarkBadOrigin(Guid watermarkOrigin, Guid eventSourceOrigin)
		{
			return new LocalizedString("ExInvalidEventWatermarkBadOrigin", "ExF64E65", false, true, ServerStrings.ResourceManager, new object[]
			{
				watermarkOrigin,
				eventSourceOrigin
			});
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0005D8D1 File Offset: 0x0005BAD1
		public static LocalizedString ExCommentFilterPropertiesNotSupported
		{
			get
			{
				return new LocalizedString("ExCommentFilterPropertiesNotSupported", "Ex34268D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0005D8F0 File Offset: 0x0005BAF0
		public static LocalizedString ExPropertyNotValidOnOccurrence(string property)
		{
			return new LocalizedString("ExPropertyNotValidOnOccurrence", "ExC92BEB", false, true, ServerStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0005D920 File Offset: 0x0005BB20
		public static LocalizedString DefaultFolderAccessDenied(string folder)
		{
			return new LocalizedString("DefaultFolderAccessDenied", "Ex68AB5D", false, true, ServerStrings.ResourceManager, new object[]
			{
				folder
			});
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0005D94F File Offset: 0x0005BB4F
		public static LocalizedString ExDictionaryDataCorruptedNullKey
		{
			get
			{
				return new LocalizedString("ExDictionaryDataCorruptedNullKey", "Ex6CD17C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0005D970 File Offset: 0x0005BB70
		public static LocalizedString COWMailboxInfoCacheTimeout(double seconds, int count)
		{
			return new LocalizedString("COWMailboxInfoCacheTimeout", "Ex48E4DC", false, true, ServerStrings.ResourceManager, new object[]
			{
				seconds,
				count
			});
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0005D9AD File Offset: 0x0005BBAD
		public static LocalizedString MigrationBatchStatusStarting
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusStarting", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0005D9CC File Offset: 0x0005BBCC
		public static LocalizedString ExFinalEventFound(string finalEvent)
		{
			return new LocalizedString("ExFinalEventFound", "Ex6CAAE4", false, true, ServerStrings.ResourceManager, new object[]
			{
				finalEvent
			});
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0005D9FB File Offset: 0x0005BBFB
		public static LocalizedString ClientCulture_0x300A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x300A", "ExFAADEB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0005DA1C File Offset: 0x0005BC1C
		public static LocalizedString FailedToFindLicenseUri(string tenantId)
		{
			return new LocalizedString("FailedToFindLicenseUri", "Ex4FC2FB", false, true, ServerStrings.ResourceManager, new object[]
			{
				tenantId
			});
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0005DA4B File Offset: 0x0005BC4B
		public static LocalizedString ExBadValueForTypeCode0
		{
			get
			{
				return new LocalizedString("ExBadValueForTypeCode0", "Ex8DDAFC", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0005DA69 File Offset: 0x0005BC69
		public static LocalizedString ErrorTimeProposalInvalidOnRecurringMaster
		{
			get
			{
				return new LocalizedString("ErrorTimeProposalInvalidOnRecurringMaster", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0005DA88 File Offset: 0x0005BC88
		public static LocalizedString BadEnumValue(Type enumType)
		{
			return new LocalizedString("BadEnumValue", "ExFCB7A2", false, true, ServerStrings.ResourceManager, new object[]
			{
				enumType
			});
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0005DAB7 File Offset: 0x0005BCB7
		public static LocalizedString SearchStateDeletionInProgress
		{
			get
			{
				return new LocalizedString("SearchStateDeletionInProgress", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0005DAD5 File Offset: 0x0005BCD5
		public static LocalizedString ExRuleIdInvalid
		{
			get
			{
				return new LocalizedString("ExRuleIdInvalid", "ExE0354D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0005DAF4 File Offset: 0x0005BCF4
		public static LocalizedString NotStreamablePropertyValue(Type type)
		{
			return new LocalizedString("NotStreamablePropertyValue", "Ex46AB03", false, true, ServerStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0005DB24 File Offset: 0x0005BD24
		public static LocalizedString ObjectMustBeOfType(string type)
		{
			return new LocalizedString("ObjectMustBeOfType", "Ex6C734F", false, true, ServerStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0005DB53 File Offset: 0x0005BD53
		public static LocalizedString MapiCannotCollapseRow
		{
			get
			{
				return new LocalizedString("MapiCannotCollapseRow", "Ex0B1A60", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0005DB71 File Offset: 0x0005BD71
		public static LocalizedString SharingUnableToGenerateEncryptedSharedFolderData
		{
			get
			{
				return new LocalizedString("SharingUnableToGenerateEncryptedSharedFolderData", "ExEA5D23", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x0005DB8F File Offset: 0x0005BD8F
		public static LocalizedString ExConnectionNotCached
		{
			get
			{
				return new LocalizedString("ExConnectionNotCached", "ExC60AD3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0005DBB0 File Offset: 0x0005BDB0
		public static LocalizedString ErrorCorruptedData(string name)
		{
			return new LocalizedString("ErrorCorruptedData", "Ex55B78A", false, true, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0005DBDF File Offset: 0x0005BDDF
		public static LocalizedString CVSPopulationTimedout
		{
			get
			{
				return new LocalizedString("CVSPopulationTimedout", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0005DBFD File Offset: 0x0005BDFD
		public static LocalizedString BadDateFormatInChangeDate
		{
			get
			{
				return new LocalizedString("BadDateFormatInChangeDate", "Ex73BFD1", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0005DC1B File Offset: 0x0005BE1B
		public static LocalizedString MigrationBatchStatusCompletedWithErrors
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusCompletedWithErrors", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0005DC3C File Offset: 0x0005BE3C
		public static LocalizedString QueryUsageRightsPrelicenseResponseHasFailure(string uri, RightsManagementFailureCode failure)
		{
			return new LocalizedString("QueryUsageRightsPrelicenseResponseHasFailure", "Ex49BEFC", false, true, ServerStrings.ResourceManager, new object[]
			{
				uri,
				failure
			});
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0005DC74 File Offset: 0x0005BE74
		public static LocalizedString NotReadSubjectPrefix
		{
			get
			{
				return new LocalizedString("NotReadSubjectPrefix", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0005DC92 File Offset: 0x0005BE92
		public static LocalizedString MapiCannotFinishSubmit
		{
			get
			{
				return new LocalizedString("MapiCannotFinishSubmit", "ExCBFB85", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0005DCB0 File Offset: 0x0005BEB0
		public static LocalizedString ClientCulture_0xC01
		{
			get
			{
				return new LocalizedString("ClientCulture_0xC01", "ExEED192", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0005DCCE File Offset: 0x0005BECE
		public static LocalizedString ExItemNotFoundInClientManifest
		{
			get
			{
				return new LocalizedString("ExItemNotFoundInClientManifest", "Ex033D29", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0005DCEC File Offset: 0x0005BEEC
		public static LocalizedString ErrorNoStoreObjectId
		{
			get
			{
				return new LocalizedString("ErrorNoStoreObjectId", "ExFB36A8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0005DD0A File Offset: 0x0005BF0A
		public static LocalizedString CalendarItemCorrelationFailed
		{
			get
			{
				return new LocalizedString("CalendarItemCorrelationFailed", "ExB2C2AD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0005DD28 File Offset: 0x0005BF28
		public static LocalizedString ExInvalidOccurrenceId
		{
			get
			{
				return new LocalizedString("ExInvalidOccurrenceId", "Ex765023", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0005DD46 File Offset: 0x0005BF46
		public static LocalizedString DateRangeOneWeek
		{
			get
			{
				return new LocalizedString("DateRangeOneWeek", "Ex10D3FA", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0005DD64 File Offset: 0x0005BF64
		public static LocalizedString EnforceRulesQuota
		{
			get
			{
				return new LocalizedString("EnforceRulesQuota", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0005DD84 File Offset: 0x0005BF84
		public static LocalizedString MigrationMailboxNotFoundOnServerError(string mailboxName, string expected, string found)
		{
			return new LocalizedString("MigrationMailboxNotFoundOnServerError", "ExD46017", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailboxName,
				expected,
				found
			});
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0005DDBB File Offset: 0x0005BFBB
		public static LocalizedString ExInvalidMonth
		{
			get
			{
				return new LocalizedString("ExInvalidMonth", "ExEB368D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x0005DDD9 File Offset: 0x0005BFD9
		public static LocalizedString MigrationUserStatusCompletionSynced
		{
			get
			{
				return new LocalizedString("MigrationUserStatusCompletionSynced", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0005DDF7 File Offset: 0x0005BFF7
		public static LocalizedString FirstFullWeek
		{
			get
			{
				return new LocalizedString("FirstFullWeek", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0005DE18 File Offset: 0x0005C018
		public static LocalizedString StreamPropertyNotFound(string property)
		{
			return new LocalizedString("StreamPropertyNotFound", "Ex78704B", false, true, ServerStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0005DE48 File Offset: 0x0005C048
		public static LocalizedString idOscContactSourcesForContactParseError(string errMessage)
		{
			return new LocalizedString("idOscContactSourcesForContactParseError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				errMessage
			});
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0005DE77 File Offset: 0x0005C077
		public static LocalizedString MigrationFeatureEndpoints
		{
			get
			{
				return new LocalizedString("MigrationFeatureEndpoints", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0005DE95 File Offset: 0x0005C095
		public static LocalizedString ExNoSearchHasBeenInitiated
		{
			get
			{
				return new LocalizedString("ExNoSearchHasBeenInitiated", "Ex69E186", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0005DEB3 File Offset: 0x0005C0B3
		public static LocalizedString MigrationUserStatusIncrementalSyncing
		{
			get
			{
				return new LocalizedString("MigrationUserStatusIncrementalSyncing", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0005DED4 File Offset: 0x0005C0D4
		public static LocalizedString ExIncorrectOriginalTimeInExtendedExceptionInfo(object originalTime, object invalidTime)
		{
			return new LocalizedString("ExIncorrectOriginalTimeInExtendedExceptionInfo", "Ex5438A5", false, true, ServerStrings.ResourceManager, new object[]
			{
				originalTime,
				invalidTime
			});
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0005DF07 File Offset: 0x0005C107
		public static LocalizedString PublicFolderMailboxesCannotBeCreatedDuringMigration
		{
			get
			{
				return new LocalizedString("PublicFolderMailboxesCannotBeCreatedDuringMigration", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0005DF28 File Offset: 0x0005C128
		public static LocalizedString DeleteFromDumpsterFailure(string mailbox)
		{
			return new LocalizedString("DeleteFromDumpsterFailure", "Ex8F6768", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0005DF58 File Offset: 0x0005C158
		public static LocalizedString DetailLevelNotAllowedByPolicy(string detailLevel)
		{
			return new LocalizedString("DetailLevelNotAllowedByPolicy", "Ex3E3DE0", false, true, ServerStrings.ResourceManager, new object[]
			{
				detailLevel
			});
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x0005DF87 File Offset: 0x0005C187
		public static LocalizedString MapiCannotCreateFilter
		{
			get
			{
				return new LocalizedString("MapiCannotCreateFilter", "ExD9C071", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0005DFA5 File Offset: 0x0005C1A5
		public static LocalizedString MapiCannotNotifyMessageNewMail
		{
			get
			{
				return new LocalizedString("MapiCannotNotifyMessageNewMail", "ExCA8F9B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0005DFC4 File Offset: 0x0005C1C4
		public static LocalizedString ErrorCalendarReminderNegative(string value)
		{
			return new LocalizedString("ErrorCalendarReminderNegative", "ExAA98E7", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0005DFF4 File Offset: 0x0005C1F4
		public static LocalizedString GroupMailboxAccessSidConstructionFailed(Guid groupMailboxGuid, string userType)
		{
			return new LocalizedString("GroupMailboxAccessSidConstructionFailed", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				groupMailboxGuid,
				userType
			});
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0005E02C File Offset: 0x0005C22C
		public static LocalizedString MigrationUserStatusSyncing
		{
			get
			{
				return new LocalizedString("MigrationUserStatusSyncing", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0005E04A File Offset: 0x0005C24A
		public static LocalizedString MigrationBatchFlagForceNewMigration
		{
			get
			{
				return new LocalizedString("MigrationBatchFlagForceNewMigration", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0005E068 File Offset: 0x0005C268
		public static LocalizedString CannotGetFinalStateSynchronizerProviderBase
		{
			get
			{
				return new LocalizedString("CannotGetFinalStateSynchronizerProviderBase", "Ex283160", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0005E086 File Offset: 0x0005C286
		public static LocalizedString ServerLocatorClientWCFCallCommunicationError
		{
			get
			{
				return new LocalizedString("ServerLocatorClientWCFCallCommunicationError", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0005E0A4 File Offset: 0x0005C2A4
		public static LocalizedString OperationTimedOut(string timeout)
		{
			return new LocalizedString("OperationTimedOut", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				timeout
			});
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0005E0D4 File Offset: 0x0005C2D4
		public static LocalizedString MailboxVersionTooLow(string mailbox, string expectedVersion, string actualVersion)
		{
			return new LocalizedString("MailboxVersionTooLow", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				mailbox,
				expectedVersion,
				actualVersion
			});
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0005E10B File Offset: 0x0005C30B
		public static LocalizedString ExValueCannotBeNull
		{
			get
			{
				return new LocalizedString("ExValueCannotBeNull", "ExEDF149", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0005E129 File Offset: 0x0005C329
		public static LocalizedString ClientCulture_0x3009
		{
			get
			{
				return new LocalizedString("ClientCulture_0x3009", "Ex626C90", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0005E148 File Offset: 0x0005C348
		public static LocalizedString ExCannotSaveSyncStateFolder(string folderName, string saveResult)
		{
			return new LocalizedString("ExCannotSaveSyncStateFolder", "Ex3C7A8F", false, true, ServerStrings.ResourceManager, new object[]
			{
				folderName,
				saveResult
			});
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0005E17B File Offset: 0x0005C37B
		public static LocalizedString MigrationTypeBulkProvisioning
		{
			get
			{
				return new LocalizedString("MigrationTypeBulkProvisioning", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x0005E199 File Offset: 0x0005C399
		public static LocalizedString ErrorFolderIsMailEnabled
		{
			get
			{
				return new LocalizedString("ErrorFolderIsMailEnabled", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x0005E1B7 File Offset: 0x0005C3B7
		public static LocalizedString ExCantAccessOccurrenceFromNewItem
		{
			get
			{
				return new LocalizedString("ExCantAccessOccurrenceFromNewItem", "Ex4954C0", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0005E1D8 File Offset: 0x0005C3D8
		public static LocalizedString ExNotSupportedNotificationType(uint notificationType)
		{
			return new LocalizedString("ExNotSupportedNotificationType", "Ex5A45EF", false, true, ServerStrings.ResourceManager, new object[]
			{
				notificationType
			});
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0005E20C File Offset: 0x0005C40C
		public static LocalizedString ConversionInvalidItemType(string type)
		{
			return new LocalizedString("ConversionInvalidItemType", "ExBF3BED", false, true, ServerStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x0005E23B File Offset: 0x0005C43B
		public static LocalizedString ConversionCorruptContent
		{
			get
			{
				return new LocalizedString("ConversionCorruptContent", "ExADAC25", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x0005E259 File Offset: 0x0005C459
		public static LocalizedString AutoDFailedToGetToken
		{
			get
			{
				return new LocalizedString("AutoDFailedToGetToken", "Ex62564D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x0005E277 File Offset: 0x0005C477
		public static LocalizedString ExCorruptPropertyTag
		{
			get
			{
				return new LocalizedString("ExCorruptPropertyTag", "ExC0FBE1", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x0005E295 File Offset: 0x0005C495
		public static LocalizedString InvalidTimeSlot
		{
			get
			{
				return new LocalizedString("InvalidTimeSlot", "ExD7205E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x0005E2B3 File Offset: 0x0005C4B3
		public static LocalizedString ExCannotOpenMultipleCorrelatedItems
		{
			get
			{
				return new LocalizedString("ExCannotOpenMultipleCorrelatedItems", "ExD91EEA", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0005E2D4 File Offset: 0x0005C4D4
		public static LocalizedString CannotFindRequestIndexEntry(Guid requestGuid)
		{
			return new LocalizedString("CannotFindRequestIndexEntry", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				requestGuid
			});
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0005E308 File Offset: 0x0005C508
		public static LocalizedString ErrorLanguageIsNull
		{
			get
			{
				return new LocalizedString("ErrorLanguageIsNull", "Ex7E96B9", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0005E328 File Offset: 0x0005C528
		public static LocalizedString ExBodyFormatConversionNotSupported(string format)
		{
			return new LocalizedString("ExBodyFormatConversionNotSupported", "Ex6F3C48", false, true, ServerStrings.ResourceManager, new object[]
			{
				format
			});
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0005E358 File Offset: 0x0005C558
		public static LocalizedString ExConfigExisted(string configName)
		{
			return new LocalizedString("ExConfigExisted", "ExF3EA79", false, true, ServerStrings.ResourceManager, new object[]
			{
				configName
			});
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x0005E387 File Offset: 0x0005C587
		public static LocalizedString ExInvalidAcrBaseProfiles
		{
			get
			{
				return new LocalizedString("ExInvalidAcrBaseProfiles", "ExBDE4F3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0005E3A5 File Offset: 0x0005C5A5
		public static LocalizedString ExMustSaveFolderToApplySearch
		{
			get
			{
				return new LocalizedString("ExMustSaveFolderToApplySearch", "ExD00A99", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0005E3C3 File Offset: 0x0005C5C3
		public static LocalizedString ExReadTopologyTimeout
		{
			get
			{
				return new LocalizedString("ExReadTopologyTimeout", "ExA11951", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0005E3E1 File Offset: 0x0005C5E1
		public static LocalizedString ExUnknownRecurrenceBlobType
		{
			get
			{
				return new LocalizedString("ExUnknownRecurrenceBlobType", "ExB6B567", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0005E3FF File Offset: 0x0005C5FF
		public static LocalizedString ClientCulture_0x419
		{
			get
			{
				return new LocalizedString("ClientCulture_0x419", "ExA616B9", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0005E420 File Offset: 0x0005C620
		public static LocalizedString CannotGetSynchronizeBuffers(Type mapiManifestType)
		{
			return new LocalizedString("CannotGetSynchronizeBuffers", "Ex288C19", false, true, ServerStrings.ResourceManager, new object[]
			{
				mapiManifestType
			});
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0005E44F File Offset: 0x0005C64F
		public static LocalizedString SpellCheckerHebrew
		{
			get
			{
				return new LocalizedString("SpellCheckerHebrew", "ExA0672B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0005E470 File Offset: 0x0005C670
		public static LocalizedString TenantLicensesDistributionPointMismatch(string tenantId, Uri serviceLocation, Uri publishLocation)
		{
			return new LocalizedString("TenantLicensesDistributionPointMismatch", "Ex549A2F", false, true, ServerStrings.ResourceManager, new object[]
			{
				tenantId,
				serviceLocation,
				publishLocation
			});
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x0005E4A7 File Offset: 0x0005C6A7
		public static LocalizedString ClientCulture_0x1001
		{
			get
			{
				return new LocalizedString("ClientCulture_0x1001", "ExA7B6EC", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0005E4C5 File Offset: 0x0005C6C5
		public static LocalizedString InvalidAttachmentId
		{
			get
			{
				return new LocalizedString("InvalidAttachmentId", "ExF4240C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0005E4E3 File Offset: 0x0005C6E3
		public static LocalizedString ClientCulture_0x43F
		{
			get
			{
				return new LocalizedString("ClientCulture_0x43F", "Ex5A6B54", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0005E501 File Offset: 0x0005C701
		public static LocalizedString ExInvalidFolderId
		{
			get
			{
				return new LocalizedString("ExInvalidFolderId", "Ex507E00", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0005E520 File Offset: 0x0005C720
		public static LocalizedString ExUrlNotFound(Uri url)
		{
			return new LocalizedString("ExUrlNotFound", "Ex835A9C", false, true, ServerStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0005E54F File Offset: 0x0005C74F
		public static LocalizedString AmDbMountNotAllowedDueToRegistryConfigurationException
		{
			get
			{
				return new LocalizedString("AmDbMountNotAllowedDueToRegistryConfigurationException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x0005E56D File Offset: 0x0005C76D
		public static LocalizedString CannotSaveReadOnlyAttachment
		{
			get
			{
				return new LocalizedString("CannotSaveReadOnlyAttachment", "Ex55ACB2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0005E58B File Offset: 0x0005C78B
		public static LocalizedString InvalidTnef
		{
			get
			{
				return new LocalizedString("InvalidTnef", "ExED4FFA", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0005E5A9 File Offset: 0x0005C7A9
		public static LocalizedString MigrationUserStatusIncrementalSynced
		{
			get
			{
				return new LocalizedString("MigrationUserStatusIncrementalSynced", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0005E5C8 File Offset: 0x0005C7C8
		public static LocalizedString InvalidICalElement(string name)
		{
			return new LocalizedString("InvalidICalElement", "Ex778037", false, true, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0005E5F7 File Offset: 0x0005C7F7
		public static LocalizedString ExAdminAuditLogsDeleteDenied
		{
			get
			{
				return new LocalizedString("ExAdminAuditLogsDeleteDenied", "ExC3F166", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0005E618 File Offset: 0x0005C818
		public static LocalizedString FailedToRpcAcquireUseLicenses(string orgId, string status, string server)
		{
			return new LocalizedString("FailedToRpcAcquireUseLicenses", "ExCAAB7A", false, true, ServerStrings.ResourceManager, new object[]
			{
				orgId,
				status,
				server
			});
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0005E650 File Offset: 0x0005C850
		public static LocalizedString ExUnknownPattern(object pattern)
		{
			return new LocalizedString("ExUnknownPattern", "Ex2AEAB5", false, true, ServerStrings.ResourceManager, new object[]
			{
				pattern
			});
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0005E67F File Offset: 0x0005C87F
		public static LocalizedString ConversionInvalidMessageCodepageCharset
		{
			get
			{
				return new LocalizedString("ConversionInvalidMessageCodepageCharset", "ExCFF2A5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0005E6A0 File Offset: 0x0005C8A0
		public static LocalizedString ExFilterNotSupportedForProperty(string filterType, string propertyName)
		{
			return new LocalizedString("ExFilterNotSupportedForProperty", "Ex876A64", false, true, ServerStrings.ResourceManager, new object[]
			{
				filterType,
				propertyName
			});
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0005E6D4 File Offset: 0x0005C8D4
		public static LocalizedString SearchObjectIsInvalid(string id)
		{
			return new LocalizedString("SearchObjectIsInvalid", "ExA4E124", false, true, ServerStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0005E703 File Offset: 0x0005C903
		public static LocalizedString ClientCulture_0x40C
		{
			get
			{
				return new LocalizedString("ClientCulture_0x40C", "ExF37899", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0005E721 File Offset: 0x0005C921
		public static LocalizedString DumpsterStatusShutdownException
		{
			get
			{
				return new LocalizedString("DumpsterStatusShutdownException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0005E740 File Offset: 0x0005C940
		public static LocalizedString FailedToDownloadServerLicensingMExData(Uri url)
		{
			return new LocalizedString("FailedToDownloadServerLicensingMExData", "Ex1C7BFF", false, true, ServerStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0005E770 File Offset: 0x0005C970
		public static LocalizedString ActiveMonitoringOperationFailedException(string errMessage)
		{
			return new LocalizedString("ActiveMonitoringOperationFailedException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				errMessage
			});
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x0005E79F File Offset: 0x0005C99F
		public static LocalizedString CannotDeleteRootFolder
		{
			get
			{
				return new LocalizedString("CannotDeleteRootFolder", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0005E7BD File Offset: 0x0005C9BD
		public static LocalizedString MapiCannotGetEffectiveRights
		{
			get
			{
				return new LocalizedString("MapiCannotGetEffectiveRights", "Ex930822", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0005E7DB File Offset: 0x0005C9DB
		public static LocalizedString InvalidMechanismToAccessGroupMailbox
		{
			get
			{
				return new LocalizedString("InvalidMechanismToAccessGroupMailbox", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0005E7FC File Offset: 0x0005C9FC
		public static LocalizedString UnsupportedClientOperation(string client)
		{
			return new LocalizedString("UnsupportedClientOperation", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				client
			});
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0005E82C File Offset: 0x0005CA2C
		public static LocalizedString ExternalLicensingDisabledForEnterprise(Uri uri)
		{
			return new LocalizedString("ExternalLicensingDisabledForEnterprise", "Ex3D3E94", false, true, ServerStrings.ResourceManager, new object[]
			{
				uri
			});
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0005E85B File Offset: 0x0005CA5B
		public static LocalizedString MapiCannotSavePermissions
		{
			get
			{
				return new LocalizedString("MapiCannotSavePermissions", "Ex75B8FE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0005E879 File Offset: 0x0005CA79
		public static LocalizedString ClientCulture_0x1004
		{
			get
			{
				return new LocalizedString("ClientCulture_0x1004", "ExF84280", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0005E897 File Offset: 0x0005CA97
		public static LocalizedString MigrationBatchStatusCreated
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusCreated", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0005E8B8 File Offset: 0x0005CAB8
		public static LocalizedString JunkEmailBlockedListXsoTooBigException(string value)
		{
			return new LocalizedString("JunkEmailBlockedListXsoTooBigException", "ExD65092", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0005E8E8 File Offset: 0x0005CAE8
		public static LocalizedString AmDatabaseCopyNotFoundException(string dbName, string serverName)
		{
			return new LocalizedString("AmDatabaseCopyNotFoundException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				dbName,
				serverName
			});
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0005E91B File Offset: 0x0005CB1B
		public static LocalizedString NotAllowedExternalSharingByPolicy
		{
			get
			{
				return new LocalizedString("NotAllowedExternalSharingByPolicy", "Ex36DB97", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0005E939 File Offset: 0x0005CB39
		public static LocalizedString InboxRuleMessageTypeReadReceipt
		{
			get
			{
				return new LocalizedString("InboxRuleMessageTypeReadReceipt", "ExDEB1AE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0005E957 File Offset: 0x0005CB57
		public static LocalizedString StoreOperationFailed
		{
			get
			{
				return new LocalizedString("StoreOperationFailed", "Ex544968", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0005E975 File Offset: 0x0005CB75
		public static LocalizedString ErrorExTimeZoneValueNoGmtMatch
		{
			get
			{
				return new LocalizedString("ErrorExTimeZoneValueNoGmtMatch", "Ex97A326", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0005E994 File Offset: 0x0005CB94
		public static LocalizedString ValueNotRecognizedForAttribute(string name)
		{
			return new LocalizedString("ValueNotRecognizedForAttribute", "ExE3054F", false, true, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0005E9C4 File Offset: 0x0005CBC4
		public static LocalizedString AmOperationFailedWithEcException(int ec)
		{
			return new LocalizedString("AmOperationFailedWithEcException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				ec
			});
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0005E9F8 File Offset: 0x0005CBF8
		public static LocalizedString ExStoreObjectValidationError
		{
			get
			{
				return new LocalizedString("ExStoreObjectValidationError", "Ex44DC6D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0005EA16 File Offset: 0x0005CC16
		public static LocalizedString MigrationBatchFlagNone
		{
			get
			{
				return new LocalizedString("MigrationBatchFlagNone", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0005EA34 File Offset: 0x0005CC34
		public static LocalizedString ClientCulture_0x4009
		{
			get
			{
				return new LocalizedString("ClientCulture_0x4009", "Ex79B0A0", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0005EA52 File Offset: 0x0005CC52
		public static LocalizedString TooManyAttachmentsOnProtectedMessage
		{
			get
			{
				return new LocalizedString("TooManyAttachmentsOnProtectedMessage", "ExB4408A", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0005EA70 File Offset: 0x0005CC70
		public static LocalizedString RuleParseError(string parameter)
		{
			return new LocalizedString("RuleParseError", "Ex591050", false, true, ServerStrings.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0005EA9F File Offset: 0x0005CC9F
		public static LocalizedString PublicFolderOpenFailedOnExistingFolder
		{
			get
			{
				return new LocalizedString("PublicFolderOpenFailedOnExistingFolder", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0005EABD File Offset: 0x0005CCBD
		public static LocalizedString ExInvalidSortOrder
		{
			get
			{
				return new LocalizedString("ExInvalidSortOrder", "ExF0900A", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0005EADB File Offset: 0x0005CCDB
		public static LocalizedString ReplyRuleNotSupportedOnNonMailPublicFolder
		{
			get
			{
				return new LocalizedString("ReplyRuleNotSupportedOnNonMailPublicFolder", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0005EAF9 File Offset: 0x0005CCF9
		public static LocalizedString ExGetPropsFailed
		{
			get
			{
				return new LocalizedString("ExGetPropsFailed", "Ex4AF038", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0005EB17 File Offset: 0x0005CD17
		public static LocalizedString EstimateStateSucceeded
		{
			get
			{
				return new LocalizedString("EstimateStateSucceeded", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x0005EB35 File Offset: 0x0005CD35
		public static LocalizedString MigrationBatchSupportedActionRemove
		{
			get
			{
				return new LocalizedString("MigrationBatchSupportedActionRemove", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0005EB53 File Offset: 0x0005CD53
		public static LocalizedString MapiCannotSaveMessageStream
		{
			get
			{
				return new LocalizedString("MapiCannotSaveMessageStream", "Ex40EBA3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0005EB74 File Offset: 0x0005CD74
		public static LocalizedString FolderRuleErrorRecordForSpecificRule(string id, string recipient, string stage, string exceptionType, string exceptionMessage)
		{
			return new LocalizedString("FolderRuleErrorRecordForSpecificRule", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				id,
				recipient,
				stage,
				exceptionType,
				exceptionMessage
			});
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0005EBB4 File Offset: 0x0005CDB4
		public static LocalizedString MapiInvalidId
		{
			get
			{
				return new LocalizedString("MapiInvalidId", "Ex00D65B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0005EBD2 File Offset: 0x0005CDD2
		public static LocalizedString ContactLinkingMaximumNumberOfContactsPerPersonError
		{
			get
			{
				return new LocalizedString("ContactLinkingMaximumNumberOfContactsPerPersonError", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0005EBF0 File Offset: 0x0005CDF0
		public static LocalizedString DataMoveReplicationConstraintUnknown(Guid databaseGuid)
		{
			return new LocalizedString("DataMoveReplicationConstraintUnknown", "ExFCEC68", false, true, ServerStrings.ResourceManager, new object[]
			{
				databaseGuid
			});
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0005EC24 File Offset: 0x0005CE24
		public static LocalizedString ConversionUnsupportedContent
		{
			get
			{
				return new LocalizedString("ConversionUnsupportedContent", "Ex274731", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0005EC42 File Offset: 0x0005CE42
		public static LocalizedString MigrationUserStatusIncrementalStopped
		{
			get
			{
				return new LocalizedString("MigrationUserStatusIncrementalStopped", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0005EC60 File Offset: 0x0005CE60
		public static LocalizedString MapiCannotCreateMessage
		{
			get
			{
				return new LocalizedString("MapiCannotCreateMessage", "ExDCC5F1", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0005EC80 File Offset: 0x0005CE80
		public static LocalizedString ExPropertyValidationFailed(string errorDescription, object property)
		{
			return new LocalizedString("ExPropertyValidationFailed", "Ex60EC60", false, true, ServerStrings.ResourceManager, new object[]
			{
				errorDescription,
				property
			});
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0005ECB3 File Offset: 0x0005CEB3
		public static LocalizedString InvalidSendAddressIdentity
		{
			get
			{
				return new LocalizedString("InvalidSendAddressIdentity", "Ex30307E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0005ECD1 File Offset: 0x0005CED1
		public static LocalizedString ClientCulture_0x425
		{
			get
			{
				return new LocalizedString("ClientCulture_0x425", "Ex64157B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0005ECF0 File Offset: 0x0005CEF0
		public static LocalizedString BadTimeFormatInChangeDate(string time)
		{
			return new LocalizedString("BadTimeFormatInChangeDate", "ExEE9D6C", false, true, ServerStrings.ResourceManager, new object[]
			{
				time
			});
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0005ED1F File Offset: 0x0005CF1F
		public static LocalizedString DisposeOOFHistoryFolder
		{
			get
			{
				return new LocalizedString("DisposeOOFHistoryFolder", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x0005ED3D File Offset: 0x0005CF3D
		public static LocalizedString ExCantDeleteLastOccurrence
		{
			get
			{
				return new LocalizedString("ExCantDeleteLastOccurrence", "ExA05995", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0005ED5C File Offset: 0x0005CF5C
		public static LocalizedString ExDeleteNotSupportedForCalculatedProperty(object proptertyID)
		{
			return new LocalizedString("ExDeleteNotSupportedForCalculatedProperty", "Ex7EB285", false, true, ServerStrings.ResourceManager, new object[]
			{
				proptertyID
			});
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0005ED8B File Offset: 0x0005CF8B
		public static LocalizedString MigrationReportBatchSuccess
		{
			get
			{
				return new LocalizedString("MigrationReportBatchSuccess", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0005EDAC File Offset: 0x0005CFAC
		public static LocalizedString DagNetworkCreateDupName(string name)
		{
			return new LocalizedString("DagNetworkCreateDupName", "ExE45F06", false, true, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0005EDDB File Offset: 0x0005CFDB
		public static LocalizedString ErrorAccessingLargeProperty
		{
			get
			{
				return new LocalizedString("ErrorAccessingLargeProperty", "ExAF00CF", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0005EDF9 File Offset: 0x0005CFF9
		public static LocalizedString OperationNotSupportedOnPublicFolderMailbox
		{
			get
			{
				return new LocalizedString("OperationNotSupportedOnPublicFolderMailbox", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x0005EE17 File Offset: 0x0005D017
		public static LocalizedString ExCannotCreateMeetingCancellation
		{
			get
			{
				return new LocalizedString("ExCannotCreateMeetingCancellation", "ExB794C5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0005EE35 File Offset: 0x0005D035
		public static LocalizedString MigrationFeaturePAW
		{
			get
			{
				return new LocalizedString("MigrationFeaturePAW", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0005EE54 File Offset: 0x0005D054
		public static LocalizedString DataMoveReplicationConstraintNotSatisfiedInvalidConstraint(DataMoveReplicationConstraintParameter constraint, Guid databaseGuid)
		{
			return new LocalizedString("DataMoveReplicationConstraintNotSatisfiedInvalidConstraint", "ExD92778", false, true, ServerStrings.ResourceManager, new object[]
			{
				constraint,
				databaseGuid
			});
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0005EE94 File Offset: 0x0005D094
		public static LocalizedString AmOperationFailedException(string errMessage)
		{
			return new LocalizedString("AmOperationFailedException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				errMessage
			});
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0005EEC3 File Offset: 0x0005D0C3
		public static LocalizedString InboxRuleFlagStatusFlagged
		{
			get
			{
				return new LocalizedString("InboxRuleFlagStatusFlagged", "ExF925D7", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0005EEE4 File Offset: 0x0005D0E4
		public static LocalizedString ExMismatchedSyncStateDataType(string expectedType, string actualType)
		{
			return new LocalizedString("ExMismatchedSyncStateDataType", "Ex311F7C", false, true, ServerStrings.ResourceManager, new object[]
			{
				expectedType,
				actualType
			});
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0005EF17 File Offset: 0x0005D117
		public static LocalizedString JunkEmailBlockedListXsoNullException
		{
			get
			{
				return new LocalizedString("JunkEmailBlockedListXsoNullException", "ExC8C78D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0005EF38 File Offset: 0x0005D138
		public static LocalizedString RpcServerIgnoreNotFoundTeamMailbox(string mailboxGuid)
		{
			return new LocalizedString("RpcServerIgnoreNotFoundTeamMailbox", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				mailboxGuid
			});
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0005EF67 File Offset: 0x0005D167
		public static LocalizedString ClientCulture_0x43E
		{
			get
			{
				return new LocalizedString("ClientCulture_0x43E", "Ex339C0C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0005EF88 File Offset: 0x0005D188
		public static LocalizedString AnchorDatabaseNotFound(string mdbGuid)
		{
			return new LocalizedString("AnchorDatabaseNotFound", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				mdbGuid
			});
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0005EFB7 File Offset: 0x0005D1B7
		public static LocalizedString TeamMailboxMessageGoToYourGroupSite
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageGoToYourGroupSite", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0005EFD5 File Offset: 0x0005D1D5
		public static LocalizedString ClientCulture_0x81A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x81A", "Ex36BF9E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0005EFF4 File Offset: 0x0005D1F4
		public static LocalizedString ExNoMatchingStorePropertyDefinition(string property)
		{
			return new LocalizedString("ExNoMatchingStorePropertyDefinition", "Ex30106A", false, true, ServerStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0005F023 File Offset: 0x0005D223
		public static LocalizedString CannotImportMessageMove
		{
			get
			{
				return new LocalizedString("CannotImportMessageMove", "Ex9F8509", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0005F041 File Offset: 0x0005D241
		public static LocalizedString FifteenMinutes
		{
			get
			{
				return new LocalizedString("FifteenMinutes", "ExD37565", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0005F05F File Offset: 0x0005D25F
		public static LocalizedString OneDays
		{
			get
			{
				return new LocalizedString("OneDays", "Ex553547", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0005F07D File Offset: 0x0005D27D
		public static LocalizedString CorruptNaturalLanguageProperty
		{
			get
			{
				return new LocalizedString("CorruptNaturalLanguageProperty", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0005F09C File Offset: 0x0005D29C
		public static LocalizedString FailedToRetrieveUserLicense(string userName, int errorCode)
		{
			return new LocalizedString("FailedToRetrieveUserLicense", "ExEBF2BA", false, true, ServerStrings.ResourceManager, new object[]
			{
				userName,
				errorCode
			});
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0005F0D4 File Offset: 0x0005D2D4
		public static LocalizedString DumpsterStatusAlreadyStartedException
		{
			get
			{
				return new LocalizedString("DumpsterStatusAlreadyStartedException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x0005F0F2 File Offset: 0x0005D2F2
		public static LocalizedString ExCannotSetSearchCriteria
		{
			get
			{
				return new LocalizedString("ExCannotSetSearchCriteria", "ExC1AAF5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0005F110 File Offset: 0x0005D310
		public static LocalizedString InvalidMailboxType(string externalDirectoryObjectId, string type)
		{
			return new LocalizedString("InvalidMailboxType", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				externalDirectoryObjectId,
				type
			});
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x0005F143 File Offset: 0x0005D343
		public static LocalizedString ExBadObjectType
		{
			get
			{
				return new LocalizedString("ExBadObjectType", "Ex7CA0F8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0005F161 File Offset: 0x0005D361
		public static LocalizedString SpellCheckerFinnish
		{
			get
			{
				return new LocalizedString("SpellCheckerFinnish", "Ex43DA68", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0005F17F File Offset: 0x0005D37F
		public static LocalizedString MigrationBatchStatusWaiting
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusWaiting", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0005F1A0 File Offset: 0x0005D3A0
		public static LocalizedString FailedToFindServerInfo(Uri url)
		{
			return new LocalizedString("FailedToFindServerInfo", "Ex3DBB29", false, true, ServerStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0005F1CF File Offset: 0x0005D3CF
		public static LocalizedString UnsupportedKindKeywords
		{
			get
			{
				return new LocalizedString("UnsupportedKindKeywords", "ExC651A7", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x0005F1ED File Offset: 0x0005D3ED
		public static LocalizedString ClientCulture_0x407
		{
			get
			{
				return new LocalizedString("ClientCulture_0x407", "ExF9CD83", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x0005F20B File Offset: 0x0005D40B
		public static LocalizedString PropertyChangeMetadataParseError
		{
			get
			{
				return new LocalizedString("PropertyChangeMetadataParseError", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0005F22C File Offset: 0x0005D42C
		public static LocalizedString FailedToReadSharedServerBoxRacIdentityFromIRMConfig(string orgId)
		{
			return new LocalizedString("FailedToReadSharedServerBoxRacIdentityFromIRMConfig", "Ex9FD150", false, true, ServerStrings.ResourceManager, new object[]
			{
				orgId
			});
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0005F25C File Offset: 0x0005D45C
		public static LocalizedString ExNonCalendarItemReturned(string msgClass)
		{
			return new LocalizedString("ExNonCalendarItemReturned", "Ex8CE0C4", false, true, ServerStrings.ResourceManager, new object[]
			{
				msgClass
			});
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0005F28B File Offset: 0x0005D48B
		public static LocalizedString SyncFailedToCreateNewItemOrBindToExistingOne
		{
			get
			{
				return new LocalizedString("SyncFailedToCreateNewItemOrBindToExistingOne", "Ex6A0319", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0005F2AC File Offset: 0x0005D4AC
		public static LocalizedString DelegateValidationFailed(string name)
		{
			return new LocalizedString("DelegateValidationFailed", "Ex5C0C76", false, true, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0005F2DB File Offset: 0x0005D4DB
		public static LocalizedString ConversionFailedInvalidMacBin
		{
			get
			{
				return new LocalizedString("ConversionFailedInvalidMacBin", "ExAB4FF2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x0005F2F9 File Offset: 0x0005D4F9
		public static LocalizedString SpellCheckerEnglishUnitedStates
		{
			get
			{
				return new LocalizedString("SpellCheckerEnglishUnitedStates", "Ex947099", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0005F318 File Offset: 0x0005D518
		public static LocalizedString ExResponseTypeNoSubjectPrefix(string responseType)
		{
			return new LocalizedString("ExResponseTypeNoSubjectPrefix", "Ex85E45A", false, true, ServerStrings.ResourceManager, new object[]
			{
				responseType
			});
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x0005F347 File Offset: 0x0005D547
		public static LocalizedString ExContactHasNoId
		{
			get
			{
				return new LocalizedString("ExContactHasNoId", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0005F368 File Offset: 0x0005D568
		public static LocalizedString MigrationNSPINoUsersFound(string exchangeServer)
		{
			return new LocalizedString("MigrationNSPINoUsersFound", "ExB6381B", false, true, ServerStrings.ResourceManager, new object[]
			{
				exchangeServer
			});
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x0005F397 File Offset: 0x0005D597
		public static LocalizedString ErrorExTimeZoneValueTimeZoneNotFound
		{
			get
			{
				return new LocalizedString("ErrorExTimeZoneValueTimeZoneNotFound", "ExECE8E5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0005F3B5 File Offset: 0x0005D5B5
		public static LocalizedString SpellCheckerGermanPostReform
		{
			get
			{
				return new LocalizedString("SpellCheckerGermanPostReform", "ExADA197", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x0005F3D3 File Offset: 0x0005D5D3
		public static LocalizedString InboxRuleMessageTypePermissionControlled
		{
			get
			{
				return new LocalizedString("InboxRuleMessageTypePermissionControlled", "Ex3F80B1", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0005F3F1 File Offset: 0x0005D5F1
		public static LocalizedString ClientCulture_0x40F
		{
			get
			{
				return new LocalizedString("ClientCulture_0x40F", "ExD2A74A", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x0005F40F File Offset: 0x0005D60F
		public static LocalizedString PropertyDefinitionsValuesNotMatch
		{
			get
			{
				return new LocalizedString("PropertyDefinitionsValuesNotMatch", "Ex9D6AF5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0005F430 File Offset: 0x0005D630
		public static LocalizedString JunkEmailTrustedListXsoGenericException(string value)
		{
			return new LocalizedString("JunkEmailTrustedListXsoGenericException", "Ex06F12D", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x0005F45F File Offset: 0x0005D65F
		public static LocalizedString ClientCulture_0xC1A
		{
			get
			{
				return new LocalizedString("ClientCulture_0xC1A", "Ex2B5F79", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0005F47D File Offset: 0x0005D67D
		public static LocalizedString DateRangeThreeMonths
		{
			get
			{
				return new LocalizedString("DateRangeThreeMonths", "Ex8F5EE5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0005F49B File Offset: 0x0005D69B
		public static LocalizedString ExConnectionAlternate
		{
			get
			{
				return new LocalizedString("ExConnectionAlternate", "ExA872D8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0005F4B9 File Offset: 0x0005D6B9
		public static LocalizedString MigrationBatchSupportedActionStart
		{
			get
			{
				return new LocalizedString("MigrationBatchSupportedActionStart", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0005F4D7 File Offset: 0x0005D6D7
		public static LocalizedString ClientCulture_0x402
		{
			get
			{
				return new LocalizedString("ClientCulture_0x402", "ExA5CF99", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0005F4F8 File Offset: 0x0005D6F8
		public static LocalizedString AttemptingSessionCreationAgainstWrongGroupMailbox(string current, string original)
		{
			return new LocalizedString("AttemptingSessionCreationAgainstWrongGroupMailbox", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				current,
				original
			});
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x0005F52B File Offset: 0x0005D72B
		public static LocalizedString ExCannotAccessAdminAuditLogsFolderId
		{
			get
			{
				return new LocalizedString("ExCannotAccessAdminAuditLogsFolderId", "Ex0F6EAC", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x0005F549 File Offset: 0x0005D749
		public static LocalizedString ClientCulture_0x424
		{
			get
			{
				return new LocalizedString("ClientCulture_0x424", "Ex64E988", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x0005F567 File Offset: 0x0005D767
		public static LocalizedString MigrationStateWaiting
		{
			get
			{
				return new LocalizedString("MigrationStateWaiting", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x0005F585 File Offset: 0x0005D785
		public static LocalizedString MigrationStageProcessing
		{
			get
			{
				return new LocalizedString("MigrationStageProcessing", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x0005F5A3 File Offset: 0x0005D7A3
		public static LocalizedString Database
		{
			get
			{
				return new LocalizedString("Database", "Ex94C5DB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0005F5C4 File Offset: 0x0005D7C4
		public static LocalizedString descInvalidTypeInBookingPolicyConfig(string type, string parameter)
		{
			return new LocalizedString("descInvalidTypeInBookingPolicyConfig", "Ex544FD6", false, true, ServerStrings.ResourceManager, new object[]
			{
				type,
				parameter
			});
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0005F5F7 File Offset: 0x0005D7F7
		public static LocalizedString MapiCannotGetTransportQueueFolderId
		{
			get
			{
				return new LocalizedString("MapiCannotGetTransportQueueFolderId", "Ex5A898B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0005F618 File Offset: 0x0005D818
		public static LocalizedString NoSupportException(LocalizedString exceptionMessage)
		{
			return new LocalizedString("NoSupportException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				exceptionMessage
			});
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0005F64C File Offset: 0x0005D84C
		public static LocalizedString MalformedWorkingHours(string mailbox, string exceptionInfo)
		{
			return new LocalizedString("MalformedWorkingHours", "ExF3C4B1", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailbox,
				exceptionInfo
			});
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0005F680 File Offset: 0x0005D880
		public static LocalizedString DataMoveReplicationConstraintNotSatisfiedThrottled(DataMoveReplicationConstraintParameter constraint, Guid databaseGuid)
		{
			return new LocalizedString("DataMoveReplicationConstraintNotSatisfiedThrottled", "Ex073F5D", false, true, ServerStrings.ResourceManager, new object[]
			{
				constraint,
				databaseGuid
			});
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x0005F6BD File Offset: 0x0005D8BD
		public static LocalizedString UnsupportedAction
		{
			get
			{
				return new LocalizedString("UnsupportedAction", "Ex652C41", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0005F6DC File Offset: 0x0005D8DC
		public static LocalizedString PublicFolderOperationDenied(string objectType, string operation)
		{
			return new LocalizedString("PublicFolderOperationDenied", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				objectType,
				operation
			});
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0005F710 File Offset: 0x0005D910
		public static LocalizedString ExInvalidO12BytesToSkip(int bytesToSkip)
		{
			return new LocalizedString("ExInvalidO12BytesToSkip", "ExBBDAA2", false, true, ServerStrings.ResourceManager, new object[]
			{
				bytesToSkip
			});
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0005F744 File Offset: 0x0005D944
		public static LocalizedString ColumnError(string columnName, LocalizedString description)
		{
			return new LocalizedString("ColumnError", "ExE345F2", false, true, ServerStrings.ResourceManager, new object[]
			{
				columnName,
				description
			});
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0005F77C File Offset: 0x0005D97C
		public static LocalizedString FolderRuleErrorInvalidRecipientEntryId
		{
			get
			{
				return new LocalizedString("FolderRuleErrorInvalidRecipientEntryId", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0005F79A File Offset: 0x0005D99A
		public static LocalizedString TeamMailboxMessageGoToTheSite
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageGoToTheSite", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0005F7B8 File Offset: 0x0005D9B8
		public static LocalizedString TwelveHours
		{
			get
			{
				return new LocalizedString("TwelveHours", "Ex763518", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0005F7D6 File Offset: 0x0005D9D6
		public static LocalizedString MigrationStageInjection
		{
			get
			{
				return new LocalizedString("MigrationStageInjection", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0005F7F4 File Offset: 0x0005D9F4
		public static LocalizedString MapiCannotGetContentsTable
		{
			get
			{
				return new LocalizedString("MapiCannotGetContentsTable", "ExFCDAC3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0005F812 File Offset: 0x0005DA12
		public static LocalizedString EstimateStateStopped
		{
			get
			{
				return new LocalizedString("EstimateStateStopped", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0005F830 File Offset: 0x0005DA30
		public static LocalizedString NullWorkHours
		{
			get
			{
				return new LocalizedString("NullWorkHours", "Ex081036", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0005F84E File Offset: 0x0005DA4E
		public static LocalizedString MigrationUserStatusCompleting
		{
			get
			{
				return new LocalizedString("MigrationUserStatusCompleting", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0005F86C File Offset: 0x0005DA6C
		public static LocalizedString ErrorValidateXsoDriverProperty(string propertyName, string description)
		{
			return new LocalizedString("ErrorValidateXsoDriverProperty", "ExBB58F9", false, true, ServerStrings.ResourceManager, new object[]
			{
				propertyName,
				description
			});
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0005F89F File Offset: 0x0005DA9F
		public static LocalizedString FiveMinutes
		{
			get
			{
				return new LocalizedString("FiveMinutes", "Ex12C5DD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0005F8C0 File Offset: 0x0005DAC0
		public static LocalizedString AmInvalidActionCodeException(int actionCode, string member, string value)
		{
			return new LocalizedString("AmInvalidActionCodeException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				actionCode,
				member,
				value
			});
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x0005F8FC File Offset: 0x0005DAFC
		public static LocalizedString InboxRuleMessageTypeVoicemail
		{
			get
			{
				return new LocalizedString("InboxRuleMessageTypeVoicemail", "Ex39910F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0005F91C File Offset: 0x0005DB1C
		public static LocalizedString FailedToReadIRMConfig(string orgId)
		{
			return new LocalizedString("FailedToReadIRMConfig", "ExABD478", false, true, ServerStrings.ResourceManager, new object[]
			{
				orgId
			});
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x0005F94B File Offset: 0x0005DB4B
		public static LocalizedString SpellCheckerPortugueseBrasil
		{
			get
			{
				return new LocalizedString("SpellCheckerPortugueseBrasil", "ExF8A303", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0005F96C File Offset: 0x0005DB6C
		public static LocalizedString CultureMismatchAfterConnect(string setCulture, string storeCulture)
		{
			return new LocalizedString("CultureMismatchAfterConnect", "ExEC3FAB", false, true, ServerStrings.ResourceManager, new object[]
			{
				setCulture,
				storeCulture
			});
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x0005F99F File Offset: 0x0005DB9F
		public static LocalizedString GenericFailureRMDecryption
		{
			get
			{
				return new LocalizedString("GenericFailureRMDecryption", "Ex713D35", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0005F9C0 File Offset: 0x0005DBC0
		public static LocalizedString ExternalIdentityInvalidSid(string mailbox, string entryId, string sid)
		{
			return new LocalizedString("ExternalIdentityInvalidSid", "ExE7931F", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailbox,
				entryId,
				sid
			});
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0005F9F8 File Offset: 0x0005DBF8
		public static LocalizedString MaxRemindersExceeded(int count, int max)
		{
			return new LocalizedString("MaxRemindersExceeded", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				count,
				max
			});
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0005FA35 File Offset: 0x0005DC35
		public static LocalizedString SpellCheckerEnglishAustralia
		{
			get
			{
				return new LocalizedString("SpellCheckerEnglishAustralia", "ExE6AA20", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0005FA54 File Offset: 0x0005DC54
		public static LocalizedString NonUniqueAssociationError(string idProperty, string values)
		{
			return new LocalizedString("NonUniqueAssociationError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				idProperty,
				values
			});
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0005FA87 File Offset: 0x0005DC87
		public static LocalizedString NoDeferredActions
		{
			get
			{
				return new LocalizedString("NoDeferredActions", "Ex8053A8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x0005FAA5 File Offset: 0x0005DCA5
		public static LocalizedString ErrorSetDateTimeFormatWithoutLanguage
		{
			get
			{
				return new LocalizedString("ErrorSetDateTimeFormatWithoutLanguage", "Ex477B73", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x0005FAC3 File Offset: 0x0005DCC3
		public static LocalizedString ClientCulture_0x1801
		{
			get
			{
				return new LocalizedString("ClientCulture_0x1801", "Ex2C8945", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x0005FAE1 File Offset: 0x0005DCE1
		public static LocalizedString MapiErrorParsingId
		{
			get
			{
				return new LocalizedString("MapiErrorParsingId", "ExFF857F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0005FAFF File Offset: 0x0005DCFF
		public static LocalizedString MigrationUserAdminTypePartnerTenant
		{
			get
			{
				return new LocalizedString("MigrationUserAdminTypePartnerTenant", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x0005FB1D File Offset: 0x0005DD1D
		public static LocalizedString MigrationUserStatusStopped
		{
			get
			{
				return new LocalizedString("MigrationUserStatusStopped", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0005FB3C File Offset: 0x0005DD3C
		public static LocalizedString ExConfigSerializeDictionaryFailed(Exception e)
		{
			return new LocalizedString("ExConfigSerializeDictionaryFailed", "Ex8050F4", false, true, ServerStrings.ResourceManager, new object[]
			{
				e
			});
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0005FB6C File Offset: 0x0005DD6C
		public static LocalizedString PublicFolderSyncFolderFailed(string innerMessage)
		{
			return new LocalizedString("PublicFolderSyncFolderFailed", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				innerMessage
			});
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0005FB9B File Offset: 0x0005DD9B
		public static LocalizedString MigrationReportBatchFailure
		{
			get
			{
				return new LocalizedString("MigrationReportBatchFailure", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0005FBB9 File Offset: 0x0005DDB9
		public static LocalizedString MapiCannotCreateAttachment
		{
			get
			{
				return new LocalizedString("MapiCannotCreateAttachment", "Ex03A335", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0005FBD8 File Offset: 0x0005DDD8
		public static LocalizedString ErrorUnsupportedConfigurationXmlCategory(string category)
		{
			return new LocalizedString("ErrorUnsupportedConfigurationXmlCategory", "Ex14D62D", false, true, ServerStrings.ResourceManager, new object[]
			{
				category
			});
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0005FC08 File Offset: 0x0005DE08
		public static LocalizedString FailedToRetrieveServerLicense(int errorCode)
		{
			return new LocalizedString("FailedToRetrieveServerLicense", "Ex6C68CC", false, true, ServerStrings.ResourceManager, new object[]
			{
				errorCode
			});
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0005FC3C File Offset: 0x0005DE3C
		public static LocalizedString NotificationEmailBodyCertExpiring
		{
			get
			{
				return new LocalizedString("NotificationEmailBodyCertExpiring", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x0005FC5A File Offset: 0x0005DE5A
		public static LocalizedString MapiCannotReadPerUserInformation
		{
			get
			{
				return new LocalizedString("MapiCannotReadPerUserInformation", "Ex948053", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0005FC78 File Offset: 0x0005DE78
		public static LocalizedString ExInvalidSubFilterProperty
		{
			get
			{
				return new LocalizedString("ExInvalidSubFilterProperty", "ExD17538", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0005FC96 File Offset: 0x0005DE96
		public static LocalizedString StockReplyTemplate
		{
			get
			{
				return new LocalizedString("StockReplyTemplate", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0005FCB4 File Offset: 0x0005DEB4
		public static LocalizedString CalNotifTypeSummary
		{
			get
			{
				return new LocalizedString("CalNotifTypeSummary", "Ex57AA7C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0005FCD2 File Offset: 0x0005DED2
		public static LocalizedString JunkEmailInvalidConstructionException
		{
			get
			{
				return new LocalizedString("JunkEmailInvalidConstructionException", "Ex1F7B70", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0005FCF0 File Offset: 0x0005DEF0
		public static LocalizedString MapiCannotCreateAssociatedMessage
		{
			get
			{
				return new LocalizedString("MapiCannotCreateAssociatedMessage", "Ex924471", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0005FD0E File Offset: 0x0005DF0E
		public static LocalizedString ClientCulture_0x413
		{
			get
			{
				return new LocalizedString("ClientCulture_0x413", "Ex794C6C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0005FD2C File Offset: 0x0005DF2C
		public static LocalizedString MapiCannotSortTable
		{
			get
			{
				return new LocalizedString("MapiCannotSortTable", "Ex03E6C9", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x0005FD4A File Offset: 0x0005DF4A
		public static LocalizedString MigrationUserStatusStopping
		{
			get
			{
				return new LocalizedString("MigrationUserStatusStopping", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0005FD68 File Offset: 0x0005DF68
		public static LocalizedString ExPropertyRequiresStreaming(PropertyDefinition propertyDefinition)
		{
			return new LocalizedString("ExPropertyRequiresStreaming", "Ex9DA8F2", false, true, ServerStrings.ResourceManager, new object[]
			{
				propertyDefinition
			});
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0005FD97 File Offset: 0x0005DF97
		public static LocalizedString MapiCannotGetRecipientTable
		{
			get
			{
				return new LocalizedString("MapiCannotGetRecipientTable", "Ex8164D2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0005FDB5 File Offset: 0x0005DFB5
		public static LocalizedString ExInvalidCallToTryUpdateCalendarItem
		{
			get
			{
				return new LocalizedString("ExInvalidCallToTryUpdateCalendarItem", "Ex6F6133", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0005FDD3 File Offset: 0x0005DFD3
		public static LocalizedString ClientCulture_0x406
		{
			get
			{
				return new LocalizedString("ClientCulture_0x406", "ExF0511D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0005FDF4 File Offset: 0x0005DFF4
		public static LocalizedString ConversationWithoutRootNode(string conversationId)
		{
			return new LocalizedString("ConversationWithoutRootNode", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				conversationId
			});
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0005FE23 File Offset: 0x0005E023
		public static LocalizedString ExCannotAccessAuditsFolderId
		{
			get
			{
				return new LocalizedString("ExCannotAccessAuditsFolderId", "Ex9362A8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x0005FE41 File Offset: 0x0005E041
		public static LocalizedString ExReadEventsFailed
		{
			get
			{
				return new LocalizedString("ExReadEventsFailed", "ExA87AB7", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x0005FE5F File Offset: 0x0005E05F
		public static LocalizedString ExCannotQueryAssociatedTable
		{
			get
			{
				return new LocalizedString("ExCannotQueryAssociatedTable", "Ex6D5BB3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0005FE80 File Offset: 0x0005E080
		public static LocalizedString idClientSessionInfoTypeParseException(string typeName, string assemblyName)
		{
			return new LocalizedString("idClientSessionInfoTypeParseException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				typeName,
				assemblyName
			});
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0005FEB3 File Offset: 0x0005E0B3
		public static LocalizedString ClientCulture_0x429
		{
			get
			{
				return new LocalizedString("ClientCulture_0x429", "Ex41D50D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0005FED1 File Offset: 0x0005E0D1
		public static LocalizedString MigrationStepProvisioningUpdate
		{
			get
			{
				return new LocalizedString("MigrationStepProvisioningUpdate", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0005FEF0 File Offset: 0x0005E0F0
		public static LocalizedString ExBatchBuilderError(object batchBuilder, string message)
		{
			return new LocalizedString("ExBatchBuilderError", "Ex6851DB", false, true, ServerStrings.ResourceManager, new object[]
			{
				batchBuilder,
				message
			});
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0005FF24 File Offset: 0x0005E124
		public static LocalizedString ActiveManagerGenericRpcVersionNotSupported(int requestServerVersion, int requestCommandId, int requestCommandMajorVersion, int requestCommandMinorVersion, int actualServerVersion, int actualMajorVersion, int actualMinorVersion)
		{
			return new LocalizedString("ActiveManagerGenericRpcVersionNotSupported", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				requestServerVersion,
				requestCommandId,
				requestCommandMajorVersion,
				requestCommandMinorVersion,
				actualServerVersion,
				actualMajorVersion,
				actualMinorVersion
			});
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0005FF94 File Offset: 0x0005E194
		public static LocalizedString ErrorTeamMailboxUserVersionUnqualified(string users)
		{
			return new LocalizedString("ErrorTeamMailboxUserVersionUnqualified", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				users
			});
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0005FFC3 File Offset: 0x0005E1C3
		public static LocalizedString TwoWeeks
		{
			get
			{
				return new LocalizedString("TwoWeeks", "Ex93CD82", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0005FFE4 File Offset: 0x0005E1E4
		public static LocalizedString ExInvalidMonthNthOccurence(int nthOccurrence)
		{
			return new LocalizedString("ExInvalidMonthNthOccurence", "Ex7CAC44", false, true, ServerStrings.ResourceManager, new object[]
			{
				nthOccurrence
			});
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00060018 File Offset: 0x0005E218
		public static LocalizedString MigrationFeatureUpgradeBlock
		{
			get
			{
				return new LocalizedString("MigrationFeatureUpgradeBlock", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00060036 File Offset: 0x0005E236
		public static LocalizedString ExInvalidServiceType
		{
			get
			{
				return new LocalizedString("ExInvalidServiceType", "ExC2CDB3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x00060054 File Offset: 0x0005E254
		public static LocalizedString NullTimeInChangeDate
		{
			get
			{
				return new LocalizedString("NullTimeInChangeDate", "ExE180A8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00060072 File Offset: 0x0005E272
		public static LocalizedString ConversionInvalidSmimeClearSignedContent
		{
			get
			{
				return new LocalizedString("ConversionInvalidSmimeClearSignedContent", "Ex24059F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00060090 File Offset: 0x0005E290
		public static LocalizedString RequestStateSuspended
		{
			get
			{
				return new LocalizedString("RequestStateSuspended", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x000600AE File Offset: 0x0005E2AE
		public static LocalizedString MapiIsFromPublicStoreCheckFailed
		{
			get
			{
				return new LocalizedString("MapiIsFromPublicStoreCheckFailed", "Ex55032A", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x000600CC File Offset: 0x0005E2CC
		public static LocalizedString ExInvalidEventWatermarkBadEventCounter(long eventCounter, long lastObservedEventCounter)
		{
			return new LocalizedString("ExInvalidEventWatermarkBadEventCounter", "Ex810694", false, true, ServerStrings.ResourceManager, new object[]
			{
				eventCounter,
				lastObservedEventCounter
			});
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0006010C File Offset: 0x0005E30C
		public static LocalizedString ErrorInvalidDateFormat(string dateFormat, string lang, string validFormats)
		{
			return new LocalizedString("ErrorInvalidDateFormat", "Ex328A48", false, true, ServerStrings.ResourceManager, new object[]
			{
				dateFormat,
				lang,
				validFormats
			});
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00060144 File Offset: 0x0005E344
		public static LocalizedString TaskRecurrenceNotSupported(string pattern, string range)
		{
			return new LocalizedString("TaskRecurrenceNotSupported", "Ex154990", false, true, ServerStrings.ResourceManager, new object[]
			{
				pattern,
				range
			});
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00060177 File Offset: 0x0005E377
		public static LocalizedString ExCannotSendMeetingMessages
		{
			get
			{
				return new LocalizedString("ExCannotSendMeetingMessages", "Ex4C4CD7", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x00060195 File Offset: 0x0005E395
		public static LocalizedString ExAuditsDeleteDenied
		{
			get
			{
				return new LocalizedString("ExAuditsDeleteDenied", "ExB55749", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x000601B3 File Offset: 0x0005E3B3
		public static LocalizedString ClientCulture_0x416
		{
			get
			{
				return new LocalizedString("ClientCulture_0x416", "Ex002C22", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x000601D4 File Offset: 0x0005E3D4
		public static LocalizedString CannotCreateCollector(Type xsoManifestType)
		{
			return new LocalizedString("CannotCreateCollector", "Ex4D427A", false, true, ServerStrings.ResourceManager, new object[]
			{
				xsoManifestType
			});
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00060203 File Offset: 0x0005E403
		public static LocalizedString MissingPropertyValue
		{
			get
			{
				return new LocalizedString("MissingPropertyValue", "Ex377209", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x00060221 File Offset: 0x0005E421
		public static LocalizedString FolderNotPublishedException
		{
			get
			{
				return new LocalizedString("FolderNotPublishedException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0006023F File Offset: 0x0005E43F
		public static LocalizedString ServerLocatorClientEndpointNotFoundException
		{
			get
			{
				return new LocalizedString("ServerLocatorClientEndpointNotFoundException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00060260 File Offset: 0x0005E460
		public static LocalizedString DataBaseNotFoundError(Guid databaseGuid)
		{
			return new LocalizedString("DataBaseNotFoundError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				databaseGuid
			});
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00060294 File Offset: 0x0005E494
		public static LocalizedString ExTooComplexGroupSortParameter
		{
			get
			{
				return new LocalizedString("ExTooComplexGroupSortParameter", "Ex192DC8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x000602B4 File Offset: 0x0005E4B4
		public static LocalizedString UnsupportedReportType(string className)
		{
			return new LocalizedString("UnsupportedReportType", "Ex3E9262", false, true, ServerStrings.ResourceManager, new object[]
			{
				className
			});
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000602E4 File Offset: 0x0005E4E4
		public static LocalizedString ExceptionIsNotPublicFolder(string name)
		{
			return new LocalizedString("ExceptionIsNotPublicFolder", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00060313 File Offset: 0x0005E513
		public static LocalizedString MapiCannotLookupEntryId
		{
			get
			{
				return new LocalizedString("MapiCannotLookupEntryId", "Ex52F50C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00060331 File Offset: 0x0005E531
		public static LocalizedString NotificationEmailBodyExportPSTCreated
		{
			get
			{
				return new LocalizedString("NotificationEmailBodyExportPSTCreated", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x0006034F File Offset: 0x0005E54F
		public static LocalizedString TeamMailboxMessageReactivatedBodyIntroText
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageReactivatedBodyIntroText", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0006036D File Offset: 0x0005E56D
		public static LocalizedString NotSupportedWithMailboxVersionException
		{
			get
			{
				return new LocalizedString("NotSupportedWithMailboxVersionException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0006038B File Offset: 0x0005E58B
		public static LocalizedString ClientCulture_0x3409
		{
			get
			{
				return new LocalizedString("ClientCulture_0x3409", "Ex9BC8BD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x000603AC File Offset: 0x0005E5AC
		public static LocalizedString ExTooManySubscriptions(string mailbox, string server)
		{
			return new LocalizedString("ExTooManySubscriptions", "Ex40965C", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailbox,
				server
			});
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x000603E0 File Offset: 0x0005E5E0
		public static LocalizedString ActiveMonitoringRpcVersionNotSupported(int requestServerVersion, int requestCommandId, int requestCommandMajorVersion, int requestCommandMinorVersion, int actualServerVersion, int actualMajorVersion, int actualMinorVersion)
		{
			return new LocalizedString("ActiveMonitoringRpcVersionNotSupported", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				requestServerVersion,
				requestCommandId,
				requestCommandMajorVersion,
				requestCommandMinorVersion,
				actualServerVersion,
				actualMajorVersion,
				actualMinorVersion
			});
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0006044D File Offset: 0x0005E64D
		public static LocalizedString ClientCulture_0x41B
		{
			get
			{
				return new LocalizedString("ClientCulture_0x41B", "Ex335AED", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0006046B File Offset: 0x0005E66B
		public static LocalizedString CannotAddAttachmentToReadOnlyCollection
		{
			get
			{
				return new LocalizedString("CannotAddAttachmentToReadOnlyCollection", "ExBE25CD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0006048C File Offset: 0x0005E68C
		public static LocalizedString ExReplyToTooManyRecipients(int maxRecipientsAllowed)
		{
			return new LocalizedString("ExReplyToTooManyRecipients", "Ex874145", false, true, ServerStrings.ResourceManager, new object[]
			{
				maxRecipientsAllowed
			});
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x000604C0 File Offset: 0x0005E6C0
		public static LocalizedString UserPhotoPreviewNotFound
		{
			get
			{
				return new LocalizedString("UserPhotoPreviewNotFound", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x000604E0 File Offset: 0x0005E6E0
		public static LocalizedString OscSyncLockNotFound(string provider, string userId, string networkId)
		{
			return new LocalizedString("OscSyncLockNotFound", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				provider,
				userId,
				networkId
			});
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00060518 File Offset: 0x0005E718
		public static LocalizedString ErrorFailedToDeletePublicFolder(string identity, string exceptionMessage)
		{
			return new LocalizedString("ErrorFailedToDeletePublicFolder", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				identity,
				exceptionMessage
			});
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0006054B File Offset: 0x0005E74B
		public static LocalizedString PublicFolderMailboxesCannotBeMovedDuringMigration
		{
			get
			{
				return new LocalizedString("PublicFolderMailboxesCannotBeMovedDuringMigration", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0006056C File Offset: 0x0005E76C
		public static LocalizedString RpcServerIgnoreClosedTeamMailbox(string mailboxGuid)
		{
			return new LocalizedString("RpcServerIgnoreClosedTeamMailbox", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				mailboxGuid
			});
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0006059C File Offset: 0x0005E79C
		public static LocalizedString FailedToVerifyDRMPropsSignature(string userIdentity, int errorCode)
		{
			return new LocalizedString("FailedToVerifyDRMPropsSignature", "Ex93C211", false, true, ServerStrings.ResourceManager, new object[]
			{
				userIdentity,
				errorCode
			});
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x000605D4 File Offset: 0x0005E7D4
		public static LocalizedString MigrationBatchDirectionOnboarding
		{
			get
			{
				return new LocalizedString("MigrationBatchDirectionOnboarding", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x000605F4 File Offset: 0x0005E7F4
		public static LocalizedString MigrationSystemMailboxNotFound(string mailboxName)
		{
			return new LocalizedString("MigrationSystemMailboxNotFound", "Ex1F974A", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailboxName
			});
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00060624 File Offset: 0x0005E824
		public static LocalizedString InvalidSmtpAddress(string address)
		{
			return new LocalizedString("InvalidSmtpAddress", "Ex0FD11F", false, true, ServerStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x00060653 File Offset: 0x0005E853
		public static LocalizedString AsyncOperationTypeMigration
		{
			get
			{
				return new LocalizedString("AsyncOperationTypeMigration", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00060674 File Offset: 0x0005E874
		public static LocalizedString InvokingMethodNotSupported(string type, string method)
		{
			return new LocalizedString("InvokingMethodNotSupported", "ExEFC7D9", false, true, ServerStrings.ResourceManager, new object[]
			{
				type,
				method
			});
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x000606A7 File Offset: 0x0005E8A7
		public static LocalizedString ClientCulture_0x40E
		{
			get
			{
				return new LocalizedString("ClientCulture_0x40E", "ExEB773A", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x000606C5 File Offset: 0x0005E8C5
		public static LocalizedString OriginatingServer
		{
			get
			{
				return new LocalizedString("OriginatingServer", "Ex3D197B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x000606E3 File Offset: 0x0005E8E3
		public static LocalizedString EstimateStatePartiallySucceeded
		{
			get
			{
				return new LocalizedString("EstimateStatePartiallySucceeded", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00060701 File Offset: 0x0005E901
		public static LocalizedString CannotImportDeletion
		{
			get
			{
				return new LocalizedString("CannotImportDeletion", "Ex965937", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0006071F File Offset: 0x0005E91F
		public static LocalizedString MigrationUserStatusSynced
		{
			get
			{
				return new LocalizedString("MigrationUserStatusSynced", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x0006073D File Offset: 0x0005E93D
		public static LocalizedString CannotImportFolderChange
		{
			get
			{
				return new LocalizedString("CannotImportFolderChange", "Ex4EB3C1", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x0006075B File Offset: 0x0005E95B
		public static LocalizedString MigrationUserStatusValidating
		{
			get
			{
				return new LocalizedString("MigrationUserStatusValidating", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0006077C File Offset: 0x0005E97C
		public static LocalizedString FailedToGetOrgContainer(Guid externalDirectoryOrgId)
		{
			return new LocalizedString("FailedToGetOrgContainer", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				externalDirectoryOrgId
			});
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x000607B0 File Offset: 0x0005E9B0
		public static LocalizedString ValidationForServiceLocationResponseFailed(Uri url, string str)
		{
			return new LocalizedString("ValidationForServiceLocationResponseFailed", "Ex2DE658", false, true, ServerStrings.ResourceManager, new object[]
			{
				url,
				str
			});
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x000607E4 File Offset: 0x0005E9E4
		public static LocalizedString MigrationObjectsCountStringContacts(string contacts)
		{
			return new LocalizedString("MigrationObjectsCountStringContacts", "Ex390820", false, true, ServerStrings.ResourceManager, new object[]
			{
				contacts
			});
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00060813 File Offset: 0x0005EA13
		public static LocalizedString ExConstraintNotSupportedForThisPropertyType
		{
			get
			{
				return new LocalizedString("ExConstraintNotSupportedForThisPropertyType", "Ex86612B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00060831 File Offset: 0x0005EA31
		public static LocalizedString NotificationEmailSubjectCertExpiring
		{
			get
			{
				return new LocalizedString("NotificationEmailSubjectCertExpiring", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00060850 File Offset: 0x0005EA50
		public static LocalizedString MailboxSearchEwsFailedExceptionWithError(string error)
		{
			return new LocalizedString("MailboxSearchEwsFailedExceptionWithError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0006087F File Offset: 0x0005EA7F
		public static LocalizedString MigrationUserStatusSummaryCompleted
		{
			get
			{
				return new LocalizedString("MigrationUserStatusSummaryCompleted", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0006089D File Offset: 0x0005EA9D
		public static LocalizedString SpellCheckerArabic
		{
			get
			{
				return new LocalizedString("SpellCheckerArabic", "ExAF5B2C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x000608BB File Offset: 0x0005EABB
		public static LocalizedString InternalLicensingDisabledForEnterprise
		{
			get
			{
				return new LocalizedString("InternalLicensingDisabledForEnterprise", "Ex175F62", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x000608D9 File Offset: 0x0005EAD9
		public static LocalizedString ClientCulture_0x240A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x240A", "Ex1B501D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x000608F8 File Offset: 0x0005EAF8
		public static LocalizedString ParsingErrorAt(int position)
		{
			return new LocalizedString("ParsingErrorAt", "ExFA340E", false, true, ServerStrings.ResourceManager, new object[]
			{
				position
			});
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0006092C File Offset: 0x0005EB2C
		public static LocalizedString RPCOperationAbortedBecauseOfAnotherRPCThread
		{
			get
			{
				return new LocalizedString("RPCOperationAbortedBecauseOfAnotherRPCThread", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x0006094A File Offset: 0x0005EB4A
		public static LocalizedString ExInvalidMdbGuid
		{
			get
			{
				return new LocalizedString("ExInvalidMdbGuid", "Ex095551", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00060968 File Offset: 0x0005EB68
		public static LocalizedString SpellCheckerEnglishUnitedKingdom
		{
			get
			{
				return new LocalizedString("SpellCheckerEnglishUnitedKingdom", "Ex4591C9", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00060986 File Offset: 0x0005EB86
		public static LocalizedString ExFilterHierarchyIsTooDeep
		{
			get
			{
				return new LocalizedString("ExFilterHierarchyIsTooDeep", "ExFA73AD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x000609A4 File Offset: 0x0005EBA4
		public static LocalizedString MapiCannotSetMessageLockState
		{
			get
			{
				return new LocalizedString("MapiCannotSetMessageLockState", "ExB84C64", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x000609C2 File Offset: 0x0005EBC2
		public static LocalizedString CannotProtectMessageForNonSmtpSender
		{
			get
			{
				return new LocalizedString("CannotProtectMessageForNonSmtpSender", "ExC5B140", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x000609E0 File Offset: 0x0005EBE0
		public static LocalizedString ExSearchFolderIsAlreadyVisibleToOutlook
		{
			get
			{
				return new LocalizedString("ExSearchFolderIsAlreadyVisibleToOutlook", "Ex4B7F3A", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x000609FE File Offset: 0x0005EBFE
		public static LocalizedString ExEntryIdFirst4Bytes
		{
			get
			{
				return new LocalizedString("ExEntryIdFirst4Bytes", "Ex511256", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x00060A1C File Offset: 0x0005EC1C
		public static LocalizedString CustomMessageLengthExceeded
		{
			get
			{
				return new LocalizedString("CustomMessageLengthExceeded", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00060A3C File Offset: 0x0005EC3C
		public static LocalizedString ExDictionaryDataCorruptedDuplicateKey(string key)
		{
			return new LocalizedString("ExDictionaryDataCorruptedDuplicateKey", "Ex5D6215", false, true, ServerStrings.ResourceManager, new object[]
			{
				key
			});
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00060A6B File Offset: 0x0005EC6B
		public static LocalizedString ExWrappedStreamFailure
		{
			get
			{
				return new LocalizedString("ExWrappedStreamFailure", "ExC79E85", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00060A89 File Offset: 0x0005EC89
		public static LocalizedString ErrorExTimeZoneValueWrongGmtFormat
		{
			get
			{
				return new LocalizedString("ErrorExTimeZoneValueWrongGmtFormat", "ExA84E98", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x00060AA7 File Offset: 0x0005ECA7
		public static LocalizedString InternalParserError
		{
			get
			{
				return new LocalizedString("InternalParserError", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00060AC8 File Offset: 0x0005ECC8
		public static LocalizedString MigrationNSPIMissingRequiredField(PropTag proptag)
		{
			return new LocalizedString("MigrationNSPIMissingRequiredField", "Ex097DA8", false, true, ServerStrings.ResourceManager, new object[]
			{
				proptag
			});
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x00060AFC File Offset: 0x0005ECFC
		public static LocalizedString ExInvalidCount
		{
			get
			{
				return new LocalizedString("ExInvalidCount", "ExC8BEE8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00060B1C File Offset: 0x0005ED1C
		public static LocalizedString MigrationTransientError(string messageDetail)
		{
			return new LocalizedString("MigrationTransientError", "ExA25383", false, true, ServerStrings.ResourceManager, new object[]
			{
				messageDetail
			});
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00060B4B File Offset: 0x0005ED4B
		public static LocalizedString ADUserNotFound
		{
			get
			{
				return new LocalizedString("ADUserNotFound", "ExFA5BEC", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00060B6C File Offset: 0x0005ED6C
		public static LocalizedString ExFolderSetPropsFailed(string exceptionMessage)
		{
			return new LocalizedString("ExFolderSetPropsFailed", "Ex30921A", false, true, ServerStrings.ResourceManager, new object[]
			{
				exceptionMessage
			});
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x00060B9B File Offset: 0x0005ED9B
		public static LocalizedString InboxRuleFlagStatusNotFlagged
		{
			get
			{
				return new LocalizedString("InboxRuleFlagStatusNotFlagged", "Ex75D0E8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00060BBC File Offset: 0x0005EDBC
		public static LocalizedString AmReferralException(string referredServer)
		{
			return new LocalizedString("AmReferralException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				referredServer
			});
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00060BEC File Offset: 0x0005EDEC
		public static LocalizedString TaskServerTransientException(string errorMessage)
		{
			return new LocalizedString("TaskServerTransientException", "Ex2EAFDE", false, true, ServerStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x00060C1B File Offset: 0x0005EE1B
		public static LocalizedString ConversionMustLoadAllPropeties
		{
			get
			{
				return new LocalizedString("ConversionMustLoadAllPropeties", "ExEB2F6D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00060C39 File Offset: 0x0005EE39
		public static LocalizedString ThreeHours
		{
			get
			{
				return new LocalizedString("ThreeHours", "ExE63F5A", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00060C58 File Offset: 0x0005EE58
		public static LocalizedString AmOperationNotValidOnCurrentRole(string error)
		{
			return new LocalizedString("AmOperationNotValidOnCurrentRole", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00060C87 File Offset: 0x0005EE87
		public static LocalizedString MapiCannotGetIDFromNames
		{
			get
			{
				return new LocalizedString("MapiCannotGetIDFromNames", "ExD4B997", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00060CA8 File Offset: 0x0005EEA8
		public static LocalizedString RangedParameter(string parameter, int max)
		{
			return new LocalizedString("RangedParameter", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				parameter,
				max
			});
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00060CE0 File Offset: 0x0005EEE0
		public static LocalizedString ErrorSigntureTooLarge
		{
			get
			{
				return new LocalizedString("ErrorSigntureTooLarge", "Ex68357B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x00060CFE File Offset: 0x0005EEFE
		public static LocalizedString MigrationBatchFlagReportInitial
		{
			get
			{
				return new LocalizedString("MigrationBatchFlagReportInitial", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x00060D1C File Offset: 0x0005EF1C
		public static LocalizedString ErrorTimeProposalEndTimeBeforeStartTime
		{
			get
			{
				return new LocalizedString("ErrorTimeProposalEndTimeBeforeStartTime", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x00060D3A File Offset: 0x0005EF3A
		public static LocalizedString CannotSetMessageFlagStatus
		{
			get
			{
				return new LocalizedString("CannotSetMessageFlagStatus", "ExC5EDAE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00060D58 File Offset: 0x0005EF58
		public static LocalizedString MigrationFlagsReport
		{
			get
			{
				return new LocalizedString("MigrationFlagsReport", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00060D78 File Offset: 0x0005EF78
		public static LocalizedString IsNotMailboxOwner(object userSid, object mailboxOwnerSid)
		{
			return new LocalizedString("IsNotMailboxOwner", "ExBB3EF9", false, true, ServerStrings.ResourceManager, new object[]
			{
				userSid,
				mailboxOwnerSid
			});
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00060DAC File Offset: 0x0005EFAC
		public static LocalizedString JunkEmailBlockedListXsoDuplicateException(string value)
		{
			return new LocalizedString("JunkEmailBlockedListXsoDuplicateException", "ExBDDD0D", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x00060DDB File Offset: 0x0005EFDB
		public static LocalizedString MigrationStepProvisioning
		{
			get
			{
				return new LocalizedString("MigrationStepProvisioning", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00060DF9 File Offset: 0x0005EFF9
		public static LocalizedString FirstFourDayWeek
		{
			get
			{
				return new LocalizedString("FirstFourDayWeek", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x00060E17 File Offset: 0x0005F017
		public static LocalizedString MapiCannotModifyRecipients
		{
			get
			{
				return new LocalizedString("MapiCannotModifyRecipients", "ExA3B4C2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00060E35 File Offset: 0x0005F035
		public static LocalizedString ConversionCorruptSummaryTnef
		{
			get
			{
				return new LocalizedString("ConversionCorruptSummaryTnef", "Ex1DB7D1", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00060E54 File Offset: 0x0005F054
		public static LocalizedString ExTooManyDuplicateDataColumns(int maxDuplicateDataColumns)
		{
			return new LocalizedString("ExTooManyDuplicateDataColumns", "Ex0117E9", false, true, ServerStrings.ResourceManager, new object[]
			{
				maxDuplicateDataColumns
			});
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x00060E88 File Offset: 0x0005F088
		public static LocalizedString ClientCulture_0x2409
		{
			get
			{
				return new LocalizedString("ClientCulture_0x2409", "Ex49949E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x00060EA6 File Offset: 0x0005F0A6
		public static LocalizedString ExAlreadyConnected
		{
			get
			{
				return new LocalizedString("ExAlreadyConnected", "Ex9A2928", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00060EC4 File Offset: 0x0005F0C4
		public static LocalizedString MigrationErrorString(string emailAddress, string localizedMessage)
		{
			return new LocalizedString("MigrationErrorString", "ExFFEE96", false, true, ServerStrings.ResourceManager, new object[]
			{
				emailAddress,
				localizedMessage
			});
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x00060EF7 File Offset: 0x0005F0F7
		public static LocalizedString ExReportMessageCorruptedDueToWrongItemAttachmentType
		{
			get
			{
				return new LocalizedString("ExReportMessageCorruptedDueToWrongItemAttachmentType", "Ex6849D8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00060F18 File Offset: 0x0005F118
		public static LocalizedString MigrationGroupMembersNotAvailable(string groupSmtpAddress)
		{
			return new LocalizedString("MigrationGroupMembersNotAvailable", "ExE2FEE0", false, true, ServerStrings.ResourceManager, new object[]
			{
				groupSmtpAddress
			});
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00060F48 File Offset: 0x0005F148
		public static LocalizedString RpcServerIgnoreUnlinkedTeamMailbox(string mailboxGuid)
		{
			return new LocalizedString("RpcServerIgnoreUnlinkedTeamMailbox", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				mailboxGuid
			});
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00060F77 File Offset: 0x0005F177
		public static LocalizedString ClientCulture_0x500A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x500A", "ExFDB090", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x00060F95 File Offset: 0x0005F195
		public static LocalizedString MigrationTypeNone
		{
			get
			{
				return new LocalizedString("MigrationTypeNone", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00060FB4 File Offset: 0x0005F1B4
		public static LocalizedString AmServerException(string errorMessage)
		{
			return new LocalizedString("AmServerException", "Ex92D94B", false, true, ServerStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x00060FE3 File Offset: 0x0005F1E3
		public static LocalizedString CannotImportReadStateChange
		{
			get
			{
				return new LocalizedString("CannotImportReadStateChange", "Ex8FC2C6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00061004 File Offset: 0x0005F204
		public static LocalizedString ExConversationNotFound(string conversationId, string conversationFamilyId)
		{
			return new LocalizedString("ExConversationNotFound", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				conversationId,
				conversationFamilyId
			});
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00061038 File Offset: 0x0005F238
		public static LocalizedString ErrorNotSupportedLanguageWithInstalledLanguagePack(string lang)
		{
			return new LocalizedString("ErrorNotSupportedLanguageWithInstalledLanguagePack", "Ex28B323", false, true, ServerStrings.ResourceManager, new object[]
			{
				lang
			});
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00061067 File Offset: 0x0005F267
		public static LocalizedString MapiCannotGetAttachmentTable
		{
			get
			{
				return new LocalizedString("MapiCannotGetAttachmentTable", "Ex76BFC6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x00061088 File Offset: 0x0005F288
		public static LocalizedString ErrorInPlaceHoldIdentityChanged(string name)
		{
			return new LocalizedString("ErrorInPlaceHoldIdentityChanged", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x000610B7 File Offset: 0x0005F2B7
		public static LocalizedString MapiCannotOpenAttachment
		{
			get
			{
				return new LocalizedString("MapiCannotOpenAttachment", "Ex18E168", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x000610D5 File Offset: 0x0005F2D5
		public static LocalizedString ExSuffixTextFilterNotSupported
		{
			get
			{
				return new LocalizedString("ExSuffixTextFilterNotSupported", "Ex27263F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x000610F3 File Offset: 0x0005F2F3
		public static LocalizedString ExSeparatorNotFoundOnCompoundValue
		{
			get
			{
				return new LocalizedString("ExSeparatorNotFoundOnCompoundValue", "Ex07AA84", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00061111 File Offset: 0x0005F311
		public static LocalizedString MigrationBatchStatusCorrupted
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusCorrupted", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0006112F File Offset: 0x0005F32F
		public static LocalizedString MigrationBatchStatusSyncing
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusSyncing", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00061150 File Offset: 0x0005F350
		public static LocalizedString InvalidSharingDataException(string name, string value)
		{
			return new LocalizedString("InvalidSharingDataException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00061183 File Offset: 0x0005F383
		public static LocalizedString ClientCulture_0x415
		{
			get
			{
				return new LocalizedString("ClientCulture_0x415", "Ex664B26", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x000611A4 File Offset: 0x0005F3A4
		public static LocalizedString MigrationOrganizationNotFound(string mailboxName)
		{
			return new LocalizedString("MigrationOrganizationNotFound", "ExA992C3", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailboxName
			});
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x000611D4 File Offset: 0x0005F3D4
		public static LocalizedString AmDbMountNotAllowedDueToAcllErrorException(string errMessage, long numLogsLost)
		{
			return new LocalizedString("AmDbMountNotAllowedDueToAcllErrorException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				errMessage,
				numLogsLost
			});
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0006120C File Offset: 0x0005F40C
		public static LocalizedString ClientCulture_0x2C01
		{
			get
			{
				return new LocalizedString("ClientCulture_0x2C01", "Ex25C35D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0006122C File Offset: 0x0005F42C
		public static LocalizedString PublicFolderSyncFolderHierarchyFailedAfterMultipleAttempts(int attempts, string innerMessage)
		{
			return new LocalizedString("PublicFolderSyncFolderHierarchyFailedAfterMultipleAttempts", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				attempts,
				innerMessage
			});
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x00061264 File Offset: 0x0005F464
		public static LocalizedString CannotAccessRemoteMailbox
		{
			get
			{
				return new LocalizedString("CannotAccessRemoteMailbox", "Ex0052F2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x00061282 File Offset: 0x0005F482
		public static LocalizedString MapiCannotFindRow
		{
			get
			{
				return new LocalizedString("MapiCannotFindRow", "ExBD9DCF", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x000612A0 File Offset: 0x0005F4A0
		public static LocalizedString NoInternalEwsAvailableException(string mailbox)
		{
			return new LocalizedString("NoInternalEwsAvailableException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x000612D0 File Offset: 0x0005F4D0
		public static LocalizedString AmInvalidConfiguration(string error)
		{
			return new LocalizedString("AmInvalidConfiguration", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x000612FF File Offset: 0x0005F4FF
		public static LocalizedString ThirtyMinutes
		{
			get
			{
				return new LocalizedString("ThirtyMinutes", "Ex1AC83C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0006131D File Offset: 0x0005F51D
		public static LocalizedString MapiCannotSeekRow
		{
			get
			{
				return new LocalizedString("MapiCannotSeekRow", "ExD55968", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0006133B File Offset: 0x0005F53B
		public static LocalizedString MigrationUserStatusFailed
		{
			get
			{
				return new LocalizedString("MigrationUserStatusFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00061359 File Offset: 0x0005F559
		public static LocalizedString ExceptionObjectHasBeenDeleted
		{
			get
			{
				return new LocalizedString("ExceptionObjectHasBeenDeleted", "ExBF3124", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00061377 File Offset: 0x0005F577
		public static LocalizedString MigrationBatchFlagDisallowExistingUsers
		{
			get
			{
				return new LocalizedString("MigrationBatchFlagDisallowExistingUsers", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x00061395 File Offset: 0x0005F595
		public static LocalizedString ClientCulture_0x464
		{
			get
			{
				return new LocalizedString("ClientCulture_0x464", "Ex737240", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x000613B4 File Offset: 0x0005F5B4
		public static LocalizedString InvalidLocalDirectorySecurityIdentifier(string sid)
		{
			return new LocalizedString("InvalidLocalDirectorySecurityIdentifier", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				sid
			});
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x000613E4 File Offset: 0x0005F5E4
		public static LocalizedString CalendarOriginatorIdCorrupt(string calendarOriginatorId)
		{
			return new LocalizedString("CalendarOriginatorIdCorrupt", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				calendarOriginatorId
			});
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x00061413 File Offset: 0x0005F613
		public static LocalizedString UnsupportedPropertyRestriction
		{
			get
			{
				return new LocalizedString("UnsupportedPropertyRestriction", "ExFDBD2B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00061434 File Offset: 0x0005F634
		public static LocalizedString AppointmentActionNotSupported(string action)
		{
			return new LocalizedString("AppointmentActionNotSupported", "Ex3BD3B7", false, true, ServerStrings.ResourceManager, new object[]
			{
				action
			});
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x00061464 File Offset: 0x0005F664
		public static LocalizedString TooManyActiveManagerClientRPCs(int maximum)
		{
			return new LocalizedString("TooManyActiveManagerClientRPCs", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				maximum
			});
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00061498 File Offset: 0x0005F698
		public static LocalizedString RightsNotAllowedByPolicy(string storeObjectType, string folderName)
		{
			return new LocalizedString("RightsNotAllowedByPolicy", "ExAD7A50", false, true, ServerStrings.ResourceManager, new object[]
			{
				storeObjectType,
				folderName
			});
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x000614CB File Offset: 0x0005F6CB
		public static LocalizedString ServerLocatorClientWCFCallTimeout
		{
			get
			{
				return new LocalizedString("ServerLocatorClientWCFCallTimeout", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x000614E9 File Offset: 0x0005F6E9
		public static LocalizedString InvalidServiceLocationResponse
		{
			get
			{
				return new LocalizedString("InvalidServiceLocationResponse", "Ex54D399", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00061507 File Offset: 0x0005F707
		public static LocalizedString MapiCannotDeleteProperties
		{
			get
			{
				return new LocalizedString("MapiCannotDeleteProperties", "Ex108358", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00061525 File Offset: 0x0005F725
		public static LocalizedString NeedFolderIdForPublicFolder
		{
			get
			{
				return new LocalizedString("NeedFolderIdForPublicFolder", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00061544 File Offset: 0x0005F744
		public static LocalizedString AmServerNotFoundToVerifyRpcVersion(string serverName)
		{
			return new LocalizedString("AmServerNotFoundToVerifyRpcVersion", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00061574 File Offset: 0x0005F774
		public static LocalizedString TaskOperationFailedException(string errMessage)
		{
			return new LocalizedString("TaskOperationFailedException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				errMessage
			});
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x000615A3 File Offset: 0x0005F7A3
		public static LocalizedString ClientCulture_0x100C
		{
			get
			{
				return new LocalizedString("ClientCulture_0x100C", "Ex91D09B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000615C4 File Offset: 0x0005F7C4
		public static LocalizedString ConversionMaxRecipientExceeded(int maxrecipients)
		{
			return new LocalizedString("ConversionMaxRecipientExceeded", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				maxrecipients
			});
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x000615F8 File Offset: 0x0005F7F8
		public static LocalizedString ManagedByRemoteExchangeOrganization
		{
			get
			{
				return new LocalizedString("ManagedByRemoteExchangeOrganization", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x00061616 File Offset: 0x0005F816
		public static LocalizedString AutoDRequestFailed
		{
			get
			{
				return new LocalizedString("AutoDRequestFailed", "ExAFA011", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00061634 File Offset: 0x0005F834
		public static LocalizedString NoSharingHandlerFoundException(string recipient)
		{
			return new LocalizedString("NoSharingHandlerFoundException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				recipient
			});
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x00061663 File Offset: 0x0005F863
		public static LocalizedString DumpsterFolderNotFound
		{
			get
			{
				return new LocalizedString("DumpsterFolderNotFound", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00061681 File Offset: 0x0005F881
		public static LocalizedString ExFolderNotFoundInClientState
		{
			get
			{
				return new LocalizedString("ExFolderNotFoundInClientState", "ExE84B6E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x000616A0 File Offset: 0x0005F8A0
		public static LocalizedString ExternalUserNotFound(string sid)
		{
			return new LocalizedString("ExternalUserNotFound", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				sid
			});
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x000616D0 File Offset: 0x0005F8D0
		public static LocalizedString ExTenantAccessBlocked(string organizationId)
		{
			return new LocalizedString("ExTenantAccessBlocked", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				organizationId
			});
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x000616FF File Offset: 0x0005F8FF
		public static LocalizedString ImportResultContainedFailure
		{
			get
			{
				return new LocalizedString("ImportResultContainedFailure", "Ex08D54F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0006171D File Offset: 0x0005F91D
		public static LocalizedString ClientCulture_0x813
		{
			get
			{
				return new LocalizedString("ClientCulture_0x813", "ExAF5FAA", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0006173B File Offset: 0x0005F93B
		public static LocalizedString ExCannotCreateMeetingResponse
		{
			get
			{
				return new LocalizedString("ExCannotCreateMeetingResponse", "ExC172FE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00061759 File Offset: 0x0005F959
		public static LocalizedString EightHours
		{
			get
			{
				return new LocalizedString("EightHours", "Ex4ECD5D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00061778 File Offset: 0x0005F978
		public static LocalizedString ExUnresolvedRecipient(string recipientDisplayName)
		{
			return new LocalizedString("ExUnresolvedRecipient", "Ex72B03F", false, true, ServerStrings.ResourceManager, new object[]
			{
				recipientDisplayName
			});
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x000617A8 File Offset: 0x0005F9A8
		public static LocalizedString ExInvalidAttachmentId(string idbytes)
		{
			return new LocalizedString("ExInvalidAttachmentId", "Ex64971C", false, true, ServerStrings.ResourceManager, new object[]
			{
				idbytes
			});
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x000617D7 File Offset: 0x0005F9D7
		public static LocalizedString OperationResultFailed
		{
			get
			{
				return new LocalizedString("OperationResultFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x000617F5 File Offset: 0x0005F9F5
		public static LocalizedString ErrorWorkingHoursEndTimeSmaller
		{
			get
			{
				return new LocalizedString("ErrorWorkingHoursEndTimeSmaller", "Ex212A1E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00061814 File Offset: 0x0005FA14
		public static LocalizedString ErrorFolderAlreadyExists(string name)
		{
			return new LocalizedString("ErrorFolderAlreadyExists", "Ex4C0E4F", false, true, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00061843 File Offset: 0x0005FA43
		public static LocalizedString RoutingTypeRequired
		{
			get
			{
				return new LocalizedString("RoutingTypeRequired", "Ex0879F8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00061864 File Offset: 0x0005FA64
		public static LocalizedString InvalidAddressFormat(string routingType, string address)
		{
			return new LocalizedString("InvalidAddressFormat", "Ex46618E", false, true, ServerStrings.ResourceManager, new object[]
			{
				routingType,
				address
			});
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00061898 File Offset: 0x0005FA98
		public static LocalizedString ExOccurrenceNotPresent(object occId)
		{
			return new LocalizedString("ExOccurrenceNotPresent", "Ex593F3C", false, true, ServerStrings.ResourceManager, new object[]
			{
				occId
			});
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x000618C7 File Offset: 0x0005FAC7
		public static LocalizedString FolderRuleCannotSaveItem
		{
			get
			{
				return new LocalizedString("FolderRuleCannotSaveItem", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x000618E5 File Offset: 0x0005FAE5
		public static LocalizedString RequestStateRemoving
		{
			get
			{
				return new LocalizedString("RequestStateRemoving", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00061903 File Offset: 0x0005FB03
		public static LocalizedString MigrationStateFailed
		{
			get
			{
				return new LocalizedString("MigrationStateFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00061921 File Offset: 0x0005FB21
		public static LocalizedString MigrationUserStatusCompletionFailed
		{
			get
			{
				return new LocalizedString("MigrationUserStatusCompletionFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0006193F File Offset: 0x0005FB3F
		public static LocalizedString MailboxSearchEwsEmptyResponse
		{
			get
			{
				return new LocalizedString("MailboxSearchEwsEmptyResponse", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00061960 File Offset: 0x0005FB60
		public static LocalizedString ExComparisonOperatorNotSupported(object relOp)
		{
			return new LocalizedString("ExComparisonOperatorNotSupported", "ExAF2A42", false, true, ServerStrings.ResourceManager, new object[]
			{
				relOp
			});
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x0006198F File Offset: 0x0005FB8F
		public static LocalizedString ClientCulture_0x140A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x140A", "Ex2EA3A8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x000619AD File Offset: 0x0005FBAD
		public static LocalizedString MigrationUserStatusRemoving
		{
			get
			{
				return new LocalizedString("MigrationUserStatusRemoving", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x000619CB File Offset: 0x0005FBCB
		public static LocalizedString MigrationFolderCorruptedItems
		{
			get
			{
				return new LocalizedString("MigrationFolderCorruptedItems", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x000619E9 File Offset: 0x0005FBE9
		public static LocalizedString ClientCulture_0x418
		{
			get
			{
				return new LocalizedString("ClientCulture_0x418", "Ex22C242", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00061A07 File Offset: 0x0005FC07
		public static LocalizedString SpellCheckerPortuguesePortugal
		{
			get
			{
				return new LocalizedString("SpellCheckerPortuguesePortugal", "Ex6F826C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00061A25 File Offset: 0x0005FC25
		public static LocalizedString TeamMailboxMessageReactivatingText
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageReactivatingText", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x00061A43 File Offset: 0x0005FC43
		public static LocalizedString SearchLogFileCreateException
		{
			get
			{
				return new LocalizedString("SearchLogFileCreateException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x00061A61 File Offset: 0x0005FC61
		public static LocalizedString ExCannotGetDeletedItem
		{
			get
			{
				return new LocalizedString("ExCannotGetDeletedItem", "Ex025082", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00061A7F File Offset: 0x0005FC7F
		public static LocalizedString MailboxSearchNameTooLong
		{
			get
			{
				return new LocalizedString("MailboxSearchNameTooLong", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00061AA0 File Offset: 0x0005FCA0
		public static LocalizedString ExternalLicensingDisabledForTenant(Uri uri, OrganizationId orgId)
		{
			return new LocalizedString("ExternalLicensingDisabledForTenant", "Ex6AA711", false, true, ServerStrings.ResourceManager, new object[]
			{
				uri,
				orgId
			});
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00061AD3 File Offset: 0x0005FCD3
		public static LocalizedString ClientCulture_0x41F
		{
			get
			{
				return new LocalizedString("ClientCulture_0x41F", "Ex3A6328", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00061AF1 File Offset: 0x0005FCF1
		public static LocalizedString ClientCulture_0x4409
		{
			get
			{
				return new LocalizedString("ClientCulture_0x4409", "Ex045C19", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00061B0F File Offset: 0x0005FD0F
		public static LocalizedString ExSubmissionQuotaExceeded
		{
			get
			{
				return new LocalizedString("ExSubmissionQuotaExceeded", "Ex6D2B2E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00061B2D File Offset: 0x0005FD2D
		public static LocalizedString ExCorruptMessageCorrelationBlob
		{
			get
			{
				return new LocalizedString("ExCorruptMessageCorrelationBlob", "Ex4AB049", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x00061B4B File Offset: 0x0005FD4B
		public static LocalizedString MigrationFolderDrumTesting
		{
			get
			{
				return new LocalizedString("MigrationFolderDrumTesting", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00061B6C File Offset: 0x0005FD6C
		public static LocalizedString CantParseParticipant(string inputString)
		{
			return new LocalizedString("CantParseParticipant", "ExEEB2A9", false, true, ServerStrings.ResourceManager, new object[]
			{
				inputString
			});
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00061B9C File Offset: 0x0005FD9C
		public static LocalizedString ErrorFolderSave(string id, string details)
		{
			return new LocalizedString("ErrorFolderSave", "Ex2CCC6C", false, true, ServerStrings.ResourceManager, new object[]
			{
				id,
				details
			});
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00061BCF File Offset: 0x0005FDCF
		public static LocalizedString ExCorruptFolderWebViewInfo
		{
			get
			{
				return new LocalizedString("ExCorruptFolderWebViewInfo", "Ex799C65", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00061BED File Offset: 0x0005FDED
		public static LocalizedString MigrationBatchFlagDisableOnCopy
		{
			get
			{
				return new LocalizedString("MigrationBatchFlagDisableOnCopy", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00061C0C File Offset: 0x0005FE0C
		public static LocalizedString ExMailboxAccessDenied(string owner, string accessingUser)
		{
			return new LocalizedString("ExMailboxAccessDenied", "Ex711EFB", false, true, ServerStrings.ResourceManager, new object[]
			{
				owner,
				accessingUser
			});
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00061C3F File Offset: 0x0005FE3F
		public static LocalizedString ICSSynchronizationFailed
		{
			get
			{
				return new LocalizedString("ICSSynchronizationFailed", "Ex69CDC7", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00061C60 File Offset: 0x0005FE60
		public static LocalizedString ExInvalidTypeInBlob(object blobType)
		{
			return new LocalizedString("ExInvalidTypeInBlob", "ExACB26B", false, true, ServerStrings.ResourceManager, new object[]
			{
				blobType
			});
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00061C8F File Offset: 0x0005FE8F
		public static LocalizedString OneHours
		{
			get
			{
				return new LocalizedString("OneHours", "ExEFAC6F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00061CB0 File Offset: 0x0005FEB0
		public static LocalizedString PromoteVeventFailure(string uid)
		{
			return new LocalizedString("PromoteVeventFailure", "ExBDC768", false, true, ServerStrings.ResourceManager, new object[]
			{
				uid
			});
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00061CDF File Offset: 0x0005FEDF
		public static LocalizedString InvalidBodyFormat
		{
			get
			{
				return new LocalizedString("InvalidBodyFormat", "Ex9E084B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00061D00 File Offset: 0x0005FF00
		public static LocalizedString FolderRuleErrorRecord(string recipient, string stage, string exceptionType, string exceptionMessage)
		{
			return new LocalizedString("FolderRuleErrorRecord", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				recipient,
				stage,
				exceptionType,
				exceptionMessage
			});
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00061D3B File Offset: 0x0005FF3B
		public static LocalizedString PeopleQuickContactsAttributionDisplayName
		{
			get
			{
				return new LocalizedString("PeopleQuickContactsAttributionDisplayName", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00061D59 File Offset: 0x0005FF59
		public static LocalizedString TwoHours
		{
			get
			{
				return new LocalizedString("TwoHours", "Ex72977C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00061D77 File Offset: 0x0005FF77
		public static LocalizedString ExPropertyDefinitionInMoreThanOnePropertyProfile
		{
			get
			{
				return new LocalizedString("ExPropertyDefinitionInMoreThanOnePropertyProfile", "Ex01B729", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00061D95 File Offset: 0x0005FF95
		public static LocalizedString TeamMailboxSyncStatusMembershipAndMaintenanceSyncFailure
		{
			get
			{
				return new LocalizedString("TeamMailboxSyncStatusMembershipAndMaintenanceSyncFailure", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00061DB3 File Offset: 0x0005FFB3
		public static LocalizedString ClientCulture_0xC0C
		{
			get
			{
				return new LocalizedString("ClientCulture_0xC0C", "Ex5441E2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00061DD1 File Offset: 0x0005FFD1
		public static LocalizedString ExUnableToCopyAttachments
		{
			get
			{
				return new LocalizedString("ExUnableToCopyAttachments", "ExB42355", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x00061DEF File Offset: 0x0005FFEF
		public static LocalizedString ExCannotUpdateResponses
		{
			get
			{
				return new LocalizedString("ExCannotUpdateResponses", "Ex9D4D4C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00061E0D File Offset: 0x0006000D
		public static LocalizedString ConversationItemHasNoBody
		{
			get
			{
				return new LocalizedString("ConversationItemHasNoBody", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00061E2B File Offset: 0x0006002B
		public static LocalizedString DelegateCollectionInvalidAfterSave
		{
			get
			{
				return new LocalizedString("DelegateCollectionInvalidAfterSave", "Ex6D9AC2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00061E49 File Offset: 0x00060049
		public static LocalizedString TeamMailboxSyncStatusDocumentAndMaintenanceSyncFailure
		{
			get
			{
				return new LocalizedString("TeamMailboxSyncStatusDocumentAndMaintenanceSyncFailure", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00061E67 File Offset: 0x00060067
		public static LocalizedString InvalidAttachmentType
		{
			get
			{
				return new LocalizedString("InvalidAttachmentType", "Ex0ABEE6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00061E85 File Offset: 0x00060085
		public static LocalizedString ExCannotMarkTaskCompletedWhenSuppressCreateOneOff
		{
			get
			{
				return new LocalizedString("ExCannotMarkTaskCompletedWhenSuppressCreateOneOff", "Ex4A8040", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00061EA3 File Offset: 0x000600A3
		public static LocalizedString CannotGetPropertyList
		{
			get
			{
				return new LocalizedString("CannotGetPropertyList", "Ex4CCA0F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00061EC1 File Offset: 0x000600C1
		public static LocalizedString ErrorInvalidConfigurationXml
		{
			get
			{
				return new LocalizedString("ErrorInvalidConfigurationXml", "ExBF50D3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00061EE0 File Offset: 0x000600E0
		public static LocalizedString MalformedTimeZoneWorkingHours(string mailbox, string exceptionInfo)
		{
			return new LocalizedString("MalformedTimeZoneWorkingHours", "Ex90FBB2", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailbox,
				exceptionInfo
			});
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00061F13 File Offset: 0x00060113
		public static LocalizedString InvalidBase64String
		{
			get
			{
				return new LocalizedString("InvalidBase64String", "ExFE7FAA", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00061F31 File Offset: 0x00060131
		public static LocalizedString RequestStateFailed
		{
			get
			{
				return new LocalizedString("RequestStateFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00061F50 File Offset: 0x00060150
		public static LocalizedString ExInvalidFileTimeInRecurrenceBlob(int fileTime)
		{
			return new LocalizedString("ExInvalidFileTimeInRecurrenceBlob", "Ex70AF44", false, true, ServerStrings.ResourceManager, new object[]
			{
				fileTime
			});
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00061F84 File Offset: 0x00060184
		public static LocalizedString InboxRuleImportanceNormal
		{
			get
			{
				return new LocalizedString("InboxRuleImportanceNormal", "Ex11ADED", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00061FA2 File Offset: 0x000601A2
		public static LocalizedString MigrationLocalhostNotFound
		{
			get
			{
				return new LocalizedString("MigrationLocalhostNotFound", "Ex822317", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00061FC0 File Offset: 0x000601C0
		public static LocalizedString ClientCulture_0x1401
		{
			get
			{
				return new LocalizedString("ClientCulture_0x1401", "Ex9DAD09", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00061FE0 File Offset: 0x000601E0
		public static LocalizedString ErrorInvalidInPlaceHoldIdentity(string name)
		{
			return new LocalizedString("ErrorInvalidInPlaceHoldIdentity", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00062010 File Offset: 0x00060210
		public static LocalizedString ExBodyConversionNotSupportedType(string format)
		{
			return new LocalizedString("ExBodyConversionNotSupportedType", "Ex8314F4", false, true, ServerStrings.ResourceManager, new object[]
			{
				format
			});
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0006203F File Offset: 0x0006023F
		public static LocalizedString TeamMailboxSyncStatusSucceeded
		{
			get
			{
				return new LocalizedString("TeamMailboxSyncStatusSucceeded", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0006205D File Offset: 0x0006025D
		public static LocalizedString MigrationErrorAttachmentCorrupted
		{
			get
			{
				return new LocalizedString("MigrationErrorAttachmentCorrupted", "Ex43FBE7", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0006207B File Offset: 0x0006027B
		public static LocalizedString OleConversionResultFailed
		{
			get
			{
				return new LocalizedString("OleConversionResultFailed", "Ex79242F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00062099 File Offset: 0x00060299
		public static LocalizedString MigrationUserStatusSummaryFailed
		{
			get
			{
				return new LocalizedString("MigrationUserStatusSummaryFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x000620B7 File Offset: 0x000602B7
		public static LocalizedString RequestStateCanceled
		{
			get
			{
				return new LocalizedString("RequestStateCanceled", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x000620D5 File Offset: 0x000602D5
		public static LocalizedString ModifyRuleInStore
		{
			get
			{
				return new LocalizedString("ModifyRuleInStore", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x000620F3 File Offset: 0x000602F3
		public static LocalizedString ExItemDeletedInRace
		{
			get
			{
				return new LocalizedString("ExItemDeletedInRace", "Ex52D036", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00062111 File Offset: 0x00060311
		public static LocalizedString ClientCulture_0x340A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x340A", "ExDE1E5F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0006212F File Offset: 0x0006032F
		public static LocalizedString WeatherUnitFahrenheit
		{
			get
			{
				return new LocalizedString("WeatherUnitFahrenheit", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x0006214D File Offset: 0x0006034D
		public static LocalizedString MessageNotRightsProtected
		{
			get
			{
				return new LocalizedString("MessageNotRightsProtected", "ExA28CD3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0006216C File Offset: 0x0006036C
		public static LocalizedString SaveConfigurationItem(string mailbox, string exceptionInfo)
		{
			return new LocalizedString("SaveConfigurationItem", "Ex0280B6", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailbox,
				exceptionInfo
			});
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x0006219F File Offset: 0x0006039F
		public static LocalizedString ConversionMaliciousContent
		{
			get
			{
				return new LocalizedString("ConversionMaliciousContent", "Ex9CCF09", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x000621C0 File Offset: 0x000603C0
		public static LocalizedString RpcServerUnhandledException(string error)
		{
			return new LocalizedString("RpcServerUnhandledException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x000621F0 File Offset: 0x000603F0
		public static LocalizedString TaskInvalidFlagStatus(int flagStatus, int taskStatus)
		{
			return new LocalizedString("TaskInvalidFlagStatus", "Ex8839D8", false, true, ServerStrings.ResourceManager, new object[]
			{
				flagStatus,
				taskStatus
			});
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00062230 File Offset: 0x00060430
		public static LocalizedString RpcServerParameterSerializationError(string error)
		{
			return new LocalizedString("RpcServerParameterSerializationError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x0006225F File Offset: 0x0006045F
		public static LocalizedString NoTemplateMessage
		{
			get
			{
				return new LocalizedString("NoTemplateMessage", "ExA6872D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00062280 File Offset: 0x00060480
		public static LocalizedString ExTimeInExtendedInfoNotSameAsExceptionInfo(string timeType, object extendedInfoTime, object exceptionInfoTime)
		{
			return new LocalizedString("ExTimeInExtendedInfoNotSameAsExceptionInfo", "Ex2F385A", false, true, ServerStrings.ResourceManager, new object[]
			{
				timeType,
				extendedInfoTime,
				exceptionInfoTime
			});
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000622B8 File Offset: 0x000604B8
		public static LocalizedString FailedToGetRmsTemplates(string tenantId)
		{
			return new LocalizedString("FailedToGetRmsTemplates", "Ex36B628", false, true, ServerStrings.ResourceManager, new object[]
			{
				tenantId
			});
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x000622E7 File Offset: 0x000604E7
		public static LocalizedString FolderRuleStageLoading
		{
			get
			{
				return new LocalizedString("FolderRuleStageLoading", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00062308 File Offset: 0x00060508
		public static LocalizedString ExInvalidFolderType(int type)
		{
			return new LocalizedString("ExInvalidFolderType", "Ex0C8624", false, true, ServerStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0006233C File Offset: 0x0006053C
		public static LocalizedString LimitedDetails
		{
			get
			{
				return new LocalizedString("LimitedDetails", "Ex1F2397", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0006235A File Offset: 0x0006055A
		public static LocalizedString AppendOOFHistoryEntry
		{
			get
			{
				return new LocalizedString("AppendOOFHistoryEntry", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00062378 File Offset: 0x00060578
		public static LocalizedString ExItemNotFoundInConversation(object itemId, object conversationId)
		{
			return new LocalizedString("ExItemNotFoundInConversation", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				itemId,
				conversationId
			});
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x000623AC File Offset: 0x000605AC
		public static LocalizedString ExInvalidMinutePeriod(int period)
		{
			return new LocalizedString("ExInvalidMinutePeriod", "Ex51B4FA", false, true, ServerStrings.ResourceManager, new object[]
			{
				period
			});
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x000623E0 File Offset: 0x000605E0
		public static LocalizedString ExStoreSessionDisconnected
		{
			get
			{
				return new LocalizedString("ExStoreSessionDisconnected", "Ex8978DF", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00062400 File Offset: 0x00060600
		public static LocalizedString ConversionMaxRecipientSizeExceeded(int maxsize, string truncatedPart)
		{
			return new LocalizedString("ConversionMaxRecipientSizeExceeded", "ExE93799", false, true, ServerStrings.ResourceManager, new object[]
			{
				maxsize,
				truncatedPart
			});
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00062438 File Offset: 0x00060638
		public static LocalizedString ErrorInvalidStatisticsStartIndex(string name)
		{
			return new LocalizedString("ErrorInvalidStatisticsStartIndex", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x00062467 File Offset: 0x00060667
		public static LocalizedString NotificationEmailSubjectCertExpired
		{
			get
			{
				return new LocalizedString("NotificationEmailSubjectCertExpired", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00062485 File Offset: 0x00060685
		public static LocalizedString MigrationUserAdminTypeDCAdmin
		{
			get
			{
				return new LocalizedString("MigrationUserAdminTypeDCAdmin", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x000624A3 File Offset: 0x000606A3
		public static LocalizedString SpellCheckerSpanish
		{
			get
			{
				return new LocalizedString("SpellCheckerSpanish", "ExDA06B6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x000624C1 File Offset: 0x000606C1
		public static LocalizedString UserPhotoNotFound
		{
			get
			{
				return new LocalizedString("UserPhotoNotFound", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x000624E0 File Offset: 0x000606E0
		public static LocalizedString MigrationInvalidStatus(string statusType, string status)
		{
			return new LocalizedString("MigrationInvalidStatus", "Ex5AC1ED", false, true, ServerStrings.ResourceManager, new object[]
			{
				statusType,
				status
			});
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00062514 File Offset: 0x00060714
		public static LocalizedString QueryUsageRightsNoPrelicenseResponse(string uri)
		{
			return new LocalizedString("QueryUsageRightsNoPrelicenseResponse", "ExE9FCED", false, true, ServerStrings.ResourceManager, new object[]
			{
				uri
			});
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x00062543 File Offset: 0x00060743
		public static LocalizedString ClientCulture_0x1C01
		{
			get
			{
				return new LocalizedString("ClientCulture_0x1C01", "ExCB8B3D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x00062561 File Offset: 0x00060761
		public static LocalizedString MigrationBatchFlagAutoStop
		{
			get
			{
				return new LocalizedString("MigrationBatchFlagAutoStop", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x0006257F File Offset: 0x0006077F
		public static LocalizedString RemoteArchiveOffline
		{
			get
			{
				return new LocalizedString("RemoteArchiveOffline", "Ex21F3BA", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x0006259D File Offset: 0x0006079D
		public static LocalizedString CannotOpenLocalFreeBusy
		{
			get
			{
				return new LocalizedString("CannotOpenLocalFreeBusy", "ExBEB5DD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x000625BC File Offset: 0x000607BC
		public static LocalizedString ExBatchBuilderNeedsPropertyToConvertRT(PropertyDefinition propertyDefinition, string currentRoutingType, string destinationRoutingType, string participant)
		{
			return new LocalizedString("ExBatchBuilderNeedsPropertyToConvertRT", "Ex95821B", false, true, ServerStrings.ResourceManager, new object[]
			{
				propertyDefinition,
				currentRoutingType,
				destinationRoutingType,
				participant
			});
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x000625F7 File Offset: 0x000607F7
		public static LocalizedString CannotFindExchangePrincipal
		{
			get
			{
				return new LocalizedString("CannotFindExchangePrincipal", "Ex47964E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00062618 File Offset: 0x00060818
		public static LocalizedString NonUniqueRecipientByExternalIdError(string externalDirectoryObjectId)
		{
			return new LocalizedString("NonUniqueRecipientByExternalIdError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				externalDirectoryObjectId
			});
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x00062647 File Offset: 0x00060847
		public static LocalizedString MigrationStageValidation
		{
			get
			{
				return new LocalizedString("MigrationStageValidation", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x00062665 File Offset: 0x00060865
		public static LocalizedString CalNotifTypeReminder
		{
			get
			{
				return new LocalizedString("CalNotifTypeReminder", "ExA22CD2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x00062683 File Offset: 0x00060883
		public static LocalizedString ExFailedToGetUnsearchableItems
		{
			get
			{
				return new LocalizedString("ExFailedToGetUnsearchableItems", "Ex8CCF32", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x000626A1 File Offset: 0x000608A1
		public static LocalizedString Monday
		{
			get
			{
				return new LocalizedString("Monday", "ExF95A69", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x000626BF File Offset: 0x000608BF
		public static LocalizedString AsyncOperationTypeUnknown
		{
			get
			{
				return new LocalizedString("AsyncOperationTypeUnknown", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x000626DD File Offset: 0x000608DD
		public static LocalizedString MigrationFolderSyncMigration
		{
			get
			{
				return new LocalizedString("MigrationFolderSyncMigration", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x000626FB File Offset: 0x000608FB
		public static LocalizedString InboxRuleFlagStatusComplete
		{
			get
			{
				return new LocalizedString("InboxRuleFlagStatusComplete", "Ex55D3AD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x00062719 File Offset: 0x00060919
		public static LocalizedString NotificationEmailSubjectMoveMailbox
		{
			get
			{
				return new LocalizedString("NotificationEmailSubjectMoveMailbox", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x00062737 File Offset: 0x00060937
		public static LocalizedString MessageRpmsgAttachmentIncorrectType
		{
			get
			{
				return new LocalizedString("MessageRpmsgAttachmentIncorrectType", "ExC052B6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00062758 File Offset: 0x00060958
		public static LocalizedString MultipleProvisioningMailboxes(string mailboxId)
		{
			return new LocalizedString("MultipleProvisioningMailboxes", "ExBB7B8C", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailboxId
			});
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00062788 File Offset: 0x00060988
		public static LocalizedString ExMailboxMustBeAccessedAsOwner(string owner, string accessingUser)
		{
			return new LocalizedString("ExMailboxMustBeAccessedAsOwner", "Ex3A00A8", false, true, ServerStrings.ResourceManager, new object[]
			{
				owner,
				accessingUser
			});
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x000627BC File Offset: 0x000609BC
		public static LocalizedString CannotCreateOscFolderBecauseOfConflict(string provider)
		{
			return new LocalizedString("CannotCreateOscFolderBecauseOfConflict", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				provider
			});
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x000627EB File Offset: 0x000609EB
		public static LocalizedString FullDetails
		{
			get
			{
				return new LocalizedString("FullDetails", "Ex469BDC", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00062809 File Offset: 0x00060A09
		public static LocalizedString JunkEmailObjectDisposedException
		{
			get
			{
				return new LocalizedString("JunkEmailObjectDisposedException", "ExAFF31F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x00062827 File Offset: 0x00060A27
		public static LocalizedString FailedToReadLocalServer
		{
			get
			{
				return new LocalizedString("FailedToReadLocalServer", "Ex5685A2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x00062845 File Offset: 0x00060A45
		public static LocalizedString MapiCannotGetCurrentRow
		{
			get
			{
				return new LocalizedString("MapiCannotGetCurrentRow", "Ex440EB2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x00062863 File Offset: 0x00060A63
		public static LocalizedString MigrationInvalidPassword
		{
			get
			{
				return new LocalizedString("MigrationInvalidPassword", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00062881 File Offset: 0x00060A81
		public static LocalizedString ExCannotDeletePropertiesOnOccurrences
		{
			get
			{
				return new LocalizedString("ExCannotDeletePropertiesOnOccurrences", "Ex48E90E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x000628A0 File Offset: 0x00060AA0
		public static LocalizedString LicenseUriInvalidForTenant(string uri, string tenantId)
		{
			return new LocalizedString("LicenseUriInvalidForTenant", "ExF3BEA7", false, true, ServerStrings.ResourceManager, new object[]
			{
				uri,
				tenantId
			});
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x000628D4 File Offset: 0x00060AD4
		public static LocalizedString ExConfigNameInvalid(string name)
		{
			return new LocalizedString("ExConfigNameInvalid", "Ex73E46E", false, true, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x00062903 File Offset: 0x00060B03
		public static LocalizedString EstimateStateFailed
		{
			get
			{
				return new LocalizedString("EstimateStateFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00062924 File Offset: 0x00060B24
		public static LocalizedString ExInvalidMultivaluePropertyFilter(string filterType)
		{
			return new LocalizedString("ExInvalidMultivaluePropertyFilter", "ExE1D1F9", false, true, ServerStrings.ResourceManager, new object[]
			{
				filterType
			});
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00062954 File Offset: 0x00060B54
		public static LocalizedString ExInvalidResponseType(object responseType)
		{
			return new LocalizedString("ExInvalidResponseType", "Ex522748", false, true, ServerStrings.ResourceManager, new object[]
			{
				responseType
			});
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00062983 File Offset: 0x00060B83
		public static LocalizedString RequestStateCompleting
		{
			get
			{
				return new LocalizedString("RequestStateCompleting", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x000629A1 File Offset: 0x00060BA1
		public static LocalizedString ClientCulture_0x41E
		{
			get
			{
				return new LocalizedString("ClientCulture_0x41E", "Ex88BB62", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x000629BF File Offset: 0x00060BBF
		public static LocalizedString InvalidSharingRecipientsException
		{
			get
			{
				return new LocalizedString("InvalidSharingRecipientsException", "ExE7475E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x000629DD File Offset: 0x00060BDD
		public static LocalizedString MapiCannotOpenFolder
		{
			get
			{
				return new LocalizedString("MapiCannotOpenFolder", "Ex61964B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x000629FB File Offset: 0x00060BFB
		public static LocalizedString ClientCulture_0x180C
		{
			get
			{
				return new LocalizedString("ClientCulture_0x180C", "Ex68FB65", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00062A1C File Offset: 0x00060C1C
		public static LocalizedString ErrorTeamMailboxSendingNotifications(string ex)
		{
			return new LocalizedString("ErrorTeamMailboxSendingNotifications", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				ex
			});
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00062A4C File Offset: 0x00060C4C
		public static LocalizedString AnonymousPublishingUrlValidationException(string url)
		{
			return new LocalizedString("AnonymousPublishingUrlValidationException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00062A7C File Offset: 0x00060C7C
		public static LocalizedString MapiCannotModifyPropertyTable(string tableName)
		{
			return new LocalizedString("MapiCannotModifyPropertyTable", "Ex4C513F", false, true, ServerStrings.ResourceManager, new object[]
			{
				tableName
			});
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x00062AAB File Offset: 0x00060CAB
		public static LocalizedString ADOperationAbortedBecauseOfAnotherADThread
		{
			get
			{
				return new LocalizedString("ADOperationAbortedBecauseOfAnotherADThread", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00062ACC File Offset: 0x00060CCC
		public static LocalizedString Ex12NotSupportedDeleteItemFlags(int flags)
		{
			return new LocalizedString("Ex12NotSupportedDeleteItemFlags", "Ex4BC6FC", false, true, ServerStrings.ResourceManager, new object[]
			{
				flags
			});
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x00062B00 File Offset: 0x00060D00
		public static LocalizedString UpdateOOFHistoryOperation
		{
			get
			{
				return new LocalizedString("UpdateOOFHistoryOperation", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00062B20 File Offset: 0x00060D20
		public static LocalizedString DisplayNameRequiredForRoutingType(string routingType)
		{
			return new LocalizedString("DisplayNameRequiredForRoutingType", "Ex1AFB71", false, true, ServerStrings.ResourceManager, new object[]
			{
				routingType
			});
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x00062B4F File Offset: 0x00060D4F
		public static LocalizedString ExAttachmentAlreadyOpen
		{
			get
			{
				return new LocalizedString("ExAttachmentAlreadyOpen", "ExE5CFF4", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00062B70 File Offset: 0x00060D70
		public static LocalizedString UserPhotoImageTooSmall(int min)
		{
			return new LocalizedString("UserPhotoImageTooSmall", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				min
			});
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x00062BA4 File Offset: 0x00060DA4
		public static LocalizedString NotificationEmailBodyImportPSTCompleted
		{
			get
			{
				return new LocalizedString("NotificationEmailBodyImportPSTCompleted", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x00062BC2 File Offset: 0x00060DC2
		public static LocalizedString ExInvalidAggregate
		{
			get
			{
				return new LocalizedString("ExInvalidAggregate", "Ex180D68", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x00062BE0 File Offset: 0x00060DE0
		public static LocalizedString NotificationEmailBodyImportPSTFailed
		{
			get
			{
				return new LocalizedString("NotificationEmailBodyImportPSTFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x00062BFE File Offset: 0x00060DFE
		public static LocalizedString ExCantAccessOccurrenceFromSingle
		{
			get
			{
				return new LocalizedString("ExCantAccessOccurrenceFromSingle", "Ex77AC41", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00062C1C File Offset: 0x00060E1C
		public static LocalizedString ProgramError(string reason)
		{
			return new LocalizedString("ProgramError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00062C4C File Offset: 0x00060E4C
		public static LocalizedString ErrorInvalidQuery(string name, string errorMessage)
		{
			return new LocalizedString("ErrorInvalidQuery", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				name,
				errorMessage
			});
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x00062C7F File Offset: 0x00060E7F
		public static LocalizedString NotificationEmailBodyExportPSTCompleted
		{
			get
			{
				return new LocalizedString("NotificationEmailBodyExportPSTCompleted", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00062C9D File Offset: 0x00060E9D
		public static LocalizedString ClientCulture_0x801
		{
			get
			{
				return new LocalizedString("ClientCulture_0x801", "Ex42ADC6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x00062CBB File Offset: 0x00060EBB
		public static LocalizedString ClientCulture_0x401
		{
			get
			{
				return new LocalizedString("ClientCulture_0x401", "Ex345771", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00062CD9 File Offset: 0x00060ED9
		public static LocalizedString CalNotifTypeNewUpdate
		{
			get
			{
				return new LocalizedString("CalNotifTypeNewUpdate", "Ex071F3E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x00062CF7 File Offset: 0x00060EF7
		public static LocalizedString NoExternalEwsAvailableException
		{
			get
			{
				return new LocalizedString("NoExternalEwsAvailableException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x00062D15 File Offset: 0x00060F15
		public static LocalizedString CannotSharePublicFolder
		{
			get
			{
				return new LocalizedString("CannotSharePublicFolder", "ExE3612F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x00062D33 File Offset: 0x00060F33
		public static LocalizedString MigrationTypeExchangeRemoteMove
		{
			get
			{
				return new LocalizedString("MigrationTypeExchangeRemoteMove", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x00062D51 File Offset: 0x00060F51
		public static LocalizedString MigrationUserStatusCompletedWithWarning
		{
			get
			{
				return new LocalizedString("MigrationUserStatusCompletedWithWarning", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00062D70 File Offset: 0x00060F70
		public static LocalizedString EulNotFoundInContainerItem(string filename)
		{
			return new LocalizedString("EulNotFoundInContainerItem", "Ex38EC88", false, true, ServerStrings.ResourceManager, new object[]
			{
				filename
			});
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00062DA0 File Offset: 0x00060FA0
		public static LocalizedString FailedToAquirePublishLicense(string sender)
		{
			return new LocalizedString("FailedToAquirePublishLicense", "ExCAE64F", false, true, ServerStrings.ResourceManager, new object[]
			{
				sender
			});
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x00062DCF File Offset: 0x00060FCF
		public static LocalizedString FailedToParseUseLicense
		{
			get
			{
				return new LocalizedString("FailedToParseUseLicense", "ExFC15EE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x00062DED File Offset: 0x00060FED
		public static LocalizedString MigrationStateDisabled
		{
			get
			{
				return new LocalizedString("MigrationStateDisabled", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x00062E0B File Offset: 0x0006100B
		public static LocalizedString MigrationBatchSupportedActionComplete
		{
			get
			{
				return new LocalizedString("MigrationBatchSupportedActionComplete", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00062E2C File Offset: 0x0006102C
		public static LocalizedString InvalidFolderId(string id)
		{
			return new LocalizedString("InvalidFolderId", "Ex638F1C", false, true, ServerStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x00062E5B File Offset: 0x0006105B
		public static LocalizedString MapiCannotOpenEmbeddedMessage
		{
			get
			{
				return new LocalizedString("MapiCannotOpenEmbeddedMessage", "Ex1F388A", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x00062E79 File Offset: 0x00061079
		public static LocalizedString InboxRuleImportanceLow
		{
			get
			{
				return new LocalizedString("InboxRuleImportanceLow", "ExBEB271", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x00062E97 File Offset: 0x00061097
		public static LocalizedString NoMapiPDLs
		{
			get
			{
				return new LocalizedString("NoMapiPDLs", "ExEABE18", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x00062EB5 File Offset: 0x000610B5
		public static LocalizedString RmExceptionGenericMessage
		{
			get
			{
				return new LocalizedString("RmExceptionGenericMessage", "ExAEE0C0", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00062ED4 File Offset: 0x000610D4
		public static LocalizedString PositiveParameter(string parameter)
		{
			return new LocalizedString("PositiveParameter", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00062F04 File Offset: 0x00061104
		public static LocalizedString RecipientAddressNotSpecifiedForExternalLicensing(Uri uri, string tenantId)
		{
			return new LocalizedString("RecipientAddressNotSpecifiedForExternalLicensing", "Ex41216E", false, true, ServerStrings.ResourceManager, new object[]
			{
				uri,
				tenantId
			});
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x00062F37 File Offset: 0x00061137
		public static LocalizedString NotRead
		{
			get
			{
				return new LocalizedString("NotRead", "Ex1070BB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00062F58 File Offset: 0x00061158
		public static LocalizedString RpcServerRequestSuccess(string server)
		{
			return new LocalizedString("RpcServerRequestSuccess", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00062F88 File Offset: 0x00061188
		public static LocalizedString AdUserNotFoundException(string errMessage)
		{
			return new LocalizedString("AdUserNotFoundException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				errMessage
			});
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x00062FB7 File Offset: 0x000611B7
		public static LocalizedString ClientCulture_0x80C
		{
			get
			{
				return new LocalizedString("ClientCulture_0x80C", "Ex3EB34F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x00062FD5 File Offset: 0x000611D5
		public static LocalizedString idUnableToAddDefaultCalendarToDefaultCalendarGroup
		{
			get
			{
				return new LocalizedString("idUnableToAddDefaultCalendarToDefaultCalendarGroup", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x00062FF3 File Offset: 0x000611F3
		public static LocalizedString ClientCulture_0x3801
		{
			get
			{
				return new LocalizedString("ClientCulture_0x3801", "Ex2CE6DA", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x00063011 File Offset: 0x00061211
		public static LocalizedString MapiCannotGetAllPerUserLongTermIds
		{
			get
			{
				return new LocalizedString("MapiCannotGetAllPerUserLongTermIds", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0006302F File Offset: 0x0006122F
		public static LocalizedString TeamMailboxMessageSiteMailboxEmailAddress
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageSiteMailboxEmailAddress", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0006304D File Offset: 0x0006124D
		public static LocalizedString CalNotifTypeDeletedUpdate
		{
			get
			{
				return new LocalizedString("CalNotifTypeDeletedUpdate", "ExAC6027", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0006306B File Offset: 0x0006126B
		public static LocalizedString CannotCreateSearchFoldersInPublicStore
		{
			get
			{
				return new LocalizedString("CannotCreateSearchFoldersInPublicStore", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x00063089 File Offset: 0x00061289
		public static LocalizedString ExDictionaryDataCorruptedNoField
		{
			get
			{
				return new LocalizedString("ExDictionaryDataCorruptedNoField", "ExAA8571", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000630A8 File Offset: 0x000612A8
		public static LocalizedString idNotSupportedWithServerVersionException(string mailboxId, int mailboxVersion, int serverVersion)
		{
			return new LocalizedString("idNotSupportedWithServerVersionException", "ExA79EBB", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailboxId,
				mailboxVersion,
				serverVersion
			});
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x000630E9 File Offset: 0x000612E9
		public static LocalizedString ExceptionFolderIsRootFolder
		{
			get
			{
				return new LocalizedString("ExceptionFolderIsRootFolder", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x00063107 File Offset: 0x00061307
		public static LocalizedString DateRangeOneDay
		{
			get
			{
				return new LocalizedString("DateRangeOneDay", "ExEEDB71", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00063128 File Offset: 0x00061328
		public static LocalizedString RpcServerStorageError(string mailboxGuid, string error)
		{
			return new LocalizedString("RpcServerStorageError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				mailboxGuid,
				error
			});
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0006315B File Offset: 0x0006135B
		public static LocalizedString ClientCulture_0x412
		{
			get
			{
				return new LocalizedString("ClientCulture_0x412", "ExBA635D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x00063179 File Offset: 0x00061379
		public static LocalizedString AppointmentTombstoneCorrupt
		{
			get
			{
				return new LocalizedString("AppointmentTombstoneCorrupt", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00063198 File Offset: 0x00061398
		public static LocalizedString QueryUsageRightsPrelicenseResponseFailedToExtractRights(string uri)
		{
			return new LocalizedString("QueryUsageRightsPrelicenseResponseFailedToExtractRights", "Ex7733F1", false, true, ServerStrings.ResourceManager, new object[]
			{
				uri
			});
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x000631C7 File Offset: 0x000613C7
		public static LocalizedString MigrationBatchAutoComplete
		{
			get
			{
				return new LocalizedString("MigrationBatchAutoComplete", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000631E8 File Offset: 0x000613E8
		public static LocalizedString UnexpectedTag(string name)
		{
			return new LocalizedString("UnexpectedTag", "ExE31BC4", false, true, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00063218 File Offset: 0x00061418
		public static LocalizedString MigrationPermanentError(string messageDetail)
		{
			return new LocalizedString("MigrationPermanentError", "Ex383EF2", false, true, ServerStrings.ResourceManager, new object[]
			{
				messageDetail
			});
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00063248 File Offset: 0x00061448
		public static LocalizedString CannotShareFolderException(string reason)
		{
			return new LocalizedString("CannotShareFolderException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00063277 File Offset: 0x00061477
		public static LocalizedString MigrationObjectsCountStringNone
		{
			get
			{
				return new LocalizedString("MigrationObjectsCountStringNone", "Ex31A129", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x00063295 File Offset: 0x00061495
		public static LocalizedString ClientCulture_0x810
		{
			get
			{
				return new LocalizedString("ClientCulture_0x810", "ExB1C12B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x000632B4 File Offset: 0x000614B4
		public static LocalizedString ErrorADUserFoundByReadOnlyButNotWrite(string legacyDn)
		{
			return new LocalizedString("ErrorADUserFoundByReadOnlyButNotWrite", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				legacyDn
			});
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x000632E3 File Offset: 0x000614E3
		public static LocalizedString MapiCannotCopyItem
		{
			get
			{
				return new LocalizedString("MapiCannotCopyItem", "Ex0605B8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00063304 File Offset: 0x00061504
		public static LocalizedString ConversionMaxEmbeddedDepthExceeded(int maxdepth)
		{
			return new LocalizedString("ConversionMaxEmbeddedDepthExceeded", "ExD55B41", false, true, ServerStrings.ResourceManager, new object[]
			{
				maxdepth
			});
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00063338 File Offset: 0x00061538
		public static LocalizedString RpcServerWrongRequestServer(string mailboxGuid, string error)
		{
			return new LocalizedString("RpcServerWrongRequestServer", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				mailboxGuid,
				error
			});
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0006336B File Offset: 0x0006156B
		public static LocalizedString ErrorNoStoreObjectIdAndFolderPath
		{
			get
			{
				return new LocalizedString("ErrorNoStoreObjectIdAndFolderPath", "Ex0F85B2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x00063389 File Offset: 0x00061589
		public static LocalizedString MigrationFolderSyncMigrationReports
		{
			get
			{
				return new LocalizedString("MigrationFolderSyncMigrationReports", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x000633A7 File Offset: 0x000615A7
		public static LocalizedString UnsupportedFormsCondition
		{
			get
			{
				return new LocalizedString("UnsupportedFormsCondition", "ExB6B9B6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x000633C5 File Offset: 0x000615C5
		public static LocalizedString ExStartTimeNotSet
		{
			get
			{
				return new LocalizedString("ExStartTimeNotSet", "ExB85FF2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x000633E3 File Offset: 0x000615E3
		public static LocalizedString ClientCulture_0x804
		{
			get
			{
				return new LocalizedString("ClientCulture_0x804", "ExA5B0A9", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00063401 File Offset: 0x00061601
		public static LocalizedString ExConversationActionItemNotFound
		{
			get
			{
				return new LocalizedString("ExConversationActionItemNotFound", "Ex4DA5D6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x0006341F File Offset: 0x0006161F
		public static LocalizedString MigrationUserStatusProvisioning
		{
			get
			{
				return new LocalizedString("MigrationUserStatusProvisioning", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x0006343D File Offset: 0x0006163D
		public static LocalizedString InboxRuleMessageTypeNonDeliveryReport
		{
			get
			{
				return new LocalizedString("InboxRuleMessageTypeNonDeliveryReport", "ExEE9434", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0006345B File Offset: 0x0006165B
		public static LocalizedString ExFailedToUnregisterExchangeTopologyNotification
		{
			get
			{
				return new LocalizedString("ExFailedToUnregisterExchangeTopologyNotification", "ExB79E36", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00063479 File Offset: 0x00061679
		public static LocalizedString TeamMailboxMessageLearnMore
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageLearnMore", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x00063497 File Offset: 0x00061697
		public static LocalizedString DateRangeThreeDays
		{
			get
			{
				return new LocalizedString("DateRangeThreeDays", "Ex6B2F97", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x000634B8 File Offset: 0x000616B8
		public static LocalizedString CantFindCalendarFolderException(object identity)
		{
			return new LocalizedString("CantFindCalendarFolderException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x000634E7 File Offset: 0x000616E7
		public static LocalizedString MapiCannotTransportSendMessage
		{
			get
			{
				return new LocalizedString("MapiCannotTransportSendMessage", "ExE286EB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x00063505 File Offset: 0x00061705
		public static LocalizedString ExSortNotSupportedInDeepTraversalQuery
		{
			get
			{
				return new LocalizedString("ExSortNotSupportedInDeepTraversalQuery", "Ex4C3A26", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x00063523 File Offset: 0x00061723
		public static LocalizedString JunkEmailAmbiguousUsernameException
		{
			get
			{
				return new LocalizedString("JunkEmailAmbiguousUsernameException", "Ex7D436F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x00063541 File Offset: 0x00061741
		public static LocalizedString MigrationBatchStatusSyncedWithErrors
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusSyncedWithErrors", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x00063560 File Offset: 0x00061760
		public static LocalizedString CannotCreateEmbeddedItem(string type)
		{
			return new LocalizedString("CannotCreateEmbeddedItem", "Ex6F46E3", false, true, ServerStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x0006358F File Offset: 0x0006178F
		public static LocalizedString FailedToFindAvailableHubs
		{
			get
			{
				return new LocalizedString("FailedToFindAvailableHubs", "Ex269861", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x000635B0 File Offset: 0x000617B0
		public static LocalizedString ErrorTeamMailboxGetUserMailboxDatabaseFailed(string user, string ex)
		{
			return new LocalizedString("ErrorTeamMailboxGetUserMailboxDatabaseFailed", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				user,
				ex
			});
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x000635E4 File Offset: 0x000617E4
		public static LocalizedString SystemAPIFailed(string method, int retVal)
		{
			return new LocalizedString("SystemAPIFailed", "ExB42018", false, true, ServerStrings.ResourceManager, new object[]
			{
				method,
				retVal
			});
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0006361C File Offset: 0x0006181C
		public static LocalizedString OleConversionInitError(string processName, int expectedId, int trueId)
		{
			return new LocalizedString("OleConversionInitError", "ExE8D18C", false, true, ServerStrings.ResourceManager, new object[]
			{
				processName,
				expectedId,
				trueId
			});
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0006365D File Offset: 0x0006185D
		public static LocalizedString InternetCalendarName
		{
			get
			{
				return new LocalizedString("InternetCalendarName", "ExB2264B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0006367C File Offset: 0x0006187C
		public static LocalizedString ExUnableToGetStreamProperty(string propertyName)
		{
			return new LocalizedString("ExUnableToGetStreamProperty", "ExC7FE77", false, true, ServerStrings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000636AC File Offset: 0x000618AC
		public static LocalizedString ExMeetingCantCrossOtherOccurrences(TimeSpan endOffset, TimeSpan minimumTimeBetweenOccurrences)
		{
			return new LocalizedString("ExMeetingCantCrossOtherOccurrences", "Ex3FC250", false, true, ServerStrings.ResourceManager, new object[]
			{
				endOffset,
				minimumTimeBetweenOccurrences
			});
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x000636E9 File Offset: 0x000618E9
		public static LocalizedString ExItemNotFound
		{
			get
			{
				return new LocalizedString("ExItemNotFound", "Ex5F9FEA", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x00063707 File Offset: 0x00061907
		public static LocalizedString ExDelegateNotSupportedRespondToMeetingRequest
		{
			get
			{
				return new LocalizedString("ExDelegateNotSupportedRespondToMeetingRequest", "Ex1502CA", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x00063725 File Offset: 0x00061925
		public static LocalizedString DisposeNonIPMFolder
		{
			get
			{
				return new LocalizedString("DisposeNonIPMFolder", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00063744 File Offset: 0x00061944
		public static LocalizedString ExConfigurationNotFound(string name)
		{
			return new LocalizedString("ExConfigurationNotFound", "ExB99D32", false, true, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00063773 File Offset: 0x00061973
		public static LocalizedString InboxRuleSensitivityPrivate
		{
			get
			{
				return new LocalizedString("InboxRuleSensitivityPrivate", "Ex1D9027", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00063791 File Offset: 0x00061991
		public static LocalizedString MigrationFeatureNone
		{
			get
			{
				return new LocalizedString("MigrationFeatureNone", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x000637AF File Offset: 0x000619AF
		public static LocalizedString ClientCulture_0x405
		{
			get
			{
				return new LocalizedString("ClientCulture_0x405", "Ex28DE6F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x000637D0 File Offset: 0x000619D0
		public static LocalizedString ExNotSupportedCreateMode(int createMode)
		{
			return new LocalizedString("ExNotSupportedCreateMode", "Ex6C2042", false, true, ServerStrings.ResourceManager, new object[]
			{
				createMode
			});
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x00063804 File Offset: 0x00061A04
		public static LocalizedString ExStringContainsSurroundingWhiteSpace
		{
			get
			{
				return new LocalizedString("ExStringContainsSurroundingWhiteSpace", "ExC4366D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x00063822 File Offset: 0x00061A22
		public static LocalizedString ClientCulture_0x4C0A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x4C0A", "ExBAF5FE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00063840 File Offset: 0x00061A40
		public static LocalizedString AmDbAcllErrorNoReplicaInstance(string database, string server)
		{
			return new LocalizedString("AmDbAcllErrorNoReplicaInstance", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				database,
				server
			});
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00063873 File Offset: 0x00061A73
		public static LocalizedString ClientCulture_0x1809
		{
			get
			{
				return new LocalizedString("ClientCulture_0x1809", "Ex108688", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x00063891 File Offset: 0x00061A91
		public static LocalizedString FailedToResealKey
		{
			get
			{
				return new LocalizedString("FailedToResealKey", "Ex2A861E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x000638AF File Offset: 0x00061AAF
		public static LocalizedString InvalidParticipantForRules
		{
			get
			{
				return new LocalizedString("InvalidParticipantForRules", "Ex7B88E5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x000638D0 File Offset: 0x00061AD0
		public static LocalizedString SyncStateCollision(string name)
		{
			return new LocalizedString("SyncStateCollision", "Ex3C9243", false, true, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x000638FF File Offset: 0x00061AFF
		public static LocalizedString SpellCheckerDanish
		{
			get
			{
				return new LocalizedString("SpellCheckerDanish", "Ex79E420", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x0006391D File Offset: 0x00061B1D
		public static LocalizedString MapiCannotGetProperties
		{
			get
			{
				return new LocalizedString("MapiCannotGetProperties", "ExB22655", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x0006393B File Offset: 0x00061B3B
		public static LocalizedString MapiCopyMessagesFailed
		{
			get
			{
				return new LocalizedString("MapiCopyMessagesFailed", "Ex0151D5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00063959 File Offset: 0x00061B59
		public static LocalizedString FailedToParseValue
		{
			get
			{
				return new LocalizedString("FailedToParseValue", "Ex91EA1B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00063978 File Offset: 0x00061B78
		public static LocalizedString RmLicenseRetrieveFailed(int code)
		{
			return new LocalizedString("RmLicenseRetrieveFailed", "ExDE16CC", false, true, ServerStrings.ResourceManager, new object[]
			{
				code
			});
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x000639AC File Offset: 0x00061BAC
		public static LocalizedString RuleWriterObjectNotFound
		{
			get
			{
				return new LocalizedString("RuleWriterObjectNotFound", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x000639CC File Offset: 0x00061BCC
		public static LocalizedString DiscoveryMailboxCannotBeSourceOrTarget(string mailbox)
		{
			return new LocalizedString("DiscoveryMailboxCannotBeSourceOrTarget", "Ex033784", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x000639FB File Offset: 0x00061BFB
		public static LocalizedString ExInvalidWatermarkString
		{
			get
			{
				return new LocalizedString("ExInvalidWatermarkString", "ExB85475", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x00063A19 File Offset: 0x00061C19
		public static LocalizedString ProvisioningRequestCsvContainsBothPasswordAndFederatedIdentity
		{
			get
			{
				return new LocalizedString("ProvisioningRequestCsvContainsBothPasswordAndFederatedIdentity", "ExDB943B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x00063A37 File Offset: 0x00061C37
		public static LocalizedString OperationResultSucceeded
		{
			get
			{
				return new LocalizedString("OperationResultSucceeded", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00063A55 File Offset: 0x00061C55
		public static LocalizedString ServerLocatorServicePermanentFault
		{
			get
			{
				return new LocalizedString("ServerLocatorServicePermanentFault", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00063A73 File Offset: 0x00061C73
		public static LocalizedString NotMailboxSession
		{
			get
			{
				return new LocalizedString("NotMailboxSession", "Ex14E4F8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x00063A91 File Offset: 0x00061C91
		public static LocalizedString MigrationStateActive
		{
			get
			{
				return new LocalizedString("MigrationStateActive", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x00063AAF File Offset: 0x00061CAF
		public static LocalizedString Null
		{
			get
			{
				return new LocalizedString("Null", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00063AD0 File Offset: 0x00061CD0
		public static LocalizedString ErrorNonUniqueLegacyDN(string legacyDN)
		{
			return new LocalizedString("ErrorNonUniqueLegacyDN", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				legacyDN
			});
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00063B00 File Offset: 0x00061D00
		public static LocalizedString ExMclIsTooBig(long actualSize, long maxAllowedSize)
		{
			return new LocalizedString("ExMclIsTooBig", "ExEBD215", false, true, ServerStrings.ResourceManager, new object[]
			{
				actualSize,
				maxAllowedSize
			});
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x00063B3D File Offset: 0x00061D3D
		public static LocalizedString FolderRuleStageOnCreatedMessage
		{
			get
			{
				return new LocalizedString("FolderRuleStageOnCreatedMessage", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x00063B5B File Offset: 0x00061D5B
		public static LocalizedString CannotSetMessageFlags
		{
			get
			{
				return new LocalizedString("CannotSetMessageFlags", "ExFAB42B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x00063B79 File Offset: 0x00061D79
		public static LocalizedString ExInvalidAsyncResult
		{
			get
			{
				return new LocalizedString("ExInvalidAsyncResult", "ExF7EEFE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00063B98 File Offset: 0x00061D98
		public static LocalizedString ExInvalidPropertyType(string propertyKey, string type)
		{
			return new LocalizedString("ExInvalidPropertyType", "Ex8CEAA8", false, true, ServerStrings.ResourceManager, new object[]
			{
				propertyKey,
				type
			});
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x00063BCB File Offset: 0x00061DCB
		public static LocalizedString ClientCulture_0x2801
		{
			get
			{
				return new LocalizedString("ClientCulture_0x2801", "ExAE73CB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x00063BE9 File Offset: 0x00061DE9
		public static LocalizedString ExFolderPropertyBagCannotSaveChanges
		{
			get
			{
				return new LocalizedString("ExFolderPropertyBagCannotSaveChanges", "ExD835DE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x00063C07 File Offset: 0x00061E07
		public static LocalizedString MigrationBatchStatusStopped
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusStopped", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00063C28 File Offset: 0x00061E28
		public static LocalizedString MailboxDatabaseRequired(Guid mailboxGuid)
		{
			return new LocalizedString("MailboxDatabaseRequired", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				mailboxGuid
			});
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x00063C5C File Offset: 0x00061E5C
		public static LocalizedString KqlParserTimeout
		{
			get
			{
				return new LocalizedString("KqlParserTimeout", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00063C7C File Offset: 0x00061E7C
		public static LocalizedString JunkEmailTrustedListXsoTooBigException(string value)
		{
			return new LocalizedString("JunkEmailTrustedListXsoTooBigException", "Ex4E399A", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00063CAB File Offset: 0x00061EAB
		public static LocalizedString TenMinutes
		{
			get
			{
				return new LocalizedString("TenMinutes", "Ex16843E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00063CCC File Offset: 0x00061ECC
		public static LocalizedString ExOperationNotSupportedForRoutingType(string operation, string routingType)
		{
			return new LocalizedString("ExOperationNotSupportedForRoutingType", "Ex068DC8", false, true, ServerStrings.ResourceManager, new object[]
			{
				operation,
				routingType
			});
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00063D00 File Offset: 0x00061F00
		public static LocalizedString DiscoveryMailboxIsNotUnique(string id1, string id2)
		{
			return new LocalizedString("DiscoveryMailboxIsNotUnique", "Ex7673AA", false, true, ServerStrings.ResourceManager, new object[]
			{
				id1,
				id2
			});
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x00063D33 File Offset: 0x00061F33
		public static LocalizedString ExMustSetSearchCriteriaToMakeVisibleToOutlook
		{
			get
			{
				return new LocalizedString("ExMustSetSearchCriteriaToMakeVisibleToOutlook", "ExE891AD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00063D54 File Offset: 0x00061F54
		public static LocalizedString ConflictingObjectType(int expected, int actual)
		{
			return new LocalizedString("ConflictingObjectType", "Ex04F689", false, true, ServerStrings.ResourceManager, new object[]
			{
				expected,
				actual
			});
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x00063D91 File Offset: 0x00061F91
		public static LocalizedString Wednesday
		{
			get
			{
				return new LocalizedString("Wednesday", "ExD565E3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x00063DAF File Offset: 0x00061FAF
		public static LocalizedString ClientCulture_0xC07
		{
			get
			{
				return new LocalizedString("ClientCulture_0xC07", "Ex3D8E96", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00063DD0 File Offset: 0x00061FD0
		public static LocalizedString MapiCannotOpenMailbox(string mailbox)
		{
			return new LocalizedString("MapiCannotOpenMailbox", "Ex2B87C8", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x00063DFF File Offset: 0x00061FFF
		public static LocalizedString LegacyMailboxSearchDescription
		{
			get
			{
				return new LocalizedString("LegacyMailboxSearchDescription", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00063E20 File Offset: 0x00062020
		public static LocalizedString ExSyncStateCorrupted(string syncstate)
		{
			return new LocalizedString("ExSyncStateCorrupted", "Ex522C7C", false, true, ServerStrings.ResourceManager, new object[]
			{
				syncstate
			});
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00063E50 File Offset: 0x00062050
		public static LocalizedString ExInvalidHexString(string hexString)
		{
			return new LocalizedString("ExInvalidHexString", "Ex4ACF3A", false, true, ServerStrings.ResourceManager, new object[]
			{
				hexString
			});
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00063E80 File Offset: 0x00062080
		public static LocalizedString MigrationUserSkippedItemString(string folder, string subject)
		{
			return new LocalizedString("MigrationUserSkippedItemString", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				folder,
				subject
			});
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00063EB4 File Offset: 0x000620B4
		public static LocalizedString FolderRuleErrorInvalidRecipient(string recipient)
		{
			return new LocalizedString("FolderRuleErrorInvalidRecipient", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				recipient
			});
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00063EE4 File Offset: 0x000620E4
		public static LocalizedString MapiCannotMatchAttachmentIds(object actualId, object expectedId)
		{
			return new LocalizedString("MapiCannotMatchAttachmentIds", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				actualId,
				expectedId
			});
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x00063F17 File Offset: 0x00062117
		public static LocalizedString MapiCannotFreeBookmark
		{
			get
			{
				return new LocalizedString("MapiCannotFreeBookmark", "ExDF15FD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00063F38 File Offset: 0x00062138
		public static LocalizedString FailedToAcquireRacAndClc(string origId, string id)
		{
			return new LocalizedString("FailedToAcquireRacAndClc", "Ex2533BB", false, true, ServerStrings.ResourceManager, new object[]
			{
				origId,
				id
			});
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00063F6B File Offset: 0x0006216B
		public static LocalizedString CannotChangePermissionsOnFolder
		{
			get
			{
				return new LocalizedString("CannotChangePermissionsOnFolder", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x00063F89 File Offset: 0x00062189
		public static LocalizedString MapiCannotSetProps
		{
			get
			{
				return new LocalizedString("MapiCannotSetProps", "Ex9054B2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x00063FA7 File Offset: 0x000621A7
		public static LocalizedString SearchStatePartiallySucceeded
		{
			get
			{
				return new LocalizedString("SearchStatePartiallySucceeded", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00063FC8 File Offset: 0x000621C8
		public static LocalizedString NotificationEmailSubjectCreated(string notificationType)
		{
			return new LocalizedString("NotificationEmailSubjectCreated", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				notificationType
			});
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00063FF8 File Offset: 0x000621F8
		public static LocalizedString SyncStateMissing(string name)
		{
			return new LocalizedString("SyncStateMissing", "ExF4DEF6", false, true, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x00064027 File Offset: 0x00062227
		public static LocalizedString InboxRuleMessageTypeAutomaticReply
		{
			get
			{
				return new LocalizedString("InboxRuleMessageTypeAutomaticReply", "Ex82D866", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00064048 File Offset: 0x00062248
		public static LocalizedString FailedToFindTargetUriFromMExData(Uri url)
		{
			return new LocalizedString("FailedToFindTargetUriFromMExData", "Ex103BC8", false, true, ServerStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00064078 File Offset: 0x00062278
		public static LocalizedString AmDatabaseNotFoundException(Guid dbGuid)
		{
			return new LocalizedString("AmDatabaseNotFoundException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				dbGuid
			});
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x000640AC File Offset: 0x000622AC
		public static LocalizedString ExInvalidHexCharacter(char hexCharacter)
		{
			return new LocalizedString("ExInvalidHexCharacter", "Ex115A50", false, true, ServerStrings.ResourceManager, new object[]
			{
				hexCharacter
			});
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x000640E0 File Offset: 0x000622E0
		public static LocalizedString RuleHistoryError
		{
			get
			{
				return new LocalizedString("RuleHistoryError", "Ex834D15", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x000640FE File Offset: 0x000622FE
		public static LocalizedString ClientCulture_0x280A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x280A", "ExB16BD5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0006411C File Offset: 0x0006231C
		public static LocalizedString ServerForDatabaseNotFound(string dbName, string databaseGuid)
		{
			return new LocalizedString("ServerForDatabaseNotFound", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				dbName,
				databaseGuid
			});
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0006414F File Offset: 0x0006234F
		public static LocalizedString ClientCulture_0x3001
		{
			get
			{
				return new LocalizedString("ClientCulture_0x3001", "Ex690E17", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00064170 File Offset: 0x00062370
		public static LocalizedString RpcClientException(string method, string server)
		{
			return new LocalizedString("RpcClientException", "Ex1D3DE9", false, true, ServerStrings.ResourceManager, new object[]
			{
				method,
				server
			});
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x000641A3 File Offset: 0x000623A3
		public static LocalizedString SharePoint
		{
			get
			{
				return new LocalizedString("SharePoint", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x000641C4 File Offset: 0x000623C4
		public static LocalizedString ErrorCouldNotUpdateMasterIdentityProperty(string name)
		{
			return new LocalizedString("ErrorCouldNotUpdateMasterIdentityProperty", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000641F4 File Offset: 0x000623F4
		public static LocalizedString DataMoveReplicationConstraintSatisfied(DataMoveReplicationConstraintParameter constraint, Guid databaseGuid)
		{
			return new LocalizedString("DataMoveReplicationConstraintSatisfied", "ExE281C0", false, true, ServerStrings.ResourceManager, new object[]
			{
				constraint,
				databaseGuid
			});
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x00064231 File Offset: 0x00062431
		public static LocalizedString NoDelegateAction
		{
			get
			{
				return new LocalizedString("NoDelegateAction", "Ex953F36", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x0006424F File Offset: 0x0006244F
		public static LocalizedString MigrationFlagsStop
		{
			get
			{
				return new LocalizedString("MigrationFlagsStop", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x0006426D File Offset: 0x0006246D
		public static LocalizedString ExNoOptimizedCodePath
		{
			get
			{
				return new LocalizedString("ExNoOptimizedCodePath", "ExECB030", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0006428C File Offset: 0x0006248C
		public static LocalizedString UnknownOscProvider(string provider)
		{
			return new LocalizedString("UnknownOscProvider", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				provider
			});
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x000642BC File Offset: 0x000624BC
		public static LocalizedString ErrorTeamMailboxUserNotResolved(string users)
		{
			return new LocalizedString("ErrorTeamMailboxUserNotResolved", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				users
			});
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x000642EB File Offset: 0x000624EB
		public static LocalizedString MigrationBatchFlagUseAdvancedValidation
		{
			get
			{
				return new LocalizedString("MigrationBatchFlagUseAdvancedValidation", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x00064309 File Offset: 0x00062509
		public static LocalizedString MapiCannotGetParentEntryId
		{
			get
			{
				return new LocalizedString("MapiCannotGetParentEntryId", "Ex8B3B77", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00064327 File Offset: 0x00062527
		public static LocalizedString ExOnlyMessagesHaveParent
		{
			get
			{
				return new LocalizedString("ExOnlyMessagesHaveParent", "ExE7329B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00064348 File Offset: 0x00062548
		public static LocalizedString FolderRuleErrorGroupDoesNotResolve(string address)
		{
			return new LocalizedString("FolderRuleErrorGroupDoesNotResolve", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x00064377 File Offset: 0x00062577
		public static LocalizedString ClientCulture_0x411
		{
			get
			{
				return new LocalizedString("ClientCulture_0x411", "Ex3F88D9", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x00064395 File Offset: 0x00062595
		public static LocalizedString ExFolderDoesNotMatchFolderId
		{
			get
			{
				return new LocalizedString("ExFolderDoesNotMatchFolderId", "Ex800E7B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x000643B3 File Offset: 0x000625B3
		public static LocalizedString MigrationTypeXO1
		{
			get
			{
				return new LocalizedString("MigrationTypeXO1", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x000643D4 File Offset: 0x000625D4
		public static LocalizedString DatabaseNotFound(string databaseId)
		{
			return new LocalizedString("DatabaseNotFound", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				databaseId
			});
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00064403 File Offset: 0x00062603
		public static LocalizedString NotOperator
		{
			get
			{
				return new LocalizedString("NotOperator", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x00064421 File Offset: 0x00062621
		public static LocalizedString ClientCulture_0x480A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x480A", "ExAA2487", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0006443F File Offset: 0x0006263F
		public static LocalizedString Saturday
		{
			get
			{
				return new LocalizedString("Saturday", "ExA50D93", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0006445D File Offset: 0x0006265D
		public static LocalizedString ExFailedToRegisterExchangeTopologyNotification
		{
			get
			{
				return new LocalizedString("ExFailedToRegisterExchangeTopologyNotification", "ExE1EBCE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0006447B File Offset: 0x0006267B
		public static LocalizedString MigrationBatchSupportedActionSet
		{
			get
			{
				return new LocalizedString("MigrationBatchSupportedActionSet", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0006449C File Offset: 0x0006269C
		public static LocalizedString ExSearchFolderCorruptOutlookBlob(string param)
		{
			return new LocalizedString("ExSearchFolderCorruptOutlookBlob", "Ex852FE1", false, true, ServerStrings.ResourceManager, new object[]
			{
				param
			});
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x000644CB File Offset: 0x000626CB
		public static LocalizedString ConversionCannotOpenJournalMessage
		{
			get
			{
				return new LocalizedString("ConversionCannotOpenJournalMessage", "Ex0E26E5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x000644EC File Offset: 0x000626EC
		public static LocalizedString ExCannotMoveOrCopyOccurrenceItem(object itemId)
		{
			return new LocalizedString("ExCannotMoveOrCopyOccurrenceItem", "Ex3D161C", false, true, ServerStrings.ResourceManager, new object[]
			{
				itemId
			});
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0006451B File Offset: 0x0006271B
		public static LocalizedString JunkEmailTrustedListXsoEmptyException
		{
			get
			{
				return new LocalizedString("JunkEmailTrustedListXsoEmptyException", "Ex29DBA4", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x00064539 File Offset: 0x00062739
		public static LocalizedString AmFailedToFindSuitableServer
		{
			get
			{
				return new LocalizedString("AmFailedToFindSuitableServer", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00064557 File Offset: 0x00062757
		public static LocalizedString OperationalError
		{
			get
			{
				return new LocalizedString("OperationalError", "Ex19E8E8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x00064575 File Offset: 0x00062775
		public static LocalizedString ExTooManySortColumns
		{
			get
			{
				return new LocalizedString("ExTooManySortColumns", "Ex894B55", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x00064593 File Offset: 0x00062793
		public static LocalizedString LoadRulesFromStore
		{
			get
			{
				return new LocalizedString("LoadRulesFromStore", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x000645B1 File Offset: 0x000627B1
		public static LocalizedString ClientCulture_0x40D
		{
			get
			{
				return new LocalizedString("ClientCulture_0x40D", "Ex65A67B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x000645CF File Offset: 0x000627CF
		public static LocalizedString ExCantCopyBadAlienDLMember
		{
			get
			{
				return new LocalizedString("ExCantCopyBadAlienDLMember", "Ex948C20", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x000645ED File Offset: 0x000627ED
		public static LocalizedString TeamMailboxMessageSendMailToTheSiteMailbox
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageSendMailToTheSiteMailbox", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0006460C File Offset: 0x0006280C
		public static LocalizedString JunkEmailBlockedListXsoFormatException(string value)
		{
			return new LocalizedString("JunkEmailBlockedListXsoFormatException", "ExA9C73B", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0006463C File Offset: 0x0006283C
		public static LocalizedString InvalidDueDate2(string dueDate, string reminderTime)
		{
			return new LocalizedString("InvalidDueDate2", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				dueDate,
				reminderTime
			});
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0006466F File Offset: 0x0006286F
		public static LocalizedString InboxRuleSensitivityPersonal
		{
			get
			{
				return new LocalizedString("InboxRuleSensitivityPersonal", "Ex74F127", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x0006468D File Offset: 0x0006288D
		public static LocalizedString ExInvalidItemCountAdvisorCondition
		{
			get
			{
				return new LocalizedString("ExInvalidItemCountAdvisorCondition", "Ex92E02C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x000646AB File Offset: 0x000628AB
		public static LocalizedString ErrorLoadingRules
		{
			get
			{
				return new LocalizedString("ErrorLoadingRules", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x000646C9 File Offset: 0x000628C9
		public static LocalizedString ClientCulture_0x414
		{
			get
			{
				return new LocalizedString("ClientCulture_0x414", "Ex3053DD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x000646E7 File Offset: 0x000628E7
		public static LocalizedString ExEndTimeNotSet
		{
			get
			{
				return new LocalizedString("ExEndTimeNotSet", "Ex568ACE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x00064705 File Offset: 0x00062905
		public static LocalizedString InboxRuleMessageTypeAutomaticForward
		{
			get
			{
				return new LocalizedString("InboxRuleMessageTypeAutomaticForward", "Ex1D44E3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00064723 File Offset: 0x00062923
		public static LocalizedString MapiCannotCopyMapiProps
		{
			get
			{
				return new LocalizedString("MapiCannotCopyMapiProps", "ExF39EC5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x00064741 File Offset: 0x00062941
		public static LocalizedString OneWeeks
		{
			get
			{
				return new LocalizedString("OneWeeks", "Ex07309C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0006475F File Offset: 0x0006295F
		public static LocalizedString TeamMailboxMessageWhatYouCanDoNext
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageWhatYouCanDoNext", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x0006477D File Offset: 0x0006297D
		public static LocalizedString MapiCannotGetReceiveFolderInfo
		{
			get
			{
				return new LocalizedString("MapiCannotGetReceiveFolderInfo", "Ex270031", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0006479C File Offset: 0x0006299C
		public static LocalizedString MigrationRunspaceError(string commandName, string msg)
		{
			return new LocalizedString("MigrationRunspaceError", "Ex650C08", false, true, ServerStrings.ResourceManager, new object[]
			{
				commandName,
				msg
			});
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x000647CF File Offset: 0x000629CF
		public static LocalizedString ExInvalidStoreObjectId
		{
			get
			{
				return new LocalizedString("ExInvalidStoreObjectId", "ExC007AC", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x000647ED File Offset: 0x000629ED
		public static LocalizedString RequestStateQueued
		{
			get
			{
				return new LocalizedString("RequestStateQueued", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x0006480B File Offset: 0x00062A0B
		public static LocalizedString RecurrenceBlobCorrupted
		{
			get
			{
				return new LocalizedString("RecurrenceBlobCorrupted", "Ex89ED2D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00064829 File Offset: 0x00062A29
		public static LocalizedString CannotFindAttachment
		{
			get
			{
				return new LocalizedString("CannotFindAttachment", "Ex5266E8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00064847 File Offset: 0x00062A47
		public static LocalizedString ExInvalidRecipientBlob
		{
			get
			{
				return new LocalizedString("ExInvalidRecipientBlob", "Ex5962A3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00064868 File Offset: 0x00062A68
		public static LocalizedString TeamMailboxMessageReactivatedSubject(string tmName)
		{
			return new LocalizedString("TeamMailboxMessageReactivatedSubject", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				tmName
			});
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x00064897 File Offset: 0x00062A97
		public static LocalizedString ExIncompleteBlob
		{
			get
			{
				return new LocalizedString("ExIncompleteBlob", "Ex3A7A3D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x000648B5 File Offset: 0x00062AB5
		public static LocalizedString ExPatternNotSet
		{
			get
			{
				return new LocalizedString("ExPatternNotSet", "Ex16EFD9", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000648D4 File Offset: 0x00062AD4
		public static LocalizedString CompositeError(LocalizedString error1, LocalizedString error2)
		{
			return new LocalizedString("CompositeError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				error1,
				error2
			});
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00064911 File Offset: 0x00062B11
		public static LocalizedString ExInvalidDayOfMonth
		{
			get
			{
				return new LocalizedString("ExInvalidDayOfMonth", "Ex98518B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x0006492F File Offset: 0x00062B2F
		public static LocalizedString ExInvalidGlobalObjectId
		{
			get
			{
				return new LocalizedString("ExInvalidGlobalObjectId", "Ex453FE1", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0006494D File Offset: 0x00062B4D
		public static LocalizedString MapiCannotGetHierarchyTable
		{
			get
			{
				return new LocalizedString("MapiCannotGetHierarchyTable", "ExA6C898", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0006496C File Offset: 0x00062B6C
		public static LocalizedString ExNullItemIdParameter(int index)
		{
			return new LocalizedString("ExNullItemIdParameter", "Ex566E8A", false, true, ServerStrings.ResourceManager, new object[]
			{
				index
			});
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x000649A0 File Offset: 0x00062BA0
		public static LocalizedString ExItemDoesNotBelongToCurrentFolder(object itemId)
		{
			return new LocalizedString("ExItemDoesNotBelongToCurrentFolder", "Ex2E8F86", false, true, ServerStrings.ResourceManager, new object[]
			{
				itemId
			});
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x000649CF File Offset: 0x00062BCF
		public static LocalizedString ClientCulture_0xC0A
		{
			get
			{
				return new LocalizedString("ClientCulture_0xC0A", "Ex89D7D7", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x000649ED File Offset: 0x00062BED
		public static LocalizedString ExInvalidFullyQualifiedServerName
		{
			get
			{
				return new LocalizedString("ExInvalidFullyQualifiedServerName", "Ex482CF4", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00064A0B File Offset: 0x00062C0B
		public static LocalizedString EstimateStateInProgress
		{
			get
			{
				return new LocalizedString("EstimateStateInProgress", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00064A29 File Offset: 0x00062C29
		public static LocalizedString MapiCannotSetReadFlags
		{
			get
			{
				return new LocalizedString("MapiCannotSetReadFlags", "Ex34D7CF", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x00064A47 File Offset: 0x00062C47
		public static LocalizedString PublicFolderQueryStatusSyncFolderHierarchyRpcFailed
		{
			get
			{
				return new LocalizedString("PublicFolderQueryStatusSyncFolderHierarchyRpcFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00064A68 File Offset: 0x00062C68
		public static LocalizedString AddressRequiredForRoutingType(string routingType)
		{
			return new LocalizedString("AddressRequiredForRoutingType", "Ex522D18", false, true, ServerStrings.ResourceManager, new object[]
			{
				routingType
			});
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x00064A97 File Offset: 0x00062C97
		public static LocalizedString ExInvalidSearchFolderScope
		{
			get
			{
				return new LocalizedString("ExInvalidSearchFolderScope", "Ex82F751", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x00064AB5 File Offset: 0x00062CB5
		public static LocalizedString ActivitySessionIsNull
		{
			get
			{
				return new LocalizedString("ActivitySessionIsNull", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x00064AD3 File Offset: 0x00062CD3
		public static LocalizedString SpellCheckerItalian
		{
			get
			{
				return new LocalizedString("SpellCheckerItalian", "ExA50EDC", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00064AF4 File Offset: 0x00062CF4
		public static LocalizedString InconsistentCalendarType(string type, string id)
		{
			return new LocalizedString("InconsistentCalendarType", "ExA69809", false, true, ServerStrings.ResourceManager, new object[]
			{
				type,
				id
			});
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x00064B27 File Offset: 0x00062D27
		public static LocalizedString FolderRuleStageOnPublicFolderAfter
		{
			get
			{
				return new LocalizedString("FolderRuleStageOnPublicFolderAfter", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x00064B45 File Offset: 0x00062D45
		public static LocalizedString NotAllowedAnonymousSharingByPolicy
		{
			get
			{
				return new LocalizedString("NotAllowedAnonymousSharingByPolicy", "Ex846A49", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00064B64 File Offset: 0x00062D64
		public static LocalizedString PropertyErrorString(string name, PropertyErrorCode code, string descr)
		{
			return new LocalizedString("PropertyErrorString", "ExD955C2", false, true, ServerStrings.ResourceManager, new object[]
			{
				name,
				code,
				descr
			});
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00064BA0 File Offset: 0x00062DA0
		public static LocalizedString ExStartDateCantBeGreaterThanMaximum(object startDate, object maximumStartDate)
		{
			return new LocalizedString("ExStartDateCantBeGreaterThanMaximum", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				startDate,
				maximumStartDate
			});
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00064BD4 File Offset: 0x00062DD4
		public static LocalizedString ExEndDateCantExceedMaxDate(object endTime, object maxTime)
		{
			return new LocalizedString("ExEndDateCantExceedMaxDate", "Ex462507", false, true, ServerStrings.ResourceManager, new object[]
			{
				endTime,
				maxTime
			});
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00064C08 File Offset: 0x00062E08
		public static LocalizedString CannotCreateManifestEx(Type xsoManifestType)
		{
			return new LocalizedString("CannotCreateManifestEx", "Ex456737", false, true, ServerStrings.ResourceManager, new object[]
			{
				xsoManifestType
			});
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x00064C37 File Offset: 0x00062E37
		public static LocalizedString MapiCannotGetCollapseState
		{
			get
			{
				return new LocalizedString("MapiCannotGetCollapseState", "Ex91A6BE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00064C55 File Offset: 0x00062E55
		public static LocalizedString ZeroMinutes
		{
			get
			{
				return new LocalizedString("ZeroMinutes", "Ex42F6F9", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x00064C73 File Offset: 0x00062E73
		public static LocalizedString RecipientNotSupportedByAnyProviderException
		{
			get
			{
				return new LocalizedString("RecipientNotSupportedByAnyProviderException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x00064C91 File Offset: 0x00062E91
		public static LocalizedString MigrationReportFinalizationSuccess
		{
			get
			{
				return new LocalizedString("MigrationReportFinalizationSuccess", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00064CAF File Offset: 0x00062EAF
		public static LocalizedString CalNotifTypeChangedUpdate
		{
			get
			{
				return new LocalizedString("CalNotifTypeChangedUpdate", "ExE24103", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00064CD0 File Offset: 0x00062ED0
		public static LocalizedString ExNewerVersionedSyncState(string syncstate, int savedVersion, int casVersion)
		{
			return new LocalizedString("ExNewerVersionedSyncState", "Ex680792", false, true, ServerStrings.ResourceManager, new object[]
			{
				syncstate,
				savedVersion,
				casVersion
			});
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00064D11 File Offset: 0x00062F11
		public static LocalizedString ExSizeFilterPropertyNotSupported
		{
			get
			{
				return new LocalizedString("ExSizeFilterPropertyNotSupported", "ExA2611E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00064D30 File Offset: 0x00062F30
		public static LocalizedString ErrorInvalidTimeFormat(string timeFormat, string lang, string validFormats)
		{
			return new LocalizedString("ErrorInvalidTimeFormat", "Ex402B93", false, true, ServerStrings.ResourceManager, new object[]
			{
				timeFormat,
				lang,
				validFormats
			});
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00064D68 File Offset: 0x00062F68
		public static LocalizedString InvalidTagName(string expected, string got)
		{
			return new LocalizedString("InvalidTagName", "ExDF59B1", false, true, ServerStrings.ResourceManager, new object[]
			{
				expected,
				got
			});
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x00064D9B File Offset: 0x00062F9B
		public static LocalizedString ExConversationActionInvalidFolderType
		{
			get
			{
				return new LocalizedString("ExConversationActionInvalidFolderType", "ExD19EDB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00064DB9 File Offset: 0x00062FB9
		public static LocalizedString ClientCulture_0x2009
		{
			get
			{
				return new LocalizedString("ClientCulture_0x2009", "Ex8B0E51", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x00064DD7 File Offset: 0x00062FD7
		public static LocalizedString UserDiscoveryMailboxNotFound
		{
			get
			{
				return new LocalizedString("UserDiscoveryMailboxNotFound", "Ex80BE98", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00064DF5 File Offset: 0x00062FF5
		public static LocalizedString ExCorruptedRecurringCalItem
		{
			get
			{
				return new LocalizedString("ExCorruptedRecurringCalItem", "ExF732FD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00064E14 File Offset: 0x00063014
		public static LocalizedString MigrationJobItemRecipientTypeMismatch(string smtpAddress, string newType, string oldType)
		{
			return new LocalizedString("MigrationJobItemRecipientTypeMismatch", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				smtpAddress,
				newType,
				oldType
			});
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x00064E4B File Offset: 0x0006304B
		public static LocalizedString ExStartDateLaterThanEndDate
		{
			get
			{
				return new LocalizedString("ExStartDateLaterThanEndDate", "Ex406EED", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00064E6C File Offset: 0x0006306C
		public static LocalizedString ExModifiedOccurrenceCrossingAdjacentOccurrenceBoundary(object startTime, object endTime, object adjacentOccStartTime, object adjacentOccEndTime)
		{
			return new LocalizedString("ExModifiedOccurrenceCrossingAdjacentOccurrenceBoundary", "Ex30ED0F", false, true, ServerStrings.ResourceManager, new object[]
			{
				startTime,
				endTime,
				adjacentOccStartTime,
				adjacentOccEndTime
			});
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00064EA7 File Offset: 0x000630A7
		public static LocalizedString ExInvalidOrganizer
		{
			get
			{
				return new LocalizedString("ExInvalidOrganizer", "Ex656263", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x00064EC5 File Offset: 0x000630C5
		public static LocalizedString MaxExclusionReached
		{
			get
			{
				return new LocalizedString("MaxExclusionReached", "Ex3A695F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x00064EE3 File Offset: 0x000630E3
		public static LocalizedString JunkEmailBlockedListXsoEmptyException
		{
			get
			{
				return new LocalizedString("JunkEmailBlockedListXsoEmptyException", "ExA36FE0", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x00064F01 File Offset: 0x00063101
		public static LocalizedString SearchStateSucceeded
		{
			get
			{
				return new LocalizedString("SearchStateSucceeded", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00064F20 File Offset: 0x00063120
		public static LocalizedString ExComparisonOperatorNotSupportedForProperty(string comparisonOperator, string propertyName)
		{
			return new LocalizedString("ExComparisonOperatorNotSupportedForProperty", "Ex0008CC", false, true, ServerStrings.ResourceManager, new object[]
			{
				comparisonOperator,
				propertyName
			});
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x00064F53 File Offset: 0x00063153
		public static LocalizedString ExInvalidJournalReportFormat
		{
			get
			{
				return new LocalizedString("ExInvalidJournalReportFormat", "Ex071254", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00064F74 File Offset: 0x00063174
		public static LocalizedString NotAuthorizedtoAccessGroupMailbox(string user, string group)
		{
			return new LocalizedString("NotAuthorizedtoAccessGroupMailbox", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				user,
				group
			});
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x00064FA7 File Offset: 0x000631A7
		public static LocalizedString RequestStateCertExpiring
		{
			get
			{
				return new LocalizedString("RequestStateCertExpiring", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00064FC8 File Offset: 0x000631C8
		public static LocalizedString TaskServerException(string errorMessage)
		{
			return new LocalizedString("TaskServerException", "ExB2F450", false, true, ServerStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x00064FF7 File Offset: 0x000631F7
		public static LocalizedString ConversionBodyConversionFailed
		{
			get
			{
				return new LocalizedString("ConversionBodyConversionFailed", "ExEC2A9C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00065018 File Offset: 0x00063218
		public static LocalizedString ErrorInvalidQueryTooLong(string name)
		{
			return new LocalizedString("ErrorInvalidQueryTooLong", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x00065047 File Offset: 0x00063247
		public static LocalizedString MigrationUserAdminTypeTenantAdmin
		{
			get
			{
				return new LocalizedString("MigrationUserAdminTypeTenantAdmin", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x00065065 File Offset: 0x00063265
		public static LocalizedString ClientCulture_0x40B
		{
			get
			{
				return new LocalizedString("ClientCulture_0x40B", "Ex8C1297", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00065084 File Offset: 0x00063284
		public static LocalizedString ExConfigDataCorrupted(string field)
		{
			return new LocalizedString("ExConfigDataCorrupted", "ExB43055", false, true, ServerStrings.ResourceManager, new object[]
			{
				field
			});
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x000650B3 File Offset: 0x000632B3
		public static LocalizedString ConversionEmptyAddress
		{
			get
			{
				return new LocalizedString("ConversionEmptyAddress", "ExD90BD9", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x000650D1 File Offset: 0x000632D1
		public static LocalizedString ClientCulture_0x380A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x380A", "ExAF4C31", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x000650EF File Offset: 0x000632EF
		public static LocalizedString PublicFoldersCannotBeAccessedDuringCompletion
		{
			get
			{
				return new LocalizedString("PublicFoldersCannotBeAccessedDuringCompletion", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00065110 File Offset: 0x00063310
		public static LocalizedString LicenseExpired(string expiryTime)
		{
			return new LocalizedString("LicenseExpired", "Ex9E8A95", false, true, ServerStrings.ResourceManager, new object[]
			{
				expiryTime
			});
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00065140 File Offset: 0x00063340
		public static LocalizedString ElementHasUnsupportedValue(string name)
		{
			return new LocalizedString("ElementHasUnsupportedValue", "Ex81A452", false, true, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00065170 File Offset: 0x00063370
		public static LocalizedString CannotResolvePropertyTagsToPropertyDefinitions(uint propertyTag)
		{
			return new LocalizedString("CannotResolvePropertyTagsToPropertyDefinitions", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				propertyTag
			});
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x000651A4 File Offset: 0x000633A4
		public static LocalizedString MigrationReportUnknown
		{
			get
			{
				return new LocalizedString("MigrationReportUnknown", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x000651C2 File Offset: 0x000633C2
		public static LocalizedString FolderRuleResolvingAddressBookEntryId
		{
			get
			{
				return new LocalizedString("FolderRuleResolvingAddressBookEntryId", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x000651E0 File Offset: 0x000633E0
		public static LocalizedString SharePointLifecyclePolicy
		{
			get
			{
				return new LocalizedString("SharePointLifecyclePolicy", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x000651FE File Offset: 0x000633FE
		public static LocalizedString MapiCannotAddNotification
		{
			get
			{
				return new LocalizedString("MapiCannotAddNotification", "ExB8C380", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0006521C File Offset: 0x0006341C
		public static LocalizedString NonNegativeParameter(string parameter)
		{
			return new LocalizedString("NonNegativeParameter", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0006524B File Offset: 0x0006344B
		public static LocalizedString ClientCulture_0x200A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x200A", "Ex549083", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x00065269 File Offset: 0x00063469
		public static LocalizedString ExFailedToCreateEventManager
		{
			get
			{
				return new LocalizedString("ExFailedToCreateEventManager", "ExD6A304", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x00065287 File Offset: 0x00063487
		public static LocalizedString MaixmumNumberOfMailboxAssociationsForUserReached
		{
			get
			{
				return new LocalizedString("MaixmumNumberOfMailboxAssociationsForUserReached", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x000652A5 File Offset: 0x000634A5
		public static LocalizedString ExInvalidEIT
		{
			get
			{
				return new LocalizedString("ExInvalidEIT", "Ex3FF77B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x000652C3 File Offset: 0x000634C3
		public static LocalizedString ExFilterAndSortNotSupportedInSimpleVirtualPropertyDefinition
		{
			get
			{
				return new LocalizedString("ExFilterAndSortNotSupportedInSimpleVirtualPropertyDefinition", "Ex61CE5C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x000652E4 File Offset: 0x000634E4
		public static LocalizedString CreateConfigurationItem(string mailbox)
		{
			return new LocalizedString("CreateConfigurationItem", "Ex6F2B5B", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x00065313 File Offset: 0x00063513
		public static LocalizedString DateRangeOneMonth
		{
			get
			{
				return new LocalizedString("DateRangeOneMonth", "ExE73099", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x00065331 File Offset: 0x00063531
		public static LocalizedString MapiInvalidParam
		{
			get
			{
				return new LocalizedString("MapiInvalidParam", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x0006534F File Offset: 0x0006354F
		public static LocalizedString MapiCannotDeleteUserPhoto
		{
			get
			{
				return new LocalizedString("MapiCannotDeleteUserPhoto", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x0006536D File Offset: 0x0006356D
		public static LocalizedString MigrationUserAdminTypePartner
		{
			get
			{
				return new LocalizedString("MigrationUserAdminTypePartner", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0006538C File Offset: 0x0006358C
		public static LocalizedString ActiveMonitoringOperationFailedWithEcException(int ec)
		{
			return new LocalizedString("ActiveMonitoringOperationFailedWithEcException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				ec
			});
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x000653C0 File Offset: 0x000635C0
		public static LocalizedString ExOrganizerCannotCallUpdateCalendarItem
		{
			get
			{
				return new LocalizedString("ExOrganizerCannotCallUpdateCalendarItem", "ExCA94E2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x000653DE File Offset: 0x000635DE
		public static LocalizedString DuplicateCondition
		{
			get
			{
				return new LocalizedString("DuplicateCondition", "Ex092B6F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x000653FC File Offset: 0x000635FC
		public static LocalizedString JunkEmailTrustedListXsoTooManyException
		{
			get
			{
				return new LocalizedString("JunkEmailTrustedListXsoTooManyException", "ExA0A929", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0006541A File Offset: 0x0006361A
		public static LocalizedString VersionNotInteger
		{
			get
			{
				return new LocalizedString("VersionNotInteger", "ExDE59FB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x00065438 File Offset: 0x00063638
		public static LocalizedString ClientCulture_0x41A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x41A", "ExB72BAF", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00065458 File Offset: 0x00063658
		public static LocalizedString ExInvalidRecurrenceInterval(int recurrenceInterval)
		{
			return new LocalizedString("ExInvalidRecurrenceInterval", "Ex3E43DC", false, true, ServerStrings.ResourceManager, new object[]
			{
				recurrenceInterval
			});
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0006548C File Offset: 0x0006368C
		public static LocalizedString FederatedMailboxNotSet(string tenantId)
		{
			return new LocalizedString("FederatedMailboxNotSet", "ExF831B4", false, true, ServerStrings.ResourceManager, new object[]
			{
				tenantId
			});
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x000654BC File Offset: 0x000636BC
		public static LocalizedString JunkEmailValidationError(string value)
		{
			return new LocalizedString("JunkEmailValidationError", "Ex4032C2", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x000654EB File Offset: 0x000636EB
		public static LocalizedString MapiCannotGetNamedProperties
		{
			get
			{
				return new LocalizedString("MapiCannotGetNamedProperties", "Ex4FFDEF", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00065509 File Offset: 0x00063709
		public static LocalizedString ExFailedToDeleteDefaultFolder
		{
			get
			{
				return new LocalizedString("ExFailedToDeleteDefaultFolder", "Ex5F0402", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00065527 File Offset: 0x00063727
		public static LocalizedString ExDefaultFoldersNotInitialized
		{
			get
			{
				return new LocalizedString("ExDefaultFoldersNotInitialized", "ExE6AFED", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x00065545 File Offset: 0x00063745
		public static LocalizedString AsyncOperationTypeCertExpiry
		{
			get
			{
				return new LocalizedString("AsyncOperationTypeCertExpiry", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00065564 File Offset: 0x00063764
		public static LocalizedString MigrationUnexpectedExchangePrincipalFound(string smtpAddress)
		{
			return new LocalizedString("MigrationUnexpectedExchangePrincipalFound", "ExE22EA7", false, true, ServerStrings.ResourceManager, new object[]
			{
				smtpAddress
			});
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00065594 File Offset: 0x00063794
		public static LocalizedString RpcServerRequestThrottled(string mailboxGuid, string nextAllowedTime)
		{
			return new LocalizedString("RpcServerRequestThrottled", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				mailboxGuid,
				nextAllowedTime
			});
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x000655C7 File Offset: 0x000637C7
		public static LocalizedString ClosingTagExpectedNoneFound
		{
			get
			{
				return new LocalizedString("ClosingTagExpectedNoneFound", "Ex681320", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x000655E8 File Offset: 0x000637E8
		public static LocalizedString DefaultFolderNotFoundInPublicFolderMailbox(object id)
		{
			return new LocalizedString("DefaultFolderNotFoundInPublicFolderMailbox", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00065618 File Offset: 0x00063818
		public static LocalizedString JunkEmailTrustedListInternalToOrganizationException(string value)
		{
			return new LocalizedString("JunkEmailTrustedListInternalToOrganizationException", "Ex5BD8BA", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x00065647 File Offset: 0x00063847
		public static LocalizedString FolderRuleStageEvaluation
		{
			get
			{
				return new LocalizedString("FolderRuleStageEvaluation", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00065668 File Offset: 0x00063868
		public static LocalizedString ExStartDateCantBeLessThanMinimum(object startDate, object minimumStartDate)
		{
			return new LocalizedString("ExStartDateCantBeLessThanMinimum", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				startDate,
				minimumStartDate
			});
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0006569C File Offset: 0x0006389C
		public static LocalizedString ExPDLCorruptOutlookBlob(string param)
		{
			return new LocalizedString("ExPDLCorruptOutlookBlob", "Ex9B9D77", false, true, ServerStrings.ResourceManager, new object[]
			{
				param
			});
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x000656CB File Offset: 0x000638CB
		public static LocalizedString ClientCulture_0x814
		{
			get
			{
				return new LocalizedString("ClientCulture_0x814", "Ex7687AE", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x000656E9 File Offset: 0x000638E9
		public static LocalizedString TeamMailboxMessageMemberInvitationBodyIntroText
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageMemberInvitationBodyIntroText", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x00065707 File Offset: 0x00063907
		public static LocalizedString ClientCulture_0xC09
		{
			get
			{
				return new LocalizedString("ClientCulture_0xC09", "Ex712706", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00065725 File Offset: 0x00063925
		public static LocalizedString UnexpectedToken
		{
			get
			{
				return new LocalizedString("UnexpectedToken", "Ex19401D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x00065743 File Offset: 0x00063943
		public static LocalizedString MapiCannotSeekRowBookmark
		{
			get
			{
				return new LocalizedString("MapiCannotSeekRowBookmark", "Ex714CD8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00065761 File Offset: 0x00063961
		public static LocalizedString PublicFolderSyncFolderRpcFailed
		{
			get
			{
				return new LocalizedString("PublicFolderSyncFolderRpcFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00065780 File Offset: 0x00063980
		public static LocalizedString DataMoveReplicationConstraintNotSatisfied(DataMoveReplicationConstraintParameter constraint, Guid databaseGuid)
		{
			return new LocalizedString("DataMoveReplicationConstraintNotSatisfied", "Ex46A4F7", false, true, ServerStrings.ResourceManager, new object[]
			{
				constraint,
				databaseGuid
			});
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x000657BD File Offset: 0x000639BD
		public static LocalizedString MigrationFlagsNone
		{
			get
			{
				return new LocalizedString("MigrationFlagsNone", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x000657DB File Offset: 0x000639DB
		public static LocalizedString JunkEmailTrustedListXsoNullException
		{
			get
			{
				return new LocalizedString("JunkEmailTrustedListXsoNullException", "Ex3B51D7", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x000657F9 File Offset: 0x000639F9
		public static LocalizedString MapiCannotQueryRows
		{
			get
			{
				return new LocalizedString("MapiCannotQueryRows", "ExF149A5", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00065818 File Offset: 0x00063A18
		public static LocalizedString ExInvalidLicense(string mailbox)
		{
			return new LocalizedString("ExInvalidLicense", "Ex78C8F8", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00065848 File Offset: 0x00063A48
		public static LocalizedString ErrorCalendarReminderTooLarge(string value)
		{
			return new LocalizedString("ErrorCalendarReminderTooLarge", "Ex9C2D25", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x00065877 File Offset: 0x00063A77
		public static LocalizedString MigrationTestMSASuccess
		{
			get
			{
				return new LocalizedString("MigrationTestMSASuccess", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00065898 File Offset: 0x00063A98
		public static LocalizedString ExSetNotSupportedForCalculatedProperty(object proptertyID)
		{
			return new LocalizedString("ExSetNotSupportedForCalculatedProperty", "ExA65ABB", false, true, ServerStrings.ResourceManager, new object[]
			{
				proptertyID
			});
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x000658C7 File Offset: 0x00063AC7
		public static LocalizedString MigrationMailboxPermissionAdmin
		{
			get
			{
				return new LocalizedString("MigrationMailboxPermissionAdmin", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x000658E5 File Offset: 0x00063AE5
		public static LocalizedString ExInvalidItemId
		{
			get
			{
				return new LocalizedString("ExInvalidItemId", "Ex967D22", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x00065903 File Offset: 0x00063B03
		public static LocalizedString MapiCannotSetSpooler
		{
			get
			{
				return new LocalizedString("MapiCannotSetSpooler", "Ex503D52", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00065924 File Offset: 0x00063B24
		public static LocalizedString ExFilterNotSupported(object restriction)
		{
			return new LocalizedString("ExFilterNotSupported", "ExD66D33", false, true, ServerStrings.ResourceManager, new object[]
			{
				restriction
			});
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00065954 File Offset: 0x00063B54
		public static LocalizedString JunkEmailTrustedListOwnersEmailAddressException(string value)
		{
			return new LocalizedString("JunkEmailTrustedListOwnersEmailAddressException", "ExF3B776", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00065984 File Offset: 0x00063B84
		public static LocalizedString ErrorUnsupportedConfigurationXmlVersion(string category, string version)
		{
			return new LocalizedString("ErrorUnsupportedConfigurationXmlVersion", "Ex438911", false, true, ServerStrings.ResourceManager, new object[]
			{
				category,
				version
			});
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x000659B7 File Offset: 0x00063BB7
		public static LocalizedString InvalidSharingTargetRecipientException
		{
			get
			{
				return new LocalizedString("InvalidSharingTargetRecipientException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x000659D8 File Offset: 0x00063BD8
		public static LocalizedString ExInvalidMultivalueElement(int elementIndex)
		{
			return new LocalizedString("ExInvalidMultivalueElement", "ExA9B69F", false, true, ServerStrings.ResourceManager, new object[]
			{
				elementIndex
			});
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x00065A0C File Offset: 0x00063C0C
		public static LocalizedString RequestStateInProgress
		{
			get
			{
				return new LocalizedString("RequestStateInProgress", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x00065A2A File Offset: 0x00063C2A
		public static LocalizedString UnlockOOFHistory
		{
			get
			{
				return new LocalizedString("UnlockOOFHistory", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00065A48 File Offset: 0x00063C48
		public static LocalizedString PublicFolderHierarchySessionNull(string user)
		{
			return new LocalizedString("PublicFolderHierarchySessionNull", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x00065A77 File Offset: 0x00063C77
		public static LocalizedString ExMclCannotBeResolved
		{
			get
			{
				return new LocalizedString("ExMclCannotBeResolved", "Ex711F76", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x00065A95 File Offset: 0x00063C95
		public static LocalizedString FailedToReadConfiguration
		{
			get
			{
				return new LocalizedString("FailedToReadConfiguration", "Ex145E0E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x00065AB3 File Offset: 0x00063CB3
		public static LocalizedString ADUserNoMailbox
		{
			get
			{
				return new LocalizedString("ADUserNoMailbox", "Ex7DC61E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x00065AD1 File Offset: 0x00063CD1
		public static LocalizedString ExReadExchangeTopologyFailed
		{
			get
			{
				return new LocalizedString("ExReadExchangeTopologyFailed", "ExA104A7", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00065AF0 File Offset: 0x00063CF0
		public static LocalizedString NotificationEmailSubjectCompleted(string notificationType)
		{
			return new LocalizedString("NotificationEmailSubjectCompleted", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				notificationType
			});
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00065B20 File Offset: 0x00063D20
		public static LocalizedString ErrorTeamMailboxUserTypeUnqualified(string users)
		{
			return new LocalizedString("ErrorTeamMailboxUserTypeUnqualified", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				users
			});
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x00065B4F File Offset: 0x00063D4F
		public static LocalizedString MigrationUserStatusCorrupted
		{
			get
			{
				return new LocalizedString("MigrationUserStatusCorrupted", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x00065B6D File Offset: 0x00063D6D
		public static LocalizedString ExMatchShouldHaveBeenCalled
		{
			get
			{
				return new LocalizedString("ExMatchShouldHaveBeenCalled", "ExE03785", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x00065B8B File Offset: 0x00063D8B
		public static LocalizedString ExCannotModifyRemovedRecipient
		{
			get
			{
				return new LocalizedString("ExCannotModifyRemovedRecipient", "Ex43F3A2", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00065BA9 File Offset: 0x00063DA9
		public static LocalizedString InvalidDateTimeFormat
		{
			get
			{
				return new LocalizedString("InvalidDateTimeFormat", "ExC829C4", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x00065BC7 File Offset: 0x00063DC7
		public static LocalizedString OleConversionPrepareFailed
		{
			get
			{
				return new LocalizedString("OleConversionPrepareFailed", "ExD62870", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x00065BE5 File Offset: 0x00063DE5
		public static LocalizedString NotificationEmailBodyExportPSTFailed
		{
			get
			{
				return new LocalizedString("NotificationEmailBodyExportPSTFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00065C03 File Offset: 0x00063E03
		public static LocalizedString ExRangeNotSet
		{
			get
			{
				return new LocalizedString("ExRangeNotSet", "Ex22F0CB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00065C24 File Offset: 0x00063E24
		public static LocalizedString ConversationCreatorSidNotSet(string conversationId, string rootDeliveredTime)
		{
			return new LocalizedString("ConversationCreatorSidNotSet", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				conversationId,
				rootDeliveredTime
			});
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00065C58 File Offset: 0x00063E58
		public static LocalizedString ExNullParameter(string argName, int argNumber)
		{
			return new LocalizedString("ExNullParameter", "Ex551FFA", false, true, ServerStrings.ResourceManager, new object[]
			{
				argName,
				argNumber
			});
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00065C90 File Offset: 0x00063E90
		public static LocalizedString ExEmptyCollection(string argument)
		{
			return new LocalizedString("ExEmptyCollection", "ExE9E1AE", false, true, ServerStrings.ResourceManager, new object[]
			{
				argument
			});
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00065CBF File Offset: 0x00063EBF
		public static LocalizedString OrganizationNotFederatedException
		{
			get
			{
				return new LocalizedString("OrganizationNotFederatedException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x00065CDD File Offset: 0x00063EDD
		public static LocalizedString ClientCulture_0x421
		{
			get
			{
				return new LocalizedString("ClientCulture_0x421", "Ex5A41A3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x00065CFB File Offset: 0x00063EFB
		public static LocalizedString ACLTooBig
		{
			get
			{
				return new LocalizedString("ACLTooBig", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00065D1C File Offset: 0x00063F1C
		public static LocalizedString MigrationAttachmentNotFound(string attachment)
		{
			return new LocalizedString("MigrationAttachmentNotFound", "ExC93802", false, true, ServerStrings.ResourceManager, new object[]
			{
				attachment
			});
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x00065D4B File Offset: 0x00063F4B
		public static LocalizedString TeamMailboxMessageNotConnectedToSite
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageNotConnectedToSite", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00065D6C File Offset: 0x00063F6C
		public static LocalizedString ActiveManagerUnknownGenericRpcCommand(int requestedServerVersion, int replyingServerVersion, int commandId)
		{
			return new LocalizedString("ActiveManagerUnknownGenericRpcCommand", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				requestedServerVersion,
				replyingServerVersion,
				commandId
			});
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00065DB2 File Offset: 0x00063FB2
		public static LocalizedString TeamMailboxSyncStatusFailed
		{
			get
			{
				return new LocalizedString("TeamMailboxSyncStatusFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00065DD0 File Offset: 0x00063FD0
		public static LocalizedString ClientCulture_0x80A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x80A", "Ex13DC11", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00065DF0 File Offset: 0x00063FF0
		public static LocalizedString RecipientAddressInvalid(string user)
		{
			return new LocalizedString("RecipientAddressInvalid", "Ex76B8AB", false, true, ServerStrings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00065E20 File Offset: 0x00064020
		public static LocalizedString ExInvalidExceptionInfoSubstringLength(int lengthWithNull, int lengthWithoutNull)
		{
			return new LocalizedString("ExInvalidExceptionInfoSubstringLength", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				lengthWithNull,
				lengthWithoutNull
			});
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00065E60 File Offset: 0x00064060
		public static LocalizedString DagNetworkCannotRemoveActiveSubnet(string name)
		{
			return new LocalizedString("DagNetworkCannotRemoveActiveSubnet", "Ex753A61", false, true, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00065E8F File Offset: 0x0006408F
		public static LocalizedString FailedToBindToUseLicense
		{
			get
			{
				return new LocalizedString("FailedToBindToUseLicense", "Ex435C84", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x00065EAD File Offset: 0x000640AD
		public static LocalizedString MapiCannotGetParentId
		{
			get
			{
				return new LocalizedString("MapiCannotGetParentId", "Ex8339EA", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x00065ECB File Offset: 0x000640CB
		public static LocalizedString ExEndDateEarlierThanStartDate
		{
			get
			{
				return new LocalizedString("ExEndDateEarlierThanStartDate", "ExF31D58", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00065EEC File Offset: 0x000640EC
		public static LocalizedString ExInvalidNullParameterForChangeTypes(string parameterName, string changeTypes)
		{
			return new LocalizedString("ExInvalidNullParameterForChangeTypes", "Ex297F8D", false, true, ServerStrings.ResourceManager, new object[]
			{
				parameterName,
				changeTypes
			});
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00065F1F File Offset: 0x0006411F
		public static LocalizedString ExInvalidCustomSerializationData
		{
			get
			{
				return new LocalizedString("ExInvalidCustomSerializationData", "Ex166206", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00065F40 File Offset: 0x00064140
		public static LocalizedString ConversionMaxBodyPartsExceeded(int maxbodyparts)
		{
			return new LocalizedString("ConversionMaxBodyPartsExceeded", "Ex3586B5", false, true, ServerStrings.ResourceManager, new object[]
			{
				maxbodyparts
			});
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00065F74 File Offset: 0x00064174
		public static LocalizedString ConversionLimitsExceeded
		{
			get
			{
				return new LocalizedString("ConversionLimitsExceeded", "Ex993D9E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x00065F92 File Offset: 0x00064192
		public static LocalizedString LargeMultivaluedPropertiesNotSupportedInTNEF
		{
			get
			{
				return new LocalizedString("LargeMultivaluedPropertiesNotSupportedInTNEF", "Ex331CDB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00065FB0 File Offset: 0x000641B0
		public static LocalizedString MigrationSkippableStepSettingTargetAddress
		{
			get
			{
				return new LocalizedString("MigrationSkippableStepSettingTargetAddress", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00065FD0 File Offset: 0x000641D0
		public static LocalizedString JunkEmailTrustedListXsoDuplicateException(string value)
		{
			return new LocalizedString("JunkEmailTrustedListXsoDuplicateException", "Ex1338C2", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x00065FFF File Offset: 0x000641FF
		public static LocalizedString InvalidPropertyKey
		{
			get
			{
				return new LocalizedString("InvalidPropertyKey", "Ex29EB20", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x0006601D File Offset: 0x0006421D
		public static LocalizedString TwoDays
		{
			get
			{
				return new LocalizedString("TwoDays", "Ex185917", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0006603B File Offset: 0x0006423B
		public static LocalizedString ExOutlookSearchFolderDoesNotHaveMailboxSession
		{
			get
			{
				return new LocalizedString("ExOutlookSearchFolderDoesNotHaveMailboxSession", "ExEFC21C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0006605C File Offset: 0x0006425C
		public static LocalizedString CannotAuthenticateUserByTheClientSecurityContext(int error)
		{
			return new LocalizedString("CannotAuthenticateUserByTheClientSecurityContext", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00066090 File Offset: 0x00064290
		public static LocalizedString ClientCulture_0x426
		{
			get
			{
				return new LocalizedString("ClientCulture_0x426", "Ex8ADF47", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x000660AE File Offset: 0x000642AE
		public static LocalizedString MalformedAdrEntry
		{
			get
			{
				return new LocalizedString("MalformedAdrEntry", "Ex695734", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x000660CC File Offset: 0x000642CC
		public static LocalizedString FailedToFindIssuanceLicenseAndURI
		{
			get
			{
				return new LocalizedString("FailedToFindIssuanceLicenseAndURI", "ExB064E6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x000660EA File Offset: 0x000642EA
		public static LocalizedString InboxRuleMessageTypeEncrypted
		{
			get
			{
				return new LocalizedString("InboxRuleMessageTypeEncrypted", "Ex5D23CD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00066108 File Offset: 0x00064308
		public static LocalizedString SuffixMatchNotSupported
		{
			get
			{
				return new LocalizedString("SuffixMatchNotSupported", "ExE557D1", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x00066126 File Offset: 0x00064326
		public static LocalizedString idClientSessionInfoParseException
		{
			get
			{
				return new LocalizedString("idClientSessionInfoParseException", "Ex560AA8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00066144 File Offset: 0x00064344
		public static LocalizedString ExComparisonFilterPropertiesNotSupported(string property1, string property2)
		{
			return new LocalizedString("ExComparisonFilterPropertiesNotSupported", "ExC22DC7", false, true, ServerStrings.ResourceManager, new object[]
			{
				property1,
				property2
			});
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00066178 File Offset: 0x00064378
		public static LocalizedString CannotGetLongTermIdFromId(long id)
		{
			return new LocalizedString("CannotGetLongTermIdFromId", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000661AC File Offset: 0x000643AC
		public static LocalizedString RecoverableItemsAccessDeniedException(string folder)
		{
			return new LocalizedString("RecoverableItemsAccessDeniedException", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				folder
			});
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x000661DB File Offset: 0x000643DB
		public static LocalizedString ClientCulture_0x1009
		{
			get
			{
				return new LocalizedString("ClientCulture_0x1009", "Ex1CD3B8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x000661F9 File Offset: 0x000643F9
		public static LocalizedString OperationResultPartiallySucceeded
		{
			get
			{
				return new LocalizedString("OperationResultPartiallySucceeded", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x00066217 File Offset: 0x00064417
		public static LocalizedString UceContentFilterLoadFailure
		{
			get
			{
				return new LocalizedString("UceContentFilterLoadFailure", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00066235 File Offset: 0x00064435
		public static LocalizedString MapiRulesError
		{
			get
			{
				return new LocalizedString("MapiRulesError", "Ex5FDC87", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00066254 File Offset: 0x00064454
		public static LocalizedString ErrorExTimeZoneValueMultipleGmtMatches(string matches)
		{
			return new LocalizedString("ErrorExTimeZoneValueMultipleGmtMatches", "ExACE841", false, true, ServerStrings.ResourceManager, new object[]
			{
				matches
			});
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00066284 File Offset: 0x00064484
		public static LocalizedString ExUnsupportedCharset(string charset)
		{
			return new LocalizedString("ExUnsupportedCharset", "Ex392060", false, true, ServerStrings.ResourceManager, new object[]
			{
				charset
			});
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x000662B4 File Offset: 0x000644B4
		public static LocalizedString FolderRuleErrorInvalidGroup(string group)
		{
			return new LocalizedString("FolderRuleErrorInvalidGroup", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				group
			});
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x000662E3 File Offset: 0x000644E3
		public static LocalizedString MigrationBatchStatusRemoving
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusRemoving", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x00066301 File Offset: 0x00064501
		public static LocalizedString ExBadFolderEntryIdSize
		{
			get
			{
				return new LocalizedString("ExBadFolderEntryIdSize", "Ex240D07", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0006631F File Offset: 0x0006451F
		public static LocalizedString NotSupportedSharingMessageException
		{
			get
			{
				return new LocalizedString("NotSupportedSharingMessageException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00066340 File Offset: 0x00064540
		public static LocalizedString CannotGetIdFromLongTermId(string longTermId)
		{
			return new LocalizedString("CannotGetIdFromLongTermId", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				longTermId
			});
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0006636F File Offset: 0x0006456F
		public static LocalizedString IncompleteExchangePrincipal
		{
			get
			{
				return new LocalizedString("IncompleteExchangePrincipal", "Ex1168AF", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x0006638D File Offset: 0x0006458D
		public static LocalizedString MigrationFeatureMultiBatch
		{
			get
			{
				return new LocalizedString("MigrationFeatureMultiBatch", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x000663AC File Offset: 0x000645AC
		public static LocalizedString ErrorXsoObjectPropertyValidationError(string name, string details)
		{
			return new LocalizedString("ErrorXsoObjectPropertyValidationError", "ExF0028C", false, true, ServerStrings.ResourceManager, new object[]
			{
				name,
				details
			});
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x000663DF File Offset: 0x000645DF
		public static LocalizedString MapiCannotSetTableColumns
		{
			get
			{
				return new LocalizedString("MapiCannotSetTableColumns", "ExB76470", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x000663FD File Offset: 0x000645FD
		public static LocalizedString FailedToReadActivityLog
		{
			get
			{
				return new LocalizedString("FailedToReadActivityLog", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0006641C File Offset: 0x0006461C
		public static LocalizedString ErrorInvalidItemHoldPeriod(string name)
		{
			return new LocalizedString("ErrorInvalidItemHoldPeriod", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0006644B File Offset: 0x0006464B
		public static LocalizedString SearchOperationFailed
		{
			get
			{
				return new LocalizedString("SearchOperationFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0006646C File Offset: 0x0006466C
		public static LocalizedString ProvisioningMailboxNotFound(string mailboxId)
		{
			return new LocalizedString("ProvisioningMailboxNotFound", "ExD455D2", false, true, ServerStrings.ResourceManager, new object[]
			{
				mailboxId
			});
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0006649B File Offset: 0x0006469B
		public static LocalizedString ExDefaultJournalFilename
		{
			get
			{
				return new LocalizedString("ExDefaultJournalFilename", "ExC29219", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x000664B9 File Offset: 0x000646B9
		public static LocalizedString MapiCannotExpandRow
		{
			get
			{
				return new LocalizedString("MapiCannotExpandRow", "Ex3FE48C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000664D8 File Offset: 0x000646D8
		public static LocalizedString ExInvalidBase64StringFormat(string base64String)
		{
			return new LocalizedString("ExInvalidBase64StringFormat", "Ex15A662", false, true, ServerStrings.ResourceManager, new object[]
			{
				base64String
			});
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00066508 File Offset: 0x00064708
		public static LocalizedString ExInvalidWABObjectType(object objectType)
		{
			return new LocalizedString("ExInvalidWABObjectType", "ExBD7ABF", false, true, ServerStrings.ResourceManager, new object[]
			{
				objectType
			});
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x00066537 File Offset: 0x00064737
		public static LocalizedString ExCannotStartDeadSessionChecking
		{
			get
			{
				return new LocalizedString("ExCannotStartDeadSessionChecking", "ExA27A81", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00066555 File Offset: 0x00064755
		public static LocalizedString SearchStateNotStarted
		{
			get
			{
				return new LocalizedString("SearchStateNotStarted", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00066573 File Offset: 0x00064773
		public static LocalizedString SpellCheckerSwedish
		{
			get
			{
				return new LocalizedString("SpellCheckerSwedish", "ExC6A1C8", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00066591 File Offset: 0x00064791
		public static LocalizedString NotificationEmailSubjectExportPst
		{
			get
			{
				return new LocalizedString("NotificationEmailSubjectExportPst", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x000665AF File Offset: 0x000647AF
		public static LocalizedString ContentRestrictionOnSearchKey
		{
			get
			{
				return new LocalizedString("ContentRestrictionOnSearchKey", "Ex8C4D5C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x000665D0 File Offset: 0x000647D0
		public static LocalizedString AggregatedMailboxNotFound(string guid)
		{
			return new LocalizedString("AggregatedMailboxNotFound", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00066600 File Offset: 0x00064800
		public static LocalizedString AnchorServerNotFound(string mdbGuid)
		{
			return new LocalizedString("AnchorServerNotFound", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				mdbGuid
			});
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00066630 File Offset: 0x00064830
		public static LocalizedString ReminderPropertyNotSupported(string itemTypeName, string propertyName)
		{
			return new LocalizedString("ReminderPropertyNotSupported", "ExF73C00", false, true, ServerStrings.ResourceManager, new object[]
			{
				itemTypeName,
				propertyName
			});
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00066664 File Offset: 0x00064864
		public static LocalizedString RpcServerParameterInvalidError(string error)
		{
			return new LocalizedString("RpcServerParameterInvalidError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x00066693 File Offset: 0x00064893
		public static LocalizedString ExUnknownFilterType
		{
			get
			{
				return new LocalizedString("ExUnknownFilterType", "ExF16ACD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x000666B1 File Offset: 0x000648B1
		public static LocalizedString ClientCulture_0x400A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x400A", "Ex6929EA", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x000666D0 File Offset: 0x000648D0
		public static LocalizedString ExSearchFolderAlreadyExists(object Guid)
		{
			return new LocalizedString("ExSearchFolderAlreadyExists", "Ex92E0FE", false, true, ServerStrings.ResourceManager, new object[]
			{
				Guid
			});
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00066700 File Offset: 0x00064900
		public static LocalizedString CannotVerifyDRMPropsSignatureUserNotFound(string sid)
		{
			return new LocalizedString("CannotVerifyDRMPropsSignatureUserNotFound", "Ex484EF1", false, true, ServerStrings.ResourceManager, new object[]
			{
				sid
			});
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00066730 File Offset: 0x00064930
		public static LocalizedString DataMoveReplicationConstraintSatisfiedForNonReplicatedDatabase(DataMoveReplicationConstraintParameter constraint, Guid databaseGuid)
		{
			return new LocalizedString("DataMoveReplicationConstraintSatisfiedForNonReplicatedDatabase", "Ex2A91E8", false, true, ServerStrings.ResourceManager, new object[]
			{
				constraint,
				databaseGuid
			});
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00066770 File Offset: 0x00064970
		public static LocalizedString ExCannotStampDefaultFolderId(string saveResult)
		{
			return new LocalizedString("ExCannotStampDefaultFolderId", "Ex206127", false, true, ServerStrings.ResourceManager, new object[]
			{
				saveResult
			});
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0006679F File Offset: 0x0006499F
		public static LocalizedString UnableToLoadDrmMessage
		{
			get
			{
				return new LocalizedString("UnableToLoadDrmMessage", "Ex71096F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x000667BD File Offset: 0x000649BD
		public static LocalizedString ExCannotParseValue
		{
			get
			{
				return new LocalizedString("ExCannotParseValue", "Ex2A507B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x000667DC File Offset: 0x000649DC
		public static LocalizedString FailedToRpcAcquireRacAndClc(string orgId, string status, string server)
		{
			return new LocalizedString("FailedToRpcAcquireRacAndClc", "Ex884C3B", false, true, ServerStrings.ResourceManager, new object[]
			{
				orgId,
				status,
				server
			});
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x00066813 File Offset: 0x00064A13
		public static LocalizedString ClientCulture_0x4001
		{
			get
			{
				return new LocalizedString("ClientCulture_0x4001", "Ex50E7A1", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00066834 File Offset: 0x00064A34
		public static LocalizedString InvalidCacheEntryId(int id)
		{
			return new LocalizedString("InvalidCacheEntryId", "ExBE4A26", false, true, ServerStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x00066868 File Offset: 0x00064A68
		public static LocalizedString TeamMailboxMessageToLearnMore
		{
			get
			{
				return new LocalizedString("TeamMailboxMessageToLearnMore", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00066886 File Offset: 0x00064A86
		public static LocalizedString MigrationBatchStatusCompleting
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusCompleting", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x000668A4 File Offset: 0x00064AA4
		public static LocalizedString MapiCannotGetRowCount
		{
			get
			{
				return new LocalizedString("MapiCannotGetRowCount", "ExE2D314", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x000668C2 File Offset: 0x00064AC2
		public static LocalizedString FolderRuleStageOnDeliveredMessage
		{
			get
			{
				return new LocalizedString("FolderRuleStageOnDeliveredMessage", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000668E0 File Offset: 0x00064AE0
		public static LocalizedString FailedToDownloadCertificationMExData(Uri url)
		{
			return new LocalizedString("FailedToDownloadCertificationMExData", "Ex574FE8", false, true, ServerStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00066910 File Offset: 0x00064B10
		public static LocalizedString MigrationGroupMembersAlreadyAvailable(string groupSmtpAddress)
		{
			return new LocalizedString("MigrationGroupMembersAlreadyAvailable", "ExDCCBA7", false, true, ServerStrings.ResourceManager, new object[]
			{
				groupSmtpAddress
			});
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00066940 File Offset: 0x00064B40
		public static LocalizedString TimeZoneReferenceWithNullTimeZone(string timeZoneId)
		{
			return new LocalizedString("TimeZoneReferenceWithNullTimeZone", "ExDC929F", false, true, ServerStrings.ResourceManager, new object[]
			{
				timeZoneId
			});
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00066970 File Offset: 0x00064B70
		public static LocalizedString ExInvalidBodyFormat(string format)
		{
			return new LocalizedString("ExInvalidBodyFormat", "ExE5F978", false, true, ServerStrings.ResourceManager, new object[]
			{
				format
			});
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000669A0 File Offset: 0x00064BA0
		public static LocalizedString idUnableToAddDefaultTaskFolderToDefaultTaskGroup(string folderType)
		{
			return new LocalizedString("idUnableToAddDefaultTaskFolderToDefaultTaskGroup", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				folderType
			});
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000669D0 File Offset: 0x00064BD0
		public static LocalizedString ReplayServiceDown(string serverName, string rpcErrorMessage)
		{
			return new LocalizedString("ReplayServiceDown", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				serverName,
				rpcErrorMessage
			});
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00066A04 File Offset: 0x00064C04
		public static LocalizedString FailedToAcquireTenantLicenses(string tenantId, string uri)
		{
			return new LocalizedString("FailedToAcquireTenantLicenses", "ExC3BC0A", false, true, ServerStrings.ResourceManager, new object[]
			{
				tenantId,
				uri
			});
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00066A38 File Offset: 0x00064C38
		public static LocalizedString ExModifiedOccurrenceCantHaveStartDateAsAdjacentOccurrence(object startDate, object adjacentStartDate)
		{
			return new LocalizedString("ExModifiedOccurrenceCantHaveStartDateAsAdjacentOccurrence", "ExE8F0D6", false, true, ServerStrings.ResourceManager, new object[]
			{
				startDate,
				adjacentStartDate
			});
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x00066A6B File Offset: 0x00064C6B
		public static LocalizedString MigrationBatchSupportedActionAppend
		{
			get
			{
				return new LocalizedString("MigrationBatchSupportedActionAppend", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00066A89 File Offset: 0x00064C89
		public static LocalizedString ExCannotOpenRejectedItem
		{
			get
			{
				return new LocalizedString("ExCannotOpenRejectedItem", "Ex92B024", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00066AA7 File Offset: 0x00064CA7
		public static LocalizedString MigrationBatchStatusFailed
		{
			get
			{
				return new LocalizedString("MigrationBatchStatusFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00066AC5 File Offset: 0x00064CC5
		public static LocalizedString VotingDataCorrupt
		{
			get
			{
				return new LocalizedString("VotingDataCorrupt", "Ex8B1234", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00066AE4 File Offset: 0x00064CE4
		public static LocalizedString WrongTimeZoneReference(string timeZoneId)
		{
			return new LocalizedString("WrongTimeZoneReference", "ExCC0652", false, true, ServerStrings.ResourceManager, new object[]
			{
				timeZoneId
			});
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00066B13 File Offset: 0x00064D13
		public static LocalizedString MigrationTestMSAFailed
		{
			get
			{
				return new LocalizedString("MigrationTestMSAFailed", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x00066B31 File Offset: 0x00064D31
		public static LocalizedString TeamMailboxSyncStatusDocumentAndMembershipSyncFailure
		{
			get
			{
				return new LocalizedString("TeamMailboxSyncStatusDocumentAndMembershipSyncFailure", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00066B50 File Offset: 0x00064D50
		public static LocalizedString ExConstraintViolationByteArrayLengthTooLong(string propertyName, long maxLength, long actualLength)
		{
			return new LocalizedString("ExConstraintViolationByteArrayLengthTooLong", "Ex1A6202", false, true, ServerStrings.ResourceManager, new object[]
			{
				propertyName,
				maxLength,
				actualLength
			});
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00066B94 File Offset: 0x00064D94
		public static LocalizedString ExPropertyError(string propertyName, string propertyErrorCode, string providerDescription)
		{
			return new LocalizedString("ExPropertyError", "ExC0E5EC", false, true, ServerStrings.ResourceManager, new object[]
			{
				propertyName,
				propertyErrorCode,
				providerDescription
			});
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00066BCC File Offset: 0x00064DCC
		public static LocalizedString ExInvalidChangeType(string changeType)
		{
			return new LocalizedString("ExInvalidChangeType", "ExF23224", false, true, ServerStrings.ResourceManager, new object[]
			{
				changeType
			});
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x00066BFB File Offset: 0x00064DFB
		public static LocalizedString PublicFoldersCannotBeMovedDuringMigration
		{
			get
			{
				return new LocalizedString("PublicFoldersCannotBeMovedDuringMigration", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00066C19 File Offset: 0x00064E19
		public static LocalizedString ConversionNoReplayContent
		{
			get
			{
				return new LocalizedString("ConversionNoReplayContent", "Ex2F3847", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00066C37 File Offset: 0x00064E37
		public static LocalizedString ClientCulture_0x404
		{
			get
			{
				return new LocalizedString("ClientCulture_0x404", "ExF292C6", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00066C58 File Offset: 0x00064E58
		public static LocalizedString ExSaveFailedBecauseOfConflicts(object itemId)
		{
			return new LocalizedString("ExSaveFailedBecauseOfConflicts", "ExE0CF58", false, true, ServerStrings.ResourceManager, new object[]
			{
				itemId
			});
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00066C88 File Offset: 0x00064E88
		public static LocalizedString SharingFolderNameWithSuffix(string folderName, int suffix)
		{
			return new LocalizedString("SharingFolderNameWithSuffix", "Ex9CF231", false, true, ServerStrings.ResourceManager, new object[]
			{
				folderName,
				suffix
			});
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00066CC0 File Offset: 0x00064EC0
		public static LocalizedString ExPropertyNotStreamable(string property)
		{
			return new LocalizedString("ExPropertyNotStreamable", "Ex170CD7", false, true, ServerStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00066CF0 File Offset: 0x00064EF0
		public static LocalizedString MigrationInvalidTargetProxyAddress(string email)
		{
			return new LocalizedString("MigrationInvalidTargetProxyAddress", "Ex61F981", false, true, ServerStrings.ResourceManager, new object[]
			{
				email
			});
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00066D1F File Offset: 0x00064F1F
		public static LocalizedString ExCantSubmitWithoutRecipients
		{
			get
			{
				return new LocalizedString("ExCantSubmitWithoutRecipients", "Ex11767B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x00066D3D File Offset: 0x00064F3D
		public static LocalizedString LastErrorMessage
		{
			get
			{
				return new LocalizedString("LastErrorMessage", "ExC64014", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00066D5C File Offset: 0x00064F5C
		public static LocalizedString RepairingIsNotSetSinceMonitorEntryIsNotFound(string monitorName, string targetResource)
		{
			return new LocalizedString("RepairingIsNotSetSinceMonitorEntryIsNotFound", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				monitorName,
				targetResource
			});
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x00066D8F File Offset: 0x00064F8F
		public static LocalizedString JunkEmailFolderNotFoundException
		{
			get
			{
				return new LocalizedString("JunkEmailFolderNotFoundException", "Ex954A7C", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00066DAD File Offset: 0x00064FAD
		public static LocalizedString ClientCulture_0x42A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x42A", "Ex2E1237", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00066DCC File Offset: 0x00064FCC
		public static LocalizedString PublicFolderConnectionThreadLimitExceeded(int limit)
		{
			return new LocalizedString("PublicFolderConnectionThreadLimitExceeded", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				limit
			});
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00066E00 File Offset: 0x00065000
		public static LocalizedString AmMoveNotApplicableForDbException
		{
			get
			{
				return new LocalizedString("AmMoveNotApplicableForDbException", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00066E20 File Offset: 0x00065020
		public static LocalizedString TeamMailboxMessageMemberInvitationSubject(string tmName)
		{
			return new LocalizedString("TeamMailboxMessageMemberInvitationSubject", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				tmName
			});
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00066E50 File Offset: 0x00065050
		public static LocalizedString ExInvalidValueForFlagsCalculatedProperty(int flag)
		{
			return new LocalizedString("ExInvalidValueForFlagsCalculatedProperty", "ExC2422A", false, true, ServerStrings.ResourceManager, new object[]
			{
				flag
			});
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00066E84 File Offset: 0x00065084
		public static LocalizedString ADUserNotFoundId(object id)
		{
			return new LocalizedString("ADUserNotFoundId", "Ex62D59A", false, true, ServerStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x00066EB3 File Offset: 0x000650B3
		public static LocalizedString ExCantUndeleteOccurrence
		{
			get
			{
				return new LocalizedString("ExCantUndeleteOccurrence", "Ex4D7D4E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x00066ED1 File Offset: 0x000650D1
		public static LocalizedString MigrationUserStatusQueued
		{
			get
			{
				return new LocalizedString("MigrationUserStatusQueued", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00066EF0 File Offset: 0x000650F0
		public static LocalizedString InvalidUrlScheme(ServiceType type, Uri url)
		{
			return new LocalizedString("InvalidUrlScheme", "ExBC6F98", false, true, ServerStrings.ResourceManager, new object[]
			{
				type,
				url
			});
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00066F28 File Offset: 0x00065128
		public static LocalizedString ExTypeSerializationNotSupported(Type type)
		{
			return new LocalizedString("ExTypeSerializationNotSupported", "Ex17D281", false, true, ServerStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00066F57 File Offset: 0x00065157
		public static LocalizedString DateRangeSixMonths
		{
			get
			{
				return new LocalizedString("DateRangeSixMonths", "Ex446F58", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00066F78 File Offset: 0x00065178
		public static LocalizedString MigrationMRSJobMissing(string identity)
		{
			return new LocalizedString("MigrationMRSJobMissing", "Ex72DB06", false, true, ServerStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00066FA8 File Offset: 0x000651A8
		public static LocalizedString FailedToCheckPublishLicenseOwnership(string organizationId)
		{
			return new LocalizedString("FailedToCheckPublishLicenseOwnership", "Ex30E3D1", false, true, ServerStrings.ResourceManager, new object[]
			{
				organizationId
			});
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00066FD7 File Offset: 0x000651D7
		public static LocalizedString MigrationSkippableStepNone
		{
			get
			{
				return new LocalizedString("MigrationSkippableStepNone", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00066FF8 File Offset: 0x000651F8
		public static LocalizedString CannotSynchronizeManifestEx(Type mapiManifestType, object clientPhase, object manifestPhase)
		{
			return new LocalizedString("CannotSynchronizeManifestEx", "Ex0673D0", false, true, ServerStrings.ResourceManager, new object[]
			{
				mapiManifestType,
				clientPhase,
				manifestPhase
			});
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0006702F File Offset: 0x0006522F
		public static LocalizedString MalformedCommentRestriction
		{
			get
			{
				return new LocalizedString("MalformedCommentRestriction", "ExEF5D98", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x0006704D File Offset: 0x0006524D
		public static LocalizedString ClientCulture_0x427
		{
			get
			{
				return new LocalizedString("ClientCulture_0x427", "Ex2F7231", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0006706B File Offset: 0x0006526B
		public static LocalizedString MigrationUserStatusSummaryActive
		{
			get
			{
				return new LocalizedString("MigrationUserStatusSummaryActive", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0006708C File Offset: 0x0006528C
		public static LocalizedString StoreDataInvalid(string value)
		{
			return new LocalizedString("StoreDataInvalid", "Ex981545", false, true, ServerStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x000670BB File Offset: 0x000652BB
		public static LocalizedString ErrorEmptyFolderNotSupported
		{
			get
			{
				return new LocalizedString("ErrorEmptyFolderNotSupported", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x000670D9 File Offset: 0x000652D9
		public static LocalizedString ClientCulture_0x540A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x540A", "Ex13031F", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x000670F7 File Offset: 0x000652F7
		public static LocalizedString ClientCulture_0x2C0A
		{
			get
			{
				return new LocalizedString("ClientCulture_0x2C0A", "Ex774EC0", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x00067115 File Offset: 0x00065315
		public static LocalizedString MissingOperand
		{
			get
			{
				return new LocalizedString("MissingOperand", "ExCCBD0D", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x00067133 File Offset: 0x00065333
		public static LocalizedString DuplicateAction
		{
			get
			{
				return new LocalizedString("DuplicateAction", "Ex69B50E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00067154 File Offset: 0x00065354
		public static LocalizedString ExTooManyInstancesOnSeries(uint maxNumberOfInstances)
		{
			return new LocalizedString("ExTooManyInstancesOnSeries", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				maxNumberOfInstances
			});
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x00067188 File Offset: 0x00065388
		public static LocalizedString SearchStateQueued
		{
			get
			{
				return new LocalizedString("SearchStateQueued", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x000671A6 File Offset: 0x000653A6
		public static LocalizedString ExQueryPropertyBagRowNotSet
		{
			get
			{
				return new LocalizedString("ExQueryPropertyBagRowNotSet", "ExE6FCB3", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x000671C4 File Offset: 0x000653C4
		public static LocalizedString Sunday
		{
			get
			{
				return new LocalizedString("Sunday", "Ex74DD10", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x000671E2 File Offset: 0x000653E2
		public static LocalizedString WeatherUnitDefault
		{
			get
			{
				return new LocalizedString("WeatherUnitDefault", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x00067200 File Offset: 0x00065400
		public static LocalizedString RequestSecurityTokenException
		{
			get
			{
				return new LocalizedString("RequestSecurityTokenException", "Ex878A42", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00067220 File Offset: 0x00065420
		public static LocalizedString InvalidAddressError(string legacyDn)
		{
			return new LocalizedString("InvalidAddressError", "", false, false, ServerStrings.ResourceManager, new object[]
			{
				legacyDn
			});
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00067250 File Offset: 0x00065450
		public static LocalizedString ExGetNotSupportedForCalculatedProperty(object proptertyID)
		{
			return new LocalizedString("ExGetNotSupportedForCalculatedProperty", "ExE56D7A", false, true, ServerStrings.ResourceManager, new object[]
			{
				proptertyID
			});
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x0006727F File Offset: 0x0006547F
		public static LocalizedString ExNoOccurrencesInRecurrence
		{
			get
			{
				return new LocalizedString("ExNoOccurrencesInRecurrence", "Ex318F74", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0006729D File Offset: 0x0006549D
		public static LocalizedString ExInvalidMclXml
		{
			get
			{
				return new LocalizedString("ExInvalidMclXml", "ExFA882B", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x000672BB File Offset: 0x000654BB
		public static LocalizedString ExInvalidIdFormat
		{
			get
			{
				return new LocalizedString("ExInvalidIdFormat", "ExE0A13E", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x000672D9 File Offset: 0x000654D9
		public static LocalizedString ExAdminAuditLogsFolderAccessDenied
		{
			get
			{
				return new LocalizedString("ExAdminAuditLogsFolderAccessDenied", "ExAFE4DD", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x000672F7 File Offset: 0x000654F7
		public static LocalizedString ClientCulture_0xC04
		{
			get
			{
				return new LocalizedString("ClientCulture_0xC04", "ExD84C19", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00067318 File Offset: 0x00065518
		public static LocalizedString ExTooManyObjects(string objectName, int count, int limits)
		{
			return new LocalizedString("ExTooManyObjects", "Ex47A925", false, true, ServerStrings.ResourceManager, new object[]
			{
				objectName,
				count,
				limits
			});
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00067359 File Offset: 0x00065559
		public static LocalizedString ExInvalidComparisionOperatorInPropertyComparisionFilter
		{
			get
			{
				return new LocalizedString("ExInvalidComparisionOperatorInPropertyComparisionFilter", "ExBCC9DB", false, true, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x00067377 File Offset: 0x00065577
		public static LocalizedString AsyncOperationTypeExportPST
		{
			get
			{
				return new LocalizedString("AsyncOperationTypeExportPST", "", false, false, ServerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00067395 File Offset: 0x00065595
		public static LocalizedString GetLocalizedString(ServerStrings.IDs key)
		{
			return new LocalizedString(ServerStrings.stringIDs[(uint)key], ServerStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000372 RID: 882
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(971);

		// Token: 0x04000373 RID: 883
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Data.Storage.ServerStrings", typeof(ServerStrings).GetTypeInfo().Assembly);

		// Token: 0x020000B6 RID: 182
		public enum IDs : uint
		{
			// Token: 0x04000375 RID: 885
			MissingRightsManagementLicense = 2721128427U,
			// Token: 0x04000376 RID: 886
			ServerLocatorServiceTransientFault = 195662054U,
			// Token: 0x04000377 RID: 887
			EmailAddressMissing = 824541142U,
			// Token: 0x04000378 RID: 888
			CannotShareSearchFolder = 2906390296U,
			// Token: 0x04000379 RID: 889
			EstimateStateStopping = 1390813357U,
			// Token: 0x0400037A RID: 890
			SpellCheckerDutch = 1028519129U,
			// Token: 0x0400037B RID: 891
			SpellCheckerNorwegianNynorsk = 2064592175U,
			// Token: 0x0400037C RID: 892
			MigrationFlagsStart = 307085623U,
			// Token: 0x0400037D RID: 893
			TeamMailboxMessageSiteAndSiteMailboxDetails = 4243709143U,
			// Token: 0x0400037E RID: 894
			CannotGetSupportedRoutingTypes = 3398846764U,
			// Token: 0x0400037F RID: 895
			ClientCulture_0x816 = 4287963345U,
			// Token: 0x04000380 RID: 896
			AsyncOperationTypeMailboxRestore = 1588035907U,
			// Token: 0x04000381 RID: 897
			MatchContainerClassValidationFailed = 1805838968U,
			// Token: 0x04000382 RID: 898
			ExCannotCreateSubfolderUnderSearchFolder = 3004251640U,
			// Token: 0x04000383 RID: 899
			InboxRuleImportanceHigh = 161866662U,
			// Token: 0x04000384 RID: 900
			MapiCopyFailedProperties = 1003186114U,
			// Token: 0x04000385 RID: 901
			ClientCulture_0x3C0A = 1489962323U,
			// Token: 0x04000386 RID: 902
			ErrorTeamMailboxGetUsersNullResponse = 2738718017U,
			// Token: 0x04000387 RID: 903
			MigrationBatchSupportedActionNone = 2367323544U,
			// Token: 0x04000388 RID: 904
			ExAuditsUpdateDenied = 2198921793U,
			// Token: 0x04000389 RID: 905
			ExBadMessageEntryIdSize = 2134621305U,
			// Token: 0x0400038A RID: 906
			ExAdminAuditLogsUpdateDenied = 2993889924U,
			// Token: 0x0400038B RID: 907
			ExInvalidNumberOfOccurrences = 949829336U,
			// Token: 0x0400038C RID: 908
			OleConversionFailed = 1342791165U,
			// Token: 0x0400038D RID: 909
			ClientCulture_0x3401 = 1551862906U,
			// Token: 0x0400038E RID: 910
			ExCannotReadFolderData = 346824164U,
			// Token: 0x0400038F RID: 911
			InboxRuleSensitivityNormal = 1533116792U,
			// Token: 0x04000390 RID: 912
			SpellCheckerCatalan = 2788778759U,
			// Token: 0x04000391 RID: 913
			TeamMailboxMessageHowToGetStarted = 894291518U,
			// Token: 0x04000392 RID: 914
			ExInvalidMasterValueAndColumnLength = 2688169016U,
			// Token: 0x04000393 RID: 915
			MigrationBatchStatusStopping = 1002199416U,
			// Token: 0x04000394 RID: 916
			ClientCulture_0x440A = 2264323463U,
			// Token: 0x04000395 RID: 917
			ExFolderAlreadyExistsInClientState = 3064105318U,
			// Token: 0x04000396 RID: 918
			InvalidPermissionsEntry = 3192255565U,
			// Token: 0x04000397 RID: 919
			ConversionInternalFailure = 753703675U,
			// Token: 0x04000398 RID: 920
			MigrationTypeExchangeOutlookAnywhere = 150715741U,
			// Token: 0x04000399 RID: 921
			ClientCulture_0x4809 = 630982379U,
			// Token: 0x0400039A RID: 922
			MigrationAutodiscoverConfigurationFailure = 2605826718U,
			// Token: 0x0400039B RID: 923
			ExDefaultContactFilename = 273958411U,
			// Token: 0x0400039C RID: 924
			TeamMailboxMessageReopenClosedSiteMailbox = 687935806U,
			// Token: 0x0400039D RID: 925
			MigrationObjectsCountStringPFs = 3547809333U,
			// Token: 0x0400039E RID: 926
			ExCannotCreateRecurringMeetingWithoutTimeZone = 1305341899U,
			// Token: 0x0400039F RID: 927
			ExInvalidSaveOnCorrelatedItem = 1790240390U,
			// Token: 0x040003A0 RID: 928
			ErrorTeamMailboxGetListItemChangesNullResponse = 2801555237U,
			// Token: 0x040003A1 RID: 929
			ExCorruptedTimeZone = 3339597578U,
			// Token: 0x040003A2 RID: 930
			MigrationUserStatusSummaryStopped = 1540857282U,
			// Token: 0x040003A3 RID: 931
			InboxRuleSensitivityCompanyConfidential = 113768464U,
			// Token: 0x040003A4 RID: 932
			FailedToAddAttachments = 3597498295U,
			// Token: 0x040003A5 RID: 933
			MapiCannotDeliverItem = 379773582U,
			// Token: 0x040003A6 RID: 934
			MapiCannotGetLocalRepIds = 2576522508U,
			// Token: 0x040003A7 RID: 935
			ClientCulture_0x3C01 = 3647547459U,
			// Token: 0x040003A8 RID: 936
			FirstDay = 209873198U,
			// Token: 0x040003A9 RID: 937
			ClientCulture_0x41D = 4287963599U,
			// Token: 0x040003AA RID: 938
			PersonIsAlreadyLinkedWithGALContact = 3944203608U,
			// Token: 0x040003AB RID: 939
			InboxRuleMessageTypeCalendaring = 303788285U,
			// Token: 0x040003AC RID: 940
			Editor = 3653626825U,
			// Token: 0x040003AD RID: 941
			CannotShareOtherPersonalFolder = 4226852782U,
			// Token: 0x040003AE RID: 942
			TeamMailboxMessageClosedBodyIntroText = 540789371U,
			// Token: 0x040003AF RID: 943
			InboxRuleMessageTypeSigned = 2800053981U,
			// Token: 0x040003B0 RID: 944
			MigrationTypePSTImport = 1894870732U,
			// Token: 0x040003B1 RID: 945
			DateRangeOneYear = 179758620U,
			// Token: 0x040003B2 RID: 946
			ExAuditsFolderAccessDenied = 1682339598U,
			// Token: 0x040003B3 RID: 947
			ClientCulture_0x1409 = 1908093332U,
			// Token: 0x040003B4 RID: 948
			NoExternalOwaAvailableException = 3485169071U,
			// Token: 0x040003B5 RID: 949
			DelegateNotSupportedFolder = 60274512U,
			// Token: 0x040003B6 RID: 950
			MigrationBatchDirectionLocal = 3906173474U,
			// Token: 0x040003B7 RID: 951
			ErrorFolderDeleted = 3130219463U,
			// Token: 0x040003B8 RID: 952
			BadDateTimeFormatInChangeDate = 3911406152U,
			// Token: 0x040003B9 RID: 953
			ClientCulture_0x1C0A = 1489962389U,
			// Token: 0x040003BA RID: 954
			TeamMailboxMessageNoActionText = 2011085458U,
			// Token: 0x040003BB RID: 955
			InvalidMigrationBatchId = 4201517212U,
			// Token: 0x040003BC RID: 956
			FolderRuleStageOnPromotedMessage = 3924992590U,
			// Token: 0x040003BD RID: 957
			idUnableToCreateDefaultTaskGroupException = 1870702413U,
			// Token: 0x040003BE RID: 958
			PublicFolderStartSyncFolderHierarchyRpcFailed = 194841891U,
			// Token: 0x040003BF RID: 959
			SearchStateFailed = 149719578U,
			// Token: 0x040003C0 RID: 960
			ClientCulture_0x2401 = 1551862809U,
			// Token: 0x040003C1 RID: 961
			MapiCannotWritePerUserInformation = 3503392803U,
			// Token: 0x040003C2 RID: 962
			SearchNameCharacterConstraint = 2750670341U,
			// Token: 0x040003C3 RID: 963
			InboxRuleMessageTypeCalendaringResponse = 4022186804U,
			// Token: 0x040003C4 RID: 964
			ErrorTimeProposalInvalidWhenNotAllowedByOrganizer = 2114183459U,
			// Token: 0x040003C5 RID: 965
			RequestStateCertExpired = 981561289U,
			// Token: 0x040003C6 RID: 966
			Thursday = 1760294240U,
			// Token: 0x040003C7 RID: 967
			MapiCannotDeleteAttachment = 1553527118U,
			// Token: 0x040003C8 RID: 968
			ExEventsNotSupportedForDelegateUser = 1512287666U,
			// Token: 0x040003C9 RID: 969
			AsyncOperationTypeImportPST = 4181674605U,
			// Token: 0x040003CA RID: 970
			MigrationStepDataMigration = 4056598008U,
			// Token: 0x040003CB RID: 971
			JunkEmailBlockedListXsoTooManyException = 2239334052U,
			// Token: 0x040003CC RID: 972
			ClientCulture_0x409 = 2721879535U,
			// Token: 0x040003CD RID: 973
			ErrorFolderCreationIsBlocked = 3828388569U,
			// Token: 0x040003CE RID: 974
			ExInvalidParticipantEntryId = 3332812272U,
			// Token: 0x040003CF RID: 975
			ExInvalidSpecifierValueError = 3512980427U,
			// Token: 0x040003D0 RID: 976
			TeamMailboxSyncStatusDocumentSyncFailureOnly = 4064061502U,
			// Token: 0x040003D1 RID: 977
			SearchStateInProgress = 2549870927U,
			// Token: 0x040003D2 RID: 978
			JunkEmailInvalidOperationException = 2171204805U,
			// Token: 0x040003D3 RID: 979
			TeamMailboxMessageWhatIsSiteMailbox = 1029618557U,
			// Token: 0x040003D4 RID: 980
			SpellCheckerFrench = 2603495179U,
			// Token: 0x040003D5 RID: 981
			ExUnknownRecurrenceBlobRange = 192606181U,
			// Token: 0x040003D6 RID: 982
			ExAttachmentCannotOpenDueToUnSave = 1753710770U,
			// Token: 0x040003D7 RID: 983
			ClientCulture_0x439 = 3125164062U,
			// Token: 0x040003D8 RID: 984
			SpellCheckerEnglishCanada = 3837865737U,
			// Token: 0x040003D9 RID: 985
			MapiCannotUpdateDeferredActionMessages = 3771526820U,
			// Token: 0x040003DA RID: 986
			MigrationStatisticsCompleteStatus = 2014714852U,
			// Token: 0x040003DB RID: 987
			OleConversionInvalidResultType = 737023398U,
			// Token: 0x040003DC RID: 988
			UnableToMakeAutoDiscoveryRequest = 3684140834U,
			// Token: 0x040003DD RID: 989
			NotificationEmailSubjectImportPst = 4266601947U,
			// Token: 0x040003DE RID: 990
			SharingMessageAttachmentNotFoundException = 1338408148U,
			// Token: 0x040003DF RID: 991
			MigrationBatchStatusIncrementalSyncing = 2642389795U,
			// Token: 0x040003E0 RID: 992
			SearchStateStopped = 4084900412U,
			// Token: 0x040003E1 RID: 993
			PublicFolderMailboxNotFound = 2354834546U,
			// Token: 0x040003E2 RID: 994
			MapiCannotGetReceiveFolder = 3021899907U,
			// Token: 0x040003E3 RID: 995
			MigrationUserStatusStarting = 445051701U,
			// Token: 0x040003E4 RID: 996
			AmDbMountNotAllowedDueToLossException = 2210183777U,
			// Token: 0x040003E5 RID: 997
			SpellCheckerGermanPreReform = 770684443U,
			// Token: 0x040003E6 RID: 998
			InvalidKindFormat = 2412724648U,
			// Token: 0x040003E7 RID: 999
			OleUnableToReadAttachment = 905358579U,
			// Token: 0x040003E8 RID: 1000
			InvalidModifier = 2666546556U,
			// Token: 0x040003E9 RID: 1001
			MigrationUserStatusProvisionUpdating = 3840447572U,
			// Token: 0x040003EA RID: 1002
			MigrationMailboxPermissionFullAccess = 594875070U,
			// Token: 0x040003EB RID: 1003
			WeatherUnitCelsius = 3928045320U,
			// Token: 0x040003EC RID: 1004
			NullDateInChangeDate = 2953752128U,
			// Token: 0x040003ED RID: 1005
			ClientCulture_0x2C09 = 4003777722U,
			// Token: 0x040003EE RID: 1006
			MigrationBatchDirectionOffboarding = 2084642916U,
			// Token: 0x040003EF RID: 1007
			CalNotifTypeUninteresting = 194112229U,
			// Token: 0x040003F0 RID: 1008
			AmDatabaseNeverMountedException = 2088104068U,
			// Token: 0x040003F1 RID: 1009
			MapiCannotSetReceiveFolder = 870651383U,
			// Token: 0x040003F2 RID: 1010
			RpcClientWrapperFailedToLoadTopology = 3000352360U,
			// Token: 0x040003F3 RID: 1011
			ExCorruptConversationActionItem = 3609586398U,
			// Token: 0x040003F4 RID: 1012
			Tuesday = 2820941203U,
			// Token: 0x040003F5 RID: 1013
			MapiCannotCreateRestriction = 3647326574U,
			// Token: 0x040003F6 RID: 1014
			CorruptJunkMoveStamp = 2969695521U,
			// Token: 0x040003F7 RID: 1015
			InvalidAttachmentNumber = 599296811U,
			// Token: 0x040003F8 RID: 1016
			ClientCulture_0x83E = 3125164046U,
			// Token: 0x040003F9 RID: 1017
			Friday = 4094875965U,
			// Token: 0x040003FA RID: 1018
			NoServerValueAvailable = 1586627622U,
			// Token: 0x040003FB RID: 1019
			DelegateInvalidPermission = 1690131397U,
			// Token: 0x040003FC RID: 1020
			OperationAborted = 1769372998U,
			// Token: 0x040003FD RID: 1021
			DiscoveryMailboxNotFound = 380587417U,
			// Token: 0x040003FE RID: 1022
			ClientCulture_0x422 = 1559080126U,
			// Token: 0x040003FF RID: 1023
			MigrationUserStatusSummarySynced = 434577051U,
			// Token: 0x04000400 RID: 1024
			FourHours = 656727185U,
			// Token: 0x04000401 RID: 1025
			MigrationUserStatusCompleted = 3033708196U,
			// Token: 0x04000402 RID: 1026
			ExVotingBlobCorrupt = 3797097900U,
			// Token: 0x04000403 RID: 1027
			HexadecimalHtmlColorPatternDescription = 912807925U,
			// Token: 0x04000404 RID: 1028
			MigrationStepInitialization = 3420516204U,
			// Token: 0x04000405 RID: 1029
			TeamMailboxSyncStatusMembershipSyncFailureOnly = 3118133575U,
			// Token: 0x04000406 RID: 1030
			InvalidEncryptedSharedFolderDataException = 788681379U,
			// Token: 0x04000407 RID: 1031
			ExCorruptRestrictionFilter = 3198793348U,
			// Token: 0x04000408 RID: 1032
			ErrorNotificationAlreadyExists = 3806649575U,
			// Token: 0x04000409 RID: 1033
			ExItemIsOpenedInReadOnlyMode = 1743342709U,
			// Token: 0x0400040A RID: 1034
			UnbalancedParenthesis = 2479239329U,
			// Token: 0x0400040B RID: 1035
			InvalidRpMsgFormat = 3259603877U,
			// Token: 0x0400040C RID: 1036
			UserPhotoNotAnImage = 586169060U,
			// Token: 0x0400040D RID: 1037
			MapiCannotCreateEntryIdFromShortTermId = 1079495974U,
			// Token: 0x0400040E RID: 1038
			ClientCulture_0x807 = 2721879405U,
			// Token: 0x0400040F RID: 1039
			MapiCannotCreateBookmark = 3407343648U,
			// Token: 0x04000410 RID: 1040
			InvalidateDateRange = 630534408U,
			// Token: 0x04000411 RID: 1041
			MigrationUserAdminTypeUnknown = 1290997994U,
			// Token: 0x04000412 RID: 1042
			CannotMoveOrCopyBetweenPrivateAndPublicMailbox = 2519000101U,
			// Token: 0x04000413 RID: 1043
			SpellCheckerNorwegianBokmal = 2234262993U,
			// Token: 0x04000414 RID: 1044
			ClientCulture_0x1007 = 3279312390U,
			// Token: 0x04000415 RID: 1045
			MigrationBatchSupportedActionStop = 3025564610U,
			// Token: 0x04000416 RID: 1046
			CrossForestNotSupported = 1295345518U,
			// Token: 0x04000417 RID: 1047
			ExCannotAccessSystemFolderId = 2475132438U,
			// Token: 0x04000418 RID: 1048
			ClientCulture_0x410 = 4287963483U,
			// Token: 0x04000419 RID: 1049
			ExStatefulFilterMustBeSetWhenSetSyncFiltersIsInvokedWithNullFilter = 153308512U,
			// Token: 0x0400041A RID: 1050
			MapiCannotReadPermissions = 2948566116U,
			// Token: 0x0400041B RID: 1051
			FolderRuleStageOnPublicFolderBefore = 3983802523U,
			// Token: 0x0400041C RID: 1052
			UnbalancedQuote = 3551282863U,
			// Token: 0x0400041D RID: 1053
			FailedToWriteActivityLog = 2092669490U,
			// Token: 0x0400041E RID: 1054
			NoFreeBusyFolder = 3948653022U,
			// Token: 0x0400041F RID: 1055
			ClientCulture_0x408 = 2721879534U,
			// Token: 0x04000420 RID: 1056
			SharingConflictException = 4050068885U,
			// Token: 0x04000421 RID: 1057
			MapiCannotQueryColumns = 329227683U,
			// Token: 0x04000422 RID: 1058
			ExCaughtMapiExceptionWhileReadingEvents = 914093861U,
			// Token: 0x04000423 RID: 1059
			ClientCulture_0x81D = 4287963459U,
			// Token: 0x04000424 RID: 1060
			ConversionInvalidSmimeContent = 1324407795U,
			// Token: 0x04000425 RID: 1061
			ExCannotRevertSentMeetingToAppointment = 1786112539U,
			// Token: 0x04000426 RID: 1062
			ExMustSaveFolderToMakeVisibleToOutlook = 1566523742U,
			// Token: 0x04000427 RID: 1063
			UnsupportedContentRestriction = 309976908U,
			// Token: 0x04000428 RID: 1064
			ClientCulture_0x42D = 1559080244U,
			// Token: 0x04000429 RID: 1065
			NotificationEmailBodyImportPSTCreated = 443718431U,
			// Token: 0x0400042A RID: 1066
			SearchTargetInSource = 59886577U,
			// Token: 0x0400042B RID: 1067
			ExAddItemAttachmentFailed = 809558091U,
			// Token: 0x0400042C RID: 1068
			ClientCulture_0x403 = 2721879541U,
			// Token: 0x0400042D RID: 1069
			ExCannotMoveOrDeleteDefaultFolders = 1083109339U,
			// Token: 0x0400042E RID: 1070
			ExCannotSeekRow = 3923324564U,
			// Token: 0x0400042F RID: 1071
			MigrationReportBatch = 3620611820U,
			// Token: 0x04000430 RID: 1072
			ExErrorInDetectE15Store = 2187234323U,
			// Token: 0x04000431 RID: 1073
			idDefaultFoldersNotLocalizedException = 2391045796U,
			// Token: 0x04000432 RID: 1074
			MigrationStateCompleted = 2969986172U,
			// Token: 0x04000433 RID: 1075
			ErrorMissingMailboxOrPermission = 297411134U,
			// Token: 0x04000434 RID: 1076
			ClientCulture_0x140C = 1101524278U,
			// Token: 0x04000435 RID: 1077
			MigrationTypeIMAP = 2192010005U,
			// Token: 0x04000436 RID: 1078
			ClientCulture_0x2809 = 630982445U,
			// Token: 0x04000437 RID: 1079
			ClientCulture_0x1404 = 1955147499U,
			// Token: 0x04000438 RID: 1080
			ExCannotRejectDeletes = 984546473U,
			// Token: 0x04000439 RID: 1081
			TeamMailboxSyncStatusMaintenanceSyncFailureOnly = 415920568U,
			// Token: 0x0400043A RID: 1082
			MigrationStateStopped = 564935456U,
			// Token: 0x0400043B RID: 1083
			MigrationStageDiscovery = 3735242122U,
			// Token: 0x0400043C RID: 1084
			ClientCulture_0x40A = 2721879655U,
			// Token: 0x0400043D RID: 1085
			NotificationEmailBodyCertExpired = 3791574374U,
			// Token: 0x0400043E RID: 1086
			ExCannotRejectSameOperationTwice = 1657454002U,
			// Token: 0x0400043F RID: 1087
			ExCannotGetSearchCriteria = 3371920351U,
			// Token: 0x04000440 RID: 1088
			ExInvalidMaxQueueSize = 3800790638U,
			// Token: 0x04000441 RID: 1089
			ADException = 1437123480U,
			// Token: 0x04000442 RID: 1090
			ExNoMailboxOwner = 653269905U,
			// Token: 0x04000443 RID: 1091
			ExNotConnected = 4002989775U,
			// Token: 0x04000444 RID: 1092
			SearchStateStopping = 478700673U,
			// Token: 0x04000445 RID: 1093
			SpellCheckerKorean = 1750841907U,
			// Token: 0x04000446 RID: 1094
			MigrationTypeExchangeLocalMove = 1435478051U,
			// Token: 0x04000447 RID: 1095
			MapiCannotSubmitMessage = 2120544431U,
			// Token: 0x04000448 RID: 1096
			ClientCulture_0x1C09 = 4003777885U,
			// Token: 0x04000449 RID: 1097
			ExInvalidOrder = 1458958954U,
			// Token: 0x0400044A RID: 1098
			NoProviderSupportShareFolder = 725138746U,
			// Token: 0x0400044B RID: 1099
			ExConnectionCacheSizeNotSet = 3332361397U,
			// Token: 0x0400044C RID: 1100
			MigrationFlagsRemove = 4100720225U,
			// Token: 0x0400044D RID: 1101
			ExInvalidRecipient = 2084653907U,
			// Token: 0x0400044E RID: 1102
			ExFoundInvalidRowType = 1532066664U,
			// Token: 0x0400044F RID: 1103
			ExInvalidOffset = 3156797033U,
			// Token: 0x04000450 RID: 1104
			NotEnoughPermissionsToPerformOperation = 2795849522U,
			// Token: 0x04000451 RID: 1105
			MigrationStatisticsPartiallyCompleteStatus = 3321416304U,
			// Token: 0x04000452 RID: 1106
			TeamMailboxSyncStatusNotAvailable = 1147224948U,
			// Token: 0x04000453 RID: 1107
			InvalidOperator = 3791300057U,
			// Token: 0x04000454 RID: 1108
			DefaultHtmlAttachmentHrefText = 1063299331U,
			// Token: 0x04000455 RID: 1109
			ConversionBodyCorrupt = 2883857825U,
			// Token: 0x04000456 RID: 1110
			ClientCulture_0x1407 = 2358432026U,
			// Token: 0x04000457 RID: 1111
			MapiCannotSaveChanges = 2091534526U,
			// Token: 0x04000458 RID: 1112
			RejectedSuggestionPersonIdSameAsPersonId = 1712595778U,
			// Token: 0x04000459 RID: 1113
			ErrorInvalidPhoneNumberFormat = 2860711473U,
			// Token: 0x0400045A RID: 1114
			MigrationStateCorrupted = 1551718751U,
			// Token: 0x0400045B RID: 1115
			ProvisioningRequestCsvContainsNeitherPasswordNorFederatedIdentity = 3286105202U,
			// Token: 0x0400045C RID: 1116
			SecurityPrincipalAlreadyDefined = 3657645519U,
			// Token: 0x0400045D RID: 1117
			KqlParseException = 968748330U,
			// Token: 0x0400045E RID: 1118
			ExEventNotFound = 2414329026U,
			// Token: 0x0400045F RID: 1119
			ThreeDays = 1227907325U,
			// Token: 0x04000460 RID: 1120
			ExInvalidSortLength = 278097122U,
			// Token: 0x04000461 RID: 1121
			MapiCannotGetPerUserLongTermIds = 3752161350U,
			// Token: 0x04000462 RID: 1122
			ExFolderWithoutMapiProp = 2656172439U,
			// Token: 0x04000463 RID: 1123
			ExChangeKeyTooLong = 3755862658U,
			// Token: 0x04000464 RID: 1124
			ExUnknownRestrictionType = 2902058685U,
			// Token: 0x04000465 RID: 1125
			ExInvalidRowCount = 91197877U,
			// Token: 0x04000466 RID: 1126
			UnsupportedExistRestriction = 2720991162U,
			// Token: 0x04000467 RID: 1127
			AvailabilityOnly = 4121599705U,
			// Token: 0x04000468 RID: 1128
			MapiCannotExecuteWithInternalAccess = 1399515408U,
			// Token: 0x04000469 RID: 1129
			ExItemNoParentId = 2879753762U,
			// Token: 0x0400046A RID: 1130
			MigrationTypePublicFolder = 1107465087U,
			// Token: 0x0400046B RID: 1131
			MapiCannotGetPerUserGuid = 130796183U,
			// Token: 0x0400046C RID: 1132
			FederationNotEnabled = 2521346493U,
			// Token: 0x0400046D RID: 1133
			RequestStateWaitingForFinalization = 241258410U,
			// Token: 0x0400046E RID: 1134
			RequestStateCompleted = 1176987915U,
			// Token: 0x0400046F RID: 1135
			TooManyCultures = 493948276U,
			// Token: 0x04000470 RID: 1136
			MapiCannotSetCollapseState = 3358444168U,
			// Token: 0x04000471 RID: 1137
			IncompleteUserInformationToAccessGroupMailbox = 1834533699U,
			// Token: 0x04000472 RID: 1138
			ClientCulture_0x2001 = 2116512813U,
			// Token: 0x04000473 RID: 1139
			CannotImportMessageChange = 1016962785U,
			// Token: 0x04000474 RID: 1140
			InvalidTimesInTimeSlot = 3299931409U,
			// Token: 0x04000475 RID: 1141
			MigrationReportFinalizationFailure = 3086195034U,
			// Token: 0x04000476 RID: 1142
			StructuredQueryException = 3050947916U,
			// Token: 0x04000477 RID: 1143
			ExUnknownResponseType = 628804796U,
			// Token: 0x04000478 RID: 1144
			RequestStateCreated = 2214930724U,
			// Token: 0x04000479 RID: 1145
			ExInvalidComparisonOperatorInComparisonFilter = 2474224463U,
			// Token: 0x0400047A RID: 1146
			MigrationFolderSettings = 3920537899U,
			// Token: 0x0400047B RID: 1147
			ClientCulture_0x809 = 2721879419U,
			// Token: 0x0400047C RID: 1148
			ExUnsupportedSeekReference = 3653170115U,
			// Token: 0x0400047D RID: 1149
			MigrationBatchStatusCompleted = 3031733457U,
			// Token: 0x0400047E RID: 1150
			MigrationTestMSAWarning = 713262539U,
			// Token: 0x0400047F RID: 1151
			InvalidDateTimeRange = 2595969499U,
			// Token: 0x04000480 RID: 1152
			MapiCannotGetMapiTable = 2125622397U,
			// Token: 0x04000481 RID: 1153
			MapiCannotCheckForNotifications = 1130047631U,
			// Token: 0x04000482 RID: 1154
			CannotStamplocalFreeBusyId = 977128087U,
			// Token: 0x04000483 RID: 1155
			ClientCulture_0x100A = 2828973696U,
			// Token: 0x04000484 RID: 1156
			MigrationBatchStatusSynced = 1893783426U,
			// Token: 0x04000485 RID: 1157
			ExchangePrincipalFromMailboxDataError = 2428434059U,
			// Token: 0x04000486 RID: 1158
			MigrationUserStatusIncrementalFailed = 3615217052U,
			// Token: 0x04000487 RID: 1159
			InvalidXml = 3321965778U,
			// Token: 0x04000488 RID: 1160
			ExEntryIdToLong = 223559137U,
			// Token: 0x04000489 RID: 1161
			ClientCulture_0x420 = 1559080128U,
			// Token: 0x0400048A RID: 1162
			PrincipalFromDifferentSite = 697160562U,
			// Token: 0x0400048B RID: 1163
			ErrorSavingRules = 3415665237U,
			// Token: 0x0400048C RID: 1164
			PublishedFolderAccessDeniedException = 1802620674U,
			// Token: 0x0400048D RID: 1165
			PublicFoldersNotEnabledForEnterprise = 305670746U,
			// Token: 0x0400048E RID: 1166
			InboxRuleMessageTypeApprovalRequest = 3382673089U,
			// Token: 0x0400048F RID: 1167
			NonUniqueRecipientError = 4042709143U,
			// Token: 0x04000490 RID: 1168
			ExSystemFolderAccessDenied = 3548482161U,
			// Token: 0x04000491 RID: 1169
			MapiCannotRemoveNotification = 3379789351U,
			// Token: 0x04000492 RID: 1170
			ClientCulture_0x180A = 1699673688U,
			// Token: 0x04000493 RID: 1171
			ExCommentFilterPropertiesNotSupported = 3770139034U,
			// Token: 0x04000494 RID: 1172
			ExDictionaryDataCorruptedNullKey = 2450087029U,
			// Token: 0x04000495 RID: 1173
			MigrationBatchStatusStarting = 3165677092U,
			// Token: 0x04000496 RID: 1174
			ClientCulture_0x300A = 2828973630U,
			// Token: 0x04000497 RID: 1175
			ExBadValueForTypeCode0 = 1987097643U,
			// Token: 0x04000498 RID: 1176
			ErrorTimeProposalInvalidOnRecurringMaster = 510893832U,
			// Token: 0x04000499 RID: 1177
			SearchStateDeletionInProgress = 2114464927U,
			// Token: 0x0400049A RID: 1178
			ExRuleIdInvalid = 1600317547U,
			// Token: 0x0400049B RID: 1179
			MapiCannotCollapseRow = 3959385997U,
			// Token: 0x0400049C RID: 1180
			SharingUnableToGenerateEncryptedSharedFolderData = 3606405084U,
			// Token: 0x0400049D RID: 1181
			ExConnectionNotCached = 3825572190U,
			// Token: 0x0400049E RID: 1182
			CVSPopulationTimedout = 931793818U,
			// Token: 0x0400049F RID: 1183
			BadDateFormatInChangeDate = 300160841U,
			// Token: 0x040004A0 RID: 1184
			MigrationBatchStatusCompletedWithErrors = 391772390U,
			// Token: 0x040004A1 RID: 1185
			NotReadSubjectPrefix = 765916263U,
			// Token: 0x040004A2 RID: 1186
			MapiCannotFinishSubmit = 1428661747U,
			// Token: 0x040004A3 RID: 1187
			ClientCulture_0xC01 = 2721875976U,
			// Token: 0x040004A4 RID: 1188
			ExItemNotFoundInClientManifest = 2811676186U,
			// Token: 0x040004A5 RID: 1189
			ErrorNoStoreObjectId = 2612888606U,
			// Token: 0x040004A6 RID: 1190
			CalendarItemCorrelationFailed = 2194255608U,
			// Token: 0x040004A7 RID: 1191
			ExInvalidOccurrenceId = 260083086U,
			// Token: 0x040004A8 RID: 1192
			DateRangeOneWeek = 2814533567U,
			// Token: 0x040004A9 RID: 1193
			EnforceRulesQuota = 2512850293U,
			// Token: 0x040004AA RID: 1194
			ExInvalidMonth = 1837976028U,
			// Token: 0x040004AB RID: 1195
			MigrationUserStatusCompletionSynced = 3370782263U,
			// Token: 0x040004AC RID: 1196
			FirstFullWeek = 1880367925U,
			// Token: 0x040004AD RID: 1197
			MigrationFeatureEndpoints = 3254542318U,
			// Token: 0x040004AE RID: 1198
			ExNoSearchHasBeenInitiated = 1291493387U,
			// Token: 0x040004AF RID: 1199
			MigrationUserStatusIncrementalSyncing = 1916012218U,
			// Token: 0x040004B0 RID: 1200
			PublicFolderMailboxesCannotBeCreatedDuringMigration = 2113494780U,
			// Token: 0x040004B1 RID: 1201
			MapiCannotCreateFilter = 2841260818U,
			// Token: 0x040004B2 RID: 1202
			MapiCannotNotifyMessageNewMail = 932673369U,
			// Token: 0x040004B3 RID: 1203
			MigrationUserStatusSyncing = 1644908980U,
			// Token: 0x040004B4 RID: 1204
			MigrationBatchFlagForceNewMigration = 2220521549U,
			// Token: 0x040004B5 RID: 1205
			CannotGetFinalStateSynchronizerProviderBase = 2553874506U,
			// Token: 0x040004B6 RID: 1206
			ServerLocatorClientWCFCallCommunicationError = 2057374892U,
			// Token: 0x040004B7 RID: 1207
			ExValueCannotBeNull = 553140815U,
			// Token: 0x040004B8 RID: 1208
			ClientCulture_0x3009 = 2472743270U,
			// Token: 0x040004B9 RID: 1209
			MigrationTypeBulkProvisioning = 2764365677U,
			// Token: 0x040004BA RID: 1210
			ErrorFolderIsMailEnabled = 156083260U,
			// Token: 0x040004BB RID: 1211
			ExCantAccessOccurrenceFromNewItem = 2030923649U,
			// Token: 0x040004BC RID: 1212
			ConversionCorruptContent = 1309170924U,
			// Token: 0x040004BD RID: 1213
			AutoDFailedToGetToken = 2021640990U,
			// Token: 0x040004BE RID: 1214
			ExCorruptPropertyTag = 4205544601U,
			// Token: 0x040004BF RID: 1215
			InvalidTimeSlot = 77158404U,
			// Token: 0x040004C0 RID: 1216
			ExCannotOpenMultipleCorrelatedItems = 3242781799U,
			// Token: 0x040004C1 RID: 1217
			ErrorLanguageIsNull = 902677431U,
			// Token: 0x040004C2 RID: 1218
			ExInvalidAcrBaseProfiles = 372387097U,
			// Token: 0x040004C3 RID: 1219
			ExMustSaveFolderToApplySearch = 2802946662U,
			// Token: 0x040004C4 RID: 1220
			ExReadTopologyTimeout = 2753071453U,
			// Token: 0x040004C5 RID: 1221
			ExUnknownRecurrenceBlobType = 3333234210U,
			// Token: 0x040004C6 RID: 1222
			ClientCulture_0x419 = 4287963476U,
			// Token: 0x040004C7 RID: 1223
			SpellCheckerHebrew = 113369212U,
			// Token: 0x040004C8 RID: 1224
			ClientCulture_0x1001 = 2116512976U,
			// Token: 0x040004C9 RID: 1225
			InvalidAttachmentId = 1820555509U,
			// Token: 0x040004CA RID: 1226
			ClientCulture_0x43F = 3125164183U,
			// Token: 0x040004CB RID: 1227
			ExInvalidFolderId = 899245499U,
			// Token: 0x040004CC RID: 1228
			AmDbMountNotAllowedDueToRegistryConfigurationException = 1012814431U,
			// Token: 0x040004CD RID: 1229
			CannotSaveReadOnlyAttachment = 911781703U,
			// Token: 0x040004CE RID: 1230
			InvalidTnef = 3787888884U,
			// Token: 0x040004CF RID: 1231
			MigrationUserStatusIncrementalSynced = 392648307U,
			// Token: 0x040004D0 RID: 1232
			ExAdminAuditLogsDeleteDenied = 2071885718U,
			// Token: 0x040004D1 RID: 1233
			ConversionInvalidMessageCodepageCharset = 2114545814U,
			// Token: 0x040004D2 RID: 1234
			ClientCulture_0x40C = 2721879653U,
			// Token: 0x040004D3 RID: 1235
			DumpsterStatusShutdownException = 2379279263U,
			// Token: 0x040004D4 RID: 1236
			CannotDeleteRootFolder = 1345910406U,
			// Token: 0x040004D5 RID: 1237
			MapiCannotGetEffectiveRights = 1856386122U,
			// Token: 0x040004D6 RID: 1238
			InvalidMechanismToAccessGroupMailbox = 3136620084U,
			// Token: 0x040004D7 RID: 1239
			MapiCannotSavePermissions = 3536723681U,
			// Token: 0x040004D8 RID: 1240
			ClientCulture_0x1004 = 2876027863U,
			// Token: 0x040004D9 RID: 1241
			MigrationBatchStatusCreated = 3588257392U,
			// Token: 0x040004DA RID: 1242
			NotAllowedExternalSharingByPolicy = 2776843979U,
			// Token: 0x040004DB RID: 1243
			InboxRuleMessageTypeReadReceipt = 1311410701U,
			// Token: 0x040004DC RID: 1244
			StoreOperationFailed = 1490728717U,
			// Token: 0x040004DD RID: 1245
			ErrorExTimeZoneValueNoGmtMatch = 33074401U,
			// Token: 0x040004DE RID: 1246
			ExStoreObjectValidationError = 3552080970U,
			// Token: 0x040004DF RID: 1247
			MigrationBatchFlagNone = 1010688174U,
			// Token: 0x040004E0 RID: 1248
			ClientCulture_0x4009 = 2472743107U,
			// Token: 0x040004E1 RID: 1249
			TooManyAttachmentsOnProtectedMessage = 3188685413U,
			// Token: 0x040004E2 RID: 1250
			PublicFolderOpenFailedOnExistingFolder = 3397519574U,
			// Token: 0x040004E3 RID: 1251
			ExInvalidSortOrder = 1522765994U,
			// Token: 0x040004E4 RID: 1252
			ReplyRuleNotSupportedOnNonMailPublicFolder = 3678175251U,
			// Token: 0x040004E5 RID: 1253
			ExGetPropsFailed = 2626172332U,
			// Token: 0x040004E6 RID: 1254
			EstimateStateSucceeded = 2344409522U,
			// Token: 0x040004E7 RID: 1255
			MigrationBatchSupportedActionRemove = 2621679806U,
			// Token: 0x040004E8 RID: 1256
			MapiCannotSaveMessageStream = 2545687272U,
			// Token: 0x040004E9 RID: 1257
			MapiInvalidId = 3515331893U,
			// Token: 0x040004EA RID: 1258
			ContactLinkingMaximumNumberOfContactsPerPersonError = 2975245987U,
			// Token: 0x040004EB RID: 1259
			ConversionUnsupportedContent = 2827742042U,
			// Token: 0x040004EC RID: 1260
			MigrationUserStatusIncrementalStopped = 4287122418U,
			// Token: 0x040004ED RID: 1261
			MapiCannotCreateMessage = 955524647U,
			// Token: 0x040004EE RID: 1262
			InvalidSendAddressIdentity = 968419245U,
			// Token: 0x040004EF RID: 1263
			ClientCulture_0x425 = 1559080133U,
			// Token: 0x040004F0 RID: 1264
			DisposeOOFHistoryFolder = 2559314081U,
			// Token: 0x040004F1 RID: 1265
			ExCantDeleteLastOccurrence = 1115353353U,
			// Token: 0x040004F2 RID: 1266
			MigrationReportBatchSuccess = 3283674777U,
			// Token: 0x040004F3 RID: 1267
			ErrorAccessingLargeProperty = 4284023892U,
			// Token: 0x040004F4 RID: 1268
			OperationNotSupportedOnPublicFolderMailbox = 208132458U,
			// Token: 0x040004F5 RID: 1269
			ExCannotCreateMeetingCancellation = 872597966U,
			// Token: 0x040004F6 RID: 1270
			MigrationFeaturePAW = 3533048780U,
			// Token: 0x040004F7 RID: 1271
			InboxRuleFlagStatusFlagged = 2850981798U,
			// Token: 0x040004F8 RID: 1272
			JunkEmailBlockedListXsoNullException = 135679156U,
			// Token: 0x040004F9 RID: 1273
			ClientCulture_0x43E = 3125164186U,
			// Token: 0x040004FA RID: 1274
			TeamMailboxMessageGoToYourGroupSite = 1695521020U,
			// Token: 0x040004FB RID: 1275
			ClientCulture_0x81A = 4287963464U,
			// Token: 0x040004FC RID: 1276
			CannotImportMessageMove = 2076121062U,
			// Token: 0x040004FD RID: 1277
			FifteenMinutes = 1860805300U,
			// Token: 0x040004FE RID: 1278
			OneDays = 823631819U,
			// Token: 0x040004FF RID: 1279
			CorruptNaturalLanguageProperty = 1680647297U,
			// Token: 0x04000500 RID: 1280
			DumpsterStatusAlreadyStartedException = 2312979504U,
			// Token: 0x04000501 RID: 1281
			ExCannotSetSearchCriteria = 4217834195U,
			// Token: 0x04000502 RID: 1282
			ExBadObjectType = 192747935U,
			// Token: 0x04000503 RID: 1283
			SpellCheckerFinnish = 298653586U,
			// Token: 0x04000504 RID: 1284
			MigrationBatchStatusWaiting = 3098387483U,
			// Token: 0x04000505 RID: 1285
			UnsupportedKindKeywords = 4038623627U,
			// Token: 0x04000506 RID: 1286
			ClientCulture_0x407 = 2721879545U,
			// Token: 0x04000507 RID: 1287
			PropertyChangeMetadataParseError = 2516633257U,
			// Token: 0x04000508 RID: 1288
			SyncFailedToCreateNewItemOrBindToExistingOne = 2642058832U,
			// Token: 0x04000509 RID: 1289
			ConversionFailedInvalidMacBin = 3516269798U,
			// Token: 0x0400050A RID: 1290
			SpellCheckerEnglishUnitedStates = 435552220U,
			// Token: 0x0400050B RID: 1291
			ExContactHasNoId = 1551474375U,
			// Token: 0x0400050C RID: 1292
			ErrorExTimeZoneValueTimeZoneNotFound = 2860195473U,
			// Token: 0x0400050D RID: 1293
			SpellCheckerGermanPostReform = 1337636428U,
			// Token: 0x0400050E RID: 1294
			InboxRuleMessageTypePermissionControlled = 851950942U,
			// Token: 0x0400050F RID: 1295
			ClientCulture_0x40F = 2721879656U,
			// Token: 0x04000510 RID: 1296
			PropertyDefinitionsValuesNotMatch = 1310001827U,
			// Token: 0x04000511 RID: 1297
			ClientCulture_0xC1A = 4287959997U,
			// Token: 0x04000512 RID: 1298
			DateRangeThreeMonths = 2706950694U,
			// Token: 0x04000513 RID: 1299
			ExConnectionAlternate = 3038510311U,
			// Token: 0x04000514 RID: 1300
			MigrationBatchSupportedActionStart = 2795037022U,
			// Token: 0x04000515 RID: 1301
			ClientCulture_0x402 = 2721879540U,
			// Token: 0x04000516 RID: 1302
			ExCannotAccessAdminAuditLogsFolderId = 3256433766U,
			// Token: 0x04000517 RID: 1303
			ClientCulture_0x424 = 1559080132U,
			// Token: 0x04000518 RID: 1304
			MigrationStateWaiting = 132681062U,
			// Token: 0x04000519 RID: 1305
			MigrationStageProcessing = 2224931539U,
			// Token: 0x0400051A RID: 1306
			Database = 662607817U,
			// Token: 0x0400051B RID: 1307
			MapiCannotGetTransportQueueFolderId = 1899965635U,
			// Token: 0x0400051C RID: 1308
			UnsupportedAction = 252713015U,
			// Token: 0x0400051D RID: 1309
			FolderRuleErrorInvalidRecipientEntryId = 305850519U,
			// Token: 0x0400051E RID: 1310
			TeamMailboxMessageGoToTheSite = 2536777203U,
			// Token: 0x0400051F RID: 1311
			TwelveHours = 834480874U,
			// Token: 0x04000520 RID: 1312
			MigrationStageInjection = 1710892423U,
			// Token: 0x04000521 RID: 1313
			MapiCannotGetContentsTable = 3729217742U,
			// Token: 0x04000522 RID: 1314
			EstimateStateStopped = 3285920218U,
			// Token: 0x04000523 RID: 1315
			NullWorkHours = 1888800485U,
			// Token: 0x04000524 RID: 1316
			MigrationUserStatusCompleting = 3088889153U,
			// Token: 0x04000525 RID: 1317
			FiveMinutes = 3726325313U,
			// Token: 0x04000526 RID: 1318
			InboxRuleMessageTypeVoicemail = 3449171400U,
			// Token: 0x04000527 RID: 1319
			SpellCheckerPortugueseBrasil = 3412096701U,
			// Token: 0x04000528 RID: 1320
			GenericFailureRMDecryption = 561027979U,
			// Token: 0x04000529 RID: 1321
			SpellCheckerEnglishAustralia = 312177309U,
			// Token: 0x0400052A RID: 1322
			NoDeferredActions = 3160415695U,
			// Token: 0x0400052B RID: 1323
			ErrorSetDateTimeFormatWithoutLanguage = 3022558012U,
			// Token: 0x0400052C RID: 1324
			ClientCulture_0x1801 = 987212968U,
			// Token: 0x0400052D RID: 1325
			MapiErrorParsingId = 2113411584U,
			// Token: 0x0400052E RID: 1326
			MigrationUserAdminTypePartnerTenant = 563787386U,
			// Token: 0x0400052F RID: 1327
			MigrationUserStatusStopped = 3598152156U,
			// Token: 0x04000530 RID: 1328
			MigrationReportBatchFailure = 909292782U,
			// Token: 0x04000531 RID: 1329
			MapiCannotCreateAttachment = 490979673U,
			// Token: 0x04000532 RID: 1330
			NotificationEmailBodyCertExpiring = 1946505183U,
			// Token: 0x04000533 RID: 1331
			MapiCannotReadPerUserInformation = 2470109498U,
			// Token: 0x04000534 RID: 1332
			ExInvalidSubFilterProperty = 2189690271U,
			// Token: 0x04000535 RID: 1333
			StockReplyTemplate = 1931248018U,
			// Token: 0x04000536 RID: 1334
			CalNotifTypeSummary = 3362131860U,
			// Token: 0x04000537 RID: 1335
			JunkEmailInvalidConstructionException = 633339293U,
			// Token: 0x04000538 RID: 1336
			MapiCannotCreateAssociatedMessage = 3309334123U,
			// Token: 0x04000539 RID: 1337
			ClientCulture_0x413 = 4287963482U,
			// Token: 0x0400053A RID: 1338
			MapiCannotSortTable = 1706900424U,
			// Token: 0x0400053B RID: 1339
			MigrationUserStatusStopping = 2822102721U,
			// Token: 0x0400053C RID: 1340
			MapiCannotGetRecipientTable = 1778962839U,
			// Token: 0x0400053D RID: 1341
			ExInvalidCallToTryUpdateCalendarItem = 2932685544U,
			// Token: 0x0400053E RID: 1342
			ClientCulture_0x406 = 2721879544U,
			// Token: 0x0400053F RID: 1343
			ExCannotAccessAuditsFolderId = 102022121U,
			// Token: 0x04000540 RID: 1344
			ExReadEventsFailed = 18047843U,
			// Token: 0x04000541 RID: 1345
			ExCannotQueryAssociatedTable = 727433418U,
			// Token: 0x04000542 RID: 1346
			ClientCulture_0x429 = 1559080121U,
			// Token: 0x04000543 RID: 1347
			MigrationStepProvisioningUpdate = 2303146172U,
			// Token: 0x04000544 RID: 1348
			TwoWeeks = 1311803567U,
			// Token: 0x04000545 RID: 1349
			MigrationFeatureUpgradeBlock = 4021122445U,
			// Token: 0x04000546 RID: 1350
			ExInvalidServiceType = 1530529173U,
			// Token: 0x04000547 RID: 1351
			NullTimeInChangeDate = 3873491039U,
			// Token: 0x04000548 RID: 1352
			ConversionInvalidSmimeClearSignedContent = 1192664468U,
			// Token: 0x04000549 RID: 1353
			RequestStateSuspended = 2464640173U,
			// Token: 0x0400054A RID: 1354
			MapiIsFromPublicStoreCheckFailed = 2393270488U,
			// Token: 0x0400054B RID: 1355
			ExCannotSendMeetingMessages = 4172275237U,
			// Token: 0x0400054C RID: 1356
			ExAuditsDeleteDenied = 2982637405U,
			// Token: 0x0400054D RID: 1357
			ClientCulture_0x416 = 4287963485U,
			// Token: 0x0400054E RID: 1358
			MissingPropertyValue = 4113710984U,
			// Token: 0x0400054F RID: 1359
			FolderNotPublishedException = 3202920824U,
			// Token: 0x04000550 RID: 1360
			ServerLocatorClientEndpointNotFoundException = 1807895935U,
			// Token: 0x04000551 RID: 1361
			ExTooComplexGroupSortParameter = 988424741U,
			// Token: 0x04000552 RID: 1362
			MapiCannotLookupEntryId = 220684915U,
			// Token: 0x04000553 RID: 1363
			NotificationEmailBodyExportPSTCreated = 543772960U,
			// Token: 0x04000554 RID: 1364
			TeamMailboxMessageReactivatedBodyIntroText = 1291339327U,
			// Token: 0x04000555 RID: 1365
			NotSupportedWithMailboxVersionException = 1027067688U,
			// Token: 0x04000556 RID: 1366
			ClientCulture_0x3409 = 1908093266U,
			// Token: 0x04000557 RID: 1367
			ClientCulture_0x41B = 4287963593U,
			// Token: 0x04000558 RID: 1368
			CannotAddAttachmentToReadOnlyCollection = 1892124050U,
			// Token: 0x04000559 RID: 1369
			UserPhotoPreviewNotFound = 1940443510U,
			// Token: 0x0400055A RID: 1370
			PublicFolderMailboxesCannotBeMovedDuringMigration = 3503580213U,
			// Token: 0x0400055B RID: 1371
			MigrationBatchDirectionOnboarding = 3739954134U,
			// Token: 0x0400055C RID: 1372
			AsyncOperationTypeMigration = 1702371863U,
			// Token: 0x0400055D RID: 1373
			ClientCulture_0x40E = 2721879659U,
			// Token: 0x0400055E RID: 1374
			OriginatingServer = 1959086372U,
			// Token: 0x0400055F RID: 1375
			EstimateStatePartiallySucceeded = 3553550262U,
			// Token: 0x04000560 RID: 1376
			CannotImportDeletion = 1063322282U,
			// Token: 0x04000561 RID: 1377
			MigrationUserStatusSynced = 668312535U,
			// Token: 0x04000562 RID: 1378
			CannotImportFolderChange = 1644403520U,
			// Token: 0x04000563 RID: 1379
			MigrationUserStatusValidating = 2717353738U,
			// Token: 0x04000564 RID: 1380
			ExConstraintNotSupportedForThisPropertyType = 2473956989U,
			// Token: 0x04000565 RID: 1381
			NotificationEmailSubjectCertExpiring = 614444523U,
			// Token: 0x04000566 RID: 1382
			MigrationUserStatusSummaryCompleted = 1918350106U,
			// Token: 0x04000567 RID: 1383
			SpellCheckerArabic = 116529921U,
			// Token: 0x04000568 RID: 1384
			InternalLicensingDisabledForEnterprise = 2162216975U,
			// Token: 0x04000569 RID: 1385
			ClientCulture_0x240A = 2264323529U,
			// Token: 0x0400056A RID: 1386
			RPCOperationAbortedBecauseOfAnotherRPCThread = 1621771988U,
			// Token: 0x0400056B RID: 1387
			ExInvalidMdbGuid = 2356708790U,
			// Token: 0x0400056C RID: 1388
			SpellCheckerEnglishUnitedKingdom = 3718014775U,
			// Token: 0x0400056D RID: 1389
			ExFilterHierarchyIsTooDeep = 1886702248U,
			// Token: 0x0400056E RID: 1390
			MapiCannotSetMessageLockState = 176188277U,
			// Token: 0x0400056F RID: 1391
			CannotProtectMessageForNonSmtpSender = 2635947676U,
			// Token: 0x04000570 RID: 1392
			ExSearchFolderIsAlreadyVisibleToOutlook = 3669082289U,
			// Token: 0x04000571 RID: 1393
			ExEntryIdFirst4Bytes = 1534555903U,
			// Token: 0x04000572 RID: 1394
			CustomMessageLengthExceeded = 4224870879U,
			// Token: 0x04000573 RID: 1395
			ExWrappedStreamFailure = 469526714U,
			// Token: 0x04000574 RID: 1396
			ErrorExTimeZoneValueWrongGmtFormat = 3215248187U,
			// Token: 0x04000575 RID: 1397
			InternalParserError = 2069696862U,
			// Token: 0x04000576 RID: 1398
			ExInvalidCount = 2542755219U,
			// Token: 0x04000577 RID: 1399
			ADUserNotFound = 3554710343U,
			// Token: 0x04000578 RID: 1400
			InboxRuleFlagStatusNotFlagged = 2441819035U,
			// Token: 0x04000579 RID: 1401
			ConversionMustLoadAllPropeties = 1728333927U,
			// Token: 0x0400057A RID: 1402
			ThreeHours = 4222627801U,
			// Token: 0x0400057B RID: 1403
			MapiCannotGetIDFromNames = 1816423621U,
			// Token: 0x0400057C RID: 1404
			ErrorSigntureTooLarge = 3922439094U,
			// Token: 0x0400057D RID: 1405
			MigrationBatchFlagReportInitial = 2247108930U,
			// Token: 0x0400057E RID: 1406
			ErrorTimeProposalEndTimeBeforeStartTime = 3391255931U,
			// Token: 0x0400057F RID: 1407
			CannotSetMessageFlagStatus = 3041727278U,
			// Token: 0x04000580 RID: 1408
			MigrationFlagsReport = 2084296733U,
			// Token: 0x04000581 RID: 1409
			MigrationStepProvisioning = 2188504663U,
			// Token: 0x04000582 RID: 1410
			FirstFourDayWeek = 3392951782U,
			// Token: 0x04000583 RID: 1411
			MapiCannotModifyRecipients = 313625050U,
			// Token: 0x04000584 RID: 1412
			ConversionCorruptSummaryTnef = 3755684524U,
			// Token: 0x04000585 RID: 1413
			ClientCulture_0x2409 = 1908093169U,
			// Token: 0x04000586 RID: 1414
			ExAlreadyConnected = 2511211668U,
			// Token: 0x04000587 RID: 1415
			ExReportMessageCorruptedDueToWrongItemAttachmentType = 2066272930U,
			// Token: 0x04000588 RID: 1416
			ClientCulture_0x500A = 2828973564U,
			// Token: 0x04000589 RID: 1417
			MigrationTypeNone = 4033769924U,
			// Token: 0x0400058A RID: 1418
			CannotImportReadStateChange = 1112725991U,
			// Token: 0x0400058B RID: 1419
			MapiCannotGetAttachmentTable = 2653492529U,
			// Token: 0x0400058C RID: 1420
			MapiCannotOpenAttachment = 4054158457U,
			// Token: 0x0400058D RID: 1421
			ExSuffixTextFilterNotSupported = 4130050910U,
			// Token: 0x0400058E RID: 1422
			ExSeparatorNotFoundOnCompoundValue = 2641324698U,
			// Token: 0x0400058F RID: 1423
			MigrationBatchStatusCorrupted = 4232824632U,
			// Token: 0x04000590 RID: 1424
			MigrationBatchStatusSyncing = 3135377227U,
			// Token: 0x04000591 RID: 1425
			ClientCulture_0x415 = 4287963488U,
			// Token: 0x04000592 RID: 1426
			ClientCulture_0x2C01 = 3647547362U,
			// Token: 0x04000593 RID: 1427
			CannotAccessRemoteMailbox = 984518113U,
			// Token: 0x04000594 RID: 1428
			MapiCannotFindRow = 456720827U,
			// Token: 0x04000595 RID: 1429
			ThirtyMinutes = 101242371U,
			// Token: 0x04000596 RID: 1430
			MapiCannotSeekRow = 1022111068U,
			// Token: 0x04000597 RID: 1431
			MigrationUserStatusFailed = 2976127978U,
			// Token: 0x04000598 RID: 1432
			ExceptionObjectHasBeenDeleted = 3086681225U,
			// Token: 0x04000599 RID: 1433
			MigrationBatchFlagDisallowExistingUsers = 2706959952U,
			// Token: 0x0400059A RID: 1434
			ClientCulture_0x464 = 3884678960U,
			// Token: 0x0400059B RID: 1435
			UnsupportedPropertyRestriction = 2868399142U,
			// Token: 0x0400059C RID: 1436
			ServerLocatorClientWCFCallTimeout = 265623663U,
			// Token: 0x0400059D RID: 1437
			InvalidServiceLocationResponse = 3055339528U,
			// Token: 0x0400059E RID: 1438
			MapiCannotDeleteProperties = 722987850U,
			// Token: 0x0400059F RID: 1439
			NeedFolderIdForPublicFolder = 4146176105U,
			// Token: 0x040005A0 RID: 1440
			ClientCulture_0x100C = 1666174282U,
			// Token: 0x040005A1 RID: 1441
			ManagedByRemoteExchangeOrganization = 813145114U,
			// Token: 0x040005A2 RID: 1442
			AutoDRequestFailed = 1511731857U,
			// Token: 0x040005A3 RID: 1443
			DumpsterFolderNotFound = 3680872945U,
			// Token: 0x040005A4 RID: 1444
			ExFolderNotFoundInClientState = 847028569U,
			// Token: 0x040005A5 RID: 1445
			ImportResultContainedFailure = 2399298613U,
			// Token: 0x040005A6 RID: 1446
			ClientCulture_0x813 = 4287963350U,
			// Token: 0x040005A7 RID: 1447
			ExCannotCreateMeetingResponse = 1419302978U,
			// Token: 0x040005A8 RID: 1448
			EightHours = 2061760288U,
			// Token: 0x040005A9 RID: 1449
			OperationResultFailed = 277994955U,
			// Token: 0x040005AA RID: 1450
			ErrorWorkingHoursEndTimeSmaller = 860625858U,
			// Token: 0x040005AB RID: 1451
			RoutingTypeRequired = 1977152861U,
			// Token: 0x040005AC RID: 1452
			FolderRuleCannotSaveItem = 244097521U,
			// Token: 0x040005AD RID: 1453
			RequestStateRemoving = 3695851705U,
			// Token: 0x040005AE RID: 1454
			MigrationStateFailed = 1058417226U,
			// Token: 0x040005AF RID: 1455
			MigrationUserStatusCompletionFailed = 2568510612U,
			// Token: 0x040005B0 RID: 1456
			MailboxSearchEwsEmptyResponse = 1709649557U,
			// Token: 0x040005B1 RID: 1457
			ClientCulture_0x140A = 2264323692U,
			// Token: 0x040005B2 RID: 1458
			MigrationUserStatusRemoving = 3414312136U,
			// Token: 0x040005B3 RID: 1459
			MigrationFolderCorruptedItems = 1240718058U,
			// Token: 0x040005B4 RID: 1460
			ClientCulture_0x418 = 4287963475U,
			// Token: 0x040005B5 RID: 1461
			SpellCheckerPortuguesePortugal = 1953718728U,
			// Token: 0x040005B6 RID: 1462
			TeamMailboxMessageReactivatingText = 3480868172U,
			// Token: 0x040005B7 RID: 1463
			SearchLogFileCreateException = 1241888079U,
			// Token: 0x040005B8 RID: 1464
			ExCannotGetDeletedItem = 2010446754U,
			// Token: 0x040005B9 RID: 1465
			MailboxSearchNameTooLong = 496581163U,
			// Token: 0x040005BA RID: 1466
			ClientCulture_0x41F = 4287963597U,
			// Token: 0x040005BB RID: 1467
			ClientCulture_0x4409 = 1908093103U,
			// Token: 0x040005BC RID: 1468
			ExSubmissionQuotaExceeded = 3877064532U,
			// Token: 0x040005BD RID: 1469
			ExCorruptMessageCorrelationBlob = 1682139678U,
			// Token: 0x040005BE RID: 1470
			MigrationFolderDrumTesting = 3199235754U,
			// Token: 0x040005BF RID: 1471
			ExCorruptFolderWebViewInfo = 4044636857U,
			// Token: 0x040005C0 RID: 1472
			MigrationBatchFlagDisableOnCopy = 3987708908U,
			// Token: 0x040005C1 RID: 1473
			ICSSynchronizationFailed = 1372163092U,
			// Token: 0x040005C2 RID: 1474
			OneHours = 4036040747U,
			// Token: 0x040005C3 RID: 1475
			InvalidBodyFormat = 1163271202U,
			// Token: 0x040005C4 RID: 1476
			PeopleQuickContactsAttributionDisplayName = 3938391037U,
			// Token: 0x040005C5 RID: 1477
			TwoHours = 1437739311U,
			// Token: 0x040005C6 RID: 1478
			ExPropertyDefinitionInMoreThanOnePropertyProfile = 1085572452U,
			// Token: 0x040005C7 RID: 1479
			TeamMailboxSyncStatusMembershipAndMaintenanceSyncFailure = 2260241133U,
			// Token: 0x040005C8 RID: 1480
			ClientCulture_0xC0C = 2721876058U,
			// Token: 0x040005C9 RID: 1481
			ExUnableToCopyAttachments = 3360384478U,
			// Token: 0x040005CA RID: 1482
			ExCannotUpdateResponses = 4014822501U,
			// Token: 0x040005CB RID: 1483
			ConversationItemHasNoBody = 3909125539U,
			// Token: 0x040005CC RID: 1484
			DelegateCollectionInvalidAfterSave = 390622925U,
			// Token: 0x040005CD RID: 1485
			TeamMailboxSyncStatusDocumentAndMaintenanceSyncFailure = 2946586312U,
			// Token: 0x040005CE RID: 1486
			InvalidAttachmentType = 3712347292U,
			// Token: 0x040005CF RID: 1487
			ExCannotMarkTaskCompletedWhenSuppressCreateOneOff = 3049568671U,
			// Token: 0x040005D0 RID: 1488
			CannotGetPropertyList = 748440690U,
			// Token: 0x040005D1 RID: 1489
			ErrorInvalidConfigurationXml = 1090294952U,
			// Token: 0x040005D2 RID: 1490
			InvalidBase64String = 3064401295U,
			// Token: 0x040005D3 RID: 1491
			RequestStateFailed = 1573110229U,
			// Token: 0x040005D4 RID: 1492
			InboxRuleImportanceNormal = 403751771U,
			// Token: 0x040005D5 RID: 1493
			MigrationLocalhostNotFound = 2718297568U,
			// Token: 0x040005D6 RID: 1494
			ClientCulture_0x1401 = 1551862972U,
			// Token: 0x040005D7 RID: 1495
			TeamMailboxSyncStatusSucceeded = 2594591409U,
			// Token: 0x040005D8 RID: 1496
			MigrationErrorAttachmentCorrupted = 3678601929U,
			// Token: 0x040005D9 RID: 1497
			OleConversionResultFailed = 974121200U,
			// Token: 0x040005DA RID: 1498
			MigrationUserStatusSummaryFailed = 559182252U,
			// Token: 0x040005DB RID: 1499
			RequestStateCanceled = 2151518657U,
			// Token: 0x040005DC RID: 1500
			ModifyRuleInStore = 1326926126U,
			// Token: 0x040005DD RID: 1501
			ExItemDeletedInRace = 1178929403U,
			// Token: 0x040005DE RID: 1502
			ClientCulture_0x340A = 2264323626U,
			// Token: 0x040005DF RID: 1503
			WeatherUnitFahrenheit = 2482422690U,
			// Token: 0x040005E0 RID: 1504
			MessageNotRightsProtected = 4014230567U,
			// Token: 0x040005E1 RID: 1505
			ConversionMaliciousContent = 704719423U,
			// Token: 0x040005E2 RID: 1506
			NoTemplateMessage = 3550236610U,
			// Token: 0x040005E3 RID: 1507
			FolderRuleStageLoading = 3838981202U,
			// Token: 0x040005E4 RID: 1508
			LimitedDetails = 2310868878U,
			// Token: 0x040005E5 RID: 1509
			AppendOOFHistoryEntry = 3851145766U,
			// Token: 0x040005E6 RID: 1510
			ExStoreSessionDisconnected = 1180958385U,
			// Token: 0x040005E7 RID: 1511
			NotificationEmailSubjectCertExpired = 1719015848U,
			// Token: 0x040005E8 RID: 1512
			MigrationUserAdminTypeDCAdmin = 1951112992U,
			// Token: 0x040005E9 RID: 1513
			SpellCheckerSpanish = 1511957081U,
			// Token: 0x040005EA RID: 1514
			UserPhotoNotFound = 693971404U,
			// Token: 0x040005EB RID: 1515
			ClientCulture_0x1C01 = 3647547525U,
			// Token: 0x040005EC RID: 1516
			MigrationBatchFlagAutoStop = 959876171U,
			// Token: 0x040005ED RID: 1517
			RemoteArchiveOffline = 1561549651U,
			// Token: 0x040005EE RID: 1518
			CannotOpenLocalFreeBusy = 89761473U,
			// Token: 0x040005EF RID: 1519
			CannotFindExchangePrincipal = 3320490165U,
			// Token: 0x040005F0 RID: 1520
			MigrationStageValidation = 2938839179U,
			// Token: 0x040005F1 RID: 1521
			CalNotifTypeReminder = 1865294994U,
			// Token: 0x040005F2 RID: 1522
			ExFailedToGetUnsearchableItems = 4057666506U,
			// Token: 0x040005F3 RID: 1523
			Monday = 3364213626U,
			// Token: 0x040005F4 RID: 1524
			AsyncOperationTypeUnknown = 135933047U,
			// Token: 0x040005F5 RID: 1525
			MigrationFolderSyncMigration = 3737835697U,
			// Token: 0x040005F6 RID: 1526
			InboxRuleFlagStatusComplete = 950284413U,
			// Token: 0x040005F7 RID: 1527
			NotificationEmailSubjectMoveMailbox = 838610512U,
			// Token: 0x040005F8 RID: 1528
			MessageRpmsgAttachmentIncorrectType = 3803104512U,
			// Token: 0x040005F9 RID: 1529
			FullDetails = 2897062825U,
			// Token: 0x040005FA RID: 1530
			JunkEmailObjectDisposedException = 4157021563U,
			// Token: 0x040005FB RID: 1531
			FailedToReadLocalServer = 4199032562U,
			// Token: 0x040005FC RID: 1532
			MapiCannotGetCurrentRow = 1207296209U,
			// Token: 0x040005FD RID: 1533
			MigrationInvalidPassword = 1074414016U,
			// Token: 0x040005FE RID: 1534
			ExCannotDeletePropertiesOnOccurrences = 2253780921U,
			// Token: 0x040005FF RID: 1535
			EstimateStateFailed = 2322466952U,
			// Token: 0x04000600 RID: 1536
			RequestStateCompleting = 1348198184U,
			// Token: 0x04000601 RID: 1537
			ClientCulture_0x41E = 4287963600U,
			// Token: 0x04000602 RID: 1538
			InvalidSharingRecipientsException = 3913958124U,
			// Token: 0x04000603 RID: 1539
			MapiCannotOpenFolder = 3340405076U,
			// Token: 0x04000604 RID: 1540
			ClientCulture_0x180C = 536874274U,
			// Token: 0x04000605 RID: 1541
			ADOperationAbortedBecauseOfAnotherADThread = 1558200374U,
			// Token: 0x04000606 RID: 1542
			UpdateOOFHistoryOperation = 1606180860U,
			// Token: 0x04000607 RID: 1543
			ExAttachmentAlreadyOpen = 3643161838U,
			// Token: 0x04000608 RID: 1544
			NotificationEmailBodyImportPSTCompleted = 1606937438U,
			// Token: 0x04000609 RID: 1545
			ExInvalidAggregate = 2205094141U,
			// Token: 0x0400060A RID: 1546
			NotificationEmailBodyImportPSTFailed = 2046213432U,
			// Token: 0x0400060B RID: 1547
			ExCantAccessOccurrenceFromSingle = 3350951418U,
			// Token: 0x0400060C RID: 1548
			NotificationEmailBodyExportPSTCompleted = 3182347319U,
			// Token: 0x0400060D RID: 1549
			ClientCulture_0x801 = 2721879411U,
			// Token: 0x0400060E RID: 1550
			ClientCulture_0x401 = 2721879543U,
			// Token: 0x0400060F RID: 1551
			CalNotifTypeNewUpdate = 1685125049U,
			// Token: 0x04000610 RID: 1552
			NoExternalEwsAvailableException = 148503943U,
			// Token: 0x04000611 RID: 1553
			CannotSharePublicFolder = 3737728227U,
			// Token: 0x04000612 RID: 1554
			MigrationTypeExchangeRemoteMove = 705409536U,
			// Token: 0x04000613 RID: 1555
			MigrationUserStatusCompletedWithWarning = 3160872492U,
			// Token: 0x04000614 RID: 1556
			FailedToParseUseLicense = 898461441U,
			// Token: 0x04000615 RID: 1557
			MigrationStateDisabled = 2930877107U,
			// Token: 0x04000616 RID: 1558
			MigrationBatchSupportedActionComplete = 3957837829U,
			// Token: 0x04000617 RID: 1559
			MapiCannotOpenEmbeddedMessage = 3969194465U,
			// Token: 0x04000618 RID: 1560
			InboxRuleImportanceLow = 2205239196U,
			// Token: 0x04000619 RID: 1561
			NoMapiPDLs = 792524531U,
			// Token: 0x0400061A RID: 1562
			RmExceptionGenericMessage = 1929574072U,
			// Token: 0x0400061B RID: 1563
			NotRead = 1065277019U,
			// Token: 0x0400061C RID: 1564
			ClientCulture_0x80C = 2721879521U,
			// Token: 0x0400061D RID: 1565
			idUnableToAddDefaultCalendarToDefaultCalendarGroup = 913845744U,
			// Token: 0x0400061E RID: 1566
			ClientCulture_0x3801 = 987212902U,
			// Token: 0x0400061F RID: 1567
			MapiCannotGetAllPerUserLongTermIds = 1599639819U,
			// Token: 0x04000620 RID: 1568
			TeamMailboxMessageSiteMailboxEmailAddress = 101151487U,
			// Token: 0x04000621 RID: 1569
			CalNotifTypeDeletedUpdate = 2267978872U,
			// Token: 0x04000622 RID: 1570
			CannotCreateSearchFoldersInPublicStore = 676596483U,
			// Token: 0x04000623 RID: 1571
			ExDictionaryDataCorruptedNoField = 3860059626U,
			// Token: 0x04000624 RID: 1572
			ExceptionFolderIsRootFolder = 3414452687U,
			// Token: 0x04000625 RID: 1573
			DateRangeOneDay = 2303439835U,
			// Token: 0x04000626 RID: 1574
			ClientCulture_0x412 = 4287963481U,
			// Token: 0x04000627 RID: 1575
			AppointmentTombstoneCorrupt = 2937224961U,
			// Token: 0x04000628 RID: 1576
			MigrationBatchAutoComplete = 627389694U,
			// Token: 0x04000629 RID: 1577
			MigrationObjectsCountStringNone = 3368210174U,
			// Token: 0x0400062A RID: 1578
			ClientCulture_0x810 = 4287963351U,
			// Token: 0x0400062B RID: 1579
			MapiCannotCopyItem = 1064525218U,
			// Token: 0x0400062C RID: 1580
			ErrorNoStoreObjectIdAndFolderPath = 1405495556U,
			// Token: 0x0400062D RID: 1581
			MigrationFolderSyncMigrationReports = 805861954U,
			// Token: 0x0400062E RID: 1582
			UnsupportedFormsCondition = 2576334771U,
			// Token: 0x0400062F RID: 1583
			ExStartTimeNotSet = 3719801171U,
			// Token: 0x04000630 RID: 1584
			ClientCulture_0x804 = 2721879406U,
			// Token: 0x04000631 RID: 1585
			ExConversationActionItemNotFound = 718334902U,
			// Token: 0x04000632 RID: 1586
			MigrationUserStatusProvisioning = 3108568302U,
			// Token: 0x04000633 RID: 1587
			InboxRuleMessageTypeNonDeliveryReport = 951826856U,
			// Token: 0x04000634 RID: 1588
			ExFailedToUnregisterExchangeTopologyNotification = 1983194762U,
			// Token: 0x04000635 RID: 1589
			TeamMailboxMessageLearnMore = 1453422183U,
			// Token: 0x04000636 RID: 1590
			DateRangeThreeDays = 1701953962U,
			// Token: 0x04000637 RID: 1591
			MapiCannotTransportSendMessage = 2423165282U,
			// Token: 0x04000638 RID: 1592
			ExSortNotSupportedInDeepTraversalQuery = 1627212429U,
			// Token: 0x04000639 RID: 1593
			JunkEmailAmbiguousUsernameException = 1452910403U,
			// Token: 0x0400063A RID: 1594
			MigrationBatchStatusSyncedWithErrors = 3249771727U,
			// Token: 0x0400063B RID: 1595
			FailedToFindAvailableHubs = 1714411372U,
			// Token: 0x0400063C RID: 1596
			InternetCalendarName = 3876212982U,
			// Token: 0x0400063D RID: 1597
			ExItemNotFound = 11761379U,
			// Token: 0x0400063E RID: 1598
			ExDelegateNotSupportedRespondToMeetingRequest = 1042040859U,
			// Token: 0x0400063F RID: 1599
			DisposeNonIPMFolder = 4247577850U,
			// Token: 0x04000640 RID: 1600
			InboxRuleSensitivityPrivate = 2585840702U,
			// Token: 0x04000641 RID: 1601
			MigrationFeatureNone = 1728869634U,
			// Token: 0x04000642 RID: 1602
			ClientCulture_0x405 = 2721879547U,
			// Token: 0x04000643 RID: 1603
			ExStringContainsSurroundingWhiteSpace = 46060694U,
			// Token: 0x04000644 RID: 1604
			ClientCulture_0x4C0A = 1489962160U,
			// Token: 0x04000645 RID: 1605
			ClientCulture_0x1809 = 630982608U,
			// Token: 0x04000646 RID: 1606
			FailedToResealKey = 2138889051U,
			// Token: 0x04000647 RID: 1607
			InvalidParticipantForRules = 2342985388U,
			// Token: 0x04000648 RID: 1608
			SpellCheckerDanish = 3831935814U,
			// Token: 0x04000649 RID: 1609
			MapiCannotGetProperties = 1280140891U,
			// Token: 0x0400064A RID: 1610
			MapiCopyMessagesFailed = 1835617035U,
			// Token: 0x0400064B RID: 1611
			FailedToParseValue = 2765862542U,
			// Token: 0x0400064C RID: 1612
			RuleWriterObjectNotFound = 1208307535U,
			// Token: 0x0400064D RID: 1613
			ExInvalidWatermarkString = 975909957U,
			// Token: 0x0400064E RID: 1614
			ProvisioningRequestCsvContainsBothPasswordAndFederatedIdentity = 3000070570U,
			// Token: 0x0400064F RID: 1615
			OperationResultSucceeded = 3348992335U,
			// Token: 0x04000650 RID: 1616
			ServerLocatorServicePermanentFault = 141464842U,
			// Token: 0x04000651 RID: 1617
			NotMailboxSession = 1039613051U,
			// Token: 0x04000652 RID: 1618
			MigrationStateActive = 1391852443U,
			// Token: 0x04000653 RID: 1619
			Null = 1743625299U,
			// Token: 0x04000654 RID: 1620
			FolderRuleStageOnCreatedMessage = 1066460788U,
			// Token: 0x04000655 RID: 1621
			CannotSetMessageFlags = 3238359831U,
			// Token: 0x04000656 RID: 1622
			ExInvalidAsyncResult = 1511089307U,
			// Token: 0x04000657 RID: 1623
			ClientCulture_0x2801 = 987212805U,
			// Token: 0x04000658 RID: 1624
			ExFolderPropertyBagCannotSaveChanges = 3815733289U,
			// Token: 0x04000659 RID: 1625
			MigrationBatchStatusStopped = 1665468465U,
			// Token: 0x0400065A RID: 1626
			KqlParserTimeout = 1852466064U,
			// Token: 0x0400065B RID: 1627
			TenMinutes = 3623312858U,
			// Token: 0x0400065C RID: 1628
			ExMustSetSearchCriteriaToMakeVisibleToOutlook = 2730199010U,
			// Token: 0x0400065D RID: 1629
			Wednesday = 3452652986U,
			// Token: 0x0400065E RID: 1630
			ClientCulture_0xC07 = 2721875974U,
			// Token: 0x0400065F RID: 1631
			LegacyMailboxSearchDescription = 3155538965U,
			// Token: 0x04000660 RID: 1632
			MapiCannotFreeBookmark = 1494015926U,
			// Token: 0x04000661 RID: 1633
			CannotChangePermissionsOnFolder = 4041018166U,
			// Token: 0x04000662 RID: 1634
			MapiCannotSetProps = 14329882U,
			// Token: 0x04000663 RID: 1635
			SearchStatePartiallySucceeded = 1082563208U,
			// Token: 0x04000664 RID: 1636
			InboxRuleMessageTypeAutomaticReply = 1720321054U,
			// Token: 0x04000665 RID: 1637
			RuleHistoryError = 270030022U,
			// Token: 0x04000666 RID: 1638
			ClientCulture_0x280A = 1699673525U,
			// Token: 0x04000667 RID: 1639
			ClientCulture_0x3001 = 2116512910U,
			// Token: 0x04000668 RID: 1640
			SharePoint = 422603853U,
			// Token: 0x04000669 RID: 1641
			NoDelegateAction = 3562004828U,
			// Token: 0x0400066A RID: 1642
			MigrationFlagsStop = 3008535783U,
			// Token: 0x0400066B RID: 1643
			ExNoOptimizedCodePath = 2280589817U,
			// Token: 0x0400066C RID: 1644
			MigrationBatchFlagUseAdvancedValidation = 2285862258U,
			// Token: 0x0400066D RID: 1645
			MapiCannotGetParentEntryId = 721699019U,
			// Token: 0x0400066E RID: 1646
			ExOnlyMessagesHaveParent = 3915122871U,
			// Token: 0x0400066F RID: 1647
			ClientCulture_0x411 = 4287963484U,
			// Token: 0x04000670 RID: 1648
			ExFolderDoesNotMatchFolderId = 1405345367U,
			// Token: 0x04000671 RID: 1649
			MigrationTypeXO1 = 2992671490U,
			// Token: 0x04000672 RID: 1650
			NotOperator = 273956641U,
			// Token: 0x04000673 RID: 1651
			ClientCulture_0x480A = 1699673459U,
			// Token: 0x04000674 RID: 1652
			Saturday = 3478111469U,
			// Token: 0x04000675 RID: 1653
			ExFailedToRegisterExchangeTopologyNotification = 4173454943U,
			// Token: 0x04000676 RID: 1654
			MigrationBatchSupportedActionSet = 1732871098U,
			// Token: 0x04000677 RID: 1655
			ConversionCannotOpenJournalMessage = 552541799U,
			// Token: 0x04000678 RID: 1656
			JunkEmailTrustedListXsoEmptyException = 2519002207U,
			// Token: 0x04000679 RID: 1657
			AmFailedToFindSuitableServer = 2181826069U,
			// Token: 0x0400067A RID: 1658
			OperationalError = 1299125360U,
			// Token: 0x0400067B RID: 1659
			ExTooManySortColumns = 2689882193U,
			// Token: 0x0400067C RID: 1660
			LoadRulesFromStore = 3810093794U,
			// Token: 0x0400067D RID: 1661
			ClientCulture_0x40D = 2721879658U,
			// Token: 0x0400067E RID: 1662
			ExCantCopyBadAlienDLMember = 1282190256U,
			// Token: 0x0400067F RID: 1663
			TeamMailboxMessageSendMailToTheSiteMailbox = 3434190160U,
			// Token: 0x04000680 RID: 1664
			InboxRuleSensitivityPersonal = 3610984697U,
			// Token: 0x04000681 RID: 1665
			ExInvalidItemCountAdvisorCondition = 2078392877U,
			// Token: 0x04000682 RID: 1666
			ErrorLoadingRules = 3955579167U,
			// Token: 0x04000683 RID: 1667
			ClientCulture_0x414 = 4287963487U,
			// Token: 0x04000684 RID: 1668
			ExEndTimeNotSet = 1246168046U,
			// Token: 0x04000685 RID: 1669
			InboxRuleMessageTypeAutomaticForward = 1055197681U,
			// Token: 0x04000686 RID: 1670
			MapiCannotCopyMapiProps = 392605258U,
			// Token: 0x04000687 RID: 1671
			OneWeeks = 2708396435U,
			// Token: 0x04000688 RID: 1672
			TeamMailboxMessageWhatYouCanDoNext = 494393471U,
			// Token: 0x04000689 RID: 1673
			MapiCannotGetReceiveFolderInfo = 1985781485U,
			// Token: 0x0400068A RID: 1674
			ExInvalidStoreObjectId = 2870966119U,
			// Token: 0x0400068B RID: 1675
			RequestStateQueued = 1332658223U,
			// Token: 0x0400068C RID: 1676
			RecurrenceBlobCorrupted = 1762000491U,
			// Token: 0x0400068D RID: 1677
			CannotFindAttachment = 3526072757U,
			// Token: 0x0400068E RID: 1678
			ExInvalidRecipientBlob = 2356618506U,
			// Token: 0x0400068F RID: 1679
			ExIncompleteBlob = 1272751292U,
			// Token: 0x04000690 RID: 1680
			ExPatternNotSet = 379946208U,
			// Token: 0x04000691 RID: 1681
			ExInvalidDayOfMonth = 1944490681U,
			// Token: 0x04000692 RID: 1682
			ExInvalidGlobalObjectId = 3835493555U,
			// Token: 0x04000693 RID: 1683
			MapiCannotGetHierarchyTable = 2185974115U,
			// Token: 0x04000694 RID: 1684
			ClientCulture_0xC0A = 2721876056U,
			// Token: 0x04000695 RID: 1685
			ExInvalidFullyQualifiedServerName = 2799724840U,
			// Token: 0x04000696 RID: 1686
			EstimateStateInProgress = 751268339U,
			// Token: 0x04000697 RID: 1687
			MapiCannotSetReadFlags = 919965275U,
			// Token: 0x04000698 RID: 1688
			PublicFolderQueryStatusSyncFolderHierarchyRpcFailed = 2710414285U,
			// Token: 0x04000699 RID: 1689
			ExInvalidSearchFolderScope = 3367423950U,
			// Token: 0x0400069A RID: 1690
			ActivitySessionIsNull = 298783488U,
			// Token: 0x0400069B RID: 1691
			SpellCheckerItalian = 3325987673U,
			// Token: 0x0400069C RID: 1692
			FolderRuleStageOnPublicFolderAfter = 3248071036U,
			// Token: 0x0400069D RID: 1693
			NotAllowedAnonymousSharingByPolicy = 1964131369U,
			// Token: 0x0400069E RID: 1694
			MapiCannotGetCollapseState = 1214453788U,
			// Token: 0x0400069F RID: 1695
			ZeroMinutes = 3651846103U,
			// Token: 0x040006A0 RID: 1696
			RecipientNotSupportedByAnyProviderException = 3749282747U,
			// Token: 0x040006A1 RID: 1697
			MigrationReportFinalizationSuccess = 1970390563U,
			// Token: 0x040006A2 RID: 1698
			CalNotifTypeChangedUpdate = 3878608135U,
			// Token: 0x040006A3 RID: 1699
			ExSizeFilterPropertyNotSupported = 3697440372U,
			// Token: 0x040006A4 RID: 1700
			ExConversationActionInvalidFolderType = 3733109861U,
			// Token: 0x040006A5 RID: 1701
			ClientCulture_0x2009 = 2472743173U,
			// Token: 0x040006A6 RID: 1702
			UserDiscoveryMailboxNotFound = 2230033988U,
			// Token: 0x040006A7 RID: 1703
			ExCorruptedRecurringCalItem = 314838903U,
			// Token: 0x040006A8 RID: 1704
			ExStartDateLaterThanEndDate = 780978771U,
			// Token: 0x040006A9 RID: 1705
			ExInvalidOrganizer = 2635186097U,
			// Token: 0x040006AA RID: 1706
			MaxExclusionReached = 3162466950U,
			// Token: 0x040006AB RID: 1707
			JunkEmailBlockedListXsoEmptyException = 291587104U,
			// Token: 0x040006AC RID: 1708
			SearchStateSucceeded = 4224027968U,
			// Token: 0x040006AD RID: 1709
			ExInvalidJournalReportFormat = 4078034164U,
			// Token: 0x040006AE RID: 1710
			RequestStateCertExpiring = 1049215562U,
			// Token: 0x040006AF RID: 1711
			ConversionBodyConversionFailed = 3206936715U,
			// Token: 0x040006B0 RID: 1712
			MigrationUserAdminTypeTenantAdmin = 167159909U,
			// Token: 0x040006B1 RID: 1713
			ClientCulture_0x40B = 2721879652U,
			// Token: 0x040006B2 RID: 1714
			ConversionEmptyAddress = 871369479U,
			// Token: 0x040006B3 RID: 1715
			ClientCulture_0x380A = 1699673622U,
			// Token: 0x040006B4 RID: 1716
			PublicFoldersCannotBeAccessedDuringCompletion = 2430821738U,
			// Token: 0x040006B5 RID: 1717
			MigrationReportUnknown = 845797650U,
			// Token: 0x040006B6 RID: 1718
			FolderRuleResolvingAddressBookEntryId = 2220118463U,
			// Token: 0x040006B7 RID: 1719
			SharePointLifecyclePolicy = 2430200359U,
			// Token: 0x040006B8 RID: 1720
			MapiCannotAddNotification = 1296109892U,
			// Token: 0x040006B9 RID: 1721
			ClientCulture_0x200A = 2828973533U,
			// Token: 0x040006BA RID: 1722
			ExFailedToCreateEventManager = 1595290732U,
			// Token: 0x040006BB RID: 1723
			MaixmumNumberOfMailboxAssociationsForUserReached = 2693957296U,
			// Token: 0x040006BC RID: 1724
			ExInvalidEIT = 4067432976U,
			// Token: 0x040006BD RID: 1725
			ExFilterAndSortNotSupportedInSimpleVirtualPropertyDefinition = 3176034413U,
			// Token: 0x040006BE RID: 1726
			DateRangeOneMonth = 2864562615U,
			// Token: 0x040006BF RID: 1727
			MapiInvalidParam = 1299122915U,
			// Token: 0x040006C0 RID: 1728
			MapiCannotDeleteUserPhoto = 94201836U,
			// Token: 0x040006C1 RID: 1729
			MigrationUserAdminTypePartner = 3755817478U,
			// Token: 0x040006C2 RID: 1730
			ExOrganizerCannotCallUpdateCalendarItem = 1504384645U,
			// Token: 0x040006C3 RID: 1731
			DuplicateCondition = 1680240084U,
			// Token: 0x040006C4 RID: 1732
			JunkEmailTrustedListXsoTooManyException = 1817477041U,
			// Token: 0x040006C5 RID: 1733
			VersionNotInteger = 1076453993U,
			// Token: 0x040006C6 RID: 1734
			ClientCulture_0x41A = 4287963596U,
			// Token: 0x040006C7 RID: 1735
			MapiCannotGetNamedProperties = 3423699388U,
			// Token: 0x040006C8 RID: 1736
			ExFailedToDeleteDefaultFolder = 543137909U,
			// Token: 0x040006C9 RID: 1737
			ExDefaultFoldersNotInitialized = 1775042376U,
			// Token: 0x040006CA RID: 1738
			AsyncOperationTypeCertExpiry = 2045069482U,
			// Token: 0x040006CB RID: 1739
			ClosingTagExpectedNoneFound = 3561051015U,
			// Token: 0x040006CC RID: 1740
			FolderRuleStageEvaluation = 4098965384U,
			// Token: 0x040006CD RID: 1741
			ClientCulture_0x814 = 4287963347U,
			// Token: 0x040006CE RID: 1742
			TeamMailboxMessageMemberInvitationBodyIntroText = 4024171282U,
			// Token: 0x040006CF RID: 1743
			ClientCulture_0xC09 = 2721875968U,
			// Token: 0x040006D0 RID: 1744
			UnexpectedToken = 3683655246U,
			// Token: 0x040006D1 RID: 1745
			MapiCannotSeekRowBookmark = 3146200960U,
			// Token: 0x040006D2 RID: 1746
			PublicFolderSyncFolderRpcFailed = 2294313398U,
			// Token: 0x040006D3 RID: 1747
			MigrationFlagsNone = 1106682409U,
			// Token: 0x040006D4 RID: 1748
			JunkEmailTrustedListXsoNullException = 3045858265U,
			// Token: 0x040006D5 RID: 1749
			MapiCannotQueryRows = 2855609935U,
			// Token: 0x040006D6 RID: 1750
			MigrationTestMSASuccess = 147262116U,
			// Token: 0x040006D7 RID: 1751
			MigrationMailboxPermissionAdmin = 732586480U,
			// Token: 0x040006D8 RID: 1752
			ExInvalidItemId = 1719335392U,
			// Token: 0x040006D9 RID: 1753
			MapiCannotSetSpooler = 3009511836U,
			// Token: 0x040006DA RID: 1754
			InvalidSharingTargetRecipientException = 1932453382U,
			// Token: 0x040006DB RID: 1755
			RequestStateInProgress = 1816014188U,
			// Token: 0x040006DC RID: 1756
			UnlockOOFHistory = 4127188442U,
			// Token: 0x040006DD RID: 1757
			ExMclCannotBeResolved = 559598517U,
			// Token: 0x040006DE RID: 1758
			FailedToReadConfiguration = 3224364440U,
			// Token: 0x040006DF RID: 1759
			ADUserNoMailbox = 749132645U,
			// Token: 0x040006E0 RID: 1760
			ExReadExchangeTopologyFailed = 1136597198U,
			// Token: 0x040006E1 RID: 1761
			MigrationUserStatusCorrupted = 152248145U,
			// Token: 0x040006E2 RID: 1762
			ExMatchShouldHaveBeenCalled = 4055176570U,
			// Token: 0x040006E3 RID: 1763
			ExCannotModifyRemovedRecipient = 3282126921U,
			// Token: 0x040006E4 RID: 1764
			InvalidDateTimeFormat = 3306331751U,
			// Token: 0x040006E5 RID: 1765
			OleConversionPrepareFailed = 3270231634U,
			// Token: 0x040006E6 RID: 1766
			NotificationEmailBodyExportPSTFailed = 2098990361U,
			// Token: 0x040006E7 RID: 1767
			ExRangeNotSet = 1342934277U,
			// Token: 0x040006E8 RID: 1768
			OrganizationNotFederatedException = 2096844093U,
			// Token: 0x040006E9 RID: 1769
			ClientCulture_0x421 = 1559080129U,
			// Token: 0x040006EA RID: 1770
			ACLTooBig = 1453305798U,
			// Token: 0x040006EB RID: 1771
			TeamMailboxMessageNotConnectedToSite = 1949750262U,
			// Token: 0x040006EC RID: 1772
			TeamMailboxSyncStatusFailed = 77059561U,
			// Token: 0x040006ED RID: 1773
			ClientCulture_0x80A = 2721879523U,
			// Token: 0x040006EE RID: 1774
			FailedToBindToUseLicense = 713774522U,
			// Token: 0x040006EF RID: 1775
			MapiCannotGetParentId = 3349952875U,
			// Token: 0x040006F0 RID: 1776
			ExEndDateEarlierThanStartDate = 3964486357U,
			// Token: 0x040006F1 RID: 1777
			ExInvalidCustomSerializationData = 2183577153U,
			// Token: 0x040006F2 RID: 1778
			ConversionLimitsExceeded = 1559063653U,
			// Token: 0x040006F3 RID: 1779
			LargeMultivaluedPropertiesNotSupportedInTNEF = 1403049999U,
			// Token: 0x040006F4 RID: 1780
			MigrationSkippableStepSettingTargetAddress = 3892201190U,
			// Token: 0x040006F5 RID: 1781
			InvalidPropertyKey = 3041066937U,
			// Token: 0x040006F6 RID: 1782
			TwoDays = 1512671255U,
			// Token: 0x040006F7 RID: 1783
			ExOutlookSearchFolderDoesNotHaveMailboxSession = 1027306046U,
			// Token: 0x040006F8 RID: 1784
			ClientCulture_0x426 = 1559080130U,
			// Token: 0x040006F9 RID: 1785
			MalformedAdrEntry = 1572145744U,
			// Token: 0x040006FA RID: 1786
			FailedToFindIssuanceLicenseAndURI = 1033019572U,
			// Token: 0x040006FB RID: 1787
			InboxRuleMessageTypeEncrypted = 4010739301U,
			// Token: 0x040006FC RID: 1788
			SuffixMatchNotSupported = 927529551U,
			// Token: 0x040006FD RID: 1789
			idClientSessionInfoParseException = 1799732388U,
			// Token: 0x040006FE RID: 1790
			ClientCulture_0x1009 = 2472743336U,
			// Token: 0x040006FF RID: 1791
			OperationResultPartiallySucceeded = 2107406877U,
			// Token: 0x04000700 RID: 1792
			UceContentFilterLoadFailure = 628765410U,
			// Token: 0x04000701 RID: 1793
			MapiRulesError = 1228234464U,
			// Token: 0x04000702 RID: 1794
			MigrationBatchStatusRemoving = 2423017895U,
			// Token: 0x04000703 RID: 1795
			ExBadFolderEntryIdSize = 2503716610U,
			// Token: 0x04000704 RID: 1796
			NotSupportedSharingMessageException = 3097513127U,
			// Token: 0x04000705 RID: 1797
			IncompleteExchangePrincipal = 885639843U,
			// Token: 0x04000706 RID: 1798
			MigrationFeatureMultiBatch = 2444446273U,
			// Token: 0x04000707 RID: 1799
			MapiCannotSetTableColumns = 2889367749U,
			// Token: 0x04000708 RID: 1800
			FailedToReadActivityLog = 3398202471U,
			// Token: 0x04000709 RID: 1801
			SearchOperationFailed = 2045304318U,
			// Token: 0x0400070A RID: 1802
			ExDefaultJournalFilename = 3997956246U,
			// Token: 0x0400070B RID: 1803
			MapiCannotExpandRow = 1407904664U,
			// Token: 0x0400070C RID: 1804
			ExCannotStartDeadSessionChecking = 3375709410U,
			// Token: 0x0400070D RID: 1805
			SearchStateNotStarted = 2953075549U,
			// Token: 0x0400070E RID: 1806
			SpellCheckerSwedish = 2989293604U,
			// Token: 0x0400070F RID: 1807
			NotificationEmailSubjectExportPst = 3084838222U,
			// Token: 0x04000710 RID: 1808
			ContentRestrictionOnSearchKey = 2174906965U,
			// Token: 0x04000711 RID: 1809
			ExUnknownFilterType = 671447089U,
			// Token: 0x04000712 RID: 1810
			ClientCulture_0x400A = 2828973467U,
			// Token: 0x04000713 RID: 1811
			UnableToLoadDrmMessage = 3647529948U,
			// Token: 0x04000714 RID: 1812
			ExCannotParseValue = 4048687496U,
			// Token: 0x04000715 RID: 1813
			ClientCulture_0x4001 = 2116512747U,
			// Token: 0x04000716 RID: 1814
			TeamMailboxMessageToLearnMore = 1918332060U,
			// Token: 0x04000717 RID: 1815
			MigrationBatchStatusCompleting = 1272465216U,
			// Token: 0x04000718 RID: 1816
			MapiCannotGetRowCount = 2453169079U,
			// Token: 0x04000719 RID: 1817
			FolderRuleStageOnDeliveredMessage = 3849632232U,
			// Token: 0x0400071A RID: 1818
			MigrationBatchSupportedActionAppend = 2614560510U,
			// Token: 0x0400071B RID: 1819
			ExCannotOpenRejectedItem = 1858795727U,
			// Token: 0x0400071C RID: 1820
			MigrationBatchStatusFailed = 82143339U,
			// Token: 0x0400071D RID: 1821
			VotingDataCorrupt = 597150166U,
			// Token: 0x0400071E RID: 1822
			MigrationTestMSAFailed = 3941673604U,
			// Token: 0x0400071F RID: 1823
			TeamMailboxSyncStatusDocumentAndMembershipSyncFailure = 656484203U,
			// Token: 0x04000720 RID: 1824
			PublicFoldersCannotBeMovedDuringMigration = 460612780U,
			// Token: 0x04000721 RID: 1825
			ConversionNoReplayContent = 480886895U,
			// Token: 0x04000722 RID: 1826
			ClientCulture_0x404 = 2721879546U,
			// Token: 0x04000723 RID: 1827
			ExCantSubmitWithoutRecipients = 495633109U,
			// Token: 0x04000724 RID: 1828
			LastErrorMessage = 1614900577U,
			// Token: 0x04000725 RID: 1829
			JunkEmailFolderNotFoundException = 2849660304U,
			// Token: 0x04000726 RID: 1830
			ClientCulture_0x42A = 1559080241U,
			// Token: 0x04000727 RID: 1831
			AmMoveNotApplicableForDbException = 3133024367U,
			// Token: 0x04000728 RID: 1832
			ExCantUndeleteOccurrence = 3710501592U,
			// Token: 0x04000729 RID: 1833
			MigrationUserStatusQueued = 2418303566U,
			// Token: 0x0400072A RID: 1834
			DateRangeSixMonths = 4210093826U,
			// Token: 0x0400072B RID: 1835
			MigrationSkippableStepNone = 1365634215U,
			// Token: 0x0400072C RID: 1836
			MalformedCommentRestriction = 3746574982U,
			// Token: 0x0400072D RID: 1837
			ClientCulture_0x427 = 1559080131U,
			// Token: 0x0400072E RID: 1838
			MigrationUserStatusSummaryActive = 2702427225U,
			// Token: 0x0400072F RID: 1839
			ErrorEmptyFolderNotSupported = 2343856628U,
			// Token: 0x04000730 RID: 1840
			ClientCulture_0x540A = 2264323560U,
			// Token: 0x04000731 RID: 1841
			ClientCulture_0x2C0A = 1489962226U,
			// Token: 0x04000732 RID: 1842
			MissingOperand = 3302221775U,
			// Token: 0x04000733 RID: 1843
			DuplicateAction = 1949879021U,
			// Token: 0x04000734 RID: 1844
			SearchStateQueued = 2882103486U,
			// Token: 0x04000735 RID: 1845
			ExQueryPropertyBagRowNotSet = 3607950093U,
			// Token: 0x04000736 RID: 1846
			Sunday = 1073167130U,
			// Token: 0x04000737 RID: 1847
			WeatherUnitDefault = 3059751105U,
			// Token: 0x04000738 RID: 1848
			RequestSecurityTokenException = 3120595407U,
			// Token: 0x04000739 RID: 1849
			ExNoOccurrencesInRecurrence = 1768187831U,
			// Token: 0x0400073A RID: 1850
			ExInvalidMclXml = 276036243U,
			// Token: 0x0400073B RID: 1851
			ExInvalidIdFormat = 4168806856U,
			// Token: 0x0400073C RID: 1852
			ExAdminAuditLogsFolderAccessDenied = 2161223297U,
			// Token: 0x0400073D RID: 1853
			ClientCulture_0xC04 = 2721875973U,
			// Token: 0x0400073E RID: 1854
			ExInvalidComparisionOperatorInPropertyComparisionFilter = 4170247214U,
			// Token: 0x0400073F RID: 1855
			AsyncOperationTypeExportPST = 1937417240U
		}

		// Token: 0x020000B7 RID: 183
		private enum ParamIDs
		{
			// Token: 0x04000741 RID: 1857
			InvalidReceiveMeetingMessageCopiesException,
			// Token: 0x04000742 RID: 1858
			AmDatabaseADException,
			// Token: 0x04000743 RID: 1859
			AddressAndOriginMismatch,
			// Token: 0x04000744 RID: 1860
			RepairingIsNotApplicableForCurrentMonitorState,
			// Token: 0x04000745 RID: 1861
			MigrationInvalidTargetAddress,
			// Token: 0x04000746 RID: 1862
			JunkEmailTrustedListXsoFormatException,
			// Token: 0x04000747 RID: 1863
			ExSyncStateAlreadyExists,
			// Token: 0x04000748 RID: 1864
			FailedToAcquireFederationRac,
			// Token: 0x04000749 RID: 1865
			SaveFailureAfterPromotion,
			// Token: 0x0400074A RID: 1866
			ErrorCannotSyncHoldObjectManagedByOtherOrg,
			// Token: 0x0400074B RID: 1867
			TaskOperationFailedWithEcException,
			// Token: 0x0400074C RID: 1868
			UserPhotoFileTooLarge,
			// Token: 0x0400074D RID: 1869
			ExternalIdentityInconsistentSid,
			// Token: 0x0400074E RID: 1870
			DataMoveReplicationConstraintFlushNotSatisfied,
			// Token: 0x0400074F RID: 1871
			PublicFoldersNotEnabledForTenant,
			// Token: 0x04000750 RID: 1872
			MigrationJobConnectionSettingsIncomplete,
			// Token: 0x04000751 RID: 1873
			JunkEmailBlockedListOwnersEmailAddressException,
			// Token: 0x04000752 RID: 1874
			ExServerNotInSite,
			// Token: 0x04000753 RID: 1875
			InvalidSharingMessageException,
			// Token: 0x04000754 RID: 1876
			CannotObtainSynchronizationUploadState,
			// Token: 0x04000755 RID: 1877
			ExFolderDeletePropsFailed,
			// Token: 0x04000756 RID: 1878
			ExConfigTypeNotMatched,
			// Token: 0x04000757 RID: 1879
			UserCannotBeFoundFromContext,
			// Token: 0x04000758 RID: 1880
			JunkEmailBlockedListXsoGenericException,
			// Token: 0x04000759 RID: 1881
			MapiCannotCreateFolder,
			// Token: 0x0400075A RID: 1882
			ExCantDeleteOpenedOccurrence,
			// Token: 0x0400075B RID: 1883
			BindToWrongObjectType,
			// Token: 0x0400075C RID: 1884
			idMailboxInfoStaleException,
			// Token: 0x0400075D RID: 1885
			ExCannotSaveInvalidObject,
			// Token: 0x0400075E RID: 1886
			AmServerTransientException,
			// Token: 0x0400075F RID: 1887
			ExCalendarTypeNotSupported,
			// Token: 0x04000760 RID: 1888
			FailedToAcquireUseLicense,
			// Token: 0x04000761 RID: 1889
			AmReplayServiceDownException,
			// Token: 0x04000762 RID: 1890
			ExDefaultFolderNotFound,
			// Token: 0x04000763 RID: 1891
			JunkEmailBlockedListInternalToOrganizationException,
			// Token: 0x04000764 RID: 1892
			FailedToAcquireUseLicenses,
			// Token: 0x04000765 RID: 1893
			AmDbNotMountedNoViableServersException,
			// Token: 0x04000766 RID: 1894
			ExNumberOfRowsToFetchInvalid,
			// Token: 0x04000767 RID: 1895
			PropertyIsReadOnly,
			// Token: 0x04000768 RID: 1896
			ActiveMonitoringServerException,
			// Token: 0x04000769 RID: 1897
			AmServerDagNotFound,
			// Token: 0x0400076A RID: 1898
			MigrationObjectsCountStringMailboxes,
			// Token: 0x0400076B RID: 1899
			AddressAndRoutingTypeMismatch,
			// Token: 0x0400076C RID: 1900
			RecipientAddressInvalidForExternalLicensing,
			// Token: 0x0400076D RID: 1901
			MigrationBatchNotFoundError,
			// Token: 0x0400076E RID: 1902
			ExInvalidMonthNthDayMask,
			// Token: 0x0400076F RID: 1903
			PublicFolderPerServerThreadLimitExceeded,
			// Token: 0x04000770 RID: 1904
			DataMoveReplicationConstraintNotSatisfiedForNonReplicatedDatabase,
			// Token: 0x04000771 RID: 1905
			FailedToCreateLicenseGenerator,
			// Token: 0x04000772 RID: 1906
			OscFolderForProviderNotFound,
			// Token: 0x04000773 RID: 1907
			ImportItemThrewGrayException,
			// Token: 0x04000774 RID: 1908
			NonCanonicalACL,
			// Token: 0x04000775 RID: 1909
			UserPhotoFileTooSmall,
			// Token: 0x04000776 RID: 1910
			InvalidWorkingPeriod,
			// Token: 0x04000777 RID: 1911
			AmFailedToDeterminePAM,
			// Token: 0x04000778 RID: 1912
			ExInvalidMAPIType,
			// Token: 0x04000779 RID: 1913
			MapiCannotOpenAttachmentId,
			// Token: 0x0400077A RID: 1914
			ExUnableToOpenOrCreateDefaultFolder,
			// Token: 0x0400077B RID: 1915
			TeamMailboxMessageWelcomeSubject,
			// Token: 0x0400077C RID: 1916
			ConversionCorruptTnef,
			// Token: 0x0400077D RID: 1917
			ExNullSortOrderParameter,
			// Token: 0x0400077E RID: 1918
			UserSidNotFound,
			// Token: 0x0400077F RID: 1919
			PublicFolderSyncFolderHierarchyFailed,
			// Token: 0x04000780 RID: 1920
			MailboxNotFoundByAdObjectId,
			// Token: 0x04000781 RID: 1921
			idResourceQuarantinedDueToBlackHole,
			// Token: 0x04000782 RID: 1922
			ExInvalidParameter,
			// Token: 0x04000783 RID: 1923
			FailedToUnprotectAttachment,
			// Token: 0x04000784 RID: 1924
			SearchMandatoryParameter,
			// Token: 0x04000785 RID: 1925
			InvalidDuration,
			// Token: 0x04000786 RID: 1926
			NotificationEmailSubjectFailed,
			// Token: 0x04000787 RID: 1927
			InternalLicensingDisabledForTenant,
			// Token: 0x04000788 RID: 1928
			ExBatchBuilderADLookupFailed,
			// Token: 0x04000789 RID: 1929
			ActiveMonitoringServiceDown,
			// Token: 0x0400078A RID: 1930
			ErrorStatisticsStartIndexIsOutOfBound,
			// Token: 0x0400078B RID: 1931
			ExServerNotFound,
			// Token: 0x0400078C RID: 1932
			CanUseApiOnlyWhenTimeZoneIsNull,
			// Token: 0x0400078D RID: 1933
			ExUnsupportedMapiType,
			// Token: 0x0400078E RID: 1934
			DataMoveReplicationConstraintNotSatisfiedNoHealthyCopies,
			// Token: 0x0400078F RID: 1935
			DagNetworkManagementError,
			// Token: 0x04000790 RID: 1936
			ActiveMonitoringServerTransientException,
			// Token: 0x04000791 RID: 1937
			TeamMailboxMessageClosedSubject,
			// Token: 0x04000792 RID: 1938
			ErrorSavingSearchObject,
			// Token: 0x04000793 RID: 1939
			FolderSaveOperationResult,
			// Token: 0x04000794 RID: 1940
			FailedToLocateTPDConfig,
			// Token: 0x04000795 RID: 1941
			ExInvalidExceptionListWithDoubleEntry,
			// Token: 0x04000796 RID: 1942
			ExCannotCreateFolder,
			// Token: 0x04000797 RID: 1943
			ExInvalidDateTimeRange,
			// Token: 0x04000798 RID: 1944
			AmServerNotFoundException,
			// Token: 0x04000799 RID: 1945
			UserHasNoEventPermisson,
			// Token: 0x0400079A RID: 1946
			RpcServerRequestAlreadyPending,
			// Token: 0x0400079B RID: 1947
			ExUnsupportedABProvider,
			// Token: 0x0400079C RID: 1948
			MigrationItemStatisticsString,
			// Token: 0x0400079D RID: 1949
			ValidationFailureAfterPromotion,
			// Token: 0x0400079E RID: 1950
			ExCurrentServerNotInSite,
			// Token: 0x0400079F RID: 1951
			CannotResolvePropertyException,
			// Token: 0x040007A0 RID: 1952
			FailedToVerifyDRMPropsSignatureADError,
			// Token: 0x040007A1 RID: 1953
			RpcServerIgnorePendingDeleteTeamMailbox,
			// Token: 0x040007A2 RID: 1954
			MigrationObjectsCountStringGroups,
			// Token: 0x040007A3 RID: 1955
			ExInvalidMonthlyDayOfMonth,
			// Token: 0x040007A4 RID: 1956
			ErrorCalendarReminderNotMinutes,
			// Token: 0x040007A5 RID: 1957
			ExCorrelationFailedForOccurrence,
			// Token: 0x040007A6 RID: 1958
			ErrorTimeProposalInvalidDuration,
			// Token: 0x040007A7 RID: 1959
			MigrationFolderNotFound,
			// Token: 0x040007A8 RID: 1960
			MigrationMailboxDatabaseInfoNotAvailable,
			// Token: 0x040007A9 RID: 1961
			MultipleSystemMigrationMailboxes,
			// Token: 0x040007AA RID: 1962
			ExInvalidNamedProperty,
			// Token: 0x040007AB RID: 1963
			DagNetworkRenameDupName,
			// Token: 0x040007AC RID: 1964
			ErrorInvalidQueryLanguage,
			// Token: 0x040007AD RID: 1965
			ExValueOfWrongType,
			// Token: 0x040007AE RID: 1966
			InvalidReminderTime,
			// Token: 0x040007AF RID: 1967
			InvalidExternalDirectoryObjectIdError,
			// Token: 0x040007B0 RID: 1968
			idUnableToCreateDefaultCalendarGroupException,
			// Token: 0x040007B1 RID: 1969
			ExInvalidWeeklyDayMask,
			// Token: 0x040007B2 RID: 1970
			IncorrectEntriesInServiceLocationResponse,
			// Token: 0x040007B3 RID: 1971
			InconsistentCalendarMethod,
			// Token: 0x040007B4 RID: 1972
			CannotCreateSynchronizerEx,
			// Token: 0x040007B5 RID: 1973
			ExInvalidExceptionCount,
			// Token: 0x040007B6 RID: 1974
			RemoteAccountPolicyNotFound,
			// Token: 0x040007B7 RID: 1975
			IncorrectServerError,
			// Token: 0x040007B8 RID: 1976
			ExInvalidYearlyRecurrencePeriod,
			// Token: 0x040007B9 RID: 1977
			RpcServerDirectoryError,
			// Token: 0x040007BA RID: 1978
			FailedToReadUserConfig,
			// Token: 0x040007BB RID: 1979
			InvalidDueDate1,
			// Token: 0x040007BC RID: 1980
			ExUnsupportedCodepage,
			// Token: 0x040007BD RID: 1981
			EmailFormatNotSupported,
			// Token: 0x040007BE RID: 1982
			ExInvalidTypeGroupCombination,
			// Token: 0x040007BF RID: 1983
			MigrationBatchErrorString,
			// Token: 0x040007C0 RID: 1984
			ClosingTagExpected,
			// Token: 0x040007C1 RID: 1985
			AmServiceDownException,
			// Token: 0x040007C2 RID: 1986
			ExInvalidAttendeeType,
			// Token: 0x040007C3 RID: 1987
			AmDatabaseMasterIsInvalid,
			// Token: 0x040007C4 RID: 1988
			MigrationGroupMembersAttachmentCorrupted,
			// Token: 0x040007C5 RID: 1989
			UseMethodInstead,
			// Token: 0x040007C6 RID: 1990
			InvalidReminderOffset,
			// Token: 0x040007C7 RID: 1991
			ExSearchFolderNoAssociatedItem,
			// Token: 0x040007C8 RID: 1992
			DatabaseLocationNotAvailable,
			// Token: 0x040007C9 RID: 1993
			MigrationJobConnectionSettingsInvalid,
			// Token: 0x040007CA RID: 1994
			MailboxSearchObjectNotExist,
			// Token: 0x040007CB RID: 1995
			ExInvalidValueTypeForCalculatedProperty,
			// Token: 0x040007CC RID: 1996
			PrincipalNotAllowedByPolicy,
			// Token: 0x040007CD RID: 1997
			ExCorruptDataRecoverError,
			// Token: 0x040007CE RID: 1998
			ActiveMonitoringUnknownGenericRpcCommand,
			// Token: 0x040007CF RID: 1999
			ExTooManySubscriptionsOnPublicStore,
			// Token: 0x040007D0 RID: 2000
			ExCalculatedPropertyStreamAccessNotSupported,
			// Token: 0x040007D1 RID: 2001
			InvalidRmsUrl,
			// Token: 0x040007D2 RID: 2002
			AmRpcOperationNotImplemented,
			// Token: 0x040007D3 RID: 2003
			SharingFolderName,
			// Token: 0x040007D4 RID: 2004
			OperationNotAllowed,
			// Token: 0x040007D5 RID: 2005
			SharingPolicyNotFound,
			// Token: 0x040007D6 RID: 2006
			InvalidExternalSharingInitiatorException,
			// Token: 0x040007D7 RID: 2007
			ExSortGroupNotSupportedForProperty,
			// Token: 0x040007D8 RID: 2008
			ConversationContainsDuplicateMids,
			// Token: 0x040007D9 RID: 2009
			ParticipantPropertyTooBig,
			// Token: 0x040007DA RID: 2010
			CopyToDumpsterFailure,
			// Token: 0x040007DB RID: 2011
			ExNotSupportedConfigurationSearchFlags,
			// Token: 0x040007DC RID: 2012
			InvalidParticipant,
			// Token: 0x040007DD RID: 2013
			ExInvalidEventWatermarkBadOrigin,
			// Token: 0x040007DE RID: 2014
			ExPropertyNotValidOnOccurrence,
			// Token: 0x040007DF RID: 2015
			DefaultFolderAccessDenied,
			// Token: 0x040007E0 RID: 2016
			COWMailboxInfoCacheTimeout,
			// Token: 0x040007E1 RID: 2017
			ExFinalEventFound,
			// Token: 0x040007E2 RID: 2018
			FailedToFindLicenseUri,
			// Token: 0x040007E3 RID: 2019
			BadEnumValue,
			// Token: 0x040007E4 RID: 2020
			NotStreamablePropertyValue,
			// Token: 0x040007E5 RID: 2021
			ObjectMustBeOfType,
			// Token: 0x040007E6 RID: 2022
			ErrorCorruptedData,
			// Token: 0x040007E7 RID: 2023
			QueryUsageRightsPrelicenseResponseHasFailure,
			// Token: 0x040007E8 RID: 2024
			MigrationMailboxNotFoundOnServerError,
			// Token: 0x040007E9 RID: 2025
			StreamPropertyNotFound,
			// Token: 0x040007EA RID: 2026
			idOscContactSourcesForContactParseError,
			// Token: 0x040007EB RID: 2027
			ExIncorrectOriginalTimeInExtendedExceptionInfo,
			// Token: 0x040007EC RID: 2028
			DeleteFromDumpsterFailure,
			// Token: 0x040007ED RID: 2029
			DetailLevelNotAllowedByPolicy,
			// Token: 0x040007EE RID: 2030
			ErrorCalendarReminderNegative,
			// Token: 0x040007EF RID: 2031
			GroupMailboxAccessSidConstructionFailed,
			// Token: 0x040007F0 RID: 2032
			OperationTimedOut,
			// Token: 0x040007F1 RID: 2033
			MailboxVersionTooLow,
			// Token: 0x040007F2 RID: 2034
			ExCannotSaveSyncStateFolder,
			// Token: 0x040007F3 RID: 2035
			ExNotSupportedNotificationType,
			// Token: 0x040007F4 RID: 2036
			ConversionInvalidItemType,
			// Token: 0x040007F5 RID: 2037
			CannotFindRequestIndexEntry,
			// Token: 0x040007F6 RID: 2038
			ExBodyFormatConversionNotSupported,
			// Token: 0x040007F7 RID: 2039
			ExConfigExisted,
			// Token: 0x040007F8 RID: 2040
			CannotGetSynchronizeBuffers,
			// Token: 0x040007F9 RID: 2041
			TenantLicensesDistributionPointMismatch,
			// Token: 0x040007FA RID: 2042
			ExUrlNotFound,
			// Token: 0x040007FB RID: 2043
			InvalidICalElement,
			// Token: 0x040007FC RID: 2044
			FailedToRpcAcquireUseLicenses,
			// Token: 0x040007FD RID: 2045
			ExUnknownPattern,
			// Token: 0x040007FE RID: 2046
			ExFilterNotSupportedForProperty,
			// Token: 0x040007FF RID: 2047
			SearchObjectIsInvalid,
			// Token: 0x04000800 RID: 2048
			FailedToDownloadServerLicensingMExData,
			// Token: 0x04000801 RID: 2049
			ActiveMonitoringOperationFailedException,
			// Token: 0x04000802 RID: 2050
			UnsupportedClientOperation,
			// Token: 0x04000803 RID: 2051
			ExternalLicensingDisabledForEnterprise,
			// Token: 0x04000804 RID: 2052
			JunkEmailBlockedListXsoTooBigException,
			// Token: 0x04000805 RID: 2053
			AmDatabaseCopyNotFoundException,
			// Token: 0x04000806 RID: 2054
			ValueNotRecognizedForAttribute,
			// Token: 0x04000807 RID: 2055
			AmOperationFailedWithEcException,
			// Token: 0x04000808 RID: 2056
			RuleParseError,
			// Token: 0x04000809 RID: 2057
			FolderRuleErrorRecordForSpecificRule,
			// Token: 0x0400080A RID: 2058
			DataMoveReplicationConstraintUnknown,
			// Token: 0x0400080B RID: 2059
			ExPropertyValidationFailed,
			// Token: 0x0400080C RID: 2060
			BadTimeFormatInChangeDate,
			// Token: 0x0400080D RID: 2061
			ExDeleteNotSupportedForCalculatedProperty,
			// Token: 0x0400080E RID: 2062
			DagNetworkCreateDupName,
			// Token: 0x0400080F RID: 2063
			DataMoveReplicationConstraintNotSatisfiedInvalidConstraint,
			// Token: 0x04000810 RID: 2064
			AmOperationFailedException,
			// Token: 0x04000811 RID: 2065
			ExMismatchedSyncStateDataType,
			// Token: 0x04000812 RID: 2066
			RpcServerIgnoreNotFoundTeamMailbox,
			// Token: 0x04000813 RID: 2067
			AnchorDatabaseNotFound,
			// Token: 0x04000814 RID: 2068
			ExNoMatchingStorePropertyDefinition,
			// Token: 0x04000815 RID: 2069
			FailedToRetrieveUserLicense,
			// Token: 0x04000816 RID: 2070
			InvalidMailboxType,
			// Token: 0x04000817 RID: 2071
			FailedToFindServerInfo,
			// Token: 0x04000818 RID: 2072
			FailedToReadSharedServerBoxRacIdentityFromIRMConfig,
			// Token: 0x04000819 RID: 2073
			ExNonCalendarItemReturned,
			// Token: 0x0400081A RID: 2074
			DelegateValidationFailed,
			// Token: 0x0400081B RID: 2075
			ExResponseTypeNoSubjectPrefix,
			// Token: 0x0400081C RID: 2076
			MigrationNSPINoUsersFound,
			// Token: 0x0400081D RID: 2077
			JunkEmailTrustedListXsoGenericException,
			// Token: 0x0400081E RID: 2078
			AttemptingSessionCreationAgainstWrongGroupMailbox,
			// Token: 0x0400081F RID: 2079
			descInvalidTypeInBookingPolicyConfig,
			// Token: 0x04000820 RID: 2080
			NoSupportException,
			// Token: 0x04000821 RID: 2081
			MalformedWorkingHours,
			// Token: 0x04000822 RID: 2082
			DataMoveReplicationConstraintNotSatisfiedThrottled,
			// Token: 0x04000823 RID: 2083
			PublicFolderOperationDenied,
			// Token: 0x04000824 RID: 2084
			ExInvalidO12BytesToSkip,
			// Token: 0x04000825 RID: 2085
			ColumnError,
			// Token: 0x04000826 RID: 2086
			ErrorValidateXsoDriverProperty,
			// Token: 0x04000827 RID: 2087
			AmInvalidActionCodeException,
			// Token: 0x04000828 RID: 2088
			FailedToReadIRMConfig,
			// Token: 0x04000829 RID: 2089
			CultureMismatchAfterConnect,
			// Token: 0x0400082A RID: 2090
			ExternalIdentityInvalidSid,
			// Token: 0x0400082B RID: 2091
			MaxRemindersExceeded,
			// Token: 0x0400082C RID: 2092
			NonUniqueAssociationError,
			// Token: 0x0400082D RID: 2093
			ExConfigSerializeDictionaryFailed,
			// Token: 0x0400082E RID: 2094
			PublicFolderSyncFolderFailed,
			// Token: 0x0400082F RID: 2095
			ErrorUnsupportedConfigurationXmlCategory,
			// Token: 0x04000830 RID: 2096
			FailedToRetrieveServerLicense,
			// Token: 0x04000831 RID: 2097
			ExPropertyRequiresStreaming,
			// Token: 0x04000832 RID: 2098
			ConversationWithoutRootNode,
			// Token: 0x04000833 RID: 2099
			idClientSessionInfoTypeParseException,
			// Token: 0x04000834 RID: 2100
			ExBatchBuilderError,
			// Token: 0x04000835 RID: 2101
			ActiveManagerGenericRpcVersionNotSupported,
			// Token: 0x04000836 RID: 2102
			ErrorTeamMailboxUserVersionUnqualified,
			// Token: 0x04000837 RID: 2103
			ExInvalidMonthNthOccurence,
			// Token: 0x04000838 RID: 2104
			ExInvalidEventWatermarkBadEventCounter,
			// Token: 0x04000839 RID: 2105
			ErrorInvalidDateFormat,
			// Token: 0x0400083A RID: 2106
			TaskRecurrenceNotSupported,
			// Token: 0x0400083B RID: 2107
			CannotCreateCollector,
			// Token: 0x0400083C RID: 2108
			DataBaseNotFoundError,
			// Token: 0x0400083D RID: 2109
			UnsupportedReportType,
			// Token: 0x0400083E RID: 2110
			ExceptionIsNotPublicFolder,
			// Token: 0x0400083F RID: 2111
			ExTooManySubscriptions,
			// Token: 0x04000840 RID: 2112
			ActiveMonitoringRpcVersionNotSupported,
			// Token: 0x04000841 RID: 2113
			ExReplyToTooManyRecipients,
			// Token: 0x04000842 RID: 2114
			OscSyncLockNotFound,
			// Token: 0x04000843 RID: 2115
			ErrorFailedToDeletePublicFolder,
			// Token: 0x04000844 RID: 2116
			RpcServerIgnoreClosedTeamMailbox,
			// Token: 0x04000845 RID: 2117
			FailedToVerifyDRMPropsSignature,
			// Token: 0x04000846 RID: 2118
			MigrationSystemMailboxNotFound,
			// Token: 0x04000847 RID: 2119
			InvalidSmtpAddress,
			// Token: 0x04000848 RID: 2120
			InvokingMethodNotSupported,
			// Token: 0x04000849 RID: 2121
			FailedToGetOrgContainer,
			// Token: 0x0400084A RID: 2122
			ValidationForServiceLocationResponseFailed,
			// Token: 0x0400084B RID: 2123
			MigrationObjectsCountStringContacts,
			// Token: 0x0400084C RID: 2124
			MailboxSearchEwsFailedExceptionWithError,
			// Token: 0x0400084D RID: 2125
			ParsingErrorAt,
			// Token: 0x0400084E RID: 2126
			ExDictionaryDataCorruptedDuplicateKey,
			// Token: 0x0400084F RID: 2127
			MigrationNSPIMissingRequiredField,
			// Token: 0x04000850 RID: 2128
			MigrationTransientError,
			// Token: 0x04000851 RID: 2129
			ExFolderSetPropsFailed,
			// Token: 0x04000852 RID: 2130
			AmReferralException,
			// Token: 0x04000853 RID: 2131
			TaskServerTransientException,
			// Token: 0x04000854 RID: 2132
			AmOperationNotValidOnCurrentRole,
			// Token: 0x04000855 RID: 2133
			RangedParameter,
			// Token: 0x04000856 RID: 2134
			IsNotMailboxOwner,
			// Token: 0x04000857 RID: 2135
			JunkEmailBlockedListXsoDuplicateException,
			// Token: 0x04000858 RID: 2136
			ExTooManyDuplicateDataColumns,
			// Token: 0x04000859 RID: 2137
			MigrationErrorString,
			// Token: 0x0400085A RID: 2138
			MigrationGroupMembersNotAvailable,
			// Token: 0x0400085B RID: 2139
			RpcServerIgnoreUnlinkedTeamMailbox,
			// Token: 0x0400085C RID: 2140
			AmServerException,
			// Token: 0x0400085D RID: 2141
			ExConversationNotFound,
			// Token: 0x0400085E RID: 2142
			ErrorNotSupportedLanguageWithInstalledLanguagePack,
			// Token: 0x0400085F RID: 2143
			ErrorInPlaceHoldIdentityChanged,
			// Token: 0x04000860 RID: 2144
			InvalidSharingDataException,
			// Token: 0x04000861 RID: 2145
			MigrationOrganizationNotFound,
			// Token: 0x04000862 RID: 2146
			AmDbMountNotAllowedDueToAcllErrorException,
			// Token: 0x04000863 RID: 2147
			PublicFolderSyncFolderHierarchyFailedAfterMultipleAttempts,
			// Token: 0x04000864 RID: 2148
			NoInternalEwsAvailableException,
			// Token: 0x04000865 RID: 2149
			AmInvalidConfiguration,
			// Token: 0x04000866 RID: 2150
			InvalidLocalDirectorySecurityIdentifier,
			// Token: 0x04000867 RID: 2151
			CalendarOriginatorIdCorrupt,
			// Token: 0x04000868 RID: 2152
			AppointmentActionNotSupported,
			// Token: 0x04000869 RID: 2153
			TooManyActiveManagerClientRPCs,
			// Token: 0x0400086A RID: 2154
			RightsNotAllowedByPolicy,
			// Token: 0x0400086B RID: 2155
			AmServerNotFoundToVerifyRpcVersion,
			// Token: 0x0400086C RID: 2156
			TaskOperationFailedException,
			// Token: 0x0400086D RID: 2157
			ConversionMaxRecipientExceeded,
			// Token: 0x0400086E RID: 2158
			NoSharingHandlerFoundException,
			// Token: 0x0400086F RID: 2159
			ExternalUserNotFound,
			// Token: 0x04000870 RID: 2160
			ExTenantAccessBlocked,
			// Token: 0x04000871 RID: 2161
			ExUnresolvedRecipient,
			// Token: 0x04000872 RID: 2162
			ExInvalidAttachmentId,
			// Token: 0x04000873 RID: 2163
			ErrorFolderAlreadyExists,
			// Token: 0x04000874 RID: 2164
			InvalidAddressFormat,
			// Token: 0x04000875 RID: 2165
			ExOccurrenceNotPresent,
			// Token: 0x04000876 RID: 2166
			ExComparisonOperatorNotSupported,
			// Token: 0x04000877 RID: 2167
			ExternalLicensingDisabledForTenant,
			// Token: 0x04000878 RID: 2168
			CantParseParticipant,
			// Token: 0x04000879 RID: 2169
			ErrorFolderSave,
			// Token: 0x0400087A RID: 2170
			ExMailboxAccessDenied,
			// Token: 0x0400087B RID: 2171
			ExInvalidTypeInBlob,
			// Token: 0x0400087C RID: 2172
			PromoteVeventFailure,
			// Token: 0x0400087D RID: 2173
			FolderRuleErrorRecord,
			// Token: 0x0400087E RID: 2174
			MalformedTimeZoneWorkingHours,
			// Token: 0x0400087F RID: 2175
			ExInvalidFileTimeInRecurrenceBlob,
			// Token: 0x04000880 RID: 2176
			ErrorInvalidInPlaceHoldIdentity,
			// Token: 0x04000881 RID: 2177
			ExBodyConversionNotSupportedType,
			// Token: 0x04000882 RID: 2178
			SaveConfigurationItem,
			// Token: 0x04000883 RID: 2179
			RpcServerUnhandledException,
			// Token: 0x04000884 RID: 2180
			TaskInvalidFlagStatus,
			// Token: 0x04000885 RID: 2181
			RpcServerParameterSerializationError,
			// Token: 0x04000886 RID: 2182
			ExTimeInExtendedInfoNotSameAsExceptionInfo,
			// Token: 0x04000887 RID: 2183
			FailedToGetRmsTemplates,
			// Token: 0x04000888 RID: 2184
			ExInvalidFolderType,
			// Token: 0x04000889 RID: 2185
			ExItemNotFoundInConversation,
			// Token: 0x0400088A RID: 2186
			ExInvalidMinutePeriod,
			// Token: 0x0400088B RID: 2187
			ConversionMaxRecipientSizeExceeded,
			// Token: 0x0400088C RID: 2188
			ErrorInvalidStatisticsStartIndex,
			// Token: 0x0400088D RID: 2189
			MigrationInvalidStatus,
			// Token: 0x0400088E RID: 2190
			QueryUsageRightsNoPrelicenseResponse,
			// Token: 0x0400088F RID: 2191
			ExBatchBuilderNeedsPropertyToConvertRT,
			// Token: 0x04000890 RID: 2192
			NonUniqueRecipientByExternalIdError,
			// Token: 0x04000891 RID: 2193
			MultipleProvisioningMailboxes,
			// Token: 0x04000892 RID: 2194
			ExMailboxMustBeAccessedAsOwner,
			// Token: 0x04000893 RID: 2195
			CannotCreateOscFolderBecauseOfConflict,
			// Token: 0x04000894 RID: 2196
			LicenseUriInvalidForTenant,
			// Token: 0x04000895 RID: 2197
			ExConfigNameInvalid,
			// Token: 0x04000896 RID: 2198
			ExInvalidMultivaluePropertyFilter,
			// Token: 0x04000897 RID: 2199
			ExInvalidResponseType,
			// Token: 0x04000898 RID: 2200
			ErrorTeamMailboxSendingNotifications,
			// Token: 0x04000899 RID: 2201
			AnonymousPublishingUrlValidationException,
			// Token: 0x0400089A RID: 2202
			MapiCannotModifyPropertyTable,
			// Token: 0x0400089B RID: 2203
			Ex12NotSupportedDeleteItemFlags,
			// Token: 0x0400089C RID: 2204
			DisplayNameRequiredForRoutingType,
			// Token: 0x0400089D RID: 2205
			UserPhotoImageTooSmall,
			// Token: 0x0400089E RID: 2206
			ProgramError,
			// Token: 0x0400089F RID: 2207
			ErrorInvalidQuery,
			// Token: 0x040008A0 RID: 2208
			EulNotFoundInContainerItem,
			// Token: 0x040008A1 RID: 2209
			FailedToAquirePublishLicense,
			// Token: 0x040008A2 RID: 2210
			InvalidFolderId,
			// Token: 0x040008A3 RID: 2211
			PositiveParameter,
			// Token: 0x040008A4 RID: 2212
			RecipientAddressNotSpecifiedForExternalLicensing,
			// Token: 0x040008A5 RID: 2213
			RpcServerRequestSuccess,
			// Token: 0x040008A6 RID: 2214
			AdUserNotFoundException,
			// Token: 0x040008A7 RID: 2215
			idNotSupportedWithServerVersionException,
			// Token: 0x040008A8 RID: 2216
			RpcServerStorageError,
			// Token: 0x040008A9 RID: 2217
			QueryUsageRightsPrelicenseResponseFailedToExtractRights,
			// Token: 0x040008AA RID: 2218
			UnexpectedTag,
			// Token: 0x040008AB RID: 2219
			MigrationPermanentError,
			// Token: 0x040008AC RID: 2220
			CannotShareFolderException,
			// Token: 0x040008AD RID: 2221
			ErrorADUserFoundByReadOnlyButNotWrite,
			// Token: 0x040008AE RID: 2222
			ConversionMaxEmbeddedDepthExceeded,
			// Token: 0x040008AF RID: 2223
			RpcServerWrongRequestServer,
			// Token: 0x040008B0 RID: 2224
			CantFindCalendarFolderException,
			// Token: 0x040008B1 RID: 2225
			CannotCreateEmbeddedItem,
			// Token: 0x040008B2 RID: 2226
			ErrorTeamMailboxGetUserMailboxDatabaseFailed,
			// Token: 0x040008B3 RID: 2227
			SystemAPIFailed,
			// Token: 0x040008B4 RID: 2228
			OleConversionInitError,
			// Token: 0x040008B5 RID: 2229
			ExUnableToGetStreamProperty,
			// Token: 0x040008B6 RID: 2230
			ExMeetingCantCrossOtherOccurrences,
			// Token: 0x040008B7 RID: 2231
			ExConfigurationNotFound,
			// Token: 0x040008B8 RID: 2232
			ExNotSupportedCreateMode,
			// Token: 0x040008B9 RID: 2233
			AmDbAcllErrorNoReplicaInstance,
			// Token: 0x040008BA RID: 2234
			SyncStateCollision,
			// Token: 0x040008BB RID: 2235
			RmLicenseRetrieveFailed,
			// Token: 0x040008BC RID: 2236
			DiscoveryMailboxCannotBeSourceOrTarget,
			// Token: 0x040008BD RID: 2237
			ErrorNonUniqueLegacyDN,
			// Token: 0x040008BE RID: 2238
			ExMclIsTooBig,
			// Token: 0x040008BF RID: 2239
			ExInvalidPropertyType,
			// Token: 0x040008C0 RID: 2240
			MailboxDatabaseRequired,
			// Token: 0x040008C1 RID: 2241
			JunkEmailTrustedListXsoTooBigException,
			// Token: 0x040008C2 RID: 2242
			ExOperationNotSupportedForRoutingType,
			// Token: 0x040008C3 RID: 2243
			DiscoveryMailboxIsNotUnique,
			// Token: 0x040008C4 RID: 2244
			ConflictingObjectType,
			// Token: 0x040008C5 RID: 2245
			MapiCannotOpenMailbox,
			// Token: 0x040008C6 RID: 2246
			ExSyncStateCorrupted,
			// Token: 0x040008C7 RID: 2247
			ExInvalidHexString,
			// Token: 0x040008C8 RID: 2248
			MigrationUserSkippedItemString,
			// Token: 0x040008C9 RID: 2249
			FolderRuleErrorInvalidRecipient,
			// Token: 0x040008CA RID: 2250
			MapiCannotMatchAttachmentIds,
			// Token: 0x040008CB RID: 2251
			FailedToAcquireRacAndClc,
			// Token: 0x040008CC RID: 2252
			NotificationEmailSubjectCreated,
			// Token: 0x040008CD RID: 2253
			SyncStateMissing,
			// Token: 0x040008CE RID: 2254
			FailedToFindTargetUriFromMExData,
			// Token: 0x040008CF RID: 2255
			AmDatabaseNotFoundException,
			// Token: 0x040008D0 RID: 2256
			ExInvalidHexCharacter,
			// Token: 0x040008D1 RID: 2257
			ServerForDatabaseNotFound,
			// Token: 0x040008D2 RID: 2258
			RpcClientException,
			// Token: 0x040008D3 RID: 2259
			ErrorCouldNotUpdateMasterIdentityProperty,
			// Token: 0x040008D4 RID: 2260
			DataMoveReplicationConstraintSatisfied,
			// Token: 0x040008D5 RID: 2261
			UnknownOscProvider,
			// Token: 0x040008D6 RID: 2262
			ErrorTeamMailboxUserNotResolved,
			// Token: 0x040008D7 RID: 2263
			FolderRuleErrorGroupDoesNotResolve,
			// Token: 0x040008D8 RID: 2264
			DatabaseNotFound,
			// Token: 0x040008D9 RID: 2265
			ExSearchFolderCorruptOutlookBlob,
			// Token: 0x040008DA RID: 2266
			ExCannotMoveOrCopyOccurrenceItem,
			// Token: 0x040008DB RID: 2267
			JunkEmailBlockedListXsoFormatException,
			// Token: 0x040008DC RID: 2268
			InvalidDueDate2,
			// Token: 0x040008DD RID: 2269
			MigrationRunspaceError,
			// Token: 0x040008DE RID: 2270
			TeamMailboxMessageReactivatedSubject,
			// Token: 0x040008DF RID: 2271
			CompositeError,
			// Token: 0x040008E0 RID: 2272
			ExNullItemIdParameter,
			// Token: 0x040008E1 RID: 2273
			ExItemDoesNotBelongToCurrentFolder,
			// Token: 0x040008E2 RID: 2274
			AddressRequiredForRoutingType,
			// Token: 0x040008E3 RID: 2275
			InconsistentCalendarType,
			// Token: 0x040008E4 RID: 2276
			PropertyErrorString,
			// Token: 0x040008E5 RID: 2277
			ExStartDateCantBeGreaterThanMaximum,
			// Token: 0x040008E6 RID: 2278
			ExEndDateCantExceedMaxDate,
			// Token: 0x040008E7 RID: 2279
			CannotCreateManifestEx,
			// Token: 0x040008E8 RID: 2280
			ExNewerVersionedSyncState,
			// Token: 0x040008E9 RID: 2281
			ErrorInvalidTimeFormat,
			// Token: 0x040008EA RID: 2282
			InvalidTagName,
			// Token: 0x040008EB RID: 2283
			MigrationJobItemRecipientTypeMismatch,
			// Token: 0x040008EC RID: 2284
			ExModifiedOccurrenceCrossingAdjacentOccurrenceBoundary,
			// Token: 0x040008ED RID: 2285
			ExComparisonOperatorNotSupportedForProperty,
			// Token: 0x040008EE RID: 2286
			NotAuthorizedtoAccessGroupMailbox,
			// Token: 0x040008EF RID: 2287
			TaskServerException,
			// Token: 0x040008F0 RID: 2288
			ErrorInvalidQueryTooLong,
			// Token: 0x040008F1 RID: 2289
			ExConfigDataCorrupted,
			// Token: 0x040008F2 RID: 2290
			LicenseExpired,
			// Token: 0x040008F3 RID: 2291
			ElementHasUnsupportedValue,
			// Token: 0x040008F4 RID: 2292
			CannotResolvePropertyTagsToPropertyDefinitions,
			// Token: 0x040008F5 RID: 2293
			NonNegativeParameter,
			// Token: 0x040008F6 RID: 2294
			CreateConfigurationItem,
			// Token: 0x040008F7 RID: 2295
			ActiveMonitoringOperationFailedWithEcException,
			// Token: 0x040008F8 RID: 2296
			ExInvalidRecurrenceInterval,
			// Token: 0x040008F9 RID: 2297
			FederatedMailboxNotSet,
			// Token: 0x040008FA RID: 2298
			JunkEmailValidationError,
			// Token: 0x040008FB RID: 2299
			MigrationUnexpectedExchangePrincipalFound,
			// Token: 0x040008FC RID: 2300
			RpcServerRequestThrottled,
			// Token: 0x040008FD RID: 2301
			DefaultFolderNotFoundInPublicFolderMailbox,
			// Token: 0x040008FE RID: 2302
			JunkEmailTrustedListInternalToOrganizationException,
			// Token: 0x040008FF RID: 2303
			ExStartDateCantBeLessThanMinimum,
			// Token: 0x04000900 RID: 2304
			ExPDLCorruptOutlookBlob,
			// Token: 0x04000901 RID: 2305
			DataMoveReplicationConstraintNotSatisfied,
			// Token: 0x04000902 RID: 2306
			ExInvalidLicense,
			// Token: 0x04000903 RID: 2307
			ErrorCalendarReminderTooLarge,
			// Token: 0x04000904 RID: 2308
			ExSetNotSupportedForCalculatedProperty,
			// Token: 0x04000905 RID: 2309
			ExFilterNotSupported,
			// Token: 0x04000906 RID: 2310
			JunkEmailTrustedListOwnersEmailAddressException,
			// Token: 0x04000907 RID: 2311
			ErrorUnsupportedConfigurationXmlVersion,
			// Token: 0x04000908 RID: 2312
			ExInvalidMultivalueElement,
			// Token: 0x04000909 RID: 2313
			PublicFolderHierarchySessionNull,
			// Token: 0x0400090A RID: 2314
			NotificationEmailSubjectCompleted,
			// Token: 0x0400090B RID: 2315
			ErrorTeamMailboxUserTypeUnqualified,
			// Token: 0x0400090C RID: 2316
			ConversationCreatorSidNotSet,
			// Token: 0x0400090D RID: 2317
			ExNullParameter,
			// Token: 0x0400090E RID: 2318
			ExEmptyCollection,
			// Token: 0x0400090F RID: 2319
			MigrationAttachmentNotFound,
			// Token: 0x04000910 RID: 2320
			ActiveManagerUnknownGenericRpcCommand,
			// Token: 0x04000911 RID: 2321
			RecipientAddressInvalid,
			// Token: 0x04000912 RID: 2322
			ExInvalidExceptionInfoSubstringLength,
			// Token: 0x04000913 RID: 2323
			DagNetworkCannotRemoveActiveSubnet,
			// Token: 0x04000914 RID: 2324
			ExInvalidNullParameterForChangeTypes,
			// Token: 0x04000915 RID: 2325
			ConversionMaxBodyPartsExceeded,
			// Token: 0x04000916 RID: 2326
			JunkEmailTrustedListXsoDuplicateException,
			// Token: 0x04000917 RID: 2327
			CannotAuthenticateUserByTheClientSecurityContext,
			// Token: 0x04000918 RID: 2328
			ExComparisonFilterPropertiesNotSupported,
			// Token: 0x04000919 RID: 2329
			CannotGetLongTermIdFromId,
			// Token: 0x0400091A RID: 2330
			RecoverableItemsAccessDeniedException,
			// Token: 0x0400091B RID: 2331
			ErrorExTimeZoneValueMultipleGmtMatches,
			// Token: 0x0400091C RID: 2332
			ExUnsupportedCharset,
			// Token: 0x0400091D RID: 2333
			FolderRuleErrorInvalidGroup,
			// Token: 0x0400091E RID: 2334
			CannotGetIdFromLongTermId,
			// Token: 0x0400091F RID: 2335
			ErrorXsoObjectPropertyValidationError,
			// Token: 0x04000920 RID: 2336
			ErrorInvalidItemHoldPeriod,
			// Token: 0x04000921 RID: 2337
			ProvisioningMailboxNotFound,
			// Token: 0x04000922 RID: 2338
			ExInvalidBase64StringFormat,
			// Token: 0x04000923 RID: 2339
			ExInvalidWABObjectType,
			// Token: 0x04000924 RID: 2340
			AggregatedMailboxNotFound,
			// Token: 0x04000925 RID: 2341
			AnchorServerNotFound,
			// Token: 0x04000926 RID: 2342
			ReminderPropertyNotSupported,
			// Token: 0x04000927 RID: 2343
			RpcServerParameterInvalidError,
			// Token: 0x04000928 RID: 2344
			ExSearchFolderAlreadyExists,
			// Token: 0x04000929 RID: 2345
			CannotVerifyDRMPropsSignatureUserNotFound,
			// Token: 0x0400092A RID: 2346
			DataMoveReplicationConstraintSatisfiedForNonReplicatedDatabase,
			// Token: 0x0400092B RID: 2347
			ExCannotStampDefaultFolderId,
			// Token: 0x0400092C RID: 2348
			FailedToRpcAcquireRacAndClc,
			// Token: 0x0400092D RID: 2349
			InvalidCacheEntryId,
			// Token: 0x0400092E RID: 2350
			FailedToDownloadCertificationMExData,
			// Token: 0x0400092F RID: 2351
			MigrationGroupMembersAlreadyAvailable,
			// Token: 0x04000930 RID: 2352
			TimeZoneReferenceWithNullTimeZone,
			// Token: 0x04000931 RID: 2353
			ExInvalidBodyFormat,
			// Token: 0x04000932 RID: 2354
			idUnableToAddDefaultTaskFolderToDefaultTaskGroup,
			// Token: 0x04000933 RID: 2355
			ReplayServiceDown,
			// Token: 0x04000934 RID: 2356
			FailedToAcquireTenantLicenses,
			// Token: 0x04000935 RID: 2357
			ExModifiedOccurrenceCantHaveStartDateAsAdjacentOccurrence,
			// Token: 0x04000936 RID: 2358
			WrongTimeZoneReference,
			// Token: 0x04000937 RID: 2359
			ExConstraintViolationByteArrayLengthTooLong,
			// Token: 0x04000938 RID: 2360
			ExPropertyError,
			// Token: 0x04000939 RID: 2361
			ExInvalidChangeType,
			// Token: 0x0400093A RID: 2362
			ExSaveFailedBecauseOfConflicts,
			// Token: 0x0400093B RID: 2363
			SharingFolderNameWithSuffix,
			// Token: 0x0400093C RID: 2364
			ExPropertyNotStreamable,
			// Token: 0x0400093D RID: 2365
			MigrationInvalidTargetProxyAddress,
			// Token: 0x0400093E RID: 2366
			RepairingIsNotSetSinceMonitorEntryIsNotFound,
			// Token: 0x0400093F RID: 2367
			PublicFolderConnectionThreadLimitExceeded,
			// Token: 0x04000940 RID: 2368
			TeamMailboxMessageMemberInvitationSubject,
			// Token: 0x04000941 RID: 2369
			ExInvalidValueForFlagsCalculatedProperty,
			// Token: 0x04000942 RID: 2370
			ADUserNotFoundId,
			// Token: 0x04000943 RID: 2371
			InvalidUrlScheme,
			// Token: 0x04000944 RID: 2372
			ExTypeSerializationNotSupported,
			// Token: 0x04000945 RID: 2373
			MigrationMRSJobMissing,
			// Token: 0x04000946 RID: 2374
			FailedToCheckPublishLicenseOwnership,
			// Token: 0x04000947 RID: 2375
			CannotSynchronizeManifestEx,
			// Token: 0x04000948 RID: 2376
			StoreDataInvalid,
			// Token: 0x04000949 RID: 2377
			ExTooManyInstancesOnSeries,
			// Token: 0x0400094A RID: 2378
			InvalidAddressError,
			// Token: 0x0400094B RID: 2379
			ExGetNotSupportedForCalculatedProperty,
			// Token: 0x0400094C RID: 2380
			ExTooManyObjects
		}
	}
}
