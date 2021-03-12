using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A53 RID: 2643
	internal static class DirectoryStrings
	{
		// Token: 0x060078B1 RID: 30897 RVA: 0x00190718 File Offset: 0x0018E918
		static DirectoryStrings()
		{
			DirectoryStrings.stringIDs.Add(2223810040U, "GroupMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(3059540560U, "InvalidTransportSyncHealthLogSizeConfiguration");
			DirectoryStrings.stringIDs.Add(3553131525U, "ReceiveExtendedProtectionPolicyNone");
			DirectoryStrings.stringIDs.Add(1352261648U, "OrganizationCapabilityManagement");
			DirectoryStrings.stringIDs.Add(3384994469U, "EsnLangTamil");
			DirectoryStrings.stringIDs.Add(1623026330U, "LdapFilterErrorInvalidWildCard");
			DirectoryStrings.stringIDs.Add(3266435989U, "Individual");
			DirectoryStrings.stringIDs.Add(2171581398U, "ExternalRelay");
			DirectoryStrings.stringIDs.Add(1719230762U, "InvalidTransportSyncDownloadSizeConfiguration");
			DirectoryStrings.stringIDs.Add(2928684304U, "MessageRateSourceFlagsAll");
			DirectoryStrings.stringIDs.Add(4209173728U, "SKUCapabilityBPOSSBasic");
			DirectoryStrings.stringIDs.Add(3376948578U, "IndustryMediaMarketingAdvertising");
			DirectoryStrings.stringIDs.Add(2570737323U, "SKUCapabilityUnmanaged");
			DirectoryStrings.stringIDs.Add(4073959654U, "BackSyncDataSourceTransientErrorMessage");
			DirectoryStrings.stringIDs.Add(3376217818U, "MailEnabledNonUniversalGroupRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(3806493464U, "ADDriverStoreAccessPermanentError");
			DirectoryStrings.stringIDs.Add(3323369056U, "DeviceType");
			DirectoryStrings.stringIDs.Add(3229570631U, "EsnLangFarsi");
			DirectoryStrings.stringIDs.Add(3503019411U, "InvalidTempErrorSetting");
			DirectoryStrings.stringIDs.Add(3874381006U, "ReplicationTypeNone");
			DirectoryStrings.stringIDs.Add(1593550494U, "IndustryBusinessServicesConsulting");
			DirectoryStrings.stringIDs.Add(210447381U, "ErrorAdfsConfigFormat");
			DirectoryStrings.stringIDs.Add(996355914U, "Quarantined");
			DirectoryStrings.stringIDs.Add(3114754472U, "OutboundConnectorSmartHostShouldBePresentIfUseMXRecordFalse");
			DirectoryStrings.stringIDs.Add(364260824U, "LongRunningCostHandle");
			DirectoryStrings.stringIDs.Add(3479185892U, "EsnLangChineseTraditional");
			DirectoryStrings.stringIDs.Add(1803878278U, "IndustryTransportation");
			DirectoryStrings.stringIDs.Add(815885189U, "Silent");
			DirectoryStrings.stringIDs.Add(3145530267U, "AlternateServiceAccountCredentialQualifiedUserNameWrongFormat");
			DirectoryStrings.stringIDs.Add(1840954879U, "InvalidBannerSetting");
			DirectoryStrings.stringIDs.Add(1924734196U, "GroupNamingPolicyCustomAttribute4");
			DirectoryStrings.stringIDs.Add(230574830U, "InboundConnectorIncorrectCloudServicesMailEnabled");
			DirectoryStrings.stringIDs.Add(3936814429U, "LdapFilterErrorAnrIsNotSupported");
			DirectoryStrings.stringIDs.Add(438888054U, "E164");
			DirectoryStrings.stringIDs.Add(1874297349U, "ErrorAuthMetaDataContentEmpty");
			DirectoryStrings.stringIDs.Add(2285243143U, "MailEnabledContactRecipientType");
			DirectoryStrings.stringIDs.Add(562488721U, "MailEnabledUniversalDistributionGroupRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(360676809U, "SendAuthMechanismExternalAuthoritative");
			DirectoryStrings.stringIDs.Add(528770240U, "InboundConnectorRequiredTlsSettingsInvalid");
			DirectoryStrings.stringIDs.Add(1924734191U, "GroupNamingPolicyCustomAttribute1");
			DirectoryStrings.stringIDs.Add(1743625100U, "Dual");
			DirectoryStrings.stringIDs.Add(1822619008U, "DatabaseCopyAutoActivationPolicyIntrasiteOnly");
			DirectoryStrings.stringIDs.Add(1594930120U, "Never");
			DirectoryStrings.stringIDs.Add(1644017996U, "ByteEncoderTypeUndefined");
			DirectoryStrings.stringIDs.Add(3817431401U, "InvalidRcvProtocolLogSizeConfiguration");
			DirectoryStrings.stringIDs.Add(2785880074U, "GetRootDseRequiresDomainController");
			DirectoryStrings.stringIDs.Add(637440764U, "InheritFromDialPlan");
			DirectoryStrings.stringIDs.Add(737697267U, "OrganizationCapabilityMessageTracking");
			DirectoryStrings.stringIDs.Add(1573064009U, "InboundConnectorInvalidTlsSenderCertificateName");
			DirectoryStrings.stringIDs.Add(3133553171U, "SoftDelete");
			DirectoryStrings.stringIDs.Add(2570855206U, "OrganizationCapabilityUMGrammar");
			DirectoryStrings.stringIDs.Add(1776488541U, "Allow");
			DirectoryStrings.stringIDs.Add(1261795780U, "DomainNameIsNull");
			DirectoryStrings.stringIDs.Add(2303788021U, "PromptForAlias");
			DirectoryStrings.stringIDs.Add(3614458512U, "ErrorSystemAddressListInWrongContainer");
			DirectoryStrings.stringIDs.Add(651742924U, "ExceptionUnableToDisableAdminTopologyMode");
			DirectoryStrings.stringIDs.Add(1241597555U, "Secured");
			DirectoryStrings.stringIDs.Add(2462615416U, "ExternalAndAuthSet");
			DirectoryStrings.stringIDs.Add(597372135U, "EsnLangJapanese");
			DirectoryStrings.stringIDs.Add(1391104995U, "EsnLangPortuguesePortugal");
			DirectoryStrings.stringIDs.Add(2487111597U, "EsnLangFinnish");
			DirectoryStrings.stringIDs.Add(2811972280U, "ExceptionOwaCannotSetPropertyOnVirtualDirectoryOtherThanExchweb");
			DirectoryStrings.stringIDs.Add(2180736490U, "WhenDelivered");
			DirectoryStrings.stringIDs.Add(2839188613U, "DomainStatePendingRelease");
			DirectoryStrings.stringIDs.Add(1631812413U, "GroupNamingPolicyExtensionCustomAttribute2");
			DirectoryStrings.stringIDs.Add(2127564032U, "AutoGroup");
			DirectoryStrings.stringIDs.Add(3880444299U, "ErrorStartDateExpiration");
			DirectoryStrings.stringIDs.Add(285960632U, "MailboxMoveStatusQueued");
			DirectoryStrings.stringIDs.Add(2220842206U, "Minute");
			DirectoryStrings.stringIDs.Add(590977256U, "SentItems");
			DirectoryStrings.stringIDs.Add(4029642168U, "ExchangeVoicemailMC");
			DirectoryStrings.stringIDs.Add(3010978409U, "AppliedInFull");
			DirectoryStrings.stringIDs.Add(2215231792U, "NoAddressSpaces");
			DirectoryStrings.stringIDs.Add(1387109368U, "SKUCapabilityEOPStandardAddOn");
			DirectoryStrings.stringIDs.Add(2260925979U, "IndustryNonProfit");
			DirectoryStrings.stringIDs.Add(3413117907U, "EsnLangDefault");
			DirectoryStrings.stringIDs.Add(707064308U, "SpecifyCustomGreetingFileName");
			DirectoryStrings.stringIDs.Add(671952847U, "EsnLangSlovenian");
			DirectoryStrings.stringIDs.Add(1059422816U, "TelExtn");
			DirectoryStrings.stringIDs.Add(4045631128U, "LdapFilterErrorInvalidGtLtOperand");
			DirectoryStrings.stringIDs.Add(434185288U, "SystemMailboxRecipientType");
			DirectoryStrings.stringIDs.Add(2824730050U, "ReplicationTypeRemote");
			DirectoryStrings.stringIDs.Add(1414596097U, "Enterprise");
			DirectoryStrings.stringIDs.Add(3115737533U, "Gsm");
			DirectoryStrings.stringIDs.Add(4137480277U, "Journal");
			DirectoryStrings.stringIDs.Add(924597469U, "SpamFilteringTestActionNone");
			DirectoryStrings.stringIDs.Add(1877544294U, "CustomRoleDescription_MyPersonalInformation");
			DirectoryStrings.stringIDs.Add(1608856269U, "MailboxMoveStatusAutoSuspended");
			DirectoryStrings.stringIDs.Add(3068683316U, "Any");
			DirectoryStrings.stringIDs.Add(2325276717U, "Location");
			DirectoryStrings.stringIDs.Add(3031587385U, "ExternalTrust");
			DirectoryStrings.stringIDs.Add(2830455760U, "IndustryPrintingPublishing");
			DirectoryStrings.stringIDs.Add(3577501733U, "AllComputers");
			DirectoryStrings.stringIDs.Add(3208958876U, "ExceptionRusNotFound");
			DirectoryStrings.stringIDs.Add(2703120928U, "GroupNamingPolicyCity");
			DirectoryStrings.stringIDs.Add(1213668011U, "NoPagesSpecified");
			DirectoryStrings.stringIDs.Add(2286842903U, "PublicDatabaseRecipientType");
			DirectoryStrings.stringIDs.Add(4050233751U, "CanEnableLocalCopyState_CanBeEnabled");
			DirectoryStrings.stringIDs.Add(1069069500U, "RedirectToRecipientsNotSet");
			DirectoryStrings.stringIDs.Add(2472102570U, "InfoAnnouncementEnabled");
			DirectoryStrings.stringIDs.Add(3867415726U, "ConfigurationSettingsADConfigDriverError");
			DirectoryStrings.stringIDs.Add(1919923094U, "LdapFilterErrorEscCharWithoutEscapable");
			DirectoryStrings.stringIDs.Add(2829212743U, "IndustryGovernment");
			DirectoryStrings.stringIDs.Add(1921301802U, "CustomRoleDescription_MyAddressInformation");
			DirectoryStrings.stringIDs.Add(861191034U, "EsnLangNorwegianNynorsk");
			DirectoryStrings.stringIDs.Add(1996416364U, "IndustryEngineeringArchitecture");
			DirectoryStrings.stringIDs.Add(2551449657U, "SendAuthMechanismBasicAuth");
			DirectoryStrings.stringIDs.Add(4060941198U, "SKUCapabilityEOPPremiumAddOn");
			DirectoryStrings.stringIDs.Add(1820951283U, "ErrorResourceTypeInvalid");
			DirectoryStrings.stringIDs.Add(2134869325U, "OrgContainerNotFoundException");
			DirectoryStrings.stringIDs.Add(4076766949U, "SKUCapabilityBPOSSStandardArchive");
			DirectoryStrings.stringIDs.Add(1359848478U, "InternalSenderAdminAddressRequired");
			DirectoryStrings.stringIDs.Add(237887777U, "CannotGetUsefulDomainInfo");
			DirectoryStrings.stringIDs.Add(332960507U, "ErrorElcSuspensionNotEnabled");
			DirectoryStrings.stringIDs.Add(2173147846U, "DatabaseMasterTypeServer");
			DirectoryStrings.stringIDs.Add(3421832948U, "ConnectionTimeoutLessThanInactivityTimeout");
			DirectoryStrings.stringIDs.Add(444871648U, "HygieneSuitePremium");
			DirectoryStrings.stringIDs.Add(2986397362U, "Exadmin");
			DirectoryStrings.stringIDs.Add(2704331370U, "ExceptionADTopologyCannotFindWellKnownExchangeGroup");
			DirectoryStrings.stringIDs.Add(2979126483U, "CommandFrequency");
			DirectoryStrings.stringIDs.Add(1861502555U, "IndustryConstruction");
			DirectoryStrings.stringIDs.Add(4263249978U, "SharedMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(2830208040U, "AccessDeniedToEventLog");
			DirectoryStrings.stringIDs.Add(1445823720U, "EsnLangSerbian");
			DirectoryStrings.stringIDs.Add(2242173198U, "ReplicationTypeUnknown");
			DirectoryStrings.stringIDs.Add(2058453416U, "ErrorDuplicateMapiIdsInConfiguredAttributes");
			DirectoryStrings.stringIDs.Add(56170716U, "DirectoryBasedEdgeBlockModeOn");
			DirectoryStrings.stringIDs.Add(2380978387U, "LiveCredentialWithoutBasic");
			DirectoryStrings.stringIDs.Add(1607133185U, "ExclusiveConfigScopes");
			DirectoryStrings.stringIDs.Add(1118921376U, "IndustryRealEstate");
			DirectoryStrings.stringIDs.Add(2956380840U, "EsnLangNorwegian");
			DirectoryStrings.stringIDs.Add(1024471425U, "ServerRoleMonitoring");
			DirectoryStrings.stringIDs.Add(1106844962U, "ASInvalidAccessMethod");
			DirectoryStrings.stringIDs.Add(403740404U, "NotApplied");
			DirectoryStrings.stringIDs.Add(2619186021U, "ConfigurationSettingsADNotificationError");
			DirectoryStrings.stringIDs.Add(729925097U, "MonitoringMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(56435549U, "EsnLangCroatian");
			DirectoryStrings.stringIDs.Add(2386455749U, "TlsAuthLevelWithDomainSecureEnabled");
			DirectoryStrings.stringIDs.Add(4104902926U, "EsnLangGerman");
			DirectoryStrings.stringIDs.Add(3816616305U, "RoleAssignmentPolicyDescription_Default");
			DirectoryStrings.stringIDs.Add(252422050U, "GroupTypeFlagsNone");
			DirectoryStrings.stringIDs.Add(2167560764U, "WellKnownRecipientTypeMailboxUsers");
			DirectoryStrings.stringIDs.Add(1324265457U, "LdapFilterErrorInvalidWildCardGtLt");
			DirectoryStrings.stringIDs.Add(3536945350U, "SmartHostNotSet");
			DirectoryStrings.stringIDs.Add(3531789014U, "DeviceRule");
			DirectoryStrings.stringIDs.Add(1299460569U, "NotTrust");
			DirectoryStrings.stringIDs.Add(2824779686U, "EmailAgeFilterAll");
			DirectoryStrings.stringIDs.Add(1524653102U, "LanguageBlockListNotSet");
			DirectoryStrings.stringIDs.Add(2547761307U, "EsnLangSerbianCyrillic");
			DirectoryStrings.stringIDs.Add(1551636376U, "CalendarAgeFilterSixMonths");
			DirectoryStrings.stringIDs.Add(1442063141U, "ErrorMetadataNotSearchProperty");
			DirectoryStrings.stringIDs.Add(52431374U, "InvalidDefaultMailbox");
			DirectoryStrings.stringIDs.Add(115734878U, "Drafts");
			DirectoryStrings.stringIDs.Add(2819908830U, "RemoteGroupMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(289102393U, "EsnLangSwahili");
			DirectoryStrings.stringIDs.Add(794922827U, "ExceptionPagedReaderReadAllAfterEnumerating");
			DirectoryStrings.stringIDs.Add(835151952U, "DsnDefaultLanguageMustBeSpecificCulture");
			DirectoryStrings.stringIDs.Add(719400811U, "BestBodyFormat");
			DirectoryStrings.stringIDs.Add(3064393132U, "CanEnableLocalCopyState_AlreadyEnabled");
			DirectoryStrings.stringIDs.Add(1010456570U, "DeviceDiscovery");
			DirectoryStrings.stringIDs.Add(3963885811U, "AccessDenied");
			DirectoryStrings.stringIDs.Add(29593584U, "InvalidActiveUserStatisticsLogSizeConfiguration");
			DirectoryStrings.stringIDs.Add(1014182364U, "ErrorActionOnExpirationSpecified");
			DirectoryStrings.stringIDs.Add(3776155750U, "TlsAuthLevelWithNoDomainOnSmartHost");
			DirectoryStrings.stringIDs.Add(1587883572U, "DeferredFailoverEntryString");
			DirectoryStrings.stringIDs.Add(4110637509U, "TaskItemsMC");
			DirectoryStrings.stringIDs.Add(1924734197U, "GroupNamingPolicyCustomAttribute7");
			DirectoryStrings.stringIDs.Add(2647513696U, "UnknownAttribute");
			DirectoryStrings.stringIDs.Add(703634004U, "MountDialOverrideBestAvailability");
			DirectoryStrings.stringIDs.Add(3562314173U, "ErrorArbitrationMailboxPropertyEmailAddressesEmpty");
			DirectoryStrings.stringIDs.Add(608628016U, "AlternateServiceAccountCredentialNotSet");
			DirectoryStrings.stringIDs.Add(2092029626U, "DataMoveReplicationConstraintAllCopies");
			DirectoryStrings.stringIDs.Add(3902771789U, "GlobalThrottlingPolicyAmbiguousException");
			DirectoryStrings.stringIDs.Add(2998146498U, "InvalidServerStatisticsLogSizeConfiguration");
			DirectoryStrings.stringIDs.Add(3361458474U, "SipResourceIdRequired");
			DirectoryStrings.stringIDs.Add(1616139245U, "EsnLangPortuguese");
			DirectoryStrings.stringIDs.Add(4241336410U, "AutoDetect");
			DirectoryStrings.stringIDs.Add(4255105347U, "SpamFilteringActionRedirect");
			DirectoryStrings.stringIDs.Add(3532819894U, "CanRunRestoreState_Invalid");
			DirectoryStrings.stringIDs.Add(2530566197U, "OutboundConnectorIncorrectCloudServicesMailEnabled");
			DirectoryStrings.stringIDs.Add(95325995U, "DatabaseCopyAutoActivationPolicyBlocked");
			DirectoryStrings.stringIDs.Add(3838277697U, "CustomRoleDescription_MyName");
			DirectoryStrings.stringIDs.Add(3720125794U, "EsnLangOriya");
			DirectoryStrings.stringIDs.Add(702028774U, "UserAgent");
			DirectoryStrings.stringIDs.Add(4211599483U, "DomainStateActive");
			DirectoryStrings.stringIDs.Add(1640391059U, "PartnersCannotHaveWildcards");
			DirectoryStrings.stringIDs.Add(352018919U, "IPv4Only");
			DirectoryStrings.stringIDs.Add(4150730333U, "InboundConnectorInvalidIPCertificateCombinations");
			DirectoryStrings.stringIDs.Add(2814018773U, "Exchange2003or2000");
			DirectoryStrings.stringIDs.Add(2730108605U, "ErrorOneProcessInitializedAsBothSingleAndMultiple");
			DirectoryStrings.stringIDs.Add(1391517930U, "RoomListGroupTypeDetails");
			DirectoryStrings.stringIDs.Add(3586618070U, "MailEnabledForestContactRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(2286188335U, "ErrorAuthMetadataNoIssuingEndpoint");
			DirectoryStrings.stringIDs.Add(2227190334U, "NonUniversalGroupRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(2587673404U, "ErrorMustBeSysConfigObject");
			DirectoryStrings.stringIDs.Add(2742534530U, "OutboundConnectorTlsSettingsInvalidTlsDomainWithoutDomainValidation");
			DirectoryStrings.stringIDs.Add(135248896U, "LdapFilterErrorInvalidBitwiseOperand");
			DirectoryStrings.stringIDs.Add(2106980546U, "ExceptionSetPreferredDCsOnlyForManagement");
			DirectoryStrings.stringIDs.Add(3635271833U, "LegacyArchiveJournals");
			DirectoryStrings.stringIDs.Add(2980205681U, "CustomInternalSubjectRequired");
			DirectoryStrings.stringIDs.Add(238304980U, "ErrorCannotAddArchiveMailbox");
			DirectoryStrings.stringIDs.Add(1479682494U, "NoNewCalls");
			DirectoryStrings.stringIDs.Add(1615193486U, "ErrorMessageClassEmpty");
			DirectoryStrings.stringIDs.Add(189075208U, "GloballyDistributedOABCacheReadTimeoutError");
			DirectoryStrings.stringIDs.Add(523533880U, "Manual");
			DirectoryStrings.stringIDs.Add(3179357576U, "ErrorAcceptedDomainCannotContainWildcardAndNegoConfig");
			DirectoryStrings.stringIDs.Add(968858937U, "UniversalSecurityGroupRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(3647297993U, "ArbitrationMailboxTypeDetails");
			DirectoryStrings.stringIDs.Add(575705180U, "CalendarAgeFilterAll");
			DirectoryStrings.stringIDs.Add(862838650U, "GroupNamingPolicyCompany");
			DirectoryStrings.stringIDs.Add(700328008U, "IndustryMining");
			DirectoryStrings.stringIDs.Add(2775202161U, "ServerRoleOSP");
			DirectoryStrings.stringIDs.Add(1190445522U, "InvalidDirectoryConfiguration");
			DirectoryStrings.stringIDs.Add(3381355689U, "ErrorDDLReferral");
			DirectoryStrings.stringIDs.Add(3541370315U, "LdapFilterErrorNoAttributeValue");
			DirectoryStrings.stringIDs.Add(606960029U, "ExternalEnrollment");
			DirectoryStrings.stringIDs.Add(161599950U, "ErrorTimeoutReadingSystemAddressListCache");
			DirectoryStrings.stringIDs.Add(2375038189U, "CanRunDefaultUpdateState_NotSuspended");
			DirectoryStrings.stringIDs.Add(1615928121U, "PreferredInternetCodePageSio2022Jp");
			DirectoryStrings.stringIDs.Add(1762945050U, "HtmlAndTextAlternative");
			DirectoryStrings.stringIDs.Add(1164140307U, "GlobalAddressList");
			DirectoryStrings.stringIDs.Add(1912797067U, "MailTipsAccessLevelNone");
			DirectoryStrings.stringIDs.Add(3599499864U, "EsnLangGalician");
			DirectoryStrings.stringIDs.Add(3786203794U, "ServerRoleFrontendTransport");
			DirectoryStrings.stringIDs.Add(4087400250U, "Exchange2009");
			DirectoryStrings.stringIDs.Add(1259815309U, "TransientMservErrorDescription");
			DirectoryStrings.stringIDs.Add(48855524U, "ReceiveAuthMechanismExchangeServer");
			DirectoryStrings.stringIDs.Add(3380639415U, "Watsons");
			DirectoryStrings.stringIDs.Add(2074397341U, "OrganizationCapabilityPstProvider");
			DirectoryStrings.stringIDs.Add(2277391560U, "ErrorCapabilityNone");
			DirectoryStrings.stringIDs.Add(224904099U, "ExceptionAllDomainControllersUnavailable");
			DirectoryStrings.stringIDs.Add(3495902273U, "ServersContainerNotFoundException");
			DirectoryStrings.stringIDs.Add(296014363U, "MailboxMoveStatusCompletionInProgress");
			DirectoryStrings.stringIDs.Add(2125756541U, "ServerRoleMailbox");
			DirectoryStrings.stringIDs.Add(3930280380U, "ErrorResourceTypeMissing");
			DirectoryStrings.stringIDs.Add(1716044995U, "Contacts");
			DirectoryStrings.stringIDs.Add(3268644368U, "SendAuthMechanismTls");
			DirectoryStrings.stringIDs.Add(3574113162U, "AggregatedSessionCannotMakeMbxChanges");
			DirectoryStrings.stringIDs.Add(1802707653U, "PAAEnabled");
			DirectoryStrings.stringIDs.Add(2191519417U, "NonPartner");
			DirectoryStrings.stringIDs.Add(4008691139U, "BasicAfterTLSWithoutBasic");
			DirectoryStrings.stringIDs.Add(2672001483U, "ErrorSharedConfigurationBothRoles");
			DirectoryStrings.stringIDs.Add(4077321274U, "EsnLangDutch");
			DirectoryStrings.stringIDs.Add(1406386932U, "DsnLanguageNotSupportedForCustomization");
			DirectoryStrings.stringIDs.Add(2440749065U, "IndustryNotSpecified");
			DirectoryStrings.stringIDs.Add(1045941944U, "ErrorDDLFilterError");
			DirectoryStrings.stringIDs.Add(3599602982U, "AddressList");
			DirectoryStrings.stringIDs.Add(795884132U, "MustDisplayComment");
			DirectoryStrings.stringIDs.Add(3464146580U, "ServerRoleFfoWebServices");
			DirectoryStrings.stringIDs.Add(1052758952U, "ServerRoleClientAccess");
			DirectoryStrings.stringIDs.Add(3595585153U, "SKUCapabilityBPOSSEnterprise");
			DirectoryStrings.stringIDs.Add(3839109662U, "InvalidReceiveAuthModeExternalOnly");
			DirectoryStrings.stringIDs.Add(2653001089U, "ErrorSettingOverrideNull");
			DirectoryStrings.stringIDs.Add(4230107429U, "LdapFilterErrorQueryTooLong");
			DirectoryStrings.stringIDs.Add(1655452658U, "ErrorMoveToDestinationFolderNotDefined");
			DirectoryStrings.stringIDs.Add(4190154187U, "MailboxMoveStatusInProgress");
			DirectoryStrings.stringIDs.Add(370461711U, "SecurityPrincipalTypeGroup");
			DirectoryStrings.stringIDs.Add(1470218539U, "X400Authoritative");
			DirectoryStrings.stringIDs.Add(3038327607U, "MailFlowPartnerInternalMailContentTypeMimeHtml");
			DirectoryStrings.stringIDs.Add(3689869554U, "MailEnabledUserRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(2902411778U, "ExtensionNull");
			DirectoryStrings.stringIDs.Add(1573777228U, "Unsecured");
			DirectoryStrings.stringIDs.Add(4079674260U, "ConnectorIdIsNotAnInteger");
			DirectoryStrings.stringIDs.Add(2617699176U, "ErrorMissingPrimaryUM");
			DirectoryStrings.stringIDs.Add(3783332642U, "CannotDetermineDataSessionType");
			DirectoryStrings.stringIDs.Add(3601314308U, "UserAgentsChanges");
			DirectoryStrings.stringIDs.Add(1601836855U, "Notes");
			DirectoryStrings.stringIDs.Add(2395522212U, "EsnLangTelugu");
			DirectoryStrings.stringIDs.Add(65728472U, "GroupNamingPolicyExtensionCustomAttribute1");
			DirectoryStrings.stringIDs.Add(1221325234U, "MailFlowPartnerInternalMailContentTypeNone");
			DirectoryStrings.stringIDs.Add(268472571U, "DefaultRapName");
			DirectoryStrings.stringIDs.Add(3648445463U, "DeleteUseDefaultAlert");
			DirectoryStrings.stringIDs.Add(4156100093U, "ErrorOrganizationResourceAddressListsCount");
			DirectoryStrings.stringIDs.Add(3706505019U, "EsnLangChineseSimplified");
			DirectoryStrings.stringIDs.Add(1919306754U, "ConferenceRoomMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(557877518U, "BlockedOutlookClientVersionPatternDescription");
			DirectoryStrings.stringIDs.Add(1448153692U, "UserHasNoSmtpProxyAddressWithFederatedDomain");
			DirectoryStrings.stringIDs.Add(2296213214U, "OrganizationCapabilityMailRouting");
			DirectoryStrings.stringIDs.Add(1325366747U, "SKUCapabilityBPOSSStandard");
			DirectoryStrings.stringIDs.Add(1850977098U, "SystemMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(3702456775U, "ExceptionADTopologyNoLocalDomain");
			DirectoryStrings.stringIDs.Add(3504940969U, "EsnLangDanish");
			DirectoryStrings.stringIDs.Add(779349581U, "IndustryRetail");
			DirectoryStrings.stringIDs.Add(2864464497U, "ErrorDDLNoSuchObject");
			DirectoryStrings.stringIDs.Add(2713172052U, "IndustryComputerRelatedProductsServices");
			DirectoryStrings.stringIDs.Add(3288506612U, "InternalRelay");
			DirectoryStrings.stringIDs.Add(602695546U, "ErrorEmptyArchiveName");
			DirectoryStrings.stringIDs.Add(1231743030U, "EmailAddressPolicyPriorityLowest");
			DirectoryStrings.stringIDs.Add(326106109U, "ExternalMdm");
			DirectoryStrings.stringIDs.Add(3799883362U, "TransportSettingsNotFoundException");
			DirectoryStrings.stringIDs.Add(3552372249U, "DomainSecureEnabledWithoutTls");
			DirectoryStrings.stringIDs.Add(1904959661U, "BccSuspiciousOutboundAdditionalRecipientsRequired");
			DirectoryStrings.stringIDs.Add(4291428005U, "NoRoleEntriesFound");
			DirectoryStrings.stringIDs.Add(1515341016U, "IndustryWholesale");
			DirectoryStrings.stringIDs.Add(3980237751U, "ServerRoleCentralAdminFrontEnd");
			DirectoryStrings.stringIDs.Add(4068243401U, "ErrorInvalidPushNotificationPlatform");
			DirectoryStrings.stringIDs.Add(2277405024U, "MailTipsAccessLevelAll");
			DirectoryStrings.stringIDs.Add(1625030180U, "PublicFolderRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(943620946U, "ValueNotAvailableForUnchangedProperty");
			DirectoryStrings.stringIDs.Add(3641768400U, "DumpsterFolder");
			DirectoryStrings.stringIDs.Add(1756055255U, "CannotParseMimeTypes");
			DirectoryStrings.stringIDs.Add(2518725308U, "ExclusiveRecipientScopes");
			DirectoryStrings.stringIDs.Add(1771494609U, "QuarantineMailboxIsInvalid");
			DirectoryStrings.stringIDs.Add(1094750789U, "MailboxPlanTypeDetails");
			DirectoryStrings.stringIDs.Add(986970413U, "ServerRoleCafeArray");
			DirectoryStrings.stringIDs.Add(2874697860U, "SendCredentialIsNull");
			DirectoryStrings.stringIDs.Add(3323264318U, "True");
			DirectoryStrings.stringIDs.Add(300685400U, "StarAcceptedDomainCannotBeAuthoritative");
			DirectoryStrings.stringIDs.Add(2302903917U, "AllRooms");
			DirectoryStrings.stringIDs.Add(2650345129U, "EsnLangRussian");
			DirectoryStrings.stringIDs.Add(3989445055U, "GroupNamingPolicyCustomAttribute10");
			DirectoryStrings.stringIDs.Add(79975170U, "SitesContainerNotFound");
			DirectoryStrings.stringIDs.Add(1581344072U, "ExceptionServerTimeoutNegative");
			DirectoryStrings.stringIDs.Add(665936024U, "ArchiveStateLocal");
			DirectoryStrings.stringIDs.Add(991629433U, "NotesMC");
			DirectoryStrings.stringIDs.Add(3403459873U, "InvalidDomain");
			DirectoryStrings.stringIDs.Add(2092969439U, "EmailAgeFilterOneMonth");
			DirectoryStrings.stringIDs.Add(4021009371U, "FullDomain");
			DirectoryStrings.stringIDs.Add(1430923151U, "DeviceModel");
			DirectoryStrings.stringIDs.Add(777275992U, "GroupRecipientType");
			DirectoryStrings.stringIDs.Add(444885611U, "RemoteSharedMailboxTypeDetails");
			DirectoryStrings.stringIDs.Add(261312351U, "LdapSearch");
			DirectoryStrings.stringIDs.Add(3378383244U, "EsnLangArabic");
			DirectoryStrings.stringIDs.Add(3490390166U, "SKUCapabilityBPOSSDeskless");
			DirectoryStrings.stringIDs.Add(3131560893U, "ModeratedRecipients");
			DirectoryStrings.stringIDs.Add(1935657977U, "ExceptionRusOperationFailed");
			DirectoryStrings.stringIDs.Add(4110564385U, "ExceptionDomainInfoRpcTooBusy");
			DirectoryStrings.stringIDs.Add(3057991035U, "ErrorArchiveDomainInvalidInDatacenter");
			DirectoryStrings.stringIDs.Add(2922780138U, "PublicFolderRecipientType");
			DirectoryStrings.stringIDs.Add(2843126128U, "ErrorMessageClassHasUnsupportedWildcard");
			DirectoryStrings.stringIDs.Add(2976002772U, "ErrorPipelineTracingRequirementsMissing");
			DirectoryStrings.stringIDs.Add(2423361114U, "GroupNamingPolicyCustomAttribute11");
			DirectoryStrings.stringIDs.Add(65716788U, "ErrorMailTipMustNotBeEmpty");
			DirectoryStrings.stringIDs.Add(1479699766U, "ComputerRecipientType");
			DirectoryStrings.stringIDs.Add(2493930652U, "ErrorArbitrationMailboxCannotBeModerated");
			DirectoryStrings.stringIDs.Add(3119627512U, "EsnLangKannada");
			DirectoryStrings.stringIDs.Add(2435266816U, "Title");
			DirectoryStrings.stringIDs.Add(389262922U, "MessageWaitingIndicatorEnabled");
			DirectoryStrings.stringIDs.Add(3178475968U, "PublicFolders");
			DirectoryStrings.stringIDs.Add(92440185U, "Millisecond");
			DirectoryStrings.stringIDs.Add(2661434578U, "StarAcceptedDomainCannotBeDefault");
			DirectoryStrings.stringIDs.Add(2033242172U, "ReceiveExtendedProtectionPolicyAllow");
			DirectoryStrings.stringIDs.Add(2995964430U, "ResourceMailbox");
			DirectoryStrings.stringIDs.Add(3176413521U, "ErrorThrottlingPolicyStateIsCorrupt");
			DirectoryStrings.stringIDs.Add(3005725472U, "MailEnabledNonUniversalGroupRecipientType");
			DirectoryStrings.stringIDs.Add(573230607U, "ExternalAuthoritativeWithoutExchangeServerPermission");
			DirectoryStrings.stringIDs.Add(2913015079U, "Authoritative");
			DirectoryStrings.stringIDs.Add(2813895538U, "ErrorPrimarySmtpAddressAndWindowsEmailAddressNotMatch");
			DirectoryStrings.stringIDs.Add(1421974844U, "PostMC");
			DirectoryStrings.stringIDs.Add(3819234583U, "UnknownConfigObject");
			DirectoryStrings.stringIDs.Add(4274370117U, "MalwareScanErrorActionAllow");
			DirectoryStrings.stringIDs.Add(1924734198U, "GroupNamingPolicyCustomAttribute6");
			DirectoryStrings.stringIDs.Add(2610662106U, "InvalidTransportSyncLogSizeConfiguration");
			DirectoryStrings.stringIDs.Add(3725493575U, "WellKnownRecipientTypeMailGroups");
			DirectoryStrings.stringIDs.Add(120558192U, "ADDriverStoreAccessTransientError");
			DirectoryStrings.stringIDs.Add(635049629U, "AACantChangeName");
			DirectoryStrings.stringIDs.Add(3247735886U, "ContactItemsMC");
			DirectoryStrings.stringIDs.Add(4170667976U, "EsnLangKorean");
			DirectoryStrings.stringIDs.Add(131007819U, "RssSubscriptionMC");
			DirectoryStrings.stringIDs.Add(1652093200U, "LdapFilterErrorSpaceMiddleType");
			DirectoryStrings.stringIDs.Add(1924734193U, "GroupNamingPolicyCustomAttribute3");
			DirectoryStrings.stringIDs.Add(3167491578U, "ExceptionNoFsmoRoleOwnerAttribute");
			DirectoryStrings.stringIDs.Add(600983985U, "NonIpmRoot");
			DirectoryStrings.stringIDs.Add(2829159765U, "ErrorTimeoutWritingSystemAddressListMemberCount");
			DirectoryStrings.stringIDs.Add(2900785706U, "ExceptionExternalError");
			DirectoryStrings.stringIDs.Add(1292798904U, "Calendar");
			DirectoryStrings.stringIDs.Add(2665399355U, "Wma");
			DirectoryStrings.stringIDs.Add(869401742U, "ErrorInvalidDNDepth");
			DirectoryStrings.stringIDs.Add(1435812789U, "CapabilityMasteredOnPremise");
			DirectoryStrings.stringIDs.Add(371000500U, "EdgeSyncEhfConnectorFailedToDecryptPassword");
			DirectoryStrings.stringIDs.Add(1758334214U, "ErrorArchiveDomainSetForNonArchive");
			DirectoryStrings.stringIDs.Add(3086681225U, "ExceptionObjectHasBeenDeleted");
			DirectoryStrings.stringIDs.Add(2211212701U, "EsnLangBengaliIndia");
			DirectoryStrings.stringIDs.Add(613950136U, "PublicFolderServer");
			DirectoryStrings.stringIDs.Add(4127869723U, "ErrorCannotSetPrimarySmtpAddress");
			DirectoryStrings.stringIDs.Add(2852597951U, "SpamFilteringActionQuarantine");
			DirectoryStrings.stringIDs.Add(1313260064U, "MailboxMoveStatusFailed");
			DirectoryStrings.stringIDs.Add(1169031248U, "SecurityPrincipalTypeUniversalSecurityGroup");
			DirectoryStrings.stringIDs.Add(1254627662U, "DynamicDLRecipientType");
			DirectoryStrings.stringIDs.Add(2863183086U, "ErrorNonTinyTenantShouldNotHaveSharedConfig");
			DirectoryStrings.stringIDs.Add(3057941193U, "CanRunRestoreState_Allowed");
			DirectoryStrings.stringIDs.Add(1199714779U, "DomainSecureWithIgnoreStartTLSEnabled");
			DirectoryStrings.stringIDs.Add(825243359U, "GroupNamingPolicyExtensionCustomAttribute4");
			DirectoryStrings.stringIDs.Add(3705158290U, "UseMsg");
			DirectoryStrings.stringIDs.Add(149761450U, "InvalidTenantFullSyncCookieException");
			DirectoryStrings.stringIDs.Add(3241000569U, "AutoDatabaseMountDialGoodAvailability");
			DirectoryStrings.stringIDs.Add(4056279737U, "ForestTrust");
			DirectoryStrings.stringIDs.Add(556546691U, "ErrorInvalidMailboxRelationType");
			DirectoryStrings.stringIDs.Add(2757465550U, "ErrorDDLInvalidDNSyntax");
			DirectoryStrings.stringIDs.Add(3615619130U, "ByteEncoderTypeUseQP");
			DirectoryStrings.stringIDs.Add(2204790954U, "NoLocatorInformationInMServException");
			DirectoryStrings.stringIDs.Add(2004555878U, "SecurityPrincipalTypeGlobalSecurityGroup");
			DirectoryStrings.stringIDs.Add(2999553646U, "CannotGetUsefulSiteInfo");
			DirectoryStrings.stringIDs.Add(1031444357U, "ErrorPipelineTracingPathNotExist");
			DirectoryStrings.stringIDs.Add(1832080745U, "MailboxServer");
			DirectoryStrings.stringIDs.Add(4019774802U, "Blocked");
			DirectoryStrings.stringIDs.Add(2649572709U, "InvalidMainStreamCookieException");
			DirectoryStrings.stringIDs.Add(1341999288U, "MoveNotAllowed");
			DirectoryStrings.stringIDs.Add(1594549261U, "RemoteRoomMailboxTypeDetails");
			DirectoryStrings.stringIDs.Add(1776235905U, "SecurityPrincipalTypeUser");
			DirectoryStrings.stringIDs.Add(2078807267U, "TextEnrichedOnly");
			DirectoryStrings.stringIDs.Add(3036506883U, "BluetoothAllow");
			DirectoryStrings.stringIDs.Add(154085973U, "GroupNamingPolicyDepartment");
			DirectoryStrings.stringIDs.Add(3388588817U, "UseDefaultSettings");
			DirectoryStrings.stringIDs.Add(3102724093U, "ByteEncoderTypeUseQPHtmlDetectTextPlain");
			DirectoryStrings.stringIDs.Add(2924600836U, "Exchange2007");
			DirectoryStrings.stringIDs.Add(788602100U, "DisabledPartner");
			DirectoryStrings.stringIDs.Add(1344968854U, "Consumer");
			DirectoryStrings.stringIDs.Add(1013979892U, "PrimaryMailboxRelationType");
			DirectoryStrings.stringIDs.Add(1484405454U, "Disabled");
			DirectoryStrings.stringIDs.Add(1091797613U, "SKUCapabilityBPOSSBasicCustomDomain");
			DirectoryStrings.stringIDs.Add(2308256473U, "ControlTextNull");
			DirectoryStrings.stringIDs.Add(629464291U, "Outbox");
			DirectoryStrings.stringIDs.Add(3086386447U, "ArchiveStateNone");
			DirectoryStrings.stringIDs.Add(1727459539U, "MailFlowPartnerInternalMailContentTypeMimeText");
			DirectoryStrings.stringIDs.Add(2238564813U, "CustomInternalBodyRequired");
			DirectoryStrings.stringIDs.Add(2086215909U, "TlsDomainWithIncorrectTlsAuthLevel");
			DirectoryStrings.stringIDs.Add(213405127U, "SystemTag");
			DirectoryStrings.stringIDs.Add(65301504U, "AllMailboxContentMC");
			DirectoryStrings.stringIDs.Add(4145265495U, "RemoteUserMailboxTypeDetails");
			DirectoryStrings.stringIDs.Add(3441693128U, "BluetoothDisable");
			DirectoryStrings.stringIDs.Add(2698858797U, "ServerRoleLanguagePacks");
			DirectoryStrings.stringIDs.Add(1415894913U, "PrincipalName");
			DirectoryStrings.stringIDs.Add(3541826428U, "IdIsNotSet");
			DirectoryStrings.stringIDs.Add(1254345332U, "ConstraintViolationSupervisionListEntryStringPartIsInvalid");
			DirectoryStrings.stringIDs.Add(2638599330U, "WellKnownRecipientTypeMailContacts");
			DirectoryStrings.stringIDs.Add(172810921U, "ServerRoleHubTransport");
			DirectoryStrings.stringIDs.Add(4251572755U, "IndustryHealthcare");
			DirectoryStrings.stringIDs.Add(3956811795U, "CapabilityPartnerManaged");
			DirectoryStrings.stringIDs.Add(4087147933U, "ErrorArchiveDatabaseArchiveDomainMissing");
			DirectoryStrings.stringIDs.Add(2967905667U, "MailEnabledUniversalSecurityGroupRecipientType");
			DirectoryStrings.stringIDs.Add(1486937545U, "ErrorRemovalNotSupported");
			DirectoryStrings.stringIDs.Add(1129549138U, "ExchangeFaxMC");
			DirectoryStrings.stringIDs.Add(2611743021U, "ByteEncoderTypeUse7Bit");
			DirectoryStrings.stringIDs.Add(2176173662U, "InvalidBindingAddressSetting");
			DirectoryStrings.stringIDs.Add(4172461161U, "ASAccessMethodNeedsAuthenticationAccount");
			DirectoryStrings.stringIDs.Add(491964191U, "CanRunDefaultUpdateState_Allowed");
			DirectoryStrings.stringIDs.Add(2884121764U, "EsnLangMalay");
			DirectoryStrings.stringIDs.Add(1167380226U, "FailedToParseAlternateServiceAccountCredential");
			DirectoryStrings.stringIDs.Add(3799817423U, "ExternalManagedMailContactTypeDetails");
			DirectoryStrings.stringIDs.Add(1403090333U, "IPv6Only");
			DirectoryStrings.stringIDs.Add(2827463711U, "MountDialOverrideLossless");
			DirectoryStrings.stringIDs.Add(3607366039U, "Percent");
			DirectoryStrings.stringIDs.Add(1922689150U, "ServerRoleProvisionedServer");
			DirectoryStrings.stringIDs.Add(2350890097U, "CalendarAgeFilterOneMonth");
			DirectoryStrings.stringIDs.Add(4169982073U, "TextOnly");
			DirectoryStrings.stringIDs.Add(1291987412U, "InvalidMsgTrackingLogSizeConfiguration");
			DirectoryStrings.stringIDs.Add(1881847987U, "ErrorArchiveDatabaseSetForNonArchive");
			DirectoryStrings.stringIDs.Add(1940813882U, "InvalidGenerationTime");
			DirectoryStrings.stringIDs.Add(820626981U, "CalendarItemMC");
			DirectoryStrings.stringIDs.Add(3745862197U, "Block");
			DirectoryStrings.stringIDs.Add(3519747066U, "ErrorNullExternalEmailAddress");
			DirectoryStrings.stringIDs.Add(1681980649U, "ExceptionRusNotRunning");
			DirectoryStrings.stringIDs.Add(862550630U, "PropertyCannotBeSetToTest");
			DirectoryStrings.stringIDs.Add(1641698628U, "LdapFilterErrorInvalidEscaping");
			DirectoryStrings.stringIDs.Add(867516332U, "ForceSave");
			DirectoryStrings.stringIDs.Add(1798526791U, "LinkedRoomMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(3794956711U, "DeleteUseCustomAlert");
			DirectoryStrings.stringIDs.Add(370123223U, "CannotDeserializePartitionHint");
			DirectoryStrings.stringIDs.Add(1284675050U, "InboundConnectorInvalidRestrictDomainsToIPAddresses");
			DirectoryStrings.stringIDs.Add(1663846227U, "GroupNamingPolicyCustomAttribute14");
			DirectoryStrings.stringIDs.Add(2839121159U, "ContactRecipientType");
			DirectoryStrings.stringIDs.Add(2600481667U, "DomainSecureWithoutDNSRoutingEnabled");
			DirectoryStrings.stringIDs.Add(3062547699U, "RunspaceServerSettingsChanged");
			DirectoryStrings.stringIDs.Add(3313482992U, "EsnLangGreek");
			DirectoryStrings.stringIDs.Add(309994549U, "TooManyEntriesError");
			DirectoryStrings.stringIDs.Add(1899391140U, "OrganizationRelationshipMissingTargetApplicationUri");
			DirectoryStrings.stringIDs.Add(3489169852U, "ComputerRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(96146822U, "Exchweb");
			DirectoryStrings.stringIDs.Add(2202908911U, "OutboundConnectorIncorrectRouteAllMessagesViaOnPremises");
			DirectoryStrings.stringIDs.Add(1499257862U, "CalendarSharingFreeBusyAvailabilityOnly");
			DirectoryStrings.stringIDs.Add(3707194059U, "ServerRoleExtendedRole5");
			DirectoryStrings.stringIDs.Add(390976058U, "AutoAttendantLink");
			DirectoryStrings.stringIDs.Add(4020865807U, "CustomRoleDescription_MyDisplayName");
			DirectoryStrings.stringIDs.Add(3949283739U, "AllUsers");
			DirectoryStrings.stringIDs.Add(4231482709U, "All");
			DirectoryStrings.stringIDs.Add(4256840071U, "OrganizationCapabilityMigration");
			DirectoryStrings.stringIDs.Add(142272059U, "DialPlan");
			DirectoryStrings.stringIDs.Add(2868284030U, "EsnLangUkrainian");
			DirectoryStrings.stringIDs.Add(2052645173U, "MessageRateSourceFlagsNone");
			DirectoryStrings.stringIDs.Add(2196808673U, "IndustryLegal");
			DirectoryStrings.stringIDs.Add(3379397641U, "CapabilityUMFeatureRestricted");
			DirectoryStrings.stringIDs.Add(1494101274U, "GroupTypeFlagsBuiltinLocal");
			DirectoryStrings.stringIDs.Add(2412300803U, "ReceiveAuthMechanismBasicAuthPlusTls");
			DirectoryStrings.stringIDs.Add(3811183882U, "Allowed");
			DirectoryStrings.stringIDs.Add(3579894660U, "ByteEncoderTypeUseQPHtml7BitTextPlain");
			DirectoryStrings.stringIDs.Add(4217035038U, "High");
			DirectoryStrings.stringIDs.Add(821502958U, "MicrosoftExchangeRecipientType");
			DirectoryStrings.stringIDs.Add(1429014682U, "BackSyncDataSourceUnavailableMessage");
			DirectoryStrings.stringIDs.Add(110833865U, "ArchiveStateOnPremise");
			DirectoryStrings.stringIDs.Add(936096413U, "OrganizationCapabilitySuiteServiceStorage");
			DirectoryStrings.stringIDs.Add(1592544157U, "MalwareScanErrorActionBlock");
			DirectoryStrings.stringIDs.Add(1924944146U, "SKUCapabilityBPOSSArchiveAddOn");
			DirectoryStrings.stringIDs.Add(4227895642U, "ExceptionRusAccessDenied");
			DirectoryStrings.stringIDs.Add(2094315795U, "ServerRoleNone");
			DirectoryStrings.stringIDs.Add(3352185505U, "AlternateServiceAccountConfigurationDisplayFormatMoreDataAvailable");
			DirectoryStrings.stringIDs.Add(2728392679U, "GloballyDistributedOABCacheWriteTimeoutError");
			DirectoryStrings.stringIDs.Add(3727360630U, "UserName");
			DirectoryStrings.stringIDs.Add(1173768533U, "Reserved1");
			DirectoryStrings.stringIDs.Add(3144162877U, "NoAddresses");
			DirectoryStrings.stringIDs.Add(2817538580U, "RegionBlockListNotSet");
			DirectoryStrings.stringIDs.Add(1398191848U, "CapabilityRichCoexistence");
			DirectoryStrings.stringIDs.Add(2032072470U, "ErrorUserAccountNameIncludeAt");
			DirectoryStrings.stringIDs.Add(634395589U, "Enabled");
			DirectoryStrings.stringIDs.Add(3840534502U, "AttachmentsWereRemovedMessage");
			DirectoryStrings.stringIDs.Add(4176423907U, "ErrorCannotFindUnusedLegacyDN");
			DirectoryStrings.stringIDs.Add(2318114319U, "EmailAgeFilterOneWeek");
			DirectoryStrings.stringIDs.Add(2193124873U, "GroupNameInNamingPolicy");
			DirectoryStrings.stringIDs.Add(2504573058U, "OrganizationCapabilityClientExtensions");
			DirectoryStrings.stringIDs.Add(3081766090U, "CalendarAgeFilterTwoWeeks");
			DirectoryStrings.stringIDs.Add(583050472U, "ErrorElcCommentNotAllowed");
			DirectoryStrings.stringIDs.Add(3275453767U, "ErrorOwnersUpdated");
			DirectoryStrings.stringIDs.Add(3776377092U, "EsnLangIndonesian");
			DirectoryStrings.stringIDs.Add(2631270417U, "Extension");
			DirectoryStrings.stringIDs.Add(1468404604U, "CanEnableLocalCopyState_Invalid");
			DirectoryStrings.stringIDs.Add(2146247679U, "MailEnabledUniversalDistributionGroupRecipientType");
			DirectoryStrings.stringIDs.Add(893817173U, "ReceiveCredentialIsNull");
			DirectoryStrings.stringIDs.Add(1885276797U, "EsnLangLithuanian");
			DirectoryStrings.stringIDs.Add(570563164U, "ServerRoleAll");
			DirectoryStrings.stringIDs.Add(756854696U, "ServerRoleEdge");
			DirectoryStrings.stringIDs.Add(982491582U, "ExceptionObjectStillExists");
			DirectoryStrings.stringIDs.Add(387112589U, "AllRecipients");
			DirectoryStrings.stringIDs.Add(3685369418U, "LdapFilterErrorNoAttributeType");
			DirectoryStrings.stringIDs.Add(3802186670U, "ServerRoleManagementFrontEnd");
			DirectoryStrings.stringIDs.Add(2609910045U, "False");
			DirectoryStrings.stringIDs.Add(519619317U, "CalendarSharingFreeBusyLimitedDetails");
			DirectoryStrings.stringIDs.Add(2662725163U, "SystemAttendantMailboxRecipientType");
			DirectoryStrings.stringIDs.Add(696678862U, "ServerRoleManagementBackEnd");
			DirectoryStrings.stringIDs.Add(4088287609U, "GroupNamingPolicyStateOrProvince");
			DirectoryStrings.stringIDs.Add(1955666018U, "IndustryFinance");
			DirectoryStrings.stringIDs.Add(3313162693U, "ErrorAgeLimitExpiration");
			DirectoryStrings.stringIDs.Add(2067616247U, "InboundConnectorMissingTlsCertificateOrSenderIP");
			DirectoryStrings.stringIDs.Add(1247338605U, "ErrorMailTipTranslationFormatIncorrect");
			DirectoryStrings.stringIDs.Add(3892578519U, "MountDialOverrideGoodAvailability");
			DirectoryStrings.stringIDs.Add(2615196936U, "ConfigReadScope");
			DirectoryStrings.stringIDs.Add(2338964630U, "UserRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(3291024868U, "MeetingRequestMC");
			DirectoryStrings.stringIDs.Add(696030922U, "Tag");
			DirectoryStrings.stringIDs.Add(3607016823U, "MailFlowPartnerInternalMailContentTypeTNEF");
			DirectoryStrings.stringIDs.Add(1793427927U, "SerialNumberMissing");
			DirectoryStrings.stringIDs.Add(2193656120U, "AttributeNameNull");
			DirectoryStrings.stringIDs.Add(1260468432U, "ErrorIsDehydratedSetOnNonTinyTenant");
			DirectoryStrings.stringIDs.Add(4069918469U, "TUIPromptEditingEnabled");
			DirectoryStrings.stringIDs.Add(885421749U, "StarAcceptedDomainCannotBeInitialDomain");
			DirectoryStrings.stringIDs.Add(1860494422U, "LdapFilterErrorNotSupportSingleComp");
			DirectoryStrings.stringIDs.Add(1191236736U, "UseTnef");
			DirectoryStrings.stringIDs.Add(654334258U, "AttachmentFilterEntryInvalid");
			DirectoryStrings.stringIDs.Add(599002007U, "Exchange2013");
			DirectoryStrings.stringIDs.Add(3145869218U, "SendAuthMechanismBasicAuthPlusTls");
			DirectoryStrings.stringIDs.Add(3341243277U, "MoveToDeletedItems");
			DirectoryStrings.stringIDs.Add(2403277311U, "TCP");
			DirectoryStrings.stringIDs.Add(2664643149U, "DocumentMC");
			DirectoryStrings.stringIDs.Add(1531250846U, "ErrorCannotSetWindowsEmailAddress");
			DirectoryStrings.stringIDs.Add(3115737588U, "Msn");
			DirectoryStrings.stringIDs.Add(619725344U, "MessageRateSourceFlagsIPAddress");
			DirectoryStrings.stringIDs.Add(4163650158U, "ErrorTextMessageIncludingAppleAttachment");
			DirectoryStrings.stringIDs.Add(3857647582U, "ForwardCallsToDefaultMailbox");
			DirectoryStrings.stringIDs.Add(3221974997U, "RoleGroupTypeDetails");
			DirectoryStrings.stringIDs.Add(3815678973U, "MailEnabledContactRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(647747116U, "EsnLangEnglish");
			DirectoryStrings.stringIDs.Add(2250496622U, "EsnLangMarathi");
			DirectoryStrings.stringIDs.Add(1092312607U, "SpecifyAnnouncementFileName");
			DirectoryStrings.stringIDs.Add(857277173U, "GroupNamingPolicyCustomAttribute12");
			DirectoryStrings.stringIDs.Add(3759553744U, "SystemAddressListDoesNotExist");
			DirectoryStrings.stringIDs.Add(3499307032U, "DefaultOabName");
			DirectoryStrings.stringIDs.Add(1855185352U, "EsnLangSpanish");
			DirectoryStrings.stringIDs.Add(3227102809U, "FederatedOrganizationIdNoNamespaceAccount");
			DirectoryStrings.stringIDs.Add(1406382714U, "RemoteEquipmentMailboxTypeDetails");
			DirectoryStrings.stringIDs.Add(2654331961U, "SpamFilteringOptionOn");
			DirectoryStrings.stringIDs.Add(1495966060U, "ErrorNoSharedConfigurationInfo");
			DirectoryStrings.stringIDs.Add(3938481035U, "EquipmentMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(375049999U, "ErrorCannotSetMoveToDestinationFolder");
			DirectoryStrings.stringIDs.Add(290262264U, "CapabilityTOUSigned");
			DirectoryStrings.stringIDs.Add(3707194054U, "ServerRoleExtendedRole2");
			DirectoryStrings.stringIDs.Add(3707194053U, "ServerRoleExtendedRole3");
			DirectoryStrings.stringIDs.Add(2283186478U, "PersonalFolder");
			DirectoryStrings.stringIDs.Add(584737882U, "CapabilityNone");
			DirectoryStrings.stringIDs.Add(31546440U, "ErrorEmptyResourceTypeofResourceMailbox");
			DirectoryStrings.stringIDs.Add(2469247251U, "InternalDNSServersNotSet");
			DirectoryStrings.stringIDs.Add(4205089983U, "ExceptionImpersonation");
			DirectoryStrings.stringIDs.Add(3072026616U, "ReceiveAuthMechanismNone");
			DirectoryStrings.stringIDs.Add(1924734199U, "GroupNamingPolicyCustomAttribute9");
			DirectoryStrings.stringIDs.Add(2999125469U, "MailEnabledDynamicDistributionGroupRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(685401583U, "SpamFilteringActionAddXHeader");
			DirectoryStrings.stringIDs.Add(141120823U, "RecentCommands");
			DirectoryStrings.stringIDs.Add(2690725740U, "SecurityPrincipalTypeNone");
			DirectoryStrings.stringIDs.Add(1589279983U, "MailboxMoveStatusNone");
			DirectoryStrings.stringIDs.Add(3976915092U, "LocalForest");
			DirectoryStrings.stringIDs.Add(221683052U, "LegacyMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(1924734194U, "GroupNamingPolicyCustomAttribute2");
			DirectoryStrings.stringIDs.Add(661425765U, "DatabaseMasterTypeUnknown");
			DirectoryStrings.stringIDs.Add(2630084427U, "ConversationHistory");
			DirectoryStrings.stringIDs.Add(2338066360U, "OutboundConnectorTlsSettingsInvalidDomainValidationWithoutTlsDomain");
			DirectoryStrings.stringIDs.Add(282367765U, "WhenMoved");
			DirectoryStrings.stringIDs.Add(1039356237U, "ErrorDuplicateLanguage");
			DirectoryStrings.stringIDs.Add(1268762784U, "ExceptionObjectAlreadyExists");
			DirectoryStrings.stringIDs.Add(908880307U, "EsnLangCzech");
			DirectoryStrings.stringIDs.Add(3859838333U, "ComponentNameInvalid");
			DirectoryStrings.stringIDs.Add(3587044099U, "ErrorAuthMetadataCannotResolveIssuer");
			DirectoryStrings.stringIDs.Add(4137211921U, "GroupNamingPolicyTitle");
			DirectoryStrings.stringIDs.Add(3738926850U, "MailboxMoveStatusSuspended");
			DirectoryStrings.stringIDs.Add(1795765194U, "DomainSecureEnabledWithExternalAuthoritative");
			DirectoryStrings.stringIDs.Add(4014391034U, "BasicAfterTLSWithoutTLS");
			DirectoryStrings.stringIDs.Add(3026477473U, "Private");
			DirectoryStrings.stringIDs.Add(1481245394U, "Mailboxes");
			DirectoryStrings.stringIDs.Add(1548074671U, "ErrorModeratorRequiredForModeration");
			DirectoryStrings.stringIDs.Add(51460946U, "CustomFromAddressRequired");
			DirectoryStrings.stringIDs.Add(1951705699U, "LdapModifyDN");
			DirectoryStrings.stringIDs.Add(2453785463U, "CustomExternalSubjectRequired");
			DirectoryStrings.stringIDs.Add(2119492277U, "ErrorInternalLocationsCountMissMatch");
			DirectoryStrings.stringIDs.Add(2620688997U, "ASOnlyOneAuthenticationMethodAllowed");
			DirectoryStrings.stringIDs.Add(4001967317U, "Tnef");
			DirectoryStrings.stringIDs.Add(2746045715U, "ByteEncoderTypeUseBase64HtmlDetectTextPlain");
			DirectoryStrings.stringIDs.Add(1347571030U, "EsnLangIcelandic");
			DirectoryStrings.stringIDs.Add(2324863376U, "ServerRoleNAT");
			DirectoryStrings.stringIDs.Add(1966081841U, "UniversalDistributionGroupRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(2222835032U, "ErrorReplicationLatency");
			DirectoryStrings.stringIDs.Add(461546579U, "EnabledPartner");
			DirectoryStrings.stringIDs.Add(1942443133U, "OutboundConnectorSmarthostTlsSettingsInvalid");
			DirectoryStrings.stringIDs.Add(1538151754U, "ExternalCompliance");
			DirectoryStrings.stringIDs.Add(1293877238U, "ErrorAuthMetadataNoSigningKey");
			DirectoryStrings.stringIDs.Add(2521212798U, "InboundConnectorIncorrectAllAcceptedDomains");
			DirectoryStrings.stringIDs.Add(1182470434U, "MoveToFolder");
			DirectoryStrings.stringIDs.Add(1421152560U, "Byte");
			DirectoryStrings.stringIDs.Add(2557668279U, "EsnLangCyrillic");
			DirectoryStrings.stringIDs.Add(1711185212U, "CanRunDefaultUpdateState_Invalid");
			DirectoryStrings.stringIDs.Add(3569405894U, "DisabledUserRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(725514504U, "InvalidRecipientType");
			DirectoryStrings.stringIDs.Add(532726102U, "EmailAgeFilterThreeDays");
			DirectoryStrings.stringIDs.Add(3099081087U, "DataMoveReplicationConstraintCISecondCopy");
			DirectoryStrings.stringIDs.Add(1499716658U, "ErrorMissingPrimarySmtp");
			DirectoryStrings.stringIDs.Add(398140363U, "ErrorELCFolderNotSpecified");
			DirectoryStrings.stringIDs.Add(4174419723U, "ErrorCannotHaveMoreThanOneDefaultThrottlingPolicy");
			DirectoryStrings.stringIDs.Add(4070703744U, "ReceiveModeCannotBeZero");
			DirectoryStrings.stringIDs.Add(3248373105U, "OwaDefaultDomainRequiredWhenLogonFormatIsUserName");
			DirectoryStrings.stringIDs.Add(2806561839U, "TLS");
			DirectoryStrings.stringIDs.Add(1432667858U, "LinkedMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(2966158940U, "Tasks");
			DirectoryStrings.stringIDs.Add(1982036425U, "RejectAndQuarantineThreshold");
			DirectoryStrings.stringIDs.Add(1213745183U, "LdapFilterErrorInvalidDecimal");
			DirectoryStrings.stringIDs.Add(2461262221U, "SpamFilteringTestActionAddXHeader");
			DirectoryStrings.stringIDs.Add(1880194541U, "OrganizationCapabilityScaleOut");
			DirectoryStrings.stringIDs.Add(3454173033U, "ConstraintViolationOneOffSupervisionListEntryStringPartIsInvalid");
			DirectoryStrings.stringIDs.Add(104454802U, "DiscoveryMailboxTypeDetails");
			DirectoryStrings.stringIDs.Add(826243425U, "ErrorAdfsTrustedIssuers");
			DirectoryStrings.stringIDs.Add(2167266391U, "DataMoveReplicationConstraintCIAllDatacenters");
			DirectoryStrings.stringIDs.Add(396559062U, "HygieneSuiteStandard");
			DirectoryStrings.stringIDs.Add(291819084U, "EsnLangHindi");
			DirectoryStrings.stringIDs.Add(708782482U, "ExceptionUnableToCreateConnections");
			DirectoryStrings.stringIDs.Add(796260281U, "SecurityPrincipalTypeWellknownSecurityPrincipal");
			DirectoryStrings.stringIDs.Add(22442200U, "Error");
			DirectoryStrings.stringIDs.Add(1999329050U, "ElcScheduleOnWrongServer");
			DirectoryStrings.stringIDs.Add(3694564633U, "SyncIssues");
			DirectoryStrings.stringIDs.Add(3184119847U, "PartiallyApplied");
			DirectoryStrings.stringIDs.Add(361358848U, "PreferredInternetCodePageUndefined");
			DirectoryStrings.stringIDs.Add(3399683424U, "NoRoleEntriesCmdletOrScriptFound");
			DirectoryStrings.stringIDs.Add(2126631961U, "CannotDeserializePartitionHintTooShort");
			DirectoryStrings.stringIDs.Add(2412912437U, "InvalidReceiveAuthModeTLSPassword");
			DirectoryStrings.stringIDs.Add(1924734200U, "GroupNamingPolicyCustomAttribute8");
			DirectoryStrings.stringIDs.Add(401605495U, "EsnLangSwedish");
			DirectoryStrings.stringIDs.Add(2253071944U, "IndustryUtilities");
			DirectoryStrings.stringIDs.Add(3692015522U, "G711");
			DirectoryStrings.stringIDs.Add(3044377029U, "ExternalDNSServersNotSet");
			DirectoryStrings.stringIDs.Add(3168546709U, "Item");
			DirectoryStrings.stringIDs.Add(3325431492U, "LdapFilterErrorUnsupportedAttributeType");
			DirectoryStrings.stringIDs.Add(2932678028U, "ExternalSenderAdminAddressRequired");
			DirectoryStrings.stringIDs.Add(582855761U, "ErrorBadLocalizedFolderName");
			DirectoryStrings.stringIDs.Add(3297182182U, "AutoDatabaseMountDialBestAvailability");
			DirectoryStrings.stringIDs.Add(391848840U, "OrganizationalFolder");
			DirectoryStrings.stringIDs.Add(2944126402U, "SpamFilteringOptionTest");
			DirectoryStrings.stringIDs.Add(56024811U, "LdapFilterErrorInvalidToken");
			DirectoryStrings.stringIDs.Add(2570241570U, "MessageRateSourceFlagsUser");
			DirectoryStrings.stringIDs.Add(1461717404U, "TextEnrichedAndTextAlternative");
			DirectoryStrings.stringIDs.Add(3920082026U, "FederatedOrganizationIdNoFederatedDomains");
			DirectoryStrings.stringIDs.Add(1191186633U, "GroupTypeFlagsUniversal");
			DirectoryStrings.stringIDs.Add(3774252481U, "CustomAlertTextRequired");
			DirectoryStrings.stringIDs.Add(1272682565U, "EsnLangEstonian");
			DirectoryStrings.stringIDs.Add(1502599728U, "Low");
			DirectoryStrings.stringIDs.Add(467677052U, "IndustryPersonalServices");
			DirectoryStrings.stringIDs.Add(3282711248U, "ErrorInvalidPipelineTracingSenderAddress");
			DirectoryStrings.stringIDs.Add(131691172U, "AccessQuarantined");
			DirectoryStrings.stringIDs.Add(3874249800U, "LdapFilterErrorTypeOnlySpaces");
			DirectoryStrings.stringIDs.Add(2878385120U, "UserFilterChoice");
			DirectoryStrings.stringIDs.Add(587065991U, "ErrorRemovePrimaryExternalSMTPAddress");
			DirectoryStrings.stringIDs.Add(62599113U, "GroupNamingPolicyOffice");
			DirectoryStrings.stringIDs.Add(3503686282U, "ErrorHostServerNotSet");
			DirectoryStrings.stringIDs.Add(1998007652U, "BitMaskOrIpAddressMatchMustBeSet");
			DirectoryStrings.stringIDs.Add(621310157U, "OrganizationCapabilityGMGen");
			DirectoryStrings.stringIDs.Add(3723962467U, "ErrorArchiveDatabaseArchiveDomainConflict");
			DirectoryStrings.stringIDs.Add(2472951404U, "ArchiveStateHostedProvisioned");
			DirectoryStrings.stringIDs.Add(614706510U, "InvalidHttpProtocolLogSizeConfiguration");
			DirectoryStrings.stringIDs.Add(3225083443U, "PermanentMservErrorDescription");
			DirectoryStrings.stringIDs.Add(1285289871U, "CustomExternalBodyRequired");
			DirectoryStrings.stringIDs.Add(1415475463U, "LdapFilterErrorUndefinedAttributeType");
			DirectoryStrings.stringIDs.Add(603975640U, "ErrorTextMessageIncludingHtmlBody");
			DirectoryStrings.stringIDs.Add(3773054995U, "WellKnownRecipientTypeResources");
			DirectoryStrings.stringIDs.Add(2097957443U, "PrimaryDefault");
			DirectoryStrings.stringIDs.Add(2943465798U, "MailFlowPartnerInternalMailContentTypeMimeHtmlText");
			DirectoryStrings.stringIDs.Add(2685207586U, "DataMoveReplicationConstraintNone");
			DirectoryStrings.stringIDs.Add(3040710945U, "ErrorAdfsAudienceUris");
			DirectoryStrings.stringIDs.Add(1190928622U, "InvalidAnrFilter");
			DirectoryStrings.stringIDs.Add(117943812U, "AuditLogMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(1849540794U, "WellKnownRecipientTypeNone");
			DirectoryStrings.stringIDs.Add(3296665743U, "EsnLangGujarati");
			DirectoryStrings.stringIDs.Add(672991527U, "DomainStateUnknown");
			DirectoryStrings.stringIDs.Add(687591962U, "IndustryManufacturing");
			DirectoryStrings.stringIDs.Add(345329738U, "IndustryHospitality");
			DirectoryStrings.stringIDs.Add(534671299U, "ErrorAdfsIssuer");
			DirectoryStrings.stringIDs.Add(1058308747U, "EmailAgeFilterOneDay");
			DirectoryStrings.stringIDs.Add(3471478219U, "AllEmailMC");
			DirectoryStrings.stringIDs.Add(1753080574U, "OrgContainerAmbiguousException");
			DirectoryStrings.stringIDs.Add(1439034128U, "GlobalThrottlingPolicyNotFoundException");
			DirectoryStrings.stringIDs.Add(3636659332U, "EsnLangTurkish");
			DirectoryStrings.stringIDs.Add(1642025802U, "SKUCapabilityBPOSSLite");
			DirectoryStrings.stringIDs.Add(2794974035U, "RecipientWriteScopes");
			DirectoryStrings.stringIDs.Add(104189932U, "CalendarAgeFilterThreeMonths");
			DirectoryStrings.stringIDs.Add(2669194754U, "MailboxMoveStatusCompletedWithWarning");
			DirectoryStrings.stringIDs.Add(3674978674U, "GroupNamingPolicyCountryOrRegion");
			DirectoryStrings.stringIDs.Add(2482287296U, "EsnLangFrench");
			DirectoryStrings.stringIDs.Add(2050489682U, "CapabilityExcludedFromBackSync");
			DirectoryStrings.stringIDs.Add(452620031U, "CapabilityBEVDirLockdown");
			DirectoryStrings.stringIDs.Add(4409738U, "ReceiveAuthMechanismBasicAuth");
			DirectoryStrings.stringIDs.Add(2046074250U, "IndustryEducation");
			DirectoryStrings.stringIDs.Add(2536752615U, "NotSpecified");
			DirectoryStrings.stringIDs.Add(3675904764U, "PermanentlyDelete");
			DirectoryStrings.stringIDs.Add(2346580185U, "FederatedIdentityMisconfigured");
			DirectoryStrings.stringIDs.Add(3845937663U, "MountDialOverrideNone");
			DirectoryStrings.stringIDs.Add(3563162252U, "AlwaysUTF8");
			DirectoryStrings.stringIDs.Add(2660137110U, "ExceptionPagedReaderIsSingleUse");
			DirectoryStrings.stringIDs.Add(3323087513U, "InvalidFilterLength");
			DirectoryStrings.stringIDs.Add(3664117547U, "MailboxMoveStatusSynced");
			DirectoryStrings.stringIDs.Add(2422734853U, "SIPSecured");
			DirectoryStrings.stringIDs.Add(630988704U, "ErrorRejectedCookie");
			DirectoryStrings.stringIDs.Add(3800196293U, "ASInvalidProxyASUrlOption");
			DirectoryStrings.stringIDs.Add(407788899U, "ServerRoleSCOM");
			DirectoryStrings.stringIDs.Add(3289792773U, "JournalItemsMC");
			DirectoryStrings.stringIDs.Add(2617145200U, "ErrorEmptySearchProperty");
			DirectoryStrings.stringIDs.Add(3900005531U, "OutboundConnectorIncorrectTransportRuleScopedParameters");
			DirectoryStrings.stringIDs.Add(107906018U, "TeamMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(4272675708U, "CustomRoleDescription_MyMobileInformation");
			DirectoryStrings.stringIDs.Add(920444171U, "ArchiveStateHostedPending");
			DirectoryStrings.stringIDs.Add(2153511661U, "DPCantChangeName");
			DirectoryStrings.stringIDs.Add(2175447826U, "OrganizationCapabilityUMDataStorage");
			DirectoryStrings.stringIDs.Add(2304217557U, "TlsAuthLevelWithRequireTlsDisabled");
			DirectoryStrings.stringIDs.Add(3453679227U, "UndefinedRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(3608358242U, "Upgrade");
			DirectoryStrings.stringIDs.Add(3905558735U, "Global");
			DirectoryStrings.stringIDs.Add(4264103832U, "DeleteMessage");
			DirectoryStrings.stringIDs.Add(4018720312U, "LdapDelete");
			DirectoryStrings.stringIDs.Add(1291341805U, "EsnLangHungarian");
			DirectoryStrings.stringIDs.Add(699909606U, "ErrorAddressAutoCopy");
			DirectoryStrings.stringIDs.Add(2968709845U, "EsnLangLatvian");
			DirectoryStrings.stringIDs.Add(674244647U, "CanRunDefaultUpdateState_NotLocal");
			DirectoryStrings.stringIDs.Add(1855823700U, "Department");
			DirectoryStrings.stringIDs.Add(1123996746U, "SpamFilteringActionJmf");
			DirectoryStrings.stringIDs.Add(1079159280U, "ErrorDDLOperationsError");
			DirectoryStrings.stringIDs.Add(64564864U, "ErrorSharedConfigurationCannotBeEnabled");
			DirectoryStrings.stringIDs.Add(1411554219U, "ErrorMailTipCultureNotSpecified");
			DirectoryStrings.stringIDs.Add(2068838733U, "LdapModify");
			DirectoryStrings.stringIDs.Add(2212942115U, "DataMoveReplicationConstraintSecondDatacenter");
			DirectoryStrings.stringIDs.Add(3501307892U, "CapabilityResourceMailbox");
			DirectoryStrings.stringIDs.Add(2955006930U, "Second");
			DirectoryStrings.stringIDs.Add(451948526U, "InboundConnectorInvalidRestrictDomainsToCertificate");
			DirectoryStrings.stringIDs.Add(97762286U, "GroupNamingPolicyCustomAttribute15");
			DirectoryStrings.stringIDs.Add(1669305113U, "SendAuthMechanismNone");
			DirectoryStrings.stringIDs.Add(2028679986U, "ServicesContainerNotFound");
			DirectoryStrings.stringIDs.Add(368981658U, "MissingDefaultOutboundCallingLineId");
			DirectoryStrings.stringIDs.Add(1638178773U, "GroupTypeFlagsDomainLocal");
			DirectoryStrings.stringIDs.Add(2292597411U, "ErrorCannotAggregateAndLinkMailbox");
			DirectoryStrings.stringIDs.Add(1975373491U, "SyncCommands");
			DirectoryStrings.stringIDs.Add(2584752109U, "PreferredInternetCodePageEsc2022Jp");
			DirectoryStrings.stringIDs.Add(2869997774U, "DirectoryBasedEdgeBlockModeOff");
			DirectoryStrings.stringIDs.Add(3205211544U, "InvalidSourceAddressSetting");
			DirectoryStrings.stringIDs.Add(3459813102U, "ElcContentSettingsDescription");
			DirectoryStrings.stringIDs.Add(3194934827U, "ServerRoleUnifiedMessaging");
			DirectoryStrings.stringIDs.Add(1193235970U, "DataMoveReplicationConstraintCIAllCopies");
			DirectoryStrings.stringIDs.Add(96477845U, "MailTipsAccessLevelLimited");
			DirectoryStrings.stringIDs.Add(4229158936U, "SecondaryMailboxRelationType");
			DirectoryStrings.stringIDs.Add(3828198519U, "Ocs");
			DirectoryStrings.stringIDs.Add(2122644134U, "IndustryOther");
			DirectoryStrings.stringIDs.Add(3032910929U, "ErrorMimeMessageIncludingUuEncodedAttachment");
			DirectoryStrings.stringIDs.Add(1886413222U, "ServerRoleDHCP");
			DirectoryStrings.stringIDs.Add(1924734195U, "GroupNamingPolicyCustomAttribute5");
			DirectoryStrings.stringIDs.Add(102260678U, "EnableNotificationEmail");
			DirectoryStrings.stringIDs.Add(4022404286U, "GroupNamingPolicyCountryCode");
			DirectoryStrings.stringIDs.Add(4204248234U, "MailboxMoveStatusCompleted");
			DirectoryStrings.stringIDs.Add(2831291713U, "IndustryCommunications");
			DirectoryStrings.stringIDs.Add(2559242555U, "LdapFilterErrorNoValidComparison");
			DirectoryStrings.stringIDs.Add(3598244064U, "RssSubscriptions");
			DirectoryStrings.stringIDs.Add(1071018894U, "EsnLangThai");
			DirectoryStrings.stringIDs.Add(2921549042U, "ErrorDDLFilterMissing");
			DirectoryStrings.stringIDs.Add(1447606358U, "ExtendedProtectionNonTlsTerminatingProxyScenarioRequireTls");
			DirectoryStrings.stringIDs.Add(267717298U, "NoResetOrAssignedMvp");
			DirectoryStrings.stringIDs.Add(663506969U, "MountDialOverrideBestEffort");
			DirectoryStrings.stringIDs.Add(2367428005U, "NoComputers");
			DirectoryStrings.stringIDs.Add(665042539U, "RegistryContentTypeException");
			DirectoryStrings.stringIDs.Add(366040629U, "DataMoveReplicationConstraintAllDatacenters");
			DirectoryStrings.stringIDs.Add(2564080149U, "ExceptionObjectNotFound");
			DirectoryStrings.stringIDs.Add(882963645U, "DomainStateCustomProvision");
			DirectoryStrings.stringIDs.Add(3517179940U, "SKUCapabilityBPOSMidSize");
			DirectoryStrings.stringIDs.Add(1117900463U, "LdapFilterErrorUnsupportedOperand");
			DirectoryStrings.stringIDs.Add(483196058U, "DirectoryBasedEdgeBlockModeDefault");
			DirectoryStrings.stringIDs.Add(113073592U, "ErrorWrongTypeParameter");
			DirectoryStrings.stringIDs.Add(2757326190U, "EsnLangCatalan");
			DirectoryStrings.stringIDs.Add(3980183679U, "InvalidSndProtocolLogSizeConfiguration");
			DirectoryStrings.stringIDs.Add(3586160528U, "GroupNamingPolicyCustomAttribute13");
			DirectoryStrings.stringIDs.Add(1960526324U, "ErrorThrottlingPolicyGlobalAndOrganizationScope");
			DirectoryStrings.stringIDs.Add(980672066U, "SMTPAddress");
			DirectoryStrings.stringIDs.Add(3976700013U, "EsnLangPolish");
			DirectoryStrings.stringIDs.Add(1017523965U, "CanEnableLocalCopyState_DatabaseEnabled");
			DirectoryStrings.stringIDs.Add(3315201717U, "EsnLangRomanian");
			DirectoryStrings.stringIDs.Add(1097129869U, "ExternalManagedGroupTypeDetails");
			DirectoryStrings.stringIDs.Add(2773964607U, "DatabaseMasterTypeDag");
			DirectoryStrings.stringIDs.Add(3197896354U, "GroupNamingPolicyExtensionCustomAttribute3");
			DirectoryStrings.stringIDs.Add(3071618850U, "ExchangeConfigurationContainerNotFoundException");
			DirectoryStrings.stringIDs.Add(170342216U, "EsnLangUrdu");
			DirectoryStrings.stringIDs.Add(579329341U, "MservAndMbxExclusive");
			DirectoryStrings.stringIDs.Add(2300412432U, "FirstLast");
			DirectoryStrings.stringIDs.Add(2228665429U, "EsnLangBulgarian");
			DirectoryStrings.stringIDs.Add(1970247521U, "MailEnabledUniversalSecurityGroupRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(3065017355U, "ErrorTimeoutReadingSystemAddressListMemberCount");
			DirectoryStrings.stringIDs.Add(3411768540U, "FaxServerURINoValue");
			DirectoryStrings.stringIDs.Add(1118762177U, "ErrorDefaultThrottlingPolicyNotFound");
			DirectoryStrings.stringIDs.Add(3372089172U, "ErrorRecipientContainerCanNotNull");
			DirectoryStrings.stringIDs.Add(2835967712U, "MoveToArchive");
			DirectoryStrings.stringIDs.Add(667068758U, "ModifySubjectValueNotSet");
			DirectoryStrings.stringIDs.Add(882536335U, "NotLocalMaiboxException");
			DirectoryStrings.stringIDs.Add(3811978523U, "RecipientReadScope");
			DirectoryStrings.stringIDs.Add(1067650092U, "Organizational");
			DirectoryStrings.stringIDs.Add(1818643265U, "SystemAttendantMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(114732651U, "OrganizationCapabilityOABGen");
			DirectoryStrings.stringIDs.Add(2360810543U, "StarOutToDialPlanEnabled");
			DirectoryStrings.stringIDs.Add(3673152204U, "AuthenticationCredentialNotSet");
			DirectoryStrings.stringIDs.Add(1776441609U, "NotifyOutboundSpamRecipientsRequired");
			DirectoryStrings.stringIDs.Add(2241039844U, "JunkEmail");
			DirectoryStrings.stringIDs.Add(2270844793U, "LdapFilterErrorValueOnlySpaces");
			DirectoryStrings.stringIDs.Add(3423767853U, "SipName");
			DirectoryStrings.stringIDs.Add(24965481U, "EsnLangMalayalam");
			DirectoryStrings.stringIDs.Add(2349327181U, "SpamFilteringActionModifySubject");
			DirectoryStrings.stringIDs.Add(1153697179U, "XHeaderValueNotSet");
			DirectoryStrings.stringIDs.Add(3613623199U, "DeletedItems");
			DirectoryStrings.stringIDs.Add(3387472355U, "OrganizationCapabilityUMGrammarReady");
			DirectoryStrings.stringIDs.Add(142823596U, "LastFirst");
			DirectoryStrings.stringIDs.Add(2055652669U, "SendAuthMechanismExchangeServer");
			DirectoryStrings.stringIDs.Add(322963092U, "RemoteTeamMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(1068346025U, "OutOfBudgets");
			DirectoryStrings.stringIDs.Add(3424913979U, "Off");
			DirectoryStrings.stringIDs.Add(3200416695U, "GroupTypeFlagsSecurityEnabled");
			DirectoryStrings.stringIDs.Add(2618688392U, "InvalidCookieException");
			DirectoryStrings.stringIDs.Add(122679092U, "UserLanguageChoice");
			DirectoryStrings.stringIDs.Add(1291237470U, "SpamFilteringTestActionBccMessage");
			DirectoryStrings.stringIDs.Add(1118847720U, "DelayCacheFull");
			DirectoryStrings.stringIDs.Add(1149691394U, "ErrorAutoCopyMessageFormat");
			DirectoryStrings.stringIDs.Add(1173768531U, "Reserved3");
			DirectoryStrings.stringIDs.Add(2523055253U, "HtmlOnly");
			DirectoryStrings.stringIDs.Add(285356425U, "DefaultFolder");
			DirectoryStrings.stringIDs.Add(1487832074U, "PublicFolderMailboxRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(1549653732U, "Mp3");
			DirectoryStrings.stringIDs.Add(2737500906U, "FederatedOrganizationIdNotEnabled");
			DirectoryStrings.stringIDs.Add(3082202919U, "EsnLangVietnamese");
			DirectoryStrings.stringIDs.Add(2532765903U, "AccessGranted");
			DirectoryStrings.stringIDs.Add(4281433724U, "MailboxUserRecipientType");
			DirectoryStrings.stringIDs.Add(1183009861U, "ExceptionNoSchemaContainerObject");
			DirectoryStrings.stringIDs.Add(2078410195U, "TargetDeliveryDomainCannotBeStar");
			DirectoryStrings.stringIDs.Add(2402032744U, "ErrorAuthMetadataCannotResolveServiceName");
			DirectoryStrings.stringIDs.Add(4046275528U, "ByteEncoderTypeUseBase64");
			DirectoryStrings.stringIDs.Add(2114338030U, "BackSyncDataSourceReplicationErrorMessage");
			DirectoryStrings.stringIDs.Add(2327110479U, "EsnLangHebrew");
			DirectoryStrings.stringIDs.Add(2099880135U, "WellKnownRecipientTypeAllRecipients");
			DirectoryStrings.stringIDs.Add(33168083U, "ExceptionCredentialsNotSupportedWithoutDC");
			DirectoryStrings.stringIDs.Add(247236896U, "NoneMailboxRelationType");
			DirectoryStrings.stringIDs.Add(1605633982U, "MailboxUserRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(3918345138U, "SpamFilteringActionDelete");
			DirectoryStrings.stringIDs.Add(1574700905U, "FederatedOrganizationIdNotFound");
			DirectoryStrings.stringIDs.Add(3777672192U, "SKUCapabilityBPOSSArchive");
			DirectoryStrings.stringIDs.Add(1038035039U, "ReceiveAuthMechanismIntegrated");
			DirectoryStrings.stringIDs.Add(1607061032U, "NameLookupEnabled");
			DirectoryStrings.stringIDs.Add(1972085753U, "ForceFilter");
			DirectoryStrings.stringIDs.Add(3515139435U, "OrganizationCapabilityOfficeMessageEncryption");
			DirectoryStrings.stringIDs.Add(906595693U, "PreferredInternetCodePageIso2022Jp");
			DirectoryStrings.stringIDs.Add(1412620754U, "AlternateServiceAccountCredentialIsInvalid");
			DirectoryStrings.stringIDs.Add(1859518684U, "EmailAgeFilterTwoWeeks");
			DirectoryStrings.stringIDs.Add(2080073494U, "DeviceOS");
			DirectoryStrings.stringIDs.Add(1451782196U, "ErrorTenantRelocationsAllowedOnlyForRootOrg");
			DirectoryStrings.stringIDs.Add(3809750167U, "OrganizationCapabilityTenantUpgrade");
			DirectoryStrings.stringIDs.Add(426414486U, "StarTlsDomainCapabilitiesNotAllowed");
			DirectoryStrings.stringIDs.Add(2391327300U, "GroupNamingPolicyExtensionCustomAttribute5");
			DirectoryStrings.stringIDs.Add(1103339046U, "ErrorTimeoutWritingSystemAddressListCache");
			DirectoryStrings.stringIDs.Add(2249628033U, "CannotGetLocalSite");
			DirectoryStrings.stringIDs.Add(3080481085U, "DatabaseCopyAutoActivationPolicyUnrestricted");
			DirectoryStrings.stringIDs.Add(3562221485U, "PrivateComputersOnly");
			DirectoryStrings.stringIDs.Add(887700241U, "Always");
			DirectoryStrings.stringIDs.Add(933193541U, "WellKnownRecipientTypeMailUsers");
			DirectoryStrings.stringIDs.Add(3512186809U, "CannotSetZeroAsEapPriority");
			DirectoryStrings.stringIDs.Add(2442344752U, "RootZone");
			DirectoryStrings.stringIDs.Add(1151884593U, "RenameNotAllowed");
			DirectoryStrings.stringIDs.Add(2846264340U, "Unknown");
			DirectoryStrings.stringIDs.Add(4013633336U, "EsnLangItalian");
			DirectoryStrings.stringIDs.Add(2446612004U, "ErrorDisplayNameInvalid");
			DirectoryStrings.stringIDs.Add(10930364U, "ConstraintViolationNotValidLegacyDN");
			DirectoryStrings.stringIDs.Add(3650953906U, "ReceiveExtendedProtectionPolicyRequire");
			DirectoryStrings.stringIDs.Add(2030161115U, "SpamFilteringOptionOff");
			DirectoryStrings.stringIDs.Add(1656602441U, "ExternallyManaged");
			DirectoryStrings.stringIDs.Add(3909129905U, "RequireTLSWithoutTLS");
			DirectoryStrings.stringIDs.Add(1437476905U, "ErrorCannotParseAuthMetadata");
			DirectoryStrings.stringIDs.Add(2411242862U, "ErrorInvalidActivationPreference");
			DirectoryStrings.stringIDs.Add(1499015349U, "CapabilityFederatedUser");
			DirectoryStrings.stringIDs.Add(992862894U, "EsnLangFilipino");
			DirectoryStrings.stringIDs.Add(2687926967U, "OutboundConnectorUseMXRecordShouldBeFalseIfSmartHostsIsPresent");
			DirectoryStrings.stringIDs.Add(1688256845U, "LdapFilterErrorBracketMismatch");
			DirectoryStrings.stringIDs.Add(843851219U, "SipResourceIdentifierRequiredNotAllowed");
			DirectoryStrings.stringIDs.Add(2178386640U, "XMSWLHeader");
			DirectoryStrings.stringIDs.Add(1536572748U, "ServerRoleCafe");
			DirectoryStrings.stringIDs.Add(3319415544U, "DeleteAndRejectThreshold");
			DirectoryStrings.stringIDs.Add(816661212U, "Policy");
			DirectoryStrings.stringIDs.Add(2870485117U, "CanRunRestoreState_NotLocal");
			DirectoryStrings.stringIDs.Add(2379521528U, "ElcAuditLogPathMissing");
			DirectoryStrings.stringIDs.Add(4221359213U, "ClientCertAuthIgnore");
			DirectoryStrings.stringIDs.Add(1173768532U, "Reserved2");
			DirectoryStrings.stringIDs.Add(3743229054U, "ConfigWriteScopes");
			DirectoryStrings.stringIDs.Add(3856328942U, "DetailsTemplateCorrupted");
			DirectoryStrings.stringIDs.Add(1942592476U, "ClientCertAuthAccepted");
			DirectoryStrings.stringIDs.Add(1778180980U, "ExceptionAdminLimitExceeded");
			DirectoryStrings.stringIDs.Add(970530017U, "DataMoveReplicationConstraintSecondCopy");
			DirectoryStrings.stringIDs.Add(3956092407U, "ReceiveAuthMechanismTls");
			DirectoryStrings.stringIDs.Add(990840756U, "CannotFindTemplateTenant");
			DirectoryStrings.stringIDs.Add(2189879122U, "FailedToReadStoreUserInformation");
			DirectoryStrings.stringIDs.Add(2227674028U, "MicrosoftExchangeRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(1188824751U, "DataMoveReplicationConstraintCINoReplication");
			DirectoryStrings.stringIDs.Add(575137052U, "ErrorTransitionCounterHasZeroCount");
			DirectoryStrings.stringIDs.Add(73047031U, "DeleteAndQuarantineThreshold");
			DirectoryStrings.stringIDs.Add(1642796619U, "IndustryAgriculture");
			DirectoryStrings.stringIDs.Add(3603173284U, "ClientCertAuthRequired");
			DirectoryStrings.stringIDs.Add(3707194057U, "ServerRoleExtendedRole7");
			DirectoryStrings.stringIDs.Add(2804536165U, "SubmissionOverrideListOnWrongServer");
			DirectoryStrings.stringIDs.Add(4170864973U, "EsnLangBasque");
			DirectoryStrings.stringIDs.Add(659240048U, "UserRecipientType");
			DirectoryStrings.stringIDs.Add(363625972U, "MailEnabledUserRecipientType");
			DirectoryStrings.stringIDs.Add(4189167987U, "GroupTypeFlagsGlobal");
			DirectoryStrings.stringIDs.Add(4091901749U, "DataMoveReplicationConstraintCISecondDatacenter");
			DirectoryStrings.stringIDs.Add(4024084584U, "LoadBalanceCannotUseBothInclusionLists");
			DirectoryStrings.stringIDs.Add(2771491650U, "ExchangeMissedcallMC");
			DirectoryStrings.stringIDs.Add(3414623930U, "RequesterNameInvalid");
			DirectoryStrings.stringIDs.Add(3393062226U, "ByteEncoderTypeUseBase64Html7BitTextPlain");
			DirectoryStrings.stringIDs.Add(2666751303U, "SecurityPrincipalTypeComputer");
			DirectoryStrings.stringIDs.Add(3235051081U, "EsnLangAmharic");
			DirectoryStrings.stringIDs.Add(3212819533U, "LimitedMoveOnlyAllowed");
			DirectoryStrings.stringIDs.Add(2828547743U, "ASInvalidAuthenticationOptionsForAccessMethod");
			DirectoryStrings.stringIDs.Add(1241097582U, "NullPasswordEncryptionKey");
			DirectoryStrings.stringIDs.Add(1738880682U, "LinkedUserTypeDetails");
			DirectoryStrings.stringIDs.Add(3409499789U, "AutoDatabaseMountDialLossless");
			DirectoryStrings.stringIDs.Add(3238398976U, "ReceiveAuthMechanismExternalAuthoritative");
			DirectoryStrings.stringIDs.Add(1472430496U, "ErrorTruncationLagTime");
			DirectoryStrings.stringIDs.Add(1996912758U, "ExceptionIdImmutable");
			DirectoryStrings.stringIDs.Add(1638589089U, "ExceptionDefaultScopeAndSearchRoot");
			DirectoryStrings.stringIDs.Add(3667281372U, "ErrorOfferProgramIdMandatoryOnSharedConfig");
			DirectoryStrings.stringIDs.Add(3707194060U, "ServerRoleExtendedRole4");
			DirectoryStrings.stringIDs.Add(3615458703U, "ErrorComment");
			DirectoryStrings.stringIDs.Add(4244443796U, "ErrorReplayLagTime");
			DirectoryStrings.stringIDs.Add(64170653U, "ExLengthOfVersionByteArrayError");
			DirectoryStrings.stringIDs.Add(3109296950U, "LdapAdd");
			DirectoryStrings.stringIDs.Add(1578973890U, "DomainStatePendingActivation");
			DirectoryStrings.stringIDs.Add(3790383196U, "Uninterruptible");
			DirectoryStrings.stringIDs.Add(3789585333U, "ErrorMustBeADRawEntry");
			DirectoryStrings.stringIDs.Add(1414246128U, "None");
			DirectoryStrings.stringIDs.Add(1049375487U, "ErrorBadLocalizedComment");
			DirectoryStrings.stringIDs.Add(1223035494U, "EsnLangSlovak");
			DirectoryStrings.stringIDs.Add(3378717027U, "LdapFilterErrorInvalidBooleanValue");
			DirectoryStrings.stringIDs.Add(1343826401U, "OabVersionsNullException");
			DirectoryStrings.stringIDs.Add(2979702410U, "Inbox");
			DirectoryStrings.stringIDs.Add(137387861U, "ContactRecipientTypeDetails");
			DirectoryStrings.stringIDs.Add(1522344710U, "EsnLangKazakh");
			DirectoryStrings.stringIDs.Add(1662145344U, "DisableFilter");
			DirectoryStrings.stringIDs.Add(3713089550U, "BluetoothHandsfreeOnly");
			DirectoryStrings.stringIDs.Add(3178378607U, "GatewayGuid");
			DirectoryStrings.stringIDs.Add(3927045149U, "CalendarSharingFreeBusyNone");
		}

		// Token: 0x060078B2 RID: 30898 RVA: 0x00195398 File Offset: 0x00193598
		public static LocalizedString ExceptionADWriteDisabled(string process, string forest)
		{
			return new LocalizedString("ExceptionADWriteDisabled", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				process,
				forest
			});
		}

		// Token: 0x17002ADF RID: 10975
		// (get) Token: 0x060078B3 RID: 30899 RVA: 0x001953CB File Offset: 0x001935CB
		public static LocalizedString GroupMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("GroupMailboxRecipientTypeDetails", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078B4 RID: 30900 RVA: 0x001953EC File Offset: 0x001935EC
		public static LocalizedString MobileAdOrphanFound(string id)
		{
			return new LocalizedString("MobileAdOrphanFound", "ExE7B760", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002AE0 RID: 10976
		// (get) Token: 0x060078B5 RID: 30901 RVA: 0x0019541B File Offset: 0x0019361B
		public static LocalizedString InvalidTransportSyncHealthLogSizeConfiguration
		{
			get
			{
				return new LocalizedString("InvalidTransportSyncHealthLogSizeConfiguration", "ExDAB1CE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AE1 RID: 10977
		// (get) Token: 0x060078B6 RID: 30902 RVA: 0x00195439 File Offset: 0x00193639
		public static LocalizedString ReceiveExtendedProtectionPolicyNone
		{
			get
			{
				return new LocalizedString("ReceiveExtendedProtectionPolicyNone", "Ex5F3B5A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AE2 RID: 10978
		// (get) Token: 0x060078B7 RID: 30903 RVA: 0x00195457 File Offset: 0x00193657
		public static LocalizedString OrganizationCapabilityManagement
		{
			get
			{
				return new LocalizedString("OrganizationCapabilityManagement", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AE3 RID: 10979
		// (get) Token: 0x060078B8 RID: 30904 RVA: 0x00195475 File Offset: 0x00193675
		public static LocalizedString EsnLangTamil
		{
			get
			{
				return new LocalizedString("EsnLangTamil", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078B9 RID: 30905 RVA: 0x00195494 File Offset: 0x00193694
		public static LocalizedString ExceptionInvalidOperationOnReadOnlyObject(string operation)
		{
			return new LocalizedString("ExceptionInvalidOperationOnReadOnlyObject", "Ex0006B6", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				operation
			});
		}

		// Token: 0x060078BA RID: 30906 RVA: 0x001954C4 File Offset: 0x001936C4
		public static LocalizedString ErrorUnsafeIdentityFilterNotAllowed(string filter, string orgId)
		{
			return new LocalizedString("ErrorUnsafeIdentityFilterNotAllowed", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				filter,
				orgId
			});
		}

		// Token: 0x17002AE4 RID: 10980
		// (get) Token: 0x060078BB RID: 30907 RVA: 0x001954F7 File Offset: 0x001936F7
		public static LocalizedString LdapFilterErrorInvalidWildCard
		{
			get
			{
				return new LocalizedString("LdapFilterErrorInvalidWildCard", "Ex59E760", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AE5 RID: 10981
		// (get) Token: 0x060078BC RID: 30908 RVA: 0x00195515 File Offset: 0x00193715
		public static LocalizedString Individual
		{
			get
			{
				return new LocalizedString("Individual", "Ex96E414", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AE6 RID: 10982
		// (get) Token: 0x060078BD RID: 30909 RVA: 0x00195533 File Offset: 0x00193733
		public static LocalizedString ExternalRelay
		{
			get
			{
				return new LocalizedString("ExternalRelay", "Ex966870", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078BE RID: 30910 RVA: 0x00195554 File Offset: 0x00193754
		public static LocalizedString ErrorProductFileDirectoryIdenticalWithCopyFileDirectory(string directoryName)
		{
			return new LocalizedString("ErrorProductFileDirectoryIdenticalWithCopyFileDirectory", "Ex8DB0BF", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				directoryName
			});
		}

		// Token: 0x17002AE7 RID: 10983
		// (get) Token: 0x060078BF RID: 30911 RVA: 0x00195583 File Offset: 0x00193783
		public static LocalizedString InvalidTransportSyncDownloadSizeConfiguration
		{
			get
			{
				return new LocalizedString("InvalidTransportSyncDownloadSizeConfiguration", "ExF31F0D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AE8 RID: 10984
		// (get) Token: 0x060078C0 RID: 30912 RVA: 0x001955A1 File Offset: 0x001937A1
		public static LocalizedString MessageRateSourceFlagsAll
		{
			get
			{
				return new LocalizedString("MessageRateSourceFlagsAll", "Ex4D0C10", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078C1 RID: 30913 RVA: 0x001955C0 File Offset: 0x001937C0
		public static LocalizedString ErrorIsServerSuitableMissingDefaultNamingContext(string dcName)
		{
			return new LocalizedString("ErrorIsServerSuitableMissingDefaultNamingContext", "Ex6B1A1F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				dcName
			});
		}

		// Token: 0x17002AE9 RID: 10985
		// (get) Token: 0x060078C2 RID: 30914 RVA: 0x001955EF File Offset: 0x001937EF
		public static LocalizedString SKUCapabilityBPOSSBasic
		{
			get
			{
				return new LocalizedString("SKUCapabilityBPOSSBasic", "Ex42AAA4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078C3 RID: 30915 RVA: 0x00195610 File Offset: 0x00193810
		public static LocalizedString ErrorBothTargetAndSourceForestPopulated(string fqdn1, string fqdn2)
		{
			return new LocalizedString("ErrorBothTargetAndSourceForestPopulated", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				fqdn1,
				fqdn2
			});
		}

		// Token: 0x17002AEA RID: 10986
		// (get) Token: 0x060078C4 RID: 30916 RVA: 0x00195643 File Offset: 0x00193843
		public static LocalizedString IndustryMediaMarketingAdvertising
		{
			get
			{
				return new LocalizedString("IndustryMediaMarketingAdvertising", "Ex44C191", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AEB RID: 10987
		// (get) Token: 0x060078C5 RID: 30917 RVA: 0x00195661 File Offset: 0x00193861
		public static LocalizedString SKUCapabilityUnmanaged
		{
			get
			{
				return new LocalizedString("SKUCapabilityUnmanaged", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AEC RID: 10988
		// (get) Token: 0x060078C6 RID: 30918 RVA: 0x0019567F File Offset: 0x0019387F
		public static LocalizedString BackSyncDataSourceTransientErrorMessage
		{
			get
			{
				return new LocalizedString("BackSyncDataSourceTransientErrorMessage", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AED RID: 10989
		// (get) Token: 0x060078C7 RID: 30919 RVA: 0x0019569D File Offset: 0x0019389D
		public static LocalizedString MailEnabledNonUniversalGroupRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailEnabledNonUniversalGroupRecipientTypeDetails", "Ex876BC8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AEE RID: 10990
		// (get) Token: 0x060078C8 RID: 30920 RVA: 0x001956BB File Offset: 0x001938BB
		public static LocalizedString ADDriverStoreAccessPermanentError
		{
			get
			{
				return new LocalizedString("ADDriverStoreAccessPermanentError", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AEF RID: 10991
		// (get) Token: 0x060078C9 RID: 30921 RVA: 0x001956D9 File Offset: 0x001938D9
		public static LocalizedString DeviceType
		{
			get
			{
				return new LocalizedString("DeviceType", "Ex1B6E63", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078CA RID: 30922 RVA: 0x001956F8 File Offset: 0x001938F8
		public static LocalizedString UnsupportedObjectClass(string objectClass)
		{
			return new LocalizedString("UnsupportedObjectClass", "Ex1D7664", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				objectClass
			});
		}

		// Token: 0x060078CB RID: 30923 RVA: 0x00195728 File Offset: 0x00193928
		public static LocalizedString DefaultAdministrativeGroupNotFoundException(string agName)
		{
			return new LocalizedString("DefaultAdministrativeGroupNotFoundException", "Ex155173", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				agName
			});
		}

		// Token: 0x060078CC RID: 30924 RVA: 0x00195758 File Offset: 0x00193958
		public static LocalizedString PropertyDependencyRequired(string propertyName, string dependantPropertyName)
		{
			return new LocalizedString("PropertyDependencyRequired", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				dependantPropertyName
			});
		}

		// Token: 0x060078CD RID: 30925 RVA: 0x0019578C File Offset: 0x0019398C
		public static LocalizedString ProviderFactoryClassNotFoundLoadException(string providerName, string assemblyPath, string factoryTypeName)
		{
			return new LocalizedString("ProviderFactoryClassNotFoundLoadException", "Ex152E2A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				providerName,
				assemblyPath,
				factoryTypeName
			});
		}

		// Token: 0x17002AF0 RID: 10992
		// (get) Token: 0x060078CE RID: 30926 RVA: 0x001957C3 File Offset: 0x001939C3
		public static LocalizedString EsnLangFarsi
		{
			get
			{
				return new LocalizedString("EsnLangFarsi", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AF1 RID: 10993
		// (get) Token: 0x060078CF RID: 30927 RVA: 0x001957E1 File Offset: 0x001939E1
		public static LocalizedString InvalidTempErrorSetting
		{
			get
			{
				return new LocalizedString("InvalidTempErrorSetting", "ExAB3AD8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078D0 RID: 30928 RVA: 0x00195800 File Offset: 0x00193A00
		public static LocalizedString ConfigurationSettingsRestrictionSummary(string nameMatch, string minVersion, string maxVersion)
		{
			return new LocalizedString("ConfigurationSettingsRestrictionSummary", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				nameMatch,
				minVersion,
				maxVersion
			});
		}

		// Token: 0x17002AF2 RID: 10994
		// (get) Token: 0x060078D1 RID: 30929 RVA: 0x00195837 File Offset: 0x00193A37
		public static LocalizedString ReplicationTypeNone
		{
			get
			{
				return new LocalizedString("ReplicationTypeNone", "Ex3AD385", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AF3 RID: 10995
		// (get) Token: 0x060078D2 RID: 30930 RVA: 0x00195855 File Offset: 0x00193A55
		public static LocalizedString IndustryBusinessServicesConsulting
		{
			get
			{
				return new LocalizedString("IndustryBusinessServicesConsulting", "Ex768107", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078D3 RID: 30931 RVA: 0x00195874 File Offset: 0x00193A74
		public static LocalizedString TenantNotFoundInGlsError(string tenant)
		{
			return new LocalizedString("TenantNotFoundInGlsError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				tenant
			});
		}

		// Token: 0x17002AF4 RID: 10996
		// (get) Token: 0x060078D4 RID: 30932 RVA: 0x001958A3 File Offset: 0x00193AA3
		public static LocalizedString ErrorAdfsConfigFormat
		{
			get
			{
				return new LocalizedString("ErrorAdfsConfigFormat", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AF5 RID: 10997
		// (get) Token: 0x060078D5 RID: 30933 RVA: 0x001958C1 File Offset: 0x00193AC1
		public static LocalizedString Quarantined
		{
			get
			{
				return new LocalizedString("Quarantined", "Ex2E9B51", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078D6 RID: 30934 RVA: 0x001958E0 File Offset: 0x00193AE0
		public static LocalizedString InvalidCallSomeoneScopeSettings(string prop1, string prop2)
		{
			return new LocalizedString("InvalidCallSomeoneScopeSettings", "Ex165C88", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				prop1,
				prop2
			});
		}

		// Token: 0x060078D7 RID: 30935 RVA: 0x00195914 File Offset: 0x00193B14
		public static LocalizedString ConfigurationSettingsNotUnique(string name)
		{
			return new LocalizedString("ConfigurationSettingsNotUnique", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17002AF6 RID: 10998
		// (get) Token: 0x060078D8 RID: 30936 RVA: 0x00195943 File Offset: 0x00193B43
		public static LocalizedString OutboundConnectorSmartHostShouldBePresentIfUseMXRecordFalse
		{
			get
			{
				return new LocalizedString("OutboundConnectorSmartHostShouldBePresentIfUseMXRecordFalse", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AF7 RID: 10999
		// (get) Token: 0x060078D9 RID: 30937 RVA: 0x00195961 File Offset: 0x00193B61
		public static LocalizedString LongRunningCostHandle
		{
			get
			{
				return new LocalizedString("LongRunningCostHandle", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AF8 RID: 11000
		// (get) Token: 0x060078DA RID: 30938 RVA: 0x0019597F File Offset: 0x00193B7F
		public static LocalizedString EsnLangChineseTraditional
		{
			get
			{
				return new LocalizedString("EsnLangChineseTraditional", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AF9 RID: 11001
		// (get) Token: 0x060078DB RID: 30939 RVA: 0x0019599D File Offset: 0x00193B9D
		public static LocalizedString IndustryTransportation
		{
			get
			{
				return new LocalizedString("IndustryTransportation", "Ex0407D8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AFA RID: 11002
		// (get) Token: 0x060078DC RID: 30940 RVA: 0x001959BB File Offset: 0x00193BBB
		public static LocalizedString Silent
		{
			get
			{
				return new LocalizedString("Silent", "ExBA24CB", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AFB RID: 11003
		// (get) Token: 0x060078DD RID: 30941 RVA: 0x001959D9 File Offset: 0x00193BD9
		public static LocalizedString AlternateServiceAccountCredentialQualifiedUserNameWrongFormat
		{
			get
			{
				return new LocalizedString("AlternateServiceAccountCredentialQualifiedUserNameWrongFormat", "Ex5F12C0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078DE RID: 30942 RVA: 0x001959F8 File Offset: 0x00193BF8
		public static LocalizedString KpkUseProblem(string propertyName)
		{
			return new LocalizedString("KpkUseProblem", "Ex81BA56", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x17002AFC RID: 11004
		// (get) Token: 0x060078DF RID: 30943 RVA: 0x00195A27 File Offset: 0x00193C27
		public static LocalizedString InvalidBannerSetting
		{
			get
			{
				return new LocalizedString("InvalidBannerSetting", "Ex287F11", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AFD RID: 11005
		// (get) Token: 0x060078E0 RID: 30944 RVA: 0x00195A45 File Offset: 0x00193C45
		public static LocalizedString GroupNamingPolicyCustomAttribute4
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute4", "Ex28B52D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AFE RID: 11006
		// (get) Token: 0x060078E1 RID: 30945 RVA: 0x00195A63 File Offset: 0x00193C63
		public static LocalizedString InboundConnectorIncorrectCloudServicesMailEnabled
		{
			get
			{
				return new LocalizedString("InboundConnectorIncorrectCloudServicesMailEnabled", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002AFF RID: 11007
		// (get) Token: 0x060078E2 RID: 30946 RVA: 0x00195A81 File Offset: 0x00193C81
		public static LocalizedString LdapFilterErrorAnrIsNotSupported
		{
			get
			{
				return new LocalizedString("LdapFilterErrorAnrIsNotSupported", "Ex6BF14B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B00 RID: 11008
		// (get) Token: 0x060078E3 RID: 30947 RVA: 0x00195A9F File Offset: 0x00193C9F
		public static LocalizedString E164
		{
			get
			{
				return new LocalizedString("E164", "ExA4442B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078E4 RID: 30948 RVA: 0x00195AC0 File Offset: 0x00193CC0
		public static LocalizedString ForwardingSmtpAddressNotValidSmtpAddress(object address)
		{
			return new LocalizedString("ForwardingSmtpAddressNotValidSmtpAddress", "Ex2E510E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x060078E5 RID: 30949 RVA: 0x00195AF0 File Offset: 0x00193CF0
		public static LocalizedString ConfigurationSettingsGroupNotFound(string name)
		{
			return new LocalizedString("ConfigurationSettingsGroupNotFound", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17002B01 RID: 11009
		// (get) Token: 0x060078E6 RID: 30950 RVA: 0x00195B1F File Offset: 0x00193D1F
		public static LocalizedString ErrorAuthMetaDataContentEmpty
		{
			get
			{
				return new LocalizedString("ErrorAuthMetaDataContentEmpty", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B02 RID: 11010
		// (get) Token: 0x060078E7 RID: 30951 RVA: 0x00195B3D File Offset: 0x00193D3D
		public static LocalizedString MailEnabledContactRecipientType
		{
			get
			{
				return new LocalizedString("MailEnabledContactRecipientType", "Ex27C052", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078E8 RID: 30952 RVA: 0x00195B5C File Offset: 0x00193D5C
		public static LocalizedString MoreThanOneRecipientWithNetId(string netId)
		{
			return new LocalizedString("MoreThanOneRecipientWithNetId", "ExCB5C82", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				netId
			});
		}

		// Token: 0x17002B03 RID: 11011
		// (get) Token: 0x060078E9 RID: 30953 RVA: 0x00195B8B File Offset: 0x00193D8B
		public static LocalizedString MailEnabledUniversalDistributionGroupRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailEnabledUniversalDistributionGroupRecipientTypeDetails", "Ex7C8E9D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B04 RID: 11012
		// (get) Token: 0x060078EA RID: 30954 RVA: 0x00195BA9 File Offset: 0x00193DA9
		public static LocalizedString SendAuthMechanismExternalAuthoritative
		{
			get
			{
				return new LocalizedString("SendAuthMechanismExternalAuthoritative", "Ex23FFEF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B05 RID: 11013
		// (get) Token: 0x060078EB RID: 30955 RVA: 0x00195BC7 File Offset: 0x00193DC7
		public static LocalizedString InboundConnectorRequiredTlsSettingsInvalid
		{
			get
			{
				return new LocalizedString("InboundConnectorRequiredTlsSettingsInvalid", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B06 RID: 11014
		// (get) Token: 0x060078EC RID: 30956 RVA: 0x00195BE5 File Offset: 0x00193DE5
		public static LocalizedString GroupNamingPolicyCustomAttribute1
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute1", "Ex71E24D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B07 RID: 11015
		// (get) Token: 0x060078ED RID: 30957 RVA: 0x00195C03 File Offset: 0x00193E03
		public static LocalizedString Dual
		{
			get
			{
				return new LocalizedString("Dual", "Ex21FA19", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B08 RID: 11016
		// (get) Token: 0x060078EE RID: 30958 RVA: 0x00195C21 File Offset: 0x00193E21
		public static LocalizedString DatabaseCopyAutoActivationPolicyIntrasiteOnly
		{
			get
			{
				return new LocalizedString("DatabaseCopyAutoActivationPolicyIntrasiteOnly", "Ex986029", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078EF RID: 30959 RVA: 0x00195C40 File Offset: 0x00193E40
		public static LocalizedString CannotResolvePartitionFqdnError(string fqdn)
		{
			return new LocalizedString("CannotResolvePartitionFqdnError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				fqdn
			});
		}

		// Token: 0x17002B09 RID: 11017
		// (get) Token: 0x060078F0 RID: 30960 RVA: 0x00195C6F File Offset: 0x00193E6F
		public static LocalizedString Never
		{
			get
			{
				return new LocalizedString("Never", "Ex5316EA", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B0A RID: 11018
		// (get) Token: 0x060078F1 RID: 30961 RVA: 0x00195C8D File Offset: 0x00193E8D
		public static LocalizedString ByteEncoderTypeUndefined
		{
			get
			{
				return new LocalizedString("ByteEncoderTypeUndefined", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B0B RID: 11019
		// (get) Token: 0x060078F2 RID: 30962 RVA: 0x00195CAB File Offset: 0x00193EAB
		public static LocalizedString InvalidRcvProtocolLogSizeConfiguration
		{
			get
			{
				return new LocalizedString("InvalidRcvProtocolLogSizeConfiguration", "Ex24493B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B0C RID: 11020
		// (get) Token: 0x060078F3 RID: 30963 RVA: 0x00195CC9 File Offset: 0x00193EC9
		public static LocalizedString GetRootDseRequiresDomainController
		{
			get
			{
				return new LocalizedString("GetRootDseRequiresDomainController", "ExDC9BB2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078F4 RID: 30964 RVA: 0x00195CE8 File Offset: 0x00193EE8
		public static LocalizedString ErrorPublicFolderReferralConflict(string entry1, string entry2)
		{
			return new LocalizedString("ErrorPublicFolderReferralConflict", "Ex0A81BB", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				entry1,
				entry2
			});
		}

		// Token: 0x060078F5 RID: 30965 RVA: 0x00195D1C File Offset: 0x00193F1C
		public static LocalizedString ExceptionADTopologyCreationTimeout(int timeoutSeconds)
		{
			return new LocalizedString("ExceptionADTopologyCreationTimeout", "Ex2BC3C3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				timeoutSeconds
			});
		}

		// Token: 0x17002B0D RID: 11021
		// (get) Token: 0x060078F6 RID: 30966 RVA: 0x00195D50 File Offset: 0x00193F50
		public static LocalizedString InheritFromDialPlan
		{
			get
			{
				return new LocalizedString("InheritFromDialPlan", "Ex03EEAC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078F7 RID: 30967 RVA: 0x00195D70 File Offset: 0x00193F70
		public static LocalizedString ExceptionADOperationFailedEntryAlreadyExist(string server, string dn)
		{
			return new LocalizedString("ExceptionADOperationFailedEntryAlreadyExist", "Ex905F80", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				dn
			});
		}

		// Token: 0x17002B0E RID: 11022
		// (get) Token: 0x060078F8 RID: 30968 RVA: 0x00195DA3 File Offset: 0x00193FA3
		public static LocalizedString OrganizationCapabilityMessageTracking
		{
			get
			{
				return new LocalizedString("OrganizationCapabilityMessageTracking", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B0F RID: 11023
		// (get) Token: 0x060078F9 RID: 30969 RVA: 0x00195DC1 File Offset: 0x00193FC1
		public static LocalizedString InboundConnectorInvalidTlsSenderCertificateName
		{
			get
			{
				return new LocalizedString("InboundConnectorInvalidTlsSenderCertificateName", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078FA RID: 30970 RVA: 0x00195DE0 File Offset: 0x00193FE0
		public static LocalizedString ADTopologyEndpointNotFoundException(string url)
		{
			return new LocalizedString("ADTopologyEndpointNotFoundException", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x17002B10 RID: 11024
		// (get) Token: 0x060078FB RID: 30971 RVA: 0x00195E0F File Offset: 0x0019400F
		public static LocalizedString SoftDelete
		{
			get
			{
				return new LocalizedString("SoftDelete", "ExF5005A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078FC RID: 30972 RVA: 0x00195E30 File Offset: 0x00194030
		public static LocalizedString AsyncTimeout(int timeoutSeconds)
		{
			return new LocalizedString("AsyncTimeout", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				timeoutSeconds
			});
		}

		// Token: 0x17002B11 RID: 11025
		// (get) Token: 0x060078FD RID: 30973 RVA: 0x00195E64 File Offset: 0x00194064
		public static LocalizedString OrganizationCapabilityUMGrammar
		{
			get
			{
				return new LocalizedString("OrganizationCapabilityUMGrammar", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B12 RID: 11026
		// (get) Token: 0x060078FE RID: 30974 RVA: 0x00195E82 File Offset: 0x00194082
		public static LocalizedString Allow
		{
			get
			{
				return new LocalizedString("Allow", "Ex47E7DF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060078FF RID: 30975 RVA: 0x00195EA0 File Offset: 0x001940A0
		public static LocalizedString ErrorNonUniqueExchangeGuid(string guidString)
		{
			return new LocalizedString("ErrorNonUniqueExchangeGuid", "Ex1D084C", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				guidString
			});
		}

		// Token: 0x17002B13 RID: 11027
		// (get) Token: 0x06007900 RID: 30976 RVA: 0x00195ECF File Offset: 0x001940CF
		public static LocalizedString DomainNameIsNull
		{
			get
			{
				return new LocalizedString("DomainNameIsNull", "Ex142269", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B14 RID: 11028
		// (get) Token: 0x06007901 RID: 30977 RVA: 0x00195EED File Offset: 0x001940ED
		public static LocalizedString PromptForAlias
		{
			get
			{
				return new LocalizedString("PromptForAlias", "ExE2E4E4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007902 RID: 30978 RVA: 0x00195F0C File Offset: 0x0019410C
		public static LocalizedString InvalidSyncObjectId(string idValue)
		{
			return new LocalizedString("InvalidSyncObjectId", "ExBBC3F0", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				idValue
			});
		}

		// Token: 0x06007903 RID: 30979 RVA: 0x00195F3C File Offset: 0x0019413C
		public static LocalizedString AggregatedSessionCannotMakeADChanges(string attribute)
		{
			return new LocalizedString("AggregatedSessionCannotMakeADChanges", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				attribute
			});
		}

		// Token: 0x17002B15 RID: 11029
		// (get) Token: 0x06007904 RID: 30980 RVA: 0x00195F6B File Offset: 0x0019416B
		public static LocalizedString ErrorSystemAddressListInWrongContainer
		{
			get
			{
				return new LocalizedString("ErrorSystemAddressListInWrongContainer", "Ex997361", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B16 RID: 11030
		// (get) Token: 0x06007905 RID: 30981 RVA: 0x00195F89 File Offset: 0x00194189
		public static LocalizedString ExceptionUnableToDisableAdminTopologyMode
		{
			get
			{
				return new LocalizedString("ExceptionUnableToDisableAdminTopologyMode", "Ex8DFF95", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B17 RID: 11031
		// (get) Token: 0x06007906 RID: 30982 RVA: 0x00195FA7 File Offset: 0x001941A7
		public static LocalizedString Secured
		{
			get
			{
				return new LocalizedString("Secured", "ExEF4E06", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B18 RID: 11032
		// (get) Token: 0x06007907 RID: 30983 RVA: 0x00195FC5 File Offset: 0x001941C5
		public static LocalizedString ExternalAndAuthSet
		{
			get
			{
				return new LocalizedString("ExternalAndAuthSet", "ExC4D34A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B19 RID: 11033
		// (get) Token: 0x06007908 RID: 30984 RVA: 0x00195FE3 File Offset: 0x001941E3
		public static LocalizedString EsnLangJapanese
		{
			get
			{
				return new LocalizedString("EsnLangJapanese", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007909 RID: 30985 RVA: 0x00196004 File Offset: 0x00194204
		public static LocalizedString ErrorInvalidRemoteRecipientType(string value)
		{
			return new LocalizedString("ErrorInvalidRemoteRecipientType", "Ex82B8CE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17002B1A RID: 11034
		// (get) Token: 0x0600790A RID: 30986 RVA: 0x00196033 File Offset: 0x00194233
		public static LocalizedString EsnLangPortuguesePortugal
		{
			get
			{
				return new LocalizedString("EsnLangPortuguesePortugal", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B1B RID: 11035
		// (get) Token: 0x0600790B RID: 30987 RVA: 0x00196051 File Offset: 0x00194251
		public static LocalizedString EsnLangFinnish
		{
			get
			{
				return new LocalizedString("EsnLangFinnish", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B1C RID: 11036
		// (get) Token: 0x0600790C RID: 30988 RVA: 0x0019606F File Offset: 0x0019426F
		public static LocalizedString ExceptionOwaCannotSetPropertyOnVirtualDirectoryOtherThanExchweb
		{
			get
			{
				return new LocalizedString("ExceptionOwaCannotSetPropertyOnVirtualDirectoryOtherThanExchweb", "ExF78B27", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600790D RID: 30989 RVA: 0x00196090 File Offset: 0x00194290
		public static LocalizedString TenantIsRelocatedException(string dn)
		{
			return new LocalizedString("TenantIsRelocatedException", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				dn
			});
		}

		// Token: 0x17002B1D RID: 11037
		// (get) Token: 0x0600790E RID: 30990 RVA: 0x001960BF File Offset: 0x001942BF
		public static LocalizedString WhenDelivered
		{
			get
			{
				return new LocalizedString("WhenDelivered", "Ex981FFA", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B1E RID: 11038
		// (get) Token: 0x0600790F RID: 30991 RVA: 0x001960DD File Offset: 0x001942DD
		public static LocalizedString DomainStatePendingRelease
		{
			get
			{
				return new LocalizedString("DomainStatePendingRelease", "Ex9BFAF1", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B1F RID: 11039
		// (get) Token: 0x06007910 RID: 30992 RVA: 0x001960FB File Offset: 0x001942FB
		public static LocalizedString GroupNamingPolicyExtensionCustomAttribute2
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyExtensionCustomAttribute2", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B20 RID: 11040
		// (get) Token: 0x06007911 RID: 30993 RVA: 0x00196119 File Offset: 0x00194319
		public static LocalizedString AutoGroup
		{
			get
			{
				return new LocalizedString("AutoGroup", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B21 RID: 11041
		// (get) Token: 0x06007912 RID: 30994 RVA: 0x00196137 File Offset: 0x00194337
		public static LocalizedString ErrorStartDateExpiration
		{
			get
			{
				return new LocalizedString("ErrorStartDateExpiration", "Ex131185", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007913 RID: 30995 RVA: 0x00196158 File Offset: 0x00194358
		public static LocalizedString ErrorSingletonMailboxLocationType(string mailboxLocationType)
		{
			return new LocalizedString("ErrorSingletonMailboxLocationType", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				mailboxLocationType
			});
		}

		// Token: 0x17002B22 RID: 11042
		// (get) Token: 0x06007914 RID: 30996 RVA: 0x00196187 File Offset: 0x00194387
		public static LocalizedString MailboxMoveStatusQueued
		{
			get
			{
				return new LocalizedString("MailboxMoveStatusQueued", "Ex15556A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B23 RID: 11043
		// (get) Token: 0x06007915 RID: 30997 RVA: 0x001961A5 File Offset: 0x001943A5
		public static LocalizedString Minute
		{
			get
			{
				return new LocalizedString("Minute", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B24 RID: 11044
		// (get) Token: 0x06007916 RID: 30998 RVA: 0x001961C3 File Offset: 0x001943C3
		public static LocalizedString SentItems
		{
			get
			{
				return new LocalizedString("SentItems", "Ex508526", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B25 RID: 11045
		// (get) Token: 0x06007917 RID: 30999 RVA: 0x001961E1 File Offset: 0x001943E1
		public static LocalizedString ExchangeVoicemailMC
		{
			get
			{
				return new LocalizedString("ExchangeVoicemailMC", "ExAA11CF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007918 RID: 31000 RVA: 0x00196200 File Offset: 0x00194400
		public static LocalizedString DuplicateHolidaysError(string s)
		{
			return new LocalizedString("DuplicateHolidaysError", "Ex3C7541", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x06007919 RID: 31001 RVA: 0x00196230 File Offset: 0x00194430
		public static LocalizedString UnknownAccountForest(string forest)
		{
			return new LocalizedString("UnknownAccountForest", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				forest
			});
		}

		// Token: 0x0600791A RID: 31002 RVA: 0x00196260 File Offset: 0x00194460
		public static LocalizedString ExchangeUpgradeBucketInvalidVersionFormat(string version)
		{
			return new LocalizedString("ExchangeUpgradeBucketInvalidVersionFormat", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x0600791B RID: 31003 RVA: 0x00196290 File Offset: 0x00194490
		public static LocalizedString InvalidControlAttributeForTemplateType(string controlType, string pageName, int controlPosition, string attributeName, string template)
		{
			return new LocalizedString("InvalidControlAttributeForTemplateType", "Ex10710B", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				controlType,
				pageName,
				controlPosition,
				attributeName,
				template
			});
		}

		// Token: 0x17002B26 RID: 11046
		// (get) Token: 0x0600791C RID: 31004 RVA: 0x001962D5 File Offset: 0x001944D5
		public static LocalizedString AppliedInFull
		{
			get
			{
				return new LocalizedString("AppliedInFull", "ExD314FB", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B27 RID: 11047
		// (get) Token: 0x0600791D RID: 31005 RVA: 0x001962F3 File Offset: 0x001944F3
		public static LocalizedString NoAddressSpaces
		{
			get
			{
				return new LocalizedString("NoAddressSpaces", "Ex95B78A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B28 RID: 11048
		// (get) Token: 0x0600791E RID: 31006 RVA: 0x00196311 File Offset: 0x00194511
		public static LocalizedString SKUCapabilityEOPStandardAddOn
		{
			get
			{
				return new LocalizedString("SKUCapabilityEOPStandardAddOn", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600791F RID: 31007 RVA: 0x00196330 File Offset: 0x00194530
		public static LocalizedString ExceptionUnsupportedFilter(string filterType)
		{
			return new LocalizedString("ExceptionUnsupportedFilter", "Ex13B3D4", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				filterType
			});
		}

		// Token: 0x17002B29 RID: 11049
		// (get) Token: 0x06007920 RID: 31008 RVA: 0x0019635F File Offset: 0x0019455F
		public static LocalizedString IndustryNonProfit
		{
			get
			{
				return new LocalizedString("IndustryNonProfit", "Ex0BDDA6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B2A RID: 11050
		// (get) Token: 0x06007921 RID: 31009 RVA: 0x0019637D File Offset: 0x0019457D
		public static LocalizedString EsnLangDefault
		{
			get
			{
				return new LocalizedString("EsnLangDefault", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B2B RID: 11051
		// (get) Token: 0x06007922 RID: 31010 RVA: 0x0019639B File Offset: 0x0019459B
		public static LocalizedString SpecifyCustomGreetingFileName
		{
			get
			{
				return new LocalizedString("SpecifyCustomGreetingFileName", "ExA4123E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B2C RID: 11052
		// (get) Token: 0x06007923 RID: 31011 RVA: 0x001963B9 File Offset: 0x001945B9
		public static LocalizedString EsnLangSlovenian
		{
			get
			{
				return new LocalizedString("EsnLangSlovenian", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B2D RID: 11053
		// (get) Token: 0x06007924 RID: 31012 RVA: 0x001963D7 File Offset: 0x001945D7
		public static LocalizedString TelExtn
		{
			get
			{
				return new LocalizedString("TelExtn", "Ex28FC3B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007925 RID: 31013 RVA: 0x001963F8 File Offset: 0x001945F8
		public static LocalizedString ErrorNonUniqueDomainAccount(string domainName, string accountName)
		{
			return new LocalizedString("ErrorNonUniqueDomainAccount", "ExF77354", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				domainName,
				accountName
			});
		}

		// Token: 0x17002B2E RID: 11054
		// (get) Token: 0x06007926 RID: 31014 RVA: 0x0019642B File Offset: 0x0019462B
		public static LocalizedString LdapFilterErrorInvalidGtLtOperand
		{
			get
			{
				return new LocalizedString("LdapFilterErrorInvalidGtLtOperand", "Ex634ACA", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B2F RID: 11055
		// (get) Token: 0x06007927 RID: 31015 RVA: 0x00196449 File Offset: 0x00194649
		public static LocalizedString SystemMailboxRecipientType
		{
			get
			{
				return new LocalizedString("SystemMailboxRecipientType", "ExE85A1A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007928 RID: 31016 RVA: 0x00196468 File Offset: 0x00194668
		public static LocalizedString PilotingOrganization_Error(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("PilotingOrganization_Error", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x17002B30 RID: 11056
		// (get) Token: 0x06007929 RID: 31017 RVA: 0x001964A3 File Offset: 0x001946A3
		public static LocalizedString ReplicationTypeRemote
		{
			get
			{
				return new LocalizedString("ReplicationTypeRemote", "Ex186CD3", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B31 RID: 11057
		// (get) Token: 0x0600792A RID: 31018 RVA: 0x001964C1 File Offset: 0x001946C1
		public static LocalizedString Enterprise
		{
			get
			{
				return new LocalizedString("Enterprise", "Ex9EE827", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B32 RID: 11058
		// (get) Token: 0x0600792B RID: 31019 RVA: 0x001964DF File Offset: 0x001946DF
		public static LocalizedString Gsm
		{
			get
			{
				return new LocalizedString("Gsm", "Ex0A6BEC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B33 RID: 11059
		// (get) Token: 0x0600792C RID: 31020 RVA: 0x001964FD File Offset: 0x001946FD
		public static LocalizedString Journal
		{
			get
			{
				return new LocalizedString("Journal", "Ex205EC8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B34 RID: 11060
		// (get) Token: 0x0600792D RID: 31021 RVA: 0x0019651B File Offset: 0x0019471B
		public static LocalizedString SpamFilteringTestActionNone
		{
			get
			{
				return new LocalizedString("SpamFilteringTestActionNone", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600792E RID: 31022 RVA: 0x0019653C File Offset: 0x0019473C
		public static LocalizedString ConfigScopeMustBeEmpty(ConfigWriteScopeType scopeType)
		{
			return new LocalizedString("ConfigScopeMustBeEmpty", "Ex69EE6C", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scopeType
			});
		}

		// Token: 0x0600792F RID: 31023 RVA: 0x00196570 File Offset: 0x00194770
		public static LocalizedString ErrorDuplicateManagedFolderAddition(string elcFolderName)
		{
			return new LocalizedString("ErrorDuplicateManagedFolderAddition", "Ex09A253", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				elcFolderName
			});
		}

		// Token: 0x06007930 RID: 31024 RVA: 0x001965A0 File Offset: 0x001947A0
		public static LocalizedString ErrorInvalidConfigScope(string configScope)
		{
			return new LocalizedString("ErrorInvalidConfigScope", "ExEB518A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				configScope
			});
		}

		// Token: 0x17002B35 RID: 11061
		// (get) Token: 0x06007931 RID: 31025 RVA: 0x001965CF File Offset: 0x001947CF
		public static LocalizedString CustomRoleDescription_MyPersonalInformation
		{
			get
			{
				return new LocalizedString("CustomRoleDescription_MyPersonalInformation", "Ex5E8698", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007932 RID: 31026 RVA: 0x001965F0 File Offset: 0x001947F0
		public static LocalizedString EmailAddressPolicyPriorityLowestFormatError(string value)
		{
			return new LocalizedString("EmailAddressPolicyPriorityLowestFormatError", "Ex8BDEA1", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17002B36 RID: 11062
		// (get) Token: 0x06007933 RID: 31027 RVA: 0x0019661F File Offset: 0x0019481F
		public static LocalizedString MailboxMoveStatusAutoSuspended
		{
			get
			{
				return new LocalizedString("MailboxMoveStatusAutoSuspended", "Ex5C6755", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B37 RID: 11063
		// (get) Token: 0x06007934 RID: 31028 RVA: 0x0019663D File Offset: 0x0019483D
		public static LocalizedString Any
		{
			get
			{
				return new LocalizedString("Any", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B38 RID: 11064
		// (get) Token: 0x06007935 RID: 31029 RVA: 0x0019665B File Offset: 0x0019485B
		public static LocalizedString Location
		{
			get
			{
				return new LocalizedString("Location", "Ex0DD32B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B39 RID: 11065
		// (get) Token: 0x06007936 RID: 31030 RVA: 0x00196679 File Offset: 0x00194879
		public static LocalizedString ExternalTrust
		{
			get
			{
				return new LocalizedString("ExternalTrust", "Ex483E87", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B3A RID: 11066
		// (get) Token: 0x06007937 RID: 31031 RVA: 0x00196697 File Offset: 0x00194897
		public static LocalizedString IndustryPrintingPublishing
		{
			get
			{
				return new LocalizedString("IndustryPrintingPublishing", "ExA934A7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B3B RID: 11067
		// (get) Token: 0x06007938 RID: 31032 RVA: 0x001966B5 File Offset: 0x001948B5
		public static LocalizedString AllComputers
		{
			get
			{
				return new LocalizedString("AllComputers", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B3C RID: 11068
		// (get) Token: 0x06007939 RID: 31033 RVA: 0x001966D3 File Offset: 0x001948D3
		public static LocalizedString ExceptionRusNotFound
		{
			get
			{
				return new LocalizedString("ExceptionRusNotFound", "Ex518DD0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B3D RID: 11069
		// (get) Token: 0x0600793A RID: 31034 RVA: 0x001966F1 File Offset: 0x001948F1
		public static LocalizedString GroupNamingPolicyCity
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCity", "Ex78CA26", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B3E RID: 11070
		// (get) Token: 0x0600793B RID: 31035 RVA: 0x0019670F File Offset: 0x0019490F
		public static LocalizedString NoPagesSpecified
		{
			get
			{
				return new LocalizedString("NoPagesSpecified", "ExAE6AF7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B3F RID: 11071
		// (get) Token: 0x0600793C RID: 31036 RVA: 0x0019672D File Offset: 0x0019492D
		public static LocalizedString PublicDatabaseRecipientType
		{
			get
			{
				return new LocalizedString("PublicDatabaseRecipientType", "Ex493E24", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600793D RID: 31037 RVA: 0x0019674C File Offset: 0x0019494C
		public static LocalizedString InvalidSubnetNameFormat(string value)
		{
			return new LocalizedString("InvalidSubnetNameFormat", "Ex130E42", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17002B40 RID: 11072
		// (get) Token: 0x0600793E RID: 31038 RVA: 0x0019677B File Offset: 0x0019497B
		public static LocalizedString CanEnableLocalCopyState_CanBeEnabled
		{
			get
			{
				return new LocalizedString("CanEnableLocalCopyState_CanBeEnabled", "Ex1D0587", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600793F RID: 31039 RVA: 0x0019679C File Offset: 0x0019499C
		public static LocalizedString ErrorServerRoleNotSupported(string id)
		{
			return new LocalizedString("ErrorServerRoleNotSupported", "ExB932D0", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06007940 RID: 31040 RVA: 0x001967CC File Offset: 0x001949CC
		public static LocalizedString ServiceInstanceContainerNotFoundException(string serviceInstance)
		{
			return new LocalizedString("ServiceInstanceContainerNotFoundException", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				serviceInstance
			});
		}

		// Token: 0x06007941 RID: 31041 RVA: 0x001967FC File Offset: 0x001949FC
		public static LocalizedString InvalidAttachmentFilterRegex(string fileSpec)
		{
			return new LocalizedString("InvalidAttachmentFilterRegex", "Ex93E816", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				fileSpec
			});
		}

		// Token: 0x17002B41 RID: 11073
		// (get) Token: 0x06007942 RID: 31042 RVA: 0x0019682B File Offset: 0x00194A2B
		public static LocalizedString RedirectToRecipientsNotSet
		{
			get
			{
				return new LocalizedString("RedirectToRecipientsNotSet", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B42 RID: 11074
		// (get) Token: 0x06007943 RID: 31043 RVA: 0x00196849 File Offset: 0x00194A49
		public static LocalizedString InfoAnnouncementEnabled
		{
			get
			{
				return new LocalizedString("InfoAnnouncementEnabled", "ExA5E081", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007944 RID: 31044 RVA: 0x00196868 File Offset: 0x00194A68
		public static LocalizedString ErrorAdfsAudienceUriFormat(string audienceUriString)
		{
			return new LocalizedString("ErrorAdfsAudienceUriFormat", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				audienceUriString
			});
		}

		// Token: 0x17002B43 RID: 11075
		// (get) Token: 0x06007945 RID: 31045 RVA: 0x00196897 File Offset: 0x00194A97
		public static LocalizedString ConfigurationSettingsADConfigDriverError
		{
			get
			{
				return new LocalizedString("ConfigurationSettingsADConfigDriverError", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B44 RID: 11076
		// (get) Token: 0x06007946 RID: 31046 RVA: 0x001968B5 File Offset: 0x00194AB5
		public static LocalizedString LdapFilterErrorEscCharWithoutEscapable
		{
			get
			{
				return new LocalizedString("LdapFilterErrorEscCharWithoutEscapable", "Ex7B5018", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B45 RID: 11077
		// (get) Token: 0x06007947 RID: 31047 RVA: 0x001968D3 File Offset: 0x00194AD3
		public static LocalizedString IndustryGovernment
		{
			get
			{
				return new LocalizedString("IndustryGovernment", "Ex83D3DC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007948 RID: 31048 RVA: 0x001968F4 File Offset: 0x00194AF4
		public static LocalizedString NspiRpcError(string error)
		{
			return new LocalizedString("NspiRpcError", "Ex34AFDD", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17002B46 RID: 11078
		// (get) Token: 0x06007949 RID: 31049 RVA: 0x00196923 File Offset: 0x00194B23
		public static LocalizedString CustomRoleDescription_MyAddressInformation
		{
			get
			{
				return new LocalizedString("CustomRoleDescription_MyAddressInformation", "Ex7F212F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B47 RID: 11079
		// (get) Token: 0x0600794A RID: 31050 RVA: 0x00196941 File Offset: 0x00194B41
		public static LocalizedString EsnLangNorwegianNynorsk
		{
			get
			{
				return new LocalizedString("EsnLangNorwegianNynorsk", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B48 RID: 11080
		// (get) Token: 0x0600794B RID: 31051 RVA: 0x0019695F File Offset: 0x00194B5F
		public static LocalizedString IndustryEngineeringArchitecture
		{
			get
			{
				return new LocalizedString("IndustryEngineeringArchitecture", "ExBCBB59", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B49 RID: 11081
		// (get) Token: 0x0600794C RID: 31052 RVA: 0x0019697D File Offset: 0x00194B7D
		public static LocalizedString SendAuthMechanismBasicAuth
		{
			get
			{
				return new LocalizedString("SendAuthMechanismBasicAuth", "ExF76D4C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B4A RID: 11082
		// (get) Token: 0x0600794D RID: 31053 RVA: 0x0019699B File Offset: 0x00194B9B
		public static LocalizedString SKUCapabilityEOPPremiumAddOn
		{
			get
			{
				return new LocalizedString("SKUCapabilityEOPPremiumAddOn", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600794E RID: 31054 RVA: 0x001969BC File Offset: 0x00194BBC
		public static LocalizedString ExceptionConflictingArguments(string property1, object value1, string property2, object value2)
		{
			return new LocalizedString("ExceptionConflictingArguments", "ExD4DA45", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property1,
				value1,
				property2,
				value2
			});
		}

		// Token: 0x0600794F RID: 31055 RVA: 0x001969F8 File Offset: 0x00194BF8
		public static LocalizedString ExceptionNoSchemaMasterServerObject(string serverId)
		{
			return new LocalizedString("ExceptionNoSchemaMasterServerObject", "Ex42060E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				serverId
			});
		}

		// Token: 0x17002B4B RID: 11083
		// (get) Token: 0x06007950 RID: 31056 RVA: 0x00196A27 File Offset: 0x00194C27
		public static LocalizedString ErrorResourceTypeInvalid
		{
			get
			{
				return new LocalizedString("ErrorResourceTypeInvalid", "ExE8B339", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007951 RID: 31057 RVA: 0x00196A48 File Offset: 0x00194C48
		public static LocalizedString ErrorMailboxExistsInCollection(string guid)
		{
			return new LocalizedString("ErrorMailboxExistsInCollection", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x06007952 RID: 31058 RVA: 0x00196A78 File Offset: 0x00194C78
		public static LocalizedString ConfigurationSettingsDriverNotInitialized(string id)
		{
			return new LocalizedString("ConfigurationSettingsDriverNotInitialized", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002B4C RID: 11084
		// (get) Token: 0x06007953 RID: 31059 RVA: 0x00196AA7 File Offset: 0x00194CA7
		public static LocalizedString OrgContainerNotFoundException
		{
			get
			{
				return new LocalizedString("OrgContainerNotFoundException", "Ex31B4F7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007954 RID: 31060 RVA: 0x00196AC8 File Offset: 0x00194CC8
		public static LocalizedString InvalidPartitionFqdn(string fqdn)
		{
			return new LocalizedString("InvalidPartitionFqdn", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				fqdn
			});
		}

		// Token: 0x06007955 RID: 31061 RVA: 0x00196AF8 File Offset: 0x00194CF8
		public static LocalizedString RoleIsMandatoryInRoleAssignment(string roleAssignment)
		{
			return new LocalizedString("RoleIsMandatoryInRoleAssignment", "ExC24890", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				roleAssignment
			});
		}

		// Token: 0x06007956 RID: 31062 RVA: 0x00196B28 File Offset: 0x00194D28
		public static LocalizedString MsaUserNotFoundInGlsError(string msaUserNetId)
		{
			return new LocalizedString("MsaUserNotFoundInGlsError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				msaUserNetId
			});
		}

		// Token: 0x17002B4D RID: 11085
		// (get) Token: 0x06007957 RID: 31063 RVA: 0x00196B57 File Offset: 0x00194D57
		public static LocalizedString SKUCapabilityBPOSSStandardArchive
		{
			get
			{
				return new LocalizedString("SKUCapabilityBPOSSStandardArchive", "Ex736D27", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B4E RID: 11086
		// (get) Token: 0x06007958 RID: 31064 RVA: 0x00196B75 File Offset: 0x00194D75
		public static LocalizedString InternalSenderAdminAddressRequired
		{
			get
			{
				return new LocalizedString("InternalSenderAdminAddressRequired", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B4F RID: 11087
		// (get) Token: 0x06007959 RID: 31065 RVA: 0x00196B93 File Offset: 0x00194D93
		public static LocalizedString CannotGetUsefulDomainInfo
		{
			get
			{
				return new LocalizedString("CannotGetUsefulDomainInfo", "Ex49782D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600795A RID: 31066 RVA: 0x00196BB4 File Offset: 0x00194DB4
		public static LocalizedString BackSyncDataSourceInDifferentSiteMessage(string domainController)
		{
			return new LocalizedString("BackSyncDataSourceInDifferentSiteMessage", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				domainController
			});
		}

		// Token: 0x17002B50 RID: 11088
		// (get) Token: 0x0600795B RID: 31067 RVA: 0x00196BE3 File Offset: 0x00194DE3
		public static LocalizedString ErrorElcSuspensionNotEnabled
		{
			get
			{
				return new LocalizedString("ErrorElcSuspensionNotEnabled", "Ex4181F3", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B51 RID: 11089
		// (get) Token: 0x0600795C RID: 31068 RVA: 0x00196C01 File Offset: 0x00194E01
		public static LocalizedString DatabaseMasterTypeServer
		{
			get
			{
				return new LocalizedString("DatabaseMasterTypeServer", "Ex11F659", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600795D RID: 31069 RVA: 0x00196C20 File Offset: 0x00194E20
		public static LocalizedString CannotResolveTenantRelocationRequestIdentity(string name)
		{
			return new LocalizedString("CannotResolveTenantRelocationRequestIdentity", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17002B52 RID: 11090
		// (get) Token: 0x0600795E RID: 31070 RVA: 0x00196C4F File Offset: 0x00194E4F
		public static LocalizedString ConnectionTimeoutLessThanInactivityTimeout
		{
			get
			{
				return new LocalizedString("ConnectionTimeoutLessThanInactivityTimeout", "ExE567D7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600795F RID: 31071 RVA: 0x00196C70 File Offset: 0x00194E70
		public static LocalizedString FFOMigration_Error(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("FFOMigration_Error", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x06007960 RID: 31072 RVA: 0x00196CAC File Offset: 0x00194EAC
		public static LocalizedString ExceptionTokenGroupsNeedsDomainSession(string id)
		{
			return new LocalizedString("ExceptionTokenGroupsNeedsDomainSession", "ExD7F5EB", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002B53 RID: 11091
		// (get) Token: 0x06007961 RID: 31073 RVA: 0x00196CDB File Offset: 0x00194EDB
		public static LocalizedString HygieneSuitePremium
		{
			get
			{
				return new LocalizedString("HygieneSuitePremium", "Ex6251E0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B54 RID: 11092
		// (get) Token: 0x06007962 RID: 31074 RVA: 0x00196CF9 File Offset: 0x00194EF9
		public static LocalizedString Exadmin
		{
			get
			{
				return new LocalizedString("Exadmin", "ExDF2256", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007963 RID: 31075 RVA: 0x00196D18 File Offset: 0x00194F18
		public static LocalizedString ErrorAdfsAudienceUriDup(string audienceUriString)
		{
			return new LocalizedString("ErrorAdfsAudienceUriDup", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				audienceUriString
			});
		}

		// Token: 0x17002B55 RID: 11093
		// (get) Token: 0x06007964 RID: 31076 RVA: 0x00196D47 File Offset: 0x00194F47
		public static LocalizedString ExceptionADTopologyCannotFindWellKnownExchangeGroup
		{
			get
			{
				return new LocalizedString("ExceptionADTopologyCannotFindWellKnownExchangeGroup", "Ex786BA2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007965 RID: 31077 RVA: 0x00196D68 File Offset: 0x00194F68
		public static LocalizedString ReplicationNotComplete(string forestName, string dcName)
		{
			return new LocalizedString("ReplicationNotComplete", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				forestName,
				dcName
			});
		}

		// Token: 0x17002B56 RID: 11094
		// (get) Token: 0x06007966 RID: 31078 RVA: 0x00196D9B File Offset: 0x00194F9B
		public static LocalizedString CommandFrequency
		{
			get
			{
				return new LocalizedString("CommandFrequency", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007967 RID: 31079 RVA: 0x00196DBC File Offset: 0x00194FBC
		public static LocalizedString UserIsMandatoryInRoleAssignment(string roleAssignment)
		{
			return new LocalizedString("UserIsMandatoryInRoleAssignment", "Ex835876", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				roleAssignment
			});
		}

		// Token: 0x17002B57 RID: 11095
		// (get) Token: 0x06007968 RID: 31080 RVA: 0x00196DEB File Offset: 0x00194FEB
		public static LocalizedString IndustryConstruction
		{
			get
			{
				return new LocalizedString("IndustryConstruction", "Ex712455", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B58 RID: 11096
		// (get) Token: 0x06007969 RID: 31081 RVA: 0x00196E09 File Offset: 0x00195009
		public static LocalizedString SharedMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("SharedMailboxRecipientTypeDetails", "ExB1D42C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600796A RID: 31082 RVA: 0x00196E28 File Offset: 0x00195028
		public static LocalizedString ConfigurationSettingsInvalidMatch(string expression)
		{
			return new LocalizedString("ConfigurationSettingsInvalidMatch", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				expression
			});
		}

		// Token: 0x17002B59 RID: 11097
		// (get) Token: 0x0600796B RID: 31083 RVA: 0x00196E57 File Offset: 0x00195057
		public static LocalizedString AccessDeniedToEventLog
		{
			get
			{
				return new LocalizedString("AccessDeniedToEventLog", "Ex87B610", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B5A RID: 11098
		// (get) Token: 0x0600796C RID: 31084 RVA: 0x00196E75 File Offset: 0x00195075
		public static LocalizedString EsnLangSerbian
		{
			get
			{
				return new LocalizedString("EsnLangSerbian", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B5B RID: 11099
		// (get) Token: 0x0600796D RID: 31085 RVA: 0x00196E93 File Offset: 0x00195093
		public static LocalizedString ReplicationTypeUnknown
		{
			get
			{
				return new LocalizedString("ReplicationTypeUnknown", "Ex89D12B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B5C RID: 11100
		// (get) Token: 0x0600796E RID: 31086 RVA: 0x00196EB1 File Offset: 0x001950B1
		public static LocalizedString ErrorDuplicateMapiIdsInConfiguredAttributes
		{
			get
			{
				return new LocalizedString("ErrorDuplicateMapiIdsInConfiguredAttributes", "Ex1AAB23", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B5D RID: 11101
		// (get) Token: 0x0600796F RID: 31087 RVA: 0x00196ECF File Offset: 0x001950CF
		public static LocalizedString DirectoryBasedEdgeBlockModeOn
		{
			get
			{
				return new LocalizedString("DirectoryBasedEdgeBlockModeOn", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B5E RID: 11102
		// (get) Token: 0x06007970 RID: 31088 RVA: 0x00196EED File Offset: 0x001950ED
		public static LocalizedString LiveCredentialWithoutBasic
		{
			get
			{
				return new LocalizedString("LiveCredentialWithoutBasic", "ExB9FFB5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007971 RID: 31089 RVA: 0x00196F0C File Offset: 0x0019510C
		public static LocalizedString TenantIsLockedDownForRelocationException(string dn)
		{
			return new LocalizedString("TenantIsLockedDownForRelocationException", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				dn
			});
		}

		// Token: 0x17002B5F RID: 11103
		// (get) Token: 0x06007972 RID: 31090 RVA: 0x00196F3B File Offset: 0x0019513B
		public static LocalizedString ExclusiveConfigScopes
		{
			get
			{
				return new LocalizedString("ExclusiveConfigScopes", "Ex1C038A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B60 RID: 11104
		// (get) Token: 0x06007973 RID: 31091 RVA: 0x00196F59 File Offset: 0x00195159
		public static LocalizedString IndustryRealEstate
		{
			get
			{
				return new LocalizedString("IndustryRealEstate", "Ex442E53", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B61 RID: 11105
		// (get) Token: 0x06007974 RID: 31092 RVA: 0x00196F77 File Offset: 0x00195177
		public static LocalizedString EsnLangNorwegian
		{
			get
			{
				return new LocalizedString("EsnLangNorwegian", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B62 RID: 11106
		// (get) Token: 0x06007975 RID: 31093 RVA: 0x00196F95 File Offset: 0x00195195
		public static LocalizedString ServerRoleMonitoring
		{
			get
			{
				return new LocalizedString("ServerRoleMonitoring", "Ex734227", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B63 RID: 11107
		// (get) Token: 0x06007976 RID: 31094 RVA: 0x00196FB3 File Offset: 0x001951B3
		public static LocalizedString ASInvalidAccessMethod
		{
			get
			{
				return new LocalizedString("ASInvalidAccessMethod", "ExB624F6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007977 RID: 31095 RVA: 0x00196FD4 File Offset: 0x001951D4
		public static LocalizedString ExceptionInvalidApprovedApplication(string cabName)
		{
			return new LocalizedString("ExceptionInvalidApprovedApplication", "Ex6B54A4", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				cabName
			});
		}

		// Token: 0x17002B64 RID: 11108
		// (get) Token: 0x06007978 RID: 31096 RVA: 0x00197003 File Offset: 0x00195203
		public static LocalizedString NotApplied
		{
			get
			{
				return new LocalizedString("NotApplied", "Ex12D9B4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B65 RID: 11109
		// (get) Token: 0x06007979 RID: 31097 RVA: 0x00197021 File Offset: 0x00195221
		public static LocalizedString ConfigurationSettingsADNotificationError
		{
			get
			{
				return new LocalizedString("ConfigurationSettingsADNotificationError", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600797A RID: 31098 RVA: 0x00197040 File Offset: 0x00195240
		public static LocalizedString ErrorNonUniqueProxy(string proxy)
		{
			return new LocalizedString("ErrorNonUniqueProxy", "Ex64B7A8", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				proxy
			});
		}

		// Token: 0x0600797B RID: 31099 RVA: 0x00197070 File Offset: 0x00195270
		public static LocalizedString ExceptionWKGuidNeedsGCSession(Guid wkguid)
		{
			return new LocalizedString("ExceptionWKGuidNeedsGCSession", "ExA532DB", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				wkguid
			});
		}

		// Token: 0x17002B66 RID: 11110
		// (get) Token: 0x0600797C RID: 31100 RVA: 0x001970A4 File Offset: 0x001952A4
		public static LocalizedString MonitoringMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MonitoringMailboxRecipientTypeDetails", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B67 RID: 11111
		// (get) Token: 0x0600797D RID: 31101 RVA: 0x001970C2 File Offset: 0x001952C2
		public static LocalizedString EsnLangCroatian
		{
			get
			{
				return new LocalizedString("EsnLangCroatian", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600797E RID: 31102 RVA: 0x001970E0 File Offset: 0x001952E0
		public static LocalizedString ErrorPolicyDontSupportedPresentationObject(Type poType, Type policyType)
		{
			return new LocalizedString("ErrorPolicyDontSupportedPresentationObject", "Ex0BDC0E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				poType,
				policyType
			});
		}

		// Token: 0x17002B68 RID: 11112
		// (get) Token: 0x0600797F RID: 31103 RVA: 0x00197113 File Offset: 0x00195313
		public static LocalizedString TlsAuthLevelWithDomainSecureEnabled
		{
			get
			{
				return new LocalizedString("TlsAuthLevelWithDomainSecureEnabled", "Ex22DD90", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B69 RID: 11113
		// (get) Token: 0x06007980 RID: 31104 RVA: 0x00197131 File Offset: 0x00195331
		public static LocalizedString EsnLangGerman
		{
			get
			{
				return new LocalizedString("EsnLangGerman", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B6A RID: 11114
		// (get) Token: 0x06007981 RID: 31105 RVA: 0x0019714F File Offset: 0x0019534F
		public static LocalizedString RoleAssignmentPolicyDescription_Default
		{
			get
			{
				return new LocalizedString("RoleAssignmentPolicyDescription_Default", "ExEDB6B3", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007982 RID: 31106 RVA: 0x00197170 File Offset: 0x00195370
		public static LocalizedString ErrorIsServerSuitableInvalidOSVersion(string dcName, string osVersion, string osServicePack, string minOSVerion, string minServicePack)
		{
			return new LocalizedString("ErrorIsServerSuitableInvalidOSVersion", "Ex8C2DEE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				dcName,
				osVersion,
				osServicePack,
				minOSVerion,
				minServicePack
			});
		}

		// Token: 0x17002B6B RID: 11115
		// (get) Token: 0x06007983 RID: 31107 RVA: 0x001971B0 File Offset: 0x001953B0
		public static LocalizedString GroupTypeFlagsNone
		{
			get
			{
				return new LocalizedString("GroupTypeFlagsNone", "Ex4DEE8D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007984 RID: 31108 RVA: 0x001971D0 File Offset: 0x001953D0
		public static LocalizedString SipUriAlreadyRegistered(string sipUri, string user)
		{
			return new LocalizedString("SipUriAlreadyRegistered", "ExDE9693", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				sipUri,
				user
			});
		}

		// Token: 0x17002B6C RID: 11116
		// (get) Token: 0x06007985 RID: 31109 RVA: 0x00197203 File Offset: 0x00195403
		public static LocalizedString WellKnownRecipientTypeMailboxUsers
		{
			get
			{
				return new LocalizedString("WellKnownRecipientTypeMailboxUsers", "Ex5323F7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B6D RID: 11117
		// (get) Token: 0x06007986 RID: 31110 RVA: 0x00197221 File Offset: 0x00195421
		public static LocalizedString LdapFilterErrorInvalidWildCardGtLt
		{
			get
			{
				return new LocalizedString("LdapFilterErrorInvalidWildCardGtLt", "ExC904F2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B6E RID: 11118
		// (get) Token: 0x06007987 RID: 31111 RVA: 0x0019723F File Offset: 0x0019543F
		public static LocalizedString SmartHostNotSet
		{
			get
			{
				return new LocalizedString("SmartHostNotSet", "Ex036929", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B6F RID: 11119
		// (get) Token: 0x06007988 RID: 31112 RVA: 0x0019725D File Offset: 0x0019545D
		public static LocalizedString DeviceRule
		{
			get
			{
				return new LocalizedString("DeviceRule", "Ex6D77B2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B70 RID: 11120
		// (get) Token: 0x06007989 RID: 31113 RVA: 0x0019727B File Offset: 0x0019547B
		public static LocalizedString NotTrust
		{
			get
			{
				return new LocalizedString("NotTrust", "Ex12ECDA", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B71 RID: 11121
		// (get) Token: 0x0600798A RID: 31114 RVA: 0x00197299 File Offset: 0x00195499
		public static LocalizedString EmailAgeFilterAll
		{
			get
			{
				return new LocalizedString("EmailAgeFilterAll", "ExD345D8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B72 RID: 11122
		// (get) Token: 0x0600798B RID: 31115 RVA: 0x001972B7 File Offset: 0x001954B7
		public static LocalizedString LanguageBlockListNotSet
		{
			get
			{
				return new LocalizedString("LanguageBlockListNotSet", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B73 RID: 11123
		// (get) Token: 0x0600798C RID: 31116 RVA: 0x001972D5 File Offset: 0x001954D5
		public static LocalizedString EsnLangSerbianCyrillic
		{
			get
			{
				return new LocalizedString("EsnLangSerbianCyrillic", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B74 RID: 11124
		// (get) Token: 0x0600798D RID: 31117 RVA: 0x001972F3 File Offset: 0x001954F3
		public static LocalizedString CalendarAgeFilterSixMonths
		{
			get
			{
				return new LocalizedString("CalendarAgeFilterSixMonths", "Ex5834F4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600798E RID: 31118 RVA: 0x00197314 File Offset: 0x00195514
		public static LocalizedString WrongDCForCurrentPartition(string scName, string partitionFqdn)
		{
			return new LocalizedString("WrongDCForCurrentPartition", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				scName,
				partitionFqdn
			});
		}

		// Token: 0x17002B75 RID: 11125
		// (get) Token: 0x0600798F RID: 31119 RVA: 0x00197347 File Offset: 0x00195547
		public static LocalizedString ErrorMetadataNotSearchProperty
		{
			get
			{
				return new LocalizedString("ErrorMetadataNotSearchProperty", "ExAFF7B8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007990 RID: 31120 RVA: 0x00197368 File Offset: 0x00195568
		public static LocalizedString BPOS_S_Policy_License_Violation(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("BPOS_S_Policy_License_Violation", "ExD4C0C9", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x06007991 RID: 31121 RVA: 0x001973A4 File Offset: 0x001955A4
		public static LocalizedString ExceptionExtendedRightsNotUnique(string displayName, string exRight1, string exRight2)
		{
			return new LocalizedString("ExceptionExtendedRightsNotUnique", "Ex952707", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				displayName,
				exRight1,
				exRight2
			});
		}

		// Token: 0x06007992 RID: 31122 RVA: 0x001973DC File Offset: 0x001955DC
		public static LocalizedString ExceptionGuidSearchRootWithDefaultScope(string guid)
		{
			return new LocalizedString("ExceptionGuidSearchRootWithDefaultScope", "Ex572273", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x17002B76 RID: 11126
		// (get) Token: 0x06007993 RID: 31123 RVA: 0x0019740B File Offset: 0x0019560B
		public static LocalizedString InvalidDefaultMailbox
		{
			get
			{
				return new LocalizedString("InvalidDefaultMailbox", "Ex7FA38D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B77 RID: 11127
		// (get) Token: 0x06007994 RID: 31124 RVA: 0x00197429 File Offset: 0x00195629
		public static LocalizedString Drafts
		{
			get
			{
				return new LocalizedString("Drafts", "Ex301322", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B78 RID: 11128
		// (get) Token: 0x06007995 RID: 31125 RVA: 0x00197447 File Offset: 0x00195647
		public static LocalizedString RemoteGroupMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("RemoteGroupMailboxRecipientTypeDetails", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B79 RID: 11129
		// (get) Token: 0x06007996 RID: 31126 RVA: 0x00197465 File Offset: 0x00195665
		public static LocalizedString EsnLangSwahili
		{
			get
			{
				return new LocalizedString("EsnLangSwahili", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B7A RID: 11130
		// (get) Token: 0x06007997 RID: 31127 RVA: 0x00197483 File Offset: 0x00195683
		public static LocalizedString ExceptionPagedReaderReadAllAfterEnumerating
		{
			get
			{
				return new LocalizedString("ExceptionPagedReaderReadAllAfterEnumerating", "ExBD3952", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B7B RID: 11131
		// (get) Token: 0x06007998 RID: 31128 RVA: 0x001974A1 File Offset: 0x001956A1
		public static LocalizedString DsnDefaultLanguageMustBeSpecificCulture
		{
			get
			{
				return new LocalizedString("DsnDefaultLanguageMustBeSpecificCulture", "ExF0474E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007999 RID: 31129 RVA: 0x001974C0 File Offset: 0x001956C0
		public static LocalizedString ErrorSettingOverrideUnknown(string errorType, string componentName, string objectName, string parameters)
		{
			return new LocalizedString("ErrorSettingOverrideUnknown", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				errorType,
				componentName,
				objectName,
				parameters
			});
		}

		// Token: 0x17002B7C RID: 11132
		// (get) Token: 0x0600799A RID: 31130 RVA: 0x001974FB File Offset: 0x001956FB
		public static LocalizedString BestBodyFormat
		{
			get
			{
				return new LocalizedString("BestBodyFormat", "Ex706614", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600799B RID: 31131 RVA: 0x0019751C File Offset: 0x0019571C
		public static LocalizedString ErrorIncorrectlyModifiedMailboxCollection(string property)
		{
			return new LocalizedString("ErrorIncorrectlyModifiedMailboxCollection", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x0600799C RID: 31132 RVA: 0x0019754C File Offset: 0x0019574C
		public static LocalizedString ConfigurationSettingsRestrictionExtraProperty(string name, string propertyName)
		{
			return new LocalizedString("ConfigurationSettingsRestrictionExtraProperty", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				name,
				propertyName
			});
		}

		// Token: 0x0600799D RID: 31133 RVA: 0x00197580 File Offset: 0x00195780
		public static LocalizedString ConstraintViolationInvalidRecipientType(string propertyName, string actualValue)
		{
			return new LocalizedString("ConstraintViolationInvalidRecipientType", "Ex6552BA", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				actualValue
			});
		}

		// Token: 0x17002B7D RID: 11133
		// (get) Token: 0x0600799E RID: 31134 RVA: 0x001975B3 File Offset: 0x001957B3
		public static LocalizedString CanEnableLocalCopyState_AlreadyEnabled
		{
			get
			{
				return new LocalizedString("CanEnableLocalCopyState_AlreadyEnabled", "Ex59D5C0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B7E RID: 11134
		// (get) Token: 0x0600799F RID: 31135 RVA: 0x001975D1 File Offset: 0x001957D1
		public static LocalizedString DeviceDiscovery
		{
			get
			{
				return new LocalizedString("DeviceDiscovery", "ExF4E0B7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B7F RID: 11135
		// (get) Token: 0x060079A0 RID: 31136 RVA: 0x001975EF File Offset: 0x001957EF
		public static LocalizedString AccessDenied
		{
			get
			{
				return new LocalizedString("AccessDenied", "Ex8805BA", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B80 RID: 11136
		// (get) Token: 0x060079A1 RID: 31137 RVA: 0x0019760D File Offset: 0x0019580D
		public static LocalizedString InvalidActiveUserStatisticsLogSizeConfiguration
		{
			get
			{
				return new LocalizedString("InvalidActiveUserStatisticsLogSizeConfiguration", "Ex2D8E8D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B81 RID: 11137
		// (get) Token: 0x060079A2 RID: 31138 RVA: 0x0019762B File Offset: 0x0019582B
		public static LocalizedString ErrorActionOnExpirationSpecified
		{
			get
			{
				return new LocalizedString("ErrorActionOnExpirationSpecified", "Ex198A72", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079A3 RID: 31139 RVA: 0x0019764C File Offset: 0x0019584C
		public static LocalizedString UnableToResolveMapiIdException(int mapiid)
		{
			return new LocalizedString("UnableToResolveMapiIdException", "Ex21AC53", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				mapiid
			});
		}

		// Token: 0x17002B82 RID: 11138
		// (get) Token: 0x060079A4 RID: 31140 RVA: 0x00197680 File Offset: 0x00195880
		public static LocalizedString TlsAuthLevelWithNoDomainOnSmartHost
		{
			get
			{
				return new LocalizedString("TlsAuthLevelWithNoDomainOnSmartHost", "ExD3505B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B83 RID: 11139
		// (get) Token: 0x060079A5 RID: 31141 RVA: 0x0019769E File Offset: 0x0019589E
		public static LocalizedString DeferredFailoverEntryString
		{
			get
			{
				return new LocalizedString("DeferredFailoverEntryString", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079A6 RID: 31142 RVA: 0x001976BC File Offset: 0x001958BC
		public static LocalizedString ErrorSettingOverrideInvalidVariantName(string componentName, string sectionName, string variantName, string availableVariantNames)
		{
			return new LocalizedString("ErrorSettingOverrideInvalidVariantName", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				componentName,
				sectionName,
				variantName,
				availableVariantNames
			});
		}

		// Token: 0x060079A7 RID: 31143 RVA: 0x001976F8 File Offset: 0x001958F8
		public static LocalizedString ErrorWebDistributionEnabledWithoutVersion4(string name)
		{
			return new LocalizedString("ErrorWebDistributionEnabledWithoutVersion4", "ExE62073", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17002B84 RID: 11140
		// (get) Token: 0x060079A8 RID: 31144 RVA: 0x00197727 File Offset: 0x00195927
		public static LocalizedString TaskItemsMC
		{
			get
			{
				return new LocalizedString("TaskItemsMC", "Ex1A139D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B85 RID: 11141
		// (get) Token: 0x060079A9 RID: 31145 RVA: 0x00197745 File Offset: 0x00195945
		public static LocalizedString GroupNamingPolicyCustomAttribute7
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute7", "Ex16034B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B86 RID: 11142
		// (get) Token: 0x060079AA RID: 31146 RVA: 0x00197763 File Offset: 0x00195963
		public static LocalizedString UnknownAttribute
		{
			get
			{
				return new LocalizedString("UnknownAttribute", "Ex6BB76E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B87 RID: 11143
		// (get) Token: 0x060079AB RID: 31147 RVA: 0x00197781 File Offset: 0x00195981
		public static LocalizedString MountDialOverrideBestAvailability
		{
			get
			{
				return new LocalizedString("MountDialOverrideBestAvailability", "Ex1CCE6E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079AC RID: 31148 RVA: 0x001977A0 File Offset: 0x001959A0
		public static LocalizedString CannotResolveAccountForestDnError(string fqdn)
		{
			return new LocalizedString("CannotResolveAccountForestDnError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				fqdn
			});
		}

		// Token: 0x17002B88 RID: 11144
		// (get) Token: 0x060079AD RID: 31149 RVA: 0x001977CF File Offset: 0x001959CF
		public static LocalizedString ErrorArbitrationMailboxPropertyEmailAddressesEmpty
		{
			get
			{
				return new LocalizedString("ErrorArbitrationMailboxPropertyEmailAddressesEmpty", "ExD43DC1", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B89 RID: 11145
		// (get) Token: 0x060079AE RID: 31150 RVA: 0x001977ED File Offset: 0x001959ED
		public static LocalizedString AlternateServiceAccountCredentialNotSet
		{
			get
			{
				return new LocalizedString("AlternateServiceAccountCredentialNotSet", "ExE2101D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079AF RID: 31151 RVA: 0x0019780C File Offset: 0x00195A0C
		public static LocalizedString MobileMetabasePathIsInvalid(string id, string path)
		{
			return new LocalizedString("MobileMetabasePathIsInvalid", "Ex95ABC7", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id,
				path
			});
		}

		// Token: 0x17002B8A RID: 11146
		// (get) Token: 0x060079B0 RID: 31152 RVA: 0x0019783F File Offset: 0x00195A3F
		public static LocalizedString DataMoveReplicationConstraintAllCopies
		{
			get
			{
				return new LocalizedString("DataMoveReplicationConstraintAllCopies", "Ex98FF4C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B8B RID: 11147
		// (get) Token: 0x060079B1 RID: 31153 RVA: 0x0019785D File Offset: 0x00195A5D
		public static LocalizedString GlobalThrottlingPolicyAmbiguousException
		{
			get
			{
				return new LocalizedString("GlobalThrottlingPolicyAmbiguousException", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B8C RID: 11148
		// (get) Token: 0x060079B2 RID: 31154 RVA: 0x0019787B File Offset: 0x00195A7B
		public static LocalizedString InvalidServerStatisticsLogSizeConfiguration
		{
			get
			{
				return new LocalizedString("InvalidServerStatisticsLogSizeConfiguration", "ExDCECA4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B8D RID: 11149
		// (get) Token: 0x060079B3 RID: 31155 RVA: 0x00197899 File Offset: 0x00195A99
		public static LocalizedString SipResourceIdRequired
		{
			get
			{
				return new LocalizedString("SipResourceIdRequired", "ExA03081", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079B4 RID: 31156 RVA: 0x001978B8 File Offset: 0x00195AB8
		public static LocalizedString InvalidAttachmentFilterExtension(string fileSpec)
		{
			return new LocalizedString("InvalidAttachmentFilterExtension", "ExF84F4A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				fileSpec
			});
		}

		// Token: 0x17002B8E RID: 11150
		// (get) Token: 0x060079B5 RID: 31157 RVA: 0x001978E7 File Offset: 0x00195AE7
		public static LocalizedString EsnLangPortuguese
		{
			get
			{
				return new LocalizedString("EsnLangPortuguese", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B8F RID: 11151
		// (get) Token: 0x060079B6 RID: 31158 RVA: 0x00197905 File Offset: 0x00195B05
		public static LocalizedString AutoDetect
		{
			get
			{
				return new LocalizedString("AutoDetect", "ExDBE7AF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079B7 RID: 31159 RVA: 0x00197924 File Offset: 0x00195B24
		public static LocalizedString ErrorInvalidAuthSettings(string orgId)
		{
			return new LocalizedString("ErrorInvalidAuthSettings", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				orgId
			});
		}

		// Token: 0x17002B90 RID: 11152
		// (get) Token: 0x060079B8 RID: 31160 RVA: 0x00197953 File Offset: 0x00195B53
		public static LocalizedString SpamFilteringActionRedirect
		{
			get
			{
				return new LocalizedString("SpamFilteringActionRedirect", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B91 RID: 11153
		// (get) Token: 0x060079B9 RID: 31161 RVA: 0x00197971 File Offset: 0x00195B71
		public static LocalizedString CanRunRestoreState_Invalid
		{
			get
			{
				return new LocalizedString("CanRunRestoreState_Invalid", "ExCC2634", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B92 RID: 11154
		// (get) Token: 0x060079BA RID: 31162 RVA: 0x0019798F File Offset: 0x00195B8F
		public static LocalizedString OutboundConnectorIncorrectCloudServicesMailEnabled
		{
			get
			{
				return new LocalizedString("OutboundConnectorIncorrectCloudServicesMailEnabled", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B93 RID: 11155
		// (get) Token: 0x060079BB RID: 31163 RVA: 0x001979AD File Offset: 0x00195BAD
		public static LocalizedString DatabaseCopyAutoActivationPolicyBlocked
		{
			get
			{
				return new LocalizedString("DatabaseCopyAutoActivationPolicyBlocked", "ExA9434B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B94 RID: 11156
		// (get) Token: 0x060079BC RID: 31164 RVA: 0x001979CB File Offset: 0x00195BCB
		public static LocalizedString CustomRoleDescription_MyName
		{
			get
			{
				return new LocalizedString("CustomRoleDescription_MyName", "Ex806355", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079BD RID: 31165 RVA: 0x001979EC File Offset: 0x00195BEC
		public static LocalizedString ErrorExchangeGroupNotFound(Guid idStringValue)
		{
			return new LocalizedString("ErrorExchangeGroupNotFound", "Ex519D3C", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				idStringValue
			});
		}

		// Token: 0x17002B95 RID: 11157
		// (get) Token: 0x060079BE RID: 31166 RVA: 0x00197A20 File Offset: 0x00195C20
		public static LocalizedString EsnLangOriya
		{
			get
			{
				return new LocalizedString("EsnLangOriya", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079BF RID: 31167 RVA: 0x00197A40 File Offset: 0x00195C40
		public static LocalizedString ErrorSettingOverrideInvalidVariantValue(string componentName, string sectionName, string variantName, string variantType, string format)
		{
			return new LocalizedString("ErrorSettingOverrideInvalidVariantValue", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				componentName,
				sectionName,
				variantName,
				variantType,
				format
			});
		}

		// Token: 0x17002B96 RID: 11158
		// (get) Token: 0x060079C0 RID: 31168 RVA: 0x00197A80 File Offset: 0x00195C80
		public static LocalizedString UserAgent
		{
			get
			{
				return new LocalizedString("UserAgent", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079C1 RID: 31169 RVA: 0x00197AA0 File Offset: 0x00195CA0
		public static LocalizedString UnKnownScopeRestrictionType(string scopeType, string objectName)
		{
			return new LocalizedString("UnKnownScopeRestrictionType", "ExDFC368", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scopeType,
				objectName
			});
		}

		// Token: 0x17002B97 RID: 11159
		// (get) Token: 0x060079C2 RID: 31170 RVA: 0x00197AD3 File Offset: 0x00195CD3
		public static LocalizedString DomainStateActive
		{
			get
			{
				return new LocalizedString("DomainStateActive", "ExE67962", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B98 RID: 11160
		// (get) Token: 0x060079C3 RID: 31171 RVA: 0x00197AF1 File Offset: 0x00195CF1
		public static LocalizedString PartnersCannotHaveWildcards
		{
			get
			{
				return new LocalizedString("PartnersCannotHaveWildcards", "Ex2959D7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079C4 RID: 31172 RVA: 0x00197B10 File Offset: 0x00195D10
		public static LocalizedString NonexistentTimeZoneError(string etz)
		{
			return new LocalizedString("NonexistentTimeZoneError", "Ex39DB84", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				etz
			});
		}

		// Token: 0x17002B99 RID: 11161
		// (get) Token: 0x060079C5 RID: 31173 RVA: 0x00197B3F File Offset: 0x00195D3F
		public static LocalizedString IPv4Only
		{
			get
			{
				return new LocalizedString("IPv4Only", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B9A RID: 11162
		// (get) Token: 0x060079C6 RID: 31174 RVA: 0x00197B5D File Offset: 0x00195D5D
		public static LocalizedString InboundConnectorInvalidIPCertificateCombinations
		{
			get
			{
				return new LocalizedString("InboundConnectorInvalidIPCertificateCombinations", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B9B RID: 11163
		// (get) Token: 0x060079C7 RID: 31175 RVA: 0x00197B7B File Offset: 0x00195D7B
		public static LocalizedString Exchange2003or2000
		{
			get
			{
				return new LocalizedString("Exchange2003or2000", "Ex091970", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B9C RID: 11164
		// (get) Token: 0x060079C8 RID: 31176 RVA: 0x00197B99 File Offset: 0x00195D99
		public static LocalizedString ErrorOneProcessInitializedAsBothSingleAndMultiple
		{
			get
			{
				return new LocalizedString("ErrorOneProcessInitializedAsBothSingleAndMultiple", "Ex013067", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B9D RID: 11165
		// (get) Token: 0x060079C9 RID: 31177 RVA: 0x00197BB7 File Offset: 0x00195DB7
		public static LocalizedString RoomListGroupTypeDetails
		{
			get
			{
				return new LocalizedString("RoomListGroupTypeDetails", "Ex2A94AA", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B9E RID: 11166
		// (get) Token: 0x060079CA RID: 31178 RVA: 0x00197BD5 File Offset: 0x00195DD5
		public static LocalizedString MailEnabledForestContactRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailEnabledForestContactRecipientTypeDetails", "ExF49729", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002B9F RID: 11167
		// (get) Token: 0x060079CB RID: 31179 RVA: 0x00197BF3 File Offset: 0x00195DF3
		public static LocalizedString ErrorAuthMetadataNoIssuingEndpoint
		{
			get
			{
				return new LocalizedString("ErrorAuthMetadataNoIssuingEndpoint", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079CC RID: 31180 RVA: 0x00197C14 File Offset: 0x00195E14
		public static LocalizedString ErrorExceededMultiTenantResourceCountQuota(string policyId, string poType, string org, int countQuota)
		{
			return new LocalizedString("ErrorExceededMultiTenantResourceCountQuota", "Ex39B7BD", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				policyId,
				poType,
				org,
				countQuota
			});
		}

		// Token: 0x17002BA0 RID: 11168
		// (get) Token: 0x060079CD RID: 31181 RVA: 0x00197C54 File Offset: 0x00195E54
		public static LocalizedString NonUniversalGroupRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("NonUniversalGroupRecipientTypeDetails", "Ex71129D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BA1 RID: 11169
		// (get) Token: 0x060079CE RID: 31182 RVA: 0x00197C72 File Offset: 0x00195E72
		public static LocalizedString ErrorMustBeSysConfigObject
		{
			get
			{
				return new LocalizedString("ErrorMustBeSysConfigObject", "Ex6088D1", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079CF RID: 31183 RVA: 0x00197C90 File Offset: 0x00195E90
		public static LocalizedString AddressBookNotFoundException(string id)
		{
			return new LocalizedString("AddressBookNotFoundException", "Ex858C48", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x060079D0 RID: 31184 RVA: 0x00197CC0 File Offset: 0x00195EC0
		public static LocalizedString ExtensionIsInvalid(string s, int i)
		{
			return new LocalizedString("ExtensionIsInvalid", "Ex9E4318", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				s,
				i
			});
		}

		// Token: 0x060079D1 RID: 31185 RVA: 0x00197CF8 File Offset: 0x00195EF8
		public static LocalizedString AppendLocalizedStrings(string str1, string str2)
		{
			return new LocalizedString("AppendLocalizedStrings", "Ex091BF9", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				str1,
				str2
			});
		}

		// Token: 0x17002BA2 RID: 11170
		// (get) Token: 0x060079D2 RID: 31186 RVA: 0x00197D2B File Offset: 0x00195F2B
		public static LocalizedString OutboundConnectorTlsSettingsInvalidTlsDomainWithoutDomainValidation
		{
			get
			{
				return new LocalizedString("OutboundConnectorTlsSettingsInvalidTlsDomainWithoutDomainValidation", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BA3 RID: 11171
		// (get) Token: 0x060079D3 RID: 31187 RVA: 0x00197D49 File Offset: 0x00195F49
		public static LocalizedString LdapFilterErrorInvalidBitwiseOperand
		{
			get
			{
				return new LocalizedString("LdapFilterErrorInvalidBitwiseOperand", "Ex76E0B9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BA4 RID: 11172
		// (get) Token: 0x060079D4 RID: 31188 RVA: 0x00197D67 File Offset: 0x00195F67
		public static LocalizedString ExceptionSetPreferredDCsOnlyForManagement
		{
			get
			{
				return new LocalizedString("ExceptionSetPreferredDCsOnlyForManagement", "Ex0B689E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079D5 RID: 31189 RVA: 0x00197D88 File Offset: 0x00195F88
		public static LocalizedString SuitabilityExceptionLdapSearch(string fqnd, string details)
		{
			return new LocalizedString("SuitabilityExceptionLdapSearch", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				fqnd,
				details
			});
		}

		// Token: 0x17002BA5 RID: 11173
		// (get) Token: 0x060079D6 RID: 31190 RVA: 0x00197DBB File Offset: 0x00195FBB
		public static LocalizedString LegacyArchiveJournals
		{
			get
			{
				return new LocalizedString("LegacyArchiveJournals", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BA6 RID: 11174
		// (get) Token: 0x060079D7 RID: 31191 RVA: 0x00197DD9 File Offset: 0x00195FD9
		public static LocalizedString CustomInternalSubjectRequired
		{
			get
			{
				return new LocalizedString("CustomInternalSubjectRequired", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BA7 RID: 11175
		// (get) Token: 0x060079D8 RID: 31192 RVA: 0x00197DF7 File Offset: 0x00195FF7
		public static LocalizedString ErrorCannotAddArchiveMailbox
		{
			get
			{
				return new LocalizedString("ErrorCannotAddArchiveMailbox", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BA8 RID: 11176
		// (get) Token: 0x060079D9 RID: 31193 RVA: 0x00197E15 File Offset: 0x00196015
		public static LocalizedString NoNewCalls
		{
			get
			{
				return new LocalizedString("NoNewCalls", "Ex6080C2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079DA RID: 31194 RVA: 0x00197E34 File Offset: 0x00196034
		public static LocalizedString ErrorExchangeMailboxExists(string guid)
		{
			return new LocalizedString("ErrorExchangeMailboxExists", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x17002BA9 RID: 11177
		// (get) Token: 0x060079DB RID: 31195 RVA: 0x00197E63 File Offset: 0x00196063
		public static LocalizedString ErrorMessageClassEmpty
		{
			get
			{
				return new LocalizedString("ErrorMessageClassEmpty", "Ex502380", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079DC RID: 31196 RVA: 0x00197E84 File Offset: 0x00196084
		public static LocalizedString ExceptionInvalidVlvFilterProperty(string propertyName)
		{
			return new LocalizedString("ExceptionInvalidVlvFilterProperty", "Ex8695E4", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x17002BAA RID: 11178
		// (get) Token: 0x060079DD RID: 31197 RVA: 0x00197EB3 File Offset: 0x001960B3
		public static LocalizedString GloballyDistributedOABCacheReadTimeoutError
		{
			get
			{
				return new LocalizedString("GloballyDistributedOABCacheReadTimeoutError", "ExAC1F55", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BAB RID: 11179
		// (get) Token: 0x060079DE RID: 31198 RVA: 0x00197ED1 File Offset: 0x001960D1
		public static LocalizedString Manual
		{
			get
			{
				return new LocalizedString("Manual", "ExF5288D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BAC RID: 11180
		// (get) Token: 0x060079DF RID: 31199 RVA: 0x00197EEF File Offset: 0x001960EF
		public static LocalizedString ErrorAcceptedDomainCannotContainWildcardAndNegoConfig
		{
			get
			{
				return new LocalizedString("ErrorAcceptedDomainCannotContainWildcardAndNegoConfig", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BAD RID: 11181
		// (get) Token: 0x060079E0 RID: 31200 RVA: 0x00197F0D File Offset: 0x0019610D
		public static LocalizedString UniversalSecurityGroupRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("UniversalSecurityGroupRecipientTypeDetails", "Ex4721BC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079E1 RID: 31201 RVA: 0x00197F2C File Offset: 0x0019612C
		public static LocalizedString ExceptionADInvalidHandleCookie(string server, string message)
		{
			return new LocalizedString("ExceptionADInvalidHandleCookie", "Ex6086AE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				message
			});
		}

		// Token: 0x17002BAE RID: 11182
		// (get) Token: 0x060079E2 RID: 31202 RVA: 0x00197F5F File Offset: 0x0019615F
		public static LocalizedString ArbitrationMailboxTypeDetails
		{
			get
			{
				return new LocalizedString("ArbitrationMailboxTypeDetails", "Ex65DB4F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079E3 RID: 31203 RVA: 0x00197F80 File Offset: 0x00196180
		public static LocalizedString ErrorAdfsTrustedIssuerFormat(string thumbprintString)
		{
			return new LocalizedString("ErrorAdfsTrustedIssuerFormat", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				thumbprintString
			});
		}

		// Token: 0x17002BAF RID: 11183
		// (get) Token: 0x060079E4 RID: 31204 RVA: 0x00197FAF File Offset: 0x001961AF
		public static LocalizedString CalendarAgeFilterAll
		{
			get
			{
				return new LocalizedString("CalendarAgeFilterAll", "Ex6DFFF0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079E5 RID: 31205 RVA: 0x00197FD0 File Offset: 0x001961D0
		public static LocalizedString KpkAccessProblem(string propertyName)
		{
			return new LocalizedString("KpkAccessProblem", "ExF59538", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x17002BB0 RID: 11184
		// (get) Token: 0x060079E6 RID: 31206 RVA: 0x00197FFF File Offset: 0x001961FF
		public static LocalizedString GroupNamingPolicyCompany
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCompany", "ExCA7DAE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BB1 RID: 11185
		// (get) Token: 0x060079E7 RID: 31207 RVA: 0x0019801D File Offset: 0x0019621D
		public static LocalizedString IndustryMining
		{
			get
			{
				return new LocalizedString("IndustryMining", "Ex7F4D0B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079E8 RID: 31208 RVA: 0x0019803C File Offset: 0x0019623C
		public static LocalizedString ApiDoesNotSupportInputFormatError(string cl, string member, string input)
		{
			return new LocalizedString("ApiDoesNotSupportInputFormatError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				cl,
				member,
				input
			});
		}

		// Token: 0x17002BB2 RID: 11186
		// (get) Token: 0x060079E9 RID: 31209 RVA: 0x00198073 File Offset: 0x00196273
		public static LocalizedString ServerRoleOSP
		{
			get
			{
				return new LocalizedString("ServerRoleOSP", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079EA RID: 31210 RVA: 0x00198094 File Offset: 0x00196294
		public static LocalizedString DomainAlreadyExistsInMserv(string domainName, int existingPartnerId, int localSitePartnerId)
		{
			return new LocalizedString("DomainAlreadyExistsInMserv", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				domainName,
				existingPartnerId,
				localSitePartnerId
			});
		}

		// Token: 0x17002BB3 RID: 11187
		// (get) Token: 0x060079EB RID: 31211 RVA: 0x001980D5 File Offset: 0x001962D5
		public static LocalizedString InvalidDirectoryConfiguration
		{
			get
			{
				return new LocalizedString("InvalidDirectoryConfiguration", "Ex6FE768", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BB4 RID: 11188
		// (get) Token: 0x060079EC RID: 31212 RVA: 0x001980F3 File Offset: 0x001962F3
		public static LocalizedString ErrorDDLReferral
		{
			get
			{
				return new LocalizedString("ErrorDDLReferral", "ExFBD693", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BB5 RID: 11189
		// (get) Token: 0x060079ED RID: 31213 RVA: 0x00198111 File Offset: 0x00196311
		public static LocalizedString LdapFilterErrorNoAttributeValue
		{
			get
			{
				return new LocalizedString("LdapFilterErrorNoAttributeValue", "ExFEA394", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BB6 RID: 11190
		// (get) Token: 0x060079EE RID: 31214 RVA: 0x0019812F File Offset: 0x0019632F
		public static LocalizedString ExternalEnrollment
		{
			get
			{
				return new LocalizedString("ExternalEnrollment", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BB7 RID: 11191
		// (get) Token: 0x060079EF RID: 31215 RVA: 0x0019814D File Offset: 0x0019634D
		public static LocalizedString ErrorTimeoutReadingSystemAddressListCache
		{
			get
			{
				return new LocalizedString("ErrorTimeoutReadingSystemAddressListCache", "Ex65877A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BB8 RID: 11192
		// (get) Token: 0x060079F0 RID: 31216 RVA: 0x0019816B File Offset: 0x0019636B
		public static LocalizedString CanRunDefaultUpdateState_NotSuspended
		{
			get
			{
				return new LocalizedString("CanRunDefaultUpdateState_NotSuspended", "ExDE60CF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BB9 RID: 11193
		// (get) Token: 0x060079F1 RID: 31217 RVA: 0x00198189 File Offset: 0x00196389
		public static LocalizedString PreferredInternetCodePageSio2022Jp
		{
			get
			{
				return new LocalizedString("PreferredInternetCodePageSio2022Jp", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079F2 RID: 31218 RVA: 0x001981A8 File Offset: 0x001963A8
		public static LocalizedString CannotResolveTenantName(string name)
		{
			return new LocalizedString("CannotResolveTenantName", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17002BBA RID: 11194
		// (get) Token: 0x060079F3 RID: 31219 RVA: 0x001981D7 File Offset: 0x001963D7
		public static LocalizedString HtmlAndTextAlternative
		{
			get
			{
				return new LocalizedString("HtmlAndTextAlternative", "Ex74CC7A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BBB RID: 11195
		// (get) Token: 0x060079F4 RID: 31220 RVA: 0x001981F5 File Offset: 0x001963F5
		public static LocalizedString GlobalAddressList
		{
			get
			{
				return new LocalizedString("GlobalAddressList", "Ex29C85E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079F5 RID: 31221 RVA: 0x00198214 File Offset: 0x00196414
		public static LocalizedString ErrorInvalidPrivateCertificate(string thumbprint)
		{
			return new LocalizedString("ErrorInvalidPrivateCertificate", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				thumbprint
			});
		}

		// Token: 0x17002BBC RID: 11196
		// (get) Token: 0x060079F6 RID: 31222 RVA: 0x00198243 File Offset: 0x00196443
		public static LocalizedString MailTipsAccessLevelNone
		{
			get
			{
				return new LocalizedString("MailTipsAccessLevelNone", "Ex41AD69", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079F7 RID: 31223 RVA: 0x00198264 File Offset: 0x00196464
		public static LocalizedString ExceptionSearchRootNotChildOfDefaultScope(string child, string scope)
		{
			return new LocalizedString("ExceptionSearchRootNotChildOfDefaultScope", "Ex5C4FAD", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				child,
				scope
			});
		}

		// Token: 0x17002BBD RID: 11197
		// (get) Token: 0x060079F8 RID: 31224 RVA: 0x00198297 File Offset: 0x00196497
		public static LocalizedString EsnLangGalician
		{
			get
			{
				return new LocalizedString("EsnLangGalician", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079F9 RID: 31225 RVA: 0x001982B8 File Offset: 0x001964B8
		public static LocalizedString ErrorLegacyVersionOfflineAddressBookWithoutPublicFolderDatabase(string name)
		{
			return new LocalizedString("ErrorLegacyVersionOfflineAddressBookWithoutPublicFolderDatabase", "Ex7756D3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060079FA RID: 31226 RVA: 0x001982E8 File Offset: 0x001964E8
		public static LocalizedString DefaultDatabaseAvailabilityGroupContainerNotFoundException(string agName)
		{
			return new LocalizedString("DefaultDatabaseAvailabilityGroupContainerNotFoundException", "Ex48CEEB", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				agName
			});
		}

		// Token: 0x060079FB RID: 31227 RVA: 0x00198318 File Offset: 0x00196518
		public static LocalizedString ErrorSettingOverrideInvalidSectionName(string componentName, string sectionName, string availableObjects)
		{
			return new LocalizedString("ErrorSettingOverrideInvalidSectionName", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				componentName,
				sectionName,
				availableObjects
			});
		}

		// Token: 0x17002BBE RID: 11198
		// (get) Token: 0x060079FC RID: 31228 RVA: 0x0019834F File Offset: 0x0019654F
		public static LocalizedString ServerRoleFrontendTransport
		{
			get
			{
				return new LocalizedString("ServerRoleFrontendTransport", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BBF RID: 11199
		// (get) Token: 0x060079FD RID: 31229 RVA: 0x0019836D File Offset: 0x0019656D
		public static LocalizedString Exchange2009
		{
			get
			{
				return new LocalizedString("Exchange2009", "Ex1B4BBA", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BC0 RID: 11200
		// (get) Token: 0x060079FE RID: 31230 RVA: 0x0019838B File Offset: 0x0019658B
		public static LocalizedString TransientMservErrorDescription
		{
			get
			{
				return new LocalizedString("TransientMservErrorDescription", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060079FF RID: 31231 RVA: 0x001983AC File Offset: 0x001965AC
		public static LocalizedString ErrorSubnetMaskGreaterThanAddress(int maskBits, string address)
		{
			return new LocalizedString("ErrorSubnetMaskGreaterThanAddress", "ExB8D1BD", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				maskBits,
				address
			});
		}

		// Token: 0x17002BC1 RID: 11201
		// (get) Token: 0x06007A00 RID: 31232 RVA: 0x001983E4 File Offset: 0x001965E4
		public static LocalizedString ReceiveAuthMechanismExchangeServer
		{
			get
			{
				return new LocalizedString("ReceiveAuthMechanismExchangeServer", "Ex27D615", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BC2 RID: 11202
		// (get) Token: 0x06007A01 RID: 31233 RVA: 0x00198402 File Offset: 0x00196602
		public static LocalizedString Watsons
		{
			get
			{
				return new LocalizedString("Watsons", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BC3 RID: 11203
		// (get) Token: 0x06007A02 RID: 31234 RVA: 0x00198420 File Offset: 0x00196620
		public static LocalizedString OrganizationCapabilityPstProvider
		{
			get
			{
				return new LocalizedString("OrganizationCapabilityPstProvider", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A03 RID: 31235 RVA: 0x00198440 File Offset: 0x00196640
		public static LocalizedString InvalidServiceInstanceIdException(string serviceInstanceId)
		{
			return new LocalizedString("InvalidServiceInstanceIdException", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				serviceInstanceId
			});
		}

		// Token: 0x17002BC4 RID: 11204
		// (get) Token: 0x06007A04 RID: 31236 RVA: 0x0019846F File Offset: 0x0019666F
		public static LocalizedString ErrorCapabilityNone
		{
			get
			{
				return new LocalizedString("ErrorCapabilityNone", "Ex522BFE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BC5 RID: 11205
		// (get) Token: 0x06007A05 RID: 31237 RVA: 0x0019848D File Offset: 0x0019668D
		public static LocalizedString ExceptionAllDomainControllersUnavailable
		{
			get
			{
				return new LocalizedString("ExceptionAllDomainControllersUnavailable", "Ex6D4173", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A06 RID: 31238 RVA: 0x001984AC File Offset: 0x001966AC
		public static LocalizedString EndpointContainerNotFoundException(string endpointName)
		{
			return new LocalizedString("EndpointContainerNotFoundException", "ExE3CF8F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				endpointName
			});
		}

		// Token: 0x06007A07 RID: 31239 RVA: 0x001984DC File Offset: 0x001966DC
		public static LocalizedString ErrorIsServerSuitableRODC(string dcName)
		{
			return new LocalizedString("ErrorIsServerSuitableRODC", "ExDF5A44", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				dcName
			});
		}

		// Token: 0x17002BC6 RID: 11206
		// (get) Token: 0x06007A08 RID: 31240 RVA: 0x0019850B File Offset: 0x0019670B
		public static LocalizedString ServersContainerNotFoundException
		{
			get
			{
				return new LocalizedString("ServersContainerNotFoundException", "Ex5CC4E4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BC7 RID: 11207
		// (get) Token: 0x06007A09 RID: 31241 RVA: 0x00198529 File Offset: 0x00196729
		public static LocalizedString MailboxMoveStatusCompletionInProgress
		{
			get
			{
				return new LocalizedString("MailboxMoveStatusCompletionInProgress", "ExAEA738", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BC8 RID: 11208
		// (get) Token: 0x06007A0A RID: 31242 RVA: 0x00198547 File Offset: 0x00196747
		public static LocalizedString ServerRoleMailbox
		{
			get
			{
				return new LocalizedString("ServerRoleMailbox", "Ex333797", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BC9 RID: 11209
		// (get) Token: 0x06007A0B RID: 31243 RVA: 0x00198565 File Offset: 0x00196765
		public static LocalizedString ErrorResourceTypeMissing
		{
			get
			{
				return new LocalizedString("ErrorResourceTypeMissing", "Ex2E99DA", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A0C RID: 31244 RVA: 0x00198584 File Offset: 0x00196784
		public static LocalizedString TransientMservError(string message)
		{
			return new LocalizedString("TransientMservError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17002BCA RID: 11210
		// (get) Token: 0x06007A0D RID: 31245 RVA: 0x001985B3 File Offset: 0x001967B3
		public static LocalizedString Contacts
		{
			get
			{
				return new LocalizedString("Contacts", "ExE0B278", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BCB RID: 11211
		// (get) Token: 0x06007A0E RID: 31246 RVA: 0x001985D1 File Offset: 0x001967D1
		public static LocalizedString SendAuthMechanismTls
		{
			get
			{
				return new LocalizedString("SendAuthMechanismTls", "Ex200B5B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A0F RID: 31247 RVA: 0x001985F0 File Offset: 0x001967F0
		public static LocalizedString ExceptionResourceUnhealthy(ResourceKey key)
		{
			return new LocalizedString("ExceptionResourceUnhealthy", "ExCE9BDB", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				key
			});
		}

		// Token: 0x17002BCC RID: 11212
		// (get) Token: 0x06007A10 RID: 31248 RVA: 0x0019861F File Offset: 0x0019681F
		public static LocalizedString AggregatedSessionCannotMakeMbxChanges
		{
			get
			{
				return new LocalizedString("AggregatedSessionCannotMakeMbxChanges", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BCD RID: 11213
		// (get) Token: 0x06007A11 RID: 31249 RVA: 0x0019863D File Offset: 0x0019683D
		public static LocalizedString PAAEnabled
		{
			get
			{
				return new LocalizedString("PAAEnabled", "Ex778C25", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BCE RID: 11214
		// (get) Token: 0x06007A12 RID: 31250 RVA: 0x0019865B File Offset: 0x0019685B
		public static LocalizedString NonPartner
		{
			get
			{
				return new LocalizedString("NonPartner", "ExEA93E7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BCF RID: 11215
		// (get) Token: 0x06007A13 RID: 31251 RVA: 0x00198679 File Offset: 0x00196879
		public static LocalizedString BasicAfterTLSWithoutBasic
		{
			get
			{
				return new LocalizedString("BasicAfterTLSWithoutBasic", "Ex134A43", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BD0 RID: 11216
		// (get) Token: 0x06007A14 RID: 31252 RVA: 0x00198697 File Offset: 0x00196897
		public static LocalizedString ErrorSharedConfigurationBothRoles
		{
			get
			{
				return new LocalizedString("ErrorSharedConfigurationBothRoles", "Ex4E9CD0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BD1 RID: 11217
		// (get) Token: 0x06007A15 RID: 31253 RVA: 0x001986B5 File Offset: 0x001968B5
		public static LocalizedString EsnLangDutch
		{
			get
			{
				return new LocalizedString("EsnLangDutch", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A16 RID: 31254 RVA: 0x001986D4 File Offset: 0x001968D4
		public static LocalizedString CannotGetComputerName(string error)
		{
			return new LocalizedString("CannotGetComputerName", "Ex036C6E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06007A17 RID: 31255 RVA: 0x00198704 File Offset: 0x00196904
		public static LocalizedString ProviderBadImpageFormatLoadException(string providerName, string assemblyPath)
		{
			return new LocalizedString("ProviderBadImpageFormatLoadException", "ExC1314A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				providerName,
				assemblyPath
			});
		}

		// Token: 0x06007A18 RID: 31256 RVA: 0x00198738 File Offset: 0x00196938
		public static LocalizedString ConfigurationSettingsOrganizationNotFound(string id)
		{
			return new LocalizedString("ConfigurationSettingsOrganizationNotFound", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06007A19 RID: 31257 RVA: 0x00198768 File Offset: 0x00196968
		public static LocalizedString CannotFindTenantByMSAUserNetID(string msaUserNetID)
		{
			return new LocalizedString("CannotFindTenantByMSAUserNetID", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				msaUserNetID
			});
		}

		// Token: 0x17002BD2 RID: 11218
		// (get) Token: 0x06007A1A RID: 31258 RVA: 0x00198797 File Offset: 0x00196997
		public static LocalizedString DsnLanguageNotSupportedForCustomization
		{
			get
			{
				return new LocalizedString("DsnLanguageNotSupportedForCustomization", "ExA50363", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A1B RID: 31259 RVA: 0x001987B8 File Offset: 0x001969B8
		public static LocalizedString RecordValueFormatChange(string key, string oldValue)
		{
			return new LocalizedString("RecordValueFormatChange", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				key,
				oldValue
			});
		}

		// Token: 0x17002BD3 RID: 11219
		// (get) Token: 0x06007A1C RID: 31260 RVA: 0x001987EB File Offset: 0x001969EB
		public static LocalizedString IndustryNotSpecified
		{
			get
			{
				return new LocalizedString("IndustryNotSpecified", "Ex735023", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BD4 RID: 11220
		// (get) Token: 0x06007A1D RID: 31261 RVA: 0x00198809 File Offset: 0x00196A09
		public static LocalizedString ErrorDDLFilterError
		{
			get
			{
				return new LocalizedString("ErrorDDLFilterError", "Ex1451DD", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BD5 RID: 11221
		// (get) Token: 0x06007A1E RID: 31262 RVA: 0x00198827 File Offset: 0x00196A27
		public static LocalizedString AddressList
		{
			get
			{
				return new LocalizedString("AddressList", "Ex2492E9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A1F RID: 31263 RVA: 0x00198848 File Offset: 0x00196A48
		public static LocalizedString LitigationHold_License_Violation(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("LitigationHold_License_Violation", "Ex163984", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x06007A20 RID: 31264 RVA: 0x00198884 File Offset: 0x00196A84
		public static LocalizedString ErrorInvalidISOCountryCode(int countrycode)
		{
			return new LocalizedString("ErrorInvalidISOCountryCode", "Ex0FBE7B", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				countrycode
			});
		}

		// Token: 0x17002BD6 RID: 11222
		// (get) Token: 0x06007A21 RID: 31265 RVA: 0x001988B8 File Offset: 0x00196AB8
		public static LocalizedString MustDisplayComment
		{
			get
			{
				return new LocalizedString("MustDisplayComment", "Ex4D13AB", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BD7 RID: 11223
		// (get) Token: 0x06007A22 RID: 31266 RVA: 0x001988D6 File Offset: 0x00196AD6
		public static LocalizedString ServerRoleFfoWebServices
		{
			get
			{
				return new LocalizedString("ServerRoleFfoWebServices", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BD8 RID: 11224
		// (get) Token: 0x06007A23 RID: 31267 RVA: 0x001988F4 File Offset: 0x00196AF4
		public static LocalizedString ServerRoleClientAccess
		{
			get
			{
				return new LocalizedString("ServerRoleClientAccess", "Ex8578F6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A24 RID: 31268 RVA: 0x00198914 File Offset: 0x00196B14
		public static LocalizedString ErrorServiceEndpointNotFound(string serviceEndpointName)
		{
			return new LocalizedString("ErrorServiceEndpointNotFound", "ExC90781", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				serviceEndpointName
			});
		}

		// Token: 0x17002BD9 RID: 11225
		// (get) Token: 0x06007A25 RID: 31269 RVA: 0x00198943 File Offset: 0x00196B43
		public static LocalizedString SKUCapabilityBPOSSEnterprise
		{
			get
			{
				return new LocalizedString("SKUCapabilityBPOSSEnterprise", "Ex95491D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BDA RID: 11226
		// (get) Token: 0x06007A26 RID: 31270 RVA: 0x00198961 File Offset: 0x00196B61
		public static LocalizedString InvalidReceiveAuthModeExternalOnly
		{
			get
			{
				return new LocalizedString("InvalidReceiveAuthModeExternalOnly", "ExC88E24", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BDB RID: 11227
		// (get) Token: 0x06007A27 RID: 31271 RVA: 0x0019897F File Offset: 0x00196B7F
		public static LocalizedString ErrorSettingOverrideNull
		{
			get
			{
				return new LocalizedString("ErrorSettingOverrideNull", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A28 RID: 31272 RVA: 0x001989A0 File Offset: 0x00196BA0
		public static LocalizedString ErrorLinkedADObjectNotInSameOrganization(string propertyName, string propertyValue, string objectId, string orgId)
		{
			return new LocalizedString("ErrorLinkedADObjectNotInSameOrganization", "Ex4BCBE7", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				propertyValue,
				objectId,
				orgId
			});
		}

		// Token: 0x17002BDC RID: 11228
		// (get) Token: 0x06007A29 RID: 31273 RVA: 0x001989DB File Offset: 0x00196BDB
		public static LocalizedString LdapFilterErrorQueryTooLong
		{
			get
			{
				return new LocalizedString("LdapFilterErrorQueryTooLong", "ExB5260B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A2A RID: 31274 RVA: 0x001989FC File Offset: 0x00196BFC
		public static LocalizedString ExceptionProxyGeneratorDLLFailed(string server)
		{
			return new LocalizedString("ExceptionProxyGeneratorDLLFailed", "ExCF6D75", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06007A2B RID: 31275 RVA: 0x00198A2C File Offset: 0x00196C2C
		public static LocalizedString BPOS_License_NumericLimitViolation(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("BPOS_License_NumericLimitViolation", "Ex4B5FC3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x06007A2C RID: 31276 RVA: 0x00198A68 File Offset: 0x00196C68
		public static LocalizedString SessionSubscriptionDisabled(Guid guid)
		{
			return new LocalizedString("SessionSubscriptionDisabled", "Ex2F34FE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x17002BDD RID: 11229
		// (get) Token: 0x06007A2D RID: 31277 RVA: 0x00198A9C File Offset: 0x00196C9C
		public static LocalizedString ErrorMoveToDestinationFolderNotDefined
		{
			get
			{
				return new LocalizedString("ErrorMoveToDestinationFolderNotDefined", "Ex89E555", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A2E RID: 31278 RVA: 0x00198ABC File Offset: 0x00196CBC
		public static LocalizedString InvalidControlTextLength(int maxLength)
		{
			return new LocalizedString("InvalidControlTextLength", "ExDD8CAA", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				maxLength
			});
		}

		// Token: 0x06007A2F RID: 31279 RVA: 0x00198AF0 File Offset: 0x00196CF0
		public static LocalizedString RelocationInProgress(string tenantName, string permError, string suspened, string autoCompletion, string currentState, string requestedState)
		{
			return new LocalizedString("RelocationInProgress", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				tenantName,
				permError,
				suspened,
				autoCompletion,
				currentState,
				requestedState
			});
		}

		// Token: 0x17002BDE RID: 11230
		// (get) Token: 0x06007A30 RID: 31280 RVA: 0x00198B35 File Offset: 0x00196D35
		public static LocalizedString MailboxMoveStatusInProgress
		{
			get
			{
				return new LocalizedString("MailboxMoveStatusInProgress", "Ex9285EC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BDF RID: 11231
		// (get) Token: 0x06007A31 RID: 31281 RVA: 0x00198B53 File Offset: 0x00196D53
		public static LocalizedString SecurityPrincipalTypeGroup
		{
			get
			{
				return new LocalizedString("SecurityPrincipalTypeGroup", "ExE9ADC8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BE0 RID: 11232
		// (get) Token: 0x06007A32 RID: 31282 RVA: 0x00198B71 File Offset: 0x00196D71
		public static LocalizedString X400Authoritative
		{
			get
			{
				return new LocalizedString("X400Authoritative", "Ex090621", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BE1 RID: 11233
		// (get) Token: 0x06007A33 RID: 31283 RVA: 0x00198B8F File Offset: 0x00196D8F
		public static LocalizedString MailFlowPartnerInternalMailContentTypeMimeHtml
		{
			get
			{
				return new LocalizedString("MailFlowPartnerInternalMailContentTypeMimeHtml", "Ex39B525", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BE2 RID: 11234
		// (get) Token: 0x06007A34 RID: 31284 RVA: 0x00198BAD File Offset: 0x00196DAD
		public static LocalizedString MailEnabledUserRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailEnabledUserRecipientTypeDetails", "ExC65CDF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A35 RID: 31285 RVA: 0x00198BCC File Offset: 0x00196DCC
		public static LocalizedString ExceptionFailedToRebuildConnection(string serverName)
		{
			return new LocalizedString("ExceptionFailedToRebuildConnection", "Ex6ED5CE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06007A36 RID: 31286 RVA: 0x00198BFC File Offset: 0x00196DFC
		public static LocalizedString RootCannotBeEmpty(ScopeRestrictionType scopeType)
		{
			return new LocalizedString("RootCannotBeEmpty", "ExD5FE2F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scopeType
			});
		}

		// Token: 0x17002BE3 RID: 11235
		// (get) Token: 0x06007A37 RID: 31287 RVA: 0x00198C30 File Offset: 0x00196E30
		public static LocalizedString ExtensionNull
		{
			get
			{
				return new LocalizedString("ExtensionNull", "Ex5D1856", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A38 RID: 31288 RVA: 0x00198C50 File Offset: 0x00196E50
		public static LocalizedString TenantIsArrivingException(string dn)
		{
			return new LocalizedString("TenantIsArrivingException", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				dn
			});
		}

		// Token: 0x06007A39 RID: 31289 RVA: 0x00198C80 File Offset: 0x00196E80
		public static LocalizedString TenantPerimeterConfigSettingsNotFoundException(string ordId)
		{
			return new LocalizedString("TenantPerimeterConfigSettingsNotFoundException", "Ex25E7EF", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				ordId
			});
		}

		// Token: 0x17002BE4 RID: 11236
		// (get) Token: 0x06007A3A RID: 31290 RVA: 0x00198CAF File Offset: 0x00196EAF
		public static LocalizedString Unsecured
		{
			get
			{
				return new LocalizedString("Unsecured", "ExB84289", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BE5 RID: 11237
		// (get) Token: 0x06007A3B RID: 31291 RVA: 0x00198CCD File Offset: 0x00196ECD
		public static LocalizedString ConnectorIdIsNotAnInteger
		{
			get
			{
				return new LocalizedString("ConnectorIdIsNotAnInteger", "Ex2FB9D0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BE6 RID: 11238
		// (get) Token: 0x06007A3C RID: 31292 RVA: 0x00198CEB File Offset: 0x00196EEB
		public static LocalizedString ErrorMissingPrimaryUM
		{
			get
			{
				return new LocalizedString("ErrorMissingPrimaryUM", "Ex7681E6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BE7 RID: 11239
		// (get) Token: 0x06007A3D RID: 31293 RVA: 0x00198D09 File Offset: 0x00196F09
		public static LocalizedString CannotDetermineDataSessionType
		{
			get
			{
				return new LocalizedString("CannotDetermineDataSessionType", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A3E RID: 31294 RVA: 0x00198D28 File Offset: 0x00196F28
		public static LocalizedString ConfigScopeCannotBeEmpty(ConfigWriteScopeType scopeType)
		{
			return new LocalizedString("ConfigScopeCannotBeEmpty", "ExE74C4B", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scopeType
			});
		}

		// Token: 0x17002BE8 RID: 11240
		// (get) Token: 0x06007A3F RID: 31295 RVA: 0x00198D5C File Offset: 0x00196F5C
		public static LocalizedString UserAgentsChanges
		{
			get
			{
				return new LocalizedString("UserAgentsChanges", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A40 RID: 31296 RVA: 0x00198D7C File Offset: 0x00196F7C
		public static LocalizedString InvalidDNFormat(string str)
		{
			return new LocalizedString("InvalidDNFormat", "Ex786089", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				str
			});
		}

		// Token: 0x17002BE9 RID: 11241
		// (get) Token: 0x06007A41 RID: 31297 RVA: 0x00198DAB File Offset: 0x00196FAB
		public static LocalizedString Notes
		{
			get
			{
				return new LocalizedString("Notes", "Ex96E8FC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BEA RID: 11242
		// (get) Token: 0x06007A42 RID: 31298 RVA: 0x00198DC9 File Offset: 0x00196FC9
		public static LocalizedString EsnLangTelugu
		{
			get
			{
				return new LocalizedString("EsnLangTelugu", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A43 RID: 31299 RVA: 0x00198DE8 File Offset: 0x00196FE8
		public static LocalizedString ErrorTransitionCounterHasDuplicateEntry(string transitiontype)
		{
			return new LocalizedString("ErrorTransitionCounterHasDuplicateEntry", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				transitiontype
			});
		}

		// Token: 0x06007A44 RID: 31300 RVA: 0x00198E18 File Offset: 0x00197018
		public static LocalizedString ExceptionADOperationFailedRemoveContainer(string server, string dn)
		{
			return new LocalizedString("ExceptionADOperationFailedRemoveContainer", "ExB26DF4", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				dn
			});
		}

		// Token: 0x17002BEB RID: 11243
		// (get) Token: 0x06007A45 RID: 31301 RVA: 0x00198E4B File Offset: 0x0019704B
		public static LocalizedString GroupNamingPolicyExtensionCustomAttribute1
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyExtensionCustomAttribute1", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BEC RID: 11244
		// (get) Token: 0x06007A46 RID: 31302 RVA: 0x00198E69 File Offset: 0x00197069
		public static LocalizedString MailFlowPartnerInternalMailContentTypeNone
		{
			get
			{
				return new LocalizedString("MailFlowPartnerInternalMailContentTypeNone", "Ex63CA8D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A47 RID: 31303 RVA: 0x00198E88 File Offset: 0x00197088
		public static LocalizedString RecipientWriteScopeNotLessThan(string leftScopeType, string rightScopeType)
		{
			return new LocalizedString("RecipientWriteScopeNotLessThan", "Ex3755BA", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				leftScopeType,
				rightScopeType
			});
		}

		// Token: 0x17002BED RID: 11245
		// (get) Token: 0x06007A48 RID: 31304 RVA: 0x00198EBB File Offset: 0x001970BB
		public static LocalizedString DefaultRapName
		{
			get
			{
				return new LocalizedString("DefaultRapName", "Ex3B46EB", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BEE RID: 11246
		// (get) Token: 0x06007A49 RID: 31305 RVA: 0x00198ED9 File Offset: 0x001970D9
		public static LocalizedString DeleteUseDefaultAlert
		{
			get
			{
				return new LocalizedString("DeleteUseDefaultAlert", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A4A RID: 31306 RVA: 0x00198EF8 File Offset: 0x001970F8
		public static LocalizedString CannotResolveTenantNameByAcceptedDomain(string domain)
		{
			return new LocalizedString("CannotResolveTenantNameByAcceptedDomain", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x17002BEF RID: 11247
		// (get) Token: 0x06007A4B RID: 31307 RVA: 0x00198F27 File Offset: 0x00197127
		public static LocalizedString ErrorOrganizationResourceAddressListsCount
		{
			get
			{
				return new LocalizedString("ErrorOrganizationResourceAddressListsCount", "ExB6E71B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A4C RID: 31308 RVA: 0x00198F48 File Offset: 0x00197148
		public static LocalizedString ExceptionSearchRootChildDomain(string childDomain, string scopeDomain)
		{
			return new LocalizedString("ExceptionSearchRootChildDomain", "ExA95B78", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				childDomain,
				scopeDomain
			});
		}

		// Token: 0x17002BF0 RID: 11248
		// (get) Token: 0x06007A4D RID: 31309 RVA: 0x00198F7B File Offset: 0x0019717B
		public static LocalizedString EsnLangChineseSimplified
		{
			get
			{
				return new LocalizedString("EsnLangChineseSimplified", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BF1 RID: 11249
		// (get) Token: 0x06007A4E RID: 31310 RVA: 0x00198F99 File Offset: 0x00197199
		public static LocalizedString ConferenceRoomMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("ConferenceRoomMailboxRecipientTypeDetails", "Ex6F3AB9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A4F RID: 31311 RVA: 0x00198FB8 File Offset: 0x001971B8
		public static LocalizedString ErrorRemovedMailboxDoesNotHaveDatabase(string id)
		{
			return new LocalizedString("ErrorRemovedMailboxDoesNotHaveDatabase", "Ex22BDC8", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002BF2 RID: 11250
		// (get) Token: 0x06007A50 RID: 31312 RVA: 0x00198FE7 File Offset: 0x001971E7
		public static LocalizedString BlockedOutlookClientVersionPatternDescription
		{
			get
			{
				return new LocalizedString("BlockedOutlookClientVersionPatternDescription", "ExCCE8F7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BF3 RID: 11251
		// (get) Token: 0x06007A51 RID: 31313 RVA: 0x00199005 File Offset: 0x00197205
		public static LocalizedString UserHasNoSmtpProxyAddressWithFederatedDomain
		{
			get
			{
				return new LocalizedString("UserHasNoSmtpProxyAddressWithFederatedDomain", "Ex9D7C34", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BF4 RID: 11252
		// (get) Token: 0x06007A52 RID: 31314 RVA: 0x00199023 File Offset: 0x00197223
		public static LocalizedString OrganizationCapabilityMailRouting
		{
			get
			{
				return new LocalizedString("OrganizationCapabilityMailRouting", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BF5 RID: 11253
		// (get) Token: 0x06007A53 RID: 31315 RVA: 0x00199041 File Offset: 0x00197241
		public static LocalizedString SKUCapabilityBPOSSStandard
		{
			get
			{
				return new LocalizedString("SKUCapabilityBPOSSStandard", "ExAE0C69", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BF6 RID: 11254
		// (get) Token: 0x06007A54 RID: 31316 RVA: 0x0019905F File Offset: 0x0019725F
		public static LocalizedString SystemMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("SystemMailboxRecipientTypeDetails", "ExBBEBE9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BF7 RID: 11255
		// (get) Token: 0x06007A55 RID: 31317 RVA: 0x0019907D File Offset: 0x0019727D
		public static LocalizedString ExceptionADTopologyNoLocalDomain
		{
			get
			{
				return new LocalizedString("ExceptionADTopologyNoLocalDomain", "Ex2D2F44", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A56 RID: 31318 RVA: 0x0019909C File Offset: 0x0019729C
		public static LocalizedString AlternateServiceAccountCredentialDisplayFormat(string isInvalid, DateTime timeStamp, string domain, string userName)
		{
			return new LocalizedString("AlternateServiceAccountCredentialDisplayFormat", "Ex494D05", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				isInvalid,
				timeStamp,
				domain,
				userName
			});
		}

		// Token: 0x06007A57 RID: 31319 RVA: 0x001990DC File Offset: 0x001972DC
		public static LocalizedString ErrorCannotSaveBecauseTooNew(ExchangeObjectVersion objectVersion, ExchangeObjectVersion currentVersion)
		{
			return new LocalizedString("ErrorCannotSaveBecauseTooNew", "Ex3E755D", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				objectVersion,
				currentVersion
			});
		}

		// Token: 0x06007A58 RID: 31320 RVA: 0x00199110 File Offset: 0x00197310
		public static LocalizedString InvalidRootDse(string server)
		{
			return new LocalizedString("InvalidRootDse", "Ex8737DC", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06007A59 RID: 31321 RVA: 0x00199140 File Offset: 0x00197340
		public static LocalizedString ExceptionCannotAddSidHistory(string srcObj, string srcDom, string dstObj, string dstDom, string errorCode)
		{
			return new LocalizedString("ExceptionCannotAddSidHistory", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				srcObj,
				srcDom,
				dstObj,
				dstDom,
				errorCode
			});
		}

		// Token: 0x06007A5A RID: 31322 RVA: 0x00199180 File Offset: 0x00197380
		public static LocalizedString ErrorInvalidServerFqdn(string fqdn, string hostName)
		{
			return new LocalizedString("ErrorInvalidServerFqdn", "Ex0E7BD0", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				fqdn,
				hostName
			});
		}

		// Token: 0x06007A5B RID: 31323 RVA: 0x001991B4 File Offset: 0x001973B4
		public static LocalizedString ExceptionSearchRootNotChildOfSessionSearchRoot(string child, string scope)
		{
			return new LocalizedString("ExceptionSearchRootNotChildOfSessionSearchRoot", "Ex6EC18C", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				child,
				scope
			});
		}

		// Token: 0x17002BF8 RID: 11256
		// (get) Token: 0x06007A5C RID: 31324 RVA: 0x001991E7 File Offset: 0x001973E7
		public static LocalizedString EsnLangDanish
		{
			get
			{
				return new LocalizedString("EsnLangDanish", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A5D RID: 31325 RVA: 0x00199208 File Offset: 0x00197408
		public static LocalizedString ErrorRemovedMailboxDoesNotHaveMailboxGuid(string id)
		{
			return new LocalizedString("ErrorRemovedMailboxDoesNotHaveMailboxGuid", "ExBCF5E3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002BF9 RID: 11257
		// (get) Token: 0x06007A5E RID: 31326 RVA: 0x00199237 File Offset: 0x00197437
		public static LocalizedString IndustryRetail
		{
			get
			{
				return new LocalizedString("IndustryRetail", "ExF465FC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BFA RID: 11258
		// (get) Token: 0x06007A5F RID: 31327 RVA: 0x00199255 File Offset: 0x00197455
		public static LocalizedString ErrorDDLNoSuchObject
		{
			get
			{
				return new LocalizedString("ErrorDDLNoSuchObject", "Ex232B18", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BFB RID: 11259
		// (get) Token: 0x06007A60 RID: 31328 RVA: 0x00199273 File Offset: 0x00197473
		public static LocalizedString IndustryComputerRelatedProductsServices
		{
			get
			{
				return new LocalizedString("IndustryComputerRelatedProductsServices", "ExA4D963", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BFC RID: 11260
		// (get) Token: 0x06007A61 RID: 31329 RVA: 0x00199291 File Offset: 0x00197491
		public static LocalizedString InternalRelay
		{
			get
			{
				return new LocalizedString("InternalRelay", "Ex7DAA91", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BFD RID: 11261
		// (get) Token: 0x06007A62 RID: 31330 RVA: 0x001992AF File Offset: 0x001974AF
		public static LocalizedString ErrorEmptyArchiveName
		{
			get
			{
				return new LocalizedString("ErrorEmptyArchiveName", "ExDE90E9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BFE RID: 11262
		// (get) Token: 0x06007A63 RID: 31331 RVA: 0x001992CD File Offset: 0x001974CD
		public static LocalizedString EmailAddressPolicyPriorityLowest
		{
			get
			{
				return new LocalizedString("EmailAddressPolicyPriorityLowest", "Ex4D5247", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002BFF RID: 11263
		// (get) Token: 0x06007A64 RID: 31332 RVA: 0x001992EB File Offset: 0x001974EB
		public static LocalizedString ExternalMdm
		{
			get
			{
				return new LocalizedString("ExternalMdm", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C00 RID: 11264
		// (get) Token: 0x06007A65 RID: 31333 RVA: 0x00199309 File Offset: 0x00197509
		public static LocalizedString TransportSettingsNotFoundException
		{
			get
			{
				return new LocalizedString("TransportSettingsNotFoundException", "ExD5A88D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C01 RID: 11265
		// (get) Token: 0x06007A66 RID: 31334 RVA: 0x00199327 File Offset: 0x00197527
		public static LocalizedString DomainSecureEnabledWithoutTls
		{
			get
			{
				return new LocalizedString("DomainSecureEnabledWithoutTls", "ExFAC5F7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C02 RID: 11266
		// (get) Token: 0x06007A67 RID: 31335 RVA: 0x00199345 File Offset: 0x00197545
		public static LocalizedString BccSuspiciousOutboundAdditionalRecipientsRequired
		{
			get
			{
				return new LocalizedString("BccSuspiciousOutboundAdditionalRecipientsRequired", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C03 RID: 11267
		// (get) Token: 0x06007A68 RID: 31336 RVA: 0x00199363 File Offset: 0x00197563
		public static LocalizedString NoRoleEntriesFound
		{
			get
			{
				return new LocalizedString("NoRoleEntriesFound", "Ex200BA5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C04 RID: 11268
		// (get) Token: 0x06007A69 RID: 31337 RVA: 0x00199381 File Offset: 0x00197581
		public static LocalizedString IndustryWholesale
		{
			get
			{
				return new LocalizedString("IndustryWholesale", "ExEAA6B6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A6A RID: 31338 RVA: 0x001993A0 File Offset: 0x001975A0
		public static LocalizedString ExceptionADTopologyServiceDown(string server, string serviceType, string error)
		{
			return new LocalizedString("ExceptionADTopologyServiceDown", "Ex33BDA3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				serviceType,
				error
			});
		}

		// Token: 0x06007A6B RID: 31339 RVA: 0x001993D8 File Offset: 0x001975D8
		public static LocalizedString CannotCalculatePropertyGeneric(string calculatedPropertyName)
		{
			return new LocalizedString("CannotCalculatePropertyGeneric", "Ex76E5F2", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				calculatedPropertyName
			});
		}

		// Token: 0x17002C05 RID: 11269
		// (get) Token: 0x06007A6C RID: 31340 RVA: 0x00199407 File Offset: 0x00197607
		public static LocalizedString ServerRoleCentralAdminFrontEnd
		{
			get
			{
				return new LocalizedString("ServerRoleCentralAdminFrontEnd", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A6D RID: 31341 RVA: 0x00199428 File Offset: 0x00197628
		public static LocalizedString ExceptionADConstraintViolation(string server, string errorMessage)
		{
			return new LocalizedString("ExceptionADConstraintViolation", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				errorMessage
			});
		}

		// Token: 0x06007A6E RID: 31342 RVA: 0x0019945C File Offset: 0x0019765C
		public static LocalizedString ADTreeDeleteNotFinishedException(string server)
		{
			return new LocalizedString("ADTreeDeleteNotFinishedException", "ExE3A815", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17002C06 RID: 11270
		// (get) Token: 0x06007A6F RID: 31343 RVA: 0x0019948B File Offset: 0x0019768B
		public static LocalizedString ErrorInvalidPushNotificationPlatform
		{
			get
			{
				return new LocalizedString("ErrorInvalidPushNotificationPlatform", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A70 RID: 31344 RVA: 0x001994AC File Offset: 0x001976AC
		public static LocalizedString InvalidAAConfiguration(string a, string b)
		{
			return new LocalizedString("InvalidAAConfiguration", "Ex7FDD1A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				a,
				b
			});
		}

		// Token: 0x17002C07 RID: 11271
		// (get) Token: 0x06007A71 RID: 31345 RVA: 0x001994DF File Offset: 0x001976DF
		public static LocalizedString MailTipsAccessLevelAll
		{
			get
			{
				return new LocalizedString("MailTipsAccessLevelAll", "ExF7A6C8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A72 RID: 31346 RVA: 0x00199500 File Offset: 0x00197700
		public static LocalizedString PermanentGlsError(string message)
		{
			return new LocalizedString("PermanentGlsError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x06007A73 RID: 31347 RVA: 0x00199530 File Offset: 0x00197730
		public static LocalizedString BPOS_S_Property_License_Violation(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("BPOS_S_Property_License_Violation", "Ex6F303E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x17002C08 RID: 11272
		// (get) Token: 0x06007A74 RID: 31348 RVA: 0x0019956B File Offset: 0x0019776B
		public static LocalizedString PublicFolderRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("PublicFolderRecipientTypeDetails", "Ex7D796A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C09 RID: 11273
		// (get) Token: 0x06007A75 RID: 31349 RVA: 0x00199589 File Offset: 0x00197789
		public static LocalizedString ValueNotAvailableForUnchangedProperty
		{
			get
			{
				return new LocalizedString("ValueNotAvailableForUnchangedProperty", "Ex50AAAB", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C0A RID: 11274
		// (get) Token: 0x06007A76 RID: 31350 RVA: 0x001995A7 File Offset: 0x001977A7
		public static LocalizedString DumpsterFolder
		{
			get
			{
				return new LocalizedString("DumpsterFolder", "ExC4D9A5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C0B RID: 11275
		// (get) Token: 0x06007A77 RID: 31351 RVA: 0x001995C5 File Offset: 0x001977C5
		public static LocalizedString CannotParseMimeTypes
		{
			get
			{
				return new LocalizedString("CannotParseMimeTypes", "ExC27AD8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A78 RID: 31352 RVA: 0x001995E4 File Offset: 0x001977E4
		public static LocalizedString CannotResolveTenantNameByExternalDirectoryId(string id)
		{
			return new LocalizedString("CannotResolveTenantNameByExternalDirectoryId", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06007A79 RID: 31353 RVA: 0x00199614 File Offset: 0x00197814
		public static LocalizedString BEVDirLockdown_Violation(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("BEVDirLockdown_Violation", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x17002C0C RID: 11276
		// (get) Token: 0x06007A7A RID: 31354 RVA: 0x0019964F File Offset: 0x0019784F
		public static LocalizedString ExclusiveRecipientScopes
		{
			get
			{
				return new LocalizedString("ExclusiveRecipientScopes", "Ex4E0C7D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A7B RID: 31355 RVA: 0x00199670 File Offset: 0x00197870
		public static LocalizedString ErrorProperty1EqProperty2(string property1Name, string property2Name, string propertyValue)
		{
			return new LocalizedString("ErrorProperty1EqProperty2", "Ex1CF833", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property1Name,
				property2Name,
				propertyValue
			});
		}

		// Token: 0x17002C0D RID: 11277
		// (get) Token: 0x06007A7C RID: 31356 RVA: 0x001996A7 File Offset: 0x001978A7
		public static LocalizedString QuarantineMailboxIsInvalid
		{
			get
			{
				return new LocalizedString("QuarantineMailboxIsInvalid", "ExE43D96", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A7D RID: 31357 RVA: 0x001996C8 File Offset: 0x001978C8
		public static LocalizedString ErrorNonUniqueSid(string sidString)
		{
			return new LocalizedString("ErrorNonUniqueSid", "Ex1BE998", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				sidString
			});
		}

		// Token: 0x06007A7E RID: 31358 RVA: 0x001996F8 File Offset: 0x001978F8
		public static LocalizedString ExceptionADTopologyErrorWhenLookingForGlobalCatalogsInForest(int error, string forest, string message)
		{
			return new LocalizedString("ExceptionADTopologyErrorWhenLookingForGlobalCatalogsInForest", "ExD96E4B", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				error,
				forest,
				message
			});
		}

		// Token: 0x06007A7F RID: 31359 RVA: 0x00199734 File Offset: 0x00197934
		public static LocalizedString ExceptionObjectPartitionDoesNotMatchSessionPartition(string dn, string fqdn)
		{
			return new LocalizedString("ExceptionObjectPartitionDoesNotMatchSessionPartition", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				dn,
				fqdn
			});
		}

		// Token: 0x06007A80 RID: 31360 RVA: 0x00199768 File Offset: 0x00197968
		public static LocalizedString ExceptionADTopologyHasNoServersInForest(string forest)
		{
			return new LocalizedString("ExceptionADTopologyHasNoServersInForest", "Ex075281", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				forest
			});
		}

		// Token: 0x17002C0E RID: 11278
		// (get) Token: 0x06007A81 RID: 31361 RVA: 0x00199797 File Offset: 0x00197997
		public static LocalizedString MailboxPlanTypeDetails
		{
			get
			{
				return new LocalizedString("MailboxPlanTypeDetails", "ExFF6044", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A82 RID: 31362 RVA: 0x001997B8 File Offset: 0x001979B8
		public static LocalizedString WACDiscoveryEndpointShouldBeAbsoluteUri(string actualValue)
		{
			return new LocalizedString("WACDiscoveryEndpointShouldBeAbsoluteUri", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				actualValue
			});
		}

		// Token: 0x17002C0F RID: 11279
		// (get) Token: 0x06007A83 RID: 31363 RVA: 0x001997E7 File Offset: 0x001979E7
		public static LocalizedString ServerRoleCafeArray
		{
			get
			{
				return new LocalizedString("ServerRoleCafeArray", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C10 RID: 11280
		// (get) Token: 0x06007A84 RID: 31364 RVA: 0x00199805 File Offset: 0x00197A05
		public static LocalizedString SendCredentialIsNull
		{
			get
			{
				return new LocalizedString("SendCredentialIsNull", "Ex5F3D40", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A85 RID: 31365 RVA: 0x00199824 File Offset: 0x00197A24
		public static LocalizedString MasteredOnPremiseCapabilityUndefinedTenantNotDirSyncing(string capability, string property)
		{
			return new LocalizedString("MasteredOnPremiseCapabilityUndefinedTenantNotDirSyncing", "ExC70267", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				capability,
				property
			});
		}

		// Token: 0x17002C11 RID: 11281
		// (get) Token: 0x06007A86 RID: 31366 RVA: 0x00199857 File Offset: 0x00197A57
		public static LocalizedString True
		{
			get
			{
				return new LocalizedString("True", "ExBC8412", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A87 RID: 31367 RVA: 0x00199878 File Offset: 0x00197A78
		public static LocalizedString CannotGetDnFromGuid(Guid guid)
		{
			return new LocalizedString("CannotGetDnFromGuid", "Ex2A9BDE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x06007A88 RID: 31368 RVA: 0x001998AC File Offset: 0x00197AAC
		public static LocalizedString ExceptionObjectHasBeenDeletedDuringCurrentOperation(string id)
		{
			return new LocalizedString("ExceptionObjectHasBeenDeletedDuringCurrentOperation", "ExE73C2C", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002C12 RID: 11282
		// (get) Token: 0x06007A89 RID: 31369 RVA: 0x001998DB File Offset: 0x00197ADB
		public static LocalizedString StarAcceptedDomainCannotBeAuthoritative
		{
			get
			{
				return new LocalizedString("StarAcceptedDomainCannotBeAuthoritative", "Ex2BF988", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A8A RID: 31370 RVA: 0x001998FC File Offset: 0x00197AFC
		public static LocalizedString CalculatedPropertyFailed(string propertyName, string basePropertyName, string errorMessage)
		{
			return new LocalizedString("CalculatedPropertyFailed", "ExCC6DFF", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				basePropertyName,
				errorMessage
			});
		}

		// Token: 0x06007A8B RID: 31371 RVA: 0x00199934 File Offset: 0x00197B34
		public static LocalizedString ApiNotSupportedInBusinessSessionError(string cl, string member)
		{
			return new LocalizedString("ApiNotSupportedInBusinessSessionError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				cl,
				member
			});
		}

		// Token: 0x17002C13 RID: 11283
		// (get) Token: 0x06007A8C RID: 31372 RVA: 0x00199967 File Offset: 0x00197B67
		public static LocalizedString AllRooms
		{
			get
			{
				return new LocalizedString("AllRooms", "Ex70BA64", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A8D RID: 31373 RVA: 0x00199988 File Offset: 0x00197B88
		public static LocalizedString RusInvalidFilter(string error)
		{
			return new LocalizedString("RusInvalidFilter", "Ex5B04B5", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17002C14 RID: 11284
		// (get) Token: 0x06007A8E RID: 31374 RVA: 0x001999B7 File Offset: 0x00197BB7
		public static LocalizedString EsnLangRussian
		{
			get
			{
				return new LocalizedString("EsnLangRussian", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A8F RID: 31375 RVA: 0x001999D8 File Offset: 0x00197BD8
		public static LocalizedString ErrorNoSuitableDCInDomain(string domainName, string errorMessage)
		{
			return new LocalizedString("ErrorNoSuitableDCInDomain", "Ex50BF2A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				domainName,
				errorMessage
			});
		}

		// Token: 0x17002C15 RID: 11285
		// (get) Token: 0x06007A90 RID: 31376 RVA: 0x00199A0B File Offset: 0x00197C0B
		public static LocalizedString GroupNamingPolicyCustomAttribute10
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute10", "Ex05D681", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C16 RID: 11286
		// (get) Token: 0x06007A91 RID: 31377 RVA: 0x00199A29 File Offset: 0x00197C29
		public static LocalizedString SitesContainerNotFound
		{
			get
			{
				return new LocalizedString("SitesContainerNotFound", "ExE31758", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C17 RID: 11287
		// (get) Token: 0x06007A92 RID: 31378 RVA: 0x00199A47 File Offset: 0x00197C47
		public static LocalizedString ExceptionServerTimeoutNegative
		{
			get
			{
				return new LocalizedString("ExceptionServerTimeoutNegative", "Ex088C34", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C18 RID: 11288
		// (get) Token: 0x06007A93 RID: 31379 RVA: 0x00199A65 File Offset: 0x00197C65
		public static LocalizedString ArchiveStateLocal
		{
			get
			{
				return new LocalizedString("ArchiveStateLocal", "Ex148892", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C19 RID: 11289
		// (get) Token: 0x06007A94 RID: 31380 RVA: 0x00199A83 File Offset: 0x00197C83
		public static LocalizedString NotesMC
		{
			get
			{
				return new LocalizedString("NotesMC", "Ex667DA1", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A95 RID: 31381 RVA: 0x00199AA4 File Offset: 0x00197CA4
		public static LocalizedString SuitabilityErrorDNS(string fqdn, string details)
		{
			return new LocalizedString("SuitabilityErrorDNS", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				fqdn,
				details
			});
		}

		// Token: 0x06007A96 RID: 31382 RVA: 0x00199AD8 File Offset: 0x00197CD8
		public static LocalizedString ExceptionGetLocalSiteArgumentException(string siteName)
		{
			return new LocalizedString("ExceptionGetLocalSiteArgumentException", "Ex95D783", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				siteName
			});
		}

		// Token: 0x17002C1A RID: 11290
		// (get) Token: 0x06007A97 RID: 31383 RVA: 0x00199B07 File Offset: 0x00197D07
		public static LocalizedString InvalidDomain
		{
			get
			{
				return new LocalizedString("InvalidDomain", "Ex9A29DE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A98 RID: 31384 RVA: 0x00199B28 File Offset: 0x00197D28
		public static LocalizedString InvalidSyncLinkFormat(string link)
		{
			return new LocalizedString("InvalidSyncLinkFormat", "Ex51D6DD", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				link
			});
		}

		// Token: 0x17002C1B RID: 11291
		// (get) Token: 0x06007A99 RID: 31385 RVA: 0x00199B57 File Offset: 0x00197D57
		public static LocalizedString EmailAgeFilterOneMonth
		{
			get
			{
				return new LocalizedString("EmailAgeFilterOneMonth", "Ex288AD3", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A9A RID: 31386 RVA: 0x00199B78 File Offset: 0x00197D78
		public static LocalizedString ErrorDuplicatePartnerApplicationId(string applicationId)
		{
			return new LocalizedString("ErrorDuplicatePartnerApplicationId", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				applicationId
			});
		}

		// Token: 0x17002C1C RID: 11292
		// (get) Token: 0x06007A9B RID: 31387 RVA: 0x00199BA7 File Offset: 0x00197DA7
		public static LocalizedString FullDomain
		{
			get
			{
				return new LocalizedString("FullDomain", "Ex106EA7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007A9C RID: 31388 RVA: 0x00199BC8 File Offset: 0x00197DC8
		public static LocalizedString ErrorAccountPartitionCantBeLocalAndHaveTrustAtTheSameTime(string id)
		{
			return new LocalizedString("ErrorAccountPartitionCantBeLocalAndHaveTrustAtTheSameTime", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06007A9D RID: 31389 RVA: 0x00199BF8 File Offset: 0x00197DF8
		public static LocalizedString ErrorADResponse(string message)
		{
			return new LocalizedString("ErrorADResponse", "Ex84338A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17002C1D RID: 11293
		// (get) Token: 0x06007A9E RID: 31390 RVA: 0x00199C27 File Offset: 0x00197E27
		public static LocalizedString DeviceModel
		{
			get
			{
				return new LocalizedString("DeviceModel", "Ex3359D0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C1E RID: 11294
		// (get) Token: 0x06007A9F RID: 31391 RVA: 0x00199C45 File Offset: 0x00197E45
		public static LocalizedString GroupRecipientType
		{
			get
			{
				return new LocalizedString("GroupRecipientType", "ExDB67AE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C1F RID: 11295
		// (get) Token: 0x06007AA0 RID: 31392 RVA: 0x00199C63 File Offset: 0x00197E63
		public static LocalizedString RemoteSharedMailboxTypeDetails
		{
			get
			{
				return new LocalizedString("RemoteSharedMailboxTypeDetails", "ExB74764", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C20 RID: 11296
		// (get) Token: 0x06007AA1 RID: 31393 RVA: 0x00199C81 File Offset: 0x00197E81
		public static LocalizedString LdapSearch
		{
			get
			{
				return new LocalizedString("LdapSearch", "Ex0C4B66", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007AA2 RID: 31394 RVA: 0x00199CA0 File Offset: 0x00197EA0
		public static LocalizedString ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory(string property)
		{
			return new LocalizedString("ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory", "Ex9307C4", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x17002C21 RID: 11297
		// (get) Token: 0x06007AA3 RID: 31395 RVA: 0x00199CCF File Offset: 0x00197ECF
		public static LocalizedString EsnLangArabic
		{
			get
			{
				return new LocalizedString("EsnLangArabic", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C22 RID: 11298
		// (get) Token: 0x06007AA4 RID: 31396 RVA: 0x00199CED File Offset: 0x00197EED
		public static LocalizedString SKUCapabilityBPOSSDeskless
		{
			get
			{
				return new LocalizedString("SKUCapabilityBPOSSDeskless", "Ex719ADB", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C23 RID: 11299
		// (get) Token: 0x06007AA5 RID: 31397 RVA: 0x00199D0B File Offset: 0x00197F0B
		public static LocalizedString ModeratedRecipients
		{
			get
			{
				return new LocalizedString("ModeratedRecipients", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C24 RID: 11300
		// (get) Token: 0x06007AA6 RID: 31398 RVA: 0x00199D29 File Offset: 0x00197F29
		public static LocalizedString ExceptionRusOperationFailed
		{
			get
			{
				return new LocalizedString("ExceptionRusOperationFailed", "ExEB0DEE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C25 RID: 11301
		// (get) Token: 0x06007AA7 RID: 31399 RVA: 0x00199D47 File Offset: 0x00197F47
		public static LocalizedString ExceptionDomainInfoRpcTooBusy
		{
			get
			{
				return new LocalizedString("ExceptionDomainInfoRpcTooBusy", "ExC400D9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C26 RID: 11302
		// (get) Token: 0x06007AA8 RID: 31400 RVA: 0x00199D65 File Offset: 0x00197F65
		public static LocalizedString ErrorArchiveDomainInvalidInDatacenter
		{
			get
			{
				return new LocalizedString("ErrorArchiveDomainInvalidInDatacenter", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007AA9 RID: 31401 RVA: 0x00199D84 File Offset: 0x00197F84
		public static LocalizedString UnrecognizedRoleEntryType(string entry)
		{
			return new LocalizedString("UnrecognizedRoleEntryType", "Ex7B3ABD", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				entry
			});
		}

		// Token: 0x17002C27 RID: 11303
		// (get) Token: 0x06007AAA RID: 31402 RVA: 0x00199DB3 File Offset: 0x00197FB3
		public static LocalizedString PublicFolderRecipientType
		{
			get
			{
				return new LocalizedString("PublicFolderRecipientType", "ExC9284D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C28 RID: 11304
		// (get) Token: 0x06007AAB RID: 31403 RVA: 0x00199DD1 File Offset: 0x00197FD1
		public static LocalizedString ErrorMessageClassHasUnsupportedWildcard
		{
			get
			{
				return new LocalizedString("ErrorMessageClassHasUnsupportedWildcard", "Ex6405EF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C29 RID: 11305
		// (get) Token: 0x06007AAC RID: 31404 RVA: 0x00199DEF File Offset: 0x00197FEF
		public static LocalizedString ErrorPipelineTracingRequirementsMissing
		{
			get
			{
				return new LocalizedString("ErrorPipelineTracingRequirementsMissing", "Ex906171", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C2A RID: 11306
		// (get) Token: 0x06007AAD RID: 31405 RVA: 0x00199E0D File Offset: 0x0019800D
		public static LocalizedString GroupNamingPolicyCustomAttribute11
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute11", "Ex7638F4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C2B RID: 11307
		// (get) Token: 0x06007AAE RID: 31406 RVA: 0x00199E2B File Offset: 0x0019802B
		public static LocalizedString ErrorMailTipMustNotBeEmpty
		{
			get
			{
				return new LocalizedString("ErrorMailTipMustNotBeEmpty", "ExC78163", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C2C RID: 11308
		// (get) Token: 0x06007AAF RID: 31407 RVA: 0x00199E49 File Offset: 0x00198049
		public static LocalizedString ComputerRecipientType
		{
			get
			{
				return new LocalizedString("ComputerRecipientType", "Ex060F59", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C2D RID: 11309
		// (get) Token: 0x06007AB0 RID: 31408 RVA: 0x00199E67 File Offset: 0x00198067
		public static LocalizedString ErrorArbitrationMailboxCannotBeModerated
		{
			get
			{
				return new LocalizedString("ErrorArbitrationMailboxCannotBeModerated", "Ex387876", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C2E RID: 11310
		// (get) Token: 0x06007AB1 RID: 31409 RVA: 0x00199E85 File Offset: 0x00198085
		public static LocalizedString EsnLangKannada
		{
			get
			{
				return new LocalizedString("EsnLangKannada", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C2F RID: 11311
		// (get) Token: 0x06007AB2 RID: 31410 RVA: 0x00199EA3 File Offset: 0x001980A3
		public static LocalizedString Title
		{
			get
			{
				return new LocalizedString("Title", "Ex40E83A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007AB3 RID: 31411 RVA: 0x00199EC4 File Offset: 0x001980C4
		public static LocalizedString FailedToUpdateEmailAddressesForExternal(string external, string propEmailAddresses, string reason)
		{
			return new LocalizedString("FailedToUpdateEmailAddressesForExternal", "ExCBEA5F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				external,
				propEmailAddresses,
				reason
			});
		}

		// Token: 0x17002C30 RID: 11312
		// (get) Token: 0x06007AB4 RID: 31412 RVA: 0x00199EFB File Offset: 0x001980FB
		public static LocalizedString MessageWaitingIndicatorEnabled
		{
			get
			{
				return new LocalizedString("MessageWaitingIndicatorEnabled", "Ex5E7DA9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C31 RID: 11313
		// (get) Token: 0x06007AB5 RID: 31413 RVA: 0x00199F19 File Offset: 0x00198119
		public static LocalizedString PublicFolders
		{
			get
			{
				return new LocalizedString("PublicFolders", "Ex7CC436", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C32 RID: 11314
		// (get) Token: 0x06007AB6 RID: 31414 RVA: 0x00199F37 File Offset: 0x00198137
		public static LocalizedString Millisecond
		{
			get
			{
				return new LocalizedString("Millisecond", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C33 RID: 11315
		// (get) Token: 0x06007AB7 RID: 31415 RVA: 0x00199F55 File Offset: 0x00198155
		public static LocalizedString StarAcceptedDomainCannotBeDefault
		{
			get
			{
				return new LocalizedString("StarAcceptedDomainCannotBeDefault", "ExC3D5D5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C34 RID: 11316
		// (get) Token: 0x06007AB8 RID: 31416 RVA: 0x00199F73 File Offset: 0x00198173
		public static LocalizedString ReceiveExtendedProtectionPolicyAllow
		{
			get
			{
				return new LocalizedString("ReceiveExtendedProtectionPolicyAllow", "Ex9A250A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C35 RID: 11317
		// (get) Token: 0x06007AB9 RID: 31417 RVA: 0x00199F91 File Offset: 0x00198191
		public static LocalizedString ResourceMailbox
		{
			get
			{
				return new LocalizedString("ResourceMailbox", "Ex81C4FC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C36 RID: 11318
		// (get) Token: 0x06007ABA RID: 31418 RVA: 0x00199FAF File Offset: 0x001981AF
		public static LocalizedString ErrorThrottlingPolicyStateIsCorrupt
		{
			get
			{
				return new LocalizedString("ErrorThrottlingPolicyStateIsCorrupt", "Ex7894C5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C37 RID: 11319
		// (get) Token: 0x06007ABB RID: 31419 RVA: 0x00199FCD File Offset: 0x001981CD
		public static LocalizedString MailEnabledNonUniversalGroupRecipientType
		{
			get
			{
				return new LocalizedString("MailEnabledNonUniversalGroupRecipientType", "ExB5904C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007ABC RID: 31420 RVA: 0x00199FEC File Offset: 0x001981EC
		public static LocalizedString ExceptionErrorFromRUS(string server, int error)
		{
			return new LocalizedString("ExceptionErrorFromRUS", "Ex5790AD", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				error
			});
		}

		// Token: 0x17002C38 RID: 11320
		// (get) Token: 0x06007ABD RID: 31421 RVA: 0x0019A024 File Offset: 0x00198224
		public static LocalizedString ExternalAuthoritativeWithoutExchangeServerPermission
		{
			get
			{
				return new LocalizedString("ExternalAuthoritativeWithoutExchangeServerPermission", "Ex2F3871", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C39 RID: 11321
		// (get) Token: 0x06007ABE RID: 31422 RVA: 0x0019A042 File Offset: 0x00198242
		public static LocalizedString Authoritative
		{
			get
			{
				return new LocalizedString("Authoritative", "Ex364E82", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C3A RID: 11322
		// (get) Token: 0x06007ABF RID: 31423 RVA: 0x0019A060 File Offset: 0x00198260
		public static LocalizedString ErrorPrimarySmtpAddressAndWindowsEmailAddressNotMatch
		{
			get
			{
				return new LocalizedString("ErrorPrimarySmtpAddressAndWindowsEmailAddressNotMatch", "Ex28C9AC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C3B RID: 11323
		// (get) Token: 0x06007AC0 RID: 31424 RVA: 0x0019A07E File Offset: 0x0019827E
		public static LocalizedString PostMC
		{
			get
			{
				return new LocalizedString("PostMC", "Ex2B153A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C3C RID: 11324
		// (get) Token: 0x06007AC1 RID: 31425 RVA: 0x0019A09C File Offset: 0x0019829C
		public static LocalizedString UnknownConfigObject
		{
			get
			{
				return new LocalizedString("UnknownConfigObject", "Ex69DC5E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007AC2 RID: 31426 RVA: 0x0019A0BC File Offset: 0x001982BC
		public static LocalizedString ErrorSubnetMaskOutOfRange(int maskBits, string address, int min, int max)
		{
			return new LocalizedString("ErrorSubnetMaskOutOfRange", "Ex3F1C79", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				maskBits,
				address,
				min,
				max
			});
		}

		// Token: 0x17002C3D RID: 11325
		// (get) Token: 0x06007AC3 RID: 31427 RVA: 0x0019A106 File Offset: 0x00198306
		public static LocalizedString MalwareScanErrorActionAllow
		{
			get
			{
				return new LocalizedString("MalwareScanErrorActionAllow", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C3E RID: 11326
		// (get) Token: 0x06007AC4 RID: 31428 RVA: 0x0019A124 File Offset: 0x00198324
		public static LocalizedString GroupNamingPolicyCustomAttribute6
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute6", "ExC01F0E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007AC5 RID: 31429 RVA: 0x0019A144 File Offset: 0x00198344
		public static LocalizedString NonUniquePilotIdentifier(string pilotId, string dialPlan)
		{
			return new LocalizedString("NonUniquePilotIdentifier", "Ex60A5D2", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				pilotId,
				dialPlan
			});
		}

		// Token: 0x06007AC6 RID: 31430 RVA: 0x0019A178 File Offset: 0x00198378
		public static LocalizedString ErrorThresholdMustBeSet(string name)
		{
			return new LocalizedString("ErrorThresholdMustBeSet", "ExC25A5B", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06007AC7 RID: 31431 RVA: 0x0019A1A8 File Offset: 0x001983A8
		public static LocalizedString ErrorProperty1NeValue1WhileProperty2EqValue2(string property1Name, string value1, string property2Name, string value2)
		{
			return new LocalizedString("ErrorProperty1NeValue1WhileProperty2EqValue2", "ExA09669", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property1Name,
				value1,
				property2Name,
				value2
			});
		}

		// Token: 0x17002C3F RID: 11327
		// (get) Token: 0x06007AC8 RID: 31432 RVA: 0x0019A1E3 File Offset: 0x001983E3
		public static LocalizedString InvalidTransportSyncLogSizeConfiguration
		{
			get
			{
				return new LocalizedString("InvalidTransportSyncLogSizeConfiguration", "Ex66671C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C40 RID: 11328
		// (get) Token: 0x06007AC9 RID: 31433 RVA: 0x0019A201 File Offset: 0x00198401
		public static LocalizedString WellKnownRecipientTypeMailGroups
		{
			get
			{
				return new LocalizedString("WellKnownRecipientTypeMailGroups", "ExCCA960", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C41 RID: 11329
		// (get) Token: 0x06007ACA RID: 31434 RVA: 0x0019A21F File Offset: 0x0019841F
		public static LocalizedString ADDriverStoreAccessTransientError
		{
			get
			{
				return new LocalizedString("ADDriverStoreAccessTransientError", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007ACB RID: 31435 RVA: 0x0019A240 File Offset: 0x00198440
		public static LocalizedString InvalidAutoAttendantSetting(string value, string argument)
		{
			return new LocalizedString("InvalidAutoAttendantSetting", "Ex7718A0", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				value,
				argument
			});
		}

		// Token: 0x17002C42 RID: 11330
		// (get) Token: 0x06007ACC RID: 31436 RVA: 0x0019A273 File Offset: 0x00198473
		public static LocalizedString AACantChangeName
		{
			get
			{
				return new LocalizedString("AACantChangeName", "Ex7EC7CE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C43 RID: 11331
		// (get) Token: 0x06007ACD RID: 31437 RVA: 0x0019A291 File Offset: 0x00198491
		public static LocalizedString ContactItemsMC
		{
			get
			{
				return new LocalizedString("ContactItemsMC", "ExB884C8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C44 RID: 11332
		// (get) Token: 0x06007ACE RID: 31438 RVA: 0x0019A2AF File Offset: 0x001984AF
		public static LocalizedString EsnLangKorean
		{
			get
			{
				return new LocalizedString("EsnLangKorean", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007ACF RID: 31439 RVA: 0x0019A2D0 File Offset: 0x001984D0
		public static LocalizedString InvalidFilterSize(int size)
		{
			return new LocalizedString("InvalidFilterSize", "ExE186A6", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				size
			});
		}

		// Token: 0x17002C45 RID: 11333
		// (get) Token: 0x06007AD0 RID: 31440 RVA: 0x0019A304 File Offset: 0x00198504
		public static LocalizedString RssSubscriptionMC
		{
			get
			{
				return new LocalizedString("RssSubscriptionMC", "ExF86F02", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007AD1 RID: 31441 RVA: 0x0019A324 File Offset: 0x00198524
		public static LocalizedString ServerComponentReadADError(string adErrorStr)
		{
			return new LocalizedString("ServerComponentReadADError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				adErrorStr
			});
		}

		// Token: 0x06007AD2 RID: 31442 RVA: 0x0019A354 File Offset: 0x00198554
		public static LocalizedString ErrorLogFolderPathEqualsCopyLogFolderPath(string path)
		{
			return new LocalizedString("ErrorLogFolderPathEqualsCopyLogFolderPath", "ExF9578F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x17002C46 RID: 11334
		// (get) Token: 0x06007AD3 RID: 31443 RVA: 0x0019A383 File Offset: 0x00198583
		public static LocalizedString LdapFilterErrorSpaceMiddleType
		{
			get
			{
				return new LocalizedString("LdapFilterErrorSpaceMiddleType", "Ex8866E5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C47 RID: 11335
		// (get) Token: 0x06007AD4 RID: 31444 RVA: 0x0019A3A1 File Offset: 0x001985A1
		public static LocalizedString GroupNamingPolicyCustomAttribute3
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute3", "Ex20271C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007AD5 RID: 31445 RVA: 0x0019A3C0 File Offset: 0x001985C0
		public static LocalizedString ErrorArchiveMailboxExists(string guid)
		{
			return new LocalizedString("ErrorArchiveMailboxExists", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x06007AD6 RID: 31446 RVA: 0x0019A3F0 File Offset: 0x001985F0
		public static LocalizedString InvalidCrossTenantIdFormat(string str)
		{
			return new LocalizedString("InvalidCrossTenantIdFormat", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				str
			});
		}

		// Token: 0x17002C48 RID: 11336
		// (get) Token: 0x06007AD7 RID: 31447 RVA: 0x0019A41F File Offset: 0x0019861F
		public static LocalizedString ExceptionNoFsmoRoleOwnerAttribute
		{
			get
			{
				return new LocalizedString("ExceptionNoFsmoRoleOwnerAttribute", "ExE7E68E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C49 RID: 11337
		// (get) Token: 0x06007AD8 RID: 31448 RVA: 0x0019A43D File Offset: 0x0019863D
		public static LocalizedString NonIpmRoot
		{
			get
			{
				return new LocalizedString("NonIpmRoot", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C4A RID: 11338
		// (get) Token: 0x06007AD9 RID: 31449 RVA: 0x0019A45B File Offset: 0x0019865B
		public static LocalizedString ErrorTimeoutWritingSystemAddressListMemberCount
		{
			get
			{
				return new LocalizedString("ErrorTimeoutWritingSystemAddressListMemberCount", "ExD84E2E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007ADA RID: 31450 RVA: 0x0019A47C File Offset: 0x0019867C
		public static LocalizedString CustomRecipientWriteScopeCannotBeEmpty(RecipientWriteScopeType scopeType)
		{
			return new LocalizedString("CustomRecipientWriteScopeCannotBeEmpty", "ExD0F2C8", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scopeType
			});
		}

		// Token: 0x17002C4B RID: 11339
		// (get) Token: 0x06007ADB RID: 31451 RVA: 0x0019A4B0 File Offset: 0x001986B0
		public static LocalizedString ExceptionExternalError
		{
			get
			{
				return new LocalizedString("ExceptionExternalError", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C4C RID: 11340
		// (get) Token: 0x06007ADC RID: 31452 RVA: 0x0019A4CE File Offset: 0x001986CE
		public static LocalizedString Calendar
		{
			get
			{
				return new LocalizedString("Calendar", "Ex113F91", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007ADD RID: 31453 RVA: 0x0019A4EC File Offset: 0x001986EC
		public static LocalizedString CannotBuildAuthenticationTypeFilterNoNamespacesOfType(string authType)
		{
			return new LocalizedString("CannotBuildAuthenticationTypeFilterNoNamespacesOfType", "ExD98642", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				authType
			});
		}

		// Token: 0x17002C4D RID: 11341
		// (get) Token: 0x06007ADE RID: 31454 RVA: 0x0019A51B File Offset: 0x0019871B
		public static LocalizedString Wma
		{
			get
			{
				return new LocalizedString("Wma", "ExACDD6A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007ADF RID: 31455 RVA: 0x0019A53C File Offset: 0x0019873C
		public static LocalizedString ErrorSystemFolderPathNotEqualLogFolderPath(NonRootLocalLongFullPath sysPath, NonRootLocalLongFullPath logPath)
		{
			return new LocalizedString("ErrorSystemFolderPathNotEqualLogFolderPath", "Ex9B0573", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				sysPath,
				logPath
			});
		}

		// Token: 0x17002C4E RID: 11342
		// (get) Token: 0x06007AE0 RID: 31456 RVA: 0x0019A56F File Offset: 0x0019876F
		public static LocalizedString ErrorInvalidDNDepth
		{
			get
			{
				return new LocalizedString("ErrorInvalidDNDepth", "Ex9AF71B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C4F RID: 11343
		// (get) Token: 0x06007AE1 RID: 31457 RVA: 0x0019A58D File Offset: 0x0019878D
		public static LocalizedString CapabilityMasteredOnPremise
		{
			get
			{
				return new LocalizedString("CapabilityMasteredOnPremise", "Ex65AADE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C50 RID: 11344
		// (get) Token: 0x06007AE2 RID: 31458 RVA: 0x0019A5AB File Offset: 0x001987AB
		public static LocalizedString EdgeSyncEhfConnectorFailedToDecryptPassword
		{
			get
			{
				return new LocalizedString("EdgeSyncEhfConnectorFailedToDecryptPassword", "ExDEB7E0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007AE3 RID: 31459 RVA: 0x0019A5CC File Offset: 0x001987CC
		public static LocalizedString LegacyGwartNotFoundException(string gwartName, string adminGroupName)
		{
			return new LocalizedString("LegacyGwartNotFoundException", "ExCA93B2", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				gwartName,
				adminGroupName
			});
		}

		// Token: 0x06007AE4 RID: 31460 RVA: 0x0019A600 File Offset: 0x00198800
		public static LocalizedString ErrorSystemFolderPathEqualsCopySystemFolderPath(string path)
		{
			return new LocalizedString("ErrorSystemFolderPathEqualsCopySystemFolderPath", "Ex71C029", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x17002C51 RID: 11345
		// (get) Token: 0x06007AE5 RID: 31461 RVA: 0x0019A62F File Offset: 0x0019882F
		public static LocalizedString ErrorArchiveDomainSetForNonArchive
		{
			get
			{
				return new LocalizedString("ErrorArchiveDomainSetForNonArchive", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C52 RID: 11346
		// (get) Token: 0x06007AE6 RID: 31462 RVA: 0x0019A64D File Offset: 0x0019884D
		public static LocalizedString ExceptionObjectHasBeenDeleted
		{
			get
			{
				return new LocalizedString("ExceptionObjectHasBeenDeleted", "Ex3124CB", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C53 RID: 11347
		// (get) Token: 0x06007AE7 RID: 31463 RVA: 0x0019A66B File Offset: 0x0019886B
		public static LocalizedString EsnLangBengaliIndia
		{
			get
			{
				return new LocalizedString("EsnLangBengaliIndia", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C54 RID: 11348
		// (get) Token: 0x06007AE8 RID: 31464 RVA: 0x0019A689 File Offset: 0x00198889
		public static LocalizedString PublicFolderServer
		{
			get
			{
				return new LocalizedString("PublicFolderServer", "Ex0B618C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C55 RID: 11349
		// (get) Token: 0x06007AE9 RID: 31465 RVA: 0x0019A6A7 File Offset: 0x001988A7
		public static LocalizedString ErrorCannotSetPrimarySmtpAddress
		{
			get
			{
				return new LocalizedString("ErrorCannotSetPrimarySmtpAddress", "Ex9727CE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C56 RID: 11350
		// (get) Token: 0x06007AEA RID: 31466 RVA: 0x0019A6C5 File Offset: 0x001988C5
		public static LocalizedString SpamFilteringActionQuarantine
		{
			get
			{
				return new LocalizedString("SpamFilteringActionQuarantine", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007AEB RID: 31467 RVA: 0x0019A6E4 File Offset: 0x001988E4
		public static LocalizedString ExceptionWKGuidNeedsDomainSession(Guid wkguid)
		{
			return new LocalizedString("ExceptionWKGuidNeedsDomainSession", "ExAFE1DE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				wkguid
			});
		}

		// Token: 0x17002C57 RID: 11351
		// (get) Token: 0x06007AEC RID: 31468 RVA: 0x0019A718 File Offset: 0x00198918
		public static LocalizedString MailboxMoveStatusFailed
		{
			get
			{
				return new LocalizedString("MailboxMoveStatusFailed", "ExF26D08", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007AED RID: 31469 RVA: 0x0019A738 File Offset: 0x00198938
		public static LocalizedString ErrorReportToManagedEnabledWithoutManager(string id, string propertyName)
		{
			return new LocalizedString("ErrorReportToManagedEnabledWithoutManager", "Ex80687E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id,
				propertyName
			});
		}

		// Token: 0x17002C58 RID: 11352
		// (get) Token: 0x06007AEE RID: 31470 RVA: 0x0019A76B File Offset: 0x0019896B
		public static LocalizedString SecurityPrincipalTypeUniversalSecurityGroup
		{
			get
			{
				return new LocalizedString("SecurityPrincipalTypeUniversalSecurityGroup", "Ex61EE5C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007AEF RID: 31471 RVA: 0x0019A78C File Offset: 0x0019898C
		public static LocalizedString ThrottlingPolicyCorrupted(string policyId)
		{
			return new LocalizedString("ThrottlingPolicyCorrupted", "Ex034692", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				policyId
			});
		}

		// Token: 0x06007AF0 RID: 31472 RVA: 0x0019A7BC File Offset: 0x001989BC
		public static LocalizedString ExceptionOwaCannotSetPropertyOnE14MailboxPolicyToNull(string property)
		{
			return new LocalizedString("ExceptionOwaCannotSetPropertyOnE14MailboxPolicyToNull", "ExAC78E8", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x06007AF1 RID: 31473 RVA: 0x0019A7EC File Offset: 0x001989EC
		public static LocalizedString EXOStandardRestrictions_Error(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("EXOStandardRestrictions_Error", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x17002C59 RID: 11353
		// (get) Token: 0x06007AF2 RID: 31474 RVA: 0x0019A827 File Offset: 0x00198A27
		public static LocalizedString DynamicDLRecipientType
		{
			get
			{
				return new LocalizedString("DynamicDLRecipientType", "Ex37D4DD", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007AF3 RID: 31475 RVA: 0x0019A848 File Offset: 0x00198A48
		public static LocalizedString InvalidDialPlan(string s)
		{
			return new LocalizedString("InvalidDialPlan", "Ex759A2F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x17002C5A RID: 11354
		// (get) Token: 0x06007AF4 RID: 31476 RVA: 0x0019A877 File Offset: 0x00198A77
		public static LocalizedString ErrorNonTinyTenantShouldNotHaveSharedConfig
		{
			get
			{
				return new LocalizedString("ErrorNonTinyTenantShouldNotHaveSharedConfig", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C5B RID: 11355
		// (get) Token: 0x06007AF5 RID: 31477 RVA: 0x0019A895 File Offset: 0x00198A95
		public static LocalizedString CanRunRestoreState_Allowed
		{
			get
			{
				return new LocalizedString("CanRunRestoreState_Allowed", "Ex6F69F3", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C5C RID: 11356
		// (get) Token: 0x06007AF6 RID: 31478 RVA: 0x0019A8B3 File Offset: 0x00198AB3
		public static LocalizedString DomainSecureWithIgnoreStartTLSEnabled
		{
			get
			{
				return new LocalizedString("DomainSecureWithIgnoreStartTLSEnabled", "ExBE80FB", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007AF7 RID: 31479 RVA: 0x0019A8D4 File Offset: 0x00198AD4
		public static LocalizedString ErrorSubnetAddressDoesNotMatchMask(string address, int maskBits, string realAddress)
		{
			return new LocalizedString("ErrorSubnetAddressDoesNotMatchMask", "Ex5B1E6A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				address,
				maskBits,
				realAddress
			});
		}

		// Token: 0x17002C5D RID: 11357
		// (get) Token: 0x06007AF8 RID: 31480 RVA: 0x0019A910 File Offset: 0x00198B10
		public static LocalizedString GroupNamingPolicyExtensionCustomAttribute4
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyExtensionCustomAttribute4", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C5E RID: 11358
		// (get) Token: 0x06007AF9 RID: 31481 RVA: 0x0019A92E File Offset: 0x00198B2E
		public static LocalizedString UseMsg
		{
			get
			{
				return new LocalizedString("UseMsg", "ExA3C339", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C5F RID: 11359
		// (get) Token: 0x06007AFA RID: 31482 RVA: 0x0019A94C File Offset: 0x00198B4C
		public static LocalizedString InvalidTenantFullSyncCookieException
		{
			get
			{
				return new LocalizedString("InvalidTenantFullSyncCookieException", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007AFB RID: 31483 RVA: 0x0019A96C File Offset: 0x00198B6C
		public static LocalizedString ErrorProperty1GtProperty2(string property1Name, string property1Value, string property2Name, string property2Value)
		{
			return new LocalizedString("ErrorProperty1GtProperty2", "ExFAD17D", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property1Name,
				property1Value,
				property2Name,
				property2Value
			});
		}

		// Token: 0x17002C60 RID: 11360
		// (get) Token: 0x06007AFC RID: 31484 RVA: 0x0019A9A7 File Offset: 0x00198BA7
		public static LocalizedString AutoDatabaseMountDialGoodAvailability
		{
			get
			{
				return new LocalizedString("AutoDatabaseMountDialGoodAvailability", "Ex0CF926", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C61 RID: 11361
		// (get) Token: 0x06007AFD RID: 31485 RVA: 0x0019A9C5 File Offset: 0x00198BC5
		public static LocalizedString ForestTrust
		{
			get
			{
				return new LocalizedString("ForestTrust", "ExD9F5D9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C62 RID: 11362
		// (get) Token: 0x06007AFE RID: 31486 RVA: 0x0019A9E3 File Offset: 0x00198BE3
		public static LocalizedString ErrorInvalidMailboxRelationType
		{
			get
			{
				return new LocalizedString("ErrorInvalidMailboxRelationType", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C63 RID: 11363
		// (get) Token: 0x06007AFF RID: 31487 RVA: 0x0019AA01 File Offset: 0x00198C01
		public static LocalizedString ErrorDDLInvalidDNSyntax
		{
			get
			{
				return new LocalizedString("ErrorDDLInvalidDNSyntax", "Ex778F19", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B00 RID: 31488 RVA: 0x0019AA20 File Offset: 0x00198C20
		public static LocalizedString InvalidNonPositiveResourceThreshold(string classification, string thresholdName, int value)
		{
			return new LocalizedString("InvalidNonPositiveResourceThreshold", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				classification,
				thresholdName,
				value
			});
		}

		// Token: 0x06007B01 RID: 31489 RVA: 0x0019AA5C File Offset: 0x00198C5C
		public static LocalizedString GlsEndpointNotFound(string endpoint, string message)
		{
			return new LocalizedString("GlsEndpointNotFound", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				endpoint,
				message
			});
		}

		// Token: 0x06007B02 RID: 31490 RVA: 0x0019AA90 File Offset: 0x00198C90
		public static LocalizedString InvalidWaveFilename(string s)
		{
			return new LocalizedString("InvalidWaveFilename", "ExE33FFA", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x17002C64 RID: 11364
		// (get) Token: 0x06007B03 RID: 31491 RVA: 0x0019AABF File Offset: 0x00198CBF
		public static LocalizedString ByteEncoderTypeUseQP
		{
			get
			{
				return new LocalizedString("ByteEncoderTypeUseQP", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B04 RID: 31492 RVA: 0x0019AAE0 File Offset: 0x00198CE0
		public static LocalizedString CannotFindTenantCUByExternalDirectoryId(string id)
		{
			return new LocalizedString("CannotFindTenantCUByExternalDirectoryId", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002C65 RID: 11365
		// (get) Token: 0x06007B05 RID: 31493 RVA: 0x0019AB0F File Offset: 0x00198D0F
		public static LocalizedString NoLocatorInformationInMServException
		{
			get
			{
				return new LocalizedString("NoLocatorInformationInMServException", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C66 RID: 11366
		// (get) Token: 0x06007B06 RID: 31494 RVA: 0x0019AB2D File Offset: 0x00198D2D
		public static LocalizedString SecurityPrincipalTypeGlobalSecurityGroup
		{
			get
			{
				return new LocalizedString("SecurityPrincipalTypeGlobalSecurityGroup", "Ex7C0C6F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B07 RID: 31495 RVA: 0x0019AB4C File Offset: 0x00198D4C
		public static LocalizedString InvalidSyncCompanyId(string idValue)
		{
			return new LocalizedString("InvalidSyncCompanyId", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				idValue
			});
		}

		// Token: 0x06007B08 RID: 31496 RVA: 0x0019AB7C File Offset: 0x00198D7C
		public static LocalizedString CustomRecipientWriteScopeMustBeEmpty(RecipientWriteScopeType scopeType)
		{
			return new LocalizedString("CustomRecipientWriteScopeMustBeEmpty", "Ex7486C6", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scopeType
			});
		}

		// Token: 0x06007B09 RID: 31497 RVA: 0x0019ABB0 File Offset: 0x00198DB0
		public static LocalizedString ErrorReportToBothManagerAndOriginator(string id, string prop1, string prop2)
		{
			return new LocalizedString("ErrorReportToBothManagerAndOriginator", "Ex9A45AC", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id,
				prop1,
				prop2
			});
		}

		// Token: 0x06007B0A RID: 31498 RVA: 0x0019ABE8 File Offset: 0x00198DE8
		public static LocalizedString PublicFolderReferralServerNotExisting(string serverGuid)
		{
			return new LocalizedString("PublicFolderReferralServerNotExisting", "ExC4DC0B", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				serverGuid
			});
		}

		// Token: 0x17002C67 RID: 11367
		// (get) Token: 0x06007B0B RID: 31499 RVA: 0x0019AC17 File Offset: 0x00198E17
		public static LocalizedString CannotGetUsefulSiteInfo
		{
			get
			{
				return new LocalizedString("CannotGetUsefulSiteInfo", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C68 RID: 11368
		// (get) Token: 0x06007B0C RID: 31500 RVA: 0x0019AC35 File Offset: 0x00198E35
		public static LocalizedString ErrorPipelineTracingPathNotExist
		{
			get
			{
				return new LocalizedString("ErrorPipelineTracingPathNotExist", "ExFFB6AE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C69 RID: 11369
		// (get) Token: 0x06007B0D RID: 31501 RVA: 0x0019AC53 File Offset: 0x00198E53
		public static LocalizedString MailboxServer
		{
			get
			{
				return new LocalizedString("MailboxServer", "Ex10681A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C6A RID: 11370
		// (get) Token: 0x06007B0E RID: 31502 RVA: 0x0019AC71 File Offset: 0x00198E71
		public static LocalizedString Blocked
		{
			get
			{
				return new LocalizedString("Blocked", "Ex81292C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C6B RID: 11371
		// (get) Token: 0x06007B0F RID: 31503 RVA: 0x0019AC8F File Offset: 0x00198E8F
		public static LocalizedString InvalidMainStreamCookieException
		{
			get
			{
				return new LocalizedString("InvalidMainStreamCookieException", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C6C RID: 11372
		// (get) Token: 0x06007B10 RID: 31504 RVA: 0x0019ACAD File Offset: 0x00198EAD
		public static LocalizedString MoveNotAllowed
		{
			get
			{
				return new LocalizedString("MoveNotAllowed", "ExA949A6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C6D RID: 11373
		// (get) Token: 0x06007B11 RID: 31505 RVA: 0x0019ACCB File Offset: 0x00198ECB
		public static LocalizedString RemoteRoomMailboxTypeDetails
		{
			get
			{
				return new LocalizedString("RemoteRoomMailboxTypeDetails", "Ex20D754", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C6E RID: 11374
		// (get) Token: 0x06007B12 RID: 31506 RVA: 0x0019ACE9 File Offset: 0x00198EE9
		public static LocalizedString SecurityPrincipalTypeUser
		{
			get
			{
				return new LocalizedString("SecurityPrincipalTypeUser", "ExA27E57", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C6F RID: 11375
		// (get) Token: 0x06007B13 RID: 31507 RVA: 0x0019AD07 File Offset: 0x00198F07
		public static LocalizedString TextEnrichedOnly
		{
			get
			{
				return new LocalizedString("TextEnrichedOnly", "ExF33ED6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C70 RID: 11376
		// (get) Token: 0x06007B14 RID: 31508 RVA: 0x0019AD25 File Offset: 0x00198F25
		public static LocalizedString BluetoothAllow
		{
			get
			{
				return new LocalizedString("BluetoothAllow", "Ex85B727", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C71 RID: 11377
		// (get) Token: 0x06007B15 RID: 31509 RVA: 0x0019AD43 File Offset: 0x00198F43
		public static LocalizedString GroupNamingPolicyDepartment
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyDepartment", "Ex1E50B4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C72 RID: 11378
		// (get) Token: 0x06007B16 RID: 31510 RVA: 0x0019AD61 File Offset: 0x00198F61
		public static LocalizedString UseDefaultSettings
		{
			get
			{
				return new LocalizedString("UseDefaultSettings", "ExDBFB4E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B17 RID: 31511 RVA: 0x0019AD80 File Offset: 0x00198F80
		public static LocalizedString ErrorNullRecipientTypeInPrecannedFilter(string includedRecipients)
		{
			return new LocalizedString("ErrorNullRecipientTypeInPrecannedFilter", "ExB4EEEE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				includedRecipients
			});
		}

		// Token: 0x17002C73 RID: 11379
		// (get) Token: 0x06007B18 RID: 31512 RVA: 0x0019ADAF File Offset: 0x00198FAF
		public static LocalizedString ByteEncoderTypeUseQPHtmlDetectTextPlain
		{
			get
			{
				return new LocalizedString("ByteEncoderTypeUseQPHtmlDetectTextPlain", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C74 RID: 11380
		// (get) Token: 0x06007B19 RID: 31513 RVA: 0x0019ADCD File Offset: 0x00198FCD
		public static LocalizedString Exchange2007
		{
			get
			{
				return new LocalizedString("Exchange2007", "Ex65D9D0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B1A RID: 31514 RVA: 0x0019ADEC File Offset: 0x00198FEC
		public static LocalizedString ProviderFileLoadException(string providerName, string assemblyPath)
		{
			return new LocalizedString("ProviderFileLoadException", "Ex3F3CB6", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				providerName,
				assemblyPath
			});
		}

		// Token: 0x06007B1B RID: 31515 RVA: 0x0019AE20 File Offset: 0x00199020
		public static LocalizedString SharedConfigurationNotFound(string tinyTenant)
		{
			return new LocalizedString("SharedConfigurationNotFound", "Ex947D5F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				tinyTenant
			});
		}

		// Token: 0x17002C75 RID: 11381
		// (get) Token: 0x06007B1C RID: 31516 RVA: 0x0019AE4F File Offset: 0x0019904F
		public static LocalizedString DisabledPartner
		{
			get
			{
				return new LocalizedString("DisabledPartner", "Ex831C1D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B1D RID: 31517 RVA: 0x0019AE70 File Offset: 0x00199070
		public static LocalizedString ExceptionFilterWithNullValue(string property)
		{
			return new LocalizedString("ExceptionFilterWithNullValue", "ExD9BA71", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x06007B1E RID: 31518 RVA: 0x0019AEA0 File Offset: 0x001990A0
		public static LocalizedString ErrorPrimarySmtpTemplateInvalid(string primary)
		{
			return new LocalizedString("ErrorPrimarySmtpTemplateInvalid", "Ex585166", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				primary
			});
		}

		// Token: 0x06007B1F RID: 31519 RVA: 0x0019AED0 File Offset: 0x001990D0
		public static LocalizedString ExceptionOwaCannotSetPropertyOnLegacyMailboxPolicy(string property)
		{
			return new LocalizedString("ExceptionOwaCannotSetPropertyOnLegacyMailboxPolicy", "ExAE3E2A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x06007B20 RID: 31520 RVA: 0x0019AF00 File Offset: 0x00199100
		public static LocalizedString InvalidControlAttributeName(string controlType, string pageName, int controlPosition, string attributeName)
		{
			return new LocalizedString("InvalidControlAttributeName", "Ex42C0A0", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				controlType,
				pageName,
				controlPosition,
				attributeName
			});
		}

		// Token: 0x17002C76 RID: 11382
		// (get) Token: 0x06007B21 RID: 31521 RVA: 0x0019AF40 File Offset: 0x00199140
		public static LocalizedString Consumer
		{
			get
			{
				return new LocalizedString("Consumer", "Ex796023", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C77 RID: 11383
		// (get) Token: 0x06007B22 RID: 31522 RVA: 0x0019AF5E File Offset: 0x0019915E
		public static LocalizedString PrimaryMailboxRelationType
		{
			get
			{
				return new LocalizedString("PrimaryMailboxRelationType", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C78 RID: 11384
		// (get) Token: 0x06007B23 RID: 31523 RVA: 0x0019AF7C File Offset: 0x0019917C
		public static LocalizedString Disabled
		{
			get
			{
				return new LocalizedString("Disabled", "Ex8A45D0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B24 RID: 31524 RVA: 0x0019AF9C File Offset: 0x0019919C
		public static LocalizedString ErrorProperty1LtProperty2(string property1Name, string property1Value, string property2Name, string property2Value)
		{
			return new LocalizedString("ErrorProperty1LtProperty2", "Ex62EFBE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property1Name,
				property1Value,
				property2Name,
				property2Value
			});
		}

		// Token: 0x17002C79 RID: 11385
		// (get) Token: 0x06007B25 RID: 31525 RVA: 0x0019AFD7 File Offset: 0x001991D7
		public static LocalizedString SKUCapabilityBPOSSBasicCustomDomain
		{
			get
			{
				return new LocalizedString("SKUCapabilityBPOSSBasicCustomDomain", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C7A RID: 11386
		// (get) Token: 0x06007B26 RID: 31526 RVA: 0x0019AFF5 File Offset: 0x001991F5
		public static LocalizedString ControlTextNull
		{
			get
			{
				return new LocalizedString("ControlTextNull", "Ex9C1B4E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C7B RID: 11387
		// (get) Token: 0x06007B27 RID: 31527 RVA: 0x0019B013 File Offset: 0x00199213
		public static LocalizedString Outbox
		{
			get
			{
				return new LocalizedString("Outbox", "Ex473C6F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C7C RID: 11388
		// (get) Token: 0x06007B28 RID: 31528 RVA: 0x0019B031 File Offset: 0x00199231
		public static LocalizedString ArchiveStateNone
		{
			get
			{
				return new LocalizedString("ArchiveStateNone", "ExF4F608", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C7D RID: 11389
		// (get) Token: 0x06007B29 RID: 31529 RVA: 0x0019B04F File Offset: 0x0019924F
		public static LocalizedString MailFlowPartnerInternalMailContentTypeMimeText
		{
			get
			{
				return new LocalizedString("MailFlowPartnerInternalMailContentTypeMimeText", "ExF2ED8D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C7E RID: 11390
		// (get) Token: 0x06007B2A RID: 31530 RVA: 0x0019B06D File Offset: 0x0019926D
		public static LocalizedString CustomInternalBodyRequired
		{
			get
			{
				return new LocalizedString("CustomInternalBodyRequired", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B2B RID: 31531 RVA: 0x0019B08C File Offset: 0x0019928C
		public static LocalizedString PartnerManaged_Violation(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("PartnerManaged_Violation", "ExD10332", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x17002C7F RID: 11391
		// (get) Token: 0x06007B2C RID: 31532 RVA: 0x0019B0C7 File Offset: 0x001992C7
		public static LocalizedString TlsDomainWithIncorrectTlsAuthLevel
		{
			get
			{
				return new LocalizedString("TlsDomainWithIncorrectTlsAuthLevel", "ExD05846", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C80 RID: 11392
		// (get) Token: 0x06007B2D RID: 31533 RVA: 0x0019B0E5 File Offset: 0x001992E5
		public static LocalizedString SystemTag
		{
			get
			{
				return new LocalizedString("SystemTag", "Ex238508", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B2E RID: 31534 RVA: 0x0019B104 File Offset: 0x00199304
		public static LocalizedString ConfigurationSettingsInvalidPriority(int priority)
		{
			return new LocalizedString("ConfigurationSettingsInvalidPriority", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				priority
			});
		}

		// Token: 0x17002C81 RID: 11393
		// (get) Token: 0x06007B2F RID: 31535 RVA: 0x0019B138 File Offset: 0x00199338
		public static LocalizedString AllMailboxContentMC
		{
			get
			{
				return new LocalizedString("AllMailboxContentMC", "Ex79A8C0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C82 RID: 11394
		// (get) Token: 0x06007B30 RID: 31536 RVA: 0x0019B156 File Offset: 0x00199356
		public static LocalizedString RemoteUserMailboxTypeDetails
		{
			get
			{
				return new LocalizedString("RemoteUserMailboxTypeDetails", "Ex5525D7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C83 RID: 11395
		// (get) Token: 0x06007B31 RID: 31537 RVA: 0x0019B174 File Offset: 0x00199374
		public static LocalizedString BluetoothDisable
		{
			get
			{
				return new LocalizedString("BluetoothDisable", "Ex869DB6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B32 RID: 31538 RVA: 0x0019B194 File Offset: 0x00199394
		public static LocalizedString ErrorParseCountryInfo(string name, int countrycode, string displayName)
		{
			return new LocalizedString("ErrorParseCountryInfo", "ExD0212C", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				name,
				countrycode,
				displayName
			});
		}

		// Token: 0x17002C84 RID: 11396
		// (get) Token: 0x06007B33 RID: 31539 RVA: 0x0019B1D0 File Offset: 0x001993D0
		public static LocalizedString ServerRoleLanguagePacks
		{
			get
			{
				return new LocalizedString("ServerRoleLanguagePacks", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B34 RID: 31540 RVA: 0x0019B1F0 File Offset: 0x001993F0
		public static LocalizedString ErrorMailTipTranslationCultureNotSupported(string culture)
		{
			return new LocalizedString("ErrorMailTipTranslationCultureNotSupported", "Ex22BD1F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				culture
			});
		}

		// Token: 0x06007B35 RID: 31541 RVA: 0x0019B220 File Offset: 0x00199420
		public static LocalizedString CantSetDialPlanProperty(string name)
		{
			return new LocalizedString("CantSetDialPlanProperty", "ExE8E1C7", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17002C85 RID: 11397
		// (get) Token: 0x06007B36 RID: 31542 RVA: 0x0019B24F File Offset: 0x0019944F
		public static LocalizedString PrincipalName
		{
			get
			{
				return new LocalizedString("PrincipalName", "Ex0C344B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B37 RID: 31543 RVA: 0x0019B270 File Offset: 0x00199470
		public static LocalizedString InvalidCustomGreetingFilename(string s)
		{
			return new LocalizedString("InvalidCustomGreetingFilename", "Ex4CF087", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x17002C86 RID: 11398
		// (get) Token: 0x06007B38 RID: 31544 RVA: 0x0019B29F File Offset: 0x0019949F
		public static LocalizedString IdIsNotSet
		{
			get
			{
				return new LocalizedString("IdIsNotSet", "Ex4120B4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C87 RID: 11399
		// (get) Token: 0x06007B39 RID: 31545 RVA: 0x0019B2BD File Offset: 0x001994BD
		public static LocalizedString ConstraintViolationSupervisionListEntryStringPartIsInvalid
		{
			get
			{
				return new LocalizedString("ConstraintViolationSupervisionListEntryStringPartIsInvalid", "ExA3BF0F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C88 RID: 11400
		// (get) Token: 0x06007B3A RID: 31546 RVA: 0x0019B2DB File Offset: 0x001994DB
		public static LocalizedString WellKnownRecipientTypeMailContacts
		{
			get
			{
				return new LocalizedString("WellKnownRecipientTypeMailContacts", "ExDF27E6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C89 RID: 11401
		// (get) Token: 0x06007B3B RID: 31547 RVA: 0x0019B2F9 File Offset: 0x001994F9
		public static LocalizedString ServerRoleHubTransport
		{
			get
			{
				return new LocalizedString("ServerRoleHubTransport", "ExDABB86", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C8A RID: 11402
		// (get) Token: 0x06007B3C RID: 31548 RVA: 0x0019B317 File Offset: 0x00199517
		public static LocalizedString IndustryHealthcare
		{
			get
			{
				return new LocalizedString("IndustryHealthcare", "ExF88F2F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B3D RID: 31549 RVA: 0x0019B338 File Offset: 0x00199538
		public static LocalizedString ExceptionCannotBindToDC(string server)
		{
			return new LocalizedString("ExceptionCannotBindToDC", "ExFAF7B3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17002C8B RID: 11403
		// (get) Token: 0x06007B3E RID: 31550 RVA: 0x0019B367 File Offset: 0x00199567
		public static LocalizedString CapabilityPartnerManaged
		{
			get
			{
				return new LocalizedString("CapabilityPartnerManaged", "Ex77CEE6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B3F RID: 31551 RVA: 0x0019B388 File Offset: 0x00199588
		public static LocalizedString ExceptionUnsupportedFilterForPropertyMultiple(string propertyName, Type filterType, Type supportedType1, Type supportedType2)
		{
			return new LocalizedString("ExceptionUnsupportedFilterForPropertyMultiple", "Ex70F37B", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				filterType,
				supportedType1,
				supportedType2
			});
		}

		// Token: 0x06007B40 RID: 31552 RVA: 0x0019B3C4 File Offset: 0x001995C4
		public static LocalizedString ExArgumentNullException(string paramName)
		{
			return new LocalizedString("ExArgumentNullException", "Ex6FB256", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				paramName
			});
		}

		// Token: 0x17002C8C RID: 11404
		// (get) Token: 0x06007B41 RID: 31553 RVA: 0x0019B3F3 File Offset: 0x001995F3
		public static LocalizedString ErrorArchiveDatabaseArchiveDomainMissing
		{
			get
			{
				return new LocalizedString("ErrorArchiveDatabaseArchiveDomainMissing", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C8D RID: 11405
		// (get) Token: 0x06007B42 RID: 31554 RVA: 0x0019B411 File Offset: 0x00199611
		public static LocalizedString MailEnabledUniversalSecurityGroupRecipientType
		{
			get
			{
				return new LocalizedString("MailEnabledUniversalSecurityGroupRecipientType", "ExBC0A67", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C8E RID: 11406
		// (get) Token: 0x06007B43 RID: 31555 RVA: 0x0019B42F File Offset: 0x0019962F
		public static LocalizedString ErrorRemovalNotSupported
		{
			get
			{
				return new LocalizedString("ErrorRemovalNotSupported", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C8F RID: 11407
		// (get) Token: 0x06007B44 RID: 31556 RVA: 0x0019B44D File Offset: 0x0019964D
		public static LocalizedString ExchangeFaxMC
		{
			get
			{
				return new LocalizedString("ExchangeFaxMC", "Ex0664DC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C90 RID: 11408
		// (get) Token: 0x06007B45 RID: 31557 RVA: 0x0019B46B File Offset: 0x0019966B
		public static LocalizedString ByteEncoderTypeUse7Bit
		{
			get
			{
				return new LocalizedString("ByteEncoderTypeUse7Bit", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C91 RID: 11409
		// (get) Token: 0x06007B46 RID: 31558 RVA: 0x0019B489 File Offset: 0x00199689
		public static LocalizedString InvalidBindingAddressSetting
		{
			get
			{
				return new LocalizedString("InvalidBindingAddressSetting", "Ex28BCF1", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C92 RID: 11410
		// (get) Token: 0x06007B47 RID: 31559 RVA: 0x0019B4A7 File Offset: 0x001996A7
		public static LocalizedString ASAccessMethodNeedsAuthenticationAccount
		{
			get
			{
				return new LocalizedString("ASAccessMethodNeedsAuthenticationAccount", "ExB071A0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B48 RID: 31560 RVA: 0x0019B4C8 File Offset: 0x001996C8
		public static LocalizedString ExceptionSearchRootNotWithinScope(string root, string scope)
		{
			return new LocalizedString("ExceptionSearchRootNotWithinScope", "Ex8B3BB7", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				root,
				scope
			});
		}

		// Token: 0x17002C93 RID: 11411
		// (get) Token: 0x06007B49 RID: 31561 RVA: 0x0019B4FB File Offset: 0x001996FB
		public static LocalizedString CanRunDefaultUpdateState_Allowed
		{
			get
			{
				return new LocalizedString("CanRunDefaultUpdateState_Allowed", "Ex873F7F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C94 RID: 11412
		// (get) Token: 0x06007B4A RID: 31562 RVA: 0x0019B519 File Offset: 0x00199719
		public static LocalizedString EsnLangMalay
		{
			get
			{
				return new LocalizedString("EsnLangMalay", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B4B RID: 31563 RVA: 0x0019B538 File Offset: 0x00199738
		public static LocalizedString ExceptionTimelimitExceeded(string server, TimeSpan serverTimeout)
		{
			return new LocalizedString("ExceptionTimelimitExceeded", "Ex2C7201", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				serverTimeout
			});
		}

		// Token: 0x17002C95 RID: 11413
		// (get) Token: 0x06007B4C RID: 31564 RVA: 0x0019B570 File Offset: 0x00199770
		public static LocalizedString FailedToParseAlternateServiceAccountCredential
		{
			get
			{
				return new LocalizedString("FailedToParseAlternateServiceAccountCredential", "Ex4C38F2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B4D RID: 31565 RVA: 0x0019B590 File Offset: 0x00199790
		public static LocalizedString ExceptionADRetryOnceOperationFailed(string server, string message)
		{
			return new LocalizedString("ExceptionADRetryOnceOperationFailed", "Ex872538", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				message
			});
		}

		// Token: 0x17002C96 RID: 11414
		// (get) Token: 0x06007B4E RID: 31566 RVA: 0x0019B5C3 File Offset: 0x001997C3
		public static LocalizedString ExternalManagedMailContactTypeDetails
		{
			get
			{
				return new LocalizedString("ExternalManagedMailContactTypeDetails", "Ex325B7F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C97 RID: 11415
		// (get) Token: 0x06007B4F RID: 31567 RVA: 0x0019B5E1 File Offset: 0x001997E1
		public static LocalizedString IPv6Only
		{
			get
			{
				return new LocalizedString("IPv6Only", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B50 RID: 31568 RVA: 0x0019B600 File Offset: 0x00199800
		public static LocalizedString ErrorNeutralCulture(string culture)
		{
			return new LocalizedString("ErrorNeutralCulture", "ExBA5E48", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				culture
			});
		}

		// Token: 0x06007B51 RID: 31569 RVA: 0x0019B630 File Offset: 0x00199830
		public static LocalizedString ExceptionInvalidOperationOnObject(string operation)
		{
			return new LocalizedString("ExceptionInvalidOperationOnObject", "Ex9CEE4B", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				operation
			});
		}

		// Token: 0x17002C98 RID: 11416
		// (get) Token: 0x06007B52 RID: 31570 RVA: 0x0019B65F File Offset: 0x0019985F
		public static LocalizedString MountDialOverrideLossless
		{
			get
			{
				return new LocalizedString("MountDialOverrideLossless", "Ex9E41A7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B53 RID: 31571 RVA: 0x0019B680 File Offset: 0x00199880
		public static LocalizedString ServerSideADTopologyServiceCallError(string server, string error)
		{
			return new LocalizedString("ServerSideADTopologyServiceCallError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				error
			});
		}

		// Token: 0x17002C99 RID: 11417
		// (get) Token: 0x06007B54 RID: 31572 RVA: 0x0019B6B3 File Offset: 0x001998B3
		public static LocalizedString Percent
		{
			get
			{
				return new LocalizedString("Percent", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B55 RID: 31573 RVA: 0x0019B6D4 File Offset: 0x001998D4
		public static LocalizedString ExceptionWin32OperationFailed(int error, string message)
		{
			return new LocalizedString("ExceptionWin32OperationFailed", "Ex8537A8", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				error,
				message
			});
		}

		// Token: 0x06007B56 RID: 31574 RVA: 0x0019B70C File Offset: 0x0019990C
		public static LocalizedString ErrorDCNotFound(string hostName)
		{
			return new LocalizedString("ErrorDCNotFound", "ExC55DB1", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				hostName
			});
		}

		// Token: 0x17002C9A RID: 11418
		// (get) Token: 0x06007B57 RID: 31575 RVA: 0x0019B73B File Offset: 0x0019993B
		public static LocalizedString ServerRoleProvisionedServer
		{
			get
			{
				return new LocalizedString("ServerRoleProvisionedServer", "ExA60544", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C9B RID: 11419
		// (get) Token: 0x06007B58 RID: 31576 RVA: 0x0019B759 File Offset: 0x00199959
		public static LocalizedString CalendarAgeFilterOneMonth
		{
			get
			{
				return new LocalizedString("CalendarAgeFilterOneMonth", "Ex2F881A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C9C RID: 11420
		// (get) Token: 0x06007B59 RID: 31577 RVA: 0x0019B777 File Offset: 0x00199977
		public static LocalizedString TextOnly
		{
			get
			{
				return new LocalizedString("TextOnly", "ExAAFCCE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B5A RID: 31578 RVA: 0x0019B798 File Offset: 0x00199998
		public static LocalizedString AddressBookNoSecurityDescriptor(string id)
		{
			return new LocalizedString("AddressBookNoSecurityDescriptor", "ExCEC884", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06007B5B RID: 31579 RVA: 0x0019B7C8 File Offset: 0x001999C8
		public static LocalizedString NotInWriteToMbxMode(string propName)
		{
			return new LocalizedString("NotInWriteToMbxMode", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				propName
			});
		}

		// Token: 0x06007B5C RID: 31580 RVA: 0x0019B7F8 File Offset: 0x001999F8
		public static LocalizedString ErrorAuthServerNotFound(string issuerIdentifier)
		{
			return new LocalizedString("ErrorAuthServerNotFound", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				issuerIdentifier
			});
		}

		// Token: 0x17002C9D RID: 11421
		// (get) Token: 0x06007B5D RID: 31581 RVA: 0x0019B827 File Offset: 0x00199A27
		public static LocalizedString InvalidMsgTrackingLogSizeConfiguration
		{
			get
			{
				return new LocalizedString("InvalidMsgTrackingLogSizeConfiguration", "ExEE9F08", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C9E RID: 11422
		// (get) Token: 0x06007B5E RID: 31582 RVA: 0x0019B845 File Offset: 0x00199A45
		public static LocalizedString ErrorArchiveDatabaseSetForNonArchive
		{
			get
			{
				return new LocalizedString("ErrorArchiveDatabaseSetForNonArchive", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002C9F RID: 11423
		// (get) Token: 0x06007B5F RID: 31583 RVA: 0x0019B863 File Offset: 0x00199A63
		public static LocalizedString InvalidGenerationTime
		{
			get
			{
				return new LocalizedString("InvalidGenerationTime", "ExEDFCB0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CA0 RID: 11424
		// (get) Token: 0x06007B60 RID: 31584 RVA: 0x0019B881 File Offset: 0x00199A81
		public static LocalizedString CalendarItemMC
		{
			get
			{
				return new LocalizedString("CalendarItemMC", "Ex925150", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B61 RID: 31585 RVA: 0x0019B8A0 File Offset: 0x00199AA0
		public static LocalizedString ErrorProductFileNameDifferentFromCopyFileName(string productFileName, string copyFileName)
		{
			return new LocalizedString("ErrorProductFileNameDifferentFromCopyFileName", "ExAC0BFA", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				productFileName,
				copyFileName
			});
		}

		// Token: 0x17002CA1 RID: 11425
		// (get) Token: 0x06007B62 RID: 31586 RVA: 0x0019B8D3 File Offset: 0x00199AD3
		public static LocalizedString Block
		{
			get
			{
				return new LocalizedString("Block", "Ex3B9A4D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B63 RID: 31587 RVA: 0x0019B8F4 File Offset: 0x00199AF4
		public static LocalizedString ValueNotInRange(int minValue, int maxValue)
		{
			return new LocalizedString("ValueNotInRange", "ExBFF981", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				minValue,
				maxValue
			});
		}

		// Token: 0x17002CA2 RID: 11426
		// (get) Token: 0x06007B64 RID: 31588 RVA: 0x0019B931 File Offset: 0x00199B31
		public static LocalizedString ErrorNullExternalEmailAddress
		{
			get
			{
				return new LocalizedString("ErrorNullExternalEmailAddress", "Ex693C8A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B65 RID: 31589 RVA: 0x0019B950 File Offset: 0x00199B50
		public static LocalizedString ErrorRemoteAccountPartitionMustHaveTrust(string id)
		{
			return new LocalizedString("ErrorRemoteAccountPartitionMustHaveTrust", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002CA3 RID: 11427
		// (get) Token: 0x06007B66 RID: 31590 RVA: 0x0019B97F File Offset: 0x00199B7F
		public static LocalizedString ExceptionRusNotRunning
		{
			get
			{
				return new LocalizedString("ExceptionRusNotRunning", "Ex5ACDCA", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CA4 RID: 11428
		// (get) Token: 0x06007B67 RID: 31591 RVA: 0x0019B99D File Offset: 0x00199B9D
		public static LocalizedString PropertyCannotBeSetToTest
		{
			get
			{
				return new LocalizedString("PropertyCannotBeSetToTest", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CA5 RID: 11429
		// (get) Token: 0x06007B68 RID: 31592 RVA: 0x0019B9BB File Offset: 0x00199BBB
		public static LocalizedString LdapFilterErrorInvalidEscaping
		{
			get
			{
				return new LocalizedString("LdapFilterErrorInvalidEscaping", "Ex5C0803", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CA6 RID: 11430
		// (get) Token: 0x06007B69 RID: 31593 RVA: 0x0019B9D9 File Offset: 0x00199BD9
		public static LocalizedString ForceSave
		{
			get
			{
				return new LocalizedString("ForceSave", "ExA22F93", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CA7 RID: 11431
		// (get) Token: 0x06007B6A RID: 31594 RVA: 0x0019B9F7 File Offset: 0x00199BF7
		public static LocalizedString LinkedRoomMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("LinkedRoomMailboxRecipientTypeDetails", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CA8 RID: 11432
		// (get) Token: 0x06007B6B RID: 31595 RVA: 0x0019BA15 File Offset: 0x00199C15
		public static LocalizedString DeleteUseCustomAlert
		{
			get
			{
				return new LocalizedString("DeleteUseCustomAlert", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CA9 RID: 11433
		// (get) Token: 0x06007B6C RID: 31596 RVA: 0x0019BA33 File Offset: 0x00199C33
		public static LocalizedString CannotDeserializePartitionHint
		{
			get
			{
				return new LocalizedString("CannotDeserializePartitionHint", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B6D RID: 31597 RVA: 0x0019BA54 File Offset: 0x00199C54
		public static LocalizedString ErrorMasterServerInvalid(string dbName)
		{
			return new LocalizedString("ErrorMasterServerInvalid", "Ex8D3ED6", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x17002CAA RID: 11434
		// (get) Token: 0x06007B6E RID: 31598 RVA: 0x0019BA83 File Offset: 0x00199C83
		public static LocalizedString InboundConnectorInvalidRestrictDomainsToIPAddresses
		{
			get
			{
				return new LocalizedString("InboundConnectorInvalidRestrictDomainsToIPAddresses", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B6F RID: 31599 RVA: 0x0019BAA4 File Offset: 0x00199CA4
		public static LocalizedString ErrorInvalidExecutingOrg(string execOrg, string currentOrg)
		{
			return new LocalizedString("ErrorInvalidExecutingOrg", "Ex7D102A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				execOrg,
				currentOrg
			});
		}

		// Token: 0x17002CAB RID: 11435
		// (get) Token: 0x06007B70 RID: 31600 RVA: 0x0019BAD7 File Offset: 0x00199CD7
		public static LocalizedString GroupNamingPolicyCustomAttribute14
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute14", "Ex355A75", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CAC RID: 11436
		// (get) Token: 0x06007B71 RID: 31601 RVA: 0x0019BAF5 File Offset: 0x00199CF5
		public static LocalizedString ContactRecipientType
		{
			get
			{
				return new LocalizedString("ContactRecipientType", "ExD04F8D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CAD RID: 11437
		// (get) Token: 0x06007B72 RID: 31602 RVA: 0x0019BB13 File Offset: 0x00199D13
		public static LocalizedString DomainSecureWithoutDNSRoutingEnabled
		{
			get
			{
				return new LocalizedString("DomainSecureWithoutDNSRoutingEnabled", "Ex973FDE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B73 RID: 31603 RVA: 0x0019BB34 File Offset: 0x00199D34
		public static LocalizedString ErrorInvalidMailboxProvisioningConstraint(string parserErrorString)
		{
			return new LocalizedString("ErrorInvalidMailboxProvisioningConstraint", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				parserErrorString
			});
		}

		// Token: 0x17002CAE RID: 11438
		// (get) Token: 0x06007B74 RID: 31604 RVA: 0x0019BB63 File Offset: 0x00199D63
		public static LocalizedString RunspaceServerSettingsChanged
		{
			get
			{
				return new LocalizedString("RunspaceServerSettingsChanged", "ExAC157D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CAF RID: 11439
		// (get) Token: 0x06007B75 RID: 31605 RVA: 0x0019BB81 File Offset: 0x00199D81
		public static LocalizedString EsnLangGreek
		{
			get
			{
				return new LocalizedString("EsnLangGreek", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B76 RID: 31606 RVA: 0x0019BBA0 File Offset: 0x00199DA0
		public static LocalizedString CannotMakePrimary(MservRecord record, string recordType)
		{
			return new LocalizedString("CannotMakePrimary", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				record,
				recordType
			});
		}

		// Token: 0x06007B77 RID: 31607 RVA: 0x0019BBD4 File Offset: 0x00199DD4
		public static LocalizedString MsaUserAlreadyExistsInGlsError(string msaUserNetId)
		{
			return new LocalizedString("MsaUserAlreadyExistsInGlsError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				msaUserNetId
			});
		}

		// Token: 0x17002CB0 RID: 11440
		// (get) Token: 0x06007B78 RID: 31608 RVA: 0x0019BC03 File Offset: 0x00199E03
		public static LocalizedString TooManyEntriesError
		{
			get
			{
				return new LocalizedString("TooManyEntriesError", "ExBA47A1", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B79 RID: 31609 RVA: 0x0019BC24 File Offset: 0x00199E24
		public static LocalizedString ServerComponentReadTimeout(int timeoutInSec)
		{
			return new LocalizedString("ServerComponentReadTimeout", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				timeoutInSec
			});
		}

		// Token: 0x06007B7A RID: 31610 RVA: 0x0019BC58 File Offset: 0x00199E58
		public static LocalizedString InvalidOABMapiPropertyParseStringException(string str)
		{
			return new LocalizedString("InvalidOABMapiPropertyParseStringException", "Ex9F9257", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				str
			});
		}

		// Token: 0x17002CB1 RID: 11441
		// (get) Token: 0x06007B7B RID: 31611 RVA: 0x0019BC87 File Offset: 0x00199E87
		public static LocalizedString OrganizationRelationshipMissingTargetApplicationUri
		{
			get
			{
				return new LocalizedString("OrganizationRelationshipMissingTargetApplicationUri", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B7C RID: 31612 RVA: 0x0019BCA8 File Offset: 0x00199EA8
		public static LocalizedString ErrorRecipientDoesNotExist(string id)
		{
			return new LocalizedString("ErrorRecipientDoesNotExist", "Ex07F5B3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002CB2 RID: 11442
		// (get) Token: 0x06007B7D RID: 31613 RVA: 0x0019BCD7 File Offset: 0x00199ED7
		public static LocalizedString ComputerRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("ComputerRecipientTypeDetails", "Ex38B480", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CB3 RID: 11443
		// (get) Token: 0x06007B7E RID: 31614 RVA: 0x0019BCF5 File Offset: 0x00199EF5
		public static LocalizedString Exchweb
		{
			get
			{
				return new LocalizedString("Exchweb", "Ex51FC39", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CB4 RID: 11444
		// (get) Token: 0x06007B7F RID: 31615 RVA: 0x0019BD13 File Offset: 0x00199F13
		public static LocalizedString OutboundConnectorIncorrectRouteAllMessagesViaOnPremises
		{
			get
			{
				return new LocalizedString("OutboundConnectorIncorrectRouteAllMessagesViaOnPremises", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CB5 RID: 11445
		// (get) Token: 0x06007B80 RID: 31616 RVA: 0x0019BD31 File Offset: 0x00199F31
		public static LocalizedString CalendarSharingFreeBusyAvailabilityOnly
		{
			get
			{
				return new LocalizedString("CalendarSharingFreeBusyAvailabilityOnly", "Ex3C5AA4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B81 RID: 31617 RVA: 0x0019BD50 File Offset: 0x00199F50
		public static LocalizedString CannotCompareScopeObjects(string leftId, string rightId)
		{
			return new LocalizedString("CannotCompareScopeObjects", "Ex8E5470", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				leftId,
				rightId
			});
		}

		// Token: 0x17002CB6 RID: 11446
		// (get) Token: 0x06007B82 RID: 31618 RVA: 0x0019BD83 File Offset: 0x00199F83
		public static LocalizedString ServerRoleExtendedRole5
		{
			get
			{
				return new LocalizedString("ServerRoleExtendedRole5", "Ex71E13E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B83 RID: 31619 RVA: 0x0019BDA4 File Offset: 0x00199FA4
		public static LocalizedString ExceptionADTopologyErrorWhenLookingForServersInDomain(int error, string domain, string message)
		{
			return new LocalizedString("ExceptionADTopologyErrorWhenLookingForServersInDomain", "Ex4D4CEC", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				error,
				domain,
				message
			});
		}

		// Token: 0x17002CB7 RID: 11447
		// (get) Token: 0x06007B84 RID: 31620 RVA: 0x0019BDE0 File Offset: 0x00199FE0
		public static LocalizedString AutoAttendantLink
		{
			get
			{
				return new LocalizedString("AutoAttendantLink", "Ex17EEE4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CB8 RID: 11448
		// (get) Token: 0x06007B85 RID: 31621 RVA: 0x0019BDFE File Offset: 0x00199FFE
		public static LocalizedString CustomRoleDescription_MyDisplayName
		{
			get
			{
				return new LocalizedString("CustomRoleDescription_MyDisplayName", "Ex29C8C7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CB9 RID: 11449
		// (get) Token: 0x06007B86 RID: 31622 RVA: 0x0019BE1C File Offset: 0x0019A01C
		public static LocalizedString AllUsers
		{
			get
			{
				return new LocalizedString("AllUsers", "Ex038FB6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CBA RID: 11450
		// (get) Token: 0x06007B87 RID: 31623 RVA: 0x0019BE3A File Offset: 0x0019A03A
		public static LocalizedString All
		{
			get
			{
				return new LocalizedString("All", "ExCCDF22", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B88 RID: 31624 RVA: 0x0019BE58 File Offset: 0x0019A058
		public static LocalizedString CannotFindTenantCUByAcceptedDomain(string domain)
		{
			return new LocalizedString("CannotFindTenantCUByAcceptedDomain", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x06007B89 RID: 31625 RVA: 0x0019BE88 File Offset: 0x0019A088
		public static LocalizedString ComposedSuitabilityReachabilityError(string fqdn, string details)
		{
			return new LocalizedString("ComposedSuitabilityReachabilityError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				fqdn,
				details
			});
		}

		// Token: 0x06007B8A RID: 31626 RVA: 0x0019BEBC File Offset: 0x0019A0BC
		public static LocalizedString ErrorNoWriteScope(string identity)
		{
			return new LocalizedString("ErrorNoWriteScope", "ExE5C228", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06007B8B RID: 31627 RVA: 0x0019BEEC File Offset: 0x0019A0EC
		public static LocalizedString ErrorNonUniqueDN(string DN)
		{
			return new LocalizedString("ErrorNonUniqueDN", "ExD784D5", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				DN
			});
		}

		// Token: 0x17002CBB RID: 11451
		// (get) Token: 0x06007B8C RID: 31628 RVA: 0x0019BF1B File Offset: 0x0019A11B
		public static LocalizedString OrganizationCapabilityMigration
		{
			get
			{
				return new LocalizedString("OrganizationCapabilityMigration", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CBC RID: 11452
		// (get) Token: 0x06007B8D RID: 31629 RVA: 0x0019BF39 File Offset: 0x0019A139
		public static LocalizedString DialPlan
		{
			get
			{
				return new LocalizedString("DialPlan", "ExB5CB6C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CBD RID: 11453
		// (get) Token: 0x06007B8E RID: 31630 RVA: 0x0019BF57 File Offset: 0x0019A157
		public static LocalizedString EsnLangUkrainian
		{
			get
			{
				return new LocalizedString("EsnLangUkrainian", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B8F RID: 31631 RVA: 0x0019BF78 File Offset: 0x0019A178
		public static LocalizedString CannotCompareAssignmentsMissingScope(string leftId, string rightId)
		{
			return new LocalizedString("CannotCompareAssignmentsMissingScope", "Ex6E6E9F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				leftId,
				rightId
			});
		}

		// Token: 0x06007B90 RID: 31632 RVA: 0x0019BFAC File Offset: 0x0019A1AC
		public static LocalizedString InvalidInfluence(Influence influence)
		{
			return new LocalizedString("InvalidInfluence", "ExF307D3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				influence
			});
		}

		// Token: 0x17002CBE RID: 11454
		// (get) Token: 0x06007B91 RID: 31633 RVA: 0x0019BFE0 File Offset: 0x0019A1E0
		public static LocalizedString MessageRateSourceFlagsNone
		{
			get
			{
				return new LocalizedString("MessageRateSourceFlagsNone", "ExB119D6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B92 RID: 31634 RVA: 0x0019C000 File Offset: 0x0019A200
		public static LocalizedString EOPPremiumRestrictions_Error(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("EOPPremiumRestrictions_Error", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x17002CBF RID: 11455
		// (get) Token: 0x06007B93 RID: 31635 RVA: 0x0019C03B File Offset: 0x0019A23B
		public static LocalizedString IndustryLegal
		{
			get
			{
				return new LocalizedString("IndustryLegal", "ExDF1C27", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CC0 RID: 11456
		// (get) Token: 0x06007B94 RID: 31636 RVA: 0x0019C059 File Offset: 0x0019A259
		public static LocalizedString CapabilityUMFeatureRestricted
		{
			get
			{
				return new LocalizedString("CapabilityUMFeatureRestricted", "Ex64A8C5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CC1 RID: 11457
		// (get) Token: 0x06007B95 RID: 31637 RVA: 0x0019C077 File Offset: 0x0019A277
		public static LocalizedString GroupTypeFlagsBuiltinLocal
		{
			get
			{
				return new LocalizedString("GroupTypeFlagsBuiltinLocal", "Ex288CF4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CC2 RID: 11458
		// (get) Token: 0x06007B96 RID: 31638 RVA: 0x0019C095 File Offset: 0x0019A295
		public static LocalizedString ReceiveAuthMechanismBasicAuthPlusTls
		{
			get
			{
				return new LocalizedString("ReceiveAuthMechanismBasicAuthPlusTls", "ExC96519", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CC3 RID: 11459
		// (get) Token: 0x06007B97 RID: 31639 RVA: 0x0019C0B3 File Offset: 0x0019A2B3
		public static LocalizedString Allowed
		{
			get
			{
				return new LocalizedString("Allowed", "Ex054DEC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CC4 RID: 11460
		// (get) Token: 0x06007B98 RID: 31640 RVA: 0x0019C0D1 File Offset: 0x0019A2D1
		public static LocalizedString ByteEncoderTypeUseQPHtml7BitTextPlain
		{
			get
			{
				return new LocalizedString("ByteEncoderTypeUseQPHtml7BitTextPlain", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B99 RID: 31641 RVA: 0x0019C0F0 File Offset: 0x0019A2F0
		public static LocalizedString AmbiguousTimeZoneNameError(string etz)
		{
			return new LocalizedString("AmbiguousTimeZoneNameError", "Ex4EE412", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				etz
			});
		}

		// Token: 0x17002CC5 RID: 11461
		// (get) Token: 0x06007B9A RID: 31642 RVA: 0x0019C11F File Offset: 0x0019A31F
		public static LocalizedString High
		{
			get
			{
				return new LocalizedString("High", "Ex278F98", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CC6 RID: 11462
		// (get) Token: 0x06007B9B RID: 31643 RVA: 0x0019C13D File Offset: 0x0019A33D
		public static LocalizedString MicrosoftExchangeRecipientType
		{
			get
			{
				return new LocalizedString("MicrosoftExchangeRecipientType", "Ex6EE737", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CC7 RID: 11463
		// (get) Token: 0x06007B9C RID: 31644 RVA: 0x0019C15B File Offset: 0x0019A35B
		public static LocalizedString BackSyncDataSourceUnavailableMessage
		{
			get
			{
				return new LocalizedString("BackSyncDataSourceUnavailableMessage", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CC8 RID: 11464
		// (get) Token: 0x06007B9D RID: 31645 RVA: 0x0019C179 File Offset: 0x0019A379
		public static LocalizedString ArchiveStateOnPremise
		{
			get
			{
				return new LocalizedString("ArchiveStateOnPremise", "Ex7043B8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007B9E RID: 31646 RVA: 0x0019C198 File Offset: 0x0019A398
		public static LocalizedString InvalidRoleEntryType(string entry, RoleType roleType, ManagementRoleEntryType roleEntryType)
		{
			return new LocalizedString("InvalidRoleEntryType", "Ex77A26E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				entry,
				roleType,
				roleEntryType
			});
		}

		// Token: 0x17002CC9 RID: 11465
		// (get) Token: 0x06007B9F RID: 31647 RVA: 0x0019C1D9 File Offset: 0x0019A3D9
		public static LocalizedString OrganizationCapabilitySuiteServiceStorage
		{
			get
			{
				return new LocalizedString("OrganizationCapabilitySuiteServiceStorage", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CCA RID: 11466
		// (get) Token: 0x06007BA0 RID: 31648 RVA: 0x0019C1F7 File Offset: 0x0019A3F7
		public static LocalizedString MalwareScanErrorActionBlock
		{
			get
			{
				return new LocalizedString("MalwareScanErrorActionBlock", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CCB RID: 11467
		// (get) Token: 0x06007BA1 RID: 31649 RVA: 0x0019C215 File Offset: 0x0019A415
		public static LocalizedString SKUCapabilityBPOSSArchiveAddOn
		{
			get
			{
				return new LocalizedString("SKUCapabilityBPOSSArchiveAddOn", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BA2 RID: 31650 RVA: 0x0019C234 File Offset: 0x0019A434
		public static LocalizedString MailboxPropertiesMustBeClearedFirst(MservRecord record)
		{
			return new LocalizedString("MailboxPropertiesMustBeClearedFirst", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				record
			});
		}

		// Token: 0x17002CCC RID: 11468
		// (get) Token: 0x06007BA3 RID: 31651 RVA: 0x0019C263 File Offset: 0x0019A463
		public static LocalizedString ExceptionRusAccessDenied
		{
			get
			{
				return new LocalizedString("ExceptionRusAccessDenied", "Ex4E1FB2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BA4 RID: 31652 RVA: 0x0019C284 File Offset: 0x0019A484
		public static LocalizedString UnexpectedGlsError(string message)
		{
			return new LocalizedString("UnexpectedGlsError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17002CCD RID: 11469
		// (get) Token: 0x06007BA5 RID: 31653 RVA: 0x0019C2B3 File Offset: 0x0019A4B3
		public static LocalizedString ServerRoleNone
		{
			get
			{
				return new LocalizedString("ServerRoleNone", "Ex4DBF20", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CCE RID: 11470
		// (get) Token: 0x06007BA6 RID: 31654 RVA: 0x0019C2D1 File Offset: 0x0019A4D1
		public static LocalizedString AlternateServiceAccountConfigurationDisplayFormatMoreDataAvailable
		{
			get
			{
				return new LocalizedString("AlternateServiceAccountConfigurationDisplayFormatMoreDataAvailable", "Ex32595D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BA7 RID: 31655 RVA: 0x0019C2F0 File Offset: 0x0019A4F0
		public static LocalizedString NotInWriteToMServMode(string propName)
		{
			return new LocalizedString("NotInWriteToMServMode", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				propName
			});
		}

		// Token: 0x17002CCF RID: 11471
		// (get) Token: 0x06007BA8 RID: 31656 RVA: 0x0019C31F File Offset: 0x0019A51F
		public static LocalizedString GloballyDistributedOABCacheWriteTimeoutError
		{
			get
			{
				return new LocalizedString("GloballyDistributedOABCacheWriteTimeoutError", "Ex45DB87", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BA9 RID: 31657 RVA: 0x0019C340 File Offset: 0x0019A540
		public static LocalizedString ErrorEmptyString(string propertyName)
		{
			return new LocalizedString("ErrorEmptyString", "Ex9EA7D3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x17002CD0 RID: 11472
		// (get) Token: 0x06007BAA RID: 31658 RVA: 0x0019C36F File Offset: 0x0019A56F
		public static LocalizedString UserName
		{
			get
			{
				return new LocalizedString("UserName", "ExAC819B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CD1 RID: 11473
		// (get) Token: 0x06007BAB RID: 31659 RVA: 0x0019C38D File Offset: 0x0019A58D
		public static LocalizedString Reserved1
		{
			get
			{
				return new LocalizedString("Reserved1", "ExE974A4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BAC RID: 31660 RVA: 0x0019C3AC File Offset: 0x0019A5AC
		public static LocalizedString ExceptionSizelimitExceeded(string server)
		{
			return new LocalizedString("ExceptionSizelimitExceeded", "ExAE3319", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17002CD2 RID: 11474
		// (get) Token: 0x06007BAD RID: 31661 RVA: 0x0019C3DB File Offset: 0x0019A5DB
		public static LocalizedString NoAddresses
		{
			get
			{
				return new LocalizedString("NoAddresses", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CD3 RID: 11475
		// (get) Token: 0x06007BAE RID: 31662 RVA: 0x0019C3F9 File Offset: 0x0019A5F9
		public static LocalizedString RegionBlockListNotSet
		{
			get
			{
				return new LocalizedString("RegionBlockListNotSet", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CD4 RID: 11476
		// (get) Token: 0x06007BAF RID: 31663 RVA: 0x0019C417 File Offset: 0x0019A617
		public static LocalizedString CapabilityRichCoexistence
		{
			get
			{
				return new LocalizedString("CapabilityRichCoexistence", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CD5 RID: 11477
		// (get) Token: 0x06007BB0 RID: 31664 RVA: 0x0019C435 File Offset: 0x0019A635
		public static LocalizedString ErrorUserAccountNameIncludeAt
		{
			get
			{
				return new LocalizedString("ErrorUserAccountNameIncludeAt", "Ex055DD6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CD6 RID: 11478
		// (get) Token: 0x06007BB1 RID: 31665 RVA: 0x0019C453 File Offset: 0x0019A653
		public static LocalizedString Enabled
		{
			get
			{
				return new LocalizedString("Enabled", "ExA964BC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CD7 RID: 11479
		// (get) Token: 0x06007BB2 RID: 31666 RVA: 0x0019C471 File Offset: 0x0019A671
		public static LocalizedString AttachmentsWereRemovedMessage
		{
			get
			{
				return new LocalizedString("AttachmentsWereRemovedMessage", "ExD7F183", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CD8 RID: 11480
		// (get) Token: 0x06007BB3 RID: 31667 RVA: 0x0019C48F File Offset: 0x0019A68F
		public static LocalizedString ErrorCannotFindUnusedLegacyDN
		{
			get
			{
				return new LocalizedString("ErrorCannotFindUnusedLegacyDN", "ExA00F16", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CD9 RID: 11481
		// (get) Token: 0x06007BB4 RID: 31668 RVA: 0x0019C4AD File Offset: 0x0019A6AD
		public static LocalizedString EmailAgeFilterOneWeek
		{
			get
			{
				return new LocalizedString("EmailAgeFilterOneWeek", "Ex91148D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CDA RID: 11482
		// (get) Token: 0x06007BB5 RID: 31669 RVA: 0x0019C4CB File Offset: 0x0019A6CB
		public static LocalizedString GroupNameInNamingPolicy
		{
			get
			{
				return new LocalizedString("GroupNameInNamingPolicy", "Ex616D28", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BB6 RID: 31670 RVA: 0x0019C4EC File Offset: 0x0019A6EC
		public static LocalizedString ErrorRealmFormatInvalid(string realm)
		{
			return new LocalizedString("ErrorRealmFormatInvalid", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				realm
			});
		}

		// Token: 0x17002CDB RID: 11483
		// (get) Token: 0x06007BB7 RID: 31671 RVA: 0x0019C51B File Offset: 0x0019A71B
		public static LocalizedString OrganizationCapabilityClientExtensions
		{
			get
			{
				return new LocalizedString("OrganizationCapabilityClientExtensions", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CDC RID: 11484
		// (get) Token: 0x06007BB8 RID: 31672 RVA: 0x0019C539 File Offset: 0x0019A739
		public static LocalizedString CalendarAgeFilterTwoWeeks
		{
			get
			{
				return new LocalizedString("CalendarAgeFilterTwoWeeks", "Ex7874E3", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BB9 RID: 31673 RVA: 0x0019C558 File Offset: 0x0019A758
		public static LocalizedString ConfigurationSettingsGroupExists(string name)
		{
			return new LocalizedString("ConfigurationSettingsGroupExists", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06007BBA RID: 31674 RVA: 0x0019C588 File Offset: 0x0019A788
		public static LocalizedString ExceptionADOperationFailedAlreadyExist(string server, string dn)
		{
			return new LocalizedString("ExceptionADOperationFailedAlreadyExist", "Ex17CDAE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				dn
			});
		}

		// Token: 0x17002CDD RID: 11485
		// (get) Token: 0x06007BBB RID: 31675 RVA: 0x0019C5BB File Offset: 0x0019A7BB
		public static LocalizedString ErrorElcCommentNotAllowed
		{
			get
			{
				return new LocalizedString("ErrorElcCommentNotAllowed", "Ex1FBDE8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CDE RID: 11486
		// (get) Token: 0x06007BBC RID: 31676 RVA: 0x0019C5D9 File Offset: 0x0019A7D9
		public static LocalizedString ErrorOwnersUpdated
		{
			get
			{
				return new LocalizedString("ErrorOwnersUpdated", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CDF RID: 11487
		// (get) Token: 0x06007BBD RID: 31677 RVA: 0x0019C5F7 File Offset: 0x0019A7F7
		public static LocalizedString EsnLangIndonesian
		{
			get
			{
				return new LocalizedString("EsnLangIndonesian", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BBE RID: 31678 RVA: 0x0019C618 File Offset: 0x0019A818
		public static LocalizedString InvalidEndpointAddressErrorMessage(string exceptionType, string wcfEndpoint)
		{
			return new LocalizedString("InvalidEndpointAddressErrorMessage", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				exceptionType,
				wcfEndpoint
			});
		}

		// Token: 0x17002CE0 RID: 11488
		// (get) Token: 0x06007BBF RID: 31679 RVA: 0x0019C64B File Offset: 0x0019A84B
		public static LocalizedString Extension
		{
			get
			{
				return new LocalizedString("Extension", "ExFED42A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BC0 RID: 31680 RVA: 0x0019C66C File Offset: 0x0019A86C
		public static LocalizedString ErrorAuthServerTypeNotFound(string type)
		{
			return new LocalizedString("ErrorAuthServerTypeNotFound", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x17002CE1 RID: 11489
		// (get) Token: 0x06007BC1 RID: 31681 RVA: 0x0019C69B File Offset: 0x0019A89B
		public static LocalizedString CanEnableLocalCopyState_Invalid
		{
			get
			{
				return new LocalizedString("CanEnableLocalCopyState_Invalid", "Ex268363", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BC2 RID: 31682 RVA: 0x0019C6BC File Offset: 0x0019A8BC
		public static LocalizedString IsMemberOfQueryFailed(string group)
		{
			return new LocalizedString("IsMemberOfQueryFailed", "Ex249393", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				group
			});
		}

		// Token: 0x17002CE2 RID: 11490
		// (get) Token: 0x06007BC3 RID: 31683 RVA: 0x0019C6EB File Offset: 0x0019A8EB
		public static LocalizedString MailEnabledUniversalDistributionGroupRecipientType
		{
			get
			{
				return new LocalizedString("MailEnabledUniversalDistributionGroupRecipientType", "ExBC6DD1", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CE3 RID: 11491
		// (get) Token: 0x06007BC4 RID: 31684 RVA: 0x0019C709 File Offset: 0x0019A909
		public static LocalizedString ReceiveCredentialIsNull
		{
			get
			{
				return new LocalizedString("ReceiveCredentialIsNull", "Ex843842", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BC5 RID: 31685 RVA: 0x0019C728 File Offset: 0x0019A928
		public static LocalizedString ExceptionADTopologyErrorWhenLookingForSite(int error, string siteName, string message)
		{
			return new LocalizedString("ExceptionADTopologyErrorWhenLookingForSite", "Ex30CCEF", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				error,
				siteName,
				message
			});
		}

		// Token: 0x06007BC6 RID: 31686 RVA: 0x0019C764 File Offset: 0x0019A964
		public static LocalizedString ErrorNotResettableProperty(string propertyName, string value)
		{
			return new LocalizedString("ErrorNotResettableProperty", "Ex989D1A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				value
			});
		}

		// Token: 0x06007BC7 RID: 31687 RVA: 0x0019C798 File Offset: 0x0019A998
		public static LocalizedString ErrorMailTipHtmlCorrupt(string message)
		{
			return new LocalizedString("ErrorMailTipHtmlCorrupt", "ExB99CE5", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x06007BC8 RID: 31688 RVA: 0x0019C7C8 File Offset: 0x0019A9C8
		public static LocalizedString ErrorInvalidOrganizationId(string dn, string ou, string cu)
		{
			return new LocalizedString("ErrorInvalidOrganizationId", "Ex0510A3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				dn,
				ou,
				cu
			});
		}

		// Token: 0x06007BC9 RID: 31689 RVA: 0x0019C800 File Offset: 0x0019AA00
		public static LocalizedString ErrorConversionFailed(string name)
		{
			return new LocalizedString("ErrorConversionFailed", "Ex50F99E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17002CE4 RID: 11492
		// (get) Token: 0x06007BCA RID: 31690 RVA: 0x0019C82F File Offset: 0x0019AA2F
		public static LocalizedString EsnLangLithuanian
		{
			get
			{
				return new LocalizedString("EsnLangLithuanian", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BCB RID: 31691 RVA: 0x0019C850 File Offset: 0x0019AA50
		public static LocalizedString ResourceMailbox_Violation(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("ResourceMailbox_Violation", "ExD234A9", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x17002CE5 RID: 11493
		// (get) Token: 0x06007BCC RID: 31692 RVA: 0x0019C88B File Offset: 0x0019AA8B
		public static LocalizedString ServerRoleAll
		{
			get
			{
				return new LocalizedString("ServerRoleAll", "ExCAD036", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CE6 RID: 11494
		// (get) Token: 0x06007BCD RID: 31693 RVA: 0x0019C8A9 File Offset: 0x0019AAA9
		public static LocalizedString ServerRoleEdge
		{
			get
			{
				return new LocalizedString("ServerRoleEdge", "ExCE4073", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BCE RID: 31694 RVA: 0x0019C8C8 File Offset: 0x0019AAC8
		public static LocalizedString DomainNotFoundInGlsError(string domain)
		{
			return new LocalizedString("DomainNotFoundInGlsError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x17002CE7 RID: 11495
		// (get) Token: 0x06007BCF RID: 31695 RVA: 0x0019C8F7 File Offset: 0x0019AAF7
		public static LocalizedString ExceptionObjectStillExists
		{
			get
			{
				return new LocalizedString("ExceptionObjectStillExists", "Ex8EA36A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CE8 RID: 11496
		// (get) Token: 0x06007BD0 RID: 31696 RVA: 0x0019C915 File Offset: 0x0019AB15
		public static LocalizedString AllRecipients
		{
			get
			{
				return new LocalizedString("AllRecipients", "ExDA64AB", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CE9 RID: 11497
		// (get) Token: 0x06007BD1 RID: 31697 RVA: 0x0019C933 File Offset: 0x0019AB33
		public static LocalizedString LdapFilterErrorNoAttributeType
		{
			get
			{
				return new LocalizedString("LdapFilterErrorNoAttributeType", "Ex723877", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CEA RID: 11498
		// (get) Token: 0x06007BD2 RID: 31698 RVA: 0x0019C951 File Offset: 0x0019AB51
		public static LocalizedString ServerRoleManagementFrontEnd
		{
			get
			{
				return new LocalizedString("ServerRoleManagementFrontEnd", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CEB RID: 11499
		// (get) Token: 0x06007BD3 RID: 31699 RVA: 0x0019C96F File Offset: 0x0019AB6F
		public static LocalizedString False
		{
			get
			{
				return new LocalizedString("False", "ExB6CDA4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BD4 RID: 31700 RVA: 0x0019C990 File Offset: 0x0019AB90
		public static LocalizedString ExceptionSchemaMismatch(string attributeName, bool isBinaryInADRecipient, bool isMultiValuedInADRecipient, bool isBinaryInRUS, bool isMultiValuedInRus)
		{
			return new LocalizedString("ExceptionSchemaMismatch", "Ex20DF9F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				attributeName,
				isBinaryInADRecipient,
				isMultiValuedInADRecipient,
				isBinaryInRUS,
				isMultiValuedInRus
			});
		}

		// Token: 0x06007BD5 RID: 31701 RVA: 0x0019C9E4 File Offset: 0x0019ABE4
		public static LocalizedString EapDuplicatedEmailAddressTemplate(string emailAddressTemplate)
		{
			return new LocalizedString("EapDuplicatedEmailAddressTemplate", "Ex876DD7", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				emailAddressTemplate
			});
		}

		// Token: 0x06007BD6 RID: 31702 RVA: 0x0019CA14 File Offset: 0x0019AC14
		public static LocalizedString NspiFailureException(int status)
		{
			return new LocalizedString("NspiFailureException", "Ex266E5C", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				status
			});
		}

		// Token: 0x06007BD7 RID: 31703 RVA: 0x0019CA48 File Offset: 0x0019AC48
		public static LocalizedString EapMustHaveOnePrimaryAddressTemplate(string prefix)
		{
			return new LocalizedString("EapMustHaveOnePrimaryAddressTemplate", "ExB9B6A1", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				prefix
			});
		}

		// Token: 0x17002CEC RID: 11500
		// (get) Token: 0x06007BD8 RID: 31704 RVA: 0x0019CA77 File Offset: 0x0019AC77
		public static LocalizedString CalendarSharingFreeBusyLimitedDetails
		{
			get
			{
				return new LocalizedString("CalendarSharingFreeBusyLimitedDetails", "ExFC8395", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CED RID: 11501
		// (get) Token: 0x06007BD9 RID: 31705 RVA: 0x0019CA95 File Offset: 0x0019AC95
		public static LocalizedString SystemAttendantMailboxRecipientType
		{
			get
			{
				return new LocalizedString("SystemAttendantMailboxRecipientType", "Ex926652", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CEE RID: 11502
		// (get) Token: 0x06007BDA RID: 31706 RVA: 0x0019CAB3 File Offset: 0x0019ACB3
		public static LocalizedString ServerRoleManagementBackEnd
		{
			get
			{
				return new LocalizedString("ServerRoleManagementBackEnd", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BDB RID: 31707 RVA: 0x0019CAD4 File Offset: 0x0019ACD4
		public static LocalizedString SuitabilityDirectoryException(string fqdn, int error, string errorMessage)
		{
			return new LocalizedString("SuitabilityDirectoryException", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				fqdn,
				error,
				errorMessage
			});
		}

		// Token: 0x06007BDC RID: 31708 RVA: 0x0019CB10 File Offset: 0x0019AD10
		public static LocalizedString ExceptionADTopologyHasNoAvailableServersInForest(string forest)
		{
			return new LocalizedString("ExceptionADTopologyHasNoAvailableServersInForest", "Ex4C422F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				forest
			});
		}

		// Token: 0x06007BDD RID: 31709 RVA: 0x0019CB40 File Offset: 0x0019AD40
		public static LocalizedString InvalidRecipientScope(object scope)
		{
			return new LocalizedString("InvalidRecipientScope", "Ex578D9C", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scope
			});
		}

		// Token: 0x06007BDE RID: 31710 RVA: 0x0019CB70 File Offset: 0x0019AD70
		public static LocalizedString UnableToResolveMapiPropertyNameException(string name)
		{
			return new LocalizedString("UnableToResolveMapiPropertyNameException", "ExD716B7", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17002CEF RID: 11503
		// (get) Token: 0x06007BDF RID: 31711 RVA: 0x0019CB9F File Offset: 0x0019AD9F
		public static LocalizedString GroupNamingPolicyStateOrProvince
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyStateOrProvince", "Ex7CC623", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CF0 RID: 11504
		// (get) Token: 0x06007BE0 RID: 31712 RVA: 0x0019CBBD File Offset: 0x0019ADBD
		public static LocalizedString IndustryFinance
		{
			get
			{
				return new LocalizedString("IndustryFinance", "Ex53C08F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CF1 RID: 11505
		// (get) Token: 0x06007BE1 RID: 31713 RVA: 0x0019CBDB File Offset: 0x0019ADDB
		public static LocalizedString ErrorAgeLimitExpiration
		{
			get
			{
				return new LocalizedString("ErrorAgeLimitExpiration", "ExD679C7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BE2 RID: 31714 RVA: 0x0019CBFC File Offset: 0x0019ADFC
		public static LocalizedString InvalidOABMapiPropertyTypeException(string type)
		{
			return new LocalizedString("InvalidOABMapiPropertyTypeException", "ExA57B43", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x17002CF2 RID: 11506
		// (get) Token: 0x06007BE3 RID: 31715 RVA: 0x0019CC2B File Offset: 0x0019AE2B
		public static LocalizedString InboundConnectorMissingTlsCertificateOrSenderIP
		{
			get
			{
				return new LocalizedString("InboundConnectorMissingTlsCertificateOrSenderIP", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CF3 RID: 11507
		// (get) Token: 0x06007BE4 RID: 31716 RVA: 0x0019CC49 File Offset: 0x0019AE49
		public static LocalizedString ErrorMailTipTranslationFormatIncorrect
		{
			get
			{
				return new LocalizedString("ErrorMailTipTranslationFormatIncorrect", "ExE140A5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CF4 RID: 11508
		// (get) Token: 0x06007BE5 RID: 31717 RVA: 0x0019CC67 File Offset: 0x0019AE67
		public static LocalizedString MountDialOverrideGoodAvailability
		{
			get
			{
				return new LocalizedString("MountDialOverrideGoodAvailability", "ExCDE6CC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BE6 RID: 31718 RVA: 0x0019CC88 File Offset: 0x0019AE88
		public static LocalizedString ErrorInvalidMailboxProvisioningAttribute(string attribute)
		{
			return new LocalizedString("ErrorInvalidMailboxProvisioningAttribute", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				attribute
			});
		}

		// Token: 0x17002CF5 RID: 11509
		// (get) Token: 0x06007BE7 RID: 31719 RVA: 0x0019CCB7 File Offset: 0x0019AEB7
		public static LocalizedString ConfigReadScope
		{
			get
			{
				return new LocalizedString("ConfigReadScope", "Ex78877B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BE8 RID: 31720 RVA: 0x0019CCD8 File Offset: 0x0019AED8
		public static LocalizedString ErrorMailboxProvisioningAttributeDoesNotMatchSchema(string keyName, string validKeys)
		{
			return new LocalizedString("ErrorMailboxProvisioningAttributeDoesNotMatchSchema", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				keyName,
				validKeys
			});
		}

		// Token: 0x06007BE9 RID: 31721 RVA: 0x0019CD0C File Offset: 0x0019AF0C
		public static LocalizedString ErrorMultiplePrimaries(string prefix)
		{
			return new LocalizedString("ErrorMultiplePrimaries", "Ex0D6BFD", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				prefix
			});
		}

		// Token: 0x06007BEA RID: 31722 RVA: 0x0019CD3C File Offset: 0x0019AF3C
		public static LocalizedString ErrorMinAdminVersionNull(ExchangeObjectVersion exchangeVersion)
		{
			return new LocalizedString("ErrorMinAdminVersionNull", "Ex7D5EA6", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				exchangeVersion
			});
		}

		// Token: 0x17002CF6 RID: 11510
		// (get) Token: 0x06007BEB RID: 31723 RVA: 0x0019CD6B File Offset: 0x0019AF6B
		public static LocalizedString UserRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("UserRecipientTypeDetails", "Ex97118C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BEC RID: 31724 RVA: 0x0019CD8C File Offset: 0x0019AF8C
		public static LocalizedString MismatchedMapiPropertyTypesException(int prop1, int prop2)
		{
			return new LocalizedString("MismatchedMapiPropertyTypesException", "Ex3D3F48", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				prop1,
				prop2
			});
		}

		// Token: 0x06007BED RID: 31725 RVA: 0x0019CDCC File Offset: 0x0019AFCC
		public static LocalizedString ServerHasNotBeenFound(int versionNumber, string identifier, bool needsExactVersionMatch, string siteName)
		{
			return new LocalizedString("ServerHasNotBeenFound", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				versionNumber,
				identifier,
				needsExactVersionMatch,
				siteName
			});
		}

		// Token: 0x06007BEE RID: 31726 RVA: 0x0019CE14 File Offset: 0x0019B014
		public static LocalizedString ErrorDuplicateKeyInMailboxProvisioningAttributes(string duplicateKeyName)
		{
			return new LocalizedString("ErrorDuplicateKeyInMailboxProvisioningAttributes", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				duplicateKeyName
			});
		}

		// Token: 0x17002CF7 RID: 11511
		// (get) Token: 0x06007BEF RID: 31727 RVA: 0x0019CE43 File Offset: 0x0019B043
		public static LocalizedString MeetingRequestMC
		{
			get
			{
				return new LocalizedString("MeetingRequestMC", "ExFEABF4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CF8 RID: 11512
		// (get) Token: 0x06007BF0 RID: 31728 RVA: 0x0019CE61 File Offset: 0x0019B061
		public static LocalizedString Tag
		{
			get
			{
				return new LocalizedString("Tag", "Ex9F8D72", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CF9 RID: 11513
		// (get) Token: 0x06007BF1 RID: 31729 RVA: 0x0019CE7F File Offset: 0x0019B07F
		public static LocalizedString MailFlowPartnerInternalMailContentTypeTNEF
		{
			get
			{
				return new LocalizedString("MailFlowPartnerInternalMailContentTypeTNEF", "ExE2BB59", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BF2 RID: 31730 RVA: 0x0019CEA0 File Offset: 0x0019B0A0
		public static LocalizedString ErrorPrimarySmtpInvalid(string primary)
		{
			return new LocalizedString("ErrorPrimarySmtpInvalid", "Ex2AD0EF", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				primary
			});
		}

		// Token: 0x17002CFA RID: 11514
		// (get) Token: 0x06007BF3 RID: 31731 RVA: 0x0019CECF File Offset: 0x0019B0CF
		public static LocalizedString SerialNumberMissing
		{
			get
			{
				return new LocalizedString("SerialNumberMissing", "Ex0FA073", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BF4 RID: 31732 RVA: 0x0019CEF0 File Offset: 0x0019B0F0
		public static LocalizedString ExceptionReferral(string target, string referral, string dn, string filter)
		{
			return new LocalizedString("ExceptionReferral", "Ex9288F1", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				target,
				referral,
				dn,
				filter
			});
		}

		// Token: 0x17002CFB RID: 11515
		// (get) Token: 0x06007BF5 RID: 31733 RVA: 0x0019CF2B File Offset: 0x0019B12B
		public static LocalizedString AttributeNameNull
		{
			get
			{
				return new LocalizedString("AttributeNameNull", "Ex6194D5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CFC RID: 11516
		// (get) Token: 0x06007BF6 RID: 31734 RVA: 0x0019CF49 File Offset: 0x0019B149
		public static LocalizedString ErrorIsDehydratedSetOnNonTinyTenant
		{
			get
			{
				return new LocalizedString("ErrorIsDehydratedSetOnNonTinyTenant", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007BF7 RID: 31735 RVA: 0x0019CF68 File Offset: 0x0019B168
		public static LocalizedString ExceptionRootDSE(string attr, string server)
		{
			return new LocalizedString("ExceptionRootDSE", "ExD724D2", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				attr,
				server
			});
		}

		// Token: 0x17002CFD RID: 11517
		// (get) Token: 0x06007BF8 RID: 31736 RVA: 0x0019CF9B File Offset: 0x0019B19B
		public static LocalizedString TUIPromptEditingEnabled
		{
			get
			{
				return new LocalizedString("TUIPromptEditingEnabled", "ExE96435", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CFE RID: 11518
		// (get) Token: 0x06007BF9 RID: 31737 RVA: 0x0019CFB9 File Offset: 0x0019B1B9
		public static LocalizedString StarAcceptedDomainCannotBeInitialDomain
		{
			get
			{
				return new LocalizedString("StarAcceptedDomainCannotBeInitialDomain", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002CFF RID: 11519
		// (get) Token: 0x06007BFA RID: 31738 RVA: 0x0019CFD7 File Offset: 0x0019B1D7
		public static LocalizedString LdapFilterErrorNotSupportSingleComp
		{
			get
			{
				return new LocalizedString("LdapFilterErrorNotSupportSingleComp", "ExC6795F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D00 RID: 11520
		// (get) Token: 0x06007BFB RID: 31739 RVA: 0x0019CFF5 File Offset: 0x0019B1F5
		public static LocalizedString UseTnef
		{
			get
			{
				return new LocalizedString("UseTnef", "Ex6B67E7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D01 RID: 11521
		// (get) Token: 0x06007BFC RID: 31740 RVA: 0x0019D013 File Offset: 0x0019B213
		public static LocalizedString AttachmentFilterEntryInvalid
		{
			get
			{
				return new LocalizedString("AttachmentFilterEntryInvalid", "ExA1D0A4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D02 RID: 11522
		// (get) Token: 0x06007BFD RID: 31741 RVA: 0x0019D031 File Offset: 0x0019B231
		public static LocalizedString Exchange2013
		{
			get
			{
				return new LocalizedString("Exchange2013", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D03 RID: 11523
		// (get) Token: 0x06007BFE RID: 31742 RVA: 0x0019D04F File Offset: 0x0019B24F
		public static LocalizedString SendAuthMechanismBasicAuthPlusTls
		{
			get
			{
				return new LocalizedString("SendAuthMechanismBasicAuthPlusTls", "Ex8B8C82", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D04 RID: 11524
		// (get) Token: 0x06007BFF RID: 31743 RVA: 0x0019D06D File Offset: 0x0019B26D
		public static LocalizedString MoveToDeletedItems
		{
			get
			{
				return new LocalizedString("MoveToDeletedItems", "Ex8BFB3B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D05 RID: 11525
		// (get) Token: 0x06007C00 RID: 31744 RVA: 0x0019D08B File Offset: 0x0019B28B
		public static LocalizedString TCP
		{
			get
			{
				return new LocalizedString("TCP", "ExD6B94B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C01 RID: 31745 RVA: 0x0019D0AC File Offset: 0x0019B2AC
		public static LocalizedString ErrorNoSuitableGCInForest(string domainName, string errorMessage)
		{
			return new LocalizedString("ErrorNoSuitableGCInForest", "Ex5BDD81", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				domainName,
				errorMessage
			});
		}

		// Token: 0x06007C02 RID: 31746 RVA: 0x0019D0E0 File Offset: 0x0019B2E0
		public static LocalizedString InvalidBiggerResourceThreshold(string classification, string thresholdName1, string thresholdName2, int value1, int value2)
		{
			return new LocalizedString("InvalidBiggerResourceThreshold", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				classification,
				thresholdName1,
				thresholdName2,
				value1,
				value2
			});
		}

		// Token: 0x17002D06 RID: 11526
		// (get) Token: 0x06007C03 RID: 31747 RVA: 0x0019D12A File Offset: 0x0019B32A
		public static LocalizedString DocumentMC
		{
			get
			{
				return new LocalizedString("DocumentMC", "Ex6C2D12", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D07 RID: 11527
		// (get) Token: 0x06007C04 RID: 31748 RVA: 0x0019D148 File Offset: 0x0019B348
		public static LocalizedString ErrorCannotSetWindowsEmailAddress
		{
			get
			{
				return new LocalizedString("ErrorCannotSetWindowsEmailAddress", "Ex0F45A4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C05 RID: 31749 RVA: 0x0019D168 File Offset: 0x0019B368
		public static LocalizedString InvalidTimeZoneId(string id)
		{
			return new LocalizedString("InvalidTimeZoneId", "ExFDA24E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06007C06 RID: 31750 RVA: 0x0019D198 File Offset: 0x0019B398
		public static LocalizedString CannotSerializePartitionHint(string hint)
		{
			return new LocalizedString("CannotSerializePartitionHint", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				hint
			});
		}

		// Token: 0x06007C07 RID: 31751 RVA: 0x0019D1C8 File Offset: 0x0019B3C8
		public static LocalizedString ErrorThisThresholdMustBeGreaterThanThatThreshold(string name1, string name2)
		{
			return new LocalizedString("ErrorThisThresholdMustBeGreaterThanThatThreshold", "ExBD3235", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				name1,
				name2
			});
		}

		// Token: 0x17002D08 RID: 11528
		// (get) Token: 0x06007C08 RID: 31752 RVA: 0x0019D1FB File Offset: 0x0019B3FB
		public static LocalizedString Msn
		{
			get
			{
				return new LocalizedString("Msn", "ExDF59E6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D09 RID: 11529
		// (get) Token: 0x06007C09 RID: 31753 RVA: 0x0019D219 File Offset: 0x0019B419
		public static LocalizedString MessageRateSourceFlagsIPAddress
		{
			get
			{
				return new LocalizedString("MessageRateSourceFlagsIPAddress", "ExA2B22B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D0A RID: 11530
		// (get) Token: 0x06007C0A RID: 31754 RVA: 0x0019D237 File Offset: 0x0019B437
		public static LocalizedString ErrorTextMessageIncludingAppleAttachment
		{
			get
			{
				return new LocalizedString("ErrorTextMessageIncludingAppleAttachment", "Ex82DD36", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D0B RID: 11531
		// (get) Token: 0x06007C0B RID: 31755 RVA: 0x0019D255 File Offset: 0x0019B455
		public static LocalizedString ForwardCallsToDefaultMailbox
		{
			get
			{
				return new LocalizedString("ForwardCallsToDefaultMailbox", "ExAD51C3", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C0C RID: 31756 RVA: 0x0019D274 File Offset: 0x0019B474
		public static LocalizedString ExceptionRemoveApprovedApplication(string id)
		{
			return new LocalizedString("ExceptionRemoveApprovedApplication", "ExD16E61", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002D0C RID: 11532
		// (get) Token: 0x06007C0D RID: 31757 RVA: 0x0019D2A3 File Offset: 0x0019B4A3
		public static LocalizedString RoleGroupTypeDetails
		{
			get
			{
				return new LocalizedString("RoleGroupTypeDetails", "Ex291323", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D0D RID: 11533
		// (get) Token: 0x06007C0E RID: 31758 RVA: 0x0019D2C1 File Offset: 0x0019B4C1
		public static LocalizedString MailEnabledContactRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailEnabledContactRecipientTypeDetails", "Ex8B3132", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C0F RID: 31759 RVA: 0x0019D2E0 File Offset: 0x0019B4E0
		public static LocalizedString ErrorJoinApprovalRequiredWithoutManager(string id)
		{
			return new LocalizedString("ErrorJoinApprovalRequiredWithoutManager", "Ex7A64B7", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06007C10 RID: 31760 RVA: 0x0019D310 File Offset: 0x0019B510
		public static LocalizedString InvalidConsumerDialPlanSetting(string prop)
		{
			return new LocalizedString("InvalidConsumerDialPlanSetting", "ExD9B17B", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				prop
			});
		}

		// Token: 0x17002D0E RID: 11534
		// (get) Token: 0x06007C11 RID: 31761 RVA: 0x0019D33F File Offset: 0x0019B53F
		public static LocalizedString EsnLangEnglish
		{
			get
			{
				return new LocalizedString("EsnLangEnglish", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D0F RID: 11535
		// (get) Token: 0x06007C12 RID: 31762 RVA: 0x0019D35D File Offset: 0x0019B55D
		public static LocalizedString EsnLangMarathi
		{
			get
			{
				return new LocalizedString("EsnLangMarathi", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C13 RID: 31763 RVA: 0x0019D37C File Offset: 0x0019B57C
		public static LocalizedString RangeInformationFormatInvalid(string str)
		{
			return new LocalizedString("RangeInformationFormatInvalid", "Ex54FC08", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				str
			});
		}

		// Token: 0x17002D10 RID: 11536
		// (get) Token: 0x06007C14 RID: 31764 RVA: 0x0019D3AB File Offset: 0x0019B5AB
		public static LocalizedString SpecifyAnnouncementFileName
		{
			get
			{
				return new LocalizedString("SpecifyAnnouncementFileName", "ExD1B2BF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C15 RID: 31765 RVA: 0x0019D3CC File Offset: 0x0019B5CC
		public static LocalizedString ExceptionADTopologyHasNoAvailableServersInDomain(string domain)
		{
			return new LocalizedString("ExceptionADTopologyHasNoAvailableServersInDomain", "Ex302882", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x06007C16 RID: 31766 RVA: 0x0019D3FC File Offset: 0x0019B5FC
		public static LocalizedString RuleMigration_Error(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("RuleMigration_Error", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x06007C17 RID: 31767 RVA: 0x0019D438 File Offset: 0x0019B638
		public static LocalizedString PropertiesMasteredOnPremise_Violation(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("PropertiesMasteredOnPremise_Violation", "Ex945F88", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x17002D11 RID: 11537
		// (get) Token: 0x06007C18 RID: 31768 RVA: 0x0019D473 File Offset: 0x0019B673
		public static LocalizedString GroupNamingPolicyCustomAttribute12
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute12", "Ex14AA12", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C19 RID: 31769 RVA: 0x0019D494 File Offset: 0x0019B694
		public static LocalizedString BadSwapOperationCount(int changeCount)
		{
			return new LocalizedString("BadSwapOperationCount", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				changeCount
			});
		}

		// Token: 0x06007C1A RID: 31770 RVA: 0x0019D4C8 File Offset: 0x0019B6C8
		public static LocalizedString CannotDetermineDataSessionTypeForObject(string type)
		{
			return new LocalizedString("CannotDetermineDataSessionTypeForObject", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x17002D12 RID: 11538
		// (get) Token: 0x06007C1B RID: 31771 RVA: 0x0019D4F7 File Offset: 0x0019B6F7
		public static LocalizedString SystemAddressListDoesNotExist
		{
			get
			{
				return new LocalizedString("SystemAddressListDoesNotExist", "Ex659C48", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D13 RID: 11539
		// (get) Token: 0x06007C1C RID: 31772 RVA: 0x0019D515 File Offset: 0x0019B715
		public static LocalizedString DefaultOabName
		{
			get
			{
				return new LocalizedString("DefaultOabName", "ExCBC06D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D14 RID: 11540
		// (get) Token: 0x06007C1D RID: 31773 RVA: 0x0019D533 File Offset: 0x0019B733
		public static LocalizedString EsnLangSpanish
		{
			get
			{
				return new LocalizedString("EsnLangSpanish", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C1E RID: 31774 RVA: 0x0019D554 File Offset: 0x0019B754
		public static LocalizedString ExceptionADOperationFailedNoSuchObject(string server, string dn)
		{
			return new LocalizedString("ExceptionADOperationFailedNoSuchObject", "Ex2A592C", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				dn
			});
		}

		// Token: 0x17002D15 RID: 11541
		// (get) Token: 0x06007C1F RID: 31775 RVA: 0x0019D587 File Offset: 0x0019B787
		public static LocalizedString FederatedOrganizationIdNoNamespaceAccount
		{
			get
			{
				return new LocalizedString("FederatedOrganizationIdNoNamespaceAccount", "Ex0CF246", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C20 RID: 31776 RVA: 0x0019D5A8 File Offset: 0x0019B7A8
		public static LocalizedString ExceptionPropertyCannotBeSearchedOn(string property)
		{
			return new LocalizedString("ExceptionPropertyCannotBeSearchedOn", "ExF6FA9E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x17002D16 RID: 11542
		// (get) Token: 0x06007C21 RID: 31777 RVA: 0x0019D5D7 File Offset: 0x0019B7D7
		public static LocalizedString RemoteEquipmentMailboxTypeDetails
		{
			get
			{
				return new LocalizedString("RemoteEquipmentMailboxTypeDetails", "Ex3FE261", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D17 RID: 11543
		// (get) Token: 0x06007C22 RID: 31778 RVA: 0x0019D5F5 File Offset: 0x0019B7F5
		public static LocalizedString SpamFilteringOptionOn
		{
			get
			{
				return new LocalizedString("SpamFilteringOptionOn", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D18 RID: 11544
		// (get) Token: 0x06007C23 RID: 31779 RVA: 0x0019D613 File Offset: 0x0019B813
		public static LocalizedString ErrorNoSharedConfigurationInfo
		{
			get
			{
				return new LocalizedString("ErrorNoSharedConfigurationInfo", "ExBC8D65", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D19 RID: 11545
		// (get) Token: 0x06007C24 RID: 31780 RVA: 0x0019D631 File Offset: 0x0019B831
		public static LocalizedString EquipmentMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("EquipmentMailboxRecipientTypeDetails", "ExBC6B80", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D1A RID: 11546
		// (get) Token: 0x06007C25 RID: 31781 RVA: 0x0019D64F File Offset: 0x0019B84F
		public static LocalizedString ErrorCannotSetMoveToDestinationFolder
		{
			get
			{
				return new LocalizedString("ErrorCannotSetMoveToDestinationFolder", "ExF94D65", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D1B RID: 11547
		// (get) Token: 0x06007C26 RID: 31782 RVA: 0x0019D66D File Offset: 0x0019B86D
		public static LocalizedString CapabilityTOUSigned
		{
			get
			{
				return new LocalizedString("CapabilityTOUSigned", "ExD7C91F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C27 RID: 31783 RVA: 0x0019D68C File Offset: 0x0019B88C
		public static LocalizedString InvalidNtds(string server)
		{
			return new LocalizedString("InvalidNtds", "Ex747A5F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17002D1C RID: 11548
		// (get) Token: 0x06007C28 RID: 31784 RVA: 0x0019D6BB File Offset: 0x0019B8BB
		public static LocalizedString ServerRoleExtendedRole2
		{
			get
			{
				return new LocalizedString("ServerRoleExtendedRole2", "ExCEDEA5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D1D RID: 11549
		// (get) Token: 0x06007C29 RID: 31785 RVA: 0x0019D6D9 File Offset: 0x0019B8D9
		public static LocalizedString ServerRoleExtendedRole3
		{
			get
			{
				return new LocalizedString("ServerRoleExtendedRole3", "Ex5AC339", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C2A RID: 31786 RVA: 0x0019D6F8 File Offset: 0x0019B8F8
		public static LocalizedString ExceptionADConfigurationObjectRequired(string objectType, string methodName)
		{
			return new LocalizedString("ExceptionADConfigurationObjectRequired", "Ex7FB667", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				objectType,
				methodName
			});
		}

		// Token: 0x06007C2B RID: 31787 RVA: 0x0019D72C File Offset: 0x0019B92C
		public static LocalizedString TenantOrgContainerNotFoundException(string orgId)
		{
			return new LocalizedString("TenantOrgContainerNotFoundException", "Ex071072", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				orgId
			});
		}

		// Token: 0x17002D1E RID: 11550
		// (get) Token: 0x06007C2C RID: 31788 RVA: 0x0019D75B File Offset: 0x0019B95B
		public static LocalizedString PersonalFolder
		{
			get
			{
				return new LocalizedString("PersonalFolder", "ExADA167", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D1F RID: 11551
		// (get) Token: 0x06007C2D RID: 31789 RVA: 0x0019D779 File Offset: 0x0019B979
		public static LocalizedString CapabilityNone
		{
			get
			{
				return new LocalizedString("CapabilityNone", "Ex0425E3", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D20 RID: 11552
		// (get) Token: 0x06007C2E RID: 31790 RVA: 0x0019D797 File Offset: 0x0019B997
		public static LocalizedString ErrorEmptyResourceTypeofResourceMailbox
		{
			get
			{
				return new LocalizedString("ErrorEmptyResourceTypeofResourceMailbox", "Ex1C6D31", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D21 RID: 11553
		// (get) Token: 0x06007C2F RID: 31791 RVA: 0x0019D7B5 File Offset: 0x0019B9B5
		public static LocalizedString InternalDNSServersNotSet
		{
			get
			{
				return new LocalizedString("InternalDNSServersNotSet", "Ex246010", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D22 RID: 11554
		// (get) Token: 0x06007C30 RID: 31792 RVA: 0x0019D7D3 File Offset: 0x0019B9D3
		public static LocalizedString ExceptionImpersonation
		{
			get
			{
				return new LocalizedString("ExceptionImpersonation", "ExDF6BB4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C31 RID: 31793 RVA: 0x0019D7F4 File Offset: 0x0019B9F4
		public static LocalizedString ExceptionADTopologyUnexpectedError(string server, string error)
		{
			return new LocalizedString("ExceptionADTopologyUnexpectedError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				error
			});
		}

		// Token: 0x17002D23 RID: 11555
		// (get) Token: 0x06007C32 RID: 31794 RVA: 0x0019D827 File Offset: 0x0019BA27
		public static LocalizedString ReceiveAuthMechanismNone
		{
			get
			{
				return new LocalizedString("ReceiveAuthMechanismNone", "Ex6818A8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D24 RID: 11556
		// (get) Token: 0x06007C33 RID: 31795 RVA: 0x0019D845 File Offset: 0x0019BA45
		public static LocalizedString GroupNamingPolicyCustomAttribute9
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute9", "ExA79439", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D25 RID: 11557
		// (get) Token: 0x06007C34 RID: 31796 RVA: 0x0019D863 File Offset: 0x0019BA63
		public static LocalizedString MailEnabledDynamicDistributionGroupRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailEnabledDynamicDistributionGroupRecipientTypeDetails", "ExCC2975", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D26 RID: 11558
		// (get) Token: 0x06007C35 RID: 31797 RVA: 0x0019D881 File Offset: 0x0019BA81
		public static LocalizedString SpamFilteringActionAddXHeader
		{
			get
			{
				return new LocalizedString("SpamFilteringActionAddXHeader", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C36 RID: 31798 RVA: 0x0019D8A0 File Offset: 0x0019BAA0
		public static LocalizedString ErrorNotInServerWriteScope(string identity)
		{
			return new LocalizedString("ErrorNotInServerWriteScope", "Ex5CC9EA", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06007C37 RID: 31799 RVA: 0x0019D8D0 File Offset: 0x0019BAD0
		public static LocalizedString DuplicatedAcceptedDomain(string domainName, string firstDup, string secondDup)
		{
			return new LocalizedString("DuplicatedAcceptedDomain", "ExAF3249", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				domainName,
				firstDup,
				secondDup
			});
		}

		// Token: 0x17002D27 RID: 11559
		// (get) Token: 0x06007C38 RID: 31800 RVA: 0x0019D907 File Offset: 0x0019BB07
		public static LocalizedString RecentCommands
		{
			get
			{
				return new LocalizedString("RecentCommands", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D28 RID: 11560
		// (get) Token: 0x06007C39 RID: 31801 RVA: 0x0019D925 File Offset: 0x0019BB25
		public static LocalizedString SecurityPrincipalTypeNone
		{
			get
			{
				return new LocalizedString("SecurityPrincipalTypeNone", "Ex45443A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C3A RID: 31802 RVA: 0x0019D944 File Offset: 0x0019BB44
		public static LocalizedString ErrorCannotSetPermanentAttributes(string permanentAttributeNames)
		{
			return new LocalizedString("ErrorCannotSetPermanentAttributes", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				permanentAttributeNames
			});
		}

		// Token: 0x17002D29 RID: 11561
		// (get) Token: 0x06007C3B RID: 31803 RVA: 0x0019D973 File Offset: 0x0019BB73
		public static LocalizedString MailboxMoveStatusNone
		{
			get
			{
				return new LocalizedString("MailboxMoveStatusNone", "Ex3A7378", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C3C RID: 31804 RVA: 0x0019D994 File Offset: 0x0019BB94
		public static LocalizedString CannotBuildAuthenticationTypeFilterBadArgument(string authType)
		{
			return new LocalizedString("CannotBuildAuthenticationTypeFilterBadArgument", "Ex27E265", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				authType
			});
		}

		// Token: 0x17002D2A RID: 11562
		// (get) Token: 0x06007C3D RID: 31805 RVA: 0x0019D9C3 File Offset: 0x0019BBC3
		public static LocalizedString LocalForest
		{
			get
			{
				return new LocalizedString("LocalForest", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D2B RID: 11563
		// (get) Token: 0x06007C3E RID: 31806 RVA: 0x0019D9E1 File Offset: 0x0019BBE1
		public static LocalizedString LegacyMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("LegacyMailboxRecipientTypeDetails", "Ex3AD144", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D2C RID: 11564
		// (get) Token: 0x06007C3F RID: 31807 RVA: 0x0019D9FF File Offset: 0x0019BBFF
		public static LocalizedString GroupNamingPolicyCustomAttribute2
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute2", "Ex624F4E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C40 RID: 31808 RVA: 0x0019DA20 File Offset: 0x0019BC20
		public static LocalizedString ErrorTargetPartitionHas2TenantsWithSameId(string oldTenant, string newPartition, string guid)
		{
			return new LocalizedString("ErrorTargetPartitionHas2TenantsWithSameId", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				oldTenant,
				newPartition,
				guid
			});
		}

		// Token: 0x17002D2D RID: 11565
		// (get) Token: 0x06007C41 RID: 31809 RVA: 0x0019DA57 File Offset: 0x0019BC57
		public static LocalizedString DatabaseMasterTypeUnknown
		{
			get
			{
				return new LocalizedString("DatabaseMasterTypeUnknown", "Ex5A641A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D2E RID: 11566
		// (get) Token: 0x06007C42 RID: 31810 RVA: 0x0019DA75 File Offset: 0x0019BC75
		public static LocalizedString ConversationHistory
		{
			get
			{
				return new LocalizedString("ConversationHistory", "Ex082CC3", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D2F RID: 11567
		// (get) Token: 0x06007C43 RID: 31811 RVA: 0x0019DA93 File Offset: 0x0019BC93
		public static LocalizedString OutboundConnectorTlsSettingsInvalidDomainValidationWithoutTlsDomain
		{
			get
			{
				return new LocalizedString("OutboundConnectorTlsSettingsInvalidDomainValidationWithoutTlsDomain", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D30 RID: 11568
		// (get) Token: 0x06007C44 RID: 31812 RVA: 0x0019DAB1 File Offset: 0x0019BCB1
		public static LocalizedString WhenMoved
		{
			get
			{
				return new LocalizedString("WhenMoved", "Ex478EE0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C45 RID: 31813 RVA: 0x0019DAD0 File Offset: 0x0019BCD0
		public static LocalizedString ExceededMaximumCollectionCount(string propertyName, int maxLength, int actualLength)
		{
			return new LocalizedString("ExceededMaximumCollectionCount", "Ex2BA297", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				maxLength,
				actualLength
			});
		}

		// Token: 0x17002D31 RID: 11569
		// (get) Token: 0x06007C46 RID: 31814 RVA: 0x0019DB11 File Offset: 0x0019BD11
		public static LocalizedString ErrorDuplicateLanguage
		{
			get
			{
				return new LocalizedString("ErrorDuplicateLanguage", "Ex055943", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C47 RID: 31815 RVA: 0x0019DB30 File Offset: 0x0019BD30
		public static LocalizedString ExceptionReferralWhenBoundToDomainController(string domain, string dc)
		{
			return new LocalizedString("ExceptionReferralWhenBoundToDomainController", "Ex3AA09E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				domain,
				dc
			});
		}

		// Token: 0x06007C48 RID: 31816 RVA: 0x0019DB64 File Offset: 0x0019BD64
		public static LocalizedString AssignmentsWithConflictingScope(string id, string anotherId, string details)
		{
			return new LocalizedString("AssignmentsWithConflictingScope", "Ex5157BB", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id,
				anotherId,
				details
			});
		}

		// Token: 0x06007C49 RID: 31817 RVA: 0x0019DB9C File Offset: 0x0019BD9C
		public static LocalizedString ExceptionReadingRootDSE(string serverName, string message)
		{
			return new LocalizedString("ExceptionReadingRootDSE", "Ex218628", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				serverName,
				message
			});
		}

		// Token: 0x17002D32 RID: 11570
		// (get) Token: 0x06007C4A RID: 31818 RVA: 0x0019DBCF File Offset: 0x0019BDCF
		public static LocalizedString ExceptionObjectAlreadyExists
		{
			get
			{
				return new LocalizedString("ExceptionObjectAlreadyExists", "ExA1C69E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D33 RID: 11571
		// (get) Token: 0x06007C4B RID: 31819 RVA: 0x0019DBED File Offset: 0x0019BDED
		public static LocalizedString EsnLangCzech
		{
			get
			{
				return new LocalizedString("EsnLangCzech", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D34 RID: 11572
		// (get) Token: 0x06007C4C RID: 31820 RVA: 0x0019DC0B File Offset: 0x0019BE0B
		public static LocalizedString ComponentNameInvalid
		{
			get
			{
				return new LocalizedString("ComponentNameInvalid", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C4D RID: 31821 RVA: 0x0019DC2C File Offset: 0x0019BE2C
		public static LocalizedString CannotGetForestInfo(string forest)
		{
			return new LocalizedString("CannotGetForestInfo", "ExCC70A2", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				forest
			});
		}

		// Token: 0x06007C4E RID: 31822 RVA: 0x0019DC5C File Offset: 0x0019BE5C
		public static LocalizedString InvalidCertificateName(string certName)
		{
			return new LocalizedString("InvalidCertificateName", "Ex07E797", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				certName
			});
		}

		// Token: 0x06007C4F RID: 31823 RVA: 0x0019DC8C File Offset: 0x0019BE8C
		public static LocalizedString CannotParse(string data)
		{
			return new LocalizedString("CannotParse", "Ex1E4981", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				data
			});
		}

		// Token: 0x06007C50 RID: 31824 RVA: 0x0019DCBC File Offset: 0x0019BEBC
		public static LocalizedString TransportSettingsAmbiguousException(string orgId)
		{
			return new LocalizedString("TransportSettingsAmbiguousException", "ExA135C4", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				orgId
			});
		}

		// Token: 0x06007C51 RID: 31825 RVA: 0x0019DCEC File Offset: 0x0019BEEC
		public static LocalizedString ExceptionADTopologyNoSuchForest(string forest)
		{
			return new LocalizedString("ExceptionADTopologyNoSuchForest", "Ex7AF243", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				forest
			});
		}

		// Token: 0x06007C52 RID: 31826 RVA: 0x0019DD1C File Offset: 0x0019BF1C
		public static LocalizedString PropertyRequired(string propertyName, string recipientType)
		{
			return new LocalizedString("PropertyRequired", "Ex99AECC", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				recipientType
			});
		}

		// Token: 0x06007C53 RID: 31827 RVA: 0x0019DD50 File Offset: 0x0019BF50
		public static LocalizedString OUsNotSmallerOrEqual(string leftOU, string rightOU)
		{
			return new LocalizedString("OUsNotSmallerOrEqual", "ExAA571A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				leftOU,
				rightOU
			});
		}

		// Token: 0x17002D35 RID: 11573
		// (get) Token: 0x06007C54 RID: 31828 RVA: 0x0019DD83 File Offset: 0x0019BF83
		public static LocalizedString ErrorAuthMetadataCannotResolveIssuer
		{
			get
			{
				return new LocalizedString("ErrorAuthMetadataCannotResolveIssuer", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D36 RID: 11574
		// (get) Token: 0x06007C55 RID: 31829 RVA: 0x0019DDA1 File Offset: 0x0019BFA1
		public static LocalizedString GroupNamingPolicyTitle
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyTitle", "ExBEFD80", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D37 RID: 11575
		// (get) Token: 0x06007C56 RID: 31830 RVA: 0x0019DDBF File Offset: 0x0019BFBF
		public static LocalizedString MailboxMoveStatusSuspended
		{
			get
			{
				return new LocalizedString("MailboxMoveStatusSuspended", "ExC51ED2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D38 RID: 11576
		// (get) Token: 0x06007C57 RID: 31831 RVA: 0x0019DDDD File Offset: 0x0019BFDD
		public static LocalizedString DomainSecureEnabledWithExternalAuthoritative
		{
			get
			{
				return new LocalizedString("DomainSecureEnabledWithExternalAuthoritative", "ExC43BF8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D39 RID: 11577
		// (get) Token: 0x06007C58 RID: 31832 RVA: 0x0019DDFB File Offset: 0x0019BFFB
		public static LocalizedString BasicAfterTLSWithoutTLS
		{
			get
			{
				return new LocalizedString("BasicAfterTLSWithoutTLS", "ExB40D87", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C59 RID: 31833 RVA: 0x0019DE1C File Offset: 0x0019C01C
		public static LocalizedString TimeoutGlsError(string message)
		{
			return new LocalizedString("TimeoutGlsError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x06007C5A RID: 31834 RVA: 0x0019DE4C File Offset: 0x0019C04C
		public static LocalizedString ExtensionAlreadyUsedAsPilotNumber(string phoneNumber, string dialPlan)
		{
			return new LocalizedString("ExtensionAlreadyUsedAsPilotNumber", "Ex6E5038", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				phoneNumber,
				dialPlan
			});
		}

		// Token: 0x17002D3A RID: 11578
		// (get) Token: 0x06007C5B RID: 31835 RVA: 0x0019DE7F File Offset: 0x0019C07F
		public static LocalizedString Private
		{
			get
			{
				return new LocalizedString("Private", "ExECF0B9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D3B RID: 11579
		// (get) Token: 0x06007C5C RID: 31836 RVA: 0x0019DE9D File Offset: 0x0019C09D
		public static LocalizedString Mailboxes
		{
			get
			{
				return new LocalizedString("Mailboxes", "Ex875C09", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C5D RID: 31837 RVA: 0x0019DEBC File Offset: 0x0019C0BC
		public static LocalizedString ErrorCannotFindRidMasterForPartition(string partition)
		{
			return new LocalizedString("ErrorCannotFindRidMasterForPartition", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				partition
			});
		}

		// Token: 0x06007C5E RID: 31838 RVA: 0x0019DEEC File Offset: 0x0019C0EC
		public static LocalizedString InvalidCharacterSet(string charset, string supported)
		{
			return new LocalizedString("InvalidCharacterSet", "Ex874AB3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				charset,
				supported
			});
		}

		// Token: 0x17002D3C RID: 11580
		// (get) Token: 0x06007C5F RID: 31839 RVA: 0x0019DF1F File Offset: 0x0019C11F
		public static LocalizedString ErrorModeratorRequiredForModeration
		{
			get
			{
				return new LocalizedString("ErrorModeratorRequiredForModeration", "ExEC501D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D3D RID: 11581
		// (get) Token: 0x06007C60 RID: 31840 RVA: 0x0019DF3D File Offset: 0x0019C13D
		public static LocalizedString CustomFromAddressRequired
		{
			get
			{
				return new LocalizedString("CustomFromAddressRequired", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D3E RID: 11582
		// (get) Token: 0x06007C61 RID: 31841 RVA: 0x0019DF5B File Offset: 0x0019C15B
		public static LocalizedString LdapModifyDN
		{
			get
			{
				return new LocalizedString("LdapModifyDN", "ExF4328B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D3F RID: 11583
		// (get) Token: 0x06007C62 RID: 31842 RVA: 0x0019DF79 File Offset: 0x0019C179
		public static LocalizedString CustomExternalSubjectRequired
		{
			get
			{
				return new LocalizedString("CustomExternalSubjectRequired", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C63 RID: 31843 RVA: 0x0019DF98 File Offset: 0x0019C198
		public static LocalizedString ExceptionADUnavailable(string serverName)
		{
			return new LocalizedString("ExceptionADUnavailable", "ExE542FC", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06007C64 RID: 31844 RVA: 0x0019DFC8 File Offset: 0x0019C1C8
		public static LocalizedString ErrorSettingOverrideInvalidFlightName(string flightName, string availableFlights)
		{
			return new LocalizedString("ErrorSettingOverrideInvalidFlightName", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				flightName,
				availableFlights
			});
		}

		// Token: 0x17002D40 RID: 11584
		// (get) Token: 0x06007C65 RID: 31845 RVA: 0x0019DFFB File Offset: 0x0019C1FB
		public static LocalizedString ErrorInternalLocationsCountMissMatch
		{
			get
			{
				return new LocalizedString("ErrorInternalLocationsCountMissMatch", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D41 RID: 11585
		// (get) Token: 0x06007C66 RID: 31846 RVA: 0x0019E019 File Offset: 0x0019C219
		public static LocalizedString ASOnlyOneAuthenticationMethodAllowed
		{
			get
			{
				return new LocalizedString("ASOnlyOneAuthenticationMethodAllowed", "Ex84660E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D42 RID: 11586
		// (get) Token: 0x06007C67 RID: 31847 RVA: 0x0019E037 File Offset: 0x0019C237
		public static LocalizedString Tnef
		{
			get
			{
				return new LocalizedString("Tnef", "Ex0DF85E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D43 RID: 11587
		// (get) Token: 0x06007C68 RID: 31848 RVA: 0x0019E055 File Offset: 0x0019C255
		public static LocalizedString ByteEncoderTypeUseBase64HtmlDetectTextPlain
		{
			get
			{
				return new LocalizedString("ByteEncoderTypeUseBase64HtmlDetectTextPlain", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C69 RID: 31849 RVA: 0x0019E074 File Offset: 0x0019C274
		public static LocalizedString TooManyCustomExtensions(string a)
		{
			return new LocalizedString("TooManyCustomExtensions", "ExAD0209", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				a
			});
		}

		// Token: 0x17002D44 RID: 11588
		// (get) Token: 0x06007C6A RID: 31850 RVA: 0x0019E0A3 File Offset: 0x0019C2A3
		public static LocalizedString EsnLangIcelandic
		{
			get
			{
				return new LocalizedString("EsnLangIcelandic", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C6B RID: 31851 RVA: 0x0019E0C4 File Offset: 0x0019C2C4
		public static LocalizedString ExceptionOwaCannotSetPropertyOnE12VirtualDirectory(string property)
		{
			return new LocalizedString("ExceptionOwaCannotSetPropertyOnE12VirtualDirectory", "ExDABBEB", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x17002D45 RID: 11589
		// (get) Token: 0x06007C6C RID: 31852 RVA: 0x0019E0F3 File Offset: 0x0019C2F3
		public static LocalizedString ServerRoleNAT
		{
			get
			{
				return new LocalizedString("ServerRoleNAT", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D46 RID: 11590
		// (get) Token: 0x06007C6D RID: 31853 RVA: 0x0019E111 File Offset: 0x0019C311
		public static LocalizedString UniversalDistributionGroupRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("UniversalDistributionGroupRecipientTypeDetails", "ExD0510E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D47 RID: 11591
		// (get) Token: 0x06007C6E RID: 31854 RVA: 0x0019E12F File Offset: 0x0019C32F
		public static LocalizedString ErrorReplicationLatency
		{
			get
			{
				return new LocalizedString("ErrorReplicationLatency", "Ex15DE8E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D48 RID: 11592
		// (get) Token: 0x06007C6F RID: 31855 RVA: 0x0019E14D File Offset: 0x0019C34D
		public static LocalizedString EnabledPartner
		{
			get
			{
				return new LocalizedString("EnabledPartner", "ExA88908", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D49 RID: 11593
		// (get) Token: 0x06007C70 RID: 31856 RVA: 0x0019E16B File Offset: 0x0019C36B
		public static LocalizedString OutboundConnectorSmarthostTlsSettingsInvalid
		{
			get
			{
				return new LocalizedString("OutboundConnectorSmarthostTlsSettingsInvalid", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D4A RID: 11594
		// (get) Token: 0x06007C71 RID: 31857 RVA: 0x0019E189 File Offset: 0x0019C389
		public static LocalizedString ExternalCompliance
		{
			get
			{
				return new LocalizedString("ExternalCompliance", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D4B RID: 11595
		// (get) Token: 0x06007C72 RID: 31858 RVA: 0x0019E1A7 File Offset: 0x0019C3A7
		public static LocalizedString ErrorAuthMetadataNoSigningKey
		{
			get
			{
				return new LocalizedString("ErrorAuthMetadataNoSigningKey", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C73 RID: 31859 RVA: 0x0019E1C8 File Offset: 0x0019C3C8
		public static LocalizedString ExArgumentException(string paramName, object value)
		{
			return new LocalizedString("ExArgumentException", "ExB1B609", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				paramName,
				value
			});
		}

		// Token: 0x17002D4C RID: 11596
		// (get) Token: 0x06007C74 RID: 31860 RVA: 0x0019E1FB File Offset: 0x0019C3FB
		public static LocalizedString InboundConnectorIncorrectAllAcceptedDomains
		{
			get
			{
				return new LocalizedString("InboundConnectorIncorrectAllAcceptedDomains", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D4D RID: 11597
		// (get) Token: 0x06007C75 RID: 31861 RVA: 0x0019E219 File Offset: 0x0019C419
		public static LocalizedString MoveToFolder
		{
			get
			{
				return new LocalizedString("MoveToFolder", "ExA31786", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D4E RID: 11598
		// (get) Token: 0x06007C76 RID: 31862 RVA: 0x0019E237 File Offset: 0x0019C437
		public static LocalizedString Byte
		{
			get
			{
				return new LocalizedString("Byte", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C77 RID: 31863 RVA: 0x0019E258 File Offset: 0x0019C458
		public static LocalizedString CannotResolvePartitionGuidError(string guid)
		{
			return new LocalizedString("CannotResolvePartitionGuidError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x17002D4F RID: 11599
		// (get) Token: 0x06007C78 RID: 31864 RVA: 0x0019E287 File Offset: 0x0019C487
		public static LocalizedString EsnLangCyrillic
		{
			get
			{
				return new LocalizedString("EsnLangCyrillic", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D50 RID: 11600
		// (get) Token: 0x06007C79 RID: 31865 RVA: 0x0019E2A5 File Offset: 0x0019C4A5
		public static LocalizedString CanRunDefaultUpdateState_Invalid
		{
			get
			{
				return new LocalizedString("CanRunDefaultUpdateState_Invalid", "ExEA5F0B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D51 RID: 11601
		// (get) Token: 0x06007C7A RID: 31866 RVA: 0x0019E2C3 File Offset: 0x0019C4C3
		public static LocalizedString DisabledUserRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("DisabledUserRecipientTypeDetails", "ExE4F7D7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D52 RID: 11602
		// (get) Token: 0x06007C7B RID: 31867 RVA: 0x0019E2E1 File Offset: 0x0019C4E1
		public static LocalizedString InvalidRecipientType
		{
			get
			{
				return new LocalizedString("InvalidRecipientType", "Ex2E8EE6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D53 RID: 11603
		// (get) Token: 0x06007C7C RID: 31868 RVA: 0x0019E2FF File Offset: 0x0019C4FF
		public static LocalizedString EmailAgeFilterThreeDays
		{
			get
			{
				return new LocalizedString("EmailAgeFilterThreeDays", "ExDABD80", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D54 RID: 11604
		// (get) Token: 0x06007C7D RID: 31869 RVA: 0x0019E31D File Offset: 0x0019C51D
		public static LocalizedString DataMoveReplicationConstraintCISecondCopy
		{
			get
			{
				return new LocalizedString("DataMoveReplicationConstraintCISecondCopy", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C7E RID: 31870 RVA: 0x0019E33C File Offset: 0x0019C53C
		public static LocalizedString CannotGetDnAtDepth(string dn, int depth)
		{
			return new LocalizedString("CannotGetDnAtDepth", "Ex09C258", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				dn,
				depth
			});
		}

		// Token: 0x17002D55 RID: 11605
		// (get) Token: 0x06007C7F RID: 31871 RVA: 0x0019E374 File Offset: 0x0019C574
		public static LocalizedString ErrorMissingPrimarySmtp
		{
			get
			{
				return new LocalizedString("ErrorMissingPrimarySmtp", "ExC71195", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D56 RID: 11606
		// (get) Token: 0x06007C80 RID: 31872 RVA: 0x0019E392 File Offset: 0x0019C592
		public static LocalizedString ErrorELCFolderNotSpecified
		{
			get
			{
				return new LocalizedString("ErrorELCFolderNotSpecified", "Ex13B523", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D57 RID: 11607
		// (get) Token: 0x06007C81 RID: 31873 RVA: 0x0019E3B0 File Offset: 0x0019C5B0
		public static LocalizedString ErrorCannotHaveMoreThanOneDefaultThrottlingPolicy
		{
			get
			{
				return new LocalizedString("ErrorCannotHaveMoreThanOneDefaultThrottlingPolicy", "Ex88B409", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D58 RID: 11608
		// (get) Token: 0x06007C82 RID: 31874 RVA: 0x0019E3CE File Offset: 0x0019C5CE
		public static LocalizedString ReceiveModeCannotBeZero
		{
			get
			{
				return new LocalizedString("ReceiveModeCannotBeZero", "Ex88AFCF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D59 RID: 11609
		// (get) Token: 0x06007C83 RID: 31875 RVA: 0x0019E3EC File Offset: 0x0019C5EC
		public static LocalizedString OwaDefaultDomainRequiredWhenLogonFormatIsUserName
		{
			get
			{
				return new LocalizedString("OwaDefaultDomainRequiredWhenLogonFormatIsUserName", "Ex4B0461", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D5A RID: 11610
		// (get) Token: 0x06007C84 RID: 31876 RVA: 0x0019E40A File Offset: 0x0019C60A
		public static LocalizedString TLS
		{
			get
			{
				return new LocalizedString("TLS", "Ex167363", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C85 RID: 31877 RVA: 0x0019E428 File Offset: 0x0019C628
		public static LocalizedString InvalidHostname(object value)
		{
			return new LocalizedString("InvalidHostname", "Ex35FD97", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17002D5B RID: 11611
		// (get) Token: 0x06007C86 RID: 31878 RVA: 0x0019E457 File Offset: 0x0019C657
		public static LocalizedString LinkedMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("LinkedMailboxRecipientTypeDetails", "Ex93A886", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D5C RID: 11612
		// (get) Token: 0x06007C87 RID: 31879 RVA: 0x0019E475 File Offset: 0x0019C675
		public static LocalizedString Tasks
		{
			get
			{
				return new LocalizedString("Tasks", "Ex6B5E90", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D5D RID: 11613
		// (get) Token: 0x06007C88 RID: 31880 RVA: 0x0019E493 File Offset: 0x0019C693
		public static LocalizedString RejectAndQuarantineThreshold
		{
			get
			{
				return new LocalizedString("RejectAndQuarantineThreshold", "ExEBA8BE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D5E RID: 11614
		// (get) Token: 0x06007C89 RID: 31881 RVA: 0x0019E4B1 File Offset: 0x0019C6B1
		public static LocalizedString LdapFilterErrorInvalidDecimal
		{
			get
			{
				return new LocalizedString("LdapFilterErrorInvalidDecimal", "ExEF43A4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D5F RID: 11615
		// (get) Token: 0x06007C8A RID: 31882 RVA: 0x0019E4CF File Offset: 0x0019C6CF
		public static LocalizedString SpamFilteringTestActionAddXHeader
		{
			get
			{
				return new LocalizedString("SpamFilteringTestActionAddXHeader", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C8B RID: 31883 RVA: 0x0019E4F0 File Offset: 0x0019C6F0
		public static LocalizedString ErrorTargetOrSourceForestPopulatedStatusNotStarted(string fqdn1, string fqdn2)
		{
			return new LocalizedString("ErrorTargetOrSourceForestPopulatedStatusNotStarted", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				fqdn1,
				fqdn2
			});
		}

		// Token: 0x17002D60 RID: 11616
		// (get) Token: 0x06007C8C RID: 31884 RVA: 0x0019E523 File Offset: 0x0019C723
		public static LocalizedString OrganizationCapabilityScaleOut
		{
			get
			{
				return new LocalizedString("OrganizationCapabilityScaleOut", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C8D RID: 31885 RVA: 0x0019E544 File Offset: 0x0019C744
		public static LocalizedString BadSwapResourceIds(byte rid00, byte rid01, byte rid10, byte rid11)
		{
			return new LocalizedString("BadSwapResourceIds", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				rid00,
				rid01,
				rid10,
				rid11
			});
		}

		// Token: 0x06007C8E RID: 31886 RVA: 0x0019E594 File Offset: 0x0019C794
		public static LocalizedString ExceptionADInvalidPassword(string server)
		{
			return new LocalizedString("ExceptionADInvalidPassword", "ExA04FE5", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06007C8F RID: 31887 RVA: 0x0019E5C4 File Offset: 0x0019C7C4
		public static LocalizedString ConfigurationSettingsDuplicateRestriction(string name, string groupName)
		{
			return new LocalizedString("ConfigurationSettingsDuplicateRestriction", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				name,
				groupName
			});
		}

		// Token: 0x17002D61 RID: 11617
		// (get) Token: 0x06007C90 RID: 31888 RVA: 0x0019E5F7 File Offset: 0x0019C7F7
		public static LocalizedString ConstraintViolationOneOffSupervisionListEntryStringPartIsInvalid
		{
			get
			{
				return new LocalizedString("ConstraintViolationOneOffSupervisionListEntryStringPartIsInvalid", "Ex21A177", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C91 RID: 31889 RVA: 0x0019E618 File Offset: 0x0019C818
		public static LocalizedString InvalidForestFqdnInGls(string forestName, string tenantName, string message)
		{
			return new LocalizedString("InvalidForestFqdnInGls", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				forestName,
				tenantName,
				message
			});
		}

		// Token: 0x17002D62 RID: 11618
		// (get) Token: 0x06007C92 RID: 31890 RVA: 0x0019E64F File Offset: 0x0019C84F
		public static LocalizedString DiscoveryMailboxTypeDetails
		{
			get
			{
				return new LocalizedString("DiscoveryMailboxTypeDetails", "ExC38CF6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C93 RID: 31891 RVA: 0x0019E670 File Offset: 0x0019C870
		public static LocalizedString ErrorNonUniqueExchangeObjectId(string exchangeObjectIdString)
		{
			return new LocalizedString("ErrorNonUniqueExchangeObjectId", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				exchangeObjectIdString
			});
		}

		// Token: 0x17002D63 RID: 11619
		// (get) Token: 0x06007C94 RID: 31892 RVA: 0x0019E69F File Offset: 0x0019C89F
		public static LocalizedString ErrorAdfsTrustedIssuers
		{
			get
			{
				return new LocalizedString("ErrorAdfsTrustedIssuers", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D64 RID: 11620
		// (get) Token: 0x06007C95 RID: 31893 RVA: 0x0019E6BD File Offset: 0x0019C8BD
		public static LocalizedString DataMoveReplicationConstraintCIAllDatacenters
		{
			get
			{
				return new LocalizedString("DataMoveReplicationConstraintCIAllDatacenters", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D65 RID: 11621
		// (get) Token: 0x06007C96 RID: 31894 RVA: 0x0019E6DB File Offset: 0x0019C8DB
		public static LocalizedString HygieneSuiteStandard
		{
			get
			{
				return new LocalizedString("HygieneSuiteStandard", "Ex6510CC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C97 RID: 31895 RVA: 0x0019E6FC File Offset: 0x0019C8FC
		public static LocalizedString ErrorNonUniqueMailboxGetMailboxLocation(string mailboxLocationType)
		{
			return new LocalizedString("ErrorNonUniqueMailboxGetMailboxLocation", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				mailboxLocationType
			});
		}

		// Token: 0x06007C98 RID: 31896 RVA: 0x0019E72C File Offset: 0x0019C92C
		public static LocalizedString ErrorMailTipDisplayableLengthExceeded(int max)
		{
			return new LocalizedString("ErrorMailTipDisplayableLengthExceeded", "Ex2C2349", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				max
			});
		}

		// Token: 0x06007C99 RID: 31897 RVA: 0x0019E760 File Offset: 0x0019C960
		public static LocalizedString ErrorIsServerSuitableMissingComputerData(string fqdn, string dcName)
		{
			return new LocalizedString("ErrorIsServerSuitableMissingComputerData", "Ex62AC2D", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				fqdn,
				dcName
			});
		}

		// Token: 0x06007C9A RID: 31898 RVA: 0x0019E794 File Offset: 0x0019C994
		public static LocalizedString ConversionFailed(string propertyName, string typeName, string errorMessage)
		{
			return new LocalizedString("ConversionFailed", "Ex235FAC", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				typeName,
				errorMessage
			});
		}

		// Token: 0x17002D66 RID: 11622
		// (get) Token: 0x06007C9B RID: 31899 RVA: 0x0019E7CB File Offset: 0x0019C9CB
		public static LocalizedString EsnLangHindi
		{
			get
			{
				return new LocalizedString("EsnLangHindi", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C9C RID: 31900 RVA: 0x0019E7EC File Offset: 0x0019C9EC
		public static LocalizedString ExceptionOneTimeBindFailed(string serverName, string message)
		{
			return new LocalizedString("ExceptionOneTimeBindFailed", "Ex422932", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				serverName,
				message
			});
		}

		// Token: 0x17002D67 RID: 11623
		// (get) Token: 0x06007C9D RID: 31901 RVA: 0x0019E81F File Offset: 0x0019CA1F
		public static LocalizedString ExceptionUnableToCreateConnections
		{
			get
			{
				return new LocalizedString("ExceptionUnableToCreateConnections", "Ex1F8D9B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007C9E RID: 31902 RVA: 0x0019E840 File Offset: 0x0019CA40
		public static LocalizedString ExceptionDefaultScopeInvalidFormat(string scope)
		{
			return new LocalizedString("ExceptionDefaultScopeInvalidFormat", "Ex451A06", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scope
			});
		}

		// Token: 0x17002D68 RID: 11624
		// (get) Token: 0x06007C9F RID: 31903 RVA: 0x0019E86F File Offset: 0x0019CA6F
		public static LocalizedString SecurityPrincipalTypeWellknownSecurityPrincipal
		{
			get
			{
				return new LocalizedString("SecurityPrincipalTypeWellknownSecurityPrincipal", "ExADF61F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CA0 RID: 31904 RVA: 0x0019E890 File Offset: 0x0019CA90
		public static LocalizedString TenantNameTooLong(string name)
		{
			return new LocalizedString("TenantNameTooLong", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06007CA1 RID: 31905 RVA: 0x0019E8C0 File Offset: 0x0019CAC0
		public static LocalizedString ExArgumentOutOfRangeException(string paramName, object actualValue)
		{
			return new LocalizedString("ExArgumentOutOfRangeException", "ExE05A51", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				paramName,
				actualValue
			});
		}

		// Token: 0x17002D69 RID: 11625
		// (get) Token: 0x06007CA2 RID: 31906 RVA: 0x0019E8F3 File Offset: 0x0019CAF3
		public static LocalizedString Error
		{
			get
			{
				return new LocalizedString("Error", "ExDCC897", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CA3 RID: 31907 RVA: 0x0019E914 File Offset: 0x0019CB14
		public static LocalizedString ErrorInvalidISOTwoLetterOrCountryCode(string name)
		{
			return new LocalizedString("ErrorInvalidISOTwoLetterOrCountryCode", "ExE0452E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17002D6A RID: 11626
		// (get) Token: 0x06007CA4 RID: 31908 RVA: 0x0019E943 File Offset: 0x0019CB43
		public static LocalizedString ElcScheduleOnWrongServer
		{
			get
			{
				return new LocalizedString("ElcScheduleOnWrongServer", "Ex968F4D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D6B RID: 11627
		// (get) Token: 0x06007CA5 RID: 31909 RVA: 0x0019E961 File Offset: 0x0019CB61
		public static LocalizedString SyncIssues
		{
			get
			{
				return new LocalizedString("SyncIssues", "Ex35C079", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CA6 RID: 31910 RVA: 0x0019E980 File Offset: 0x0019CB80
		public static LocalizedString ConfigurationSettingsDatabaseNotFound(string id)
		{
			return new LocalizedString("ConfigurationSettingsDatabaseNotFound", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002D6C RID: 11628
		// (get) Token: 0x06007CA7 RID: 31911 RVA: 0x0019E9AF File Offset: 0x0019CBAF
		public static LocalizedString PartiallyApplied
		{
			get
			{
				return new LocalizedString("PartiallyApplied", "Ex17281D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D6D RID: 11629
		// (get) Token: 0x06007CA8 RID: 31912 RVA: 0x0019E9CD File Offset: 0x0019CBCD
		public static LocalizedString PreferredInternetCodePageUndefined
		{
			get
			{
				return new LocalizedString("PreferredInternetCodePageUndefined", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D6E RID: 11630
		// (get) Token: 0x06007CA9 RID: 31913 RVA: 0x0019E9EB File Offset: 0x0019CBEB
		public static LocalizedString NoRoleEntriesCmdletOrScriptFound
		{
			get
			{
				return new LocalizedString("NoRoleEntriesCmdletOrScriptFound", "Ex6F96DA", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CAA RID: 31914 RVA: 0x0019EA0C File Offset: 0x0019CC0C
		public static LocalizedString ApiNotSupportedError(string cl, string member)
		{
			return new LocalizedString("ApiNotSupportedError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				cl,
				member
			});
		}

		// Token: 0x17002D6F RID: 11631
		// (get) Token: 0x06007CAB RID: 31915 RVA: 0x0019EA3F File Offset: 0x0019CC3F
		public static LocalizedString CannotDeserializePartitionHintTooShort
		{
			get
			{
				return new LocalizedString("CannotDeserializePartitionHintTooShort", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CAC RID: 31916 RVA: 0x0019EA60 File Offset: 0x0019CC60
		public static LocalizedString ErrorDatabaseCopiesInvalid(string dbName)
		{
			return new LocalizedString("ErrorDatabaseCopiesInvalid", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x17002D70 RID: 11632
		// (get) Token: 0x06007CAD RID: 31917 RVA: 0x0019EA8F File Offset: 0x0019CC8F
		public static LocalizedString InvalidReceiveAuthModeTLSPassword
		{
			get
			{
				return new LocalizedString("InvalidReceiveAuthModeTLSPassword", "ExA0E3C8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D71 RID: 11633
		// (get) Token: 0x06007CAE RID: 31918 RVA: 0x0019EAAD File Offset: 0x0019CCAD
		public static LocalizedString GroupNamingPolicyCustomAttribute8
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute8", "Ex504F82", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D72 RID: 11634
		// (get) Token: 0x06007CAF RID: 31919 RVA: 0x0019EACB File Offset: 0x0019CCCB
		public static LocalizedString EsnLangSwedish
		{
			get
			{
				return new LocalizedString("EsnLangSwedish", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CB0 RID: 31920 RVA: 0x0019EAEC File Offset: 0x0019CCEC
		public static LocalizedString InvalidExtension(string property, int length)
		{
			return new LocalizedString("InvalidExtension", "Ex04010B", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property,
				length
			});
		}

		// Token: 0x17002D73 RID: 11635
		// (get) Token: 0x06007CB1 RID: 31921 RVA: 0x0019EB24 File Offset: 0x0019CD24
		public static LocalizedString IndustryUtilities
		{
			get
			{
				return new LocalizedString("IndustryUtilities", "Ex9A2A93", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D74 RID: 11636
		// (get) Token: 0x06007CB2 RID: 31922 RVA: 0x0019EB42 File Offset: 0x0019CD42
		public static LocalizedString G711
		{
			get
			{
				return new LocalizedString("G711", "ExF843A4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D75 RID: 11637
		// (get) Token: 0x06007CB3 RID: 31923 RVA: 0x0019EB60 File Offset: 0x0019CD60
		public static LocalizedString ExternalDNSServersNotSet
		{
			get
			{
				return new LocalizedString("ExternalDNSServersNotSet", "Ex040011", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D76 RID: 11638
		// (get) Token: 0x06007CB4 RID: 31924 RVA: 0x0019EB7E File Offset: 0x0019CD7E
		public static LocalizedString Item
		{
			get
			{
				return new LocalizedString("Item", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D77 RID: 11639
		// (get) Token: 0x06007CB5 RID: 31925 RVA: 0x0019EB9C File Offset: 0x0019CD9C
		public static LocalizedString LdapFilterErrorUnsupportedAttributeType
		{
			get
			{
				return new LocalizedString("LdapFilterErrorUnsupportedAttributeType", "Ex4A2EBD", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CB6 RID: 31926 RVA: 0x0019EBBC File Offset: 0x0019CDBC
		public static LocalizedString ExceptionDnLimitExceeded(int actualCount, int limit)
		{
			return new LocalizedString("ExceptionDnLimitExceeded", "Ex4D1B00", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				actualCount,
				limit
			});
		}

		// Token: 0x17002D78 RID: 11640
		// (get) Token: 0x06007CB7 RID: 31927 RVA: 0x0019EBF9 File Offset: 0x0019CDF9
		public static LocalizedString ExternalSenderAdminAddressRequired
		{
			get
			{
				return new LocalizedString("ExternalSenderAdminAddressRequired", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CB8 RID: 31928 RVA: 0x0019EC18 File Offset: 0x0019CE18
		public static LocalizedString ConfigurationSettingsRestrictionNotExpected(string name)
		{
			return new LocalizedString("ConfigurationSettingsRestrictionNotExpected", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17002D79 RID: 11641
		// (get) Token: 0x06007CB9 RID: 31929 RVA: 0x0019EC47 File Offset: 0x0019CE47
		public static LocalizedString ErrorBadLocalizedFolderName
		{
			get
			{
				return new LocalizedString("ErrorBadLocalizedFolderName", "Ex096A6F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D7A RID: 11642
		// (get) Token: 0x06007CBA RID: 31930 RVA: 0x0019EC65 File Offset: 0x0019CE65
		public static LocalizedString AutoDatabaseMountDialBestAvailability
		{
			get
			{
				return new LocalizedString("AutoDatabaseMountDialBestAvailability", "Ex200EC7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D7B RID: 11643
		// (get) Token: 0x06007CBB RID: 31931 RVA: 0x0019EC83 File Offset: 0x0019CE83
		public static LocalizedString OrganizationalFolder
		{
			get
			{
				return new LocalizedString("OrganizationalFolder", "ExBAF4FF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CBC RID: 31932 RVA: 0x0019ECA4 File Offset: 0x0019CEA4
		public static LocalizedString ErrorNotNullProperty(string propertyName)
		{
			return new LocalizedString("ErrorNotNullProperty", "Ex52EBD7", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x06007CBD RID: 31933 RVA: 0x0019ECD4 File Offset: 0x0019CED4
		public static LocalizedString ErrorConversionFailedWithError(string name, uint err)
		{
			return new LocalizedString("ErrorConversionFailedWithError", "ExF4B43B", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				name,
				err
			});
		}

		// Token: 0x06007CBE RID: 31934 RVA: 0x0019ED0C File Offset: 0x0019CF0C
		public static LocalizedString ExceptionCannotBindToDomain(string domaincontroller, string domain, string errorCode)
		{
			return new LocalizedString("ExceptionCannotBindToDomain", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				domaincontroller,
				domain,
				errorCode
			});
		}

		// Token: 0x17002D7C RID: 11644
		// (get) Token: 0x06007CBF RID: 31935 RVA: 0x0019ED43 File Offset: 0x0019CF43
		public static LocalizedString SpamFilteringOptionTest
		{
			get
			{
				return new LocalizedString("SpamFilteringOptionTest", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CC0 RID: 31936 RVA: 0x0019ED64 File Offset: 0x0019CF64
		public static LocalizedString ExceptionInvalidCredentialsFailedToGetIdentity(string server)
		{
			return new LocalizedString("ExceptionInvalidCredentialsFailedToGetIdentity", "ExF3DAEC", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06007CC1 RID: 31937 RVA: 0x0019ED94 File Offset: 0x0019CF94
		public static LocalizedString CannotCalculateProperty(string calculatedPropertyName, string errorMessage)
		{
			return new LocalizedString("CannotCalculateProperty", "Ex35C090", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				calculatedPropertyName,
				errorMessage
			});
		}

		// Token: 0x06007CC2 RID: 31938 RVA: 0x0019EDC8 File Offset: 0x0019CFC8
		public static LocalizedString ExceptionNotifyErrorGettingResults(string server)
		{
			return new LocalizedString("ExceptionNotifyErrorGettingResults", "ExB18FF0", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06007CC3 RID: 31939 RVA: 0x0019EDF8 File Offset: 0x0019CFF8
		public static LocalizedString ErrorSettingOverrideInvalidParameterSyntax(string componentName, string sectionName, string parameter)
		{
			return new LocalizedString("ErrorSettingOverrideInvalidParameterSyntax", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				componentName,
				sectionName,
				parameter
			});
		}

		// Token: 0x17002D7D RID: 11645
		// (get) Token: 0x06007CC4 RID: 31940 RVA: 0x0019EE2F File Offset: 0x0019D02F
		public static LocalizedString LdapFilterErrorInvalidToken
		{
			get
			{
				return new LocalizedString("LdapFilterErrorInvalidToken", "ExB88801", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D7E RID: 11646
		// (get) Token: 0x06007CC5 RID: 31941 RVA: 0x0019EE4D File Offset: 0x0019D04D
		public static LocalizedString MessageRateSourceFlagsUser
		{
			get
			{
				return new LocalizedString("MessageRateSourceFlagsUser", "Ex5D2DFE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CC6 RID: 31942 RVA: 0x0019EE6C File Offset: 0x0019D06C
		public static LocalizedString WrongDelegationTypeForPolicy(string roleAssignment)
		{
			return new LocalizedString("WrongDelegationTypeForPolicy", "Ex44DDEC", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				roleAssignment
			});
		}

		// Token: 0x17002D7F RID: 11647
		// (get) Token: 0x06007CC7 RID: 31943 RVA: 0x0019EE9B File Offset: 0x0019D09B
		public static LocalizedString TextEnrichedAndTextAlternative
		{
			get
			{
				return new LocalizedString("TextEnrichedAndTextAlternative", "Ex80E553", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CC8 RID: 31944 RVA: 0x0019EEBC File Offset: 0x0019D0BC
		public static LocalizedString ExceptionCreateLdapConnection(string server, string message, uint errorCode)
		{
			return new LocalizedString("ExceptionCreateLdapConnection", "Ex267C27", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				message,
				errorCode
			});
		}

		// Token: 0x17002D80 RID: 11648
		// (get) Token: 0x06007CC9 RID: 31945 RVA: 0x0019EEF8 File Offset: 0x0019D0F8
		public static LocalizedString FederatedOrganizationIdNoFederatedDomains
		{
			get
			{
				return new LocalizedString("FederatedOrganizationIdNoFederatedDomains", "ExB63C8A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CCA RID: 31946 RVA: 0x0019EF18 File Offset: 0x0019D118
		public static LocalizedString ExceptionADTopologyErrorWhenLookingForTrustRelationships(int error, string fqdn, string message)
		{
			return new LocalizedString("ExceptionADTopologyErrorWhenLookingForTrustRelationships", "Ex289101", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				error,
				fqdn,
				message
			});
		}

		// Token: 0x06007CCB RID: 31947 RVA: 0x0019EF54 File Offset: 0x0019D154
		public static LocalizedString ErrorMailboxCollectionNotSupportType(string mailboxLocationType)
		{
			return new LocalizedString("ErrorMailboxCollectionNotSupportType", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				mailboxLocationType
			});
		}

		// Token: 0x17002D81 RID: 11649
		// (get) Token: 0x06007CCC RID: 31948 RVA: 0x0019EF83 File Offset: 0x0019D183
		public static LocalizedString GroupTypeFlagsUniversal
		{
			get
			{
				return new LocalizedString("GroupTypeFlagsUniversal", "Ex4EB5B1", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D82 RID: 11650
		// (get) Token: 0x06007CCD RID: 31949 RVA: 0x0019EFA1 File Offset: 0x0019D1A1
		public static LocalizedString CustomAlertTextRequired
		{
			get
			{
				return new LocalizedString("CustomAlertTextRequired", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CCE RID: 31950 RVA: 0x0019EFC0 File Offset: 0x0019D1C0
		public static LocalizedString ExceptionInvalidVlvFilterOption(string matchOption)
		{
			return new LocalizedString("ExceptionInvalidVlvFilterOption", "ExA0016D", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				matchOption
			});
		}

		// Token: 0x17002D83 RID: 11651
		// (get) Token: 0x06007CCF RID: 31951 RVA: 0x0019EFEF File Offset: 0x0019D1EF
		public static LocalizedString EsnLangEstonian
		{
			get
			{
				return new LocalizedString("EsnLangEstonian", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D84 RID: 11652
		// (get) Token: 0x06007CD0 RID: 31952 RVA: 0x0019F00D File Offset: 0x0019D20D
		public static LocalizedString Low
		{
			get
			{
				return new LocalizedString("Low", "ExFCE381", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D85 RID: 11653
		// (get) Token: 0x06007CD1 RID: 31953 RVA: 0x0019F02B File Offset: 0x0019D22B
		public static LocalizedString IndustryPersonalServices
		{
			get
			{
				return new LocalizedString("IndustryPersonalServices", "Ex6209CE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D86 RID: 11654
		// (get) Token: 0x06007CD2 RID: 31954 RVA: 0x0019F049 File Offset: 0x0019D249
		public static LocalizedString ErrorInvalidPipelineTracingSenderAddress
		{
			get
			{
				return new LocalizedString("ErrorInvalidPipelineTracingSenderAddress", "Ex32A044", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D87 RID: 11655
		// (get) Token: 0x06007CD3 RID: 31955 RVA: 0x0019F067 File Offset: 0x0019D267
		public static LocalizedString AccessQuarantined
		{
			get
			{
				return new LocalizedString("AccessQuarantined", "Ex775044", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D88 RID: 11656
		// (get) Token: 0x06007CD4 RID: 31956 RVA: 0x0019F085 File Offset: 0x0019D285
		public static LocalizedString LdapFilterErrorTypeOnlySpaces
		{
			get
			{
				return new LocalizedString("LdapFilterErrorTypeOnlySpaces", "Ex210D48", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CD5 RID: 31957 RVA: 0x0019F0A4 File Offset: 0x0019D2A4
		public static LocalizedString ServerSideADTopologyUnexpectedError(string server, string error)
		{
			return new LocalizedString("ServerSideADTopologyUnexpectedError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				error
			});
		}

		// Token: 0x17002D89 RID: 11657
		// (get) Token: 0x06007CD6 RID: 31958 RVA: 0x0019F0D7 File Offset: 0x0019D2D7
		public static LocalizedString UserFilterChoice
		{
			get
			{
				return new LocalizedString("UserFilterChoice", "Ex5F282E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CD7 RID: 31959 RVA: 0x0019F0F8 File Offset: 0x0019D2F8
		public static LocalizedString CannotBuildCapabilityFilterUnsupportedCapability(string capability)
		{
			return new LocalizedString("CannotBuildCapabilityFilterUnsupportedCapability", "Ex7B88DE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				capability
			});
		}

		// Token: 0x06007CD8 RID: 31960 RVA: 0x0019F128 File Offset: 0x0019D328
		public static LocalizedString ExceptionInvalidVlvFilter(string filterType)
		{
			return new LocalizedString("ExceptionInvalidVlvFilter", "ExDA53B9", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				filterType
			});
		}

		// Token: 0x17002D8A RID: 11658
		// (get) Token: 0x06007CD9 RID: 31961 RVA: 0x0019F157 File Offset: 0x0019D357
		public static LocalizedString ErrorRemovePrimaryExternalSMTPAddress
		{
			get
			{
				return new LocalizedString("ErrorRemovePrimaryExternalSMTPAddress", "Ex3EA3AC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D8B RID: 11659
		// (get) Token: 0x06007CDA RID: 31962 RVA: 0x0019F175 File Offset: 0x0019D375
		public static LocalizedString GroupNamingPolicyOffice
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyOffice", "Ex3BB510", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D8C RID: 11660
		// (get) Token: 0x06007CDB RID: 31963 RVA: 0x0019F193 File Offset: 0x0019D393
		public static LocalizedString ErrorHostServerNotSet
		{
			get
			{
				return new LocalizedString("ErrorHostServerNotSet", "Ex3D0E53", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D8D RID: 11661
		// (get) Token: 0x06007CDC RID: 31964 RVA: 0x0019F1B1 File Offset: 0x0019D3B1
		public static LocalizedString BitMaskOrIpAddressMatchMustBeSet
		{
			get
			{
				return new LocalizedString("BitMaskOrIpAddressMatchMustBeSet", "ExA1D701", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CDD RID: 31965 RVA: 0x0019F1D0 File Offset: 0x0019D3D0
		public static LocalizedString PerimeterSettingsAmbiguousException(string orgId)
		{
			return new LocalizedString("PerimeterSettingsAmbiguousException", "Ex92854D", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				orgId
			});
		}

		// Token: 0x17002D8E RID: 11662
		// (get) Token: 0x06007CDE RID: 31966 RVA: 0x0019F1FF File Offset: 0x0019D3FF
		public static LocalizedString OrganizationCapabilityGMGen
		{
			get
			{
				return new LocalizedString("OrganizationCapabilityGMGen", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D8F RID: 11663
		// (get) Token: 0x06007CDF RID: 31967 RVA: 0x0019F21D File Offset: 0x0019D41D
		public static LocalizedString ErrorArchiveDatabaseArchiveDomainConflict
		{
			get
			{
				return new LocalizedString("ErrorArchiveDatabaseArchiveDomainConflict", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D90 RID: 11664
		// (get) Token: 0x06007CE0 RID: 31968 RVA: 0x0019F23B File Offset: 0x0019D43B
		public static LocalizedString ArchiveStateHostedProvisioned
		{
			get
			{
				return new LocalizedString("ArchiveStateHostedProvisioned", "Ex9DB0C4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CE1 RID: 31969 RVA: 0x0019F25C File Offset: 0x0019D45C
		public static LocalizedString ExceptionCannotRemoveDsServer(string server)
		{
			return new LocalizedString("ExceptionCannotRemoveDsServer", "Ex4729E3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06007CE2 RID: 31970 RVA: 0x0019F28C File Offset: 0x0019D48C
		public static LocalizedString ExceptionCannotUseCredentials(TopologyMode topo)
		{
			return new LocalizedString("ExceptionCannotUseCredentials", "ExDEFCDE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				topo
			});
		}

		// Token: 0x17002D91 RID: 11665
		// (get) Token: 0x06007CE3 RID: 31971 RVA: 0x0019F2C0 File Offset: 0x0019D4C0
		public static LocalizedString InvalidHttpProtocolLogSizeConfiguration
		{
			get
			{
				return new LocalizedString("InvalidHttpProtocolLogSizeConfiguration", "ExFDC4E4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CE4 RID: 31972 RVA: 0x0019F2E0 File Offset: 0x0019D4E0
		public static LocalizedString ExceptionUnsupportedFilterForProperty(string propertyName, Type filterType, Type supportedType)
		{
			return new LocalizedString("ExceptionUnsupportedFilterForProperty", "Ex988CDB", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				filterType,
				supportedType
			});
		}

		// Token: 0x06007CE5 RID: 31973 RVA: 0x0019F318 File Offset: 0x0019D518
		public static LocalizedString SuitabilityReachabilityError(string fqdn, string port, string details)
		{
			return new LocalizedString("SuitabilityReachabilityError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				fqdn,
				port,
				details
			});
		}

		// Token: 0x17002D92 RID: 11666
		// (get) Token: 0x06007CE6 RID: 31974 RVA: 0x0019F34F File Offset: 0x0019D54F
		public static LocalizedString PermanentMservErrorDescription
		{
			get
			{
				return new LocalizedString("PermanentMservErrorDescription", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D93 RID: 11667
		// (get) Token: 0x06007CE7 RID: 31975 RVA: 0x0019F36D File Offset: 0x0019D56D
		public static LocalizedString CustomExternalBodyRequired
		{
			get
			{
				return new LocalizedString("CustomExternalBodyRequired", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D94 RID: 11668
		// (get) Token: 0x06007CE8 RID: 31976 RVA: 0x0019F38B File Offset: 0x0019D58B
		public static LocalizedString LdapFilterErrorUndefinedAttributeType
		{
			get
			{
				return new LocalizedString("LdapFilterErrorUndefinedAttributeType", "Ex987FCC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D95 RID: 11669
		// (get) Token: 0x06007CE9 RID: 31977 RVA: 0x0019F3A9 File Offset: 0x0019D5A9
		public static LocalizedString ErrorTextMessageIncludingHtmlBody
		{
			get
			{
				return new LocalizedString("ErrorTextMessageIncludingHtmlBody", "ExEB2B72", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D96 RID: 11670
		// (get) Token: 0x06007CEA RID: 31978 RVA: 0x0019F3C7 File Offset: 0x0019D5C7
		public static LocalizedString WellKnownRecipientTypeResources
		{
			get
			{
				return new LocalizedString("WellKnownRecipientTypeResources", "Ex90AF45", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D97 RID: 11671
		// (get) Token: 0x06007CEB RID: 31979 RVA: 0x0019F3E5 File Offset: 0x0019D5E5
		public static LocalizedString PrimaryDefault
		{
			get
			{
				return new LocalizedString("PrimaryDefault", "Ex9EFEF4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D98 RID: 11672
		// (get) Token: 0x06007CEC RID: 31980 RVA: 0x0019F403 File Offset: 0x0019D603
		public static LocalizedString MailFlowPartnerInternalMailContentTypeMimeHtmlText
		{
			get
			{
				return new LocalizedString("MailFlowPartnerInternalMailContentTypeMimeHtmlText", "Ex456B24", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D99 RID: 11673
		// (get) Token: 0x06007CED RID: 31981 RVA: 0x0019F421 File Offset: 0x0019D621
		public static LocalizedString DataMoveReplicationConstraintNone
		{
			get
			{
				return new LocalizedString("DataMoveReplicationConstraintNone", "Ex5E0C04", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D9A RID: 11674
		// (get) Token: 0x06007CEE RID: 31982 RVA: 0x0019F43F File Offset: 0x0019D63F
		public static LocalizedString ErrorAdfsAudienceUris
		{
			get
			{
				return new LocalizedString("ErrorAdfsAudienceUris", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D9B RID: 11675
		// (get) Token: 0x06007CEF RID: 31983 RVA: 0x0019F45D File Offset: 0x0019D65D
		public static LocalizedString InvalidAnrFilter
		{
			get
			{
				return new LocalizedString("InvalidAnrFilter", "ExCA2BA5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D9C RID: 11676
		// (get) Token: 0x06007CF0 RID: 31984 RVA: 0x0019F47B File Offset: 0x0019D67B
		public static LocalizedString AuditLogMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("AuditLogMailboxRecipientTypeDetails", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D9D RID: 11677
		// (get) Token: 0x06007CF1 RID: 31985 RVA: 0x0019F499 File Offset: 0x0019D699
		public static LocalizedString WellKnownRecipientTypeNone
		{
			get
			{
				return new LocalizedString("WellKnownRecipientTypeNone", "ExC8B2E3", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D9E RID: 11678
		// (get) Token: 0x06007CF2 RID: 31986 RVA: 0x0019F4B7 File Offset: 0x0019D6B7
		public static LocalizedString EsnLangGujarati
		{
			get
			{
				return new LocalizedString("EsnLangGujarati", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002D9F RID: 11679
		// (get) Token: 0x06007CF3 RID: 31987 RVA: 0x0019F4D5 File Offset: 0x0019D6D5
		public static LocalizedString DomainStateUnknown
		{
			get
			{
				return new LocalizedString("DomainStateUnknown", "Ex44041F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DA0 RID: 11680
		// (get) Token: 0x06007CF4 RID: 31988 RVA: 0x0019F4F3 File Offset: 0x0019D6F3
		public static LocalizedString IndustryManufacturing
		{
			get
			{
				return new LocalizedString("IndustryManufacturing", "ExB982E6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DA1 RID: 11681
		// (get) Token: 0x06007CF5 RID: 31989 RVA: 0x0019F511 File Offset: 0x0019D711
		public static LocalizedString IndustryHospitality
		{
			get
			{
				return new LocalizedString("IndustryHospitality", "Ex720546", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CF6 RID: 31990 RVA: 0x0019F530 File Offset: 0x0019D730
		public static LocalizedString ExceptionInvalidOperationOnReadOnlySession(string operation)
		{
			return new LocalizedString("ExceptionInvalidOperationOnReadOnlySession", "Ex069AB3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				operation
			});
		}

		// Token: 0x17002DA2 RID: 11682
		// (get) Token: 0x06007CF7 RID: 31991 RVA: 0x0019F55F File Offset: 0x0019D75F
		public static LocalizedString ErrorAdfsIssuer
		{
			get
			{
				return new LocalizedString("ErrorAdfsIssuer", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DA3 RID: 11683
		// (get) Token: 0x06007CF8 RID: 31992 RVA: 0x0019F57D File Offset: 0x0019D77D
		public static LocalizedString EmailAgeFilterOneDay
		{
			get
			{
				return new LocalizedString("EmailAgeFilterOneDay", "Ex13CBCE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DA4 RID: 11684
		// (get) Token: 0x06007CF9 RID: 31993 RVA: 0x0019F59B File Offset: 0x0019D79B
		public static LocalizedString AllEmailMC
		{
			get
			{
				return new LocalizedString("AllEmailMC", "ExE78357", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CFA RID: 31994 RVA: 0x0019F5BC File Offset: 0x0019D7BC
		public static LocalizedString ConfigurationSettingsRestrictionExpected(string name)
		{
			return new LocalizedString("ConfigurationSettingsRestrictionExpected", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17002DA5 RID: 11685
		// (get) Token: 0x06007CFB RID: 31995 RVA: 0x0019F5EB File Offset: 0x0019D7EB
		public static LocalizedString OrgContainerAmbiguousException
		{
			get
			{
				return new LocalizedString("OrgContainerAmbiguousException", "ExE63874", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CFC RID: 31996 RVA: 0x0019F60C File Offset: 0x0019D80C
		public static LocalizedString ExceptionADTopologyServiceNotStarted(string server)
		{
			return new LocalizedString("ExceptionADTopologyServiceNotStarted", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17002DA6 RID: 11686
		// (get) Token: 0x06007CFD RID: 31997 RVA: 0x0019F63B File Offset: 0x0019D83B
		public static LocalizedString GlobalThrottlingPolicyNotFoundException
		{
			get
			{
				return new LocalizedString("GlobalThrottlingPolicyNotFoundException", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007CFE RID: 31998 RVA: 0x0019F65C File Offset: 0x0019D85C
		public static LocalizedString ExEmptyStringArgumentException(string paramName)
		{
			return new LocalizedString("ExEmptyStringArgumentException", "ExC8F283", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				paramName
			});
		}

		// Token: 0x06007CFF RID: 31999 RVA: 0x0019F68C File Offset: 0x0019D88C
		public static LocalizedString ConstraintLocationValueReservedForSystemUse(string constraintNameValue)
		{
			return new LocalizedString("ConstraintLocationValueReservedForSystemUse", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				constraintNameValue
			});
		}

		// Token: 0x17002DA7 RID: 11687
		// (get) Token: 0x06007D00 RID: 32000 RVA: 0x0019F6BB File Offset: 0x0019D8BB
		public static LocalizedString EsnLangTurkish
		{
			get
			{
				return new LocalizedString("EsnLangTurkish", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D01 RID: 32001 RVA: 0x0019F6DC File Offset: 0x0019D8DC
		public static LocalizedString FailedToReadAlternateServiceAccountConfigFromRegistry(string keyPath)
		{
			return new LocalizedString("FailedToReadAlternateServiceAccountConfigFromRegistry", "ExC76F9D", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				keyPath
			});
		}

		// Token: 0x06007D02 RID: 32002 RVA: 0x0019F70C File Offset: 0x0019D90C
		public static LocalizedString ErrorGlobalWebDistributionAndVDirsSet(string name)
		{
			return new LocalizedString("ErrorGlobalWebDistributionAndVDirsSet", "Ex012158", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06007D03 RID: 32003 RVA: 0x0019F73C File Offset: 0x0019D93C
		public static LocalizedString ServerComponentLocalRegistryError(string regErrorStr)
		{
			return new LocalizedString("ServerComponentLocalRegistryError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				regErrorStr
			});
		}

		// Token: 0x17002DA8 RID: 11688
		// (get) Token: 0x06007D04 RID: 32004 RVA: 0x0019F76B File Offset: 0x0019D96B
		public static LocalizedString SKUCapabilityBPOSSLite
		{
			get
			{
				return new LocalizedString("SKUCapabilityBPOSSLite", "Ex6A7867", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DA9 RID: 11689
		// (get) Token: 0x06007D05 RID: 32005 RVA: 0x0019F789 File Offset: 0x0019D989
		public static LocalizedString RecipientWriteScopes
		{
			get
			{
				return new LocalizedString("RecipientWriteScopes", "Ex4D4E9F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DAA RID: 11690
		// (get) Token: 0x06007D06 RID: 32006 RVA: 0x0019F7A7 File Offset: 0x0019D9A7
		public static LocalizedString CalendarAgeFilterThreeMonths
		{
			get
			{
				return new LocalizedString("CalendarAgeFilterThreeMonths", "Ex5D7DB9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DAB RID: 11691
		// (get) Token: 0x06007D07 RID: 32007 RVA: 0x0019F7C5 File Offset: 0x0019D9C5
		public static LocalizedString MailboxMoveStatusCompletedWithWarning
		{
			get
			{
				return new LocalizedString("MailboxMoveStatusCompletedWithWarning", "ExFA97CF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DAC RID: 11692
		// (get) Token: 0x06007D08 RID: 32008 RVA: 0x0019F7E3 File Offset: 0x0019D9E3
		public static LocalizedString GroupNamingPolicyCountryOrRegion
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCountryOrRegion", "ExB259C2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DAD RID: 11693
		// (get) Token: 0x06007D09 RID: 32009 RVA: 0x0019F801 File Offset: 0x0019DA01
		public static LocalizedString EsnLangFrench
		{
			get
			{
				return new LocalizedString("EsnLangFrench", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D0A RID: 32010 RVA: 0x0019F820 File Offset: 0x0019DA20
		public static LocalizedString TooManyKeyMappings(string a)
		{
			return new LocalizedString("TooManyKeyMappings", "ExDA6DBD", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				a
			});
		}

		// Token: 0x17002DAE RID: 11694
		// (get) Token: 0x06007D0B RID: 32011 RVA: 0x0019F84F File Offset: 0x0019DA4F
		public static LocalizedString CapabilityExcludedFromBackSync
		{
			get
			{
				return new LocalizedString("CapabilityExcludedFromBackSync", "Ex7E8F7B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DAF RID: 11695
		// (get) Token: 0x06007D0C RID: 32012 RVA: 0x0019F86D File Offset: 0x0019DA6D
		public static LocalizedString CapabilityBEVDirLockdown
		{
			get
			{
				return new LocalizedString("CapabilityBEVDirLockdown", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D0D RID: 32013 RVA: 0x0019F88C File Offset: 0x0019DA8C
		public static LocalizedString ScopeCannotBeExclusive(ScopeRestrictionType scopeType)
		{
			return new LocalizedString("ScopeCannotBeExclusive", "ExF616B9", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scopeType
			});
		}

		// Token: 0x06007D0E RID: 32014 RVA: 0x0019F8C0 File Offset: 0x0019DAC0
		public static LocalizedString UnsupportedADSyntaxException(string syntax)
		{
			return new LocalizedString("UnsupportedADSyntaxException", "ExCD0111", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				syntax
			});
		}

		// Token: 0x06007D0F RID: 32015 RVA: 0x0019F8F0 File Offset: 0x0019DAF0
		public static LocalizedString CannotFindOabException(string oabId)
		{
			return new LocalizedString("CannotFindOabException", "Ex0A505A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				oabId
			});
		}

		// Token: 0x06007D10 RID: 32016 RVA: 0x0019F920 File Offset: 0x0019DB20
		public static LocalizedString ExceptionInvalidOperationOnInvalidSession(string operation)
		{
			return new LocalizedString("ExceptionInvalidOperationOnInvalidSession", "Ex6ADB8F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				operation
			});
		}

		// Token: 0x17002DB0 RID: 11696
		// (get) Token: 0x06007D11 RID: 32017 RVA: 0x0019F94F File Offset: 0x0019DB4F
		public static LocalizedString ReceiveAuthMechanismBasicAuth
		{
			get
			{
				return new LocalizedString("ReceiveAuthMechanismBasicAuth", "Ex01BF69", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DB1 RID: 11697
		// (get) Token: 0x06007D12 RID: 32018 RVA: 0x0019F96D File Offset: 0x0019DB6D
		public static LocalizedString IndustryEducation
		{
			get
			{
				return new LocalizedString("IndustryEducation", "ExD5213F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D13 RID: 32019 RVA: 0x0019F98C File Offset: 0x0019DB8C
		public static LocalizedString ErrorProperty1EqValue1WhileProperty2EqValue2(string property1Name, string value1, string property2Name, string value2)
		{
			return new LocalizedString("ErrorProperty1EqValue1WhileProperty2EqValue2", "ExF0EF89", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property1Name,
				value1,
				property2Name,
				value2
			});
		}

		// Token: 0x17002DB2 RID: 11698
		// (get) Token: 0x06007D14 RID: 32020 RVA: 0x0019F9C7 File Offset: 0x0019DBC7
		public static LocalizedString NotSpecified
		{
			get
			{
				return new LocalizedString("NotSpecified", "ExA5AF6D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DB3 RID: 11699
		// (get) Token: 0x06007D15 RID: 32021 RVA: 0x0019F9E5 File Offset: 0x0019DBE5
		public static LocalizedString PermanentlyDelete
		{
			get
			{
				return new LocalizedString("PermanentlyDelete", "ExBDB7B1", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DB4 RID: 11700
		// (get) Token: 0x06007D16 RID: 32022 RVA: 0x0019FA03 File Offset: 0x0019DC03
		public static LocalizedString FederatedIdentityMisconfigured
		{
			get
			{
				return new LocalizedString("FederatedIdentityMisconfigured", "ExAD915C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D17 RID: 32023 RVA: 0x0019FA24 File Offset: 0x0019DC24
		public static LocalizedString ErrorSettingOverrideInvalidComponentName(string componentName, string availableComponents)
		{
			return new LocalizedString("ErrorSettingOverrideInvalidComponentName", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				componentName,
				availableComponents
			});
		}

		// Token: 0x17002DB5 RID: 11701
		// (get) Token: 0x06007D18 RID: 32024 RVA: 0x0019FA57 File Offset: 0x0019DC57
		public static LocalizedString MountDialOverrideNone
		{
			get
			{
				return new LocalizedString("MountDialOverrideNone", "Ex5F8A1B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DB6 RID: 11702
		// (get) Token: 0x06007D19 RID: 32025 RVA: 0x0019FA75 File Offset: 0x0019DC75
		public static LocalizedString AlwaysUTF8
		{
			get
			{
				return new LocalizedString("AlwaysUTF8", "Ex3FF45F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DB7 RID: 11703
		// (get) Token: 0x06007D1A RID: 32026 RVA: 0x0019FA93 File Offset: 0x0019DC93
		public static LocalizedString ExceptionPagedReaderIsSingleUse
		{
			get
			{
				return new LocalizedString("ExceptionPagedReaderIsSingleUse", "ExAE75BD", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DB8 RID: 11704
		// (get) Token: 0x06007D1B RID: 32027 RVA: 0x0019FAB1 File Offset: 0x0019DCB1
		public static LocalizedString InvalidFilterLength
		{
			get
			{
				return new LocalizedString("InvalidFilterLength", "ExD5D91B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DB9 RID: 11705
		// (get) Token: 0x06007D1C RID: 32028 RVA: 0x0019FACF File Offset: 0x0019DCCF
		public static LocalizedString MailboxMoveStatusSynced
		{
			get
			{
				return new LocalizedString("MailboxMoveStatusSynced", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D1D RID: 32029 RVA: 0x0019FAF0 File Offset: 0x0019DCF0
		public static LocalizedString RecipientWriteScopeNotLessThanBecauseOfDelegationFlags(string leftScopeType, string rightScopeType)
		{
			return new LocalizedString("RecipientWriteScopeNotLessThanBecauseOfDelegationFlags", "ExCEDFCB", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				leftScopeType,
				rightScopeType
			});
		}

		// Token: 0x17002DBA RID: 11706
		// (get) Token: 0x06007D1E RID: 32030 RVA: 0x0019FB23 File Offset: 0x0019DD23
		public static LocalizedString SIPSecured
		{
			get
			{
				return new LocalizedString("SIPSecured", "Ex71C298", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DBB RID: 11707
		// (get) Token: 0x06007D1F RID: 32031 RVA: 0x0019FB41 File Offset: 0x0019DD41
		public static LocalizedString ErrorRejectedCookie
		{
			get
			{
				return new LocalizedString("ErrorRejectedCookie", "Ex28C6DD", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D20 RID: 32032 RVA: 0x0019FB60 File Offset: 0x0019DD60
		public static LocalizedString TooManyDataInLdapProperty(string ldapPropertyName, int maxCount)
		{
			return new LocalizedString("TooManyDataInLdapProperty", "Ex96AA64", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				ldapPropertyName,
				maxCount
			});
		}

		// Token: 0x17002DBC RID: 11708
		// (get) Token: 0x06007D21 RID: 32033 RVA: 0x0019FB98 File Offset: 0x0019DD98
		public static LocalizedString ASInvalidProxyASUrlOption
		{
			get
			{
				return new LocalizedString("ASInvalidProxyASUrlOption", "Ex434DE7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D22 RID: 32034 RVA: 0x0019FBB8 File Offset: 0x0019DDB8
		public static LocalizedString ErrorInvalidMailboxProvisioningAttributes(int maximumAllowedAttributes)
		{
			return new LocalizedString("ErrorInvalidMailboxProvisioningAttributes", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				maximumAllowedAttributes
			});
		}

		// Token: 0x17002DBD RID: 11709
		// (get) Token: 0x06007D23 RID: 32035 RVA: 0x0019FBEC File Offset: 0x0019DDEC
		public static LocalizedString ServerRoleSCOM
		{
			get
			{
				return new LocalizedString("ServerRoleSCOM", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D24 RID: 32036 RVA: 0x0019FC0C File Offset: 0x0019DE0C
		public static LocalizedString ExceptionUnsupportedOperatorForProperty(string propertyName, string operatorName)
		{
			return new LocalizedString("ExceptionUnsupportedOperatorForProperty", "Ex1F6696", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				operatorName
			});
		}

		// Token: 0x17002DBE RID: 11710
		// (get) Token: 0x06007D25 RID: 32037 RVA: 0x0019FC3F File Offset: 0x0019DE3F
		public static LocalizedString JournalItemsMC
		{
			get
			{
				return new LocalizedString("JournalItemsMC", "Ex184082", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D26 RID: 32038 RVA: 0x0019FC60 File Offset: 0x0019DE60
		public static LocalizedString ErrorTargetPartitionSctMissing(string oldTenant, string newPartition, string sct)
		{
			return new LocalizedString("ErrorTargetPartitionSctMissing", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				oldTenant,
				newPartition,
				sct
			});
		}

		// Token: 0x06007D27 RID: 32039 RVA: 0x0019FC98 File Offset: 0x0019DE98
		public static LocalizedString TenantTransportSettingsNotFoundException(string orgId)
		{
			return new LocalizedString("TenantTransportSettingsNotFoundException", "Ex9564DC", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				orgId
			});
		}

		// Token: 0x06007D28 RID: 32040 RVA: 0x0019FCC8 File Offset: 0x0019DEC8
		public static LocalizedString ErrorNonUniqueLegacyDN(string legacyDN)
		{
			return new LocalizedString("ErrorNonUniqueLegacyDN", "ExEB64EC", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				legacyDN
			});
		}

		// Token: 0x06007D29 RID: 32041 RVA: 0x0019FCF8 File Offset: 0x0019DEF8
		public static LocalizedString ErrorAccountPartitionCantBeLocalAndSecondaryAtTheSameTime(string id)
		{
			return new LocalizedString("ErrorAccountPartitionCantBeLocalAndSecondaryAtTheSameTime", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002DBF RID: 11711
		// (get) Token: 0x06007D2A RID: 32042 RVA: 0x0019FD27 File Offset: 0x0019DF27
		public static LocalizedString ErrorEmptySearchProperty
		{
			get
			{
				return new LocalizedString("ErrorEmptySearchProperty", "ExD5949E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D2B RID: 32043 RVA: 0x0019FD48 File Offset: 0x0019DF48
		public static LocalizedString ExceptionADTopologyTimeoutCall(string server, string error)
		{
			return new LocalizedString("ExceptionADTopologyTimeoutCall", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				error
			});
		}

		// Token: 0x06007D2C RID: 32044 RVA: 0x0019FD7C File Offset: 0x0019DF7C
		public static LocalizedString ExceptionUnsupportedOperator(string operatorName, string filterType)
		{
			return new LocalizedString("ExceptionUnsupportedOperator", "Ex00F5CF", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				operatorName,
				filterType
			});
		}

		// Token: 0x06007D2D RID: 32045 RVA: 0x0019FDB0 File Offset: 0x0019DFB0
		public static LocalizedString NoMatchingTenantInTargetPartition(string oldTenant, string newPartition, string guid)
		{
			return new LocalizedString("NoMatchingTenantInTargetPartition", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				oldTenant,
				newPartition,
				guid
			});
		}

		// Token: 0x06007D2E RID: 32046 RVA: 0x0019FDE8 File Offset: 0x0019DFE8
		public static LocalizedString RootMustBeEmpty(ScopeRestrictionType scopeType)
		{
			return new LocalizedString("RootMustBeEmpty", "ExFE601A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scopeType
			});
		}

		// Token: 0x17002DC0 RID: 11712
		// (get) Token: 0x06007D2F RID: 32047 RVA: 0x0019FE1C File Offset: 0x0019E01C
		public static LocalizedString OutboundConnectorIncorrectTransportRuleScopedParameters
		{
			get
			{
				return new LocalizedString("OutboundConnectorIncorrectTransportRuleScopedParameters", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DC1 RID: 11713
		// (get) Token: 0x06007D30 RID: 32048 RVA: 0x0019FE3A File Offset: 0x0019E03A
		public static LocalizedString TeamMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("TeamMailboxRecipientTypeDetails", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DC2 RID: 11714
		// (get) Token: 0x06007D31 RID: 32049 RVA: 0x0019FE58 File Offset: 0x0019E058
		public static LocalizedString CustomRoleDescription_MyMobileInformation
		{
			get
			{
				return new LocalizedString("CustomRoleDescription_MyMobileInformation", "ExC0AC83", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DC3 RID: 11715
		// (get) Token: 0x06007D32 RID: 32050 RVA: 0x0019FE76 File Offset: 0x0019E076
		public static LocalizedString ArchiveStateHostedPending
		{
			get
			{
				return new LocalizedString("ArchiveStateHostedPending", "Ex2CD23D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DC4 RID: 11716
		// (get) Token: 0x06007D33 RID: 32051 RVA: 0x0019FE94 File Offset: 0x0019E094
		public static LocalizedString DPCantChangeName
		{
			get
			{
				return new LocalizedString("DPCantChangeName", "Ex0147D6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DC5 RID: 11717
		// (get) Token: 0x06007D34 RID: 32052 RVA: 0x0019FEB2 File Offset: 0x0019E0B2
		public static LocalizedString OrganizationCapabilityUMDataStorage
		{
			get
			{
				return new LocalizedString("OrganizationCapabilityUMDataStorage", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DC6 RID: 11718
		// (get) Token: 0x06007D35 RID: 32053 RVA: 0x0019FED0 File Offset: 0x0019E0D0
		public static LocalizedString TlsAuthLevelWithRequireTlsDisabled
		{
			get
			{
				return new LocalizedString("TlsAuthLevelWithRequireTlsDisabled", "Ex7A63EC", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DC7 RID: 11719
		// (get) Token: 0x06007D36 RID: 32054 RVA: 0x0019FEEE File Offset: 0x0019E0EE
		public static LocalizedString UndefinedRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("UndefinedRecipientTypeDetails", "Ex8AA2FD", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DC8 RID: 11720
		// (get) Token: 0x06007D37 RID: 32055 RVA: 0x0019FF0C File Offset: 0x0019E10C
		public static LocalizedString Upgrade
		{
			get
			{
				return new LocalizedString("Upgrade", "ExF45929", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DC9 RID: 11721
		// (get) Token: 0x06007D38 RID: 32056 RVA: 0x0019FF2A File Offset: 0x0019E12A
		public static LocalizedString Global
		{
			get
			{
				return new LocalizedString("Global", "ExEA06B3", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DCA RID: 11722
		// (get) Token: 0x06007D39 RID: 32057 RVA: 0x0019FF48 File Offset: 0x0019E148
		public static LocalizedString DeleteMessage
		{
			get
			{
				return new LocalizedString("DeleteMessage", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DCB RID: 11723
		// (get) Token: 0x06007D3A RID: 32058 RVA: 0x0019FF66 File Offset: 0x0019E166
		public static LocalizedString LdapDelete
		{
			get
			{
				return new LocalizedString("LdapDelete", "Ex11D92C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DCC RID: 11724
		// (get) Token: 0x06007D3B RID: 32059 RVA: 0x0019FF84 File Offset: 0x0019E184
		public static LocalizedString EsnLangHungarian
		{
			get
			{
				return new LocalizedString("EsnLangHungarian", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D3C RID: 32060 RVA: 0x0019FFA4 File Offset: 0x0019E1A4
		public static LocalizedString ExceptionValueNotPresent(string propertyName, string objectName)
		{
			return new LocalizedString("ExceptionValueNotPresent", "ExBBAFE5", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				objectName
			});
		}

		// Token: 0x17002DCD RID: 11725
		// (get) Token: 0x06007D3D RID: 32061 RVA: 0x0019FFD7 File Offset: 0x0019E1D7
		public static LocalizedString ErrorAddressAutoCopy
		{
			get
			{
				return new LocalizedString("ErrorAddressAutoCopy", "ExE5CBF5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DCE RID: 11726
		// (get) Token: 0x06007D3E RID: 32062 RVA: 0x0019FFF5 File Offset: 0x0019E1F5
		public static LocalizedString EsnLangLatvian
		{
			get
			{
				return new LocalizedString("EsnLangLatvian", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D3F RID: 32063 RVA: 0x001A0014 File Offset: 0x0019E214
		public static LocalizedString ErrorMinAdminVersionOutOfSync(int minAdminVersion, ExchangeObjectVersion currentVersion, int correctMinAdminVersion)
		{
			return new LocalizedString("ErrorMinAdminVersionOutOfSync", "Ex02F649", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				minAdminVersion,
				currentVersion,
				correctMinAdminVersion
			});
		}

		// Token: 0x17002DCF RID: 11727
		// (get) Token: 0x06007D40 RID: 32064 RVA: 0x001A0055 File Offset: 0x0019E255
		public static LocalizedString CanRunDefaultUpdateState_NotLocal
		{
			get
			{
				return new LocalizedString("CanRunDefaultUpdateState_NotLocal", "Ex16C492", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DD0 RID: 11728
		// (get) Token: 0x06007D41 RID: 32065 RVA: 0x001A0073 File Offset: 0x0019E273
		public static LocalizedString Department
		{
			get
			{
				return new LocalizedString("Department", "Ex584E88", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D42 RID: 32066 RVA: 0x001A0094 File Offset: 0x0019E294
		public static LocalizedString MasteredOnPremiseCapabilityUndefinedNotTenant(string capability)
		{
			return new LocalizedString("MasteredOnPremiseCapabilityUndefinedNotTenant", "ExB23FD3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				capability
			});
		}

		// Token: 0x06007D43 RID: 32067 RVA: 0x001A00C4 File Offset: 0x0019E2C4
		public static LocalizedString ExceptionUnsupportedTextFilterOption(string option)
		{
			return new LocalizedString("ExceptionUnsupportedTextFilterOption", "Ex748AA9", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				option
			});
		}

		// Token: 0x17002DD1 RID: 11729
		// (get) Token: 0x06007D44 RID: 32068 RVA: 0x001A00F3 File Offset: 0x0019E2F3
		public static LocalizedString SpamFilteringActionJmf
		{
			get
			{
				return new LocalizedString("SpamFilteringActionJmf", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D45 RID: 32069 RVA: 0x001A0114 File Offset: 0x0019E314
		public static LocalizedString ErrorServiceAccountThrottlingPolicy(string scope)
		{
			return new LocalizedString("ErrorServiceAccountThrottlingPolicy", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				scope
			});
		}

		// Token: 0x17002DD2 RID: 11730
		// (get) Token: 0x06007D46 RID: 32070 RVA: 0x001A0143 File Offset: 0x0019E343
		public static LocalizedString ErrorDDLOperationsError
		{
			get
			{
				return new LocalizedString("ErrorDDLOperationsError", "ExC1558E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DD3 RID: 11731
		// (get) Token: 0x06007D47 RID: 32071 RVA: 0x001A0161 File Offset: 0x0019E361
		public static LocalizedString ErrorSharedConfigurationCannotBeEnabled
		{
			get
			{
				return new LocalizedString("ErrorSharedConfigurationCannotBeEnabled", "Ex37918C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DD4 RID: 11732
		// (get) Token: 0x06007D48 RID: 32072 RVA: 0x001A017F File Offset: 0x0019E37F
		public static LocalizedString ErrorMailTipCultureNotSpecified
		{
			get
			{
				return new LocalizedString("ErrorMailTipCultureNotSpecified", "Ex918659", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D49 RID: 32073 RVA: 0x001A01A0 File Offset: 0x0019E3A0
		public static LocalizedString ErrorDLAsBothAcceptedAndRejected(string dl)
		{
			return new LocalizedString("ErrorDLAsBothAcceptedAndRejected", "Ex3CB891", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				dl
			});
		}

		// Token: 0x06007D4A RID: 32074 RVA: 0x001A01D0 File Offset: 0x0019E3D0
		public static LocalizedString ErrorSettingOverrideSyntax(string componentName, string sectionName, string parameters, string error)
		{
			return new LocalizedString("ErrorSettingOverrideSyntax", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				componentName,
				sectionName,
				parameters,
				error
			});
		}

		// Token: 0x06007D4B RID: 32075 RVA: 0x001A020C File Offset: 0x0019E40C
		public static LocalizedString PermanentMservError(string message)
		{
			return new LocalizedString("PermanentMservError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17002DD5 RID: 11733
		// (get) Token: 0x06007D4C RID: 32076 RVA: 0x001A023B File Offset: 0x0019E43B
		public static LocalizedString LdapModify
		{
			get
			{
				return new LocalizedString("LdapModify", "Ex983C72", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DD6 RID: 11734
		// (get) Token: 0x06007D4D RID: 32077 RVA: 0x001A0259 File Offset: 0x0019E459
		public static LocalizedString DataMoveReplicationConstraintSecondDatacenter
		{
			get
			{
				return new LocalizedString("DataMoveReplicationConstraintSecondDatacenter", "Ex20CA37", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DD7 RID: 11735
		// (get) Token: 0x06007D4E RID: 32078 RVA: 0x001A0277 File Offset: 0x0019E477
		public static LocalizedString CapabilityResourceMailbox
		{
			get
			{
				return new LocalizedString("CapabilityResourceMailbox", "Ex75E71B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D4F RID: 32079 RVA: 0x001A0298 File Offset: 0x0019E498
		public static LocalizedString InvalidCapabilityOnMailboxPlan(string currentCapability, string skuCapabilities)
		{
			return new LocalizedString("InvalidCapabilityOnMailboxPlan", "ExC0F98E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				currentCapability,
				skuCapabilities
			});
		}

		// Token: 0x17002DD8 RID: 11736
		// (get) Token: 0x06007D50 RID: 32080 RVA: 0x001A02CB File Offset: 0x0019E4CB
		public static LocalizedString Second
		{
			get
			{
				return new LocalizedString("Second", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DD9 RID: 11737
		// (get) Token: 0x06007D51 RID: 32081 RVA: 0x001A02E9 File Offset: 0x0019E4E9
		public static LocalizedString InboundConnectorInvalidRestrictDomainsToCertificate
		{
			get
			{
				return new LocalizedString("InboundConnectorInvalidRestrictDomainsToCertificate", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DDA RID: 11738
		// (get) Token: 0x06007D52 RID: 32082 RVA: 0x001A0307 File Offset: 0x0019E507
		public static LocalizedString GroupNamingPolicyCustomAttribute15
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute15", "ExE36B6F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DDB RID: 11739
		// (get) Token: 0x06007D53 RID: 32083 RVA: 0x001A0325 File Offset: 0x0019E525
		public static LocalizedString SendAuthMechanismNone
		{
			get
			{
				return new LocalizedString("SendAuthMechanismNone", "ExD84EDF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DDC RID: 11740
		// (get) Token: 0x06007D54 RID: 32084 RVA: 0x001A0343 File Offset: 0x0019E543
		public static LocalizedString ServicesContainerNotFound
		{
			get
			{
				return new LocalizedString("ServicesContainerNotFound", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DDD RID: 11741
		// (get) Token: 0x06007D55 RID: 32085 RVA: 0x001A0361 File Offset: 0x0019E561
		public static LocalizedString MissingDefaultOutboundCallingLineId
		{
			get
			{
				return new LocalizedString("MissingDefaultOutboundCallingLineId", "ExCE5E1F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DDE RID: 11742
		// (get) Token: 0x06007D56 RID: 32086 RVA: 0x001A037F File Offset: 0x0019E57F
		public static LocalizedString GroupTypeFlagsDomainLocal
		{
			get
			{
				return new LocalizedString("GroupTypeFlagsDomainLocal", "Ex986159", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D57 RID: 32087 RVA: 0x001A03A0 File Offset: 0x0019E5A0
		public static LocalizedString SharedConfigurationVersionNotSupported(string tinyTenant, string version)
		{
			return new LocalizedString("SharedConfigurationVersionNotSupported", "ExB4A8CC", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				tinyTenant,
				version
			});
		}

		// Token: 0x17002DDF RID: 11743
		// (get) Token: 0x06007D58 RID: 32088 RVA: 0x001A03D3 File Offset: 0x0019E5D3
		public static LocalizedString ErrorCannotAggregateAndLinkMailbox
		{
			get
			{
				return new LocalizedString("ErrorCannotAggregateAndLinkMailbox", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DE0 RID: 11744
		// (get) Token: 0x06007D59 RID: 32089 RVA: 0x001A03F1 File Offset: 0x0019E5F1
		public static LocalizedString SyncCommands
		{
			get
			{
				return new LocalizedString("SyncCommands", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D5A RID: 32090 RVA: 0x001A0410 File Offset: 0x0019E610
		public static LocalizedString OwaAdOrphanFound(string id)
		{
			return new LocalizedString("OwaAdOrphanFound", "ExCFCCE5", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x06007D5B RID: 32091 RVA: 0x001A0440 File Offset: 0x0019E640
		public static LocalizedString ExceptionRUSServerDown(string server)
		{
			return new LocalizedString("ExceptionRUSServerDown", "ExF5CFA4", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17002DE1 RID: 11745
		// (get) Token: 0x06007D5C RID: 32092 RVA: 0x001A046F File Offset: 0x0019E66F
		public static LocalizedString PreferredInternetCodePageEsc2022Jp
		{
			get
			{
				return new LocalizedString("PreferredInternetCodePageEsc2022Jp", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D5D RID: 32093 RVA: 0x001A0490 File Offset: 0x0019E690
		public static LocalizedString DefaultRoutingGroupNotFoundException(string rgName)
		{
			return new LocalizedString("DefaultRoutingGroupNotFoundException", "ExEC1E62", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				rgName
			});
		}

		// Token: 0x17002DE2 RID: 11746
		// (get) Token: 0x06007D5E RID: 32094 RVA: 0x001A04BF File Offset: 0x0019E6BF
		public static LocalizedString DirectoryBasedEdgeBlockModeOff
		{
			get
			{
				return new LocalizedString("DirectoryBasedEdgeBlockModeOff", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DE3 RID: 11747
		// (get) Token: 0x06007D5F RID: 32095 RVA: 0x001A04DD File Offset: 0x0019E6DD
		public static LocalizedString InvalidSourceAddressSetting
		{
			get
			{
				return new LocalizedString("InvalidSourceAddressSetting", "Ex51BAE5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DE4 RID: 11748
		// (get) Token: 0x06007D60 RID: 32096 RVA: 0x001A04FB File Offset: 0x0019E6FB
		public static LocalizedString ElcContentSettingsDescription
		{
			get
			{
				return new LocalizedString("ElcContentSettingsDescription", "Ex0E091C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DE5 RID: 11749
		// (get) Token: 0x06007D61 RID: 32097 RVA: 0x001A0519 File Offset: 0x0019E719
		public static LocalizedString ServerRoleUnifiedMessaging
		{
			get
			{
				return new LocalizedString("ServerRoleUnifiedMessaging", "Ex8F8134", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DE6 RID: 11750
		// (get) Token: 0x06007D62 RID: 32098 RVA: 0x001A0537 File Offset: 0x0019E737
		public static LocalizedString DataMoveReplicationConstraintCIAllCopies
		{
			get
			{
				return new LocalizedString("DataMoveReplicationConstraintCIAllCopies", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DE7 RID: 11751
		// (get) Token: 0x06007D63 RID: 32099 RVA: 0x001A0555 File Offset: 0x0019E755
		public static LocalizedString MailTipsAccessLevelLimited
		{
			get
			{
				return new LocalizedString("MailTipsAccessLevelLimited", "Ex919781", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DE8 RID: 11752
		// (get) Token: 0x06007D64 RID: 32100 RVA: 0x001A0573 File Offset: 0x0019E773
		public static LocalizedString SecondaryMailboxRelationType
		{
			get
			{
				return new LocalizedString("SecondaryMailboxRelationType", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DE9 RID: 11753
		// (get) Token: 0x06007D65 RID: 32101 RVA: 0x001A0591 File Offset: 0x0019E791
		public static LocalizedString Ocs
		{
			get
			{
				return new LocalizedString("Ocs", "Ex7E09BF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DEA RID: 11754
		// (get) Token: 0x06007D66 RID: 32102 RVA: 0x001A05AF File Offset: 0x0019E7AF
		public static LocalizedString IndustryOther
		{
			get
			{
				return new LocalizedString("IndustryOther", "Ex357EFD", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DEB RID: 11755
		// (get) Token: 0x06007D67 RID: 32103 RVA: 0x001A05CD File Offset: 0x0019E7CD
		public static LocalizedString ErrorMimeMessageIncludingUuEncodedAttachment
		{
			get
			{
				return new LocalizedString("ErrorMimeMessageIncludingUuEncodedAttachment", "Ex1ABC61", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D68 RID: 32104 RVA: 0x001A05EC File Offset: 0x0019E7EC
		public static LocalizedString ExceptionCopyChangesForIncompatibleTypes(Type type1, Type type2)
		{
			return new LocalizedString("ExceptionCopyChangesForIncompatibleTypes", "ExCF37A6", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				type1,
				type2
			});
		}

		// Token: 0x17002DEC RID: 11756
		// (get) Token: 0x06007D69 RID: 32105 RVA: 0x001A061F File Offset: 0x0019E81F
		public static LocalizedString ServerRoleDHCP
		{
			get
			{
				return new LocalizedString("ServerRoleDHCP", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DED RID: 11757
		// (get) Token: 0x06007D6A RID: 32106 RVA: 0x001A063D File Offset: 0x0019E83D
		public static LocalizedString GroupNamingPolicyCustomAttribute5
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute5", "ExB92AC6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D6B RID: 32107 RVA: 0x001A065C File Offset: 0x0019E85C
		public static LocalizedString ErrorSettingOverrideInvalidParameterName(string componentName, string sectionName, string parameterName, string availableParameterNames)
		{
			return new LocalizedString("ErrorSettingOverrideInvalidParameterName", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				componentName,
				sectionName,
				parameterName,
				availableParameterNames
			});
		}

		// Token: 0x17002DEE RID: 11758
		// (get) Token: 0x06007D6C RID: 32108 RVA: 0x001A0697 File Offset: 0x0019E897
		public static LocalizedString EnableNotificationEmail
		{
			get
			{
				return new LocalizedString("EnableNotificationEmail", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DEF RID: 11759
		// (get) Token: 0x06007D6D RID: 32109 RVA: 0x001A06B5 File Offset: 0x0019E8B5
		public static LocalizedString GroupNamingPolicyCountryCode
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCountryCode", "ExE7BFF9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DF0 RID: 11760
		// (get) Token: 0x06007D6E RID: 32110 RVA: 0x001A06D3 File Offset: 0x0019E8D3
		public static LocalizedString MailboxMoveStatusCompleted
		{
			get
			{
				return new LocalizedString("MailboxMoveStatusCompleted", "Ex3E6503", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D6F RID: 32111 RVA: 0x001A06F4 File Offset: 0x0019E8F4
		public static LocalizedString RangePropertyResponseDoesNotContainRangeInformation(string str)
		{
			return new LocalizedString("RangePropertyResponseDoesNotContainRangeInformation", "Ex93BF72", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				str
			});
		}

		// Token: 0x06007D70 RID: 32112 RVA: 0x001A0724 File Offset: 0x0019E924
		public static LocalizedString ExceptionSeverNotInPartition(string serverName, string partitionName)
		{
			return new LocalizedString("ExceptionSeverNotInPartition", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				serverName,
				partitionName
			});
		}

		// Token: 0x17002DF1 RID: 11761
		// (get) Token: 0x06007D71 RID: 32113 RVA: 0x001A0757 File Offset: 0x0019E957
		public static LocalizedString IndustryCommunications
		{
			get
			{
				return new LocalizedString("IndustryCommunications", "Ex460042", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DF2 RID: 11762
		// (get) Token: 0x06007D72 RID: 32114 RVA: 0x001A0775 File Offset: 0x0019E975
		public static LocalizedString LdapFilterErrorNoValidComparison
		{
			get
			{
				return new LocalizedString("LdapFilterErrorNoValidComparison", "Ex6AEFD1", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D73 RID: 32115 RVA: 0x001A0794 File Offset: 0x0019E994
		public static LocalizedString CannotResolvePartitionFqdnFromAccountForestDnError(string dn)
		{
			return new LocalizedString("CannotResolvePartitionFqdnFromAccountForestDnError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				dn
			});
		}

		// Token: 0x17002DF3 RID: 11763
		// (get) Token: 0x06007D74 RID: 32116 RVA: 0x001A07C3 File Offset: 0x0019E9C3
		public static LocalizedString RssSubscriptions
		{
			get
			{
				return new LocalizedString("RssSubscriptions", "Ex870DD4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D75 RID: 32117 RVA: 0x001A07E4 File Offset: 0x0019E9E4
		public static LocalizedString ErrorSettingOverrideUnexpected(string errorType)
		{
			return new LocalizedString("ErrorSettingOverrideUnexpected", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				errorType
			});
		}

		// Token: 0x06007D76 RID: 32118 RVA: 0x001A0814 File Offset: 0x0019EA14
		public static LocalizedString OrgWideDelegatingWriteScopeMustBeTheSameAsRoleImplicitWriteScope(RecipientWriteScopeType scopeType)
		{
			return new LocalizedString("OrgWideDelegatingWriteScopeMustBeTheSameAsRoleImplicitWriteScope", "Ex3D27BC", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scopeType
			});
		}

		// Token: 0x06007D77 RID: 32119 RVA: 0x001A0848 File Offset: 0x0019EA48
		public static LocalizedString InvalidPhrase(string listName, int length)
		{
			return new LocalizedString("InvalidPhrase", "Ex54C462", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				listName,
				length
			});
		}

		// Token: 0x17002DF4 RID: 11764
		// (get) Token: 0x06007D78 RID: 32120 RVA: 0x001A0880 File Offset: 0x0019EA80
		public static LocalizedString EsnLangThai
		{
			get
			{
				return new LocalizedString("EsnLangThai", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D79 RID: 32121 RVA: 0x001A08A0 File Offset: 0x0019EAA0
		public static LocalizedString ErrorRealmNotFound(string realm)
		{
			return new LocalizedString("ErrorRealmNotFound", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				realm
			});
		}

		// Token: 0x17002DF5 RID: 11765
		// (get) Token: 0x06007D7A RID: 32122 RVA: 0x001A08CF File Offset: 0x0019EACF
		public static LocalizedString ErrorDDLFilterMissing
		{
			get
			{
				return new LocalizedString("ErrorDDLFilterMissing", "Ex4EFFD9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DF6 RID: 11766
		// (get) Token: 0x06007D7B RID: 32123 RVA: 0x001A08ED File Offset: 0x0019EAED
		public static LocalizedString ExtendedProtectionNonTlsTerminatingProxyScenarioRequireTls
		{
			get
			{
				return new LocalizedString("ExtendedProtectionNonTlsTerminatingProxyScenarioRequireTls", "ExA5D67C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D7C RID: 32124 RVA: 0x001A090C File Offset: 0x0019EB0C
		public static LocalizedString ErrorLogonFailuresBeforePINReset(int logonFailuresBeforePINReset, string maxLogonAttempts)
		{
			return new LocalizedString("ErrorLogonFailuresBeforePINReset", "ExDA4E3D", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				logonFailuresBeforePINReset,
				maxLogonAttempts
			});
		}

		// Token: 0x06007D7D RID: 32125 RVA: 0x001A0944 File Offset: 0x0019EB44
		public static LocalizedString TenantNotFoundInMservError(string tenant)
		{
			return new LocalizedString("TenantNotFoundInMservError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				tenant
			});
		}

		// Token: 0x17002DF7 RID: 11767
		// (get) Token: 0x06007D7E RID: 32126 RVA: 0x001A0973 File Offset: 0x0019EB73
		public static LocalizedString NoResetOrAssignedMvp
		{
			get
			{
				return new LocalizedString("NoResetOrAssignedMvp", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DF8 RID: 11768
		// (get) Token: 0x06007D7F RID: 32127 RVA: 0x001A0991 File Offset: 0x0019EB91
		public static LocalizedString MountDialOverrideBestEffort
		{
			get
			{
				return new LocalizedString("MountDialOverrideBestEffort", "ExD53E80", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D80 RID: 32128 RVA: 0x001A09B0 File Offset: 0x0019EBB0
		public static LocalizedString ErrorIsServerInMaintenanceMode(string fqdn)
		{
			return new LocalizedString("ErrorIsServerInMaintenanceMode", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				fqdn
			});
		}

		// Token: 0x06007D81 RID: 32129 RVA: 0x001A09E0 File Offset: 0x0019EBE0
		public static LocalizedString ExceptionADOperationFailed(string server, string errorMessage)
		{
			return new LocalizedString("ExceptionADOperationFailed", "Ex6AE46B", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				errorMessage
			});
		}

		// Token: 0x17002DF9 RID: 11769
		// (get) Token: 0x06007D82 RID: 32130 RVA: 0x001A0A13 File Offset: 0x0019EC13
		public static LocalizedString NoComputers
		{
			get
			{
				return new LocalizedString("NoComputers", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D83 RID: 32131 RVA: 0x001A0A34 File Offset: 0x0019EC34
		public static LocalizedString ConfigWriteScopeNotLessThanBecauseOfDelegationFlags(string leftScopeType, string rightScopeType)
		{
			return new LocalizedString("ConfigWriteScopeNotLessThanBecauseOfDelegationFlags", "ExAE2E51", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				leftScopeType,
				rightScopeType
			});
		}

		// Token: 0x17002DFA RID: 11770
		// (get) Token: 0x06007D84 RID: 32132 RVA: 0x001A0A67 File Offset: 0x0019EC67
		public static LocalizedString RegistryContentTypeException
		{
			get
			{
				return new LocalizedString("RegistryContentTypeException", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DFB RID: 11771
		// (get) Token: 0x06007D85 RID: 32133 RVA: 0x001A0A85 File Offset: 0x0019EC85
		public static LocalizedString DataMoveReplicationConstraintAllDatacenters
		{
			get
			{
				return new LocalizedString("DataMoveReplicationConstraintAllDatacenters", "Ex3525F0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DFC RID: 11772
		// (get) Token: 0x06007D86 RID: 32134 RVA: 0x001A0AA3 File Offset: 0x0019ECA3
		public static LocalizedString ExceptionObjectNotFound
		{
			get
			{
				return new LocalizedString("ExceptionObjectNotFound", "Ex7571B9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DFD RID: 11773
		// (get) Token: 0x06007D87 RID: 32135 RVA: 0x001A0AC1 File Offset: 0x0019ECC1
		public static LocalizedString DomainStateCustomProvision
		{
			get
			{
				return new LocalizedString("DomainStateCustomProvision", "ExACD43A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DFE RID: 11774
		// (get) Token: 0x06007D88 RID: 32136 RVA: 0x001A0ADF File Offset: 0x0019ECDF
		public static LocalizedString SKUCapabilityBPOSMidSize
		{
			get
			{
				return new LocalizedString("SKUCapabilityBPOSMidSize", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002DFF RID: 11775
		// (get) Token: 0x06007D89 RID: 32137 RVA: 0x001A0AFD File Offset: 0x0019ECFD
		public static LocalizedString LdapFilterErrorUnsupportedOperand
		{
			get
			{
				return new LocalizedString("LdapFilterErrorUnsupportedOperand", "Ex3BC457", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E00 RID: 11776
		// (get) Token: 0x06007D8A RID: 32138 RVA: 0x001A0B1B File Offset: 0x0019ED1B
		public static LocalizedString DirectoryBasedEdgeBlockModeDefault
		{
			get
			{
				return new LocalizedString("DirectoryBasedEdgeBlockModeDefault", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D8B RID: 32139 RVA: 0x001A0B3C File Offset: 0x0019ED3C
		public static LocalizedString SwapShouldNotChangeValues(string oldValue, byte oldRid, string newValue, byte newRid)
		{
			return new LocalizedString("SwapShouldNotChangeValues", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				oldValue,
				oldRid,
				newValue,
				newRid
			});
		}

		// Token: 0x06007D8C RID: 32140 RVA: 0x001A0B84 File Offset: 0x0019ED84
		public static LocalizedString ConfigurationSettingsHistorySummary(string name, int count)
		{
			return new LocalizedString("ConfigurationSettingsHistorySummary", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				name,
				count
			});
		}

		// Token: 0x17002E01 RID: 11777
		// (get) Token: 0x06007D8D RID: 32141 RVA: 0x001A0BBC File Offset: 0x0019EDBC
		public static LocalizedString ErrorWrongTypeParameter
		{
			get
			{
				return new LocalizedString("ErrorWrongTypeParameter", "ExA51C26", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E02 RID: 11778
		// (get) Token: 0x06007D8E RID: 32142 RVA: 0x001A0BDA File Offset: 0x0019EDDA
		public static LocalizedString EsnLangCatalan
		{
			get
			{
				return new LocalizedString("EsnLangCatalan", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E03 RID: 11779
		// (get) Token: 0x06007D8F RID: 32143 RVA: 0x001A0BF8 File Offset: 0x0019EDF8
		public static LocalizedString InvalidSndProtocolLogSizeConfiguration
		{
			get
			{
				return new LocalizedString("InvalidSndProtocolLogSizeConfiguration", "Ex2E47CB", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E04 RID: 11780
		// (get) Token: 0x06007D90 RID: 32144 RVA: 0x001A0C16 File Offset: 0x0019EE16
		public static LocalizedString GroupNamingPolicyCustomAttribute13
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyCustomAttribute13", "Ex448D4C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E05 RID: 11781
		// (get) Token: 0x06007D91 RID: 32145 RVA: 0x001A0C34 File Offset: 0x0019EE34
		public static LocalizedString ErrorThrottlingPolicyGlobalAndOrganizationScope
		{
			get
			{
				return new LocalizedString("ErrorThrottlingPolicyGlobalAndOrganizationScope", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E06 RID: 11782
		// (get) Token: 0x06007D92 RID: 32146 RVA: 0x001A0C52 File Offset: 0x0019EE52
		public static LocalizedString SMTPAddress
		{
			get
			{
				return new LocalizedString("SMTPAddress", "Ex8A0496", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D93 RID: 32147 RVA: 0x001A0C70 File Offset: 0x0019EE70
		public static LocalizedString ExceptionUnsupportedPropertyValue(string propertyName, object value)
		{
			return new LocalizedString("ExceptionUnsupportedPropertyValue", "Ex22FD94", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				value
			});
		}

		// Token: 0x17002E07 RID: 11783
		// (get) Token: 0x06007D94 RID: 32148 RVA: 0x001A0CA3 File Offset: 0x0019EEA3
		public static LocalizedString EsnLangPolish
		{
			get
			{
				return new LocalizedString("EsnLangPolish", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E08 RID: 11784
		// (get) Token: 0x06007D95 RID: 32149 RVA: 0x001A0CC1 File Offset: 0x0019EEC1
		public static LocalizedString CanEnableLocalCopyState_DatabaseEnabled
		{
			get
			{
				return new LocalizedString("CanEnableLocalCopyState_DatabaseEnabled", "ExC31189", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E09 RID: 11785
		// (get) Token: 0x06007D96 RID: 32150 RVA: 0x001A0CDF File Offset: 0x0019EEDF
		public static LocalizedString EsnLangRomanian
		{
			get
			{
				return new LocalizedString("EsnLangRomanian", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E0A RID: 11786
		// (get) Token: 0x06007D97 RID: 32151 RVA: 0x001A0CFD File Offset: 0x0019EEFD
		public static LocalizedString ExternalManagedGroupTypeDetails
		{
			get
			{
				return new LocalizedString("ExternalManagedGroupTypeDetails", "Ex5EA363", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E0B RID: 11787
		// (get) Token: 0x06007D98 RID: 32152 RVA: 0x001A0D1B File Offset: 0x0019EF1B
		public static LocalizedString DatabaseMasterTypeDag
		{
			get
			{
				return new LocalizedString("DatabaseMasterTypeDag", "Ex763399", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D99 RID: 32153 RVA: 0x001A0D3C File Offset: 0x0019EF3C
		public static LocalizedString ExceptionNetLogonOperation(string netLogonOperation, string domain)
		{
			return new LocalizedString("ExceptionNetLogonOperation", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				netLogonOperation,
				domain
			});
		}

		// Token: 0x17002E0C RID: 11788
		// (get) Token: 0x06007D9A RID: 32154 RVA: 0x001A0D6F File Offset: 0x0019EF6F
		public static LocalizedString GroupNamingPolicyExtensionCustomAttribute3
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyExtensionCustomAttribute3", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007D9B RID: 32155 RVA: 0x001A0D90 File Offset: 0x0019EF90
		public static LocalizedString ExceptionADTopologyNoSuchDomain(string domain)
		{
			return new LocalizedString("ExceptionADTopologyNoSuchDomain", "ExCD40EE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x06007D9C RID: 32156 RVA: 0x001A0DC0 File Offset: 0x0019EFC0
		public static LocalizedString InvalidNumberOfCapabilitiesOnMailboxPlan(string skuCapabilities)
		{
			return new LocalizedString("InvalidNumberOfCapabilitiesOnMailboxPlan", "Ex3A8F0D", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				skuCapabilities
			});
		}

		// Token: 0x17002E0D RID: 11789
		// (get) Token: 0x06007D9D RID: 32157 RVA: 0x001A0DEF File Offset: 0x0019EFEF
		public static LocalizedString ExchangeConfigurationContainerNotFoundException
		{
			get
			{
				return new LocalizedString("ExchangeConfigurationContainerNotFoundException", "Ex63E670", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E0E RID: 11790
		// (get) Token: 0x06007D9E RID: 32158 RVA: 0x001A0E0D File Offset: 0x0019F00D
		public static LocalizedString EsnLangUrdu
		{
			get
			{
				return new LocalizedString("EsnLangUrdu", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E0F RID: 11791
		// (get) Token: 0x06007D9F RID: 32159 RVA: 0x001A0E2B File Offset: 0x0019F02B
		public static LocalizedString MservAndMbxExclusive
		{
			get
			{
				return new LocalizedString("MservAndMbxExclusive", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E10 RID: 11792
		// (get) Token: 0x06007DA0 RID: 32160 RVA: 0x001A0E49 File Offset: 0x0019F049
		public static LocalizedString FirstLast
		{
			get
			{
				return new LocalizedString("FirstLast", "Ex28BC2F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E11 RID: 11793
		// (get) Token: 0x06007DA1 RID: 32161 RVA: 0x001A0E67 File Offset: 0x0019F067
		public static LocalizedString EsnLangBulgarian
		{
			get
			{
				return new LocalizedString("EsnLangBulgarian", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E12 RID: 11794
		// (get) Token: 0x06007DA2 RID: 32162 RVA: 0x001A0E85 File Offset: 0x0019F085
		public static LocalizedString MailEnabledUniversalSecurityGroupRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailEnabledUniversalSecurityGroupRecipientTypeDetails", "Ex060A04", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DA3 RID: 32163 RVA: 0x001A0EA4 File Offset: 0x0019F0A4
		public static LocalizedString ExceptionReadOnlyBecauseTooNew(ExchangeObjectVersion objectVersion, ExchangeObjectVersion currentVersion)
		{
			return new LocalizedString("ExceptionReadOnlyBecauseTooNew", "ExF977EA", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				objectVersion,
				currentVersion
			});
		}

		// Token: 0x17002E13 RID: 11795
		// (get) Token: 0x06007DA4 RID: 32164 RVA: 0x001A0ED7 File Offset: 0x0019F0D7
		public static LocalizedString ErrorTimeoutReadingSystemAddressListMemberCount
		{
			get
			{
				return new LocalizedString("ErrorTimeoutReadingSystemAddressListMemberCount", "Ex4388F6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E14 RID: 11796
		// (get) Token: 0x06007DA5 RID: 32165 RVA: 0x001A0EF5 File Offset: 0x0019F0F5
		public static LocalizedString FaxServerURINoValue
		{
			get
			{
				return new LocalizedString("FaxServerURINoValue", "Ex8F5C5F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E15 RID: 11797
		// (get) Token: 0x06007DA6 RID: 32166 RVA: 0x001A0F13 File Offset: 0x0019F113
		public static LocalizedString ErrorDefaultThrottlingPolicyNotFound
		{
			get
			{
				return new LocalizedString("ErrorDefaultThrottlingPolicyNotFound", "ExF7CACF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DA7 RID: 32167 RVA: 0x001A0F34 File Offset: 0x0019F134
		public static LocalizedString CannotGetSiteInfo(string error)
		{
			return new LocalizedString("CannotGetSiteInfo", "Ex25AC16", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06007DA8 RID: 32168 RVA: 0x001A0F64 File Offset: 0x0019F164
		public static LocalizedString UnableToDeserializeXMLError(string errorStr)
		{
			return new LocalizedString("UnableToDeserializeXMLError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				errorStr
			});
		}

		// Token: 0x06007DA9 RID: 32169 RVA: 0x001A0F94 File Offset: 0x0019F194
		public static LocalizedString ConfigurationSettingsRestrictionMissingProperty(string name, string propertyName)
		{
			return new LocalizedString("ConfigurationSettingsRestrictionMissingProperty", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				name,
				propertyName
			});
		}

		// Token: 0x17002E16 RID: 11798
		// (get) Token: 0x06007DAA RID: 32170 RVA: 0x001A0FC7 File Offset: 0x0019F1C7
		public static LocalizedString ErrorRecipientContainerCanNotNull
		{
			get
			{
				return new LocalizedString("ErrorRecipientContainerCanNotNull", "ExB42B18", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E17 RID: 11799
		// (get) Token: 0x06007DAB RID: 32171 RVA: 0x001A0FE5 File Offset: 0x0019F1E5
		public static LocalizedString MoveToArchive
		{
			get
			{
				return new LocalizedString("MoveToArchive", "ExC6E83C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DAC RID: 32172 RVA: 0x001A1004 File Offset: 0x0019F204
		public static LocalizedString ConfigurationSettingsInvalidScopeFilter(string error)
		{
			return new LocalizedString("ConfigurationSettingsInvalidScopeFilter", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17002E18 RID: 11800
		// (get) Token: 0x06007DAD RID: 32173 RVA: 0x001A1033 File Offset: 0x0019F233
		public static LocalizedString ModifySubjectValueNotSet
		{
			get
			{
				return new LocalizedString("ModifySubjectValueNotSet", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DAE RID: 32174 RVA: 0x001A1054 File Offset: 0x0019F254
		public static LocalizedString OwaMetabasePathIsInvalid(string id, string path)
		{
			return new LocalizedString("OwaMetabasePathIsInvalid", "Ex1E041E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id,
				path
			});
		}

		// Token: 0x17002E19 RID: 11801
		// (get) Token: 0x06007DAF RID: 32175 RVA: 0x001A1087 File Offset: 0x0019F287
		public static LocalizedString NotLocalMaiboxException
		{
			get
			{
				return new LocalizedString("NotLocalMaiboxException", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DB0 RID: 32176 RVA: 0x001A10A8 File Offset: 0x0019F2A8
		public static LocalizedString InvalidDNStringFormat(string str)
		{
			return new LocalizedString("InvalidDNStringFormat", "ExDDC2EA", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				str
			});
		}

		// Token: 0x17002E1A RID: 11802
		// (get) Token: 0x06007DB1 RID: 32177 RVA: 0x001A10D7 File Offset: 0x0019F2D7
		public static LocalizedString RecipientReadScope
		{
			get
			{
				return new LocalizedString("RecipientReadScope", "ExD041A7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DB2 RID: 32178 RVA: 0x001A10F8 File Offset: 0x0019F2F8
		public static LocalizedString ExceptionADTopologyNoServersForPartition(string partition)
		{
			return new LocalizedString("ExceptionADTopologyNoServersForPartition", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				partition
			});
		}

		// Token: 0x06007DB3 RID: 32179 RVA: 0x001A1128 File Offset: 0x0019F328
		public static LocalizedString TenantAlreadyExistsInMserv(Guid tenantGuid, int existingPartnerId, int localSitePartnerId)
		{
			return new LocalizedString("TenantAlreadyExistsInMserv", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				tenantGuid,
				existingPartnerId,
				localSitePartnerId
			});
		}

		// Token: 0x17002E1B RID: 11803
		// (get) Token: 0x06007DB4 RID: 32180 RVA: 0x001A116E File Offset: 0x0019F36E
		public static LocalizedString Organizational
		{
			get
			{
				return new LocalizedString("Organizational", "Ex752504", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DB5 RID: 32181 RVA: 0x001A118C File Offset: 0x0019F38C
		public static LocalizedString ExceptionUnsupportedDefaultValueFilter(string propertyName, string operatorName, string value)
		{
			return new LocalizedString("ExceptionUnsupportedDefaultValueFilter", "Ex512393", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				operatorName,
				value
			});
		}

		// Token: 0x17002E1C RID: 11804
		// (get) Token: 0x06007DB6 RID: 32182 RVA: 0x001A11C3 File Offset: 0x0019F3C3
		public static LocalizedString SystemAttendantMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("SystemAttendantMailboxRecipientTypeDetails", "Ex3BEE54", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DB7 RID: 32183 RVA: 0x001A11E4 File Offset: 0x0019F3E4
		public static LocalizedString InvalidResourceThresholdBetweenClassifications(string thresholdName, string classification1, string classification2, int value1, int value2)
		{
			return new LocalizedString("InvalidResourceThresholdBetweenClassifications", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				thresholdName,
				classification1,
				classification2,
				value1,
				value2
			});
		}

		// Token: 0x17002E1D RID: 11805
		// (get) Token: 0x06007DB8 RID: 32184 RVA: 0x001A122E File Offset: 0x0019F42E
		public static LocalizedString OrganizationCapabilityOABGen
		{
			get
			{
				return new LocalizedString("OrganizationCapabilityOABGen", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E1E RID: 11806
		// (get) Token: 0x06007DB9 RID: 32185 RVA: 0x001A124C File Offset: 0x0019F44C
		public static LocalizedString StarOutToDialPlanEnabled
		{
			get
			{
				return new LocalizedString("StarOutToDialPlanEnabled", "ExA00A9E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E1F RID: 11807
		// (get) Token: 0x06007DBA RID: 32186 RVA: 0x001A126A File Offset: 0x0019F46A
		public static LocalizedString AuthenticationCredentialNotSet
		{
			get
			{
				return new LocalizedString("AuthenticationCredentialNotSet", "ExA19CDE", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E20 RID: 11808
		// (get) Token: 0x06007DBB RID: 32187 RVA: 0x001A1288 File Offset: 0x0019F488
		public static LocalizedString NotifyOutboundSpamRecipientsRequired
		{
			get
			{
				return new LocalizedString("NotifyOutboundSpamRecipientsRequired", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E21 RID: 11809
		// (get) Token: 0x06007DBC RID: 32188 RVA: 0x001A12A6 File Offset: 0x0019F4A6
		public static LocalizedString JunkEmail
		{
			get
			{
				return new LocalizedString("JunkEmail", "Ex426E42", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E22 RID: 11810
		// (get) Token: 0x06007DBD RID: 32189 RVA: 0x001A12C4 File Offset: 0x0019F4C4
		public static LocalizedString LdapFilterErrorValueOnlySpaces
		{
			get
			{
				return new LocalizedString("LdapFilterErrorValueOnlySpaces", "Ex8168A0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E23 RID: 11811
		// (get) Token: 0x06007DBE RID: 32190 RVA: 0x001A12E2 File Offset: 0x0019F4E2
		public static LocalizedString SipName
		{
			get
			{
				return new LocalizedString("SipName", "Ex04214A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E24 RID: 11812
		// (get) Token: 0x06007DBF RID: 32191 RVA: 0x001A1300 File Offset: 0x0019F500
		public static LocalizedString EsnLangMalayalam
		{
			get
			{
				return new LocalizedString("EsnLangMalayalam", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E25 RID: 11813
		// (get) Token: 0x06007DC0 RID: 32192 RVA: 0x001A131E File Offset: 0x0019F51E
		public static LocalizedString SpamFilteringActionModifySubject
		{
			get
			{
				return new LocalizedString("SpamFilteringActionModifySubject", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E26 RID: 11814
		// (get) Token: 0x06007DC1 RID: 32193 RVA: 0x001A133C File Offset: 0x0019F53C
		public static LocalizedString XHeaderValueNotSet
		{
			get
			{
				return new LocalizedString("XHeaderValueNotSet", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E27 RID: 11815
		// (get) Token: 0x06007DC2 RID: 32194 RVA: 0x001A135A File Offset: 0x0019F55A
		public static LocalizedString DeletedItems
		{
			get
			{
				return new LocalizedString("DeletedItems", "ExE37A57", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DC3 RID: 32195 RVA: 0x001A1378 File Offset: 0x0019F578
		public static LocalizedString ExceptionInvalidAddressFormat(string address)
		{
			return new LocalizedString("ExceptionInvalidAddressFormat", "Ex903F52", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x06007DC4 RID: 32196 RVA: 0x001A13A8 File Offset: 0x0019F5A8
		public static LocalizedString ExceptionMostDerivedOnBase(string objectName)
		{
			return new LocalizedString("ExceptionMostDerivedOnBase", "Ex7C5148", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				objectName
			});
		}

		// Token: 0x06007DC5 RID: 32197 RVA: 0x001A13D8 File Offset: 0x0019F5D8
		public static LocalizedString ExceptionOwaCannotSetPropertyOnE12VirtualDirectoryToNull(string property)
		{
			return new LocalizedString("ExceptionOwaCannotSetPropertyOnE12VirtualDirectoryToNull", "Ex934429", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x17002E28 RID: 11816
		// (get) Token: 0x06007DC6 RID: 32198 RVA: 0x001A1407 File Offset: 0x0019F607
		public static LocalizedString OrganizationCapabilityUMGrammarReady
		{
			get
			{
				return new LocalizedString("OrganizationCapabilityUMGrammarReady", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E29 RID: 11817
		// (get) Token: 0x06007DC7 RID: 32199 RVA: 0x001A1425 File Offset: 0x0019F625
		public static LocalizedString LastFirst
		{
			get
			{
				return new LocalizedString("LastFirst", "Ex9B48A8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DC8 RID: 32200 RVA: 0x001A1444 File Offset: 0x0019F644
		public static LocalizedString ErrorCopySystemFolderPathNotEqualCopyLogFolderPath(NonRootLocalLongFullPath cpySysPath, NonRootLocalLongFullPath cpyLogPath)
		{
			return new LocalizedString("ErrorCopySystemFolderPathNotEqualCopyLogFolderPath", "Ex608DB3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				cpySysPath,
				cpyLogPath
			});
		}

		// Token: 0x06007DC9 RID: 32201 RVA: 0x001A1478 File Offset: 0x0019F678
		public static LocalizedString ConfigurationSettingsUnsupportedVersion(string version)
		{
			return new LocalizedString("ConfigurationSettingsUnsupportedVersion", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x17002E2A RID: 11818
		// (get) Token: 0x06007DCA RID: 32202 RVA: 0x001A14A7 File Offset: 0x0019F6A7
		public static LocalizedString SendAuthMechanismExchangeServer
		{
			get
			{
				return new LocalizedString("SendAuthMechanismExchangeServer", "Ex8D4B00", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E2B RID: 11819
		// (get) Token: 0x06007DCB RID: 32203 RVA: 0x001A14C5 File Offset: 0x0019F6C5
		public static LocalizedString RemoteTeamMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("RemoteTeamMailboxRecipientTypeDetails", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E2C RID: 11820
		// (get) Token: 0x06007DCC RID: 32204 RVA: 0x001A14E3 File Offset: 0x0019F6E3
		public static LocalizedString OutOfBudgets
		{
			get
			{
				return new LocalizedString("OutOfBudgets", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E2D RID: 11821
		// (get) Token: 0x06007DCD RID: 32205 RVA: 0x001A1501 File Offset: 0x0019F701
		public static LocalizedString Off
		{
			get
			{
				return new LocalizedString("Off", "Ex7DB229", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DCE RID: 32206 RVA: 0x001A1520 File Offset: 0x0019F720
		public static LocalizedString ErrorInvalidOpathFilter(string filter)
		{
			return new LocalizedString("ErrorInvalidOpathFilter", "Ex7CFE2C", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				filter
			});
		}

		// Token: 0x17002E2E RID: 11822
		// (get) Token: 0x06007DCF RID: 32207 RVA: 0x001A154F File Offset: 0x0019F74F
		public static LocalizedString GroupTypeFlagsSecurityEnabled
		{
			get
			{
				return new LocalizedString("GroupTypeFlagsSecurityEnabled", "ExAE6448", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E2F RID: 11823
		// (get) Token: 0x06007DD0 RID: 32208 RVA: 0x001A156D File Offset: 0x0019F76D
		public static LocalizedString InvalidCookieException
		{
			get
			{
				return new LocalizedString("InvalidCookieException", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DD1 RID: 32209 RVA: 0x001A158C File Offset: 0x0019F78C
		public static LocalizedString WrongAssigneeTypeForPolicyOrPartnerApplication(string roleAssignment)
		{
			return new LocalizedString("WrongAssigneeTypeForPolicyOrPartnerApplication", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				roleAssignment
			});
		}

		// Token: 0x06007DD2 RID: 32210 RVA: 0x001A15BC File Offset: 0x0019F7BC
		public static LocalizedString WrongScopeForCurrentPartition(string scopeDN, string partitionFqdn)
		{
			return new LocalizedString("WrongScopeForCurrentPartition", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				scopeDN,
				partitionFqdn
			});
		}

		// Token: 0x17002E30 RID: 11824
		// (get) Token: 0x06007DD3 RID: 32211 RVA: 0x001A15EF File Offset: 0x0019F7EF
		public static LocalizedString UserLanguageChoice
		{
			get
			{
				return new LocalizedString("UserLanguageChoice", "Ex91975E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E31 RID: 11825
		// (get) Token: 0x06007DD4 RID: 32212 RVA: 0x001A160D File Offset: 0x0019F80D
		public static LocalizedString SpamFilteringTestActionBccMessage
		{
			get
			{
				return new LocalizedString("SpamFilteringTestActionBccMessage", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DD5 RID: 32213 RVA: 0x001A162C File Offset: 0x0019F82C
		public static LocalizedString ErrorInvalidLegacyRdnPrefix(string prefix)
		{
			return new LocalizedString("ErrorInvalidLegacyRdnPrefix", "ExCF04C9", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				prefix
			});
		}

		// Token: 0x17002E32 RID: 11826
		// (get) Token: 0x06007DD6 RID: 32214 RVA: 0x001A165B File Offset: 0x0019F85B
		public static LocalizedString DelayCacheFull
		{
			get
			{
				return new LocalizedString("DelayCacheFull", "Ex0EBFE8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E33 RID: 11827
		// (get) Token: 0x06007DD7 RID: 32215 RVA: 0x001A1679 File Offset: 0x0019F879
		public static LocalizedString ErrorAutoCopyMessageFormat
		{
			get
			{
				return new LocalizedString("ErrorAutoCopyMessageFormat", "Ex55AEF3", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DD8 RID: 32216 RVA: 0x001A1698 File Offset: 0x0019F898
		public static LocalizedString InvalidIdFormat(string str)
		{
			return new LocalizedString("InvalidIdFormat", "Ex0BB379", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				str
			});
		}

		// Token: 0x17002E34 RID: 11828
		// (get) Token: 0x06007DD9 RID: 32217 RVA: 0x001A16C7 File Offset: 0x0019F8C7
		public static LocalizedString Reserved3
		{
			get
			{
				return new LocalizedString("Reserved3", "Ex79ABC9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E35 RID: 11829
		// (get) Token: 0x06007DDA RID: 32218 RVA: 0x001A16E5 File Offset: 0x0019F8E5
		public static LocalizedString HtmlOnly
		{
			get
			{
				return new LocalizedString("HtmlOnly", "Ex43F6E0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DDB RID: 32219 RVA: 0x001A1704 File Offset: 0x0019F904
		public static LocalizedString LocalServerNotFound(string fqdn)
		{
			return new LocalizedString("LocalServerNotFound", "Ex28CE91", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				fqdn
			});
		}

		// Token: 0x17002E36 RID: 11830
		// (get) Token: 0x06007DDC RID: 32220 RVA: 0x001A1733 File Offset: 0x0019F933
		public static LocalizedString DefaultFolder
		{
			get
			{
				return new LocalizedString("DefaultFolder", "Ex578F5D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DDD RID: 32221 RVA: 0x001A1754 File Offset: 0x0019F954
		public static LocalizedString ErrorResultsAreNonUnique(string filter)
		{
			return new LocalizedString("ErrorResultsAreNonUnique", "ExD53968", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				filter
			});
		}

		// Token: 0x17002E37 RID: 11831
		// (get) Token: 0x06007DDE RID: 32222 RVA: 0x001A1783 File Offset: 0x0019F983
		public static LocalizedString PublicFolderMailboxRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("PublicFolderMailboxRecipientTypeDetails", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DDF RID: 32223 RVA: 0x001A17A4 File Offset: 0x0019F9A4
		public static LocalizedString OrganizationMailboxNotFound(string id)
		{
			return new LocalizedString("OrganizationMailboxNotFound", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002E38 RID: 11832
		// (get) Token: 0x06007DE0 RID: 32224 RVA: 0x001A17D3 File Offset: 0x0019F9D3
		public static LocalizedString Mp3
		{
			get
			{
				return new LocalizedString("Mp3", "Ex574040", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DE1 RID: 32225 RVA: 0x001A17F4 File Offset: 0x0019F9F4
		public static LocalizedString ExceptionUnsupportedPropertyValueType(string propertyName, Type valueType, Type supportedType)
		{
			return new LocalizedString("ExceptionUnsupportedPropertyValueType", "ExCF8596", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				valueType,
				supportedType
			});
		}

		// Token: 0x17002E39 RID: 11833
		// (get) Token: 0x06007DE2 RID: 32226 RVA: 0x001A182B File Offset: 0x0019FA2B
		public static LocalizedString FederatedOrganizationIdNotEnabled
		{
			get
			{
				return new LocalizedString("FederatedOrganizationIdNotEnabled", "Ex325103", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DE3 RID: 32227 RVA: 0x001A184C File Offset: 0x0019FA4C
		public static LocalizedString ExceptionInvalidBitwiseComparison(string propertyName, string operatorName)
		{
			return new LocalizedString("ExceptionInvalidBitwiseComparison", "ExFE6432", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				operatorName
			});
		}

		// Token: 0x17002E3A RID: 11834
		// (get) Token: 0x06007DE4 RID: 32228 RVA: 0x001A187F File Offset: 0x0019FA7F
		public static LocalizedString EsnLangVietnamese
		{
			get
			{
				return new LocalizedString("EsnLangVietnamese", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E3B RID: 11835
		// (get) Token: 0x06007DE5 RID: 32229 RVA: 0x001A189D File Offset: 0x0019FA9D
		public static LocalizedString AccessGranted
		{
			get
			{
				return new LocalizedString("AccessGranted", "ExAFD833", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E3C RID: 11836
		// (get) Token: 0x06007DE6 RID: 32230 RVA: 0x001A18BB File Offset: 0x0019FABB
		public static LocalizedString MailboxUserRecipientType
		{
			get
			{
				return new LocalizedString("MailboxUserRecipientType", "ExC81C49", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DE7 RID: 32231 RVA: 0x001A18DC File Offset: 0x0019FADC
		public static LocalizedString CannotFindTemplateUser(string dn)
		{
			return new LocalizedString("CannotFindTemplateUser", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				dn
			});
		}

		// Token: 0x06007DE8 RID: 32232 RVA: 0x001A190C File Offset: 0x0019FB0C
		public static LocalizedString ErrorNoSuitableGC(string server, string forest)
		{
			return new LocalizedString("ErrorNoSuitableGC", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				forest
			});
		}

		// Token: 0x17002E3D RID: 11837
		// (get) Token: 0x06007DE9 RID: 32233 RVA: 0x001A193F File Offset: 0x0019FB3F
		public static LocalizedString ExceptionNoSchemaContainerObject
		{
			get
			{
				return new LocalizedString("ExceptionNoSchemaContainerObject", "Ex125C4F", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E3E RID: 11838
		// (get) Token: 0x06007DEA RID: 32234 RVA: 0x001A195D File Offset: 0x0019FB5D
		public static LocalizedString TargetDeliveryDomainCannotBeStar
		{
			get
			{
				return new LocalizedString("TargetDeliveryDomainCannotBeStar", "ExD5728C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E3F RID: 11839
		// (get) Token: 0x06007DEB RID: 32235 RVA: 0x001A197B File Offset: 0x0019FB7B
		public static LocalizedString ErrorAuthMetadataCannotResolveServiceName
		{
			get
			{
				return new LocalizedString("ErrorAuthMetadataCannotResolveServiceName", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E40 RID: 11840
		// (get) Token: 0x06007DEC RID: 32236 RVA: 0x001A1999 File Offset: 0x0019FB99
		public static LocalizedString ByteEncoderTypeUseBase64
		{
			get
			{
				return new LocalizedString("ByteEncoderTypeUseBase64", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DED RID: 32237 RVA: 0x001A19B8 File Offset: 0x0019FBB8
		public static LocalizedString ExceptionServerUnavailable(string serverName)
		{
			return new LocalizedString("ExceptionServerUnavailable", "Ex99F94E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06007DEE RID: 32238 RVA: 0x001A19E8 File Offset: 0x0019FBE8
		public static LocalizedString CannotGetDomainFromDN(string dn)
		{
			return new LocalizedString("CannotGetDomainFromDN", "ExA3F6DB", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				dn
			});
		}

		// Token: 0x06007DEF RID: 32239 RVA: 0x001A1A18 File Offset: 0x0019FC18
		public static LocalizedString FederatedUser_Violation(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("FederatedUser_Violation", "ExD9DDB9", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x06007DF0 RID: 32240 RVA: 0x001A1A54 File Offset: 0x0019FC54
		public static LocalizedString BPOS_Feature_UsageLocation_Violation(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("BPOS_Feature_UsageLocation_Violation", "Ex721F28", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x17002E41 RID: 11841
		// (get) Token: 0x06007DF1 RID: 32241 RVA: 0x001A1A8F File Offset: 0x0019FC8F
		public static LocalizedString BackSyncDataSourceReplicationErrorMessage
		{
			get
			{
				return new LocalizedString("BackSyncDataSourceReplicationErrorMessage", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DF2 RID: 32242 RVA: 0x001A1AB0 File Offset: 0x0019FCB0
		public static LocalizedString InvalidMaxOutboundConnectionConfiguration(string value1, string value2)
		{
			return new LocalizedString("InvalidMaxOutboundConnectionConfiguration", "Ex4AAF8A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				value1,
				value2
			});
		}

		// Token: 0x17002E42 RID: 11842
		// (get) Token: 0x06007DF3 RID: 32243 RVA: 0x001A1AE3 File Offset: 0x0019FCE3
		public static LocalizedString EsnLangHebrew
		{
			get
			{
				return new LocalizedString("EsnLangHebrew", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DF4 RID: 32244 RVA: 0x001A1B04 File Offset: 0x0019FD04
		public static LocalizedString ExceptionADVlvSizeLimitExceeded(string server, string message)
		{
			return new LocalizedString("ExceptionADVlvSizeLimitExceeded", "Ex66F6D8", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				message
			});
		}

		// Token: 0x06007DF5 RID: 32245 RVA: 0x001A1B38 File Offset: 0x0019FD38
		public static LocalizedString ExceptionWKGuidNeedsConfigSession(Guid wkguid)
		{
			return new LocalizedString("ExceptionWKGuidNeedsConfigSession", "Ex86A38D", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				wkguid
			});
		}

		// Token: 0x17002E43 RID: 11843
		// (get) Token: 0x06007DF6 RID: 32246 RVA: 0x001A1B6C File Offset: 0x0019FD6C
		public static LocalizedString WellKnownRecipientTypeAllRecipients
		{
			get
			{
				return new LocalizedString("WellKnownRecipientTypeAllRecipients", "Ex26FB9E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DF7 RID: 32247 RVA: 0x001A1B8C File Offset: 0x0019FD8C
		public static LocalizedString InvalidCookieServiceInstanceIdException(string serviceInstanceId)
		{
			return new LocalizedString("InvalidCookieServiceInstanceIdException", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				serviceInstanceId
			});
		}

		// Token: 0x06007DF8 RID: 32248 RVA: 0x001A1BBC File Offset: 0x0019FDBC
		public static LocalizedString InternalDsnLanguageNotSupported(string language)
		{
			return new LocalizedString("InternalDsnLanguageNotSupported", "Ex6520A9", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				language
			});
		}

		// Token: 0x17002E44 RID: 11844
		// (get) Token: 0x06007DF9 RID: 32249 RVA: 0x001A1BEB File Offset: 0x0019FDEB
		public static LocalizedString ExceptionCredentialsNotSupportedWithoutDC
		{
			get
			{
				return new LocalizedString("ExceptionCredentialsNotSupportedWithoutDC", "ExF75F28", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DFA RID: 32250 RVA: 0x001A1C0C File Offset: 0x0019FE0C
		public static LocalizedString ErrorAdditionalInfo(string info)
		{
			return new LocalizedString("ErrorAdditionalInfo", "Ex86C126", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				info
			});
		}

		// Token: 0x06007DFB RID: 32251 RVA: 0x001A1C3C File Offset: 0x0019FE3C
		public static LocalizedString ExceptionOverBudget(string policyPart, string policyValue, BudgetType budgetType, int backoffTime)
		{
			return new LocalizedString("ExceptionOverBudget", "ExFF035A", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				policyPart,
				policyValue,
				budgetType,
				backoffTime
			});
		}

		// Token: 0x06007DFC RID: 32252 RVA: 0x001A1C84 File Offset: 0x0019FE84
		public static LocalizedString ExceptionInvalidAccountName(string name)
		{
			return new LocalizedString("ExceptionInvalidAccountName", "ExA44AB9", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06007DFD RID: 32253 RVA: 0x001A1CB4 File Offset: 0x0019FEB4
		public static LocalizedString InvalidConfigScope(object scope)
		{
			return new LocalizedString("InvalidConfigScope", "ExC47354", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scope
			});
		}

		// Token: 0x17002E45 RID: 11845
		// (get) Token: 0x06007DFE RID: 32254 RVA: 0x001A1CE3 File Offset: 0x0019FEE3
		public static LocalizedString NoneMailboxRelationType
		{
			get
			{
				return new LocalizedString("NoneMailboxRelationType", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007DFF RID: 32255 RVA: 0x001A1D04 File Offset: 0x0019FF04
		public static LocalizedString ConfigScopeNotLessThan(string leftScopeType, string rightScopeType)
		{
			return new LocalizedString("ConfigScopeNotLessThan", "ExF1D274", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				leftScopeType,
				rightScopeType
			});
		}

		// Token: 0x17002E46 RID: 11846
		// (get) Token: 0x06007E00 RID: 32256 RVA: 0x001A1D37 File Offset: 0x0019FF37
		public static LocalizedString MailboxUserRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MailboxUserRecipientTypeDetails", "Ex24B5AF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E47 RID: 11847
		// (get) Token: 0x06007E01 RID: 32257 RVA: 0x001A1D55 File Offset: 0x0019FF55
		public static LocalizedString SpamFilteringActionDelete
		{
			get
			{
				return new LocalizedString("SpamFilteringActionDelete", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E02 RID: 32258 RVA: 0x001A1D74 File Offset: 0x0019FF74
		public static LocalizedString ExceptionInvalidScopeOperation(ConfigScopes configScope)
		{
			return new LocalizedString("ExceptionInvalidScopeOperation", "Ex27ECA1", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				configScope
			});
		}

		// Token: 0x17002E48 RID: 11848
		// (get) Token: 0x06007E03 RID: 32259 RVA: 0x001A1DA8 File Offset: 0x0019FFA8
		public static LocalizedString FederatedOrganizationIdNotFound
		{
			get
			{
				return new LocalizedString("FederatedOrganizationIdNotFound", "Ex7DF8A1", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E04 RID: 32260 RVA: 0x001A1DC8 File Offset: 0x0019FFC8
		public static LocalizedString ErrorNonUniqueNetId(string netIdString)
		{
			return new LocalizedString("ErrorNonUniqueNetId", "Ex53C1DB", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				netIdString
			});
		}

		// Token: 0x06007E05 RID: 32261 RVA: 0x001A1DF8 File Offset: 0x0019FFF8
		public static LocalizedString InvalidMailboxMoveFlags(object scope)
		{
			return new LocalizedString("InvalidMailboxMoveFlags", "ExFEA005", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scope
			});
		}

		// Token: 0x06007E06 RID: 32262 RVA: 0x001A1E28 File Offset: 0x001A0028
		public static LocalizedString ExInvalidTypeArgumentException(string paramName, Type type, Type expectedType)
		{
			return new LocalizedString("ExInvalidTypeArgumentException", "ExDF0EAE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				paramName,
				type,
				expectedType
			});
		}

		// Token: 0x06007E07 RID: 32263 RVA: 0x001A1E60 File Offset: 0x001A0060
		public static LocalizedString HostNameMatchesMultipleComputers(string hostName, string dn1, string dn2)
		{
			return new LocalizedString("HostNameMatchesMultipleComputers", "ExB03808", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				hostName,
				dn1,
				dn2
			});
		}

		// Token: 0x17002E49 RID: 11849
		// (get) Token: 0x06007E08 RID: 32264 RVA: 0x001A1E97 File Offset: 0x001A0097
		public static LocalizedString SKUCapabilityBPOSSArchive
		{
			get
			{
				return new LocalizedString("SKUCapabilityBPOSSArchive", "Ex11BC4E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E09 RID: 32265 RVA: 0x001A1EB8 File Offset: 0x001A00B8
		public static LocalizedString ErrorDefaultElcFolderTypeExists(string elcFolderName, string defaultFolderType)
		{
			return new LocalizedString("ErrorDefaultElcFolderTypeExists", "ExD6B3EB", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				elcFolderName,
				defaultFolderType
			});
		}

		// Token: 0x06007E0A RID: 32266 RVA: 0x001A1EEC File Offset: 0x001A00EC
		public static LocalizedString ExceptionADTopologyDomainNameIsNotFqdn(string name, string fqdn)
		{
			return new LocalizedString("ExceptionADTopologyDomainNameIsNotFqdn", "ExC147A9", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				name,
				fqdn
			});
		}

		// Token: 0x17002E4A RID: 11850
		// (get) Token: 0x06007E0B RID: 32267 RVA: 0x001A1F1F File Offset: 0x001A011F
		public static LocalizedString ReceiveAuthMechanismIntegrated
		{
			get
			{
				return new LocalizedString("ReceiveAuthMechanismIntegrated", "Ex010602", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E0C RID: 32268 RVA: 0x001A1F40 File Offset: 0x001A0140
		public static LocalizedString ExceptionDefaultScopeMustContainDomainDN(string scope)
		{
			return new LocalizedString("ExceptionDefaultScopeMustContainDomainDN", "Ex8F7242", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scope
			});
		}

		// Token: 0x17002E4B RID: 11851
		// (get) Token: 0x06007E0D RID: 32269 RVA: 0x001A1F6F File Offset: 0x001A016F
		public static LocalizedString NameLookupEnabled
		{
			get
			{
				return new LocalizedString("NameLookupEnabled", "Ex447BD1", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E0E RID: 32270 RVA: 0x001A1F90 File Offset: 0x001A0190
		public static LocalizedString ErrorInvalidFolderLinksAddition(string elcFolderName, string error)
		{
			return new LocalizedString("ErrorInvalidFolderLinksAddition", "Ex75D6EE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				elcFolderName,
				error
			});
		}

		// Token: 0x06007E0F RID: 32271 RVA: 0x001A1FC4 File Offset: 0x001A01C4
		public static LocalizedString ExceptionADTopologyHasNoServersInDomain(string domain)
		{
			return new LocalizedString("ExceptionADTopologyHasNoServersInDomain", "Ex4CEB8D", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x17002E4C RID: 11852
		// (get) Token: 0x06007E10 RID: 32272 RVA: 0x001A1FF3 File Offset: 0x001A01F3
		public static LocalizedString ForceFilter
		{
			get
			{
				return new LocalizedString("ForceFilter", "ExB23077", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E11 RID: 32273 RVA: 0x001A2014 File Offset: 0x001A0214
		public static LocalizedString ExceptionWriteOnceProperty(string propertyName)
		{
			return new LocalizedString("ExceptionWriteOnceProperty", "Ex3949D6", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x17002E4D RID: 11853
		// (get) Token: 0x06007E12 RID: 32274 RVA: 0x001A2043 File Offset: 0x001A0243
		public static LocalizedString OrganizationCapabilityOfficeMessageEncryption
		{
			get
			{
				return new LocalizedString("OrganizationCapabilityOfficeMessageEncryption", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E13 RID: 32275 RVA: 0x001A2064 File Offset: 0x001A0264
		public static LocalizedString ExceptionOrgScopeNotInUserScope(string orgScope, string userScope)
		{
			return new LocalizedString("ExceptionOrgScopeNotInUserScope", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				orgScope,
				userScope
			});
		}

		// Token: 0x17002E4E RID: 11854
		// (get) Token: 0x06007E14 RID: 32276 RVA: 0x001A2097 File Offset: 0x001A0297
		public static LocalizedString PreferredInternetCodePageIso2022Jp
		{
			get
			{
				return new LocalizedString("PreferredInternetCodePageIso2022Jp", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E4F RID: 11855
		// (get) Token: 0x06007E15 RID: 32277 RVA: 0x001A20B5 File Offset: 0x001A02B5
		public static LocalizedString AlternateServiceAccountCredentialIsInvalid
		{
			get
			{
				return new LocalizedString("AlternateServiceAccountCredentialIsInvalid", "Ex6A6106", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E50 RID: 11856
		// (get) Token: 0x06007E16 RID: 32278 RVA: 0x001A20D3 File Offset: 0x001A02D3
		public static LocalizedString EmailAgeFilterTwoWeeks
		{
			get
			{
				return new LocalizedString("EmailAgeFilterTwoWeeks", "Ex306743", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E51 RID: 11857
		// (get) Token: 0x06007E17 RID: 32279 RVA: 0x001A20F1 File Offset: 0x001A02F1
		public static LocalizedString DeviceOS
		{
			get
			{
				return new LocalizedString("DeviceOS", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E18 RID: 32280 RVA: 0x001A2110 File Offset: 0x001A0310
		public static LocalizedString ExceptionInvalidVlvSeekReference(string seekReference)
		{
			return new LocalizedString("ExceptionInvalidVlvSeekReference", "Ex0E58AB", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				seekReference
			});
		}

		// Token: 0x06007E19 RID: 32281 RVA: 0x001A2140 File Offset: 0x001A0340
		public static LocalizedString ExceptionADOperationFailedNotAMember(string server)
		{
			return new LocalizedString("ExceptionADOperationFailedNotAMember", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17002E52 RID: 11858
		// (get) Token: 0x06007E1A RID: 32282 RVA: 0x001A216F File Offset: 0x001A036F
		public static LocalizedString ErrorTenantRelocationsAllowedOnlyForRootOrg
		{
			get
			{
				return new LocalizedString("ErrorTenantRelocationsAllowedOnlyForRootOrg", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E53 RID: 11859
		// (get) Token: 0x06007E1B RID: 32283 RVA: 0x001A218D File Offset: 0x001A038D
		public static LocalizedString OrganizationCapabilityTenantUpgrade
		{
			get
			{
				return new LocalizedString("OrganizationCapabilityTenantUpgrade", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E1C RID: 32284 RVA: 0x001A21AC File Offset: 0x001A03AC
		public static LocalizedString ErrorIsServerSuitableMissingOperatingSystemResponse(string dcName)
		{
			return new LocalizedString("ErrorIsServerSuitableMissingOperatingSystemResponse", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				dcName
			});
		}

		// Token: 0x17002E54 RID: 11860
		// (get) Token: 0x06007E1D RID: 32285 RVA: 0x001A21DB File Offset: 0x001A03DB
		public static LocalizedString StarTlsDomainCapabilitiesNotAllowed
		{
			get
			{
				return new LocalizedString("StarTlsDomainCapabilitiesNotAllowed", "ExE9B082", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E55 RID: 11861
		// (get) Token: 0x06007E1E RID: 32286 RVA: 0x001A21F9 File Offset: 0x001A03F9
		public static LocalizedString GroupNamingPolicyExtensionCustomAttribute5
		{
			get
			{
				return new LocalizedString("GroupNamingPolicyExtensionCustomAttribute5", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E56 RID: 11862
		// (get) Token: 0x06007E1F RID: 32287 RVA: 0x001A2217 File Offset: 0x001A0417
		public static LocalizedString ErrorTimeoutWritingSystemAddressListCache
		{
			get
			{
				return new LocalizedString("ErrorTimeoutWritingSystemAddressListCache", "ExC0C4B0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E20 RID: 32288 RVA: 0x001A2238 File Offset: 0x001A0438
		public static LocalizedString ErrorPartnerApplicationNotFound(string applicationId)
		{
			return new LocalizedString("ErrorPartnerApplicationNotFound", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				applicationId
			});
		}

		// Token: 0x06007E21 RID: 32289 RVA: 0x001A2268 File Offset: 0x001A0468
		public static LocalizedString ExceptionInvalidCredentials(string server, string credential)
		{
			return new LocalizedString("ExceptionInvalidCredentials", "ExE5DB78", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				credential
			});
		}

		// Token: 0x06007E22 RID: 32290 RVA: 0x001A229C File Offset: 0x001A049C
		public static LocalizedString MicrosoftExchangeRecipientDisplayNameError(string displayName)
		{
			return new LocalizedString("MicrosoftExchangeRecipientDisplayNameError", "ExA2C1DB", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				displayName
			});
		}

		// Token: 0x06007E23 RID: 32291 RVA: 0x001A22CC File Offset: 0x001A04CC
		public static LocalizedString CannotGetChild(string message)
		{
			return new LocalizedString("CannotGetChild", "Ex9A591E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17002E57 RID: 11863
		// (get) Token: 0x06007E24 RID: 32292 RVA: 0x001A22FB File Offset: 0x001A04FB
		public static LocalizedString CannotGetLocalSite
		{
			get
			{
				return new LocalizedString("CannotGetLocalSite", "Ex890F37", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E58 RID: 11864
		// (get) Token: 0x06007E25 RID: 32293 RVA: 0x001A2319 File Offset: 0x001A0519
		public static LocalizedString DatabaseCopyAutoActivationPolicyUnrestricted
		{
			get
			{
				return new LocalizedString("DatabaseCopyAutoActivationPolicyUnrestricted", "Ex729064", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E59 RID: 11865
		// (get) Token: 0x06007E26 RID: 32294 RVA: 0x001A2337 File Offset: 0x001A0537
		public static LocalizedString PrivateComputersOnly
		{
			get
			{
				return new LocalizedString("PrivateComputersOnly", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E5A RID: 11866
		// (get) Token: 0x06007E27 RID: 32295 RVA: 0x001A2355 File Offset: 0x001A0555
		public static LocalizedString Always
		{
			get
			{
				return new LocalizedString("Always", "ExA361BF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E28 RID: 32296 RVA: 0x001A2374 File Offset: 0x001A0574
		public static LocalizedString FacebookEnabled_Error(string objectName, string cmdLet, string parameters, string capabilities)
		{
			return new LocalizedString("FacebookEnabled_Error", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				objectName,
				cmdLet,
				parameters,
				capabilities
			});
		}

		// Token: 0x17002E5B RID: 11867
		// (get) Token: 0x06007E29 RID: 32297 RVA: 0x001A23AF File Offset: 0x001A05AF
		public static LocalizedString WellKnownRecipientTypeMailUsers
		{
			get
			{
				return new LocalizedString("WellKnownRecipientTypeMailUsers", "ExA8268E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E2A RID: 32298 RVA: 0x001A23D0 File Offset: 0x001A05D0
		public static LocalizedString ExceptionADTopologyErrorWhenLookingForForest(int error, string fqdn, string message)
		{
			return new LocalizedString("ExceptionADTopologyErrorWhenLookingForForest", "ExA62B6D", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				error,
				fqdn,
				message
			});
		}

		// Token: 0x17002E5C RID: 11868
		// (get) Token: 0x06007E2B RID: 32299 RVA: 0x001A240C File Offset: 0x001A060C
		public static LocalizedString CannotSetZeroAsEapPriority
		{
			get
			{
				return new LocalizedString("CannotSetZeroAsEapPriority", "Ex6985D2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E2C RID: 32300 RVA: 0x001A242C File Offset: 0x001A062C
		public static LocalizedString ErrorMultiValuedPropertyTooLarge(string property, int count, int max)
		{
			return new LocalizedString("ErrorMultiValuedPropertyTooLarge", "ExC89A6F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				property,
				count,
				max
			});
		}

		// Token: 0x06007E2D RID: 32301 RVA: 0x001A2470 File Offset: 0x001A0670
		public static LocalizedString ErrorLinkedADObjectNotInFirstOrganization(string propertyName, string propertyValue, string objectId, string firstOrgId)
		{
			return new LocalizedString("ErrorLinkedADObjectNotInFirstOrganization", "Ex3F9F60", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				propertyValue,
				objectId,
				firstOrgId
			});
		}

		// Token: 0x17002E5D RID: 11869
		// (get) Token: 0x06007E2E RID: 32302 RVA: 0x001A24AB File Offset: 0x001A06AB
		public static LocalizedString RootZone
		{
			get
			{
				return new LocalizedString("RootZone", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E5E RID: 11870
		// (get) Token: 0x06007E2F RID: 32303 RVA: 0x001A24C9 File Offset: 0x001A06C9
		public static LocalizedString RenameNotAllowed
		{
			get
			{
				return new LocalizedString("RenameNotAllowed", "Ex742D23", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E5F RID: 11871
		// (get) Token: 0x06007E30 RID: 32304 RVA: 0x001A24E7 File Offset: 0x001A06E7
		public static LocalizedString Unknown
		{
			get
			{
				return new LocalizedString("Unknown", "Ex5D54E9", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E60 RID: 11872
		// (get) Token: 0x06007E31 RID: 32305 RVA: 0x001A2505 File Offset: 0x001A0705
		public static LocalizedString EsnLangItalian
		{
			get
			{
				return new LocalizedString("EsnLangItalian", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E61 RID: 11873
		// (get) Token: 0x06007E32 RID: 32306 RVA: 0x001A2523 File Offset: 0x001A0723
		public static LocalizedString ErrorDisplayNameInvalid
		{
			get
			{
				return new LocalizedString("ErrorDisplayNameInvalid", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E62 RID: 11874
		// (get) Token: 0x06007E33 RID: 32307 RVA: 0x001A2541 File Offset: 0x001A0741
		public static LocalizedString ConstraintViolationNotValidLegacyDN
		{
			get
			{
				return new LocalizedString("ConstraintViolationNotValidLegacyDN", "ExBE78E8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E63 RID: 11875
		// (get) Token: 0x06007E34 RID: 32308 RVA: 0x001A255F File Offset: 0x001A075F
		public static LocalizedString ReceiveExtendedProtectionPolicyRequire
		{
			get
			{
				return new LocalizedString("ReceiveExtendedProtectionPolicyRequire", "Ex4D9510", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E35 RID: 32309 RVA: 0x001A2580 File Offset: 0x001A0780
		public static LocalizedString ErrorExceededHosterResourceCountQuota(string policyId, string poType, int countQuota)
		{
			return new LocalizedString("ErrorExceededHosterResourceCountQuota", "Ex77A0D5", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				policyId,
				poType,
				countQuota
			});
		}

		// Token: 0x17002E64 RID: 11876
		// (get) Token: 0x06007E36 RID: 32310 RVA: 0x001A25BC File Offset: 0x001A07BC
		public static LocalizedString SpamFilteringOptionOff
		{
			get
			{
				return new LocalizedString("SpamFilteringOptionOff", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E37 RID: 32311 RVA: 0x001A25DC File Offset: 0x001A07DC
		public static LocalizedString ConstraintViolationValueNotSupportedLCID(string propertyName, int LCID)
		{
			return new LocalizedString("ConstraintViolationValueNotSupportedLCID", "Ex1F6DE9", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				LCID
			});
		}

		// Token: 0x06007E38 RID: 32312 RVA: 0x001A2614 File Offset: 0x001A0814
		public static LocalizedString RusUnableToPerformValidation(string s)
		{
			return new LocalizedString("RusUnableToPerformValidation", "Ex030A7B", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x17002E65 RID: 11877
		// (get) Token: 0x06007E39 RID: 32313 RVA: 0x001A2643 File Offset: 0x001A0843
		public static LocalizedString ExternallyManaged
		{
			get
			{
				return new LocalizedString("ExternallyManaged", "ExF9D9E6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E66 RID: 11878
		// (get) Token: 0x06007E3A RID: 32314 RVA: 0x001A2661 File Offset: 0x001A0861
		public static LocalizedString RequireTLSWithoutTLS
		{
			get
			{
				return new LocalizedString("RequireTLSWithoutTLS", "Ex22F161", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E3B RID: 32315 RVA: 0x001A2680 File Offset: 0x001A0880
		public static LocalizedString ErrorSecondaryAccountPartitionCantBeUsedForProvisioning(string id)
		{
			return new LocalizedString("ErrorSecondaryAccountPartitionCantBeUsedForProvisioning", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002E67 RID: 11879
		// (get) Token: 0x06007E3C RID: 32316 RVA: 0x001A26AF File Offset: 0x001A08AF
		public static LocalizedString ErrorCannotParseAuthMetadata
		{
			get
			{
				return new LocalizedString("ErrorCannotParseAuthMetadata", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E68 RID: 11880
		// (get) Token: 0x06007E3D RID: 32317 RVA: 0x001A26CD File Offset: 0x001A08CD
		public static LocalizedString ErrorInvalidActivationPreference
		{
			get
			{
				return new LocalizedString("ErrorInvalidActivationPreference", "Ex7CD088", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E69 RID: 11881
		// (get) Token: 0x06007E3E RID: 32318 RVA: 0x001A26EB File Offset: 0x001A08EB
		public static LocalizedString CapabilityFederatedUser
		{
			get
			{
				return new LocalizedString("CapabilityFederatedUser", "Ex7DF90D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E3F RID: 32319 RVA: 0x001A270C File Offset: 0x001A090C
		public static LocalizedString ErrorInvalidLegacyCommonName(string cn)
		{
			return new LocalizedString("ErrorInvalidLegacyCommonName", "Ex33411D", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				cn
			});
		}

		// Token: 0x17002E6A RID: 11882
		// (get) Token: 0x06007E40 RID: 32320 RVA: 0x001A273B File Offset: 0x001A093B
		public static LocalizedString EsnLangFilipino
		{
			get
			{
				return new LocalizedString("EsnLangFilipino", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E41 RID: 32321 RVA: 0x001A275C File Offset: 0x001A095C
		public static LocalizedString ExceptionExtendedRightNotFound(string displayName)
		{
			return new LocalizedString("ExceptionExtendedRightNotFound", "Ex176FFE", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				displayName
			});
		}

		// Token: 0x06007E42 RID: 32322 RVA: 0x001A278C File Offset: 0x001A098C
		public static LocalizedString FailedToWriteAlternateServiceAccountConfigToRegistry(string keyPath)
		{
			return new LocalizedString("FailedToWriteAlternateServiceAccountConfigToRegistry", "Ex7C1F04", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				keyPath
			});
		}

		// Token: 0x06007E43 RID: 32323 RVA: 0x001A27BC File Offset: 0x001A09BC
		public static LocalizedString ExceptionUnsupportedMatchOptionsForProperty(string propertyName, string optionsType)
		{
			return new LocalizedString("ExceptionUnsupportedMatchOptionsForProperty", "Ex14A0D0", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				propertyName,
				optionsType
			});
		}

		// Token: 0x06007E44 RID: 32324 RVA: 0x001A27F0 File Offset: 0x001A09F0
		public static LocalizedString ExceptionUnknownDirectoryAttribute(string srcObj, string dn)
		{
			return new LocalizedString("ExceptionUnknownDirectoryAttribute", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				srcObj,
				dn
			});
		}

		// Token: 0x17002E6B RID: 11883
		// (get) Token: 0x06007E45 RID: 32325 RVA: 0x001A2823 File Offset: 0x001A0A23
		public static LocalizedString OutboundConnectorUseMXRecordShouldBeFalseIfSmartHostsIsPresent
		{
			get
			{
				return new LocalizedString("OutboundConnectorUseMXRecordShouldBeFalseIfSmartHostsIsPresent", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E46 RID: 32326 RVA: 0x001A2844 File Offset: 0x001A0A44
		public static LocalizedString ErrorCannotAcquireAuthMetadata(string url, string error)
		{
			return new LocalizedString("ErrorCannotAcquireAuthMetadata", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				url,
				error
			});
		}

		// Token: 0x17002E6C RID: 11884
		// (get) Token: 0x06007E47 RID: 32327 RVA: 0x001A2877 File Offset: 0x001A0A77
		public static LocalizedString LdapFilterErrorBracketMismatch
		{
			get
			{
				return new LocalizedString("LdapFilterErrorBracketMismatch", "Ex886AD3", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E48 RID: 32328 RVA: 0x001A2898 File Offset: 0x001A0A98
		public static LocalizedString ExtensionNotUnique(string s, string dialPlan)
		{
			return new LocalizedString("ExtensionNotUnique", "ExC9C216", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				s,
				dialPlan
			});
		}

		// Token: 0x17002E6D RID: 11885
		// (get) Token: 0x06007E49 RID: 32329 RVA: 0x001A28CB File Offset: 0x001A0ACB
		public static LocalizedString SipResourceIdentifierRequiredNotAllowed
		{
			get
			{
				return new LocalizedString("SipResourceIdentifierRequiredNotAllowed", "ExC29F3D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E6E RID: 11886
		// (get) Token: 0x06007E4A RID: 32330 RVA: 0x001A28E9 File Offset: 0x001A0AE9
		public static LocalizedString XMSWLHeader
		{
			get
			{
				return new LocalizedString("XMSWLHeader", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E4B RID: 32331 RVA: 0x001A2908 File Offset: 0x001A0B08
		public static LocalizedString ExceptionADTopologyNoSuchSite(string siteName, string errorMessage)
		{
			return new LocalizedString("ExceptionADTopologyNoSuchSite", "Ex863AD3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				siteName,
				errorMessage
			});
		}

		// Token: 0x06007E4C RID: 32332 RVA: 0x001A293C File Offset: 0x001A0B3C
		public static LocalizedString ErrorRecipientTypeIsNotValidForDeliveryRestrictionOrModeration(string id, string type)
		{
			return new LocalizedString("ErrorRecipientTypeIsNotValidForDeliveryRestrictionOrModeration", "Ex159685", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id,
				type
			});
		}

		// Token: 0x17002E6F RID: 11887
		// (get) Token: 0x06007E4D RID: 32333 RVA: 0x001A296F File Offset: 0x001A0B6F
		public static LocalizedString ServerRoleCafe
		{
			get
			{
				return new LocalizedString("ServerRoleCafe", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E70 RID: 11888
		// (get) Token: 0x06007E4E RID: 32334 RVA: 0x001A298D File Offset: 0x001A0B8D
		public static LocalizedString DeleteAndRejectThreshold
		{
			get
			{
				return new LocalizedString("DeleteAndRejectThreshold", "ExF3F819", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E4F RID: 32335 RVA: 0x001A29AC File Offset: 0x001A0BAC
		public static LocalizedString ErrorTwoOrMoreUniqueRecipientTypes(string value)
		{
			return new LocalizedString("ErrorTwoOrMoreUniqueRecipientTypes", "Ex13C1DF", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17002E71 RID: 11889
		// (get) Token: 0x06007E50 RID: 32336 RVA: 0x001A29DB File Offset: 0x001A0BDB
		public static LocalizedString Policy
		{
			get
			{
				return new LocalizedString("Policy", "Ex37A429", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E72 RID: 11890
		// (get) Token: 0x06007E51 RID: 32337 RVA: 0x001A29F9 File Offset: 0x001A0BF9
		public static LocalizedString CanRunRestoreState_NotLocal
		{
			get
			{
				return new LocalizedString("CanRunRestoreState_NotLocal", "Ex85C03B", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E73 RID: 11891
		// (get) Token: 0x06007E52 RID: 32338 RVA: 0x001A2A17 File Offset: 0x001A0C17
		public static LocalizedString ElcAuditLogPathMissing
		{
			get
			{
				return new LocalizedString("ElcAuditLogPathMissing", "Ex9C7E49", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E74 RID: 11892
		// (get) Token: 0x06007E53 RID: 32339 RVA: 0x001A2A35 File Offset: 0x001A0C35
		public static LocalizedString ClientCertAuthIgnore
		{
			get
			{
				return new LocalizedString("ClientCertAuthIgnore", "ExB83ACA", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E75 RID: 11893
		// (get) Token: 0x06007E54 RID: 32340 RVA: 0x001A2A53 File Offset: 0x001A0C53
		public static LocalizedString Reserved2
		{
			get
			{
				return new LocalizedString("Reserved2", "Ex6752DF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E55 RID: 32341 RVA: 0x001A2A74 File Offset: 0x001A0C74
		public static LocalizedString ExceptionADTopologyErrorWhenLookingForLocalDomainTrustRelationships(int error, string message)
		{
			return new LocalizedString("ExceptionADTopologyErrorWhenLookingForLocalDomainTrustRelationships", "Ex788FD9", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				error,
				message
			});
		}

		// Token: 0x06007E56 RID: 32342 RVA: 0x001A2AAC File Offset: 0x001A0CAC
		public static LocalizedString ExternalEmailAddressInvalid(string message)
		{
			return new LocalizedString("ExternalEmailAddressInvalid", "Ex4300A1", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17002E76 RID: 11894
		// (get) Token: 0x06007E57 RID: 32343 RVA: 0x001A2ADB File Offset: 0x001A0CDB
		public static LocalizedString ConfigWriteScopes
		{
			get
			{
				return new LocalizedString("ConfigWriteScopes", "Ex64C531", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E77 RID: 11895
		// (get) Token: 0x06007E58 RID: 32344 RVA: 0x001A2AF9 File Offset: 0x001A0CF9
		public static LocalizedString DetailsTemplateCorrupted
		{
			get
			{
				return new LocalizedString("DetailsTemplateCorrupted", "ExEB07EF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E78 RID: 11896
		// (get) Token: 0x06007E59 RID: 32345 RVA: 0x001A2B17 File Offset: 0x001A0D17
		public static LocalizedString ClientCertAuthAccepted
		{
			get
			{
				return new LocalizedString("ClientCertAuthAccepted", "ExC62663", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E5A RID: 32346 RVA: 0x001A2B38 File Offset: 0x001A0D38
		public static LocalizedString ErrorGroupMemberDepartRestrictionApprovalRequired(string id)
		{
			return new LocalizedString("ErrorGroupMemberDepartRestrictionApprovalRequired", "Ex0C7650", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x17002E79 RID: 11897
		// (get) Token: 0x06007E5B RID: 32347 RVA: 0x001A2B67 File Offset: 0x001A0D67
		public static LocalizedString ExceptionAdminLimitExceeded
		{
			get
			{
				return new LocalizedString("ExceptionAdminLimitExceeded", "ExA990B2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E7A RID: 11898
		// (get) Token: 0x06007E5C RID: 32348 RVA: 0x001A2B85 File Offset: 0x001A0D85
		public static LocalizedString DataMoveReplicationConstraintSecondCopy
		{
			get
			{
				return new LocalizedString("DataMoveReplicationConstraintSecondCopy", "Ex3A5152", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E5D RID: 32349 RVA: 0x001A2BA4 File Offset: 0x001A0DA4
		public static LocalizedString TransientGlsError(string message)
		{
			return new LocalizedString("TransientGlsError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17002E7B RID: 11899
		// (get) Token: 0x06007E5E RID: 32350 RVA: 0x001A2BD3 File Offset: 0x001A0DD3
		public static LocalizedString ReceiveAuthMechanismTls
		{
			get
			{
				return new LocalizedString("ReceiveAuthMechanismTls", "Ex70CA91", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E7C RID: 11900
		// (get) Token: 0x06007E5F RID: 32351 RVA: 0x001A2BF1 File Offset: 0x001A0DF1
		public static LocalizedString CannotFindTemplateTenant
		{
			get
			{
				return new LocalizedString("CannotFindTemplateTenant", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E7D RID: 11901
		// (get) Token: 0x06007E60 RID: 32352 RVA: 0x001A2C0F File Offset: 0x001A0E0F
		public static LocalizedString FailedToReadStoreUserInformation
		{
			get
			{
				return new LocalizedString("FailedToReadStoreUserInformation", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E61 RID: 32353 RVA: 0x001A2C30 File Offset: 0x001A0E30
		public static LocalizedString FilterCannotBeEmpty(ScopeRestrictionType scopeType)
		{
			return new LocalizedString("FilterCannotBeEmpty", "ExC4D6A3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scopeType
			});
		}

		// Token: 0x06007E62 RID: 32354 RVA: 0x001A2C64 File Offset: 0x001A0E64
		public static LocalizedString ExceptionADTopologyServiceCallError(string server, string error)
		{
			return new LocalizedString("ExceptionADTopologyServiceCallError", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				error
			});
		}

		// Token: 0x17002E7E RID: 11902
		// (get) Token: 0x06007E63 RID: 32355 RVA: 0x001A2C97 File Offset: 0x001A0E97
		public static LocalizedString MicrosoftExchangeRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("MicrosoftExchangeRecipientTypeDetails", "ExFC8F34", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E7F RID: 11903
		// (get) Token: 0x06007E64 RID: 32356 RVA: 0x001A2CB5 File Offset: 0x001A0EB5
		public static LocalizedString DataMoveReplicationConstraintCINoReplication
		{
			get
			{
				return new LocalizedString("DataMoveReplicationConstraintCINoReplication", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E65 RID: 32357 RVA: 0x001A2CD4 File Offset: 0x001A0ED4
		public static LocalizedString InvalidDistributionGroupNamingPolicyFormat(string value, string placeHolders)
		{
			return new LocalizedString("InvalidDistributionGroupNamingPolicyFormat", "Ex6CBA35", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				value,
				placeHolders
			});
		}

		// Token: 0x17002E80 RID: 11904
		// (get) Token: 0x06007E66 RID: 32358 RVA: 0x001A2D07 File Offset: 0x001A0F07
		public static LocalizedString ErrorTransitionCounterHasZeroCount
		{
			get
			{
				return new LocalizedString("ErrorTransitionCounterHasZeroCount", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E81 RID: 11905
		// (get) Token: 0x06007E67 RID: 32359 RVA: 0x001A2D25 File Offset: 0x001A0F25
		public static LocalizedString DeleteAndQuarantineThreshold
		{
			get
			{
				return new LocalizedString("DeleteAndQuarantineThreshold", "Ex9B4DE4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E68 RID: 32360 RVA: 0x001A2D44 File Offset: 0x001A0F44
		public static LocalizedString ConfigurationSettingsNotFoundForGroup(string groupName, string key)
		{
			return new LocalizedString("ConfigurationSettingsNotFoundForGroup", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				groupName,
				key
			});
		}

		// Token: 0x17002E82 RID: 11906
		// (get) Token: 0x06007E69 RID: 32361 RVA: 0x001A2D77 File Offset: 0x001A0F77
		public static LocalizedString IndustryAgriculture
		{
			get
			{
				return new LocalizedString("IndustryAgriculture", "Ex592140", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E6A RID: 32362 RVA: 0x001A2D98 File Offset: 0x001A0F98
		public static LocalizedString CannotGetDomainInfo(string error)
		{
			return new LocalizedString("CannotGetDomainInfo", "ExCAD228", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06007E6B RID: 32363 RVA: 0x001A2DC8 File Offset: 0x001A0FC8
		public static LocalizedString ExceptionGuidSearchRootWithScope(string guid)
		{
			return new LocalizedString("ExceptionGuidSearchRootWithScope", "ExA302C0", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x17002E83 RID: 11907
		// (get) Token: 0x06007E6C RID: 32364 RVA: 0x001A2DF7 File Offset: 0x001A0FF7
		public static LocalizedString ClientCertAuthRequired
		{
			get
			{
				return new LocalizedString("ClientCertAuthRequired", "ExAFB27E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E84 RID: 11908
		// (get) Token: 0x06007E6D RID: 32365 RVA: 0x001A2E15 File Offset: 0x001A1015
		public static LocalizedString ServerRoleExtendedRole7
		{
			get
			{
				return new LocalizedString("ServerRoleExtendedRole7", "ExD7581D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E85 RID: 11909
		// (get) Token: 0x06007E6E RID: 32366 RVA: 0x001A2E33 File Offset: 0x001A1033
		public static LocalizedString SubmissionOverrideListOnWrongServer
		{
			get
			{
				return new LocalizedString("SubmissionOverrideListOnWrongServer", "Ex19E9E0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E86 RID: 11910
		// (get) Token: 0x06007E6F RID: 32367 RVA: 0x001A2E51 File Offset: 0x001A1051
		public static LocalizedString EsnLangBasque
		{
			get
			{
				return new LocalizedString("EsnLangBasque", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E87 RID: 11911
		// (get) Token: 0x06007E70 RID: 32368 RVA: 0x001A2E6F File Offset: 0x001A106F
		public static LocalizedString UserRecipientType
		{
			get
			{
				return new LocalizedString("UserRecipientType", "Ex8D44B2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E88 RID: 11912
		// (get) Token: 0x06007E71 RID: 32369 RVA: 0x001A2E8D File Offset: 0x001A108D
		public static LocalizedString MailEnabledUserRecipientType
		{
			get
			{
				return new LocalizedString("MailEnabledUserRecipientType", "ExAA595C", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E89 RID: 11913
		// (get) Token: 0x06007E72 RID: 32370 RVA: 0x001A2EAB File Offset: 0x001A10AB
		public static LocalizedString GroupTypeFlagsGlobal
		{
			get
			{
				return new LocalizedString("GroupTypeFlagsGlobal", "Ex470809", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E8A RID: 11914
		// (get) Token: 0x06007E73 RID: 32371 RVA: 0x001A2EC9 File Offset: 0x001A10C9
		public static LocalizedString DataMoveReplicationConstraintCISecondDatacenter
		{
			get
			{
				return new LocalizedString("DataMoveReplicationConstraintCISecondDatacenter", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E8B RID: 11915
		// (get) Token: 0x06007E74 RID: 32372 RVA: 0x001A2EE7 File Offset: 0x001A10E7
		public static LocalizedString LoadBalanceCannotUseBothInclusionLists
		{
			get
			{
				return new LocalizedString("LoadBalanceCannotUseBothInclusionLists", "ExEEC7D2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E8C RID: 11916
		// (get) Token: 0x06007E75 RID: 32373 RVA: 0x001A2F05 File Offset: 0x001A1105
		public static LocalizedString ExchangeMissedcallMC
		{
			get
			{
				return new LocalizedString("ExchangeMissedcallMC", "Ex7DE132", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E76 RID: 32374 RVA: 0x001A2F24 File Offset: 0x001A1124
		public static LocalizedString ErrorCannotFindTenant(string tenant, string partition)
		{
			return new LocalizedString("ErrorCannotFindTenant", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				tenant,
				partition
			});
		}

		// Token: 0x17002E8D RID: 11917
		// (get) Token: 0x06007E77 RID: 32375 RVA: 0x001A2F57 File Offset: 0x001A1157
		public static LocalizedString RequesterNameInvalid
		{
			get
			{
				return new LocalizedString("RequesterNameInvalid", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E78 RID: 32376 RVA: 0x001A2F78 File Offset: 0x001A1178
		public static LocalizedString ErrorNoSuitableDC(string server, string forest)
		{
			return new LocalizedString("ErrorNoSuitableDC", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				server,
				forest
			});
		}

		// Token: 0x06007E79 RID: 32377 RVA: 0x001A2FAC File Offset: 0x001A11AC
		public static LocalizedString SharingPolicyDuplicateDomain(string domains)
		{
			return new LocalizedString("SharingPolicyDuplicateDomain", "Ex63CC41", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				domains
			});
		}

		// Token: 0x06007E7A RID: 32378 RVA: 0x001A2FDC File Offset: 0x001A11DC
		public static LocalizedString ErrorNonUniqueCertificate(string certificate)
		{
			return new LocalizedString("ErrorNonUniqueCertificate", "ExC29AE0", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				certificate
			});
		}

		// Token: 0x17002E8E RID: 11918
		// (get) Token: 0x06007E7B RID: 32379 RVA: 0x001A300B File Offset: 0x001A120B
		public static LocalizedString ByteEncoderTypeUseBase64Html7BitTextPlain
		{
			get
			{
				return new LocalizedString("ByteEncoderTypeUseBase64Html7BitTextPlain", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E8F RID: 11919
		// (get) Token: 0x06007E7C RID: 32380 RVA: 0x001A3029 File Offset: 0x001A1229
		public static LocalizedString SecurityPrincipalTypeComputer
		{
			get
			{
				return new LocalizedString("SecurityPrincipalTypeComputer", "Ex62AE96", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E90 RID: 11920
		// (get) Token: 0x06007E7D RID: 32381 RVA: 0x001A3047 File Offset: 0x001A1247
		public static LocalizedString EsnLangAmharic
		{
			get
			{
				return new LocalizedString("EsnLangAmharic", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E7E RID: 32382 RVA: 0x001A3068 File Offset: 0x001A1268
		public static LocalizedString OrgWideDelegatingConfigScopeMustBeTheSameAsRoleImplicitWriteScope(ConfigWriteScopeType scopeType)
		{
			return new LocalizedString("OrgWideDelegatingConfigScopeMustBeTheSameAsRoleImplicitWriteScope", "ExCB74DA", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				scopeType
			});
		}

		// Token: 0x17002E91 RID: 11921
		// (get) Token: 0x06007E7F RID: 32383 RVA: 0x001A309C File Offset: 0x001A129C
		public static LocalizedString LimitedMoveOnlyAllowed
		{
			get
			{
				return new LocalizedString("LimitedMoveOnlyAllowed", "ExDE80AA", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E92 RID: 11922
		// (get) Token: 0x06007E80 RID: 32384 RVA: 0x001A30BA File Offset: 0x001A12BA
		public static LocalizedString ASInvalidAuthenticationOptionsForAccessMethod
		{
			get
			{
				return new LocalizedString("ASInvalidAuthenticationOptionsForAccessMethod", "Ex12F6AD", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E93 RID: 11923
		// (get) Token: 0x06007E81 RID: 32385 RVA: 0x001A30D8 File Offset: 0x001A12D8
		public static LocalizedString NullPasswordEncryptionKey
		{
			get
			{
				return new LocalizedString("NullPasswordEncryptionKey", "Ex18E022", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E94 RID: 11924
		// (get) Token: 0x06007E82 RID: 32386 RVA: 0x001A30F6 File Offset: 0x001A12F6
		public static LocalizedString LinkedUserTypeDetails
		{
			get
			{
				return new LocalizedString("LinkedUserTypeDetails", "ExB27AF6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E83 RID: 32387 RVA: 0x001A3114 File Offset: 0x001A1314
		public static LocalizedString ExceptionNativeErrorWhenLookingForServersInDomain(int error, string domain, string message)
		{
			return new LocalizedString("ExceptionNativeErrorWhenLookingForServersInDomain", "Ex9AD963", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				error,
				domain,
				message
			});
		}

		// Token: 0x06007E84 RID: 32388 RVA: 0x001A3150 File Offset: 0x001A1350
		public static LocalizedString ErrorMoreThanOneSKUCapability(string values)
		{
			return new LocalizedString("ErrorMoreThanOneSKUCapability", "Ex71B824", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				values
			});
		}

		// Token: 0x17002E95 RID: 11925
		// (get) Token: 0x06007E85 RID: 32389 RVA: 0x001A317F File Offset: 0x001A137F
		public static LocalizedString AutoDatabaseMountDialLossless
		{
			get
			{
				return new LocalizedString("AutoDatabaseMountDialLossless", "Ex4C0DF7", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E96 RID: 11926
		// (get) Token: 0x06007E86 RID: 32390 RVA: 0x001A319D File Offset: 0x001A139D
		public static LocalizedString ReceiveAuthMechanismExternalAuthoritative
		{
			get
			{
				return new LocalizedString("ReceiveAuthMechanismExternalAuthoritative", "Ex402E55", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E97 RID: 11927
		// (get) Token: 0x06007E87 RID: 32391 RVA: 0x001A31BB File Offset: 0x001A13BB
		public static LocalizedString ErrorTruncationLagTime
		{
			get
			{
				return new LocalizedString("ErrorTruncationLagTime", "Ex3A1B51", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E98 RID: 11928
		// (get) Token: 0x06007E88 RID: 32392 RVA: 0x001A31D9 File Offset: 0x001A13D9
		public static LocalizedString ExceptionIdImmutable
		{
			get
			{
				return new LocalizedString("ExceptionIdImmutable", "ExB9F6F0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E89 RID: 32393 RVA: 0x001A31F8 File Offset: 0x001A13F8
		public static LocalizedString AlternateServiceAccountConfigurationDisplayFormat(string latestCredential, string previousCredential, string elipsis)
		{
			return new LocalizedString("AlternateServiceAccountConfigurationDisplayFormat", "ExC646BD", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				latestCredential,
				previousCredential,
				elipsis
			});
		}

		// Token: 0x17002E99 RID: 11929
		// (get) Token: 0x06007E8A RID: 32394 RVA: 0x001A322F File Offset: 0x001A142F
		public static LocalizedString ExceptionDefaultScopeAndSearchRoot
		{
			get
			{
				return new LocalizedString("ExceptionDefaultScopeAndSearchRoot", "Ex843EB0", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E9A RID: 11930
		// (get) Token: 0x06007E8B RID: 32395 RVA: 0x001A324D File Offset: 0x001A144D
		public static LocalizedString ErrorOfferProgramIdMandatoryOnSharedConfig
		{
			get
			{
				return new LocalizedString("ErrorOfferProgramIdMandatoryOnSharedConfig", "Ex3C3F13", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E8C RID: 32396 RVA: 0x001A326C File Offset: 0x001A146C
		public static LocalizedString DefaultDatabaseContainerNotFoundException(string agName)
		{
			return new LocalizedString("DefaultDatabaseContainerNotFoundException", "ExEBBDF2", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				agName
			});
		}

		// Token: 0x17002E9B RID: 11931
		// (get) Token: 0x06007E8D RID: 32397 RVA: 0x001A329B File Offset: 0x001A149B
		public static LocalizedString ServerRoleExtendedRole4
		{
			get
			{
				return new LocalizedString("ServerRoleExtendedRole4", "Ex125DC2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E8E RID: 32398 RVA: 0x001A32BC File Offset: 0x001A14BC
		public static LocalizedString ExceptionAdamGetServerFromDomainDN(string DN)
		{
			return new LocalizedString("ExceptionAdamGetServerFromDomainDN", "Ex502EE4", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				DN
			});
		}

		// Token: 0x17002E9C RID: 11932
		// (get) Token: 0x06007E8F RID: 32399 RVA: 0x001A32EB File Offset: 0x001A14EB
		public static LocalizedString ErrorComment
		{
			get
			{
				return new LocalizedString("ErrorComment", "Ex9EF6E6", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002E9D RID: 11933
		// (get) Token: 0x06007E90 RID: 32400 RVA: 0x001A3309 File Offset: 0x001A1509
		public static LocalizedString ErrorReplayLagTime
		{
			get
			{
				return new LocalizedString("ErrorReplayLagTime", "Ex0077A5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E91 RID: 32401 RVA: 0x001A3328 File Offset: 0x001A1528
		public static LocalizedString ErrorEdbFilePathInRoot(string edbFileFullPath)
		{
			return new LocalizedString("ErrorEdbFilePathInRoot", "Ex709FA0", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				edbFileFullPath
			});
		}

		// Token: 0x17002E9E RID: 11934
		// (get) Token: 0x06007E92 RID: 32402 RVA: 0x001A3357 File Offset: 0x001A1557
		public static LocalizedString ExLengthOfVersionByteArrayError
		{
			get
			{
				return new LocalizedString("ExLengthOfVersionByteArrayError", "Ex4E3FF4", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E93 RID: 32403 RVA: 0x001A3378 File Offset: 0x001A1578
		public static LocalizedString ConfigurationSettingsGroupSummary(string name, int priority, bool enabled, int count)
		{
			return new LocalizedString("ConfigurationSettingsGroupSummary", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				name,
				priority,
				enabled,
				count
			});
		}

		// Token: 0x17002E9F RID: 11935
		// (get) Token: 0x06007E94 RID: 32404 RVA: 0x001A33C2 File Offset: 0x001A15C2
		public static LocalizedString LdapAdd
		{
			get
			{
				return new LocalizedString("LdapAdd", "Ex33E59E", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002EA0 RID: 11936
		// (get) Token: 0x06007E95 RID: 32405 RVA: 0x001A33E0 File Offset: 0x001A15E0
		public static LocalizedString DomainStatePendingActivation
		{
			get
			{
				return new LocalizedString("DomainStatePendingActivation", "ExFE17FF", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E96 RID: 32406 RVA: 0x001A3400 File Offset: 0x001A1600
		public static LocalizedString InvalidSmartHost(string smartHost)
		{
			return new LocalizedString("InvalidSmartHost", "Ex969628", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				smartHost
			});
		}

		// Token: 0x06007E97 RID: 32407 RVA: 0x001A3430 File Offset: 0x001A1630
		public static LocalizedString ProviderFileNotFoundLoadException(string providerName, string assemblyPath)
		{
			return new LocalizedString("ProviderFileNotFoundLoadException", "Ex2C855E", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				providerName,
				assemblyPath
			});
		}

		// Token: 0x17002EA1 RID: 11937
		// (get) Token: 0x06007E98 RID: 32408 RVA: 0x001A3463 File Offset: 0x001A1663
		public static LocalizedString Uninterruptible
		{
			get
			{
				return new LocalizedString("Uninterruptible", "Ex9AF75D", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002EA2 RID: 11938
		// (get) Token: 0x06007E99 RID: 32409 RVA: 0x001A3481 File Offset: 0x001A1681
		public static LocalizedString ErrorMustBeADRawEntry
		{
			get
			{
				return new LocalizedString("ErrorMustBeADRawEntry", "ExD92094", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002EA3 RID: 11939
		// (get) Token: 0x06007E9A RID: 32410 RVA: 0x001A349F File Offset: 0x001A169F
		public static LocalizedString None
		{
			get
			{
				return new LocalizedString("None", "Ex93DDB2", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007E9B RID: 32411 RVA: 0x001A34C0 File Offset: 0x001A16C0
		public static LocalizedString ErrorRecipientAsBothAcceptedAndRejected(string recipient)
		{
			return new LocalizedString("ErrorRecipientAsBothAcceptedAndRejected", "ExF446CA", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				recipient
			});
		}

		// Token: 0x17002EA4 RID: 11940
		// (get) Token: 0x06007E9C RID: 32412 RVA: 0x001A34EF File Offset: 0x001A16EF
		public static LocalizedString ErrorBadLocalizedComment
		{
			get
			{
				return new LocalizedString("ErrorBadLocalizedComment", "Ex68C60A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002EA5 RID: 11941
		// (get) Token: 0x06007E9D RID: 32413 RVA: 0x001A350D File Offset: 0x001A170D
		public static LocalizedString EsnLangSlovak
		{
			get
			{
				return new LocalizedString("EsnLangSlovak", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002EA6 RID: 11942
		// (get) Token: 0x06007E9E RID: 32414 RVA: 0x001A352B File Offset: 0x001A172B
		public static LocalizedString LdapFilterErrorInvalidBooleanValue
		{
			get
			{
				return new LocalizedString("LdapFilterErrorInvalidBooleanValue", "Ex9F209A", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002EA7 RID: 11943
		// (get) Token: 0x06007E9F RID: 32415 RVA: 0x001A3549 File Offset: 0x001A1749
		public static LocalizedString OabVersionsNullException
		{
			get
			{
				return new LocalizedString("OabVersionsNullException", "Ex0E1513", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007EA0 RID: 32416 RVA: 0x001A3568 File Offset: 0x001A1768
		public static LocalizedString DuplicateTlsDomainCapabilitiesNotAllowed(SmtpDomainWithSubdomains domain)
		{
			return new LocalizedString("DuplicateTlsDomainCapabilitiesNotAllowed", "Ex999C8F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				domain
			});
		}

		// Token: 0x06007EA1 RID: 32417 RVA: 0x001A3598 File Offset: 0x001A1798
		public static LocalizedString CannotConstructCrossTenantObjectId(string property)
		{
			return new LocalizedString("CannotConstructCrossTenantObjectId", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x06007EA2 RID: 32418 RVA: 0x001A35C8 File Offset: 0x001A17C8
		public static LocalizedString CrossRecordMismatch(MservRecord record1, MservRecord record2)
		{
			return new LocalizedString("CrossRecordMismatch", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				record1,
				record2
			});
		}

		// Token: 0x17002EA8 RID: 11944
		// (get) Token: 0x06007EA3 RID: 32419 RVA: 0x001A35FB File Offset: 0x001A17FB
		public static LocalizedString Inbox
		{
			get
			{
				return new LocalizedString("Inbox", "ExF47939", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007EA4 RID: 32420 RVA: 0x001A361C File Offset: 0x001A181C
		public static LocalizedString ErrorStartDateAfterEndDate(string start, string end)
		{
			return new LocalizedString("ErrorStartDateAfterEndDate", "Ex33BBE5", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				start,
				end
			});
		}

		// Token: 0x06007EA5 RID: 32421 RVA: 0x001A3650 File Offset: 0x001A1850
		public static LocalizedString ErrorNotInReadScope(string identity)
		{
			return new LocalizedString("ErrorNotInReadScope", "ExD700E7", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x06007EA6 RID: 32422 RVA: 0x001A3680 File Offset: 0x001A1880
		public static LocalizedString ErrorSubnetMaskLessThanMinRange(int maskBits, int minRange)
		{
			return new LocalizedString("ErrorSubnetMaskLessThanMinRange", "ExA97C7C", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				maskBits,
				minRange
			});
		}

		// Token: 0x17002EA9 RID: 11945
		// (get) Token: 0x06007EA7 RID: 32423 RVA: 0x001A36BD File Offset: 0x001A18BD
		public static LocalizedString ContactRecipientTypeDetails
		{
			get
			{
				return new LocalizedString("ContactRecipientTypeDetails", "Ex6F6521", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007EA8 RID: 32424 RVA: 0x001A36DC File Offset: 0x001A18DC
		public static LocalizedString InvalidTenantRecordInGls(Guid tenantGuid, string resourceForestFqdn, string accountForestFqdn)
		{
			return new LocalizedString("InvalidTenantRecordInGls", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				tenantGuid,
				resourceForestFqdn,
				accountForestFqdn
			});
		}

		// Token: 0x17002EAA RID: 11946
		// (get) Token: 0x06007EA9 RID: 32425 RVA: 0x001A3718 File Offset: 0x001A1918
		public static LocalizedString EsnLangKazakh
		{
			get
			{
				return new LocalizedString("EsnLangKazakh", "", false, false, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007EAA RID: 32426 RVA: 0x001A3738 File Offset: 0x001A1938
		public static LocalizedString ErrorNonUniqueLiveIdMemberName(string liveIdMemberName)
		{
			return new LocalizedString("ErrorNonUniqueLiveIdMemberName", "", false, false, DirectoryStrings.ResourceManager, new object[]
			{
				liveIdMemberName
			});
		}

		// Token: 0x17002EAB RID: 11947
		// (get) Token: 0x06007EAB RID: 32427 RVA: 0x001A3767 File Offset: 0x001A1967
		public static LocalizedString DisableFilter
		{
			get
			{
				return new LocalizedString("DisableFilter", "Ex758142", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002EAC RID: 11948
		// (get) Token: 0x06007EAC RID: 32428 RVA: 0x001A3785 File Offset: 0x001A1985
		public static LocalizedString BluetoothHandsfreeOnly
		{
			get
			{
				return new LocalizedString("BluetoothHandsfreeOnly", "Ex5497C8", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002EAD RID: 11949
		// (get) Token: 0x06007EAD RID: 32429 RVA: 0x001A37A3 File Offset: 0x001A19A3
		public static LocalizedString GatewayGuid
		{
			get
			{
				return new LocalizedString("GatewayGuid", "Ex6F7DD5", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17002EAE RID: 11950
		// (get) Token: 0x06007EAE RID: 32430 RVA: 0x001A37C1 File Offset: 0x001A19C1
		public static LocalizedString CalendarSharingFreeBusyNone
		{
			get
			{
				return new LocalizedString("CalendarSharingFreeBusyNone", "Ex584674", false, true, DirectoryStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06007EAF RID: 32431 RVA: 0x001A37E0 File Offset: 0x001A19E0
		public static LocalizedString ExceptionAccessDeniedFromRUS(string server)
		{
			return new LocalizedString("ExceptionAccessDeniedFromRUS", "Ex08CB7F", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06007EB0 RID: 32432 RVA: 0x001A3810 File Offset: 0x001A1A10
		public static LocalizedString CannotCompareScopeObjectWithOU(string leftId, string leftType, string ou)
		{
			return new LocalizedString("CannotCompareScopeObjectWithOU", "Ex81EF20", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				leftId,
				leftType,
				ou
			});
		}

		// Token: 0x06007EB1 RID: 32433 RVA: 0x001A3848 File Offset: 0x001A1A48
		public static LocalizedString ErrorInvalidLegacyDN(string legacyDN)
		{
			return new LocalizedString("ErrorInvalidLegacyDN", "ExA5C9D3", false, true, DirectoryStrings.ResourceManager, new object[]
			{
				legacyDN
			});
		}

		// Token: 0x06007EB2 RID: 32434 RVA: 0x001A3877 File Offset: 0x001A1A77
		public static LocalizedString GetLocalizedString(DirectoryStrings.IDs key)
		{
			return new LocalizedString(DirectoryStrings.stringIDs[(uint)key], DirectoryStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04004F85 RID: 20357
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(976);

		// Token: 0x04004F86 RID: 20358
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Data.Directory.Strings", typeof(DirectoryStrings).GetTypeInfo().Assembly);

		// Token: 0x02000A54 RID: 2644
		public enum IDs : uint
		{
			// Token: 0x04004F88 RID: 20360
			GroupMailboxRecipientTypeDetails = 2223810040U,
			// Token: 0x04004F89 RID: 20361
			InvalidTransportSyncHealthLogSizeConfiguration = 3059540560U,
			// Token: 0x04004F8A RID: 20362
			ReceiveExtendedProtectionPolicyNone = 3553131525U,
			// Token: 0x04004F8B RID: 20363
			OrganizationCapabilityManagement = 1352261648U,
			// Token: 0x04004F8C RID: 20364
			EsnLangTamil = 3384994469U,
			// Token: 0x04004F8D RID: 20365
			LdapFilterErrorInvalidWildCard = 1623026330U,
			// Token: 0x04004F8E RID: 20366
			Individual = 3266435989U,
			// Token: 0x04004F8F RID: 20367
			ExternalRelay = 2171581398U,
			// Token: 0x04004F90 RID: 20368
			InvalidTransportSyncDownloadSizeConfiguration = 1719230762U,
			// Token: 0x04004F91 RID: 20369
			MessageRateSourceFlagsAll = 2928684304U,
			// Token: 0x04004F92 RID: 20370
			SKUCapabilityBPOSSBasic = 4209173728U,
			// Token: 0x04004F93 RID: 20371
			IndustryMediaMarketingAdvertising = 3376948578U,
			// Token: 0x04004F94 RID: 20372
			SKUCapabilityUnmanaged = 2570737323U,
			// Token: 0x04004F95 RID: 20373
			BackSyncDataSourceTransientErrorMessage = 4073959654U,
			// Token: 0x04004F96 RID: 20374
			MailEnabledNonUniversalGroupRecipientTypeDetails = 3376217818U,
			// Token: 0x04004F97 RID: 20375
			ADDriverStoreAccessPermanentError = 3806493464U,
			// Token: 0x04004F98 RID: 20376
			DeviceType = 3323369056U,
			// Token: 0x04004F99 RID: 20377
			EsnLangFarsi = 3229570631U,
			// Token: 0x04004F9A RID: 20378
			InvalidTempErrorSetting = 3503019411U,
			// Token: 0x04004F9B RID: 20379
			ReplicationTypeNone = 3874381006U,
			// Token: 0x04004F9C RID: 20380
			IndustryBusinessServicesConsulting = 1593550494U,
			// Token: 0x04004F9D RID: 20381
			ErrorAdfsConfigFormat = 210447381U,
			// Token: 0x04004F9E RID: 20382
			Quarantined = 996355914U,
			// Token: 0x04004F9F RID: 20383
			OutboundConnectorSmartHostShouldBePresentIfUseMXRecordFalse = 3114754472U,
			// Token: 0x04004FA0 RID: 20384
			LongRunningCostHandle = 364260824U,
			// Token: 0x04004FA1 RID: 20385
			EsnLangChineseTraditional = 3479185892U,
			// Token: 0x04004FA2 RID: 20386
			IndustryTransportation = 1803878278U,
			// Token: 0x04004FA3 RID: 20387
			Silent = 815885189U,
			// Token: 0x04004FA4 RID: 20388
			AlternateServiceAccountCredentialQualifiedUserNameWrongFormat = 3145530267U,
			// Token: 0x04004FA5 RID: 20389
			InvalidBannerSetting = 1840954879U,
			// Token: 0x04004FA6 RID: 20390
			GroupNamingPolicyCustomAttribute4 = 1924734196U,
			// Token: 0x04004FA7 RID: 20391
			InboundConnectorIncorrectCloudServicesMailEnabled = 230574830U,
			// Token: 0x04004FA8 RID: 20392
			LdapFilterErrorAnrIsNotSupported = 3936814429U,
			// Token: 0x04004FA9 RID: 20393
			E164 = 438888054U,
			// Token: 0x04004FAA RID: 20394
			ErrorAuthMetaDataContentEmpty = 1874297349U,
			// Token: 0x04004FAB RID: 20395
			MailEnabledContactRecipientType = 2285243143U,
			// Token: 0x04004FAC RID: 20396
			MailEnabledUniversalDistributionGroupRecipientTypeDetails = 562488721U,
			// Token: 0x04004FAD RID: 20397
			SendAuthMechanismExternalAuthoritative = 360676809U,
			// Token: 0x04004FAE RID: 20398
			InboundConnectorRequiredTlsSettingsInvalid = 528770240U,
			// Token: 0x04004FAF RID: 20399
			GroupNamingPolicyCustomAttribute1 = 1924734191U,
			// Token: 0x04004FB0 RID: 20400
			Dual = 1743625100U,
			// Token: 0x04004FB1 RID: 20401
			DatabaseCopyAutoActivationPolicyIntrasiteOnly = 1822619008U,
			// Token: 0x04004FB2 RID: 20402
			Never = 1594930120U,
			// Token: 0x04004FB3 RID: 20403
			ByteEncoderTypeUndefined = 1644017996U,
			// Token: 0x04004FB4 RID: 20404
			InvalidRcvProtocolLogSizeConfiguration = 3817431401U,
			// Token: 0x04004FB5 RID: 20405
			GetRootDseRequiresDomainController = 2785880074U,
			// Token: 0x04004FB6 RID: 20406
			InheritFromDialPlan = 637440764U,
			// Token: 0x04004FB7 RID: 20407
			OrganizationCapabilityMessageTracking = 737697267U,
			// Token: 0x04004FB8 RID: 20408
			InboundConnectorInvalidTlsSenderCertificateName = 1573064009U,
			// Token: 0x04004FB9 RID: 20409
			SoftDelete = 3133553171U,
			// Token: 0x04004FBA RID: 20410
			OrganizationCapabilityUMGrammar = 2570855206U,
			// Token: 0x04004FBB RID: 20411
			Allow = 1776488541U,
			// Token: 0x04004FBC RID: 20412
			DomainNameIsNull = 1261795780U,
			// Token: 0x04004FBD RID: 20413
			PromptForAlias = 2303788021U,
			// Token: 0x04004FBE RID: 20414
			ErrorSystemAddressListInWrongContainer = 3614458512U,
			// Token: 0x04004FBF RID: 20415
			ExceptionUnableToDisableAdminTopologyMode = 651742924U,
			// Token: 0x04004FC0 RID: 20416
			Secured = 1241597555U,
			// Token: 0x04004FC1 RID: 20417
			ExternalAndAuthSet = 2462615416U,
			// Token: 0x04004FC2 RID: 20418
			EsnLangJapanese = 597372135U,
			// Token: 0x04004FC3 RID: 20419
			EsnLangPortuguesePortugal = 1391104995U,
			// Token: 0x04004FC4 RID: 20420
			EsnLangFinnish = 2487111597U,
			// Token: 0x04004FC5 RID: 20421
			ExceptionOwaCannotSetPropertyOnVirtualDirectoryOtherThanExchweb = 2811972280U,
			// Token: 0x04004FC6 RID: 20422
			WhenDelivered = 2180736490U,
			// Token: 0x04004FC7 RID: 20423
			DomainStatePendingRelease = 2839188613U,
			// Token: 0x04004FC8 RID: 20424
			GroupNamingPolicyExtensionCustomAttribute2 = 1631812413U,
			// Token: 0x04004FC9 RID: 20425
			AutoGroup = 2127564032U,
			// Token: 0x04004FCA RID: 20426
			ErrorStartDateExpiration = 3880444299U,
			// Token: 0x04004FCB RID: 20427
			MailboxMoveStatusQueued = 285960632U,
			// Token: 0x04004FCC RID: 20428
			Minute = 2220842206U,
			// Token: 0x04004FCD RID: 20429
			SentItems = 590977256U,
			// Token: 0x04004FCE RID: 20430
			ExchangeVoicemailMC = 4029642168U,
			// Token: 0x04004FCF RID: 20431
			AppliedInFull = 3010978409U,
			// Token: 0x04004FD0 RID: 20432
			NoAddressSpaces = 2215231792U,
			// Token: 0x04004FD1 RID: 20433
			SKUCapabilityEOPStandardAddOn = 1387109368U,
			// Token: 0x04004FD2 RID: 20434
			IndustryNonProfit = 2260925979U,
			// Token: 0x04004FD3 RID: 20435
			EsnLangDefault = 3413117907U,
			// Token: 0x04004FD4 RID: 20436
			SpecifyCustomGreetingFileName = 707064308U,
			// Token: 0x04004FD5 RID: 20437
			EsnLangSlovenian = 671952847U,
			// Token: 0x04004FD6 RID: 20438
			TelExtn = 1059422816U,
			// Token: 0x04004FD7 RID: 20439
			LdapFilterErrorInvalidGtLtOperand = 4045631128U,
			// Token: 0x04004FD8 RID: 20440
			SystemMailboxRecipientType = 434185288U,
			// Token: 0x04004FD9 RID: 20441
			ReplicationTypeRemote = 2824730050U,
			// Token: 0x04004FDA RID: 20442
			Enterprise = 1414596097U,
			// Token: 0x04004FDB RID: 20443
			Gsm = 3115737533U,
			// Token: 0x04004FDC RID: 20444
			Journal = 4137480277U,
			// Token: 0x04004FDD RID: 20445
			SpamFilteringTestActionNone = 924597469U,
			// Token: 0x04004FDE RID: 20446
			CustomRoleDescription_MyPersonalInformation = 1877544294U,
			// Token: 0x04004FDF RID: 20447
			MailboxMoveStatusAutoSuspended = 1608856269U,
			// Token: 0x04004FE0 RID: 20448
			Any = 3068683316U,
			// Token: 0x04004FE1 RID: 20449
			Location = 2325276717U,
			// Token: 0x04004FE2 RID: 20450
			ExternalTrust = 3031587385U,
			// Token: 0x04004FE3 RID: 20451
			IndustryPrintingPublishing = 2830455760U,
			// Token: 0x04004FE4 RID: 20452
			AllComputers = 3577501733U,
			// Token: 0x04004FE5 RID: 20453
			ExceptionRusNotFound = 3208958876U,
			// Token: 0x04004FE6 RID: 20454
			GroupNamingPolicyCity = 2703120928U,
			// Token: 0x04004FE7 RID: 20455
			NoPagesSpecified = 1213668011U,
			// Token: 0x04004FE8 RID: 20456
			PublicDatabaseRecipientType = 2286842903U,
			// Token: 0x04004FE9 RID: 20457
			CanEnableLocalCopyState_CanBeEnabled = 4050233751U,
			// Token: 0x04004FEA RID: 20458
			RedirectToRecipientsNotSet = 1069069500U,
			// Token: 0x04004FEB RID: 20459
			InfoAnnouncementEnabled = 2472102570U,
			// Token: 0x04004FEC RID: 20460
			ConfigurationSettingsADConfigDriverError = 3867415726U,
			// Token: 0x04004FED RID: 20461
			LdapFilterErrorEscCharWithoutEscapable = 1919923094U,
			// Token: 0x04004FEE RID: 20462
			IndustryGovernment = 2829212743U,
			// Token: 0x04004FEF RID: 20463
			CustomRoleDescription_MyAddressInformation = 1921301802U,
			// Token: 0x04004FF0 RID: 20464
			EsnLangNorwegianNynorsk = 861191034U,
			// Token: 0x04004FF1 RID: 20465
			IndustryEngineeringArchitecture = 1996416364U,
			// Token: 0x04004FF2 RID: 20466
			SendAuthMechanismBasicAuth = 2551449657U,
			// Token: 0x04004FF3 RID: 20467
			SKUCapabilityEOPPremiumAddOn = 4060941198U,
			// Token: 0x04004FF4 RID: 20468
			ErrorResourceTypeInvalid = 1820951283U,
			// Token: 0x04004FF5 RID: 20469
			OrgContainerNotFoundException = 2134869325U,
			// Token: 0x04004FF6 RID: 20470
			SKUCapabilityBPOSSStandardArchive = 4076766949U,
			// Token: 0x04004FF7 RID: 20471
			InternalSenderAdminAddressRequired = 1359848478U,
			// Token: 0x04004FF8 RID: 20472
			CannotGetUsefulDomainInfo = 237887777U,
			// Token: 0x04004FF9 RID: 20473
			ErrorElcSuspensionNotEnabled = 332960507U,
			// Token: 0x04004FFA RID: 20474
			DatabaseMasterTypeServer = 2173147846U,
			// Token: 0x04004FFB RID: 20475
			ConnectionTimeoutLessThanInactivityTimeout = 3421832948U,
			// Token: 0x04004FFC RID: 20476
			HygieneSuitePremium = 444871648U,
			// Token: 0x04004FFD RID: 20477
			Exadmin = 2986397362U,
			// Token: 0x04004FFE RID: 20478
			ExceptionADTopologyCannotFindWellKnownExchangeGroup = 2704331370U,
			// Token: 0x04004FFF RID: 20479
			CommandFrequency = 2979126483U,
			// Token: 0x04005000 RID: 20480
			IndustryConstruction = 1861502555U,
			// Token: 0x04005001 RID: 20481
			SharedMailboxRecipientTypeDetails = 4263249978U,
			// Token: 0x04005002 RID: 20482
			AccessDeniedToEventLog = 2830208040U,
			// Token: 0x04005003 RID: 20483
			EsnLangSerbian = 1445823720U,
			// Token: 0x04005004 RID: 20484
			ReplicationTypeUnknown = 2242173198U,
			// Token: 0x04005005 RID: 20485
			ErrorDuplicateMapiIdsInConfiguredAttributes = 2058453416U,
			// Token: 0x04005006 RID: 20486
			DirectoryBasedEdgeBlockModeOn = 56170716U,
			// Token: 0x04005007 RID: 20487
			LiveCredentialWithoutBasic = 2380978387U,
			// Token: 0x04005008 RID: 20488
			ExclusiveConfigScopes = 1607133185U,
			// Token: 0x04005009 RID: 20489
			IndustryRealEstate = 1118921376U,
			// Token: 0x0400500A RID: 20490
			EsnLangNorwegian = 2956380840U,
			// Token: 0x0400500B RID: 20491
			ServerRoleMonitoring = 1024471425U,
			// Token: 0x0400500C RID: 20492
			ASInvalidAccessMethod = 1106844962U,
			// Token: 0x0400500D RID: 20493
			NotApplied = 403740404U,
			// Token: 0x0400500E RID: 20494
			ConfigurationSettingsADNotificationError = 2619186021U,
			// Token: 0x0400500F RID: 20495
			MonitoringMailboxRecipientTypeDetails = 729925097U,
			// Token: 0x04005010 RID: 20496
			EsnLangCroatian = 56435549U,
			// Token: 0x04005011 RID: 20497
			TlsAuthLevelWithDomainSecureEnabled = 2386455749U,
			// Token: 0x04005012 RID: 20498
			EsnLangGerman = 4104902926U,
			// Token: 0x04005013 RID: 20499
			RoleAssignmentPolicyDescription_Default = 3816616305U,
			// Token: 0x04005014 RID: 20500
			GroupTypeFlagsNone = 252422050U,
			// Token: 0x04005015 RID: 20501
			WellKnownRecipientTypeMailboxUsers = 2167560764U,
			// Token: 0x04005016 RID: 20502
			LdapFilterErrorInvalidWildCardGtLt = 1324265457U,
			// Token: 0x04005017 RID: 20503
			SmartHostNotSet = 3536945350U,
			// Token: 0x04005018 RID: 20504
			DeviceRule = 3531789014U,
			// Token: 0x04005019 RID: 20505
			NotTrust = 1299460569U,
			// Token: 0x0400501A RID: 20506
			EmailAgeFilterAll = 2824779686U,
			// Token: 0x0400501B RID: 20507
			LanguageBlockListNotSet = 1524653102U,
			// Token: 0x0400501C RID: 20508
			EsnLangSerbianCyrillic = 2547761307U,
			// Token: 0x0400501D RID: 20509
			CalendarAgeFilterSixMonths = 1551636376U,
			// Token: 0x0400501E RID: 20510
			ErrorMetadataNotSearchProperty = 1442063141U,
			// Token: 0x0400501F RID: 20511
			InvalidDefaultMailbox = 52431374U,
			// Token: 0x04005020 RID: 20512
			Drafts = 115734878U,
			// Token: 0x04005021 RID: 20513
			RemoteGroupMailboxRecipientTypeDetails = 2819908830U,
			// Token: 0x04005022 RID: 20514
			EsnLangSwahili = 289102393U,
			// Token: 0x04005023 RID: 20515
			ExceptionPagedReaderReadAllAfterEnumerating = 794922827U,
			// Token: 0x04005024 RID: 20516
			DsnDefaultLanguageMustBeSpecificCulture = 835151952U,
			// Token: 0x04005025 RID: 20517
			BestBodyFormat = 719400811U,
			// Token: 0x04005026 RID: 20518
			CanEnableLocalCopyState_AlreadyEnabled = 3064393132U,
			// Token: 0x04005027 RID: 20519
			DeviceDiscovery = 1010456570U,
			// Token: 0x04005028 RID: 20520
			AccessDenied = 3963885811U,
			// Token: 0x04005029 RID: 20521
			InvalidActiveUserStatisticsLogSizeConfiguration = 29593584U,
			// Token: 0x0400502A RID: 20522
			ErrorActionOnExpirationSpecified = 1014182364U,
			// Token: 0x0400502B RID: 20523
			TlsAuthLevelWithNoDomainOnSmartHost = 3776155750U,
			// Token: 0x0400502C RID: 20524
			DeferredFailoverEntryString = 1587883572U,
			// Token: 0x0400502D RID: 20525
			TaskItemsMC = 4110637509U,
			// Token: 0x0400502E RID: 20526
			GroupNamingPolicyCustomAttribute7 = 1924734197U,
			// Token: 0x0400502F RID: 20527
			UnknownAttribute = 2647513696U,
			// Token: 0x04005030 RID: 20528
			MountDialOverrideBestAvailability = 703634004U,
			// Token: 0x04005031 RID: 20529
			ErrorArbitrationMailboxPropertyEmailAddressesEmpty = 3562314173U,
			// Token: 0x04005032 RID: 20530
			AlternateServiceAccountCredentialNotSet = 608628016U,
			// Token: 0x04005033 RID: 20531
			DataMoveReplicationConstraintAllCopies = 2092029626U,
			// Token: 0x04005034 RID: 20532
			GlobalThrottlingPolicyAmbiguousException = 3902771789U,
			// Token: 0x04005035 RID: 20533
			InvalidServerStatisticsLogSizeConfiguration = 2998146498U,
			// Token: 0x04005036 RID: 20534
			SipResourceIdRequired = 3361458474U,
			// Token: 0x04005037 RID: 20535
			EsnLangPortuguese = 1616139245U,
			// Token: 0x04005038 RID: 20536
			AutoDetect = 4241336410U,
			// Token: 0x04005039 RID: 20537
			SpamFilteringActionRedirect = 4255105347U,
			// Token: 0x0400503A RID: 20538
			CanRunRestoreState_Invalid = 3532819894U,
			// Token: 0x0400503B RID: 20539
			OutboundConnectorIncorrectCloudServicesMailEnabled = 2530566197U,
			// Token: 0x0400503C RID: 20540
			DatabaseCopyAutoActivationPolicyBlocked = 95325995U,
			// Token: 0x0400503D RID: 20541
			CustomRoleDescription_MyName = 3838277697U,
			// Token: 0x0400503E RID: 20542
			EsnLangOriya = 3720125794U,
			// Token: 0x0400503F RID: 20543
			UserAgent = 702028774U,
			// Token: 0x04005040 RID: 20544
			DomainStateActive = 4211599483U,
			// Token: 0x04005041 RID: 20545
			PartnersCannotHaveWildcards = 1640391059U,
			// Token: 0x04005042 RID: 20546
			IPv4Only = 352018919U,
			// Token: 0x04005043 RID: 20547
			InboundConnectorInvalidIPCertificateCombinations = 4150730333U,
			// Token: 0x04005044 RID: 20548
			Exchange2003or2000 = 2814018773U,
			// Token: 0x04005045 RID: 20549
			ErrorOneProcessInitializedAsBothSingleAndMultiple = 2730108605U,
			// Token: 0x04005046 RID: 20550
			RoomListGroupTypeDetails = 1391517930U,
			// Token: 0x04005047 RID: 20551
			MailEnabledForestContactRecipientTypeDetails = 3586618070U,
			// Token: 0x04005048 RID: 20552
			ErrorAuthMetadataNoIssuingEndpoint = 2286188335U,
			// Token: 0x04005049 RID: 20553
			NonUniversalGroupRecipientTypeDetails = 2227190334U,
			// Token: 0x0400504A RID: 20554
			ErrorMustBeSysConfigObject = 2587673404U,
			// Token: 0x0400504B RID: 20555
			OutboundConnectorTlsSettingsInvalidTlsDomainWithoutDomainValidation = 2742534530U,
			// Token: 0x0400504C RID: 20556
			LdapFilterErrorInvalidBitwiseOperand = 135248896U,
			// Token: 0x0400504D RID: 20557
			ExceptionSetPreferredDCsOnlyForManagement = 2106980546U,
			// Token: 0x0400504E RID: 20558
			LegacyArchiveJournals = 3635271833U,
			// Token: 0x0400504F RID: 20559
			CustomInternalSubjectRequired = 2980205681U,
			// Token: 0x04005050 RID: 20560
			ErrorCannotAddArchiveMailbox = 238304980U,
			// Token: 0x04005051 RID: 20561
			NoNewCalls = 1479682494U,
			// Token: 0x04005052 RID: 20562
			ErrorMessageClassEmpty = 1615193486U,
			// Token: 0x04005053 RID: 20563
			GloballyDistributedOABCacheReadTimeoutError = 189075208U,
			// Token: 0x04005054 RID: 20564
			Manual = 523533880U,
			// Token: 0x04005055 RID: 20565
			ErrorAcceptedDomainCannotContainWildcardAndNegoConfig = 3179357576U,
			// Token: 0x04005056 RID: 20566
			UniversalSecurityGroupRecipientTypeDetails = 968858937U,
			// Token: 0x04005057 RID: 20567
			ArbitrationMailboxTypeDetails = 3647297993U,
			// Token: 0x04005058 RID: 20568
			CalendarAgeFilterAll = 575705180U,
			// Token: 0x04005059 RID: 20569
			GroupNamingPolicyCompany = 862838650U,
			// Token: 0x0400505A RID: 20570
			IndustryMining = 700328008U,
			// Token: 0x0400505B RID: 20571
			ServerRoleOSP = 2775202161U,
			// Token: 0x0400505C RID: 20572
			InvalidDirectoryConfiguration = 1190445522U,
			// Token: 0x0400505D RID: 20573
			ErrorDDLReferral = 3381355689U,
			// Token: 0x0400505E RID: 20574
			LdapFilterErrorNoAttributeValue = 3541370315U,
			// Token: 0x0400505F RID: 20575
			ExternalEnrollment = 606960029U,
			// Token: 0x04005060 RID: 20576
			ErrorTimeoutReadingSystemAddressListCache = 161599950U,
			// Token: 0x04005061 RID: 20577
			CanRunDefaultUpdateState_NotSuspended = 2375038189U,
			// Token: 0x04005062 RID: 20578
			PreferredInternetCodePageSio2022Jp = 1615928121U,
			// Token: 0x04005063 RID: 20579
			HtmlAndTextAlternative = 1762945050U,
			// Token: 0x04005064 RID: 20580
			GlobalAddressList = 1164140307U,
			// Token: 0x04005065 RID: 20581
			MailTipsAccessLevelNone = 1912797067U,
			// Token: 0x04005066 RID: 20582
			EsnLangGalician = 3599499864U,
			// Token: 0x04005067 RID: 20583
			ServerRoleFrontendTransport = 3786203794U,
			// Token: 0x04005068 RID: 20584
			Exchange2009 = 4087400250U,
			// Token: 0x04005069 RID: 20585
			TransientMservErrorDescription = 1259815309U,
			// Token: 0x0400506A RID: 20586
			ReceiveAuthMechanismExchangeServer = 48855524U,
			// Token: 0x0400506B RID: 20587
			Watsons = 3380639415U,
			// Token: 0x0400506C RID: 20588
			OrganizationCapabilityPstProvider = 2074397341U,
			// Token: 0x0400506D RID: 20589
			ErrorCapabilityNone = 2277391560U,
			// Token: 0x0400506E RID: 20590
			ExceptionAllDomainControllersUnavailable = 224904099U,
			// Token: 0x0400506F RID: 20591
			ServersContainerNotFoundException = 3495902273U,
			// Token: 0x04005070 RID: 20592
			MailboxMoveStatusCompletionInProgress = 296014363U,
			// Token: 0x04005071 RID: 20593
			ServerRoleMailbox = 2125756541U,
			// Token: 0x04005072 RID: 20594
			ErrorResourceTypeMissing = 3930280380U,
			// Token: 0x04005073 RID: 20595
			Contacts = 1716044995U,
			// Token: 0x04005074 RID: 20596
			SendAuthMechanismTls = 3268644368U,
			// Token: 0x04005075 RID: 20597
			AggregatedSessionCannotMakeMbxChanges = 3574113162U,
			// Token: 0x04005076 RID: 20598
			PAAEnabled = 1802707653U,
			// Token: 0x04005077 RID: 20599
			NonPartner = 2191519417U,
			// Token: 0x04005078 RID: 20600
			BasicAfterTLSWithoutBasic = 4008691139U,
			// Token: 0x04005079 RID: 20601
			ErrorSharedConfigurationBothRoles = 2672001483U,
			// Token: 0x0400507A RID: 20602
			EsnLangDutch = 4077321274U,
			// Token: 0x0400507B RID: 20603
			DsnLanguageNotSupportedForCustomization = 1406386932U,
			// Token: 0x0400507C RID: 20604
			IndustryNotSpecified = 2440749065U,
			// Token: 0x0400507D RID: 20605
			ErrorDDLFilterError = 1045941944U,
			// Token: 0x0400507E RID: 20606
			AddressList = 3599602982U,
			// Token: 0x0400507F RID: 20607
			MustDisplayComment = 795884132U,
			// Token: 0x04005080 RID: 20608
			ServerRoleFfoWebServices = 3464146580U,
			// Token: 0x04005081 RID: 20609
			ServerRoleClientAccess = 1052758952U,
			// Token: 0x04005082 RID: 20610
			SKUCapabilityBPOSSEnterprise = 3595585153U,
			// Token: 0x04005083 RID: 20611
			InvalidReceiveAuthModeExternalOnly = 3839109662U,
			// Token: 0x04005084 RID: 20612
			ErrorSettingOverrideNull = 2653001089U,
			// Token: 0x04005085 RID: 20613
			LdapFilterErrorQueryTooLong = 4230107429U,
			// Token: 0x04005086 RID: 20614
			ErrorMoveToDestinationFolderNotDefined = 1655452658U,
			// Token: 0x04005087 RID: 20615
			MailboxMoveStatusInProgress = 4190154187U,
			// Token: 0x04005088 RID: 20616
			SecurityPrincipalTypeGroup = 370461711U,
			// Token: 0x04005089 RID: 20617
			X400Authoritative = 1470218539U,
			// Token: 0x0400508A RID: 20618
			MailFlowPartnerInternalMailContentTypeMimeHtml = 3038327607U,
			// Token: 0x0400508B RID: 20619
			MailEnabledUserRecipientTypeDetails = 3689869554U,
			// Token: 0x0400508C RID: 20620
			ExtensionNull = 2902411778U,
			// Token: 0x0400508D RID: 20621
			Unsecured = 1573777228U,
			// Token: 0x0400508E RID: 20622
			ConnectorIdIsNotAnInteger = 4079674260U,
			// Token: 0x0400508F RID: 20623
			ErrorMissingPrimaryUM = 2617699176U,
			// Token: 0x04005090 RID: 20624
			CannotDetermineDataSessionType = 3783332642U,
			// Token: 0x04005091 RID: 20625
			UserAgentsChanges = 3601314308U,
			// Token: 0x04005092 RID: 20626
			Notes = 1601836855U,
			// Token: 0x04005093 RID: 20627
			EsnLangTelugu = 2395522212U,
			// Token: 0x04005094 RID: 20628
			GroupNamingPolicyExtensionCustomAttribute1 = 65728472U,
			// Token: 0x04005095 RID: 20629
			MailFlowPartnerInternalMailContentTypeNone = 1221325234U,
			// Token: 0x04005096 RID: 20630
			DefaultRapName = 268472571U,
			// Token: 0x04005097 RID: 20631
			DeleteUseDefaultAlert = 3648445463U,
			// Token: 0x04005098 RID: 20632
			ErrorOrganizationResourceAddressListsCount = 4156100093U,
			// Token: 0x04005099 RID: 20633
			EsnLangChineseSimplified = 3706505019U,
			// Token: 0x0400509A RID: 20634
			ConferenceRoomMailboxRecipientTypeDetails = 1919306754U,
			// Token: 0x0400509B RID: 20635
			BlockedOutlookClientVersionPatternDescription = 557877518U,
			// Token: 0x0400509C RID: 20636
			UserHasNoSmtpProxyAddressWithFederatedDomain = 1448153692U,
			// Token: 0x0400509D RID: 20637
			OrganizationCapabilityMailRouting = 2296213214U,
			// Token: 0x0400509E RID: 20638
			SKUCapabilityBPOSSStandard = 1325366747U,
			// Token: 0x0400509F RID: 20639
			SystemMailboxRecipientTypeDetails = 1850977098U,
			// Token: 0x040050A0 RID: 20640
			ExceptionADTopologyNoLocalDomain = 3702456775U,
			// Token: 0x040050A1 RID: 20641
			EsnLangDanish = 3504940969U,
			// Token: 0x040050A2 RID: 20642
			IndustryRetail = 779349581U,
			// Token: 0x040050A3 RID: 20643
			ErrorDDLNoSuchObject = 2864464497U,
			// Token: 0x040050A4 RID: 20644
			IndustryComputerRelatedProductsServices = 2713172052U,
			// Token: 0x040050A5 RID: 20645
			InternalRelay = 3288506612U,
			// Token: 0x040050A6 RID: 20646
			ErrorEmptyArchiveName = 602695546U,
			// Token: 0x040050A7 RID: 20647
			EmailAddressPolicyPriorityLowest = 1231743030U,
			// Token: 0x040050A8 RID: 20648
			ExternalMdm = 326106109U,
			// Token: 0x040050A9 RID: 20649
			TransportSettingsNotFoundException = 3799883362U,
			// Token: 0x040050AA RID: 20650
			DomainSecureEnabledWithoutTls = 3552372249U,
			// Token: 0x040050AB RID: 20651
			BccSuspiciousOutboundAdditionalRecipientsRequired = 1904959661U,
			// Token: 0x040050AC RID: 20652
			NoRoleEntriesFound = 4291428005U,
			// Token: 0x040050AD RID: 20653
			IndustryWholesale = 1515341016U,
			// Token: 0x040050AE RID: 20654
			ServerRoleCentralAdminFrontEnd = 3980237751U,
			// Token: 0x040050AF RID: 20655
			ErrorInvalidPushNotificationPlatform = 4068243401U,
			// Token: 0x040050B0 RID: 20656
			MailTipsAccessLevelAll = 2277405024U,
			// Token: 0x040050B1 RID: 20657
			PublicFolderRecipientTypeDetails = 1625030180U,
			// Token: 0x040050B2 RID: 20658
			ValueNotAvailableForUnchangedProperty = 943620946U,
			// Token: 0x040050B3 RID: 20659
			DumpsterFolder = 3641768400U,
			// Token: 0x040050B4 RID: 20660
			CannotParseMimeTypes = 1756055255U,
			// Token: 0x040050B5 RID: 20661
			ExclusiveRecipientScopes = 2518725308U,
			// Token: 0x040050B6 RID: 20662
			QuarantineMailboxIsInvalid = 1771494609U,
			// Token: 0x040050B7 RID: 20663
			MailboxPlanTypeDetails = 1094750789U,
			// Token: 0x040050B8 RID: 20664
			ServerRoleCafeArray = 986970413U,
			// Token: 0x040050B9 RID: 20665
			SendCredentialIsNull = 2874697860U,
			// Token: 0x040050BA RID: 20666
			True = 3323264318U,
			// Token: 0x040050BB RID: 20667
			StarAcceptedDomainCannotBeAuthoritative = 300685400U,
			// Token: 0x040050BC RID: 20668
			AllRooms = 2302903917U,
			// Token: 0x040050BD RID: 20669
			EsnLangRussian = 2650345129U,
			// Token: 0x040050BE RID: 20670
			GroupNamingPolicyCustomAttribute10 = 3989445055U,
			// Token: 0x040050BF RID: 20671
			SitesContainerNotFound = 79975170U,
			// Token: 0x040050C0 RID: 20672
			ExceptionServerTimeoutNegative = 1581344072U,
			// Token: 0x040050C1 RID: 20673
			ArchiveStateLocal = 665936024U,
			// Token: 0x040050C2 RID: 20674
			NotesMC = 991629433U,
			// Token: 0x040050C3 RID: 20675
			InvalidDomain = 3403459873U,
			// Token: 0x040050C4 RID: 20676
			EmailAgeFilterOneMonth = 2092969439U,
			// Token: 0x040050C5 RID: 20677
			FullDomain = 4021009371U,
			// Token: 0x040050C6 RID: 20678
			DeviceModel = 1430923151U,
			// Token: 0x040050C7 RID: 20679
			GroupRecipientType = 777275992U,
			// Token: 0x040050C8 RID: 20680
			RemoteSharedMailboxTypeDetails = 444885611U,
			// Token: 0x040050C9 RID: 20681
			LdapSearch = 261312351U,
			// Token: 0x040050CA RID: 20682
			EsnLangArabic = 3378383244U,
			// Token: 0x040050CB RID: 20683
			SKUCapabilityBPOSSDeskless = 3490390166U,
			// Token: 0x040050CC RID: 20684
			ModeratedRecipients = 3131560893U,
			// Token: 0x040050CD RID: 20685
			ExceptionRusOperationFailed = 1935657977U,
			// Token: 0x040050CE RID: 20686
			ExceptionDomainInfoRpcTooBusy = 4110564385U,
			// Token: 0x040050CF RID: 20687
			ErrorArchiveDomainInvalidInDatacenter = 3057991035U,
			// Token: 0x040050D0 RID: 20688
			PublicFolderRecipientType = 2922780138U,
			// Token: 0x040050D1 RID: 20689
			ErrorMessageClassHasUnsupportedWildcard = 2843126128U,
			// Token: 0x040050D2 RID: 20690
			ErrorPipelineTracingRequirementsMissing = 2976002772U,
			// Token: 0x040050D3 RID: 20691
			GroupNamingPolicyCustomAttribute11 = 2423361114U,
			// Token: 0x040050D4 RID: 20692
			ErrorMailTipMustNotBeEmpty = 65716788U,
			// Token: 0x040050D5 RID: 20693
			ComputerRecipientType = 1479699766U,
			// Token: 0x040050D6 RID: 20694
			ErrorArbitrationMailboxCannotBeModerated = 2493930652U,
			// Token: 0x040050D7 RID: 20695
			EsnLangKannada = 3119627512U,
			// Token: 0x040050D8 RID: 20696
			Title = 2435266816U,
			// Token: 0x040050D9 RID: 20697
			MessageWaitingIndicatorEnabled = 389262922U,
			// Token: 0x040050DA RID: 20698
			PublicFolders = 3178475968U,
			// Token: 0x040050DB RID: 20699
			Millisecond = 92440185U,
			// Token: 0x040050DC RID: 20700
			StarAcceptedDomainCannotBeDefault = 2661434578U,
			// Token: 0x040050DD RID: 20701
			ReceiveExtendedProtectionPolicyAllow = 2033242172U,
			// Token: 0x040050DE RID: 20702
			ResourceMailbox = 2995964430U,
			// Token: 0x040050DF RID: 20703
			ErrorThrottlingPolicyStateIsCorrupt = 3176413521U,
			// Token: 0x040050E0 RID: 20704
			MailEnabledNonUniversalGroupRecipientType = 3005725472U,
			// Token: 0x040050E1 RID: 20705
			ExternalAuthoritativeWithoutExchangeServerPermission = 573230607U,
			// Token: 0x040050E2 RID: 20706
			Authoritative = 2913015079U,
			// Token: 0x040050E3 RID: 20707
			ErrorPrimarySmtpAddressAndWindowsEmailAddressNotMatch = 2813895538U,
			// Token: 0x040050E4 RID: 20708
			PostMC = 1421974844U,
			// Token: 0x040050E5 RID: 20709
			UnknownConfigObject = 3819234583U,
			// Token: 0x040050E6 RID: 20710
			MalwareScanErrorActionAllow = 4274370117U,
			// Token: 0x040050E7 RID: 20711
			GroupNamingPolicyCustomAttribute6 = 1924734198U,
			// Token: 0x040050E8 RID: 20712
			InvalidTransportSyncLogSizeConfiguration = 2610662106U,
			// Token: 0x040050E9 RID: 20713
			WellKnownRecipientTypeMailGroups = 3725493575U,
			// Token: 0x040050EA RID: 20714
			ADDriverStoreAccessTransientError = 120558192U,
			// Token: 0x040050EB RID: 20715
			AACantChangeName = 635049629U,
			// Token: 0x040050EC RID: 20716
			ContactItemsMC = 3247735886U,
			// Token: 0x040050ED RID: 20717
			EsnLangKorean = 4170667976U,
			// Token: 0x040050EE RID: 20718
			RssSubscriptionMC = 131007819U,
			// Token: 0x040050EF RID: 20719
			LdapFilterErrorSpaceMiddleType = 1652093200U,
			// Token: 0x040050F0 RID: 20720
			GroupNamingPolicyCustomAttribute3 = 1924734193U,
			// Token: 0x040050F1 RID: 20721
			ExceptionNoFsmoRoleOwnerAttribute = 3167491578U,
			// Token: 0x040050F2 RID: 20722
			NonIpmRoot = 600983985U,
			// Token: 0x040050F3 RID: 20723
			ErrorTimeoutWritingSystemAddressListMemberCount = 2829159765U,
			// Token: 0x040050F4 RID: 20724
			ExceptionExternalError = 2900785706U,
			// Token: 0x040050F5 RID: 20725
			Calendar = 1292798904U,
			// Token: 0x040050F6 RID: 20726
			Wma = 2665399355U,
			// Token: 0x040050F7 RID: 20727
			ErrorInvalidDNDepth = 869401742U,
			// Token: 0x040050F8 RID: 20728
			CapabilityMasteredOnPremise = 1435812789U,
			// Token: 0x040050F9 RID: 20729
			EdgeSyncEhfConnectorFailedToDecryptPassword = 371000500U,
			// Token: 0x040050FA RID: 20730
			ErrorArchiveDomainSetForNonArchive = 1758334214U,
			// Token: 0x040050FB RID: 20731
			ExceptionObjectHasBeenDeleted = 3086681225U,
			// Token: 0x040050FC RID: 20732
			EsnLangBengaliIndia = 2211212701U,
			// Token: 0x040050FD RID: 20733
			PublicFolderServer = 613950136U,
			// Token: 0x040050FE RID: 20734
			ErrorCannotSetPrimarySmtpAddress = 4127869723U,
			// Token: 0x040050FF RID: 20735
			SpamFilteringActionQuarantine = 2852597951U,
			// Token: 0x04005100 RID: 20736
			MailboxMoveStatusFailed = 1313260064U,
			// Token: 0x04005101 RID: 20737
			SecurityPrincipalTypeUniversalSecurityGroup = 1169031248U,
			// Token: 0x04005102 RID: 20738
			DynamicDLRecipientType = 1254627662U,
			// Token: 0x04005103 RID: 20739
			ErrorNonTinyTenantShouldNotHaveSharedConfig = 2863183086U,
			// Token: 0x04005104 RID: 20740
			CanRunRestoreState_Allowed = 3057941193U,
			// Token: 0x04005105 RID: 20741
			DomainSecureWithIgnoreStartTLSEnabled = 1199714779U,
			// Token: 0x04005106 RID: 20742
			GroupNamingPolicyExtensionCustomAttribute4 = 825243359U,
			// Token: 0x04005107 RID: 20743
			UseMsg = 3705158290U,
			// Token: 0x04005108 RID: 20744
			InvalidTenantFullSyncCookieException = 149761450U,
			// Token: 0x04005109 RID: 20745
			AutoDatabaseMountDialGoodAvailability = 3241000569U,
			// Token: 0x0400510A RID: 20746
			ForestTrust = 4056279737U,
			// Token: 0x0400510B RID: 20747
			ErrorInvalidMailboxRelationType = 556546691U,
			// Token: 0x0400510C RID: 20748
			ErrorDDLInvalidDNSyntax = 2757465550U,
			// Token: 0x0400510D RID: 20749
			ByteEncoderTypeUseQP = 3615619130U,
			// Token: 0x0400510E RID: 20750
			NoLocatorInformationInMServException = 2204790954U,
			// Token: 0x0400510F RID: 20751
			SecurityPrincipalTypeGlobalSecurityGroup = 2004555878U,
			// Token: 0x04005110 RID: 20752
			CannotGetUsefulSiteInfo = 2999553646U,
			// Token: 0x04005111 RID: 20753
			ErrorPipelineTracingPathNotExist = 1031444357U,
			// Token: 0x04005112 RID: 20754
			MailboxServer = 1832080745U,
			// Token: 0x04005113 RID: 20755
			Blocked = 4019774802U,
			// Token: 0x04005114 RID: 20756
			InvalidMainStreamCookieException = 2649572709U,
			// Token: 0x04005115 RID: 20757
			MoveNotAllowed = 1341999288U,
			// Token: 0x04005116 RID: 20758
			RemoteRoomMailboxTypeDetails = 1594549261U,
			// Token: 0x04005117 RID: 20759
			SecurityPrincipalTypeUser = 1776235905U,
			// Token: 0x04005118 RID: 20760
			TextEnrichedOnly = 2078807267U,
			// Token: 0x04005119 RID: 20761
			BluetoothAllow = 3036506883U,
			// Token: 0x0400511A RID: 20762
			GroupNamingPolicyDepartment = 154085973U,
			// Token: 0x0400511B RID: 20763
			UseDefaultSettings = 3388588817U,
			// Token: 0x0400511C RID: 20764
			ByteEncoderTypeUseQPHtmlDetectTextPlain = 3102724093U,
			// Token: 0x0400511D RID: 20765
			Exchange2007 = 2924600836U,
			// Token: 0x0400511E RID: 20766
			DisabledPartner = 788602100U,
			// Token: 0x0400511F RID: 20767
			Consumer = 1344968854U,
			// Token: 0x04005120 RID: 20768
			PrimaryMailboxRelationType = 1013979892U,
			// Token: 0x04005121 RID: 20769
			Disabled = 1484405454U,
			// Token: 0x04005122 RID: 20770
			SKUCapabilityBPOSSBasicCustomDomain = 1091797613U,
			// Token: 0x04005123 RID: 20771
			ControlTextNull = 2308256473U,
			// Token: 0x04005124 RID: 20772
			Outbox = 629464291U,
			// Token: 0x04005125 RID: 20773
			ArchiveStateNone = 3086386447U,
			// Token: 0x04005126 RID: 20774
			MailFlowPartnerInternalMailContentTypeMimeText = 1727459539U,
			// Token: 0x04005127 RID: 20775
			CustomInternalBodyRequired = 2238564813U,
			// Token: 0x04005128 RID: 20776
			TlsDomainWithIncorrectTlsAuthLevel = 2086215909U,
			// Token: 0x04005129 RID: 20777
			SystemTag = 213405127U,
			// Token: 0x0400512A RID: 20778
			AllMailboxContentMC = 65301504U,
			// Token: 0x0400512B RID: 20779
			RemoteUserMailboxTypeDetails = 4145265495U,
			// Token: 0x0400512C RID: 20780
			BluetoothDisable = 3441693128U,
			// Token: 0x0400512D RID: 20781
			ServerRoleLanguagePacks = 2698858797U,
			// Token: 0x0400512E RID: 20782
			PrincipalName = 1415894913U,
			// Token: 0x0400512F RID: 20783
			IdIsNotSet = 3541826428U,
			// Token: 0x04005130 RID: 20784
			ConstraintViolationSupervisionListEntryStringPartIsInvalid = 1254345332U,
			// Token: 0x04005131 RID: 20785
			WellKnownRecipientTypeMailContacts = 2638599330U,
			// Token: 0x04005132 RID: 20786
			ServerRoleHubTransport = 172810921U,
			// Token: 0x04005133 RID: 20787
			IndustryHealthcare = 4251572755U,
			// Token: 0x04005134 RID: 20788
			CapabilityPartnerManaged = 3956811795U,
			// Token: 0x04005135 RID: 20789
			ErrorArchiveDatabaseArchiveDomainMissing = 4087147933U,
			// Token: 0x04005136 RID: 20790
			MailEnabledUniversalSecurityGroupRecipientType = 2967905667U,
			// Token: 0x04005137 RID: 20791
			ErrorRemovalNotSupported = 1486937545U,
			// Token: 0x04005138 RID: 20792
			ExchangeFaxMC = 1129549138U,
			// Token: 0x04005139 RID: 20793
			ByteEncoderTypeUse7Bit = 2611743021U,
			// Token: 0x0400513A RID: 20794
			InvalidBindingAddressSetting = 2176173662U,
			// Token: 0x0400513B RID: 20795
			ASAccessMethodNeedsAuthenticationAccount = 4172461161U,
			// Token: 0x0400513C RID: 20796
			CanRunDefaultUpdateState_Allowed = 491964191U,
			// Token: 0x0400513D RID: 20797
			EsnLangMalay = 2884121764U,
			// Token: 0x0400513E RID: 20798
			FailedToParseAlternateServiceAccountCredential = 1167380226U,
			// Token: 0x0400513F RID: 20799
			ExternalManagedMailContactTypeDetails = 3799817423U,
			// Token: 0x04005140 RID: 20800
			IPv6Only = 1403090333U,
			// Token: 0x04005141 RID: 20801
			MountDialOverrideLossless = 2827463711U,
			// Token: 0x04005142 RID: 20802
			Percent = 3607366039U,
			// Token: 0x04005143 RID: 20803
			ServerRoleProvisionedServer = 1922689150U,
			// Token: 0x04005144 RID: 20804
			CalendarAgeFilterOneMonth = 2350890097U,
			// Token: 0x04005145 RID: 20805
			TextOnly = 4169982073U,
			// Token: 0x04005146 RID: 20806
			InvalidMsgTrackingLogSizeConfiguration = 1291987412U,
			// Token: 0x04005147 RID: 20807
			ErrorArchiveDatabaseSetForNonArchive = 1881847987U,
			// Token: 0x04005148 RID: 20808
			InvalidGenerationTime = 1940813882U,
			// Token: 0x04005149 RID: 20809
			CalendarItemMC = 820626981U,
			// Token: 0x0400514A RID: 20810
			Block = 3745862197U,
			// Token: 0x0400514B RID: 20811
			ErrorNullExternalEmailAddress = 3519747066U,
			// Token: 0x0400514C RID: 20812
			ExceptionRusNotRunning = 1681980649U,
			// Token: 0x0400514D RID: 20813
			PropertyCannotBeSetToTest = 862550630U,
			// Token: 0x0400514E RID: 20814
			LdapFilterErrorInvalidEscaping = 1641698628U,
			// Token: 0x0400514F RID: 20815
			ForceSave = 867516332U,
			// Token: 0x04005150 RID: 20816
			LinkedRoomMailboxRecipientTypeDetails = 1798526791U,
			// Token: 0x04005151 RID: 20817
			DeleteUseCustomAlert = 3794956711U,
			// Token: 0x04005152 RID: 20818
			CannotDeserializePartitionHint = 370123223U,
			// Token: 0x04005153 RID: 20819
			InboundConnectorInvalidRestrictDomainsToIPAddresses = 1284675050U,
			// Token: 0x04005154 RID: 20820
			GroupNamingPolicyCustomAttribute14 = 1663846227U,
			// Token: 0x04005155 RID: 20821
			ContactRecipientType = 2839121159U,
			// Token: 0x04005156 RID: 20822
			DomainSecureWithoutDNSRoutingEnabled = 2600481667U,
			// Token: 0x04005157 RID: 20823
			RunspaceServerSettingsChanged = 3062547699U,
			// Token: 0x04005158 RID: 20824
			EsnLangGreek = 3313482992U,
			// Token: 0x04005159 RID: 20825
			TooManyEntriesError = 309994549U,
			// Token: 0x0400515A RID: 20826
			OrganizationRelationshipMissingTargetApplicationUri = 1899391140U,
			// Token: 0x0400515B RID: 20827
			ComputerRecipientTypeDetails = 3489169852U,
			// Token: 0x0400515C RID: 20828
			Exchweb = 96146822U,
			// Token: 0x0400515D RID: 20829
			OutboundConnectorIncorrectRouteAllMessagesViaOnPremises = 2202908911U,
			// Token: 0x0400515E RID: 20830
			CalendarSharingFreeBusyAvailabilityOnly = 1499257862U,
			// Token: 0x0400515F RID: 20831
			ServerRoleExtendedRole5 = 3707194059U,
			// Token: 0x04005160 RID: 20832
			AutoAttendantLink = 390976058U,
			// Token: 0x04005161 RID: 20833
			CustomRoleDescription_MyDisplayName = 4020865807U,
			// Token: 0x04005162 RID: 20834
			AllUsers = 3949283739U,
			// Token: 0x04005163 RID: 20835
			All = 4231482709U,
			// Token: 0x04005164 RID: 20836
			OrganizationCapabilityMigration = 4256840071U,
			// Token: 0x04005165 RID: 20837
			DialPlan = 142272059U,
			// Token: 0x04005166 RID: 20838
			EsnLangUkrainian = 2868284030U,
			// Token: 0x04005167 RID: 20839
			MessageRateSourceFlagsNone = 2052645173U,
			// Token: 0x04005168 RID: 20840
			IndustryLegal = 2196808673U,
			// Token: 0x04005169 RID: 20841
			CapabilityUMFeatureRestricted = 3379397641U,
			// Token: 0x0400516A RID: 20842
			GroupTypeFlagsBuiltinLocal = 1494101274U,
			// Token: 0x0400516B RID: 20843
			ReceiveAuthMechanismBasicAuthPlusTls = 2412300803U,
			// Token: 0x0400516C RID: 20844
			Allowed = 3811183882U,
			// Token: 0x0400516D RID: 20845
			ByteEncoderTypeUseQPHtml7BitTextPlain = 3579894660U,
			// Token: 0x0400516E RID: 20846
			High = 4217035038U,
			// Token: 0x0400516F RID: 20847
			MicrosoftExchangeRecipientType = 821502958U,
			// Token: 0x04005170 RID: 20848
			BackSyncDataSourceUnavailableMessage = 1429014682U,
			// Token: 0x04005171 RID: 20849
			ArchiveStateOnPremise = 110833865U,
			// Token: 0x04005172 RID: 20850
			OrganizationCapabilitySuiteServiceStorage = 936096413U,
			// Token: 0x04005173 RID: 20851
			MalwareScanErrorActionBlock = 1592544157U,
			// Token: 0x04005174 RID: 20852
			SKUCapabilityBPOSSArchiveAddOn = 1924944146U,
			// Token: 0x04005175 RID: 20853
			ExceptionRusAccessDenied = 4227895642U,
			// Token: 0x04005176 RID: 20854
			ServerRoleNone = 2094315795U,
			// Token: 0x04005177 RID: 20855
			AlternateServiceAccountConfigurationDisplayFormatMoreDataAvailable = 3352185505U,
			// Token: 0x04005178 RID: 20856
			GloballyDistributedOABCacheWriteTimeoutError = 2728392679U,
			// Token: 0x04005179 RID: 20857
			UserName = 3727360630U,
			// Token: 0x0400517A RID: 20858
			Reserved1 = 1173768533U,
			// Token: 0x0400517B RID: 20859
			NoAddresses = 3144162877U,
			// Token: 0x0400517C RID: 20860
			RegionBlockListNotSet = 2817538580U,
			// Token: 0x0400517D RID: 20861
			CapabilityRichCoexistence = 1398191848U,
			// Token: 0x0400517E RID: 20862
			ErrorUserAccountNameIncludeAt = 2032072470U,
			// Token: 0x0400517F RID: 20863
			Enabled = 634395589U,
			// Token: 0x04005180 RID: 20864
			AttachmentsWereRemovedMessage = 3840534502U,
			// Token: 0x04005181 RID: 20865
			ErrorCannotFindUnusedLegacyDN = 4176423907U,
			// Token: 0x04005182 RID: 20866
			EmailAgeFilterOneWeek = 2318114319U,
			// Token: 0x04005183 RID: 20867
			GroupNameInNamingPolicy = 2193124873U,
			// Token: 0x04005184 RID: 20868
			OrganizationCapabilityClientExtensions = 2504573058U,
			// Token: 0x04005185 RID: 20869
			CalendarAgeFilterTwoWeeks = 3081766090U,
			// Token: 0x04005186 RID: 20870
			ErrorElcCommentNotAllowed = 583050472U,
			// Token: 0x04005187 RID: 20871
			ErrorOwnersUpdated = 3275453767U,
			// Token: 0x04005188 RID: 20872
			EsnLangIndonesian = 3776377092U,
			// Token: 0x04005189 RID: 20873
			Extension = 2631270417U,
			// Token: 0x0400518A RID: 20874
			CanEnableLocalCopyState_Invalid = 1468404604U,
			// Token: 0x0400518B RID: 20875
			MailEnabledUniversalDistributionGroupRecipientType = 2146247679U,
			// Token: 0x0400518C RID: 20876
			ReceiveCredentialIsNull = 893817173U,
			// Token: 0x0400518D RID: 20877
			EsnLangLithuanian = 1885276797U,
			// Token: 0x0400518E RID: 20878
			ServerRoleAll = 570563164U,
			// Token: 0x0400518F RID: 20879
			ServerRoleEdge = 756854696U,
			// Token: 0x04005190 RID: 20880
			ExceptionObjectStillExists = 982491582U,
			// Token: 0x04005191 RID: 20881
			AllRecipients = 387112589U,
			// Token: 0x04005192 RID: 20882
			LdapFilterErrorNoAttributeType = 3685369418U,
			// Token: 0x04005193 RID: 20883
			ServerRoleManagementFrontEnd = 3802186670U,
			// Token: 0x04005194 RID: 20884
			False = 2609910045U,
			// Token: 0x04005195 RID: 20885
			CalendarSharingFreeBusyLimitedDetails = 519619317U,
			// Token: 0x04005196 RID: 20886
			SystemAttendantMailboxRecipientType = 2662725163U,
			// Token: 0x04005197 RID: 20887
			ServerRoleManagementBackEnd = 696678862U,
			// Token: 0x04005198 RID: 20888
			GroupNamingPolicyStateOrProvince = 4088287609U,
			// Token: 0x04005199 RID: 20889
			IndustryFinance = 1955666018U,
			// Token: 0x0400519A RID: 20890
			ErrorAgeLimitExpiration = 3313162693U,
			// Token: 0x0400519B RID: 20891
			InboundConnectorMissingTlsCertificateOrSenderIP = 2067616247U,
			// Token: 0x0400519C RID: 20892
			ErrorMailTipTranslationFormatIncorrect = 1247338605U,
			// Token: 0x0400519D RID: 20893
			MountDialOverrideGoodAvailability = 3892578519U,
			// Token: 0x0400519E RID: 20894
			ConfigReadScope = 2615196936U,
			// Token: 0x0400519F RID: 20895
			UserRecipientTypeDetails = 2338964630U,
			// Token: 0x040051A0 RID: 20896
			MeetingRequestMC = 3291024868U,
			// Token: 0x040051A1 RID: 20897
			Tag = 696030922U,
			// Token: 0x040051A2 RID: 20898
			MailFlowPartnerInternalMailContentTypeTNEF = 3607016823U,
			// Token: 0x040051A3 RID: 20899
			SerialNumberMissing = 1793427927U,
			// Token: 0x040051A4 RID: 20900
			AttributeNameNull = 2193656120U,
			// Token: 0x040051A5 RID: 20901
			ErrorIsDehydratedSetOnNonTinyTenant = 1260468432U,
			// Token: 0x040051A6 RID: 20902
			TUIPromptEditingEnabled = 4069918469U,
			// Token: 0x040051A7 RID: 20903
			StarAcceptedDomainCannotBeInitialDomain = 885421749U,
			// Token: 0x040051A8 RID: 20904
			LdapFilterErrorNotSupportSingleComp = 1860494422U,
			// Token: 0x040051A9 RID: 20905
			UseTnef = 1191236736U,
			// Token: 0x040051AA RID: 20906
			AttachmentFilterEntryInvalid = 654334258U,
			// Token: 0x040051AB RID: 20907
			Exchange2013 = 599002007U,
			// Token: 0x040051AC RID: 20908
			SendAuthMechanismBasicAuthPlusTls = 3145869218U,
			// Token: 0x040051AD RID: 20909
			MoveToDeletedItems = 3341243277U,
			// Token: 0x040051AE RID: 20910
			TCP = 2403277311U,
			// Token: 0x040051AF RID: 20911
			DocumentMC = 2664643149U,
			// Token: 0x040051B0 RID: 20912
			ErrorCannotSetWindowsEmailAddress = 1531250846U,
			// Token: 0x040051B1 RID: 20913
			Msn = 3115737588U,
			// Token: 0x040051B2 RID: 20914
			MessageRateSourceFlagsIPAddress = 619725344U,
			// Token: 0x040051B3 RID: 20915
			ErrorTextMessageIncludingAppleAttachment = 4163650158U,
			// Token: 0x040051B4 RID: 20916
			ForwardCallsToDefaultMailbox = 3857647582U,
			// Token: 0x040051B5 RID: 20917
			RoleGroupTypeDetails = 3221974997U,
			// Token: 0x040051B6 RID: 20918
			MailEnabledContactRecipientTypeDetails = 3815678973U,
			// Token: 0x040051B7 RID: 20919
			EsnLangEnglish = 647747116U,
			// Token: 0x040051B8 RID: 20920
			EsnLangMarathi = 2250496622U,
			// Token: 0x040051B9 RID: 20921
			SpecifyAnnouncementFileName = 1092312607U,
			// Token: 0x040051BA RID: 20922
			GroupNamingPolicyCustomAttribute12 = 857277173U,
			// Token: 0x040051BB RID: 20923
			SystemAddressListDoesNotExist = 3759553744U,
			// Token: 0x040051BC RID: 20924
			DefaultOabName = 3499307032U,
			// Token: 0x040051BD RID: 20925
			EsnLangSpanish = 1855185352U,
			// Token: 0x040051BE RID: 20926
			FederatedOrganizationIdNoNamespaceAccount = 3227102809U,
			// Token: 0x040051BF RID: 20927
			RemoteEquipmentMailboxTypeDetails = 1406382714U,
			// Token: 0x040051C0 RID: 20928
			SpamFilteringOptionOn = 2654331961U,
			// Token: 0x040051C1 RID: 20929
			ErrorNoSharedConfigurationInfo = 1495966060U,
			// Token: 0x040051C2 RID: 20930
			EquipmentMailboxRecipientTypeDetails = 3938481035U,
			// Token: 0x040051C3 RID: 20931
			ErrorCannotSetMoveToDestinationFolder = 375049999U,
			// Token: 0x040051C4 RID: 20932
			CapabilityTOUSigned = 290262264U,
			// Token: 0x040051C5 RID: 20933
			ServerRoleExtendedRole2 = 3707194054U,
			// Token: 0x040051C6 RID: 20934
			ServerRoleExtendedRole3 = 3707194053U,
			// Token: 0x040051C7 RID: 20935
			PersonalFolder = 2283186478U,
			// Token: 0x040051C8 RID: 20936
			CapabilityNone = 584737882U,
			// Token: 0x040051C9 RID: 20937
			ErrorEmptyResourceTypeofResourceMailbox = 31546440U,
			// Token: 0x040051CA RID: 20938
			InternalDNSServersNotSet = 2469247251U,
			// Token: 0x040051CB RID: 20939
			ExceptionImpersonation = 4205089983U,
			// Token: 0x040051CC RID: 20940
			ReceiveAuthMechanismNone = 3072026616U,
			// Token: 0x040051CD RID: 20941
			GroupNamingPolicyCustomAttribute9 = 1924734199U,
			// Token: 0x040051CE RID: 20942
			MailEnabledDynamicDistributionGroupRecipientTypeDetails = 2999125469U,
			// Token: 0x040051CF RID: 20943
			SpamFilteringActionAddXHeader = 685401583U,
			// Token: 0x040051D0 RID: 20944
			RecentCommands = 141120823U,
			// Token: 0x040051D1 RID: 20945
			SecurityPrincipalTypeNone = 2690725740U,
			// Token: 0x040051D2 RID: 20946
			MailboxMoveStatusNone = 1589279983U,
			// Token: 0x040051D3 RID: 20947
			LocalForest = 3976915092U,
			// Token: 0x040051D4 RID: 20948
			LegacyMailboxRecipientTypeDetails = 221683052U,
			// Token: 0x040051D5 RID: 20949
			GroupNamingPolicyCustomAttribute2 = 1924734194U,
			// Token: 0x040051D6 RID: 20950
			DatabaseMasterTypeUnknown = 661425765U,
			// Token: 0x040051D7 RID: 20951
			ConversationHistory = 2630084427U,
			// Token: 0x040051D8 RID: 20952
			OutboundConnectorTlsSettingsInvalidDomainValidationWithoutTlsDomain = 2338066360U,
			// Token: 0x040051D9 RID: 20953
			WhenMoved = 282367765U,
			// Token: 0x040051DA RID: 20954
			ErrorDuplicateLanguage = 1039356237U,
			// Token: 0x040051DB RID: 20955
			ExceptionObjectAlreadyExists = 1268762784U,
			// Token: 0x040051DC RID: 20956
			EsnLangCzech = 908880307U,
			// Token: 0x040051DD RID: 20957
			ComponentNameInvalid = 3859838333U,
			// Token: 0x040051DE RID: 20958
			ErrorAuthMetadataCannotResolveIssuer = 3587044099U,
			// Token: 0x040051DF RID: 20959
			GroupNamingPolicyTitle = 4137211921U,
			// Token: 0x040051E0 RID: 20960
			MailboxMoveStatusSuspended = 3738926850U,
			// Token: 0x040051E1 RID: 20961
			DomainSecureEnabledWithExternalAuthoritative = 1795765194U,
			// Token: 0x040051E2 RID: 20962
			BasicAfterTLSWithoutTLS = 4014391034U,
			// Token: 0x040051E3 RID: 20963
			Private = 3026477473U,
			// Token: 0x040051E4 RID: 20964
			Mailboxes = 1481245394U,
			// Token: 0x040051E5 RID: 20965
			ErrorModeratorRequiredForModeration = 1548074671U,
			// Token: 0x040051E6 RID: 20966
			CustomFromAddressRequired = 51460946U,
			// Token: 0x040051E7 RID: 20967
			LdapModifyDN = 1951705699U,
			// Token: 0x040051E8 RID: 20968
			CustomExternalSubjectRequired = 2453785463U,
			// Token: 0x040051E9 RID: 20969
			ErrorInternalLocationsCountMissMatch = 2119492277U,
			// Token: 0x040051EA RID: 20970
			ASOnlyOneAuthenticationMethodAllowed = 2620688997U,
			// Token: 0x040051EB RID: 20971
			Tnef = 4001967317U,
			// Token: 0x040051EC RID: 20972
			ByteEncoderTypeUseBase64HtmlDetectTextPlain = 2746045715U,
			// Token: 0x040051ED RID: 20973
			EsnLangIcelandic = 1347571030U,
			// Token: 0x040051EE RID: 20974
			ServerRoleNAT = 2324863376U,
			// Token: 0x040051EF RID: 20975
			UniversalDistributionGroupRecipientTypeDetails = 1966081841U,
			// Token: 0x040051F0 RID: 20976
			ErrorReplicationLatency = 2222835032U,
			// Token: 0x040051F1 RID: 20977
			EnabledPartner = 461546579U,
			// Token: 0x040051F2 RID: 20978
			OutboundConnectorSmarthostTlsSettingsInvalid = 1942443133U,
			// Token: 0x040051F3 RID: 20979
			ExternalCompliance = 1538151754U,
			// Token: 0x040051F4 RID: 20980
			ErrorAuthMetadataNoSigningKey = 1293877238U,
			// Token: 0x040051F5 RID: 20981
			InboundConnectorIncorrectAllAcceptedDomains = 2521212798U,
			// Token: 0x040051F6 RID: 20982
			MoveToFolder = 1182470434U,
			// Token: 0x040051F7 RID: 20983
			Byte = 1421152560U,
			// Token: 0x040051F8 RID: 20984
			EsnLangCyrillic = 2557668279U,
			// Token: 0x040051F9 RID: 20985
			CanRunDefaultUpdateState_Invalid = 1711185212U,
			// Token: 0x040051FA RID: 20986
			DisabledUserRecipientTypeDetails = 3569405894U,
			// Token: 0x040051FB RID: 20987
			InvalidRecipientType = 725514504U,
			// Token: 0x040051FC RID: 20988
			EmailAgeFilterThreeDays = 532726102U,
			// Token: 0x040051FD RID: 20989
			DataMoveReplicationConstraintCISecondCopy = 3099081087U,
			// Token: 0x040051FE RID: 20990
			ErrorMissingPrimarySmtp = 1499716658U,
			// Token: 0x040051FF RID: 20991
			ErrorELCFolderNotSpecified = 398140363U,
			// Token: 0x04005200 RID: 20992
			ErrorCannotHaveMoreThanOneDefaultThrottlingPolicy = 4174419723U,
			// Token: 0x04005201 RID: 20993
			ReceiveModeCannotBeZero = 4070703744U,
			// Token: 0x04005202 RID: 20994
			OwaDefaultDomainRequiredWhenLogonFormatIsUserName = 3248373105U,
			// Token: 0x04005203 RID: 20995
			TLS = 2806561839U,
			// Token: 0x04005204 RID: 20996
			LinkedMailboxRecipientTypeDetails = 1432667858U,
			// Token: 0x04005205 RID: 20997
			Tasks = 2966158940U,
			// Token: 0x04005206 RID: 20998
			RejectAndQuarantineThreshold = 1982036425U,
			// Token: 0x04005207 RID: 20999
			LdapFilterErrorInvalidDecimal = 1213745183U,
			// Token: 0x04005208 RID: 21000
			SpamFilteringTestActionAddXHeader = 2461262221U,
			// Token: 0x04005209 RID: 21001
			OrganizationCapabilityScaleOut = 1880194541U,
			// Token: 0x0400520A RID: 21002
			ConstraintViolationOneOffSupervisionListEntryStringPartIsInvalid = 3454173033U,
			// Token: 0x0400520B RID: 21003
			DiscoveryMailboxTypeDetails = 104454802U,
			// Token: 0x0400520C RID: 21004
			ErrorAdfsTrustedIssuers = 826243425U,
			// Token: 0x0400520D RID: 21005
			DataMoveReplicationConstraintCIAllDatacenters = 2167266391U,
			// Token: 0x0400520E RID: 21006
			HygieneSuiteStandard = 396559062U,
			// Token: 0x0400520F RID: 21007
			EsnLangHindi = 291819084U,
			// Token: 0x04005210 RID: 21008
			ExceptionUnableToCreateConnections = 708782482U,
			// Token: 0x04005211 RID: 21009
			SecurityPrincipalTypeWellknownSecurityPrincipal = 796260281U,
			// Token: 0x04005212 RID: 21010
			Error = 22442200U,
			// Token: 0x04005213 RID: 21011
			ElcScheduleOnWrongServer = 1999329050U,
			// Token: 0x04005214 RID: 21012
			SyncIssues = 3694564633U,
			// Token: 0x04005215 RID: 21013
			PartiallyApplied = 3184119847U,
			// Token: 0x04005216 RID: 21014
			PreferredInternetCodePageUndefined = 361358848U,
			// Token: 0x04005217 RID: 21015
			NoRoleEntriesCmdletOrScriptFound = 3399683424U,
			// Token: 0x04005218 RID: 21016
			CannotDeserializePartitionHintTooShort = 2126631961U,
			// Token: 0x04005219 RID: 21017
			InvalidReceiveAuthModeTLSPassword = 2412912437U,
			// Token: 0x0400521A RID: 21018
			GroupNamingPolicyCustomAttribute8 = 1924734200U,
			// Token: 0x0400521B RID: 21019
			EsnLangSwedish = 401605495U,
			// Token: 0x0400521C RID: 21020
			IndustryUtilities = 2253071944U,
			// Token: 0x0400521D RID: 21021
			G711 = 3692015522U,
			// Token: 0x0400521E RID: 21022
			ExternalDNSServersNotSet = 3044377029U,
			// Token: 0x0400521F RID: 21023
			Item = 3168546709U,
			// Token: 0x04005220 RID: 21024
			LdapFilterErrorUnsupportedAttributeType = 3325431492U,
			// Token: 0x04005221 RID: 21025
			ExternalSenderAdminAddressRequired = 2932678028U,
			// Token: 0x04005222 RID: 21026
			ErrorBadLocalizedFolderName = 582855761U,
			// Token: 0x04005223 RID: 21027
			AutoDatabaseMountDialBestAvailability = 3297182182U,
			// Token: 0x04005224 RID: 21028
			OrganizationalFolder = 391848840U,
			// Token: 0x04005225 RID: 21029
			SpamFilteringOptionTest = 2944126402U,
			// Token: 0x04005226 RID: 21030
			LdapFilterErrorInvalidToken = 56024811U,
			// Token: 0x04005227 RID: 21031
			MessageRateSourceFlagsUser = 2570241570U,
			// Token: 0x04005228 RID: 21032
			TextEnrichedAndTextAlternative = 1461717404U,
			// Token: 0x04005229 RID: 21033
			FederatedOrganizationIdNoFederatedDomains = 3920082026U,
			// Token: 0x0400522A RID: 21034
			GroupTypeFlagsUniversal = 1191186633U,
			// Token: 0x0400522B RID: 21035
			CustomAlertTextRequired = 3774252481U,
			// Token: 0x0400522C RID: 21036
			EsnLangEstonian = 1272682565U,
			// Token: 0x0400522D RID: 21037
			Low = 1502599728U,
			// Token: 0x0400522E RID: 21038
			IndustryPersonalServices = 467677052U,
			// Token: 0x0400522F RID: 21039
			ErrorInvalidPipelineTracingSenderAddress = 3282711248U,
			// Token: 0x04005230 RID: 21040
			AccessQuarantined = 131691172U,
			// Token: 0x04005231 RID: 21041
			LdapFilterErrorTypeOnlySpaces = 3874249800U,
			// Token: 0x04005232 RID: 21042
			UserFilterChoice = 2878385120U,
			// Token: 0x04005233 RID: 21043
			ErrorRemovePrimaryExternalSMTPAddress = 587065991U,
			// Token: 0x04005234 RID: 21044
			GroupNamingPolicyOffice = 62599113U,
			// Token: 0x04005235 RID: 21045
			ErrorHostServerNotSet = 3503686282U,
			// Token: 0x04005236 RID: 21046
			BitMaskOrIpAddressMatchMustBeSet = 1998007652U,
			// Token: 0x04005237 RID: 21047
			OrganizationCapabilityGMGen = 621310157U,
			// Token: 0x04005238 RID: 21048
			ErrorArchiveDatabaseArchiveDomainConflict = 3723962467U,
			// Token: 0x04005239 RID: 21049
			ArchiveStateHostedProvisioned = 2472951404U,
			// Token: 0x0400523A RID: 21050
			InvalidHttpProtocolLogSizeConfiguration = 614706510U,
			// Token: 0x0400523B RID: 21051
			PermanentMservErrorDescription = 3225083443U,
			// Token: 0x0400523C RID: 21052
			CustomExternalBodyRequired = 1285289871U,
			// Token: 0x0400523D RID: 21053
			LdapFilterErrorUndefinedAttributeType = 1415475463U,
			// Token: 0x0400523E RID: 21054
			ErrorTextMessageIncludingHtmlBody = 603975640U,
			// Token: 0x0400523F RID: 21055
			WellKnownRecipientTypeResources = 3773054995U,
			// Token: 0x04005240 RID: 21056
			PrimaryDefault = 2097957443U,
			// Token: 0x04005241 RID: 21057
			MailFlowPartnerInternalMailContentTypeMimeHtmlText = 2943465798U,
			// Token: 0x04005242 RID: 21058
			DataMoveReplicationConstraintNone = 2685207586U,
			// Token: 0x04005243 RID: 21059
			ErrorAdfsAudienceUris = 3040710945U,
			// Token: 0x04005244 RID: 21060
			InvalidAnrFilter = 1190928622U,
			// Token: 0x04005245 RID: 21061
			AuditLogMailboxRecipientTypeDetails = 117943812U,
			// Token: 0x04005246 RID: 21062
			WellKnownRecipientTypeNone = 1849540794U,
			// Token: 0x04005247 RID: 21063
			EsnLangGujarati = 3296665743U,
			// Token: 0x04005248 RID: 21064
			DomainStateUnknown = 672991527U,
			// Token: 0x04005249 RID: 21065
			IndustryManufacturing = 687591962U,
			// Token: 0x0400524A RID: 21066
			IndustryHospitality = 345329738U,
			// Token: 0x0400524B RID: 21067
			ErrorAdfsIssuer = 534671299U,
			// Token: 0x0400524C RID: 21068
			EmailAgeFilterOneDay = 1058308747U,
			// Token: 0x0400524D RID: 21069
			AllEmailMC = 3471478219U,
			// Token: 0x0400524E RID: 21070
			OrgContainerAmbiguousException = 1753080574U,
			// Token: 0x0400524F RID: 21071
			GlobalThrottlingPolicyNotFoundException = 1439034128U,
			// Token: 0x04005250 RID: 21072
			EsnLangTurkish = 3636659332U,
			// Token: 0x04005251 RID: 21073
			SKUCapabilityBPOSSLite = 1642025802U,
			// Token: 0x04005252 RID: 21074
			RecipientWriteScopes = 2794974035U,
			// Token: 0x04005253 RID: 21075
			CalendarAgeFilterThreeMonths = 104189932U,
			// Token: 0x04005254 RID: 21076
			MailboxMoveStatusCompletedWithWarning = 2669194754U,
			// Token: 0x04005255 RID: 21077
			GroupNamingPolicyCountryOrRegion = 3674978674U,
			// Token: 0x04005256 RID: 21078
			EsnLangFrench = 2482287296U,
			// Token: 0x04005257 RID: 21079
			CapabilityExcludedFromBackSync = 2050489682U,
			// Token: 0x04005258 RID: 21080
			CapabilityBEVDirLockdown = 452620031U,
			// Token: 0x04005259 RID: 21081
			ReceiveAuthMechanismBasicAuth = 4409738U,
			// Token: 0x0400525A RID: 21082
			IndustryEducation = 2046074250U,
			// Token: 0x0400525B RID: 21083
			NotSpecified = 2536752615U,
			// Token: 0x0400525C RID: 21084
			PermanentlyDelete = 3675904764U,
			// Token: 0x0400525D RID: 21085
			FederatedIdentityMisconfigured = 2346580185U,
			// Token: 0x0400525E RID: 21086
			MountDialOverrideNone = 3845937663U,
			// Token: 0x0400525F RID: 21087
			AlwaysUTF8 = 3563162252U,
			// Token: 0x04005260 RID: 21088
			ExceptionPagedReaderIsSingleUse = 2660137110U,
			// Token: 0x04005261 RID: 21089
			InvalidFilterLength = 3323087513U,
			// Token: 0x04005262 RID: 21090
			MailboxMoveStatusSynced = 3664117547U,
			// Token: 0x04005263 RID: 21091
			SIPSecured = 2422734853U,
			// Token: 0x04005264 RID: 21092
			ErrorRejectedCookie = 630988704U,
			// Token: 0x04005265 RID: 21093
			ASInvalidProxyASUrlOption = 3800196293U,
			// Token: 0x04005266 RID: 21094
			ServerRoleSCOM = 407788899U,
			// Token: 0x04005267 RID: 21095
			JournalItemsMC = 3289792773U,
			// Token: 0x04005268 RID: 21096
			ErrorEmptySearchProperty = 2617145200U,
			// Token: 0x04005269 RID: 21097
			OutboundConnectorIncorrectTransportRuleScopedParameters = 3900005531U,
			// Token: 0x0400526A RID: 21098
			TeamMailboxRecipientTypeDetails = 107906018U,
			// Token: 0x0400526B RID: 21099
			CustomRoleDescription_MyMobileInformation = 4272675708U,
			// Token: 0x0400526C RID: 21100
			ArchiveStateHostedPending = 920444171U,
			// Token: 0x0400526D RID: 21101
			DPCantChangeName = 2153511661U,
			// Token: 0x0400526E RID: 21102
			OrganizationCapabilityUMDataStorage = 2175447826U,
			// Token: 0x0400526F RID: 21103
			TlsAuthLevelWithRequireTlsDisabled = 2304217557U,
			// Token: 0x04005270 RID: 21104
			UndefinedRecipientTypeDetails = 3453679227U,
			// Token: 0x04005271 RID: 21105
			Upgrade = 3608358242U,
			// Token: 0x04005272 RID: 21106
			Global = 3905558735U,
			// Token: 0x04005273 RID: 21107
			DeleteMessage = 4264103832U,
			// Token: 0x04005274 RID: 21108
			LdapDelete = 4018720312U,
			// Token: 0x04005275 RID: 21109
			EsnLangHungarian = 1291341805U,
			// Token: 0x04005276 RID: 21110
			ErrorAddressAutoCopy = 699909606U,
			// Token: 0x04005277 RID: 21111
			EsnLangLatvian = 2968709845U,
			// Token: 0x04005278 RID: 21112
			CanRunDefaultUpdateState_NotLocal = 674244647U,
			// Token: 0x04005279 RID: 21113
			Department = 1855823700U,
			// Token: 0x0400527A RID: 21114
			SpamFilteringActionJmf = 1123996746U,
			// Token: 0x0400527B RID: 21115
			ErrorDDLOperationsError = 1079159280U,
			// Token: 0x0400527C RID: 21116
			ErrorSharedConfigurationCannotBeEnabled = 64564864U,
			// Token: 0x0400527D RID: 21117
			ErrorMailTipCultureNotSpecified = 1411554219U,
			// Token: 0x0400527E RID: 21118
			LdapModify = 2068838733U,
			// Token: 0x0400527F RID: 21119
			DataMoveReplicationConstraintSecondDatacenter = 2212942115U,
			// Token: 0x04005280 RID: 21120
			CapabilityResourceMailbox = 3501307892U,
			// Token: 0x04005281 RID: 21121
			Second = 2955006930U,
			// Token: 0x04005282 RID: 21122
			InboundConnectorInvalidRestrictDomainsToCertificate = 451948526U,
			// Token: 0x04005283 RID: 21123
			GroupNamingPolicyCustomAttribute15 = 97762286U,
			// Token: 0x04005284 RID: 21124
			SendAuthMechanismNone = 1669305113U,
			// Token: 0x04005285 RID: 21125
			ServicesContainerNotFound = 2028679986U,
			// Token: 0x04005286 RID: 21126
			MissingDefaultOutboundCallingLineId = 368981658U,
			// Token: 0x04005287 RID: 21127
			GroupTypeFlagsDomainLocal = 1638178773U,
			// Token: 0x04005288 RID: 21128
			ErrorCannotAggregateAndLinkMailbox = 2292597411U,
			// Token: 0x04005289 RID: 21129
			SyncCommands = 1975373491U,
			// Token: 0x0400528A RID: 21130
			PreferredInternetCodePageEsc2022Jp = 2584752109U,
			// Token: 0x0400528B RID: 21131
			DirectoryBasedEdgeBlockModeOff = 2869997774U,
			// Token: 0x0400528C RID: 21132
			InvalidSourceAddressSetting = 3205211544U,
			// Token: 0x0400528D RID: 21133
			ElcContentSettingsDescription = 3459813102U,
			// Token: 0x0400528E RID: 21134
			ServerRoleUnifiedMessaging = 3194934827U,
			// Token: 0x0400528F RID: 21135
			DataMoveReplicationConstraintCIAllCopies = 1193235970U,
			// Token: 0x04005290 RID: 21136
			MailTipsAccessLevelLimited = 96477845U,
			// Token: 0x04005291 RID: 21137
			SecondaryMailboxRelationType = 4229158936U,
			// Token: 0x04005292 RID: 21138
			Ocs = 3828198519U,
			// Token: 0x04005293 RID: 21139
			IndustryOther = 2122644134U,
			// Token: 0x04005294 RID: 21140
			ErrorMimeMessageIncludingUuEncodedAttachment = 3032910929U,
			// Token: 0x04005295 RID: 21141
			ServerRoleDHCP = 1886413222U,
			// Token: 0x04005296 RID: 21142
			GroupNamingPolicyCustomAttribute5 = 1924734195U,
			// Token: 0x04005297 RID: 21143
			EnableNotificationEmail = 102260678U,
			// Token: 0x04005298 RID: 21144
			GroupNamingPolicyCountryCode = 4022404286U,
			// Token: 0x04005299 RID: 21145
			MailboxMoveStatusCompleted = 4204248234U,
			// Token: 0x0400529A RID: 21146
			IndustryCommunications = 2831291713U,
			// Token: 0x0400529B RID: 21147
			LdapFilterErrorNoValidComparison = 2559242555U,
			// Token: 0x0400529C RID: 21148
			RssSubscriptions = 3598244064U,
			// Token: 0x0400529D RID: 21149
			EsnLangThai = 1071018894U,
			// Token: 0x0400529E RID: 21150
			ErrorDDLFilterMissing = 2921549042U,
			// Token: 0x0400529F RID: 21151
			ExtendedProtectionNonTlsTerminatingProxyScenarioRequireTls = 1447606358U,
			// Token: 0x040052A0 RID: 21152
			NoResetOrAssignedMvp = 267717298U,
			// Token: 0x040052A1 RID: 21153
			MountDialOverrideBestEffort = 663506969U,
			// Token: 0x040052A2 RID: 21154
			NoComputers = 2367428005U,
			// Token: 0x040052A3 RID: 21155
			RegistryContentTypeException = 665042539U,
			// Token: 0x040052A4 RID: 21156
			DataMoveReplicationConstraintAllDatacenters = 366040629U,
			// Token: 0x040052A5 RID: 21157
			ExceptionObjectNotFound = 2564080149U,
			// Token: 0x040052A6 RID: 21158
			DomainStateCustomProvision = 882963645U,
			// Token: 0x040052A7 RID: 21159
			SKUCapabilityBPOSMidSize = 3517179940U,
			// Token: 0x040052A8 RID: 21160
			LdapFilterErrorUnsupportedOperand = 1117900463U,
			// Token: 0x040052A9 RID: 21161
			DirectoryBasedEdgeBlockModeDefault = 483196058U,
			// Token: 0x040052AA RID: 21162
			ErrorWrongTypeParameter = 113073592U,
			// Token: 0x040052AB RID: 21163
			EsnLangCatalan = 2757326190U,
			// Token: 0x040052AC RID: 21164
			InvalidSndProtocolLogSizeConfiguration = 3980183679U,
			// Token: 0x040052AD RID: 21165
			GroupNamingPolicyCustomAttribute13 = 3586160528U,
			// Token: 0x040052AE RID: 21166
			ErrorThrottlingPolicyGlobalAndOrganizationScope = 1960526324U,
			// Token: 0x040052AF RID: 21167
			SMTPAddress = 980672066U,
			// Token: 0x040052B0 RID: 21168
			EsnLangPolish = 3976700013U,
			// Token: 0x040052B1 RID: 21169
			CanEnableLocalCopyState_DatabaseEnabled = 1017523965U,
			// Token: 0x040052B2 RID: 21170
			EsnLangRomanian = 3315201717U,
			// Token: 0x040052B3 RID: 21171
			ExternalManagedGroupTypeDetails = 1097129869U,
			// Token: 0x040052B4 RID: 21172
			DatabaseMasterTypeDag = 2773964607U,
			// Token: 0x040052B5 RID: 21173
			GroupNamingPolicyExtensionCustomAttribute3 = 3197896354U,
			// Token: 0x040052B6 RID: 21174
			ExchangeConfigurationContainerNotFoundException = 3071618850U,
			// Token: 0x040052B7 RID: 21175
			EsnLangUrdu = 170342216U,
			// Token: 0x040052B8 RID: 21176
			MservAndMbxExclusive = 579329341U,
			// Token: 0x040052B9 RID: 21177
			FirstLast = 2300412432U,
			// Token: 0x040052BA RID: 21178
			EsnLangBulgarian = 2228665429U,
			// Token: 0x040052BB RID: 21179
			MailEnabledUniversalSecurityGroupRecipientTypeDetails = 1970247521U,
			// Token: 0x040052BC RID: 21180
			ErrorTimeoutReadingSystemAddressListMemberCount = 3065017355U,
			// Token: 0x040052BD RID: 21181
			FaxServerURINoValue = 3411768540U,
			// Token: 0x040052BE RID: 21182
			ErrorDefaultThrottlingPolicyNotFound = 1118762177U,
			// Token: 0x040052BF RID: 21183
			ErrorRecipientContainerCanNotNull = 3372089172U,
			// Token: 0x040052C0 RID: 21184
			MoveToArchive = 2835967712U,
			// Token: 0x040052C1 RID: 21185
			ModifySubjectValueNotSet = 667068758U,
			// Token: 0x040052C2 RID: 21186
			NotLocalMaiboxException = 882536335U,
			// Token: 0x040052C3 RID: 21187
			RecipientReadScope = 3811978523U,
			// Token: 0x040052C4 RID: 21188
			Organizational = 1067650092U,
			// Token: 0x040052C5 RID: 21189
			SystemAttendantMailboxRecipientTypeDetails = 1818643265U,
			// Token: 0x040052C6 RID: 21190
			OrganizationCapabilityOABGen = 114732651U,
			// Token: 0x040052C7 RID: 21191
			StarOutToDialPlanEnabled = 2360810543U,
			// Token: 0x040052C8 RID: 21192
			AuthenticationCredentialNotSet = 3673152204U,
			// Token: 0x040052C9 RID: 21193
			NotifyOutboundSpamRecipientsRequired = 1776441609U,
			// Token: 0x040052CA RID: 21194
			JunkEmail = 2241039844U,
			// Token: 0x040052CB RID: 21195
			LdapFilterErrorValueOnlySpaces = 2270844793U,
			// Token: 0x040052CC RID: 21196
			SipName = 3423767853U,
			// Token: 0x040052CD RID: 21197
			EsnLangMalayalam = 24965481U,
			// Token: 0x040052CE RID: 21198
			SpamFilteringActionModifySubject = 2349327181U,
			// Token: 0x040052CF RID: 21199
			XHeaderValueNotSet = 1153697179U,
			// Token: 0x040052D0 RID: 21200
			DeletedItems = 3613623199U,
			// Token: 0x040052D1 RID: 21201
			OrganizationCapabilityUMGrammarReady = 3387472355U,
			// Token: 0x040052D2 RID: 21202
			LastFirst = 142823596U,
			// Token: 0x040052D3 RID: 21203
			SendAuthMechanismExchangeServer = 2055652669U,
			// Token: 0x040052D4 RID: 21204
			RemoteTeamMailboxRecipientTypeDetails = 322963092U,
			// Token: 0x040052D5 RID: 21205
			OutOfBudgets = 1068346025U,
			// Token: 0x040052D6 RID: 21206
			Off = 3424913979U,
			// Token: 0x040052D7 RID: 21207
			GroupTypeFlagsSecurityEnabled = 3200416695U,
			// Token: 0x040052D8 RID: 21208
			InvalidCookieException = 2618688392U,
			// Token: 0x040052D9 RID: 21209
			UserLanguageChoice = 122679092U,
			// Token: 0x040052DA RID: 21210
			SpamFilteringTestActionBccMessage = 1291237470U,
			// Token: 0x040052DB RID: 21211
			DelayCacheFull = 1118847720U,
			// Token: 0x040052DC RID: 21212
			ErrorAutoCopyMessageFormat = 1149691394U,
			// Token: 0x040052DD RID: 21213
			Reserved3 = 1173768531U,
			// Token: 0x040052DE RID: 21214
			HtmlOnly = 2523055253U,
			// Token: 0x040052DF RID: 21215
			DefaultFolder = 285356425U,
			// Token: 0x040052E0 RID: 21216
			PublicFolderMailboxRecipientTypeDetails = 1487832074U,
			// Token: 0x040052E1 RID: 21217
			Mp3 = 1549653732U,
			// Token: 0x040052E2 RID: 21218
			FederatedOrganizationIdNotEnabled = 2737500906U,
			// Token: 0x040052E3 RID: 21219
			EsnLangVietnamese = 3082202919U,
			// Token: 0x040052E4 RID: 21220
			AccessGranted = 2532765903U,
			// Token: 0x040052E5 RID: 21221
			MailboxUserRecipientType = 4281433724U,
			// Token: 0x040052E6 RID: 21222
			ExceptionNoSchemaContainerObject = 1183009861U,
			// Token: 0x040052E7 RID: 21223
			TargetDeliveryDomainCannotBeStar = 2078410195U,
			// Token: 0x040052E8 RID: 21224
			ErrorAuthMetadataCannotResolveServiceName = 2402032744U,
			// Token: 0x040052E9 RID: 21225
			ByteEncoderTypeUseBase64 = 4046275528U,
			// Token: 0x040052EA RID: 21226
			BackSyncDataSourceReplicationErrorMessage = 2114338030U,
			// Token: 0x040052EB RID: 21227
			EsnLangHebrew = 2327110479U,
			// Token: 0x040052EC RID: 21228
			WellKnownRecipientTypeAllRecipients = 2099880135U,
			// Token: 0x040052ED RID: 21229
			ExceptionCredentialsNotSupportedWithoutDC = 33168083U,
			// Token: 0x040052EE RID: 21230
			NoneMailboxRelationType = 247236896U,
			// Token: 0x040052EF RID: 21231
			MailboxUserRecipientTypeDetails = 1605633982U,
			// Token: 0x040052F0 RID: 21232
			SpamFilteringActionDelete = 3918345138U,
			// Token: 0x040052F1 RID: 21233
			FederatedOrganizationIdNotFound = 1574700905U,
			// Token: 0x040052F2 RID: 21234
			SKUCapabilityBPOSSArchive = 3777672192U,
			// Token: 0x040052F3 RID: 21235
			ReceiveAuthMechanismIntegrated = 1038035039U,
			// Token: 0x040052F4 RID: 21236
			NameLookupEnabled = 1607061032U,
			// Token: 0x040052F5 RID: 21237
			ForceFilter = 1972085753U,
			// Token: 0x040052F6 RID: 21238
			OrganizationCapabilityOfficeMessageEncryption = 3515139435U,
			// Token: 0x040052F7 RID: 21239
			PreferredInternetCodePageIso2022Jp = 906595693U,
			// Token: 0x040052F8 RID: 21240
			AlternateServiceAccountCredentialIsInvalid = 1412620754U,
			// Token: 0x040052F9 RID: 21241
			EmailAgeFilterTwoWeeks = 1859518684U,
			// Token: 0x040052FA RID: 21242
			DeviceOS = 2080073494U,
			// Token: 0x040052FB RID: 21243
			ErrorTenantRelocationsAllowedOnlyForRootOrg = 1451782196U,
			// Token: 0x040052FC RID: 21244
			OrganizationCapabilityTenantUpgrade = 3809750167U,
			// Token: 0x040052FD RID: 21245
			StarTlsDomainCapabilitiesNotAllowed = 426414486U,
			// Token: 0x040052FE RID: 21246
			GroupNamingPolicyExtensionCustomAttribute5 = 2391327300U,
			// Token: 0x040052FF RID: 21247
			ErrorTimeoutWritingSystemAddressListCache = 1103339046U,
			// Token: 0x04005300 RID: 21248
			CannotGetLocalSite = 2249628033U,
			// Token: 0x04005301 RID: 21249
			DatabaseCopyAutoActivationPolicyUnrestricted = 3080481085U,
			// Token: 0x04005302 RID: 21250
			PrivateComputersOnly = 3562221485U,
			// Token: 0x04005303 RID: 21251
			Always = 887700241U,
			// Token: 0x04005304 RID: 21252
			WellKnownRecipientTypeMailUsers = 933193541U,
			// Token: 0x04005305 RID: 21253
			CannotSetZeroAsEapPriority = 3512186809U,
			// Token: 0x04005306 RID: 21254
			RootZone = 2442344752U,
			// Token: 0x04005307 RID: 21255
			RenameNotAllowed = 1151884593U,
			// Token: 0x04005308 RID: 21256
			Unknown = 2846264340U,
			// Token: 0x04005309 RID: 21257
			EsnLangItalian = 4013633336U,
			// Token: 0x0400530A RID: 21258
			ErrorDisplayNameInvalid = 2446612004U,
			// Token: 0x0400530B RID: 21259
			ConstraintViolationNotValidLegacyDN = 10930364U,
			// Token: 0x0400530C RID: 21260
			ReceiveExtendedProtectionPolicyRequire = 3650953906U,
			// Token: 0x0400530D RID: 21261
			SpamFilteringOptionOff = 2030161115U,
			// Token: 0x0400530E RID: 21262
			ExternallyManaged = 1656602441U,
			// Token: 0x0400530F RID: 21263
			RequireTLSWithoutTLS = 3909129905U,
			// Token: 0x04005310 RID: 21264
			ErrorCannotParseAuthMetadata = 1437476905U,
			// Token: 0x04005311 RID: 21265
			ErrorInvalidActivationPreference = 2411242862U,
			// Token: 0x04005312 RID: 21266
			CapabilityFederatedUser = 1499015349U,
			// Token: 0x04005313 RID: 21267
			EsnLangFilipino = 992862894U,
			// Token: 0x04005314 RID: 21268
			OutboundConnectorUseMXRecordShouldBeFalseIfSmartHostsIsPresent = 2687926967U,
			// Token: 0x04005315 RID: 21269
			LdapFilterErrorBracketMismatch = 1688256845U,
			// Token: 0x04005316 RID: 21270
			SipResourceIdentifierRequiredNotAllowed = 843851219U,
			// Token: 0x04005317 RID: 21271
			XMSWLHeader = 2178386640U,
			// Token: 0x04005318 RID: 21272
			ServerRoleCafe = 1536572748U,
			// Token: 0x04005319 RID: 21273
			DeleteAndRejectThreshold = 3319415544U,
			// Token: 0x0400531A RID: 21274
			Policy = 816661212U,
			// Token: 0x0400531B RID: 21275
			CanRunRestoreState_NotLocal = 2870485117U,
			// Token: 0x0400531C RID: 21276
			ElcAuditLogPathMissing = 2379521528U,
			// Token: 0x0400531D RID: 21277
			ClientCertAuthIgnore = 4221359213U,
			// Token: 0x0400531E RID: 21278
			Reserved2 = 1173768532U,
			// Token: 0x0400531F RID: 21279
			ConfigWriteScopes = 3743229054U,
			// Token: 0x04005320 RID: 21280
			DetailsTemplateCorrupted = 3856328942U,
			// Token: 0x04005321 RID: 21281
			ClientCertAuthAccepted = 1942592476U,
			// Token: 0x04005322 RID: 21282
			ExceptionAdminLimitExceeded = 1778180980U,
			// Token: 0x04005323 RID: 21283
			DataMoveReplicationConstraintSecondCopy = 970530017U,
			// Token: 0x04005324 RID: 21284
			ReceiveAuthMechanismTls = 3956092407U,
			// Token: 0x04005325 RID: 21285
			CannotFindTemplateTenant = 990840756U,
			// Token: 0x04005326 RID: 21286
			FailedToReadStoreUserInformation = 2189879122U,
			// Token: 0x04005327 RID: 21287
			MicrosoftExchangeRecipientTypeDetails = 2227674028U,
			// Token: 0x04005328 RID: 21288
			DataMoveReplicationConstraintCINoReplication = 1188824751U,
			// Token: 0x04005329 RID: 21289
			ErrorTransitionCounterHasZeroCount = 575137052U,
			// Token: 0x0400532A RID: 21290
			DeleteAndQuarantineThreshold = 73047031U,
			// Token: 0x0400532B RID: 21291
			IndustryAgriculture = 1642796619U,
			// Token: 0x0400532C RID: 21292
			ClientCertAuthRequired = 3603173284U,
			// Token: 0x0400532D RID: 21293
			ServerRoleExtendedRole7 = 3707194057U,
			// Token: 0x0400532E RID: 21294
			SubmissionOverrideListOnWrongServer = 2804536165U,
			// Token: 0x0400532F RID: 21295
			EsnLangBasque = 4170864973U,
			// Token: 0x04005330 RID: 21296
			UserRecipientType = 659240048U,
			// Token: 0x04005331 RID: 21297
			MailEnabledUserRecipientType = 363625972U,
			// Token: 0x04005332 RID: 21298
			GroupTypeFlagsGlobal = 4189167987U,
			// Token: 0x04005333 RID: 21299
			DataMoveReplicationConstraintCISecondDatacenter = 4091901749U,
			// Token: 0x04005334 RID: 21300
			LoadBalanceCannotUseBothInclusionLists = 4024084584U,
			// Token: 0x04005335 RID: 21301
			ExchangeMissedcallMC = 2771491650U,
			// Token: 0x04005336 RID: 21302
			RequesterNameInvalid = 3414623930U,
			// Token: 0x04005337 RID: 21303
			ByteEncoderTypeUseBase64Html7BitTextPlain = 3393062226U,
			// Token: 0x04005338 RID: 21304
			SecurityPrincipalTypeComputer = 2666751303U,
			// Token: 0x04005339 RID: 21305
			EsnLangAmharic = 3235051081U,
			// Token: 0x0400533A RID: 21306
			LimitedMoveOnlyAllowed = 3212819533U,
			// Token: 0x0400533B RID: 21307
			ASInvalidAuthenticationOptionsForAccessMethod = 2828547743U,
			// Token: 0x0400533C RID: 21308
			NullPasswordEncryptionKey = 1241097582U,
			// Token: 0x0400533D RID: 21309
			LinkedUserTypeDetails = 1738880682U,
			// Token: 0x0400533E RID: 21310
			AutoDatabaseMountDialLossless = 3409499789U,
			// Token: 0x0400533F RID: 21311
			ReceiveAuthMechanismExternalAuthoritative = 3238398976U,
			// Token: 0x04005340 RID: 21312
			ErrorTruncationLagTime = 1472430496U,
			// Token: 0x04005341 RID: 21313
			ExceptionIdImmutable = 1996912758U,
			// Token: 0x04005342 RID: 21314
			ExceptionDefaultScopeAndSearchRoot = 1638589089U,
			// Token: 0x04005343 RID: 21315
			ErrorOfferProgramIdMandatoryOnSharedConfig = 3667281372U,
			// Token: 0x04005344 RID: 21316
			ServerRoleExtendedRole4 = 3707194060U,
			// Token: 0x04005345 RID: 21317
			ErrorComment = 3615458703U,
			// Token: 0x04005346 RID: 21318
			ErrorReplayLagTime = 4244443796U,
			// Token: 0x04005347 RID: 21319
			ExLengthOfVersionByteArrayError = 64170653U,
			// Token: 0x04005348 RID: 21320
			LdapAdd = 3109296950U,
			// Token: 0x04005349 RID: 21321
			DomainStatePendingActivation = 1578973890U,
			// Token: 0x0400534A RID: 21322
			Uninterruptible = 3790383196U,
			// Token: 0x0400534B RID: 21323
			ErrorMustBeADRawEntry = 3789585333U,
			// Token: 0x0400534C RID: 21324
			None = 1414246128U,
			// Token: 0x0400534D RID: 21325
			ErrorBadLocalizedComment = 1049375487U,
			// Token: 0x0400534E RID: 21326
			EsnLangSlovak = 1223035494U,
			// Token: 0x0400534F RID: 21327
			LdapFilterErrorInvalidBooleanValue = 3378717027U,
			// Token: 0x04005350 RID: 21328
			OabVersionsNullException = 1343826401U,
			// Token: 0x04005351 RID: 21329
			Inbox = 2979702410U,
			// Token: 0x04005352 RID: 21330
			ContactRecipientTypeDetails = 137387861U,
			// Token: 0x04005353 RID: 21331
			EsnLangKazakh = 1522344710U,
			// Token: 0x04005354 RID: 21332
			DisableFilter = 1662145344U,
			// Token: 0x04005355 RID: 21333
			BluetoothHandsfreeOnly = 3713089550U,
			// Token: 0x04005356 RID: 21334
			GatewayGuid = 3178378607U,
			// Token: 0x04005357 RID: 21335
			CalendarSharingFreeBusyNone = 3927045149U
		}

		// Token: 0x02000A55 RID: 2645
		private enum ParamIDs
		{
			// Token: 0x04005359 RID: 21337
			ExceptionADWriteDisabled,
			// Token: 0x0400535A RID: 21338
			MobileAdOrphanFound,
			// Token: 0x0400535B RID: 21339
			ExceptionInvalidOperationOnReadOnlyObject,
			// Token: 0x0400535C RID: 21340
			ErrorUnsafeIdentityFilterNotAllowed,
			// Token: 0x0400535D RID: 21341
			ErrorProductFileDirectoryIdenticalWithCopyFileDirectory,
			// Token: 0x0400535E RID: 21342
			ErrorIsServerSuitableMissingDefaultNamingContext,
			// Token: 0x0400535F RID: 21343
			ErrorBothTargetAndSourceForestPopulated,
			// Token: 0x04005360 RID: 21344
			UnsupportedObjectClass,
			// Token: 0x04005361 RID: 21345
			DefaultAdministrativeGroupNotFoundException,
			// Token: 0x04005362 RID: 21346
			PropertyDependencyRequired,
			// Token: 0x04005363 RID: 21347
			ProviderFactoryClassNotFoundLoadException,
			// Token: 0x04005364 RID: 21348
			ConfigurationSettingsRestrictionSummary,
			// Token: 0x04005365 RID: 21349
			TenantNotFoundInGlsError,
			// Token: 0x04005366 RID: 21350
			InvalidCallSomeoneScopeSettings,
			// Token: 0x04005367 RID: 21351
			ConfigurationSettingsNotUnique,
			// Token: 0x04005368 RID: 21352
			KpkUseProblem,
			// Token: 0x04005369 RID: 21353
			ForwardingSmtpAddressNotValidSmtpAddress,
			// Token: 0x0400536A RID: 21354
			ConfigurationSettingsGroupNotFound,
			// Token: 0x0400536B RID: 21355
			MoreThanOneRecipientWithNetId,
			// Token: 0x0400536C RID: 21356
			CannotResolvePartitionFqdnError,
			// Token: 0x0400536D RID: 21357
			ErrorPublicFolderReferralConflict,
			// Token: 0x0400536E RID: 21358
			ExceptionADTopologyCreationTimeout,
			// Token: 0x0400536F RID: 21359
			ExceptionADOperationFailedEntryAlreadyExist,
			// Token: 0x04005370 RID: 21360
			ADTopologyEndpointNotFoundException,
			// Token: 0x04005371 RID: 21361
			AsyncTimeout,
			// Token: 0x04005372 RID: 21362
			ErrorNonUniqueExchangeGuid,
			// Token: 0x04005373 RID: 21363
			InvalidSyncObjectId,
			// Token: 0x04005374 RID: 21364
			AggregatedSessionCannotMakeADChanges,
			// Token: 0x04005375 RID: 21365
			ErrorInvalidRemoteRecipientType,
			// Token: 0x04005376 RID: 21366
			TenantIsRelocatedException,
			// Token: 0x04005377 RID: 21367
			ErrorSingletonMailboxLocationType,
			// Token: 0x04005378 RID: 21368
			DuplicateHolidaysError,
			// Token: 0x04005379 RID: 21369
			UnknownAccountForest,
			// Token: 0x0400537A RID: 21370
			ExchangeUpgradeBucketInvalidVersionFormat,
			// Token: 0x0400537B RID: 21371
			InvalidControlAttributeForTemplateType,
			// Token: 0x0400537C RID: 21372
			ExceptionUnsupportedFilter,
			// Token: 0x0400537D RID: 21373
			ErrorNonUniqueDomainAccount,
			// Token: 0x0400537E RID: 21374
			PilotingOrganization_Error,
			// Token: 0x0400537F RID: 21375
			ConfigScopeMustBeEmpty,
			// Token: 0x04005380 RID: 21376
			ErrorDuplicateManagedFolderAddition,
			// Token: 0x04005381 RID: 21377
			ErrorInvalidConfigScope,
			// Token: 0x04005382 RID: 21378
			EmailAddressPolicyPriorityLowestFormatError,
			// Token: 0x04005383 RID: 21379
			InvalidSubnetNameFormat,
			// Token: 0x04005384 RID: 21380
			ErrorServerRoleNotSupported,
			// Token: 0x04005385 RID: 21381
			ServiceInstanceContainerNotFoundException,
			// Token: 0x04005386 RID: 21382
			InvalidAttachmentFilterRegex,
			// Token: 0x04005387 RID: 21383
			ErrorAdfsAudienceUriFormat,
			// Token: 0x04005388 RID: 21384
			NspiRpcError,
			// Token: 0x04005389 RID: 21385
			ExceptionConflictingArguments,
			// Token: 0x0400538A RID: 21386
			ExceptionNoSchemaMasterServerObject,
			// Token: 0x0400538B RID: 21387
			ErrorMailboxExistsInCollection,
			// Token: 0x0400538C RID: 21388
			ConfigurationSettingsDriverNotInitialized,
			// Token: 0x0400538D RID: 21389
			InvalidPartitionFqdn,
			// Token: 0x0400538E RID: 21390
			RoleIsMandatoryInRoleAssignment,
			// Token: 0x0400538F RID: 21391
			MsaUserNotFoundInGlsError,
			// Token: 0x04005390 RID: 21392
			BackSyncDataSourceInDifferentSiteMessage,
			// Token: 0x04005391 RID: 21393
			CannotResolveTenantRelocationRequestIdentity,
			// Token: 0x04005392 RID: 21394
			FFOMigration_Error,
			// Token: 0x04005393 RID: 21395
			ExceptionTokenGroupsNeedsDomainSession,
			// Token: 0x04005394 RID: 21396
			ErrorAdfsAudienceUriDup,
			// Token: 0x04005395 RID: 21397
			ReplicationNotComplete,
			// Token: 0x04005396 RID: 21398
			UserIsMandatoryInRoleAssignment,
			// Token: 0x04005397 RID: 21399
			ConfigurationSettingsInvalidMatch,
			// Token: 0x04005398 RID: 21400
			TenantIsLockedDownForRelocationException,
			// Token: 0x04005399 RID: 21401
			ExceptionInvalidApprovedApplication,
			// Token: 0x0400539A RID: 21402
			ErrorNonUniqueProxy,
			// Token: 0x0400539B RID: 21403
			ExceptionWKGuidNeedsGCSession,
			// Token: 0x0400539C RID: 21404
			ErrorPolicyDontSupportedPresentationObject,
			// Token: 0x0400539D RID: 21405
			ErrorIsServerSuitableInvalidOSVersion,
			// Token: 0x0400539E RID: 21406
			SipUriAlreadyRegistered,
			// Token: 0x0400539F RID: 21407
			WrongDCForCurrentPartition,
			// Token: 0x040053A0 RID: 21408
			BPOS_S_Policy_License_Violation,
			// Token: 0x040053A1 RID: 21409
			ExceptionExtendedRightsNotUnique,
			// Token: 0x040053A2 RID: 21410
			ExceptionGuidSearchRootWithDefaultScope,
			// Token: 0x040053A3 RID: 21411
			ErrorSettingOverrideUnknown,
			// Token: 0x040053A4 RID: 21412
			ErrorIncorrectlyModifiedMailboxCollection,
			// Token: 0x040053A5 RID: 21413
			ConfigurationSettingsRestrictionExtraProperty,
			// Token: 0x040053A6 RID: 21414
			ConstraintViolationInvalidRecipientType,
			// Token: 0x040053A7 RID: 21415
			UnableToResolveMapiIdException,
			// Token: 0x040053A8 RID: 21416
			ErrorSettingOverrideInvalidVariantName,
			// Token: 0x040053A9 RID: 21417
			ErrorWebDistributionEnabledWithoutVersion4,
			// Token: 0x040053AA RID: 21418
			CannotResolveAccountForestDnError,
			// Token: 0x040053AB RID: 21419
			MobileMetabasePathIsInvalid,
			// Token: 0x040053AC RID: 21420
			InvalidAttachmentFilterExtension,
			// Token: 0x040053AD RID: 21421
			ErrorInvalidAuthSettings,
			// Token: 0x040053AE RID: 21422
			ErrorExchangeGroupNotFound,
			// Token: 0x040053AF RID: 21423
			ErrorSettingOverrideInvalidVariantValue,
			// Token: 0x040053B0 RID: 21424
			UnKnownScopeRestrictionType,
			// Token: 0x040053B1 RID: 21425
			NonexistentTimeZoneError,
			// Token: 0x040053B2 RID: 21426
			ErrorExceededMultiTenantResourceCountQuota,
			// Token: 0x040053B3 RID: 21427
			AddressBookNotFoundException,
			// Token: 0x040053B4 RID: 21428
			ExtensionIsInvalid,
			// Token: 0x040053B5 RID: 21429
			AppendLocalizedStrings,
			// Token: 0x040053B6 RID: 21430
			SuitabilityExceptionLdapSearch,
			// Token: 0x040053B7 RID: 21431
			ErrorExchangeMailboxExists,
			// Token: 0x040053B8 RID: 21432
			ExceptionInvalidVlvFilterProperty,
			// Token: 0x040053B9 RID: 21433
			ExceptionADInvalidHandleCookie,
			// Token: 0x040053BA RID: 21434
			ErrorAdfsTrustedIssuerFormat,
			// Token: 0x040053BB RID: 21435
			KpkAccessProblem,
			// Token: 0x040053BC RID: 21436
			ApiDoesNotSupportInputFormatError,
			// Token: 0x040053BD RID: 21437
			DomainAlreadyExistsInMserv,
			// Token: 0x040053BE RID: 21438
			CannotResolveTenantName,
			// Token: 0x040053BF RID: 21439
			ErrorInvalidPrivateCertificate,
			// Token: 0x040053C0 RID: 21440
			ExceptionSearchRootNotChildOfDefaultScope,
			// Token: 0x040053C1 RID: 21441
			ErrorLegacyVersionOfflineAddressBookWithoutPublicFolderDatabase,
			// Token: 0x040053C2 RID: 21442
			DefaultDatabaseAvailabilityGroupContainerNotFoundException,
			// Token: 0x040053C3 RID: 21443
			ErrorSettingOverrideInvalidSectionName,
			// Token: 0x040053C4 RID: 21444
			ErrorSubnetMaskGreaterThanAddress,
			// Token: 0x040053C5 RID: 21445
			InvalidServiceInstanceIdException,
			// Token: 0x040053C6 RID: 21446
			EndpointContainerNotFoundException,
			// Token: 0x040053C7 RID: 21447
			ErrorIsServerSuitableRODC,
			// Token: 0x040053C8 RID: 21448
			TransientMservError,
			// Token: 0x040053C9 RID: 21449
			ExceptionResourceUnhealthy,
			// Token: 0x040053CA RID: 21450
			CannotGetComputerName,
			// Token: 0x040053CB RID: 21451
			ProviderBadImpageFormatLoadException,
			// Token: 0x040053CC RID: 21452
			ConfigurationSettingsOrganizationNotFound,
			// Token: 0x040053CD RID: 21453
			CannotFindTenantByMSAUserNetID,
			// Token: 0x040053CE RID: 21454
			RecordValueFormatChange,
			// Token: 0x040053CF RID: 21455
			LitigationHold_License_Violation,
			// Token: 0x040053D0 RID: 21456
			ErrorInvalidISOCountryCode,
			// Token: 0x040053D1 RID: 21457
			ErrorServiceEndpointNotFound,
			// Token: 0x040053D2 RID: 21458
			ErrorLinkedADObjectNotInSameOrganization,
			// Token: 0x040053D3 RID: 21459
			ExceptionProxyGeneratorDLLFailed,
			// Token: 0x040053D4 RID: 21460
			BPOS_License_NumericLimitViolation,
			// Token: 0x040053D5 RID: 21461
			SessionSubscriptionDisabled,
			// Token: 0x040053D6 RID: 21462
			InvalidControlTextLength,
			// Token: 0x040053D7 RID: 21463
			RelocationInProgress,
			// Token: 0x040053D8 RID: 21464
			ExceptionFailedToRebuildConnection,
			// Token: 0x040053D9 RID: 21465
			RootCannotBeEmpty,
			// Token: 0x040053DA RID: 21466
			TenantIsArrivingException,
			// Token: 0x040053DB RID: 21467
			TenantPerimeterConfigSettingsNotFoundException,
			// Token: 0x040053DC RID: 21468
			ConfigScopeCannotBeEmpty,
			// Token: 0x040053DD RID: 21469
			InvalidDNFormat,
			// Token: 0x040053DE RID: 21470
			ErrorTransitionCounterHasDuplicateEntry,
			// Token: 0x040053DF RID: 21471
			ExceptionADOperationFailedRemoveContainer,
			// Token: 0x040053E0 RID: 21472
			RecipientWriteScopeNotLessThan,
			// Token: 0x040053E1 RID: 21473
			CannotResolveTenantNameByAcceptedDomain,
			// Token: 0x040053E2 RID: 21474
			ExceptionSearchRootChildDomain,
			// Token: 0x040053E3 RID: 21475
			ErrorRemovedMailboxDoesNotHaveDatabase,
			// Token: 0x040053E4 RID: 21476
			AlternateServiceAccountCredentialDisplayFormat,
			// Token: 0x040053E5 RID: 21477
			ErrorCannotSaveBecauseTooNew,
			// Token: 0x040053E6 RID: 21478
			InvalidRootDse,
			// Token: 0x040053E7 RID: 21479
			ExceptionCannotAddSidHistory,
			// Token: 0x040053E8 RID: 21480
			ErrorInvalidServerFqdn,
			// Token: 0x040053E9 RID: 21481
			ExceptionSearchRootNotChildOfSessionSearchRoot,
			// Token: 0x040053EA RID: 21482
			ErrorRemovedMailboxDoesNotHaveMailboxGuid,
			// Token: 0x040053EB RID: 21483
			ExceptionADTopologyServiceDown,
			// Token: 0x040053EC RID: 21484
			CannotCalculatePropertyGeneric,
			// Token: 0x040053ED RID: 21485
			ExceptionADConstraintViolation,
			// Token: 0x040053EE RID: 21486
			ADTreeDeleteNotFinishedException,
			// Token: 0x040053EF RID: 21487
			InvalidAAConfiguration,
			// Token: 0x040053F0 RID: 21488
			PermanentGlsError,
			// Token: 0x040053F1 RID: 21489
			BPOS_S_Property_License_Violation,
			// Token: 0x040053F2 RID: 21490
			CannotResolveTenantNameByExternalDirectoryId,
			// Token: 0x040053F3 RID: 21491
			BEVDirLockdown_Violation,
			// Token: 0x040053F4 RID: 21492
			ErrorProperty1EqProperty2,
			// Token: 0x040053F5 RID: 21493
			ErrorNonUniqueSid,
			// Token: 0x040053F6 RID: 21494
			ExceptionADTopologyErrorWhenLookingForGlobalCatalogsInForest,
			// Token: 0x040053F7 RID: 21495
			ExceptionObjectPartitionDoesNotMatchSessionPartition,
			// Token: 0x040053F8 RID: 21496
			ExceptionADTopologyHasNoServersInForest,
			// Token: 0x040053F9 RID: 21497
			WACDiscoveryEndpointShouldBeAbsoluteUri,
			// Token: 0x040053FA RID: 21498
			MasteredOnPremiseCapabilityUndefinedTenantNotDirSyncing,
			// Token: 0x040053FB RID: 21499
			CannotGetDnFromGuid,
			// Token: 0x040053FC RID: 21500
			ExceptionObjectHasBeenDeletedDuringCurrentOperation,
			// Token: 0x040053FD RID: 21501
			CalculatedPropertyFailed,
			// Token: 0x040053FE RID: 21502
			ApiNotSupportedInBusinessSessionError,
			// Token: 0x040053FF RID: 21503
			RusInvalidFilter,
			// Token: 0x04005400 RID: 21504
			ErrorNoSuitableDCInDomain,
			// Token: 0x04005401 RID: 21505
			SuitabilityErrorDNS,
			// Token: 0x04005402 RID: 21506
			ExceptionGetLocalSiteArgumentException,
			// Token: 0x04005403 RID: 21507
			InvalidSyncLinkFormat,
			// Token: 0x04005404 RID: 21508
			ErrorDuplicatePartnerApplicationId,
			// Token: 0x04005405 RID: 21509
			ErrorAccountPartitionCantBeLocalAndHaveTrustAtTheSameTime,
			// Token: 0x04005406 RID: 21510
			ErrorADResponse,
			// Token: 0x04005407 RID: 21511
			ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory,
			// Token: 0x04005408 RID: 21512
			UnrecognizedRoleEntryType,
			// Token: 0x04005409 RID: 21513
			FailedToUpdateEmailAddressesForExternal,
			// Token: 0x0400540A RID: 21514
			ExceptionErrorFromRUS,
			// Token: 0x0400540B RID: 21515
			ErrorSubnetMaskOutOfRange,
			// Token: 0x0400540C RID: 21516
			NonUniquePilotIdentifier,
			// Token: 0x0400540D RID: 21517
			ErrorThresholdMustBeSet,
			// Token: 0x0400540E RID: 21518
			ErrorProperty1NeValue1WhileProperty2EqValue2,
			// Token: 0x0400540F RID: 21519
			InvalidAutoAttendantSetting,
			// Token: 0x04005410 RID: 21520
			InvalidFilterSize,
			// Token: 0x04005411 RID: 21521
			ServerComponentReadADError,
			// Token: 0x04005412 RID: 21522
			ErrorLogFolderPathEqualsCopyLogFolderPath,
			// Token: 0x04005413 RID: 21523
			ErrorArchiveMailboxExists,
			// Token: 0x04005414 RID: 21524
			InvalidCrossTenantIdFormat,
			// Token: 0x04005415 RID: 21525
			CustomRecipientWriteScopeCannotBeEmpty,
			// Token: 0x04005416 RID: 21526
			CannotBuildAuthenticationTypeFilterNoNamespacesOfType,
			// Token: 0x04005417 RID: 21527
			ErrorSystemFolderPathNotEqualLogFolderPath,
			// Token: 0x04005418 RID: 21528
			LegacyGwartNotFoundException,
			// Token: 0x04005419 RID: 21529
			ErrorSystemFolderPathEqualsCopySystemFolderPath,
			// Token: 0x0400541A RID: 21530
			ExceptionWKGuidNeedsDomainSession,
			// Token: 0x0400541B RID: 21531
			ErrorReportToManagedEnabledWithoutManager,
			// Token: 0x0400541C RID: 21532
			ThrottlingPolicyCorrupted,
			// Token: 0x0400541D RID: 21533
			ExceptionOwaCannotSetPropertyOnE14MailboxPolicyToNull,
			// Token: 0x0400541E RID: 21534
			EXOStandardRestrictions_Error,
			// Token: 0x0400541F RID: 21535
			InvalidDialPlan,
			// Token: 0x04005420 RID: 21536
			ErrorSubnetAddressDoesNotMatchMask,
			// Token: 0x04005421 RID: 21537
			ErrorProperty1GtProperty2,
			// Token: 0x04005422 RID: 21538
			InvalidNonPositiveResourceThreshold,
			// Token: 0x04005423 RID: 21539
			GlsEndpointNotFound,
			// Token: 0x04005424 RID: 21540
			InvalidWaveFilename,
			// Token: 0x04005425 RID: 21541
			CannotFindTenantCUByExternalDirectoryId,
			// Token: 0x04005426 RID: 21542
			InvalidSyncCompanyId,
			// Token: 0x04005427 RID: 21543
			CustomRecipientWriteScopeMustBeEmpty,
			// Token: 0x04005428 RID: 21544
			ErrorReportToBothManagerAndOriginator,
			// Token: 0x04005429 RID: 21545
			PublicFolderReferralServerNotExisting,
			// Token: 0x0400542A RID: 21546
			ErrorNullRecipientTypeInPrecannedFilter,
			// Token: 0x0400542B RID: 21547
			ProviderFileLoadException,
			// Token: 0x0400542C RID: 21548
			SharedConfigurationNotFound,
			// Token: 0x0400542D RID: 21549
			ExceptionFilterWithNullValue,
			// Token: 0x0400542E RID: 21550
			ErrorPrimarySmtpTemplateInvalid,
			// Token: 0x0400542F RID: 21551
			ExceptionOwaCannotSetPropertyOnLegacyMailboxPolicy,
			// Token: 0x04005430 RID: 21552
			InvalidControlAttributeName,
			// Token: 0x04005431 RID: 21553
			ErrorProperty1LtProperty2,
			// Token: 0x04005432 RID: 21554
			PartnerManaged_Violation,
			// Token: 0x04005433 RID: 21555
			ConfigurationSettingsInvalidPriority,
			// Token: 0x04005434 RID: 21556
			ErrorParseCountryInfo,
			// Token: 0x04005435 RID: 21557
			ErrorMailTipTranslationCultureNotSupported,
			// Token: 0x04005436 RID: 21558
			CantSetDialPlanProperty,
			// Token: 0x04005437 RID: 21559
			InvalidCustomGreetingFilename,
			// Token: 0x04005438 RID: 21560
			ExceptionCannotBindToDC,
			// Token: 0x04005439 RID: 21561
			ExceptionUnsupportedFilterForPropertyMultiple,
			// Token: 0x0400543A RID: 21562
			ExArgumentNullException,
			// Token: 0x0400543B RID: 21563
			ExceptionSearchRootNotWithinScope,
			// Token: 0x0400543C RID: 21564
			ExceptionTimelimitExceeded,
			// Token: 0x0400543D RID: 21565
			ExceptionADRetryOnceOperationFailed,
			// Token: 0x0400543E RID: 21566
			ErrorNeutralCulture,
			// Token: 0x0400543F RID: 21567
			ExceptionInvalidOperationOnObject,
			// Token: 0x04005440 RID: 21568
			ServerSideADTopologyServiceCallError,
			// Token: 0x04005441 RID: 21569
			ExceptionWin32OperationFailed,
			// Token: 0x04005442 RID: 21570
			ErrorDCNotFound,
			// Token: 0x04005443 RID: 21571
			AddressBookNoSecurityDescriptor,
			// Token: 0x04005444 RID: 21572
			NotInWriteToMbxMode,
			// Token: 0x04005445 RID: 21573
			ErrorAuthServerNotFound,
			// Token: 0x04005446 RID: 21574
			ErrorProductFileNameDifferentFromCopyFileName,
			// Token: 0x04005447 RID: 21575
			ValueNotInRange,
			// Token: 0x04005448 RID: 21576
			ErrorRemoteAccountPartitionMustHaveTrust,
			// Token: 0x04005449 RID: 21577
			ErrorMasterServerInvalid,
			// Token: 0x0400544A RID: 21578
			ErrorInvalidExecutingOrg,
			// Token: 0x0400544B RID: 21579
			ErrorInvalidMailboxProvisioningConstraint,
			// Token: 0x0400544C RID: 21580
			CannotMakePrimary,
			// Token: 0x0400544D RID: 21581
			MsaUserAlreadyExistsInGlsError,
			// Token: 0x0400544E RID: 21582
			ServerComponentReadTimeout,
			// Token: 0x0400544F RID: 21583
			InvalidOABMapiPropertyParseStringException,
			// Token: 0x04005450 RID: 21584
			ErrorRecipientDoesNotExist,
			// Token: 0x04005451 RID: 21585
			CannotCompareScopeObjects,
			// Token: 0x04005452 RID: 21586
			ExceptionADTopologyErrorWhenLookingForServersInDomain,
			// Token: 0x04005453 RID: 21587
			CannotFindTenantCUByAcceptedDomain,
			// Token: 0x04005454 RID: 21588
			ComposedSuitabilityReachabilityError,
			// Token: 0x04005455 RID: 21589
			ErrorNoWriteScope,
			// Token: 0x04005456 RID: 21590
			ErrorNonUniqueDN,
			// Token: 0x04005457 RID: 21591
			CannotCompareAssignmentsMissingScope,
			// Token: 0x04005458 RID: 21592
			InvalidInfluence,
			// Token: 0x04005459 RID: 21593
			EOPPremiumRestrictions_Error,
			// Token: 0x0400545A RID: 21594
			AmbiguousTimeZoneNameError,
			// Token: 0x0400545B RID: 21595
			InvalidRoleEntryType,
			// Token: 0x0400545C RID: 21596
			MailboxPropertiesMustBeClearedFirst,
			// Token: 0x0400545D RID: 21597
			UnexpectedGlsError,
			// Token: 0x0400545E RID: 21598
			NotInWriteToMServMode,
			// Token: 0x0400545F RID: 21599
			ErrorEmptyString,
			// Token: 0x04005460 RID: 21600
			ExceptionSizelimitExceeded,
			// Token: 0x04005461 RID: 21601
			ErrorRealmFormatInvalid,
			// Token: 0x04005462 RID: 21602
			ConfigurationSettingsGroupExists,
			// Token: 0x04005463 RID: 21603
			ExceptionADOperationFailedAlreadyExist,
			// Token: 0x04005464 RID: 21604
			InvalidEndpointAddressErrorMessage,
			// Token: 0x04005465 RID: 21605
			ErrorAuthServerTypeNotFound,
			// Token: 0x04005466 RID: 21606
			IsMemberOfQueryFailed,
			// Token: 0x04005467 RID: 21607
			ExceptionADTopologyErrorWhenLookingForSite,
			// Token: 0x04005468 RID: 21608
			ErrorNotResettableProperty,
			// Token: 0x04005469 RID: 21609
			ErrorMailTipHtmlCorrupt,
			// Token: 0x0400546A RID: 21610
			ErrorInvalidOrganizationId,
			// Token: 0x0400546B RID: 21611
			ErrorConversionFailed,
			// Token: 0x0400546C RID: 21612
			ResourceMailbox_Violation,
			// Token: 0x0400546D RID: 21613
			DomainNotFoundInGlsError,
			// Token: 0x0400546E RID: 21614
			ExceptionSchemaMismatch,
			// Token: 0x0400546F RID: 21615
			EapDuplicatedEmailAddressTemplate,
			// Token: 0x04005470 RID: 21616
			NspiFailureException,
			// Token: 0x04005471 RID: 21617
			EapMustHaveOnePrimaryAddressTemplate,
			// Token: 0x04005472 RID: 21618
			SuitabilityDirectoryException,
			// Token: 0x04005473 RID: 21619
			ExceptionADTopologyHasNoAvailableServersInForest,
			// Token: 0x04005474 RID: 21620
			InvalidRecipientScope,
			// Token: 0x04005475 RID: 21621
			UnableToResolveMapiPropertyNameException,
			// Token: 0x04005476 RID: 21622
			InvalidOABMapiPropertyTypeException,
			// Token: 0x04005477 RID: 21623
			ErrorInvalidMailboxProvisioningAttribute,
			// Token: 0x04005478 RID: 21624
			ErrorMailboxProvisioningAttributeDoesNotMatchSchema,
			// Token: 0x04005479 RID: 21625
			ErrorMultiplePrimaries,
			// Token: 0x0400547A RID: 21626
			ErrorMinAdminVersionNull,
			// Token: 0x0400547B RID: 21627
			MismatchedMapiPropertyTypesException,
			// Token: 0x0400547C RID: 21628
			ServerHasNotBeenFound,
			// Token: 0x0400547D RID: 21629
			ErrorDuplicateKeyInMailboxProvisioningAttributes,
			// Token: 0x0400547E RID: 21630
			ErrorPrimarySmtpInvalid,
			// Token: 0x0400547F RID: 21631
			ExceptionReferral,
			// Token: 0x04005480 RID: 21632
			ExceptionRootDSE,
			// Token: 0x04005481 RID: 21633
			ErrorNoSuitableGCInForest,
			// Token: 0x04005482 RID: 21634
			InvalidBiggerResourceThreshold,
			// Token: 0x04005483 RID: 21635
			InvalidTimeZoneId,
			// Token: 0x04005484 RID: 21636
			CannotSerializePartitionHint,
			// Token: 0x04005485 RID: 21637
			ErrorThisThresholdMustBeGreaterThanThatThreshold,
			// Token: 0x04005486 RID: 21638
			ExceptionRemoveApprovedApplication,
			// Token: 0x04005487 RID: 21639
			ErrorJoinApprovalRequiredWithoutManager,
			// Token: 0x04005488 RID: 21640
			InvalidConsumerDialPlanSetting,
			// Token: 0x04005489 RID: 21641
			RangeInformationFormatInvalid,
			// Token: 0x0400548A RID: 21642
			ExceptionADTopologyHasNoAvailableServersInDomain,
			// Token: 0x0400548B RID: 21643
			RuleMigration_Error,
			// Token: 0x0400548C RID: 21644
			PropertiesMasteredOnPremise_Violation,
			// Token: 0x0400548D RID: 21645
			BadSwapOperationCount,
			// Token: 0x0400548E RID: 21646
			CannotDetermineDataSessionTypeForObject,
			// Token: 0x0400548F RID: 21647
			ExceptionADOperationFailedNoSuchObject,
			// Token: 0x04005490 RID: 21648
			ExceptionPropertyCannotBeSearchedOn,
			// Token: 0x04005491 RID: 21649
			InvalidNtds,
			// Token: 0x04005492 RID: 21650
			ExceptionADConfigurationObjectRequired,
			// Token: 0x04005493 RID: 21651
			TenantOrgContainerNotFoundException,
			// Token: 0x04005494 RID: 21652
			ExceptionADTopologyUnexpectedError,
			// Token: 0x04005495 RID: 21653
			ErrorNotInServerWriteScope,
			// Token: 0x04005496 RID: 21654
			DuplicatedAcceptedDomain,
			// Token: 0x04005497 RID: 21655
			ErrorCannotSetPermanentAttributes,
			// Token: 0x04005498 RID: 21656
			CannotBuildAuthenticationTypeFilterBadArgument,
			// Token: 0x04005499 RID: 21657
			ErrorTargetPartitionHas2TenantsWithSameId,
			// Token: 0x0400549A RID: 21658
			ExceededMaximumCollectionCount,
			// Token: 0x0400549B RID: 21659
			ExceptionReferralWhenBoundToDomainController,
			// Token: 0x0400549C RID: 21660
			AssignmentsWithConflictingScope,
			// Token: 0x0400549D RID: 21661
			ExceptionReadingRootDSE,
			// Token: 0x0400549E RID: 21662
			CannotGetForestInfo,
			// Token: 0x0400549F RID: 21663
			InvalidCertificateName,
			// Token: 0x040054A0 RID: 21664
			CannotParse,
			// Token: 0x040054A1 RID: 21665
			TransportSettingsAmbiguousException,
			// Token: 0x040054A2 RID: 21666
			ExceptionADTopologyNoSuchForest,
			// Token: 0x040054A3 RID: 21667
			PropertyRequired,
			// Token: 0x040054A4 RID: 21668
			OUsNotSmallerOrEqual,
			// Token: 0x040054A5 RID: 21669
			TimeoutGlsError,
			// Token: 0x040054A6 RID: 21670
			ExtensionAlreadyUsedAsPilotNumber,
			// Token: 0x040054A7 RID: 21671
			ErrorCannotFindRidMasterForPartition,
			// Token: 0x040054A8 RID: 21672
			InvalidCharacterSet,
			// Token: 0x040054A9 RID: 21673
			ExceptionADUnavailable,
			// Token: 0x040054AA RID: 21674
			ErrorSettingOverrideInvalidFlightName,
			// Token: 0x040054AB RID: 21675
			TooManyCustomExtensions,
			// Token: 0x040054AC RID: 21676
			ExceptionOwaCannotSetPropertyOnE12VirtualDirectory,
			// Token: 0x040054AD RID: 21677
			ExArgumentException,
			// Token: 0x040054AE RID: 21678
			CannotResolvePartitionGuidError,
			// Token: 0x040054AF RID: 21679
			CannotGetDnAtDepth,
			// Token: 0x040054B0 RID: 21680
			InvalidHostname,
			// Token: 0x040054B1 RID: 21681
			ErrorTargetOrSourceForestPopulatedStatusNotStarted,
			// Token: 0x040054B2 RID: 21682
			BadSwapResourceIds,
			// Token: 0x040054B3 RID: 21683
			ExceptionADInvalidPassword,
			// Token: 0x040054B4 RID: 21684
			ConfigurationSettingsDuplicateRestriction,
			// Token: 0x040054B5 RID: 21685
			InvalidForestFqdnInGls,
			// Token: 0x040054B6 RID: 21686
			ErrorNonUniqueExchangeObjectId,
			// Token: 0x040054B7 RID: 21687
			ErrorNonUniqueMailboxGetMailboxLocation,
			// Token: 0x040054B8 RID: 21688
			ErrorMailTipDisplayableLengthExceeded,
			// Token: 0x040054B9 RID: 21689
			ErrorIsServerSuitableMissingComputerData,
			// Token: 0x040054BA RID: 21690
			ConversionFailed,
			// Token: 0x040054BB RID: 21691
			ExceptionOneTimeBindFailed,
			// Token: 0x040054BC RID: 21692
			ExceptionDefaultScopeInvalidFormat,
			// Token: 0x040054BD RID: 21693
			TenantNameTooLong,
			// Token: 0x040054BE RID: 21694
			ExArgumentOutOfRangeException,
			// Token: 0x040054BF RID: 21695
			ErrorInvalidISOTwoLetterOrCountryCode,
			// Token: 0x040054C0 RID: 21696
			ConfigurationSettingsDatabaseNotFound,
			// Token: 0x040054C1 RID: 21697
			ApiNotSupportedError,
			// Token: 0x040054C2 RID: 21698
			ErrorDatabaseCopiesInvalid,
			// Token: 0x040054C3 RID: 21699
			InvalidExtension,
			// Token: 0x040054C4 RID: 21700
			ExceptionDnLimitExceeded,
			// Token: 0x040054C5 RID: 21701
			ConfigurationSettingsRestrictionNotExpected,
			// Token: 0x040054C6 RID: 21702
			ErrorNotNullProperty,
			// Token: 0x040054C7 RID: 21703
			ErrorConversionFailedWithError,
			// Token: 0x040054C8 RID: 21704
			ExceptionCannotBindToDomain,
			// Token: 0x040054C9 RID: 21705
			ExceptionInvalidCredentialsFailedToGetIdentity,
			// Token: 0x040054CA RID: 21706
			CannotCalculateProperty,
			// Token: 0x040054CB RID: 21707
			ExceptionNotifyErrorGettingResults,
			// Token: 0x040054CC RID: 21708
			ErrorSettingOverrideInvalidParameterSyntax,
			// Token: 0x040054CD RID: 21709
			WrongDelegationTypeForPolicy,
			// Token: 0x040054CE RID: 21710
			ExceptionCreateLdapConnection,
			// Token: 0x040054CF RID: 21711
			ExceptionADTopologyErrorWhenLookingForTrustRelationships,
			// Token: 0x040054D0 RID: 21712
			ErrorMailboxCollectionNotSupportType,
			// Token: 0x040054D1 RID: 21713
			ExceptionInvalidVlvFilterOption,
			// Token: 0x040054D2 RID: 21714
			ServerSideADTopologyUnexpectedError,
			// Token: 0x040054D3 RID: 21715
			CannotBuildCapabilityFilterUnsupportedCapability,
			// Token: 0x040054D4 RID: 21716
			ExceptionInvalidVlvFilter,
			// Token: 0x040054D5 RID: 21717
			PerimeterSettingsAmbiguousException,
			// Token: 0x040054D6 RID: 21718
			ExceptionCannotRemoveDsServer,
			// Token: 0x040054D7 RID: 21719
			ExceptionCannotUseCredentials,
			// Token: 0x040054D8 RID: 21720
			ExceptionUnsupportedFilterForProperty,
			// Token: 0x040054D9 RID: 21721
			SuitabilityReachabilityError,
			// Token: 0x040054DA RID: 21722
			ExceptionInvalidOperationOnReadOnlySession,
			// Token: 0x040054DB RID: 21723
			ConfigurationSettingsRestrictionExpected,
			// Token: 0x040054DC RID: 21724
			ExceptionADTopologyServiceNotStarted,
			// Token: 0x040054DD RID: 21725
			ExEmptyStringArgumentException,
			// Token: 0x040054DE RID: 21726
			ConstraintLocationValueReservedForSystemUse,
			// Token: 0x040054DF RID: 21727
			FailedToReadAlternateServiceAccountConfigFromRegistry,
			// Token: 0x040054E0 RID: 21728
			ErrorGlobalWebDistributionAndVDirsSet,
			// Token: 0x040054E1 RID: 21729
			ServerComponentLocalRegistryError,
			// Token: 0x040054E2 RID: 21730
			TooManyKeyMappings,
			// Token: 0x040054E3 RID: 21731
			ScopeCannotBeExclusive,
			// Token: 0x040054E4 RID: 21732
			UnsupportedADSyntaxException,
			// Token: 0x040054E5 RID: 21733
			CannotFindOabException,
			// Token: 0x040054E6 RID: 21734
			ExceptionInvalidOperationOnInvalidSession,
			// Token: 0x040054E7 RID: 21735
			ErrorProperty1EqValue1WhileProperty2EqValue2,
			// Token: 0x040054E8 RID: 21736
			ErrorSettingOverrideInvalidComponentName,
			// Token: 0x040054E9 RID: 21737
			RecipientWriteScopeNotLessThanBecauseOfDelegationFlags,
			// Token: 0x040054EA RID: 21738
			TooManyDataInLdapProperty,
			// Token: 0x040054EB RID: 21739
			ErrorInvalidMailboxProvisioningAttributes,
			// Token: 0x040054EC RID: 21740
			ExceptionUnsupportedOperatorForProperty,
			// Token: 0x040054ED RID: 21741
			ErrorTargetPartitionSctMissing,
			// Token: 0x040054EE RID: 21742
			TenantTransportSettingsNotFoundException,
			// Token: 0x040054EF RID: 21743
			ErrorNonUniqueLegacyDN,
			// Token: 0x040054F0 RID: 21744
			ErrorAccountPartitionCantBeLocalAndSecondaryAtTheSameTime,
			// Token: 0x040054F1 RID: 21745
			ExceptionADTopologyTimeoutCall,
			// Token: 0x040054F2 RID: 21746
			ExceptionUnsupportedOperator,
			// Token: 0x040054F3 RID: 21747
			NoMatchingTenantInTargetPartition,
			// Token: 0x040054F4 RID: 21748
			RootMustBeEmpty,
			// Token: 0x040054F5 RID: 21749
			ExceptionValueNotPresent,
			// Token: 0x040054F6 RID: 21750
			ErrorMinAdminVersionOutOfSync,
			// Token: 0x040054F7 RID: 21751
			MasteredOnPremiseCapabilityUndefinedNotTenant,
			// Token: 0x040054F8 RID: 21752
			ExceptionUnsupportedTextFilterOption,
			// Token: 0x040054F9 RID: 21753
			ErrorServiceAccountThrottlingPolicy,
			// Token: 0x040054FA RID: 21754
			ErrorDLAsBothAcceptedAndRejected,
			// Token: 0x040054FB RID: 21755
			ErrorSettingOverrideSyntax,
			// Token: 0x040054FC RID: 21756
			PermanentMservError,
			// Token: 0x040054FD RID: 21757
			InvalidCapabilityOnMailboxPlan,
			// Token: 0x040054FE RID: 21758
			SharedConfigurationVersionNotSupported,
			// Token: 0x040054FF RID: 21759
			OwaAdOrphanFound,
			// Token: 0x04005500 RID: 21760
			ExceptionRUSServerDown,
			// Token: 0x04005501 RID: 21761
			DefaultRoutingGroupNotFoundException,
			// Token: 0x04005502 RID: 21762
			ExceptionCopyChangesForIncompatibleTypes,
			// Token: 0x04005503 RID: 21763
			ErrorSettingOverrideInvalidParameterName,
			// Token: 0x04005504 RID: 21764
			RangePropertyResponseDoesNotContainRangeInformation,
			// Token: 0x04005505 RID: 21765
			ExceptionSeverNotInPartition,
			// Token: 0x04005506 RID: 21766
			CannotResolvePartitionFqdnFromAccountForestDnError,
			// Token: 0x04005507 RID: 21767
			ErrorSettingOverrideUnexpected,
			// Token: 0x04005508 RID: 21768
			OrgWideDelegatingWriteScopeMustBeTheSameAsRoleImplicitWriteScope,
			// Token: 0x04005509 RID: 21769
			InvalidPhrase,
			// Token: 0x0400550A RID: 21770
			ErrorRealmNotFound,
			// Token: 0x0400550B RID: 21771
			ErrorLogonFailuresBeforePINReset,
			// Token: 0x0400550C RID: 21772
			TenantNotFoundInMservError,
			// Token: 0x0400550D RID: 21773
			ErrorIsServerInMaintenanceMode,
			// Token: 0x0400550E RID: 21774
			ExceptionADOperationFailed,
			// Token: 0x0400550F RID: 21775
			ConfigWriteScopeNotLessThanBecauseOfDelegationFlags,
			// Token: 0x04005510 RID: 21776
			SwapShouldNotChangeValues,
			// Token: 0x04005511 RID: 21777
			ConfigurationSettingsHistorySummary,
			// Token: 0x04005512 RID: 21778
			ExceptionUnsupportedPropertyValue,
			// Token: 0x04005513 RID: 21779
			ExceptionNetLogonOperation,
			// Token: 0x04005514 RID: 21780
			ExceptionADTopologyNoSuchDomain,
			// Token: 0x04005515 RID: 21781
			InvalidNumberOfCapabilitiesOnMailboxPlan,
			// Token: 0x04005516 RID: 21782
			ExceptionReadOnlyBecauseTooNew,
			// Token: 0x04005517 RID: 21783
			CannotGetSiteInfo,
			// Token: 0x04005518 RID: 21784
			UnableToDeserializeXMLError,
			// Token: 0x04005519 RID: 21785
			ConfigurationSettingsRestrictionMissingProperty,
			// Token: 0x0400551A RID: 21786
			ConfigurationSettingsInvalidScopeFilter,
			// Token: 0x0400551B RID: 21787
			OwaMetabasePathIsInvalid,
			// Token: 0x0400551C RID: 21788
			InvalidDNStringFormat,
			// Token: 0x0400551D RID: 21789
			ExceptionADTopologyNoServersForPartition,
			// Token: 0x0400551E RID: 21790
			TenantAlreadyExistsInMserv,
			// Token: 0x0400551F RID: 21791
			ExceptionUnsupportedDefaultValueFilter,
			// Token: 0x04005520 RID: 21792
			InvalidResourceThresholdBetweenClassifications,
			// Token: 0x04005521 RID: 21793
			ExceptionInvalidAddressFormat,
			// Token: 0x04005522 RID: 21794
			ExceptionMostDerivedOnBase,
			// Token: 0x04005523 RID: 21795
			ExceptionOwaCannotSetPropertyOnE12VirtualDirectoryToNull,
			// Token: 0x04005524 RID: 21796
			ErrorCopySystemFolderPathNotEqualCopyLogFolderPath,
			// Token: 0x04005525 RID: 21797
			ConfigurationSettingsUnsupportedVersion,
			// Token: 0x04005526 RID: 21798
			ErrorInvalidOpathFilter,
			// Token: 0x04005527 RID: 21799
			WrongAssigneeTypeForPolicyOrPartnerApplication,
			// Token: 0x04005528 RID: 21800
			WrongScopeForCurrentPartition,
			// Token: 0x04005529 RID: 21801
			ErrorInvalidLegacyRdnPrefix,
			// Token: 0x0400552A RID: 21802
			InvalidIdFormat,
			// Token: 0x0400552B RID: 21803
			LocalServerNotFound,
			// Token: 0x0400552C RID: 21804
			ErrorResultsAreNonUnique,
			// Token: 0x0400552D RID: 21805
			OrganizationMailboxNotFound,
			// Token: 0x0400552E RID: 21806
			ExceptionUnsupportedPropertyValueType,
			// Token: 0x0400552F RID: 21807
			ExceptionInvalidBitwiseComparison,
			// Token: 0x04005530 RID: 21808
			CannotFindTemplateUser,
			// Token: 0x04005531 RID: 21809
			ErrorNoSuitableGC,
			// Token: 0x04005532 RID: 21810
			ExceptionServerUnavailable,
			// Token: 0x04005533 RID: 21811
			CannotGetDomainFromDN,
			// Token: 0x04005534 RID: 21812
			FederatedUser_Violation,
			// Token: 0x04005535 RID: 21813
			BPOS_Feature_UsageLocation_Violation,
			// Token: 0x04005536 RID: 21814
			InvalidMaxOutboundConnectionConfiguration,
			// Token: 0x04005537 RID: 21815
			ExceptionADVlvSizeLimitExceeded,
			// Token: 0x04005538 RID: 21816
			ExceptionWKGuidNeedsConfigSession,
			// Token: 0x04005539 RID: 21817
			InvalidCookieServiceInstanceIdException,
			// Token: 0x0400553A RID: 21818
			InternalDsnLanguageNotSupported,
			// Token: 0x0400553B RID: 21819
			ErrorAdditionalInfo,
			// Token: 0x0400553C RID: 21820
			ExceptionOverBudget,
			// Token: 0x0400553D RID: 21821
			ExceptionInvalidAccountName,
			// Token: 0x0400553E RID: 21822
			InvalidConfigScope,
			// Token: 0x0400553F RID: 21823
			ConfigScopeNotLessThan,
			// Token: 0x04005540 RID: 21824
			ExceptionInvalidScopeOperation,
			// Token: 0x04005541 RID: 21825
			ErrorNonUniqueNetId,
			// Token: 0x04005542 RID: 21826
			InvalidMailboxMoveFlags,
			// Token: 0x04005543 RID: 21827
			ExInvalidTypeArgumentException,
			// Token: 0x04005544 RID: 21828
			HostNameMatchesMultipleComputers,
			// Token: 0x04005545 RID: 21829
			ErrorDefaultElcFolderTypeExists,
			// Token: 0x04005546 RID: 21830
			ExceptionADTopologyDomainNameIsNotFqdn,
			// Token: 0x04005547 RID: 21831
			ExceptionDefaultScopeMustContainDomainDN,
			// Token: 0x04005548 RID: 21832
			ErrorInvalidFolderLinksAddition,
			// Token: 0x04005549 RID: 21833
			ExceptionADTopologyHasNoServersInDomain,
			// Token: 0x0400554A RID: 21834
			ExceptionWriteOnceProperty,
			// Token: 0x0400554B RID: 21835
			ExceptionOrgScopeNotInUserScope,
			// Token: 0x0400554C RID: 21836
			ExceptionInvalidVlvSeekReference,
			// Token: 0x0400554D RID: 21837
			ExceptionADOperationFailedNotAMember,
			// Token: 0x0400554E RID: 21838
			ErrorIsServerSuitableMissingOperatingSystemResponse,
			// Token: 0x0400554F RID: 21839
			ErrorPartnerApplicationNotFound,
			// Token: 0x04005550 RID: 21840
			ExceptionInvalidCredentials,
			// Token: 0x04005551 RID: 21841
			MicrosoftExchangeRecipientDisplayNameError,
			// Token: 0x04005552 RID: 21842
			CannotGetChild,
			// Token: 0x04005553 RID: 21843
			FacebookEnabled_Error,
			// Token: 0x04005554 RID: 21844
			ExceptionADTopologyErrorWhenLookingForForest,
			// Token: 0x04005555 RID: 21845
			ErrorMultiValuedPropertyTooLarge,
			// Token: 0x04005556 RID: 21846
			ErrorLinkedADObjectNotInFirstOrganization,
			// Token: 0x04005557 RID: 21847
			ErrorExceededHosterResourceCountQuota,
			// Token: 0x04005558 RID: 21848
			ConstraintViolationValueNotSupportedLCID,
			// Token: 0x04005559 RID: 21849
			RusUnableToPerformValidation,
			// Token: 0x0400555A RID: 21850
			ErrorSecondaryAccountPartitionCantBeUsedForProvisioning,
			// Token: 0x0400555B RID: 21851
			ErrorInvalidLegacyCommonName,
			// Token: 0x0400555C RID: 21852
			ExceptionExtendedRightNotFound,
			// Token: 0x0400555D RID: 21853
			FailedToWriteAlternateServiceAccountConfigToRegistry,
			// Token: 0x0400555E RID: 21854
			ExceptionUnsupportedMatchOptionsForProperty,
			// Token: 0x0400555F RID: 21855
			ExceptionUnknownDirectoryAttribute,
			// Token: 0x04005560 RID: 21856
			ErrorCannotAcquireAuthMetadata,
			// Token: 0x04005561 RID: 21857
			ExtensionNotUnique,
			// Token: 0x04005562 RID: 21858
			ExceptionADTopologyNoSuchSite,
			// Token: 0x04005563 RID: 21859
			ErrorRecipientTypeIsNotValidForDeliveryRestrictionOrModeration,
			// Token: 0x04005564 RID: 21860
			ErrorTwoOrMoreUniqueRecipientTypes,
			// Token: 0x04005565 RID: 21861
			ExceptionADTopologyErrorWhenLookingForLocalDomainTrustRelationships,
			// Token: 0x04005566 RID: 21862
			ExternalEmailAddressInvalid,
			// Token: 0x04005567 RID: 21863
			ErrorGroupMemberDepartRestrictionApprovalRequired,
			// Token: 0x04005568 RID: 21864
			TransientGlsError,
			// Token: 0x04005569 RID: 21865
			FilterCannotBeEmpty,
			// Token: 0x0400556A RID: 21866
			ExceptionADTopologyServiceCallError,
			// Token: 0x0400556B RID: 21867
			InvalidDistributionGroupNamingPolicyFormat,
			// Token: 0x0400556C RID: 21868
			ConfigurationSettingsNotFoundForGroup,
			// Token: 0x0400556D RID: 21869
			CannotGetDomainInfo,
			// Token: 0x0400556E RID: 21870
			ExceptionGuidSearchRootWithScope,
			// Token: 0x0400556F RID: 21871
			ErrorCannotFindTenant,
			// Token: 0x04005570 RID: 21872
			ErrorNoSuitableDC,
			// Token: 0x04005571 RID: 21873
			SharingPolicyDuplicateDomain,
			// Token: 0x04005572 RID: 21874
			ErrorNonUniqueCertificate,
			// Token: 0x04005573 RID: 21875
			OrgWideDelegatingConfigScopeMustBeTheSameAsRoleImplicitWriteScope,
			// Token: 0x04005574 RID: 21876
			ExceptionNativeErrorWhenLookingForServersInDomain,
			// Token: 0x04005575 RID: 21877
			ErrorMoreThanOneSKUCapability,
			// Token: 0x04005576 RID: 21878
			AlternateServiceAccountConfigurationDisplayFormat,
			// Token: 0x04005577 RID: 21879
			DefaultDatabaseContainerNotFoundException,
			// Token: 0x04005578 RID: 21880
			ExceptionAdamGetServerFromDomainDN,
			// Token: 0x04005579 RID: 21881
			ErrorEdbFilePathInRoot,
			// Token: 0x0400557A RID: 21882
			ConfigurationSettingsGroupSummary,
			// Token: 0x0400557B RID: 21883
			InvalidSmartHost,
			// Token: 0x0400557C RID: 21884
			ProviderFileNotFoundLoadException,
			// Token: 0x0400557D RID: 21885
			ErrorRecipientAsBothAcceptedAndRejected,
			// Token: 0x0400557E RID: 21886
			DuplicateTlsDomainCapabilitiesNotAllowed,
			// Token: 0x0400557F RID: 21887
			CannotConstructCrossTenantObjectId,
			// Token: 0x04005580 RID: 21888
			CrossRecordMismatch,
			// Token: 0x04005581 RID: 21889
			ErrorStartDateAfterEndDate,
			// Token: 0x04005582 RID: 21890
			ErrorNotInReadScope,
			// Token: 0x04005583 RID: 21891
			ErrorSubnetMaskLessThanMinRange,
			// Token: 0x04005584 RID: 21892
			InvalidTenantRecordInGls,
			// Token: 0x04005585 RID: 21893
			ErrorNonUniqueLiveIdMemberName,
			// Token: 0x04005586 RID: 21894
			ExceptionAccessDeniedFromRUS,
			// Token: 0x04005587 RID: 21895
			CannotCompareScopeObjectWithOU,
			// Token: 0x04005588 RID: 21896
			ErrorInvalidLegacyDN
		}
	}
}
