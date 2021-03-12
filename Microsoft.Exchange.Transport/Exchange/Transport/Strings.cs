using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000579 RID: 1401
	internal static class Strings
	{
		// Token: 0x06003FDF RID: 16351 RVA: 0x00116440 File Offset: 0x00114640
		static Strings()
		{
			Strings.stringIDs.Add(3699713858U, "RoutingNoAdSites");
			Strings.stringIDs.Add(101432279U, "LatencyComponentHeartbeat");
			Strings.stringIDs.Add(677442004U, "ComponentsDisabledNone");
			Strings.stringIDs.Add(680722199U, "LatencyComponentRmsRequestDelegationToken");
			Strings.stringIDs.Add(2008205339U, "LatencyComponentRmsAcquireServerBoxRac");
			Strings.stringIDs.Add(393272335U, "LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreStats");
			Strings.stringIDs.Add(642728361U, "DatabaseOpen");
			Strings.stringIDs.Add(2221898189U, "ColumnName");
			Strings.stringIDs.Add(28104421U, "ShadowSendConnector");
			Strings.stringIDs.Add(781896450U, "LatencyComponentDeliveryQueueMailboxInsufficientResources");
			Strings.stringIDs.Add(897286993U, "Restricted");
			Strings.stringIDs.Add(3327987659U, "LatencyComponentStoreDriverDeliveryContentConversion");
			Strings.stringIDs.Add(3070454738U, "IntraorgSendConnectorName");
			Strings.stringIDs.Add(2741991560U, "LatencyComponentSmtpReceiveCommitLocal");
			Strings.stringIDs.Add(2269890262U, "LatencyComponentDeliveryQueueMailbox");
			Strings.stringIDs.Add(366323840U, "LatencyComponentDeliveryAgent");
			Strings.stringIDs.Add(2455354172U, "LatencyComponentRmsFindServiceLocation");
			Strings.stringIDs.Add(229786213U, "NormalPriority");
			Strings.stringIDs.Add(2359831369U, "LatencyComponentSmtpReceiveOnEndOfHeaders");
			Strings.stringIDs.Add(3475865632U, "Confidential");
			Strings.stringIDs.Add(3022513318U, "NormalRisk");
			Strings.stringIDs.Add(2222703033U, "ShadowRedundancyNoActiveServerInNexthopSolution");
			Strings.stringIDs.Add(1699301985U, "LatencyComponentStoreDriverOnCompletedMessage");
			Strings.stringIDs.Add(130958697U, "HighRisk");
			Strings.stringIDs.Add(1048481989U, "DatabaseRecoveryActionDelete");
			Strings.stringIDs.Add(1785499833U, "LatencyComponentSmtpReceiveOnProxyInboundMessage");
			Strings.stringIDs.Add(3547958413U, "LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionSmtpOut");
			Strings.stringIDs.Add(4212340469U, "LatencyComponentSmtpSend");
			Strings.stringIDs.Add(3759339643U, "MediumResourceUses");
			Strings.stringIDs.Add(3152927583U, "LatencyComponentSmtpReceiveCommitRemote");
			Strings.stringIDs.Add(4145912840U, "LatencyComponentCategorizer");
			Strings.stringIDs.Add(1414246128U, "None");
			Strings.stringIDs.Add(2123701631U, "LatencyComponentDumpster");
			Strings.stringIDs.Add(2393050832U, "EnumeratorBadPosition");
			Strings.stringIDs.Add(1226316175U, "LatencyComponentRmsAcquireCertificationMexData");
			Strings.stringIDs.Add(2892365298U, "ContentAggregationComponent");
			Strings.stringIDs.Add(1244889643U, "DiscardingDataFalse");
			Strings.stringIDs.Add(1050903407U, "DatabaseClosed");
			Strings.stringIDs.Add(2048777343U, "FailedToReadServerRole");
			Strings.stringIDs.Add(821664177U, "LatencyComponentDeliveryQueueMailboxDynamicMailboxDatabaseThrottlingLimitExceeded");
			Strings.stringIDs.Add(2000842882U, "LatencyComponentMailSubmissionServiceNotify");
			Strings.stringIDs.Add(217112309U, "LatencyComponentStoreDriverSubmissionStore");
			Strings.stringIDs.Add(3442182595U, "SeekFailed");
			Strings.stringIDs.Add(1243242609U, "LatencyComponentMailSubmissionServiceNotifyRetrySchedule");
			Strings.stringIDs.Add(1618072168U, "LatencyComponentStoreDriverOnDemotedMessage");
			Strings.stringIDs.Add(683438393U, "Public");
			Strings.stringIDs.Add(1815474557U, "AcceptedDomainTableNotLoaded");
			Strings.stringIDs.Add(1747692937U, "LatencyComponentServiceRestart");
			Strings.stringIDs.Add(525138956U, "NormalRiskNonePriority");
			Strings.stringIDs.Add(4093059930U, "LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionContentConversion");
			Strings.stringIDs.Add(526223919U, "LatencyComponentDeliveryQueueMailboxMapiExceptionTimeout");
			Strings.stringIDs.Add(177467553U, "LatencyComponentSmtpSendMailboxDelivery");
			Strings.stringIDs.Add(680901367U, "LatencyComponentDeliveryQueueExternal");
			Strings.stringIDs.Add(464945964U, "HighAndBulkRisk");
			Strings.stringIDs.Add(850634908U, "JetOperationFailure");
			Strings.stringIDs.Add(4222452556U, "LatencyComponentSmtpReceive");
			Strings.stringIDs.Add(2713736967U, "LatencyComponentSmtpReceiveDataExternal");
			Strings.stringIDs.Add(2517616835U, "LatencyComponentStoreDriverDeliveryAD");
			Strings.stringIDs.Add(3066388364U, "LatencyComponentDeliveryQueueMailboxDeliverAgentTransientFailure");
			Strings.stringIDs.Add(2895510700U, "LatencyComponentCategorizerFinal");
			Strings.stringIDs.Add(412845223U, "BulkRisk");
			Strings.stringIDs.Add(2314636918U, "LatencyComponentSubmissionQueue");
			Strings.stringIDs.Add(871184586U, "LatencyComponentStoreDriverSubmit");
			Strings.stringIDs.Add(1421458560U, "DumpsterJobStatusQueued");
			Strings.stringIDs.Add(320885804U, "LatencyComponentStoreDriverDeliveryMailboxDatabaseThrottling");
			Strings.stringIDs.Add(939699173U, "LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionOnDemotedMessage");
			Strings.stringIDs.Add(3432225769U, "SecureMailInvalidNumberOfLayers");
			Strings.stringIDs.Add(3242396317U, "LowRiskLowPriority");
			Strings.stringIDs.Add(1951232521U, "LatencyComponentMailSubmissionServiceFailedAttempt");
			Strings.stringIDs.Add(4208962580U, "RemoteDomainTableNotLoaded");
			Strings.stringIDs.Add(1681384042U, "OutboundMailDeliveryToRemoteDomainsComponent");
			Strings.stringIDs.Add(4095319950U, "AttachmentReadFailed");
			Strings.stringIDs.Add(2378292844U, "LatencyComponentMailboxRules");
			Strings.stringIDs.Add(3395591492U, "LowResourceUses");
			Strings.stringIDs.Add(1151336747U, "LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreDisposeSession");
			Strings.stringIDs.Add(4217035038U, "High");
			Strings.stringIDs.Add(198598092U, "IdentityParameterNotFound");
			Strings.stringIDs.Add(1049847567U, "NotOpenForWrite");
			Strings.stringIDs.Add(4063125577U, "TcpListenerError");
			Strings.stringIDs.Add(429833158U, "LatencyComponentMexRuntimeThreadpoolQueue");
			Strings.stringIDs.Add(1306604228U, "LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionPerfContextLdap");
			Strings.stringIDs.Add(1836041741U, "NormalResourceUses");
			Strings.stringIDs.Add(364271663U, "DumpsterJobStatusCreated");
			Strings.stringIDs.Add(3603045308U, "LowPriority");
			Strings.stringIDs.Add(2295242475U, "ExternalDestinationInboundProxySendConnector");
			Strings.stringIDs.Add(1092559024U, "IPFilterDatabaseInstanceName");
			Strings.stringIDs.Add(3889172535U, "ActivationFailed");
			Strings.stringIDs.Add(2198647971U, "LatencyComponentDelivery");
			Strings.stringIDs.Add(3545397251U, "AggregateResource");
			Strings.stringIDs.Add(1807713273U, "LatencyComponentProcessingSchedulerScoped");
			Strings.stringIDs.Add(1437992463U, "LatencyComponentSmtpReceiveOnRcpt2Command");
			Strings.stringIDs.Add(68652566U, "LatencyComponentStoreDriverOnPromotedMessage");
			Strings.stringIDs.Add(1004476481U, "PoisonMessageRegistryAccessFailed");
			Strings.stringIDs.Add(4020652394U, "HighResourceUses");
			Strings.stringIDs.Add(3706269143U, "EnvelopRecipientDisposed");
			Strings.stringIDs.Add(2556558528U, "SystemMemory");
			Strings.stringIDs.Add(184239278U, "ReadOrgContainerFailed");
			Strings.stringIDs.Add(488909308U, "LatencyComponentStoreDriverOnCreatedMessage");
			Strings.stringIDs.Add(3548219859U, "CategorizerMaxConfigLoadRetriesReached");
			Strings.stringIDs.Add(1542680578U, "LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreOpenSession");
			Strings.stringIDs.Add(1797311582U, "RoutingLocalServerIsNotBridgehead");
			Strings.stringIDs.Add(1030204127U, "LatencyComponentCategorizerOnCategorizedMessage");
			Strings.stringIDs.Add(727067537U, "LatencyComponentRmsAcquireB2BRac");
			Strings.stringIDs.Add(2163339405U, "InternalDestinationInboundProxySendConnector");
			Strings.stringIDs.Add(1080294189U, "LatencyComponentDeliveryQueueMailboxMaxConcurrentMessageSizeLimitExceeded");
			Strings.stringIDs.Add(170486207U, "LatencyComponentStoreDriverDeliveryRpc");
			Strings.stringIDs.Add(1602038336U, "ConnectionInUse");
			Strings.stringIDs.Add(4260841639U, "LatencyComponentMailSubmissionService");
			Strings.stringIDs.Add(4278195556U, "NonePriority");
			Strings.stringIDs.Add(3768774525U, "LatencyComponentRmsAcquireTemplateInfo");
			Strings.stringIDs.Add(3451284522U, "LatencyComponentContentAggregation");
			Strings.stringIDs.Add(2443567820U, "LatencyComponentMailSubmissionServiceShadowResubmitDecision");
			Strings.stringIDs.Add(3636715311U, "LatencyComponentContentAggregationMailItemCommit");
			Strings.stringIDs.Add(1674978893U, "DatabaseRecoveryActionMove");
			Strings.stringIDs.Add(3640623549U, "ReadTransportServerConfigFailed");
			Strings.stringIDs.Add(4082435799U, "LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionHubSelector");
			Strings.stringIDs.Add(1054523613U, "MessageTrackingConfigNotFound");
			Strings.stringIDs.Add(16119773U, "LatencyComponentDeliveryQueueInternal");
			Strings.stringIDs.Add(2383564204U, "LatencyComponentDeliveryQueueMailboxMailboxServerOffline");
			Strings.stringIDs.Add(3307097748U, "PrivateBytesResource");
			Strings.stringIDs.Add(4156740374U, "LatencyComponentDeliveryQueueMailboxMailboxDatabaseOffline");
			Strings.stringIDs.Add(1447270652U, "RoutingMaxConfigLoadRetriesReached");
			Strings.stringIDs.Add(1851026156U, "LatencyComponentRmsAcquireServerLicensingMexData");
			Strings.stringIDs.Add(2614121165U, "LatencyComponentDeliveryQueueMailboxRecipientThreadLimitExceeded");
			Strings.stringIDs.Add(1010194958U, "SmtpReceiveParserNegativeBytes");
			Strings.stringIDs.Add(702550931U, "InvalidRoleChange");
			Strings.stringIDs.Add(3077939474U, "LatencyComponentPoisonQueue");
			Strings.stringIDs.Add(3953400951U, "BootScannerComponent");
			Strings.stringIDs.Add(1434651324U, "NotInTransaction");
			Strings.stringIDs.Add(3231014581U, "LatencyComponentSmtpReceiveOnRcptCommand");
			Strings.stringIDs.Add(2675789057U, "BodyReadFailed");
			Strings.stringIDs.Add(1355928173U, "TextConvertersFailed");
			Strings.stringIDs.Add(604013917U, "RoutingNoRoutingGroups");
			Strings.stringIDs.Add(1040763200U, "LatencyComponentStoreDriverDeliveryMessageConcurrency");
			Strings.stringIDs.Add(2064442254U, "Submission");
			Strings.stringIDs.Add(1817505332U, "ReadingADConfigFailed");
			Strings.stringIDs.Add(4179070144U, "LatencyComponentTooManyComponents");
			Strings.stringIDs.Add(531266226U, "ReadTransportConfigConfigFailed");
			Strings.stringIDs.Add(3659171826U, "InvalidMessageResubmissionState");
			Strings.stringIDs.Add(2339552741U, "LatencyComponentSmtpReceiveOnEndOfData");
			Strings.stringIDs.Add(551877430U, "InvalidTransportRole");
			Strings.stringIDs.Add(2370750848U, "LatencyComponentReplay");
			Strings.stringIDs.Add(3447320822U, "CloneMoveDestination");
			Strings.stringIDs.Add(3132079455U, "LowRiskNonePriority");
			Strings.stringIDs.Add(3933849364U, "LatencyComponentCategorizerBifurcation");
			Strings.stringIDs.Add(3849329560U, "SchemaInvalid");
			Strings.stringIDs.Add(2223967066U, "QuoteNestLevel");
			Strings.stringIDs.Add(1966949091U, "LatencyComponentUnknown");
			Strings.stringIDs.Add(1884563717U, "LatencyComponentTotal");
			Strings.stringIDs.Add(2229981216U, "LatencyComponentRmsAcquireLicense");
			Strings.stringIDs.Add(1359462263U, "InboundMailSubmissionFromReplayDirectoryComponent");
			Strings.stringIDs.Add(3952286128U, "NoColumns");
			Strings.stringIDs.Add(3021848955U, "LatencyComponentRmsAcquireClc");
			Strings.stringIDs.Add(1362050859U, "CloneMoveComplete");
			Strings.stringIDs.Add(1615937519U, "LatencyComponentQuarantineReleaseOrReport");
			Strings.stringIDs.Add(662654939U, "LatencyComponentSubmissionAssistant");
			Strings.stringIDs.Add(529499203U, "DumpsterJobResponseSuccess");
			Strings.stringIDs.Add(1767865225U, "AgentComponentFailed");
			Strings.stringIDs.Add(2584904976U, "ClientProxySendConnector");
			Strings.stringIDs.Add(4128944152U, "Basic");
			Strings.stringIDs.Add(2705384945U, "LatencyComponentCategorizerOnSubmittedMessage");
			Strings.stringIDs.Add(3565174541U, "NormalRiskNormalPriority");
			Strings.stringIDs.Add(1411027439U, "LatencyComponentSmtpReceiveCommit");
			Strings.stringIDs.Add(3882493397U, "RoutingNoLocalServer");
			Strings.stringIDs.Add(3622146271U, "SecureMailSecondLayerMustBeEnveloped");
			Strings.stringIDs.Add(3840724219U, "MailItemDeferred");
			Strings.stringIDs.Add(3706643741U, "InboundMailSubmissionFromMailboxComponent");
			Strings.stringIDs.Add(56765413U, "AttachmentProtectionFailed");
			Strings.stringIDs.Add(4115715614U, "LatencyComponentSubmissionAssistantThrottling");
			Strings.stringIDs.Add(1586175673U, "LatencyComponentCategorizerLocking");
			Strings.stringIDs.Add(955728834U, "ValueNull");
			Strings.stringIDs.Add(2197880345U, "TooManyAgents");
			Strings.stringIDs.Add(3324883850U, "DumpsterJobStatusCompleted");
			Strings.stringIDs.Add(2127171860U, "LatencyComponentStoreDriverOnDeliveredMessage");
			Strings.stringIDs.Add(1858725880U, "LatencyComponentRmsAcquireB2BLicense");
			Strings.stringIDs.Add(2602495526U, "LatencyComponentDeliveryQueueMailboxMapiExceptionLockViolation");
			Strings.stringIDs.Add(1946647341U, "Medium");
			Strings.stringIDs.Add(1261670220U, "NotBufferedStream");
			Strings.stringIDs.Add(2888586632U, "IncorrectBaseStream");
			Strings.stringIDs.Add(26231820U, "LatencyComponentProcess");
			Strings.stringIDs.Add(3133088287U, "CommitMailFailed");
			Strings.stringIDs.Add(1974402452U, "NonAsciiData");
			Strings.stringIDs.Add(2736623442U, "SeekBarred");
			Strings.stringIDs.Add(3819242299U, "RoutingLocalRgNotSet");
			Strings.stringIDs.Add(262955557U, "MessagingDatabaseInstanceName");
			Strings.stringIDs.Add(2000471546U, "LatencyComponentUnreachableQueue");
			Strings.stringIDs.Add(1113830170U, "LatencyComponentStoreDriverDelivery");
			Strings.stringIDs.Add(1062938671U, "DatabaseStillInUse");
			Strings.stringIDs.Add(680830688U, "LatencyComponentDeferral");
			Strings.stringIDs.Add(793374748U, "DumpsterJobResponseRetryLater");
			Strings.stringIDs.Add(1287594420U, "RoutingNoLocalAdSite");
			Strings.stringIDs.Add(3350471676U, "LatencyComponentRmsAcquireTemplates");
			Strings.stringIDs.Add(3717583665U, "InvalidRoutingOverrideEvent");
			Strings.stringIDs.Add(1384341089U, "GetSclThresholdDefaultValueOutOfRange");
			Strings.stringIDs.Add(3449384489U, "LatencyComponentSmtpReceiveDataInternal");
			Strings.stringIDs.Add(2279316324U, "InvalidTenantLicensePair");
			Strings.stringIDs.Add(3889416851U, "ColumnIndexesMustBeSequential");
			Strings.stringIDs.Add(3854009776U, "LatencyComponentNonSmtpGateway");
			Strings.stringIDs.Add(2176966386U, "LatencyComponentDeliveryAgentOnDeliverMailItem");
			Strings.stringIDs.Add(2711829446U, "ShadowRedundancyComponentBanner");
			Strings.stringIDs.Add(350383284U, "CloneMoveSourceModified");
			Strings.stringIDs.Add(1674978920U, "DatabaseRecoveryActionNone");
			Strings.stringIDs.Add(1142385700U, "LatencyComponentMailSubmissionServiceThrottling");
			Strings.stringIDs.Add(1673427229U, "LatencyComponentDsnGenerator");
			Strings.stringIDs.Add(2385069791U, "RowDeleted");
			Strings.stringIDs.Add(4154552087U, "LatencyComponentCategorizerContentConversion");
			Strings.stringIDs.Add(3393444732U, "LatencyComponentExternalServers");
			Strings.stringIDs.Add(3672571637U, "ReadMicrosoftExchangeRecipientFailed");
			Strings.stringIDs.Add(3993229380U, "SeekGeneralFailure");
			Strings.stringIDs.Add(3321191670U, "LatencyComponentCategorizerRouting");
			Strings.stringIDs.Add(1692037724U, "LatencyComponentOriginalMailDsn");
			Strings.stringIDs.Add(1471624947U, "LatencyComponentPickup");
			Strings.stringIDs.Add(615004755U, "LowRisk");
			Strings.stringIDs.Add(2919439981U, "LatencyComponentDeliveryAgentOnOpenConnection");
			Strings.stringIDs.Add(2349744297U, "LatencyComponentCategorizerOnRoutedMessage");
			Strings.stringIDs.Add(1601350579U, "CategorizerConfigValidationFailed");
			Strings.stringIDs.Add(2863890221U, "LatencyComponentNone");
			Strings.stringIDs.Add(2071866321U, "LatencyComponentDeliveryQueueLocking");
			Strings.stringIDs.Add(2768133224U, "LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionAD");
			Strings.stringIDs.Add(3862731819U, "MessageResubmissionComponentBanner");
			Strings.stringIDs.Add(3955727133U, "TotalExcludingPriorityNone");
			Strings.stringIDs.Add(3906053774U, "CloneMoveSourceNull");
			Strings.stringIDs.Add(92299947U, "LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionSmtp");
			Strings.stringIDs.Add(3127930220U, "LatencyComponentAgent");
			Strings.stringIDs.Add(650877237U, "InboundMailSubmissionFromHubsComponent");
			Strings.stringIDs.Add(3875073169U, "AlreadyJoined");
			Strings.stringIDs.Add(3994617651U, "LatencyComponentStoreDriverSubmissionRpc");
			Strings.stringIDs.Add(2896583167U, "NormalAndLowRisk");
			Strings.stringIDs.Add(175131561U, "LatencyComponentMailboxTransportSubmissionStoreDriverSubmission");
			Strings.stringIDs.Add(335920517U, "InvalidRoutingOverride");
			Strings.stringIDs.Add(3689512814U, "LatencyComponentSmtpReceiveOnDataCommand");
			Strings.stringIDs.Add(4114179756U, "LowRiskNormalPriority");
			Strings.stringIDs.Add(3171622157U, "AalCalClassificationDisplayName");
			Strings.stringIDs.Add(1744966745U, "HighlyConfidential");
			Strings.stringIDs.Add(2327669046U, "LatencyComponentStoreDriverOnInitializedMessage");
			Strings.stringIDs.Add(4016006905U, "LatencyComponentStoreDriverDeliveryStore");
			Strings.stringIDs.Add(3611628552U, "InvalidRowState");
			Strings.stringIDs.Add(3334507377U, "LatencyComponentMailboxTransportSubmissionService");
			Strings.stringIDs.Add(3514558095U, "LatencyComponentStoreDriverSubmissionAD");
			Strings.stringIDs.Add(2595056666U, "DumpsterJobStatusProcessing");
			Strings.stringIDs.Add(2912766470U, "RoutingNoLocalRgObject");
			Strings.stringIDs.Add(2117972488U, "PendingTransactions");
			Strings.stringIDs.Add(3750964199U, "TrailingEscape");
			Strings.stringIDs.Add(1679638210U, "CloneMoveTargetNotNew");
			Strings.stringIDs.Add(2607217314U, "LatencyComponentCategorizerResolver");
			Strings.stringIDs.Add(99902445U, "TransportComponentLoadFailed");
			Strings.stringIDs.Add(2986853275U, "LatencyComponentProcessingScheduler");
			Strings.stringIDs.Add(2104269935U, "LatencyComponentSmtpSendConnect");
			Strings.stringIDs.Add(936688982U, "Minimum");
			Strings.stringIDs.Add(1677063078U, "InboundMailSubmissionFromPickupDirectoryComponent");
			Strings.stringIDs.Add(1209655894U, "BreadCrumbSize");
			Strings.stringIDs.Add(885455923U, "CloneMoveSourceNotSaved");
			Strings.stringIDs.Add(3063068326U, "LatencyComponentShadowQueue");
			Strings.stringIDs.Add(3504101496U, "InvalidCursorState");
			Strings.stringIDs.Add(3589283362U, "LatencyComponentUnderThreshold");
			Strings.stringIDs.Add(3124262306U, "HighPriority");
			Strings.stringIDs.Add(1180878696U, "CircularClone");
			Strings.stringIDs.Add(3959631871U, "InvalidDeleteState");
			Strings.stringIDs.Add(642210074U, "LatencyComponentExternalPartnerServers");
			Strings.stringIDs.Add(1582326851U, "MailboxProxySendConnector");
			Strings.stringIDs.Add(3238954214U, "ExternalDestinationOutboundProxySendConnector");
			Strings.stringIDs.Add(2588281919U, "InvalidTransportServerRole");
			Strings.stringIDs.Add(3502104427U, "IncorrectColumn");
			Strings.stringIDs.Add(2368056258U, "CountWrong");
			Strings.stringIDs.Add(1954095043U, "LatencyComponentRmsAcquirePreLicense");
			Strings.stringIDs.Add(3452310564U, "SecureMailOuterLayerMustBeSigned");
			Strings.stringIDs.Add(3835436216U, "LatencyComponentMailboxMove");
			Strings.stringIDs.Add(1450242468U, "InternalDestinationOutboundProxySendConnector");
			Strings.stringIDs.Add(3494923132U, "IncorrectBrace");
			Strings.stringIDs.Add(309843435U, "KeyLength");
			Strings.stringIDs.Add(1227018995U, "InvalidRank");
			Strings.stringIDs.Add(3974043093U, "MimeWriteStreamOpen");
			Strings.stringIDs.Add(1578443098U, "StreamStateInvalid");
			Strings.stringIDs.Add(2070671180U, "NormalRiskLowPriority");
			Strings.stringIDs.Add(2721873196U, "InboundMailSubmissionFromInternetComponent");
			Strings.stringIDs.Add(3160113472U, "LatencyComponentCategorizerOnResolvedMessage");
			Strings.stringIDs.Add(4049537308U, "BodyFormatUnsupported");
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x00117A23 File Offset: 0x00115C23
		public static LocalizedString RoutingNoAdSites
		{
			get
			{
				return new LocalizedString("RoutingNoAdSites", "ExB4B5E0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06003FE1 RID: 16353 RVA: 0x00117A41 File Offset: 0x00115C41
		public static LocalizedString LatencyComponentHeartbeat
		{
			get
			{
				return new LocalizedString("LatencyComponentHeartbeat", "Ex3347E3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x00117A5F File Offset: 0x00115C5F
		public static LocalizedString ComponentsDisabledNone
		{
			get
			{
				return new LocalizedString("ComponentsDisabledNone", "ExBC74F8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06003FE3 RID: 16355 RVA: 0x00117A7D File Offset: 0x00115C7D
		public static LocalizedString LatencyComponentRmsRequestDelegationToken
		{
			get
			{
				return new LocalizedString("LatencyComponentRmsRequestDelegationToken", "Ex1029ED", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06003FE4 RID: 16356 RVA: 0x00117A9B File Offset: 0x00115C9B
		public static LocalizedString LatencyComponentRmsAcquireServerBoxRac
		{
			get
			{
				return new LocalizedString("LatencyComponentRmsAcquireServerBoxRac", "Ex9BBD15", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06003FE5 RID: 16357 RVA: 0x00117AB9 File Offset: 0x00115CB9
		public static LocalizedString LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreStats
		{
			get
			{
				return new LocalizedString("LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreStats", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x00117AD7 File Offset: 0x00115CD7
		public static LocalizedString DatabaseOpen
		{
			get
			{
				return new LocalizedString("DatabaseOpen", "ExC6ED6D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06003FE7 RID: 16359 RVA: 0x00117AF5 File Offset: 0x00115CF5
		public static LocalizedString ColumnName
		{
			get
			{
				return new LocalizedString("ColumnName", "ExF5AE98", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06003FE8 RID: 16360 RVA: 0x00117B13 File Offset: 0x00115D13
		public static LocalizedString ShadowSendConnector
		{
			get
			{
				return new LocalizedString("ShadowSendConnector", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06003FE9 RID: 16361 RVA: 0x00117B31 File Offset: 0x00115D31
		public static LocalizedString LatencyComponentDeliveryQueueMailboxInsufficientResources
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryQueueMailboxInsufficientResources", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06003FEA RID: 16362 RVA: 0x00117B4F File Offset: 0x00115D4F
		public static LocalizedString Restricted
		{
			get
			{
				return new LocalizedString("Restricted", "ExFF6B5E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06003FEB RID: 16363 RVA: 0x00117B6D File Offset: 0x00115D6D
		public static LocalizedString LatencyComponentStoreDriverDeliveryContentConversion
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverDeliveryContentConversion", "ExEB2D89", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06003FEC RID: 16364 RVA: 0x00117B8B File Offset: 0x00115D8B
		public static LocalizedString IntraorgSendConnectorName
		{
			get
			{
				return new LocalizedString("IntraorgSendConnectorName", "Ex25CD79", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x00117BAC File Offset: 0x00115DAC
		public static LocalizedString PhysicalMemoryUses(int pressure, int limit)
		{
			return new LocalizedString("PhysicalMemoryUses", "Ex6A7FEE", false, true, Strings.ResourceManager, new object[]
			{
				pressure,
				limit
			});
		}

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06003FEE RID: 16366 RVA: 0x00117BE9 File Offset: 0x00115DE9
		public static LocalizedString LatencyComponentSmtpReceiveCommitLocal
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpReceiveCommitLocal", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06003FEF RID: 16367 RVA: 0x00117C07 File Offset: 0x00115E07
		public static LocalizedString LatencyComponentDeliveryQueueMailbox
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryQueueMailbox", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06003FF0 RID: 16368 RVA: 0x00117C25 File Offset: 0x00115E25
		public static LocalizedString LatencyComponentDeliveryAgent
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryAgent", "ExCC0B74", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06003FF1 RID: 16369 RVA: 0x00117C43 File Offset: 0x00115E43
		public static LocalizedString LatencyComponentRmsFindServiceLocation
		{
			get
			{
				return new LocalizedString("LatencyComponentRmsFindServiceLocation", "Ex19390E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06003FF2 RID: 16370 RVA: 0x00117C61 File Offset: 0x00115E61
		public static LocalizedString NormalPriority
		{
			get
			{
				return new LocalizedString("NormalPriority", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06003FF3 RID: 16371 RVA: 0x00117C7F File Offset: 0x00115E7F
		public static LocalizedString LatencyComponentSmtpReceiveOnEndOfHeaders
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpReceiveOnEndOfHeaders", "ExF1ACF6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06003FF4 RID: 16372 RVA: 0x00117C9D File Offset: 0x00115E9D
		public static LocalizedString Confidential
		{
			get
			{
				return new LocalizedString("Confidential", "Ex3F6E10", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06003FF5 RID: 16373 RVA: 0x00117CBB File Offset: 0x00115EBB
		public static LocalizedString NormalRisk
		{
			get
			{
				return new LocalizedString("NormalRisk", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x00117CD9 File Offset: 0x00115ED9
		public static LocalizedString ShadowRedundancyNoActiveServerInNexthopSolution
		{
			get
			{
				return new LocalizedString("ShadowRedundancyNoActiveServerInNexthopSolution", "Ex69F8D5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06003FF7 RID: 16375 RVA: 0x00117CF7 File Offset: 0x00115EF7
		public static LocalizedString LatencyComponentStoreDriverOnCompletedMessage
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverOnCompletedMessage", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x00117D18 File Offset: 0x00115F18
		public static LocalizedString DatabaseAttachFailed(string databaseName)
		{
			return new LocalizedString("DatabaseAttachFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06003FF9 RID: 16377 RVA: 0x00117D47 File Offset: 0x00115F47
		public static LocalizedString HighRisk
		{
			get
			{
				return new LocalizedString("HighRisk", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06003FFA RID: 16378 RVA: 0x00117D65 File Offset: 0x00115F65
		public static LocalizedString DatabaseRecoveryActionDelete
		{
			get
			{
				return new LocalizedString("DatabaseRecoveryActionDelete", "Ex62B1D2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06003FFB RID: 16379 RVA: 0x00117D83 File Offset: 0x00115F83
		public static LocalizedString LatencyComponentSmtpReceiveOnProxyInboundMessage
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpReceiveOnProxyInboundMessage", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06003FFC RID: 16380 RVA: 0x00117DA1 File Offset: 0x00115FA1
		public static LocalizedString LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionSmtpOut
		{
			get
			{
				return new LocalizedString("LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionSmtpOut", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06003FFD RID: 16381 RVA: 0x00117DBF File Offset: 0x00115FBF
		public static LocalizedString LatencyComponentSmtpSend
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpSend", "ExDD5B34", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06003FFE RID: 16382 RVA: 0x00117DDD File Offset: 0x00115FDD
		public static LocalizedString MediumResourceUses
		{
			get
			{
				return new LocalizedString("MediumResourceUses", "Ex6A4267", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06003FFF RID: 16383 RVA: 0x00117DFB File Offset: 0x00115FFB
		public static LocalizedString LatencyComponentSmtpReceiveCommitRemote
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpReceiveCommitRemote", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x00117E1C File Offset: 0x0011601C
		public static LocalizedString InvalidCharset(string name)
		{
			return new LocalizedString("InvalidCharset", "Ex38A3AD", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06004001 RID: 16385 RVA: 0x00117E4B File Offset: 0x0011604B
		public static LocalizedString LatencyComponentCategorizer
		{
			get
			{
				return new LocalizedString("LatencyComponentCategorizer", "ExFACFA2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x00117E6C File Offset: 0x0011606C
		public static LocalizedString ResourcesInNormalPressure(string resources)
		{
			return new LocalizedString("ResourcesInNormalPressure", "ExE6E8C7", false, true, Strings.ResourceManager, new object[]
			{
				resources
			});
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x00117E9C File Offset: 0x0011609C
		public static LocalizedString ResourceUses(string name, int pressure, string uses, int normal, int medium, int high)
		{
			return new LocalizedString("ResourceUses", "Ex374B24", false, true, Strings.ResourceManager, new object[]
			{
				name,
				pressure,
				uses,
				normal,
				medium,
				high
			});
		}

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x00117EF5 File Offset: 0x001160F5
		public static LocalizedString None
		{
			get
			{
				return new LocalizedString("None", "Ex8D93DD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06004005 RID: 16389 RVA: 0x00117F13 File Offset: 0x00116113
		public static LocalizedString LatencyComponentDumpster
		{
			get
			{
				return new LocalizedString("LatencyComponentDumpster", "Ex36EE06", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x00117F31 File Offset: 0x00116131
		public static LocalizedString EnumeratorBadPosition
		{
			get
			{
				return new LocalizedString("EnumeratorBadPosition", "Ex8642C3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06004007 RID: 16391 RVA: 0x00117F4F File Offset: 0x0011614F
		public static LocalizedString LatencyComponentRmsAcquireCertificationMexData
		{
			get
			{
				return new LocalizedString("LatencyComponentRmsAcquireCertificationMexData", "Ex50E004", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x06004008 RID: 16392 RVA: 0x00117F6D File Offset: 0x0011616D
		public static LocalizedString ContentAggregationComponent
		{
			get
			{
				return new LocalizedString("ContentAggregationComponent", "Ex16EB9F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06004009 RID: 16393 RVA: 0x00117F8B File Offset: 0x0011618B
		public static LocalizedString DiscardingDataFalse
		{
			get
			{
				return new LocalizedString("DiscardingDataFalse", "ExE5C5D8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x00117FA9 File Offset: 0x001161A9
		public static LocalizedString DatabaseClosed
		{
			get
			{
				return new LocalizedString("DatabaseClosed", "Ex92FBE4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x0600400B RID: 16395 RVA: 0x00117FC7 File Offset: 0x001161C7
		public static LocalizedString FailedToReadServerRole
		{
			get
			{
				return new LocalizedString("FailedToReadServerRole", "ExC69380", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x00117FE5 File Offset: 0x001161E5
		public static LocalizedString LatencyComponentDeliveryQueueMailboxDynamicMailboxDatabaseThrottlingLimitExceeded
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryQueueMailboxDynamicMailboxDatabaseThrottlingLimitExceeded", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x0600400D RID: 16397 RVA: 0x00118003 File Offset: 0x00116203
		public static LocalizedString LatencyComponentMailSubmissionServiceNotify
		{
			get
			{
				return new LocalizedString("LatencyComponentMailSubmissionServiceNotify", "Ex5B1A7D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x00118021 File Offset: 0x00116221
		public static LocalizedString LatencyComponentStoreDriverSubmissionStore
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverSubmissionStore", "Ex93C7FE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x0600400F RID: 16399 RVA: 0x0011803F File Offset: 0x0011623F
		public static LocalizedString SeekFailed
		{
			get
			{
				return new LocalizedString("SeekFailed", "ExDCE0E2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x0011805D File Offset: 0x0011625D
		public static LocalizedString LatencyComponentMailSubmissionServiceNotifyRetrySchedule
		{
			get
			{
				return new LocalizedString("LatencyComponentMailSubmissionServiceNotifyRetrySchedule", "ExA34296", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x06004011 RID: 16401 RVA: 0x0011807B File Offset: 0x0011627B
		public static LocalizedString LatencyComponentStoreDriverOnDemotedMessage
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverOnDemotedMessage", "Ex339F42", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06004012 RID: 16402 RVA: 0x00118099 File Offset: 0x00116299
		public static LocalizedString Public
		{
			get
			{
				return new LocalizedString("Public", "ExCEA3DE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x001180B8 File Offset: 0x001162B8
		public static LocalizedString DatabaseAndDatabaseLogDeleted(string databasePath, string databaseLogPath)
		{
			return new LocalizedString("DatabaseAndDatabaseLogDeleted", "", false, false, Strings.ResourceManager, new object[]
			{
				databasePath,
				databaseLogPath
			});
		}

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x001180EB File Offset: 0x001162EB
		public static LocalizedString AcceptedDomainTableNotLoaded
		{
			get
			{
				return new LocalizedString("AcceptedDomainTableNotLoaded", "Ex353A51", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06004015 RID: 16405 RVA: 0x00118109 File Offset: 0x00116309
		public static LocalizedString LatencyComponentServiceRestart
		{
			get
			{
				return new LocalizedString("LatencyComponentServiceRestart", "Ex87CE3A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x00118127 File Offset: 0x00116327
		public static LocalizedString NormalRiskNonePriority
		{
			get
			{
				return new LocalizedString("NormalRiskNonePriority", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x00118148 File Offset: 0x00116348
		public static LocalizedString RoutingDecryptConnectorPasswordFailure(string connectorName, string errorCode)
		{
			return new LocalizedString("RoutingDecryptConnectorPasswordFailure", "ExE7DD67", false, true, Strings.ResourceManager, new object[]
			{
				connectorName,
				errorCode
			});
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x0011817C File Offset: 0x0011637C
		public static LocalizedString DatabaseLoggingResource(string loggingPath)
		{
			return new LocalizedString("DatabaseLoggingResource", "Ex2F7357", false, true, Strings.ResourceManager, new object[]
			{
				loggingPath
			});
		}

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06004019 RID: 16409 RVA: 0x001181AB File Offset: 0x001163AB
		public static LocalizedString LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionContentConversion
		{
			get
			{
				return new LocalizedString("LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionContentConversion", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x0600401A RID: 16410 RVA: 0x001181C9 File Offset: 0x001163C9
		public static LocalizedString LatencyComponentDeliveryQueueMailboxMapiExceptionTimeout
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryQueueMailboxMapiExceptionTimeout", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x0600401B RID: 16411 RVA: 0x001181E7 File Offset: 0x001163E7
		public static LocalizedString LatencyComponentSmtpSendMailboxDelivery
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpSendMailboxDelivery", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x0600401C RID: 16412 RVA: 0x00118205 File Offset: 0x00116405
		public static LocalizedString LatencyComponentDeliveryQueueExternal
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryQueueExternal", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x0600401D RID: 16413 RVA: 0x00118223 File Offset: 0x00116423
		public static LocalizedString HighAndBulkRisk
		{
			get
			{
				return new LocalizedString("HighAndBulkRisk", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x0600401E RID: 16414 RVA: 0x00118241 File Offset: 0x00116441
		public static LocalizedString JetOperationFailure
		{
			get
			{
				return new LocalizedString("JetOperationFailure", "Ex053A49", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x0600401F RID: 16415 RVA: 0x0011825F File Offset: 0x0011645F
		public static LocalizedString LatencyComponentSmtpReceive
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpReceive", "Ex86CE7C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06004020 RID: 16416 RVA: 0x0011827D File Offset: 0x0011647D
		public static LocalizedString LatencyComponentSmtpReceiveDataExternal
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpReceiveDataExternal", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06004021 RID: 16417 RVA: 0x0011829B File Offset: 0x0011649B
		public static LocalizedString LatencyComponentStoreDriverDeliveryAD
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverDeliveryAD", "Ex561E96", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06004022 RID: 16418 RVA: 0x001182B9 File Offset: 0x001164B9
		public static LocalizedString LatencyComponentDeliveryQueueMailboxDeliverAgentTransientFailure
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryQueueMailboxDeliverAgentTransientFailure", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06004023 RID: 16419 RVA: 0x001182D7 File Offset: 0x001164D7
		public static LocalizedString LatencyComponentCategorizerFinal
		{
			get
			{
				return new LocalizedString("LatencyComponentCategorizerFinal", "Ex66AF88", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06004024 RID: 16420 RVA: 0x001182F5 File Offset: 0x001164F5
		public static LocalizedString BulkRisk
		{
			get
			{
				return new LocalizedString("BulkRisk", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06004025 RID: 16421 RVA: 0x00118313 File Offset: 0x00116513
		public static LocalizedString LatencyComponentSubmissionQueue
		{
			get
			{
				return new LocalizedString("LatencyComponentSubmissionQueue", "Ex3B52C2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x00118331 File Offset: 0x00116531
		public static LocalizedString LatencyComponentStoreDriverSubmit
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverSubmit", "Ex757015", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06004027 RID: 16423 RVA: 0x0011834F File Offset: 0x0011654F
		public static LocalizedString DumpsterJobStatusQueued
		{
			get
			{
				return new LocalizedString("DumpsterJobStatusQueued", "Ex89538D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x00118370 File Offset: 0x00116570
		public static LocalizedString TlsCertificateNameNotFound(string tlsCertificateName, string connectorName)
		{
			return new LocalizedString("TlsCertificateNameNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				tlsCertificateName,
				connectorName
			});
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06004029 RID: 16425 RVA: 0x001183A3 File Offset: 0x001165A3
		public static LocalizedString LatencyComponentStoreDriverDeliveryMailboxDatabaseThrottling
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverDeliveryMailboxDatabaseThrottling", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x0600402A RID: 16426 RVA: 0x001183C1 File Offset: 0x001165C1
		public static LocalizedString LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionOnDemotedMessage
		{
			get
			{
				return new LocalizedString("LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionOnDemotedMessage", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x0600402B RID: 16427 RVA: 0x001183DF File Offset: 0x001165DF
		public static LocalizedString SecureMailInvalidNumberOfLayers
		{
			get
			{
				return new LocalizedString("SecureMailInvalidNumberOfLayers", "Ex8EF442", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x0600402C RID: 16428 RVA: 0x001183FD File Offset: 0x001165FD
		public static LocalizedString LowRiskLowPriority
		{
			get
			{
				return new LocalizedString("LowRiskLowPriority", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x0600402D RID: 16429 RVA: 0x0011841B File Offset: 0x0011661B
		public static LocalizedString LatencyComponentMailSubmissionServiceFailedAttempt
		{
			get
			{
				return new LocalizedString("LatencyComponentMailSubmissionServiceFailedAttempt", "ExA27F43", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x0600402E RID: 16430 RVA: 0x00118439 File Offset: 0x00116639
		public static LocalizedString RemoteDomainTableNotLoaded
		{
			get
			{
				return new LocalizedString("RemoteDomainTableNotLoaded", "ExFF7502", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600402F RID: 16431 RVA: 0x00118458 File Offset: 0x00116658
		public static LocalizedString ColumnsMustBeOrdered(string columnName)
		{
			return new LocalizedString("ColumnsMustBeOrdered", "Ex09A2C2", false, true, Strings.ResourceManager, new object[]
			{
				columnName
			});
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06004030 RID: 16432 RVA: 0x00118487 File Offset: 0x00116687
		public static LocalizedString OutboundMailDeliveryToRemoteDomainsComponent
		{
			get
			{
				return new LocalizedString("OutboundMailDeliveryToRemoteDomainsComponent", "Ex31F696", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06004031 RID: 16433 RVA: 0x001184A5 File Offset: 0x001166A5
		public static LocalizedString AttachmentReadFailed
		{
			get
			{
				return new LocalizedString("AttachmentReadFailed", "Ex0885BC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06004032 RID: 16434 RVA: 0x001184C3 File Offset: 0x001166C3
		public static LocalizedString LatencyComponentMailboxRules
		{
			get
			{
				return new LocalizedString("LatencyComponentMailboxRules", "ExB7F29A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x001184E4 File Offset: 0x001166E4
		public static LocalizedString RoutingIdenticalExchangeLegacyDns(string server1, string server2)
		{
			return new LocalizedString("RoutingIdenticalExchangeLegacyDns", "ExF4FA16", false, true, Strings.ResourceManager, new object[]
			{
				server1,
				server2
			});
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06004034 RID: 16436 RVA: 0x00118517 File Offset: 0x00116717
		public static LocalizedString LowResourceUses
		{
			get
			{
				return new LocalizedString("LowResourceUses", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06004035 RID: 16437 RVA: 0x00118535 File Offset: 0x00116735
		public static LocalizedString LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreDisposeSession
		{
			get
			{
				return new LocalizedString("LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreDisposeSession", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06004036 RID: 16438 RVA: 0x00118553 File Offset: 0x00116753
		public static LocalizedString High
		{
			get
			{
				return new LocalizedString("High", "ExA8278F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x00118571 File Offset: 0x00116771
		public static LocalizedString IdentityParameterNotFound
		{
			get
			{
				return new LocalizedString("IdentityParameterNotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06004038 RID: 16440 RVA: 0x0011858F File Offset: 0x0011678F
		public static LocalizedString NotOpenForWrite
		{
			get
			{
				return new LocalizedString("NotOpenForWrite", "ExADA24D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06004039 RID: 16441 RVA: 0x001185AD File Offset: 0x001167AD
		public static LocalizedString TcpListenerError
		{
			get
			{
				return new LocalizedString("TcpListenerError", "Ex521241", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x0600403A RID: 16442 RVA: 0x001185CB File Offset: 0x001167CB
		public static LocalizedString LatencyComponentMexRuntimeThreadpoolQueue
		{
			get
			{
				return new LocalizedString("LatencyComponentMexRuntimeThreadpoolQueue", "Ex289D68", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x0600403B RID: 16443 RVA: 0x001185E9 File Offset: 0x001167E9
		public static LocalizedString LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionPerfContextLdap
		{
			get
			{
				return new LocalizedString("LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionPerfContextLdap", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x0600403C RID: 16444 RVA: 0x00118607 File Offset: 0x00116807
		public static LocalizedString NormalResourceUses
		{
			get
			{
				return new LocalizedString("NormalResourceUses", "ExA91D90", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x0600403D RID: 16445 RVA: 0x00118625 File Offset: 0x00116825
		public static LocalizedString DumpsterJobStatusCreated
		{
			get
			{
				return new LocalizedString("DumpsterJobStatusCreated", "Ex450EBD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x0600403E RID: 16446 RVA: 0x00118643 File Offset: 0x00116843
		public static LocalizedString LowPriority
		{
			get
			{
				return new LocalizedString("LowPriority", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x0600403F RID: 16447 RVA: 0x00118661 File Offset: 0x00116861
		public static LocalizedString ExternalDestinationInboundProxySendConnector
		{
			get
			{
				return new LocalizedString("ExternalDestinationInboundProxySendConnector", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06004040 RID: 16448 RVA: 0x0011867F File Offset: 0x0011687F
		public static LocalizedString IPFilterDatabaseInstanceName
		{
			get
			{
				return new LocalizedString("IPFilterDatabaseInstanceName", "Ex3E0C50", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06004041 RID: 16449 RVA: 0x0011869D File Offset: 0x0011689D
		public static LocalizedString ActivationFailed
		{
			get
			{
				return new LocalizedString("ActivationFailed", "Ex3BCE23", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06004042 RID: 16450 RVA: 0x001186BB File Offset: 0x001168BB
		public static LocalizedString LatencyComponentDelivery
		{
			get
			{
				return new LocalizedString("LatencyComponentDelivery", "Ex24CE50", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06004043 RID: 16451 RVA: 0x001186D9 File Offset: 0x001168D9
		public static LocalizedString AggregateResource
		{
			get
			{
				return new LocalizedString("AggregateResource", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x001186F8 File Offset: 0x001168F8
		public static LocalizedString DataBaseError(string databaseName)
		{
			return new LocalizedString("DataBaseError", "", false, false, Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x00118728 File Offset: 0x00116928
		public static LocalizedString DuplicateColumnIndexes(string columnNameA, string columnNameB)
		{
			return new LocalizedString("DuplicateColumnIndexes", "Ex9939EC", false, true, Strings.ResourceManager, new object[]
			{
				columnNameA,
				columnNameB
			});
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06004046 RID: 16454 RVA: 0x0011875B File Offset: 0x0011695B
		public static LocalizedString LatencyComponentProcessingSchedulerScoped
		{
			get
			{
				return new LocalizedString("LatencyComponentProcessingSchedulerScoped", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x00118779 File Offset: 0x00116979
		public static LocalizedString LatencyComponentSmtpReceiveOnRcpt2Command
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpReceiveOnRcpt2Command", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x06004048 RID: 16456 RVA: 0x00118797 File Offset: 0x00116997
		public static LocalizedString LatencyComponentStoreDriverOnPromotedMessage
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverOnPromotedMessage", "Ex0F574F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x06004049 RID: 16457 RVA: 0x001187B5 File Offset: 0x001169B5
		public static LocalizedString PoisonMessageRegistryAccessFailed
		{
			get
			{
				return new LocalizedString("PoisonMessageRegistryAccessFailed", "ExD03EAE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x0600404A RID: 16458 RVA: 0x001187D3 File Offset: 0x001169D3
		public static LocalizedString HighResourceUses
		{
			get
			{
				return new LocalizedString("HighResourceUses", "Ex53218A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x0600404B RID: 16459 RVA: 0x001187F1 File Offset: 0x001169F1
		public static LocalizedString EnvelopRecipientDisposed
		{
			get
			{
				return new LocalizedString("EnvelopRecipientDisposed", "Ex65E8AE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x0600404C RID: 16460 RVA: 0x0011880F File Offset: 0x00116A0F
		public static LocalizedString SystemMemory
		{
			get
			{
				return new LocalizedString("SystemMemory", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x0600404D RID: 16461 RVA: 0x0011882D File Offset: 0x00116A2D
		public static LocalizedString ReadOrgContainerFailed
		{
			get
			{
				return new LocalizedString("ReadOrgContainerFailed", "ExDB3FBD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x0600404E RID: 16462 RVA: 0x0011884B File Offset: 0x00116A4B
		public static LocalizedString LatencyComponentStoreDriverOnCreatedMessage
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverOnCreatedMessage", "ExAFC8A5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x0600404F RID: 16463 RVA: 0x00118869 File Offset: 0x00116A69
		public static LocalizedString CategorizerMaxConfigLoadRetriesReached
		{
			get
			{
				return new LocalizedString("CategorizerMaxConfigLoadRetriesReached", "Ex22D247", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06004050 RID: 16464 RVA: 0x00118887 File Offset: 0x00116A87
		public static LocalizedString LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreOpenSession
		{
			get
			{
				return new LocalizedString("LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreOpenSession", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06004051 RID: 16465 RVA: 0x001188A5 File Offset: 0x00116AA5
		public static LocalizedString RoutingLocalServerIsNotBridgehead
		{
			get
			{
				return new LocalizedString("RoutingLocalServerIsNotBridgehead", "Ex225794", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x001188C4 File Offset: 0x00116AC4
		public static LocalizedString FilePathOnLockedVolume(string filePath, int retrySeconds, int retryCount, int maxRetryCount)
		{
			return new LocalizedString("FilePathOnLockedVolume", "", false, false, Strings.ResourceManager, new object[]
			{
				filePath,
				retrySeconds,
				retryCount,
				maxRetryCount
			});
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x00118910 File Offset: 0x00116B10
		public static LocalizedString BitlockerQueryFailed(string filePath, string exception, int retrySeconds, int retryCount, int maxRetryCount)
		{
			return new LocalizedString("BitlockerQueryFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				filePath,
				exception,
				retrySeconds,
				retryCount,
				maxRetryCount
			});
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06004054 RID: 16468 RVA: 0x0011895F File Offset: 0x00116B5F
		public static LocalizedString LatencyComponentCategorizerOnCategorizedMessage
		{
			get
			{
				return new LocalizedString("LatencyComponentCategorizerOnCategorizedMessage", "ExF69D17", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x06004055 RID: 16469 RVA: 0x0011897D File Offset: 0x00116B7D
		public static LocalizedString LatencyComponentRmsAcquireB2BRac
		{
			get
			{
				return new LocalizedString("LatencyComponentRmsAcquireB2BRac", "Ex0ED294", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x06004056 RID: 16470 RVA: 0x0011899B File Offset: 0x00116B9B
		public static LocalizedString InternalDestinationInboundProxySendConnector
		{
			get
			{
				return new LocalizedString("InternalDestinationInboundProxySendConnector", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06004057 RID: 16471 RVA: 0x001189B9 File Offset: 0x00116BB9
		public static LocalizedString LatencyComponentDeliveryQueueMailboxMaxConcurrentMessageSizeLimitExceeded
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryQueueMailboxMaxConcurrentMessageSizeLimitExceeded", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06004058 RID: 16472 RVA: 0x001189D7 File Offset: 0x00116BD7
		public static LocalizedString LatencyComponentStoreDriverDeliveryRpc
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverDeliveryRpc", "Ex2DB2CA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06004059 RID: 16473 RVA: 0x001189F5 File Offset: 0x00116BF5
		public static LocalizedString ConnectionInUse
		{
			get
			{
				return new LocalizedString("ConnectionInUse", "ExC13129", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x0600405A RID: 16474 RVA: 0x00118A13 File Offset: 0x00116C13
		public static LocalizedString LatencyComponentMailSubmissionService
		{
			get
			{
				return new LocalizedString("LatencyComponentMailSubmissionService", "ExF3DDF1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x0600405B RID: 16475 RVA: 0x00118A31 File Offset: 0x00116C31
		public static LocalizedString NonePriority
		{
			get
			{
				return new LocalizedString("NonePriority", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x00118A50 File Offset: 0x00116C50
		public static LocalizedString InvalidSmtpAddress(string address)
		{
			return new LocalizedString("InvalidSmtpAddress", "Ex8C830F", false, true, Strings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x0600405D RID: 16477 RVA: 0x00118A7F File Offset: 0x00116C7F
		public static LocalizedString LatencyComponentRmsAcquireTemplateInfo
		{
			get
			{
				return new LocalizedString("LatencyComponentRmsAcquireTemplateInfo", "ExD68E65", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x00118A9D File Offset: 0x00116C9D
		public static LocalizedString LatencyComponentContentAggregation
		{
			get
			{
				return new LocalizedString("LatencyComponentContentAggregation", "Ex30AF3D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x0600405F RID: 16479 RVA: 0x00118ABB File Offset: 0x00116CBB
		public static LocalizedString LatencyComponentMailSubmissionServiceShadowResubmitDecision
		{
			get
			{
				return new LocalizedString("LatencyComponentMailSubmissionServiceShadowResubmitDecision", "Ex79D02D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06004060 RID: 16480 RVA: 0x00118AD9 File Offset: 0x00116CD9
		public static LocalizedString LatencyComponentContentAggregationMailItemCommit
		{
			get
			{
				return new LocalizedString("LatencyComponentContentAggregationMailItemCommit", "Ex1030A7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x06004061 RID: 16481 RVA: 0x00118AF7 File Offset: 0x00116CF7
		public static LocalizedString DatabaseRecoveryActionMove
		{
			get
			{
				return new LocalizedString("DatabaseRecoveryActionMove", "Ex3E9869", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06004062 RID: 16482 RVA: 0x00118B15 File Offset: 0x00116D15
		public static LocalizedString ReadTransportServerConfigFailed
		{
			get
			{
				return new LocalizedString("ReadTransportServerConfigFailed", "Ex9C96A5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06004063 RID: 16483 RVA: 0x00118B33 File Offset: 0x00116D33
		public static LocalizedString LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionHubSelector
		{
			get
			{
				return new LocalizedString("LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionHubSelector", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06004064 RID: 16484 RVA: 0x00118B51 File Offset: 0x00116D51
		public static LocalizedString MessageTrackingConfigNotFound
		{
			get
			{
				return new LocalizedString("MessageTrackingConfigNotFound", "ExEB8A78", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x00118B70 File Offset: 0x00116D70
		public static LocalizedString RMSTemplateNotFound(Guid templateId)
		{
			return new LocalizedString("RMSTemplateNotFound", "Ex4060AE", false, true, Strings.ResourceManager, new object[]
			{
				templateId
			});
		}

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06004066 RID: 16486 RVA: 0x00118BA4 File Offset: 0x00116DA4
		public static LocalizedString LatencyComponentDeliveryQueueInternal
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryQueueInternal", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06004067 RID: 16487 RVA: 0x00118BC2 File Offset: 0x00116DC2
		public static LocalizedString LatencyComponentDeliveryQueueMailboxMailboxServerOffline
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryQueueMailboxMailboxServerOffline", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06004068 RID: 16488 RVA: 0x00118BE0 File Offset: 0x00116DE0
		public static LocalizedString PrivateBytesResource
		{
			get
			{
				return new LocalizedString("PrivateBytesResource", "ExF17246", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x00118C00 File Offset: 0x00116E00
		public static LocalizedString VersionBuckets(string databasePath)
		{
			return new LocalizedString("VersionBuckets", "", false, false, Strings.ResourceManager, new object[]
			{
				databasePath
			});
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x0600406A RID: 16490 RVA: 0x00118C2F File Offset: 0x00116E2F
		public static LocalizedString LatencyComponentDeliveryQueueMailboxMailboxDatabaseOffline
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryQueueMailboxMailboxDatabaseOffline", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x00118C50 File Offset: 0x00116E50
		public static LocalizedString CannotModifyCompletedRequest(long requestId)
		{
			return new LocalizedString("CannotModifyCompletedRequest", "", false, false, Strings.ResourceManager, new object[]
			{
				requestId
			});
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x0600406C RID: 16492 RVA: 0x00118C84 File Offset: 0x00116E84
		public static LocalizedString RoutingMaxConfigLoadRetriesReached
		{
			get
			{
				return new LocalizedString("RoutingMaxConfigLoadRetriesReached", "Ex7D039C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x0600406D RID: 16493 RVA: 0x00118CA2 File Offset: 0x00116EA2
		public static LocalizedString LatencyComponentRmsAcquireServerLicensingMexData
		{
			get
			{
				return new LocalizedString("LatencyComponentRmsAcquireServerLicensingMexData", "ExD58728", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x0600406E RID: 16494 RVA: 0x00118CC0 File Offset: 0x00116EC0
		public static LocalizedString LatencyComponentDeliveryQueueMailboxRecipientThreadLimitExceeded
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryQueueMailboxRecipientThreadLimitExceeded", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x0600406F RID: 16495 RVA: 0x00118CDE File Offset: 0x00116EDE
		public static LocalizedString SmtpReceiveParserNegativeBytes
		{
			get
			{
				return new LocalizedString("SmtpReceiveParserNegativeBytes", "Ex3B35E5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06004070 RID: 16496 RVA: 0x00118CFC File Offset: 0x00116EFC
		public static LocalizedString InvalidRoleChange
		{
			get
			{
				return new LocalizedString("InvalidRoleChange", "Ex19D8AA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x00118D1C File Offset: 0x00116F1C
		public static LocalizedString DefaultAuthoritativeDomainNotFound(OrganizationId orgId)
		{
			return new LocalizedString("DefaultAuthoritativeDomainNotFound", "ExF5500C", false, true, Strings.ResourceManager, new object[]
			{
				orgId
			});
		}

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x00118D4B File Offset: 0x00116F4B
		public static LocalizedString LatencyComponentPoisonQueue
		{
			get
			{
				return new LocalizedString("LatencyComponentPoisonQueue", "ExC760D5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06004073 RID: 16499 RVA: 0x00118D69 File Offset: 0x00116F69
		public static LocalizedString BootScannerComponent
		{
			get
			{
				return new LocalizedString("BootScannerComponent", "Ex7FD341", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06004074 RID: 16500 RVA: 0x00118D87 File Offset: 0x00116F87
		public static LocalizedString NotInTransaction
		{
			get
			{
				return new LocalizedString("NotInTransaction", "Ex28A52B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06004075 RID: 16501 RVA: 0x00118DA5 File Offset: 0x00116FA5
		public static LocalizedString LatencyComponentSmtpReceiveOnRcptCommand
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpReceiveOnRcptCommand", "ExB873C3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06004076 RID: 16502 RVA: 0x00118DC3 File Offset: 0x00116FC3
		public static LocalizedString BodyReadFailed
		{
			get
			{
				return new LocalizedString("BodyReadFailed", "Ex6EC7B9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06004077 RID: 16503 RVA: 0x00118DE1 File Offset: 0x00116FE1
		public static LocalizedString TextConvertersFailed
		{
			get
			{
				return new LocalizedString("TextConvertersFailed", "Ex38BA0F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06004078 RID: 16504 RVA: 0x00118DFF File Offset: 0x00116FFF
		public static LocalizedString RoutingNoRoutingGroups
		{
			get
			{
				return new LocalizedString("RoutingNoRoutingGroups", "Ex733C6D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06004079 RID: 16505 RVA: 0x00118E1D File Offset: 0x0011701D
		public static LocalizedString LatencyComponentStoreDriverDeliveryMessageConcurrency
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverDeliveryMessageConcurrency", "Ex5E0FC5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x0600407A RID: 16506 RVA: 0x00118E3B File Offset: 0x0011703B
		public static LocalizedString Submission
		{
			get
			{
				return new LocalizedString("Submission", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x0600407B RID: 16507 RVA: 0x00118E59 File Offset: 0x00117059
		public static LocalizedString ReadingADConfigFailed
		{
			get
			{
				return new LocalizedString("ReadingADConfigFailed", "Ex4986D4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600407C RID: 16508 RVA: 0x00118E78 File Offset: 0x00117078
		public static LocalizedString TemporaryStorageResource(string tempPath)
		{
			return new LocalizedString("TemporaryStorageResource", "", false, false, Strings.ResourceManager, new object[]
			{
				tempPath
			});
		}

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x0600407D RID: 16509 RVA: 0x00118EA7 File Offset: 0x001170A7
		public static LocalizedString LatencyComponentTooManyComponents
		{
			get
			{
				return new LocalizedString("LatencyComponentTooManyComponents", "Ex261973", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x0600407E RID: 16510 RVA: 0x00118EC5 File Offset: 0x001170C5
		public static LocalizedString ReadTransportConfigConfigFailed
		{
			get
			{
				return new LocalizedString("ReadTransportConfigConfigFailed", "Ex96B07F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x00118EE4 File Offset: 0x001170E4
		public static LocalizedString SchemaRequiredColumnNotFound(string table, string columnName)
		{
			return new LocalizedString("SchemaRequiredColumnNotFound", "ExB91D51", false, true, Strings.ResourceManager, new object[]
			{
				table,
				columnName
			});
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06004080 RID: 16512 RVA: 0x00118F17 File Offset: 0x00117117
		public static LocalizedString InvalidMessageResubmissionState
		{
			get
			{
				return new LocalizedString("InvalidMessageResubmissionState", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06004081 RID: 16513 RVA: 0x00118F35 File Offset: 0x00117135
		public static LocalizedString LatencyComponentSmtpReceiveOnEndOfData
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpReceiveOnEndOfData", "Ex8D038F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x00118F54 File Offset: 0x00117154
		public static LocalizedString QueueLength(string queueName)
		{
			return new LocalizedString("QueueLength", "", false, false, Strings.ResourceManager, new object[]
			{
				queueName
			});
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06004083 RID: 16515 RVA: 0x00118F83 File Offset: 0x00117183
		public static LocalizedString InvalidTransportRole
		{
			get
			{
				return new LocalizedString("InvalidTransportRole", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x00118FA1 File Offset: 0x001171A1
		public static LocalizedString LatencyComponentReplay
		{
			get
			{
				return new LocalizedString("LatencyComponentReplay", "ExCD83D5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06004085 RID: 16517 RVA: 0x00118FBF File Offset: 0x001171BF
		public static LocalizedString CloneMoveDestination
		{
			get
			{
				return new LocalizedString("CloneMoveDestination", "Ex2A3AFF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x00118FDD File Offset: 0x001171DD
		public static LocalizedString LowRiskNonePriority
		{
			get
			{
				return new LocalizedString("LowRiskNonePriority", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06004087 RID: 16519 RVA: 0x00118FFB File Offset: 0x001171FB
		public static LocalizedString LatencyComponentCategorizerBifurcation
		{
			get
			{
				return new LocalizedString("LatencyComponentCategorizerBifurcation", "Ex4E745B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06004088 RID: 16520 RVA: 0x00119019 File Offset: 0x00117219
		public static LocalizedString SchemaInvalid
		{
			get
			{
				return new LocalizedString("SchemaInvalid", "Ex18CF4C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06004089 RID: 16521 RVA: 0x00119037 File Offset: 0x00117237
		public static LocalizedString QuoteNestLevel
		{
			get
			{
				return new LocalizedString("QuoteNestLevel", "Ex8DA874", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x0600408A RID: 16522 RVA: 0x00119055 File Offset: 0x00117255
		public static LocalizedString LatencyComponentUnknown
		{
			get
			{
				return new LocalizedString("LatencyComponentUnknown", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x0600408B RID: 16523 RVA: 0x00119073 File Offset: 0x00117273
		public static LocalizedString LatencyComponentTotal
		{
			get
			{
				return new LocalizedString("LatencyComponentTotal", "Ex2FE97B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x0600408C RID: 16524 RVA: 0x00119091 File Offset: 0x00117291
		public static LocalizedString LatencyComponentRmsAcquireLicense
		{
			get
			{
				return new LocalizedString("LatencyComponentRmsAcquireLicense", "ExF834A0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x0600408D RID: 16525 RVA: 0x001190AF File Offset: 0x001172AF
		public static LocalizedString InboundMailSubmissionFromReplayDirectoryComponent
		{
			get
			{
				return new LocalizedString("InboundMailSubmissionFromReplayDirectoryComponent", "ExA0D92A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x0600408E RID: 16526 RVA: 0x001190CD File Offset: 0x001172CD
		public static LocalizedString NoColumns
		{
			get
			{
				return new LocalizedString("NoColumns", "Ex432F52", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x0600408F RID: 16527 RVA: 0x001190EB File Offset: 0x001172EB
		public static LocalizedString LatencyComponentRmsAcquireClc
		{
			get
			{
				return new LocalizedString("LatencyComponentRmsAcquireClc", "Ex983A86", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06004090 RID: 16528 RVA: 0x00119109 File Offset: 0x00117309
		public static LocalizedString CloneMoveComplete
		{
			get
			{
				return new LocalizedString("CloneMoveComplete", "ExC21EF2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x00119128 File Offset: 0x00117328
		public static LocalizedString DatabaseIsNotMovable(string sourcePath, string destPath)
		{
			return new LocalizedString("DatabaseIsNotMovable", "ExC75FF5", false, true, Strings.ResourceManager, new object[]
			{
				sourcePath,
				destPath
			});
		}

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06004092 RID: 16530 RVA: 0x0011915B File Offset: 0x0011735B
		public static LocalizedString LatencyComponentQuarantineReleaseOrReport
		{
			get
			{
				return new LocalizedString("LatencyComponentQuarantineReleaseOrReport", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06004093 RID: 16531 RVA: 0x00119179 File Offset: 0x00117379
		public static LocalizedString LatencyComponentSubmissionAssistant
		{
			get
			{
				return new LocalizedString("LatencyComponentSubmissionAssistant", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06004094 RID: 16532 RVA: 0x00119197 File Offset: 0x00117397
		public static LocalizedString DumpsterJobResponseSuccess
		{
			get
			{
				return new LocalizedString("DumpsterJobResponseSuccess", "Ex051B82", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x001191B8 File Offset: 0x001173B8
		public static LocalizedString ExtractNotAllowed(Uri url, string orgId)
		{
			return new LocalizedString("ExtractNotAllowed", "ExFA32C2", false, true, Strings.ResourceManager, new object[]
			{
				url,
				orgId
			});
		}

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06004096 RID: 16534 RVA: 0x001191EB File Offset: 0x001173EB
		public static LocalizedString AgentComponentFailed
		{
			get
			{
				return new LocalizedString("AgentComponentFailed", "Ex76E2B6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x0011920C File Offset: 0x0011740C
		public static LocalizedString CannotRemoveRequestInRunningState(long requestId)
		{
			return new LocalizedString("CannotRemoveRequestInRunningState", "", false, false, Strings.ResourceManager, new object[]
			{
				requestId
			});
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06004098 RID: 16536 RVA: 0x00119240 File Offset: 0x00117440
		public static LocalizedString ClientProxySendConnector
		{
			get
			{
				return new LocalizedString("ClientProxySendConnector", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06004099 RID: 16537 RVA: 0x0011925E File Offset: 0x0011745E
		public static LocalizedString Basic
		{
			get
			{
				return new LocalizedString("Basic", "ExA01C4F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x0600409A RID: 16538 RVA: 0x0011927C File Offset: 0x0011747C
		public static LocalizedString LatencyComponentCategorizerOnSubmittedMessage
		{
			get
			{
				return new LocalizedString("LatencyComponentCategorizerOnSubmittedMessage", "Ex952BE3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x0600409B RID: 16539 RVA: 0x0011929A File Offset: 0x0011749A
		public static LocalizedString NormalRiskNormalPriority
		{
			get
			{
				return new LocalizedString("NormalRiskNormalPriority", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x0600409C RID: 16540 RVA: 0x001192B8 File Offset: 0x001174B8
		public static LocalizedString LatencyComponentSmtpReceiveCommit
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpReceiveCommit", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x0600409D RID: 16541 RVA: 0x001192D6 File Offset: 0x001174D6
		public static LocalizedString RoutingNoLocalServer
		{
			get
			{
				return new LocalizedString("RoutingNoLocalServer", "Ex42844E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x0600409E RID: 16542 RVA: 0x001192F4 File Offset: 0x001174F4
		public static LocalizedString SecureMailSecondLayerMustBeEnveloped
		{
			get
			{
				return new LocalizedString("SecureMailSecondLayerMustBeEnveloped", "Ex200777", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x00119314 File Offset: 0x00117514
		public static LocalizedString DatabaseAndDatabaseLogMoved(string databasePath, string databaseMovePath, string databaseLogPath, string databaseLogMovePath)
		{
			return new LocalizedString("DatabaseAndDatabaseLogMoved", "", false, false, Strings.ResourceManager, new object[]
			{
				databasePath,
				databaseMovePath,
				databaseLogPath,
				databaseLogMovePath
			});
		}

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x060040A0 RID: 16544 RVA: 0x0011934F File Offset: 0x0011754F
		public static LocalizedString MailItemDeferred
		{
			get
			{
				return new LocalizedString("MailItemDeferred", "Ex371895", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x060040A1 RID: 16545 RVA: 0x0011936D File Offset: 0x0011756D
		public static LocalizedString InboundMailSubmissionFromMailboxComponent
		{
			get
			{
				return new LocalizedString("InboundMailSubmissionFromMailboxComponent", "ExA8CDDA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x060040A2 RID: 16546 RVA: 0x0011938B File Offset: 0x0011758B
		public static LocalizedString AttachmentProtectionFailed
		{
			get
			{
				return new LocalizedString("AttachmentProtectionFailed", "ExEA40C1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x060040A3 RID: 16547 RVA: 0x001193A9 File Offset: 0x001175A9
		public static LocalizedString LatencyComponentSubmissionAssistantThrottling
		{
			get
			{
				return new LocalizedString("LatencyComponentSubmissionAssistantThrottling", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x001193C8 File Offset: 0x001175C8
		public static LocalizedString DiskFull(string path)
		{
			return new LocalizedString("DiskFull", "ExFECC85", false, true, Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x001193F8 File Offset: 0x001175F8
		public static LocalizedString DatabaseResource(string databasePath)
		{
			return new LocalizedString("DatabaseResource", "Ex6ADCE8", false, true, Strings.ResourceManager, new object[]
			{
				databasePath
			});
		}

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x060040A6 RID: 16550 RVA: 0x00119427 File Offset: 0x00117627
		public static LocalizedString LatencyComponentCategorizerLocking
		{
			get
			{
				return new LocalizedString("LatencyComponentCategorizerLocking", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x060040A7 RID: 16551 RVA: 0x00119445 File Offset: 0x00117645
		public static LocalizedString ValueNull
		{
			get
			{
				return new LocalizedString("ValueNull", "ExC68788", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x00119464 File Offset: 0x00117664
		public static LocalizedString DatabaseSchemaNotSupported(string databaseName)
		{
			return new LocalizedString("DatabaseSchemaNotSupported", "", false, false, Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x060040A9 RID: 16553 RVA: 0x00119493 File Offset: 0x00117693
		public static LocalizedString TooManyAgents
		{
			get
			{
				return new LocalizedString("TooManyAgents", "ExB98825", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x001194B4 File Offset: 0x001176B4
		public static LocalizedString ColumnAccessInvalid(string columnName)
		{
			return new LocalizedString("ColumnAccessInvalid", "ExF3238F", false, true, Strings.ResourceManager, new object[]
			{
				columnName
			});
		}

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x060040AB RID: 16555 RVA: 0x001194E3 File Offset: 0x001176E3
		public static LocalizedString DumpsterJobStatusCompleted
		{
			get
			{
				return new LocalizedString("DumpsterJobStatusCompleted", "Ex2E880E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x060040AC RID: 16556 RVA: 0x00119501 File Offset: 0x00117701
		public static LocalizedString LatencyComponentStoreDriverOnDeliveredMessage
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverOnDeliveredMessage", "ExB82838", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x060040AD RID: 16557 RVA: 0x0011951F File Offset: 0x0011771F
		public static LocalizedString LatencyComponentRmsAcquireB2BLicense
		{
			get
			{
				return new LocalizedString("LatencyComponentRmsAcquireB2BLicense", "Ex281C09", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x060040AE RID: 16558 RVA: 0x0011953D File Offset: 0x0011773D
		public static LocalizedString LatencyComponentDeliveryQueueMailboxMapiExceptionLockViolation
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryQueueMailboxMapiExceptionLockViolation", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x060040AF RID: 16559 RVA: 0x0011955B File Offset: 0x0011775B
		public static LocalizedString Medium
		{
			get
			{
				return new LocalizedString("Medium", "ExFE566F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x060040B0 RID: 16560 RVA: 0x00119579 File Offset: 0x00117779
		public static LocalizedString NotBufferedStream
		{
			get
			{
				return new LocalizedString("NotBufferedStream", "Ex623A85", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x00119598 File Offset: 0x00117798
		public static LocalizedString DatabaseMoved(string databasePath, string movePath)
		{
			return new LocalizedString("DatabaseMoved", "", false, false, Strings.ResourceManager, new object[]
			{
				databasePath,
				movePath
			});
		}

		// Token: 0x060040B2 RID: 16562 RVA: 0x001195CC File Offset: 0x001177CC
		public static LocalizedString IndexOutOfBounds(int index, int count)
		{
			return new LocalizedString("IndexOutOfBounds", "Ex3A8B25", false, true, Strings.ResourceManager, new object[]
			{
				index,
				count
			});
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x060040B3 RID: 16563 RVA: 0x00119609 File Offset: 0x00117809
		public static LocalizedString IncorrectBaseStream
		{
			get
			{
				return new LocalizedString("IncorrectBaseStream", "Ex697148", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x060040B4 RID: 16564 RVA: 0x00119627 File Offset: 0x00117827
		public static LocalizedString LatencyComponentProcess
		{
			get
			{
				return new LocalizedString("LatencyComponentProcess", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x060040B5 RID: 16565 RVA: 0x00119645 File Offset: 0x00117845
		public static LocalizedString CommitMailFailed
		{
			get
			{
				return new LocalizedString("CommitMailFailed", "ExD1D56C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x060040B6 RID: 16566 RVA: 0x00119663 File Offset: 0x00117863
		public static LocalizedString NonAsciiData
		{
			get
			{
				return new LocalizedString("NonAsciiData", "ExE35D2D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x00119684 File Offset: 0x00117884
		public static LocalizedString OutboundConnectorNotFound(string name, OrganizationId orgId)
		{
			return new LocalizedString("OutboundConnectorNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				name,
				orgId
			});
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x001196B8 File Offset: 0x001178B8
		public static LocalizedString SchemaVersion(long expected, long found)
		{
			return new LocalizedString("SchemaVersion", "Ex8ED756", false, true, Strings.ResourceManager, new object[]
			{
				expected,
				found
			});
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x060040B9 RID: 16569 RVA: 0x001196F5 File Offset: 0x001178F5
		public static LocalizedString SeekBarred
		{
			get
			{
				return new LocalizedString("SeekBarred", "Ex39B2E6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x060040BA RID: 16570 RVA: 0x00119713 File Offset: 0x00117913
		public static LocalizedString RoutingLocalRgNotSet
		{
			get
			{
				return new LocalizedString("RoutingLocalRgNotSet", "Ex72888D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x00119734 File Offset: 0x00117934
		public static LocalizedString TransportComponentLoadFailedWithName(string componentName)
		{
			return new LocalizedString("TransportComponentLoadFailedWithName", "", false, false, Strings.ResourceManager, new object[]
			{
				componentName
			});
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x060040BC RID: 16572 RVA: 0x00119763 File Offset: 0x00117963
		public static LocalizedString MessagingDatabaseInstanceName
		{
			get
			{
				return new LocalizedString("MessagingDatabaseInstanceName", "Ex235DD2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x060040BD RID: 16573 RVA: 0x00119781 File Offset: 0x00117981
		public static LocalizedString LatencyComponentUnreachableQueue
		{
			get
			{
				return new LocalizedString("LatencyComponentUnreachableQueue", "ExE09761", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x060040BE RID: 16574 RVA: 0x0011979F File Offset: 0x0011799F
		public static LocalizedString LatencyComponentStoreDriverDelivery
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverDelivery", "ExDE552B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x060040BF RID: 16575 RVA: 0x001197BD File Offset: 0x001179BD
		public static LocalizedString DatabaseStillInUse
		{
			get
			{
				return new LocalizedString("DatabaseStillInUse", "ExAEEC70", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x060040C0 RID: 16576 RVA: 0x001197DB File Offset: 0x001179DB
		public static LocalizedString LatencyComponentDeferral
		{
			get
			{
				return new LocalizedString("LatencyComponentDeferral", "ExAA114E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x001197FC File Offset: 0x001179FC
		public static LocalizedString LatencyComponentMExReserved(int index)
		{
			return new LocalizedString("LatencyComponentMExReserved", "Ex899D35", false, true, Strings.ResourceManager, new object[]
			{
				index
			});
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x00119830 File Offset: 0x00117A30
		public static LocalizedString ValueIsTooLarge(int length, int maxLength)
		{
			return new LocalizedString("ValueIsTooLarge", "ExBB4978", false, true, Strings.ResourceManager, new object[]
			{
				length,
				maxLength
			});
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x0011986D File Offset: 0x00117A6D
		public static LocalizedString DumpsterJobResponseRetryLater
		{
			get
			{
				return new LocalizedString("DumpsterJobResponseRetryLater", "Ex9C089F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x060040C4 RID: 16580 RVA: 0x0011988B File Offset: 0x00117A8B
		public static LocalizedString RoutingNoLocalAdSite
		{
			get
			{
				return new LocalizedString("RoutingNoLocalAdSite", "Ex2DC77D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x060040C5 RID: 16581 RVA: 0x001198A9 File Offset: 0x00117AA9
		public static LocalizedString LatencyComponentRmsAcquireTemplates
		{
			get
			{
				return new LocalizedString("LatencyComponentRmsAcquireTemplates", "Ex7EB976", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x060040C6 RID: 16582 RVA: 0x001198C7 File Offset: 0x00117AC7
		public static LocalizedString InvalidRoutingOverrideEvent
		{
			get
			{
				return new LocalizedString("InvalidRoutingOverrideEvent", "ExDD58BB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x060040C7 RID: 16583 RVA: 0x001198E5 File Offset: 0x00117AE5
		public static LocalizedString GetSclThresholdDefaultValueOutOfRange
		{
			get
			{
				return new LocalizedString("GetSclThresholdDefaultValueOutOfRange", "Ex7F4CB6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x060040C8 RID: 16584 RVA: 0x00119903 File Offset: 0x00117B03
		public static LocalizedString LatencyComponentSmtpReceiveDataInternal
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpReceiveDataInternal", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x060040C9 RID: 16585 RVA: 0x00119921 File Offset: 0x00117B21
		public static LocalizedString InvalidTenantLicensePair
		{
			get
			{
				return new LocalizedString("InvalidTenantLicensePair", "Ex698AF4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x060040CA RID: 16586 RVA: 0x0011993F File Offset: 0x00117B3F
		public static LocalizedString ColumnIndexesMustBeSequential
		{
			get
			{
				return new LocalizedString("ColumnIndexesMustBeSequential", "Ex61D691", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x060040CB RID: 16587 RVA: 0x0011995D File Offset: 0x00117B5D
		public static LocalizedString LatencyComponentNonSmtpGateway
		{
			get
			{
				return new LocalizedString("LatencyComponentNonSmtpGateway", "Ex6C9727", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x060040CC RID: 16588 RVA: 0x0011997B File Offset: 0x00117B7B
		public static LocalizedString LatencyComponentDeliveryAgentOnDeliverMailItem
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryAgentOnDeliverMailItem", "ExA2B488", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x060040CD RID: 16589 RVA: 0x00119999 File Offset: 0x00117B99
		public static LocalizedString ShadowRedundancyComponentBanner
		{
			get
			{
				return new LocalizedString("ShadowRedundancyComponentBanner", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x060040CE RID: 16590 RVA: 0x001199B7 File Offset: 0x00117BB7
		public static LocalizedString CloneMoveSourceModified
		{
			get
			{
				return new LocalizedString("CloneMoveSourceModified", "Ex67EFA9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x060040CF RID: 16591 RVA: 0x001199D5 File Offset: 0x00117BD5
		public static LocalizedString DatabaseRecoveryActionNone
		{
			get
			{
				return new LocalizedString("DatabaseRecoveryActionNone", "Ex074D46", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x060040D0 RID: 16592 RVA: 0x001199F3 File Offset: 0x00117BF3
		public static LocalizedString LatencyComponentMailSubmissionServiceThrottling
		{
			get
			{
				return new LocalizedString("LatencyComponentMailSubmissionServiceThrottling", "ExAA6A02", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x00119A14 File Offset: 0x00117C14
		public static LocalizedString DataBaseInUse(string databaseName)
		{
			return new LocalizedString("DataBaseInUse", "", false, false, Strings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x00119A44 File Offset: 0x00117C44
		public static LocalizedString SchemaTypeMismatch(JET_coltyp expected, JET_coltyp got)
		{
			return new LocalizedString("SchemaTypeMismatch", "Ex6DC38A", false, true, Strings.ResourceManager, new object[]
			{
				expected,
				got
			});
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x060040D3 RID: 16595 RVA: 0x00119A81 File Offset: 0x00117C81
		public static LocalizedString LatencyComponentDsnGenerator
		{
			get
			{
				return new LocalizedString("LatencyComponentDsnGenerator", "ExCD3EC7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x060040D4 RID: 16596 RVA: 0x00119A9F File Offset: 0x00117C9F
		public static LocalizedString RowDeleted
		{
			get
			{
				return new LocalizedString("RowDeleted", "Ex699AF2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x060040D5 RID: 16597 RVA: 0x00119ABD File Offset: 0x00117CBD
		public static LocalizedString LatencyComponentCategorizerContentConversion
		{
			get
			{
				return new LocalizedString("LatencyComponentCategorizerContentConversion", "ExC970B7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x00119ADB File Offset: 0x00117CDB
		public static LocalizedString LatencyComponentExternalServers
		{
			get
			{
				return new LocalizedString("LatencyComponentExternalServers", "ExDEB5BD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x00119AFC File Offset: 0x00117CFC
		public static LocalizedString UsedDiskSpaceResource(string tempPath)
		{
			return new LocalizedString("UsedDiskSpaceResource", "", false, false, Strings.ResourceManager, new object[]
			{
				tempPath
			});
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x060040D8 RID: 16600 RVA: 0x00119B2B File Offset: 0x00117D2B
		public static LocalizedString ReadMicrosoftExchangeRecipientFailed
		{
			get
			{
				return new LocalizedString("ReadMicrosoftExchangeRecipientFailed", "ExE36E8E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x060040D9 RID: 16601 RVA: 0x00119B49 File Offset: 0x00117D49
		public static LocalizedString SeekGeneralFailure
		{
			get
			{
				return new LocalizedString("SeekGeneralFailure", "Ex7CBFF4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x060040DA RID: 16602 RVA: 0x00119B67 File Offset: 0x00117D67
		public static LocalizedString LatencyComponentCategorizerRouting
		{
			get
			{
				return new LocalizedString("LatencyComponentCategorizerRouting", "Ex2ACB31", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x060040DB RID: 16603 RVA: 0x00119B85 File Offset: 0x00117D85
		public static LocalizedString LatencyComponentOriginalMailDsn
		{
			get
			{
				return new LocalizedString("LatencyComponentOriginalMailDsn", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x060040DC RID: 16604 RVA: 0x00119BA3 File Offset: 0x00117DA3
		public static LocalizedString LatencyComponentPickup
		{
			get
			{
				return new LocalizedString("LatencyComponentPickup", "Ex666A76", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x060040DD RID: 16605 RVA: 0x00119BC1 File Offset: 0x00117DC1
		public static LocalizedString LowRisk
		{
			get
			{
				return new LocalizedString("LowRisk", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x060040DE RID: 16606 RVA: 0x00119BDF File Offset: 0x00117DDF
		public static LocalizedString LatencyComponentDeliveryAgentOnOpenConnection
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryAgentOnOpenConnection", "Ex2B96CC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x00119C00 File Offset: 0x00117E00
		public static LocalizedString ConfigurationLoaderFailed(string componentName)
		{
			return new LocalizedString("ConfigurationLoaderFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				componentName
			});
		}

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x060040E0 RID: 16608 RVA: 0x00119C2F File Offset: 0x00117E2F
		public static LocalizedString LatencyComponentCategorizerOnRoutedMessage
		{
			get
			{
				return new LocalizedString("LatencyComponentCategorizerOnRoutedMessage", "Ex0FC763", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x060040E1 RID: 16609 RVA: 0x00119C4D File Offset: 0x00117E4D
		public static LocalizedString CategorizerConfigValidationFailed
		{
			get
			{
				return new LocalizedString("CategorizerConfigValidationFailed", "ExD59195", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x060040E2 RID: 16610 RVA: 0x00119C6B File Offset: 0x00117E6B
		public static LocalizedString LatencyComponentNone
		{
			get
			{
				return new LocalizedString("LatencyComponentNone", "Ex3E6FB9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x060040E3 RID: 16611 RVA: 0x00119C89 File Offset: 0x00117E89
		public static LocalizedString LatencyComponentDeliveryQueueLocking
		{
			get
			{
				return new LocalizedString("LatencyComponentDeliveryQueueLocking", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x060040E4 RID: 16612 RVA: 0x00119CA7 File Offset: 0x00117EA7
		public static LocalizedString LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionAD
		{
			get
			{
				return new LocalizedString("LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionAD", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x060040E5 RID: 16613 RVA: 0x00119CC5 File Offset: 0x00117EC5
		public static LocalizedString MessageResubmissionComponentBanner
		{
			get
			{
				return new LocalizedString("MessageResubmissionComponentBanner", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x060040E6 RID: 16614 RVA: 0x00119CE3 File Offset: 0x00117EE3
		public static LocalizedString TotalExcludingPriorityNone
		{
			get
			{
				return new LocalizedString("TotalExcludingPriorityNone", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x060040E7 RID: 16615 RVA: 0x00119D01 File Offset: 0x00117F01
		public static LocalizedString CloneMoveSourceNull
		{
			get
			{
				return new LocalizedString("CloneMoveSourceNull", "Ex1A2B8A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x060040E8 RID: 16616 RVA: 0x00119D1F File Offset: 0x00117F1F
		public static LocalizedString LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionSmtp
		{
			get
			{
				return new LocalizedString("LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionSmtp", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x060040E9 RID: 16617 RVA: 0x00119D3D File Offset: 0x00117F3D
		public static LocalizedString LatencyComponentAgent
		{
			get
			{
				return new LocalizedString("LatencyComponentAgent", "Ex985392", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x060040EA RID: 16618 RVA: 0x00119D5B File Offset: 0x00117F5B
		public static LocalizedString InboundMailSubmissionFromHubsComponent
		{
			get
			{
				return new LocalizedString("InboundMailSubmissionFromHubsComponent", "ExFBCBCC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x060040EB RID: 16619 RVA: 0x00119D79 File Offset: 0x00117F79
		public static LocalizedString AlreadyJoined
		{
			get
			{
				return new LocalizedString("AlreadyJoined", "ExA45BBD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x060040EC RID: 16620 RVA: 0x00119D97 File Offset: 0x00117F97
		public static LocalizedString LatencyComponentStoreDriverSubmissionRpc
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverSubmissionRpc", "ExA6DEA7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x060040ED RID: 16621 RVA: 0x00119DB5 File Offset: 0x00117FB5
		public static LocalizedString NormalAndLowRisk
		{
			get
			{
				return new LocalizedString("NormalAndLowRisk", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x060040EE RID: 16622 RVA: 0x00119DD3 File Offset: 0x00117FD3
		public static LocalizedString LatencyComponentMailboxTransportSubmissionStoreDriverSubmission
		{
			get
			{
				return new LocalizedString("LatencyComponentMailboxTransportSubmissionStoreDriverSubmission", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x060040EF RID: 16623 RVA: 0x00119DF1 File Offset: 0x00117FF1
		public static LocalizedString InvalidRoutingOverride
		{
			get
			{
				return new LocalizedString("InvalidRoutingOverride", "Ex451377", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x060040F0 RID: 16624 RVA: 0x00119E0F File Offset: 0x0011800F
		public static LocalizedString LatencyComponentSmtpReceiveOnDataCommand
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpReceiveOnDataCommand", "Ex9FD61C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x060040F1 RID: 16625 RVA: 0x00119E2D File Offset: 0x0011802D
		public static LocalizedString LowRiskNormalPriority
		{
			get
			{
				return new LocalizedString("LowRiskNormalPriority", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x060040F2 RID: 16626 RVA: 0x00119E4B File Offset: 0x0011804B
		public static LocalizedString AalCalClassificationDisplayName
		{
			get
			{
				return new LocalizedString("AalCalClassificationDisplayName", "Ex9AD315", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x060040F3 RID: 16627 RVA: 0x00119E69 File Offset: 0x00118069
		public static LocalizedString HighlyConfidential
		{
			get
			{
				return new LocalizedString("HighlyConfidential", "Ex670E75", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x060040F4 RID: 16628 RVA: 0x00119E87 File Offset: 0x00118087
		public static LocalizedString LatencyComponentStoreDriverOnInitializedMessage
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverOnInitializedMessage", "Ex5CD5C2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x060040F5 RID: 16629 RVA: 0x00119EA5 File Offset: 0x001180A5
		public static LocalizedString LatencyComponentStoreDriverDeliveryStore
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverDeliveryStore", "Ex2EDF67", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x060040F6 RID: 16630 RVA: 0x00119EC3 File Offset: 0x001180C3
		public static LocalizedString InvalidRowState
		{
			get
			{
				return new LocalizedString("InvalidRowState", "Ex83E4B9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x060040F7 RID: 16631 RVA: 0x00119EE1 File Offset: 0x001180E1
		public static LocalizedString LatencyComponentMailboxTransportSubmissionService
		{
			get
			{
				return new LocalizedString("LatencyComponentMailboxTransportSubmissionService", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x00119EFF File Offset: 0x001180FF
		public static LocalizedString LatencyComponentStoreDriverSubmissionAD
		{
			get
			{
				return new LocalizedString("LatencyComponentStoreDriverSubmissionAD", "Ex737FC0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x060040F9 RID: 16633 RVA: 0x00119F1D File Offset: 0x0011811D
		public static LocalizedString DumpsterJobStatusProcessing
		{
			get
			{
				return new LocalizedString("DumpsterJobStatusProcessing", "Ex1D5F76", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x00119F3C File Offset: 0x0011813C
		public static LocalizedString ResourcesInAboveNormalPressure(string resources)
		{
			return new LocalizedString("ResourcesInAboveNormalPressure", "ExD8F560", false, true, Strings.ResourceManager, new object[]
			{
				resources
			});
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x00119F6C File Offset: 0x0011816C
		public static LocalizedString ComponentsDisabledByBackPressure(string componentNames)
		{
			return new LocalizedString("ComponentsDisabledByBackPressure", "Ex099F3B", false, true, Strings.ResourceManager, new object[]
			{
				componentNames
			});
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x060040FC RID: 16636 RVA: 0x00119F9B File Offset: 0x0011819B
		public static LocalizedString RoutingNoLocalRgObject
		{
			get
			{
				return new LocalizedString("RoutingNoLocalRgObject", "Ex7F036B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x060040FD RID: 16637 RVA: 0x00119FB9 File Offset: 0x001181B9
		public static LocalizedString PendingTransactions
		{
			get
			{
				return new LocalizedString("PendingTransactions", "Ex8507C2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x060040FE RID: 16638 RVA: 0x00119FD7 File Offset: 0x001181D7
		public static LocalizedString TrailingEscape
		{
			get
			{
				return new LocalizedString("TrailingEscape", "ExDFC953", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x060040FF RID: 16639 RVA: 0x00119FF5 File Offset: 0x001181F5
		public static LocalizedString CloneMoveTargetNotNew
		{
			get
			{
				return new LocalizedString("CloneMoveTargetNotNew", "Ex79B83C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x06004100 RID: 16640 RVA: 0x0011A013 File Offset: 0x00118213
		public static LocalizedString LatencyComponentCategorizerResolver
		{
			get
			{
				return new LocalizedString("LatencyComponentCategorizerResolver", "Ex0AA974", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x0011A034 File Offset: 0x00118234
		public static LocalizedString InvalidColumn(string table, int index)
		{
			return new LocalizedString("InvalidColumn", "ExF9F0C7", false, true, Strings.ResourceManager, new object[]
			{
				table,
				index
			});
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x0011A06C File Offset: 0x0011826C
		public static LocalizedString SubmissionQueueUses(int pressure, string uses, int normal, int medium, int high)
		{
			return new LocalizedString("SubmissionQueueUses", "Ex757B0C", false, true, Strings.ResourceManager, new object[]
			{
				pressure,
				uses,
				normal,
				medium,
				high
			});
		}

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x06004103 RID: 16643 RVA: 0x0011A0C0 File Offset: 0x001182C0
		public static LocalizedString TransportComponentLoadFailed
		{
			get
			{
				return new LocalizedString("TransportComponentLoadFailed", "Ex9C6F9A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x06004104 RID: 16644 RVA: 0x0011A0DE File Offset: 0x001182DE
		public static LocalizedString LatencyComponentProcessingScheduler
		{
			get
			{
				return new LocalizedString("LatencyComponentProcessingScheduler", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x06004105 RID: 16645 RVA: 0x0011A0FC File Offset: 0x001182FC
		public static LocalizedString LatencyComponentSmtpSendConnect
		{
			get
			{
				return new LocalizedString("LatencyComponentSmtpSendConnect", "ExB63E7B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x06004106 RID: 16646 RVA: 0x0011A11A File Offset: 0x0011831A
		public static LocalizedString Minimum
		{
			get
			{
				return new LocalizedString("Minimum", "Ex6C6620", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x06004107 RID: 16647 RVA: 0x0011A138 File Offset: 0x00118338
		public static LocalizedString InboundMailSubmissionFromPickupDirectoryComponent
		{
			get
			{
				return new LocalizedString("InboundMailSubmissionFromPickupDirectoryComponent", "Ex52059E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x06004108 RID: 16648 RVA: 0x0011A156 File Offset: 0x00118356
		public static LocalizedString BreadCrumbSize
		{
			get
			{
				return new LocalizedString("BreadCrumbSize", "ExE83A07", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x06004109 RID: 16649 RVA: 0x0011A174 File Offset: 0x00118374
		public static LocalizedString CloneMoveSourceNotSaved
		{
			get
			{
				return new LocalizedString("CloneMoveSourceNotSaved", "Ex0910C2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x0600410A RID: 16650 RVA: 0x0011A192 File Offset: 0x00118392
		public static LocalizedString LatencyComponentShadowQueue
		{
			get
			{
				return new LocalizedString("LatencyComponentShadowQueue", "ExDA7A0F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x0011A1B0 File Offset: 0x001183B0
		public static LocalizedString RoutingIdenticalFqdns(string server1, string server2)
		{
			return new LocalizedString("RoutingIdenticalFqdns", "ExF53547", false, true, Strings.ResourceManager, new object[]
			{
				server1,
				server2
			});
		}

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x0600410C RID: 16652 RVA: 0x0011A1E3 File Offset: 0x001183E3
		public static LocalizedString InvalidCursorState
		{
			get
			{
				return new LocalizedString("InvalidCursorState", "ExECFD0B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x0600410D RID: 16653 RVA: 0x0011A201 File Offset: 0x00118401
		public static LocalizedString LatencyComponentUnderThreshold
		{
			get
			{
				return new LocalizedString("LatencyComponentUnderThreshold", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x0600410E RID: 16654 RVA: 0x0011A21F File Offset: 0x0011841F
		public static LocalizedString HighPriority
		{
			get
			{
				return new LocalizedString("HighPriority", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x0600410F RID: 16655 RVA: 0x0011A23D File Offset: 0x0011843D
		public static LocalizedString CircularClone
		{
			get
			{
				return new LocalizedString("CircularClone", "Ex3C545C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x06004110 RID: 16656 RVA: 0x0011A25B File Offset: 0x0011845B
		public static LocalizedString InvalidDeleteState
		{
			get
			{
				return new LocalizedString("InvalidDeleteState", "Ex1D9051", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x06004111 RID: 16657 RVA: 0x0011A279 File Offset: 0x00118479
		public static LocalizedString LatencyComponentExternalPartnerServers
		{
			get
			{
				return new LocalizedString("LatencyComponentExternalPartnerServers", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x06004112 RID: 16658 RVA: 0x0011A297 File Offset: 0x00118497
		public static LocalizedString MailboxProxySendConnector
		{
			get
			{
				return new LocalizedString("MailboxProxySendConnector", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x06004113 RID: 16659 RVA: 0x0011A2B5 File Offset: 0x001184B5
		public static LocalizedString ExternalDestinationOutboundProxySendConnector
		{
			get
			{
				return new LocalizedString("ExternalDestinationOutboundProxySendConnector", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x0011A2D4 File Offset: 0x001184D4
		public static LocalizedString NotEnoughRightsToReEncrypt(int rights)
		{
			return new LocalizedString("NotEnoughRightsToReEncrypt", "Ex12BA80", false, true, Strings.ResourceManager, new object[]
			{
				rights
			});
		}

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x06004115 RID: 16661 RVA: 0x0011A308 File Offset: 0x00118508
		public static LocalizedString InvalidTransportServerRole
		{
			get
			{
				return new LocalizedString("InvalidTransportServerRole", "Ex32892A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x06004116 RID: 16662 RVA: 0x0011A326 File Offset: 0x00118526
		public static LocalizedString IncorrectColumn
		{
			get
			{
				return new LocalizedString("IncorrectColumn", "ExCA9176", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x06004117 RID: 16663 RVA: 0x0011A344 File Offset: 0x00118544
		public static LocalizedString CountWrong
		{
			get
			{
				return new LocalizedString("CountWrong", "Ex463102", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x06004118 RID: 16664 RVA: 0x0011A362 File Offset: 0x00118562
		public static LocalizedString LatencyComponentRmsAcquirePreLicense
		{
			get
			{
				return new LocalizedString("LatencyComponentRmsAcquirePreLicense", "Ex99F176", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x06004119 RID: 16665 RVA: 0x0011A380 File Offset: 0x00118580
		public static LocalizedString SecureMailOuterLayerMustBeSigned
		{
			get
			{
				return new LocalizedString("SecureMailOuterLayerMustBeSigned", "Ex40FCCE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x0600411A RID: 16666 RVA: 0x0011A39E File Offset: 0x0011859E
		public static LocalizedString LatencyComponentMailboxMove
		{
			get
			{
				return new LocalizedString("LatencyComponentMailboxMove", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x0011A3BC File Offset: 0x001185BC
		public static LocalizedString VersionBucketUses(int pressure, string uses, int normal, int medium, int high)
		{
			return new LocalizedString("VersionBucketUses", "Ex12A0D5", false, true, Strings.ResourceManager, new object[]
			{
				pressure,
				uses,
				normal,
				medium,
				high
			});
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x0600411C RID: 16668 RVA: 0x0011A410 File Offset: 0x00118610
		public static LocalizedString InternalDestinationOutboundProxySendConnector
		{
			get
			{
				return new LocalizedString("InternalDestinationOutboundProxySendConnector", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x0600411D RID: 16669 RVA: 0x0011A42E File Offset: 0x0011862E
		public static LocalizedString IncorrectBrace
		{
			get
			{
				return new LocalizedString("IncorrectBrace", "Ex51FA89", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x0600411E RID: 16670 RVA: 0x0011A44C File Offset: 0x0011864C
		public static LocalizedString KeyLength
		{
			get
			{
				return new LocalizedString("KeyLength", "ExAF3CC0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x0600411F RID: 16671 RVA: 0x0011A46A File Offset: 0x0011866A
		public static LocalizedString InvalidRank
		{
			get
			{
				return new LocalizedString("InvalidRank", "Ex6A2384", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x0011A488 File Offset: 0x00118688
		public static LocalizedString EventAgentComponent(string eventName, string agentName)
		{
			return new LocalizedString("EventAgentComponent", "ExCF38A9", false, true, Strings.ResourceManager, new object[]
			{
				eventName,
				agentName
			});
		}

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06004121 RID: 16673 RVA: 0x0011A4BB File Offset: 0x001186BB
		public static LocalizedString MimeWriteStreamOpen
		{
			get
			{
				return new LocalizedString("MimeWriteStreamOpen", "ExCA0E3B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x0011A4DC File Offset: 0x001186DC
		public static LocalizedString DatabaseDeleted(string databasePath)
		{
			return new LocalizedString("DatabaseDeleted", "", false, false, Strings.ResourceManager, new object[]
			{
				databasePath
			});
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x0011A50C File Offset: 0x0011870C
		public static LocalizedString AalCalBanner(string aal, string cal, string mechanism)
		{
			return new LocalizedString("AalCalBanner", "Ex8BB6D1", false, true, Strings.ResourceManager, new object[]
			{
				aal,
				cal,
				mechanism
			});
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06004124 RID: 16676 RVA: 0x0011A543 File Offset: 0x00118743
		public static LocalizedString StreamStateInvalid
		{
			get
			{
				return new LocalizedString("StreamStateInvalid", "Ex3391A3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x06004125 RID: 16677 RVA: 0x0011A561 File Offset: 0x00118761
		public static LocalizedString NormalRiskLowPriority
		{
			get
			{
				return new LocalizedString("NormalRiskLowPriority", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x06004126 RID: 16678 RVA: 0x0011A57F File Offset: 0x0011877F
		public static LocalizedString InboundMailSubmissionFromInternetComponent
		{
			get
			{
				return new LocalizedString("InboundMailSubmissionFromInternetComponent", "ExB43ECA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x06004127 RID: 16679 RVA: 0x0011A59D File Offset: 0x0011879D
		public static LocalizedString LatencyComponentCategorizerOnResolvedMessage
		{
			get
			{
				return new LocalizedString("LatencyComponentCategorizerOnResolvedMessage", "Ex1AC891", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x06004128 RID: 16680 RVA: 0x0011A5BB File Offset: 0x001187BB
		public static LocalizedString BodyFormatUnsupported
		{
			get
			{
				return new LocalizedString("BodyFormatUnsupported", "Ex8D1105", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x0011A5D9 File Offset: 0x001187D9
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040023EC RID: 9196
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(277);

		// Token: 0x040023ED RID: 9197
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Transport.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200057A RID: 1402
		public enum IDs : uint
		{
			// Token: 0x040023EF RID: 9199
			RoutingNoAdSites = 3699713858U,
			// Token: 0x040023F0 RID: 9200
			LatencyComponentHeartbeat = 101432279U,
			// Token: 0x040023F1 RID: 9201
			ComponentsDisabledNone = 677442004U,
			// Token: 0x040023F2 RID: 9202
			LatencyComponentRmsRequestDelegationToken = 680722199U,
			// Token: 0x040023F3 RID: 9203
			LatencyComponentRmsAcquireServerBoxRac = 2008205339U,
			// Token: 0x040023F4 RID: 9204
			LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreStats = 393272335U,
			// Token: 0x040023F5 RID: 9205
			DatabaseOpen = 642728361U,
			// Token: 0x040023F6 RID: 9206
			ColumnName = 2221898189U,
			// Token: 0x040023F7 RID: 9207
			ShadowSendConnector = 28104421U,
			// Token: 0x040023F8 RID: 9208
			LatencyComponentDeliveryQueueMailboxInsufficientResources = 781896450U,
			// Token: 0x040023F9 RID: 9209
			Restricted = 897286993U,
			// Token: 0x040023FA RID: 9210
			LatencyComponentStoreDriverDeliveryContentConversion = 3327987659U,
			// Token: 0x040023FB RID: 9211
			IntraorgSendConnectorName = 3070454738U,
			// Token: 0x040023FC RID: 9212
			LatencyComponentSmtpReceiveCommitLocal = 2741991560U,
			// Token: 0x040023FD RID: 9213
			LatencyComponentDeliveryQueueMailbox = 2269890262U,
			// Token: 0x040023FE RID: 9214
			LatencyComponentDeliveryAgent = 366323840U,
			// Token: 0x040023FF RID: 9215
			LatencyComponentRmsFindServiceLocation = 2455354172U,
			// Token: 0x04002400 RID: 9216
			NormalPriority = 229786213U,
			// Token: 0x04002401 RID: 9217
			LatencyComponentSmtpReceiveOnEndOfHeaders = 2359831369U,
			// Token: 0x04002402 RID: 9218
			Confidential = 3475865632U,
			// Token: 0x04002403 RID: 9219
			NormalRisk = 3022513318U,
			// Token: 0x04002404 RID: 9220
			ShadowRedundancyNoActiveServerInNexthopSolution = 2222703033U,
			// Token: 0x04002405 RID: 9221
			LatencyComponentStoreDriverOnCompletedMessage = 1699301985U,
			// Token: 0x04002406 RID: 9222
			HighRisk = 130958697U,
			// Token: 0x04002407 RID: 9223
			DatabaseRecoveryActionDelete = 1048481989U,
			// Token: 0x04002408 RID: 9224
			LatencyComponentSmtpReceiveOnProxyInboundMessage = 1785499833U,
			// Token: 0x04002409 RID: 9225
			LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionSmtpOut = 3547958413U,
			// Token: 0x0400240A RID: 9226
			LatencyComponentSmtpSend = 4212340469U,
			// Token: 0x0400240B RID: 9227
			MediumResourceUses = 3759339643U,
			// Token: 0x0400240C RID: 9228
			LatencyComponentSmtpReceiveCommitRemote = 3152927583U,
			// Token: 0x0400240D RID: 9229
			LatencyComponentCategorizer = 4145912840U,
			// Token: 0x0400240E RID: 9230
			None = 1414246128U,
			// Token: 0x0400240F RID: 9231
			LatencyComponentDumpster = 2123701631U,
			// Token: 0x04002410 RID: 9232
			EnumeratorBadPosition = 2393050832U,
			// Token: 0x04002411 RID: 9233
			LatencyComponentRmsAcquireCertificationMexData = 1226316175U,
			// Token: 0x04002412 RID: 9234
			ContentAggregationComponent = 2892365298U,
			// Token: 0x04002413 RID: 9235
			DiscardingDataFalse = 1244889643U,
			// Token: 0x04002414 RID: 9236
			DatabaseClosed = 1050903407U,
			// Token: 0x04002415 RID: 9237
			FailedToReadServerRole = 2048777343U,
			// Token: 0x04002416 RID: 9238
			LatencyComponentDeliveryQueueMailboxDynamicMailboxDatabaseThrottlingLimitExceeded = 821664177U,
			// Token: 0x04002417 RID: 9239
			LatencyComponentMailSubmissionServiceNotify = 2000842882U,
			// Token: 0x04002418 RID: 9240
			LatencyComponentStoreDriverSubmissionStore = 217112309U,
			// Token: 0x04002419 RID: 9241
			SeekFailed = 3442182595U,
			// Token: 0x0400241A RID: 9242
			LatencyComponentMailSubmissionServiceNotifyRetrySchedule = 1243242609U,
			// Token: 0x0400241B RID: 9243
			LatencyComponentStoreDriverOnDemotedMessage = 1618072168U,
			// Token: 0x0400241C RID: 9244
			Public = 683438393U,
			// Token: 0x0400241D RID: 9245
			AcceptedDomainTableNotLoaded = 1815474557U,
			// Token: 0x0400241E RID: 9246
			LatencyComponentServiceRestart = 1747692937U,
			// Token: 0x0400241F RID: 9247
			NormalRiskNonePriority = 525138956U,
			// Token: 0x04002420 RID: 9248
			LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionContentConversion = 4093059930U,
			// Token: 0x04002421 RID: 9249
			LatencyComponentDeliveryQueueMailboxMapiExceptionTimeout = 526223919U,
			// Token: 0x04002422 RID: 9250
			LatencyComponentSmtpSendMailboxDelivery = 177467553U,
			// Token: 0x04002423 RID: 9251
			LatencyComponentDeliveryQueueExternal = 680901367U,
			// Token: 0x04002424 RID: 9252
			HighAndBulkRisk = 464945964U,
			// Token: 0x04002425 RID: 9253
			JetOperationFailure = 850634908U,
			// Token: 0x04002426 RID: 9254
			LatencyComponentSmtpReceive = 4222452556U,
			// Token: 0x04002427 RID: 9255
			LatencyComponentSmtpReceiveDataExternal = 2713736967U,
			// Token: 0x04002428 RID: 9256
			LatencyComponentStoreDriverDeliveryAD = 2517616835U,
			// Token: 0x04002429 RID: 9257
			LatencyComponentDeliveryQueueMailboxDeliverAgentTransientFailure = 3066388364U,
			// Token: 0x0400242A RID: 9258
			LatencyComponentCategorizerFinal = 2895510700U,
			// Token: 0x0400242B RID: 9259
			BulkRisk = 412845223U,
			// Token: 0x0400242C RID: 9260
			LatencyComponentSubmissionQueue = 2314636918U,
			// Token: 0x0400242D RID: 9261
			LatencyComponentStoreDriverSubmit = 871184586U,
			// Token: 0x0400242E RID: 9262
			DumpsterJobStatusQueued = 1421458560U,
			// Token: 0x0400242F RID: 9263
			LatencyComponentStoreDriverDeliveryMailboxDatabaseThrottling = 320885804U,
			// Token: 0x04002430 RID: 9264
			LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionOnDemotedMessage = 939699173U,
			// Token: 0x04002431 RID: 9265
			SecureMailInvalidNumberOfLayers = 3432225769U,
			// Token: 0x04002432 RID: 9266
			LowRiskLowPriority = 3242396317U,
			// Token: 0x04002433 RID: 9267
			LatencyComponentMailSubmissionServiceFailedAttempt = 1951232521U,
			// Token: 0x04002434 RID: 9268
			RemoteDomainTableNotLoaded = 4208962580U,
			// Token: 0x04002435 RID: 9269
			OutboundMailDeliveryToRemoteDomainsComponent = 1681384042U,
			// Token: 0x04002436 RID: 9270
			AttachmentReadFailed = 4095319950U,
			// Token: 0x04002437 RID: 9271
			LatencyComponentMailboxRules = 2378292844U,
			// Token: 0x04002438 RID: 9272
			LowResourceUses = 3395591492U,
			// Token: 0x04002439 RID: 9273
			LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreDisposeSession = 1151336747U,
			// Token: 0x0400243A RID: 9274
			High = 4217035038U,
			// Token: 0x0400243B RID: 9275
			IdentityParameterNotFound = 198598092U,
			// Token: 0x0400243C RID: 9276
			NotOpenForWrite = 1049847567U,
			// Token: 0x0400243D RID: 9277
			TcpListenerError = 4063125577U,
			// Token: 0x0400243E RID: 9278
			LatencyComponentMexRuntimeThreadpoolQueue = 429833158U,
			// Token: 0x0400243F RID: 9279
			LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionPerfContextLdap = 1306604228U,
			// Token: 0x04002440 RID: 9280
			NormalResourceUses = 1836041741U,
			// Token: 0x04002441 RID: 9281
			DumpsterJobStatusCreated = 364271663U,
			// Token: 0x04002442 RID: 9282
			LowPriority = 3603045308U,
			// Token: 0x04002443 RID: 9283
			ExternalDestinationInboundProxySendConnector = 2295242475U,
			// Token: 0x04002444 RID: 9284
			IPFilterDatabaseInstanceName = 1092559024U,
			// Token: 0x04002445 RID: 9285
			ActivationFailed = 3889172535U,
			// Token: 0x04002446 RID: 9286
			LatencyComponentDelivery = 2198647971U,
			// Token: 0x04002447 RID: 9287
			AggregateResource = 3545397251U,
			// Token: 0x04002448 RID: 9288
			LatencyComponentProcessingSchedulerScoped = 1807713273U,
			// Token: 0x04002449 RID: 9289
			LatencyComponentSmtpReceiveOnRcpt2Command = 1437992463U,
			// Token: 0x0400244A RID: 9290
			LatencyComponentStoreDriverOnPromotedMessage = 68652566U,
			// Token: 0x0400244B RID: 9291
			PoisonMessageRegistryAccessFailed = 1004476481U,
			// Token: 0x0400244C RID: 9292
			HighResourceUses = 4020652394U,
			// Token: 0x0400244D RID: 9293
			EnvelopRecipientDisposed = 3706269143U,
			// Token: 0x0400244E RID: 9294
			SystemMemory = 2556558528U,
			// Token: 0x0400244F RID: 9295
			ReadOrgContainerFailed = 184239278U,
			// Token: 0x04002450 RID: 9296
			LatencyComponentStoreDriverOnCreatedMessage = 488909308U,
			// Token: 0x04002451 RID: 9297
			CategorizerMaxConfigLoadRetriesReached = 3548219859U,
			// Token: 0x04002452 RID: 9298
			LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionStoreOpenSession = 1542680578U,
			// Token: 0x04002453 RID: 9299
			RoutingLocalServerIsNotBridgehead = 1797311582U,
			// Token: 0x04002454 RID: 9300
			LatencyComponentCategorizerOnCategorizedMessage = 1030204127U,
			// Token: 0x04002455 RID: 9301
			LatencyComponentRmsAcquireB2BRac = 727067537U,
			// Token: 0x04002456 RID: 9302
			InternalDestinationInboundProxySendConnector = 2163339405U,
			// Token: 0x04002457 RID: 9303
			LatencyComponentDeliveryQueueMailboxMaxConcurrentMessageSizeLimitExceeded = 1080294189U,
			// Token: 0x04002458 RID: 9304
			LatencyComponentStoreDriverDeliveryRpc = 170486207U,
			// Token: 0x04002459 RID: 9305
			ConnectionInUse = 1602038336U,
			// Token: 0x0400245A RID: 9306
			LatencyComponentMailSubmissionService = 4260841639U,
			// Token: 0x0400245B RID: 9307
			NonePriority = 4278195556U,
			// Token: 0x0400245C RID: 9308
			LatencyComponentRmsAcquireTemplateInfo = 3768774525U,
			// Token: 0x0400245D RID: 9309
			LatencyComponentContentAggregation = 3451284522U,
			// Token: 0x0400245E RID: 9310
			LatencyComponentMailSubmissionServiceShadowResubmitDecision = 2443567820U,
			// Token: 0x0400245F RID: 9311
			LatencyComponentContentAggregationMailItemCommit = 3636715311U,
			// Token: 0x04002460 RID: 9312
			DatabaseRecoveryActionMove = 1674978893U,
			// Token: 0x04002461 RID: 9313
			ReadTransportServerConfigFailed = 3640623549U,
			// Token: 0x04002462 RID: 9314
			LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionHubSelector = 4082435799U,
			// Token: 0x04002463 RID: 9315
			MessageTrackingConfigNotFound = 1054523613U,
			// Token: 0x04002464 RID: 9316
			LatencyComponentDeliveryQueueInternal = 16119773U,
			// Token: 0x04002465 RID: 9317
			LatencyComponentDeliveryQueueMailboxMailboxServerOffline = 2383564204U,
			// Token: 0x04002466 RID: 9318
			PrivateBytesResource = 3307097748U,
			// Token: 0x04002467 RID: 9319
			LatencyComponentDeliveryQueueMailboxMailboxDatabaseOffline = 4156740374U,
			// Token: 0x04002468 RID: 9320
			RoutingMaxConfigLoadRetriesReached = 1447270652U,
			// Token: 0x04002469 RID: 9321
			LatencyComponentRmsAcquireServerLicensingMexData = 1851026156U,
			// Token: 0x0400246A RID: 9322
			LatencyComponentDeliveryQueueMailboxRecipientThreadLimitExceeded = 2614121165U,
			// Token: 0x0400246B RID: 9323
			SmtpReceiveParserNegativeBytes = 1010194958U,
			// Token: 0x0400246C RID: 9324
			InvalidRoleChange = 702550931U,
			// Token: 0x0400246D RID: 9325
			LatencyComponentPoisonQueue = 3077939474U,
			// Token: 0x0400246E RID: 9326
			BootScannerComponent = 3953400951U,
			// Token: 0x0400246F RID: 9327
			NotInTransaction = 1434651324U,
			// Token: 0x04002470 RID: 9328
			LatencyComponentSmtpReceiveOnRcptCommand = 3231014581U,
			// Token: 0x04002471 RID: 9329
			BodyReadFailed = 2675789057U,
			// Token: 0x04002472 RID: 9330
			TextConvertersFailed = 1355928173U,
			// Token: 0x04002473 RID: 9331
			RoutingNoRoutingGroups = 604013917U,
			// Token: 0x04002474 RID: 9332
			LatencyComponentStoreDriverDeliveryMessageConcurrency = 1040763200U,
			// Token: 0x04002475 RID: 9333
			Submission = 2064442254U,
			// Token: 0x04002476 RID: 9334
			ReadingADConfigFailed = 1817505332U,
			// Token: 0x04002477 RID: 9335
			LatencyComponentTooManyComponents = 4179070144U,
			// Token: 0x04002478 RID: 9336
			ReadTransportConfigConfigFailed = 531266226U,
			// Token: 0x04002479 RID: 9337
			InvalidMessageResubmissionState = 3659171826U,
			// Token: 0x0400247A RID: 9338
			LatencyComponentSmtpReceiveOnEndOfData = 2339552741U,
			// Token: 0x0400247B RID: 9339
			InvalidTransportRole = 551877430U,
			// Token: 0x0400247C RID: 9340
			LatencyComponentReplay = 2370750848U,
			// Token: 0x0400247D RID: 9341
			CloneMoveDestination = 3447320822U,
			// Token: 0x0400247E RID: 9342
			LowRiskNonePriority = 3132079455U,
			// Token: 0x0400247F RID: 9343
			LatencyComponentCategorizerBifurcation = 3933849364U,
			// Token: 0x04002480 RID: 9344
			SchemaInvalid = 3849329560U,
			// Token: 0x04002481 RID: 9345
			QuoteNestLevel = 2223967066U,
			// Token: 0x04002482 RID: 9346
			LatencyComponentUnknown = 1966949091U,
			// Token: 0x04002483 RID: 9347
			LatencyComponentTotal = 1884563717U,
			// Token: 0x04002484 RID: 9348
			LatencyComponentRmsAcquireLicense = 2229981216U,
			// Token: 0x04002485 RID: 9349
			InboundMailSubmissionFromReplayDirectoryComponent = 1359462263U,
			// Token: 0x04002486 RID: 9350
			NoColumns = 3952286128U,
			// Token: 0x04002487 RID: 9351
			LatencyComponentRmsAcquireClc = 3021848955U,
			// Token: 0x04002488 RID: 9352
			CloneMoveComplete = 1362050859U,
			// Token: 0x04002489 RID: 9353
			LatencyComponentQuarantineReleaseOrReport = 1615937519U,
			// Token: 0x0400248A RID: 9354
			LatencyComponentSubmissionAssistant = 662654939U,
			// Token: 0x0400248B RID: 9355
			DumpsterJobResponseSuccess = 529499203U,
			// Token: 0x0400248C RID: 9356
			AgentComponentFailed = 1767865225U,
			// Token: 0x0400248D RID: 9357
			ClientProxySendConnector = 2584904976U,
			// Token: 0x0400248E RID: 9358
			Basic = 4128944152U,
			// Token: 0x0400248F RID: 9359
			LatencyComponentCategorizerOnSubmittedMessage = 2705384945U,
			// Token: 0x04002490 RID: 9360
			NormalRiskNormalPriority = 3565174541U,
			// Token: 0x04002491 RID: 9361
			LatencyComponentSmtpReceiveCommit = 1411027439U,
			// Token: 0x04002492 RID: 9362
			RoutingNoLocalServer = 3882493397U,
			// Token: 0x04002493 RID: 9363
			SecureMailSecondLayerMustBeEnveloped = 3622146271U,
			// Token: 0x04002494 RID: 9364
			MailItemDeferred = 3840724219U,
			// Token: 0x04002495 RID: 9365
			InboundMailSubmissionFromMailboxComponent = 3706643741U,
			// Token: 0x04002496 RID: 9366
			AttachmentProtectionFailed = 56765413U,
			// Token: 0x04002497 RID: 9367
			LatencyComponentSubmissionAssistantThrottling = 4115715614U,
			// Token: 0x04002498 RID: 9368
			LatencyComponentCategorizerLocking = 1586175673U,
			// Token: 0x04002499 RID: 9369
			ValueNull = 955728834U,
			// Token: 0x0400249A RID: 9370
			TooManyAgents = 2197880345U,
			// Token: 0x0400249B RID: 9371
			DumpsterJobStatusCompleted = 3324883850U,
			// Token: 0x0400249C RID: 9372
			LatencyComponentStoreDriverOnDeliveredMessage = 2127171860U,
			// Token: 0x0400249D RID: 9373
			LatencyComponentRmsAcquireB2BLicense = 1858725880U,
			// Token: 0x0400249E RID: 9374
			LatencyComponentDeliveryQueueMailboxMapiExceptionLockViolation = 2602495526U,
			// Token: 0x0400249F RID: 9375
			Medium = 1946647341U,
			// Token: 0x040024A0 RID: 9376
			NotBufferedStream = 1261670220U,
			// Token: 0x040024A1 RID: 9377
			IncorrectBaseStream = 2888586632U,
			// Token: 0x040024A2 RID: 9378
			LatencyComponentProcess = 26231820U,
			// Token: 0x040024A3 RID: 9379
			CommitMailFailed = 3133088287U,
			// Token: 0x040024A4 RID: 9380
			NonAsciiData = 1974402452U,
			// Token: 0x040024A5 RID: 9381
			SeekBarred = 2736623442U,
			// Token: 0x040024A6 RID: 9382
			RoutingLocalRgNotSet = 3819242299U,
			// Token: 0x040024A7 RID: 9383
			MessagingDatabaseInstanceName = 262955557U,
			// Token: 0x040024A8 RID: 9384
			LatencyComponentUnreachableQueue = 2000471546U,
			// Token: 0x040024A9 RID: 9385
			LatencyComponentStoreDriverDelivery = 1113830170U,
			// Token: 0x040024AA RID: 9386
			DatabaseStillInUse = 1062938671U,
			// Token: 0x040024AB RID: 9387
			LatencyComponentDeferral = 680830688U,
			// Token: 0x040024AC RID: 9388
			DumpsterJobResponseRetryLater = 793374748U,
			// Token: 0x040024AD RID: 9389
			RoutingNoLocalAdSite = 1287594420U,
			// Token: 0x040024AE RID: 9390
			LatencyComponentRmsAcquireTemplates = 3350471676U,
			// Token: 0x040024AF RID: 9391
			InvalidRoutingOverrideEvent = 3717583665U,
			// Token: 0x040024B0 RID: 9392
			GetSclThresholdDefaultValueOutOfRange = 1384341089U,
			// Token: 0x040024B1 RID: 9393
			LatencyComponentSmtpReceiveDataInternal = 3449384489U,
			// Token: 0x040024B2 RID: 9394
			InvalidTenantLicensePair = 2279316324U,
			// Token: 0x040024B3 RID: 9395
			ColumnIndexesMustBeSequential = 3889416851U,
			// Token: 0x040024B4 RID: 9396
			LatencyComponentNonSmtpGateway = 3854009776U,
			// Token: 0x040024B5 RID: 9397
			LatencyComponentDeliveryAgentOnDeliverMailItem = 2176966386U,
			// Token: 0x040024B6 RID: 9398
			ShadowRedundancyComponentBanner = 2711829446U,
			// Token: 0x040024B7 RID: 9399
			CloneMoveSourceModified = 350383284U,
			// Token: 0x040024B8 RID: 9400
			DatabaseRecoveryActionNone = 1674978920U,
			// Token: 0x040024B9 RID: 9401
			LatencyComponentMailSubmissionServiceThrottling = 1142385700U,
			// Token: 0x040024BA RID: 9402
			LatencyComponentDsnGenerator = 1673427229U,
			// Token: 0x040024BB RID: 9403
			RowDeleted = 2385069791U,
			// Token: 0x040024BC RID: 9404
			LatencyComponentCategorizerContentConversion = 4154552087U,
			// Token: 0x040024BD RID: 9405
			LatencyComponentExternalServers = 3393444732U,
			// Token: 0x040024BE RID: 9406
			ReadMicrosoftExchangeRecipientFailed = 3672571637U,
			// Token: 0x040024BF RID: 9407
			SeekGeneralFailure = 3993229380U,
			// Token: 0x040024C0 RID: 9408
			LatencyComponentCategorizerRouting = 3321191670U,
			// Token: 0x040024C1 RID: 9409
			LatencyComponentOriginalMailDsn = 1692037724U,
			// Token: 0x040024C2 RID: 9410
			LatencyComponentPickup = 1471624947U,
			// Token: 0x040024C3 RID: 9411
			LowRisk = 615004755U,
			// Token: 0x040024C4 RID: 9412
			LatencyComponentDeliveryAgentOnOpenConnection = 2919439981U,
			// Token: 0x040024C5 RID: 9413
			LatencyComponentCategorizerOnRoutedMessage = 2349744297U,
			// Token: 0x040024C6 RID: 9414
			CategorizerConfigValidationFailed = 1601350579U,
			// Token: 0x040024C7 RID: 9415
			LatencyComponentNone = 2863890221U,
			// Token: 0x040024C8 RID: 9416
			LatencyComponentDeliveryQueueLocking = 2071866321U,
			// Token: 0x040024C9 RID: 9417
			LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionAD = 2768133224U,
			// Token: 0x040024CA RID: 9418
			MessageResubmissionComponentBanner = 3862731819U,
			// Token: 0x040024CB RID: 9419
			TotalExcludingPriorityNone = 3955727133U,
			// Token: 0x040024CC RID: 9420
			CloneMoveSourceNull = 3906053774U,
			// Token: 0x040024CD RID: 9421
			LatencyComponentMailboxTransportSubmissionStoreDriverSubmissionSmtp = 92299947U,
			// Token: 0x040024CE RID: 9422
			LatencyComponentAgent = 3127930220U,
			// Token: 0x040024CF RID: 9423
			InboundMailSubmissionFromHubsComponent = 650877237U,
			// Token: 0x040024D0 RID: 9424
			AlreadyJoined = 3875073169U,
			// Token: 0x040024D1 RID: 9425
			LatencyComponentStoreDriverSubmissionRpc = 3994617651U,
			// Token: 0x040024D2 RID: 9426
			NormalAndLowRisk = 2896583167U,
			// Token: 0x040024D3 RID: 9427
			LatencyComponentMailboxTransportSubmissionStoreDriverSubmission = 175131561U,
			// Token: 0x040024D4 RID: 9428
			InvalidRoutingOverride = 335920517U,
			// Token: 0x040024D5 RID: 9429
			LatencyComponentSmtpReceiveOnDataCommand = 3689512814U,
			// Token: 0x040024D6 RID: 9430
			LowRiskNormalPriority = 4114179756U,
			// Token: 0x040024D7 RID: 9431
			AalCalClassificationDisplayName = 3171622157U,
			// Token: 0x040024D8 RID: 9432
			HighlyConfidential = 1744966745U,
			// Token: 0x040024D9 RID: 9433
			LatencyComponentStoreDriverOnInitializedMessage = 2327669046U,
			// Token: 0x040024DA RID: 9434
			LatencyComponentStoreDriverDeliveryStore = 4016006905U,
			// Token: 0x040024DB RID: 9435
			InvalidRowState = 3611628552U,
			// Token: 0x040024DC RID: 9436
			LatencyComponentMailboxTransportSubmissionService = 3334507377U,
			// Token: 0x040024DD RID: 9437
			LatencyComponentStoreDriverSubmissionAD = 3514558095U,
			// Token: 0x040024DE RID: 9438
			DumpsterJobStatusProcessing = 2595056666U,
			// Token: 0x040024DF RID: 9439
			RoutingNoLocalRgObject = 2912766470U,
			// Token: 0x040024E0 RID: 9440
			PendingTransactions = 2117972488U,
			// Token: 0x040024E1 RID: 9441
			TrailingEscape = 3750964199U,
			// Token: 0x040024E2 RID: 9442
			CloneMoveTargetNotNew = 1679638210U,
			// Token: 0x040024E3 RID: 9443
			LatencyComponentCategorizerResolver = 2607217314U,
			// Token: 0x040024E4 RID: 9444
			TransportComponentLoadFailed = 99902445U,
			// Token: 0x040024E5 RID: 9445
			LatencyComponentProcessingScheduler = 2986853275U,
			// Token: 0x040024E6 RID: 9446
			LatencyComponentSmtpSendConnect = 2104269935U,
			// Token: 0x040024E7 RID: 9447
			Minimum = 936688982U,
			// Token: 0x040024E8 RID: 9448
			InboundMailSubmissionFromPickupDirectoryComponent = 1677063078U,
			// Token: 0x040024E9 RID: 9449
			BreadCrumbSize = 1209655894U,
			// Token: 0x040024EA RID: 9450
			CloneMoveSourceNotSaved = 885455923U,
			// Token: 0x040024EB RID: 9451
			LatencyComponentShadowQueue = 3063068326U,
			// Token: 0x040024EC RID: 9452
			InvalidCursorState = 3504101496U,
			// Token: 0x040024ED RID: 9453
			LatencyComponentUnderThreshold = 3589283362U,
			// Token: 0x040024EE RID: 9454
			HighPriority = 3124262306U,
			// Token: 0x040024EF RID: 9455
			CircularClone = 1180878696U,
			// Token: 0x040024F0 RID: 9456
			InvalidDeleteState = 3959631871U,
			// Token: 0x040024F1 RID: 9457
			LatencyComponentExternalPartnerServers = 642210074U,
			// Token: 0x040024F2 RID: 9458
			MailboxProxySendConnector = 1582326851U,
			// Token: 0x040024F3 RID: 9459
			ExternalDestinationOutboundProxySendConnector = 3238954214U,
			// Token: 0x040024F4 RID: 9460
			InvalidTransportServerRole = 2588281919U,
			// Token: 0x040024F5 RID: 9461
			IncorrectColumn = 3502104427U,
			// Token: 0x040024F6 RID: 9462
			CountWrong = 2368056258U,
			// Token: 0x040024F7 RID: 9463
			LatencyComponentRmsAcquirePreLicense = 1954095043U,
			// Token: 0x040024F8 RID: 9464
			SecureMailOuterLayerMustBeSigned = 3452310564U,
			// Token: 0x040024F9 RID: 9465
			LatencyComponentMailboxMove = 3835436216U,
			// Token: 0x040024FA RID: 9466
			InternalDestinationOutboundProxySendConnector = 1450242468U,
			// Token: 0x040024FB RID: 9467
			IncorrectBrace = 3494923132U,
			// Token: 0x040024FC RID: 9468
			KeyLength = 309843435U,
			// Token: 0x040024FD RID: 9469
			InvalidRank = 1227018995U,
			// Token: 0x040024FE RID: 9470
			MimeWriteStreamOpen = 3974043093U,
			// Token: 0x040024FF RID: 9471
			StreamStateInvalid = 1578443098U,
			// Token: 0x04002500 RID: 9472
			NormalRiskLowPriority = 2070671180U,
			// Token: 0x04002501 RID: 9473
			InboundMailSubmissionFromInternetComponent = 2721873196U,
			// Token: 0x04002502 RID: 9474
			LatencyComponentCategorizerOnResolvedMessage = 3160113472U,
			// Token: 0x04002503 RID: 9475
			BodyFormatUnsupported = 4049537308U
		}

		// Token: 0x0200057B RID: 1403
		private enum ParamIDs
		{
			// Token: 0x04002505 RID: 9477
			PhysicalMemoryUses,
			// Token: 0x04002506 RID: 9478
			DatabaseAttachFailed,
			// Token: 0x04002507 RID: 9479
			InvalidCharset,
			// Token: 0x04002508 RID: 9480
			ResourcesInNormalPressure,
			// Token: 0x04002509 RID: 9481
			ResourceUses,
			// Token: 0x0400250A RID: 9482
			DatabaseAndDatabaseLogDeleted,
			// Token: 0x0400250B RID: 9483
			RoutingDecryptConnectorPasswordFailure,
			// Token: 0x0400250C RID: 9484
			DatabaseLoggingResource,
			// Token: 0x0400250D RID: 9485
			TlsCertificateNameNotFound,
			// Token: 0x0400250E RID: 9486
			ColumnsMustBeOrdered,
			// Token: 0x0400250F RID: 9487
			RoutingIdenticalExchangeLegacyDns,
			// Token: 0x04002510 RID: 9488
			DataBaseError,
			// Token: 0x04002511 RID: 9489
			DuplicateColumnIndexes,
			// Token: 0x04002512 RID: 9490
			FilePathOnLockedVolume,
			// Token: 0x04002513 RID: 9491
			BitlockerQueryFailed,
			// Token: 0x04002514 RID: 9492
			InvalidSmtpAddress,
			// Token: 0x04002515 RID: 9493
			RMSTemplateNotFound,
			// Token: 0x04002516 RID: 9494
			VersionBuckets,
			// Token: 0x04002517 RID: 9495
			CannotModifyCompletedRequest,
			// Token: 0x04002518 RID: 9496
			DefaultAuthoritativeDomainNotFound,
			// Token: 0x04002519 RID: 9497
			TemporaryStorageResource,
			// Token: 0x0400251A RID: 9498
			SchemaRequiredColumnNotFound,
			// Token: 0x0400251B RID: 9499
			QueueLength,
			// Token: 0x0400251C RID: 9500
			DatabaseIsNotMovable,
			// Token: 0x0400251D RID: 9501
			ExtractNotAllowed,
			// Token: 0x0400251E RID: 9502
			CannotRemoveRequestInRunningState,
			// Token: 0x0400251F RID: 9503
			DatabaseAndDatabaseLogMoved,
			// Token: 0x04002520 RID: 9504
			DiskFull,
			// Token: 0x04002521 RID: 9505
			DatabaseResource,
			// Token: 0x04002522 RID: 9506
			DatabaseSchemaNotSupported,
			// Token: 0x04002523 RID: 9507
			ColumnAccessInvalid,
			// Token: 0x04002524 RID: 9508
			DatabaseMoved,
			// Token: 0x04002525 RID: 9509
			IndexOutOfBounds,
			// Token: 0x04002526 RID: 9510
			OutboundConnectorNotFound,
			// Token: 0x04002527 RID: 9511
			SchemaVersion,
			// Token: 0x04002528 RID: 9512
			TransportComponentLoadFailedWithName,
			// Token: 0x04002529 RID: 9513
			LatencyComponentMExReserved,
			// Token: 0x0400252A RID: 9514
			ValueIsTooLarge,
			// Token: 0x0400252B RID: 9515
			DataBaseInUse,
			// Token: 0x0400252C RID: 9516
			SchemaTypeMismatch,
			// Token: 0x0400252D RID: 9517
			UsedDiskSpaceResource,
			// Token: 0x0400252E RID: 9518
			ConfigurationLoaderFailed,
			// Token: 0x0400252F RID: 9519
			ResourcesInAboveNormalPressure,
			// Token: 0x04002530 RID: 9520
			ComponentsDisabledByBackPressure,
			// Token: 0x04002531 RID: 9521
			InvalidColumn,
			// Token: 0x04002532 RID: 9522
			SubmissionQueueUses,
			// Token: 0x04002533 RID: 9523
			RoutingIdenticalFqdns,
			// Token: 0x04002534 RID: 9524
			NotEnoughRightsToReEncrypt,
			// Token: 0x04002535 RID: 9525
			VersionBucketUses,
			// Token: 0x04002536 RID: 9526
			EventAgentComponent,
			// Token: 0x04002537 RID: 9527
			DatabaseDeleted,
			// Token: 0x04002538 RID: 9528
			AalCalBanner
		}
	}
}
