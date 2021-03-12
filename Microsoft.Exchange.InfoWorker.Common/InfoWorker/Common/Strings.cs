using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x0200031E RID: 798
	internal static class Strings
	{
		// Token: 0x0600176E RID: 5998 RVA: 0x0006E9A4 File Offset: 0x0006CBA4
		static Strings()
		{
			Strings.stringIDs.Add(4118843607U, "TrackingErrorCrossPremiseMisconfiguration");
			Strings.stringIDs.Add(887069165U, "LogFieldsResultSizeCopied");
			Strings.stringIDs.Add(2434190600U, "MailtipsApplicationName");
			Strings.stringIDs.Add(3039411601U, "descNotDefaultCalendar");
			Strings.stringIDs.Add(3645107181U, "TrackingPermanentError");
			Strings.stringIDs.Add(1560475167U, "WrongTargetServer");
			Strings.stringIDs.Add(4159961541U, "InvalidSearchQuery");
			Strings.stringIDs.Add(2177919315U, "ProgressCompletingSearch");
			Strings.stringIDs.Add(3855823295U, "descWorkHoursStartTimeInvalid");
			Strings.stringIDs.Add(996171111U, "descMailboxLogonFailed");
			Strings.stringIDs.Add(3025732470U, "InstallAssistantsServiceTask");
			Strings.stringIDs.Add(4098403379U, "MessageInvalidTimeZoneTransitionGroupNullId");
			Strings.stringIDs.Add(3696858122U, "FailedCommunicationException");
			Strings.stringIDs.Add(3416023611U, "LogFieldsNumberUnsuccessfulMailboxes");
			Strings.stringIDs.Add(4244107904U, "SearchNotStarted");
			Strings.stringIDs.Add(2960157992U, "TrackingErrorConnectivity");
			Strings.stringIDs.Add(946397903U, "TargetFolder");
			Strings.stringIDs.Add(1774313355U, "Unsearchable");
			Strings.stringIDs.Add(3752482865U, "LogFieldsRecipients");
			Strings.stringIDs.Add(357978241U, "InvalidPreviewItemInResultRows");
			Strings.stringIDs.Add(2642055223U, "descInvalidSmptAddres");
			Strings.stringIDs.Add(2249986005U, "TrackingErrorBudgetExceeded");
			Strings.stringIDs.Add(1952477638U, "LogFieldsKeywordMbxs");
			Strings.stringIDs.Add(859073841U, "descQueryGenerationNotRequired");
			Strings.stringIDs.Add(3528487049U, "TrackingNoDefaultDomain");
			Strings.stringIDs.Add(457909195U, "InvalidChangeKeyReturned");
			Strings.stringIDs.Add(3832545148U, "ArchiveMailbox");
			Strings.stringIDs.Add(4235476896U, "LogFieldsKeywordHitCount");
			Strings.stringIDs.Add(824833090U, "LogFieldsName");
			Strings.stringIDs.Add(3196432253U, "LogFieldsStatus");
			Strings.stringIDs.Add(1572073070U, "LogFieldsEndDate");
			Strings.stringIDs.Add(1970283657U, "LogFieldsSenders");
			Strings.stringIDs.Add(1734826510U, "TrackingErrorTimeBudgetExceeded");
			Strings.stringIDs.Add(127105864U, "TrackingLogVersionIncompatible");
			Strings.stringIDs.Add(4195952492U, "AutodiscoverFailedException");
			Strings.stringIDs.Add(215769503U, "descInvalidMaxNonWorkHourResultsPerDay");
			Strings.stringIDs.Add(1477200533U, "descNullAutoDiscoverResponse");
			Strings.stringIDs.Add(1446717588U, "UnexpectedRemoteDataException");
			Strings.stringIDs.Add(4270171893U, "LogFieldsIncludeKeywordStatistics");
			Strings.stringIDs.Add(2870421805U, "UnexpectedError");
			Strings.stringIDs.Add(2171828426U, "descInvalidMaximumResults");
			Strings.stringIDs.Add(1023107612U, "descOOFCannotReadOofConfigData");
			Strings.stringIDs.Add(366714169U, "descInvalidSecurityDescriptor");
			Strings.stringIDs.Add(540675128U, "InvalidResultMerge");
			Strings.stringIDs.Add(252553502U, "SearchObjectNotFound");
			Strings.stringIDs.Add(1359157944U, "TrackingInstanceBudgetExceeded");
			Strings.stringIDs.Add(3961981453U, "MessageInvalidTimeZoneOutOfRange");
			Strings.stringIDs.Add(3014888409U, "LogFieldsStartDate");
			Strings.stringIDs.Add(273956641U, "NotOperator");
			Strings.stringIDs.Add(959712720U, "LogFieldsSourceRecipients");
			Strings.stringIDs.Add(1261494857U, "TrackingErrorQueueViewerRpc");
			Strings.stringIDs.Add(2084277545U, "SearchLogHeader");
			Strings.stringIDs.Add(609234870U, "MessageInvalidTimeZoneInvalidOffsetFormat");
			Strings.stringIDs.Add(669181240U, "descInvalidCredentials");
			Strings.stringIDs.Add(3577556142U, "LogFieldsSearchOperation");
			Strings.stringIDs.Add(3644766027U, "MessageInvalidTimeZoneNonFirstTransition");
			Strings.stringIDs.Add(184705256U, "descMeetingSuggestionsDurationTooSmall");
			Strings.stringIDs.Add(1884462766U, "DeleteItemsFailed");
			Strings.stringIDs.Add(2550395136U, "PendingSynchronizationException");
			Strings.stringIDs.Add(4277240717U, "LogFieldsNumberMailboxesToSearch");
			Strings.stringIDs.Add(2182808069U, "LogFieldsKeywordKeyword");
			Strings.stringIDs.Add(2422079678U, "ObjectNotFound");
			Strings.stringIDs.Add(3865092385U, "MessageInvalidTimeZoneMissedPeriod");
			Strings.stringIDs.Add(3051609629U, "LogFieldsResultSize");
			Strings.stringIDs.Add(447822483U, "MessageInvalidTimeZoneDayOfWeekValue");
			Strings.stringIDs.Add(3099813970U, "TrackingBusy");
			Strings.stringIDs.Add(3278774409U, "InvalidAppointmentException");
			Strings.stringIDs.Add(1105384606U, "descInvalidNetworkServiceContext");
			Strings.stringIDs.Add(1524653606U, "ResultsNotDeduped");
			Strings.stringIDs.Add(610144303U, "ErrorTimeZone");
			Strings.stringIDs.Add(2456116760U, "BatchSynchronizationFailedException");
			Strings.stringIDs.Add(876631183U, "descStartAndEndTimesOutSideFreeBusyData");
			Strings.stringIDs.Add(4252318527U, "descWin32InteropError");
			Strings.stringIDs.Add(3219875537U, "TrackingErrorLegacySender");
			Strings.stringIDs.Add(3436346834U, "descNoEwsResponse");
			Strings.stringIDs.Add(3301704787U, "LogFieldsResultsLink");
			Strings.stringIDs.Add(2977121749U, "TrackingErrorLegacySenderMultiMessageSearch");
			Strings.stringIDs.Add(2205699811U, "LogFieldsManagedBy");
			Strings.stringIDs.Add(1676524031U, "KeywordHitEmptyQuery");
			Strings.stringIDs.Add(2152565173U, "SearchQueryEmpty");
			Strings.stringIDs.Add(2705707569U, "ServerShutdown");
			Strings.stringIDs.Add(377459750U, "TrackingErrorSuffixForAdministrator");
			Strings.stringIDs.Add(4273761671U, "SubscriptionNotFoundException");
			Strings.stringIDs.Add(2881797395U, "LogFieldsResultSizeEstimate");
			Strings.stringIDs.Add(3863825871U, "descNullDateInChangeDate");
			Strings.stringIDs.Add(232000386U, "SharingFolderNotFoundException");
			Strings.stringIDs.Add(1607453502U, "LogMailAll");
			Strings.stringIDs.Add(3106977401U, "descInvalidScheduledOofDuration");
			Strings.stringIDs.Add(1899968567U, "TrackingTransientErrorMultiMessageSearch");
			Strings.stringIDs.Add(2680389304U, "AqsParserError");
			Strings.stringIDs.Add(829039958U, "LegacyOofMessage");
			Strings.stringIDs.Add(118976146U, "descInvalidClientSecurityContext");
			Strings.stringIDs.Add(3912319220U, "LogMailFooter");
			Strings.stringIDs.Add(2822320878U, "descInvalidFormatInEmail");
			Strings.stringIDs.Add(2977444274U, "descNoCalendar");
			Strings.stringIDs.Add(1018641786U, "UnexpectedUserResponses");
			Strings.stringIDs.Add(2215464039U, "LogFieldsIdentity");
			Strings.stringIDs.Add(3854866410U, "FreeBusyApplicationName");
			Strings.stringIDs.Add(2236533269U, "SearchAborted");
			Strings.stringIDs.Add(1756460035U, "AutodiscoverTimedOut");
			Strings.stringIDs.Add(3871609939U, "LogFieldsSearchDumpster");
			Strings.stringIDs.Add(274121238U, "descInvalidAuthorizationContext");
			Strings.stringIDs.Add(4205846133U, "LogFieldsMessageTypes");
			Strings.stringIDs.Add(3832116504U, "PositiveParameter");
			Strings.stringIDs.Add(1114362743U, "LogMailSeeAttachment");
			Strings.stringIDs.Add(3835343804U, "descMeetingSuggestionsInvalidTimeInterval");
			Strings.stringIDs.Add(2769462193U, "KeywordStatsNotRequested");
			Strings.stringIDs.Add(1382860876U, "RbacTargetMailboxAuthorizationError");
			Strings.stringIDs.Add(636866827U, "SortedResultNullParameters");
			Strings.stringIDs.Add(1214298741U, "LogFieldsLastEndTime");
			Strings.stringIDs.Add(55350379U, "TrackingErrorInvalidADData");
			Strings.stringIDs.Add(3068064914U, "LogFieldsResume");
			Strings.stringIDs.Add(3770945073U, "descWorkHoursStartEndInvalid");
			Strings.stringIDs.Add(872236584U, "SearchAlreadStarted");
			Strings.stringIDs.Add(908708480U, "MessageInvalidTimeZoneReferenceToPeriod");
			Strings.stringIDs.Add(489766921U, "ProgressSearchingInProgress");
			Strings.stringIDs.Add(3558390196U, "descCorruptUserOofSettingsXmlDocument");
			Strings.stringIDs.Add(1295345518U, "CrossForestNotSupported");
			Strings.stringIDs.Add(3240652606U, "LogFieldsNumberSuccessfulMailboxes");
			Strings.stringIDs.Add(1087048587U, "descIdentityArrayEmpty");
			Strings.stringIDs.Add(2265146620U, "MessageInvalidTimeZoneReferenceToGroup");
			Strings.stringIDs.Add(1894419184U, "descInvalidTimeZoneBias");
			Strings.stringIDs.Add(1223102710U, "descPublicFolderServerNotFound");
			Strings.stringIDs.Add(2319913820U, "TrackingErrorCrossPremiseAuthentication");
			Strings.stringIDs.Add(3558532322U, "MessageInvalidTimeZonePeriodNullId");
			Strings.stringIDs.Add(147513433U, "StorePermanantError");
			Strings.stringIDs.Add(3376565836U, "TrackingErrorTransientUnexpected");
			Strings.stringIDs.Add(1389565281U, "TrackingErrorModerationDecisionLogsFromE14Rtm");
			Strings.stringIDs.Add(3931032456U, "TrackingErrorCASUriDiscovery");
			Strings.stringIDs.Add(2307745798U, "descFailedToFindPublicFolderServer");
			Strings.stringIDs.Add(1664401949U, "MessageTrackingApplicationName");
			Strings.stringIDs.Add(3586747816U, "descAutoDiscoverThruDirectoryFailed");
			Strings.stringIDs.Add(468011246U, "MessageInvalidTimeZoneDuplicatePeriods");
			Strings.stringIDs.Add(1053420363U, "LogFieldsPercentComplete");
			Strings.stringIDs.Add(863256271U, "descFreeBusyAndSuggestionsNull");
			Strings.stringIDs.Add(3837654127U, "LogFieldsSuccessfulMailboxes");
			Strings.stringIDs.Add(534171625U, "UninstallAssistantsServiceTask");
			Strings.stringIDs.Add(2865084951U, "ProgressSearching");
			Strings.stringIDs.Add(193137347U, "SearchInvalidPagination");
			Strings.stringIDs.Add(2006533237U, "LogFieldsSearchQuery");
			Strings.stringIDs.Add(1759034327U, "TrackingTotalBudgetExceeded");
			Strings.stringIDs.Add(2697293287U, "descSuggestionMustStartOnThirtyMinuteBoundary");
			Strings.stringIDs.Add(3124261155U, "SearchServerShutdown");
			Strings.stringIDs.Add(4107635786U, "descFailedToCreateLegacyOofRule");
			Strings.stringIDs.Add(4123771088U, "PhotosApplicationName");
			Strings.stringIDs.Add(1880771860U, "ADUserNotFoundException");
			Strings.stringIDs.Add(4285214785U, "ProgressSearchingSources");
			Strings.stringIDs.Add(2804157263U, "LogMailNone");
			Strings.stringIDs.Add(1233757588U, "LogFieldsStatusMailRecipients");
			Strings.stringIDs.Add(3305370967U, "LogFieldsResultNumber");
			Strings.stringIDs.Add(4287340460U, "TrackingErrorPermanentUnexpected");
			Strings.stringIDs.Add(1954290077U, "ProgressCompleting");
			Strings.stringIDs.Add(270334674U, "descInvalidUserOofSettings");
			Strings.stringIDs.Add(1275939085U, "LogFieldsResultNumberEstimate");
			Strings.stringIDs.Add(3222940042U, "LogFieldsKeywordHits");
			Strings.stringIDs.Add(3351215994U, "UnknownError");
			Strings.stringIDs.Add(1265936670U, "MessageInvalidTimeZonePeriodNullBias");
			Strings.stringIDs.Add(2937454784U, "MailboxSeachCountIncludeUnsearchable");
			Strings.stringIDs.Add(859087807U, "LogMailBlank");
			Strings.stringIDs.Add(3091073294U, "descWorkHoursEndTimeInvalid");
			Strings.stringIDs.Add(2858750991U, "ProgessCreatingThreads");
			Strings.stringIDs.Add(2569697819U, "descOOFCannotReadExternalOofOptions");
			Strings.stringIDs.Add(1910512436U, "PrimaryMailbox");
			Strings.stringIDs.Add(2852570616U, "MessageInvalidTimeZoneCustomTimeZoneThreeElements");
			Strings.stringIDs.Add(763976563U, "TrackingTransientError");
			Strings.stringIDs.Add(1809134602U, "LogFieldsLastStartTime");
			Strings.stringIDs.Add(1824636832U, "CopyItemsFailed");
			Strings.stringIDs.Add(1910425077U, "DummyApplicationName");
			Strings.stringIDs.Add(3276944824U, "MessageInvalidTimeZoneDuplicateGroups");
			Strings.stringIDs.Add(1614878877U, "ExecutingUserNeedSmtpAddress");
			Strings.stringIDs.Add(3442221872U, "MessageInvalidTimeZoneMoreThanTwoPeriodsUnsupported");
			Strings.stringIDs.Add(3955434964U, "descMailRecipientNotFound");
			Strings.stringIDs.Add(2671831934U, "descNullUserName");
			Strings.stringIDs.Add(1407995413U, "NonNegativeParameter");
			Strings.stringIDs.Add(1633879312U, "LogFieldsEstimateNotExcludeDuplicates");
			Strings.stringIDs.Add(18603439U, "StoreTransientError");
			Strings.stringIDs.Add(1541817855U, "LogFieldsLogLevel");
			Strings.stringIDs.Add(2131297471U, "TrackingWSRequestCorrupt");
			Strings.stringIDs.Add(2936774726U, "SearchDisabled");
			Strings.stringIDs.Add(1705124535U, "descNullCredentialsToServiceDiscoveryRequest");
			Strings.stringIDs.Add(697045372U, "LogFieldsUnsuccessfulMailboxes");
			Strings.stringIDs.Add(2530022313U, "MessageInvalidTimeZoneTimeZoneNotFound");
			Strings.stringIDs.Add(3753969000U, "MessageInvalidTimeZoneIntValueIsInvalid");
			Strings.stringIDs.Add(3488925402U, "ScheduleConfigurationSchedule");
			Strings.stringIDs.Add(3675304455U, "descOofRuleSaveException");
			Strings.stringIDs.Add(3342799224U, "LogFieldsTargetMailbox");
			Strings.stringIDs.Add(1441197009U, "descInvalidSuggestionsTimeRange");
			Strings.stringIDs.Add(670434636U, "descFailedToGetUserOofPolicy");
			Strings.stringIDs.Add(983402700U, "descMeetingSuggestionsDurationTooLarge");
			Strings.stringIDs.Add(2411109197U, "ProgressOpening");
			Strings.stringIDs.Add(1471504673U, "TrackingErrorBudgetExceededMultiMessageSearch");
			Strings.stringIDs.Add(2991551487U, "descLocalServerObjectNotFound");
			Strings.stringIDs.Add(2706524390U, "RecoverableItems");
			Strings.stringIDs.Add(3367175223U, "descProxyRequestFailed");
			Strings.stringIDs.Add(516398220U, "ProgessCreating");
			Strings.stringIDs.Add(924370705U, "descClientDisconnected");
			Strings.stringIDs.Add(3760653578U, "SearchThrottled");
			Strings.stringIDs.Add(1620607520U, "descInvalidTransitionTime");
			Strings.stringIDs.Add(2976781692U, "TargetMailboxOutOfSpace");
			Strings.stringIDs.Add(1848520589U, "LogFieldsLastRunBy");
			Strings.stringIDs.Add(4135638738U, "UnableToReadServiceTopology");
			Strings.stringIDs.Add(607437832U, "descElcRootFolderName");
			Strings.stringIDs.Add(647128549U, "descNoMailTipsInEwsResponseMessage");
			Strings.stringIDs.Add(4209067056U, "descFailedToGetRules");
			Strings.stringIDs.Add(794348359U, "LogFieldsStoppedBy");
			Strings.stringIDs.Add(2268440642U, "descProxyRequestProcessingSocketError");
			Strings.stringIDs.Add(3466285021U, "descProxyRequestNotAllowed");
			Strings.stringIDs.Add(1848500058U, "descInvalidAccessLevel");
			Strings.stringIDs.Add(784192305U, "TrackingPermanentErrorMultiMessageSearch");
			Strings.stringIDs.Add(2804255286U, "LogFieldsCreatedBy");
			Strings.stringIDs.Add(1225651387U, "LowSystemResource");
			Strings.stringIDs.Add(854567314U, "TrackingErrorLogSearchServiceDown");
			Strings.stringIDs.Add(2312386357U, "LogMailNotApplicable");
			Strings.stringIDs.Add(3289802537U, "descNoFreeBusyAccess");
			Strings.stringIDs.Add(348874544U, "ADUserMisconfiguredException");
			Strings.stringIDs.Add(2033796534U, "ProgressOpeningTarget");
			Strings.stringIDs.Add(1554201361U, "descOOFVirusDetectedOofReplyMessage");
			Strings.stringIDs.Add(2912574056U, "MessageInvalidTimeZonePeriodNullName");
			Strings.stringIDs.Add(3332140560U, "MessageInvalidTimeZoneFirstTransition");
			Strings.stringIDs.Add(3216622764U, "InvalidContactException");
			Strings.stringIDs.Add(3436010521U, "ErrorRemoveOngoingSearch");
			Strings.stringIDs.Add(1594184545U, "TrackingErrorReadStatus");
			Strings.stringIDs.Add(3134958540U, "TrackingErrorCrossForestAuthentication");
			Strings.stringIDs.Add(1892050364U, "descInvalidFreeBusyViewType");
			Strings.stringIDs.Add(3341082622U, "descLogonAsNetworkServiceFailed");
			Strings.stringIDs.Add(2837247303U, "TrackingErrorCrossForestMisconfiguration");
			Strings.stringIDs.Add(2100288003U, "MessageInvalidTimeZoneMissedGroup");
			Strings.stringIDs.Add(478692077U, "NoKeywordStatsForCopySearch");
			Strings.stringIDs.Add(3248017497U, "descMailboxFailover");
			Strings.stringIDs.Add(1104837598U, "LogFieldsExcludeDuplicateMessages");
			Strings.stringIDs.Add(1743551038U, "LogFieldsErrors");
			Strings.stringIDs.Add(3607788283U, "mailTipsTenant");
			Strings.stringIDs.Add(3153221581U, "SearchArgument");
			Strings.stringIDs.Add(3176897035U, "TrackingErrorCrossPremiseMisconfigurationMultiMessageSearch");
			Strings.stringIDs.Add(2497578736U, "descNullEmailToAutoDiscoverRequest");
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x0006FC3F File Offset: 0x0006DE3F
		public static LocalizedString TrackingErrorCrossPremiseMisconfiguration
		{
			get
			{
				return new LocalizedString("TrackingErrorCrossPremiseMisconfiguration", "Ex162699", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0006FC60 File Offset: 0x0006DE60
		public static LocalizedString SearchServerFailed(string displayName, int code, string message)
		{
			return new LocalizedString("SearchServerFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				displayName,
				code,
				message
			});
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0006FC9C File Offset: 0x0006DE9C
		public static LocalizedString descElcCannotFindDefaultFolder(string folderName, string mailboxName)
		{
			return new LocalizedString("descElcCannotFindDefaultFolder", "ExDFC4E4", false, true, Strings.ResourceManager, new object[]
			{
				folderName,
				mailboxName
			});
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0006FCD0 File Offset: 0x0006DED0
		public static LocalizedString LogMailHeaderInstructions(string name)
		{
			return new LocalizedString("LogMailHeaderInstructions", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x0006FCFF File Offset: 0x0006DEFF
		public static LocalizedString LogFieldsResultSizeCopied
		{
			get
			{
				return new LocalizedString("LogFieldsResultSizeCopied", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0006FD20 File Offset: 0x0006DF20
		public static LocalizedString descPublicFolderRequestProcessingError(string exceptionMessage, string currentRequestString)
		{
			return new LocalizedString("descPublicFolderRequestProcessingError", "ExE57497", false, true, Strings.ResourceManager, new object[]
			{
				exceptionMessage,
				currentRequestString
			});
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x0006FD53 File Offset: 0x0006DF53
		public static LocalizedString MailtipsApplicationName
		{
			get
			{
				return new LocalizedString("MailtipsApplicationName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0006FD74 File Offset: 0x0006DF74
		public static LocalizedString descConfigurationInformationNotFound(string dnsDomainName)
		{
			return new LocalizedString("descConfigurationInformationNotFound", "Ex49339A", false, true, Strings.ResourceManager, new object[]
			{
				dnsDomainName
			});
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x0006FDA3 File Offset: 0x0006DFA3
		public static LocalizedString descNotDefaultCalendar
		{
			get
			{
				return new LocalizedString("descNotDefaultCalendar", "ExCD1E1C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x0006FDC1 File Offset: 0x0006DFC1
		public static LocalizedString TrackingPermanentError
		{
			get
			{
				return new LocalizedString("TrackingPermanentError", "Ex99D8B3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0006FDE0 File Offset: 0x0006DFE0
		public static LocalizedString InvalidReferenceItemInPreviewResult(string url)
		{
			return new LocalizedString("InvalidReferenceItemInPreviewResult", "", false, false, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0006FE0F File Offset: 0x0006E00F
		public static LocalizedString WrongTargetServer
		{
			get
			{
				return new LocalizedString("WrongTargetServer", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0006FE30 File Offset: 0x0006E030
		public static LocalizedString descVirusScanInProgress(string emailAddress)
		{
			return new LocalizedString("descVirusScanInProgress", "ExB58C61", false, true, Strings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x0006FE5F File Offset: 0x0006E05F
		public static LocalizedString InvalidSearchQuery
		{
			get
			{
				return new LocalizedString("InvalidSearchQuery", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0006FE80 File Offset: 0x0006E080
		public static LocalizedString InconsistentSortedResults(string mailboxGuid, string referenceItem, string item)
		{
			return new LocalizedString("InconsistentSortedResults", "", false, false, Strings.ResourceManager, new object[]
			{
				mailboxGuid,
				referenceItem,
				item
			});
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0006FEB8 File Offset: 0x0006E0B8
		public static LocalizedString InvalidSortedResultParameter(string first, string second)
		{
			return new LocalizedString("InvalidSortedResultParameter", "", false, false, Strings.ResourceManager, new object[]
			{
				first,
				second
			});
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x0006FEEB File Offset: 0x0006E0EB
		public static LocalizedString ProgressCompletingSearch
		{
			get
			{
				return new LocalizedString("ProgressCompletingSearch", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0006FF0C File Offset: 0x0006E10C
		public static LocalizedString InvalidOwaUrlInPreviewResult(string errorHint, string url)
		{
			return new LocalizedString("InvalidOwaUrlInPreviewResult", "", false, false, Strings.ResourceManager, new object[]
			{
				errorHint,
				url
			});
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0006FF40 File Offset: 0x0006E140
		public static LocalizedString DeleteItemFailedForMessage(string exception)
		{
			return new LocalizedString("DeleteItemFailedForMessage", "", false, false, Strings.ResourceManager, new object[]
			{
				exception
			});
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x0006FF6F File Offset: 0x0006E16F
		public static LocalizedString descWorkHoursStartTimeInvalid
		{
			get
			{
				return new LocalizedString("descWorkHoursStartTimeInvalid", "Ex9A9647", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0006FF90 File Offset: 0x0006E190
		public static LocalizedString descMissingMailboxArrayElement(string index)
		{
			return new LocalizedString("descMissingMailboxArrayElement", "ExAC1F58", false, true, Strings.ResourceManager, new object[]
			{
				index
			});
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x0006FFBF File Offset: 0x0006E1BF
		public static LocalizedString descMailboxLogonFailed
		{
			get
			{
				return new LocalizedString("descMailboxLogonFailed", "Ex210994", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0006FFDD File Offset: 0x0006E1DD
		public static LocalizedString InstallAssistantsServiceTask
		{
			get
			{
				return new LocalizedString("InstallAssistantsServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x0006FFFB File Offset: 0x0006E1FB
		public static LocalizedString MessageInvalidTimeZoneTransitionGroupNullId
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneTransitionGroupNullId", "Ex63A053", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x00070019 File Offset: 0x0006E219
		public static LocalizedString FailedCommunicationException
		{
			get
			{
				return new LocalizedString("FailedCommunicationException", "Ex1F3BFA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x00070037 File Offset: 0x0006E237
		public static LocalizedString LogFieldsNumberUnsuccessfulMailboxes
		{
			get
			{
				return new LocalizedString("LogFieldsNumberUnsuccessfulMailboxes", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00070058 File Offset: 0x0006E258
		public static LocalizedString descMisconfiguredOrganizationRelationship(string organizationRelationship)
		{
			return new LocalizedString("descMisconfiguredOrganizationRelationship", "", false, false, Strings.ResourceManager, new object[]
			{
				organizationRelationship
			});
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x00070087 File Offset: 0x0006E287
		public static LocalizedString SearchNotStarted
		{
			get
			{
				return new LocalizedString("SearchNotStarted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x000700A5 File Offset: 0x0006E2A5
		public static LocalizedString TrackingErrorConnectivity
		{
			get
			{
				return new LocalizedString("TrackingErrorConnectivity", "Ex3D9B9F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x000700C4 File Offset: 0x0006E2C4
		public static LocalizedString descNotAGroupOrUserOrContact(string emailAddress)
		{
			return new LocalizedString("descNotAGroupOrUserOrContact", "Ex2F4000", false, true, Strings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x000700F3 File Offset: 0x0006E2F3
		public static LocalizedString TargetFolder
		{
			get
			{
				return new LocalizedString("TargetFolder", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00070114 File Offset: 0x0006E314
		public static LocalizedString descProxyForPersonalNotAllowed(string recipient)
		{
			return new LocalizedString("descProxyForPersonalNotAllowed", "Ex0FB03A", false, true, Strings.ResourceManager, new object[]
			{
				recipient
			});
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x00070143 File Offset: 0x0006E343
		public static LocalizedString Unsearchable
		{
			get
			{
				return new LocalizedString("Unsearchable", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x00070161 File Offset: 0x0006E361
		public static LocalizedString LogFieldsRecipients
		{
			get
			{
				return new LocalizedString("LogFieldsRecipients", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x0007017F File Offset: 0x0006E37F
		public static LocalizedString InvalidPreviewItemInResultRows
		{
			get
			{
				return new LocalizedString("InvalidPreviewItemInResultRows", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x0007019D File Offset: 0x0006E39D
		public static LocalizedString descInvalidSmptAddres
		{
			get
			{
				return new LocalizedString("descInvalidSmptAddres", "ExD96D91", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x000701BB File Offset: 0x0006E3BB
		public static LocalizedString TrackingErrorBudgetExceeded
		{
			get
			{
				return new LocalizedString("TrackingErrorBudgetExceeded", "ExFCC67B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x000701D9 File Offset: 0x0006E3D9
		public static LocalizedString LogFieldsKeywordMbxs
		{
			get
			{
				return new LocalizedString("LogFieldsKeywordMbxs", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x000701F8 File Offset: 0x0006E3F8
		public static LocalizedString UnknownDayOfWeek(string dayOfWeek)
		{
			return new LocalizedString("UnknownDayOfWeek", "Ex2DDE2A", false, true, Strings.ResourceManager, new object[]
			{
				dayOfWeek
			});
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00070228 File Offset: 0x0006E428
		public static LocalizedString descInvalidMonth(int min, int max)
		{
			return new LocalizedString("descInvalidMonth", "", false, false, Strings.ResourceManager, new object[]
			{
				min,
				max
			});
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00070268 File Offset: 0x0006E468
		public static LocalizedString InvalidKeywordStatsRequest(string mdbGuid, string server)
		{
			return new LocalizedString("InvalidKeywordStatsRequest", "", false, false, Strings.ResourceManager, new object[]
			{
				mdbGuid,
				server
			});
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x0007029B File Offset: 0x0006E49B
		public static LocalizedString descQueryGenerationNotRequired
		{
			get
			{
				return new LocalizedString("descQueryGenerationNotRequired", "ExC90C22", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x000702B9 File Offset: 0x0006E4B9
		public static LocalizedString TrackingNoDefaultDomain
		{
			get
			{
				return new LocalizedString("TrackingNoDefaultDomain", "ExD1A01B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x000702D8 File Offset: 0x0006E4D8
		public static LocalizedString PhotoRetrievalFailedUnauthorizedAccessError(string innerExceptionMessage)
		{
			return new LocalizedString("PhotoRetrievalFailedUnauthorizedAccessError", "", false, false, Strings.ResourceManager, new object[]
			{
				innerExceptionMessage
			});
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00070308 File Offset: 0x0006E508
		public static LocalizedString ArgumentValidationFailedException(string argumentName)
		{
			return new LocalizedString("ArgumentValidationFailedException", "", false, false, Strings.ResourceManager, new object[]
			{
				argumentName
			});
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00070338 File Offset: 0x0006E538
		public static LocalizedString descDeliveryRestricted(string emailAddress)
		{
			return new LocalizedString("descDeliveryRestricted", "ExF083C1", false, true, Strings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00070368 File Offset: 0x0006E568
		public static LocalizedString UnknownRecurrenceRange(string range)
		{
			return new LocalizedString("UnknownRecurrenceRange", "Ex69C13A", false, true, Strings.ResourceManager, new object[]
			{
				range
			});
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x00070397 File Offset: 0x0006E597
		public static LocalizedString InvalidChangeKeyReturned
		{
			get
			{
				return new LocalizedString("InvalidChangeKeyReturned", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000703B8 File Offset: 0x0006E5B8
		public static LocalizedString LogMailHeader(string name, string status)
		{
			return new LocalizedString("LogMailHeader", "", false, false, Strings.ResourceManager, new object[]
			{
				name,
				status
			});
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x000703EB File Offset: 0x0006E5EB
		public static LocalizedString ArchiveMailbox
		{
			get
			{
				return new LocalizedString("ArchiveMailbox", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x00070409 File Offset: 0x0006E609
		public static LocalizedString LogFieldsKeywordHitCount
		{
			get
			{
				return new LocalizedString("LogFieldsKeywordHitCount", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00070428 File Offset: 0x0006E628
		public static LocalizedString descMailTipsSenderNotUnique(string address)
		{
			return new LocalizedString("descMailTipsSenderNotUnique", "Ex462EA0", false, true, Strings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x00070457 File Offset: 0x0006E657
		public static LocalizedString LogFieldsName
		{
			get
			{
				return new LocalizedString("LogFieldsName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x00070475 File Offset: 0x0006E675
		public static LocalizedString LogFieldsStatus
		{
			get
			{
				return new LocalizedString("LogFieldsStatus", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x00070493 File Offset: 0x0006E693
		public static LocalizedString LogFieldsEndDate
		{
			get
			{
				return new LocalizedString("LogFieldsEndDate", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x000704B4 File Offset: 0x0006E6B4
		public static LocalizedString InvalidFailedMailboxesResultDuplicateEntries(string url)
		{
			return new LocalizedString("InvalidFailedMailboxesResultDuplicateEntries", "", false, false, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x000704E4 File Offset: 0x0006E6E4
		public static LocalizedString RbacSourceMailboxAuthorizationError(string name)
		{
			return new LocalizedString("RbacSourceMailboxAuthorizationError", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x00070513 File Offset: 0x0006E713
		public static LocalizedString LogFieldsSenders
		{
			get
			{
				return new LocalizedString("LogFieldsSenders", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x00070531 File Offset: 0x0006E731
		public static LocalizedString TrackingErrorTimeBudgetExceeded
		{
			get
			{
				return new LocalizedString("TrackingErrorTimeBudgetExceeded", "Ex53FB82", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x0007054F File Offset: 0x0006E74F
		public static LocalizedString TrackingLogVersionIncompatible
		{
			get
			{
				return new LocalizedString("TrackingLogVersionIncompatible", "Ex0A6A33", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00070570 File Offset: 0x0006E770
		public static LocalizedString descUnknownDefFolder(string adFolder, string mailbox)
		{
			return new LocalizedString("descUnknownDefFolder", "Ex40A559", false, true, Strings.ResourceManager, new object[]
			{
				adFolder,
				mailbox
			});
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x000705A3 File Offset: 0x0006E7A3
		public static LocalizedString AutodiscoverFailedException
		{
			get
			{
				return new LocalizedString("AutodiscoverFailedException", "ExB63563", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x000705C1 File Offset: 0x0006E7C1
		public static LocalizedString descInvalidMaxNonWorkHourResultsPerDay
		{
			get
			{
				return new LocalizedString("descInvalidMaxNonWorkHourResultsPerDay", "Ex1A7FF0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x000705DF File Offset: 0x0006E7DF
		public static LocalizedString descNullAutoDiscoverResponse
		{
			get
			{
				return new LocalizedString("descNullAutoDiscoverResponse", "Ex182BA2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x000705FD File Offset: 0x0006E7FD
		public static LocalizedString UnexpectedRemoteDataException
		{
			get
			{
				return new LocalizedString("UnexpectedRemoteDataException", "ExA312C1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x0007061B File Offset: 0x0006E81B
		public static LocalizedString LogFieldsIncludeKeywordStatistics
		{
			get
			{
				return new LocalizedString("LogFieldsIncludeKeywordStatistics", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x00070639 File Offset: 0x0006E839
		public static LocalizedString UnexpectedError
		{
			get
			{
				return new LocalizedString("UnexpectedError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x00070657 File Offset: 0x0006E857
		public static LocalizedString descInvalidMaximumResults
		{
			get
			{
				return new LocalizedString("descInvalidMaximumResults", "ExD7AB2B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x00070675 File Offset: 0x0006E875
		public static LocalizedString descOOFCannotReadOofConfigData
		{
			get
			{
				return new LocalizedString("descOOFCannotReadOofConfigData", "ExFFEE20", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00070694 File Offset: 0x0006E894
		public static LocalizedString descProxyRequestProcessingError(string exceptionMessage, string currentRequestString)
		{
			return new LocalizedString("descProxyRequestProcessingError", "ExCEEA5B", false, true, Strings.ResourceManager, new object[]
			{
				exceptionMessage,
				currentRequestString
			});
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x000706C7 File Offset: 0x0006E8C7
		public static LocalizedString descInvalidSecurityDescriptor
		{
			get
			{
				return new LocalizedString("descInvalidSecurityDescriptor", "ExE95D66", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x000706E5 File Offset: 0x0006E8E5
		public static LocalizedString InvalidResultMerge
		{
			get
			{
				return new LocalizedString("InvalidResultMerge", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x00070703 File Offset: 0x0006E903
		public static LocalizedString SearchObjectNotFound
		{
			get
			{
				return new LocalizedString("SearchObjectNotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00070724 File Offset: 0x0006E924
		public static LocalizedString descSoapAutoDiscoverRequestUserSettingError(string url, string settingName, string errorMessage)
		{
			return new LocalizedString("descSoapAutoDiscoverRequestUserSettingError", "ExE23D0D", false, true, Strings.ResourceManager, new object[]
			{
				url,
				settingName,
				errorMessage
			});
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x0007075B File Offset: 0x0006E95B
		public static LocalizedString TrackingInstanceBudgetExceeded
		{
			get
			{
				return new LocalizedString("TrackingInstanceBudgetExceeded", "Ex2C68C2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0007077C File Offset: 0x0006E97C
		public static LocalizedString descFailedToCreateELCRoot(string mailboxName)
		{
			return new LocalizedString("descFailedToCreateELCRoot", "ExF5B44D", false, true, Strings.ResourceManager, new object[]
			{
				mailboxName
			});
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x000707AB File Offset: 0x0006E9AB
		public static LocalizedString MessageInvalidTimeZoneOutOfRange
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneOutOfRange", "Ex65F4C5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x000707C9 File Offset: 0x0006E9C9
		public static LocalizedString LogFieldsStartDate
		{
			get
			{
				return new LocalizedString("LogFieldsStartDate", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x000707E7 File Offset: 0x0006E9E7
		public static LocalizedString NotOperator
		{
			get
			{
				return new LocalizedString("NotOperator", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00070808 File Offset: 0x0006EA08
		public static LocalizedString descElcFolderExists(string folderName)
		{
			return new LocalizedString("descElcFolderExists", "ExB3CC72", false, true, Strings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00070838 File Offset: 0x0006EA38
		public static LocalizedString descFailedToCreateOneOrMoreOrganizationalFolders(string message)
		{
			return new LocalizedString("descFailedToCreateOneOrMoreOrganizationalFolders", "Ex9A45E4", false, true, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x00070867 File Offset: 0x0006EA67
		public static LocalizedString LogFieldsSourceRecipients
		{
			get
			{
				return new LocalizedString("LogFieldsSourceRecipients", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x00070885 File Offset: 0x0006EA85
		public static LocalizedString TrackingErrorQueueViewerRpc
		{
			get
			{
				return new LocalizedString("TrackingErrorQueueViewerRpc", "Ex44F6D9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x000708A4 File Offset: 0x0006EAA4
		public static LocalizedString SearchServerError(int errorCode)
		{
			return new LocalizedString("SearchServerError", "", false, false, Strings.ResourceManager, new object[]
			{
				errorCode
			});
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x000708D8 File Offset: 0x0006EAD8
		public static LocalizedString InvalidItemHashInPreviewResult(string url)
		{
			return new LocalizedString("InvalidItemHashInPreviewResult", "", false, false, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00070908 File Offset: 0x0006EB08
		public static LocalizedString descFailedToSyncFolder(string folderName, string mailboxName)
		{
			return new LocalizedString("descFailedToSyncFolder", "ExCF9A93", false, true, Strings.ResourceManager, new object[]
			{
				folderName,
				mailboxName
			});
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x0007093B File Offset: 0x0006EB3B
		public static LocalizedString SearchLogHeader
		{
			get
			{
				return new LocalizedString("SearchLogHeader", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x00070959 File Offset: 0x0006EB59
		public static LocalizedString MessageInvalidTimeZoneInvalidOffsetFormat
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneInvalidOffsetFormat", "Ex0D4D83", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x00070977 File Offset: 0x0006EB77
		public static LocalizedString descInvalidCredentials
		{
			get
			{
				return new LocalizedString("descInvalidCredentials", "Ex4A709C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x00070995 File Offset: 0x0006EB95
		public static LocalizedString LogFieldsSearchOperation
		{
			get
			{
				return new LocalizedString("LogFieldsSearchOperation", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x000709B4 File Offset: 0x0006EBB4
		public static LocalizedString MailboxRefinerInRefinersSection(string url)
		{
			return new LocalizedString("MailboxRefinerInRefinersSection", "", false, false, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x000709E4 File Offset: 0x0006EBE4
		public static LocalizedString descRequestStreamTooBig(string allowedSize, string actualSize)
		{
			return new LocalizedString("descRequestStreamTooBig", "Ex029E9A", false, true, Strings.ResourceManager, new object[]
			{
				allowedSize,
				actualSize
			});
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x00070A17 File Offset: 0x0006EC17
		public static LocalizedString MessageInvalidTimeZoneNonFirstTransition
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneNonFirstTransition", "Ex70E8B3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x00070A35 File Offset: 0x0006EC35
		public static LocalizedString descMeetingSuggestionsDurationTooSmall
		{
			get
			{
				return new LocalizedString("descMeetingSuggestionsDurationTooSmall", "Ex6BFC3C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00070A54 File Offset: 0x0006EC54
		public static LocalizedString descProtocolNotFoundInAutoDiscoverResponse(string protocol, string response)
		{
			return new LocalizedString("descProtocolNotFoundInAutoDiscoverResponse", "ExD0B00D", false, true, Strings.ResourceManager, new object[]
			{
				protocol,
				response
			});
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x00070A87 File Offset: 0x0006EC87
		public static LocalizedString DeleteItemsFailed
		{
			get
			{
				return new LocalizedString("DeleteItemsFailed", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00070AA8 File Offset: 0x0006ECA8
		public static LocalizedString UnableToFindSearchObject(string id)
		{
			return new LocalizedString("UnableToFindSearchObject", "", false, false, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x00070AD7 File Offset: 0x0006ECD7
		public static LocalizedString PendingSynchronizationException
		{
			get
			{
				return new LocalizedString("PendingSynchronizationException", "Ex36C2F8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x00070AF5 File Offset: 0x0006ECF5
		public static LocalizedString LogFieldsNumberMailboxesToSearch
		{
			get
			{
				return new LocalizedString("LogFieldsNumberMailboxesToSearch", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00070B14 File Offset: 0x0006ED14
		public static LocalizedString descMinimumRequiredVersionProxyServerNotFound(int currentVersion, int requiredVersion, string email)
		{
			return new LocalizedString("descMinimumRequiredVersionProxyServerNotFound", "ExBF1341", false, true, Strings.ResourceManager, new object[]
			{
				currentVersion,
				requiredVersion,
				email
			});
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00070B58 File Offset: 0x0006ED58
		public static LocalizedString descIndividualMailboxLimitReached(string emailAddress, int mailboxCount)
		{
			return new LocalizedString("descIndividualMailboxLimitReached", "Ex396E68", false, true, Strings.ResourceManager, new object[]
			{
				emailAddress,
				mailboxCount
			});
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00070B90 File Offset: 0x0006ED90
		public static LocalizedString descTagNotInAD(string tagDN)
		{
			return new LocalizedString("descTagNotInAD", "ExCE2883", false, true, Strings.ResourceManager, new object[]
			{
				tagDN
			});
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00070BC0 File Offset: 0x0006EDC0
		public static LocalizedString descAvailabilityAddressSpaceFailed(string id)
		{
			return new LocalizedString("descAvailabilityAddressSpaceFailed", "Ex481C63", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00070BF0 File Offset: 0x0006EDF0
		public static LocalizedString descUnsupportedSecurityDescriptorVersion(string mailboxAddress, string expectedVersion, string actualVersion)
		{
			return new LocalizedString("descUnsupportedSecurityDescriptorVersion", "ExF83A8E", false, true, Strings.ResourceManager, new object[]
			{
				mailboxAddress,
				expectedVersion,
				actualVersion
			});
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00070C28 File Offset: 0x0006EE28
		public static LocalizedString SearchMailboxNotFound(string mailboxGuid, string databaseName, string server)
		{
			return new LocalizedString("SearchMailboxNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				mailboxGuid,
				databaseName,
				server
			});
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00070C60 File Offset: 0x0006EE60
		public static LocalizedString SearchTaskCancelledPrimary(string displayName, string mailboxGuid)
		{
			return new LocalizedString("SearchTaskCancelledPrimary", "", false, false, Strings.ResourceManager, new object[]
			{
				displayName,
				mailboxGuid
			});
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00070C94 File Offset: 0x0006EE94
		public static LocalizedString descCannotResolveEmailAddress(string emailAddress)
		{
			return new LocalizedString("descCannotResolveEmailAddress", "ExB6A32C", false, true, Strings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x00070CC3 File Offset: 0x0006EEC3
		public static LocalizedString LogFieldsKeywordKeyword
		{
			get
			{
				return new LocalizedString("LogFieldsKeywordKeyword", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x00070CE1 File Offset: 0x0006EEE1
		public static LocalizedString ObjectNotFound
		{
			get
			{
				return new LocalizedString("ObjectNotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00070D00 File Offset: 0x0006EF00
		public static LocalizedString descMissingProperty(string propertyName, string unexpectedObject)
		{
			return new LocalizedString("descMissingProperty", "ExD328B9", false, true, Strings.ResourceManager, new object[]
			{
				propertyName,
				unexpectedObject
			});
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x00070D33 File Offset: 0x0006EF33
		public static LocalizedString MessageInvalidTimeZoneMissedPeriod
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneMissedPeriod", "ExF09C89", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x00070D51 File Offset: 0x0006EF51
		public static LocalizedString LogFieldsResultSize
		{
			get
			{
				return new LocalizedString("LogFieldsResultSize", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00070D70 File Offset: 0x0006EF70
		public static LocalizedString descAutoDiscoverFailed(string email)
		{
			return new LocalizedString("descAutoDiscoverFailed", "Ex28E399", false, true, Strings.ResourceManager, new object[]
			{
				email
			});
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00070DA0 File Offset: 0x0006EFA0
		public static LocalizedString UnabledToLocateMailboxServer(string server)
		{
			return new LocalizedString("UnabledToLocateMailboxServer", "Ex15DAE8", false, true, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00070DD0 File Offset: 0x0006EFD0
		public static LocalizedString ArchiveSearchPopulationFailed(string displayName, string mailboxGuid)
		{
			return new LocalizedString("ArchiveSearchPopulationFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				displayName,
				mailboxGuid
			});
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x00070E03 File Offset: 0x0006F003
		public static LocalizedString MessageInvalidTimeZoneDayOfWeekValue
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneDayOfWeekValue", "ExD43B58", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00070E24 File Offset: 0x0006F024
		public static LocalizedString InvalidSearchRequest(string mdbGuid, string server)
		{
			return new LocalizedString("InvalidSearchRequest", "", false, false, Strings.ResourceManager, new object[]
			{
				mdbGuid,
				server
			});
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00070E58 File Offset: 0x0006F058
		public static LocalizedString DiscoverySearchCIFailure(string mdbGuid, string server, int errorCode)
		{
			return new LocalizedString("DiscoverySearchCIFailure", "", false, false, Strings.ResourceManager, new object[]
			{
				mdbGuid,
				server,
				errorCode
			});
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00070E94 File Offset: 0x0006F094
		public static LocalizedString descInvalidTargetAddress(string mailbox)
		{
			return new LocalizedString("descInvalidTargetAddress", "Ex11472D", false, true, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00070EC3 File Offset: 0x0006F0C3
		public static LocalizedString TrackingBusy
		{
			get
			{
				return new LocalizedString("TrackingBusy", "Ex886638", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00070EE4 File Offset: 0x0006F0E4
		public static LocalizedString descCannotBindToElcRootFolder(string rootFolderId, string mailboxName)
		{
			return new LocalizedString("descCannotBindToElcRootFolder", "Ex77513F", false, true, Strings.ResourceManager, new object[]
			{
				rootFolderId,
				mailboxName
			});
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00070F18 File Offset: 0x0006F118
		public static LocalizedString descSoapAutoDiscoverRequestError(string url, string exceptionMessage)
		{
			return new LocalizedString("descSoapAutoDiscoverRequestError", "Ex4A38DB", false, true, Strings.ResourceManager, new object[]
			{
				url,
				exceptionMessage
			});
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00070F4C File Offset: 0x0006F14C
		public static LocalizedString descAutoDiscoverRequestError(string exceptionMessage, string currentRequestString)
		{
			return new LocalizedString("descAutoDiscoverRequestError", "ExC47A40", false, true, Strings.ResourceManager, new object[]
			{
				exceptionMessage,
				currentRequestString
			});
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x00070F7F File Offset: 0x0006F17F
		public static LocalizedString InvalidAppointmentException
		{
			get
			{
				return new LocalizedString("InvalidAppointmentException", "Ex8CC700", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00070FA0 File Offset: 0x0006F1A0
		public static LocalizedString SearchOverBudget(int maximumSearches)
		{
			return new LocalizedString("SearchOverBudget", "", false, false, Strings.ResourceManager, new object[]
			{
				maximumSearches
			});
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00070FD4 File Offset: 0x0006F1D4
		public static LocalizedString descUnknownWebRequestType(string request)
		{
			return new LocalizedString("descUnknownWebRequestType", "Ex622664", false, true, Strings.ResourceManager, new object[]
			{
				request
			});
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x00071003 File Offset: 0x0006F203
		public static LocalizedString descInvalidNetworkServiceContext
		{
			get
			{
				return new LocalizedString("descInvalidNetworkServiceContext", "ExB0EAC3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00071024 File Offset: 0x0006F224
		public static LocalizedString descIdentityArrayTooBig(string allowedSize, string actualSize)
		{
			return new LocalizedString("descIdentityArrayTooBig", "Ex74173A", false, true, Strings.ResourceManager, new object[]
			{
				allowedSize,
				actualSize
			});
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x00071057 File Offset: 0x0006F257
		public static LocalizedString ResultsNotDeduped
		{
			get
			{
				return new LocalizedString("ResultsNotDeduped", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00071075 File Offset: 0x0006F275
		public static LocalizedString ErrorTimeZone
		{
			get
			{
				return new LocalizedString("ErrorTimeZone", "Ex53B2E7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00071094 File Offset: 0x0006F294
		public static LocalizedString TrackingLogsCorruptOnServer(string server)
		{
			return new LocalizedString("TrackingLogsCorruptOnServer", "ExC58339", false, true, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x000710C3 File Offset: 0x0006F2C3
		public static LocalizedString BatchSynchronizationFailedException
		{
			get
			{
				return new LocalizedString("BatchSynchronizationFailedException", "ExDA1698", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x000710E1 File Offset: 0x0006F2E1
		public static LocalizedString descStartAndEndTimesOutSideFreeBusyData
		{
			get
			{
				return new LocalizedString("descStartAndEndTimesOutSideFreeBusyData", "Ex7F4DE6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x000710FF File Offset: 0x0006F2FF
		public static LocalizedString descWin32InteropError
		{
			get
			{
				return new LocalizedString("descWin32InteropError", "Ex252861", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00071120 File Offset: 0x0006F320
		public static LocalizedString descExceededMaxRedirectionDepth(string email, int maxDepth)
		{
			return new LocalizedString("descExceededMaxRedirectionDepth", "Ex89C252", false, true, Strings.ResourceManager, new object[]
			{
				email,
				maxDepth
			});
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00071158 File Offset: 0x0006F358
		public static LocalizedString SearchNonFullTextSortByProperty(string sortByProperty)
		{
			return new LocalizedString("SearchNonFullTextSortByProperty", "", false, false, Strings.ResourceManager, new object[]
			{
				sortByProperty
			});
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x00071187 File Offset: 0x0006F387
		public static LocalizedString TrackingErrorLegacySender
		{
			get
			{
				return new LocalizedString("TrackingErrorLegacySender", "Ex19374A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000711A8 File Offset: 0x0006F3A8
		public static LocalizedString descCrossForestServiceMissing(string mailbox)
		{
			return new LocalizedString("descCrossForestServiceMissing", "Ex86F5F2", false, true, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x000711D7 File Offset: 0x0006F3D7
		public static LocalizedString descNoEwsResponse
		{
			get
			{
				return new LocalizedString("descNoEwsResponse", "Ex00AF6A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x000711F8 File Offset: 0x0006F3F8
		public static LocalizedString CrossServerCallFailed(string errorHint, string errorCode, string errorMessage)
		{
			return new LocalizedString("CrossServerCallFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				errorHint,
				errorCode,
				errorMessage
			});
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00071230 File Offset: 0x0006F430
		public static LocalizedString PrimarySearchFolderTimeout(string displayName, string mailboxGuid)
		{
			return new LocalizedString("PrimarySearchFolderTimeout", "", false, false, Strings.ResourceManager, new object[]
			{
				displayName,
				mailboxGuid
			});
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x00071263 File Offset: 0x0006F463
		public static LocalizedString LogFieldsResultsLink
		{
			get
			{
				return new LocalizedString("LogFieldsResultsLink", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00071284 File Offset: 0x0006F484
		public static LocalizedString TrackingErrorUserObjectCorrupt(string user, string attribute)
		{
			return new LocalizedString("TrackingErrorUserObjectCorrupt", "ExB2C799", false, true, Strings.ResourceManager, new object[]
			{
				user,
				attribute
			});
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x000712B8 File Offset: 0x0006F4B8
		public static LocalizedString descVirusDetected(string emailAddress)
		{
			return new LocalizedString("descVirusDetected", "ExD6DC3B", false, true, Strings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x000712E8 File Offset: 0x0006F4E8
		public static LocalizedString FailedToConvertSearchCriteriaToRestriction(string query, string database, string failure)
		{
			return new LocalizedString("FailedToConvertSearchCriteriaToRestriction", "", false, false, Strings.ResourceManager, new object[]
			{
				query,
				database,
				failure
			});
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00071320 File Offset: 0x0006F520
		public static LocalizedString PrimarySearchPopulationFailed(string displayName, string mailboxGuid)
		{
			return new LocalizedString("PrimarySearchPopulationFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				displayName,
				mailboxGuid
			});
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x00071353 File Offset: 0x0006F553
		public static LocalizedString TrackingErrorLegacySenderMultiMessageSearch
		{
			get
			{
				return new LocalizedString("TrackingErrorLegacySenderMultiMessageSearch", "ExAE0B5D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x00071371 File Offset: 0x0006F571
		public static LocalizedString LogFieldsManagedBy
		{
			get
			{
				return new LocalizedString("LogFieldsManagedBy", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0007138F File Offset: 0x0006F58F
		public static LocalizedString KeywordHitEmptyQuery
		{
			get
			{
				return new LocalizedString("KeywordHitEmptyQuery", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x000713B0 File Offset: 0x0006F5B0
		public static LocalizedString KeywordStatisticsSearchDisabled(int maximumMailboxes)
		{
			return new LocalizedString("KeywordStatisticsSearchDisabled", "", false, false, Strings.ResourceManager, new object[]
			{
				maximumMailboxes
			});
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x000713E4 File Offset: 0x0006F5E4
		public static LocalizedString descInvalidTypeInBookingPolicyConfig(string type, string parameter)
		{
			return new LocalizedString("descInvalidTypeInBookingPolicyConfig", "Ex4B544F", false, true, Strings.ResourceManager, new object[]
			{
				type,
				parameter
			});
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00071417 File Offset: 0x0006F617
		public static LocalizedString SearchQueryEmpty
		{
			get
			{
				return new LocalizedString("SearchQueryEmpty", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x00071435 File Offset: 0x0006F635
		public static LocalizedString ServerShutdown
		{
			get
			{
				return new LocalizedString("ServerShutdown", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x00071453 File Offset: 0x0006F653
		public static LocalizedString TrackingErrorSuffixForAdministrator
		{
			get
			{
				return new LocalizedString("TrackingErrorSuffixForAdministrator", "Ex98DD8C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x00071471 File Offset: 0x0006F671
		public static LocalizedString SubscriptionNotFoundException
		{
			get
			{
				return new LocalizedString("SubscriptionNotFoundException", "Ex0935AF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00071490 File Offset: 0x0006F690
		public static LocalizedString EmptyMailboxStatsServerResponse(string url)
		{
			return new LocalizedString("EmptyMailboxStatsServerResponse", "", false, false, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x000714C0 File Offset: 0x0006F6C0
		public static LocalizedString PreviewSearchDisabled(int maxPreviewMailboxes, int maxResultsOnlyMailboxes)
		{
			return new LocalizedString("PreviewSearchDisabled", "", false, false, Strings.ResourceManager, new object[]
			{
				maxPreviewMailboxes,
				maxResultsOnlyMailboxes
			});
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x000714FD File Offset: 0x0006F6FD
		public static LocalizedString LogFieldsResultSizeEstimate
		{
			get
			{
				return new LocalizedString("LogFieldsResultSizeEstimate", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0007151B File Offset: 0x0006F71B
		public static LocalizedString descNullDateInChangeDate
		{
			get
			{
				return new LocalizedString("descNullDateInChangeDate", "Ex3632C8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00071539 File Offset: 0x0006F739
		public static LocalizedString SharingFolderNotFoundException
		{
			get
			{
				return new LocalizedString("SharingFolderNotFoundException", "ExAD861F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00071558 File Offset: 0x0006F758
		public static LocalizedString descFailedToCreateOrganizationalFolder(string folderName, string mailboxName)
		{
			return new LocalizedString("descFailedToCreateOrganizationalFolder", "Ex1F3461", false, true, Strings.ResourceManager, new object[]
			{
				folderName,
				mailboxName
			});
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x0007158B File Offset: 0x0006F78B
		public static LocalizedString LogMailAll
		{
			get
			{
				return new LocalizedString("LogMailAll", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x000715AC File Offset: 0x0006F7AC
		public static LocalizedString ArchiveSearchFolderTimeout(string displayName, string mailboxGuid)
		{
			return new LocalizedString("ArchiveSearchFolderTimeout", "", false, false, Strings.ResourceManager, new object[]
			{
				displayName,
				mailboxGuid
			});
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x000715DF File Offset: 0x0006F7DF
		public static LocalizedString descInvalidScheduledOofDuration
		{
			get
			{
				return new LocalizedString("descInvalidScheduledOofDuration", "ExCC478D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00071600 File Offset: 0x0006F800
		public static LocalizedString descSoapAutoDiscoverRequestUserSettingInvalidError(string url, string settingName)
		{
			return new LocalizedString("descSoapAutoDiscoverRequestUserSettingInvalidError", "ExB106D1", false, true, Strings.ResourceManager, new object[]
			{
				url,
				settingName
			});
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00071634 File Offset: 0x0006F834
		public static LocalizedString descInvalidSmtpAddress(string emailAddress)
		{
			return new LocalizedString("descInvalidSmtpAddress", "Ex2B6F09", false, true, Strings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x00071663 File Offset: 0x0006F863
		public static LocalizedString TrackingTransientErrorMultiMessageSearch
		{
			get
			{
				return new LocalizedString("TrackingTransientErrorMultiMessageSearch", "Ex693ED5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x00071681 File Offset: 0x0006F881
		public static LocalizedString AqsParserError
		{
			get
			{
				return new LocalizedString("AqsParserError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x0007169F File Offset: 0x0006F89F
		public static LocalizedString LegacyOofMessage
		{
			get
			{
				return new LocalizedString("LegacyOofMessage", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x000716BD File Offset: 0x0006F8BD
		public static LocalizedString descInvalidClientSecurityContext
		{
			get
			{
				return new LocalizedString("descInvalidClientSecurityContext", "ExEDC8FE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x000716DB File Offset: 0x0006F8DB
		public static LocalizedString LogMailFooter
		{
			get
			{
				return new LocalizedString("LogMailFooter", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x000716F9 File Offset: 0x0006F8F9
		public static LocalizedString descInvalidFormatInEmail
		{
			get
			{
				return new LocalizedString("descInvalidFormatInEmail", "ExEE9BA0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x00071717 File Offset: 0x0006F917
		public static LocalizedString descNoCalendar
		{
			get
			{
				return new LocalizedString("descNoCalendar", "Ex85D1EA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00071738 File Offset: 0x0006F938
		public static LocalizedString DiscoverySearchAborted(string queryCorrelationId, string mdbGuid, string server)
		{
			return new LocalizedString("DiscoverySearchAborted", "", false, false, Strings.ResourceManager, new object[]
			{
				queryCorrelationId,
				mdbGuid,
				server
			});
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x0007176F File Offset: 0x0006F96F
		public static LocalizedString UnexpectedUserResponses
		{
			get
			{
				return new LocalizedString("UnexpectedUserResponses", "Ex3F8449", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x0007178D File Offset: 0x0006F98D
		public static LocalizedString LogFieldsIdentity
		{
			get
			{
				return new LocalizedString("LogFieldsIdentity", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x000717AB File Offset: 0x0006F9AB
		public static LocalizedString FreeBusyApplicationName
		{
			get
			{
				return new LocalizedString("FreeBusyApplicationName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000717CC File Offset: 0x0006F9CC
		public static LocalizedString descDateMustHaveZeroTimeSpan(string argument)
		{
			return new LocalizedString("descDateMustHaveZeroTimeSpan", "Ex8C6BED", false, true, Strings.ResourceManager, new object[]
			{
				argument
			});
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x000717FB File Offset: 0x0006F9FB
		public static LocalizedString SearchAborted
		{
			get
			{
				return new LocalizedString("SearchAborted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0007181C File Offset: 0x0006FA1C
		public static LocalizedString SearchServerErrorMessage(string message)
		{
			return new LocalizedString("SearchServerErrorMessage", "", false, false, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0007184C File Offset: 0x0006FA4C
		public static LocalizedString SearchInvalidSortSpecification(string sortByProperty)
		{
			return new LocalizedString("SearchInvalidSortSpecification", "", false, false, Strings.ResourceManager, new object[]
			{
				sortByProperty
			});
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0007187C File Offset: 0x0006FA7C
		public static LocalizedString InvalidRecipientArrayInPreviewResult(string url)
		{
			return new LocalizedString("InvalidRecipientArrayInPreviewResult", "", false, false, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x000718AC File Offset: 0x0006FAAC
		public static LocalizedString SearchTooManyMailboxes(int search)
		{
			return new LocalizedString("SearchTooManyMailboxes", "", false, false, Strings.ResourceManager, new object[]
			{
				search
			});
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x000718E0 File Offset: 0x0006FAE0
		public static LocalizedString InvalidPreviewSearchResults(string url)
		{
			return new LocalizedString("InvalidPreviewSearchResults", "", false, false, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x0007190F File Offset: 0x0006FB0F
		public static LocalizedString AutodiscoverTimedOut
		{
			get
			{
				return new LocalizedString("AutodiscoverTimedOut", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x0007192D File Offset: 0x0006FB2D
		public static LocalizedString LogFieldsSearchDumpster
		{
			get
			{
				return new LocalizedString("LogFieldsSearchDumpster", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0007194C File Offset: 0x0006FB4C
		public static LocalizedString descRecipientVersionNotSupported(string email, long version, long minimumVersion)
		{
			return new LocalizedString("descRecipientVersionNotSupported", "ExEB0F33", false, true, Strings.ResourceManager, new object[]
			{
				email,
				version,
				minimumVersion
			});
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00071990 File Offset: 0x0006FB90
		public static LocalizedString SearchTaskTimeoutArchive(string displayName, string mailboxGuid)
		{
			return new LocalizedString("SearchTaskTimeoutArchive", "", false, false, Strings.ResourceManager, new object[]
			{
				displayName,
				mailboxGuid
			});
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x000719C3 File Offset: 0x0006FBC3
		public static LocalizedString descInvalidAuthorizationContext
		{
			get
			{
				return new LocalizedString("descInvalidAuthorizationContext", "Ex9B5A48", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x000719E4 File Offset: 0x0006FBE4
		public static LocalizedString descInvalidGoodThreshold(int startValue, int endValue)
		{
			return new LocalizedString("descInvalidGoodThreshold", "Ex2D689F", false, true, Strings.ResourceManager, new object[]
			{
				startValue,
				endValue
			});
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x00071A21 File Offset: 0x0006FC21
		public static LocalizedString LogFieldsMessageTypes
		{
			get
			{
				return new LocalizedString("LogFieldsMessageTypes", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x00071A3F File Offset: 0x0006FC3F
		public static LocalizedString PositiveParameter
		{
			get
			{
				return new LocalizedString("PositiveParameter", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00071A60 File Offset: 0x0006FC60
		public static LocalizedString descNotAValidExchangePrincipal(string emailAddress)
		{
			return new LocalizedString("descNotAValidExchangePrincipal", "Ex13EC65", false, true, Strings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x00071A8F File Offset: 0x0006FC8F
		public static LocalizedString LogMailSeeAttachment
		{
			get
			{
				return new LocalizedString("LogMailSeeAttachment", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x00071AAD File Offset: 0x0006FCAD
		public static LocalizedString descMeetingSuggestionsInvalidTimeInterval
		{
			get
			{
				return new LocalizedString("descMeetingSuggestionsInvalidTimeInterval", "Ex97292C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x00071ACB File Offset: 0x0006FCCB
		public static LocalizedString KeywordStatsNotRequested
		{
			get
			{
				return new LocalizedString("KeywordStatsNotRequested", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00071AEC File Offset: 0x0006FCEC
		public static LocalizedString descMisconfiguredIntraOrganizationConnector(string intraOrganizationConnector)
		{
			return new LocalizedString("descMisconfiguredIntraOrganizationConnector", "", false, false, Strings.ResourceManager, new object[]
			{
				intraOrganizationConnector
			});
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00071B1C File Offset: 0x0006FD1C
		public static LocalizedString descSoapAutoDiscoverInvalidResponseError(string url)
		{
			return new LocalizedString("descSoapAutoDiscoverInvalidResponseError", "Ex40B39B", false, true, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x00071B4B File Offset: 0x0006FD4B
		public static LocalizedString RbacTargetMailboxAuthorizationError
		{
			get
			{
				return new LocalizedString("RbacTargetMailboxAuthorizationError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x00071B69 File Offset: 0x0006FD69
		public static LocalizedString SortedResultNullParameters
		{
			get
			{
				return new LocalizedString("SortedResultNullParameters", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00071B88 File Offset: 0x0006FD88
		public static LocalizedString EmptyRefinerServerResponse(string url)
		{
			return new LocalizedString("EmptyRefinerServerResponse", "", false, false, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00071BB8 File Offset: 0x0006FDB8
		public static LocalizedString descProxyNoResultError(string recipient, string source)
		{
			return new LocalizedString("descProxyNoResultError", "Ex779AFF", false, true, Strings.ResourceManager, new object[]
			{
				recipient,
				source
			});
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x00071BEB File Offset: 0x0006FDEB
		public static LocalizedString LogFieldsLastEndTime
		{
			get
			{
				return new LocalizedString("LogFieldsLastEndTime", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00071C0C File Offset: 0x0006FE0C
		public static LocalizedString UnknownRecurrencePattern(string patternName)
		{
			return new LocalizedString("UnknownRecurrencePattern", "Ex8DB95F", false, true, Strings.ResourceManager, new object[]
			{
				patternName
			});
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x00071C3B File Offset: 0x0006FE3B
		public static LocalizedString TrackingErrorInvalidADData
		{
			get
			{
				return new LocalizedString("TrackingErrorInvalidADData", "Ex4DDA29", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00071C5C File Offset: 0x0006FE5C
		public static LocalizedString descEmptySecurityDescriptor(string mailboxAddress)
		{
			return new LocalizedString("descEmptySecurityDescriptor", "ExC95235", false, true, Strings.ResourceManager, new object[]
			{
				mailboxAddress
			});
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00071C8C File Offset: 0x0006FE8C
		public static LocalizedString CreateFolderFailed(string name)
		{
			return new LocalizedString("CreateFolderFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x00071CBB File Offset: 0x0006FEBB
		public static LocalizedString LogFieldsResume
		{
			get
			{
				return new LocalizedString("LogFieldsResume", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x00071CD9 File Offset: 0x0006FED9
		public static LocalizedString descWorkHoursStartEndInvalid
		{
			get
			{
				return new LocalizedString("descWorkHoursStartEndInvalid", "ExB5214F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x00071CF7 File Offset: 0x0006FEF7
		public static LocalizedString SearchAlreadStarted
		{
			get
			{
				return new LocalizedString("SearchAlreadStarted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00071D18 File Offset: 0x0006FF18
		public static LocalizedString SearchWorkerError(string mailbox, string message)
		{
			return new LocalizedString("SearchWorkerError", "", false, false, Strings.ResourceManager, new object[]
			{
				mailbox,
				message
			});
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00071D4C File Offset: 0x0006FF4C
		public static LocalizedString descAutoDiscoverFailedWithException(string email, string exception)
		{
			return new LocalizedString("descAutoDiscoverFailedWithException", "ExA21E87", false, true, Strings.ResourceManager, new object[]
			{
				email,
				exception
			});
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x00071D7F File Offset: 0x0006FF7F
		public static LocalizedString MessageInvalidTimeZoneReferenceToPeriod
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneReferenceToPeriod", "ExCEC857", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00071D9D File Offset: 0x0006FF9D
		public static LocalizedString ProgressSearchingInProgress
		{
			get
			{
				return new LocalizedString("ProgressSearchingInProgress", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x00071DBB File Offset: 0x0006FFBB
		public static LocalizedString descCorruptUserOofSettingsXmlDocument
		{
			get
			{
				return new LocalizedString("descCorruptUserOofSettingsXmlDocument", "Ex63FB06", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00071DD9 File Offset: 0x0006FFD9
		public static LocalizedString CrossForestNotSupported
		{
			get
			{
				return new LocalizedString("CrossForestNotSupported", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00071DF8 File Offset: 0x0006FFF8
		public static LocalizedString SearchTransientError(string searchType, string message)
		{
			return new LocalizedString("SearchTransientError", "", false, false, Strings.ResourceManager, new object[]
			{
				searchType,
				message
			});
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00071E2C File Offset: 0x0007002C
		public static LocalizedString descTimeIntervalTooBig(string propertyName, string allowedTimeSpanInDays, string actualTimeSpanInDays)
		{
			return new LocalizedString("descTimeIntervalTooBig", "ExC408D3", false, true, Strings.ResourceManager, new object[]
			{
				propertyName,
				allowedTimeSpanInDays,
				actualTimeSpanInDays
			});
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00071E64 File Offset: 0x00070064
		public static LocalizedString descMissingIntraforestCAS(string mailbox)
		{
			return new LocalizedString("descMissingIntraforestCAS", "Ex7D1A6C", false, true, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x00071E93 File Offset: 0x00070093
		public static LocalizedString LogFieldsNumberSuccessfulMailboxes
		{
			get
			{
				return new LocalizedString("LogFieldsNumberSuccessfulMailboxes", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x00071EB1 File Offset: 0x000700B1
		public static LocalizedString descIdentityArrayEmpty
		{
			get
			{
				return new LocalizedString("descIdentityArrayEmpty", "ExA95C53", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00071ED0 File Offset: 0x000700D0
		public static LocalizedString InvalidFailedMailboxesResultWebServiceResponse(string url)
		{
			return new LocalizedString("InvalidFailedMailboxesResultWebServiceResponse", "", false, false, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00071F00 File Offset: 0x00070100
		public static LocalizedString SearchAdminRpcInvalidQuery(string searchType, string query)
		{
			return new LocalizedString("SearchAdminRpcInvalidQuery", "", false, false, Strings.ResourceManager, new object[]
			{
				searchType,
				query
			});
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x00071F33 File Offset: 0x00070133
		public static LocalizedString MessageInvalidTimeZoneReferenceToGroup
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneReferenceToGroup", "Ex0C86D3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x00071F51 File Offset: 0x00070151
		public static LocalizedString descInvalidTimeZoneBias
		{
			get
			{
				return new LocalizedString("descInvalidTimeZoneBias", "ExB64900", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x00071F6F File Offset: 0x0007016F
		public static LocalizedString descPublicFolderServerNotFound
		{
			get
			{
				return new LocalizedString("descPublicFolderServerNotFound", "Ex91C2B3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00071F90 File Offset: 0x00070190
		public static LocalizedString MaxAllowedKeywordsExceeded(int keywordsCount, int maxAllowedKeywordCount)
		{
			return new LocalizedString("MaxAllowedKeywordsExceeded", "", false, false, Strings.ResourceManager, new object[]
			{
				keywordsCount,
				maxAllowedKeywordCount
			});
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x00071FCD File Offset: 0x000701CD
		public static LocalizedString TrackingErrorCrossPremiseAuthentication
		{
			get
			{
				return new LocalizedString("TrackingErrorCrossPremiseAuthentication", "Ex9A5B7F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x00071FEB File Offset: 0x000701EB
		public static LocalizedString MessageInvalidTimeZonePeriodNullId
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZonePeriodNullId", "Ex579284", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0007200C File Offset: 0x0007020C
		public static LocalizedString SortIComparableTypeException(string type)
		{
			return new LocalizedString("SortIComparableTypeException", "", false, false, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0007203C File Offset: 0x0007023C
		public static LocalizedString SourceMailboxUserNotFoundInAD(string name)
		{
			return new LocalizedString("SourceMailboxUserNotFoundInAD", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x0007206B File Offset: 0x0007026B
		public static LocalizedString StorePermanantError
		{
			get
			{
				return new LocalizedString("StorePermanantError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x00072089 File Offset: 0x00070289
		public static LocalizedString TrackingErrorTransientUnexpected
		{
			get
			{
				return new LocalizedString("TrackingErrorTransientUnexpected", "ExB9110B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x000720A7 File Offset: 0x000702A7
		public static LocalizedString TrackingErrorModerationDecisionLogsFromE14Rtm
		{
			get
			{
				return new LocalizedString("TrackingErrorModerationDecisionLogsFromE14Rtm", "ExA7889C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x000720C8 File Offset: 0x000702C8
		public static LocalizedString descProxyRemoteServerError(string source)
		{
			return new LocalizedString("descProxyRemoteServerError", "Ex002282", false, true, Strings.ResourceManager, new object[]
			{
				source
			});
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000720F8 File Offset: 0x000702F8
		public static LocalizedString descInvalidTimeInterval(string propertyName)
		{
			return new LocalizedString("descInvalidTimeInterval", "Ex15E887", false, true, Strings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x00072127 File Offset: 0x00070327
		public static LocalizedString TrackingErrorCASUriDiscovery
		{
			get
			{
				return new LocalizedString("TrackingErrorCASUriDiscovery", "ExE86234", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x00072145 File Offset: 0x00070345
		public static LocalizedString descFailedToFindPublicFolderServer
		{
			get
			{
				return new LocalizedString("descFailedToFindPublicFolderServer", "Ex9260B3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x00072163 File Offset: 0x00070363
		public static LocalizedString MessageTrackingApplicationName
		{
			get
			{
				return new LocalizedString("MessageTrackingApplicationName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x00072181 File Offset: 0x00070381
		public static LocalizedString descAutoDiscoverThruDirectoryFailed
		{
			get
			{
				return new LocalizedString("descAutoDiscoverThruDirectoryFailed", "Ex9D46ED", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x000721A0 File Offset: 0x000703A0
		public static LocalizedString TargetFolderNotFound(string folderName)
		{
			return new LocalizedString("TargetFolderNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x000721CF File Offset: 0x000703CF
		public static LocalizedString MessageInvalidTimeZoneDuplicatePeriods
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneDuplicatePeriods", "Ex99CA23", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x000721F0 File Offset: 0x000703F0
		public static LocalizedString descCannotFindOrganizationalFolderInMailbox(string folderName, string mailboxName)
		{
			return new LocalizedString("descCannotFindOrganizationalFolderInMailbox", "ExFFA08F", false, true, Strings.ResourceManager, new object[]
			{
				folderName,
				mailboxName
			});
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00072224 File Offset: 0x00070424
		public static LocalizedString UnknownBodyFormat(string format)
		{
			return new LocalizedString("UnknownBodyFormat", "Ex0B9762", false, true, Strings.ResourceManager, new object[]
			{
				format
			});
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00072254 File Offset: 0x00070454
		public static LocalizedString SearchFolderTimeout(string mailbox)
		{
			return new LocalizedString("SearchFolderTimeout", "", false, false, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00072284 File Offset: 0x00070484
		public static LocalizedString LogMailSimpleHeader(string status)
		{
			return new LocalizedString("LogMailSimpleHeader", "", false, false, Strings.ResourceManager, new object[]
			{
				status
			});
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x000722B4 File Offset: 0x000704B4
		public static LocalizedString InvalidIdInPreviewResult(string url)
		{
			return new LocalizedString("InvalidIdInPreviewResult", "", false, false, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x000722E3 File Offset: 0x000704E3
		public static LocalizedString LogFieldsPercentComplete
		{
			get
			{
				return new LocalizedString("LogFieldsPercentComplete", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x00072301 File Offset: 0x00070501
		public static LocalizedString descFreeBusyAndSuggestionsNull
		{
			get
			{
				return new LocalizedString("descFreeBusyAndSuggestionsNull", "ExC506D2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0007231F File Offset: 0x0007051F
		public static LocalizedString LogFieldsSuccessfulMailboxes
		{
			get
			{
				return new LocalizedString("LogFieldsSuccessfulMailboxes", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00072340 File Offset: 0x00070540
		public static LocalizedString CorruptedFolder(string mailbox)
		{
			return new LocalizedString("CorruptedFolder", "", false, false, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0007236F File Offset: 0x0007056F
		public static LocalizedString UninstallAssistantsServiceTask
		{
			get
			{
				return new LocalizedString("UninstallAssistantsServiceTask", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x0007238D File Offset: 0x0007058D
		public static LocalizedString ProgressSearching
		{
			get
			{
				return new LocalizedString("ProgressSearching", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x000723AB File Offset: 0x000705AB
		public static LocalizedString SearchInvalidPagination
		{
			get
			{
				return new LocalizedString("SearchInvalidPagination", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x000723C9 File Offset: 0x000705C9
		public static LocalizedString LogFieldsSearchQuery
		{
			get
			{
				return new LocalizedString("LogFieldsSearchQuery", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x000723E7 File Offset: 0x000705E7
		public static LocalizedString TrackingTotalBudgetExceeded
		{
			get
			{
				return new LocalizedString("TrackingTotalBudgetExceeded", "Ex9C58F4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x00072405 File Offset: 0x00070605
		public static LocalizedString descSuggestionMustStartOnThirtyMinuteBoundary
		{
			get
			{
				return new LocalizedString("descSuggestionMustStartOnThirtyMinuteBoundary", "Ex63022C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x00072423 File Offset: 0x00070623
		public static LocalizedString SearchServerShutdown
		{
			get
			{
				return new LocalizedString("SearchServerShutdown", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x00072444 File Offset: 0x00070644
		public static LocalizedString descUnsupportedSecurityDescriptorHeader(string mailboxAddress, string expectedHeaderLength, string actualHeaderLength)
		{
			return new LocalizedString("descUnsupportedSecurityDescriptorHeader", "Ex175878", false, true, Strings.ResourceManager, new object[]
			{
				mailboxAddress,
				expectedHeaderLength,
				actualHeaderLength
			});
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0007247C File Offset: 0x0007067C
		public static LocalizedString OWAServiceUrlFailure(string name, string message)
		{
			return new LocalizedString("OWAServiceUrlFailure", "", false, false, Strings.ResourceManager, new object[]
			{
				name,
				message
			});
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x000724AF File Offset: 0x000706AF
		public static LocalizedString descFailedToCreateLegacyOofRule
		{
			get
			{
				return new LocalizedString("descFailedToCreateLegacyOofRule", "Ex24070E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x000724CD File Offset: 0x000706CD
		public static LocalizedString PhotosApplicationName
		{
			get
			{
				return new LocalizedString("PhotosApplicationName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x000724EC File Offset: 0x000706EC
		public static LocalizedString SearchAdminRpcCallMaxSearches(string mailboxGuid)
		{
			return new LocalizedString("SearchAdminRpcCallMaxSearches", "", false, false, Strings.ResourceManager, new object[]
			{
				mailboxGuid
			});
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0007251C File Offset: 0x0007071C
		public static LocalizedString descInvalidDayOrder(int min, int max)
		{
			return new LocalizedString("descInvalidDayOrder", "ExBAECFF", false, true, Strings.ResourceManager, new object[]
			{
				min,
				max
			});
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0007255C File Offset: 0x0007075C
		public static LocalizedString SearchAdminRpcCallAccessDenied(string displayName, string mailboxGuid)
		{
			return new LocalizedString("SearchAdminRpcCallAccessDenied", "", false, false, Strings.ResourceManager, new object[]
			{
				displayName,
				mailboxGuid
			});
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0007258F File Offset: 0x0007078F
		public static LocalizedString ADUserNotFoundException
		{
			get
			{
				return new LocalizedString("ADUserNotFoundException", "Ex4F874A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x000725B0 File Offset: 0x000707B0
		public static LocalizedString InvalidPreviewResultWebServiceResponse(string url)
		{
			return new LocalizedString("InvalidPreviewResultWebServiceResponse", "", false, false, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x000725DF File Offset: 0x000707DF
		public static LocalizedString ProgressSearchingSources
		{
			get
			{
				return new LocalizedString("ProgressSearchingSources", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x000725FD File Offset: 0x000707FD
		public static LocalizedString LogMailNone
		{
			get
			{
				return new LocalizedString("LogMailNone", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x0007261B File Offset: 0x0007081B
		public static LocalizedString LogFieldsStatusMailRecipients
		{
			get
			{
				return new LocalizedString("LogFieldsStatusMailRecipients", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0007263C File Offset: 0x0007083C
		public static LocalizedString descOnlyDefaultFreeBusyIntervalSupported(int fbInterval)
		{
			return new LocalizedString("descOnlyDefaultFreeBusyIntervalSupported", "Ex941130", false, true, Strings.ResourceManager, new object[]
			{
				fbInterval
			});
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x00072670 File Offset: 0x00070870
		public static LocalizedString LogFieldsResultNumber
		{
			get
			{
				return new LocalizedString("LogFieldsResultNumber", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0007268E File Offset: 0x0007088E
		public static LocalizedString TrackingErrorPermanentUnexpected
		{
			get
			{
				return new LocalizedString("TrackingErrorPermanentUnexpected", "ExA3DA08", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x000726AC File Offset: 0x000708AC
		public static LocalizedString descResultSetTooBig(string allowedSize, string actualSize)
		{
			return new LocalizedString("descResultSetTooBig", "Ex389125", false, true, Strings.ResourceManager, new object[]
			{
				allowedSize,
				actualSize
			});
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x000726DF File Offset: 0x000708DF
		public static LocalizedString ProgressCompleting
		{
			get
			{
				return new LocalizedString("ProgressCompleting", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x000726FD File Offset: 0x000708FD
		public static LocalizedString descInvalidUserOofSettings
		{
			get
			{
				return new LocalizedString("descInvalidUserOofSettings", "ExCD9903", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0007271B File Offset: 0x0007091B
		public static LocalizedString LogFieldsResultNumberEstimate
		{
			get
			{
				return new LocalizedString("LogFieldsResultNumberEstimate", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x0007273C File Offset: 0x0007093C
		public static LocalizedString descTimeoutExpired(string state)
		{
			return new LocalizedString("descTimeoutExpired", "ExA5689D", false, true, Strings.ResourceManager, new object[]
			{
				state
			});
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0007276B File Offset: 0x0007096B
		public static LocalizedString LogFieldsKeywordHits
		{
			get
			{
				return new LocalizedString("LogFieldsKeywordHits", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x00072789 File Offset: 0x00070989
		public static LocalizedString UnknownError
		{
			get
			{
				return new LocalizedString("UnknownError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x000727A7 File Offset: 0x000709A7
		public static LocalizedString MessageInvalidTimeZonePeriodNullBias
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZonePeriodNullBias", "Ex776834", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x000727C5 File Offset: 0x000709C5
		public static LocalizedString MailboxSeachCountIncludeUnsearchable
		{
			get
			{
				return new LocalizedString("MailboxSeachCountIncludeUnsearchable", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x000727E3 File Offset: 0x000709E3
		public static LocalizedString LogMailBlank
		{
			get
			{
				return new LocalizedString("LogMailBlank", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x00072801 File Offset: 0x00070A01
		public static LocalizedString descWorkHoursEndTimeInvalid
		{
			get
			{
				return new LocalizedString("descWorkHoursEndTimeInvalid", "ExA6460B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00072820 File Offset: 0x00070A20
		public static LocalizedString RefinerValueNullOrCountZero(string url)
		{
			return new LocalizedString("RefinerValueNullOrCountZero", "", false, false, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00072850 File Offset: 0x00070A50
		public static LocalizedString descNotAContactOrUser(string emailAddress)
		{
			return new LocalizedString("descNotAContactOrUser", "Ex5506B1", false, true, Strings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00072880 File Offset: 0x00070A80
		public static LocalizedString descFreeBusyDLLimitReached(int allowedSize)
		{
			return new LocalizedString("descFreeBusyDLLimitReached", "Ex503E92", false, true, Strings.ResourceManager, new object[]
			{
				allowedSize
			});
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x000728B4 File Offset: 0x00070AB4
		public static LocalizedString ProgessCreatingThreads
		{
			get
			{
				return new LocalizedString("ProgessCreatingThreads", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x000728D2 File Offset: 0x00070AD2
		public static LocalizedString descOOFCannotReadExternalOofOptions
		{
			get
			{
				return new LocalizedString("descOOFCannotReadExternalOofOptions", "Ex2B01CC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x000728F0 File Offset: 0x00070AF0
		public static LocalizedString PrimaryMailbox
		{
			get
			{
				return new LocalizedString("PrimaryMailbox", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00072910 File Offset: 0x00070B10
		public static LocalizedString descFailedToFindElcRoot(string mailboxName)
		{
			return new LocalizedString("descFailedToFindElcRoot", "Ex354896", false, true, Strings.ResourceManager, new object[]
			{
				mailboxName
			});
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x0007293F File Offset: 0x00070B3F
		public static LocalizedString MessageInvalidTimeZoneCustomTimeZoneThreeElements
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneCustomTimeZoneThreeElements", "Ex04F1B2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0007295D File Offset: 0x00070B5D
		public static LocalizedString TrackingTransientError
		{
			get
			{
				return new LocalizedString("TrackingTransientError", "ExCC0EAB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x0007297B File Offset: 0x00070B7B
		public static LocalizedString LogFieldsLastStartTime
		{
			get
			{
				return new LocalizedString("LogFieldsLastStartTime", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0007299C File Offset: 0x00070B9C
		public static LocalizedString FailedToFetchPreviewItems(string mailbox)
		{
			return new LocalizedString("FailedToFetchPreviewItems", "", false, false, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x000729CB File Offset: 0x00070BCB
		public static LocalizedString CopyItemsFailed
		{
			get
			{
				return new LocalizedString("CopyItemsFailed", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x000729E9 File Offset: 0x00070BE9
		public static LocalizedString DummyApplicationName
		{
			get
			{
				return new LocalizedString("DummyApplicationName", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x00072A07 File Offset: 0x00070C07
		public static LocalizedString MessageInvalidTimeZoneDuplicateGroups
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneDuplicateGroups", "ExAA88C0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00072A28 File Offset: 0x00070C28
		public static LocalizedString descInvalidMergedFreeBusyInterval(string minimumValue, string maximumValue)
		{
			return new LocalizedString("descInvalidMergedFreeBusyInterval", "Ex3161D9", false, true, Strings.ResourceManager, new object[]
			{
				minimumValue,
				maximumValue
			});
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00072A5C File Offset: 0x00070C5C
		public static LocalizedString UnknownRecurrenceOrderType(string name)
		{
			return new LocalizedString("UnknownRecurrenceOrderType", "ExFEC19C", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x00072A8B File Offset: 0x00070C8B
		public static LocalizedString ExecutingUserNeedSmtpAddress
		{
			get
			{
				return new LocalizedString("ExecutingUserNeedSmtpAddress", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x00072AA9 File Offset: 0x00070CA9
		public static LocalizedString MessageInvalidTimeZoneMoreThanTwoPeriodsUnsupported
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneMoreThanTwoPeriodsUnsupported", "Ex5D4BB5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x00072AC7 File Offset: 0x00070CC7
		public static LocalizedString descMailRecipientNotFound
		{
			get
			{
				return new LocalizedString("descMailRecipientNotFound", "Ex8FF96B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x00072AE5 File Offset: 0x00070CE5
		public static LocalizedString descNullUserName
		{
			get
			{
				return new LocalizedString("descNullUserName", "Ex2FA90E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x00072B03 File Offset: 0x00070D03
		public static LocalizedString NonNegativeParameter
		{
			get
			{
				return new LocalizedString("NonNegativeParameter", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x00072B21 File Offset: 0x00070D21
		public static LocalizedString LogFieldsEstimateNotExcludeDuplicates
		{
			get
			{
				return new LocalizedString("LogFieldsEstimateNotExcludeDuplicates", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00072B40 File Offset: 0x00070D40
		public static LocalizedString descFailedToGetOrganizationalFoldersForMailbox(string mailboxName)
		{
			return new LocalizedString("descFailedToGetOrganizationalFoldersForMailbox", "ExAE2798", false, true, Strings.ResourceManager, new object[]
			{
				mailboxName
			});
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00072B70 File Offset: 0x00070D70
		public static LocalizedString descElcNoMatchingOrgFolder(string folderName)
		{
			return new LocalizedString("descElcNoMatchingOrgFolder", "ExA8325E", false, true, Strings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x00072B9F File Offset: 0x00070D9F
		public static LocalizedString StoreTransientError
		{
			get
			{
				return new LocalizedString("StoreTransientError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00072BC0 File Offset: 0x00070DC0
		public static LocalizedString descInvalidTransitionBias(int min, int max)
		{
			return new LocalizedString("descInvalidTransitionBias", "ExDD6AC9", false, true, Strings.ResourceManager, new object[]
			{
				min,
				max
			});
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x00072BFD File Offset: 0x00070DFD
		public static LocalizedString LogFieldsLogLevel
		{
			get
			{
				return new LocalizedString("LogFieldsLogLevel", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00072C1C File Offset: 0x00070E1C
		public static LocalizedString SearchUserNotFound(string displayName)
		{
			return new LocalizedString("SearchUserNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				displayName
			});
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x00072C4B File Offset: 0x00070E4B
		public static LocalizedString TrackingWSRequestCorrupt
		{
			get
			{
				return new LocalizedString("TrackingWSRequestCorrupt", "Ex3B789B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x00072C69 File Offset: 0x00070E69
		public static LocalizedString SearchDisabled
		{
			get
			{
				return new LocalizedString("SearchDisabled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00072C88 File Offset: 0x00070E88
		public static LocalizedString PhotoRetrievalFailedIOError(string innerExceptionMessage)
		{
			return new LocalizedString("PhotoRetrievalFailedIOError", "", false, false, Strings.ResourceManager, new object[]
			{
				innerExceptionMessage
			});
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00072CB8 File Offset: 0x00070EB8
		public static LocalizedString DatabaseLocationUnavailable(string mailbox)
		{
			return new LocalizedString("DatabaseLocationUnavailable", "", false, false, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00072CE8 File Offset: 0x00070EE8
		public static LocalizedString SearchTooManyKeywords(int keywords)
		{
			return new LocalizedString("SearchTooManyKeywords", "", false, false, Strings.ResourceManager, new object[]
			{
				keywords
			});
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x00072D1C File Offset: 0x00070F1C
		public static LocalizedString descNullCredentialsToServiceDiscoveryRequest
		{
			get
			{
				return new LocalizedString("descNullCredentialsToServiceDiscoveryRequest", "ExE5E275", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x00072D3A File Offset: 0x00070F3A
		public static LocalizedString LogFieldsUnsuccessfulMailboxes
		{
			get
			{
				return new LocalizedString("LogFieldsUnsuccessfulMailboxes", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x00072D58 File Offset: 0x00070F58
		public static LocalizedString MessageInvalidTimeZoneTimeZoneNotFound
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneTimeZoneNotFound", "Ex50B464", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x00072D76 File Offset: 0x00070F76
		public static LocalizedString MessageInvalidTimeZoneIntValueIsInvalid
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneIntValueIsInvalid", "Ex6F7FEF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00072D94 File Offset: 0x00070F94
		public static LocalizedString descFailedToCreateElcRootRetry(string mailboxName)
		{
			return new LocalizedString("descFailedToCreateElcRootRetry", "Ex4D5CA4", false, true, Strings.ResourceManager, new object[]
			{
				mailboxName
			});
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00072DC4 File Offset: 0x00070FC4
		public static LocalizedString SearchAdminRpcSearchCallTimedout(string prefix, int mailboxesCount, string database, string query)
		{
			return new LocalizedString("SearchAdminRpcSearchCallTimedout", "", false, false, Strings.ResourceManager, new object[]
			{
				prefix,
				mailboxesCount,
				database,
				query
			});
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x00072E04 File Offset: 0x00071004
		public static LocalizedString ScheduleConfigurationSchedule
		{
			get
			{
				return new LocalizedString("ScheduleConfigurationSchedule", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x00072E22 File Offset: 0x00071022
		public static LocalizedString descOofRuleSaveException
		{
			get
			{
				return new LocalizedString("descOofRuleSaveException", "Ex8FBABC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00072E40 File Offset: 0x00071040
		public static LocalizedString TrackingRpcError(int errorCode)
		{
			return new LocalizedString("TrackingRpcError", "Ex0705CC", false, true, Strings.ResourceManager, new object[]
			{
				errorCode
			});
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x00072E74 File Offset: 0x00071074
		public static LocalizedString LogFieldsTargetMailbox
		{
			get
			{
				return new LocalizedString("LogFieldsTargetMailbox", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00072E94 File Offset: 0x00071094
		public static LocalizedString SearchNonFullTextPaginationProperty(string paginationClause)
		{
			return new LocalizedString("SearchNonFullTextPaginationProperty", "", false, false, Strings.ResourceManager, new object[]
			{
				paginationClause
			});
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00072EC4 File Offset: 0x000710C4
		public static LocalizedString descPFNotSupported(string mailbox)
		{
			return new LocalizedString("descPFNotSupported", "", false, false, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x00072EF3 File Offset: 0x000710F3
		public static LocalizedString descInvalidSuggestionsTimeRange
		{
			get
			{
				return new LocalizedString("descInvalidSuggestionsTimeRange", "Ex2C2FBC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x00072F11 File Offset: 0x00071111
		public static LocalizedString descFailedToGetUserOofPolicy
		{
			get
			{
				return new LocalizedString("descFailedToGetUserOofPolicy", "ExC7E9E0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00072F30 File Offset: 0x00071130
		public static LocalizedString NestedFanout(string mailbox)
		{
			return new LocalizedString("NestedFanout", "", false, false, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x00072F5F File Offset: 0x0007115F
		public static LocalizedString descMeetingSuggestionsDurationTooLarge
		{
			get
			{
				return new LocalizedString("descMeetingSuggestionsDurationTooLarge", "Ex8A792F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x00072F7D File Offset: 0x0007117D
		public static LocalizedString ProgressOpening
		{
			get
			{
				return new LocalizedString("ProgressOpening", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00072F9C File Offset: 0x0007119C
		public static LocalizedString descMailTipsSenderNotFound(string address)
		{
			return new LocalizedString("descMailTipsSenderNotFound", "Ex4C4966", false, true, Strings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x00072FCB File Offset: 0x000711CB
		public static LocalizedString TrackingErrorBudgetExceededMultiMessageSearch
		{
			get
			{
				return new LocalizedString("TrackingErrorBudgetExceededMultiMessageSearch", "ExFB7FE7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x00072FE9 File Offset: 0x000711E9
		public static LocalizedString descLocalServerObjectNotFound
		{
			get
			{
				return new LocalizedString("descLocalServerObjectNotFound", "ExB4A1CC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00073008 File Offset: 0x00071208
		public static LocalizedString CISearchFailed(string mailbox)
		{
			return new LocalizedString("CISearchFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x00073037 File Offset: 0x00071237
		public static LocalizedString RecoverableItems
		{
			get
			{
				return new LocalizedString("RecoverableItems", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x00073055 File Offset: 0x00071255
		public static LocalizedString descProxyRequestFailed
		{
			get
			{
				return new LocalizedString("descProxyRequestFailed", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00073074 File Offset: 0x00071274
		public static LocalizedString CouldNotFindOrgRelationship(string domain)
		{
			return new LocalizedString("CouldNotFindOrgRelationship", "", false, false, Strings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x000730A3 File Offset: 0x000712A3
		public static LocalizedString ProgessCreating
		{
			get
			{
				return new LocalizedString("ProgessCreating", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000730C4 File Offset: 0x000712C4
		public static LocalizedString descInvalidYear(int min, int max)
		{
			return new LocalizedString("descInvalidYear", "", false, false, Strings.ResourceManager, new object[]
			{
				min,
				max
			});
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x00073101 File Offset: 0x00071301
		public static LocalizedString descClientDisconnected
		{
			get
			{
				return new LocalizedString("descClientDisconnected", "ExAFAFB1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x0007311F File Offset: 0x0007131F
		public static LocalizedString SearchThrottled
		{
			get
			{
				return new LocalizedString("SearchThrottled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0007313D File Offset: 0x0007133D
		public static LocalizedString descInvalidTransitionTime
		{
			get
			{
				return new LocalizedString("descInvalidTransitionTime", "ExE9C5D7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0007315C File Offset: 0x0007135C
		public static LocalizedString InvalidMailboxInMailboxStatistics(string url)
		{
			return new LocalizedString("InvalidMailboxInMailboxStatistics", "", false, false, Strings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x0007318B File Offset: 0x0007138B
		public static LocalizedString TargetMailboxOutOfSpace
		{
			get
			{
				return new LocalizedString("TargetMailboxOutOfSpace", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x000731A9 File Offset: 0x000713A9
		public static LocalizedString LogFieldsLastRunBy
		{
			get
			{
				return new LocalizedString("LogFieldsLastRunBy", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x000731C7 File Offset: 0x000713C7
		public static LocalizedString UnableToReadServiceTopology
		{
			get
			{
				return new LocalizedString("UnableToReadServiceTopology", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x000731E5 File Offset: 0x000713E5
		public static LocalizedString descElcRootFolderName
		{
			get
			{
				return new LocalizedString("descElcRootFolderName", "ExF0E936", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x00073203 File Offset: 0x00071403
		public static LocalizedString descNoMailTipsInEwsResponseMessage
		{
			get
			{
				return new LocalizedString("descNoMailTipsInEwsResponseMessage", "Ex746289", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x00073221 File Offset: 0x00071421
		public static LocalizedString descFailedToGetRules
		{
			get
			{
				return new LocalizedString("descFailedToGetRules", "ExD47947", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0007323F File Offset: 0x0007143F
		public static LocalizedString LogFieldsStoppedBy
		{
			get
			{
				return new LocalizedString("LogFieldsStoppedBy", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00073260 File Offset: 0x00071460
		public static LocalizedString descAutoDiscoverBadRedirectLocation(string emailAddress, string redirectAddress)
		{
			return new LocalizedString("descAutoDiscoverBadRedirectLocation", "", false, false, Strings.ResourceManager, new object[]
			{
				emailAddress,
				redirectAddress
			});
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00073294 File Offset: 0x00071494
		public static LocalizedString SearchTaskTimeoutPrimary(string displayName, string mailboxGuid)
		{
			return new LocalizedString("SearchTaskTimeoutPrimary", "", false, false, Strings.ResourceManager, new object[]
			{
				displayName,
				mailboxGuid
			});
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x000732C7 File Offset: 0x000714C7
		public static LocalizedString descProxyRequestProcessingSocketError
		{
			get
			{
				return new LocalizedString("descProxyRequestProcessingSocketError", "Ex9AF0F5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x000732E5 File Offset: 0x000714E5
		public static LocalizedString descProxyRequestNotAllowed
		{
			get
			{
				return new LocalizedString("descProxyRequestNotAllowed", "ExF290A7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00073304 File Offset: 0x00071504
		public static LocalizedString SourceMailboxVersionError(string name)
		{
			return new LocalizedString("SourceMailboxVersionError", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00073334 File Offset: 0x00071534
		public static LocalizedString InsufficientSpaceOnTargetMailbox(string estimated, string available)
		{
			return new LocalizedString("InsufficientSpaceOnTargetMailbox", "", false, false, Strings.ResourceManager, new object[]
			{
				estimated,
				available
			});
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x00073367 File Offset: 0x00071567
		public static LocalizedString descInvalidAccessLevel
		{
			get
			{
				return new LocalizedString("descInvalidAccessLevel", "Ex104820", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x00073385 File Offset: 0x00071585
		public static LocalizedString TrackingPermanentErrorMultiMessageSearch
		{
			get
			{
				return new LocalizedString("TrackingPermanentErrorMultiMessageSearch", "Ex1036C2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x000733A4 File Offset: 0x000715A4
		public static LocalizedString PhotoRetrievalFailedWin32Error(string innerExceptionMessage)
		{
			return new LocalizedString("PhotoRetrievalFailedWin32Error", "", false, false, Strings.ResourceManager, new object[]
			{
				innerExceptionMessage
			});
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x000733D3 File Offset: 0x000715D3
		public static LocalizedString LogFieldsCreatedBy
		{
			get
			{
				return new LocalizedString("LogFieldsCreatedBy", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x000733F1 File Offset: 0x000715F1
		public static LocalizedString LowSystemResource
		{
			get
			{
				return new LocalizedString("LowSystemResource", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x0007340F File Offset: 0x0007160F
		public static LocalizedString TrackingErrorLogSearchServiceDown
		{
			get
			{
				return new LocalizedString("TrackingErrorLogSearchServiceDown", "Ex32E8EE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x00073430 File Offset: 0x00071630
		public static LocalizedString descSoapAutoDiscoverResponseError(string url, string errorMessage)
		{
			return new LocalizedString("descSoapAutoDiscoverResponseError", "Ex6C7F2F", false, true, Strings.ResourceManager, new object[]
			{
				url,
				errorMessage
			});
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x00073463 File Offset: 0x00071663
		public static LocalizedString LogMailNotApplicable
		{
			get
			{
				return new LocalizedString("LogMailNotApplicable", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x00073481 File Offset: 0x00071681
		public static LocalizedString descNoFreeBusyAccess
		{
			get
			{
				return new LocalizedString("descNoFreeBusyAccess", "ExD48DD4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x0007349F File Offset: 0x0007169F
		public static LocalizedString ADUserMisconfiguredException
		{
			get
			{
				return new LocalizedString("ADUserMisconfiguredException", "Ex1896E6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x000734BD File Offset: 0x000716BD
		public static LocalizedString ProgressOpeningTarget
		{
			get
			{
				return new LocalizedString("ProgressOpeningTarget", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x000734DB File Offset: 0x000716DB
		public static LocalizedString descOOFVirusDetectedOofReplyMessage
		{
			get
			{
				return new LocalizedString("descOOFVirusDetectedOofReplyMessage", "Ex21097B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x000734F9 File Offset: 0x000716F9
		public static LocalizedString MessageInvalidTimeZonePeriodNullName
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZonePeriodNullName", "ExAB806F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x00073517 File Offset: 0x00071717
		public static LocalizedString MessageInvalidTimeZoneFirstTransition
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneFirstTransition", "Ex28AF95", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00073538 File Offset: 0x00071738
		public static LocalizedString descErrorEwsResponse(int errorCode)
		{
			return new LocalizedString("descErrorEwsResponse", "Ex16A609", false, true, Strings.ResourceManager, new object[]
			{
				errorCode
			});
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0007356C File Offset: 0x0007176C
		public static LocalizedString descInvalidConfigForCrossForestRequest(string mailbox)
		{
			return new LocalizedString("descInvalidConfigForCrossForestRequest", "Ex8264F7", false, true, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x0007359B File Offset: 0x0007179B
		public static LocalizedString InvalidContactException
		{
			get
			{
				return new LocalizedString("InvalidContactException", "ExC950E2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x000735B9 File Offset: 0x000717B9
		public static LocalizedString ErrorRemoveOngoingSearch
		{
			get
			{
				return new LocalizedString("ErrorRemoveOngoingSearch", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x000735D8 File Offset: 0x000717D8
		public static LocalizedString SearchAdminRpcCallFailed(string mailboxGuid, int errorCode)
		{
			return new LocalizedString("SearchAdminRpcCallFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				mailboxGuid,
				errorCode
			});
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x00073610 File Offset: 0x00071810
		public static LocalizedString SearchTaskCancelled(string mailboxGuids, string databaseGuid)
		{
			return new LocalizedString("SearchTaskCancelled", "", false, false, Strings.ResourceManager, new object[]
			{
				mailboxGuids,
				databaseGuid
			});
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00073644 File Offset: 0x00071844
		public static LocalizedString descMissingArgument(string argument)
		{
			return new LocalizedString("descMissingArgument", "ExB6FDB1", false, true, Strings.ResourceManager, new object[]
			{
				argument
			});
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x00073673 File Offset: 0x00071873
		public static LocalizedString TrackingErrorReadStatus
		{
			get
			{
				return new LocalizedString("TrackingErrorReadStatus", "Ex065EFB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00073694 File Offset: 0x00071894
		public static LocalizedString SearchTaskCancelledArchive(string displayName, string mailboxGuid)
		{
			return new LocalizedString("SearchTaskCancelledArchive", "", false, false, Strings.ResourceManager, new object[]
			{
				displayName,
				mailboxGuid
			});
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x000736C7 File Offset: 0x000718C7
		public static LocalizedString TrackingErrorCrossForestAuthentication
		{
			get
			{
				return new LocalizedString("TrackingErrorCrossForestAuthentication", "Ex1FADC8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x000736E5 File Offset: 0x000718E5
		public static LocalizedString descInvalidFreeBusyViewType
		{
			get
			{
				return new LocalizedString("descInvalidFreeBusyViewType", "ExE51E78", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00073704 File Offset: 0x00071904
		public static LocalizedString FailedToGetItem(string messageClass, string folder)
		{
			return new LocalizedString("FailedToGetItem", "", false, false, Strings.ResourceManager, new object[]
			{
				messageClass,
				folder
			});
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00073738 File Offset: 0x00071938
		public static LocalizedString descProxyRequestProcessingIOError(string exceptionMessage)
		{
			return new LocalizedString("descProxyRequestProcessingIOError", "ExEF88FC", false, true, Strings.ResourceManager, new object[]
			{
				exceptionMessage
			});
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00073768 File Offset: 0x00071968
		public static LocalizedString descInputFolderNamesContainDuplicates(string folderName)
		{
			return new LocalizedString("descInputFolderNamesContainDuplicates", "ExB58B40", false, true, Strings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x00073797 File Offset: 0x00071997
		public static LocalizedString descLogonAsNetworkServiceFailed
		{
			get
			{
				return new LocalizedString("descLogonAsNetworkServiceFailed", "ExE7C7B2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x000737B5 File Offset: 0x000719B5
		public static LocalizedString TrackingErrorCrossForestMisconfiguration
		{
			get
			{
				return new LocalizedString("TrackingErrorCrossForestMisconfiguration", "ExDDEADD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000737D4 File Offset: 0x000719D4
		public static LocalizedString RangedParameter(int max)
		{
			return new LocalizedString("RangedParameter", "", false, false, Strings.ResourceManager, new object[]
			{
				max
			});
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00073808 File Offset: 0x00071A08
		public static LocalizedString descPrimaryDefaultCorrupted(string policyName, int tagCount)
		{
			return new LocalizedString("descPrimaryDefaultCorrupted", "Ex80411F", false, true, Strings.ResourceManager, new object[]
			{
				policyName,
				tagCount
			});
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x00073840 File Offset: 0x00071A40
		public static LocalizedString MessageInvalidTimeZoneMissedGroup
		{
			get
			{
				return new LocalizedString("MessageInvalidTimeZoneMissedGroup", "Ex0E6923", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x0007385E File Offset: 0x00071A5E
		public static LocalizedString NoKeywordStatsForCopySearch
		{
			get
			{
				return new LocalizedString("NoKeywordStatsForCopySearch", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0007387C File Offset: 0x00071A7C
		public static LocalizedString descMailboxFailover
		{
			get
			{
				return new LocalizedString("descMailboxFailover", "ExAA06FD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0007389C File Offset: 0x00071A9C
		public static LocalizedString InvalidUnknownMailboxInPreviewResult(string url, string legacyDn, string mailboxGuid)
		{
			return new LocalizedString("InvalidUnknownMailboxInPreviewResult", "", false, false, Strings.ResourceManager, new object[]
			{
				url,
				legacyDn,
				mailboxGuid
			});
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x000738D3 File Offset: 0x00071AD3
		public static LocalizedString LogFieldsExcludeDuplicateMessages
		{
			get
			{
				return new LocalizedString("LogFieldsExcludeDuplicateMessages", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x000738F4 File Offset: 0x00071AF4
		public static LocalizedString SourceMailboxCrossPremiseError(string name)
		{
			return new LocalizedString("SourceMailboxCrossPremiseError", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x00073923 File Offset: 0x00071B23
		public static LocalizedString LogFieldsErrors
		{
			get
			{
				return new LocalizedString("LogFieldsErrors", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x00073941 File Offset: 0x00071B41
		public static LocalizedString mailTipsTenant
		{
			get
			{
				return new LocalizedString("mailTipsTenant", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x00073960 File Offset: 0x00071B60
		public static LocalizedString descE14orHigherProxyServerNotFound(string email, int serverVersion)
		{
			return new LocalizedString("descE14orHigherProxyServerNotFound", "ExD7FBFA", false, true, Strings.ResourceManager, new object[]
			{
				email,
				serverVersion
			});
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00073998 File Offset: 0x00071B98
		public static LocalizedString RemoteMailbox(string name)
		{
			return new LocalizedString("RemoteMailbox", "", false, false, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x000739C7 File Offset: 0x00071BC7
		public static LocalizedString SearchArgument
		{
			get
			{
				return new LocalizedString("SearchArgument", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x000739E5 File Offset: 0x00071BE5
		public static LocalizedString TrackingErrorCrossPremiseMisconfigurationMultiMessageSearch
		{
			get
			{
				return new LocalizedString("TrackingErrorCrossPremiseMisconfigurationMultiMessageSearch", "Ex4CD829", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x00073A03 File Offset: 0x00071C03
		public static LocalizedString descNullEmailToAutoDiscoverRequest
		{
			get
			{
				return new LocalizedString("descNullEmailToAutoDiscoverRequest", "ExB7D446", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00073A21 File Offset: 0x00071C21
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000FA5 RID: 4005
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(235);

		// Token: 0x04000FA6 RID: 4006
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.InfoWorker.Common.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200031F RID: 799
		public enum IDs : uint
		{
			// Token: 0x04000FA8 RID: 4008
			TrackingErrorCrossPremiseMisconfiguration = 4118843607U,
			// Token: 0x04000FA9 RID: 4009
			LogFieldsResultSizeCopied = 887069165U,
			// Token: 0x04000FAA RID: 4010
			MailtipsApplicationName = 2434190600U,
			// Token: 0x04000FAB RID: 4011
			descNotDefaultCalendar = 3039411601U,
			// Token: 0x04000FAC RID: 4012
			TrackingPermanentError = 3645107181U,
			// Token: 0x04000FAD RID: 4013
			WrongTargetServer = 1560475167U,
			// Token: 0x04000FAE RID: 4014
			InvalidSearchQuery = 4159961541U,
			// Token: 0x04000FAF RID: 4015
			ProgressCompletingSearch = 2177919315U,
			// Token: 0x04000FB0 RID: 4016
			descWorkHoursStartTimeInvalid = 3855823295U,
			// Token: 0x04000FB1 RID: 4017
			descMailboxLogonFailed = 996171111U,
			// Token: 0x04000FB2 RID: 4018
			InstallAssistantsServiceTask = 3025732470U,
			// Token: 0x04000FB3 RID: 4019
			MessageInvalidTimeZoneTransitionGroupNullId = 4098403379U,
			// Token: 0x04000FB4 RID: 4020
			FailedCommunicationException = 3696858122U,
			// Token: 0x04000FB5 RID: 4021
			LogFieldsNumberUnsuccessfulMailboxes = 3416023611U,
			// Token: 0x04000FB6 RID: 4022
			SearchNotStarted = 4244107904U,
			// Token: 0x04000FB7 RID: 4023
			TrackingErrorConnectivity = 2960157992U,
			// Token: 0x04000FB8 RID: 4024
			TargetFolder = 946397903U,
			// Token: 0x04000FB9 RID: 4025
			Unsearchable = 1774313355U,
			// Token: 0x04000FBA RID: 4026
			LogFieldsRecipients = 3752482865U,
			// Token: 0x04000FBB RID: 4027
			InvalidPreviewItemInResultRows = 357978241U,
			// Token: 0x04000FBC RID: 4028
			descInvalidSmptAddres = 2642055223U,
			// Token: 0x04000FBD RID: 4029
			TrackingErrorBudgetExceeded = 2249986005U,
			// Token: 0x04000FBE RID: 4030
			LogFieldsKeywordMbxs = 1952477638U,
			// Token: 0x04000FBF RID: 4031
			descQueryGenerationNotRequired = 859073841U,
			// Token: 0x04000FC0 RID: 4032
			TrackingNoDefaultDomain = 3528487049U,
			// Token: 0x04000FC1 RID: 4033
			InvalidChangeKeyReturned = 457909195U,
			// Token: 0x04000FC2 RID: 4034
			ArchiveMailbox = 3832545148U,
			// Token: 0x04000FC3 RID: 4035
			LogFieldsKeywordHitCount = 4235476896U,
			// Token: 0x04000FC4 RID: 4036
			LogFieldsName = 824833090U,
			// Token: 0x04000FC5 RID: 4037
			LogFieldsStatus = 3196432253U,
			// Token: 0x04000FC6 RID: 4038
			LogFieldsEndDate = 1572073070U,
			// Token: 0x04000FC7 RID: 4039
			LogFieldsSenders = 1970283657U,
			// Token: 0x04000FC8 RID: 4040
			TrackingErrorTimeBudgetExceeded = 1734826510U,
			// Token: 0x04000FC9 RID: 4041
			TrackingLogVersionIncompatible = 127105864U,
			// Token: 0x04000FCA RID: 4042
			AutodiscoverFailedException = 4195952492U,
			// Token: 0x04000FCB RID: 4043
			descInvalidMaxNonWorkHourResultsPerDay = 215769503U,
			// Token: 0x04000FCC RID: 4044
			descNullAutoDiscoverResponse = 1477200533U,
			// Token: 0x04000FCD RID: 4045
			UnexpectedRemoteDataException = 1446717588U,
			// Token: 0x04000FCE RID: 4046
			LogFieldsIncludeKeywordStatistics = 4270171893U,
			// Token: 0x04000FCF RID: 4047
			UnexpectedError = 2870421805U,
			// Token: 0x04000FD0 RID: 4048
			descInvalidMaximumResults = 2171828426U,
			// Token: 0x04000FD1 RID: 4049
			descOOFCannotReadOofConfigData = 1023107612U,
			// Token: 0x04000FD2 RID: 4050
			descInvalidSecurityDescriptor = 366714169U,
			// Token: 0x04000FD3 RID: 4051
			InvalidResultMerge = 540675128U,
			// Token: 0x04000FD4 RID: 4052
			SearchObjectNotFound = 252553502U,
			// Token: 0x04000FD5 RID: 4053
			TrackingInstanceBudgetExceeded = 1359157944U,
			// Token: 0x04000FD6 RID: 4054
			MessageInvalidTimeZoneOutOfRange = 3961981453U,
			// Token: 0x04000FD7 RID: 4055
			LogFieldsStartDate = 3014888409U,
			// Token: 0x04000FD8 RID: 4056
			NotOperator = 273956641U,
			// Token: 0x04000FD9 RID: 4057
			LogFieldsSourceRecipients = 959712720U,
			// Token: 0x04000FDA RID: 4058
			TrackingErrorQueueViewerRpc = 1261494857U,
			// Token: 0x04000FDB RID: 4059
			SearchLogHeader = 2084277545U,
			// Token: 0x04000FDC RID: 4060
			MessageInvalidTimeZoneInvalidOffsetFormat = 609234870U,
			// Token: 0x04000FDD RID: 4061
			descInvalidCredentials = 669181240U,
			// Token: 0x04000FDE RID: 4062
			LogFieldsSearchOperation = 3577556142U,
			// Token: 0x04000FDF RID: 4063
			MessageInvalidTimeZoneNonFirstTransition = 3644766027U,
			// Token: 0x04000FE0 RID: 4064
			descMeetingSuggestionsDurationTooSmall = 184705256U,
			// Token: 0x04000FE1 RID: 4065
			DeleteItemsFailed = 1884462766U,
			// Token: 0x04000FE2 RID: 4066
			PendingSynchronizationException = 2550395136U,
			// Token: 0x04000FE3 RID: 4067
			LogFieldsNumberMailboxesToSearch = 4277240717U,
			// Token: 0x04000FE4 RID: 4068
			LogFieldsKeywordKeyword = 2182808069U,
			// Token: 0x04000FE5 RID: 4069
			ObjectNotFound = 2422079678U,
			// Token: 0x04000FE6 RID: 4070
			MessageInvalidTimeZoneMissedPeriod = 3865092385U,
			// Token: 0x04000FE7 RID: 4071
			LogFieldsResultSize = 3051609629U,
			// Token: 0x04000FE8 RID: 4072
			MessageInvalidTimeZoneDayOfWeekValue = 447822483U,
			// Token: 0x04000FE9 RID: 4073
			TrackingBusy = 3099813970U,
			// Token: 0x04000FEA RID: 4074
			InvalidAppointmentException = 3278774409U,
			// Token: 0x04000FEB RID: 4075
			descInvalidNetworkServiceContext = 1105384606U,
			// Token: 0x04000FEC RID: 4076
			ResultsNotDeduped = 1524653606U,
			// Token: 0x04000FED RID: 4077
			ErrorTimeZone = 610144303U,
			// Token: 0x04000FEE RID: 4078
			BatchSynchronizationFailedException = 2456116760U,
			// Token: 0x04000FEF RID: 4079
			descStartAndEndTimesOutSideFreeBusyData = 876631183U,
			// Token: 0x04000FF0 RID: 4080
			descWin32InteropError = 4252318527U,
			// Token: 0x04000FF1 RID: 4081
			TrackingErrorLegacySender = 3219875537U,
			// Token: 0x04000FF2 RID: 4082
			descNoEwsResponse = 3436346834U,
			// Token: 0x04000FF3 RID: 4083
			LogFieldsResultsLink = 3301704787U,
			// Token: 0x04000FF4 RID: 4084
			TrackingErrorLegacySenderMultiMessageSearch = 2977121749U,
			// Token: 0x04000FF5 RID: 4085
			LogFieldsManagedBy = 2205699811U,
			// Token: 0x04000FF6 RID: 4086
			KeywordHitEmptyQuery = 1676524031U,
			// Token: 0x04000FF7 RID: 4087
			SearchQueryEmpty = 2152565173U,
			// Token: 0x04000FF8 RID: 4088
			ServerShutdown = 2705707569U,
			// Token: 0x04000FF9 RID: 4089
			TrackingErrorSuffixForAdministrator = 377459750U,
			// Token: 0x04000FFA RID: 4090
			SubscriptionNotFoundException = 4273761671U,
			// Token: 0x04000FFB RID: 4091
			LogFieldsResultSizeEstimate = 2881797395U,
			// Token: 0x04000FFC RID: 4092
			descNullDateInChangeDate = 3863825871U,
			// Token: 0x04000FFD RID: 4093
			SharingFolderNotFoundException = 232000386U,
			// Token: 0x04000FFE RID: 4094
			LogMailAll = 1607453502U,
			// Token: 0x04000FFF RID: 4095
			descInvalidScheduledOofDuration = 3106977401U,
			// Token: 0x04001000 RID: 4096
			TrackingTransientErrorMultiMessageSearch = 1899968567U,
			// Token: 0x04001001 RID: 4097
			AqsParserError = 2680389304U,
			// Token: 0x04001002 RID: 4098
			LegacyOofMessage = 829039958U,
			// Token: 0x04001003 RID: 4099
			descInvalidClientSecurityContext = 118976146U,
			// Token: 0x04001004 RID: 4100
			LogMailFooter = 3912319220U,
			// Token: 0x04001005 RID: 4101
			descInvalidFormatInEmail = 2822320878U,
			// Token: 0x04001006 RID: 4102
			descNoCalendar = 2977444274U,
			// Token: 0x04001007 RID: 4103
			UnexpectedUserResponses = 1018641786U,
			// Token: 0x04001008 RID: 4104
			LogFieldsIdentity = 2215464039U,
			// Token: 0x04001009 RID: 4105
			FreeBusyApplicationName = 3854866410U,
			// Token: 0x0400100A RID: 4106
			SearchAborted = 2236533269U,
			// Token: 0x0400100B RID: 4107
			AutodiscoverTimedOut = 1756460035U,
			// Token: 0x0400100C RID: 4108
			LogFieldsSearchDumpster = 3871609939U,
			// Token: 0x0400100D RID: 4109
			descInvalidAuthorizationContext = 274121238U,
			// Token: 0x0400100E RID: 4110
			LogFieldsMessageTypes = 4205846133U,
			// Token: 0x0400100F RID: 4111
			PositiveParameter = 3832116504U,
			// Token: 0x04001010 RID: 4112
			LogMailSeeAttachment = 1114362743U,
			// Token: 0x04001011 RID: 4113
			descMeetingSuggestionsInvalidTimeInterval = 3835343804U,
			// Token: 0x04001012 RID: 4114
			KeywordStatsNotRequested = 2769462193U,
			// Token: 0x04001013 RID: 4115
			RbacTargetMailboxAuthorizationError = 1382860876U,
			// Token: 0x04001014 RID: 4116
			SortedResultNullParameters = 636866827U,
			// Token: 0x04001015 RID: 4117
			LogFieldsLastEndTime = 1214298741U,
			// Token: 0x04001016 RID: 4118
			TrackingErrorInvalidADData = 55350379U,
			// Token: 0x04001017 RID: 4119
			LogFieldsResume = 3068064914U,
			// Token: 0x04001018 RID: 4120
			descWorkHoursStartEndInvalid = 3770945073U,
			// Token: 0x04001019 RID: 4121
			SearchAlreadStarted = 872236584U,
			// Token: 0x0400101A RID: 4122
			MessageInvalidTimeZoneReferenceToPeriod = 908708480U,
			// Token: 0x0400101B RID: 4123
			ProgressSearchingInProgress = 489766921U,
			// Token: 0x0400101C RID: 4124
			descCorruptUserOofSettingsXmlDocument = 3558390196U,
			// Token: 0x0400101D RID: 4125
			CrossForestNotSupported = 1295345518U,
			// Token: 0x0400101E RID: 4126
			LogFieldsNumberSuccessfulMailboxes = 3240652606U,
			// Token: 0x0400101F RID: 4127
			descIdentityArrayEmpty = 1087048587U,
			// Token: 0x04001020 RID: 4128
			MessageInvalidTimeZoneReferenceToGroup = 2265146620U,
			// Token: 0x04001021 RID: 4129
			descInvalidTimeZoneBias = 1894419184U,
			// Token: 0x04001022 RID: 4130
			descPublicFolderServerNotFound = 1223102710U,
			// Token: 0x04001023 RID: 4131
			TrackingErrorCrossPremiseAuthentication = 2319913820U,
			// Token: 0x04001024 RID: 4132
			MessageInvalidTimeZonePeriodNullId = 3558532322U,
			// Token: 0x04001025 RID: 4133
			StorePermanantError = 147513433U,
			// Token: 0x04001026 RID: 4134
			TrackingErrorTransientUnexpected = 3376565836U,
			// Token: 0x04001027 RID: 4135
			TrackingErrorModerationDecisionLogsFromE14Rtm = 1389565281U,
			// Token: 0x04001028 RID: 4136
			TrackingErrorCASUriDiscovery = 3931032456U,
			// Token: 0x04001029 RID: 4137
			descFailedToFindPublicFolderServer = 2307745798U,
			// Token: 0x0400102A RID: 4138
			MessageTrackingApplicationName = 1664401949U,
			// Token: 0x0400102B RID: 4139
			descAutoDiscoverThruDirectoryFailed = 3586747816U,
			// Token: 0x0400102C RID: 4140
			MessageInvalidTimeZoneDuplicatePeriods = 468011246U,
			// Token: 0x0400102D RID: 4141
			LogFieldsPercentComplete = 1053420363U,
			// Token: 0x0400102E RID: 4142
			descFreeBusyAndSuggestionsNull = 863256271U,
			// Token: 0x0400102F RID: 4143
			LogFieldsSuccessfulMailboxes = 3837654127U,
			// Token: 0x04001030 RID: 4144
			UninstallAssistantsServiceTask = 534171625U,
			// Token: 0x04001031 RID: 4145
			ProgressSearching = 2865084951U,
			// Token: 0x04001032 RID: 4146
			SearchInvalidPagination = 193137347U,
			// Token: 0x04001033 RID: 4147
			LogFieldsSearchQuery = 2006533237U,
			// Token: 0x04001034 RID: 4148
			TrackingTotalBudgetExceeded = 1759034327U,
			// Token: 0x04001035 RID: 4149
			descSuggestionMustStartOnThirtyMinuteBoundary = 2697293287U,
			// Token: 0x04001036 RID: 4150
			SearchServerShutdown = 3124261155U,
			// Token: 0x04001037 RID: 4151
			descFailedToCreateLegacyOofRule = 4107635786U,
			// Token: 0x04001038 RID: 4152
			PhotosApplicationName = 4123771088U,
			// Token: 0x04001039 RID: 4153
			ADUserNotFoundException = 1880771860U,
			// Token: 0x0400103A RID: 4154
			ProgressSearchingSources = 4285214785U,
			// Token: 0x0400103B RID: 4155
			LogMailNone = 2804157263U,
			// Token: 0x0400103C RID: 4156
			LogFieldsStatusMailRecipients = 1233757588U,
			// Token: 0x0400103D RID: 4157
			LogFieldsResultNumber = 3305370967U,
			// Token: 0x0400103E RID: 4158
			TrackingErrorPermanentUnexpected = 4287340460U,
			// Token: 0x0400103F RID: 4159
			ProgressCompleting = 1954290077U,
			// Token: 0x04001040 RID: 4160
			descInvalidUserOofSettings = 270334674U,
			// Token: 0x04001041 RID: 4161
			LogFieldsResultNumberEstimate = 1275939085U,
			// Token: 0x04001042 RID: 4162
			LogFieldsKeywordHits = 3222940042U,
			// Token: 0x04001043 RID: 4163
			UnknownError = 3351215994U,
			// Token: 0x04001044 RID: 4164
			MessageInvalidTimeZonePeriodNullBias = 1265936670U,
			// Token: 0x04001045 RID: 4165
			MailboxSeachCountIncludeUnsearchable = 2937454784U,
			// Token: 0x04001046 RID: 4166
			LogMailBlank = 859087807U,
			// Token: 0x04001047 RID: 4167
			descWorkHoursEndTimeInvalid = 3091073294U,
			// Token: 0x04001048 RID: 4168
			ProgessCreatingThreads = 2858750991U,
			// Token: 0x04001049 RID: 4169
			descOOFCannotReadExternalOofOptions = 2569697819U,
			// Token: 0x0400104A RID: 4170
			PrimaryMailbox = 1910512436U,
			// Token: 0x0400104B RID: 4171
			MessageInvalidTimeZoneCustomTimeZoneThreeElements = 2852570616U,
			// Token: 0x0400104C RID: 4172
			TrackingTransientError = 763976563U,
			// Token: 0x0400104D RID: 4173
			LogFieldsLastStartTime = 1809134602U,
			// Token: 0x0400104E RID: 4174
			CopyItemsFailed = 1824636832U,
			// Token: 0x0400104F RID: 4175
			DummyApplicationName = 1910425077U,
			// Token: 0x04001050 RID: 4176
			MessageInvalidTimeZoneDuplicateGroups = 3276944824U,
			// Token: 0x04001051 RID: 4177
			ExecutingUserNeedSmtpAddress = 1614878877U,
			// Token: 0x04001052 RID: 4178
			MessageInvalidTimeZoneMoreThanTwoPeriodsUnsupported = 3442221872U,
			// Token: 0x04001053 RID: 4179
			descMailRecipientNotFound = 3955434964U,
			// Token: 0x04001054 RID: 4180
			descNullUserName = 2671831934U,
			// Token: 0x04001055 RID: 4181
			NonNegativeParameter = 1407995413U,
			// Token: 0x04001056 RID: 4182
			LogFieldsEstimateNotExcludeDuplicates = 1633879312U,
			// Token: 0x04001057 RID: 4183
			StoreTransientError = 18603439U,
			// Token: 0x04001058 RID: 4184
			LogFieldsLogLevel = 1541817855U,
			// Token: 0x04001059 RID: 4185
			TrackingWSRequestCorrupt = 2131297471U,
			// Token: 0x0400105A RID: 4186
			SearchDisabled = 2936774726U,
			// Token: 0x0400105B RID: 4187
			descNullCredentialsToServiceDiscoveryRequest = 1705124535U,
			// Token: 0x0400105C RID: 4188
			LogFieldsUnsuccessfulMailboxes = 697045372U,
			// Token: 0x0400105D RID: 4189
			MessageInvalidTimeZoneTimeZoneNotFound = 2530022313U,
			// Token: 0x0400105E RID: 4190
			MessageInvalidTimeZoneIntValueIsInvalid = 3753969000U,
			// Token: 0x0400105F RID: 4191
			ScheduleConfigurationSchedule = 3488925402U,
			// Token: 0x04001060 RID: 4192
			descOofRuleSaveException = 3675304455U,
			// Token: 0x04001061 RID: 4193
			LogFieldsTargetMailbox = 3342799224U,
			// Token: 0x04001062 RID: 4194
			descInvalidSuggestionsTimeRange = 1441197009U,
			// Token: 0x04001063 RID: 4195
			descFailedToGetUserOofPolicy = 670434636U,
			// Token: 0x04001064 RID: 4196
			descMeetingSuggestionsDurationTooLarge = 983402700U,
			// Token: 0x04001065 RID: 4197
			ProgressOpening = 2411109197U,
			// Token: 0x04001066 RID: 4198
			TrackingErrorBudgetExceededMultiMessageSearch = 1471504673U,
			// Token: 0x04001067 RID: 4199
			descLocalServerObjectNotFound = 2991551487U,
			// Token: 0x04001068 RID: 4200
			RecoverableItems = 2706524390U,
			// Token: 0x04001069 RID: 4201
			descProxyRequestFailed = 3367175223U,
			// Token: 0x0400106A RID: 4202
			ProgessCreating = 516398220U,
			// Token: 0x0400106B RID: 4203
			descClientDisconnected = 924370705U,
			// Token: 0x0400106C RID: 4204
			SearchThrottled = 3760653578U,
			// Token: 0x0400106D RID: 4205
			descInvalidTransitionTime = 1620607520U,
			// Token: 0x0400106E RID: 4206
			TargetMailboxOutOfSpace = 2976781692U,
			// Token: 0x0400106F RID: 4207
			LogFieldsLastRunBy = 1848520589U,
			// Token: 0x04001070 RID: 4208
			UnableToReadServiceTopology = 4135638738U,
			// Token: 0x04001071 RID: 4209
			descElcRootFolderName = 607437832U,
			// Token: 0x04001072 RID: 4210
			descNoMailTipsInEwsResponseMessage = 647128549U,
			// Token: 0x04001073 RID: 4211
			descFailedToGetRules = 4209067056U,
			// Token: 0x04001074 RID: 4212
			LogFieldsStoppedBy = 794348359U,
			// Token: 0x04001075 RID: 4213
			descProxyRequestProcessingSocketError = 2268440642U,
			// Token: 0x04001076 RID: 4214
			descProxyRequestNotAllowed = 3466285021U,
			// Token: 0x04001077 RID: 4215
			descInvalidAccessLevel = 1848500058U,
			// Token: 0x04001078 RID: 4216
			TrackingPermanentErrorMultiMessageSearch = 784192305U,
			// Token: 0x04001079 RID: 4217
			LogFieldsCreatedBy = 2804255286U,
			// Token: 0x0400107A RID: 4218
			LowSystemResource = 1225651387U,
			// Token: 0x0400107B RID: 4219
			TrackingErrorLogSearchServiceDown = 854567314U,
			// Token: 0x0400107C RID: 4220
			LogMailNotApplicable = 2312386357U,
			// Token: 0x0400107D RID: 4221
			descNoFreeBusyAccess = 3289802537U,
			// Token: 0x0400107E RID: 4222
			ADUserMisconfiguredException = 348874544U,
			// Token: 0x0400107F RID: 4223
			ProgressOpeningTarget = 2033796534U,
			// Token: 0x04001080 RID: 4224
			descOOFVirusDetectedOofReplyMessage = 1554201361U,
			// Token: 0x04001081 RID: 4225
			MessageInvalidTimeZonePeriodNullName = 2912574056U,
			// Token: 0x04001082 RID: 4226
			MessageInvalidTimeZoneFirstTransition = 3332140560U,
			// Token: 0x04001083 RID: 4227
			InvalidContactException = 3216622764U,
			// Token: 0x04001084 RID: 4228
			ErrorRemoveOngoingSearch = 3436010521U,
			// Token: 0x04001085 RID: 4229
			TrackingErrorReadStatus = 1594184545U,
			// Token: 0x04001086 RID: 4230
			TrackingErrorCrossForestAuthentication = 3134958540U,
			// Token: 0x04001087 RID: 4231
			descInvalidFreeBusyViewType = 1892050364U,
			// Token: 0x04001088 RID: 4232
			descLogonAsNetworkServiceFailed = 3341082622U,
			// Token: 0x04001089 RID: 4233
			TrackingErrorCrossForestMisconfiguration = 2837247303U,
			// Token: 0x0400108A RID: 4234
			MessageInvalidTimeZoneMissedGroup = 2100288003U,
			// Token: 0x0400108B RID: 4235
			NoKeywordStatsForCopySearch = 478692077U,
			// Token: 0x0400108C RID: 4236
			descMailboxFailover = 3248017497U,
			// Token: 0x0400108D RID: 4237
			LogFieldsExcludeDuplicateMessages = 1104837598U,
			// Token: 0x0400108E RID: 4238
			LogFieldsErrors = 1743551038U,
			// Token: 0x0400108F RID: 4239
			mailTipsTenant = 3607788283U,
			// Token: 0x04001090 RID: 4240
			SearchArgument = 3153221581U,
			// Token: 0x04001091 RID: 4241
			TrackingErrorCrossPremiseMisconfigurationMultiMessageSearch = 3176897035U,
			// Token: 0x04001092 RID: 4242
			descNullEmailToAutoDiscoverRequest = 2497578736U
		}

		// Token: 0x02000320 RID: 800
		private enum ParamIDs
		{
			// Token: 0x04001094 RID: 4244
			SearchServerFailed,
			// Token: 0x04001095 RID: 4245
			descElcCannotFindDefaultFolder,
			// Token: 0x04001096 RID: 4246
			LogMailHeaderInstructions,
			// Token: 0x04001097 RID: 4247
			descPublicFolderRequestProcessingError,
			// Token: 0x04001098 RID: 4248
			descConfigurationInformationNotFound,
			// Token: 0x04001099 RID: 4249
			InvalidReferenceItemInPreviewResult,
			// Token: 0x0400109A RID: 4250
			descVirusScanInProgress,
			// Token: 0x0400109B RID: 4251
			InconsistentSortedResults,
			// Token: 0x0400109C RID: 4252
			InvalidSortedResultParameter,
			// Token: 0x0400109D RID: 4253
			InvalidOwaUrlInPreviewResult,
			// Token: 0x0400109E RID: 4254
			DeleteItemFailedForMessage,
			// Token: 0x0400109F RID: 4255
			descMissingMailboxArrayElement,
			// Token: 0x040010A0 RID: 4256
			descMisconfiguredOrganizationRelationship,
			// Token: 0x040010A1 RID: 4257
			descNotAGroupOrUserOrContact,
			// Token: 0x040010A2 RID: 4258
			descProxyForPersonalNotAllowed,
			// Token: 0x040010A3 RID: 4259
			UnknownDayOfWeek,
			// Token: 0x040010A4 RID: 4260
			descInvalidMonth,
			// Token: 0x040010A5 RID: 4261
			InvalidKeywordStatsRequest,
			// Token: 0x040010A6 RID: 4262
			PhotoRetrievalFailedUnauthorizedAccessError,
			// Token: 0x040010A7 RID: 4263
			ArgumentValidationFailedException,
			// Token: 0x040010A8 RID: 4264
			descDeliveryRestricted,
			// Token: 0x040010A9 RID: 4265
			UnknownRecurrenceRange,
			// Token: 0x040010AA RID: 4266
			LogMailHeader,
			// Token: 0x040010AB RID: 4267
			descMailTipsSenderNotUnique,
			// Token: 0x040010AC RID: 4268
			InvalidFailedMailboxesResultDuplicateEntries,
			// Token: 0x040010AD RID: 4269
			RbacSourceMailboxAuthorizationError,
			// Token: 0x040010AE RID: 4270
			descUnknownDefFolder,
			// Token: 0x040010AF RID: 4271
			descProxyRequestProcessingError,
			// Token: 0x040010B0 RID: 4272
			descSoapAutoDiscoverRequestUserSettingError,
			// Token: 0x040010B1 RID: 4273
			descFailedToCreateELCRoot,
			// Token: 0x040010B2 RID: 4274
			descElcFolderExists,
			// Token: 0x040010B3 RID: 4275
			descFailedToCreateOneOrMoreOrganizationalFolders,
			// Token: 0x040010B4 RID: 4276
			SearchServerError,
			// Token: 0x040010B5 RID: 4277
			InvalidItemHashInPreviewResult,
			// Token: 0x040010B6 RID: 4278
			descFailedToSyncFolder,
			// Token: 0x040010B7 RID: 4279
			MailboxRefinerInRefinersSection,
			// Token: 0x040010B8 RID: 4280
			descRequestStreamTooBig,
			// Token: 0x040010B9 RID: 4281
			descProtocolNotFoundInAutoDiscoverResponse,
			// Token: 0x040010BA RID: 4282
			UnableToFindSearchObject,
			// Token: 0x040010BB RID: 4283
			descMinimumRequiredVersionProxyServerNotFound,
			// Token: 0x040010BC RID: 4284
			descIndividualMailboxLimitReached,
			// Token: 0x040010BD RID: 4285
			descTagNotInAD,
			// Token: 0x040010BE RID: 4286
			descAvailabilityAddressSpaceFailed,
			// Token: 0x040010BF RID: 4287
			descUnsupportedSecurityDescriptorVersion,
			// Token: 0x040010C0 RID: 4288
			SearchMailboxNotFound,
			// Token: 0x040010C1 RID: 4289
			SearchTaskCancelledPrimary,
			// Token: 0x040010C2 RID: 4290
			descCannotResolveEmailAddress,
			// Token: 0x040010C3 RID: 4291
			descMissingProperty,
			// Token: 0x040010C4 RID: 4292
			descAutoDiscoverFailed,
			// Token: 0x040010C5 RID: 4293
			UnabledToLocateMailboxServer,
			// Token: 0x040010C6 RID: 4294
			ArchiveSearchPopulationFailed,
			// Token: 0x040010C7 RID: 4295
			InvalidSearchRequest,
			// Token: 0x040010C8 RID: 4296
			DiscoverySearchCIFailure,
			// Token: 0x040010C9 RID: 4297
			descInvalidTargetAddress,
			// Token: 0x040010CA RID: 4298
			descCannotBindToElcRootFolder,
			// Token: 0x040010CB RID: 4299
			descSoapAutoDiscoverRequestError,
			// Token: 0x040010CC RID: 4300
			descAutoDiscoverRequestError,
			// Token: 0x040010CD RID: 4301
			SearchOverBudget,
			// Token: 0x040010CE RID: 4302
			descUnknownWebRequestType,
			// Token: 0x040010CF RID: 4303
			descIdentityArrayTooBig,
			// Token: 0x040010D0 RID: 4304
			TrackingLogsCorruptOnServer,
			// Token: 0x040010D1 RID: 4305
			descExceededMaxRedirectionDepth,
			// Token: 0x040010D2 RID: 4306
			SearchNonFullTextSortByProperty,
			// Token: 0x040010D3 RID: 4307
			descCrossForestServiceMissing,
			// Token: 0x040010D4 RID: 4308
			CrossServerCallFailed,
			// Token: 0x040010D5 RID: 4309
			PrimarySearchFolderTimeout,
			// Token: 0x040010D6 RID: 4310
			TrackingErrorUserObjectCorrupt,
			// Token: 0x040010D7 RID: 4311
			descVirusDetected,
			// Token: 0x040010D8 RID: 4312
			FailedToConvertSearchCriteriaToRestriction,
			// Token: 0x040010D9 RID: 4313
			PrimarySearchPopulationFailed,
			// Token: 0x040010DA RID: 4314
			KeywordStatisticsSearchDisabled,
			// Token: 0x040010DB RID: 4315
			descInvalidTypeInBookingPolicyConfig,
			// Token: 0x040010DC RID: 4316
			EmptyMailboxStatsServerResponse,
			// Token: 0x040010DD RID: 4317
			PreviewSearchDisabled,
			// Token: 0x040010DE RID: 4318
			descFailedToCreateOrganizationalFolder,
			// Token: 0x040010DF RID: 4319
			ArchiveSearchFolderTimeout,
			// Token: 0x040010E0 RID: 4320
			descSoapAutoDiscoverRequestUserSettingInvalidError,
			// Token: 0x040010E1 RID: 4321
			descInvalidSmtpAddress,
			// Token: 0x040010E2 RID: 4322
			DiscoverySearchAborted,
			// Token: 0x040010E3 RID: 4323
			descDateMustHaveZeroTimeSpan,
			// Token: 0x040010E4 RID: 4324
			SearchServerErrorMessage,
			// Token: 0x040010E5 RID: 4325
			SearchInvalidSortSpecification,
			// Token: 0x040010E6 RID: 4326
			InvalidRecipientArrayInPreviewResult,
			// Token: 0x040010E7 RID: 4327
			SearchTooManyMailboxes,
			// Token: 0x040010E8 RID: 4328
			InvalidPreviewSearchResults,
			// Token: 0x040010E9 RID: 4329
			descRecipientVersionNotSupported,
			// Token: 0x040010EA RID: 4330
			SearchTaskTimeoutArchive,
			// Token: 0x040010EB RID: 4331
			descInvalidGoodThreshold,
			// Token: 0x040010EC RID: 4332
			descNotAValidExchangePrincipal,
			// Token: 0x040010ED RID: 4333
			descMisconfiguredIntraOrganizationConnector,
			// Token: 0x040010EE RID: 4334
			descSoapAutoDiscoverInvalidResponseError,
			// Token: 0x040010EF RID: 4335
			EmptyRefinerServerResponse,
			// Token: 0x040010F0 RID: 4336
			descProxyNoResultError,
			// Token: 0x040010F1 RID: 4337
			UnknownRecurrencePattern,
			// Token: 0x040010F2 RID: 4338
			descEmptySecurityDescriptor,
			// Token: 0x040010F3 RID: 4339
			CreateFolderFailed,
			// Token: 0x040010F4 RID: 4340
			SearchWorkerError,
			// Token: 0x040010F5 RID: 4341
			descAutoDiscoverFailedWithException,
			// Token: 0x040010F6 RID: 4342
			SearchTransientError,
			// Token: 0x040010F7 RID: 4343
			descTimeIntervalTooBig,
			// Token: 0x040010F8 RID: 4344
			descMissingIntraforestCAS,
			// Token: 0x040010F9 RID: 4345
			InvalidFailedMailboxesResultWebServiceResponse,
			// Token: 0x040010FA RID: 4346
			SearchAdminRpcInvalidQuery,
			// Token: 0x040010FB RID: 4347
			MaxAllowedKeywordsExceeded,
			// Token: 0x040010FC RID: 4348
			SortIComparableTypeException,
			// Token: 0x040010FD RID: 4349
			SourceMailboxUserNotFoundInAD,
			// Token: 0x040010FE RID: 4350
			descProxyRemoteServerError,
			// Token: 0x040010FF RID: 4351
			descInvalidTimeInterval,
			// Token: 0x04001100 RID: 4352
			TargetFolderNotFound,
			// Token: 0x04001101 RID: 4353
			descCannotFindOrganizationalFolderInMailbox,
			// Token: 0x04001102 RID: 4354
			UnknownBodyFormat,
			// Token: 0x04001103 RID: 4355
			SearchFolderTimeout,
			// Token: 0x04001104 RID: 4356
			LogMailSimpleHeader,
			// Token: 0x04001105 RID: 4357
			InvalidIdInPreviewResult,
			// Token: 0x04001106 RID: 4358
			CorruptedFolder,
			// Token: 0x04001107 RID: 4359
			descUnsupportedSecurityDescriptorHeader,
			// Token: 0x04001108 RID: 4360
			OWAServiceUrlFailure,
			// Token: 0x04001109 RID: 4361
			SearchAdminRpcCallMaxSearches,
			// Token: 0x0400110A RID: 4362
			descInvalidDayOrder,
			// Token: 0x0400110B RID: 4363
			SearchAdminRpcCallAccessDenied,
			// Token: 0x0400110C RID: 4364
			InvalidPreviewResultWebServiceResponse,
			// Token: 0x0400110D RID: 4365
			descOnlyDefaultFreeBusyIntervalSupported,
			// Token: 0x0400110E RID: 4366
			descResultSetTooBig,
			// Token: 0x0400110F RID: 4367
			descTimeoutExpired,
			// Token: 0x04001110 RID: 4368
			RefinerValueNullOrCountZero,
			// Token: 0x04001111 RID: 4369
			descNotAContactOrUser,
			// Token: 0x04001112 RID: 4370
			descFreeBusyDLLimitReached,
			// Token: 0x04001113 RID: 4371
			descFailedToFindElcRoot,
			// Token: 0x04001114 RID: 4372
			FailedToFetchPreviewItems,
			// Token: 0x04001115 RID: 4373
			descInvalidMergedFreeBusyInterval,
			// Token: 0x04001116 RID: 4374
			UnknownRecurrenceOrderType,
			// Token: 0x04001117 RID: 4375
			descFailedToGetOrganizationalFoldersForMailbox,
			// Token: 0x04001118 RID: 4376
			descElcNoMatchingOrgFolder,
			// Token: 0x04001119 RID: 4377
			descInvalidTransitionBias,
			// Token: 0x0400111A RID: 4378
			SearchUserNotFound,
			// Token: 0x0400111B RID: 4379
			PhotoRetrievalFailedIOError,
			// Token: 0x0400111C RID: 4380
			DatabaseLocationUnavailable,
			// Token: 0x0400111D RID: 4381
			SearchTooManyKeywords,
			// Token: 0x0400111E RID: 4382
			descFailedToCreateElcRootRetry,
			// Token: 0x0400111F RID: 4383
			SearchAdminRpcSearchCallTimedout,
			// Token: 0x04001120 RID: 4384
			TrackingRpcError,
			// Token: 0x04001121 RID: 4385
			SearchNonFullTextPaginationProperty,
			// Token: 0x04001122 RID: 4386
			descPFNotSupported,
			// Token: 0x04001123 RID: 4387
			NestedFanout,
			// Token: 0x04001124 RID: 4388
			descMailTipsSenderNotFound,
			// Token: 0x04001125 RID: 4389
			CISearchFailed,
			// Token: 0x04001126 RID: 4390
			CouldNotFindOrgRelationship,
			// Token: 0x04001127 RID: 4391
			descInvalidYear,
			// Token: 0x04001128 RID: 4392
			InvalidMailboxInMailboxStatistics,
			// Token: 0x04001129 RID: 4393
			descAutoDiscoverBadRedirectLocation,
			// Token: 0x0400112A RID: 4394
			SearchTaskTimeoutPrimary,
			// Token: 0x0400112B RID: 4395
			SourceMailboxVersionError,
			// Token: 0x0400112C RID: 4396
			InsufficientSpaceOnTargetMailbox,
			// Token: 0x0400112D RID: 4397
			PhotoRetrievalFailedWin32Error,
			// Token: 0x0400112E RID: 4398
			descSoapAutoDiscoverResponseError,
			// Token: 0x0400112F RID: 4399
			descErrorEwsResponse,
			// Token: 0x04001130 RID: 4400
			descInvalidConfigForCrossForestRequest,
			// Token: 0x04001131 RID: 4401
			SearchAdminRpcCallFailed,
			// Token: 0x04001132 RID: 4402
			SearchTaskCancelled,
			// Token: 0x04001133 RID: 4403
			descMissingArgument,
			// Token: 0x04001134 RID: 4404
			SearchTaskCancelledArchive,
			// Token: 0x04001135 RID: 4405
			FailedToGetItem,
			// Token: 0x04001136 RID: 4406
			descProxyRequestProcessingIOError,
			// Token: 0x04001137 RID: 4407
			descInputFolderNamesContainDuplicates,
			// Token: 0x04001138 RID: 4408
			RangedParameter,
			// Token: 0x04001139 RID: 4409
			descPrimaryDefaultCorrupted,
			// Token: 0x0400113A RID: 4410
			InvalidUnknownMailboxInPreviewResult,
			// Token: 0x0400113B RID: 4411
			SourceMailboxCrossPremiseError,
			// Token: 0x0400113C RID: 4412
			descE14orHigherProxyServerNotFound,
			// Token: 0x0400113D RID: 4413
			RemoteMailbox
		}
	}
}
