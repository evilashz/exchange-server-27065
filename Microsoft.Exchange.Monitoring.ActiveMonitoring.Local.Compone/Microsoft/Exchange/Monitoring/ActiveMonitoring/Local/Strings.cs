using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x02000595 RID: 1429
	internal static class Strings
	{
		// Token: 0x060023BD RID: 9149 RVA: 0x000D5ABC File Offset: 0x000D3CBC
		static Strings()
		{
			Strings.stringIDs.Add(2286371297U, "DatabaseGuidNotSupplied");
			Strings.stringIDs.Add(1189391965U, "OABGenTenantOutOfSLABody");
			Strings.stringIDs.Add(52276049U, "SearchFailToSaveMessage");
			Strings.stringIDs.Add(3960338262U, "ForwardSyncHaltEscalationSubject");
			Strings.stringIDs.Add(312538007U, "MaintenanceFailureEscalationMessage");
			Strings.stringIDs.Add(299573369U, "DatabaseSpaceHelpString");
			Strings.stringIDs.Add(683259685U, "PumServiceNotRunningEscalationMessage");
			Strings.stringIDs.Add(1378981546U, "HealthSetMaintenanceEscalationSubjectPrefix");
			Strings.stringIDs.Add(2358296594U, "RegisterDnsHostRecordResponderName");
			Strings.stringIDs.Add(2482686375U, "RcaDiscoveryOutlookAnywhereNotFound");
			Strings.stringIDs.Add(3458357958U, "UnableToCompleteTopologyEscalationMessage");
			Strings.stringIDs.Add(945891153U, "LargestDeliveryQueueLengthEscalationMessage");
			Strings.stringIDs.Add(1718676120U, "DSNotifyQueueHigh15MinutesEscalationMessage");
			Strings.stringIDs.Add(2503653255U, "OutStandingATQRequests15MinutesEscalationMessage");
			Strings.stringIDs.Add(2592211666U, "ADDatabaseCorruption1017EscalationMessage");
			Strings.stringIDs.Add(2321911502U, "DeviceDegradedEscalationMessage");
			Strings.stringIDs.Add(1918560769U, "RemoteDomainControllerStateEscalationMessage");
			Strings.stringIDs.Add(164779627U, "OWACalendarAppPoolEscalationBody");
			Strings.stringIDs.Add(1218627587U, "MediaEstablishedFailedEscalationMessage");
			Strings.stringIDs.Add(1980518279U, "RequestForFfoApprovalToOfflineFailed");
			Strings.stringIDs.Add(3706262484U, "InsufficientInformationKCCEscalationMessage");
			Strings.stringIDs.Add(2150007296U, "ClusterNodeEvictedEscalationMessage");
			Strings.stringIDs.Add(635314528U, "OwaOutsideInDatabaseAvailabilityFailuresSubject");
			Strings.stringIDs.Add(112230113U, "RouteTableRecoveryResponderName");
			Strings.stringIDs.Add(2257888472U, "AssistantsActiveDatabaseSubject");
			Strings.stringIDs.Add(3750243459U, "ForwardSyncMonopolizedEscalationSubject");
			Strings.stringIDs.Add(805912300U, "UMProtectedVoiceMessageEncryptDecryptFailedEscalationMessage");
			Strings.stringIDs.Add(1299295308U, "SearchIndexFailureEscalationMessage");
			Strings.stringIDs.Add(4294224569U, "ForwardSyncCookieNotUpToDateEscalationMessage");
			Strings.stringIDs.Add(2092011713U, "CannotBootEscalationMessage");
			Strings.stringIDs.Add(2288602703U, "PassiveReplicationPerformanceCounterProbeEscalationMessage");
			Strings.stringIDs.Add(3399715728U, "OwaTooManyStartPageFailuresSubject");
			Strings.stringIDs.Add(4261321224U, "OwaOutsideInDatabaseAvailabilityFailuresBody");
			Strings.stringIDs.Add(409381696U, "SearchWordBreakerLoadingFailureEscalationMessage");
			Strings.stringIDs.Add(741999051U, "Pop3CommandProcessingTimeEscalationMessage");
			Strings.stringIDs.Add(3741312692U, "DeltaSyncEndpointUnreachableEscalationMessage");
			Strings.stringIDs.Add(1119726172U, "EventLogProbeRedEvents");
			Strings.stringIDs.Add(109013642U, "ProvisioningBigVolumeErrorProbeName");
			Strings.stringIDs.Add(2897164042U, "PassiveADReplicationMonitorEscalationMessage");
			Strings.stringIDs.Add(373161856U, "UMCertificateThumbprint");
			Strings.stringIDs.Add(893346260U, "ForwardSyncMonopolizedEscalationMessage");
			Strings.stringIDs.Add(2704062871U, "NoResponseHeadersAvailable");
			Strings.stringIDs.Add(380917770U, "AdminAuditingAvailabilityFailureEscalationSubject");
			Strings.stringIDs.Add(1943611390U, "DeltaSyncPartnerAuthenticationFailedEscalationMessage");
			Strings.stringIDs.Add(1893530126U, "SharedCacheEscalationSubject");
			Strings.stringIDs.Add(3957270050U, "JournalingEscalationSubject");
			Strings.stringIDs.Add(2370864531U, "HighProcessor15MinutesEscalationMessage");
			Strings.stringIDs.Add(3308508053U, "NetworkAdapterRssEscalationMessage");
			Strings.stringIDs.Add(1482699764U, "CPUOverThresholdErrorEscalationSubject");
			Strings.stringIDs.Add(1218523202U, "Transport80thPercentileMissingSLAEscalationMessage");
			Strings.stringIDs.Add(2302596019U, "InferenceTrainingSLAEscalationMessage");
			Strings.stringIDs.Add(2517955710U, "EDiscoveryEscalationBodyEntText");
			Strings.stringIDs.Add(1451525307U, "AsynchronousAuditSearchAvailabilityFailureEscalationSubject");
			Strings.stringIDs.Add(3795580654U, "SearchRopNotSupportedEscalationMessage");
			Strings.stringIDs.Add(909096326U, "PushNotificationEnterpriseUnknownError");
			Strings.stringIDs.Add(610566341U, "OwaClientAccessRoleNotInstalled");
			Strings.stringIDs.Add(3645413289U, "BridgeHeadReplicationEscalationMessage");
			Strings.stringIDs.Add(2836474835U, "PushNotificationEnterpriseNotConfigured");
			Strings.stringIDs.Add(892806076U, "IncompatibleVectorEscalationMessage");
			Strings.stringIDs.Add(4138453910U, "DatabaseCorruptionEscalationMessage");
			Strings.stringIDs.Add(1752264565U, "ReplicationOutdatedObjectsFailedEscalationMessage");
			Strings.stringIDs.Add(443970804U, "DatabaseCorruptEscalationMessage");
			Strings.stringIDs.Add(48294403U, "HealthSetAlertSuppressionWarning");
			Strings.stringIDs.Add(1065939221U, "OwaIMInitializationFailedMessage");
			Strings.stringIDs.Add(2172476143U, "ForwardSyncHaltEscalationMessage");
			Strings.stringIDs.Add(1679303927U, "OfflineGLSEscalationMessage");
			Strings.stringIDs.Add(2914700647U, "UnableToRunEscalateByDatabaseHealthResponder");
			Strings.stringIDs.Add(2514146180U, "AggregateDeliveryQueueLengthEscalationMessage");
			Strings.stringIDs.Add(4070157535U, "NoCafeMonitoringAccountsAvailable");
			Strings.stringIDs.Add(2950938112U, "MediaEdgeResourceAllocationFailedEscalationMessage");
			Strings.stringIDs.Add(1420198520U, "DRAPendingReplication5MinutesEscalationMessage");
			Strings.stringIDs.Add(357692756U, "SchemaPartitionFailedEscalationMessage");
			Strings.stringIDs.Add(2064178893U, "DatabaseSchemaVersionCheckEscalationSubject");
			Strings.stringIDs.Add(878340914U, "UMSipListeningPort");
			Strings.stringIDs.Add(3803135725U, "ELCMailboxSLAEscalationSubject");
			Strings.stringIDs.Add(1586522085U, "DHCPNacksEscalationMessage");
			Strings.stringIDs.Add(1507797344U, "ELCArchiveDumpsterEscalationMessage");
			Strings.stringIDs.Add(3628981090U, "KDCServiceStatusTestMessage");
			Strings.stringIDs.Add(401186895U, "LowMemoryUnderThresholdErrorEscalationSubject");
			Strings.stringIDs.Add(1108409616U, "OwaIMInitializationFailedSubject");
			Strings.stringIDs.Add(3717637996U, "PingConnectivityEscalationSubject");
			Strings.stringIDs.Add(529762034U, "PublicFolderConnectionCountEscalationMessage");
			Strings.stringIDs.Add(663711086U, "FastNodeNotHealthyEscalationMessage");
			Strings.stringIDs.Add(1307224852U, "CheckDCMMDivergenceScriptExceptionMessage");
			Strings.stringIDs.Add(1220266742U, "CrossPremiseMailflowEscalationMessage");
			Strings.stringIDs.Add(2945963449U, "ForwardSyncStandardCompanyEscalationSubject");
			Strings.stringIDs.Add(1462332256U, "JournalArchiveEscalationSubject");
			Strings.stringIDs.Add(3746115424U, "DoMTConnectivityEscalateMessage");
			Strings.stringIDs.Add(680617032U, "InferenceComponentDisabledEscalationMessage");
			Strings.stringIDs.Add(2776049552U, "NoBackendMonitoringAccountsAvailable");
			Strings.stringIDs.Add(349561476U, "ActiveDirectoryConnectivityEscalationMessage");
			Strings.stringIDs.Add(4196713157U, "SyntheticReplicationTransactionEscalationMessage");
			Strings.stringIDs.Add(814146069U, "OabFileLoadExceptionEncounteredSubject");
			Strings.stringIDs.Add(4268033680U, "RegistryAccessDeniedEscalationMessage");
			Strings.stringIDs.Add(2465131700U, "AuditLogSearchServiceletEscalationSubject");
			Strings.stringIDs.Add(649999061U, "EventLogProbeLogName");
			Strings.stringIDs.Add(536159405U, "Imap4ProtocolUnhealthy");
			Strings.stringIDs.Add(1179543515U, "DLExpansionEscalationMessage");
			Strings.stringIDs.Add(2046186369U, "ReplicationFailuresEscalationMessage");
			Strings.stringIDs.Add(1915337724U, "SCTStateMonitoringScriptExceptionMessage");
			Strings.stringIDs.Add(1417399775U, "ELCExceptionEscalationMessage");
			Strings.stringIDs.Add(1844700225U, "OabTooManyHttpErrorResponsesEncounteredBody");
			Strings.stringIDs.Add(275645054U, "QuarantineEscalationMessage");
			Strings.stringIDs.Add(3642796412U, "TransportRejectingMessageSubmissions");
			Strings.stringIDs.Add(3898201799U, "PublicFolderConnectionCountEscalationSubject");
			Strings.stringIDs.Add(2285670747U, "PowerShellProfileEscalationSubject");
			Strings.stringIDs.Add(93821081U, "DivergenceBetweenCAAndAD1006EscalationMessage");
			Strings.stringIDs.Add(726991111U, "UnreachableQueueLengthEscalationMessage");
			Strings.stringIDs.Add(3639475733U, "OabFileLoadExceptionEncounteredBody");
			Strings.stringIDs.Add(252047361U, "PublicFolderSyncEscalationSubject");
			Strings.stringIDs.Add(4139459438U, "Imap4CommandProcessingTimeEscalationMessage");
			Strings.stringIDs.Add(3378952345U, "InvalidSearchResultsExceptionMessage");
			Strings.stringIDs.Add(4272242116U, "SearchInformationNotAvailable");
			Strings.stringIDs.Add(2844472229U, "ActiveDatabaseAvailabilityEscalationSubject");
			Strings.stringIDs.Add(952182041U, "ELCPermanentEscalationSubject");
			Strings.stringIDs.Add(1117652268U, "EventLogProbeGreenEvents");
			Strings.stringIDs.Add(4155705872U, "ClusterHangEscalationMessage");
			Strings.stringIDs.Add(891001936U, "FEPServiceNotRunningEscalationMessage");
			Strings.stringIDs.Add(286684653U, "RidMonitorEscalationMessage");
			Strings.stringIDs.Add(2576630513U, "SystemMailboxGuidNotFound");
			Strings.stringIDs.Add(2727074534U, "MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedEscalationMessage");
			Strings.stringIDs.Add(3448175994U, "SearchTransportAgentFailureEscalationMessage");
			Strings.stringIDs.Add(2551706401U, "TransportMessageCategorizationEscalationMessage");
			Strings.stringIDs.Add(2595326046U, "InocrrectSCTStateExceptionMessage");
			Strings.stringIDs.Add(1789741911U, "DataIssueEscalationMessage");
			Strings.stringIDs.Add(3993818915U, "KerbAuthFailureEscalationMessagPAC");
			Strings.stringIDs.Add(423349970U, "DivergenceInDefinitionEscalationMessage");
			Strings.stringIDs.Add(2198917190U, "MobilityAccount");
			Strings.stringIDs.Add(682587060U, "OwaTooManyLogoffFailuresBody");
			Strings.stringIDs.Add(770708540U, "ForwardSyncProcessRepeatedlyCrashingEscalationSubject");
			Strings.stringIDs.Add(1169357891U, "ADDatabaseCorruptionEscalationMessage");
			Strings.stringIDs.Add(580917981U, "MailboxAuditingAvailabilityFailureEscalationSubject");
			Strings.stringIDs.Add(1969709639U, "TopologyServiceConnectivityEscalationMessage");
			Strings.stringIDs.Add(4160621849U, "UMSipTransport");
			Strings.stringIDs.Add(2022121183U, "OabProtocolEscalationBody");
			Strings.stringIDs.Add(3048659136U, "PushNotificationEnterpriseEmptyServiceUri");
			Strings.stringIDs.Add(3214050158U, "PushNotificationEnterpriseAuthError");
			Strings.stringIDs.Add(2021347314U, "UnableToRunAlertNotificationTypeByDatabaseCopyStateResponder");
			Strings.stringIDs.Add(1920206617U, "ELCDumpsterWarningEscalationSubject");
			Strings.stringIDs.Add(2542492469U, "OabMailboxEscalationBody");
			Strings.stringIDs.Add(3640747412U, "CheckZombieDCEscalateMessage");
			Strings.stringIDs.Add(2206456195U, "RidSetMonitorEscalationMessage");
			Strings.stringIDs.Add(584392819U, "PushNotificationCafeEndpointUnhealthy");
			Strings.stringIDs.Add(632484951U, "ProvisioningBigVolumeErrorEscalationMessage");
			Strings.stringIDs.Add(58433467U, "PublicFolderMailboxQuotaEscalationMessage");
			Strings.stringIDs.Add(1077120006U, "CASRoutingFailureEscalationSubject");
			Strings.stringIDs.Add(3549972347U, "OAuthRequestFailureEscalationBody");
			Strings.stringIDs.Add(3456628766U, "GLSEscalationMessage");
			Strings.stringIDs.Add(2809892364U, "SCTNotFoundForAllVersionsExceptionMessage");
			Strings.stringIDs.Add(1613949426U, "SqlOutputStreamInRetryEscalationMessage");
			Strings.stringIDs.Add(3213813466U, "DefaultEscalationSubject");
			Strings.stringIDs.Add(471095525U, "MailboxAuditingAvailabilityFailureEscalationBody");
			Strings.stringIDs.Add(1276311330U, "BulkProvisioningNoProgressEscalationSubject");
			Strings.stringIDs.Add(1377854022U, "InfrastructureValidationSubject");
			Strings.stringIDs.Add(1194398797U, "SearchMemoryUsageOverThresholdEscalationMessage");
			Strings.stringIDs.Add(105667215U, "SharedCacheEscalationMessage");
			Strings.stringIDs.Add(799661977U, "CannotRecoverEscalationMessage");
			Strings.stringIDs.Add(4214982447U, "AsynchronousAuditSearchAvailabilityFailureEscalationBody");
			Strings.stringIDs.Add(483050246U, "OwaIMLogAnalyzerMessage");
			Strings.stringIDs.Add(1005998838U, "UMCertificateSubjectName");
			Strings.stringIDs.Add(1108329015U, "OwaNoMailboxesAvailable");
			Strings.stringIDs.Add(2516924014U, "TransportCategorizerJobsUnavailableEscalationMessage");
			Strings.stringIDs.Add(910032993U, "SingleAvailableDatabaseCopyEscalationMessage");
			Strings.stringIDs.Add(1345149923U, "DivergenceInSiteNameEscalationMessage");
			Strings.stringIDs.Add(3893081264U, "NullSearchResponseExceptionMessage");
			Strings.stringIDs.Add(144342951U, "UncategorizedProcess");
			Strings.stringIDs.Add(3984209328U, "MSExchangeProtectedServiceHostCrashingMessage");
			Strings.stringIDs.Add(3400380005U, "UMDatacenterLoadBalancerSipOptionsPingEscalationMessage");
			Strings.stringIDs.Add(564297537U, "PassiveReplicationMonitorEscalationMessage");
			Strings.stringIDs.Add(4039996833U, "ReinstallServerEscalationMessage");
			Strings.stringIDs.Add(2217573206U, "ForwardSyncCookieNotUpToDateEscalationSubject");
			Strings.stringIDs.Add(2260546867U, "ActiveDirectoryConnectivityLocalEscalationMessage");
			Strings.stringIDs.Add(70227012U, "PassiveDatabaseAvailabilityEscalationSubject");
			Strings.stringIDs.Add(3278262633U, "OabTooManyHttpErrorResponsesEncounteredSubject");
			Strings.stringIDs.Add(2089742391U, "ELCTransientEscalationSubject");
			Strings.stringIDs.Add(1923047497U, "SiteFailureEscalationMessage");
			Strings.stringIDs.Add(1012424075U, "OnlineMeetingCreateEscalationBody");
			Strings.stringIDs.Add(462239732U, "SiteMailboxDocumentSyncEscalationSubject");
			Strings.stringIDs.Add(4075662140U, "NTDSCorruptionEscalationMessage");
			Strings.stringIDs.Add(3665176336U, "TopoDiscoveryFailedAllServersEscalationMessage");
			Strings.stringIDs.Add(400522478U, "VersionStore1479EscalationMessage");
			Strings.stringIDs.Add(868604876U, "AssistantsNotRunningToCompletionSubject");
			Strings.stringIDs.Add(1817218534U, "AdminAuditingAvailabilityFailureEscalationBody");
			Strings.stringIDs.Add(3226571974U, "ForwardSyncLiteCompanyEscalationSubject");
			Strings.stringIDs.Add(4083150976U, "MSExchangeInformationStoreCannotContactADEscalationMessage");
			Strings.stringIDs.Add(465857804U, "DHCPServerRequestsEscalationMessage");
			Strings.stringIDs.Add(4092609939U, "NoNTDSObjectEscalationMessage");
			Strings.stringIDs.Add(88349250U, "PublicFolderMoveJobStuckEscalationSubject");
			Strings.stringIDs.Add(4169066611U, "SCTMonitoringScriptExceptionMessage");
			Strings.stringIDs.Add(380251366U, "ProvisionedDCBelowMinimumEscalationMessage");
			Strings.stringIDs.Add(3378026954U, "KerbAuthFailureEscalationMessage");
			Strings.stringIDs.Add(1277128402U, "RequestsQueuedOver500EscalationMessage");
			Strings.stringIDs.Add(2497401637U, "PopImapGuid");
			Strings.stringIDs.Add(77538240U, "MaintenanceFailureEscalationSubject");
			Strings.stringIDs.Add(1577351776U, "TransportServerDownEscalationMessage");
			Strings.stringIDs.Add(3174826533U, "PopImapEndpoint");
			Strings.stringIDs.Add(3229931132U, "NtlmConnectivityEscalationMessage");
			Strings.stringIDs.Add(499742697U, "OabTooManyWebAppStartsSubject");
			Strings.stringIDs.Add(3922382486U, "CafeEscalationSubjectUnhealthy");
			Strings.stringIDs.Add(2676970837U, "DnsHostRecordProbeName");
			Strings.stringIDs.Add(1037389911U, "ELCArchiveDumpsterWarningEscalationSubject");
			Strings.stringIDs.Add(1536257732U, "JournalFilterAgentEscalationMessage");
			Strings.stringIDs.Add(1098709463U, "WacDiscoveryFailureSubject");
			Strings.stringIDs.Add(887962606U, "CASRoutingLatencyEscalationSubject");
			Strings.stringIDs.Add(1060163078U, "EDSJobPoisonedEscalationMessage");
			Strings.stringIDs.Add(938822773U, "JournalFilterAgentEscalationSubject");
			Strings.stringIDs.Add(3660582759U, "DnsHostRecordMonitorName");
			Strings.stringIDs.Add(3238760785U, "VersionStore2008EscalationMessage");
			Strings.stringIDs.Add(2695821343U, "DnsServiceMonitorName");
			Strings.stringIDs.Add(3687026854U, "DatabaseAvailabilityHelpString");
			Strings.stringIDs.Add(3465747486U, "PublicFolderMailboxQuotaEscalationSubject");
			Strings.stringIDs.Add(1074585207U, "HealthSetEscalationSubjectPrefix");
			Strings.stringIDs.Add(4054742343U, "DHCPServerDeclinesEscalationMessage");
			Strings.stringIDs.Add(235731656U, "TrustMonitorProbeEscalationMessage");
			Strings.stringIDs.Add(959637577U, "InvalidIncludedAssistantType");
			Strings.stringIDs.Add(861211960U, "ReplicationDisabledEscalationMessage");
			Strings.stringIDs.Add(2731022862U, "ProvisioningBigVolumeErrorEscalateResponderName");
			Strings.stringIDs.Add(3881757534U, "CASRoutingLatencyEscalationBody");
			Strings.stringIDs.Add(1221520849U, "SecurityAlertMalwareDetectedEscalationMessage");
			Strings.stringIDs.Add(1306675352U, "SynchronousAuditSearchAvailabilityFailureEscalationBody");
			Strings.stringIDs.Add(4173100116U, "MaintenanceTimeoutEscalationMessage");
			Strings.stringIDs.Add(217815433U, "DeltaSyncServiceEndpointsLoadFailedEscalationMessage");
			Strings.stringIDs.Add(269594253U, "SchedulingLatencyEscalateResponderMessage");
			Strings.stringIDs.Add(4073533312U, "PowerShellProfileEscalationMessage");
			Strings.stringIDs.Add(3913093087U, "OAuthRequestFailureEscalationSubject");
			Strings.stringIDs.Add(1166689995U, "OabMailboxNoOrgMailbox");
			Strings.stringIDs.Add(3054327783U, "PushNotificationEnterpriseEmptyDomain");
			Strings.stringIDs.Add(3814043621U, "AssistantsOutOfSlaMessage");
			Strings.stringIDs.Add(3546783417U, "EwsAutodEscalationSubjectUnhealthy");
			Strings.stringIDs.Add(1293802800U, "HttpConnectivityEscalationSubject");
			Strings.stringIDs.Add(4157781316U, "DatabaseObjectNotFoundException");
			Strings.stringIDs.Add(2661017855U, "FSMODCNotProvisionedEscalationMessage");
			Strings.stringIDs.Add(3444300625U, "AssistantsNotRunningToCompletionMessage");
			Strings.stringIDs.Add(1281304358U, "DLExpansionEscalationSubject");
			Strings.stringIDs.Add(2407301436U, "RcaTaskOutlineFailed");
			Strings.stringIDs.Add(3706272566U, "CASRoutingFailureEscalationBody");
			Strings.stringIDs.Add(2189832741U, "DnsServiceProbeName");
			Strings.stringIDs.Add(2148760356U, "SynchronousAuditSearchAvailabilityFailureEscalationSubject");
			Strings.stringIDs.Add(4134231892U, "CannotRebuildIndexEscalationMessage");
			Strings.stringIDs.Add(3667272656U, "UMPipelineFullEscalationMessageString");
			Strings.stringIDs.Add(1880452434U, "DatabaseNotAttachedReadOnly");
			Strings.stringIDs.Add(2815102519U, "InfrastructureValidationMessage");
			Strings.stringIDs.Add(977800858U, "OwaTooManyHttpErrorResponsesEncounteredSubject");
			Strings.stringIDs.Add(3387090034U, "CPUOverThresholdWarningEscalationSubject");
			Strings.stringIDs.Add(35045070U, "SubscriptionSlaMissedEscalationMessage");
			Strings.stringIDs.Add(1680117983U, "ActiveDirectoryConnectivityConfigDCEscalationMessage");
			Strings.stringIDs.Add(3785224032U, "OwaMailboxRoleNotInstalled");
			Strings.stringIDs.Add(421810782U, "PushNotificationEnterpriseNetworkingError");
			Strings.stringIDs.Add(1548802155U, "DatabaseAvailabilityTimeout");
			Strings.stringIDs.Add(843177689U, "DatabaseGuidNotFound");
			Strings.stringIDs.Add(678296020U, "SearchIndexBacklogAggregatedEscalationMessage");
			Strings.stringIDs.Add(2793575470U, "PublicFolderLocalEWSLogonEscalationMessage");
			Strings.stringIDs.Add(3926584359U, "TenantRelocationErrorsFoundExceptionMessage");
			Strings.stringIDs.Add(3506328701U, "UMCallRouterCertificateNearExpiryEscalationMessage");
			Strings.stringIDs.Add(136972698U, "SlowADWritesEscalationMessage");
			Strings.stringIDs.Add(2850892442U, "RcaEscalationBodyEnt");
			Strings.stringIDs.Add(2395533582U, "MailboxDatabasesUnavailable");
			Strings.stringIDs.Add(515794671U, "RetryRemoteDeliveryQueueLengthEscalationMessage");
			Strings.stringIDs.Add(3544714230U, "FailedToUpgradeIndexEscalationMessage");
			Strings.stringIDs.Add(18446274U, "EventAssistantsWatermarksHelpString");
			Strings.stringIDs.Add(1165995200U, "InferenceClassifcationSLAEscalationMessage");
			Strings.stringIDs.Add(4059291937U, "MRSUnhealthyMessage");
			Strings.stringIDs.Add(3867394701U, "UMServiceType");
			Strings.stringIDs.Add(365492232U, "DivergenceBetweenCAAndAD1003EscalationMessage");
			Strings.stringIDs.Add(182386565U, "AssistantsActiveDatabaseMessage");
			Strings.stringIDs.Add(1990015256U, "SchedulingLatencyEscalateResponderSubject");
			Strings.stringIDs.Add(3218355606U, "OfficeGraphTransportDeliveryAgentFailureEscalationMessage");
			Strings.stringIDs.Add(1104137584U, "OfficeGraphMessageTracingPluginFailureEscalationMessage");
			Strings.stringIDs.Add(424604115U, "LogicalDiskFreeMegabytesEscalationMessage");
			Strings.stringIDs.Add(1920697843U, "OwaIMLogAnalyzerSubject");
			Strings.stringIDs.Add(3515405004U, "RaidDegradedEscalationMessage");
			Strings.stringIDs.Add(1236475651U, "BulkProvisioningNoProgressEscalationMessage");
			Strings.stringIDs.Add(1313509642U, "AssistantsOutOfSlaSubject");
			Strings.stringIDs.Add(1818757842U, "OwaTooManyHttpErrorResponsesEncounteredBody");
			Strings.stringIDs.Add(2102184447U, "CheckSumEscalationMessage");
			Strings.stringIDs.Add(3498323379U, "PublicFolderLocalEWSLogonEscalationSubject");
			Strings.stringIDs.Add(776931395U, "UMServerAddress");
			Strings.stringIDs.Add(3415559268U, "CafeArrayNameCouldNotBeRetrieved");
			Strings.stringIDs.Add(3465381739U, "EDSServiceNotRunningEscalationMessage");
			Strings.stringIDs.Add(2618809902U, "SearchFailToCheckNodeState");
			Strings.stringIDs.Add(2406788764U, "OwaTooManyStartPageFailuresBody");
			Strings.stringIDs.Add(1518847271U, "QuarantineEscalationSubject");
			Strings.stringIDs.Add(1941315341U, "HostControllerServiceRunningMessage");
			Strings.stringIDs.Add(3578257008U, "SearchGracefulDegradationManagerFailureEscalationMessage");
			Strings.stringIDs.Add(46842886U, "ProvisioningBigVolumeErrorMonitorName");
			Strings.stringIDs.Add(3672282944U, "OwaTooManyWebAppStartsSubject");
			Strings.stringIDs.Add(1495785996U, "SearchQueryStxSimpleQueryMode");
			Strings.stringIDs.Add(3956529557U, "OABGenTenantOutOfSLASubject");
			Strings.stringIDs.Add(951276022U, "ELCDumpsterEscalationMessage");
			Strings.stringIDs.Add(1559006762U, "DirectoryConfigDiscrepancyEscalationMessage");
			Strings.stringIDs.Add(1496058059U, "NetworkAdapterRecoveryResponderName");
			Strings.stringIDs.Add(898751369U, "SearchGracefulDegradationStatusEscalationMessage");
			Strings.stringIDs.Add(3135153224U, "DnsServiceRestartResponderName");
			Strings.stringIDs.Add(3845606054U, "ELCMailboxSLAEscalationMessage");
			Strings.stringIDs.Add(1456195959U, "JournalingEscalationMessage");
			Strings.stringIDs.Add(1598120667U, "MaintenanceTimeoutEscalationSubject");
			Strings.stringIDs.Add(3433261884U, "ContentsUnpredictableEscalationMessage");
			Strings.stringIDs.Add(3788119207U, "EscalationSubjectUnhealthy");
			Strings.stringIDs.Add(859758172U, "AsyncAuditLogSearchEscalationSubject");
			Strings.stringIDs.Add(706708545U, "DefaultEscalationMessage");
			Strings.stringIDs.Add(2480657645U, "SyntheticReplicationMonitorEscalationMessage");
			Strings.stringIDs.Add(31173904U, "EDiscoveryEscalationBodyDCHTML");
			Strings.stringIDs.Add(1499273528U, "OwaTooManyLogoffFailuresSubject");
			Strings.stringIDs.Add(4220476005U, "CheckProvisionedDCExceptionMessage");
			Strings.stringIDs.Add(3913310860U, "ProvisioningBigVolumeErrorEscalationSubject");
			Strings.stringIDs.Add(3906074550U, "UMCertificateNearExpiryEscalationMessage");
			Strings.stringIDs.Add(182285359U, "FailureItemMessageForNTFSCorruption");
			Strings.stringIDs.Add(821565004U, "DatabaseRPCLatencyMonitorGreenMessage");
			Strings.stringIDs.Add(1705255289U, "RelocationServicePermanentExceptionMessage");
			Strings.stringIDs.Add(3730111670U, "LiveIdAuthenticationEscalationMesage");
			Strings.stringIDs.Add(3256369209U, "JournalArchiveEscalationMessage");
			Strings.stringIDs.Add(1746979174U, "Pop3ProtocolUnhealthy");
			Strings.stringIDs.Add(408002753U, "HxServiceEscalationMessageUnhealthy");
			Strings.stringIDs.Add(2258779484U, "RequestForNewRidPoolFailedEscalationMessage");
			Strings.stringIDs.Add(2903867061U, "DatabaseSizeEscalationSubject");
			Strings.stringIDs.Add(4196623762U, "OabMailboxManifestEmpty");
			Strings.stringIDs.Add(1260554753U, "CheckFsmoRolesScriptExceptionMessage");
			Strings.stringIDs.Add(1084277863U, "PopImapSecondaryEndpoint");
			Strings.stringIDs.Add(3639485535U, "CannotFunctionNormallyEscalationMessage");
			Strings.stringIDs.Add(116423715U, "EscalationMessageHealthy");
			Strings.stringIDs.Add(2944547573U, "PublicFolderMoveJobStuckEscalationMessage");
			Strings.stringIDs.Add(667399815U, "SearchServiceNotRunningEscalationMessage");
			Strings.stringIDs.Add(1455075163U, "MobilityAccountPassword");
			Strings.stringIDs.Add(3746052928U, "EventLogProbeProviderName");
			Strings.stringIDs.Add(1122579724U, "VersionStore623EscalationMessage");
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x000D745F File Offset: 0x000D565F
		public static LocalizedString DatabaseGuidNotSupplied
		{
			get
			{
				return new LocalizedString("DatabaseGuidNotSupplied", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000D7478 File Offset: 0x000D5678
		public static LocalizedString QuarantinedMailboxEscalationMessageEnt(string databaseName)
		{
			return new LocalizedString("QuarantinedMailboxEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x000D74A0 File Offset: 0x000D56A0
		public static LocalizedString OABGenTenantOutOfSLABody
		{
			get
			{
				return new LocalizedString("OABGenTenantOutOfSLABody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000D74B8 File Offset: 0x000D56B8
		public static LocalizedString PopSelfTestEscalationBodyDC(string serverName, string probeName)
		{
			return new LocalizedString("PopSelfTestEscalationBodyDC", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x000D74E4 File Offset: 0x000D56E4
		public static LocalizedString SearchFailToSaveMessage
		{
			get
			{
				return new LocalizedString("SearchFailToSaveMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000D74FC File Offset: 0x000D56FC
		public static LocalizedString OneCopyMonitorFailureEscalationSubject(string component, string machine, int threshold)
		{
			return new LocalizedString("OneCopyMonitorFailureEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				threshold
			});
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000D7534 File Offset: 0x000D5734
		public static LocalizedString CircularLoggingDisabledEscalationMessage(string database)
		{
			return new LocalizedString("CircularLoggingDisabledEscalationMessage", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000D755C File Offset: 0x000D575C
		public static LocalizedString MailSubmissionBehindWatermarksEscalationMessageEnt(TimeSpan ageThreshold, TimeSpan duration, string databaseName, string invokeNowCommand, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("MailSubmissionBehindWatermarksEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				ageThreshold,
				duration,
				databaseName,
				invokeNowCommand,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x000D759F File Offset: 0x000D579F
		public static LocalizedString ForwardSyncHaltEscalationSubject
		{
			get
			{
				return new LocalizedString("ForwardSyncHaltEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x000D75B6 File Offset: 0x000D57B6
		public static LocalizedString MaintenanceFailureEscalationMessage
		{
			get
			{
				return new LocalizedString("MaintenanceFailureEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x000D75CD File Offset: 0x000D57CD
		public static LocalizedString DatabaseSpaceHelpString
		{
			get
			{
				return new LocalizedString("DatabaseSpaceHelpString", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x000D75E4 File Offset: 0x000D57E4
		public static LocalizedString PumServiceNotRunningEscalationMessage
		{
			get
			{
				return new LocalizedString("PumServiceNotRunningEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060023CA RID: 9162 RVA: 0x000D75FB File Offset: 0x000D57FB
		public static LocalizedString HealthSetMaintenanceEscalationSubjectPrefix
		{
			get
			{
				return new LocalizedString("HealthSetMaintenanceEscalationSubjectPrefix", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000D7614 File Offset: 0x000D5814
		public static LocalizedString InsufficientRedundancyEscalationSubject(string component, string machine)
		{
			return new LocalizedString("InsufficientRedundancyEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine
			});
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x000D7640 File Offset: 0x000D5840
		public static LocalizedString RegisterDnsHostRecordResponderName
		{
			get
			{
				return new LocalizedString("RegisterDnsHostRecordResponderName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000D7658 File Offset: 0x000D5858
		public static LocalizedString LagCopyHealthProblemEscalationSubject(string component, string machine, string database)
		{
			return new LocalizedString("LagCopyHealthProblemEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				database
			});
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x000D7688 File Offset: 0x000D5888
		public static LocalizedString RcaDiscoveryOutlookAnywhereNotFound
		{
			get
			{
				return new LocalizedString("RcaDiscoveryOutlookAnywhereNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000D76A0 File Offset: 0x000D58A0
		public static LocalizedString ActiveDatabaseAvailabilityEscalationMessageEnt(string invokeNowCommand, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("ActiveDatabaseAvailabilityEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				invokeNowCommand,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000D76CC File Offset: 0x000D58CC
		public static LocalizedString StoreAdminRPCInterfaceEscalationEscalationMessageEnt(TimeSpan duration)
		{
			return new LocalizedString("StoreAdminRPCInterfaceEscalationEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				duration
			});
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060023D1 RID: 9169 RVA: 0x000D76F9 File Offset: 0x000D58F9
		public static LocalizedString UnableToCompleteTopologyEscalationMessage
		{
			get
			{
				return new LocalizedString("UnableToCompleteTopologyEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060023D2 RID: 9170 RVA: 0x000D7710 File Offset: 0x000D5910
		public static LocalizedString LargestDeliveryQueueLengthEscalationMessage
		{
			get
			{
				return new LocalizedString("LargestDeliveryQueueLengthEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060023D3 RID: 9171 RVA: 0x000D7727 File Offset: 0x000D5927
		public static LocalizedString DSNotifyQueueHigh15MinutesEscalationMessage
		{
			get
			{
				return new LocalizedString("DSNotifyQueueHigh15MinutesEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000D7740 File Offset: 0x000D5940
		public static LocalizedString InfrastructureValidationError(string error)
		{
			return new LocalizedString("InfrastructureValidationError", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x000D7768 File Offset: 0x000D5968
		public static LocalizedString OutStandingATQRequests15MinutesEscalationMessage
		{
			get
			{
				return new LocalizedString("OutStandingATQRequests15MinutesEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x000D777F File Offset: 0x000D597F
		public static LocalizedString ADDatabaseCorruption1017EscalationMessage
		{
			get
			{
				return new LocalizedString("ADDatabaseCorruption1017EscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x000D7796 File Offset: 0x000D5996
		public static LocalizedString DeviceDegradedEscalationMessage
		{
			get
			{
				return new LocalizedString("DeviceDegradedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000D77B0 File Offset: 0x000D59B0
		public static LocalizedString AvailabilityServiceEscalationHtmlBody(string monitorName)
		{
			return new LocalizedString("AvailabilityServiceEscalationHtmlBody", Strings.ResourceManager, new object[]
			{
				monitorName
			});
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000D77D8 File Offset: 0x000D59D8
		public static LocalizedString RwsDatamartConnectionEscalationBody(string serverName)
		{
			return new LocalizedString("RwsDatamartConnectionEscalationBody", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000D7800 File Offset: 0x000D5A00
		public static LocalizedString ServiceNotRunningEscalationMessage(string serviceName)
		{
			return new LocalizedString("ServiceNotRunningEscalationMessage", Strings.ResourceManager, new object[]
			{
				serviceName
			});
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000D7828 File Offset: 0x000D5A28
		public static LocalizedString DatabaseLocationNotFoundException(string databaseGuid)
		{
			return new LocalizedString("DatabaseLocationNotFoundException", Strings.ResourceManager, new object[]
			{
				databaseGuid
			});
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000D7850 File Offset: 0x000D5A50
		public static LocalizedString OwaCustomerTouchPointEscalationHtmlBody(string serverName, string logPath)
		{
			return new LocalizedString("OwaCustomerTouchPointEscalationHtmlBody", Strings.ResourceManager, new object[]
			{
				serverName,
				logPath
			});
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x000D787C File Offset: 0x000D5A7C
		public static LocalizedString RemoteDomainControllerStateEscalationMessage
		{
			get
			{
				return new LocalizedString("RemoteDomainControllerStateEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000D7894 File Offset: 0x000D5A94
		public static LocalizedString EwsAutodSelfTestEscalationRecoveryDetails(string appPool)
		{
			return new LocalizedString("EwsAutodSelfTestEscalationRecoveryDetails", Strings.ResourceManager, new object[]
			{
				appPool
			});
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000D78BC File Offset: 0x000D5ABC
		public static LocalizedString MRSServiceNotRunningSubject(string service)
		{
			return new LocalizedString("MRSServiceNotRunningSubject", Strings.ResourceManager, new object[]
			{
				service
			});
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x000D78E4 File Offset: 0x000D5AE4
		public static LocalizedString OWACalendarAppPoolEscalationBody
		{
			get
			{
				return new LocalizedString("OWACalendarAppPoolEscalationBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000D78FC File Offset: 0x000D5AFC
		public static LocalizedString SearchQueryStxSuccess(int hits, string query, string mailboxSmtpAddress)
		{
			return new LocalizedString("SearchQueryStxSuccess", Strings.ResourceManager, new object[]
			{
				hits,
				query,
				mailboxSmtpAddress
			});
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000D7934 File Offset: 0x000D5B34
		public static LocalizedString QuarantinedMailboxEscalationMessageDc(string databaseName)
		{
			return new LocalizedString("QuarantinedMailboxEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x000D795C File Offset: 0x000D5B5C
		public static LocalizedString MediaEstablishedFailedEscalationMessage
		{
			get
			{
				return new LocalizedString("MediaEstablishedFailedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x000D7973 File Offset: 0x000D5B73
		public static LocalizedString RequestForFfoApprovalToOfflineFailed
		{
			get
			{
				return new LocalizedString("RequestForFfoApprovalToOfflineFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x000D798A File Offset: 0x000D5B8A
		public static LocalizedString InsufficientInformationKCCEscalationMessage
		{
			get
			{
				return new LocalizedString("InsufficientInformationKCCEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x000D79A1 File Offset: 0x000D5BA1
		public static LocalizedString ClusterNodeEvictedEscalationMessage
		{
			get
			{
				return new LocalizedString("ClusterNodeEvictedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x000D79B8 File Offset: 0x000D5BB8
		public static LocalizedString OwaOutsideInDatabaseAvailabilityFailuresSubject
		{
			get
			{
				return new LocalizedString("OwaOutsideInDatabaseAvailabilityFailuresSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x000D79CF File Offset: 0x000D5BCF
		public static LocalizedString RouteTableRecoveryResponderName
		{
			get
			{
				return new LocalizedString("RouteTableRecoveryResponderName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000D79E8 File Offset: 0x000D5BE8
		public static LocalizedString ExchangeCrashExceededErrorThresholdMessage(string processName)
		{
			return new LocalizedString("ExchangeCrashExceededErrorThresholdMessage", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x000D7A10 File Offset: 0x000D5C10
		public static LocalizedString AssistantsActiveDatabaseSubject
		{
			get
			{
				return new LocalizedString("AssistantsActiveDatabaseSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000D7A28 File Offset: 0x000D5C28
		public static LocalizedString DatabaseRepeatedMountsEscalationMessage(string databaseName, TimeSpan duration)
		{
			return new LocalizedString("DatabaseRepeatedMountsEscalationMessage", Strings.ResourceManager, new object[]
			{
				databaseName,
				duration
			});
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000D7A5C File Offset: 0x000D5C5C
		public static LocalizedString OnlineMeetingCreateEscalationSubject(string serverName)
		{
			return new LocalizedString("OnlineMeetingCreateEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x000D7A84 File Offset: 0x000D5C84
		public static LocalizedString ForwardSyncMonopolizedEscalationSubject
		{
			get
			{
				return new LocalizedString("ForwardSyncMonopolizedEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000D7A9C File Offset: 0x000D5C9C
		public static LocalizedString LocalMachineDriveEncryptionLockEscalationMessage(string volumes, string serverName)
		{
			return new LocalizedString("LocalMachineDriveEncryptionLockEscalationMessage", Strings.ResourceManager, new object[]
			{
				volumes,
				serverName
			});
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x000D7AC8 File Offset: 0x000D5CC8
		public static LocalizedString UMProtectedVoiceMessageEncryptDecryptFailedEscalationMessage
		{
			get
			{
				return new LocalizedString("UMProtectedVoiceMessageEncryptDecryptFailedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000D7ADF File Offset: 0x000D5CDF
		public static LocalizedString SearchIndexFailureEscalationMessage
		{
			get
			{
				return new LocalizedString("SearchIndexFailureEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x000D7AF6 File Offset: 0x000D5CF6
		public static LocalizedString ForwardSyncCookieNotUpToDateEscalationMessage
		{
			get
			{
				return new LocalizedString("ForwardSyncCookieNotUpToDateEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000D7B10 File Offset: 0x000D5D10
		public static LocalizedString DbFailureItemIoHardEscalationSubject(string component, string machine, string dbCopy)
		{
			return new LocalizedString("DbFailureItemIoHardEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				dbCopy
			});
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x000D7B40 File Offset: 0x000D5D40
		public static LocalizedString CannotBootEscalationMessage
		{
			get
			{
				return new LocalizedString("CannotBootEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000D7B58 File Offset: 0x000D5D58
		public static LocalizedString LogVolumeSpaceEscalationMessage(string database)
		{
			return new LocalizedString("LogVolumeSpaceEscalationMessage", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000D7B80 File Offset: 0x000D5D80
		public static LocalizedString DatabaseSchemaVersionCheckEscalationMessageDc(string invokeNowCommand, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("DatabaseSchemaVersionCheckEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				invokeNowCommand,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000D7BAC File Offset: 0x000D5DAC
		public static LocalizedString ProcessCrashing(string serviceName, string server)
		{
			return new LocalizedString("ProcessCrashing", Strings.ResourceManager, new object[]
			{
				serviceName,
				server
			});
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000D7BD8 File Offset: 0x000D5DD8
		public static LocalizedString CafeEscalationRecoveryDetails(string appPool)
		{
			return new LocalizedString("CafeEscalationRecoveryDetails", Strings.ResourceManager, new object[]
			{
				appPool
			});
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000D7C00 File Offset: 0x000D5E00
		public static LocalizedString StoreProcessRepeatedlyCrashingEscalationMessageEnt(string processName, int count, TimeSpan duration)
		{
			return new LocalizedString("StoreProcessRepeatedlyCrashingEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				processName,
				count,
				duration
			});
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x000D7C3A File Offset: 0x000D5E3A
		public static LocalizedString PassiveReplicationPerformanceCounterProbeEscalationMessage
		{
			get
			{
				return new LocalizedString("PassiveReplicationPerformanceCounterProbeEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x000D7C54 File Offset: 0x000D5E54
		public static LocalizedString PrivateWorkingSetExceededErrorThresholdMessage(string processName)
		{
			return new LocalizedString("PrivateWorkingSetExceededErrorThresholdMessage", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000D7C7C File Offset: 0x000D5E7C
		public static LocalizedString ProcessorTimeExceededWarningThresholdMessage(string processName)
		{
			return new LocalizedString("ProcessorTimeExceededWarningThresholdMessage", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060023FC RID: 9212 RVA: 0x000D7CA4 File Offset: 0x000D5EA4
		public static LocalizedString OwaTooManyStartPageFailuresSubject
		{
			get
			{
				return new LocalizedString("OwaTooManyStartPageFailuresSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x000D7CBB File Offset: 0x000D5EBB
		public static LocalizedString OwaOutsideInDatabaseAvailabilityFailuresBody
		{
			get
			{
				return new LocalizedString("OwaOutsideInDatabaseAvailabilityFailuresBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000D7CD4 File Offset: 0x000D5ED4
		public static LocalizedString DatabaseAvailabilityFailure(string database)
		{
			return new LocalizedString("DatabaseAvailabilityFailure", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000D7CFC File Offset: 0x000D5EFC
		public static LocalizedString EDiscoveryscalationSubject(string monitorIdentity, string target)
		{
			return new LocalizedString("EDiscoveryscalationSubject", Strings.ResourceManager, new object[]
			{
				monitorIdentity,
				target
			});
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x000D7D28 File Offset: 0x000D5F28
		public static LocalizedString SearchWordBreakerLoadingFailureEscalationMessage
		{
			get
			{
				return new LocalizedString("SearchWordBreakerLoadingFailureEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x000D7D3F File Offset: 0x000D5F3F
		public static LocalizedString Pop3CommandProcessingTimeEscalationMessage
		{
			get
			{
				return new LocalizedString("Pop3CommandProcessingTimeEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000D7D58 File Offset: 0x000D5F58
		public static LocalizedString ParseDiagnosticsStringError(string error)
		{
			return new LocalizedString("ParseDiagnosticsStringError", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002403 RID: 9219 RVA: 0x000D7D80 File Offset: 0x000D5F80
		public static LocalizedString DeltaSyncEndpointUnreachableEscalationMessage
		{
			get
			{
				return new LocalizedString("DeltaSyncEndpointUnreachableEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x000D7D97 File Offset: 0x000D5F97
		public static LocalizedString EventLogProbeRedEvents
		{
			get
			{
				return new LocalizedString("EventLogProbeRedEvents", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x000D7DAE File Offset: 0x000D5FAE
		public static LocalizedString ProvisioningBigVolumeErrorProbeName
		{
			get
			{
				return new LocalizedString("ProvisioningBigVolumeErrorProbeName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000D7DC8 File Offset: 0x000D5FC8
		public static LocalizedString SystemDriveSpaceEscalationSubject(string component, string machine, string drive, string threshold)
		{
			return new LocalizedString("SystemDriveSpaceEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				drive,
				threshold
			});
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000D7DFC File Offset: 0x000D5FFC
		public static LocalizedString DatabaseConsistencyEscalationMessage(string databaseName)
		{
			return new LocalizedString("DatabaseConsistencyEscalationMessage", Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x000D7E24 File Offset: 0x000D6024
		public static LocalizedString PassiveADReplicationMonitorEscalationMessage
		{
			get
			{
				return new LocalizedString("PassiveADReplicationMonitorEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x000D7E3C File Offset: 0x000D603C
		public static LocalizedString GenericOverallXFailureEscalationMessage(string target)
		{
			return new LocalizedString("GenericOverallXFailureEscalationMessage", Strings.ResourceManager, new object[]
			{
				target
			});
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x0600240A RID: 9226 RVA: 0x000D7E64 File Offset: 0x000D6064
		public static LocalizedString UMCertificateThumbprint
		{
			get
			{
				return new LocalizedString("UMCertificateThumbprint", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x000D7E7B File Offset: 0x000D607B
		public static LocalizedString ForwardSyncMonopolizedEscalationMessage
		{
			get
			{
				return new LocalizedString("ForwardSyncMonopolizedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000D7E94 File Offset: 0x000D6094
		public static LocalizedString ImapProxyTestEscalationBodyDC(string serverName, string probeName)
		{
			return new LocalizedString("ImapProxyTestEscalationBodyDC", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000D7EC0 File Offset: 0x000D60C0
		public static LocalizedString PopSelfTestEscalationBodyENT(string serverName, string probeName)
		{
			return new LocalizedString("PopSelfTestEscalationBodyENT", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x000D7EEC File Offset: 0x000D60EC
		public static LocalizedString NoResponseHeadersAvailable
		{
			get
			{
				return new LocalizedString("NoResponseHeadersAvailable", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x000D7F03 File Offset: 0x000D6103
		public static LocalizedString AdminAuditingAvailabilityFailureEscalationSubject
		{
			get
			{
				return new LocalizedString("AdminAuditingAvailabilityFailureEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000D7F1C File Offset: 0x000D611C
		public static LocalizedString TransportSyncManagerServiceNotRunningEscalationMessage(string service)
		{
			return new LocalizedString("TransportSyncManagerServiceNotRunningEscalationMessage", Strings.ResourceManager, new object[]
			{
				service
			});
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000D7F44 File Offset: 0x000D6144
		public static LocalizedString EscalationMessageUnhealthy(string customMessage)
		{
			return new LocalizedString("EscalationMessageUnhealthy", Strings.ResourceManager, new object[]
			{
				customMessage
			});
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000D7F6C File Offset: 0x000D616C
		public static LocalizedString EacDeepTestEscalationSubject(string serverName)
		{
			return new LocalizedString("EacDeepTestEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000D7F94 File Offset: 0x000D6194
		public static LocalizedString InvokeNowDefinitionFailure(string requestId)
		{
			return new LocalizedString("InvokeNowDefinitionFailure", Strings.ResourceManager, new object[]
			{
				requestId
			});
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000D7FBC File Offset: 0x000D61BC
		public static LocalizedString ImapCustomerTouchPointEscalationBodyENT(string serverName, string probeName)
		{
			return new LocalizedString("ImapCustomerTouchPointEscalationBodyENT", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000D7FE8 File Offset: 0x000D61E8
		public static LocalizedString PushNotificationChannelError(string channelType, string channelName, string serverName)
		{
			return new LocalizedString("PushNotificationChannelError", Strings.ResourceManager, new object[]
			{
				channelType,
				channelName,
				serverName
			});
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x000D8018 File Offset: 0x000D6218
		public static LocalizedString DeltaSyncPartnerAuthenticationFailedEscalationMessage
		{
			get
			{
				return new LocalizedString("DeltaSyncPartnerAuthenticationFailedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000D8030 File Offset: 0x000D6230
		public static LocalizedString DBAvailableButUnloadedByTransportSyncManagerMessage(string databaseName, string guid)
		{
			return new LocalizedString("DBAvailableButUnloadedByTransportSyncManagerMessage", Strings.ResourceManager, new object[]
			{
				databaseName,
				guid
			});
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000D805C File Offset: 0x000D625C
		public static LocalizedString MonitoringAccountUnavailable(string mailboxDatabaseName)
		{
			return new LocalizedString("MonitoringAccountUnavailable", Strings.ResourceManager, new object[]
			{
				mailboxDatabaseName
			});
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000D8084 File Offset: 0x000D6284
		public static LocalizedString LocalDriveLogSpaceEscalationMessageDc(string drive, TimeSpan duration, string threshold)
		{
			return new LocalizedString("LocalDriveLogSpaceEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				drive,
				duration,
				threshold
			});
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x000D80B9 File Offset: 0x000D62B9
		public static LocalizedString SharedCacheEscalationSubject
		{
			get
			{
				return new LocalizedString("SharedCacheEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000D80D0 File Offset: 0x000D62D0
		public static LocalizedString DatabaseRPCLatencyEscalationSubject(string databaseName, int latency, TimeSpan duration)
		{
			return new LocalizedString("DatabaseRPCLatencyEscalationSubject", Strings.ResourceManager, new object[]
			{
				databaseName,
				latency,
				duration
			});
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000D810C File Offset: 0x000D630C
		public static LocalizedString LocalDriveLogSpaceEscalationMessageEnt(string drive, TimeSpan duration, string threshold)
		{
			return new LocalizedString("LocalDriveLogSpaceEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				drive,
				duration,
				threshold
			});
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000D8144 File Offset: 0x000D6344
		public static LocalizedString SearchQueryFailure(string databaseName, string failureRate, string threshold, string total, string successful, string errorEvents)
		{
			return new LocalizedString("SearchQueryFailure", Strings.ResourceManager, new object[]
			{
				databaseName,
				failureRate,
				threshold,
				total,
				successful,
				errorEvents
			});
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000D8184 File Offset: 0x000D6384
		public static LocalizedString EndpointManagerEndpointUninitialized(string name)
		{
			return new LocalizedString("EndpointManagerEndpointUninitialized", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x000D81AC File Offset: 0x000D63AC
		public static LocalizedString JournalingEscalationSubject
		{
			get
			{
				return new LocalizedString("JournalingEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x000D81C3 File Offset: 0x000D63C3
		public static LocalizedString HighProcessor15MinutesEscalationMessage
		{
			get
			{
				return new LocalizedString("HighProcessor15MinutesEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x000D81DA File Offset: 0x000D63DA
		public static LocalizedString NetworkAdapterRssEscalationMessage
		{
			get
			{
				return new LocalizedString("NetworkAdapterRssEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000D81F4 File Offset: 0x000D63F4
		public static LocalizedString MigrationNotificationMessage(string notification)
		{
			return new LocalizedString("MigrationNotificationMessage", Strings.ResourceManager, new object[]
			{
				notification
			});
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x000D821C File Offset: 0x000D641C
		public static LocalizedString HostControllerServiceNodeExcessivePrivateBytes(string details)
		{
			return new LocalizedString("HostControllerServiceNodeExcessivePrivateBytes", Strings.ResourceManager, new object[]
			{
				details
			});
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000D8244 File Offset: 0x000D6444
		public static LocalizedString HostControllerServiceNodeExcessivePrivateBytesDetails(string nodeName, double thresholdGb, long actual)
		{
			return new LocalizedString("HostControllerServiceNodeExcessivePrivateBytesDetails", Strings.ResourceManager, new object[]
			{
				nodeName,
				thresholdGb,
				actual
			});
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x000D827E File Offset: 0x000D647E
		public static LocalizedString CPUOverThresholdErrorEscalationSubject
		{
			get
			{
				return new LocalizedString("CPUOverThresholdErrorEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000D8298 File Offset: 0x000D6498
		public static LocalizedString SearchIndexServerCopyStatus(string message, string serverCopyStatus)
		{
			return new LocalizedString("SearchIndexServerCopyStatus", Strings.ResourceManager, new object[]
			{
				message,
				serverCopyStatus
			});
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002427 RID: 9255 RVA: 0x000D82C4 File Offset: 0x000D64C4
		public static LocalizedString Transport80thPercentileMissingSLAEscalationMessage
		{
			get
			{
				return new LocalizedString("Transport80thPercentileMissingSLAEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x000D82DB File Offset: 0x000D64DB
		public static LocalizedString InferenceTrainingSLAEscalationMessage
		{
			get
			{
				return new LocalizedString("InferenceTrainingSLAEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002429 RID: 9257 RVA: 0x000D82F2 File Offset: 0x000D64F2
		public static LocalizedString EDiscoveryEscalationBodyEntText
		{
			get
			{
				return new LocalizedString("EDiscoveryEscalationBodyEntText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x000D8309 File Offset: 0x000D6509
		public static LocalizedString AsynchronousAuditSearchAvailabilityFailureEscalationSubject
		{
			get
			{
				return new LocalizedString("AsynchronousAuditSearchAvailabilityFailureEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000D8320 File Offset: 0x000D6520
		public static LocalizedString SearchActiveCopyUnhealthyEscalationMessage(string databaseName)
		{
			return new LocalizedString("SearchActiveCopyUnhealthyEscalationMessage", Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x000D8348 File Offset: 0x000D6548
		public static LocalizedString SearchRopNotSupportedEscalationMessage
		{
			get
			{
				return new LocalizedString("SearchRopNotSupportedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600242D RID: 9261 RVA: 0x000D835F File Offset: 0x000D655F
		public static LocalizedString PushNotificationEnterpriseUnknownError
		{
			get
			{
				return new LocalizedString("PushNotificationEnterpriseUnknownError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600242E RID: 9262 RVA: 0x000D8376 File Offset: 0x000D6576
		public static LocalizedString OwaClientAccessRoleNotInstalled
		{
			get
			{
				return new LocalizedString("OwaClientAccessRoleNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x0600242F RID: 9263 RVA: 0x000D838D File Offset: 0x000D658D
		public static LocalizedString BridgeHeadReplicationEscalationMessage
		{
			get
			{
				return new LocalizedString("BridgeHeadReplicationEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x000D83A4 File Offset: 0x000D65A4
		public static LocalizedString PushNotificationEnterpriseNotConfigured
		{
			get
			{
				return new LocalizedString("PushNotificationEnterpriseNotConfigured", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000D83BC File Offset: 0x000D65BC
		public static LocalizedString ProcessRepeatedlyCrashingEscalationSubject(string processName)
		{
			return new LocalizedString("ProcessRepeatedlyCrashingEscalationSubject", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000D83E4 File Offset: 0x000D65E4
		public static LocalizedString UMCallRouterRecentCallRejectedMessageString(int percentageValue)
		{
			return new LocalizedString("UMCallRouterRecentCallRejectedMessageString", Strings.ResourceManager, new object[]
			{
				percentageValue
			});
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000D8414 File Offset: 0x000D6614
		public static LocalizedString OwaCustomerTouchPointEscalationBody(string serverName, string logPath)
		{
			return new LocalizedString("OwaCustomerTouchPointEscalationBody", Strings.ResourceManager, new object[]
			{
				serverName,
				logPath
			});
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000D8440 File Offset: 0x000D6640
		public static LocalizedString SearchIndexCrawlingNoProgress(string databaseName, string status, long numberOfDocumentsIndexedCrawler, DateTime startTime, DateTime endTime)
		{
			return new LocalizedString("SearchIndexCrawlingNoProgress", Strings.ResourceManager, new object[]
			{
				databaseName,
				status,
				numberOfDocumentsIndexedCrawler,
				startTime,
				endTime
			});
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000D8488 File Offset: 0x000D6688
		public static LocalizedString ActiveSyncDeepTestEscalationBodyDC(string serverName, string probeName)
		{
			return new LocalizedString("ActiveSyncDeepTestEscalationBodyDC", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002436 RID: 9270 RVA: 0x000D84B4 File Offset: 0x000D66B4
		public static LocalizedString IncompatibleVectorEscalationMessage
		{
			get
			{
				return new LocalizedString("IncompatibleVectorEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002437 RID: 9271 RVA: 0x000D84CB File Offset: 0x000D66CB
		public static LocalizedString DatabaseCorruptionEscalationMessage
		{
			get
			{
				return new LocalizedString("DatabaseCorruptionEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002438 RID: 9272 RVA: 0x000D84E2 File Offset: 0x000D66E2
		public static LocalizedString ReplicationOutdatedObjectsFailedEscalationMessage
		{
			get
			{
				return new LocalizedString("ReplicationOutdatedObjectsFailedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000D84FC File Offset: 0x000D66FC
		public static LocalizedString ComponentHealthPercentFailureEscalationMessageUnhealthy(int percentFailureThreshold, int monitoringIntervalMinutes)
		{
			return new LocalizedString("ComponentHealthPercentFailureEscalationMessageUnhealthy", Strings.ResourceManager, new object[]
			{
				percentFailureThreshold,
				monitoringIntervalMinutes
			});
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000D8534 File Offset: 0x000D6734
		public static LocalizedString RwsDatamartAvailabilityEscalationSubject(string serverName, string cName)
		{
			return new LocalizedString("RwsDatamartAvailabilityEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName,
				cName
			});
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000D8560 File Offset: 0x000D6760
		public static LocalizedString CafeServerNotOwner(string mailboxDatabaseName, string upn)
		{
			return new LocalizedString("CafeServerNotOwner", Strings.ResourceManager, new object[]
			{
				mailboxDatabaseName,
				upn
			});
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000D858C File Offset: 0x000D678C
		public static LocalizedString CafeOfflineFailedEscalationRecoveryDetails(string appPool)
		{
			return new LocalizedString("CafeOfflineFailedEscalationRecoveryDetails", Strings.ResourceManager, new object[]
			{
				appPool
			});
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000D85B4 File Offset: 0x000D67B4
		public static LocalizedString SearchIndexCopyUnhealthy(string databaseName, string status, string errorMessage, string diagnosticInfoError, string nodesInfo)
		{
			return new LocalizedString("SearchIndexCopyUnhealthy", Strings.ResourceManager, new object[]
			{
				databaseName,
				status,
				errorMessage,
				diagnosticInfoError,
				nodesInfo
			});
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x0600243E RID: 9278 RVA: 0x000D85ED File Offset: 0x000D67ED
		public static LocalizedString DatabaseCorruptEscalationMessage
		{
			get
			{
				return new LocalizedString("DatabaseCorruptEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x000D8604 File Offset: 0x000D6804
		public static LocalizedString HealthSetAlertSuppressionWarning
		{
			get
			{
				return new LocalizedString("HealthSetAlertSuppressionWarning", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000D861C File Offset: 0x000D681C
		public static LocalizedString SearchInstantSearchStxZeroHitMonitoringMailbox(string query, string mailboxSmtpAddress, string timestamp)
		{
			return new LocalizedString("SearchInstantSearchStxZeroHitMonitoringMailbox", Strings.ResourceManager, new object[]
			{
				query,
				mailboxSmtpAddress,
				timestamp
			});
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000D864C File Offset: 0x000D684C
		public static LocalizedString ImapSelfTestEscalationBodyENT(string serverName, string probeName)
		{
			return new LocalizedString("ImapSelfTestEscalationBodyENT", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000D8678 File Offset: 0x000D6878
		public static LocalizedString StalledCopyEscalationSubject(string component, string machine, string dbCopy, string threshold)
		{
			return new LocalizedString("StalledCopyEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				dbCopy,
				threshold
			});
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x000D86AC File Offset: 0x000D68AC
		public static LocalizedString OwaIMInitializationFailedMessage
		{
			get
			{
				return new LocalizedString("OwaIMInitializationFailedMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000D86C4 File Offset: 0x000D68C4
		public static LocalizedString InsufficientRedundancyEscalationMessage(string database)
		{
			return new LocalizedString("InsufficientRedundancyEscalationMessage", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000D86EC File Offset: 0x000D68EC
		public static LocalizedString DatabaseLogicalPhysicalSizeRatioEscalationMessageDc(double threshold, TimeSpan duration, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("DatabaseLogicalPhysicalSizeRatioEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				threshold,
				duration,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002446 RID: 9286 RVA: 0x000D8726 File Offset: 0x000D6926
		public static LocalizedString ForwardSyncHaltEscalationMessage
		{
			get
			{
				return new LocalizedString("ForwardSyncHaltEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000D8740 File Offset: 0x000D6940
		public static LocalizedString LocalMachineDriveBootVolumeEncryptionStateEscalationMessage(string serverName)
		{
			return new LocalizedString("LocalMachineDriveBootVolumeEncryptionStateEscalationMessage", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000D8768 File Offset: 0x000D6968
		public static LocalizedString LocalMachineDriveNotProtectedWithDraEscalationMessage(string volumes, string serverName)
		{
			return new LocalizedString("LocalMachineDriveNotProtectedWithDraEscalationMessage", Strings.ResourceManager, new object[]
			{
				volumes,
				serverName
			});
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000D8794 File Offset: 0x000D6994
		public static LocalizedString ReplServiceCrashEscalationMessage(int times, int hour)
		{
			return new LocalizedString("ReplServiceCrashEscalationMessage", Strings.ResourceManager, new object[]
			{
				times,
				hour
			});
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000D87CC File Offset: 0x000D69CC
		public static LocalizedString PassiveDatabaseAvailabilityEscalationMessageEnt(string invokeNowCommand, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("PassiveDatabaseAvailabilityEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				invokeNowCommand,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x000D87F8 File Offset: 0x000D69F8
		public static LocalizedString OfflineGLSEscalationMessage
		{
			get
			{
				return new LocalizedString("OfflineGLSEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000D8810 File Offset: 0x000D6A10
		public static LocalizedString CircularLoggingDisabledEscalationSubject(string component, string machine, string database)
		{
			return new LocalizedString("CircularLoggingDisabledEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				database
			});
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x000D8840 File Offset: 0x000D6A40
		public static LocalizedString UnableToRunEscalateByDatabaseHealthResponder
		{
			get
			{
				return new LocalizedString("UnableToRunEscalateByDatabaseHealthResponder", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000D8858 File Offset: 0x000D6A58
		public static LocalizedString AttributeMissingFromProbeDefinition(string attributeName)
		{
			return new LocalizedString("AttributeMissingFromProbeDefinition", Strings.ResourceManager, new object[]
			{
				attributeName
			});
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000D8880 File Offset: 0x000D6A80
		public static LocalizedString UnableToGetDatabaseState(string database)
		{
			return new LocalizedString("UnableToGetDatabaseState", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000D88A8 File Offset: 0x000D6AA8
		public static LocalizedString ProcessorTimeExceededErrorThresholdWithAffinitizationMessage(string processName)
		{
			return new LocalizedString("ProcessorTimeExceededErrorThresholdWithAffinitizationMessage", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x000D88D0 File Offset: 0x000D6AD0
		public static LocalizedString AggregateDeliveryQueueLengthEscalationMessage
		{
			get
			{
				return new LocalizedString("AggregateDeliveryQueueLengthEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000D88E8 File Offset: 0x000D6AE8
		public static LocalizedString DatabaseCopyBehindEscalationSubject(string component, string machine, string dbCopy, int threshold)
		{
			return new LocalizedString("DatabaseCopyBehindEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				dbCopy,
				threshold
			});
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002453 RID: 9299 RVA: 0x000D8921 File Offset: 0x000D6B21
		public static LocalizedString NoCafeMonitoringAccountsAvailable
		{
			get
			{
				return new LocalizedString("NoCafeMonitoringAccountsAvailable", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x000D8938 File Offset: 0x000D6B38
		public static LocalizedString MediaEdgeResourceAllocationFailedEscalationMessage
		{
			get
			{
				return new LocalizedString("MediaEdgeResourceAllocationFailedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002455 RID: 9301 RVA: 0x000D894F File Offset: 0x000D6B4F
		public static LocalizedString DRAPendingReplication5MinutesEscalationMessage
		{
			get
			{
				return new LocalizedString("DRAPendingReplication5MinutesEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06002456 RID: 9302 RVA: 0x000D8966 File Offset: 0x000D6B66
		public static LocalizedString SchemaPartitionFailedEscalationMessage
		{
			get
			{
				return new LocalizedString("SchemaPartitionFailedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000D8980 File Offset: 0x000D6B80
		public static LocalizedString SearchIndexCopyStatusError(string copyName, string result, string errorMessage)
		{
			return new LocalizedString("SearchIndexCopyStatusError", Strings.ResourceManager, new object[]
			{
				copyName,
				result,
				errorMessage
			});
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x000D89B0 File Offset: 0x000D6BB0
		public static LocalizedString DatabaseSchemaVersionCheckEscalationSubject
		{
			get
			{
				return new LocalizedString("DatabaseSchemaVersionCheckEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000D89C8 File Offset: 0x000D6BC8
		public static LocalizedString DatabaseDiskReadLatencyEscalationMessageEnt(TimeSpan duration, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("DatabaseDiskReadLatencyEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				duration,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x000D89F9 File Offset: 0x000D6BF9
		public static LocalizedString UMSipListeningPort
		{
			get
			{
				return new LocalizedString("UMSipListeningPort", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000D8A10 File Offset: 0x000D6C10
		public static LocalizedString UnableToGetDatabaseSize(string database)
		{
			return new LocalizedString("UnableToGetDatabaseSize", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x000D8A38 File Offset: 0x000D6C38
		public static LocalizedString ELCMailboxSLAEscalationSubject
		{
			get
			{
				return new LocalizedString("ELCMailboxSLAEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000D8A50 File Offset: 0x000D6C50
		public static LocalizedString LocalMachineDriveNotProtectedWithDraEscalationSubject(string serverName)
		{
			return new LocalizedString("LocalMachineDriveNotProtectedWithDraEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000D8A78 File Offset: 0x000D6C78
		public static LocalizedString StoreProcessRepeatedlyCrashingEscalationMessageDc(string processName, int count, TimeSpan duration)
		{
			return new LocalizedString("StoreProcessRepeatedlyCrashingEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				processName,
				count,
				duration
			});
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000D8AB4 File Offset: 0x000D6CB4
		public static LocalizedString SearchCatalogNotLoaded(string databaseName, string serverName, string diagnosticInfoXml)
		{
			return new LocalizedString("SearchCatalogNotLoaded", Strings.ResourceManager, new object[]
			{
				databaseName,
				serverName,
				diagnosticInfoXml
			});
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000D8AE4 File Offset: 0x000D6CE4
		public static LocalizedString ActiveSyncDeepTestEscalationBodyENT(string serverName, string probeName)
		{
			return new LocalizedString("ActiveSyncDeepTestEscalationBodyENT", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000D8B10 File Offset: 0x000D6D10
		public static LocalizedString DatabasePercentRPCRequestsEscalationMessageEnt(string databaseName, int percentRequests, TimeSpan duration)
		{
			return new LocalizedString("DatabasePercentRPCRequestsEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				databaseName,
				percentRequests,
				duration
			});
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x000D8B4A File Offset: 0x000D6D4A
		public static LocalizedString DHCPNacksEscalationMessage
		{
			get
			{
				return new LocalizedString("DHCPNacksEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x000D8B61 File Offset: 0x000D6D61
		public static LocalizedString ELCArchiveDumpsterEscalationMessage
		{
			get
			{
				return new LocalizedString("ELCArchiveDumpsterEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x000D8B78 File Offset: 0x000D6D78
		public static LocalizedString KDCServiceStatusTestMessage
		{
			get
			{
				return new LocalizedString("KDCServiceStatusTestMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000D8B90 File Offset: 0x000D6D90
		public static LocalizedString DatabaseRPCLatencyEscalationMessageEnt(string databaseName, int latency, TimeSpan duration)
		{
			return new LocalizedString("DatabaseRPCLatencyEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				databaseName,
				latency,
				duration
			});
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x000D8BCA File Offset: 0x000D6DCA
		public static LocalizedString LowMemoryUnderThresholdErrorEscalationSubject
		{
			get
			{
				return new LocalizedString("LowMemoryUnderThresholdErrorEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x000D8BE1 File Offset: 0x000D6DE1
		public static LocalizedString OwaIMInitializationFailedSubject
		{
			get
			{
				return new LocalizedString("OwaIMInitializationFailedSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000D8BF8 File Offset: 0x000D6DF8
		public static LocalizedString ForwardSyncStandardCompanyEscalationMessage(int count, int duration)
		{
			return new LocalizedString("ForwardSyncStandardCompanyEscalationMessage", Strings.ResourceManager, new object[]
			{
				count,
				duration
			});
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000D8C30 File Offset: 0x000D6E30
		public static LocalizedString PutDCIntoMMFailureEscalateMessage(string originalError, string dcFQDN)
		{
			return new LocalizedString("PutDCIntoMMFailureEscalateMessage", Strings.ResourceManager, new object[]
			{
				originalError,
				dcFQDN
			});
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000D8C5C File Offset: 0x000D6E5C
		public static LocalizedString HostControllerServiceNodeOperationFailed(string nodeName, string operation)
		{
			return new LocalizedString("HostControllerServiceNodeOperationFailed", Strings.ResourceManager, new object[]
			{
				nodeName,
				operation
			});
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600246B RID: 9323 RVA: 0x000D8C88 File Offset: 0x000D6E88
		public static LocalizedString PingConnectivityEscalationSubject
		{
			get
			{
				return new LocalizedString("PingConnectivityEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x000D8C9F File Offset: 0x000D6E9F
		public static LocalizedString PublicFolderConnectionCountEscalationMessage
		{
			get
			{
				return new LocalizedString("PublicFolderConnectionCountEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x000D8CB6 File Offset: 0x000D6EB6
		public static LocalizedString FastNodeNotHealthyEscalationMessage
		{
			get
			{
				return new LocalizedString("FastNodeNotHealthyEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000D8CD0 File Offset: 0x000D6ED0
		public static LocalizedString ObserverHeartbeatEscalateResponderSubject(string subject)
		{
			return new LocalizedString("ObserverHeartbeatEscalateResponderSubject", Strings.ResourceManager, new object[]
			{
				subject
			});
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000D8CF8 File Offset: 0x000D6EF8
		public static LocalizedString AntimalwareEngineErrorsEscalationMessage(string engineName, double threshold, int duration)
		{
			return new LocalizedString("AntimalwareEngineErrorsEscalationMessage", Strings.ResourceManager, new object[]
			{
				engineName,
				threshold,
				duration
			});
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000D8D34 File Offset: 0x000D6F34
		public static LocalizedString AuditLogSearchServiceletEscalationMessage(string serverName)
		{
			return new LocalizedString("AuditLogSearchServiceletEscalationMessage", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000D8D5C File Offset: 0x000D6F5C
		public static LocalizedString OwaMailboxDatabaseDoesntExist(string targetResource)
		{
			return new LocalizedString("OwaMailboxDatabaseDoesntExist", Strings.ResourceManager, new object[]
			{
				targetResource
			});
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x000D8D84 File Offset: 0x000D6F84
		public static LocalizedString CheckDCMMDivergenceScriptExceptionMessage
		{
			get
			{
				return new LocalizedString("CheckDCMMDivergenceScriptExceptionMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000D8D9C File Offset: 0x000D6F9C
		public static LocalizedString OwaTooManyWebAppStartsBody(string serverName)
		{
			return new LocalizedString("OwaTooManyWebAppStartsBody", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000D8DC4 File Offset: 0x000D6FC4
		public static LocalizedString EscalationMessageFailuresUnhealthy(string customMessage)
		{
			return new LocalizedString("EscalationMessageFailuresUnhealthy", Strings.ResourceManager, new object[]
			{
				customMessage
			});
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000D8DEC File Offset: 0x000D6FEC
		public static LocalizedString SearchQueryStxZeroHitMonitoringMailbox(string query, string mailboxSmtpAddress, string timestamp, string errorEvents)
		{
			return new LocalizedString("SearchQueryStxZeroHitMonitoringMailbox", Strings.ResourceManager, new object[]
			{
				query,
				mailboxSmtpAddress,
				timestamp,
				errorEvents
			});
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000D8E20 File Offset: 0x000D7020
		public static LocalizedString ClusterServiceCrashEscalationMessage(int times, int hour, int duration)
		{
			return new LocalizedString("ClusterServiceCrashEscalationMessage", Strings.ResourceManager, new object[]
			{
				times,
				hour,
				duration
			});
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000D8E60 File Offset: 0x000D7060
		public static LocalizedString OWACalendarAppPoolEscalationSubject(string serverName)
		{
			return new LocalizedString("OWACalendarAppPoolEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x000D8E88 File Offset: 0x000D7088
		public static LocalizedString CrossPremiseMailflowEscalationMessage
		{
			get
			{
				return new LocalizedString("CrossPremiseMailflowEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000D8EA0 File Offset: 0x000D70A0
		public static LocalizedString UMSipOptionsToUMServiceFailedEscalationSubject(string serverName)
		{
			return new LocalizedString("UMSipOptionsToUMServiceFailedEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600247A RID: 9338 RVA: 0x000D8EC8 File Offset: 0x000D70C8
		public static LocalizedString ForwardSyncStandardCompanyEscalationSubject
		{
			get
			{
				return new LocalizedString("ForwardSyncStandardCompanyEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000D8EE0 File Offset: 0x000D70E0
		public static LocalizedString PutDCIntoMMSuccessNotificationMessage(string originalError, string dcFQDN)
		{
			return new LocalizedString("PutDCIntoMMSuccessNotificationMessage", Strings.ResourceManager, new object[]
			{
				originalError,
				dcFQDN
			});
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000D8F0C File Offset: 0x000D710C
		public static LocalizedString EseDbDivergenceDetectedEscalationMessage(string machine, string database)
		{
			return new LocalizedString("EseDbDivergenceDetectedEscalationMessage", Strings.ResourceManager, new object[]
			{
				machine,
				database
			});
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x000D8F38 File Offset: 0x000D7138
		public static LocalizedString JournalArchiveEscalationSubject
		{
			get
			{
				return new LocalizedString("JournalArchiveEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000D8F50 File Offset: 0x000D7150
		public static LocalizedString ClusterServiceCrashEscalationSubject(string component, string target, int times, int hour)
		{
			return new LocalizedString("ClusterServiceCrashEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				target,
				times,
				hour
			});
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x000D8F8E File Offset: 0x000D718E
		public static LocalizedString DoMTConnectivityEscalateMessage
		{
			get
			{
				return new LocalizedString("DoMTConnectivityEscalateMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x000D8FA5 File Offset: 0x000D71A5
		public static LocalizedString InferenceComponentDisabledEscalationMessage
		{
			get
			{
				return new LocalizedString("InferenceComponentDisabledEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000D8FBC File Offset: 0x000D71BC
		public static LocalizedString LocalDriveLogSpaceEscalationSubject(string drive, TimeSpan duration)
		{
			return new LocalizedString("LocalDriveLogSpaceEscalationSubject", Strings.ResourceManager, new object[]
			{
				drive,
				duration
			});
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000D8FF0 File Offset: 0x000D71F0
		public static LocalizedString ImapDeepTestEscalationBodyDC(string serverName, string probeName)
		{
			return new LocalizedString("ImapDeepTestEscalationBodyDC", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x000D901C File Offset: 0x000D721C
		public static LocalizedString NoBackendMonitoringAccountsAvailable
		{
			get
			{
				return new LocalizedString("NoBackendMonitoringAccountsAvailable", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x000D9033 File Offset: 0x000D7233
		public static LocalizedString ActiveDirectoryConnectivityEscalationMessage
		{
			get
			{
				return new LocalizedString("ActiveDirectoryConnectivityEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x000D904A File Offset: 0x000D724A
		public static LocalizedString SyntheticReplicationTransactionEscalationMessage
		{
			get
			{
				return new LocalizedString("SyntheticReplicationTransactionEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x000D9061 File Offset: 0x000D7261
		public static LocalizedString OabFileLoadExceptionEncounteredSubject
		{
			get
			{
				return new LocalizedString("OabFileLoadExceptionEncounteredSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x000D9078 File Offset: 0x000D7278
		public static LocalizedString ObserverHeartbeatEscalateResponderMessage(string subject, string observer)
		{
			return new LocalizedString("ObserverHeartbeatEscalateResponderMessage", Strings.ResourceManager, new object[]
			{
				subject,
				observer
			});
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x000D90A4 File Offset: 0x000D72A4
		public static LocalizedString RegistryAccessDeniedEscalationMessage
		{
			get
			{
				return new LocalizedString("RegistryAccessDeniedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000D90BC File Offset: 0x000D72BC
		public static LocalizedString ReplServiceDownEscalationMessage(string machine, int restartService, int failoverTime, int bugcheckTime)
		{
			return new LocalizedString("ReplServiceDownEscalationMessage", Strings.ResourceManager, new object[]
			{
				machine,
				restartService,
				failoverTime,
				bugcheckTime
			});
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x000D90FF File Offset: 0x000D72FF
		public static LocalizedString AuditLogSearchServiceletEscalationSubject
		{
			get
			{
				return new LocalizedString("AuditLogSearchServiceletEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000D9118 File Offset: 0x000D7318
		public static LocalizedString RemoteStoreAdminRPCInterfaceEscalationEscalationMessageEnt(TimeSpan duration)
		{
			return new LocalizedString("RemoteStoreAdminRPCInterfaceEscalationEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				duration
			});
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x000D9145 File Offset: 0x000D7345
		public static LocalizedString EventLogProbeLogName
		{
			get
			{
				return new LocalizedString("EventLogProbeLogName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x000D915C File Offset: 0x000D735C
		public static LocalizedString Imap4ProtocolUnhealthy
		{
			get
			{
				return new LocalizedString("Imap4ProtocolUnhealthy", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000D9174 File Offset: 0x000D7374
		public static LocalizedString SearchNumberOfParserServersDegradation(int registryKey, int defaultRegistryKey, string memoryUsage)
		{
			return new LocalizedString("SearchNumberOfParserServersDegradation", Strings.ResourceManager, new object[]
			{
				registryKey,
				defaultRegistryKey,
				memoryUsage
			});
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000D91B0 File Offset: 0x000D73B0
		public static LocalizedString UnMonitoredDatabaseEscalationSubject(string component, string machine, string database, TimeSpan duration)
		{
			return new LocalizedString("UnMonitoredDatabaseEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				database,
				duration
			});
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000D91EC File Offset: 0x000D73EC
		public static LocalizedString NumberOfActiveBackgroundTasksEscalationMessageEnt(string databaseName, int threshold, TimeSpan duration)
		{
			return new LocalizedString("NumberOfActiveBackgroundTasksEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				databaseName,
				threshold,
				duration
			});
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000D9228 File Offset: 0x000D7428
		public static LocalizedString OwaIMSigninFailedMessage(string serverName)
		{
			return new LocalizedString("OwaIMSigninFailedMessage", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x000D9250 File Offset: 0x000D7450
		public static LocalizedString DLExpansionEscalationMessage
		{
			get
			{
				return new LocalizedString("DLExpansionEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000D9268 File Offset: 0x000D7468
		public static LocalizedString HostControllerNodeRestartDetails(string startTime, string endTime, string restarts)
		{
			return new LocalizedString("HostControllerNodeRestartDetails", Strings.ResourceManager, new object[]
			{
				startTime,
				endTime,
				restarts
			});
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x000D9298 File Offset: 0x000D7498
		public static LocalizedString ReplicationFailuresEscalationMessage
		{
			get
			{
				return new LocalizedString("ReplicationFailuresEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x000D92AF File Offset: 0x000D74AF
		public static LocalizedString SCTStateMonitoringScriptExceptionMessage
		{
			get
			{
				return new LocalizedString("SCTStateMonitoringScriptExceptionMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000D92C8 File Offset: 0x000D74C8
		public static LocalizedString AssistantsOutOfSlaError(string error)
		{
			return new LocalizedString("AssistantsOutOfSlaError", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000D92F0 File Offset: 0x000D74F0
		public static LocalizedString SearchIndexStall(string databaseName, string lastPollTimestamp, string serverName)
		{
			return new LocalizedString("SearchIndexStall", Strings.ResourceManager, new object[]
			{
				databaseName,
				lastPollTimestamp,
				serverName
			});
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000D9320 File Offset: 0x000D7520
		public static LocalizedString UMTranscriptionThrottledEscalationMessage(int percentageValue)
		{
			return new LocalizedString("UMTranscriptionThrottledEscalationMessage", Strings.ResourceManager, new object[]
			{
				percentageValue
			});
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000D9350 File Offset: 0x000D7550
		public static LocalizedString SiteFailureEscalationSubject(string component, string machine)
		{
			return new LocalizedString("SiteFailureEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine
			});
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x000D937C File Offset: 0x000D757C
		public static LocalizedString ELCExceptionEscalationMessage
		{
			get
			{
				return new LocalizedString("ELCExceptionEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000D9394 File Offset: 0x000D7594
		public static LocalizedString PopProxyTestEscalationBodyENT(string serverName, string probeName)
		{
			return new LocalizedString("PopProxyTestEscalationBodyENT", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000D93C0 File Offset: 0x000D75C0
		public static LocalizedString PushNotificationDatacenterBackendEndpointUnhealthy(string serverName)
		{
			return new LocalizedString("PushNotificationDatacenterBackendEndpointUnhealthy", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000D93E8 File Offset: 0x000D75E8
		public static LocalizedString ComponentHealthErrorHeader(int failedResults)
		{
			return new LocalizedString("ComponentHealthErrorHeader", Strings.ResourceManager, new object[]
			{
				failedResults
			});
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000D9418 File Offset: 0x000D7618
		public static LocalizedString ImapEscalationSubject(string probeName, string serverName)
		{
			return new LocalizedString("ImapEscalationSubject", Strings.ResourceManager, new object[]
			{
				probeName,
				serverName
			});
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000D9444 File Offset: 0x000D7644
		public static LocalizedString PushNotificationPublisherUnhealthy(string channelName, string serverName)
		{
			return new LocalizedString("PushNotificationPublisherUnhealthy", Strings.ResourceManager, new object[]
			{
				channelName,
				serverName
			});
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x000D9470 File Offset: 0x000D7670
		public static LocalizedString OabTooManyHttpErrorResponsesEncounteredBody
		{
			get
			{
				return new LocalizedString("OabTooManyHttpErrorResponsesEncounteredBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000D9488 File Offset: 0x000D7688
		public static LocalizedString EacSelfTestEscalationBody(string serverName, string logPath)
		{
			return new LocalizedString("EacSelfTestEscalationBody", Strings.ResourceManager, new object[]
			{
				serverName,
				logPath
			});
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060024A2 RID: 9378 RVA: 0x000D94B4 File Offset: 0x000D76B4
		public static LocalizedString QuarantineEscalationMessage
		{
			get
			{
				return new LocalizedString("QuarantineEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x000D94CB File Offset: 0x000D76CB
		public static LocalizedString TransportRejectingMessageSubmissions
		{
			get
			{
				return new LocalizedString("TransportRejectingMessageSubmissions", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060024A4 RID: 9380 RVA: 0x000D94E2 File Offset: 0x000D76E2
		public static LocalizedString PublicFolderConnectionCountEscalationSubject
		{
			get
			{
				return new LocalizedString("PublicFolderConnectionCountEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000D94FC File Offset: 0x000D76FC
		public static LocalizedString MonitoringAccountImproper(string mailboxDatabaseName, string upn)
		{
			return new LocalizedString("MonitoringAccountImproper", Strings.ResourceManager, new object[]
			{
				mailboxDatabaseName,
				upn
			});
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060024A6 RID: 9382 RVA: 0x000D9528 File Offset: 0x000D7728
		public static LocalizedString PowerShellProfileEscalationSubject
		{
			get
			{
				return new LocalizedString("PowerShellProfileEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060024A7 RID: 9383 RVA: 0x000D953F File Offset: 0x000D773F
		public static LocalizedString DivergenceBetweenCAAndAD1006EscalationMessage
		{
			get
			{
				return new LocalizedString("DivergenceBetweenCAAndAD1006EscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060024A8 RID: 9384 RVA: 0x000D9556 File Offset: 0x000D7756
		public static LocalizedString UnreachableQueueLengthEscalationMessage
		{
			get
			{
				return new LocalizedString("UnreachableQueueLengthEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000D9570 File Offset: 0x000D7770
		public static LocalizedString SearchQueryFailureEscalationMessage(string databaseName)
		{
			return new LocalizedString("SearchQueryFailureEscalationMessage", Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000D9598 File Offset: 0x000D7798
		public static LocalizedString PushNotificationCafeUnexpectedResponse(string response, string requestHeaders, string responseHeaders, string body)
		{
			return new LocalizedString("PushNotificationCafeUnexpectedResponse", Strings.ResourceManager, new object[]
			{
				response,
				requestHeaders,
				responseHeaders,
				body
			});
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000D95CC File Offset: 0x000D77CC
		public static LocalizedString ForwardSyncLiteCompanyEscalationMessage(int count, int duration)
		{
			return new LocalizedString("ForwardSyncLiteCompanyEscalationMessage", Strings.ResourceManager, new object[]
			{
				count,
				duration
			});
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x000D9602 File Offset: 0x000D7802
		public static LocalizedString OabFileLoadExceptionEncounteredBody
		{
			get
			{
				return new LocalizedString("OabFileLoadExceptionEncounteredBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000D961C File Offset: 0x000D781C
		public static LocalizedString LastDBDiscoveryTimeFailedMessage(string interval)
		{
			return new LocalizedString("LastDBDiscoveryTimeFailedMessage", Strings.ResourceManager, new object[]
			{
				interval
			});
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060024AE RID: 9390 RVA: 0x000D9644 File Offset: 0x000D7844
		public static LocalizedString PublicFolderSyncEscalationSubject
		{
			get
			{
				return new LocalizedString("PublicFolderSyncEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000D965C File Offset: 0x000D785C
		public static LocalizedString StoreAdminRPCInterfaceEscalationSubject(TimeSpan duration)
		{
			return new LocalizedString("StoreAdminRPCInterfaceEscalationSubject", Strings.ResourceManager, new object[]
			{
				duration
			});
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x000D9689 File Offset: 0x000D7889
		public static LocalizedString Imap4CommandProcessingTimeEscalationMessage
		{
			get
			{
				return new LocalizedString("Imap4CommandProcessingTimeEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000D96A0 File Offset: 0x000D78A0
		public static LocalizedString ProcessRepeatedlyCrashingEscalationMessage(string processName, int minCount, int durationMinutes)
		{
			return new LocalizedString("ProcessRepeatedlyCrashingEscalationMessage", Strings.ResourceManager, new object[]
			{
				processName,
				minCount,
				durationMinutes
			});
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x000D96DA File Offset: 0x000D78DA
		public static LocalizedString InvalidSearchResultsExceptionMessage
		{
			get
			{
				return new LocalizedString("InvalidSearchResultsExceptionMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000D96F4 File Offset: 0x000D78F4
		public static LocalizedString UMServiceRecentCallRejectedEscalationMessageString(int percentageValue)
		{
			return new LocalizedString("UMServiceRecentCallRejectedEscalationMessageString", Strings.ResourceManager, new object[]
			{
				percentageValue
			});
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x000D9721 File Offset: 0x000D7921
		public static LocalizedString SearchInformationNotAvailable
		{
			get
			{
				return new LocalizedString("SearchInformationNotAvailable", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x000D9738 File Offset: 0x000D7938
		public static LocalizedString ActiveDatabaseAvailabilityEscalationSubject
		{
			get
			{
				return new LocalizedString("ActiveDatabaseAvailabilityEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000D9750 File Offset: 0x000D7950
		public static LocalizedString InvokeNowAssemblyInfoFailure(string monitorIdentity)
		{
			return new LocalizedString("InvokeNowAssemblyInfoFailure", Strings.ResourceManager, new object[]
			{
				monitorIdentity
			});
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000D9778 File Offset: 0x000D7978
		public static LocalizedString UnableToGetDatabaseSchemaVersion(string database)
		{
			return new LocalizedString("UnableToGetDatabaseSchemaVersion", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x000D97A0 File Offset: 0x000D79A0
		public static LocalizedString ELCPermanentEscalationSubject
		{
			get
			{
				return new LocalizedString("ELCPermanentEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x000D97B7 File Offset: 0x000D79B7
		public static LocalizedString EventLogProbeGreenEvents
		{
			get
			{
				return new LocalizedString("EventLogProbeGreenEvents", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000D97D0 File Offset: 0x000D79D0
		public static LocalizedString InferenceDisabledComponentDetails(string componentName, string location)
		{
			return new LocalizedString("InferenceDisabledComponentDetails", Strings.ResourceManager, new object[]
			{
				componentName,
				location
			});
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060024BB RID: 9403 RVA: 0x000D97FC File Offset: 0x000D79FC
		public static LocalizedString ClusterHangEscalationMessage
		{
			get
			{
				return new LocalizedString("ClusterHangEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x000D9813 File Offset: 0x000D7A13
		public static LocalizedString FEPServiceNotRunningEscalationMessage
		{
			get
			{
				return new LocalizedString("FEPServiceNotRunningEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000D982C File Offset: 0x000D7A2C
		public static LocalizedString LocalMachineDriveEncryptionSuspendEscalationSubject(string serverName)
		{
			return new LocalizedString("LocalMachineDriveEncryptionSuspendEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000D9854 File Offset: 0x000D7A54
		public static LocalizedString QuarantinedMailboxEscalationSubject(string databaseName)
		{
			return new LocalizedString("QuarantinedMailboxEscalationSubject", Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000D987C File Offset: 0x000D7A7C
		public static LocalizedString SearchLocalCopyStatusEscalationMessage(string databaseName)
		{
			return new LocalizedString("SearchLocalCopyStatusEscalationMessage", Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000D98A4 File Offset: 0x000D7AA4
		public static LocalizedString ProcessCrashDetectionEscalationMessage(string process)
		{
			return new LocalizedString("ProcessCrashDetectionEscalationMessage", Strings.ResourceManager, new object[]
			{
				process
			});
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000D98CC File Offset: 0x000D7ACC
		public static LocalizedString LogVolumeSpaceEscalationSubject(string component, string machine, string database)
		{
			return new LocalizedString("LogVolumeSpaceEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				database
			});
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000D98FC File Offset: 0x000D7AFC
		public static LocalizedString ClusterGroupDownEscalationSubject(string component, string target, int threshold)
		{
			return new LocalizedString("ClusterGroupDownEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				target,
				threshold
			});
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000D9934 File Offset: 0x000D7B34
		public static LocalizedString LocalMachineDriveBootVolumeEncryptionStateEscalationSubject(string serverName)
		{
			return new LocalizedString("LocalMachineDriveBootVolumeEncryptionStateEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000D995C File Offset: 0x000D7B5C
		public static LocalizedString DatabaseCopySlowReplayEscalationSubject(string component, string machine, string dbCopy, int threshold)
		{
			return new LocalizedString("DatabaseCopySlowReplayEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				dbCopy,
				threshold
			});
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x000D9995 File Offset: 0x000D7B95
		public static LocalizedString RidMonitorEscalationMessage
		{
			get
			{
				return new LocalizedString("RidMonitorEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x000D99AC File Offset: 0x000D7BAC
		public static LocalizedString SystemMailboxGuidNotFound
		{
			get
			{
				return new LocalizedString("SystemMailboxGuidNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x000D99C3 File Offset: 0x000D7BC3
		public static LocalizedString MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedEscalationMessage
		{
			get
			{
				return new LocalizedString("MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000D99DC File Offset: 0x000D7BDC
		public static LocalizedString SearchIndexBacklogWithHistory(string databaseName, string backlog, string retryQueue, string lastTime, string lastBacklog, string lastRetryQueue, string upTime, string serverStatus)
		{
			return new LocalizedString("SearchIndexBacklogWithHistory", Strings.ResourceManager, new object[]
			{
				databaseName,
				backlog,
				retryQueue,
				lastTime,
				lastBacklog,
				lastRetryQueue,
				upTime,
				serverStatus
			});
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000D9A24 File Offset: 0x000D7C24
		public static LocalizedString EacCtpTestEscalationBody(string serverName, string logPath)
		{
			return new LocalizedString("EacCtpTestEscalationBody", Strings.ResourceManager, new object[]
			{
				serverName,
				logPath
			});
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000D9A50 File Offset: 0x000D7C50
		public static LocalizedString OabMailboxEscalationSubject(string oabGuid, string serverName)
		{
			return new LocalizedString("OabMailboxEscalationSubject", Strings.ResourceManager, new object[]
			{
				oabGuid,
				serverName
			});
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060024CB RID: 9419 RVA: 0x000D9A7C File Offset: 0x000D7C7C
		public static LocalizedString SearchTransportAgentFailureEscalationMessage
		{
			get
			{
				return new LocalizedString("SearchTransportAgentFailureEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x000D9A93 File Offset: 0x000D7C93
		public static LocalizedString TransportMessageCategorizationEscalationMessage
		{
			get
			{
				return new LocalizedString("TransportMessageCategorizationEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000D9AAC File Offset: 0x000D7CAC
		public static LocalizedString ServerInMaintenanceModeForTooLongEscalationSubject(string component, string machine, string threshold)
		{
			return new LocalizedString("ServerInMaintenanceModeForTooLongEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				threshold
			});
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000D9ADC File Offset: 0x000D7CDC
		public static LocalizedString ServiceNotRunningEscalationMessageEnt(string serviceName)
		{
			return new LocalizedString("ServiceNotRunningEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				serviceName
			});
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000D9B04 File Offset: 0x000D7D04
		public static LocalizedString DatabaseCopySlowReplayEscalationMessage(string database, int threshold)
		{
			return new LocalizedString("DatabaseCopySlowReplayEscalationMessage", Strings.ResourceManager, new object[]
			{
				database,
				threshold
			});
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000D9B38 File Offset: 0x000D7D38
		public static LocalizedString UMGrammarUsageEscalationMessage(int percentageValue)
		{
			return new LocalizedString("UMGrammarUsageEscalationMessage", Strings.ResourceManager, new object[]
			{
				percentageValue
			});
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000D9B68 File Offset: 0x000D7D68
		public static LocalizedString FireWallEscalationMessage(int count, int duration)
		{
			return new LocalizedString("FireWallEscalationMessage", Strings.ResourceManager, new object[]
			{
				count,
				duration
			});
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x000D9B9E File Offset: 0x000D7D9E
		public static LocalizedString InocrrectSCTStateExceptionMessage
		{
			get
			{
				return new LocalizedString("InocrrectSCTStateExceptionMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000D9BB8 File Offset: 0x000D7DB8
		public static LocalizedString HAPassiveCopyUnhealthy(string copyState)
		{
			return new LocalizedString("HAPassiveCopyUnhealthy", Strings.ResourceManager, new object[]
			{
				copyState
			});
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000D9BE0 File Offset: 0x000D7DE0
		public static LocalizedString UMSipOptionsToUMServiceFailedEscalationBody(string serverName)
		{
			return new LocalizedString("UMSipOptionsToUMServiceFailedEscalationBody", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x000D9C08 File Offset: 0x000D7E08
		public static LocalizedString DataIssueEscalationMessage
		{
			get
			{
				return new LocalizedString("DataIssueEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000D9C20 File Offset: 0x000D7E20
		public static LocalizedString MailSubmissionBehindWatermarksEscalationSubject(TimeSpan ageThreshold, TimeSpan duration, string databaseName)
		{
			return new LocalizedString("MailSubmissionBehindWatermarksEscalationSubject", Strings.ResourceManager, new object[]
			{
				ageThreshold,
				duration,
				databaseName
			});
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000D9C5C File Offset: 0x000D7E5C
		public static LocalizedString ComponentHealthErrorContent(string componentName, string resultName, DateTime executionEndTime)
		{
			return new LocalizedString("ComponentHealthErrorContent", Strings.ResourceManager, new object[]
			{
				componentName,
				resultName,
				executionEndTime
			});
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x000D9C91 File Offset: 0x000D7E91
		public static LocalizedString KerbAuthFailureEscalationMessagPAC
		{
			get
			{
				return new LocalizedString("KerbAuthFailureEscalationMessagPAC", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000D9CA8 File Offset: 0x000D7EA8
		public static LocalizedString PrivateWorkingSetExceededWarningThresholdMessage(string processName)
		{
			return new LocalizedString("PrivateWorkingSetExceededWarningThresholdMessage", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x000D9CD0 File Offset: 0x000D7ED0
		public static LocalizedString DivergenceInDefinitionEscalationMessage
		{
			get
			{
				return new LocalizedString("DivergenceInDefinitionEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060024DB RID: 9435 RVA: 0x000D9CE7 File Offset: 0x000D7EE7
		public static LocalizedString MobilityAccount
		{
			get
			{
				return new LocalizedString("MobilityAccount", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000D9D00 File Offset: 0x000D7F00
		public static LocalizedString ActiveSyncSelfTestEscalationBodyDC(string serverName, string probeName)
		{
			return new LocalizedString("ActiveSyncSelfTestEscalationBodyDC", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000D9D2C File Offset: 0x000D7F2C
		public static LocalizedString NTFSCorruptionEscalationMessage(string database, string threshold)
		{
			return new LocalizedString("NTFSCorruptionEscalationMessage", Strings.ResourceManager, new object[]
			{
				database,
				threshold
			});
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000D9D58 File Offset: 0x000D7F58
		public static LocalizedString FailedAndSuspendedCopyEscalationMessage(string database, string threshold)
		{
			return new LocalizedString("FailedAndSuspendedCopyEscalationMessage", Strings.ResourceManager, new object[]
			{
				database,
				threshold
			});
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000D9D84 File Offset: 0x000D7F84
		public static LocalizedString OneCopyMonitorFailureMessage(int duration, int threshold)
		{
			return new LocalizedString("OneCopyMonitorFailureMessage", Strings.ResourceManager, new object[]
			{
				duration,
				threshold
			});
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000D9DBC File Offset: 0x000D7FBC
		public static LocalizedString EventAssistantsProcessRepeatedlyCrashingEscalationMessageEnt(string processName, int count, TimeSpan duration)
		{
			return new LocalizedString("EventAssistantsProcessRepeatedlyCrashingEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				processName,
				count,
				duration
			});
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x000D9DF6 File Offset: 0x000D7FF6
		public static LocalizedString OwaTooManyLogoffFailuresBody
		{
			get
			{
				return new LocalizedString("OwaTooManyLogoffFailuresBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x000D9E0D File Offset: 0x000D800D
		public static LocalizedString ForwardSyncProcessRepeatedlyCrashingEscalationSubject
		{
			get
			{
				return new LocalizedString("ForwardSyncProcessRepeatedlyCrashingEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000D9E24 File Offset: 0x000D8024
		public static LocalizedString PutMultipleDCIntoMMFailureEscalateMessage(string dcFQDN)
		{
			return new LocalizedString("PutMultipleDCIntoMMFailureEscalateMessage", Strings.ResourceManager, new object[]
			{
				dcFQDN
			});
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000D9E4C File Offset: 0x000D804C
		public static LocalizedString SystemDriveSpaceEscalationMessage(string machine, string drive, string threshold)
		{
			return new LocalizedString("SystemDriveSpaceEscalationMessage", Strings.ResourceManager, new object[]
			{
				machine,
				drive,
				threshold
			});
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000D9E7C File Offset: 0x000D807C
		public static LocalizedString DatabasePercentRPCRequestsEscalationSubject(string databaseName, int percentRequests)
		{
			return new LocalizedString("DatabasePercentRPCRequestsEscalationSubject", Strings.ResourceManager, new object[]
			{
				databaseName,
				percentRequests
			});
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000D9EB0 File Offset: 0x000D80B0
		public static LocalizedString SearchQuerySlow(string databaseName, string slowRate, string threshold)
		{
			return new LocalizedString("SearchQuerySlow", Strings.ResourceManager, new object[]
			{
				databaseName,
				slowRate,
				threshold
			});
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x000D9EE0 File Offset: 0x000D80E0
		public static LocalizedString ADDatabaseCorruptionEscalationMessage
		{
			get
			{
				return new LocalizedString("ADDatabaseCorruptionEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000D9EF8 File Offset: 0x000D80F8
		public static LocalizedString SearchIndexSuspendedEscalationMessage(string databaseName)
		{
			return new LocalizedString("SearchIndexSuspendedEscalationMessage", Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000D9F20 File Offset: 0x000D8120
		public static LocalizedString InvokeNowInvalidWorkDefinition(string requestId)
		{
			return new LocalizedString("InvokeNowInvalidWorkDefinition", Strings.ResourceManager, new object[]
			{
				requestId
			});
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000D9F48 File Offset: 0x000D8148
		public static LocalizedString OabTooManyWebAppStartsBody(string serverName)
		{
			return new LocalizedString("OabTooManyWebAppStartsBody", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000D9F70 File Offset: 0x000D8170
		public static LocalizedString HostControllerServiceNodeUnhealthy(string details)
		{
			return new LocalizedString("HostControllerServiceNodeUnhealthy", Strings.ResourceManager, new object[]
			{
				details
			});
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000D9F98 File Offset: 0x000D8198
		public static LocalizedString HostControllerExcessiveNodeRestarts(string nodeName, string count, string minutes, string details)
		{
			return new LocalizedString("HostControllerExcessiveNodeRestarts", Strings.ResourceManager, new object[]
			{
				nodeName,
				count,
				minutes,
				details
			});
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x000D9FCC File Offset: 0x000D81CC
		public static LocalizedString MailboxAuditingAvailabilityFailureEscalationSubject
		{
			get
			{
				return new LocalizedString("MailboxAuditingAvailabilityFailureEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x000D9FE3 File Offset: 0x000D81E3
		public static LocalizedString TopologyServiceConnectivityEscalationMessage
		{
			get
			{
				return new LocalizedString("TopologyServiceConnectivityEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000D9FFC File Offset: 0x000D81FC
		public static LocalizedString OwaDeepTestEscalationHtmlBody(string serverName, string logPath)
		{
			return new LocalizedString("OwaDeepTestEscalationHtmlBody", Strings.ResourceManager, new object[]
			{
				serverName,
				logPath
			});
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000DA028 File Offset: 0x000D8228
		public static LocalizedString SuspendedCopyEscalationMessage(string database, int threshold)
		{
			return new LocalizedString("SuspendedCopyEscalationMessage", Strings.ResourceManager, new object[]
			{
				database,
				threshold
			});
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x000DA059 File Offset: 0x000D8259
		public static LocalizedString UMSipTransport
		{
			get
			{
				return new LocalizedString("UMSipTransport", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x000DA070 File Offset: 0x000D8270
		public static LocalizedString OabProtocolEscalationBody
		{
			get
			{
				return new LocalizedString("OabProtocolEscalationBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x000DA087 File Offset: 0x000D8287
		public static LocalizedString PushNotificationEnterpriseEmptyServiceUri
		{
			get
			{
				return new LocalizedString("PushNotificationEnterpriseEmptyServiceUri", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x000DA09E File Offset: 0x000D829E
		public static LocalizedString PushNotificationEnterpriseAuthError
		{
			get
			{
				return new LocalizedString("PushNotificationEnterpriseAuthError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000DA0B8 File Offset: 0x000D82B8
		public static LocalizedString ImapProxyTestEscalationBodyENT(string serverName, string probeName)
		{
			return new LocalizedString("ImapProxyTestEscalationBodyENT", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000DA0E4 File Offset: 0x000D82E4
		public static LocalizedString SearchResourceLoadEscalationMessage(string databaseName, int minutes)
		{
			return new LocalizedString("SearchResourceLoadEscalationMessage", Strings.ResourceManager, new object[]
			{
				databaseName,
				minutes
			});
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000DA118 File Offset: 0x000D8318
		public static LocalizedString InvalidAccessToken(string userSid)
		{
			return new LocalizedString("InvalidAccessToken", Strings.ResourceManager, new object[]
			{
				userSid
			});
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x000DA140 File Offset: 0x000D8340
		public static LocalizedString LocalMachineDriveProtectedWithDraWithoutDecryptorEscalationMessage(string volumes, string serverName)
		{
			return new LocalizedString("LocalMachineDriveProtectedWithDraWithoutDecryptorEscalationMessage", Strings.ResourceManager, new object[]
			{
				volumes,
				serverName
			});
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x000DA16C File Offset: 0x000D836C
		public static LocalizedString UnableToRunAlertNotificationTypeByDatabaseCopyStateResponder
		{
			get
			{
				return new LocalizedString("UnableToRunAlertNotificationTypeByDatabaseCopyStateResponder", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x000DA183 File Offset: 0x000D8383
		public static LocalizedString ELCDumpsterWarningEscalationSubject
		{
			get
			{
				return new LocalizedString("ELCDumpsterWarningEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000DA19C File Offset: 0x000D839C
		public static LocalizedString MigrationNotificationSubject(string component, string notification)
		{
			return new LocalizedString("MigrationNotificationSubject", Strings.ResourceManager, new object[]
			{
				component,
				notification
			});
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000DA1C8 File Offset: 0x000D83C8
		public static LocalizedString PublicFolderSyncEscalationMessage(int minCount, int durationMinutes)
		{
			return new LocalizedString("PublicFolderSyncEscalationMessage", Strings.ResourceManager, new object[]
			{
				minCount,
				durationMinutes
			});
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000DA200 File Offset: 0x000D8400
		public static LocalizedString RcaWorkItemCreationSummaryEntry(int successful, int total)
		{
			return new LocalizedString("RcaWorkItemCreationSummaryEntry", Strings.ResourceManager, new object[]
			{
				successful,
				total
			});
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x000DA236 File Offset: 0x000D8436
		public static LocalizedString OabMailboxEscalationBody
		{
			get
			{
				return new LocalizedString("OabMailboxEscalationBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000DA250 File Offset: 0x000D8450
		public static LocalizedString WacDiscoveryFailureBody(string serverName)
		{
			return new LocalizedString("WacDiscoveryFailureBody", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x000DA278 File Offset: 0x000D8478
		public static LocalizedString CheckZombieDCEscalateMessage
		{
			get
			{
				return new LocalizedString("CheckZombieDCEscalateMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000DA290 File Offset: 0x000D8490
		public static LocalizedString HighLogGenerationRateEscalationSubject(string component, string machine, string dbCopy, int threshold)
		{
			return new LocalizedString("HighLogGenerationRateEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				dbCopy,
				threshold
			});
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x000DA2C9 File Offset: 0x000D84C9
		public static LocalizedString RidSetMonitorEscalationMessage
		{
			get
			{
				return new LocalizedString("RidSetMonitorEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x000DA2E0 File Offset: 0x000D84E0
		public static LocalizedString PushNotificationCafeEndpointUnhealthy
		{
			get
			{
				return new LocalizedString("PushNotificationCafeEndpointUnhealthy", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000DA2F8 File Offset: 0x000D84F8
		public static LocalizedString DatabaseLogicalPhysicalSizeRatioEscalationMessageEnt(double threshold, TimeSpan duration, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("DatabaseLogicalPhysicalSizeRatioEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				threshold,
				duration,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x000DA332 File Offset: 0x000D8532
		public static LocalizedString ProvisioningBigVolumeErrorEscalationMessage
		{
			get
			{
				return new LocalizedString("ProvisioningBigVolumeErrorEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000DA34C File Offset: 0x000D854C
		public static LocalizedString OwaSelfTestEscalationBody(string serverName, string logPath)
		{
			return new LocalizedString("OwaSelfTestEscalationBody", Strings.ResourceManager, new object[]
			{
				serverName,
				logPath
			});
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000DA378 File Offset: 0x000D8578
		public static LocalizedString MailboxAssistantsBehindWatermarksEscalationSubject(TimeSpan ageThreshold, TimeSpan duration)
		{
			return new LocalizedString("MailboxAssistantsBehindWatermarksEscalationSubject", Strings.ResourceManager, new object[]
			{
				ageThreshold,
				duration
			});
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002508 RID: 9480 RVA: 0x000DA3AE File Offset: 0x000D85AE
		public static LocalizedString PublicFolderMailboxQuotaEscalationMessage
		{
			get
			{
				return new LocalizedString("PublicFolderMailboxQuotaEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x000DA3C5 File Offset: 0x000D85C5
		public static LocalizedString CASRoutingFailureEscalationSubject
		{
			get
			{
				return new LocalizedString("CASRoutingFailureEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x000DA3DC File Offset: 0x000D85DC
		public static LocalizedString OAuthRequestFailureEscalationBody
		{
			get
			{
				return new LocalizedString("OAuthRequestFailureEscalationBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x000DA3F3 File Offset: 0x000D85F3
		public static LocalizedString GLSEscalationMessage
		{
			get
			{
				return new LocalizedString("GLSEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x000DA40A File Offset: 0x000D860A
		public static LocalizedString SCTNotFoundForAllVersionsExceptionMessage
		{
			get
			{
				return new LocalizedString("SCTNotFoundForAllVersionsExceptionMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000DA424 File Offset: 0x000D8624
		public static LocalizedString ControllerFailureMessage(string machine, int duration)
		{
			return new LocalizedString("ControllerFailureMessage", Strings.ResourceManager, new object[]
			{
				machine,
				duration
			});
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000DA458 File Offset: 0x000D8658
		public static LocalizedString HighLogGenerationRateEscalationMessage(string database, int threshold, string logGenThreshold)
		{
			return new LocalizedString("HighLogGenerationRateEscalationMessage", Strings.ResourceManager, new object[]
			{
				database,
				threshold,
				logGenThreshold
			});
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000DA490 File Offset: 0x000D8690
		public static LocalizedString SearchFeedingControllerFailureEscalationMessage(string databaseName, int threshold, int minutes)
		{
			return new LocalizedString("SearchFeedingControllerFailureEscalationMessage", Strings.ResourceManager, new object[]
			{
				databaseName,
				threshold,
				minutes
			});
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000DA4CC File Offset: 0x000D86CC
		public static LocalizedString ClusterNetworkDownEscalationSubject(string component, string target, int threshold)
		{
			return new LocalizedString("ClusterNetworkDownEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				target,
				threshold
			});
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x000DA501 File Offset: 0x000D8701
		public static LocalizedString SqlOutputStreamInRetryEscalationMessage
		{
			get
			{
				return new LocalizedString("SqlOutputStreamInRetryEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000DA518 File Offset: 0x000D8718
		public static LocalizedString FireWallEscalationSubject(string machine)
		{
			return new LocalizedString("FireWallEscalationSubject", Strings.ResourceManager, new object[]
			{
				machine
			});
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x000DA540 File Offset: 0x000D8740
		public static LocalizedString DefaultEscalationSubject
		{
			get
			{
				return new LocalizedString("DefaultEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002514 RID: 9492 RVA: 0x000DA557 File Offset: 0x000D8757
		public static LocalizedString MailboxAuditingAvailabilityFailureEscalationBody
		{
			get
			{
				return new LocalizedString("MailboxAuditingAvailabilityFailureEscalationBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002515 RID: 9493 RVA: 0x000DA56E File Offset: 0x000D876E
		public static LocalizedString BulkProvisioningNoProgressEscalationSubject
		{
			get
			{
				return new LocalizedString("BulkProvisioningNoProgressEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000DA588 File Offset: 0x000D8788
		public static LocalizedString ServerVersionNotFound(string serverName)
		{
			return new LocalizedString("ServerVersionNotFound", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000DA5B0 File Offset: 0x000D87B0
		public static LocalizedString EacDeepTestEscalationBody(string serverName, string logPath)
		{
			return new LocalizedString("EacDeepTestEscalationBody", Strings.ResourceManager, new object[]
			{
				serverName,
				logPath
			});
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002518 RID: 9496 RVA: 0x000DA5DC File Offset: 0x000D87DC
		public static LocalizedString InfrastructureValidationSubject
		{
			get
			{
				return new LocalizedString("InfrastructureValidationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x000DA5F3 File Offset: 0x000D87F3
		public static LocalizedString SearchMemoryUsageOverThresholdEscalationMessage
		{
			get
			{
				return new LocalizedString("SearchMemoryUsageOverThresholdEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000DA60C File Offset: 0x000D880C
		public static LocalizedString VersionBucketsAllocatedEscalationEscalationMessageDc(TimeSpan duration, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("VersionBucketsAllocatedEscalationEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				duration,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x000DA63D File Offset: 0x000D883D
		public static LocalizedString SharedCacheEscalationMessage
		{
			get
			{
				return new LocalizedString("SharedCacheEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000DA654 File Offset: 0x000D8854
		public static LocalizedString SearchIndexCrawlingWithHealthyCopy(string databaseName, string healthyCopyServerName)
		{
			return new LocalizedString("SearchIndexCrawlingWithHealthyCopy", Strings.ResourceManager, new object[]
			{
				databaseName,
				healthyCopyServerName
			});
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x000DA680 File Offset: 0x000D8880
		public static LocalizedString ProcessorTimeExceededErrorThresholdSubject(string processName)
		{
			return new LocalizedString("ProcessorTimeExceededErrorThresholdSubject", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000DA6A8 File Offset: 0x000D88A8
		public static LocalizedString MailboxAssistantsBehindWatermarksEscalationMessageEnt(TimeSpan ageThreshold, TimeSpan duration, string invokeNowCommand, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("MailboxAssistantsBehindWatermarksEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				ageThreshold,
				duration,
				invokeNowCommand,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x000DA6E6 File Offset: 0x000D88E6
		public static LocalizedString CannotRecoverEscalationMessage
		{
			get
			{
				return new LocalizedString("CannotRecoverEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x000DA6FD File Offset: 0x000D88FD
		public static LocalizedString AsynchronousAuditSearchAvailabilityFailureEscalationBody
		{
			get
			{
				return new LocalizedString("AsynchronousAuditSearchAvailabilityFailureEscalationBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x000DA714 File Offset: 0x000D8914
		public static LocalizedString OwaIMLogAnalyzerMessage
		{
			get
			{
				return new LocalizedString("OwaIMLogAnalyzerMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002522 RID: 9506 RVA: 0x000DA72B File Offset: 0x000D892B
		public static LocalizedString UMCertificateSubjectName
		{
			get
			{
				return new LocalizedString("UMCertificateSubjectName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000DA744 File Offset: 0x000D8944
		public static LocalizedString PassiveDatabaseAvailabilityEscalationMessageDc(string invokeNowCommand, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("PassiveDatabaseAvailabilityEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				invokeNowCommand,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002524 RID: 9508 RVA: 0x000DA770 File Offset: 0x000D8970
		public static LocalizedString OwaNoMailboxesAvailable
		{
			get
			{
				return new LocalizedString("OwaNoMailboxesAvailable", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000DA788 File Offset: 0x000D8988
		public static LocalizedString SuspendedCopyEscalationSubject(string component, string machine, string dbCopy, int threshold)
		{
			return new LocalizedString("SuspendedCopyEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				dbCopy,
				threshold
			});
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000DA7C4 File Offset: 0x000D89C4
		public static LocalizedString SearchCatalogSuspended(string databaseName, string databaseState)
		{
			return new LocalizedString("SearchCatalogSuspended", Strings.ResourceManager, new object[]
			{
				databaseName,
				databaseState
			});
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x000DA7F0 File Offset: 0x000D89F0
		public static LocalizedString UMRecentPartnerTranscriptionFailedEscalationMessageString(int percentageValue)
		{
			return new LocalizedString("UMRecentPartnerTranscriptionFailedEscalationMessageString", Strings.ResourceManager, new object[]
			{
				percentageValue
			});
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000DA820 File Offset: 0x000D8A20
		public static LocalizedString EseDbTimeAdvanceEscalationMessage(string machine, string database)
		{
			return new LocalizedString("EseDbTimeAdvanceEscalationMessage", Strings.ResourceManager, new object[]
			{
				machine,
				database
			});
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x000DA84C File Offset: 0x000D8A4C
		public static LocalizedString TransportCategorizerJobsUnavailableEscalationMessage
		{
			get
			{
				return new LocalizedString("TransportCategorizerJobsUnavailableEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x0600252A RID: 9514 RVA: 0x000DA863 File Offset: 0x000D8A63
		public static LocalizedString SingleAvailableDatabaseCopyEscalationMessage
		{
			get
			{
				return new LocalizedString("SingleAvailableDatabaseCopyEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x0600252B RID: 9515 RVA: 0x000DA87A File Offset: 0x000D8A7A
		public static LocalizedString DivergenceInSiteNameEscalationMessage
		{
			get
			{
				return new LocalizedString("DivergenceInSiteNameEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x000DA891 File Offset: 0x000D8A91
		public static LocalizedString NullSearchResponseExceptionMessage
		{
			get
			{
				return new LocalizedString("NullSearchResponseExceptionMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000DA8A8 File Offset: 0x000D8AA8
		public static LocalizedString ControllerFailureEscalationSubject(string component, string machine)
		{
			return new LocalizedString("ControllerFailureEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine
			});
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000DA8D4 File Offset: 0x000D8AD4
		public static LocalizedString SearchWordBreakerLoadingFailure(string nodeName, string timeStamp, string redEventId, string channelName, string greenEventId)
		{
			return new LocalizedString("SearchWordBreakerLoadingFailure", Strings.ResourceManager, new object[]
			{
				nodeName,
				timeStamp,
				redEventId,
				channelName,
				greenEventId
			});
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000DA910 File Offset: 0x000D8B10
		public static LocalizedString SearchIndexMultiCrawling(string databaseName, int count, string details)
		{
			return new LocalizedString("SearchIndexMultiCrawling", Strings.ResourceManager, new object[]
			{
				databaseName,
				count,
				details
			});
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000DA948 File Offset: 0x000D8B48
		public static LocalizedString AvailabilityServiceEscalationBody(string probeType)
		{
			return new LocalizedString("AvailabilityServiceEscalationBody", Strings.ResourceManager, new object[]
			{
				probeType
			});
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000DA970 File Offset: 0x000D8B70
		public static LocalizedString OwaSelfTestEscalationHtmlBody(string serverName, string logPath)
		{
			return new LocalizedString("OwaSelfTestEscalationHtmlBody", Strings.ResourceManager, new object[]
			{
				serverName,
				logPath
			});
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000DA99C File Offset: 0x000D8B9C
		public static LocalizedString ProcessorTimeExceededWarningThresholdSubject(string processName)
		{
			return new LocalizedString("ProcessorTimeExceededWarningThresholdSubject", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000DA9C4 File Offset: 0x000D8BC4
		public static LocalizedString SearchIndexSingleHealthyCopyWithSeeding(string databaseName, string details, string seedingStartTime)
		{
			return new LocalizedString("SearchIndexSingleHealthyCopyWithSeeding", Strings.ResourceManager, new object[]
			{
				databaseName,
				details,
				seedingStartTime
			});
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x000DA9F4 File Offset: 0x000D8BF4
		public static LocalizedString DatabasePercentRPCRequestsEscalationMessageDc(string databaseName, int percentRequests, TimeSpan duration)
		{
			return new LocalizedString("DatabasePercentRPCRequestsEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				databaseName,
				percentRequests,
				duration
			});
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000DAA30 File Offset: 0x000D8C30
		public static LocalizedString DatabaseCopyBehindEscalationMessage(string database, int threshold)
		{
			return new LocalizedString("DatabaseCopyBehindEscalationMessage", Strings.ResourceManager, new object[]
			{
				database,
				threshold
			});
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000DAA64 File Offset: 0x000D8C64
		public static LocalizedString EseInconsistentDataDetectedEscalationSubject(string component, string machine, string database)
		{
			return new LocalizedString("EseInconsistentDataDetectedEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				database
			});
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x000DAA94 File Offset: 0x000D8C94
		public static LocalizedString UncategorizedProcess
		{
			get
			{
				return new LocalizedString("UncategorizedProcess", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x000DAAAC File Offset: 0x000D8CAC
		public static LocalizedString DatabaseValidationNullRef(string database)
		{
			return new LocalizedString("DatabaseValidationNullRef", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000DAAD4 File Offset: 0x000D8CD4
		public static LocalizedString CafeThreadCountMessageUnhealthy(string appPool, string percentThreshold, string maxThreads)
		{
			return new LocalizedString("CafeThreadCountMessageUnhealthy", Strings.ResourceManager, new object[]
			{
				appPool,
				percentThreshold,
				maxThreads
			});
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x000DAB04 File Offset: 0x000D8D04
		public static LocalizedString MSExchangeProtectedServiceHostCrashingMessage
		{
			get
			{
				return new LocalizedString("MSExchangeProtectedServiceHostCrashingMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x000DAB1B File Offset: 0x000D8D1B
		public static LocalizedString UMDatacenterLoadBalancerSipOptionsPingEscalationMessage
		{
			get
			{
				return new LocalizedString("UMDatacenterLoadBalancerSipOptionsPingEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x000DAB32 File Offset: 0x000D8D32
		public static LocalizedString PassiveReplicationMonitorEscalationMessage
		{
			get
			{
				return new LocalizedString("PassiveReplicationMonitorEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x000DAB4C File Offset: 0x000D8D4C
		public static LocalizedString PotentialInsufficientRedundancyEscalationSubject(string component, string machine)
		{
			return new LocalizedString("PotentialInsufficientRedundancyEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine
			});
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x000DAB78 File Offset: 0x000D8D78
		public static LocalizedString ReinstallServerEscalationMessage
		{
			get
			{
				return new LocalizedString("ReinstallServerEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000DAB90 File Offset: 0x000D8D90
		public static LocalizedString ClusterNetworkReportErrorEscalationMessage(int suppression)
		{
			return new LocalizedString("ClusterNetworkReportErrorEscalationMessage", Strings.ResourceManager, new object[]
			{
				suppression
			});
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x000DABBD File Offset: 0x000D8DBD
		public static LocalizedString ForwardSyncCookieNotUpToDateEscalationSubject
		{
			get
			{
				return new LocalizedString("ForwardSyncCookieNotUpToDateEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000DABD4 File Offset: 0x000D8DD4
		public static LocalizedString DbFailureItemIoHardEscalationMessage(string database)
		{
			return new LocalizedString("DbFailureItemIoHardEscalationMessage", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000DABFC File Offset: 0x000D8DFC
		public static LocalizedString MonitoringAccountDomainUnavailable(string mailboxDatabaseName)
		{
			return new LocalizedString("MonitoringAccountDomainUnavailable", Strings.ResourceManager, new object[]
			{
				mailboxDatabaseName
			});
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000DAC24 File Offset: 0x000D8E24
		public static LocalizedString ClusterNodeEvictedEscalationSubject(string component, string target)
		{
			return new LocalizedString("ClusterNodeEvictedEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				target
			});
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002544 RID: 9540 RVA: 0x000DAC50 File Offset: 0x000D8E50
		public static LocalizedString ActiveDirectoryConnectivityLocalEscalationMessage
		{
			get
			{
				return new LocalizedString("ActiveDirectoryConnectivityLocalEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000DAC68 File Offset: 0x000D8E68
		public static LocalizedString LocalMachineDriveEncryptionSuspendEscalationMessage(string volumes, string serverName)
		{
			return new LocalizedString("LocalMachineDriveEncryptionSuspendEscalationMessage", Strings.ResourceManager, new object[]
			{
				volumes,
				serverName
			});
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000DAC94 File Offset: 0x000D8E94
		public static LocalizedString ActiveManagerUnhealthyEscalationSubject(string component, string machine)
		{
			return new LocalizedString("ActiveManagerUnhealthyEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine
			});
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000DACC0 File Offset: 0x000D8EC0
		public static LocalizedString SearchIndexSeedingNoProgres(string databaseName, string percent, string seedingSource)
		{
			return new LocalizedString("SearchIndexSeedingNoProgres", Strings.ResourceManager, new object[]
			{
				databaseName,
				percent,
				seedingSource
			});
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002548 RID: 9544 RVA: 0x000DACF0 File Offset: 0x000D8EF0
		public static LocalizedString PassiveDatabaseAvailabilityEscalationSubject
		{
			get
			{
				return new LocalizedString("PassiveDatabaseAvailabilityEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000DAD08 File Offset: 0x000D8F08
		public static LocalizedString LagCopyHealthProblemEscalationMessage(string database)
		{
			return new LocalizedString("LagCopyHealthProblemEscalationMessage", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x0600254A RID: 9546 RVA: 0x000DAD30 File Offset: 0x000D8F30
		public static LocalizedString OabTooManyHttpErrorResponsesEncounteredSubject
		{
			get
			{
				return new LocalizedString("OabTooManyHttpErrorResponsesEncounteredSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x000DAD47 File Offset: 0x000D8F47
		public static LocalizedString ELCTransientEscalationSubject
		{
			get
			{
				return new LocalizedString("ELCTransientEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000DAD60 File Offset: 0x000D8F60
		public static LocalizedString FailedAndSuspendedCopyEscalationSubject(string component, string machine, string dbCopy, string threshold)
		{
			return new LocalizedString("FailedAndSuspendedCopyEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				dbCopy,
				threshold
			});
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000DAD94 File Offset: 0x000D8F94
		public static LocalizedString RemoteStoreAdminRPCInterfaceEscalationSubject(TimeSpan duration)
		{
			return new LocalizedString("RemoteStoreAdminRPCInterfaceEscalationSubject", Strings.ResourceManager, new object[]
			{
				duration
			});
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x000DADC1 File Offset: 0x000D8FC1
		public static LocalizedString SiteFailureEscalationMessage
		{
			get
			{
				return new LocalizedString("SiteFailureEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x000DADD8 File Offset: 0x000D8FD8
		public static LocalizedString InvokeNowPickupEventNotFound(string requestId)
		{
			return new LocalizedString("InvokeNowPickupEventNotFound", Strings.ResourceManager, new object[]
			{
				requestId
			});
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002550 RID: 9552 RVA: 0x000DAE00 File Offset: 0x000D9000
		public static LocalizedString OnlineMeetingCreateEscalationBody
		{
			get
			{
				return new LocalizedString("OnlineMeetingCreateEscalationBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x000DAE17 File Offset: 0x000D9017
		public static LocalizedString SiteMailboxDocumentSyncEscalationSubject
		{
			get
			{
				return new LocalizedString("SiteMailboxDocumentSyncEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x000DAE2E File Offset: 0x000D902E
		public static LocalizedString NTDSCorruptionEscalationMessage
		{
			get
			{
				return new LocalizedString("NTDSCorruptionEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x000DAE45 File Offset: 0x000D9045
		public static LocalizedString TopoDiscoveryFailedAllServersEscalationMessage
		{
			get
			{
				return new LocalizedString("TopoDiscoveryFailedAllServersEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000DAE5C File Offset: 0x000D905C
		public static LocalizedString ActiveSyncSelfTestEscalationBodyENT(string serverName, string probeName)
		{
			return new LocalizedString("ActiveSyncSelfTestEscalationBodyENT", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000DAE88 File Offset: 0x000D9088
		public static LocalizedString PrivateWorkingSetExceededErrorThresholdSubject(string processName)
		{
			return new LocalizedString("PrivateWorkingSetExceededErrorThresholdSubject", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000DAEB0 File Offset: 0x000D90B0
		public static LocalizedString AvailabilityServiceEscalationSubjectUnhealthy(string probeType)
		{
			return new LocalizedString("AvailabilityServiceEscalationSubjectUnhealthy", Strings.ResourceManager, new object[]
			{
				probeType
			});
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x000DAED8 File Offset: 0x000D90D8
		public static LocalizedString ActiveSyncEscalationSubject(string probeName, string serverName)
		{
			return new LocalizedString("ActiveSyncEscalationSubject", Strings.ResourceManager, new object[]
			{
				probeName,
				serverName
			});
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x000DAF04 File Offset: 0x000D9104
		public static LocalizedString StoreMaintenanceAssistantEscalationMessageEnt(TimeSpan duration, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("StoreMaintenanceAssistantEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				duration,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000DAF38 File Offset: 0x000D9138
		public static LocalizedString SearchTransportAgentFailure(string failureRate, string threshold, string window, string total, string failed)
		{
			return new LocalizedString("SearchTransportAgentFailure", Strings.ResourceManager, new object[]
			{
				failureRate,
				threshold,
				window,
				total,
				failed
			});
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x000DAF71 File Offset: 0x000D9171
		public static LocalizedString VersionStore1479EscalationMessage
		{
			get
			{
				return new LocalizedString("VersionStore1479EscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x000DAF88 File Offset: 0x000D9188
		public static LocalizedString AssistantsNotRunningError(string error)
		{
			return new LocalizedString("AssistantsNotRunningError", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x000DAFB0 File Offset: 0x000D91B0
		public static LocalizedString AssistantsNotRunningToCompletionSubject
		{
			get
			{
				return new LocalizedString("AssistantsNotRunningToCompletionSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x000DAFC8 File Offset: 0x000D91C8
		public static LocalizedString ImapCustomerTouchPointEscalationBodyDC(string serverName, string probeName)
		{
			return new LocalizedString("ImapCustomerTouchPointEscalationBodyDC", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x0600255E RID: 9566 RVA: 0x000DAFF4 File Offset: 0x000D91F4
		public static LocalizedString AdminAuditingAvailabilityFailureEscalationBody
		{
			get
			{
				return new LocalizedString("AdminAuditingAvailabilityFailureEscalationBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x0600255F RID: 9567 RVA: 0x000DB00B File Offset: 0x000D920B
		public static LocalizedString ForwardSyncLiteCompanyEscalationSubject
		{
			get
			{
				return new LocalizedString("ForwardSyncLiteCompanyEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x000DB022 File Offset: 0x000D9222
		public static LocalizedString MSExchangeInformationStoreCannotContactADEscalationMessage
		{
			get
			{
				return new LocalizedString("MSExchangeInformationStoreCannotContactADEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x000DB039 File Offset: 0x000D9239
		public static LocalizedString DHCPServerRequestsEscalationMessage
		{
			get
			{
				return new LocalizedString("DHCPServerRequestsEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000DB050 File Offset: 0x000D9250
		public static LocalizedString RpsFailedEscalationMessage(string virtualDirectory)
		{
			return new LocalizedString("RpsFailedEscalationMessage", Strings.ResourceManager, new object[]
			{
				virtualDirectory
			});
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000DB078 File Offset: 0x000D9278
		public static LocalizedString ForwardSyncProcessRepeatedlyCrashingEscalationMessage(int count, int duration)
		{
			return new LocalizedString("ForwardSyncProcessRepeatedlyCrashingEscalationMessage", Strings.ResourceManager, new object[]
			{
				count,
				duration
			});
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000DB0B0 File Offset: 0x000D92B0
		public static LocalizedString StoreAdminRPCInterfaceNotResponding(string server)
		{
			return new LocalizedString("StoreAdminRPCInterfaceNotResponding", Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x000DB0D8 File Offset: 0x000D92D8
		public static LocalizedString NoNTDSObjectEscalationMessage
		{
			get
			{
				return new LocalizedString("NoNTDSObjectEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x000DB0F0 File Offset: 0x000D92F0
		public static LocalizedString OwaCustomerTouchPointEscalationSubject(string serverName)
		{
			return new LocalizedString("OwaCustomerTouchPointEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002567 RID: 9575 RVA: 0x000DB118 File Offset: 0x000D9318
		public static LocalizedString PublicFolderMoveJobStuckEscalationSubject
		{
			get
			{
				return new LocalizedString("PublicFolderMoveJobStuckEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x000DB12F File Offset: 0x000D932F
		public static LocalizedString SCTMonitoringScriptExceptionMessage
		{
			get
			{
				return new LocalizedString("SCTMonitoringScriptExceptionMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000DB148 File Offset: 0x000D9348
		public static LocalizedString OwaIMSigninFailedSubject(string serverName)
		{
			return new LocalizedString("OwaIMSigninFailedSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000DB170 File Offset: 0x000D9370
		public static LocalizedString UMPipelineSLAEscalationMessageString(int percentageValue)
		{
			return new LocalizedString("UMPipelineSLAEscalationMessageString", Strings.ResourceManager, new object[]
			{
				percentageValue
			});
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000DB1A0 File Offset: 0x000D93A0
		public static LocalizedString ComponentHealthHeartbeatEscalationMessageUnhealthy(int heartbeatThreshold, int monitoringIntervalMinutes)
		{
			return new LocalizedString("ComponentHealthHeartbeatEscalationMessageUnhealthy", Strings.ResourceManager, new object[]
			{
				heartbeatThreshold,
				monitoringIntervalMinutes
			});
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x000DB1D6 File Offset: 0x000D93D6
		public static LocalizedString ProvisionedDCBelowMinimumEscalationMessage
		{
			get
			{
				return new LocalizedString("ProvisionedDCBelowMinimumEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x000DB1ED File Offset: 0x000D93ED
		public static LocalizedString KerbAuthFailureEscalationMessage
		{
			get
			{
				return new LocalizedString("KerbAuthFailureEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x0600256E RID: 9582 RVA: 0x000DB204 File Offset: 0x000D9404
		public static LocalizedString RequestsQueuedOver500EscalationMessage
		{
			get
			{
				return new LocalizedString("RequestsQueuedOver500EscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x000DB21B File Offset: 0x000D941B
		public static LocalizedString PopImapGuid
		{
			get
			{
				return new LocalizedString("PopImapGuid", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000DB234 File Offset: 0x000D9434
		public static LocalizedString SearchResourceLoadUnhealthy(string databaseName, string copyStatus, string moveJob, string resource, string resourceHistory)
		{
			return new LocalizedString("SearchResourceLoadUnhealthy", Strings.ResourceManager, new object[]
			{
				databaseName,
				copyStatus,
				moveJob,
				resource,
				resourceHistory
			});
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002571 RID: 9585 RVA: 0x000DB26D File Offset: 0x000D946D
		public static LocalizedString MaintenanceFailureEscalationSubject
		{
			get
			{
				return new LocalizedString("MaintenanceFailureEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002572 RID: 9586 RVA: 0x000DB284 File Offset: 0x000D9484
		public static LocalizedString TransportServerDownEscalationMessage
		{
			get
			{
				return new LocalizedString("TransportServerDownEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000DB29C File Offset: 0x000D949C
		public static LocalizedString UnmonitoredDatabaseEscalationMessage(string database)
		{
			return new LocalizedString("UnmonitoredDatabaseEscalationMessage", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x000DB2C4 File Offset: 0x000D94C4
		public static LocalizedString PopImapEndpoint
		{
			get
			{
				return new LocalizedString("PopImapEndpoint", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000DB2DC File Offset: 0x000D94DC
		public static LocalizedString LongRunningWerMgrTriggerWarningThresholdSubject(string processName)
		{
			return new LocalizedString("LongRunningWerMgrTriggerWarningThresholdSubject", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000DB304 File Offset: 0x000D9504
		public static LocalizedString EseDbTimeSmallerEscalationMessage(string machine, string database)
		{
			return new LocalizedString("EseDbTimeSmallerEscalationMessage", Strings.ResourceManager, new object[]
			{
				machine,
				database
			});
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x000DB330 File Offset: 0x000D9530
		public static LocalizedString NtlmConnectivityEscalationMessage
		{
			get
			{
				return new LocalizedString("NtlmConnectivityEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000DB348 File Offset: 0x000D9548
		public static LocalizedString SearchProcessCrashingTooManyTimesEscalationMessage(string processName, int count, int seconds)
		{
			return new LocalizedString("SearchProcessCrashingTooManyTimesEscalationMessage", Strings.ResourceManager, new object[]
			{
				processName,
				count,
				seconds
			});
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000DB384 File Offset: 0x000D9584
		public static LocalizedString GetDiagnosticInfoTimeoutMessage(int timeoutSeconds)
		{
			return new LocalizedString("GetDiagnosticInfoTimeoutMessage", Strings.ResourceManager, new object[]
			{
				timeoutSeconds
			});
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000DB3B4 File Offset: 0x000D95B4
		public static LocalizedString WatermarksBehind(string database)
		{
			return new LocalizedString("WatermarksBehind", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000DB3DC File Offset: 0x000D95DC
		public static LocalizedString ClusterGroupDownEscalationMessage(int suppression)
		{
			return new LocalizedString("ClusterGroupDownEscalationMessage", Strings.ResourceManager, new object[]
			{
				suppression
			});
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000DB40C File Offset: 0x000D960C
		public static LocalizedString PswsEscalationBody(string serverName)
		{
			return new LocalizedString("PswsEscalationBody", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x000DB434 File Offset: 0x000D9634
		public static LocalizedString EseDbTimeSmallerEscalationSubject(string component, string machine, string database)
		{
			return new LocalizedString("EseDbTimeSmallerEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				database
			});
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000DB464 File Offset: 0x000D9664
		public static LocalizedString OabMailboxFileNotFound(string fileName)
		{
			return new LocalizedString("OabMailboxFileNotFound", Strings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000DB48C File Offset: 0x000D968C
		public static LocalizedString SearchCatalogInFailedAndSuspendedState(string databaseName, string stateString, string healthyCopyServer, string timestamp, string localServer)
		{
			return new LocalizedString("SearchCatalogInFailedAndSuspendedState", Strings.ResourceManager, new object[]
			{
				databaseName,
				stateString,
				healthyCopyServer,
				timestamp,
				localServer
			});
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000DB4C8 File Offset: 0x000D96C8
		public static LocalizedString EseSinglePageLogicalCorruptionDetectedSubject(string component, string machine, string database)
		{
			return new LocalizedString("EseSinglePageLogicalCorruptionDetectedSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				database
			});
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002581 RID: 9601 RVA: 0x000DB4F8 File Offset: 0x000D96F8
		public static LocalizedString OabTooManyWebAppStartsSubject
		{
			get
			{
				return new LocalizedString("OabTooManyWebAppStartsSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000DB510 File Offset: 0x000D9710
		public static LocalizedString StoreAdminRPCInterfaceEscalationEscalationMessageDc(TimeSpan duration)
		{
			return new LocalizedString("StoreAdminRPCInterfaceEscalationEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				duration
			});
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002583 RID: 9603 RVA: 0x000DB53D File Offset: 0x000D973D
		public static LocalizedString CafeEscalationSubjectUnhealthy
		{
			get
			{
				return new LocalizedString("CafeEscalationSubjectUnhealthy", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x000DB554 File Offset: 0x000D9754
		public static LocalizedString DnsHostRecordProbeName
		{
			get
			{
				return new LocalizedString("DnsHostRecordProbeName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002585 RID: 9605 RVA: 0x000DB56B File Offset: 0x000D976B
		public static LocalizedString ELCArchiveDumpsterWarningEscalationSubject
		{
			get
			{
				return new LocalizedString("ELCArchiveDumpsterWarningEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000DB584 File Offset: 0x000D9784
		public static LocalizedString VersionBucketsAllocatedEscalationSubject(TimeSpan duration)
		{
			return new LocalizedString("VersionBucketsAllocatedEscalationSubject", Strings.ResourceManager, new object[]
			{
				duration
			});
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000DB5B4 File Offset: 0x000D97B4
		public static LocalizedString OwaDeepTestEscalationBody(string serverName, string logPath)
		{
			return new LocalizedString("OwaDeepTestEscalationBody", Strings.ResourceManager, new object[]
			{
				serverName,
				logPath
			});
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002588 RID: 9608 RVA: 0x000DB5E0 File Offset: 0x000D97E0
		public static LocalizedString JournalFilterAgentEscalationMessage
		{
			get
			{
				return new LocalizedString("JournalFilterAgentEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x000DB5F7 File Offset: 0x000D97F7
		public static LocalizedString WacDiscoveryFailureSubject
		{
			get
			{
				return new LocalizedString("WacDiscoveryFailureSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x000DB60E File Offset: 0x000D980E
		public static LocalizedString CASRoutingLatencyEscalationSubject
		{
			get
			{
				return new LocalizedString("CASRoutingLatencyEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000DB628 File Offset: 0x000D9828
		public static LocalizedString MRSLongQueueScanMessage(string dumpDirectory)
		{
			return new LocalizedString("MRSLongQueueScanMessage", Strings.ResourceManager, new object[]
			{
				dumpDirectory
			});
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000DB650 File Offset: 0x000D9850
		public static LocalizedString SearchIndexFailure(string failureRate, string threshold, string completedCallbacks, string failedCallbacks, string minutes)
		{
			return new LocalizedString("SearchIndexFailure", Strings.ResourceManager, new object[]
			{
				failureRate,
				threshold,
				completedCallbacks,
				failedCallbacks,
				minutes
			});
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x000DB689 File Offset: 0x000D9889
		public static LocalizedString EDSJobPoisonedEscalationMessage
		{
			get
			{
				return new LocalizedString("EDSJobPoisonedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000DB6A0 File Offset: 0x000D98A0
		public static LocalizedString MRSRPCPingSubject(string service)
		{
			return new LocalizedString("MRSRPCPingSubject", Strings.ResourceManager, new object[]
			{
				service
			});
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000DB6C8 File Offset: 0x000D98C8
		public static LocalizedString UserThrottlingLockedOutUsersSubject(string protocol)
		{
			return new LocalizedString("UserThrottlingLockedOutUsersSubject", Strings.ResourceManager, new object[]
			{
				protocol
			});
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x000DB6F0 File Offset: 0x000D98F0
		public static LocalizedString JournalFilterAgentEscalationSubject
		{
			get
			{
				return new LocalizedString("JournalFilterAgentEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x000DB707 File Offset: 0x000D9907
		public static LocalizedString DnsHostRecordMonitorName
		{
			get
			{
				return new LocalizedString("DnsHostRecordMonitorName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000DB720 File Offset: 0x000D9920
		public static LocalizedString ClusterHangEscalationSubject(string component, string target)
		{
			return new LocalizedString("ClusterHangEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				target
			});
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000DB74C File Offset: 0x000D994C
		public static LocalizedString RcaEscalationSubject(string monitorIdentity, string target)
		{
			return new LocalizedString("RcaEscalationSubject", Strings.ResourceManager, new object[]
			{
				monitorIdentity,
				target
			});
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002594 RID: 9620 RVA: 0x000DB778 File Offset: 0x000D9978
		public static LocalizedString VersionStore2008EscalationMessage
		{
			get
			{
				return new LocalizedString("VersionStore2008EscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000DB790 File Offset: 0x000D9990
		public static LocalizedString OwaDeepTestEscalationSubject(string serverName)
		{
			return new LocalizedString("OwaDeepTestEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x000DB7B8 File Offset: 0x000D99B8
		public static LocalizedString DnsServiceMonitorName
		{
			get
			{
				return new LocalizedString("DnsServiceMonitorName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000DB7D0 File Offset: 0x000D99D0
		public static LocalizedString ClusterServiceDownEscalationMessage(int suppression)
		{
			return new LocalizedString("ClusterServiceDownEscalationMessage", Strings.ResourceManager, new object[]
			{
				suppression
			});
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000DB800 File Offset: 0x000D9A00
		public static LocalizedString EscalationMessagePercentUnhealthy(string customMessage)
		{
			return new LocalizedString("EscalationMessagePercentUnhealthy", Strings.ResourceManager, new object[]
			{
				customMessage
			});
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x000DB828 File Offset: 0x000D9A28
		public static LocalizedString DatabaseAvailabilityHelpString
		{
			get
			{
				return new LocalizedString("DatabaseAvailabilityHelpString", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000DB840 File Offset: 0x000D9A40
		public static LocalizedString SearchIndexCopyStatus(string copyName, string databaseStatus, string catalogStatus, string errorMessage)
		{
			return new LocalizedString("SearchIndexCopyStatus", Strings.ResourceManager, new object[]
			{
				copyName,
				databaseStatus,
				catalogStatus,
				errorMessage
			});
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x000DB874 File Offset: 0x000D9A74
		public static LocalizedString PublicFolderMailboxQuotaEscalationSubject
		{
			get
			{
				return new LocalizedString("PublicFolderMailboxQuotaEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x0600259C RID: 9628 RVA: 0x000DB88B File Offset: 0x000D9A8B
		public static LocalizedString HealthSetEscalationSubjectPrefix
		{
			get
			{
				return new LocalizedString("HealthSetEscalationSubjectPrefix", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x000DB8A2 File Offset: 0x000D9AA2
		public static LocalizedString DHCPServerDeclinesEscalationMessage
		{
			get
			{
				return new LocalizedString("DHCPServerDeclinesEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000DB8BC File Offset: 0x000D9ABC
		public static LocalizedString JobobjectCpuExceededThresholdSubject(string jobObjectName)
		{
			return new LocalizedString("JobobjectCpuExceededThresholdSubject", Strings.ResourceManager, new object[]
			{
				jobObjectName
			});
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000DB8E4 File Offset: 0x000D9AE4
		public static LocalizedString FindPlacesRequestsError(string serverName)
		{
			return new LocalizedString("FindPlacesRequestsError", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060025A0 RID: 9632 RVA: 0x000DB90C File Offset: 0x000D9B0C
		public static LocalizedString TrustMonitorProbeEscalationMessage
		{
			get
			{
				return new LocalizedString("TrustMonitorProbeEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x000DB923 File Offset: 0x000D9B23
		public static LocalizedString InvalidIncludedAssistantType
		{
			get
			{
				return new LocalizedString("InvalidIncludedAssistantType", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000DB93C File Offset: 0x000D9B3C
		public static LocalizedString RemoteStoreAdminRPCInterfaceEscalationEscalationMessageDc(TimeSpan duration)
		{
			return new LocalizedString("RemoteStoreAdminRPCInterfaceEscalationEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				duration
			});
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x000DB969 File Offset: 0x000D9B69
		public static LocalizedString ReplicationDisabledEscalationMessage
		{
			get
			{
				return new LocalizedString("ReplicationDisabledEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x000DB980 File Offset: 0x000D9B80
		public static LocalizedString EseInconsistentDataDetectedEscalationMessage(string machine, string database)
		{
			return new LocalizedString("EseInconsistentDataDetectedEscalationMessage", Strings.ResourceManager, new object[]
			{
				machine,
				database
			});
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000DB9AC File Offset: 0x000D9BAC
		public static LocalizedString DatabaseNotFoundInADException(string databaseGuid)
		{
			return new LocalizedString("DatabaseNotFoundInADException", Strings.ResourceManager, new object[]
			{
				databaseGuid
			});
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000DB9D4 File Offset: 0x000D9BD4
		public static LocalizedString PrivateWorkingSetExceededWarningThresholdSubject(string processName)
		{
			return new LocalizedString("PrivateWorkingSetExceededWarningThresholdSubject", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000DB9FC File Offset: 0x000D9BFC
		public static LocalizedString MRSRepeatedlyCrashingEscalationSubject(string service)
		{
			return new LocalizedString("MRSRepeatedlyCrashingEscalationSubject", Strings.ResourceManager, new object[]
			{
				service
			});
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x000DBA24 File Offset: 0x000D9C24
		public static LocalizedString ProvisioningBigVolumeErrorEscalateResponderName
		{
			get
			{
				return new LocalizedString("ProvisioningBigVolumeErrorEscalateResponderName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x000DBA3B File Offset: 0x000D9C3B
		public static LocalizedString CASRoutingLatencyEscalationBody
		{
			get
			{
				return new LocalizedString("CASRoutingLatencyEscalationBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000DBA54 File Offset: 0x000D9C54
		public static LocalizedString ProcessorTimeExceededErrorThresholdMessage(string processName)
		{
			return new LocalizedString("ProcessorTimeExceededErrorThresholdMessage", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000DBA7C File Offset: 0x000D9C7C
		public static LocalizedString MRSLongQueueScanSubject(string service)
		{
			return new LocalizedString("MRSLongQueueScanSubject", Strings.ResourceManager, new object[]
			{
				service
			});
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000DBAA4 File Offset: 0x000D9CA4
		public static LocalizedString StalledCopyEscalationMessage(string database, string threshold)
		{
			return new LocalizedString("StalledCopyEscalationMessage", Strings.ResourceManager, new object[]
			{
				database,
				threshold
			});
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000DBAD0 File Offset: 0x000D9CD0
		public static LocalizedString OabProtocolEscalationSubject(string serverName)
		{
			return new LocalizedString("OabProtocolEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060025AE RID: 9646 RVA: 0x000DBAF8 File Offset: 0x000D9CF8
		public static LocalizedString SecurityAlertMalwareDetectedEscalationMessage
		{
			get
			{
				return new LocalizedString("SecurityAlertMalwareDetectedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x000DBB10 File Offset: 0x000D9D10
		public static LocalizedString ProcessorTimeExceededWarningThresholdWithAffinitizationMessage(string processName)
		{
			return new LocalizedString("ProcessorTimeExceededWarningThresholdWithAffinitizationMessage", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x000DBB38 File Offset: 0x000D9D38
		public static LocalizedString InvalidSystemMailbox(string mailboxDatabaseName)
		{
			return new LocalizedString("InvalidSystemMailbox", Strings.ResourceManager, new object[]
			{
				mailboxDatabaseName
			});
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x000DBB60 File Offset: 0x000D9D60
		public static LocalizedString SynchronousAuditSearchAvailabilityFailureEscalationBody
		{
			get
			{
				return new LocalizedString("SynchronousAuditSearchAvailabilityFailureEscalationBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x000DBB78 File Offset: 0x000D9D78
		public static LocalizedString CafeEscalationMessageUnhealthyForDC(string cafeArrayName)
		{
			return new LocalizedString("CafeEscalationMessageUnhealthyForDC", Strings.ResourceManager, new object[]
			{
				cafeArrayName
			});
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000DBBA0 File Offset: 0x000D9DA0
		public static LocalizedString UMSipOptionsToUMCallRouterServiceFailedEscalationSubject(string serverName)
		{
			return new LocalizedString("UMSipOptionsToUMCallRouterServiceFailedEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x000DBBC8 File Offset: 0x000D9DC8
		public static LocalizedString MaintenanceTimeoutEscalationMessage
		{
			get
			{
				return new LocalizedString("MaintenanceTimeoutEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x000DBBDF File Offset: 0x000D9DDF
		public static LocalizedString DeltaSyncServiceEndpointsLoadFailedEscalationMessage
		{
			get
			{
				return new LocalizedString("DeltaSyncServiceEndpointsLoadFailedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x000DBBF8 File Offset: 0x000D9DF8
		public static LocalizedString PutMultipleDCIntoMMSuccessNotificationMessage(string dcFQDN)
		{
			return new LocalizedString("PutMultipleDCIntoMMSuccessNotificationMessage", Strings.ResourceManager, new object[]
			{
				dcFQDN
			});
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x000DBC20 File Offset: 0x000D9E20
		public static LocalizedString InferenceTrainingDataCollectionRepeatedCrashEscalationMessage(string processName, int minCount, int durationMinutes)
		{
			return new LocalizedString("InferenceTrainingDataCollectionRepeatedCrashEscalationMessage", Strings.ResourceManager, new object[]
			{
				processName,
				minCount,
				durationMinutes
			});
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000DBC5C File Offset: 0x000D9E5C
		public static LocalizedString SearchInstantSearchStxException(string query, string mailboxSmtpAddress, string exception, string queryStats)
		{
			return new LocalizedString("SearchInstantSearchStxException", Strings.ResourceManager, new object[]
			{
				query,
				mailboxSmtpAddress,
				exception,
				queryStats
			});
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x000DBC90 File Offset: 0x000D9E90
		public static LocalizedString SchedulingLatencyEscalateResponderMessage
		{
			get
			{
				return new LocalizedString("SchedulingLatencyEscalateResponderMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x000DBCA7 File Offset: 0x000D9EA7
		public static LocalizedString PowerShellProfileEscalationMessage
		{
			get
			{
				return new LocalizedString("PowerShellProfileEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x000DBCBE File Offset: 0x000D9EBE
		public static LocalizedString OAuthRequestFailureEscalationSubject
		{
			get
			{
				return new LocalizedString("OAuthRequestFailureEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x000DBCD8 File Offset: 0x000D9ED8
		public static LocalizedString ActiveSyncCustomerTouchPointEscalationBodyDC(string serverName, string probeName)
		{
			return new LocalizedString("ActiveSyncCustomerTouchPointEscalationBodyDC", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000DBD04 File Offset: 0x000D9F04
		public static LocalizedString HealthSetsStates(string command)
		{
			return new LocalizedString("HealthSetsStates", Strings.ResourceManager, new object[]
			{
				command
			});
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000DBD2C File Offset: 0x000D9F2C
		public static LocalizedString LongRunningWerMgrTriggerWarningThresholdMessage(string processName)
		{
			return new LocalizedString("LongRunningWerMgrTriggerWarningThresholdMessage", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000DBD54 File Offset: 0x000D9F54
		public static LocalizedString ReplServiceDownEscalationSubject(string component, string machine)
		{
			return new LocalizedString("ReplServiceDownEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine
			});
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x000DBD80 File Offset: 0x000D9F80
		public static LocalizedString SearchIndexBacklogWithProcessingRateAndHistory(string databaseName, string backlog, string retryQueue, string lastTime, string lastBacklog, string lastRetryQueue, string completedCount, string processingRate, string minutes, string upTime, string serverStatus)
		{
			return new LocalizedString("SearchIndexBacklogWithProcessingRateAndHistory", Strings.ResourceManager, new object[]
			{
				databaseName,
				backlog,
				retryQueue,
				lastTime,
				lastBacklog,
				lastRetryQueue,
				completedCount,
				processingRate,
				minutes,
				upTime,
				serverStatus
			});
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x000DBDDC File Offset: 0x000D9FDC
		public static LocalizedString MultipleRecipientsFound(string queryFilter)
		{
			return new LocalizedString("MultipleRecipientsFound", Strings.ResourceManager, new object[]
			{
				queryFilter
			});
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000DBE04 File Offset: 0x000DA004
		public static LocalizedString CafeThreadCountSubjectUnhealthy(string appPool)
		{
			return new LocalizedString("CafeThreadCountSubjectUnhealthy", Strings.ResourceManager, new object[]
			{
				appPool
			});
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000DBE2C File Offset: 0x000DA02C
		public static LocalizedString PotentialInsufficientRedundancyEscalationMessage(string machine)
		{
			return new LocalizedString("PotentialInsufficientRedundancyEscalationMessage", Strings.ResourceManager, new object[]
			{
				machine
			});
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x000DBE54 File Offset: 0x000DA054
		public static LocalizedString OabMailboxNoOrgMailbox
		{
			get
			{
				return new LocalizedString("OabMailboxNoOrgMailbox", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000DBE6C File Offset: 0x000DA06C
		public static LocalizedString InvalidUserName(string userName)
		{
			return new LocalizedString("InvalidUserName", Strings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000DBE94 File Offset: 0x000DA094
		public static LocalizedString SearchInstantSearchStxEscalationMessage(string databaseName, int threshold, int interval)
		{
			return new LocalizedString("SearchInstantSearchStxEscalationMessage", Strings.ResourceManager, new object[]
			{
				databaseName,
				threshold,
				interval
			});
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x000DBED0 File Offset: 0x000DA0D0
		public static LocalizedString MailboxAssistantsBehindWatermarksEscalationMessageDc(TimeSpan ageThreshold, TimeSpan duration, string invokeNowCommand, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("MailboxAssistantsBehindWatermarksEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				ageThreshold,
				duration,
				invokeNowCommand,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x000DBF10 File Offset: 0x000DA110
		public static LocalizedString ExchangeCrashExceededErrorThresholdSubject(string processName)
		{
			return new LocalizedString("ExchangeCrashExceededErrorThresholdSubject", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x000DBF38 File Offset: 0x000DA138
		public static LocalizedString PushNotificationEnterpriseEmptyDomain
		{
			get
			{
				return new LocalizedString("PushNotificationEnterpriseEmptyDomain", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000DBF50 File Offset: 0x000DA150
		public static LocalizedString RcaWorkItemDescriptionEntry(string alertMask, string exceptionMessage)
		{
			return new LocalizedString("RcaWorkItemDescriptionEntry", Strings.ResourceManager, new object[]
			{
				alertMask,
				exceptionMessage
			});
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000DBF7C File Offset: 0x000DA17C
		public static LocalizedString SearchIndexActiveCopyUnhealthy(string databaseName, string status, string errorMessage, string diagnosticInfoError, string nodesInfo)
		{
			return new LocalizedString("SearchIndexActiveCopyUnhealthy", Strings.ResourceManager, new object[]
			{
				databaseName,
				status,
				errorMessage,
				diagnosticInfoError,
				nodesInfo
			});
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x000DBFB5 File Offset: 0x000DA1B5
		public static LocalizedString AssistantsOutOfSlaMessage
		{
			get
			{
				return new LocalizedString("AssistantsOutOfSlaMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x000DBFCC File Offset: 0x000DA1CC
		public static LocalizedString EwsAutodEscalationSubjectUnhealthy
		{
			get
			{
				return new LocalizedString("EwsAutodEscalationSubjectUnhealthy", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000DBFE4 File Offset: 0x000DA1E4
		public static LocalizedString UMCallRouterRecentMissedCallNotificationProxyFailedEscalationMessageString(int percentageValue)
		{
			return new LocalizedString("UMCallRouterRecentMissedCallNotificationProxyFailedEscalationMessageString", Strings.ResourceManager, new object[]
			{
				percentageValue
			});
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x000DC011 File Offset: 0x000DA211
		public static LocalizedString HttpConnectivityEscalationSubject
		{
			get
			{
				return new LocalizedString("HttpConnectivityEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000DC028 File Offset: 0x000DA228
		public static LocalizedString SearchNumDiskPartsEscalationMessage(string databaseName)
		{
			return new LocalizedString("SearchNumDiskPartsEscalationMessage", Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x000DC050 File Offset: 0x000DA250
		public static LocalizedString DatabaseObjectNotFoundException
		{
			get
			{
				return new LocalizedString("DatabaseObjectNotFoundException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000DC068 File Offset: 0x000DA268
		public static LocalizedString InferenceClassificationRepeatedCrashEscalationMessage(string processName, int minCount, int durationMinutes)
		{
			return new LocalizedString("InferenceClassificationRepeatedCrashEscalationMessage", Strings.ResourceManager, new object[]
			{
				processName,
				minCount,
				durationMinutes
			});
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x000DC0A4 File Offset: 0x000DA2A4
		public static LocalizedString AssistantsActiveDatabaseError(string error)
		{
			return new LocalizedString("AssistantsActiveDatabaseError", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000DC0CC File Offset: 0x000DA2CC
		public static LocalizedString DatabaseDiskReadLatencyEscalationSubject(TimeSpan duration)
		{
			return new LocalizedString("DatabaseDiskReadLatencyEscalationSubject", Strings.ResourceManager, new object[]
			{
				duration
			});
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x000DC0F9 File Offset: 0x000DA2F9
		public static LocalizedString FSMODCNotProvisionedEscalationMessage
		{
			get
			{
				return new LocalizedString("FSMODCNotProvisionedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000DC110 File Offset: 0x000DA310
		public static LocalizedString NumberOfActiveBackgroundTasksEscalationMessageDc(string databaseName, int threshold, TimeSpan duration)
		{
			return new LocalizedString("NumberOfActiveBackgroundTasksEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				databaseName,
				threshold,
				duration
			});
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x000DC14A File Offset: 0x000DA34A
		public static LocalizedString AssistantsNotRunningToCompletionMessage
		{
			get
			{
				return new LocalizedString("AssistantsNotRunningToCompletionMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000DC164 File Offset: 0x000DA364
		public static LocalizedString UserthtottlingLockedOutUsersMessage(string protocol, int threshold, int sample)
		{
			return new LocalizedString("UserthtottlingLockedOutUsersMessage", Strings.ResourceManager, new object[]
			{
				protocol,
				threshold,
				sample
			});
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000DC1A0 File Offset: 0x000DA3A0
		public static LocalizedString SearchCatalogHasError(string databaseName, string error, string serverName)
		{
			return new LocalizedString("SearchCatalogHasError", Strings.ResourceManager, new object[]
			{
				databaseName,
				error,
				serverName
			});
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060025DA RID: 9690 RVA: 0x000DC1D0 File Offset: 0x000DA3D0
		public static LocalizedString DLExpansionEscalationSubject
		{
			get
			{
				return new LocalizedString("DLExpansionEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000DC1E8 File Offset: 0x000DA3E8
		public static LocalizedString NTFSCorruptionEscalationSubject(string component, string machine, string database)
		{
			return new LocalizedString("NTFSCorruptionEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				database
			});
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000DC218 File Offset: 0x000DA418
		public static LocalizedString ActiveManagerUnhealthyEscalationMessage(string machine, int restartService, int bugcheckTime)
		{
			return new LocalizedString("ActiveManagerUnhealthyEscalationMessage", Strings.ResourceManager, new object[]
			{
				machine,
				restartService,
				bugcheckTime
			});
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x000DC252 File Offset: 0x000DA452
		public static LocalizedString RcaTaskOutlineFailed
		{
			get
			{
				return new LocalizedString("RcaTaskOutlineFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x000DC269 File Offset: 0x000DA469
		public static LocalizedString CASRoutingFailureEscalationBody
		{
			get
			{
				return new LocalizedString("CASRoutingFailureEscalationBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x000DC280 File Offset: 0x000DA480
		public static LocalizedString DnsServiceProbeName
		{
			get
			{
				return new LocalizedString("DnsServiceProbeName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000DC298 File Offset: 0x000DA498
		public static LocalizedString HighDiskLatencyEscalationSubject(string component, string machine, string threshold, string suppresion)
		{
			return new LocalizedString("HighDiskLatencyEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				threshold,
				suppresion
			});
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000DC2CC File Offset: 0x000DA4CC
		public static LocalizedString TooManyDatabaseMountedEscalationSubject(string component, string machine)
		{
			return new LocalizedString("TooManyDatabaseMountedEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine
			});
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x000DC2F8 File Offset: 0x000DA4F8
		public static LocalizedString SynchronousAuditSearchAvailabilityFailureEscalationSubject
		{
			get
			{
				return new LocalizedString("SynchronousAuditSearchAvailabilityFailureEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x000DC30F File Offset: 0x000DA50F
		public static LocalizedString CannotRebuildIndexEscalationMessage
		{
			get
			{
				return new LocalizedString("CannotRebuildIndexEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x000DC326 File Offset: 0x000DA526
		public static LocalizedString UMPipelineFullEscalationMessageString
		{
			get
			{
				return new LocalizedString("UMPipelineFullEscalationMessageString", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000DC340 File Offset: 0x000DA540
		public static LocalizedString PasswordVerificationFailed(string mailboxDatabaseName, string upn, string error)
		{
			return new LocalizedString("PasswordVerificationFailed", Strings.ResourceManager, new object[]
			{
				mailboxDatabaseName,
				upn,
				error
			});
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x000DC370 File Offset: 0x000DA570
		public static LocalizedString LocalMachineDriveEncryptionStateEscalationMessage(string volumes, string serverName)
		{
			return new LocalizedString("LocalMachineDriveEncryptionStateEscalationMessage", Strings.ResourceManager, new object[]
			{
				volumes,
				serverName
			});
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x000DC39C File Offset: 0x000DA59C
		public static LocalizedString DatabaseNotAttachedReadOnly
		{
			get
			{
				return new LocalizedString("DatabaseNotAttachedReadOnly", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x000DC3B4 File Offset: 0x000DA5B4
		public static LocalizedString UMWorkerProcessRecentCallRejectedEscalationMessageString(int percentageValue)
		{
			return new LocalizedString("UMWorkerProcessRecentCallRejectedEscalationMessageString", Strings.ResourceManager, new object[]
			{
				percentageValue
			});
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x000DC3E4 File Offset: 0x000DA5E4
		public static LocalizedString SingleAvailableDatabaseCopyEscalationSubject(string component, string machine)
		{
			return new LocalizedString("SingleAvailableDatabaseCopyEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine
			});
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060025EA RID: 9706 RVA: 0x000DC410 File Offset: 0x000DA610
		public static LocalizedString InfrastructureValidationMessage
		{
			get
			{
				return new LocalizedString("InfrastructureValidationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000DC428 File Offset: 0x000DA628
		public static LocalizedString PopProxyTestEscalationBodyDC(string serverName, string probeName)
		{
			return new LocalizedString("PopProxyTestEscalationBodyDC", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x000DC454 File Offset: 0x000DA654
		public static LocalizedString OwaTooManyHttpErrorResponsesEncounteredSubject
		{
			get
			{
				return new LocalizedString("OwaTooManyHttpErrorResponsesEncounteredSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x000DC46B File Offset: 0x000DA66B
		public static LocalizedString CPUOverThresholdWarningEscalationSubject
		{
			get
			{
				return new LocalizedString("CPUOverThresholdWarningEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000DC484 File Offset: 0x000DA684
		public static LocalizedString StoreNotificationEscalationMessage(string dbName)
		{
			return new LocalizedString("StoreNotificationEscalationMessage", Strings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060025EF RID: 9711 RVA: 0x000DC4AC File Offset: 0x000DA6AC
		public static LocalizedString SubscriptionSlaMissedEscalationMessage
		{
			get
			{
				return new LocalizedString("SubscriptionSlaMissedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x000DC4C3 File Offset: 0x000DA6C3
		public static LocalizedString ActiveDirectoryConnectivityConfigDCEscalationMessage
		{
			get
			{
				return new LocalizedString("ActiveDirectoryConnectivityConfigDCEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x000DC4DA File Offset: 0x000DA6DA
		public static LocalizedString OwaMailboxRoleNotInstalled
		{
			get
			{
				return new LocalizedString("OwaMailboxRoleNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000DC4F4 File Offset: 0x000DA6F4
		public static LocalizedString CafeEscalationMessageUnhealthy(string recoveryDetails, string cafeArrayName)
		{
			return new LocalizedString("CafeEscalationMessageUnhealthy", Strings.ResourceManager, new object[]
			{
				recoveryDetails,
				cafeArrayName
			});
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060025F3 RID: 9715 RVA: 0x000DC520 File Offset: 0x000DA720
		public static LocalizedString PushNotificationEnterpriseNetworkingError
		{
			get
			{
				return new LocalizedString("PushNotificationEnterpriseNetworkingError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x000DC538 File Offset: 0x000DA738
		public static LocalizedString PopCustomerTouchPointEscalationBodyDC(string serverName, string probeName)
		{
			return new LocalizedString("PopCustomerTouchPointEscalationBodyDC", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x000DC564 File Offset: 0x000DA764
		public static LocalizedString PopEscalationSubject(string probeName, string serverName)
		{
			return new LocalizedString("PopEscalationSubject", Strings.ResourceManager, new object[]
			{
				probeName,
				serverName
			});
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x000DC590 File Offset: 0x000DA790
		public static LocalizedString DatabaseAvailabilityTimeout
		{
			get
			{
				return new LocalizedString("DatabaseAvailabilityTimeout", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060025F7 RID: 9719 RVA: 0x000DC5A7 File Offset: 0x000DA7A7
		public static LocalizedString DatabaseGuidNotFound
		{
			get
			{
				return new LocalizedString("DatabaseGuidNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x000DC5BE File Offset: 0x000DA7BE
		public static LocalizedString SearchIndexBacklogAggregatedEscalationMessage
		{
			get
			{
				return new LocalizedString("SearchIndexBacklogAggregatedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x000DC5D5 File Offset: 0x000DA7D5
		public static LocalizedString PublicFolderLocalEWSLogonEscalationMessage
		{
			get
			{
				return new LocalizedString("PublicFolderLocalEWSLogonEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060025FA RID: 9722 RVA: 0x000DC5EC File Offset: 0x000DA7EC
		public static LocalizedString TenantRelocationErrorsFoundExceptionMessage
		{
			get
			{
				return new LocalizedString("TenantRelocationErrorsFoundExceptionMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x000DC603 File Offset: 0x000DA803
		public static LocalizedString UMCallRouterCertificateNearExpiryEscalationMessage
		{
			get
			{
				return new LocalizedString("UMCallRouterCertificateNearExpiryEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x060025FC RID: 9724 RVA: 0x000DC61A File Offset: 0x000DA81A
		public static LocalizedString SlowADWritesEscalationMessage
		{
			get
			{
				return new LocalizedString("SlowADWritesEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x000DC634 File Offset: 0x000DA834
		public static LocalizedString InferenceComponentDisabled(string details)
		{
			return new LocalizedString("InferenceComponentDisabled", Strings.ResourceManager, new object[]
			{
				details
			});
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x000DC65C File Offset: 0x000DA85C
		public static LocalizedString RcaEscalationBodyEnt
		{
			get
			{
				return new LocalizedString("RcaEscalationBodyEnt", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000DC674 File Offset: 0x000DA874
		public static LocalizedString SearchQueryStxEscalationMessage(string databaseName, int threshold, int interval)
		{
			return new LocalizedString("SearchQueryStxEscalationMessage", Strings.ResourceManager, new object[]
			{
				databaseName,
				threshold,
				interval
			});
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x000DC6B0 File Offset: 0x000DA8B0
		public static LocalizedString DatabaseSizeEscalationMessageEnt(string invokeNowCommand, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("DatabaseSizeEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				invokeNowCommand,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x000DC6DC File Offset: 0x000DA8DC
		public static LocalizedString ActiveSyncCustomerTouchPointEscalationBodyENT(string serverName, string probeName)
		{
			return new LocalizedString("ActiveSyncCustomerTouchPointEscalationBodyENT", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x000DC708 File Offset: 0x000DA908
		public static LocalizedString MailboxDatabasesUnavailable
		{
			get
			{
				return new LocalizedString("MailboxDatabasesUnavailable", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x000DC71F File Offset: 0x000DA91F
		public static LocalizedString RetryRemoteDeliveryQueueLengthEscalationMessage
		{
			get
			{
				return new LocalizedString("RetryRemoteDeliveryQueueLengthEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000DC738 File Offset: 0x000DA938
		public static LocalizedString HealthSetMonitorsStates(string command)
		{
			return new LocalizedString("HealthSetMonitorsStates", Strings.ResourceManager, new object[]
			{
				command
			});
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x000DC760 File Offset: 0x000DA960
		public static LocalizedString FailedToUpgradeIndexEscalationMessage
		{
			get
			{
				return new LocalizedString("FailedToUpgradeIndexEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x000DC778 File Offset: 0x000DA978
		public static LocalizedString SearchGetDiagnosticInfoTimeout(int timeoutSeconds)
		{
			return new LocalizedString("SearchGetDiagnosticInfoTimeout", Strings.ResourceManager, new object[]
			{
				timeoutSeconds
			});
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x000DC7A5 File Offset: 0x000DA9A5
		public static LocalizedString EventAssistantsWatermarksHelpString
		{
			get
			{
				return new LocalizedString("EventAssistantsWatermarksHelpString", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000DC7BC File Offset: 0x000DA9BC
		public static LocalizedString InvalidMailboxDatabaseEndpoint(string message)
		{
			return new LocalizedString("InvalidMailboxDatabaseEndpoint", Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x000DC7E4 File Offset: 0x000DA9E4
		public static LocalizedString InferenceClassifcationSLAEscalationMessage
		{
			get
			{
				return new LocalizedString("InferenceClassifcationSLAEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x000DC7FB File Offset: 0x000DA9FB
		public static LocalizedString MRSUnhealthyMessage
		{
			get
			{
				return new LocalizedString("MRSUnhealthyMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x000DC812 File Offset: 0x000DAA12
		public static LocalizedString UMServiceType
		{
			get
			{
				return new LocalizedString("UMServiceType", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x000DC829 File Offset: 0x000DAA29
		public static LocalizedString DivergenceBetweenCAAndAD1003EscalationMessage
		{
			get
			{
				return new LocalizedString("DivergenceBetweenCAAndAD1003EscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000DC840 File Offset: 0x000DAA40
		public static LocalizedString PopDeepTestEscalationBodyDC(string serverName, string probeName)
		{
			return new LocalizedString("PopDeepTestEscalationBodyDC", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000DC86C File Offset: 0x000DAA6C
		public static LocalizedString LocalMachineDriveEncryptionLockEscalationSubject(string serverName)
		{
			return new LocalizedString("LocalMachineDriveEncryptionLockEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000DC894 File Offset: 0x000DAA94
		public static LocalizedString SearchGracefulDegradationStatus(string timestamp)
		{
			return new LocalizedString("SearchGracefulDegradationStatus", Strings.ResourceManager, new object[]
			{
				timestamp
			});
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000DC8BC File Offset: 0x000DAABC
		public static LocalizedString EseDbDivergenceDetectedSubject(string component, string machine, string database)
		{
			return new LocalizedString("EseDbDivergenceDetectedSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				database
			});
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000DC8EC File Offset: 0x000DAAEC
		public static LocalizedString SearchIndexActiveCopySeedingWithHealthyCopy(string databaseName, string healthyCopyServerName)
		{
			return new LocalizedString("SearchIndexActiveCopySeedingWithHealthyCopy", Strings.ResourceManager, new object[]
			{
				databaseName,
				healthyCopyServerName
			});
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000DC918 File Offset: 0x000DAB18
		public static LocalizedString NumberOfActiveBackgroundTasksEscalationSubject(string databaseName, int threshold, TimeSpan duration)
		{
			return new LocalizedString("NumberOfActiveBackgroundTasksEscalationSubject", Strings.ResourceManager, new object[]
			{
				databaseName,
				threshold,
				duration
			});
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x000DC954 File Offset: 0x000DAB54
		public static LocalizedString StoreMaintenanceAssistantEscalationSubject(TimeSpan duration)
		{
			return new LocalizedString("StoreMaintenanceAssistantEscalationSubject", Strings.ResourceManager, new object[]
			{
				duration
			});
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x000DC981 File Offset: 0x000DAB81
		public static LocalizedString AssistantsActiveDatabaseMessage
		{
			get
			{
				return new LocalizedString("AssistantsActiveDatabaseMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x000DC998 File Offset: 0x000DAB98
		public static LocalizedString StoreNotificationEscalationSubject(string component, string machine, string database)
		{
			return new LocalizedString("StoreNotificationEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				database
			});
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000DC9C8 File Offset: 0x000DABC8
		public static LocalizedString RwsDatamartAvailabilityEscalationBody(string serverName, string cName)
		{
			return new LocalizedString("RwsDatamartAvailabilityEscalationBody", Strings.ResourceManager, new object[]
			{
				serverName,
				cName
			});
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x000DC9F4 File Offset: 0x000DABF4
		public static LocalizedString ComponentHealthPercentFailureEscalationMessageHealthy(int percentFailureThreshold, int monitoringIntervalMinutes)
		{
			return new LocalizedString("ComponentHealthPercentFailureEscalationMessageHealthy", Strings.ResourceManager, new object[]
			{
				percentFailureThreshold,
				monitoringIntervalMinutes
			});
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x000DCA2C File Offset: 0x000DAC2C
		public static LocalizedString ActiveDatabaseAvailabilityEscalationMessageDc(string invokeNowCommand, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("ActiveDatabaseAvailabilityEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				invokeNowCommand,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x000DCA58 File Offset: 0x000DAC58
		public static LocalizedString SchedulingLatencyEscalateResponderSubject
		{
			get
			{
				return new LocalizedString("SchedulingLatencyEscalateResponderSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x000DCA6F File Offset: 0x000DAC6F
		public static LocalizedString OfficeGraphTransportDeliveryAgentFailureEscalationMessage
		{
			get
			{
				return new LocalizedString("OfficeGraphTransportDeliveryAgentFailureEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000DCA88 File Offset: 0x000DAC88
		public static LocalizedString SearchIndexDatabaseCopyStatus(string message, string databaseCopyStatus)
		{
			return new LocalizedString("SearchIndexDatabaseCopyStatus", Strings.ResourceManager, new object[]
			{
				message,
				databaseCopyStatus
			});
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x000DCAB4 File Offset: 0x000DACB4
		public static LocalizedString OfficeGraphMessageTracingPluginFailureEscalationMessage
		{
			get
			{
				return new LocalizedString("OfficeGraphMessageTracingPluginFailureEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600261D RID: 9757 RVA: 0x000DCACB File Offset: 0x000DACCB
		public static LocalizedString LogicalDiskFreeMegabytesEscalationMessage
		{
			get
			{
				return new LocalizedString("LogicalDiskFreeMegabytesEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x000DCAE2 File Offset: 0x000DACE2
		public static LocalizedString OwaIMLogAnalyzerSubject
		{
			get
			{
				return new LocalizedString("OwaIMLogAnalyzerSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000DCAFC File Offset: 0x000DACFC
		public static LocalizedString OabMailboxManifestAddressListEmpty(string addrList)
		{
			return new LocalizedString("OabMailboxManifestAddressListEmpty", Strings.ResourceManager, new object[]
			{
				addrList
			});
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x000DCB24 File Offset: 0x000DAD24
		public static LocalizedString PopCustomerTouchPointEscalationBodyENT(string serverName, string probeName)
		{
			return new LocalizedString("PopCustomerTouchPointEscalationBodyENT", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x000DCB50 File Offset: 0x000DAD50
		public static LocalizedString ServerInMaintenanceModeForTooLongEscalationMessage(string machine, string threshold)
		{
			return new LocalizedString("ServerInMaintenanceModeForTooLongEscalationMessage", Strings.ResourceManager, new object[]
			{
				machine,
				threshold
			});
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x000DCB7C File Offset: 0x000DAD7C
		public static LocalizedString RaidDegradedEscalationMessage
		{
			get
			{
				return new LocalizedString("RaidDegradedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x000DCB93 File Offset: 0x000DAD93
		public static LocalizedString BulkProvisioningNoProgressEscalationMessage
		{
			get
			{
				return new LocalizedString("BulkProvisioningNoProgressEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000DCBAC File Offset: 0x000DADAC
		public static LocalizedString LocalMachineDriveEncryptionStateEscalationSubject(string serverName)
		{
			return new LocalizedString("LocalMachineDriveEncryptionStateEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002625 RID: 9765 RVA: 0x000DCBD4 File Offset: 0x000DADD4
		public static LocalizedString AssistantsOutOfSlaSubject
		{
			get
			{
				return new LocalizedString("AssistantsOutOfSlaSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x000DCBEB File Offset: 0x000DADEB
		public static LocalizedString OwaTooManyHttpErrorResponsesEncounteredBody
		{
			get
			{
				return new LocalizedString("OwaTooManyHttpErrorResponsesEncounteredBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x000DCC04 File Offset: 0x000DAE04
		public static LocalizedString EseLostFlushDetectedEscalationSubject(string component, string machine, string database)
		{
			return new LocalizedString("EseLostFlushDetectedEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				database
			});
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x000DCC34 File Offset: 0x000DAE34
		public static LocalizedString CheckSumEscalationMessage
		{
			get
			{
				return new LocalizedString("CheckSumEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x000DCC4B File Offset: 0x000DAE4B
		public static LocalizedString PublicFolderLocalEWSLogonEscalationSubject
		{
			get
			{
				return new LocalizedString("PublicFolderLocalEWSLogonEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x000DCC64 File Offset: 0x000DAE64
		public static LocalizedString ImapSelfTestEscalationBodyDC(string serverName, string probeName)
		{
			return new LocalizedString("ImapSelfTestEscalationBodyDC", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x000DCC90 File Offset: 0x000DAE90
		public static LocalizedString ArchiveNamePrefix(string primaryMailboxName)
		{
			return new LocalizedString("ArchiveNamePrefix", Strings.ResourceManager, new object[]
			{
				primaryMailboxName
			});
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x000DCCB8 File Offset: 0x000DAEB8
		public static LocalizedString ReplServiceCrashEscalationSubject(string component, string machine, int times, int hour)
		{
			return new LocalizedString("ReplServiceCrashEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				times,
				hour
			});
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x000DCCF8 File Offset: 0x000DAEF8
		public static LocalizedString EventAssistantsProcessRepeatedlyCrashingEscalationMessageDc(string processName, int count, TimeSpan duration)
		{
			return new LocalizedString("EventAssistantsProcessRepeatedlyCrashingEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				processName,
				count,
				duration
			});
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x000DCD32 File Offset: 0x000DAF32
		public static LocalizedString UMServerAddress
		{
			get
			{
				return new LocalizedString("UMServerAddress", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x000DCD49 File Offset: 0x000DAF49
		public static LocalizedString CafeArrayNameCouldNotBeRetrieved
		{
			get
			{
				return new LocalizedString("CafeArrayNameCouldNotBeRetrieved", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x000DCD60 File Offset: 0x000DAF60
		public static LocalizedString ComponentHealthHeartbeatEscalationMessageHealthy(int heartbeatThreshold, int monitoringIntervalMinutes)
		{
			return new LocalizedString("ComponentHealthHeartbeatEscalationMessageHealthy", Strings.ResourceManager, new object[]
			{
				heartbeatThreshold,
				monitoringIntervalMinutes
			});
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000DCD98 File Offset: 0x000DAF98
		public static LocalizedString OwaSelfTestEscalationSubject(string serverName)
		{
			return new LocalizedString("OwaSelfTestEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000DCDC0 File Offset: 0x000DAFC0
		public static LocalizedString EseSinglePageLogicalCorruptionDetectedEscalationMessage(string machine, string database)
		{
			return new LocalizedString("EseSinglePageLogicalCorruptionDetectedEscalationMessage", Strings.ResourceManager, new object[]
			{
				machine,
				database
			});
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x000DCDEC File Offset: 0x000DAFEC
		public static LocalizedString JobobjectCpuExceededThresholdMessage(string jobObjectName)
		{
			return new LocalizedString("JobobjectCpuExceededThresholdMessage", Strings.ResourceManager, new object[]
			{
				jobObjectName
			});
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x000DCE14 File Offset: 0x000DB014
		public static LocalizedString EDSServiceNotRunningEscalationMessage
		{
			get
			{
				return new LocalizedString("EDSServiceNotRunningEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x000DCE2C File Offset: 0x000DB02C
		public static LocalizedString HighDiskLatencyEscalationMessage(string machine, string threshold, string suppresion)
		{
			return new LocalizedString("HighDiskLatencyEscalationMessage", Strings.ResourceManager, new object[]
			{
				machine,
				threshold,
				suppresion
			});
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x000DCE5C File Offset: 0x000DB05C
		public static LocalizedString SearchFailToCheckNodeState
		{
			get
			{
				return new LocalizedString("SearchFailToCheckNodeState", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x000DCE74 File Offset: 0x000DB074
		public static LocalizedString ImapDeepTestEscalationBodyENT(string serverName, string probeName)
		{
			return new LocalizedString("ImapDeepTestEscalationBodyENT", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000DCEA0 File Offset: 0x000DB0A0
		public static LocalizedString OfficeGraphMessageTracingPluginLogDirectoryExceedsSizeLimit(string machineName, string logDirectorySizeInMB, string sizeLimitInMB)
		{
			return new LocalizedString("OfficeGraphMessageTracingPluginLogDirectoryExceedsSizeLimit", Strings.ResourceManager, new object[]
			{
				machineName,
				logDirectorySizeInMB,
				sizeLimitInMB
			});
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x000DCED0 File Offset: 0x000DB0D0
		public static LocalizedString OwaTooManyStartPageFailuresBody
		{
			get
			{
				return new LocalizedString("OwaTooManyStartPageFailuresBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x000DCEE8 File Offset: 0x000DB0E8
		public static LocalizedString MailSubmissionBehindWatermarksEscalationMessageDc(TimeSpan ageThreshold, TimeSpan duration, string databaseName, string invokeNowCommand, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("MailSubmissionBehindWatermarksEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				ageThreshold,
				duration,
				databaseName,
				invokeNowCommand,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x000DCF2B File Offset: 0x000DB12B
		public static LocalizedString QuarantineEscalationSubject
		{
			get
			{
				return new LocalizedString("QuarantineEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000DCF44 File Offset: 0x000DB144
		public static LocalizedString AsyncAuditLogSearchEscalationMessage(string serverName, string notification)
		{
			return new LocalizedString("AsyncAuditLogSearchEscalationMessage", Strings.ResourceManager, new object[]
			{
				serverName,
				notification
			});
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x000DCF70 File Offset: 0x000DB170
		public static LocalizedString DatabaseLogicalPhysicalSizeRatioEscalationSubject(TimeSpan duration)
		{
			return new LocalizedString("DatabaseLogicalPhysicalSizeRatioEscalationSubject", Strings.ResourceManager, new object[]
			{
				duration
			});
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x000DCF9D File Offset: 0x000DB19D
		public static LocalizedString HostControllerServiceRunningMessage
		{
			get
			{
				return new LocalizedString("HostControllerServiceRunningMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x000DCFB4 File Offset: 0x000DB1B4
		public static LocalizedString ServiceNotRunningEscalationMessageDc(string serviceName)
		{
			return new LocalizedString("ServiceNotRunningEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				serviceName
			});
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x000DCFDC File Offset: 0x000DB1DC
		public static LocalizedString SearchGracefulDegradationManagerFailureEscalationMessage
		{
			get
			{
				return new LocalizedString("SearchGracefulDegradationManagerFailureEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000DCFF4 File Offset: 0x000DB1F4
		public static LocalizedString DatabaseRPCLatencyEscalationMessageDc(string databaseName, int latency, TimeSpan duration)
		{
			return new LocalizedString("DatabaseRPCLatencyEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				databaseName,
				latency,
				duration
			});
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002642 RID: 9794 RVA: 0x000DD02E File Offset: 0x000DB22E
		public static LocalizedString ProvisioningBigVolumeErrorMonitorName
		{
			get
			{
				return new LocalizedString("ProvisioningBigVolumeErrorMonitorName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x000DD048 File Offset: 0x000DB248
		public static LocalizedString ClusterServiceDownEscalationSubject(string component, string target, int threshold)
		{
			return new LocalizedString("ClusterServiceDownEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				target,
				threshold
			});
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x000DD07D File Offset: 0x000DB27D
		public static LocalizedString OwaTooManyWebAppStartsSubject
		{
			get
			{
				return new LocalizedString("OwaTooManyWebAppStartsSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002645 RID: 9797 RVA: 0x000DD094 File Offset: 0x000DB294
		public static LocalizedString SearchQueryStxSimpleQueryMode
		{
			get
			{
				return new LocalizedString("SearchQueryStxSimpleQueryMode", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000DD0AC File Offset: 0x000DB2AC
		public static LocalizedString UnableToGetStoreUsageStatisticsData(string database)
		{
			return new LocalizedString("UnableToGetStoreUsageStatisticsData", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x000DD0D4 File Offset: 0x000DB2D4
		public static LocalizedString BingServicesLatencyError(string serverName)
		{
			return new LocalizedString("BingServicesLatencyError", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002648 RID: 9800 RVA: 0x000DD0FC File Offset: 0x000DB2FC
		public static LocalizedString OABGenTenantOutOfSLASubject
		{
			get
			{
				return new LocalizedString("OABGenTenantOutOfSLASubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000DD114 File Offset: 0x000DB314
		public static LocalizedString UMSipOptionsToUMCallRouterServiceFailedEscalationBody(string serverName)
		{
			return new LocalizedString("UMSipOptionsToUMCallRouterServiceFailedEscalationBody", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x000DD13C File Offset: 0x000DB33C
		public static LocalizedString CouldNotAddExchangeSnapInExceptionMessage(string snapInName)
		{
			return new LocalizedString("CouldNotAddExchangeSnapInExceptionMessage", Strings.ResourceManager, new object[]
			{
				snapInName
			});
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x000DD164 File Offset: 0x000DB364
		public static LocalizedString ELCDumpsterEscalationMessage
		{
			get
			{
				return new LocalizedString("ELCDumpsterEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000DD17C File Offset: 0x000DB37C
		public static LocalizedString ClassficationEngineErrorsEscalationMessage(string engineName, double threshold, int duration)
		{
			return new LocalizedString("ClassficationEngineErrorsEscalationMessage", Strings.ResourceManager, new object[]
			{
				engineName,
				threshold,
				duration
			});
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000DD1B8 File Offset: 0x000DB3B8
		public static LocalizedString PopDeepTestEscalationBodyENT(string serverName, string probeName)
		{
			return new LocalizedString("PopDeepTestEscalationBodyENT", Strings.ResourceManager, new object[]
			{
				serverName,
				probeName
			});
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000DD1E4 File Offset: 0x000DB3E4
		public static LocalizedString DatabaseRepeatedMountsEscalationSubject(string databaseName, TimeSpan duration)
		{
			return new LocalizedString("DatabaseRepeatedMountsEscalationSubject", Strings.ResourceManager, new object[]
			{
				databaseName,
				duration
			});
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x000DD215 File Offset: 0x000DB415
		public static LocalizedString DirectoryConfigDiscrepancyEscalationMessage
		{
			get
			{
				return new LocalizedString("DirectoryConfigDiscrepancyEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x000DD22C File Offset: 0x000DB42C
		public static LocalizedString RwsDatamartConnectionEscalationSubject(string serverName)
		{
			return new LocalizedString("RwsDatamartConnectionEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x000DD254 File Offset: 0x000DB454
		public static LocalizedString NetworkAdapterRecoveryResponderName
		{
			get
			{
				return new LocalizedString("NetworkAdapterRecoveryResponderName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x000DD26B File Offset: 0x000DB46B
		public static LocalizedString SearchGracefulDegradationStatusEscalationMessage
		{
			get
			{
				return new LocalizedString("SearchGracefulDegradationStatusEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000DD284 File Offset: 0x000DB484
		public static LocalizedString SearchIndexBacklog(string databaseName, string backlog, string retryQueue, string upTime, string serverStatus)
		{
			return new LocalizedString("SearchIndexBacklog", Strings.ResourceManager, new object[]
			{
				databaseName,
				backlog,
				retryQueue,
				upTime,
				serverStatus
			});
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x000DD2BD File Offset: 0x000DB4BD
		public static LocalizedString DnsServiceRestartResponderName
		{
			get
			{
				return new LocalizedString("DnsServiceRestartResponderName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x000DD2D4 File Offset: 0x000DB4D4
		public static LocalizedString ELCMailboxSLAEscalationMessage
		{
			get
			{
				return new LocalizedString("ELCMailboxSLAEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x000DD2EB File Offset: 0x000DB4EB
		public static LocalizedString JournalingEscalationMessage
		{
			get
			{
				return new LocalizedString("JournalingEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002657 RID: 9815 RVA: 0x000DD302 File Offset: 0x000DB502
		public static LocalizedString MaintenanceTimeoutEscalationSubject
		{
			get
			{
				return new LocalizedString("MaintenanceTimeoutEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x000DD319 File Offset: 0x000DB519
		public static LocalizedString ContentsUnpredictableEscalationMessage
		{
			get
			{
				return new LocalizedString("ContentsUnpredictableEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x000DD330 File Offset: 0x000DB530
		public static LocalizedString EscalationSubjectUnhealthy
		{
			get
			{
				return new LocalizedString("EscalationSubjectUnhealthy", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000DD348 File Offset: 0x000DB548
		public static LocalizedString TransportSyncOutOfSLA(string databaseName, string guid)
		{
			return new LocalizedString("TransportSyncOutOfSLA", Strings.ResourceManager, new object[]
			{
				databaseName,
				guid
			});
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000DD374 File Offset: 0x000DB574
		public static LocalizedString EwsAutodEscalationMessageUnhealthy(string recoveryDetails)
		{
			return new LocalizedString("EwsAutodEscalationMessageUnhealthy", Strings.ResourceManager, new object[]
			{
				recoveryDetails
			});
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x000DD39C File Offset: 0x000DB59C
		public static LocalizedString AsyncAuditLogSearchEscalationSubject
		{
			get
			{
				return new LocalizedString("AsyncAuditLogSearchEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000DD3B4 File Offset: 0x000DB5B4
		public static LocalizedString SecurityAlertEscalationMessage(string alertName)
		{
			return new LocalizedString("SecurityAlertEscalationMessage", Strings.ResourceManager, new object[]
			{
				alertName
			});
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x000DD3DC File Offset: 0x000DB5DC
		public static LocalizedString VersionBucketsAllocatedEscalationEscalationMessageEnt(TimeSpan duration, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("VersionBucketsAllocatedEscalationEscalationMessageEnt", Strings.ResourceManager, new object[]
			{
				duration,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x000DD40D File Offset: 0x000DB60D
		public static LocalizedString DefaultEscalationMessage
		{
			get
			{
				return new LocalizedString("DefaultEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000DD424 File Offset: 0x000DB624
		public static LocalizedString PushNotificationSendPublishNotificationError(string serverName)
		{
			return new LocalizedString("PushNotificationSendPublishNotificationError", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000DD44C File Offset: 0x000DB64C
		public static LocalizedString SearchCatalogTooBig(string databaseName, string databaseSizeGb, string catalogSizeGb, string threshold)
		{
			return new LocalizedString("SearchCatalogTooBig", Strings.ResourceManager, new object[]
			{
				databaseName,
				databaseSizeGb,
				catalogSizeGb,
				threshold
			});
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x000DD480 File Offset: 0x000DB680
		public static LocalizedString SyntheticReplicationMonitorEscalationMessage
		{
			get
			{
				return new LocalizedString("SyntheticReplicationMonitorEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06002663 RID: 9827 RVA: 0x000DD497 File Offset: 0x000DB697
		public static LocalizedString EDiscoveryEscalationBodyDCHTML
		{
			get
			{
				return new LocalizedString("EDiscoveryEscalationBodyDCHTML", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000DD4B0 File Offset: 0x000DB6B0
		public static LocalizedString SearchMemoryUsageOverThreshold(string memoryUsage)
		{
			return new LocalizedString("SearchMemoryUsageOverThreshold", Strings.ResourceManager, new object[]
			{
				memoryUsage
			});
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x000DD4D8 File Offset: 0x000DB6D8
		public static LocalizedString OwaTooManyLogoffFailuresSubject
		{
			get
			{
				return new LocalizedString("OwaTooManyLogoffFailuresSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x000DD4EF File Offset: 0x000DB6EF
		public static LocalizedString CheckProvisionedDCExceptionMessage
		{
			get
			{
				return new LocalizedString("CheckProvisionedDCExceptionMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x000DD506 File Offset: 0x000DB706
		public static LocalizedString ProvisioningBigVolumeErrorEscalationSubject
		{
			get
			{
				return new LocalizedString("ProvisioningBigVolumeErrorEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000DD520 File Offset: 0x000DB720
		public static LocalizedString LocalMachineDriveProtectedWithDraWithoutDecryptorEscalationSubject(string serverName)
		{
			return new LocalizedString("LocalMachineDriveProtectedWithDraWithoutDecryptorEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000DD548 File Offset: 0x000DB748
		public static LocalizedString StoreMaintenanceAssistantEscalationMessageDc(TimeSpan duration, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("StoreMaintenanceAssistantEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				duration,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x000DD579 File Offset: 0x000DB779
		public static LocalizedString UMCertificateNearExpiryEscalationMessage
		{
			get
			{
				return new LocalizedString("UMCertificateNearExpiryEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x000DD590 File Offset: 0x000DB790
		public static LocalizedString FailureItemMessageForNTFSCorruption
		{
			get
			{
				return new LocalizedString("FailureItemMessageForNTFSCorruption", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x000DD5A7 File Offset: 0x000DB7A7
		public static LocalizedString DatabaseRPCLatencyMonitorGreenMessage
		{
			get
			{
				return new LocalizedString("DatabaseRPCLatencyMonitorGreenMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x0600266D RID: 9837 RVA: 0x000DD5BE File Offset: 0x000DB7BE
		public static LocalizedString RelocationServicePermanentExceptionMessage
		{
			get
			{
				return new LocalizedString("RelocationServicePermanentExceptionMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000DD5D8 File Offset: 0x000DB7D8
		public static LocalizedString EacSelfTestEscalationSubject(string serverName)
		{
			return new LocalizedString("EacSelfTestEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000DD600 File Offset: 0x000DB800
		public static LocalizedString EacCtpTestEscalationSubject(string serverName)
		{
			return new LocalizedString("EacCtpTestEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x000DD628 File Offset: 0x000DB828
		public static LocalizedString LiveIdAuthenticationEscalationMesage
		{
			get
			{
				return new LocalizedString("LiveIdAuthenticationEscalationMesage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x000DD640 File Offset: 0x000DB840
		public static LocalizedString InvokeNowProbeResultNotFound(string requestId, int workDefinitionId)
		{
			return new LocalizedString("InvokeNowProbeResultNotFound", Strings.ResourceManager, new object[]
			{
				requestId,
				workDefinitionId
			});
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x000DD671 File Offset: 0x000DB871
		public static LocalizedString JournalArchiveEscalationMessage
		{
			get
			{
				return new LocalizedString("JournalArchiveEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000DD688 File Offset: 0x000DB888
		public static LocalizedString BugCheckActionFailed(string errMsg)
		{
			return new LocalizedString("BugCheckActionFailed", Strings.ResourceManager, new object[]
			{
				errMsg
			});
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x000DD6B0 File Offset: 0x000DB8B0
		public static LocalizedString Pop3ProtocolUnhealthy
		{
			get
			{
				return new LocalizedString("Pop3ProtocolUnhealthy", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x000DD6C8 File Offset: 0x000DB8C8
		public static LocalizedString SiteMailboxDocumentSyncEscalationMessage(int minFailPercent)
		{
			return new LocalizedString("SiteMailboxDocumentSyncEscalationMessage", Strings.ResourceManager, new object[]
			{
				minFailPercent
			});
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000DD6F8 File Offset: 0x000DB8F8
		public static LocalizedString SearchIndexBacklogWithProcessingRate(string databaseName, string backlog, string retryQueue, string completedCount, string processingRate, string minutes, string upTime, string serverStatus)
		{
			return new LocalizedString("SearchIndexBacklogWithProcessingRate", Strings.ResourceManager, new object[]
			{
				databaseName,
				backlog,
				retryQueue,
				completedCount,
				processingRate,
				minutes,
				upTime,
				serverStatus
			});
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x000DD740 File Offset: 0x000DB940
		public static LocalizedString HxServiceEscalationMessageUnhealthy
		{
			get
			{
				return new LocalizedString("HxServiceEscalationMessageUnhealthy", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000DD758 File Offset: 0x000DB958
		public static LocalizedString SearchSingleCopyEscalationMessage(string databaseName)
		{
			return new LocalizedString("SearchSingleCopyEscalationMessage", Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x000DD780 File Offset: 0x000DB980
		public static LocalizedString RequestForNewRidPoolFailedEscalationMessage
		{
			get
			{
				return new LocalizedString("RequestForNewRidPoolFailedEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x000DD798 File Offset: 0x000DB998
		public static LocalizedString SearchIndexSingleHealthyCopy(string databaseName, string details)
		{
			return new LocalizedString("SearchIndexSingleHealthyCopy", Strings.ResourceManager, new object[]
			{
				databaseName,
				details
			});
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x000DD7C4 File Offset: 0x000DB9C4
		public static LocalizedString MRSRepeatedlyCrashingEscalationMessage(int count, TimeSpan duration)
		{
			return new LocalizedString("MRSRepeatedlyCrashingEscalationMessage", Strings.ResourceManager, new object[]
			{
				count,
				duration
			});
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x000DD7FA File Offset: 0x000DB9FA
		public static LocalizedString DatabaseSizeEscalationSubject
		{
			get
			{
				return new LocalizedString("DatabaseSizeEscalationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000DD814 File Offset: 0x000DBA14
		public static LocalizedString DatabaseSizeEscalationMessageDc(string invokeNowCommand, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("DatabaseSizeEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				invokeNowCommand,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x000DD840 File Offset: 0x000DBA40
		public static LocalizedString TooManyDatabaseMountedEscalationMessage(int threshold)
		{
			return new LocalizedString("TooManyDatabaseMountedEscalationMessage", Strings.ResourceManager, new object[]
			{
				threshold
			});
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x000DD86D File Offset: 0x000DBA6D
		public static LocalizedString OabMailboxManifestEmpty
		{
			get
			{
				return new LocalizedString("OabMailboxManifestEmpty", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x000DD884 File Offset: 0x000DBA84
		public static LocalizedString SearchCatalogNotHealthyEscalationMessage(string databaseName)
		{
			return new LocalizedString("SearchCatalogNotHealthyEscalationMessage", Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x000DD8AC File Offset: 0x000DBAAC
		public static LocalizedString CheckFsmoRolesScriptExceptionMessage
		{
			get
			{
				return new LocalizedString("CheckFsmoRolesScriptExceptionMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002682 RID: 9858 RVA: 0x000DD8C3 File Offset: 0x000DBAC3
		public static LocalizedString PopImapSecondaryEndpoint
		{
			get
			{
				return new LocalizedString("PopImapSecondaryEndpoint", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002683 RID: 9859 RVA: 0x000DD8DA File Offset: 0x000DBADA
		public static LocalizedString CannotFunctionNormallyEscalationMessage
		{
			get
			{
				return new LocalizedString("CannotFunctionNormallyEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000DD8F4 File Offset: 0x000DBAF4
		public static LocalizedString RcaTaskOutlineEntry(int milliseconds, LocalizedString resultType, LocalizedString taskName)
		{
			return new LocalizedString("RcaTaskOutlineEntry", Strings.ResourceManager, new object[]
			{
				milliseconds,
				resultType,
				taskName
			});
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000DD934 File Offset: 0x000DBB34
		public static LocalizedString AssistantsNotRunningToCompletionError(string error)
		{
			return new LocalizedString("AssistantsNotRunningToCompletionError", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x000DD95C File Offset: 0x000DBB5C
		public static LocalizedString NetworkAdapterRssEscalationSubject(string serverName)
		{
			return new LocalizedString("NetworkAdapterRssEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x000DD984 File Offset: 0x000DBB84
		public static LocalizedString EseDbTimeAdvanceEscalationSubject(string component, string machine, string database)
		{
			return new LocalizedString("EseDbTimeAdvanceEscalationSubject", Strings.ResourceManager, new object[]
			{
				component,
				machine,
				database
			});
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000DD9B4 File Offset: 0x000DBBB4
		public static LocalizedString SearchIndexActiveCopyNotIndxed(string databaseName, string state)
		{
			return new LocalizedString("SearchIndexActiveCopyNotIndxed", Strings.ResourceManager, new object[]
			{
				databaseName,
				state
			});
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002689 RID: 9865 RVA: 0x000DD9E0 File Offset: 0x000DBBE0
		public static LocalizedString EscalationMessageHealthy
		{
			get
			{
				return new LocalizedString("EscalationMessageHealthy", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x000DD9F8 File Offset: 0x000DBBF8
		public static LocalizedString ServiceNotRunningEscalationSubject(string serviceName)
		{
			return new LocalizedString("ServiceNotRunningEscalationSubject", Strings.ResourceManager, new object[]
			{
				serviceName
			});
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000DDA20 File Offset: 0x000DBC20
		public static LocalizedString SearchCatalogNotificationFeederLastEventZero(string databaseName, string serverName)
		{
			return new LocalizedString("SearchCatalogNotificationFeederLastEventZero", Strings.ResourceManager, new object[]
			{
				databaseName,
				serverName
			});
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x000DDA4C File Offset: 0x000DBC4C
		public static LocalizedString PublicFolderMoveJobStuckEscalationMessage
		{
			get
			{
				return new LocalizedString("PublicFolderMoveJobStuckEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x000DDA63 File Offset: 0x000DBC63
		public static LocalizedString SearchServiceNotRunningEscalationMessage
		{
			get
			{
				return new LocalizedString("SearchServiceNotRunningEscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000DDA7C File Offset: 0x000DBC7C
		public static LocalizedString SearchQueryStxTimeout(string query, string mailboxSmtpAddress, int seconds)
		{
			return new LocalizedString("SearchQueryStxTimeout", Strings.ResourceManager, new object[]
			{
				query,
				mailboxSmtpAddress,
				seconds
			});
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000DDAB4 File Offset: 0x000DBCB4
		public static LocalizedString DatabaseDiskReadLatencyEscalationMessageDc(TimeSpan duration, string unhealthyMonitorsCommand)
		{
			return new LocalizedString("DatabaseDiskReadLatencyEscalationMessageDc", Strings.ResourceManager, new object[]
			{
				duration,
				unhealthyMonitorsCommand
			});
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000DDAE8 File Offset: 0x000DBCE8
		public static LocalizedString SearchIndexCopyBacklogStatus(string copyName, string databaseStatus, string catalogStatus, string backlog, string retryQueueSize)
		{
			return new LocalizedString("SearchIndexCopyBacklogStatus", Strings.ResourceManager, new object[]
			{
				copyName,
				databaseStatus,
				catalogStatus,
				backlog,
				retryQueueSize
			});
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000DDB24 File Offset: 0x000DBD24
		public static LocalizedString EseLostFlushDetectedEscalationMessage(string machine, string database)
		{
			return new LocalizedString("EseLostFlushDetectedEscalationMessage", Strings.ResourceManager, new object[]
			{
				machine,
				database
			});
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x000DDB50 File Offset: 0x000DBD50
		public static LocalizedString MobilityAccountPassword
		{
			get
			{
				return new LocalizedString("MobilityAccountPassword", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x000DDB67 File Offset: 0x000DBD67
		public static LocalizedString EventLogProbeProviderName
		{
			get
			{
				return new LocalizedString("EventLogProbeProviderName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000DDB80 File Offset: 0x000DBD80
		public static LocalizedString PswsEscalationSubject(string serverName)
		{
			return new LocalizedString("PswsEscalationSubject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x000DDBA8 File Offset: 0x000DBDA8
		public static LocalizedString VersionStore623EscalationMessage
		{
			get
			{
				return new LocalizedString("VersionStore623EscalationMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000DDBBF File Offset: 0x000DBDBF
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04001997 RID: 6551
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(325);

		// Token: 0x04001998 RID: 6552
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000596 RID: 1430
		public enum IDs : uint
		{
			// Token: 0x0400199A RID: 6554
			DatabaseGuidNotSupplied = 2286371297U,
			// Token: 0x0400199B RID: 6555
			OABGenTenantOutOfSLABody = 1189391965U,
			// Token: 0x0400199C RID: 6556
			SearchFailToSaveMessage = 52276049U,
			// Token: 0x0400199D RID: 6557
			ForwardSyncHaltEscalationSubject = 3960338262U,
			// Token: 0x0400199E RID: 6558
			MaintenanceFailureEscalationMessage = 312538007U,
			// Token: 0x0400199F RID: 6559
			DatabaseSpaceHelpString = 299573369U,
			// Token: 0x040019A0 RID: 6560
			PumServiceNotRunningEscalationMessage = 683259685U,
			// Token: 0x040019A1 RID: 6561
			HealthSetMaintenanceEscalationSubjectPrefix = 1378981546U,
			// Token: 0x040019A2 RID: 6562
			RegisterDnsHostRecordResponderName = 2358296594U,
			// Token: 0x040019A3 RID: 6563
			RcaDiscoveryOutlookAnywhereNotFound = 2482686375U,
			// Token: 0x040019A4 RID: 6564
			UnableToCompleteTopologyEscalationMessage = 3458357958U,
			// Token: 0x040019A5 RID: 6565
			LargestDeliveryQueueLengthEscalationMessage = 945891153U,
			// Token: 0x040019A6 RID: 6566
			DSNotifyQueueHigh15MinutesEscalationMessage = 1718676120U,
			// Token: 0x040019A7 RID: 6567
			OutStandingATQRequests15MinutesEscalationMessage = 2503653255U,
			// Token: 0x040019A8 RID: 6568
			ADDatabaseCorruption1017EscalationMessage = 2592211666U,
			// Token: 0x040019A9 RID: 6569
			DeviceDegradedEscalationMessage = 2321911502U,
			// Token: 0x040019AA RID: 6570
			RemoteDomainControllerStateEscalationMessage = 1918560769U,
			// Token: 0x040019AB RID: 6571
			OWACalendarAppPoolEscalationBody = 164779627U,
			// Token: 0x040019AC RID: 6572
			MediaEstablishedFailedEscalationMessage = 1218627587U,
			// Token: 0x040019AD RID: 6573
			RequestForFfoApprovalToOfflineFailed = 1980518279U,
			// Token: 0x040019AE RID: 6574
			InsufficientInformationKCCEscalationMessage = 3706262484U,
			// Token: 0x040019AF RID: 6575
			ClusterNodeEvictedEscalationMessage = 2150007296U,
			// Token: 0x040019B0 RID: 6576
			OwaOutsideInDatabaseAvailabilityFailuresSubject = 635314528U,
			// Token: 0x040019B1 RID: 6577
			RouteTableRecoveryResponderName = 112230113U,
			// Token: 0x040019B2 RID: 6578
			AssistantsActiveDatabaseSubject = 2257888472U,
			// Token: 0x040019B3 RID: 6579
			ForwardSyncMonopolizedEscalationSubject = 3750243459U,
			// Token: 0x040019B4 RID: 6580
			UMProtectedVoiceMessageEncryptDecryptFailedEscalationMessage = 805912300U,
			// Token: 0x040019B5 RID: 6581
			SearchIndexFailureEscalationMessage = 1299295308U,
			// Token: 0x040019B6 RID: 6582
			ForwardSyncCookieNotUpToDateEscalationMessage = 4294224569U,
			// Token: 0x040019B7 RID: 6583
			CannotBootEscalationMessage = 2092011713U,
			// Token: 0x040019B8 RID: 6584
			PassiveReplicationPerformanceCounterProbeEscalationMessage = 2288602703U,
			// Token: 0x040019B9 RID: 6585
			OwaTooManyStartPageFailuresSubject = 3399715728U,
			// Token: 0x040019BA RID: 6586
			OwaOutsideInDatabaseAvailabilityFailuresBody = 4261321224U,
			// Token: 0x040019BB RID: 6587
			SearchWordBreakerLoadingFailureEscalationMessage = 409381696U,
			// Token: 0x040019BC RID: 6588
			Pop3CommandProcessingTimeEscalationMessage = 741999051U,
			// Token: 0x040019BD RID: 6589
			DeltaSyncEndpointUnreachableEscalationMessage = 3741312692U,
			// Token: 0x040019BE RID: 6590
			EventLogProbeRedEvents = 1119726172U,
			// Token: 0x040019BF RID: 6591
			ProvisioningBigVolumeErrorProbeName = 109013642U,
			// Token: 0x040019C0 RID: 6592
			PassiveADReplicationMonitorEscalationMessage = 2897164042U,
			// Token: 0x040019C1 RID: 6593
			UMCertificateThumbprint = 373161856U,
			// Token: 0x040019C2 RID: 6594
			ForwardSyncMonopolizedEscalationMessage = 893346260U,
			// Token: 0x040019C3 RID: 6595
			NoResponseHeadersAvailable = 2704062871U,
			// Token: 0x040019C4 RID: 6596
			AdminAuditingAvailabilityFailureEscalationSubject = 380917770U,
			// Token: 0x040019C5 RID: 6597
			DeltaSyncPartnerAuthenticationFailedEscalationMessage = 1943611390U,
			// Token: 0x040019C6 RID: 6598
			SharedCacheEscalationSubject = 1893530126U,
			// Token: 0x040019C7 RID: 6599
			JournalingEscalationSubject = 3957270050U,
			// Token: 0x040019C8 RID: 6600
			HighProcessor15MinutesEscalationMessage = 2370864531U,
			// Token: 0x040019C9 RID: 6601
			NetworkAdapterRssEscalationMessage = 3308508053U,
			// Token: 0x040019CA RID: 6602
			CPUOverThresholdErrorEscalationSubject = 1482699764U,
			// Token: 0x040019CB RID: 6603
			Transport80thPercentileMissingSLAEscalationMessage = 1218523202U,
			// Token: 0x040019CC RID: 6604
			InferenceTrainingSLAEscalationMessage = 2302596019U,
			// Token: 0x040019CD RID: 6605
			EDiscoveryEscalationBodyEntText = 2517955710U,
			// Token: 0x040019CE RID: 6606
			AsynchronousAuditSearchAvailabilityFailureEscalationSubject = 1451525307U,
			// Token: 0x040019CF RID: 6607
			SearchRopNotSupportedEscalationMessage = 3795580654U,
			// Token: 0x040019D0 RID: 6608
			PushNotificationEnterpriseUnknownError = 909096326U,
			// Token: 0x040019D1 RID: 6609
			OwaClientAccessRoleNotInstalled = 610566341U,
			// Token: 0x040019D2 RID: 6610
			BridgeHeadReplicationEscalationMessage = 3645413289U,
			// Token: 0x040019D3 RID: 6611
			PushNotificationEnterpriseNotConfigured = 2836474835U,
			// Token: 0x040019D4 RID: 6612
			IncompatibleVectorEscalationMessage = 892806076U,
			// Token: 0x040019D5 RID: 6613
			DatabaseCorruptionEscalationMessage = 4138453910U,
			// Token: 0x040019D6 RID: 6614
			ReplicationOutdatedObjectsFailedEscalationMessage = 1752264565U,
			// Token: 0x040019D7 RID: 6615
			DatabaseCorruptEscalationMessage = 443970804U,
			// Token: 0x040019D8 RID: 6616
			HealthSetAlertSuppressionWarning = 48294403U,
			// Token: 0x040019D9 RID: 6617
			OwaIMInitializationFailedMessage = 1065939221U,
			// Token: 0x040019DA RID: 6618
			ForwardSyncHaltEscalationMessage = 2172476143U,
			// Token: 0x040019DB RID: 6619
			OfflineGLSEscalationMessage = 1679303927U,
			// Token: 0x040019DC RID: 6620
			UnableToRunEscalateByDatabaseHealthResponder = 2914700647U,
			// Token: 0x040019DD RID: 6621
			AggregateDeliveryQueueLengthEscalationMessage = 2514146180U,
			// Token: 0x040019DE RID: 6622
			NoCafeMonitoringAccountsAvailable = 4070157535U,
			// Token: 0x040019DF RID: 6623
			MediaEdgeResourceAllocationFailedEscalationMessage = 2950938112U,
			// Token: 0x040019E0 RID: 6624
			DRAPendingReplication5MinutesEscalationMessage = 1420198520U,
			// Token: 0x040019E1 RID: 6625
			SchemaPartitionFailedEscalationMessage = 357692756U,
			// Token: 0x040019E2 RID: 6626
			DatabaseSchemaVersionCheckEscalationSubject = 2064178893U,
			// Token: 0x040019E3 RID: 6627
			UMSipListeningPort = 878340914U,
			// Token: 0x040019E4 RID: 6628
			ELCMailboxSLAEscalationSubject = 3803135725U,
			// Token: 0x040019E5 RID: 6629
			DHCPNacksEscalationMessage = 1586522085U,
			// Token: 0x040019E6 RID: 6630
			ELCArchiveDumpsterEscalationMessage = 1507797344U,
			// Token: 0x040019E7 RID: 6631
			KDCServiceStatusTestMessage = 3628981090U,
			// Token: 0x040019E8 RID: 6632
			LowMemoryUnderThresholdErrorEscalationSubject = 401186895U,
			// Token: 0x040019E9 RID: 6633
			OwaIMInitializationFailedSubject = 1108409616U,
			// Token: 0x040019EA RID: 6634
			PingConnectivityEscalationSubject = 3717637996U,
			// Token: 0x040019EB RID: 6635
			PublicFolderConnectionCountEscalationMessage = 529762034U,
			// Token: 0x040019EC RID: 6636
			FastNodeNotHealthyEscalationMessage = 663711086U,
			// Token: 0x040019ED RID: 6637
			CheckDCMMDivergenceScriptExceptionMessage = 1307224852U,
			// Token: 0x040019EE RID: 6638
			CrossPremiseMailflowEscalationMessage = 1220266742U,
			// Token: 0x040019EF RID: 6639
			ForwardSyncStandardCompanyEscalationSubject = 2945963449U,
			// Token: 0x040019F0 RID: 6640
			JournalArchiveEscalationSubject = 1462332256U,
			// Token: 0x040019F1 RID: 6641
			DoMTConnectivityEscalateMessage = 3746115424U,
			// Token: 0x040019F2 RID: 6642
			InferenceComponentDisabledEscalationMessage = 680617032U,
			// Token: 0x040019F3 RID: 6643
			NoBackendMonitoringAccountsAvailable = 2776049552U,
			// Token: 0x040019F4 RID: 6644
			ActiveDirectoryConnectivityEscalationMessage = 349561476U,
			// Token: 0x040019F5 RID: 6645
			SyntheticReplicationTransactionEscalationMessage = 4196713157U,
			// Token: 0x040019F6 RID: 6646
			OabFileLoadExceptionEncounteredSubject = 814146069U,
			// Token: 0x040019F7 RID: 6647
			RegistryAccessDeniedEscalationMessage = 4268033680U,
			// Token: 0x040019F8 RID: 6648
			AuditLogSearchServiceletEscalationSubject = 2465131700U,
			// Token: 0x040019F9 RID: 6649
			EventLogProbeLogName = 649999061U,
			// Token: 0x040019FA RID: 6650
			Imap4ProtocolUnhealthy = 536159405U,
			// Token: 0x040019FB RID: 6651
			DLExpansionEscalationMessage = 1179543515U,
			// Token: 0x040019FC RID: 6652
			ReplicationFailuresEscalationMessage = 2046186369U,
			// Token: 0x040019FD RID: 6653
			SCTStateMonitoringScriptExceptionMessage = 1915337724U,
			// Token: 0x040019FE RID: 6654
			ELCExceptionEscalationMessage = 1417399775U,
			// Token: 0x040019FF RID: 6655
			OabTooManyHttpErrorResponsesEncounteredBody = 1844700225U,
			// Token: 0x04001A00 RID: 6656
			QuarantineEscalationMessage = 275645054U,
			// Token: 0x04001A01 RID: 6657
			TransportRejectingMessageSubmissions = 3642796412U,
			// Token: 0x04001A02 RID: 6658
			PublicFolderConnectionCountEscalationSubject = 3898201799U,
			// Token: 0x04001A03 RID: 6659
			PowerShellProfileEscalationSubject = 2285670747U,
			// Token: 0x04001A04 RID: 6660
			DivergenceBetweenCAAndAD1006EscalationMessage = 93821081U,
			// Token: 0x04001A05 RID: 6661
			UnreachableQueueLengthEscalationMessage = 726991111U,
			// Token: 0x04001A06 RID: 6662
			OabFileLoadExceptionEncounteredBody = 3639475733U,
			// Token: 0x04001A07 RID: 6663
			PublicFolderSyncEscalationSubject = 252047361U,
			// Token: 0x04001A08 RID: 6664
			Imap4CommandProcessingTimeEscalationMessage = 4139459438U,
			// Token: 0x04001A09 RID: 6665
			InvalidSearchResultsExceptionMessage = 3378952345U,
			// Token: 0x04001A0A RID: 6666
			SearchInformationNotAvailable = 4272242116U,
			// Token: 0x04001A0B RID: 6667
			ActiveDatabaseAvailabilityEscalationSubject = 2844472229U,
			// Token: 0x04001A0C RID: 6668
			ELCPermanentEscalationSubject = 952182041U,
			// Token: 0x04001A0D RID: 6669
			EventLogProbeGreenEvents = 1117652268U,
			// Token: 0x04001A0E RID: 6670
			ClusterHangEscalationMessage = 4155705872U,
			// Token: 0x04001A0F RID: 6671
			FEPServiceNotRunningEscalationMessage = 891001936U,
			// Token: 0x04001A10 RID: 6672
			RidMonitorEscalationMessage = 286684653U,
			// Token: 0x04001A11 RID: 6673
			SystemMailboxGuidNotFound = 2576630513U,
			// Token: 0x04001A12 RID: 6674
			MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedEscalationMessage = 2727074534U,
			// Token: 0x04001A13 RID: 6675
			SearchTransportAgentFailureEscalationMessage = 3448175994U,
			// Token: 0x04001A14 RID: 6676
			TransportMessageCategorizationEscalationMessage = 2551706401U,
			// Token: 0x04001A15 RID: 6677
			InocrrectSCTStateExceptionMessage = 2595326046U,
			// Token: 0x04001A16 RID: 6678
			DataIssueEscalationMessage = 1789741911U,
			// Token: 0x04001A17 RID: 6679
			KerbAuthFailureEscalationMessagPAC = 3993818915U,
			// Token: 0x04001A18 RID: 6680
			DivergenceInDefinitionEscalationMessage = 423349970U,
			// Token: 0x04001A19 RID: 6681
			MobilityAccount = 2198917190U,
			// Token: 0x04001A1A RID: 6682
			OwaTooManyLogoffFailuresBody = 682587060U,
			// Token: 0x04001A1B RID: 6683
			ForwardSyncProcessRepeatedlyCrashingEscalationSubject = 770708540U,
			// Token: 0x04001A1C RID: 6684
			ADDatabaseCorruptionEscalationMessage = 1169357891U,
			// Token: 0x04001A1D RID: 6685
			MailboxAuditingAvailabilityFailureEscalationSubject = 580917981U,
			// Token: 0x04001A1E RID: 6686
			TopologyServiceConnectivityEscalationMessage = 1969709639U,
			// Token: 0x04001A1F RID: 6687
			UMSipTransport = 4160621849U,
			// Token: 0x04001A20 RID: 6688
			OabProtocolEscalationBody = 2022121183U,
			// Token: 0x04001A21 RID: 6689
			PushNotificationEnterpriseEmptyServiceUri = 3048659136U,
			// Token: 0x04001A22 RID: 6690
			PushNotificationEnterpriseAuthError = 3214050158U,
			// Token: 0x04001A23 RID: 6691
			UnableToRunAlertNotificationTypeByDatabaseCopyStateResponder = 2021347314U,
			// Token: 0x04001A24 RID: 6692
			ELCDumpsterWarningEscalationSubject = 1920206617U,
			// Token: 0x04001A25 RID: 6693
			OabMailboxEscalationBody = 2542492469U,
			// Token: 0x04001A26 RID: 6694
			CheckZombieDCEscalateMessage = 3640747412U,
			// Token: 0x04001A27 RID: 6695
			RidSetMonitorEscalationMessage = 2206456195U,
			// Token: 0x04001A28 RID: 6696
			PushNotificationCafeEndpointUnhealthy = 584392819U,
			// Token: 0x04001A29 RID: 6697
			ProvisioningBigVolumeErrorEscalationMessage = 632484951U,
			// Token: 0x04001A2A RID: 6698
			PublicFolderMailboxQuotaEscalationMessage = 58433467U,
			// Token: 0x04001A2B RID: 6699
			CASRoutingFailureEscalationSubject = 1077120006U,
			// Token: 0x04001A2C RID: 6700
			OAuthRequestFailureEscalationBody = 3549972347U,
			// Token: 0x04001A2D RID: 6701
			GLSEscalationMessage = 3456628766U,
			// Token: 0x04001A2E RID: 6702
			SCTNotFoundForAllVersionsExceptionMessage = 2809892364U,
			// Token: 0x04001A2F RID: 6703
			SqlOutputStreamInRetryEscalationMessage = 1613949426U,
			// Token: 0x04001A30 RID: 6704
			DefaultEscalationSubject = 3213813466U,
			// Token: 0x04001A31 RID: 6705
			MailboxAuditingAvailabilityFailureEscalationBody = 471095525U,
			// Token: 0x04001A32 RID: 6706
			BulkProvisioningNoProgressEscalationSubject = 1276311330U,
			// Token: 0x04001A33 RID: 6707
			InfrastructureValidationSubject = 1377854022U,
			// Token: 0x04001A34 RID: 6708
			SearchMemoryUsageOverThresholdEscalationMessage = 1194398797U,
			// Token: 0x04001A35 RID: 6709
			SharedCacheEscalationMessage = 105667215U,
			// Token: 0x04001A36 RID: 6710
			CannotRecoverEscalationMessage = 799661977U,
			// Token: 0x04001A37 RID: 6711
			AsynchronousAuditSearchAvailabilityFailureEscalationBody = 4214982447U,
			// Token: 0x04001A38 RID: 6712
			OwaIMLogAnalyzerMessage = 483050246U,
			// Token: 0x04001A39 RID: 6713
			UMCertificateSubjectName = 1005998838U,
			// Token: 0x04001A3A RID: 6714
			OwaNoMailboxesAvailable = 1108329015U,
			// Token: 0x04001A3B RID: 6715
			TransportCategorizerJobsUnavailableEscalationMessage = 2516924014U,
			// Token: 0x04001A3C RID: 6716
			SingleAvailableDatabaseCopyEscalationMessage = 910032993U,
			// Token: 0x04001A3D RID: 6717
			DivergenceInSiteNameEscalationMessage = 1345149923U,
			// Token: 0x04001A3E RID: 6718
			NullSearchResponseExceptionMessage = 3893081264U,
			// Token: 0x04001A3F RID: 6719
			UncategorizedProcess = 144342951U,
			// Token: 0x04001A40 RID: 6720
			MSExchangeProtectedServiceHostCrashingMessage = 3984209328U,
			// Token: 0x04001A41 RID: 6721
			UMDatacenterLoadBalancerSipOptionsPingEscalationMessage = 3400380005U,
			// Token: 0x04001A42 RID: 6722
			PassiveReplicationMonitorEscalationMessage = 564297537U,
			// Token: 0x04001A43 RID: 6723
			ReinstallServerEscalationMessage = 4039996833U,
			// Token: 0x04001A44 RID: 6724
			ForwardSyncCookieNotUpToDateEscalationSubject = 2217573206U,
			// Token: 0x04001A45 RID: 6725
			ActiveDirectoryConnectivityLocalEscalationMessage = 2260546867U,
			// Token: 0x04001A46 RID: 6726
			PassiveDatabaseAvailabilityEscalationSubject = 70227012U,
			// Token: 0x04001A47 RID: 6727
			OabTooManyHttpErrorResponsesEncounteredSubject = 3278262633U,
			// Token: 0x04001A48 RID: 6728
			ELCTransientEscalationSubject = 2089742391U,
			// Token: 0x04001A49 RID: 6729
			SiteFailureEscalationMessage = 1923047497U,
			// Token: 0x04001A4A RID: 6730
			OnlineMeetingCreateEscalationBody = 1012424075U,
			// Token: 0x04001A4B RID: 6731
			SiteMailboxDocumentSyncEscalationSubject = 462239732U,
			// Token: 0x04001A4C RID: 6732
			NTDSCorruptionEscalationMessage = 4075662140U,
			// Token: 0x04001A4D RID: 6733
			TopoDiscoveryFailedAllServersEscalationMessage = 3665176336U,
			// Token: 0x04001A4E RID: 6734
			VersionStore1479EscalationMessage = 400522478U,
			// Token: 0x04001A4F RID: 6735
			AssistantsNotRunningToCompletionSubject = 868604876U,
			// Token: 0x04001A50 RID: 6736
			AdminAuditingAvailabilityFailureEscalationBody = 1817218534U,
			// Token: 0x04001A51 RID: 6737
			ForwardSyncLiteCompanyEscalationSubject = 3226571974U,
			// Token: 0x04001A52 RID: 6738
			MSExchangeInformationStoreCannotContactADEscalationMessage = 4083150976U,
			// Token: 0x04001A53 RID: 6739
			DHCPServerRequestsEscalationMessage = 465857804U,
			// Token: 0x04001A54 RID: 6740
			NoNTDSObjectEscalationMessage = 4092609939U,
			// Token: 0x04001A55 RID: 6741
			PublicFolderMoveJobStuckEscalationSubject = 88349250U,
			// Token: 0x04001A56 RID: 6742
			SCTMonitoringScriptExceptionMessage = 4169066611U,
			// Token: 0x04001A57 RID: 6743
			ProvisionedDCBelowMinimumEscalationMessage = 380251366U,
			// Token: 0x04001A58 RID: 6744
			KerbAuthFailureEscalationMessage = 3378026954U,
			// Token: 0x04001A59 RID: 6745
			RequestsQueuedOver500EscalationMessage = 1277128402U,
			// Token: 0x04001A5A RID: 6746
			PopImapGuid = 2497401637U,
			// Token: 0x04001A5B RID: 6747
			MaintenanceFailureEscalationSubject = 77538240U,
			// Token: 0x04001A5C RID: 6748
			TransportServerDownEscalationMessage = 1577351776U,
			// Token: 0x04001A5D RID: 6749
			PopImapEndpoint = 3174826533U,
			// Token: 0x04001A5E RID: 6750
			NtlmConnectivityEscalationMessage = 3229931132U,
			// Token: 0x04001A5F RID: 6751
			OabTooManyWebAppStartsSubject = 499742697U,
			// Token: 0x04001A60 RID: 6752
			CafeEscalationSubjectUnhealthy = 3922382486U,
			// Token: 0x04001A61 RID: 6753
			DnsHostRecordProbeName = 2676970837U,
			// Token: 0x04001A62 RID: 6754
			ELCArchiveDumpsterWarningEscalationSubject = 1037389911U,
			// Token: 0x04001A63 RID: 6755
			JournalFilterAgentEscalationMessage = 1536257732U,
			// Token: 0x04001A64 RID: 6756
			WacDiscoveryFailureSubject = 1098709463U,
			// Token: 0x04001A65 RID: 6757
			CASRoutingLatencyEscalationSubject = 887962606U,
			// Token: 0x04001A66 RID: 6758
			EDSJobPoisonedEscalationMessage = 1060163078U,
			// Token: 0x04001A67 RID: 6759
			JournalFilterAgentEscalationSubject = 938822773U,
			// Token: 0x04001A68 RID: 6760
			DnsHostRecordMonitorName = 3660582759U,
			// Token: 0x04001A69 RID: 6761
			VersionStore2008EscalationMessage = 3238760785U,
			// Token: 0x04001A6A RID: 6762
			DnsServiceMonitorName = 2695821343U,
			// Token: 0x04001A6B RID: 6763
			DatabaseAvailabilityHelpString = 3687026854U,
			// Token: 0x04001A6C RID: 6764
			PublicFolderMailboxQuotaEscalationSubject = 3465747486U,
			// Token: 0x04001A6D RID: 6765
			HealthSetEscalationSubjectPrefix = 1074585207U,
			// Token: 0x04001A6E RID: 6766
			DHCPServerDeclinesEscalationMessage = 4054742343U,
			// Token: 0x04001A6F RID: 6767
			TrustMonitorProbeEscalationMessage = 235731656U,
			// Token: 0x04001A70 RID: 6768
			InvalidIncludedAssistantType = 959637577U,
			// Token: 0x04001A71 RID: 6769
			ReplicationDisabledEscalationMessage = 861211960U,
			// Token: 0x04001A72 RID: 6770
			ProvisioningBigVolumeErrorEscalateResponderName = 2731022862U,
			// Token: 0x04001A73 RID: 6771
			CASRoutingLatencyEscalationBody = 3881757534U,
			// Token: 0x04001A74 RID: 6772
			SecurityAlertMalwareDetectedEscalationMessage = 1221520849U,
			// Token: 0x04001A75 RID: 6773
			SynchronousAuditSearchAvailabilityFailureEscalationBody = 1306675352U,
			// Token: 0x04001A76 RID: 6774
			MaintenanceTimeoutEscalationMessage = 4173100116U,
			// Token: 0x04001A77 RID: 6775
			DeltaSyncServiceEndpointsLoadFailedEscalationMessage = 217815433U,
			// Token: 0x04001A78 RID: 6776
			SchedulingLatencyEscalateResponderMessage = 269594253U,
			// Token: 0x04001A79 RID: 6777
			PowerShellProfileEscalationMessage = 4073533312U,
			// Token: 0x04001A7A RID: 6778
			OAuthRequestFailureEscalationSubject = 3913093087U,
			// Token: 0x04001A7B RID: 6779
			OabMailboxNoOrgMailbox = 1166689995U,
			// Token: 0x04001A7C RID: 6780
			PushNotificationEnterpriseEmptyDomain = 3054327783U,
			// Token: 0x04001A7D RID: 6781
			AssistantsOutOfSlaMessage = 3814043621U,
			// Token: 0x04001A7E RID: 6782
			EwsAutodEscalationSubjectUnhealthy = 3546783417U,
			// Token: 0x04001A7F RID: 6783
			HttpConnectivityEscalationSubject = 1293802800U,
			// Token: 0x04001A80 RID: 6784
			DatabaseObjectNotFoundException = 4157781316U,
			// Token: 0x04001A81 RID: 6785
			FSMODCNotProvisionedEscalationMessage = 2661017855U,
			// Token: 0x04001A82 RID: 6786
			AssistantsNotRunningToCompletionMessage = 3444300625U,
			// Token: 0x04001A83 RID: 6787
			DLExpansionEscalationSubject = 1281304358U,
			// Token: 0x04001A84 RID: 6788
			RcaTaskOutlineFailed = 2407301436U,
			// Token: 0x04001A85 RID: 6789
			CASRoutingFailureEscalationBody = 3706272566U,
			// Token: 0x04001A86 RID: 6790
			DnsServiceProbeName = 2189832741U,
			// Token: 0x04001A87 RID: 6791
			SynchronousAuditSearchAvailabilityFailureEscalationSubject = 2148760356U,
			// Token: 0x04001A88 RID: 6792
			CannotRebuildIndexEscalationMessage = 4134231892U,
			// Token: 0x04001A89 RID: 6793
			UMPipelineFullEscalationMessageString = 3667272656U,
			// Token: 0x04001A8A RID: 6794
			DatabaseNotAttachedReadOnly = 1880452434U,
			// Token: 0x04001A8B RID: 6795
			InfrastructureValidationMessage = 2815102519U,
			// Token: 0x04001A8C RID: 6796
			OwaTooManyHttpErrorResponsesEncounteredSubject = 977800858U,
			// Token: 0x04001A8D RID: 6797
			CPUOverThresholdWarningEscalationSubject = 3387090034U,
			// Token: 0x04001A8E RID: 6798
			SubscriptionSlaMissedEscalationMessage = 35045070U,
			// Token: 0x04001A8F RID: 6799
			ActiveDirectoryConnectivityConfigDCEscalationMessage = 1680117983U,
			// Token: 0x04001A90 RID: 6800
			OwaMailboxRoleNotInstalled = 3785224032U,
			// Token: 0x04001A91 RID: 6801
			PushNotificationEnterpriseNetworkingError = 421810782U,
			// Token: 0x04001A92 RID: 6802
			DatabaseAvailabilityTimeout = 1548802155U,
			// Token: 0x04001A93 RID: 6803
			DatabaseGuidNotFound = 843177689U,
			// Token: 0x04001A94 RID: 6804
			SearchIndexBacklogAggregatedEscalationMessage = 678296020U,
			// Token: 0x04001A95 RID: 6805
			PublicFolderLocalEWSLogonEscalationMessage = 2793575470U,
			// Token: 0x04001A96 RID: 6806
			TenantRelocationErrorsFoundExceptionMessage = 3926584359U,
			// Token: 0x04001A97 RID: 6807
			UMCallRouterCertificateNearExpiryEscalationMessage = 3506328701U,
			// Token: 0x04001A98 RID: 6808
			SlowADWritesEscalationMessage = 136972698U,
			// Token: 0x04001A99 RID: 6809
			RcaEscalationBodyEnt = 2850892442U,
			// Token: 0x04001A9A RID: 6810
			MailboxDatabasesUnavailable = 2395533582U,
			// Token: 0x04001A9B RID: 6811
			RetryRemoteDeliveryQueueLengthEscalationMessage = 515794671U,
			// Token: 0x04001A9C RID: 6812
			FailedToUpgradeIndexEscalationMessage = 3544714230U,
			// Token: 0x04001A9D RID: 6813
			EventAssistantsWatermarksHelpString = 18446274U,
			// Token: 0x04001A9E RID: 6814
			InferenceClassifcationSLAEscalationMessage = 1165995200U,
			// Token: 0x04001A9F RID: 6815
			MRSUnhealthyMessage = 4059291937U,
			// Token: 0x04001AA0 RID: 6816
			UMServiceType = 3867394701U,
			// Token: 0x04001AA1 RID: 6817
			DivergenceBetweenCAAndAD1003EscalationMessage = 365492232U,
			// Token: 0x04001AA2 RID: 6818
			AssistantsActiveDatabaseMessage = 182386565U,
			// Token: 0x04001AA3 RID: 6819
			SchedulingLatencyEscalateResponderSubject = 1990015256U,
			// Token: 0x04001AA4 RID: 6820
			OfficeGraphTransportDeliveryAgentFailureEscalationMessage = 3218355606U,
			// Token: 0x04001AA5 RID: 6821
			OfficeGraphMessageTracingPluginFailureEscalationMessage = 1104137584U,
			// Token: 0x04001AA6 RID: 6822
			LogicalDiskFreeMegabytesEscalationMessage = 424604115U,
			// Token: 0x04001AA7 RID: 6823
			OwaIMLogAnalyzerSubject = 1920697843U,
			// Token: 0x04001AA8 RID: 6824
			RaidDegradedEscalationMessage = 3515405004U,
			// Token: 0x04001AA9 RID: 6825
			BulkProvisioningNoProgressEscalationMessage = 1236475651U,
			// Token: 0x04001AAA RID: 6826
			AssistantsOutOfSlaSubject = 1313509642U,
			// Token: 0x04001AAB RID: 6827
			OwaTooManyHttpErrorResponsesEncounteredBody = 1818757842U,
			// Token: 0x04001AAC RID: 6828
			CheckSumEscalationMessage = 2102184447U,
			// Token: 0x04001AAD RID: 6829
			PublicFolderLocalEWSLogonEscalationSubject = 3498323379U,
			// Token: 0x04001AAE RID: 6830
			UMServerAddress = 776931395U,
			// Token: 0x04001AAF RID: 6831
			CafeArrayNameCouldNotBeRetrieved = 3415559268U,
			// Token: 0x04001AB0 RID: 6832
			EDSServiceNotRunningEscalationMessage = 3465381739U,
			// Token: 0x04001AB1 RID: 6833
			SearchFailToCheckNodeState = 2618809902U,
			// Token: 0x04001AB2 RID: 6834
			OwaTooManyStartPageFailuresBody = 2406788764U,
			// Token: 0x04001AB3 RID: 6835
			QuarantineEscalationSubject = 1518847271U,
			// Token: 0x04001AB4 RID: 6836
			HostControllerServiceRunningMessage = 1941315341U,
			// Token: 0x04001AB5 RID: 6837
			SearchGracefulDegradationManagerFailureEscalationMessage = 3578257008U,
			// Token: 0x04001AB6 RID: 6838
			ProvisioningBigVolumeErrorMonitorName = 46842886U,
			// Token: 0x04001AB7 RID: 6839
			OwaTooManyWebAppStartsSubject = 3672282944U,
			// Token: 0x04001AB8 RID: 6840
			SearchQueryStxSimpleQueryMode = 1495785996U,
			// Token: 0x04001AB9 RID: 6841
			OABGenTenantOutOfSLASubject = 3956529557U,
			// Token: 0x04001ABA RID: 6842
			ELCDumpsterEscalationMessage = 951276022U,
			// Token: 0x04001ABB RID: 6843
			DirectoryConfigDiscrepancyEscalationMessage = 1559006762U,
			// Token: 0x04001ABC RID: 6844
			NetworkAdapterRecoveryResponderName = 1496058059U,
			// Token: 0x04001ABD RID: 6845
			SearchGracefulDegradationStatusEscalationMessage = 898751369U,
			// Token: 0x04001ABE RID: 6846
			DnsServiceRestartResponderName = 3135153224U,
			// Token: 0x04001ABF RID: 6847
			ELCMailboxSLAEscalationMessage = 3845606054U,
			// Token: 0x04001AC0 RID: 6848
			JournalingEscalationMessage = 1456195959U,
			// Token: 0x04001AC1 RID: 6849
			MaintenanceTimeoutEscalationSubject = 1598120667U,
			// Token: 0x04001AC2 RID: 6850
			ContentsUnpredictableEscalationMessage = 3433261884U,
			// Token: 0x04001AC3 RID: 6851
			EscalationSubjectUnhealthy = 3788119207U,
			// Token: 0x04001AC4 RID: 6852
			AsyncAuditLogSearchEscalationSubject = 859758172U,
			// Token: 0x04001AC5 RID: 6853
			DefaultEscalationMessage = 706708545U,
			// Token: 0x04001AC6 RID: 6854
			SyntheticReplicationMonitorEscalationMessage = 2480657645U,
			// Token: 0x04001AC7 RID: 6855
			EDiscoveryEscalationBodyDCHTML = 31173904U,
			// Token: 0x04001AC8 RID: 6856
			OwaTooManyLogoffFailuresSubject = 1499273528U,
			// Token: 0x04001AC9 RID: 6857
			CheckProvisionedDCExceptionMessage = 4220476005U,
			// Token: 0x04001ACA RID: 6858
			ProvisioningBigVolumeErrorEscalationSubject = 3913310860U,
			// Token: 0x04001ACB RID: 6859
			UMCertificateNearExpiryEscalationMessage = 3906074550U,
			// Token: 0x04001ACC RID: 6860
			FailureItemMessageForNTFSCorruption = 182285359U,
			// Token: 0x04001ACD RID: 6861
			DatabaseRPCLatencyMonitorGreenMessage = 821565004U,
			// Token: 0x04001ACE RID: 6862
			RelocationServicePermanentExceptionMessage = 1705255289U,
			// Token: 0x04001ACF RID: 6863
			LiveIdAuthenticationEscalationMesage = 3730111670U,
			// Token: 0x04001AD0 RID: 6864
			JournalArchiveEscalationMessage = 3256369209U,
			// Token: 0x04001AD1 RID: 6865
			Pop3ProtocolUnhealthy = 1746979174U,
			// Token: 0x04001AD2 RID: 6866
			HxServiceEscalationMessageUnhealthy = 408002753U,
			// Token: 0x04001AD3 RID: 6867
			RequestForNewRidPoolFailedEscalationMessage = 2258779484U,
			// Token: 0x04001AD4 RID: 6868
			DatabaseSizeEscalationSubject = 2903867061U,
			// Token: 0x04001AD5 RID: 6869
			OabMailboxManifestEmpty = 4196623762U,
			// Token: 0x04001AD6 RID: 6870
			CheckFsmoRolesScriptExceptionMessage = 1260554753U,
			// Token: 0x04001AD7 RID: 6871
			PopImapSecondaryEndpoint = 1084277863U,
			// Token: 0x04001AD8 RID: 6872
			CannotFunctionNormallyEscalationMessage = 3639485535U,
			// Token: 0x04001AD9 RID: 6873
			EscalationMessageHealthy = 116423715U,
			// Token: 0x04001ADA RID: 6874
			PublicFolderMoveJobStuckEscalationMessage = 2944547573U,
			// Token: 0x04001ADB RID: 6875
			SearchServiceNotRunningEscalationMessage = 667399815U,
			// Token: 0x04001ADC RID: 6876
			MobilityAccountPassword = 1455075163U,
			// Token: 0x04001ADD RID: 6877
			EventLogProbeProviderName = 3746052928U,
			// Token: 0x04001ADE RID: 6878
			VersionStore623EscalationMessage = 1122579724U
		}

		// Token: 0x02000597 RID: 1431
		private enum ParamIDs
		{
			// Token: 0x04001AE0 RID: 6880
			QuarantinedMailboxEscalationMessageEnt,
			// Token: 0x04001AE1 RID: 6881
			PopSelfTestEscalationBodyDC,
			// Token: 0x04001AE2 RID: 6882
			OneCopyMonitorFailureEscalationSubject,
			// Token: 0x04001AE3 RID: 6883
			CircularLoggingDisabledEscalationMessage,
			// Token: 0x04001AE4 RID: 6884
			MailSubmissionBehindWatermarksEscalationMessageEnt,
			// Token: 0x04001AE5 RID: 6885
			InsufficientRedundancyEscalationSubject,
			// Token: 0x04001AE6 RID: 6886
			LagCopyHealthProblemEscalationSubject,
			// Token: 0x04001AE7 RID: 6887
			ActiveDatabaseAvailabilityEscalationMessageEnt,
			// Token: 0x04001AE8 RID: 6888
			StoreAdminRPCInterfaceEscalationEscalationMessageEnt,
			// Token: 0x04001AE9 RID: 6889
			InfrastructureValidationError,
			// Token: 0x04001AEA RID: 6890
			AvailabilityServiceEscalationHtmlBody,
			// Token: 0x04001AEB RID: 6891
			RwsDatamartConnectionEscalationBody,
			// Token: 0x04001AEC RID: 6892
			ServiceNotRunningEscalationMessage,
			// Token: 0x04001AED RID: 6893
			DatabaseLocationNotFoundException,
			// Token: 0x04001AEE RID: 6894
			OwaCustomerTouchPointEscalationHtmlBody,
			// Token: 0x04001AEF RID: 6895
			EwsAutodSelfTestEscalationRecoveryDetails,
			// Token: 0x04001AF0 RID: 6896
			MRSServiceNotRunningSubject,
			// Token: 0x04001AF1 RID: 6897
			SearchQueryStxSuccess,
			// Token: 0x04001AF2 RID: 6898
			QuarantinedMailboxEscalationMessageDc,
			// Token: 0x04001AF3 RID: 6899
			ExchangeCrashExceededErrorThresholdMessage,
			// Token: 0x04001AF4 RID: 6900
			DatabaseRepeatedMountsEscalationMessage,
			// Token: 0x04001AF5 RID: 6901
			OnlineMeetingCreateEscalationSubject,
			// Token: 0x04001AF6 RID: 6902
			LocalMachineDriveEncryptionLockEscalationMessage,
			// Token: 0x04001AF7 RID: 6903
			DbFailureItemIoHardEscalationSubject,
			// Token: 0x04001AF8 RID: 6904
			LogVolumeSpaceEscalationMessage,
			// Token: 0x04001AF9 RID: 6905
			DatabaseSchemaVersionCheckEscalationMessageDc,
			// Token: 0x04001AFA RID: 6906
			ProcessCrashing,
			// Token: 0x04001AFB RID: 6907
			CafeEscalationRecoveryDetails,
			// Token: 0x04001AFC RID: 6908
			StoreProcessRepeatedlyCrashingEscalationMessageEnt,
			// Token: 0x04001AFD RID: 6909
			PrivateWorkingSetExceededErrorThresholdMessage,
			// Token: 0x04001AFE RID: 6910
			ProcessorTimeExceededWarningThresholdMessage,
			// Token: 0x04001AFF RID: 6911
			DatabaseAvailabilityFailure,
			// Token: 0x04001B00 RID: 6912
			EDiscoveryscalationSubject,
			// Token: 0x04001B01 RID: 6913
			ParseDiagnosticsStringError,
			// Token: 0x04001B02 RID: 6914
			SystemDriveSpaceEscalationSubject,
			// Token: 0x04001B03 RID: 6915
			DatabaseConsistencyEscalationMessage,
			// Token: 0x04001B04 RID: 6916
			GenericOverallXFailureEscalationMessage,
			// Token: 0x04001B05 RID: 6917
			ImapProxyTestEscalationBodyDC,
			// Token: 0x04001B06 RID: 6918
			PopSelfTestEscalationBodyENT,
			// Token: 0x04001B07 RID: 6919
			TransportSyncManagerServiceNotRunningEscalationMessage,
			// Token: 0x04001B08 RID: 6920
			EscalationMessageUnhealthy,
			// Token: 0x04001B09 RID: 6921
			EacDeepTestEscalationSubject,
			// Token: 0x04001B0A RID: 6922
			InvokeNowDefinitionFailure,
			// Token: 0x04001B0B RID: 6923
			ImapCustomerTouchPointEscalationBodyENT,
			// Token: 0x04001B0C RID: 6924
			PushNotificationChannelError,
			// Token: 0x04001B0D RID: 6925
			DBAvailableButUnloadedByTransportSyncManagerMessage,
			// Token: 0x04001B0E RID: 6926
			MonitoringAccountUnavailable,
			// Token: 0x04001B0F RID: 6927
			LocalDriveLogSpaceEscalationMessageDc,
			// Token: 0x04001B10 RID: 6928
			DatabaseRPCLatencyEscalationSubject,
			// Token: 0x04001B11 RID: 6929
			LocalDriveLogSpaceEscalationMessageEnt,
			// Token: 0x04001B12 RID: 6930
			SearchQueryFailure,
			// Token: 0x04001B13 RID: 6931
			EndpointManagerEndpointUninitialized,
			// Token: 0x04001B14 RID: 6932
			MigrationNotificationMessage,
			// Token: 0x04001B15 RID: 6933
			HostControllerServiceNodeExcessivePrivateBytes,
			// Token: 0x04001B16 RID: 6934
			HostControllerServiceNodeExcessivePrivateBytesDetails,
			// Token: 0x04001B17 RID: 6935
			SearchIndexServerCopyStatus,
			// Token: 0x04001B18 RID: 6936
			SearchActiveCopyUnhealthyEscalationMessage,
			// Token: 0x04001B19 RID: 6937
			ProcessRepeatedlyCrashingEscalationSubject,
			// Token: 0x04001B1A RID: 6938
			UMCallRouterRecentCallRejectedMessageString,
			// Token: 0x04001B1B RID: 6939
			OwaCustomerTouchPointEscalationBody,
			// Token: 0x04001B1C RID: 6940
			SearchIndexCrawlingNoProgress,
			// Token: 0x04001B1D RID: 6941
			ActiveSyncDeepTestEscalationBodyDC,
			// Token: 0x04001B1E RID: 6942
			ComponentHealthPercentFailureEscalationMessageUnhealthy,
			// Token: 0x04001B1F RID: 6943
			RwsDatamartAvailabilityEscalationSubject,
			// Token: 0x04001B20 RID: 6944
			CafeServerNotOwner,
			// Token: 0x04001B21 RID: 6945
			CafeOfflineFailedEscalationRecoveryDetails,
			// Token: 0x04001B22 RID: 6946
			SearchIndexCopyUnhealthy,
			// Token: 0x04001B23 RID: 6947
			SearchInstantSearchStxZeroHitMonitoringMailbox,
			// Token: 0x04001B24 RID: 6948
			ImapSelfTestEscalationBodyENT,
			// Token: 0x04001B25 RID: 6949
			StalledCopyEscalationSubject,
			// Token: 0x04001B26 RID: 6950
			InsufficientRedundancyEscalationMessage,
			// Token: 0x04001B27 RID: 6951
			DatabaseLogicalPhysicalSizeRatioEscalationMessageDc,
			// Token: 0x04001B28 RID: 6952
			LocalMachineDriveBootVolumeEncryptionStateEscalationMessage,
			// Token: 0x04001B29 RID: 6953
			LocalMachineDriveNotProtectedWithDraEscalationMessage,
			// Token: 0x04001B2A RID: 6954
			ReplServiceCrashEscalationMessage,
			// Token: 0x04001B2B RID: 6955
			PassiveDatabaseAvailabilityEscalationMessageEnt,
			// Token: 0x04001B2C RID: 6956
			CircularLoggingDisabledEscalationSubject,
			// Token: 0x04001B2D RID: 6957
			AttributeMissingFromProbeDefinition,
			// Token: 0x04001B2E RID: 6958
			UnableToGetDatabaseState,
			// Token: 0x04001B2F RID: 6959
			ProcessorTimeExceededErrorThresholdWithAffinitizationMessage,
			// Token: 0x04001B30 RID: 6960
			DatabaseCopyBehindEscalationSubject,
			// Token: 0x04001B31 RID: 6961
			SearchIndexCopyStatusError,
			// Token: 0x04001B32 RID: 6962
			DatabaseDiskReadLatencyEscalationMessageEnt,
			// Token: 0x04001B33 RID: 6963
			UnableToGetDatabaseSize,
			// Token: 0x04001B34 RID: 6964
			LocalMachineDriveNotProtectedWithDraEscalationSubject,
			// Token: 0x04001B35 RID: 6965
			StoreProcessRepeatedlyCrashingEscalationMessageDc,
			// Token: 0x04001B36 RID: 6966
			SearchCatalogNotLoaded,
			// Token: 0x04001B37 RID: 6967
			ActiveSyncDeepTestEscalationBodyENT,
			// Token: 0x04001B38 RID: 6968
			DatabasePercentRPCRequestsEscalationMessageEnt,
			// Token: 0x04001B39 RID: 6969
			DatabaseRPCLatencyEscalationMessageEnt,
			// Token: 0x04001B3A RID: 6970
			ForwardSyncStandardCompanyEscalationMessage,
			// Token: 0x04001B3B RID: 6971
			PutDCIntoMMFailureEscalateMessage,
			// Token: 0x04001B3C RID: 6972
			HostControllerServiceNodeOperationFailed,
			// Token: 0x04001B3D RID: 6973
			ObserverHeartbeatEscalateResponderSubject,
			// Token: 0x04001B3E RID: 6974
			AntimalwareEngineErrorsEscalationMessage,
			// Token: 0x04001B3F RID: 6975
			AuditLogSearchServiceletEscalationMessage,
			// Token: 0x04001B40 RID: 6976
			OwaMailboxDatabaseDoesntExist,
			// Token: 0x04001B41 RID: 6977
			OwaTooManyWebAppStartsBody,
			// Token: 0x04001B42 RID: 6978
			EscalationMessageFailuresUnhealthy,
			// Token: 0x04001B43 RID: 6979
			SearchQueryStxZeroHitMonitoringMailbox,
			// Token: 0x04001B44 RID: 6980
			ClusterServiceCrashEscalationMessage,
			// Token: 0x04001B45 RID: 6981
			OWACalendarAppPoolEscalationSubject,
			// Token: 0x04001B46 RID: 6982
			UMSipOptionsToUMServiceFailedEscalationSubject,
			// Token: 0x04001B47 RID: 6983
			PutDCIntoMMSuccessNotificationMessage,
			// Token: 0x04001B48 RID: 6984
			EseDbDivergenceDetectedEscalationMessage,
			// Token: 0x04001B49 RID: 6985
			ClusterServiceCrashEscalationSubject,
			// Token: 0x04001B4A RID: 6986
			LocalDriveLogSpaceEscalationSubject,
			// Token: 0x04001B4B RID: 6987
			ImapDeepTestEscalationBodyDC,
			// Token: 0x04001B4C RID: 6988
			ObserverHeartbeatEscalateResponderMessage,
			// Token: 0x04001B4D RID: 6989
			ReplServiceDownEscalationMessage,
			// Token: 0x04001B4E RID: 6990
			RemoteStoreAdminRPCInterfaceEscalationEscalationMessageEnt,
			// Token: 0x04001B4F RID: 6991
			SearchNumberOfParserServersDegradation,
			// Token: 0x04001B50 RID: 6992
			UnMonitoredDatabaseEscalationSubject,
			// Token: 0x04001B51 RID: 6993
			NumberOfActiveBackgroundTasksEscalationMessageEnt,
			// Token: 0x04001B52 RID: 6994
			OwaIMSigninFailedMessage,
			// Token: 0x04001B53 RID: 6995
			HostControllerNodeRestartDetails,
			// Token: 0x04001B54 RID: 6996
			AssistantsOutOfSlaError,
			// Token: 0x04001B55 RID: 6997
			SearchIndexStall,
			// Token: 0x04001B56 RID: 6998
			UMTranscriptionThrottledEscalationMessage,
			// Token: 0x04001B57 RID: 6999
			SiteFailureEscalationSubject,
			// Token: 0x04001B58 RID: 7000
			PopProxyTestEscalationBodyENT,
			// Token: 0x04001B59 RID: 7001
			PushNotificationDatacenterBackendEndpointUnhealthy,
			// Token: 0x04001B5A RID: 7002
			ComponentHealthErrorHeader,
			// Token: 0x04001B5B RID: 7003
			ImapEscalationSubject,
			// Token: 0x04001B5C RID: 7004
			PushNotificationPublisherUnhealthy,
			// Token: 0x04001B5D RID: 7005
			EacSelfTestEscalationBody,
			// Token: 0x04001B5E RID: 7006
			MonitoringAccountImproper,
			// Token: 0x04001B5F RID: 7007
			SearchQueryFailureEscalationMessage,
			// Token: 0x04001B60 RID: 7008
			PushNotificationCafeUnexpectedResponse,
			// Token: 0x04001B61 RID: 7009
			ForwardSyncLiteCompanyEscalationMessage,
			// Token: 0x04001B62 RID: 7010
			LastDBDiscoveryTimeFailedMessage,
			// Token: 0x04001B63 RID: 7011
			StoreAdminRPCInterfaceEscalationSubject,
			// Token: 0x04001B64 RID: 7012
			ProcessRepeatedlyCrashingEscalationMessage,
			// Token: 0x04001B65 RID: 7013
			UMServiceRecentCallRejectedEscalationMessageString,
			// Token: 0x04001B66 RID: 7014
			InvokeNowAssemblyInfoFailure,
			// Token: 0x04001B67 RID: 7015
			UnableToGetDatabaseSchemaVersion,
			// Token: 0x04001B68 RID: 7016
			InferenceDisabledComponentDetails,
			// Token: 0x04001B69 RID: 7017
			LocalMachineDriveEncryptionSuspendEscalationSubject,
			// Token: 0x04001B6A RID: 7018
			QuarantinedMailboxEscalationSubject,
			// Token: 0x04001B6B RID: 7019
			SearchLocalCopyStatusEscalationMessage,
			// Token: 0x04001B6C RID: 7020
			ProcessCrashDetectionEscalationMessage,
			// Token: 0x04001B6D RID: 7021
			LogVolumeSpaceEscalationSubject,
			// Token: 0x04001B6E RID: 7022
			ClusterGroupDownEscalationSubject,
			// Token: 0x04001B6F RID: 7023
			LocalMachineDriveBootVolumeEncryptionStateEscalationSubject,
			// Token: 0x04001B70 RID: 7024
			DatabaseCopySlowReplayEscalationSubject,
			// Token: 0x04001B71 RID: 7025
			SearchIndexBacklogWithHistory,
			// Token: 0x04001B72 RID: 7026
			EacCtpTestEscalationBody,
			// Token: 0x04001B73 RID: 7027
			OabMailboxEscalationSubject,
			// Token: 0x04001B74 RID: 7028
			ServerInMaintenanceModeForTooLongEscalationSubject,
			// Token: 0x04001B75 RID: 7029
			ServiceNotRunningEscalationMessageEnt,
			// Token: 0x04001B76 RID: 7030
			DatabaseCopySlowReplayEscalationMessage,
			// Token: 0x04001B77 RID: 7031
			UMGrammarUsageEscalationMessage,
			// Token: 0x04001B78 RID: 7032
			FireWallEscalationMessage,
			// Token: 0x04001B79 RID: 7033
			HAPassiveCopyUnhealthy,
			// Token: 0x04001B7A RID: 7034
			UMSipOptionsToUMServiceFailedEscalationBody,
			// Token: 0x04001B7B RID: 7035
			MailSubmissionBehindWatermarksEscalationSubject,
			// Token: 0x04001B7C RID: 7036
			ComponentHealthErrorContent,
			// Token: 0x04001B7D RID: 7037
			PrivateWorkingSetExceededWarningThresholdMessage,
			// Token: 0x04001B7E RID: 7038
			ActiveSyncSelfTestEscalationBodyDC,
			// Token: 0x04001B7F RID: 7039
			NTFSCorruptionEscalationMessage,
			// Token: 0x04001B80 RID: 7040
			FailedAndSuspendedCopyEscalationMessage,
			// Token: 0x04001B81 RID: 7041
			OneCopyMonitorFailureMessage,
			// Token: 0x04001B82 RID: 7042
			EventAssistantsProcessRepeatedlyCrashingEscalationMessageEnt,
			// Token: 0x04001B83 RID: 7043
			PutMultipleDCIntoMMFailureEscalateMessage,
			// Token: 0x04001B84 RID: 7044
			SystemDriveSpaceEscalationMessage,
			// Token: 0x04001B85 RID: 7045
			DatabasePercentRPCRequestsEscalationSubject,
			// Token: 0x04001B86 RID: 7046
			SearchQuerySlow,
			// Token: 0x04001B87 RID: 7047
			SearchIndexSuspendedEscalationMessage,
			// Token: 0x04001B88 RID: 7048
			InvokeNowInvalidWorkDefinition,
			// Token: 0x04001B89 RID: 7049
			OabTooManyWebAppStartsBody,
			// Token: 0x04001B8A RID: 7050
			HostControllerServiceNodeUnhealthy,
			// Token: 0x04001B8B RID: 7051
			HostControllerExcessiveNodeRestarts,
			// Token: 0x04001B8C RID: 7052
			OwaDeepTestEscalationHtmlBody,
			// Token: 0x04001B8D RID: 7053
			SuspendedCopyEscalationMessage,
			// Token: 0x04001B8E RID: 7054
			ImapProxyTestEscalationBodyENT,
			// Token: 0x04001B8F RID: 7055
			SearchResourceLoadEscalationMessage,
			// Token: 0x04001B90 RID: 7056
			InvalidAccessToken,
			// Token: 0x04001B91 RID: 7057
			LocalMachineDriveProtectedWithDraWithoutDecryptorEscalationMessage,
			// Token: 0x04001B92 RID: 7058
			MigrationNotificationSubject,
			// Token: 0x04001B93 RID: 7059
			PublicFolderSyncEscalationMessage,
			// Token: 0x04001B94 RID: 7060
			RcaWorkItemCreationSummaryEntry,
			// Token: 0x04001B95 RID: 7061
			WacDiscoveryFailureBody,
			// Token: 0x04001B96 RID: 7062
			HighLogGenerationRateEscalationSubject,
			// Token: 0x04001B97 RID: 7063
			DatabaseLogicalPhysicalSizeRatioEscalationMessageEnt,
			// Token: 0x04001B98 RID: 7064
			OwaSelfTestEscalationBody,
			// Token: 0x04001B99 RID: 7065
			MailboxAssistantsBehindWatermarksEscalationSubject,
			// Token: 0x04001B9A RID: 7066
			ControllerFailureMessage,
			// Token: 0x04001B9B RID: 7067
			HighLogGenerationRateEscalationMessage,
			// Token: 0x04001B9C RID: 7068
			SearchFeedingControllerFailureEscalationMessage,
			// Token: 0x04001B9D RID: 7069
			ClusterNetworkDownEscalationSubject,
			// Token: 0x04001B9E RID: 7070
			FireWallEscalationSubject,
			// Token: 0x04001B9F RID: 7071
			ServerVersionNotFound,
			// Token: 0x04001BA0 RID: 7072
			EacDeepTestEscalationBody,
			// Token: 0x04001BA1 RID: 7073
			VersionBucketsAllocatedEscalationEscalationMessageDc,
			// Token: 0x04001BA2 RID: 7074
			SearchIndexCrawlingWithHealthyCopy,
			// Token: 0x04001BA3 RID: 7075
			ProcessorTimeExceededErrorThresholdSubject,
			// Token: 0x04001BA4 RID: 7076
			MailboxAssistantsBehindWatermarksEscalationMessageEnt,
			// Token: 0x04001BA5 RID: 7077
			PassiveDatabaseAvailabilityEscalationMessageDc,
			// Token: 0x04001BA6 RID: 7078
			SuspendedCopyEscalationSubject,
			// Token: 0x04001BA7 RID: 7079
			SearchCatalogSuspended,
			// Token: 0x04001BA8 RID: 7080
			UMRecentPartnerTranscriptionFailedEscalationMessageString,
			// Token: 0x04001BA9 RID: 7081
			EseDbTimeAdvanceEscalationMessage,
			// Token: 0x04001BAA RID: 7082
			ControllerFailureEscalationSubject,
			// Token: 0x04001BAB RID: 7083
			SearchWordBreakerLoadingFailure,
			// Token: 0x04001BAC RID: 7084
			SearchIndexMultiCrawling,
			// Token: 0x04001BAD RID: 7085
			AvailabilityServiceEscalationBody,
			// Token: 0x04001BAE RID: 7086
			OwaSelfTestEscalationHtmlBody,
			// Token: 0x04001BAF RID: 7087
			ProcessorTimeExceededWarningThresholdSubject,
			// Token: 0x04001BB0 RID: 7088
			SearchIndexSingleHealthyCopyWithSeeding,
			// Token: 0x04001BB1 RID: 7089
			DatabasePercentRPCRequestsEscalationMessageDc,
			// Token: 0x04001BB2 RID: 7090
			DatabaseCopyBehindEscalationMessage,
			// Token: 0x04001BB3 RID: 7091
			EseInconsistentDataDetectedEscalationSubject,
			// Token: 0x04001BB4 RID: 7092
			DatabaseValidationNullRef,
			// Token: 0x04001BB5 RID: 7093
			CafeThreadCountMessageUnhealthy,
			// Token: 0x04001BB6 RID: 7094
			PotentialInsufficientRedundancyEscalationSubject,
			// Token: 0x04001BB7 RID: 7095
			ClusterNetworkReportErrorEscalationMessage,
			// Token: 0x04001BB8 RID: 7096
			DbFailureItemIoHardEscalationMessage,
			// Token: 0x04001BB9 RID: 7097
			MonitoringAccountDomainUnavailable,
			// Token: 0x04001BBA RID: 7098
			ClusterNodeEvictedEscalationSubject,
			// Token: 0x04001BBB RID: 7099
			LocalMachineDriveEncryptionSuspendEscalationMessage,
			// Token: 0x04001BBC RID: 7100
			ActiveManagerUnhealthyEscalationSubject,
			// Token: 0x04001BBD RID: 7101
			SearchIndexSeedingNoProgres,
			// Token: 0x04001BBE RID: 7102
			LagCopyHealthProblemEscalationMessage,
			// Token: 0x04001BBF RID: 7103
			FailedAndSuspendedCopyEscalationSubject,
			// Token: 0x04001BC0 RID: 7104
			RemoteStoreAdminRPCInterfaceEscalationSubject,
			// Token: 0x04001BC1 RID: 7105
			InvokeNowPickupEventNotFound,
			// Token: 0x04001BC2 RID: 7106
			ActiveSyncSelfTestEscalationBodyENT,
			// Token: 0x04001BC3 RID: 7107
			PrivateWorkingSetExceededErrorThresholdSubject,
			// Token: 0x04001BC4 RID: 7108
			AvailabilityServiceEscalationSubjectUnhealthy,
			// Token: 0x04001BC5 RID: 7109
			ActiveSyncEscalationSubject,
			// Token: 0x04001BC6 RID: 7110
			StoreMaintenanceAssistantEscalationMessageEnt,
			// Token: 0x04001BC7 RID: 7111
			SearchTransportAgentFailure,
			// Token: 0x04001BC8 RID: 7112
			AssistantsNotRunningError,
			// Token: 0x04001BC9 RID: 7113
			ImapCustomerTouchPointEscalationBodyDC,
			// Token: 0x04001BCA RID: 7114
			RpsFailedEscalationMessage,
			// Token: 0x04001BCB RID: 7115
			ForwardSyncProcessRepeatedlyCrashingEscalationMessage,
			// Token: 0x04001BCC RID: 7116
			StoreAdminRPCInterfaceNotResponding,
			// Token: 0x04001BCD RID: 7117
			OwaCustomerTouchPointEscalationSubject,
			// Token: 0x04001BCE RID: 7118
			OwaIMSigninFailedSubject,
			// Token: 0x04001BCF RID: 7119
			UMPipelineSLAEscalationMessageString,
			// Token: 0x04001BD0 RID: 7120
			ComponentHealthHeartbeatEscalationMessageUnhealthy,
			// Token: 0x04001BD1 RID: 7121
			SearchResourceLoadUnhealthy,
			// Token: 0x04001BD2 RID: 7122
			UnmonitoredDatabaseEscalationMessage,
			// Token: 0x04001BD3 RID: 7123
			LongRunningWerMgrTriggerWarningThresholdSubject,
			// Token: 0x04001BD4 RID: 7124
			EseDbTimeSmallerEscalationMessage,
			// Token: 0x04001BD5 RID: 7125
			SearchProcessCrashingTooManyTimesEscalationMessage,
			// Token: 0x04001BD6 RID: 7126
			GetDiagnosticInfoTimeoutMessage,
			// Token: 0x04001BD7 RID: 7127
			WatermarksBehind,
			// Token: 0x04001BD8 RID: 7128
			ClusterGroupDownEscalationMessage,
			// Token: 0x04001BD9 RID: 7129
			PswsEscalationBody,
			// Token: 0x04001BDA RID: 7130
			EseDbTimeSmallerEscalationSubject,
			// Token: 0x04001BDB RID: 7131
			OabMailboxFileNotFound,
			// Token: 0x04001BDC RID: 7132
			SearchCatalogInFailedAndSuspendedState,
			// Token: 0x04001BDD RID: 7133
			EseSinglePageLogicalCorruptionDetectedSubject,
			// Token: 0x04001BDE RID: 7134
			StoreAdminRPCInterfaceEscalationEscalationMessageDc,
			// Token: 0x04001BDF RID: 7135
			VersionBucketsAllocatedEscalationSubject,
			// Token: 0x04001BE0 RID: 7136
			OwaDeepTestEscalationBody,
			// Token: 0x04001BE1 RID: 7137
			MRSLongQueueScanMessage,
			// Token: 0x04001BE2 RID: 7138
			SearchIndexFailure,
			// Token: 0x04001BE3 RID: 7139
			MRSRPCPingSubject,
			// Token: 0x04001BE4 RID: 7140
			UserThrottlingLockedOutUsersSubject,
			// Token: 0x04001BE5 RID: 7141
			ClusterHangEscalationSubject,
			// Token: 0x04001BE6 RID: 7142
			RcaEscalationSubject,
			// Token: 0x04001BE7 RID: 7143
			OwaDeepTestEscalationSubject,
			// Token: 0x04001BE8 RID: 7144
			ClusterServiceDownEscalationMessage,
			// Token: 0x04001BE9 RID: 7145
			EscalationMessagePercentUnhealthy,
			// Token: 0x04001BEA RID: 7146
			SearchIndexCopyStatus,
			// Token: 0x04001BEB RID: 7147
			JobobjectCpuExceededThresholdSubject,
			// Token: 0x04001BEC RID: 7148
			FindPlacesRequestsError,
			// Token: 0x04001BED RID: 7149
			RemoteStoreAdminRPCInterfaceEscalationEscalationMessageDc,
			// Token: 0x04001BEE RID: 7150
			EseInconsistentDataDetectedEscalationMessage,
			// Token: 0x04001BEF RID: 7151
			DatabaseNotFoundInADException,
			// Token: 0x04001BF0 RID: 7152
			PrivateWorkingSetExceededWarningThresholdSubject,
			// Token: 0x04001BF1 RID: 7153
			MRSRepeatedlyCrashingEscalationSubject,
			// Token: 0x04001BF2 RID: 7154
			ProcessorTimeExceededErrorThresholdMessage,
			// Token: 0x04001BF3 RID: 7155
			MRSLongQueueScanSubject,
			// Token: 0x04001BF4 RID: 7156
			StalledCopyEscalationMessage,
			// Token: 0x04001BF5 RID: 7157
			OabProtocolEscalationSubject,
			// Token: 0x04001BF6 RID: 7158
			ProcessorTimeExceededWarningThresholdWithAffinitizationMessage,
			// Token: 0x04001BF7 RID: 7159
			InvalidSystemMailbox,
			// Token: 0x04001BF8 RID: 7160
			CafeEscalationMessageUnhealthyForDC,
			// Token: 0x04001BF9 RID: 7161
			UMSipOptionsToUMCallRouterServiceFailedEscalationSubject,
			// Token: 0x04001BFA RID: 7162
			PutMultipleDCIntoMMSuccessNotificationMessage,
			// Token: 0x04001BFB RID: 7163
			InferenceTrainingDataCollectionRepeatedCrashEscalationMessage,
			// Token: 0x04001BFC RID: 7164
			SearchInstantSearchStxException,
			// Token: 0x04001BFD RID: 7165
			ActiveSyncCustomerTouchPointEscalationBodyDC,
			// Token: 0x04001BFE RID: 7166
			HealthSetsStates,
			// Token: 0x04001BFF RID: 7167
			LongRunningWerMgrTriggerWarningThresholdMessage,
			// Token: 0x04001C00 RID: 7168
			ReplServiceDownEscalationSubject,
			// Token: 0x04001C01 RID: 7169
			SearchIndexBacklogWithProcessingRateAndHistory,
			// Token: 0x04001C02 RID: 7170
			MultipleRecipientsFound,
			// Token: 0x04001C03 RID: 7171
			CafeThreadCountSubjectUnhealthy,
			// Token: 0x04001C04 RID: 7172
			PotentialInsufficientRedundancyEscalationMessage,
			// Token: 0x04001C05 RID: 7173
			InvalidUserName,
			// Token: 0x04001C06 RID: 7174
			SearchInstantSearchStxEscalationMessage,
			// Token: 0x04001C07 RID: 7175
			MailboxAssistantsBehindWatermarksEscalationMessageDc,
			// Token: 0x04001C08 RID: 7176
			ExchangeCrashExceededErrorThresholdSubject,
			// Token: 0x04001C09 RID: 7177
			RcaWorkItemDescriptionEntry,
			// Token: 0x04001C0A RID: 7178
			SearchIndexActiveCopyUnhealthy,
			// Token: 0x04001C0B RID: 7179
			UMCallRouterRecentMissedCallNotificationProxyFailedEscalationMessageString,
			// Token: 0x04001C0C RID: 7180
			SearchNumDiskPartsEscalationMessage,
			// Token: 0x04001C0D RID: 7181
			InferenceClassificationRepeatedCrashEscalationMessage,
			// Token: 0x04001C0E RID: 7182
			AssistantsActiveDatabaseError,
			// Token: 0x04001C0F RID: 7183
			DatabaseDiskReadLatencyEscalationSubject,
			// Token: 0x04001C10 RID: 7184
			NumberOfActiveBackgroundTasksEscalationMessageDc,
			// Token: 0x04001C11 RID: 7185
			UserthtottlingLockedOutUsersMessage,
			// Token: 0x04001C12 RID: 7186
			SearchCatalogHasError,
			// Token: 0x04001C13 RID: 7187
			NTFSCorruptionEscalationSubject,
			// Token: 0x04001C14 RID: 7188
			ActiveManagerUnhealthyEscalationMessage,
			// Token: 0x04001C15 RID: 7189
			HighDiskLatencyEscalationSubject,
			// Token: 0x04001C16 RID: 7190
			TooManyDatabaseMountedEscalationSubject,
			// Token: 0x04001C17 RID: 7191
			PasswordVerificationFailed,
			// Token: 0x04001C18 RID: 7192
			LocalMachineDriveEncryptionStateEscalationMessage,
			// Token: 0x04001C19 RID: 7193
			UMWorkerProcessRecentCallRejectedEscalationMessageString,
			// Token: 0x04001C1A RID: 7194
			SingleAvailableDatabaseCopyEscalationSubject,
			// Token: 0x04001C1B RID: 7195
			PopProxyTestEscalationBodyDC,
			// Token: 0x04001C1C RID: 7196
			StoreNotificationEscalationMessage,
			// Token: 0x04001C1D RID: 7197
			CafeEscalationMessageUnhealthy,
			// Token: 0x04001C1E RID: 7198
			PopCustomerTouchPointEscalationBodyDC,
			// Token: 0x04001C1F RID: 7199
			PopEscalationSubject,
			// Token: 0x04001C20 RID: 7200
			InferenceComponentDisabled,
			// Token: 0x04001C21 RID: 7201
			SearchQueryStxEscalationMessage,
			// Token: 0x04001C22 RID: 7202
			DatabaseSizeEscalationMessageEnt,
			// Token: 0x04001C23 RID: 7203
			ActiveSyncCustomerTouchPointEscalationBodyENT,
			// Token: 0x04001C24 RID: 7204
			HealthSetMonitorsStates,
			// Token: 0x04001C25 RID: 7205
			SearchGetDiagnosticInfoTimeout,
			// Token: 0x04001C26 RID: 7206
			InvalidMailboxDatabaseEndpoint,
			// Token: 0x04001C27 RID: 7207
			PopDeepTestEscalationBodyDC,
			// Token: 0x04001C28 RID: 7208
			LocalMachineDriveEncryptionLockEscalationSubject,
			// Token: 0x04001C29 RID: 7209
			SearchGracefulDegradationStatus,
			// Token: 0x04001C2A RID: 7210
			EseDbDivergenceDetectedSubject,
			// Token: 0x04001C2B RID: 7211
			SearchIndexActiveCopySeedingWithHealthyCopy,
			// Token: 0x04001C2C RID: 7212
			NumberOfActiveBackgroundTasksEscalationSubject,
			// Token: 0x04001C2D RID: 7213
			StoreMaintenanceAssistantEscalationSubject,
			// Token: 0x04001C2E RID: 7214
			StoreNotificationEscalationSubject,
			// Token: 0x04001C2F RID: 7215
			RwsDatamartAvailabilityEscalationBody,
			// Token: 0x04001C30 RID: 7216
			ComponentHealthPercentFailureEscalationMessageHealthy,
			// Token: 0x04001C31 RID: 7217
			ActiveDatabaseAvailabilityEscalationMessageDc,
			// Token: 0x04001C32 RID: 7218
			SearchIndexDatabaseCopyStatus,
			// Token: 0x04001C33 RID: 7219
			OabMailboxManifestAddressListEmpty,
			// Token: 0x04001C34 RID: 7220
			PopCustomerTouchPointEscalationBodyENT,
			// Token: 0x04001C35 RID: 7221
			ServerInMaintenanceModeForTooLongEscalationMessage,
			// Token: 0x04001C36 RID: 7222
			LocalMachineDriveEncryptionStateEscalationSubject,
			// Token: 0x04001C37 RID: 7223
			EseLostFlushDetectedEscalationSubject,
			// Token: 0x04001C38 RID: 7224
			ImapSelfTestEscalationBodyDC,
			// Token: 0x04001C39 RID: 7225
			ArchiveNamePrefix,
			// Token: 0x04001C3A RID: 7226
			ReplServiceCrashEscalationSubject,
			// Token: 0x04001C3B RID: 7227
			EventAssistantsProcessRepeatedlyCrashingEscalationMessageDc,
			// Token: 0x04001C3C RID: 7228
			ComponentHealthHeartbeatEscalationMessageHealthy,
			// Token: 0x04001C3D RID: 7229
			OwaSelfTestEscalationSubject,
			// Token: 0x04001C3E RID: 7230
			EseSinglePageLogicalCorruptionDetectedEscalationMessage,
			// Token: 0x04001C3F RID: 7231
			JobobjectCpuExceededThresholdMessage,
			// Token: 0x04001C40 RID: 7232
			HighDiskLatencyEscalationMessage,
			// Token: 0x04001C41 RID: 7233
			ImapDeepTestEscalationBodyENT,
			// Token: 0x04001C42 RID: 7234
			OfficeGraphMessageTracingPluginLogDirectoryExceedsSizeLimit,
			// Token: 0x04001C43 RID: 7235
			MailSubmissionBehindWatermarksEscalationMessageDc,
			// Token: 0x04001C44 RID: 7236
			AsyncAuditLogSearchEscalationMessage,
			// Token: 0x04001C45 RID: 7237
			DatabaseLogicalPhysicalSizeRatioEscalationSubject,
			// Token: 0x04001C46 RID: 7238
			ServiceNotRunningEscalationMessageDc,
			// Token: 0x04001C47 RID: 7239
			DatabaseRPCLatencyEscalationMessageDc,
			// Token: 0x04001C48 RID: 7240
			ClusterServiceDownEscalationSubject,
			// Token: 0x04001C49 RID: 7241
			UnableToGetStoreUsageStatisticsData,
			// Token: 0x04001C4A RID: 7242
			BingServicesLatencyError,
			// Token: 0x04001C4B RID: 7243
			UMSipOptionsToUMCallRouterServiceFailedEscalationBody,
			// Token: 0x04001C4C RID: 7244
			CouldNotAddExchangeSnapInExceptionMessage,
			// Token: 0x04001C4D RID: 7245
			ClassficationEngineErrorsEscalationMessage,
			// Token: 0x04001C4E RID: 7246
			PopDeepTestEscalationBodyENT,
			// Token: 0x04001C4F RID: 7247
			DatabaseRepeatedMountsEscalationSubject,
			// Token: 0x04001C50 RID: 7248
			RwsDatamartConnectionEscalationSubject,
			// Token: 0x04001C51 RID: 7249
			SearchIndexBacklog,
			// Token: 0x04001C52 RID: 7250
			TransportSyncOutOfSLA,
			// Token: 0x04001C53 RID: 7251
			EwsAutodEscalationMessageUnhealthy,
			// Token: 0x04001C54 RID: 7252
			SecurityAlertEscalationMessage,
			// Token: 0x04001C55 RID: 7253
			VersionBucketsAllocatedEscalationEscalationMessageEnt,
			// Token: 0x04001C56 RID: 7254
			PushNotificationSendPublishNotificationError,
			// Token: 0x04001C57 RID: 7255
			SearchCatalogTooBig,
			// Token: 0x04001C58 RID: 7256
			SearchMemoryUsageOverThreshold,
			// Token: 0x04001C59 RID: 7257
			LocalMachineDriveProtectedWithDraWithoutDecryptorEscalationSubject,
			// Token: 0x04001C5A RID: 7258
			StoreMaintenanceAssistantEscalationMessageDc,
			// Token: 0x04001C5B RID: 7259
			EacSelfTestEscalationSubject,
			// Token: 0x04001C5C RID: 7260
			EacCtpTestEscalationSubject,
			// Token: 0x04001C5D RID: 7261
			InvokeNowProbeResultNotFound,
			// Token: 0x04001C5E RID: 7262
			BugCheckActionFailed,
			// Token: 0x04001C5F RID: 7263
			SiteMailboxDocumentSyncEscalationMessage,
			// Token: 0x04001C60 RID: 7264
			SearchIndexBacklogWithProcessingRate,
			// Token: 0x04001C61 RID: 7265
			SearchSingleCopyEscalationMessage,
			// Token: 0x04001C62 RID: 7266
			SearchIndexSingleHealthyCopy,
			// Token: 0x04001C63 RID: 7267
			MRSRepeatedlyCrashingEscalationMessage,
			// Token: 0x04001C64 RID: 7268
			DatabaseSizeEscalationMessageDc,
			// Token: 0x04001C65 RID: 7269
			TooManyDatabaseMountedEscalationMessage,
			// Token: 0x04001C66 RID: 7270
			SearchCatalogNotHealthyEscalationMessage,
			// Token: 0x04001C67 RID: 7271
			RcaTaskOutlineEntry,
			// Token: 0x04001C68 RID: 7272
			AssistantsNotRunningToCompletionError,
			// Token: 0x04001C69 RID: 7273
			NetworkAdapterRssEscalationSubject,
			// Token: 0x04001C6A RID: 7274
			EseDbTimeAdvanceEscalationSubject,
			// Token: 0x04001C6B RID: 7275
			SearchIndexActiveCopyNotIndxed,
			// Token: 0x04001C6C RID: 7276
			ServiceNotRunningEscalationSubject,
			// Token: 0x04001C6D RID: 7277
			SearchCatalogNotificationFeederLastEventZero,
			// Token: 0x04001C6E RID: 7278
			SearchQueryStxTimeout,
			// Token: 0x04001C6F RID: 7279
			DatabaseDiskReadLatencyEscalationMessageDc,
			// Token: 0x04001C70 RID: 7280
			SearchIndexCopyBacklogStatus,
			// Token: 0x04001C71 RID: 7281
			EseLostFlushDetectedEscalationMessage,
			// Token: 0x04001C72 RID: 7282
			PswsEscalationSubject
		}
	}
}
