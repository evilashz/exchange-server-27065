using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000002 RID: 2
	internal static class SystemMessages
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static SystemMessages()
		{
			SystemMessages.stringIDs.Add(3060389430U, "HumanText5_2_1");
			SystemMessages.stringIDs.Add(224226320U, "ArchiveQuotaWarningSubject");
			SystemMessages.stringIDs.Add(1616176476U, "ShortText5_1_7");
			SystemMessages.stringIDs.Add(955676612U, "DSNEnhanced_5_4_4_SMTPSEND_DNS_NonExistentDomain");
			SystemMessages.stringIDs.Add(4216909332U, "DataCenterHumanText5_4_6");
			SystemMessages.stringIDs.Add(2547234435U, "ExAttachmentRemovedSenderDescription");
			SystemMessages.stringIDs.Add(3483876933U, "HumanText5_3_5");
			SystemMessages.stringIDs.Add(3038358855U, "DSNEnhanced_5_2_3_QUEUE_Priority");
			SystemMessages.stringIDs.Add(171052060U, "ShortText5_3_5");
			SystemMessages.stringIDs.Add(2132860851U, "HumanText5_4_8");
			SystemMessages.stringIDs.Add(264042572U, "QuotaWarningSubjectPF");
			SystemMessages.stringIDs.Add(1070379938U, "E4EViewMessage");
			SystemMessages.stringIDs.Add(1498176974U, "ReadSubject");
			SystemMessages.stringIDs.Add(3726703438U, "HumanReadableFinalText");
			SystemMessages.stringIDs.Add(1459475246U, "DSNEnhanced_5_2_3_RESOLVER_RST_SendSizeLimit_Sender");
			SystemMessages.stringIDs.Add(2954596652U, "ArchiveQuotaWarningTopText");
			SystemMessages.stringIDs.Add(3283016966U, "ShortText5_6_2");
			SystemMessages.stringIDs.Add(1494114401U, "DSNEnhanced_5_7_1_RESOLVER_RST_NotAuthorizedToGroup");
			SystemMessages.stringIDs.Add(286121318U, "QuotaWarningFolderHierarchyDepthSubject");
			SystemMessages.stringIDs.Add(1411446847U, "DecisionProcessedNotificationSubjectPrefix");
			SystemMessages.stringIDs.Add(4198661606U, "QuotaWarningFolderHierarchyDepthNoLimitDetails");
			SystemMessages.stringIDs.Add(3925426878U, "QuotaProhibitReceiveFolderHierarchyDepthSubject");
			SystemMessages.stringIDs.Add(4044098184U, "QuotaWarningFolderHierarchyDepthDetails");
			SystemMessages.stringIDs.Add(80797861U, "DSNEnhanced_5_6_0_RESOLVER_MT_ModerationReencrptionFailed");
			SystemMessages.stringIDs.Add(715499088U, "ShortText5_2_0");
			SystemMessages.stringIDs.Add(1234410400U, "QuotaProhibitReceiveFoldersCountDetails");
			SystemMessages.stringIDs.Add(1595973500U, "ShortText5_4_2");
			SystemMessages.stringIDs.Add(1979055051U, "ShortText5_7_2");
			SystemMessages.stringIDs.Add(3244754815U, "NotReadSubject");
			SystemMessages.stringIDs.Add(3645242410U, "HumanText5_7_0");
			SystemMessages.stringIDs.Add(2798911709U, "DSNEnhanced_5_2_3_RESOLVER_RST_SendSizeLimit_Org");
			SystemMessages.stringIDs.Add(937708661U, "HumanText5_7_10");
			SystemMessages.stringIDs.Add(634034053U, "HumanText5_5_3");
			SystemMessages.stringIDs.Add(816255637U, "ShortText5_7_4");
			SystemMessages.stringIDs.Add(2677307879U, "HumanText5_3_3");
			SystemMessages.stringIDs.Add(2038752517U, "HumanText5_1_7");
			SystemMessages.stringIDs.Add(2058955493U, "HumanText5_6_0");
			SystemMessages.stringIDs.Add(292011585U, "ShortText5_5_2");
			SystemMessages.stringIDs.Add(3633934039U, "QuotaProhibitReceiveFolderHierarchyChildrenCountSubject");
			SystemMessages.stringIDs.Add(4064869466U, "QuotaWarningSubject");
			SystemMessages.stringIDs.Add(1655670966U, "HumanText5_6_5");
			SystemMessages.stringIDs.Add(3182260417U, "ShortText5_1_6");
			SystemMessages.stringIDs.Add(2766572931U, "HumanText5_2_2_StoreNDR");
			SystemMessages.stringIDs.Add(3706504469U, "ShortText5_3_0");
			SystemMessages.stringIDs.Add(1156273137U, "ArchiveQuotaFullTopText");
			SystemMessages.stringIDs.Add(3666692194U, "DoNotForwardName");
			SystemMessages.stringIDs.Add(3686301493U, "ShortText5_6_5");
			SystemMessages.stringIDs.Add(3685029433U, "QuotaWarningMailboxMessagesPerFolderNoLimitSubject");
			SystemMessages.stringIDs.Add(1737136001U, "ShortText5_3_4");
			SystemMessages.stringIDs.Add(3625039434U, "HumanText5_6_1");
			SystemMessages.stringIDs.Add(3888161794U, "QuotaProhibitReceiveMailboxMessagesPerFolderCountDetails");
			SystemMessages.stringIDs.Add(3162057441U, "ShortText5_4_3");
			SystemMessages.stringIDs.Add(2844775350U, "RejectButtonText");
			SystemMessages.stringIDs.Add(1467823001U, "DataCenterHumanText5_1_0");
			SystemMessages.stringIDs.Add(448693229U, "ArchiveQuotaFullDetails");
			SystemMessages.stringIDs.Add(3424179467U, "ShortText5_5_0");
			SystemMessages.stringIDs.Add(513074528U, "HumanText5_7_2");
			SystemMessages.stringIDs.Add(997824090U, "ShortText5_0_0");
			SystemMessages.stringIDs.Add(3457394445U, "DataCenterHumanText5_4_1");
			SystemMessages.stringIDs.Add(2762698027U, "RelayedHumanReadableTopText");
			SystemMessages.stringIDs.Add(2043697631U, "FailedSubject");
			SystemMessages.stringIDs.Add(2357302625U, "DelayedHumanReadableTopText");
			SystemMessages.stringIDs.Add(34865187U, "QuotaMaxSize");
			SystemMessages.stringIDs.Add(2785624105U, "ShortText5_7_8");
			SystemMessages.stringIDs.Add(4194575540U, "QuotaSendTopText");
			SystemMessages.stringIDs.Add(2295532082U, "QuotaSendSubjectPF");
			SystemMessages.stringIDs.Add(2798267404U, "HumanText5_1_0");
			SystemMessages.stringIDs.Add(2435388829U, "HumanText5_7_9");
			SystemMessages.stringIDs.Add(1858095526U, "ShortText5_5_1");
			SystemMessages.stringIDs.Add(2240869372U, "QuotaProhibitReceiveMailboxMessagesPerFolderCountSubject");
			SystemMessages.stringIDs.Add(913497348U, "ExpandedHumanReadableTopText");
			SystemMessages.stringIDs.Add(2179915018U, "HumanText5_4_3");
			SystemMessages.stringIDs.Add(894078784U, "FailedHumanReadableTopText");
			SystemMessages.stringIDs.Add(3183792054U, "BodyHeaderFontTag");
			SystemMessages.stringIDs.Add(4029344418U, "BodyBlockFontTag");
			SystemMessages.stringIDs.Add(1086182386U, "DSNEnhanced_5_4_4_ROUTING_NoNextHop");
			SystemMessages.stringIDs.Add(1775266991U, "E4EDisclaimer");
			SystemMessages.stringIDs.Add(4171950547U, "ModerationNdrNotificationForSenderTopText");
			SystemMessages.stringIDs.Add(4135226003U, "ExOrarMailSenderDescription");
			SystemMessages.stringIDs.Add(777497770U, "QuotaWarningFolderHierarchyChildrenNoLimitDetails");
			SystemMessages.stringIDs.Add(1075924926U, "DSNEnhanced_5_7_1_RESOLVER_RST_AuthRequired");
			SystemMessages.stringIDs.Add(3187564900U, "E4EHeaderCustom");
			SystemMessages.stringIDs.Add(3017333248U, "DelayedSubject");
			SystemMessages.stringIDs.Add(3276713205U, "ModerationExpiryNoticationForModeratorTopText");
			SystemMessages.stringIDs.Add(3020894940U, "ShortText5_5_3");
			SystemMessages.stringIDs.Add(89587025U, "HumanText5_6_4");
			SystemMessages.stringIDs.Add(2397807267U, "E4EViewMessageOTPButton");
			SystemMessages.stringIDs.Add(3154497764U, "HumanText5_1_8");
			SystemMessages.stringIDs.Add(1062071583U, "RejectedNotificationSubjectPrefix");
			SystemMessages.stringIDs.Add(3905817172U, "QuotaWarningMailboxMessagesPerFolderCountSubject");
			SystemMessages.stringIDs.Add(1575770524U, "ShortText5_7_1");
			SystemMessages.stringIDs.Add(1494305489U, "HumanText5_2_0");
			SystemMessages.stringIDs.Add(1211980487U, "HumanText5_0_0");
			SystemMessages.stringIDs.Add(3597730735U, "EnhancedDsnTextFontTag");
			SystemMessages.stringIDs.Add(3847666970U, "ShortText5_2_2");
			SystemMessages.stringIDs.Add(273945714U, "ApprovalRequestPreview");
			SystemMessages.stringIDs.Add(613831077U, "HumanText5_4_2");
			SystemMessages.stringIDs.Add(1416892261U, "OriginalHeadersTitle");
			SystemMessages.stringIDs.Add(3886882982U, "ModeratorsOofSubjectPrefix");
			SystemMessages.stringIDs.Add(3328018042U, "ModeratorRejectTopText");
			SystemMessages.stringIDs.Add(3837274988U, "FinalTextFontTag");
			SystemMessages.stringIDs.Add(4028322787U, "ExOrarMailDisplayName");
			SystemMessages.stringIDs.Add(228586384U, "DSNEnhanced_5_7_1_RESOLVER_RST_DLNeedsSenderRestrictions");
			SystemMessages.stringIDs.Add(3547364398U, "ModeratorCommentsHeader");
			SystemMessages.stringIDs.Add(2894800102U, "ModeratorsNdrSubjectPrefix");
			SystemMessages.stringIDs.Add(2504161731U, "ReceivingServerTitle");
			SystemMessages.stringIDs.Add(3335723890U, "QuotaSendDetails");
			SystemMessages.stringIDs.Add(69384049U, "HumanText5_1_3");
			SystemMessages.stringIDs.Add(1797209243U, "QuarantinedHumanReadableTopText");
			SystemMessages.stringIDs.Add(2296356702U, "DeliveredSubject");
			SystemMessages.stringIDs.Add(4273299450U, "DSNEnhanced_5_2_3_RESOLVER_RST_RecipSizeLimit_DL");
			SystemMessages.stringIDs.Add(1203275484U, "DSNEnhanced_5_7_1_APPROVAL_NotAuthorized");
			SystemMessages.stringIDs.Add(109790001U, "HumanText5_7_5");
			SystemMessages.stringIDs.Add(4001472770U, "HumanText5_7_8");
			SystemMessages.stringIDs.Add(238766803U, "DecisionConflictNotificationSubjectPrefix");
			SystemMessages.stringIDs.Add(1830882122U, "QuotaWarningNoLimitSubjectPF");
			SystemMessages.stringIDs.Add(439284250U, "BodyDownload");
			SystemMessages.stringIDs.Add(1979593922U, "QuotaProhibitReceiveFoldersCountSubject");
			SystemMessages.stringIDs.Add(2618404892U, "ShortText5_7_100");
			SystemMessages.stringIDs.Add(2028676810U, "QuotaWarningTopText");
			SystemMessages.stringIDs.Add(2900729894U, "ShortText5_7_300");
			SystemMessages.stringIDs.Add(3949783936U, "FailedHumanReadableTopTextForTextMessageNotification");
			SystemMessages.stringIDs.Add(2592547661U, "DelayHumanReadableExplanation");
			SystemMessages.stringIDs.Add(1118783615U, "ShortText5_2_3");
			SystemMessages.stringIDs.Add(2820071495U, "InternetConfidentialDescription");
			SystemMessages.stringIDs.Add(2657104903U, "HumanText5_2_2");
			SystemMessages.stringIDs.Add(836458613U, "ShortText5_4_7");
			SystemMessages.stringIDs.Add(742696162U, "ApprovalRequestTopText");
			SystemMessages.stringIDs.Add(4102229319U, "HumanText5_4_4");
			SystemMessages.stringIDs.Add(1091303688U, "QuotaWarningFolderHierarchyDepthNoLimitSubject");
			SystemMessages.stringIDs.Add(325931173U, "ModerationOofNotificationForSenderTopText");
			SystemMessages.stringIDs.Add(76943726U, "ShortText5_4_8");
			SystemMessages.stringIDs.Add(3301930267U, "ExpandedSubject");
			SystemMessages.stringIDs.Add(4122432295U, "HumanText5_5_5");
			SystemMessages.stringIDs.Add(3033906942U, "DataCenterHumanText5_1_1");
			SystemMessages.stringIDs.Add(3806593754U, "ArchiveQuotaWarningDetails");
			SystemMessages.stringIDs.Add(1586140930U, "E4EViewMessageInfo");
			SystemMessages.stringIDs.Add(3351672821U, "DeliveredHumanReadableTopText");
			SystemMessages.stringIDs.Add(304371086U, "RelayedSubject");
			SystemMessages.stringIDs.Add(754993578U, "HumanText5_3_4");
			SystemMessages.stringIDs.Add(2281583029U, "ShortText5_2_1");
			SystemMessages.stringIDs.Add(4035994692U, "QuotaSendSubject");
			SystemMessages.stringIDs.Add(2422745530U, "ShortText5_1_1");
			SystemMessages.stringIDs.Add(3158471333U, "DiagnosticsFontTag");
			SystemMessages.stringIDs.Add(833904234U, "QuotaWarningFoldersCountNoLimitDetails");
			SystemMessages.stringIDs.Add(1675873942U, "HumanText5_7_4");
			SystemMessages.stringIDs.Add(375773780U, "QuotaProhibitReceiveFolderHierarchyDepthDetails");
			SystemMessages.stringIDs.Add(2271073400U, "QuotaWarningFoldersCountDetails");
			SystemMessages.stringIDs.Add(799906996U, "E4EReceivedMessage");
			SystemMessages.stringIDs.Add(3093270387U, "ExPartnerMailDisplayName");
			SystemMessages.stringIDs.Add(4198466683U, "ExAttachmentRemovedDisplayName");
			SystemMessages.stringIDs.Add(977621114U, "ShortText5_3_3");
			SystemMessages.stringIDs.Add(2308350314U, "QuotaWarningDetailsPF");
			SystemMessages.stringIDs.Add(2844551000U, "ArchiveQuotaWarningNoLimitDetails");
			SystemMessages.stringIDs.Add(4076586603U, "GeneratingServerTitle");
			SystemMessages.stringIDs.Add(1431455589U, "QuotaSendReceiveSubject");
			SystemMessages.stringIDs.Add(3632599111U, "ShortText5_1_8");
			SystemMessages.stringIDs.Add(3585544944U, "ShortText5_1_3");
			SystemMessages.stringIDs.Add(159863259U, "ExOrarMailRecipientDescription");
			SystemMessages.stringIDs.Add(1343379807U, "ArchiveQuotaFullSubject");
			SystemMessages.stringIDs.Add(2333692916U, "QuotaWarningNoLimitDetailsPF");
			SystemMessages.stringIDs.Add(2402542554U, "ShortText5_4_4");
			SystemMessages.stringIDs.Add(2088818508U, "HumanText_InitMsg");
			SystemMessages.stringIDs.Add(322888411U, "HumanText5_7_300");
			SystemMessages.stringIDs.Add(4249345458U, "BodyReceiveRMEmail");
			SystemMessages.stringIDs.Add(1733655566U, "QuotaWarningFoldersCountSubject");
			SystemMessages.stringIDs.Add(3171360660U, "QuotaWarningDetails");
			SystemMessages.stringIDs.Add(3080592406U, "HumanText5_3_0");
			SystemMessages.stringIDs.Add(605213413U, "HumanText5_7_100");
			SystemMessages.stringIDs.Add(3604836458U, "HumanText5_1_6");
			SystemMessages.stringIDs.Add(2382339578U, "ShortText5_7_7");
			SystemMessages.stringIDs.Add(347238620U, "QuotaWarningFolderHierarchyChildrenNoLimitSubject");
			SystemMessages.stringIDs.Add(3141854465U, "ShortText5_7_0");
			SystemMessages.stringIDs.Add(3362917408U, "HumanText5_5_0");
			SystemMessages.stringIDs.Add(1321639685U, "QuotaProhibitReceiveFolderHierarchyChildrenCountDetails");
			SystemMessages.stringIDs.Add(3988829471U, "ShortText5_1_0");
			SystemMessages.stringIDs.Add(2455731400U, "QuotaWarningFoldersCountNoLimitSubject");
			SystemMessages.stringIDs.Add(1776630491U, "HumanText5_4_0");
			SystemMessages.stringIDs.Add(750807939U, "ExAttachmentRemovedRecipientDescription");
			SystemMessages.stringIDs.Add(3452592194U, "QuotaCurrentSize");
			SystemMessages.stringIDs.Add(2915608171U, "DecisionUpdateTopText");
			SystemMessages.stringIDs.Add(3342714432U, "HumanText5_4_1");
			SystemMessages.stringIDs.Add(3565341968U, "ShortText5_4_6");
			SystemMessages.stringIDs.Add(3948423519U, "ShortText5_7_6");
			SystemMessages.stringIDs.Add(1242161506U, "ApprovalRequestSubjectPrefix");
			SystemMessages.stringIDs.Add(4133660744U, "E4ESignIn");
			SystemMessages.stringIDs.Add(831975206U, "InternetConfidentialName");
			SystemMessages.stringIDs.Add(1272589415U, "HumanText5_7_7");
			SystemMessages.stringIDs.Add(412971110U, "ShortText5_7_3");
			SystemMessages.stringIDs.Add(452614234U, "QuotaWarningNoLimitDetails");
			SystemMessages.stringIDs.Add(1230109185U, "E4EEncryptedMessage");
			SystemMessages.stringIDs.Add(1796833467U, "HumanText5_5_1");
			SystemMessages.stringIDs.Add(3316231944U, "DataCenterHumanText5_7_1");
			SystemMessages.stringIDs.Add(2261380053U, "ShortText5_5_6");
			SystemMessages.stringIDs.Add(1525463967U, "QuotaWarningFolderHierarchyChildrenCountDetails");
			SystemMessages.stringIDs.Add(1219540164U, "ShortText5_7_9");
			SystemMessages.stringIDs.Add(856661589U, "ShortText5_1_2");
			SystemMessages.stringIDs.Add(859070570U, "QuotaWarningMailboxMessagesPerFolderCountDetails");
			SystemMessages.stringIDs.Add(3241957883U, "HumanText5_7_3");
			SystemMessages.stringIDs.Add(1793216387U, "DoNotForwardDescription");
			SystemMessages.stringIDs.Add(4019621683U, "ModerationExpiryNoticationForSenderTopText");
			SystemMessages.stringIDs.Add(210546550U, "HumanText5_4_7");
			SystemMessages.stringIDs.Add(1569096212U, "DataCenterHumanText4_4_7");
			SystemMessages.stringIDs.Add(492871552U, "HumanText5_6_3");
			SystemMessages.stringIDs.Add(4243391820U, "HumanText5_3_2");
			SystemMessages.stringIDs.Add(3124761826U, "ApprovalCommentAttachmentFilename");
			SystemMessages.stringIDs.Add(2375073438U, "ArchiveQuotaWarningNoLimitSubject");
			SystemMessages.stringIDs.Add(472668576U, "HumanText5_1_4");
			SystemMessages.stringIDs.Add(2988353893U, "E4EHosted");
			SystemMessages.stringIDs.Add(3224920035U, "QuotaSendReceiveDetails");
			SystemMessages.stringIDs.Add(3041097916U, "ShortText5_2_4");
			SystemMessages.stringIDs.Add(843859989U, "DSNEnhanced_5_7_1_RESOLVER_RST_NotAuthorized");
			SystemMessages.stringIDs.Add(1098580639U, "ShortText5_5_4");
			SystemMessages.stringIDs.Add(86736107U, "QuotaWarningMailboxMessagesPerFolderNoLimitDetails");
			SystemMessages.stringIDs.Add(2120217552U, "ShortText5_6_4");
			SystemMessages.stringIDs.Add(2019461003U, "ShortText5_1_4");
			SystemMessages.stringIDs.Add(1635467990U, "HumanText5_1_2");
			SystemMessages.stringIDs.Add(3715345541U, "QuotaWarningFolderHierarchyChildrenCountSubject");
			SystemMessages.stringIDs.Add(2200117994U, "HumanText5_5_2");
			SystemMessages.stringIDs.Add(433174086U, "ShortText5_4_0");
			SystemMessages.stringIDs.Add(2079158469U, "HumanText5_7_1");
			SystemMessages.stringIDs.Add(3463673957U, "HumanText5_2_4");
			SystemMessages.stringIDs.Add(357259713U, "DSNEnhanced_5_2_3_RESOLVER_RST_RecipSizeLimit");
			SystemMessages.stringIDs.Add(20682666U, "E4EWaitMessage");
			SystemMessages.stringIDs.Add(1418666303U, "ApprovalRequestExpiryTopText");
			SystemMessages.stringIDs.Add(1835138854U, "E4EViewMessageButton");
			SystemMessages.stringIDs.Add(554133611U, "ShortText5_6_3");
			SystemMessages.stringIDs.Add(1232183463U, "HumanText5_1_1");
			SystemMessages.stringIDs.Add(2489798467U, "Moderation_Reencryption_Exception");
			SystemMessages.stringIDs.Add(150849084U, "ShortText5_6_0");
			SystemMessages.stringIDs.Add(1716933025U, "ShortText5_6_1");
			SystemMessages.stringIDs.Add(2028225464U, "ApprovalRequestExpiredNotificationSubjectPrefix");
			SystemMessages.stringIDs.Add(1393548940U, "HumanText5_5_4");
			SystemMessages.stringIDs.Add(1999258027U, "ShortText5_4_1");
			SystemMessages.stringIDs.Add(2807992444U, "QuotaWarningNoLimitSubject");
			SystemMessages.stringIDs.Add(724420710U, "DecisionConflictTopText");
			SystemMessages.stringIDs.Add(2707856071U, "FailedHumanReadableTopTextForTextMessage");
			SystemMessages.stringIDs.Add(3221754907U, "HumanText5_6_2");
			SystemMessages.stringIDs.Add(96782060U, "QuotaSendDetailsPF");
			SystemMessages.stringIDs.Add(2838673356U, "HumanText5_7_6");
			SystemMessages.stringIDs.Add(2912990139U, "QuotaSendReceiveTopText");
			SystemMessages.stringIDs.Add(4223188844U, "HumanText5_2_3");
			SystemMessages.stringIDs.Add(3163998196U, "ApproveButtonText");
			SystemMessages.stringIDs.Add(3827208783U, "DiagnosticInformationTitle");
			SystemMessages.stringIDs.Add(2939429905U, "HumanText5_4_6");
			SystemMessages.stringIDs.Add(230749526U, "HumanText5_5_6");
			SystemMessages.stringIDs.Add(3827463994U, "ShortText5_5_5");
			SystemMessages.stringIDs.Add(3545138992U, "ShortText5_7_5");
			SystemMessages.stringIDs.Add(2279047500U, "DSNEnhanced_5_7_1_TRANSPORT_RULES_RejectMessage");
			SystemMessages.stringIDs.Add(1514508465U, "HumanText5_3_1");
			SystemMessages.stringIDs.Add(2140420528U, "ShortText5_3_1");
			SystemMessages.stringIDs.Add(2543705055U, "ShortText5_3_2");
			SystemMessages.stringIDs.Add(2107776300U, "ShortText5_7_10");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000034D3 File Offset: 0x000016D3
		public static LocalizedString HumanText5_2_1
		{
			get
			{
				return new LocalizedString("HumanText5_2_1", "ExB408C8", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000034F1 File Offset: 0x000016F1
		public static LocalizedString ArchiveQuotaWarningSubject
		{
			get
			{
				return new LocalizedString("ArchiveQuotaWarningSubject", "Ex9E567B", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000350F File Offset: 0x0000170F
		public static LocalizedString ShortText5_1_7
		{
			get
			{
				return new LocalizedString("ShortText5_1_7", "ExFA341E", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000352D File Offset: 0x0000172D
		public static LocalizedString DSNEnhanced_5_4_4_SMTPSEND_DNS_NonExistentDomain
		{
			get
			{
				return new LocalizedString("DSNEnhanced_5_4_4_SMTPSEND_DNS_NonExistentDomain", "ExFCCCD7", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000354C File Offset: 0x0000174C
		public static LocalizedString HumanReadableBoldedSubjectLine(string subject)
		{
			return new LocalizedString("HumanReadableBoldedSubjectLine", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				subject
			});
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000357B File Offset: 0x0000177B
		public static LocalizedString DataCenterHumanText5_4_6
		{
			get
			{
				return new LocalizedString("DataCenterHumanText5_4_6", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00003599 File Offset: 0x00001799
		public static LocalizedString ExAttachmentRemovedSenderDescription
		{
			get
			{
				return new LocalizedString("ExAttachmentRemovedSenderDescription", "ExEE203E", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000035B7 File Offset: 0x000017B7
		public static LocalizedString HumanText5_3_5
		{
			get
			{
				return new LocalizedString("HumanText5_3_5", "Ex93851F", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000035D8 File Offset: 0x000017D8
		public static LocalizedString DsnParamTextMessageSizePerRecipientInMB(string currentSize, string maxSize)
		{
			return new LocalizedString("DsnParamTextMessageSizePerRecipientInMB", "Ex869418", false, true, SystemMessages.ResourceManager, new object[]
			{
				currentSize,
				maxSize
			});
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000360B File Offset: 0x0000180B
		public static LocalizedString DSNEnhanced_5_2_3_QUEUE_Priority
		{
			get
			{
				return new LocalizedString("DSNEnhanced_5_2_3_QUEUE_Priority", "Ex91FDA3", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00003629 File Offset: 0x00001829
		public static LocalizedString ShortText5_3_5
		{
			get
			{
				return new LocalizedString("ShortText5_3_5", "Ex87FD47", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00003647 File Offset: 0x00001847
		public static LocalizedString HumanText5_4_8
		{
			get
			{
				return new LocalizedString("HumanText5_4_8", "Ex5183B0", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00003665 File Offset: 0x00001865
		public static LocalizedString QuotaWarningSubjectPF
		{
			get
			{
				return new LocalizedString("QuotaWarningSubjectPF", "Ex935943", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00003683 File Offset: 0x00001883
		public static LocalizedString E4EViewMessage
		{
			get
			{
				return new LocalizedString("E4EViewMessage", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000036A1 File Offset: 0x000018A1
		public static LocalizedString ReadSubject
		{
			get
			{
				return new LocalizedString("ReadSubject", "Ex60F14F", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000036BF File Offset: 0x000018BF
		public static LocalizedString HumanReadableFinalText
		{
			get
			{
				return new LocalizedString("HumanReadableFinalText", "Ex409525", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000036DD File Offset: 0x000018DD
		public static LocalizedString DSNEnhanced_5_2_3_RESOLVER_RST_SendSizeLimit_Sender
		{
			get
			{
				return new LocalizedString("DSNEnhanced_5_2_3_RESOLVER_RST_SendSizeLimit_Sender", "ExB3F3FB", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000036FB File Offset: 0x000018FB
		public static LocalizedString ArchiveQuotaWarningTopText
		{
			get
			{
				return new LocalizedString("ArchiveQuotaWarningTopText", "Ex9117F0", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00003719 File Offset: 0x00001919
		public static LocalizedString ShortText5_6_2
		{
			get
			{
				return new LocalizedString("ShortText5_6_2", "Ex19F714", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00003737 File Offset: 0x00001937
		public static LocalizedString DSNEnhanced_5_7_1_RESOLVER_RST_NotAuthorizedToGroup
		{
			get
			{
				return new LocalizedString("DSNEnhanced_5_7_1_RESOLVER_RST_NotAuthorizedToGroup", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00003755 File Offset: 0x00001955
		public static LocalizedString QuotaWarningFolderHierarchyDepthSubject
		{
			get
			{
				return new LocalizedString("QuotaWarningFolderHierarchyDepthSubject", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00003773 File Offset: 0x00001973
		public static LocalizedString DecisionProcessedNotificationSubjectPrefix
		{
			get
			{
				return new LocalizedString("DecisionProcessedNotificationSubjectPrefix", "Ex2B2687", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00003791 File Offset: 0x00001991
		public static LocalizedString QuotaWarningFolderHierarchyDepthNoLimitDetails
		{
			get
			{
				return new LocalizedString("QuotaWarningFolderHierarchyDepthNoLimitDetails", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000037AF File Offset: 0x000019AF
		public static LocalizedString QuotaProhibitReceiveFolderHierarchyDepthSubject
		{
			get
			{
				return new LocalizedString("QuotaProhibitReceiveFolderHierarchyDepthSubject", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000037CD File Offset: 0x000019CD
		public static LocalizedString QuotaWarningFolderHierarchyDepthDetails
		{
			get
			{
				return new LocalizedString("QuotaWarningFolderHierarchyDepthDetails", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000037EB File Offset: 0x000019EB
		public static LocalizedString DSNEnhanced_5_6_0_RESOLVER_MT_ModerationReencrptionFailed
		{
			get
			{
				return new LocalizedString("DSNEnhanced_5_6_0_RESOLVER_MT_ModerationReencrptionFailed", "Ex83139A", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00003809 File Offset: 0x00001A09
		public static LocalizedString ShortText5_2_0
		{
			get
			{
				return new LocalizedString("ShortText5_2_0", "Ex73A191", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00003827 File Offset: 0x00001A27
		public static LocalizedString QuotaProhibitReceiveFoldersCountDetails
		{
			get
			{
				return new LocalizedString("QuotaProhibitReceiveFoldersCountDetails", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00003845 File Offset: 0x00001A45
		public static LocalizedString ShortText5_4_2
		{
			get
			{
				return new LocalizedString("ShortText5_4_2", "Ex397784", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00003863 File Offset: 0x00001A63
		public static LocalizedString ShortText5_7_2
		{
			get
			{
				return new LocalizedString("ShortText5_7_2", "Ex505A9B", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00003881 File Offset: 0x00001A81
		public static LocalizedString NotReadSubject
		{
			get
			{
				return new LocalizedString("NotReadSubject", "ExE3B31A", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000389F File Offset: 0x00001A9F
		public static LocalizedString HumanText5_7_0
		{
			get
			{
				return new LocalizedString("HumanText5_7_0", "Ex35FDD9", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000038BD File Offset: 0x00001ABD
		public static LocalizedString DSNEnhanced_5_2_3_RESOLVER_RST_SendSizeLimit_Org
		{
			get
			{
				return new LocalizedString("DSNEnhanced_5_2_3_RESOLVER_RST_SendSizeLimit_Org", "Ex274547", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000038DB File Offset: 0x00001ADB
		public static LocalizedString HumanText5_7_10
		{
			get
			{
				return new LocalizedString("HumanText5_7_10", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000038F9 File Offset: 0x00001AF9
		public static LocalizedString HumanText5_5_3
		{
			get
			{
				return new LocalizedString("HumanText5_5_3", "ExDB4E85", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00003917 File Offset: 0x00001B17
		public static LocalizedString ShortText5_7_4
		{
			get
			{
				return new LocalizedString("ShortText5_7_4", "Ex0D8773", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00003935 File Offset: 0x00001B35
		public static LocalizedString HumanText5_3_3
		{
			get
			{
				return new LocalizedString("HumanText5_3_3", "ExC668E0", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00003953 File Offset: 0x00001B53
		public static LocalizedString HumanText5_1_7
		{
			get
			{
				return new LocalizedString("HumanText5_1_7", "ExCF82B7", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00003971 File Offset: 0x00001B71
		public static LocalizedString HumanText5_6_0
		{
			get
			{
				return new LocalizedString("HumanText5_6_0", "ExEFB6F0", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003990 File Offset: 0x00001B90
		public static LocalizedString QuotaProhibitReceiveFoldersCountTopText(string count)
		{
			return new LocalizedString("QuotaProhibitReceiveFoldersCountTopText", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000039BF File Offset: 0x00001BBF
		public static LocalizedString ShortText5_5_2
		{
			get
			{
				return new LocalizedString("ShortText5_5_2", "Ex786EEE", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000039DD File Offset: 0x00001BDD
		public static LocalizedString QuotaProhibitReceiveFolderHierarchyChildrenCountSubject
		{
			get
			{
				return new LocalizedString("QuotaProhibitReceiveFolderHierarchyChildrenCountSubject", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000039FC File Offset: 0x00001BFC
		public static LocalizedString QuotaWarningFolderHierarchyDepthTopText(string folder, string count)
		{
			return new LocalizedString("QuotaWarningFolderHierarchyDepthTopText", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				folder,
				count
			});
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00003A2F File Offset: 0x00001C2F
		public static LocalizedString QuotaWarningSubject
		{
			get
			{
				return new LocalizedString("QuotaWarningSubject", "Ex81CBC1", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003A4D File Offset: 0x00001C4D
		public static LocalizedString HumanText5_6_5
		{
			get
			{
				return new LocalizedString("HumanText5_6_5", "ExC4BEFD", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00003A6B File Offset: 0x00001C6B
		public static LocalizedString ShortText5_1_6
		{
			get
			{
				return new LocalizedString("ShortText5_1_6", "ExD6357D", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003A89 File Offset: 0x00001C89
		public static LocalizedString HumanText5_2_2_StoreNDR
		{
			get
			{
				return new LocalizedString("HumanText5_2_2_StoreNDR", "Ex94675B", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003AA7 File Offset: 0x00001CA7
		public static LocalizedString ShortText5_3_0
		{
			get
			{
				return new LocalizedString("ShortText5_3_0", "Ex0455D6", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00003AC5 File Offset: 0x00001CC5
		public static LocalizedString ArchiveQuotaFullTopText
		{
			get
			{
				return new LocalizedString("ArchiveQuotaFullTopText", "ExB1BA4D", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00003AE3 File Offset: 0x00001CE3
		public static LocalizedString DoNotForwardName
		{
			get
			{
				return new LocalizedString("DoNotForwardName", "Ex58E3CC", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003B04 File Offset: 0x00001D04
		public static LocalizedString QuotaProhibitReceiveFolderHierarchyDepthTopText(string folder, string count)
		{
			return new LocalizedString("QuotaProhibitReceiveFolderHierarchyDepthTopText", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				folder,
				count
			});
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003B38 File Offset: 0x00001D38
		public static LocalizedString ModeratedDLApprovalRequestRecipientList(int count)
		{
			return new LocalizedString("ModeratedDLApprovalRequestRecipientList", "Ex917C07", false, true, SystemMessages.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003B6C File Offset: 0x00001D6C
		public static LocalizedString ShortText5_6_5
		{
			get
			{
				return new LocalizedString("ShortText5_6_5", "Ex153777", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003B8A File Offset: 0x00001D8A
		public static LocalizedString QuotaWarningMailboxMessagesPerFolderNoLimitSubject
		{
			get
			{
				return new LocalizedString("QuotaWarningMailboxMessagesPerFolderNoLimitSubject", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public static LocalizedString ShortText5_3_4
		{
			get
			{
				return new LocalizedString("ShortText5_3_4", "Ex71E89C", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00003BC6 File Offset: 0x00001DC6
		public static LocalizedString HumanText5_6_1
		{
			get
			{
				return new LocalizedString("HumanText5_6_1", "Ex655E57", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public static LocalizedString QuotaProhibitReceiveMailboxMessagesPerFolderCountDetails
		{
			get
			{
				return new LocalizedString("QuotaProhibitReceiveMailboxMessagesPerFolderCountDetails", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003C02 File Offset: 0x00001E02
		public static LocalizedString ShortText5_4_3
		{
			get
			{
				return new LocalizedString("ShortText5_4_3", "Ex62D181", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003C20 File Offset: 0x00001E20
		public static LocalizedString RejectButtonText
		{
			get
			{
				return new LocalizedString("RejectButtonText", "Ex4E7583", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003C3E File Offset: 0x00001E3E
		public static LocalizedString DataCenterHumanText5_1_0
		{
			get
			{
				return new LocalizedString("DataCenterHumanText5_1_0", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003C5C File Offset: 0x00001E5C
		public static LocalizedString QuotaWarningFoldersCountNoLimitTopText(string count)
		{
			return new LocalizedString("QuotaWarningFoldersCountNoLimitTopText", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003C8B File Offset: 0x00001E8B
		public static LocalizedString ArchiveQuotaFullDetails
		{
			get
			{
				return new LocalizedString("ArchiveQuotaFullDetails", "Ex0DD730", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003CA9 File Offset: 0x00001EA9
		public static LocalizedString ShortText5_5_0
		{
			get
			{
				return new LocalizedString("ShortText5_5_0", "Ex97BC73", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003CC8 File Offset: 0x00001EC8
		public static LocalizedString QuotaProhibitReceiveMailboxMessagesPerFolderCountTopText(string folder, string count)
		{
			return new LocalizedString("QuotaProhibitReceiveMailboxMessagesPerFolderCountTopText", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				folder,
				count
			});
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003CFB File Offset: 0x00001EFB
		public static LocalizedString HumanText5_7_2
		{
			get
			{
				return new LocalizedString("HumanText5_7_2", "ExC68461", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003D1C File Offset: 0x00001F1C
		public static LocalizedString DsnParamTextRecipientCount(string currentRecipientCount, string maxRecipientCount)
		{
			return new LocalizedString("DsnParamTextRecipientCount", "Ex3F3468", false, true, SystemMessages.ResourceManager, new object[]
			{
				currentRecipientCount,
				maxRecipientCount
			});
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003D50 File Offset: 0x00001F50
		public static LocalizedString HumanTextFailedSmtpToSmsGatewayNotification(string number, string carrier, string bodyText)
		{
			return new LocalizedString("HumanTextFailedSmtpToSmsGatewayNotification", "Ex068E4A", false, true, SystemMessages.ResourceManager, new object[]
			{
				number,
				carrier,
				bodyText
			});
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003D87 File Offset: 0x00001F87
		public static LocalizedString ShortText5_0_0
		{
			get
			{
				return new LocalizedString("ShortText5_0_0", "Ex9431D4", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003DA5 File Offset: 0x00001FA5
		public static LocalizedString DataCenterHumanText5_4_1
		{
			get
			{
				return new LocalizedString("DataCenterHumanText5_4_1", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003DC3 File Offset: 0x00001FC3
		public static LocalizedString RelayedHumanReadableTopText
		{
			get
			{
				return new LocalizedString("RelayedHumanReadableTopText", "Ex0935BC", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003DE1 File Offset: 0x00001FE1
		public static LocalizedString FailedSubject
		{
			get
			{
				return new LocalizedString("FailedSubject", "Ex1B3E1B", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003DFF File Offset: 0x00001FFF
		public static LocalizedString DelayedHumanReadableTopText
		{
			get
			{
				return new LocalizedString("DelayedHumanReadableTopText", "Ex7B6028", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003E1D File Offset: 0x0000201D
		public static LocalizedString QuotaMaxSize
		{
			get
			{
				return new LocalizedString("QuotaMaxSize", "Ex9915CA", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003E3B File Offset: 0x0000203B
		public static LocalizedString ShortText5_7_8
		{
			get
			{
				return new LocalizedString("ShortText5_7_8", "Ex517439", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003E59 File Offset: 0x00002059
		public static LocalizedString QuotaSendTopText
		{
			get
			{
				return new LocalizedString("QuotaSendTopText", "Ex7E7395", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003E77 File Offset: 0x00002077
		public static LocalizedString QuotaSendSubjectPF
		{
			get
			{
				return new LocalizedString("QuotaSendSubjectPF", "Ex0B015A", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003E95 File Offset: 0x00002095
		public static LocalizedString HumanText5_1_0
		{
			get
			{
				return new LocalizedString("HumanText5_1_0", "ExCA6A4C", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003EB3 File Offset: 0x000020B3
		public static LocalizedString HumanText5_7_9
		{
			get
			{
				return new LocalizedString("HumanText5_7_9", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003ED1 File Offset: 0x000020D1
		public static LocalizedString ShortText5_5_1
		{
			get
			{
				return new LocalizedString("ShortText5_5_1", "ExBE46D2", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003EEF File Offset: 0x000020EF
		public static LocalizedString QuotaProhibitReceiveMailboxMessagesPerFolderCountSubject
		{
			get
			{
				return new LocalizedString("QuotaProhibitReceiveMailboxMessagesPerFolderCountSubject", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003F10 File Offset: 0x00002110
		public static LocalizedString QuotaWarningFolderHierarchyDepthNoLimitTopText(string folder, string count)
		{
			return new LocalizedString("QuotaWarningFolderHierarchyDepthNoLimitTopText", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				folder,
				count
			});
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003F43 File Offset: 0x00002143
		public static LocalizedString ExpandedHumanReadableTopText
		{
			get
			{
				return new LocalizedString("ExpandedHumanReadableTopText", "Ex8A1995", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003F61 File Offset: 0x00002161
		public static LocalizedString HumanText5_4_3
		{
			get
			{
				return new LocalizedString("HumanText5_4_3", "Ex0FF9D9", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003F7F File Offset: 0x0000217F
		public static LocalizedString FailedHumanReadableTopText
		{
			get
			{
				return new LocalizedString("FailedHumanReadableTopText", "ExD02008", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003F9D File Offset: 0x0000219D
		public static LocalizedString BodyHeaderFontTag
		{
			get
			{
				return new LocalizedString("BodyHeaderFontTag", "ExB30ACE", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003FBB File Offset: 0x000021BB
		public static LocalizedString BodyBlockFontTag
		{
			get
			{
				return new LocalizedString("BodyBlockFontTag", "ExD050DF", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003FD9 File Offset: 0x000021D9
		public static LocalizedString DSNEnhanced_5_4_4_ROUTING_NoNextHop
		{
			get
			{
				return new LocalizedString("DSNEnhanced_5_4_4_ROUTING_NoNextHop", "ExADF848", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003FF7 File Offset: 0x000021F7
		public static LocalizedString E4EDisclaimer
		{
			get
			{
				return new LocalizedString("E4EDisclaimer", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004015 File Offset: 0x00002215
		public static LocalizedString ModerationNdrNotificationForSenderTopText
		{
			get
			{
				return new LocalizedString("ModerationNdrNotificationForSenderTopText", "Ex170399", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00004033 File Offset: 0x00002233
		public static LocalizedString ExOrarMailSenderDescription
		{
			get
			{
				return new LocalizedString("ExOrarMailSenderDescription", "Ex6CA2C7", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00004051 File Offset: 0x00002251
		public static LocalizedString QuotaWarningFolderHierarchyChildrenNoLimitDetails
		{
			get
			{
				return new LocalizedString("QuotaWarningFolderHierarchyChildrenNoLimitDetails", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000406F File Offset: 0x0000226F
		public static LocalizedString DSNEnhanced_5_7_1_RESOLVER_RST_AuthRequired
		{
			get
			{
				return new LocalizedString("DSNEnhanced_5_7_1_RESOLVER_RST_AuthRequired", "ExB24F25", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000408D File Offset: 0x0000228D
		public static LocalizedString E4EHeaderCustom
		{
			get
			{
				return new LocalizedString("E4EHeaderCustom", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000040AB File Offset: 0x000022AB
		public static LocalizedString DelayedSubject
		{
			get
			{
				return new LocalizedString("DelayedSubject", "Ex1B9E9C", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000040CC File Offset: 0x000022CC
		public static LocalizedString ArchiveQuotaWarningNoLimitTopText(string size)
		{
			return new LocalizedString("ArchiveQuotaWarningNoLimitTopText", "Ex367C56", false, true, SystemMessages.ResourceManager, new object[]
			{
				size
			});
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000040FB File Offset: 0x000022FB
		public static LocalizedString ModerationExpiryNoticationForModeratorTopText
		{
			get
			{
				return new LocalizedString("ModerationExpiryNoticationForModeratorTopText", "Ex28DF95", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00004119 File Offset: 0x00002319
		public static LocalizedString ShortText5_5_3
		{
			get
			{
				return new LocalizedString("ShortText5_5_3", "Ex4F4960", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00004137 File Offset: 0x00002337
		public static LocalizedString HumanText5_6_4
		{
			get
			{
				return new LocalizedString("HumanText5_6_4", "ExBE73FF", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00004155 File Offset: 0x00002355
		public static LocalizedString E4EViewMessageOTPButton
		{
			get
			{
				return new LocalizedString("E4EViewMessageOTPButton", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004173 File Offset: 0x00002373
		public static LocalizedString HumanText5_1_8
		{
			get
			{
				return new LocalizedString("HumanText5_1_8", "Ex18A34D", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00004191 File Offset: 0x00002391
		public static LocalizedString RejectedNotificationSubjectPrefix
		{
			get
			{
				return new LocalizedString("RejectedNotificationSubjectPrefix", "Ex65F68A", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000041AF File Offset: 0x000023AF
		public static LocalizedString QuotaWarningMailboxMessagesPerFolderCountSubject
		{
			get
			{
				return new LocalizedString("QuotaWarningMailboxMessagesPerFolderCountSubject", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000041CD File Offset: 0x000023CD
		public static LocalizedString ShortText5_7_1
		{
			get
			{
				return new LocalizedString("ShortText5_7_1", "Ex6A075C", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000041EB File Offset: 0x000023EB
		public static LocalizedString HumanText5_2_0
		{
			get
			{
				return new LocalizedString("HumanText5_2_0", "Ex355D15", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00004209 File Offset: 0x00002409
		public static LocalizedString HumanText5_0_0
		{
			get
			{
				return new LocalizedString("HumanText5_0_0", "Ex04D4D6", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004227 File Offset: 0x00002427
		public static LocalizedString EnhancedDsnTextFontTag
		{
			get
			{
				return new LocalizedString("EnhancedDsnTextFontTag", "Ex7A1961", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00004245 File Offset: 0x00002445
		public static LocalizedString ShortText5_2_2
		{
			get
			{
				return new LocalizedString("ShortText5_2_2", "Ex4EEE80", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004264 File Offset: 0x00002464
		public static LocalizedString HumanReadableBoldedToLine(string toAddresses)
		{
			return new LocalizedString("HumanReadableBoldedToLine", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				toAddresses
			});
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00004293 File Offset: 0x00002493
		public static LocalizedString ApprovalRequestPreview
		{
			get
			{
				return new LocalizedString("ApprovalRequestPreview", "Ex0C11DE", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000042B1 File Offset: 0x000024B1
		public static LocalizedString HumanText5_4_2
		{
			get
			{
				return new LocalizedString("HumanText5_4_2", "Ex397263", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000042CF File Offset: 0x000024CF
		public static LocalizedString OriginalHeadersTitle
		{
			get
			{
				return new LocalizedString("OriginalHeadersTitle", "Ex9968C9", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000042ED File Offset: 0x000024ED
		public static LocalizedString ModeratorsOofSubjectPrefix
		{
			get
			{
				return new LocalizedString("ModeratorsOofSubjectPrefix", "Ex7AA69E", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000430B File Offset: 0x0000250B
		public static LocalizedString ModeratorRejectTopText
		{
			get
			{
				return new LocalizedString("ModeratorRejectTopText", "Ex6D6465", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004329 File Offset: 0x00002529
		public static LocalizedString FinalTextFontTag
		{
			get
			{
				return new LocalizedString("FinalTextFontTag", "Ex32AFD3", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004347 File Offset: 0x00002547
		public static LocalizedString ExOrarMailDisplayName
		{
			get
			{
				return new LocalizedString("ExOrarMailDisplayName", "ExC77430", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004365 File Offset: 0x00002565
		public static LocalizedString DSNEnhanced_5_7_1_RESOLVER_RST_DLNeedsSenderRestrictions
		{
			get
			{
				return new LocalizedString("DSNEnhanced_5_7_1_RESOLVER_RST_DLNeedsSenderRestrictions", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004384 File Offset: 0x00002584
		public static LocalizedString DelayedHumanReadableBottomTextHours(int expiryTimeHours, int expiryTimeMinutes)
		{
			return new LocalizedString("DelayedHumanReadableBottomTextHours", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				expiryTimeHours,
				expiryTimeMinutes
			});
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000043C1 File Offset: 0x000025C1
		public static LocalizedString ModeratorCommentsHeader
		{
			get
			{
				return new LocalizedString("ModeratorCommentsHeader", "ExEA0405", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000043DF File Offset: 0x000025DF
		public static LocalizedString ModeratorsNdrSubjectPrefix
		{
			get
			{
				return new LocalizedString("ModeratorsNdrSubjectPrefix", "Ex4045FF", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000043FD File Offset: 0x000025FD
		public static LocalizedString ReceivingServerTitle
		{
			get
			{
				return new LocalizedString("ReceivingServerTitle", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000441B File Offset: 0x0000261B
		public static LocalizedString QuotaSendDetails
		{
			get
			{
				return new LocalizedString("QuotaSendDetails", "Ex70521D", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004439 File Offset: 0x00002639
		public static LocalizedString HumanText5_1_3
		{
			get
			{
				return new LocalizedString("HumanText5_1_3", "Ex42EDF0", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004458 File Offset: 0x00002658
		public static LocalizedString E4EOpenAttachment(string name)
		{
			return new LocalizedString("E4EOpenAttachment", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004488 File Offset: 0x00002688
		public static LocalizedString QuotaWarningFolderHierarchyChildrenNoLimitTopText(string folder, string count)
		{
			return new LocalizedString("QuotaWarningFolderHierarchyChildrenNoLimitTopText", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				folder,
				count
			});
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000044BB File Offset: 0x000026BB
		public static LocalizedString QuarantinedHumanReadableTopText
		{
			get
			{
				return new LocalizedString("QuarantinedHumanReadableTopText", "Ex03E04A", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000044D9 File Offset: 0x000026D9
		public static LocalizedString DeliveredSubject
		{
			get
			{
				return new LocalizedString("DeliveredSubject", "Ex7D49AA", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000044F7 File Offset: 0x000026F7
		public static LocalizedString DSNEnhanced_5_2_3_RESOLVER_RST_RecipSizeLimit_DL
		{
			get
			{
				return new LocalizedString("DSNEnhanced_5_2_3_RESOLVER_RST_RecipSizeLimit_DL", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00004515 File Offset: 0x00002715
		public static LocalizedString DSNEnhanced_5_7_1_APPROVAL_NotAuthorized
		{
			get
			{
				return new LocalizedString("DSNEnhanced_5_7_1_APPROVAL_NotAuthorized", "ExB013C8", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00004533 File Offset: 0x00002733
		public static LocalizedString HumanText5_7_5
		{
			get
			{
				return new LocalizedString("HumanText5_7_5", "Ex65B0FA", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004551 File Offset: 0x00002751
		public static LocalizedString HumanText5_7_8
		{
			get
			{
				return new LocalizedString("HumanText5_7_8", "Ex852059", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000456F File Offset: 0x0000276F
		public static LocalizedString DecisionConflictNotificationSubjectPrefix
		{
			get
			{
				return new LocalizedString("DecisionConflictNotificationSubjectPrefix", "Ex163DA9", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000458D File Offset: 0x0000278D
		public static LocalizedString QuotaWarningNoLimitSubjectPF
		{
			get
			{
				return new LocalizedString("QuotaWarningNoLimitSubjectPF", "Ex814216", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000045AB File Offset: 0x000027AB
		public static LocalizedString BodyDownload
		{
			get
			{
				return new LocalizedString("BodyDownload", "ExF04F81", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000045C9 File Offset: 0x000027C9
		public static LocalizedString QuotaProhibitReceiveFoldersCountSubject
		{
			get
			{
				return new LocalizedString("QuotaProhibitReceiveFoldersCountSubject", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000045E7 File Offset: 0x000027E7
		public static LocalizedString ShortText5_7_100
		{
			get
			{
				return new LocalizedString("ShortText5_7_100", "Ex04234F", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004608 File Offset: 0x00002808
		public static LocalizedString ModeratedDLApprovalRequest(string senderName)
		{
			return new LocalizedString("ModeratedDLApprovalRequest", "Ex1893BF", false, true, SystemMessages.ResourceManager, new object[]
			{
				senderName
			});
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004637 File Offset: 0x00002837
		public static LocalizedString QuotaWarningTopText
		{
			get
			{
				return new LocalizedString("QuotaWarningTopText", "ExC96165", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004655 File Offset: 0x00002855
		public static LocalizedString ShortText5_7_300
		{
			get
			{
				return new LocalizedString("ShortText5_7_300", "Ex666A2C", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004673 File Offset: 0x00002873
		public static LocalizedString FailedHumanReadableTopTextForTextMessageNotification
		{
			get
			{
				return new LocalizedString("FailedHumanReadableTopTextForTextMessageNotification", "Ex5B9C89", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004691 File Offset: 0x00002891
		public static LocalizedString DelayHumanReadableExplanation
		{
			get
			{
				return new LocalizedString("DelayHumanReadableExplanation", "Ex764691", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000046AF File Offset: 0x000028AF
		public static LocalizedString ShortText5_2_3
		{
			get
			{
				return new LocalizedString("ShortText5_2_3", "Ex38D1AF", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000046D0 File Offset: 0x000028D0
		public static LocalizedString QuotaProhibitReceiveFolderHierarchyChildrenCountTopText(string folder, string count)
		{
			return new LocalizedString("QuotaProhibitReceiveFolderHierarchyChildrenCountTopText", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				folder,
				count
			});
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004703 File Offset: 0x00002903
		public static LocalizedString InternetConfidentialDescription
		{
			get
			{
				return new LocalizedString("InternetConfidentialDescription", "ExE274BE", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004721 File Offset: 0x00002921
		public static LocalizedString HumanText5_2_2
		{
			get
			{
				return new LocalizedString("HumanText5_2_2", "ExFA7D2D", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004740 File Offset: 0x00002940
		public static LocalizedString ExternalFailedHumanReadableErrorText(string externalserver)
		{
			return new LocalizedString("ExternalFailedHumanReadableErrorText", "Ex50A06C", false, true, SystemMessages.ResourceManager, new object[]
			{
				externalserver
			});
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000476F File Offset: 0x0000296F
		public static LocalizedString ShortText5_4_7
		{
			get
			{
				return new LocalizedString("ShortText5_4_7", "Ex10388D", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000478D File Offset: 0x0000298D
		public static LocalizedString ApprovalRequestTopText
		{
			get
			{
				return new LocalizedString("ApprovalRequestTopText", "ExBB0B32", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000047AB File Offset: 0x000029AB
		public static LocalizedString HumanText5_4_4
		{
			get
			{
				return new LocalizedString("HumanText5_4_4", "ExFEC724", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000047C9 File Offset: 0x000029C9
		public static LocalizedString QuotaWarningFolderHierarchyDepthNoLimitSubject
		{
			get
			{
				return new LocalizedString("QuotaWarningFolderHierarchyDepthNoLimitSubject", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000047E7 File Offset: 0x000029E7
		public static LocalizedString ModerationOofNotificationForSenderTopText
		{
			get
			{
				return new LocalizedString("ModerationOofNotificationForSenderTopText", "ExD3B813", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004805 File Offset: 0x00002A05
		public static LocalizedString ShortText5_4_8
		{
			get
			{
				return new LocalizedString("ShortText5_4_8", "Ex818E63", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004824 File Offset: 0x00002A24
		public static LocalizedString DecisionConflictWithDetailsNotification(string decisionMaker, string decision, string time, string timeZone)
		{
			return new LocalizedString("DecisionConflictWithDetailsNotification", "Ex813718", false, true, SystemMessages.ResourceManager, new object[]
			{
				decisionMaker,
				decision,
				time,
				timeZone
			});
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000485F File Offset: 0x00002A5F
		public static LocalizedString ExpandedSubject
		{
			get
			{
				return new LocalizedString("ExpandedSubject", "Ex191809", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000487D File Offset: 0x00002A7D
		public static LocalizedString HumanText5_5_5
		{
			get
			{
				return new LocalizedString("HumanText5_5_5", "Ex34C9AA", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000489B File Offset: 0x00002A9B
		public static LocalizedString DataCenterHumanText5_1_1
		{
			get
			{
				return new LocalizedString("DataCenterHumanText5_1_1", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000048B9 File Offset: 0x00002AB9
		public static LocalizedString ArchiveQuotaWarningDetails
		{
			get
			{
				return new LocalizedString("ArchiveQuotaWarningDetails", "ExA3ED4F", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000048D8 File Offset: 0x00002AD8
		public static LocalizedString DsnParamTextMessageSizePerMessageInMB(string currentSize, string maxSize)
		{
			return new LocalizedString("DsnParamTextMessageSizePerMessageInMB", "Ex3CAC48", false, true, SystemMessages.ResourceManager, new object[]
			{
				currentSize,
				maxSize
			});
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000490B File Offset: 0x00002B0B
		public static LocalizedString E4EViewMessageInfo
		{
			get
			{
				return new LocalizedString("E4EViewMessageInfo", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000492C File Offset: 0x00002B2C
		public static LocalizedString DecisionConflictNotification(string subject)
		{
			return new LocalizedString("DecisionConflictNotification", "Ex4DDABA", false, true, SystemMessages.ResourceManager, new object[]
			{
				subject
			});
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000495B File Offset: 0x00002B5B
		public static LocalizedString DeliveredHumanReadableTopText
		{
			get
			{
				return new LocalizedString("DeliveredHumanReadableTopText", "Ex21EDE0", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004979 File Offset: 0x00002B79
		public static LocalizedString RelayedSubject
		{
			get
			{
				return new LocalizedString("RelayedSubject", "Ex34EFE0", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004997 File Offset: 0x00002B97
		public static LocalizedString HumanText5_3_4
		{
			get
			{
				return new LocalizedString("HumanText5_3_4", "Ex9EC0EA", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000049B5 File Offset: 0x00002BB5
		public static LocalizedString ShortText5_2_1
		{
			get
			{
				return new LocalizedString("ShortText5_2_1", "ExE38CD1", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000049D3 File Offset: 0x00002BD3
		public static LocalizedString QuotaSendSubject
		{
			get
			{
				return new LocalizedString("QuotaSendSubject", "Ex591232", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000049F1 File Offset: 0x00002BF1
		public static LocalizedString ShortText5_1_1
		{
			get
			{
				return new LocalizedString("ShortText5_1_1", "Ex99BDE6", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004A0F File Offset: 0x00002C0F
		public static LocalizedString DiagnosticsFontTag
		{
			get
			{
				return new LocalizedString("DiagnosticsFontTag", "Ex6CC99E", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004A30 File Offset: 0x00002C30
		public static LocalizedString HumanReadableBoldedCcLine(string ccAddresses)
		{
			return new LocalizedString("HumanReadableBoldedCcLine", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				ccAddresses
			});
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004A5F File Offset: 0x00002C5F
		public static LocalizedString QuotaWarningFoldersCountNoLimitDetails
		{
			get
			{
				return new LocalizedString("QuotaWarningFoldersCountNoLimitDetails", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004A7D File Offset: 0x00002C7D
		public static LocalizedString HumanText5_7_4
		{
			get
			{
				return new LocalizedString("HumanText5_7_4", "ExEE4688", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004A9B File Offset: 0x00002C9B
		public static LocalizedString QuotaProhibitReceiveFolderHierarchyDepthDetails
		{
			get
			{
				return new LocalizedString("QuotaProhibitReceiveFolderHierarchyDepthDetails", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004ABC File Offset: 0x00002CBC
		public static LocalizedString ModeratorExpiryNotification(string sender, int expiryTime)
		{
			return new LocalizedString("ModeratorExpiryNotification", "Ex515F05", false, true, SystemMessages.ResourceManager, new object[]
			{
				sender,
				expiryTime
			});
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004AF4 File Offset: 0x00002CF4
		public static LocalizedString ApprovalRequestExpiryNotification(string subject)
		{
			return new LocalizedString("ApprovalRequestExpiryNotification", "Ex8588C0", false, true, SystemMessages.ResourceManager, new object[]
			{
				subject
			});
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004B23 File Offset: 0x00002D23
		public static LocalizedString QuotaWarningFoldersCountDetails
		{
			get
			{
				return new LocalizedString("QuotaWarningFoldersCountDetails", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004B41 File Offset: 0x00002D41
		public static LocalizedString E4EReceivedMessage
		{
			get
			{
				return new LocalizedString("E4EReceivedMessage", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004B5F File Offset: 0x00002D5F
		public static LocalizedString ExPartnerMailDisplayName
		{
			get
			{
				return new LocalizedString("ExPartnerMailDisplayName", "ExCB1EDF", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004B7D File Offset: 0x00002D7D
		public static LocalizedString ExAttachmentRemovedDisplayName
		{
			get
			{
				return new LocalizedString("ExAttachmentRemovedDisplayName", "Ex0BF4FE", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004B9B File Offset: 0x00002D9B
		public static LocalizedString ShortText5_3_3
		{
			get
			{
				return new LocalizedString("ShortText5_3_3", "Ex765DB3", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004BB9 File Offset: 0x00002DB9
		public static LocalizedString QuotaWarningDetailsPF
		{
			get
			{
				return new LocalizedString("QuotaWarningDetailsPF", "ExAD6D3E", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004BD7 File Offset: 0x00002DD7
		public static LocalizedString ArchiveQuotaWarningNoLimitDetails
		{
			get
			{
				return new LocalizedString("ArchiveQuotaWarningNoLimitDetails", "ExC33E78", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004BF5 File Offset: 0x00002DF5
		public static LocalizedString GeneratingServerTitle
		{
			get
			{
				return new LocalizedString("GeneratingServerTitle", "Ex3CEBB8", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004C13 File Offset: 0x00002E13
		public static LocalizedString QuotaSendReceiveSubject
		{
			get
			{
				return new LocalizedString("QuotaSendReceiveSubject", "Ex0F3D81", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004C31 File Offset: 0x00002E31
		public static LocalizedString ShortText5_1_8
		{
			get
			{
				return new LocalizedString("ShortText5_1_8", "Ex017A6D", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004C4F File Offset: 0x00002E4F
		public static LocalizedString ShortText5_1_3
		{
			get
			{
				return new LocalizedString("ShortText5_1_3", "ExA7F278", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004C6D File Offset: 0x00002E6D
		public static LocalizedString ExOrarMailRecipientDescription
		{
			get
			{
				return new LocalizedString("ExOrarMailRecipientDescription", "Ex466A0C", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004C8B File Offset: 0x00002E8B
		public static LocalizedString ArchiveQuotaFullSubject
		{
			get
			{
				return new LocalizedString("ArchiveQuotaFullSubject", "Ex4AFF7C", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004CA9 File Offset: 0x00002EA9
		public static LocalizedString QuotaWarningNoLimitDetailsPF
		{
			get
			{
				return new LocalizedString("QuotaWarningNoLimitDetailsPF", "Ex0FD9BD", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004CC7 File Offset: 0x00002EC7
		public static LocalizedString ShortText5_4_4
		{
			get
			{
				return new LocalizedString("ShortText5_4_4", "Ex3C1A13", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004CE5 File Offset: 0x00002EE5
		public static LocalizedString HumanText_InitMsg
		{
			get
			{
				return new LocalizedString("HumanText_InitMsg", "Ex28619D", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004D03 File Offset: 0x00002F03
		public static LocalizedString HumanText5_7_300
		{
			get
			{
				return new LocalizedString("HumanText5_7_300", "ExD068FC", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004D24 File Offset: 0x00002F24
		public static LocalizedString ExternalFailedHumanReadableTopText(string externalserver)
		{
			return new LocalizedString("ExternalFailedHumanReadableTopText", "ExF565D8", false, true, SystemMessages.ResourceManager, new object[]
			{
				externalserver
			});
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004D53 File Offset: 0x00002F53
		public static LocalizedString BodyReceiveRMEmail
		{
			get
			{
				return new LocalizedString("BodyReceiveRMEmail", "Ex7140EA", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004D71 File Offset: 0x00002F71
		public static LocalizedString QuotaWarningFoldersCountSubject
		{
			get
			{
				return new LocalizedString("QuotaWarningFoldersCountSubject", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004D8F File Offset: 0x00002F8F
		public static LocalizedString QuotaWarningDetails
		{
			get
			{
				return new LocalizedString("QuotaWarningDetails", "Ex69BEE7", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004DAD File Offset: 0x00002FAD
		public static LocalizedString HumanText5_3_0
		{
			get
			{
				return new LocalizedString("HumanText5_3_0", "ExC6D5CB", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004DCB File Offset: 0x00002FCB
		public static LocalizedString HumanText5_7_100
		{
			get
			{
				return new LocalizedString("HumanText5_7_100", "ExB0DB4D", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004DE9 File Offset: 0x00002FE9
		public static LocalizedString HumanText5_1_6
		{
			get
			{
				return new LocalizedString("HumanText5_1_6", "Ex15812E", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004E07 File Offset: 0x00003007
		public static LocalizedString ShortText5_7_7
		{
			get
			{
				return new LocalizedString("ShortText5_7_7", "Ex51E620", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004E25 File Offset: 0x00003025
		public static LocalizedString QuotaWarningFolderHierarchyChildrenNoLimitSubject
		{
			get
			{
				return new LocalizedString("QuotaWarningFolderHierarchyChildrenNoLimitSubject", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004E43 File Offset: 0x00003043
		public static LocalizedString ShortText5_7_0
		{
			get
			{
				return new LocalizedString("ShortText5_7_0", "ExC2157B", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004E61 File Offset: 0x00003061
		public static LocalizedString HumanText5_5_0
		{
			get
			{
				return new LocalizedString("HumanText5_5_0", "ExE647CD", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004E7F File Offset: 0x0000307F
		public static LocalizedString QuotaProhibitReceiveFolderHierarchyChildrenCountDetails
		{
			get
			{
				return new LocalizedString("QuotaProhibitReceiveFolderHierarchyChildrenCountDetails", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004E9D File Offset: 0x0000309D
		public static LocalizedString ShortText5_1_0
		{
			get
			{
				return new LocalizedString("ShortText5_1_0", "Ex2FC69C", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004EBB File Offset: 0x000030BB
		public static LocalizedString QuotaWarningFoldersCountNoLimitSubject
		{
			get
			{
				return new LocalizedString("QuotaWarningFoldersCountNoLimitSubject", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004ED9 File Offset: 0x000030D9
		public static LocalizedString HumanText5_4_0
		{
			get
			{
				return new LocalizedString("HumanText5_4_0", "Ex25AC0A", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004EF7 File Offset: 0x000030F7
		public static LocalizedString ExAttachmentRemovedRecipientDescription
		{
			get
			{
				return new LocalizedString("ExAttachmentRemovedRecipientDescription", "Ex8DF76F", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004F15 File Offset: 0x00003115
		public static LocalizedString QuotaCurrentSize
		{
			get
			{
				return new LocalizedString("QuotaCurrentSize", "ExAC7949", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004F33 File Offset: 0x00003133
		public static LocalizedString DecisionUpdateTopText
		{
			get
			{
				return new LocalizedString("DecisionUpdateTopText", "Ex088DD0", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004F51 File Offset: 0x00003151
		public static LocalizedString HumanText5_4_1
		{
			get
			{
				return new LocalizedString("HumanText5_4_1", "Ex2A4CBE", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004F70 File Offset: 0x00003170
		public static LocalizedString QuotaWarningMailboxMessagesPerFolderCountTopText(string folder, string count)
		{
			return new LocalizedString("QuotaWarningMailboxMessagesPerFolderCountTopText", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				folder,
				count
			});
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004FA4 File Offset: 0x000031A4
		public static LocalizedString ExternalFailedHumanReadableErrorNoDetailText(string externalserver)
		{
			return new LocalizedString("ExternalFailedHumanReadableErrorNoDetailText", "ExDC79C1", false, true, SystemMessages.ResourceManager, new object[]
			{
				externalserver
			});
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004FD3 File Offset: 0x000031D3
		public static LocalizedString ShortText5_4_6
		{
			get
			{
				return new LocalizedString("ShortText5_4_6", "Ex1A0BC6", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004FF1 File Offset: 0x000031F1
		public static LocalizedString ShortText5_7_6
		{
			get
			{
				return new LocalizedString("ShortText5_7_6", "ExE82864", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000500F File Offset: 0x0000320F
		public static LocalizedString ApprovalRequestSubjectPrefix
		{
			get
			{
				return new LocalizedString("ApprovalRequestSubjectPrefix", "ExE89649", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000502D File Offset: 0x0000322D
		public static LocalizedString E4ESignIn
		{
			get
			{
				return new LocalizedString("E4ESignIn", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000504B File Offset: 0x0000324B
		public static LocalizedString InternetConfidentialName
		{
			get
			{
				return new LocalizedString("InternetConfidentialName", "ExC319D4", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005069 File Offset: 0x00003269
		public static LocalizedString HumanText5_7_7
		{
			get
			{
				return new LocalizedString("HumanText5_7_7", "Ex750074", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005087 File Offset: 0x00003287
		public static LocalizedString ShortText5_7_3
		{
			get
			{
				return new LocalizedString("ShortText5_7_3", "ExA8A389", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000050A5 File Offset: 0x000032A5
		public static LocalizedString QuotaWarningNoLimitDetails
		{
			get
			{
				return new LocalizedString("QuotaWarningNoLimitDetails", "Ex1A7280", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000050C3 File Offset: 0x000032C3
		public static LocalizedString E4EEncryptedMessage
		{
			get
			{
				return new LocalizedString("E4EEncryptedMessage", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000050E1 File Offset: 0x000032E1
		public static LocalizedString HumanText5_5_1
		{
			get
			{
				return new LocalizedString("HumanText5_5_1", "Ex4A8919", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005100 File Offset: 0x00003300
		public static LocalizedString QuotaWarningFoldersCountTopText(string count)
		{
			return new LocalizedString("QuotaWarningFoldersCountTopText", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000512F File Offset: 0x0000332F
		public static LocalizedString DataCenterHumanText5_7_1
		{
			get
			{
				return new LocalizedString("DataCenterHumanText5_7_1", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000514D File Offset: 0x0000334D
		public static LocalizedString ShortText5_5_6
		{
			get
			{
				return new LocalizedString("ShortText5_5_6", "ExCFD2EE", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000516B File Offset: 0x0000336B
		public static LocalizedString QuotaWarningFolderHierarchyChildrenCountDetails
		{
			get
			{
				return new LocalizedString("QuotaWarningFolderHierarchyChildrenCountDetails", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000518C File Offset: 0x0000338C
		public static LocalizedString SizeInFolders(string count)
		{
			return new LocalizedString("SizeInFolders", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000051BC File Offset: 0x000033BC
		public static LocalizedString HumanReadableSubjectLine(string subject)
		{
			return new LocalizedString("HumanReadableSubjectLine", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				subject
			});
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000051EB File Offset: 0x000033EB
		public static LocalizedString ShortText5_7_9
		{
			get
			{
				return new LocalizedString("ShortText5_7_9", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005209 File Offset: 0x00003409
		public static LocalizedString ShortText5_1_2
		{
			get
			{
				return new LocalizedString("ShortText5_1_2", "Ex45C40E", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005227 File Offset: 0x00003427
		public static LocalizedString QuotaWarningMailboxMessagesPerFolderCountDetails
		{
			get
			{
				return new LocalizedString("QuotaWarningMailboxMessagesPerFolderCountDetails", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00005245 File Offset: 0x00003445
		public static LocalizedString HumanText5_7_3
		{
			get
			{
				return new LocalizedString("HumanText5_7_3", "ExEB0312", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005263 File Offset: 0x00003463
		public static LocalizedString DoNotForwardDescription
		{
			get
			{
				return new LocalizedString("DoNotForwardDescription", "ExE9ED1D", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005281 File Offset: 0x00003481
		public static LocalizedString ModerationExpiryNoticationForSenderTopText
		{
			get
			{
				return new LocalizedString("ModerationExpiryNoticationForSenderTopText", "Ex3EB3AA", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000529F File Offset: 0x0000349F
		public static LocalizedString HumanText5_4_7
		{
			get
			{
				return new LocalizedString("HumanText5_4_7", "Ex57C427", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000052C0 File Offset: 0x000034C0
		public static LocalizedString DelayedHumanReadableBottomText(int expiryTimeDays, int expiryTimeHours, int expiryTimeMinutes)
		{
			return new LocalizedString("DelayedHumanReadableBottomText", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				expiryTimeDays,
				expiryTimeHours,
				expiryTimeMinutes
			});
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005306 File Offset: 0x00003506
		public static LocalizedString DataCenterHumanText4_4_7
		{
			get
			{
				return new LocalizedString("DataCenterHumanText4_4_7", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00005324 File Offset: 0x00003524
		public static LocalizedString HumanText5_6_3
		{
			get
			{
				return new LocalizedString("HumanText5_6_3", "Ex9A4CB3", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005342 File Offset: 0x00003542
		public static LocalizedString HumanText5_3_2
		{
			get
			{
				return new LocalizedString("HumanText5_3_2", "Ex823C2B", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005360 File Offset: 0x00003560
		public static LocalizedString ApprovalCommentAttachmentFilename
		{
			get
			{
				return new LocalizedString("ApprovalCommentAttachmentFilename", "Ex948363", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000537E File Offset: 0x0000357E
		public static LocalizedString ArchiveQuotaWarningNoLimitSubject
		{
			get
			{
				return new LocalizedString("ArchiveQuotaWarningNoLimitSubject", "ExE3A691", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000539C File Offset: 0x0000359C
		public static LocalizedString HumanText5_1_4
		{
			get
			{
				return new LocalizedString("HumanText5_1_4", "Ex8CE1B3", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000053BA File Offset: 0x000035BA
		public static LocalizedString E4EHosted
		{
			get
			{
				return new LocalizedString("E4EHosted", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000053D8 File Offset: 0x000035D8
		public static LocalizedString QuotaSendReceiveDetails
		{
			get
			{
				return new LocalizedString("QuotaSendReceiveDetails", "Ex9C6C59", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000053F6 File Offset: 0x000035F6
		public static LocalizedString ShortText5_2_4
		{
			get
			{
				return new LocalizedString("ShortText5_2_4", "Ex3BD4EB", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005414 File Offset: 0x00003614
		public static LocalizedString DSNEnhanced_5_7_1_RESOLVER_RST_NotAuthorized
		{
			get
			{
				return new LocalizedString("DSNEnhanced_5_7_1_RESOLVER_RST_NotAuthorized", "Ex62D442", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005432 File Offset: 0x00003632
		public static LocalizedString ShortText5_5_4
		{
			get
			{
				return new LocalizedString("ShortText5_5_4", "ExD5A915", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005450 File Offset: 0x00003650
		public static LocalizedString QuotaWarningMailboxMessagesPerFolderNoLimitDetails
		{
			get
			{
				return new LocalizedString("QuotaWarningMailboxMessagesPerFolderNoLimitDetails", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000546E File Offset: 0x0000366E
		public static LocalizedString ShortText5_6_4
		{
			get
			{
				return new LocalizedString("ShortText5_6_4", "Ex54C089", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000548C File Offset: 0x0000368C
		public static LocalizedString ShortText5_1_4
		{
			get
			{
				return new LocalizedString("ShortText5_1_4", "Ex25B38E", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000054AA File Offset: 0x000036AA
		public static LocalizedString HumanText5_1_2
		{
			get
			{
				return new LocalizedString("HumanText5_1_2", "Ex0CE300", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000054C8 File Offset: 0x000036C8
		public static LocalizedString QuotaWarningFolderHierarchyChildrenCountSubject
		{
			get
			{
				return new LocalizedString("QuotaWarningFolderHierarchyChildrenCountSubject", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000054E6 File Offset: 0x000036E6
		public static LocalizedString HumanText5_5_2
		{
			get
			{
				return new LocalizedString("HumanText5_5_2", "ExE2A31B", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005504 File Offset: 0x00003704
		public static LocalizedString ShortText5_4_0
		{
			get
			{
				return new LocalizedString("ShortText5_4_0", "ExFCD628", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00005522 File Offset: 0x00003722
		public static LocalizedString HumanText5_7_1
		{
			get
			{
				return new LocalizedString("HumanText5_7_1", "Ex41CD9B", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005540 File Offset: 0x00003740
		public static LocalizedString DsnParamTextMessageSizePerMessageInKB(string currentSize, string maxSize)
		{
			return new LocalizedString("DsnParamTextMessageSizePerMessageInKB", "ExB8F513", false, true, SystemMessages.ResourceManager, new object[]
			{
				currentSize,
				maxSize
			});
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005573 File Offset: 0x00003773
		public static LocalizedString HumanText5_2_4
		{
			get
			{
				return new LocalizedString("HumanText5_2_4", "Ex5D1D57", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005594 File Offset: 0x00003794
		public static LocalizedString DsnParamTextMessageSizePerRecipientInKB(string currentSize, string maxSize)
		{
			return new LocalizedString("DsnParamTextMessageSizePerRecipientInKB", "Ex588BA3", false, true, SystemMessages.ResourceManager, new object[]
			{
				currentSize,
				maxSize
			});
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000055C7 File Offset: 0x000037C7
		public static LocalizedString DSNEnhanced_5_2_3_RESOLVER_RST_RecipSizeLimit
		{
			get
			{
				return new LocalizedString("DSNEnhanced_5_2_3_RESOLVER_RST_RecipSizeLimit", "ExE616ED", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000055E8 File Offset: 0x000037E8
		public static LocalizedString QuotaWarningTopTextPF(string publicFolderName)
		{
			return new LocalizedString("QuotaWarningTopTextPF", "Ex37F059", false, true, SystemMessages.ResourceManager, new object[]
			{
				publicFolderName
			});
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00005617 File Offset: 0x00003817
		public static LocalizedString E4EWaitMessage
		{
			get
			{
				return new LocalizedString("E4EWaitMessage", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005635 File Offset: 0x00003835
		public static LocalizedString ApprovalRequestExpiryTopText
		{
			get
			{
				return new LocalizedString("ApprovalRequestExpiryTopText", "Ex39753B", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005653 File Offset: 0x00003853
		public static LocalizedString E4EViewMessageButton
		{
			get
			{
				return new LocalizedString("E4EViewMessageButton", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005671 File Offset: 0x00003871
		public static LocalizedString ShortText5_6_3
		{
			get
			{
				return new LocalizedString("ShortText5_6_3", "Ex73807B", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005690 File Offset: 0x00003890
		public static LocalizedString QuotaSendTopTextPF(string publicFolderName)
		{
			return new LocalizedString("QuotaSendTopTextPF", "Ex839B3B", false, true, SystemMessages.ResourceManager, new object[]
			{
				publicFolderName
			});
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000056C0 File Offset: 0x000038C0
		public static LocalizedString QuotaWarningNoLimitTopTextPF(string publicFolderName, string size)
		{
			return new LocalizedString("QuotaWarningNoLimitTopTextPF", "ExF36392", false, true, SystemMessages.ResourceManager, new object[]
			{
				publicFolderName,
				size
			});
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000056F3 File Offset: 0x000038F3
		public static LocalizedString HumanText5_1_1
		{
			get
			{
				return new LocalizedString("HumanText5_1_1", "Ex53AE07", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005711 File Offset: 0x00003911
		public static LocalizedString Moderation_Reencryption_Exception
		{
			get
			{
				return new LocalizedString("Moderation_Reencryption_Exception", "Ex0DDCB9", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005730 File Offset: 0x00003930
		public static LocalizedString QuotaWarningNoLimitTopText(string size)
		{
			return new LocalizedString("QuotaWarningNoLimitTopText", "ExFFD076", false, true, SystemMessages.ResourceManager, new object[]
			{
				size
			});
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000575F File Offset: 0x0000395F
		public static LocalizedString ShortText5_6_0
		{
			get
			{
				return new LocalizedString("ShortText5_6_0", "Ex9D8F56", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000577D File Offset: 0x0000397D
		public static LocalizedString ShortText5_6_1
		{
			get
			{
				return new LocalizedString("ShortText5_6_1", "Ex994293", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000579C File Offset: 0x0000399C
		public static LocalizedString QuotaWarningMailboxMessagesPerFolderNoLimitTopText(string folder, string count)
		{
			return new LocalizedString("QuotaWarningMailboxMessagesPerFolderNoLimitTopText", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				folder,
				count
			});
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000057CF File Offset: 0x000039CF
		public static LocalizedString ApprovalRequestExpiredNotificationSubjectPrefix
		{
			get
			{
				return new LocalizedString("ApprovalRequestExpiredNotificationSubjectPrefix", "Ex26246C", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000057ED File Offset: 0x000039ED
		public static LocalizedString HumanText5_5_4
		{
			get
			{
				return new LocalizedString("HumanText5_5_4", "ExD72E6F", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000580B File Offset: 0x00003A0B
		public static LocalizedString ShortText5_4_1
		{
			get
			{
				return new LocalizedString("ShortText5_4_1", "Ex844FC6", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005829 File Offset: 0x00003A29
		public static LocalizedString QuotaWarningNoLimitSubject
		{
			get
			{
				return new LocalizedString("QuotaWarningNoLimitSubject", "ExD6C43B", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005848 File Offset: 0x00003A48
		public static LocalizedString HumanTextFailedPasscodeWithReason(string number, string error)
		{
			return new LocalizedString("HumanTextFailedPasscodeWithReason", "Ex694130", false, true, SystemMessages.ResourceManager, new object[]
			{
				number,
				error
			});
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000587C File Offset: 0x00003A7C
		public static LocalizedString SizeInMessages(string count)
		{
			return new LocalizedString("SizeInMessages", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000058AB File Offset: 0x00003AAB
		public static LocalizedString DecisionConflictTopText
		{
			get
			{
				return new LocalizedString("DecisionConflictTopText", "Ex5159BD", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000058C9 File Offset: 0x00003AC9
		public static LocalizedString FailedHumanReadableTopTextForTextMessage
		{
			get
			{
				return new LocalizedString("FailedHumanReadableTopTextForTextMessage", "ExCCCB72", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000058E7 File Offset: 0x00003AE7
		public static LocalizedString HumanText5_6_2
		{
			get
			{
				return new LocalizedString("HumanText5_6_2", "Ex2DC288", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005905 File Offset: 0x00003B05
		public static LocalizedString QuotaSendDetailsPF
		{
			get
			{
				return new LocalizedString("QuotaSendDetailsPF", "ExDFB39B", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005923 File Offset: 0x00003B23
		public static LocalizedString HumanText5_7_6
		{
			get
			{
				return new LocalizedString("HumanText5_7_6", "ExAED519", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005941 File Offset: 0x00003B41
		public static LocalizedString QuotaSendReceiveTopText
		{
			get
			{
				return new LocalizedString("QuotaSendReceiveTopText", "Ex8C5663", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000595F File Offset: 0x00003B5F
		public static LocalizedString HumanText5_2_3
		{
			get
			{
				return new LocalizedString("HumanText5_2_3", "Ex4E71B6", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000597D File Offset: 0x00003B7D
		public static LocalizedString ApproveButtonText
		{
			get
			{
				return new LocalizedString("ApproveButtonText", "Ex32115A", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000599B File Offset: 0x00003B9B
		public static LocalizedString DiagnosticInformationTitle
		{
			get
			{
				return new LocalizedString("DiagnosticInformationTitle", "Ex0BFB98", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000059BC File Offset: 0x00003BBC
		public static LocalizedString HumanReadableBoldedFromLine(string from)
		{
			return new LocalizedString("HumanReadableBoldedFromLine", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				from
			});
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000059EB File Offset: 0x00003BEB
		public static LocalizedString HumanText5_4_6
		{
			get
			{
				return new LocalizedString("HumanText5_4_6", "Ex1FED2A", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005A0C File Offset: 0x00003C0C
		public static LocalizedString HumanTextFailedPasscodeWithoutReason(string number)
		{
			return new LocalizedString("HumanTextFailedPasscodeWithoutReason", "Ex39BE84", false, true, SystemMessages.ResourceManager, new object[]
			{
				number
			});
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005A3B File Offset: 0x00003C3B
		public static LocalizedString HumanText5_5_6
		{
			get
			{
				return new LocalizedString("HumanText5_5_6", "Ex34BE17", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005A5C File Offset: 0x00003C5C
		public static LocalizedString QuotaWarningFolderHierarchyChildrenCountTopText(string folder, string count)
		{
			return new LocalizedString("QuotaWarningFolderHierarchyChildrenCountTopText", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				folder,
				count
			});
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005A8F File Offset: 0x00003C8F
		public static LocalizedString ShortText5_5_5
		{
			get
			{
				return new LocalizedString("ShortText5_5_5", "Ex2633AC", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005AAD File Offset: 0x00003CAD
		public static LocalizedString ShortText5_7_5
		{
			get
			{
				return new LocalizedString("ShortText5_7_5", "Ex90F8FD", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005ACC File Offset: 0x00003CCC
		public static LocalizedString HumanTextFailedOmsNotification(string bodyText, string error)
		{
			return new LocalizedString("HumanTextFailedOmsNotification", "Ex89D5CB", false, true, SystemMessages.ResourceManager, new object[]
			{
				bodyText,
				error
			});
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005AFF File Offset: 0x00003CFF
		public static LocalizedString DSNEnhanced_5_7_1_TRANSPORT_RULES_RejectMessage
		{
			get
			{
				return new LocalizedString("DSNEnhanced_5_7_1_TRANSPORT_RULES_RejectMessage", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005B1D File Offset: 0x00003D1D
		public static LocalizedString HumanText5_3_1
		{
			get
			{
				return new LocalizedString("HumanText5_3_1", "ExE2A518", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005B3C File Offset: 0x00003D3C
		public static LocalizedString HumanReadableRemoteServerText(string remoteServer)
		{
			return new LocalizedString("HumanReadableRemoteServerText", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				remoteServer
			});
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005B6B File Offset: 0x00003D6B
		public static LocalizedString ShortText5_3_1
		{
			get
			{
				return new LocalizedString("ShortText5_3_1", "Ex11B600", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005B8C File Offset: 0x00003D8C
		public static LocalizedString SizeInMB(string count)
		{
			return new LocalizedString("SizeInMB", "", false, false, SystemMessages.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005BBB File Offset: 0x00003DBB
		public static LocalizedString ShortText5_3_2
		{
			get
			{
				return new LocalizedString("ShortText5_3_2", "ExED88AE", false, true, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00005BD9 File Offset: 0x00003DD9
		public static LocalizedString ShortText5_7_10
		{
			get
			{
				return new LocalizedString("ShortText5_7_10", "", false, false, SystemMessages.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005BF7 File Offset: 0x00003DF7
		public static LocalizedString GetLocalizedString(SystemMessages.IDs key)
		{
			return new LocalizedString(SystemMessages.stringIDs[(uint)key], SystemMessages.ResourceManager, new object[0]);
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(253);

		// Token: 0x04000002 RID: 2
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Core.SystemMessages", typeof(SystemMessages).GetTypeInfo().Assembly);

		// Token: 0x02000003 RID: 3
		public enum IDs : uint
		{
			// Token: 0x04000004 RID: 4
			HumanText5_2_1 = 3060389430U,
			// Token: 0x04000005 RID: 5
			ArchiveQuotaWarningSubject = 224226320U,
			// Token: 0x04000006 RID: 6
			ShortText5_1_7 = 1616176476U,
			// Token: 0x04000007 RID: 7
			DSNEnhanced_5_4_4_SMTPSEND_DNS_NonExistentDomain = 955676612U,
			// Token: 0x04000008 RID: 8
			DataCenterHumanText5_4_6 = 4216909332U,
			// Token: 0x04000009 RID: 9
			ExAttachmentRemovedSenderDescription = 2547234435U,
			// Token: 0x0400000A RID: 10
			HumanText5_3_5 = 3483876933U,
			// Token: 0x0400000B RID: 11
			DSNEnhanced_5_2_3_QUEUE_Priority = 3038358855U,
			// Token: 0x0400000C RID: 12
			ShortText5_3_5 = 171052060U,
			// Token: 0x0400000D RID: 13
			HumanText5_4_8 = 2132860851U,
			// Token: 0x0400000E RID: 14
			QuotaWarningSubjectPF = 264042572U,
			// Token: 0x0400000F RID: 15
			E4EViewMessage = 1070379938U,
			// Token: 0x04000010 RID: 16
			ReadSubject = 1498176974U,
			// Token: 0x04000011 RID: 17
			HumanReadableFinalText = 3726703438U,
			// Token: 0x04000012 RID: 18
			DSNEnhanced_5_2_3_RESOLVER_RST_SendSizeLimit_Sender = 1459475246U,
			// Token: 0x04000013 RID: 19
			ArchiveQuotaWarningTopText = 2954596652U,
			// Token: 0x04000014 RID: 20
			ShortText5_6_2 = 3283016966U,
			// Token: 0x04000015 RID: 21
			DSNEnhanced_5_7_1_RESOLVER_RST_NotAuthorizedToGroup = 1494114401U,
			// Token: 0x04000016 RID: 22
			QuotaWarningFolderHierarchyDepthSubject = 286121318U,
			// Token: 0x04000017 RID: 23
			DecisionProcessedNotificationSubjectPrefix = 1411446847U,
			// Token: 0x04000018 RID: 24
			QuotaWarningFolderHierarchyDepthNoLimitDetails = 4198661606U,
			// Token: 0x04000019 RID: 25
			QuotaProhibitReceiveFolderHierarchyDepthSubject = 3925426878U,
			// Token: 0x0400001A RID: 26
			QuotaWarningFolderHierarchyDepthDetails = 4044098184U,
			// Token: 0x0400001B RID: 27
			DSNEnhanced_5_6_0_RESOLVER_MT_ModerationReencrptionFailed = 80797861U,
			// Token: 0x0400001C RID: 28
			ShortText5_2_0 = 715499088U,
			// Token: 0x0400001D RID: 29
			QuotaProhibitReceiveFoldersCountDetails = 1234410400U,
			// Token: 0x0400001E RID: 30
			ShortText5_4_2 = 1595973500U,
			// Token: 0x0400001F RID: 31
			ShortText5_7_2 = 1979055051U,
			// Token: 0x04000020 RID: 32
			NotReadSubject = 3244754815U,
			// Token: 0x04000021 RID: 33
			HumanText5_7_0 = 3645242410U,
			// Token: 0x04000022 RID: 34
			DSNEnhanced_5_2_3_RESOLVER_RST_SendSizeLimit_Org = 2798911709U,
			// Token: 0x04000023 RID: 35
			HumanText5_7_10 = 937708661U,
			// Token: 0x04000024 RID: 36
			HumanText5_5_3 = 634034053U,
			// Token: 0x04000025 RID: 37
			ShortText5_7_4 = 816255637U,
			// Token: 0x04000026 RID: 38
			HumanText5_3_3 = 2677307879U,
			// Token: 0x04000027 RID: 39
			HumanText5_1_7 = 2038752517U,
			// Token: 0x04000028 RID: 40
			HumanText5_6_0 = 2058955493U,
			// Token: 0x04000029 RID: 41
			ShortText5_5_2 = 292011585U,
			// Token: 0x0400002A RID: 42
			QuotaProhibitReceiveFolderHierarchyChildrenCountSubject = 3633934039U,
			// Token: 0x0400002B RID: 43
			QuotaWarningSubject = 4064869466U,
			// Token: 0x0400002C RID: 44
			HumanText5_6_5 = 1655670966U,
			// Token: 0x0400002D RID: 45
			ShortText5_1_6 = 3182260417U,
			// Token: 0x0400002E RID: 46
			HumanText5_2_2_StoreNDR = 2766572931U,
			// Token: 0x0400002F RID: 47
			ShortText5_3_0 = 3706504469U,
			// Token: 0x04000030 RID: 48
			ArchiveQuotaFullTopText = 1156273137U,
			// Token: 0x04000031 RID: 49
			DoNotForwardName = 3666692194U,
			// Token: 0x04000032 RID: 50
			ShortText5_6_5 = 3686301493U,
			// Token: 0x04000033 RID: 51
			QuotaWarningMailboxMessagesPerFolderNoLimitSubject = 3685029433U,
			// Token: 0x04000034 RID: 52
			ShortText5_3_4 = 1737136001U,
			// Token: 0x04000035 RID: 53
			HumanText5_6_1 = 3625039434U,
			// Token: 0x04000036 RID: 54
			QuotaProhibitReceiveMailboxMessagesPerFolderCountDetails = 3888161794U,
			// Token: 0x04000037 RID: 55
			ShortText5_4_3 = 3162057441U,
			// Token: 0x04000038 RID: 56
			RejectButtonText = 2844775350U,
			// Token: 0x04000039 RID: 57
			DataCenterHumanText5_1_0 = 1467823001U,
			// Token: 0x0400003A RID: 58
			ArchiveQuotaFullDetails = 448693229U,
			// Token: 0x0400003B RID: 59
			ShortText5_5_0 = 3424179467U,
			// Token: 0x0400003C RID: 60
			HumanText5_7_2 = 513074528U,
			// Token: 0x0400003D RID: 61
			ShortText5_0_0 = 997824090U,
			// Token: 0x0400003E RID: 62
			DataCenterHumanText5_4_1 = 3457394445U,
			// Token: 0x0400003F RID: 63
			RelayedHumanReadableTopText = 2762698027U,
			// Token: 0x04000040 RID: 64
			FailedSubject = 2043697631U,
			// Token: 0x04000041 RID: 65
			DelayedHumanReadableTopText = 2357302625U,
			// Token: 0x04000042 RID: 66
			QuotaMaxSize = 34865187U,
			// Token: 0x04000043 RID: 67
			ShortText5_7_8 = 2785624105U,
			// Token: 0x04000044 RID: 68
			QuotaSendTopText = 4194575540U,
			// Token: 0x04000045 RID: 69
			QuotaSendSubjectPF = 2295532082U,
			// Token: 0x04000046 RID: 70
			HumanText5_1_0 = 2798267404U,
			// Token: 0x04000047 RID: 71
			HumanText5_7_9 = 2435388829U,
			// Token: 0x04000048 RID: 72
			ShortText5_5_1 = 1858095526U,
			// Token: 0x04000049 RID: 73
			QuotaProhibitReceiveMailboxMessagesPerFolderCountSubject = 2240869372U,
			// Token: 0x0400004A RID: 74
			ExpandedHumanReadableTopText = 913497348U,
			// Token: 0x0400004B RID: 75
			HumanText5_4_3 = 2179915018U,
			// Token: 0x0400004C RID: 76
			FailedHumanReadableTopText = 894078784U,
			// Token: 0x0400004D RID: 77
			BodyHeaderFontTag = 3183792054U,
			// Token: 0x0400004E RID: 78
			BodyBlockFontTag = 4029344418U,
			// Token: 0x0400004F RID: 79
			DSNEnhanced_5_4_4_ROUTING_NoNextHop = 1086182386U,
			// Token: 0x04000050 RID: 80
			E4EDisclaimer = 1775266991U,
			// Token: 0x04000051 RID: 81
			ModerationNdrNotificationForSenderTopText = 4171950547U,
			// Token: 0x04000052 RID: 82
			ExOrarMailSenderDescription = 4135226003U,
			// Token: 0x04000053 RID: 83
			QuotaWarningFolderHierarchyChildrenNoLimitDetails = 777497770U,
			// Token: 0x04000054 RID: 84
			DSNEnhanced_5_7_1_RESOLVER_RST_AuthRequired = 1075924926U,
			// Token: 0x04000055 RID: 85
			E4EHeaderCustom = 3187564900U,
			// Token: 0x04000056 RID: 86
			DelayedSubject = 3017333248U,
			// Token: 0x04000057 RID: 87
			ModerationExpiryNoticationForModeratorTopText = 3276713205U,
			// Token: 0x04000058 RID: 88
			ShortText5_5_3 = 3020894940U,
			// Token: 0x04000059 RID: 89
			HumanText5_6_4 = 89587025U,
			// Token: 0x0400005A RID: 90
			E4EViewMessageOTPButton = 2397807267U,
			// Token: 0x0400005B RID: 91
			HumanText5_1_8 = 3154497764U,
			// Token: 0x0400005C RID: 92
			RejectedNotificationSubjectPrefix = 1062071583U,
			// Token: 0x0400005D RID: 93
			QuotaWarningMailboxMessagesPerFolderCountSubject = 3905817172U,
			// Token: 0x0400005E RID: 94
			ShortText5_7_1 = 1575770524U,
			// Token: 0x0400005F RID: 95
			HumanText5_2_0 = 1494305489U,
			// Token: 0x04000060 RID: 96
			HumanText5_0_0 = 1211980487U,
			// Token: 0x04000061 RID: 97
			EnhancedDsnTextFontTag = 3597730735U,
			// Token: 0x04000062 RID: 98
			ShortText5_2_2 = 3847666970U,
			// Token: 0x04000063 RID: 99
			ApprovalRequestPreview = 273945714U,
			// Token: 0x04000064 RID: 100
			HumanText5_4_2 = 613831077U,
			// Token: 0x04000065 RID: 101
			OriginalHeadersTitle = 1416892261U,
			// Token: 0x04000066 RID: 102
			ModeratorsOofSubjectPrefix = 3886882982U,
			// Token: 0x04000067 RID: 103
			ModeratorRejectTopText = 3328018042U,
			// Token: 0x04000068 RID: 104
			FinalTextFontTag = 3837274988U,
			// Token: 0x04000069 RID: 105
			ExOrarMailDisplayName = 4028322787U,
			// Token: 0x0400006A RID: 106
			DSNEnhanced_5_7_1_RESOLVER_RST_DLNeedsSenderRestrictions = 228586384U,
			// Token: 0x0400006B RID: 107
			ModeratorCommentsHeader = 3547364398U,
			// Token: 0x0400006C RID: 108
			ModeratorsNdrSubjectPrefix = 2894800102U,
			// Token: 0x0400006D RID: 109
			ReceivingServerTitle = 2504161731U,
			// Token: 0x0400006E RID: 110
			QuotaSendDetails = 3335723890U,
			// Token: 0x0400006F RID: 111
			HumanText5_1_3 = 69384049U,
			// Token: 0x04000070 RID: 112
			QuarantinedHumanReadableTopText = 1797209243U,
			// Token: 0x04000071 RID: 113
			DeliveredSubject = 2296356702U,
			// Token: 0x04000072 RID: 114
			DSNEnhanced_5_2_3_RESOLVER_RST_RecipSizeLimit_DL = 4273299450U,
			// Token: 0x04000073 RID: 115
			DSNEnhanced_5_7_1_APPROVAL_NotAuthorized = 1203275484U,
			// Token: 0x04000074 RID: 116
			HumanText5_7_5 = 109790001U,
			// Token: 0x04000075 RID: 117
			HumanText5_7_8 = 4001472770U,
			// Token: 0x04000076 RID: 118
			DecisionConflictNotificationSubjectPrefix = 238766803U,
			// Token: 0x04000077 RID: 119
			QuotaWarningNoLimitSubjectPF = 1830882122U,
			// Token: 0x04000078 RID: 120
			BodyDownload = 439284250U,
			// Token: 0x04000079 RID: 121
			QuotaProhibitReceiveFoldersCountSubject = 1979593922U,
			// Token: 0x0400007A RID: 122
			ShortText5_7_100 = 2618404892U,
			// Token: 0x0400007B RID: 123
			QuotaWarningTopText = 2028676810U,
			// Token: 0x0400007C RID: 124
			ShortText5_7_300 = 2900729894U,
			// Token: 0x0400007D RID: 125
			FailedHumanReadableTopTextForTextMessageNotification = 3949783936U,
			// Token: 0x0400007E RID: 126
			DelayHumanReadableExplanation = 2592547661U,
			// Token: 0x0400007F RID: 127
			ShortText5_2_3 = 1118783615U,
			// Token: 0x04000080 RID: 128
			InternetConfidentialDescription = 2820071495U,
			// Token: 0x04000081 RID: 129
			HumanText5_2_2 = 2657104903U,
			// Token: 0x04000082 RID: 130
			ShortText5_4_7 = 836458613U,
			// Token: 0x04000083 RID: 131
			ApprovalRequestTopText = 742696162U,
			// Token: 0x04000084 RID: 132
			HumanText5_4_4 = 4102229319U,
			// Token: 0x04000085 RID: 133
			QuotaWarningFolderHierarchyDepthNoLimitSubject = 1091303688U,
			// Token: 0x04000086 RID: 134
			ModerationOofNotificationForSenderTopText = 325931173U,
			// Token: 0x04000087 RID: 135
			ShortText5_4_8 = 76943726U,
			// Token: 0x04000088 RID: 136
			ExpandedSubject = 3301930267U,
			// Token: 0x04000089 RID: 137
			HumanText5_5_5 = 4122432295U,
			// Token: 0x0400008A RID: 138
			DataCenterHumanText5_1_1 = 3033906942U,
			// Token: 0x0400008B RID: 139
			ArchiveQuotaWarningDetails = 3806593754U,
			// Token: 0x0400008C RID: 140
			E4EViewMessageInfo = 1586140930U,
			// Token: 0x0400008D RID: 141
			DeliveredHumanReadableTopText = 3351672821U,
			// Token: 0x0400008E RID: 142
			RelayedSubject = 304371086U,
			// Token: 0x0400008F RID: 143
			HumanText5_3_4 = 754993578U,
			// Token: 0x04000090 RID: 144
			ShortText5_2_1 = 2281583029U,
			// Token: 0x04000091 RID: 145
			QuotaSendSubject = 4035994692U,
			// Token: 0x04000092 RID: 146
			ShortText5_1_1 = 2422745530U,
			// Token: 0x04000093 RID: 147
			DiagnosticsFontTag = 3158471333U,
			// Token: 0x04000094 RID: 148
			QuotaWarningFoldersCountNoLimitDetails = 833904234U,
			// Token: 0x04000095 RID: 149
			HumanText5_7_4 = 1675873942U,
			// Token: 0x04000096 RID: 150
			QuotaProhibitReceiveFolderHierarchyDepthDetails = 375773780U,
			// Token: 0x04000097 RID: 151
			QuotaWarningFoldersCountDetails = 2271073400U,
			// Token: 0x04000098 RID: 152
			E4EReceivedMessage = 799906996U,
			// Token: 0x04000099 RID: 153
			ExPartnerMailDisplayName = 3093270387U,
			// Token: 0x0400009A RID: 154
			ExAttachmentRemovedDisplayName = 4198466683U,
			// Token: 0x0400009B RID: 155
			ShortText5_3_3 = 977621114U,
			// Token: 0x0400009C RID: 156
			QuotaWarningDetailsPF = 2308350314U,
			// Token: 0x0400009D RID: 157
			ArchiveQuotaWarningNoLimitDetails = 2844551000U,
			// Token: 0x0400009E RID: 158
			GeneratingServerTitle = 4076586603U,
			// Token: 0x0400009F RID: 159
			QuotaSendReceiveSubject = 1431455589U,
			// Token: 0x040000A0 RID: 160
			ShortText5_1_8 = 3632599111U,
			// Token: 0x040000A1 RID: 161
			ShortText5_1_3 = 3585544944U,
			// Token: 0x040000A2 RID: 162
			ExOrarMailRecipientDescription = 159863259U,
			// Token: 0x040000A3 RID: 163
			ArchiveQuotaFullSubject = 1343379807U,
			// Token: 0x040000A4 RID: 164
			QuotaWarningNoLimitDetailsPF = 2333692916U,
			// Token: 0x040000A5 RID: 165
			ShortText5_4_4 = 2402542554U,
			// Token: 0x040000A6 RID: 166
			HumanText_InitMsg = 2088818508U,
			// Token: 0x040000A7 RID: 167
			HumanText5_7_300 = 322888411U,
			// Token: 0x040000A8 RID: 168
			BodyReceiveRMEmail = 4249345458U,
			// Token: 0x040000A9 RID: 169
			QuotaWarningFoldersCountSubject = 1733655566U,
			// Token: 0x040000AA RID: 170
			QuotaWarningDetails = 3171360660U,
			// Token: 0x040000AB RID: 171
			HumanText5_3_0 = 3080592406U,
			// Token: 0x040000AC RID: 172
			HumanText5_7_100 = 605213413U,
			// Token: 0x040000AD RID: 173
			HumanText5_1_6 = 3604836458U,
			// Token: 0x040000AE RID: 174
			ShortText5_7_7 = 2382339578U,
			// Token: 0x040000AF RID: 175
			QuotaWarningFolderHierarchyChildrenNoLimitSubject = 347238620U,
			// Token: 0x040000B0 RID: 176
			ShortText5_7_0 = 3141854465U,
			// Token: 0x040000B1 RID: 177
			HumanText5_5_0 = 3362917408U,
			// Token: 0x040000B2 RID: 178
			QuotaProhibitReceiveFolderHierarchyChildrenCountDetails = 1321639685U,
			// Token: 0x040000B3 RID: 179
			ShortText5_1_0 = 3988829471U,
			// Token: 0x040000B4 RID: 180
			QuotaWarningFoldersCountNoLimitSubject = 2455731400U,
			// Token: 0x040000B5 RID: 181
			HumanText5_4_0 = 1776630491U,
			// Token: 0x040000B6 RID: 182
			ExAttachmentRemovedRecipientDescription = 750807939U,
			// Token: 0x040000B7 RID: 183
			QuotaCurrentSize = 3452592194U,
			// Token: 0x040000B8 RID: 184
			DecisionUpdateTopText = 2915608171U,
			// Token: 0x040000B9 RID: 185
			HumanText5_4_1 = 3342714432U,
			// Token: 0x040000BA RID: 186
			ShortText5_4_6 = 3565341968U,
			// Token: 0x040000BB RID: 187
			ShortText5_7_6 = 3948423519U,
			// Token: 0x040000BC RID: 188
			ApprovalRequestSubjectPrefix = 1242161506U,
			// Token: 0x040000BD RID: 189
			E4ESignIn = 4133660744U,
			// Token: 0x040000BE RID: 190
			InternetConfidentialName = 831975206U,
			// Token: 0x040000BF RID: 191
			HumanText5_7_7 = 1272589415U,
			// Token: 0x040000C0 RID: 192
			ShortText5_7_3 = 412971110U,
			// Token: 0x040000C1 RID: 193
			QuotaWarningNoLimitDetails = 452614234U,
			// Token: 0x040000C2 RID: 194
			E4EEncryptedMessage = 1230109185U,
			// Token: 0x040000C3 RID: 195
			HumanText5_5_1 = 1796833467U,
			// Token: 0x040000C4 RID: 196
			DataCenterHumanText5_7_1 = 3316231944U,
			// Token: 0x040000C5 RID: 197
			ShortText5_5_6 = 2261380053U,
			// Token: 0x040000C6 RID: 198
			QuotaWarningFolderHierarchyChildrenCountDetails = 1525463967U,
			// Token: 0x040000C7 RID: 199
			ShortText5_7_9 = 1219540164U,
			// Token: 0x040000C8 RID: 200
			ShortText5_1_2 = 856661589U,
			// Token: 0x040000C9 RID: 201
			QuotaWarningMailboxMessagesPerFolderCountDetails = 859070570U,
			// Token: 0x040000CA RID: 202
			HumanText5_7_3 = 3241957883U,
			// Token: 0x040000CB RID: 203
			DoNotForwardDescription = 1793216387U,
			// Token: 0x040000CC RID: 204
			ModerationExpiryNoticationForSenderTopText = 4019621683U,
			// Token: 0x040000CD RID: 205
			HumanText5_4_7 = 210546550U,
			// Token: 0x040000CE RID: 206
			DataCenterHumanText4_4_7 = 1569096212U,
			// Token: 0x040000CF RID: 207
			HumanText5_6_3 = 492871552U,
			// Token: 0x040000D0 RID: 208
			HumanText5_3_2 = 4243391820U,
			// Token: 0x040000D1 RID: 209
			ApprovalCommentAttachmentFilename = 3124761826U,
			// Token: 0x040000D2 RID: 210
			ArchiveQuotaWarningNoLimitSubject = 2375073438U,
			// Token: 0x040000D3 RID: 211
			HumanText5_1_4 = 472668576U,
			// Token: 0x040000D4 RID: 212
			E4EHosted = 2988353893U,
			// Token: 0x040000D5 RID: 213
			QuotaSendReceiveDetails = 3224920035U,
			// Token: 0x040000D6 RID: 214
			ShortText5_2_4 = 3041097916U,
			// Token: 0x040000D7 RID: 215
			DSNEnhanced_5_7_1_RESOLVER_RST_NotAuthorized = 843859989U,
			// Token: 0x040000D8 RID: 216
			ShortText5_5_4 = 1098580639U,
			// Token: 0x040000D9 RID: 217
			QuotaWarningMailboxMessagesPerFolderNoLimitDetails = 86736107U,
			// Token: 0x040000DA RID: 218
			ShortText5_6_4 = 2120217552U,
			// Token: 0x040000DB RID: 219
			ShortText5_1_4 = 2019461003U,
			// Token: 0x040000DC RID: 220
			HumanText5_1_2 = 1635467990U,
			// Token: 0x040000DD RID: 221
			QuotaWarningFolderHierarchyChildrenCountSubject = 3715345541U,
			// Token: 0x040000DE RID: 222
			HumanText5_5_2 = 2200117994U,
			// Token: 0x040000DF RID: 223
			ShortText5_4_0 = 433174086U,
			// Token: 0x040000E0 RID: 224
			HumanText5_7_1 = 2079158469U,
			// Token: 0x040000E1 RID: 225
			HumanText5_2_4 = 3463673957U,
			// Token: 0x040000E2 RID: 226
			DSNEnhanced_5_2_3_RESOLVER_RST_RecipSizeLimit = 357259713U,
			// Token: 0x040000E3 RID: 227
			E4EWaitMessage = 20682666U,
			// Token: 0x040000E4 RID: 228
			ApprovalRequestExpiryTopText = 1418666303U,
			// Token: 0x040000E5 RID: 229
			E4EViewMessageButton = 1835138854U,
			// Token: 0x040000E6 RID: 230
			ShortText5_6_3 = 554133611U,
			// Token: 0x040000E7 RID: 231
			HumanText5_1_1 = 1232183463U,
			// Token: 0x040000E8 RID: 232
			Moderation_Reencryption_Exception = 2489798467U,
			// Token: 0x040000E9 RID: 233
			ShortText5_6_0 = 150849084U,
			// Token: 0x040000EA RID: 234
			ShortText5_6_1 = 1716933025U,
			// Token: 0x040000EB RID: 235
			ApprovalRequestExpiredNotificationSubjectPrefix = 2028225464U,
			// Token: 0x040000EC RID: 236
			HumanText5_5_4 = 1393548940U,
			// Token: 0x040000ED RID: 237
			ShortText5_4_1 = 1999258027U,
			// Token: 0x040000EE RID: 238
			QuotaWarningNoLimitSubject = 2807992444U,
			// Token: 0x040000EF RID: 239
			DecisionConflictTopText = 724420710U,
			// Token: 0x040000F0 RID: 240
			FailedHumanReadableTopTextForTextMessage = 2707856071U,
			// Token: 0x040000F1 RID: 241
			HumanText5_6_2 = 3221754907U,
			// Token: 0x040000F2 RID: 242
			QuotaSendDetailsPF = 96782060U,
			// Token: 0x040000F3 RID: 243
			HumanText5_7_6 = 2838673356U,
			// Token: 0x040000F4 RID: 244
			QuotaSendReceiveTopText = 2912990139U,
			// Token: 0x040000F5 RID: 245
			HumanText5_2_3 = 4223188844U,
			// Token: 0x040000F6 RID: 246
			ApproveButtonText = 3163998196U,
			// Token: 0x040000F7 RID: 247
			DiagnosticInformationTitle = 3827208783U,
			// Token: 0x040000F8 RID: 248
			HumanText5_4_6 = 2939429905U,
			// Token: 0x040000F9 RID: 249
			HumanText5_5_6 = 230749526U,
			// Token: 0x040000FA RID: 250
			ShortText5_5_5 = 3827463994U,
			// Token: 0x040000FB RID: 251
			ShortText5_7_5 = 3545138992U,
			// Token: 0x040000FC RID: 252
			DSNEnhanced_5_7_1_TRANSPORT_RULES_RejectMessage = 2279047500U,
			// Token: 0x040000FD RID: 253
			HumanText5_3_1 = 1514508465U,
			// Token: 0x040000FE RID: 254
			ShortText5_3_1 = 2140420528U,
			// Token: 0x040000FF RID: 255
			ShortText5_3_2 = 2543705055U,
			// Token: 0x04000100 RID: 256
			ShortText5_7_10 = 2107776300U
		}

		// Token: 0x02000004 RID: 4
		private enum ParamIDs
		{
			// Token: 0x04000102 RID: 258
			HumanReadableBoldedSubjectLine,
			// Token: 0x04000103 RID: 259
			DsnParamTextMessageSizePerRecipientInMB,
			// Token: 0x04000104 RID: 260
			QuotaProhibitReceiveFoldersCountTopText,
			// Token: 0x04000105 RID: 261
			QuotaWarningFolderHierarchyDepthTopText,
			// Token: 0x04000106 RID: 262
			QuotaProhibitReceiveFolderHierarchyDepthTopText,
			// Token: 0x04000107 RID: 263
			ModeratedDLApprovalRequestRecipientList,
			// Token: 0x04000108 RID: 264
			QuotaWarningFoldersCountNoLimitTopText,
			// Token: 0x04000109 RID: 265
			QuotaProhibitReceiveMailboxMessagesPerFolderCountTopText,
			// Token: 0x0400010A RID: 266
			DsnParamTextRecipientCount,
			// Token: 0x0400010B RID: 267
			HumanTextFailedSmtpToSmsGatewayNotification,
			// Token: 0x0400010C RID: 268
			QuotaWarningFolderHierarchyDepthNoLimitTopText,
			// Token: 0x0400010D RID: 269
			ArchiveQuotaWarningNoLimitTopText,
			// Token: 0x0400010E RID: 270
			HumanReadableBoldedToLine,
			// Token: 0x0400010F RID: 271
			DelayedHumanReadableBottomTextHours,
			// Token: 0x04000110 RID: 272
			E4EOpenAttachment,
			// Token: 0x04000111 RID: 273
			QuotaWarningFolderHierarchyChildrenNoLimitTopText,
			// Token: 0x04000112 RID: 274
			ModeratedDLApprovalRequest,
			// Token: 0x04000113 RID: 275
			QuotaProhibitReceiveFolderHierarchyChildrenCountTopText,
			// Token: 0x04000114 RID: 276
			ExternalFailedHumanReadableErrorText,
			// Token: 0x04000115 RID: 277
			DecisionConflictWithDetailsNotification,
			// Token: 0x04000116 RID: 278
			DsnParamTextMessageSizePerMessageInMB,
			// Token: 0x04000117 RID: 279
			DecisionConflictNotification,
			// Token: 0x04000118 RID: 280
			HumanReadableBoldedCcLine,
			// Token: 0x04000119 RID: 281
			ModeratorExpiryNotification,
			// Token: 0x0400011A RID: 282
			ApprovalRequestExpiryNotification,
			// Token: 0x0400011B RID: 283
			ExternalFailedHumanReadableTopText,
			// Token: 0x0400011C RID: 284
			QuotaWarningMailboxMessagesPerFolderCountTopText,
			// Token: 0x0400011D RID: 285
			ExternalFailedHumanReadableErrorNoDetailText,
			// Token: 0x0400011E RID: 286
			QuotaWarningFoldersCountTopText,
			// Token: 0x0400011F RID: 287
			SizeInFolders,
			// Token: 0x04000120 RID: 288
			HumanReadableSubjectLine,
			// Token: 0x04000121 RID: 289
			DelayedHumanReadableBottomText,
			// Token: 0x04000122 RID: 290
			DsnParamTextMessageSizePerMessageInKB,
			// Token: 0x04000123 RID: 291
			DsnParamTextMessageSizePerRecipientInKB,
			// Token: 0x04000124 RID: 292
			QuotaWarningTopTextPF,
			// Token: 0x04000125 RID: 293
			QuotaSendTopTextPF,
			// Token: 0x04000126 RID: 294
			QuotaWarningNoLimitTopTextPF,
			// Token: 0x04000127 RID: 295
			QuotaWarningNoLimitTopText,
			// Token: 0x04000128 RID: 296
			QuotaWarningMailboxMessagesPerFolderNoLimitTopText,
			// Token: 0x04000129 RID: 297
			HumanTextFailedPasscodeWithReason,
			// Token: 0x0400012A RID: 298
			SizeInMessages,
			// Token: 0x0400012B RID: 299
			HumanReadableBoldedFromLine,
			// Token: 0x0400012C RID: 300
			HumanTextFailedPasscodeWithoutReason,
			// Token: 0x0400012D RID: 301
			QuotaWarningFolderHierarchyChildrenCountTopText,
			// Token: 0x0400012E RID: 302
			HumanTextFailedOmsNotification,
			// Token: 0x0400012F RID: 303
			HumanReadableRemoteServerText,
			// Token: 0x04000130 RID: 304
			SizeInMB
		}
	}
}
