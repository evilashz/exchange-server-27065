using System;
using System.Reflection;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004B9 RID: 1209
	public abstract class UMMonitoringConstants
	{
		// Token: 0x04001546 RID: 5446
		public const string AlertTypeIdFormat = "Exchange/UM/{0}";

		// Token: 0x04001547 RID: 5447
		public const string MsDiagnostics = "ms-diagnostics";

		// Token: 0x04001548 RID: 5448
		public const string MsDiagnosticsPublic = "ms-diagnostics-public";

		// Token: 0x04001549 RID: 5449
		public const int E14RedirectProvisionalResponseCode = 15637;

		// Token: 0x0400154A RID: 5450
		public const int E15RedirectProvisionalResponseCode = 15643;

		// Token: 0x0400154B RID: 5451
		public const int E14CallReceivedProvisionalResponseCode = 15638;

		// Token: 0x0400154C RID: 5452
		public const int E15CallReceivedProvisionalResponseCode = 15644;

		// Token: 0x0400154D RID: 5453
		public const int ExUMLyncServerProvisionalResponseCodeMin = 15000;

		// Token: 0x0400154E RID: 5454
		public const int ExUMLyncServerProvisionalResponseCodeMax = 15499;

		// Token: 0x0400154F RID: 5455
		public const int ExUMExchangeServerProvisionalResponseCodeMin = 15500;

		// Token: 0x04001550 RID: 5456
		public const int ExUMExchangeServerProvisionalResponseCodeMax = 15899;

		// Token: 0x04001551 RID: 5457
		public const int DiagnosticsTraceCode = 15900;

		// Token: 0x04001552 RID: 5458
		public const int CallStartCode = 15901;

		// Token: 0x04001553 RID: 5459
		public const int CallEstablishingCode = 15902;

		// Token: 0x04001554 RID: 5460
		public const int CallEstablishedCode = 15903;

		// Token: 0x04001555 RID: 5461
		public const int CallEstablishFailedCode = 15904;

		// Token: 0x04001556 RID: 5462
		public const int CallDisconnectedCode = 15905;

		// Token: 0x04001557 RID: 5463
		public const int CallAudioReceivedCode = 15906;

		// Token: 0x04001558 RID: 5464
		public const int StateAttributeLength = 1024;

		// Token: 0x04001559 RID: 5465
		public const string MsFe = "ms-fe";

		// Token: 0x0400155A RID: 5466
		public const string WildCard = "*";

		// Token: 0x0400155B RID: 5467
		public const string UMServiceTypeParameterName = "UMServiceType";

		// Token: 0x0400155C RID: 5468
		public const string UMSipTransportParameterName = "UMSipTransport";

		// Token: 0x0400155D RID: 5469
		public const string UMMediaProtocolParameterName = "UMMediaProtocol";

		// Token: 0x0400155E RID: 5470
		public const string UMCertificateThumbprintParameterName = "UMCertificateThumbprint";

		// Token: 0x0400155F RID: 5471
		public const string UMCertificateSubjectNameParameterName = "UMCertificateSubjectName";

		// Token: 0x04001560 RID: 5472
		public const string UMSipListeningPortParameterName = "UMSipListeningPort";

		// Token: 0x04001561 RID: 5473
		public const string UMSrcAccountExtensionParameterName = "SrcAccountExtension";

		// Token: 0x04001562 RID: 5474
		public const string UMDestAccountExtensionParameterName = "DestAccountExtension";

		// Token: 0x04001563 RID: 5475
		public const string UMSrcAccountSipUriParameterName = "SrcAccountSipUri";

		// Token: 0x04001564 RID: 5476
		public const string DestAccountSipUriParameterName = "DestAccountSipUri";

		// Token: 0x04001565 RID: 5477
		public const string UMDestAccountTenantDomainParameterName = "DestAccountTenantDomain";

		// Token: 0x04001566 RID: 5478
		public const string UMActiveMonitoringCertificateSubjectName = "um.o365.exchangemon.net";

		// Token: 0x04001567 RID: 5479
		public const string UMServerAddressForOutsideInProbeParameterName = "DestAccountGatewayForwardingAddress";

		// Token: 0x04001568 RID: 5480
		public const string UMServiceAddressForLocalMonitoringProbeParameterName = "UMServiceAddress";

		// Token: 0x04001569 RID: 5481
		public const string MaxCallsReached = "15500";

		// Token: 0x0400156A RID: 5482
		public const string NoWorkerProcess = "15501";

		// Token: 0x0400156B RID: 5483
		public const string DiskspaceFull = "15503";

		// Token: 0x0400156C RID: 5484
		public const string TransientError = "15604";

		// Token: 0x0400156D RID: 5485
		public const string ConnectionFailed = "ConnectionFailed";

		// Token: 0x0400156E RID: 5486
		public const string CertificateNotConfigured = "CertificateNotConfigured";

		// Token: 0x0400156F RID: 5487
		public const string CertificateMissing = "CertificateMissing";

		// Token: 0x04001570 RID: 5488
		public const string OneBoxDifferentCertificateConfiguredOnUMServices = "OneBoxDifferentCertificateConfiguredOnUMServices";

		// Token: 0x04001571 RID: 5489
		public const string WildCardInCertificateSubjectName = "WildCardInCertificateSubjectName";

		// Token: 0x04001572 RID: 5490
		public const string Localhost = "localhost";

		// Token: 0x04001573 RID: 5491
		public const string UMCallRouterTestProbe = "UMCallRouterTestProbe";

		// Token: 0x04001574 RID: 5492
		public const string UMCallRouterTestMonitor = "UMCallRouterTestMonitor";

		// Token: 0x04001575 RID: 5493
		public const string UMCallRouterTestEscalate = "UMCallRouterTestEscalate";

		// Token: 0x04001576 RID: 5494
		public const string UMCallRouterTestRestart = "UMCallRouterTestRestart";

		// Token: 0x04001577 RID: 5495
		public const string UMCallRouterTestOffline = "UMCallRouterTestOffline";

		// Token: 0x04001578 RID: 5496
		public const string UMSelfTestProbe = "UMSelfTestProbe";

		// Token: 0x04001579 RID: 5497
		public const string UMSelfTestMonitor = "UMSelfTestMonitor";

		// Token: 0x0400157A RID: 5498
		public const string UMSelfTestRestart = "UMSelfTestRestart";

		// Token: 0x0400157B RID: 5499
		public const string UMSelfTestWithoutRecoveryEscalate = "UMSelfTestWithoutRecoveryEscalate";

		// Token: 0x0400157C RID: 5500
		public const string UMSelfTestEscalate = "UMSelfTestEscalate";

		// Token: 0x0400157D RID: 5501
		public const string UMCallRouterRecentMissedCallNotificationProxyFailedMonitor = "UMCallRouterRecentMissedCallNotificationProxyFailedMonitor";

		// Token: 0x0400157E RID: 5502
		public const string UMCallRouterRecentMissedCallNotificationProxyFailedEscalate = "UMCallRouterRecentMissedCallNotificationProxyFailedEscalate";

		// Token: 0x0400157F RID: 5503
		public const string UMRecentPartnerTranscriptionFailedMonitor = "UMServiceRecentPartnerTranscriptionFailedMonitor";

		// Token: 0x04001580 RID: 5504
		public const string UMRecentPartnerTranscriptionFailedEscalate = "UMServiceRecentPartnerTranscriptionFailedEscalate";

		// Token: 0x04001581 RID: 5505
		public const string UMPipelineSLAMonitor = "UMPipelineSLAMonitor";

		// Token: 0x04001582 RID: 5506
		public const string UMPipelineSLAEscalate = "UMPipelineSLAEscalate";

		// Token: 0x04001583 RID: 5507
		public const string UMPipelineFullMonitor = "UMPipelineFullMonitor";

		// Token: 0x04001584 RID: 5508
		public const string UMPipelineFullEscalate = "UMPipelineFullEscalate";

		// Token: 0x04001585 RID: 5509
		public const string MediaEstablishedFailedMonitor = "MediaEstablishedFailedMonitor";

		// Token: 0x04001586 RID: 5510
		public const string MediaEstablishedFailedEscalate = "MediaEstablishedFailedEscalate";

		// Token: 0x04001587 RID: 5511
		public const string MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedMonitor = "MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedMonitor";

		// Token: 0x04001588 RID: 5512
		public const string MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedEscalate = "MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedEscalate";

		// Token: 0x04001589 RID: 5513
		public const string MediaEdgeResourceAllocationFailedMonitor = "MediaEdgeResourceAllocationFailedMonitor";

		// Token: 0x0400158A RID: 5514
		public const string MediaEdgeResourceAllocationFailedEscalate = "MediaEdgeResourceAllocationFailedEscalate";

		// Token: 0x0400158B RID: 5515
		public const string UMCertificateNearExpiryMonitor = "UMCertificateNearExpiryMonitor";

		// Token: 0x0400158C RID: 5516
		public const string UMCertificateNearExpiryEscalate = "UMCertificateNearExpiryEscalate";

		// Token: 0x0400158D RID: 5517
		public const string UMCallRouterCertificateNearExpiryMonitor = "UMCallRouterCertificateNearExpiryMonitor";

		// Token: 0x0400158E RID: 5518
		public const string UMCallRouterCertificateNearExpiryEscalate = "UMCallRouterCertificateNearExpiryEscalate";

		// Token: 0x0400158F RID: 5519
		public const string UMProtectedVoiceMessageEncryptDecryptFailedMonitor = "UMProtectedVoiceMessageEncryptDecryptFailedMonitor";

		// Token: 0x04001590 RID: 5520
		public const string UMProtectedVoiceMessageEncryptDecryptFailedEscalate = "UMProtectedVoiceMessageEncryptDecryptFailedEscalate";

		// Token: 0x04001591 RID: 5521
		public const string UMTranscriptionThrottledMonitor = "UMTranscriptionThrottledMonitor";

		// Token: 0x04001592 RID: 5522
		public const string UMTranscriptionThrottledEscalate = "UMTranscriptionThrottledEscalate";

		// Token: 0x04001593 RID: 5523
		public const string UMGrammarUsageMonitor = "UMGrammarUsageMonitor";

		// Token: 0x04001594 RID: 5524
		public const string UMGrammarUsageEscalate = "UMGrammarUsageEscalate";

		// Token: 0x04001595 RID: 5525
		public const int TimeToWaitForMedia = 30;

		// Token: 0x04001596 RID: 5526
		public static readonly string UMCallRouterHealthSet = ExchangeComponent.UMCallRouter.Name;

		// Token: 0x04001597 RID: 5527
		public static readonly string UMProtocolHealthSet = ExchangeComponent.UMProtocol.Name;

		// Token: 0x04001598 RID: 5528
		public static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04001599 RID: 5529
		public static readonly string UmEscalationTeam = ExchangeComponent.UMCallRouter.EscalationTeam;
	}
}
