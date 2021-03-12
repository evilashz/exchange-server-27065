using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel.EnumTypes
{
	// Token: 0x02000005 RID: 5
	internal static class EnumStrings
	{
		// Token: 0x06000269 RID: 617 RVA: 0x000076C8 File Offset: 0x000058C8
		static EnumStrings()
		{
			EnumStrings.stringIDs.Add(2831161206U, "CopyStatusDisconnected");
			EnumStrings.stringIDs.Add(2979702410U, "Inbox");
			EnumStrings.stringIDs.Add(3692015522U, "G711");
			EnumStrings.stringIDs.Add(3707194053U, "ServerRoleExtendedRole3");
			EnumStrings.stringIDs.Add(2125756541U, "ServerRoleMailbox");
			EnumStrings.stringIDs.Add(3194934827U, "ServerRoleUnifiedMessaging");
			EnumStrings.stringIDs.Add(1208301054U, "CopyStatusMounting");
			EnumStrings.stringIDs.Add(62599113U, "GroupNamingPolicyOffice");
			EnumStrings.stringIDs.Add(3725493575U, "WellKnownRecipientTypeMailGroups");
			EnumStrings.stringIDs.Add(4134731995U, "EnterpriseTrialEdition");
			EnumStrings.stringIDs.Add(1377545167U, "ADAttributeCustomAttribute1");
			EnumStrings.stringIDs.Add(756854696U, "ServerRoleEdge");
			EnumStrings.stringIDs.Add(2191591450U, "CoexistenceTrialEdition");
			EnumStrings.stringIDs.Add(172810921U, "ServerRoleHubTransport");
			EnumStrings.stringIDs.Add(1016721882U, "ADAttributeLastName");
			EnumStrings.stringIDs.Add(107906018U, "TeamMailboxRecipientTypeDetails");
			EnumStrings.stringIDs.Add(2385442291U, "ContentIndexStatusDisabled");
			EnumStrings.stringIDs.Add(2476473719U, "CcRecipientType");
			EnumStrings.stringIDs.Add(2328530086U, "FallbackIgnore");
			EnumStrings.stringIDs.Add(1970247521U, "MailEnabledUniversalSecurityGroupRecipientTypeDetails");
			EnumStrings.stringIDs.Add(623586765U, "CopyStatusMounted");
			EnumStrings.stringIDs.Add(3115737533U, "Gsm");
			EnumStrings.stringIDs.Add(1924734198U, "GroupNamingPolicyCustomAttribute6");
			EnumStrings.stringIDs.Add(2129610537U, "MessageTypeOof");
			EnumStrings.stringIDs.Add(862838650U, "GroupNamingPolicyCompany");
			EnumStrings.stringIDs.Add(2654331961U, "SpamFilteringOptionOn");
			EnumStrings.stringIDs.Add(1414246128U, "None");
			EnumStrings.stringIDs.Add(2630084427U, "ConversationHistory");
			EnumStrings.stringIDs.Add(1479682494U, "NoNewCalls");
			EnumStrings.stringIDs.Add(1716515484U, "Adfs");
			EnumStrings.stringIDs.Add(1277957481U, "Certificate");
			EnumStrings.stringIDs.Add(2003039265U, "ConnectorTypePartner");
			EnumStrings.stringIDs.Add(2468414724U, "ADAttributeInitials");
			EnumStrings.stringIDs.Add(135933047U, "AsyncOperationTypeUnknown");
			EnumStrings.stringIDs.Add(1059422816U, "TelExtn");
			EnumStrings.stringIDs.Add(2913015079U, "Authoritative");
			EnumStrings.stringIDs.Add(2227674028U, "MicrosoftExchangeRecipientTypeDetails");
			EnumStrings.stringIDs.Add(1091035505U, "LiveIdNegotiate");
			EnumStrings.stringIDs.Add(1677492010U, "SpecificUsers");
			EnumStrings.stringIDs.Add(1377545174U, "ADAttributeCustomAttribute8");
			EnumStrings.stringIDs.Add(1690995826U, "WSSecurity");
			EnumStrings.stringIDs.Add(2041595767U, "MessageTypeAutoForward");
			EnumStrings.stringIDs.Add(3211494971U, "Misconfigured");
			EnumStrings.stringIDs.Add(2773964607U, "DatabaseMasterTypeDag");
			EnumStrings.stringIDs.Add(680782013U, "MessageTypeReadReceipt");
			EnumStrings.stringIDs.Add(3341243277U, "MoveToDeletedItems");
			EnumStrings.stringIDs.Add(2611996929U, "RedirectRecipientType");
			EnumStrings.stringIDs.Add(4145265495U, "RemoteUserMailboxTypeDetails");
			EnumStrings.stringIDs.Add(393808047U, "ContentIndexStatusAutoSuspended");
			EnumStrings.stringIDs.Add(3799817423U, "ExternalManagedMailContactTypeDetails");
			EnumStrings.stringIDs.Add(635036566U, "Negotiate");
			EnumStrings.stringIDs.Add(3786203794U, "ServerRoleFrontendTransport");
			EnumStrings.stringIDs.Add(3655046083U, "CopyStatusDisconnectedAndResynchronizing");
			EnumStrings.stringIDs.Add(3811183882U, "Allowed");
			EnumStrings.stringIDs.Add(3197896354U, "GroupNamingPolicyExtensionCustomAttribute3");
			EnumStrings.stringIDs.Add(3050431750U, "ADAttributeName");
			EnumStrings.stringIDs.Add(1484405454U, "Disabled");
			EnumStrings.stringIDs.Add(4060482376U, "ConnectorTypeOnPremises");
			EnumStrings.stringIDs.Add(4072748617U, "MessageTypeApprovalRequest");
			EnumStrings.stringIDs.Add(3115100581U, "RejectUnlessExplicitOverrideActionType");
			EnumStrings.stringIDs.Add(2167560764U, "WellKnownRecipientTypeMailboxUsers");
			EnumStrings.stringIDs.Add(3144351139U, "FallbackReject");
			EnumStrings.stringIDs.Add(444885611U, "RemoteSharedMailboxTypeDetails");
			EnumStrings.stringIDs.Add(1241597555U, "Secured");
			EnumStrings.stringIDs.Add(1182470434U, "MoveToFolder");
			EnumStrings.stringIDs.Add(1377545163U, "ADAttributeCustomAttribute5");
			EnumStrings.stringIDs.Add(4199979286U, "ToRecipientType");
			EnumStrings.stringIDs.Add(3221974997U, "RoleGroupTypeDetails");
			EnumStrings.stringIDs.Add(2966158940U, "Tasks");
			EnumStrings.stringIDs.Add(2173147846U, "DatabaseMasterTypeServer");
			EnumStrings.stringIDs.Add(2338964630U, "UserRecipientTypeDetails");
			EnumStrings.stringIDs.Add(3990056197U, "CopyStatusNonExchangeReplication");
			EnumStrings.stringIDs.Add(1310067130U, "CopyStatusNotConfigured");
			EnumStrings.stringIDs.Add(1010456570U, "DeviceDiscovery");
			EnumStrings.stringIDs.Add(137387861U, "ContactRecipientTypeDetails");
			EnumStrings.stringIDs.Add(1361373610U, "ContentIndexStatusSeeding");
			EnumStrings.stringIDs.Add(2423361114U, "GroupNamingPolicyCustomAttribute11");
			EnumStrings.stringIDs.Add(3168546739U, "Ntlm");
			EnumStrings.stringIDs.Add(590977256U, "SentItems");
			EnumStrings.stringIDs.Add(1377545169U, "ADAttributeCustomAttribute3");
			EnumStrings.stringIDs.Add(3850073087U, "ADAttributePagerNumber");
			EnumStrings.stringIDs.Add(2002903510U, "ADAttributeStreet");
			EnumStrings.stringIDs.Add(2665399355U, "Wma");
			EnumStrings.stringIDs.Add(2703120928U, "GroupNamingPolicyCity");
			EnumStrings.stringIDs.Add(600983985U, "NonIpmRoot");
			EnumStrings.stringIDs.Add(1937417240U, "AsyncOperationTypeExportPST");
			EnumStrings.stringIDs.Add(2889762178U, "UnknownEdition");
			EnumStrings.stringIDs.Add(41715449U, "ModeEnforce");
			EnumStrings.stringIDs.Add(3918497079U, "EvaluationNotEqual");
			EnumStrings.stringIDs.Add(2099880135U, "WellKnownRecipientTypeAllRecipients");
			EnumStrings.stringIDs.Add(1048761747U, "ADAttributeCustomAttribute14");
			EnumStrings.stringIDs.Add(661425765U, "DatabaseMasterTypeUnknown");
			EnumStrings.stringIDs.Add(4137481806U, "ADAttributePhoneNumber");
			EnumStrings.stringIDs.Add(1924734196U, "GroupNamingPolicyCustomAttribute4");
			EnumStrings.stringIDs.Add(553174585U, "StandardTrialEdition");
			EnumStrings.stringIDs.Add(2283186478U, "PersonalFolder");
			EnumStrings.stringIDs.Add(754287197U, "LiveIdBasic");
			EnumStrings.stringIDs.Add(933193541U, "WellKnownRecipientTypeMailUsers");
			EnumStrings.stringIDs.Add(1818643265U, "SystemAttendantMailboxRecipientTypeDetails");
			EnumStrings.stringIDs.Add(1373187244U, "CopyStatusInitializing");
			EnumStrings.stringIDs.Add(1052758952U, "ServerRoleClientAccess");
			EnumStrings.stringIDs.Add(1903193717U, "MessageTypeCalendaring");
			EnumStrings.stringIDs.Add(3694564633U, "SyncIssues");
			EnumStrings.stringIDs.Add(798637440U, "AlwaysEnabled");
			EnumStrings.stringIDs.Add(1631091055U, "ContentIndexStatusUnknown");
			EnumStrings.stringIDs.Add(4263249978U, "SharedMailboxRecipientTypeDetails");
			EnumStrings.stringIDs.Add(3288506612U, "InternalRelay");
			EnumStrings.stringIDs.Add(1474747046U, "CoexistenceEdition");
			EnumStrings.stringIDs.Add(1924734197U, "GroupNamingPolicyCustomAttribute7");
			EnumStrings.stringIDs.Add(629464291U, "Outbox");
			EnumStrings.stringIDs.Add(2614845688U, "ADAttributeCustomAttribute15");
			EnumStrings.stringIDs.Add(1849540794U, "WellKnownRecipientTypeNone");
			EnumStrings.stringIDs.Add(25634710U, "ManagementRelationshipManager");
			EnumStrings.stringIDs.Add(986970413U, "ServerRoleCafeArray");
			EnumStrings.stringIDs.Add(1097129869U, "ExternalManagedGroupTypeDetails");
			EnumStrings.stringIDs.Add(3086386447U, "ArchiveStateNone");
			EnumStrings.stringIDs.Add(2509095413U, "EvaluatedUserSender");
			EnumStrings.stringIDs.Add(1389339898U, "IncidentReportIncludeOriginalMail");
			EnumStrings.stringIDs.Add(65728472U, "GroupNamingPolicyExtensionCustomAttribute1");
			EnumStrings.stringIDs.Add(4289093673U, "ADAttributeEmail");
			EnumStrings.stringIDs.Add(438888054U, "E164");
			EnumStrings.stringIDs.Add(4231482709U, "All");
			EnumStrings.stringIDs.Add(428619956U, "ManagementRelationshipDirectReport");
			EnumStrings.stringIDs.Add(637440764U, "InheritFromDialPlan");
			EnumStrings.stringIDs.Add(3459736224U, "EvaluationEqual");
			EnumStrings.stringIDs.Add(1094750789U, "MailboxPlanTypeDetails");
			EnumStrings.stringIDs.Add(3262572344U, "FallbackWrap");
			EnumStrings.stringIDs.Add(1377545162U, "ADAttributeCustomAttribute4");
			EnumStrings.stringIDs.Add(3367615085U, "ADAttributeDepartment");
			EnumStrings.stringIDs.Add(2944126402U, "SpamFilteringOptionTest");
			EnumStrings.stringIDs.Add(3026477473U, "Private");
			EnumStrings.stringIDs.Add(4226527350U, "ADAttributeCity");
			EnumStrings.stringIDs.Add(104454802U, "DiscoveryMailboxTypeDetails");
			EnumStrings.stringIDs.Add(4260106383U, "ADAttributePOBox");
			EnumStrings.stringIDs.Add(3707194057U, "ServerRoleExtendedRole7");
			EnumStrings.stringIDs.Add(3708929833U, "Everyone");
			EnumStrings.stringIDs.Add(221683052U, "LegacyMailboxRecipientTypeDetails");
			EnumStrings.stringIDs.Add(2182511137U, "ADAttributeFaxNumber");
			EnumStrings.stringIDs.Add(29398792U, "IncidentReportDoNotIncludeOriginalMail");
			EnumStrings.stringIDs.Add(3631693406U, "ExternalUser");
			EnumStrings.stringIDs.Add(1406382714U, "RemoteEquipmentMailboxTypeDetails");
			EnumStrings.stringIDs.Add(696030922U, "Tag");
			EnumStrings.stringIDs.Add(1494101274U, "GroupTypeFlagsBuiltinLocal");
			EnumStrings.stringIDs.Add(3802186670U, "ServerRoleManagementFrontEnd");
			EnumStrings.stringIDs.Add(4088287609U, "GroupNamingPolicyStateOrProvince");
			EnumStrings.stringIDs.Add(920444171U, "ArchiveStateHostedPending");
			EnumStrings.stringIDs.Add(322963092U, "RemoteTeamMailboxRecipientTypeDetails");
			EnumStrings.stringIDs.Add(381216251U, "ADAttributeZipCode");
			EnumStrings.stringIDs.Add(3675904764U, "PermanentlyDelete");
			EnumStrings.stringIDs.Add(2325276717U, "Location");
			EnumStrings.stringIDs.Add(3938481035U, "EquipmentMailboxRecipientTypeDetails");
			EnumStrings.stringIDs.Add(3673730471U, "CopyStatusDismounted");
			EnumStrings.stringIDs.Add(3423767853U, "SipName");
			EnumStrings.stringIDs.Add(3869829980U, "ModeAudit");
			EnumStrings.stringIDs.Add(3641768400U, "DumpsterFolder");
			EnumStrings.stringIDs.Add(1067650092U, "Organizational");
			EnumStrings.stringIDs.Add(2986926906U, "ADAttributeFirstName");
			EnumStrings.stringIDs.Add(407788899U, "ServerRoleSCOM");
			EnumStrings.stringIDs.Add(3613623199U, "DeletedItems");
			EnumStrings.stringIDs.Add(1924734194U, "GroupNamingPolicyCustomAttribute2");
			EnumStrings.stringIDs.Add(1377545168U, "ADAttributeCustomAttribute2");
			EnumStrings.stringIDs.Add(1638178773U, "GroupTypeFlagsDomainLocal");
			EnumStrings.stringIDs.Add(3980237751U, "ServerRoleCentralAdminFrontEnd");
			EnumStrings.stringIDs.Add(2795331228U, "InternalUser");
			EnumStrings.stringIDs.Add(1923042104U, "ContentIndexStatusFailedAndSuspended");
			EnumStrings.stringIDs.Add(3600528589U, "ADAttributeCountry");
			EnumStrings.stringIDs.Add(2030161115U, "SpamFilteringOptionOff");
			EnumStrings.stringIDs.Add(4137211921U, "GroupNamingPolicyTitle");
			EnumStrings.stringIDs.Add(1798370525U, "BccRecipientType");
			EnumStrings.stringIDs.Add(4181674605U, "AsyncOperationTypeImportPST");
			EnumStrings.stringIDs.Add(2736707353U, "RejectUnlessFalsePositiveOverrideActionType");
			EnumStrings.stringIDs.Add(3918345138U, "SpamFilteringActionDelete");
			EnumStrings.stringIDs.Add(1625030180U, "PublicFolderRecipientTypeDetails");
			EnumStrings.stringIDs.Add(685401583U, "SpamFilteringActionAddXHeader");
			EnumStrings.stringIDs.Add(2349327181U, "SpamFilteringActionModifySubject");
			EnumStrings.stringIDs.Add(3268869348U, "ContentIndexStatusHealthyAndUpgrading");
			EnumStrings.stringIDs.Add(4128944152U, "Basic");
			EnumStrings.stringIDs.Add(1855823700U, "Department");
			EnumStrings.stringIDs.Add(3606274629U, "MessageTypeSigned");
			EnumStrings.stringIDs.Add(2638599330U, "WellKnownRecipientTypeMailContacts");
			EnumStrings.stringIDs.Add(2411750738U, "ADAttributeMobileNumber");
			EnumStrings.stringIDs.Add(117825870U, "MessageTypeVoicemail");
			EnumStrings.stringIDs.Add(3689869554U, "MailEnabledUserRecipientTypeDetails");
			EnumStrings.stringIDs.Add(1549653732U, "Mp3");
			EnumStrings.stringIDs.Add(2447598924U, "RejectUnlessSilentOverrideActionType");
			EnumStrings.stringIDs.Add(3674978674U, "GroupNamingPolicyCountryOrRegion");
			EnumStrings.stringIDs.Add(2698858797U, "ServerRoleLanguagePacks");
			EnumStrings.stringIDs.Add(1359519288U, "CopyStatusSinglePageRestore");
			EnumStrings.stringIDs.Add(3376217818U, "MailEnabledNonUniversalGroupRecipientTypeDetails");
			EnumStrings.stringIDs.Add(1123996746U, "SpamFilteringActionJmf");
			EnumStrings.stringIDs.Add(115734878U, "Drafts");
			EnumStrings.stringIDs.Add(3374360575U, "ADAttributeCustomAttribute10");
			EnumStrings.stringIDs.Add(110833865U, "ArchiveStateOnPremise");
			EnumStrings.stringIDs.Add(1966081841U, "UniversalDistributionGroupRecipientTypeDetails");
			EnumStrings.stringIDs.Add(2852597951U, "SpamFilteringActionQuarantine");
			EnumStrings.stringIDs.Add(1377545164U, "ADAttributeCustomAttribute6");
			EnumStrings.stringIDs.Add(252422050U, "GroupTypeFlagsNone");
			EnumStrings.stringIDs.Add(2562345274U, "ContentIndexStatusFailed");
			EnumStrings.stringIDs.Add(2775202161U, "ServerRoleOSP");
			EnumStrings.stringIDs.Add(3689464497U, "ADAttributeOtherFaxNumber");
			EnumStrings.stringIDs.Add(97762286U, "GroupNamingPolicyCustomAttribute15");
			EnumStrings.stringIDs.Add(634395589U, "Enabled");
			EnumStrings.stringIDs.Add(1377545165U, "ADAttributeCustomAttribute7");
			EnumStrings.stringIDs.Add(1924734191U, "GroupNamingPolicyCustomAttribute1");
			EnumStrings.stringIDs.Add(1191186633U, "GroupTypeFlagsUniversal");
			EnumStrings.stringIDs.Add(645477220U, "ADAttributeCustomAttribute11");
			EnumStrings.stringIDs.Add(2391327300U, "GroupNamingPolicyExtensionCustomAttribute5");
			EnumStrings.stringIDs.Add(3707194059U, "ServerRoleExtendedRole5");
			EnumStrings.stringIDs.Add(3309342631U, "OAuth");
			EnumStrings.stringIDs.Add(856583401U, "ADAttributeOtherHomePhoneNumber");
			EnumStrings.stringIDs.Add(665936024U, "ArchiveStateLocal");
			EnumStrings.stringIDs.Add(3586160528U, "GroupNamingPolicyCustomAttribute13");
			EnumStrings.stringIDs.Add(3489169852U, "ComputerRecipientTypeDetails");
			EnumStrings.stringIDs.Add(1582423804U, "LiveIdFba");
			EnumStrings.stringIDs.Add(494686544U, "ADAttributeManager");
			EnumStrings.stringIDs.Add(3162495226U, "ADAttributeOtherPhoneNumber");
			EnumStrings.stringIDs.Add(3464146580U, "ServerRoleFfoWebServices");
			EnumStrings.stringIDs.Add(1575862374U, "ContentIndexStatusCrawling");
			EnumStrings.stringIDs.Add(2835967712U, "MoveToArchive");
			EnumStrings.stringIDs.Add(729925097U, "MonitoringMailboxRecipientTypeDetails");
			EnumStrings.stringIDs.Add(570563164U, "ServerRoleAll");
			EnumStrings.stringIDs.Add(825243359U, "GroupNamingPolicyExtensionCustomAttribute4");
			EnumStrings.stringIDs.Add(3773054995U, "WellKnownRecipientTypeResources");
			EnumStrings.stringIDs.Add(1924734195U, "GroupNamingPolicyCustomAttribute5");
			EnumStrings.stringIDs.Add(872998734U, "WindowsIntegrated");
			EnumStrings.stringIDs.Add(980672066U, "SMTPAddress");
			EnumStrings.stringIDs.Add(1452889642U, "ADAttributeUserLogonName");
			EnumStrings.stringIDs.Add(863112602U, "ADAttributeNotes");
			EnumStrings.stringIDs.Add(1738880682U, "LinkedUserTypeDetails");
			EnumStrings.stringIDs.Add(2303788021U, "PromptForAlias");
			EnumStrings.stringIDs.Add(2227190334U, "NonUniversalGroupRecipientTypeDetails");
			EnumStrings.stringIDs.Add(2634964433U, "ADAttributeTitle");
			EnumStrings.stringIDs.Add(2422734853U, "SIPSecured");
			EnumStrings.stringIDs.Add(4189810048U, "CopyStatusDismounting");
			EnumStrings.stringIDs.Add(729299916U, "CopyStatusServiceDown");
			EnumStrings.stringIDs.Add(1487832074U, "PublicFolderMailboxRecipientTypeDetails");
			EnumStrings.stringIDs.Add(996355914U, "Quarantined");
			EnumStrings.stringIDs.Add(3200416695U, "GroupTypeFlagsSecurityEnabled");
			EnumStrings.stringIDs.Add(2094315795U, "ServerRoleNone");
			EnumStrings.stringIDs.Add(26915469U, "EnterpriseEdition");
			EnumStrings.stringIDs.Add(2045069482U, "AsyncOperationTypeCertExpiry");
			EnumStrings.stringIDs.Add(715964235U, "ExternalPartner");
			EnumStrings.stringIDs.Add(1924734193U, "GroupNamingPolicyCustomAttribute3");
			EnumStrings.stringIDs.Add(1765158362U, "CopyStatusFailedAndSuspended");
			EnumStrings.stringIDs.Add(3949283739U, "AllUsers");
			EnumStrings.stringIDs.Add(2605454650U, "CopyStatusSuspended");
			EnumStrings.stringIDs.Add(4137480277U, "Journal");
			EnumStrings.stringIDs.Add(2321790947U, "StandardEdition");
			EnumStrings.stringIDs.Add(3453679227U, "UndefinedRecipientTypeDetails");
			EnumStrings.stringIDs.Add(2160282563U, "CopyStatusSeedingSource");
			EnumStrings.stringIDs.Add(230388220U, "ModeAuditAndNotify");
			EnumStrings.stringIDs.Add(2891753468U, "ADAttributeCompany");
			EnumStrings.stringIDs.Add(2030715989U, "EvaluatedUserRecipient");
			EnumStrings.stringIDs.Add(4019774802U, "Blocked");
			EnumStrings.stringIDs.Add(2155604814U, "ExternalNonPartner");
			EnumStrings.stringIDs.Add(3815678973U, "MailEnabledContactRecipientTypeDetails");
			EnumStrings.stringIDs.Add(1573777228U, "Unsecured");
			EnumStrings.stringIDs.Add(2472951404U, "ArchiveStateHostedProvisioned");
			EnumStrings.stringIDs.Add(1924734199U, "GroupNamingPolicyCustomAttribute9");
			EnumStrings.stringIDs.Add(1292798904U, "Calendar");
			EnumStrings.stringIDs.Add(3647297993U, "ArbitrationMailboxTypeDetails");
			EnumStrings.stringIDs.Add(3569405894U, "DisabledUserRecipientTypeDetails");
			EnumStrings.stringIDs.Add(1960737953U, "CopyStatusUnknown");
			EnumStrings.stringIDs.Add(142823596U, "LastFirst");
			EnumStrings.stringIDs.Add(2872629304U, "MessageTypePermissionControlled");
			EnumStrings.stringIDs.Add(3598244064U, "RssSubscriptions");
			EnumStrings.stringIDs.Add(4010596708U, "ContentIndexStatusHealthy");
			EnumStrings.stringIDs.Add(1808276634U, "ADAttributeCustomAttribute13");
			EnumStrings.stringIDs.Add(645017541U, "Kerberos");
			EnumStrings.stringIDs.Add(1484668346U, "CopyStatusHealthy");
			EnumStrings.stringIDs.Add(1391517930U, "RoomListGroupTypeDetails");
			EnumStrings.stringIDs.Add(1377545175U, "ADAttributeCustomAttribute9");
			EnumStrings.stringIDs.Add(1536572748U, "ServerRoleCafe");
			EnumStrings.stringIDs.Add(1924734200U, "GroupNamingPolicyCustomAttribute8");
			EnumStrings.stringIDs.Add(3133553171U, "SoftDelete");
			EnumStrings.stringIDs.Add(2171581398U, "ExternalRelay");
			EnumStrings.stringIDs.Add(2300412432U, "FirstLast");
			EnumStrings.stringIDs.Add(1663846227U, "GroupNamingPolicyCustomAttribute14");
			EnumStrings.stringIDs.Add(4189167987U, "GroupTypeFlagsGlobal");
			EnumStrings.stringIDs.Add(1919306754U, "ConferenceRoomMailboxRecipientTypeDetails");
			EnumStrings.stringIDs.Add(1588035907U, "AsyncOperationTypeMailboxRestore");
			EnumStrings.stringIDs.Add(2846264340U, "Unknown");
			EnumStrings.stringIDs.Add(3882899654U, "ADAttributeState");
			EnumStrings.stringIDs.Add(2223810040U, "GroupMailboxRecipientTypeDetails");
			EnumStrings.stringIDs.Add(1927573801U, "ADAttributeOffice");
			EnumStrings.stringIDs.Add(1545501201U, "CopyStatusResynchronizing");
			EnumStrings.stringIDs.Add(1432667858U, "LinkedMailboxRecipientTypeDetails");
			EnumStrings.stringIDs.Add(968858937U, "UniversalSecurityGroupRecipientTypeDetails");
			EnumStrings.stringIDs.Add(242192693U, "ADAttributeCustomAttribute12");
			EnumStrings.stringIDs.Add(1631812413U, "GroupNamingPolicyExtensionCustomAttribute2");
			EnumStrings.stringIDs.Add(1594549261U, "RemoteRoomMailboxTypeDetails");
			EnumStrings.stringIDs.Add(2435266816U, "Title");
			EnumStrings.stringIDs.Add(1605633982U, "MailboxUserRecipientTypeDetails");
			EnumStrings.stringIDs.Add(3707194060U, "ServerRoleExtendedRole4");
			EnumStrings.stringIDs.Add(1922689150U, "ServerRoleProvisionedServer");
			EnumStrings.stringIDs.Add(4255105347U, "SpamFilteringActionRedirect");
			EnumStrings.stringIDs.Add(857277173U, "GroupNamingPolicyCustomAttribute12");
			EnumStrings.stringIDs.Add(562488721U, "MailEnabledUniversalDistributionGroupRecipientTypeDetails");
			EnumStrings.stringIDs.Add(604363629U, "NotifyOnlyActionType");
			EnumStrings.stringIDs.Add(498572210U, "RejectMessageActionType");
			EnumStrings.stringIDs.Add(1587179080U, "CopyStatusMisconfigured");
			EnumStrings.stringIDs.Add(1457839961U, "ADAttributeHomePhoneNumber");
			EnumStrings.stringIDs.Add(1716044995U, "Contacts");
			EnumStrings.stringIDs.Add(3635271833U, "LegacyArchiveJournals");
			EnumStrings.stringIDs.Add(154085973U, "GroupNamingPolicyDepartment");
			EnumStrings.stringIDs.Add(696678862U, "ServerRoleManagementBackEnd");
			EnumStrings.stringIDs.Add(815864422U, "Digest");
			EnumStrings.stringIDs.Add(1702371863U, "AsyncOperationTypeMigration");
			EnumStrings.stringIDs.Add(4015121608U, "CopyStatusFailed");
			EnumStrings.stringIDs.Add(1601836855U, "Notes");
			EnumStrings.stringIDs.Add(3707194054U, "ServerRoleExtendedRole2");
			EnumStrings.stringIDs.Add(273163868U, "NegoEx");
			EnumStrings.stringIDs.Add(3544120613U, "MessageTypeEncrypted");
			EnumStrings.stringIDs.Add(3989445055U, "GroupNamingPolicyCustomAttribute10");
			EnumStrings.stringIDs.Add(2999125469U, "MailEnabledDynamicDistributionGroupRecipientTypeDetails");
			EnumStrings.stringIDs.Add(1056819816U, "ContentIndexStatusSuspended");
			EnumStrings.stringIDs.Add(1099314853U, "Fba");
			EnumStrings.stringIDs.Add(3985647980U, "CopyStatusDisconnectedAndHealthy");
			EnumStrings.stringIDs.Add(3586618070U, "MailEnabledForestContactRecipientTypeDetails");
			EnumStrings.stringIDs.Add(2241039844U, "JunkEmail");
			EnumStrings.stringIDs.Add(1024471425U, "ServerRoleMonitoring");
			EnumStrings.stringIDs.Add(1850977098U, "SystemMailboxRecipientTypeDetails");
			EnumStrings.stringIDs.Add(4022404286U, "GroupNamingPolicyCountryCode");
			EnumStrings.stringIDs.Add(448862132U, "CopyStatusSeeding");
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00009057 File Offset: 0x00007257
		public static LocalizedString CopyStatusDisconnected
		{
			get
			{
				return new LocalizedString("CopyStatusDisconnected", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000906E File Offset: 0x0000726E
		public static LocalizedString Inbox
		{
			get
			{
				return new LocalizedString("Inbox", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00009085 File Offset: 0x00007285
		public static LocalizedString G711
		{
			get
			{
				return new LocalizedString("G711", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000909C File Offset: 0x0000729C
		public static LocalizedString ServerRoleExtendedRole3
		{
			get
			{
				return new LocalizedString("ServerRoleExtendedRole3", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000090B3 File Offset: 0x000072B3
		public static LocalizedString ServerRoleMailbox
		{
			get
			{
				return new LocalizedString("ServerRoleMailbox", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000090CA File Offset: 0x000072CA
		public static LocalizedString ServerRoleUnifiedMessaging
		{
			get
			{
				return new LocalizedString("ServerRoleUnifiedMessaging", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000270 RID: 624 RVA: 0x000090E1 File Offset: 0x000072E1
		public static LocalizedString CopyStatusMounting
		{
			get
			{
				return new LocalizedString("CopyStatusMounting", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000090F8 File Offset: 0x000072F8
		public static LocalizedString GroupNamingPolicyOffice
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyOffice", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000910F File Offset: 0x0000730F
		public static LocalizedString WellKnownRecipientTypeMailGroups
		{
			get
			{
				return new LocalizedString("WellKnownRecipientTypeMailGroups", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00009126 File Offset: 0x00007326
		public static LocalizedString EnterpriseTrialEdition
		{
			get
			{
				return new LocalizedString("EnterpriseTrialEdition", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000913D File Offset: 0x0000733D
		public static LocalizedString ADAttributeCustomAttribute1
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute1", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00009154 File Offset: 0x00007354
		public static LocalizedString ServerRoleEdge
		{
			get
			{
				return new LocalizedString("ServerRoleEdge", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000916B File Offset: 0x0000736B
		public static LocalizedString CoexistenceTrialEdition
		{
			get
			{
				return new LocalizedString("CoexistenceTrialEdition", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00009182 File Offset: 0x00007382
		public static LocalizedString ServerRoleHubTransport
		{
			get
			{
				return new LocalizedString("ServerRoleHubTransport", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00009199 File Offset: 0x00007399
		public static LocalizedString ADAttributeLastName
		{
			get
			{
				return new LocalizedString("ADAttributeLastName", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000091B0 File Offset: 0x000073B0
		public static LocalizedString TeamMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("TeamMailboxRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000091C7 File Offset: 0x000073C7
		public static LocalizedString ContentIndexStatusDisabled
		{
			get
			{
				return new LocalizedString("ContentIndexStatusDisabled", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600027B RID: 635 RVA: 0x000091DE File Offset: 0x000073DE
		public static LocalizedString CcRecipientType
		{
			get
			{
				return new LocalizedString("CcRecipientType", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600027C RID: 636 RVA: 0x000091F5 File Offset: 0x000073F5
		public static LocalizedString FallbackIgnore
		{
			get
			{
				return new LocalizedString("FallbackIgnore", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000920C File Offset: 0x0000740C
		public static LocalizedString MailEnabledUniversalSecurityGroupRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailEnabledUniversalSecurityGroupRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00009223 File Offset: 0x00007423
		public static LocalizedString CopyStatusMounted
		{
			get
			{
				return new LocalizedString("CopyStatusMounted", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000923A File Offset: 0x0000743A
		public static LocalizedString Gsm
		{
			get
			{
				return new LocalizedString("Gsm", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00009251 File Offset: 0x00007451
		public static LocalizedString GroupNamingPolicyCustomAttribute6
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute6", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00009268 File Offset: 0x00007468
		public static LocalizedString MessageTypeOof
		{
			get
			{
				return new LocalizedString("MessageTypeOof", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000927F File Offset: 0x0000747F
		public static LocalizedString GroupNamingPolicyCompany
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCompany", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00009296 File Offset: 0x00007496
		public static LocalizedString SpamFilteringOptionOn
		{
			get
			{
				return new LocalizedString("SpamFilteringOptionOn", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000284 RID: 644 RVA: 0x000092AD File Offset: 0x000074AD
		public static LocalizedString None
		{
			get
			{
				return new LocalizedString("None", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000285 RID: 645 RVA: 0x000092C4 File Offset: 0x000074C4
		public static LocalizedString ConversationHistory
		{
			get
			{
				return new LocalizedString("ConversationHistory", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000092DB File Offset: 0x000074DB
		public static LocalizedString NoNewCalls
		{
			get
			{
				return new LocalizedString("NoNewCalls", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000092F2 File Offset: 0x000074F2
		public static LocalizedString Adfs
		{
			get
			{
				return new LocalizedString("Adfs", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00009309 File Offset: 0x00007509
		public static LocalizedString Certificate
		{
			get
			{
				return new LocalizedString("Certificate", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00009320 File Offset: 0x00007520
		public static LocalizedString ConnectorTypePartner
		{
			get
			{
				return new LocalizedString("ConnectorTypePartner", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00009337 File Offset: 0x00007537
		public static LocalizedString ADAttributeInitials
		{
			get
			{
				return new LocalizedString("ADAttributeInitials", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000934E File Offset: 0x0000754E
		public static LocalizedString AsyncOperationTypeUnknown
		{
			get
			{
				return new LocalizedString("AsyncOperationTypeUnknown", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00009365 File Offset: 0x00007565
		public static LocalizedString TelExtn
		{
			get
			{
				return new LocalizedString("TelExtn", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000937C File Offset: 0x0000757C
		public static LocalizedString Authoritative
		{
			get
			{
				return new LocalizedString("Authoritative", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00009393 File Offset: 0x00007593
		public static LocalizedString MicrosoftExchangeRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MicrosoftExchangeRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600028F RID: 655 RVA: 0x000093AA File Offset: 0x000075AA
		public static LocalizedString LiveIdNegotiate
		{
			get
			{
				return new LocalizedString("LiveIdNegotiate", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000290 RID: 656 RVA: 0x000093C1 File Offset: 0x000075C1
		public static LocalizedString SpecificUsers
		{
			get
			{
				return new LocalizedString("SpecificUsers", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000093D8 File Offset: 0x000075D8
		public static LocalizedString ADAttributeCustomAttribute8
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute8", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000093EF File Offset: 0x000075EF
		public static LocalizedString WSSecurity
		{
			get
			{
				return new LocalizedString("WSSecurity", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00009406 File Offset: 0x00007606
		public static LocalizedString MessageTypeAutoForward
		{
			get
			{
				return new LocalizedString("MessageTypeAutoForward", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000941D File Offset: 0x0000761D
		public static LocalizedString Misconfigured
		{
			get
			{
				return new LocalizedString("Misconfigured", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00009434 File Offset: 0x00007634
		public static LocalizedString DatabaseMasterTypeDag
		{
			get
			{
				return new LocalizedString("DatabaseMasterTypeDag", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000944B File Offset: 0x0000764B
		public static LocalizedString MessageTypeReadReceipt
		{
			get
			{
				return new LocalizedString("MessageTypeReadReceipt", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00009462 File Offset: 0x00007662
		public static LocalizedString MoveToDeletedItems
		{
			get
			{
				return new LocalizedString("MoveToDeletedItems", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00009479 File Offset: 0x00007679
		public static LocalizedString RedirectRecipientType
		{
			get
			{
				return new LocalizedString("RedirectRecipientType", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00009490 File Offset: 0x00007690
		public static LocalizedString RemoteUserMailboxTypeDetails
		{
			get
			{
				return new LocalizedString("RemoteUserMailboxTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600029A RID: 666 RVA: 0x000094A7 File Offset: 0x000076A7
		public static LocalizedString ContentIndexStatusAutoSuspended
		{
			get
			{
				return new LocalizedString("ContentIndexStatusAutoSuspended", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000094BE File Offset: 0x000076BE
		public static LocalizedString ExternalManagedMailContactTypeDetails
		{
			get
			{
				return new LocalizedString("ExternalManagedMailContactTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000094D5 File Offset: 0x000076D5
		public static LocalizedString Negotiate
		{
			get
			{
				return new LocalizedString("Negotiate", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600029D RID: 669 RVA: 0x000094EC File Offset: 0x000076EC
		public static LocalizedString ServerRoleFrontendTransport
		{
			get
			{
				return new LocalizedString("ServerRoleFrontendTransport", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00009503 File Offset: 0x00007703
		public static LocalizedString CopyStatusDisconnectedAndResynchronizing
		{
			get
			{
				return new LocalizedString("CopyStatusDisconnectedAndResynchronizing", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000951A File Offset: 0x0000771A
		public static LocalizedString Allowed
		{
			get
			{
				return new LocalizedString("Allowed", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00009531 File Offset: 0x00007731
		public static LocalizedString GroupNamingPolicyExtensionCustomAttribute3
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyExtensionCustomAttribute3", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00009548 File Offset: 0x00007748
		public static LocalizedString ADAttributeName
		{
			get
			{
				return new LocalizedString("ADAttributeName", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000955F File Offset: 0x0000775F
		public static LocalizedString Disabled
		{
			get
			{
				return new LocalizedString("Disabled", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00009576 File Offset: 0x00007776
		public static LocalizedString ConnectorTypeOnPremises
		{
			get
			{
				return new LocalizedString("ConnectorTypeOnPremises", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000958D File Offset: 0x0000778D
		public static LocalizedString MessageTypeApprovalRequest
		{
			get
			{
				return new LocalizedString("MessageTypeApprovalRequest", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x000095A4 File Offset: 0x000077A4
		public static LocalizedString RejectUnlessExplicitOverrideActionType
		{
			get
			{
				return new LocalizedString("RejectUnlessExplicitOverrideActionType", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x000095BB File Offset: 0x000077BB
		public static LocalizedString WellKnownRecipientTypeMailboxUsers
		{
			get
			{
				return new LocalizedString("WellKnownRecipientTypeMailboxUsers", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000095D2 File Offset: 0x000077D2
		public static LocalizedString FallbackReject
		{
			get
			{
				return new LocalizedString("FallbackReject", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x000095E9 File Offset: 0x000077E9
		public static LocalizedString RemoteSharedMailboxTypeDetails
		{
			get
			{
				return new LocalizedString("RemoteSharedMailboxTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00009600 File Offset: 0x00007800
		public static LocalizedString Secured
		{
			get
			{
				return new LocalizedString("Secured", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00009617 File Offset: 0x00007817
		public static LocalizedString MoveToFolder
		{
			get
			{
				return new LocalizedString("MoveToFolder", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000962E File Offset: 0x0000782E
		public static LocalizedString ADAttributeCustomAttribute5
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute5", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00009645 File Offset: 0x00007845
		public static LocalizedString ToRecipientType
		{
			get
			{
				return new LocalizedString("ToRecipientType", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000965C File Offset: 0x0000785C
		public static LocalizedString RoleGroupTypeDetails
		{
			get
			{
				return new LocalizedString("RoleGroupTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00009673 File Offset: 0x00007873
		public static LocalizedString Tasks
		{
			get
			{
				return new LocalizedString("Tasks", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000968A File Offset: 0x0000788A
		public static LocalizedString DatabaseMasterTypeServer
		{
			get
			{
				return new LocalizedString("DatabaseMasterTypeServer", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x000096A1 File Offset: 0x000078A1
		public static LocalizedString UserRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("UserRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x000096B8 File Offset: 0x000078B8
		public static LocalizedString CopyStatusNonExchangeReplication
		{
			get
			{
				return new LocalizedString("CopyStatusNonExchangeReplication", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000096CF File Offset: 0x000078CF
		public static LocalizedString CopyStatusNotConfigured
		{
			get
			{
				return new LocalizedString("CopyStatusNotConfigured", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000096E6 File Offset: 0x000078E6
		public static LocalizedString DeviceDiscovery
		{
			get
			{
				return new LocalizedString("DeviceDiscovery", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000096FD File Offset: 0x000078FD
		public static LocalizedString ContactRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("ContactRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00009714 File Offset: 0x00007914
		public static LocalizedString ContentIndexStatusSeeding
		{
			get
			{
				return new LocalizedString("ContentIndexStatusSeeding", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000972B File Offset: 0x0000792B
		public static LocalizedString GroupNamingPolicyCustomAttribute11
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute11", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00009742 File Offset: 0x00007942
		public static LocalizedString Ntlm
		{
			get
			{
				return new LocalizedString("Ntlm", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00009759 File Offset: 0x00007959
		public static LocalizedString SentItems
		{
			get
			{
				return new LocalizedString("SentItems", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00009770 File Offset: 0x00007970
		public static LocalizedString ADAttributeCustomAttribute3
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute3", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00009787 File Offset: 0x00007987
		public static LocalizedString ADAttributePagerNumber
		{
			get
			{
				return new LocalizedString("ADAttributePagerNumber", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000979E File Offset: 0x0000799E
		public static LocalizedString ADAttributeStreet
		{
			get
			{
				return new LocalizedString("ADAttributeStreet", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000097B5 File Offset: 0x000079B5
		public static LocalizedString Wma
		{
			get
			{
				return new LocalizedString("Wma", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000097CC File Offset: 0x000079CC
		public static LocalizedString GroupNamingPolicyCity
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCity", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060002BE RID: 702 RVA: 0x000097E3 File Offset: 0x000079E3
		public static LocalizedString NonIpmRoot
		{
			get
			{
				return new LocalizedString("NonIpmRoot", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060002BF RID: 703 RVA: 0x000097FA File Offset: 0x000079FA
		public static LocalizedString AsyncOperationTypeExportPST
		{
			get
			{
				return new LocalizedString("AsyncOperationTypeExportPST", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00009811 File Offset: 0x00007A11
		public static LocalizedString UnknownEdition
		{
			get
			{
				return new LocalizedString("UnknownEdition", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00009828 File Offset: 0x00007A28
		public static LocalizedString ModeEnforce
		{
			get
			{
				return new LocalizedString("ModeEnforce", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000983F File Offset: 0x00007A3F
		public static LocalizedString EvaluationNotEqual
		{
			get
			{
				return new LocalizedString("EvaluationNotEqual", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00009856 File Offset: 0x00007A56
		public static LocalizedString WellKnownRecipientTypeAllRecipients
		{
			get
			{
				return new LocalizedString("WellKnownRecipientTypeAllRecipients", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000986D File Offset: 0x00007A6D
		public static LocalizedString ADAttributeCustomAttribute14
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute14", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00009884 File Offset: 0x00007A84
		public static LocalizedString DatabaseMasterTypeUnknown
		{
			get
			{
				return new LocalizedString("DatabaseMasterTypeUnknown", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000989B File Offset: 0x00007A9B
		public static LocalizedString ADAttributePhoneNumber
		{
			get
			{
				return new LocalizedString("ADAttributePhoneNumber", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000098B2 File Offset: 0x00007AB2
		public static LocalizedString GroupNamingPolicyCustomAttribute4
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute4", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x000098C9 File Offset: 0x00007AC9
		public static LocalizedString StandardTrialEdition
		{
			get
			{
				return new LocalizedString("StandardTrialEdition", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x000098E0 File Offset: 0x00007AE0
		public static LocalizedString PersonalFolder
		{
			get
			{
				return new LocalizedString("PersonalFolder", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060002CA RID: 714 RVA: 0x000098F7 File Offset: 0x00007AF7
		public static LocalizedString LiveIdBasic
		{
			get
			{
				return new LocalizedString("LiveIdBasic", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000990E File Offset: 0x00007B0E
		public static LocalizedString WellKnownRecipientTypeMailUsers
		{
			get
			{
				return new LocalizedString("WellKnownRecipientTypeMailUsers", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00009925 File Offset: 0x00007B25
		public static LocalizedString SystemAttendantMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("SystemAttendantMailboxRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000993C File Offset: 0x00007B3C
		public static LocalizedString CopyStatusInitializing
		{
			get
			{
				return new LocalizedString("CopyStatusInitializing", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00009953 File Offset: 0x00007B53
		public static LocalizedString ServerRoleClientAccess
		{
			get
			{
				return new LocalizedString("ServerRoleClientAccess", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000996A File Offset: 0x00007B6A
		public static LocalizedString MessageTypeCalendaring
		{
			get
			{
				return new LocalizedString("MessageTypeCalendaring", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00009981 File Offset: 0x00007B81
		public static LocalizedString SyncIssues
		{
			get
			{
				return new LocalizedString("SyncIssues", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00009998 File Offset: 0x00007B98
		public static LocalizedString AlwaysEnabled
		{
			get
			{
				return new LocalizedString("AlwaysEnabled", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x000099AF File Offset: 0x00007BAF
		public static LocalizedString ContentIndexStatusUnknown
		{
			get
			{
				return new LocalizedString("ContentIndexStatusUnknown", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x000099C6 File Offset: 0x00007BC6
		public static LocalizedString SharedMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("SharedMailboxRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x000099DD File Offset: 0x00007BDD
		public static LocalizedString InternalRelay
		{
			get
			{
				return new LocalizedString("InternalRelay", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x000099F4 File Offset: 0x00007BF4
		public static LocalizedString CoexistenceEdition
		{
			get
			{
				return new LocalizedString("CoexistenceEdition", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00009A0B File Offset: 0x00007C0B
		public static LocalizedString GroupNamingPolicyCustomAttribute7
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute7", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00009A22 File Offset: 0x00007C22
		public static LocalizedString Outbox
		{
			get
			{
				return new LocalizedString("Outbox", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00009A39 File Offset: 0x00007C39
		public static LocalizedString ADAttributeCustomAttribute15
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute15", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00009A50 File Offset: 0x00007C50
		public static LocalizedString WellKnownRecipientTypeNone
		{
			get
			{
				return new LocalizedString("WellKnownRecipientTypeNone", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00009A67 File Offset: 0x00007C67
		public static LocalizedString ManagementRelationshipManager
		{
			get
			{
				return new LocalizedString("ManagementRelationshipManager", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00009A7E File Offset: 0x00007C7E
		public static LocalizedString ServerRoleCafeArray
		{
			get
			{
				return new LocalizedString("ServerRoleCafeArray", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00009A95 File Offset: 0x00007C95
		public static LocalizedString ExternalManagedGroupTypeDetails
		{
			get
			{
				return new LocalizedString("ExternalManagedGroupTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00009AAC File Offset: 0x00007CAC
		public static LocalizedString ArchiveStateNone
		{
			get
			{
				return new LocalizedString("ArchiveStateNone", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00009AC3 File Offset: 0x00007CC3
		public static LocalizedString EvaluatedUserSender
		{
			get
			{
				return new LocalizedString("EvaluatedUserSender", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00009ADA File Offset: 0x00007CDA
		public static LocalizedString IncidentReportIncludeOriginalMail
		{
			get
			{
				return new LocalizedString("IncidentReportIncludeOriginalMail", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00009AF1 File Offset: 0x00007CF1
		public static LocalizedString GroupNamingPolicyExtensionCustomAttribute1
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyExtensionCustomAttribute1", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00009B08 File Offset: 0x00007D08
		public static LocalizedString ADAttributeEmail
		{
			get
			{
				return new LocalizedString("ADAttributeEmail", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00009B1F File Offset: 0x00007D1F
		public static LocalizedString E164
		{
			get
			{
				return new LocalizedString("E164", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00009B36 File Offset: 0x00007D36
		public static LocalizedString All
		{
			get
			{
				return new LocalizedString("All", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00009B4D File Offset: 0x00007D4D
		public static LocalizedString ManagementRelationshipDirectReport
		{
			get
			{
				return new LocalizedString("ManagementRelationshipDirectReport", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00009B64 File Offset: 0x00007D64
		public static LocalizedString InheritFromDialPlan
		{
			get
			{
				return new LocalizedString("InheritFromDialPlan", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00009B7B File Offset: 0x00007D7B
		public static LocalizedString EvaluationEqual
		{
			get
			{
				return new LocalizedString("EvaluationEqual", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00009B92 File Offset: 0x00007D92
		public static LocalizedString MailboxPlanTypeDetails
		{
			get
			{
				return new LocalizedString("MailboxPlanTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00009BA9 File Offset: 0x00007DA9
		public static LocalizedString FallbackWrap
		{
			get
			{
				return new LocalizedString("FallbackWrap", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00009BC0 File Offset: 0x00007DC0
		public static LocalizedString ADAttributeCustomAttribute4
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute4", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00009BD7 File Offset: 0x00007DD7
		public static LocalizedString ADAttributeDepartment
		{
			get
			{
				return new LocalizedString("ADAttributeDepartment", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00009BEE File Offset: 0x00007DEE
		public static LocalizedString SpamFilteringOptionTest
		{
			get
			{
				return new LocalizedString("SpamFilteringOptionTest", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00009C05 File Offset: 0x00007E05
		public static LocalizedString Private
		{
			get
			{
				return new LocalizedString("Private", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00009C1C File Offset: 0x00007E1C
		public static LocalizedString ADAttributeCity
		{
			get
			{
				return new LocalizedString("ADAttributeCity", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00009C33 File Offset: 0x00007E33
		public static LocalizedString DiscoveryMailboxTypeDetails
		{
			get
			{
				return new LocalizedString("DiscoveryMailboxTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00009C4A File Offset: 0x00007E4A
		public static LocalizedString ADAttributePOBox
		{
			get
			{
				return new LocalizedString("ADAttributePOBox", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00009C61 File Offset: 0x00007E61
		public static LocalizedString ServerRoleExtendedRole7
		{
			get
			{
				return new LocalizedString("ServerRoleExtendedRole7", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00009C78 File Offset: 0x00007E78
		public static LocalizedString Everyone
		{
			get
			{
				return new LocalizedString("Everyone", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00009C8F File Offset: 0x00007E8F
		public static LocalizedString LegacyMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("LegacyMailboxRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00009CA6 File Offset: 0x00007EA6
		public static LocalizedString ADAttributeFaxNumber
		{
			get
			{
				return new LocalizedString("ADAttributeFaxNumber", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00009CBD File Offset: 0x00007EBD
		public static LocalizedString IncidentReportDoNotIncludeOriginalMail
		{
			get
			{
				return new LocalizedString("IncidentReportDoNotIncludeOriginalMail", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public static LocalizedString ExternalUser
		{
			get
			{
				return new LocalizedString("ExternalUser", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00009CEB File Offset: 0x00007EEB
		public static LocalizedString RemoteEquipmentMailboxTypeDetails
		{
			get
			{
				return new LocalizedString("RemoteEquipmentMailboxTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00009D02 File Offset: 0x00007F02
		public static LocalizedString Tag
		{
			get
			{
				return new LocalizedString("Tag", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00009D19 File Offset: 0x00007F19
		public static LocalizedString GroupTypeFlagsBuiltinLocal
		{
			get
			{
				return new LocalizedString("GroupTypeFlagsBuiltinLocal", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00009D30 File Offset: 0x00007F30
		public static LocalizedString ServerRoleManagementFrontEnd
		{
			get
			{
				return new LocalizedString("ServerRoleManagementFrontEnd", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00009D47 File Offset: 0x00007F47
		public static LocalizedString GroupNamingPolicyStateOrProvince
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyStateOrProvince", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00009D5E File Offset: 0x00007F5E
		public static LocalizedString ArchiveStateHostedPending
		{
			get
			{
				return new LocalizedString("ArchiveStateHostedPending", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00009D75 File Offset: 0x00007F75
		public static LocalizedString RemoteTeamMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("RemoteTeamMailboxRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00009D8C File Offset: 0x00007F8C
		public static LocalizedString ADAttributeZipCode
		{
			get
			{
				return new LocalizedString("ADAttributeZipCode", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00009DA3 File Offset: 0x00007FA3
		public static LocalizedString PermanentlyDelete
		{
			get
			{
				return new LocalizedString("PermanentlyDelete", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00009DBA File Offset: 0x00007FBA
		public static LocalizedString Location
		{
			get
			{
				return new LocalizedString("Location", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00009DD1 File Offset: 0x00007FD1
		public static LocalizedString EquipmentMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("EquipmentMailboxRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00009DE8 File Offset: 0x00007FE8
		public static LocalizedString CopyStatusDismounted
		{
			get
			{
				return new LocalizedString("CopyStatusDismounted", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00009DFF File Offset: 0x00007FFF
		public static LocalizedString SipName
		{
			get
			{
				return new LocalizedString("SipName", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00009E16 File Offset: 0x00008016
		public static LocalizedString ModeAudit
		{
			get
			{
				return new LocalizedString("ModeAudit", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00009E2D File Offset: 0x0000802D
		public static LocalizedString DumpsterFolder
		{
			get
			{
				return new LocalizedString("DumpsterFolder", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00009E44 File Offset: 0x00008044
		public static LocalizedString Organizational
		{
			get
			{
				return new LocalizedString("Organizational", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00009E5B File Offset: 0x0000805B
		public static LocalizedString ADAttributeFirstName
		{
			get
			{
				return new LocalizedString("ADAttributeFirstName", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00009E72 File Offset: 0x00008072
		public static LocalizedString ServerRoleSCOM
		{
			get
			{
				return new LocalizedString("ServerRoleSCOM", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00009E89 File Offset: 0x00008089
		public static LocalizedString DeletedItems
		{
			get
			{
				return new LocalizedString("DeletedItems", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00009EA0 File Offset: 0x000080A0
		public static LocalizedString GroupNamingPolicyCustomAttribute2
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute2", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00009EB7 File Offset: 0x000080B7
		public static LocalizedString ADAttributeCustomAttribute2
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute2", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00009ECE File Offset: 0x000080CE
		public static LocalizedString GroupTypeFlagsDomainLocal
		{
			get
			{
				return new LocalizedString("GroupTypeFlagsDomainLocal", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00009EE5 File Offset: 0x000080E5
		public static LocalizedString ServerRoleCentralAdminFrontEnd
		{
			get
			{
				return new LocalizedString("ServerRoleCentralAdminFrontEnd", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00009EFC File Offset: 0x000080FC
		public static LocalizedString InternalUser
		{
			get
			{
				return new LocalizedString("InternalUser", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00009F13 File Offset: 0x00008113
		public static LocalizedString ContentIndexStatusFailedAndSuspended
		{
			get
			{
				return new LocalizedString("ContentIndexStatusFailedAndSuspended", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00009F2A File Offset: 0x0000812A
		public static LocalizedString ADAttributeCountry
		{
			get
			{
				return new LocalizedString("ADAttributeCountry", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00009F41 File Offset: 0x00008141
		public static LocalizedString SpamFilteringOptionOff
		{
			get
			{
				return new LocalizedString("SpamFilteringOptionOff", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00009F58 File Offset: 0x00008158
		public static LocalizedString GroupNamingPolicyTitle
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyTitle", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00009F6F File Offset: 0x0000816F
		public static LocalizedString BccRecipientType
		{
			get
			{
				return new LocalizedString("BccRecipientType", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00009F86 File Offset: 0x00008186
		public static LocalizedString AsyncOperationTypeImportPST
		{
			get
			{
				return new LocalizedString("AsyncOperationTypeImportPST", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00009F9D File Offset: 0x0000819D
		public static LocalizedString RejectUnlessFalsePositiveOverrideActionType
		{
			get
			{
				return new LocalizedString("RejectUnlessFalsePositiveOverrideActionType", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00009FB4 File Offset: 0x000081B4
		public static LocalizedString SpamFilteringActionDelete
		{
			get
			{
				return new LocalizedString("SpamFilteringActionDelete", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00009FCB File Offset: 0x000081CB
		public static LocalizedString PublicFolderRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("PublicFolderRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00009FE2 File Offset: 0x000081E2
		public static LocalizedString SpamFilteringActionAddXHeader
		{
			get
			{
				return new LocalizedString("SpamFilteringActionAddXHeader", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00009FF9 File Offset: 0x000081F9
		public static LocalizedString SpamFilteringActionModifySubject
		{
			get
			{
				return new LocalizedString("SpamFilteringActionModifySubject", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000A010 File Offset: 0x00008210
		public static LocalizedString ContentIndexStatusHealthyAndUpgrading
		{
			get
			{
				return new LocalizedString("ContentIndexStatusHealthyAndUpgrading", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000A027 File Offset: 0x00008227
		public static LocalizedString Basic
		{
			get
			{
				return new LocalizedString("Basic", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000A03E File Offset: 0x0000823E
		public static LocalizedString Department
		{
			get
			{
				return new LocalizedString("Department", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000A055 File Offset: 0x00008255
		public static LocalizedString MessageTypeSigned
		{
			get
			{
				return new LocalizedString("MessageTypeSigned", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000A06C File Offset: 0x0000826C
		public static LocalizedString WellKnownRecipientTypeMailContacts
		{
			get
			{
				return new LocalizedString("WellKnownRecipientTypeMailContacts", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000A083 File Offset: 0x00008283
		public static LocalizedString ADAttributeMobileNumber
		{
			get
			{
				return new LocalizedString("ADAttributeMobileNumber", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000A09A File Offset: 0x0000829A
		public static LocalizedString MessageTypeVoicemail
		{
			get
			{
				return new LocalizedString("MessageTypeVoicemail", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000A0B1 File Offset: 0x000082B1
		public static LocalizedString MailEnabledUserRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailEnabledUserRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000A0C8 File Offset: 0x000082C8
		public static LocalizedString Mp3
		{
			get
			{
				return new LocalizedString("Mp3", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000A0DF File Offset: 0x000082DF
		public static LocalizedString RejectUnlessSilentOverrideActionType
		{
			get
			{
				return new LocalizedString("RejectUnlessSilentOverrideActionType", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000A0F6 File Offset: 0x000082F6
		public static LocalizedString GroupNamingPolicyCountryOrRegion
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCountryOrRegion", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000A10D File Offset: 0x0000830D
		public static LocalizedString ServerRoleLanguagePacks
		{
			get
			{
				return new LocalizedString("ServerRoleLanguagePacks", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000A124 File Offset: 0x00008324
		public static LocalizedString CopyStatusSinglePageRestore
		{
			get
			{
				return new LocalizedString("CopyStatusSinglePageRestore", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000A13B File Offset: 0x0000833B
		public static LocalizedString MailEnabledNonUniversalGroupRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailEnabledNonUniversalGroupRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000A152 File Offset: 0x00008352
		public static LocalizedString SpamFilteringActionJmf
		{
			get
			{
				return new LocalizedString("SpamFilteringActionJmf", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000A169 File Offset: 0x00008369
		public static LocalizedString Drafts
		{
			get
			{
				return new LocalizedString("Drafts", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000A180 File Offset: 0x00008380
		public static LocalizedString ADAttributeCustomAttribute10
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute10", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000A197 File Offset: 0x00008397
		public static LocalizedString ArchiveStateOnPremise
		{
			get
			{
				return new LocalizedString("ArchiveStateOnPremise", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000A1B0 File Offset: 0x000083B0
		public static LocalizedString UnsupportServerEdition(string edition)
		{
			return new LocalizedString("UnsupportServerEdition", EnumStrings.ResourceManager, new object[]
			{
				edition
			});
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000A1D8 File Offset: 0x000083D8
		public static LocalizedString UniversalDistributionGroupRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("UniversalDistributionGroupRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000A1EF File Offset: 0x000083EF
		public static LocalizedString SpamFilteringActionQuarantine
		{
			get
			{
				return new LocalizedString("SpamFilteringActionQuarantine", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000A206 File Offset: 0x00008406
		public static LocalizedString ADAttributeCustomAttribute6
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute6", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000A21D File Offset: 0x0000841D
		public static LocalizedString GroupTypeFlagsNone
		{
			get
			{
				return new LocalizedString("GroupTypeFlagsNone", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000A234 File Offset: 0x00008434
		public static LocalizedString ContentIndexStatusFailed
		{
			get
			{
				return new LocalizedString("ContentIndexStatusFailed", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000A24B File Offset: 0x0000844B
		public static LocalizedString ServerRoleOSP
		{
			get
			{
				return new LocalizedString("ServerRoleOSP", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000A262 File Offset: 0x00008462
		public static LocalizedString ADAttributeOtherFaxNumber
		{
			get
			{
				return new LocalizedString("ADAttributeOtherFaxNumber", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000A279 File Offset: 0x00008479
		public static LocalizedString GroupNamingPolicyCustomAttribute15
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute15", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000A290 File Offset: 0x00008490
		public static LocalizedString Enabled
		{
			get
			{
				return new LocalizedString("Enabled", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000A2A7 File Offset: 0x000084A7
		public static LocalizedString ADAttributeCustomAttribute7
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute7", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000A2BE File Offset: 0x000084BE
		public static LocalizedString GroupNamingPolicyCustomAttribute1
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute1", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000A2D5 File Offset: 0x000084D5
		public static LocalizedString GroupTypeFlagsUniversal
		{
			get
			{
				return new LocalizedString("GroupTypeFlagsUniversal", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000A2EC File Offset: 0x000084EC
		public static LocalizedString ADAttributeCustomAttribute11
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute11", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000A303 File Offset: 0x00008503
		public static LocalizedString GroupNamingPolicyExtensionCustomAttribute5
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyExtensionCustomAttribute5", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000A31A File Offset: 0x0000851A
		public static LocalizedString ServerRoleExtendedRole5
		{
			get
			{
				return new LocalizedString("ServerRoleExtendedRole5", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000A331 File Offset: 0x00008531
		public static LocalizedString OAuth
		{
			get
			{
				return new LocalizedString("OAuth", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000A348 File Offset: 0x00008548
		public static LocalizedString ADAttributeOtherHomePhoneNumber
		{
			get
			{
				return new LocalizedString("ADAttributeOtherHomePhoneNumber", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000A35F File Offset: 0x0000855F
		public static LocalizedString ArchiveStateLocal
		{
			get
			{
				return new LocalizedString("ArchiveStateLocal", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000A376 File Offset: 0x00008576
		public static LocalizedString GroupNamingPolicyCustomAttribute13
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute13", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000A38D File Offset: 0x0000858D
		public static LocalizedString ComputerRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("ComputerRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000A3A4 File Offset: 0x000085A4
		public static LocalizedString LiveIdFba
		{
			get
			{
				return new LocalizedString("LiveIdFba", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000A3BB File Offset: 0x000085BB
		public static LocalizedString ADAttributeManager
		{
			get
			{
				return new LocalizedString("ADAttributeManager", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000A3D2 File Offset: 0x000085D2
		public static LocalizedString ADAttributeOtherPhoneNumber
		{
			get
			{
				return new LocalizedString("ADAttributeOtherPhoneNumber", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000A3E9 File Offset: 0x000085E9
		public static LocalizedString ServerRoleFfoWebServices
		{
			get
			{
				return new LocalizedString("ServerRoleFfoWebServices", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000A400 File Offset: 0x00008600
		public static LocalizedString ContentIndexStatusCrawling
		{
			get
			{
				return new LocalizedString("ContentIndexStatusCrawling", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000A417 File Offset: 0x00008617
		public static LocalizedString MoveToArchive
		{
			get
			{
				return new LocalizedString("MoveToArchive", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000A42E File Offset: 0x0000862E
		public static LocalizedString MonitoringMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MonitoringMailboxRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000A445 File Offset: 0x00008645
		public static LocalizedString ServerRoleAll
		{
			get
			{
				return new LocalizedString("ServerRoleAll", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000A45C File Offset: 0x0000865C
		public static LocalizedString GroupNamingPolicyExtensionCustomAttribute4
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyExtensionCustomAttribute4", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000A473 File Offset: 0x00008673
		public static LocalizedString WellKnownRecipientTypeResources
		{
			get
			{
				return new LocalizedString("WellKnownRecipientTypeResources", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000A48A File Offset: 0x0000868A
		public static LocalizedString GroupNamingPolicyCustomAttribute5
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute5", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000A4A1 File Offset: 0x000086A1
		public static LocalizedString WindowsIntegrated
		{
			get
			{
				return new LocalizedString("WindowsIntegrated", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000A4B8 File Offset: 0x000086B8
		public static LocalizedString SMTPAddress
		{
			get
			{
				return new LocalizedString("SMTPAddress", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000A4CF File Offset: 0x000086CF
		public static LocalizedString ADAttributeUserLogonName
		{
			get
			{
				return new LocalizedString("ADAttributeUserLogonName", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000A4E6 File Offset: 0x000086E6
		public static LocalizedString ADAttributeNotes
		{
			get
			{
				return new LocalizedString("ADAttributeNotes", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000A4FD File Offset: 0x000086FD
		public static LocalizedString LinkedUserTypeDetails
		{
			get
			{
				return new LocalizedString("LinkedUserTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000A514 File Offset: 0x00008714
		public static LocalizedString PromptForAlias
		{
			get
			{
				return new LocalizedString("PromptForAlias", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000A52B File Offset: 0x0000872B
		public static LocalizedString NonUniversalGroupRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("NonUniversalGroupRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000A542 File Offset: 0x00008742
		public static LocalizedString ADAttributeTitle
		{
			get
			{
				return new LocalizedString("ADAttributeTitle", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000A559 File Offset: 0x00008759
		public static LocalizedString SIPSecured
		{
			get
			{
				return new LocalizedString("SIPSecured", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000A570 File Offset: 0x00008770
		public static LocalizedString CopyStatusDismounting
		{
			get
			{
				return new LocalizedString("CopyStatusDismounting", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000A587 File Offset: 0x00008787
		public static LocalizedString CopyStatusServiceDown
		{
			get
			{
				return new LocalizedString("CopyStatusServiceDown", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000A59E File Offset: 0x0000879E
		public static LocalizedString PublicFolderMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("PublicFolderMailboxRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000A5B5 File Offset: 0x000087B5
		public static LocalizedString Quarantined
		{
			get
			{
				return new LocalizedString("Quarantined", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000A5CC File Offset: 0x000087CC
		public static LocalizedString GroupTypeFlagsSecurityEnabled
		{
			get
			{
				return new LocalizedString("GroupTypeFlagsSecurityEnabled", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000A5E3 File Offset: 0x000087E3
		public static LocalizedString ServerRoleNone
		{
			get
			{
				return new LocalizedString("ServerRoleNone", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000A5FA File Offset: 0x000087FA
		public static LocalizedString EnterpriseEdition
		{
			get
			{
				return new LocalizedString("EnterpriseEdition", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000A611 File Offset: 0x00008811
		public static LocalizedString AsyncOperationTypeCertExpiry
		{
			get
			{
				return new LocalizedString("AsyncOperationTypeCertExpiry", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000A628 File Offset: 0x00008828
		public static LocalizedString ExternalPartner
		{
			get
			{
				return new LocalizedString("ExternalPartner", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000A63F File Offset: 0x0000883F
		public static LocalizedString GroupNamingPolicyCustomAttribute3
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute3", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000A656 File Offset: 0x00008856
		public static LocalizedString CopyStatusFailedAndSuspended
		{
			get
			{
				return new LocalizedString("CopyStatusFailedAndSuspended", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000A66D File Offset: 0x0000886D
		public static LocalizedString AllUsers
		{
			get
			{
				return new LocalizedString("AllUsers", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000A684 File Offset: 0x00008884
		public static LocalizedString CopyStatusSuspended
		{
			get
			{
				return new LocalizedString("CopyStatusSuspended", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000A69B File Offset: 0x0000889B
		public static LocalizedString Journal
		{
			get
			{
				return new LocalizedString("Journal", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000A6B2 File Offset: 0x000088B2
		public static LocalizedString StandardEdition
		{
			get
			{
				return new LocalizedString("StandardEdition", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000A6C9 File Offset: 0x000088C9
		public static LocalizedString UndefinedRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("UndefinedRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000A6E0 File Offset: 0x000088E0
		public static LocalizedString CopyStatusSeedingSource
		{
			get
			{
				return new LocalizedString("CopyStatusSeedingSource", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000A6F7 File Offset: 0x000088F7
		public static LocalizedString ModeAuditAndNotify
		{
			get
			{
				return new LocalizedString("ModeAuditAndNotify", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000A70E File Offset: 0x0000890E
		public static LocalizedString ADAttributeCompany
		{
			get
			{
				return new LocalizedString("ADAttributeCompany", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000A725 File Offset: 0x00008925
		public static LocalizedString EvaluatedUserRecipient
		{
			get
			{
				return new LocalizedString("EvaluatedUserRecipient", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000A73C File Offset: 0x0000893C
		public static LocalizedString Blocked
		{
			get
			{
				return new LocalizedString("Blocked", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000A753 File Offset: 0x00008953
		public static LocalizedString ExternalNonPartner
		{
			get
			{
				return new LocalizedString("ExternalNonPartner", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000A76A File Offset: 0x0000896A
		public static LocalizedString MailEnabledContactRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailEnabledContactRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000A781 File Offset: 0x00008981
		public static LocalizedString Unsecured
		{
			get
			{
				return new LocalizedString("Unsecured", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000A798 File Offset: 0x00008998
		public static LocalizedString ArchiveStateHostedProvisioned
		{
			get
			{
				return new LocalizedString("ArchiveStateHostedProvisioned", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000A7AF File Offset: 0x000089AF
		public static LocalizedString GroupNamingPolicyCustomAttribute9
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute9", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000A7C6 File Offset: 0x000089C6
		public static LocalizedString Calendar
		{
			get
			{
				return new LocalizedString("Calendar", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000A7DD File Offset: 0x000089DD
		public static LocalizedString ArbitrationMailboxTypeDetails
		{
			get
			{
				return new LocalizedString("ArbitrationMailboxTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000A7F4 File Offset: 0x000089F4
		public static LocalizedString DisabledUserRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("DisabledUserRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000A80B File Offset: 0x00008A0B
		public static LocalizedString CopyStatusUnknown
		{
			get
			{
				return new LocalizedString("CopyStatusUnknown", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000A822 File Offset: 0x00008A22
		public static LocalizedString LastFirst
		{
			get
			{
				return new LocalizedString("LastFirst", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000A839 File Offset: 0x00008A39
		public static LocalizedString MessageTypePermissionControlled
		{
			get
			{
				return new LocalizedString("MessageTypePermissionControlled", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000A850 File Offset: 0x00008A50
		public static LocalizedString RssSubscriptions
		{
			get
			{
				return new LocalizedString("RssSubscriptions", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000A867 File Offset: 0x00008A67
		public static LocalizedString ContentIndexStatusHealthy
		{
			get
			{
				return new LocalizedString("ContentIndexStatusHealthy", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000A87E File Offset: 0x00008A7E
		public static LocalizedString ADAttributeCustomAttribute13
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute13", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000A895 File Offset: 0x00008A95
		public static LocalizedString Kerberos
		{
			get
			{
				return new LocalizedString("Kerberos", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000A8AC File Offset: 0x00008AAC
		public static LocalizedString CopyStatusHealthy
		{
			get
			{
				return new LocalizedString("CopyStatusHealthy", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000A8C3 File Offset: 0x00008AC3
		public static LocalizedString RoomListGroupTypeDetails
		{
			get
			{
				return new LocalizedString("RoomListGroupTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000A8DA File Offset: 0x00008ADA
		public static LocalizedString ADAttributeCustomAttribute9
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute9", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000A8F1 File Offset: 0x00008AF1
		public static LocalizedString ServerRoleCafe
		{
			get
			{
				return new LocalizedString("ServerRoleCafe", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000A908 File Offset: 0x00008B08
		public static LocalizedString GroupNamingPolicyCustomAttribute8
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute8", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000A91F File Offset: 0x00008B1F
		public static LocalizedString SoftDelete
		{
			get
			{
				return new LocalizedString("SoftDelete", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000A936 File Offset: 0x00008B36
		public static LocalizedString ExternalRelay
		{
			get
			{
				return new LocalizedString("ExternalRelay", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000A94D File Offset: 0x00008B4D
		public static LocalizedString FirstLast
		{
			get
			{
				return new LocalizedString("FirstLast", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000A964 File Offset: 0x00008B64
		public static LocalizedString GroupNamingPolicyCustomAttribute14
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute14", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000A97B File Offset: 0x00008B7B
		public static LocalizedString GroupTypeFlagsGlobal
		{
			get
			{
				return new LocalizedString("GroupTypeFlagsGlobal", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000A992 File Offset: 0x00008B92
		public static LocalizedString ConferenceRoomMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("ConferenceRoomMailboxRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000A9A9 File Offset: 0x00008BA9
		public static LocalizedString AsyncOperationTypeMailboxRestore
		{
			get
			{
				return new LocalizedString("AsyncOperationTypeMailboxRestore", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		public static LocalizedString Unknown
		{
			get
			{
				return new LocalizedString("Unknown", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000A9D7 File Offset: 0x00008BD7
		public static LocalizedString ADAttributeState
		{
			get
			{
				return new LocalizedString("ADAttributeState", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000A9EE File Offset: 0x00008BEE
		public static LocalizedString GroupMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("GroupMailboxRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000AA05 File Offset: 0x00008C05
		public static LocalizedString ADAttributeOffice
		{
			get
			{
				return new LocalizedString("ADAttributeOffice", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000AA1C File Offset: 0x00008C1C
		public static LocalizedString CopyStatusResynchronizing
		{
			get
			{
				return new LocalizedString("CopyStatusResynchronizing", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000AA33 File Offset: 0x00008C33
		public static LocalizedString LinkedMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("LinkedMailboxRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000AA4A File Offset: 0x00008C4A
		public static LocalizedString UniversalSecurityGroupRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("UniversalSecurityGroupRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000AA61 File Offset: 0x00008C61
		public static LocalizedString ADAttributeCustomAttribute12
		{
			get
			{
				return new LocalizedString("ADAttributeCustomAttribute12", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000AA78 File Offset: 0x00008C78
		public static LocalizedString GroupNamingPolicyExtensionCustomAttribute2
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyExtensionCustomAttribute2", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000AA8F File Offset: 0x00008C8F
		public static LocalizedString RemoteRoomMailboxTypeDetails
		{
			get
			{
				return new LocalizedString("RemoteRoomMailboxTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000AAA6 File Offset: 0x00008CA6
		public static LocalizedString Title
		{
			get
			{
				return new LocalizedString("Title", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000AABD File Offset: 0x00008CBD
		public static LocalizedString MailboxUserRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailboxUserRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		public static LocalizedString ServerRoleExtendedRole4
		{
			get
			{
				return new LocalizedString("ServerRoleExtendedRole4", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000AAEB File Offset: 0x00008CEB
		public static LocalizedString ServerRoleProvisionedServer
		{
			get
			{
				return new LocalizedString("ServerRoleProvisionedServer", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000AB02 File Offset: 0x00008D02
		public static LocalizedString SpamFilteringActionRedirect
		{
			get
			{
				return new LocalizedString("SpamFilteringActionRedirect", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000AB19 File Offset: 0x00008D19
		public static LocalizedString GroupNamingPolicyCustomAttribute12
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute12", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000AB30 File Offset: 0x00008D30
		public static LocalizedString MailEnabledUniversalDistributionGroupRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailEnabledUniversalDistributionGroupRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000AB47 File Offset: 0x00008D47
		public static LocalizedString NotifyOnlyActionType
		{
			get
			{
				return new LocalizedString("NotifyOnlyActionType", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000AB5E File Offset: 0x00008D5E
		public static LocalizedString RejectMessageActionType
		{
			get
			{
				return new LocalizedString("RejectMessageActionType", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000AB75 File Offset: 0x00008D75
		public static LocalizedString CopyStatusMisconfigured
		{
			get
			{
				return new LocalizedString("CopyStatusMisconfigured", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000AB8C File Offset: 0x00008D8C
		public static LocalizedString ADAttributeHomePhoneNumber
		{
			get
			{
				return new LocalizedString("ADAttributeHomePhoneNumber", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000ABA3 File Offset: 0x00008DA3
		public static LocalizedString Contacts
		{
			get
			{
				return new LocalizedString("Contacts", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000ABBA File Offset: 0x00008DBA
		public static LocalizedString LegacyArchiveJournals
		{
			get
			{
				return new LocalizedString("LegacyArchiveJournals", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000ABD1 File Offset: 0x00008DD1
		public static LocalizedString GroupNamingPolicyDepartment
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyDepartment", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		public static LocalizedString ServerRoleManagementBackEnd
		{
			get
			{
				return new LocalizedString("ServerRoleManagementBackEnd", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000ABFF File Offset: 0x00008DFF
		public static LocalizedString Digest
		{
			get
			{
				return new LocalizedString("Digest", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000AC16 File Offset: 0x00008E16
		public static LocalizedString AsyncOperationTypeMigration
		{
			get
			{
				return new LocalizedString("AsyncOperationTypeMigration", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000AC2D File Offset: 0x00008E2D
		public static LocalizedString CopyStatusFailed
		{
			get
			{
				return new LocalizedString("CopyStatusFailed", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000AC44 File Offset: 0x00008E44
		public static LocalizedString Notes
		{
			get
			{
				return new LocalizedString("Notes", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000AC5B File Offset: 0x00008E5B
		public static LocalizedString ServerRoleExtendedRole2
		{
			get
			{
				return new LocalizedString("ServerRoleExtendedRole2", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000AC72 File Offset: 0x00008E72
		public static LocalizedString NegoEx
		{
			get
			{
				return new LocalizedString("NegoEx", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000AC89 File Offset: 0x00008E89
		public static LocalizedString MessageTypeEncrypted
		{
			get
			{
				return new LocalizedString("MessageTypeEncrypted", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		public static LocalizedString GroupNamingPolicyCustomAttribute10
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute10", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000ACB7 File Offset: 0x00008EB7
		public static LocalizedString MailEnabledDynamicDistributionGroupRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailEnabledDynamicDistributionGroupRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000ACCE File Offset: 0x00008ECE
		public static LocalizedString ContentIndexStatusSuspended
		{
			get
			{
				return new LocalizedString("ContentIndexStatusSuspended", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000ACE5 File Offset: 0x00008EE5
		public static LocalizedString Fba
		{
			get
			{
				return new LocalizedString("Fba", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000ACFC File Offset: 0x00008EFC
		public static LocalizedString CopyStatusDisconnectedAndHealthy
		{
			get
			{
				return new LocalizedString("CopyStatusDisconnectedAndHealthy", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000AD13 File Offset: 0x00008F13
		public static LocalizedString MailEnabledForestContactRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailEnabledForestContactRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000AD2A File Offset: 0x00008F2A
		public static LocalizedString JunkEmail
		{
			get
			{
				return new LocalizedString("JunkEmail", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000AD41 File Offset: 0x00008F41
		public static LocalizedString ServerRoleMonitoring
		{
			get
			{
				return new LocalizedString("ServerRoleMonitoring", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000AD58 File Offset: 0x00008F58
		public static LocalizedString SystemMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("SystemMailboxRecipientTypeDetails", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000AD6F File Offset: 0x00008F6F
		public static LocalizedString GroupNamingPolicyCountryCode
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCountryCode", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000AD86 File Offset: 0x00008F86
		public static LocalizedString CopyStatusSeeding
		{
			get
			{
				return new LocalizedString("CopyStatusSeeding", EnumStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000AD9D File Offset: 0x00008F9D
		public static LocalizedString GetLocalizedString(EnumStrings.IDs key)
		{
			return new LocalizedString(EnumStrings.stringIDs[(uint)key], EnumStrings.ResourceManager, new object[0]);
		}

		// Token: 0x0400026B RID: 619
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(324);

		// Token: 0x0400026C RID: 620
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.ControlPanel.EnumStrings", typeof(EnumStrings).GetTypeInfo().Assembly);

		// Token: 0x02000006 RID: 6
		public enum IDs : uint
		{
			// Token: 0x0400026E RID: 622
			CopyStatusDisconnected = 2831161206U,
			// Token: 0x0400026F RID: 623
			Inbox = 2979702410U,
			// Token: 0x04000270 RID: 624
			G711 = 3692015522U,
			// Token: 0x04000271 RID: 625
			ServerRoleExtendedRole3 = 3707194053U,
			// Token: 0x04000272 RID: 626
			ServerRoleMailbox = 2125756541U,
			// Token: 0x04000273 RID: 627
			ServerRoleUnifiedMessaging = 3194934827U,
			// Token: 0x04000274 RID: 628
			CopyStatusMounting = 1208301054U,
			// Token: 0x04000275 RID: 629
			GroupNamingPolicyOffice = 62599113U,
			// Token: 0x04000276 RID: 630
			WellKnownRecipientTypeMailGroups = 3725493575U,
			// Token: 0x04000277 RID: 631
			EnterpriseTrialEdition = 4134731995U,
			// Token: 0x04000278 RID: 632
			ADAttributeCustomAttribute1 = 1377545167U,
			// Token: 0x04000279 RID: 633
			ServerRoleEdge = 756854696U,
			// Token: 0x0400027A RID: 634
			CoexistenceTrialEdition = 2191591450U,
			// Token: 0x0400027B RID: 635
			ServerRoleHubTransport = 172810921U,
			// Token: 0x0400027C RID: 636
			ADAttributeLastName = 1016721882U,
			// Token: 0x0400027D RID: 637
			TeamMailboxRecipientTypeDetails = 107906018U,
			// Token: 0x0400027E RID: 638
			ContentIndexStatusDisabled = 2385442291U,
			// Token: 0x0400027F RID: 639
			CcRecipientType = 2476473719U,
			// Token: 0x04000280 RID: 640
			FallbackIgnore = 2328530086U,
			// Token: 0x04000281 RID: 641
			MailEnabledUniversalSecurityGroupRecipientTypeDetails = 1970247521U,
			// Token: 0x04000282 RID: 642
			CopyStatusMounted = 623586765U,
			// Token: 0x04000283 RID: 643
			Gsm = 3115737533U,
			// Token: 0x04000284 RID: 644
			GroupNamingPolicyCustomAttribute6 = 1924734198U,
			// Token: 0x04000285 RID: 645
			MessageTypeOof = 2129610537U,
			// Token: 0x04000286 RID: 646
			GroupNamingPolicyCompany = 862838650U,
			// Token: 0x04000287 RID: 647
			SpamFilteringOptionOn = 2654331961U,
			// Token: 0x04000288 RID: 648
			None = 1414246128U,
			// Token: 0x04000289 RID: 649
			ConversationHistory = 2630084427U,
			// Token: 0x0400028A RID: 650
			NoNewCalls = 1479682494U,
			// Token: 0x0400028B RID: 651
			Adfs = 1716515484U,
			// Token: 0x0400028C RID: 652
			Certificate = 1277957481U,
			// Token: 0x0400028D RID: 653
			ConnectorTypePartner = 2003039265U,
			// Token: 0x0400028E RID: 654
			ADAttributeInitials = 2468414724U,
			// Token: 0x0400028F RID: 655
			AsyncOperationTypeUnknown = 135933047U,
			// Token: 0x04000290 RID: 656
			TelExtn = 1059422816U,
			// Token: 0x04000291 RID: 657
			Authoritative = 2913015079U,
			// Token: 0x04000292 RID: 658
			MicrosoftExchangeRecipientTypeDetails = 2227674028U,
			// Token: 0x04000293 RID: 659
			LiveIdNegotiate = 1091035505U,
			// Token: 0x04000294 RID: 660
			SpecificUsers = 1677492010U,
			// Token: 0x04000295 RID: 661
			ADAttributeCustomAttribute8 = 1377545174U,
			// Token: 0x04000296 RID: 662
			WSSecurity = 1690995826U,
			// Token: 0x04000297 RID: 663
			MessageTypeAutoForward = 2041595767U,
			// Token: 0x04000298 RID: 664
			Misconfigured = 3211494971U,
			// Token: 0x04000299 RID: 665
			DatabaseMasterTypeDag = 2773964607U,
			// Token: 0x0400029A RID: 666
			MessageTypeReadReceipt = 680782013U,
			// Token: 0x0400029B RID: 667
			MoveToDeletedItems = 3341243277U,
			// Token: 0x0400029C RID: 668
			RedirectRecipientType = 2611996929U,
			// Token: 0x0400029D RID: 669
			RemoteUserMailboxTypeDetails = 4145265495U,
			// Token: 0x0400029E RID: 670
			ContentIndexStatusAutoSuspended = 393808047U,
			// Token: 0x0400029F RID: 671
			ExternalManagedMailContactTypeDetails = 3799817423U,
			// Token: 0x040002A0 RID: 672
			Negotiate = 635036566U,
			// Token: 0x040002A1 RID: 673
			ServerRoleFrontendTransport = 3786203794U,
			// Token: 0x040002A2 RID: 674
			CopyStatusDisconnectedAndResynchronizing = 3655046083U,
			// Token: 0x040002A3 RID: 675
			Allowed = 3811183882U,
			// Token: 0x040002A4 RID: 676
			GroupNamingPolicyExtensionCustomAttribute3 = 3197896354U,
			// Token: 0x040002A5 RID: 677
			ADAttributeName = 3050431750U,
			// Token: 0x040002A6 RID: 678
			Disabled = 1484405454U,
			// Token: 0x040002A7 RID: 679
			ConnectorTypeOnPremises = 4060482376U,
			// Token: 0x040002A8 RID: 680
			MessageTypeApprovalRequest = 4072748617U,
			// Token: 0x040002A9 RID: 681
			RejectUnlessExplicitOverrideActionType = 3115100581U,
			// Token: 0x040002AA RID: 682
			WellKnownRecipientTypeMailboxUsers = 2167560764U,
			// Token: 0x040002AB RID: 683
			FallbackReject = 3144351139U,
			// Token: 0x040002AC RID: 684
			RemoteSharedMailboxTypeDetails = 444885611U,
			// Token: 0x040002AD RID: 685
			Secured = 1241597555U,
			// Token: 0x040002AE RID: 686
			MoveToFolder = 1182470434U,
			// Token: 0x040002AF RID: 687
			ADAttributeCustomAttribute5 = 1377545163U,
			// Token: 0x040002B0 RID: 688
			ToRecipientType = 4199979286U,
			// Token: 0x040002B1 RID: 689
			RoleGroupTypeDetails = 3221974997U,
			// Token: 0x040002B2 RID: 690
			Tasks = 2966158940U,
			// Token: 0x040002B3 RID: 691
			DatabaseMasterTypeServer = 2173147846U,
			// Token: 0x040002B4 RID: 692
			UserRecipientTypeDetails = 2338964630U,
			// Token: 0x040002B5 RID: 693
			CopyStatusNonExchangeReplication = 3990056197U,
			// Token: 0x040002B6 RID: 694
			CopyStatusNotConfigured = 1310067130U,
			// Token: 0x040002B7 RID: 695
			DeviceDiscovery = 1010456570U,
			// Token: 0x040002B8 RID: 696
			ContactRecipientTypeDetails = 137387861U,
			// Token: 0x040002B9 RID: 697
			ContentIndexStatusSeeding = 1361373610U,
			// Token: 0x040002BA RID: 698
			GroupNamingPolicyCustomAttribute11 = 2423361114U,
			// Token: 0x040002BB RID: 699
			Ntlm = 3168546739U,
			// Token: 0x040002BC RID: 700
			SentItems = 590977256U,
			// Token: 0x040002BD RID: 701
			ADAttributeCustomAttribute3 = 1377545169U,
			// Token: 0x040002BE RID: 702
			ADAttributePagerNumber = 3850073087U,
			// Token: 0x040002BF RID: 703
			ADAttributeStreet = 2002903510U,
			// Token: 0x040002C0 RID: 704
			Wma = 2665399355U,
			// Token: 0x040002C1 RID: 705
			GroupNamingPolicyCity = 2703120928U,
			// Token: 0x040002C2 RID: 706
			NonIpmRoot = 600983985U,
			// Token: 0x040002C3 RID: 707
			AsyncOperationTypeExportPST = 1937417240U,
			// Token: 0x040002C4 RID: 708
			UnknownEdition = 2889762178U,
			// Token: 0x040002C5 RID: 709
			ModeEnforce = 41715449U,
			// Token: 0x040002C6 RID: 710
			EvaluationNotEqual = 3918497079U,
			// Token: 0x040002C7 RID: 711
			WellKnownRecipientTypeAllRecipients = 2099880135U,
			// Token: 0x040002C8 RID: 712
			ADAttributeCustomAttribute14 = 1048761747U,
			// Token: 0x040002C9 RID: 713
			DatabaseMasterTypeUnknown = 661425765U,
			// Token: 0x040002CA RID: 714
			ADAttributePhoneNumber = 4137481806U,
			// Token: 0x040002CB RID: 715
			GroupNamingPolicyCustomAttribute4 = 1924734196U,
			// Token: 0x040002CC RID: 716
			StandardTrialEdition = 553174585U,
			// Token: 0x040002CD RID: 717
			PersonalFolder = 2283186478U,
			// Token: 0x040002CE RID: 718
			LiveIdBasic = 754287197U,
			// Token: 0x040002CF RID: 719
			WellKnownRecipientTypeMailUsers = 933193541U,
			// Token: 0x040002D0 RID: 720
			SystemAttendantMailboxRecipientTypeDetails = 1818643265U,
			// Token: 0x040002D1 RID: 721
			CopyStatusInitializing = 1373187244U,
			// Token: 0x040002D2 RID: 722
			ServerRoleClientAccess = 1052758952U,
			// Token: 0x040002D3 RID: 723
			MessageTypeCalendaring = 1903193717U,
			// Token: 0x040002D4 RID: 724
			SyncIssues = 3694564633U,
			// Token: 0x040002D5 RID: 725
			AlwaysEnabled = 798637440U,
			// Token: 0x040002D6 RID: 726
			ContentIndexStatusUnknown = 1631091055U,
			// Token: 0x040002D7 RID: 727
			SharedMailboxRecipientTypeDetails = 4263249978U,
			// Token: 0x040002D8 RID: 728
			InternalRelay = 3288506612U,
			// Token: 0x040002D9 RID: 729
			CoexistenceEdition = 1474747046U,
			// Token: 0x040002DA RID: 730
			GroupNamingPolicyCustomAttribute7 = 1924734197U,
			// Token: 0x040002DB RID: 731
			Outbox = 629464291U,
			// Token: 0x040002DC RID: 732
			ADAttributeCustomAttribute15 = 2614845688U,
			// Token: 0x040002DD RID: 733
			WellKnownRecipientTypeNone = 1849540794U,
			// Token: 0x040002DE RID: 734
			ManagementRelationshipManager = 25634710U,
			// Token: 0x040002DF RID: 735
			ServerRoleCafeArray = 986970413U,
			// Token: 0x040002E0 RID: 736
			ExternalManagedGroupTypeDetails = 1097129869U,
			// Token: 0x040002E1 RID: 737
			ArchiveStateNone = 3086386447U,
			// Token: 0x040002E2 RID: 738
			EvaluatedUserSender = 2509095413U,
			// Token: 0x040002E3 RID: 739
			IncidentReportIncludeOriginalMail = 1389339898U,
			// Token: 0x040002E4 RID: 740
			GroupNamingPolicyExtensionCustomAttribute1 = 65728472U,
			// Token: 0x040002E5 RID: 741
			ADAttributeEmail = 4289093673U,
			// Token: 0x040002E6 RID: 742
			E164 = 438888054U,
			// Token: 0x040002E7 RID: 743
			All = 4231482709U,
			// Token: 0x040002E8 RID: 744
			ManagementRelationshipDirectReport = 428619956U,
			// Token: 0x040002E9 RID: 745
			InheritFromDialPlan = 637440764U,
			// Token: 0x040002EA RID: 746
			EvaluationEqual = 3459736224U,
			// Token: 0x040002EB RID: 747
			MailboxPlanTypeDetails = 1094750789U,
			// Token: 0x040002EC RID: 748
			FallbackWrap = 3262572344U,
			// Token: 0x040002ED RID: 749
			ADAttributeCustomAttribute4 = 1377545162U,
			// Token: 0x040002EE RID: 750
			ADAttributeDepartment = 3367615085U,
			// Token: 0x040002EF RID: 751
			SpamFilteringOptionTest = 2944126402U,
			// Token: 0x040002F0 RID: 752
			Private = 3026477473U,
			// Token: 0x040002F1 RID: 753
			ADAttributeCity = 4226527350U,
			// Token: 0x040002F2 RID: 754
			DiscoveryMailboxTypeDetails = 104454802U,
			// Token: 0x040002F3 RID: 755
			ADAttributePOBox = 4260106383U,
			// Token: 0x040002F4 RID: 756
			ServerRoleExtendedRole7 = 3707194057U,
			// Token: 0x040002F5 RID: 757
			Everyone = 3708929833U,
			// Token: 0x040002F6 RID: 758
			LegacyMailboxRecipientTypeDetails = 221683052U,
			// Token: 0x040002F7 RID: 759
			ADAttributeFaxNumber = 2182511137U,
			// Token: 0x040002F8 RID: 760
			IncidentReportDoNotIncludeOriginalMail = 29398792U,
			// Token: 0x040002F9 RID: 761
			ExternalUser = 3631693406U,
			// Token: 0x040002FA RID: 762
			RemoteEquipmentMailboxTypeDetails = 1406382714U,
			// Token: 0x040002FB RID: 763
			Tag = 696030922U,
			// Token: 0x040002FC RID: 764
			GroupTypeFlagsBuiltinLocal = 1494101274U,
			// Token: 0x040002FD RID: 765
			ServerRoleManagementFrontEnd = 3802186670U,
			// Token: 0x040002FE RID: 766
			GroupNamingPolicyStateOrProvince = 4088287609U,
			// Token: 0x040002FF RID: 767
			ArchiveStateHostedPending = 920444171U,
			// Token: 0x04000300 RID: 768
			RemoteTeamMailboxRecipientTypeDetails = 322963092U,
			// Token: 0x04000301 RID: 769
			ADAttributeZipCode = 381216251U,
			// Token: 0x04000302 RID: 770
			PermanentlyDelete = 3675904764U,
			// Token: 0x04000303 RID: 771
			Location = 2325276717U,
			// Token: 0x04000304 RID: 772
			EquipmentMailboxRecipientTypeDetails = 3938481035U,
			// Token: 0x04000305 RID: 773
			CopyStatusDismounted = 3673730471U,
			// Token: 0x04000306 RID: 774
			SipName = 3423767853U,
			// Token: 0x04000307 RID: 775
			ModeAudit = 3869829980U,
			// Token: 0x04000308 RID: 776
			DumpsterFolder = 3641768400U,
			// Token: 0x04000309 RID: 777
			Organizational = 1067650092U,
			// Token: 0x0400030A RID: 778
			ADAttributeFirstName = 2986926906U,
			// Token: 0x0400030B RID: 779
			ServerRoleSCOM = 407788899U,
			// Token: 0x0400030C RID: 780
			DeletedItems = 3613623199U,
			// Token: 0x0400030D RID: 781
			GroupNamingPolicyCustomAttribute2 = 1924734194U,
			// Token: 0x0400030E RID: 782
			ADAttributeCustomAttribute2 = 1377545168U,
			// Token: 0x0400030F RID: 783
			GroupTypeFlagsDomainLocal = 1638178773U,
			// Token: 0x04000310 RID: 784
			ServerRoleCentralAdminFrontEnd = 3980237751U,
			// Token: 0x04000311 RID: 785
			InternalUser = 2795331228U,
			// Token: 0x04000312 RID: 786
			ContentIndexStatusFailedAndSuspended = 1923042104U,
			// Token: 0x04000313 RID: 787
			ADAttributeCountry = 3600528589U,
			// Token: 0x04000314 RID: 788
			SpamFilteringOptionOff = 2030161115U,
			// Token: 0x04000315 RID: 789
			GroupNamingPolicyTitle = 4137211921U,
			// Token: 0x04000316 RID: 790
			BccRecipientType = 1798370525U,
			// Token: 0x04000317 RID: 791
			AsyncOperationTypeImportPST = 4181674605U,
			// Token: 0x04000318 RID: 792
			RejectUnlessFalsePositiveOverrideActionType = 2736707353U,
			// Token: 0x04000319 RID: 793
			SpamFilteringActionDelete = 3918345138U,
			// Token: 0x0400031A RID: 794
			PublicFolderRecipientTypeDetails = 1625030180U,
			// Token: 0x0400031B RID: 795
			SpamFilteringActionAddXHeader = 685401583U,
			// Token: 0x0400031C RID: 796
			SpamFilteringActionModifySubject = 2349327181U,
			// Token: 0x0400031D RID: 797
			ContentIndexStatusHealthyAndUpgrading = 3268869348U,
			// Token: 0x0400031E RID: 798
			Basic = 4128944152U,
			// Token: 0x0400031F RID: 799
			Department = 1855823700U,
			// Token: 0x04000320 RID: 800
			MessageTypeSigned = 3606274629U,
			// Token: 0x04000321 RID: 801
			WellKnownRecipientTypeMailContacts = 2638599330U,
			// Token: 0x04000322 RID: 802
			ADAttributeMobileNumber = 2411750738U,
			// Token: 0x04000323 RID: 803
			MessageTypeVoicemail = 117825870U,
			// Token: 0x04000324 RID: 804
			MailEnabledUserRecipientTypeDetails = 3689869554U,
			// Token: 0x04000325 RID: 805
			Mp3 = 1549653732U,
			// Token: 0x04000326 RID: 806
			RejectUnlessSilentOverrideActionType = 2447598924U,
			// Token: 0x04000327 RID: 807
			GroupNamingPolicyCountryOrRegion = 3674978674U,
			// Token: 0x04000328 RID: 808
			ServerRoleLanguagePacks = 2698858797U,
			// Token: 0x04000329 RID: 809
			CopyStatusSinglePageRestore = 1359519288U,
			// Token: 0x0400032A RID: 810
			MailEnabledNonUniversalGroupRecipientTypeDetails = 3376217818U,
			// Token: 0x0400032B RID: 811
			SpamFilteringActionJmf = 1123996746U,
			// Token: 0x0400032C RID: 812
			Drafts = 115734878U,
			// Token: 0x0400032D RID: 813
			ADAttributeCustomAttribute10 = 3374360575U,
			// Token: 0x0400032E RID: 814
			ArchiveStateOnPremise = 110833865U,
			// Token: 0x0400032F RID: 815
			UniversalDistributionGroupRecipientTypeDetails = 1966081841U,
			// Token: 0x04000330 RID: 816
			SpamFilteringActionQuarantine = 2852597951U,
			// Token: 0x04000331 RID: 817
			ADAttributeCustomAttribute6 = 1377545164U,
			// Token: 0x04000332 RID: 818
			GroupTypeFlagsNone = 252422050U,
			// Token: 0x04000333 RID: 819
			ContentIndexStatusFailed = 2562345274U,
			// Token: 0x04000334 RID: 820
			ServerRoleOSP = 2775202161U,
			// Token: 0x04000335 RID: 821
			ADAttributeOtherFaxNumber = 3689464497U,
			// Token: 0x04000336 RID: 822
			GroupNamingPolicyCustomAttribute15 = 97762286U,
			// Token: 0x04000337 RID: 823
			Enabled = 634395589U,
			// Token: 0x04000338 RID: 824
			ADAttributeCustomAttribute7 = 1377545165U,
			// Token: 0x04000339 RID: 825
			GroupNamingPolicyCustomAttribute1 = 1924734191U,
			// Token: 0x0400033A RID: 826
			GroupTypeFlagsUniversal = 1191186633U,
			// Token: 0x0400033B RID: 827
			ADAttributeCustomAttribute11 = 645477220U,
			// Token: 0x0400033C RID: 828
			GroupNamingPolicyExtensionCustomAttribute5 = 2391327300U,
			// Token: 0x0400033D RID: 829
			ServerRoleExtendedRole5 = 3707194059U,
			// Token: 0x0400033E RID: 830
			OAuth = 3309342631U,
			// Token: 0x0400033F RID: 831
			ADAttributeOtherHomePhoneNumber = 856583401U,
			// Token: 0x04000340 RID: 832
			ArchiveStateLocal = 665936024U,
			// Token: 0x04000341 RID: 833
			GroupNamingPolicyCustomAttribute13 = 3586160528U,
			// Token: 0x04000342 RID: 834
			ComputerRecipientTypeDetails = 3489169852U,
			// Token: 0x04000343 RID: 835
			LiveIdFba = 1582423804U,
			// Token: 0x04000344 RID: 836
			ADAttributeManager = 494686544U,
			// Token: 0x04000345 RID: 837
			ADAttributeOtherPhoneNumber = 3162495226U,
			// Token: 0x04000346 RID: 838
			ServerRoleFfoWebServices = 3464146580U,
			// Token: 0x04000347 RID: 839
			ContentIndexStatusCrawling = 1575862374U,
			// Token: 0x04000348 RID: 840
			MoveToArchive = 2835967712U,
			// Token: 0x04000349 RID: 841
			MonitoringMailboxRecipientTypeDetails = 729925097U,
			// Token: 0x0400034A RID: 842
			ServerRoleAll = 570563164U,
			// Token: 0x0400034B RID: 843
			GroupNamingPolicyExtensionCustomAttribute4 = 825243359U,
			// Token: 0x0400034C RID: 844
			WellKnownRecipientTypeResources = 3773054995U,
			// Token: 0x0400034D RID: 845
			GroupNamingPolicyCustomAttribute5 = 1924734195U,
			// Token: 0x0400034E RID: 846
			WindowsIntegrated = 872998734U,
			// Token: 0x0400034F RID: 847
			SMTPAddress = 980672066U,
			// Token: 0x04000350 RID: 848
			ADAttributeUserLogonName = 1452889642U,
			// Token: 0x04000351 RID: 849
			ADAttributeNotes = 863112602U,
			// Token: 0x04000352 RID: 850
			LinkedUserTypeDetails = 1738880682U,
			// Token: 0x04000353 RID: 851
			PromptForAlias = 2303788021U,
			// Token: 0x04000354 RID: 852
			NonUniversalGroupRecipientTypeDetails = 2227190334U,
			// Token: 0x04000355 RID: 853
			ADAttributeTitle = 2634964433U,
			// Token: 0x04000356 RID: 854
			SIPSecured = 2422734853U,
			// Token: 0x04000357 RID: 855
			CopyStatusDismounting = 4189810048U,
			// Token: 0x04000358 RID: 856
			CopyStatusServiceDown = 729299916U,
			// Token: 0x04000359 RID: 857
			PublicFolderMailboxRecipientTypeDetails = 1487832074U,
			// Token: 0x0400035A RID: 858
			Quarantined = 996355914U,
			// Token: 0x0400035B RID: 859
			GroupTypeFlagsSecurityEnabled = 3200416695U,
			// Token: 0x0400035C RID: 860
			ServerRoleNone = 2094315795U,
			// Token: 0x0400035D RID: 861
			EnterpriseEdition = 26915469U,
			// Token: 0x0400035E RID: 862
			AsyncOperationTypeCertExpiry = 2045069482U,
			// Token: 0x0400035F RID: 863
			ExternalPartner = 715964235U,
			// Token: 0x04000360 RID: 864
			GroupNamingPolicyCustomAttribute3 = 1924734193U,
			// Token: 0x04000361 RID: 865
			CopyStatusFailedAndSuspended = 1765158362U,
			// Token: 0x04000362 RID: 866
			AllUsers = 3949283739U,
			// Token: 0x04000363 RID: 867
			CopyStatusSuspended = 2605454650U,
			// Token: 0x04000364 RID: 868
			Journal = 4137480277U,
			// Token: 0x04000365 RID: 869
			StandardEdition = 2321790947U,
			// Token: 0x04000366 RID: 870
			UndefinedRecipientTypeDetails = 3453679227U,
			// Token: 0x04000367 RID: 871
			CopyStatusSeedingSource = 2160282563U,
			// Token: 0x04000368 RID: 872
			ModeAuditAndNotify = 230388220U,
			// Token: 0x04000369 RID: 873
			ADAttributeCompany = 2891753468U,
			// Token: 0x0400036A RID: 874
			EvaluatedUserRecipient = 2030715989U,
			// Token: 0x0400036B RID: 875
			Blocked = 4019774802U,
			// Token: 0x0400036C RID: 876
			ExternalNonPartner = 2155604814U,
			// Token: 0x0400036D RID: 877
			MailEnabledContactRecipientTypeDetails = 3815678973U,
			// Token: 0x0400036E RID: 878
			Unsecured = 1573777228U,
			// Token: 0x0400036F RID: 879
			ArchiveStateHostedProvisioned = 2472951404U,
			// Token: 0x04000370 RID: 880
			GroupNamingPolicyCustomAttribute9 = 1924734199U,
			// Token: 0x04000371 RID: 881
			Calendar = 1292798904U,
			// Token: 0x04000372 RID: 882
			ArbitrationMailboxTypeDetails = 3647297993U,
			// Token: 0x04000373 RID: 883
			DisabledUserRecipientTypeDetails = 3569405894U,
			// Token: 0x04000374 RID: 884
			CopyStatusUnknown = 1960737953U,
			// Token: 0x04000375 RID: 885
			LastFirst = 142823596U,
			// Token: 0x04000376 RID: 886
			MessageTypePermissionControlled = 2872629304U,
			// Token: 0x04000377 RID: 887
			RssSubscriptions = 3598244064U,
			// Token: 0x04000378 RID: 888
			ContentIndexStatusHealthy = 4010596708U,
			// Token: 0x04000379 RID: 889
			ADAttributeCustomAttribute13 = 1808276634U,
			// Token: 0x0400037A RID: 890
			Kerberos = 645017541U,
			// Token: 0x0400037B RID: 891
			CopyStatusHealthy = 1484668346U,
			// Token: 0x0400037C RID: 892
			RoomListGroupTypeDetails = 1391517930U,
			// Token: 0x0400037D RID: 893
			ADAttributeCustomAttribute9 = 1377545175U,
			// Token: 0x0400037E RID: 894
			ServerRoleCafe = 1536572748U,
			// Token: 0x0400037F RID: 895
			GroupNamingPolicyCustomAttribute8 = 1924734200U,
			// Token: 0x04000380 RID: 896
			SoftDelete = 3133553171U,
			// Token: 0x04000381 RID: 897
			ExternalRelay = 2171581398U,
			// Token: 0x04000382 RID: 898
			FirstLast = 2300412432U,
			// Token: 0x04000383 RID: 899
			GroupNamingPolicyCustomAttribute14 = 1663846227U,
			// Token: 0x04000384 RID: 900
			GroupTypeFlagsGlobal = 4189167987U,
			// Token: 0x04000385 RID: 901
			ConferenceRoomMailboxRecipientTypeDetails = 1919306754U,
			// Token: 0x04000386 RID: 902
			AsyncOperationTypeMailboxRestore = 1588035907U,
			// Token: 0x04000387 RID: 903
			Unknown = 2846264340U,
			// Token: 0x04000388 RID: 904
			ADAttributeState = 3882899654U,
			// Token: 0x04000389 RID: 905
			GroupMailboxRecipientTypeDetails = 2223810040U,
			// Token: 0x0400038A RID: 906
			ADAttributeOffice = 1927573801U,
			// Token: 0x0400038B RID: 907
			CopyStatusResynchronizing = 1545501201U,
			// Token: 0x0400038C RID: 908
			LinkedMailboxRecipientTypeDetails = 1432667858U,
			// Token: 0x0400038D RID: 909
			UniversalSecurityGroupRecipientTypeDetails = 968858937U,
			// Token: 0x0400038E RID: 910
			ADAttributeCustomAttribute12 = 242192693U,
			// Token: 0x0400038F RID: 911
			GroupNamingPolicyExtensionCustomAttribute2 = 1631812413U,
			// Token: 0x04000390 RID: 912
			RemoteRoomMailboxTypeDetails = 1594549261U,
			// Token: 0x04000391 RID: 913
			Title = 2435266816U,
			// Token: 0x04000392 RID: 914
			MailboxUserRecipientTypeDetails = 1605633982U,
			// Token: 0x04000393 RID: 915
			ServerRoleExtendedRole4 = 3707194060U,
			// Token: 0x04000394 RID: 916
			ServerRoleProvisionedServer = 1922689150U,
			// Token: 0x04000395 RID: 917
			SpamFilteringActionRedirect = 4255105347U,
			// Token: 0x04000396 RID: 918
			GroupNamingPolicyCustomAttribute12 = 857277173U,
			// Token: 0x04000397 RID: 919
			MailEnabledUniversalDistributionGroupRecipientTypeDetails = 562488721U,
			// Token: 0x04000398 RID: 920
			NotifyOnlyActionType = 604363629U,
			// Token: 0x04000399 RID: 921
			RejectMessageActionType = 498572210U,
			// Token: 0x0400039A RID: 922
			CopyStatusMisconfigured = 1587179080U,
			// Token: 0x0400039B RID: 923
			ADAttributeHomePhoneNumber = 1457839961U,
			// Token: 0x0400039C RID: 924
			Contacts = 1716044995U,
			// Token: 0x0400039D RID: 925
			LegacyArchiveJournals = 3635271833U,
			// Token: 0x0400039E RID: 926
			GroupNamingPolicyDepartment = 154085973U,
			// Token: 0x0400039F RID: 927
			ServerRoleManagementBackEnd = 696678862U,
			// Token: 0x040003A0 RID: 928
			Digest = 815864422U,
			// Token: 0x040003A1 RID: 929
			AsyncOperationTypeMigration = 1702371863U,
			// Token: 0x040003A2 RID: 930
			CopyStatusFailed = 4015121608U,
			// Token: 0x040003A3 RID: 931
			Notes = 1601836855U,
			// Token: 0x040003A4 RID: 932
			ServerRoleExtendedRole2 = 3707194054U,
			// Token: 0x040003A5 RID: 933
			NegoEx = 273163868U,
			// Token: 0x040003A6 RID: 934
			MessageTypeEncrypted = 3544120613U,
			// Token: 0x040003A7 RID: 935
			GroupNamingPolicyCustomAttribute10 = 3989445055U,
			// Token: 0x040003A8 RID: 936
			MailEnabledDynamicDistributionGroupRecipientTypeDetails = 2999125469U,
			// Token: 0x040003A9 RID: 937
			ContentIndexStatusSuspended = 1056819816U,
			// Token: 0x040003AA RID: 938
			Fba = 1099314853U,
			// Token: 0x040003AB RID: 939
			CopyStatusDisconnectedAndHealthy = 3985647980U,
			// Token: 0x040003AC RID: 940
			MailEnabledForestContactRecipientTypeDetails = 3586618070U,
			// Token: 0x040003AD RID: 941
			JunkEmail = 2241039844U,
			// Token: 0x040003AE RID: 942
			ServerRoleMonitoring = 1024471425U,
			// Token: 0x040003AF RID: 943
			SystemMailboxRecipientTypeDetails = 1850977098U,
			// Token: 0x040003B0 RID: 944
			GroupNamingPolicyCountryCode = 4022404286U,
			// Token: 0x040003B1 RID: 945
			CopyStatusSeeding = 448862132U
		}

		// Token: 0x02000007 RID: 7
		private enum ParamIDs
		{
			// Token: 0x040003B3 RID: 947
			UnsupportServerEdition
		}
	}
}
