using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000005 RID: 5
	public static class CoreStrings
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00005C14 File Offset: 0x00003E14
		static CoreStrings()
		{
			CoreStrings.stringIDs.Add(2092707270U, "RoleTypeDescription_RemoteAndAcceptedDomains");
			CoreStrings.stringIDs.Add(1886101956U, "TimeZoneSouthAfricaStandardTime");
			CoreStrings.stringIDs.Add(3906704818U, "PolicyTipNotifyOnly");
			CoreStrings.stringIDs.Add(97673562U, "TimeZoneLibyaStandardTime");
			CoreStrings.stringIDs.Add(1829747073U, "ReportSeverityLow");
			CoreStrings.stringIDs.Add(2344870457U, "RoleTypeDescription_ArchiveApplication");
			CoreStrings.stringIDs.Add(3472506566U, "EventDelivered");
			CoreStrings.stringIDs.Add(3720562738U, "AntispamIPBlockListSettingMigrated");
			CoreStrings.stringIDs.Add(327230581U, "RoleTypeDescription_ActiveMonitoring");
			CoreStrings.stringIDs.Add(2962389050U, "RoleTypeDescription_UMPrompts");
			CoreStrings.stringIDs.Add(146865784U, "TimeZoneCentralAsiaStandardTime");
			CoreStrings.stringIDs.Add(146778149U, "TimeZoneESouthAmericaStandardTime");
			CoreStrings.stringIDs.Add(80585286U, "TimeZoneMountainStandardTime");
			CoreStrings.stringIDs.Add(1446960263U, "TimeZoneKaliningradStandardTime");
			CoreStrings.stringIDs.Add(137362532U, "ExchangeViewOnlyManagementForestOperatorDescription");
			CoreStrings.stringIDs.Add(3151979146U, "AntispamMigrationSucceeded");
			CoreStrings.stringIDs.Add(1683344478U, "TimeZoneNorthAsiaStandardTime");
			CoreStrings.stringIDs.Add(89789448U, "InvalidSender");
			CoreStrings.stringIDs.Add(1221420638U, "RoleTypeDescription_MyTeamMailboxes");
			CoreStrings.stringIDs.Add(2027768067U, "RoleTypeDescription_MyDistributionGroups");
			CoreStrings.stringIDs.Add(1738495689U, "TimeZoneFijiStandardTime");
			CoreStrings.stringIDs.Add(1010967094U, "TimeZoneEAfricaStandardTime");
			CoreStrings.stringIDs.Add(498311706U, "NotAuthorizedToViewRecipientPath");
			CoreStrings.stringIDs.Add(2847306432U, "RoleTypeDescription_UserOptions");
			CoreStrings.stringIDs.Add(3322063993U, "RoleTypeDescription_UmRecipientManagement");
			CoreStrings.stringIDs.Add(1029644363U, "ReportSeverityHigh");
			CoreStrings.stringIDs.Add(2763281369U, "RoleTypeDescription_CmdletExtensionAgents");
			CoreStrings.stringIDs.Add(2874223383U, "AntimalwareUserOptOut");
			CoreStrings.stringIDs.Add(2736802282U, "RoleTypeDescription_MyLogon");
			CoreStrings.stringIDs.Add(2385442291U, "ContentIndexStatusDisabled");
			CoreStrings.stringIDs.Add(466036006U, "PolicyTipRejectOverride");
			CoreStrings.stringIDs.Add(1875295180U, "StatusDelivered");
			CoreStrings.stringIDs.Add(1255891408U, "TimeZoneWMongoliaStandardTime");
			CoreStrings.stringIDs.Add(872336955U, "RoleTypeDescription_TransportHygiene");
			CoreStrings.stringIDs.Add(112955817U, "TraceLevelHigh");
			CoreStrings.stringIDs.Add(4100231581U, "RoleTypeDescription_InformationRightsManagement");
			CoreStrings.stringIDs.Add(1461929024U, "TestModifySubject");
			CoreStrings.stringIDs.Add(2652353769U, "TimeZoneLordHoweStandardTime");
			CoreStrings.stringIDs.Add(1319959286U, "InvalidSenderForAdmin");
			CoreStrings.stringIDs.Add(1633693746U, "RoleTypeDescription_DiscoveryManagement");
			CoreStrings.stringIDs.Add(3964643789U, "TimeZoneTurksAndCaicosStandardTime");
			CoreStrings.stringIDs.Add(2230053377U, "RoleTypeDescription_MailboxExport");
			CoreStrings.stringIDs.Add(3106057138U, "AntispamIPAllowListSettingMigrated");
			CoreStrings.stringIDs.Add(4036159751U, "ExchangeManagementForestOperatorDescription");
			CoreStrings.stringIDs.Add(2575879303U, "TimeZoneArabianStandardTime");
			CoreStrings.stringIDs.Add(2009673553U, "TimeZoneIsraelStandardTime");
			CoreStrings.stringIDs.Add(4146612654U, "RoleTypeDescription_ActiveDirectoryPermissions");
			CoreStrings.stringIDs.Add(3378761982U, "RunOnce");
			CoreStrings.stringIDs.Add(4217676503U, "MessageDirectionAll");
			CoreStrings.stringIDs.Add(2281308004U, "RoleTypeDescription_UmManagement");
			CoreStrings.stringIDs.Add(2385887948U, "AuditSeverityLevelHigh");
			CoreStrings.stringIDs.Add(1680752826U, "TimeZoneArgentinaStandardTime");
			CoreStrings.stringIDs.Add(2184195641U, "RoleTypeDescription_Migration");
			CoreStrings.stringIDs.Add(1406395995U, "TimeZoneUlaanbaatarStandardTime");
			CoreStrings.stringIDs.Add(1799519309U, "TimeZoneSaratovStandardTime");
			CoreStrings.stringIDs.Add(1545851488U, "TimeZoneVenezuelaStandardTime");
			CoreStrings.stringIDs.Add(1103744372U, "ExchangeAllMailboxesDescription");
			CoreStrings.stringIDs.Add(926819582U, "RoleTypeDescription_GALSynchronizationManagement");
			CoreStrings.stringIDs.Add(3690262447U, "RoleTypeDescription_ViewOnlyRoleManagement");
			CoreStrings.stringIDs.Add(683655822U, "RoleTypeDescription_LiveID");
			CoreStrings.stringIDs.Add(3406878936U, "RoleTypeDescription_TransportAgents");
			CoreStrings.stringIDs.Add(2562309877U, "RoleTypeDescription_MyDiagnostics");
			CoreStrings.stringIDs.Add(411395545U, "TrackingWarningNoResultsDueToUntrackableMessagePath");
			CoreStrings.stringIDs.Add(4221322756U, "EventTransferredToLegacyExchangeServer");
			CoreStrings.stringIDs.Add(1998960776U, "RoleTypeDescription_TeamMailboxes");
			CoreStrings.stringIDs.Add(1130640660U, "TimeZoneSAPacificStandardTime");
			CoreStrings.stringIDs.Add(1318716879U, "ExchangeServerManagementDescription");
			CoreStrings.stringIDs.Add(74443346U, "UnsupportedSenderForTracking");
			CoreStrings.stringIDs.Add(2164669709U, "TimeZoneMontevideoStandardTime");
			CoreStrings.stringIDs.Add(2912446405U, "TimeZoneUSEasternStandardTime");
			CoreStrings.stringIDs.Add(517696069U, "EventApprovedModerationIW");
			CoreStrings.stringIDs.Add(103080662U, "TimeZoneCentralAmericaStandardTime");
			CoreStrings.stringIDs.Add(393808047U, "ContentIndexStatusAutoSuspended");
			CoreStrings.stringIDs.Add(2501174206U, "TimeZoneBahiaStandardTime");
			CoreStrings.stringIDs.Add(2005680638U, "RoleTypeDescription_EdgeSubscriptions");
			CoreStrings.stringIDs.Add(3905123624U, "TimeZoneCubaStandardTime");
			CoreStrings.stringIDs.Add(3492975378U, "TimeZoneEasterIslandStandardTime");
			CoreStrings.stringIDs.Add(3708464597U, "JobStatusInProgress");
			CoreStrings.stringIDs.Add(2294597162U, "TimeZoneUTC09");
			CoreStrings.stringIDs.Add(2993499200U, "TimeZoneEgyptStandardTime");
			CoreStrings.stringIDs.Add(3231667300U, "StatusRead");
			CoreStrings.stringIDs.Add(2740570033U, "TimeZoneCaucasusStandardTime");
			CoreStrings.stringIDs.Add(4120033037U, "RoleTypeDescription_MailEnabledPublicFolders");
			CoreStrings.stringIDs.Add(4113945646U, "RoleTypeDescription_ViewOnlyAuditLogs");
			CoreStrings.stringIDs.Add(576007077U, "TrackingWarningReadStatusUnavailable");
			CoreStrings.stringIDs.Add(176786758U, "ViewOnlyPIIGroupDescription");
			CoreStrings.stringIDs.Add(2073899161U, "RoleTypeDescription_FederatedSharing");
			CoreStrings.stringIDs.Add(3390444882U, "RoleTypeDescription_RoleManagement");
			CoreStrings.stringIDs.Add(2903872252U, "TrackingExplanationNormalTimeSpan");
			CoreStrings.stringIDs.Add(2937865572U, "RoleTypeDescription_ExchangeServers");
			CoreStrings.stringIDs.Add(641655538U, "TimeZoneAstrakhanStandardTime");
			CoreStrings.stringIDs.Add(840197340U, "ReportSeverityMedium");
			CoreStrings.stringIDs.Add(424676612U, "RoleTypeDescription_SupportDiagnostics");
			CoreStrings.stringIDs.Add(2220779553U, "TimeZoneCentralEuropeanStandardTime");
			CoreStrings.stringIDs.Add(1476061287U, "TimeZoneKoreaStandardTime");
			CoreStrings.stringIDs.Add(1361373610U, "ContentIndexStatusSeeding");
			CoreStrings.stringIDs.Add(3998935900U, "TimeZoneWAustraliaStandardTime");
			CoreStrings.stringIDs.Add(3209229526U, "RoleTypeDescription_Custom");
			CoreStrings.stringIDs.Add(390519148U, "AntispamActionTypeSettingMigrated");
			CoreStrings.stringIDs.Add(559475236U, "TrackingExplanationExcessiveTimeSpan");
			CoreStrings.stringIDs.Add(2416995053U, "QuarantineSpam");
			CoreStrings.stringIDs.Add(728513229U, "TimeZoneUTC11");
			CoreStrings.stringIDs.Add(1698995718U, "RoleTypeDescription_PublicFolderReplication");
			CoreStrings.stringIDs.Add(3202170171U, "Encrypt");
			CoreStrings.stringIDs.Add(1562661243U, "TimeZoneRussiaTimeZone11");
			CoreStrings.stringIDs.Add(875871474U, "TimeZoneEkaterinburgStandardTime");
			CoreStrings.stringIDs.Add(3860979609U, "RoleTypeDescription_LegalHold");
			CoreStrings.stringIDs.Add(50740234U, "TimeZoneTocantinsStandardTime");
			CoreStrings.stringIDs.Add(2428028299U, "TimeZoneArabicStandardTime");
			CoreStrings.stringIDs.Add(1011790132U, "RoleTypeDescription_MailboxImportExport");
			CoreStrings.stringIDs.Add(4065087132U, "RoleTypeDescription_Supervision");
			CoreStrings.stringIDs.Add(937823105U, "RoleTypeDescription_LawEnforcementRequests");
			CoreStrings.stringIDs.Add(851475441U, "RoleTypeDescription_MailboxSearchApplication");
			CoreStrings.stringIDs.Add(20620266U, "RoleTypeDescription_RetentionManagement");
			CoreStrings.stringIDs.Add(937651576U, "TimeZoneWestAsiaStandardTime");
			CoreStrings.stringIDs.Add(1727765283U, "QuarantineTransportRule");
			CoreStrings.stringIDs.Add(3421391191U, "RoleTypeDescription_ViewOnlyCentralAdminManagement");
			CoreStrings.stringIDs.Add(2151396155U, "EventFailedTransportRulesIW");
			CoreStrings.stringIDs.Add(199727676U, "EventDelayedAfterTransferToPartnerOrgIW");
			CoreStrings.stringIDs.Add(1348529911U, "RoleTypeDescription_DatacenterOperationsDCOnly");
			CoreStrings.stringIDs.Add(4089915379U, "TimeZoneTomskStandardTime");
			CoreStrings.stringIDs.Add(3099813970U, "TrackingBusy");
			CoreStrings.stringIDs.Add(1728005806U, "TimeZoneTongaStandardTime");
			CoreStrings.stringIDs.Add(3917786073U, "TimeZoneTasmaniaStandardTime");
			CoreStrings.stringIDs.Add(2471410929U, "ExchangePublicFolderAdminDescription");
			CoreStrings.stringIDs.Add(1018209439U, "TrafficScopeOutbound");
			CoreStrings.stringIDs.Add(2309256410U, "EventForwardedToDelegateAndDeleted");
			CoreStrings.stringIDs.Add(3851963997U, "RoleTypeDescription_GALSynchronization");
			CoreStrings.stringIDs.Add(1747500934U, "CompressionOutOfMemory");
			CoreStrings.stringIDs.Add(1069292955U, "TrafficScopeDisabled");
			CoreStrings.stringIDs.Add(1969466653U, "RoleTypeDescription_OrganizationManagement");
			CoreStrings.stringIDs.Add(1654105079U, "TimeZoneOmskStandardTime");
			CoreStrings.stringIDs.Add(1984558815U, "TimeZoneBelarusStandardTime");
			CoreStrings.stringIDs.Add(3386037373U, "TimeZoneParaguayStandardTime");
			CoreStrings.stringIDs.Add(1198640795U, "RoleTypeDescription_Reporting");
			CoreStrings.stringIDs.Add(3103556173U, "TimeZoneChathamIslandsStandardTime");
			CoreStrings.stringIDs.Add(1660783915U, "RoleTypeDescription_MyMailboxDelegation");
			CoreStrings.stringIDs.Add(397099290U, "RoleTypeDescription_ExchangeVirtualDirectories");
			CoreStrings.stringIDs.Add(3213863256U, "TimeZoneAUSEasternStandardTime");
			CoreStrings.stringIDs.Add(3256513739U, "EventNotRead");
			CoreStrings.stringIDs.Add(463534759U, "TimeZoneMiddleEastStandardTime");
			CoreStrings.stringIDs.Add(907728851U, "RoleTypeDescription_ApplicationImpersonation");
			CoreStrings.stringIDs.Add(2979872069U, "TrackingWarningTooManyEvents");
			CoreStrings.stringIDs.Add(1631091055U, "ContentIndexStatusUnknown");
			CoreStrings.stringIDs.Add(1826684897U, "TimeZoneSyriaStandardTime");
			CoreStrings.stringIDs.Add(3804745356U, "TimeZoneMauritiusStandardTime");
			CoreStrings.stringIDs.Add(667589187U, "TrackingMessageTypeNotSupported");
			CoreStrings.stringIDs.Add(3096621845U, "TimeZoneCentralPacificStandardTime");
			CoreStrings.stringIDs.Add(1156915939U, "RoleTypeDescription_MailboxSearch");
			CoreStrings.stringIDs.Add(1027560048U, "StdUnknownTimeZone");
			CoreStrings.stringIDs.Add(3485911895U, "StatusUnsuccessFul");
			CoreStrings.stringIDs.Add(1187302658U, "RoleTypeDescription_MyLinkedInEnabled");
			CoreStrings.stringIDs.Add(4008183169U, "TestXHeader");
			CoreStrings.stringIDs.Add(1479842702U, "RoleTypeDescription_ReceiveConnectors");
			CoreStrings.stringIDs.Add(1338433320U, "TimeZoneRussiaTimeZone3");
			CoreStrings.stringIDs.Add(2262407331U, "TimeZoneTransbaikalStandardTime");
			CoreStrings.stringIDs.Add(2629692075U, "RoleTypeDescription_Databases");
			CoreStrings.stringIDs.Add(14056478U, "StatusTransferred");
			CoreStrings.stringIDs.Add(2237907931U, "TimeZoneGeorgianStandardTime");
			CoreStrings.stringIDs.Add(3620221593U, "Decrypt");
			CoreStrings.stringIDs.Add(3879259618U, "RoleTypeDescription_MyReadWriteMailboxApps");
			CoreStrings.stringIDs.Add(3742415118U, "TimeZoneBougainvilleStandardTime");
			CoreStrings.stringIDs.Add(99561377U, "RoleTypeDescription_EdgeSync");
			CoreStrings.stringIDs.Add(2333794793U, "TimeZoneTurkeyStandardTime");
			CoreStrings.stringIDs.Add(3305616866U, "RoleTypeDescription_MyMailSubscriptions");
			CoreStrings.stringIDs.Add(1393844023U, "PartialMessages");
			CoreStrings.stringIDs.Add(3351363629U, "InboundIpMigrationCompleted");
			CoreStrings.stringIDs.Add(626810642U, "RoleTypeDescription_MyMarketplaceApps");
			CoreStrings.stringIDs.Add(448910837U, "TimeZoneJordanStandardTime");
			CoreStrings.stringIDs.Add(1664785551U, "EventPendingModerationHelpDesk");
			CoreStrings.stringIDs.Add(1334000728U, "DeliveryStatusDelivered");
			CoreStrings.stringIDs.Add(135488574U, "TimeZoneEEuropeStandardTime");
			CoreStrings.stringIDs.Add(3847090244U, "RoleTypeDescription_MyVoiceMail");
			CoreStrings.stringIDs.Add(3097579504U, "TimeZoneMyanmarStandardTime");
			CoreStrings.stringIDs.Add(1321416518U, "TrackingExplanationLogsDeleted");
			CoreStrings.stringIDs.Add(2198659572U, "ExchangeOrgAdminDescription");
			CoreStrings.stringIDs.Add(2229119179U, "TimeZoneNepalStandardTime");
			CoreStrings.stringIDs.Add(203352201U, "TimeZoneCenAustraliaStandardTime");
			CoreStrings.stringIDs.Add(2350709050U, "JobStatusFailed");
			CoreStrings.stringIDs.Add(2909789462U, "RoleTypeDescription_TransportQueues");
			CoreStrings.stringIDs.Add(2239001839U, "TimeZoneWestPacificStandardTime");
			CoreStrings.stringIDs.Add(21646529U, "TrackingWarningNoResultsDueToTrackingTooEarly");
			CoreStrings.stringIDs.Add(4161689178U, "RoleTypeDescription_ViewOnlyOrganizationManagement");
			CoreStrings.stringIDs.Add(2206122650U, "RoleTypeDescription_ViewOnlyRecipients");
			CoreStrings.stringIDs.Add(1776488541U, "Allow");
			CoreStrings.stringIDs.Add(430852938U, "DomainScopeAlLDomains");
			CoreStrings.stringIDs.Add(1082730632U, "NoValidDomainNameExistsInDomainSettings");
			CoreStrings.stringIDs.Add(377106190U, "ExchangeRecordsManagementDescription");
			CoreStrings.stringIDs.Add(2294597161U, "TimeZoneUTC08");
			CoreStrings.stringIDs.Add(1319136228U, "RoleTypeDescription_NetworkingManagement");
			CoreStrings.stringIDs.Add(3173642551U, "EventTransferredToForeignOrgHelpDesk");
			CoreStrings.stringIDs.Add(3245137676U, "RoleTypeDescription_MyFacebookEnabled");
			CoreStrings.stringIDs.Add(3487147524U, "TimeZoneCentralEuropeStandardTime");
			CoreStrings.stringIDs.Add(254529528U, "RoleTypeDescription_SecurityGroupCreationAndMembership");
			CoreStrings.stringIDs.Add(1316318251U, "DeliveryStatusExpanded");
			CoreStrings.stringIDs.Add(1981651471U, "StatusPending");
			CoreStrings.stringIDs.Add(3359056161U, "TimeZoneMidAtlanticStandardTime");
			CoreStrings.stringIDs.Add(970642999U, "RoleTypeDescription_ResetPassword");
			CoreStrings.stringIDs.Add(1907760708U, "ExchangeUMManagementDescription");
			CoreStrings.stringIDs.Add(1392628209U, "TimeZoneUTC");
			CoreStrings.stringIDs.Add(547528310U, "RoleTypeDescription_DataLossPrevention");
			CoreStrings.stringIDs.Add(429272165U, "EventPendingModerationIW");
			CoreStrings.stringIDs.Add(1939880180U, "RoleTypeDescription_MyCustomApps");
			CoreStrings.stringIDs.Add(2722115341U, "RoleTypeDescription_DatabaseAvailabilityGroups");
			CoreStrings.stringIDs.Add(2031056185U, "ExchangeHelpDeskDescription");
			CoreStrings.stringIDs.Add(2388819289U, "MessageDirectionReceived");
			CoreStrings.stringIDs.Add(1562661838U, "SpamQuarantineMigrationSucceeded");
			CoreStrings.stringIDs.Add(2708860089U, "EventFailedModerationIW");
			CoreStrings.stringIDs.Add(2294597167U, "TimeZoneUTC02");
			CoreStrings.stringIDs.Add(4202622721U, "AntimalwareScopingConstraint");
			CoreStrings.stringIDs.Add(2928288207U, "DeliveryStatusFailed");
			CoreStrings.stringIDs.Add(3278150655U, "QuarantineInbound");
			CoreStrings.stringIDs.Add(872985302U, "RoleTypeDescription_Throttling");
			CoreStrings.stringIDs.Add(2019318218U, "RoleTypeDescription_DataCenterDestructiveOperations");
			CoreStrings.stringIDs.Add(2945933912U, "RoleTypeDescription_AddressLists");
			CoreStrings.stringIDs.Add(1959014922U, "RoleTypeDescription_CentralAdminManagement");
			CoreStrings.stringIDs.Add(694118059U, "TimeZoneAfghanistanStandardTime");
			CoreStrings.stringIDs.Add(2433016809U, "EventTransferredToForeignOrgIW");
			CoreStrings.stringIDs.Add(1457011382U, "JobStatusCancelled");
			CoreStrings.stringIDs.Add(2033924825U, "TimeZoneAtlanticStandardTime");
			CoreStrings.stringIDs.Add(1901284917U, "TimeZoneArabStandardTime");
			CoreStrings.stringIDs.Add(238886746U, "RoleTypeDescription_MailRecipients");
			CoreStrings.stringIDs.Add(2886777393U, "RoleTypeDescription_WorkloadManagement");
			CoreStrings.stringIDs.Add(1286295614U, "TimeZoneAlaskanStandardTime");
			CoreStrings.stringIDs.Add(1235839409U, "MsoManagedTenantHelpdeskGroupDescription");
			CoreStrings.stringIDs.Add(1838296451U, "AntimalwareInboundRecipientNotifications");
			CoreStrings.stringIDs.Add(270033310U, "TestXHeaderAndModifySubject");
			CoreStrings.stringIDs.Add(1923042104U, "ContentIndexStatusFailedAndSuspended");
			CoreStrings.stringIDs.Add(2104897796U, "MsoManagedTenantAdminGroupDescription");
			CoreStrings.stringIDs.Add(1558282382U, "RoleTypeDescription_DataCenterOperations");
			CoreStrings.stringIDs.Add(3700079135U, "EventModerationExpired");
			CoreStrings.stringIDs.Add(29170872U, "TraceLevelMedium");
			CoreStrings.stringIDs.Add(432229092U, "RoleTypeDescription_MoveMailboxes");
			CoreStrings.stringIDs.Add(1038994972U, "RoleTypeDescription_MailRecipientCreation");
			CoreStrings.stringIDs.Add(3021954325U, "TimeZoneLineIslandsStandardTime");
			CoreStrings.stringIDs.Add(2008936115U, "MissingIdentityParameter");
			CoreStrings.stringIDs.Add(3268869348U, "ContentIndexStatusHealthyAndUpgrading");
			CoreStrings.stringIDs.Add(1450163010U, "TrafficScopeInbound");
			CoreStrings.stringIDs.Add(880515475U, "RoleTypeDescription_UnScopedRoleManagement");
			CoreStrings.stringIDs.Add(267867511U, "AuditSeverityLevelDoNotAudit");
			CoreStrings.stringIDs.Add(1778108211U, "ExchangeHygieneManagementDescription");
			CoreStrings.stringIDs.Add(1246550425U, "ClassIdExtensions");
			CoreStrings.stringIDs.Add(4236830390U, "TimeZoneNamibiaStandardTime");
			CoreStrings.stringIDs.Add(2735513322U, "RejectedExplanationContentFiltering");
			CoreStrings.stringIDs.Add(1179870130U, "QuarantineOutbound");
			CoreStrings.stringIDs.Add(2454182516U, "RunWeekly");
			CoreStrings.stringIDs.Add(1623584678U, "TimeZoneAltaiStandardTime");
			CoreStrings.stringIDs.Add(889690570U, "EventSubmittedCrossSite");
			CoreStrings.stringIDs.Add(565087651U, "TimeZoneEasternStandardTime");
			CoreStrings.stringIDs.Add(1000900012U, "TimeZoneAzerbaijanStandardTime");
			CoreStrings.stringIDs.Add(2216352038U, "TimeZonePakistanStandardTime");
			CoreStrings.stringIDs.Add(790166856U, "RoleTypeDescription_OrgMarketplaceApps");
			CoreStrings.stringIDs.Add(3066204968U, "TimeZonePacificSAStandardTime");
			CoreStrings.stringIDs.Add(2981542010U, "TimeZoneRussianStandardTime");
			CoreStrings.stringIDs.Add(2623630612U, "RoleTypeDescription_POP3AndIMAP4Protocols");
			CoreStrings.stringIDs.Add(860548277U, "TimeZoneTaipeiStandardTime");
			CoreStrings.stringIDs.Add(1145350245U, "RoleTypeDescription_HelpdeskRecipientManagement");
			CoreStrings.stringIDs.Add(756612021U, "TimeZoneHawaiianStandardTime");
			CoreStrings.stringIDs.Add(2907857020U, "TimeZoneMagallanesStandardTime");
			CoreStrings.stringIDs.Add(101906589U, "EventFailedTransportRulesHelpDesk");
			CoreStrings.stringIDs.Add(2104951827U, "TimeZoneTokyoStandardTime");
			CoreStrings.stringIDs.Add(2425183541U, "DeliveryStatusAll");
			CoreStrings.stringIDs.Add(4216139125U, "RoleTypeDescription_MyOptions");
			CoreStrings.stringIDs.Add(996841945U, "RoleTypeDescription_DisasterRecovery");
			CoreStrings.stringIDs.Add(3202528089U, "RoleTypeDescription_EmailAddressPolicies");
			CoreStrings.stringIDs.Add(3655383969U, "RoleTypeDescription_SendConnectors");
			CoreStrings.stringIDs.Add(3185790196U, "TimeZoneSudanStandardTime");
			CoreStrings.stringIDs.Add(1544756182U, "EventRead");
			CoreStrings.stringIDs.Add(2467780809U, "TimeZoneGreenlandStandardTime");
			CoreStrings.stringIDs.Add(2169942641U, "RoleTypeDescription_OutlookProvider");
			CoreStrings.stringIDs.Add(1460332657U, "EventTransferredIntermediate");
			CoreStrings.stringIDs.Add(3444675422U, "RoleTypeDescription_OrgCustomApps");
			CoreStrings.stringIDs.Add(2174613220U, "TimeZoneBangladeshStandardTime");
			CoreStrings.stringIDs.Add(373243623U, "TimeZoneGMTStandardTime");
			CoreStrings.stringIDs.Add(1223375664U, "TimeZoneGTBStandardTime");
			CoreStrings.stringIDs.Add(4078458935U, "JobStatusNotStarted");
			CoreStrings.stringIDs.Add(1663352993U, "TrackingWarningNoResultsDueToLogsNotFound");
			CoreStrings.stringIDs.Add(77017683U, "TimeZoneSAWesternStandardTime");
			CoreStrings.stringIDs.Add(3490088586U, "TimeZoneHaitiStandardTime");
			CoreStrings.stringIDs.Add(2700445791U, "TrackingErrorFailedToInitialize");
			CoreStrings.stringIDs.Add(728892142U, "TimeZoneKamchatkaStandardTime");
			CoreStrings.stringIDs.Add(4111676399U, "TrackingWarningResultsMissingTransient");
			CoreStrings.stringIDs.Add(2247997721U, "RulesMerged");
			CoreStrings.stringIDs.Add(3804873683U, "RoleTypeDescription_MyRetentionPolicies");
			CoreStrings.stringIDs.Add(647678831U, "DomainScopedRulesMerged");
			CoreStrings.stringIDs.Add(1612370800U, "EventResolvedHelpDesk");
			CoreStrings.stringIDs.Add(1531217347U, "RoleTypeDescription_LegalHoldApplication");
			CoreStrings.stringIDs.Add(3498536455U, "TimeZoneSingaporeStandardTime");
			CoreStrings.stringIDs.Add(1949143649U, "AntimalwareAdminAddressNull");
			CoreStrings.stringIDs.Add(2725696980U, "RoleTypeDescription_UserApplication");
			CoreStrings.stringIDs.Add(2096766553U, "RoleTypeDescription_MyContactInformation");
			CoreStrings.stringIDs.Add(967467375U, "TimeZoneNorthAsiaEastStandardTime");
			CoreStrings.stringIDs.Add(4108314201U, "ExchangeRecipientAdminDescription");
			CoreStrings.stringIDs.Add(226934128U, "TimeZoneCapeVerdeStandardTime");
			CoreStrings.stringIDs.Add(3317872187U, "TrackingSearchNotAuthorized");
			CoreStrings.stringIDs.Add(1575862374U, "ContentIndexStatusCrawling");
			CoreStrings.stringIDs.Add(2874655766U, "TimeZoneMagadanStandardTime");
			CoreStrings.stringIDs.Add(2055163269U, "TimeZoneAUSCentralStandardTime");
			CoreStrings.stringIDs.Add(2585558426U, "ExchangeDiscoveryManagementDescription");
			CoreStrings.stringIDs.Add(3586764147U, "EventMessageDefer");
			CoreStrings.stringIDs.Add(2033601224U, "TimeZoneVolgogradStandardTime");
			CoreStrings.stringIDs.Add(2373652411U, "TimeZonePacificStandardTimeMexico");
			CoreStrings.stringIDs.Add(2679378804U, "TimeZoneSamoaStandardTime");
			CoreStrings.stringIDs.Add(2872669904U, "RoleTypeDescription_PersonallyIdentifiableInformation");
			CoreStrings.stringIDs.Add(728513226U, "TimeZoneUTC12");
			CoreStrings.stringIDs.Add(3152944101U, "RoleTypeDescription_MessageTracking");
			CoreStrings.stringIDs.Add(1084831951U, "TraceLevelLow");
			CoreStrings.stringIDs.Add(661809288U, "RoleTypeDescription_MyTextMessaging");
			CoreStrings.stringIDs.Add(2577770798U, "RoleTypeDescription_RecipientPolicies");
			CoreStrings.stringIDs.Add(953426317U, "TimeZoneSAEasternStandardTime");
			CoreStrings.stringIDs.Add(2376615838U, "RoleTypeDescription_TeamMailboxLifecycleApplication");
			CoreStrings.stringIDs.Add(1205552534U, "InvalidMessageTrackingReportId");
			CoreStrings.stringIDs.Add(430105556U, "TimeZoneIndiaStandardTime");
			CoreStrings.stringIDs.Add(2562345274U, "ContentIndexStatusFailed");
			CoreStrings.stringIDs.Add(1494389266U, "TimeZoneNewfoundlandStandardTime");
			CoreStrings.stringIDs.Add(2246440755U, "TimeZoneYakutskStandardTime");
			CoreStrings.stringIDs.Add(1274798456U, "RoleTypeDescription_OrganizationHelpSettings");
			CoreStrings.stringIDs.Add(1835211555U, "EventFailedModerationHelpDesk");
			CoreStrings.stringIDs.Add(903792657U, "RoleTypeDescription_PublicFolders");
			CoreStrings.stringIDs.Add(3005390568U, "TimeZonePacificStandardTime");
			CoreStrings.stringIDs.Add(61789322U, "TimeZoneAleutianStandardTime");
			CoreStrings.stringIDs.Add(3049152015U, "ExchangeManagementForestTier1SupportDescription");
			CoreStrings.stringIDs.Add(2034512234U, "TimeZoneNorthKoreaStandardTime");
			CoreStrings.stringIDs.Add(920357151U, "DltUnknownTimeZone");
			CoreStrings.stringIDs.Add(2373064468U, "RoleTypeDescription_ExchangeConnectors");
			CoreStrings.stringIDs.Add(4036330655U, "RoleTypeDescription_ViewOnlyCentralAdminSupport");
			CoreStrings.stringIDs.Add(4097835338U, "TimeZoneNorfolkStandardTime");
			CoreStrings.stringIDs.Add(273095822U, "RoleTypeDescription_MyProfileInformation");
			CoreStrings.stringIDs.Add(1814215051U, "TimeZoneSaoTomeStandardTime");
			CoreStrings.stringIDs.Add(2648516368U, "RoleTypeDescription_Journaling");
			CoreStrings.stringIDs.Add(1736152949U, "TimeZoneAzoresStandardTime");
			CoreStrings.stringIDs.Add(290876870U, "TimeZoneAusCentralWStandardTime");
			CoreStrings.stringIDs.Add(4043635750U, "TimeZoneUSMountainStandardTime");
			CoreStrings.stringIDs.Add(606097983U, "RoleTypeDescription_ExchangeCrossServiceIntegration");
			CoreStrings.stringIDs.Add(3335916139U, "PolicyMigrationSucceeded");
			CoreStrings.stringIDs.Add(140823076U, "TimeZoneCentralBrazilianStandardTime");
			CoreStrings.stringIDs.Add(2103082753U, "EventRulesCc");
			CoreStrings.stringIDs.Add(2088363403U, "ComplianceManagementGroupDescription");
			CoreStrings.stringIDs.Add(1556269475U, "TimeZoneWCentralAfricaStandardTime");
			CoreStrings.stringIDs.Add(3371502447U, "RoleTypeDescription_UMMailboxes");
			CoreStrings.stringIDs.Add(1332960560U, "ExchangeAllHostedOrgsDescription");
			CoreStrings.stringIDs.Add(1124818555U, "RoleTypeDescription_DistributionGroups");
			CoreStrings.stringIDs.Add(92857077U, "ExchangeDelegatedSetupDescription");
			CoreStrings.stringIDs.Add(2984557217U, "TimeZoneVladivostokStandardTime");
			CoreStrings.stringIDs.Add(1755122990U, "TimeZoneCentralStandardTime");
			CoreStrings.stringIDs.Add(278680123U, "RoleTypeDescription_OrganizationClientAccess");
			CoreStrings.stringIDs.Add(137603179U, "ExchangeViewOnlyAdminDescription");
			CoreStrings.stringIDs.Add(2189811946U, "TimeZoneEAustraliaStandardTime");
			CoreStrings.stringIDs.Add(763976563U, "TrackingTransientError");
			CoreStrings.stringIDs.Add(3970531209U, "TrafficScopeInboundAndOutbound");
			CoreStrings.stringIDs.Add(675468529U, "RoleTypeDescription_CentralAdminCredentialManagement");
			CoreStrings.stringIDs.Add(2365507750U, "RoleTypeDescription_UnScoped");
			CoreStrings.stringIDs.Add(2259701124U, "RoleTypeDescription_MyDistributionGroupMembership");
			CoreStrings.stringIDs.Add(3079546428U, "RoleTypeDescription_OfficeExtensionApplication");
			CoreStrings.stringIDs.Add(3118752499U, "RoleTypeDescription_DistributionGroupManagement");
			CoreStrings.stringIDs.Add(2462125228U, "AntimalwareMigrationSucceeded");
			CoreStrings.stringIDs.Add(529456528U, "TimeZoneWEuropeStandardTime");
			CoreStrings.stringIDs.Add(172038955U, "RoleTypeDescription_AuditLogs");
			CoreStrings.stringIDs.Add(3669915041U, "EventSubmitted");
			CoreStrings.stringIDs.Add(2836851600U, "TimeZoneEasternStandardTimeMexico");
			CoreStrings.stringIDs.Add(2471825646U, "TimeZoneRomanceStandardTime");
			CoreStrings.stringIDs.Add(2847973369U, "TimeZoneIranStandardTime");
			CoreStrings.stringIDs.Add(2304159373U, "RoleTypeDescription_AccessToCustomerDataDCOnly");
			CoreStrings.stringIDs.Add(102930739U, "InvalidIdentityForAdmin");
			CoreStrings.stringIDs.Add(3118313859U, "TimeZoneMarquesasStandardTime");
			CoreStrings.stringIDs.Add(4010596708U, "ContentIndexStatusHealthy");
			CoreStrings.stringIDs.Add(2141699896U, "RunMonthly");
			CoreStrings.stringIDs.Add(1224765338U, "RoleTypeDescription_OrganizationTransportSettings");
			CoreStrings.stringIDs.Add(56162392U, "RoleTypeDescription_ViewOnlyConfiguration");
			CoreStrings.stringIDs.Add(3450693719U, "TimeZoneMountainStandardTimeMexico");
			CoreStrings.stringIDs.Add(3338361539U, "RoleTypeDescription_PartnerDelegatedTenantManagement");
			CoreStrings.stringIDs.Add(1621594316U, "RoleTypeDescription_OrganizationConfiguration");
			CoreStrings.stringIDs.Add(3266046896U, "MsoMailTenantAdminGroupDescription");
			CoreStrings.stringIDs.Add(4268682097U, "TimeZoneSEAsiaStandardTime");
			CoreStrings.stringIDs.Add(3867579719U, "RoleTypeDescription_DatabaseCopies");
			CoreStrings.stringIDs.Add(1556691491U, "TimeZoneCentralStandardTimeMexico");
			CoreStrings.stringIDs.Add(2695329098U, "OutboundIpMigrationCompleted");
			CoreStrings.stringIDs.Add(2154772013U, "RecipientPathFilterNeeded");
			CoreStrings.stringIDs.Add(3188452985U, "TrackingWarningResultsMissingConnection");
			CoreStrings.stringIDs.Add(4058915032U, "TimeZoneNCentralAsiaStandardTime");
			CoreStrings.stringIDs.Add(1753131922U, "PolicyTipReject");
			CoreStrings.stringIDs.Add(2037460782U, "AuditSeverityLevelLow");
			CoreStrings.stringIDs.Add(2054972301U, "RoleTypeDescription_RecipientManagement");
			CoreStrings.stringIDs.Add(435759238U, "EventForwarded");
			CoreStrings.stringIDs.Add(1260062527U, "EventApprovedModerationHelpDesk");
			CoreStrings.stringIDs.Add(2235638931U, "Reject");
			CoreStrings.stringIDs.Add(3526220825U, "TimeZoneSaintPierreStandardTime");
			CoreStrings.stringIDs.Add(1391837657U, "TimeZoneMoroccoStandardTime");
			CoreStrings.stringIDs.Add(3250495282U, "TimeZoneFLEStandardTime");
			CoreStrings.stringIDs.Add(566029912U, "RoleTypeDescription_MailTips");
			CoreStrings.stringIDs.Add(3881958977U, "RoleTypeDescription_Monitoring");
			CoreStrings.stringIDs.Add(2892044242U, "AntispamEdgeBlockModeSettingNotMigrated");
			CoreStrings.stringIDs.Add(779085836U, "TimeZoneCanadaCentralStandardTime");
			CoreStrings.stringIDs.Add(2024391679U, "ExchangeManagementForestMonitoringDescription");
			CoreStrings.stringIDs.Add(1004126924U, "TimeZoneSakhalinStandardTime");
			CoreStrings.stringIDs.Add(59677322U, "TimeZoneSriLankaStandardTime");
			CoreStrings.stringIDs.Add(766524993U, "RoleTypeDescription_AutoDiscover");
			CoreStrings.stringIDs.Add(4291544598U, "TimeZoneRussiaTimeZone10");
			CoreStrings.stringIDs.Add(3673094505U, "EventTransferredToSMSMessage");
			CoreStrings.stringIDs.Add(3152178141U, "TrackingWarningNoResultsDueToLogsExpired");
			CoreStrings.stringIDs.Add(3797334152U, "MessageDirectionSent");
			CoreStrings.stringIDs.Add(4116693438U, "RoleTypeDescription_RecordsManagement");
			CoreStrings.stringIDs.Add(825624151U, "RoleTypeDescription_TransportRules");
			CoreStrings.stringIDs.Add(1566193246U, "ForceTls");
			CoreStrings.stringIDs.Add(3297737305U, "EventFailedGeneral");
			CoreStrings.stringIDs.Add(2580146609U, "RoleTypeDescription_ExchangeServerCertificates");
			CoreStrings.stringIDs.Add(1092647167U, "EventPending");
			CoreStrings.stringIDs.Add(845119240U, "TimeZoneChinaStandardTime");
			CoreStrings.stringIDs.Add(4218393972U, "TimeZoneWestBankStandardTime");
			CoreStrings.stringIDs.Add(1316750960U, "RoleTypeDescription_MyBaseOptions");
			CoreStrings.stringIDs.Add(3120202503U, "TimeZoneGreenwichStandardTime");
			CoreStrings.stringIDs.Add(3840952578U, "RoleTypeDescription_UMPromptManagement");
			CoreStrings.stringIDs.Add(3678129241U, "AuditSeverityLevelMedium");
			CoreStrings.stringIDs.Add(4008278162U, "StatusFilterCannotBeSpecified");
			CoreStrings.stringIDs.Add(1056819816U, "ContentIndexStatusSuspended");
			CoreStrings.stringIDs.Add(728513227U, "TimeZoneUTC13");
			CoreStrings.stringIDs.Add(1737084126U, "PolicyTipUrl");
			CoreStrings.stringIDs.Add(2937930519U, "JobStatusDone");
			CoreStrings.stringIDs.Add(2403632430U, "Quarantine");
			CoreStrings.stringIDs.Add(3403825909U, "TrackingWarningResultsMissingFatal");
			CoreStrings.stringIDs.Add(1215124770U, "TimeZoneNewZealandStandardTime");
			CoreStrings.stringIDs.Add(3584252448U, "ExecutableAttachments");
			CoreStrings.stringIDs.Add(2732724415U, "TimeZoneDatelineStandardTime");
			CoreStrings.stringIDs.Add(1156214899U, "EventQueueRetryIW");
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00007D73 File Offset: 0x00005F73
		public static LocalizedString RoleTypeDescription_RemoteAndAcceptedDomains
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_RemoteAndAcceptedDomains", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00007D91 File Offset: 0x00005F91
		public static LocalizedString TimeZoneSouthAfricaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneSouthAfricaStandardTime", "Ex180677", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00007DAF File Offset: 0x00005FAF
		public static LocalizedString PolicyTipNotifyOnly
		{
			get
			{
				return new LocalizedString("PolicyTipNotifyOnly", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00007DCD File Offset: 0x00005FCD
		public static LocalizedString TimeZoneLibyaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneLibyaStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00007DEB File Offset: 0x00005FEB
		public static LocalizedString ReportSeverityLow
		{
			get
			{
				return new LocalizedString("ReportSeverityLow", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00007E09 File Offset: 0x00006009
		public static LocalizedString RoleTypeDescription_ArchiveApplication
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ArchiveApplication", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007E28 File Offset: 0x00006028
		public static LocalizedString InvalidIpRange(int ruleId, string invalidIpRange)
		{
			return new LocalizedString("InvalidIpRange", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId,
				invalidIpRange
			});
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00007E60 File Offset: 0x00006060
		public static LocalizedString EventDelivered
		{
			get
			{
				return new LocalizedString("EventDelivered", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00007E7E File Offset: 0x0000607E
		public static LocalizedString AntispamIPBlockListSettingMigrated
		{
			get
			{
				return new LocalizedString("AntispamIPBlockListSettingMigrated", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00007E9C File Offset: 0x0000609C
		public static LocalizedString RoleTypeDescription_ActiveMonitoring
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ActiveMonitoring", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00007EBA File Offset: 0x000060BA
		public static LocalizedString RoleTypeDescription_UMPrompts
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_UMPrompts", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00007ED8 File Offset: 0x000060D8
		public static LocalizedString TimeZoneCentralAsiaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneCentralAsiaStandardTime", "Ex7D6A1F", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00007EF6 File Offset: 0x000060F6
		public static LocalizedString TimeZoneESouthAmericaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneESouthAmericaStandardTime", "ExAA4B78", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00007F14 File Offset: 0x00006114
		public static LocalizedString TimeZoneMountainStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneMountainStandardTime", "Ex5F9A41", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00007F32 File Offset: 0x00006132
		public static LocalizedString TimeZoneKaliningradStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneKaliningradStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00007F50 File Offset: 0x00006150
		public static LocalizedString ExchangeViewOnlyManagementForestOperatorDescription
		{
			get
			{
				return new LocalizedString("ExchangeViewOnlyManagementForestOperatorDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00007F6E File Offset: 0x0000616E
		public static LocalizedString AntispamMigrationSucceeded
		{
			get
			{
				return new LocalizedString("AntispamMigrationSucceeded", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00007F8C File Offset: 0x0000618C
		public static LocalizedString TimeZoneNorthAsiaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneNorthAsiaStandardTime", "ExA93F77", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00007FAA File Offset: 0x000061AA
		public static LocalizedString InvalidSender
		{
			get
			{
				return new LocalizedString("InvalidSender", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00007FC8 File Offset: 0x000061C8
		public static LocalizedString RoleTypeDescription_MyTeamMailboxes
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyTeamMailboxes", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00007FE6 File Offset: 0x000061E6
		public static LocalizedString RoleTypeDescription_MyDistributionGroups
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyDistributionGroups", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00008004 File Offset: 0x00006204
		public static LocalizedString TimeZoneFijiStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneFijiStandardTime", "Ex762231", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00008022 File Offset: 0x00006222
		public static LocalizedString TimeZoneEAfricaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneEAfricaStandardTime", "ExCF702A", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00008040 File Offset: 0x00006240
		public static LocalizedString NotAuthorizedToViewRecipientPath
		{
			get
			{
				return new LocalizedString("NotAuthorizedToViewRecipientPath", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000805E File Offset: 0x0000625E
		public static LocalizedString RoleTypeDescription_UserOptions
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_UserOptions", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000807C File Offset: 0x0000627C
		public static LocalizedString RoleTypeDescription_UmRecipientManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_UmRecipientManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000809C File Offset: 0x0000629C
		public static LocalizedString InvalidDomainNameInDomainSettings(string domainName)
		{
			return new LocalizedString("InvalidDomainNameInDomainSettings", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				domainName
			});
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000080CB File Offset: 0x000062CB
		public static LocalizedString ReportSeverityHigh
		{
			get
			{
				return new LocalizedString("ReportSeverityHigh", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000080EC File Offset: 0x000062EC
		public static LocalizedString FopePolicyRuleIsTooLargeToMigrate(string ruleName, ulong ruleSize, ulong maxRuleSize)
		{
			return new LocalizedString("FopePolicyRuleIsTooLargeToMigrate", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleName,
				ruleSize,
				maxRuleSize
			});
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000812D File Offset: 0x0000632D
		public static LocalizedString RoleTypeDescription_CmdletExtensionAgents
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_CmdletExtensionAgents", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000814C File Offset: 0x0000634C
		public static LocalizedString OpportunisticTls(int ruleId)
		{
			return new LocalizedString("OpportunisticTls", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00008180 File Offset: 0x00006380
		public static LocalizedString AntimalwareUserOptOut
		{
			get
			{
				return new LocalizedString("AntimalwareUserOptOut", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000081A0 File Offset: 0x000063A0
		public static LocalizedString EventMovedToFolderByInboxRuleHelpDesk(string folderName)
		{
			return new LocalizedString("EventMovedToFolderByInboxRuleHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000081CF File Offset: 0x000063CF
		public static LocalizedString RoleTypeDescription_MyLogon
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyLogon", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000081ED File Offset: 0x000063ED
		public static LocalizedString ContentIndexStatusDisabled
		{
			get
			{
				return new LocalizedString("ContentIndexStatusDisabled", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000820B File Offset: 0x0000640B
		public static LocalizedString PolicyTipRejectOverride
		{
			get
			{
				return new LocalizedString("PolicyTipRejectOverride", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00008229 File Offset: 0x00006429
		public static LocalizedString StatusDelivered
		{
			get
			{
				return new LocalizedString("StatusDelivered", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00008247 File Offset: 0x00006447
		public static LocalizedString TimeZoneWMongoliaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneWMongoliaStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00008265 File Offset: 0x00006465
		public static LocalizedString RoleTypeDescription_TransportHygiene
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_TransportHygiene", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00008283 File Offset: 0x00006483
		public static LocalizedString TraceLevelHigh
		{
			get
			{
				return new LocalizedString("TraceLevelHigh", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000082A1 File Offset: 0x000064A1
		public static LocalizedString RoleTypeDescription_InformationRightsManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_InformationRightsManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000082BF File Offset: 0x000064BF
		public static LocalizedString TestModifySubject
		{
			get
			{
				return new LocalizedString("TestModifySubject", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000082DD File Offset: 0x000064DD
		public static LocalizedString TimeZoneLordHoweStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneLordHoweStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000082FB File Offset: 0x000064FB
		public static LocalizedString InvalidSenderForAdmin
		{
			get
			{
				return new LocalizedString("InvalidSenderForAdmin", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00008319 File Offset: 0x00006519
		public static LocalizedString RoleTypeDescription_DiscoveryManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_DiscoveryManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008338 File Offset: 0x00006538
		public static LocalizedString FopePolicyRuleContainsInvalidPattern(string ruleName)
		{
			return new LocalizedString("FopePolicyRuleContainsInvalidPattern", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00008367 File Offset: 0x00006567
		public static LocalizedString TimeZoneTurksAndCaicosStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneTurksAndCaicosStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00008385 File Offset: 0x00006585
		public static LocalizedString RoleTypeDescription_MailboxExport
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MailboxExport", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000083A3 File Offset: 0x000065A3
		public static LocalizedString AntispamIPAllowListSettingMigrated
		{
			get
			{
				return new LocalizedString("AntispamIPAllowListSettingMigrated", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000083C1 File Offset: 0x000065C1
		public static LocalizedString ExchangeManagementForestOperatorDescription
		{
			get
			{
				return new LocalizedString("ExchangeManagementForestOperatorDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000083DF File Offset: 0x000065DF
		public static LocalizedString TimeZoneArabianStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneArabianStandardTime", "Ex36DED6", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000083FD File Offset: 0x000065FD
		public static LocalizedString TimeZoneIsraelStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneIsraelStandardTime", "Ex2004E1", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000841B File Offset: 0x0000661B
		public static LocalizedString RoleTypeDescription_ActiveDirectoryPermissions
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ActiveDirectoryPermissions", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000843C File Offset: 0x0000663C
		public static LocalizedString EventSubmittedHelpDesk(string hubServerFqdn)
		{
			return new LocalizedString("EventSubmittedHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				hubServerFqdn
			});
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000846B File Offset: 0x0000666B
		public static LocalizedString RunOnce
		{
			get
			{
				return new LocalizedString("RunOnce", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000848C File Offset: 0x0000668C
		public static LocalizedString FopePolicyRuleExpired(int ruleId, DateTime expiredOn)
		{
			return new LocalizedString("FopePolicyRuleExpired", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId,
				expiredOn
			});
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000084C9 File Offset: 0x000066C9
		public static LocalizedString MessageDirectionAll
		{
			get
			{
				return new LocalizedString("MessageDirectionAll", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000084E7 File Offset: 0x000066E7
		public static LocalizedString RoleTypeDescription_UmManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_UmManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00008505 File Offset: 0x00006705
		public static LocalizedString AuditSeverityLevelHigh
		{
			get
			{
				return new LocalizedString("AuditSeverityLevelHigh", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00008523 File Offset: 0x00006723
		public static LocalizedString TimeZoneArgentinaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneArgentinaStandardTime", "ExBE5AC7", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008544 File Offset: 0x00006744
		public static LocalizedString RecipientDomainConditionContainsInvalidDomainNames(int ruleId, string domainNames)
		{
			return new LocalizedString("RecipientDomainConditionContainsInvalidDomainNames", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId,
				domainNames
			});
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000857C File Offset: 0x0000677C
		public static LocalizedString RoleTypeDescription_Migration
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_Migration", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000859A File Offset: 0x0000679A
		public static LocalizedString TimeZoneUlaanbaatarStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneUlaanbaatarStandardTime", "Ex5AA96E", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000085B8 File Offset: 0x000067B8
		public static LocalizedString TimeZoneSaratovStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneSaratovStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000085D8 File Offset: 0x000067D8
		public static LocalizedString DistributionGroupForVirtualDomainsCreated(string dgName, string dgOwner)
		{
			return new LocalizedString("DistributionGroupForVirtualDomainsCreated", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				dgName,
				dgOwner
			});
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000860C File Offset: 0x0000680C
		public static LocalizedString EventSmtpReceiveHelpDesk(string local, string remote)
		{
			return new LocalizedString("EventSmtpReceiveHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				local,
				remote
			});
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000863F File Offset: 0x0000683F
		public static LocalizedString TimeZoneVenezuelaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneVenezuelaStandardTime", "Ex5A6891", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008660 File Offset: 0x00006860
		public static LocalizedString RecipientDomainNames(string recipientDomain)
		{
			return new LocalizedString("RecipientDomainNames", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				recipientDomain
			});
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000868F File Offset: 0x0000688F
		public static LocalizedString ExchangeAllMailboxesDescription
		{
			get
			{
				return new LocalizedString("ExchangeAllMailboxesDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000086AD File Offset: 0x000068AD
		public static LocalizedString RoleTypeDescription_GALSynchronizationManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_GALSynchronizationManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000086CB File Offset: 0x000068CB
		public static LocalizedString RoleTypeDescription_ViewOnlyRoleManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ViewOnlyRoleManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000086E9 File Offset: 0x000068E9
		public static LocalizedString RoleTypeDescription_LiveID
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_LiveID", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008708 File Offset: 0x00006908
		public static LocalizedString BodyCaseSensitive(string body)
		{
			return new LocalizedString("BodyCaseSensitive", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				body
			});
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008738 File Offset: 0x00006938
		public static LocalizedString TenantSkuNotSupportedByAntispam(string skuName)
		{
			return new LocalizedString("TenantSkuNotSupportedByAntispam", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				skuName
			});
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008768 File Offset: 0x00006968
		public static LocalizedString DisabledInboundConnector(string name)
		{
			return new LocalizedString("DisabledInboundConnector", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008798 File Offset: 0x00006998
		public static LocalizedString InvalidSmtpAddressInFopeRule(int ruleId, string smtpAddress)
		{
			return new LocalizedString("InvalidSmtpAddressInFopeRule", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId,
				smtpAddress
			});
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000087D0 File Offset: 0x000069D0
		public static LocalizedString RoleTypeDescription_TransportAgents
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_TransportAgents", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000087EE File Offset: 0x000069EE
		public static LocalizedString RoleTypeDescription_MyDiagnostics
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyDiagnostics", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000880C File Offset: 0x00006A0C
		public static LocalizedString TrackingWarningNoResultsDueToUntrackableMessagePath
		{
			get
			{
				return new LocalizedString("TrackingWarningNoResultsDueToUntrackableMessagePath", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000882A File Offset: 0x00006A2A
		public static LocalizedString EventTransferredToLegacyExchangeServer
		{
			get
			{
				return new LocalizedString("EventTransferredToLegacyExchangeServer", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00008848 File Offset: 0x00006A48
		public static LocalizedString RoleTypeDescription_TeamMailboxes
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_TeamMailboxes", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00008866 File Offset: 0x00006A66
		public static LocalizedString TimeZoneSAPacificStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneSAPacificStandardTime", "ExA231DF", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00008884 File Offset: 0x00006A84
		public static LocalizedString ExchangeServerManagementDescription
		{
			get
			{
				return new LocalizedString("ExchangeServerManagementDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000088A2 File Offset: 0x00006AA2
		public static LocalizedString UnsupportedSenderForTracking
		{
			get
			{
				return new LocalizedString("UnsupportedSenderForTracking", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000088C0 File Offset: 0x00006AC0
		public static LocalizedString TimeZoneMontevideoStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneMontevideoStandardTime", "ExFB22C1", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000088DE File Offset: 0x00006ADE
		public static LocalizedString TimeZoneUSEasternStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneUSEasternStandardTime", "ExB9A839", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000088FC File Offset: 0x00006AFC
		public static LocalizedString EventApprovedModerationIW
		{
			get
			{
				return new LocalizedString("EventApprovedModerationIW", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000891A File Offset: 0x00006B1A
		public static LocalizedString TimeZoneCentralAmericaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneCentralAmericaStandardTime", "Ex441256", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00008938 File Offset: 0x00006B38
		public static LocalizedString ContentIndexStatusAutoSuspended
		{
			get
			{
				return new LocalizedString("ContentIndexStatusAutoSuspended", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00008956 File Offset: 0x00006B56
		public static LocalizedString TimeZoneBahiaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneBahiaStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00008974 File Offset: 0x00006B74
		public static LocalizedString RoleTypeDescription_EdgeSubscriptions
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_EdgeSubscriptions", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008994 File Offset: 0x00006B94
		public static LocalizedString RecipientAddresses(string recipientAddress)
		{
			return new LocalizedString("RecipientAddresses", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				recipientAddress
			});
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000089C3 File Offset: 0x00006BC3
		public static LocalizedString TimeZoneCubaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneCubaStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000089E1 File Offset: 0x00006BE1
		public static LocalizedString TimeZoneEasterIslandStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneEasterIslandStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008A00 File Offset: 0x00006C00
		public static LocalizedString SubjectExactMatchCondition(int ruleId)
		{
			return new LocalizedString("SubjectExactMatchCondition", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00008A34 File Offset: 0x00006C34
		public static LocalizedString JobStatusInProgress
		{
			get
			{
				return new LocalizedString("JobStatusInProgress", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008A54 File Offset: 0x00006C54
		public static LocalizedString SubjectExactMatchCaseSensitive(string subject)
		{
			return new LocalizedString("SubjectExactMatchCaseSensitive", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				subject
			});
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00008A83 File Offset: 0x00006C83
		public static LocalizedString TimeZoneUTC09
		{
			get
			{
				return new LocalizedString("TimeZoneUTC09", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00008AA1 File Offset: 0x00006CA1
		public static LocalizedString TimeZoneEgyptStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneEgyptStandardTime", "ExB364F1", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00008ABF File Offset: 0x00006CBF
		public static LocalizedString StatusRead
		{
			get
			{
				return new LocalizedString("StatusRead", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00008ADD File Offset: 0x00006CDD
		public static LocalizedString TimeZoneCaucasusStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneCaucasusStandardTime", "ExAE218F", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00008AFB File Offset: 0x00006CFB
		public static LocalizedString RoleTypeDescription_MailEnabledPublicFolders
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MailEnabledPublicFolders", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00008B19 File Offset: 0x00006D19
		public static LocalizedString RoleTypeDescription_ViewOnlyAuditLogs
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ViewOnlyAuditLogs", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00008B37 File Offset: 0x00006D37
		public static LocalizedString TrackingWarningReadStatusUnavailable
		{
			get
			{
				return new LocalizedString("TrackingWarningReadStatusUnavailable", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00008B55 File Offset: 0x00006D55
		public static LocalizedString ViewOnlyPIIGroupDescription
		{
			get
			{
				return new LocalizedString("ViewOnlyPIIGroupDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00008B73 File Offset: 0x00006D73
		public static LocalizedString RoleTypeDescription_FederatedSharing
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_FederatedSharing", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00008B91 File Offset: 0x00006D91
		public static LocalizedString RoleTypeDescription_RoleManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_RoleManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008BB0 File Offset: 0x00006DB0
		public static LocalizedString AntimalwareInboundRecipientNotificationsScoped(string policyName)
		{
			return new LocalizedString("AntimalwareInboundRecipientNotificationsScoped", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				policyName
			});
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00008BDF File Offset: 0x00006DDF
		public static LocalizedString TrackingExplanationNormalTimeSpan
		{
			get
			{
				return new LocalizedString("TrackingExplanationNormalTimeSpan", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00008BFD File Offset: 0x00006DFD
		public static LocalizedString RoleTypeDescription_ExchangeServers
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ExchangeServers", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00008C1B File Offset: 0x00006E1B
		public static LocalizedString TimeZoneAstrakhanStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneAstrakhanStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00008C39 File Offset: 0x00006E39
		public static LocalizedString ReportSeverityMedium
		{
			get
			{
				return new LocalizedString("ReportSeverityMedium", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008C58 File Offset: 0x00006E58
		public static LocalizedString Body(string body)
		{
			return new LocalizedString("Body", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				body
			});
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00008C87 File Offset: 0x00006E87
		public static LocalizedString RoleTypeDescription_SupportDiagnostics
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_SupportDiagnostics", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00008CA5 File Offset: 0x00006EA5
		public static LocalizedString TimeZoneCentralEuropeanStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneCentralEuropeanStandardTime", "Ex980388", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008CC3 File Offset: 0x00006EC3
		public static LocalizedString TimeZoneKoreaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneKoreaStandardTime", "Ex088D32", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00008CE1 File Offset: 0x00006EE1
		public static LocalizedString ContentIndexStatusSeeding
		{
			get
			{
				return new LocalizedString("ContentIndexStatusSeeding", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00008CFF File Offset: 0x00006EFF
		public static LocalizedString TimeZoneWAustraliaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneWAustraliaStandardTime", "ExEFC547", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00008D1D File Offset: 0x00006F1D
		public static LocalizedString RoleTypeDescription_Custom
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_Custom", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00008D3B File Offset: 0x00006F3B
		public static LocalizedString AntispamActionTypeSettingMigrated
		{
			get
			{
				return new LocalizedString("AntispamActionTypeSettingMigrated", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00008D59 File Offset: 0x00006F59
		public static LocalizedString TrackingExplanationExcessiveTimeSpan
		{
			get
			{
				return new LocalizedString("TrackingExplanationExcessiveTimeSpan", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00008D77 File Offset: 0x00006F77
		public static LocalizedString QuarantineSpam
		{
			get
			{
				return new LocalizedString("QuarantineSpam", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00008D95 File Offset: 0x00006F95
		public static LocalizedString TimeZoneUTC11
		{
			get
			{
				return new LocalizedString("TimeZoneUTC11", "Ex598DA7", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00008DB3 File Offset: 0x00006FB3
		public static LocalizedString RoleTypeDescription_PublicFolderReplication
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_PublicFolderReplication", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008DD4 File Offset: 0x00006FD4
		public static LocalizedString EventQueueRetryNoRetryTimeHelpDesk(string queueName, string errorMessage)
		{
			return new LocalizedString("EventQueueRetryNoRetryTimeHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				queueName,
				errorMessage
			});
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00008E07 File Offset: 0x00007007
		public static LocalizedString Encrypt
		{
			get
			{
				return new LocalizedString("Encrypt", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00008E25 File Offset: 0x00007025
		public static LocalizedString TimeZoneRussiaTimeZone11
		{
			get
			{
				return new LocalizedString("TimeZoneRussiaTimeZone11", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00008E43 File Offset: 0x00007043
		public static LocalizedString TimeZoneEkaterinburgStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneEkaterinburgStandardTime", "Ex7F643C", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00008E61 File Offset: 0x00007061
		public static LocalizedString RoleTypeDescription_LegalHold
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_LegalHold", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00008E7F File Offset: 0x0000707F
		public static LocalizedString TimeZoneTocantinsStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneTocantinsStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00008E9D File Offset: 0x0000709D
		public static LocalizedString TimeZoneArabicStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneArabicStandardTime", "Ex249593", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008EBC File Offset: 0x000070BC
		public static LocalizedString SubjectCaseSensitiveCondition(int ruleId)
		{
			return new LocalizedString("SubjectCaseSensitiveCondition", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00008EF0 File Offset: 0x000070F0
		public static LocalizedString RoleTypeDescription_MailboxImportExport
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MailboxImportExport", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00008F0E File Offset: 0x0000710E
		public static LocalizedString RoleTypeDescription_Supervision
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_Supervision", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00008F2C File Offset: 0x0000712C
		public static LocalizedString RoleTypeDescription_LawEnforcementRequests
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_LawEnforcementRequests", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00008F4A File Offset: 0x0000714A
		public static LocalizedString RoleTypeDescription_MailboxSearchApplication
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MailboxSearchApplication", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00008F68 File Offset: 0x00007168
		public static LocalizedString RoleTypeDescription_RetentionManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_RetentionManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00008F86 File Offset: 0x00007186
		public static LocalizedString TimeZoneWestAsiaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneWestAsiaStandardTime", "Ex12FC9B", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00008FA4 File Offset: 0x000071A4
		public static LocalizedString QuarantineTransportRule
		{
			get
			{
				return new LocalizedString("QuarantineTransportRule", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00008FC2 File Offset: 0x000071C2
		public static LocalizedString RoleTypeDescription_ViewOnlyCentralAdminManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ViewOnlyCentralAdminManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00008FE0 File Offset: 0x000071E0
		public static LocalizedString EventFailedTransportRulesIW
		{
			get
			{
				return new LocalizedString("EventFailedTransportRulesIW", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00008FFE File Offset: 0x000071FE
		public static LocalizedString EventDelayedAfterTransferToPartnerOrgIW
		{
			get
			{
				return new LocalizedString("EventDelayedAfterTransferToPartnerOrgIW", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000901C File Offset: 0x0000721C
		public static LocalizedString RoleTypeDescription_DatacenterOperationsDCOnly
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_DatacenterOperationsDCOnly", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000903A File Offset: 0x0000723A
		public static LocalizedString TimeZoneTomskStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneTomskStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009058 File Offset: 0x00007258
		public static LocalizedString MaximumMessageSize(int maxSize)
		{
			return new LocalizedString("MaximumMessageSize", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				maxSize
			});
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000908C File Offset: 0x0000728C
		public static LocalizedString TrackingBusy
		{
			get
			{
				return new LocalizedString("TrackingBusy", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000090AA File Offset: 0x000072AA
		public static LocalizedString TimeZoneTongaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneTongaStandardTime", "Ex2872C8", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000090C8 File Offset: 0x000072C8
		public static LocalizedString TimeZoneTasmaniaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneTasmaniaStandardTime", "ExC5BEEF", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000090E6 File Offset: 0x000072E6
		public static LocalizedString ExchangePublicFolderAdminDescription
		{
			get
			{
				return new LocalizedString("ExchangePublicFolderAdminDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00009104 File Offset: 0x00007304
		public static LocalizedString TrafficScopeOutbound
		{
			get
			{
				return new LocalizedString("TrafficScopeOutbound", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00009124 File Offset: 0x00007324
		public static LocalizedString EventQueueRetryNoErrorHelpDesk(string server, string inRetrySinceTime, string lastAttemptTime, string timeZone)
		{
			return new LocalizedString("EventQueueRetryNoErrorHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				server,
				inRetrySinceTime,
				lastAttemptTime,
				timeZone
			});
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000915F File Offset: 0x0000735F
		public static LocalizedString EventForwardedToDelegateAndDeleted
		{
			get
			{
				return new LocalizedString("EventForwardedToDelegateAndDeleted", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000917D File Offset: 0x0000737D
		public static LocalizedString RoleTypeDescription_GALSynchronization
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_GALSynchronization", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000919B File Offset: 0x0000739B
		public static LocalizedString CompressionOutOfMemory
		{
			get
			{
				return new LocalizedString("CompressionOutOfMemory", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000091BC File Offset: 0x000073BC
		public static LocalizedString InvalidUserName(string userName)
		{
			return new LocalizedString("InvalidUserName", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000091EB File Offset: 0x000073EB
		public static LocalizedString TrafficScopeDisabled
		{
			get
			{
				return new LocalizedString("TrafficScopeDisabled", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00009209 File Offset: 0x00007409
		public static LocalizedString RoleTypeDescription_OrganizationManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_OrganizationManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00009227 File Offset: 0x00007427
		public static LocalizedString TimeZoneOmskStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneOmskStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00009248 File Offset: 0x00007448
		public static LocalizedString AntimalwareAdminAddressValidations(string invalidSmtpAddress)
		{
			return new LocalizedString("AntimalwareAdminAddressValidations", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				invalidSmtpAddress
			});
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00009278 File Offset: 0x00007478
		public static LocalizedString SenderDomainNames(string senderDomain)
		{
			return new LocalizedString("SenderDomainNames", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				senderDomain
			});
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000092A7 File Offset: 0x000074A7
		public static LocalizedString TimeZoneBelarusStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneBelarusStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000092C5 File Offset: 0x000074C5
		public static LocalizedString TimeZoneParaguayStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneParaguayStandardTime", "ExCDD9CA", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000092E3 File Offset: 0x000074E3
		public static LocalizedString RoleTypeDescription_Reporting
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_Reporting", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00009301 File Offset: 0x00007501
		public static LocalizedString TimeZoneChathamIslandsStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneChathamIslandsStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000931F File Offset: 0x0000751F
		public static LocalizedString RoleTypeDescription_MyMailboxDelegation
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyMailboxDelegation", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000933D File Offset: 0x0000753D
		public static LocalizedString RoleTypeDescription_ExchangeVirtualDirectories
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ExchangeVirtualDirectories", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000935C File Offset: 0x0000755C
		public static LocalizedString Subject(string subject)
		{
			return new LocalizedString("Subject", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				subject
			});
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000938C File Offset: 0x0000758C
		public static LocalizedString CharacterSets(string charsets)
		{
			return new LocalizedString("CharacterSets", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				charsets
			});
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000093BB File Offset: 0x000075BB
		public static LocalizedString TimeZoneAUSEasternStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneAUSEasternStandardTime", "Ex7DA421", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000093D9 File Offset: 0x000075D9
		public static LocalizedString EventNotRead
		{
			get
			{
				return new LocalizedString("EventNotRead", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000093F7 File Offset: 0x000075F7
		public static LocalizedString TimeZoneMiddleEastStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneMiddleEastStandardTime", "Ex4F5342", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00009418 File Offset: 0x00007618
		public static LocalizedString AdminNotificationContainsMultipleAddresses(int ruleId, int numAdminAddress, string firstAddress, string skippedAddresses)
		{
			return new LocalizedString("AdminNotificationContainsMultipleAddresses", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId,
				numAdminAddress,
				firstAddress,
				skippedAddresses
			});
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000945D File Offset: 0x0000765D
		public static LocalizedString RoleTypeDescription_ApplicationImpersonation
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ApplicationImpersonation", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000947B File Offset: 0x0000767B
		public static LocalizedString TrackingWarningTooManyEvents
		{
			get
			{
				return new LocalizedString("TrackingWarningTooManyEvents", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00009499 File Offset: 0x00007699
		public static LocalizedString ContentIndexStatusUnknown
		{
			get
			{
				return new LocalizedString("ContentIndexStatusUnknown", "Ex891A11", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000094B7 File Offset: 0x000076B7
		public static LocalizedString TimeZoneSyriaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneSyriaStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000094D5 File Offset: 0x000076D5
		public static LocalizedString TimeZoneMauritiusStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneMauritiusStandardTime", "Ex15E3D2", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000094F3 File Offset: 0x000076F3
		public static LocalizedString TrackingMessageTypeNotSupported
		{
			get
			{
				return new LocalizedString("TrackingMessageTypeNotSupported", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00009511 File Offset: 0x00007711
		public static LocalizedString TimeZoneCentralPacificStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneCentralPacificStandardTime", "Ex12E5BB", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009530 File Offset: 0x00007730
		public static LocalizedString FopePolicyRuleDisabled(int ruleId)
		{
			return new LocalizedString("FopePolicyRuleDisabled", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00009564 File Offset: 0x00007764
		public static LocalizedString RoleTypeDescription_MailboxSearch
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MailboxSearch", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00009582 File Offset: 0x00007782
		public static LocalizedString StdUnknownTimeZone
		{
			get
			{
				return new LocalizedString("StdUnknownTimeZone", "Ex1B20DC", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000095A0 File Offset: 0x000077A0
		public static LocalizedString StatusUnsuccessFul
		{
			get
			{
				return new LocalizedString("StatusUnsuccessFul", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000095BE File Offset: 0x000077BE
		public static LocalizedString RoleTypeDescription_MyLinkedInEnabled
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyLinkedInEnabled", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000095DC File Offset: 0x000077DC
		public static LocalizedString TestXHeader
		{
			get
			{
				return new LocalizedString("TestXHeader", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000095FA File Offset: 0x000077FA
		public static LocalizedString RoleTypeDescription_ReceiveConnectors
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ReceiveConnectors", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00009618 File Offset: 0x00007818
		public static LocalizedString TimeZoneRussiaTimeZone3
		{
			get
			{
				return new LocalizedString("TimeZoneRussiaTimeZone3", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00009636 File Offset: 0x00007836
		public static LocalizedString TimeZoneTransbaikalStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneTransbaikalStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00009654 File Offset: 0x00007854
		public static LocalizedString RoleTypeDescription_Databases
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_Databases", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009674 File Offset: 0x00007874
		public static LocalizedString DistributionGroupForExcludedUsersCreated(string dgName, string dgOwner)
		{
			return new LocalizedString("DistributionGroupForExcludedUsersCreated", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				dgName,
				dgOwner
			});
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000096A8 File Offset: 0x000078A8
		public static LocalizedString BodyCaseSensitiveCondition(int ruleId)
		{
			return new LocalizedString("BodyCaseSensitiveCondition", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000096DC File Offset: 0x000078DC
		public static LocalizedString StatusTransferred
		{
			get
			{
				return new LocalizedString("StatusTransferred", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000096FC File Offset: 0x000078FC
		public static LocalizedString NoValidSmtpAddress(int ruleId)
		{
			return new LocalizedString("NoValidSmtpAddress", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00009730 File Offset: 0x00007930
		public static LocalizedString TimeZoneGeorgianStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneGeorgianStandardTime", "Ex1225C4", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000974E File Offset: 0x0000794E
		public static LocalizedString Decrypt
		{
			get
			{
				return new LocalizedString("Decrypt", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000976C File Offset: 0x0000796C
		public static LocalizedString RoleTypeDescription_MyReadWriteMailboxApps
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyReadWriteMailboxApps", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000978A File Offset: 0x0000798A
		public static LocalizedString TimeZoneBougainvilleStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneBougainvilleStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000097A8 File Offset: 0x000079A8
		public static LocalizedString RoleTypeDescription_EdgeSync
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_EdgeSync", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x000097C6 File Offset: 0x000079C6
		public static LocalizedString TimeZoneTurkeyStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneTurkeyStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000097E4 File Offset: 0x000079E4
		public static LocalizedString RoleTypeDescription_MyMailSubscriptions
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyMailSubscriptions", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00009802 File Offset: 0x00007A02
		public static LocalizedString PartialMessages
		{
			get
			{
				return new LocalizedString("PartialMessages", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00009820 File Offset: 0x00007A20
		public static LocalizedString InboundIpMigrationCompleted
		{
			get
			{
				return new LocalizedString("InboundIpMigrationCompleted", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000983E File Offset: 0x00007A3E
		public static LocalizedString RoleTypeDescription_MyMarketplaceApps
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyMarketplaceApps", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000985C File Offset: 0x00007A5C
		public static LocalizedString TimeZoneJordanStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneJordanStandardTime", "Ex72D0C7", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000987A File Offset: 0x00007A7A
		public static LocalizedString EventPendingModerationHelpDesk
		{
			get
			{
				return new LocalizedString("EventPendingModerationHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00009898 File Offset: 0x00007A98
		public static LocalizedString DeliveryStatusDelivered
		{
			get
			{
				return new LocalizedString("DeliveryStatusDelivered", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000098B6 File Offset: 0x00007AB6
		public static LocalizedString TimeZoneEEuropeStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneEEuropeStandardTime", "Ex906D9D", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000098D4 File Offset: 0x00007AD4
		public static LocalizedString RoleTypeDescription_MyVoiceMail
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyVoiceMail", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000098F2 File Offset: 0x00007AF2
		public static LocalizedString TimeZoneMyanmarStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneMyanmarStandardTime", "ExAA522C", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00009910 File Offset: 0x00007B10
		public static LocalizedString TrackingExplanationLogsDeleted
		{
			get
			{
				return new LocalizedString("TrackingExplanationLogsDeleted", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000992E File Offset: 0x00007B2E
		public static LocalizedString ExchangeOrgAdminDescription
		{
			get
			{
				return new LocalizedString("ExchangeOrgAdminDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000994C File Offset: 0x00007B4C
		public static LocalizedString FopePolicyRuleContainsRecipientAddressAndRecipientDomainConditions(int ruleId)
		{
			return new LocalizedString("FopePolicyRuleContainsRecipientAddressAndRecipientDomainConditions", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00009980 File Offset: 0x00007B80
		public static LocalizedString TimeZoneNepalStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneNepalStandardTime", "Ex3BEAAA", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000099A0 File Offset: 0x00007BA0
		public static LocalizedString FopePolicyRuleHasMaxRecipientsCondition(int ruleId, int maxRecipients)
		{
			return new LocalizedString("FopePolicyRuleHasMaxRecipientsCondition", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId,
				maxRecipients
			});
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000099E0 File Offset: 0x00007BE0
		public static LocalizedString ClassIdProperty(int ruleId)
		{
			return new LocalizedString("ClassIdProperty", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00009A14 File Offset: 0x00007C14
		public static LocalizedString TimeZoneCenAustraliaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneCenAustraliaStandardTime", "Ex0396B1", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00009A32 File Offset: 0x00007C32
		public static LocalizedString JobStatusFailed
		{
			get
			{
				return new LocalizedString("JobStatusFailed", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00009A50 File Offset: 0x00007C50
		public static LocalizedString RoleTypeDescription_TransportQueues
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_TransportQueues", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00009A70 File Offset: 0x00007C70
		public static LocalizedString MigratedFooterSizeExceedsDisclaimerMaxSize(string domain, string disclaimer, int actualSize, int maxSize)
		{
			return new LocalizedString("MigratedFooterSizeExceedsDisclaimerMaxSize", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				domain,
				disclaimer,
				actualSize,
				maxSize
			});
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00009AB8 File Offset: 0x00007CB8
		public static LocalizedString BodyExactMatchCaseSensitive(string body)
		{
			return new LocalizedString("BodyExactMatchCaseSensitive", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				body
			});
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00009AE7 File Offset: 0x00007CE7
		public static LocalizedString TimeZoneWestPacificStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneWestPacificStandardTime", "Ex631179", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00009B05 File Offset: 0x00007D05
		public static LocalizedString TrackingWarningNoResultsDueToTrackingTooEarly
		{
			get
			{
				return new LocalizedString("TrackingWarningNoResultsDueToTrackingTooEarly", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00009B23 File Offset: 0x00007D23
		public static LocalizedString RoleTypeDescription_ViewOnlyOrganizationManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ViewOnlyOrganizationManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00009B41 File Offset: 0x00007D41
		public static LocalizedString RoleTypeDescription_ViewOnlyRecipients
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ViewOnlyRecipients", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00009B5F File Offset: 0x00007D5F
		public static LocalizedString Allow
		{
			get
			{
				return new LocalizedString("Allow", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00009B7D File Offset: 0x00007D7D
		public static LocalizedString DomainScopeAlLDomains
		{
			get
			{
				return new LocalizedString("DomainScopeAlLDomains", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00009B9B File Offset: 0x00007D9B
		public static LocalizedString NoValidDomainNameExistsInDomainSettings
		{
			get
			{
				return new LocalizedString("NoValidDomainNameExistsInDomainSettings", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00009BBC File Offset: 0x00007DBC
		public static LocalizedString EventMovedToFolderByInboxRuleIW(string folderName)
		{
			return new LocalizedString("EventMovedToFolderByInboxRuleIW", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009BEC File Offset: 0x00007DEC
		public static LocalizedString DistributionListEmpty(int ruleId, string distributionList)
		{
			return new LocalizedString("DistributionListEmpty", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId,
				distributionList
			});
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00009C24 File Offset: 0x00007E24
		public static LocalizedString ExchangeRecordsManagementDescription
		{
			get
			{
				return new LocalizedString("ExchangeRecordsManagementDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00009C42 File Offset: 0x00007E42
		public static LocalizedString TimeZoneUTC08
		{
			get
			{
				return new LocalizedString("TimeZoneUTC08", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00009C60 File Offset: 0x00007E60
		public static LocalizedString RoleTypeDescription_NetworkingManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_NetworkingManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00009C7E File Offset: 0x00007E7E
		public static LocalizedString EventTransferredToForeignOrgHelpDesk
		{
			get
			{
				return new LocalizedString("EventTransferredToForeignOrgHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00009C9C File Offset: 0x00007E9C
		public static LocalizedString RoleTypeDescription_MyFacebookEnabled
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyFacebookEnabled", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00009CBA File Offset: 0x00007EBA
		public static LocalizedString TimeZoneCentralEuropeStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneCentralEuropeStandardTime", "ExC535AE", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009CD8 File Offset: 0x00007ED8
		public static LocalizedString RoleTypeDescription_SecurityGroupCreationAndMembership
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_SecurityGroupCreationAndMembership", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00009CF6 File Offset: 0x00007EF6
		public static LocalizedString DeliveryStatusExpanded
		{
			get
			{
				return new LocalizedString("DeliveryStatusExpanded", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00009D14 File Offset: 0x00007F14
		public static LocalizedString StatusPending
		{
			get
			{
				return new LocalizedString("StatusPending", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00009D32 File Offset: 0x00007F32
		public static LocalizedString TimeZoneMidAtlanticStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneMidAtlanticStandardTime", "Ex399DFA", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00009D50 File Offset: 0x00007F50
		public static LocalizedString RoleTypeDescription_ResetPassword
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ResetPassword", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009D70 File Offset: 0x00007F70
		public static LocalizedString NoValidDomainNameExistsInDomainScopedRule(int ruleId)
		{
			return new LocalizedString("NoValidDomainNameExistsInDomainScopedRule", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00009DA4 File Offset: 0x00007FA4
		public static LocalizedString InvalidRecipientForAdmin(string recipient)
		{
			return new LocalizedString("InvalidRecipientForAdmin", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				recipient
			});
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00009DD3 File Offset: 0x00007FD3
		public static LocalizedString ExchangeUMManagementDescription
		{
			get
			{
				return new LocalizedString("ExchangeUMManagementDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00009DF1 File Offset: 0x00007FF1
		public static LocalizedString TimeZoneUTC
		{
			get
			{
				return new LocalizedString("TimeZoneUTC", "ExD2D4A1", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00009E0F File Offset: 0x0000800F
		public static LocalizedString RoleTypeDescription_DataLossPrevention
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_DataLossPrevention", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00009E2D File Offset: 0x0000802D
		public static LocalizedString EventPendingModerationIW
		{
			get
			{
				return new LocalizedString("EventPendingModerationIW", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00009E4B File Offset: 0x0000804B
		public static LocalizedString RoleTypeDescription_MyCustomApps
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyCustomApps", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00009E69 File Offset: 0x00008069
		public static LocalizedString RoleTypeDescription_DatabaseAvailabilityGroups
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_DatabaseAvailabilityGroups", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00009E87 File Offset: 0x00008087
		public static LocalizedString ExchangeHelpDeskDescription
		{
			get
			{
				return new LocalizedString("ExchangeHelpDeskDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00009EA5 File Offset: 0x000080A5
		public static LocalizedString MessageDirectionReceived
		{
			get
			{
				return new LocalizedString("MessageDirectionReceived", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00009EC3 File Offset: 0x000080C3
		public static LocalizedString SpamQuarantineMigrationSucceeded
		{
			get
			{
				return new LocalizedString("SpamQuarantineMigrationSucceeded", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00009EE1 File Offset: 0x000080E1
		public static LocalizedString EventFailedModerationIW
		{
			get
			{
				return new LocalizedString("EventFailedModerationIW", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00009EFF File Offset: 0x000080FF
		public static LocalizedString TimeZoneUTC02
		{
			get
			{
				return new LocalizedString("TimeZoneUTC02", "ExAD5C35", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00009F1D File Offset: 0x0000811D
		public static LocalizedString AntimalwareScopingConstraint
		{
			get
			{
				return new LocalizedString("AntimalwareScopingConstraint", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00009F3B File Offset: 0x0000813B
		public static LocalizedString DeliveryStatusFailed
		{
			get
			{
				return new LocalizedString("DeliveryStatusFailed", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00009F59 File Offset: 0x00008159
		public static LocalizedString QuarantineInbound
		{
			get
			{
				return new LocalizedString("QuarantineInbound", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009F78 File Offset: 0x00008178
		public static LocalizedString NoValidIpRangesInFopeRule(int ruleId)
		{
			return new LocalizedString("NoValidIpRangesInFopeRule", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009FAC File Offset: 0x000081AC
		public static LocalizedString CompressionError(int errCode)
		{
			return new LocalizedString("CompressionError", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				errCode
			});
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00009FE0 File Offset: 0x000081E0
		public static LocalizedString RoleTypeDescription_Throttling
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_Throttling", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00009FFE File Offset: 0x000081FE
		public static LocalizedString RoleTypeDescription_DataCenterDestructiveOperations
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_DataCenterDestructiveOperations", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000A01C File Offset: 0x0000821C
		public static LocalizedString RoleTypeDescription_AddressLists
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_AddressLists", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000A03A File Offset: 0x0000823A
		public static LocalizedString RoleTypeDescription_CentralAdminManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_CentralAdminManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000A058 File Offset: 0x00008258
		public static LocalizedString TimeZoneAfghanistanStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneAfghanistanStandardTime", "Ex611A94", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000A076 File Offset: 0x00008276
		public static LocalizedString EventTransferredToForeignOrgIW
		{
			get
			{
				return new LocalizedString("EventTransferredToForeignOrgIW", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000A094 File Offset: 0x00008294
		public static LocalizedString AttachmentExtensionContainsInvalidCharacters(int FopePolicyRuleId, string extension)
		{
			return new LocalizedString("AttachmentExtensionContainsInvalidCharacters", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				FopePolicyRuleId,
				extension
			});
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000A0CC File Offset: 0x000082CC
		public static LocalizedString JobStatusCancelled
		{
			get
			{
				return new LocalizedString("JobStatusCancelled", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A0EC File Offset: 0x000082EC
		public static LocalizedString SubjectCaseSensitive(string subject)
		{
			return new LocalizedString("SubjectCaseSensitive", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				subject
			});
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000A11B File Offset: 0x0000831B
		public static LocalizedString TimeZoneAtlanticStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneAtlanticStandardTime", "Ex611C88", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000A139 File Offset: 0x00008339
		public static LocalizedString TimeZoneArabStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneArabStandardTime", "ExBDF984", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000A157 File Offset: 0x00008357
		public static LocalizedString RoleTypeDescription_MailRecipients
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MailRecipients", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000A175 File Offset: 0x00008375
		public static LocalizedString RoleTypeDescription_WorkloadManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_WorkloadManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000A193 File Offset: 0x00008393
		public static LocalizedString TimeZoneAlaskanStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneAlaskanStandardTime", "ExE1F137", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000A1B4 File Offset: 0x000083B4
		public static LocalizedString TrackingSearchException(LocalizedString reason)
		{
			return new LocalizedString("TrackingSearchException", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000A1E8 File Offset: 0x000083E8
		public static LocalizedString MsoManagedTenantHelpdeskGroupDescription
		{
			get
			{
				return new LocalizedString("MsoManagedTenantHelpdeskGroupDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000A208 File Offset: 0x00008408
		public static LocalizedString DecompressionError(int errCode)
		{
			return new LocalizedString("DecompressionError", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				errCode
			});
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000A23C File Offset: 0x0000843C
		public static LocalizedString AntimalwareInboundRecipientNotifications
		{
			get
			{
				return new LocalizedString("AntimalwareInboundRecipientNotifications", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000A25A File Offset: 0x0000845A
		public static LocalizedString TestXHeaderAndModifySubject
		{
			get
			{
				return new LocalizedString("TestXHeaderAndModifySubject", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000A278 File Offset: 0x00008478
		public static LocalizedString ContentIndexStatusFailedAndSuspended
		{
			get
			{
				return new LocalizedString("ContentIndexStatusFailedAndSuspended", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000A296 File Offset: 0x00008496
		public static LocalizedString MsoManagedTenantAdminGroupDescription
		{
			get
			{
				return new LocalizedString("MsoManagedTenantAdminGroupDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000A2B4 File Offset: 0x000084B4
		public static LocalizedString RoleTypeDescription_DataCenterOperations
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_DataCenterOperations", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000A2D2 File Offset: 0x000084D2
		public static LocalizedString EventModerationExpired
		{
			get
			{
				return new LocalizedString("EventModerationExpired", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000A2F0 File Offset: 0x000084F0
		public static LocalizedString DomainLevelAdminNotSupportedByEOP(string userName, string roleNames)
		{
			return new LocalizedString("DomainLevelAdminNotSupportedByEOP", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				userName,
				roleNames
			});
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000A323 File Offset: 0x00008523
		public static LocalizedString TraceLevelMedium
		{
			get
			{
				return new LocalizedString("TraceLevelMedium", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000A341 File Offset: 0x00008541
		public static LocalizedString RoleTypeDescription_MoveMailboxes
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MoveMailboxes", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000A35F File Offset: 0x0000855F
		public static LocalizedString RoleTypeDescription_MailRecipientCreation
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MailRecipientCreation", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000A37D File Offset: 0x0000857D
		public static LocalizedString TimeZoneLineIslandsStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneLineIslandsStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A39B File Offset: 0x0000859B
		public static LocalizedString MissingIdentityParameter
		{
			get
			{
				return new LocalizedString("MissingIdentityParameter", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A3B9 File Offset: 0x000085B9
		public static LocalizedString ContentIndexStatusHealthyAndUpgrading
		{
			get
			{
				return new LocalizedString("ContentIndexStatusHealthyAndUpgrading", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000A3D8 File Offset: 0x000085D8
		public static LocalizedString EventTransferredToLegacyExchangeServerHelpDesk(string local, string remote)
		{
			return new LocalizedString("EventTransferredToLegacyExchangeServerHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				local,
				remote
			});
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A40B File Offset: 0x0000860B
		public static LocalizedString TrafficScopeInbound
		{
			get
			{
				return new LocalizedString("TrafficScopeInbound", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000A429 File Offset: 0x00008629
		public static LocalizedString RoleTypeDescription_UnScopedRoleManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_UnScopedRoleManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A448 File Offset: 0x00008648
		public static LocalizedString AntimalwareAdminAddressValidationsScoped(string invalidSmtpAddress, string policyName)
		{
			return new LocalizedString("AntimalwareAdminAddressValidationsScoped", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				invalidSmtpAddress,
				policyName
			});
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000A47B File Offset: 0x0000867B
		public static LocalizedString AuditSeverityLevelDoNotAudit
		{
			get
			{
				return new LocalizedString("AuditSeverityLevelDoNotAudit", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A499 File Offset: 0x00008699
		public static LocalizedString ExchangeHygieneManagementDescription
		{
			get
			{
				return new LocalizedString("ExchangeHygieneManagementDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000A4B7 File Offset: 0x000086B7
		public static LocalizedString ClassIdExtensions
		{
			get
			{
				return new LocalizedString("ClassIdExtensions", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000A4D5 File Offset: 0x000086D5
		public static LocalizedString TimeZoneNamibiaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneNamibiaStandardTime", "ExEBD3D6", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000A4F3 File Offset: 0x000086F3
		public static LocalizedString RejectedExplanationContentFiltering
		{
			get
			{
				return new LocalizedString("RejectedExplanationContentFiltering", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000A511 File Offset: 0x00008711
		public static LocalizedString QuarantineOutbound
		{
			get
			{
				return new LocalizedString("QuarantineOutbound", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000A52F File Offset: 0x0000872F
		public static LocalizedString RunWeekly
		{
			get
			{
				return new LocalizedString("RunWeekly", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000A54D File Offset: 0x0000874D
		public static LocalizedString TimeZoneAltaiStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneAltaiStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000A56B File Offset: 0x0000876B
		public static LocalizedString EventSubmittedCrossSite
		{
			get
			{
				return new LocalizedString("EventSubmittedCrossSite", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000A589 File Offset: 0x00008789
		public static LocalizedString TimeZoneEasternStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneEasternStandardTime", "Ex469AF3", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A5A8 File Offset: 0x000087A8
		public static LocalizedString DisabledOutboundConnector(string name)
		{
			return new LocalizedString("DisabledOutboundConnector", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A5D8 File Offset: 0x000087D8
		public static LocalizedString BodyCondition(int ruleId)
		{
			return new LocalizedString("BodyCondition", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000A60C File Offset: 0x0000880C
		public static LocalizedString TimeZoneAzerbaijanStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneAzerbaijanStandardTime", "ExDED1C0", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A62C File Offset: 0x0000882C
		public static LocalizedString FopePolicyRuleContainsIncompatibleConditions(int ruleId)
		{
			return new LocalizedString("FopePolicyRuleContainsIncompatibleConditions", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A660 File Offset: 0x00008860
		public static LocalizedString PlusUnknownTimeZone(int hour, int minute)
		{
			return new LocalizedString("PlusUnknownTimeZone", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				hour,
				minute
			});
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000A69D File Offset: 0x0000889D
		public static LocalizedString TimeZonePakistanStandardTime
		{
			get
			{
				return new LocalizedString("TimeZonePakistanStandardTime", "ExC176EA", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A6BB File Offset: 0x000088BB
		public static LocalizedString RoleTypeDescription_OrgMarketplaceApps
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_OrgMarketplaceApps", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000A6D9 File Offset: 0x000088D9
		public static LocalizedString TimeZonePacificSAStandardTime
		{
			get
			{
				return new LocalizedString("TimeZonePacificSAStandardTime", "Ex16C4A1", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000A6F7 File Offset: 0x000088F7
		public static LocalizedString TimeZoneRussianStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneRussianStandardTime", "Ex5D4EC1", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000A715 File Offset: 0x00008915
		public static LocalizedString RoleTypeDescription_POP3AndIMAP4Protocols
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_POP3AndIMAP4Protocols", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A734 File Offset: 0x00008934
		public static LocalizedString InvalidAttachmentExtensionCondition(int ruleId)
		{
			return new LocalizedString("InvalidAttachmentExtensionCondition", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000A768 File Offset: 0x00008968
		public static LocalizedString TimeZoneTaipeiStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneTaipeiStandardTime", "ExF4738A", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000A786 File Offset: 0x00008986
		public static LocalizedString RoleTypeDescription_HelpdeskRecipientManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_HelpdeskRecipientManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000A7A4 File Offset: 0x000089A4
		public static LocalizedString TimeZoneHawaiianStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneHawaiianStandardTime", "ExD9A3CB", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000A7C2 File Offset: 0x000089C2
		public static LocalizedString TimeZoneMagallanesStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneMagallanesStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000A7E0 File Offset: 0x000089E0
		public static LocalizedString EventFailedTransportRulesHelpDesk
		{
			get
			{
				return new LocalizedString("EventFailedTransportRulesHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000A7FE File Offset: 0x000089FE
		public static LocalizedString TimeZoneTokyoStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneTokyoStandardTime", "ExB8F00C", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000A81C File Offset: 0x00008A1C
		public static LocalizedString DeliveryStatusAll
		{
			get
			{
				return new LocalizedString("DeliveryStatusAll", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000A83A File Offset: 0x00008A3A
		public static LocalizedString RoleTypeDescription_MyOptions
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyOptions", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A858 File Offset: 0x00008A58
		public static LocalizedString FopePolicyRuleHasProhibitedRegularExpressions(int ruleId, string reason)
		{
			return new LocalizedString("FopePolicyRuleHasProhibitedRegularExpressions", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId,
				reason
			});
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000A890 File Offset: 0x00008A90
		public static LocalizedString RoleTypeDescription_DisasterRecovery
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_DisasterRecovery", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000A8AE File Offset: 0x00008AAE
		public static LocalizedString RoleTypeDescription_EmailAddressPolicies
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_EmailAddressPolicies", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A8CC File Offset: 0x00008ACC
		public static LocalizedString HeaderValueMatch(string headerValue)
		{
			return new LocalizedString("HeaderValueMatch", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				headerValue
			});
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A8FB File Offset: 0x00008AFB
		public static LocalizedString RoleTypeDescription_SendConnectors
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_SendConnectors", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A91C File Offset: 0x00008B1C
		public static LocalizedString FopePolicyRuleSummary(int RuleId, string trafficScope, string domainScope, string action, string details)
		{
			return new LocalizedString("FopePolicyRuleSummary", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				RuleId,
				trafficScope,
				domainScope,
				action,
				details
			});
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A964 File Offset: 0x00008B64
		public static LocalizedString BCC(string to)
		{
			return new LocalizedString("BCC", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				to
			});
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000A993 File Offset: 0x00008B93
		public static LocalizedString TimeZoneSudanStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneSudanStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000A9B1 File Offset: 0x00008BB1
		public static LocalizedString EventRead
		{
			get
			{
				return new LocalizedString("EventRead", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000A9CF File Offset: 0x00008BCF
		public static LocalizedString TimeZoneGreenlandStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneGreenlandStandardTime", "ExDE17E6", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000A9ED File Offset: 0x00008BED
		public static LocalizedString RoleTypeDescription_OutlookProvider
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_OutlookProvider", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000AA0B File Offset: 0x00008C0B
		public static LocalizedString EventTransferredIntermediate
		{
			get
			{
				return new LocalizedString("EventTransferredIntermediate", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000AA29 File Offset: 0x00008C29
		public static LocalizedString RoleTypeDescription_OrgCustomApps
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_OrgCustomApps", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000AA47 File Offset: 0x00008C47
		public static LocalizedString TimeZoneBangladeshStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneBangladeshStandardTime", "ExF23691", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000AA68 File Offset: 0x00008C68
		public static LocalizedString BodyExactMatch(string body)
		{
			return new LocalizedString("BodyExactMatch", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				body
			});
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000AA97 File Offset: 0x00008C97
		public static LocalizedString TimeZoneGMTStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneGMTStandardTime", "Ex35C612", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000AAB5 File Offset: 0x00008CB5
		public static LocalizedString TimeZoneGTBStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneGTBStandardTime", "Ex96BA20", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000AAD3 File Offset: 0x00008CD3
		public static LocalizedString JobStatusNotStarted
		{
			get
			{
				return new LocalizedString("JobStatusNotStarted", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000AAF4 File Offset: 0x00008CF4
		public static LocalizedString TenantSkuNotSupported(string skuName)
		{
			return new LocalizedString("TenantSkuNotSupported", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				skuName
			});
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000AB23 File Offset: 0x00008D23
		public static LocalizedString TrackingWarningNoResultsDueToLogsNotFound
		{
			get
			{
				return new LocalizedString("TrackingWarningNoResultsDueToLogsNotFound", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000AB44 File Offset: 0x00008D44
		public static LocalizedString SubjectExactMatch(string subject)
		{
			return new LocalizedString("SubjectExactMatch", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				subject
			});
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000AB73 File Offset: 0x00008D73
		public static LocalizedString TimeZoneSAWesternStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneSAWesternStandardTime", "ExCD3627", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000AB94 File Offset: 0x00008D94
		public static LocalizedString FopePolicyRuleIsPartialMessage(int ruleId)
		{
			return new LocalizedString("FopePolicyRuleIsPartialMessage", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		public static LocalizedString TimeZoneHaitiStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneHaitiStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000ABE6 File Offset: 0x00008DE6
		public static LocalizedString TrackingErrorFailedToInitialize
		{
			get
			{
				return new LocalizedString("TrackingErrorFailedToInitialize", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000AC04 File Offset: 0x00008E04
		public static LocalizedString TimeZoneKamchatkaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneKamchatkaStandardTime", "ExD1FCF5", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000AC22 File Offset: 0x00008E22
		public static LocalizedString TrackingWarningResultsMissingTransient
		{
			get
			{
				return new LocalizedString("TrackingWarningResultsMissingTransient", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000AC40 File Offset: 0x00008E40
		public static LocalizedString RulesMerged
		{
			get
			{
				return new LocalizedString("RulesMerged", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000AC60 File Offset: 0x00008E60
		public static LocalizedString AntimalwareVirtualDomainFailure(string invalidVirtualDomain)
		{
			return new LocalizedString("AntimalwareVirtualDomainFailure", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				invalidVirtualDomain
			});
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000AC8F File Offset: 0x00008E8F
		public static LocalizedString RoleTypeDescription_MyRetentionPolicies
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyRetentionPolicies", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000ACAD File Offset: 0x00008EAD
		public static LocalizedString DomainScopedRulesMerged
		{
			get
			{
				return new LocalizedString("DomainScopedRulesMerged", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000ACCB File Offset: 0x00008ECB
		public static LocalizedString EventResolvedHelpDesk
		{
			get
			{
				return new LocalizedString("EventResolvedHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000ACE9 File Offset: 0x00008EE9
		public static LocalizedString RoleTypeDescription_LegalHoldApplication
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_LegalHoldApplication", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000AD07 File Offset: 0x00008F07
		public static LocalizedString TimeZoneSingaporeStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneSingaporeStandardTime", "Ex85CE3C", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000AD25 File Offset: 0x00008F25
		public static LocalizedString AntimalwareAdminAddressNull
		{
			get
			{
				return new LocalizedString("AntimalwareAdminAddressNull", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000AD43 File Offset: 0x00008F43
		public static LocalizedString RoleTypeDescription_UserApplication
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_UserApplication", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000AD64 File Offset: 0x00008F64
		public static LocalizedString InboundConnectorWithoutSenderIPsAndCert(string name)
		{
			return new LocalizedString("InboundConnectorWithoutSenderIPsAndCert", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000AD93 File Offset: 0x00008F93
		public static LocalizedString RoleTypeDescription_MyContactInformation
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyContactInformation", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000ADB1 File Offset: 0x00008FB1
		public static LocalizedString TimeZoneNorthAsiaEastStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneNorthAsiaEastStandardTime", "ExA847F9", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000ADCF File Offset: 0x00008FCF
		public static LocalizedString ExchangeRecipientAdminDescription
		{
			get
			{
				return new LocalizedString("ExchangeRecipientAdminDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000ADED File Offset: 0x00008FED
		public static LocalizedString TimeZoneCapeVerdeStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneCapeVerdeStandardTime", "ExEBA757", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000AE0B File Offset: 0x0000900B
		public static LocalizedString TrackingSearchNotAuthorized
		{
			get
			{
				return new LocalizedString("TrackingSearchNotAuthorized", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000AE29 File Offset: 0x00009029
		public static LocalizedString ContentIndexStatusCrawling
		{
			get
			{
				return new LocalizedString("ContentIndexStatusCrawling", "ExBFB72B", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000AE47 File Offset: 0x00009047
		public static LocalizedString TimeZoneMagadanStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneMagadanStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000AE65 File Offset: 0x00009065
		public static LocalizedString TimeZoneAUSCentralStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneAUSCentralStandardTime", "Ex3FEDDD", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000AE83 File Offset: 0x00009083
		public static LocalizedString ExchangeDiscoveryManagementDescription
		{
			get
			{
				return new LocalizedString("ExchangeDiscoveryManagementDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000AEA1 File Offset: 0x000090A1
		public static LocalizedString EventMessageDefer
		{
			get
			{
				return new LocalizedString("EventMessageDefer", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000AEBF File Offset: 0x000090BF
		public static LocalizedString TimeZoneVolgogradStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneVolgogradStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000AEE0 File Offset: 0x000090E0
		public static LocalizedString FilenameWordMatchCondition(int ruleId)
		{
			return new LocalizedString("FilenameWordMatchCondition", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000AF14 File Offset: 0x00009114
		public static LocalizedString TimeZonePacificStandardTimeMexico
		{
			get
			{
				return new LocalizedString("TimeZonePacificStandardTimeMexico", "Ex7BA959", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000AF34 File Offset: 0x00009134
		public static LocalizedString AttachmentFilenames(string attachmentFilenames)
		{
			return new LocalizedString("AttachmentFilenames", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				attachmentFilenames
			});
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000AF63 File Offset: 0x00009163
		public static LocalizedString TimeZoneSamoaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneSamoaStandardTime", "Ex02633F", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000AF81 File Offset: 0x00009181
		public static LocalizedString RoleTypeDescription_PersonallyIdentifiableInformation
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_PersonallyIdentifiableInformation", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000AF9F File Offset: 0x0000919F
		public static LocalizedString TimeZoneUTC12
		{
			get
			{
				return new LocalizedString("TimeZoneUTC12", "ExEEDEA2", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000AFBD File Offset: 0x000091BD
		public static LocalizedString RoleTypeDescription_MessageTracking
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MessageTracking", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000AFDB File Offset: 0x000091DB
		public static LocalizedString TraceLevelLow
		{
			get
			{
				return new LocalizedString("TraceLevelLow", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000AFF9 File Offset: 0x000091F9
		public static LocalizedString RoleTypeDescription_MyTextMessaging
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyTextMessaging", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000B017 File Offset: 0x00009217
		public static LocalizedString RoleTypeDescription_RecipientPolicies
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_RecipientPolicies", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000B035 File Offset: 0x00009235
		public static LocalizedString TimeZoneSAEasternStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneSAEasternStandardTime", "Ex2B0B5B", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000B054 File Offset: 0x00009254
		public static LocalizedString EventSubmittedCrossSiteHelpDesk(string hubServerFqdn)
		{
			return new LocalizedString("EventSubmittedCrossSiteHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				hubServerFqdn
			});
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B083 File Offset: 0x00009283
		public static LocalizedString RoleTypeDescription_TeamMailboxLifecycleApplication
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_TeamMailboxLifecycleApplication", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000B0A4 File Offset: 0x000092A4
		public static LocalizedString InboundFopePolicyRuleWithDuplicateDomainName(int ruleId)
		{
			return new LocalizedString("InboundFopePolicyRuleWithDuplicateDomainName", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000B0D8 File Offset: 0x000092D8
		public static LocalizedString InvalidSecondaryEmailAddresses(string userName)
		{
			return new LocalizedString("InvalidSecondaryEmailAddresses", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000B107 File Offset: 0x00009307
		public static LocalizedString InvalidMessageTrackingReportId
		{
			get
			{
				return new LocalizedString("InvalidMessageTrackingReportId", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000B125 File Offset: 0x00009325
		public static LocalizedString TimeZoneIndiaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneIndiaStandardTime", "ExEB2050", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B144 File Offset: 0x00009344
		public static LocalizedString DistributionListExpanded(int ruleId, string distributionList)
		{
			return new LocalizedString("DistributionListExpanded", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId,
				distributionList
			});
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000B17C File Offset: 0x0000937C
		public static LocalizedString ContentIndexStatusFailed
		{
			get
			{
				return new LocalizedString("ContentIndexStatusFailed", "Ex0C6286", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000B19A File Offset: 0x0000939A
		public static LocalizedString TimeZoneNewfoundlandStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneNewfoundlandStandardTime", "ExCAC12E", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000B1B8 File Offset: 0x000093B8
		public static LocalizedString TimeZoneYakutskStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneYakutskStandardTime", "ExD95910", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000B1D8 File Offset: 0x000093D8
		public static LocalizedString NoValidRecipientDomainNameExistsInRecipientDomainCondition(int ruleId)
		{
			return new LocalizedString("NoValidRecipientDomainNameExistsInRecipientDomainCondition", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000B20C File Offset: 0x0000940C
		public static LocalizedString RoleTypeDescription_OrganizationHelpSettings
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_OrganizationHelpSettings", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B22C File Offset: 0x0000942C
		public static LocalizedString FopePolicyConsolidationList(string ruleName, string fopeIds)
		{
			return new LocalizedString("FopePolicyConsolidationList", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleName,
				fopeIds
			});
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000B25F File Offset: 0x0000945F
		public static LocalizedString EventFailedModerationHelpDesk
		{
			get
			{
				return new LocalizedString("EventFailedModerationHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000B27D File Offset: 0x0000947D
		public static LocalizedString RoleTypeDescription_PublicFolders
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_PublicFolders", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000B29B File Offset: 0x0000949B
		public static LocalizedString TimeZonePacificStandardTime
		{
			get
			{
				return new LocalizedString("TimeZonePacificStandardTime", "ExC6C6F0", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000B2B9 File Offset: 0x000094B9
		public static LocalizedString TimeZoneAleutianStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneAleutianStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000B2D7 File Offset: 0x000094D7
		public static LocalizedString ExchangeManagementForestTier1SupportDescription
		{
			get
			{
				return new LocalizedString("ExchangeManagementForestTier1SupportDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000B2F5 File Offset: 0x000094F5
		public static LocalizedString TimeZoneNorthKoreaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneNorthKoreaStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000B313 File Offset: 0x00009513
		public static LocalizedString DltUnknownTimeZone
		{
			get
			{
				return new LocalizedString("DltUnknownTimeZone", "Ex6DAA38", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000B331 File Offset: 0x00009531
		public static LocalizedString RoleTypeDescription_ExchangeConnectors
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ExchangeConnectors", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000B34F File Offset: 0x0000954F
		public static LocalizedString RoleTypeDescription_ViewOnlyCentralAdminSupport
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ViewOnlyCentralAdminSupport", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000B36D File Offset: 0x0000956D
		public static LocalizedString TimeZoneNorfolkStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneNorfolkStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000B38B File Offset: 0x0000958B
		public static LocalizedString RoleTypeDescription_MyProfileInformation
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyProfileInformation", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000B3A9 File Offset: 0x000095A9
		public static LocalizedString TimeZoneSaoTomeStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneSaoTomeStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000B3C8 File Offset: 0x000095C8
		public static LocalizedString AttachmentExtensions(string attachmentExtensions)
		{
			return new LocalizedString("AttachmentExtensions", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				attachmentExtensions
			});
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000B3F7 File Offset: 0x000095F7
		public static LocalizedString RoleTypeDescription_Journaling
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_Journaling", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000B415 File Offset: 0x00009615
		public static LocalizedString TimeZoneAzoresStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneAzoresStandardTime", "Ex15D215", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000B433 File Offset: 0x00009633
		public static LocalizedString TimeZoneAusCentralWStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneAusCentralWStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000B451 File Offset: 0x00009651
		public static LocalizedString TimeZoneUSMountainStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneUSMountainStandardTime", "Ex48C151", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000B46F File Offset: 0x0000966F
		public static LocalizedString RoleTypeDescription_ExchangeCrossServiceIntegration
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ExchangeCrossServiceIntegration", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000B48D File Offset: 0x0000968D
		public static LocalizedString PolicyMigrationSucceeded
		{
			get
			{
				return new LocalizedString("PolicyMigrationSucceeded", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000B4AB File Offset: 0x000096AB
		public static LocalizedString TimeZoneCentralBrazilianStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneCentralBrazilianStandardTime", "Ex10A588", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000B4C9 File Offset: 0x000096C9
		public static LocalizedString EventRulesCc
		{
			get
			{
				return new LocalizedString("EventRulesCc", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000B4E8 File Offset: 0x000096E8
		public static LocalizedString MaximumRecipientNumber(int maxRecipients)
		{
			return new LocalizedString("MaximumRecipientNumber", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				maxRecipients
			});
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000B51C File Offset: 0x0000971C
		public static LocalizedString EventDeliveredInboxRule(string folderName)
		{
			return new LocalizedString("EventDeliveredInboxRule", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000B54B File Offset: 0x0000974B
		public static LocalizedString ComplianceManagementGroupDescription
		{
			get
			{
				return new LocalizedString("ComplianceManagementGroupDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000B569 File Offset: 0x00009769
		public static LocalizedString TimeZoneWCentralAfricaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneWCentralAfricaStandardTime", "Ex87B651", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000B587 File Offset: 0x00009787
		public static LocalizedString RoleTypeDescription_UMMailboxes
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_UMMailboxes", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000B5A5 File Offset: 0x000097A5
		public static LocalizedString ExchangeAllHostedOrgsDescription
		{
			get
			{
				return new LocalizedString("ExchangeAllHostedOrgsDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000B5C3 File Offset: 0x000097C3
		public static LocalizedString RoleTypeDescription_DistributionGroups
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_DistributionGroups", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B5E1 File Offset: 0x000097E1
		public static LocalizedString ExchangeDelegatedSetupDescription
		{
			get
			{
				return new LocalizedString("ExchangeDelegatedSetupDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000B5FF File Offset: 0x000097FF
		public static LocalizedString TimeZoneVladivostokStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneVladivostokStandardTime", "Ex15317C", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000B61D File Offset: 0x0000981D
		public static LocalizedString TimeZoneCentralStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneCentralStandardTime", "ExFA79C8", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000B63B File Offset: 0x0000983B
		public static LocalizedString RoleTypeDescription_OrganizationClientAccess
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_OrganizationClientAccess", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000B659 File Offset: 0x00009859
		public static LocalizedString ExchangeViewOnlyAdminDescription
		{
			get
			{
				return new LocalizedString("ExchangeViewOnlyAdminDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000B677 File Offset: 0x00009877
		public static LocalizedString TimeZoneEAustraliaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneEAustraliaStandardTime", "ExF6E116", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000B698 File Offset: 0x00009898
		public static LocalizedString DomainScopedRuleContainsInvalidDomainNames(int ruleId, string domainNames)
		{
			return new LocalizedString("DomainScopedRuleContainsInvalidDomainNames", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId,
				domainNames
			});
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000B6D0 File Offset: 0x000098D0
		public static LocalizedString InvalidDomainNameInConnectorSetting(string invalidDomainName)
		{
			return new LocalizedString("InvalidDomainNameInConnectorSetting", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				invalidDomainName
			});
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000B700 File Offset: 0x00009900
		public static LocalizedString OutboundDomainScopedConnectorsMigrated(string connectors)
		{
			return new LocalizedString("OutboundDomainScopedConnectorsMigrated", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				connectors
			});
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000B730 File Offset: 0x00009930
		public static LocalizedString TenantSkuFilteringNotSupported(string skuName)
		{
			return new LocalizedString("TenantSkuFilteringNotSupported", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				skuName
			});
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000B760 File Offset: 0x00009960
		public static LocalizedString FopePolicyRuleHasWordsThatExceedMaximumLength(int ruleId, int maxWordLength)
		{
			return new LocalizedString("FopePolicyRuleHasWordsThatExceedMaximumLength", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId,
				maxWordLength
			});
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000B7A0 File Offset: 0x000099A0
		public static LocalizedString MinusUnknownTimeZone(int hour, int minute)
		{
			return new LocalizedString("MinusUnknownTimeZone", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				hour,
				minute
			});
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000B7E0 File Offset: 0x000099E0
		public static LocalizedString AntimalwareTruncation(string longMessage, string domainName)
		{
			return new LocalizedString("AntimalwareTruncation", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				longMessage,
				domainName
			});
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000B813 File Offset: 0x00009A13
		public static LocalizedString TrackingTransientError
		{
			get
			{
				return new LocalizedString("TrackingTransientError", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B831 File Offset: 0x00009A31
		public static LocalizedString TrafficScopeInboundAndOutbound
		{
			get
			{
				return new LocalizedString("TrafficScopeInboundAndOutbound", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000B850 File Offset: 0x00009A50
		public static LocalizedString ExamineArchivesProperty(int ruleId)
		{
			return new LocalizedString("ExamineArchivesProperty", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000B884 File Offset: 0x00009A84
		public static LocalizedString RoleTypeDescription_CentralAdminCredentialManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_CentralAdminCredentialManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000B8A2 File Offset: 0x00009AA2
		public static LocalizedString RoleTypeDescription_UnScoped
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_UnScoped", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000B8C0 File Offset: 0x00009AC0
		public static LocalizedString RoleTypeDescription_MyDistributionGroupMembership
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyDistributionGroupMembership", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000B8DE File Offset: 0x00009ADE
		public static LocalizedString RoleTypeDescription_OfficeExtensionApplication
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_OfficeExtensionApplication", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000B8FC File Offset: 0x00009AFC
		public static LocalizedString RoleTypeDescription_DistributionGroupManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_DistributionGroupManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000B91A File Offset: 0x00009B1A
		public static LocalizedString AntimalwareMigrationSucceeded
		{
			get
			{
				return new LocalizedString("AntimalwareMigrationSucceeded", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B938 File Offset: 0x00009B38
		public static LocalizedString TimeZoneWEuropeStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneWEuropeStandardTime", "ExB33E7A", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000B956 File Offset: 0x00009B56
		public static LocalizedString RoleTypeDescription_AuditLogs
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_AuditLogs", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000B974 File Offset: 0x00009B74
		public static LocalizedString EventSubmitted
		{
			get
			{
				return new LocalizedString("EventSubmitted", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000B992 File Offset: 0x00009B92
		public static LocalizedString TimeZoneEasternStandardTimeMexico
		{
			get
			{
				return new LocalizedString("TimeZoneEasternStandardTimeMexico", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		public static LocalizedString TimeZoneRomanceStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneRomanceStandardTime", "Ex5E5458", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		public static LocalizedString InvalidDomainNameInHipaaDomainSetting(string invalidDomainName)
		{
			return new LocalizedString("InvalidDomainNameInHipaaDomainSetting", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				invalidDomainName
			});
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000BA00 File Offset: 0x00009C00
		public static LocalizedString UnableToAddUserToDistributionGroup(string user, string dg, string error)
		{
			return new LocalizedString("UnableToAddUserToDistributionGroup", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				user,
				dg,
				error
			});
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000BA37 File Offset: 0x00009C37
		public static LocalizedString TimeZoneIranStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneIranStandardTime", "Ex5D6DCC", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000BA55 File Offset: 0x00009C55
		public static LocalizedString RoleTypeDescription_AccessToCustomerDataDCOnly
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_AccessToCustomerDataDCOnly", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000BA74 File Offset: 0x00009C74
		public static LocalizedString Redirect(string to)
		{
			return new LocalizedString("Redirect", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				to
			});
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000BAA3 File Offset: 0x00009CA3
		public static LocalizedString InvalidIdentityForAdmin
		{
			get
			{
				return new LocalizedString("InvalidIdentityForAdmin", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000BAC1 File Offset: 0x00009CC1
		public static LocalizedString TimeZoneMarquesasStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneMarquesasStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000BADF File Offset: 0x00009CDF
		public static LocalizedString ContentIndexStatusHealthy
		{
			get
			{
				return new LocalizedString("ContentIndexStatusHealthy", "ExAE6F15", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000BAFD File Offset: 0x00009CFD
		public static LocalizedString RunMonthly
		{
			get
			{
				return new LocalizedString("RunMonthly", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000BB1B File Offset: 0x00009D1B
		public static LocalizedString RoleTypeDescription_OrganizationTransportSettings
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_OrganizationTransportSettings", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000BB39 File Offset: 0x00009D39
		public static LocalizedString RoleTypeDescription_ViewOnlyConfiguration
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ViewOnlyConfiguration", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000BB58 File Offset: 0x00009D58
		public static LocalizedString HipaaPolicyMigrated(string domains)
		{
			return new LocalizedString("HipaaPolicyMigrated", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				domains
			});
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000BB87 File Offset: 0x00009D87
		public static LocalizedString TimeZoneMountainStandardTimeMexico
		{
			get
			{
				return new LocalizedString("TimeZoneMountainStandardTimeMexico", "Ex7226F6", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000BBA5 File Offset: 0x00009DA5
		public static LocalizedString RoleTypeDescription_PartnerDelegatedTenantManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_PartnerDelegatedTenantManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		public static LocalizedString EventResolvedWithDetailsHelpDesk(string originalAddress, string resolvedAddress)
		{
			return new LocalizedString("EventResolvedWithDetailsHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				originalAddress,
				resolvedAddress
			});
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000BBF7 File Offset: 0x00009DF7
		public static LocalizedString RoleTypeDescription_OrganizationConfiguration
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_OrganizationConfiguration", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000BC15 File Offset: 0x00009E15
		public static LocalizedString MsoMailTenantAdminGroupDescription
		{
			get
			{
				return new LocalizedString("MsoMailTenantAdminGroupDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000BC33 File Offset: 0x00009E33
		public static LocalizedString TimeZoneSEAsiaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneSEAsiaStandardTime", "ExBCFCCD", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000BC51 File Offset: 0x00009E51
		public static LocalizedString RoleTypeDescription_DatabaseCopies
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_DatabaseCopies", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000BC6F File Offset: 0x00009E6F
		public static LocalizedString TimeZoneCentralStandardTimeMexico
		{
			get
			{
				return new LocalizedString("TimeZoneCentralStandardTimeMexico", "Ex0243BD", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000BC8D File Offset: 0x00009E8D
		public static LocalizedString OutboundIpMigrationCompleted
		{
			get
			{
				return new LocalizedString("OutboundIpMigrationCompleted", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000BCAB File Offset: 0x00009EAB
		public static LocalizedString RecipientPathFilterNeeded
		{
			get
			{
				return new LocalizedString("RecipientPathFilterNeeded", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000BCC9 File Offset: 0x00009EC9
		public static LocalizedString TrackingWarningResultsMissingConnection
		{
			get
			{
				return new LocalizedString("TrackingWarningResultsMissingConnection", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		public static LocalizedString InboundConnectorsWithSpamOrConnectionFilteringDisabledMigrated(string connectors)
		{
			return new LocalizedString("InboundConnectorsWithSpamOrConnectionFilteringDisabledMigrated", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				connectors
			});
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000BD18 File Offset: 0x00009F18
		public static LocalizedString SenderDomainConditionContainsInvalidDomainNames(int ruleId, string domainNames)
		{
			return new LocalizedString("SenderDomainConditionContainsInvalidDomainNames", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId,
				domainNames
			});
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000BD50 File Offset: 0x00009F50
		public static LocalizedString TimeZoneNCentralAsiaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneNCentralAsiaStandardTime", "Ex18B2E7", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000BD6E File Offset: 0x00009F6E
		public static LocalizedString PolicyTipReject
		{
			get
			{
				return new LocalizedString("PolicyTipReject", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000BD8C File Offset: 0x00009F8C
		public static LocalizedString AuditSeverityLevelLow
		{
			get
			{
				return new LocalizedString("AuditSeverityLevelLow", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000BDAA File Offset: 0x00009FAA
		public static LocalizedString RoleTypeDescription_RecipientManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_RecipientManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000BDC8 File Offset: 0x00009FC8
		public static LocalizedString EventForwarded
		{
			get
			{
				return new LocalizedString("EventForwarded", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000BDE6 File Offset: 0x00009FE6
		public static LocalizedString EventApprovedModerationHelpDesk
		{
			get
			{
				return new LocalizedString("EventApprovedModerationHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000BE04 File Offset: 0x0000A004
		public static LocalizedString Reject
		{
			get
			{
				return new LocalizedString("Reject", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000BE22 File Offset: 0x0000A022
		public static LocalizedString TimeZoneSaintPierreStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneSaintPierreStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000BE40 File Offset: 0x0000A040
		public static LocalizedString TimeZoneMoroccoStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneMoroccoStandardTime", "Ex106F0F", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000BE60 File Offset: 0x0000A060
		public static LocalizedString FopePolicyRuleIsSkippableAntiSpamRule(int ruleId)
		{
			return new LocalizedString("FopePolicyRuleIsSkippableAntiSpamRule", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000BE94 File Offset: 0x0000A094
		public static LocalizedString TimeZoneFLEStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneFLEStandardTime", "Ex68ED89", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000BEB2 File Offset: 0x0000A0B2
		public static LocalizedString RoleTypeDescription_MailTips
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MailTips", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		public static LocalizedString RoleTypeDescription_Monitoring
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_Monitoring", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		public static LocalizedString NoValidSenderDomainNameExistsInSenderDomainCondition(int ruleId)
		{
			return new LocalizedString("NoValidSenderDomainNameExistsInSenderDomainCondition", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000BF24 File Offset: 0x0000A124
		public static LocalizedString AntispamEdgeBlockModeSettingNotMigrated
		{
			get
			{
				return new LocalizedString("AntispamEdgeBlockModeSettingNotMigrated", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000BF42 File Offset: 0x0000A142
		public static LocalizedString TimeZoneCanadaCentralStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneCanadaCentralStandardTime", "Ex4FC02A", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000BF60 File Offset: 0x0000A160
		public static LocalizedString ExchangeManagementForestMonitoringDescription
		{
			get
			{
				return new LocalizedString("ExchangeManagementForestMonitoringDescription", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000BF7E File Offset: 0x0000A17E
		public static LocalizedString TimeZoneSakhalinStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneSakhalinStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000BF9C File Offset: 0x0000A19C
		public static LocalizedString EventExpanded(string groupName)
		{
			return new LocalizedString("EventExpanded", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				groupName
			});
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000BFCC File Offset: 0x0000A1CC
		public static LocalizedString FopePolicyRuleHasUnrecognizedAction(int ruleId, int actionId)
		{
			return new LocalizedString("FopePolicyRuleHasUnrecognizedAction", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId,
				actionId
			});
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000C009 File Offset: 0x0000A209
		public static LocalizedString TimeZoneSriLankaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneSriLankaStandardTime", "ExD87835", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000C027 File Offset: 0x0000A227
		public static LocalizedString RoleTypeDescription_AutoDiscover
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_AutoDiscover", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000C045 File Offset: 0x0000A245
		public static LocalizedString TimeZoneRussiaTimeZone10
		{
			get
			{
				return new LocalizedString("TimeZoneRussiaTimeZone10", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000C064 File Offset: 0x0000A264
		public static LocalizedString EventSmtpSendHelpDesk(string local, string remote)
		{
			return new LocalizedString("EventSmtpSendHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				local,
				remote
			});
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000C098 File Offset: 0x0000A298
		public static LocalizedString SenderIpAddresses(string senderIpAddresses)
		{
			return new LocalizedString("SenderIpAddresses", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				senderIpAddresses
			});
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public static LocalizedString EventQueueRetryHelpDesk(string server, string inRetrySinceTime, string lastAttemptTime, string timeZone, string errorMessage)
		{
			return new LocalizedString("EventQueueRetryHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				server,
				inRetrySinceTime,
				lastAttemptTime,
				timeZone,
				errorMessage
			});
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000C108 File Offset: 0x0000A308
		public static LocalizedString EventTransferredToSMSMessage
		{
			get
			{
				return new LocalizedString("EventTransferredToSMSMessage", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000C126 File Offset: 0x0000A326
		public static LocalizedString TrackingWarningNoResultsDueToLogsExpired
		{
			get
			{
				return new LocalizedString("TrackingWarningNoResultsDueToLogsExpired", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000C144 File Offset: 0x0000A344
		public static LocalizedString MessageDirectionSent
		{
			get
			{
				return new LocalizedString("MessageDirectionSent", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000C164 File Offset: 0x0000A364
		public static LocalizedString HeaderNameMatch(string headerName)
		{
			return new LocalizedString("HeaderNameMatch", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				headerName
			});
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000C194 File Offset: 0x0000A394
		public static LocalizedString EventDelayedAfterTransferToPartnerOrgHelpDesk(string partnerOrgDomain)
		{
			return new LocalizedString("EventDelayedAfterTransferToPartnerOrgHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				partnerOrgDomain
			});
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000C1C3 File Offset: 0x0000A3C3
		public static LocalizedString RoleTypeDescription_RecordsManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_RecordsManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000C1E1 File Offset: 0x0000A3E1
		public static LocalizedString RoleTypeDescription_TransportRules
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_TransportRules", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000C1FF File Offset: 0x0000A3FF
		public static LocalizedString ForceTls
		{
			get
			{
				return new LocalizedString("ForceTls", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000C21D File Offset: 0x0000A41D
		public static LocalizedString EventFailedGeneral
		{
			get
			{
				return new LocalizedString("EventFailedGeneral", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000C23B File Offset: 0x0000A43B
		public static LocalizedString RoleTypeDescription_ExchangeServerCertificates
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_ExchangeServerCertificates", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000C259 File Offset: 0x0000A459
		public static LocalizedString EventPending
		{
			get
			{
				return new LocalizedString("EventPending", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000C277 File Offset: 0x0000A477
		public static LocalizedString TimeZoneChinaStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneChinaStandardTime", "ExE7D7D4", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000C295 File Offset: 0x0000A495
		public static LocalizedString TimeZoneWestBankStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneWestBankStandardTime", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000C2B4 File Offset: 0x0000A4B4
		public static LocalizedString Description(string description)
		{
			return new LocalizedString("Description", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				description
			});
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		public static LocalizedString InboundConnectorsWithPolicyFilteringDisabledMigrated(string connectors)
		{
			return new LocalizedString("InboundConnectorsWithPolicyFilteringDisabledMigrated", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				connectors
			});
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000C313 File Offset: 0x0000A513
		public static LocalizedString RoleTypeDescription_MyBaseOptions
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_MyBaseOptions", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000C331 File Offset: 0x0000A531
		public static LocalizedString TimeZoneGreenwichStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneGreenwichStandardTime", "ExB5297B", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000C350 File Offset: 0x0000A550
		public static LocalizedString EventPendingAfterTransferToPartnerOrgHelpDesk(string partnerOrgDomain)
		{
			return new LocalizedString("EventPendingAfterTransferToPartnerOrgHelpDesk", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				partnerOrgDomain
			});
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000C37F File Offset: 0x0000A57F
		public static LocalizedString RoleTypeDescription_UMPromptManagement
		{
			get
			{
				return new LocalizedString("RoleTypeDescription_UMPromptManagement", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000C39D File Offset: 0x0000A59D
		public static LocalizedString AuditSeverityLevelMedium
		{
			get
			{
				return new LocalizedString("AuditSeverityLevelMedium", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000C3BB File Offset: 0x0000A5BB
		public static LocalizedString StatusFilterCannotBeSpecified
		{
			get
			{
				return new LocalizedString("StatusFilterCannotBeSpecified", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000C3D9 File Offset: 0x0000A5D9
		public static LocalizedString ContentIndexStatusSuspended
		{
			get
			{
				return new LocalizedString("ContentIndexStatusSuspended", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000C3F7 File Offset: 0x0000A5F7
		public static LocalizedString TimeZoneUTC13
		{
			get
			{
				return new LocalizedString("TimeZoneUTC13", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000C415 File Offset: 0x0000A615
		public static LocalizedString PolicyTipUrl
		{
			get
			{
				return new LocalizedString("PolicyTipUrl", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000C434 File Offset: 0x0000A634
		public static LocalizedString DomainNameTruncated(string domainName, string truncatedDomainName)
		{
			return new LocalizedString("DomainNameTruncated", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				domainName,
				truncatedDomainName
			});
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000C467 File Offset: 0x0000A667
		public static LocalizedString JobStatusDone
		{
			get
			{
				return new LocalizedString("JobStatusDone", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000C488 File Offset: 0x0000A688
		public static LocalizedString SenderAddress(string senderAddress)
		{
			return new LocalizedString("SenderAddress", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				senderAddress
			});
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000C4B7 File Offset: 0x0000A6B7
		public static LocalizedString Quarantine
		{
			get
			{
				return new LocalizedString("Quarantine", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000C4D5 File Offset: 0x0000A6D5
		public static LocalizedString TrackingWarningResultsMissingFatal
		{
			get
			{
				return new LocalizedString("TrackingWarningResultsMissingFatal", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		public static LocalizedString BodyExactMatchCondition(int ruleId)
		{
			return new LocalizedString("BodyExactMatchCondition", "", false, false, CoreStrings.ResourceManager, new object[]
			{
				ruleId
			});
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000C528 File Offset: 0x0000A728
		public static LocalizedString TimeZoneNewZealandStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneNewZealandStandardTime", "Ex2E0FE2", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000C546 File Offset: 0x0000A746
		public static LocalizedString ExecutableAttachments
		{
			get
			{
				return new LocalizedString("ExecutableAttachments", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000C564 File Offset: 0x0000A764
		public static LocalizedString TimeZoneDatelineStandardTime
		{
			get
			{
				return new LocalizedString("TimeZoneDatelineStandardTime", "ExCC54E8", false, true, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000C582 File Offset: 0x0000A782
		public static LocalizedString EventQueueRetryIW
		{
			get
			{
				return new LocalizedString("EventQueueRetryIW", "", false, false, CoreStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		public static LocalizedString GetLocalizedString(CoreStrings.IDs key)
		{
			return new LocalizedString(CoreStrings.stringIDs[(uint)key], CoreStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000131 RID: 305
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(424);

		// Token: 0x04000132 RID: 306
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Core.CoreStrings", typeof(CoreStrings).GetTypeInfo().Assembly);

		// Token: 0x02000006 RID: 6
		public enum IDs : uint
		{
			// Token: 0x04000134 RID: 308
			RoleTypeDescription_RemoteAndAcceptedDomains = 2092707270U,
			// Token: 0x04000135 RID: 309
			TimeZoneSouthAfricaStandardTime = 1886101956U,
			// Token: 0x04000136 RID: 310
			PolicyTipNotifyOnly = 3906704818U,
			// Token: 0x04000137 RID: 311
			TimeZoneLibyaStandardTime = 97673562U,
			// Token: 0x04000138 RID: 312
			ReportSeverityLow = 1829747073U,
			// Token: 0x04000139 RID: 313
			RoleTypeDescription_ArchiveApplication = 2344870457U,
			// Token: 0x0400013A RID: 314
			EventDelivered = 3472506566U,
			// Token: 0x0400013B RID: 315
			AntispamIPBlockListSettingMigrated = 3720562738U,
			// Token: 0x0400013C RID: 316
			RoleTypeDescription_ActiveMonitoring = 327230581U,
			// Token: 0x0400013D RID: 317
			RoleTypeDescription_UMPrompts = 2962389050U,
			// Token: 0x0400013E RID: 318
			TimeZoneCentralAsiaStandardTime = 146865784U,
			// Token: 0x0400013F RID: 319
			TimeZoneESouthAmericaStandardTime = 146778149U,
			// Token: 0x04000140 RID: 320
			TimeZoneMountainStandardTime = 80585286U,
			// Token: 0x04000141 RID: 321
			TimeZoneKaliningradStandardTime = 1446960263U,
			// Token: 0x04000142 RID: 322
			ExchangeViewOnlyManagementForestOperatorDescription = 137362532U,
			// Token: 0x04000143 RID: 323
			AntispamMigrationSucceeded = 3151979146U,
			// Token: 0x04000144 RID: 324
			TimeZoneNorthAsiaStandardTime = 1683344478U,
			// Token: 0x04000145 RID: 325
			InvalidSender = 89789448U,
			// Token: 0x04000146 RID: 326
			RoleTypeDescription_MyTeamMailboxes = 1221420638U,
			// Token: 0x04000147 RID: 327
			RoleTypeDescription_MyDistributionGroups = 2027768067U,
			// Token: 0x04000148 RID: 328
			TimeZoneFijiStandardTime = 1738495689U,
			// Token: 0x04000149 RID: 329
			TimeZoneEAfricaStandardTime = 1010967094U,
			// Token: 0x0400014A RID: 330
			NotAuthorizedToViewRecipientPath = 498311706U,
			// Token: 0x0400014B RID: 331
			RoleTypeDescription_UserOptions = 2847306432U,
			// Token: 0x0400014C RID: 332
			RoleTypeDescription_UmRecipientManagement = 3322063993U,
			// Token: 0x0400014D RID: 333
			ReportSeverityHigh = 1029644363U,
			// Token: 0x0400014E RID: 334
			RoleTypeDescription_CmdletExtensionAgents = 2763281369U,
			// Token: 0x0400014F RID: 335
			AntimalwareUserOptOut = 2874223383U,
			// Token: 0x04000150 RID: 336
			RoleTypeDescription_MyLogon = 2736802282U,
			// Token: 0x04000151 RID: 337
			ContentIndexStatusDisabled = 2385442291U,
			// Token: 0x04000152 RID: 338
			PolicyTipRejectOverride = 466036006U,
			// Token: 0x04000153 RID: 339
			StatusDelivered = 1875295180U,
			// Token: 0x04000154 RID: 340
			TimeZoneWMongoliaStandardTime = 1255891408U,
			// Token: 0x04000155 RID: 341
			RoleTypeDescription_TransportHygiene = 872336955U,
			// Token: 0x04000156 RID: 342
			TraceLevelHigh = 112955817U,
			// Token: 0x04000157 RID: 343
			RoleTypeDescription_InformationRightsManagement = 4100231581U,
			// Token: 0x04000158 RID: 344
			TestModifySubject = 1461929024U,
			// Token: 0x04000159 RID: 345
			TimeZoneLordHoweStandardTime = 2652353769U,
			// Token: 0x0400015A RID: 346
			InvalidSenderForAdmin = 1319959286U,
			// Token: 0x0400015B RID: 347
			RoleTypeDescription_DiscoveryManagement = 1633693746U,
			// Token: 0x0400015C RID: 348
			TimeZoneTurksAndCaicosStandardTime = 3964643789U,
			// Token: 0x0400015D RID: 349
			RoleTypeDescription_MailboxExport = 2230053377U,
			// Token: 0x0400015E RID: 350
			AntispamIPAllowListSettingMigrated = 3106057138U,
			// Token: 0x0400015F RID: 351
			ExchangeManagementForestOperatorDescription = 4036159751U,
			// Token: 0x04000160 RID: 352
			TimeZoneArabianStandardTime = 2575879303U,
			// Token: 0x04000161 RID: 353
			TimeZoneIsraelStandardTime = 2009673553U,
			// Token: 0x04000162 RID: 354
			RoleTypeDescription_ActiveDirectoryPermissions = 4146612654U,
			// Token: 0x04000163 RID: 355
			RunOnce = 3378761982U,
			// Token: 0x04000164 RID: 356
			MessageDirectionAll = 4217676503U,
			// Token: 0x04000165 RID: 357
			RoleTypeDescription_UmManagement = 2281308004U,
			// Token: 0x04000166 RID: 358
			AuditSeverityLevelHigh = 2385887948U,
			// Token: 0x04000167 RID: 359
			TimeZoneArgentinaStandardTime = 1680752826U,
			// Token: 0x04000168 RID: 360
			RoleTypeDescription_Migration = 2184195641U,
			// Token: 0x04000169 RID: 361
			TimeZoneUlaanbaatarStandardTime = 1406395995U,
			// Token: 0x0400016A RID: 362
			TimeZoneSaratovStandardTime = 1799519309U,
			// Token: 0x0400016B RID: 363
			TimeZoneVenezuelaStandardTime = 1545851488U,
			// Token: 0x0400016C RID: 364
			ExchangeAllMailboxesDescription = 1103744372U,
			// Token: 0x0400016D RID: 365
			RoleTypeDescription_GALSynchronizationManagement = 926819582U,
			// Token: 0x0400016E RID: 366
			RoleTypeDescription_ViewOnlyRoleManagement = 3690262447U,
			// Token: 0x0400016F RID: 367
			RoleTypeDescription_LiveID = 683655822U,
			// Token: 0x04000170 RID: 368
			RoleTypeDescription_TransportAgents = 3406878936U,
			// Token: 0x04000171 RID: 369
			RoleTypeDescription_MyDiagnostics = 2562309877U,
			// Token: 0x04000172 RID: 370
			TrackingWarningNoResultsDueToUntrackableMessagePath = 411395545U,
			// Token: 0x04000173 RID: 371
			EventTransferredToLegacyExchangeServer = 4221322756U,
			// Token: 0x04000174 RID: 372
			RoleTypeDescription_TeamMailboxes = 1998960776U,
			// Token: 0x04000175 RID: 373
			TimeZoneSAPacificStandardTime = 1130640660U,
			// Token: 0x04000176 RID: 374
			ExchangeServerManagementDescription = 1318716879U,
			// Token: 0x04000177 RID: 375
			UnsupportedSenderForTracking = 74443346U,
			// Token: 0x04000178 RID: 376
			TimeZoneMontevideoStandardTime = 2164669709U,
			// Token: 0x04000179 RID: 377
			TimeZoneUSEasternStandardTime = 2912446405U,
			// Token: 0x0400017A RID: 378
			EventApprovedModerationIW = 517696069U,
			// Token: 0x0400017B RID: 379
			TimeZoneCentralAmericaStandardTime = 103080662U,
			// Token: 0x0400017C RID: 380
			ContentIndexStatusAutoSuspended = 393808047U,
			// Token: 0x0400017D RID: 381
			TimeZoneBahiaStandardTime = 2501174206U,
			// Token: 0x0400017E RID: 382
			RoleTypeDescription_EdgeSubscriptions = 2005680638U,
			// Token: 0x0400017F RID: 383
			TimeZoneCubaStandardTime = 3905123624U,
			// Token: 0x04000180 RID: 384
			TimeZoneEasterIslandStandardTime = 3492975378U,
			// Token: 0x04000181 RID: 385
			JobStatusInProgress = 3708464597U,
			// Token: 0x04000182 RID: 386
			TimeZoneUTC09 = 2294597162U,
			// Token: 0x04000183 RID: 387
			TimeZoneEgyptStandardTime = 2993499200U,
			// Token: 0x04000184 RID: 388
			StatusRead = 3231667300U,
			// Token: 0x04000185 RID: 389
			TimeZoneCaucasusStandardTime = 2740570033U,
			// Token: 0x04000186 RID: 390
			RoleTypeDescription_MailEnabledPublicFolders = 4120033037U,
			// Token: 0x04000187 RID: 391
			RoleTypeDescription_ViewOnlyAuditLogs = 4113945646U,
			// Token: 0x04000188 RID: 392
			TrackingWarningReadStatusUnavailable = 576007077U,
			// Token: 0x04000189 RID: 393
			ViewOnlyPIIGroupDescription = 176786758U,
			// Token: 0x0400018A RID: 394
			RoleTypeDescription_FederatedSharing = 2073899161U,
			// Token: 0x0400018B RID: 395
			RoleTypeDescription_RoleManagement = 3390444882U,
			// Token: 0x0400018C RID: 396
			TrackingExplanationNormalTimeSpan = 2903872252U,
			// Token: 0x0400018D RID: 397
			RoleTypeDescription_ExchangeServers = 2937865572U,
			// Token: 0x0400018E RID: 398
			TimeZoneAstrakhanStandardTime = 641655538U,
			// Token: 0x0400018F RID: 399
			ReportSeverityMedium = 840197340U,
			// Token: 0x04000190 RID: 400
			RoleTypeDescription_SupportDiagnostics = 424676612U,
			// Token: 0x04000191 RID: 401
			TimeZoneCentralEuropeanStandardTime = 2220779553U,
			// Token: 0x04000192 RID: 402
			TimeZoneKoreaStandardTime = 1476061287U,
			// Token: 0x04000193 RID: 403
			ContentIndexStatusSeeding = 1361373610U,
			// Token: 0x04000194 RID: 404
			TimeZoneWAustraliaStandardTime = 3998935900U,
			// Token: 0x04000195 RID: 405
			RoleTypeDescription_Custom = 3209229526U,
			// Token: 0x04000196 RID: 406
			AntispamActionTypeSettingMigrated = 390519148U,
			// Token: 0x04000197 RID: 407
			TrackingExplanationExcessiveTimeSpan = 559475236U,
			// Token: 0x04000198 RID: 408
			QuarantineSpam = 2416995053U,
			// Token: 0x04000199 RID: 409
			TimeZoneUTC11 = 728513229U,
			// Token: 0x0400019A RID: 410
			RoleTypeDescription_PublicFolderReplication = 1698995718U,
			// Token: 0x0400019B RID: 411
			Encrypt = 3202170171U,
			// Token: 0x0400019C RID: 412
			TimeZoneRussiaTimeZone11 = 1562661243U,
			// Token: 0x0400019D RID: 413
			TimeZoneEkaterinburgStandardTime = 875871474U,
			// Token: 0x0400019E RID: 414
			RoleTypeDescription_LegalHold = 3860979609U,
			// Token: 0x0400019F RID: 415
			TimeZoneTocantinsStandardTime = 50740234U,
			// Token: 0x040001A0 RID: 416
			TimeZoneArabicStandardTime = 2428028299U,
			// Token: 0x040001A1 RID: 417
			RoleTypeDescription_MailboxImportExport = 1011790132U,
			// Token: 0x040001A2 RID: 418
			RoleTypeDescription_Supervision = 4065087132U,
			// Token: 0x040001A3 RID: 419
			RoleTypeDescription_LawEnforcementRequests = 937823105U,
			// Token: 0x040001A4 RID: 420
			RoleTypeDescription_MailboxSearchApplication = 851475441U,
			// Token: 0x040001A5 RID: 421
			RoleTypeDescription_RetentionManagement = 20620266U,
			// Token: 0x040001A6 RID: 422
			TimeZoneWestAsiaStandardTime = 937651576U,
			// Token: 0x040001A7 RID: 423
			QuarantineTransportRule = 1727765283U,
			// Token: 0x040001A8 RID: 424
			RoleTypeDescription_ViewOnlyCentralAdminManagement = 3421391191U,
			// Token: 0x040001A9 RID: 425
			EventFailedTransportRulesIW = 2151396155U,
			// Token: 0x040001AA RID: 426
			EventDelayedAfterTransferToPartnerOrgIW = 199727676U,
			// Token: 0x040001AB RID: 427
			RoleTypeDescription_DatacenterOperationsDCOnly = 1348529911U,
			// Token: 0x040001AC RID: 428
			TimeZoneTomskStandardTime = 4089915379U,
			// Token: 0x040001AD RID: 429
			TrackingBusy = 3099813970U,
			// Token: 0x040001AE RID: 430
			TimeZoneTongaStandardTime = 1728005806U,
			// Token: 0x040001AF RID: 431
			TimeZoneTasmaniaStandardTime = 3917786073U,
			// Token: 0x040001B0 RID: 432
			ExchangePublicFolderAdminDescription = 2471410929U,
			// Token: 0x040001B1 RID: 433
			TrafficScopeOutbound = 1018209439U,
			// Token: 0x040001B2 RID: 434
			EventForwardedToDelegateAndDeleted = 2309256410U,
			// Token: 0x040001B3 RID: 435
			RoleTypeDescription_GALSynchronization = 3851963997U,
			// Token: 0x040001B4 RID: 436
			CompressionOutOfMemory = 1747500934U,
			// Token: 0x040001B5 RID: 437
			TrafficScopeDisabled = 1069292955U,
			// Token: 0x040001B6 RID: 438
			RoleTypeDescription_OrganizationManagement = 1969466653U,
			// Token: 0x040001B7 RID: 439
			TimeZoneOmskStandardTime = 1654105079U,
			// Token: 0x040001B8 RID: 440
			TimeZoneBelarusStandardTime = 1984558815U,
			// Token: 0x040001B9 RID: 441
			TimeZoneParaguayStandardTime = 3386037373U,
			// Token: 0x040001BA RID: 442
			RoleTypeDescription_Reporting = 1198640795U,
			// Token: 0x040001BB RID: 443
			TimeZoneChathamIslandsStandardTime = 3103556173U,
			// Token: 0x040001BC RID: 444
			RoleTypeDescription_MyMailboxDelegation = 1660783915U,
			// Token: 0x040001BD RID: 445
			RoleTypeDescription_ExchangeVirtualDirectories = 397099290U,
			// Token: 0x040001BE RID: 446
			TimeZoneAUSEasternStandardTime = 3213863256U,
			// Token: 0x040001BF RID: 447
			EventNotRead = 3256513739U,
			// Token: 0x040001C0 RID: 448
			TimeZoneMiddleEastStandardTime = 463534759U,
			// Token: 0x040001C1 RID: 449
			RoleTypeDescription_ApplicationImpersonation = 907728851U,
			// Token: 0x040001C2 RID: 450
			TrackingWarningTooManyEvents = 2979872069U,
			// Token: 0x040001C3 RID: 451
			ContentIndexStatusUnknown = 1631091055U,
			// Token: 0x040001C4 RID: 452
			TimeZoneSyriaStandardTime = 1826684897U,
			// Token: 0x040001C5 RID: 453
			TimeZoneMauritiusStandardTime = 3804745356U,
			// Token: 0x040001C6 RID: 454
			TrackingMessageTypeNotSupported = 667589187U,
			// Token: 0x040001C7 RID: 455
			TimeZoneCentralPacificStandardTime = 3096621845U,
			// Token: 0x040001C8 RID: 456
			RoleTypeDescription_MailboxSearch = 1156915939U,
			// Token: 0x040001C9 RID: 457
			StdUnknownTimeZone = 1027560048U,
			// Token: 0x040001CA RID: 458
			StatusUnsuccessFul = 3485911895U,
			// Token: 0x040001CB RID: 459
			RoleTypeDescription_MyLinkedInEnabled = 1187302658U,
			// Token: 0x040001CC RID: 460
			TestXHeader = 4008183169U,
			// Token: 0x040001CD RID: 461
			RoleTypeDescription_ReceiveConnectors = 1479842702U,
			// Token: 0x040001CE RID: 462
			TimeZoneRussiaTimeZone3 = 1338433320U,
			// Token: 0x040001CF RID: 463
			TimeZoneTransbaikalStandardTime = 2262407331U,
			// Token: 0x040001D0 RID: 464
			RoleTypeDescription_Databases = 2629692075U,
			// Token: 0x040001D1 RID: 465
			StatusTransferred = 14056478U,
			// Token: 0x040001D2 RID: 466
			TimeZoneGeorgianStandardTime = 2237907931U,
			// Token: 0x040001D3 RID: 467
			Decrypt = 3620221593U,
			// Token: 0x040001D4 RID: 468
			RoleTypeDescription_MyReadWriteMailboxApps = 3879259618U,
			// Token: 0x040001D5 RID: 469
			TimeZoneBougainvilleStandardTime = 3742415118U,
			// Token: 0x040001D6 RID: 470
			RoleTypeDescription_EdgeSync = 99561377U,
			// Token: 0x040001D7 RID: 471
			TimeZoneTurkeyStandardTime = 2333794793U,
			// Token: 0x040001D8 RID: 472
			RoleTypeDescription_MyMailSubscriptions = 3305616866U,
			// Token: 0x040001D9 RID: 473
			PartialMessages = 1393844023U,
			// Token: 0x040001DA RID: 474
			InboundIpMigrationCompleted = 3351363629U,
			// Token: 0x040001DB RID: 475
			RoleTypeDescription_MyMarketplaceApps = 626810642U,
			// Token: 0x040001DC RID: 476
			TimeZoneJordanStandardTime = 448910837U,
			// Token: 0x040001DD RID: 477
			EventPendingModerationHelpDesk = 1664785551U,
			// Token: 0x040001DE RID: 478
			DeliveryStatusDelivered = 1334000728U,
			// Token: 0x040001DF RID: 479
			TimeZoneEEuropeStandardTime = 135488574U,
			// Token: 0x040001E0 RID: 480
			RoleTypeDescription_MyVoiceMail = 3847090244U,
			// Token: 0x040001E1 RID: 481
			TimeZoneMyanmarStandardTime = 3097579504U,
			// Token: 0x040001E2 RID: 482
			TrackingExplanationLogsDeleted = 1321416518U,
			// Token: 0x040001E3 RID: 483
			ExchangeOrgAdminDescription = 2198659572U,
			// Token: 0x040001E4 RID: 484
			TimeZoneNepalStandardTime = 2229119179U,
			// Token: 0x040001E5 RID: 485
			TimeZoneCenAustraliaStandardTime = 203352201U,
			// Token: 0x040001E6 RID: 486
			JobStatusFailed = 2350709050U,
			// Token: 0x040001E7 RID: 487
			RoleTypeDescription_TransportQueues = 2909789462U,
			// Token: 0x040001E8 RID: 488
			TimeZoneWestPacificStandardTime = 2239001839U,
			// Token: 0x040001E9 RID: 489
			TrackingWarningNoResultsDueToTrackingTooEarly = 21646529U,
			// Token: 0x040001EA RID: 490
			RoleTypeDescription_ViewOnlyOrganizationManagement = 4161689178U,
			// Token: 0x040001EB RID: 491
			RoleTypeDescription_ViewOnlyRecipients = 2206122650U,
			// Token: 0x040001EC RID: 492
			Allow = 1776488541U,
			// Token: 0x040001ED RID: 493
			DomainScopeAlLDomains = 430852938U,
			// Token: 0x040001EE RID: 494
			NoValidDomainNameExistsInDomainSettings = 1082730632U,
			// Token: 0x040001EF RID: 495
			ExchangeRecordsManagementDescription = 377106190U,
			// Token: 0x040001F0 RID: 496
			TimeZoneUTC08 = 2294597161U,
			// Token: 0x040001F1 RID: 497
			RoleTypeDescription_NetworkingManagement = 1319136228U,
			// Token: 0x040001F2 RID: 498
			EventTransferredToForeignOrgHelpDesk = 3173642551U,
			// Token: 0x040001F3 RID: 499
			RoleTypeDescription_MyFacebookEnabled = 3245137676U,
			// Token: 0x040001F4 RID: 500
			TimeZoneCentralEuropeStandardTime = 3487147524U,
			// Token: 0x040001F5 RID: 501
			RoleTypeDescription_SecurityGroupCreationAndMembership = 254529528U,
			// Token: 0x040001F6 RID: 502
			DeliveryStatusExpanded = 1316318251U,
			// Token: 0x040001F7 RID: 503
			StatusPending = 1981651471U,
			// Token: 0x040001F8 RID: 504
			TimeZoneMidAtlanticStandardTime = 3359056161U,
			// Token: 0x040001F9 RID: 505
			RoleTypeDescription_ResetPassword = 970642999U,
			// Token: 0x040001FA RID: 506
			ExchangeUMManagementDescription = 1907760708U,
			// Token: 0x040001FB RID: 507
			TimeZoneUTC = 1392628209U,
			// Token: 0x040001FC RID: 508
			RoleTypeDescription_DataLossPrevention = 547528310U,
			// Token: 0x040001FD RID: 509
			EventPendingModerationIW = 429272165U,
			// Token: 0x040001FE RID: 510
			RoleTypeDescription_MyCustomApps = 1939880180U,
			// Token: 0x040001FF RID: 511
			RoleTypeDescription_DatabaseAvailabilityGroups = 2722115341U,
			// Token: 0x04000200 RID: 512
			ExchangeHelpDeskDescription = 2031056185U,
			// Token: 0x04000201 RID: 513
			MessageDirectionReceived = 2388819289U,
			// Token: 0x04000202 RID: 514
			SpamQuarantineMigrationSucceeded = 1562661838U,
			// Token: 0x04000203 RID: 515
			EventFailedModerationIW = 2708860089U,
			// Token: 0x04000204 RID: 516
			TimeZoneUTC02 = 2294597167U,
			// Token: 0x04000205 RID: 517
			AntimalwareScopingConstraint = 4202622721U,
			// Token: 0x04000206 RID: 518
			DeliveryStatusFailed = 2928288207U,
			// Token: 0x04000207 RID: 519
			QuarantineInbound = 3278150655U,
			// Token: 0x04000208 RID: 520
			RoleTypeDescription_Throttling = 872985302U,
			// Token: 0x04000209 RID: 521
			RoleTypeDescription_DataCenterDestructiveOperations = 2019318218U,
			// Token: 0x0400020A RID: 522
			RoleTypeDescription_AddressLists = 2945933912U,
			// Token: 0x0400020B RID: 523
			RoleTypeDescription_CentralAdminManagement = 1959014922U,
			// Token: 0x0400020C RID: 524
			TimeZoneAfghanistanStandardTime = 694118059U,
			// Token: 0x0400020D RID: 525
			EventTransferredToForeignOrgIW = 2433016809U,
			// Token: 0x0400020E RID: 526
			JobStatusCancelled = 1457011382U,
			// Token: 0x0400020F RID: 527
			TimeZoneAtlanticStandardTime = 2033924825U,
			// Token: 0x04000210 RID: 528
			TimeZoneArabStandardTime = 1901284917U,
			// Token: 0x04000211 RID: 529
			RoleTypeDescription_MailRecipients = 238886746U,
			// Token: 0x04000212 RID: 530
			RoleTypeDescription_WorkloadManagement = 2886777393U,
			// Token: 0x04000213 RID: 531
			TimeZoneAlaskanStandardTime = 1286295614U,
			// Token: 0x04000214 RID: 532
			MsoManagedTenantHelpdeskGroupDescription = 1235839409U,
			// Token: 0x04000215 RID: 533
			AntimalwareInboundRecipientNotifications = 1838296451U,
			// Token: 0x04000216 RID: 534
			TestXHeaderAndModifySubject = 270033310U,
			// Token: 0x04000217 RID: 535
			ContentIndexStatusFailedAndSuspended = 1923042104U,
			// Token: 0x04000218 RID: 536
			MsoManagedTenantAdminGroupDescription = 2104897796U,
			// Token: 0x04000219 RID: 537
			RoleTypeDescription_DataCenterOperations = 1558282382U,
			// Token: 0x0400021A RID: 538
			EventModerationExpired = 3700079135U,
			// Token: 0x0400021B RID: 539
			TraceLevelMedium = 29170872U,
			// Token: 0x0400021C RID: 540
			RoleTypeDescription_MoveMailboxes = 432229092U,
			// Token: 0x0400021D RID: 541
			RoleTypeDescription_MailRecipientCreation = 1038994972U,
			// Token: 0x0400021E RID: 542
			TimeZoneLineIslandsStandardTime = 3021954325U,
			// Token: 0x0400021F RID: 543
			MissingIdentityParameter = 2008936115U,
			// Token: 0x04000220 RID: 544
			ContentIndexStatusHealthyAndUpgrading = 3268869348U,
			// Token: 0x04000221 RID: 545
			TrafficScopeInbound = 1450163010U,
			// Token: 0x04000222 RID: 546
			RoleTypeDescription_UnScopedRoleManagement = 880515475U,
			// Token: 0x04000223 RID: 547
			AuditSeverityLevelDoNotAudit = 267867511U,
			// Token: 0x04000224 RID: 548
			ExchangeHygieneManagementDescription = 1778108211U,
			// Token: 0x04000225 RID: 549
			ClassIdExtensions = 1246550425U,
			// Token: 0x04000226 RID: 550
			TimeZoneNamibiaStandardTime = 4236830390U,
			// Token: 0x04000227 RID: 551
			RejectedExplanationContentFiltering = 2735513322U,
			// Token: 0x04000228 RID: 552
			QuarantineOutbound = 1179870130U,
			// Token: 0x04000229 RID: 553
			RunWeekly = 2454182516U,
			// Token: 0x0400022A RID: 554
			TimeZoneAltaiStandardTime = 1623584678U,
			// Token: 0x0400022B RID: 555
			EventSubmittedCrossSite = 889690570U,
			// Token: 0x0400022C RID: 556
			TimeZoneEasternStandardTime = 565087651U,
			// Token: 0x0400022D RID: 557
			TimeZoneAzerbaijanStandardTime = 1000900012U,
			// Token: 0x0400022E RID: 558
			TimeZonePakistanStandardTime = 2216352038U,
			// Token: 0x0400022F RID: 559
			RoleTypeDescription_OrgMarketplaceApps = 790166856U,
			// Token: 0x04000230 RID: 560
			TimeZonePacificSAStandardTime = 3066204968U,
			// Token: 0x04000231 RID: 561
			TimeZoneRussianStandardTime = 2981542010U,
			// Token: 0x04000232 RID: 562
			RoleTypeDescription_POP3AndIMAP4Protocols = 2623630612U,
			// Token: 0x04000233 RID: 563
			TimeZoneTaipeiStandardTime = 860548277U,
			// Token: 0x04000234 RID: 564
			RoleTypeDescription_HelpdeskRecipientManagement = 1145350245U,
			// Token: 0x04000235 RID: 565
			TimeZoneHawaiianStandardTime = 756612021U,
			// Token: 0x04000236 RID: 566
			TimeZoneMagallanesStandardTime = 2907857020U,
			// Token: 0x04000237 RID: 567
			EventFailedTransportRulesHelpDesk = 101906589U,
			// Token: 0x04000238 RID: 568
			TimeZoneTokyoStandardTime = 2104951827U,
			// Token: 0x04000239 RID: 569
			DeliveryStatusAll = 2425183541U,
			// Token: 0x0400023A RID: 570
			RoleTypeDescription_MyOptions = 4216139125U,
			// Token: 0x0400023B RID: 571
			RoleTypeDescription_DisasterRecovery = 996841945U,
			// Token: 0x0400023C RID: 572
			RoleTypeDescription_EmailAddressPolicies = 3202528089U,
			// Token: 0x0400023D RID: 573
			RoleTypeDescription_SendConnectors = 3655383969U,
			// Token: 0x0400023E RID: 574
			TimeZoneSudanStandardTime = 3185790196U,
			// Token: 0x0400023F RID: 575
			EventRead = 1544756182U,
			// Token: 0x04000240 RID: 576
			TimeZoneGreenlandStandardTime = 2467780809U,
			// Token: 0x04000241 RID: 577
			RoleTypeDescription_OutlookProvider = 2169942641U,
			// Token: 0x04000242 RID: 578
			EventTransferredIntermediate = 1460332657U,
			// Token: 0x04000243 RID: 579
			RoleTypeDescription_OrgCustomApps = 3444675422U,
			// Token: 0x04000244 RID: 580
			TimeZoneBangladeshStandardTime = 2174613220U,
			// Token: 0x04000245 RID: 581
			TimeZoneGMTStandardTime = 373243623U,
			// Token: 0x04000246 RID: 582
			TimeZoneGTBStandardTime = 1223375664U,
			// Token: 0x04000247 RID: 583
			JobStatusNotStarted = 4078458935U,
			// Token: 0x04000248 RID: 584
			TrackingWarningNoResultsDueToLogsNotFound = 1663352993U,
			// Token: 0x04000249 RID: 585
			TimeZoneSAWesternStandardTime = 77017683U,
			// Token: 0x0400024A RID: 586
			TimeZoneHaitiStandardTime = 3490088586U,
			// Token: 0x0400024B RID: 587
			TrackingErrorFailedToInitialize = 2700445791U,
			// Token: 0x0400024C RID: 588
			TimeZoneKamchatkaStandardTime = 728892142U,
			// Token: 0x0400024D RID: 589
			TrackingWarningResultsMissingTransient = 4111676399U,
			// Token: 0x0400024E RID: 590
			RulesMerged = 2247997721U,
			// Token: 0x0400024F RID: 591
			RoleTypeDescription_MyRetentionPolicies = 3804873683U,
			// Token: 0x04000250 RID: 592
			DomainScopedRulesMerged = 647678831U,
			// Token: 0x04000251 RID: 593
			EventResolvedHelpDesk = 1612370800U,
			// Token: 0x04000252 RID: 594
			RoleTypeDescription_LegalHoldApplication = 1531217347U,
			// Token: 0x04000253 RID: 595
			TimeZoneSingaporeStandardTime = 3498536455U,
			// Token: 0x04000254 RID: 596
			AntimalwareAdminAddressNull = 1949143649U,
			// Token: 0x04000255 RID: 597
			RoleTypeDescription_UserApplication = 2725696980U,
			// Token: 0x04000256 RID: 598
			RoleTypeDescription_MyContactInformation = 2096766553U,
			// Token: 0x04000257 RID: 599
			TimeZoneNorthAsiaEastStandardTime = 967467375U,
			// Token: 0x04000258 RID: 600
			ExchangeRecipientAdminDescription = 4108314201U,
			// Token: 0x04000259 RID: 601
			TimeZoneCapeVerdeStandardTime = 226934128U,
			// Token: 0x0400025A RID: 602
			TrackingSearchNotAuthorized = 3317872187U,
			// Token: 0x0400025B RID: 603
			ContentIndexStatusCrawling = 1575862374U,
			// Token: 0x0400025C RID: 604
			TimeZoneMagadanStandardTime = 2874655766U,
			// Token: 0x0400025D RID: 605
			TimeZoneAUSCentralStandardTime = 2055163269U,
			// Token: 0x0400025E RID: 606
			ExchangeDiscoveryManagementDescription = 2585558426U,
			// Token: 0x0400025F RID: 607
			EventMessageDefer = 3586764147U,
			// Token: 0x04000260 RID: 608
			TimeZoneVolgogradStandardTime = 2033601224U,
			// Token: 0x04000261 RID: 609
			TimeZonePacificStandardTimeMexico = 2373652411U,
			// Token: 0x04000262 RID: 610
			TimeZoneSamoaStandardTime = 2679378804U,
			// Token: 0x04000263 RID: 611
			RoleTypeDescription_PersonallyIdentifiableInformation = 2872669904U,
			// Token: 0x04000264 RID: 612
			TimeZoneUTC12 = 728513226U,
			// Token: 0x04000265 RID: 613
			RoleTypeDescription_MessageTracking = 3152944101U,
			// Token: 0x04000266 RID: 614
			TraceLevelLow = 1084831951U,
			// Token: 0x04000267 RID: 615
			RoleTypeDescription_MyTextMessaging = 661809288U,
			// Token: 0x04000268 RID: 616
			RoleTypeDescription_RecipientPolicies = 2577770798U,
			// Token: 0x04000269 RID: 617
			TimeZoneSAEasternStandardTime = 953426317U,
			// Token: 0x0400026A RID: 618
			RoleTypeDescription_TeamMailboxLifecycleApplication = 2376615838U,
			// Token: 0x0400026B RID: 619
			InvalidMessageTrackingReportId = 1205552534U,
			// Token: 0x0400026C RID: 620
			TimeZoneIndiaStandardTime = 430105556U,
			// Token: 0x0400026D RID: 621
			ContentIndexStatusFailed = 2562345274U,
			// Token: 0x0400026E RID: 622
			TimeZoneNewfoundlandStandardTime = 1494389266U,
			// Token: 0x0400026F RID: 623
			TimeZoneYakutskStandardTime = 2246440755U,
			// Token: 0x04000270 RID: 624
			RoleTypeDescription_OrganizationHelpSettings = 1274798456U,
			// Token: 0x04000271 RID: 625
			EventFailedModerationHelpDesk = 1835211555U,
			// Token: 0x04000272 RID: 626
			RoleTypeDescription_PublicFolders = 903792657U,
			// Token: 0x04000273 RID: 627
			TimeZonePacificStandardTime = 3005390568U,
			// Token: 0x04000274 RID: 628
			TimeZoneAleutianStandardTime = 61789322U,
			// Token: 0x04000275 RID: 629
			ExchangeManagementForestTier1SupportDescription = 3049152015U,
			// Token: 0x04000276 RID: 630
			TimeZoneNorthKoreaStandardTime = 2034512234U,
			// Token: 0x04000277 RID: 631
			DltUnknownTimeZone = 920357151U,
			// Token: 0x04000278 RID: 632
			RoleTypeDescription_ExchangeConnectors = 2373064468U,
			// Token: 0x04000279 RID: 633
			RoleTypeDescription_ViewOnlyCentralAdminSupport = 4036330655U,
			// Token: 0x0400027A RID: 634
			TimeZoneNorfolkStandardTime = 4097835338U,
			// Token: 0x0400027B RID: 635
			RoleTypeDescription_MyProfileInformation = 273095822U,
			// Token: 0x0400027C RID: 636
			TimeZoneSaoTomeStandardTime = 1814215051U,
			// Token: 0x0400027D RID: 637
			RoleTypeDescription_Journaling = 2648516368U,
			// Token: 0x0400027E RID: 638
			TimeZoneAzoresStandardTime = 1736152949U,
			// Token: 0x0400027F RID: 639
			TimeZoneAusCentralWStandardTime = 290876870U,
			// Token: 0x04000280 RID: 640
			TimeZoneUSMountainStandardTime = 4043635750U,
			// Token: 0x04000281 RID: 641
			RoleTypeDescription_ExchangeCrossServiceIntegration = 606097983U,
			// Token: 0x04000282 RID: 642
			PolicyMigrationSucceeded = 3335916139U,
			// Token: 0x04000283 RID: 643
			TimeZoneCentralBrazilianStandardTime = 140823076U,
			// Token: 0x04000284 RID: 644
			EventRulesCc = 2103082753U,
			// Token: 0x04000285 RID: 645
			ComplianceManagementGroupDescription = 2088363403U,
			// Token: 0x04000286 RID: 646
			TimeZoneWCentralAfricaStandardTime = 1556269475U,
			// Token: 0x04000287 RID: 647
			RoleTypeDescription_UMMailboxes = 3371502447U,
			// Token: 0x04000288 RID: 648
			ExchangeAllHostedOrgsDescription = 1332960560U,
			// Token: 0x04000289 RID: 649
			RoleTypeDescription_DistributionGroups = 1124818555U,
			// Token: 0x0400028A RID: 650
			ExchangeDelegatedSetupDescription = 92857077U,
			// Token: 0x0400028B RID: 651
			TimeZoneVladivostokStandardTime = 2984557217U,
			// Token: 0x0400028C RID: 652
			TimeZoneCentralStandardTime = 1755122990U,
			// Token: 0x0400028D RID: 653
			RoleTypeDescription_OrganizationClientAccess = 278680123U,
			// Token: 0x0400028E RID: 654
			ExchangeViewOnlyAdminDescription = 137603179U,
			// Token: 0x0400028F RID: 655
			TimeZoneEAustraliaStandardTime = 2189811946U,
			// Token: 0x04000290 RID: 656
			TrackingTransientError = 763976563U,
			// Token: 0x04000291 RID: 657
			TrafficScopeInboundAndOutbound = 3970531209U,
			// Token: 0x04000292 RID: 658
			RoleTypeDescription_CentralAdminCredentialManagement = 675468529U,
			// Token: 0x04000293 RID: 659
			RoleTypeDescription_UnScoped = 2365507750U,
			// Token: 0x04000294 RID: 660
			RoleTypeDescription_MyDistributionGroupMembership = 2259701124U,
			// Token: 0x04000295 RID: 661
			RoleTypeDescription_OfficeExtensionApplication = 3079546428U,
			// Token: 0x04000296 RID: 662
			RoleTypeDescription_DistributionGroupManagement = 3118752499U,
			// Token: 0x04000297 RID: 663
			AntimalwareMigrationSucceeded = 2462125228U,
			// Token: 0x04000298 RID: 664
			TimeZoneWEuropeStandardTime = 529456528U,
			// Token: 0x04000299 RID: 665
			RoleTypeDescription_AuditLogs = 172038955U,
			// Token: 0x0400029A RID: 666
			EventSubmitted = 3669915041U,
			// Token: 0x0400029B RID: 667
			TimeZoneEasternStandardTimeMexico = 2836851600U,
			// Token: 0x0400029C RID: 668
			TimeZoneRomanceStandardTime = 2471825646U,
			// Token: 0x0400029D RID: 669
			TimeZoneIranStandardTime = 2847973369U,
			// Token: 0x0400029E RID: 670
			RoleTypeDescription_AccessToCustomerDataDCOnly = 2304159373U,
			// Token: 0x0400029F RID: 671
			InvalidIdentityForAdmin = 102930739U,
			// Token: 0x040002A0 RID: 672
			TimeZoneMarquesasStandardTime = 3118313859U,
			// Token: 0x040002A1 RID: 673
			ContentIndexStatusHealthy = 4010596708U,
			// Token: 0x040002A2 RID: 674
			RunMonthly = 2141699896U,
			// Token: 0x040002A3 RID: 675
			RoleTypeDescription_OrganizationTransportSettings = 1224765338U,
			// Token: 0x040002A4 RID: 676
			RoleTypeDescription_ViewOnlyConfiguration = 56162392U,
			// Token: 0x040002A5 RID: 677
			TimeZoneMountainStandardTimeMexico = 3450693719U,
			// Token: 0x040002A6 RID: 678
			RoleTypeDescription_PartnerDelegatedTenantManagement = 3338361539U,
			// Token: 0x040002A7 RID: 679
			RoleTypeDescription_OrganizationConfiguration = 1621594316U,
			// Token: 0x040002A8 RID: 680
			MsoMailTenantAdminGroupDescription = 3266046896U,
			// Token: 0x040002A9 RID: 681
			TimeZoneSEAsiaStandardTime = 4268682097U,
			// Token: 0x040002AA RID: 682
			RoleTypeDescription_DatabaseCopies = 3867579719U,
			// Token: 0x040002AB RID: 683
			TimeZoneCentralStandardTimeMexico = 1556691491U,
			// Token: 0x040002AC RID: 684
			OutboundIpMigrationCompleted = 2695329098U,
			// Token: 0x040002AD RID: 685
			RecipientPathFilterNeeded = 2154772013U,
			// Token: 0x040002AE RID: 686
			TrackingWarningResultsMissingConnection = 3188452985U,
			// Token: 0x040002AF RID: 687
			TimeZoneNCentralAsiaStandardTime = 4058915032U,
			// Token: 0x040002B0 RID: 688
			PolicyTipReject = 1753131922U,
			// Token: 0x040002B1 RID: 689
			AuditSeverityLevelLow = 2037460782U,
			// Token: 0x040002B2 RID: 690
			RoleTypeDescription_RecipientManagement = 2054972301U,
			// Token: 0x040002B3 RID: 691
			EventForwarded = 435759238U,
			// Token: 0x040002B4 RID: 692
			EventApprovedModerationHelpDesk = 1260062527U,
			// Token: 0x040002B5 RID: 693
			Reject = 2235638931U,
			// Token: 0x040002B6 RID: 694
			TimeZoneSaintPierreStandardTime = 3526220825U,
			// Token: 0x040002B7 RID: 695
			TimeZoneMoroccoStandardTime = 1391837657U,
			// Token: 0x040002B8 RID: 696
			TimeZoneFLEStandardTime = 3250495282U,
			// Token: 0x040002B9 RID: 697
			RoleTypeDescription_MailTips = 566029912U,
			// Token: 0x040002BA RID: 698
			RoleTypeDescription_Monitoring = 3881958977U,
			// Token: 0x040002BB RID: 699
			AntispamEdgeBlockModeSettingNotMigrated = 2892044242U,
			// Token: 0x040002BC RID: 700
			TimeZoneCanadaCentralStandardTime = 779085836U,
			// Token: 0x040002BD RID: 701
			ExchangeManagementForestMonitoringDescription = 2024391679U,
			// Token: 0x040002BE RID: 702
			TimeZoneSakhalinStandardTime = 1004126924U,
			// Token: 0x040002BF RID: 703
			TimeZoneSriLankaStandardTime = 59677322U,
			// Token: 0x040002C0 RID: 704
			RoleTypeDescription_AutoDiscover = 766524993U,
			// Token: 0x040002C1 RID: 705
			TimeZoneRussiaTimeZone10 = 4291544598U,
			// Token: 0x040002C2 RID: 706
			EventTransferredToSMSMessage = 3673094505U,
			// Token: 0x040002C3 RID: 707
			TrackingWarningNoResultsDueToLogsExpired = 3152178141U,
			// Token: 0x040002C4 RID: 708
			MessageDirectionSent = 3797334152U,
			// Token: 0x040002C5 RID: 709
			RoleTypeDescription_RecordsManagement = 4116693438U,
			// Token: 0x040002C6 RID: 710
			RoleTypeDescription_TransportRules = 825624151U,
			// Token: 0x040002C7 RID: 711
			ForceTls = 1566193246U,
			// Token: 0x040002C8 RID: 712
			EventFailedGeneral = 3297737305U,
			// Token: 0x040002C9 RID: 713
			RoleTypeDescription_ExchangeServerCertificates = 2580146609U,
			// Token: 0x040002CA RID: 714
			EventPending = 1092647167U,
			// Token: 0x040002CB RID: 715
			TimeZoneChinaStandardTime = 845119240U,
			// Token: 0x040002CC RID: 716
			TimeZoneWestBankStandardTime = 4218393972U,
			// Token: 0x040002CD RID: 717
			RoleTypeDescription_MyBaseOptions = 1316750960U,
			// Token: 0x040002CE RID: 718
			TimeZoneGreenwichStandardTime = 3120202503U,
			// Token: 0x040002CF RID: 719
			RoleTypeDescription_UMPromptManagement = 3840952578U,
			// Token: 0x040002D0 RID: 720
			AuditSeverityLevelMedium = 3678129241U,
			// Token: 0x040002D1 RID: 721
			StatusFilterCannotBeSpecified = 4008278162U,
			// Token: 0x040002D2 RID: 722
			ContentIndexStatusSuspended = 1056819816U,
			// Token: 0x040002D3 RID: 723
			TimeZoneUTC13 = 728513227U,
			// Token: 0x040002D4 RID: 724
			PolicyTipUrl = 1737084126U,
			// Token: 0x040002D5 RID: 725
			JobStatusDone = 2937930519U,
			// Token: 0x040002D6 RID: 726
			Quarantine = 2403632430U,
			// Token: 0x040002D7 RID: 727
			TrackingWarningResultsMissingFatal = 3403825909U,
			// Token: 0x040002D8 RID: 728
			TimeZoneNewZealandStandardTime = 1215124770U,
			// Token: 0x040002D9 RID: 729
			ExecutableAttachments = 3584252448U,
			// Token: 0x040002DA RID: 730
			TimeZoneDatelineStandardTime = 2732724415U,
			// Token: 0x040002DB RID: 731
			EventQueueRetryIW = 1156214899U
		}

		// Token: 0x02000007 RID: 7
		private enum ParamIDs
		{
			// Token: 0x040002DD RID: 733
			InvalidIpRange,
			// Token: 0x040002DE RID: 734
			InvalidDomainNameInDomainSettings,
			// Token: 0x040002DF RID: 735
			FopePolicyRuleIsTooLargeToMigrate,
			// Token: 0x040002E0 RID: 736
			OpportunisticTls,
			// Token: 0x040002E1 RID: 737
			EventMovedToFolderByInboxRuleHelpDesk,
			// Token: 0x040002E2 RID: 738
			FopePolicyRuleContainsInvalidPattern,
			// Token: 0x040002E3 RID: 739
			EventSubmittedHelpDesk,
			// Token: 0x040002E4 RID: 740
			FopePolicyRuleExpired,
			// Token: 0x040002E5 RID: 741
			RecipientDomainConditionContainsInvalidDomainNames,
			// Token: 0x040002E6 RID: 742
			DistributionGroupForVirtualDomainsCreated,
			// Token: 0x040002E7 RID: 743
			EventSmtpReceiveHelpDesk,
			// Token: 0x040002E8 RID: 744
			RecipientDomainNames,
			// Token: 0x040002E9 RID: 745
			BodyCaseSensitive,
			// Token: 0x040002EA RID: 746
			TenantSkuNotSupportedByAntispam,
			// Token: 0x040002EB RID: 747
			DisabledInboundConnector,
			// Token: 0x040002EC RID: 748
			InvalidSmtpAddressInFopeRule,
			// Token: 0x040002ED RID: 749
			RecipientAddresses,
			// Token: 0x040002EE RID: 750
			SubjectExactMatchCondition,
			// Token: 0x040002EF RID: 751
			SubjectExactMatchCaseSensitive,
			// Token: 0x040002F0 RID: 752
			AntimalwareInboundRecipientNotificationsScoped,
			// Token: 0x040002F1 RID: 753
			Body,
			// Token: 0x040002F2 RID: 754
			EventQueueRetryNoRetryTimeHelpDesk,
			// Token: 0x040002F3 RID: 755
			SubjectCaseSensitiveCondition,
			// Token: 0x040002F4 RID: 756
			MaximumMessageSize,
			// Token: 0x040002F5 RID: 757
			EventQueueRetryNoErrorHelpDesk,
			// Token: 0x040002F6 RID: 758
			InvalidUserName,
			// Token: 0x040002F7 RID: 759
			AntimalwareAdminAddressValidations,
			// Token: 0x040002F8 RID: 760
			SenderDomainNames,
			// Token: 0x040002F9 RID: 761
			Subject,
			// Token: 0x040002FA RID: 762
			CharacterSets,
			// Token: 0x040002FB RID: 763
			AdminNotificationContainsMultipleAddresses,
			// Token: 0x040002FC RID: 764
			FopePolicyRuleDisabled,
			// Token: 0x040002FD RID: 765
			DistributionGroupForExcludedUsersCreated,
			// Token: 0x040002FE RID: 766
			BodyCaseSensitiveCondition,
			// Token: 0x040002FF RID: 767
			NoValidSmtpAddress,
			// Token: 0x04000300 RID: 768
			FopePolicyRuleContainsRecipientAddressAndRecipientDomainConditions,
			// Token: 0x04000301 RID: 769
			FopePolicyRuleHasMaxRecipientsCondition,
			// Token: 0x04000302 RID: 770
			ClassIdProperty,
			// Token: 0x04000303 RID: 771
			MigratedFooterSizeExceedsDisclaimerMaxSize,
			// Token: 0x04000304 RID: 772
			BodyExactMatchCaseSensitive,
			// Token: 0x04000305 RID: 773
			EventMovedToFolderByInboxRuleIW,
			// Token: 0x04000306 RID: 774
			DistributionListEmpty,
			// Token: 0x04000307 RID: 775
			NoValidDomainNameExistsInDomainScopedRule,
			// Token: 0x04000308 RID: 776
			InvalidRecipientForAdmin,
			// Token: 0x04000309 RID: 777
			NoValidIpRangesInFopeRule,
			// Token: 0x0400030A RID: 778
			CompressionError,
			// Token: 0x0400030B RID: 779
			AttachmentExtensionContainsInvalidCharacters,
			// Token: 0x0400030C RID: 780
			SubjectCaseSensitive,
			// Token: 0x0400030D RID: 781
			TrackingSearchException,
			// Token: 0x0400030E RID: 782
			DecompressionError,
			// Token: 0x0400030F RID: 783
			DomainLevelAdminNotSupportedByEOP,
			// Token: 0x04000310 RID: 784
			EventTransferredToLegacyExchangeServerHelpDesk,
			// Token: 0x04000311 RID: 785
			AntimalwareAdminAddressValidationsScoped,
			// Token: 0x04000312 RID: 786
			DisabledOutboundConnector,
			// Token: 0x04000313 RID: 787
			BodyCondition,
			// Token: 0x04000314 RID: 788
			FopePolicyRuleContainsIncompatibleConditions,
			// Token: 0x04000315 RID: 789
			PlusUnknownTimeZone,
			// Token: 0x04000316 RID: 790
			InvalidAttachmentExtensionCondition,
			// Token: 0x04000317 RID: 791
			FopePolicyRuleHasProhibitedRegularExpressions,
			// Token: 0x04000318 RID: 792
			HeaderValueMatch,
			// Token: 0x04000319 RID: 793
			FopePolicyRuleSummary,
			// Token: 0x0400031A RID: 794
			BCC,
			// Token: 0x0400031B RID: 795
			BodyExactMatch,
			// Token: 0x0400031C RID: 796
			TenantSkuNotSupported,
			// Token: 0x0400031D RID: 797
			SubjectExactMatch,
			// Token: 0x0400031E RID: 798
			FopePolicyRuleIsPartialMessage,
			// Token: 0x0400031F RID: 799
			AntimalwareVirtualDomainFailure,
			// Token: 0x04000320 RID: 800
			InboundConnectorWithoutSenderIPsAndCert,
			// Token: 0x04000321 RID: 801
			FilenameWordMatchCondition,
			// Token: 0x04000322 RID: 802
			AttachmentFilenames,
			// Token: 0x04000323 RID: 803
			EventSubmittedCrossSiteHelpDesk,
			// Token: 0x04000324 RID: 804
			InboundFopePolicyRuleWithDuplicateDomainName,
			// Token: 0x04000325 RID: 805
			InvalidSecondaryEmailAddresses,
			// Token: 0x04000326 RID: 806
			DistributionListExpanded,
			// Token: 0x04000327 RID: 807
			NoValidRecipientDomainNameExistsInRecipientDomainCondition,
			// Token: 0x04000328 RID: 808
			FopePolicyConsolidationList,
			// Token: 0x04000329 RID: 809
			AttachmentExtensions,
			// Token: 0x0400032A RID: 810
			MaximumRecipientNumber,
			// Token: 0x0400032B RID: 811
			EventDeliveredInboxRule,
			// Token: 0x0400032C RID: 812
			DomainScopedRuleContainsInvalidDomainNames,
			// Token: 0x0400032D RID: 813
			InvalidDomainNameInConnectorSetting,
			// Token: 0x0400032E RID: 814
			OutboundDomainScopedConnectorsMigrated,
			// Token: 0x0400032F RID: 815
			TenantSkuFilteringNotSupported,
			// Token: 0x04000330 RID: 816
			FopePolicyRuleHasWordsThatExceedMaximumLength,
			// Token: 0x04000331 RID: 817
			MinusUnknownTimeZone,
			// Token: 0x04000332 RID: 818
			AntimalwareTruncation,
			// Token: 0x04000333 RID: 819
			ExamineArchivesProperty,
			// Token: 0x04000334 RID: 820
			InvalidDomainNameInHipaaDomainSetting,
			// Token: 0x04000335 RID: 821
			UnableToAddUserToDistributionGroup,
			// Token: 0x04000336 RID: 822
			Redirect,
			// Token: 0x04000337 RID: 823
			HipaaPolicyMigrated,
			// Token: 0x04000338 RID: 824
			EventResolvedWithDetailsHelpDesk,
			// Token: 0x04000339 RID: 825
			InboundConnectorsWithSpamOrConnectionFilteringDisabledMigrated,
			// Token: 0x0400033A RID: 826
			SenderDomainConditionContainsInvalidDomainNames,
			// Token: 0x0400033B RID: 827
			FopePolicyRuleIsSkippableAntiSpamRule,
			// Token: 0x0400033C RID: 828
			NoValidSenderDomainNameExistsInSenderDomainCondition,
			// Token: 0x0400033D RID: 829
			EventExpanded,
			// Token: 0x0400033E RID: 830
			FopePolicyRuleHasUnrecognizedAction,
			// Token: 0x0400033F RID: 831
			EventSmtpSendHelpDesk,
			// Token: 0x04000340 RID: 832
			SenderIpAddresses,
			// Token: 0x04000341 RID: 833
			EventQueueRetryHelpDesk,
			// Token: 0x04000342 RID: 834
			HeaderNameMatch,
			// Token: 0x04000343 RID: 835
			EventDelayedAfterTransferToPartnerOrgHelpDesk,
			// Token: 0x04000344 RID: 836
			Description,
			// Token: 0x04000345 RID: 837
			InboundConnectorsWithPolicyFilteringDisabledMigrated,
			// Token: 0x04000346 RID: 838
			EventPendingAfterTransferToPartnerOrgHelpDesk,
			// Token: 0x04000347 RID: 839
			DomainNameTruncated,
			// Token: 0x04000348 RID: 840
			SenderAddress,
			// Token: 0x04000349 RID: 841
			BodyExactMatchCondition
		}
	}
}
