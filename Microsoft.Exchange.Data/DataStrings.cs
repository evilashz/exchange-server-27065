using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000D1 RID: 209
	internal static class DataStrings
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x00012EA4 File Offset: 0x000110A4
		static DataStrings()
		{
			DataStrings.stringIDs.Add(401351504U, "NotesProxyAddressPrefixDisplayName");
			DataStrings.stringIDs.Add(2802501868U, "InvalidCallerIdItemTypePersonalContact");
			DataStrings.stringIDs.Add(1831343041U, "ExceptionParseNotSupported");
			DataStrings.stringIDs.Add(2207492931U, "InvalidKeySelectionA");
			DataStrings.stringIDs.Add(2003039265U, "ConnectorTypePartner");
			DataStrings.stringIDs.Add(214463947U, "SclValue");
			DataStrings.stringIDs.Add(343989129U, "MessageStatusLocked");
			DataStrings.stringIDs.Add(2044618123U, "DaysOfWeek_None");
			DataStrings.stringIDs.Add(3268914636U, "InvalidAddressSpaceTypeNullOrEmpty");
			DataStrings.stringIDs.Add(1025341760U, "ProxyAddressShouldNotBeAllSpace");
			DataStrings.stringIDs.Add(3216786356U, "CalendarSharingFreeBusyReviewer");
			DataStrings.stringIDs.Add(2296262216U, "ProtocolLocalRpc");
			DataStrings.stringIDs.Add(3662889629U, "InsufficientSpace");
			DataStrings.stringIDs.Add(1357933441U, "ExchangeUsers");
			DataStrings.stringIDs.Add(4099951545U, "RoutingIncompatibleDeliveryDomain");
			DataStrings.stringIDs.Add(4134731995U, "EnterpriseTrialEdition");
			DataStrings.stringIDs.Add(315555574U, "InvalidNumberFormatString");
			DataStrings.stringIDs.Add(1448686489U, "Partners");
			DataStrings.stringIDs.Add(3119708545U, "CustomScheduleDaily12PM");
			DataStrings.stringIDs.Add(2191591450U, "CoexistenceTrialEdition");
			DataStrings.stringIDs.Add(3866345678U, "CustomExtensionInvalidArgument");
			DataStrings.stringIDs.Add(955250317U, "NonWorkingHours");
			DataStrings.stringIDs.Add(801480496U, "ClientAccessProtocolIMAP4");
			DataStrings.stringIDs.Add(319956112U, "ShadowMessagePreferencePreferRemote");
			DataStrings.stringIDs.Add(842516494U, "CustomScheduleDailyFromMidnightTo4AM");
			DataStrings.stringIDs.Add(2432881730U, "CustomScheduleDailyFrom8AMTo5PMAtWeekDays");
			DataStrings.stringIDs.Add(3910386293U, "EventLogText");
			DataStrings.stringIDs.Add(2532083337U, "ClientAccessAdfsAuthentication");
			DataStrings.stringIDs.Add(3646660508U, "ErrorADFormatError");
			DataStrings.stringIDs.Add(4100370227U, "MeetingFullDetailsWithAttendees");
			DataStrings.stringIDs.Add(2139945176U, "CustomScheduleDaily3AM");
			DataStrings.stringIDs.Add(1571847379U, "ExceptionCalculatedDependsOnCalculated");
			DataStrings.stringIDs.Add(4153254550U, "RoutingNoRouteToMdb");
			DataStrings.stringIDs.Add(3267940029U, "SmtpReceiveCapabilitiesAcceptProxyProtocol");
			DataStrings.stringIDs.Add(554923823U, "DeferReasonADTransientFailureDuringContentConversion");
			DataStrings.stringIDs.Add(3857745828U, "HeaderPromotionModeMayCreate");
			DataStrings.stringIDs.Add(2392738789U, "ConnectorSourceHybridWizard");
			DataStrings.stringIDs.Add(2680336401U, "DeferReasonAgent");
			DataStrings.stringIDs.Add(86136247U, "InvalidCustomMenuKeyMappingA");
			DataStrings.stringIDs.Add(352888273U, "InvalidResourcePropertySyntax");
			DataStrings.stringIDs.Add(651446229U, "SmtpReceiveCapabilitiesAcceptCloudServicesMail");
			DataStrings.stringIDs.Add(830368596U, "CcMailProxyAddressPrefixDisplayName");
			DataStrings.stringIDs.Add(1420072811U, "CustomScheduleEveryHour");
			DataStrings.stringIDs.Add(255146954U, "DeferReasonConcurrencyLimitInStoreDriver");
			DataStrings.stringIDs.Add(1322244886U, "GroupByMonth");
			DataStrings.stringIDs.Add(906828259U, "ContactsSharing");
			DataStrings.stringIDs.Add(2020388072U, "ExceptionUnsupportedNetworkProtocol");
			DataStrings.stringIDs.Add(452471808U, "SmtpReceiveCapabilitiesAcceptXAttrProtocol");
			DataStrings.stringIDs.Add(2998256021U, "ExceptionInvlidCharInProtocolName");
			DataStrings.stringIDs.Add(1284528402U, "TransientFailure");
			DataStrings.stringIDs.Add(2446644924U, "InvalidInputErrorMsg");
			DataStrings.stringIDs.Add(242132428U, "ErrorFileShareWitnessServerNameMustNotBeEmpty");
			DataStrings.stringIDs.Add(2360034689U, "HostnameTooLong");
			DataStrings.stringIDs.Add(118693033U, "InvalidKeyMappingVoiceMail");
			DataStrings.stringIDs.Add(2600270045U, "KindKeywordEmail");
			DataStrings.stringIDs.Add(3423705850U, "ConstraintViolationStringLengthIsEmpty");
			DataStrings.stringIDs.Add(1737591621U, "AnonymousUsers");
			DataStrings.stringIDs.Add(2996964666U, "ErrorQuotionMarkNotSupportedInKql");
			DataStrings.stringIDs.Add(2637923947U, "LatAmSpanish");
			DataStrings.stringIDs.Add(1831009676U, "SearchRecipientsCc");
			DataStrings.stringIDs.Add(1181135370U, "InvalidOperationCurrentProperty");
			DataStrings.stringIDs.Add(665138238U, "ErrorPoliciesUpgradeCustomFailures");
			DataStrings.stringIDs.Add(643679508U, "CustomScheduleEveryHalfHour");
			DataStrings.stringIDs.Add(4198186357U, "EmptyExchangeObjectVersion");
			DataStrings.stringIDs.Add(2760883904U, "CustomGreetingFilePatternDescription");
			DataStrings.stringIDs.Add(1485002803U, "Partitioned");
			DataStrings.stringIDs.Add(220124761U, "ClientAccessProtocolOA");
			DataStrings.stringIDs.Add(3095705478U, "PublicFolderPermissionRolePublishingAuthor");
			DataStrings.stringIDs.Add(2578042802U, "InvalidIPAddressInSmartHost");
			DataStrings.stringIDs.Add(2736312437U, "DeliveryTypeSmtpRelayToMailboxDeliveryGroup");
			DataStrings.stringIDs.Add(2889762178U, "UnknownEdition");
			DataStrings.stringIDs.Add(3765811860U, "InvalidFormatExchangeObjectVersion");
			DataStrings.stringIDs.Add(3427924322U, "ToInternet");
			DataStrings.stringIDs.Add(1226277855U, "KindKeywordTasks");
			DataStrings.stringIDs.Add(736960779U, "MarkedAsRetryDeliveryIfRejected");
			DataStrings.stringIDs.Add(4060482376U, "ConnectorTypeOnPremises");
			DataStrings.stringIDs.Add(2163423328U, "HeaderValue");
			DataStrings.stringIDs.Add(2117971051U, "SmtpReceiveCapabilitiesAcceptProxyFromProtocol");
			DataStrings.stringIDs.Add(1153159321U, "PoisonQueueNextHopDomain");
			DataStrings.stringIDs.Add(936162170U, "GroupWiseProxyAddressPrefixDisplayName");
			DataStrings.stringIDs.Add(3861972062U, "MsMailProxyAddressPrefixDisplayName");
			DataStrings.stringIDs.Add(2924600836U, "Exchange2007");
			DataStrings.stringIDs.Add(2909775076U, "HeaderName");
			DataStrings.stringIDs.Add(1178729042U, "RejectText");
			DataStrings.stringIDs.Add(2493635772U, "NameValidationSpaceAllowedPatternDescription");
			DataStrings.stringIDs.Add(2255949938U, "ConstraintViolationStringLengthCauseOutOfMemory");
			DataStrings.stringIDs.Add(3639763634U, "InvalidFormat");
			DataStrings.stringIDs.Add(729961244U, "CustomPeriod");
			DataStrings.stringIDs.Add(95015764U, "PublicFolderPermissionRolePublishingEditor");
			DataStrings.stringIDs.Add(1893156877U, "InvalidKeyMappingKey");
			DataStrings.stringIDs.Add(224331957U, "StringConversionDelegateNotSet");
			DataStrings.stringIDs.Add(2603796430U, "NumberingPlanPatternDescription");
			DataStrings.stringIDs.Add(716522036U, "MeetingFullDetails");
			DataStrings.stringIDs.Add(3427843085U, "ProtocolLoggingLevelNone");
			DataStrings.stringIDs.Add(2353628225U, "ErrorInputFormatError");
			DataStrings.stringIDs.Add(3390434404U, "Ascending");
			DataStrings.stringIDs.Add(3345411900U, "SmtpReceiveCapabilitiesAcceptXOriginalFromProtocol");
			DataStrings.stringIDs.Add(599002008U, "Exchange2003");
			DataStrings.stringIDs.Add(1604545240U, "WorkingHours");
			DataStrings.stringIDs.Add(2838855213U, "DeferReasonTransientAttributionFailure");
			DataStrings.stringIDs.Add(2536591407U, "FromEnterprise");
			DataStrings.stringIDs.Add(1206366831U, "TlsAuthLevelCertificateValidation");
			DataStrings.stringIDs.Add(4094875965U, "Friday");
			DataStrings.stringIDs.Add(4009888891U, "MeetingLimitedDetails");
			DataStrings.stringIDs.Add(61359385U, "KeyMappingInvalidArgument");
			DataStrings.stringIDs.Add(1186448673U, "LegacyDNPatternDescription");
			DataStrings.stringIDs.Add(2245804386U, "SmtpReceiveCapabilitiesAcceptXSysProbeProtocol");
			DataStrings.stringIDs.Add(2677919833U, "SentTime");
			DataStrings.stringIDs.Add(2511844601U, "SearchSender");
			DataStrings.stringIDs.Add(2397470300U, "ErrorCannotAddNullValue");
			DataStrings.stringIDs.Add(3811769309U, "ScheduleModeScheduledTimes");
			DataStrings.stringIDs.Add(391606028U, "ProtocolAppleTalk");
			DataStrings.stringIDs.Add(3390939332U, "DigitStringOrEmptyPatternDescription");
			DataStrings.stringIDs.Add(26915469U, "EnterpriseEdition");
			DataStrings.stringIDs.Add(2726993793U, "ConnectorSourceDefault");
			DataStrings.stringIDs.Add(3372601165U, "ExceptionVersionlessObject");
			DataStrings.stringIDs.Add(2815103562U, "AliasPatternDescription");
			DataStrings.stringIDs.Add(1026314696U, "ReceivedTime");
			DataStrings.stringIDs.Add(3479640092U, "ParameterNameEmptyException");
			DataStrings.stringIDs.Add(2043548597U, "RecipientStatusLocked");
			DataStrings.stringIDs.Add(1248401958U, "SmtpReceiveCapabilitiesAcceptProxyToProtocol");
			DataStrings.stringIDs.Add(1109398861U, "PermissionGroupsNone");
			DataStrings.stringIDs.Add(2137442040U, "ArgumentMustBeAscii");
			DataStrings.stringIDs.Add(1284326164U, "ExceptionNetworkProtocolDuplicate");
			DataStrings.stringIDs.Add(3511836031U, "RecipientStatusRetry");
			DataStrings.stringIDs.Add(1002496550U, "GroupExpansionRecipients");
			DataStrings.stringIDs.Add(1061876008U, "LegacyDNProxyAddressPrefixDisplayName");
			DataStrings.stringIDs.Add(781217110U, "HeaderPromotionModeMustCreate");
			DataStrings.stringIDs.Add(2082847001U, "ProtocolLoggingLevelVerbose");
			DataStrings.stringIDs.Add(1176489840U, "HeaderPromotionModeNoCreate");
			DataStrings.stringIDs.Add(462794175U, "EncryptionTypeSSL");
			DataStrings.stringIDs.Add(3512036720U, "KindKeywordDocs");
			DataStrings.stringIDs.Add(2589626668U, "Unreachable");
			DataStrings.stringIDs.Add(1884450703U, "CustomScheduleSundayAtMidnight");
			DataStrings.stringIDs.Add(4294726685U, "ExceptionUnknownUnit");
			DataStrings.stringIDs.Add(3410257022U, "SendNone");
			DataStrings.stringIDs.Add(1100730082U, "SubjectPrefix");
			DataStrings.stringIDs.Add(3791230818U, "DeliveryTypeSmtpRelayToServers");
			DataStrings.stringIDs.Add(1667689070U, "InvalidKeyMappingFindMeFirstNumberDuration");
			DataStrings.stringIDs.Add(2482088490U, "AirSyncProxyAddressPrefixDisplayName");
			DataStrings.stringIDs.Add(1474747046U, "CoexistenceEdition");
			DataStrings.stringIDs.Add(2175102537U, "UnsearchableItemsAdded");
			DataStrings.stringIDs.Add(2846565657U, "InvalidKeyMappingFormat");
			DataStrings.stringIDs.Add(4125301959U, "ClientAccessProtocolPOP3");
			DataStrings.stringIDs.Add(4143129766U, "Word");
			DataStrings.stringIDs.Add(3614810764U, "AddressSpaceIsTooLong");
			DataStrings.stringIDs.Add(3699252422U, "AddressFamilyMismatch");
			DataStrings.stringIDs.Add(294615990U, "KindKeywordFaxes");
			DataStrings.stringIDs.Add(2440320060U, "ExceptionEmptyProxyAddress");
			DataStrings.stringIDs.Add(882442171U, "ExceptionObjectInvalid");
			DataStrings.stringIDs.Add(1177570172U, "ExceptionReadOnlyMultiValuedProperty");
			DataStrings.stringIDs.Add(2820941203U, "Tuesday");
			DataStrings.stringIDs.Add(2714058314U, "DeferReasonADTransientFailureDuringResolve");
			DataStrings.stringIDs.Add(3770991413U, "DeliveryTypeNonSmtpGatewayDelivery");
			DataStrings.stringIDs.Add(980447290U, "UseExchangeDSNs");
			DataStrings.stringIDs.Add(1316672322U, "KindKeywordContacts");
			DataStrings.stringIDs.Add(2112156755U, "FromLocal");
			DataStrings.stringIDs.Add(3991588639U, "ErrorPoliciesDowngradeDnsFailures");
			DataStrings.stringIDs.Add(466678310U, "TlsAuthLevelEncryptionOnly");
			DataStrings.stringIDs.Add(1073167130U, "Sunday");
			DataStrings.stringIDs.Add(1629702106U, "CustomScheduleDailyFrom9AMTo6PMAtWeekDays");
			DataStrings.stringIDs.Add(1777112844U, "Descending");
			DataStrings.stringIDs.Add(2350082752U, "DeliveryTypeUnreachable");
			DataStrings.stringIDs.Add(4067663994U, "KindKeywordPosts");
			DataStrings.stringIDs.Add(4234859176U, "ErrorServerGuidAndNameBothEmpty");
			DataStrings.stringIDs.Add(64170653U, "ExLengthOfVersionByteArrayError");
			DataStrings.stringIDs.Add(3444147793U, "SearchRecipientsTo");
			DataStrings.stringIDs.Add(918988081U, "DeliveryTypeMapiDelivery");
			DataStrings.stringIDs.Add(2906226218U, "SearchRecipientsBcc");
			DataStrings.stringIDs.Add(1947725858U, "EmptyNameInHostname");
			DataStrings.stringIDs.Add(1689084926U, "DisclaimerText");
			DataStrings.stringIDs.Add(1950405677U, "InvalidSmtpDomainWildcard");
			DataStrings.stringIDs.Add(3328112862U, "CustomScheduleDailyFrom2AMTo6AM");
			DataStrings.stringIDs.Add(3884278210U, "InvalidTimeOfDayFormat");
			DataStrings.stringIDs.Add(1054423051U, "Failed");
			DataStrings.stringIDs.Add(2176757305U, "CustomScheduleDailyFrom11PMTo6AM");
			DataStrings.stringIDs.Add(3124035205U, "ColonPrefix");
			DataStrings.stringIDs.Add(3544743625U, "ToGroupExpansionRecipients");
			DataStrings.stringIDs.Add(923847139U, "InvalidCallerIdItemFormat");
			DataStrings.stringIDs.Add(4060377141U, "PublicFolderPermissionRoleOwner");
			DataStrings.stringIDs.Add(2634610906U, "PermissionGroupsCustom");
			DataStrings.stringIDs.Add(3561826809U, "KindKeywordIm");
			DataStrings.stringIDs.Add(1977971622U, "GroupByTotal");
			DataStrings.stringIDs.Add(3781744254U, "BccGroupExpansionRecipients");
			DataStrings.stringIDs.Add(833225182U, "ExceptionInvlidNetworkAddressFormat");
			DataStrings.stringIDs.Add(2456836287U, "KindKeywordJournals");
			DataStrings.stringIDs.Add(1525449578U, "EmptyExchangeBuild");
			DataStrings.stringIDs.Add(2321790947U, "StandardEdition");
			DataStrings.stringIDs.Add(29693289U, "FormatExchangeBuildWrong");
			DataStrings.stringIDs.Add(79160252U, "ClientAccessProtocolPSWS");
			DataStrings.stringIDs.Add(3706983644U, "DeliveryTypeSmtpRelayToConnectorSourceServers");
			DataStrings.stringIDs.Add(3519073218U, "CustomScheduleSaturdayAtMidnight");
			DataStrings.stringIDs.Add(71385097U, "ExceptionNoValue");
			DataStrings.stringIDs.Add(2608795131U, "MailRecipientTypeDistributionGroup");
			DataStrings.stringIDs.Add(1561011830U, "ExceptionInvlidProtocolAddressFormat");
			DataStrings.stringIDs.Add(2364433662U, "CustomScheduleFridayAtMidnight");
			DataStrings.stringIDs.Add(255999871U, "DeliveryTypeShadowRedundancy");
			DataStrings.stringIDs.Add(3951803838U, "DeferReasonLoopDetected");
			DataStrings.stringIDs.Add(3183375374U, "SearchRecipients");
			DataStrings.stringIDs.Add(2862582797U, "CalendarSharingFreeBusySimple");
			DataStrings.stringIDs.Add(3422989433U, "PublicFolderPermissionRoleReviewer");
			DataStrings.stringIDs.Add(2267899661U, "QueueStatusActive");
			DataStrings.stringIDs.Add(2387319017U, "TlsAuthLevelCertificateExpiryCheck");
			DataStrings.stringIDs.Add(3030346869U, "InvalidAddressSpaceAddress");
			DataStrings.stringIDs.Add(1501056036U, "FromPartner");
			DataStrings.stringIDs.Add(3502523774U, "AllDays");
			DataStrings.stringIDs.Add(2099540954U, "DeferReasonTargetSiteInboundMailDisabled");
			DataStrings.stringIDs.Add(2760297639U, "ExceptionFormatNotSupported");
			DataStrings.stringIDs.Add(2279665409U, "DeliveryTypeDeliveryAgent");
			DataStrings.stringIDs.Add(1531043460U, "EstimatedItems");
			DataStrings.stringIDs.Add(1261683271U, "CmdletParameterEmptyValidationException");
			DataStrings.stringIDs.Add(66601190U, "MessageStatusPendingRemove");
			DataStrings.stringIDs.Add(3129286587U, "QueueStatusConnecting");
			DataStrings.stringIDs.Add(1108839058U, "DeliveryTypeSmtpRelayToRemoteAdSite");
			DataStrings.stringIDs.Add(3478111469U, "Saturday");
			DataStrings.stringIDs.Add(4135023588U, "ToEnterprise");
			DataStrings.stringIDs.Add(339456021U, "InvalidHolidayScheduleFormat");
			DataStrings.stringIDs.Add(1915155164U, "InvalidTimeOfDayFormatWorkingHours");
			DataStrings.stringIDs.Add(3119708320U, "CustomScheduleDaily11PM");
			DataStrings.stringIDs.Add(1761145356U, "QueueStatusSuspended");
			DataStrings.stringIDs.Add(999227985U, "SubmissionQueueNextHopDomain");
			DataStrings.stringIDs.Add(3153135486U, "ErrorNotSupportedForChangesOnlyCopy");
			DataStrings.stringIDs.Add(4098793892U, "CcGroupExpansionRecipients");
			DataStrings.stringIDs.Add(3039999898U, "ProxyAddressPrefixTooLong");
			DataStrings.stringIDs.Add(1660006804U, "Pattern");
			DataStrings.stringIDs.Add(1029030096U, "DeliveryTypeSmartHostConnectorDelivery");
			DataStrings.stringIDs.Add(2368086636U, "KindKeywordVoiceMail");
			DataStrings.stringIDs.Add(570747971U, "RoutingNoRouteToMta");
			DataStrings.stringIDs.Add(2517721976U, "ClientAccessProtocolEWS");
			DataStrings.stringIDs.Add(810977739U, "MailRecipientTypeExternal");
			DataStrings.stringIDs.Add(749395574U, "DeliveryTypeSmtpRelayWithinAdSite");
			DataStrings.stringIDs.Add(1800544803U, "InvalidDialGroupEntryCsvFormat");
			DataStrings.stringIDs.Add(607066203U, "KindKeywordMeetings");
			DataStrings.stringIDs.Add(2682624686U, "CustomScheduleEveryFourHours");
			DataStrings.stringIDs.Add(3134372274U, "MailRecipientTypeUnknown");
			DataStrings.stringIDs.Add(894196715U, "AmbiguousRecipient");
			DataStrings.stringIDs.Add(182065869U, "SmtpReceiveCapabilitiesAllowConsumerMail");
			DataStrings.stringIDs.Add(1601563988U, "InvalidFlagValue");
			DataStrings.stringIDs.Add(1435915854U, "TlsAuthLevelDomainValidation");
			DataStrings.stringIDs.Add(3942979931U, "ClientAccessBasicAuthentication");
			DataStrings.stringIDs.Add(3828585651U, "InvalidTimeOfDayFormatCustomWorkingHours");
			DataStrings.stringIDs.Add(1720677880U, "AttachmentContent");
			DataStrings.stringIDs.Add(1152451692U, "InvalidKeyMappingTransferToGalContact");
			DataStrings.stringIDs.Add(2941311901U, "ProtocolSpx");
			DataStrings.stringIDs.Add(4237384485U, "ErrorPoliciesDowngradeCustomFailures");
			DataStrings.stringIDs.Add(4240385111U, "ErrorCostOutOfRange");
			DataStrings.stringIDs.Add(1140124266U, "ExceptionNegativeUnit");
			DataStrings.stringIDs.Add(982561113U, "DeliveryTypeSmtpDeliveryToMailbox");
			DataStrings.stringIDs.Add(2755629282U, "PropertyIsMandatory");
			DataStrings.stringIDs.Add(2085180697U, "RoutingNonBHExpansionServer");
			DataStrings.stringIDs.Add(838517570U, "ExceptionReadOnlyPropertyBag");
			DataStrings.stringIDs.Add(1594003981U, "UnreachableQueueNextHopDomain");
			DataStrings.stringIDs.Add(682709795U, "InvalidSmtpDomain");
			DataStrings.stringIDs.Add(416319699U, "InvalidDialledNumberFormatC");
			DataStrings.stringIDs.Add(3338773880U, "DeliveryTypeDnsConnectorDelivery");
			DataStrings.stringIDs.Add(2544690258U, "DigitStringPatternDescription");
			DataStrings.stringIDs.Add(3001525213U, "CustomScheduleDailyFrom9AMTo5PMAtWeekDays");
			DataStrings.stringIDs.Add(4197662657U, "InvalidKeyMappingContext");
			DataStrings.stringIDs.Add(3826988642U, "CustomScheduleDaily1AM");
			DataStrings.stringIDs.Add(1512272061U, "FromInternet");
			DataStrings.stringIDs.Add(2854645212U, "TotalCopiedItems");
			DataStrings.stringIDs.Add(4083806514U, "ClientAccessProtocolRPS");
			DataStrings.stringIDs.Add(3406691936U, "ClientAccessNonBasicAuthentication");
			DataStrings.stringIDs.Add(3877102044U, "RoutingNoMdb");
			DataStrings.stringIDs.Add(443721013U, "PublicFolderPermissionRoleEditor");
			DataStrings.stringIDs.Add(3090942252U, "MailRecipientTypeMailbox");
			DataStrings.stringIDs.Add(460086660U, "CopyErrors");
			DataStrings.stringIDs.Add(2182454218U, "SnapinNameTooShort");
			DataStrings.stringIDs.Add(4028819673U, "TextBody");
			DataStrings.stringIDs.Add(1277617174U, "DeliveryTypeSmtpRelayToTiRg");
			DataStrings.stringIDs.Add(2058499689U, "ConstraintViolationNoLeadingOrTrailingWhitespace");
			DataStrings.stringIDs.Add(616309426U, "CalendarSharingFreeBusyDetail");
			DataStrings.stringIDs.Add(4177834631U, "ItemClass");
			DataStrings.stringIDs.Add(38623232U, "InvalidNotationFormat");
			DataStrings.stringIDs.Add(2067382811U, "ClientAccessProtocolOAB");
			DataStrings.stringIDs.Add(2956076415U, "InvalidCallerIdItemTypePhoneNumber");
			DataStrings.stringIDs.Add(3803801081U, "ExceptionDurationOverflow");
			DataStrings.stringIDs.Add(4064690660U, "ExchangeLegacyServers");
			DataStrings.stringIDs.Add(1367191786U, "Down");
			DataStrings.stringIDs.Add(416319697U, "InvalidDialledNumberFormatA");
			DataStrings.stringIDs.Add(3211494971U, "Misconfigured");
			DataStrings.stringIDs.Add(974174060U, "ProtocolTcpIP");
			DataStrings.stringIDs.Add(3287485561U, "RoleEntryStringMustBeCommaSeparated");
			DataStrings.stringIDs.Add(3779112048U, "SmtpReceiveCapabilitiesAcceptOorgProtocol");
			DataStrings.stringIDs.Add(4100738364U, "WeekendDays");
			DataStrings.stringIDs.Add(2509290958U, "ProtocolNetBios");
			DataStrings.stringIDs.Add(1022093144U, "CustomScheduleDailyFrom8AMTo12PMAnd1PMTo5PMAtWeekDays");
			DataStrings.stringIDs.Add(1817337337U, "ConfigurationSettingsAppSettingsError");
			DataStrings.stringIDs.Add(1261519073U, "InvalidKeyMappingTransferToNumber");
			DataStrings.stringIDs.Add(452901710U, "CustomScheduleDaily5AM");
			DataStrings.stringIDs.Add(871198498U, "KindKeywordRssFeeds");
			DataStrings.stringIDs.Add(1937548848U, "MessageStatusSuspended");
			DataStrings.stringIDs.Add(462978851U, "ShadowMessagePreferenceLocalOnly");
			DataStrings.stringIDs.Add(2047193656U, "Unavailable");
			DataStrings.stringIDs.Add(1434991878U, "RejectStatusCode");
			DataStrings.stringIDs.Add(1760294240U, "Thursday");
			DataStrings.stringIDs.Add(1039830289U, "StartingAddressAndMaskAddressFamilyMismatch");
			DataStrings.stringIDs.Add(2802337392U, "InvalidCallerIdItemTypeDefaultContactsFolder");
			DataStrings.stringIDs.Add(3137483865U, "ErrorPoliciesDefault");
			DataStrings.stringIDs.Add(268882683U, "PublicFolderPermissionRoleContributor");
			DataStrings.stringIDs.Add(2587222631U, "Weekdays");
			DataStrings.stringIDs.Add(3048094658U, "SmtpReceiveCapabilitiesAllowSubmit");
			DataStrings.stringIDs.Add(3673950617U, "PropertyNotEmptyOrNull");
			DataStrings.stringIDs.Add(2788726202U, "MAPIBlockOutlookVersionsPatternDescription");
			DataStrings.stringIDs.Add(4263235384U, "BucketVersionPatternDescription");
			DataStrings.stringIDs.Add(1602600165U, "MessageStatusNone");
			DataStrings.stringIDs.Add(2795839773U, "PublicFolderPermissionRoleAuthor");
			DataStrings.stringIDs.Add(2292506358U, "DeliveryTypeUndefined");
			DataStrings.stringIDs.Add(1405683073U, "SmtpReceiveCapabilitiesAcceptCrossForestMail");
			DataStrings.stringIDs.Add(2397369159U, "ConnectorSourceMigrated");
			DataStrings.stringIDs.Add(2934715437U, "RoutingNoMatchingConnector");
			DataStrings.stringIDs.Add(548875607U, "QueueStatusNone");
			DataStrings.stringIDs.Add(2067383266U, "ClientAccessProtocolEAC");
			DataStrings.stringIDs.Add(2929555192U, "EncryptionTypeTLS");
			DataStrings.stringIDs.Add(3496963749U, "RoleEntryNameTooShort");
			DataStrings.stringIDs.Add(3076383364U, "DuplicatesRemoved");
			DataStrings.stringIDs.Add(681220170U, "MessageStatusPendingSuspend");
			DataStrings.stringIDs.Add(3785867729U, "SmtpReceiveCapabilitiesAcceptOrgHeaders");
			DataStrings.stringIDs.Add(2114081924U, "CustomScheduleDailyAtMidnight");
			DataStrings.stringIDs.Add(412393748U, "RecipientStatusComplete");
			DataStrings.stringIDs.Add(2721491554U, "InvalidCallerIdItemTypeGALContactr");
			DataStrings.stringIDs.Add(1093243629U, "ExchangeServers");
			DataStrings.stringIDs.Add(1206036754U, "ScheduleModeNever");
			DataStrings.stringIDs.Add(1511350752U, "CustomScheduleDailyFrom11PMTo3AM");
			DataStrings.stringIDs.Add(416319702U, "InvalidDialledNumberFormatD");
			DataStrings.stringIDs.Add(1826938428U, "CustomScheduleDailyFrom1AMTo5AM");
			DataStrings.stringIDs.Add(344942096U, "ProxyAddressPrefixShouldNotBeAllSpace");
			DataStrings.stringIDs.Add(835983261U, "CustomScheduleDaily2AM");
			DataStrings.stringIDs.Add(1899327622U, "InvalidKeyMappingFindMeSecondNumber");
			DataStrings.stringIDs.Add(202819370U, "MessageStatusReady");
			DataStrings.stringIDs.Add(1069174646U, "GroupByDay");
			DataStrings.stringIDs.Add(3028676818U, "DeliveryTypeHeartbeat");
			DataStrings.stringIDs.Add(2470774060U, "FileExtensionOrSplatPatternDescription");
			DataStrings.stringIDs.Add(3713180243U, "MessageStatusActive");
			DataStrings.stringIDs.Add(3364213626U, "Monday");
			DataStrings.stringIDs.Add(2973542840U, "DeferReasonTransientAcceptedDomainsLoadFailure");
			DataStrings.stringIDs.Add(3065954297U, "ExceptionEventSourceNull");
			DataStrings.stringIDs.Add(1205942262U, "PublicFolderPermissionRoleNonEditingAuthor");
			DataStrings.stringIDs.Add(700746266U, "CustomScheduleEveryTwoHours");
			DataStrings.stringIDs.Add(2988328408U, "InvalidCallerIdItemTypePersonaContact");
			DataStrings.stringIDs.Add(1169266063U, "DeferReasonConfigUpdate");
			DataStrings.stringIDs.Add(654440350U, "ConstraintViolationValueIsNullOrEmpty");
			DataStrings.stringIDs.Add(1069535743U, "SubjectProperty");
			DataStrings.stringIDs.Add(1074863583U, "InvalidKeySelection_Zero");
			DataStrings.stringIDs.Add(2743519022U, "QueueStatusReady");
			DataStrings.stringIDs.Add(2677933048U, "ProtocolNamedPipes");
			DataStrings.stringIDs.Add(1839463316U, "ProtocolVnsSpp");
			DataStrings.stringIDs.Add(2247071192U, "KindKeywordNotes");
			DataStrings.stringIDs.Add(1707192398U, "ErrorCannotConvert");
			DataStrings.stringIDs.Add(416319700U, "InvalidDialledNumberFormatB");
			DataStrings.stringIDs.Add(1593650977U, "InvalidDialGroupEntryFormat");
			DataStrings.stringIDs.Add(3543498202U, "EapMustHaveOneEnabledPrimarySmtpAddressTemplate");
			DataStrings.stringIDs.Add(2682420737U, "FileExtensionPatternDescription");
			DataStrings.stringIDs.Add(2846264340U, "Unknown");
			DataStrings.stringIDs.Add(4060777377U, "QueueStatusRetry");
			DataStrings.stringIDs.Add(3452652986U, "Wednesday");
			DataStrings.stringIDs.Add(3417603459U, "InvalidKeyMappingFindMe");
			DataStrings.stringIDs.Add(417785204U, "DeferReasonReroutedByStoreDriver");
			DataStrings.stringIDs.Add(1326579539U, "ScheduleModeAlways");
			DataStrings.stringIDs.Add(2583398905U, "FileIsEmpty");
			DataStrings.stringIDs.Add(621231604U, "ExceptionSerializationDataAbsent");
			DataStrings.stringIDs.Add(3488668113U, "SmtpReceiveCapabilitiesAcceptOorgHeader");
			DataStrings.stringIDs.Add(881737792U, "DsnText");
			DataStrings.stringIDs.Add(269836784U, "CustomScheduleDailyFrom9AMTo12PMAnd1PMTo6PMAtWeekDays");
			DataStrings.stringIDs.Add(1301289463U, "StorageTransientFailureDuringContentConversion");
			DataStrings.stringIDs.Add(1500391415U, "MessageStatusRetry");
			DataStrings.stringIDs.Add(387961094U, "CustomProxyAddressPrefixDisplayName");
			DataStrings.stringIDs.Add(1088965676U, "CLIDPatternDescription");
			DataStrings.stringIDs.Add(2067383282U, "ClientAccessProtocolEAS");
			DataStrings.stringIDs.Add(304927915U, "ConstraintViolationInvalidWindowsLiveIDLocalPart");
			DataStrings.stringIDs.Add(1914858911U, "ExceptionParseInternalMessageId");
			DataStrings.stringIDs.Add(2476902678U, "RecipientStatusReady");
			DataStrings.stringIDs.Add(2221667633U, "ReceiveNone");
			DataStrings.stringIDs.Add(560731754U, "UserPrincipalNamePatternDescription");
			DataStrings.stringIDs.Add(2579867249U, "ToPartner");
			DataStrings.stringIDs.Add(824224038U, "DeliveryTypeSmtpRelayToDag");
			DataStrings.stringIDs.Add(1676404342U, "DeliveryTypeSmtpRelayWithinAdSiteToEdge");
			DataStrings.stringIDs.Add(1472458119U, "DeferReasonRecipientThreadLimitExceeded");
			DataStrings.stringIDs.Add(1543969273U, "Up");
			DataStrings.stringIDs.Add(4133244061U, "PreserveDSNBody");
			DataStrings.stringIDs.Add(2194722870U, "ElcScheduleInsufficientGap");
			DataStrings.stringIDs.Add(3501929169U, "ExceptionTypeNotEnhancedTimeSpanOrTimeSpan");
			DataStrings.stringIDs.Add(986397318U, "Recipients");
			DataStrings.stringIDs.Add(3443907091U, "CustomScheduleDaily4AM");
			DataStrings.stringIDs.Add(4277209464U, "ExceptionDefaultTypeMismatch");
			DataStrings.stringIDs.Add(2517721504U, "ClientAccessProtocolOWA");
			DataStrings.stringIDs.Add(2947517465U, "RecipientStatusNone");
			DataStrings.stringIDs.Add(553174585U, "StandardTrialEdition");
			DataStrings.stringIDs.Add(1980952396U, "ShadowMessagePreferenceRemoteOnly");
			DataStrings.stringIDs.Add(1065872813U, "DoNotConvert");
			DataStrings.stringIDs.Add(3119708351U, "CustomScheduleDaily10PM");
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00014D6F File Offset: 0x00012F6F
		public static LocalizedString NotesProxyAddressPrefixDisplayName
		{
			get
			{
				return new LocalizedString("NotesProxyAddressPrefixDisplayName", "Ex8F6208", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00014D8D File Offset: 0x00012F8D
		public static LocalizedString InvalidCallerIdItemTypePersonalContact
		{
			get
			{
				return new LocalizedString("InvalidCallerIdItemTypePersonalContact", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00014DAC File Offset: 0x00012FAC
		public static LocalizedString AddressSpaceCostOutOfRange(int minCost, int maxCost)
		{
			return new LocalizedString("AddressSpaceCostOutOfRange", "ExFB6E8C", false, true, DataStrings.ResourceManager, new object[]
			{
				minCost,
				maxCost
			});
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00014DEC File Offset: 0x00012FEC
		public static LocalizedString ExceptionWriteOnceProperty(string propertyName)
		{
			return new LocalizedString("ExceptionWriteOnceProperty", "Ex49D657", false, true, DataStrings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00014E1B File Offset: 0x0001301B
		public static LocalizedString ExceptionParseNotSupported
		{
			get
			{
				return new LocalizedString("ExceptionParseNotSupported", "Ex2465C4", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00014E39 File Offset: 0x00013039
		public static LocalizedString InvalidKeySelectionA
		{
			get
			{
				return new LocalizedString("InvalidKeySelectionA", "ExAD41C5", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00014E57 File Offset: 0x00013057
		public static LocalizedString ConnectorTypePartner
		{
			get
			{
				return new LocalizedString("ConnectorTypePartner", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00014E78 File Offset: 0x00013078
		public static LocalizedString SharingPolicyDomainInvalidAction(string value)
		{
			return new LocalizedString("SharingPolicyDomainInvalidAction", "Ex375C51", false, true, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00014EA7 File Offset: 0x000130A7
		public static LocalizedString SclValue
		{
			get
			{
				return new LocalizedString("SclValue", "ExAB6A24", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00014EC8 File Offset: 0x000130C8
		public static LocalizedString InvalidCIDRLengthIPv6(short cidrlength)
		{
			return new LocalizedString("InvalidCIDRLengthIPv6", "Ex119BC6", false, true, DataStrings.ResourceManager, new object[]
			{
				cidrlength
			});
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00014EFC File Offset: 0x000130FC
		public static LocalizedString MessageStatusLocked
		{
			get
			{
				return new LocalizedString("MessageStatusLocked", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00014F1C File Offset: 0x0001311C
		public static LocalizedString ErrorToStringNotImplemented(string sourceType)
		{
			return new LocalizedString("ErrorToStringNotImplemented", "Ex7FFFA1", false, true, DataStrings.ResourceManager, new object[]
			{
				sourceType
			});
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00014F4B File Offset: 0x0001314B
		public static LocalizedString DaysOfWeek_None
		{
			get
			{
				return new LocalizedString("DaysOfWeek_None", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00014F69 File Offset: 0x00013169
		public static LocalizedString InvalidAddressSpaceTypeNullOrEmpty
		{
			get
			{
				return new LocalizedString("InvalidAddressSpaceTypeNullOrEmpty", "Ex3CE1A7", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00014F87 File Offset: 0x00013187
		public static LocalizedString ProxyAddressShouldNotBeAllSpace
		{
			get
			{
				return new LocalizedString("ProxyAddressShouldNotBeAllSpace", "ExF7F3BF", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00014FA5 File Offset: 0x000131A5
		public static LocalizedString CalendarSharingFreeBusyReviewer
		{
			get
			{
				return new LocalizedString("CalendarSharingFreeBusyReviewer", "ExC08914", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00014FC3 File Offset: 0x000131C3
		public static LocalizedString ProtocolLocalRpc
		{
			get
			{
				return new LocalizedString("ProtocolLocalRpc", "Ex4A438E", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00014FE1 File Offset: 0x000131E1
		public static LocalizedString InsufficientSpace
		{
			get
			{
				return new LocalizedString("InsufficientSpace", "ExB951DE", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00014FFF File Offset: 0x000131FF
		public static LocalizedString ExchangeUsers
		{
			get
			{
				return new LocalizedString("ExchangeUsers", "ExA30F35", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0001501D File Offset: 0x0001321D
		public static LocalizedString RoutingIncompatibleDeliveryDomain
		{
			get
			{
				return new LocalizedString("RoutingIncompatibleDeliveryDomain", "Ex14B04A", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001503C File Offset: 0x0001323C
		public static LocalizedString CmdletFullNameFormatException(string name)
		{
			return new LocalizedString("CmdletFullNameFormatException", "ExE01E63", false, true, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001506B File Offset: 0x0001326B
		public static LocalizedString EnterpriseTrialEdition
		{
			get
			{
				return new LocalizedString("EnterpriseTrialEdition", "Ex444ED8", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001508C File Offset: 0x0001328C
		public static LocalizedString NumberFormatStringTooLong(string propertyName, int maxLength, int actualLength)
		{
			return new LocalizedString("NumberFormatStringTooLong", "", false, false, DataStrings.ResourceManager, new object[]
			{
				propertyName,
				maxLength,
				actualLength
			});
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x000150CD File Offset: 0x000132CD
		public static LocalizedString InvalidNumberFormatString
		{
			get
			{
				return new LocalizedString("InvalidNumberFormatString", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000150EC File Offset: 0x000132EC
		public static LocalizedString ExceptionInvalidFormat(string token, Type type, string invalidQuery, int position)
		{
			return new LocalizedString("ExceptionInvalidFormat", "Ex79A704", false, true, DataStrings.ResourceManager, new object[]
			{
				token,
				type,
				invalidQuery,
				position
			});
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001512C File Offset: 0x0001332C
		public static LocalizedString ErrorFileShareWitnessServerNameIsNotValidHostNameorFqdnWildcard(string computerName)
		{
			return new LocalizedString("ErrorFileShareWitnessServerNameIsNotValidHostNameorFqdnWildcard", "", false, false, DataStrings.ResourceManager, new object[]
			{
				computerName
			});
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0001515B File Offset: 0x0001335B
		public static LocalizedString Partners
		{
			get
			{
				return new LocalizedString("Partners", "Ex19525C", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001517C File Offset: 0x0001337C
		public static LocalizedString InvalidTypeArgumentException(string paramName, Type type, Type expectedType)
		{
			return new LocalizedString("InvalidTypeArgumentException", "Ex65174A", false, true, DataStrings.ResourceManager, new object[]
			{
				paramName,
				type,
				expectedType
			});
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x000151B3 File Offset: 0x000133B3
		public static LocalizedString CustomScheduleDaily12PM
		{
			get
			{
				return new LocalizedString("CustomScheduleDaily12PM", "ExAE8945", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000151D4 File Offset: 0x000133D4
		public static LocalizedString PropertyTypeMismatch(string actualType, string requiredType)
		{
			return new LocalizedString("PropertyTypeMismatch", "ExF9F1C0", false, true, DataStrings.ResourceManager, new object[]
			{
				actualType,
				requiredType
			});
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00015207 File Offset: 0x00013407
		public static LocalizedString CoexistenceTrialEdition
		{
			get
			{
				return new LocalizedString("CoexistenceTrialEdition", "Ex39589B", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00015225 File Offset: 0x00013425
		public static LocalizedString CustomExtensionInvalidArgument
		{
			get
			{
				return new LocalizedString("CustomExtensionInvalidArgument", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00015243 File Offset: 0x00013443
		public static LocalizedString NonWorkingHours
		{
			get
			{
				return new LocalizedString("NonWorkingHours", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00015261 File Offset: 0x00013461
		public static LocalizedString ClientAccessProtocolIMAP4
		{
			get
			{
				return new LocalizedString("ClientAccessProtocolIMAP4", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0001527F File Offset: 0x0001347F
		public static LocalizedString ShadowMessagePreferencePreferRemote
		{
			get
			{
				return new LocalizedString("ShadowMessagePreferencePreferRemote", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001529D File Offset: 0x0001349D
		public static LocalizedString CustomScheduleDailyFromMidnightTo4AM
		{
			get
			{
				return new LocalizedString("CustomScheduleDailyFromMidnightTo4AM", "Ex5C8BA3", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x000152BB File Offset: 0x000134BB
		public static LocalizedString CustomScheduleDailyFrom8AMTo5PMAtWeekDays
		{
			get
			{
				return new LocalizedString("CustomScheduleDailyFrom8AMTo5PMAtWeekDays", "ExA362EC", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000152DC File Offset: 0x000134DC
		public static LocalizedString ScriptRoleEntryNameInvalidException(string name)
		{
			return new LocalizedString("ScriptRoleEntryNameInvalidException", "ExB2981E", false, true, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001530C File Offset: 0x0001350C
		public static LocalizedString ExceptionParseNonFilterablePropertyErrorWithList(string propertyName, string knownProperties, string invalidQuery, int position)
		{
			return new LocalizedString("ExceptionParseNonFilterablePropertyErrorWithList", "", false, false, DataStrings.ResourceManager, new object[]
			{
				propertyName,
				knownProperties,
				invalidQuery,
				position
			});
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001534C File Offset: 0x0001354C
		public static LocalizedString ExceptionProtocolConnectionSettingsInvalidEncryptionType(string settings)
		{
			return new LocalizedString("ExceptionProtocolConnectionSettingsInvalidEncryptionType", "ExB4D5E2", false, true, DataStrings.ResourceManager, new object[]
			{
				settings
			});
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001537C File Offset: 0x0001357C
		public static LocalizedString ErrorCannotCopyFromDifferentType(Type thisType, Type otherType)
		{
			return new LocalizedString("ErrorCannotCopyFromDifferentType", "ExEB6451", false, true, DataStrings.ResourceManager, new object[]
			{
				thisType,
				otherType
			});
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000153B0 File Offset: 0x000135B0
		public static LocalizedString ConfigurationSettingsPropertyNotFound2(string name, string knownProperties)
		{
			return new LocalizedString("ConfigurationSettingsPropertyNotFound2", "", false, false, DataStrings.ResourceManager, new object[]
			{
				name,
				knownProperties
			});
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x000153E4 File Offset: 0x000135E4
		public static LocalizedString InvalidCIDRLengthIPv4(short cidrlength)
		{
			return new LocalizedString("InvalidCIDRLengthIPv4", "ExE65AB7", false, true, DataStrings.ResourceManager, new object[]
			{
				cidrlength
			});
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00015418 File Offset: 0x00013618
		public static LocalizedString EventLogText
		{
			get
			{
				return new LocalizedString("EventLogText", "Ex796CF5", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00015436 File Offset: 0x00013636
		public static LocalizedString ClientAccessAdfsAuthentication
		{
			get
			{
				return new LocalizedString("ClientAccessAdfsAuthentication", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00015454 File Offset: 0x00013654
		public static LocalizedString ErrorADFormatError
		{
			get
			{
				return new LocalizedString("ErrorADFormatError", "Ex5DCFC1", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00015472 File Offset: 0x00013672
		public static LocalizedString MeetingFullDetailsWithAttendees
		{
			get
			{
				return new LocalizedString("MeetingFullDetailsWithAttendees", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00015490 File Offset: 0x00013690
		public static LocalizedString CustomScheduleDaily3AM
		{
			get
			{
				return new LocalizedString("CustomScheduleDaily3AM", "ExDABD94", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x000154AE File Offset: 0x000136AE
		public static LocalizedString ExceptionCalculatedDependsOnCalculated
		{
			get
			{
				return new LocalizedString("ExceptionCalculatedDependsOnCalculated", "ExDBC5CA", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x000154CC File Offset: 0x000136CC
		public static LocalizedString RoutingNoRouteToMdb
		{
			get
			{
				return new LocalizedString("RoutingNoRouteToMdb", "Ex145AF6", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x000154EC File Offset: 0x000136EC
		public static LocalizedString ConstraintViolationValueIsNotInGivenStringArray(string arrayValues, string input)
		{
			return new LocalizedString("ConstraintViolationValueIsNotInGivenStringArray", "ExE53E39", false, true, DataStrings.ResourceManager, new object[]
			{
				arrayValues,
				input
			});
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001551F File Offset: 0x0001371F
		public static LocalizedString SmtpReceiveCapabilitiesAcceptProxyProtocol
		{
			get
			{
				return new LocalizedString("SmtpReceiveCapabilitiesAcceptProxyProtocol", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001553D File Offset: 0x0001373D
		public static LocalizedString DeferReasonADTransientFailureDuringContentConversion
		{
			get
			{
				return new LocalizedString("DeferReasonADTransientFailureDuringContentConversion", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001555C File Offset: 0x0001375C
		public static LocalizedString ExceptionRemoveSmtpPrimary(string primary)
		{
			return new LocalizedString("ExceptionRemoveSmtpPrimary", "Ex277A23", false, true, DataStrings.ResourceManager, new object[]
			{
				primary
			});
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001558B File Offset: 0x0001378B
		public static LocalizedString HeaderPromotionModeMayCreate
		{
			get
			{
				return new LocalizedString("HeaderPromotionModeMayCreate", "Ex16B729", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000155AC File Offset: 0x000137AC
		public static LocalizedString ErrorCannotConvertFromBinary(string resultType, string error)
		{
			return new LocalizedString("ErrorCannotConvertFromBinary", "Ex4C9ABB", false, true, DataStrings.ResourceManager, new object[]
			{
				resultType,
				error
			});
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x000155DF File Offset: 0x000137DF
		public static LocalizedString ConnectorSourceHybridWizard
		{
			get
			{
				return new LocalizedString("ConnectorSourceHybridWizard", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x000155FD File Offset: 0x000137FD
		public static LocalizedString DeferReasonAgent
		{
			get
			{
				return new LocalizedString("DeferReasonAgent", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001561B File Offset: 0x0001381B
		public static LocalizedString InvalidCustomMenuKeyMappingA
		{
			get
			{
				return new LocalizedString("InvalidCustomMenuKeyMappingA", "Ex26F508", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00015639 File Offset: 0x00013839
		public static LocalizedString InvalidResourcePropertySyntax
		{
			get
			{
				return new LocalizedString("InvalidResourcePropertySyntax", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00015657 File Offset: 0x00013857
		public static LocalizedString SmtpReceiveCapabilitiesAcceptCloudServicesMail
		{
			get
			{
				return new LocalizedString("SmtpReceiveCapabilitiesAcceptCloudServicesMail", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00015675 File Offset: 0x00013875
		public static LocalizedString CcMailProxyAddressPrefixDisplayName
		{
			get
			{
				return new LocalizedString("CcMailProxyAddressPrefixDisplayName", "Ex0C72EB", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00015693 File Offset: 0x00013893
		public static LocalizedString CustomScheduleEveryHour
		{
			get
			{
				return new LocalizedString("CustomScheduleEveryHour", "ExD1431D", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x000156B1 File Offset: 0x000138B1
		public static LocalizedString DeferReasonConcurrencyLimitInStoreDriver
		{
			get
			{
				return new LocalizedString("DeferReasonConcurrencyLimitInStoreDriver", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x000156CF File Offset: 0x000138CF
		public static LocalizedString GroupByMonth
		{
			get
			{
				return new LocalizedString("GroupByMonth", "ExCBBDED", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000156F0 File Offset: 0x000138F0
		public static LocalizedString InvalidSmtpX509Identifier(string s)
		{
			return new LocalizedString("InvalidSmtpX509Identifier", "", false, false, DataStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00015720 File Offset: 0x00013920
		public static LocalizedString DNWithBinaryFormatError(string dnwb)
		{
			return new LocalizedString("DNWithBinaryFormatError", "ExF65E77", false, true, DataStrings.ResourceManager, new object[]
			{
				dnwb
			});
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001574F File Offset: 0x0001394F
		public static LocalizedString ContactsSharing
		{
			get
			{
				return new LocalizedString("ContactsSharing", "Ex874F2A", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001576D File Offset: 0x0001396D
		public static LocalizedString ExceptionUnsupportedNetworkProtocol
		{
			get
			{
				return new LocalizedString("ExceptionUnsupportedNetworkProtocol", "ExD9EE37", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001578B File Offset: 0x0001398B
		public static LocalizedString SmtpReceiveCapabilitiesAcceptXAttrProtocol
		{
			get
			{
				return new LocalizedString("SmtpReceiveCapabilitiesAcceptXAttrProtocol", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x000157A9 File Offset: 0x000139A9
		public static LocalizedString ExceptionInvlidCharInProtocolName
		{
			get
			{
				return new LocalizedString("ExceptionInvlidCharInProtocolName", "Ex87B6A7", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000157C8 File Offset: 0x000139C8
		public static LocalizedString DialGroupNotSpecifiedOnDialPlan(string name)
		{
			return new LocalizedString("DialGroupNotSpecifiedOnDialPlan", "", false, false, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x000157F7 File Offset: 0x000139F7
		public static LocalizedString TransientFailure
		{
			get
			{
				return new LocalizedString("TransientFailure", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00015815 File Offset: 0x00013A15
		public static LocalizedString InvalidInputErrorMsg
		{
			get
			{
				return new LocalizedString("InvalidInputErrorMsg", "ExDEB0CE", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00015833 File Offset: 0x00013A33
		public static LocalizedString ErrorFileShareWitnessServerNameMustNotBeEmpty
		{
			get
			{
				return new LocalizedString("ErrorFileShareWitnessServerNameMustNotBeEmpty", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00015854 File Offset: 0x00013A54
		public static LocalizedString ConstraintViolationPathLength(string path, string error)
		{
			return new LocalizedString("ConstraintViolationPathLength", "Ex00FF3E", false, true, DataStrings.ResourceManager, new object[]
			{
				path,
				error
			});
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00015888 File Offset: 0x00013A88
		public static LocalizedString ConfigurationSettingsPropertyBadValue(string name, string value)
		{
			return new LocalizedString("ConfigurationSettingsPropertyBadValue", "", false, false, DataStrings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x000158BB File Offset: 0x00013ABB
		public static LocalizedString HostnameTooLong
		{
			get
			{
				return new LocalizedString("HostnameTooLong", "Ex528C69", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x000158D9 File Offset: 0x00013AD9
		public static LocalizedString InvalidKeyMappingVoiceMail
		{
			get
			{
				return new LocalizedString("InvalidKeyMappingVoiceMail", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x000158F7 File Offset: 0x00013AF7
		public static LocalizedString KindKeywordEmail
		{
			get
			{
				return new LocalizedString("KindKeywordEmail", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00015915 File Offset: 0x00013B15
		public static LocalizedString ConstraintViolationStringLengthIsEmpty
		{
			get
			{
				return new LocalizedString("ConstraintViolationStringLengthIsEmpty", "Ex916A5D", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00015933 File Offset: 0x00013B33
		public static LocalizedString AnonymousUsers
		{
			get
			{
				return new LocalizedString("AnonymousUsers", "Ex82B65C", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00015954 File Offset: 0x00013B54
		public static LocalizedString ThrottlingPolicyStateCorrupted(string value)
		{
			return new LocalizedString("ThrottlingPolicyStateCorrupted", "ExC8845E", false, true, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00015983 File Offset: 0x00013B83
		public static LocalizedString ErrorQuotionMarkNotSupportedInKql
		{
			get
			{
				return new LocalizedString("ErrorQuotionMarkNotSupportedInKql", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x000159A1 File Offset: 0x00013BA1
		public static LocalizedString LatAmSpanish
		{
			get
			{
				return new LocalizedString("LatAmSpanish", "ExEE5C93", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000159C0 File Offset: 0x00013BC0
		public static LocalizedString ConstraintViolationObjectIsBelowRange(string lowObject)
		{
			return new LocalizedString("ConstraintViolationObjectIsBelowRange", "ExD08CD7", false, true, DataStrings.ResourceManager, new object[]
			{
				lowObject
			});
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x000159EF File Offset: 0x00013BEF
		public static LocalizedString SearchRecipientsCc
		{
			get
			{
				return new LocalizedString("SearchRecipientsCc", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00015A0D File Offset: 0x00013C0D
		public static LocalizedString InvalidOperationCurrentProperty
		{
			get
			{
				return new LocalizedString("InvalidOperationCurrentProperty", "Ex4E3059", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00015A2B File Offset: 0x00013C2B
		public static LocalizedString ErrorPoliciesUpgradeCustomFailures
		{
			get
			{
				return new LocalizedString("ErrorPoliciesUpgradeCustomFailures", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00015A4C File Offset: 0x00013C4C
		public static LocalizedString ExceptionVariablesNotSupported(string invalidQuery, int position)
		{
			return new LocalizedString("ExceptionVariablesNotSupported", "Ex2D4D13", false, true, DataStrings.ResourceManager, new object[]
			{
				invalidQuery,
				position
			});
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00015A84 File Offset: 0x00013C84
		public static LocalizedString CustomScheduleEveryHalfHour
		{
			get
			{
				return new LocalizedString("CustomScheduleEveryHalfHour", "Ex668840", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00015AA2 File Offset: 0x00013CA2
		public static LocalizedString EmptyExchangeObjectVersion
		{
			get
			{
				return new LocalizedString("EmptyExchangeObjectVersion", "ExFAC945", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00015AC0 File Offset: 0x00013CC0
		public static LocalizedString CustomGreetingFilePatternDescription
		{
			get
			{
				return new LocalizedString("CustomGreetingFilePatternDescription", "ExB92914", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00015ADE File Offset: 0x00013CDE
		public static LocalizedString Partitioned
		{
			get
			{
				return new LocalizedString("Partitioned", "ExA7EC3C", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00015AFC File Offset: 0x00013CFC
		public static LocalizedString ClientAccessProtocolOA
		{
			get
			{
				return new LocalizedString("ClientAccessProtocolOA", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00015B1A File Offset: 0x00013D1A
		public static LocalizedString PublicFolderPermissionRolePublishingAuthor
		{
			get
			{
				return new LocalizedString("PublicFolderPermissionRolePublishingAuthor", "Ex219C85", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00015B38 File Offset: 0x00013D38
		public static LocalizedString InvalidIPAddressInSmartHost
		{
			get
			{
				return new LocalizedString("InvalidIPAddressInSmartHost", "ExAF9E3B", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00015B56 File Offset: 0x00013D56
		public static LocalizedString DeliveryTypeSmtpRelayToMailboxDeliveryGroup
		{
			get
			{
				return new LocalizedString("DeliveryTypeSmtpRelayToMailboxDeliveryGroup", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00015B74 File Offset: 0x00013D74
		public static LocalizedString UnknownEdition
		{
			get
			{
				return new LocalizedString("UnknownEdition", "ExEDE19B", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00015B94 File Offset: 0x00013D94
		public static LocalizedString ConfigurationSettingsScopePropertyBadValue(string name, string value)
		{
			return new LocalizedString("ConfigurationSettingsScopePropertyBadValue", "", false, false, DataStrings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00015BC7 File Offset: 0x00013DC7
		public static LocalizedString InvalidFormatExchangeObjectVersion
		{
			get
			{
				return new LocalizedString("InvalidFormatExchangeObjectVersion", "ExF5FF7E", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00015BE8 File Offset: 0x00013DE8
		public static LocalizedString InvalidEumAddress(string address)
		{
			return new LocalizedString("InvalidEumAddress", "Ex8348ED", false, true, DataStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00015C18 File Offset: 0x00013E18
		public static LocalizedString ElcScheduleInvalidType(string actualType)
		{
			return new LocalizedString("ElcScheduleInvalidType", "Ex52A42B", false, true, DataStrings.ResourceManager, new object[]
			{
				actualType
			});
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00015C47 File Offset: 0x00013E47
		public static LocalizedString ToInternet
		{
			get
			{
				return new LocalizedString("ToInternet", "Ex5E345F", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00015C68 File Offset: 0x00013E68
		public static LocalizedString ExceptionUnsupportedTypeConversion(string desired)
		{
			return new LocalizedString("ExceptionUnsupportedTypeConversion", "ExC2AC8C", false, true, DataStrings.ResourceManager, new object[]
			{
				desired
			});
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00015C97 File Offset: 0x00013E97
		public static LocalizedString KindKeywordTasks
		{
			get
			{
				return new LocalizedString("KindKeywordTasks", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00015CB8 File Offset: 0x00013EB8
		public static LocalizedString InvalidSmtpAddress(string address)
		{
			return new LocalizedString("InvalidSmtpAddress", "ExD11FBD", false, true, DataStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00015CE7 File Offset: 0x00013EE7
		public static LocalizedString MarkedAsRetryDeliveryIfRejected
		{
			get
			{
				return new LocalizedString("MarkedAsRetryDeliveryIfRejected", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00015D05 File Offset: 0x00013F05
		public static LocalizedString ConnectorTypeOnPremises
		{
			get
			{
				return new LocalizedString("ConnectorTypeOnPremises", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00015D23 File Offset: 0x00013F23
		public static LocalizedString HeaderValue
		{
			get
			{
				return new LocalizedString("HeaderValue", "ExC88A06", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00015D41 File Offset: 0x00013F41
		public static LocalizedString SmtpReceiveCapabilitiesAcceptProxyFromProtocol
		{
			get
			{
				return new LocalizedString("SmtpReceiveCapabilitiesAcceptProxyFromProtocol", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00015D5F File Offset: 0x00013F5F
		public static LocalizedString PoisonQueueNextHopDomain
		{
			get
			{
				return new LocalizedString("PoisonQueueNextHopDomain", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00015D80 File Offset: 0x00013F80
		public static LocalizedString ErrorObjectVersionReadOnly(string name)
		{
			return new LocalizedString("ErrorObjectVersionReadOnly", "Ex2F3F64", false, true, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00015DB0 File Offset: 0x00013FB0
		public static LocalizedString ErrorFileShareWitnessServerNameIsNotValidHostNameorFqdn(string computerName)
		{
			return new LocalizedString("ErrorFileShareWitnessServerNameIsNotValidHostNameorFqdn", "", false, false, DataStrings.ResourceManager, new object[]
			{
				computerName
			});
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00015DDF File Offset: 0x00013FDF
		public static LocalizedString GroupWiseProxyAddressPrefixDisplayName
		{
			get
			{
				return new LocalizedString("GroupWiseProxyAddressPrefixDisplayName", "Ex9B0DBA", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00015DFD File Offset: 0x00013FFD
		public static LocalizedString MsMailProxyAddressPrefixDisplayName
		{
			get
			{
				return new LocalizedString("MsMailProxyAddressPrefixDisplayName", "Ex7936C6", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00015E1B File Offset: 0x0001401B
		public static LocalizedString Exchange2007
		{
			get
			{
				return new LocalizedString("Exchange2007", "ExD9D03B", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00015E3C File Offset: 0x0001403C
		public static LocalizedString ExceptionParseNonFilterablePropertyError(string propertyName, string invalidQuery, int position)
		{
			return new LocalizedString("ExceptionParseNonFilterablePropertyError", "ExDF50CE", false, true, DataStrings.ResourceManager, new object[]
			{
				propertyName,
				invalidQuery,
				position
			});
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00015E78 File Offset: 0x00014078
		public static LocalizedString HeaderName
		{
			get
			{
				return new LocalizedString("HeaderName", "ExE2AE7E", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00015E96 File Offset: 0x00014096
		public static LocalizedString RejectText
		{
			get
			{
				return new LocalizedString("RejectText", "Ex8C2FA7", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00015EB4 File Offset: 0x000140B4
		public static LocalizedString NameValidationSpaceAllowedPatternDescription
		{
			get
			{
				return new LocalizedString("NameValidationSpaceAllowedPatternDescription", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00015ED2 File Offset: 0x000140D2
		public static LocalizedString ConstraintViolationStringLengthCauseOutOfMemory
		{
			get
			{
				return new LocalizedString("ConstraintViolationStringLengthCauseOutOfMemory", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00015EF0 File Offset: 0x000140F0
		public static LocalizedString ExceptionUnsupportedDestinationType(string type)
		{
			return new LocalizedString("ExceptionUnsupportedDestinationType", "Ex8C29A6", false, true, DataStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00015F1F File Offset: 0x0001411F
		public static LocalizedString InvalidFormat
		{
			get
			{
				return new LocalizedString("InvalidFormat", "ExBD3CCB", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00015F3D File Offset: 0x0001413D
		public static LocalizedString CustomPeriod
		{
			get
			{
				return new LocalizedString("CustomPeriod", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00015F5B File Offset: 0x0001415B
		public static LocalizedString PublicFolderPermissionRolePublishingEditor
		{
			get
			{
				return new LocalizedString("PublicFolderPermissionRolePublishingEditor", "Ex2F1F29", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00015F7C File Offset: 0x0001417C
		public static LocalizedString ConstraintViolationNotValidEmailAddress(string emailAddress)
		{
			return new LocalizedString("ConstraintViolationNotValidEmailAddress", "ExBB250A", false, true, DataStrings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00015FAC File Offset: 0x000141AC
		public static LocalizedString ErrorInputSchedulerBuilder(int actual, int expected)
		{
			return new LocalizedString("ErrorInputSchedulerBuilder", "Ex4A115B", false, true, DataStrings.ResourceManager, new object[]
			{
				actual,
				expected
			});
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00015FEC File Offset: 0x000141EC
		public static LocalizedString ConstraintViolationInvalidUriScheme(Uri uri, string uriSchemes)
		{
			return new LocalizedString("ConstraintViolationInvalidUriScheme", "", false, false, DataStrings.ResourceManager, new object[]
			{
				uri,
				uriSchemes
			});
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00016020 File Offset: 0x00014220
		public static LocalizedString ExceptionCurrentIndexOutOfRange(string current, string minimum, string maximum)
		{
			return new LocalizedString("ExceptionCurrentIndexOutOfRange", "ExA12D5C", false, true, DataStrings.ResourceManager, new object[]
			{
				current,
				minimum,
				maximum
			});
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00016057 File Offset: 0x00014257
		public static LocalizedString InvalidKeyMappingKey
		{
			get
			{
				return new LocalizedString("InvalidKeyMappingKey", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00016075 File Offset: 0x00014275
		public static LocalizedString StringConversionDelegateNotSet
		{
			get
			{
				return new LocalizedString("StringConversionDelegateNotSet", "ExE84810", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00016093 File Offset: 0x00014293
		public static LocalizedString NumberingPlanPatternDescription
		{
			get
			{
				return new LocalizedString("NumberingPlanPatternDescription", "Ex722E57", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x000160B1 File Offset: 0x000142B1
		public static LocalizedString MeetingFullDetails
		{
			get
			{
				return new LocalizedString("MeetingFullDetails", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x000160CF File Offset: 0x000142CF
		public static LocalizedString ProtocolLoggingLevelNone
		{
			get
			{
				return new LocalizedString("ProtocolLoggingLevelNone", "Ex896283", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000160F0 File Offset: 0x000142F0
		public static LocalizedString IncludeExcludeConflict(string value)
		{
			return new LocalizedString("IncludeExcludeConflict", "", false, false, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0001611F File Offset: 0x0001431F
		public static LocalizedString ErrorInputFormatError
		{
			get
			{
				return new LocalizedString("ErrorInputFormatError", "Ex758C3C", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001613D File Offset: 0x0001433D
		public static LocalizedString Ascending
		{
			get
			{
				return new LocalizedString("Ascending", "Ex7640A8", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001615C File Offset: 0x0001435C
		public static LocalizedString ExceptionReadOnlyBecauseTooNew(ExchangeObjectVersion objectVersion, ExchangeObjectVersion currentVersion)
		{
			return new LocalizedString("ExceptionReadOnlyBecauseTooNew", "Ex77EADC", false, true, DataStrings.ResourceManager, new object[]
			{
				objectVersion,
				currentVersion
			});
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0001618F File Offset: 0x0001438F
		public static LocalizedString SmtpReceiveCapabilitiesAcceptXOriginalFromProtocol
		{
			get
			{
				return new LocalizedString("SmtpReceiveCapabilitiesAcceptXOriginalFromProtocol", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x000161AD File Offset: 0x000143AD
		public static LocalizedString Exchange2003
		{
			get
			{
				return new LocalizedString("Exchange2003", "Ex14D09F", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x000161CB File Offset: 0x000143CB
		public static LocalizedString WorkingHours
		{
			get
			{
				return new LocalizedString("WorkingHours", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x000161E9 File Offset: 0x000143E9
		public static LocalizedString DeferReasonTransientAttributionFailure
		{
			get
			{
				return new LocalizedString("DeferReasonTransientAttributionFailure", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00016207 File Offset: 0x00014407
		public static LocalizedString FromEnterprise
		{
			get
			{
				return new LocalizedString("FromEnterprise", "Ex3A11E3", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00016225 File Offset: 0x00014425
		public static LocalizedString TlsAuthLevelCertificateValidation
		{
			get
			{
				return new LocalizedString("TlsAuthLevelCertificateValidation", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00016244 File Offset: 0x00014444
		public static LocalizedString PropertyNotACollection(string actualType)
		{
			return new LocalizedString("PropertyNotACollection", "Ex0D1DEF", false, true, DataStrings.ResourceManager, new object[]
			{
				actualType
			});
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00016273 File Offset: 0x00014473
		public static LocalizedString Friday
		{
			get
			{
				return new LocalizedString("Friday", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00016291 File Offset: 0x00014491
		public static LocalizedString MeetingLimitedDetails
		{
			get
			{
				return new LocalizedString("MeetingLimitedDetails", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x000162AF File Offset: 0x000144AF
		public static LocalizedString KeyMappingInvalidArgument
		{
			get
			{
				return new LocalizedString("KeyMappingInvalidArgument", "Ex021240", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000162D0 File Offset: 0x000144D0
		public static LocalizedString ErrorFileShareWitnessServerNameIsLocalhost(string computerName)
		{
			return new LocalizedString("ErrorFileShareWitnessServerNameIsLocalhost", "", false, false, DataStrings.ResourceManager, new object[]
			{
				computerName
			});
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x000162FF File Offset: 0x000144FF
		public static LocalizedString LegacyDNPatternDescription
		{
			get
			{
				return new LocalizedString("LegacyDNPatternDescription", "ExA6B413", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001631D File Offset: 0x0001451D
		public static LocalizedString SmtpReceiveCapabilitiesAcceptXSysProbeProtocol
		{
			get
			{
				return new LocalizedString("SmtpReceiveCapabilitiesAcceptXSysProbeProtocol", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001633B File Offset: 0x0001453B
		public static LocalizedString SentTime
		{
			get
			{
				return new LocalizedString("SentTime", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001635C File Offset: 0x0001455C
		public static LocalizedString CollectiionWithTooManyItemsFormat(string value)
		{
			return new LocalizedString("CollectiionWithTooManyItemsFormat", "Ex4166C5", false, true, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001638B File Offset: 0x0001458B
		public static LocalizedString SearchSender
		{
			get
			{
				return new LocalizedString("SearchSender", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000163AC File Offset: 0x000145AC
		public static LocalizedString ConstraintViolationEnumValueNotDefined(string actualValue, string enumType)
		{
			return new LocalizedString("ConstraintViolationEnumValueNotDefined", "Ex37CD44", false, true, DataStrings.ResourceManager, new object[]
			{
				actualValue,
				enumType
			});
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000163E0 File Offset: 0x000145E0
		public static LocalizedString ExArgumentNullException(string paramName)
		{
			return new LocalizedString("ExArgumentNullException", "ExB256EB", false, true, DataStrings.ResourceManager, new object[]
			{
				paramName
			});
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00016410 File Offset: 0x00014610
		public static LocalizedString ExceptionProtocolConnectionSettingsInvalidFormat(string settings)
		{
			return new LocalizedString("ExceptionProtocolConnectionSettingsInvalidFormat", "ExDA3189", false, true, DataStrings.ResourceManager, new object[]
			{
				settings
			});
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00016440 File Offset: 0x00014640
		public static LocalizedString ErrorInvalidEnumValue(string values)
		{
			return new LocalizedString("ErrorInvalidEnumValue", "ExEFE4B2", false, true, DataStrings.ResourceManager, new object[]
			{
				values
			});
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00016470 File Offset: 0x00014670
		public static LocalizedString ExceptionComparisonNotSupported(string name, Type type, ComparisonOperator comparison)
		{
			return new LocalizedString("ExceptionComparisonNotSupported", "Ex7B21BF", false, true, DataStrings.ResourceManager, new object[]
			{
				name,
				type,
				comparison
			});
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x000164AC File Offset: 0x000146AC
		public static LocalizedString ErrorCannotAddNullValue
		{
			get
			{
				return new LocalizedString("ErrorCannotAddNullValue", "Ex4521B2", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000164CC File Offset: 0x000146CC
		public static LocalizedString InvalidDialGroupEntryElementLength(string val, string name, int len)
		{
			return new LocalizedString("InvalidDialGroupEntryElementLength", "", false, false, DataStrings.ResourceManager, new object[]
			{
				val,
				name,
				len
			});
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00016508 File Offset: 0x00014708
		public static LocalizedString ExceptionGeoCoordinatesWithInvalidLatitude(string geoCoordinates)
		{
			return new LocalizedString("ExceptionGeoCoordinatesWithInvalidLatitude", "", false, false, DataStrings.ResourceManager, new object[]
			{
				geoCoordinates
			});
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00016537 File Offset: 0x00014737
		public static LocalizedString ScheduleModeScheduledTimes
		{
			get
			{
				return new LocalizedString("ScheduleModeScheduledTimes", "ExE1314D", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00016555 File Offset: 0x00014755
		public static LocalizedString ProtocolAppleTalk
		{
			get
			{
				return new LocalizedString("ProtocolAppleTalk", "Ex18A87A", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00016573 File Offset: 0x00014773
		public static LocalizedString DigitStringOrEmptyPatternDescription
		{
			get
			{
				return new LocalizedString("DigitStringOrEmptyPatternDescription", "ExCDCC63", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00016591 File Offset: 0x00014791
		public static LocalizedString EnterpriseEdition
		{
			get
			{
				return new LocalizedString("EnterpriseEdition", "ExA73FFA", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000165B0 File Offset: 0x000147B0
		public static LocalizedString ExceptionInvalidLatitude(double lat)
		{
			return new LocalizedString("ExceptionInvalidLatitude", "", false, false, DataStrings.ResourceManager, new object[]
			{
				lat
			});
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x000165E4 File Offset: 0x000147E4
		public static LocalizedString ConnectorSourceDefault
		{
			get
			{
				return new LocalizedString("ConnectorSourceDefault", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00016602 File Offset: 0x00014802
		public static LocalizedString ExceptionVersionlessObject
		{
			get
			{
				return new LocalizedString("ExceptionVersionlessObject", "ExA9E0E4", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00016620 File Offset: 0x00014820
		public static LocalizedString AliasPatternDescription
		{
			get
			{
				return new LocalizedString("AliasPatternDescription", "Ex57B176", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0001663E File Offset: 0x0001483E
		public static LocalizedString ReceivedTime
		{
			get
			{
				return new LocalizedString("ReceivedTime", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001665C File Offset: 0x0001485C
		public static LocalizedString ParameterNameEmptyException
		{
			get
			{
				return new LocalizedString("ParameterNameEmptyException", "ExC865C2", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0001667A File Offset: 0x0001487A
		public static LocalizedString RecipientStatusLocked
		{
			get
			{
				return new LocalizedString("RecipientStatusLocked", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00016698 File Offset: 0x00014898
		public static LocalizedString SmtpReceiveCapabilitiesAcceptProxyToProtocol
		{
			get
			{
				return new LocalizedString("SmtpReceiveCapabilitiesAcceptProxyToProtocol", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x000166B6 File Offset: 0x000148B6
		public static LocalizedString PermissionGroupsNone
		{
			get
			{
				return new LocalizedString("PermissionGroupsNone", "Ex25D8F9", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000166D4 File Offset: 0x000148D4
		public static LocalizedString InvalidOrganizationSummaryEntryFormat(string s)
		{
			return new LocalizedString("InvalidOrganizationSummaryEntryFormat", "Ex74AC48", false, true, DataStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00016704 File Offset: 0x00014904
		public static LocalizedString InvalidDateFormat(string date, string fmt1)
		{
			return new LocalizedString("InvalidDateFormat", "", false, false, DataStrings.ResourceManager, new object[]
			{
				date,
				fmt1
			});
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00016737 File Offset: 0x00014937
		public static LocalizedString ArgumentMustBeAscii
		{
			get
			{
				return new LocalizedString("ArgumentMustBeAscii", "Ex3E1102", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00016755 File Offset: 0x00014955
		public static LocalizedString ExceptionNetworkProtocolDuplicate
		{
			get
			{
				return new LocalizedString("ExceptionNetworkProtocolDuplicate", "ExB269FF", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00016773 File Offset: 0x00014973
		public static LocalizedString RecipientStatusRetry
		{
			get
			{
				return new LocalizedString("RecipientStatusRetry", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00016794 File Offset: 0x00014994
		public static LocalizedString InvalidEumAddressTemplateFormat(string template)
		{
			return new LocalizedString("InvalidEumAddressTemplateFormat", "ExD4A618", false, true, DataStrings.ResourceManager, new object[]
			{
				template
			});
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x000167C3 File Offset: 0x000149C3
		public static LocalizedString GroupExpansionRecipients
		{
			get
			{
				return new LocalizedString("GroupExpansionRecipients", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x000167E1 File Offset: 0x000149E1
		public static LocalizedString LegacyDNProxyAddressPrefixDisplayName
		{
			get
			{
				return new LocalizedString("LegacyDNProxyAddressPrefixDisplayName", "ExCF27DD", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x000167FF File Offset: 0x000149FF
		public static LocalizedString HeaderPromotionModeMustCreate
		{
			get
			{
				return new LocalizedString("HeaderPromotionModeMustCreate", "ExE35B1A", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0001681D File Offset: 0x00014A1D
		public static LocalizedString ProtocolLoggingLevelVerbose
		{
			get
			{
				return new LocalizedString("ProtocolLoggingLevelVerbose", "ExD3B628", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0001683B File Offset: 0x00014A3B
		public static LocalizedString HeaderPromotionModeNoCreate
		{
			get
			{
				return new LocalizedString("HeaderPromotionModeNoCreate", "ExBCF9A1", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00016859 File Offset: 0x00014A59
		public static LocalizedString EncryptionTypeSSL
		{
			get
			{
				return new LocalizedString("EncryptionTypeSSL", "Ex96F5EC", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00016878 File Offset: 0x00014A78
		public static LocalizedString ExceptionProtocolConnectionSettingsInvalidPort(string settings, int min, int max)
		{
			return new LocalizedString("ExceptionProtocolConnectionSettingsInvalidPort", "ExC94EB8", false, true, DataStrings.ResourceManager, new object[]
			{
				settings,
				min,
				max
			});
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000168BC File Offset: 0x00014ABC
		public static LocalizedString ConstraintViolationObjectIsBeyondRange(string highObject)
		{
			return new LocalizedString("ConstraintViolationObjectIsBeyondRange", "ExA7FF86", false, true, DataStrings.ResourceManager, new object[]
			{
				highObject
			});
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x000168EB File Offset: 0x00014AEB
		public static LocalizedString KindKeywordDocs
		{
			get
			{
				return new LocalizedString("KindKeywordDocs", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00016909 File Offset: 0x00014B09
		public static LocalizedString Unreachable
		{
			get
			{
				return new LocalizedString("Unreachable", "Ex85C2BE", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00016927 File Offset: 0x00014B27
		public static LocalizedString CustomScheduleSundayAtMidnight
		{
			get
			{
				return new LocalizedString("CustomScheduleSundayAtMidnight", "Ex5DEC9D", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00016948 File Offset: 0x00014B48
		public static LocalizedString ExceptionGeoCoordinatesWithInvalidLongitude(string geoCoordinates)
		{
			return new LocalizedString("ExceptionGeoCoordinatesWithInvalidLongitude", "", false, false, DataStrings.ResourceManager, new object[]
			{
				geoCoordinates
			});
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00016978 File Offset: 0x00014B78
		public static LocalizedString ExceptionInvalidOperation(string op, string typeName)
		{
			return new LocalizedString("ExceptionInvalidOperation", "ExA72668", false, true, DataStrings.ResourceManager, new object[]
			{
				op,
				typeName
			});
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x000169AB File Offset: 0x00014BAB
		public static LocalizedString ExceptionUnknownUnit
		{
			get
			{
				return new LocalizedString("ExceptionUnknownUnit", "ExFDAF41", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000169CC File Offset: 0x00014BCC
		public static LocalizedString ErrorOperationNotSupported(string originalType, string resultType)
		{
			return new LocalizedString("ErrorOperationNotSupported", "Ex30D899", false, true, DataStrings.ResourceManager, new object[]
			{
				originalType,
				resultType
			});
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00016A00 File Offset: 0x00014C00
		public static LocalizedString ErrorIncorrectLiveIdFormat(string netID)
		{
			return new LocalizedString("ErrorIncorrectLiveIdFormat", "Ex7138C9", false, true, DataStrings.ResourceManager, new object[]
			{
				netID
			});
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00016A2F File Offset: 0x00014C2F
		public static LocalizedString SendNone
		{
			get
			{
				return new LocalizedString("SendNone", "Ex7A6E9F", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00016A50 File Offset: 0x00014C50
		public static LocalizedString UnknownColumns(string columns, IEnumerable<string> unknownColumns)
		{
			return new LocalizedString("UnknownColumns", "Ex510233", false, true, DataStrings.ResourceManager, new object[]
			{
				columns,
				unknownColumns
			});
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00016A83 File Offset: 0x00014C83
		public static LocalizedString SubjectPrefix
		{
			get
			{
				return new LocalizedString("SubjectPrefix", "Ex1B5E7F", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00016AA1 File Offset: 0x00014CA1
		public static LocalizedString DeliveryTypeSmtpRelayToServers
		{
			get
			{
				return new LocalizedString("DeliveryTypeSmtpRelayToServers", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00016ABF File Offset: 0x00014CBF
		public static LocalizedString InvalidKeyMappingFindMeFirstNumberDuration
		{
			get
			{
				return new LocalizedString("InvalidKeyMappingFindMeFirstNumberDuration", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00016AE0 File Offset: 0x00014CE0
		public static LocalizedString InvalidTypeArgumentExceptionMultipleExceptedTypes(string paramName, Type type, Type expectedType, Type expectedType2)
		{
			return new LocalizedString("InvalidTypeArgumentExceptionMultipleExceptedTypes", "Ex2BDBFD", false, true, DataStrings.ResourceManager, new object[]
			{
				paramName,
				type,
				expectedType,
				expectedType2
			});
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00016B1B File Offset: 0x00014D1B
		public static LocalizedString AirSyncProxyAddressPrefixDisplayName
		{
			get
			{
				return new LocalizedString("AirSyncProxyAddressPrefixDisplayName", "ExAF06B5", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00016B39 File Offset: 0x00014D39
		public static LocalizedString CoexistenceEdition
		{
			get
			{
				return new LocalizedString("CoexistenceEdition", "Ex6175A7", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00016B57 File Offset: 0x00014D57
		public static LocalizedString UnsearchableItemsAdded
		{
			get
			{
				return new LocalizedString("UnsearchableItemsAdded", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00016B75 File Offset: 0x00014D75
		public static LocalizedString InvalidKeyMappingFormat
		{
			get
			{
				return new LocalizedString("InvalidKeyMappingFormat", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00016B93 File Offset: 0x00014D93
		public static LocalizedString ClientAccessProtocolPOP3
		{
			get
			{
				return new LocalizedString("ClientAccessProtocolPOP3", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00016BB4 File Offset: 0x00014DB4
		public static LocalizedString ConstraintViolationIpV6NotAllowed(string ipAddress)
		{
			return new LocalizedString("ConstraintViolationIpV6NotAllowed", "", false, false, DataStrings.ResourceManager, new object[]
			{
				ipAddress
			});
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00016BE4 File Offset: 0x00014DE4
		public static LocalizedString ExceptionEventCategoryNotFound(string eventCategory)
		{
			return new LocalizedString("ExceptionEventCategoryNotFound", "Ex0F2690", false, true, DataStrings.ResourceManager, new object[]
			{
				eventCategory
			});
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00016C13 File Offset: 0x00014E13
		public static LocalizedString Word
		{
			get
			{
				return new LocalizedString("Word", "ExFDE75C", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00016C31 File Offset: 0x00014E31
		public static LocalizedString AddressSpaceIsTooLong
		{
			get
			{
				return new LocalizedString("AddressSpaceIsTooLong", "Ex259905", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00016C4F File Offset: 0x00014E4F
		public static LocalizedString AddressFamilyMismatch
		{
			get
			{
				return new LocalizedString("AddressFamilyMismatch", "Ex9025C4", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00016C6D File Offset: 0x00014E6D
		public static LocalizedString KindKeywordFaxes
		{
			get
			{
				return new LocalizedString("KindKeywordFaxes", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00016C8C File Offset: 0x00014E8C
		public static LocalizedString ErrorObjectSerialization(ObjectId id, Version currentVersion, Version objectVersion)
		{
			return new LocalizedString("ErrorObjectSerialization", "ExBA6ECA", false, true, DataStrings.ResourceManager, new object[]
			{
				id,
				currentVersion,
				objectVersion
			});
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00016CC3 File Offset: 0x00014EC3
		public static LocalizedString ExceptionEmptyProxyAddress
		{
			get
			{
				return new LocalizedString("ExceptionEmptyProxyAddress", "ExE01039", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00016CE1 File Offset: 0x00014EE1
		public static LocalizedString ExceptionObjectInvalid
		{
			get
			{
				return new LocalizedString("ExceptionObjectInvalid", "Ex39A256", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00016D00 File Offset: 0x00014F00
		public static LocalizedString ConstraintViolationInvalidUriKind(Uri uri, UriKind uriKind)
		{
			return new LocalizedString("ConstraintViolationInvalidUriKind", "Ex246112", false, true, DataStrings.ResourceManager, new object[]
			{
				uri,
				uriKind
			});
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00016D38 File Offset: 0x00014F38
		public static LocalizedString ExceptionReadOnlyMultiValuedProperty
		{
			get
			{
				return new LocalizedString("ExceptionReadOnlyMultiValuedProperty", "ExA3B4E0", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00016D58 File Offset: 0x00014F58
		public static LocalizedString ParameterNameInvalidCharException(string name)
		{
			return new LocalizedString("ParameterNameInvalidCharException", "Ex31250B", false, true, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00016D87 File Offset: 0x00014F87
		public static LocalizedString Tuesday
		{
			get
			{
				return new LocalizedString("Tuesday", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00016DA5 File Offset: 0x00014FA5
		public static LocalizedString DeferReasonADTransientFailureDuringResolve
		{
			get
			{
				return new LocalizedString("DeferReasonADTransientFailureDuringResolve", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00016DC3 File Offset: 0x00014FC3
		public static LocalizedString DeliveryTypeNonSmtpGatewayDelivery
		{
			get
			{
				return new LocalizedString("DeliveryTypeNonSmtpGatewayDelivery", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00016DE1 File Offset: 0x00014FE1
		public static LocalizedString UseExchangeDSNs
		{
			get
			{
				return new LocalizedString("UseExchangeDSNs", "ExA4287D", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00016E00 File Offset: 0x00015000
		public static LocalizedString ConfigurationSettingsScopePropertyNotFound(string name)
		{
			return new LocalizedString("ConfigurationSettingsScopePropertyNotFound", "", false, false, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00016E30 File Offset: 0x00015030
		public static LocalizedString ConstraintViolationSecurityDescriptorContainsInheritedACEs(string sddl)
		{
			return new LocalizedString("ConstraintViolationSecurityDescriptorContainsInheritedACEs", "", false, false, DataStrings.ResourceManager, new object[]
			{
				sddl
			});
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00016E5F File Offset: 0x0001505F
		public static LocalizedString KindKeywordContacts
		{
			get
			{
				return new LocalizedString("KindKeywordContacts", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x00016E7D File Offset: 0x0001507D
		public static LocalizedString FromLocal
		{
			get
			{
				return new LocalizedString("FromLocal", "Ex10A5E0", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00016E9B File Offset: 0x0001509B
		public static LocalizedString ErrorPoliciesDowngradeDnsFailures
		{
			get
			{
				return new LocalizedString("ErrorPoliciesDowngradeDnsFailures", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00016EBC File Offset: 0x000150BC
		public static LocalizedString ConstraintViolationInvalidIntRange(int min, int max, string range)
		{
			return new LocalizedString("ConstraintViolationInvalidIntRange", "", false, false, DataStrings.ResourceManager, new object[]
			{
				min,
				max,
				range
			});
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00016EFD File Offset: 0x000150FD
		public static LocalizedString TlsAuthLevelEncryptionOnly
		{
			get
			{
				return new LocalizedString("TlsAuthLevelEncryptionOnly", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00016F1C File Offset: 0x0001511C
		public static LocalizedString ConstraintViolationValueIsNotAllowed(string validValues, string input)
		{
			return new LocalizedString("ConstraintViolationValueIsNotAllowed", "ExC6B952", false, true, DataStrings.ResourceManager, new object[]
			{
				validValues,
				input
			});
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00016F50 File Offset: 0x00015150
		public static LocalizedString ConstraintViolationMalformedExtensionValuePair(string actualValue)
		{
			return new LocalizedString("ConstraintViolationMalformedExtensionValuePair", "Ex6FFF62", false, true, DataStrings.ResourceManager, new object[]
			{
				actualValue
			});
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00016F7F File Offset: 0x0001517F
		public static LocalizedString Sunday
		{
			get
			{
				return new LocalizedString("Sunday", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00016F9D File Offset: 0x0001519D
		public static LocalizedString CustomScheduleDailyFrom9AMTo6PMAtWeekDays
		{
			get
			{
				return new LocalizedString("CustomScheduleDailyFrom9AMTo6PMAtWeekDays", "Ex4BC927", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00016FBB File Offset: 0x000151BB
		public static LocalizedString Descending
		{
			get
			{
				return new LocalizedString("Descending", "ExE0EE79", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00016FD9 File Offset: 0x000151D9
		public static LocalizedString DeliveryTypeUnreachable
		{
			get
			{
				return new LocalizedString("DeliveryTypeUnreachable", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00016FF7 File Offset: 0x000151F7
		public static LocalizedString KindKeywordPosts
		{
			get
			{
				return new LocalizedString("KindKeywordPosts", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00017015 File Offset: 0x00015215
		public static LocalizedString ErrorServerGuidAndNameBothEmpty
		{
			get
			{
				return new LocalizedString("ErrorServerGuidAndNameBothEmpty", "ExCE3A3B", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00017034 File Offset: 0x00015234
		public static LocalizedString ElcScheduleInvalidIntervals(string actualInterval)
		{
			return new LocalizedString("ElcScheduleInvalidIntervals", "Ex8BF199", false, true, DataStrings.ResourceManager, new object[]
			{
				actualInterval
			});
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00017063 File Offset: 0x00015263
		public static LocalizedString ExLengthOfVersionByteArrayError
		{
			get
			{
				return new LocalizedString("ExLengthOfVersionByteArrayError", "Ex3FF47B", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00017081 File Offset: 0x00015281
		public static LocalizedString SearchRecipientsTo
		{
			get
			{
				return new LocalizedString("SearchRecipientsTo", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000170A0 File Offset: 0x000152A0
		public static LocalizedString ExceptionUsingInvalidAddress(string address, string error)
		{
			return new LocalizedString("ExceptionUsingInvalidAddress", "ExB883C4", false, true, DataStrings.ResourceManager, new object[]
			{
				address,
				error
			});
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000170D4 File Offset: 0x000152D4
		public static LocalizedString InvalidNumber(string value, string propertyname)
		{
			return new LocalizedString("InvalidNumber", "", false, false, DataStrings.ResourceManager, new object[]
			{
				value,
				propertyname
			});
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00017108 File Offset: 0x00015308
		public static LocalizedString ErrorCannotSaveBecauseTooNew(ExchangeObjectVersion objectVersion, ExchangeObjectVersion currentVersion)
		{
			return new LocalizedString("ErrorCannotSaveBecauseTooNew", "Ex755DF0", false, true, DataStrings.ResourceManager, new object[]
			{
				objectVersion,
				currentVersion
			});
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001713C File Offset: 0x0001533C
		public static LocalizedString ConfigurationSettingsScopePropertyFailedValidation(string name, string value)
		{
			return new LocalizedString("ConfigurationSettingsScopePropertyFailedValidation", "", false, false, DataStrings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001716F File Offset: 0x0001536F
		public static LocalizedString DeliveryTypeMapiDelivery
		{
			get
			{
				return new LocalizedString("DeliveryTypeMapiDelivery", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001718D File Offset: 0x0001538D
		public static LocalizedString SearchRecipientsBcc
		{
			get
			{
				return new LocalizedString("SearchRecipientsBcc", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x000171AB File Offset: 0x000153AB
		public static LocalizedString EmptyNameInHostname
		{
			get
			{
				return new LocalizedString("EmptyNameInHostname", "ExF963B0", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x000171C9 File Offset: 0x000153C9
		public static LocalizedString DisclaimerText
		{
			get
			{
				return new LocalizedString("DisclaimerText", "Ex4D1DEA", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000171E8 File Offset: 0x000153E8
		public static LocalizedString InvalidDomainInSmtpX509Identifier(string s)
		{
			return new LocalizedString("InvalidDomainInSmtpX509Identifier", "", false, false, DataStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00017217 File Offset: 0x00015417
		public static LocalizedString InvalidSmtpDomainWildcard
		{
			get
			{
				return new LocalizedString("InvalidSmtpDomainWildcard", "Ex5F07FC", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00017238 File Offset: 0x00015438
		public static LocalizedString InvalidAddressSpaceCostFormat(string s)
		{
			return new LocalizedString("InvalidAddressSpaceCostFormat", "ExB81FA0", false, true, DataStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00017267 File Offset: 0x00015467
		public static LocalizedString CustomScheduleDailyFrom2AMTo6AM
		{
			get
			{
				return new LocalizedString("CustomScheduleDailyFrom2AMTo6AM", "Ex3B0A96", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00017285 File Offset: 0x00015485
		public static LocalizedString InvalidTimeOfDayFormat
		{
			get
			{
				return new LocalizedString("InvalidTimeOfDayFormat", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x000172A3 File Offset: 0x000154A3
		public static LocalizedString Failed
		{
			get
			{
				return new LocalizedString("Failed", "ExAD0308", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x000172C1 File Offset: 0x000154C1
		public static LocalizedString CustomScheduleDailyFrom11PMTo6AM
		{
			get
			{
				return new LocalizedString("CustomScheduleDailyFrom11PMTo6AM", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000172E0 File Offset: 0x000154E0
		public static LocalizedString ServicePlanFeatureCheckFailed(string feature, string sku)
		{
			return new LocalizedString("ServicePlanFeatureCheckFailed", "Ex3EC25C", false, true, DataStrings.ResourceManager, new object[]
			{
				feature,
				sku
			});
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00017313 File Offset: 0x00015513
		public static LocalizedString ColonPrefix
		{
			get
			{
				return new LocalizedString("ColonPrefix", "Ex1209EF", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00017331 File Offset: 0x00015531
		public static LocalizedString ToGroupExpansionRecipients
		{
			get
			{
				return new LocalizedString("ToGroupExpansionRecipients", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001734F File Offset: 0x0001554F
		public static LocalizedString InvalidCallerIdItemFormat
		{
			get
			{
				return new LocalizedString("InvalidCallerIdItemFormat", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00017370 File Offset: 0x00015570
		public static LocalizedString InvalidSMTPAddressTemplateFormat(string template)
		{
			return new LocalizedString("InvalidSMTPAddressTemplateFormat", "ExA86A7F", false, true, DataStrings.ResourceManager, new object[]
			{
				template
			});
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0001739F File Offset: 0x0001559F
		public static LocalizedString PublicFolderPermissionRoleOwner
		{
			get
			{
				return new LocalizedString("PublicFolderPermissionRoleOwner", "ExE13D97", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x000173BD File Offset: 0x000155BD
		public static LocalizedString PermissionGroupsCustom
		{
			get
			{
				return new LocalizedString("PermissionGroupsCustom", "ExCF4BB4", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x000173DB File Offset: 0x000155DB
		public static LocalizedString KindKeywordIm
		{
			get
			{
				return new LocalizedString("KindKeywordIm", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x000173F9 File Offset: 0x000155F9
		public static LocalizedString GroupByTotal
		{
			get
			{
				return new LocalizedString("GroupByTotal", "Ex8E1724", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00017418 File Offset: 0x00015618
		public static LocalizedString InvalidOrganizationSummaryEntryValue(string value)
		{
			return new LocalizedString("InvalidOrganizationSummaryEntryValue", "Ex119611", false, true, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00017447 File Offset: 0x00015647
		public static LocalizedString BccGroupExpansionRecipients
		{
			get
			{
				return new LocalizedString("BccGroupExpansionRecipients", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00017465 File Offset: 0x00015665
		public static LocalizedString ExceptionInvlidNetworkAddressFormat
		{
			get
			{
				return new LocalizedString("ExceptionInvlidNetworkAddressFormat", "Ex6ABEF6", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00017483 File Offset: 0x00015683
		public static LocalizedString KindKeywordJournals
		{
			get
			{
				return new LocalizedString("KindKeywordJournals", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x000174A1 File Offset: 0x000156A1
		public static LocalizedString EmptyExchangeBuild
		{
			get
			{
				return new LocalizedString("EmptyExchangeBuild", "Ex68E6B0", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x000174BF File Offset: 0x000156BF
		public static LocalizedString StandardEdition
		{
			get
			{
				return new LocalizedString("StandardEdition", "Ex248BE8", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x000174DD File Offset: 0x000156DD
		public static LocalizedString FormatExchangeBuildWrong
		{
			get
			{
				return new LocalizedString("FormatExchangeBuildWrong", "ExD0EE61", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000174FC File Offset: 0x000156FC
		public static LocalizedString DataNotCloneable(string datatype)
		{
			return new LocalizedString("DataNotCloneable", "Ex66D056", false, true, DataStrings.ResourceManager, new object[]
			{
				datatype
			});
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0001752B File Offset: 0x0001572B
		public static LocalizedString ClientAccessProtocolPSWS
		{
			get
			{
				return new LocalizedString("ClientAccessProtocolPSWS", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00017549 File Offset: 0x00015749
		public static LocalizedString DeliveryTypeSmtpRelayToConnectorSourceServers
		{
			get
			{
				return new LocalizedString("DeliveryTypeSmtpRelayToConnectorSourceServers", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00017567 File Offset: 0x00015767
		public static LocalizedString CustomScheduleSaturdayAtMidnight
		{
			get
			{
				return new LocalizedString("CustomScheduleSaturdayAtMidnight", "ExCB17BE", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00017585 File Offset: 0x00015785
		public static LocalizedString ExceptionNoValue
		{
			get
			{
				return new LocalizedString("ExceptionNoValue", "ExD91A99", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x000175A3 File Offset: 0x000157A3
		public static LocalizedString MailRecipientTypeDistributionGroup
		{
			get
			{
				return new LocalizedString("MailRecipientTypeDistributionGroup", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x000175C1 File Offset: 0x000157C1
		public static LocalizedString ExceptionInvlidProtocolAddressFormat
		{
			get
			{
				return new LocalizedString("ExceptionInvlidProtocolAddressFormat", "ExBC5C32", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x000175DF File Offset: 0x000157DF
		public static LocalizedString CustomScheduleFridayAtMidnight
		{
			get
			{
				return new LocalizedString("CustomScheduleFridayAtMidnight", "ExC5DF82", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00017600 File Offset: 0x00015800
		public static LocalizedString ExceptionBitMaskNotSupported(string name, Type type)
		{
			return new LocalizedString("ExceptionBitMaskNotSupported", "Ex482DC2", false, true, DataStrings.ResourceManager, new object[]
			{
				name,
				type
			});
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00017634 File Offset: 0x00015834
		public static LocalizedString ConstraintViolationStringContainsInvalidCharacters2(string invalidCharacters, string input)
		{
			return new LocalizedString("ConstraintViolationStringContainsInvalidCharacters2", "Ex746C45", false, true, DataStrings.ResourceManager, new object[]
			{
				invalidCharacters,
				input
			});
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00017667 File Offset: 0x00015867
		public static LocalizedString DeliveryTypeShadowRedundancy
		{
			get
			{
				return new LocalizedString("DeliveryTypeShadowRedundancy", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00017685 File Offset: 0x00015885
		public static LocalizedString DeferReasonLoopDetected
		{
			get
			{
				return new LocalizedString("DeferReasonLoopDetected", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x000176A3 File Offset: 0x000158A3
		public static LocalizedString SearchRecipients
		{
			get
			{
				return new LocalizedString("SearchRecipients", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000176C4 File Offset: 0x000158C4
		public static LocalizedString InvalidIPAddressMask(string ipAddress)
		{
			return new LocalizedString("InvalidIPAddressMask", "Ex3854FD", false, true, DataStrings.ResourceManager, new object[]
			{
				ipAddress
			});
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000176F4 File Offset: 0x000158F4
		public static LocalizedString ConstraintViolationStringDoesNotMatchRegularExpression(string pattern, string input)
		{
			return new LocalizedString("ConstraintViolationStringDoesNotMatchRegularExpression", "ExD5AF6C", false, true, DataStrings.ResourceManager, new object[]
			{
				pattern,
				input
			});
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00017727 File Offset: 0x00015927
		public static LocalizedString CalendarSharingFreeBusySimple
		{
			get
			{
				return new LocalizedString("CalendarSharingFreeBusySimple", "ExC35F6E", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00017745 File Offset: 0x00015945
		public static LocalizedString PublicFolderPermissionRoleReviewer
		{
			get
			{
				return new LocalizedString("PublicFolderPermissionRoleReviewer", "Ex0A53E3", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00017764 File Offset: 0x00015964
		public static LocalizedString ScheduleDateInvalid(DateTime start, DateTime end)
		{
			return new LocalizedString("ScheduleDateInvalid", "", false, false, DataStrings.ResourceManager, new object[]
			{
				start,
				end
			});
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000177A1 File Offset: 0x000159A1
		public static LocalizedString QueueStatusActive
		{
			get
			{
				return new LocalizedString("QueueStatusActive", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000177C0 File Offset: 0x000159C0
		public static LocalizedString InvalidSmtpReceiveDomainCapabilities(string s)
		{
			return new LocalizedString("InvalidSmtpReceiveDomainCapabilities", "Ex020660", false, true, DataStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x000177F0 File Offset: 0x000159F0
		public static LocalizedString ExceptionCannotResolveOperation(string op, string type1, string type2)
		{
			return new LocalizedString("ExceptionCannotResolveOperation", "ExD39651", false, true, DataStrings.ResourceManager, new object[]
			{
				op,
				type1,
				type2
			});
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00017828 File Offset: 0x00015A28
		public static LocalizedString InvalidConnectedDomainFormat(string s)
		{
			return new LocalizedString("InvalidConnectedDomainFormat", "ExAA0D6C", false, true, DataStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00017857 File Offset: 0x00015A57
		public static LocalizedString TlsAuthLevelCertificateExpiryCheck
		{
			get
			{
				return new LocalizedString("TlsAuthLevelCertificateExpiryCheck", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00017875 File Offset: 0x00015A75
		public static LocalizedString InvalidAddressSpaceAddress
		{
			get
			{
				return new LocalizedString("InvalidAddressSpaceAddress", "ExAA6190", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00017894 File Offset: 0x00015A94
		public static LocalizedString InvalidOrganizationSummaryEntryKey(string key)
		{
			return new LocalizedString("InvalidOrganizationSummaryEntryKey", "Ex65A1FA", false, true, DataStrings.ResourceManager, new object[]
			{
				key
			});
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x000178C3 File Offset: 0x00015AC3
		public static LocalizedString FromPartner
		{
			get
			{
				return new LocalizedString("FromPartner", "ExB2FA65", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x000178E1 File Offset: 0x00015AE1
		public static LocalizedString AllDays
		{
			get
			{
				return new LocalizedString("AllDays", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00017900 File Offset: 0x00015B00
		public static LocalizedString ExceptionEndPointPortOutOfRange(int min, int max, int value)
		{
			return new LocalizedString("ExceptionEndPointPortOutOfRange", "Ex8DC954", false, true, DataStrings.ResourceManager, new object[]
			{
				min,
				max,
				value
			});
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00017946 File Offset: 0x00015B46
		public static LocalizedString DeferReasonTargetSiteInboundMailDisabled
		{
			get
			{
				return new LocalizedString("DeferReasonTargetSiteInboundMailDisabled", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00017964 File Offset: 0x00015B64
		public static LocalizedString ExceptionGeoCoordinatesWithInvalidAltitude(string geoCoordinates)
		{
			return new LocalizedString("ExceptionGeoCoordinatesWithInvalidAltitude", "", false, false, DataStrings.ResourceManager, new object[]
			{
				geoCoordinates
			});
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00017993 File Offset: 0x00015B93
		public static LocalizedString ExceptionFormatNotSupported
		{
			get
			{
				return new LocalizedString("ExceptionFormatNotSupported", "Ex96C3FF", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000179B4 File Offset: 0x00015BB4
		public static LocalizedString ConstraintViolationEnumValueNotAllowed(string actualValue)
		{
			return new LocalizedString("ConstraintViolationEnumValueNotAllowed", "ExEE6068", false, true, DataStrings.ResourceManager, new object[]
			{
				actualValue
			});
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000179E4 File Offset: 0x00015BE4
		public static LocalizedString SharingPolicyDomainInvalidDomain(string value)
		{
			return new LocalizedString("SharingPolicyDomainInvalidDomain", "Ex8F6F62", false, true, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00017A13 File Offset: 0x00015C13
		public static LocalizedString DeliveryTypeDeliveryAgent
		{
			get
			{
				return new LocalizedString("DeliveryTypeDeliveryAgent", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00017A31 File Offset: 0x00015C31
		public static LocalizedString EstimatedItems
		{
			get
			{
				return new LocalizedString("EstimatedItems", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00017A4F File Offset: 0x00015C4F
		public static LocalizedString CmdletParameterEmptyValidationException
		{
			get
			{
				return new LocalizedString("CmdletParameterEmptyValidationException", "Ex7F0EEC", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00017A6D File Offset: 0x00015C6D
		public static LocalizedString MessageStatusPendingRemove
		{
			get
			{
				return new LocalizedString("MessageStatusPendingRemove", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00017A8B File Offset: 0x00015C8B
		public static LocalizedString QueueStatusConnecting
		{
			get
			{
				return new LocalizedString("QueueStatusConnecting", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00017AAC File Offset: 0x00015CAC
		public static LocalizedString UnsupportServerEdition(string edition)
		{
			return new LocalizedString("UnsupportServerEdition", "Ex58CEE2", false, true, DataStrings.ResourceManager, new object[]
			{
				edition
			});
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00017ADB File Offset: 0x00015CDB
		public static LocalizedString DeliveryTypeSmtpRelayToRemoteAdSite
		{
			get
			{
				return new LocalizedString("DeliveryTypeSmtpRelayToRemoteAdSite", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00017AF9 File Offset: 0x00015CF9
		public static LocalizedString Saturday
		{
			get
			{
				return new LocalizedString("Saturday", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00017B17 File Offset: 0x00015D17
		public static LocalizedString ToEnterprise
		{
			get
			{
				return new LocalizedString("ToEnterprise", "Ex639CAE", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00017B35 File Offset: 0x00015D35
		public static LocalizedString InvalidHolidayScheduleFormat
		{
			get
			{
				return new LocalizedString("InvalidHolidayScheduleFormat", "Ex69C3D0", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00017B54 File Offset: 0x00015D54
		public static LocalizedString ConstraintViolationByteArrayLengthTooLong(int maxLength, int actualLength)
		{
			return new LocalizedString("ConstraintViolationByteArrayLengthTooLong", "Ex309B7D", false, true, DataStrings.ResourceManager, new object[]
			{
				maxLength,
				actualLength
			});
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00017B91 File Offset: 0x00015D91
		public static LocalizedString InvalidTimeOfDayFormatWorkingHours
		{
			get
			{
				return new LocalizedString("InvalidTimeOfDayFormatWorkingHours", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00017BAF File Offset: 0x00015DAF
		public static LocalizedString CustomScheduleDaily11PM
		{
			get
			{
				return new LocalizedString("CustomScheduleDaily11PM", "Ex37365F", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00017BD0 File Offset: 0x00015DD0
		public static LocalizedString InvalidCIDRLength(string cidrlength)
		{
			return new LocalizedString("InvalidCIDRLength", "Ex57742F", false, true, DataStrings.ResourceManager, new object[]
			{
				cidrlength
			});
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00017C00 File Offset: 0x00015E00
		public static LocalizedString ErrorFileShareWitnessServerNameCannotConvert(string computerName)
		{
			return new LocalizedString("ErrorFileShareWitnessServerNameCannotConvert", "", false, false, DataStrings.ResourceManager, new object[]
			{
				computerName
			});
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00017C30 File Offset: 0x00015E30
		public static LocalizedString ExceptionFormatNotCorrect(string value)
		{
			return new LocalizedString("ExceptionFormatNotCorrect", "Ex718F94", false, true, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00017C5F File Offset: 0x00015E5F
		public static LocalizedString QueueStatusSuspended
		{
			get
			{
				return new LocalizedString("QueueStatusSuspended", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00017C80 File Offset: 0x00015E80
		public static LocalizedString ConstraintViolationByteArrayLengthTooShort(int minLength, int actualLength)
		{
			return new LocalizedString("ConstraintViolationByteArrayLengthTooShort", "ExA88136", false, true, DataStrings.ResourceManager, new object[]
			{
				minLength,
				actualLength
			});
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00017CC0 File Offset: 0x00015EC0
		public static LocalizedString InvalidX400Domain(string domain)
		{
			return new LocalizedString("InvalidX400Domain", "Ex51AF8A", false, true, DataStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00017CF0 File Offset: 0x00015EF0
		public static LocalizedString InvalidDialGroupEntryElementFormat(string name)
		{
			return new LocalizedString("InvalidDialGroupEntryElementFormat", "", false, false, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00017D1F File Offset: 0x00015F1F
		public static LocalizedString SubmissionQueueNextHopDomain
		{
			get
			{
				return new LocalizedString("SubmissionQueueNextHopDomain", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00017D3D File Offset: 0x00015F3D
		public static LocalizedString ErrorNotSupportedForChangesOnlyCopy
		{
			get
			{
				return new LocalizedString("ErrorNotSupportedForChangesOnlyCopy", "Ex65E5CE", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x00017D5B File Offset: 0x00015F5B
		public static LocalizedString CcGroupExpansionRecipients
		{
			get
			{
				return new LocalizedString("CcGroupExpansionRecipients", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00017D79 File Offset: 0x00015F79
		public static LocalizedString ProxyAddressPrefixTooLong
		{
			get
			{
				return new LocalizedString("ProxyAddressPrefixTooLong", "ExBCCEFE", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00017D98 File Offset: 0x00015F98
		public static LocalizedString ExceptionPropertyTooNew(string name, ExchangeObjectVersion versionRequired, ExchangeObjectVersion objectVersion)
		{
			return new LocalizedString("ExceptionPropertyTooNew", "Ex51405F", false, true, DataStrings.ResourceManager, new object[]
			{
				name,
				versionRequired,
				objectVersion
			});
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00017DCF File Offset: 0x00015FCF
		public static LocalizedString Pattern
		{
			get
			{
				return new LocalizedString("Pattern", "ExE1853E", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00017DF0 File Offset: 0x00015FF0
		public static LocalizedString RequiredColumnMissing(string missingColumn)
		{
			return new LocalizedString("RequiredColumnMissing", "Ex45050E", false, true, DataStrings.ResourceManager, new object[]
			{
				missingColumn
			});
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00017E20 File Offset: 0x00016020
		public static LocalizedString ExceptionQueueIdentityCompare(string type)
		{
			return new LocalizedString("ExceptionQueueIdentityCompare", "", false, false, DataStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00017E50 File Offset: 0x00016050
		public static LocalizedString ErrorIncorrectWindowsLiveIdFormat(string id)
		{
			return new LocalizedString("ErrorIncorrectWindowsLiveIdFormat", "Ex5A2092", false, true, DataStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00017E80 File Offset: 0x00016080
		public static LocalizedString InvalidIPRange(string startAddress, string endAddress)
		{
			return new LocalizedString("InvalidIPRange", "ExE4B7F5", false, true, DataStrings.ResourceManager, new object[]
			{
				startAddress,
				endAddress
			});
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00017EB3 File Offset: 0x000160B3
		public static LocalizedString DeliveryTypeSmartHostConnectorDelivery
		{
			get
			{
				return new LocalizedString("DeliveryTypeSmartHostConnectorDelivery", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00017ED1 File Offset: 0x000160D1
		public static LocalizedString KindKeywordVoiceMail
		{
			get
			{
				return new LocalizedString("KindKeywordVoiceMail", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00017EEF File Offset: 0x000160EF
		public static LocalizedString RoutingNoRouteToMta
		{
			get
			{
				return new LocalizedString("RoutingNoRouteToMta", "Ex6528F8", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00017F0D File Offset: 0x0001610D
		public static LocalizedString ClientAccessProtocolEWS
		{
			get
			{
				return new LocalizedString("ClientAccessProtocolEWS", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00017F2C File Offset: 0x0001612C
		public static LocalizedString ExceptionInvalidMeumAddress(string address)
		{
			return new LocalizedString("ExceptionInvalidMeumAddress", "Ex11F8EB", false, true, DataStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00017F5B File Offset: 0x0001615B
		public static LocalizedString MailRecipientTypeExternal
		{
			get
			{
				return new LocalizedString("MailRecipientTypeExternal", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00017F79 File Offset: 0x00016179
		public static LocalizedString DeliveryTypeSmtpRelayWithinAdSite
		{
			get
			{
				return new LocalizedString("DeliveryTypeSmtpRelayWithinAdSite", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00017F98 File Offset: 0x00016198
		public static LocalizedString ErrorSerialNumberFormatError(string serialNumber)
		{
			return new LocalizedString("ErrorSerialNumberFormatError", "Ex9FC4FD", false, true, DataStrings.ResourceManager, new object[]
			{
				serialNumber
			});
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00017FC7 File Offset: 0x000161C7
		public static LocalizedString InvalidDialGroupEntryCsvFormat
		{
			get
			{
				return new LocalizedString("InvalidDialGroupEntryCsvFormat", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00017FE8 File Offset: 0x000161E8
		public static LocalizedString ExceptionProtocolConnectionSettingsInvalidHostname(string settings)
		{
			return new LocalizedString("ExceptionProtocolConnectionSettingsInvalidHostname", "ExF95D91", false, true, DataStrings.ResourceManager, new object[]
			{
				settings
			});
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00018018 File Offset: 0x00016218
		public static LocalizedString ErrorCannotConvertToBinary(string error)
		{
			return new LocalizedString("ErrorCannotConvertToBinary", "ExBA027C", false, true, DataStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00018047 File Offset: 0x00016247
		public static LocalizedString KindKeywordMeetings
		{
			get
			{
				return new LocalizedString("KindKeywordMeetings", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00018065 File Offset: 0x00016265
		public static LocalizedString CustomScheduleEveryFourHours
		{
			get
			{
				return new LocalizedString("CustomScheduleEveryFourHours", "ExCEB31E", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00018084 File Offset: 0x00016284
		public static LocalizedString ProxyAddressTemplateEmptyPrefixOrValue(string template)
		{
			return new LocalizedString("ProxyAddressTemplateEmptyPrefixOrValue", "ExAF1843", false, true, DataStrings.ResourceManager, new object[]
			{
				template
			});
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000180B4 File Offset: 0x000162B4
		public static LocalizedString InvalidAddressSpaceType(string addressType)
		{
			return new LocalizedString("InvalidAddressSpaceType", "Ex5D713F", false, true, DataStrings.ResourceManager, new object[]
			{
				addressType
			});
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x000180E3 File Offset: 0x000162E3
		public static LocalizedString MailRecipientTypeUnknown
		{
			get
			{
				return new LocalizedString("MailRecipientTypeUnknown", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00018101 File Offset: 0x00016301
		public static LocalizedString AmbiguousRecipient
		{
			get
			{
				return new LocalizedString("AmbiguousRecipient", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001811F File Offset: 0x0001631F
		public static LocalizedString SmtpReceiveCapabilitiesAllowConsumerMail
		{
			get
			{
				return new LocalizedString("SmtpReceiveCapabilitiesAllowConsumerMail", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00018140 File Offset: 0x00016340
		public static LocalizedString ErrorFileShareWitnessServerNameMustNotBeIP(string computerName)
		{
			return new LocalizedString("ErrorFileShareWitnessServerNameMustNotBeIP", "", false, false, DataStrings.ResourceManager, new object[]
			{
				computerName
			});
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001816F File Offset: 0x0001636F
		public static LocalizedString InvalidFlagValue
		{
			get
			{
				return new LocalizedString("InvalidFlagValue", "ExB5B5F5", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001818D File Offset: 0x0001638D
		public static LocalizedString TlsAuthLevelDomainValidation
		{
			get
			{
				return new LocalizedString("TlsAuthLevelDomainValidation", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000181AC File Offset: 0x000163AC
		public static LocalizedString StarDomainNotAllowed(string property)
		{
			return new LocalizedString("StarDomainNotAllowed", "Ex2050B9", false, true, DataStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x000181DB File Offset: 0x000163DB
		public static LocalizedString ClientAccessBasicAuthentication
		{
			get
			{
				return new LocalizedString("ClientAccessBasicAuthentication", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x000181F9 File Offset: 0x000163F9
		public static LocalizedString InvalidTimeOfDayFormatCustomWorkingHours
		{
			get
			{
				return new LocalizedString("InvalidTimeOfDayFormatCustomWorkingHours", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00018217 File Offset: 0x00016417
		public static LocalizedString AttachmentContent
		{
			get
			{
				return new LocalizedString("AttachmentContent", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00018238 File Offset: 0x00016438
		public static LocalizedString ErrorValueAlreadyPresent(string value)
		{
			return new LocalizedString("ErrorValueAlreadyPresent", "Ex0659B9", false, true, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00018267 File Offset: 0x00016467
		public static LocalizedString InvalidKeyMappingTransferToGalContact
		{
			get
			{
				return new LocalizedString("InvalidKeyMappingTransferToGalContact", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00018285 File Offset: 0x00016485
		public static LocalizedString ProtocolSpx
		{
			get
			{
				return new LocalizedString("ProtocolSpx", "Ex6115B0", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000182A4 File Offset: 0x000164A4
		public static LocalizedString ErrorNonGeneric(string typeName)
		{
			return new LocalizedString("ErrorNonGeneric", "ExC7B407", false, true, DataStrings.ResourceManager, new object[]
			{
				typeName
			});
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x000182D3 File Offset: 0x000164D3
		public static LocalizedString ErrorPoliciesDowngradeCustomFailures
		{
			get
			{
				return new LocalizedString("ErrorPoliciesDowngradeCustomFailures", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000182F4 File Offset: 0x000164F4
		public static LocalizedString Int32ParsableStringConstraintViolation(string value)
		{
			return new LocalizedString("Int32ParsableStringConstraintViolation", "Ex2BD81F", false, true, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00018323 File Offset: 0x00016523
		public static LocalizedString ErrorCostOutOfRange
		{
			get
			{
				return new LocalizedString("ErrorCostOutOfRange", "Ex1B6704", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00018344 File Offset: 0x00016544
		public static LocalizedString ExceptionRemoveEumPrimary(string primary)
		{
			return new LocalizedString("ExceptionRemoveEumPrimary", "Ex0EBE82", false, true, DataStrings.ResourceManager, new object[]
			{
				primary
			});
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00018373 File Offset: 0x00016573
		public static LocalizedString ExceptionNegativeUnit
		{
			get
			{
				return new LocalizedString("ExceptionNegativeUnit", "ExA4258B", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00018394 File Offset: 0x00016594
		public static LocalizedString ConstraintViolationIpRangeNotAllowed(string ipAddress, ulong maxAllowed)
		{
			return new LocalizedString("ConstraintViolationIpRangeNotAllowed", "", false, false, DataStrings.ResourceManager, new object[]
			{
				ipAddress,
				maxAllowed
			});
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000183CC File Offset: 0x000165CC
		public static LocalizedString DeliveryTypeSmtpDeliveryToMailbox
		{
			get
			{
				return new LocalizedString("DeliveryTypeSmtpDeliveryToMailbox", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000183EC File Offset: 0x000165EC
		public static LocalizedString ErrorOutOfRange(int min, int max)
		{
			return new LocalizedString("ErrorOutOfRange", "Ex3E498C", false, true, DataStrings.ResourceManager, new object[]
			{
				min,
				max
			});
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00018429 File Offset: 0x00016629
		public static LocalizedString PropertyIsMandatory
		{
			get
			{
				return new LocalizedString("PropertyIsMandatory", "ExF0DCB8", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00018447 File Offset: 0x00016647
		public static LocalizedString RoutingNonBHExpansionServer
		{
			get
			{
				return new LocalizedString("RoutingNonBHExpansionServer", "Ex2E6B0F", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00018465 File Offset: 0x00016665
		public static LocalizedString ExceptionReadOnlyPropertyBag
		{
			get
			{
				return new LocalizedString("ExceptionReadOnlyPropertyBag", "Ex794ED6", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00018483 File Offset: 0x00016683
		public static LocalizedString UnreachableQueueNextHopDomain
		{
			get
			{
				return new LocalizedString("UnreachableQueueNextHopDomain", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x000184A1 File Offset: 0x000166A1
		public static LocalizedString InvalidSmtpDomain
		{
			get
			{
				return new LocalizedString("InvalidSmtpDomain", "ExA19A7C", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x000184BF File Offset: 0x000166BF
		public static LocalizedString InvalidDialledNumberFormatC
		{
			get
			{
				return new LocalizedString("InvalidDialledNumberFormatC", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x000184DD File Offset: 0x000166DD
		public static LocalizedString DeliveryTypeDnsConnectorDelivery
		{
			get
			{
				return new LocalizedString("DeliveryTypeDnsConnectorDelivery", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000184FC File Offset: 0x000166FC
		public static LocalizedString DialGroupNotSpecifiedOnDialPlanB(string name, string group)
		{
			return new LocalizedString("DialGroupNotSpecifiedOnDialPlanB", "", false, false, DataStrings.ResourceManager, new object[]
			{
				name,
				group
			});
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00018530 File Offset: 0x00016730
		public static LocalizedString SharingPolicyDomainInvalidActionForDomain(string value)
		{
			return new LocalizedString("SharingPolicyDomainInvalidActionForDomain", "", false, false, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00018560 File Offset: 0x00016760
		public static LocalizedString InvalidTlsCertificateName(string s)
		{
			return new LocalizedString("InvalidTlsCertificateName", "", false, false, DataStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001858F File Offset: 0x0001678F
		public static LocalizedString DigitStringPatternDescription
		{
			get
			{
				return new LocalizedString("DigitStringPatternDescription", "ExCF6A08", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x000185AD File Offset: 0x000167AD
		public static LocalizedString CustomScheduleDailyFrom9AMTo5PMAtWeekDays
		{
			get
			{
				return new LocalizedString("CustomScheduleDailyFrom9AMTo5PMAtWeekDays", "Ex0697CA", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x000185CB File Offset: 0x000167CB
		public static LocalizedString InvalidKeyMappingContext
		{
			get
			{
				return new LocalizedString("InvalidKeyMappingContext", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x000185E9 File Offset: 0x000167E9
		public static LocalizedString CustomScheduleDaily1AM
		{
			get
			{
				return new LocalizedString("CustomScheduleDaily1AM", "Ex4A5C60", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00018607 File Offset: 0x00016807
		public static LocalizedString FromInternet
		{
			get
			{
				return new LocalizedString("FromInternet", "Ex1D11EA", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00018628 File Offset: 0x00016828
		public static LocalizedString ErrorInputOfScheduleMustExclusive(string exclusiveInput)
		{
			return new LocalizedString("ErrorInputOfScheduleMustExclusive", "ExE1CFCF", false, true, DataStrings.ResourceManager, new object[]
			{
				exclusiveInput
			});
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00018658 File Offset: 0x00016858
		public static LocalizedString ExceptionCannotSetDifferentType(Type propertyType, Type otherType)
		{
			return new LocalizedString("ExceptionCannotSetDifferentType", "Ex7AEA22", false, true, DataStrings.ResourceManager, new object[]
			{
				propertyType,
				otherType
			});
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001868B File Offset: 0x0001688B
		public static LocalizedString TotalCopiedItems
		{
			get
			{
				return new LocalizedString("TotalCopiedItems", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x000186A9 File Offset: 0x000168A9
		public static LocalizedString ClientAccessProtocolRPS
		{
			get
			{
				return new LocalizedString("ClientAccessProtocolRPS", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000186C8 File Offset: 0x000168C8
		public static LocalizedString ErrorUnknownOperation(string op, string adds, string removes)
		{
			return new LocalizedString("ErrorUnknownOperation", "ExE436F3", false, true, DataStrings.ResourceManager, new object[]
			{
				op,
				adds,
				removes
			});
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x000186FF File Offset: 0x000168FF
		public static LocalizedString ClientAccessNonBasicAuthentication
		{
			get
			{
				return new LocalizedString("ClientAccessNonBasicAuthentication", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00018720 File Offset: 0x00016920
		public static LocalizedString ExceptionParseError(string invalidQuery, int position)
		{
			return new LocalizedString("ExceptionParseError", "Ex0D5420", false, true, DataStrings.ResourceManager, new object[]
			{
				invalidQuery,
				position
			});
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00018758 File Offset: 0x00016958
		public static LocalizedString LinkedPartnerGroupInformationInvalidParameter(string propertyValue)
		{
			return new LocalizedString("LinkedPartnerGroupInformationInvalidParameter", "Ex1CA2DB", false, true, DataStrings.ResourceManager, new object[]
			{
				propertyValue
			});
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00018787 File Offset: 0x00016987
		public static LocalizedString RoutingNoMdb
		{
			get
			{
				return new LocalizedString("RoutingNoMdb", "ExD1ECC6", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x000187A5 File Offset: 0x000169A5
		public static LocalizedString PublicFolderPermissionRoleEditor
		{
			get
			{
				return new LocalizedString("PublicFolderPermissionRoleEditor", "Ex91F409", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000187C4 File Offset: 0x000169C4
		public static LocalizedString SharingPolicyDomainInvalidActionForAnonymous(string value)
		{
			return new LocalizedString("SharingPolicyDomainInvalidActionForAnonymous", "ExF4E04C", false, true, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000187F4 File Offset: 0x000169F4
		public static LocalizedString ConstraintViolationDontMatchUnit(string unit, string timeSpan)
		{
			return new LocalizedString("ConstraintViolationDontMatchUnit", "Ex3F6F4A", false, true, DataStrings.ResourceManager, new object[]
			{
				unit,
				timeSpan
			});
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00018827 File Offset: 0x00016A27
		public static LocalizedString MailRecipientTypeMailbox
		{
			get
			{
				return new LocalizedString("MailRecipientTypeMailbox", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00018845 File Offset: 0x00016A45
		public static LocalizedString CopyErrors
		{
			get
			{
				return new LocalizedString("CopyErrors", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00018863 File Offset: 0x00016A63
		public static LocalizedString SnapinNameTooShort
		{
			get
			{
				return new LocalizedString("SnapinNameTooShort", "Ex86B3D3", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00018881 File Offset: 0x00016A81
		public static LocalizedString TextBody
		{
			get
			{
				return new LocalizedString("TextBody", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001889F File Offset: 0x00016A9F
		public static LocalizedString DeliveryTypeSmtpRelayToTiRg
		{
			get
			{
				return new LocalizedString("DeliveryTypeSmtpRelayToTiRg", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000188C0 File Offset: 0x00016AC0
		public static LocalizedString ClientAccessRulesBlockedConnection(string ruleName)
		{
			return new LocalizedString("ClientAccessRulesBlockedConnection", "", false, false, DataStrings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x000188EF File Offset: 0x00016AEF
		public static LocalizedString ConstraintViolationNoLeadingOrTrailingWhitespace
		{
			get
			{
				return new LocalizedString("ConstraintViolationNoLeadingOrTrailingWhitespace", "Ex9E6D8B", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001890D File Offset: 0x00016B0D
		public static LocalizedString CalendarSharingFreeBusyDetail
		{
			get
			{
				return new LocalizedString("CalendarSharingFreeBusyDetail", "ExAB93FA", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001892C File Offset: 0x00016B2C
		public static LocalizedString ErrorLengthOfExDataTimeByteArray(int length)
		{
			return new LocalizedString("ErrorLengthOfExDataTimeByteArray", "Ex15DC70", false, true, DataStrings.ResourceManager, new object[]
			{
				length
			});
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00018960 File Offset: 0x00016B60
		public static LocalizedString ConfigurationSettingsScopePropertyNotFound2(string name, string knownScopes)
		{
			return new LocalizedString("ConfigurationSettingsScopePropertyNotFound2", "", false, false, DataStrings.ResourceManager, new object[]
			{
				name,
				knownScopes
			});
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00018993 File Offset: 0x00016B93
		public static LocalizedString ItemClass
		{
			get
			{
				return new LocalizedString("ItemClass", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000189B4 File Offset: 0x00016BB4
		public static LocalizedString PropertyName(string propertyName)
		{
			return new LocalizedString("PropertyName", "Ex9C3C13", false, true, DataStrings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x000189E3 File Offset: 0x00016BE3
		public static LocalizedString InvalidNotationFormat
		{
			get
			{
				return new LocalizedString("InvalidNotationFormat", "ExEA7943", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00018A01 File Offset: 0x00016C01
		public static LocalizedString ClientAccessProtocolOAB
		{
			get
			{
				return new LocalizedString("ClientAccessProtocolOAB", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00018A20 File Offset: 0x00016C20
		public static LocalizedString InvalidCharInString(string s, string c)
		{
			return new LocalizedString("InvalidCharInString", "", false, false, DataStrings.ResourceManager, new object[]
			{
				s,
				c
			});
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00018A53 File Offset: 0x00016C53
		public static LocalizedString InvalidCallerIdItemTypePhoneNumber
		{
			get
			{
				return new LocalizedString("InvalidCallerIdItemTypePhoneNumber", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00018A71 File Offset: 0x00016C71
		public static LocalizedString ExceptionDurationOverflow
		{
			get
			{
				return new LocalizedString("ExceptionDurationOverflow", "ExCD0088", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00018A8F File Offset: 0x00016C8F
		public static LocalizedString ExchangeLegacyServers
		{
			get
			{
				return new LocalizedString("ExchangeLegacyServers", "ExECFE38", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00018AAD File Offset: 0x00016CAD
		public static LocalizedString Down
		{
			get
			{
				return new LocalizedString("Down", "Ex455BE6", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00018ACC File Offset: 0x00016CCC
		public static LocalizedString InvalidX400AddressSpace(string s)
		{
			return new LocalizedString("InvalidX400AddressSpace", "Ex8015D9", false, true, DataStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00018AFB File Offset: 0x00016CFB
		public static LocalizedString InvalidDialledNumberFormatA
		{
			get
			{
				return new LocalizedString("InvalidDialledNumberFormatA", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00018B1C File Offset: 0x00016D1C
		public static LocalizedString ConstraintViolationStringLengthTooShort(int minLength, int actualLength)
		{
			return new LocalizedString("ConstraintViolationStringLengthTooShort", "ExF2B74F", false, true, DataStrings.ResourceManager, new object[]
			{
				minLength,
				actualLength
			});
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00018B59 File Offset: 0x00016D59
		public static LocalizedString Misconfigured
		{
			get
			{
				return new LocalizedString("Misconfigured", "ExBB5420", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x00018B77 File Offset: 0x00016D77
		public static LocalizedString ProtocolTcpIP
		{
			get
			{
				return new LocalizedString("ProtocolTcpIP", "Ex69620F", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00018B98 File Offset: 0x00016D98
		public static LocalizedString InvalidIPAddressOrHostNameInSmartHost(string s)
		{
			return new LocalizedString("InvalidIPAddressOrHostNameInSmartHost", "Ex6F5D30", false, true, DataStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00018BC8 File Offset: 0x00016DC8
		public static LocalizedString SmtpResponseConstraintViolation(string property, string value)
		{
			return new LocalizedString("SmtpResponseConstraintViolation", "ExF50434", false, true, DataStrings.ResourceManager, new object[]
			{
				property,
				value
			});
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00018BFC File Offset: 0x00016DFC
		public static LocalizedString ExceptionEndPointMissingSeparator(string ipBinding)
		{
			return new LocalizedString("ExceptionEndPointMissingSeparator", "Ex008998", false, true, DataStrings.ResourceManager, new object[]
			{
				ipBinding
			});
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00018C2B File Offset: 0x00016E2B
		public static LocalizedString RoleEntryStringMustBeCommaSeparated
		{
			get
			{
				return new LocalizedString("RoleEntryStringMustBeCommaSeparated", "ExBE931F", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00018C49 File Offset: 0x00016E49
		public static LocalizedString SmtpReceiveCapabilitiesAcceptOorgProtocol
		{
			get
			{
				return new LocalizedString("SmtpReceiveCapabilitiesAcceptOorgProtocol", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00018C68 File Offset: 0x00016E68
		public static LocalizedString ExceptionFormatInvalid(string input)
		{
			return new LocalizedString("ExceptionFormatInvalid", "Ex5B47E7", false, true, DataStrings.ResourceManager, new object[]
			{
				input
			});
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00018C98 File Offset: 0x00016E98
		public static LocalizedString IncludeExcludeInvalid(string value)
		{
			return new LocalizedString("IncludeExcludeInvalid", "", false, false, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x00018CC7 File Offset: 0x00016EC7
		public static LocalizedString WeekendDays
		{
			get
			{
				return new LocalizedString("WeekendDays", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00018CE5 File Offset: 0x00016EE5
		public static LocalizedString ProtocolNetBios
		{
			get
			{
				return new LocalizedString("ProtocolNetBios", "Ex9D59FF", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00018D03 File Offset: 0x00016F03
		public static LocalizedString CustomScheduleDailyFrom8AMTo12PMAnd1PMTo5PMAtWeekDays
		{
			get
			{
				return new LocalizedString("CustomScheduleDailyFrom8AMTo12PMAnd1PMTo5PMAtWeekDays", "Ex5F5396", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00018D21 File Offset: 0x00016F21
		public static LocalizedString ConfigurationSettingsAppSettingsError
		{
			get
			{
				return new LocalizedString("ConfigurationSettingsAppSettingsError", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00018D3F File Offset: 0x00016F3F
		public static LocalizedString InvalidKeyMappingTransferToNumber
		{
			get
			{
				return new LocalizedString("InvalidKeyMappingTransferToNumber", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00018D5D File Offset: 0x00016F5D
		public static LocalizedString CustomScheduleDaily5AM
		{
			get
			{
				return new LocalizedString("CustomScheduleDaily5AM", "Ex6D671C", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00018D7B File Offset: 0x00016F7B
		public static LocalizedString KindKeywordRssFeeds
		{
			get
			{
				return new LocalizedString("KindKeywordRssFeeds", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00018D9C File Offset: 0x00016F9C
		public static LocalizedString ErrorCannotConvertNull(string type)
		{
			return new LocalizedString("ErrorCannotConvertNull", "ExB15E10", false, true, DataStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00018DCC File Offset: 0x00016FCC
		public static LocalizedString InvalidScopedAddressSpace(string s)
		{
			return new LocalizedString("InvalidScopedAddressSpace", "ExB0B976", false, true, DataStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00018DFB File Offset: 0x00016FFB
		public static LocalizedString MessageStatusSuspended
		{
			get
			{
				return new LocalizedString("MessageStatusSuspended", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00018E19 File Offset: 0x00017019
		public static LocalizedString ShadowMessagePreferenceLocalOnly
		{
			get
			{
				return new LocalizedString("ShadowMessagePreferenceLocalOnly", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00018E37 File Offset: 0x00017037
		public static LocalizedString Unavailable
		{
			get
			{
				return new LocalizedString("Unavailable", "ExADA259", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00018E58 File Offset: 0x00017058
		public static LocalizedString ErrorConversionFailedWithException(string value, string originalType, string resultType, Exception inner)
		{
			return new LocalizedString("ErrorConversionFailedWithException", "ExFC3A2E", false, true, DataStrings.ResourceManager, new object[]
			{
				value,
				originalType,
				resultType,
				inner
			});
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00018E94 File Offset: 0x00017094
		public static LocalizedString DuplicateParameterException(string paramName)
		{
			return new LocalizedString("DuplicateParameterException", "ExBA0CA8", false, true, DataStrings.ResourceManager, new object[]
			{
				paramName
			});
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x00018EC3 File Offset: 0x000170C3
		public static LocalizedString RejectStatusCode
		{
			get
			{
				return new LocalizedString("RejectStatusCode", "Ex336AFD", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00018EE4 File Offset: 0x000170E4
		public static LocalizedString ExceptionInvalidEumAddress(string address)
		{
			return new LocalizedString("ExceptionInvalidEumAddress", "Ex1255BC", false, true, DataStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x00018F13 File Offset: 0x00017113
		public static LocalizedString Thursday
		{
			get
			{
				return new LocalizedString("Thursday", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00018F34 File Offset: 0x00017134
		public static LocalizedString DuplicatedColumn(string duplicatedColumn)
		{
			return new LocalizedString("DuplicatedColumn", "ExE8A76C", false, true, DataStrings.ResourceManager, new object[]
			{
				duplicatedColumn
			});
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00018F63 File Offset: 0x00017163
		public static LocalizedString StartingAddressAndMaskAddressFamilyMismatch
		{
			get
			{
				return new LocalizedString("StartingAddressAndMaskAddressFamilyMismatch", "Ex55EF2D", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00018F81 File Offset: 0x00017181
		public static LocalizedString InvalidCallerIdItemTypeDefaultContactsFolder
		{
			get
			{
				return new LocalizedString("InvalidCallerIdItemTypeDefaultContactsFolder", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00018F9F File Offset: 0x0001719F
		public static LocalizedString ErrorPoliciesDefault
		{
			get
			{
				return new LocalizedString("ErrorPoliciesDefault", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00018FC0 File Offset: 0x000171C0
		public static LocalizedString ConstraintViolationValueIsDisallowed(string invalidValues, string input)
		{
			return new LocalizedString("ConstraintViolationValueIsDisallowed", "Ex75F989", false, true, DataStrings.ResourceManager, new object[]
			{
				invalidValues,
				input
			});
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00018FF3 File Offset: 0x000171F3
		public static LocalizedString PublicFolderPermissionRoleContributor
		{
			get
			{
				return new LocalizedString("PublicFolderPermissionRoleContributor", "ExD5FF81", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x00019011 File Offset: 0x00017211
		public static LocalizedString Weekdays
		{
			get
			{
				return new LocalizedString("Weekdays", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001902F File Offset: 0x0001722F
		public static LocalizedString SmtpReceiveCapabilitiesAllowSubmit
		{
			get
			{
				return new LocalizedString("SmtpReceiveCapabilitiesAllowSubmit", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00019050 File Offset: 0x00017250
		public static LocalizedString ExceptionEndPointInvalidPort(string ipBinding)
		{
			return new LocalizedString("ExceptionEndPointInvalidPort", "Ex9F2388", false, true, DataStrings.ResourceManager, new object[]
			{
				ipBinding
			});
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001907F File Offset: 0x0001727F
		public static LocalizedString PropertyNotEmptyOrNull
		{
			get
			{
				return new LocalizedString("PropertyNotEmptyOrNull", "Ex9D869A", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000190A0 File Offset: 0x000172A0
		public static LocalizedString ErrorReadOnlyCauseObject(string name)
		{
			return new LocalizedString("ErrorReadOnlyCauseObject", "ExF07C01", false, true, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000190D0 File Offset: 0x000172D0
		public static LocalizedString ErrorCannotConvertFromString(string value, string resultType, string error)
		{
			return new LocalizedString("ErrorCannotConvertFromString", "Ex57F8F7", false, true, DataStrings.ResourceManager, new object[]
			{
				value,
				resultType,
				error
			});
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00019107 File Offset: 0x00017307
		public static LocalizedString MAPIBlockOutlookVersionsPatternDescription
		{
			get
			{
				return new LocalizedString("MAPIBlockOutlookVersionsPatternDescription", "ExD18D66", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x00019125 File Offset: 0x00017325
		public static LocalizedString BucketVersionPatternDescription
		{
			get
			{
				return new LocalizedString("BucketVersionPatternDescription", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00019143 File Offset: 0x00017343
		public static LocalizedString MessageStatusNone
		{
			get
			{
				return new LocalizedString("MessageStatusNone", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00019161 File Offset: 0x00017361
		public static LocalizedString PublicFolderPermissionRoleAuthor
		{
			get
			{
				return new LocalizedString("PublicFolderPermissionRoleAuthor", "Ex4E0FFA", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001917F File Offset: 0x0001737F
		public static LocalizedString DeliveryTypeUndefined
		{
			get
			{
				return new LocalizedString("DeliveryTypeUndefined", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001919D File Offset: 0x0001739D
		public static LocalizedString SmtpReceiveCapabilitiesAcceptCrossForestMail
		{
			get
			{
				return new LocalizedString("SmtpReceiveCapabilitiesAcceptCrossForestMail", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x000191BB File Offset: 0x000173BB
		public static LocalizedString ConnectorSourceMigrated
		{
			get
			{
				return new LocalizedString("ConnectorSourceMigrated", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x000191D9 File Offset: 0x000173D9
		public static LocalizedString RoutingNoMatchingConnector
		{
			get
			{
				return new LocalizedString("RoutingNoMatchingConnector", "Ex8457BA", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x000191F7 File Offset: 0x000173F7
		public static LocalizedString QueueStatusNone
		{
			get
			{
				return new LocalizedString("QueueStatusNone", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00019215 File Offset: 0x00017415
		public static LocalizedString ClientAccessProtocolEAC
		{
			get
			{
				return new LocalizedString("ClientAccessProtocolEAC", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00019233 File Offset: 0x00017433
		public static LocalizedString EncryptionTypeTLS
		{
			get
			{
				return new LocalizedString("EncryptionTypeTLS", "Ex25776E", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00019251 File Offset: 0x00017451
		public static LocalizedString RoleEntryNameTooShort
		{
			get
			{
				return new LocalizedString("RoleEntryNameTooShort", "ExC54CD3", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001926F File Offset: 0x0001746F
		public static LocalizedString DuplicatesRemoved
		{
			get
			{
				return new LocalizedString("DuplicatesRemoved", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001928D File Offset: 0x0001748D
		public static LocalizedString MessageStatusPendingSuspend
		{
			get
			{
				return new LocalizedString("MessageStatusPendingSuspend", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x000192AC File Offset: 0x000174AC
		public static LocalizedString ConstraintNoTrailingSpecificCharacter(string input, char invalidChar)
		{
			return new LocalizedString("ConstraintNoTrailingSpecificCharacter", "Ex8A4B03", false, true, DataStrings.ResourceManager, new object[]
			{
				input,
				invalidChar
			});
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000192E4 File Offset: 0x000174E4
		public static LocalizedString DependencyCheckFailed(string feature, string featureValue, string dependencyFeatureName, string dependencyFeatureValue)
		{
			return new LocalizedString("DependencyCheckFailed", "Ex74B1D5", false, true, DataStrings.ResourceManager, new object[]
			{
				feature,
				featureValue,
				dependencyFeatureName,
				dependencyFeatureValue
			});
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001931F File Offset: 0x0001751F
		public static LocalizedString SmtpReceiveCapabilitiesAcceptOrgHeaders
		{
			get
			{
				return new LocalizedString("SmtpReceiveCapabilitiesAcceptOrgHeaders", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0001933D File Offset: 0x0001753D
		public static LocalizedString CustomScheduleDailyAtMidnight
		{
			get
			{
				return new LocalizedString("CustomScheduleDailyAtMidnight", "ExBF07EE", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001935B File Offset: 0x0001755B
		public static LocalizedString RecipientStatusComplete
		{
			get
			{
				return new LocalizedString("RecipientStatusComplete", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00019379 File Offset: 0x00017579
		public static LocalizedString InvalidCallerIdItemTypeGALContactr
		{
			get
			{
				return new LocalizedString("InvalidCallerIdItemTypeGALContactr", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00019397 File Offset: 0x00017597
		public static LocalizedString ExchangeServers
		{
			get
			{
				return new LocalizedString("ExchangeServers", "ExBCB94C", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x000193B5 File Offset: 0x000175B5
		public static LocalizedString ScheduleModeNever
		{
			get
			{
				return new LocalizedString("ScheduleModeNever", "Ex78E645", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000193D4 File Offset: 0x000175D4
		public static LocalizedString ConfigurationSettingsPropertyNotFound(string name)
		{
			return new LocalizedString("ConfigurationSettingsPropertyNotFound", "", false, false, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00019403 File Offset: 0x00017603
		public static LocalizedString CustomScheduleDailyFrom11PMTo3AM
		{
			get
			{
				return new LocalizedString("CustomScheduleDailyFrom11PMTo3AM", "Ex44DB2D", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00019421 File Offset: 0x00017621
		public static LocalizedString InvalidDialledNumberFormatD
		{
			get
			{
				return new LocalizedString("InvalidDialledNumberFormatD", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00019440 File Offset: 0x00017640
		public static LocalizedString ErrorCannotConvertToString(string error)
		{
			return new LocalizedString("ErrorCannotConvertToString", "ExFD6919", false, true, DataStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00019470 File Offset: 0x00017670
		public static LocalizedString ServicePlanSchemaCheckFailed(string schemaError)
		{
			return new LocalizedString("ServicePlanSchemaCheckFailed", "Ex09C509", false, true, DataStrings.ResourceManager, new object[]
			{
				schemaError
			});
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001949F File Offset: 0x0001769F
		public static LocalizedString CustomScheduleDailyFrom1AMTo5AM
		{
			get
			{
				return new LocalizedString("CustomScheduleDailyFrom1AMTo5AM", "Ex864B55", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x000194BD File Offset: 0x000176BD
		public static LocalizedString ProxyAddressPrefixShouldNotBeAllSpace
		{
			get
			{
				return new LocalizedString("ProxyAddressPrefixShouldNotBeAllSpace", "Ex56DEBF", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x000194DC File Offset: 0x000176DC
		public static LocalizedString ApplicationPermissionRoleEntryParameterNotEmptyException(string name)
		{
			return new LocalizedString("ApplicationPermissionRoleEntryParameterNotEmptyException", "ExC82900", false, true, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001950B File Offset: 0x0001770B
		public static LocalizedString CustomScheduleDaily2AM
		{
			get
			{
				return new LocalizedString("CustomScheduleDaily2AM", "Ex69544A", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001952C File Offset: 0x0001772C
		public static LocalizedString SharingPolicyDomainInvalid(string value)
		{
			return new LocalizedString("SharingPolicyDomainInvalid", "Ex693DF9", false, true, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001955B File Offset: 0x0001775B
		public static LocalizedString InvalidKeyMappingFindMeSecondNumber
		{
			get
			{
				return new LocalizedString("InvalidKeyMappingFindMeSecondNumber", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001957C File Offset: 0x0001777C
		public static LocalizedString ExceptionValueOverflow(string minValue, string maxValue, string value)
		{
			return new LocalizedString("ExceptionValueOverflow", "Ex59AFCB", false, true, DataStrings.ResourceManager, new object[]
			{
				minValue,
				maxValue,
				value
			});
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x000195B3 File Offset: 0x000177B3
		public static LocalizedString MessageStatusReady
		{
			get
			{
				return new LocalizedString("MessageStatusReady", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x000195D1 File Offset: 0x000177D1
		public static LocalizedString GroupByDay
		{
			get
			{
				return new LocalizedString("GroupByDay", "ExD5BDA4", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x000195EF File Offset: 0x000177EF
		public static LocalizedString DeliveryTypeHeartbeat
		{
			get
			{
				return new LocalizedString("DeliveryTypeHeartbeat", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00019610 File Offset: 0x00017810
		public static LocalizedString ErrorToBinaryNotImplemented(string sourceType)
		{
			return new LocalizedString("ErrorToBinaryNotImplemented", "Ex5E8F74", false, true, DataStrings.ResourceManager, new object[]
			{
				sourceType
			});
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00019640 File Offset: 0x00017840
		public static LocalizedString BadEnumValue(Type enumType)
		{
			return new LocalizedString("BadEnumValue", "", false, false, DataStrings.ResourceManager, new object[]
			{
				enumType
			});
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00019670 File Offset: 0x00017870
		public static LocalizedString ExceptionInvalidServerName(string server)
		{
			return new LocalizedString("ExceptionInvalidServerName", "", false, false, DataStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000196A0 File Offset: 0x000178A0
		public static LocalizedString ExceptionInvalidSmtpAddress(string address)
		{
			return new LocalizedString("ExceptionInvalidSmtpAddress", "Ex7573F6", false, true, DataStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x000196CF File Offset: 0x000178CF
		public static LocalizedString FileExtensionOrSplatPatternDescription
		{
			get
			{
				return new LocalizedString("FileExtensionOrSplatPatternDescription", "ExADC20C", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x000196ED File Offset: 0x000178ED
		public static LocalizedString MessageStatusActive
		{
			get
			{
				return new LocalizedString("MessageStatusActive", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001970B File Offset: 0x0001790B
		public static LocalizedString Monday
		{
			get
			{
				return new LocalizedString("Monday", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00019729 File Offset: 0x00017929
		public static LocalizedString DeferReasonTransientAcceptedDomainsLoadFailure
		{
			get
			{
				return new LocalizedString("DeferReasonTransientAcceptedDomainsLoadFailure", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00019748 File Offset: 0x00017948
		public static LocalizedString ConstraintViolationStringContainsInvalidCharacters(string invalidCharacters, string input)
		{
			return new LocalizedString("ConstraintViolationStringContainsInvalidCharacters", "Ex15340E", false, true, DataStrings.ResourceManager, new object[]
			{
				invalidCharacters,
				input
			});
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001977B File Offset: 0x0001797B
		public static LocalizedString ExceptionEventSourceNull
		{
			get
			{
				return new LocalizedString("ExceptionEventSourceNull", "Ex6D5B28", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00019799 File Offset: 0x00017999
		public static LocalizedString PublicFolderPermissionRoleNonEditingAuthor
		{
			get
			{
				return new LocalizedString("PublicFolderPermissionRoleNonEditingAuthor", "Ex046A9A", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x000197B8 File Offset: 0x000179B8
		public static LocalizedString InvalidIntRangeArgument(string argument)
		{
			return new LocalizedString("InvalidIntRangeArgument", "", false, false, DataStrings.ResourceManager, new object[]
			{
				argument
			});
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x000197E7 File Offset: 0x000179E7
		public static LocalizedString CustomScheduleEveryTwoHours
		{
			get
			{
				return new LocalizedString("CustomScheduleEveryTwoHours", "Ex14ED89", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00019808 File Offset: 0x00017A08
		public static LocalizedString WrongNumberOfColumns(int rowNumber, int expectedColumnCount, int actualColumnCount)
		{
			return new LocalizedString("WrongNumberOfColumns", "Ex2C49BB", false, true, DataStrings.ResourceManager, new object[]
			{
				rowNumber,
				expectedColumnCount,
				actualColumnCount
			});
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0001984E File Offset: 0x00017A4E
		public static LocalizedString InvalidCallerIdItemTypePersonaContact
		{
			get
			{
				return new LocalizedString("InvalidCallerIdItemTypePersonaContact", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0001986C File Offset: 0x00017A6C
		public static LocalizedString DeferReasonConfigUpdate
		{
			get
			{
				return new LocalizedString("DeferReasonConfigUpdate", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0001988A File Offset: 0x00017A8A
		public static LocalizedString ConstraintViolationValueIsNullOrEmpty
		{
			get
			{
				return new LocalizedString("ConstraintViolationValueIsNullOrEmpty", "ExB7847B", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x000198A8 File Offset: 0x00017AA8
		public static LocalizedString SubjectProperty
		{
			get
			{
				return new LocalizedString("SubjectProperty", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000198C8 File Offset: 0x00017AC8
		public static LocalizedString FilterOnlyAttributes(string attributeName)
		{
			return new LocalizedString("FilterOnlyAttributes", "Ex125B9D", false, true, DataStrings.ResourceManager, new object[]
			{
				attributeName
			});
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000198F8 File Offset: 0x00017AF8
		public static LocalizedString InvalidAlternateMailboxString(string blob, char separator)
		{
			return new LocalizedString("InvalidAlternateMailboxString", "Ex9ED82F", false, true, DataStrings.ResourceManager, new object[]
			{
				blob,
				separator
			});
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00019930 File Offset: 0x00017B30
		public static LocalizedString InvalidKeySelection_Zero
		{
			get
			{
				return new LocalizedString("InvalidKeySelection_Zero", "ExFBBC38", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001994E File Offset: 0x00017B4E
		public static LocalizedString QueueStatusReady
		{
			get
			{
				return new LocalizedString("QueueStatusReady", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001996C File Offset: 0x00017B6C
		public static LocalizedString ProtocolNamedPipes
		{
			get
			{
				return new LocalizedString("ProtocolNamedPipes", "Ex19D08D", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001998C File Offset: 0x00017B8C
		public static LocalizedString ConstraintViolationValueOutOfRange(string minValue, string maxValue, string actualValue)
		{
			return new LocalizedString("ConstraintViolationValueOutOfRange", "Ex503186", false, true, DataStrings.ResourceManager, new object[]
			{
				minValue,
				maxValue,
				actualValue
			});
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000199C4 File Offset: 0x00017BC4
		public static LocalizedString ExceptionInvalidLongitude(double lon)
		{
			return new LocalizedString("ExceptionInvalidLongitude", "", false, false, DataStrings.ResourceManager, new object[]
			{
				lon
			});
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000199F8 File Offset: 0x00017BF8
		public static LocalizedString ExceptionGeoCoordinatesWithWrongFormat(string geoCoordinates)
		{
			return new LocalizedString("ExceptionGeoCoordinatesWithWrongFormat", "", false, false, DataStrings.ResourceManager, new object[]
			{
				geoCoordinates
			});
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00019A28 File Offset: 0x00017C28
		public static LocalizedString RoleEntryNameInvalidException(string name)
		{
			return new LocalizedString("RoleEntryNameInvalidException", "ExE1DB37", false, true, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00019A58 File Offset: 0x00017C58
		public static LocalizedString ConstraintViolationStringLengthTooLong(int maxLength, int actualLength)
		{
			return new LocalizedString("ConstraintViolationStringLengthTooLong", "ExA75D27", false, true, DataStrings.ResourceManager, new object[]
			{
				maxLength,
				actualLength
			});
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00019A95 File Offset: 0x00017C95
		public static LocalizedString ProtocolVnsSpp
		{
			get
			{
				return new LocalizedString("ProtocolVnsSpp", "Ex85E948", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00019AB4 File Offset: 0x00017CB4
		public static LocalizedString ExceptionUnsupportedSourceType(object obj, Type type)
		{
			return new LocalizedString("ExceptionUnsupportedSourceType", "Ex4086F4", false, true, DataStrings.ResourceManager, new object[]
			{
				obj,
				type
			});
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00019AE8 File Offset: 0x00017CE8
		public static LocalizedString InvalidRoleEntryType(string entryType)
		{
			return new LocalizedString("InvalidRoleEntryType", "ExA26E16", false, true, DataStrings.ResourceManager, new object[]
			{
				entryType
			});
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00019B18 File Offset: 0x00017D18
		public static LocalizedString ConfigurationSettingsPropertyBadType(string name, string type)
		{
			return new LocalizedString("ConfigurationSettingsPropertyBadType", "", false, false, DataStrings.ResourceManager, new object[]
			{
				name,
				type
			});
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00019B4B File Offset: 0x00017D4B
		public static LocalizedString KindKeywordNotes
		{
			get
			{
				return new LocalizedString("KindKeywordNotes", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00019B6C File Offset: 0x00017D6C
		public static LocalizedString InvalidSmtpDomainName(string address)
		{
			return new LocalizedString("InvalidSmtpDomainName", "Ex41D592", false, true, DataStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00019B9B File Offset: 0x00017D9B
		public static LocalizedString ErrorCannotConvert
		{
			get
			{
				return new LocalizedString("ErrorCannotConvert", "Ex9D3C98", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x00019BB9 File Offset: 0x00017DB9
		public static LocalizedString InvalidDialledNumberFormatB
		{
			get
			{
				return new LocalizedString("InvalidDialledNumberFormatB", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00019BD7 File Offset: 0x00017DD7
		public static LocalizedString InvalidDialGroupEntryFormat
		{
			get
			{
				return new LocalizedString("InvalidDialGroupEntryFormat", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x00019BF5 File Offset: 0x00017DF5
		public static LocalizedString EapMustHaveOneEnabledPrimarySmtpAddressTemplate
		{
			get
			{
				return new LocalizedString("EapMustHaveOneEnabledPrimarySmtpAddressTemplate", "Ex77737B", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00019C13 File Offset: 0x00017E13
		public static LocalizedString FileExtensionPatternDescription
		{
			get
			{
				return new LocalizedString("FileExtensionPatternDescription", "Ex543745", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00019C34 File Offset: 0x00017E34
		public static LocalizedString TooManyRows(int maximumRowCount)
		{
			return new LocalizedString("TooManyRows", "Ex6D5ECE", false, true, DataStrings.ResourceManager, new object[]
			{
				maximumRowCount
			});
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00019C68 File Offset: 0x00017E68
		public static LocalizedString Unknown
		{
			get
			{
				return new LocalizedString("Unknown", "Ex54E959", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00019C86 File Offset: 0x00017E86
		public static LocalizedString QueueStatusRetry
		{
			get
			{
				return new LocalizedString("QueueStatusRetry", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00019CA4 File Offset: 0x00017EA4
		public static LocalizedString InvalidHostname(string s)
		{
			return new LocalizedString("InvalidHostname", "ExFD9758", false, true, DataStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00019CD3 File Offset: 0x00017ED3
		public static LocalizedString Wednesday
		{
			get
			{
				return new LocalizedString("Wednesday", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00019CF1 File Offset: 0x00017EF1
		public static LocalizedString InvalidKeyMappingFindMe
		{
			get
			{
				return new LocalizedString("InvalidKeyMappingFindMe", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00019D0F File Offset: 0x00017F0F
		public static LocalizedString DeferReasonReroutedByStoreDriver
		{
			get
			{
				return new LocalizedString("DeferReasonReroutedByStoreDriver", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00019D30 File Offset: 0x00017F30
		public static LocalizedString InvalidX509IdentifierFormat(string value)
		{
			return new LocalizedString("InvalidX509IdentifierFormat", "Ex2F4975", false, true, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00019D5F File Offset: 0x00017F5F
		public static LocalizedString ScheduleModeAlways
		{
			get
			{
				return new LocalizedString("ScheduleModeAlways", "ExAB9272", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00019D80 File Offset: 0x00017F80
		public static LocalizedString ConstraintViolationStringIsNotValidCultureInfo(string value)
		{
			return new LocalizedString("ConstraintViolationStringIsNotValidCultureInfo", "Ex96BDA9", false, true, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00019DB0 File Offset: 0x00017FB0
		public static LocalizedString ErrorContainsOutOfRange(string value)
		{
			return new LocalizedString("ErrorContainsOutOfRange", "Ex7CB832", false, true, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00019DE0 File Offset: 0x00017FE0
		public static LocalizedString ErrorConversionFailed(ProviderPropertyDefinition property, object value)
		{
			return new LocalizedString("ErrorConversionFailed", "ExF99E12", false, true, DataStrings.ResourceManager, new object[]
			{
				property,
				value
			});
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00019E14 File Offset: 0x00018014
		public static LocalizedString ExceptionInvalidFilterOperator(string op, string invalidQuery, int position)
		{
			return new LocalizedString("ExceptionInvalidFilterOperator", "ExB87763", false, true, DataStrings.ResourceManager, new object[]
			{
				op,
				invalidQuery,
				position
			});
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00019E50 File Offset: 0x00018050
		public static LocalizedString FileIsEmpty
		{
			get
			{
				return new LocalizedString("FileIsEmpty", "Ex4E4545", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00019E6E File Offset: 0x0001806E
		public static LocalizedString ExceptionSerializationDataAbsent
		{
			get
			{
				return new LocalizedString("ExceptionSerializationDataAbsent", "ExC24362", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00019E8C File Offset: 0x0001808C
		public static LocalizedString SmtpReceiveCapabilitiesAcceptOorgHeader
		{
			get
			{
				return new LocalizedString("SmtpReceiveCapabilitiesAcceptOorgHeader", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00019EAA File Offset: 0x000180AA
		public static LocalizedString DsnText
		{
			get
			{
				return new LocalizedString("DsnText", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00019EC8 File Offset: 0x000180C8
		public static LocalizedString CustomScheduleDailyFrom9AMTo12PMAnd1PMTo6PMAtWeekDays
		{
			get
			{
				return new LocalizedString("CustomScheduleDailyFrom9AMTo12PMAnd1PMTo6PMAtWeekDays", "Ex7CC99C", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00019EE6 File Offset: 0x000180E6
		public static LocalizedString StorageTransientFailureDuringContentConversion
		{
			get
			{
				return new LocalizedString("StorageTransientFailureDuringContentConversion", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00019F04 File Offset: 0x00018104
		public static LocalizedString ErrorInvalidScheduleType(string typeName)
		{
			return new LocalizedString("ErrorInvalidScheduleType", "Ex60837A", false, true, DataStrings.ResourceManager, new object[]
			{
				typeName
			});
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00019F33 File Offset: 0x00018133
		public static LocalizedString MessageStatusRetry
		{
			get
			{
				return new LocalizedString("MessageStatusRetry", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x00019F51 File Offset: 0x00018151
		public static LocalizedString CustomProxyAddressPrefixDisplayName
		{
			get
			{
				return new LocalizedString("CustomProxyAddressPrefixDisplayName", "Ex7F31AD", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x00019F6F File Offset: 0x0001816F
		public static LocalizedString CLIDPatternDescription
		{
			get
			{
				return new LocalizedString("CLIDPatternDescription", "Ex667AA2", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x00019F8D File Offset: 0x0001818D
		public static LocalizedString ClientAccessProtocolEAS
		{
			get
			{
				return new LocalizedString("ClientAccessProtocolEAS", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00019FAC File Offset: 0x000181AC
		public static LocalizedString ErrorDSNCultureInput(string cultureName)
		{
			return new LocalizedString("ErrorDSNCultureInput", "ExA6BD51", false, true, DataStrings.ResourceManager, new object[]
			{
				cultureName
			});
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00019FDC File Offset: 0x000181DC
		public static LocalizedString ExceptionEmptyPrefixEum(string address)
		{
			return new LocalizedString("ExceptionEmptyPrefixEum", "Ex4B2C15", false, true, DataStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001A00B File Offset: 0x0001820B
		public static LocalizedString ConstraintViolationInvalidWindowsLiveIDLocalPart
		{
			get
			{
				return new LocalizedString("ConstraintViolationInvalidWindowsLiveIDLocalPart", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0001A029 File Offset: 0x00018229
		public static LocalizedString ExceptionParseInternalMessageId
		{
			get
			{
				return new LocalizedString("ExceptionParseInternalMessageId", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001A047 File Offset: 0x00018247
		public static LocalizedString RecipientStatusReady
		{
			get
			{
				return new LocalizedString("RecipientStatusReady", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0001A065 File Offset: 0x00018265
		public static LocalizedString ReceiveNone
		{
			get
			{
				return new LocalizedString("ReceiveNone", "ExBE515B", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001A084 File Offset: 0x00018284
		public static LocalizedString ErrorReadOnlyCauseProperty(string name)
		{
			return new LocalizedString("ErrorReadOnlyCauseProperty", "Ex5CB650", false, true, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001A0B4 File Offset: 0x000182B4
		public static LocalizedString ErrorOrignalMultiValuedProperty(string name)
		{
			return new LocalizedString("ErrorOrignalMultiValuedProperty", "ExBAE406", false, true, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0001A0E3 File Offset: 0x000182E3
		public static LocalizedString UserPrincipalNamePatternDescription
		{
			get
			{
				return new LocalizedString("UserPrincipalNamePatternDescription", "Ex74BF44", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0001A101 File Offset: 0x00018301
		public static LocalizedString ToPartner
		{
			get
			{
				return new LocalizedString("ToPartner", "ExAFACB1", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001A11F File Offset: 0x0001831F
		public static LocalizedString DeliveryTypeSmtpRelayToDag
		{
			get
			{
				return new LocalizedString("DeliveryTypeSmtpRelayToDag", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001A140 File Offset: 0x00018340
		public static LocalizedString InvalidIPAddressFormat(string ipAddress)
		{
			return new LocalizedString("InvalidIPAddressFormat", "ExD5D51B", false, true, DataStrings.ResourceManager, new object[]
			{
				ipAddress
			});
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001A170 File Offset: 0x00018370
		public static LocalizedString ExceptionEventSourceNotFound(string eventSource)
		{
			return new LocalizedString("ExceptionEventSourceNotFound", "ExFFF932", false, true, DataStrings.ResourceManager, new object[]
			{
				eventSource
			});
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0001A19F File Offset: 0x0001839F
		public static LocalizedString DeliveryTypeSmtpRelayWithinAdSiteToEdge
		{
			get
			{
				return new LocalizedString("DeliveryTypeSmtpRelayWithinAdSiteToEdge", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001A1C0 File Offset: 0x000183C0
		public static LocalizedString InvalidNumberFormat(string value)
		{
			return new LocalizedString("InvalidNumberFormat", "", false, false, DataStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0001A1EF File Offset: 0x000183EF
		public static LocalizedString DeferReasonRecipientThreadLimitExceeded
		{
			get
			{
				return new LocalizedString("DeferReasonRecipientThreadLimitExceeded", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001A210 File Offset: 0x00018410
		public static LocalizedString InvalidIPRangeFormat(string s)
		{
			return new LocalizedString("InvalidIPRangeFormat", "Ex549848", false, true, DataStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0001A23F File Offset: 0x0001843F
		public static LocalizedString Up
		{
			get
			{
				return new LocalizedString("Up", "Ex9D354C", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0001A25D File Offset: 0x0001845D
		public static LocalizedString PreserveDSNBody
		{
			get
			{
				return new LocalizedString("PreserveDSNBody", "Ex4BE49F", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0001A27B File Offset: 0x0001847B
		public static LocalizedString ElcScheduleInsufficientGap
		{
			get
			{
				return new LocalizedString("ElcScheduleInsufficientGap", "Ex2F9187", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0001A299 File Offset: 0x00018499
		public static LocalizedString ExceptionTypeNotEnhancedTimeSpanOrTimeSpan
		{
			get
			{
				return new LocalizedString("ExceptionTypeNotEnhancedTimeSpanOrTimeSpan", "Ex902DFC", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001A2B8 File Offset: 0x000184B8
		public static LocalizedString ConstraintViolationNotValidDomain(string domain)
		{
			return new LocalizedString("ConstraintViolationNotValidDomain", "Ex01B62F", false, true, DataStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0001A2E7 File Offset: 0x000184E7
		public static LocalizedString Recipients
		{
			get
			{
				return new LocalizedString("Recipients", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0001A305 File Offset: 0x00018505
		public static LocalizedString CustomScheduleDaily4AM
		{
			get
			{
				return new LocalizedString("CustomScheduleDaily4AM", "Ex2B23AD", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001A324 File Offset: 0x00018524
		public static LocalizedString AdminAuditLogInvalidParameterOrModifiedProperty(string propertyValue)
		{
			return new LocalizedString("AdminAuditLogInvalidParameterOrModifiedProperty", "Ex174688", false, true, DataStrings.ResourceManager, new object[]
			{
				propertyValue
			});
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001A354 File Offset: 0x00018554
		public static LocalizedString ConfigurationSettingsPropertyFailedValidation(string name, string value)
		{
			return new LocalizedString("ConfigurationSettingsPropertyFailedValidation", "", false, false, DataStrings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0001A387 File Offset: 0x00018587
		public static LocalizedString ExceptionDefaultTypeMismatch
		{
			get
			{
				return new LocalizedString("ExceptionDefaultTypeMismatch", "ExFB6B13", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001A3A8 File Offset: 0x000185A8
		public static LocalizedString WebServiceRoleEntryParameterNotEmptyException(string name)
		{
			return new LocalizedString("WebServiceRoleEntryParameterNotEmptyException", "", false, false, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0001A3D7 File Offset: 0x000185D7
		public static LocalizedString ClientAccessProtocolOWA
		{
			get
			{
				return new LocalizedString("ClientAccessProtocolOWA", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001A3F8 File Offset: 0x000185F8
		public static LocalizedString ConstraintViolationStringDoesNotContainNonWhitespaceCharacter(string input)
		{
			return new LocalizedString("ConstraintViolationStringDoesNotContainNonWhitespaceCharacter", "ExCC54B0", false, true, DataStrings.ResourceManager, new object[]
			{
				input
			});
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001A428 File Offset: 0x00018628
		public static LocalizedString ExceptionUnsupportedDataFormat(object data)
		{
			return new LocalizedString("ExceptionUnsupportedDataFormat", "Ex30771D", false, true, DataStrings.ResourceManager, new object[]
			{
				data
			});
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0001A457 File Offset: 0x00018657
		public static LocalizedString RecipientStatusNone
		{
			get
			{
				return new LocalizedString("RecipientStatusNone", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001A478 File Offset: 0x00018678
		public static LocalizedString ProhibitedColumnPresent(string prohibitedColumn)
		{
			return new LocalizedString("ProhibitedColumnPresent", "Ex4D5119", false, true, DataStrings.ResourceManager, new object[]
			{
				prohibitedColumn
			});
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0001A4A7 File Offset: 0x000186A7
		public static LocalizedString StandardTrialEdition
		{
			get
			{
				return new LocalizedString("StandardTrialEdition", "Ex200B65", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0001A4C5 File Offset: 0x000186C5
		public static LocalizedString ShadowMessagePreferenceRemoteOnly
		{
			get
			{
				return new LocalizedString("ShadowMessagePreferenceRemoteOnly", "", false, false, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001A4E4 File Offset: 0x000186E4
		public static LocalizedString ConstraintViolationStringDoesContainsNonASCIICharacter(string input)
		{
			return new LocalizedString("ConstraintViolationStringDoesContainsNonASCIICharacter", "Ex15BD8B", false, true, DataStrings.ResourceManager, new object[]
			{
				input
			});
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001A514 File Offset: 0x00018714
		public static LocalizedString ExceptionEmptyPrefix(string address)
		{
			return new LocalizedString("ExceptionEmptyPrefix", "Ex54A3BD", false, true, DataStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001A543 File Offset: 0x00018743
		public static LocalizedString DoNotConvert
		{
			get
			{
				return new LocalizedString("DoNotConvert", "ExC2DDEE", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001A564 File Offset: 0x00018764
		public static LocalizedString ErrorMvpNotImplemented(string type, string propName)
		{
			return new LocalizedString("ErrorMvpNotImplemented", "ExAF1C63", false, true, DataStrings.ResourceManager, new object[]
			{
				type,
				propName
			});
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0001A597 File Offset: 0x00018797
		public static LocalizedString CustomScheduleDaily10PM
		{
			get
			{
				return new LocalizedString("CustomScheduleDaily10PM", "ExCB21D8", false, true, DataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001A5B8 File Offset: 0x000187B8
		public static LocalizedString SnapinNameInvalidCharException(string name)
		{
			return new LocalizedString("SnapinNameInvalidCharException", "Ex9CFED5", false, true, DataStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001A5E8 File Offset: 0x000187E8
		public static LocalizedString ExceptionEndPointInvalidIPAddress(string ipBinding)
		{
			return new LocalizedString("ExceptionEndPointInvalidIPAddress", "Ex07E0D9", false, true, DataStrings.ResourceManager, new object[]
			{
				ipBinding
			});
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001A617 File Offset: 0x00018817
		public static LocalizedString GetLocalizedString(DataStrings.IDs key)
		{
			return new LocalizedString(DataStrings.stringIDs[(uint)key], DataStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000313 RID: 787
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(391);

		// Token: 0x04000314 RID: 788
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Data.DataStrings", typeof(DataStrings).GetTypeInfo().Assembly);

		// Token: 0x020000D2 RID: 210
		public enum IDs : uint
		{
			// Token: 0x04000316 RID: 790
			NotesProxyAddressPrefixDisplayName = 401351504U,
			// Token: 0x04000317 RID: 791
			InvalidCallerIdItemTypePersonalContact = 2802501868U,
			// Token: 0x04000318 RID: 792
			ExceptionParseNotSupported = 1831343041U,
			// Token: 0x04000319 RID: 793
			InvalidKeySelectionA = 2207492931U,
			// Token: 0x0400031A RID: 794
			ConnectorTypePartner = 2003039265U,
			// Token: 0x0400031B RID: 795
			SclValue = 214463947U,
			// Token: 0x0400031C RID: 796
			MessageStatusLocked = 343989129U,
			// Token: 0x0400031D RID: 797
			DaysOfWeek_None = 2044618123U,
			// Token: 0x0400031E RID: 798
			InvalidAddressSpaceTypeNullOrEmpty = 3268914636U,
			// Token: 0x0400031F RID: 799
			ProxyAddressShouldNotBeAllSpace = 1025341760U,
			// Token: 0x04000320 RID: 800
			CalendarSharingFreeBusyReviewer = 3216786356U,
			// Token: 0x04000321 RID: 801
			ProtocolLocalRpc = 2296262216U,
			// Token: 0x04000322 RID: 802
			InsufficientSpace = 3662889629U,
			// Token: 0x04000323 RID: 803
			ExchangeUsers = 1357933441U,
			// Token: 0x04000324 RID: 804
			RoutingIncompatibleDeliveryDomain = 4099951545U,
			// Token: 0x04000325 RID: 805
			EnterpriseTrialEdition = 4134731995U,
			// Token: 0x04000326 RID: 806
			InvalidNumberFormatString = 315555574U,
			// Token: 0x04000327 RID: 807
			Partners = 1448686489U,
			// Token: 0x04000328 RID: 808
			CustomScheduleDaily12PM = 3119708545U,
			// Token: 0x04000329 RID: 809
			CoexistenceTrialEdition = 2191591450U,
			// Token: 0x0400032A RID: 810
			CustomExtensionInvalidArgument = 3866345678U,
			// Token: 0x0400032B RID: 811
			NonWorkingHours = 955250317U,
			// Token: 0x0400032C RID: 812
			ClientAccessProtocolIMAP4 = 801480496U,
			// Token: 0x0400032D RID: 813
			ShadowMessagePreferencePreferRemote = 319956112U,
			// Token: 0x0400032E RID: 814
			CustomScheduleDailyFromMidnightTo4AM = 842516494U,
			// Token: 0x0400032F RID: 815
			CustomScheduleDailyFrom8AMTo5PMAtWeekDays = 2432881730U,
			// Token: 0x04000330 RID: 816
			EventLogText = 3910386293U,
			// Token: 0x04000331 RID: 817
			ClientAccessAdfsAuthentication = 2532083337U,
			// Token: 0x04000332 RID: 818
			ErrorADFormatError = 3646660508U,
			// Token: 0x04000333 RID: 819
			MeetingFullDetailsWithAttendees = 4100370227U,
			// Token: 0x04000334 RID: 820
			CustomScheduleDaily3AM = 2139945176U,
			// Token: 0x04000335 RID: 821
			ExceptionCalculatedDependsOnCalculated = 1571847379U,
			// Token: 0x04000336 RID: 822
			RoutingNoRouteToMdb = 4153254550U,
			// Token: 0x04000337 RID: 823
			SmtpReceiveCapabilitiesAcceptProxyProtocol = 3267940029U,
			// Token: 0x04000338 RID: 824
			DeferReasonADTransientFailureDuringContentConversion = 554923823U,
			// Token: 0x04000339 RID: 825
			HeaderPromotionModeMayCreate = 3857745828U,
			// Token: 0x0400033A RID: 826
			ConnectorSourceHybridWizard = 2392738789U,
			// Token: 0x0400033B RID: 827
			DeferReasonAgent = 2680336401U,
			// Token: 0x0400033C RID: 828
			InvalidCustomMenuKeyMappingA = 86136247U,
			// Token: 0x0400033D RID: 829
			InvalidResourcePropertySyntax = 352888273U,
			// Token: 0x0400033E RID: 830
			SmtpReceiveCapabilitiesAcceptCloudServicesMail = 651446229U,
			// Token: 0x0400033F RID: 831
			CcMailProxyAddressPrefixDisplayName = 830368596U,
			// Token: 0x04000340 RID: 832
			CustomScheduleEveryHour = 1420072811U,
			// Token: 0x04000341 RID: 833
			DeferReasonConcurrencyLimitInStoreDriver = 255146954U,
			// Token: 0x04000342 RID: 834
			GroupByMonth = 1322244886U,
			// Token: 0x04000343 RID: 835
			ContactsSharing = 906828259U,
			// Token: 0x04000344 RID: 836
			ExceptionUnsupportedNetworkProtocol = 2020388072U,
			// Token: 0x04000345 RID: 837
			SmtpReceiveCapabilitiesAcceptXAttrProtocol = 452471808U,
			// Token: 0x04000346 RID: 838
			ExceptionInvlidCharInProtocolName = 2998256021U,
			// Token: 0x04000347 RID: 839
			TransientFailure = 1284528402U,
			// Token: 0x04000348 RID: 840
			InvalidInputErrorMsg = 2446644924U,
			// Token: 0x04000349 RID: 841
			ErrorFileShareWitnessServerNameMustNotBeEmpty = 242132428U,
			// Token: 0x0400034A RID: 842
			HostnameTooLong = 2360034689U,
			// Token: 0x0400034B RID: 843
			InvalidKeyMappingVoiceMail = 118693033U,
			// Token: 0x0400034C RID: 844
			KindKeywordEmail = 2600270045U,
			// Token: 0x0400034D RID: 845
			ConstraintViolationStringLengthIsEmpty = 3423705850U,
			// Token: 0x0400034E RID: 846
			AnonymousUsers = 1737591621U,
			// Token: 0x0400034F RID: 847
			ErrorQuotionMarkNotSupportedInKql = 2996964666U,
			// Token: 0x04000350 RID: 848
			LatAmSpanish = 2637923947U,
			// Token: 0x04000351 RID: 849
			SearchRecipientsCc = 1831009676U,
			// Token: 0x04000352 RID: 850
			InvalidOperationCurrentProperty = 1181135370U,
			// Token: 0x04000353 RID: 851
			ErrorPoliciesUpgradeCustomFailures = 665138238U,
			// Token: 0x04000354 RID: 852
			CustomScheduleEveryHalfHour = 643679508U,
			// Token: 0x04000355 RID: 853
			EmptyExchangeObjectVersion = 4198186357U,
			// Token: 0x04000356 RID: 854
			CustomGreetingFilePatternDescription = 2760883904U,
			// Token: 0x04000357 RID: 855
			Partitioned = 1485002803U,
			// Token: 0x04000358 RID: 856
			ClientAccessProtocolOA = 220124761U,
			// Token: 0x04000359 RID: 857
			PublicFolderPermissionRolePublishingAuthor = 3095705478U,
			// Token: 0x0400035A RID: 858
			InvalidIPAddressInSmartHost = 2578042802U,
			// Token: 0x0400035B RID: 859
			DeliveryTypeSmtpRelayToMailboxDeliveryGroup = 2736312437U,
			// Token: 0x0400035C RID: 860
			UnknownEdition = 2889762178U,
			// Token: 0x0400035D RID: 861
			InvalidFormatExchangeObjectVersion = 3765811860U,
			// Token: 0x0400035E RID: 862
			ToInternet = 3427924322U,
			// Token: 0x0400035F RID: 863
			KindKeywordTasks = 1226277855U,
			// Token: 0x04000360 RID: 864
			MarkedAsRetryDeliveryIfRejected = 736960779U,
			// Token: 0x04000361 RID: 865
			ConnectorTypeOnPremises = 4060482376U,
			// Token: 0x04000362 RID: 866
			HeaderValue = 2163423328U,
			// Token: 0x04000363 RID: 867
			SmtpReceiveCapabilitiesAcceptProxyFromProtocol = 2117971051U,
			// Token: 0x04000364 RID: 868
			PoisonQueueNextHopDomain = 1153159321U,
			// Token: 0x04000365 RID: 869
			GroupWiseProxyAddressPrefixDisplayName = 936162170U,
			// Token: 0x04000366 RID: 870
			MsMailProxyAddressPrefixDisplayName = 3861972062U,
			// Token: 0x04000367 RID: 871
			Exchange2007 = 2924600836U,
			// Token: 0x04000368 RID: 872
			HeaderName = 2909775076U,
			// Token: 0x04000369 RID: 873
			RejectText = 1178729042U,
			// Token: 0x0400036A RID: 874
			NameValidationSpaceAllowedPatternDescription = 2493635772U,
			// Token: 0x0400036B RID: 875
			ConstraintViolationStringLengthCauseOutOfMemory = 2255949938U,
			// Token: 0x0400036C RID: 876
			InvalidFormat = 3639763634U,
			// Token: 0x0400036D RID: 877
			CustomPeriod = 729961244U,
			// Token: 0x0400036E RID: 878
			PublicFolderPermissionRolePublishingEditor = 95015764U,
			// Token: 0x0400036F RID: 879
			InvalidKeyMappingKey = 1893156877U,
			// Token: 0x04000370 RID: 880
			StringConversionDelegateNotSet = 224331957U,
			// Token: 0x04000371 RID: 881
			NumberingPlanPatternDescription = 2603796430U,
			// Token: 0x04000372 RID: 882
			MeetingFullDetails = 716522036U,
			// Token: 0x04000373 RID: 883
			ProtocolLoggingLevelNone = 3427843085U,
			// Token: 0x04000374 RID: 884
			ErrorInputFormatError = 2353628225U,
			// Token: 0x04000375 RID: 885
			Ascending = 3390434404U,
			// Token: 0x04000376 RID: 886
			SmtpReceiveCapabilitiesAcceptXOriginalFromProtocol = 3345411900U,
			// Token: 0x04000377 RID: 887
			Exchange2003 = 599002008U,
			// Token: 0x04000378 RID: 888
			WorkingHours = 1604545240U,
			// Token: 0x04000379 RID: 889
			DeferReasonTransientAttributionFailure = 2838855213U,
			// Token: 0x0400037A RID: 890
			FromEnterprise = 2536591407U,
			// Token: 0x0400037B RID: 891
			TlsAuthLevelCertificateValidation = 1206366831U,
			// Token: 0x0400037C RID: 892
			Friday = 4094875965U,
			// Token: 0x0400037D RID: 893
			MeetingLimitedDetails = 4009888891U,
			// Token: 0x0400037E RID: 894
			KeyMappingInvalidArgument = 61359385U,
			// Token: 0x0400037F RID: 895
			LegacyDNPatternDescription = 1186448673U,
			// Token: 0x04000380 RID: 896
			SmtpReceiveCapabilitiesAcceptXSysProbeProtocol = 2245804386U,
			// Token: 0x04000381 RID: 897
			SentTime = 2677919833U,
			// Token: 0x04000382 RID: 898
			SearchSender = 2511844601U,
			// Token: 0x04000383 RID: 899
			ErrorCannotAddNullValue = 2397470300U,
			// Token: 0x04000384 RID: 900
			ScheduleModeScheduledTimes = 3811769309U,
			// Token: 0x04000385 RID: 901
			ProtocolAppleTalk = 391606028U,
			// Token: 0x04000386 RID: 902
			DigitStringOrEmptyPatternDescription = 3390939332U,
			// Token: 0x04000387 RID: 903
			EnterpriseEdition = 26915469U,
			// Token: 0x04000388 RID: 904
			ConnectorSourceDefault = 2726993793U,
			// Token: 0x04000389 RID: 905
			ExceptionVersionlessObject = 3372601165U,
			// Token: 0x0400038A RID: 906
			AliasPatternDescription = 2815103562U,
			// Token: 0x0400038B RID: 907
			ReceivedTime = 1026314696U,
			// Token: 0x0400038C RID: 908
			ParameterNameEmptyException = 3479640092U,
			// Token: 0x0400038D RID: 909
			RecipientStatusLocked = 2043548597U,
			// Token: 0x0400038E RID: 910
			SmtpReceiveCapabilitiesAcceptProxyToProtocol = 1248401958U,
			// Token: 0x0400038F RID: 911
			PermissionGroupsNone = 1109398861U,
			// Token: 0x04000390 RID: 912
			ArgumentMustBeAscii = 2137442040U,
			// Token: 0x04000391 RID: 913
			ExceptionNetworkProtocolDuplicate = 1284326164U,
			// Token: 0x04000392 RID: 914
			RecipientStatusRetry = 3511836031U,
			// Token: 0x04000393 RID: 915
			GroupExpansionRecipients = 1002496550U,
			// Token: 0x04000394 RID: 916
			LegacyDNProxyAddressPrefixDisplayName = 1061876008U,
			// Token: 0x04000395 RID: 917
			HeaderPromotionModeMustCreate = 781217110U,
			// Token: 0x04000396 RID: 918
			ProtocolLoggingLevelVerbose = 2082847001U,
			// Token: 0x04000397 RID: 919
			HeaderPromotionModeNoCreate = 1176489840U,
			// Token: 0x04000398 RID: 920
			EncryptionTypeSSL = 462794175U,
			// Token: 0x04000399 RID: 921
			KindKeywordDocs = 3512036720U,
			// Token: 0x0400039A RID: 922
			Unreachable = 2589626668U,
			// Token: 0x0400039B RID: 923
			CustomScheduleSundayAtMidnight = 1884450703U,
			// Token: 0x0400039C RID: 924
			ExceptionUnknownUnit = 4294726685U,
			// Token: 0x0400039D RID: 925
			SendNone = 3410257022U,
			// Token: 0x0400039E RID: 926
			SubjectPrefix = 1100730082U,
			// Token: 0x0400039F RID: 927
			DeliveryTypeSmtpRelayToServers = 3791230818U,
			// Token: 0x040003A0 RID: 928
			InvalidKeyMappingFindMeFirstNumberDuration = 1667689070U,
			// Token: 0x040003A1 RID: 929
			AirSyncProxyAddressPrefixDisplayName = 2482088490U,
			// Token: 0x040003A2 RID: 930
			CoexistenceEdition = 1474747046U,
			// Token: 0x040003A3 RID: 931
			UnsearchableItemsAdded = 2175102537U,
			// Token: 0x040003A4 RID: 932
			InvalidKeyMappingFormat = 2846565657U,
			// Token: 0x040003A5 RID: 933
			ClientAccessProtocolPOP3 = 4125301959U,
			// Token: 0x040003A6 RID: 934
			Word = 4143129766U,
			// Token: 0x040003A7 RID: 935
			AddressSpaceIsTooLong = 3614810764U,
			// Token: 0x040003A8 RID: 936
			AddressFamilyMismatch = 3699252422U,
			// Token: 0x040003A9 RID: 937
			KindKeywordFaxes = 294615990U,
			// Token: 0x040003AA RID: 938
			ExceptionEmptyProxyAddress = 2440320060U,
			// Token: 0x040003AB RID: 939
			ExceptionObjectInvalid = 882442171U,
			// Token: 0x040003AC RID: 940
			ExceptionReadOnlyMultiValuedProperty = 1177570172U,
			// Token: 0x040003AD RID: 941
			Tuesday = 2820941203U,
			// Token: 0x040003AE RID: 942
			DeferReasonADTransientFailureDuringResolve = 2714058314U,
			// Token: 0x040003AF RID: 943
			DeliveryTypeNonSmtpGatewayDelivery = 3770991413U,
			// Token: 0x040003B0 RID: 944
			UseExchangeDSNs = 980447290U,
			// Token: 0x040003B1 RID: 945
			KindKeywordContacts = 1316672322U,
			// Token: 0x040003B2 RID: 946
			FromLocal = 2112156755U,
			// Token: 0x040003B3 RID: 947
			ErrorPoliciesDowngradeDnsFailures = 3991588639U,
			// Token: 0x040003B4 RID: 948
			TlsAuthLevelEncryptionOnly = 466678310U,
			// Token: 0x040003B5 RID: 949
			Sunday = 1073167130U,
			// Token: 0x040003B6 RID: 950
			CustomScheduleDailyFrom9AMTo6PMAtWeekDays = 1629702106U,
			// Token: 0x040003B7 RID: 951
			Descending = 1777112844U,
			// Token: 0x040003B8 RID: 952
			DeliveryTypeUnreachable = 2350082752U,
			// Token: 0x040003B9 RID: 953
			KindKeywordPosts = 4067663994U,
			// Token: 0x040003BA RID: 954
			ErrorServerGuidAndNameBothEmpty = 4234859176U,
			// Token: 0x040003BB RID: 955
			ExLengthOfVersionByteArrayError = 64170653U,
			// Token: 0x040003BC RID: 956
			SearchRecipientsTo = 3444147793U,
			// Token: 0x040003BD RID: 957
			DeliveryTypeMapiDelivery = 918988081U,
			// Token: 0x040003BE RID: 958
			SearchRecipientsBcc = 2906226218U,
			// Token: 0x040003BF RID: 959
			EmptyNameInHostname = 1947725858U,
			// Token: 0x040003C0 RID: 960
			DisclaimerText = 1689084926U,
			// Token: 0x040003C1 RID: 961
			InvalidSmtpDomainWildcard = 1950405677U,
			// Token: 0x040003C2 RID: 962
			CustomScheduleDailyFrom2AMTo6AM = 3328112862U,
			// Token: 0x040003C3 RID: 963
			InvalidTimeOfDayFormat = 3884278210U,
			// Token: 0x040003C4 RID: 964
			Failed = 1054423051U,
			// Token: 0x040003C5 RID: 965
			CustomScheduleDailyFrom11PMTo6AM = 2176757305U,
			// Token: 0x040003C6 RID: 966
			ColonPrefix = 3124035205U,
			// Token: 0x040003C7 RID: 967
			ToGroupExpansionRecipients = 3544743625U,
			// Token: 0x040003C8 RID: 968
			InvalidCallerIdItemFormat = 923847139U,
			// Token: 0x040003C9 RID: 969
			PublicFolderPermissionRoleOwner = 4060377141U,
			// Token: 0x040003CA RID: 970
			PermissionGroupsCustom = 2634610906U,
			// Token: 0x040003CB RID: 971
			KindKeywordIm = 3561826809U,
			// Token: 0x040003CC RID: 972
			GroupByTotal = 1977971622U,
			// Token: 0x040003CD RID: 973
			BccGroupExpansionRecipients = 3781744254U,
			// Token: 0x040003CE RID: 974
			ExceptionInvlidNetworkAddressFormat = 833225182U,
			// Token: 0x040003CF RID: 975
			KindKeywordJournals = 2456836287U,
			// Token: 0x040003D0 RID: 976
			EmptyExchangeBuild = 1525449578U,
			// Token: 0x040003D1 RID: 977
			StandardEdition = 2321790947U,
			// Token: 0x040003D2 RID: 978
			FormatExchangeBuildWrong = 29693289U,
			// Token: 0x040003D3 RID: 979
			ClientAccessProtocolPSWS = 79160252U,
			// Token: 0x040003D4 RID: 980
			DeliveryTypeSmtpRelayToConnectorSourceServers = 3706983644U,
			// Token: 0x040003D5 RID: 981
			CustomScheduleSaturdayAtMidnight = 3519073218U,
			// Token: 0x040003D6 RID: 982
			ExceptionNoValue = 71385097U,
			// Token: 0x040003D7 RID: 983
			MailRecipientTypeDistributionGroup = 2608795131U,
			// Token: 0x040003D8 RID: 984
			ExceptionInvlidProtocolAddressFormat = 1561011830U,
			// Token: 0x040003D9 RID: 985
			CustomScheduleFridayAtMidnight = 2364433662U,
			// Token: 0x040003DA RID: 986
			DeliveryTypeShadowRedundancy = 255999871U,
			// Token: 0x040003DB RID: 987
			DeferReasonLoopDetected = 3951803838U,
			// Token: 0x040003DC RID: 988
			SearchRecipients = 3183375374U,
			// Token: 0x040003DD RID: 989
			CalendarSharingFreeBusySimple = 2862582797U,
			// Token: 0x040003DE RID: 990
			PublicFolderPermissionRoleReviewer = 3422989433U,
			// Token: 0x040003DF RID: 991
			QueueStatusActive = 2267899661U,
			// Token: 0x040003E0 RID: 992
			TlsAuthLevelCertificateExpiryCheck = 2387319017U,
			// Token: 0x040003E1 RID: 993
			InvalidAddressSpaceAddress = 3030346869U,
			// Token: 0x040003E2 RID: 994
			FromPartner = 1501056036U,
			// Token: 0x040003E3 RID: 995
			AllDays = 3502523774U,
			// Token: 0x040003E4 RID: 996
			DeferReasonTargetSiteInboundMailDisabled = 2099540954U,
			// Token: 0x040003E5 RID: 997
			ExceptionFormatNotSupported = 2760297639U,
			// Token: 0x040003E6 RID: 998
			DeliveryTypeDeliveryAgent = 2279665409U,
			// Token: 0x040003E7 RID: 999
			EstimatedItems = 1531043460U,
			// Token: 0x040003E8 RID: 1000
			CmdletParameterEmptyValidationException = 1261683271U,
			// Token: 0x040003E9 RID: 1001
			MessageStatusPendingRemove = 66601190U,
			// Token: 0x040003EA RID: 1002
			QueueStatusConnecting = 3129286587U,
			// Token: 0x040003EB RID: 1003
			DeliveryTypeSmtpRelayToRemoteAdSite = 1108839058U,
			// Token: 0x040003EC RID: 1004
			Saturday = 3478111469U,
			// Token: 0x040003ED RID: 1005
			ToEnterprise = 4135023588U,
			// Token: 0x040003EE RID: 1006
			InvalidHolidayScheduleFormat = 339456021U,
			// Token: 0x040003EF RID: 1007
			InvalidTimeOfDayFormatWorkingHours = 1915155164U,
			// Token: 0x040003F0 RID: 1008
			CustomScheduleDaily11PM = 3119708320U,
			// Token: 0x040003F1 RID: 1009
			QueueStatusSuspended = 1761145356U,
			// Token: 0x040003F2 RID: 1010
			SubmissionQueueNextHopDomain = 999227985U,
			// Token: 0x040003F3 RID: 1011
			ErrorNotSupportedForChangesOnlyCopy = 3153135486U,
			// Token: 0x040003F4 RID: 1012
			CcGroupExpansionRecipients = 4098793892U,
			// Token: 0x040003F5 RID: 1013
			ProxyAddressPrefixTooLong = 3039999898U,
			// Token: 0x040003F6 RID: 1014
			Pattern = 1660006804U,
			// Token: 0x040003F7 RID: 1015
			DeliveryTypeSmartHostConnectorDelivery = 1029030096U,
			// Token: 0x040003F8 RID: 1016
			KindKeywordVoiceMail = 2368086636U,
			// Token: 0x040003F9 RID: 1017
			RoutingNoRouteToMta = 570747971U,
			// Token: 0x040003FA RID: 1018
			ClientAccessProtocolEWS = 2517721976U,
			// Token: 0x040003FB RID: 1019
			MailRecipientTypeExternal = 810977739U,
			// Token: 0x040003FC RID: 1020
			DeliveryTypeSmtpRelayWithinAdSite = 749395574U,
			// Token: 0x040003FD RID: 1021
			InvalidDialGroupEntryCsvFormat = 1800544803U,
			// Token: 0x040003FE RID: 1022
			KindKeywordMeetings = 607066203U,
			// Token: 0x040003FF RID: 1023
			CustomScheduleEveryFourHours = 2682624686U,
			// Token: 0x04000400 RID: 1024
			MailRecipientTypeUnknown = 3134372274U,
			// Token: 0x04000401 RID: 1025
			AmbiguousRecipient = 894196715U,
			// Token: 0x04000402 RID: 1026
			SmtpReceiveCapabilitiesAllowConsumerMail = 182065869U,
			// Token: 0x04000403 RID: 1027
			InvalidFlagValue = 1601563988U,
			// Token: 0x04000404 RID: 1028
			TlsAuthLevelDomainValidation = 1435915854U,
			// Token: 0x04000405 RID: 1029
			ClientAccessBasicAuthentication = 3942979931U,
			// Token: 0x04000406 RID: 1030
			InvalidTimeOfDayFormatCustomWorkingHours = 3828585651U,
			// Token: 0x04000407 RID: 1031
			AttachmentContent = 1720677880U,
			// Token: 0x04000408 RID: 1032
			InvalidKeyMappingTransferToGalContact = 1152451692U,
			// Token: 0x04000409 RID: 1033
			ProtocolSpx = 2941311901U,
			// Token: 0x0400040A RID: 1034
			ErrorPoliciesDowngradeCustomFailures = 4237384485U,
			// Token: 0x0400040B RID: 1035
			ErrorCostOutOfRange = 4240385111U,
			// Token: 0x0400040C RID: 1036
			ExceptionNegativeUnit = 1140124266U,
			// Token: 0x0400040D RID: 1037
			DeliveryTypeSmtpDeliveryToMailbox = 982561113U,
			// Token: 0x0400040E RID: 1038
			PropertyIsMandatory = 2755629282U,
			// Token: 0x0400040F RID: 1039
			RoutingNonBHExpansionServer = 2085180697U,
			// Token: 0x04000410 RID: 1040
			ExceptionReadOnlyPropertyBag = 838517570U,
			// Token: 0x04000411 RID: 1041
			UnreachableQueueNextHopDomain = 1594003981U,
			// Token: 0x04000412 RID: 1042
			InvalidSmtpDomain = 682709795U,
			// Token: 0x04000413 RID: 1043
			InvalidDialledNumberFormatC = 416319699U,
			// Token: 0x04000414 RID: 1044
			DeliveryTypeDnsConnectorDelivery = 3338773880U,
			// Token: 0x04000415 RID: 1045
			DigitStringPatternDescription = 2544690258U,
			// Token: 0x04000416 RID: 1046
			CustomScheduleDailyFrom9AMTo5PMAtWeekDays = 3001525213U,
			// Token: 0x04000417 RID: 1047
			InvalidKeyMappingContext = 4197662657U,
			// Token: 0x04000418 RID: 1048
			CustomScheduleDaily1AM = 3826988642U,
			// Token: 0x04000419 RID: 1049
			FromInternet = 1512272061U,
			// Token: 0x0400041A RID: 1050
			TotalCopiedItems = 2854645212U,
			// Token: 0x0400041B RID: 1051
			ClientAccessProtocolRPS = 4083806514U,
			// Token: 0x0400041C RID: 1052
			ClientAccessNonBasicAuthentication = 3406691936U,
			// Token: 0x0400041D RID: 1053
			RoutingNoMdb = 3877102044U,
			// Token: 0x0400041E RID: 1054
			PublicFolderPermissionRoleEditor = 443721013U,
			// Token: 0x0400041F RID: 1055
			MailRecipientTypeMailbox = 3090942252U,
			// Token: 0x04000420 RID: 1056
			CopyErrors = 460086660U,
			// Token: 0x04000421 RID: 1057
			SnapinNameTooShort = 2182454218U,
			// Token: 0x04000422 RID: 1058
			TextBody = 4028819673U,
			// Token: 0x04000423 RID: 1059
			DeliveryTypeSmtpRelayToTiRg = 1277617174U,
			// Token: 0x04000424 RID: 1060
			ConstraintViolationNoLeadingOrTrailingWhitespace = 2058499689U,
			// Token: 0x04000425 RID: 1061
			CalendarSharingFreeBusyDetail = 616309426U,
			// Token: 0x04000426 RID: 1062
			ItemClass = 4177834631U,
			// Token: 0x04000427 RID: 1063
			InvalidNotationFormat = 38623232U,
			// Token: 0x04000428 RID: 1064
			ClientAccessProtocolOAB = 2067382811U,
			// Token: 0x04000429 RID: 1065
			InvalidCallerIdItemTypePhoneNumber = 2956076415U,
			// Token: 0x0400042A RID: 1066
			ExceptionDurationOverflow = 3803801081U,
			// Token: 0x0400042B RID: 1067
			ExchangeLegacyServers = 4064690660U,
			// Token: 0x0400042C RID: 1068
			Down = 1367191786U,
			// Token: 0x0400042D RID: 1069
			InvalidDialledNumberFormatA = 416319697U,
			// Token: 0x0400042E RID: 1070
			Misconfigured = 3211494971U,
			// Token: 0x0400042F RID: 1071
			ProtocolTcpIP = 974174060U,
			// Token: 0x04000430 RID: 1072
			RoleEntryStringMustBeCommaSeparated = 3287485561U,
			// Token: 0x04000431 RID: 1073
			SmtpReceiveCapabilitiesAcceptOorgProtocol = 3779112048U,
			// Token: 0x04000432 RID: 1074
			WeekendDays = 4100738364U,
			// Token: 0x04000433 RID: 1075
			ProtocolNetBios = 2509290958U,
			// Token: 0x04000434 RID: 1076
			CustomScheduleDailyFrom8AMTo12PMAnd1PMTo5PMAtWeekDays = 1022093144U,
			// Token: 0x04000435 RID: 1077
			ConfigurationSettingsAppSettingsError = 1817337337U,
			// Token: 0x04000436 RID: 1078
			InvalidKeyMappingTransferToNumber = 1261519073U,
			// Token: 0x04000437 RID: 1079
			CustomScheduleDaily5AM = 452901710U,
			// Token: 0x04000438 RID: 1080
			KindKeywordRssFeeds = 871198498U,
			// Token: 0x04000439 RID: 1081
			MessageStatusSuspended = 1937548848U,
			// Token: 0x0400043A RID: 1082
			ShadowMessagePreferenceLocalOnly = 462978851U,
			// Token: 0x0400043B RID: 1083
			Unavailable = 2047193656U,
			// Token: 0x0400043C RID: 1084
			RejectStatusCode = 1434991878U,
			// Token: 0x0400043D RID: 1085
			Thursday = 1760294240U,
			// Token: 0x0400043E RID: 1086
			StartingAddressAndMaskAddressFamilyMismatch = 1039830289U,
			// Token: 0x0400043F RID: 1087
			InvalidCallerIdItemTypeDefaultContactsFolder = 2802337392U,
			// Token: 0x04000440 RID: 1088
			ErrorPoliciesDefault = 3137483865U,
			// Token: 0x04000441 RID: 1089
			PublicFolderPermissionRoleContributor = 268882683U,
			// Token: 0x04000442 RID: 1090
			Weekdays = 2587222631U,
			// Token: 0x04000443 RID: 1091
			SmtpReceiveCapabilitiesAllowSubmit = 3048094658U,
			// Token: 0x04000444 RID: 1092
			PropertyNotEmptyOrNull = 3673950617U,
			// Token: 0x04000445 RID: 1093
			MAPIBlockOutlookVersionsPatternDescription = 2788726202U,
			// Token: 0x04000446 RID: 1094
			BucketVersionPatternDescription = 4263235384U,
			// Token: 0x04000447 RID: 1095
			MessageStatusNone = 1602600165U,
			// Token: 0x04000448 RID: 1096
			PublicFolderPermissionRoleAuthor = 2795839773U,
			// Token: 0x04000449 RID: 1097
			DeliveryTypeUndefined = 2292506358U,
			// Token: 0x0400044A RID: 1098
			SmtpReceiveCapabilitiesAcceptCrossForestMail = 1405683073U,
			// Token: 0x0400044B RID: 1099
			ConnectorSourceMigrated = 2397369159U,
			// Token: 0x0400044C RID: 1100
			RoutingNoMatchingConnector = 2934715437U,
			// Token: 0x0400044D RID: 1101
			QueueStatusNone = 548875607U,
			// Token: 0x0400044E RID: 1102
			ClientAccessProtocolEAC = 2067383266U,
			// Token: 0x0400044F RID: 1103
			EncryptionTypeTLS = 2929555192U,
			// Token: 0x04000450 RID: 1104
			RoleEntryNameTooShort = 3496963749U,
			// Token: 0x04000451 RID: 1105
			DuplicatesRemoved = 3076383364U,
			// Token: 0x04000452 RID: 1106
			MessageStatusPendingSuspend = 681220170U,
			// Token: 0x04000453 RID: 1107
			SmtpReceiveCapabilitiesAcceptOrgHeaders = 3785867729U,
			// Token: 0x04000454 RID: 1108
			CustomScheduleDailyAtMidnight = 2114081924U,
			// Token: 0x04000455 RID: 1109
			RecipientStatusComplete = 412393748U,
			// Token: 0x04000456 RID: 1110
			InvalidCallerIdItemTypeGALContactr = 2721491554U,
			// Token: 0x04000457 RID: 1111
			ExchangeServers = 1093243629U,
			// Token: 0x04000458 RID: 1112
			ScheduleModeNever = 1206036754U,
			// Token: 0x04000459 RID: 1113
			CustomScheduleDailyFrom11PMTo3AM = 1511350752U,
			// Token: 0x0400045A RID: 1114
			InvalidDialledNumberFormatD = 416319702U,
			// Token: 0x0400045B RID: 1115
			CustomScheduleDailyFrom1AMTo5AM = 1826938428U,
			// Token: 0x0400045C RID: 1116
			ProxyAddressPrefixShouldNotBeAllSpace = 344942096U,
			// Token: 0x0400045D RID: 1117
			CustomScheduleDaily2AM = 835983261U,
			// Token: 0x0400045E RID: 1118
			InvalidKeyMappingFindMeSecondNumber = 1899327622U,
			// Token: 0x0400045F RID: 1119
			MessageStatusReady = 202819370U,
			// Token: 0x04000460 RID: 1120
			GroupByDay = 1069174646U,
			// Token: 0x04000461 RID: 1121
			DeliveryTypeHeartbeat = 3028676818U,
			// Token: 0x04000462 RID: 1122
			FileExtensionOrSplatPatternDescription = 2470774060U,
			// Token: 0x04000463 RID: 1123
			MessageStatusActive = 3713180243U,
			// Token: 0x04000464 RID: 1124
			Monday = 3364213626U,
			// Token: 0x04000465 RID: 1125
			DeferReasonTransientAcceptedDomainsLoadFailure = 2973542840U,
			// Token: 0x04000466 RID: 1126
			ExceptionEventSourceNull = 3065954297U,
			// Token: 0x04000467 RID: 1127
			PublicFolderPermissionRoleNonEditingAuthor = 1205942262U,
			// Token: 0x04000468 RID: 1128
			CustomScheduleEveryTwoHours = 700746266U,
			// Token: 0x04000469 RID: 1129
			InvalidCallerIdItemTypePersonaContact = 2988328408U,
			// Token: 0x0400046A RID: 1130
			DeferReasonConfigUpdate = 1169266063U,
			// Token: 0x0400046B RID: 1131
			ConstraintViolationValueIsNullOrEmpty = 654440350U,
			// Token: 0x0400046C RID: 1132
			SubjectProperty = 1069535743U,
			// Token: 0x0400046D RID: 1133
			InvalidKeySelection_Zero = 1074863583U,
			// Token: 0x0400046E RID: 1134
			QueueStatusReady = 2743519022U,
			// Token: 0x0400046F RID: 1135
			ProtocolNamedPipes = 2677933048U,
			// Token: 0x04000470 RID: 1136
			ProtocolVnsSpp = 1839463316U,
			// Token: 0x04000471 RID: 1137
			KindKeywordNotes = 2247071192U,
			// Token: 0x04000472 RID: 1138
			ErrorCannotConvert = 1707192398U,
			// Token: 0x04000473 RID: 1139
			InvalidDialledNumberFormatB = 416319700U,
			// Token: 0x04000474 RID: 1140
			InvalidDialGroupEntryFormat = 1593650977U,
			// Token: 0x04000475 RID: 1141
			EapMustHaveOneEnabledPrimarySmtpAddressTemplate = 3543498202U,
			// Token: 0x04000476 RID: 1142
			FileExtensionPatternDescription = 2682420737U,
			// Token: 0x04000477 RID: 1143
			Unknown = 2846264340U,
			// Token: 0x04000478 RID: 1144
			QueueStatusRetry = 4060777377U,
			// Token: 0x04000479 RID: 1145
			Wednesday = 3452652986U,
			// Token: 0x0400047A RID: 1146
			InvalidKeyMappingFindMe = 3417603459U,
			// Token: 0x0400047B RID: 1147
			DeferReasonReroutedByStoreDriver = 417785204U,
			// Token: 0x0400047C RID: 1148
			ScheduleModeAlways = 1326579539U,
			// Token: 0x0400047D RID: 1149
			FileIsEmpty = 2583398905U,
			// Token: 0x0400047E RID: 1150
			ExceptionSerializationDataAbsent = 621231604U,
			// Token: 0x0400047F RID: 1151
			SmtpReceiveCapabilitiesAcceptOorgHeader = 3488668113U,
			// Token: 0x04000480 RID: 1152
			DsnText = 881737792U,
			// Token: 0x04000481 RID: 1153
			CustomScheduleDailyFrom9AMTo12PMAnd1PMTo6PMAtWeekDays = 269836784U,
			// Token: 0x04000482 RID: 1154
			StorageTransientFailureDuringContentConversion = 1301289463U,
			// Token: 0x04000483 RID: 1155
			MessageStatusRetry = 1500391415U,
			// Token: 0x04000484 RID: 1156
			CustomProxyAddressPrefixDisplayName = 387961094U,
			// Token: 0x04000485 RID: 1157
			CLIDPatternDescription = 1088965676U,
			// Token: 0x04000486 RID: 1158
			ClientAccessProtocolEAS = 2067383282U,
			// Token: 0x04000487 RID: 1159
			ConstraintViolationInvalidWindowsLiveIDLocalPart = 304927915U,
			// Token: 0x04000488 RID: 1160
			ExceptionParseInternalMessageId = 1914858911U,
			// Token: 0x04000489 RID: 1161
			RecipientStatusReady = 2476902678U,
			// Token: 0x0400048A RID: 1162
			ReceiveNone = 2221667633U,
			// Token: 0x0400048B RID: 1163
			UserPrincipalNamePatternDescription = 560731754U,
			// Token: 0x0400048C RID: 1164
			ToPartner = 2579867249U,
			// Token: 0x0400048D RID: 1165
			DeliveryTypeSmtpRelayToDag = 824224038U,
			// Token: 0x0400048E RID: 1166
			DeliveryTypeSmtpRelayWithinAdSiteToEdge = 1676404342U,
			// Token: 0x0400048F RID: 1167
			DeferReasonRecipientThreadLimitExceeded = 1472458119U,
			// Token: 0x04000490 RID: 1168
			Up = 1543969273U,
			// Token: 0x04000491 RID: 1169
			PreserveDSNBody = 4133244061U,
			// Token: 0x04000492 RID: 1170
			ElcScheduleInsufficientGap = 2194722870U,
			// Token: 0x04000493 RID: 1171
			ExceptionTypeNotEnhancedTimeSpanOrTimeSpan = 3501929169U,
			// Token: 0x04000494 RID: 1172
			Recipients = 986397318U,
			// Token: 0x04000495 RID: 1173
			CustomScheduleDaily4AM = 3443907091U,
			// Token: 0x04000496 RID: 1174
			ExceptionDefaultTypeMismatch = 4277209464U,
			// Token: 0x04000497 RID: 1175
			ClientAccessProtocolOWA = 2517721504U,
			// Token: 0x04000498 RID: 1176
			RecipientStatusNone = 2947517465U,
			// Token: 0x04000499 RID: 1177
			StandardTrialEdition = 553174585U,
			// Token: 0x0400049A RID: 1178
			ShadowMessagePreferenceRemoteOnly = 1980952396U,
			// Token: 0x0400049B RID: 1179
			DoNotConvert = 1065872813U,
			// Token: 0x0400049C RID: 1180
			CustomScheduleDaily10PM = 3119708351U
		}

		// Token: 0x020000D3 RID: 211
		private enum ParamIDs
		{
			// Token: 0x0400049E RID: 1182
			AddressSpaceCostOutOfRange,
			// Token: 0x0400049F RID: 1183
			ExceptionWriteOnceProperty,
			// Token: 0x040004A0 RID: 1184
			SharingPolicyDomainInvalidAction,
			// Token: 0x040004A1 RID: 1185
			InvalidCIDRLengthIPv6,
			// Token: 0x040004A2 RID: 1186
			ErrorToStringNotImplemented,
			// Token: 0x040004A3 RID: 1187
			CmdletFullNameFormatException,
			// Token: 0x040004A4 RID: 1188
			NumberFormatStringTooLong,
			// Token: 0x040004A5 RID: 1189
			ExceptionInvalidFormat,
			// Token: 0x040004A6 RID: 1190
			ErrorFileShareWitnessServerNameIsNotValidHostNameorFqdnWildcard,
			// Token: 0x040004A7 RID: 1191
			InvalidTypeArgumentException,
			// Token: 0x040004A8 RID: 1192
			PropertyTypeMismatch,
			// Token: 0x040004A9 RID: 1193
			ScriptRoleEntryNameInvalidException,
			// Token: 0x040004AA RID: 1194
			ExceptionParseNonFilterablePropertyErrorWithList,
			// Token: 0x040004AB RID: 1195
			ExceptionProtocolConnectionSettingsInvalidEncryptionType,
			// Token: 0x040004AC RID: 1196
			ErrorCannotCopyFromDifferentType,
			// Token: 0x040004AD RID: 1197
			ConfigurationSettingsPropertyNotFound2,
			// Token: 0x040004AE RID: 1198
			InvalidCIDRLengthIPv4,
			// Token: 0x040004AF RID: 1199
			ConstraintViolationValueIsNotInGivenStringArray,
			// Token: 0x040004B0 RID: 1200
			ExceptionRemoveSmtpPrimary,
			// Token: 0x040004B1 RID: 1201
			ErrorCannotConvertFromBinary,
			// Token: 0x040004B2 RID: 1202
			InvalidSmtpX509Identifier,
			// Token: 0x040004B3 RID: 1203
			DNWithBinaryFormatError,
			// Token: 0x040004B4 RID: 1204
			DialGroupNotSpecifiedOnDialPlan,
			// Token: 0x040004B5 RID: 1205
			ConstraintViolationPathLength,
			// Token: 0x040004B6 RID: 1206
			ConfigurationSettingsPropertyBadValue,
			// Token: 0x040004B7 RID: 1207
			ThrottlingPolicyStateCorrupted,
			// Token: 0x040004B8 RID: 1208
			ConstraintViolationObjectIsBelowRange,
			// Token: 0x040004B9 RID: 1209
			ExceptionVariablesNotSupported,
			// Token: 0x040004BA RID: 1210
			ConfigurationSettingsScopePropertyBadValue,
			// Token: 0x040004BB RID: 1211
			InvalidEumAddress,
			// Token: 0x040004BC RID: 1212
			ElcScheduleInvalidType,
			// Token: 0x040004BD RID: 1213
			ExceptionUnsupportedTypeConversion,
			// Token: 0x040004BE RID: 1214
			InvalidSmtpAddress,
			// Token: 0x040004BF RID: 1215
			ErrorObjectVersionReadOnly,
			// Token: 0x040004C0 RID: 1216
			ErrorFileShareWitnessServerNameIsNotValidHostNameorFqdn,
			// Token: 0x040004C1 RID: 1217
			ExceptionParseNonFilterablePropertyError,
			// Token: 0x040004C2 RID: 1218
			ExceptionUnsupportedDestinationType,
			// Token: 0x040004C3 RID: 1219
			ConstraintViolationNotValidEmailAddress,
			// Token: 0x040004C4 RID: 1220
			ErrorInputSchedulerBuilder,
			// Token: 0x040004C5 RID: 1221
			ConstraintViolationInvalidUriScheme,
			// Token: 0x040004C6 RID: 1222
			ExceptionCurrentIndexOutOfRange,
			// Token: 0x040004C7 RID: 1223
			IncludeExcludeConflict,
			// Token: 0x040004C8 RID: 1224
			ExceptionReadOnlyBecauseTooNew,
			// Token: 0x040004C9 RID: 1225
			PropertyNotACollection,
			// Token: 0x040004CA RID: 1226
			ErrorFileShareWitnessServerNameIsLocalhost,
			// Token: 0x040004CB RID: 1227
			CollectiionWithTooManyItemsFormat,
			// Token: 0x040004CC RID: 1228
			ConstraintViolationEnumValueNotDefined,
			// Token: 0x040004CD RID: 1229
			ExArgumentNullException,
			// Token: 0x040004CE RID: 1230
			ExceptionProtocolConnectionSettingsInvalidFormat,
			// Token: 0x040004CF RID: 1231
			ErrorInvalidEnumValue,
			// Token: 0x040004D0 RID: 1232
			ExceptionComparisonNotSupported,
			// Token: 0x040004D1 RID: 1233
			InvalidDialGroupEntryElementLength,
			// Token: 0x040004D2 RID: 1234
			ExceptionGeoCoordinatesWithInvalidLatitude,
			// Token: 0x040004D3 RID: 1235
			ExceptionInvalidLatitude,
			// Token: 0x040004D4 RID: 1236
			InvalidOrganizationSummaryEntryFormat,
			// Token: 0x040004D5 RID: 1237
			InvalidDateFormat,
			// Token: 0x040004D6 RID: 1238
			InvalidEumAddressTemplateFormat,
			// Token: 0x040004D7 RID: 1239
			ExceptionProtocolConnectionSettingsInvalidPort,
			// Token: 0x040004D8 RID: 1240
			ConstraintViolationObjectIsBeyondRange,
			// Token: 0x040004D9 RID: 1241
			ExceptionGeoCoordinatesWithInvalidLongitude,
			// Token: 0x040004DA RID: 1242
			ExceptionInvalidOperation,
			// Token: 0x040004DB RID: 1243
			ErrorOperationNotSupported,
			// Token: 0x040004DC RID: 1244
			ErrorIncorrectLiveIdFormat,
			// Token: 0x040004DD RID: 1245
			UnknownColumns,
			// Token: 0x040004DE RID: 1246
			InvalidTypeArgumentExceptionMultipleExceptedTypes,
			// Token: 0x040004DF RID: 1247
			ConstraintViolationIpV6NotAllowed,
			// Token: 0x040004E0 RID: 1248
			ExceptionEventCategoryNotFound,
			// Token: 0x040004E1 RID: 1249
			ErrorObjectSerialization,
			// Token: 0x040004E2 RID: 1250
			ConstraintViolationInvalidUriKind,
			// Token: 0x040004E3 RID: 1251
			ParameterNameInvalidCharException,
			// Token: 0x040004E4 RID: 1252
			ConfigurationSettingsScopePropertyNotFound,
			// Token: 0x040004E5 RID: 1253
			ConstraintViolationSecurityDescriptorContainsInheritedACEs,
			// Token: 0x040004E6 RID: 1254
			ConstraintViolationInvalidIntRange,
			// Token: 0x040004E7 RID: 1255
			ConstraintViolationValueIsNotAllowed,
			// Token: 0x040004E8 RID: 1256
			ConstraintViolationMalformedExtensionValuePair,
			// Token: 0x040004E9 RID: 1257
			ElcScheduleInvalidIntervals,
			// Token: 0x040004EA RID: 1258
			ExceptionUsingInvalidAddress,
			// Token: 0x040004EB RID: 1259
			InvalidNumber,
			// Token: 0x040004EC RID: 1260
			ErrorCannotSaveBecauseTooNew,
			// Token: 0x040004ED RID: 1261
			ConfigurationSettingsScopePropertyFailedValidation,
			// Token: 0x040004EE RID: 1262
			InvalidDomainInSmtpX509Identifier,
			// Token: 0x040004EF RID: 1263
			InvalidAddressSpaceCostFormat,
			// Token: 0x040004F0 RID: 1264
			ServicePlanFeatureCheckFailed,
			// Token: 0x040004F1 RID: 1265
			InvalidSMTPAddressTemplateFormat,
			// Token: 0x040004F2 RID: 1266
			InvalidOrganizationSummaryEntryValue,
			// Token: 0x040004F3 RID: 1267
			DataNotCloneable,
			// Token: 0x040004F4 RID: 1268
			ExceptionBitMaskNotSupported,
			// Token: 0x040004F5 RID: 1269
			ConstraintViolationStringContainsInvalidCharacters2,
			// Token: 0x040004F6 RID: 1270
			InvalidIPAddressMask,
			// Token: 0x040004F7 RID: 1271
			ConstraintViolationStringDoesNotMatchRegularExpression,
			// Token: 0x040004F8 RID: 1272
			ScheduleDateInvalid,
			// Token: 0x040004F9 RID: 1273
			InvalidSmtpReceiveDomainCapabilities,
			// Token: 0x040004FA RID: 1274
			ExceptionCannotResolveOperation,
			// Token: 0x040004FB RID: 1275
			InvalidConnectedDomainFormat,
			// Token: 0x040004FC RID: 1276
			InvalidOrganizationSummaryEntryKey,
			// Token: 0x040004FD RID: 1277
			ExceptionEndPointPortOutOfRange,
			// Token: 0x040004FE RID: 1278
			ExceptionGeoCoordinatesWithInvalidAltitude,
			// Token: 0x040004FF RID: 1279
			ConstraintViolationEnumValueNotAllowed,
			// Token: 0x04000500 RID: 1280
			SharingPolicyDomainInvalidDomain,
			// Token: 0x04000501 RID: 1281
			UnsupportServerEdition,
			// Token: 0x04000502 RID: 1282
			ConstraintViolationByteArrayLengthTooLong,
			// Token: 0x04000503 RID: 1283
			InvalidCIDRLength,
			// Token: 0x04000504 RID: 1284
			ErrorFileShareWitnessServerNameCannotConvert,
			// Token: 0x04000505 RID: 1285
			ExceptionFormatNotCorrect,
			// Token: 0x04000506 RID: 1286
			ConstraintViolationByteArrayLengthTooShort,
			// Token: 0x04000507 RID: 1287
			InvalidX400Domain,
			// Token: 0x04000508 RID: 1288
			InvalidDialGroupEntryElementFormat,
			// Token: 0x04000509 RID: 1289
			ExceptionPropertyTooNew,
			// Token: 0x0400050A RID: 1290
			RequiredColumnMissing,
			// Token: 0x0400050B RID: 1291
			ExceptionQueueIdentityCompare,
			// Token: 0x0400050C RID: 1292
			ErrorIncorrectWindowsLiveIdFormat,
			// Token: 0x0400050D RID: 1293
			InvalidIPRange,
			// Token: 0x0400050E RID: 1294
			ExceptionInvalidMeumAddress,
			// Token: 0x0400050F RID: 1295
			ErrorSerialNumberFormatError,
			// Token: 0x04000510 RID: 1296
			ExceptionProtocolConnectionSettingsInvalidHostname,
			// Token: 0x04000511 RID: 1297
			ErrorCannotConvertToBinary,
			// Token: 0x04000512 RID: 1298
			ProxyAddressTemplateEmptyPrefixOrValue,
			// Token: 0x04000513 RID: 1299
			InvalidAddressSpaceType,
			// Token: 0x04000514 RID: 1300
			ErrorFileShareWitnessServerNameMustNotBeIP,
			// Token: 0x04000515 RID: 1301
			StarDomainNotAllowed,
			// Token: 0x04000516 RID: 1302
			ErrorValueAlreadyPresent,
			// Token: 0x04000517 RID: 1303
			ErrorNonGeneric,
			// Token: 0x04000518 RID: 1304
			Int32ParsableStringConstraintViolation,
			// Token: 0x04000519 RID: 1305
			ExceptionRemoveEumPrimary,
			// Token: 0x0400051A RID: 1306
			ConstraintViolationIpRangeNotAllowed,
			// Token: 0x0400051B RID: 1307
			ErrorOutOfRange,
			// Token: 0x0400051C RID: 1308
			DialGroupNotSpecifiedOnDialPlanB,
			// Token: 0x0400051D RID: 1309
			SharingPolicyDomainInvalidActionForDomain,
			// Token: 0x0400051E RID: 1310
			InvalidTlsCertificateName,
			// Token: 0x0400051F RID: 1311
			ErrorInputOfScheduleMustExclusive,
			// Token: 0x04000520 RID: 1312
			ExceptionCannotSetDifferentType,
			// Token: 0x04000521 RID: 1313
			ErrorUnknownOperation,
			// Token: 0x04000522 RID: 1314
			ExceptionParseError,
			// Token: 0x04000523 RID: 1315
			LinkedPartnerGroupInformationInvalidParameter,
			// Token: 0x04000524 RID: 1316
			SharingPolicyDomainInvalidActionForAnonymous,
			// Token: 0x04000525 RID: 1317
			ConstraintViolationDontMatchUnit,
			// Token: 0x04000526 RID: 1318
			ClientAccessRulesBlockedConnection,
			// Token: 0x04000527 RID: 1319
			ErrorLengthOfExDataTimeByteArray,
			// Token: 0x04000528 RID: 1320
			ConfigurationSettingsScopePropertyNotFound2,
			// Token: 0x04000529 RID: 1321
			PropertyName,
			// Token: 0x0400052A RID: 1322
			InvalidCharInString,
			// Token: 0x0400052B RID: 1323
			InvalidX400AddressSpace,
			// Token: 0x0400052C RID: 1324
			ConstraintViolationStringLengthTooShort,
			// Token: 0x0400052D RID: 1325
			InvalidIPAddressOrHostNameInSmartHost,
			// Token: 0x0400052E RID: 1326
			SmtpResponseConstraintViolation,
			// Token: 0x0400052F RID: 1327
			ExceptionEndPointMissingSeparator,
			// Token: 0x04000530 RID: 1328
			ExceptionFormatInvalid,
			// Token: 0x04000531 RID: 1329
			IncludeExcludeInvalid,
			// Token: 0x04000532 RID: 1330
			ErrorCannotConvertNull,
			// Token: 0x04000533 RID: 1331
			InvalidScopedAddressSpace,
			// Token: 0x04000534 RID: 1332
			ErrorConversionFailedWithException,
			// Token: 0x04000535 RID: 1333
			DuplicateParameterException,
			// Token: 0x04000536 RID: 1334
			ExceptionInvalidEumAddress,
			// Token: 0x04000537 RID: 1335
			DuplicatedColumn,
			// Token: 0x04000538 RID: 1336
			ConstraintViolationValueIsDisallowed,
			// Token: 0x04000539 RID: 1337
			ExceptionEndPointInvalidPort,
			// Token: 0x0400053A RID: 1338
			ErrorReadOnlyCauseObject,
			// Token: 0x0400053B RID: 1339
			ErrorCannotConvertFromString,
			// Token: 0x0400053C RID: 1340
			ConstraintNoTrailingSpecificCharacter,
			// Token: 0x0400053D RID: 1341
			DependencyCheckFailed,
			// Token: 0x0400053E RID: 1342
			ConfigurationSettingsPropertyNotFound,
			// Token: 0x0400053F RID: 1343
			ErrorCannotConvertToString,
			// Token: 0x04000540 RID: 1344
			ServicePlanSchemaCheckFailed,
			// Token: 0x04000541 RID: 1345
			ApplicationPermissionRoleEntryParameterNotEmptyException,
			// Token: 0x04000542 RID: 1346
			SharingPolicyDomainInvalid,
			// Token: 0x04000543 RID: 1347
			ExceptionValueOverflow,
			// Token: 0x04000544 RID: 1348
			ErrorToBinaryNotImplemented,
			// Token: 0x04000545 RID: 1349
			BadEnumValue,
			// Token: 0x04000546 RID: 1350
			ExceptionInvalidServerName,
			// Token: 0x04000547 RID: 1351
			ExceptionInvalidSmtpAddress,
			// Token: 0x04000548 RID: 1352
			ConstraintViolationStringContainsInvalidCharacters,
			// Token: 0x04000549 RID: 1353
			InvalidIntRangeArgument,
			// Token: 0x0400054A RID: 1354
			WrongNumberOfColumns,
			// Token: 0x0400054B RID: 1355
			FilterOnlyAttributes,
			// Token: 0x0400054C RID: 1356
			InvalidAlternateMailboxString,
			// Token: 0x0400054D RID: 1357
			ConstraintViolationValueOutOfRange,
			// Token: 0x0400054E RID: 1358
			ExceptionInvalidLongitude,
			// Token: 0x0400054F RID: 1359
			ExceptionGeoCoordinatesWithWrongFormat,
			// Token: 0x04000550 RID: 1360
			RoleEntryNameInvalidException,
			// Token: 0x04000551 RID: 1361
			ConstraintViolationStringLengthTooLong,
			// Token: 0x04000552 RID: 1362
			ExceptionUnsupportedSourceType,
			// Token: 0x04000553 RID: 1363
			InvalidRoleEntryType,
			// Token: 0x04000554 RID: 1364
			ConfigurationSettingsPropertyBadType,
			// Token: 0x04000555 RID: 1365
			InvalidSmtpDomainName,
			// Token: 0x04000556 RID: 1366
			TooManyRows,
			// Token: 0x04000557 RID: 1367
			InvalidHostname,
			// Token: 0x04000558 RID: 1368
			InvalidX509IdentifierFormat,
			// Token: 0x04000559 RID: 1369
			ConstraintViolationStringIsNotValidCultureInfo,
			// Token: 0x0400055A RID: 1370
			ErrorContainsOutOfRange,
			// Token: 0x0400055B RID: 1371
			ErrorConversionFailed,
			// Token: 0x0400055C RID: 1372
			ExceptionInvalidFilterOperator,
			// Token: 0x0400055D RID: 1373
			ErrorInvalidScheduleType,
			// Token: 0x0400055E RID: 1374
			ErrorDSNCultureInput,
			// Token: 0x0400055F RID: 1375
			ExceptionEmptyPrefixEum,
			// Token: 0x04000560 RID: 1376
			ErrorReadOnlyCauseProperty,
			// Token: 0x04000561 RID: 1377
			ErrorOrignalMultiValuedProperty,
			// Token: 0x04000562 RID: 1378
			InvalidIPAddressFormat,
			// Token: 0x04000563 RID: 1379
			ExceptionEventSourceNotFound,
			// Token: 0x04000564 RID: 1380
			InvalidNumberFormat,
			// Token: 0x04000565 RID: 1381
			InvalidIPRangeFormat,
			// Token: 0x04000566 RID: 1382
			ConstraintViolationNotValidDomain,
			// Token: 0x04000567 RID: 1383
			AdminAuditLogInvalidParameterOrModifiedProperty,
			// Token: 0x04000568 RID: 1384
			ConfigurationSettingsPropertyFailedValidation,
			// Token: 0x04000569 RID: 1385
			WebServiceRoleEntryParameterNotEmptyException,
			// Token: 0x0400056A RID: 1386
			ConstraintViolationStringDoesNotContainNonWhitespaceCharacter,
			// Token: 0x0400056B RID: 1387
			ExceptionUnsupportedDataFormat,
			// Token: 0x0400056C RID: 1388
			ProhibitedColumnPresent,
			// Token: 0x0400056D RID: 1389
			ConstraintViolationStringDoesContainsNonASCIICharacter,
			// Token: 0x0400056E RID: 1390
			ExceptionEmptyPrefix,
			// Token: 0x0400056F RID: 1391
			ErrorMvpNotImplemented,
			// Token: 0x04000570 RID: 1392
			SnapinNameInvalidCharException,
			// Token: 0x04000571 RID: 1393
			ExceptionEndPointInvalidIPAddress
		}
	}
}
