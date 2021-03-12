using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000002 RID: 2
	public static class ClientStrings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static ClientStrings()
		{
			ClientStrings.stringIDs.Add(4109493280U, "PostedTo");
			ClientStrings.stringIDs.Add(3201544700U, "UnknownDelegateUser");
			ClientStrings.stringIDs.Add(2520011618U, "ClutterNotificationInvitationIfYouDontLikeIt");
			ClientStrings.stringIDs.Add(2979702410U, "Inbox");
			ClientStrings.stringIDs.Add(2660925179U, "CategoryKqlKeyword");
			ClientStrings.stringIDs.Add(2842725608U, "RequestedActionReply");
			ClientStrings.stringIDs.Add(699667745U, "ClutterNotificationAutoEnablementNoticeHeader");
			ClientStrings.stringIDs.Add(605024017U, "MeetingTentative");
			ClientStrings.stringIDs.Add(3115038347U, "BodyKqlKeyword");
			ClientStrings.stringIDs.Add(2958797346U, "WhenAllDays");
			ClientStrings.stringIDs.Add(1567502238U, "GroupMailboxWelcomeEmailFooterUnsubscribeLinkText");
			ClientStrings.stringIDs.Add(800057761U, "TaskWhenDailyRegeneratingPattern");
			ClientStrings.stringIDs.Add(1082093008U, "UpdateRumLocationFlag");
			ClientStrings.stringIDs.Add(3496891301U, "CcColon");
			ClientStrings.stringIDs.Add(3728294467U, "GroupMailboxWelcomeEmailUnsubscribeToInboxTitle");
			ClientStrings.stringIDs.Add(729677225U, "HighKqlKeyword");
			ClientStrings.stringIDs.Add(2868846894U, "OnBehalfOf");
			ClientStrings.stringIDs.Add(3636275243U, "SentKqlKeyword");
			ClientStrings.stringIDs.Add(996097864U, "UpdateRumCancellationFlag");
			ClientStrings.stringIDs.Add(2420384142U, "PrivateAppointmentSubject");
			ClientStrings.stringIDs.Add(2485040215U, "GroupMailboxWelcomeMessageHeader2");
			ClientStrings.stringIDs.Add(2052801377U, "Busy");
			ClientStrings.stringIDs.Add(2694011047U, "CalendarWhenEveryOtherDay");
			ClientStrings.stringIDs.Add(2530535283U, "WhenWeekDays");
			ClientStrings.stringIDs.Add(2983597720U, "WhenOnEveryDayOfTheWeek");
			ClientStrings.stringIDs.Add(186518193U, "ClutterNotificationAutoEnablementNoticeHowItWorks");
			ClientStrings.stringIDs.Add(1289378056U, "RequestedActionRead");
			ClientStrings.stringIDs.Add(3076491009U, "GroupMailboxWelcomeEmailPublicTypeText");
			ClientStrings.stringIDs.Add(4206219086U, "PolicyTipDefaultMessageRejectOverride");
			ClientStrings.stringIDs.Add(3798065398U, "WhenLast");
			ClientStrings.stringIDs.Add(920038251U, "GroupMailboxWelcomeEmailUnsubscribeToInboxSubtitle");
			ClientStrings.stringIDs.Add(3798064771U, "WhenPart");
			ClientStrings.stringIDs.Add(998340857U, "GroupMailboxAddedMemberMessageDocument2");
			ClientStrings.stringIDs.Add(3514687136U, "UpdateRumStartTimeFlag");
			ClientStrings.stringIDs.Add(3831476238U, "MyContactsFolderName");
			ClientStrings.stringIDs.Add(2537597608U, "DocumentSyncIssues");
			ClientStrings.stringIDs.Add(2892702617U, "MeetingAccept");
			ClientStrings.stringIDs.Add(3827225687U, "SearchFolders");
			ClientStrings.stringIDs.Add(77678270U, "OOF");
			ClientStrings.stringIDs.Add(28323196U, "GroupMailboxWelcomeEmailFooterUnsubscribeDescirptionText");
			ClientStrings.stringIDs.Add(3435327809U, "GroupMailboxWelcomeEmailShareFilesSubTitle");
			ClientStrings.stringIDs.Add(2986714461U, "GroupMailboxWelcomeEmailPrivateTypeText");
			ClientStrings.stringIDs.Add(4003125105U, "GroupMailboxWelcomeEmailFooterSubscribeLinkText");
			ClientStrings.stringIDs.Add(295620541U, "SentColon");
			ClientStrings.stringIDs.Add(109016526U, "ClutterNotificationO365Closing");
			ClientStrings.stringIDs.Add(606093953U, "MyCalendars");
			ClientStrings.stringIDs.Add(2337243514U, "AttachmentNamesKqlKeyword");
			ClientStrings.stringIDs.Add(3055530828U, "UMFaxFolderName");
			ClientStrings.stringIDs.Add(4038690134U, "GroupMailboxSuggestToSubscribe");
			ClientStrings.stringIDs.Add(3709219576U, "WherePart");
			ClientStrings.stringIDs.Add(2985148172U, "MeetingProposedNewTime");
			ClientStrings.stringIDs.Add(2966158940U, "Tasks");
			ClientStrings.stringIDs.Add(2957657555U, "ClutterNotificationAutoEnablementNoticeWeCallIt");
			ClientStrings.stringIDs.Add(2243726652U, "Configuration");
			ClientStrings.stringIDs.Add(2687792634U, "ReceivedKqlKeyword");
			ClientStrings.stringIDs.Add(2676822854U, "ItemForward");
			ClientStrings.stringIDs.Add(4037918816U, "PublicFolderMailboxHierarchyInfo");
			ClientStrings.stringIDs.Add(998340860U, "GroupMailboxAddedMemberMessageDocument1");
			ClientStrings.stringIDs.Add(1139539543U, "GroupMailboxAddedSelfMessageHeader");
			ClientStrings.stringIDs.Add(627342781U, "GroupSubscriptionPageSubscribedInfo");
			ClientStrings.stringIDs.Add(2377878649U, "TaskWhenMonthlyRegeneratingPattern");
			ClientStrings.stringIDs.Add(2677919833U, "SentTime");
			ClientStrings.stringIDs.Add(590977256U, "SentItems");
			ClientStrings.stringIDs.Add(931240115U, "UpdateRumDuplicateFlags");
			ClientStrings.stringIDs.Add(3416726118U, "ToDoSearch");
			ClientStrings.stringIDs.Add(831219486U, "GroupMailboxWelcomeEmailO365FooterBrowseViewCalendar");
			ClientStrings.stringIDs.Add(586169060U, "UserPhotoNotAnImage");
			ClientStrings.stringIDs.Add(51726234U, "OofReply");
			ClientStrings.stringIDs.Add(2992898169U, "UpdateRumDescription");
			ClientStrings.stringIDs.Add(1190007884U, "SharingRequestDenied");
			ClientStrings.stringIDs.Add(3198424631U, "CommonViews");
			ClientStrings.stringIDs.Add(257954985U, "TaskWhenDailyEveryDay");
			ClientStrings.stringIDs.Add(2561496264U, "NoDataAvailable");
			ClientStrings.stringIDs.Add(3430669166U, "Root");
			ClientStrings.stringIDs.Add(3011322582U, "KoreanLunar");
			ClientStrings.stringIDs.Add(3694564633U, "SyncIssues");
			ClientStrings.stringIDs.Add(2696977948U, "UnifiedInbox");
			ClientStrings.stringIDs.Add(843955115U, "ContactSubjectFormat");
			ClientStrings.stringIDs.Add(2093606811U, "ChineseLunar");
			ClientStrings.stringIDs.Add(629464291U, "Outbox");
			ClientStrings.stringIDs.Add(2692137911U, "UpdateRumInconsistencyFlagsLabel");
			ClientStrings.stringIDs.Add(2173140516U, "Hijri");
			ClientStrings.stringIDs.Add(946577161U, "MeetingsKqlKeyword");
			ClientStrings.stringIDs.Add(2717587388U, "ClutterNotificationInvitationHowItWorks");
			ClientStrings.stringIDs.Add(998340858U, "GroupMailboxAddedMemberMessageDocument3");
			ClientStrings.stringIDs.Add(732114395U, "UpdateRumRecurrenceFlag");
			ClientStrings.stringIDs.Add(1261883245U, "PolicyTagKqlKeyword");
			ClientStrings.stringIDs.Add(3061781790U, "GroupSubscriptionPageBodyFont");
			ClientStrings.stringIDs.Add(780435470U, "ClutterNotificationOptInSubject");
			ClientStrings.stringIDs.Add(2831541605U, "ClutterNotificationAutoEnablementNoticeItsAutomatic");
			ClientStrings.stringIDs.Add(1987690819U, "WorkingElsewhere");
			ClientStrings.stringIDs.Add(1210128587U, "NTUser");
			ClientStrings.stringIDs.Add(3623590716U, "ElcRoot");
			ClientStrings.stringIDs.Add(3637574683U, "ContactFullNameFormat");
			ClientStrings.stringIDs.Add(2442729051U, "GroupMailboxAddedMemberMessageCalendar1");
			ClientStrings.stringIDs.Add(3064946485U, "WhenFifth");
			ClientStrings.stringIDs.Add(690865655U, "ClutterNotificationInvitationIntro");
			ClientStrings.stringIDs.Add(725603143U, "ExpiresKqlKeyword");
			ClientStrings.stringIDs.Add(1486137843U, "GroupMailboxAddedMemberMessageConversation2");
			ClientStrings.stringIDs.Add(462355316U, "CommunicatorHistoryFolderName");
			ClientStrings.stringIDs.Add(898888055U, "LowKqlKeyword");
			ClientStrings.stringIDs.Add(3493739462U, "AllKqlKeyword");
			ClientStrings.stringIDs.Add(3753304505U, "GroupMailboxAddedMemberToPublicGroupMessage");
			ClientStrings.stringIDs.Add(1712196986U, "Followup");
			ClientStrings.stringIDs.Add(1669914831U, "HebrewLunar");
			ClientStrings.stringIDs.Add(1601836855U, "Notes");
			ClientStrings.stringIDs.Add(4285257241U, "UpdateRumEndTimeFlag");
			ClientStrings.stringIDs.Add(4086680553U, "JournalsKqlKeyword");
			ClientStrings.stringIDs.Add(2395883303U, "AttendeeInquiryRumDescription");
			ClientStrings.stringIDs.Add(1358110507U, "ClutterNotificationOptInHeader");
			ClientStrings.stringIDs.Add(2274376870U, "LegacyPDLFakeEntry");
			ClientStrings.stringIDs.Add(2426305242U, "PolicyTipDefaultMessageReject");
			ClientStrings.stringIDs.Add(637088748U, "VoicemailKqlKeyword");
			ClientStrings.stringIDs.Add(2543409328U, "PostedOn");
			ClientStrings.stringIDs.Add(20079527U, "MeetingCancel");
			ClientStrings.stringIDs.Add(2918743951U, "FromColon");
			ClientStrings.stringIDs.Add(3613623199U, "DeletedItems");
			ClientStrings.stringIDs.Add(242457402U, "DocsKqlKeyword");
			ClientStrings.stringIDs.Add(3850103120U, "ClutterNotificationO365DisplayName");
			ClientStrings.stringIDs.Add(2773211886U, "LocalFailures");
			ClientStrings.stringIDs.Add(3659541315U, "GroupMailboxWelcomeEmailO365FooterBrowseConversations");
			ClientStrings.stringIDs.Add(2676513095U, "JapaneseLunar");
			ClientStrings.stringIDs.Add(3411057958U, "ClutterNotificationPeriodicSurveySteps");
			ClientStrings.stringIDs.Add(3709255463U, "TaskStatusInProgress");
			ClientStrings.stringIDs.Add(4078173350U, "AutomaticDisplayName");
			ClientStrings.stringIDs.Add(587246061U, "SharingInvitationInstruction");
			ClientStrings.stringIDs.Add(2610926242U, "BirthdayCalendarFolderName");
			ClientStrings.stringIDs.Add(3519087566U, "NormalKqlKeyword");
			ClientStrings.stringIDs.Add(2937701464U, "ClutterNotificationPeriodicCheckBack");
			ClientStrings.stringIDs.Add(104437952U, "ToKqlKeyword");
			ClientStrings.stringIDs.Add(115734878U, "Drafts");
			ClientStrings.stringIDs.Add(3043371929U, "MeetingDecline");
			ClientStrings.stringIDs.Add(3230188362U, "WhenSecond");
			ClientStrings.stringIDs.Add(232501731U, "SubjectKqlKeyword");
			ClientStrings.stringIDs.Add(2664647494U, "ServerFailures");
			ClientStrings.stringIDs.Add(1134735502U, "ClutterNotificationInvitationHeader");
			ClientStrings.stringIDs.Add(1618289846U, "UMVoiceMailFolderName");
			ClientStrings.stringIDs.Add(1744667246U, "CalendarWhenDailyEveryDay");
			ClientStrings.stringIDs.Add(954396011U, "WhenThird");
			ClientStrings.stringIDs.Add(2864414954U, "ClutterNotificationOptInPrivacy");
			ClientStrings.stringIDs.Add(1025048278U, "SharingInvitationUpdatedSubjectLine");
			ClientStrings.stringIDs.Add(4052458853U, "OtherCalendars");
			ClientStrings.stringIDs.Add(1178930756U, "GroupMailboxWelcomeEmailSubscribeToInboxSubtitle");
			ClientStrings.stringIDs.Add(1797669216U, "Tentative");
			ClientStrings.stringIDs.Add(2411966652U, "AttachmentKqlKeyword");
			ClientStrings.stringIDs.Add(3710484300U, "WhenOnWeekDays");
			ClientStrings.stringIDs.Add(3291021260U, "ClutterNotificationHeaderFont");
			ClientStrings.stringIDs.Add(3252707377U, "TaskWhenWeeklyRegeneratingPattern");
			ClientStrings.stringIDs.Add(4048827998U, "FaxesKqlKeyword");
			ClientStrings.stringIDs.Add(3033231921U, "TaskStatusNotStarted");
			ClientStrings.stringIDs.Add(3465339554U, "ToColon");
			ClientStrings.stringIDs.Add(3882605011U, "IsReadKqlKeyword");
			ClientStrings.stringIDs.Add(2572393147U, "IsFlaggedKqlKeyword");
			ClientStrings.stringIDs.Add(136108241U, "CancellationRumTitle");
			ClientStrings.stringIDs.Add(2244917544U, "ClutterNotificationPeriodicHeader");
			ClientStrings.stringIDs.Add(3855098919U, "GroupSubscriptionPageRequestFailedInfo");
			ClientStrings.stringIDs.Add(3366209149U, "ClutterNotificationInvitationSubject");
			ClientStrings.stringIDs.Add(2530009445U, "FromKqlKeyword");
			ClientStrings.stringIDs.Add(3938988508U, "GroupSubscriptionPageUnsubscribedInfo");
			ClientStrings.stringIDs.Add(1708086969U, "GroupMailboxAddedMemberToPrivateGroupMessage");
			ClientStrings.stringIDs.Add(4132923658U, "WhenOneMoreOccurrence");
			ClientStrings.stringIDs.Add(4182366305U, "GroupSubscriptionUnsubscribeLinkWord");
			ClientStrings.stringIDs.Add(1233830609U, "PublicFolderMailboxDumpsterInfo");
			ClientStrings.stringIDs.Add(2188469818U, "TaskStatusWaitOnOthers");
			ClientStrings.stringIDs.Add(2197783321U, "GroupMailboxWelcomeEmailO365FooterBrowseShareFiles");
			ClientStrings.stringIDs.Add(433190147U, "ClutterNotificationPeriodicSubject");
			ClientStrings.stringIDs.Add(2755014636U, "GroupMailboxAddedMemberNoJoinedByMessageHeader");
			ClientStrings.stringIDs.Add(3895923139U, "RequestedActionNoResponseNecessary");
			ClientStrings.stringIDs.Add(1562762538U, "TaskWhenYearlyRegeneratingPattern");
			ClientStrings.stringIDs.Add(3323263744U, "Free");
			ClientStrings.stringIDs.Add(1871485283U, "CancellationRumDescription");
			ClientStrings.stringIDs.Add(99688204U, "RequestedActionReplyToAll");
			ClientStrings.stringIDs.Add(3733626243U, "GroupMailboxWelcomeEmailO365FooterTitle");
			ClientStrings.stringIDs.Add(2403605275U, "ItemReply");
			ClientStrings.stringIDs.Add(3824460724U, "WhenFirst");
			ClientStrings.stringIDs.Add(483059378U, "RpcClientInitError");
			ClientStrings.stringIDs.Add(348255911U, "RequestedActionDoNotForward");
			ClientStrings.stringIDs.Add(2074560464U, "SizeKqlKeyword");
			ClientStrings.stringIDs.Add(2936941939U, "GroupSubscriptionPagePrivateGroupInfo");
			ClientStrings.stringIDs.Add(693971404U, "UserPhotoNotFound");
			ClientStrings.stringIDs.Add(2768584303U, "FromFavoriteSendersFolderName");
			ClientStrings.stringIDs.Add(2869025391U, "GroupMailboxAddedMemberMessageFont");
			ClientStrings.stringIDs.Add(2905210277U, "RequestedActionForward");
			ClientStrings.stringIDs.Add(4167892423U, "CcKqlKeyword");
			ClientStrings.stringIDs.Add(1697583518U, "Conversations");
			ClientStrings.stringIDs.Add(3614445595U, "GroupSubscriptionPagePublicGroupInfo");
			ClientStrings.stringIDs.Add(677901016U, "ContactsKqlKeyword");
			ClientStrings.stringIDs.Add(3513814141U, "WhenBothWeekendDays");
			ClientStrings.stringIDs.Add(2377377097U, "Conflicts");
			ClientStrings.stringIDs.Add(51369050U, "PostsKqlKeyword");
			ClientStrings.stringIDs.Add(1940443510U, "UserPhotoPreviewNotFound");
			ClientStrings.stringIDs.Add(1236916945U, "WhenEveryDay");
			ClientStrings.stringIDs.Add(1844900681U, "SharingRequestInstruction");
			ClientStrings.stringIDs.Add(940910702U, "ClutterNotificationAutoEnablementNoticeIntro");
			ClientStrings.stringIDs.Add(1418871467U, "ClutterNotificationBodyFont");
			ClientStrings.stringIDs.Add(1685151768U, "ClutterNotificationInvitationO365Helps");
			ClientStrings.stringIDs.Add(3953967230U, "WhenFourth");
			ClientStrings.stringIDs.Add(1657114432U, "RequestedActionFollowUp");
			ClientStrings.stringIDs.Add(3221177017U, "GroupSubscriptionPageRequestFailed");
			ClientStrings.stringIDs.Add(1300788680U, "ClutterNotificationAutoEnablementNoticeSubject");
			ClientStrings.stringIDs.Add(2028589045U, "PeoplesCalendars");
			ClientStrings.stringIDs.Add(1660374630U, "EHAMigration");
			ClientStrings.stringIDs.Add(3413891549U, "SubjectColon");
			ClientStrings.stringIDs.Add(1292798904U, "Calendar");
			ClientStrings.stringIDs.Add(899464318U, "RequestedActionAny");
			ClientStrings.stringIDs.Add(2912044233U, "WhenEveryWeekDay");
			ClientStrings.stringIDs.Add(2607340737U, "TasksKqlKeyword");
			ClientStrings.stringIDs.Add(802022316U, "NotesKqlKeyword");
			ClientStrings.stringIDs.Add(1726717990U, "TaskStatusCompleted");
			ClientStrings.stringIDs.Add(3501929050U, "ClutterNotificationInvitationItsAutomatic");
			ClientStrings.stringIDs.Add(3598244064U, "RssSubscriptions");
			ClientStrings.stringIDs.Add(153688296U, "RequestedActionCall");
			ClientStrings.stringIDs.Add(3795294554U, "PolicyTipDefaultMessageNotifyOnly");
			ClientStrings.stringIDs.Add(219237725U, "FailedToUploadToSharePointTitle");
			ClientStrings.stringIDs.Add(2255519906U, "TaskStatusDeferred");
			ClientStrings.stringIDs.Add(3425704829U, "SharingRequestAllowed");
			ClientStrings.stringIDs.Add(1694200734U, "UpdateRumMissingItemFlag");
			ClientStrings.stringIDs.Add(963851651U, "ParticipantsKqlKeyword");
			ClientStrings.stringIDs.Add(1044563024U, "GroupMailboxWelcomeEmailStartConversationTitle");
			ClientStrings.stringIDs.Add(2833103505U, "GroupMailboxWelcomeEmailShareFilesTitle");
			ClientStrings.stringIDs.Add(3635271833U, "LegacyArchiveJournals");
			ClientStrings.stringIDs.Add(3126868667U, "RecipientsKqlKeyword");
			ClientStrings.stringIDs.Add(2904245352U, "RssFeedsKqlKeyword");
			ClientStrings.stringIDs.Add(2776152054U, "GroupMailboxWelcomeEmailSubscribeToInboxTitle");
			ClientStrings.stringIDs.Add(2442729049U, "GroupMailboxAddedMemberMessageCalendar3");
			ClientStrings.stringIDs.Add(1616483908U, "RequestedActionReview");
			ClientStrings.stringIDs.Add(3043633671U, "KindKqlKeyword");
			ClientStrings.stringIDs.Add(916455203U, "EmailKqlKeyword");
			ClientStrings.stringIDs.Add(2450331336U, "UTC");
			ClientStrings.stringIDs.Add(1793503025U, "BccKqlKeyword");
			ClientStrings.stringIDs.Add(3029761970U, "RequestedActionForYourInformation");
			ClientStrings.stringIDs.Add(4137480277U, "Journal");
			ClientStrings.stringIDs.Add(1486137846U, "GroupMailboxAddedMemberMessageConversation1");
			ClientStrings.stringIDs.Add(1486137844U, "GroupMailboxAddedMemberMessageConversation3");
			ClientStrings.stringIDs.Add(136364712U, "HasAttachmentKqlKeyword");
			ClientStrings.stringIDs.Add(1780326221U, "GroupSubscriptionPageGroupInfoFont");
			ClientStrings.stringIDs.Add(1716044995U, "Contacts");
			ClientStrings.stringIDs.Add(2442729048U, "GroupMailboxAddedMemberMessageCalendar2");
			ClientStrings.stringIDs.Add(1836161108U, "FavoritesFolderName");
			ClientStrings.stringIDs.Add(1516506571U, "ImKqlKeyword");
			ClientStrings.stringIDs.Add(2755562843U, "ImportanceKqlKeyword");
			ClientStrings.stringIDs.Add(1165698602U, "MyTasks");
			ClientStrings.stringIDs.Add(143815687U, "SharingInvitationAndRequestInstruction");
			ClientStrings.stringIDs.Add(632725474U, "TaskWhenEveryOtherDay");
			ClientStrings.stringIDs.Add(710925581U, "Conversation");
			ClientStrings.stringIDs.Add(3191048500U, "FailedToDeleteFromSharePointTitle");
			ClientStrings.stringIDs.Add(2241039844U, "JunkEmail");
			ClientStrings.stringIDs.Add(765381967U, "GroupMailboxWelcomeEmailFooterSubscribeDescriptionText");
			ClientStrings.stringIDs.Add(1976982380U, "ClutterFolderName");
			ClientStrings.stringIDs.Add(261418034U, "ClutterNotificationInvitationWeCallIt");
			ClientStrings.stringIDs.Add(1524084310U, "GroupMailboxWelcomeEmailDefaultDescription");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000034AB File Offset: 0x000016AB
		public static LocalizedString PostedTo
		{
			get
			{
				return new LocalizedString("PostedTo", "Ex6A8EA5", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000034CC File Offset: 0x000016CC
		public static LocalizedString ClutterNotificationAutoEnablementNoticeYoullBeEnabed(string optOutUrl)
		{
			return new LocalizedString("ClutterNotificationAutoEnablementNoticeYoullBeEnabed", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				optOutUrl
			});
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000034FB File Offset: 0x000016FB
		public static LocalizedString UnknownDelegateUser
		{
			get
			{
				return new LocalizedString("UnknownDelegateUser", "Ex18B5E4", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000351C File Offset: 0x0000171C
		public static LocalizedString FailedToSynchronizeChangesFromSharePoint(string sharePointUrl)
		{
			return new LocalizedString("FailedToSynchronizeChangesFromSharePoint", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				sharePointUrl
			});
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000354B File Offset: 0x0000174B
		public static LocalizedString ClutterNotificationInvitationIfYouDontLikeIt
		{
			get
			{
				return new LocalizedString("ClutterNotificationInvitationIfYouDontLikeIt", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00003569 File Offset: 0x00001769
		public static LocalizedString Inbox
		{
			get
			{
				return new LocalizedString("Inbox", "Ex1DF479", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00003588 File Offset: 0x00001788
		public static LocalizedString ClutterNotificationAutoEnablementNoticeLearnMore(string clutterAnnouncementUrl, string clutterLearnMoreUrl)
		{
			return new LocalizedString("ClutterNotificationAutoEnablementNoticeLearnMore", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				clutterAnnouncementUrl,
				clutterLearnMoreUrl
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000035BC File Offset: 0x000017BC
		public static LocalizedString RemindersSearchFolderName(string uniqueSuffix)
		{
			return new LocalizedString("RemindersSearchFolderName", "Ex69B0D6", false, true, ClientStrings.ResourceManager, new object[]
			{
				uniqueSuffix
			});
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000035EB File Offset: 0x000017EB
		public static LocalizedString CategoryKqlKeyword
		{
			get
			{
				return new LocalizedString("CategoryKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00003609 File Offset: 0x00001809
		public static LocalizedString RequestedActionReply
		{
			get
			{
				return new LocalizedString("RequestedActionReply", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00003627 File Offset: 0x00001827
		public static LocalizedString ClutterNotificationAutoEnablementNoticeHeader
		{
			get
			{
				return new LocalizedString("ClutterNotificationAutoEnablementNoticeHeader", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00003645 File Offset: 0x00001845
		public static LocalizedString MeetingTentative
		{
			get
			{
				return new LocalizedString("MeetingTentative", "Ex762E2C", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00003663 File Offset: 0x00001863
		public static LocalizedString BodyKqlKeyword
		{
			get
			{
				return new LocalizedString("BodyKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00003681 File Offset: 0x00001881
		public static LocalizedString WhenAllDays
		{
			get
			{
				return new LocalizedString("WhenAllDays", "Ex5B0CF6", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000369F File Offset: 0x0000189F
		public static LocalizedString GroupMailboxWelcomeEmailFooterUnsubscribeLinkText
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailFooterUnsubscribeLinkText", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000036BD File Offset: 0x000018BD
		public static LocalizedString TaskWhenDailyRegeneratingPattern
		{
			get
			{
				return new LocalizedString("TaskWhenDailyRegeneratingPattern", "Ex6C7B28", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000036DC File Offset: 0x000018DC
		public static LocalizedString MdnReadNoTime(LocalizedString messageInfo)
		{
			return new LocalizedString("MdnReadNoTime", "ExC8D803", false, true, ClientStrings.ResourceManager, new object[]
			{
				messageInfo
			});
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00003710 File Offset: 0x00001910
		public static LocalizedString UpdateRumLocationFlag
		{
			get
			{
				return new LocalizedString("UpdateRumLocationFlag", "Ex99408F", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003730 File Offset: 0x00001930
		public static LocalizedString FailedMaintenanceSynchronizationsText(string sharePointUrl, string error)
		{
			return new LocalizedString("FailedMaintenanceSynchronizationsText", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				sharePointUrl,
				error
			});
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00003763 File Offset: 0x00001963
		public static LocalizedString CcColon
		{
			get
			{
				return new LocalizedString("CcColon", "Ex9B53F3", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00003781 File Offset: 0x00001981
		public static LocalizedString GroupMailboxWelcomeEmailUnsubscribeToInboxTitle
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailUnsubscribeToInboxTitle", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000379F File Offset: 0x0000199F
		public static LocalizedString HighKqlKeyword
		{
			get
			{
				return new LocalizedString("HighKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000037C0 File Offset: 0x000019C0
		public static LocalizedString CalendarWhenYearly(IFormattable month, int day)
		{
			return new LocalizedString("CalendarWhenYearly", "ExE68E1B", false, true, ClientStrings.ResourceManager, new object[]
			{
				month,
				day
			});
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000037F8 File Offset: 0x000019F8
		public static LocalizedString OnBehalfOf
		{
			get
			{
				return new LocalizedString("OnBehalfOf", "ExC7CC16", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00003816 File Offset: 0x00001A16
		public static LocalizedString SentKqlKeyword
		{
			get
			{
				return new LocalizedString("SentKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00003834 File Offset: 0x00001A34
		public static LocalizedString UpdateRumCancellationFlag
		{
			get
			{
				return new LocalizedString("UpdateRumCancellationFlag", "ExE60B17", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00003852 File Offset: 0x00001A52
		public static LocalizedString PrivateAppointmentSubject
		{
			get
			{
				return new LocalizedString("PrivateAppointmentSubject", "ExBAF266", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003870 File Offset: 0x00001A70
		public static LocalizedString AlternateCalendarWhenMonthlyEveryNMonths(LocalizedString calendar, int day, int interval)
		{
			return new LocalizedString("AlternateCalendarWhenMonthlyEveryNMonths", "ExEA6399", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				day,
				interval
			});
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000038B6 File Offset: 0x00001AB6
		public static LocalizedString GroupMailboxWelcomeMessageHeader2
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeMessageHeader2", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000038D4 File Offset: 0x00001AD4
		public static LocalizedString Busy
		{
			get
			{
				return new LocalizedString("Busy", "ExC0FED8", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000038F2 File Offset: 0x00001AF2
		public static LocalizedString CalendarWhenEveryOtherDay
		{
			get
			{
				return new LocalizedString("CalendarWhenEveryOtherDay", "ExC2FA6D", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003910 File Offset: 0x00001B10
		public static LocalizedString AlternateCalendarWhenYearlyThLeap(LocalizedString calendar, LocalizedString order, IFormattable dayOfWeek, IFormattable month)
		{
			return new LocalizedString("AlternateCalendarWhenYearlyThLeap", "Ex74959D", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				order,
				dayOfWeek,
				month
			});
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00003955 File Offset: 0x00001B55
		public static LocalizedString WhenWeekDays
		{
			get
			{
				return new LocalizedString("WhenWeekDays", "Ex6DE869", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00003973 File Offset: 0x00001B73
		public static LocalizedString WhenOnEveryDayOfTheWeek
		{
			get
			{
				return new LocalizedString("WhenOnEveryDayOfTheWeek", "Ex7511ED", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003994 File Offset: 0x00001B94
		public static LocalizedString FailedToSynchronizeChangesFromSharePointText(string sharePointDocumentLibUrl, string error)
		{
			return new LocalizedString("FailedToSynchronizeChangesFromSharePointText", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				sharePointDocumentLibUrl,
				error
			});
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000039C7 File Offset: 0x00001BC7
		public static LocalizedString ClutterNotificationAutoEnablementNoticeHowItWorks
		{
			get
			{
				return new LocalizedString("ClutterNotificationAutoEnablementNoticeHowItWorks", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000039E5 File Offset: 0x00001BE5
		public static LocalizedString RequestedActionRead
		{
			get
			{
				return new LocalizedString("RequestedActionRead", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00003A03 File Offset: 0x00001C03
		public static LocalizedString GroupMailboxWelcomeEmailPublicTypeText
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailPublicTypeText", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003A24 File Offset: 0x00001C24
		public static LocalizedString ClutterNotificationOptInHowToTrain(string clutterFolderName)
		{
			return new LocalizedString("ClutterNotificationOptInHowToTrain", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				clutterFolderName
			});
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00003A53 File Offset: 0x00001C53
		public static LocalizedString PolicyTipDefaultMessageRejectOverride
		{
			get
			{
				return new LocalizedString("PolicyTipDefaultMessageRejectOverride", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00003A71 File Offset: 0x00001C71
		public static LocalizedString WhenLast
		{
			get
			{
				return new LocalizedString("WhenLast", "Ex6DEADF", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00003A8F File Offset: 0x00001C8F
		public static LocalizedString GroupMailboxWelcomeEmailUnsubscribeToInboxSubtitle
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailUnsubscribeToInboxSubtitle", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public static LocalizedString WhenRecurringNoEndDateDaysHours(LocalizedString pattern, object startDate, object startTime, int days, int hours)
		{
			return new LocalizedString("WhenRecurringNoEndDateDaysHours", "ExA993DA", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				startTime,
				days,
				hours
			});
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003B00 File Offset: 0x00001D00
		public static LocalizedString WhenRecurringWithEndDateNoDuration(LocalizedString pattern, object startDate, object endDate, object startTime)
		{
			return new LocalizedString("WhenRecurringWithEndDateNoDuration", "ExB2E4D5", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate,
				startTime
			});
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003B40 File Offset: 0x00001D40
		public static LocalizedString WhenPart
		{
			get
			{
				return new LocalizedString("WhenPart", "ExE5D896", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00003B5E File Offset: 0x00001D5E
		public static LocalizedString GroupMailboxAddedMemberMessageDocument2
		{
			get
			{
				return new LocalizedString("GroupMailboxAddedMemberMessageDocument2", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003B7C File Offset: 0x00001D7C
		public static LocalizedString FailedToUploadToSharePointErrorText(string fileName, string sharePointFolderUrl)
		{
			return new LocalizedString("FailedToUploadToSharePointErrorText", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				fileName,
				sharePointFolderUrl
			});
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public static LocalizedString SharingInvitationAndRequest(string user, string email, LocalizedString foldertype)
		{
			return new LocalizedString("SharingInvitationAndRequest", "ExD48D4B", false, true, ClientStrings.ResourceManager, new object[]
			{
				user,
				email,
				foldertype
			});
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00003BEC File Offset: 0x00001DEC
		public static LocalizedString UpdateRumStartTimeFlag
		{
			get
			{
				return new LocalizedString("UpdateRumStartTimeFlag", "Ex4EED57", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00003C0A File Offset: 0x00001E0A
		public static LocalizedString MyContactsFolderName
		{
			get
			{
				return new LocalizedString("MyContactsFolderName", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003C28 File Offset: 0x00001E28
		public static LocalizedString WhenFiveDaysOfWeek(object day1, object day2, object day3, object day4, object day5)
		{
			return new LocalizedString("WhenFiveDaysOfWeek", "ExA08DF3", false, true, ClientStrings.ResourceManager, new object[]
			{
				day1,
				day2,
				day3,
				day4,
				day5
			});
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003C68 File Offset: 0x00001E68
		public static LocalizedString DocumentSyncIssues
		{
			get
			{
				return new LocalizedString("DocumentSyncIssues", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003C88 File Offset: 0x00001E88
		public static LocalizedString AlternateCalendarTaskWhenMonthlyThEveryMonth(LocalizedString calendar, LocalizedString order, IFormattable day)
		{
			return new LocalizedString("AlternateCalendarTaskWhenMonthlyThEveryMonth", "ExD6F1A7", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				order,
				day
			});
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003CC9 File Offset: 0x00001EC9
		public static LocalizedString MeetingAccept
		{
			get
			{
				return new LocalizedString("MeetingAccept", "ExDBDCBC", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public static LocalizedString CalendarWhenWeeklyEveryWeek(IFormattable dayOfWeek)
		{
			return new LocalizedString("CalendarWhenWeeklyEveryWeek", "ExCC097A", false, true, ClientStrings.ResourceManager, new object[]
			{
				dayOfWeek
			});
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00003D17 File Offset: 0x00001F17
		public static LocalizedString SearchFolders
		{
			get
			{
				return new LocalizedString("SearchFolders", "ExA655CF", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003D38 File Offset: 0x00001F38
		public static LocalizedString WhenRecurringNoEndDateOneDayHoursMinutes(LocalizedString pattern, object startDate, object startTime, int hours, int minutes)
		{
			return new LocalizedString("WhenRecurringNoEndDateOneDayHoursMinutes", "Ex838E4B", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				startTime,
				hours,
				minutes
			});
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003D88 File Offset: 0x00001F88
		public static LocalizedString WhenThreeDaysOfWeek(object day1, object day2, object day3)
		{
			return new LocalizedString("WhenThreeDaysOfWeek", "Ex31FBB5", false, true, ClientStrings.ResourceManager, new object[]
			{
				day1,
				day2,
				day3
			});
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003DBF File Offset: 0x00001FBF
		public static LocalizedString OOF
		{
			get
			{
				return new LocalizedString("OOF", "ExE39E3C", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003DDD File Offset: 0x00001FDD
		public static LocalizedString GroupMailboxWelcomeEmailFooterUnsubscribeDescirptionText
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailFooterUnsubscribeDescirptionText", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003DFC File Offset: 0x00001FFC
		public static LocalizedString AlternateCalendarWhenMonthlyEveryMonth(LocalizedString calendar, int day)
		{
			return new LocalizedString("AlternateCalendarWhenMonthlyEveryMonth", "ExD369C7", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				day
			});
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003E3C File Offset: 0x0000203C
		public static LocalizedString WhenRecurringWithEndDateDaysNoDuration(LocalizedString pattern, object startDate, object endDate, object startTime, int days)
		{
			return new LocalizedString("WhenRecurringWithEndDateDaysNoDuration", "Ex5AF575", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate,
				startTime,
				days
			});
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003E86 File Offset: 0x00002086
		public static LocalizedString GroupMailboxWelcomeEmailShareFilesSubTitle
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailShareFilesSubTitle", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003EA4 File Offset: 0x000020A4
		public static LocalizedString GroupMailboxWelcomeEmailHeader(string groupName)
		{
			return new LocalizedString("GroupMailboxWelcomeEmailHeader", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				groupName
			});
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003ED3 File Offset: 0x000020D3
		public static LocalizedString GroupMailboxWelcomeEmailPrivateTypeText
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailPrivateTypeText", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003EF1 File Offset: 0x000020F1
		public static LocalizedString GroupMailboxWelcomeEmailFooterSubscribeLinkText
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailFooterSubscribeLinkText", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003F10 File Offset: 0x00002110
		public static LocalizedString MdnNotReadNoTime(LocalizedString messageInfo)
		{
			return new LocalizedString("MdnNotReadNoTime", "Ex65D268", false, true, ClientStrings.ResourceManager, new object[]
			{
				messageInfo
			});
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003F44 File Offset: 0x00002144
		public static LocalizedString SentColon
		{
			get
			{
				return new LocalizedString("SentColon", "Ex6B1F4E", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003F62 File Offset: 0x00002162
		public static LocalizedString ClutterNotificationO365Closing
		{
			get
			{
				return new LocalizedString("ClutterNotificationO365Closing", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003F80 File Offset: 0x00002180
		public static LocalizedString MyCalendars
		{
			get
			{
				return new LocalizedString("MyCalendars", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003FA0 File Offset: 0x000021A0
		public static LocalizedString WhenRecurringWithEndDateDaysOneHourMinutes(LocalizedString pattern, object startDate, object endDate, object startTime, int days, int minutes)
		{
			return new LocalizedString("WhenRecurringWithEndDateDaysOneHourMinutes", "Ex245174", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate,
				startTime,
				days,
				minutes
			});
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003FF4 File Offset: 0x000021F4
		public static LocalizedString WhenRecurringNoEndDateOneDayOneHour(LocalizedString pattern, object startDate, object startTime)
		{
			return new LocalizedString("WhenRecurringNoEndDateOneDayOneHour", "Ex60B9D3", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				startTime
			});
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00004030 File Offset: 0x00002230
		public static LocalizedString AttachmentNamesKqlKeyword
		{
			get
			{
				return new LocalizedString("AttachmentNamesKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000404E File Offset: 0x0000224E
		public static LocalizedString UMFaxFolderName
		{
			get
			{
				return new LocalizedString("UMFaxFolderName", "ExCE057A", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000406C File Offset: 0x0000226C
		public static LocalizedString GroupMailboxSuggestToSubscribe
		{
			get
			{
				return new LocalizedString("GroupMailboxSuggestToSubscribe", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000408C File Offset: 0x0000228C
		public static LocalizedString GroupMailboxWelcomeMessageSubject(string gmName)
		{
			return new LocalizedString("GroupMailboxWelcomeMessageSubject", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				gmName
			});
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000040BC File Offset: 0x000022BC
		public static LocalizedString GroupMailboxAddedMemberNoJoinedBySubject(string gmName)
		{
			return new LocalizedString("GroupMailboxAddedMemberNoJoinedBySubject", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				gmName
			});
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000040EB File Offset: 0x000022EB
		public static LocalizedString WherePart
		{
			get
			{
				return new LocalizedString("WherePart", "ExE44CAD", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00004109 File Offset: 0x00002309
		public static LocalizedString MeetingProposedNewTime
		{
			get
			{
				return new LocalizedString("MeetingProposedNewTime", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00004127 File Offset: 0x00002327
		public static LocalizedString Tasks
		{
			get
			{
				return new LocalizedString("Tasks", "Ex876B5E", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00004145 File Offset: 0x00002345
		public static LocalizedString ClutterNotificationAutoEnablementNoticeWeCallIt
		{
			get
			{
				return new LocalizedString("ClutterNotificationAutoEnablementNoticeWeCallIt", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00004163 File Offset: 0x00002363
		public static LocalizedString Configuration
		{
			get
			{
				return new LocalizedString("Configuration", "Ex4DD342", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00004181 File Offset: 0x00002381
		public static LocalizedString ReceivedKqlKeyword
		{
			get
			{
				return new LocalizedString("ReceivedKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000419F File Offset: 0x0000239F
		public static LocalizedString ItemForward
		{
			get
			{
				return new LocalizedString("ItemForward", "Ex2D7BDE", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000041BD File Offset: 0x000023BD
		public static LocalizedString PublicFolderMailboxHierarchyInfo
		{
			get
			{
				return new LocalizedString("PublicFolderMailboxHierarchyInfo", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000041DB File Offset: 0x000023DB
		public static LocalizedString GroupMailboxAddedMemberMessageDocument1
		{
			get
			{
				return new LocalizedString("GroupMailboxAddedMemberMessageDocument1", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000041F9 File Offset: 0x000023F9
		public static LocalizedString GroupMailboxAddedSelfMessageHeader
		{
			get
			{
				return new LocalizedString("GroupMailboxAddedSelfMessageHeader", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00004217 File Offset: 0x00002417
		public static LocalizedString GroupSubscriptionPageSubscribedInfo
		{
			get
			{
				return new LocalizedString("GroupSubscriptionPageSubscribedInfo", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004235 File Offset: 0x00002435
		public static LocalizedString TaskWhenMonthlyRegeneratingPattern
		{
			get
			{
				return new LocalizedString("TaskWhenMonthlyRegeneratingPattern", "Ex8B6BCA", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004254 File Offset: 0x00002454
		public static LocalizedString GroupMailboxAddedMemberMessageSubject(string gmName)
		{
			return new LocalizedString("GroupMailboxAddedMemberMessageSubject", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				gmName
			});
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004284 File Offset: 0x00002484
		public static LocalizedString AlternateCalendarTaskWhenYearlyLeap(LocalizedString calendar, IFormattable month, int day)
		{
			return new LocalizedString("AlternateCalendarTaskWhenYearlyLeap", "Ex8B6D9B", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				month,
				day
			});
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000042C5 File Offset: 0x000024C5
		public static LocalizedString SentTime
		{
			get
			{
				return new LocalizedString("SentTime", "ExA30CE2", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000042E4 File Offset: 0x000024E4
		public static LocalizedString WhenRecurringWithEndDateOneDayOneHour(LocalizedString pattern, object startDate, object endDate, object startTime)
		{
			return new LocalizedString("WhenRecurringWithEndDateOneDayOneHour", "ExE88870", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate,
				startTime
			});
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00004324 File Offset: 0x00002524
		public static LocalizedString SentItems
		{
			get
			{
				return new LocalizedString("SentItems", "Ex195085", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00004342 File Offset: 0x00002542
		public static LocalizedString UpdateRumDuplicateFlags
		{
			get
			{
				return new LocalizedString("UpdateRumDuplicateFlags", "Ex66CCCF", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004360 File Offset: 0x00002560
		public static LocalizedString GroupMailboxJoinRequestEmailSubject(string userName, string groupName)
		{
			return new LocalizedString("GroupMailboxJoinRequestEmailSubject", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				userName,
				groupName
			});
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00004393 File Offset: 0x00002593
		public static LocalizedString ToDoSearch
		{
			get
			{
				return new LocalizedString("ToDoSearch", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000043B1 File Offset: 0x000025B1
		public static LocalizedString GroupMailboxWelcomeEmailO365FooterBrowseViewCalendar
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailO365FooterBrowseViewCalendar", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000043CF File Offset: 0x000025CF
		public static LocalizedString UserPhotoNotAnImage
		{
			get
			{
				return new LocalizedString("UserPhotoNotAnImage", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000043F0 File Offset: 0x000025F0
		public static LocalizedString SharingAccept(string user, string email, LocalizedString foldertype)
		{
			return new LocalizedString("SharingAccept", "Ex8FC5D4", false, true, ClientStrings.ResourceManager, new object[]
			{
				user,
				email,
				foldertype
			});
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000442C File Offset: 0x0000262C
		public static LocalizedString OofReply
		{
			get
			{
				return new LocalizedString("OofReply", "ExACCA0D", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000444C File Offset: 0x0000264C
		public static LocalizedString TaskWhenNWeeksRegeneratingPattern(int weeks)
		{
			return new LocalizedString("TaskWhenNWeeksRegeneratingPattern", "Ex06AC26", false, true, ClientStrings.ResourceManager, new object[]
			{
				weeks
			});
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004480 File Offset: 0x00002680
		public static LocalizedString AlternateCalendarWhenMonthlyEveryOtherMonth(LocalizedString calendar, int day)
		{
			return new LocalizedString("AlternateCalendarWhenMonthlyEveryOtherMonth", "Ex5A3C51", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				day
			});
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000044BD File Offset: 0x000026BD
		public static LocalizedString UpdateRumDescription
		{
			get
			{
				return new LocalizedString("UpdateRumDescription", "Ex0462D8", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000044DB File Offset: 0x000026DB
		public static LocalizedString SharingRequestDenied
		{
			get
			{
				return new LocalizedString("SharingRequestDenied", "Ex78C810", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000044F9 File Offset: 0x000026F9
		public static LocalizedString CommonViews
		{
			get
			{
				return new LocalizedString("CommonViews", "Ex52F7B4", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004518 File Offset: 0x00002718
		public static LocalizedString ClutterNotificationOptInLearnMore(string clutterLearnMoreUrl)
		{
			return new LocalizedString("ClutterNotificationOptInLearnMore", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				clutterLearnMoreUrl
			});
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004548 File Offset: 0x00002748
		public static LocalizedString TaskWhenYearlyTh(LocalizedString order, IFormattable dayOfWeek, IFormattable month)
		{
			return new LocalizedString("TaskWhenYearlyTh", "Ex8CB8FA", false, true, ClientStrings.ResourceManager, new object[]
			{
				order,
				dayOfWeek,
				month
			});
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004584 File Offset: 0x00002784
		public static LocalizedString CalendarWhenWeeklyEveryAlterateWeek(IFormattable dayOfWeek)
		{
			return new LocalizedString("CalendarWhenWeeklyEveryAlterateWeek", "Ex4D8837", false, true, ClientStrings.ResourceManager, new object[]
			{
				dayOfWeek
			});
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000045B3 File Offset: 0x000027B3
		public static LocalizedString TaskWhenDailyEveryDay
		{
			get
			{
				return new LocalizedString("TaskWhenDailyEveryDay", "ExA30F3D", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000045D4 File Offset: 0x000027D4
		public static LocalizedString TaskWhenMonthlyEveryMonth(int day)
		{
			return new LocalizedString("TaskWhenMonthlyEveryMonth", "Ex531066", false, true, ClientStrings.ResourceManager, new object[]
			{
				day
			});
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004608 File Offset: 0x00002808
		public static LocalizedString NoDataAvailable
		{
			get
			{
				return new LocalizedString("NoDataAvailable", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004628 File Offset: 0x00002828
		public static LocalizedString AppendStrings(LocalizedString str1, LocalizedString str2)
		{
			return new LocalizedString("AppendStrings", "Ex622157", false, true, ClientStrings.ResourceManager, new object[]
			{
				str1,
				str2
			});
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004668 File Offset: 0x00002868
		public static LocalizedString ClutterNotificationDisableDeepLink(string disableUrl)
		{
			return new LocalizedString("ClutterNotificationDisableDeepLink", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				disableUrl
			});
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004697 File Offset: 0x00002897
		public static LocalizedString Root
		{
			get
			{
				return new LocalizedString("Root", "Ex97F5F2", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000046B8 File Offset: 0x000028B8
		public static LocalizedString CalendarWhenWeeklyEveryNWeeks(int interval, IFormattable dayOfWeek)
		{
			return new LocalizedString("CalendarWhenWeeklyEveryNWeeks", "Ex027619", false, true, ClientStrings.ResourceManager, new object[]
			{
				interval,
				dayOfWeek
			});
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000046F0 File Offset: 0x000028F0
		public static LocalizedString KoreanLunar
		{
			get
			{
				return new LocalizedString("KoreanLunar", "Ex44E415", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000470E File Offset: 0x0000290E
		public static LocalizedString SyncIssues
		{
			get
			{
				return new LocalizedString("SyncIssues", "Ex7D35C0", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000472C File Offset: 0x0000292C
		public static LocalizedString UnifiedInbox
		{
			get
			{
				return new LocalizedString("UnifiedInbox", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000474C File Offset: 0x0000294C
		public static LocalizedString WhenRecurringWithEndDateNoTimeInDay(LocalizedString pattern, object startDate, object endDate)
		{
			return new LocalizedString("WhenRecurringWithEndDateNoTimeInDay", "Ex4D97BF", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate
			});
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004788 File Offset: 0x00002988
		public static LocalizedString ContactSubjectFormat
		{
			get
			{
				return new LocalizedString("ContactSubjectFormat", "ExF57A6A", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000047A8 File Offset: 0x000029A8
		public static LocalizedString FailedMaintenanceSynchronizations(string sharePointUrl)
		{
			return new LocalizedString("FailedMaintenanceSynchronizations", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				sharePointUrl
			});
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000047D7 File Offset: 0x000029D7
		public static LocalizedString ChineseLunar
		{
			get
			{
				return new LocalizedString("ChineseLunar", "Ex435B21", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000047F5 File Offset: 0x000029F5
		public static LocalizedString Outbox
		{
			get
			{
				return new LocalizedString("Outbox", "Ex36473C", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004813 File Offset: 0x00002A13
		public static LocalizedString UpdateRumInconsistencyFlagsLabel
		{
			get
			{
				return new LocalizedString("UpdateRumInconsistencyFlagsLabel", "Ex2283D1", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004834 File Offset: 0x00002A34
		public static LocalizedString WhenRecurringNoEndDateNoTimeInDay(LocalizedString pattern, object startDate)
		{
			return new LocalizedString("WhenRecurringNoEndDateNoTimeInDay", "ExF86A19", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate
			});
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000486C File Offset: 0x00002A6C
		public static LocalizedString WhenRecurringWithEndDateDaysOneHour(LocalizedString pattern, object startDate, object endDate, object startTime, int days)
		{
			return new LocalizedString("WhenRecurringWithEndDateDaysOneHour", "Ex537F1F", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate,
				startTime,
				days
			});
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000048B8 File Offset: 0x00002AB8
		public static LocalizedString UserPhotoFileTooLarge(int limit)
		{
			return new LocalizedString("UserPhotoFileTooLarge", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				limit
			});
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000048EC File Offset: 0x00002AEC
		public static LocalizedString Hijri
		{
			get
			{
				return new LocalizedString("Hijri", "Ex33AAED", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000490A File Offset: 0x00002B0A
		public static LocalizedString MeetingsKqlKeyword
		{
			get
			{
				return new LocalizedString("MeetingsKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00004928 File Offset: 0x00002B28
		public static LocalizedString ClutterNotificationInvitationHowItWorks
		{
			get
			{
				return new LocalizedString("ClutterNotificationInvitationHowItWorks", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004946 File Offset: 0x00002B46
		public static LocalizedString GroupMailboxAddedMemberMessageDocument3
		{
			get
			{
				return new LocalizedString("GroupMailboxAddedMemberMessageDocument3", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004964 File Offset: 0x00002B64
		public static LocalizedString UpdateRumRecurrenceFlag
		{
			get
			{
				return new LocalizedString("UpdateRumRecurrenceFlag", "Ex21C763", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004982 File Offset: 0x00002B82
		public static LocalizedString PolicyTagKqlKeyword
		{
			get
			{
				return new LocalizedString("PolicyTagKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000049A0 File Offset: 0x00002BA0
		public static LocalizedString ClutterNotificationPeriodicIntro(string clutterFolderName)
		{
			return new LocalizedString("ClutterNotificationPeriodicIntro", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				clutterFolderName
			});
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000049D0 File Offset: 0x00002BD0
		public static LocalizedString WhenRecurringWithEndDateDaysHoursMinutes(LocalizedString pattern, object startDate, object endDate, object startTime, int days, int hours, int minutes)
		{
			return new LocalizedString("WhenRecurringWithEndDateDaysHoursMinutes", "ExC2BC40", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate,
				startTime,
				days,
				hours,
				minutes
			});
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004A30 File Offset: 0x00002C30
		public static LocalizedString GroupMailboxWelcomeEmailStartConversationSubtitle(string emailAddress)
		{
			return new LocalizedString("GroupMailboxWelcomeEmailStartConversationSubtitle", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004A5F File Offset: 0x00002C5F
		public static LocalizedString GroupSubscriptionPageBodyFont
		{
			get
			{
				return new LocalizedString("GroupSubscriptionPageBodyFont", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004A7D File Offset: 0x00002C7D
		public static LocalizedString ClutterNotificationOptInSubject
		{
			get
			{
				return new LocalizedString("ClutterNotificationOptInSubject", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004A9B File Offset: 0x00002C9B
		public static LocalizedString ClutterNotificationAutoEnablementNoticeItsAutomatic
		{
			get
			{
				return new LocalizedString("ClutterNotificationAutoEnablementNoticeItsAutomatic", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004ABC File Offset: 0x00002CBC
		public static LocalizedString MdnRead(LocalizedString messageInfo, object dateTime, LocalizedString timeZoneName)
		{
			return new LocalizedString("MdnRead", "Ex3D3070", false, true, ClientStrings.ResourceManager, new object[]
			{
				messageInfo,
				dateTime,
				timeZoneName
			});
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004AFD File Offset: 0x00002CFD
		public static LocalizedString WorkingElsewhere
		{
			get
			{
				return new LocalizedString("WorkingElsewhere", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004B1B File Offset: 0x00002D1B
		public static LocalizedString NTUser
		{
			get
			{
				return new LocalizedString("NTUser", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004B3C File Offset: 0x00002D3C
		public static LocalizedString CalendarWhenMonthlyEveryMonth(int day)
		{
			return new LocalizedString("CalendarWhenMonthlyEveryMonth", "ExD8A463", false, true, ClientStrings.ResourceManager, new object[]
			{
				day
			});
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004B70 File Offset: 0x00002D70
		public static LocalizedString TaskWhenWeeklyEveryWeek(IFormattable dayOfWeek)
		{
			return new LocalizedString("TaskWhenWeeklyEveryWeek", "ExDFC345", false, true, ClientStrings.ResourceManager, new object[]
			{
				dayOfWeek
			});
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004BA0 File Offset: 0x00002DA0
		public static LocalizedString WhenRecurringNoEndDateDaysHoursMinutes(LocalizedString pattern, object startDate, object startTime, int days, int hours, int minutes)
		{
			return new LocalizedString("WhenRecurringNoEndDateDaysHoursMinutes", "ExBC53EB", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				startTime,
				days,
				hours,
				minutes
			});
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004BFC File Offset: 0x00002DFC
		public static LocalizedString GroupMailboxAddedSelfMessageSubject(string gmName)
		{
			return new LocalizedString("GroupMailboxAddedSelfMessageSubject", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				gmName
			});
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004C2B File Offset: 0x00002E2B
		public static LocalizedString ElcRoot
		{
			get
			{
				return new LocalizedString("ElcRoot", "Ex1873C0", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004C4C File Offset: 0x00002E4C
		public static LocalizedString CalendarWhenMonthlyThEveryOtherMonth(LocalizedString order, IFormattable day)
		{
			return new LocalizedString("CalendarWhenMonthlyThEveryOtherMonth", "ExF258AC", false, true, ClientStrings.ResourceManager, new object[]
			{
				order,
				day
			});
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004C84 File Offset: 0x00002E84
		public static LocalizedString ContactFullNameFormat
		{
			get
			{
				return new LocalizedString("ContactFullNameFormat", "ExBD0F67", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004CA4 File Offset: 0x00002EA4
		public static LocalizedString AlternateCalendarTaskWhenYearly(LocalizedString calendar, IFormattable month, int day)
		{
			return new LocalizedString("AlternateCalendarTaskWhenYearly", "Ex0D998D", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				month,
				day
			});
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004CE5 File Offset: 0x00002EE5
		public static LocalizedString GroupMailboxAddedMemberMessageCalendar1
		{
			get
			{
				return new LocalizedString("GroupMailboxAddedMemberMessageCalendar1", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004D03 File Offset: 0x00002F03
		public static LocalizedString WhenFifth
		{
			get
			{
				return new LocalizedString("WhenFifth", "Ex476F6D", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004D21 File Offset: 0x00002F21
		public static LocalizedString ClutterNotificationInvitationIntro
		{
			get
			{
				return new LocalizedString("ClutterNotificationInvitationIntro", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004D3F File Offset: 0x00002F3F
		public static LocalizedString ExpiresKqlKeyword
		{
			get
			{
				return new LocalizedString("ExpiresKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004D60 File Offset: 0x00002F60
		public static LocalizedString WhenRecurringNoEndDateDaysOneHourMinutes(LocalizedString pattern, object startDate, object startTime, int days, int minutes)
		{
			return new LocalizedString("WhenRecurringNoEndDateDaysOneHourMinutes", "Ex558993", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				startTime,
				days,
				minutes
			});
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004DAF File Offset: 0x00002FAF
		public static LocalizedString GroupMailboxAddedMemberMessageConversation2
		{
			get
			{
				return new LocalizedString("GroupMailboxAddedMemberMessageConversation2", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004DCD File Offset: 0x00002FCD
		public static LocalizedString CommunicatorHistoryFolderName
		{
			get
			{
				return new LocalizedString("CommunicatorHistoryFolderName", "ExD56E0C", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004DEC File Offset: 0x00002FEC
		public static LocalizedString WhenRecurringWithEndDateOneDayNoDuration(LocalizedString pattern, object startDate, object endDate, object startTime)
		{
			return new LocalizedString("WhenRecurringWithEndDateOneDayNoDuration", "ExC6260D", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate,
				startTime
			});
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004E2C File Offset: 0x0000302C
		public static LocalizedString LowKqlKeyword
		{
			get
			{
				return new LocalizedString("LowKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004E4A File Offset: 0x0000304A
		public static LocalizedString AllKqlKeyword
		{
			get
			{
				return new LocalizedString("AllKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004E68 File Offset: 0x00003068
		public static LocalizedString GroupMailboxAddedMemberToPublicGroupMessage
		{
			get
			{
				return new LocalizedString("GroupMailboxAddedMemberToPublicGroupMessage", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004E86 File Offset: 0x00003086
		public static LocalizedString Followup
		{
			get
			{
				return new LocalizedString("Followup", "Ex32BF1B", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004EA4 File Offset: 0x000030A4
		public static LocalizedString HebrewLunar
		{
			get
			{
				return new LocalizedString("HebrewLunar", "Ex2A9AA3", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004EC2 File Offset: 0x000030C2
		public static LocalizedString Notes
		{
			get
			{
				return new LocalizedString("Notes", "ExE296E8", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004EE0 File Offset: 0x000030E0
		public static LocalizedString GroupSubscriptionPageSubscribeFailedMaxSubscribers(string groupName)
		{
			return new LocalizedString("GroupSubscriptionPageSubscribeFailedMaxSubscribers", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				groupName
			});
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004F10 File Offset: 0x00003110
		public static LocalizedString WhenRecurringWithEndDateOneDayHoursMinutes(LocalizedString pattern, object startDate, object endDate, object startTime, int hours, int minutes)
		{
			return new LocalizedString("WhenRecurringWithEndDateOneDayHoursMinutes", "ExA34C75", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate,
				startTime,
				hours,
				minutes
			});
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004F64 File Offset: 0x00003164
		public static LocalizedString UpdateRumEndTimeFlag
		{
			get
			{
				return new LocalizedString("UpdateRumEndTimeFlag", "Ex035A43", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004F82 File Offset: 0x00003182
		public static LocalizedString JournalsKqlKeyword
		{
			get
			{
				return new LocalizedString("JournalsKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004FA0 File Offset: 0x000031A0
		public static LocalizedString SharingICal(string icalurl)
		{
			return new LocalizedString("SharingICal", "ExB5F831", false, true, ClientStrings.ResourceManager, new object[]
			{
				icalurl
			});
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004FCF File Offset: 0x000031CF
		public static LocalizedString AttendeeInquiryRumDescription
		{
			get
			{
				return new LocalizedString("AttendeeInquiryRumDescription", "Ex1DA182", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004FED File Offset: 0x000031ED
		public static LocalizedString ClutterNotificationOptInHeader
		{
			get
			{
				return new LocalizedString("ClutterNotificationOptInHeader", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000500B File Offset: 0x0000320B
		public static LocalizedString LegacyPDLFakeEntry
		{
			get
			{
				return new LocalizedString("LegacyPDLFakeEntry", "ExE78DB7", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00005029 File Offset: 0x00003229
		public static LocalizedString PolicyTipDefaultMessageReject
		{
			get
			{
				return new LocalizedString("PolicyTipDefaultMessageReject", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00005047 File Offset: 0x00003247
		public static LocalizedString VoicemailKqlKeyword
		{
			get
			{
				return new LocalizedString("VoicemailKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00005065 File Offset: 0x00003265
		public static LocalizedString PostedOn
		{
			get
			{
				return new LocalizedString("PostedOn", "Ex06826A", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00005083 File Offset: 0x00003283
		public static LocalizedString MeetingCancel
		{
			get
			{
				return new LocalizedString("MeetingCancel", "ExEF9EF1", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000050A1 File Offset: 0x000032A1
		public static LocalizedString FromColon
		{
			get
			{
				return new LocalizedString("FromColon", "Ex804C4B", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000050BF File Offset: 0x000032BF
		public static LocalizedString DeletedItems
		{
			get
			{
				return new LocalizedString("DeletedItems", "Ex86E37A", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000050E0 File Offset: 0x000032E0
		public static LocalizedString SharingAnonymous(string user, LocalizedString foldertype, string foldername, string browseurl)
		{
			return new LocalizedString("SharingAnonymous", "ExE57B1A", false, true, ClientStrings.ResourceManager, new object[]
			{
				user,
				foldertype,
				foldername,
				browseurl
			});
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005120 File Offset: 0x00003320
		public static LocalizedString AlternateCalendarTaskWhenMonthlyThEveryOtherMonth(LocalizedString calendar, LocalizedString order, IFormattable day)
		{
			return new LocalizedString("AlternateCalendarTaskWhenMonthlyThEveryOtherMonth", "ExA2E77C", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				order,
				day
			});
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005161 File Offset: 0x00003361
		public static LocalizedString DocsKqlKeyword
		{
			get
			{
				return new LocalizedString("DocsKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005180 File Offset: 0x00003380
		public static LocalizedString WhenRecurringNoEndDateNoDuration(LocalizedString pattern, object startDate, object startTime)
		{
			return new LocalizedString("WhenRecurringNoEndDateNoDuration", "ExF8844E", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				startTime
			});
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000051BC File Offset: 0x000033BC
		public static LocalizedString MdnNotRead(LocalizedString messageInfo, object dateTime, LocalizedString timeZoneName)
		{
			return new LocalizedString("MdnNotRead", "Ex6E8C2A", false, true, ClientStrings.ResourceManager, new object[]
			{
				messageInfo,
				dateTime,
				timeZoneName
			});
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000051FD File Offset: 0x000033FD
		public static LocalizedString ClutterNotificationO365DisplayName
		{
			get
			{
				return new LocalizedString("ClutterNotificationO365DisplayName", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000521B File Offset: 0x0000341B
		public static LocalizedString LocalFailures
		{
			get
			{
				return new LocalizedString("LocalFailures", "Ex54C8EC", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00005239 File Offset: 0x00003439
		public static LocalizedString GroupMailboxWelcomeEmailO365FooterBrowseConversations
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailO365FooterBrowseConversations", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00005257 File Offset: 0x00003457
		public static LocalizedString JapaneseLunar
		{
			get
			{
				return new LocalizedString("JapaneseLunar", "Ex682E5C", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005278 File Offset: 0x00003478
		public static LocalizedString GroupMailboxWelcomeEmailSecondaryHeaderYouJoined(string groupName)
		{
			return new LocalizedString("GroupMailboxWelcomeEmailSecondaryHeaderYouJoined", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				groupName
			});
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000052A7 File Offset: 0x000034A7
		public static LocalizedString ClutterNotificationPeriodicSurveySteps
		{
			get
			{
				return new LocalizedString("ClutterNotificationPeriodicSurveySteps", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000052C5 File Offset: 0x000034C5
		public static LocalizedString TaskStatusInProgress
		{
			get
			{
				return new LocalizedString("TaskStatusInProgress", "Ex1C5C27", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000052E4 File Offset: 0x000034E4
		public static LocalizedString AlternateCalendarWhenMonthlyThEveryMonth(LocalizedString calendar, LocalizedString order, IFormattable day)
		{
			return new LocalizedString("AlternateCalendarWhenMonthlyThEveryMonth", "Ex1B4E51", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				order,
				day
			});
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005328 File Offset: 0x00003528
		public static LocalizedString GroupSubscriptionUnsubscribeInfoHtml(string groupName, string link)
		{
			return new LocalizedString("GroupSubscriptionUnsubscribeInfoHtml", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				groupName,
				link
			});
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000535B File Offset: 0x0000355B
		public static LocalizedString AutomaticDisplayName
		{
			get
			{
				return new LocalizedString("AutomaticDisplayName", "Ex084271", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005379 File Offset: 0x00003579
		public static LocalizedString SharingInvitationInstruction
		{
			get
			{
				return new LocalizedString("SharingInvitationInstruction", "Ex04F28A", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005398 File Offset: 0x00003598
		public static LocalizedString GroupSubscriptionPageSubscribeFailedNotAMember(string groupName)
		{
			return new LocalizedString("GroupSubscriptionPageSubscribeFailedNotAMember", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				groupName
			});
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000053C8 File Offset: 0x000035C8
		public static LocalizedString WhenRecurringNoEndDateDaysOneHour(LocalizedString pattern, object startDate, object startTime, int days)
		{
			return new LocalizedString("WhenRecurringNoEndDateDaysOneHour", "ExE54279", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				startTime,
				days
			});
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000540D File Offset: 0x0000360D
		public static LocalizedString BirthdayCalendarFolderName
		{
			get
			{
				return new LocalizedString("BirthdayCalendarFolderName", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000542B File Offset: 0x0000362B
		public static LocalizedString NormalKqlKeyword
		{
			get
			{
				return new LocalizedString("NormalKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005449 File Offset: 0x00003649
		public static LocalizedString ClutterNotificationPeriodicCheckBack
		{
			get
			{
				return new LocalizedString("ClutterNotificationPeriodicCheckBack", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005468 File Offset: 0x00003668
		public static LocalizedString WhenSixDaysOfWeek(object day1, object day2, object day3, object day4, object day5, object day6)
		{
			return new LocalizedString("WhenSixDaysOfWeek", "Ex5EC036", false, true, ClientStrings.ResourceManager, new object[]
			{
				day1,
				day2,
				day3,
				day4,
				day5,
				day6
			});
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000054B0 File Offset: 0x000036B0
		public static LocalizedString AlternateCalendarWhenYearly(LocalizedString calendar, IFormattable month, int day)
		{
			return new LocalizedString("AlternateCalendarWhenYearly", "ExFDD6D7", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				month,
				day
			});
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000054F4 File Offset: 0x000036F4
		public static LocalizedString WhenTwoDaysOfWeek(object day1, object day2)
		{
			return new LocalizedString("WhenTwoDaysOfWeek", "Ex8009C4", false, true, ClientStrings.ResourceManager, new object[]
			{
				day1,
				day2
			});
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005527 File Offset: 0x00003727
		public static LocalizedString ToKqlKeyword
		{
			get
			{
				return new LocalizedString("ToKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005548 File Offset: 0x00003748
		public static LocalizedString SharingRequest(string user, string email, LocalizedString foldertype)
		{
			return new LocalizedString("SharingRequest", "Ex0A9AF7", false, true, ClientStrings.ResourceManager, new object[]
			{
				user,
				email,
				foldertype
			});
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005584 File Offset: 0x00003784
		public static LocalizedString JointStrings(LocalizedString str1, LocalizedString str2)
		{
			return new LocalizedString("JointStrings", "Ex4C0B94", false, true, ClientStrings.ResourceManager, new object[]
			{
				str1,
				str2
			});
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000055C1 File Offset: 0x000037C1
		public static LocalizedString Drafts
		{
			get
			{
				return new LocalizedString("Drafts", "Ex573013", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000055E0 File Offset: 0x000037E0
		public static LocalizedString UserPhotoImageTooSmall(int min)
		{
			return new LocalizedString("UserPhotoImageTooSmall", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				min
			});
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005614 File Offset: 0x00003814
		public static LocalizedString MeetingDecline
		{
			get
			{
				return new LocalizedString("MeetingDecline", "Ex8FCFE3", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005632 File Offset: 0x00003832
		public static LocalizedString WhenSecond
		{
			get
			{
				return new LocalizedString("WhenSecond", "Ex7FD30B", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005650 File Offset: 0x00003850
		public static LocalizedString SubjectKqlKeyword
		{
			get
			{
				return new LocalizedString("SubjectKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005670 File Offset: 0x00003870
		public static LocalizedString GroupSubscriptionPageUnsubscribed(string groupName)
		{
			return new LocalizedString("GroupSubscriptionPageUnsubscribed", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				groupName
			});
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000569F File Offset: 0x0000389F
		public static LocalizedString ServerFailures
		{
			get
			{
				return new LocalizedString("ServerFailures", "ExFB6DB7", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000056BD File Offset: 0x000038BD
		public static LocalizedString ClutterNotificationInvitationHeader
		{
			get
			{
				return new LocalizedString("ClutterNotificationInvitationHeader", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000056DC File Offset: 0x000038DC
		public static LocalizedString AlternateCalendarTaskWhenMonthlyThEveryNMonths(LocalizedString calendar, LocalizedString order, IFormattable day, int interval)
		{
			return new LocalizedString("AlternateCalendarTaskWhenMonthlyThEveryNMonths", "Ex9DEDEE", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				order,
				day,
				interval
			});
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005728 File Offset: 0x00003928
		public static LocalizedString ClutterNotificationOptInFeedback(string feedbackMailtoUrl)
		{
			return new LocalizedString("ClutterNotificationOptInFeedback", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				feedbackMailtoUrl
			});
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005757 File Offset: 0x00003957
		public static LocalizedString UMVoiceMailFolderName
		{
			get
			{
				return new LocalizedString("UMVoiceMailFolderName", "ExA1DE2B", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00005775 File Offset: 0x00003975
		public static LocalizedString CalendarWhenDailyEveryDay
		{
			get
			{
				return new LocalizedString("CalendarWhenDailyEveryDay", "Ex862A92", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005793 File Offset: 0x00003993
		public static LocalizedString WhenThird
		{
			get
			{
				return new LocalizedString("WhenThird", "Ex3BC251", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000057B1 File Offset: 0x000039B1
		public static LocalizedString ClutterNotificationOptInPrivacy
		{
			get
			{
				return new LocalizedString("ClutterNotificationOptInPrivacy", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000057D0 File Offset: 0x000039D0
		public static LocalizedString WhenFourDaysOfWeek(object day1, object day2, object day3, object day4)
		{
			return new LocalizedString("WhenFourDaysOfWeek", "Ex69B20B", false, true, ClientStrings.ResourceManager, new object[]
			{
				day1,
				day2,
				day3,
				day4
			});
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000580B File Offset: 0x00003A0B
		public static LocalizedString SharingInvitationUpdatedSubjectLine
		{
			get
			{
				return new LocalizedString("SharingInvitationUpdatedSubjectLine", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000582C File Offset: 0x00003A2C
		public static LocalizedString CalendarWhenMonthlyThEveryNMonths(LocalizedString order, IFormattable day, int interval)
		{
			return new LocalizedString("CalendarWhenMonthlyThEveryNMonths", "ExCD657F", false, true, ClientStrings.ResourceManager, new object[]
			{
				order,
				day,
				interval
			});
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000586D File Offset: 0x00003A6D
		public static LocalizedString OtherCalendars
		{
			get
			{
				return new LocalizedString("OtherCalendars", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000588C File Offset: 0x00003A8C
		public static LocalizedString ClutterNotificationOptInIntro(string clutterFolderName)
		{
			return new LocalizedString("ClutterNotificationOptInIntro", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				clutterFolderName
			});
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000058BC File Offset: 0x00003ABC
		public static LocalizedString SharingInvitation(string user, string email, LocalizedString foldertype)
		{
			return new LocalizedString("SharingInvitation", "ExAF24EA", false, true, ClientStrings.ResourceManager, new object[]
			{
				user,
				email,
				foldertype
			});
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000058F8 File Offset: 0x00003AF8
		public static LocalizedString GroupMailboxWelcomeEmailSubscribeToInboxSubtitle
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailSubscribeToInboxSubtitle", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005916 File Offset: 0x00003B16
		public static LocalizedString Tentative
		{
			get
			{
				return new LocalizedString("Tentative", "Ex49F338", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005934 File Offset: 0x00003B34
		public static LocalizedString CalendarWhenYearlyTh(LocalizedString order, IFormattable dayOfWeek, IFormattable month)
		{
			return new LocalizedString("CalendarWhenYearlyTh", "Ex5A2275", false, true, ClientStrings.ResourceManager, new object[]
			{
				order,
				dayOfWeek,
				month
			});
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005970 File Offset: 0x00003B70
		public static LocalizedString FailedToSynchronizeMembershipFromSharePointText(string sharePointUrl, string error)
		{
			return new LocalizedString("FailedToSynchronizeMembershipFromSharePointText", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				sharePointUrl,
				error
			});
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000059A3 File Offset: 0x00003BA3
		public static LocalizedString AttachmentKqlKeyword
		{
			get
			{
				return new LocalizedString("AttachmentKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000059C1 File Offset: 0x00003BC1
		public static LocalizedString WhenOnWeekDays
		{
			get
			{
				return new LocalizedString("WhenOnWeekDays", "Ex298111", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000059E0 File Offset: 0x00003BE0
		public static LocalizedString TaskWhenMonthlyEveryOtherMonth(int day)
		{
			return new LocalizedString("TaskWhenMonthlyEveryOtherMonth", "Ex34620A", false, true, ClientStrings.ResourceManager, new object[]
			{
				day
			});
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005A14 File Offset: 0x00003C14
		public static LocalizedString SharingInvitationNonPrimary(string user, string email, string foldername, LocalizedString foldertype)
		{
			return new LocalizedString("SharingInvitationNonPrimary", "Ex74C6C0", false, true, ClientStrings.ResourceManager, new object[]
			{
				user,
				email,
				foldername,
				foldertype
			});
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005A54 File Offset: 0x00003C54
		public static LocalizedString WhenSevenDaysOfWeek(object day1, object day2, object day3, object day4, object day5, object day6, object day7)
		{
			return new LocalizedString("WhenSevenDaysOfWeek", "ExFFC7B4", false, true, ClientStrings.ResourceManager, new object[]
			{
				day1,
				day2,
				day3,
				day4,
				day5,
				day6,
				day7
			});
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005AA0 File Offset: 0x00003CA0
		public static LocalizedString GroupMailboxAddedMemberMessageHeader(string userName)
		{
			return new LocalizedString("GroupMailboxAddedMemberMessageHeader", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005ACF File Offset: 0x00003CCF
		public static LocalizedString ClutterNotificationHeaderFont
		{
			get
			{
				return new LocalizedString("ClutterNotificationHeaderFont", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005AED File Offset: 0x00003CED
		public static LocalizedString TaskWhenWeeklyRegeneratingPattern
		{
			get
			{
				return new LocalizedString("TaskWhenWeeklyRegeneratingPattern", "Ex279C6D", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005B0B File Offset: 0x00003D0B
		public static LocalizedString FaxesKqlKeyword
		{
			get
			{
				return new LocalizedString("FaxesKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005B29 File Offset: 0x00003D29
		public static LocalizedString TaskStatusNotStarted
		{
			get
			{
				return new LocalizedString("TaskStatusNotStarted", "Ex6F7061", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005B47 File Offset: 0x00003D47
		public static LocalizedString ToColon
		{
			get
			{
				return new LocalizedString("ToColon", "Ex2F3835", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005B65 File Offset: 0x00003D65
		public static LocalizedString IsReadKqlKeyword
		{
			get
			{
				return new LocalizedString("IsReadKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005B83 File Offset: 0x00003D83
		public static LocalizedString IsFlaggedKqlKeyword
		{
			get
			{
				return new LocalizedString("IsFlaggedKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005BA1 File Offset: 0x00003DA1
		public static LocalizedString CancellationRumTitle
		{
			get
			{
				return new LocalizedString("CancellationRumTitle", "Ex5D18C8", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005BBF File Offset: 0x00003DBF
		public static LocalizedString ClutterNotificationPeriodicHeader
		{
			get
			{
				return new LocalizedString("ClutterNotificationPeriodicHeader", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public static LocalizedString WhenOneDayOfWeek(object day1)
		{
			return new LocalizedString("WhenOneDayOfWeek", "ExFBF8CE", false, true, ClientStrings.ResourceManager, new object[]
			{
				day1
			});
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005C0F File Offset: 0x00003E0F
		public static LocalizedString GroupSubscriptionPageRequestFailedInfo
		{
			get
			{
				return new LocalizedString("GroupSubscriptionPageRequestFailedInfo", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005C2D File Offset: 0x00003E2D
		public static LocalizedString ClutterNotificationInvitationSubject
		{
			get
			{
				return new LocalizedString("ClutterNotificationInvitationSubject", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005C4B File Offset: 0x00003E4B
		public static LocalizedString FromKqlKeyword
		{
			get
			{
				return new LocalizedString("FromKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005C69 File Offset: 0x00003E69
		public static LocalizedString GroupSubscriptionPageUnsubscribedInfo
		{
			get
			{
				return new LocalizedString("GroupSubscriptionPageUnsubscribedInfo", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00005C87 File Offset: 0x00003E87
		public static LocalizedString GroupMailboxAddedMemberToPrivateGroupMessage
		{
			get
			{
				return new LocalizedString("GroupMailboxAddedMemberToPrivateGroupMessage", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005CA8 File Offset: 0x00003EA8
		public static LocalizedString AlternateCalendarTaskWhenMonthlyEveryOtherMonth(LocalizedString calendar, int day)
		{
			return new LocalizedString("AlternateCalendarTaskWhenMonthlyEveryOtherMonth", "Ex1A371B", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				day
			});
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00005CE5 File Offset: 0x00003EE5
		public static LocalizedString WhenOneMoreOccurrence
		{
			get
			{
				return new LocalizedString("WhenOneMoreOccurrence", "Ex130EC1", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005D03 File Offset: 0x00003F03
		public static LocalizedString GroupSubscriptionUnsubscribeLinkWord
		{
			get
			{
				return new LocalizedString("GroupSubscriptionUnsubscribeLinkWord", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005D21 File Offset: 0x00003F21
		public static LocalizedString PublicFolderMailboxDumpsterInfo
		{
			get
			{
				return new LocalizedString("PublicFolderMailboxDumpsterInfo", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005D40 File Offset: 0x00003F40
		public static LocalizedString CalendarWhenDailyEveryNDays(int interval)
		{
			return new LocalizedString("CalendarWhenDailyEveryNDays", "ExCAD81F", false, true, ClientStrings.ResourceManager, new object[]
			{
				interval
			});
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005D74 File Offset: 0x00003F74
		public static LocalizedString TaskStatusWaitOnOthers
		{
			get
			{
				return new LocalizedString("TaskStatusWaitOnOthers", "Ex66CFEB", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005D92 File Offset: 0x00003F92
		public static LocalizedString GroupMailboxWelcomeEmailO365FooterBrowseShareFiles
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailO365FooterBrowseShareFiles", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public static LocalizedString CalendarWhenMonthlyEveryOtherMonth(int day)
		{
			return new LocalizedString("CalendarWhenMonthlyEveryOtherMonth", "Ex7FC862", false, true, ClientStrings.ResourceManager, new object[]
			{
				day
			});
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public static LocalizedString ClutterNotificationPeriodicSubject
		{
			get
			{
				return new LocalizedString("ClutterNotificationPeriodicSubject", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005E02 File Offset: 0x00004002
		public static LocalizedString GroupMailboxAddedMemberNoJoinedByMessageHeader
		{
			get
			{
				return new LocalizedString("GroupMailboxAddedMemberNoJoinedByMessageHeader", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005E20 File Offset: 0x00004020
		public static LocalizedString FailedToSynchronizeHierarchyChangesFromSharePointText(string siteUrl, string error)
		{
			return new LocalizedString("FailedToSynchronizeHierarchyChangesFromSharePointText", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				siteUrl,
				error
			});
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005E53 File Offset: 0x00004053
		public static LocalizedString RequestedActionNoResponseNecessary
		{
			get
			{
				return new LocalizedString("RequestedActionNoResponseNecessary", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005E74 File Offset: 0x00004074
		public static LocalizedString RpcClientRequestError(string error)
		{
			return new LocalizedString("RpcClientRequestError", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005EA4 File Offset: 0x000040A4
		public static LocalizedString WhenRecurringNoEndDateOneDayHours(LocalizedString pattern, object startDate, object startTime, int hours)
		{
			return new LocalizedString("WhenRecurringNoEndDateOneDayHours", "Ex924D65", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				startTime,
				hours
			});
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005EEC File Offset: 0x000040EC
		public static LocalizedString WhenRecurringWithEndDateDaysMinutes(LocalizedString pattern, object startDate, object endDate, object startTime, int days, int minutes)
		{
			return new LocalizedString("WhenRecurringWithEndDateDaysMinutes", "Ex0A0761", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate,
				startTime,
				days,
				minutes
			});
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005F40 File Offset: 0x00004140
		public static LocalizedString SharingDecline(string user, string email, LocalizedString foldertype)
		{
			return new LocalizedString("SharingDecline", "Ex3767B2", false, true, ClientStrings.ResourceManager, new object[]
			{
				user,
				email,
				foldertype
			});
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005F7C File Offset: 0x0000417C
		public static LocalizedString TaskWhenYearlyRegeneratingPattern
		{
			get
			{
				return new LocalizedString("TaskWhenYearlyRegeneratingPattern", "ExECFD1E", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005F9A File Offset: 0x0000419A
		public static LocalizedString Free
		{
			get
			{
				return new LocalizedString("Free", "ExA3A029", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005FB8 File Offset: 0x000041B8
		public static LocalizedString CancellationRumDescription
		{
			get
			{
				return new LocalizedString("CancellationRumDescription", "Ex62915A", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005FD6 File Offset: 0x000041D6
		public static LocalizedString RequestedActionReplyToAll
		{
			get
			{
				return new LocalizedString("RequestedActionReplyToAll", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005FF4 File Offset: 0x000041F4
		public static LocalizedString GroupMailboxWelcomeEmailO365FooterTitle
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailO365FooterTitle", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006014 File Offset: 0x00004214
		public static LocalizedString ClutterNotificationTakeSurveyDeepLink(string surveyUrl)
		{
			return new LocalizedString("ClutterNotificationTakeSurveyDeepLink", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				surveyUrl
			});
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006043 File Offset: 0x00004243
		public static LocalizedString ItemReply
		{
			get
			{
				return new LocalizedString("ItemReply", "ExB56A1F", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00006061 File Offset: 0x00004261
		public static LocalizedString WhenFirst
		{
			get
			{
				return new LocalizedString("WhenFirst", "Ex265D19", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006080 File Offset: 0x00004280
		public static LocalizedString AlternateCalendarTaskWhenMonthlyEveryMonth(LocalizedString calendar, int day)
		{
			return new LocalizedString("AlternateCalendarTaskWhenMonthlyEveryMonth", "Ex8E0C57", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				day
			});
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000060C0 File Offset: 0x000042C0
		public static LocalizedString TaskWhenDailyEveryNDays(int interval)
		{
			return new LocalizedString("TaskWhenDailyEveryNDays", "Ex1D8736", false, true, ClientStrings.ResourceManager, new object[]
			{
				interval
			});
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000060F4 File Offset: 0x000042F4
		public static LocalizedString RpcClientInitError
		{
			get
			{
				return new LocalizedString("RpcClientInitError", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00006112 File Offset: 0x00004312
		public static LocalizedString RequestedActionDoNotForward
		{
			get
			{
				return new LocalizedString("RequestedActionDoNotForward", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006130 File Offset: 0x00004330
		public static LocalizedString SizeKqlKeyword
		{
			get
			{
				return new LocalizedString("SizeKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000614E File Offset: 0x0000434E
		public static LocalizedString GroupSubscriptionPagePrivateGroupInfo
		{
			get
			{
				return new LocalizedString("GroupSubscriptionPagePrivateGroupInfo", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000616C File Offset: 0x0000436C
		public static LocalizedString UserPhotoNotFound
		{
			get
			{
				return new LocalizedString("UserPhotoNotFound", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000618A File Offset: 0x0000438A
		public static LocalizedString FromFavoriteSendersFolderName
		{
			get
			{
				return new LocalizedString("FromFavoriteSendersFolderName", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000061A8 File Offset: 0x000043A8
		public static LocalizedString GroupMailboxAddedMemberMessageFont
		{
			get
			{
				return new LocalizedString("GroupMailboxAddedMemberMessageFont", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000061C6 File Offset: 0x000043C6
		public static LocalizedString RequestedActionForward
		{
			get
			{
				return new LocalizedString("RequestedActionForward", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000061E4 File Offset: 0x000043E4
		public static LocalizedString CcKqlKeyword
		{
			get
			{
				return new LocalizedString("CcKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006204 File Offset: 0x00004404
		public static LocalizedString TaskWhenNMonthsRegeneratingPattern(int months)
		{
			return new LocalizedString("TaskWhenNMonthsRegeneratingPattern", "ExD7271C", false, true, ClientStrings.ResourceManager, new object[]
			{
				months
			});
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006238 File Offset: 0x00004438
		public static LocalizedString Conversations
		{
			get
			{
				return new LocalizedString("Conversations", "Ex43F63E", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00006256 File Offset: 0x00004456
		public static LocalizedString GroupSubscriptionPagePublicGroupInfo
		{
			get
			{
				return new LocalizedString("GroupSubscriptionPagePublicGroupInfo", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006274 File Offset: 0x00004474
		public static LocalizedString ContactsKqlKeyword
		{
			get
			{
				return new LocalizedString("ContactsKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00006292 File Offset: 0x00004492
		public static LocalizedString WhenBothWeekendDays
		{
			get
			{
				return new LocalizedString("WhenBothWeekendDays", "ExE8A92D", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000062B0 File Offset: 0x000044B0
		public static LocalizedString Conflicts
		{
			get
			{
				return new LocalizedString("Conflicts", "Ex623D81", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000062CE File Offset: 0x000044CE
		public static LocalizedString PostsKqlKeyword
		{
			get
			{
				return new LocalizedString("PostsKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000062EC File Offset: 0x000044EC
		public static LocalizedString GroupMailboxAddedMemberMessageTitle(string groupName)
		{
			return new LocalizedString("GroupMailboxAddedMemberMessageTitle", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				groupName
			});
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000631B File Offset: 0x0000451B
		public static LocalizedString UserPhotoPreviewNotFound
		{
			get
			{
				return new LocalizedString("UserPhotoPreviewNotFound", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00006339 File Offset: 0x00004539
		public static LocalizedString WhenEveryDay
		{
			get
			{
				return new LocalizedString("WhenEveryDay", "Ex239A05", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006358 File Offset: 0x00004558
		public static LocalizedString WhenNMoreOccurrences(int numberOccurrence)
		{
			return new LocalizedString("WhenNMoreOccurrences", "Ex25956F", false, true, ClientStrings.ResourceManager, new object[]
			{
				numberOccurrence
			});
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000638C File Offset: 0x0000458C
		public static LocalizedString SharingRequestInstruction
		{
			get
			{
				return new LocalizedString("SharingRequestInstruction", "Ex77A584", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000063AC File Offset: 0x000045AC
		public static LocalizedString GroupMailboxJoinRequestEmailBody(string attachedMessage)
		{
			return new LocalizedString("GroupMailboxJoinRequestEmailBody", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				attachedMessage
			});
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000063DB File Offset: 0x000045DB
		public static LocalizedString ClutterNotificationAutoEnablementNoticeIntro
		{
			get
			{
				return new LocalizedString("ClutterNotificationAutoEnablementNoticeIntro", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000063FC File Offset: 0x000045FC
		public static LocalizedString AlternateCalendarTaskWhenYearlyThLeap(LocalizedString calendar, LocalizedString order, IFormattable dayOfWeek, IFormattable month)
		{
			return new LocalizedString("AlternateCalendarTaskWhenYearlyThLeap", "ExF91A57", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				order,
				dayOfWeek,
				month
			});
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00006441 File Offset: 0x00004641
		public static LocalizedString ClutterNotificationBodyFont
		{
			get
			{
				return new LocalizedString("ClutterNotificationBodyFont", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006460 File Offset: 0x00004660
		public static LocalizedString ClutterNotificationInvitationLearnMore(string clutterLearnMoreUrl)
		{
			return new LocalizedString("ClutterNotificationInvitationLearnMore", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				clutterLearnMoreUrl
			});
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006490 File Offset: 0x00004690
		public static LocalizedString CalendarWhenMonthlyThEveryMonth(LocalizedString order, IFormattable day)
		{
			return new LocalizedString("CalendarWhenMonthlyThEveryMonth", "Ex1CF625", false, true, ClientStrings.ResourceManager, new object[]
			{
				order,
				day
			});
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000064C8 File Offset: 0x000046C8
		public static LocalizedString ClutterNotificationInvitationO365Helps
		{
			get
			{
				return new LocalizedString("ClutterNotificationInvitationO365Helps", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000064E6 File Offset: 0x000046E6
		public static LocalizedString WhenFourth
		{
			get
			{
				return new LocalizedString("WhenFourth", "Ex641093", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006504 File Offset: 0x00004704
		public static LocalizedString AlternateCalendarTaskWhenYearlyTh(LocalizedString calendar, LocalizedString order, IFormattable dayOfWeek, IFormattable month)
		{
			return new LocalizedString("AlternateCalendarTaskWhenYearlyTh", "Ex794037", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				order,
				dayOfWeek,
				month
			});
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000654C File Offset: 0x0000474C
		public static LocalizedString TaskWhenMonthlyThEveryNMonths(LocalizedString order, IFormattable day, int interval)
		{
			return new LocalizedString("TaskWhenMonthlyThEveryNMonths", "Ex68FD6B", false, true, ClientStrings.ResourceManager, new object[]
			{
				order,
				day,
				interval
			});
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006590 File Offset: 0x00004790
		public static LocalizedString WhenRecurringWithEndDateOneDayMinutes(LocalizedString pattern, object startDate, object endDate, object startTime, int minutes)
		{
			return new LocalizedString("WhenRecurringWithEndDateOneDayMinutes", "Ex649875", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate,
				startTime,
				minutes
			});
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000065DA File Offset: 0x000047DA
		public static LocalizedString RequestedActionFollowUp
		{
			get
			{
				return new LocalizedString("RequestedActionFollowUp", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000065F8 File Offset: 0x000047F8
		public static LocalizedString GroupSubscriptionPageRequestFailed
		{
			get
			{
				return new LocalizedString("GroupSubscriptionPageRequestFailed", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006618 File Offset: 0x00004818
		public static LocalizedString WhenRecurringWithEndDateOneDayOneHourMinutes(LocalizedString pattern, object startDate, object endDate, object startTime, int minutes)
		{
			return new LocalizedString("WhenRecurringWithEndDateOneDayOneHourMinutes", "Ex20EE81", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate,
				startTime,
				minutes
			});
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00006662 File Offset: 0x00004862
		public static LocalizedString ClutterNotificationAutoEnablementNoticeSubject
		{
			get
			{
				return new LocalizedString("ClutterNotificationAutoEnablementNoticeSubject", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006680 File Offset: 0x00004880
		public static LocalizedString GroupMailboxWelcomeMessageHeader1(string groupName)
		{
			return new LocalizedString("GroupMailboxWelcomeMessageHeader1", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				groupName
			});
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000066B0 File Offset: 0x000048B0
		public static LocalizedString WhenRecurringNoEndDateDaysMinutes(LocalizedString pattern, object startDate, object startTime, int days, int minutes)
		{
			return new LocalizedString("WhenRecurringNoEndDateDaysMinutes", "ExCF9103", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				startTime,
				days,
				minutes
			});
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000066FF File Offset: 0x000048FF
		public static LocalizedString PeoplesCalendars
		{
			get
			{
				return new LocalizedString("PeoplesCalendars", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000671D File Offset: 0x0000491D
		public static LocalizedString EHAMigration
		{
			get
			{
				return new LocalizedString("EHAMigration", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000673C File Offset: 0x0000493C
		public static LocalizedString AlternateCalendarWhenYearlyTh(LocalizedString calendar, LocalizedString order, IFormattable dayOfWeek, IFormattable month)
		{
			return new LocalizedString("AlternateCalendarWhenYearlyTh", "Ex5C8400", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				order,
				dayOfWeek,
				month
			});
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006781 File Offset: 0x00004981
		public static LocalizedString SubjectColon
		{
			get
			{
				return new LocalizedString("SubjectColon", "Ex6FE519", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000067A0 File Offset: 0x000049A0
		public static LocalizedString CalendarWhenMonthlyEveryNMonths(int day, int interval)
		{
			return new LocalizedString("CalendarWhenMonthlyEveryNMonths", "Ex3B3883", false, true, ClientStrings.ResourceManager, new object[]
			{
				day,
				interval
			});
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000067DD File Offset: 0x000049DD
		public static LocalizedString Calendar
		{
			get
			{
				return new LocalizedString("Calendar", "Ex7A113F", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000067FB File Offset: 0x000049FB
		public static LocalizedString RequestedActionAny
		{
			get
			{
				return new LocalizedString("RequestedActionAny", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006819 File Offset: 0x00004A19
		public static LocalizedString WhenEveryWeekDay
		{
			get
			{
				return new LocalizedString("WhenEveryWeekDay", "ExC63BFC", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006838 File Offset: 0x00004A38
		public static LocalizedString TaskWhenYearly(IFormattable month, int day)
		{
			return new LocalizedString("TaskWhenYearly", "Ex421B2D", false, true, ClientStrings.ResourceManager, new object[]
			{
				month,
				day
			});
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006870 File Offset: 0x00004A70
		public static LocalizedString TaskWhenMonthlyEveryNMonths(int day, int interval)
		{
			return new LocalizedString("TaskWhenMonthlyEveryNMonths", "ExD14484", false, true, ClientStrings.ResourceManager, new object[]
			{
				day,
				interval
			});
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000068AD File Offset: 0x00004AAD
		public static LocalizedString TasksKqlKeyword
		{
			get
			{
				return new LocalizedString("TasksKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000068CC File Offset: 0x00004ACC
		public static LocalizedString WhenStartsEndsSameDay(object startDate, object startTime, object endTime)
		{
			return new LocalizedString("WhenStartsEndsSameDay", "Ex85CF71", false, true, ClientStrings.ResourceManager, new object[]
			{
				startDate,
				startTime,
				endTime
			});
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006904 File Offset: 0x00004B04
		public static LocalizedString FailedToSynchronizeMembershipFromSharePoint(string sharePointUrl)
		{
			return new LocalizedString("FailedToSynchronizeMembershipFromSharePoint", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				sharePointUrl
			});
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006934 File Offset: 0x00004B34
		public static LocalizedString GroupSubscriptionPageSubscribed(string groupName)
		{
			return new LocalizedString("GroupSubscriptionPageSubscribed", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				groupName
			});
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006963 File Offset: 0x00004B63
		public static LocalizedString NotesKqlKeyword
		{
			get
			{
				return new LocalizedString("NotesKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006981 File Offset: 0x00004B81
		public static LocalizedString TaskStatusCompleted
		{
			get
			{
				return new LocalizedString("TaskStatusCompleted", "ExD86FAF", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000069A0 File Offset: 0x00004BA0
		public static LocalizedString RumFooter(string version)
		{
			return new LocalizedString("RumFooter", "Ex6501CD", false, true, ClientStrings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000069D0 File Offset: 0x00004BD0
		public static LocalizedString FailedToDeleteFromSharePointErrorText(string fileName, string sharePointFolderUrl)
		{
			return new LocalizedString("FailedToDeleteFromSharePointErrorText", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				fileName,
				sharePointFolderUrl
			});
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006A03 File Offset: 0x00004C03
		public static LocalizedString ClutterNotificationInvitationItsAutomatic
		{
			get
			{
				return new LocalizedString("ClutterNotificationInvitationItsAutomatic", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006A21 File Offset: 0x00004C21
		public static LocalizedString RssSubscriptions
		{
			get
			{
				return new LocalizedString("RssSubscriptions", "ExD1870D", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006A3F File Offset: 0x00004C3F
		public static LocalizedString RequestedActionCall
		{
			get
			{
				return new LocalizedString("RequestedActionCall", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006A60 File Offset: 0x00004C60
		public static LocalizedString TaskWhenNDaysRegeneratingPattern(int days)
		{
			return new LocalizedString("TaskWhenNDaysRegeneratingPattern", "Ex3CC5D3", false, true, ClientStrings.ResourceManager, new object[]
			{
				days
			});
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006A94 File Offset: 0x00004C94
		public static LocalizedString PolicyTipDefaultMessageNotifyOnly
		{
			get
			{
				return new LocalizedString("PolicyTipDefaultMessageNotifyOnly", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006AB2 File Offset: 0x00004CB2
		public static LocalizedString FailedToUploadToSharePointTitle
		{
			get
			{
				return new LocalizedString("FailedToUploadToSharePointTitle", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public static LocalizedString TaskWhenWeeklyEveryAlterateWeek(IFormattable dayOfWeek)
		{
			return new LocalizedString("TaskWhenWeeklyEveryAlterateWeek", "ExFD87C4", false, true, ClientStrings.ResourceManager, new object[]
			{
				dayOfWeek
			});
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006B00 File Offset: 0x00004D00
		public static LocalizedString PublicFolderMailboxInfoFolderEnumeration(int current, int total)
		{
			return new LocalizedString("PublicFolderMailboxInfoFolderEnumeration", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				current,
				total
			});
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006B40 File Offset: 0x00004D40
		public static LocalizedString ClutterNotificationPeriodicLearnMore(string clutterLearnMoreUrl)
		{
			return new LocalizedString("ClutterNotificationPeriodicLearnMore", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				clutterLearnMoreUrl
			});
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00006B6F File Offset: 0x00004D6F
		public static LocalizedString TaskStatusDeferred
		{
			get
			{
				return new LocalizedString("TaskStatusDeferred", "Ex7BEE4A", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006B90 File Offset: 0x00004D90
		public static LocalizedString MissingItemRumDescription(string version)
		{
			return new LocalizedString("MissingItemRumDescription", "Ex5C85B5", false, true, ClientStrings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00006BBF File Offset: 0x00004DBF
		public static LocalizedString SharingRequestAllowed
		{
			get
			{
				return new LocalizedString("SharingRequestAllowed", "Ex78AF25", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006BE0 File Offset: 0x00004DE0
		public static LocalizedString WhenRecurringNoEndDate(LocalizedString pattern, object startDate, object startTime, object endTime)
		{
			return new LocalizedString("WhenRecurringNoEndDate", "Ex89790F", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				startTime,
				endTime
			});
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006C20 File Offset: 0x00004E20
		public static LocalizedString ClutterNotificationEnableDeepLink(string enableUrl)
		{
			return new LocalizedString("ClutterNotificationEnableDeepLink", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				enableUrl
			});
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00006C4F File Offset: 0x00004E4F
		public static LocalizedString UpdateRumMissingItemFlag
		{
			get
			{
				return new LocalizedString("UpdateRumMissingItemFlag", "Ex3B3373", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00006C6D File Offset: 0x00004E6D
		public static LocalizedString ParticipantsKqlKeyword
		{
			get
			{
				return new LocalizedString("ParticipantsKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006C8C File Offset: 0x00004E8C
		public static LocalizedString AlternateCalendarWhenMonthlyThEveryNMonths(LocalizedString calendar, LocalizedString order, IFormattable day, int interval)
		{
			return new LocalizedString("AlternateCalendarWhenMonthlyThEveryNMonths", "ExCBAD9A", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				order,
				day,
				interval
			});
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006CD6 File Offset: 0x00004ED6
		public static LocalizedString GroupMailboxWelcomeEmailStartConversationTitle
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailStartConversationTitle", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00006CF4 File Offset: 0x00004EF4
		public static LocalizedString GroupMailboxWelcomeEmailShareFilesTitle
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailShareFilesTitle", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00006D12 File Offset: 0x00004F12
		public static LocalizedString LegacyArchiveJournals
		{
			get
			{
				return new LocalizedString("LegacyArchiveJournals", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006D30 File Offset: 0x00004F30
		public static LocalizedString TaskWhenMonthlyThEveryMonth(LocalizedString order, IFormattable day)
		{
			return new LocalizedString("TaskWhenMonthlyThEveryMonth", "Ex2BEA98", false, true, ClientStrings.ResourceManager, new object[]
			{
				order,
				day
			});
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006D68 File Offset: 0x00004F68
		public static LocalizedString RecipientsKqlKeyword
		{
			get
			{
				return new LocalizedString("RecipientsKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006D88 File Offset: 0x00004F88
		public static LocalizedString WhenRecurringNoEndDateOneDayMinutes(LocalizedString pattern, object startDate, object startTime, int minutes)
		{
			return new LocalizedString("WhenRecurringNoEndDateOneDayMinutes", "Ex9AB11F", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				startTime,
				minutes
			});
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006DCD File Offset: 0x00004FCD
		public static LocalizedString RssFeedsKqlKeyword
		{
			get
			{
				return new LocalizedString("RssFeedsKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00006DEB File Offset: 0x00004FEB
		public static LocalizedString GroupMailboxWelcomeEmailSubscribeToInboxTitle
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailSubscribeToInboxTitle", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00006E09 File Offset: 0x00005009
		public static LocalizedString GroupMailboxAddedMemberMessageCalendar3
		{
			get
			{
				return new LocalizedString("GroupMailboxAddedMemberMessageCalendar3", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00006E27 File Offset: 0x00005027
		public static LocalizedString RequestedActionReview
		{
			get
			{
				return new LocalizedString("RequestedActionReview", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00006E45 File Offset: 0x00005045
		public static LocalizedString KindKqlKeyword
		{
			get
			{
				return new LocalizedString("KindKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00006E63 File Offset: 0x00005063
		public static LocalizedString EmailKqlKeyword
		{
			get
			{
				return new LocalizedString("EmailKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006E81 File Offset: 0x00005081
		public static LocalizedString UTC
		{
			get
			{
				return new LocalizedString("UTC", "ExBFFB60", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006EA0 File Offset: 0x000050A0
		public static LocalizedString WhenRecurringNoEndDateOneDayNoDuration(LocalizedString pattern, object startDate, object startTime)
		{
			return new LocalizedString("WhenRecurringNoEndDateOneDayNoDuration", "ExE6F8BA", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				startTime
			});
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006EDC File Offset: 0x000050DC
		public static LocalizedString WhenRecurringNoEndDateDaysNoDuration(LocalizedString pattern, object startDate, object startTime, int days)
		{
			return new LocalizedString("WhenRecurringNoEndDateDaysNoDuration", "Ex3BA4A0", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				startTime,
				days
			});
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00006F21 File Offset: 0x00005121
		public static LocalizedString BccKqlKeyword
		{
			get
			{
				return new LocalizedString("BccKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006F40 File Offset: 0x00005140
		public static LocalizedString WhenRecurringWithEndDateDaysHours(LocalizedString pattern, object startDate, object endDate, object startTime, int days, int hours)
		{
			return new LocalizedString("WhenRecurringWithEndDateDaysHours", "Ex87F34E", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate,
				startTime,
				days,
				hours
			});
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006F94 File Offset: 0x00005194
		public static LocalizedString ErrorSharePointSiteHasNoValidUrl(string url)
		{
			return new LocalizedString("ErrorSharePointSiteHasNoValidUrl", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00006FC3 File Offset: 0x000051C3
		public static LocalizedString RequestedActionForYourInformation
		{
			get
			{
				return new LocalizedString("RequestedActionForYourInformation", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00006FE1 File Offset: 0x000051E1
		public static LocalizedString Journal
		{
			get
			{
				return new LocalizedString("Journal", "Ex72205E", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00006FFF File Offset: 0x000051FF
		public static LocalizedString GroupMailboxAddedMemberMessageConversation1
		{
			get
			{
				return new LocalizedString("GroupMailboxAddedMemberMessageConversation1", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000701D File Offset: 0x0000521D
		public static LocalizedString GroupMailboxAddedMemberMessageConversation3
		{
			get
			{
				return new LocalizedString("GroupMailboxAddedMemberMessageConversation3", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000703C File Offset: 0x0000523C
		public static LocalizedString TaskWhenNYearsRegeneratingPattern(int years)
		{
			return new LocalizedString("TaskWhenNYearsRegeneratingPattern", "ExF1A6C8", false, true, ClientStrings.ResourceManager, new object[]
			{
				years
			});
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007070 File Offset: 0x00005270
		public static LocalizedString UserPhotoFileTooSmall(int min)
		{
			return new LocalizedString("UserPhotoFileTooSmall", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				min
			});
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000070A4 File Offset: 0x000052A4
		public static LocalizedString HasAttachmentKqlKeyword
		{
			get
			{
				return new LocalizedString("HasAttachmentKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000070C2 File Offset: 0x000052C2
		public static LocalizedString GroupSubscriptionPageGroupInfoFont
		{
			get
			{
				return new LocalizedString("GroupSubscriptionPageGroupInfoFont", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000070E0 File Offset: 0x000052E0
		public static LocalizedString TaskWhenMonthlyThEveryOtherMonth(LocalizedString order, IFormattable day)
		{
			return new LocalizedString("TaskWhenMonthlyThEveryOtherMonth", "Ex4B4F6E", false, true, ClientStrings.ResourceManager, new object[]
			{
				order,
				day
			});
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00007118 File Offset: 0x00005318
		public static LocalizedString Contacts
		{
			get
			{
				return new LocalizedString("Contacts", "ExD0E0B2", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007138 File Offset: 0x00005338
		public static LocalizedString GroupSubscriptionUnsubscribeInfoText(string groupName, string link)
		{
			return new LocalizedString("GroupSubscriptionUnsubscribeInfoText", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				groupName,
				link
			});
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000716C File Offset: 0x0000536C
		public static LocalizedString WhenRecurringWithEndDate(LocalizedString pattern, object startDate, object endDate, object startTime, object endTime)
		{
			return new LocalizedString("WhenRecurringWithEndDate", "Ex99B4D6", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate,
				startTime,
				endTime
			});
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000071B4 File Offset: 0x000053B4
		public static LocalizedString WhenRecurringNoEndDateOneDayOneHourMinutes(LocalizedString pattern, object startDate, object startTime, int minutes)
		{
			return new LocalizedString("WhenRecurringNoEndDateOneDayOneHourMinutes", "Ex70B707", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				startTime,
				minutes
			});
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000071F9 File Offset: 0x000053F9
		public static LocalizedString GroupMailboxAddedMemberMessageCalendar2
		{
			get
			{
				return new LocalizedString("GroupMailboxAddedMemberMessageCalendar2", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00007217 File Offset: 0x00005417
		public static LocalizedString FavoritesFolderName
		{
			get
			{
				return new LocalizedString("FavoritesFolderName", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00007235 File Offset: 0x00005435
		public static LocalizedString ImKqlKeyword
		{
			get
			{
				return new LocalizedString("ImKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007254 File Offset: 0x00005454
		public static LocalizedString GroupMailboxWelcomeEmailSecondaryHeaderAddedBy(string addedBy, string groupName)
		{
			return new LocalizedString("GroupMailboxWelcomeEmailSecondaryHeaderAddedBy", "", false, false, ClientStrings.ResourceManager, new object[]
			{
				addedBy,
				groupName
			});
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00007287 File Offset: 0x00005487
		public static LocalizedString ImportanceKqlKeyword
		{
			get
			{
				return new LocalizedString("ImportanceKqlKeyword", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000072A5 File Offset: 0x000054A5
		public static LocalizedString MyTasks
		{
			get
			{
				return new LocalizedString("MyTasks", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000072C4 File Offset: 0x000054C4
		public static LocalizedString AlternateCalendarWhenYearlyLeap(LocalizedString calendar, IFormattable month, int day)
		{
			return new LocalizedString("AlternateCalendarWhenYearlyLeap", "Ex488312", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				month,
				day
			});
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007308 File Offset: 0x00005508
		public static LocalizedString WhenRecurringWithEndDateOneDayHours(LocalizedString pattern, object startDate, object endDate, object startTime, int hours)
		{
			return new LocalizedString("WhenRecurringWithEndDateOneDayHours", "Ex481B2E", false, true, ClientStrings.ResourceManager, new object[]
			{
				pattern,
				startDate,
				endDate,
				startTime,
				hours
			});
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00007352 File Offset: 0x00005552
		public static LocalizedString SharingInvitationAndRequestInstruction
		{
			get
			{
				return new LocalizedString("SharingInvitationAndRequestInstruction", "Ex85F32F", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007370 File Offset: 0x00005570
		public static LocalizedString WhenStartsEndsDifferentDay(object startDate, object startTime, object endDate, object endTime)
		{
			return new LocalizedString("WhenStartsEndsDifferentDay", "Ex9BF390", false, true, ClientStrings.ResourceManager, new object[]
			{
				startDate,
				startTime,
				endDate,
				endTime
			});
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000073AB File Offset: 0x000055AB
		public static LocalizedString TaskWhenEveryOtherDay
		{
			get
			{
				return new LocalizedString("TaskWhenEveryOtherDay", "ExB24AF6", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000073C9 File Offset: 0x000055C9
		public static LocalizedString Conversation
		{
			get
			{
				return new LocalizedString("Conversation", "ExED2DA4", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000073E7 File Offset: 0x000055E7
		public static LocalizedString FailedToDeleteFromSharePointTitle
		{
			get
			{
				return new LocalizedString("FailedToDeleteFromSharePointTitle", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007408 File Offset: 0x00005608
		public static LocalizedString AlternateCalendarTaskWhenMonthlyEveryNMonths(LocalizedString calendar, int day, int interval)
		{
			return new LocalizedString("AlternateCalendarTaskWhenMonthlyEveryNMonths", "Ex791D9D", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				day,
				interval
			});
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007450 File Offset: 0x00005650
		public static LocalizedString AlternateCalendarWhenMonthlyThEveryOtherMonth(LocalizedString calendar, LocalizedString order, IFormattable day)
		{
			return new LocalizedString("AlternateCalendarWhenMonthlyThEveryOtherMonth", "Ex23A3FF", false, true, ClientStrings.ResourceManager, new object[]
			{
				calendar,
				order,
				day
			});
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00007491 File Offset: 0x00005691
		public static LocalizedString JunkEmail
		{
			get
			{
				return new LocalizedString("JunkEmail", "Ex9A426E", false, true, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000074AF File Offset: 0x000056AF
		public static LocalizedString GroupMailboxWelcomeEmailFooterSubscribeDescriptionText
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailFooterSubscribeDescriptionText", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000074CD File Offset: 0x000056CD
		public static LocalizedString ClutterFolderName
		{
			get
			{
				return new LocalizedString("ClutterFolderName", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000074EC File Offset: 0x000056EC
		public static LocalizedString TaskWhenWeeklyEveryNWeeks(int interval, IFormattable dayOfWeek)
		{
			return new LocalizedString("TaskWhenWeeklyEveryNWeeks", "Ex45C71F", false, true, ClientStrings.ResourceManager, new object[]
			{
				interval,
				dayOfWeek
			});
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007524 File Offset: 0x00005724
		public static LocalizedString ClutterNotificationInvitationWeCallIt
		{
			get
			{
				return new LocalizedString("ClutterNotificationInvitationWeCallIt", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007542 File Offset: 0x00005742
		public static LocalizedString GroupMailboxWelcomeEmailDefaultDescription
		{
			get
			{
				return new LocalizedString("GroupMailboxWelcomeEmailDefaultDescription", "", false, false, ClientStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007560 File Offset: 0x00005760
		public static LocalizedString GetLocalizedString(ClientStrings.IDs key)
		{
			return new LocalizedString(ClientStrings.stringIDs[(uint)key], ClientStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(251);

		// Token: 0x04000002 RID: 2
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Data.Storage.ClientStrings", typeof(ClientStrings).GetTypeInfo().Assembly);

		// Token: 0x02000003 RID: 3
		public enum IDs : uint
		{
			// Token: 0x04000004 RID: 4
			PostedTo = 4109493280U,
			// Token: 0x04000005 RID: 5
			UnknownDelegateUser = 3201544700U,
			// Token: 0x04000006 RID: 6
			ClutterNotificationInvitationIfYouDontLikeIt = 2520011618U,
			// Token: 0x04000007 RID: 7
			Inbox = 2979702410U,
			// Token: 0x04000008 RID: 8
			CategoryKqlKeyword = 2660925179U,
			// Token: 0x04000009 RID: 9
			RequestedActionReply = 2842725608U,
			// Token: 0x0400000A RID: 10
			ClutterNotificationAutoEnablementNoticeHeader = 699667745U,
			// Token: 0x0400000B RID: 11
			MeetingTentative = 605024017U,
			// Token: 0x0400000C RID: 12
			BodyKqlKeyword = 3115038347U,
			// Token: 0x0400000D RID: 13
			WhenAllDays = 2958797346U,
			// Token: 0x0400000E RID: 14
			GroupMailboxWelcomeEmailFooterUnsubscribeLinkText = 1567502238U,
			// Token: 0x0400000F RID: 15
			TaskWhenDailyRegeneratingPattern = 800057761U,
			// Token: 0x04000010 RID: 16
			UpdateRumLocationFlag = 1082093008U,
			// Token: 0x04000011 RID: 17
			CcColon = 3496891301U,
			// Token: 0x04000012 RID: 18
			GroupMailboxWelcomeEmailUnsubscribeToInboxTitle = 3728294467U,
			// Token: 0x04000013 RID: 19
			HighKqlKeyword = 729677225U,
			// Token: 0x04000014 RID: 20
			OnBehalfOf = 2868846894U,
			// Token: 0x04000015 RID: 21
			SentKqlKeyword = 3636275243U,
			// Token: 0x04000016 RID: 22
			UpdateRumCancellationFlag = 996097864U,
			// Token: 0x04000017 RID: 23
			PrivateAppointmentSubject = 2420384142U,
			// Token: 0x04000018 RID: 24
			GroupMailboxWelcomeMessageHeader2 = 2485040215U,
			// Token: 0x04000019 RID: 25
			Busy = 2052801377U,
			// Token: 0x0400001A RID: 26
			CalendarWhenEveryOtherDay = 2694011047U,
			// Token: 0x0400001B RID: 27
			WhenWeekDays = 2530535283U,
			// Token: 0x0400001C RID: 28
			WhenOnEveryDayOfTheWeek = 2983597720U,
			// Token: 0x0400001D RID: 29
			ClutterNotificationAutoEnablementNoticeHowItWorks = 186518193U,
			// Token: 0x0400001E RID: 30
			RequestedActionRead = 1289378056U,
			// Token: 0x0400001F RID: 31
			GroupMailboxWelcomeEmailPublicTypeText = 3076491009U,
			// Token: 0x04000020 RID: 32
			PolicyTipDefaultMessageRejectOverride = 4206219086U,
			// Token: 0x04000021 RID: 33
			WhenLast = 3798065398U,
			// Token: 0x04000022 RID: 34
			GroupMailboxWelcomeEmailUnsubscribeToInboxSubtitle = 920038251U,
			// Token: 0x04000023 RID: 35
			WhenPart = 3798064771U,
			// Token: 0x04000024 RID: 36
			GroupMailboxAddedMemberMessageDocument2 = 998340857U,
			// Token: 0x04000025 RID: 37
			UpdateRumStartTimeFlag = 3514687136U,
			// Token: 0x04000026 RID: 38
			MyContactsFolderName = 3831476238U,
			// Token: 0x04000027 RID: 39
			DocumentSyncIssues = 2537597608U,
			// Token: 0x04000028 RID: 40
			MeetingAccept = 2892702617U,
			// Token: 0x04000029 RID: 41
			SearchFolders = 3827225687U,
			// Token: 0x0400002A RID: 42
			OOF = 77678270U,
			// Token: 0x0400002B RID: 43
			GroupMailboxWelcomeEmailFooterUnsubscribeDescirptionText = 28323196U,
			// Token: 0x0400002C RID: 44
			GroupMailboxWelcomeEmailShareFilesSubTitle = 3435327809U,
			// Token: 0x0400002D RID: 45
			GroupMailboxWelcomeEmailPrivateTypeText = 2986714461U,
			// Token: 0x0400002E RID: 46
			GroupMailboxWelcomeEmailFooterSubscribeLinkText = 4003125105U,
			// Token: 0x0400002F RID: 47
			SentColon = 295620541U,
			// Token: 0x04000030 RID: 48
			ClutterNotificationO365Closing = 109016526U,
			// Token: 0x04000031 RID: 49
			MyCalendars = 606093953U,
			// Token: 0x04000032 RID: 50
			AttachmentNamesKqlKeyword = 2337243514U,
			// Token: 0x04000033 RID: 51
			UMFaxFolderName = 3055530828U,
			// Token: 0x04000034 RID: 52
			GroupMailboxSuggestToSubscribe = 4038690134U,
			// Token: 0x04000035 RID: 53
			WherePart = 3709219576U,
			// Token: 0x04000036 RID: 54
			MeetingProposedNewTime = 2985148172U,
			// Token: 0x04000037 RID: 55
			Tasks = 2966158940U,
			// Token: 0x04000038 RID: 56
			ClutterNotificationAutoEnablementNoticeWeCallIt = 2957657555U,
			// Token: 0x04000039 RID: 57
			Configuration = 2243726652U,
			// Token: 0x0400003A RID: 58
			ReceivedKqlKeyword = 2687792634U,
			// Token: 0x0400003B RID: 59
			ItemForward = 2676822854U,
			// Token: 0x0400003C RID: 60
			PublicFolderMailboxHierarchyInfo = 4037918816U,
			// Token: 0x0400003D RID: 61
			GroupMailboxAddedMemberMessageDocument1 = 998340860U,
			// Token: 0x0400003E RID: 62
			GroupMailboxAddedSelfMessageHeader = 1139539543U,
			// Token: 0x0400003F RID: 63
			GroupSubscriptionPageSubscribedInfo = 627342781U,
			// Token: 0x04000040 RID: 64
			TaskWhenMonthlyRegeneratingPattern = 2377878649U,
			// Token: 0x04000041 RID: 65
			SentTime = 2677919833U,
			// Token: 0x04000042 RID: 66
			SentItems = 590977256U,
			// Token: 0x04000043 RID: 67
			UpdateRumDuplicateFlags = 931240115U,
			// Token: 0x04000044 RID: 68
			ToDoSearch = 3416726118U,
			// Token: 0x04000045 RID: 69
			GroupMailboxWelcomeEmailO365FooterBrowseViewCalendar = 831219486U,
			// Token: 0x04000046 RID: 70
			UserPhotoNotAnImage = 586169060U,
			// Token: 0x04000047 RID: 71
			OofReply = 51726234U,
			// Token: 0x04000048 RID: 72
			UpdateRumDescription = 2992898169U,
			// Token: 0x04000049 RID: 73
			SharingRequestDenied = 1190007884U,
			// Token: 0x0400004A RID: 74
			CommonViews = 3198424631U,
			// Token: 0x0400004B RID: 75
			TaskWhenDailyEveryDay = 257954985U,
			// Token: 0x0400004C RID: 76
			NoDataAvailable = 2561496264U,
			// Token: 0x0400004D RID: 77
			Root = 3430669166U,
			// Token: 0x0400004E RID: 78
			KoreanLunar = 3011322582U,
			// Token: 0x0400004F RID: 79
			SyncIssues = 3694564633U,
			// Token: 0x04000050 RID: 80
			UnifiedInbox = 2696977948U,
			// Token: 0x04000051 RID: 81
			ContactSubjectFormat = 843955115U,
			// Token: 0x04000052 RID: 82
			ChineseLunar = 2093606811U,
			// Token: 0x04000053 RID: 83
			Outbox = 629464291U,
			// Token: 0x04000054 RID: 84
			UpdateRumInconsistencyFlagsLabel = 2692137911U,
			// Token: 0x04000055 RID: 85
			Hijri = 2173140516U,
			// Token: 0x04000056 RID: 86
			MeetingsKqlKeyword = 946577161U,
			// Token: 0x04000057 RID: 87
			ClutterNotificationInvitationHowItWorks = 2717587388U,
			// Token: 0x04000058 RID: 88
			GroupMailboxAddedMemberMessageDocument3 = 998340858U,
			// Token: 0x04000059 RID: 89
			UpdateRumRecurrenceFlag = 732114395U,
			// Token: 0x0400005A RID: 90
			PolicyTagKqlKeyword = 1261883245U,
			// Token: 0x0400005B RID: 91
			GroupSubscriptionPageBodyFont = 3061781790U,
			// Token: 0x0400005C RID: 92
			ClutterNotificationOptInSubject = 780435470U,
			// Token: 0x0400005D RID: 93
			ClutterNotificationAutoEnablementNoticeItsAutomatic = 2831541605U,
			// Token: 0x0400005E RID: 94
			WorkingElsewhere = 1987690819U,
			// Token: 0x0400005F RID: 95
			NTUser = 1210128587U,
			// Token: 0x04000060 RID: 96
			ElcRoot = 3623590716U,
			// Token: 0x04000061 RID: 97
			ContactFullNameFormat = 3637574683U,
			// Token: 0x04000062 RID: 98
			GroupMailboxAddedMemberMessageCalendar1 = 2442729051U,
			// Token: 0x04000063 RID: 99
			WhenFifth = 3064946485U,
			// Token: 0x04000064 RID: 100
			ClutterNotificationInvitationIntro = 690865655U,
			// Token: 0x04000065 RID: 101
			ExpiresKqlKeyword = 725603143U,
			// Token: 0x04000066 RID: 102
			GroupMailboxAddedMemberMessageConversation2 = 1486137843U,
			// Token: 0x04000067 RID: 103
			CommunicatorHistoryFolderName = 462355316U,
			// Token: 0x04000068 RID: 104
			LowKqlKeyword = 898888055U,
			// Token: 0x04000069 RID: 105
			AllKqlKeyword = 3493739462U,
			// Token: 0x0400006A RID: 106
			GroupMailboxAddedMemberToPublicGroupMessage = 3753304505U,
			// Token: 0x0400006B RID: 107
			Followup = 1712196986U,
			// Token: 0x0400006C RID: 108
			HebrewLunar = 1669914831U,
			// Token: 0x0400006D RID: 109
			Notes = 1601836855U,
			// Token: 0x0400006E RID: 110
			UpdateRumEndTimeFlag = 4285257241U,
			// Token: 0x0400006F RID: 111
			JournalsKqlKeyword = 4086680553U,
			// Token: 0x04000070 RID: 112
			AttendeeInquiryRumDescription = 2395883303U,
			// Token: 0x04000071 RID: 113
			ClutterNotificationOptInHeader = 1358110507U,
			// Token: 0x04000072 RID: 114
			LegacyPDLFakeEntry = 2274376870U,
			// Token: 0x04000073 RID: 115
			PolicyTipDefaultMessageReject = 2426305242U,
			// Token: 0x04000074 RID: 116
			VoicemailKqlKeyword = 637088748U,
			// Token: 0x04000075 RID: 117
			PostedOn = 2543409328U,
			// Token: 0x04000076 RID: 118
			MeetingCancel = 20079527U,
			// Token: 0x04000077 RID: 119
			FromColon = 2918743951U,
			// Token: 0x04000078 RID: 120
			DeletedItems = 3613623199U,
			// Token: 0x04000079 RID: 121
			DocsKqlKeyword = 242457402U,
			// Token: 0x0400007A RID: 122
			ClutterNotificationO365DisplayName = 3850103120U,
			// Token: 0x0400007B RID: 123
			LocalFailures = 2773211886U,
			// Token: 0x0400007C RID: 124
			GroupMailboxWelcomeEmailO365FooterBrowseConversations = 3659541315U,
			// Token: 0x0400007D RID: 125
			JapaneseLunar = 2676513095U,
			// Token: 0x0400007E RID: 126
			ClutterNotificationPeriodicSurveySteps = 3411057958U,
			// Token: 0x0400007F RID: 127
			TaskStatusInProgress = 3709255463U,
			// Token: 0x04000080 RID: 128
			AutomaticDisplayName = 4078173350U,
			// Token: 0x04000081 RID: 129
			SharingInvitationInstruction = 587246061U,
			// Token: 0x04000082 RID: 130
			BirthdayCalendarFolderName = 2610926242U,
			// Token: 0x04000083 RID: 131
			NormalKqlKeyword = 3519087566U,
			// Token: 0x04000084 RID: 132
			ClutterNotificationPeriodicCheckBack = 2937701464U,
			// Token: 0x04000085 RID: 133
			ToKqlKeyword = 104437952U,
			// Token: 0x04000086 RID: 134
			Drafts = 115734878U,
			// Token: 0x04000087 RID: 135
			MeetingDecline = 3043371929U,
			// Token: 0x04000088 RID: 136
			WhenSecond = 3230188362U,
			// Token: 0x04000089 RID: 137
			SubjectKqlKeyword = 232501731U,
			// Token: 0x0400008A RID: 138
			ServerFailures = 2664647494U,
			// Token: 0x0400008B RID: 139
			ClutterNotificationInvitationHeader = 1134735502U,
			// Token: 0x0400008C RID: 140
			UMVoiceMailFolderName = 1618289846U,
			// Token: 0x0400008D RID: 141
			CalendarWhenDailyEveryDay = 1744667246U,
			// Token: 0x0400008E RID: 142
			WhenThird = 954396011U,
			// Token: 0x0400008F RID: 143
			ClutterNotificationOptInPrivacy = 2864414954U,
			// Token: 0x04000090 RID: 144
			SharingInvitationUpdatedSubjectLine = 1025048278U,
			// Token: 0x04000091 RID: 145
			OtherCalendars = 4052458853U,
			// Token: 0x04000092 RID: 146
			GroupMailboxWelcomeEmailSubscribeToInboxSubtitle = 1178930756U,
			// Token: 0x04000093 RID: 147
			Tentative = 1797669216U,
			// Token: 0x04000094 RID: 148
			AttachmentKqlKeyword = 2411966652U,
			// Token: 0x04000095 RID: 149
			WhenOnWeekDays = 3710484300U,
			// Token: 0x04000096 RID: 150
			ClutterNotificationHeaderFont = 3291021260U,
			// Token: 0x04000097 RID: 151
			TaskWhenWeeklyRegeneratingPattern = 3252707377U,
			// Token: 0x04000098 RID: 152
			FaxesKqlKeyword = 4048827998U,
			// Token: 0x04000099 RID: 153
			TaskStatusNotStarted = 3033231921U,
			// Token: 0x0400009A RID: 154
			ToColon = 3465339554U,
			// Token: 0x0400009B RID: 155
			IsReadKqlKeyword = 3882605011U,
			// Token: 0x0400009C RID: 156
			IsFlaggedKqlKeyword = 2572393147U,
			// Token: 0x0400009D RID: 157
			CancellationRumTitle = 136108241U,
			// Token: 0x0400009E RID: 158
			ClutterNotificationPeriodicHeader = 2244917544U,
			// Token: 0x0400009F RID: 159
			GroupSubscriptionPageRequestFailedInfo = 3855098919U,
			// Token: 0x040000A0 RID: 160
			ClutterNotificationInvitationSubject = 3366209149U,
			// Token: 0x040000A1 RID: 161
			FromKqlKeyword = 2530009445U,
			// Token: 0x040000A2 RID: 162
			GroupSubscriptionPageUnsubscribedInfo = 3938988508U,
			// Token: 0x040000A3 RID: 163
			GroupMailboxAddedMemberToPrivateGroupMessage = 1708086969U,
			// Token: 0x040000A4 RID: 164
			WhenOneMoreOccurrence = 4132923658U,
			// Token: 0x040000A5 RID: 165
			GroupSubscriptionUnsubscribeLinkWord = 4182366305U,
			// Token: 0x040000A6 RID: 166
			PublicFolderMailboxDumpsterInfo = 1233830609U,
			// Token: 0x040000A7 RID: 167
			TaskStatusWaitOnOthers = 2188469818U,
			// Token: 0x040000A8 RID: 168
			GroupMailboxWelcomeEmailO365FooterBrowseShareFiles = 2197783321U,
			// Token: 0x040000A9 RID: 169
			ClutterNotificationPeriodicSubject = 433190147U,
			// Token: 0x040000AA RID: 170
			GroupMailboxAddedMemberNoJoinedByMessageHeader = 2755014636U,
			// Token: 0x040000AB RID: 171
			RequestedActionNoResponseNecessary = 3895923139U,
			// Token: 0x040000AC RID: 172
			TaskWhenYearlyRegeneratingPattern = 1562762538U,
			// Token: 0x040000AD RID: 173
			Free = 3323263744U,
			// Token: 0x040000AE RID: 174
			CancellationRumDescription = 1871485283U,
			// Token: 0x040000AF RID: 175
			RequestedActionReplyToAll = 99688204U,
			// Token: 0x040000B0 RID: 176
			GroupMailboxWelcomeEmailO365FooterTitle = 3733626243U,
			// Token: 0x040000B1 RID: 177
			ItemReply = 2403605275U,
			// Token: 0x040000B2 RID: 178
			WhenFirst = 3824460724U,
			// Token: 0x040000B3 RID: 179
			RpcClientInitError = 483059378U,
			// Token: 0x040000B4 RID: 180
			RequestedActionDoNotForward = 348255911U,
			// Token: 0x040000B5 RID: 181
			SizeKqlKeyword = 2074560464U,
			// Token: 0x040000B6 RID: 182
			GroupSubscriptionPagePrivateGroupInfo = 2936941939U,
			// Token: 0x040000B7 RID: 183
			UserPhotoNotFound = 693971404U,
			// Token: 0x040000B8 RID: 184
			FromFavoriteSendersFolderName = 2768584303U,
			// Token: 0x040000B9 RID: 185
			GroupMailboxAddedMemberMessageFont = 2869025391U,
			// Token: 0x040000BA RID: 186
			RequestedActionForward = 2905210277U,
			// Token: 0x040000BB RID: 187
			CcKqlKeyword = 4167892423U,
			// Token: 0x040000BC RID: 188
			Conversations = 1697583518U,
			// Token: 0x040000BD RID: 189
			GroupSubscriptionPagePublicGroupInfo = 3614445595U,
			// Token: 0x040000BE RID: 190
			ContactsKqlKeyword = 677901016U,
			// Token: 0x040000BF RID: 191
			WhenBothWeekendDays = 3513814141U,
			// Token: 0x040000C0 RID: 192
			Conflicts = 2377377097U,
			// Token: 0x040000C1 RID: 193
			PostsKqlKeyword = 51369050U,
			// Token: 0x040000C2 RID: 194
			UserPhotoPreviewNotFound = 1940443510U,
			// Token: 0x040000C3 RID: 195
			WhenEveryDay = 1236916945U,
			// Token: 0x040000C4 RID: 196
			SharingRequestInstruction = 1844900681U,
			// Token: 0x040000C5 RID: 197
			ClutterNotificationAutoEnablementNoticeIntro = 940910702U,
			// Token: 0x040000C6 RID: 198
			ClutterNotificationBodyFont = 1418871467U,
			// Token: 0x040000C7 RID: 199
			ClutterNotificationInvitationO365Helps = 1685151768U,
			// Token: 0x040000C8 RID: 200
			WhenFourth = 3953967230U,
			// Token: 0x040000C9 RID: 201
			RequestedActionFollowUp = 1657114432U,
			// Token: 0x040000CA RID: 202
			GroupSubscriptionPageRequestFailed = 3221177017U,
			// Token: 0x040000CB RID: 203
			ClutterNotificationAutoEnablementNoticeSubject = 1300788680U,
			// Token: 0x040000CC RID: 204
			PeoplesCalendars = 2028589045U,
			// Token: 0x040000CD RID: 205
			EHAMigration = 1660374630U,
			// Token: 0x040000CE RID: 206
			SubjectColon = 3413891549U,
			// Token: 0x040000CF RID: 207
			Calendar = 1292798904U,
			// Token: 0x040000D0 RID: 208
			RequestedActionAny = 899464318U,
			// Token: 0x040000D1 RID: 209
			WhenEveryWeekDay = 2912044233U,
			// Token: 0x040000D2 RID: 210
			TasksKqlKeyword = 2607340737U,
			// Token: 0x040000D3 RID: 211
			NotesKqlKeyword = 802022316U,
			// Token: 0x040000D4 RID: 212
			TaskStatusCompleted = 1726717990U,
			// Token: 0x040000D5 RID: 213
			ClutterNotificationInvitationItsAutomatic = 3501929050U,
			// Token: 0x040000D6 RID: 214
			RssSubscriptions = 3598244064U,
			// Token: 0x040000D7 RID: 215
			RequestedActionCall = 153688296U,
			// Token: 0x040000D8 RID: 216
			PolicyTipDefaultMessageNotifyOnly = 3795294554U,
			// Token: 0x040000D9 RID: 217
			FailedToUploadToSharePointTitle = 219237725U,
			// Token: 0x040000DA RID: 218
			TaskStatusDeferred = 2255519906U,
			// Token: 0x040000DB RID: 219
			SharingRequestAllowed = 3425704829U,
			// Token: 0x040000DC RID: 220
			UpdateRumMissingItemFlag = 1694200734U,
			// Token: 0x040000DD RID: 221
			ParticipantsKqlKeyword = 963851651U,
			// Token: 0x040000DE RID: 222
			GroupMailboxWelcomeEmailStartConversationTitle = 1044563024U,
			// Token: 0x040000DF RID: 223
			GroupMailboxWelcomeEmailShareFilesTitle = 2833103505U,
			// Token: 0x040000E0 RID: 224
			LegacyArchiveJournals = 3635271833U,
			// Token: 0x040000E1 RID: 225
			RecipientsKqlKeyword = 3126868667U,
			// Token: 0x040000E2 RID: 226
			RssFeedsKqlKeyword = 2904245352U,
			// Token: 0x040000E3 RID: 227
			GroupMailboxWelcomeEmailSubscribeToInboxTitle = 2776152054U,
			// Token: 0x040000E4 RID: 228
			GroupMailboxAddedMemberMessageCalendar3 = 2442729049U,
			// Token: 0x040000E5 RID: 229
			RequestedActionReview = 1616483908U,
			// Token: 0x040000E6 RID: 230
			KindKqlKeyword = 3043633671U,
			// Token: 0x040000E7 RID: 231
			EmailKqlKeyword = 916455203U,
			// Token: 0x040000E8 RID: 232
			UTC = 2450331336U,
			// Token: 0x040000E9 RID: 233
			BccKqlKeyword = 1793503025U,
			// Token: 0x040000EA RID: 234
			RequestedActionForYourInformation = 3029761970U,
			// Token: 0x040000EB RID: 235
			Journal = 4137480277U,
			// Token: 0x040000EC RID: 236
			GroupMailboxAddedMemberMessageConversation1 = 1486137846U,
			// Token: 0x040000ED RID: 237
			GroupMailboxAddedMemberMessageConversation3 = 1486137844U,
			// Token: 0x040000EE RID: 238
			HasAttachmentKqlKeyword = 136364712U,
			// Token: 0x040000EF RID: 239
			GroupSubscriptionPageGroupInfoFont = 1780326221U,
			// Token: 0x040000F0 RID: 240
			Contacts = 1716044995U,
			// Token: 0x040000F1 RID: 241
			GroupMailboxAddedMemberMessageCalendar2 = 2442729048U,
			// Token: 0x040000F2 RID: 242
			FavoritesFolderName = 1836161108U,
			// Token: 0x040000F3 RID: 243
			ImKqlKeyword = 1516506571U,
			// Token: 0x040000F4 RID: 244
			ImportanceKqlKeyword = 2755562843U,
			// Token: 0x040000F5 RID: 245
			MyTasks = 1165698602U,
			// Token: 0x040000F6 RID: 246
			SharingInvitationAndRequestInstruction = 143815687U,
			// Token: 0x040000F7 RID: 247
			TaskWhenEveryOtherDay = 632725474U,
			// Token: 0x040000F8 RID: 248
			Conversation = 710925581U,
			// Token: 0x040000F9 RID: 249
			FailedToDeleteFromSharePointTitle = 3191048500U,
			// Token: 0x040000FA RID: 250
			JunkEmail = 2241039844U,
			// Token: 0x040000FB RID: 251
			GroupMailboxWelcomeEmailFooterSubscribeDescriptionText = 765381967U,
			// Token: 0x040000FC RID: 252
			ClutterFolderName = 1976982380U,
			// Token: 0x040000FD RID: 253
			ClutterNotificationInvitationWeCallIt = 261418034U,
			// Token: 0x040000FE RID: 254
			GroupMailboxWelcomeEmailDefaultDescription = 1524084310U
		}

		// Token: 0x02000004 RID: 4
		private enum ParamIDs
		{
			// Token: 0x04000100 RID: 256
			ClutterNotificationAutoEnablementNoticeYoullBeEnabed,
			// Token: 0x04000101 RID: 257
			FailedToSynchronizeChangesFromSharePoint,
			// Token: 0x04000102 RID: 258
			ClutterNotificationAutoEnablementNoticeLearnMore,
			// Token: 0x04000103 RID: 259
			RemindersSearchFolderName,
			// Token: 0x04000104 RID: 260
			MdnReadNoTime,
			// Token: 0x04000105 RID: 261
			FailedMaintenanceSynchronizationsText,
			// Token: 0x04000106 RID: 262
			CalendarWhenYearly,
			// Token: 0x04000107 RID: 263
			AlternateCalendarWhenMonthlyEveryNMonths,
			// Token: 0x04000108 RID: 264
			AlternateCalendarWhenYearlyThLeap,
			// Token: 0x04000109 RID: 265
			FailedToSynchronizeChangesFromSharePointText,
			// Token: 0x0400010A RID: 266
			ClutterNotificationOptInHowToTrain,
			// Token: 0x0400010B RID: 267
			WhenRecurringNoEndDateDaysHours,
			// Token: 0x0400010C RID: 268
			WhenRecurringWithEndDateNoDuration,
			// Token: 0x0400010D RID: 269
			FailedToUploadToSharePointErrorText,
			// Token: 0x0400010E RID: 270
			SharingInvitationAndRequest,
			// Token: 0x0400010F RID: 271
			WhenFiveDaysOfWeek,
			// Token: 0x04000110 RID: 272
			AlternateCalendarTaskWhenMonthlyThEveryMonth,
			// Token: 0x04000111 RID: 273
			CalendarWhenWeeklyEveryWeek,
			// Token: 0x04000112 RID: 274
			WhenRecurringNoEndDateOneDayHoursMinutes,
			// Token: 0x04000113 RID: 275
			WhenThreeDaysOfWeek,
			// Token: 0x04000114 RID: 276
			AlternateCalendarWhenMonthlyEveryMonth,
			// Token: 0x04000115 RID: 277
			WhenRecurringWithEndDateDaysNoDuration,
			// Token: 0x04000116 RID: 278
			GroupMailboxWelcomeEmailHeader,
			// Token: 0x04000117 RID: 279
			MdnNotReadNoTime,
			// Token: 0x04000118 RID: 280
			WhenRecurringWithEndDateDaysOneHourMinutes,
			// Token: 0x04000119 RID: 281
			WhenRecurringNoEndDateOneDayOneHour,
			// Token: 0x0400011A RID: 282
			GroupMailboxWelcomeMessageSubject,
			// Token: 0x0400011B RID: 283
			GroupMailboxAddedMemberNoJoinedBySubject,
			// Token: 0x0400011C RID: 284
			GroupMailboxAddedMemberMessageSubject,
			// Token: 0x0400011D RID: 285
			AlternateCalendarTaskWhenYearlyLeap,
			// Token: 0x0400011E RID: 286
			WhenRecurringWithEndDateOneDayOneHour,
			// Token: 0x0400011F RID: 287
			GroupMailboxJoinRequestEmailSubject,
			// Token: 0x04000120 RID: 288
			SharingAccept,
			// Token: 0x04000121 RID: 289
			TaskWhenNWeeksRegeneratingPattern,
			// Token: 0x04000122 RID: 290
			AlternateCalendarWhenMonthlyEveryOtherMonth,
			// Token: 0x04000123 RID: 291
			ClutterNotificationOptInLearnMore,
			// Token: 0x04000124 RID: 292
			TaskWhenYearlyTh,
			// Token: 0x04000125 RID: 293
			CalendarWhenWeeklyEveryAlterateWeek,
			// Token: 0x04000126 RID: 294
			TaskWhenMonthlyEveryMonth,
			// Token: 0x04000127 RID: 295
			AppendStrings,
			// Token: 0x04000128 RID: 296
			ClutterNotificationDisableDeepLink,
			// Token: 0x04000129 RID: 297
			CalendarWhenWeeklyEveryNWeeks,
			// Token: 0x0400012A RID: 298
			WhenRecurringWithEndDateNoTimeInDay,
			// Token: 0x0400012B RID: 299
			FailedMaintenanceSynchronizations,
			// Token: 0x0400012C RID: 300
			WhenRecurringNoEndDateNoTimeInDay,
			// Token: 0x0400012D RID: 301
			WhenRecurringWithEndDateDaysOneHour,
			// Token: 0x0400012E RID: 302
			UserPhotoFileTooLarge,
			// Token: 0x0400012F RID: 303
			ClutterNotificationPeriodicIntro,
			// Token: 0x04000130 RID: 304
			WhenRecurringWithEndDateDaysHoursMinutes,
			// Token: 0x04000131 RID: 305
			GroupMailboxWelcomeEmailStartConversationSubtitle,
			// Token: 0x04000132 RID: 306
			MdnRead,
			// Token: 0x04000133 RID: 307
			CalendarWhenMonthlyEveryMonth,
			// Token: 0x04000134 RID: 308
			TaskWhenWeeklyEveryWeek,
			// Token: 0x04000135 RID: 309
			WhenRecurringNoEndDateDaysHoursMinutes,
			// Token: 0x04000136 RID: 310
			GroupMailboxAddedSelfMessageSubject,
			// Token: 0x04000137 RID: 311
			CalendarWhenMonthlyThEveryOtherMonth,
			// Token: 0x04000138 RID: 312
			AlternateCalendarTaskWhenYearly,
			// Token: 0x04000139 RID: 313
			WhenRecurringNoEndDateDaysOneHourMinutes,
			// Token: 0x0400013A RID: 314
			WhenRecurringWithEndDateOneDayNoDuration,
			// Token: 0x0400013B RID: 315
			GroupSubscriptionPageSubscribeFailedMaxSubscribers,
			// Token: 0x0400013C RID: 316
			WhenRecurringWithEndDateOneDayHoursMinutes,
			// Token: 0x0400013D RID: 317
			SharingICal,
			// Token: 0x0400013E RID: 318
			SharingAnonymous,
			// Token: 0x0400013F RID: 319
			AlternateCalendarTaskWhenMonthlyThEveryOtherMonth,
			// Token: 0x04000140 RID: 320
			WhenRecurringNoEndDateNoDuration,
			// Token: 0x04000141 RID: 321
			MdnNotRead,
			// Token: 0x04000142 RID: 322
			GroupMailboxWelcomeEmailSecondaryHeaderYouJoined,
			// Token: 0x04000143 RID: 323
			AlternateCalendarWhenMonthlyThEveryMonth,
			// Token: 0x04000144 RID: 324
			GroupSubscriptionUnsubscribeInfoHtml,
			// Token: 0x04000145 RID: 325
			GroupSubscriptionPageSubscribeFailedNotAMember,
			// Token: 0x04000146 RID: 326
			WhenRecurringNoEndDateDaysOneHour,
			// Token: 0x04000147 RID: 327
			WhenSixDaysOfWeek,
			// Token: 0x04000148 RID: 328
			AlternateCalendarWhenYearly,
			// Token: 0x04000149 RID: 329
			WhenTwoDaysOfWeek,
			// Token: 0x0400014A RID: 330
			SharingRequest,
			// Token: 0x0400014B RID: 331
			JointStrings,
			// Token: 0x0400014C RID: 332
			UserPhotoImageTooSmall,
			// Token: 0x0400014D RID: 333
			GroupSubscriptionPageUnsubscribed,
			// Token: 0x0400014E RID: 334
			AlternateCalendarTaskWhenMonthlyThEveryNMonths,
			// Token: 0x0400014F RID: 335
			ClutterNotificationOptInFeedback,
			// Token: 0x04000150 RID: 336
			WhenFourDaysOfWeek,
			// Token: 0x04000151 RID: 337
			CalendarWhenMonthlyThEveryNMonths,
			// Token: 0x04000152 RID: 338
			ClutterNotificationOptInIntro,
			// Token: 0x04000153 RID: 339
			SharingInvitation,
			// Token: 0x04000154 RID: 340
			CalendarWhenYearlyTh,
			// Token: 0x04000155 RID: 341
			FailedToSynchronizeMembershipFromSharePointText,
			// Token: 0x04000156 RID: 342
			TaskWhenMonthlyEveryOtherMonth,
			// Token: 0x04000157 RID: 343
			SharingInvitationNonPrimary,
			// Token: 0x04000158 RID: 344
			WhenSevenDaysOfWeek,
			// Token: 0x04000159 RID: 345
			GroupMailboxAddedMemberMessageHeader,
			// Token: 0x0400015A RID: 346
			WhenOneDayOfWeek,
			// Token: 0x0400015B RID: 347
			AlternateCalendarTaskWhenMonthlyEveryOtherMonth,
			// Token: 0x0400015C RID: 348
			CalendarWhenDailyEveryNDays,
			// Token: 0x0400015D RID: 349
			CalendarWhenMonthlyEveryOtherMonth,
			// Token: 0x0400015E RID: 350
			FailedToSynchronizeHierarchyChangesFromSharePointText,
			// Token: 0x0400015F RID: 351
			RpcClientRequestError,
			// Token: 0x04000160 RID: 352
			WhenRecurringNoEndDateOneDayHours,
			// Token: 0x04000161 RID: 353
			WhenRecurringWithEndDateDaysMinutes,
			// Token: 0x04000162 RID: 354
			SharingDecline,
			// Token: 0x04000163 RID: 355
			ClutterNotificationTakeSurveyDeepLink,
			// Token: 0x04000164 RID: 356
			AlternateCalendarTaskWhenMonthlyEveryMonth,
			// Token: 0x04000165 RID: 357
			TaskWhenDailyEveryNDays,
			// Token: 0x04000166 RID: 358
			TaskWhenNMonthsRegeneratingPattern,
			// Token: 0x04000167 RID: 359
			GroupMailboxAddedMemberMessageTitle,
			// Token: 0x04000168 RID: 360
			WhenNMoreOccurrences,
			// Token: 0x04000169 RID: 361
			GroupMailboxJoinRequestEmailBody,
			// Token: 0x0400016A RID: 362
			AlternateCalendarTaskWhenYearlyThLeap,
			// Token: 0x0400016B RID: 363
			ClutterNotificationInvitationLearnMore,
			// Token: 0x0400016C RID: 364
			CalendarWhenMonthlyThEveryMonth,
			// Token: 0x0400016D RID: 365
			AlternateCalendarTaskWhenYearlyTh,
			// Token: 0x0400016E RID: 366
			TaskWhenMonthlyThEveryNMonths,
			// Token: 0x0400016F RID: 367
			WhenRecurringWithEndDateOneDayMinutes,
			// Token: 0x04000170 RID: 368
			WhenRecurringWithEndDateOneDayOneHourMinutes,
			// Token: 0x04000171 RID: 369
			GroupMailboxWelcomeMessageHeader1,
			// Token: 0x04000172 RID: 370
			WhenRecurringNoEndDateDaysMinutes,
			// Token: 0x04000173 RID: 371
			AlternateCalendarWhenYearlyTh,
			// Token: 0x04000174 RID: 372
			CalendarWhenMonthlyEveryNMonths,
			// Token: 0x04000175 RID: 373
			TaskWhenYearly,
			// Token: 0x04000176 RID: 374
			TaskWhenMonthlyEveryNMonths,
			// Token: 0x04000177 RID: 375
			WhenStartsEndsSameDay,
			// Token: 0x04000178 RID: 376
			FailedToSynchronizeMembershipFromSharePoint,
			// Token: 0x04000179 RID: 377
			GroupSubscriptionPageSubscribed,
			// Token: 0x0400017A RID: 378
			RumFooter,
			// Token: 0x0400017B RID: 379
			FailedToDeleteFromSharePointErrorText,
			// Token: 0x0400017C RID: 380
			TaskWhenNDaysRegeneratingPattern,
			// Token: 0x0400017D RID: 381
			TaskWhenWeeklyEveryAlterateWeek,
			// Token: 0x0400017E RID: 382
			PublicFolderMailboxInfoFolderEnumeration,
			// Token: 0x0400017F RID: 383
			ClutterNotificationPeriodicLearnMore,
			// Token: 0x04000180 RID: 384
			MissingItemRumDescription,
			// Token: 0x04000181 RID: 385
			WhenRecurringNoEndDate,
			// Token: 0x04000182 RID: 386
			ClutterNotificationEnableDeepLink,
			// Token: 0x04000183 RID: 387
			AlternateCalendarWhenMonthlyThEveryNMonths,
			// Token: 0x04000184 RID: 388
			TaskWhenMonthlyThEveryMonth,
			// Token: 0x04000185 RID: 389
			WhenRecurringNoEndDateOneDayMinutes,
			// Token: 0x04000186 RID: 390
			WhenRecurringNoEndDateOneDayNoDuration,
			// Token: 0x04000187 RID: 391
			WhenRecurringNoEndDateDaysNoDuration,
			// Token: 0x04000188 RID: 392
			WhenRecurringWithEndDateDaysHours,
			// Token: 0x04000189 RID: 393
			ErrorSharePointSiteHasNoValidUrl,
			// Token: 0x0400018A RID: 394
			TaskWhenNYearsRegeneratingPattern,
			// Token: 0x0400018B RID: 395
			UserPhotoFileTooSmall,
			// Token: 0x0400018C RID: 396
			TaskWhenMonthlyThEveryOtherMonth,
			// Token: 0x0400018D RID: 397
			GroupSubscriptionUnsubscribeInfoText,
			// Token: 0x0400018E RID: 398
			WhenRecurringWithEndDate,
			// Token: 0x0400018F RID: 399
			WhenRecurringNoEndDateOneDayOneHourMinutes,
			// Token: 0x04000190 RID: 400
			GroupMailboxWelcomeEmailSecondaryHeaderAddedBy,
			// Token: 0x04000191 RID: 401
			AlternateCalendarWhenYearlyLeap,
			// Token: 0x04000192 RID: 402
			WhenRecurringWithEndDateOneDayHours,
			// Token: 0x04000193 RID: 403
			WhenStartsEndsDifferentDay,
			// Token: 0x04000194 RID: 404
			AlternateCalendarTaskWhenMonthlyEveryNMonths,
			// Token: 0x04000195 RID: 405
			AlternateCalendarWhenMonthlyThEveryOtherMonth,
			// Token: 0x04000196 RID: 406
			TaskWhenWeeklyEveryNWeeks
		}
	}
}
