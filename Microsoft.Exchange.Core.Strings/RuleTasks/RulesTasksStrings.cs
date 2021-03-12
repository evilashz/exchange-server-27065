using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core.RuleTasks
{
	// Token: 0x02000027 RID: 39
	public static class RulesTasksStrings
	{
		// Token: 0x060003DF RID: 991 RVA: 0x0000D4DC File Offset: 0x0000B6DC
		static RulesTasksStrings()
		{
			RulesTasksStrings.stringIDs.Add(3817895306U, "LinkedPredicateHeaderMatches");
			RulesTasksStrings.stringIDs.Add(3690383901U, "SetAuditSeverityDisplayName");
			RulesTasksStrings.stringIDs.Add(856583401U, "ADAttributeOtherHomePhoneNumber");
			RulesTasksStrings.stringIDs.Add(607657692U, "JournalingParameterErrorFullReportWithoutGcc");
			RulesTasksStrings.stringIDs.Add(3705591125U, "RecipientTypeDescription");
			RulesTasksStrings.stringIDs.Add(1840928351U, "EvaluatedUserDisplayName");
			RulesTasksStrings.stringIDs.Add(1484331716U, "LinkedPredicateHeaderContains");
			RulesTasksStrings.stringIDs.Add(2129610537U, "MessageTypeOof");
			RulesTasksStrings.stringIDs.Add(862345881U, "ImportanceDisplayName");
			RulesTasksStrings.stringIDs.Add(887494032U, "DlpPolicyModeIsOverridenByModeParameter");
			RulesTasksStrings.stringIDs.Add(3832906569U, "LinkedActionApplyHtmlDisclaimer");
			RulesTasksStrings.stringIDs.Add(2799440992U, "RejectMessageParameterWillBeIgnored");
			RulesTasksStrings.stringIDs.Add(4206835639U, "LinkedPredicateRecipientAddressContainsWords");
			RulesTasksStrings.stringIDs.Add(3386546304U, "AttachmentSizeDescription");
			RulesTasksStrings.stringIDs.Add(1966725662U, "LinkedPredicateRecipientAddressMatchesPatternsException");
			RulesTasksStrings.stringIDs.Add(2655586213U, "AttributeValueDescription");
			RulesTasksStrings.stringIDs.Add(3729435602U, "LinkedPredicateAttachmentMatchesPatternsException");
			RulesTasksStrings.stringIDs.Add(694309943U, "LinkedActionLogEvent");
			RulesTasksStrings.stringIDs.Add(3534737571U, "LinkedPredicateAnyOfToHeader");
			RulesTasksStrings.stringIDs.Add(3304155086U, "EvaluatedUserDescription");
			RulesTasksStrings.stringIDs.Add(1377545167U, "ADAttributeCustomAttribute1");
			RulesTasksStrings.stringIDs.Add(996206651U, "LinkedPredicateAttachmentPropertyContainsWordsException");
			RulesTasksStrings.stringIDs.Add(2978999437U, "LinkedActionApplyClassification");
			RulesTasksStrings.stringIDs.Add(1938376749U, "LinkedActionSetScl");
			RulesTasksStrings.stringIDs.Add(2156000375U, "LinkedPredicateAnyOfCcHeaderException");
			RulesTasksStrings.stringIDs.Add(207684870U, "ArgumentNotSet");
			RulesTasksStrings.stringIDs.Add(2267758972U, "InvalidRuleName");
			RulesTasksStrings.stringIDs.Add(5978988U, "RuleDescriptionDisclaimerRejectFallback");
			RulesTasksStrings.stringIDs.Add(2498488782U, "LinkedPredicateFromAddressContainsException");
			RulesTasksStrings.stringIDs.Add(3718292884U, "LinkedPredicateAttachmentNameMatchesException");
			RulesTasksStrings.stringIDs.Add(1016721882U, "ADAttributeLastName");
			RulesTasksStrings.stringIDs.Add(151284935U, "GenerateNotificationDisplayName");
			RulesTasksStrings.stringIDs.Add(281219183U, "RuleDescriptionNotifySenderNotifyOnly");
			RulesTasksStrings.stringIDs.Add(3600528589U, "ADAttributeCountry");
			RulesTasksStrings.stringIDs.Add(3542666061U, "MessageHeaderDisplayName");
			RulesTasksStrings.stringIDs.Add(4246027971U, "LinkedPredicateAttachmentMatchesPatterns");
			RulesTasksStrings.stringIDs.Add(3208048360U, "ListsDisplayName");
			RulesTasksStrings.stringIDs.Add(2328530086U, "FallbackIgnore");
			RulesTasksStrings.stringIDs.Add(77980785U, "LinkedPredicateSenderIpRangesException");
			RulesTasksStrings.stringIDs.Add(763738384U, "AttributeValueDisplayName");
			RulesTasksStrings.stringIDs.Add(2380701036U, "LinkedPredicateRecipientDomainIsException");
			RulesTasksStrings.stringIDs.Add(1655133361U, "InboxRuleDescriptionHasAttachment");
			RulesTasksStrings.stringIDs.Add(2340468721U, "RuleDescriptionHasSenderOverride");
			RulesTasksStrings.stringIDs.Add(2538565266U, "LinkedPredicateRecipientInSenderListException");
			RulesTasksStrings.stringIDs.Add(705865031U, "LinkedPredicateAttachmentExtensionMatchesWordsException");
			RulesTasksStrings.stringIDs.Add(3850060255U, "ImportanceNormal");
			RulesTasksStrings.stringIDs.Add(3099194133U, "LinkedActionCopyTo");
			RulesTasksStrings.stringIDs.Add(2306965519U, "ListsDescription");
			RulesTasksStrings.stringIDs.Add(1130765528U, "TextPatternsDescription");
			RulesTasksStrings.stringIDs.Add(1191033945U, "NewRuleSyncAcrossDifferentVersionsNeeded");
			RulesTasksStrings.stringIDs.Add(3108375176U, "RmsTemplateDescription");
			RulesTasksStrings.stringIDs.Add(4223753465U, "LinkedPredicateRecipientInSenderList");
			RulesTasksStrings.stringIDs.Add(2468414724U, "ADAttributeInitials");
			RulesTasksStrings.stringIDs.Add(1331793023U, "LinkedPredicateHeaderContainsException");
			RulesTasksStrings.stringIDs.Add(3721269126U, "LinkedPredicateContentCharacterSetContainsWords");
			RulesTasksStrings.stringIDs.Add(1697423233U, "MessageTypeDescription");
			RulesTasksStrings.stringIDs.Add(3882899654U, "ADAttributeState");
			RulesTasksStrings.stringIDs.Add(3643356345U, "FromScopeDisplayName");
			RulesTasksStrings.stringIDs.Add(2812686069U, "InboxRuleDescriptionAnd");
			RulesTasksStrings.stringIDs.Add(1377545174U, "ADAttributeCustomAttribute8");
			RulesTasksStrings.stringIDs.Add(1720532765U, "LinkedPredicateSubjectMatches");
			RulesTasksStrings.stringIDs.Add(2041595767U, "MessageTypeAutoForward");
			RulesTasksStrings.stringIDs.Add(680782013U, "MessageTypeReadReceipt");
			RulesTasksStrings.stringIDs.Add(431368248U, "LinkedActionBlindCopyTo");
			RulesTasksStrings.stringIDs.Add(2112278218U, "LinkedPredicateSentToScopeException");
			RulesTasksStrings.stringIDs.Add(3773694392U, "LinkedActionStopRuleProcessing");
			RulesTasksStrings.stringIDs.Add(3956509743U, "LinkedPredicateAnyOfToCcHeader");
			RulesTasksStrings.stringIDs.Add(3369692937U, "LinkedPredicateHasSenderOverride");
			RulesTasksStrings.stringIDs.Add(2611996929U, "RedirectRecipientType");
			RulesTasksStrings.stringIDs.Add(1231044387U, "FallbackActionDisplayName");
			RulesTasksStrings.stringIDs.Add(161711511U, "NoAction");
			RulesTasksStrings.stringIDs.Add(3921523715U, "InboxRuleDescriptionFlaggedForAnyAction");
			RulesTasksStrings.stringIDs.Add(1645493202U, "LinkedActionSetHeader");
			RulesTasksStrings.stringIDs.Add(3850705779U, "SenderIpRangesDisplayName");
			RulesTasksStrings.stringIDs.Add(2375893868U, "LinkedPredicateAttachmentIsUnsupported");
			RulesTasksStrings.stringIDs.Add(3050431750U, "ADAttributeName");
			RulesTasksStrings.stringIDs.Add(3095013874U, "EventMessageDisplayName");
			RulesTasksStrings.stringIDs.Add(1080850367U, "IncidentReportOriginalMailnDisplayName");
			RulesTasksStrings.stringIDs.Add(3689464497U, "ADAttributeOtherFaxNumber");
			RulesTasksStrings.stringIDs.Add(3115100581U, "RejectUnlessExplicitOverrideActionType");
			RulesTasksStrings.stringIDs.Add(4231516709U, "RuleDescriptionHasNoClassification");
			RulesTasksStrings.stringIDs.Add(130230884U, "LinkedPredicateAnyOfRecipientAddressMatchesException");
			RulesTasksStrings.stringIDs.Add(3856180838U, "MessageTypeDisplayName");
			RulesTasksStrings.stringIDs.Add(3144351139U, "FallbackReject");
			RulesTasksStrings.stringIDs.Add(3374360575U, "ADAttributeCustomAttribute10");
			RulesTasksStrings.stringIDs.Add(4289093673U, "ADAttributeEmail");
			RulesTasksStrings.stringIDs.Add(1377545163U, "ADAttributeCustomAttribute5");
			RulesTasksStrings.stringIDs.Add(1301491835U, "LinkedPredicateSubjectOrBodyMatchesException");
			RulesTasksStrings.stringIDs.Add(2591548322U, "InboxRuleDescriptionMyNameInToBox");
			RulesTasksStrings.stringIDs.Add(4199979286U, "ToRecipientType");
			RulesTasksStrings.stringIDs.Add(1321315595U, "LinkedPredicateAttachmentContainsWords");
			RulesTasksStrings.stringIDs.Add(2934581752U, "LinkedActionRightsProtectMessage");
			RulesTasksStrings.stringIDs.Add(3527153583U, "LinkedActionSetAuditSeverity");
			RulesTasksStrings.stringIDs.Add(1125919747U, "LinkedPredicateBetweenMemberOf");
			RulesTasksStrings.stringIDs.Add(1050054139U, "RemoveRuleSyncAcrossDifferentVersionsNeeded");
			RulesTasksStrings.stringIDs.Add(2569693958U, "ADAttributeDisplayName");
			RulesTasksStrings.stringIDs.Add(354503089U, "LinkedPredicateSentToScope");
			RulesTasksStrings.stringIDs.Add(3455130960U, "DisclaimerLocationDescription");
			RulesTasksStrings.stringIDs.Add(1316941123U, "FromAddressesDisplayName");
			RulesTasksStrings.stringIDs.Add(1648356515U, "LinkedActionRejectMessage");
			RulesTasksStrings.stringIDs.Add(328892189U, "LinkedPredicateSenderAttributeMatchesException");
			RulesTasksStrings.stringIDs.Add(1857695339U, "LinkedActionRouteMessageOutboundRequireTls");
			RulesTasksStrings.stringIDs.Add(3266871523U, "LinkedPredicateAttachmentNameMatches");
			RulesTasksStrings.stringIDs.Add(3211626450U, "HeaderValueDescription");
			RulesTasksStrings.stringIDs.Add(4103233806U, "RecipientTypeDisplayName");
			RulesTasksStrings.stringIDs.Add(2869706848U, "LinkedPredicateFromMemberOfException");
			RulesTasksStrings.stringIDs.Add(1377545169U, "ADAttributeCustomAttribute3");
			RulesTasksStrings.stringIDs.Add(3850073087U, "ADAttributePagerNumber");
			RulesTasksStrings.stringIDs.Add(117825870U, "MessageTypeVoicemail");
			RulesTasksStrings.stringIDs.Add(903770574U, "LinkedPredicateWithImportance");
			RulesTasksStrings.stringIDs.Add(2749090792U, "GenerateNotificationDescription");
			RulesTasksStrings.stringIDs.Add(2634964433U, "ADAttributeTitle");
			RulesTasksStrings.stringIDs.Add(517035719U, "TextPatternsDisplayName");
			RulesTasksStrings.stringIDs.Add(1086713105U, "LinkedPredicateHeaderMatchesException");
			RulesTasksStrings.stringIDs.Add(240566931U, "EvaluationDisplayName");
			RulesTasksStrings.stringIDs.Add(37980935U, "ToDLAddressDescription");
			RulesTasksStrings.stringIDs.Add(1419022259U, "DuplicateDataClassificationSpecified");
			RulesTasksStrings.stringIDs.Add(4270036386U, "RuleDescriptionRouteMessageOutboundRequireTls");
			RulesTasksStrings.stringIDs.Add(3918497079U, "EvaluationNotEqual");
			RulesTasksStrings.stringIDs.Add(1318242726U, "StatusCodeDisplayName");
			RulesTasksStrings.stringIDs.Add(4178557944U, "ConnectorNameDescription");
			RulesTasksStrings.stringIDs.Add(2828094232U, "RuleDescriptionAndDelimiter");
			RulesTasksStrings.stringIDs.Add(1048761747U, "ADAttributeCustomAttribute14");
			RulesTasksStrings.stringIDs.Add(1514251090U, "LinkedActionApplyOME");
			RulesTasksStrings.stringIDs.Add(4137481806U, "ADAttributePhoneNumber");
			RulesTasksStrings.stringIDs.Add(1927573801U, "ADAttributeOffice");
			RulesTasksStrings.stringIDs.Add(1784539898U, "LinkedPredicateSenderInRecipientListException");
			RulesTasksStrings.stringIDs.Add(947201504U, "RuleDescriptionDisconnect");
			RulesTasksStrings.stringIDs.Add(250901884U, "RuleDescriptionOrDelimiter");
			RulesTasksStrings.stringIDs.Add(162518937U, "LinkedActionSmtpRejectMessage");
			RulesTasksStrings.stringIDs.Add(1218766792U, "LinkedPredicateAnyOfToHeaderMemberOf");
			RulesTasksStrings.stringIDs.Add(3733737272U, "ErrorInboxRuleMustHaveActions");
			RulesTasksStrings.stringIDs.Add(4170515100U, "QuarantineActionNotAvailable");
			RulesTasksStrings.stringIDs.Add(1484034422U, "LinkedPredicateAttachmentPropertyContainsWords");
			RulesTasksStrings.stringIDs.Add(2351889904U, "LinkedPredicateAnyOfToHeaderException");
			RulesTasksStrings.stringIDs.Add(3617076017U, "EventMessageDescription");
			RulesTasksStrings.stringIDs.Add(2990680076U, "LinkedPredicateAnyOfRecipientAddressContainsException");
			RulesTasksStrings.stringIDs.Add(1857974425U, "RmsTemplateDisplayName");
			RulesTasksStrings.stringIDs.Add(999677583U, "LinkedPredicateRecipientAttributeContainsException");
			RulesTasksStrings.stringIDs.Add(2463949652U, "LinkedPredicateHasClassification");
			RulesTasksStrings.stringIDs.Add(1025350601U, "InboxRuleDescriptionSentOnlyToMe");
			RulesTasksStrings.stringIDs.Add(3905367638U, "LinkedPredicateAttachmentIsPasswordProtected");
			RulesTasksStrings.stringIDs.Add(4055862009U, "PromptToUpgradeRulesFormat");
			RulesTasksStrings.stringIDs.Add(2147095286U, "NotifySenderActionRequiresMcdcPredicate");
			RulesTasksStrings.stringIDs.Add(1853592207U, "LinkedActionGenerateNotificationAction");
			RulesTasksStrings.stringIDs.Add(3677381679U, "ClassificationDisplayName");
			RulesTasksStrings.stringIDs.Add(104177927U, "LinkedActionPrependSubject");
			RulesTasksStrings.stringIDs.Add(2002903510U, "ADAttributeStreet");
			RulesTasksStrings.stringIDs.Add(3208826121U, "FromDLAddressDisplayName");
			RulesTasksStrings.stringIDs.Add(1121933765U, "LinkedPredicateMessageTypeMatchesException");
			RulesTasksStrings.stringIDs.Add(2614845688U, "ADAttributeCustomAttribute15");
			RulesTasksStrings.stringIDs.Add(3575782782U, "RuleDescriptionQuarantine");
			RulesTasksStrings.stringIDs.Add(1485874038U, "MissingDataClassificationsParameter");
			RulesTasksStrings.stringIDs.Add(2891590424U, "LinkedPredicateHasNoClassificationException");
			RulesTasksStrings.stringIDs.Add(3816854736U, "LinkedPredicateFromScope");
			RulesTasksStrings.stringIDs.Add(25634710U, "ManagementRelationshipManager");
			RulesTasksStrings.stringIDs.Add(2463135270U, "LinkedPredicateSubjectMatchesException");
			RulesTasksStrings.stringIDs.Add(3105847291U, "LinkedPredicateSentTo");
			RulesTasksStrings.stringIDs.Add(1943465529U, "LinkedPredicateAnyOfCcHeaderMemberOf");
			RulesTasksStrings.stringIDs.Add(2075495475U, "InboxRuleDescriptionFolderNotFound");
			RulesTasksStrings.stringIDs.Add(3967326684U, "PrefixDescription");
			RulesTasksStrings.stringIDs.Add(2509095413U, "EvaluatedUserSender");
			RulesTasksStrings.stringIDs.Add(1886943840U, "RuleDescriptionDeleteMessage");
			RulesTasksStrings.stringIDs.Add(2588281890U, "LinkedPredicateHasSenderOverrideException");
			RulesTasksStrings.stringIDs.Add(4011098093U, "SubTypeDisplayName");
			RulesTasksStrings.stringIDs.Add(929006655U, "ParameterVersionMismatch");
			RulesTasksStrings.stringIDs.Add(2191228215U, "LinkedPredicateManagementRelationship");
			RulesTasksStrings.stringIDs.Add(2135540736U, "LinkedActionRouteMessageOutboundConnector");
			RulesTasksStrings.stringIDs.Add(3722738631U, "LinkedActionModerateMessageByUser");
			RulesTasksStrings.stringIDs.Add(3172147964U, "LinkedActionAddToRecipient");
			RulesTasksStrings.stringIDs.Add(1516175560U, "IncidentReportContentDisplayName");
			RulesTasksStrings.stringIDs.Add(2942456627U, "LinkedPredicateSubjectContains");
			RulesTasksStrings.stringIDs.Add(790749074U, "LinkedPredicateRecipientAddressContainsWordsException");
			RulesTasksStrings.stringIDs.Add(3262572344U, "FallbackWrap");
			RulesTasksStrings.stringIDs.Add(1377545162U, "ADAttributeCustomAttribute4");
			RulesTasksStrings.stringIDs.Add(699378618U, "ADAttributeEvaluationTypeContains");
			RulesTasksStrings.stringIDs.Add(2876513949U, "ADAttributeEvaluationTypeDescription");
			RulesTasksStrings.stringIDs.Add(3627783386U, "InvalidPredicate");
			RulesTasksStrings.stringIDs.Add(3367615085U, "ADAttributeDepartment");
			RulesTasksStrings.stringIDs.Add(4233384325U, "RuleStateDisabled");
			RulesTasksStrings.stringIDs.Add(1474730269U, "LinkedPredicateSclOverException");
			RulesTasksStrings.stringIDs.Add(4156341045U, "ConnectorNameDisplayName");
			RulesTasksStrings.stringIDs.Add(1640312644U, "LinkedPredicateSentToException");
			RulesTasksStrings.stringIDs.Add(975362769U, "EnhancedStatusCodeDescription");
			RulesTasksStrings.stringIDs.Add(3680228519U, "LinkedActionDisconnect");
			RulesTasksStrings.stringIDs.Add(4260106383U, "ADAttributePOBox");
			RulesTasksStrings.stringIDs.Add(2557875829U, "RuleDescriptionRemoveOME");
			RulesTasksStrings.stringIDs.Add(4072748617U, "MessageTypeApprovalRequest");
			RulesTasksStrings.stringIDs.Add(2182511137U, "ADAttributeFaxNumber");
			RulesTasksStrings.stringIDs.Add(1453329754U, "ErrorAccessingTransportSettings");
			RulesTasksStrings.stringIDs.Add(3459736224U, "EvaluationEqual");
			RulesTasksStrings.stringIDs.Add(1008100316U, "LinkedPredicateRecipientAttributeContains");
			RulesTasksStrings.stringIDs.Add(1994464450U, "LinkedPredicateAttachmentSizeOver");
			RulesTasksStrings.stringIDs.Add(3312695260U, "LinkedPredicateFrom");
			RulesTasksStrings.stringIDs.Add(583131402U, "LinkedPredicateAnyOfToCcHeaderException");
			RulesTasksStrings.stringIDs.Add(2016017520U, "LinkedPredicateSubjectOrBodyContains");
			RulesTasksStrings.stringIDs.Add(2966559302U, "LinkedPredicateAttachmentContainsWordsException");
			RulesTasksStrings.stringIDs.Add(2052587897U, "InboxRuleDescriptionFlaggedForNoResponse");
			RulesTasksStrings.stringIDs.Add(3353805215U, "StatusCodeDescription");
			RulesTasksStrings.stringIDs.Add(1663659467U, "LinkedPredicateFromScopeException");
			RulesTasksStrings.stringIDs.Add(1157968148U, "LinkedPredicateAttachmentProcessingLimitExceeded");
			RulesTasksStrings.stringIDs.Add(428619956U, "ManagementRelationshipDirectReport");
			RulesTasksStrings.stringIDs.Add(381216251U, "ADAttributeZipCode");
			RulesTasksStrings.stringIDs.Add(4146681835U, "SetRuleSyncAcrossDifferentVersionsNeeded");
			RulesTasksStrings.stringIDs.Add(2075192559U, "IncidentReportContentDescription");
			RulesTasksStrings.stringIDs.Add(1604244669U, "InvalidIncidentReportOriginalMail");
			RulesTasksStrings.stringIDs.Add(2457055209U, "LinkedPredicateRecipientAttributeMatchesException");
			RulesTasksStrings.stringIDs.Add(2083948085U, "ClientAccessRulesAuthenticationTypeInvalid");
			RulesTasksStrings.stringIDs.Add(1419241760U, "ClassificationDescription");
			RulesTasksStrings.stringIDs.Add(6731194U, "ReportDestinationDescription");
			RulesTasksStrings.stringIDs.Add(2492349756U, "LinkedPredicateSentToMemberOf");
			RulesTasksStrings.stringIDs.Add(1882052979U, "ToScopeDescription");
			RulesTasksStrings.stringIDs.Add(3163284624U, "WordsDisplayName");
			RulesTasksStrings.stringIDs.Add(3682029584U, "ModerateActionMustBeTheOnlyAction");
			RulesTasksStrings.stringIDs.Add(2063369946U, "InvalidRejectEnhancedStatus");
			RulesTasksStrings.stringIDs.Add(2986926906U, "ADAttributeFirstName");
			RulesTasksStrings.stringIDs.Add(3927787984U, "LinkedPredicateAnyOfCcHeader");
			RulesTasksStrings.stringIDs.Add(3247824756U, "ErrorInvalidCharException");
			RulesTasksStrings.stringIDs.Add(1871050104U, "LinkedPredicateMessageSizeOver");
			RulesTasksStrings.stringIDs.Add(312772195U, "LinkedPredicateAttachmentProcessingLimitExceededException");
			RulesTasksStrings.stringIDs.Add(278098341U, "ManagementRelationshipDescription");
			RulesTasksStrings.stringIDs.Add(3355891993U, "LinkedPredicateSenderDomainIs");
			RulesTasksStrings.stringIDs.Add(2795331228U, "InternalUser");
			RulesTasksStrings.stringIDs.Add(2609648925U, "InvalidRmsTemplate");
			RulesTasksStrings.stringIDs.Add(3500719009U, "HeaderValueDisplayName");
			RulesTasksStrings.stringIDs.Add(2141139127U, "EdgeParameterNotValidOnHubRole");
			RulesTasksStrings.stringIDs.Add(1798370525U, "BccRecipientType");
			RulesTasksStrings.stringIDs.Add(2736707353U, "RejectUnlessFalsePositiveOverrideActionType");
			RulesTasksStrings.stringIDs.Add(4141158958U, "LinkedPredicateSenderAttributeContains");
			RulesTasksStrings.stringIDs.Add(2139768671U, "LinkedPredicateAttachmentHasExecutableContentPredicate");
			RulesTasksStrings.stringIDs.Add(1691025395U, "PromptToRemoveLogEventAction");
			RulesTasksStrings.stringIDs.Add(2412654301U, "ToAddressesDescription");
			RulesTasksStrings.stringIDs.Add(3606274629U, "MessageTypeSigned");
			RulesTasksStrings.stringIDs.Add(2370124961U, "RejectReasonDescription");
			RulesTasksStrings.stringIDs.Add(2333734413U, "InboxRuleDescriptionFlaggedForFYI");
			RulesTasksStrings.stringIDs.Add(2632339802U, "LinkedPredicateRecipientAttributeMatches");
			RulesTasksStrings.stringIDs.Add(1776244123U, "LinkedPredicateAnyOfRecipientAddressMatches");
			RulesTasksStrings.stringIDs.Add(3487178095U, "LinkedPredicateRecipientAddressMatchesPatterns");
			RulesTasksStrings.stringIDs.Add(1279708626U, "SclValueDisplayName");
			RulesTasksStrings.stringIDs.Add(3219452859U, "PrependDisclaimer");
			RulesTasksStrings.stringIDs.Add(1416106290U, "ClientAccessRuleSetDatacenterAdminsOnlyError");
			RulesTasksStrings.stringIDs.Add(2826669353U, "LinkedPredicateAnyOfToCcHeaderMemberOfException");
			RulesTasksStrings.stringIDs.Add(1377545165U, "ADAttributeCustomAttribute7");
			RulesTasksStrings.stringIDs.Add(1713926708U, "FromDLAddressDescription");
			RulesTasksStrings.stringIDs.Add(756934846U, "RejectReasonDisplayName");
			RulesTasksStrings.stringIDs.Add(2589988606U, "LinkedPredicateSenderIpRanges");
			RulesTasksStrings.stringIDs.Add(1489020517U, "LinkedPredicateMessageContainsDataClassifications");
			RulesTasksStrings.stringIDs.Add(976180545U, "LinkedPredicateAnyOfToHeaderMemberOfException");
			RulesTasksStrings.stringIDs.Add(900910488U, "SetAuditSeverityDescription");
			RulesTasksStrings.stringIDs.Add(3952930894U, "RuleDescriptionAttachmentIsUnsupported");
			RulesTasksStrings.stringIDs.Add(1377545164U, "ADAttributeCustomAttribute6");
			RulesTasksStrings.stringIDs.Add(2479616296U, "LinkedPredicateBetweenMemberOfException");
			RulesTasksStrings.stringIDs.Add(4226527350U, "ADAttributeCity");
			RulesTasksStrings.stringIDs.Add(3204500204U, "DlpRuleMustContainMessageContainsDataClassificationPredicate");
			RulesTasksStrings.stringIDs.Add(3673771834U, "ImportanceDescription");
			RulesTasksStrings.stringIDs.Add(436298925U, "AppendDisclaimer");
			RulesTasksStrings.stringIDs.Add(2937791153U, "NegativePriority");
			RulesTasksStrings.stringIDs.Add(503362863U, "MessageSizeDisplayName");
			RulesTasksStrings.stringIDs.Add(523804848U, "LinkedPredicateSenderAttributeMatches");
			RulesTasksStrings.stringIDs.Add(1160183589U, "LinkedPredicateFromException");
			RulesTasksStrings.stringIDs.Add(296148748U, "LinkedPredicateAnyOfCcHeaderMemberOfException");
			RulesTasksStrings.stringIDs.Add(4241674U, "LinkedPredicateManagementRelationshipException");
			RulesTasksStrings.stringIDs.Add(1296350534U, "LinkedPredicateSenderDomainIsException");
			RulesTasksStrings.stringIDs.Add(1273212758U, "ToDLAddressDisplayName");
			RulesTasksStrings.stringIDs.Add(1316087888U, "ADAttributeEvaluationTypeDisplayName");
			RulesTasksStrings.stringIDs.Add(1461448431U, "DisclaimerLocationDisplayName");
			RulesTasksStrings.stringIDs.Add(1535769152U, "ImportanceHigh");
			RulesTasksStrings.stringIDs.Add(2537686292U, "InboxRuleDescriptionStopProcessingRules");
			RulesTasksStrings.stringIDs.Add(4149032993U, "InboxRuleDescriptionIf");
			RulesTasksStrings.stringIDs.Add(1251033108U, "LinkedActionRemoveOME");
			RulesTasksStrings.stringIDs.Add(3037379396U, "RuleDescriptionAttachmentIsPasswordProtected");
			RulesTasksStrings.stringIDs.Add(89686081U, "LinkedPredicateSubjectOrBodyContainsException");
			RulesTasksStrings.stringIDs.Add(494686544U, "ADAttributeManager");
			RulesTasksStrings.stringIDs.Add(3162495226U, "ADAttributeOtherPhoneNumber");
			RulesTasksStrings.stringIDs.Add(3800656714U, "LinkedPredicateFromAddressMatchesException");
			RulesTasksStrings.stringIDs.Add(3942685886U, "EvaluationDescription");
			RulesTasksStrings.stringIDs.Add(575057689U, "PrefixDisplayName");
			RulesTasksStrings.stringIDs.Add(2936457540U, "LinkedActionRemoveHeader");
			RulesTasksStrings.stringIDs.Add(2128154505U, "LinkedPredicateSenderInRecipientList");
			RulesTasksStrings.stringIDs.Add(2438278052U, "JournalingParameterErrorGccWithOrganization");
			RulesTasksStrings.stringIDs.Add(3631693406U, "ExternalUser");
			RulesTasksStrings.stringIDs.Add(573203429U, "RuleDescriptionStopRuleProcessing");
			RulesTasksStrings.stringIDs.Add(248554005U, "ErrorInboxRuleHasNoAction");
			RulesTasksStrings.stringIDs.Add(2822159338U, "RuleDescriptionModerateMessageByManager");
			RulesTasksStrings.stringIDs.Add(3080369366U, "RuleDescriptionAttachmentProcessingLimitExceeded");
			RulesTasksStrings.stringIDs.Add(2547374239U, "DisclaimerTextDisplayName");
			RulesTasksStrings.stringIDs.Add(1704483791U, "HubParameterNotValidOnEdgeRole");
			RulesTasksStrings.stringIDs.Add(2511974477U, "LinkedActionModerateMessageByManager");
			RulesTasksStrings.stringIDs.Add(2319428331U, "ADAttributeDescription");
			RulesTasksStrings.stringIDs.Add(2845043929U, "InboxRuleDescriptionMyNameNotInToBox");
			RulesTasksStrings.stringIDs.Add(1377545168U, "ADAttributeCustomAttribute2");
			RulesTasksStrings.stringIDs.Add(777113388U, "JournalingParameterErrorGccWithoutRecipient");
			RulesTasksStrings.stringIDs.Add(4027942746U, "ADAttributeEvaluationTypeEquals");
			RulesTasksStrings.stringIDs.Add(2409541533U, "LinkedPredicateFromAddressMatches");
			RulesTasksStrings.stringIDs.Add(2388837118U, "MessageDataClassificationDisplayName");
			RulesTasksStrings.stringIDs.Add(620435488U, "FromAddressesDescription");
			RulesTasksStrings.stringIDs.Add(1680325849U, "LinkedPredicateManagerIs");
			RulesTasksStrings.stringIDs.Add(787621858U, "LinkedActionAddManagerAsRecipientType");
			RulesTasksStrings.stringIDs.Add(1452889642U, "ADAttributeUserLogonName");
			RulesTasksStrings.stringIDs.Add(863112602U, "ADAttributeNotes");
			RulesTasksStrings.stringIDs.Add(3943169064U, "InboxRuleDescriptionSubscriptionNotFound");
			RulesTasksStrings.stringIDs.Add(914353404U, "IncidentReportOriginalMailDescription");
			RulesTasksStrings.stringIDs.Add(2376119956U, "JournalingParameterErrorExpiryDateWithoutGcc");
			RulesTasksStrings.stringIDs.Add(587899315U, "WordsDescription");
			RulesTasksStrings.stringIDs.Add(1041810761U, "PromptToOverwriteRulesOnImport");
			RulesTasksStrings.stringIDs.Add(638620002U, "SenderIpRangesDescription");
			RulesTasksStrings.stringIDs.Add(3576176750U, "LinkedPredicateAttachmentHasExecutableContentPredicateException");
			RulesTasksStrings.stringIDs.Add(4116851739U, "DeleteMessageActionMustBeTheOnlyAction");
			RulesTasksStrings.stringIDs.Add(3557196794U, "LinkedPredicateAnyOfToCcHeaderMemberOf");
			RulesTasksStrings.stringIDs.Add(1505963751U, "InvalidClassification");
			RulesTasksStrings.stringIDs.Add(2003153304U, "LinkedPredicateMessageContainsDataClassificationsException");
			RulesTasksStrings.stringIDs.Add(3380805386U, "InboxRuleDescriptionMessageClassificationNotFound");
			RulesTasksStrings.stringIDs.Add(2049063793U, "LinkedActionNotifySenderAction");
			RulesTasksStrings.stringIDs.Add(1070980084U, "LinkedPredicateAttachmentExtensionMatchesWords");
			RulesTasksStrings.stringIDs.Add(2365361139U, "RuleDescriptionApplyOME");
			RulesTasksStrings.stringIDs.Add(715964235U, "ExternalPartner");
			RulesTasksStrings.stringIDs.Add(1596558929U, "LinkedPredicateAttachmentSizeOverException");
			RulesTasksStrings.stringIDs.Add(562852627U, "LinkedPredicateADAttributeComparisonException");
			RulesTasksStrings.stringIDs.Add(3544120613U, "MessageTypeEncrypted");
			RulesTasksStrings.stringIDs.Add(1324690146U, "SenderNotificationTypeDescription");
			RulesTasksStrings.stringIDs.Add(2303954899U, "SenderNotificationTypeDisplayName");
			RulesTasksStrings.stringIDs.Add(3806797692U, "LinkedPredicateMessageTypeMatches");
			RulesTasksStrings.stringIDs.Add(1457839961U, "ADAttributeHomePhoneNumber");
			RulesTasksStrings.stringIDs.Add(2334278303U, "LinkedPredicateFromAddressContains");
			RulesTasksStrings.stringIDs.Add(4162282226U, "LinkedPredicateSubjectOrBodyMatches");
			RulesTasksStrings.stringIDs.Add(2891753468U, "ADAttributeCompany");
			RulesTasksStrings.stringIDs.Add(2030715989U, "EvaluatedUserRecipient");
			RulesTasksStrings.stringIDs.Add(816385242U, "RuleDescriptionAttachmentHasExecutableContent");
			RulesTasksStrings.stringIDs.Add(890082467U, "LinkedPredicateHasClassificationException");
			RulesTasksStrings.stringIDs.Add(398851013U, "LinkedPredicateAnyOfRecipientAddressContains");
			RulesTasksStrings.stringIDs.Add(952041456U, "LinkedPredicateSclOver");
			RulesTasksStrings.stringIDs.Add(1485258685U, "LinkedActionDeleteMessage");
			RulesTasksStrings.stringIDs.Add(3943754367U, "RuleDescriptionDisclaimerIgnoreFallback");
			RulesTasksStrings.stringIDs.Add(3708149033U, "RejectMessageActionMustBeTheOnlyAction");
			RulesTasksStrings.stringIDs.Add(1708203343U, "RuleDescriptionDisclaimerWrapFallback");
			RulesTasksStrings.stringIDs.Add(1663446825U, "InboxRuleDescriptionMyNameInToOrCcBox");
			RulesTasksStrings.stringIDs.Add(1780014951U, "LinkedPredicateMessageSizeOverException");
			RulesTasksStrings.stringIDs.Add(2476473719U, "CcRecipientType");
			RulesTasksStrings.stringIDs.Add(2808719285U, "ClientAccessRulesNameAlreadyInUse");
			RulesTasksStrings.stringIDs.Add(2767244755U, "LinkedPredicateContentCharacterSetContainsWordsException");
			RulesTasksStrings.stringIDs.Add(403146382U, "LinkedPredicateSubjectContainsException");
			RulesTasksStrings.stringIDs.Add(1808276634U, "ADAttributeCustomAttribute13");
			RulesTasksStrings.stringIDs.Add(2828094182U, "ToAddressesDisplayName");
			RulesTasksStrings.stringIDs.Add(3891477573U, "LinkedPredicateHasNoClassification");
			RulesTasksStrings.stringIDs.Add(2269102077U, "LinkedActionQuarantine");
			RulesTasksStrings.stringIDs.Add(2872629304U, "MessageTypePermissionControlled");
			RulesTasksStrings.stringIDs.Add(1377545175U, "ADAttributeCustomAttribute9");
			RulesTasksStrings.stringIDs.Add(4006689082U, "SubTypeDescription");
			RulesTasksStrings.stringIDs.Add(2743288863U, "LinkedPredicateSenderAttributeContainsException");
			RulesTasksStrings.stringIDs.Add(972971379U, "ReportDestinationDisplayName");
			RulesTasksStrings.stringIDs.Add(2666086208U, "InboxRuleDescriptionDeleteMessage");
			RulesTasksStrings.stringIDs.Add(893471950U, "ToScopeDisplayName");
			RulesTasksStrings.stringIDs.Add(4135645967U, "NotifySenderActionIsBeingOverridded");
			RulesTasksStrings.stringIDs.Add(1386608555U, "JournalingParameterErrorGccWithScope");
			RulesTasksStrings.stringIDs.Add(2720144322U, "RuleStateEnabled");
			RulesTasksStrings.stringIDs.Add(3836787260U, "LinkedActionRedirectMessage");
			RulesTasksStrings.stringIDs.Add(242192693U, "ADAttributeCustomAttribute12");
			RulesTasksStrings.stringIDs.Add(1453324876U, "RuleNameAlreadyExist");
			RulesTasksStrings.stringIDs.Add(1873569937U, "LinkedPredicateAttachmentIsPasswordProtectedException");
			RulesTasksStrings.stringIDs.Add(2411750738U, "ADAttributeMobileNumber");
			RulesTasksStrings.stringIDs.Add(3894352642U, "LinkedPredicateADAttributeComparison");
			RulesTasksStrings.stringIDs.Add(1954471536U, "EnhancedStatusCodeDisplayName");
			RulesTasksStrings.stringIDs.Add(889104748U, "InboxRuleDescriptionTakeActions");
			RulesTasksStrings.stringIDs.Add(3134498491U, "RejectMessageActionIsBeingOverridded");
			RulesTasksStrings.stringIDs.Add(553196496U, "ManagementRelationshipDisplayName");
			RulesTasksStrings.stringIDs.Add(403803765U, "AttachmentSizeDisplayName");
			RulesTasksStrings.stringIDs.Add(2021688564U, "ClientAccessRuleRemoveDatacenterAdminsOnlyError");
			RulesTasksStrings.stringIDs.Add(1957537238U, "InvalidMessageDataClassificationEmptyName");
			RulesTasksStrings.stringIDs.Add(498572210U, "RejectMessageActionType");
			RulesTasksStrings.stringIDs.Add(3512455487U, "InboxRuleDescriptionMyNameInCcBox");
			RulesTasksStrings.stringIDs.Add(1408423837U, "LinkedPredicateRecipientDomainIs");
			RulesTasksStrings.stringIDs.Add(3557021932U, "MissingDataClassificationsName");
			RulesTasksStrings.stringIDs.Add(3340948424U, "LinkedActionGenerateIncidentReportAction");
			RulesTasksStrings.stringIDs.Add(1466544691U, "InvalidAction");
			RulesTasksStrings.stringIDs.Add(115064938U, "MessageHeaderDescription");
			RulesTasksStrings.stringIDs.Add(742388560U, "DisclaimerTextDescription");
			RulesTasksStrings.stringIDs.Add(3609362930U, "MessageSizeDescription");
			RulesTasksStrings.stringIDs.Add(4164216509U, "SclValueDescription");
			RulesTasksStrings.stringIDs.Add(2447598924U, "RejectUnlessSilentOverrideActionType");
			RulesTasksStrings.stringIDs.Add(4271012524U, "LinkedPredicateManagerIsException");
			RulesTasksStrings.stringIDs.Add(2391892399U, "MessageDataClassificationDescription");
			RulesTasksStrings.stringIDs.Add(2953542218U, "ImportanceLow");
			RulesTasksStrings.stringIDs.Add(785480795U, "LinkedPredicateFromMemberOf");
			RulesTasksStrings.stringIDs.Add(1467203807U, "InboxRuleDescriptionOr");
			RulesTasksStrings.stringIDs.Add(2881863453U, "LinkedPredicateAttachmentIsUnsupportedException");
			RulesTasksStrings.stringIDs.Add(598779112U, "FromScopeDescription");
			RulesTasksStrings.stringIDs.Add(2155604814U, "ExternalNonPartner");
			RulesTasksStrings.stringIDs.Add(645477220U, "ADAttributeCustomAttribute11");
			RulesTasksStrings.stringIDs.Add(3309135616U, "FallbackActionDescription");
			RulesTasksStrings.stringIDs.Add(382355823U, "InboxRuleDescriptionMarkAsRead");
			RulesTasksStrings.stringIDs.Add(1118581569U, "LinkedPredicateSentToMemberOfException");
			RulesTasksStrings.stringIDs.Add(4189101821U, "LinkedPredicateWithImportanceException");
			RulesTasksStrings.stringIDs.Add(3339775592U, "ImportFileDataIsNull");
			RulesTasksStrings.stringIDs.Add(1903193717U, "MessageTypeCalendaring");
			RulesTasksStrings.stringIDs.Add(604363629U, "NotifyOnlyActionType");
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000F3F7 File Offset: 0x0000D5F7
		public static LocalizedString LinkedPredicateHeaderMatches
		{
			get
			{
				return new LocalizedString("LinkedPredicateHeaderMatches", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000F40E File Offset: 0x0000D60E
		public static LocalizedString SetAuditSeverityDisplayName
		{
			get
			{
				return new LocalizedString("SetAuditSeverityDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000F425 File Offset: 0x0000D625
		public static LocalizedString ADAttributeOtherHomePhoneNumber
		{
			get
			{
				return new LocalizedString("ADAttributeOtherHomePhoneNumber", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000F43C File Offset: 0x0000D63C
		public static LocalizedString JournalingParameterErrorFullReportWithoutGcc
		{
			get
			{
				return new LocalizedString("JournalingParameterErrorFullReportWithoutGcc", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000F453 File Offset: 0x0000D653
		public static LocalizedString RecipientTypeDescription
		{
			get
			{
				return new LocalizedString("RecipientTypeDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000F46C File Offset: 0x0000D66C
		public static LocalizedString IncidentReportConflictingParameters(string parameter1, string parameter2)
		{
			return new LocalizedString("IncidentReportConflictingParameters", RulesTasksStrings.ResourceManager, new object[]
			{
				parameter1,
				parameter2
			});
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000F498 File Offset: 0x0000D698
		public static LocalizedString EvaluatedUserDisplayName
		{
			get
			{
				return new LocalizedString("EvaluatedUserDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000F4AF File Offset: 0x0000D6AF
		public static LocalizedString LinkedPredicateHeaderContains
		{
			get
			{
				return new LocalizedString("LinkedPredicateHeaderContains", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000F4C6 File Offset: 0x0000D6C6
		public static LocalizedString MessageTypeOof
		{
			get
			{
				return new LocalizedString("MessageTypeOof", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000F4DD File Offset: 0x0000D6DD
		public static LocalizedString ImportanceDisplayName
		{
			get
			{
				return new LocalizedString("ImportanceDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		public static LocalizedString DlpPolicyModeIsOverridenByModeParameter
		{
			get
			{
				return new LocalizedString("DlpPolicyModeIsOverridenByModeParameter", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000F50B File Offset: 0x0000D70B
		public static LocalizedString LinkedActionApplyHtmlDisclaimer
		{
			get
			{
				return new LocalizedString("LinkedActionApplyHtmlDisclaimer", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000F522 File Offset: 0x0000D722
		public static LocalizedString RejectMessageParameterWillBeIgnored
		{
			get
			{
				return new LocalizedString("RejectMessageParameterWillBeIgnored", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000F539 File Offset: 0x0000D739
		public static LocalizedString LinkedPredicateRecipientAddressContainsWords
		{
			get
			{
				return new LocalizedString("LinkedPredicateRecipientAddressContainsWords", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000F550 File Offset: 0x0000D750
		public static LocalizedString AttachmentSizeDescription
		{
			get
			{
				return new LocalizedString("AttachmentSizeDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000F567 File Offset: 0x0000D767
		public static LocalizedString LinkedPredicateRecipientAddressMatchesPatternsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateRecipientAddressMatchesPatternsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000F57E File Offset: 0x0000D77E
		public static LocalizedString AttributeValueDescription
		{
			get
			{
				return new LocalizedString("AttributeValueDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000F598 File Offset: 0x0000D798
		public static LocalizedString InboxRuleDescriptionMoveToFolder(string folder)
		{
			return new LocalizedString("InboxRuleDescriptionMoveToFolder", RulesTasksStrings.ResourceManager, new object[]
			{
				folder
			});
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		public static LocalizedString RuleDescriptionRecipientInSenderList(string lists)
		{
			return new LocalizedString("RuleDescriptionRecipientInSenderList", RulesTasksStrings.ResourceManager, new object[]
			{
				lists
			});
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000F5E8 File Offset: 0x0000D7E8
		public static LocalizedString RuleDescriptionSentToMemberOf(string addresses)
		{
			return new LocalizedString("RuleDescriptionSentToMemberOf", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses
			});
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000F610 File Offset: 0x0000D810
		public static LocalizedString LinkedPredicateAttachmentMatchesPatternsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentMatchesPatternsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000F627 File Offset: 0x0000D827
		public static LocalizedString LinkedActionLogEvent
		{
			get
			{
				return new LocalizedString("LinkedActionLogEvent", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000F63E File Offset: 0x0000D83E
		public static LocalizedString LinkedPredicateAnyOfToHeader
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfToHeader", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000F658 File Offset: 0x0000D858
		public static LocalizedString RuleDescriptionManagementRelationship(string relationship)
		{
			return new LocalizedString("RuleDescriptionManagementRelationship", RulesTasksStrings.ResourceManager, new object[]
			{
				relationship
			});
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000F680 File Offset: 0x0000D880
		public static LocalizedString EvaluatedUserDescription
		{
			get
			{
				return new LocalizedString("EvaluatedUserDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000F698 File Offset: 0x0000D898
		public static LocalizedString RuleDescriptionHasClassification(string classification)
		{
			return new LocalizedString("RuleDescriptionHasClassification", RulesTasksStrings.ResourceManager, new object[]
			{
				classification
			});
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		public static LocalizedString ADAttributeCustomAttribute1
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute1", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000F6D7 File Offset: 0x0000D8D7
		public static LocalizedString LinkedPredicateAttachmentPropertyContainsWordsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentPropertyContainsWordsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000F6EE File Offset: 0x0000D8EE
		public static LocalizedString LinkedActionApplyClassification
		{
			get
			{
				return new LocalizedString("LinkedActionApplyClassification", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000F705 File Offset: 0x0000D905
		public static LocalizedString LinkedActionSetScl
		{
			get
			{
				return new LocalizedString("LinkedActionSetScl", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000F71C File Offset: 0x0000D91C
		public static LocalizedString LinkedPredicateAnyOfCcHeaderException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfCcHeaderException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000F733 File Offset: 0x0000D933
		public static LocalizedString ArgumentNotSet
		{
			get
			{
				return new LocalizedString("ArgumentNotSet", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000F74A File Offset: 0x0000D94A
		public static LocalizedString InvalidRuleName
		{
			get
			{
				return new LocalizedString("InvalidRuleName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000F761 File Offset: 0x0000D961
		public static LocalizedString RuleDescriptionDisclaimerRejectFallback
		{
			get
			{
				return new LocalizedString("RuleDescriptionDisclaimerRejectFallback", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000F778 File Offset: 0x0000D978
		public static LocalizedString LinkedPredicateFromAddressContainsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateFromAddressContainsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000F78F File Offset: 0x0000D98F
		public static LocalizedString LinkedPredicateAttachmentNameMatchesException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentNameMatchesException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000F7A6 File Offset: 0x0000D9A6
		public static LocalizedString ADAttributeLastName
		{
			get
			{
				return new LocalizedString("ADAttributeLastName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000F7BD File Offset: 0x0000D9BD
		public static LocalizedString GenerateNotificationDisplayName
		{
			get
			{
				return new LocalizedString("GenerateNotificationDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000F7D4 File Offset: 0x0000D9D4
		public static LocalizedString RuleDescriptionNotifySenderNotifyOnly
		{
			get
			{
				return new LocalizedString("RuleDescriptionNotifySenderNotifyOnly", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000F7EB File Offset: 0x0000D9EB
		public static LocalizedString ADAttributeCountry
		{
			get
			{
				return new LocalizedString("ADAttributeCountry", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000F802 File Offset: 0x0000DA02
		public static LocalizedString MessageHeaderDisplayName
		{
			get
			{
				return new LocalizedString("MessageHeaderDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000F819 File Offset: 0x0000DA19
		public static LocalizedString LinkedPredicateAttachmentMatchesPatterns
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentMatchesPatterns", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000F830 File Offset: 0x0000DA30
		public static LocalizedString InboxRuleDescriptionFlaggedForUnsupportedAction(string action)
		{
			return new LocalizedString("InboxRuleDescriptionFlaggedForUnsupportedAction", RulesTasksStrings.ResourceManager, new object[]
			{
				action
			});
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000F858 File Offset: 0x0000DA58
		public static LocalizedString ListsDisplayName
		{
			get
			{
				return new LocalizedString("ListsDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000F86F File Offset: 0x0000DA6F
		public static LocalizedString FallbackIgnore
		{
			get
			{
				return new LocalizedString("FallbackIgnore", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000F888 File Offset: 0x0000DA88
		public static LocalizedString InvalidAuditSeverityLevel(string severityLevel)
		{
			return new LocalizedString("InvalidAuditSeverityLevel", RulesTasksStrings.ResourceManager, new object[]
			{
				severityLevel
			});
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
		public static LocalizedString RuleDescriptionAnyOfToCcHeaderMemberOf(string addresses)
		{
			return new LocalizedString("RuleDescriptionAnyOfToCcHeaderMemberOf", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses
			});
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		public static LocalizedString CannotParseRuleDueToVersion(string name)
		{
			return new LocalizedString("CannotParseRuleDueToVersion", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000F900 File Offset: 0x0000DB00
		public static LocalizedString RuleDescriptionHeaderContains(string header, string words)
		{
			return new LocalizedString("RuleDescriptionHeaderContains", RulesTasksStrings.ResourceManager, new object[]
			{
				header,
				words
			});
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000F92C File Offset: 0x0000DB2C
		public static LocalizedString LinkedPredicateSenderIpRangesException
		{
			get
			{
				return new LocalizedString("LinkedPredicateSenderIpRangesException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000F943 File Offset: 0x0000DB43
		public static LocalizedString AttributeValueDisplayName
		{
			get
			{
				return new LocalizedString("AttributeValueDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000F95A File Offset: 0x0000DB5A
		public static LocalizedString LinkedPredicateRecipientDomainIsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateRecipientDomainIsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000F971 File Offset: 0x0000DB71
		public static LocalizedString InboxRuleDescriptionHasAttachment
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionHasAttachment", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000F988 File Offset: 0x0000DB88
		public static LocalizedString RuleDescriptionHasSenderOverride
		{
			get
			{
				return new LocalizedString("RuleDescriptionHasSenderOverride", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000F9A0 File Offset: 0x0000DBA0
		public static LocalizedString CustomizedDsnNotConfigured(string status)
		{
			return new LocalizedString("CustomizedDsnNotConfigured", RulesTasksStrings.ResourceManager, new object[]
			{
				status
			});
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000F9C8 File Offset: 0x0000DBC8
		public static LocalizedString LinkedPredicateRecipientInSenderListException
		{
			get
			{
				return new LocalizedString("LinkedPredicateRecipientInSenderListException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000F9DF File Offset: 0x0000DBDF
		public static LocalizedString LinkedPredicateAttachmentExtensionMatchesWordsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentExtensionMatchesWordsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000F9F6 File Offset: 0x0000DBF6
		public static LocalizedString ImportanceNormal
		{
			get
			{
				return new LocalizedString("ImportanceNormal", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000FA0D File Offset: 0x0000DC0D
		public static LocalizedString LinkedActionCopyTo
		{
			get
			{
				return new LocalizedString("LinkedActionCopyTo", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000FA24 File Offset: 0x0000DC24
		public static LocalizedString ListsDescription
		{
			get
			{
				return new LocalizedString("ListsDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		public static LocalizedString ClientAccessRulesFilterPropertyRequired(string name)
		{
			return new LocalizedString("ClientAccessRulesFilterPropertyRequired", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000FA64 File Offset: 0x0000DC64
		public static LocalizedString RuleDescriptionRecipientAttributeContains(string words)
		{
			return new LocalizedString("RuleDescriptionRecipientAttributeContains", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000FA8C File Offset: 0x0000DC8C
		public static LocalizedString TextPatternsDescription
		{
			get
			{
				return new LocalizedString("TextPatternsDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
		public static LocalizedString InboxRuleDescriptionApplyRetentionPolicyTag(string policyTag)
		{
			return new LocalizedString("InboxRuleDescriptionApplyRetentionPolicyTag", RulesTasksStrings.ResourceManager, new object[]
			{
				policyTag
			});
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000FACC File Offset: 0x0000DCCC
		public static LocalizedString RuleDescriptionNotifySenderRejectUnlessExplicitOverride(string rejectText, string rejectCode)
		{
			return new LocalizedString("RuleDescriptionNotifySenderRejectUnlessExplicitOverride", RulesTasksStrings.ResourceManager, new object[]
			{
				rejectText,
				rejectCode
			});
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
		public static LocalizedString NewRuleSyncAcrossDifferentVersionsNeeded
		{
			get
			{
				return new LocalizedString("NewRuleSyncAcrossDifferentVersionsNeeded", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000FB10 File Offset: 0x0000DD10
		public static LocalizedString OutlookProtectionRuleRmsTemplateNotUnique(string name)
		{
			return new LocalizedString("OutlookProtectionRuleRmsTemplateNotUnique", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000FB38 File Offset: 0x0000DD38
		public static LocalizedString RmsTemplateDescription
		{
			get
			{
				return new LocalizedString("RmsTemplateDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000FB50 File Offset: 0x0000DD50
		public static LocalizedString RuleDescriptionNotifySenderRejectUnlessSilentOverride(string rejectText, string rejectCode)
		{
			return new LocalizedString("RuleDescriptionNotifySenderRejectUnlessSilentOverride", RulesTasksStrings.ResourceManager, new object[]
			{
				rejectText,
				rejectCode
			});
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000FB7C File Offset: 0x0000DD7C
		public static LocalizedString RuleDescriptionAttachmentExtensionMatchesWords(string words)
		{
			return new LocalizedString("RuleDescriptionAttachmentExtensionMatchesWords", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
		public static LocalizedString InboxRuleDescriptionReceivedBeforeDate(string date)
		{
			return new LocalizedString("InboxRuleDescriptionReceivedBeforeDate", RulesTasksStrings.ResourceManager, new object[]
			{
				date
			});
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000FBCC File Offset: 0x0000DDCC
		public static LocalizedString InboxRuleDescriptionCopyToFolder(string folder)
		{
			return new LocalizedString("InboxRuleDescriptionCopyToFolder", RulesTasksStrings.ResourceManager, new object[]
			{
				folder
			});
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000FBF4 File Offset: 0x0000DDF4
		public static LocalizedString RuleDescriptionMessageSizeOver(string size)
		{
			return new LocalizedString("RuleDescriptionMessageSizeOver", RulesTasksStrings.ResourceManager, new object[]
			{
				size
			});
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000FC1C File Offset: 0x0000DE1C
		public static LocalizedString LinkedPredicateRecipientInSenderList
		{
			get
			{
				return new LocalizedString("LinkedPredicateRecipientInSenderList", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000FC33 File Offset: 0x0000DE33
		public static LocalizedString ADAttributeInitials
		{
			get
			{
				return new LocalizedString("ADAttributeInitials", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000FC4A File Offset: 0x0000DE4A
		public static LocalizedString LinkedPredicateHeaderContainsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateHeaderContainsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000FC61 File Offset: 0x0000DE61
		public static LocalizedString LinkedPredicateContentCharacterSetContainsWords
		{
			get
			{
				return new LocalizedString("LinkedPredicateContentCharacterSetContainsWords", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000FC78 File Offset: 0x0000DE78
		public static LocalizedString MessageTypeDescription
		{
			get
			{
				return new LocalizedString("MessageTypeDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000FC90 File Offset: 0x0000DE90
		public static LocalizedString InboxRuleDescriptionFromSubscription(string subscriptionEmailAddresses)
		{
			return new LocalizedString("InboxRuleDescriptionFromSubscription", RulesTasksStrings.ResourceManager, new object[]
			{
				subscriptionEmailAddresses
			});
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000FCB8 File Offset: 0x0000DEB8
		public static LocalizedString ADAttributeState
		{
			get
			{
				return new LocalizedString("ADAttributeState", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000FCCF File Offset: 0x0000DECF
		public static LocalizedString FromScopeDisplayName
		{
			get
			{
				return new LocalizedString("FromScopeDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000FCE6 File Offset: 0x0000DEE6
		public static LocalizedString InboxRuleDescriptionAnd
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionAnd", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000FD00 File Offset: 0x0000DF00
		public static LocalizedString InboxRuleDescriptionRedirectTo(string recipients)
		{
			return new LocalizedString("InboxRuleDescriptionRedirectTo", RulesTasksStrings.ResourceManager, new object[]
			{
				recipients
			});
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000FD28 File Offset: 0x0000DF28
		public static LocalizedString ADAttributeCustomAttribute8
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute8", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000FD3F File Offset: 0x0000DF3F
		public static LocalizedString LinkedPredicateSubjectMatches
		{
			get
			{
				return new LocalizedString("LinkedPredicateSubjectMatches", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000FD56 File Offset: 0x0000DF56
		public static LocalizedString MessageTypeAutoForward
		{
			get
			{
				return new LocalizedString("MessageTypeAutoForward", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000FD6D File Offset: 0x0000DF6D
		public static LocalizedString MessageTypeReadReceipt
		{
			get
			{
				return new LocalizedString("MessageTypeReadReceipt", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000FD84 File Offset: 0x0000DF84
		public static LocalizedString LinkedActionBlindCopyTo
		{
			get
			{
				return new LocalizedString("LinkedActionBlindCopyTo", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000FD9B File Offset: 0x0000DF9B
		public static LocalizedString LinkedPredicateSentToScopeException
		{
			get
			{
				return new LocalizedString("LinkedPredicateSentToScopeException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000FDB2 File Offset: 0x0000DFB2
		public static LocalizedString LinkedActionStopRuleProcessing
		{
			get
			{
				return new LocalizedString("LinkedActionStopRuleProcessing", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000FDC9 File Offset: 0x0000DFC9
		public static LocalizedString LinkedPredicateAnyOfToCcHeader
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfToCcHeader", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
		public static LocalizedString InboxRuleDescriptionSentTo(string address)
		{
			return new LocalizedString("InboxRuleDescriptionSentTo", RulesTasksStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000FE08 File Offset: 0x0000E008
		public static LocalizedString LinkedPredicateHasSenderOverride
		{
			get
			{
				return new LocalizedString("LinkedPredicateHasSenderOverride", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000FE1F File Offset: 0x0000E01F
		public static LocalizedString RedirectRecipientType
		{
			get
			{
				return new LocalizedString("RedirectRecipientType", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000FE38 File Offset: 0x0000E038
		public static LocalizedString InboxRuleDescriptionReceivedAfterDate(string date)
		{
			return new LocalizedString("InboxRuleDescriptionReceivedAfterDate", RulesTasksStrings.ResourceManager, new object[]
			{
				date
			});
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000FE60 File Offset: 0x0000E060
		public static LocalizedString ConditionIncompatibleWithNotifySenderAction(string conditionName, string subtypeName)
		{
			return new LocalizedString("ConditionIncompatibleWithNotifySenderAction", RulesTasksStrings.ResourceManager, new object[]
			{
				conditionName,
				subtypeName
			});
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000FE8C File Offset: 0x0000E08C
		public static LocalizedString FallbackActionDisplayName
		{
			get
			{
				return new LocalizedString("FallbackActionDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000FEA3 File Offset: 0x0000E0A3
		public static LocalizedString NoAction
		{
			get
			{
				return new LocalizedString("NoAction", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000FEBA File Offset: 0x0000E0BA
		public static LocalizedString InboxRuleDescriptionFlaggedForAnyAction
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionFlaggedForAnyAction", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000FED4 File Offset: 0x0000E0D4
		public static LocalizedString ClientAccessRulesIpPropertyRequired(string name)
		{
			return new LocalizedString("ClientAccessRulesIpPropertyRequired", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000FEFC File Offset: 0x0000E0FC
		public static LocalizedString LinkedActionSetHeader
		{
			get
			{
				return new LocalizedString("LinkedActionSetHeader", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000FF14 File Offset: 0x0000E114
		public static LocalizedString RuleDescriptionADAttributeMatchesText(string evaluatedUser, string attribute, string evaluation, string value)
		{
			return new LocalizedString("RuleDescriptionADAttributeMatchesText", RulesTasksStrings.ResourceManager, new object[]
			{
				evaluatedUser,
				attribute,
				evaluation,
				value
			});
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000FF48 File Offset: 0x0000E148
		public static LocalizedString RuleDescriptionSenderAttributeContains(string words)
		{
			return new LocalizedString("RuleDescriptionSenderAttributeContains", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000FF70 File Offset: 0x0000E170
		public static LocalizedString SenderIpRangesDisplayName
		{
			get
			{
				return new LocalizedString("SenderIpRangesDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000FF88 File Offset: 0x0000E188
		public static LocalizedString RuleDescriptionFromAddressMatches(string patterns)
		{
			return new LocalizedString("RuleDescriptionFromAddressMatches", RulesTasksStrings.ResourceManager, new object[]
			{
				patterns
			});
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
		public static LocalizedString InboxRuleDescriptionFlaggedForAction(string action)
		{
			return new LocalizedString("InboxRuleDescriptionFlaggedForAction", RulesTasksStrings.ResourceManager, new object[]
			{
				action
			});
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
		public static LocalizedString LinkedPredicateAttachmentIsUnsupported
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentIsUnsupported", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000FFEF File Offset: 0x0000E1EF
		public static LocalizedString ADAttributeName
		{
			get
			{
				return new LocalizedString("ADAttributeName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00010008 File Offset: 0x0000E208
		public static LocalizedString RuleDescriptionSentToScope(string scope)
		{
			return new LocalizedString("RuleDescriptionSentToScope", RulesTasksStrings.ResourceManager, new object[]
			{
				scope
			});
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00010030 File Offset: 0x0000E230
		public static LocalizedString EventMessageDisplayName
		{
			get
			{
				return new LocalizedString("EventMessageDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00010047 File Offset: 0x0000E247
		public static LocalizedString IncidentReportOriginalMailnDisplayName
		{
			get
			{
				return new LocalizedString("IncidentReportOriginalMailnDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00010060 File Offset: 0x0000E260
		public static LocalizedString InboxRuleDescriptionForwardTo(string address)
		{
			return new LocalizedString("InboxRuleDescriptionForwardTo", RulesTasksStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00010088 File Offset: 0x0000E288
		public static LocalizedString ADAttributeOtherFaxNumber
		{
			get
			{
				return new LocalizedString("ADAttributeOtherFaxNumber", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001009F File Offset: 0x0000E29F
		public static LocalizedString RejectUnlessExplicitOverrideActionType
		{
			get
			{
				return new LocalizedString("RejectUnlessExplicitOverrideActionType", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x000100B6 File Offset: 0x0000E2B6
		public static LocalizedString RuleDescriptionHasNoClassification
		{
			get
			{
				return new LocalizedString("RuleDescriptionHasNoClassification", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000100D0 File Offset: 0x0000E2D0
		public static LocalizedString InboxRuleDescriptionForwardAsAttachmentTo(string recipients)
		{
			return new LocalizedString("InboxRuleDescriptionForwardAsAttachmentTo", RulesTasksStrings.ResourceManager, new object[]
			{
				recipients
			});
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x000100F8 File Offset: 0x0000E2F8
		public static LocalizedString LinkedPredicateAnyOfRecipientAddressMatchesException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfRecipientAddressMatchesException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0001010F File Offset: 0x0000E30F
		public static LocalizedString MessageTypeDisplayName
		{
			get
			{
				return new LocalizedString("MessageTypeDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00010126 File Offset: 0x0000E326
		public static LocalizedString FallbackReject
		{
			get
			{
				return new LocalizedString("FallbackReject", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00010140 File Offset: 0x0000E340
		public static LocalizedString InvalidMacroName(string invalidMacroName)
		{
			return new LocalizedString("InvalidMacroName", RulesTasksStrings.ResourceManager, new object[]
			{
				invalidMacroName
			});
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00010168 File Offset: 0x0000E368
		public static LocalizedString ErrorRuleXmlTooBig(int currentSize, long maxSize)
		{
			return new LocalizedString("ErrorRuleXmlTooBig", RulesTasksStrings.ResourceManager, new object[]
			{
				currentSize,
				maxSize
			});
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0001019E File Offset: 0x0000E39E
		public static LocalizedString ADAttributeCustomAttribute10
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute10", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x000101B5 File Offset: 0x0000E3B5
		public static LocalizedString ADAttributeEmail
		{
			get
			{
				return new LocalizedString("ADAttributeEmail", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x000101CC File Offset: 0x0000E3CC
		public static LocalizedString ADAttributeCustomAttribute5
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute5", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x000101E3 File Offset: 0x0000E3E3
		public static LocalizedString LinkedPredicateSubjectOrBodyMatchesException
		{
			get
			{
				return new LocalizedString("LinkedPredicateSubjectOrBodyMatchesException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x000101FA File Offset: 0x0000E3FA
		public static LocalizedString InboxRuleDescriptionMyNameInToBox
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionMyNameInToBox", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00010214 File Offset: 0x0000E414
		public static LocalizedString RuleDescriptionModerateMessageByUser(string addresses)
		{
			return new LocalizedString("RuleDescriptionModerateMessageByUser", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses
			});
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0001023C File Offset: 0x0000E43C
		public static LocalizedString ToRecipientType
		{
			get
			{
				return new LocalizedString("ToRecipientType", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00010253 File Offset: 0x0000E453
		public static LocalizedString LinkedPredicateAttachmentContainsWords
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentContainsWords", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0001026A File Offset: 0x0000E46A
		public static LocalizedString LinkedActionRightsProtectMessage
		{
			get
			{
				return new LocalizedString("LinkedActionRightsProtectMessage", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00010281 File Offset: 0x0000E481
		public static LocalizedString LinkedActionSetAuditSeverity
		{
			get
			{
				return new LocalizedString("LinkedActionSetAuditSeverity", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00010298 File Offset: 0x0000E498
		public static LocalizedString LinkedPredicateBetweenMemberOf
		{
			get
			{
				return new LocalizedString("LinkedPredicateBetweenMemberOf", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000102B0 File Offset: 0x0000E4B0
		public static LocalizedString RuleStateParameterValueIsInconsistentWithDlpPolicyState(string enabledParameterName)
		{
			return new LocalizedString("RuleStateParameterValueIsInconsistentWithDlpPolicyState", RulesTasksStrings.ResourceManager, new object[]
			{
				enabledParameterName
			});
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000102D8 File Offset: 0x0000E4D8
		public static LocalizedString RuleDescriptionAttachmentMatchesPatterns(string patterns)
		{
			return new LocalizedString("RuleDescriptionAttachmentMatchesPatterns", RulesTasksStrings.ResourceManager, new object[]
			{
				patterns
			});
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00010300 File Offset: 0x0000E500
		public static LocalizedString RemoveRuleSyncAcrossDifferentVersionsNeeded
		{
			get
			{
				return new LocalizedString("RemoveRuleSyncAcrossDifferentVersionsNeeded", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00010317 File Offset: 0x0000E517
		public static LocalizedString ADAttributeDisplayName
		{
			get
			{
				return new LocalizedString("ADAttributeDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0001032E File Offset: 0x0000E52E
		public static LocalizedString LinkedPredicateSentToScope
		{
			get
			{
				return new LocalizedString("LinkedPredicateSentToScope", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00010345 File Offset: 0x0000E545
		public static LocalizedString DisclaimerLocationDescription
		{
			get
			{
				return new LocalizedString("DisclaimerLocationDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0001035C File Offset: 0x0000E55C
		public static LocalizedString FromAddressesDisplayName
		{
			get
			{
				return new LocalizedString("FromAddressesDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00010374 File Offset: 0x0000E574
		public static LocalizedString InboxRuleDescriptionReceivedBetweenDates(string before, string after)
		{
			return new LocalizedString("InboxRuleDescriptionReceivedBetweenDates", RulesTasksStrings.ResourceManager, new object[]
			{
				before,
				after
			});
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x000103A0 File Offset: 0x0000E5A0
		public static LocalizedString LinkedActionRejectMessage
		{
			get
			{
				return new LocalizedString("LinkedActionRejectMessage", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x000103B7 File Offset: 0x0000E5B7
		public static LocalizedString LinkedPredicateSenderAttributeMatchesException
		{
			get
			{
				return new LocalizedString("LinkedPredicateSenderAttributeMatchesException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x000103CE File Offset: 0x0000E5CE
		public static LocalizedString LinkedActionRouteMessageOutboundRequireTls
		{
			get
			{
				return new LocalizedString("LinkedActionRouteMessageOutboundRequireTls", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x000103E5 File Offset: 0x0000E5E5
		public static LocalizedString LinkedPredicateAttachmentNameMatches
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentNameMatches", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000103FC File Offset: 0x0000E5FC
		public static LocalizedString HeaderValueDescription
		{
			get
			{
				return new LocalizedString("HeaderValueDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00010413 File Offset: 0x0000E613
		public static LocalizedString RecipientTypeDisplayName
		{
			get
			{
				return new LocalizedString("RecipientTypeDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0001042A File Offset: 0x0000E62A
		public static LocalizedString LinkedPredicateFromMemberOfException
		{
			get
			{
				return new LocalizedString("LinkedPredicateFromMemberOfException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00010444 File Offset: 0x0000E644
		public static LocalizedString InboxRuleDescriptionBodyContainsWords(string words)
		{
			return new LocalizedString("InboxRuleDescriptionBodyContainsWords", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0001046C File Offset: 0x0000E66C
		public static LocalizedString ADAttributeCustomAttribute3
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute3", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00010484 File Offset: 0x0000E684
		public static LocalizedString InboxRuleDescriptionSubjectOrBodyContainsWords(string words)
		{
			return new LocalizedString("InboxRuleDescriptionSubjectOrBodyContainsWords", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x000104AC File Offset: 0x0000E6AC
		public static LocalizedString ADAttributePagerNumber
		{
			get
			{
				return new LocalizedString("ADAttributePagerNumber", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x000104C3 File Offset: 0x0000E6C3
		public static LocalizedString MessageTypeVoicemail
		{
			get
			{
				return new LocalizedString("MessageTypeVoicemail", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x000104DA File Offset: 0x0000E6DA
		public static LocalizedString LinkedPredicateWithImportance
		{
			get
			{
				return new LocalizedString("LinkedPredicateWithImportance", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x000104F1 File Offset: 0x0000E6F1
		public static LocalizedString GenerateNotificationDescription
		{
			get
			{
				return new LocalizedString("GenerateNotificationDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00010508 File Offset: 0x0000E708
		public static LocalizedString MacroNameNotSpecified(string attribute)
		{
			return new LocalizedString("MacroNameNotSpecified", RulesTasksStrings.ResourceManager, new object[]
			{
				attribute
			});
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00010530 File Offset: 0x0000E730
		public static LocalizedString RuleDescriptionBlindCopyTo(string recipients)
		{
			return new LocalizedString("RuleDescriptionBlindCopyTo", RulesTasksStrings.ResourceManager, new object[]
			{
				recipients
			});
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00010558 File Offset: 0x0000E758
		public static LocalizedString ADAttributeTitle
		{
			get
			{
				return new LocalizedString("ADAttributeTitle", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0001056F File Offset: 0x0000E76F
		public static LocalizedString TextPatternsDisplayName
		{
			get
			{
				return new LocalizedString("TextPatternsDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00010588 File Offset: 0x0000E788
		public static LocalizedString OutlookProtectionRuleRmsTemplateNotFound(string name)
		{
			return new LocalizedString("OutlookProtectionRuleRmsTemplateNotFound", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000105B0 File Offset: 0x0000E7B0
		public static LocalizedString RuleDescriptionPrependSubject(string prefix)
		{
			return new LocalizedString("RuleDescriptionPrependSubject", RulesTasksStrings.ResourceManager, new object[]
			{
				prefix
			});
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x000105D8 File Offset: 0x0000E7D8
		public static LocalizedString LinkedPredicateHeaderMatchesException
		{
			get
			{
				return new LocalizedString("LinkedPredicateHeaderMatchesException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x000105EF File Offset: 0x0000E7EF
		public static LocalizedString EvaluationDisplayName
		{
			get
			{
				return new LocalizedString("EvaluationDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00010606 File Offset: 0x0000E806
		public static LocalizedString ToDLAddressDescription
		{
			get
			{
				return new LocalizedString("ToDLAddressDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001061D File Offset: 0x0000E81D
		public static LocalizedString DuplicateDataClassificationSpecified
		{
			get
			{
				return new LocalizedString("DuplicateDataClassificationSpecified", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00010634 File Offset: 0x0000E834
		public static LocalizedString RuleDescriptionRouteMessageOutboundRequireTls
		{
			get
			{
				return new LocalizedString("RuleDescriptionRouteMessageOutboundRequireTls", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001064B File Offset: 0x0000E84B
		public static LocalizedString EvaluationNotEqual
		{
			get
			{
				return new LocalizedString("EvaluationNotEqual", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00010662 File Offset: 0x0000E862
		public static LocalizedString StatusCodeDisplayName
		{
			get
			{
				return new LocalizedString("StatusCodeDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00010679 File Offset: 0x0000E879
		public static LocalizedString ConnectorNameDescription
		{
			get
			{
				return new LocalizedString("ConnectorNameDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00010690 File Offset: 0x0000E890
		public static LocalizedString RuleDescriptionSentTo(string addresses)
		{
			return new LocalizedString("RuleDescriptionSentTo", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses
			});
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000106B8 File Offset: 0x0000E8B8
		public static LocalizedString InboxRuleDescriptionWithSensitivity(string sensitivity)
		{
			return new LocalizedString("InboxRuleDescriptionWithSensitivity", RulesTasksStrings.ResourceManager, new object[]
			{
				sensitivity
			});
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x000106E0 File Offset: 0x0000E8E0
		public static LocalizedString RuleDescriptionAndDelimiter
		{
			get
			{
				return new LocalizedString("RuleDescriptionAndDelimiter", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000106F7 File Offset: 0x0000E8F7
		public static LocalizedString ADAttributeCustomAttribute14
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute14", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0001070E File Offset: 0x0000E90E
		public static LocalizedString LinkedActionApplyOME
		{
			get
			{
				return new LocalizedString("LinkedActionApplyOME", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00010728 File Offset: 0x0000E928
		public static LocalizedString ClientAccessRulesUsernamePatternRequired(string name)
		{
			return new LocalizedString("ClientAccessRulesUsernamePatternRequired", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00010750 File Offset: 0x0000E950
		public static LocalizedString ADAttributePhoneNumber
		{
			get
			{
				return new LocalizedString("ADAttributePhoneNumber", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00010767 File Offset: 0x0000E967
		public static LocalizedString ADAttributeOffice
		{
			get
			{
				return new LocalizedString("ADAttributeOffice", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0001077E File Offset: 0x0000E97E
		public static LocalizedString LinkedPredicateSenderInRecipientListException
		{
			get
			{
				return new LocalizedString("LinkedPredicateSenderInRecipientListException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00010798 File Offset: 0x0000E998
		public static LocalizedString ErrorTooManyRegexCharsInRuleCollection(int currentChars, long maxChars)
		{
			return new LocalizedString("ErrorTooManyRegexCharsInRuleCollection", RulesTasksStrings.ResourceManager, new object[]
			{
				currentChars,
				maxChars
			});
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000107D0 File Offset: 0x0000E9D0
		public static LocalizedString NoSmtpAddressForRecipientId(string recipId)
		{
			return new LocalizedString("NoSmtpAddressForRecipientId", RulesTasksStrings.ResourceManager, new object[]
			{
				recipId
			});
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000107F8 File Offset: 0x0000E9F8
		public static LocalizedString RuleDescriptionDisconnect
		{
			get
			{
				return new LocalizedString("RuleDescriptionDisconnect", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0001080F File Offset: 0x0000EA0F
		public static LocalizedString RuleDescriptionOrDelimiter
		{
			get
			{
				return new LocalizedString("RuleDescriptionOrDelimiter", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00010828 File Offset: 0x0000EA28
		public static LocalizedString RuleDescriptionCopyTo(string recipients)
		{
			return new LocalizedString("RuleDescriptionCopyTo", RulesTasksStrings.ResourceManager, new object[]
			{
				recipients
			});
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00010850 File Offset: 0x0000EA50
		public static LocalizedString RuleDescriptionGenerateIncidentReport(string reportDestination, string includeOriginalMail)
		{
			return new LocalizedString("RuleDescriptionGenerateIncidentReport", RulesTasksStrings.ResourceManager, new object[]
			{
				reportDestination,
				includeOriginalMail
			});
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0001087C File Offset: 0x0000EA7C
		public static LocalizedString LinkedActionSmtpRejectMessage
		{
			get
			{
				return new LocalizedString("LinkedActionSmtpRejectMessage", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00010893 File Offset: 0x0000EA93
		public static LocalizedString LinkedPredicateAnyOfToHeaderMemberOf
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfToHeaderMemberOf", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x000108AA File Offset: 0x0000EAAA
		public static LocalizedString ErrorInboxRuleMustHaveActions
		{
			get
			{
				return new LocalizedString("ErrorInboxRuleMustHaveActions", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x000108C1 File Offset: 0x0000EAC1
		public static LocalizedString QuarantineActionNotAvailable
		{
			get
			{
				return new LocalizedString("QuarantineActionNotAvailable", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000108D8 File Offset: 0x0000EAD8
		public static LocalizedString InboxRuleDescriptionFrom(string address)
		{
			return new LocalizedString("InboxRuleDescriptionFrom", RulesTasksStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00010900 File Offset: 0x0000EB00
		public static LocalizedString LinkedPredicateAttachmentPropertyContainsWords
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentPropertyContainsWords", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00010917 File Offset: 0x0000EB17
		public static LocalizedString LinkedPredicateAnyOfToHeaderException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfToHeaderException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001092E File Offset: 0x0000EB2E
		public static LocalizedString EventMessageDescription
		{
			get
			{
				return new LocalizedString("EventMessageDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00010945 File Offset: 0x0000EB45
		public static LocalizedString LinkedPredicateAnyOfRecipientAddressContainsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfRecipientAddressContainsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0001095C File Offset: 0x0000EB5C
		public static LocalizedString RmsTemplateDisplayName
		{
			get
			{
				return new LocalizedString("RmsTemplateDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00010973 File Offset: 0x0000EB73
		public static LocalizedString LinkedPredicateRecipientAttributeContainsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateRecipientAttributeContainsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001098C File Offset: 0x0000EB8C
		public static LocalizedString RuleDescriptionSetAuditSeverity(string severityLevel)
		{
			return new LocalizedString("RuleDescriptionSetAuditSeverity", RulesTasksStrings.ResourceManager, new object[]
			{
				severityLevel
			});
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000109B4 File Offset: 0x0000EBB4
		public static LocalizedString AttachmentMetadataParameterContainsEmptyWords(string input)
		{
			return new LocalizedString("AttachmentMetadataParameterContainsEmptyWords", RulesTasksStrings.ResourceManager, new object[]
			{
				input
			});
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x000109DC File Offset: 0x0000EBDC
		public static LocalizedString LinkedPredicateHasClassification
		{
			get
			{
				return new LocalizedString("LinkedPredicateHasClassification", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x000109F3 File Offset: 0x0000EBF3
		public static LocalizedString InboxRuleDescriptionSentOnlyToMe
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionSentOnlyToMe", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00010A0A File Offset: 0x0000EC0A
		public static LocalizedString LinkedPredicateAttachmentIsPasswordProtected
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentIsPasswordProtected", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00010A21 File Offset: 0x0000EC21
		public static LocalizedString PromptToUpgradeRulesFormat
		{
			get
			{
				return new LocalizedString("PromptToUpgradeRulesFormat", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00010A38 File Offset: 0x0000EC38
		public static LocalizedString NotifySenderActionRequiresMcdcPredicate
		{
			get
			{
				return new LocalizedString("NotifySenderActionRequiresMcdcPredicate", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00010A4F File Offset: 0x0000EC4F
		public static LocalizedString LinkedActionGenerateNotificationAction
		{
			get
			{
				return new LocalizedString("LinkedActionGenerateNotificationAction", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00010A66 File Offset: 0x0000EC66
		public static LocalizedString ClassificationDisplayName
		{
			get
			{
				return new LocalizedString("ClassificationDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00010A7D File Offset: 0x0000EC7D
		public static LocalizedString LinkedActionPrependSubject
		{
			get
			{
				return new LocalizedString("LinkedActionPrependSubject", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00010A94 File Offset: 0x0000EC94
		public static LocalizedString InvalidSmtpAddress(string address)
		{
			return new LocalizedString("InvalidSmtpAddress", RulesTasksStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00010ABC File Offset: 0x0000ECBC
		public static LocalizedString ADAttributeStreet
		{
			get
			{
				return new LocalizedString("ADAttributeStreet", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00010AD3 File Offset: 0x0000ECD3
		public static LocalizedString FromDLAddressDisplayName
		{
			get
			{
				return new LocalizedString("FromDLAddressDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00010AEA File Offset: 0x0000ECEA
		public static LocalizedString LinkedPredicateMessageTypeMatchesException
		{
			get
			{
				return new LocalizedString("LinkedPredicateMessageTypeMatchesException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00010B01 File Offset: 0x0000ED01
		public static LocalizedString ADAttributeCustomAttribute15
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute15", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00010B18 File Offset: 0x0000ED18
		public static LocalizedString RuleDescriptionQuarantine
		{
			get
			{
				return new LocalizedString("RuleDescriptionQuarantine", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00010B2F File Offset: 0x0000ED2F
		public static LocalizedString MissingDataClassificationsParameter
		{
			get
			{
				return new LocalizedString("MissingDataClassificationsParameter", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00010B48 File Offset: 0x0000ED48
		public static LocalizedString InboxRuleDescriptionWithImportance(string importance)
		{
			return new LocalizedString("InboxRuleDescriptionWithImportance", RulesTasksStrings.ResourceManager, new object[]
			{
				importance
			});
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00010B70 File Offset: 0x0000ED70
		public static LocalizedString RuleDescriptionApplyClassification(string classification)
		{
			return new LocalizedString("RuleDescriptionApplyClassification", RulesTasksStrings.ResourceManager, new object[]
			{
				classification
			});
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00010B98 File Offset: 0x0000ED98
		public static LocalizedString LinkedPredicateHasNoClassificationException
		{
			get
			{
				return new LocalizedString("LinkedPredicateHasNoClassificationException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00010BAF File Offset: 0x0000EDAF
		public static LocalizedString LinkedPredicateFromScope
		{
			get
			{
				return new LocalizedString("LinkedPredicateFromScope", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00010BC8 File Offset: 0x0000EDC8
		public static LocalizedString RuleDescriptionAnyOfToHeader(string addresses)
		{
			return new LocalizedString("RuleDescriptionAnyOfToHeader", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses
			});
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00010BF0 File Offset: 0x0000EDF0
		public static LocalizedString ManagementRelationshipManager
		{
			get
			{
				return new LocalizedString("ManagementRelationshipManager", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00010C07 File Offset: 0x0000EE07
		public static LocalizedString LinkedPredicateSubjectMatchesException
		{
			get
			{
				return new LocalizedString("LinkedPredicateSubjectMatchesException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00010C1E File Offset: 0x0000EE1E
		public static LocalizedString LinkedPredicateSentTo
		{
			get
			{
				return new LocalizedString("LinkedPredicateSentTo", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00010C35 File Offset: 0x0000EE35
		public static LocalizedString LinkedPredicateAnyOfCcHeaderMemberOf
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfCcHeaderMemberOf", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00010C4C File Offset: 0x0000EE4C
		public static LocalizedString InboxRuleDescriptionFolderNotFound
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionFolderNotFound", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00010C63 File Offset: 0x0000EE63
		public static LocalizedString PrefixDescription
		{
			get
			{
				return new LocalizedString("PrefixDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00010C7C File Offset: 0x0000EE7C
		public static LocalizedString RuleDescriptionAttachmentPropertyContainsWords(string words)
		{
			return new LocalizedString("RuleDescriptionAttachmentPropertyContainsWords", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00010CA4 File Offset: 0x0000EEA4
		public static LocalizedString TestClientAccessRuleUserNotFoundOrMoreThanOne(string name)
		{
			return new LocalizedString("TestClientAccessRuleUserNotFoundOrMoreThanOne", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00010CCC File Offset: 0x0000EECC
		public static LocalizedString InboxRuleDescriptionWithSizeBetween(string min, string max)
		{
			return new LocalizedString("InboxRuleDescriptionWithSizeBetween", RulesTasksStrings.ResourceManager, new object[]
			{
				min,
				max
			});
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00010CF8 File Offset: 0x0000EEF8
		public static LocalizedString EvaluatedUserSender
		{
			get
			{
				return new LocalizedString("EvaluatedUserSender", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00010D0F File Offset: 0x0000EF0F
		public static LocalizedString RuleDescriptionDeleteMessage
		{
			get
			{
				return new LocalizedString("RuleDescriptionDeleteMessage", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00010D28 File Offset: 0x0000EF28
		public static LocalizedString InvalidRecipient(string recipient)
		{
			return new LocalizedString("InvalidRecipient", RulesTasksStrings.ResourceManager, new object[]
			{
				recipient
			});
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00010D50 File Offset: 0x0000EF50
		public static LocalizedString LinkedPredicateHasSenderOverrideException
		{
			get
			{
				return new LocalizedString("LinkedPredicateHasSenderOverrideException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00010D67 File Offset: 0x0000EF67
		public static LocalizedString SubTypeDisplayName
		{
			get
			{
				return new LocalizedString("SubTypeDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00010D7E File Offset: 0x0000EF7E
		public static LocalizedString ParameterVersionMismatch
		{
			get
			{
				return new LocalizedString("ParameterVersionMismatch", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00010D95 File Offset: 0x0000EF95
		public static LocalizedString LinkedPredicateManagementRelationship
		{
			get
			{
				return new LocalizedString("LinkedPredicateManagementRelationship", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00010DAC File Offset: 0x0000EFAC
		public static LocalizedString LinkedActionRouteMessageOutboundConnector
		{
			get
			{
				return new LocalizedString("LinkedActionRouteMessageOutboundConnector", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00010DC4 File Offset: 0x0000EFC4
		public static LocalizedString HeaderNameNotAllowed(string headerName)
		{
			return new LocalizedString("HeaderNameNotAllowed", RulesTasksStrings.ResourceManager, new object[]
			{
				headerName
			});
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00010DEC File Offset: 0x0000EFEC
		public static LocalizedString LinkedActionModerateMessageByUser
		{
			get
			{
				return new LocalizedString("LinkedActionModerateMessageByUser", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00010E04 File Offset: 0x0000F004
		public static LocalizedString InboxRuleDescriptionSendTextMessageNotificationTo(string address)
		{
			return new LocalizedString("InboxRuleDescriptionSendTextMessageNotificationTo", RulesTasksStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00010E2C File Offset: 0x0000F02C
		public static LocalizedString LinkedActionAddToRecipient
		{
			get
			{
				return new LocalizedString("LinkedActionAddToRecipient", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00010E43 File Offset: 0x0000F043
		public static LocalizedString IncidentReportContentDisplayName
		{
			get
			{
				return new LocalizedString("IncidentReportContentDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00010E5C File Offset: 0x0000F05C
		public static LocalizedString RuleDescriptionAnyOfToCcHeader(string addresses)
		{
			return new LocalizedString("RuleDescriptionAnyOfToCcHeader", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses
			});
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00010E84 File Offset: 0x0000F084
		public static LocalizedString LinkedPredicateSubjectContains
		{
			get
			{
				return new LocalizedString("LinkedPredicateSubjectContains", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00010E9C File Offset: 0x0000F09C
		public static LocalizedString RuleDescriptionRouteMessageOutboundConnector(string connectorName)
		{
			return new LocalizedString("RuleDescriptionRouteMessageOutboundConnector", RulesTasksStrings.ResourceManager, new object[]
			{
				connectorName
			});
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00010EC4 File Offset: 0x0000F0C4
		public static LocalizedString LinkedPredicateRecipientAddressContainsWordsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateRecipientAddressContainsWordsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00010EDB File Offset: 0x0000F0DB
		public static LocalizedString FallbackWrap
		{
			get
			{
				return new LocalizedString("FallbackWrap", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00010EF2 File Offset: 0x0000F0F2
		public static LocalizedString ADAttributeCustomAttribute4
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute4", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00010F0C File Offset: 0x0000F10C
		public static LocalizedString RuleDescriptionHeaderMatches(string header, string patterns)
		{
			return new LocalizedString("RuleDescriptionHeaderMatches", RulesTasksStrings.ResourceManager, new object[]
			{
				header,
				patterns
			});
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00010F38 File Offset: 0x0000F138
		public static LocalizedString ADAttributeEvaluationTypeContains
		{
			get
			{
				return new LocalizedString("ADAttributeEvaluationTypeContains", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00010F4F File Offset: 0x0000F14F
		public static LocalizedString ADAttributeEvaluationTypeDescription
		{
			get
			{
				return new LocalizedString("ADAttributeEvaluationTypeDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00010F66 File Offset: 0x0000F166
		public static LocalizedString InvalidPredicate
		{
			get
			{
				return new LocalizedString("InvalidPredicate", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00010F7D File Offset: 0x0000F17D
		public static LocalizedString ADAttributeDepartment
		{
			get
			{
				return new LocalizedString("ADAttributeDepartment", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00010F94 File Offset: 0x0000F194
		public static LocalizedString ExternalScopeInvalidInDc(string scope, string stringInOrganization, string notInOrganization)
		{
			return new LocalizedString("ExternalScopeInvalidInDc", RulesTasksStrings.ResourceManager, new object[]
			{
				scope,
				stringInOrganization,
				notInOrganization
			});
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		public static LocalizedString RuleStateDisabled
		{
			get
			{
				return new LocalizedString("RuleStateDisabled", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00010FDB File Offset: 0x0000F1DB
		public static LocalizedString LinkedPredicateSclOverException
		{
			get
			{
				return new LocalizedString("LinkedPredicateSclOverException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00010FF2 File Offset: 0x0000F1F2
		public static LocalizedString ConnectorNameDisplayName
		{
			get
			{
				return new LocalizedString("ConnectorNameDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00011009 File Offset: 0x0000F209
		public static LocalizedString LinkedPredicateSentToException
		{
			get
			{
				return new LocalizedString("LinkedPredicateSentToException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00011020 File Offset: 0x0000F220
		public static LocalizedString EnhancedStatusCodeDescription
		{
			get
			{
				return new LocalizedString("EnhancedStatusCodeDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00011037 File Offset: 0x0000F237
		public static LocalizedString LinkedActionDisconnect
		{
			get
			{
				return new LocalizedString("LinkedActionDisconnect", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0001104E File Offset: 0x0000F24E
		public static LocalizedString ADAttributePOBox
		{
			get
			{
				return new LocalizedString("ADAttributePOBox", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00011065 File Offset: 0x0000F265
		public static LocalizedString RuleDescriptionRemoveOME
		{
			get
			{
				return new LocalizedString("RuleDescriptionRemoveOME", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0001107C File Offset: 0x0000F27C
		public static LocalizedString MessageTypeApprovalRequest
		{
			get
			{
				return new LocalizedString("MessageTypeApprovalRequest", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00011094 File Offset: 0x0000F294
		public static LocalizedString RuleDescriptionAddManagerAsRecipientType(string recipientType)
		{
			return new LocalizedString("RuleDescriptionAddManagerAsRecipientType", RulesTasksStrings.ResourceManager, new object[]
			{
				recipientType
			});
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x000110BC File Offset: 0x0000F2BC
		public static LocalizedString ADAttributeFaxNumber
		{
			get
			{
				return new LocalizedString("ADAttributeFaxNumber", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000110D4 File Offset: 0x0000F2D4
		public static LocalizedString ErrorTooManyAddedRecipientsInRuleCollection(int currentRecipients, int maxRecipients)
		{
			return new LocalizedString("ErrorTooManyAddedRecipientsInRuleCollection", RulesTasksStrings.ResourceManager, new object[]
			{
				currentRecipients,
				maxRecipients
			});
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0001110A File Offset: 0x0000F30A
		public static LocalizedString ErrorAccessingTransportSettings
		{
			get
			{
				return new LocalizedString("ErrorAccessingTransportSettings", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00011121 File Offset: 0x0000F321
		public static LocalizedString EvaluationEqual
		{
			get
			{
				return new LocalizedString("EvaluationEqual", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00011138 File Offset: 0x0000F338
		public static LocalizedString LinkedPredicateRecipientAttributeContains
		{
			get
			{
				return new LocalizedString("LinkedPredicateRecipientAttributeContains", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0001114F File Offset: 0x0000F34F
		public static LocalizedString LinkedPredicateAttachmentSizeOver
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentSizeOver", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00011166 File Offset: 0x0000F366
		public static LocalizedString LinkedPredicateFrom
		{
			get
			{
				return new LocalizedString("LinkedPredicateFrom", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001117D File Offset: 0x0000F37D
		public static LocalizedString LinkedPredicateAnyOfToCcHeaderException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfToCcHeaderException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00011194 File Offset: 0x0000F394
		public static LocalizedString CommentsHaveInvalidChars(int ch)
		{
			return new LocalizedString("CommentsHaveInvalidChars", RulesTasksStrings.ResourceManager, new object[]
			{
				ch
			});
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000111C4 File Offset: 0x0000F3C4
		public static LocalizedString InvalidMessageDataClassificationParameterMinGreaterThanMax(string paramName1, string paramName2)
		{
			return new LocalizedString("InvalidMessageDataClassificationParameterMinGreaterThanMax", RulesTasksStrings.ResourceManager, new object[]
			{
				paramName1,
				paramName2
			});
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x000111F0 File Offset: 0x0000F3F0
		public static LocalizedString LinkedPredicateSubjectOrBodyContains
		{
			get
			{
				return new LocalizedString("LinkedPredicateSubjectOrBodyContains", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00011207 File Offset: 0x0000F407
		public static LocalizedString LinkedPredicateAttachmentContainsWordsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentContainsWordsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00011220 File Offset: 0x0000F420
		public static LocalizedString RuleDescriptionAttachmentContainsWords(string words)
		{
			return new LocalizedString("RuleDescriptionAttachmentContainsWords", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00011248 File Offset: 0x0000F448
		public static LocalizedString RuleDescriptionPrependHtmlDisclaimer(string text)
		{
			return new LocalizedString("RuleDescriptionPrependHtmlDisclaimer", RulesTasksStrings.ResourceManager, new object[]
			{
				text
			});
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00011270 File Offset: 0x0000F470
		public static LocalizedString RuleDescriptionFromScope(string scope)
		{
			return new LocalizedString("RuleDescriptionFromScope", RulesTasksStrings.ResourceManager, new object[]
			{
				scope
			});
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00011298 File Offset: 0x0000F498
		public static LocalizedString InboxRuleDescriptionFlaggedForNoResponse
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionFlaggedForNoResponse", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x000112AF File Offset: 0x0000F4AF
		public static LocalizedString StatusCodeDescription
		{
			get
			{
				return new LocalizedString("StatusCodeDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000112C8 File Offset: 0x0000F4C8
		public static LocalizedString RuleDescriptionContentCharacterSetContainsWords(string words)
		{
			return new LocalizedString("RuleDescriptionContentCharacterSetContainsWords", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x000112F0 File Offset: 0x0000F4F0
		public static LocalizedString LinkedPredicateFromScopeException
		{
			get
			{
				return new LocalizedString("LinkedPredicateFromScopeException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00011307 File Offset: 0x0000F507
		public static LocalizedString LinkedPredicateAttachmentProcessingLimitExceeded
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentProcessingLimitExceeded", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001131E File Offset: 0x0000F51E
		public static LocalizedString ManagementRelationshipDirectReport
		{
			get
			{
				return new LocalizedString("ManagementRelationshipDirectReport", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00011335 File Offset: 0x0000F535
		public static LocalizedString ADAttributeZipCode
		{
			get
			{
				return new LocalizedString("ADAttributeZipCode", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001134C File Offset: 0x0000F54C
		public static LocalizedString SetRuleSyncAcrossDifferentVersionsNeeded
		{
			get
			{
				return new LocalizedString("SetRuleSyncAcrossDifferentVersionsNeeded", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00011363 File Offset: 0x0000F563
		public static LocalizedString IncidentReportContentDescription
		{
			get
			{
				return new LocalizedString("IncidentReportContentDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0001137A File Offset: 0x0000F57A
		public static LocalizedString InvalidIncidentReportOriginalMail
		{
			get
			{
				return new LocalizedString("InvalidIncidentReportOriginalMail", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00011391 File Offset: 0x0000F591
		public static LocalizedString LinkedPredicateRecipientAttributeMatchesException
		{
			get
			{
				return new LocalizedString("LinkedPredicateRecipientAttributeMatchesException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x000113A8 File Offset: 0x0000F5A8
		public static LocalizedString ClientAccessRulesAuthenticationTypeInvalid
		{
			get
			{
				return new LocalizedString("ClientAccessRulesAuthenticationTypeInvalid", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x000113BF File Offset: 0x0000F5BF
		public static LocalizedString ClassificationDescription
		{
			get
			{
				return new LocalizedString("ClassificationDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x000113D6 File Offset: 0x0000F5D6
		public static LocalizedString ReportDestinationDescription
		{
			get
			{
				return new LocalizedString("ReportDestinationDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000113ED File Offset: 0x0000F5ED
		public static LocalizedString LinkedPredicateSentToMemberOf
		{
			get
			{
				return new LocalizedString("LinkedPredicateSentToMemberOf", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00011404 File Offset: 0x0000F604
		public static LocalizedString NoRecipientsForRecipientId(string recipId)
		{
			return new LocalizedString("NoRecipientsForRecipientId", RulesTasksStrings.ResourceManager, new object[]
			{
				recipId
			});
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0001142C File Offset: 0x0000F62C
		public static LocalizedString ToScopeDescription
		{
			get
			{
				return new LocalizedString("ToScopeDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00011444 File Offset: 0x0000F644
		public static LocalizedString RuleDescriptionMessageContainsDataClassifications(string lists)
		{
			return new LocalizedString("RuleDescriptionMessageContainsDataClassifications", RulesTasksStrings.ResourceManager, new object[]
			{
				lists
			});
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001146C File Offset: 0x0000F66C
		public static LocalizedString AttachmentMetadataPropertyNotSpecified(string input)
		{
			return new LocalizedString("AttachmentMetadataPropertyNotSpecified", RulesTasksStrings.ResourceManager, new object[]
			{
				input
			});
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00011494 File Offset: 0x0000F694
		public static LocalizedString WordsDisplayName
		{
			get
			{
				return new LocalizedString("WordsDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x000114AB File Offset: 0x0000F6AB
		public static LocalizedString ModerateActionMustBeTheOnlyAction
		{
			get
			{
				return new LocalizedString("ModerateActionMustBeTheOnlyAction", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x000114C2 File Offset: 0x0000F6C2
		public static LocalizedString InvalidRejectEnhancedStatus
		{
			get
			{
				return new LocalizedString("InvalidRejectEnhancedStatus", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x000114D9 File Offset: 0x0000F6D9
		public static LocalizedString ADAttributeFirstName
		{
			get
			{
				return new LocalizedString("ADAttributeFirstName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x000114F0 File Offset: 0x0000F6F0
		public static LocalizedString LinkedPredicateAnyOfCcHeader
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfCcHeader", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00011507 File Offset: 0x0000F707
		public static LocalizedString ErrorInvalidCharException
		{
			get
			{
				return new LocalizedString("ErrorInvalidCharException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0001151E File Offset: 0x0000F71E
		public static LocalizedString LinkedPredicateMessageSizeOver
		{
			get
			{
				return new LocalizedString("LinkedPredicateMessageSizeOver", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00011535 File Offset: 0x0000F735
		public static LocalizedString LinkedPredicateAttachmentProcessingLimitExceededException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentProcessingLimitExceededException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0001154C File Offset: 0x0000F74C
		public static LocalizedString ManagementRelationshipDescription
		{
			get
			{
				return new LocalizedString("ManagementRelationshipDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00011563 File Offset: 0x0000F763
		public static LocalizedString LinkedPredicateSenderDomainIs
		{
			get
			{
				return new LocalizedString("LinkedPredicateSenderDomainIs", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0001157A File Offset: 0x0000F77A
		public static LocalizedString InternalUser
		{
			get
			{
				return new LocalizedString("InternalUser", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00011591 File Offset: 0x0000F791
		public static LocalizedString InvalidRmsTemplate
		{
			get
			{
				return new LocalizedString("InvalidRmsTemplate", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x000115A8 File Offset: 0x0000F7A8
		public static LocalizedString HeaderValueDisplayName
		{
			get
			{
				return new LocalizedString("HeaderValueDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000115C0 File Offset: 0x0000F7C0
		public static LocalizedString RuleDescriptionFrom(string addresses)
		{
			return new LocalizedString("RuleDescriptionFrom", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses
			});
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x000115E8 File Offset: 0x0000F7E8
		public static LocalizedString EdgeParameterNotValidOnHubRole
		{
			get
			{
				return new LocalizedString("EdgeParameterNotValidOnHubRole", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x000115FF File Offset: 0x0000F7FF
		public static LocalizedString BccRecipientType
		{
			get
			{
				return new LocalizedString("BccRecipientType", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00011618 File Offset: 0x0000F818
		public static LocalizedString RuleDescriptionGenerateNotification(string content)
		{
			return new LocalizedString("RuleDescriptionGenerateNotification", RulesTasksStrings.ResourceManager, new object[]
			{
				content
			});
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00011640 File Offset: 0x0000F840
		public static LocalizedString InboxRuleDescriptionRecipientAddressContainsWords(string words)
		{
			return new LocalizedString("InboxRuleDescriptionRecipientAddressContainsWords", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00011668 File Offset: 0x0000F868
		public static LocalizedString RejectUnlessFalsePositiveOverrideActionType
		{
			get
			{
				return new LocalizedString("RejectUnlessFalsePositiveOverrideActionType", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00011680 File Offset: 0x0000F880
		public static LocalizedString RuleDescriptionAnyOfRecipientAddressContains(string words)
		{
			return new LocalizedString("RuleDescriptionAnyOfRecipientAddressContains", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x000116A8 File Offset: 0x0000F8A8
		public static LocalizedString LinkedPredicateSenderAttributeContains
		{
			get
			{
				return new LocalizedString("LinkedPredicateSenderAttributeContains", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x000116BF File Offset: 0x0000F8BF
		public static LocalizedString LinkedPredicateAttachmentHasExecutableContentPredicate
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentHasExecutableContentPredicate", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000116D8 File Offset: 0x0000F8D8
		public static LocalizedString RuleDescriptionSetScl(string sclValue)
		{
			return new LocalizedString("RuleDescriptionSetScl", RulesTasksStrings.ResourceManager, new object[]
			{
				sclValue
			});
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00011700 File Offset: 0x0000F900
		public static LocalizedString RuleDescriptionSubjectOrBodyMatches(string patterns)
		{
			return new LocalizedString("RuleDescriptionSubjectOrBodyMatches", RulesTasksStrings.ResourceManager, new object[]
			{
				patterns
			});
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00011728 File Offset: 0x0000F928
		public static LocalizedString PromptToRemoveLogEventAction
		{
			get
			{
				return new LocalizedString("PromptToRemoveLogEventAction", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0001173F File Offset: 0x0000F93F
		public static LocalizedString ToAddressesDescription
		{
			get
			{
				return new LocalizedString("ToAddressesDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00011756 File Offset: 0x0000F956
		public static LocalizedString MessageTypeSigned
		{
			get
			{
				return new LocalizedString("MessageTypeSigned", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00011770 File Offset: 0x0000F970
		public static LocalizedString RuleDescriptionSubjectMatches(string patterns)
		{
			return new LocalizedString("RuleDescriptionSubjectMatches", RulesTasksStrings.ResourceManager, new object[]
			{
				patterns
			});
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00011798 File Offset: 0x0000F998
		public static LocalizedString RejectReasonDescription
		{
			get
			{
				return new LocalizedString("RejectReasonDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x000117AF File Offset: 0x0000F9AF
		public static LocalizedString InboxRuleDescriptionFlaggedForFYI
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionFlaggedForFYI", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x000117C6 File Offset: 0x0000F9C6
		public static LocalizedString LinkedPredicateRecipientAttributeMatches
		{
			get
			{
				return new LocalizedString("LinkedPredicateRecipientAttributeMatches", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x000117DD File Offset: 0x0000F9DD
		public static LocalizedString LinkedPredicateAnyOfRecipientAddressMatches
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfRecipientAddressMatches", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x000117F4 File Offset: 0x0000F9F4
		public static LocalizedString LinkedPredicateRecipientAddressMatchesPatterns
		{
			get
			{
				return new LocalizedString("LinkedPredicateRecipientAddressMatchesPatterns", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0001180B File Offset: 0x0000FA0B
		public static LocalizedString SclValueDisplayName
		{
			get
			{
				return new LocalizedString("SclValueDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00011824 File Offset: 0x0000FA24
		public static LocalizedString RuleDescriptionRejectMessage(string rejectText, string rejectCode)
		{
			return new LocalizedString("RuleDescriptionRejectMessage", RulesTasksStrings.ResourceManager, new object[]
			{
				rejectText,
				rejectCode
			});
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00011850 File Offset: 0x0000FA50
		public static LocalizedString RuleDescriptionNotifySenderRejectMessage(string rejectText, string rejectCode)
		{
			return new LocalizedString("RuleDescriptionNotifySenderRejectMessage", RulesTasksStrings.ResourceManager, new object[]
			{
				rejectText,
				rejectCode
			});
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001187C File Offset: 0x0000FA7C
		public static LocalizedString PrependDisclaimer
		{
			get
			{
				return new LocalizedString("PrependDisclaimer", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00011893 File Offset: 0x0000FA93
		public static LocalizedString ClientAccessRuleSetDatacenterAdminsOnlyError
		{
			get
			{
				return new LocalizedString("ClientAccessRuleSetDatacenterAdminsOnlyError", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x000118AA File Offset: 0x0000FAAA
		public static LocalizedString LinkedPredicateAnyOfToCcHeaderMemberOfException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfToCcHeaderMemberOfException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x000118C1 File Offset: 0x0000FAC1
		public static LocalizedString ADAttributeCustomAttribute7
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute7", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000118D8 File Offset: 0x0000FAD8
		public static LocalizedString AtatchmentExtensionMatchesWordsParameterContainsWordsThatStartWithDot(string predicateName, string Words)
		{
			return new LocalizedString("AtatchmentExtensionMatchesWordsParameterContainsWordsThatStartWithDot", RulesTasksStrings.ResourceManager, new object[]
			{
				predicateName,
				Words
			});
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00011904 File Offset: 0x0000FB04
		public static LocalizedString InvalidMessageDataClassificationParameterLessThanOne(string paramName)
		{
			return new LocalizedString("InvalidMessageDataClassificationParameterLessThanOne", RulesTasksStrings.ResourceManager, new object[]
			{
				paramName
			});
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001192C File Offset: 0x0000FB2C
		public static LocalizedString FromDLAddressDescription
		{
			get
			{
				return new LocalizedString("FromDLAddressDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00011943 File Offset: 0x0000FB43
		public static LocalizedString RejectReasonDisplayName
		{
			get
			{
				return new LocalizedString("RejectReasonDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001195A File Offset: 0x0000FB5A
		public static LocalizedString LinkedPredicateSenderIpRanges
		{
			get
			{
				return new LocalizedString("LinkedPredicateSenderIpRanges", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00011971 File Offset: 0x0000FB71
		public static LocalizedString LinkedPredicateMessageContainsDataClassifications
		{
			get
			{
				return new LocalizedString("LinkedPredicateMessageContainsDataClassifications", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00011988 File Offset: 0x0000FB88
		public static LocalizedString ErrorMaxParameterLengthExceeded(string parameterName, int maxValueLength)
		{
			return new LocalizedString("ErrorMaxParameterLengthExceeded", RulesTasksStrings.ResourceManager, new object[]
			{
				parameterName,
				maxValueLength
			});
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000119BC File Offset: 0x0000FBBC
		public static LocalizedString RuleDescriptionManagerIs(string evaluatesUser, string addresses)
		{
			return new LocalizedString("RuleDescriptionManagerIs", RulesTasksStrings.ResourceManager, new object[]
			{
				evaluatesUser,
				addresses
			});
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x000119E8 File Offset: 0x0000FBE8
		public static LocalizedString InvalidDisclaimerMacroName(string invalidMacroName)
		{
			return new LocalizedString("InvalidDisclaimerMacroName", RulesTasksStrings.ResourceManager, new object[]
			{
				invalidMacroName
			});
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00011A10 File Offset: 0x0000FC10
		public static LocalizedString LinkedPredicateAnyOfToHeaderMemberOfException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfToHeaderMemberOfException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00011A27 File Offset: 0x0000FC27
		public static LocalizedString SetAuditSeverityDescription
		{
			get
			{
				return new LocalizedString("SetAuditSeverityDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00011A40 File Offset: 0x0000FC40
		public static LocalizedString RuleDescriptionRecipientAddressContains(string words)
		{
			return new LocalizedString("RuleDescriptionRecipientAddressContains", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00011A68 File Offset: 0x0000FC68
		public static LocalizedString HubServerVersionNotFound(string server)
		{
			return new LocalizedString("HubServerVersionNotFound", RulesTasksStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00011A90 File Offset: 0x0000FC90
		public static LocalizedString RuleDescriptionAttachmentIsUnsupported
		{
			get
			{
				return new LocalizedString("RuleDescriptionAttachmentIsUnsupported", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00011AA7 File Offset: 0x0000FCA7
		public static LocalizedString ADAttributeCustomAttribute6
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute6", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00011AC0 File Offset: 0x0000FCC0
		public static LocalizedString RuleDescriptionSenderDomainIs(string domains)
		{
			return new LocalizedString("RuleDescriptionSenderDomainIs", RulesTasksStrings.ResourceManager, new object[]
			{
				domains
			});
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00011AE8 File Offset: 0x0000FCE8
		public static LocalizedString LinkedPredicateBetweenMemberOfException
		{
			get
			{
				return new LocalizedString("LinkedPredicateBetweenMemberOfException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00011AFF File Offset: 0x0000FCFF
		public static LocalizedString ADAttributeCity
		{
			get
			{
				return new LocalizedString("ADAttributeCity", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00011B16 File Offset: 0x0000FD16
		public static LocalizedString DlpRuleMustContainMessageContainsDataClassificationPredicate
		{
			get
			{
				return new LocalizedString("DlpRuleMustContainMessageContainsDataClassificationPredicate", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00011B2D File Offset: 0x0000FD2D
		public static LocalizedString ImportanceDescription
		{
			get
			{
				return new LocalizedString("ImportanceDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00011B44 File Offset: 0x0000FD44
		public static LocalizedString AppendDisclaimer
		{
			get
			{
				return new LocalizedString("AppendDisclaimer", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00011B5C File Offset: 0x0000FD5C
		public static LocalizedString CorruptRuleCollection(string reason)
		{
			return new LocalizedString("CorruptRuleCollection", RulesTasksStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00011B84 File Offset: 0x0000FD84
		public static LocalizedString NegativePriority
		{
			get
			{
				return new LocalizedString("NegativePriority", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00011B9B File Offset: 0x0000FD9B
		public static LocalizedString MessageSizeDisplayName
		{
			get
			{
				return new LocalizedString("MessageSizeDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00011BB4 File Offset: 0x0000FDB4
		public static LocalizedString RuleDescriptionAnyOfCcHeaderMemberOf(string addresses)
		{
			return new LocalizedString("RuleDescriptionAnyOfCcHeaderMemberOf", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses
			});
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00011BDC File Offset: 0x0000FDDC
		public static LocalizedString LinkedPredicateSenderAttributeMatches
		{
			get
			{
				return new LocalizedString("LinkedPredicateSenderAttributeMatches", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00011BF3 File Offset: 0x0000FDF3
		public static LocalizedString LinkedPredicateFromException
		{
			get
			{
				return new LocalizedString("LinkedPredicateFromException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00011C0C File Offset: 0x0000FE0C
		public static LocalizedString RuleDescriptionBetweenMemberOf(string addresses, string addresses2)
		{
			return new LocalizedString("RuleDescriptionBetweenMemberOf", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses,
				addresses2
			});
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00011C38 File Offset: 0x0000FE38
		public static LocalizedString LinkedPredicateAnyOfCcHeaderMemberOfException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfCcHeaderMemberOfException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00011C4F File Offset: 0x0000FE4F
		public static LocalizedString LinkedPredicateManagementRelationshipException
		{
			get
			{
				return new LocalizedString("LinkedPredicateManagementRelationshipException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00011C66 File Offset: 0x0000FE66
		public static LocalizedString LinkedPredicateSenderDomainIsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateSenderDomainIsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00011C7D File Offset: 0x0000FE7D
		public static LocalizedString ToDLAddressDisplayName
		{
			get
			{
				return new LocalizedString("ToDLAddressDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00011C94 File Offset: 0x0000FE94
		public static LocalizedString ADAttributeEvaluationTypeDisplayName
		{
			get
			{
				return new LocalizedString("ADAttributeEvaluationTypeDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00011CAB File Offset: 0x0000FEAB
		public static LocalizedString DisclaimerLocationDisplayName
		{
			get
			{
				return new LocalizedString("DisclaimerLocationDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00011CC2 File Offset: 0x0000FEC2
		public static LocalizedString ImportanceHigh
		{
			get
			{
				return new LocalizedString("ImportanceHigh", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00011CD9 File Offset: 0x0000FED9
		public static LocalizedString InboxRuleDescriptionStopProcessingRules
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionStopProcessingRules", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00011CF0 File Offset: 0x0000FEF0
		public static LocalizedString CannotCreateRuleDueToVersion(string name)
		{
			return new LocalizedString("CannotCreateRuleDueToVersion", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00011D18 File Offset: 0x0000FF18
		public static LocalizedString InboxRuleDescriptionIf
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionIf", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00011D30 File Offset: 0x0000FF30
		public static LocalizedString RuleDescriptionAddToRecipient(string recipients)
		{
			return new LocalizedString("RuleDescriptionAddToRecipient", RulesTasksStrings.ResourceManager, new object[]
			{
				recipients
			});
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00011D58 File Offset: 0x0000FF58
		public static LocalizedString LinkedActionRemoveOME
		{
			get
			{
				return new LocalizedString("LinkedActionRemoveOME", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00011D6F File Offset: 0x0000FF6F
		public static LocalizedString RuleDescriptionAttachmentIsPasswordProtected
		{
			get
			{
				return new LocalizedString("RuleDescriptionAttachmentIsPasswordProtected", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00011D86 File Offset: 0x0000FF86
		public static LocalizedString LinkedPredicateSubjectOrBodyContainsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateSubjectOrBodyContainsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00011DA0 File Offset: 0x0000FFA0
		public static LocalizedString InvalidMessageClassification(string classification)
		{
			return new LocalizedString("InvalidMessageClassification", RulesTasksStrings.ResourceManager, new object[]
			{
				classification
			});
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00011DC8 File Offset: 0x0000FFC8
		public static LocalizedString RuleDescriptionRecipientAttributeMatches(string patterns)
		{
			return new LocalizedString("RuleDescriptionRecipientAttributeMatches", RulesTasksStrings.ResourceManager, new object[]
			{
				patterns
			});
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00011DF0 File Offset: 0x0000FFF0
		public static LocalizedString ADAttributeManager
		{
			get
			{
				return new LocalizedString("ADAttributeManager", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00011E07 File Offset: 0x00010007
		public static LocalizedString ADAttributeOtherPhoneNumber
		{
			get
			{
				return new LocalizedString("ADAttributeOtherPhoneNumber", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00011E20 File Offset: 0x00010020
		public static LocalizedString InboxRuleDescriptionWithSizeLessThan(string size)
		{
			return new LocalizedString("InboxRuleDescriptionWithSizeLessThan", RulesTasksStrings.ResourceManager, new object[]
			{
				size
			});
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00011E48 File Offset: 0x00010048
		public static LocalizedString LinkedPredicateFromAddressMatchesException
		{
			get
			{
				return new LocalizedString("LinkedPredicateFromAddressMatchesException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00011E60 File Offset: 0x00010060
		public static LocalizedString RuleNotFound(string rule)
		{
			return new LocalizedString("RuleNotFound", RulesTasksStrings.ResourceManager, new object[]
			{
				rule
			});
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00011E88 File Offset: 0x00010088
		public static LocalizedString EvaluationDescription
		{
			get
			{
				return new LocalizedString("EvaluationDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00011E9F File Offset: 0x0001009F
		public static LocalizedString PrefixDisplayName
		{
			get
			{
				return new LocalizedString("PrefixDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00011EB6 File Offset: 0x000100B6
		public static LocalizedString LinkedActionRemoveHeader
		{
			get
			{
				return new LocalizedString("LinkedActionRemoveHeader", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00011ECD File Offset: 0x000100CD
		public static LocalizedString LinkedPredicateSenderInRecipientList
		{
			get
			{
				return new LocalizedString("LinkedPredicateSenderInRecipientList", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00011EE4 File Offset: 0x000100E4
		public static LocalizedString ClientAccessRuleActionNotSupported(string action)
		{
			return new LocalizedString("ClientAccessRuleActionNotSupported", RulesTasksStrings.ResourceManager, new object[]
			{
				action
			});
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00011F0C File Offset: 0x0001010C
		public static LocalizedString JournalingParameterErrorGccWithOrganization
		{
			get
			{
				return new LocalizedString("JournalingParameterErrorGccWithOrganization", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00011F23 File Offset: 0x00010123
		public static LocalizedString ExternalUser
		{
			get
			{
				return new LocalizedString("ExternalUser", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00011F3C File Offset: 0x0001013C
		public static LocalizedString OutboundConnectorIdNotFound(string connectorIdentity)
		{
			return new LocalizedString("OutboundConnectorIdNotFound", RulesTasksStrings.ResourceManager, new object[]
			{
				connectorIdentity
			});
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00011F64 File Offset: 0x00010164
		public static LocalizedString RuleDescriptionStopRuleProcessing
		{
			get
			{
				return new LocalizedString("RuleDescriptionStopRuleProcessing", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00011F7B File Offset: 0x0001017B
		public static LocalizedString ErrorInboxRuleHasNoAction
		{
			get
			{
				return new LocalizedString("ErrorInboxRuleHasNoAction", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00011F92 File Offset: 0x00010192
		public static LocalizedString RuleDescriptionModerateMessageByManager
		{
			get
			{
				return new LocalizedString("RuleDescriptionModerateMessageByManager", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00011FA9 File Offset: 0x000101A9
		public static LocalizedString RuleDescriptionAttachmentProcessingLimitExceeded
		{
			get
			{
				return new LocalizedString("RuleDescriptionAttachmentProcessingLimitExceeded", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00011FC0 File Offset: 0x000101C0
		public static LocalizedString DisclaimerTextDisplayName
		{
			get
			{
				return new LocalizedString("DisclaimerTextDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00011FD7 File Offset: 0x000101D7
		public static LocalizedString HubParameterNotValidOnEdgeRole
		{
			get
			{
				return new LocalizedString("HubParameterNotValidOnEdgeRole", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00011FEE File Offset: 0x000101EE
		public static LocalizedString LinkedActionModerateMessageByManager
		{
			get
			{
				return new LocalizedString("LinkedActionModerateMessageByManager", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00012008 File Offset: 0x00010208
		public static LocalizedString InboxRuleDescriptionMessageType(string type)
		{
			return new LocalizedString("InboxRuleDescriptionMessageType", RulesTasksStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00012030 File Offset: 0x00010230
		public static LocalizedString RuleDescriptionRemoveHeader(string headerName)
		{
			return new LocalizedString("RuleDescriptionRemoveHeader", RulesTasksStrings.ResourceManager, new object[]
			{
				headerName
			});
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00012058 File Offset: 0x00010258
		public static LocalizedString RuleDescriptionSubjectOrBodyContains(string words)
		{
			return new LocalizedString("RuleDescriptionSubjectOrBodyContains", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00012080 File Offset: 0x00010280
		public static LocalizedString ServerVersionNull(string server, string rulename)
		{
			return new LocalizedString("ServerVersionNull", RulesTasksStrings.ResourceManager, new object[]
			{
				server,
				rulename
			});
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x000120AC File Offset: 0x000102AC
		public static LocalizedString ADAttributeDescription
		{
			get
			{
				return new LocalizedString("ADAttributeDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000120C4 File Offset: 0x000102C4
		public static LocalizedString OutlookProtectionRuleClassificationNotUnique(string name)
		{
			return new LocalizedString("OutlookProtectionRuleClassificationNotUnique", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x000120EC File Offset: 0x000102EC
		public static LocalizedString InboxRuleDescriptionMyNameNotInToBox
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionMyNameNotInToBox", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00012103 File Offset: 0x00010303
		public static LocalizedString ADAttributeCustomAttribute2
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute2", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0001211A File Offset: 0x0001031A
		public static LocalizedString JournalingParameterErrorGccWithoutRecipient
		{
			get
			{
				return new LocalizedString("JournalingParameterErrorGccWithoutRecipient", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00012134 File Offset: 0x00010334
		public static LocalizedString CommentsTooLong(int limit, int actual)
		{
			return new LocalizedString("CommentsTooLong", RulesTasksStrings.ResourceManager, new object[]
			{
				limit,
				actual
			});
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001216A File Offset: 0x0001036A
		public static LocalizedString ADAttributeEvaluationTypeEquals
		{
			get
			{
				return new LocalizedString("ADAttributeEvaluationTypeEquals", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00012184 File Offset: 0x00010384
		public static LocalizedString RuleDescriptionRedirectMessage(string addresses)
		{
			return new LocalizedString("RuleDescriptionRedirectMessage", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses
			});
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000121AC File Offset: 0x000103AC
		public static LocalizedString OutlookProtectionRuleClassificationNotFound(string name)
		{
			return new LocalizedString("OutlookProtectionRuleClassificationNotFound", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000121D4 File Offset: 0x000103D4
		public static LocalizedString RuleDescriptionLogEvent(string message)
		{
			return new LocalizedString("RuleDescriptionLogEvent", RulesTasksStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x000121FC File Offset: 0x000103FC
		public static LocalizedString LinkedPredicateFromAddressMatches
		{
			get
			{
				return new LocalizedString("LinkedPredicateFromAddressMatches", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00012213 File Offset: 0x00010413
		public static LocalizedString MessageDataClassificationDisplayName
		{
			get
			{
				return new LocalizedString("MessageDataClassificationDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0001222A File Offset: 0x0001042A
		public static LocalizedString FromAddressesDescription
		{
			get
			{
				return new LocalizedString("FromAddressesDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00012241 File Offset: 0x00010441
		public static LocalizedString LinkedPredicateManagerIs
		{
			get
			{
				return new LocalizedString("LinkedPredicateManagerIs", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00012258 File Offset: 0x00010458
		public static LocalizedString RuleDescriptionSetHeader(string headerName, string headerValue)
		{
			return new LocalizedString("RuleDescriptionSetHeader", RulesTasksStrings.ResourceManager, new object[]
			{
				headerName,
				headerValue
			});
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00012284 File Offset: 0x00010484
		public static LocalizedString LinkedActionAddManagerAsRecipientType
		{
			get
			{
				return new LocalizedString("LinkedActionAddManagerAsRecipientType", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001229C File Offset: 0x0001049C
		public static LocalizedString InboxRuleDescriptionHeaderContainsWords(string words)
		{
			return new LocalizedString("InboxRuleDescriptionHeaderContainsWords", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x000122C4 File Offset: 0x000104C4
		public static LocalizedString ADAttributeUserLogonName
		{
			get
			{
				return new LocalizedString("ADAttributeUserLogonName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000122DC File Offset: 0x000104DC
		public static LocalizedString RuleDescriptionWithImportance(string importance)
		{
			return new LocalizedString("RuleDescriptionWithImportance", RulesTasksStrings.ResourceManager, new object[]
			{
				importance
			});
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00012304 File Offset: 0x00010504
		public static LocalizedString ADAttributeNotes
		{
			get
			{
				return new LocalizedString("ADAttributeNotes", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001231B File Offset: 0x0001051B
		public static LocalizedString InboxRuleDescriptionSubscriptionNotFound
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionSubscriptionNotFound", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00012332 File Offset: 0x00010532
		public static LocalizedString IncidentReportOriginalMailDescription
		{
			get
			{
				return new LocalizedString("IncidentReportOriginalMailDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00012349 File Offset: 0x00010549
		public static LocalizedString JournalingParameterErrorExpiryDateWithoutGcc
		{
			get
			{
				return new LocalizedString("JournalingParameterErrorExpiryDateWithoutGcc", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00012360 File Offset: 0x00010560
		public static LocalizedString InboxRuleDescriptionFromAddressContainsWords(string words)
		{
			return new LocalizedString("InboxRuleDescriptionFromAddressContainsWords", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00012388 File Offset: 0x00010588
		public static LocalizedString WordsDescription
		{
			get
			{
				return new LocalizedString("WordsDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001239F File Offset: 0x0001059F
		public static LocalizedString PromptToOverwriteRulesOnImport
		{
			get
			{
				return new LocalizedString("PromptToOverwriteRulesOnImport", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x000123B6 File Offset: 0x000105B6
		public static LocalizedString SenderIpRangesDescription
		{
			get
			{
				return new LocalizedString("SenderIpRangesDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x000123CD File Offset: 0x000105CD
		public static LocalizedString LinkedPredicateAttachmentHasExecutableContentPredicateException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentHasExecutableContentPredicateException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000123E4 File Offset: 0x000105E4
		public static LocalizedString InvalidMessageDataClassificationParameterConfidenceExceedsMaxAllowed(string paramName, int maxAllowedValue)
		{
			return new LocalizedString("InvalidMessageDataClassificationParameterConfidenceExceedsMaxAllowed", RulesTasksStrings.ResourceManager, new object[]
			{
				paramName,
				maxAllowedValue
			});
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00012415 File Offset: 0x00010615
		public static LocalizedString DeleteMessageActionMustBeTheOnlyAction
		{
			get
			{
				return new LocalizedString("DeleteMessageActionMustBeTheOnlyAction", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001242C File Offset: 0x0001062C
		public static LocalizedString LinkedPredicateAnyOfToCcHeaderMemberOf
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfToCcHeaderMemberOf", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00012443 File Offset: 0x00010643
		public static LocalizedString InvalidClassification
		{
			get
			{
				return new LocalizedString("InvalidClassification", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001245A File Offset: 0x0001065A
		public static LocalizedString LinkedPredicateMessageContainsDataClassificationsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateMessageContainsDataClassificationsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00012474 File Offset: 0x00010674
		public static LocalizedString ClientAccessRuleWillBeAddedToCollection(string name)
		{
			return new LocalizedString("ClientAccessRuleWillBeAddedToCollection", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001249C File Offset: 0x0001069C
		public static LocalizedString InboxRuleDescriptionApplyCategory(string category)
		{
			return new LocalizedString("InboxRuleDescriptionApplyCategory", RulesTasksStrings.ResourceManager, new object[]
			{
				category
			});
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x000124C4 File Offset: 0x000106C4
		public static LocalizedString InboxRuleDescriptionMessageClassificationNotFound
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionMessageClassificationNotFound", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x000124DB File Offset: 0x000106DB
		public static LocalizedString LinkedActionNotifySenderAction
		{
			get
			{
				return new LocalizedString("LinkedActionNotifySenderAction", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x000124F2 File Offset: 0x000106F2
		public static LocalizedString LinkedPredicateAttachmentExtensionMatchesWords
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentExtensionMatchesWords", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00012509 File Offset: 0x00010709
		public static LocalizedString RuleDescriptionApplyOME
		{
			get
			{
				return new LocalizedString("RuleDescriptionApplyOME", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00012520 File Offset: 0x00010720
		public static LocalizedString ExternalPartner
		{
			get
			{
				return new LocalizedString("ExternalPartner", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00012537 File Offset: 0x00010737
		public static LocalizedString LinkedPredicateAttachmentSizeOverException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentSizeOverException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00012550 File Offset: 0x00010750
		public static LocalizedString RuleDescriptionAttachmentSizeOver(string size)
		{
			return new LocalizedString("RuleDescriptionAttachmentSizeOver", RulesTasksStrings.ResourceManager, new object[]
			{
				size
			});
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00012578 File Offset: 0x00010778
		public static LocalizedString LinkedPredicateADAttributeComparisonException
		{
			get
			{
				return new LocalizedString("LinkedPredicateADAttributeComparisonException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001258F File Offset: 0x0001078F
		public static LocalizedString MessageTypeEncrypted
		{
			get
			{
				return new LocalizedString("MessageTypeEncrypted", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x000125A6 File Offset: 0x000107A6
		public static LocalizedString SenderNotificationTypeDescription
		{
			get
			{
				return new LocalizedString("SenderNotificationTypeDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000125C0 File Offset: 0x000107C0
		public static LocalizedString ClientAccessRulesAuthenticationTypeRequired(string name)
		{
			return new LocalizedString("ClientAccessRulesAuthenticationTypeRequired", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x000125E8 File Offset: 0x000107E8
		public static LocalizedString RuleDescriptionSenderAttributeMatches(string patterns)
		{
			return new LocalizedString("RuleDescriptionSenderAttributeMatches", RulesTasksStrings.ResourceManager, new object[]
			{
				patterns
			});
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00012610 File Offset: 0x00010810
		public static LocalizedString InboxRuleDescriptionSubjectContainsWords(string words)
		{
			return new LocalizedString("InboxRuleDescriptionSubjectContainsWords", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00012638 File Offset: 0x00010838
		public static LocalizedString SenderNotificationTypeDisplayName
		{
			get
			{
				return new LocalizedString("SenderNotificationTypeDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001264F File Offset: 0x0001084F
		public static LocalizedString LinkedPredicateMessageTypeMatches
		{
			get
			{
				return new LocalizedString("LinkedPredicateMessageTypeMatches", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00012666 File Offset: 0x00010866
		public static LocalizedString ADAttributeHomePhoneNumber
		{
			get
			{
				return new LocalizedString("ADAttributeHomePhoneNumber", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001267D File Offset: 0x0001087D
		public static LocalizedString LinkedPredicateFromAddressContains
		{
			get
			{
				return new LocalizedString("LinkedPredicateFromAddressContains", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00012694 File Offset: 0x00010894
		public static LocalizedString InboxRuleDescriptionWithSizeGreaterThan(string size)
		{
			return new LocalizedString("InboxRuleDescriptionWithSizeGreaterThan", RulesTasksStrings.ResourceManager, new object[]
			{
				size
			});
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x000126BC File Offset: 0x000108BC
		public static LocalizedString LinkedPredicateSubjectOrBodyMatches
		{
			get
			{
				return new LocalizedString("LinkedPredicateSubjectOrBodyMatches", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x000126D3 File Offset: 0x000108D3
		public static LocalizedString ADAttributeCompany
		{
			get
			{
				return new LocalizedString("ADAttributeCompany", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x000126EA File Offset: 0x000108EA
		public static LocalizedString EvaluatedUserRecipient
		{
			get
			{
				return new LocalizedString("EvaluatedUserRecipient", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00012704 File Offset: 0x00010904
		public static LocalizedString ClientAccessRulesPortRangePropertyRequired(string name)
		{
			return new LocalizedString("ClientAccessRulesPortRangePropertyRequired", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001272C File Offset: 0x0001092C
		public static LocalizedString RuleDescriptionAnyOfToHeaderMemberOf(string addresses)
		{
			return new LocalizedString("RuleDescriptionAnyOfToHeaderMemberOf", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses
			});
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00012754 File Offset: 0x00010954
		public static LocalizedString RuleDescriptionRightsProtectMessage(string template)
		{
			return new LocalizedString("RuleDescriptionRightsProtectMessage", RulesTasksStrings.ResourceManager, new object[]
			{
				template
			});
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001277C File Offset: 0x0001097C
		public static LocalizedString RuleDescriptionAttachmentHasExecutableContent
		{
			get
			{
				return new LocalizedString("RuleDescriptionAttachmentHasExecutableContent", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00012793 File Offset: 0x00010993
		public static LocalizedString LinkedPredicateHasClassificationException
		{
			get
			{
				return new LocalizedString("LinkedPredicateHasClassificationException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000127AC File Offset: 0x000109AC
		public static LocalizedString RuleDescriptionAnyOfCcHeader(string addresses)
		{
			return new LocalizedString("RuleDescriptionAnyOfCcHeader", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses
			});
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000127D4 File Offset: 0x000109D4
		public static LocalizedString RuleDescriptionRecipientDomainIs(string domains)
		{
			return new LocalizedString("RuleDescriptionRecipientDomainIs", RulesTasksStrings.ResourceManager, new object[]
			{
				domains
			});
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x000127FC File Offset: 0x000109FC
		public static LocalizedString LinkedPredicateAnyOfRecipientAddressContains
		{
			get
			{
				return new LocalizedString("LinkedPredicateAnyOfRecipientAddressContains", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00012814 File Offset: 0x00010A14
		public static LocalizedString RuleDescriptionAnyOfRecipientAddressMatches(string addresses)
		{
			return new LocalizedString("RuleDescriptionAnyOfRecipientAddressMatches", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses
			});
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001283C File Offset: 0x00010A3C
		public static LocalizedString RuleDescriptionApplyHtmlDisclaimer(string text)
		{
			return new LocalizedString("RuleDescriptionApplyHtmlDisclaimer", RulesTasksStrings.ResourceManager, new object[]
			{
				text
			});
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00012864 File Offset: 0x00010A64
		public static LocalizedString ClientAccessRuleWillBeConsideredEnabled(string name)
		{
			return new LocalizedString("ClientAccessRuleWillBeConsideredEnabled", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001288C File Offset: 0x00010A8C
		public static LocalizedString LinkedPredicateSclOver
		{
			get
			{
				return new LocalizedString("LinkedPredicateSclOver", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x000128A3 File Offset: 0x00010AA3
		public static LocalizedString LinkedActionDeleteMessage
		{
			get
			{
				return new LocalizedString("LinkedActionDeleteMessage", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x000128BA File Offset: 0x00010ABA
		public static LocalizedString RuleDescriptionDisclaimerIgnoreFallback
		{
			get
			{
				return new LocalizedString("RuleDescriptionDisclaimerIgnoreFallback", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000128D4 File Offset: 0x00010AD4
		public static LocalizedString RuleDescriptionFromMemberOf(string addresses)
		{
			return new LocalizedString("RuleDescriptionFromMemberOf", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses
			});
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000128FC File Offset: 0x00010AFC
		public static LocalizedString CannotModifyRuleDueToVersion(string name)
		{
			return new LocalizedString("CannotModifyRuleDueToVersion", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00012924 File Offset: 0x00010B24
		public static LocalizedString RejectMessageActionMustBeTheOnlyAction
		{
			get
			{
				return new LocalizedString("RejectMessageActionMustBeTheOnlyAction", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0001293B File Offset: 0x00010B3B
		public static LocalizedString RuleDescriptionDisclaimerWrapFallback
		{
			get
			{
				return new LocalizedString("RuleDescriptionDisclaimerWrapFallback", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00012952 File Offset: 0x00010B52
		public static LocalizedString InboxRuleDescriptionMyNameInToOrCcBox
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionMyNameInToOrCcBox", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001296C File Offset: 0x00010B6C
		public static LocalizedString InboxRuleDescriptionHasClassification(string classifications)
		{
			return new LocalizedString("InboxRuleDescriptionHasClassification", RulesTasksStrings.ResourceManager, new object[]
			{
				classifications
			});
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00012994 File Offset: 0x00010B94
		public static LocalizedString CorruptRule(string name, string reason)
		{
			return new LocalizedString("CorruptRule", RulesTasksStrings.ResourceManager, new object[]
			{
				name,
				reason
			});
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x000129C0 File Offset: 0x00010BC0
		public static LocalizedString LinkedPredicateMessageSizeOverException
		{
			get
			{
				return new LocalizedString("LinkedPredicateMessageSizeOverException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x000129D7 File Offset: 0x00010BD7
		public static LocalizedString CcRecipientType
		{
			get
			{
				return new LocalizedString("CcRecipientType", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000129F0 File Offset: 0x00010BF0
		public static LocalizedString TestClientAccessRuleFoundUsername(string username)
		{
			return new LocalizedString("TestClientAccessRuleFoundUsername", RulesTasksStrings.ResourceManager, new object[]
			{
				username
			});
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00012A18 File Offset: 0x00010C18
		public static LocalizedString ClientAccessRulesNameAlreadyInUse
		{
			get
			{
				return new LocalizedString("ClientAccessRulesNameAlreadyInUse", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00012A30 File Offset: 0x00010C30
		public static LocalizedString RuleDescriptionADAttributeComparison(string attribute, string evaluation)
		{
			return new LocalizedString("RuleDescriptionADAttributeComparison", RulesTasksStrings.ResourceManager, new object[]
			{
				attribute,
				evaluation
			});
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00012A5C File Offset: 0x00010C5C
		public static LocalizedString LinkedPredicateContentCharacterSetContainsWordsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateContentCharacterSetContainsWordsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00012A73 File Offset: 0x00010C73
		public static LocalizedString LinkedPredicateSubjectContainsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateSubjectContainsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00012A8C File Offset: 0x00010C8C
		public static LocalizedString InvalidRecipientForModerationAction(string recipient)
		{
			return new LocalizedString("InvalidRecipientForModerationAction", RulesTasksStrings.ResourceManager, new object[]
			{
				recipient
			});
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00012AB4 File Offset: 0x00010CB4
		public static LocalizedString ADAttributeCustomAttribute13
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute13", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00012ACB File Offset: 0x00010CCB
		public static LocalizedString ToAddressesDisplayName
		{
			get
			{
				return new LocalizedString("ToAddressesDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00012AE2 File Offset: 0x00010CE2
		public static LocalizedString LinkedPredicateHasNoClassification
		{
			get
			{
				return new LocalizedString("LinkedPredicateHasNoClassification", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00012AFC File Offset: 0x00010CFC
		public static LocalizedString RuleDescriptionMessageTypeMatches(string messageType)
		{
			return new LocalizedString("RuleDescriptionMessageTypeMatches", RulesTasksStrings.ResourceManager, new object[]
			{
				messageType
			});
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00012B24 File Offset: 0x00010D24
		public static LocalizedString LinkedActionQuarantine
		{
			get
			{
				return new LocalizedString("LinkedActionQuarantine", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00012B3B File Offset: 0x00010D3B
		public static LocalizedString MessageTypePermissionControlled
		{
			get
			{
				return new LocalizedString("MessageTypePermissionControlled", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00012B54 File Offset: 0x00010D54
		public static LocalizedString RuleDescriptionIpRanges(string lists)
		{
			return new LocalizedString("RuleDescriptionIpRanges", RulesTasksStrings.ResourceManager, new object[]
			{
				lists
			});
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00012B7C File Offset: 0x00010D7C
		public static LocalizedString ADAttributeCustomAttribute9
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute9", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00012B93 File Offset: 0x00010D93
		public static LocalizedString SubTypeDescription
		{
			get
			{
				return new LocalizedString("SubTypeDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00012BAA File Offset: 0x00010DAA
		public static LocalizedString LinkedPredicateSenderAttributeContainsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateSenderAttributeContainsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00012BC4 File Offset: 0x00010DC4
		public static LocalizedString ConfirmationMessageSetClientAccessRule(string name)
		{
			return new LocalizedString("ConfirmationMessageSetClientAccessRule", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00012BEC File Offset: 0x00010DEC
		public static LocalizedString ReportDestinationDisplayName
		{
			get
			{
				return new LocalizedString("ReportDestinationDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00012C04 File Offset: 0x00010E04
		public static LocalizedString IncompleteParameterGroup(string parameter, string otherParameters)
		{
			return new LocalizedString("IncompleteParameterGroup", RulesTasksStrings.ResourceManager, new object[]
			{
				parameter,
				otherParameters
			});
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00012C30 File Offset: 0x00010E30
		public static LocalizedString InboxRuleDescriptionDeleteMessage
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionDeleteMessage", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00012C48 File Offset: 0x00010E48
		public static LocalizedString MoreThanOneRecipientForRecipientId(string recipId)
		{
			return new LocalizedString("MoreThanOneRecipientForRecipientId", RulesTasksStrings.ResourceManager, new object[]
			{
				recipId
			});
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00012C70 File Offset: 0x00010E70
		public static LocalizedString ToScopeDisplayName
		{
			get
			{
				return new LocalizedString("ToScopeDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00012C88 File Offset: 0x00010E88
		public static LocalizedString RuleDescriptionNotifySenderRejectUnlessFalsePositiveOverride(string rejectText, string rejectCode)
		{
			return new LocalizedString("RuleDescriptionNotifySenderRejectUnlessFalsePositiveOverride", RulesTasksStrings.ResourceManager, new object[]
			{
				rejectText,
				rejectCode
			});
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00012CB4 File Offset: 0x00010EB4
		public static LocalizedString ConditionIncompatibleWithTheSubtype(string conditionName, string subtypeName)
		{
			return new LocalizedString("ConditionIncompatibleWithTheSubtype", RulesTasksStrings.ResourceManager, new object[]
			{
				conditionName,
				subtypeName
			});
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00012CE0 File Offset: 0x00010EE0
		public static LocalizedString NotifySenderActionIsBeingOverridded
		{
			get
			{
				return new LocalizedString("NotifySenderActionIsBeingOverridded", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00012CF8 File Offset: 0x00010EF8
		public static LocalizedString ClientAccessRulesLimitError(int limit)
		{
			return new LocalizedString("ClientAccessRulesLimitError", RulesTasksStrings.ResourceManager, new object[]
			{
				limit
			});
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00012D25 File Offset: 0x00010F25
		public static LocalizedString JournalingParameterErrorGccWithScope
		{
			get
			{
				return new LocalizedString("JournalingParameterErrorGccWithScope", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00012D3C File Offset: 0x00010F3C
		public static LocalizedString RuleStateEnabled
		{
			get
			{
				return new LocalizedString("RuleStateEnabled", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00012D54 File Offset: 0x00010F54
		public static LocalizedString RuleDescriptionSubjectContains(string words)
		{
			return new LocalizedString("RuleDescriptionSubjectContains", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00012D7C File Offset: 0x00010F7C
		public static LocalizedString LinkedActionRedirectMessage
		{
			get
			{
				return new LocalizedString("LinkedActionRedirectMessage", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00012D93 File Offset: 0x00010F93
		public static LocalizedString ADAttributeCustomAttribute12
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute12", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00012DAA File Offset: 0x00010FAA
		public static LocalizedString RuleNameAlreadyExist
		{
			get
			{
				return new LocalizedString("RuleNameAlreadyExist", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00012DC1 File Offset: 0x00010FC1
		public static LocalizedString LinkedPredicateAttachmentIsPasswordProtectedException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentIsPasswordProtectedException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00012DD8 File Offset: 0x00010FD8
		public static LocalizedString ADAttributeMobileNumber
		{
			get
			{
				return new LocalizedString("ADAttributeMobileNumber", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00012DEF File Offset: 0x00010FEF
		public static LocalizedString LinkedPredicateADAttributeComparison
		{
			get
			{
				return new LocalizedString("LinkedPredicateADAttributeComparison", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00012E06 File Offset: 0x00011006
		public static LocalizedString EnhancedStatusCodeDisplayName
		{
			get
			{
				return new LocalizedString("EnhancedStatusCodeDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00012E20 File Offset: 0x00011020
		public static LocalizedString InboxRuleDescriptionMarkImportance(string importance)
		{
			return new LocalizedString("InboxRuleDescriptionMarkImportance", RulesTasksStrings.ResourceManager, new object[]
			{
				importance
			});
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00012E48 File Offset: 0x00011048
		public static LocalizedString InboxRuleDescriptionTakeActions
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionTakeActions", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00012E5F File Offset: 0x0001105F
		public static LocalizedString RejectMessageActionIsBeingOverridded
		{
			get
			{
				return new LocalizedString("RejectMessageActionIsBeingOverridded", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00012E76 File Offset: 0x00011076
		public static LocalizedString ManagementRelationshipDisplayName
		{
			get
			{
				return new LocalizedString("ManagementRelationshipDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00012E90 File Offset: 0x00011090
		public static LocalizedString ErrorAddedRecipientCannotBeDistributionGroup(string recipient)
		{
			return new LocalizedString("ErrorAddedRecipientCannotBeDistributionGroup", RulesTasksStrings.ResourceManager, new object[]
			{
				recipient
			});
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00012EB8 File Offset: 0x000110B8
		public static LocalizedString ClientAccessRulesProtocolPropertyRequired(string name)
		{
			return new LocalizedString("ClientAccessRulesProtocolPropertyRequired", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00012EE0 File Offset: 0x000110E0
		public static LocalizedString RuleDescriptionSenderInRecipientList(string lists)
		{
			return new LocalizedString("RuleDescriptionSenderInRecipientList", RulesTasksStrings.ResourceManager, new object[]
			{
				lists
			});
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00012F08 File Offset: 0x00011108
		public static LocalizedString ExportSkippedE15Rules(int skippedRuleCount)
		{
			return new LocalizedString("ExportSkippedE15Rules", RulesTasksStrings.ResourceManager, new object[]
			{
				skippedRuleCount
			});
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00012F35 File Offset: 0x00011135
		public static LocalizedString AttachmentSizeDisplayName
		{
			get
			{
				return new LocalizedString("AttachmentSizeDisplayName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00012F4C File Offset: 0x0001114C
		public static LocalizedString ClientAccessRuleRemoveDatacenterAdminsOnlyError
		{
			get
			{
				return new LocalizedString("ClientAccessRuleRemoveDatacenterAdminsOnlyError", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00012F63 File Offset: 0x00011163
		public static LocalizedString InvalidMessageDataClassificationEmptyName
		{
			get
			{
				return new LocalizedString("InvalidMessageDataClassificationEmptyName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00012F7A File Offset: 0x0001117A
		public static LocalizedString RejectMessageActionType
		{
			get
			{
				return new LocalizedString("RejectMessageActionType", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00012F91 File Offset: 0x00011191
		public static LocalizedString InboxRuleDescriptionMyNameInCcBox
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionMyNameInCcBox", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00012FA8 File Offset: 0x000111A8
		public static LocalizedString RuleDescriptionRecipientAddressMatches(string addresses)
		{
			return new LocalizedString("RuleDescriptionRecipientAddressMatches", RulesTasksStrings.ResourceManager, new object[]
			{
				addresses
			});
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00012FD0 File Offset: 0x000111D0
		public static LocalizedString InvalidMessageDataClassification(string dataClassification)
		{
			return new LocalizedString("InvalidMessageDataClassification", RulesTasksStrings.ResourceManager, new object[]
			{
				dataClassification
			});
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00012FF8 File Offset: 0x000111F8
		public static LocalizedString LinkedPredicateRecipientDomainIs
		{
			get
			{
				return new LocalizedString("LinkedPredicateRecipientDomainIs", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00013010 File Offset: 0x00011210
		public static LocalizedString ErrorTooManyTransportRules(int max)
		{
			return new LocalizedString("ErrorTooManyTransportRules", RulesTasksStrings.ResourceManager, new object[]
			{
				max
			});
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0001303D File Offset: 0x0001123D
		public static LocalizedString MissingDataClassificationsName
		{
			get
			{
				return new LocalizedString("MissingDataClassificationsName", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00013054 File Offset: 0x00011254
		public static LocalizedString LinkedActionGenerateIncidentReportAction
		{
			get
			{
				return new LocalizedString("LinkedActionGenerateIncidentReportAction", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0001306B File Offset: 0x0001126B
		public static LocalizedString InvalidAction
		{
			get
			{
				return new LocalizedString("InvalidAction", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00013084 File Offset: 0x00011284
		public static LocalizedString RuleDescriptionFromAddressContains(string words)
		{
			return new LocalizedString("RuleDescriptionFromAddressContains", RulesTasksStrings.ResourceManager, new object[]
			{
				words
			});
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x000130AC File Offset: 0x000112AC
		public static LocalizedString MessageHeaderDescription
		{
			get
			{
				return new LocalizedString("MessageHeaderDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000130C4 File Offset: 0x000112C4
		public static LocalizedString ConfirmationMessageNewClientAccessRule(string name)
		{
			return new LocalizedString("ConfirmationMessageNewClientAccessRule", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x000130EC File Offset: 0x000112EC
		public static LocalizedString DisclaimerTextDescription
		{
			get
			{
				return new LocalizedString("DisclaimerTextDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00013103 File Offset: 0x00011303
		public static LocalizedString MessageSizeDescription
		{
			get
			{
				return new LocalizedString("MessageSizeDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001311A File Offset: 0x0001131A
		public static LocalizedString SclValueDescription
		{
			get
			{
				return new LocalizedString("SclValueDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00013134 File Offset: 0x00011334
		public static LocalizedString RuleDescriptionSclOver(string sclValue)
		{
			return new LocalizedString("RuleDescriptionSclOver", RulesTasksStrings.ResourceManager, new object[]
			{
				sclValue
			});
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0001315C File Offset: 0x0001135C
		public static LocalizedString RejectUnlessSilentOverrideActionType
		{
			get
			{
				return new LocalizedString("RejectUnlessSilentOverrideActionType", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00013173 File Offset: 0x00011373
		public static LocalizedString LinkedPredicateManagerIsException
		{
			get
			{
				return new LocalizedString("LinkedPredicateManagerIsException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001318A File Offset: 0x0001138A
		public static LocalizedString MessageDataClassificationDescription
		{
			get
			{
				return new LocalizedString("MessageDataClassificationDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000131A4 File Offset: 0x000113A4
		public static LocalizedString InvalidRegex(string regex)
		{
			return new LocalizedString("InvalidRegex", RulesTasksStrings.ResourceManager, new object[]
			{
				regex
			});
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x000131CC File Offset: 0x000113CC
		public static LocalizedString ImportanceLow
		{
			get
			{
				return new LocalizedString("ImportanceLow", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x000131E3 File Offset: 0x000113E3
		public static LocalizedString LinkedPredicateFromMemberOf
		{
			get
			{
				return new LocalizedString("LinkedPredicateFromMemberOf", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000131FC File Offset: 0x000113FC
		public static LocalizedString RuleDescriptionAttachmentNameMatches(string patterns)
		{
			return new LocalizedString("RuleDescriptionAttachmentNameMatches", RulesTasksStrings.ResourceManager, new object[]
			{
				patterns
			});
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00013224 File Offset: 0x00011424
		public static LocalizedString InboxRuleDescriptionOr
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionOr", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001323B File Offset: 0x0001143B
		public static LocalizedString LinkedPredicateAttachmentIsUnsupportedException
		{
			get
			{
				return new LocalizedString("LinkedPredicateAttachmentIsUnsupportedException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00013254 File Offset: 0x00011454
		public static LocalizedString InvalidDlpPolicy(string dlpPolicy)
		{
			return new LocalizedString("InvalidDlpPolicy", RulesTasksStrings.ResourceManager, new object[]
			{
				dlpPolicy
			});
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0001327C File Offset: 0x0001147C
		public static LocalizedString FromScopeDescription
		{
			get
			{
				return new LocalizedString("FromScopeDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00013293 File Offset: 0x00011493
		public static LocalizedString ExternalNonPartner
		{
			get
			{
				return new LocalizedString("ExternalNonPartner", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000132AA File Offset: 0x000114AA
		public static LocalizedString ADAttributeCustomAttribute11
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute11", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x000132C1 File Offset: 0x000114C1
		public static LocalizedString FallbackActionDescription
		{
			get
			{
				return new LocalizedString("FallbackActionDescription", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x000132D8 File Offset: 0x000114D8
		public static LocalizedString InboxRuleDescriptionMarkAsRead
		{
			get
			{
				return new LocalizedString("InboxRuleDescriptionMarkAsRead", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x000132F0 File Offset: 0x000114F0
		public static LocalizedString ConfirmationMessageRemoveClientAccessRule(string name)
		{
			return new LocalizedString("ConfirmationMessageRemoveClientAccessRule", RulesTasksStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00013318 File Offset: 0x00011518
		public static LocalizedString LinkedPredicateSentToMemberOfException
		{
			get
			{
				return new LocalizedString("LinkedPredicateSentToMemberOfException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001332F File Offset: 0x0001152F
		public static LocalizedString LinkedPredicateWithImportanceException
		{
			get
			{
				return new LocalizedString("LinkedPredicateWithImportanceException", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00013346 File Offset: 0x00011546
		public static LocalizedString ImportFileDataIsNull
		{
			get
			{
				return new LocalizedString("ImportFileDataIsNull", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001335D File Offset: 0x0001155D
		public static LocalizedString MessageTypeCalendaring
		{
			get
			{
				return new LocalizedString("MessageTypeCalendaring", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00013374 File Offset: 0x00011574
		public static LocalizedString NotifyOnlyActionType
		{
			get
			{
				return new LocalizedString("NotifyOnlyActionType", RulesTasksStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001338B File Offset: 0x0001158B
		public static LocalizedString GetLocalizedString(RulesTasksStrings.IDs key)
		{
			return new LocalizedString(RulesTasksStrings.stringIDs[(uint)key], RulesTasksStrings.ResourceManager, new object[0]);
		}

		// Token: 0x0400036A RID: 874
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(395);

		// Token: 0x0400036B RID: 875
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Core.RulesTasksStrings", typeof(RulesTasksStrings).GetTypeInfo().Assembly);

		// Token: 0x02000028 RID: 40
		public enum IDs : uint
		{
			// Token: 0x0400036D RID: 877
			LinkedPredicateHeaderMatches = 3817895306U,
			// Token: 0x0400036E RID: 878
			SetAuditSeverityDisplayName = 3690383901U,
			// Token: 0x0400036F RID: 879
			ADAttributeOtherHomePhoneNumber = 856583401U,
			// Token: 0x04000370 RID: 880
			JournalingParameterErrorFullReportWithoutGcc = 607657692U,
			// Token: 0x04000371 RID: 881
			RecipientTypeDescription = 3705591125U,
			// Token: 0x04000372 RID: 882
			EvaluatedUserDisplayName = 1840928351U,
			// Token: 0x04000373 RID: 883
			LinkedPredicateHeaderContains = 1484331716U,
			// Token: 0x04000374 RID: 884
			MessageTypeOof = 2129610537U,
			// Token: 0x04000375 RID: 885
			ImportanceDisplayName = 862345881U,
			// Token: 0x04000376 RID: 886
			DlpPolicyModeIsOverridenByModeParameter = 887494032U,
			// Token: 0x04000377 RID: 887
			LinkedActionApplyHtmlDisclaimer = 3832906569U,
			// Token: 0x04000378 RID: 888
			RejectMessageParameterWillBeIgnored = 2799440992U,
			// Token: 0x04000379 RID: 889
			LinkedPredicateRecipientAddressContainsWords = 4206835639U,
			// Token: 0x0400037A RID: 890
			AttachmentSizeDescription = 3386546304U,
			// Token: 0x0400037B RID: 891
			LinkedPredicateRecipientAddressMatchesPatternsException = 1966725662U,
			// Token: 0x0400037C RID: 892
			AttributeValueDescription = 2655586213U,
			// Token: 0x0400037D RID: 893
			LinkedPredicateAttachmentMatchesPatternsException = 3729435602U,
			// Token: 0x0400037E RID: 894
			LinkedActionLogEvent = 694309943U,
			// Token: 0x0400037F RID: 895
			LinkedPredicateAnyOfToHeader = 3534737571U,
			// Token: 0x04000380 RID: 896
			EvaluatedUserDescription = 3304155086U,
			// Token: 0x04000381 RID: 897
			ADAttributeCustomAttribute1 = 1377545167U,
			// Token: 0x04000382 RID: 898
			LinkedPredicateAttachmentPropertyContainsWordsException = 996206651U,
			// Token: 0x04000383 RID: 899
			LinkedActionApplyClassification = 2978999437U,
			// Token: 0x04000384 RID: 900
			LinkedActionSetScl = 1938376749U,
			// Token: 0x04000385 RID: 901
			LinkedPredicateAnyOfCcHeaderException = 2156000375U,
			// Token: 0x04000386 RID: 902
			ArgumentNotSet = 207684870U,
			// Token: 0x04000387 RID: 903
			InvalidRuleName = 2267758972U,
			// Token: 0x04000388 RID: 904
			RuleDescriptionDisclaimerRejectFallback = 5978988U,
			// Token: 0x04000389 RID: 905
			LinkedPredicateFromAddressContainsException = 2498488782U,
			// Token: 0x0400038A RID: 906
			LinkedPredicateAttachmentNameMatchesException = 3718292884U,
			// Token: 0x0400038B RID: 907
			ADAttributeLastName = 1016721882U,
			// Token: 0x0400038C RID: 908
			GenerateNotificationDisplayName = 151284935U,
			// Token: 0x0400038D RID: 909
			RuleDescriptionNotifySenderNotifyOnly = 281219183U,
			// Token: 0x0400038E RID: 910
			ADAttributeCountry = 3600528589U,
			// Token: 0x0400038F RID: 911
			MessageHeaderDisplayName = 3542666061U,
			// Token: 0x04000390 RID: 912
			LinkedPredicateAttachmentMatchesPatterns = 4246027971U,
			// Token: 0x04000391 RID: 913
			ListsDisplayName = 3208048360U,
			// Token: 0x04000392 RID: 914
			FallbackIgnore = 2328530086U,
			// Token: 0x04000393 RID: 915
			LinkedPredicateSenderIpRangesException = 77980785U,
			// Token: 0x04000394 RID: 916
			AttributeValueDisplayName = 763738384U,
			// Token: 0x04000395 RID: 917
			LinkedPredicateRecipientDomainIsException = 2380701036U,
			// Token: 0x04000396 RID: 918
			InboxRuleDescriptionHasAttachment = 1655133361U,
			// Token: 0x04000397 RID: 919
			RuleDescriptionHasSenderOverride = 2340468721U,
			// Token: 0x04000398 RID: 920
			LinkedPredicateRecipientInSenderListException = 2538565266U,
			// Token: 0x04000399 RID: 921
			LinkedPredicateAttachmentExtensionMatchesWordsException = 705865031U,
			// Token: 0x0400039A RID: 922
			ImportanceNormal = 3850060255U,
			// Token: 0x0400039B RID: 923
			LinkedActionCopyTo = 3099194133U,
			// Token: 0x0400039C RID: 924
			ListsDescription = 2306965519U,
			// Token: 0x0400039D RID: 925
			TextPatternsDescription = 1130765528U,
			// Token: 0x0400039E RID: 926
			NewRuleSyncAcrossDifferentVersionsNeeded = 1191033945U,
			// Token: 0x0400039F RID: 927
			RmsTemplateDescription = 3108375176U,
			// Token: 0x040003A0 RID: 928
			LinkedPredicateRecipientInSenderList = 4223753465U,
			// Token: 0x040003A1 RID: 929
			ADAttributeInitials = 2468414724U,
			// Token: 0x040003A2 RID: 930
			LinkedPredicateHeaderContainsException = 1331793023U,
			// Token: 0x040003A3 RID: 931
			LinkedPredicateContentCharacterSetContainsWords = 3721269126U,
			// Token: 0x040003A4 RID: 932
			MessageTypeDescription = 1697423233U,
			// Token: 0x040003A5 RID: 933
			ADAttributeState = 3882899654U,
			// Token: 0x040003A6 RID: 934
			FromScopeDisplayName = 3643356345U,
			// Token: 0x040003A7 RID: 935
			InboxRuleDescriptionAnd = 2812686069U,
			// Token: 0x040003A8 RID: 936
			ADAttributeCustomAttribute8 = 1377545174U,
			// Token: 0x040003A9 RID: 937
			LinkedPredicateSubjectMatches = 1720532765U,
			// Token: 0x040003AA RID: 938
			MessageTypeAutoForward = 2041595767U,
			// Token: 0x040003AB RID: 939
			MessageTypeReadReceipt = 680782013U,
			// Token: 0x040003AC RID: 940
			LinkedActionBlindCopyTo = 431368248U,
			// Token: 0x040003AD RID: 941
			LinkedPredicateSentToScopeException = 2112278218U,
			// Token: 0x040003AE RID: 942
			LinkedActionStopRuleProcessing = 3773694392U,
			// Token: 0x040003AF RID: 943
			LinkedPredicateAnyOfToCcHeader = 3956509743U,
			// Token: 0x040003B0 RID: 944
			LinkedPredicateHasSenderOverride = 3369692937U,
			// Token: 0x040003B1 RID: 945
			RedirectRecipientType = 2611996929U,
			// Token: 0x040003B2 RID: 946
			FallbackActionDisplayName = 1231044387U,
			// Token: 0x040003B3 RID: 947
			NoAction = 161711511U,
			// Token: 0x040003B4 RID: 948
			InboxRuleDescriptionFlaggedForAnyAction = 3921523715U,
			// Token: 0x040003B5 RID: 949
			LinkedActionSetHeader = 1645493202U,
			// Token: 0x040003B6 RID: 950
			SenderIpRangesDisplayName = 3850705779U,
			// Token: 0x040003B7 RID: 951
			LinkedPredicateAttachmentIsUnsupported = 2375893868U,
			// Token: 0x040003B8 RID: 952
			ADAttributeName = 3050431750U,
			// Token: 0x040003B9 RID: 953
			EventMessageDisplayName = 3095013874U,
			// Token: 0x040003BA RID: 954
			IncidentReportOriginalMailnDisplayName = 1080850367U,
			// Token: 0x040003BB RID: 955
			ADAttributeOtherFaxNumber = 3689464497U,
			// Token: 0x040003BC RID: 956
			RejectUnlessExplicitOverrideActionType = 3115100581U,
			// Token: 0x040003BD RID: 957
			RuleDescriptionHasNoClassification = 4231516709U,
			// Token: 0x040003BE RID: 958
			LinkedPredicateAnyOfRecipientAddressMatchesException = 130230884U,
			// Token: 0x040003BF RID: 959
			MessageTypeDisplayName = 3856180838U,
			// Token: 0x040003C0 RID: 960
			FallbackReject = 3144351139U,
			// Token: 0x040003C1 RID: 961
			ADAttributeCustomAttribute10 = 3374360575U,
			// Token: 0x040003C2 RID: 962
			ADAttributeEmail = 4289093673U,
			// Token: 0x040003C3 RID: 963
			ADAttributeCustomAttribute5 = 1377545163U,
			// Token: 0x040003C4 RID: 964
			LinkedPredicateSubjectOrBodyMatchesException = 1301491835U,
			// Token: 0x040003C5 RID: 965
			InboxRuleDescriptionMyNameInToBox = 2591548322U,
			// Token: 0x040003C6 RID: 966
			ToRecipientType = 4199979286U,
			// Token: 0x040003C7 RID: 967
			LinkedPredicateAttachmentContainsWords = 1321315595U,
			// Token: 0x040003C8 RID: 968
			LinkedActionRightsProtectMessage = 2934581752U,
			// Token: 0x040003C9 RID: 969
			LinkedActionSetAuditSeverity = 3527153583U,
			// Token: 0x040003CA RID: 970
			LinkedPredicateBetweenMemberOf = 1125919747U,
			// Token: 0x040003CB RID: 971
			RemoveRuleSyncAcrossDifferentVersionsNeeded = 1050054139U,
			// Token: 0x040003CC RID: 972
			ADAttributeDisplayName = 2569693958U,
			// Token: 0x040003CD RID: 973
			LinkedPredicateSentToScope = 354503089U,
			// Token: 0x040003CE RID: 974
			DisclaimerLocationDescription = 3455130960U,
			// Token: 0x040003CF RID: 975
			FromAddressesDisplayName = 1316941123U,
			// Token: 0x040003D0 RID: 976
			LinkedActionRejectMessage = 1648356515U,
			// Token: 0x040003D1 RID: 977
			LinkedPredicateSenderAttributeMatchesException = 328892189U,
			// Token: 0x040003D2 RID: 978
			LinkedActionRouteMessageOutboundRequireTls = 1857695339U,
			// Token: 0x040003D3 RID: 979
			LinkedPredicateAttachmentNameMatches = 3266871523U,
			// Token: 0x040003D4 RID: 980
			HeaderValueDescription = 3211626450U,
			// Token: 0x040003D5 RID: 981
			RecipientTypeDisplayName = 4103233806U,
			// Token: 0x040003D6 RID: 982
			LinkedPredicateFromMemberOfException = 2869706848U,
			// Token: 0x040003D7 RID: 983
			ADAttributeCustomAttribute3 = 1377545169U,
			// Token: 0x040003D8 RID: 984
			ADAttributePagerNumber = 3850073087U,
			// Token: 0x040003D9 RID: 985
			MessageTypeVoicemail = 117825870U,
			// Token: 0x040003DA RID: 986
			LinkedPredicateWithImportance = 903770574U,
			// Token: 0x040003DB RID: 987
			GenerateNotificationDescription = 2749090792U,
			// Token: 0x040003DC RID: 988
			ADAttributeTitle = 2634964433U,
			// Token: 0x040003DD RID: 989
			TextPatternsDisplayName = 517035719U,
			// Token: 0x040003DE RID: 990
			LinkedPredicateHeaderMatchesException = 1086713105U,
			// Token: 0x040003DF RID: 991
			EvaluationDisplayName = 240566931U,
			// Token: 0x040003E0 RID: 992
			ToDLAddressDescription = 37980935U,
			// Token: 0x040003E1 RID: 993
			DuplicateDataClassificationSpecified = 1419022259U,
			// Token: 0x040003E2 RID: 994
			RuleDescriptionRouteMessageOutboundRequireTls = 4270036386U,
			// Token: 0x040003E3 RID: 995
			EvaluationNotEqual = 3918497079U,
			// Token: 0x040003E4 RID: 996
			StatusCodeDisplayName = 1318242726U,
			// Token: 0x040003E5 RID: 997
			ConnectorNameDescription = 4178557944U,
			// Token: 0x040003E6 RID: 998
			RuleDescriptionAndDelimiter = 2828094232U,
			// Token: 0x040003E7 RID: 999
			ADAttributeCustomAttribute14 = 1048761747U,
			// Token: 0x040003E8 RID: 1000
			LinkedActionApplyOME = 1514251090U,
			// Token: 0x040003E9 RID: 1001
			ADAttributePhoneNumber = 4137481806U,
			// Token: 0x040003EA RID: 1002
			ADAttributeOffice = 1927573801U,
			// Token: 0x040003EB RID: 1003
			LinkedPredicateSenderInRecipientListException = 1784539898U,
			// Token: 0x040003EC RID: 1004
			RuleDescriptionDisconnect = 947201504U,
			// Token: 0x040003ED RID: 1005
			RuleDescriptionOrDelimiter = 250901884U,
			// Token: 0x040003EE RID: 1006
			LinkedActionSmtpRejectMessage = 162518937U,
			// Token: 0x040003EF RID: 1007
			LinkedPredicateAnyOfToHeaderMemberOf = 1218766792U,
			// Token: 0x040003F0 RID: 1008
			ErrorInboxRuleMustHaveActions = 3733737272U,
			// Token: 0x040003F1 RID: 1009
			QuarantineActionNotAvailable = 4170515100U,
			// Token: 0x040003F2 RID: 1010
			LinkedPredicateAttachmentPropertyContainsWords = 1484034422U,
			// Token: 0x040003F3 RID: 1011
			LinkedPredicateAnyOfToHeaderException = 2351889904U,
			// Token: 0x040003F4 RID: 1012
			EventMessageDescription = 3617076017U,
			// Token: 0x040003F5 RID: 1013
			LinkedPredicateAnyOfRecipientAddressContainsException = 2990680076U,
			// Token: 0x040003F6 RID: 1014
			RmsTemplateDisplayName = 1857974425U,
			// Token: 0x040003F7 RID: 1015
			LinkedPredicateRecipientAttributeContainsException = 999677583U,
			// Token: 0x040003F8 RID: 1016
			LinkedPredicateHasClassification = 2463949652U,
			// Token: 0x040003F9 RID: 1017
			InboxRuleDescriptionSentOnlyToMe = 1025350601U,
			// Token: 0x040003FA RID: 1018
			LinkedPredicateAttachmentIsPasswordProtected = 3905367638U,
			// Token: 0x040003FB RID: 1019
			PromptToUpgradeRulesFormat = 4055862009U,
			// Token: 0x040003FC RID: 1020
			NotifySenderActionRequiresMcdcPredicate = 2147095286U,
			// Token: 0x040003FD RID: 1021
			LinkedActionGenerateNotificationAction = 1853592207U,
			// Token: 0x040003FE RID: 1022
			ClassificationDisplayName = 3677381679U,
			// Token: 0x040003FF RID: 1023
			LinkedActionPrependSubject = 104177927U,
			// Token: 0x04000400 RID: 1024
			ADAttributeStreet = 2002903510U,
			// Token: 0x04000401 RID: 1025
			FromDLAddressDisplayName = 3208826121U,
			// Token: 0x04000402 RID: 1026
			LinkedPredicateMessageTypeMatchesException = 1121933765U,
			// Token: 0x04000403 RID: 1027
			ADAttributeCustomAttribute15 = 2614845688U,
			// Token: 0x04000404 RID: 1028
			RuleDescriptionQuarantine = 3575782782U,
			// Token: 0x04000405 RID: 1029
			MissingDataClassificationsParameter = 1485874038U,
			// Token: 0x04000406 RID: 1030
			LinkedPredicateHasNoClassificationException = 2891590424U,
			// Token: 0x04000407 RID: 1031
			LinkedPredicateFromScope = 3816854736U,
			// Token: 0x04000408 RID: 1032
			ManagementRelationshipManager = 25634710U,
			// Token: 0x04000409 RID: 1033
			LinkedPredicateSubjectMatchesException = 2463135270U,
			// Token: 0x0400040A RID: 1034
			LinkedPredicateSentTo = 3105847291U,
			// Token: 0x0400040B RID: 1035
			LinkedPredicateAnyOfCcHeaderMemberOf = 1943465529U,
			// Token: 0x0400040C RID: 1036
			InboxRuleDescriptionFolderNotFound = 2075495475U,
			// Token: 0x0400040D RID: 1037
			PrefixDescription = 3967326684U,
			// Token: 0x0400040E RID: 1038
			EvaluatedUserSender = 2509095413U,
			// Token: 0x0400040F RID: 1039
			RuleDescriptionDeleteMessage = 1886943840U,
			// Token: 0x04000410 RID: 1040
			LinkedPredicateHasSenderOverrideException = 2588281890U,
			// Token: 0x04000411 RID: 1041
			SubTypeDisplayName = 4011098093U,
			// Token: 0x04000412 RID: 1042
			ParameterVersionMismatch = 929006655U,
			// Token: 0x04000413 RID: 1043
			LinkedPredicateManagementRelationship = 2191228215U,
			// Token: 0x04000414 RID: 1044
			LinkedActionRouteMessageOutboundConnector = 2135540736U,
			// Token: 0x04000415 RID: 1045
			LinkedActionModerateMessageByUser = 3722738631U,
			// Token: 0x04000416 RID: 1046
			LinkedActionAddToRecipient = 3172147964U,
			// Token: 0x04000417 RID: 1047
			IncidentReportContentDisplayName = 1516175560U,
			// Token: 0x04000418 RID: 1048
			LinkedPredicateSubjectContains = 2942456627U,
			// Token: 0x04000419 RID: 1049
			LinkedPredicateRecipientAddressContainsWordsException = 790749074U,
			// Token: 0x0400041A RID: 1050
			FallbackWrap = 3262572344U,
			// Token: 0x0400041B RID: 1051
			ADAttributeCustomAttribute4 = 1377545162U,
			// Token: 0x0400041C RID: 1052
			ADAttributeEvaluationTypeContains = 699378618U,
			// Token: 0x0400041D RID: 1053
			ADAttributeEvaluationTypeDescription = 2876513949U,
			// Token: 0x0400041E RID: 1054
			InvalidPredicate = 3627783386U,
			// Token: 0x0400041F RID: 1055
			ADAttributeDepartment = 3367615085U,
			// Token: 0x04000420 RID: 1056
			RuleStateDisabled = 4233384325U,
			// Token: 0x04000421 RID: 1057
			LinkedPredicateSclOverException = 1474730269U,
			// Token: 0x04000422 RID: 1058
			ConnectorNameDisplayName = 4156341045U,
			// Token: 0x04000423 RID: 1059
			LinkedPredicateSentToException = 1640312644U,
			// Token: 0x04000424 RID: 1060
			EnhancedStatusCodeDescription = 975362769U,
			// Token: 0x04000425 RID: 1061
			LinkedActionDisconnect = 3680228519U,
			// Token: 0x04000426 RID: 1062
			ADAttributePOBox = 4260106383U,
			// Token: 0x04000427 RID: 1063
			RuleDescriptionRemoveOME = 2557875829U,
			// Token: 0x04000428 RID: 1064
			MessageTypeApprovalRequest = 4072748617U,
			// Token: 0x04000429 RID: 1065
			ADAttributeFaxNumber = 2182511137U,
			// Token: 0x0400042A RID: 1066
			ErrorAccessingTransportSettings = 1453329754U,
			// Token: 0x0400042B RID: 1067
			EvaluationEqual = 3459736224U,
			// Token: 0x0400042C RID: 1068
			LinkedPredicateRecipientAttributeContains = 1008100316U,
			// Token: 0x0400042D RID: 1069
			LinkedPredicateAttachmentSizeOver = 1994464450U,
			// Token: 0x0400042E RID: 1070
			LinkedPredicateFrom = 3312695260U,
			// Token: 0x0400042F RID: 1071
			LinkedPredicateAnyOfToCcHeaderException = 583131402U,
			// Token: 0x04000430 RID: 1072
			LinkedPredicateSubjectOrBodyContains = 2016017520U,
			// Token: 0x04000431 RID: 1073
			LinkedPredicateAttachmentContainsWordsException = 2966559302U,
			// Token: 0x04000432 RID: 1074
			InboxRuleDescriptionFlaggedForNoResponse = 2052587897U,
			// Token: 0x04000433 RID: 1075
			StatusCodeDescription = 3353805215U,
			// Token: 0x04000434 RID: 1076
			LinkedPredicateFromScopeException = 1663659467U,
			// Token: 0x04000435 RID: 1077
			LinkedPredicateAttachmentProcessingLimitExceeded = 1157968148U,
			// Token: 0x04000436 RID: 1078
			ManagementRelationshipDirectReport = 428619956U,
			// Token: 0x04000437 RID: 1079
			ADAttributeZipCode = 381216251U,
			// Token: 0x04000438 RID: 1080
			SetRuleSyncAcrossDifferentVersionsNeeded = 4146681835U,
			// Token: 0x04000439 RID: 1081
			IncidentReportContentDescription = 2075192559U,
			// Token: 0x0400043A RID: 1082
			InvalidIncidentReportOriginalMail = 1604244669U,
			// Token: 0x0400043B RID: 1083
			LinkedPredicateRecipientAttributeMatchesException = 2457055209U,
			// Token: 0x0400043C RID: 1084
			ClientAccessRulesAuthenticationTypeInvalid = 2083948085U,
			// Token: 0x0400043D RID: 1085
			ClassificationDescription = 1419241760U,
			// Token: 0x0400043E RID: 1086
			ReportDestinationDescription = 6731194U,
			// Token: 0x0400043F RID: 1087
			LinkedPredicateSentToMemberOf = 2492349756U,
			// Token: 0x04000440 RID: 1088
			ToScopeDescription = 1882052979U,
			// Token: 0x04000441 RID: 1089
			WordsDisplayName = 3163284624U,
			// Token: 0x04000442 RID: 1090
			ModerateActionMustBeTheOnlyAction = 3682029584U,
			// Token: 0x04000443 RID: 1091
			InvalidRejectEnhancedStatus = 2063369946U,
			// Token: 0x04000444 RID: 1092
			ADAttributeFirstName = 2986926906U,
			// Token: 0x04000445 RID: 1093
			LinkedPredicateAnyOfCcHeader = 3927787984U,
			// Token: 0x04000446 RID: 1094
			ErrorInvalidCharException = 3247824756U,
			// Token: 0x04000447 RID: 1095
			LinkedPredicateMessageSizeOver = 1871050104U,
			// Token: 0x04000448 RID: 1096
			LinkedPredicateAttachmentProcessingLimitExceededException = 312772195U,
			// Token: 0x04000449 RID: 1097
			ManagementRelationshipDescription = 278098341U,
			// Token: 0x0400044A RID: 1098
			LinkedPredicateSenderDomainIs = 3355891993U,
			// Token: 0x0400044B RID: 1099
			InternalUser = 2795331228U,
			// Token: 0x0400044C RID: 1100
			InvalidRmsTemplate = 2609648925U,
			// Token: 0x0400044D RID: 1101
			HeaderValueDisplayName = 3500719009U,
			// Token: 0x0400044E RID: 1102
			EdgeParameterNotValidOnHubRole = 2141139127U,
			// Token: 0x0400044F RID: 1103
			BccRecipientType = 1798370525U,
			// Token: 0x04000450 RID: 1104
			RejectUnlessFalsePositiveOverrideActionType = 2736707353U,
			// Token: 0x04000451 RID: 1105
			LinkedPredicateSenderAttributeContains = 4141158958U,
			// Token: 0x04000452 RID: 1106
			LinkedPredicateAttachmentHasExecutableContentPredicate = 2139768671U,
			// Token: 0x04000453 RID: 1107
			PromptToRemoveLogEventAction = 1691025395U,
			// Token: 0x04000454 RID: 1108
			ToAddressesDescription = 2412654301U,
			// Token: 0x04000455 RID: 1109
			MessageTypeSigned = 3606274629U,
			// Token: 0x04000456 RID: 1110
			RejectReasonDescription = 2370124961U,
			// Token: 0x04000457 RID: 1111
			InboxRuleDescriptionFlaggedForFYI = 2333734413U,
			// Token: 0x04000458 RID: 1112
			LinkedPredicateRecipientAttributeMatches = 2632339802U,
			// Token: 0x04000459 RID: 1113
			LinkedPredicateAnyOfRecipientAddressMatches = 1776244123U,
			// Token: 0x0400045A RID: 1114
			LinkedPredicateRecipientAddressMatchesPatterns = 3487178095U,
			// Token: 0x0400045B RID: 1115
			SclValueDisplayName = 1279708626U,
			// Token: 0x0400045C RID: 1116
			PrependDisclaimer = 3219452859U,
			// Token: 0x0400045D RID: 1117
			ClientAccessRuleSetDatacenterAdminsOnlyError = 1416106290U,
			// Token: 0x0400045E RID: 1118
			LinkedPredicateAnyOfToCcHeaderMemberOfException = 2826669353U,
			// Token: 0x0400045F RID: 1119
			ADAttributeCustomAttribute7 = 1377545165U,
			// Token: 0x04000460 RID: 1120
			FromDLAddressDescription = 1713926708U,
			// Token: 0x04000461 RID: 1121
			RejectReasonDisplayName = 756934846U,
			// Token: 0x04000462 RID: 1122
			LinkedPredicateSenderIpRanges = 2589988606U,
			// Token: 0x04000463 RID: 1123
			LinkedPredicateMessageContainsDataClassifications = 1489020517U,
			// Token: 0x04000464 RID: 1124
			LinkedPredicateAnyOfToHeaderMemberOfException = 976180545U,
			// Token: 0x04000465 RID: 1125
			SetAuditSeverityDescription = 900910488U,
			// Token: 0x04000466 RID: 1126
			RuleDescriptionAttachmentIsUnsupported = 3952930894U,
			// Token: 0x04000467 RID: 1127
			ADAttributeCustomAttribute6 = 1377545164U,
			// Token: 0x04000468 RID: 1128
			LinkedPredicateBetweenMemberOfException = 2479616296U,
			// Token: 0x04000469 RID: 1129
			ADAttributeCity = 4226527350U,
			// Token: 0x0400046A RID: 1130
			DlpRuleMustContainMessageContainsDataClassificationPredicate = 3204500204U,
			// Token: 0x0400046B RID: 1131
			ImportanceDescription = 3673771834U,
			// Token: 0x0400046C RID: 1132
			AppendDisclaimer = 436298925U,
			// Token: 0x0400046D RID: 1133
			NegativePriority = 2937791153U,
			// Token: 0x0400046E RID: 1134
			MessageSizeDisplayName = 503362863U,
			// Token: 0x0400046F RID: 1135
			LinkedPredicateSenderAttributeMatches = 523804848U,
			// Token: 0x04000470 RID: 1136
			LinkedPredicateFromException = 1160183589U,
			// Token: 0x04000471 RID: 1137
			LinkedPredicateAnyOfCcHeaderMemberOfException = 296148748U,
			// Token: 0x04000472 RID: 1138
			LinkedPredicateManagementRelationshipException = 4241674U,
			// Token: 0x04000473 RID: 1139
			LinkedPredicateSenderDomainIsException = 1296350534U,
			// Token: 0x04000474 RID: 1140
			ToDLAddressDisplayName = 1273212758U,
			// Token: 0x04000475 RID: 1141
			ADAttributeEvaluationTypeDisplayName = 1316087888U,
			// Token: 0x04000476 RID: 1142
			DisclaimerLocationDisplayName = 1461448431U,
			// Token: 0x04000477 RID: 1143
			ImportanceHigh = 1535769152U,
			// Token: 0x04000478 RID: 1144
			InboxRuleDescriptionStopProcessingRules = 2537686292U,
			// Token: 0x04000479 RID: 1145
			InboxRuleDescriptionIf = 4149032993U,
			// Token: 0x0400047A RID: 1146
			LinkedActionRemoveOME = 1251033108U,
			// Token: 0x0400047B RID: 1147
			RuleDescriptionAttachmentIsPasswordProtected = 3037379396U,
			// Token: 0x0400047C RID: 1148
			LinkedPredicateSubjectOrBodyContainsException = 89686081U,
			// Token: 0x0400047D RID: 1149
			ADAttributeManager = 494686544U,
			// Token: 0x0400047E RID: 1150
			ADAttributeOtherPhoneNumber = 3162495226U,
			// Token: 0x0400047F RID: 1151
			LinkedPredicateFromAddressMatchesException = 3800656714U,
			// Token: 0x04000480 RID: 1152
			EvaluationDescription = 3942685886U,
			// Token: 0x04000481 RID: 1153
			PrefixDisplayName = 575057689U,
			// Token: 0x04000482 RID: 1154
			LinkedActionRemoveHeader = 2936457540U,
			// Token: 0x04000483 RID: 1155
			LinkedPredicateSenderInRecipientList = 2128154505U,
			// Token: 0x04000484 RID: 1156
			JournalingParameterErrorGccWithOrganization = 2438278052U,
			// Token: 0x04000485 RID: 1157
			ExternalUser = 3631693406U,
			// Token: 0x04000486 RID: 1158
			RuleDescriptionStopRuleProcessing = 573203429U,
			// Token: 0x04000487 RID: 1159
			ErrorInboxRuleHasNoAction = 248554005U,
			// Token: 0x04000488 RID: 1160
			RuleDescriptionModerateMessageByManager = 2822159338U,
			// Token: 0x04000489 RID: 1161
			RuleDescriptionAttachmentProcessingLimitExceeded = 3080369366U,
			// Token: 0x0400048A RID: 1162
			DisclaimerTextDisplayName = 2547374239U,
			// Token: 0x0400048B RID: 1163
			HubParameterNotValidOnEdgeRole = 1704483791U,
			// Token: 0x0400048C RID: 1164
			LinkedActionModerateMessageByManager = 2511974477U,
			// Token: 0x0400048D RID: 1165
			ADAttributeDescription = 2319428331U,
			// Token: 0x0400048E RID: 1166
			InboxRuleDescriptionMyNameNotInToBox = 2845043929U,
			// Token: 0x0400048F RID: 1167
			ADAttributeCustomAttribute2 = 1377545168U,
			// Token: 0x04000490 RID: 1168
			JournalingParameterErrorGccWithoutRecipient = 777113388U,
			// Token: 0x04000491 RID: 1169
			ADAttributeEvaluationTypeEquals = 4027942746U,
			// Token: 0x04000492 RID: 1170
			LinkedPredicateFromAddressMatches = 2409541533U,
			// Token: 0x04000493 RID: 1171
			MessageDataClassificationDisplayName = 2388837118U,
			// Token: 0x04000494 RID: 1172
			FromAddressesDescription = 620435488U,
			// Token: 0x04000495 RID: 1173
			LinkedPredicateManagerIs = 1680325849U,
			// Token: 0x04000496 RID: 1174
			LinkedActionAddManagerAsRecipientType = 787621858U,
			// Token: 0x04000497 RID: 1175
			ADAttributeUserLogonName = 1452889642U,
			// Token: 0x04000498 RID: 1176
			ADAttributeNotes = 863112602U,
			// Token: 0x04000499 RID: 1177
			InboxRuleDescriptionSubscriptionNotFound = 3943169064U,
			// Token: 0x0400049A RID: 1178
			IncidentReportOriginalMailDescription = 914353404U,
			// Token: 0x0400049B RID: 1179
			JournalingParameterErrorExpiryDateWithoutGcc = 2376119956U,
			// Token: 0x0400049C RID: 1180
			WordsDescription = 587899315U,
			// Token: 0x0400049D RID: 1181
			PromptToOverwriteRulesOnImport = 1041810761U,
			// Token: 0x0400049E RID: 1182
			SenderIpRangesDescription = 638620002U,
			// Token: 0x0400049F RID: 1183
			LinkedPredicateAttachmentHasExecutableContentPredicateException = 3576176750U,
			// Token: 0x040004A0 RID: 1184
			DeleteMessageActionMustBeTheOnlyAction = 4116851739U,
			// Token: 0x040004A1 RID: 1185
			LinkedPredicateAnyOfToCcHeaderMemberOf = 3557196794U,
			// Token: 0x040004A2 RID: 1186
			InvalidClassification = 1505963751U,
			// Token: 0x040004A3 RID: 1187
			LinkedPredicateMessageContainsDataClassificationsException = 2003153304U,
			// Token: 0x040004A4 RID: 1188
			InboxRuleDescriptionMessageClassificationNotFound = 3380805386U,
			// Token: 0x040004A5 RID: 1189
			LinkedActionNotifySenderAction = 2049063793U,
			// Token: 0x040004A6 RID: 1190
			LinkedPredicateAttachmentExtensionMatchesWords = 1070980084U,
			// Token: 0x040004A7 RID: 1191
			RuleDescriptionApplyOME = 2365361139U,
			// Token: 0x040004A8 RID: 1192
			ExternalPartner = 715964235U,
			// Token: 0x040004A9 RID: 1193
			LinkedPredicateAttachmentSizeOverException = 1596558929U,
			// Token: 0x040004AA RID: 1194
			LinkedPredicateADAttributeComparisonException = 562852627U,
			// Token: 0x040004AB RID: 1195
			MessageTypeEncrypted = 3544120613U,
			// Token: 0x040004AC RID: 1196
			SenderNotificationTypeDescription = 1324690146U,
			// Token: 0x040004AD RID: 1197
			SenderNotificationTypeDisplayName = 2303954899U,
			// Token: 0x040004AE RID: 1198
			LinkedPredicateMessageTypeMatches = 3806797692U,
			// Token: 0x040004AF RID: 1199
			ADAttributeHomePhoneNumber = 1457839961U,
			// Token: 0x040004B0 RID: 1200
			LinkedPredicateFromAddressContains = 2334278303U,
			// Token: 0x040004B1 RID: 1201
			LinkedPredicateSubjectOrBodyMatches = 4162282226U,
			// Token: 0x040004B2 RID: 1202
			ADAttributeCompany = 2891753468U,
			// Token: 0x040004B3 RID: 1203
			EvaluatedUserRecipient = 2030715989U,
			// Token: 0x040004B4 RID: 1204
			RuleDescriptionAttachmentHasExecutableContent = 816385242U,
			// Token: 0x040004B5 RID: 1205
			LinkedPredicateHasClassificationException = 890082467U,
			// Token: 0x040004B6 RID: 1206
			LinkedPredicateAnyOfRecipientAddressContains = 398851013U,
			// Token: 0x040004B7 RID: 1207
			LinkedPredicateSclOver = 952041456U,
			// Token: 0x040004B8 RID: 1208
			LinkedActionDeleteMessage = 1485258685U,
			// Token: 0x040004B9 RID: 1209
			RuleDescriptionDisclaimerIgnoreFallback = 3943754367U,
			// Token: 0x040004BA RID: 1210
			RejectMessageActionMustBeTheOnlyAction = 3708149033U,
			// Token: 0x040004BB RID: 1211
			RuleDescriptionDisclaimerWrapFallback = 1708203343U,
			// Token: 0x040004BC RID: 1212
			InboxRuleDescriptionMyNameInToOrCcBox = 1663446825U,
			// Token: 0x040004BD RID: 1213
			LinkedPredicateMessageSizeOverException = 1780014951U,
			// Token: 0x040004BE RID: 1214
			CcRecipientType = 2476473719U,
			// Token: 0x040004BF RID: 1215
			ClientAccessRulesNameAlreadyInUse = 2808719285U,
			// Token: 0x040004C0 RID: 1216
			LinkedPredicateContentCharacterSetContainsWordsException = 2767244755U,
			// Token: 0x040004C1 RID: 1217
			LinkedPredicateSubjectContainsException = 403146382U,
			// Token: 0x040004C2 RID: 1218
			ADAttributeCustomAttribute13 = 1808276634U,
			// Token: 0x040004C3 RID: 1219
			ToAddressesDisplayName = 2828094182U,
			// Token: 0x040004C4 RID: 1220
			LinkedPredicateHasNoClassification = 3891477573U,
			// Token: 0x040004C5 RID: 1221
			LinkedActionQuarantine = 2269102077U,
			// Token: 0x040004C6 RID: 1222
			MessageTypePermissionControlled = 2872629304U,
			// Token: 0x040004C7 RID: 1223
			ADAttributeCustomAttribute9 = 1377545175U,
			// Token: 0x040004C8 RID: 1224
			SubTypeDescription = 4006689082U,
			// Token: 0x040004C9 RID: 1225
			LinkedPredicateSenderAttributeContainsException = 2743288863U,
			// Token: 0x040004CA RID: 1226
			ReportDestinationDisplayName = 972971379U,
			// Token: 0x040004CB RID: 1227
			InboxRuleDescriptionDeleteMessage = 2666086208U,
			// Token: 0x040004CC RID: 1228
			ToScopeDisplayName = 893471950U,
			// Token: 0x040004CD RID: 1229
			NotifySenderActionIsBeingOverridded = 4135645967U,
			// Token: 0x040004CE RID: 1230
			JournalingParameterErrorGccWithScope = 1386608555U,
			// Token: 0x040004CF RID: 1231
			RuleStateEnabled = 2720144322U,
			// Token: 0x040004D0 RID: 1232
			LinkedActionRedirectMessage = 3836787260U,
			// Token: 0x040004D1 RID: 1233
			ADAttributeCustomAttribute12 = 242192693U,
			// Token: 0x040004D2 RID: 1234
			RuleNameAlreadyExist = 1453324876U,
			// Token: 0x040004D3 RID: 1235
			LinkedPredicateAttachmentIsPasswordProtectedException = 1873569937U,
			// Token: 0x040004D4 RID: 1236
			ADAttributeMobileNumber = 2411750738U,
			// Token: 0x040004D5 RID: 1237
			LinkedPredicateADAttributeComparison = 3894352642U,
			// Token: 0x040004D6 RID: 1238
			EnhancedStatusCodeDisplayName = 1954471536U,
			// Token: 0x040004D7 RID: 1239
			InboxRuleDescriptionTakeActions = 889104748U,
			// Token: 0x040004D8 RID: 1240
			RejectMessageActionIsBeingOverridded = 3134498491U,
			// Token: 0x040004D9 RID: 1241
			ManagementRelationshipDisplayName = 553196496U,
			// Token: 0x040004DA RID: 1242
			AttachmentSizeDisplayName = 403803765U,
			// Token: 0x040004DB RID: 1243
			ClientAccessRuleRemoveDatacenterAdminsOnlyError = 2021688564U,
			// Token: 0x040004DC RID: 1244
			InvalidMessageDataClassificationEmptyName = 1957537238U,
			// Token: 0x040004DD RID: 1245
			RejectMessageActionType = 498572210U,
			// Token: 0x040004DE RID: 1246
			InboxRuleDescriptionMyNameInCcBox = 3512455487U,
			// Token: 0x040004DF RID: 1247
			LinkedPredicateRecipientDomainIs = 1408423837U,
			// Token: 0x040004E0 RID: 1248
			MissingDataClassificationsName = 3557021932U,
			// Token: 0x040004E1 RID: 1249
			LinkedActionGenerateIncidentReportAction = 3340948424U,
			// Token: 0x040004E2 RID: 1250
			InvalidAction = 1466544691U,
			// Token: 0x040004E3 RID: 1251
			MessageHeaderDescription = 115064938U,
			// Token: 0x040004E4 RID: 1252
			DisclaimerTextDescription = 742388560U,
			// Token: 0x040004E5 RID: 1253
			MessageSizeDescription = 3609362930U,
			// Token: 0x040004E6 RID: 1254
			SclValueDescription = 4164216509U,
			// Token: 0x040004E7 RID: 1255
			RejectUnlessSilentOverrideActionType = 2447598924U,
			// Token: 0x040004E8 RID: 1256
			LinkedPredicateManagerIsException = 4271012524U,
			// Token: 0x040004E9 RID: 1257
			MessageDataClassificationDescription = 2391892399U,
			// Token: 0x040004EA RID: 1258
			ImportanceLow = 2953542218U,
			// Token: 0x040004EB RID: 1259
			LinkedPredicateFromMemberOf = 785480795U,
			// Token: 0x040004EC RID: 1260
			InboxRuleDescriptionOr = 1467203807U,
			// Token: 0x040004ED RID: 1261
			LinkedPredicateAttachmentIsUnsupportedException = 2881863453U,
			// Token: 0x040004EE RID: 1262
			FromScopeDescription = 598779112U,
			// Token: 0x040004EF RID: 1263
			ExternalNonPartner = 2155604814U,
			// Token: 0x040004F0 RID: 1264
			ADAttributeCustomAttribute11 = 645477220U,
			// Token: 0x040004F1 RID: 1265
			FallbackActionDescription = 3309135616U,
			// Token: 0x040004F2 RID: 1266
			InboxRuleDescriptionMarkAsRead = 382355823U,
			// Token: 0x040004F3 RID: 1267
			LinkedPredicateSentToMemberOfException = 1118581569U,
			// Token: 0x040004F4 RID: 1268
			LinkedPredicateWithImportanceException = 4189101821U,
			// Token: 0x040004F5 RID: 1269
			ImportFileDataIsNull = 3339775592U,
			// Token: 0x040004F6 RID: 1270
			MessageTypeCalendaring = 1903193717U,
			// Token: 0x040004F7 RID: 1271
			NotifyOnlyActionType = 604363629U
		}

		// Token: 0x02000029 RID: 41
		private enum ParamIDs
		{
			// Token: 0x040004F9 RID: 1273
			IncidentReportConflictingParameters,
			// Token: 0x040004FA RID: 1274
			InboxRuleDescriptionMoveToFolder,
			// Token: 0x040004FB RID: 1275
			RuleDescriptionRecipientInSenderList,
			// Token: 0x040004FC RID: 1276
			RuleDescriptionSentToMemberOf,
			// Token: 0x040004FD RID: 1277
			RuleDescriptionManagementRelationship,
			// Token: 0x040004FE RID: 1278
			RuleDescriptionHasClassification,
			// Token: 0x040004FF RID: 1279
			InboxRuleDescriptionFlaggedForUnsupportedAction,
			// Token: 0x04000500 RID: 1280
			InvalidAuditSeverityLevel,
			// Token: 0x04000501 RID: 1281
			RuleDescriptionAnyOfToCcHeaderMemberOf,
			// Token: 0x04000502 RID: 1282
			CannotParseRuleDueToVersion,
			// Token: 0x04000503 RID: 1283
			RuleDescriptionHeaderContains,
			// Token: 0x04000504 RID: 1284
			CustomizedDsnNotConfigured,
			// Token: 0x04000505 RID: 1285
			ClientAccessRulesFilterPropertyRequired,
			// Token: 0x04000506 RID: 1286
			RuleDescriptionRecipientAttributeContains,
			// Token: 0x04000507 RID: 1287
			InboxRuleDescriptionApplyRetentionPolicyTag,
			// Token: 0x04000508 RID: 1288
			RuleDescriptionNotifySenderRejectUnlessExplicitOverride,
			// Token: 0x04000509 RID: 1289
			OutlookProtectionRuleRmsTemplateNotUnique,
			// Token: 0x0400050A RID: 1290
			RuleDescriptionNotifySenderRejectUnlessSilentOverride,
			// Token: 0x0400050B RID: 1291
			RuleDescriptionAttachmentExtensionMatchesWords,
			// Token: 0x0400050C RID: 1292
			InboxRuleDescriptionReceivedBeforeDate,
			// Token: 0x0400050D RID: 1293
			InboxRuleDescriptionCopyToFolder,
			// Token: 0x0400050E RID: 1294
			RuleDescriptionMessageSizeOver,
			// Token: 0x0400050F RID: 1295
			InboxRuleDescriptionFromSubscription,
			// Token: 0x04000510 RID: 1296
			InboxRuleDescriptionRedirectTo,
			// Token: 0x04000511 RID: 1297
			InboxRuleDescriptionSentTo,
			// Token: 0x04000512 RID: 1298
			InboxRuleDescriptionReceivedAfterDate,
			// Token: 0x04000513 RID: 1299
			ConditionIncompatibleWithNotifySenderAction,
			// Token: 0x04000514 RID: 1300
			ClientAccessRulesIpPropertyRequired,
			// Token: 0x04000515 RID: 1301
			RuleDescriptionADAttributeMatchesText,
			// Token: 0x04000516 RID: 1302
			RuleDescriptionSenderAttributeContains,
			// Token: 0x04000517 RID: 1303
			RuleDescriptionFromAddressMatches,
			// Token: 0x04000518 RID: 1304
			InboxRuleDescriptionFlaggedForAction,
			// Token: 0x04000519 RID: 1305
			RuleDescriptionSentToScope,
			// Token: 0x0400051A RID: 1306
			InboxRuleDescriptionForwardTo,
			// Token: 0x0400051B RID: 1307
			InboxRuleDescriptionForwardAsAttachmentTo,
			// Token: 0x0400051C RID: 1308
			InvalidMacroName,
			// Token: 0x0400051D RID: 1309
			ErrorRuleXmlTooBig,
			// Token: 0x0400051E RID: 1310
			RuleDescriptionModerateMessageByUser,
			// Token: 0x0400051F RID: 1311
			RuleStateParameterValueIsInconsistentWithDlpPolicyState,
			// Token: 0x04000520 RID: 1312
			RuleDescriptionAttachmentMatchesPatterns,
			// Token: 0x04000521 RID: 1313
			InboxRuleDescriptionReceivedBetweenDates,
			// Token: 0x04000522 RID: 1314
			InboxRuleDescriptionBodyContainsWords,
			// Token: 0x04000523 RID: 1315
			InboxRuleDescriptionSubjectOrBodyContainsWords,
			// Token: 0x04000524 RID: 1316
			MacroNameNotSpecified,
			// Token: 0x04000525 RID: 1317
			RuleDescriptionBlindCopyTo,
			// Token: 0x04000526 RID: 1318
			OutlookProtectionRuleRmsTemplateNotFound,
			// Token: 0x04000527 RID: 1319
			RuleDescriptionPrependSubject,
			// Token: 0x04000528 RID: 1320
			RuleDescriptionSentTo,
			// Token: 0x04000529 RID: 1321
			InboxRuleDescriptionWithSensitivity,
			// Token: 0x0400052A RID: 1322
			ClientAccessRulesUsernamePatternRequired,
			// Token: 0x0400052B RID: 1323
			ErrorTooManyRegexCharsInRuleCollection,
			// Token: 0x0400052C RID: 1324
			NoSmtpAddressForRecipientId,
			// Token: 0x0400052D RID: 1325
			RuleDescriptionCopyTo,
			// Token: 0x0400052E RID: 1326
			RuleDescriptionGenerateIncidentReport,
			// Token: 0x0400052F RID: 1327
			InboxRuleDescriptionFrom,
			// Token: 0x04000530 RID: 1328
			RuleDescriptionSetAuditSeverity,
			// Token: 0x04000531 RID: 1329
			AttachmentMetadataParameterContainsEmptyWords,
			// Token: 0x04000532 RID: 1330
			InvalidSmtpAddress,
			// Token: 0x04000533 RID: 1331
			InboxRuleDescriptionWithImportance,
			// Token: 0x04000534 RID: 1332
			RuleDescriptionApplyClassification,
			// Token: 0x04000535 RID: 1333
			RuleDescriptionAnyOfToHeader,
			// Token: 0x04000536 RID: 1334
			RuleDescriptionAttachmentPropertyContainsWords,
			// Token: 0x04000537 RID: 1335
			TestClientAccessRuleUserNotFoundOrMoreThanOne,
			// Token: 0x04000538 RID: 1336
			InboxRuleDescriptionWithSizeBetween,
			// Token: 0x04000539 RID: 1337
			InvalidRecipient,
			// Token: 0x0400053A RID: 1338
			HeaderNameNotAllowed,
			// Token: 0x0400053B RID: 1339
			InboxRuleDescriptionSendTextMessageNotificationTo,
			// Token: 0x0400053C RID: 1340
			RuleDescriptionAnyOfToCcHeader,
			// Token: 0x0400053D RID: 1341
			RuleDescriptionRouteMessageOutboundConnector,
			// Token: 0x0400053E RID: 1342
			RuleDescriptionHeaderMatches,
			// Token: 0x0400053F RID: 1343
			ExternalScopeInvalidInDc,
			// Token: 0x04000540 RID: 1344
			RuleDescriptionAddManagerAsRecipientType,
			// Token: 0x04000541 RID: 1345
			ErrorTooManyAddedRecipientsInRuleCollection,
			// Token: 0x04000542 RID: 1346
			CommentsHaveInvalidChars,
			// Token: 0x04000543 RID: 1347
			InvalidMessageDataClassificationParameterMinGreaterThanMax,
			// Token: 0x04000544 RID: 1348
			RuleDescriptionAttachmentContainsWords,
			// Token: 0x04000545 RID: 1349
			RuleDescriptionPrependHtmlDisclaimer,
			// Token: 0x04000546 RID: 1350
			RuleDescriptionFromScope,
			// Token: 0x04000547 RID: 1351
			RuleDescriptionContentCharacterSetContainsWords,
			// Token: 0x04000548 RID: 1352
			NoRecipientsForRecipientId,
			// Token: 0x04000549 RID: 1353
			RuleDescriptionMessageContainsDataClassifications,
			// Token: 0x0400054A RID: 1354
			AttachmentMetadataPropertyNotSpecified,
			// Token: 0x0400054B RID: 1355
			RuleDescriptionFrom,
			// Token: 0x0400054C RID: 1356
			RuleDescriptionGenerateNotification,
			// Token: 0x0400054D RID: 1357
			InboxRuleDescriptionRecipientAddressContainsWords,
			// Token: 0x0400054E RID: 1358
			RuleDescriptionAnyOfRecipientAddressContains,
			// Token: 0x0400054F RID: 1359
			RuleDescriptionSetScl,
			// Token: 0x04000550 RID: 1360
			RuleDescriptionSubjectOrBodyMatches,
			// Token: 0x04000551 RID: 1361
			RuleDescriptionSubjectMatches,
			// Token: 0x04000552 RID: 1362
			RuleDescriptionRejectMessage,
			// Token: 0x04000553 RID: 1363
			RuleDescriptionNotifySenderRejectMessage,
			// Token: 0x04000554 RID: 1364
			AtatchmentExtensionMatchesWordsParameterContainsWordsThatStartWithDot,
			// Token: 0x04000555 RID: 1365
			InvalidMessageDataClassificationParameterLessThanOne,
			// Token: 0x04000556 RID: 1366
			ErrorMaxParameterLengthExceeded,
			// Token: 0x04000557 RID: 1367
			RuleDescriptionManagerIs,
			// Token: 0x04000558 RID: 1368
			InvalidDisclaimerMacroName,
			// Token: 0x04000559 RID: 1369
			RuleDescriptionRecipientAddressContains,
			// Token: 0x0400055A RID: 1370
			HubServerVersionNotFound,
			// Token: 0x0400055B RID: 1371
			RuleDescriptionSenderDomainIs,
			// Token: 0x0400055C RID: 1372
			CorruptRuleCollection,
			// Token: 0x0400055D RID: 1373
			RuleDescriptionAnyOfCcHeaderMemberOf,
			// Token: 0x0400055E RID: 1374
			RuleDescriptionBetweenMemberOf,
			// Token: 0x0400055F RID: 1375
			CannotCreateRuleDueToVersion,
			// Token: 0x04000560 RID: 1376
			RuleDescriptionAddToRecipient,
			// Token: 0x04000561 RID: 1377
			InvalidMessageClassification,
			// Token: 0x04000562 RID: 1378
			RuleDescriptionRecipientAttributeMatches,
			// Token: 0x04000563 RID: 1379
			InboxRuleDescriptionWithSizeLessThan,
			// Token: 0x04000564 RID: 1380
			RuleNotFound,
			// Token: 0x04000565 RID: 1381
			ClientAccessRuleActionNotSupported,
			// Token: 0x04000566 RID: 1382
			OutboundConnectorIdNotFound,
			// Token: 0x04000567 RID: 1383
			InboxRuleDescriptionMessageType,
			// Token: 0x04000568 RID: 1384
			RuleDescriptionRemoveHeader,
			// Token: 0x04000569 RID: 1385
			RuleDescriptionSubjectOrBodyContains,
			// Token: 0x0400056A RID: 1386
			ServerVersionNull,
			// Token: 0x0400056B RID: 1387
			OutlookProtectionRuleClassificationNotUnique,
			// Token: 0x0400056C RID: 1388
			CommentsTooLong,
			// Token: 0x0400056D RID: 1389
			RuleDescriptionRedirectMessage,
			// Token: 0x0400056E RID: 1390
			OutlookProtectionRuleClassificationNotFound,
			// Token: 0x0400056F RID: 1391
			RuleDescriptionLogEvent,
			// Token: 0x04000570 RID: 1392
			RuleDescriptionSetHeader,
			// Token: 0x04000571 RID: 1393
			InboxRuleDescriptionHeaderContainsWords,
			// Token: 0x04000572 RID: 1394
			RuleDescriptionWithImportance,
			// Token: 0x04000573 RID: 1395
			InboxRuleDescriptionFromAddressContainsWords,
			// Token: 0x04000574 RID: 1396
			InvalidMessageDataClassificationParameterConfidenceExceedsMaxAllowed,
			// Token: 0x04000575 RID: 1397
			ClientAccessRuleWillBeAddedToCollection,
			// Token: 0x04000576 RID: 1398
			InboxRuleDescriptionApplyCategory,
			// Token: 0x04000577 RID: 1399
			RuleDescriptionAttachmentSizeOver,
			// Token: 0x04000578 RID: 1400
			ClientAccessRulesAuthenticationTypeRequired,
			// Token: 0x04000579 RID: 1401
			RuleDescriptionSenderAttributeMatches,
			// Token: 0x0400057A RID: 1402
			InboxRuleDescriptionSubjectContainsWords,
			// Token: 0x0400057B RID: 1403
			InboxRuleDescriptionWithSizeGreaterThan,
			// Token: 0x0400057C RID: 1404
			ClientAccessRulesPortRangePropertyRequired,
			// Token: 0x0400057D RID: 1405
			RuleDescriptionAnyOfToHeaderMemberOf,
			// Token: 0x0400057E RID: 1406
			RuleDescriptionRightsProtectMessage,
			// Token: 0x0400057F RID: 1407
			RuleDescriptionAnyOfCcHeader,
			// Token: 0x04000580 RID: 1408
			RuleDescriptionRecipientDomainIs,
			// Token: 0x04000581 RID: 1409
			RuleDescriptionAnyOfRecipientAddressMatches,
			// Token: 0x04000582 RID: 1410
			RuleDescriptionApplyHtmlDisclaimer,
			// Token: 0x04000583 RID: 1411
			ClientAccessRuleWillBeConsideredEnabled,
			// Token: 0x04000584 RID: 1412
			RuleDescriptionFromMemberOf,
			// Token: 0x04000585 RID: 1413
			CannotModifyRuleDueToVersion,
			// Token: 0x04000586 RID: 1414
			InboxRuleDescriptionHasClassification,
			// Token: 0x04000587 RID: 1415
			CorruptRule,
			// Token: 0x04000588 RID: 1416
			TestClientAccessRuleFoundUsername,
			// Token: 0x04000589 RID: 1417
			RuleDescriptionADAttributeComparison,
			// Token: 0x0400058A RID: 1418
			InvalidRecipientForModerationAction,
			// Token: 0x0400058B RID: 1419
			RuleDescriptionMessageTypeMatches,
			// Token: 0x0400058C RID: 1420
			RuleDescriptionIpRanges,
			// Token: 0x0400058D RID: 1421
			ConfirmationMessageSetClientAccessRule,
			// Token: 0x0400058E RID: 1422
			IncompleteParameterGroup,
			// Token: 0x0400058F RID: 1423
			MoreThanOneRecipientForRecipientId,
			// Token: 0x04000590 RID: 1424
			RuleDescriptionNotifySenderRejectUnlessFalsePositiveOverride,
			// Token: 0x04000591 RID: 1425
			ConditionIncompatibleWithTheSubtype,
			// Token: 0x04000592 RID: 1426
			ClientAccessRulesLimitError,
			// Token: 0x04000593 RID: 1427
			RuleDescriptionSubjectContains,
			// Token: 0x04000594 RID: 1428
			InboxRuleDescriptionMarkImportance,
			// Token: 0x04000595 RID: 1429
			ErrorAddedRecipientCannotBeDistributionGroup,
			// Token: 0x04000596 RID: 1430
			ClientAccessRulesProtocolPropertyRequired,
			// Token: 0x04000597 RID: 1431
			RuleDescriptionSenderInRecipientList,
			// Token: 0x04000598 RID: 1432
			ExportSkippedE15Rules,
			// Token: 0x04000599 RID: 1433
			RuleDescriptionRecipientAddressMatches,
			// Token: 0x0400059A RID: 1434
			InvalidMessageDataClassification,
			// Token: 0x0400059B RID: 1435
			ErrorTooManyTransportRules,
			// Token: 0x0400059C RID: 1436
			RuleDescriptionFromAddressContains,
			// Token: 0x0400059D RID: 1437
			ConfirmationMessageNewClientAccessRule,
			// Token: 0x0400059E RID: 1438
			RuleDescriptionSclOver,
			// Token: 0x0400059F RID: 1439
			InvalidRegex,
			// Token: 0x040005A0 RID: 1440
			RuleDescriptionAttachmentNameMatches,
			// Token: 0x040005A1 RID: 1441
			InvalidDlpPolicy,
			// Token: 0x040005A2 RID: 1442
			ConfirmationMessageRemoveClientAccessRule
		}
	}
}
